Imports DevExpress.Data
Imports System.IO

Public Class wINVENTranferWHIntransitTracking

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitGrid()

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        ''------Start Add Summary Grid-------------
        'Dim sFieldCount As String = ""
        'Dim sFieldSum As String = "FNUsedQuantity|FNUsedPlusQuantity|FNRSVQuantity|FNPOQuantity|FNRCVQuantity|FNRTSQuantity|FNPOBalQuantity" & _
        '    "|FNRCVStockQuantity|FNTROInQuantity|FNTROOutQuantity|FNISSQuantity|FNRETQuantity|FNADJInQuantity|FNADJOutQuantity|FNTRWInQuantity|FNTRWOutQuantity|FNSaleQuantity|FNTerminateQuantity|FNOnhandQuantity"

        'Dim sFieldGrpCount As String = ""
        'Dim sFieldGrpSum As String = "FNUsedQuantity|FNUsedPlusQuantity|FNRSVQuantity|FNPOQuantity|FNRCVQuantity|FNRTSQuantity|FNPOBalQuantity" & _
        '     "|FNRCVStockQuantity|FNTROInQuantity|FNTROOutQuantity|FNISSQuantity|FNRETQuantity|FNADJInQuantity|FNADJOutQuantity|FNTRWInQuantity|FNTRWOutQuantity|FNSaleQuantity|FNTerminateQuantity|FNOnhandQuantity"

        'Dim sFieldCustomSum As String = ""
        'Dim sFieldCustomGrpSum As String = ""

        'With ogvtime
        '    .ClearGrouping()
        '    .ClearDocument()
        '    '.Columns("FTDateTrans").Group()

        '    For Each Str As String In sFieldCount.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
        '        End If
        '    Next

        '    For Each Str As String In sFieldCustomSum.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
        '        End If
        '    Next

        '    For Each Str As String In sFieldSum.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit.ToString & "}"
        '        End If
        '    Next

        '    For Each Str As String In sFieldGrpCount.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
        '        End If
        '    Next

        '    For Each Str As String In sFieldCustomGrpSum.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
        '        End If
        '    Next

        '    For Each Str As String In sFieldGrpSum.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n" & HI.ST.Config.QtyDigit.ToString & "})")
        '        End If
        '    Next

        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        '    .OptionsView.ShowFooter = True
        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

        '    .ExpandAllGroups()
        '    .RefreshData()


        'End With
        ''------End Add Summary Grid-------------
    End Sub
#End Region

#Region "Custom summaries"

    Private totalSum As Integer = 0
    Private GrpSum As Integer = 0

    Private Sub InitStartValue()
        totalSum = 0
        GrpSum = 0
    End Sub

    'Private Sub ogv_CustomDrawGroupRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs) Handles ogv.CustomDrawGroupRow

    '    Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo = e.Info
    '    Dim Handle As Integer = ogv.GetDataRowHandleByGroupRowHandle(e.RowHandle)

    '    'Select Case info.Column.FieldName.ToString.ToUpper
    '    '    Case "FNWorkingDay"

    '    Dim GrpDisplayText As String = ogv.GetGroupSummaryText(e.RowHandle)  'ogv.GetGroupRowValue(e.RowHandle, info.Column)
    '    Dim GrpDisplayTextReplace As String = Nothing
    '    Dim GrpDisplayTextReplaceNew As String = Nothing
    '    GrpDisplayTextReplace = GrpDisplayText.Split(")")(1)

    '    If GrpDisplayTextReplace <> "" Then
    '        If GrpDisplayTextReplace.Split("=").Length >= 2 Then
    '            Dim Title1 As String = GrpDisplayTextReplace.Split("=")(0)
    '            Dim Title2 As String = GrpDisplayTextReplace.Split("=")(1)

    '            If IsNumeric(Title2) = False Then
    '                Title2 = "0"
    '            End If
    '            Dim _Sum As Integer = CDbl(Title2)
    '            Dim NetDisplay As String = ""
    '            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
    '                NetDisplay = Format((_Sum \ 480), "00") & " วัน : " & Format(((_Sum Mod 480) \ 60), "00") & " ชั่วโมง : " & Format(((_Sum Mod 480) Mod 60), "00") & " นาที"
    '            Else
    '                NetDisplay = Format((_Sum \ 480), "00") & " Day : " & Format(((_Sum Mod 480) \ 60), "00") & " Hour : " & Format(((_Sum Mod 480) Mod 60), "00") & " Minute"
    '            End If

    '            GrpDisplayTextReplaceNew = Title1 & "=" & NetDisplay
    '            GrpDisplayText = GrpDisplayText.Replace(GrpDisplayTextReplace, GrpDisplayTextReplaceNew)
    '        End If


    '    info.GroupText = info.Column.Caption + ":" + info.GroupValueText + ""
    '    info.GroupText += "" + GrpDisplayText + ""

    '    'End Select

    'End Sub
    Private Sub Gridview_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvtime.CustomSummaryCalculate
        If e.SummaryProcess = CustomSummaryProcess.Start Then
            InitStartValue()
        End If

        Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
            Case "FNTime", "FNOT1", "FNOT1_5", "FNOT2", "FNOT3", "FNOT4"
                If e.SummaryProcess = CustomSummaryProcess.Calculate Then

                    If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                        If e.IsGroupSummary Then
                            Dim Seq As Integer = 1
                            For Each Str As String In e.FieldValue.ToString.Split(":")
                                Select Case Seq
                                    Case 1
                                        GrpSum = GrpSum + (Integer.Parse(Val(Str)) * 60)
                                    Case Else
                                        GrpSum = GrpSum + Integer.Parse(Val(Str))
                                End Select
                                Seq = Seq + 1
                            Next
                        End If

                        If e.IsTotalSummary Then
                            Dim Seq As Integer = 1
                            For Each Str As String In e.FieldValue.ToString.Split(":")
                                Select Case Seq
                                    Case 1
                                        totalSum = totalSum + (Integer.Parse(Val(Str)) * 60)
                                    Case Else
                                        totalSum = totalSum + Integer.Parse(Val(Str))
                                End Select

                                Seq = Seq + 1
                            Next
                        End If

                    End If

                    If e.IsGroupSummary Then
                        Dim GrpDisplay As String = ""
                        GrpDisplay = Format(((GrpSum) \ 60), "00") & " : " & Format(((GrpSum) Mod 60), "00")
                        e.TotalValue = GrpSum
                    End If

                    If e.IsTotalSummary Then
                        Dim NetDisplay As String = ""

                        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                            NetDisplay = Format(((totalSum) \ 60), "00") & " : " & Format(((totalSum) Mod 60), "00")
                        Else
                            NetDisplay = Format(((totalSum) \ 60), "00") & " : " & Format(((totalSum) Mod 60), "00")
                        End If

                        e.TotalValue = NetDisplay ' totalSum 'NetDisplay

                    End If
                End If
        End Select
    End Sub


#End Region

#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

    Private _ActualDate As String = ""
    ReadOnly Property ActualDate As String
        Get
            Return _ActualDate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property

#End Region

#Region "Procedure"

    Private Sub LoadData()

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0

        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        _Qry = "   Select FTTransferWHNo"
        _Qry &= vbCrLf & "  , CASE WHEN ISDATE(FDTransferWHDate) = 1 THEN Convert(nvarchar(10), Convert(Datetime,FDTransferWHDate) ,103) ELSE '' END AS FDTransferWHDate "
        _Qry &= vbCrLf & "  , FTTransferWHBy"
        _Qry &= vbCrLf & "  ,FTCmpCode"
        _Qry &= vbCrLf & "  ,FTCmpName"
        _Qry &= vbCrLf & "  , FTWHCode"
        _Qry &= vbCrLf & "   ,FTCmpCodeTo"
        _Qry &= vbCrLf & "   ,FTCmpToName"
        _Qry &= vbCrLf & "  , FTWHCodeTo"
        _Qry &= vbCrLf & "  , FTRemark"
        _Qry &= vbCrLf & "  , FTStateApprove"
        _Qry &= vbCrLf & "  , FTApproveBy"
        _Qry &= vbCrLf & "  , CASE WHEN ISDATE(FDApproveDate) = 1 THEN Convert(nvarchar(10), Convert(Datetime,FDApproveDate) ,103) ELSE '' END AS FDApproveDate"
        _Qry &= vbCrLf & "  , FTOrderNo"
        _Qry &= vbCrLf & "  , FNHSysRawMatId"
        _Qry &= vbCrLf & "  , FTRawMatCode"
        _Qry &= vbCrLf & "  , FTRawMatName"
        _Qry &= vbCrLf & "  , FTRawMatColorCode"
        _Qry &= vbCrLf & "  , FTRawMatSizeCode"
        _Qry &= vbCrLf & "  , FTUnitCode"
        _Qry &= vbCrLf & "  , SUM(FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & "  , SUM(FNQuantityIn) AS FNQuantityIn"
        _Qry &= vbCrLf & "  , SUM(FNQuantity - FNQuantityIn) AS FNQuantityInTransit"
        _Qry &= vbCrLf & "  FROM     (SELECT A.FTTransferWHNo"
        _Qry &= vbCrLf & "  , A.FDTransferWHDate"
        _Qry &= vbCrLf & "  , A.FTTransferWHBy"
        _Qry &= vbCrLf & "  , WHA.FTWHCode"
        _Qry &= vbCrLf & "  , WHB.FTWHCode AS FTWHCodeTo"
        _Qry &= vbCrLf & " , A.FTRemark"
        _Qry &= vbCrLf & "  , A.FTStateApprove"
        _Qry &= vbCrLf & "  , A.FTApproveBy"
        _Qry &= vbCrLf & "  , A.FDApproveDate"
        _Qry &= vbCrLf & "  , BO.FTOrderNo "
        _Qry &= vbCrLf & "  , B.FNHSysRawMatId"
        _Qry &= vbCrLf & "  , IM.FTRawMatCode"
        _Qry &= vbCrLf & "  , ISNULL(IMC.FTRawMatColorCode, '') AS FTRawMatColorCode"
        _Qry &= vbCrLf & "  , ISNULL(IMS.FTRawMatSizeCode, '') AS FTRawMatSizeCode "
        _Qry &= vbCrLf & "  , U.FTUnitCode"
        _Qry &= vbCrLf & "  , BO.FTBarcodeNo"
        _Qry &= vbCrLf & "   , BO.FNQuantity"
        _Qry &= vbCrLf & "   , ISNULL(BI.FNQuantity, 0) AS FNQuantityIn"
        _Qry &= vbCrLf & "   , CA.FTCmpCode"
        _Qry &= vbCrLf & "  ,  CB.FTCmpCode AS FTCmpCodeTo"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "  , IM.FTRawMatNameTH AS FTRawMatName"
            _Qry &= vbCrLf & "   , CB.FTCmpNameTH AS FTCmpToName"
            _Qry &= vbCrLf & "   , CA.FTCmpNameTH AS FTCmpName"
        Else
            _Qry &= vbCrLf & "  , IM.FTRawMatNameEN AS FTRawMatName"
            _Qry &= vbCrLf & "   , CB.FTCmpNameEN AS FTCmpToName"
            _Qry &= vbCrLf & "   , CA.FTCmpNameEN AS FTCmpName"
        End If

        _Qry &= vbCrLf & "  , ISNULL(IMC.FNRawMatColorSeq, 0) AS FNRawMatColorSeq"
        _Qry &= vbCrLf & "  , ISNULL(IMS.FNRawMatSizeSeq, 0) AS FNRawMatSizeSeq "

        _Qry &= vbCrLf & "   FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferWH AS A WITH(NOLOCK)   INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS WHA WITH(NOLOCK)   ON A.FNHSysWHId = WHA.FNHSysWHId INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS WHB WITH(NOLOCK)   ON A.FNHSysWHIdTo = WHB.FNHSysWHId INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO  WITH(NOLOCK)  ON A.FTTransferWHNo = BO.FTDocumentNo INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK)   ON BO.FTBarcodeNo = B.FTBarcodeNo INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM  WITH(NOLOCK)    ON B.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK)   ON IM.FNHSysUnitId = U.FNHSysUnitId INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS CA WITH(NOLOCK)   ON WHA.FNHSysCmpId = CA.FNHSysCmpId INNER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS CB WITH(NOLOCK)   ON WHB.FNHSysCmpId = CB.FNHSysCmpId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS BI WITH(NOLOCK)   ON BO.FTBarcodeNo = BI.FTBarcodeNo AND BO.FTDocumentNo = BI.FTDocumentNo AND BO.FTOrderNo = BI.FTOrderNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS IMS WITH(NOLOCK)   ON IM.FNHSysRawMatSizeId = IMS.FNHSysRawMatSizeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS IMC WITH(NOLOCK) ON IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId"

        _Qry &= vbCrLf & "  WHERE A.FTTransferWHNo<>'' "

        If FNHSysCmpId.Text <> "" Then
            _Qry &= vbCrLf & " AND CA.FTCmpCode ='" & HI.UL.ULF.rpQuoted(FNHSysCmpId.Text) & "'  "
        End If

        If FNHSysCmpIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND CB.FTCmpCode ='" & HI.UL.ULF.rpQuoted(FNHSysCmpIdTo.Text) & "'  "
        End If

        If FNHSysWHId.Text <> "" Then
            _Qry &= vbCrLf & " AND WHA.FTWHCode ='" & HI.UL.ULF.rpQuoted(FNHSysWHId.Text) & "'  "
        End If

        If FNHSysWHIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND WHB.FTWHCode ='" & HI.UL.ULF.rpQuoted(FNHSysWHIdTo.Text) & "'  "
        End If

        If FTOrderNo.Text <> "" Then
            _Qry &= vbCrLf & " AND BO.FTOrderNo >='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'  "
        End If

        If FTOrderNoTo.Text <> "" Then
            _Qry &= vbCrLf & " AND BO.FTOrderNo <='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
        End If

        If FTStartDate.Text <> "" Then
            _Qry &= vbCrLf & " AND A.FDTransferWHDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "'  "
        End If

        If FTEndDate.Text <> "" Then
            _Qry &= vbCrLf & " AND A.FDTransferWHDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) & "'  "
        End If

        _Qry &= vbCrLf & "       ) AS A_1"
        _Qry &= vbCrLf & "            GROUP BY FTTransferWHNo"
        _Qry &= vbCrLf & "   , FDTransferWHDate"
        _Qry &= vbCrLf & "   , FTTransferWHBy"
        _Qry &= vbCrLf & "   ,FTCmpCode"
        _Qry &= vbCrLf & "   ,FTCmpName"
        _Qry &= vbCrLf & "   , FTWHCode"
        _Qry &= vbCrLf & "    ,FTCmpCodeTo"
        _Qry &= vbCrLf & "    ,FTCmpToName"
        _Qry &= vbCrLf & "   , FTWHCodeTo"
        _Qry &= vbCrLf & "   , FTRemark"
        _Qry &= vbCrLf & "   , FTStateApprove"
        _Qry &= vbCrLf & "   , FTApproveBy"
        _Qry &= vbCrLf & "   , FDApproveDate"
        _Qry &= vbCrLf & "   , FTOrderNo"
        _Qry &= vbCrLf & "   , FNHSysRawMatId"
        _Qry &= vbCrLf & "   , FTRawMatCode"
        _Qry &= vbCrLf & "   , FTRawMatName"
        _Qry &= vbCrLf & "   , FTRawMatColorCode"
        _Qry &= vbCrLf & "   , FTRawMatSizeCode"
        _Qry &= vbCrLf & "   , FTUnitCode"
        _Qry &= vbCrLf & "  , FNRawMatColorSeq "
        _Qry &= vbCrLf & "  , FNRawMatSizeSeq  "
        _Qry &= vbCrLf & "  HAVING SUM(FNQuantity - FNQuantityIn)  > 0 "
        '_Qry &= vbCrLf & "  ORDER BY FTTransferWHNo,FTOrderNo,FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode "
        _Qry &= vbCrLf & "  ORDER BY FTTransferWHNo,FTOrderNo,FTRawMatCode,FTRawMatColorCode,FNRawMatSizeSeq "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Me.ogdtime.DataSource = _dt
        _Spls.Close()

        _RowDataChange = False

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNHSysCmpId.Text <> "" Then
            _Pass = True
        End If

        If Me.FNHSysCmpIdTo.Text <> "" Then
            _Pass = True
        End If

        If Me.FNHSysWHId.Text <> "" Then
            _Pass = True
        End If

        If Me.FNHSysWHIdTo.Text <> "" Then
            _Pass = True
        End If

        If Me.FTOrderNo.Text <> "" Then
            _Pass = True
        End If

        If Me.FTOrderNoTo.Text <> "" Then
            _Pass = True
        End If

        If Me.FTStartDate.Text <> "" Then
            _Pass = True
        End If

        If Me.FTEndDate.Text <> "" Then
            _Pass = True
        End If

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function
#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            StateCal = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then
            Call LoadData()
        End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ogbheader_Click(sender As Object, e As EventArgs) Handles ogbheader.Click

    End Sub
End Class