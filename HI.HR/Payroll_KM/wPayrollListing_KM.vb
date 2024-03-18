Imports DevExpress.Data
Imports System.Text

Public Class wPayrollListing_KM
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
        Dim sFieldCount As String = "FTEmpCode"
        Dim sFieldSum As String = "FCBaht|FCOt1_Baht|FCOt15_Baht|FCOt2_Baht|FCOt3_Baht|FCOt4_Baht|FNIncentiveAmt|FCNetBaht|FNPayLeaveVacationBaht|FNPayLeaveOtherBaht|FNTotalAdd|FNTotalAddOther|FNTotalExpense|FNTotalExpenseOther|FNTotalIncome|FNTotalRecalSSO|FNSocial|FNTax|FHolidayBaht|FNNetpayDiff|FNNetpay|FTUnionDuesAmt|FNWorkShift|FNSpecialSkill"
        sFieldSum &= "|FNSeniorityAmt44|FNSeniorityAmt45|FNSeniorityAmt46|FNSeniorityAmt47"
        Dim sFieldGrpCount As String = "FTEmpCode"
        Dim sFieldGrpSum As String = "FCBaht|FNTotalIncome|FNTotalRecalSSO|FNSocial|FNTax|FNNetpay"

        Dim sFieldCustomSum As String = "FNWorkingDay|FNOt1|FNOt15|FNOt2|FNOt3|FNOt4"
        Dim sFieldCustomGrpSum As String = "FNWorkingDay"

        With ogv
            .ClearGrouping()
            .ClearDocument()
            ' .Columns("FTEmpTypeCode").Group()
            '.Columns("FTDeptCode").Group()
            '.Columns("FTDivisonCode").Group()
            ' .Columns("FTSectCode").Group()
            ' .Columns("FTUnitSectCode").Group()

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
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n3}"
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
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n3})")
                End If
            Next

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
        Dim _Spls As New HI.TL.SplashScreen("Load Data Please Waiting......")
        Dim _Qry As String = ""
        Try
            _Qry = " SELECT       M.FNHSysEmpID, M.FTEmpCode"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & "  ,P1.FTPreNameNameTH + ' ' + M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"
                _Qry &= vbCrLf & "  ,ES.FTNameTH  AS FTEmpStatusName "
                _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameTH  AS FTEmpTypeName "
                _Qry &= vbCrLf & "  ,Dept.FTDeptDescTH  AS FTDeptName "
                _Qry &= vbCrLf & "  ,OrgDiv.FTDivisonNameTH  AS FTDivisonName "
                _Qry &= vbCrLf & "  ,OrgSect.FTSectNameTH  AS FTSectName "
                _Qry &= vbCrLf & "  ,OrgUnitSect.FTUnitSectNameTH  AS FTUnitSectName "
                _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameTH  AS FTPositName "
                _Qry &= vbCrLf & "  , L.FTNameTH AS FTEmpTypeGroup "
            Else
                _Qry &= vbCrLf & "  ,P1.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpName"
                _Qry &= vbCrLf & "  ,ES.FTNameEN  AS FTEmpStatusName "
                _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameEN  AS FTEmpTypeName "
                _Qry &= vbCrLf & "  ,Dept.FTDeptDescEN  AS FTDeptName "
                _Qry &= vbCrLf & "  ,OrgDiv.FTDivisonNameEN  AS FTDivisonName "
                _Qry &= vbCrLf & "  ,OrgSect.FTSectNameEN  AS FTSectName "
                _Qry &= vbCrLf & "  ,OrgUnitSect.FTUnitSectNameEN  AS FTUnitSectName "
                _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameEN  AS FTPositName "
                _Qry &= vbCrLf & "  ,L.FTNameEN AS FTEmpTypeGroup "

            End If

            _Qry &= vbCrLf & "  ,ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
            _Qry &= vbCrLf & "  ,ISNULL(Dept.FTDeptCode,'') AS FTDeptCode, ISNULL(OrgDiv.FTDivisonCode,'') AS FTDivisonCode"
            _Qry &= vbCrLf & "  ,ISNULL(OrgSect.FTSectCode,'') AS FTSectCode,ISNULL(OrgUnitSect.FTUnitSectCode,'') AS FTUnitSectCode, OrgPosit.FTPositCode, M.FNEmpStatus"
            _Qry &= vbCrLf & "  ,P.FTPayYear, P.FTPayTerm, P.FNHSysEmpID, P.FTEmpIdNo"
            _Qry &= vbCrLf & "  ,P.FNHoliday, P.FNSalary, P.FNWorkingHour"
            _Qry &= vbCrLf & "  ,Replace(Convert(varchar(30),P.FNOt1),'.',':') AS  FNOt1"
            _Qry &= vbCrLf & "  , Replace(Convert(varchar(30),P.FNOt15),'.',':') AS FNOt15"
            _Qry &= vbCrLf & "  , Replace(Convert(varchar(30),P.FNOt2),'.',':') AS FNOt2"
            _Qry &= vbCrLf & "  , Replace(Convert(varchar(30),P.FNOt3),'.',':') AS FNOt3"
            _Qry &= vbCrLf & "  ,P.FNOt4, P.FNTotalLeavePay, P.FCBaht, P.FCOt1_Baht, P.FCOt15_Baht, P.FCOt2_Baht"
            _Qry &= vbCrLf & "  ,P.FCOt3_Baht, P.FCOt4_Baht, P.FCNetBaht, P.FNPayLeaveVacationBaht,P.FNPayLeaveOtherBaht"
            _Qry &= vbCrLf & "  ,P.FNTotalRecalSSO, P.FNTotalRecalTAX, P.FNTotalAdd, P.FNTotalAddOther, P.FNTotalExpense"
            _Qry &= vbCrLf & "  ,P.FNTotalExpenseOther, P.FNTotalIncome, P.FNSocial, P.FNTax, P.FHolidayBaht"
            _Qry &= vbCrLf & "  ,P.FNNetpay, P.FNPayLeaveSSo"
            _Qry &= vbCrLf & "  ,ISNULL(VP.[011] ,0) AS FNIncentiveAmt"
            _Qry &= vbCrLf & " ,  (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(P.FNTotalWKNMin / 480.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((P.FNTotalWKNMin % 480.00) / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((P.FNTotalWKNMin % 480.00) % 60.00))),2))  AS FNWorkingDay"

            _Qry &= vbCrLf & "  , P.FNAttandanceAmt, P.FNHealtCareAmt "
            _Qry &= vbCrLf & " ,P.FNTransportAmt, P.FNChildCareAmt, P.FNOTMealAmt, P.FNSocialBase, P.FNWorkAgeSalary, P.FNOTMealAmtUS,P.FNNetpayDiff "


            If ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & ", UNI.FTNameTH AS FTUnion  "
            Else
                _Qry &= vbCrLf & ", UNI.FTNameEN AS FTUnion  "
            End If
            _Qry &= vbCrLf & " , VP.[108] as FTUnionDuesAmt  "


            _Qry &= vbCrLf & " , VP.[044] AS FNSeniorityAmt44, VP.[045] as FNSeniorityAmt45, VP.[046] as  FNSeniorityAmt46, VP.[047] as FNSeniorityAmt47"

            _Qry &= vbCrLf & "  , VP.[039] AS FNBonus "

            _Qry &= vbCrLf & "  , VP.[017] AS FNAllowance "
            _Qry &= vbCrLf & "  , VP.[048] AS FNAllowance_NewYear "
            _Qry &= vbCrLf & "  , VP.[050] AS FNAllowance_Trip "


            _Qry &= vbCrLf & "  , VP.[001] AS FNWorkShift "
            _Qry &= vbCrLf & "  , VP.[013] AS FNSpecialSkill "



            _Qry &= vbCrLf & "  , P.[FNVacationRetMin] AS FNVacationRetMin "
            _Qry &= vbCrLf & "  , P.[FNReturnTax] AS FNReturnTax "
            _Qry &= vbCrLf & "  , P.[FNParturitionLeaveBaht] AS FNParturitionLeaveBaht "
            _Qry &= vbCrLf & "  , P.[FNVacationRetAmt] AS FNVacationRetAmt "

            _Qry &= vbCrLf & "  , P.[FNTotalRecalPensionScheme] AS FNTotalRecalPensionScheme "
            _Qry &= vbCrLf & "  , P.[FNPensionScheme] AS FNPensionScheme "
            _Qry &= vbCrLf & "  , P.[FNPensionSchemeCmp] AS FNPensionSchemeCmp "

            _Qry &= vbCrLf & "  , P.[FNParturitionLeaveUS1] AS FNParturitionLeaveUS1 "
            _Qry &= vbCrLf & "  , P.[FNParturitionLeaveUS2] AS FNParturitionLeaveUS2 "
            _Qry &= vbCrLf & "  , P.[FNParturitionLeaveUS3] AS FNParturitionLeaveUS3 "
            _Qry &= vbCrLf & "  , P.[FNPensionSchemeAdvance2] AS FNPensionSchemeAdvance2 "
            _Qry &= vbCrLf & "  , P.[FNPensionSchemeAdvance3] AS FNPensionSchemeAdvance3 "

            _Qry &= vbCrLf & " ,CASE WHEN ISDATE(M.FDDateStart) =1 THEN CONVERT(varchar(10),Convert(datetime,M.FDDateStart),103) ELSE '' END AS FDDateStart "
            _Qry &= vbCrLf & " ,CASE WHEN ISDATE(M.FDBirthDate) =1 THEN CONVERT(varchar(10),Convert(datetime,M.FDBirthDate),103) ELSE '' END AS FDBirthDate "

            _Qry &= vbCrLf & " , P.FTEmpIdNo "
            _Qry &= vbCrLf & " , M.FTSocialNo  "


            _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS P WITH (NOLOCK) ON M.FNHSysEmpID=P.FNHSysEmpID INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P1 WITH (NOLOCK) ON M.FNHSysPreNameId = P1.FNHSysPreNameId  INNER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON P.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON P.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS OrgDiv WITH (NOLOCK) ON P.FNHSysDivisonId = OrgDiv.FNHSysDivisonId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS OrgSect WITH (NOLOCK) ON P.FNHSysSectId = OrgSect.FNHSysSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS OrgUnitSect WITH (NOLOCK) ON P.FNHSysUnitSectId = OrgUnitSect.FNHSysUnitSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS OrgPosit WITH (NOLOCK) ON P.FNHSysPositId = OrgPosit.FNHSysPositId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MEmpStatus AS ES ON M.FNEmpStatus = ES.FNListIndex"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.V_PayrollFin AS VP ON M.FNHSysEmpID=VP.FNHSysEmpID and P.FTPayYear = VP.FTPayYear and  P.FTPayTerm  = VP.FTPayTerm "

            _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].[dbo].[HSysListData] WHERE FTListName='FNEmpTypeGroup' AND ET.FNEmpTypeGroup=FNListIndex ) L "

            _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].[dbo].[HSysListData] WHERE FTListName='FNUnion' AND P.FNUnion=FNListIndex ) UNI "


            _Qry &= vbCrLf & "  WHERE        (M.FTEmpCode <> '')"
            _Qry &= vbCrLf & "   AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "

            _Qry &= vbCrLf & "   AND  P.FTPayYear ='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "'  "
            _Qry &= vbCrLf & "   AND  P.FTPayTerm ='" & HI.UL.ULF.rpQuoted(Me.FTPayTerm.Text) & "'  "


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
                _Qry &= vbCrLf & " AND   OrgUnitSect.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
            End If

            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND   OrgUnitSect.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
            End If

            _Qry = _Qry & HI.ST.Security.PermissionFilterEmployeeSalary()



            With Me.ogc
                .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
                ogv.ExpandAllGroups()
                ogv.RefreshData()
                ogv.BestFitColumns()
            End With
        Catch ex As Exception
            _Spls.Close()
        End Try
        _Spls.Close()


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
End Class