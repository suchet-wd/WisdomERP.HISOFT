Imports DevExpress.Data
Imports System.IO
Imports System.Windows.Forms
Imports Microsoft.Office.Interop
Imports DevExpress.XtraEditors.Controls

Public Class wPurchaseTracking

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
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNPOQuantity|FNRcvQuantity|FNRTsQuantity|FNPOBalQuantity"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FNPOQuantity|FNRcvQuantity|FNRTsQuantity|FNPOBalQuantity"

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogvtime
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()

            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldCustomSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit.ToString & "}"
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldCustomGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n" & HI.ST.Config.QtyDigit.ToString & "})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            .ExpandAllGroups()
            .RefreshData()


        End With
        '------End Add Summary Grid-------------
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
        Dim _Qry As String = ""
        Dim _dt As DataTable


        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        _Qry = " SELECT  '0' AS FTSelect , FTPurchaseNo"
        _Qry &= vbCrLf & ",FDPurchaseDate"
        _Qry &= vbCrLf & ",FTPurchaseBy"
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",(SELECT FTDeliveryDescTH FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDelivery WHERE FNHSysDeliveryID = A.FNHSysDeliveryID) AS FNHSysDeliveryID,FDDeliveryDate"
        Else
            _Qry &= vbCrLf & ",(SELECT FTDeliveryDescEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDelivery WHERE FNHSysDeliveryID = A.FNHSysDeliveryID) AS FNHSysDeliveryID,FDDeliveryDate"
        End If
        _Qry &= vbCrLf & ",FTStateSendApp,FTPOCustPO,FTPOOrderNo,FTPOStyleCode,FNPoState"
        _Qry &= vbCrLf & ",FNHSysDeliveryID"
        _Qry &= vbCrLf & ",FDDeliveryDate"
        _Qry &= vbCrLf & ",FTSendAppDate"
        _Qry &= vbCrLf & ",FTStateSuperVisorApp"
        _Qry &= vbCrLf & ",FTStateSuperVisorReject"
        _Qry &= vbCrLf & ",FTSuperVisorAppDate"
        _Qry &= vbCrLf & ",FTStateManagerApp"
        _Qry &= vbCrLf & ",FTStateManagerReject"
        _Qry &= vbCrLf & ",FTSuperManagerAppDate"
        _Qry &= vbCrLf & ",FNHSysSuplId"
        _Qry &= vbCrLf & ",FTSuplCode"
        _Qry &= vbCrLf & ",FNHSysRawMatId"
        _Qry &= vbCrLf & ",FNHSysUnitId"
        _Qry &= vbCrLf & ",FTRawMatCode"
        _Qry &= vbCrLf & ",FTRawMatColorCode"
        _Qry &= vbCrLf & ",FTRawMatSizeCode"
        _Qry &= vbCrLf & ",FTRawMatName"
        _Qry &= vbCrLf & ",FTSuplName"
        _Qry &= vbCrLf & ",FTUnitCode"
        _Qry &= vbCrLf & ",FNPOQuantity"
        _Qry &= vbCrLf & ",FNRcvQuantity"
        _Qry &= vbCrLf & ",FNRTsQuantity"
        _Qry &= vbCrLf & ",((FNPOQuantity - FNRcvQuantity) + FNRTsQuantity) AS FNPOBalQuantity"
        _Qry &= vbCrLf & "  FROM ( Select A.FTPurchaseNo,A.FTPurchaseBy,A.FNHSysDeliveryID"

        _Qry &= vbCrLf & " , [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.fn_PoHeader_CustomerPO(A.FTPurchaseNo) as FTPOCustPO "
        _Qry &= vbCrLf & " ,    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.fn_PoHeader_OrderNo(A.FTPurchaseNo) As FTPOOrderNo "
        _Qry &= vbCrLf & " ,     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.fn_PoHeader_StyleNo(A.FTPurchaseNo) As  FTPOStyleCode "

        _Qry &= vbCrLf & "  ,Case When ISDATE(A.FDPurchaseDate) = 1 Then Convert(varchar(10),Convert(datetime,A.FDPurchaseDate) ,103) Else '' END AS FDPurchaseDate "
        _Qry &= vbCrLf & "  ,CASE WHEN ISDATE(A.FDDeliveryDate) = 1 THEN Convert(varchar(10),Convert(datetime,A.FDDeliveryDate) ,103) ELSE '' END AS FDDeliveryDate "
        _Qry &= vbCrLf & "  ,CASE WHEN A.FTStateSendApp ='1' THEN '1' ELSE '0' END AS FTStateSendApp"
        _Qry &= vbCrLf & "  ,CASE WHEN ISDATE(A.FTSendAppDate) = 1 THEN Convert(varchar(10),Convert(datetime,A.FTSendAppDate) ,103) ELSE '' END AS FTSendAppDate "
        _Qry &= vbCrLf & "  ,CASE WHEN A.FTStateSuperVisorApp ='1' THEN '1' ELSE '0' END AS FTStateSuperVisorApp"
        _Qry &= vbCrLf & "  ,CASE WHEN A.FTStateSuperVisorApp ='2' THEN '1' ELSE '0' END AS FTStateSuperVisorReject"
        _Qry &= vbCrLf & "  ,CASE WHEN ISDATE(A.FTSuperVisorAppDate) = 1 THEN Convert(varchar(10),Convert(datetime,A.FTSuperVisorAppDate) ,103) ELSE '' END AS FTSuperVisorAppDate "
        _Qry &= vbCrLf & "  ,CASE WHEN A.FTStateManagerApp ='1' THEN '1' ELSE '0' END AS FTStateManagerApp"
        _Qry &= vbCrLf & "  ,CASE WHEN A.FTStateManagerApp ='2' THEN '1' ELSE '0' END AS FTStateManagerReject"
        _Qry &= vbCrLf & "  ,CASE WHEN ISDATE(A.FTSuperManagerAppDate) = 1 THEN Convert(varchar(10),Convert(datetime,A.FTSuperManagerAppDate) ,103) ELSE '' END AS FTSuperManagerAppDate "
        _Qry &= vbCrLf & "  ,A.FNHSysSuplId,Sup.FTSuplCode,A.FNPoState"
        _Qry &= vbCrLf & "  ,A.FNHSysRawMatId"
        _Qry &= vbCrLf & "  ,A.FNHSysUnitId"
        _Qry &= vbCrLf & "  ,M.FTRawMatCode"
        _Qry &= vbCrLf & "  ,ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
        _Qry &= vbCrLf & "  ,ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",M.FTRawMatNameTH AS FTRawMatName"
            _Qry &= vbCrLf & ",Sup.FTSuplNameTH AS FTSuplName"
        Else
            _Qry &= vbCrLf & ",M.FTRawMatNameEN AS FTRawMatName"
            _Qry &= vbCrLf & ",Sup.FTSuplNameEN AS FTSuplName"
        End If

        _Qry &= vbCrLf & "  ,ISNULL(U.FTUnitCode,'') AS FTUnitCode"
        _Qry &= vbCrLf & "  ,A.FNQuantity AS FNPOQuantity"
        _Qry &= vbCrLf & "  ,A.FNRcvQuantity AS FNRcvQuantity"
        _Qry &= vbCrLf & "  ,Convert(Numeric(18," & HI.ST.Config.QtyDigit & "),(A.FNRTsQuantity * ISNULL(UV.FNRateFrom,1)) / CASE WHEN ISNULL(UV.FNRateTo,1) =0 THEN 1 ELSE ISNULL(UV.FNRateTo,1) END)  AS FNRTsQuantity"
        _Qry &= vbCrLf & "  ,(SELECT TOP 1 FNRevisedSeq"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised AS PR  WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTPurchaseNo=A.FTPurchaseNo"
        _Qry &= vbCrLf & " ORDER BY FNRevisedSeq DESC"
        _Qry &= vbCrLf & " ) AS FNRevisedSeq"
        _Qry &= vbCrLf & "  ,(SELECT TOP 1 CASE WHEN ISDATE(FTRevisedDate) = 1 THEN Convert(varchar(10),Convert(datetime,FTRevisedDate) ,103) ELSE '' END FTRevisedDate"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised AS PR  WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTPurchaseNo=A.FTPurchaseNo"
        _Qry &= vbCrLf & " ORDER BY FNRevisedSeq DESC"
        _Qry &= vbCrLf & " ) AS FTRevisedDate"
        _Qry &= vbCrLf & "  ,(SELECT TOP 1 FTPurchaseRevisedBy"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_Revised AS PR  WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTPurchaseNo=A.FTPurchaseNo"
        _Qry &= vbCrLf & " ORDER BY FNRevisedSeq DESC"
        _Qry &= vbCrLf & " ) AS FTPurchaseRevisedBy"
        _Qry &= vbCrLf & "   FROM"
        _Qry &= vbCrLf & "  (SELECT        A.FTPurchaseNo, A.FDPurchaseDate,A.FTPurchaseBy,A.FNHSysDeliveryID, A.FDDeliveryDate, A.FTStateSendApp, A.FTSendAppDate, A.FTStateSuperVisorApp, A.FTSuperVisorAppDate, A.FTStateManagerApp,A.FNPoState, "
        _Qry &= vbCrLf & "   A.FTSuperManagerAppDate, A.FNHSysSuplId, A.FNHSysRawMatId, A.FNQuantity, A.FNHSysUnitId"
        _Qry &= vbCrLf & " 	 , ISNULL(B.FNQuantity,0) AS FNRcvQuantity,(ISNULL(C.FNQuantity,0) + ISNULL(D.FNQuantity,0)) AS FNRTsQuantity"
        _Qry &= vbCrLf & "  FROM            (SELECT        H.FTPurchaseNo, H.FDPurchaseDate,H.FTPurchaseBy,H.FNHSysDeliveryID, H.FDDeliveryDate, H.FTStateSendApp, H.FTStateSuperVisorApp, H.FTStateManagerApp, D.FNHSysRawMatId, H.FNHSysSuplId,MAX(H.FNPoState) As FNPoState, SUM(D.FNQuantity) "
        _Qry &= vbCrLf & "     AS FNQuantity, H.FTSendAppDate, H.FTSuperVisorAppDate, H.FTSuperManagerAppDate, D.FNHSysUnitId"
        _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS H  WITH(NOLOCK)   INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS D  WITH(NOLOCK)   ON H.FTPurchaseNo = D.FTPurchaseNo"
        _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS Sup WITH(NOLOCK) ON H.FNHSysSuplId = Sup.FNHSysSuplId "

        _Qry &= vbCrLf & " WHERE  H.FTPurchaseNo<>'' "

        If Me.FTStartPurchaseDate.Text <> "" Then
            _Qry &= vbCrLf & " AND  H.FDPurchaseDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartPurchaseDate.Text) & "' "
        End If

        If Me.FTEndPurchaseDate.Text <> "" Then
            _Qry &= vbCrLf & " AND  H.FDPurchaseDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndPurchaseDate.Text) & "' "
        End If

        If Me.FTStartDelivery.Text <> "" Then
            _Qry &= vbCrLf & " AND  H.FDDeliveryDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDelivery.Text) & "' "
        End If

        If Me.FTEndDelivery.Text <> "" Then
            _Qry &= vbCrLf & " AND  H.FDDeliveryDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDelivery.Text) & "' "
        End If

        If Me.FTPurchaseNo.Text <> "" Then
            _Qry &= vbCrLf & " AND  H.FTPurchaseNo >='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "' "
        End If

        If Me.FTPurchaseNoTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  H.FTPurchaseNo <='" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNoTo.Text) & "' "
        End If

        If Me.FNHSysSuplId.Text <> "" Then
            _Qry &= vbCrLf & " AND  Sup.FTSuplCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "' "
        End If

        If Me.FNHSysSuplIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  Sup.FTSuplCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplIdTo.Text) & "' "
        End If

        _Qry &= vbCrLf & "    GROUP BY H.FTPurchaseNo, H.FDPurchaseDate,H.FTPurchaseBy,H.FNHSysDeliveryID, H.FDDeliveryDate, H.FTStateSendApp, H.FTStateSuperVisorApp, H.FTStateManagerApp, D.FNHSysRawMatId, H.FNHSysSuplId, H.FTSendAppDate, "
        _Qry &= vbCrLf & "     H.FTSuperVisorAppDate, H.FTSuperManagerAppDate, D.FNHSysUnitId) AS A LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    (SELECT        A.FTPurchaseNo, B.FNHSysRawMatId, SUM(B.FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS B  WITH(NOLOCK)   ON A.FTReceiveNo = B.FTReceiveNo"
        _Qry &= vbCrLf & "    GROUP BY A.FTPurchaseNo, B.FNHSysRawMatId) AS B ON A.FTPurchaseNo = B.FTPurchaseNo AND A.FNHSysRawMatId = B.FNHSysRawMatId"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN ("
        _Qry &= vbCrLf & " 	SELECT        A.FTPurchaseNo, C.FNHSysRawMatId, SUM(B.FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & " 	FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS A WITH(NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B  WITH(NOLOCK)  ON A.FTReturnSuplNo = B.FTDocumentNo INNER JOIN"
        _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS C WITH(NOLOCK)  ON B.FTBarcodeNo = C.FTBarcodeNo"
        _Qry &= vbCrLf & " 	GROUP BY A.FTPurchaseNo, C.FNHSysRawMatId"
        _Qry &= vbCrLf & "   ) AS C ON A.FTPurchaseNo = C.FTPurchaseNo AND A.FNHSysRawMatId = C.FNHSysRawMatId"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN ("
        _Qry &= vbCrLf & " 	SELECT        A.FTPurchaseNo, C.FNHSysRawMatId, SUM(B.FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & " 	FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplierAfterIssue AS A WITH(NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & " 	     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT_RTS_AfterIssue AS B WITH(NOLOCK)  ON A.FTReturnSuplNo = B.FTDocumentNo INNER JOIN"
        _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS C WITH(NOLOCK)  ON B.FTBarcodeNo = C.FTBarcodeNo"
        _Qry &= vbCrLf & " 	GROUP BY A.FTPurchaseNo, C.FNHSysRawMatId"
        _Qry &= vbCrLf & "    ) AS D ON A.FTPurchaseNo = D.FTPurchaseNo AND A.FNHSysRawMatId = D.FNHSysRawMatId"
        _Qry &= vbCrLf & "   ) AS A   "
        _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH(NOLOCK) ON A.FNHSysRawMatId = M.FNHSysRawMatId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH(NOLOCK) ON M.FNHSysRawMatColorId = C.FNHSysRawMatColorId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH(NOLOCK) ON M.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON A.FNHSysUnitId = U.FNHSysUnitId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert AS UV WITH(NOLOCK) ON A.FNHSysUnitId = UV.FNHSysUnitId AND M.FNHSysUnitId =UV.FNHSysUnitIdTo "
        _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS Sup WITH(NOLOCK) ON A.FNHSysSuplId = Sup.FNHSysSuplId "
        _Qry &= vbCrLf & " ) AS A"


        _Qry &= vbCrLf & " ORDER BY  FTPurchaseNo,FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode  "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        Me.ogdtime.DataSource = _dt.Copy
        _dt.Dispose()
        _Spls.Close()

        _RowDataChange = False

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FTStartPurchaseDate.Text <> "" Then
            _Pass = True
        End If

        If Me.FTEndPurchaseDate.Text <> "" Then
            _Pass = True
        End If

        If Me.FTStartDelivery.Text <> "" Then
            _Pass = True
        End If

        If Me.FTEndDelivery.Text <> "" Then
            _Pass = True
        End If

        If Me.FTPurchaseNo.Text <> "" Then
            _Pass = True
        End If

        If Me.FTPurchaseNoTo.Text <> "" Then
            _Pass = True
        End If

        If Me.FNHSysSuplId.Text <> "" Then
            _Pass = True
        End If

        If Me.FNHSysSuplIdTo.Text <> "" Then
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

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)

            Dim StateShowSelect As Boolean = (ocmsendmail.Enabled)
            ochkselectall.Visible = StateShowSelect

            With Me.ogvtime
                .Columns.ColumnByFieldName("FTSelect").Visible = StateShowSelect
                .Columns.ColumnByFieldName("FTSelect").OptionsColumn.ShowInCustomizationForm = StateShowSelect
            End With
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

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvtime)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ogbheader_Click(sender As Object, e As EventArgs) Handles ogbheader.Click

    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        With Me.ogvtime
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _PoNo As String = "" & .GetFocusedRowCellValue("FTPurchaseNo").ToString
            Dim _FNPoState As Integer = 0

            Dim _Qry As String = ""

            _Qry = "Select TOP 1 FNPoState   "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_PoNo) & "' "

            _FNPoState = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PUR, "0")))

            With New HI.RP.Report

                Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language

                If _FNPoState = 0 Then
                    HI.ST.Lang.Language = ST.Lang.eLang.TH
                Else
                    HI.ST.Lang.Language = ST.Lang.eLang.EN
                End If

                .FormTitle = Me.Text
                .ReportFolderName = "PurchaseOrder\"
                .ReportName = "PurchaseOrder.rpt"
                .AddParameter("Draft", "DRAFT")
                .Formular = "{TPURTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(_PoNo) & "'"
                .Preview()

                HI.ST.Lang.Language = _tmplang
            End With

        End With
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub hideContainerTop_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ochkselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ochkselectall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With ogdtime
                If Not (.DataSource Is Nothing) And ogvtime.RowCount > 0 Then

                    With ogvtime
                        For I As Integer = 0 To .RowCount - 1
                            If .GetRowCellValue(I, .Columns.ColumnByFieldName("FTStateSuperVisorApp")).ToString = "1" Then
                                .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                            End If

                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmmail_Click(sender As Object, e As EventArgs) Handles ocmsendmail.Click


        'With New wPurchaseTrackingPIMail
        '    .ShowDialog()
        'End With
        Dim _CheckPath As String = "C:\WISDOMPOPDF"

        Try

            If Directory.Exists(_CheckPath) = False Then
                Directory.CreateDirectory(_CheckPath)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            Exit Sub
        End Try


        Try

            Dim dtpo As DataTable
            Dim dtpoList As DataTable


            With CType(Me.ogdtime.DataSource, DataTable)
                .AcceptChanges()

                dtpo = .Copy()

            End With

            If dtpo.Select("FTSelect='1' AND FTStateSuperVisorApp='1'").Length <= 0 Then

                Exit Sub
            End If

            dtpoList = dtpo.Select("FTSelect='1'  AND FTStateSuperVisorApp='1' ").CopyToDataTable

            Dim _FTMail As String = ""
            Dim _FTMailCC As String = ""
            Dim TemplateMail As String = ""
            Dim _Sql As String = ""
            Dim PoNo As String = ""
            Dim PoAllNo As String = ""
            Dim PoState As Integer = 0

            Dim grp As List(Of Integer) = (dtpoList.Select("FNHSysSuplId>0", "FNHSysSuplId").CopyToDataTable).AsEnumerable() _
                                                      .Select(Function(r) r.Field(Of Integer)("FNHSysSuplId")) _
                                                      .Distinct() _
                                                      .ToList()


            For Each Ind As Integer In grp
                _FTMail = ""
                _FTMailCC = ""
                TemplateMail = ""
                PoAllNo = ""
                Dim dtsupl As DataTable
                _Sql = "Select TOP 1 FTPOMailTo,FTPOMailCC,FRTemplateMail "
                _Sql &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier With(NOLOCK) "
                _Sql &= vbCrLf & " WHERE FNHSysSuplId=" & Integer.Parse(Val(Ind)) & ""

                dtsupl = HI.Conn.SQLConn.GetDataTable(_Sql, Conn.DB.DataBaseName.DB_MASTER)

                For Each Rmail As DataRow In dtsupl.Rows
                    _FTMail = Rmail!FTPOMailTo.ToString
                    _FTMailCC = Rmail!FTPOMailCC.ToString
                    TemplateMail = Rmail!FRTemplateMail.ToString

                Next
                dtsupl.Dispose()


                Dim _Spls As New HI.TL.SplashScreen("Creating....Mail Please Wait.")
                Try

                    Dim OutlookMessage As Outlook.MailItem
                    Dim AppOutlook As New Outlook.Application
                    Dim objNS As Outlook._NameSpace = AppOutlook.Session
                    Dim objFolder As Outlook.MAPIFolder
                    Dim oInsp As Outlook.Inspector
                    Dim mySignature As String

                    objFolder = objNS.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderDrafts)

                    Try
                        OutlookMessage = AppOutlook.CreateItem(Outlook.OlItemType.olMailItem)

                        With OutlookMessage


                            Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language
                            Try


                                For Each R As DataRow In dtpoList.Select("FNHSysSuplId = " & Val(Ind) & "")
                                    PoNo = R!FTPurchaseNo.ToString
                                    PoState = Val(R!FNPoState.ToString)


                                    If PoAllNo = "" Then

                                        PoAllNo = PoNo
                                    Else
                                        PoAllNo = PoAllNo & "," & PoNo
                                    End If

                                    With New HI.RP.Report
                                        .FormTitle = "Convert To " & PoNo & ".pdf"
                                        .ReportFolderName = "PurchaseOrder\"  '"Purchase Report\" '
                                        .ReportName = "PurchaseOrder.rpt"
                                        .AddParameter("Draft", "")
                                        .Formular = "{TPURTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(PoNo) & "'"

                                        ' ตรวจสอบ โฟร์เดอร์ก่อน


                                        .PathExport = _CheckPath & ""
                                        '.PathExport = "\\hisoft_svr\HI SOFT SYSTEM\PO PDF\" & Temp_FTPurchaseBy & "\"
                                        .ExportName = PoNo
                                        .ExportFile = HI.RP.Report.ExFile.PDF

                                        ' กรณีหาไฟล์ไม่เจอ  ????
                                        .PrevieNoSplash(PoState)

                                        Dim _FIleExportPDFName As String = .ExportFileSuccessName

                                    End With


                                    Try

                                        Dim Str_Doc_Name As String = _CheckPath & "\" & PoNo & ".pdf"


                                        If File.Exists(Str_Doc_Name) = True Then
                                            .Attachments.Add(Str_Doc_Name)
                                        End If

                                    Catch ex As Exception

                                    End Try


                                Next



                            Catch ex As Exception
                            End Try


                            .Display()
                            .To = _FTMail
                            .CC = _FTMailCC
                            .Subject = PoAllNo

                            oInsp = .GetInspector
                            mySignature = .HTMLBody

                            If TemplateMail <> "" Then

                                Try
                                    .HTMLBody = TemplateMail.Replace("{0}", PoAllNo) & mySignature
                                Catch ex As Exception

                                    .HTMLBody = "<p>" & "PO Ref No.  " & PoAllNo & "</p>" & mySignature
                                    '.Body = "PO Ref No.  " & PoAllNo & .Body
                                End Try


                            Else
                                '.Body = "PO Ref No.  " & PoAllNo & .Body
                                .HTMLBody = "<p>" & "PO Ref No.  " & PoAllNo & "</p>" & mySignature
                            End If

                            '.To = _FTMail
                            '.CC = _FTMailCC
                            '.Subject = PoAllNo

                            'If TemplateMail <> "" Then
                            '    .HTMLBody = TemplateMail.Replace("{0}", PoAllNo)
                            'Else
                            '    .Body = "PO Ref No.  " & PoAllNo
                            'End If

                            _Spls.Close()
                            '.Display(True)


                            Try
                                ' .Send()

                                Dim cmdstring As String
                                cmdstring = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase SET FTStateSendMail='1'"
                                cmdstring &= vbCrLf & ",FTSendMailBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                cmdstring &= vbCrLf & ",FTSendMailDate=" & HI.UL.ULDate.FormatDateDB & " "
                                cmdstring &= vbCrLf & ",FTSendMailTime=" & HI.UL.ULDate.FormatTimeDB & " "
                                cmdstring &= vbCrLf & ",FTSystemSendMailBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                cmdstring &= vbCrLf & ",FTSystemSendMailDate=" & HI.UL.ULDate.FormatDateDB & " "
                                cmdstring &= vbCrLf & ",FTSystemSendMailTime=" & HI.UL.ULDate.FormatTimeDB & " "
                                cmdstring &= vbCrLf & "  WHERE FTPurchaseNo IN ('" & PoAllNo.Replace(",", "','") & "')"
                                cmdstring &= vbCrLf & " select FTPurchaseNo,FTStateSendMail,FTSendMailBy,CASE WHEN ISDATE(FTSendMailDate) = 1 THEN Convert(varchar(10),Convert(datetime,FTSendMailDate),103) ELSE '' END AS  FTSendMailDate,FTSendMailTime "
                                cmdstring &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase   "
                                cmdstring &= vbCrLf & "  WHERE FTPurchaseNo IN ('" & PoAllNo.Replace(",", "','") & "')"

                                Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PUR)

                                For Each R As DataRow In dt.Rows

                                    'ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTStateSendMail", R!FTStateSendMail.ToString)
                                    'ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendMailBy", R!FTSendMailBy.ToString)
                                    'ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendMailDate", R!FTSendMailDate.ToString)
                                    'ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, "FTSendMailTime", R!FTSendMailTime.ToString)



                                    With CType(Me.ogdtime.DataSource, DataTable)
                                        .AcceptChanges()

                                        For Each Rxp As DataRow In .Select("FTPurchaseNo='" & HI.UL.ULF.rpQuoted(R!FTPurchaseNo.ToString) & "'")
                                            Rxp!FTSelect = "0"

                                            Rxp!FTStateSendMail = R!FTStateSendMail.ToString
                                            Rxp!FTSendMailBy = R!FTSendMailBy.ToString
                                            Rxp!FTSendMailDate = R!FTSendMailDate.ToString
                                            Rxp!FTSendMailTime = R!FTSendMailTime.ToString
                                        Next

                                        .AcceptChanges()

                                    End With


                                Next

                                dt.Dispose()




                            Catch ex As Exception

                            Finally

                            End Try

                        End With





                    Catch ex As Exception
                        _Spls.Close()
                        HI.MG.ShowMsg.mInfo("เนื่องจากพบข้อผิดพลาดบางประการ ระบบจึงไม่สามารถทำการส่งเมลล์ได้ !!!", 1408280001, Me.Text, , MessageBoxIcon.Warning)
                    Finally
                        OutlookMessage = Nothing
                        AppOutlook = Nothing
                    End Try



                Catch ex As Exception
                    _Spls.Close()
                    HI.MG.ShowMsg.mInfo("ไม่พบ Microsoft Outlook ไม่สามารถทำการส่งเมลล์ได้ !!!", 1408280002, Me.Text, , MessageBoxIcon.Warning)
                End Try


                For Each R As DataRow In dtpoList.Select("FNHSysSuplId = " & Val(Ind) & "")
                    PoNo = R!FTPurchaseNo.ToString

                    Try
                        If File.Exists(_CheckPath & "\" & PoNo & ".pdf") = True Then
                            File.Delete(_CheckPath & "\" & PoNo & ".pdf")
                        End If
                    Catch ex As Exception
                    End Try


                Next

            Next


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub RepCheckEdit_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepCheckEdit.EditValueChanging
        Try
            With ogvtime

                If .GetFocusedRowCellValue("FTStateSuperVisorApp").ToString = "1" Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub
End Class