Imports DevExpress.Data
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports System.IO

Public Class wTimeLeaveListingDocket

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _ActualDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

        'Me.FNHSysCmpId.Text = "1"
        'Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        'Me.FNHSysCmpId.Properties.ReadOnly = True
        'Me.FNHSysCmpId.Properties.Buttons(0).Visible = False
        'Me.FNHSysCmpId.Properties.Buttons(0).Enabled = False

        Call InitGrid()

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()

        '_CfgIncentiveAmtDigit = HI.Conn.SQLConn.GetField("SELECT FTCfgData FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].[dbo].[TSESystemConfig] WHERE FTCfgName = 'CfgIncentiveAmtDigit' ", Conn.DB.DataBaseName.DB_SECURITY, "")


        ''------Start Add Summary Grid-------------
        'Dim sFieldCount As String = "FTEmpCode"
        'Dim sFieldSum As String = "FNNetProAmt|FNNetAmt|FNNetPayAmt|FNQAAmt|FNProOT|FNProNormal|FNAmtOT|FNAmtNormal|FNNetPay|FNNetIncen|FNAmt|FNNetPerDay"
        'Dim sFieldGrpCount As String = "FTDateTrans"
        'Dim sFieldGrpSum As String = "FNNetProAmt|FNNetAmt|FNNetPayAmt|FNQAAmt|FNProOT|FNProNormal|FNAmtOT|FNAmtNormal|FNNetPay|FNNetIncen|FNAmt|FNNetPerDay"

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
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n" + _CfgIncentiveAmtDigit + "}"
        '            .Columns(Str).DisplayFormat.FormatString = "{0:n" + _CfgIncentiveAmtDigit + "}"
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
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
        '        End If
        '    Next

        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        '    .OptionsView.ShowFooter = True
        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

        '    .ExpandAllGroups()
        '    .RefreshData()


        ' End With
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




    Private Sub Gridview_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvDocket.CustomSummaryCalculate
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

                        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
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

    Private _CfgIncentiveAmtDigit As String = ""
    Public Property CfgIncentiveAmtDigit As String
        Get
            Return _CfgIncentiveAmtDigit
        End Get
        Set(value As String)
            _CfgIncentiveAmtDigit = value
        End Set
    End Property


#End Region

#Region "Procedure"

    Private Sub LoadData()

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        Dim _FTStartCalculateDate As String = ""
        Dim _FTEndCalculateDate As String = ""

        'Dim _FTStartCalculateDate As String = HI.UL.ULDate.ConvertEnDB(FDStartDate.Text)
        'Dim _FTEndCalculateDate As String = HI.UL.ULDate.ConvertEnDB(FDEndDate.Text)

        Dim _Lang As String = ""
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Lang = "TH"
        Else
            _Lang = "EN"
        End If

        Dim List_Nationality As String = ""

        With ogcNationality
            If Not (.DataSource Is Nothing) And ogvNationality.RowCount > 0 Then

                With ogvNationality
                    For I As Integer = 0 To .RowCount - 1

                        If .GetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect")).ToString = "1" Then
                            If List_Nationality <> "" Then
                                List_Nationality = List_Nationality & "|" & .GetRowCellValue(I, .Columns.ColumnByFieldName("FNHSysNationalityId")).ToString
                            Else
                                List_Nationality = .GetRowCellValue(I, .Columns.ColumnByFieldName("FNHSysNationalityId")).ToString
                            End If
                        End If
                    Next
                End With

                CType(.DataSource, DataTable).AcceptChanges()
            End If

        End With

        List_Nationality = "[" & List_Nationality & "]"

        Dim _SqlWhere As String = "1 "
        Dim _FTUnitSectCode As String = ""
        Dim _FTUnitSectCodeTo As String = ""


        Dim day As Integer = 0
        Select Case Me.FNDayInAdvance.SelectedIndex
            Case 0
                day = 7
            Case 1
                day = 15
            Case Else
                day = 7
        End Select

        Dim sex As Integer = 99
        If Me.FNEmpSex.SelectedIndex >= 0 Then
            sex = FNEmpSex.SelectedIndex
        End If


        If _SqlWhere <> "" Then
            _SqlWhere = "'" + _SqlWhere + "'"
        Else
            _SqlWhere = "1"
        End If


        Dim _FNHSysCmpId As Integer = 0
        _FNHSysCmpId = HI.ST.SysInfo.CmpID

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GET_DOCKET_TIME_LEAVE '" & _FTStartCalculateDate & "','" & _FTEndCalculateDate & "'," & _Lang & "," & _FNHSysCmpId & "," & day & "," & List_Nationality & "," & sex
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        _Spls.Close()


        _Qry = ""




        Dim _colcount As Integer = 0

        With Me.ogvDocket

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FNListIndex".ToUpper, "FTNameTH".ToUpper, "FTNameEN".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FNListIndex".ToUpper, "FTNameTH".ToUpper, "FTNameEN".ToUpper
                        Case Else
                            _colcount = _colcount + 1
                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                .Visible = True
                                .FieldName = Col.ColumnName.ToString

                                .Name = "FTDate" & Col.ColumnName.ToString
                                If IsDate(Col.ColumnName.ToString) Then

                                    .Caption = Format(CType(Col.ColumnName.ToString, Date), "dd/MM/yyyy")
                                Else
                                    .Caption = Col.ColumnName.ToString
                                End If

                            End With

                            .Columns.Add(ColG)

                            With .Columns(Col.ColumnName.ToString)

                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                '.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                '.DisplayFormat.FormatString = "{0:n2}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                .Width = 150

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False
                                    .ReadOnly = True
                                End With

                            End With

                            .Columns(Col.ColumnName.ToString).Width = 90
                            .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n2}"

                    End Select

                Next

            End If


        End With

        Me.ogcDocket.DataSource = _dt.Copy

        _colcount = 0


    End Sub

    Private Sub ogvDocket_CustomDrawCell(sender As Object, e As RowCellCustomDrawEventArgs) Handles ogvDocket.CustomDrawCell
        Select Case e.Column.FieldName.ToString.ToUpper

            Case "FNListIndex".ToUpper, "FTNameTH".ToUpper, "FTNameEN".ToUpper
            Case Else
                If ogvDocket.GetRowCellValue(e.RowHandle, "FNListIndex") = "905" Then
                    e.DisplayText = Format(e.CellValue, "0.00") & " %"
                Else



                    'If e.CellValue.ToString = "0.00" Then
                    '    e.DisplayText = ""
                    'Else
                    '    e.DisplayText = Format(e.CellValue, "#0")
                    'End If


                End If


        End Select

        'If e.RowHandle = GridControl.AutoFilterRowHandle Then
        '    e.DisplayText = "AutoFilterRow"
        'End If
        'If e.RowHandle = GridControl.NewItemRowHandle Then
        '    e.DisplayText = "NewItemRow"
        'End If
    End Sub

#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Me.FNHSysCmpId.Text = "1"
            'Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvDocket)
            StateCal = False


            FNEmpSex.SelectedIndex = -1


            bind_nationality()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub bind_nationality()

        Dim _Qry As String = ""
        Dim dt As DataTable
        Dim _Dt As DataTable = Nothing

        _Qry = " SELECT  '1' as FTSelect, FTNationalityCode,FTNationalityNameEN AS FTDescription,FNHSysNationalityId  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMNationality WITH ( NOLOCK ) WHERE  FTStateActive ='1' "
        _Qry &= vbCrLf & " AND FNHSysNationalityId IN ( SELECT DISTINCT( FNHSysNationalityId ) FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee  WHERE FNHSysCmpId = " & HI.ST.SysInfo.CmpID & ")  "
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        If _Dt Is Nothing Then
            _Dt = dt.Copy
        Else
            _Dt.Merge(dt.Copy)
        End If
        dt.Dispose()

        Me.ogcNationality.DataSource = _Dt.Copy
        Me.ogvNationality.ExpandAllGroups()
        _Dt.Dispose()


        'With ogcNationality
        '    If Not (.DataSource Is Nothing) And ogvNationality.RowCount > 0 Then

        '        With ogvNationality
        '            For I As Integer = 0 To .RowCount - 1
        '                .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), 1)
        '            Next
        '        End With

        '        CType(.DataSource, DataTable).AcceptChanges()
        '    End If

        'End With

    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        'If Me.FDStartDate.Text <> "" Then
        '    If Me.FDEndDate.Text <> "" Then

        Call LoadData()

        '    Else
        '        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDEndDate_lbl.Text)
        '        FDEndDate.Focus()
        '    End If
        'Else
        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateRequest_lbl.Text)
        '    FDStartDate.Focus()
        'End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvDocket)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        With Me.ogvDocket

            Dim _Spls As New HI.TL.SplashScreen("Generating data...   Please Wait  ")
            Me.LoadData()
            _Spls.Close()

            Dim _Qry As String = ""

            _Qry &= "   {THRMEmployee.FNHSysCmpId}=" & HI.ST.SysInfo.CmpID & ""


            With New HI.RP.Report

                'If Me.FDStartDate.Text <> "" Then
                '    .AddParameter("SFTDate", Me.FDStartDate.Text)
                'End If

                'If Me.FDEndDate.Text <> "" Then
                '    .AddParameter("EFTDate", Me.FDEndDate.Text)
                'End If

                .FormTitle = Me.Text
                .ReportFolderName = "Human Report\"
                .ReportName = "RptSample.rpt"
                .Formular = _Qry
                .Preview()
            End With

        End With
    End Sub


End Class