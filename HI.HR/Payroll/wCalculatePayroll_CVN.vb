Public Class wCalculatePayroll_CVN
    Private _ListNotCal As wListEmplyeeNotCalPayroll
#Region "General"

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _ListNotCal = New wListEmplyeeNotCalPayroll
        HI.TL.HandlerControl.AddHandlerObj(_ListNotCal)


        Dim _SystemLang As New ST.SysLanguage
        Try
            Call _SystemLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ListNotCal.Name.ToString.Trim, _ListNotCal)
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmclose.Click
        Me.Close()
    End Sub

    Private Function GenQuery(SDate As String, EDate As String, Optional StateDel As Boolean = False) As String
        Dim _Qry As String = ""

        _Qry = " SELECT      '0' AS FTSelect,  M.FNHSysEmpID, M.FTEmpCode,M.FNHSysEmpTypeId"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & " , ISNULL(P.FTPreNameNameTH,N'') + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

        Else
            _Qry &= vbCrLf & " , ISNULL(P.FTPreNameNameEN,N'') + ' ' + M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
        End If

        _Qry &= vbCrLf & " , M.FTDeligentCode "
        _Qry &= vbCrLf & " FROM        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        _Qry &= vbCrLf & "   THRMTimeShift AS SH WITH (NOLOCK) ON M.FNHSysShiftID = SH.FNHSysShiftID "
        _Qry &= vbCrLf & "   INNER Join "
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS SE WITH (NOLOCK) ON M.FNHSysSectId = SE.FNHSysSectId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId "
        _Qry &= vbCrLf & "   WHERE  M.FTEmpCode <> ''  "
        _Qry &= vbCrLf & "   AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "
        '  _Qry &= vbCrLf & "   AND M.FDDateStart <=N'" & EDate & "' "

        If FTPayTerm.Text = "01" Then
            _Qry &= vbCrLf & "   AND  ( CASE WHEN ISNULL(M.FDDateTransfer,'') <> '' AND ISNULL(M.FDDateTransfer,'') > M.FDDateStart THEN ISNULL(M.FDDateTransfer,'') ELSE M.FDDateStart  END <=N'" & EDate & "' "

            _Qry &= vbCrLf & " OR (M.FNHSysEmpID IN ("
            _Qry &= vbCrLf & "SELECT     P.FNHSysEmpID "
            _Qry &= vbCrLf & "  FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS P  WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M  WITH (NOLOCK)  ON P.FNHSysEmpID = M.FNHSysEmpID"
            _Qry &= vbCrLf & "  WHERE     (P.FTPayYear = '" & (Integer.Parse(Val(Me.FTPayYear.Text)) - 1).ToString("0000") & "') "
            _Qry &= vbCrLf & "  AND (P.FTPayTerm = '25') "
            _Qry &= vbCrLf & "  AND (M.FDDateEnd <> '') "
            _Qry &= vbCrLf & "  AND M.FDDateEnd<='" & SDate & "'"
            _Qry &= vbCrLf & "))"
            _Qry &= vbCrLf & "     )"
        Else
            _Qry &= vbCrLf & "   AND CASE WHEN ISNULL(M.FDDateTransfer,'') <> '' AND ISNULL(M.FDDateTransfer,'') > M.FDDateStart THEN ISNULL(M.FDDateTransfer,'') ELSE M.FDDateStart  END <=N'" & EDate & "' "
        End If

        If StateDel = False Then

            If FTPayTerm.Text = "01" Then
                _Qry &= vbCrLf & "   AND ((M.FDDateEnd =N'' OR M.FDDateEnd >'" & SDate & "' )   "

                _Qry &= vbCrLf & " OR (M.FDDateEnd<>'' AND M.FDDateEnd <='" & SDate & "' AND  M.FNHSysEmpID IN ("
                _Qry &= vbCrLf & "SELECT     P.FNHSysEmpID "
                _Qry &= vbCrLf & "  FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS P  WITH (NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M  WITH (NOLOCK)  ON P.FNHSysEmpID = M.FNHSysEmpID"
                _Qry &= vbCrLf & "  WHERE     (P.FTPayYear = '" & (Integer.Parse(Val(Me.FTPayYear.Text)) - 1).ToString("0000") & "') "
                _Qry &= vbCrLf & "  AND (P.FTPayTerm = '25') "
                _Qry &= vbCrLf & "  AND (M.FDDateEnd <> '') "
                _Qry &= vbCrLf & "  AND M.FDDateEnd<='" & SDate & "'"
                _Qry &= vbCrLf & "))"
                _Qry &= vbCrLf & "     )"

            Else
                _Qry &= vbCrLf & "   AND (M.FDDateEnd =N'' OR M.FDDateEnd >'" & SDate & "' )   "
            End If

        End If

        If Not (HI.ST.SysInfo.Admin) Then

            _Qry &= vbCrLf & "  AND M.FNHSysEmpTypeId IN ("
            _Qry &= vbCrLf & " Select DISTINCT UPT.FNHSysEmpTypeId"
            _Qry &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID"
            _Qry &= vbCrLf & "  WHERE UP.FTUserName=N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & "  AND UPM.FTMnuName=N'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) & "' "

            _Qry &= vbCrLf & "  )      "
            _Qry &= vbCrLf & " AND M.FNHSysSectId IN ( "
            _Qry &= vbCrLf & " Select DISTINCT S.FNHSysSectId"
            _Qry &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID"
            _Qry &= vbCrLf & "   CROSS JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S  WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE UP.FTUserName=N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & "  AND UPM.FTMnuName=N'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) & "' "
            _Qry &= vbCrLf & "  AND UPT.FTStateAll=N'1' "
            _Qry &= vbCrLf & " UNION"
            _Qry &= vbCrLf & " Select DISTINCT S.FNHSysSectId"
            _Qry &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeTypeSect AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID"
            _Qry &= vbCrLf & "   INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH(NOLOCK) ON UPT.FNHSysSectId = S.FNHSysSectId  "
            _Qry &= vbCrLf & "  WHERE UP.FTUserName=N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & "  AND UPM.FTMnuName=N'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) & "' "
            _Qry &= vbCrLf & "  )      "

        End If

        If Me.FNHSysEmpTypeId.Text <> "" Then
            _Qry &= vbCrLf & " AND ET.FTEmpTypeCode=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
        End If

        '------Criteria By Employeee Code
        If Me.FNHSysEmpId.Text <> "" Then
            _Qry &= vbCrLf & " AND M.FTEmpCode >=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
        End If

        If Me.FNHSysEmpIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND M.FTEmpCode <=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
        End If

        '------Criteria By Department
        If Me.FNHSysDeptId.Text <> "" Then
            _Qry &= vbCrLf & " AND  Dept.FTDeptCode>=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
        End If

        If Me.FNHSysDeptIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  Dept.FTDeptCode<=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
        End If

        '------Criteria By Division
        If Me.FNHSysDivisonId.Text <> "" Then
            _Qry &= vbCrLf & " AND  DI.FTDivisonCode>=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
        End If

        If Me.FNHSysDivisonIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  DI.FTDivisonCode<=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
        End If

        '------Criteria By Sect
        If Me.FNHSysSectId.Text <> "" Then
            _Qry &= vbCrLf & " AND  SE.FTSectCode>=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
        End If

        If Me.FNHSysSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  SE.FTSectCode<=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
        End If

        '------Criteria Unit Sect
        If Me.FNHSysUnitSectId.Text <> "" Then
            _Qry &= vbCrLf & " AND   US.FTUnitSectCode>=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
        End If

        If Me.FNHSysUnitSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND   US.FTUnitSectCode<=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
        End If


        Return _Qry
    End Function


    Private Sub ocmcalculate_Click(sender As System.Object, e As System.EventArgs) Handles ocmcalculate.Click
        If Me.FNHSysEmpTypeId.Text <> "" And FNHSysEmpTypeId.Properties.Tag.ToString <> "" Then
            If HI.UL.ULDate.CheckDate(Me.FTStartDate.Text) <> "" And HI.UL.ULDate.CheckDate(Me.FTEndDate.Text) <> "" Then

                Dim dtemptype As DataTable
                Dim _Dt As DataTable
                Dim _Qry As String = ""
                Dim _QryDel As String = ""
                Dim _StateVacationRet As Integer
                Dim _FTStaDeductAbsent As Integer = 0
                Dim _FTStaCalPayRoll As Integer = 0
                Dim _FNStateSalaryType As Integer = 0

                If Integer.Parse(FTPayYear.Text) >= 2014 Then
                    _Qry = " SELECT   TOP 1 FCCfgRetValue"
                    _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigReturnVacationSet WITH(NOLOCK) "
                    _Qry &= vbCrLf & "  WHERE      (FNCalType =" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & ")"
                    _Qry &= vbCrLf & "  AND (FTCfgRetTerm = '" & HI.UL.ULF.rpQuoted(FTPayTerm.Text) & "')"
                    _StateVacationRet = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))
                Else
                    _StateVacationRet = 0
                End If

                Dim _FNWorkDayInWeekBF As Integer = 0
                Dim _FNWorkDayInWeek As Integer = 15
                Dim _FNWorkDayInMonth As Integer = 30
                Dim _dtWKDay As DataTable

                _Qry = " SELECT SUM(ISNULL(A.FNWorkDay,0)) AS FNMonthWorkDay"
                _Qry &= vbCrLf & " ,B.FNWorkDay AS FNWeekWorkDay"
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS A WITH(NOLOCK)"
                _Qry &= vbCrLf & "  INNER Join"
                _Qry &= vbCrLf & " (SELECT FTPayTerm, FTPayYear,FNMonth, FNHSysEmpTypeId, ISNULL(FNWorkDay,0) AS FNWorkDay"
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS X WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE  (FTPayTerm = '" & HI.UL.ULF.rpQuoted(FTPayTerm.Text) & "') "
                _Qry &= vbCrLf & " AND (FTPayYear = '" & HI.UL.ULF.rpQuoted(FTPayYear.Text) & "') "
                _Qry &= vbCrLf & " AND (FNHSysEmpTypeId =" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & ")) AS B"
                _Qry &= vbCrLf & " ON A.FTPayYear = B.FTPayYear"
                _Qry &= vbCrLf & "  AND A.FNHSysEmpTypeId = B.FNHSysEmpTypeId "
                _Qry &= vbCrLf & " AND A.FNMonth = B.FNMonth"
                _Qry &= vbCrLf & " GROUP BY B.FNWorkDay"
                _dtWKDay = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                For Each R As DataRow In _dtWKDay.Rows

                    If Val(R!FNMonthWorkDay.ToString) > 0 Then
                        _FNWorkDayInMonth = Val(R!FNMonthWorkDay.ToString)

                        If _FNWorkDayInMonth > 30 Then _FNWorkDayInMonth = 30

                    End If

                    If Val(R!FNWeekWorkDay.ToString) > 0 Then
                        _FNWorkDayInWeek = Val(R!FNWeekWorkDay.ToString)
                    End If

                    Exit For
                Next

                _Qry = " SELECT SUM(ISNULL(A.FNWorkDay,0)) AS FNWeekWorkDay"
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS A WITH(NOLOCK)"
                _Qry &= vbCrLf & "  INNER Join"
                _Qry &= vbCrLf & " (SELECT FTPayTerm, FTPayYear,FNMonth, FNHSysEmpTypeId, ISNULL(FNWorkDay,0) AS FNWorkDay"
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS X WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE  (FTPayTerm = '" & HI.UL.ULF.rpQuoted(FTPayTerm.Text) & "') "
                _Qry &= vbCrLf & " AND (FTPayYear = '" & HI.UL.ULF.rpQuoted(FTPayYear.Text) & "') "
                _Qry &= vbCrLf & " AND (FNHSysEmpTypeId =" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & ")) AS B"
                _Qry &= vbCrLf & " ON A.FTPayYear = B.FTPayYear"
                _Qry &= vbCrLf & "  AND A.FNHSysEmpTypeId = B.FNHSysEmpTypeId "
                _Qry &= vbCrLf & " AND A.FNMonth = B.FNMonth"
                _Qry &= vbCrLf & " WHERE  (A.FTPayTerm < '" & HI.UL.ULF.rpQuoted(FTPayTerm.Text) & "') "
                _Qry &= vbCrLf & " GROUP BY B.FNWorkDay"
                _dtWKDay = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                For Each R As DataRow In _dtWKDay.Rows

                    If Val(R!FNWeekWorkDay.ToString) > 0 Then
                        _FNWorkDayInWeekBF = Val(R!FNWeekWorkDay.ToString)
                    End If

                    Exit For
                Next
                _dtWKDay.Dispose()

                _Qry = "SELECT TOP 1 FNCalType,FTStaDeductAbsent,FTStaCalPayRoll,FNStateSalaryType FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType  WITH(NOLOCK) WHERE FNHSysEmpTypeId=" & Integer.Parse(Val(FNHSysEmpTypeId.Properties.Tag.ToString)) & "  "
                dtemptype = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                Dim _TmpCalType As Integer = 0

                Dim SDate As String = HI.UL.ULDate.ConvertEnDB(FTStartDate.Text)
                Dim EDate As String = HI.UL.ULDate.ConvertEnDB(FTEndDate.Text)

                For Each R As DataRow In dtemptype.Rows
                    _TmpCalType = Integer.Parse(Val(R!FNCalType.ToString))
                    _FTStaDeductAbsent = Integer.Parse(Val(R!FTStaDeductAbsent.ToString))
                    _FTStaCalPayRoll = Integer.Parse(Val(R!FTStaCalPayRoll.ToString))
                    _FNStateSalaryType = Integer.Parse(Val(R!FNStateSalaryType.ToString))
                    Exit For
                Next
                dtemptype.Dispose()
                'If _TmpCalType = 2 Or _TmpCalType = 3 Then

                '    If _FTStaCalPayRoll = 1 Then
                '        SDate = HI.UL.ULDate.ConvertEnDB(Microsoft.VisualBasic.Left(EDate, 8) & "01")  'วันแรกของเดือน
                '        EDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(HI.UL.ULDate.AddMonth(Microsoft.VisualBasic.Left(EDate, 8) & "01", 1), -1)) 'วันแของเดือน
                '    End If

                'End If

                _Qry = Me.GenQuery(SDate, EDate, True)

                _QryDel = "DELETE  P FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS P , (" & _Qry & ") As M"
                _QryDel &= vbCrLf & " WHERE P.FNHSysEmpID = M.FNHSysEmpID"
                _QryDel &= vbCrLf & " AND P.FTPayYear = '" & FTPayYear.Text & "'"
                _QryDel &= vbCrLf & " AND P.FTPayTerm = '" & FTPayTerm.Text & "'"
                HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

                _QryDel = "DELETE  PF FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollCalculate AS PF , (" & _Qry & ") As M"
                _QryDel &= vbCrLf & " WHERE PF.FNHSysEmpID = M.FNHSysEmpID"
                _QryDel &= vbCrLf & " AND PF.FTPayYear = '" & FTPayYear.Text & "'"
                _QryDel &= vbCrLf & " AND PF.FTPayTerm = '" & FTPayTerm.Text & "'"
                HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

                _QryDel = "  DELETE PF FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin As PF, (" & _Qry & ")  As M"
                _QryDel &= vbCrLf & " WHERE PF.FNHSysEmpID = M.FNHSysEmpID"
                _QryDel &= vbCrLf & " AND PF.FTPayYear = '" & FTPayYear.Text & "'"
                _QryDel &= vbCrLf & " AND PF.FTPayTerm = '" & FTPayTerm.Text & "'"
                HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

                _QryDel = "  DELETE PFM FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFinCalculate As PFM, (" & _Qry & ")  As M"
                _QryDel &= vbCrLf & " WHERE PFM.FNHSysEmpID = M.FNHSysEmpID"
                _QryDel &= vbCrLf & " AND PFM.FTPayYear = '" & FTPayYear.Text & "'"
                _QryDel &= vbCrLf & " AND PFM.FTPayTerm = '" & FTPayTerm.Text & "'"
                HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

                _QryDel = "  DELETE PML  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollLeave AS PML, (" & _Qry & ") AS M"
                _QryDel &= vbCrLf & " WHERE PML.FNHSysEmpID = M.FNHSysEmpID"
                _QryDel &= vbCrLf & " AND PML.FTPayYear = '" & FTPayYear.Text & "'"
                _QryDel &= vbCrLf & " AND PML.FTPayTerm = '" & FTPayTerm.Text & "'"
                HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

                _QryDel = "  DELETE PMC  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTManageCalculate AS PMC, (" & _Qry & ") AS M"
                _QryDel &= vbCrLf & " WHERE PMC.FNHSysEmpID = M.FNHSysEmpID"
                _QryDel &= vbCrLf & " AND PMC.FTPayYear = '" & FTPayYear.Text & "'"
                _QryDel &= vbCrLf & " AND PMC.FTPayTerm = '" & FTPayTerm.Text & "'"
                HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)


                _Qry = Me.GenQuery(SDate, EDate)
                _Qry &= vbCrLf & " ORDER BY  M.FTEmpCode  "
                Dim _Spls As New HI.TL.SplashScreen("Prepre Data For Calculate.. Please Wait ")
                _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)



                Dim _Rec As Integer = 0
                Dim _RecError As Integer = 0
                Dim _TotalRec As Integer = _Dt.Rows.Count
                HI.HRCAL.Calculate.LoadSocialRate()
                HI.HRCAL.Calculate.LoadTaxRate()
                HI.HRCAL.Calculate.LoadDiscountTax()
                Dim _dttmpemp As New DataTable

                _dttmpemp.Columns.Add("FTEmpCode", GetType(String))
                _dttmpemp.Columns.Add("FTEmpName", GetType(String))

                For Each R As DataRow In _Dt.Rows

                    _Rec = _Rec + 1
                    _Spls.UpdateInformation("Calculating... Employee Code " & R!FTEmpCode.ToString & "    " & R!FTEmpName.ToString & "  Record  " & _Rec.ToString & " Of " & _TotalRec.ToString & "  (" & Format((_Rec * 100.0) / _TotalRec, "0.00") & " % ) ")

                    If HI.HRCAL.Calculate.CalculateWeekEnd_CVN(HI.ST.UserInfo.UserName, R!FNHSysEmpID.ToString _
                                            , R!FNHSysEmpTypeId.ToString, HI.UL.ULDate.ConvertEnDB(FTStartDate.Text), HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) _
                                            , FTPayYear.Text, FTPayTerm.Text, FDPayDate.Text, R!FTDeligentCode.ToString, _TmpCalType.ToString _
                                            , False, _StateVacationRet, _FTStaDeductAbsent, _FTStaCalPayRoll, _FNStateSalaryType, _FNWorkDayInWeek, _FNWorkDayInMonth) = False Then

                        _dttmpemp.Rows.Add(R!FTEmpCode.ToString, R!FTEmpName.ToString)

                        _RecError = _RecError + 1

                    End If

                Next

                _Spls.Close()

                HI.MG.ShowMsg.mInvalidData("", 1105030002, Me.Text, (_Rec - _RecError).ToString & " Records  ")

                If _dttmpemp.Rows.Count > 0 Then
                    HI.MG.ShowMsg.mInvalidData("พบข้อมูล วันทำงานงวด ยังไม่มีการ Accept ไม่สามารถทำการคำนวณได้ !!!", 1175030002, Me.Text)

                    With _ListNotCal
                        .ogclist.DataSource = _dttmpemp.Copy
                        .ShowDialog()
                    End With

                End If

                _dttmpemp.Dispose()
            Else

                HI.MG.ShowMsg.mInvalidData("", 1104040001, Me.Text)

                If HI.UL.ULDate.CheckDate(Me.FTStartDate.Text) = "" Then
                    FTStartDate.Focus()
                ElseIf HI.UL.ULDate.CheckDate(Me.FTEndDate.Text) = "" Then
                    FTEndDate.Focus()
                End If

            End If

        Else

            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysEmpTypeId_lbl.Text)
            FNHSysEmpTypeId.Focus()

        End If

    End Sub

    Private Sub FNHSysEmpTypeId_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FNHSysEmpTypeId.EditValueChanged
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.FNHSysEmpTypeId_ValueChanged(AddressOf FNHSysEmpTypeId_EditValueChanged), New Object() {sender, e})
            Else
                If FNHSysEmpTypeId.Text <> "" Then
                    Dim _Qry As String = ""
                    Dim _Dt As DataTable

                    _Qry = "SELECT TOP 1  FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WITH(NOLOCK) WHERE FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "'"
                    FNHSysEmpTypeId.Properties.Tag = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0"))

                    _Qry = " SELECT        D.FTPayTerm, D.FTPayYear, D.FNHSysEmpTypeId, D.FNMonth, D.FTTermOfMonth"
                    _Qry &= vbCrLf & " , D.FDPayDate, D.FDCalDateBegin, D.FDCalDateEnd, D.FDDateClose, "
                    _Qry &= vbCrLf & "  D.FTStateTermEndOfYear,D.FDPayDate "

                    If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                        _Qry &= vbCrLf & " , V_ListMonth.FTNameTH  AS FTMonth "
                    Else
                        _Qry &= vbCrLf & " , V_ListMonth.FTNameEN AS FTMonth "
                    End If

                    _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayHD AS H WITH (NOLOCK) INNER JOIN"
                    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS D WITH (NOLOCK) ON H.FTPayTerm = D.FTPayTerm "
                    _Qry &= vbCrLf & "  AND H.FTPayYear = D.FTPayYear AND "
                    _Qry &= vbCrLf & "    H.FNHSysEmpTypeId = D.FNHSysEmpTypeId INNER JOIN"
                    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_ListMonth ON D.FNMonth = V_ListMonth.FNListIndex"
                    _Qry &= vbCrLf & " WHERE H.FNHSysEmpTypeId =" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "

                    _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                    For Each R As DataRow In _Dt.Rows

                        Me.FTStartDate.Text = HI.UL.ULDate.ConvertEN(R!FDCalDateBegin.ToString)
                        Me.FTEndDate.Text = HI.UL.ULDate.ConvertEN(R!FDCalDateEnd.ToString)
                        Me.FTMonth.Text = R!FTMonth.ToString
                        Me.FTPayTerm.Text = R!FTPayTerm.ToString
                        Me.FTPayYear.Text = R!FTPayYear.ToString
                        Me.FDPayDate.Text = HI.UL.ULDate.ConvertEN(R!FDPayDate.ToString)

                        Exit For

                    Next

                End If
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub wCalculateWeekend_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub
#End Region


End Class