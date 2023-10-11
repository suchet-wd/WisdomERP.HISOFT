Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraEditors

Public Class wSMPQCSendSupl
    Private _FNSendSuplType As Integer = 0
    Private _Static As Boolean = False
    Private _StateSave As Boolean = True
    Private _wAccept As wSMPQCSendSupllist


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        _wAccept = New wSMPQCSendSupllist
        HI.TL.HandlerControl.AddHandlerObj(_wAccept)

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wAccept.Name.ToString.Trim, _wAccept)
        Catch ex As Exception
        Finally
        End Try


        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub FTBarcodeNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FTBarcodeNo.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Enter

                    Call LoadBarcodeInfo(FTBarcodeNo.Text)
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Function CheckBarCode(Key As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT Top 1  FTBarcodeSendSuplNo "
            _Cmd &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplToBranch_Barcode WITH(NOLOCK) "
            _Cmd &= vbCrLf & " WHERE FTBarcodeSendSuplNo ='" & Key & "'"
            _Cmd &= vbCrLf & "UNION"
            _Cmd &= vbCrLf & "SELECT Top 1  FTBarcodeSendSuplNo "
            _Cmd &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSupl_Barcode WITH(NOLOCK) "
            _Cmd &= vbCrLf & " WHERE FTBarcodeSendSuplNo ='" & Key & "'"

            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE).Rows.Count > 0
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub LoadBarcodeInfo(Key As String)

        Try
            If Not (CheckBarCode(Key)) Then
                FTStyleCode.Text = ""
                FTOrderNo.Text = ""
                FTOrderProdNo.Text = ""
                FTColorway.Text = ""
                FTSizeBreakDown.Text = ""
                FNHSysOperationId.Text = ""
                FNHSysMarkId.Text = ""
                FNHSysPartId.Text = ""
                FTBarcodeBundleNo.Text = ""
                FNHSysSuplId.Text = ""
                FNQuantity.Value = 0
                FNQCActualQty.Value = 0
                HI.MG.ShowMsg.mInfo("ข้อมูลบาร์โค้ดไม่ถูกต้อง....", 15061109471, Me.Text, "", MessageBoxIcon.Stop)
                Me.FTBarcodeNo.Focus()
                Me.FTBarcodeNo.SelectAll()
                Exit Sub
            End If

            Dim _Qry As String = ""
            Dim dt As DataTable
            Dim _StatePass As Boolean = False

            _Qry = " SELECT  TOP 1 B.FTBarcodeSendSuplNo"
            _Qry &= vbCrLf & " 	, B.FNHSysPartId"
            _Qry &= vbCrLf & " 	, B.FNSendSuplType"
            _Qry &= vbCrLf & " 	, B.FNHSysSuplId"
            _Qry &= vbCrLf & " 	, B.FTBarcodeBundleNo"
            _Qry &= vbCrLf & " 	, O.FTSMPOrderNo as FTOrderNo "
            _Qry &= vbCrLf & " 	, B.FTOrderProdNo"
            _Qry &= vbCrLf & " 	, B.FTSendSuplRef"
            _Qry &= vbCrLf & " 	, B.FNHSysCmpId"
            _Qry &= vbCrLf & " 	, S.FTSuplCode"
            _Qry &= vbCrLf & " 	, B.FTSendSuplNo"
            _Qry &= vbCrLf & " 	, B.FNBunbleSeq"
            _Qry &= vbCrLf & " 	, B.FTColorway"
            _Qry &= vbCrLf & " 	, B.FTSizeBreakDown"
            _Qry &= vbCrLf & " 	, B.FNQuantity"
            _Qry &= vbCrLf & " 	, ST.FTStyleCode"
            _Qry &= vbCrLf & " 	,MP.FTPartCode "
            _Qry &= vbCrLf & " 	,ISNULL(B.FNHSysOperationId,0) AS FNHSysOperationId"
            _Qry &= vbCrLf & " 	,ISNULL(B.FNSeq,0) AS FNSeq"
            _Qry &= vbCrLf & " 	,ISNULL(B.FNHSysOperationIdTo,0) AS FNHSysOperationIdTo"
            _Qry &= vbCrLf & " 	,Mpp.FTOperationCode "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " 	,MP.FTPartNameTH AS FTPartName "
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameTH  AS FTOperationName"
            Else
                _Qry &= vbCrLf & " 	,MP.FTPartNameEN AS FTPartName"
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameEN AS FTOperationName"
            End If

            _Qry &= vbCrLf & " ,0 AS FNHSysMarkId"
            _Qry &= vbCrLf & " 	 FROM   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder AS O WITH (NOLOCK) "
            _Qry &= vbCrLf & " 	    INNER JOIN    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle AS ST WITH (NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId "
            ''_Qry &= vbCrLf & " 	    RIGHT OUTER JOIN    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TPRODTOrderProd AS ODP WITH (NOLOCK)  ON O.FTOrderNo = ODP.FTOrderNo "
            _Qry &= vbCrLf & " 	    RIGHT OUTER JOIN    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplToBranch_Barcode AS B WITH (NOLOCK)  ON  O.FTSMPOrderNo =  B.FTOrderProdNo  "


            _Qry &= vbCrLf & " 	      LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMSupplier AS S WITH (NOLOCK) ON B.FNHSysSuplId = S.FNHSysSuplId"
            _Qry &= vbCrLf & " 		    "
            _Qry &= vbCrLf & " 	      LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMPart  AS MP WITH (NOLOCK) ON B.FNHSysPartId = MP.FNHSysPartId"
            _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TPRODMOperation AS MPP WITH (NOLOCK)  ON  B.FNHSysOperationId   = MPP.FNHSysOperationId"
            _Qry &= vbCrLf & "   WHERE B.FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(Key) & "' --AND ISNULL(BB.FTStateGenBarcode,'') ='1' "
            _Qry &= vbCrLf & "UNION "

            _Qry &= vbCrLf & " SELECT  TOP 1 B.FTBarcodeSendSuplNo"
            _Qry &= vbCrLf & " 	, B.FNHSysPartId"
            _Qry &= vbCrLf & " 	, B.FNSendSuplType"
            _Qry &= vbCrLf & " 	, B.FNHSysSuplId"
            _Qry &= vbCrLf & " 	, B.FTBarcodeBundleNo"
            _Qry &= vbCrLf & " 	, O.FTSMPOrderNo as FTOrderNo "
            _Qry &= vbCrLf & " 	, B.FTOrderProdNo"
            _Qry &= vbCrLf & " 	, B.FTSendSuplRef"
            _Qry &= vbCrLf & " 	, B.FNHSysCmpId"
            _Qry &= vbCrLf & " 	, S.FTSuplCode"
            _Qry &= vbCrLf & " 	, B.FTSendSuplNo"
            _Qry &= vbCrLf & " 	, B.FNBunbleSeq"
            _Qry &= vbCrLf & " 	, B.FTColorway"
            _Qry &= vbCrLf & " 	, B.FTSizeBreakDown"
            _Qry &= vbCrLf & " 	, B.FNQuantity"
            _Qry &= vbCrLf & " 	, ST.FTStyleCode"
            _Qry &= vbCrLf & " 	,MP.FTPartCode "
            _Qry &= vbCrLf & " 	,ISNULL(SD.FNHSysOperationId,0) AS FNHSysOperationId"
            _Qry &= vbCrLf & " 	,0 AS FNSeq"
            _Qry &= vbCrLf & " 	,ISNULL(SD.FNHSysOperationIdTo,0) AS FNHSysOperationIdTo"
            _Qry &= vbCrLf & " 	,Mpp.FTOperationCode "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & " 	,MP.FTPartNameTH AS FTPartName "
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameTH  AS FTOperationName"
            Else
                _Qry &= vbCrLf & " 	,MP.FTPartNameEN AS FTPartName"
                _Qry &= vbCrLf & " 	,MPP.FTOperationNameEN AS FTOperationName"
            End If

            _Qry &= vbCrLf & " ,0 AS FNHSysMarkId"
            _Qry &= vbCrLf & " 	 FROM   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder AS O WITH (NOLOCK) "
            _Qry &= vbCrLf & " 	      INNER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle AS ST WITH (NOLOCK)  ON O.FNHSysStyleId = ST.FNHSysStyleId "
            ''_Qry &= vbCrLf & " 	      RIGHT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTOrderProd AS ODP WITH (NOLOCK)  ON O.FTOrderNo = ODP.FTOrderNo "
            _Qry &= vbCrLf & " 	RIGHT OUTER JOIN ( Select  B.FTBarcodeSendSuplNo, FNHSysPartId , FNSendSuplType , SB.FNHSysSuplId , BD.FTBarcodeBundleNo , BD.FTOrderProdNo , FTSendSuplRef ,SB.FNHSysCmpId , SB.FTSendSuplNo "
            _Qry &= vbCrLf & " 	  , BD.FNBunbleSeq , BD.FTColorway, BD.FTSizeBreakDown , BD.FNQuantity  "
            _Qry &= vbCrLf & " 	   From    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSupl_Barcode AS B WITH (NOLOCK)   "
            _Qry &= vbCrLf & " 	          INNER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSupl AS SB WITH(NOLOCK) ON B.FTSendSuplNo = SB.FTSendSuplNo  "
            _Qry &= vbCrLf & " 	           LEFT OUTER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTBarcode_SendSupl AS BS WITH(NOLOCK) ON B.FTBarcodeSendSuplNo = BS.FTBarcodeSendSuplNo  "
            _Qry &= vbCrLf & " 	  INNER JOIN " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTBundle AS BD WITH(NOLOCK) ON BS.FTBarcodeBundleNo = BD.FTBarcodeBundleNo"
            _Qry &= vbCrLf & " 	 ) AS B  ON  O.FTSMPOrderNo =  B.FTOrderProdNo      "


            _Qry &= vbCrLf & " 	      INNER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMSupplier AS S WITH (NOLOCK) ON B.FNHSysSuplId = S.FNHSysSuplId"
            _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTOrderProd_SendSupl AS SD WITH(NOLOCK) ON B.FTSendSuplRef = SD.FTSendSuplRef AND O.FTSMPOrderNo = SD.FTOrderProdNo  "


            _Qry &= vbCrLf & " 		    LEFT OUTER JOIN"
            _Qry &= vbCrLf & " 	        " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMPart  AS MP WITH (NOLOCK) ON B.FNHSysPartId = MP.FNHSysPartId"
            _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TPRODMOperation AS MPP WITH (NOLOCK)  ON SD.FNHSysOperationId   = MPP.FNHSysOperationId"
            _Qry &= vbCrLf & "   WHERE B.FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(Key) & "' --AND ISNULL(BB.FTStateGenBarcode,'') ='1' "

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)
            FTStyleCode.Text = ""
            FTOrderNo.Text = ""
            FTOrderProdNo.Text = ""
            FTColorway.Text = ""
            FTSizeBreakDown.Text = ""
            FNHSysOperationId.Text = ""
            FNHSysMarkId.Text = ""
            FNHSysPartId.Text = ""
            FTBarcodeBundleNo.Text = ""
            FNHSysSuplId.Text = ""
            FNQuantity.Value = 0
            FNQCActualQty.Value = 0
            Me.FTRemark.Text = ""
            If dt.Rows.Count > 0 Then

                For Each R As DataRow In dt.Rows

                    FTStyleCode.Text = R!FTStyleCode.ToString
                    FTOrderNo.Text = R!FTOrderNo.ToString
                    FTOrderProdNo.Text = R!FTOrderProdNo.ToString
                    FTColorway.Text = R!FTColorway.ToString
                    FTSizeBreakDown.Text = R!FTSizeBreakDown.ToString
                    FNHSysOperationId.Text = R!FTOperationName.ToString
                    FNHSysMarkId.Text = R!FNHSysMarkId.ToString
                    FNHSysPartId.Text = R!FTPartName.ToString
                    FTBarcodeBundleNo.Text = R!FNBunbleSeq.ToString
                    FNQuantity.Value = Double.Parse(R!FNQuantity.ToString)
                    _FNSendSuplType = Integer.Parse(R!FNSendSuplType.ToString)
                    FTRemark.Text = GetRemark()
                    Me.FNQCActualQty.Value = GetDefectQty()
                    Me.FNHSysSuplId.Text = R!FTSuplCode.ToString
                    Call CreateGroupDefect(_FNSendSuplType)
                    Exit For

                Next
            Else

                If Me.FNQuantity.Value <= 0 Then

                    HI.MG.ShowMsg.mInfo("ยังไม่มีการรับงานเข้าระบบ.....กรุณาทำรับ", 15060900928, Me.Text, "", MessageBoxIcon.Stop)

                    Me.FTBarcodeNo.Focus()
                    Me.FTBarcodeNo.SelectAll()

                End If

            End If

            FTBarcodeNo.Focus()
            FTBarcodeNo.SelectAll()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private _TileGroup As DevExpress.XtraEditors.TileGroup


    Private Sub CreateGroupDefect(ByVal _FNSendSuplType As Integer)
        Try
            Dim _Cmd As String = ""
            Try
                TileControl.Groups.Remove(_TileGroup)
                While (TileControl.Groups.Count > 0)
                    TileControl.Groups.RemoveAt(0)
                End While

            Catch ex As Exception
            End Try
            TileControl.ShowGroupText = True

            _Cmd = "  SELECT    distinct CASE WHEN D.FNSubQCType = 0 Then  L.FTName Else  T.FTNameSub END AS FTName ,D.FNSubQCType "
            _Cmd &= vbCrLf & ",CASE WHEN D.FNSubQCType = 0 Then  L.FNSeq Else T.FNSeq END AS  FNSeq"
            _Cmd &= vbCrLf & "FROM   " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TQAMQCSuplDetail AS D LEFT OUTER JOIN "
            _Cmd &= vbCrLf & "(SELECT   -1 AS FNSeq ,  FNListIndex"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", FTNameTH as FTName "
            Else
                _Cmd &= vbCrLf & ", FTNameEN as FTName "
            End If

            _Cmd &= vbCrLf & "FROM   " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.HSysListData WITH(NOLOCK) "
            _Cmd &= vbCrLf & "where FTListName ='FNSendSuplType' ) AS L ON D.FNSendSuplType = L.FNListIndex"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN ("
            _Cmd &= vbCrLf & "SELECT  FNListIndex AS FNSeq,  FNListIndex "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", FTNameTH as FTNameSub"
            Else
                _Cmd &= vbCrLf & ", FTNameEN as FTNameSub"
            End If

            _Cmd &= vbCrLf & "FROM      " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.HSysListData WITH(NOLOCK) "
            _Cmd &= vbCrLf & "where FTListName ='FNSubQCType'"
            _Cmd &= vbCrLf & ") AS T ON D.FNSubQCType = T.FNListIndex"
            _Cmd &= vbCrLf & "WHERE  D.FNSendSuplType=" & Integer.Parse(_FNSendSuplType)
            _Cmd &= vbCrLf & "Order by FNSeq"

            Dim _oGDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MASTER)

            For Each G As DataRow In _oGDt.Rows
                _TileGroup = New DevExpress.XtraEditors.TileGroup
                _TileGroup.Text = "" & HI.UL.ULF.rpQuoted(G!FTName.ToString)
                TileControl.Groups.Add(_TileGroup)
                TileControl.ItemSize = 80

                _Cmd = "  SELECT     FNHSysQCSuplDetailId, FNSendSuplType, FNSubQCType, FTQCSupDetailCode "

                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    _Cmd &= vbCrLf & ", FTQCSupDetailNameTH  AS FTQASupDetailName"
                Else
                    _Cmd &= vbCrLf & ", FTQCSupDetailNameEN  AS FTQASupDetailName"
                End If
                _Cmd &= vbCrLf & "FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TQAMQCSuplDetail WITH(NOLOCK) "
                _Cmd &= vbCrLf & "WHERE FNSendSuplType=" & Integer.Parse(_FNSendSuplType)
                _Cmd &= vbCrLf & "And FNSubQCType=" & Integer.Parse(G!FNSubQCType.ToString)
                Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MASTER)

                For Each R As DataRow In _oDt.Rows

                    Dim _i As New DevExpress.XtraEditors.TileItem
                    _i.AllowAnimation = True
                    _i.BackgroundImageScaleMode = TileItemImageScaleMode.ZoomInside

                    _i.AppearanceItem.Normal.BackColor = Color.DodgerBlue
                    _i.AppearanceItem.Normal.BorderColor = Color.LightBlue
                    _i.AppearanceItem.Normal.ForeColor = Color.Black
                    _i.ItemSize = TileItemSize.Wide
                    _i.ContentAnimation = TileItemContentAnimationType.ScrollLeft
                    _i.Name = R!FTQCSupDetailCode.ToString
                    '_i.Text = R!FTQASupDetailName.ToString
                    _i.Text = R!FTQCSupDetailCode.ToString

                    _i.Id = CInt(R!FNHSysQCSuplDetailId)
                    Dim Elmt As DevExpress.XtraEditors.TileItemElement
                    Elmt = New DevExpress.XtraEditors.TileItemElement
                    'Elmt.Text = R!FTQCSupDetailCode.ToString
                    Elmt.Text = R!FTQASupDetailName.ToString
                    Elmt.TextAlignment = TileItemContentAlignment.BottomLeft
                    _i.Elements.Add(Elmt)
                    _TileGroup.Items.Add(_i)
                Next
            Next
            AddHandler TileControl.RightItemClick, AddressOf TileControl_RightItemClick
            AddHandler TileControl.ItemClick, AddressOf TileControl_ItemClick
        Catch ex As Exception

        End Try
    End Sub


    Private Sub TileControl_ItemClick(sender As Object, e As TileItemEventArgs) Handles TileControl.ItemClick
        Try
            If Me.FNQuantity.Value <= 0 Then
                Exit Sub
            End If
            If e.Item.Checked = False Then
                If (_StateSave) Then
                    Me.FNQCActualQty.Value += +1
                    _StateSave = False
                End If
                Call _SaveData()
                Call _AddDefect(e.Item.Id.ToString)
                e.Item.Checked = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TileControl_RightItemClick(sender As Object, e As TileItemEventArgs) Handles TileControl.RightItemClick
        Try
            If Me.FNQuantity.Value <= 0 Then
                Exit Sub
            End If
            If e.Item.Checked = True Then
                Call _RemoveDefect(e.Item.Id.ToString)
                e.Item.Checked = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function GetDefectQty() As Double
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select FNDefectQty From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect"
            _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
            Return Double.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE, "0"))
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function GetRemark() As String
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select FTRemark From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect"
            _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE, "")
        Catch ex As Exception
            Return ""
        End Try
    End Function


    Private Function _SaveData() As Boolean
        Try
            Dim _Cmd As String = ""

            'If Me.FNQCActualQty.Value = 0 Then
            '    Return False
            'End If

            _Cmd = "UPDATE " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect"
            _Cmd &= vbCrLf & "Set FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & ",FDUpdDate =" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",FNDefectQty=" & Double.Parse(Me.FNQCActualQty.Value)
            _Cmd &= vbCrLf & ",FTRemark='" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
            _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
            '_Cmd &= vbCrLf & "AND FDDateTrans=" & HI.UL.ULDate.FormatDateDB
            If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE) = False Then
                _Cmd = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect ( FTInsUser, FDInsDate, FTInsTime,   FTBarcodeSendSuplNo, FNDefectQty, FTRemark  )"
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
                _Cmd &= vbCrLf & "," & Double.Parse(Me.FNQCActualQty.Value)
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
                '_Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE) = False Then
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub _AddDefect(ByVal _DetailId As Integer)
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "Select *  From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect_Detail WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo ='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
            _Cmd &= vbCrLf & "AND FNHSysQCSuplDetailId=" & _DetailId
            _Cmd &= vbCrLf & "AND FNSeq=" & Integer.Parse(Me.FNQCActualQty.Value)
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
            If _oDt.Rows.Count > 0 Then Exit Sub

            _Cmd = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect_Detail"
            _Cmd &= " ( FTInsUser, FDInsDate, FTInsTime, FTBarcodeSendSuplNo, FNHSysQCSuplDetailId,FNSeq)"
            _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
            _Cmd &= vbCrLf & "," & _DetailId
            _Cmd &= vbCrLf & "," & Me.FNQCActualQty.Value
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub _RemoveDefect(ByVal _DetailId As Integer)
        Try
            Dim _Cmd As String = ""
            _Cmd = "Delete From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect_Detail"
            _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo ='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
            _Cmd &= vbCrLf & "AND FNHSysQCSuplDetailId=" & _DetailId
            _Cmd &= vbCrLf & "AND FNSeq=" & Integer.Parse(Me.FNQCActualQty.Value)
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
            _Cmd = "Select Top 1 FNHSysQCSuplDetailId From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect_Detail WITH(NOLOCK)  "
            _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo ='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
            If Integer.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE, "0")) <= 0 Then
                _Cmd = "Delete From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect"
                _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
                _StateSave = True
                Me.FNQCActualQty.Value = 0
            Else
                _Cmd = "Select Top 1 FNHSysQCSuplDetailId From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect_Detail WITH(NOLOCK)  "
                _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo ='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
                _Cmd &= vbCrLf & "AND FNSeq=" & Integer.Parse(Me.FNQCActualQty.Value)
                If Integer.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE, "0")) <= 0 Then
                    _StateSave = True
                    Me.FNQCActualQty.Value = (Me.FNQCActualQty.Value - 1)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub _DelDefect()
        Try
            Dim _Cmd As String = ""
            _Cmd = "Delete From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect_Detail"
            _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo ='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
            _Cmd &= vbCrLf & "AND FNSeq=" & Integer.Parse(Me.FNQCActualQty.Value)
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
            _Cmd = "Select Top 1 FNHSysQCSuplDetailId From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect_Detail WITH(NOLOCK)  "
            _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo ='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
            If Integer.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE, "0")) <= 0 Then
                _Cmd = "Delete From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect"
                _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeNo.Text) & "'"
                '_Cmd &= vbCrLf & "And FDDateTrans=" & HI.UL.ULDate.FormatDateDB
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function VerrifyData()
        Try
            If Me.FTBarcodeNo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTBarcodeBundleNo_lbl.Text)
                Me.FTBarcodeNo.Focus()
                Return False
            End If
            If Me.FTOrderNo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTBarcodeBundleNo_lbl.Text)
                Me.FTBarcodeNo.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If VerrifyData() Then
                Dim _State As Boolean
                Dim _msg As String = GetQtyMsg(_State)
                'If Not (_State) Then
                If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, _msg.ToString, Me.Text) = True Then
                    If _SaveData() Then
                        _StateSave = True
                    End If
                    Call CreateGroupDefect(_FNSendSuplType)
                Else
                End If
                'Else
                '    If _SaveData() Then
                '        _StateSave = True
                '    End If
                '    Call CreateGroupDefect(_FNSendSuplType)
                'End If
            End If
            FTBarcodeNo.Focus()
            FTBarcodeNo.SelectAll()
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetQtyMsg(ByRef _State As Boolean) As String
        Try
            _State = False
            'ส่งปัก
            Dim _Cmd As String = ""
            Dim _EmQty, _Scr, _Fab, _Acc, _heat As Double
            _Cmd = "SELECT     COUNT(Q.FNHSysQCSuplDetailId) AS FNHSysQCSuplDetailId  "
            _Cmd &= vbCrLf & "FROM  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect_Detail AS Q  WITH(NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TQAMQCSuplDetail AS D  WITH(NOLOCK)  ON Q.FNHSysQCSuplDetailId = D.FNHSysQCSuplDetailId"
            _Cmd &= vbCrLf & "WHERE     (Q.FTBarcodeSendSuplNo = N'" & Me.FTBarcodeNo.Text & "')"
            _Cmd &= vbCrLf & "and Q.FNSeq =" & Me.FNQCActualQty.Value
            _Cmd &= vbCrLf & "and D.FNSendSuplType = 0"
            _Cmd &= vbCrLf & "and D.FNSubQCType = 0"
            _Cmd &= vbCrLf & "group by   D.FNSendSuplType"
            _EmQty = Double.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE, "0"))

            'ส่งพิมพ์
            _Cmd = "SELECT     COUNT(Q.FNHSysQCSuplDetailId) AS FNHSysQCSuplDetailId  "
            _Cmd &= vbCrLf & "FROM  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect_Detail AS Q  WITH(NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TQAMQCSuplDetail AS D  WITH(NOLOCK)  ON Q.FNHSysQCSuplDetailId = D.FNHSysQCSuplDetailId"
            _Cmd &= vbCrLf & "WHERE     (Q.FTBarcodeSendSuplNo = N'" & Me.FTBarcodeNo.Text & "')"
            _Cmd &= vbCrLf & "and Q.FNSeq =" & Me.FNQCActualQty.Value
            _Cmd &= vbCrLf & "and D.FNSendSuplType = 1"
            _Cmd &= vbCrLf & "and D.FNSubQCType = 0"
            _Cmd &= vbCrLf & "group by   D.FNSendSuplType"
            _Scr = Double.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE, "0"))

            'ส่งรีด
            _Cmd = "SELECT     COUNT(Q.FNHSysQCSuplDetailId) AS FNHSysQCSuplDetailId  "
            _Cmd &= vbCrLf & "FROM  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect_Detail AS Q  WITH(NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TQAMQCSuplDetail AS D  WITH(NOLOCK)  ON Q.FNHSysQCSuplDetailId = D.FNHSysQCSuplDetailId"
            _Cmd &= vbCrLf & "WHERE     (Q.FTBarcodeSendSuplNo = N'" & Me.FTBarcodeNo.Text & "')"
            _Cmd &= vbCrLf & "and Q.FNSeq =" & Me.FNQCActualQty.Value
            _Cmd &= vbCrLf & "and D.FNSendSuplType = 2"
            _Cmd &= vbCrLf & "and D.FNSubQCType = 0"
            _Cmd &= vbCrLf & "group by   D.FNSendSuplType"
            _heat = Double.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE, "0"))

            'Fabric
            _Cmd = "SELECT     COUNT(Q.FNHSysQCSuplDetailId) AS FNHSysQCSuplDetailId  "
            _Cmd &= vbCrLf & "FROM  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect_Detail AS Q  WITH(NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TQAMQCSuplDetail AS D  WITH(NOLOCK)  ON Q.FNHSysQCSuplDetailId = D.FNHSysQCSuplDetailId"
            _Cmd &= vbCrLf & "WHERE     (Q.FTBarcodeSendSuplNo = N'" & Me.FTBarcodeNo.Text & "')"
            _Cmd &= vbCrLf & "and Q.FNSeq =" & Me.FNQCActualQty.Value
            _Cmd &= vbCrLf & "and D.FNSubQCType = 1"
            _Cmd &= vbCrLf & "group by   D.FNSendSuplType"
            _Fab = Double.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE, "0"))

            'Acc
            _Cmd = "SELECT     COUNT(Q.FNHSysQCSuplDetailId) AS FNHSysQCSuplDetailId  "
            _Cmd &= vbCrLf & "FROM  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect_Detail AS Q  WITH(NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TQAMQCSuplDetail AS D  WITH(NOLOCK)  ON Q.FNHSysQCSuplDetailId = D.FNHSysQCSuplDetailId"
            _Cmd &= vbCrLf & "WHERE     (Q.FTBarcodeSendSuplNo = N'" & Me.FTBarcodeNo.Text & "')"
            _Cmd &= vbCrLf & "and Q.FNSeq =" & Me.FNQCActualQty.Value
            _Cmd &= vbCrLf & "and D.FNSubQCType = 2"
            _Cmd &= vbCrLf & "group by   D.FNSendSuplType"
            _Acc = Double.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE, "0"))

            If (_EmQty > 0 Or _Scr > 0 Or _heat > 0) And _Fab > 0 And _Acc > 0 Then _State = True

            Dim _Return As String = ""
            _Return = "" & Me.FTEmbroidDefectQty_lbl.Text & " " & _EmQty.ToString & " " & Me.FTUnitDefect_lbl.Text
            _Return &= vbCrLf & "" & Me.FTScreenDefectQty_lbl.Text & " " & _Scr.ToString & " " & Me.FTUnitDefect_lbl.Text
            _Return &= vbCrLf & "" & Me.FTHeatDefectQty_lbl.Text & " " & _heat.ToString & " " & Me.FTUnitDefect_lbl.Text
            _Return &= vbCrLf & "" & Me.FTFabricDefectQty_lbl.Text & " " & _Fab.ToString & " " & Me.FTUnitDefect_lbl.Text
            _Return &= vbCrLf & "" & Me.FTAccessoryDefectQty_lbl.Text & " " & _Acc.ToString & " " & Me.FTUnitDefect_lbl.Text

            Return _Return.ToString
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub wQCSendSupl_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Try
            Me.FTBarcodeNo.Focus()
            Me.FTBarcodeNo.SelectAll()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wQCSendSupl_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            _StateSave = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            Try
                Call _DelDefect()
                If Me.FNQCActualQty.Value >= 1 Then
                    Me.FNQCActualQty.Value = Me.FNQCActualQty.Value - 1
                End If
                Call CreateGroupDefect(_FNSendSuplType)
                _StateSave = True
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            _StateSave = True
            HI.TL.HandlerControl.ClearControl(Me)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmApcepptqc_Click(sender As Object, e As EventArgs) Handles ocmApcepptqc.Click
        Try
            With _wAccept
                .Data = LoadOnhandFG()
                .ShowDialog()
                If (.Process) Then
                    If .Data.Select("FTSelect='1'").Length <= 0 Then Exit Sub
                    If _SaveDataApcept(.Data) Then
                        HI.MG.ShowMsg.mInfo("Apcept QC SendSupl Success.", 1609161057, Me.Text, "", MessageBoxIcon.Information)
                    End If
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Function LoadOnhandFG() As DataTable
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT '0' AS FTSelect ,  R.FTRcvSuplNo, R.FTBarcodeSendSuplNo, B.FTBarcodeBundleNo, LEFT( B.FTOrderProdNo , len( B.FTOrderProdNo) -4 )  as FTOrderNo ,B.FNQuantity "
            _Cmd &= vbCrLf & "FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTBundle As B With (NOLOCK) RIGHT OUTER JOIN"
            _Cmd &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTBarcode_SendSupl AS BS WITH (NOLOCK) ON B.FTOrderProdNo = BS.FTOrderProdNo AND B.FTBarcodeBundleNo = BS.FTBarcodeBundleNo RIGHT OUTER JOIN"
            _Cmd &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTReceiveSupl_Barcode As R With (NOLOCK) On BS.FTBarcodeSendSuplNo = R.FTBarcodeSendSuplNo"
            _Cmd &= vbCrLf & "WHERE  (R.FTBarcodeSendSuplNo NOT IN"
            _Cmd &= vbCrLf & "  (SELECT FTBarcodeSendSuplNo"
            _Cmd &= vbCrLf & "  FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect))  "
            _Cmd &= vbCrLf & " and Isnull(B.FTBarcodeBundleNo,'')  <> '' "

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
            Return _oDt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Private Function _SaveDataApcept(_odt As DataTable) As Boolean
        Try
            Dim _Cmd As String = ""

            For Each R As DataRow In _odt.Select("FTSelect = '1'")
                _Cmd = "UPDATE " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect"
                _Cmd &= vbCrLf & "Set FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ",FDUpdDate =" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",FNDefectQty=0"
                _Cmd &= vbCrLf & ",FTRemark='" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
                _Cmd &= vbCrLf & "WHERE FTBarcodeSendSuplNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString) & "'"
                '_Cmd &= vbCrLf & "AND FDDateTrans=" & HI.UL.ULDate.FormatDateDB
                If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE) = False Then
                    _Cmd = "INSERT INTO " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPTSendSuplDefect ( FTInsUser, FDInsDate, FTInsTime,   FTBarcodeSendSuplNo, FNDefectQty, FTRemark  )"
                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString)) & "'"
                    _Cmd &= vbCrLf & ",0"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
                    '_Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE) = False Then
                        Return False
                    End If
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub FTBarcodeNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTBarcodeNo.EditValueChanged

    End Sub
End Class