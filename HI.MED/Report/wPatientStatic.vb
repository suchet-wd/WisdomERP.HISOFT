Public Class wPatientStatic 


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
        Dim sFieldSum As String = "" '"FTGrpType"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FTGrpType"

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = "" '"[01],[02],[03],[04],[05]"


        With ogvDetail
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

        With ogvDetail
            
            .ClearGrouping()
            .ClearDocument()
            .Columns("FTGrpType").Group()
        


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
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing) ', "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})"
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing) ', "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n" & HI.ST.Config.QtyDigit.ToString & "})"
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


    Private Sub GetData()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = " Select Seq, FTGrpType , FTTypeofDiseaseCode"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", FTTypeofDiseaseNameTH AS FTTypeofDiseaseName"
            Else
                _Cmd &= vbCrLf & ", FTTypeofDiseaseNameEN AS FTTypeofDiseaseName"
            End If
            _Cmd &= vbCrLf & ",Isnull([01],0) AS [01],Isnull([02],0) AS [02],Isnull([03],0) AS [03],Isnull([04],0) AS [04]"
            _Cmd &= vbCrLf & ",Isnull([05],0) AS [05],Isnull([06],0) AS [06],Isnull([07],0) AS [07]"
            _Cmd &= vbCrLf & ",Isnull([08],0) AS [08] ,Isnull([09],0) AS [09],Isnull([10],0) AS [10]"
            _Cmd &= vbCrLf & ",Isnull([11],0) AS [11],Isnull([12],0) AS [12]"
            _Cmd &= vbCrLf & "From(SELECT  1 AS Seq , '" & HI.UL.ULF.rpQuoted(Me.FTTypeofDiseaseCode.Text) & "' AS FTGrpType, T.FTTypeofDiseaseCode, T.FTTypeofDiseaseNameTH, T.FTTypeofDiseaseNameEN"
            _Cmd &= vbCrLf & ", COUNT(H.FNHSysEmpId) AS FNHSysEmpId" ' --, H.FDMECDate, H.FTMECTime"
            _Cmd &= vbCrLf & ",RIGHT('0'+convert(nvarchar(2),MONTH(convert(datetime,H.FDMECDate))),2) AS FTMONTH"
            _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTGeneral AS H WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMECMTypeofDisease AS T WITH (NOLOCK) ON H.FNHSysTypeofDiseaseId = T.FNHSysTypeofDiseaseId"
            _Cmd &= vbCrLf & "WHERE YEAR(convert(datetime,H.FDMECDate)) = '" & HI.UL.ULF.rpQuoted(Me.FTYear.Text) & "'"

            Select Case Me.FNRptType.SelectedIndex
                Case 1
                    _Cmd &= vbCrLf & " And H.FTMECTime >='08:00:00' And H.FTMECTime <= '17:25:00'"
                Case 2
                    _Cmd &= vbCrLf & " And H.FTMECTime >='17:25:01'   "
            End Select

            _Cmd &= vbCrLf & "group by  T.FTTypeofDiseaseCode, T.FTTypeofDiseaseNameTH, T.FTTypeofDiseaseNameEN , MONTH(convert(datetime,H.FDMECDate))"
            _Cmd &= vbCrLf & "UNION ALL"
            _Cmd &= vbCrLf & "SELECT     2 AS Seq, '" & HI.UL.ULF.rpQuoted(Me.FTAccident.Text) & "' AS FTGrpType,'' as FTTypeofDiseaseCode, L.FTNameTH, L.FTNameEN, COUNT(H.FNHSysEmpId) AS FNHSysEmpId, "
            _Cmd &= vbCrLf & "      RIGHT('0' + CONVERT(nvarchar(2), MONTH(CONVERT(datetime, H.FDDate))), 2) AS FTMONTH"
            _Cmd &= vbCrLf & "FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTAccident AS H WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "(SELECT      FNListIndex, FTNameTH, FTNameEN "
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE     (FTListName = N'FNAccidentType')) AS L   ON H.FNAccidentType = L.FNListIndex"
            _Cmd &= vbCrLf & "WHERE     (YEAR(CONVERT(datetime, H.FDDate)) = '" & HI.UL.ULF.rpQuoted(Me.FTYear.Text) & "')"
            Select Case Me.FNRptType.SelectedIndex
                Case 1
                    _Cmd &= vbCrLf & " And H.FTTime >='08:00:00' And H.FTMECTime <= '17:25:00'"
                Case 2
                    _Cmd &= vbCrLf & " And H.FTTime >='17:25:01'   "
            End Select
            _Cmd &= vbCrLf & "GROUP BY  L.FTNameTH, L.FTNameEN, MONTH(CONVERT(datetime, H.FDDate))"

            _Cmd &= vbCrLf & "   UNION ALL"
            _Cmd &= vbCrLf & "SELECT     4 AS Seq, '" & HI.UL.ULF.rpQuoted(Me.FTConsulting.Text) & "' AS FTGrpType,'' as FTTypeofDiseaseCode,  'การให้คำปรึกษา' as FTNameTH,   'Consulting' as FTNameEN, COUNT(H.FNHSysEmpId) AS FNHSysEmpId, "
            _Cmd &= vbCrLf & "                     RIGHT('0' + CONVERT(nvarchar(2), MONTH(CONVERT(datetime, H.FDDate))), 2) AS FTMONTH"
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTConsul AS H WITH (NOLOCK)  "

            _Cmd &= vbCrLf & "WHERE     (YEAR(CONVERT(datetime, H.FDDate)) = '" & HI.UL.ULF.rpQuoted(Me.FTYear.Text) & "')"
            Select Case Me.FNRptType.SelectedIndex
                Case 1
                    _Cmd &= vbCrLf & " And H.FTTime >='08:00:00' And H.FTMECTime <= '17:25:00'"
                Case 2
                    _Cmd &= vbCrLf & " And H.FTTime >='17:25:01'   "
            End Select
            _Cmd &= vbCrLf & "GROUP BY   MONTH(CONVERT(datetime, H.FDDate))"

            _Cmd &= vbCrLf & "UNION ALL "
            _Cmd &= vbCrLf & "SELECT  3 AS Seq, '" & HI.UL.ULF.rpQuoted(Me.FTOpinionCode.Text) & "' AS FTGrpType, D.FTOpinionCode, D.FTOpinionNameTH, D.FTOpinionNameEN "
            _Cmd &= vbCrLf & ",  COUNT(H.FNHSysEmpId) AS FNHSysEmpId"
            _Cmd &= vbCrLf & ",RIGHT('0'+convert(nvarchar(2),MONTH(convert(datetime,H.FDMECDate))),2) AS FTMONTH"
            _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MEDC) & "]..TMECTGeneral AS H WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMECMOpinion AS D WITH (NOLOCK) ON H.FNHSysOpinionId = D.FNHSysOpinionId"
            _Cmd &= vbCrLf & "WHERE YEAR(convert(datetime,H.FDMECDate)) = '" & HI.UL.ULF.rpQuoted(Me.FTYear.Text) & "'"
            _Cmd &= vbCrLf & "AND D.FTOpinionNameEN Not Like '%Work%'"

            Select Case Me.FNRptType.SelectedIndex
                Case 1
                    _Cmd &= vbCrLf & " And H.FTMECTime >='08:00:00' And H.FTMECTime <= '17:25:00'"
                Case 2
                    _Cmd &= vbCrLf & " And H.FTMECTime >='17:25:01'   "
            End Select

            _Cmd &= vbCrLf & "group by  D.FTOpinionCode, D.FTOpinionNameTH, D.FTOpinionNameEN , MONTH(convert(datetime,H.FDMECDate))  ) AS T "


            _Cmd &= vbCrLf & "pivot(sum(FNHSysEmpId) For FTMONTH IN( [01] ,[02],[03],[04], [05] ,[06],[07], [08] ,[09],[10], [11] ,[12])) AS Pv"
            _Cmd &= vbCrLf & "Order by seq"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
            Me.ogcDetail.DataSource = _oDt
            Me.ogvDetail.Columns("Seq").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            Me.ogvDetail.ExpandAllGroups()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If Not (VerrifyData()) Then Exit Sub
            Call GetData()
        Catch ex As Exception
        End Try
    End Sub
    Private Function VerrifyData() As Boolean
        Try
            If Me.FTYear.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTYear_lbl.Text)
                Me.FTYear.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvDetail)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub wPatientStatic_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.ogvDetail.OptionsView.ShowAutoFilterRow = False
        Catch ex As Exception

        End Try
    End Sub
End Class