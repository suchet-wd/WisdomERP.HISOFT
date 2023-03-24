Imports DevExpress.Data
Imports System.IO

Public Class wMEDStockCard

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Dim oSysLang As New ST.SysLanguage
        'Try
        '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ListBarcodeOnhand.Name.ToString.Trim, _ListBarcodeOnhand)
        'Catch ex As Exception
        'Finally
        'End Try
        'HI.TL.HandlerControl.AddHandlerObj(_ListBarcodeOnhand)
        Call InitGrid()
    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNQuantity|FNQuantityIss"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FNQuantity|FNQuantityIss"

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""


        With ogvtime
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsMenu.EnableColumnMenu = False
            .OptionsMenu.ShowAutoFilterRowItem = False
            .OptionsFilter.AllowFilterEditor = False
            .OptionsFilter.AllowColumnMRUFilterList = False
            .OptionsFilter.AllowMRUFilterList = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
            .OptionsCustomization.AllowFilter = False
            .OptionsCustomization.AllowSort = False
        End With

        With ogvtime
            .ClearGrouping()
            .ClearDocument()
            .ClearGrouping()
            .ClearDocument()
            .Columns("FTDrugCode").Group()
            '.Columns("FTDrugName").Group()
            .Columns("FTDrugUnitCode").Group()


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

        Dim _Cmd As String = ""
        Dim _dt As DataTable

        If Me.FTSDate.Text <> "" Or Me.FTEDate.Text <> "" Then
            If Me.FTSDate.Text = "" Then
                Me.FTSDate.Focus()
                Exit Sub
            End If
            If Me.FTEDate.Text = "" Then
                Me.FTEDate.Focus()
                Exit Sub
            End If
        End If


        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")


        _Cmd = " Select    T.FNHSysDrugId  , T.FTDocumentRefNo ,CASE WHEN ISDATE(T.FDMEDRcvDate) = 1 Then convert(nvarchar(10),convert(datetime,T.FDMEDRcvDate),103) Else '' End AS FDMEDRcvDate "
        _Cmd &= vbCrLf & " ,CASE WHEN T.FTMEDRcvNo<>'D-ISS' Then T.FTMEDRcvNo Else '' END AS FTMEDRcvNo"
        _Cmd &= vbCrLf & ",CASE WHEN T.FTMEDRcvNo<>'D-ISS' Then T.FNQuantity Else 0 END AS FNQuantity"
        _Cmd &= vbCrLf & ",CASE WHEN T.FTMEDRcvNo='D-ISS' Then T.FTMEDRcvNo Else '' END AS FTMEDIssNo"
        _Cmd &= vbCrLf & ",CASE WHEN T.FTMEDRcvNo='D-ISS' Then abs(T.FNQuantity) Else 0 END AS FNQuantityIss"

        _Cmd &= vbCrLf & ", D.FTDrugCode ,U.FTDrugUnitCode , U.FTDrugUnitNameEN , U.FTDrugUnitNameTH"
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Cmd &= vbCrLf & ", D.FTDrugNameTH  AS FTDrugName "
        Else
            _Cmd &= vbCrLf & ", D.FTDrugNameEN  AS FTDrugName "
        End If
        _Cmd &= vbCrLf & ",ISNULL(("
        _Cmd &= vbCrLf & " CASE WHEN T.FTMEDRcvNo = 'BAL' THEN "
        _Cmd &= vbCrLf & " ( "

        _Cmd &= vbCrLf & "Select SUM(FNQuantity) AS FNQuantity "
        _Cmd &= vbCrLf & "From (Select ROW_NUMBER() Over(Order by FNHSysDrugId,FTMEDRcvNo,FDMEDRcvDate ASC )  AS Seq,  FTMEDRcvNo, FDMEDRcvDate,  FNHSysDrugId,  FNQuantity  "
        _Cmd &= vbCrLf & "From ("
        _Cmd &= vbCrLf & "SELECT     H.FTMEDRcvNo, H.FDMEDRcvDate,   D.FNHSysDrugId,  D.FNQuantity , H.FTMEDRcvNo AS FTDocumentRefNo"
        _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTRecieve AS H WITH (NOLOCK) LEFT OUTER JOIN"
        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTRecieve_Detail AS D WITH (NOLOCK) ON H.FTMEDRcvNo = D.FTMEDRcvNo"

        _Cmd &= vbCrLf & "UNION ALL"
        _Cmd &= vbCrLf & "SELECT     'D-ISS' AS FTMEDRcvNo , H.FDMECDate, D.FNHSysDrugId, -D.FNQuantity, D.FTDocumentRefNo"
        _Cmd &= vbCrLf & "FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTGeneral AS H WITH (NOLOCK) INNER JOIN"
        _Cmd &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTDrugPay AS D WITH (NOLOCK) ON H.FNHSysMECGenId = D.FNHSysMECGenId "

      
        _Cmd &= vbCrLf & ") AS T  ) AS Z  "
        _Cmd &= vbCrLf & "Where FNHSysDrugId = T.FNHSysDrugId"
        _Cmd &= vbCrLf & "and  FDMEDRcvDate <'" & HI.UL.ULDate.ConvertEnDB(Me.FTSDate.Text) & "'"
        '_Cmd &= vbCrLf & "and Seq <= T.Seq"

        _Cmd &= vbCrLf & " ) ELSE  ("
        _Cmd &= vbCrLf & "Select SUM(FNQuantity) AS FNQuantity "
        _Cmd &= vbCrLf & "From (Select ROW_NUMBER() Over(Order by FNHSysDrugId,FTMEDRcvNo,FDMEDRcvDate ASC )  AS Seq,  FTMEDRcvNo, FDMEDRcvDate,  FNHSysDrugId,  FNQuantity  "
        _Cmd &= vbCrLf & "From ("
        _Cmd &= vbCrLf & "SELECT     H.FTMEDRcvNo, H.FDMEDRcvDate,   D.FNHSysDrugId,  D.FNQuantity , H.FTMEDRcvNo AS FTDocumentRefNo"
        _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTRecieve AS H WITH (NOLOCK) LEFT OUTER JOIN"
        _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTRecieve_Detail AS D WITH (NOLOCK) ON H.FTMEDRcvNo = D.FTMEDRcvNo"
        _Cmd &= vbCrLf & "UNION ALL"
        _Cmd &= vbCrLf & "SELECT     'D-ISS' AS FTMEDRcvNo , H.FDMECDate, D.FNHSysDrugId, -D.FNQuantity, D.FTDocumentRefNo"
        _Cmd &= vbCrLf & "FROM         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTGeneral AS H WITH (NOLOCK) INNER JOIN"
        _Cmd &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTDrugPay AS D WITH (NOLOCK) ON H.FNHSysMECGenId = D.FNHSysMECGenId "
        _Cmd &= vbCrLf & ") AS T  ) AS Z  "
        _Cmd &= vbCrLf & "Where FNHSysDrugId = T.FNHSysDrugId"
        If Me.FTSDate.Text = "" Then
            _Cmd &= vbCrLf & "and Seq <= T.Seq"
        End If


        _Cmd &= vbCrLf & " ) END  "
        _Cmd &= vbCrLf & "),0) AS FNQuantityBal"
        _Cmd &= vbCrLf & "From ("


        _Cmd &= vbCrLf & "Select ROW_NUMBER() Over(Order by FNHSysDrugId,FTMEDRcvNo,FDMEDRcvDate ASC )  AS Seq, "
        _Cmd &= vbCrLf & "FTMEDRcvNo, FDMEDRcvDate, FNHSysDrugId, FNQuantity, FTDocumentRefNo"
        _Cmd &= vbCrLf & "From ("
        If Me.FTSDate.Text <> "" And Me.FTEDate.Text <> "" Then
            _Cmd &= vbCrLf & "Select 'BAL' AS  FTMEDRcvNo,'' AS  FDMEDRcvDate, FNHSysDrugId, sum(FNQuantity) AS FNQuantity , FTDocumentRefNo"
            _Cmd &= vbCrLf & "From( SELECT     H.FTMEDRcvNo, H.FDMEDRcvDate,   D.FNHSysDrugId,  D.FNQuantity , H.FTMEDRcvNo AS FTDocumentRefNo"
            _Cmd &= vbCrLf & "FROM    [HITECH_MEDICAL].dbo.TMECTRecieve AS H WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "     [HITECH_MEDICAL].dbo.TMECTRecieve_Detail AS D WITH (NOLOCK) ON H.FTMEDRcvNo = D.FTMEDRcvNo"
            _Cmd &= vbCrLf & "WHERE H.FDMEDRcvDate  < '" & HI.UL.ULDate.ConvertEnDB(Me.FTSDate.Text) & "'"
            _Cmd &= vbCrLf & "          UNION ALL"
            _Cmd &= vbCrLf & "SELECT     'D-ISS' AS FTMEDRcvNo , H.FDMECDate, D.FNHSysDrugId, -D.FNQuantity, D.FTDocumentRefNo"
            _Cmd &= vbCrLf & "FROM   [HITECH_MEDICAL].dbo.TMECTGeneral AS H WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & " [HITECH_MEDICAL].dbo.TMECTDrugPay AS D WITH (NOLOCK) ON H.FNHSysMECGenId = D.FNHSysMECGenId "
            _Cmd &= vbCrLf & "WHERE H.FDMECDate < '" & HI.UL.ULDate.ConvertEnDB(Me.FTSDate.Text) & "'  "
            _Cmd &= vbCrLf & ") AS T"
            _Cmd &= vbCrLf & "Group by FNHSysDrugId,  FTDocumentRefNo"
            _Cmd &= vbCrLf & "   UNION ALL"
        End If

        _Cmd &= vbCrLf & "SELECT     H.FTMEDRcvNo, H.FDMEDRcvDate,   D.FNHSysDrugId,  D.FNQuantity , H.FTMEDRcvNo AS FTDocumentRefNo"
        _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTRecieve AS H WITH (NOLOCK) LEFT OUTER JOIN"
        _Cmd &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTRecieve_Detail AS D WITH (NOLOCK) ON H.FTMEDRcvNo = D.FTMEDRcvNo"
        If Me.FTSDate.Text <> "" Then
            _Cmd &= vbCrLf & "AND H.FDMEDRcvDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTSDate.Text) & "'"
        End If
        If Me.FTEDate.Text <> "" Then
            _Cmd &= vbCrLf & "AND H.FDMEDRcvDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEDate.Text) & "'"
        End If
        _Cmd &= vbCrLf & "      UNION ALL"
        _Cmd &= vbCrLf & "SELECT     'D-ISS' AS FTMEDRcvNo , H.FDMECDate, D.FNHSysDrugId, -D.FNQuantity, D.FTDocumentRefNo"
        _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTGeneral AS H WITH (NOLOCK) INNER JOIN"
        _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "].dbo.TMECTDrugPay AS D WITH (NOLOCK) ON H.FNHSysMECGenId = D.FNHSysMECGenId"
        If Me.FTSDate.Text <> "" Then
            _Cmd &= vbCrLf & "AND H.FDMECDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTSDate.Text) & "'"
        End If
        If Me.FTEDate.Text <> "" Then
            _Cmd &= vbCrLf & "AND H.FDMECDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEDate.Text) & "'"
        End If
        _Cmd &= vbCrLf & ") AS Z ) AS T "
        _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMECMDrug AS D WITH(NOLOCK) ON T.FNHSysDrugId = D.FNHSysDrugId"
        _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMECMDrugUnit AS U WITH(NOLOCK) ON D.FNHSysDrugUnitId_Rcv = U.FNHSysDrugUnitId"
        _Cmd &= vbCrLf & "WHERE D.FTDrugCode <> ''"

        If Me.FTSDate.Text <> "" Then
            _Cmd &= vbCrLf & "AND ((T.FDMEDRcvDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTSDate.Text) & "'"
        End If
        If Me.FTEDate.Text <> "" Then
            _Cmd &= vbCrLf & "AND T.FDMEDRcvDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEDate.Text) & "') OR T.FDMEDRcvDate='')"
        End If

        If Me.FNHSysDrugId.Text <> "" Then
            _Cmd &= vbCrLf & "AND D.FTDrugCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysDrugId.Text) & "'"
        End If
        If Me.FNHSysDrugIdTo.Text <> "" Then
            _Cmd &= vbCrLf & "AND D.FTDrugCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysDrugIdTo.Text) & "'"
        End If

        _Cmd &= vbCrLf & "group by T.Seq, T.FTMEDRcvNo, T.FDMEDRcvDate,  T.FNHSysDrugId,  T.FNQuantity , T.FTDocumentRefNo"
        _Cmd &= vbCrLf & ", D.FTDrugCode , D.FTDrugNameEN , D.FTDrugNameTH ,U.FTDrugUnitCode , U.FTDrugUnitNameEN , U.FTDrugUnitNameTH"
        _Cmd &= vbCrLf & "Order by  D.FTDrugCode ASC  , T.FTMEDRcvNo asc,T.FDMEDRcvDate asc"




        _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MEDC)

        Me.ogdtime.DataSource = _dt
        Try
            Me.ogvtime.ExpandAllGroups()
        Catch ex As Exception
        End Try

        _Spls.Close()

        _RowDataChange = False

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNHSysDrugId.Text <> "" Then
            _Pass = True
        End If
        If Me.FNHSysDrugIdTo.Text <> "" Then
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
            Me.ogvtime.OptionsView.ShowAutoFilterRow = False
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)
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

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub



End Class