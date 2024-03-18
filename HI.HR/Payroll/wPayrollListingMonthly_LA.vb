Imports DevExpress.Data
Imports System.Text

Public Class wPayrollListingMonthly_LA
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
        Dim sFieldSum As String = "FCBaht|FCOt1_Baht|FCOt15_Baht|FCOt2_Baht|FCOt3_Baht|FCOt4_Baht|FNIncentiveAmt|FCNetBaht|FNPayLeaveVacationBaht|FNPayLeaveOtherBaht|FNTotalAdd|FNTotalAddOther|FNTotalExpense|FNTotalExpenseOther|FNTotalIncome|FNTotalRecalSSO|FNSocial|FNTax|FHolidayBaht|FNNetpay|FNTotalRecalTAX|008|009|014|016|017|032|043|050|112|113|053|054|055|056|057|058|059|061|062|063|064|080|081|FNSocialCmp"

        Dim sFieldGrpCount As String = "FTEmpCode"
        Dim sFieldGrpSum As String = "FCBaht|FNTotalIncome|FNTotalRecalSSO|FNSocial|FNTax|FNNetpay|FNTotalRecalTAX|FNSocialCmp"

        Dim sFieldCustomSum As String = "FNWorkingDay|FNOt1|FNOt15|FNOt2|FNOt3|FNOt4"
        Dim sFieldCustomGrpSum As String = "FNWorkingDay"

        With ogv
            .ClearGrouping()
            .ClearDocument()

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
                                        totalSum = totalSum + Integer.Parse((Str))
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
        _Qry = "SELECT FTEmpCode,FTEmpName,FTEmpStatusName,FTEmpTypeName,FTDeptName,FTDivisonName,FTSectName,FTUnitSectName,"
        _Qry &= vbCrLf & "FTPositName,FTAccountGroupCode,FTAccountGroupName,FTEmpTypeCode,FTDeptCode,FTDivisonCode,"
        _Qry &= vbCrLf & "FTSectCode,FTUnitSectCode,FTPositCode,FNEmpStatus,FTPayYear,FNHSysEmpID,FTEmpIdNo,"
        _Qry &= vbCrLf & "SUM(FNHoliday) AS FNHoliday,MAX(FNSalary) AS FNSalary,SUM(FNWorkingHour) AS FNWorkingHour,"
        _Qry &= vbCrLf & "SUM(FNOt1) AS FNOt1,SUM(FNOt15) AS FNOt15, SUM(FNOt2) AS FNOt2,"
        _Qry &= vbCrLf & "SUM(FNOt3) AS FNOt3, SUM(FNOt4) AS FNOt4, SUM(FNTotalLeavePay) AS FNTotalLeavePay,"
        _Qry &= vbCrLf & "SUM(FCBaht) AS FCBaht, SUM(FCOt1_Baht) AS FCOt1_Baht,SUM(FCOt15_Baht) AS FCOt15_Baht, "
        _Qry &= vbCrLf & "SUM(FCOt2_Baht) AS FCOt2_Baht, SUM(FCOt3_Baht) AS FCOt3_Baht, SUM(FCOt4_Baht) AS FCOt4_Baht,"
        _Qry &= vbCrLf & "SUM(FCNetBaht) AS FCNetBaht, SUM(FNPayLeaveVacationBaht) AS FNPayLeaveVacationBaht, "
        _Qry &= vbCrLf & "SUM(FNPayLeaveOtherBaht) AS FNPayLeaveOtherBaht, SUM(FNParturitionLeaveBaht) AS FNParturitionLeaveBaht, "
        _Qry &= vbCrLf & "SUM(FNSickLeaveBaht) AS FNSickLeaveBaht, SUM(FNTotalRecalSSO) AS FNTotalRecalSSO,"
        _Qry &= vbCrLf & "SUM(FNTotalRecalTAX) AS FNTotalRecalTAX, SUM(FNTotalAdd) AS FNTotalAdd, "
        _Qry &= vbCrLf & "SUM(FNTotalAddOther) AS FNTotalAddOther,SUM(FNTotalExpense) AS FNTotalExpense,"
        _Qry &= vbCrLf & "SUM(FNTotalExpenseOther) AS FNTotalExpenseOther, SUM(FNTotalIncome) AS FNTotalIncome, "
        _Qry &= vbCrLf & "SUM(FNSocial) AS FNSocial,SUM(FNTax) AS FNTax, SUM(FHolidayBaht) AS FHolidayBaht, "
        _Qry &= vbCrLf & "SUM(FNNetpay) AS FNNetpay, SUM(FNPayLeaveSSo) AS FNPayLeaveSSo,SUM(FNSocialCmp) AS FNSocialCmp, "
        '--SUM(FNWorkingDay) AS FNWorkingDay,
        _Qry &= vbCrLf & "SUM([001]) AS '001',SUM([009]) AS '009',SUM([012]) AS '012',SUM([016]) AS '016',"
        _Qry &= vbCrLf & "SUM([032]) AS '032',SUM([033]) AS '033',SUM([034]) AS '034',SUM([035]) AS '035',"
        _Qry &= vbCrLf & "SUM([036]) AS '036',SUM([043]) AS '043',SUM([106]) AS '106',SUM([108]) AS '108',"
        _Qry &= vbCrLf & "SUM([109]) AS '109',SUM([110]) AS '110',SUM([111]) AS '111',SUM([112]) AS '112',"
        _Qry &= vbCrLf & "SUM([113]) AS '113',SUM([002]) AS '002',SUM([017]) AS '017',SUM([008]) AS '008',"
        _Qry &= vbCrLf & "SUM([014]) AS '014',SUM([050]) AS '050',SUM(FNIncentiveAmt) AS FNIncentiveAmt,"
        _Qry &= vbCrLf & "SUM([051]) AS '051',SUM([053]) AS '053',SUM([054]) AS '054',SUM([055]) AS '055',"
        _Qry &= vbCrLf & "SUM([056]) AS '056',SUM([057]) AS '057',SUM([058]) AS '058',SUM([059]) AS '059',"
        _Qry &= vbCrLf & "SUM([060]) AS '060',SUM([061]) AS '061',SUM([062]) AS '062',SUM([063]) AS '063',"
        _Qry &= vbCrLf & "SUM([064]) AS '064',SUM([080]) AS '080',SUM([081]) AS '081',"

        _Qry &= vbCrLf & " FTStateHRSent,FTUserHRSent,FTTimeHRSent,FTStateEmployeeAccept,"
        _Qry &= vbCrLf & "FTUserEmployeeAccept,FTDateEmployeeAccept,FTTimeEmployeeAccept,FTStateHRAccept,"
        _Qry &= vbCrLf & "FTUseHRAccept,FTDateHRAccept,FTTimeHRAccept "
        _Qry &= vbCrLf & " ,(Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(SUM(FNTotalWKNMin) / (max(FCHour) * 60)))),2)"
        _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((SUM(FNTotalWKNMin) % (max(FCHour) * 60)) / 60.00))),2)"
        _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((SUM(FNTotalWKNMin) % (max(FCHour) * 60)) % 60.00))),2))  AS FNWorkingDay"

        _Qry &= vbCrLf & " FROM ("


        _Qry &= vbCrLf & " SELECT M.FTEmpCode"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & " ,P1.FTPreNameNameTH + ' ' + M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"
            _Qry &= vbCrLf & " ,ES.FTNameTH AS FTEmpStatusName,ET.FTEmpTypeNameTH AS FTEmpTypeName "
            _Qry &= vbCrLf & " ,Dept.FTDeptDescTH AS FTDeptName ,OrgDiv.FTDivisonNameTH AS FTDivisonName "
            _Qry &= vbCrLf & " ,OrgSect.FTSectNameTH AS FTSectName ,OrgUnitSect.FTUnitSectNameTH AS FTUnitSectName "
            _Qry &= vbCrLf & " ,OrgPosit.FTPositNameTH AS FTPositName "
            _Qry &= vbCrLf & " ,ACG.FTAccountGroupCode, ACG.FTAccountGroupNameTH AS FTAccountGroupName "
        Else
            _Qry &= vbCrLf & " ,P1.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpName"
            _Qry &= vbCrLf & " ,ES.FTNameEN AS FTEmpStatusName, ET.FTEmpTypeNameEN AS FTEmpTypeName "
            _Qry &= vbCrLf & " ,Dept.FTDeptDescEN AS FTDeptName, OrgDiv.FTDivisonNameEN AS FTDivisonName "
            _Qry &= vbCrLf & " ,OrgSect.FTSectNameEN AS FTSectName, OrgUnitSect.FTUnitSectNameEN AS FTUnitSectName "
            _Qry &= vbCrLf & " ,OrgPosit.FTPositNameEN AS FTPositName "
            _Qry &= vbCrLf & " ,ACG.FTAccountGroupCode, ACG.FTAccountGroupNameEN AS FTAccountGroupName"
        End If

        _Qry &= vbCrLf & " ,ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
        _Qry &= vbCrLf & " ,ISNULL(Dept.FTDeptCode,'') AS FTDeptCode, ISNULL(OrgDiv.FTDivisonCode,'') AS FTDivisonCode"
        _Qry &= vbCrLf & " ,ISNULL(OrgSect.FTSectCode,'') AS FTSectCode,ISNULL(OrgUnitSect.FTUnitSectCode,'') AS FTUnitSectCode"
        _Qry &= vbCrLf & " ,OrgPosit.FTPositCode, M.FNEmpStatus,P.FTPayYear, P.FTPayTerm, P.FNHSysEmpID, P.FTEmpIdNo"
        _Qry &= vbCrLf & " ,P.FNHoliday, P.FNSalary, P.FNWorkingHour"

        ' Modify By Chet 20230725
        _Qry &= vbCrLf & " ,ISNULL(P.FNOt1,0) AS FNOt1, ISNULL(P.FNOt15,0) AS  FNOt15, ISNULL(P.FNOt2,0) AS FNOt2 "
        _Qry &= vbCrLf & " ,ISNULL(P.FNOt3, 0) As FNOt3  , ISNULL(P.FNOt4,0) As FNOt4"
        '_Qry &= vbCrLf & "  ,Replace(Convert(varchar(30),P.FNOt1),'.',':') AS  FNOt1"
        '_Qry &= vbCrLf & "  , Replace(Convert(varchar(30),P.FNOt15),'.',':') AS FNOt15"
        '_Qry &= vbCrLf & "  , Replace(Convert(varchar(30),P.FNOt2),'.',':') AS FNOt2"
        '_Qry &= vbCrLf & "  , Replace(Convert(varchar(30),P.FNOt3),'.',':') AS FNOt3"
        '_Qry &= vbCrLf & "  , Replace(Convert(varchar(30),P.FNOt4),'.',':') AS FNOt4"
        _Qry &= vbCrLf & "  ,P.FNTotalLeavePay, P.FCBaht, P.FCOt1_Baht, P.FCOt15_Baht, P.FCOt2_Baht"
        _Qry &= vbCrLf & "  ,P.FCOt3_Baht, P.FCOt4_Baht, P.FCNetBaht, P.FNPayLeaveVacationBaht,ISNULL(P.FNPayLeaveOtherBaht,0) AS FNPayLeaveOtherBaht , ISNULL(P.FNParturitionLeaveBaht,0) AS FNParturitionLeaveBaht, ISNULL(P.FNSickLeaveBaht,0) AS FNSickLeaveBaht "
        _Qry &= vbCrLf & "  ,P.FNTotalRecalSSO, P.FNTotalRecalTAX, P.FNTotalAdd, P.FNTotalAddOther, P.FNTotalExpense"
        _Qry &= vbCrLf & "  ,P.FNTotalExpenseOther, P.FNTotalIncome, P.FNSocial ,  P.FNTax,ISNULL(P.FNSocialCmp,0) as FNSocialCmp, P.FHolidayBaht"
        _Qry &= vbCrLf & "  ,P.FNNetpay, P.FNPayLeaveSSo,P.FNTotalWKNMin,TS.FCHour"
        ' _Qry &= vbCrLf & "  ,ISNULL(P.FNIncentiveAmt,0) AS FNIncentiveAmt"
        '-------------Comment By Chet 20230725
        '_Qry &= vbCrLf & " ,  (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(P.FNTotalWKNMin / (TS.FCHour * 60)))),2)"
        '_Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((P.FNTotalWKNMin % (TS.FCHour * 60)) / 60.00))),2)"
        '_Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((P.FNTotalWKNMin % (TS.FCHour * 60)) % 60.00))),2))  AS FNWorkingDay"

        _Qry &= vbCrLf & ",ISNULL([001],0) [001], ISNULL([009],0) [009],ISNULL([012],0)[012], ISNULL([016],0)[016],"
        _Qry &= vbCrLf & "ISNULL([032], 0)[032], ISNULL([033],0)[033], ISNULL([034],0)[034], ISNULL([035],0)[035],"
        _Qry &= vbCrLf & "ISNULL([036],0)[036], ISNULL([043],0)[043], ISNULL([106],0)[106], ISNULL([108],0)[108],"
        _Qry &= vbCrLf & "ISNULL([109],0)[109], ISNULL([110],0)[110], ISNULL([111],0)[111], ISNULL([112],0)[112],"
        _Qry &= vbCrLf & "ISNULL([113],0)[113], ISNULL([002],0) [002], ISNULL([017],0) [017], ISNULL([008],0) [008],"
        _Qry &= vbCrLf & "ISNULL([014],0) [014], ISNULL([050],0) [050], ISNULL([011],0) As FNIncentiveAmt,"
        _Qry &= vbCrLf & "ISNULL([051],0) [051], ISNULL([053],0) [053], ISNULL([054],0) [054], ISNULL([055],0) [055],"
        _Qry &= vbCrLf & "ISNULL([056],0) [056], ISNULL([057],0) [057], ISNULL([058],0) [058], ISNULL([059],0) [059],"
        _Qry &= vbCrLf & "ISNULL([060],0) [060], ISNULL([061],0) [061], ISNULL([062],0) [062], ISNULL([063],0) [063],"
        _Qry &= vbCrLf & "ISNULL([064],0) [064],ISNULL([080],0) [080],ISNULL([081],0) [081],"

        _Qry &= vbCrLf & "  ISNULL(FTStateHRSent,'0') as 'FTStateHRSent',"

        _Qry &= vbCrLf & "ISNULL(FTUserHRSent,'') as 'FTUserHRSent', ISNULL(FTDateHRSent,'') as 'FTDateHRSent',  "
        _Qry &= vbCrLf & "ISNULL(FTTimeHRSent,'') as 'FTTimeHRSent', ISNULL(FTStateEmployeeAccept,'0') as 'FTStateEmployeeAccept',"
        _Qry &= vbCrLf & "ISNULL(FTUserEmployeeAccept,'') as 'FTUserEmployeeAccept', "
        _Qry &= vbCrLf & "ISNULL(FTDateEmployeeAccept,'') as 'FTDateEmployeeAccept',"
        _Qry &= vbCrLf & "ISNULL(FTTimeEmployeeAccept,'') as 'FTTimeEmployeeAccept',"
        _Qry &= vbCrLf & "ISNULL(FTStateHRAccept,'0') as 'FTStateHRAccept',"
        _Qry &= vbCrLf & "ISNULL(FTUseHRAccept,'') as 'FTUseHRAccept',ISNULL(FTDateHRAccept,'') as 'FTDateHRAccept',"
        _Qry &= vbCrLf & "ISNULL(FTTimeHRAccept,'') as 'FTTimeHRAccept'"

        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) "
        _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS P WITH (NOLOCK) ON M.FNHSysEmpID=P.FNHSysEmpID "
        _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P1 WITH (NOLOCK) ON M.FNHSysPreNameId = P1.FNHSysPreNameId  "
        _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON P.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON P.FNHSysDeptId = Dept.FNHSysDeptId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS OrgDiv WITH (NOLOCK) ON P.FNHSysDivisonId = OrgDiv.FNHSysDivisonId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS OrgSect WITH (NOLOCK) ON P.FNHSysSectId = OrgSect.FNHSysSectId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS OrgUnitSect WITH (NOLOCK) ON P.FNHSysUnitSectId = OrgUnitSect.FNHSysUnitSectId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS OrgPosit WITH (NOLOCK) ON P.FNHSysPositId = OrgPosit.FNHSysPositId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS TS WITH (NOLOCK) ON M.FNHSysShiftID=TS.FNHSysShiftID "
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MEmpStatus AS ES ON M.FNEmpStatus = ES.FNListIndex"
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMAccountGroup AS ACG WITH(NOLOCK) ON OrgUnitSect.FNHSysAccountGroupId = ACG.FNHSysAccountGroupId "

        _Qry &= vbCrLf & " LEFT JOIN ( SELECT FTPayYear, FTPayTerm,FNHSysEmpID,[001], [009],[012], [016],[032],[033],[034],"
        _Qry &= vbCrLf & " [035],[036], [043],[106],[108],[109],[110],[111],[112],[113] ,[002],[017],[008],[014] ,[050],"
        _Qry &= vbCrLf & " [011] ,[051] , [053], [054], [055], [056], [057], [058], [059], [060], [061], [062], [063], [064], [080], [081] "
        _Qry &= vbCrLf & " FROM ( SELECT  FTPayYear, FTPayTerm,FNHSysEmpID,FTFinCode,  FCTotalFinAmt "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin "
        _Qry &= vbCrLf & " WHERE FTPayYear ='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "'  "
        '_Qry &= vbCrLf & "   AND  FTPayTerm ='" & HI.UL.ULF.rpQuoted(Me.FTPayTerm.Text) & "'  "
        _Qry &= vbCrLf & " ) D "
        _Qry &= vbCrLf & " PIVOT ( MAX(FCTotalFinAmt) For FTFinCode in ([001], [009],[012], [016],[032],[033],[034],[035],"
        _Qry &= vbCrLf & " [036],[043],[106],[108],[109],[110],[111],[112],[113],[002],[017],[008],[014] ,[050],[011], "
        _Qry &= vbCrLf & " [051] , [053], [054], [055], [056], [057], [058], [059], [060], [061], [062], [063], [064], [080], [081]) "
        _Qry &= vbCrLf & " ) piv) AS V_Fin ON V_Fin.FNHSysEmpID=M.FNHSysEmpID And V_Fin.FTPayYear=P.FTPayYear And V_Fin.FTPayTerm=P.FTPayTerm "
        _Qry &= vbCrLf & "  WHERE (M.FTEmpCode <> '') AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID
        _Qry &= vbCrLf & "   AND  P.FTPayYear ='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "'  "
        '_Qry &= vbCrLf & "   AND  P.FTPayTerm ='" & HI.UL.ULF.rpQuoted(Me.FTPayTerm.Text) & "'  "

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

        _Qry &= vbCrLf & " AND P.FTPayTerm in (SELECT DISTINCT PDT.FTPayTerm as PayTerm from HITECH_HR.dbo.THRMCfgPayDT AS PDT "

        ''_Qry &= vbCrLf & " JOIN HITECH_MASTER.dbo.THRMEmpType AS ET WITH(NOLOCK) ON PDT.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "

        _Qry &= vbCrLf & " where FTPayYear = '" & HI.UL.ULF.rpQuoted(FTPayYear.Text) & "'  AND PDT.FNHSysEmpTypeId = M.FNHSysEmpTypeId"
        _Qry &= vbCrLf & " AND PDT.FNMonth = " & (FNMonth.SelectedIndex + 1) & ")"

        _Qry &= vbCrLf & " ) AS D GROUP BY FTEmpCode,FTEmpName,FTEmpStatusName,FTEmpTypeName,FTDeptName,FTDivisonName,"
        _Qry &= vbCrLf & "FTSectName,FTUnitSectName,FTPositName,FTAccountGroupCode,"
        _Qry &= vbCrLf & "FTAccountGroupName,FTEmpTypeCode,FTDeptCode,FTDivisonCode,FTSectCode,FTUnitSectCode,FTPositCode,"
        _Qry &= vbCrLf & "FNEmpStatus,FTPayYear,FNHSysEmpID,FTEmpIdNo,"
        _Qry &= vbCrLf & "FTStateHRSent,FTUserHRSent,FTTimeHRSent,FTStateEmployeeAccept,FTUserEmployeeAccept,"
        _Qry &= vbCrLf & "FTDateEmployeeAccept,FTTimeEmployeeAccept,FTStateHRAccept,FTUseHRAccept,FTDateHRAccept,FTTimeHRAccept"
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

        If Me.FTPayYear.Text <> "" And FTPayYear.Text.Length = 4 Then
            If Me.FNMonth.Text <> "" Then
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

    Private Sub ochkselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ochkselectall.CheckedChanged
        Try



            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With ogc
                If Not (.DataSource Is Nothing) And ogv.RowCount > 0 Then

                    With ogv
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTStateHRSent"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmSendApprove_Click(sender As Object, e As EventArgs) Handles ocmSendApprove.Click
        Try
            Dim _dt As DataTable
            With CType(ogc.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy()
            End With

            Dim _FNHSysEmpID As Integer = 0
            Dim _FTPayYear As String = ""
            Dim _FTPayTerm As String = ""
            Dim _Qry As String = ""

            For Each R As DataRow In _dt.Select("FTStateHRSent='1'")

                '_EmpCountCalincentive = 0
                '_CalDate = HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString)

                _FNHSysEmpID = Val(R!FNHSysEmpID.ToString)

                _FTPayYear = R!FTPayYear.ToString
                _FTPayTerm = R!FTPayTerm.ToString




                _Qry = ""
                _Qry = " UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTPayroll set "


                _Qry &= vbCrLf & " FTStateHRSent = '1'"
                _Qry &= vbCrLf & " ,FTUserHRSent= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ,FTDateHRSent= " & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & " , FTTimeHRSent=" & HI.UL.ULDate.FormatTimeDB & ""

                _Qry &= vbCrLf & " WHERE FNHSysEmpID='" & Val(_FNHSysEmpID) & ""
                _Qry &= vbCrLf & " AND FTPayYear = '" & HI.UL.ULF.rpQuoted(_FTPayYear) & "'"
                _Qry &= vbCrLf & " AND FTPayTerm = '" & HI.UL.ULF.rpQuoted(_FTPayTerm) & "'"


                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then

                End If

            Next
        Catch ex As Exception
        End Try
    End Sub


    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click
        Try

            Dim _dt As DataTable
            With CType(ogc.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy()
            End With

            Dim _FNHSysEmpID As Integer = 0
            Dim _FTPayYear As String = ""
            Dim _FTPayTerm As String = ""
            Dim _Qry As String = ""

            For Each R As DataRow In _dt.Select("FTStateHRSent='1'")

                '_EmpCountCalincentive = 0
                '_CalDate = HI.UL.ULDate.ConvertEnDB(R!FDScanDateOrg.ToString)

                _FNHSysEmpID = Val(R!FNHSysEmpID.ToString)

                _FTPayYear = R!FTPayYear.ToString
                _FTPayTerm = R!FTPayTerm.ToString


                '_Qry &= vbCrLf & " , ISNULL(FTStateEmployeeAccept,'0') as 'FTStateEmployeeAccept'  "
                '_Qry &= vbCrLf & " , ISNULL(FTUserEmployeeAccept,'') as 'FTUserEmployeeAccept'  "
                '_Qry &= vbCrLf & " , ISNULL(FTDateEmployeeAccept,'') as 'FTDateEmployeeAccept'  "
                '_Qry &= vbCrLf & " , ISNULL(FTTimeEmployeeAccept,'') as 'FTTimeEmployeeAccept'  "

                '_Qry &= vbCrLf & " , ISNULL(FTStateHRAccept,'0') as 'FTStateHRAccept'  "
                '_Qry &= vbCrLf & " , ISNULL(FTUseHRAccept,'') as 'FTUseHRAccept'  "
                '_Qry &= vbCrLf & " , ISNULL(FTDateHRAccept,'') as 'FTDateHRAccept'  "
                '_Qry &= vbCrLf & " , ISNULL(FTTimeHRAccept,'') as 'FTTimeHRAccept'  "


                _Qry = ""
                _Qry = " UPDATE " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTPayroll set "

                _Qry &= vbCrLf & " FTStateHRSent = '1' "
                _Qry &= vbCrLf & " ,FTUserHRSent= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ,FTDateHRSent= " & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & " , FTTimeHRSent=" & HI.UL.ULDate.FormatTimeDB & ""

                _Qry &= vbCrLf & " WHERE FNHSysEmpID='" & Val(_FNHSysEmpID) & ""
                _Qry &= vbCrLf & " AND FTPayYear = '" & HI.UL.ULF.rpQuoted(_FTPayYear) & "'"
                _Qry &= vbCrLf & " AND FTPayTerm = '" & HI.UL.ULF.rpQuoted(_FTPayTerm) & "'"


                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then

                End If

            Next

        Catch ex As Exception

        End Try
    End Sub
End Class