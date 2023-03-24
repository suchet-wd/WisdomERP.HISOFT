Imports DevExpress.Data
Imports System.Text
Imports System.Data.OleDb
Imports DevExpress.Office.PInvoke.Win32
Imports System.IO

Public Class wSocialTrack_KM
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
        Dim sFieldCount As String = "" '"FTEmpCode"
        Dim sFieldSum As String = "" '"FCBaht|FCOt1_Baht|FCOt15_Baht|FCOt2_Baht|FCOt3_Baht|FCOt4_Baht|FNIncentiveAmt|FCNetBaht|FNPayLeaveVacationBaht|FNPayLeaveOtherBaht|FNTotalAdd|FNTotalAddOther|FNTotalExpense|FNTotalExpenseOther|FNTotalIncome|FNTotalRecalSSO|FNSocial|FNTax|FHolidayBaht|FNNetpayDiff|FNNetpay"

        Dim sFieldGrpCount As String = "" '"FTEmpCode"
        Dim sFieldGrpSum As String = "" '"FCBaht|FNTotalIncome|FNTotalRecalSSO|FNSocial|FNTax|FNNetpay"

        Dim sFieldCustomSum As String = "" '"FNWorkingDay|FNOt1|FNOt15|FNOt2|FNOt3|FNOt4"
        Dim sFieldCustomGrpSum As String = "" '"FNWorkingDay"

        'With ogv
        '    .ClearGrouping()
        '    .ClearDocument()
        '    ' .Columns("FTEmpTypeCode").Group()
        '    '.Columns("FTDeptCode").Group()
        '    '.Columns("FTDivisonCode").Group()
        '    ' .Columns("FTSectCode").Group()
        '    ' .Columns("FTUnitSectCode").Group()

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
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n3}"
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
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n3})")
        '        End If
        '    Next

        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        '    .OptionsView.ShowFooter = True
        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
        '    .OptionsView.ShowGroupPanel = True
        '    .ExpandAllGroups()
        '    .RefreshData()

        'End With
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

    Private Sub ogv_CustomDrawGroupRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs) Handles ogv.CustomDrawGroupRow

        Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo = e.Info
        Dim Handle As Integer = ogv.GetDataRowHandleByGroupRowHandle(e.RowHandle)

        'Select Case info.Column.FieldName.ToString.ToUpper
        '    Case "FNWorkingDay"

        Dim GrpDisplayText As String = ogv.GetGroupSummaryText(e.RowHandle)  'ogv.GetGroupRowValue(e.RowHandle, info.Column)
        Dim GrpDisplayTextReplace As String = Nothing
        Dim GrpDisplayTextReplaceNew As String = Nothing
        GrpDisplayTextReplace = GrpDisplayText.Split(")")(1)

        If GrpDisplayTextReplace <> "" Then
            If GrpDisplayTextReplace.Split("=").Length >= 2 Then
                Dim Title1 As String = GrpDisplayTextReplace.Split("=")(0)
                Dim Title2 As String = GrpDisplayTextReplace.Split("=")(1)

                If IsNumeric(Title2) = False Then
                    Title2 = "0"
                End If
                Dim _Sum As Integer = CDbl(Title2)
                Dim NetDisplay As String = ""
                If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                    NetDisplay = Format((_Sum \ 480), "00") & " วัน : " & Format(((_Sum Mod 480) \ 60), "00") & " ชั่วโมง : " & Format(((_Sum Mod 480) Mod 60), "00") & " นาที"
                Else
                    NetDisplay = Format((_Sum \ 480), "00") & " Day : " & Format(((_Sum Mod 480) \ 60), "00") & " Hour : " & Format(((_Sum Mod 480) Mod 60), "00") & " Minute"
                End If

                GrpDisplayTextReplaceNew = Title1 & "=" & NetDisplay
                GrpDisplayText = GrpDisplayText.Replace(GrpDisplayTextReplace, GrpDisplayTextReplaceNew)
            End If
        End If

        info.GroupText = info.Column.Caption + ":" + info.GroupValueText + ""
        info.GroupText += "" + GrpDisplayText + ""

        'End Select

    End Sub


    Private Sub Gridview_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogv.CustomSummaryCalculate
        If e.SummaryProcess = CustomSummaryProcess.Start Then
            InitStartValue()
        End If

        Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
            Case "FNWorkingDay"
                If e.SummaryProcess = CustomSummaryProcess.Calculate Then

                    If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                        If e.IsGroupSummary Then
                            Dim Seq As Integer = 1
                            For Each Str As String In e.FieldValue.ToString.Split(":")
                                Select Case Seq
                                    Case 1
                                        GrpSum = GrpSum + (Integer.Parse(Val(Str)) * 480)
                                    Case 2
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
                                        totalSum = totalSum + (Integer.Parse(Val(Str)) * 480)
                                    Case 2
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
                        GrpDisplay = Format((GrpSum \ 480), "00") & ":" & Format(((GrpSum Mod 480) \ 60), "00") & ":" & Format(((GrpSum Mod 480) Mod 60), "00")
                        e.TotalValue = GrpSum
                    End If

                    If e.IsTotalSummary Then
                        Dim NetDisplay As String = ""

                        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                            NetDisplay = Format((totalSum \ 480), "00") & " วัน : " & Format(((totalSum Mod 480) \ 60), "00") & " ชั่วโมง : " & Format(((totalSum Mod 480) Mod 60), "00") & " นาที"
                        Else
                            NetDisplay = Format((totalSum \ 480), "00") & " Day : " & Format(((totalSum Mod 480) \ 60), "00") & " Hour : " & Format(((totalSum Mod 480) Mod 60), "00") & " Minute"
                        End If

                        e.TotalValue = NetDisplay ' totalSum 'NetDisplay

                    End If
                End If
            Case "FNOt1", "FNOt15", "FNOt2", "FNOt3", "FNOt4"


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

#Region "Procedure"
    Private Sub LoadDataInfo()

        Dim _Qry As String = ""

        '_Qry = " SELECT   ROW_NUMBER ( ) over(  ORDER  BY M.FTEmpCodeRefer  asc ) AS FNRowNo ,"
        '_Qry &= vbCrLf & "  M.FTEmpCodeRefer, M.FTSocialNo, M.FTEmpIdNo, M.FTEmpSurnameTH, M.FTEmpNameTH, M.FTEmpSurnameEN, M.FTEmpNameEN  "
        '_Qry &= vbCrLf & "  , M.FDBirthDate"

        '_Qry &= vbCrLf & "  , M.FDDateStart  "

        '_Qry &= vbCrLf & "   ,( R.FNNetpay * Isnull( R.FNSocialExchangeRate,1) ) AS FNSalaryReal, R.FNNetpay , R.FNSocialBase , "
        '_Qry &= vbCrLf & "    R.FNSocial, R.FNTotalRecalSSO"

        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & " , S.FTUnitSectNameTH  AS FTUnitSectName "
        '    _Qry &= vbCrLf & "  ,X.FTNameTH As  FNEmpSex"
        '    _Qry &= vbCrLf & ", N.FTNationalityNameTH  as FTNationalityName"
        '    _Qry &= vbCrLf & ", P.FTPositNameTH AS FTPositName"
        '    _Qry &= vbCrLf & " , L.FTNameTH As FNEmpStatus  "
        'Else
        '    _Qry &= vbCrLf & " , S.FTUnitSectNameEN  AS FTUnitSectName "
        '    _Qry &= vbCrLf & "  ,X.FTNameEN As  FNEmpSex"
        '    _Qry &= vbCrLf & ", N.FTNationalityNameEN  as FTNationalityName"
        '    _Qry &= vbCrLf & ", P.FTPositNameEN AS FTPositName"
        '    _Qry &= vbCrLf & " , L.FTNameEN As FNEmpStatus  "
        'End If


        '_Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee As M With (NOLOCK) LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll As R With (NOLOCK) On M.FNHSysEmpID = R.FNHSysEmpID"
        '_Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As S With(NOLOCK) On M.FNHSysUnitSectId = S.FNHSysUnitSectId"
        '_Qry &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition As P With(NOLOCK) On M.FNHSysPositId = P.FNHSysPositId"
        '_Qry &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMNationality As N With(NOLOCK) On M.FNHSysNationalityId = N.FNHSysNationalityId"
        '_Qry &= vbCrLf & "    LEFT OUTER JOIN (Select FTListName, FNListIndex, FTNameTH, FTNameEN"
        '_Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData "
        '_Qry &= vbCrLf & "  WHERE  (FTListName = 'FNEmpStatus')) AS L ON M.FNEmpStatus = L.FNListIndex"
        '_Qry &= vbCrLf & "  LEFT OUTER JOIN  ("
        '_Qry &= vbCrLf & "  Select FTListName, FNListIndex, FTNameTH, FTNameEN"
        '_Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData "
        '_Qry &= vbCrLf & "  WHERE  (FTListName = 'FNEmpSex')) AS X ON M.FNEmpSex = X.FNListIndex"

        '_Qry &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS OrgDiv WITH (NOLOCK) ON M.FNHSysDivisonId = OrgDiv.FNHSysDivisonId LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS OrgSect WITH (NOLOCK) ON M.FNHSysSectId = OrgSect.FNHSysSectId  "



        '_Qry &= vbCrLf & "   Where  R.FTPayYear ='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "'  "
        '_Qry &= vbCrLf & "   AND  R.FTPayTerm ='" & HI.UL.ULF.rpQuoted(Me.FTPayTerm.Text) & "'  "



        Dim _dt As DataTable

        _Qry = " SELECT  TOP 1  FTStateCmpPayOnly "
        _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCmpSocialDiscountTaxConfig AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "

        Dim StateCmpPayOnly As Boolean = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0") = "1"



        _Qry = "   Select   ROW_NUMBER ( ) over(  ORDER  BY  FTEmpCodeRefer  asc ) AS FNRowNo , FTEmpCode  "
        _Qry &= vbCrLf & ",  FTEmpCodeRefer  , FTSocialNo  , FTEmpIdNo     ,max( FTEmpSurnameTH) AS FTEmpSurnameTH  , max(FTEmpNameTH) as FTEmpNameTH  "
        _Qry &= vbCrLf & "  , max( FTEmpSurnameEN) as FTEmpSurnameEN   , max( FTEmpNameEN  ) as FTEmpNameEN   "
        _Qry &= vbCrLf & "  , max(FDBirthDate )as FDBirthDate   , max(FDDateStart  ) as FDDateStart    ,sum(FNNetpay) AS FNSalaryReal "
        _Qry &= vbCrLf & "  , sum(FNNetpay) as FNNetpay  , sum(FNTotalRecalSSO)  as FNTotalRecalSSO  ,max( FTUnitSectName ) AS FTUnitSectName   ,max(FNEmpSex)  As  FNEmpSex "
        _Qry &= vbCrLf & " , max(FTNationalityName)   as FTNationalityName , max(FTPositName) AS FTPositName , max(FNEmpStatus)  As FNEmpStatus   "
        _Qry &= vbCrLf & "  , sum(FNTotalIncome ) as FNTotalIncome   "
        _Qry &= vbCrLf & "  , sum(FNTotalIncome )   as FNTotalIncomeReil  "
        _Qry &= vbCrLf & " , sum(FNAssumedWageRiel) as FNAssumedWageRiel "
        _Qry &= vbCrLf & " , sum(FNContributionOfInjuryRiel)  as FNContributionOfInjuryRiel "
        _Qry &= vbCrLf & "  , sum(FNContribution_of_Health_Insurance)  as FNContribution_of_Health_Insurance   "
        _Qry &= vbCrLf & "  , sum(FNEmployee_Contribution_Riel) as  FNEmployee_Contribution_Riel "
        _Qry &= vbCrLf & "  , sum(FNCompany_Contribution_Riel) as FNCompany_Contribution_Riel  "
        _Qry &= vbCrLf & "  , sum(FNTotalAll)  as FNTotalAll  "
        _Qry &= vbCrLf & "  , max(FTEmpName) AS FTEmpName "



        _Qry &= vbCrLf & " From ( SELECT    M.FTEmpCode , "
        _Qry &= vbCrLf & "   M.FTEmpCodeRefer  , M.FTSocialNo  , M.FTEmpIdNo   "
        _Qry &= vbCrLf & "   ,max( M.FTEmpSurnameTH) AS FTEmpSurnameTH  , max(M.FTEmpNameTH) as FTEmpNameTH  , max( M.FTEmpSurnameEN) as FTEmpSurnameEN   , max( M.FTEmpNameEN  ) as FTEmpNameEN   "
        _Qry &= vbCrLf & "    , max(M.FDBirthDate )as FDBirthDate   , max(M.FDDateStart  ) as FDDateStart    ,( sum(R.FNNetpay)  * Isnull( R.FNSocialExchangeRate,1) ) AS FNSalaryReal "

        _Qry &= vbCrLf & "    , sum(R.FNNetpay) as FNNetpay  , sum(R.FNTotalRecalSSO)  as FNTotalRecalSSO  ,max( S.FTUnitSectNameTH ) AS FTUnitSectName   ,max(X.FTNameTH)  As  FNEmpSex "
        _Qry &= vbCrLf & "  ,  max(N.FTNationalityNameTH)   as FTNationalityName , max(P.FTPositNameTH) AS FTPositName , max(L.FTNameTH)  As FNEmpStatus   "
        _Qry &= vbCrLf & "   ,sum(R.FNTotalIncome ) as FNTotalIncome    "
        _Qry &= vbCrLf & "   ,convert(numeric(18,0) , sum(R.FNTotalIncome ) * R.FNSocialExchangeRate )  as FNTotalIncomeReil  "
        _Qry &= vbCrLf & "   ,(select top 1  FNSocialBase   From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgSocailKMRate WITH(NOLOCK) WHERE  FNSocialStartRange   <= convert(numeric(18,0) , sum(R.FNTotalIncome ) * R.FNSocialExchangeRate )  and FNSocialEndRange >=convert(numeric(18,0) , sum(R.FNTotalIncome ) * R.FNSocialExchangeRate )  ) as FNAssumedWageRiel "
        _Qry &= vbCrLf & "   , (select top 1  FNSocialAmt   From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgSocailKMRate WITH(NOLOCK) WHERE  FNSocialStartRange   <= convert(numeric(18,0) , sum(R.FNTotalIncome ) * R.FNSocialExchangeRate )  and FNSocialEndRange >=convert(numeric(18,0) , sum(R.FNTotalIncome ) * R.FNSocialExchangeRate )  ) as FNContributionOfInjuryRiel "
        _Qry &= vbCrLf & "  , ((Select top 1   FNSocialRate From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCmpSocialDiscountTaxConfig  WITH(NOLOCK)  where fnhsyscmpId =  " & Integer.Parse(HI.ST.SysInfo.CmpID) & "   ) *   "
        _Qry &= vbCrLf & "   (select top 1  FNSocialBase   From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgSocailKMRate WITH(NOLOCK) WHERE  FNSocialStartRange   <= convert(numeric(18,0) , sum(R.FNTotalIncome ) * R.FNSocialExchangeRate )  and FNSocialEndRange >=convert(numeric(18,0) , sum(R.FNTotalIncome ) * R.FNSocialExchangeRate )  )  "
        _Qry &= vbCrLf & "      * 2 )  / 100    as FNContribution_of_Health_Insurance   "
        If StateCmpPayOnly Then
            _Qry &= vbCrLf & " , 0 as  FNEmployee_Contribution_Riel "
        Else
            _Qry &= vbCrLf & "   , ( (Select top 1   FNSocialRate From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCmpSocialDiscountTaxConfig  WITH(NOLOCK)  where fnhsyscmpId =  " & Integer.Parse(HI.ST.SysInfo.CmpID) & "   ) *    "
            _Qry &= vbCrLf & "  ( ((select top 1  FNSocialBase   From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgSocailKMRate WITH(NOLOCK) WHERE  FNSocialStartRange   <= convert(numeric(18,0) , sum(R.FNTotalIncome ) * R.FNSocialExchangeRate )  and FNSocialEndRange >=convert(numeric(18,0) , sum(R.FNTotalIncome ) * R.FNSocialExchangeRate )  )  "
            _Qry &= vbCrLf & "    ) ) / 100 ) as FNEmployee_Contribution_Riel   "
        End If


        _Qry &= vbCrLf & "  ,  (select top 1  FNSocialAmt   From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgSocailKMRate WITH(NOLOCK) WHERE  FNSocialStartRange   <= convert(numeric(18,0) , sum(R.FNTotalIncome ) * R.FNSocialExchangeRate )  and FNSocialEndRange >=convert(numeric(18,0) , sum(R.FNTotalIncome ) * R.FNSocialExchangeRate )  ) +  "
        _Qry &= vbCrLf & "    (((Select top 1   FNSocialRate From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCmpSocialDiscountTaxConfig  WITH(NOLOCK)  where fnhsyscmpId = " & Integer.Parse(HI.ST.SysInfo.CmpID) & "  ) *  "
        _Qry &= vbCrLf & "   (select top 1  FNSocialBase   From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgSocailKMRate WITH(NOLOCK) WHERE  FNSocialStartRange   <= convert(numeric(18,0) , sum(R.FNTotalIncome ) * R.FNSocialExchangeRate )  and FNSocialEndRange >=convert(numeric(18,0) , sum(R.FNTotalIncome ) * R.FNSocialExchangeRate )  )  "
        _Qry &= vbCrLf & "   ) / 100 )  as FNCompany_Contribution_Riel   "


        If StateCmpPayOnly Then
            _Qry &= vbCrLf & " , ( 0 + "
        Else
            _Qry &= vbCrLf & "    , ( ( (Select top 1   FNSocialRate From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCmpSocialDiscountTaxConfig  WITH(NOLOCK)  where fnhsyscmpId = 1311090006  ) *    "
            _Qry &= vbCrLf & " ( ((select top 1  FNSocialBase   From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgSocailKMRate WITH(NOLOCK) WHERE  FNSocialStartRange   <= convert(numeric(18,0) , sum(R.FNTotalIncome ) * R.FNSocialExchangeRate )  and FNSocialEndRange >=convert(numeric(18,0) , sum(R.FNTotalIncome ) * R.FNSocialExchangeRate )  )  "
            _Qry &= vbCrLf & "   ) ) / 100 )   +  "
        End If



        _Qry &= vbCrLf & "  (select top 1  FNSocialAmt   From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgSocailKMRate WITH(NOLOCK) WHERE  FNSocialStartRange   <= convert(numeric(18,0) , sum(R.FNTotalIncome ) * R.FNSocialExchangeRate )  and FNSocialEndRange >=convert(numeric(18,0) , sum(R.FNTotalIncome ) * R.FNSocialExchangeRate )  ) + "
        _Qry &= vbCrLf & " (((Select top 1   FNSocialRate From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCmpSocialDiscountTaxConfig  WITH(NOLOCK)  where fnhsyscmpId =  " & Integer.Parse(HI.ST.SysInfo.CmpID) & "   ) *   "
        _Qry &= vbCrLf & "  (select top 1  FNSocialBase   From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgSocailKMRate WITH(NOLOCK) WHERE  FNSocialStartRange   <= convert(numeric(18,0) , sum(R.FNTotalIncome ) * R.FNSocialExchangeRate )  and FNSocialEndRange >=convert(numeric(18,0) , sum(R.FNTotalIncome ) * R.FNSocialExchangeRate )  )  "
        _Qry &= vbCrLf & "  ) / 100 )      )  as FNTotalAll  "


        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & " ,  max(M.FTEmpNameTH) + ' ' + max( M.FTEmpSurnameTH)  AS FTEmpName "

        Else
            _Qry &= vbCrLf & " ,  max(M.FTEmpNameEN) + ' ' + max( M.FTEmpSurnameEN)  AS FTEmpName "

        End If


        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee As M With (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll As R With (NOLOCK) On M.FNHSysEmpID = R.FNHSysEmpID"
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As S With(NOLOCK) On M.FNHSysUnitSectId = S.FNHSysUnitSectId"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition As P With(NOLOCK) On M.FNHSysPositId = P.FNHSysPositId"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMNationality As N With(NOLOCK) On M.FNHSysNationalityId = N.FNHSysNationalityId"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN (Select FTListName, FNListIndex, FTNameTH, FTNameEN"
        _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData "
        _Qry &= vbCrLf & "  WHERE  (FTListName = 'FNEmpStatus')) AS L ON M.FNEmpStatus = L.FNListIndex"
        _Qry &= vbCrLf & "  LEFT OUTER JOIN  ("
        _Qry &= vbCrLf & "  Select FTListName, FNListIndex, FTNameTH, FTNameEN"
        _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData "
        _Qry &= vbCrLf & "  WHERE  (FTListName = 'FNEmpSex')) AS X ON M.FNEmpSex = X.FNListIndex"

        _Qry &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS OrgDiv WITH (NOLOCK) ON M.FNHSysDivisonId = OrgDiv.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS OrgSect WITH (NOLOCK) ON M.FNHSysSectId = OrgSect.FNHSysSectId  "



        _Qry &= vbCrLf & "   Where  R.FTPayYear ='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "'  "
        '_Qry &= vbCrLf & "   AND  R.FTPayTerm ='" & HI.UL.ULF.rpQuoted(Me.FTPayTerm.Text) & "'  "
        _Qry &= vbCrLf & "  and  R.FTPayTerm  in   ( Select  FTPayTerm From THRMCfgPayDT WITH(NOLOCK) where FNMonth = " & (FNMonth.SelectedIndex + 1) & "  and FTPayYear = '" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "'  )" 'and FNHSysEmpTypeId = " & Integer.Parse(Me.FNHSysEmpTypeId.Properties.Tag.ToString) & "


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
            _Qry &= vbCrLf & " AND  OrgDiv.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
        End If

        If Me.FNHSysDivisonIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  OrgDiv.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
        End If

        '------Criteria By Sect
        If Me.FNHSysSectId.Text <> "" Then
            _Qry &= vbCrLf & " AND  OrgSect.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
        End If

        If Me.FNHSysSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  OrgSect.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
        End If

        '------Criteria Unit Sect
        If Me.FNHSysUnitSectId.Text <> "" Then
            _Qry &= vbCrLf & " AND   S.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
        End If

        If Me.FNHSysUnitSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND   S.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
        End If

        _Qry = _Qry & HI.ST.Security.PermissionFilterEmployeeSalary()

        _Qry &= vbCrLf & "   group by M.FTEmpCodeRefer , M.FTSocialNo  , M.FTEmpIdNo   ,  M.FTEmpCode , R.FNSocialExchangeRate   "
        _Qry &= vbCrLf & " ) AS M "
        _Qry &= vbCrLf & "   group by FTEmpCodeRefer , FTSocialNo  , FTEmpIdNo   ,  FTEmpCode     "

        _Qry &= vbCrLf & "ORDER BY   FNRowNo  asc "


        With Me.ogc
            .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            ogv.ExpandAllGroups()
            ogv.RefreshData()
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
        'If FNHSysEmpTypeId.Text = "" Then
        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysEmpTypeId_lbl.Text)
        '    FNHSysEmpTypeId.Focus()
        '    FNHSysEmpTypeId.SelectAll()
        '    Exit Sub
        'End If

        If Me.FTPayYear.Text <> "" And FTPayYear.Text.Length = 4 Then
            If Me.FNMonth.Text <> "" Then
                Me.LoadDataInfo()
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMonth_lbl.Text)
                FNMonth.Focus()
                FNMonth.SelectAll()
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

    Private Function Exporttoaccess() As Boolean
        Try
            Dim _Cmd As String = ""



            Dim dt As New DataTable

            dt = DirectCast(Me.ogc.DataSource, DataTable)




            Dim connString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\NOH-NB\Desktop\test2.accdb;Persist Security Info=False;"

            Dim accConnection As New OleDb.OleDbConnection(connString)
            Dim selectCommand As String = "SELECT Enter_ID FROM TBL_Beneficiary "
            Dim accDataAdapter As New OleDb.OleDbDataAdapter(selectCommand, accConnection)

            Dim accCommandBuilder As New OleDb.OleDbCommandBuilder()
            accDataAdapter.InsertCommand = accCommandBuilder.GetInsertCommand()
            accDataAdapter.UpdateCommand = accCommandBuilder.GetUpdateCommand()

            Dim accDataTable As DataTable = dt.Copy()


            accDataAdapter.Update(accDataTable)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private _FileName As String = ""
    Private Sub ocmExportAccess_Click(sender As Object, e As EventArgs) Handles ocmExportAccess.Click
        ' Exporttoaccess()
        Dim dt As DataTable = DirectCast(Me.ogc.DataSource, DataTable)
        If dt.Rows.Count <= 0 Then
            HI.MG.ShowMsg.mInfo("Pls Load Data!! ", 1609091603, Me.Text)
            Exit Sub
        End If
        Dim Op As New System.Windows.Forms.SaveFileDialog
        Op.Filter = "Access 2007-2015 (*.accdb)|*accdb"
        Op.ShowDialog()
        Try
            If Op.FileName <> "" Then
                _FileName = Op.FileName.ToString
                'ExportExcel()
                Dim _Spls As New HI.TL.SplashScreen("Prepre Data For Calculate.. Please Wait ")
                Call wRecords(_Spls)
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub wRecords(_Spls As HI.TL.SplashScreen)
        Try

            Dim objConn As OleDbConnection
            Dim objCmd As OleDbCommand
            Dim strConnString As String
            Dim strSQL As String
            Dim _Path As String = System.Windows.Forms.Application.StartupPath.ToString
            Dim TmpFile As String = _Path & "\Reports\TmpSocial_KM.accdb"
            strConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & TmpFile & ";Persist Security Info=False;"
            objConn = New OleDbConnection(strConnString)
            objConn.Open()


            Dim dt As DataTable = DirectCast(Me.ogc.DataSource, DataTable)
            strSQL = "Delete From  TBL_Beneficiary"
            objCmd = New OleDbCommand(strSQL, objConn)
            objCmd.ExecuteNonQuery()

            For Each R As DataRow In dt.Rows
                strSQL = "INSERT INTO TBL_Beneficiary (Enter_ID , NSSF_ID , ID_National , Ben_FName_kh , Ben_LName_kh , Ben_FName_Eng , Ben_LName_Eng , Sex ,Date_of_Birth "
                strSQL &= " ,Nationality ,Hired_Date , Ben_Group, Ben_Position, Ben_Status , Ben_Wage , Ben_WageInDollar , Ben_Assum , Ben_Contribution) "
                strSQL &= vbCrLf & "VALUES ('" & R!FTEmpCodeRefer.ToString & "'"
                strSQL &= vbCrLf & ",'" & R!FTSocialNo.ToString & "'"
                strSQL &= vbCrLf & ",'" & R!FTEmpIdNo.ToString & "'"
                strSQL &= vbCrLf & ",'" & R!FTEmpSurnameTH.ToString & "'"
                strSQL &= vbCrLf & ",'" & R!FTEmpNameTH.ToString & "'"
                strSQL &= vbCrLf & ",'" & R!FTEmpSurnameEN.ToString & "'"
                strSQL &= vbCrLf & ",'" & R!FTEmpNameEN.ToString & "'"
                strSQL &= vbCrLf & ",'" & R!FNEmpSex.ToString & "'"
                strSQL &= vbCrLf & ",'" & R!FDBirthDate.ToString & "'"
                strSQL &= vbCrLf & ",'" & R!FTNationalityName.ToString & "'"
                strSQL &= vbCrLf & ",'" & R!FDDateStart.ToString & "'"
                strSQL &= vbCrLf & ",'" & R!FTUnitSectName.ToString & "'"
                strSQL &= vbCrLf & ",'" & R!FTPositName.ToString & "'"
                strSQL &= vbCrLf & ",'" & R!FNEmpStatus.ToString & "'"
                strSQL &= vbCrLf & "," & R!FNSalaryReal.ToString & ""
                strSQL &= vbCrLf & "," & R!FNNetpay.ToString & ""
                strSQL &= vbCrLf & "," & R!FNSocialBase.ToString & ""
                strSQL &= vbCrLf & "," & R!FNSocial.ToString & ""

                strSQL &= vbCrLf & " )"

                objCmd = New OleDbCommand(strSQL, objConn)
                objCmd.ExecuteNonQuery()
            Next
            If Microsoft.VisualBasic.Right(_FileName, 6).ToString <> ".accdb" Then _FileName = _FileName & ".accdb"
            objConn.Close()
            objConn = Nothing
            File.Copy(TmpFile, _FileName)
            _Spls.Close()
            HI.MG.ShowMsg.mInfo("Save file Social Success.", 1609091540, Me.Text)
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub

End Class