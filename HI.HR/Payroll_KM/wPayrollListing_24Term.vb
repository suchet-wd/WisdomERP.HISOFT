Imports DevExpress.CodeParser
Imports DevExpress.Data
Imports DevExpress.XtraPrinting.Native
Imports System.Text

Public Class wPayrollListing_24Term
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
    'Private Sub InitGrid()
    '    '------Start Add Summary Grid-------------
    '    Dim sFieldCount As String = "FTEmpCode"
    '    Dim sFieldSum As String = "FCBaht|FCOt1_Baht|FCOt15_Baht|FCOt2_Baht|FCOt3_Baht|FCOt4_Baht|FNIncentiveAmt|FCNetBaht|FNPayLeaveVacationBaht|FNPayLeaveOtherBaht|FNTotalAdd|FNTotalAddOther|FNTotalExpense|FNTotalExpenseOther|FNTotalIncome|FNTotalRecalSSO|FNSocial|FNTax|FHolidayBaht|FNNetpayDiff|FNNetpay|FTUnionDuesAmt"
    '    sFieldSum &= "|FNSeniorityAmt44|FNSeniorityAmt45|FNSeniorityAmt46|FNSeniorityAmt47"
    '    Dim sFieldGrpCount As String = "FTEmpCode"
    '    Dim sFieldGrpSum As String = "FCBaht|FNTotalIncome|FNTotalRecalSSO|FNSocial|FNTax|FNNetpay"

    '    Dim sFieldCustomSum As String = "FNWorkingDay|FNOt1|FNOt15|FNOt2|FNOt3|FNOt4"
    '    Dim sFieldCustomGrpSum As String = "FNWorkingDay"

    '    With ogv
    '        .ClearGrouping()
    '        .ClearDocument()
    '        ' .Columns("FTEmpTypeCode").Group()
    '        '.Columns("FTDeptCode").Group()
    '        '.Columns("FTDivisonCode").Group()
    '        ' .Columns("FTSectCode").Group()
    '        ' .Columns("FTUnitSectCode").Group()

    '        For Each Str As String In sFieldCount.Split("|")
    '            If Str <> "" Then
    '                .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
    '                .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
    '            End If
    '        Next

    '        For Each Str As String In sFieldCustomSum.Split("|")
    '            If Str <> "" Then
    '                .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
    '                .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
    '            End If
    '        Next


    '        For Each Str As String In sFieldSum.Split("|")
    '            If Str <> "" Then
    '                .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
    '                .Columns(Str).SummaryItem.DisplayFormat = "{0:n3}"
    '            End If
    '        Next

    '        For Each Str As String In sFieldGrpCount.Split("|")
    '            If Str <> "" Then
    '                .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
    '            End If
    '        Next

    '        For Each Str As String In sFieldCustomGrpSum.Split("|")
    '            If Str <> "" Then
    '                .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
    '            End If
    '        Next

    '        For Each Str As String In sFieldGrpSum.Split("|")
    '            If Str <> "" Then
    '                .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n3})")
    '            End If
    '        Next

    '        .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
    '        .OptionsView.ShowFooter = True
    '        .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
    '        .OptionsView.ShowGroupPanel = True
    '        .ExpandAllGroups()
    '        .RefreshData()

    '    End With
    '    '------End Add Summary Grid-------------
    'End Sub
#End Region

#Region "Custom summaries"

    Private totalSum As Integer = 0
    Private GrpSum As Integer = 0

    Private Sub InitStartValue()
        totalSum = 0
        GrpSum = 0
    End Sub



#End Region

#Region "Procedure"

    Private Sub LoadDataInfo()
        Dim _Spls As New HI.TL.SplashScreen("Load Data Please Waiting......")
        Dim _Qry As String = ""
        Try
            Dim _Lang As String = ""
            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Lang = "TH"
            Else
                _Lang = "EN"
            End If

            _Qry &= vbCrLf & " DELETE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.TmpEmpIdPayRoll24 WHERE  FTUserLogIn = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.TmpEmpIdPayRoll24 (FNHSysEmpID, FTUserLogIn) "
            _Qry &= vbCrLf & " SELECT  M.FNHSysEmpID , '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS OrgPosit WITH (NOLOCK) ON M.FNHSysPositId = OrgPosit.FNHSysPositId LEFT OUTER JOIN"

            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPositionGrp AS G WITH(NOLOCK) ON OrgPosit.FNHSysPositGrpId = G.FNHSysPositGrpId "

            Dim _SqlWhere As String = "1 "

            _Qry &= vbCrLf & " WHERE 1=1 "
            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & " AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
            End If

            '------Criteria By Employeee Code
            If Me.FNHSysEmpId.Text <> "" Then
                _Qry &= vbCrLf & " AND M.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
            End If

            If Me.FNHSysEmpIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND M.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
            End If

            '------Criteria By Department
            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & " AND  Dept.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
            End If

            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  Dept.FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
            End If

            '------Criteria By Division
            If Me.FNHSysDivisonId.Text <> "" Then
                _Qry &= vbCrLf & " AND  Di.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
            End If

            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  Di.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
            End If

            '------Criteria By Sect
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND  ST.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
            End If

            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  ST.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
            End If
            '------Criteria Unit Sect
            Dim _FTUnitSectCode As String = ""
            Dim _FTUnitSectCodeTo As String = ""

            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND   US.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
            End If

            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND   US.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
            End If

            'Dim _SqlWhere As String = "1 "
            'If Me.FNHSysEmpTypeId.Text <> "" Then
            '    _Qry &= vbCrLf & " AND ET.FTEmpTypeCode=''" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "'' "
            'End If

            '''------Criteria By Employeee Code
            'If Me.FNHSysEmpId.Text <> "" Then
            '    _Qry &= vbCrLf & " AND E.FTEmpCode >=''" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "'' "
            'End If

            'If Me.FNHSysEmpIdTo.Text <> "" Then
            '    _Qry &= vbCrLf & " AND E.FTEmpCode <=''" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "'' "
            'End If

            '''------Criteria By Department
            'If Me.FNHSysDeptId.Text <> "" Then
            '    _Qry &= vbCrLf & " AND  D.FTDeptCode>=''" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "'' "
            'End If

            'If Me.FNHSysDeptIdTo.Text <> "" Then
            '    _Qry &= vbCrLf & " AND  D.FTDeptCode<=''" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "'' "
            'End If

            '''------Criteria By Division
            'If Me.FNHSysDivisonId.Text <> "" Then
            '    _Qry &= vbCrLf & " AND  Di.FTDivisonCode>=''" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "'' "
            'End If

            'If Me.FNHSysDivisonIdTo.Text <> "" Then
            '    _Qry &= vbCrLf & " AND  Di.FTDivisonCode<=''" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "'' "
            'End If

            '''------Criteria By Sect
            'If Me.FNHSysSectId.Text <> "" Then
            '    _Qry &= vbCrLf & " AND  S.FTSectCode>=''" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "'' "
            'End If

            'If Me.FNHSysSectIdTo.Text <> "" Then
            '    _Qry &= vbCrLf & " AND  S.FTSectCode<=''" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "'' "
            'End If
            '''------Criteria Unit Sect
            'Dim _FTUnitSectCode As String = ""
            'Dim _FTUnitSectCodeTo As String = ""

            'If Me.FNHSysUnitSectId.Text <> "" Then
            '    _Qry &= vbCrLf & " AND   US.FTUnitSectCode>=''" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "'' "
            'End If

            'If Me.FNHSysUnitSectIdTo.Text <> "" Then
            '    _Qry &= vbCrLf & " AND   US.FTUnitSectCode<=''" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "'' "
            'End If



            Dim _From As String = ""
            Dim _To As String = ""

            _From = FTPayYearTermFrom.SelectedItem.ToString

            _To = FTPayYearTermTo.SelectedItem.ToString

            ''  _SqlWhere = _SqlWhere & HI.ST.Security.PermissionFilterEmployeeSalary()
            _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)
            ' HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


            'If _SqlWhere <> "" Then
            '    _SqlWhere = "'" + _SqlWhere + "'"
            'Else
            '    _SqlWhere = "1"
            'End If


            _SqlWhere = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)

            Dim _dt As DataTable

            _Qry &= vbCrLf & ""
            _Qry &= vbCrLf & " Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GetPayroll_24Term '" & _From & "','" & _To & "'," & _Lang & "," & _SqlWhere
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


            Dim _colcount As Integer = 0



            With Me.ogv

                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case .Columns(I).FieldName.ToString.ToUpper

                        Case "FNHSysEmpID".ToUpper, "FTEmpCode".ToUpper, "FTEmpName".ToUpper, "FTEmpTypeCode".ToUpper, "FTDeptCode".ToUpper, "FTDivisonCode".ToUpper, "FTSectCode".ToUpper, "FTUnitSectCode".ToUpper, "FTPositCode".ToUpper, "FinDesc".ToUpper, "seq".ToUpper, "code".ToUpper
                            .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                        Case Else
                            .Columns.Remove(.Columns(I))
                    End Select
                Next

                If Not (_dt Is Nothing) Then
                    For Each Col As DataColumn In _dt.Columns

                        Select Case Col.ColumnName.ToString.ToUpper
                            Case "FNHSysEmpID".ToUpper, "FTEmpCode".ToUpper, "FTEmpName".ToUpper, "FTEmpTypeCode".ToUpper, "FTDeptCode".ToUpper, "FTDivisonCode".ToUpper, "FTSectCode".ToUpper, "FTUnitSectCode".ToUpper, "FTPositCode".ToUpper, "FinDesc".ToUpper, "seq".ToUpper, "code".ToUpper
                            Case Else
                                _colcount = _colcount + 1
                                Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                                With ColG
                                    .Visible = True
                                    .FieldName = Col.ColumnName.ToString

                                    .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                    .Caption = Col.ColumnName.ToString

                                End With

                                .Columns.Add(ColG)

                                With .Columns(Col.ColumnName.ToString)

                                    .OptionsFilter.AllowAutoFilter = False
                                    .OptionsFilter.AllowFilter = False
                                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                    .DisplayFormat.FormatString = "{0:n5}"
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
                                .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n5}"

                        End Select

                    Next

                End If

                'If _colcount > 4 Then
                '    .BestFitColumns()
                'End If

            End With

            Me.ogc.DataSource = _dt.Copy
            'If _colcount > 4 Then
            '    ogvjobprod.BestFitColumns()
            'End If

            _colcount = 0


            'With Me.ogc
            '    .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            '    ogv.ExpandAllGroups()
            '    ogv.RefreshData()
            '    ogv.BestFitColumns()
            'End With
        Catch ex As Exception
            _Spls.Close()
        End Try
        _Spls.Close()


    End Sub

#End Region

#Region "General"
    Private Sub wEmployeeListing_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

        Me.FTPayYearTermFrom.Properties.Items.Clear()
        Me.FTPayYearTermTo.Properties.Items.Clear()
        ' Call InitGrid()
        Dim toYear As String = DateTime.Now.ToString("yyyy")
        Dim toMonth As String = DateTime.Now.ToString("MM")

        Dim PayYearTerm As String = ""


        PayYearTerm = toYear + "/" + toMonth
        Call bindPayYearTerm(PayYearTerm)

        Me.FTPayYearTermTo.SelectedIndex = 0
        Me.FTPayYearTermFrom.SelectedIndex = 24

        'Me.FTPayYear.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTPayYear FROM  THRMCfgPayHD WITH(NOLOCK) Order BY FTPayYear Desc", Conn.DB.DataBaseName.DB_HR, "")
    End Sub

    Private Function bindPayYearTerm(ByVal _PayYearTerm As String)

        Try
            Dim _Qry As String = ""
            Dim _dt As DataTable

            _Qry = " SELECT TOP 40   FTPayYear +'/'+ FTPayTerm AS FTPayYearTerm, FNHSysEmpTypeId, ROW_NUMBER() OVER ( order by FNHSysEmpTypeId) AS id "
            _Qry &= vbCrLf & " FROM [dbo].[THRMCfgPayDT] "
            _Qry &= vbCrLf & " WHERE FNHSysEmpTypeId = '1306010001' AND FTPayYear +'/'+ RIGHT('0' + CAST(FNMonth aS varchar(2)),2) <='" & _PayYearTerm & "'"
            _Qry &= vbCrLf & " ORDER BY FTPayYear DESC, FTPayTerm DESC  "


            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            Me.FTPayYearTermTo.Properties.Items.Clear()


            For Each Row As DataRow In _dt.Rows
                Me.FTPayYearTermFrom.Properties.Items.Add(Row("FTPayYearTerm"))
                Me.FTPayYearTermTo.Properties.Items.Add(Row("FTPayYearTerm"))
            Next


        Catch ex As Exception

        End Try

    End Function


    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)

        ' Me.FTPayYear.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTPayYear FROM  THRMCfgPayHD WITH(NOLOCK) Order BY FTPayYear Desc", Conn.DB.DataBaseName.DB_HR, "")
    End Sub

    Private Sub ocmload_Click(sender As System.Object, e As System.EventArgs) Handles ocmload.Click

        'If Me.FNHSysEmpTypeId.Text <> "" Then

        If Me.FTPayYearTermFrom.Text <> "" And Me.FTPayYearTermTo.Text <> "" Then

                Dim _From As Integer = Val(FTPayYearTermFrom.Text.Replace("/", ""))
                Dim _To As Integer = Val(FTPayYearTermTo.Text.Replace("/", ""))

                If _From <= _To Then

                    'If (Me.FNHSysDeptId.Text <> "" And Me.FNHSysDeptIdTo.Text <> "") Or (Me.FNHSysDivisonId.Text <> "" And Me.FNHSysDivisonIdTo.Text <> "") Or
                    '    (Me.FNHSysSectId.Text <> "" And Me.FNHSysSectIdTo.Text <> "") Or (Me.FNHSysUnitSectId.Text <> "" And Me.FNHSysUnitSectIdTo.Text <> "") Or
                    '    (Me.FNHSysEmpId.Text <> "" And Me.FNHSysEmpIdTo.Text <> "") Then


                    Me.LoadDataInfo()

                    'Else
                    '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysEmpId_lbl.Text)
                    'End If

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPayYearTermFrom_lbl.Text)

                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPayYearTermFrom_lbl.Text)
                FTPayYearTermFrom.Focus()
                FTPayYearTermFrom.SelectAll()

            End If
        'Else
        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysEmpTypeId_lbl.Text)
        'End If


    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click

    End Sub
End Class