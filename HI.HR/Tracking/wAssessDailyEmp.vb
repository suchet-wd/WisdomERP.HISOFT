Imports DevExpress.XtraGrid.Columns
Public Class wAssessDailyEmp
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Call LoadAsess()
        Call LoadAsess2Default()
        Call SwitchTab()
        Call LoadDataAssDefault()
    End Sub

    Private row As Integer : Private Score As Integer : Private Multi As Integer
    Private _valueSick As Integer = 0
    Private _valueBusi As Integer = 0
    Private _valueAbsent As Integer = 0
    Private _valueLate As Double = 0
    Private Sum As Double = 0
    Private Total As Double = 0
    Private chkStg As Boolean = False
    Private RowPositOgcName As Integer
    Private stgClick As Integer
    Private _Save As Boolean = False
    Private _data As Boolean = False
    Private _StateFocus As Boolean = False
    Private _FormLoad As Boolean = False

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

    Private Function LoadData() As Boolean
        Dim _Qry As String = ""
        Dim _dt As DataTable



        _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_CALCULATE_STATIC_EMPLEAVE_Pro '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTDateStart.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTDateEnd.Text) & "'"
        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        _Qry = "  SELECT       '0' as FTSelect,M.FNHSysEmpID, M.FTEmpCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"

        Else
            _Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpName"

        End If

        _Qry &= vbCrLf & ", ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
        _Qry &= vbCrLf & ", ISNULL(Dept.FTDeptCode,'') AS FTDeptCode, ISNULL(DI.FTDivisonCode,'') AS FTDivisonCode"
        _Qry &= vbCrLf & ", ISNULL(ST.FTSectCode,'') AS FTSectCode,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode"
        _Qry &= vbCrLf & ", ISNULL(OrgPosit.FTPositCode,'') AS FTPositCode"

        _Qry &= vbCrLf & " ,CASE WHEN ISDATE(M.FDDateStart) =1 THEN  CONVERT(varchar(10),Convert(datetime,M.FDDateStart),103) ELSE '' END AS FDDateStart"
        _Qry &= vbCrLf & " ,CASE WHEN ISDATE(M.FDDateProbation)=1 THEN CONVERT(varchar(10),Convert(datetime,M.FDDateProbation),103) ELSE '' END AS FDDateProbation"
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

        _Qry &= vbCrLf & "  ,MST.FNLeaveMatriculateTime  AS FNLeaveMatriculate"
        _Qry &= vbCrLf & " ,MST.FNLeaveMatriculateCutPoint"

        _Qry &= vbCrLf & " ,MST.FNLeaveOtherTime  AS FNLeaveOther"

        _Qry &= vbCrLf & " ,MST.FNLeaveOtherCutPoint"
        _Qry &= vbCrLf & " ,MST.TotalPointCut"
        _Qry &= vbCrLf & " ,MST.TotalPoint"
        _Qry &= vbCrLf & " ,MST.TotalPointBal"

        _Qry &= vbCrLf & " FROM "
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M  WITH (NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.TRPTTempEmpStaticByYear AS MST  WITH (NOLOCK)   ON M.FNHSysEmpID=MST.FNHSysEmpID INNER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS OrgPosit WITH (NOLOCK) ON M.FNHSysPositId = OrgPosit.FNHSysPositId "


        _Qry &= vbCrLf & " WHERE  M.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND MST.FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

        '=====Crireria by EmpType
        If Me.FNHSysEmpTypeId.Text <> "" Then
            _Qry &= vbCrLf & " AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "'"
        End If

        '===== Criteria by DatePass ProStart
        If Me.FTDateStart.Text <> "" Then
            _Qry &= vbCrLf & " AND M.FDDateProbation>= '" & UL.ULDate.ConvertEnDB(Me.FTDateStart.Text) & "'"
        End If

        '===== Criteria by DatePass ProStart
        If Me.FTDateEnd.Text <> "" Then
            _Qry &= vbCrLf & " AND M.FDDateProbation<= '" & UL.ULDate.ConvertEnDB(Me.FTDateEnd.Text) & "'"
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

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.ogcDetail.DataSource = _dt.Copy
        Me.ogcSumry.DataSource = _dt.Copy
        Me.ogcName.DataSource = _dt.Copy
        stgClick = 0
        Return True
    End Function

    Private Function CheckCriteria() As Boolean
        Dim _Pass As Boolean = False
        'If Me.FTDateStart.Text <> "" And Me.FTDateEnd.Text <> "" Then
        '    If FNHSysEmpTypeId.Text <> "" Then
        '        Return True
        '    Else
        '        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.lb_EmpType.Text)
        '        Me.FNHSysEmpTypeId.Focus()
        '        Return False
        '    End If
        'Else
        '    HI.MG.ShowMsg.mInfo("กรุณาเลือกวันที่เริ่มต้นและวันที่สิ้นสุด !!!", 1602041429, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        '    Return False
        'End If
        'Modify byjoker 2017/03/29 17.01
        If Me.FTDateStart.Text <> "" Then
            If Me.FTDateEnd.Text <> "" Then
                _Pass = True
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTDateEnd_lb.Text)
                Me.FTDateEnd.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTDateStart_lb.Text)
            Me.FTDateStart.Focus()
        End If
        Return _Pass
    End Function

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If CheckCriteria() Then
            Dim Spls As New HI.TL.SplashScreen("Please Wait Loading data....")
            If LoadData() Then
                Spls.Close()
                Me.otbmain.SelectedTabPageIndex = 0
            Else
                Spls.Close()
                Me.otbmain.SelectedTabPageIndex = 0
            End If
        End If
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        ogcDetail.DataSource = Nothing
        ogcSumry.DataSource = Nothing
        ogcName.DataSource = Nothing
        Me.otbmain.SelectedTabPageIndex = 0

        With bgvAsess
            For i = 0 To .RowCount - 1
                .SetRowCellValue(i, "VeryGood", "0")
                .SetRowCellValue(i, "Good", "0")
                .SetRowCellValue(i, "Fair", "0")
                .SetRowCellValue(i, "Poor", "0")
                .SetRowCellValue(i, "Score", "0")
            Next
        End With
        With ogvAsess2
            For i = 0 To .RowCount - 1
                .SetRowCellValue(i, "FNLeaveSick", "0")
                .SetRowCellValue(i, "FNLeaveBusi", "0")
                .SetRowCellValue(i, "FNLate", "0")
                .SetRowCellValue(i, "FNAbsent", "0")
                .SetRowCellValue(i, "FNScore", "0")
            Next
        End With

    End Sub

    Private Sub wAssessDailyEmp_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Me.FNHSysEmpTypeId.Text = "D"
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Call setGrid()
        _FormLoad = True
        RemoveHandler bgvAsess.DoubleClick, AddressOf HI.TL.HandlerControl.GridView_DoubleClick

    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Try
            If ogvDetail.RowCount <= 0 Then Exit Sub
            If ogvName.RowCount <= 0 Then Exit Sub
            Dim EmpCode As String = ""
            Dim _Qry As String = ""

            If (otbmain.SelectedTabPage.Name = otpevaluate.Name) Then
                With CType(ogcDetail.DataSource, DataTable)
                    For Each R As DataRow In .Select("FTSelect='1'")
                        If EmpCode = "" Then
                            EmpCode = "{V_EvaluateByPro.FTEmpCode}='" & R!FTEmpCode.ToString & "'"
                        Else
                            EmpCode += " or {V_EvaluateByPro.FTEmpCode}='" & R!FTEmpCode.ToString & "'"
                        End If
                    Next
                End With
            ElseIf (otbmain.SelectedTabPage.Name = otpAsess.Name) Then
                With CType(ogcName.DataSource, DataTable)
                    For Each R As DataRow In .Select("FTSelect='1'")
                        If EmpCode = "" Then
                            EmpCode = "{V_EvaluateByPro.FTEmpCode}='" & R!FTEmpCode.ToString & "'"
                        Else
                            EmpCode += " or {V_EvaluateByPro.FTEmpCode}='" & R!FTEmpCode.ToString & "'"
                        End If
                    Next
                End With
            End If
            If EmpCode <> "" Then
                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .ReportFolderName = "Human Report\"
                    .ReportName = "employeeEvaluateByPro.rpt"
                    .Formular = EmpCode
                    .Preview()
                End With
            End If
        Catch ex As Exception

        End Try

    End Sub

    'Private Sub LoadAsess()
    '    Dim Qry As String = ""
    '    Dim dt As DataTable
    '    'Dim dt2 As DataTable = getDatatable()

    '    Qry = "select Seq,Condition,Multi,'0' as VeryGood,'0' as Good,'0' as Fair,'0' as Poor,'' as Score from"
    '    Qry &= vbCrLf & "(select FNListIndex as Seq,FTNameTH as Condition from"
    '    Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData"
    '    Qry &= vbCrLf & "where FTListName='FNAsess'"
    '    Qry &= vbCrLf & ") AS A LEFT OUTER JOIN"
    '    Qry &= vbCrLf & "(select FTNameTH AS Multi,FNListIndex from"
    '    Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData"
    '    Qry &= vbCrLf & "where FTlistName='FNMulti'"
    '    Qry &= vbCrLf & ")AS B ON A.Seq=B.FNListIndex"
    '    dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_SYSTEM)
    '    ogcAsess.DataSource = dt

    'End Sub

    Private Sub LoadAsess2Default()
        Dim Qry As String = ""
        Dim dt As DataTable

        Qry = "select M.FNSeq as Seq,'0' as FNLeaveSick,'0' as FNLeaveBusi,'0' as FNAbsent,'0' as FNLate,'0' as FNScore "
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            Qry &= vbCrLf & ",M.FTEvaluateNameTH as Condition from"
        Else
            Qry &= vbCrLf & ",M.FTEvaluateNameEN as Condition from"
        End If
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEvaluateMaster AS M WITH(NOLOCK)"
        Qry &= vbCrLf & "where M.FNHsysEvaluateID=1602269999"
        dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)
        ogcAsess2.DataSource = dt.Copy

    End Sub

    Private Function LoadDataAss2(id As Integer)
        Dim Qry As String = ""
        Dim dt As DataTable

        Dim s As Integer
        Dim b As Integer
        Dim l As Integer
        Dim a As Integer

        Qry = "select xx.FNAbsent,xx.FNLate,xx.FNLeaveBusi,xx.FNLeaveSick from"
        Qry &= vbCrLf & "(select"
        Qry &= vbCrLf & "(SELECT  count(FNLateNormalMin)"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrans AS T WITH(NOLOCK)"
        Qry &= vbCrLf & "WHERE T.FNHSysEmpID = A.FNHSysEmpID"
        Qry &= vbCrLf & "AND T.FTDateTrans >=A.FDDateStart"
        Qry &= vbCrLf & "AND T.FTDateTrans < convert(varchar(10),getdate(),111)"
        Qry &= vbCrLf & "AND T.FNLateNormalMin>0"
        Qry &= vbCrLf & ") AS FNLate"

        Qry &= vbCrLf & ",(SELECT  count(FNAbsent)"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrans AS T WITH(NOLOCK)"
        Qry &= vbCrLf & "WHERE T.FNHSysEmpID = A.FNHSysEmpID"
        Qry &= vbCrLf & "AND T.FTDateTrans >=A.FDDateStart"
        Qry &= vbCrLf & "AND T.FTDateTrans < convert(varchar(10),getdate(),111)"
        Qry &= vbCrLf & "AND T.FNAbsent>0"
        Qry &= vbCrLf & ") AS FNAbsent"

        Qry &= vbCrLf & ",(SELECT  count(FTLeaveType)"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS T WITH(NOLOCK)"
        Qry &= vbCrLf & "WHERE T.FNHSysEmpID = A.FNHSysEmpID"
        Qry &= vbCrLf & "AND T.FTDateTrans >=A.FDDateStart"
        Qry &= vbCrLf & "AND T.FTDateTrans < convert(varchar(10),getdate(),111)"
        Qry &= vbCrLf & "AND FTLeaveType='0'"
        Qry &= vbCrLf & ") AS FNLeaveSick"

        Qry &= vbCrLf & ",(SELECT  count(FTLeaveType)"
        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS T WITH(NOLOCK)"
        Qry &= vbCrLf & "WHERE T.FNHSysEmpID = A.FNHSysEmpID"
        Qry &= vbCrLf & "AND T.FTDateTrans >=A.FDDateStart"
        Qry &= vbCrLf & "AND T.FTDateTrans < convert(varchar(10),getdate(),111)"
        Qry &= vbCrLf & "AND FTLeaveType='1'"
        Qry &= vbCrLf & ") AS FNLeaveBusi"
        Qry &= vbCrLf & ",(SELECT FNHSysEmpID from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee) AS FNHSysEmpID"
        Qry &= vbCrLf & "from"
        Qry &= vbCrLf & "(select m.FNHSysEmpID,m.FDDateStart,M.FDDateProbation  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee as m"
        Qry &= vbCrLf & ") AS A WHERE A.FNHSysEmpID=" & id & ""
        Qry &= vbCrLf & ") AS XX"



        dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)

        If (dt Is Nothing) Then
            Return False
        Else
            For Each R As DataRow In dt.Rows
                s = R!FNLeaveSick.ToString
                b = R!FNLeaveBusi.ToString
                l = R!FNLate.ToString
                a = R!FNAbsent.ToString
                With ogvAsess2
                    .SetRowCellValue(0, "FNLeaveSick", s)
                    .SetRowCellValue(0, "FNLeaveBusi", b)
                    .SetRowCellValue(0, "FNLate", l)
                    .SetRowCellValue(0, "FNAbsent", a)
                End With
            Next
            With ogvAsess2
                _valueSick = (Integer.Parse(Val(.GetRowCellValue(0, "FNLeaveSick")).ToString))
                _valueBusi = (Integer.Parse(Val(.GetRowCellValue(0, "FNLeaveBusi")).ToString))
                _valueLate = 0.5 * (Integer.Parse(Val(.GetRowCellValue(0, "FNLate")).ToString))
                _valueAbsent = 4 * (Integer.Parse(Val(.GetRowCellValue(0, "FNAbsent")).ToString))

                Sum = _valueAbsent + _valueBusi + _valueLate + _valueSick
                Total = 8 - Sum
                .SetRowCellValue(.FocusedRowHandle, "FNScore", Total)
            End With
            Return True
        End If




    End Function

    Private Sub setGrid()
        With bgvAsess
            .OptionsView.ShowAutoFilterRow = False
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True

            .Columns("Score").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Score")
            .Columns("Score").SummaryItem.DisplayFormat = "{0:n0}"
        End With

        With ogvAsess2
            .OptionsView.ShowAutoFilterRow = False
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True

            .Columns("FNScore").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNScore")
            .Columns("FNScore").SummaryItem.DisplayFormat = "{0:n2}"
        End With
    End Sub

    'Private Sub LoadData_Absent_Leave_Late(EmpId As Integer)
    '    Dim Qry As String
    '    Dim dt As DataTable

    '    ogcAsess2.DataSource = Nothing

    '    Qry = "SELECT YY.Seq,YY.Condition,zz.FNAbsent,zz.FNLate,zz.FNLeaveBusi,zz.FNLeaveSick,'' as FNScore"
    '    Qry &= vbCrLf & "FROM"
    '    Qry &= vbCrLf & "(select FNListIndex,FNListIndex AS Seq,"
    '    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
    '        Qry &= vbCrLf & "FTNameTH as Condition"
    '    Else
    '        Qry &= vbCrLf & "FTNameEN as Condition"
    '    End If
    '    Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData"
    '    Qry &= vbCrLf & "WHERE FTListName='FNStatic') AS YY"
    '    Qry &= vbCrLf & "LEFT OUtER JOIN"

    '    Qry &= vbCrLf & "(select xx.AmoFNAbsent as FNAbsent,xx.AmoFNLateNormalMin as FNLate,xx.AmoFNLeaveBusuness as FNLeaveBusi,xx.AmoFNLeaveSick as FNLeaveSick,xx.FNListIndex"
    '    Qry &= vbCrLf & "from"
    '    Qry &= vbCrLf & "(select FNHSysEmpID,"
    '    Qry &= vbCrLf & "(SELECT  count(FNLateNormalMin)"
    '    Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrans AS T WITH(NOLOCK)"
    '    Qry &= vbCrLf & "WHERE T.FNHSysEmpID = A.FNHSysEmpID"
    '    Qry &= vbCrLf & "AND T.FTDateTrans >=A.FDDateStart"
    '    Qry &= vbCrLf & "AND T.FTDateTrans <=A.FDDateProbation"
    '    Qry &= vbCrLf & "AND T.FNLateNormalMin>0"
    '    Qry &= vbCrLf & ") AS AmoFNLateNormalMin"

    '    Qry &= vbCrLf & ",(SELECT  count(FNAbsent)"
    '    Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrans AS T WITH(NOLOCK)"
    '    Qry &= vbCrLf & "WHERE T.FNHSysEmpID = A.FNHSysEmpID"
    '    Qry &= vbCrLf & "AND T.FTDateTrans >=A.FDDateStart"
    '    Qry &= vbCrLf & "AND T.FTDateTrans <=A.FDDateProbation"
    '    Qry &= vbCrLf & "AND T.FNAbsent>0"
    '    Qry &= vbCrLf & ") AS AmoFNAbsent"

    '    Qry &= vbCrLf & ",(SELECT  count(FTLeaveType)"
    '    Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS T WITH(NOLOCK)"
    '    Qry &= vbCrLf & "WHERE T.FNHSysEmpID = A.FNHSysEmpID"
    '    Qry &= vbCrLf & "AND T.FTDateTrans >=A.FDDateStart"
    '    Qry &= vbCrLf & "AND T.FTDateTrans <=A.FDDateProbation"
    '    Qry &= vbCrLf & "AND FTLeaveType='0'"
    '    Qry &= vbCrLf & ") AS AmoFNLeaveSick"

    '    Qry &= vbCrLf & ",(SELECT  count(FTLeaveType)"
    '    Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS T WITH(NOLOCK)"
    '    Qry &= vbCrLf & "WHERE T.FNHSysEmpID = A.FNHSysEmpID"
    '    Qry &= vbCrLf & "AND T.FTDateTrans >=A.FDDateStart"
    '    Qry &= vbCrLf & "AND T.FTDateTrans <=A.FDDateProbation"
    '    Qry &= vbCrLf & "AND FTLeaveType='1'"
    '    Qry &= vbCrLf & ") AS AmoFNLeaveBusuness "
    '    Qry &= vbCrLf & ",(SeLeCT FNListIndex"
    '    Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData AS Sys"
    '    Qry &= vbCrLf & "WHERE Sys.FTListName='FNStatic'"
    '    Qry &= vbCrLf & ") AS FNListIndex"
    '    Qry &= vbCrLf & "from"
    '    Qry &= vbCrLf & "(select E.FNHSysEmpID,E.FDDateProbation,E.FDDateStart"
    '    Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee AS E"
    '    Qry &= vbCrLf & ") AS A ) AS XX"
    '    Qry &= vbCrLf & "where xx.FNHSysEmpID=" & EmpId & ") as ZZ ON YY.FNListIndex=ZZ.FNListIndex"
    '    dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_SYSTEM)
    '    ogcAsess2.DataSource = dt

    '    With ogvAsess2
    '        _valueSick = (Integer.Parse(Val(.GetRowCellValue(.FocusedRowHandle, "FNLeaveSick")).ToString))
    '        _valueBusi = (Integer.Parse(Val(.GetRowCellValue(.FocusedRowHandle, "FNLeaveBusi")).ToString))
    '        _valueLate = 0.5 * (Integer.Parse(Val(.GetRowCellValue(.FocusedRowHandle, "FNLate")).ToString))
    '        _valueAbsent = 4 * (Integer.Parse(Val(.GetRowCellValue(.FocusedRowHandle, "FNAbsent")).ToString))

    '        Sum = _valueAbsent + _valueBusi + _valueLate + _valueSick
    '        Total = 8 - Sum
    '        .SetRowCellValue(.FocusedRowHandle, "FNScore", Total)
    '    End With

    'End Sub

    Private Function LoadDataAssDefault() As Boolean
        Dim Qry As String = ""
        Dim dt As DataTable

        Qry = "select '0' as Poor,'0' as Fair,'0' as Good,'0' as VeryGood,'0' as Score"
        Qry &= vbCrLf & ",MAS.FNSeq as Seq,MAS.FNPoint as Multi,MAS.FNHsysEvaluateID "
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            Qry &= vbCrLf & ",MAS.FTEvaluateNameTH as Condition from"
        Else
            Qry &= vbCrLf & ",MAS.FTEvaluateNameEN as Condition from"
        End If

        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEvaluateMaster AS MAS WITH(NOLOCK)"
        Qry &= vbCrLf & "where MAS.FNPoint<>0"

        dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)
        If Not (dt Is Nothing) Then
            ogcAsess.DataSource = dt.Copy
            Return True
        Else
            Return False
        End If
    End Function

    Private Function LoadDataAss(empID As Integer) As Boolean
        Dim Qry As String = ""
        Dim dt As DataTable
        Qry = "select D.FNState1 as Poor,D.FNState2 as Fair,D.FNState3 as Good,D.FNState4 as VeryGood,D.FNTotalPoint as Score  "
        Qry &= vbCrLf & ",M.FNSeq as Seq,M.FNPoint as Multi,M.FNHsysEvaluateID"
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            Qry &= vbCrLf & ",M.FTEvaluateNameTH as Condition from"
        Else
            Qry &= vbCrLf & ",M.FTEvaluateNameEN as Condition from"
        End If

        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTEvaluateEmployee_D AS D WITH(NOLOCK) INNER JOIN"
        Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEvaluateMaster AS M WITH(NOLOCK) ON D.FNHsysEvaluateID=M.FNHsysEvaluateID"
        Qry &= vbCrLf & "where D.FNHSysEmpID=" & empID & ""
        dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)

        If dt.Rows.Count <= 0 Then
            _data = False
            Return False
        Else
            ogcAsess.DataSource = dt
            _data = True
            Return True
        End If

    End Function

    Private Sub SwitchTab()
        Me.ocmapprove.Visible = (Me.otbmain.SelectedTabPage.Name = otpAsess.Name)
        Me.ocmreject.Visible = (Me.otbmain.SelectedTabPage.Name = otpAsess.Name)
        Me.ocmdelete.Visible = (Me.otbmain.SelectedTabPage.Name = otpAsess.Name)
        HI.TL.METHOD.CallActiveToolBarFunction(Me)
    End Sub

    Private Function SaveData() As Boolean

        Dim dt As DataTable
        Dim dt2 As DataTable
        Dim numArr As Integer
        Dim Qry As String = ""
        Dim stVery As String
        Dim stGood As String
        Dim stFair As String
        Dim stPoor As String
        Dim ckDTVer As String
        Dim chkDTGood As String
        Dim chkDTFair As String
        Dim chkDTPoor As String
        Dim chkAss As Boolean = False
        Dim chdt As Boolean = False
        Dim _SysEmpId As Integer
        'Dim _StateIns As Boolean = False

        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            With bgvAsess
                numArr = .RowCount - 1
            End With

            CType(ogcAsess.DataSource, DataTable).AcceptChanges()
            dt = CType(ogcAsess.DataSource, DataTable)

            CType(ogcAsess2.DataSource, DataTable).AcceptChanges()
            dt2 = CType(ogcAsess2.DataSource, DataTable)

            For Each RechekDT As DataRow In dt.Rows
                ckDTVer = RechekDT!VeryGood.ToString
                chkDTGood = RechekDT!Good.ToString
                chkDTFair = RechekDT!Fair.ToString
                chkDTPoor = RechekDT!Poor.ToString
                If ckDTVer = "0" And chkDTGood = "0" And chkDTFair = "0" And chkDTPoor = "0" Then
                    chdt = True
                Else
                    chdt = False
                    Exit For
                End If
            Next

            If Not (chdt) Then
                Dim arr(numArr) As String
                With bgvAsess
                    Dim p As Integer = 0
                    For Each R As DataRow In dt.Rows
                        stVery = R!VeryGood.ToString
                        stGood = R!Good.ToString
                        stFair = R!Fair.ToString
                        stPoor = R!Poor.ToString

                        If stVery = "1" Or stGood = "1" Or stFair = "1" Or stPoor = "1" Then
                            arr(p) = "1"
                        Else
                            arr(p) = "0"
                        End If
                        p = p + 1
                    Next
                End With
                For Each Rray As String In arr
                    Select Case Rray
                        Case "0"
                            'Message แจ้งว่า ประเมิน ไม่ครบเงื่อนไข
                            HI.MG.ShowMsg.mInfo("กรุณาเลือกเงื่อนไขให้ครบ", 1603021511, Me.Text)

                            chkAss = False
                            Exit For
                        Case Else
                            chkAss = True
                    End Select
                Next
            Else
                'message แจ้งเตือนว่า ประเมินก่อน
                HI.MG.ShowMsg.mInfo("ประเมินก่อนนะ", 1603021512, Me.Text)
            End If
            If chkAss Then
                With ogvName
                    _SysEmpId = Integer.Parse(Val(.GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID")).ToString)
                End With

                For Each R As DataRow In dt.Rows

                    Qry = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTEvaluateEmployee_D"
                    Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    Qry &= vbCrLf & ",FNState4='" & R!VeryGood.ToString & "'"
                    Qry &= vbCrLf & ",FNState3='" & R!Good.ToString & "'"
                    Qry &= vbCrLf & ",FNState2='" & R!Fair.ToString & "'"
                    Qry &= vbCrLf & ",FNState1='" & R!Poor.ToString & "'"
                    Qry &= vbCrLf & ",FNTotalPoint=" & Val(R!Score.ToString) & ""
                    Qry &= vbCrLf & "WHERE FNHSysEmpID=" & _SysEmpId & ""
                    Qry &= vbCrLf & "AND FNHsysEvaluateID=" & Val(R!FNHsysEvaluateID.ToString) & ""
                    If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        Qry = "insert [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTEvaluateEmployee_D"
                        Qry &= vbCrLf & "(FTInsUser,FDInsDate,FTInsTime,FNHSysEmpID,FNHsysEvaluateID,FNPoint,FNState4,FNState3,FNState2,FNState1,FNTotalPoint)"
                        Qry &= vbCrLf & "SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                        Qry &= vbCrLf & "," & _SysEmpId & "," & R!FNHsysEvaluateID.ToString & "," & Val(R!Multi.ToString) & ""
                        Qry &= vbCrLf & ",'" & R!VeryGood.ToString & "','" & R!Good.ToString & "','" & R!Fair.ToString & "','" & R!Poor.ToString & "'"
                        Qry &= vbCrLf & "," & Val(R!Score.ToString) & ""

                        If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                            _Save = False
                        End If

                    End If
                Next

                For Each R2 As DataRow In dt2.Rows

                    Qry = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTEvaluateEmployee_H"
                    Qry &= vbCrLf & "SET FTUpdUser='" & HI.ST.UserInfo.UserName & "',FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    Qry &= vbCrLf & ",FTStateLeader='1'"
                    Qry &= vbCrLf & "WHERE FNHSysEmpID=" & _SysEmpId & ""
                    If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTEvaluateEmployee_H"
                        Qry &= vbCrLf & "(FTInsUser,FDInsDate,FTInsTime,FNHSysEmpID,FNLeaveSick,FNLeaveBusiness,FNLate,FNAbsent,FNTotalPoint,FTStateLeader,FTStateLeaderBy,FTStateLeadeDate,FTStateLeadeTime)"
                        Qry &= vbCrLf & "SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                        Qry &= vbCrLf & "," & _SysEmpId & "," & Val(R2!FNLeaveSick.ToString) & "," & Val(R2!FNLeaveBusi.ToString) & "," & Val(R2!FNLate.ToString) & "," & Val(R2!FNAbsent.ToString) & "," & Val(R2!FNScore.ToString) & ""
                        Qry &= vbCrLf & ",'1'"
                        Qry &= vbCrLf & ",'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    End If
                    If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                        _Save = False
                    End If
                Next

            Else
                Return False
            End If
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Save = True
            chkStg = False
            _StateFocus = False
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Save = False
            Return False
        End Try
    End Function

    Private Function Delete() As Boolean
        Dim Qry As String = ""
        Dim id As Integer

        With ogvName
            id = Integer.Parse(Val(.GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID")).ToString)
        End With
        Try
            Qry = "delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTEvaluateEmployee_H"
            Qry &= vbCrLf & "WHERE FNHSysEmpID=" & id & ""
            HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_HR)
            Qry = "delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTEvaluateEmployee_D"
            Qry &= vbCrLf & "WHERE FNHSysEmpID=" & id & ""
            HI.Conn.SQLConn.ExecuteNonQuery(Qry, Conn.DB.DataBaseName.DB_HR)
        Catch ex As Exception
            Return False
        End Try
        Return True

    End Function

    Private Function Reject() As Boolean
        Dim Qry As String = ""
        Dim _SysEmpId As Integer
        Dim dt As DataTable
        Dim dt2 As DataTable
        Dim numArr As Integer
        Dim stVery As String
        Dim stGood As String
        Dim stFair As String
        Dim stPoor As String
        Dim ckDTVer As String
        Dim chkDTGood As String
        Dim chkDTFair As String
        Dim chkDTPoor As String
        Dim chkAss As Boolean = False
        Dim chdt As Boolean = False
        'Dim _StateIns As Boolean = False

        With ogvName
            _SysEmpId = Integer.Parse(Val(.GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID")).ToString)
        End With
        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            With bgvAsess
                numArr = .RowCount - 1
            End With

            CType(ogcAsess.DataSource, DataTable).AcceptChanges()
            dt = CType(ogcAsess.DataSource, DataTable)

            CType(ogcAsess2.DataSource, DataTable).AcceptChanges()
            dt2 = CType(ogcAsess2.DataSource, DataTable)

            For Each RechekDT As DataRow In dt.Rows
                ckDTVer = RechekDT!VeryGood.ToString
                chkDTGood = RechekDT!Good.ToString
                chkDTFair = RechekDT!Fair.ToString
                chkDTPoor = RechekDT!Poor.ToString
                If ckDTVer = "0" And chkDTGood = "0" And chkDTFair = "0" And chkDTPoor = "0" Then
                    chdt = True
                Else
                    chdt = False
                    Exit For
                End If
            Next

            If Not (chdt) Then
                Dim arr(numArr) As String
                With bgvAsess
                    Dim p As Integer = 0
                    For Each R As DataRow In dt.Rows
                        stVery = R!VeryGood.ToString
                        stGood = R!Good.ToString
                        stFair = R!Fair.ToString
                        stPoor = R!Poor.ToString

                        If stVery = "1" Or stGood = "1" Or stFair = "1" Or stPoor = "1" Then
                            arr(p) = "1"
                        Else
                            arr(p) = "0"
                        End If
                        p = p + 1
                    Next
                End With
                For Each Rray As String In arr
                    Select Case Rray
                        Case "0"
                            'Message แจ้งว่า ประเมิน ไม่ครบเงื่อนไข
                            HI.MG.ShowMsg.mInfo("กรุณาเลือกเงื่อนไขให้ครบ", 1603021511, Me.Text)

                            chkAss = False
                            Exit For
                        Case Else
                            chkAss = True
                    End Select
                Next
            Else
                'message แจ้งเตือนว่า ประเมินก่อน
                HI.MG.ShowMsg.mInfo("ประเมินก่อนนะ", 1603021512, Me.Text)
            End If


            With ogvName
                _SysEmpId = Integer.Parse(Val(.GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID")).ToString)
            End With
            If chkAss Then
                For Each R As DataRow In dt.Rows

                    Qry = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTEvaluateEmployee_D"
                    Qry &= vbCrLf & "SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    Qry &= vbCrLf & ",FNState4='" & R!VeryGood.ToString & "'"
                    Qry &= vbCrLf & ",FNState3='" & R!Good.ToString & "'"
                    Qry &= vbCrLf & ",FNState2='" & R!Fair.ToString & "'"
                    Qry &= vbCrLf & ",FNState1='" & R!Poor.ToString & "'"
                    Qry &= vbCrLf & ",FNTotalPoint=" & Val(R!Score.ToString) & ""
                    Qry &= vbCrLf & "WHERE FNHSysEmpID=" & _SysEmpId & ""
                    Qry &= vbCrLf & "AND FNHsysEvaluateID=" & Val(R!FNHsysEvaluateID.ToString) & ""

                    If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        Qry = "insert [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTEvaluateEmployee_D"
                        Qry &= vbCrLf & "(FTInsUser,FDInsDate,FTInsTime,FNHSysEmpID,FNHsysEvaluateID,FNPoint,FNState4,FNState3,FNState2,FNState1,FNTotalPoint)"
                        Qry &= vbCrLf & "SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                        Qry &= vbCrLf & "," & _SysEmpId & "," & R!FNHsysEvaluateID.ToString & "," & Val(R!Multi.ToString) & ""
                        Qry &= vbCrLf & ",'" & R!VeryGood.ToString & "','" & R!Good.ToString & "','" & R!Fair.ToString & "','" & R!Poor.ToString & "'"
                        Qry &= vbCrLf & "," & Val(R!Score.ToString) & ""

                        If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                            _Save = False
                        End If
                    End If
                Next
                For Each R2 As DataRow In dt2.Rows
                    Qry = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTEvaluateEmployee_H"
                    Qry &= vbCrLf & "SET FTUpdUser='" & HI.ST.UserInfo.UserName & "',FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    Qry &= vbCrLf & ",FTStateLeader='0'"
                    Qry &= vbCrLf & "WHERE FNHSysEmpID=" & _SysEmpId & ""
                    If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        Qry = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTEvaluateEmployee_H"
                        Qry &= vbCrLf & "(FTInsUser,FDInsDate,FTInsTime,FNHSysEmpID,FNLeaveSick,FNLeaveBusiness,FNLate,FNAbsent,FNTotalPoint,FTStateLeader,FTStateLeaderBy,FTStateLeadeDate,FTStateLeadeTime)"
                        Qry &= vbCrLf & "SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                        Qry &= vbCrLf & "," & _SysEmpId & "," & Val(R2!FNLeaveSick.ToString) & "," & Val(R2!FNLeaveBusi.ToString) & "," & Val(R2!FNLate.ToString) & "," & Val(R2!FNAbsent.ToString) & "," & Val(R2!FNScore.ToString) & ""
                        Qry &= vbCrLf & ",'0'"
                        Qry &= vbCrLf & ",'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                        If HI.Conn.SQLConn.Execute_Tran(Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                            _Save = False
                        End If
                    End If
                Next
            Else
                Return False
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Sub FTVeryGood_EditValueChanged(sender As Object, e As EventArgs) Handles FTVeryGood.EditValueChanged

        With bgvAsess
            row = .FocusedRowHandle
            Score = Integer.Parse(Val(.GetRowCellValue(row, "Score")).ToString)
            Multi = Integer.Parse(Val(.GetRowCellValue(row, "Multi")).ToString)
            If Score = Multi * 4 Then
                .SetRowCellValue(row, "Score", 0.0)
                'chkStg = False
                _StateFocus = False
            Else
                .SetRowCellValue(row, "Score", Multi * 4)
                'chkStg = True
                _StateFocus = True
            End If
            For i As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(i).FieldName.ToString
                    Case "Good", "Fair", "Poor"
                        .SetRowCellValue(row, "Good", "0")
                        .SetRowCellValue(row, "Fair", "0")
                        .SetRowCellValue(row, "Poor", "0")
                End Select
            Next
        End With
        chkStg = True
        '_StateFocus = True
    End Sub

    Private Sub FTGood_EditValueChanged(sender As Object, e As EventArgs) Handles FTGood.EditValueChanged
        With bgvAsess
            row = .FocusedRowHandle
            Score = Integer.Parse(Val(.GetRowCellValue(row, "Score")).ToString)
            Multi = Integer.Parse(Val(.GetRowCellValue(row, "Multi")).ToString)
            If Score = Multi * 3 Then
                .SetRowCellValue(row, "Score", 0.0)
                _StateFocus = False
            Else
                .SetRowCellValue(row, "Score", Multi * 3)
                _StateFocus = True
            End If
            For i As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(i).FieldName.ToString
                    Case "VeryGood", "Fair", "Poor"
                        .SetRowCellValue(row, "VeryGood", "0")
                        .SetRowCellValue(row, "Fair", "0")
                        .SetRowCellValue(row, "Poor", "0")
                End Select
            Next
        End With
        chkStg = True
        '_StateFocus = True
    End Sub

    Private Sub FTFair_EditValueChanged(sender As Object, e As EventArgs) Handles FTFair.EditValueChanged
        With bgvAsess
            row = .FocusedRowHandle
            Score = Integer.Parse(Val(.GetRowCellValue(row, "Score")).ToString)
            Multi = Integer.Parse(Val(.GetRowCellValue(row, "Multi")).ToString)
            If Score = Multi * 2 Then
                .SetRowCellValue(row, "Score", 0.0)
                _StateFocus = False
            Else
                .SetRowCellValue(row, "Score", Multi * 2)
                _StateFocus = True
            End If
            For i As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(i).FieldName.ToString
                    Case "VeryGood", "Good", "Poor"
                        .SetRowCellValue(row, "VeryGood", "0")
                        .SetRowCellValue(row, "Good", "0")
                        .SetRowCellValue(row, "Poor", "0")
                End Select
            Next
        End With
        chkStg = True
        '_StateFocus = True
    End Sub

    Private Sub FTPoor_EditValueChanged(sender As Object, e As EventArgs) Handles FTPoor.EditValueChanged
        With bgvAsess
            row = .FocusedRowHandle
            Score = Integer.Parse(Val(.GetRowCellValue(row, "Score")).ToString)
            Multi = Integer.Parse(Val(.GetRowCellValue(row, "Multi")).ToString)
            If Score = Multi * 1 Then
                .SetRowCellValue(row, "Score", 0.0)
                _StateFocus = False
            Else
                .SetRowCellValue(row, "Score", Multi * 1)
                _StateFocus = True
            End If
            For i As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(i).FieldName.ToString
                    Case "VeryGood", "Fair", "Good"
                        .SetRowCellValue(row, "VeryGood", "0")
                        .SetRowCellValue(row, "Fair", "0")
                        .SetRowCellValue(row, "Good", "0")
                End Select
            Next
        End With
        chkStg = True
        '_StateFocus = True
    End Sub

    'Private Sub ResFTSelect_EditValueChanged(sender As Object, e As EventArgs) Handles ResFTSelect.EditValueChanged
    '    Dim _RowValue As Integer
    '    Dim EmpId As Integer = 0
    '    With ogvName
    '        row = .FocusedRowHandle
    '        _RowValue = Integer.Parse(Val(.GetRowCellValue(row, "FTSelect")).ToString)
    '        EmpId = Integer.Parse(Val(.GetRowCellValue(row, "FNHSysEmpID")).ToString)
    '        For i As Integer = .RowCount - 1 To 0 Step -1
    '            If i = row Then
    '                If _RowValue = 1 Then
    '                    .SetRowCellValue(row, "FTSelect", "0")
    '                Else
    '                    .SetRowCellValue(row, "FTSelect", "1")
    '                End If
    '            Else
    '                .SetRowCellValue(i, "FTSelect", "0")
    '            End If
    '        Next
    '        If EmpId > 0 Then
    '            'With bgvAsess
    '            '    .Columns("VeryGood").OptionsColumn.AllowEdit = True
    '            '    .Columns("VeryGood").OptionsColumn.ReadOnly = False
    '            '    .Columns("Good").OptionsColumn.AllowEdit = True
    '            '    .Columns("Good").OptionsColumn.ReadOnly = False
    '            '    .Columns("Fair").OptionsColumn.AllowEdit = True
    '            '    .Columns("Fair").OptionsColumn.ReadOnly = False
    '            '    .Columns("Poor").OptionsColumn.AllowEdit = True
    '            '    .Columns("Poor").OptionsColumn.ReadOnly = False
    '            'End With
    '            Call LoadData_Absent_Leave_Late(EmpId)
    '        Else
    '            Call LoadAsess2()
    '        End If

    '    End With
    'End Sub

    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click
        Dim chdt As Boolean = False
        Dim dt As DataTable
        CType(ogcAsess.DataSource, DataTable).AcceptChanges()
        dt = CType(ogcAsess.DataSource, DataTable)
        For Each RechekDT As DataRow In dt.Rows
            If RechekDT!VeryGood.ToString = "1" Or RechekDT!Good.ToString = "1" Or RechekDT!Fair.ToString = "1" Or RechekDT!Poor.ToString = "1" Then
                chdt = True
            Else
                chdt = False
                Exit For
            End If
        Next

        If chdt Then
            If SaveData() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        Else
            HI.MG.ShowMsg.mInfo("กรุณาเลือกเงื่อนไขให้ครบ", 1603021511, Me.Text)
        End If

    End Sub

    Private Sub otbmain_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbmain.SelectedPageChanged
        Call SwitchTab()
        Call ClearCheckEdit()
    End Sub

    Private Sub ogvName_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles ogvName.RowClick
        Dim empid As Integer
        Dim chkAss As Boolean = False
        Dim chdt As Boolean = False
        Dim Qry As String = ""
        Dim ckDTVer As String
        Dim chkDTGood As String
        Dim chkDTFair As String
        Dim chkDTPoor As String
        Dim numerArr As Integer = 0
        Dim Em As Boolean

        CType(ogcAsess.DataSource, DataTable).AcceptChanges()
        Dim dt = CType(ogcAsess.DataSource, DataTable)

        With bgvAsess
            numerArr = .RowCount - 1
        End With

        With ogvName
            empid = 0
            empid = Integer.Parse(Val(.GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID")).ToString)
            If empid > 0 Then
                With bgvAsess
                    .Columns("VeryGood").OptionsColumn.AllowEdit = True
                    .Columns("VeryGood").OptionsColumn.ReadOnly = False
                    .Columns("Good").OptionsColumn.AllowEdit = True
                    .Columns("Good").OptionsColumn.ReadOnly = False
                    .Columns("Fair").OptionsColumn.AllowEdit = True
                    .Columns("Fair").OptionsColumn.ReadOnly = False
                    .Columns("Poor").OptionsColumn.AllowEdit = True
                    .Columns("Poor").OptionsColumn.ReadOnly = False
                End With
            End If

            If LoadDataAss2(empid) Then
                If LoadDataAss(empid) Then
                Else
                    If chkStg Then
                        For Each Empty As DataRow In dt.Rows
                            If Empty!VeryGood.ToString = "0" And Empty!Good.ToString = "0" And Empty!Fair.ToString = "0" And Empty!Poor.ToString = "0" Then
                                Em = True
                            Else
                                Em = False
                                Exit For
                            End If
                        Next
                        If Em Then
                            chkStg = False
                            Exit Sub
                        Else
                            For Each RechekDT As DataRow In dt.Rows
                                ckDTVer = RechekDT!VeryGood.ToString
                                chkDTGood = RechekDT!Good.ToString
                                chkDTFair = RechekDT!Fair.ToString
                                chkDTPoor = RechekDT!Poor.ToString
                                If ckDTVer = "1" Or chkDTGood = "1" Or chkDTFair = "1" Or chkDTPoor = "1" Then
                                    chdt = True
                                Else
                                    If .FocusedRowHandle <> RowPositOgcName Then
                                        HI.MG.ShowMsg.mInfo("กรุณาเลือกเงื่อนไขให้ครบ", 1603021511, Me.Text)
                                    End If
                                    .FocusedRowHandle = RowPositOgcName
                                    chdt = False
                                    Exit For
                                End If
                            Next
                            If chdt Then
                                With bgvAsess
                                    For i = 0 To .RowCount - 1
                                        .SetRowCellValue(i, "VeryGood", "0")
                                        .SetRowCellValue(i, "Good", "0")
                                        .SetRowCellValue(i, "Fair", "0")
                                        .SetRowCellValue(i, "Poor", "0")
                                        .SetRowCellValue(i, "Score", "0")
                                    Next
                                End With
                                chkStg = False
                            Else
                            End If
                        End If
                    Else
                        If Not (_data) Then
                            With bgvAsess
                                For i = 0 To .RowCount - 1
                                    .SetRowCellValue(i, "VeryGood", "0")
                                    .SetRowCellValue(i, "Good", "0")
                                    .SetRowCellValue(i, "Fair", "0")
                                    .SetRowCellValue(i, "Poor", "0")
                                    .SetRowCellValue(i, "Score", "0")
                                Next
                            End With
                        End If
                    End If
                End If
            End If
        End With
        If Not (_StateFocus) Then
            With ogvName
                RowPositOgcName = .FocusedRowHandle
            End With
        End If
    End Sub

    Private Sub ocmreject_Click(sender As Object, e As EventArgs) Handles ocmreject.Click
        Dim chdt As Boolean = False
        Dim dt As DataTable
        CType(ogcAsess.DataSource, DataTable).AcceptChanges()
        dt = CType(ogcAsess.DataSource, DataTable)
        For Each RechekDT As DataRow In dt.Rows
            If RechekDT!VeryGood.ToString = "1" Or RechekDT!Good.ToString = "1" Or RechekDT!Fair.ToString = "1" Or RechekDT!Poor.ToString = "1" Then
                chdt = True
            Else
                chdt = False
                Exit For
            End If
        Next
        If chdt Then
            If Reject() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        Else
            HI.MG.ShowMsg.mInfo("กรุณาเลือกเงื่อนไขให้ครบ", 1603021511, Me.Text)
        End If

    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete, "" & ogvName.GetRowCellValue(ogvName.FocusedRowHandle, "FTEmpName") & "") Then
            If Delete() Then
                With bgvAsess
                    For i = 0 To .RowCount - 1
                        .SetRowCellValue(i, "VeryGood", "0")
                        .SetRowCellValue(i, "Good", "0")
                        .SetRowCellValue(i, "Fair", "0")
                        .SetRowCellValue(i, "Poor", "0")
                        .SetRowCellValue(i, "Score", "0")
                    Next
                End With
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            End If
        End If

    End Sub

    Private Sub bgvAsess_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles bgvAsess.CellValueChanged
        Dim _values1 As Double
        Dim _values2 As Double
        Dim TotalScore As Double
        CType(ogcAsess.DataSource, DataTable).AcceptChanges()
        Dim dt = CType(ogcAsess.DataSource, DataTable)
        CType(ogcAsess2.DataSource, DataTable).AcceptChanges()
        Dim _dt = CType(ogcAsess2.DataSource, DataTable)

        For Each R As DataRow In dt.Rows
            _values1 += R!Score.ToString
        Next

        For Each Rs As DataRow In _dt.Rows
            _values2 += Rs!FNScore.ToString
        Next

        TotalScore = _values1 + _values2

        If TotalScore < 50 Then
            Score_lbl.Text = TotalScore
            Score_lbl.Appearance.ForeColor = Drawing.Color.Red
            'Score_lbl.Appearance.BackColor = Drawing.Color.LightCyan
        Else
            Score_lbl.Text = TotalScore
            Score_lbl.Appearance.ForeColor = Drawing.Color.Green
            'Score_lbl.Appearance.BackColor = Drawing.Color.LightCyan
        End If
    End Sub

    'Private Sub ogcAsess_DoubleClick(sender As Object, e As EventArgs) Handles ogcAsess.DoubleClick
    '    CType(ogcAsess.DataSource, DataTable).AcceptChanges()
    '    Dim dt = CType(ogcAsess.DataSource, DataTable)
    '    Dim _value As Integer
    '    Dim _val2 As Integer
    '    Dim checkVal As Boolean = False

    '    Dim _dt = CType(ogcAsess2.DataSource, DataTable)
    '    For Each _R As DataRow In _dt.Rows
    '        _val2 = _R!FNScore.ToString
    '    Next
    '    With bgvAsess
    '        For Each Rchk As DataRow In dt.Rows
    '            If Rchk!VeryGood.ToString = "1" Or Rchk!Good.ToString = "1" Or Rchk!Fair.ToString = "1" Or Rchk!Poor.ToString = "1" Then
    '                checkVal = True
    '            Else
    '                checkVal = False
    '                Exit For
    '            End If
    '        Next
    '        If checkVal Then
    '            If Integer.Parse(Val(.GetRowCellValue(.FocusedRowHandle, "Score")).ToString) > 0 Then
    '                For Each R As DataRow In dt.Rows
    '                    _value += R!Score.ToString
    '                Next
    '                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
    '                    HI.MG.ShowMsg.mInfo("รวมคะแนนประเมินทั้งหมด", 1603140912, Me.Text, " " & _value + _val2 & " คะแนน ")
    '                Else
    '                    HI.MG.ShowMsg.mInfo("รวมคะแนนประเมินทั้งหมด", 1603140912, Me.Text, " " & _value + _val2 & " Mark ")
    '                End If

    '            End If
    '        Else
    '            HI.MG.ShowMsg.mInfo("กรุณาเลือกเงื่อนไขให้ครบ", 1603021511, Me.Text)
    '        End If

    '    End With
    'End Sub

    Private Sub bgvAsess_DataSourceChanged(sender As Object, e As EventArgs) Handles bgvAsess.DataSourceChanged
        Dim _values1 As Double
        Dim _values2 As Double
        Dim TotalScore As Double
        CType(ogcAsess.DataSource, DataTable).AcceptChanges()
        Dim dt = CType(ogcAsess.DataSource, DataTable)
        CType(ogcAsess2.DataSource, DataTable).AcceptChanges()
        Dim _dt = CType(ogcAsess2.DataSource, DataTable)

        For Each R As DataRow In dt.Rows
            _values1 += R!Score.ToString
        Next

        For Each Rs As DataRow In _dt.Rows
            _values2 += Rs!FNScore.ToString
        Next

        TotalScore = _values1 + _values2

        If TotalScore < 50 Then
            Score_lbl.Text = TotalScore
            Score_lbl.Appearance.ForeColor = Drawing.Color.Red
            'Score_lbl.Appearance.BackColor = Drawing.Color.LightCyan

        Else
            Score_lbl.Text = TotalScore
            Score_lbl.Appearance.ForeColor = Drawing.Color.Green
            'Score_lbl.Appearance.BackColor = Drawing.Color.LightCyan
        End If
    End Sub

    Private Sub ockall_CheckedChanged(sender As Object, e As EventArgs) Handles ockall.CheckedChanged
        If _FormLoad = False Then Exit Sub
        Try
            If ogvDetail.RowCount <= 0 Then Exit Sub
            With CType(ogcDetail.DataSource, DataTable)
                If Me.ockall.Checked = True Then
                    For Each R As DataRow In .Rows
                        R!FTSelect = "1"
                    Next
                    .AcceptChanges()
                Else
                    For Each R As DataRow In .Rows
                        R!FTSelect = "0"
                    Next
                    .AcceptChanges()
                End If
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ClearCheckEdit()
        Try
            If (Me.otbmain.SelectedTabPage.Name = Me.otpAsess.Name) Then
                If ogvDetail.RowCount <= 0 Then Exit Sub
                With CType(ogcDetail.DataSource, DataTable)
                    For Each R As DataRow In .Rows
                        R!FTSelect = "0"
                    Next
                    Me.ockall.Checked = False
                    .AcceptChanges()
                End With
            ElseIf (Me.otbmain.SelectedTabPage.Name = Me.otpevaluate.Name) Then
                If ogvName.RowCount <= 0 Then Exit Sub
                With CType(ogcName.DataSource, DataTable)
                    For Each R As DataRow In .Rows
                        R!FTSelect = "0"
                    Next
                    Me.ockall2.Checked = False
                    .AcceptChanges()
                End With
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ockall2_EditValueChanged(sender As Object, e As EventArgs) Handles ockall2.EditValueChanged
        If _FormLoad = False Then Exit Sub
        Try
            If ogvName.RowCount <= 0 Then Exit Sub
            With CType(ogcName.DataSource, DataTable)
                If Me.ockall2.Checked = True Then
                    For Each R As DataRow In .Rows
                        R!FTSelect = "1"
                    Next
                    .AcceptChanges()
                Else
                    For Each R As DataRow In .Rows
                        R!FTSelect = "0"
                    Next
                    .AcceptChanges()
                End If
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class