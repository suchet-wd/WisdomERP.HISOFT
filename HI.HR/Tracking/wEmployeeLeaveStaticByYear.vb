Public Class wEmployeeLeaveStaticByYear

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
#Region "Procedure"
    Private Function LoadDataInfo(Spls As HI.TL.SplashScreen) As Boolean
        Dim _Qry As String = ""

        Dim _FTYear As String = Microsoft.VisualBasic.Left(HI.UL.ULDate.ConvertEnDB(Me.FTDateStart.Text), 4)


        _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_CALCULATE_STATIC_EMPLEAVE_YEAR '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & _FTYear & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTDateStart.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTDateEnd.Text) & "' , " & Val(HI.ST.SysInfo.CmpID)
        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        '_Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_CALCULATE_EMPLEAVE_RETVACATION_YEAR '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & _FTYear & "','" & _FTYear & "/01/01" & "','" & _FTYear & "/12/31" & "'"
        'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        _Qry = "  SELECT       M.FNHSysEmpID, M.FTEmpCode"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"
            _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameTH  AS FTEmpTypeName "
            _Qry &= vbCrLf & "  ,Dept.FTDeptDescTH  AS FTDeptName "
            _Qry &= vbCrLf & "  ,DI.FTDivisonNameTH  AS FTDivisonName "
            _Qry &= vbCrLf & "  ,ST.FTSectNameTH  AS FTSectName "
            _Qry &= vbCrLf & "  ,US.FTUnitSectNameTH  AS FTUnitSectName "
            _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameTH  AS FTPositName "
            _Qry &= vbCrLf & "   ,Convert(varchar(30),Convert(numeric(18,0),(dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd) -(dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd) % 12) )  /12)) + ' ปี '"
            _Qry &= vbCrLf & "   + Convert(varchar(30),Convert(numeric(18,0),(dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd) % 12  )) ) + ' เดือน' AS FTWorkAge "
        Else
            _Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpName"
            _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameEN  AS FTEmpTypeName "
            _Qry &= vbCrLf & "  ,Dept.FTDeptDescEN  AS FTDeptName "
            _Qry &= vbCrLf & "  ,DI.FTDivisonNameEN  AS FTDivisonName "
            _Qry &= vbCrLf & "  ,ST.FTSectNameEN  AS FTSectName "
            _Qry &= vbCrLf & "  ,US.FTUnitSectNameEN  AS FTUnitSectName "
            _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameEN  AS FTPositName "
            _Qry &= vbCrLf & "   ,Convert(varchar(30),Convert(numeric(18,0),(dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd) -(dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd) % 12) )  /12)) + ' Year '"
            _Qry &= vbCrLf & "   + Convert(varchar(30),Convert(numeric(18,0),(dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd) % 12  )) ) + ' Month' AS FTWorkAge "
        End If

        _Qry &= vbCrLf & ", ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
        _Qry &= vbCrLf & ", ISNULL(Dept.FTDeptCode,'') AS FTDeptCode, ISNULL(DI.FTDivisonCode,'') AS FTDivisonCode"
        _Qry &= vbCrLf & ", ISNULL(ST.FTSectCode,'') AS FTSectCode,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode"
        _Qry &= vbCrLf & ", OrgPosit.FTPositCode"

        _Qry &= vbCrLf & " ,CASE WHEN ISDATE(M.FDDateStart) =1 THEN  CONVERT(varchar(10),Convert(datetime,M.FDDateStart),103) ELSE '' END AS FDDateStart"
        _Qry &= vbCrLf & " ,MST.FNTotalWorkDayInYear  AS FNTotalWorkDayInYear"
        _Qry &= vbCrLf & " ,MST.FNTimeMinTime  AS FNWorkingDay"
        _Qry &= vbCrLf & ", MST.FNLateNormalMinTime  AS FNLateNormalMin"
        _Qry &= vbCrLf & " ,MST.FNLateNormalMinCutPoint"
        _Qry &= vbCrLf & ", MST.FNLateNormalMin  AS FNLateNormalMinTotal"
        _Qry &= vbCrLf & " ,MST.FNAbsentTime AS FNAbsent"
        _Qry &= vbCrLf & " ,MST.FNAbsentCutPoint"
        _Qry &= vbCrLf & " ,MST.FNAbsent AS FNAbsentTimeTotalMin"
        _Qry &= vbCrLf & " ,MST.FNLeaveSickTime  AS FNLeaveSick"
        _Qry &= vbCrLf & "  ,MST.FNLeaveSickCutPoint"

        _Qry &= vbCrLf & "  ,MST.FNLeaveBusunessTime  AS FNLeaveBusuness"
        _Qry &= vbCrLf & " ,MST.FNLeaveBusunessCutPoint"

        _Qry &= vbCrLf & "  ,MST.FNLeaveVacationTime  AS FNLeaveVacation"
        _Qry &= vbCrLf & " ,MST.FNLeaveVacationCutPoint"

        _Qry &= vbCrLf & "  ,MST.FNLeavePragnentTime AS FNLeavePragnent"
        _Qry &= vbCrLf & " ,MST.FNLeavePragnentCutPoint"

        _Qry &= vbCrLf & "  ,MST.FNLeavePaternityTime AS FNLeavePaternity "
        _Qry &= vbCrLf & " ,MST.FNLeavePaternityCutPoint"


        _Qry &= vbCrLf & "  ,MST.FNLeaveMatriculateTime  AS FNLeaveMatriculate"
        _Qry &= vbCrLf & " ,MST.FNLeaveMatriculateCutPoint"

        _Qry &= vbCrLf & " ,MST.FNLeaveOtherTime  AS FNLeaveOther"

        _Qry &= vbCrLf & " ,MST.FNLeaveOtherCutPoint"
        _Qry &= vbCrLf & " ,MST.TotalPointCut"
        _Qry &= vbCrLf & " ,MST.TotalPoint"
        _Qry &= vbCrLf & " ,MST.FTEveliatePer"
        _Qry &= vbCrLf & " ,MST.TotalPointBal"

        _Qry &= vbCrLf & " ,   Replace(Convert(varchar(30),SH.FTIn1),'.',':') AS FTStartTimeShift"

        _Qry &= vbCrLf & "  , ( Convert(varchar(30),Convert(numeric(18,0),Floor((MST.FNEmpVacation * 480) / 480.00)))"
        _Qry &= vbCrLf & " +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((MST.FNEmpVacation * 480)   % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((MST.FNEmpVacation * 480)   % 480.00) % 60.00))),2))  AS  FNEmpVacation  "

        _Qry &= vbCrLf & ", CASE WHEN isnull(M.FDDateProbation,'') <> '' AND M.FDDateProbation < '" & HI.UL.ULDate.ConvertEnDB(Me.FTDateEnd.Text) & "' Then "
        _Qry &= vbCrLf & "   ( Convert(varchar(30),Convert(numeric(18,0),Floor((MST.FNEmpVacation * 480) / 480.00)))"
        _Qry &= vbCrLf & " +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((MST.FNEmpVacation * 480)   % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((MST.FNEmpVacation * 480)   % 480.00) % 60.00))),2))    "
        _Qry &= vbCrLf & " ELSE '0:00:00' END AS  FNEmpVacationRight "

        _Qry &= vbCrLf & " ,( Convert(varchar(30),Convert(numeric(18,0),Floor(MST.FNLeaveVacation / 480.00))) "
        _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((MST.FNLeaveVacation   % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((MST.FNLeaveVacation   % 480.00) % 60.00))),2))  AS  FNEmpVacationUsed "

        _Qry &= vbCrLf & ",( Convert(varchar(30),Convert(numeric(18,0),Floor(( (MST.FNEmpVacation - (CONVERT(numeric(18, 2), MST.FNLeaveVacation / 480.00) )) * 480 ) / 480.00)))"
        _Qry &= vbCrLf & " +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((( (MST.FNEmpVacation - (CONVERT(numeric(18, 2), MST.FNLeaveVacation / 480.00) )) * 480 )   % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & " +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((( (MST.FNEmpVacation - (CONVERT(numeric(18, 2), MST.FNLeaveVacation / 480.00) )) * 480 )   % 480.00) % 60.00))),2))  AS  FNEmpVacationBal 	"

        _Qry &= vbCrLf & ", MST.FNLeaveBusinessRegulationTime  "
        _Qry &= vbCrLf & ", MST.FNLeaveBusinessRegulationCovidTime  "
        _Qry &= vbCrLf & ", MST.FNLeaveMarriesTime  "
        _Qry &= vbCrLf & ", MST.FNLeaveNotPaidTime  "
        _Qry &= vbCrLf & ", MST.FNLeaveMilitaryTime  "
        _Qry &= vbCrLf & ", MST.FNLeaveAccidentOnWorkTime  "
        _Qry &= vbCrLf & ", MST.FNLeaveQuarantineCovidPaidTime  "
        _Qry &= vbCrLf & ", MST.FNLeaveQuarantineCovidTime  "
        _Qry &= vbCrLf & ", MST.FNLeaveVaccinationEffectTime  "
        _Qry &= vbCrLf & ", MST.FNLeaveSuspendTime  "
        _Qry &= vbCrLf & ", MST.FNLeaveSpecialTime  "
        _Qry &= vbCrLf & ", MST.FNLeaveSterilizationTime  "





        _Qry &= vbCrLf & " FROM "
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M  WITH (NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.TRPTTempEmpStaticByYear AS MST  WITH (NOLOCK)   ON M.FNHSysEmpID=MST.FNHSysEmpID INNER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS OrgPosit WITH (NOLOCK) ON M.FNHSysPositId = OrgPosit.FNHSysPositId  LEFT OUTER JOIN "
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS SH WITH(NOLOCK) ON M.FNHSysShiftID = SH.FNHSysShiftID "


        _Qry &= vbCrLf & " WHERE  M.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " And MST.FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "


        If Me.FNHSysEmpTypeId.Text <> "" Then
            _Qry &= vbCrLf & "AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "'"
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
            _Qry &= vbCrLf & " AND  DI.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
        End If

        If Me.FNHSysDivisonIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  DI.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
        End If

        '------Criteria By Sect
        If Me.FNHSysSectId.Text <> "" Then
            _Qry &= vbCrLf & " AND  ST.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
        End If

        If Me.FNHSysSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  ST.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
        End If

        '------Criteria Unit Sect
        If Me.FNHSysUnitSectId.Text <> "" Then
            _Qry &= vbCrLf & " AND   US.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
        End If

        If Me.FNHSysUnitSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND   US.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
        End If

        _Qry = vbCrLf & "" & HI.ST.Security.PermissionFilterEmployee(_Qry) & ""

        _Qry &= vbCrLf & " ORDER BY M.FTEmpCode "

        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        With Me.ogc
            .DataSource = _dt.Copy
            ogv.ExpandAllGroups()
            ogv.RefreshData()
        End With

        With Me.ogctime
            .DataSource = _dt.Copy
            ogvtime.ExpandAllGroups()
            ogvtime.RefreshData()
        End With

        _dt.Dispose()

        Return True
    End Function
#End Region

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        With ogv


            '.Columns("FTEmpTypeCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTEmpTypeCode")
            '.Columns("FTDeptCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTDeptCode")
            '.Columns("FTDivisonCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTDivisonCode")
            '.Columns("FTSectCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTSectCode")
            '.Columns("FTUnitSectCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTUnitSectCode")

            '.Columns("FTEmpTypeCode").Group()
            '.Columns("FTSectCode").Group()
            '.Columns("FTEmpStatusName").Group()

            .Columns("FTEmpCode").Summary.Add(DevExpress.Data.SummaryItemType.Count, "FTEmpCode")
            .Columns("FTEmpCode").SummaryItem.DisplayFormat = "{0:n0}"
            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "FTEmpCode", Nothing, "(Count by " & .Columns.ColumnByFieldName("FTEmpCode").Caption & "={0:n0})")
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

#Region "General"
    Private Sub wEmployeeLeaveOfYear_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Call InitGrid()

        HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)

    End Sub

    Private Function VerifyData() As Boolean
        If Me.FTDateStart.Text <> "" And Me.FTDateEnd.Text <> "" Then
            If Microsoft.VisualBasic.Right(Me.FTDateStart.Text, 4) = Microsoft.VisualBasic.Right(Me.FTDateEnd.Text, 4) Then
                Return True
            Else
                HI.MG.ShowMsg.mInfo("วันที่เริ่มต้นและวันที่สิ้นสุด ไม่ได้อยู่ในปีเดียวกันกรุณาทำการตรวจสอบ !!!", 15062655487, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                Return False
            End If
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการระบุวันที่เริ่มต้นแล้ะวันที่สิ้นสุด !!!", 15062655488, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click


        HI.TL.HandlerControl.ClearControl(Me)
        'HI.TL.HandlerControl.ClearControl(ogc)
        ogc.DataSource = Nothing
    End Sub

    Private Sub ocmload_Click(sender As System.Object, e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then
            Dim _Spls As New HI.TL.SplashScreen("Loading data...   Please Wait  ")
            Me.LoadDataInfo(_Spls)
            Me.otbmain.SelectedTabPageIndex = 0
            _Spls.Close()
        End If

    End Sub

    Private Sub ogv_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogv.RowCellStyle
        With ogv
            If .GetRowCellValue(e.RowHandle, "FNEmpStatus") = "2" Then
                e.Appearance.ForeColor = Drawing.Color.Red
            End If
        End With
    End Sub


#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogv)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        With Me.ogvtime

            Dim _Spls As New HI.TL.SplashScreen("Generating data...   Please Wait  ")
            Me.LoadDataInfo(_Spls)
            Me.otbmain.SelectedTabPageIndex = 0
            _Spls.Close()

            Dim _Qry As String = ""

            _Qry = " {TRPTTempEmpStaticByYear.FTUserLogin} = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & " AND  {THRMEmployee.FNHSysCmpId}=" & HI.ST.SysInfo.CmpID & ""

            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & "AND  {THRMEmpType.FTEmpTypeCode}='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "'"
            End If

            '------Criteria By Employeee Code
            If Me.FNHSysEmpId.Text <> "" Then
                _Qry &= vbCrLf & " AND {THRMEmployee.FTEmpCode} >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
            End If

            If Me.FNHSysEmpIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND {THRMEmployee.FTEmpCode} <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
            End If

            '------Criteria By Department
            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & " AND  {TCNMDepartment.FTDeptCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
            End If

            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  {TCNMDepartment.FTDeptCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
            End If

            '------Criteria By Division
            If Me.FNHSysDivisonId.Text <> "" Then
                _Qry &= vbCrLf & " AND  {TCNMDivision.FTDivisonCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
            End If

            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  {TCNMDivision.FTDivisonCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
            End If

            '------Criteria By Sect
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND  {TCNMSect.FTSectCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
            End If

            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  {TCNMSect.FTSectCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
            End If

            '------Criteria Unit Sect
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND   {TCNMUnitSect.FTUnitSectCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
            End If

            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND   {TCNMUnitSect.FTUnitSectCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
            End If

            With Me.ogv

                If .RowCount > 0 Then

                    Dim _AllEmpCode As String = ""

                    For I As Integer = 0 To .RowCount - 1

                        If _AllEmpCode = "" Then
                            _AllEmpCode = "" & .GetRowCellValue(I, "FTEmpCode").ToString
                        Else
                            _AllEmpCode = _AllEmpCode & "','" & .GetRowCellValue(I, "FTEmpCode").ToString
                        End If

                    Next

                    _Qry &= vbCrLf & " AND {THRMEmployee.FTEmpCode}  IN ['" & _AllEmpCode & "'] "

                End If

            End With

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Human Report\"
                .ReportName = "EmployeeEvaluateByYear.rpt"
                .Formular = _Qry
                .Preview()
            End With

        End With
    End Sub
End Class