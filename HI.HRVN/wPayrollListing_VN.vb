Imports DevExpress.Data
Imports System.Text

Public Class wPayrollListing_VN
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Call LoadGrid()
        Call InitGrid()
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
        Dim sFieldSum As String = "FCBaht|FCOt1_Baht|FCOt15_Baht|FCOt2_Baht|FCOt3_Baht|FCOt4_Baht|FNIncentiveAmt|FCNetBaht|FNPayLeaveVacationBaht|FNPayLeaveOtherBaht|FNTotalAdd|FNTotalAddOther|FNTotalExpense|FNTotalExpenseOther|FNTotalIncome|FNTotalRecalSSO|FNSocial|FNTax|FHolidayBaht|FNNetpay"

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
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
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
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
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

        Dim _Qry As String = ""

        _Qry = "  SELECT       M.FNHSysEmpID, M.FTEmpCode"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  ,P1.FTPreNameNameTH + ' ' + M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"
            _Qry &= vbCrLf & "  ,ES.FTNameTH  AS FTEmpStatusName "
            _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameTH  AS FTEmpTypeName "
            _Qry &= vbCrLf & "  ,Dept.FTDeptDescTH  AS FTDeptName "
            _Qry &= vbCrLf & "  ,OrgDiv.FTDivisonNameTH  AS FTDivisonName "
            _Qry &= vbCrLf & "  ,OrgSect.FTSectNameTH  AS FTSectName "
            _Qry &= vbCrLf & "  ,OrgUnitSect.FTUnitSectNameTH  AS FTUnitSectName "
            _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameTH  AS FTPositName "
        Else
            _Qry &= vbCrLf & "  ,P1.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpName"
            _Qry &= vbCrLf & "  ,ES.FTNameEN  AS FTEmpStatusName "
            _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameEN  AS FTEmpTypeName "
            _Qry &= vbCrLf & "  ,Dept.FTDeptDescEN  AS FTDeptName "
            _Qry &= vbCrLf & "  ,OrgDiv.FTDivisonNameEN  AS FTDivisonName "
            _Qry &= vbCrLf & "  ,OrgSect.FTSectNameEN  AS FTSectName "
            _Qry &= vbCrLf & "  ,OrgUnitSect.FTUnitSectNameEN  AS FTUnitSectName "
            _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameEN  AS FTPositName "
        End If

        _Qry &= vbCrLf & "  ,ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
        _Qry &= vbCrLf & "  ,ISNULL(Dept.FTDeptCode,'') AS FTDeptCode, ISNULL(OrgDiv.FTDivisonCode,'') AS FTDivisonCode"
        _Qry &= vbCrLf & "  ,ISNULL(OrgSect.FTSectCode,'') AS FTSectCode,ISNULL(OrgUnitSect.FTUnitSectCode,'') AS FTUnitSectCode, OrgPosit.FTPositCode, M.FNEmpStatus"
        _Qry &= vbCrLf & "  ,P.FTPayYear, P.FTPayTerm, P.FNHSysEmpID, P.FTEmpIdNo"
        _Qry &= vbCrLf & "  ,P.FNHoliday, P.FNSalary, P.FNWorkingHour"
        _Qry &= vbCrLf & "  ,Replace(Convert(varchar(30),   FLOOR(CONVERT(NUMERIC(18,2),(isnull(P.FNPayRestOTMin,0) + P.FNOt1Min) / 60) )    + CONVERT(NUMERIC(18,2),(( isnull(P.FNPayRestOTMin,0) + P.FNOt1Min )  % 60) / 100.00))  ,'.',':') AS  FNOt1"

        '_Qry &= vbCrLf & "  ,Replace(Convert(varchar(30),P.FNOt1   ),'.',':') AS  FNOt1"

        _Qry &= vbCrLf & "  , Replace(Convert(varchar(30),P.FNOt15),'.',':') AS FNOt15"
        _Qry &= vbCrLf & "  , Replace(Convert(varchar(30),P.FNOt2),'.',':') AS FNOt2"
        _Qry &= vbCrLf & "  , Replace(Convert(varchar(30),P.FNOt3),'.',':') AS FNOt3"
        _Qry &= vbCrLf & "  ,P.FNOt4, P.FNTotalLeavePay, P.FCBaht, P.FCOt1_Baht + P.FNPayRestOTBaht as FCOt1_Baht , P.FCOt15_Baht, P.FCOt2_Baht"
        _Qry &= vbCrLf & "  ,P.FCOt3_Baht, P.FCOt4_Baht, P.FCNetBaht, P.FNPayLeaveVacationBaht,P.FNPayLeaveOtherBaht"
        _Qry &= vbCrLf & "  ,P.FNTotalRecalSSO, P.FNTotalRecalTAX, P.FNTotalAdd, P.FNTotalAddOther, P.FNTotalExpense"
        _Qry &= vbCrLf & "  ,P.FNTotalExpenseOther, P.FNTotalIncome, P.FNSocial, P.FNTax, P.FHolidayBaht"
        _Qry &= vbCrLf & "  ,P.FNNetpay, P.FNPayLeaveSSo"
        _Qry &= vbCrLf & "  ,ISNULL(P.FNIncentiveAmt,0) AS FNIncentiveAmt"
        _Qry &= vbCrLf & " ,  (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(P.FNTotalWKNMin / 480.00))),2)"
        _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((P.FNTotalWKNMin % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((P.FNTotalWKNMin % 480.00) % 60.00))),2))  AS FNWorkingDay"

        _Qry &= vbCrLf & "  , P.FNAttandanceAmt, P.FNHealtCareAmt "
        _Qry &= vbCrLf & " ,P.FNTransportAmt, P.FNChildCareAmt, P.FNOTMealAmt, P.FNSocialBase, P.FNWorkAgeSalary, P.FNOTMealAmtUS "

        'new 
        _Qry &= vbCrLf & ",P.FNSkillBaht, P.FNHarmfulBaht, 0 as FNPayRestOTBaht, 0 as FNPayRestOTMin"
        _Qry &= vbCrLf & ", P.FNSocialInsuranceEmployee, P.FNSocialInsuranceEmployer, P.FNHealthInsuranceEmployee"
        _Qry &= vbCrLf & ",P.FNHealthInsuranceEmployer, P.FNUnemploymentInsuranceEmployee, P.FNUnemploymentInsuranceEmployer, P.FNUnionInsuranceEmployee, P.FNUnionInsuranceEmployer"

        _Qry &= vbCrLf & ", T.[001], T.[002], T.[003], T.[004], T.[005], T.[006], T.[007], T.[008], T.[009], T.[010], T.[011], T.[012], T.[013], T.[014], T.[015], T.[016], T.[017], T.[018], T.[019], T.[023], T.[024], T.[101], T.[102], "
        _Qry &= vbCrLf & "  T.[103], T.[104], T.[105], T.[106], T.[107], T.[108], T.[109], T.[110],T.[025], T.[027]"

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
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.V_PayrollFin AS T ON M.FNHSysEmpID = T.FNHSysEmpID AND  P.FTPayYear = T.FTPayYear"
        _Qry &= vbCrLf & " AND P.FTPayTerm  = T.FTPayTerm"
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



        Dim _oDt As System.Data.DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        '_Qry = "Select FTFormName, FTObjectName"
        'Select Case HI.ST.Lang.Language
        '    Case ST.Lang.eLang.TH
        '        _Qry &= vbCrLf & ", FTLangTH AS FTLang"
        '    Case ST.Lang.eLang.EN
        '        _Qry &= vbCrLf & ", FTLangEN AS FTLang"
        '    Case ST.Lang.eLang.KM
        '        _Qry &= vbCrLf & ", FTLangKM AS FTLang"
        '    Case ST.Lang.eLang.VT
        '        _Qry &= vbCrLf & ", FTLangVT AS FTLang"
        'End Select
        '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_LANG) & "].dbo.HSysLanguage WITH(NOLOCK) "
        '_Qry &= vbCrLf & "WHERE FTFormName='" & HI.UL.ULF.rpQuoted(Me.Name.ToString) & "'"
        'Dim _oDtLang As System.Data.DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)
        'With Me.ogv
        '    For I As Integer = .Columns.Count - 1 To 0 Step -1

        '        Select Case .Columns(I).FieldName.ToString.ToUpper

        '            Case "FNHSysEmpID".ToUpper, "FTEmpCode".ToUpper, "FTEmpName".ToUpper, "FTEmpStatusName".ToUpper, "FTEmpTypeName".ToUpper, "FTDeptName".ToUpper _
        '                , "FTDivisonName".ToUpper, "FTSectName".ToUpper, "FTUnitSectName".ToUpper, "FTPositName".ToUpper, "FTEmpTypeCode".ToUpper, "FTDeptCode".ToUpper _
        '                , "FTDivisonCode".ToUpper, "FTSectCode".ToUpper, "FTUnitSectCode".ToUpper, "FTPositCode".ToUpper, "FNEmpStatus".ToUpper, "FTPayYear".ToUpper _
        '                , "FTPayTerm".ToUpper, "FNHSysEmpID".ToUpper, "FTEmpIdNo".ToUpper, "FNHoliday".ToUpper, "FNSalary".ToUpper, "FNWorkingHour".ToUpper, "FNOt1".ToUpper _
        '                , "FNOt1".ToUpper, "FNOt15".ToUpper, "FNOt2".ToUpper, "FNOt3".ToUpper, "FNOt4".ToUpper, "FNTotalLeavePay".ToUpper, "FCBaht".ToUpper, "FCOt1_Baht".ToUpper, "FCOt15_Baht".ToUpper _
        '                , "FCOt2_Baht".ToUpper, "FCOt3_Baht".ToUpper, "FCOt4_Baht".ToUpper, "FCNetBaht".ToUpper, "FNPayLeaveVacationBaht".ToUpper, "FNPayLeaveOtherBaht".ToUpper, "FNTotalRecalSSO".ToUpper _
        '                , "FNTotalRecalTAX".ToUpper, "FNTotalAdd".ToUpper, "FNTotalAddOther".ToUpper, "FNTotalExpense".ToUpper, "FNTotalExpenseOther".ToUpper, "FNTotalIncome".ToUpper _
        '                , "FNSocial".ToUpper, "FNTax".ToUpper, "FHolidayBaht".ToUpper, "FNNetpay".ToUpper, "FNPayLeaveSSo".ToUpper, "FNNetpay".ToUpper, "FNPayLeaveSSo".ToUpper, "FNIncentiveAmt".ToUpper _
        '                , "FNIncentiveAmt".ToUpper, "FNWorkingDay".ToUpper, "FNAttandanceAmt".ToUpper, "FNHealtCareAmt".ToUpper, "FNTransportAmt".ToUpper, "FNChildCareAmt".ToUpper _
        '                , "FNTransportAmt".ToUpper, "FNChildCareAmt".ToUpper, "FNOTMealAmt".ToUpper, "FNSocialBase".ToUpper, "FNWorkAgeSalary".ToUpper, "FNOTMealAmtUS".ToUpper _
        '                , "FNTransportAmt".ToUpper, "FNChildCareAmt".ToUpper, "FNOTMealAmt".ToUpper, "FNSocialBase".ToUpper, "FNWorkAgeSalary".ToUpper, "FNOTMealAmtUS".ToUpper _
        '                , "FNSkillBaht".ToUpper, "FNHarmfulBaht".ToUpper, "FNPayRestOTBaht".ToUpper, "FNPayRestOTMin".ToUpper, "FNSocialInsuranceEmployee".ToUpper, "FNSocialInsuranceEmployer".ToUpper _
        '                , "FNHealthInsuranceEmployee".ToUpper, "FNHealthInsuranceEmployer".ToUpper, "FNUnemploymentInsuranceEmployee".ToUpper, "FNHealthInsuranceEmployer".ToUpper _
        '                , "FNUnemploymentInsuranceEmployee".ToUpper, "FNUnemploymentInsuranceEmployer".ToUpper, "FNUnionInsuranceEmployee".ToUpper, "FNUnionInsuranceEmployer".ToUpper
        '            Case Else
        '                .Columns.Remove(.Columns(I))
        '        End Select
        '    Next


        '    If Not (_oDt Is Nothing) Then
        '        For Each Col As DataColumn In _oDt.Columns
        '            Select Case Col.ColumnName.ToString.ToUpper

        '                Case "FNHSysEmpID1".ToUpper, "FNHSysEmpID".ToUpper, "FTEmpCode".ToUpper, "FTEmpName".ToUpper, "FTEmpStatusName".ToUpper, "FTEmpTypeName".ToUpper, "FTDeptName".ToUpper _
        '                    , "FTDivisonName".ToUpper, "FTSectName".ToUpper, "FTUnitSectName".ToUpper, "FTPositName".ToUpper, "FTEmpTypeCode".ToUpper, "FTDeptCode".ToUpper _
        '                    , "FTDivisonCode".ToUpper, "FTSectCode".ToUpper, "FTUnitSectCode".ToUpper, "FTPositCode".ToUpper, "FNEmpStatus".ToUpper, "FTPayYear".ToUpper _
        '                    , "FTPayTerm".ToUpper, "FNHSysEmpID".ToUpper, "FTEmpIdNo".ToUpper, "FNHoliday".ToUpper, "FNSalary".ToUpper, "FNWorkingHour".ToUpper, "FNOt1".ToUpper _
        '                    , "FNOt1".ToUpper, "FNOt15".ToUpper, "FNOt2".ToUpper, "FNOt3".ToUpper, "FNOt4".ToUpper, "FNTotalLeavePay".ToUpper, "FCBaht".ToUpper, "FCOt1_Baht".ToUpper, "FCOt15_Baht".ToUpper _
        '                    , "FCOt2_Baht".ToUpper, "FCOt3_Baht".ToUpper, "FCOt4_Baht".ToUpper, "FCNetBaht".ToUpper, "FNPayLeaveVacationBaht".ToUpper, "FNPayLeaveOtherBaht".ToUpper, "FNTotalRecalSSO".ToUpper _
        '                    , "FNTotalRecalTAX".ToUpper, "FNTotalAdd".ToUpper, "FNTotalAddOther".ToUpper, "FNTotalExpense".ToUpper, "FNTotalExpenseOther".ToUpper, "FNTotalIncome".ToUpper _
        '                    , "FNSocial".ToUpper, "FNTax".ToUpper, "FHolidayBaht".ToUpper, "FNNetpay".ToUpper, "FNPayLeaveSSo".ToUpper, "FNNetpay".ToUpper, "FNPayLeaveSSo".ToUpper, "FNIncentiveAmt".ToUpper _
        '                    , "FNIncentiveAmt".ToUpper, "FNWorkingDay".ToUpper, "FNAttandanceAmt".ToUpper, "FNHealtCareAmt".ToUpper, "FNTransportAmt".ToUpper, "FNChildCareAmt".ToUpper _
        '                    , "FNTransportAmt".ToUpper, "FNChildCareAmt".ToUpper, "FNOTMealAmt".ToUpper, "FNSocialBase".ToUpper, "FNWorkAgeSalary".ToUpper, "FNOTMealAmtUS".ToUpper _
        '                    , "FNTransportAmt".ToUpper, "FNChildCareAmt".ToUpper, "FNOTMealAmt".ToUpper, "FNSocialBase".ToUpper, "FNWorkAgeSalary".ToUpper, "FNOTMealAmtUS".ToUpper _
        '                    , "FNSkillBaht".ToUpper, "FNHarmfulBaht".ToUpper, "FNPayRestOTBaht".ToUpper, "FNPayRestOTMin".ToUpper, "FNSocialInsuranceEmployee".ToUpper, "FNSocialInsuranceEmployer".ToUpper _
        '                    , "FNHealthInsuranceEmployee".ToUpper, "FNHealthInsuranceEmployer".ToUpper, "FNUnemploymentInsuranceEmployee".ToUpper, "FNHealthInsuranceEmployer".ToUpper _
        '                    , "FNUnemploymentInsuranceEmployee".ToUpper, "FNUnemploymentInsuranceEmployer".ToUpper, "FNUnionInsuranceEmployee".ToUpper, "FNUnionInsuranceEmployer".ToUpper
        '                Case Else

        '                    Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
        '                    With ColG
        '                        .Visible = True
        '                        .FieldName = Col.ColumnName.ToString
        '                        .Name = "c" & Col.ColumnName.ToString
        '                        For Each R As DataRow In _oDtLang.Select("FTObjectName='" & HI.UL.ULF.rpQuoted(.Name) & "'")
        '                            .Caption = HI.UL.ULF.rpQuoted(R!FTLang.ToString)
        '                            Exit For
        '                        Next
        '                    End With

        '                    .Columns.Add(ColG)


        '                    With .Columns(Col.ColumnName.ToString)

        '                        '.OptionsFilter.AllowAutoFilter = False
        '                        '.OptionsFilter.AllowFilter = False
        '                        .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '                        .DisplayFormat.FormatString = "{0:n0}"
        '                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far



        '                        With .OptionsColumn
        '                            '.AllowMove = False
        '                            '.AllowGroup = DevExpress.Utils.DefaultBoolean.False
        '                            '.AllowSort = DevExpress.Utils.DefaultBoolean.False
        '                            .AllowEdit = False
        '                            .ReadOnly = True
        '                        End With

        '                    End With

        '                    .Columns(Col.ColumnName.ToString).Width = 200
        '                    .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
        '                    .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"
        '            End Select
        '        Next
        '    End If


        'End With

        With Me.ogc
            .DataSource = _oDt
            ogv.ExpandAllGroups()
            ogv.RefreshData()
            ogv.BestFitColumns()
        End With
        Dim oSysLang As New ST.SysLanguage
        Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.ToString.Trim, Me.Name)
        Call HI.ST.Lang.SP_SETxLanguage(Me)
    End Sub


    'Private Sub LoadGrid()

    '    Dim _Qry As String = ""

    '    _Qry = "  SELECT   Top 0    M.FNHSysEmpID, M.FTEmpCode"

    '    If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
    '        _Qry &= vbCrLf & "  ,P1.FTPreNameNameTH + ' ' + M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"
    '        _Qry &= vbCrLf & "  ,ES.FTNameTH  AS FTEmpStatusName "
    '        _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameTH  AS FTEmpTypeName "
    '        _Qry &= vbCrLf & "  ,Dept.FTDeptDescTH  AS FTDeptName "
    '        _Qry &= vbCrLf & "  ,OrgDiv.FTDivisonNameTH  AS FTDivisonName "
    '        _Qry &= vbCrLf & "  ,OrgSect.FTSectNameTH  AS FTSectName "
    '        _Qry &= vbCrLf & "  ,OrgUnitSect.FTUnitSectNameTH  AS FTUnitSectName "
    '        _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameTH  AS FTPositName "
    '    Else
    '        _Qry &= vbCrLf & "  ,P1.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpName"
    '        _Qry &= vbCrLf & "  ,ES.FTNameEN  AS FTEmpStatusName "
    '        _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameEN  AS FTEmpTypeName "
    '        _Qry &= vbCrLf & "  ,Dept.FTDeptDescEN  AS FTDeptName "
    '        _Qry &= vbCrLf & "  ,OrgDiv.FTDivisonNameEN  AS FTDivisonName "
    '        _Qry &= vbCrLf & "  ,OrgSect.FTSectNameEN  AS FTSectName "
    '        _Qry &= vbCrLf & "  ,OrgUnitSect.FTUnitSectNameEN  AS FTUnitSectName "
    '        _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameEN  AS FTPositName "
    '    End If

    '    _Qry &= vbCrLf & "  ,ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
    '    _Qry &= vbCrLf & "  ,ISNULL(Dept.FTDeptCode,'') AS FTDeptCode, ISNULL(OrgDiv.FTDivisonCode,'') AS FTDivisonCode"
    '    _Qry &= vbCrLf & "  ,ISNULL(OrgSect.FTSectCode,'') AS FTSectCode,ISNULL(OrgUnitSect.FTUnitSectCode,'') AS FTUnitSectCode, OrgPosit.FTPositCode, M.FNEmpStatus"
    '    _Qry &= vbCrLf & "  ,P.FTPayYear, P.FTPayTerm, P.FNHSysEmpID, P.FTEmpIdNo"
    '    _Qry &= vbCrLf & "  ,P.FNHoliday, P.FNSalary, P.FNWorkingHour"
    '    _Qry &= vbCrLf & "  ,Replace(Convert(varchar(30),P.FNOt1),'.',':') AS  FNOt1"
    '    _Qry &= vbCrLf & "  , Replace(Convert(varchar(30),P.FNOt15),'.',':') AS FNOt15"
    '    _Qry &= vbCrLf & "  , Replace(Convert(varchar(30),P.FNOt2),'.',':') AS FNOt2"
    '    _Qry &= vbCrLf & "  , Replace(Convert(varchar(30),P.FNOt3),'.',':') AS FNOt3"
    '    _Qry &= vbCrLf & "  ,P.FNOt4, P.FNTotalLeavePay, P.FCBaht, P.FCOt1_Baht, P.FCOt15_Baht, P.FCOt2_Baht"
    '    _Qry &= vbCrLf & "  ,P.FCOt3_Baht, P.FCOt4_Baht, P.FCNetBaht, P.FNPayLeaveVacationBaht,P.FNPayLeaveOtherBaht"
    '    _Qry &= vbCrLf & "  ,P.FNTotalRecalSSO, P.FNTotalRecalTAX, P.FNTotalAdd, P.FNTotalAddOther, P.FNTotalExpense"
    '    _Qry &= vbCrLf & "  ,P.FNTotalExpenseOther, P.FNTotalIncome, P.FNSocial, P.FNTax, P.FHolidayBaht"
    '    _Qry &= vbCrLf & "  , P.FNPayLeaveSSo"
    '    _Qry &= vbCrLf & "  ,ISNULL(P.FNIncentiveAmt,0) AS FNIncentiveAmt"
    '    _Qry &= vbCrLf & " ,  (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(P.FNTotalWKNMin / 480.00))),2)"
    '    _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((P.FNTotalWKNMin % 480.00) / 60.00))),2)"
    '    _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((P.FNTotalWKNMin % 480.00) % 60.00))),2))  AS FNWorkingDay"

    '    _Qry &= vbCrLf & "  , P.FNAttandanceAmt, P.FNHealtCareAmt "
    '    _Qry &= vbCrLf & " ,P.FNTransportAmt, P.FNChildCareAmt, P.FNOTMealAmt, P.FNSocialBase, P.FNWorkAgeSalary, P.FNOTMealAmtUS "

    '    'new 
    '    _Qry &= vbCrLf & ",P.FNSkillBaht, P.FNHarmfulBaht, P.FNPayRestOTBaht, P.FNPayRestOTMin"
    '    _Qry &= vbCrLf & ", P.FNSocialInsuranceEmployee, P.FNSocialInsuranceEmployer, P.FNHealthInsuranceEmployee"
    '    _Qry &= vbCrLf & ",P.FNHealthInsuranceEmployer, P.FNUnemploymentInsuranceEmployee, P.FNUnemploymentInsuranceEmployer, P.FNUnionInsuranceEmployee, P.FNUnionInsuranceEmployer"

    '    _Qry &= vbCrLf & ", T.[001], T.[002], T.[003], T.[004], T.[005], T.[006], T.[007], T.[008], T.[009], T.[010], T.[011], T.[012], T.[013], T.[014], T.[015], T.[016], T.[017], T.[018], T.[019], T.[023], T.[024], T.[101], T.[102], "
    '    _Qry &= vbCrLf & "  T.[103], T.[104], T.[105], T.[106], T.[107], T.[108], T.[109], T.[110] ,P.FNNetpay"

    '    _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
    '    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS P WITH (NOLOCK) ON M.FNHSysEmpID=P.FNHSysEmpID INNER JOIN"
    '    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P1 WITH (NOLOCK) ON M.FNHSysPreNameId = P1.FNHSysPreNameId  INNER JOIN"
    '    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON P.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
    '    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON P.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
    '    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS OrgDiv WITH (NOLOCK) ON P.FNHSysDivisonId = OrgDiv.FNHSysDivisonId LEFT OUTER JOIN"
    '    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS OrgSect WITH (NOLOCK) ON P.FNHSysSectId = OrgSect.FNHSysSectId LEFT OUTER JOIN"
    '    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS OrgUnitSect WITH (NOLOCK) ON P.FNHSysUnitSectId = OrgUnitSect.FNHSysUnitSectId LEFT OUTER JOIN"
    '    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS OrgPosit WITH (NOLOCK) ON P.FNHSysPositId = OrgPosit.FNHSysPositId LEFT OUTER JOIN"
    '    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MEmpStatus AS ES ON M.FNEmpStatus = ES.FNListIndex"
    '    _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.V_PayrollFin AS T ON M.FNHSysEmpID = T.FNHSysEmpID AND  P.FTPayYear = T.FTPayYear"
    '    _Qry &= vbCrLf & " AND P.FTPayTerm  = T.FTPayTerm"
    '    _Qry &= vbCrLf & "  WHERE        (M.FTEmpCode <> '')"
    '    _Qry &= vbCrLf & "   AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "

    '    _Qry &= vbCrLf & "   AND  P.FTPayYear ='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "'  "
    '    _Qry &= vbCrLf & "   AND  P.FTPayTerm ='" & HI.UL.ULF.rpQuoted(Me.FTPayTerm.Text) & "'  "


    '    If Me.FNHSysEmpTypeId.Text <> "" Then
    '        _Qry &= vbCrLf & " AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
    '    End If

    '    '------Criteria By Employeee Code
    '    If Me.FNHSysEmpId.Text <> "" Then
    '        _Qry &= vbCrLf & " AND M.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
    '    End If

    '    If Me.FNHSysEmpIdTo.Text <> "" Then
    '        _Qry &= vbCrLf & " AND M.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
    '    End If

    '    '------Criteria By Department
    '    If Me.FNHSysDeptId.Text <> "" Then
    '        _Qry &= vbCrLf & " AND  Dept.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
    '    End If

    '    If Me.FNHSysDeptIdTo.Text <> "" Then
    '        _Qry &= vbCrLf & " AND  Dept.FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
    '    End If

    '    '------Criteria By Division
    '    If Me.FNHSysDivisonId.Text <> "" Then
    '        _Qry &= vbCrLf & " AND  OrgDiv.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
    '    End If

    '    If Me.FNHSysDivisonIdTo.Text <> "" Then
    '        _Qry &= vbCrLf & " AND  OrgDiv.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
    '    End If

    '    '------Criteria By Sect
    '    If Me.FNHSysSectId.Text <> "" Then
    '        _Qry &= vbCrLf & " AND  OrgSect.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
    '    End If

    '    If Me.FNHSysSectIdTo.Text <> "" Then
    '        _Qry &= vbCrLf & " AND  OrgSect.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
    '    End If

    '    '------Criteria Unit Sect
    '    If Me.FNHSysUnitSectId.Text <> "" Then
    '        _Qry &= vbCrLf & " AND   OrgUnitSect.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
    '    End If

    '    If Me.FNHSysUnitSectIdTo.Text <> "" Then
    '        _Qry &= vbCrLf & " AND   OrgUnitSect.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
    '    End If

    '    _Qry = _Qry & HI.ST.Security.PermissionFilterEmployeeSalary()



    '    Dim _oDt As System.Data.DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

    '    _Qry = "Select FTFormName, FTObjectName"
    '    Select Case HI.ST.Lang.Language
    '        Case ST.Lang.eLang.TH
    '            _Qry &= vbCrLf & ", FTLangTH AS FTLang"
    '        Case ST.Lang.eLang.EN
    '            _Qry &= vbCrLf & ", FTLangEN AS FTLang"
    '        Case ST.Lang.eLang.KM
    '            _Qry &= vbCrLf & ", FTLangKM AS FTLang"
    '        Case ST.Lang.eLang.VT
    '            _Qry &= vbCrLf & ", FTLangVT AS FTLang"
    '    End Select
    '    _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_LANG) & "].dbo.HSysLanguage WITH(NOLOCK) "
    '    _Qry &= vbCrLf & "WHERE FTFormName='" & HI.UL.ULF.rpQuoted(Me.Name.ToString) & "'"
    '    Dim _oDtLang As System.Data.DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)
    '    With Me.ogv
    '        For I As Integer = .Columns.Count - 1 To 0 Step -1

    '            Select Case .Columns(I).FieldName.ToString.ToUpper

    '                Case "FNHSysEmpID".ToUpper, "FTEmpCode".ToUpper, "FTEmpName".ToUpper, "FTEmpStatusName".ToUpper, "FTEmpTypeName".ToUpper, "FTDeptName".ToUpper _
    '                    , "FTDivisonName".ToUpper, "FTSectName".ToUpper, "FTUnitSectName".ToUpper, "FTPositName".ToUpper, "FTEmpTypeCode".ToUpper, "FTDeptCode".ToUpper _
    '                    , "FTDivisonCode".ToUpper, "FTSectCode".ToUpper, "FTUnitSectCode".ToUpper, "FTPositCode".ToUpper, "FNEmpStatus".ToUpper, "FTPayYear".ToUpper _
    '                    , "FTPayTerm".ToUpper, "FNHSysEmpID".ToUpper, "FTEmpIdNo".ToUpper, "FNHoliday".ToUpper, "FNSalary".ToUpper, "FNWorkingHour".ToUpper, "FNOt1".ToUpper _
    '                    , "FNOt1".ToUpper, "FNOt15".ToUpper, "FNOt2".ToUpper, "FNOt3".ToUpper, "FNOt4".ToUpper, "FNTotalLeavePay".ToUpper, "FCBaht".ToUpper, "FCOt1_Baht".ToUpper, "FCOt15_Baht".ToUpper _
    '                    , "FCOt2_Baht".ToUpper, "FCOt3_Baht".ToUpper, "FCOt4_Baht".ToUpper, "FCNetBaht".ToUpper, "FNPayLeaveVacationBaht".ToUpper, "FNPayLeaveOtherBaht".ToUpper, "FNTotalRecalSSO".ToUpper _
    '                    , "FNTotalRecalTAX".ToUpper, "FNTotalAdd".ToUpper, "FNTotalAddOther".ToUpper, "FNTotalExpense".ToUpper, "FNTotalExpenseOther".ToUpper, "FNTotalIncome".ToUpper _
    '                    , "FNSocial".ToUpper, "FNTax".ToUpper, "FHolidayBaht".ToUpper, "FNNetpay".ToUpper, "FNPayLeaveSSo".ToUpper, "FNNetpay".ToUpper, "FNPayLeaveSSo".ToUpper, "FNIncentiveAmt".ToUpper _
    '                    , "FNIncentiveAmt".ToUpper, "FNWorkingDay".ToUpper, "FNAttandanceAmt".ToUpper, "FNHealtCareAmt".ToUpper, "FNTransportAmt".ToUpper, "FNChildCareAmt".ToUpper _
    '                    , "FNTransportAmt".ToUpper, "FNChildCareAmt".ToUpper, "FNOTMealAmt".ToUpper, "FNSocialBase".ToUpper, "FNWorkAgeSalary".ToUpper, "FNOTMealAmtUS".ToUpper _
    '                    , "FNTransportAmt".ToUpper, "FNChildCareAmt".ToUpper, "FNOTMealAmt".ToUpper, "FNSocialBase".ToUpper, "FNWorkAgeSalary".ToUpper, "FNOTMealAmtUS".ToUpper _
    '                    , "FNSkillBaht".ToUpper, "FNHarmfulBaht".ToUpper, "FNPayRestOTBaht".ToUpper, "FNPayRestOTMin".ToUpper, "FNSocialInsuranceEmployee".ToUpper, "FNSocialInsuranceEmployer".ToUpper _
    '                    , "FNHealthInsuranceEmployee".ToUpper, "FNHealthInsuranceEmployer".ToUpper, "FNUnemploymentInsuranceEmployee".ToUpper, "FNHealthInsuranceEmployer".ToUpper _
    '                    , "FNUnemploymentInsuranceEmployee".ToUpper, "FNUnemploymentInsuranceEmployer".ToUpper, "FNUnionInsuranceEmployee".ToUpper, "FNUnionInsuranceEmployer".ToUpper
    '                Case Else
    '                    .Columns.Remove(.Columns(I))
    '            End Select
    '        Next


    '        If Not (_oDt Is Nothing) Then
    '            For Each Col As DataColumn In _oDt.Columns
    '                Select Case Col.ColumnName.ToString.ToUpper

    '                    Case "FNHSysEmpID1".ToUpper, "FNHSysEmpID".ToUpper, "FTEmpCode".ToUpper, "FTEmpName".ToUpper, "FTEmpStatusName".ToUpper, "FTEmpTypeName".ToUpper, "FTDeptName".ToUpper _
    '                        , "FTDivisonName".ToUpper, "FTSectName".ToUpper, "FTUnitSectName".ToUpper, "FTPositName".ToUpper, "FTEmpTypeCode".ToUpper, "FTDeptCode".ToUpper _
    '                        , "FTDivisonCode".ToUpper, "FTSectCode".ToUpper, "FTUnitSectCode".ToUpper, "FTPositCode".ToUpper, "FNEmpStatus".ToUpper, "FTPayYear".ToUpper _
    '                        , "FTPayTerm".ToUpper, "FNHSysEmpID".ToUpper, "FTEmpIdNo".ToUpper, "FNHoliday".ToUpper, "FNSalary".ToUpper, "FNWorkingHour".ToUpper, "FNOt1".ToUpper _
    '                        , "FNOt1".ToUpper, "FNOt15".ToUpper, "FNOt2".ToUpper, "FNOt3".ToUpper, "FNOt4".ToUpper, "FNTotalLeavePay".ToUpper, "FCBaht".ToUpper, "FCOt1_Baht".ToUpper, "FCOt15_Baht".ToUpper _
    '                        , "FCOt2_Baht".ToUpper, "FCOt3_Baht".ToUpper, "FCOt4_Baht".ToUpper, "FCNetBaht".ToUpper, "FNPayLeaveVacationBaht".ToUpper, "FNPayLeaveOtherBaht".ToUpper, "FNTotalRecalSSO".ToUpper _
    '                        , "FNTotalRecalTAX".ToUpper, "FNTotalAdd".ToUpper, "FNTotalAddOther".ToUpper, "FNTotalExpense".ToUpper, "FNTotalExpenseOther".ToUpper, "FNTotalIncome".ToUpper _
    '                        , "FNSocial".ToUpper, "FNTax".ToUpper, "FHolidayBaht".ToUpper, "FNPayLeaveSSo".ToUpper, "FNPayLeaveSSo".ToUpper, "FNIncentiveAmt".ToUpper _
    '                        , "FNIncentiveAmt".ToUpper, "FNWorkingDay".ToUpper, "FNAttandanceAmt".ToUpper, "FNHealtCareAmt".ToUpper, "FNTransportAmt".ToUpper, "FNChildCareAmt".ToUpper _
    '                        , "FNTransportAmt".ToUpper, "FNChildCareAmt".ToUpper, "FNOTMealAmt".ToUpper, "FNSocialBase".ToUpper, "FNWorkAgeSalary".ToUpper, "FNOTMealAmtUS".ToUpper _
    '                        , "FNTransportAmt".ToUpper, "FNChildCareAmt".ToUpper, "FNOTMealAmt".ToUpper, "FNSocialBase".ToUpper, "FNWorkAgeSalary".ToUpper, "FNOTMealAmtUS".ToUpper _
    '                        , "FNSkillBaht".ToUpper, "FNHarmfulBaht".ToUpper, "FNPayRestOTBaht".ToUpper, "FNPayRestOTMin".ToUpper, "FNSocialInsuranceEmployee".ToUpper, "FNSocialInsuranceEmployer".ToUpper _
    '                        , "FNHealthInsuranceEmployee".ToUpper, "FNHealthInsuranceEmployer".ToUpper, "FNUnemploymentInsuranceEmployee".ToUpper, "FNHealthInsuranceEmployer".ToUpper _
    '                        , "FNUnemploymentInsuranceEmployee".ToUpper, "FNUnemploymentInsuranceEmployer".ToUpper, "FNUnionInsuranceEmployee".ToUpper, "FNUnionInsuranceEmployer".ToUpper
    '                    Case "FNNetpay".ToUpper
    '                        .Columns("FNNetpay").VisibleIndex = 100
    '                    Case Else

    '                        Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
    '                        With ColG
    '                            .Visible = True
    '                            .FieldName = Col.ColumnName.ToString
    '                            .Name = "c" & Col.ColumnName.ToString
    '                            For Each R As DataRow In _oDtLang.Select("FTObjectName='" & HI.UL.ULF.rpQuoted(.Name) & "'")
    '                                .Caption = HI.UL.ULF.rpQuoted(R!FTLang.ToString)
    '                                Exit For
    '                            Next
    '                        End With

    '                        .Columns.Add(ColG)


    '                        With .Columns(Col.ColumnName.ToString)

    '                            '.OptionsFilter.AllowAutoFilter = False
    '                            '.OptionsFilter.AllowFilter = False
    '                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '                            .DisplayFormat.FormatString = "{0:n0}"
    '                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
    '                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    '                            If Col.ColumnName.ToString Like "0*" Then
    '                                .AppearanceCell.BackColor = System.Drawing.Color.RoyalBlue
    '                            Else
    '                                .AppearanceCell.BackColor = System.Drawing.Color.Silver
    '                            End If



    '                            With .OptionsColumn
    '                                '.AllowMove = False
    '                                '.AllowGroup = DevExpress.Utils.DefaultBoolean.False
    '                                '.AllowSort = DevExpress.Utils.DefaultBoolean.False
    '                                .AllowEdit = False
    '                                .ReadOnly = True
    '                            End With

    '                        End With

    '                        .Columns(Col.ColumnName.ToString).Width = 200
    '                        .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
    '                        .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"
    '                End Select
    '            Next
    '        End If


    '    End With

    '    With Me.ogc
    '        .DataSource = _oDt
    '        'ogv.ExpandAllGroups()
    '        'ogv.RefreshData()
    '        'ogv.BestFitColumns()
    '    End With
    '    'Dim oSysLang As New ST.SysLanguage
    '    'Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.ToString.Trim, Me.Name)
    '    'Call HI.ST.Lang.SP_SETxLanguage(Me)
    'End Sub

#End Region

#Region "General"
    Private Sub wEmployeeListing_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        'Call InitGrid()
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
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogv)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub
End Class