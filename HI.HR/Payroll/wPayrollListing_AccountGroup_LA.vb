Imports DevExpress.Data
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Text

Public Class wPayrollListing_AccountGroup_LA
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

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

#End Region

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        'Dim sFieldCount As String = "N_Count"
        ' Dim sFieldSum As String = "FCNetBaht|OTBaht|FNIncentiveAmt|FNSocial|FNTax|FNNetpay"

        ' Dim sFieldGrpCount As String = "N_Count"
        ' Dim sFieldGrpSum As String = "FCNetBaht|FNSocial|FNTax|FNNetpay"

        Dim sFieldCustomSum As String = "FCNetBaht|FNSickLeaveBaht|FNPayLeaveVacationBaht|FNPayLeaveOtherBaht|FNParturitionLeaveBaht|OTBaht|F001|F009|F011|F017|F018|FStaleWage|FAdd|FNTotalIncome|FDeduct|FStudentLoan|FNSocial|FNSocialCmp|FNTax|FNNetpay|N_Count|F008|F053|F054|F055|F056|F057|F058|F059|F061|F062|F063|F064|F014|F043|FNNetpayAvg"

        'Dim sFieldCustomGrpSum As String = "FNWorkingDay"

        Dim t_flag As String = ""

        With ogv

            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTEmpTypeCode").Group()
            '.Columns("FTDeptCode").Group()
            '.Columns("FTDivisonCode").Group()
            '.Columns("FTSectCode").Group()
            '.Columns("FTUnitSectCode").Group()

            'For Each Str As String In sFieldCount.Split("|")
            '    If Str <> "" Then
            '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
            '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
            '    End If
            'Next

            For Each Str As String In sFieldCustomSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next


            'For Each Str As String In sFieldSum.Split("|")
            '    If Str <> "" Then
            '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
            '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
            '    End If
            'Next


            'For Each Str As String In sFieldGrpCount.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
            '    End If
            'Next

            'For Each Str As String In sFieldCustomGrpSum.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
            '    End If
            'Next

            'For Each Str As String In sFieldGrpSum.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
            '    End If
            'Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            .OptionsView.ShowGroupPanel = True
            .ExpandAllGroups()
            .RefreshData()

        End With
        '------End Add Summary Grid-------------
    End Sub
#End Region

#Region "Custom summaries"

    Private totalSum As Decimal = 0
    Private GrpSum As Decimal = 0

    Private Sub InitStartValue()
        totalSum = 0
        GrpSum = 0
    End Sub

    Private Sub Gridview_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogv.CustomSummaryCalculate
        If e.SummaryProcess = CustomSummaryProcess.Start Then
            InitStartValue()
        End If

        Dim flag As String = ""
        Dim view As GridView
        view = sender
        flag = view.GetRowCellValue(e.RowHandle, "t_flag").ToString
        'flag = ogv.GetFocusedRowCellValue("t_flag").ToString
        If flag = "2" Then


            Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
                Case "FCNetBaht", "OTBaht", "F001", "F009", "F011", "F017", "F018", "FStaleWage", "FAdd", "FStudentLoan", "FNTotalIncome", "FDeduct", "FNSocial", "FNTax", "FNNetpay", "N_Count", "F008", "F053", "F054", "F055", "F056", "F057", "F058", "F059", "F061", "F062", "F063", "F064", "FNSocialCmp"
                    ''   FCNetBaht|OTBaht|FNIncentiveAmt|F009|F011|FAdd|FStudentLoan|FNSocial|FNTax|FNNetpay|N_Count" F008|F053|F054|F055|F056|F057|F058|F059|F061|F062|F063|F064"
                    If e.SummaryProcess = CustomSummaryProcess.Calculate Then

                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then


                            If e.IsTotalSummary Then
                                totalSum = totalSum + Decimal.Parse(Val(e.FieldValue.ToString))

                            End If

                        End If



                        If e.IsTotalSummary Then

                            e.TotalValue = totalSum ' totalSum 'NetDisplay

                        End If
                    End If
            End Select

        End If

    End Sub

#End Region

#Region "Procedure"
    Private Sub LoadDataInfo()


        ' Dim _Dt As DataTable = Nothing
        Dim _Qry As String = ""
        Dim dt As New DataTable

        Dim Lang As String = "TH"
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            Lang = "TH"
        Else
            Lang = "EN"
        End If

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GetPayroll_ByTerm_LA '" & FTPayTerm.Text & "','" & FTPayYear.Text & "','" & Val(HI.ST.SysInfo.CmpID) & "' , '" & Lang & "'"
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        With Me.ogc
            .DataSource = dt
            ''   ogv.ExpandAllGroups()
            ''  ogv.RefreshData()
            ogv.BestFitColumns()
        End With

    End Sub

#End Region

#Region "General"
    Private Sub wEmployeeListing_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Call InitGrid()
        Me.FTPayYear.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTPayYear FROM  THRMCfgPayHD WITH(NOLOCK) Order BY FTPayYear Desc", Conn.DB.DataBaseName.DB_HR, "")
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Me.FTPayYear.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTPayYear FROM  THRMCfgPayHD WITH(NOLOCK) Order BY FTPayYear Desc", Conn.DB.DataBaseName.DB_HR, "")
    End Sub

    Private Sub ocmload_Click(sender As System.Object, e As System.EventArgs) Handles ocmload.Click

        If Me.FTPayYear.Text <> "" And FTPayYear.Text.Length = 4 Then
            If Me.FTPayTerm.Text <> "" Then
                Me.LoadDataInfo()
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPayTerm_lbl.Text)
                FTPayTerm.Focus()
                FTPayTerm.SelectAll()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPayYear_lbl.Text)
            FTPayYear.Focus()
            FTPayYear.SelectAll()
        End If

    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click

    End Sub



    Private Sub ogv_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogv.RowCellStyle

        With ogv


            Try
                If .GetRowCellValue(e.RowHandle, "t_flag") = "2" Then
                    e.Appearance.BackColor = System.Drawing.Color.GreenYellow
                    e.Appearance.Font = New System.Drawing.Font("tahoma", 8, System.Drawing.FontStyle.Bold)

                End If
                'If .GetRowCellValue(e.RowHandle, "t_flag") = "1" Then

                '    .SetRowCellValue(e.RowHandle, "FTCostName", "")
                'End If
            Catch ex As Exception
            End Try

        End With
    End Sub
End Class