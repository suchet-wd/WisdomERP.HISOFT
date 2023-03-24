Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls

Public Class wChangeBarcodeProdDetail

    Private _StateSetSelectBySelect As Boolean = True
    Private _StateSetSelectAll As Boolean = True

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


#Region "Init Control"
    Private Sub InitGrid()

        With ogvbarcode
            '.OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = True
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With


    End Sub
#End Region


    Private Sub LoadOrderProdDetailInfo(ByVal Key As Object)
        Dim _Qry As String
        Dim dt As DataTable

        _Qry = "SELECT  '0' AS FTSelect, A.FTBarcodeBundleNo, A.FNBunbleSeq, A.FTColorway , A.FTSizeBreakDown, A.FNQuantity"
        _Qry &= vbCrLf & ",ISNULL(("
        _Qry &= vbCrLf & ""
        _Qry &= vbCrLf & "  SELECT TOP 1 "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "   C.FTMarkNameTH"
        Else
            _Qry &= vbCrLf & "   C.FTMarkNameEN "
        End If

        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS AA WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS B WITH(NOLOCK)  ON AA.FTLayCutNo = B.FTLayCutNo INNER JOIN"
        _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMMark AS C WITH(NOLOCK)  ON B.FNHSysMarkId = C.FNHSysMarkId"
        _Qry &= vbCrLf & " WHERE  (AA.FTBarcodeBundleNo = A.FTBarcodeBundleNo)"
        _Qry &= vbCrLf & ""
        _Qry &= vbCrLf & "),'') AS FTMarkName"
        _Qry &= vbCrLf & " ,A.FTPOLineItemNo"
        _Qry &= vbCrLf & " ,ISNULL(A.FTChangeToLineItemNo,'') AS FTChangeToLineItemNo"
        _Qry &= vbCrLf & " ,ISNULL(A.FTChangeToLineItemNo,'') AS FTChangeToLineItemNoNew"
        _Qry &= vbCrLf & " ,ISNULL(A.FTColorwayNew,'') AS FTColorwayNew "
        _Qry &= vbCrLf & " ,ISNULL(A.FTOrderProdNo,'') AS FTOrderProdNo "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS A  WITH(NOLOCK)"
        _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS B WITH(NOLOCK) ON A.FTOrderProdNo=B.FTOrderProdNo "

        _Qry &= vbCrLf & "     INNER Join(SELECT  A.FTColorway, A.FTSizeBreakDown, A.FTNikePOLineItem "
        _Qry &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown As A With(NOLOCK) INNER Join "
        _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Divert_BreakDown AS B  WITH(NOLOCK)  ON A.FTOrderNo = B.FTOrderNo  "
        _Qry &= vbCrLf & " And A.FTSubOrderNo = B.FTSubOrderNo "
        _Qry &= vbCrLf & "   And A.FTColorway = B.FTColorway "
        _Qry &= vbCrLf & "  And A.FTSizeBreakDown = B.FTSizeBreakDown "
        _Qry &= vbCrLf & "  WHERE(A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(Key.ToString) & "') "
        _Qry &= vbCrLf & " And  A.FTNikePOLineItem <> B.FTNikePOLineItem "
        _Qry &= vbCrLf & "    GROUP BY A.FTColorway, A.FTSizeBreakDown, A.FTNikePOLineItem "
        _Qry &= vbCrLf & "    ) As ODV On A.FTColorway = ODV.FTColorway  And A.FTSizeBreakDown = ODV.FTSizeBreakDown  And A.FTPOLineItemNo = ODV.FTNikePOLineItem "


        _Qry &= vbCrLf & " WHERE B.FTOrderNo='" & HI.UL.ULF.rpQuoted(Key.ToString) & "' AND ISNULL(A.FTStateGenBarcode,'') ='1'  "
        _Qry &= vbCrLf & " Order By  A.FTColorway,A.FTPOLineItemNo "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Me.ogcbarcode.DataSource = dt.Copy
        ogcbarcode.Refresh()
        dt.Dispose()

    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})
        Else
            If FTOrderNo.Text <> "" Then
                If HI.Conn.SQLConn.GetField("SELECT TOP 1 FTOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' AND FNHSysCmpId=" & Val(FNHSysCmpId.Properties.Tag.ToString) & "", Conn.DB.DataBaseName.DB_MERCHAN, "") <> "" Then
                    FTLineNo.Properties.DataSource = Nothing
                    FTLineNo.Text = ""
                    FTColorwayNew.Text = ""
                    Call LoadDetailDivert(Me.FTOrderNo.Text, "", "")
                    Call LoadOrderProdDetailInfo(FTOrderNo.Text)
                    Call LoadListDivert(FTOrderNo.Text)
                    FNChangeBundleType.SelectedIndex = 0
                    FNQuantityBal.Value = 0
                    FNQuantity.Value = 0
                Else
                    Me.ogcbarcode.DataSource = Nothing
                    FTLineNo.Properties.DataSource = Nothing
                    FTLineNo.Text = ""
                    FTColorwayNew.Text = ""
                    FNChangeBundleType.SelectedIndex = 0
                    FNQuantityBal.Value = 0
                    FNQuantity.Value = 0
                End If
            Else
                Me.ogcbarcode.DataSource = Nothing
                FTLineNo.Properties.DataSource = Nothing
                FTLineNo.Text = ""
                FTColorwayNew.Text = ""

                FNChangeBundleType.SelectedIndex = 0
                FNQuantityBal.Value = 0
                FNQuantity.Value = 0
            End If

        End If
    End Sub

    Private Sub wGenerateBarcode_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

        Call InitGrid()

        FNChangeBundleType.SelectedIndex = 0


        FTBarcodeNo.EnterMoveNextControl = False
        FNQuantityBal.EnterMoveNextControl = False
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click

        HI.TL.HandlerControl.ClearControl(Me)

        Me.ogcbarcode.DataSource = Nothing
        FTLineNo.Properties.DataSource = Nothing

    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Call FTOrderNo_EditValueChanged(FTOrderNo, New System.EventArgs)
    End Sub

    Private Sub ocmgeneratebarcodewip_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        'CType(ogclaycut.DataSource, DataTable).AcceptChanges()

        'If CType(ogclaycut.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then
        '    If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Generate Barcode ใช่หรือไม่ ?", 1505300001) = True Then

        '        Dim _Spls As New HI.TL.SplashScreen("Generating....Barcode , Please Wait...")
        '        If Me.GenerateBarcode() Then
        '            Call LoadOrderProdDetailInfo(otbjobprod.SelectedTabPage.Name.ToString)
        '            _Spls.Close()
        '            HI.MG.ShowMsg.mInfo("Generate Barcode Complete..", 1405300002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
        '        Else
        '            _Spls.Close()
        '            HI.MG.ShowMsg.mInfo("ไม่สามารถทำการ Generate Barcode ได้ กรุณาทำการติดต่อ Admin...", 1405300003, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        '        End If

        '    End If
        'Else
        '    HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกข้อมูลมัดงานสำการ Generate Barcode", 1405300004, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        'End If
    End Sub

    Private Sub LoadListDivert(OrderNo As String)

        Dim dt As New DataTable
        Dim _Qry As String

        _Qry = " Select  FTNikePOLineItemNew "
        _Qry &= vbCrLf & ",FTColorwayNew "
        _Qry &= vbCrLf & ", FNSeq  "
        _Qry &= vbCrLf & ", FTNikePOLineItemNew + FTColorwayNew  AS FTKey "
        _Qry &= vbCrLf & "  FROM( "
        _Qry &= vbCrLf & "  Select '' AS FTNikePOLineItemNew,'' AS FTColorwayNew ,0 AS FNSeq "
        _Qry &= vbCrLf & "  UNION "
        _Qry &= vbCrLf & "  Select  B.FTNikePOLineItem As FTNikePOLineItemNew, CASE WHEN ISNULL(B.FTColorwayNew, B.FTColorway) = B.FTColorway THEN '' ELSE ISNULL(B.FTColorwayNew, B.FTColorway)  END As FTColorwayNew,1 As FNSeq "
        _Qry &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown As A WITH(NOLOCK) INNER Join "
        _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Divert_BreakDown AS B WITH(NOLOCK) ON A.FTOrderNo = B.FTOrderNo And A.FTSubOrderNo = B.FTSubOrderNo And A.FTColorway = B.FTColorway And A.FTSizeBreakDown = B.FTSizeBreakDown "
        _Qry &= vbCrLf & "  Where (A.FTOrderNo = '" & HI.UL.ULF.rpQuoted(OrderNo) & "') "
        _Qry &= vbCrLf & "  And (A.FTNikePOLineItem <>  B.FTNikePOLineItem ) "
        _Qry &= vbCrLf & "  GROUP BY B.FTNikePOLineItem, CASE WHEN ISNULL(B.FTColorwayNew, B.FTColorway) = B.FTColorway THEN '' ELSE ISNULL(B.FTColorwayNew, B.FTColorway)  END ) AS X "
        _Qry &= vbCrLf & "  Order By FNSeq, FTNikePOLineItemNew, FTColorwayNew "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Me.FTLineNo.Properties.DataSource = dt.Copy

        dt.Dispose()

    End Sub

    Private Sub LoadDetailDivert(OrderNo As String, ColorWay As String, LineNo As String)

        Dim dt As New DataTable
        Dim _Qry As String

        _Qry = " Select  FTNikePOLineItemNew "
        _Qry &= vbCrLf & ",FTColorwayNew "
        _Qry &= vbCrLf & ",FNSeq  "
        _Qry &= vbCrLf & " FROM(Select '' AS FTNikePOLineItemNew,'' AS FTColorwayNew ,0 AS FNSeq "
        _Qry &= vbCrLf & " UNION "
        _Qry &= vbCrLf & "  Select  B.FTNikePOLineItem As FTNikePOLineItemNew, CASE WHEN ISNULL(B.FTColorwayNew, B.FTColorway) = B.FTColorway THEN '' ELSE ISNULL(B.FTColorwayNew, B.FTColorway)  END As FTColorwayNew,1 As FNSeq "
        _Qry &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown As A WITH(NOLOCK) INNER Join "
        _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Divert_BreakDown AS B WITH(NOLOCK) ON A.FTOrderNo = B.FTOrderNo And A.FTSubOrderNo = B.FTSubOrderNo And A.FTColorway = B.FTColorway And A.FTSizeBreakDown = B.FTSizeBreakDown "
        _Qry &= vbCrLf & " Where (A.FTOrderNo = '" & HI.UL.ULF.rpQuoted(OrderNo) & "') "
        _Qry &= vbCrLf & "  And (A.FTColorway = '" & HI.UL.ULF.rpQuoted(ColorWay) & "') "
        _Qry &= vbCrLf & " And (A.FTNikePOLineItem = '" & HI.UL.ULF.rpQuoted(LineNo) & "') "
        _Qry &= vbCrLf & " And (A.FTNikePOLineItem <>  B.FTNikePOLineItem ) "
        _Qry &= vbCrLf & " GROUP BY B.FTNikePOLineItem, CASE WHEN ISNULL(B.FTColorwayNew, B.FTColorway) = B.FTColorway THEN '' ELSE ISNULL(B.FTColorwayNew, B.FTColorway)  END ) AS X "
        _Qry &= vbCrLf & " Order By FNSeq, FTNikePOLineItemNew, FTColorwayNew "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Me.ReposFTChangeToLineItemNo.DataSource = dt.Copy

        dt.Dispose()

    End Sub

    Private Sub ReposFTChangeToLineItemNo_EditValueChanged(sender As Object, e As EventArgs) Handles ReposFTChangeToLineItemNo.EditValueChanged
        Try

            With Me.ogvbarcode
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.LookUpEdit = DirectCast(sender, DevExpress.XtraEditors.LookUpEdit)
                'Dim _PartName As String = obj.GetColumnValue("FTPartName").ToString
                'Dim _FNHSysPartId As String = obj.GetColumnValue("FNHSysPartId").ToString
                'Dim _FNSendSuplTypeName As String = obj.GetColumnValue("FNSendSuplTypeName").ToString
                'Dim _FNSendSuplType As String = obj.GetColumnValue("FNSendSuplType").ToString
                Dim _Obj As System.Data.DataRowView = obj.GetSelectedDataRow()

                Dim _ChangeToLineItemNoNew As String = _Obj.Item("FTNikePOLineItemNew").ToString()
                Dim _ColorwayNew As String = _Obj.Item("FTColorwayNew").ToString()


                .SetFocusedRowCellValue("FTChangeToLineItemNoNew", _ChangeToLineItemNoNew)
                .SetFocusedRowCellValue("FTColorwayNew", _ColorwayNew)

            End With

            CType(Me.ogcbarcode.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReposFTChangeToLineItemNo_Leave(sender As Object, e As EventArgs) Handles ReposFTChangeToLineItemNo.Leave
    End Sub

    Private Sub FTBarcodeNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTBarcodeNo.EditValueChanged
    End Sub

    Private Function ValidateChangeData(OrderNo As String, BarKey As String, LineNoNew As String, ColorwayNew As String) As Boolean
        Dim cmd As String = ""
        Dim _dt As New DataTable
        Dim _Statepass As Boolean = True


        cmd = " Select  FTNikePOLineItem, FTOrderNo, '' FTSubOrderNo, FTColorway, FTSizeBreakDown, FTNikePOLineItemNew, FTColorwayNew, SUM(FNQuantity) As FNQuantity,FNQuantityB,FNBundleQuantity"
        cmd &= vbCrLf & " , [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.FN_GET_ColorWay_ChangeDivert(FTOrderNo, '', FTColorway, FTSizeBreakDown, FTNikePOLineItem,'" & HI.UL.ULF.rpQuoted(LineNoNew) & "','" & HI.UL.ULF.rpQuoted(ColorwayNew) & "','" & HI.UL.ULF.rpQuoted(BarKey) & "')  AS FNQuantityDivert"
        cmd &= vbCrLf & "  FROM(         Select  B.FTNikePOLineItem, B.FTOrderNo, B.FTSubOrderNo, B.FTColorway, B.FTSizeBreakDown, A.FTNikePOLineItem As FTNikePOLineItemNew "
        cmd &= vbCrLf & "  , Case When ISNULL(A.FTColorwayNew, A.FTColorway) = A.FTColorway Then '' ELSE ISNULL(A.FTColorwayNew, A.FTColorway) END AS FTColorwayNew "
        cmd &= vbCrLf & "   ,C.FTBarcodeBundleNo,A.FNQuantity,C.FNQuantityB,C.FNBundleQuantity"
        cmd &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Divert_BreakDown As A WITH(NOLOCK) INNER Join"
        cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS B WITH(NOLOCK) ON A.FTOrderNo = B.FTOrderNo "
        cmd &= vbCrLf & " And A.FTSubOrderNo = B.FTSubOrderNo"
        cmd &= vbCrLf & " 	And A.FTColorway = B.FTColorway "
        cmd &= vbCrLf & " And A.FTSizeBreakDown = B.FTSizeBreakDown"
        cmd &= vbCrLf & "  And A.FTNikePOLineItem <> B.FTNikePOLineItem"
        cmd &= vbCrLf & "  INNER Join("
        cmd &= vbCrLf & "   Select  BA.FTBarcodeBundleNo, BP.FTOrderNo,BP.FTSubOrderNo, BA.FTColorway, BA.FTSizeBreakDown, BA.FTPOLineItemNo, BA.FTChangeToLineItemNo,BA.FNQuantity AS FNBundleQuantity"

        If FNChangeBundleType.SelectedIndex = 0 Then
            cmd &= vbCrLf & " ,BA.FNQuantity AS FNQuantityB "
        Else
            cmd &= vbCrLf & " ," & FNQuantityBal.Value & " AS FNQuantityB "
        End If

        cmd &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBundle As BA WITH(NOLOCK) "
        cmd &= vbCrLf & "    OUTEr APPLY(Select TOP 1 BP.FTOrderNo ,'' AS FTSubOrderNo "
        cmd &= vbCrLf & "                From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd_Detail AS BP WITH(NOLOCK)  "
        cmd &= vbCrLf & "                Where BP.FTOrderProdNo = BA.FTOrderProdNo   "
        cmd &= vbCrLf & "   ) AS BP "


        'cmd &= vbCrLf & "   INNER Join"
        'cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTOrderProd_Detail AS BP WITH(NOLOCK) ON BA.FTOrderProdNo = BP.FTOrderProdNo "
        'cmd &= vbCrLf & " And BA.FTColorway = BP.FTColorway"
        'cmd &= vbCrLf & "   And BA.FTSizeBreakDown = BP.FTSizeBreakDown"
        cmd &= vbCrLf & "  WHERE  BA.FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(BarKey) & "' "
        cmd &= vbCrLf & "  ) As C On B.FTOrderNo = C.FTOrderNo  "

        '  cmd &= vbCrLf & " And B.FTSubOrderNo = C.FTSubOrderNo "

        cmd &= vbCrLf & "  And B.FTColorway = C.FTColorway And B.FTSizeBreakDown = C.FTSizeBreakDown "
        cmd &= vbCrLf & " And B.FTNikePOLineItem = C.FTPOLineItemNo "
        cmd &= vbCrLf & " Where (A.FTOrderNo = '" & HI.UL.ULF.rpQuoted(OrderNo) & "') "
        cmd &= vbCrLf & "   AND  (A.FTNikePOLineItem = '" & HI.UL.ULF.rpQuoted(LineNoNew) & "') "
        cmd &= vbCrLf & "   AND  (Case When ISNULL(A.FTColorwayNew, A.FTColorway) = A.FTColorway THEN '' ELSE ISNULL(A.FTColorwayNew, A.FTColorway) END  = '" & HI.UL.ULF.rpQuoted(ColorwayNew) & "') "
        cmd &= vbCrLf & " )   AS A "
        cmd &= vbCrLf & "   GROUP BY FTNikePOLineItem, FTOrderNo,  FTColorway, FTSizeBreakDown, FTNikePOLineItemNew, FTColorwayNew,FNQuantityB,FNBundleQuantity "

        _dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MERCHAN)

        If _dt.Rows.Count < 0 Then
            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลการ Divert ของมัดงานนี้ กรุณาทำการตรวจสอบ !!! ", 1702240457, Me.Text,, MessageBoxIcon.Warning)
            _Statepass = False
        Else
            Dim divertqty As Integer = 0
            Dim chgqty As Integer = 0
            Dim bdty As Integer = 0

            For Each R As DataRow In _dt.Rows
                divertqty = Val(R!FNQuantity.ToString)
                chgqty = Val(R!FNQuantityDivert.ToString)
                bdty = Val(R!FNQuantityB.ToString)

                FNQuantity.Value = Val(R!FNBundleQuantity.ToString)
                Exit For
            Next

            If divertqty < (chgqty + bdty) Then
                HI.MG.ShowMsg.mInfo("จำนวนที่ต้องการเปลี่ยนเกินยอด Divert กรุณาทำการตรวจสอบ !!! ", 1702240459, Me.Text, "Balance  : " & (divertqty - chgqty).ToString(), MessageBoxIcon.Warning)
                _Statepass = False
            End If
        End If

        _dt.Dispose()

        Return _Statepass
    End Function

    Private Sub UpdateData(BarKey As String, Colorway As String, LineNo As String, Optional StateBarcode As Integer = 0)
        Dim cmd As String = ""


        If FNChangeBundleType.SelectedIndex = 0 Or FNQuantity.Value = FNQuantityBal.Value Then
            cmd = "  Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle"
            cmd &= vbCrLf & " SET FTColorwayNew='" & HI.UL.ULF.rpQuoted(Colorway) & "'"
            cmd &= vbCrLf & ",FTChangeToLineItemNo='" & HI.UL.ULF.rpQuoted(LineNo) & "'"
            cmd &= vbCrLf & " WHERE FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(BarKey) & "'"
            cmd &= vbCrLf & " AND (ISNULL(FTColorwayNew,'')<>'" & HI.UL.ULF.rpQuoted(Colorway) & "'"
            cmd &= vbCrLf & " OR ISNULL(FTChangeToLineItemNo,'')<>'" & HI.UL.ULF.rpQuoted(LineNo) & "')"

            HI.Conn.SQLConn.ExecuteOnly(cmd, Conn.DB.DataBaseName.DB_PROD)

            cmd = "  Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail"
            cmd &= vbCrLf & " SET FTColorwayNew='" & HI.UL.ULF.rpQuoted(Colorway) & "'"
            cmd &= vbCrLf & " WHERE FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(BarKey) & "'"

            HI.Conn.SQLConn.ExecuteOnly(cmd, Conn.DB.DataBaseName.DB_PROD)

        Else
            cmd = "  Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.USP_BUNDLESPLITCHANGEPOLINE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(BarKey) & "','" & HI.UL.ULF.rpQuoted(Colorway) & "','" & HI.UL.ULF.rpQuoted(LineNo) & "'," & FNQuantity.Value & "," & FNQuantityBal.Value & "  "

            HI.Conn.SQLConn.ExecuteOnly(cmd, Conn.DB.DataBaseName.DB_PROD)
        End If

        Call LoadOrderProdDetailInfo(Me.FTOrderNo.Text.Trim())

    End Sub

    Private Sub ogvbarcode_ShowingEditor(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ogvbarcode.ShowingEditor

        Try
            With ogvbarcode
                If .FocusedRowHandle < 0 Then Exit Sub
                If .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                Dim ColorWay As String = ""
                Dim LineNo As String = ""

                ColorWay = "" & .GetFocusedRowCellValue("FTColorway").ToString()
                LineNo = "" & .GetFocusedRowCellValue("FTPOLineItemNo").ToString()
                Call LoadDetailDivert(Me.FTOrderNo.Text, ColorWay, LineNo)

            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub FTBarcodeNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTBarcodeNo.KeyDown, FNQuantityBal.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                If Me.FTOrderNo.Text <> "" Then

                    If FTBarcodeNo.Text <> "" Then

                        If FTLineNo.Text = "" And FTColorwayNew.Text = "" Then
                            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลรายการบาร์โค๊ดที่ต้องการทำการเปลี่ยน กรุณาทำการตรวจสอบ !!!", 1702220476, Me.Text,, MessageBoxIcon.Warning)
                            Exit Sub
                        End If

                        Dim StateBarcode As Integer = 0

                        If sender.name.ToString.ToLower = "FTBarcodeNo".ToLower Then

                            FNQuantityBal.Value = 0
                            FNQuantity.Value = 0

                            StateBarcode = 0

                        Else
                            StateBarcode = 1

                            If FNQuantityBal.Value <= 0 Then

                                HI.MG.ShowMsg.mInfo("กรุณาทำการระบุจำนวน ที่ต้องการทำการแบ่ง !!!", 1702228476, Me.Text,, MessageBoxIcon.Warning)
                                FNQuantityBal.Focus()
                                FNQuantityBal.SelectAll()
                                Exit Sub
                            End If

                        End If

                        If FNChangeBundleType.SelectedIndex <> 0 Then
                        End If

                        With CType(Me.ogcbarcode.DataSource, DataTable)
                            .AcceptChanges()
                            If .Rows.Count > 0 Then
                                If .Select("FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(FTBarcodeNo.Text) & "'").Length > 0 Then

                                    'For Each R As DataRow In .Select("FTBarcodeBundleNo='" & HI.UL.ULF.rpQuoted(FTBarcodeNo.Text) & "'")
                                    '    Exit For
                                    'Next

                                    If FTLineNo.Text = "" And FTColorwayNew.Text = "" Then
                                    Else

                                        If ValidateChangeData(FTOrderNo.Text, FTBarcodeNo.Text.Trim(), FTLineNo.Text, FTColorwayNew.Text) = False Then
                                            FTBarcodeNo.Focus()
                                            FTBarcodeNo.SelectAll()
                                            Exit Sub
                                        End If

                                        If StateBarcode = 0 And FNChangeBundleType.SelectedIndex = 1 Then
                                            FNQuantityBal.Value = FNQuantity.Value
                                            FNQuantityBal.Focus()
                                            FNQuantityBal.SelectAll()
                                            Exit Sub
                                        End If

                                    End If

                                    UpdateData(FTBarcodeNo.Text.Trim(), FTColorwayNew.Text, FTLineNo.Text)

                                    FTBarcodeNo.Focus()
                                    FTBarcodeNo.SelectAll()
                                    FNQuantityBal.Value = 0
                                    FNQuantity.Value = 0

                                Else
                                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลรายการบาร์โค๊ดที่ต้องการทำการเปลี่ยน กรุณาทำการตรวจสอบ !!!", 1702220476, Me.Text,, MessageBoxIcon.Warning)
                                End If

                            Else
                                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลรายการที่ต้องการทำการเปลี่ยน กรุณาทำการตรวจสอบ !!!", 1702220475, Me.Text,, MessageBoxIcon.Warning)
                            End If

                        End With

                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTBarcodeNo_lbl.Text)
                        FTBarcodeNo.Focus()
                    End If

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderNo_lbl.Text)
                    FTOrderNo.Focus()
                End If
        End Select
    End Sub

    Private Sub ogvbarcode_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvbarcode.RowStyle
        Try

            With ogvbarcode
                Try

                    If "" & .GetRowCellValue(e.RowHandle, "FTChangeToLineItemNoNew").ToString() <> "" Or "" & .GetRowCellValue(e.RowHandle, "FTColorwayNew").ToString() <> "" Then
                        e.Appearance.ForeColor = System.Drawing.Color.FromArgb(&H0, &H0, &HFF)
                    End If
                Catch ex As Exception
                End Try
            End With



        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTLineNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTLineNo.EditValueChanged

        Dim ColorWay As String = ""
        Dim LineNo As String = ""

        Dim _Obj As System.Data.DataRowView = FTLineNo.GetSelectedDataRow()

        Dim _ChangeToLineItemNoNew As String = _Obj.Item("FTNikePOLineItemNew").ToString()
        Dim _ColorwayNew As String = _Obj.Item("FTColorwayNew").ToString()
        FTColorwayNew.Text = _ColorwayNew

    End Sub

    Private Sub FNChangeBundleType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNChangeBundleType.SelectedIndexChanged
        FNQuantityBal.Value = 0
        FNQuantity.Value = 0

        FNQuantityBal.Visible = (FNChangeBundleType.SelectedIndex = 1)
        FNQuantityBal_lbl.Visible = (FNChangeBundleType.SelectedIndex = 1)
        FNQuantityBal.ReadOnly = (FNChangeBundleType.SelectedIndex = 0)


        FNQuantity.Visible = (FNChangeBundleType.SelectedIndex = 1)
        FNQuantity_lbl.Visible = (FNChangeBundleType.SelectedIndex = 1)

    End Sub

    Private Sub FNQuantityBal_EditValueChanged(sender As Object, e As EventArgs) Handles FNQuantityBal.EditValueChanged

    End Sub

    Private Sub FNQuantityBal_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FNQuantityBal.EditValueChanging
        Try
            If Val(e.NewValue.ToString) < 0 Then
                e.Cancel = True

            Else
                If Val(e.NewValue.ToString) > FNQuantity.Value Then
                    e.Cancel = True
                Else
                    e.Cancel = False
                End If
            End If
        Catch ex As Exception
            FNQuantityBal.Value = FNQuantity.Value
        End Try
    End Sub
End Class