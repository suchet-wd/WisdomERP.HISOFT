Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls

Public Class wDCPrepareIssueAddBarRcv

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private _TransferNo As String = ""
    Public Property TransferNo() As String
        Get
            Return _TransferNo
        End Get
        Set(value As String)
            _TransferNo = value
        End Set
    End Property

    Private _SysWHID As Integer = 0
    Public Property WHID() As Integer
        Get
            Return _SysWHID
        End Get
        Set(value As Integer)
            _SysWHID = value
        End Set
    End Property

    Private _WHTo As String = ""
    Public Property WHTo() As String
        Get
            Return _WHTo
        End Get
        Set(value As String)
            _WHTo = value
        End Set
    End Property

    Private _FNPriceTrans As Double = -1
    Public Property FNPriceTrans As Double
        Get
            Return _FNPriceTrans
        End Get
        Set(value As Double)
            _FNPriceTrans = value
        End Set
    End Property

    Private _StateProc As Boolean = False
    Public Property StateProc() As Boolean
        Get
            Return _StateProc
        End Get
        Set(value As Boolean)
            _StateProc = value
        End Set
    End Property

    Private _MainObject As Object = Nothing
    Public Property MainObject As Object
        Get
            Return _MainObject
        End Get
        Set(value As Object)
            _MainObject = value
        End Set
    End Property

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        StateProc = False
        Me.Close()
    End Sub

    Private Function AutoTransfer() As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Issue.. Please Wait.... ")
        Try

            Dim _Qry As String = ""

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            With CType(Me.ogcbarcode.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Select("FTSelect='1'")

                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPrepare_Barcode(FTInsUser, FDInsDate, FTInsTime, FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FNQuantity,  FTStateReserve,FTDocumentRefNo,FNHSysCmpId,FTIssueReferNo)  "
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "' "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(TransferNo) & "' "
                    _Qry &= vbCrLf & "," & Val(R!FNHSysWHId.ToString) & " "
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                    _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNQuantity.ToString)) & " "
                    _Qry &= vbCrLf & ",'','" & HI.UL.ULF.rpQuoted(R!FTDocumentNo.ToString) & "'," & Val(HI.ST.SysInfo.CmpID) & ",'' "

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        _Spls.Close()
                        Return False
                    End If

                Next

            End With

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()

            HI.MG.ShowMsg.mInfo("Issue Complete... ", 1412030015, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)

            Return True

        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            Return False

        End Try

        Return True
    End Function

    Private Sub ocmauto_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        If Me.ogcbarcode.DataSource Is Nothing Then Exit Sub
        With CType(ogcbarcode.DataSource, DataTable)
            .AcceptChanges()
            If .Select("FTSelect='1'").Length <= 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Barcode ที่ต้องการทำการ Transfer !!!", 1412030011, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If



        End With

        If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Issue ใช่หรือไม่ ?", 1412778812) = True Then
            If Me.AutoTransfer Then
                StateProc = True
                Try

                    Call CallByName(Me.MainObject, "LoadIssueDetail", CallType.Method, {Me.TransferNo})
                Catch ex As Exception
                End Try

                Call ocmload_Click(ocmload, New System.EventArgs)

                ' Me.Close()
            Else
                HI.MG.ShowMsg.mInfo("ระบบไม่สามารถทำการ Transfer ได้ เนื่องจากพบข้อผิดพลาดบางประการกรูณาทำการติดต่อ System Admin !!!", 1412030013, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If
        End If

    End Sub

    Private Sub FTStaSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles FTStaSelectAll.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.FTStaSelectAll.Checked Then
                _State = "1"
            End If

            With ogcbarcode
                If Not (.DataSource Is Nothing) And ogvbarcode.RowCount > 0 Then

                    With ogvbarcode
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click

        If Me.FTReceiveNo.Text <> "" Then

            Dim _Qry As String = ""
            Dim dtauto As DataTable

            _Qry = " Select '1' AS FTSelect ,Row_Number() Over (Order BY M.FTBarcodeNo) AS FNSeq"
            _Qry &= vbCrLf & " ,M.FTBarcodeNo "
            _Qry &= vbCrLf & " ,WS.FTWHCode"
            _Qry &= vbCrLf & ",M.FTOrderNo"
            _Qry &= vbCrLf & " ,M.FNQuantity - ISNULL(M.FNTransactionQty,0) AS FNQuantityOrg "
            _Qry &= vbCrLf & " ,M.FNQuantity - ISNULL(M.FNTransactionQty,0) AS FNQuantity "
            _Qry &= vbCrLf & "  , IM.FTRawMatCode"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " , IM.FTRawMatNameTH AS FTMatDesc"
            Else
                _Qry &= vbCrLf & " , IM.FTRawMatNameEN AS FTMatDesc"
            End If

            _Qry &= vbCrLf & ",ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
            _Qry &= vbCrLf & ",ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"
            _Qry &= vbCrLf & ",M.FTFabricFrontSize"
            _Qry &= vbCrLf & ",M.FNHSysWHId,M.FTBatchNo,M.FTGrade,M.FTDocumentNo,M.FNHSysCmpIdTo,M.FNOrderType,CmpTo.FTCmpCode"
            _Qry &= vbCrLf & "  FROM"
            _Qry &= vbCrLf & "  (SELECT FTBarcodeNo"
            _Qry &= vbCrLf & "   ,FNHSysWHId"
            _Qry &= vbCrLf & "   ,FTOrderNo"
            _Qry &= vbCrLf & "  ,FNQuantity"
            _Qry &= vbCrLf & "  ,ISNULL(("
            _Qry &= vbCrLf & "  SELECT SUM(FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FTBarcodeNo = A.FTBarcodeNo AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
            _Qry &= vbCrLf & "  ),0) "
            _Qry &= vbCrLf & "  +ISNULL(("
            _Qry &= vbCrLf & "  SELECT SUM(FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENDCPrepare_Barcode WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE FTBarcodeNo = A.FTBarcodeNo AND FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "'  AND ISNULL(FTIssueReferNo,'')='' "
            _Qry &= vbCrLf & "  ),0) AS  FNTransactionQty"

            _Qry &= vbCrLf & "   ,FTPurchaseNo"
            _Qry &= vbCrLf & "   ,FTDocumentNo"
            _Qry &= vbCrLf & "   ,FNHSysRawMatId,FTFabricFrontSize,FTBatchNo,FTGrade,FNHSysCmpIdTo,FNOrderType"
            _Qry &= vbCrLf & "   FROM"
            _Qry &= vbCrLf & "  (   SELECT B.FTBarcodeNo,B.FNHSysWHId,B.FTOrderNo,SUM(B.FNQuantity) AS FNQuantity"
            _Qry &= vbCrLf & "  ,B.FTPurchaseNo,B.FTDocumentNo ,B.FNHSysCmpId,B.FNHSysRawMatId,B.FTFabricFrontSize,B.FTBatchNo,B.FTGrade,C.FNHSysCmpId AS FNHSysCmpIdTo,C.FNOrderType "
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK)"
            _Qry &= vbCrLf & "   ,[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS C WITH(NOLOCK)"
            _Qry &= vbCrLf & "   WHERE B.FTOrderNo = C.FTOrderNo"
            _Qry &= vbCrLf & "  AND B.FTDocumentNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' AND B.FNHSysWHId=" & Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)) & ""
            _Qry &= vbCrLf & "  AND C.FNOrderType <> 4"
            _Qry &= vbCrLf & "   GROUP BY B.FTBarcodeNo,B.FNHSysWHId,B.FTOrderNo,B.FTPurchaseNo,B.FTDocumentNo "
            _Qry &= vbCrLf & "  ,B.FNHSysCmpId,B.FNHSysRawMatId,B.FTFabricFrontSize,B.FTBatchNo,B.FTGrade,C.FNHSysCmpId,C.FNOrderType "
            _Qry &= vbCrLf & "  ) AS A ) AS M"
            _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON M.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH (NOLOCK) ON IM.FNHSysUnitId = U.FNHSysUnitId"
            _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS WS WITH (NOLOCK) ON M.FNHSysWHId = WS.FNHSysWHId"
            _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS CmpTo WITH (NOLOCK) ON M.FNHSysCmpIdTo = CmpTo.FNHSysCmpId"
            _Qry &= vbCrLf & "    WHERE (M.FNQuantity - ISNULL(M.FNTransactionQty,0) ) > 0"
            _Qry &= vbCrLf & " ORDER BY  M.FTBarcodeNo"
            dtauto = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

            If dtauto.Rows.Count > 0 Then
                ogcbarcode.DataSource = dtauto.Copy
            Else
                ogcbarcode.DataSource = Nothing
                ' HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Barcode ที่สามารถทำการ โอนได้ กรุณาทำการตรวจสอบ !!!", 1410020009, Me.Text, , MessageBoxIcon.Warning)
            End If

            dtauto.Dispose()

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTReceiveNo_lbl.Text)
            FTReceiveNo.Focus()
        End If
    End Sub

    Private Sub FTReceiveNo_KeyDown(sender As Object, e As KeyEventArgs) Handles FTReceiveNo.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter
                Call ocmload_Click(ocmload, New System.EventArgs)
        End Select
    End Sub

    Private Sub ReposFNQuantity_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ReposFNQuantity.EditValueChanging
        Try
            With ogvbarcode

                Dim _Bal As Double = Val(.GetFocusedRowCellValue("FNQuantityOrg"))

                If Val(e.NewValue) > _Bal Then
                    e.Cancel = True
                Else
                    e.Cancel = False
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub
End Class