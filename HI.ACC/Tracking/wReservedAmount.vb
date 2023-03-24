Imports DevExpress.Data
Imports System.IO

Public Class wReservedAmount

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitGrid()
        Call LoadListData()

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

        _Qry = " SELECT FTOrderNo,FTReserveNo,FTStyleCode,FTWHCode,FTRawMatCode"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(FDReserveDate) = 1 THEN Convert(nvarchar(10), Convert(Datetime,FDReserveDate) ,103) ELSE '' END AS FDReserveDate"
        _Qry &= vbCrLf & ",FTCmpCodeCreate "
        _Qry &= vbCrLf & ",FTReserveBy"
        _Qry &= vbCrLf & ",FTCmpCode"
        _Qry &= vbCrLf & ",FTRawMatName"
        _Qry &= vbCrLf & ",FTRawMatColorCode"
        _Qry &= vbCrLf & ",FTRawMatSizeCode,FTUnitCode"
        _Qry &= vbCrLf & ",Reserved AS Reserved"
        _Qry &= vbCrLf & ",FNAmount"
        _Qry &= vbCrLf & ",FTFacPurchaseNo,FTOrderNoFrom"
        _Qry &= vbCrLf & "FROM"
        _Qry &= vbCrLf & "(SELECT R.FTOrderNo,R.FTReserveNo,R.FDReserveDate,R.FTReserveBy,C.FTCmpCode,S.FTStyleCode,W.FTWHCode,IM.FTRawMatCode,C2.FTCmpCode AS FTCmpCodeCreate"
        ' _Qry &= vbCrLf & ",BI.FTBarcodeNo"
        _Qry &= vbCrLf & ",sum( ISNULL(Bi.FNQuantity,0)) AS Reserved,ISNULL(X2.FTOrderNoFrom,'') AS FTOrderNoFrom"
        _Qry &= vbCrLf & ", sum( Convert(numeric(18,2),ISNULL(Bi.FNQuantity,0) * ISNULL(B.FNPrice,0))) AS FNAmount"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",IM.FTRawMatNameTH AS FTRawMatName"
        Else
            _Qry &= vbCrLf & ",IM.FTRawMatNameEN AS FTRawMatName"
        End If

        _Qry &= vbCrLf & ",ISNULL(IMC.FTRawMatColorCode,'') AS FTRawMatColorCode,ISNULL(IMS.FTRawMatSizeCode,'') AS FTRawMatSizeCode,U.FTUnitCode"
        _Qry &= vbCrLf & ",MAX(ISNULL(RPO.FTFacPurchaseNo,'')) AS FTFacPurchaseNo "
        _Qry &= vbCrLf & "FROM"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode AS B WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode_IN AS BI WITH (NOLOCK) ON B.FTBarcodeNo = BI.FTBarcodeNo INNER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENReserve AS R WITH (NOLOCK) ON BI.FTDocumentNo = R.FTReserveNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON BI.FTOrderNo=O.FTOrderNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS S WITH(NOLOCK) ON O.FNHSysStyleId=S.FNHSysStyleId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMWarehouse AS W WITH(NOLOCK) ON BI.FNHSysWHId=W.FNHSysWHId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TINVENMMaterial AS IM WITH(NOLOCK) ON B.FNHSysRawMatId=IM.FNHSysRawMatId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMUnit AS U WITH(NOLOCK) ON IM.FNHSysUnitId=U.FNHSysUnitId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TINVENMMatColor AS IMC WITH(NOLOCK) ON IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TINVENMMatSize AS IMS WITH(NOLOCK) ON IM.FNHSysRawMatSizeId = IMS.FNHSysRawMatSizeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS C WITH(NOLOCK) ON O.FNHSysCmpId=C.FNHSysCmpId"
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS C2 WITH(NOLOCK) ON R.FNHSysCmpId=C2.FNHSysCmpId"


        _Qry &= vbCrLf & " OUTER APPLY ( "
        _Qry &= vbCrLf & " SELECT TOP 1 FTFacPurchaseNo,FDFacPurchaseDate,FTFacPurchaseBy"
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTFacPurchase AS X WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE X.FTPurchaseNoRef = R.FTReserveNo  "
        _Qry &= vbCrLf & "   ) RPO "


        _Qry &= vbCrLf & " OUTER APPLY (SELECT  TOP 1 X.FTOrderNo AS FTOrderNoFrom  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "]..TINVENBarcode_OUT AS X WITH (NOLOCK)  WHERE X.FTBarcodeNo =BI.FTBarcodeNo AND X.FTDocumentNo =BI.FTDocumentNo  ) AS X2"


        _Qry &= vbCrLf & "  WHERE R.FTReserveNo <>'' "


        If FTPurchaseNo.Text <> "" Then
            _Qry &= vbCrLf & " AND B.FTPurchaseNo >='" & HI.UL.ULF.rpQuoted(FTPurchaseNo.Text) & "' "
        End If

        If FTPurchaseNoTo.Text <> "" Then
            _Qry &= vbCrLf & " AND B.FTPurchaseNo <='" & HI.UL.ULF.rpQuoted(FTPurchaseNoTo.Text) & "' "
        End If

        If FNHSysWHId.Text <> "" Then
            _Qry &= vbCrLf & " AND W.FTWHCode >='" & HI.UL.ULF.rpQuoted(FNHSysWHId.Text) & "' "
        End If

        If FNHSysWHIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND W.FTWHCode <='" & HI.UL.ULF.rpQuoted(FNHSysWHIdTo.Text) & "' "
        End If

        If FTStartDate.Text <> "" Then
            _Qry &= vbCrLf & " AND R.FDReserveDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "'  "
        End If

        If FTEndDate.Text <> "" Then
            _Qry &= vbCrLf & " AND R.FDReserveDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) & "'  "
        End If

        If FTOrderNo.Text <> "" Then
            _Qry &= vbCrLf & " AND BI.FTOrderNo >='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'  "
        End If

        If FTOrderNoTo.Text <> "" Then
            _Qry &= vbCrLf & " AND BI.FTOrderNo <='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
        End If

        _Qry &= vbCrLf & "GROUP BY R.FTOrderNo,R.FTReserveNo,R.FDReserveDate,R.FTReserveBy,C.FTCmpCode,S.FTStyleCode,W.FTWHCode,IM.FTRawMatCode,C2.FTCmpCode,ISNULL(X2.FTOrderNoFrom,'')"
        ' _Qry &= vbCrLf & ",BI.FTBarcodeNo"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",IM.FTRawMatNameTH"
        Else
            _Qry &= vbCrLf & ",IM.FTRawMatNameEN"
        End If

        _Qry &= vbCrLf & ",IMC.FTRawMatColorCode"
        _Qry &= vbCrLf & ",IMS.FTRawMatSizeCode,U.FTUnitCode)AS AA"
        _Qry &= vbCrLf & "ORDER BY AA.FTReserveNo DESC"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Me.ogdtime.DataSource = _dt
        _Spls.Close()

        _RowDataChange = False

    End Sub

    Private Sub LoadListData()


    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        'If Me.FNHSysCmpId.Text <> "" Then
        '    _Pass = True
        'End If

        'If Me.FNHSysCmpIdTo.Text <> "" Then
        '    _Pass = True
        'End If

        'If Me.FNHSysWHId.Text <> "" Then
        '    _Pass = True
        'End If

        'If Me.FNHSysWHIdTo.Text <> "" Then
        '    _Pass = True
        'End If

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


        If Me.FTPurchaseNo.Text <> "" Then
            _Pass = True
        End If

        If Me.FTPurchaseNoTo.Text <> "" Then
            _Pass = True
        End If

        If Me.FNHSysWHId.Text <> "" Then
            _Pass = True
        End If

        If Me.FNHSysWHIdTo.Text <> "" Then
            _Pass = True
        End If


        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function
#End Region


    'Private Sub Gridview_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvtime.CustomSummaryCalculate
    '    If e.SummaryProcess = CustomSummaryProcess.Start Then
    '        InitStartValue()
    '    End If

    '    Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
    '        Case "FNTime", "FNOT1", "FNOT1_5", "FNOT2", "FNOT3", "FNOT4"
    '            If e.SummaryProcess = CustomSummaryProcess.Calculate Then

    '                If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
    '                    If e.IsGroupSummary Then
    '                        Dim Seq As Integer = 1
    '                        For Each Str As String In e.FieldValue.ToString.Split(":")
    '                            Select Case Seq
    '                                Case 1
    '                                    GrpSum = GrpSum + (Integer.Parse(Val(Str)) * 60)
    '                                Case Else
    '                                    GrpSum = GrpSum + Integer.Parse(Val(Str))
    '                            End Select
    '                            Seq = Seq + 1
    '                        Next
    '                    End If

    '                    If e.IsTotalSummary Then
    '                        Dim Seq As Integer = 1
    '                        For Each Str As String In e.FieldValue.ToString.Split(":")
    '                            Select Case Seq
    '                                Case 1
    '                                    totalSum = totalSum + (Integer.Parse(Val(Str)) * 60)
    '                                Case Else
    '                                    totalSum = totalSum + Integer.Parse(Val(Str))
    '                            End Select

    '                            Seq = Seq + 1
    '                        Next
    '                    End If

    '                End If

    '                If e.IsGroupSummary Then
    '                    Dim GrpDisplay As String = ""
    '                    GrpDisplay = Format(((GrpSum) \ 60), "00") & " : " & Format(((GrpSum) Mod 60), "00")
    '                    e.TotalValue = GrpSum
    '                End If

    '                If e.IsTotalSummary Then
    '                    Dim NetDisplay As String = ""

    '                    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
    '                        NetDisplay = Format(((totalSum) \ 60), "00") & " : " & Format(((totalSum) Mod 60), "00")
    '                    Else
    '                        NetDisplay = Format(((totalSum) \ 60), "00") & " : " & Format(((totalSum) Mod 60), "00")
    '                    End If

    '                    e.TotalValue = NetDisplay ' totalSum 'NetDisplay

    '                End If
    '            End If
    '    End Select
    'End Sub


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