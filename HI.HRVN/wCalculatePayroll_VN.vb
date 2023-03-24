Public Class wCalculatePayroll_VN

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
            If System.Diagnostics.Debugger.IsAttached = True Then
                Me.Text = Me.Text & " (:: Humance Resource Manament Vietnam XXX ไฮเทค แอพพาเรล สาขาเวียดนาม ::)"
            End If
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmclose.Click
        Me.Close()
    End Sub

    Private Function GenQuery(SDate As String, EDate As String, Optional StateDel As Boolean = False) As String
        Dim _Qry As String = ""

        _Qry = " SELECT '0' AS FTSelect,  M.FNHSysEmpID, M.FTEmpCode,M.FNHSysEmpTypeId"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & " , ISNULL(P.FTPreNameNameTH,N'') + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

        Else
            _Qry &= vbCrLf & " , ISNULL(P.FTPreNameNameEN,N'') + ' ' + M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
        End If

        _Qry &= vbCrLf & " , M.FTDeligentCode "
        _Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        _Qry &= vbCrLf & "      THRMTimeShift AS SH WITH (NOLOCK) ON M.FNHSysShiftID = SH.FNHSysShiftID "
        _Qry &= vbCrLf & "      INNER JOIN "
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS SE WITH (NOLOCK) ON M.FNHSysSectId = SE.FNHSysSectId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId "
        _Qry &= vbCrLf & "   WHERE  M.FTEmpCode <> ''  "
        _Qry &= vbCrLf & "   AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "
        _Qry &= vbCrLf & "   AND M.FDDateStart <=N'" & EDate & "' "

        If StateDel = False Then
            _Qry &= vbCrLf & "   AND (M.FDDateEnd =N'' OR M.FDDateEnd >'" & SDate & "' )   "
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
            _Qry &= vbCrLf & " And  FNEmpTypeGroup =" & Val(FNEmpTypeGroup.SelectedIndex.ToString)
        End If



        If Me.FNHSysEmpTypeId.Text <> "" Then
            _Qry &= vbCrLf & " And ET.FTEmpTypeCode=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
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

        'Dim FNHSysWorkdayPerMonth As Integer = HI.TL.RunID.GetRunNoID("THRMCfgWorkdayPerMonth", "FNHSysWorkdayPerMonthId", HI.Conn.DB.DataBaseName.DB_HR)
        'If System.Diagnostics.Debugger.IsAttached = True Then Exit Sub

        If Me.FNHSysEmpTypeId.Text <> "" And FNHSysEmpTypeId.Properties.Tag.ToString <> "" Then
            If HI.UL.ULDate.CheckDate(Me.FTStartDate.Text) <> "" And HI.UL.ULDate.CheckDate(Me.FTEndDate.Text) <> "" Then

                Dim dtemptype As System.Data.DataTable
                Dim _Dt As System.Data.DataTable
                Dim _Qry As String = ""
                Dim _QryDel As String = ""
                Dim _StateVacationRet As Integer

                If CDbl(FNExchangeRate.Value) <= 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาทำการระบุอัตราแลกเปลี่ยน !!!", 1408160001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    Me.FNExchangeRate.Focus()
                    Exit Sub
                End If

                If CDbl(FNExchangeRateTHB.Value) <= 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาทำการระบุอัตราแลกเปลี่ยน Bath/US !!!", 1409190001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    Me.FNExchangeRateTHB.Focus()
                    Exit Sub
                End If

                If Integer.Parse(FTPayYear.Text) >= 2014 Then
                    _Qry = " SELECT  TOP 1 FCCfgRetValue"
                    _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigReturnVacationSet WITH(NOLOCK) "
                    _Qry &= vbCrLf & "  WHERE (FNCalType =" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & ")"
                    _Qry &= vbCrLf & "  AND (FTCfgRetTerm = '" & HI.UL.ULF.rpQuoted(FTPayTerm.Text) & "')"
                    _StateVacationRet = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))
                Else
                    _StateVacationRet = 0
                End If

                Dim _FTStaDeductAbsent As Integer = 0
                Dim _FTStaCalPayRoll As Integer = 0
                Dim _FNStateSalaryType As Integer = 0

                _Qry = "SELECT TOP 1 FNCalType,FTStaDeductAbsent,FTStaCalPayRoll,FNStateSalaryType FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType  WITH(NOLOCK) WHERE FNHSysEmpTypeId=" & Integer.Parse(Val(FNHSysEmpTypeId.Properties.Tag.ToString)) & "  "
                dtemptype = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                Dim _TmpCalType As Integer = 0 'Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))

                Dim SDate As String = HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) '...วันที่เริ่มต้นงวดการคำนวณ Start date (Re-Calculate End Monthly)
                Dim EDate As String = HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) '...วันที่สิ้นสุดงวดการคำนวณ End date (Re-Calculate End Monthly)

                For Each R As DataRow In dtemptype.Rows
                    _TmpCalType = Integer.Parse(Val(R!FNCalType.ToString))
                    _FTStaDeductAbsent = Integer.Parse(Val(R!FTStaDeductAbsent.ToString))
                    _FTStaCalPayRoll = Integer.Parse(Val(R!FTStaCalPayRoll.ToString))
                    _FNStateSalaryType = Integer.Parse(Val(R!FNStateSalaryType.ToString))

                    Exit For

                Next
                dtemptype.Dispose()

                If _TmpCalType = 2 Or _TmpCalType = 3 Then

                    If _FTStaCalPayRoll = 1 Then
                        SDate = HI.UL.ULDate.ConvertEnDB(Microsoft.VisualBasic.Left(EDate, 8) & "01")  'วันแรกของเดือน
                        EDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(HI.UL.ULDate.AddMonth(Microsoft.VisualBasic.Left(EDate, 8) & "01", 1), -1)) 'วันสุดท้ายของเดือน
                    End If

                End If

                _Qry = Me.GenQuery(SDate, EDate, True)

                '/*DELETE LEFT TABLE (Table P) WITH LEFT JOIN RIGHT TABLE (Derived Table Alias M)*/

                _QryDel = "DELETE  P FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS P , (" & _Qry & ") AS M"
                _QryDel &= vbCrLf & "WHERE P.FNHSysEmpID = M.FNHSysEmpID"
                _QryDel &= vbCrLf & "      AND P.FTPayYear = '" & FTPayYear.Text & "'"
                _QryDel &= vbCrLf & "      AND P.FTPayTerm = '" & FTPayTerm.Text & "'"
                HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

                _QryDel = "DELETE  PF FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollCalculate AS PF , (" & _Qry & ") AS M"
                _QryDel &= vbCrLf & "WHERE PF.FNHSysEmpID = M.FNHSysEmpID"
                _QryDel &= vbCrLf & "      AND PF.FTPayYear = '" & FTPayYear.Text & "'"
                _QryDel &= vbCrLf & "      AND PF.FTPayTerm = '" & FTPayTerm.Text & "'"
                HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

                _QryDel = "DELETE PF FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin As PF, (" & _Qry & ")  AS M"
                _QryDel &= vbCrLf & "WHERE PF.FNHSysEmpID = M.FNHSysEmpID"
                _QryDel &= vbCrLf & "      AND PF.FTPayYear = '" & FTPayYear.Text & "'"
                _QryDel &= vbCrLf & "      AND PF.FTPayTerm = '" & FTPayTerm.Text & "'"
                HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

                _QryDel = "DELETE PFM FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFinCalculate As PFM, (" & _Qry & ")  AS M"
                _QryDel &= vbCrLf & "WHERE PFM.FNHSysEmpID = M.FNHSysEmpID"
                _QryDel &= vbCrLf & "      AND PFM.FTPayYear = '" & FTPayYear.Text & "'"
                _QryDel &= vbCrLf & "      AND PFM.FTPayTerm = '" & FTPayTerm.Text & "'"
                HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

                _QryDel = "DELETE PML FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollLeave AS PML, (" & _Qry & ") AS M"
                _QryDel &= vbCrLf & "WHERE PML.FNHSysEmpID = M.FNHSysEmpID"
                _QryDel &= vbCrLf & "      AND PML.FTPayYear = '" & FTPayYear.Text & "'"
                _QryDel &= vbCrLf & "      AND PML.FTPayTerm = '" & FTPayTerm.Text & "'"
                HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

                _QryDel = "DELETE PMC FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTManageCalculate AS PMC, (" & _Qry & ") AS M"
                _QryDel &= vbCrLf & "WHERE PMC.FNHSysEmpID = M.FNHSysEmpID"
                _QryDel &= vbCrLf & "      AND PMC.FTPayYear = '" & FTPayYear.Text & "'"
                _QryDel &= vbCrLf & "      AND PMC.FTPayTerm = '" & FTPayTerm.Text & "'"
                HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)


                _Qry = Me.GenQuery(SDate, EDate)
                _Qry &= vbCrLf & " ORDER BY  M.FTEmpCode"

                'Dim _Spls As New HI.TL.SplashScreen("Prepre Data For Calculate.. Please Wait ")
                Dim _Spls As HI.TL.SplashScreen

                If Not (System.Diagnostics.Debugger.IsAttached) = True Then
                    _Spls = New HI.TL.SplashScreen("Prepre Data For Calculate.. Please Wait")
                End If

                _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                Dim _Rec As Integer = 0
                Dim _RecError As Integer = 0
                Dim _TotalRec As Integer = _Dt.Rows.Count

                Calculate.LoadSocialRate()
                Calculate.LoadTaxRate()
                Calculate.LoadDiscountTax()

                '...Factory Vietnam
                Calculate.LoadInsuranceVNRate()

                '...business workday
                Dim FNMaxWorkday As Integer = 26

                _Qry = ""
                _Qry = "SELECT [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.[FN_BUSINESS_WORKING_DAY](" & Integer.Parse(FTPayYear.Text.Trim) & ", " & Integer.Parse(FTPayTerm.Text.Trim) & ", " & FNMaxWorkday & ") AS FNBusinessWorkday;"

                Dim FNBusinessWorkday As Integer
                'FNBusinessWorkday = Val(HI.Conn.SQLConn.GetField(_Qry, HI.Conn.DB.DataBaseName.DB_HR, "26")) '...workingday per month
                FNBusinessWorkday = HI.HRVN.Calculate.GetDayPerMonth(FTPayTerm.Text, FTPayYear.Text, Integer.Parse(FNHSysEmpTypeId.Properties.Tag.ToString))

                If System.Diagnostics.Debugger.IsAttached = True Then
                    MsgBox("FNBusiness Workday : {" & String.Format("{0} days.", FNBusinessWorkday) & "}" & Environment.NewLine & "Payment Year : " & FTPayYear.Text.Trim & Environment.NewLine & "Payment Term : " & FTPayTerm.Text.Trim, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "Re-Calcualte Month End...")
                End If

                Dim _dttmpemp As New System.Data.DataTable
                '...invoke form wListEmplyeeNotCalPayroll when after re-calculate payroll
                _dttmpemp.Columns.Add("FTEmpCode", GetType(String))
                _dttmpemp.Columns.Add("FTEmpName", GetType(String))

                For Each R As DataRow In _Dt.Rows

                    _Rec = _Rec + 1

                    REM _Spls.UpdateInformation("Calculating... Employee Code " & R!FTEmpCode.ToString & "    " & R!FTEmpName.ToString & "  Record  " & _Rec.ToString & " Of " & _TotalRec.ToString & "  (" & Format((_Rec * 100.0) / _TotalRec, "0.00") & " % ) ")

                    If Not System.Diagnostics.Debugger.IsAttached = True Then
                        _Spls.UpdateInformation("Calculating... Employee Code " & R!FTEmpCode.ToString & "    " & R!FTEmpName.ToString & "  Record  " & _Rec.ToString & " Of " & _TotalRec.ToString & "  (" & Format((_Rec * 100.0) / _TotalRec, "0.00") & " % ) ")
                    End If

                    If Calculate.CalculateWeekEnd_VN(HI.ST.UserInfo.UserName, R!FNHSysEmpID.ToString _
                                            , R!FNHSysEmpTypeId.ToString, HI.UL.ULDate.ConvertEnDB(FTStartDate.Text), HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) _
                                            , FTPayYear.Text, FTPayTerm.Text, FDPayDate.Text, R!FTDeligentCode.ToString, _TmpCalType.ToString, FNBusinessWorkday _
                                            , False, _StateVacationRet, _FTStaDeductAbsent, _FTStaCalPayRoll, _FNStateSalaryType, FNExchangeRate.Value, FNExchangeRateTHB.Value) = False Then

                        _dttmpemp.Rows.Add(R!FTEmpCode.ToString, R!FTEmpName.ToString)

                        _RecError = _RecError + 1

                    End If

                Next

                _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT"
                _Qry &= vbCrLf & "SET FNExchangeRate = " & FNExchangeRate.Value & ","
                _Qry &= vbCrLf & "    FNExchangeRateTHB = " & FNExchangeRateTHB.Value & ""
                _Qry &= vbCrLf & "WHERE FNHSysEmpTypeId = " & Integer.Parse(Val(FNHSysEmpTypeId.Properties.Tag.ToString)) & " "
                _Qry &= vbCrLf & "      AND FTPayYear = '" & FTPayYear.Text & "'"
                _Qry &= vbCrLf & "      AND FTPayTerm = '" & FTPayTerm.Text & "'"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                REM _Spls.Close()

                If Not System.Diagnostics.Debugger.IsAttached = True Then
                    _Spls.Close()
                End If

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
                    Dim _Dt As System.Data.DataTable

                    _Qry = "SELECT TOP 1  FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WITH(NOLOCK) WHERE FTEmpTypeCode=N'" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "' AND FNHSysCmpId=" & HI.ST.SysInfo.CmpID
                    FNHSysEmpTypeId.Properties.Tag = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0"))

                    _Qry = " SELECT        D.FTPayTerm, D.FTPayYear, D.FNHSysEmpTypeId, D.FNMonth, D.FTTermOfMonth"
                    _Qry &= vbCrLf & " , D.FDPayDate, D.FDCalDateBegin, D.FDCalDateEnd, D.FDDateClose, "
                    _Qry &= vbCrLf & "  D.FTStateTermEndOfYear,D.FDPayDate,D.FNExchangeRate,D.FNExchangeRateTHB "

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
                        Me.FNExchangeRate.Value = Val(R!FNExchangeRate)
                        Me.FNExchangeRateTHB.Value = Val(R!FNExchangeRateTHB)

                        If Val(R!FNExchangeRate.ToString) = 0 Then

                            _Qry = " SELECT TOP 1  Max(ISNULL(FNExchangeRate,0) )  AS FNExchangeRate FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS D WITH (NOLOCK) "
                            _Qry &= vbCrLf & " WHERE D.FDCalDateEnd ='" & HI.UL.ULF.rpQuoted(R!FDCalDateEnd.ToString) & "' "
                            _Qry &= vbCrLf & " AND ISNULL(FNExchangeRate,0) > 0 "

                            Me.FNExchangeRate.Value = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))

                            _Qry = " SELECT TOP 1  Max(ISNULL(FNExchangeRateTHB,0) )  AS FNExchangeRateTHB FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS D WITH (NOLOCK) "
                            _Qry &= vbCrLf & " WHERE D.FDCalDateEnd ='" & HI.UL.ULF.rpQuoted(R!FDCalDateEnd.ToString) & "' "
                            _Qry &= vbCrLf & " AND ISNULL(FNExchangeRateTHB,0) > 0 "

                            Me.FNExchangeRateTHB.Value = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))

                        End If

                        Exit For

                    Next

                End If
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub wCalculateWeekend_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode  '...default company
        Me.FNEmpTypeGroup.SelectedIndex = -1
    End Sub

#End Region

    Private Sub FNHSysEmpTypeId_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FNHSysEmpTypeId.KeyDown

    End Sub

    Private Sub FNHSysEmpTypeId_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles FNHSysEmpTypeId.KeyPress

    End Sub

    Private Sub FNHSysEmpTypeId_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FNHSysEmpTypeId.KeyUp

    End Sub

    Private Sub FNHSysEmpTypeId_LostFocus(sender As Object, e As EventArgs) Handles FNHSysEmpTypeId.LostFocus

    End Sub

    Private Sub FNHSysDivisonId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysDivisonId.EditValueChanged

    End Sub

    Private Sub FNHSysDivisonId_Spin(sender As Object, e As DevExpress.XtraEditors.Controls.SpinEventArgs) Handles FNHSysDivisonId.Spin

    End Sub

    Private Sub FNEmpTypeGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNEmpTypeGroup.SelectedIndexChanged
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.FNHSysEmpTypeId_ValueChanged(AddressOf FNHSysEmpTypeId_EditValueChanged), New Object() {sender, e})
            Else
                If FNEmpTypeGroup.SelectedIndex >= 0 Then
                    Dim _Qry As String = ""
                    Dim _Dt As System.Data.DataTable

                    '_Qry = "SELECT TOP 1  FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WITH(NOLOCK) WHERE FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "' AND FNHSysCmpId=" & HI.ST.SysInfo.CmpID
                    'FNHSysEmpTypeId.Properties.Tag = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0"))



                    _Qry = " SELECT   TOP 1     D.FTPayTerm, D.FTPayYear, D.FNHSysEmpTypeId, D.FNMonth, D.FTTermOfMonth"
                    _Qry &= vbCrLf & " , D.FDPayDate, D.FDCalDateBegin, D.FDCalDateEnd, D.FDDateClose, "
                    _Qry &= vbCrLf & "  D.FTStateTermEndOfYear,D.FDPayDate "

                    If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                        _Qry &= vbCrLf & " , V_ListMonth.FTNameTH  AS FTMonth "
                    Else
                        _Qry &= vbCrLf & " , V_ListMonth.FTNameEN AS FTMonth "
                    End If

                    _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayHD AS H WITH (NOLOCK) INNER JOIN "
                    _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType E ON H.FNHSysEmpTypeId= E.FNHSysEmpTypeId INNER JOIN "
                    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS D WITH (NOLOCK) ON H.FTPayTerm = D.FTPayTerm "
                    _Qry &= vbCrLf & "  AND H.FTPayYear = D.FTPayYear AND "
                    _Qry &= vbCrLf & "    H.FNHSysEmpTypeId = D.FNHSysEmpTypeId INNER JOIN"
                    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_ListMonth ON D.FNMonth = V_ListMonth.FNListIndex "
                    _Qry &= vbCrLf & " WHERE E.FNEmpTypeGroup =" & Val(FNEmpTypeGroup.SelectedIndex.ToString) & " AND E.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "
                    _Qry &= vbCrLf & " ORDER BY D.FTPayTerm DESC , D.FTPayYear DESC  "

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
                    Me.FNHSysEmpTypeId.Text = ""
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub
End Class