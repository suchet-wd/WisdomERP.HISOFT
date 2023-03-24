Imports DevExpress.Data
Imports System.IO

Public Class wWageDailyListing_VT

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _ActualDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

        Me.FNHSysCmpId.Text = "1"
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Me.FNHSysCmpId.Properties.ReadOnly = True
        Me.FNHSysCmpId.Properties.Buttons(0).Visible = False
        Me.FNHSysCmpId.Properties.Buttons(0).Enabled = False

        Call InitGrid()

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()

        _CfgIncentiveAmtDigit = HI.Conn.SQLConn.GetField("SELECT FTCfgData FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].[dbo].[TSESystemConfig] WHERE FTCfgName = 'CfgIncentiveAmtDigit' ", Conn.DB.DataBaseName.DB_SECURITY, "")


        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = "FTEmpCode"
        Dim sFieldSum As String = "FNNetProAmt|FNNetAmt|FNNetPayAmt|FNQAAmt|FNProOT|FNProNormal|FNAmtOT|FNAmtNormal|FNNetPay|FNNetIncen|FNAmt|FNNetPerDay"
        Dim sFieldGrpCount As String = "FTDateTrans"
        Dim sFieldGrpSum As String = "FNNetProAmt|FNNetAmt|FNNetPayAmt|FNQAAmt|FNProOT|FNProNormal|FNAmtOT|FNAmtNormal|FNNetPay|FNNetIncen|FNAmt|FNNetPerDay"

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
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n" + _CfgIncentiveAmtDigit + "}"
                    .Columns(Str).DisplayFormat.FormatString = "{0:n" + _CfgIncentiveAmtDigit + "}"
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
    '            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal  Then
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
        Dim _FTStartCalculateDate As String = HI.UL.ULDate.ConvertEnDB(FDStartDate.Text)
        Dim _FTEndCalculateDate As String = HI.UL.ULDate.ConvertEnDB(FDEndDate.Text)

        _Qry = " SELECT   CASE WHEN  ISDATE(isnull(Prod.FTDateTrans,Bonus.FTDateTrans)) = 1 THEN Convert(varchar(10),Convert(Datetime,isnull(Prod.FTDateTrans,Bonus.FTDateTrans)),103) ELSE '' END As FTDateTrans   , M.FNHSysEmpID, M.FTEmpCode,M.FNHSysEmpTypeId"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & " , PR.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

        Else
            _Qry &= vbCrLf & " , PR.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
        End If

        _Qry &= vbCrLf & " ,ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
        _Qry &= vbCrLf & " ,ISNULL(D.FTDeptCode,'') AS FTDeptCode "
        _Qry &= vbCrLf & " ,ISNULL(Di.FTDivisonCode,'') AS FTDivisonCode "
        _Qry &= vbCrLf & " ,ISNULL(S.FTSectCode,'') AS FTSectCode "
        _Qry &= vbCrLf & " ,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode "
        _Qry &= vbCrLf & " ,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode "
        _Qry &= vbCrLf & " , case when ISNULL(Prod.FNAmtNormal,0) > M.FNSalary then M.FNSalary else ISNULL(Prod.FNAmtNormal,0) end  AS FNAmtNormal "
        _Qry &= vbCrLf & " ,ISNULL(Prod.FNAmtOT,0) AS FNAmtOT "
        _Qry &= vbCrLf & " , case when ISNULL(Prod.FNAmtNormal,0) > M.FNSalary then M.FNSalary else ISNULL(Prod.FNAmtNormal,0) end  +  ISNULL(Prod.FNAmtOT,0)   AS FNNetAmt "
        _Qry &= vbCrLf & " ,ISNULL(Prod.FNProNormal,0) AS FNProNormal "
        _Qry &= vbCrLf & " ,ISNULL(Prod.FNProOT,0) AS FNProOT "
        _Qry &= vbCrLf & " ,ISNULL(Prod.FNProOther,0) AS FNProOther "
        _Qry &= vbCrLf & " ,ISNULL(Prod.FNQAAmt,0) AS FNQAAmt "
        _Qry &= vbCrLf & " ,ISNULL(Prod.FNNetProAmt,0) AS FNNetProAmt "
        _Qry &= vbCrLf & " ,CASE WHEN   ISNULL(Prod.FNNetProAmt,0)  > ISNULL(Prod.FNNetAmt,0) THEN  ISNULL(Prod.FNNetProAmt,0)   ELSE ISNULL(Prod.FNNetAmt,0)   END  AS FNNetPayAmt "

        _Qry &= vbCrLf & " , case when ISNULL(Prod.FNAmtNormal,0) > M.FNSalary then M.FNSalary else ISNULL(Prod.FNAmtNormal,0) end  +  ISNULL(Prod.FNAmtOT,0)    AS FNNetPay "


        ''_Qry &= vbCrLf & " ,Case When   ISNULL(Prod.FNNetProAmt,0)  >  case when ISNULL(Prod.FNAmtNormal,0) > M.FNSalary then M.FNSalary else ISNULL(Prod.FNAmtNormal,0) end  +  ISNULL(Prod.FNAmtOT,0)    Then  (ISNULL(Prod.FNNetProAmt,0) - ( case when ISNULL(Prod.FNAmtNormal,0) > M.FNSalary then M.FNSalary else ISNULL(Prod.FNAmtNormal,0) end  +  ISNULL(Prod.FNAmtOT,0))   )   Else 0   End  As FNIncen  "
        _Qry &= vbCrLf & " ,ISNULL(Prod.FNProNormal,0) AS FNIncen "


        _Qry &= vbCrLf & " ,ISNULL(Prod.FNAmtFixedIncentive,0) As FNIncenGaruntee"

        '_Qry &= vbCrLf & " ,Case When ISNULL(Prod.FNAmtFixedIncentive,0) > 0 Then "
        '_Qry &= vbCrLf & " Case When (Case When   ISNULL(Prod.FNNetProAmt,0)  > ISNULL(Prod.FNNetAmt,0) Then  (ISNULL(Prod.FNNetProAmt,0) -ISNULL(Prod.FNNetAmt,0))   Else 0   End) >ISNULL(Prod.FNAmtFixedIncentive,0) Then (Case When   ISNULL(Prod.FNNetProAmt,0)  > ISNULL(Prod.FNNetAmt,0) Then  (ISNULL(Prod.FNNetProAmt,0) -ISNULL(Prod.FNNetAmt,0))   Else 0   End) Else ISNULL(Prod.FNAmtFixedIncentive,0) End"
        '_Qry &= vbCrLf & " Else (Case When   ISNULL(Prod.FNNetProAmt,0)  >case when ISNULL(Prod.FNAmtNormal,0) > M.FNSalary then M.FNSalary else ISNULL(Prod.FNAmtNormal,0) end  +  ISNULL(Prod.FNAmtOT,0) Then    (ISNULL(Prod.FNNetProAmt,0) - ISNULL(Prod.FNNetAmt,0))    Else 0   End) End  As FNNetIncen "
        _Qry &= vbCrLf & " , ISNULL(Prod.FNProNormal,0) + isnull(Bonus.FNAmt,0)  As FNNetIncen "

        _Qry &= vbCrLf & ",isnull(Bonus.FNAmt,0)  As FNAmt  ,  (Case When ISNULL(Prod.FNAmtFixedIncentive,0) > 0 Then "
        _Qry &= vbCrLf & "Case When (Case When   ISNULL(Prod.FNNetProAmt,0)  > ISNULL(Prod.FNNetAmt,0) Then  (ISNULL(Prod.FNNetProAmt,0) -ISNULL(Prod.FNNetAmt,0))   Else 0   End) >ISNULL(Prod.FNAmtFixedIncentive,0) Then (Case When   ISNULL(Prod.FNNetProAmt,0)  > ISNULL(Prod.FNNetAmt,0) Then  (ISNULL(Prod.FNNetProAmt,0) -ISNULL(Prod.FNNetAmt,0))   Else 0   End) Else ISNULL(Prod.FNAmtFixedIncentive,0) End"
        _Qry &= vbCrLf & " Else (Case When   ISNULL(Prod.FNNetProAmt,0)  > ISNULL(Prod.FNNetAmt,0) Then  (ISNULL(Prod.FNNetProAmt,0) -ISNULL(Prod.FNNetAmt,0))   Else 0   End) End) +ISNULL(Prod.FNNetAmt,0)  +  isnull(Bonus.FNAmt,0)  As FNNetPerDay "


        _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee As M With  (NOLOCK)  LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition As P With (NOLOCK) On M.FNHSysPositId = P.FNHSysPositId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As US With (NOLOCK) On M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType As ET With (NOLOCK) On M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect As S With (NOLOCK) On M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision As Di With (NOLOCK) On M.FNHSysDivisonId = Di.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment As D With (NOLOCK) On M.FNHSysDeptId = D.FNHSysDeptId"
        _Qry &= vbCrLf & "  INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename As PR With (NOLOCK) On M.FNHSysPreNameId = PR.FNHSysPreNameId"
        _Qry &= vbCrLf & "  LEFT JOIN  (Select * From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily With(NOLOCK) where   FTDateTrans>='" & _FTStartCalculateDate & "'  AND FTDateTrans<='" & _FTEndCalculateDate & "'  ) AS Prod  ON M.FNHSysEmpID = Prod.FNHSysEmpID"
        _Qry &= vbCrLf & "  LEFT JOIN  (Select * From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily_Bonus WITH(NOLOCK)     where   FTDateTrans>='" & _FTStartCalculateDate & "'  AND FTDateTrans<='" & _FTEndCalculateDate & "'  ) AS Bonus  ON M.FNHSysEmpID = Bonus.FNHSysEmpID"
        _Qry &= vbCrLf & "    and Prod.FTDateTrans = Bonus.FTDateTrans "
        _Qry &= vbCrLf & "  WHERE  M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & "  "
        _Qry &= vbCrLf & " And M.FDDateStart <='" & _FTEndCalculateDate & "' "
        _Qry &= vbCrLf & " AND (M.FDDateEnd ='' OR M.FDDateEnd >'" & _FTEndCalculateDate & "' )   "
        _Qry &= vbCrLf & " AND  ET.FNInsurType <>0"
        '_Qry &= vbCrLf & " AND  FTDateTrans>='" & _FTStartCalculateDate & "'  AND FTDateTrans<='" & _FTEndCalculateDate & "' "
        '_Qry &= vbCrLf & " AND  Bonus.FTDateTrans>='" & _FTStartCalculateDate & "'  AND Bonus.FTDateTrans<='" & _FTEndCalculateDate & "' "

        _Qry = _Qry & HI.ST.Security.PermissionFilterEmployeeSalary()

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
            _Qry &= vbCrLf & " AND  D.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
        End If

        If Me.FNHSysDeptIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  D.FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
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
            _Qry &= vbCrLf & " AND  S.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
        End If

        If Me.FNHSysSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  S.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
        End If

        '------Criteria Unit Sect
        If Me.FNHSysUnitSectId.Text <> "" Then
            _Qry &= vbCrLf & " AND   US.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
        End If

        If Me.FNHSysUnitSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND   US.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
        End If

        _Qry &= vbCrLf & " ORDER BY  M.FTEmpCode  "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.ogdtime.DataSource = _dt
        Me.ogvtime.BestFitColumns()
        ogvtime.ExpandAllGroups()
        _Spls.Close()

        _RowDataChange = False

    End Sub

#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.FNHSysCmpId.Text = "1"
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)
            StateCal = False


        Catch ex As Exception
        End Try
    End Sub
    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If Me.FDStartDate.Text <> "" Then
            If Me.FDEndDate.Text <> "" Then
                Call LoadData()
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDEndDate_lbl.Text)
                FDEndDate.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateRequest_lbl.Text)
            FDStartDate.Focus()
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

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        With Me.ogvtime

            Dim _Spls As New HI.TL.SplashScreen("Generating data...   Please Wait  ")
            Me.LoadData()
            _Spls.Close()

            Dim _Qry As String = ""

            _Qry &= "   {THRMEmployee.FNHSysCmpId}=" & HI.ST.SysInfo.CmpID & ""

            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & "AND  {V_SMP.FTEmpTypeCode}='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "'"
            End If

            '------Criteria By Employeee Code
            If Me.FDStartDate.Text <> "" Then
                _Qry &= vbCrLf & "AND  {V_SMP.FTDate}>='" & HI.UL.ULDate.ConvertEnDB(FDStartDate.Text) & "'"
            End If
            If Me.FDEndDate.Text <> "" Then
                _Qry &= vbCrLf & "AND  {V_SMP.FTDate}<='" & HI.UL.ULDate.ConvertEnDB(FDEndDate.Text) & "'"
            End If
            If Me.FNHSysEmpId.Text <> "" Then
                _Qry &= vbCrLf & " AND {V_SMP.FTEmpCode} >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
            End If

            If Me.FNHSysEmpIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND {V_SMP.FTEmpCode} <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
            End If

            '------Criteria By Department
            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & " AND  {V_SMP.FTDeptCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
            End If

            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  {V_SMP.FTDeptCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
            End If

            '------Criteria By Division
            If Me.FNHSysDivisonId.Text <> "" Then
                _Qry &= vbCrLf & " AND  {V_SMP.FTDivisonCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
            End If

            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  {V_SMP.FTDivisonCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
            End If

            '------Criteria By Sect
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND  {V_SMP.FTSectCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
            End If

            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  {V_SMP.FTSectCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
            End If

            '------Criteria Unit Sect
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND   {V_SMP.FTUnitSectCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
            End If

            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND   {V_SMP.FTUnitSectCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
            End If



            With New HI.RP.Report

                If Me.FDStartDate.Text <> "" Then
                    .AddParameter("SFTDate", Me.FDStartDate.Text)
                End If

                If Me.FDEndDate.Text <> "" Then
                    .AddParameter("EFTDate", Me.FDEndDate.Text)
                End If

                .FormTitle = Me.Text
                .ReportFolderName = "Human Report\"
                .ReportName = "RptSample.rpt"
                .Formular = _Qry
                .Preview()
            End With

        End With
    End Sub


End Class