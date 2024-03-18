Imports HI.TL
Imports HI.HRCAL

Public Class wAdjustPayroll_CVN

#Region "Variable"

    Private _InitLoad As Boolean = False
    Private _RateOT1 As Double
    Private _RateOT15 As Double
    Private _RateOT2 As Double
    Private _RateOT4 As Double
    Private _RateOT3 As Double

#End Region

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.FNHSysCmpId.Text = "1"
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Me.FNHSysCmpId.Properties.ReadOnly = True
        Me.FNHSysCmpId.Properties.Buttons(0).Visible = False
        Me.FNHSysCmpId.Properties.Buttons(0).Enabled = False
    End Sub

#Region "Property"

    Private _FNCalType As Integer = 0
    Property FNCalType As Integer
        Get
            Return _FNCalType
        End Get
        Set(ByVal value As Integer)
            _FNCalType = value
        End Set
    End Property

    Private _FNSalaryDivide As Integer = 1
    Property FNSalaryDivide As Integer
        Get
            Return _FNSalaryDivide
        End Get
        Set(ByVal value As Integer)
            _FNSalaryDivide = value
        End Set
    End Property

    Private _SalaryPerDay As Double = 0
    Property SalaryPerDay As Integer
        Get
            Return _SalaryPerDay
        End Get
        Set(ByVal value As Integer)
            _SalaryPerDay = value
        End Set
    End Property

#End Region

#Region "Procedure"

    Private Sub LoadEmpInfo(ByVal FNHSysEmpID As String)
        _InitLoad = True


        Dim _PathEmpPic As String
        _PathEmpPic = ""
        Dim cmdstring As String = "Select Top 1 FTCfgData FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE FTCfgName='PathEmpPic'"

        _PathEmpPic = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "")

        Dim _dt As DataTable
        Dim _Dt2 As DataTable

        Dim _Qry As String = ""
        _Qry = " SELECT    TOP 1     M.FTEmpCode, M.FTEmpCodeRefer, M.FTEmpNameTH, M.FTEmpSurnameTH, M.FTEmpNicknameTH, M.FTEmpNameEN, M.FNHSysEmpTypeId, M.FNHSysDeptId, "
        _Qry &= vbCrLf & "   D.FTDeptCode, Di.FTDivisonCode, M.FNHSysDivisonId, M.FNHSysSectId, S.FTSectCode, ET.FTEmpTypeCode, M.FNHSysUnitSectId, US.FTUnitSectCode,"
        _Qry &= vbCrLf & "  M.FNHSysEmpID, M.FTEmpPicName, M.FNHSysPositId, P.FTPositCode,M.FNSalary,ET.FNCalType,ET.FNSalaryDivide "
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId = P.FNHSysPositId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
        _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID  =" & Val(FNHSysEmpID) & " "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        FTEmpPicName.Image = Nothing
        FNSalary.Value = 0

        HI.TL.HandlerControl.ClearControl(Me.ogbpay)

        If _dt.Rows.Count > 0 Then
            For Each R As DataRow In _dt.Rows
                If _PathEmpPic = "" Then
                    FTEmpPicName.Image = HI.UL.ULImage.LoadImage(HI.ST.SysInfo.SysPath & "EmpPicture\" & R!FTEmpPicName.ToString)
                Else
                    FTEmpPicName.Image = HI.UL.ULImage.LoadImage(_PathEmpPic & R!FTEmpPicName.ToString)
                End If
                FNHSysEmpTypeId.Text = R!FTEmpTypeCode.ToString
                FNHSysDeptId.Text = R!FTDeptCode.ToString
                FNHSysDivisonId.Text = R!FTDivisonCode.ToString
                FNHSysSectId.Text = R!FTSectCode.ToString
                FNHSysUnitSectId.Text = R!FTUnitSectCode.ToString
                FNHSysPositId.Text = R!FTPositCode.ToString

                Me.FNCalType = Val(R!FNCalType.ToString)

                If Val(R!FNSalaryDivide.ToString) <= 0 Then
                    Me.FNSalaryDivide = 1
                Else
                    Me.FNSalaryDivide = Val(R!FNSalaryDivide.ToString)
                End If

                FNSalary.Value = Format(Val(R!FNSalary.ToString) / Me.FNSalaryDivide, "0.00")

                Select Case Val(R!FNCalType.ToString)
                    Case 0, 1
                        Me.SalaryPerDay = FNSalary.Value / Me.FNSalaryDivide
                    Case 2
                        Me.SalaryPerDay = FNSalary.Value / 30
                End Select


                _Qry = " SELECT        D.FTPayTerm, D.FTPayYear, D.FNHSysEmpTypeId, D.FNMonth, D.FTTermOfMonth"
                _Qry &= vbCrLf & " , D.FDPayDate, D.FDCalDateBegin, D.FDCalDateEnd, D.FDDateClose, "
                _Qry &= vbCrLf & "  D.FTStateTermEndOfYear,D.FDPayDate "

                If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                    _Qry &= vbCrLf & " , V_ListMonth.FTNameTH  AS FTMonth "
                Else
                    _Qry &= vbCrLf & " , V_ListMonth.FTNameEN AS FTMonth "
                End If

                _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayHD AS H WITH (NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS D WITH (NOLOCK) ON H.FTPayTerm = D.FTPayTerm "
                _Qry &= vbCrLf & "  AND H.FTPayYear = D.FTPayYear AND "
                _Qry &= vbCrLf & "    H.FNHSysEmpTypeId = D.FNHSysEmpTypeId INNER JOIN"
                _Qry &= vbCrLf & "    V_ListMonth ON D.FNMonth = V_ListMonth.FNListIndex"
                _Qry &= vbCrLf & " WHERE H.FNHSysEmpTypeId =" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "

                _Dt2 = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                For Each R2 As DataRow In _Dt2.Rows

                    Me.FTStartDate.Text = HI.UL.ULDate.ConvertEN(R2!FDCalDateBegin.ToString)
                    Me.FTEndDate.Text = HI.UL.ULDate.ConvertEN(R2!FDCalDateEnd.ToString)
                    Me.FTMonth.Text = R2!FTMonth.ToString
                    Me.FTPayTerm.Text = R2!FTPayTerm.ToString
                    Me.FTPayYear.Text = R2!FTPayYear.ToString
                    Me.FDPayDate.Text = HI.UL.ULDate.ConvertEN(R2!FDPayDate.ToString)

                    Exit For

                Next

                Exit For
            Next
        Else
            FNHSysEmpTypeId.Text = ""
            FNHSysDeptId.Text = ""
            FNHSysDivisonId.Text = ""
            FNHSysSectId.Text = ""
            FNHSysUnitSectId.Text = ""
            FNHSysPositId.Text = ""
            Me.FDPayDate.Text = ""
            Me.FTStartDate.Text = ""
            Me.FTEndDate.Text = ""
            Me.FTMonth.Text = ""
            Me.FTPayTerm.Text = ""
            Me.FTPayYear.Text = ""
            FNSalary.Value = 0
        End If

        Dim _dtot As DataTable
        _Qry = " SELECT FTCfgOTCode,FCCfgOTValue,ISNULL(FCCfgOTAmtPlus,0) AS FCCfgOTAmtPlus  "
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigOTSet WITH (NOLOCK) "
        _Qry &= vbCrLf & "  WHERE  (FNCalType  = " & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & ")"
        _dtot = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        For Each R3 As DataRow In _dtot.Rows
            Select Case R3!FTCfgOTCode.ToString.ToUpper
                Case "01"
                    _RateOT1 = Val(R3!FCCfgOTValue.ToString)
                Case "02"
                    _RateOT15 = Val(R3!FCCfgOTValue.ToString)
                Case "03"
                    _RateOT2 = Val(R3!FCCfgOTValue.ToString)
                Case "04"
                    _RateOT3 = Val(R3!FCCfgOTValue.ToString)
                Case "05"
                    _RateOT4 = Val(R3!FCCfgOTValue.ToString)
            End Select

        Next

        Call SetShowFinance(FNHSysEmpID)
        Call LoadPayroll()

        _InitLoad = False
    End Sub

    Private Sub SetShowFinance(ByVal EmpCode As String)
        Dim oDbdt As New DataTable
        Dim _Qry As String

        Try

            _Qry = "SELECT FTFinCode,FTFinDesc,FTFinAmt,FTStaSocial,FTStaTax,FCFinAmt,FCFinAmtOther "
            _Qry &= vbCrLf & " FROM ("
            _Qry &= vbCrLf & " SELECT THRMFinanceSet.FTFinCode,FNFinSeqNo"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & ",FTFinDescTH AS FTFinDesc"
            Else
                _Qry &= vbCrLf & ",FTFinDescEN AS FTFinDesc"
            End If

            _Qry &= vbCrLf & ",CASE WHEN ISNULL(THRMEmployeeFin.FTFinCode,'')='' THEN   Convert(numeric(18,2),0.00) ELSE   FTFinAmt  END AS FTFinAmt"
            _Qry &= vbCrLf & ",ISNULL(THRMEmployeeFin.FCFinAmt,0) AS FCFinAmt, ISNULL(THRMEmployeeFin.FCFinAmtOther,0)  AS FCFinAmtOther"
            _Qry &= vbCrLf & ",  THRMFinanceSet.FTStaSocial, THRMFinanceSet.FTStaTax"
            _Qry &= vbCrLf & " FROM "
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT FTFinCode,FTType,FTStaSocial,FTStaTax "
            _Qry &= vbCrLf & " FROM THRMFinanceSet WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE   ISNULL(FTStaActive,'')='1' AND (FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " OR FNHSysCmpId=0) "
            _Qry &= vbCrLf & ") THRMFinanceSet"
            _Qry &= vbCrLf & " INNER JOIN THRMFinance WITH(NOLOCK) ON THRMFinanceSet.FTFinCode=THRMFinance.FTFinCode AND FTFinType='1' AND FTStaActive='1'"
            _Qry &= vbCrLf & " Left JOIN"
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT FTFinCode,FCFinAmt,FCFinAmtOther,FCTotalFinAmt As FTFinAmt "
            _Qry &= vbCrLf & " FROM THRTPayRollFin  WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTPayYear='" & FTPayYear.Text & "' AND FTPayTerm='" & FTPayTerm.Text & "' AND FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & ") THRMEmployeeFin"
            _Qry &= vbCrLf & " ON THRMFinanceSet.FTFinCode=THRMEmployeeFin.FTFinCode"
            _Qry &= vbCrLf & " ) T  "


            _Qry &= vbCrLf & "  WHERE ISNULL(FTFinDesc,'') <> '' "
            _Qry &= vbCrLf & " ORDER BY FNFinSeqNo"
            ogdIncome.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            _Qry = "SELECT FTFinCode,FTFinDesc,FTFinAmt,FTStaSocial,FTStaTax,FCFinAmt,FCFinAmtOther "
            _Qry &= vbCrLf & " FROM ("
            _Qry &= vbCrLf & " SELECT THRMFinanceSet.FTFinCode,FNFinSeqNo"
            _Qry &= vbCrLf & ",  THRMFinanceSet.FTStaSocial, THRMFinanceSet.FTStaTax"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & ",FTFinDescTH AS FTFinDesc"
            Else
                _Qry &= vbCrLf & ",FTFinDescEN AS FTFinDesc"
            End If

            _Qry &= vbCrLf & ",CASE WHEN ISNULL(THRMEmployeeFin.FTFinCode,'')='' THEN   Convert(numeric(18,2),0.00) ELSE   FTFinAmt END AS FTFinAmt"
            _Qry &= vbCrLf & ",ISNULL(THRMEmployeeFin.FCFinAmt,0) AS FCFinAmt, ISNULL(THRMEmployeeFin.FCFinAmtOther,0)  AS FCFinAmtOther"
            _Qry &= vbCrLf & " FROM "
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT FTFinCode,FTType,FTStaSocial,FTStaTax "
            _Qry &= vbCrLf & " FROM THRMFinanceSet WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE   ISNULL(FTStaActive,'')='1' AND (FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " OR FNHSysCmpId=0) "
            _Qry &= vbCrLf & ") THRMFinanceSet"
            _Qry &= vbCrLf & " INNER JOIN THRMFinance WITH(NOLOCK) ON THRMFinanceSet.FTFinCode=THRMFinance.FTFinCode AND FTFinType='2' AND FTStaActive='1'"
            _Qry &= vbCrLf & " Left JOIN"
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT FTFinCode,FCFinAmt,FCFinAmtOther,FCTotalFinAmt AS FTFinAmt"
            _Qry &= vbCrLf & " FROM THRTPayRollFin WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPayYear='" & FTPayYear.Text & "' AND FTPayTerm='" & FTPayTerm.Text & "' AND FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & ""
            _Qry &= vbCrLf & ") THRMEmployeeFin"
            _Qry &= vbCrLf & " ON THRMFinanceSet.FTFinCode=THRMEmployeeFin.FTFinCode"
            _Qry &= vbCrLf & " ) T "

            _Qry &= vbCrLf & "  WHERE ISNULL(FTFinDesc,'') <> '' "
            _Qry &= vbCrLf & " ORDER BY FNFinSeqNo"

            ogdDeduct.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
        End Try

    End Sub

    Private Sub LoadPayroll()
        Dim _Qry As String
        Dim _dt As DataTable

        _Qry = " SELECT        FTPayYear, FTPayTerm, FNHSysEmpID, FNHoliday, FNSalary, FNWorkingHour,FNOt1, FNOt15, FNOt2, FNOt3, FNOt4, FNTotalLeavePay, "
        _Qry &= vbCrLf & "   FNTotalLeaveNotPay, FNTotalLeave, FNTotalWKNMin,  FNOt1Min, FNOt15Min, FNOt2Min, FNOt3Min, FNOt4Min, FNTotalLateMin, FNLateCutMin,"
        _Qry &= vbCrLf & "  FNLateCutAbsentMin, FNAbsentMin, FNTotalWKMin, FNTotalLeavePayMin, FNTotalLeaveNotPayMin, FNTotalLeaveMin, FCBaht,  FCOt1_Baht,"
        _Qry &= vbCrLf & "  FCOt15_Baht, FCOt2_Baht, FCOt3_Baht, FCOt4_Baht, FCNetBaht, FNDiligentBaht, FNPayLeaveVacationBaht, FNPayLeaveOtherBaht, FNLateCutAmt,"
        _Qry &= vbCrLf & "  FNLateCutAbsentAmt, FNAbsentAmt, FNTotalRecalSSO, FNTotalRecalTAX, FNTotalAdd, FNTotalAddOther, FNTotalExpense, FNTotalExpenseOther, FNTotalIncome,"
        _Qry &= vbCrLf & "  FNSocial, FNTax, FHolidayBaht, FNNetpay, FNAccumulateIncomeYear, FNAccumulateSocialYear, FNAccumulateTax, FTStateInDustin, FNTotalCalContributedAmt,"
        _Qry &= vbCrLf & "  FNContributedAmt, FNCmpContributedAmt, FNTotalCalWorkmen, FNWorkmenAmt,  FNAmtRetire,"
        _Qry &= vbCrLf & "  FNPayLeaveSSo, FNWorkingDay, FNAdjBeforeCal"
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS P WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTPayYear='" & Me.FTPayYear.Text & "' AND  FTPayTerm='" & Me.FTPayTerm.Text & "' AND  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        For Each R As DataRow In _dt.Rows

            Me.FNSalary.Value = Val(R!FNSalary.ToString)
            Me.FNTax.Value = Val(R!FNTax.ToString)
            Me.FNSocial.Value = Val(R!FNSocial.ToString)
            Me.FNWorkingDay.Value = Val(R!FNWorkingDay.ToString)
            Me.FCBaht.Value = Val(R!FCBaht.ToString)
            Me.FNOt1.Value = Val(R!FNOt1.ToString)
            Me.FNOt15.Value = Val(R!FNOt15.ToString)
            Me.FNOt2.Value = Val(R!FNOt2.ToString)
            Me.FNOt3.Value = Val(R!FNOt3.ToString)
            Me.FNOt4.Value = Val(R!FNOt4.ToString)
            Me.FCOt1_Baht.Value = Val(R!FCOt1_Baht.ToString)
            Me.FCOt15_Baht.Value = Val(R!FCOt15_Baht.ToString)
            Me.FCOt2_Baht.Value = Val(R!FCOt2_Baht.ToString)
            Me.FCOt3_Baht.Value = Val(R!FCOt3_Baht.ToString)
            Me.FCOt4_Baht.Value = Val(R!FCOt4_Baht.ToString)
            Me.FNNetpay.Value = Val(R!FNNetpay.ToString)
            Me.FNHoliday.Value = Val(R!FNHoliday.ToString)
            Me.FHolidayBaht.Value = Val(R!FHolidayBaht.ToString)
            Me.FNContributedAmt.Value = Val(R!FNContributedAmt.ToString)
            Me.FNCmpContributedAmt.Value = Val(R!FNCmpContributedAmt.ToString)
            Me.FNTotalRecalSSO.Value = Val(R!FNTotalRecalSSO.ToString)
            Me.FNDiligentBaht.Value = Val(R!FNDiligentBaht.ToString)
            Me.FNPayLeaveVacationBaht.Value = Val(R!FNPayLeaveVacationBaht.ToString)
            Me.FNPayLeaveOtherBaht.Value = Val(R!FNPayLeaveOtherBaht.ToString)

        Next

    End Sub

#End Region

#Region "General"

    Private Sub FNHSysEmpID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNHSysEmpID.EditValueChanged
        If Me.InvokeRequired Then
            Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FNHSysEmpID_EditValueChanged), New Object() {sender, e})
        Else
            If FNHSysEmpID.Text <> "" Then

                Dim _Qry As String = "SELECT TOP 1 FNHSysEmpID  FROM THRMEmployee WITH(NOLOCK) WHERE FTEmpCode ='" & HI.UL.ULF.rpQuoted(FNHSysEmpID.Text) & "' "

                _Qry = HI.ST.Security.PermissionEmpType(_Qry)

                FNHSysEmpID.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

                Call LoadEmpInfo(FNHSysEmpID.Properties.Tag.ToString)
            Else

            End If
        End If
    End Sub

    Private Sub ocmexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Me.FNHSysEmpID.Focus()
    End Sub

    Private Function SaveData(ByVal oSpls As HI.TL.SplashScreen) As Boolean
        Try

            CType(ogdIncome.DataSource, DataTable).AcceptChanges()
            CType(ogdDeduct.DataSource, DataTable).AcceptChanges()

            Dim _Qry As String = ""
            Dim _EmpTaxYear As New HCfg.EmpTaxYear
            Dim _EmpDisTax As New HCfg.EmployeeDiscountTax
            Dim _Totalcaltax As Double = 0

            HI.HRCAL.Calculate.LoadSocialRate()
            HI.HRCAL.Calculate.LoadTaxRate()
            HI.HRCAL.Calculate.LoadDiscountTax()

            '-------------------- Config ค่าลดหย่อน และการส่งเข้าก่องทุนต่างๆ--------------------
            _Qry = "   SELECT        M.FNHSysCmpId As FTCmpCode, M.FNHSysEmpID AS FTEmpCode, M.FDDateStart, M.FDDateEnd, M.FDDateProbation, M.FTProbationSta, M.FNEmpStatus AS FTStatus, M.FNHSysEmpTypeId AS FTTypeEmp"
            _Qry &= vbCrLf & " ,'' AS FTLineCode, '' As FTGrpCode, M.FNHSysDeptId AS FTDeptCode, "
            _Qry &= vbCrLf & "  '' AS FTFactoryCode, M.FNHSysDivisonId AS FTDivCode, M.FNHSysSectId AS FTSectCode, '' AS FTSecUnitCode, M.FNHSysUnitSectId AS FTUnitSecCode"
            _Qry &= vbCrLf & " , M.FNHSysPositId AS FTPositCode,'' as FTJobGrade,'' AS FTCostCNCode, M.FNLateCutSta AS FTLateCutSta,"
            _Qry &= vbCrLf & "   M.FNPaidOTSta AS FTPaidOTSta, M.FTEmpIdNo, M.FTSocialNo, M.FTTaxNo, M.FNCalSocialSta AS FTCalSocialSta, M.FNCalTaxSta AS FTCalTaxSta, M.FNHSysPayRollPayId AS FTPayCode"
            _Qry &= vbCrLf & " , M.FTAccNo, M.FNHSysBankId AS FTBnkCode, M.FNHSysBankBranchId AS FTBnkBchCode,M.FNSalary AS FTSalary, "
            _Qry &= vbCrLf & "  M.FCIncomeBefore, M.FCTaxBefore, M.FCSocialBefore, M.FCDisabledDependents, M.FCHealthInsurFatherMotherMate, M.FTHealthInsurIDFather,"
            _Qry &= vbCrLf & "   ET.FNCalType AS FTCalType, ET.FNInsurType AS FTInsurType,M.FNMaritalStatus AS FTMaritalCode,M.FDFundBegin, M.FDFundEnd,"

            '------------------------- ลดหย่อนต่างๆ -----------------------------------
            _Qry &= vbCrLf & " M.FCModFather, M.FCModMother, M.FCModMateFather, M.FCModMateMother, "
            _Qry &= vbCrLf & " M.FCPremium, M.FCInterest, M.FCUnitRMF, M.FCUnitLTF, M.FCDeductDonate, M.FCDisabledDependents,M.FCDeductDonateStudy, "
            _Qry &= vbCrLf & "  M.FCHealthInsurFatherMotherMate, M.FTHealthInsurIDFather,M.FTHealthInsurIDMother,"
            _Qry &= vbCrLf & " M.FTHealthInsurIDFatherMate, M.FTHealthInsurIDMotherMate,M.FTMateIncome,M.FCExceptAgeOver,M.FCExceptAgeOverMate,M.FCDeductDividend "
            '------------------------- ลดหย่อนต่างๆ -----------------------------------

            '------------------------- อายุงาน -----------------------------------
            _Qry &= vbCrLf & ", CASE WHEN ISDATE(M.FdDateStart) = 1 AND ISDATE(M.FDRetire) = 1 THEN  Datediff(month,M.FdDateStart,M.FDRetire) ELSE 0 END AS FNWorkAge,ISNULL(ET.FNSalaryDivide,0) AS FNSalaryDivide"
            '------------------------- อายุงาน -----------------------------------
            _Qry &= vbCrLf & ",ISNULL(ET.FTStatePayHoliday,'') AS FTStatePayHoliday "
            _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "	WHERE     (M.FNHSysEmpID =" & Val(Val(Me.FNHSysEmpID.Properties.Tag.ToString)) & " ) "

            Dim _Dtemp As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            Dim _Brach As String = ""
            Dim _FTEmpIdNo As String
            Dim _PayYear As String = Me.FTPayYear.Text
            Dim _PayTerm As String = Me.FTPayTerm.Text
            Dim _FCAccumulateIncome As Double = 0
            Dim _FCAccumulateTax As Double = 0
            Dim _FCAccumulateSocial As Double = 0
            Dim CountTerm As Double = 0
            Dim FTTotalCalContributedAcc As Double = 0
            Dim FTTotalCalWorkmenAcc As Double = 0
            Dim _TotalInstalment As Integer
            Dim _Instalment As Integer = Val(FTPayTerm.Text)
            Dim _TaxAmt As Double = 0

            For Each R As DataRow In _Dtemp.Rows

                If R!FTCalType.ToString = "2" Then
                    _TotalInstalment = 12
                Else
                    _TotalInstalment = 24
                End If

                _Qry = " SELECT      FNHSysEmpID, FTChildSex, FTStudySta"
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeChild WITH(NOLOCK) "
                _Qry &= vbCrLf & " WHERE   (FNHSysEmpID = " & Val(Val(Me.FNHSysEmpID.Properties.Tag.ToString)) & ")"

                Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                Dim _ChildNotStudy As Integer = 0
                Dim _ChildStudy As Integer = 0

                For Each _Drow As DataRow In _dt.Rows
                    If _Drow!FTStudySta.ToString = "1" Then
                        _ChildStudy = _ChildStudy + 1
                    Else
                        _ChildNotStudy = _ChildNotStudy + 1
                    End If
                Next

                _FTEmpIdNo = R!FTTaxNo.ToString ' R!FTEmpIdNo.ToString
                '----------- Get Summary ------------------
                HI.HRCAL.Calculate.LoadIncomeTax(_FTEmpIdNo, _PayYear, _PayTerm, _FCAccumulateIncome, _FCAccumulateTax, _FCAccumulateSocial, CountTerm, FTTotalCalContributedAcc, FTTotalCalWorkmenAcc, Integer.Parse(Val(Me.FNHSysEmpID.Properties.Tag.ToString)))
                '----------- Get Summary ------------------

                With _EmpTaxYear
                    .FTAmt = 0 'เงินได้ก่อนหักค่าใช้จ่าย
                    .FTExpenses = 0 'ค่าใช้จ่ายส่วนตัว
                    .FTNetAmt = 0 'เงินได้หลังหักค่าใช้จ่าย
                    .FTModEmp = 0 'ลดหย่อนส่วนตัว
                    .FTModMate = 0 'ลดหย่อนคู่สมรส
                    .FNChildNotLern = 0 'จำนวนบุตรไม่ศึกษา
                    .FNChildLern = 0 'จำนวนบุตรศึกษา
                    .FTChildNotLern = 0 'ลดหย่อนบุตรไม่ศึกษา
                    .FTChildLern = 0 'ลดหย่อนบุตรศึกษา
                    .FTInsurance = 0 'ลดหย่อนเบี้ยประกัน
                    .FTProvidentfund = 0 'กองทุนเลียงชีพส่วนที่ไม่เกิน 10000
                    .FTInterest = 0 'ดอกเบี้ยเงินกู้
                    .FTSocial = 0 'ประกันสังคม
                    .FTDonation = 0 'เงินบริจาค
                    .FTProvidentfundOver = 0 'กองทุนเลียงชีพส่วนที่เกิน 10000
                    .FTGPF = 0 'เงิน กบข.
                    .FTSavingsFund = 0 'เงินกองทุนสงเคราะห์
                    .FTCommutation = 0 'เงินชดเชยตามกฎหมายแรงงาน
                    .FTUnitRMF = 0 'ค่าซื้อหน่วยลงทุน RTF
                    .FTModFather = 0 'ลดหย่อนบิดา
                    .FTModMother = 0 'ลดหย่อนมารดา
                    .FTModFatherMate = 0 'ลดหย่อนบิดาคู่สมรส
                    .FTModMotherMate = 0 'ลดหย่อนมารดาคู่สมรส
                    .FTUnitLTF = 0 'ค่าซื้อหน่วยลงทุน LTF
                    .FTDonationLern = 0 'เงินบริจาคเพื่อสนับสนุนการศึกษา
                    .FTParentsHealthInsurance = 0 'เบี้ยประกันสุขภาพบิดามารดา
                    .FTSupportSport = 0 'เงินสนับสนุนการกีฬา
                    .FTAcquisitionOfProperty = 0 'ค่าซื้ออาคาร
                    .FTPension = 0 'บำนาญ
                    .FTTravel = 0 'ท่องเที่ยวในประเทศ
                    .FTTotalCalTax = 0 'เงินได้สุทธิ
                    .FTTotalTax = 0 'ภาษีที่ต้องจ่าย
                End With


                With _EmpDisTax

                    .BaseSlary = 0
                    .OtherSlary = 0
                    .BeforeIncom = _FCAccumulateIncome
                    .BeforeTax = _FCAccumulateTax
                    .FTMateIncome = (R!FTMateIncome.ToString = "0")
                    ' ----------------------------------------- Clear Discount Tax Value -------------------------
                    .Cfg_ModChildAllowanceRateNotStudied = _ChildNotStudy 'บุตรไม่ศึกษา อัตราลดหย่อนบุตร บุตร (ไม่ศึกษา) คนละ
                    .Cfg_ModChildAllowanceRateStudy = _ChildStudy 'บุตรจำนวนบุตรที่ลดหย่อนได้ 
                    '-------------ลดหย่อนบุตร-----------------

                    '--- หักเงินสมทบเข้ากองทุนเลี้ยงชีพ
                    .Cfg_ContributedDeducToTheFund = FTTotalCalContributedAcc 'ลูกจ้าง
                    '---เปอร์เซนต์ หักเงินสมทบเข้ากองทุนเลี้ยงชีพ

                    .Cfg_ModDeductibleDonations = CDbl(Val(R!FCDeductDonate.ToString)) 'Val(R!FCDeductDonate.ToString) ' % ลดหย่อนเงินบริจาค
                    .Cfg_ModDeductDonateStudy = CDbl(Val(R!FCDeductDonateStudy.ToString))
                    .Cfg_ModFatherReduction = CDbl(Val(R!FCModFather.ToString)) ' Val(R!FCModFather.ToString) 'ลดหย่อนบิดา
                    .Cfg_ModInsurancePremiums = CDbl(Val(R!FCPremium.ToString)) ' Val(R!FCPremium.ToString) 'ค่าเบี้ยประกันชีวิตส่วนบุคคล
                    .Cfg_ModLendingforHousing = CDbl(Val(R!FCInterest.ToString)) 'Val(R!FCInterest.ToString) 'ดอกเบี้ยเงินกู้เพื่อที่อยู่อาศัย

                    .Cfg_ModLTFChk = CDbl(Val(R!FCUnitLTF.ToString)) 'Val(R!FCUnitLTF.ToString) 'หักค่าซื้อหน่วยลงทุนในกองทุนรวมหุ้นระยะยาว (LTF) ไม่เกิน
                    .Cfg_ModMateFatherReduction = CDbl(Val(R!FCModMateFather.ToString)) ' Val(R!FCModMateFather.ToString) 'ลดหย่อนบิดา คู่สมรส
                    .Cfg_ModMateMotherReduction = CDbl(Val(R!FCModMateMother.ToString)) ' Val(R!FCModMateMother.ToString) 'ลดหย่อนมารดา คู่สมรส
                    .Cfg_ModMotherReduction = CDbl(Val(R!FCModMother.ToString)) 'Val(R!FCModMother.ToString) 'ลดหย่อนมารดา

                    .Cfg_ModPersonalExpenChk = 0 ' ค่าใช้จ่ายส่วนบุคคล ลดหย่อนไม่เกิน

                    .Cfg_ModRateReductionsByMarital = (IIf(R!FTMaritalCode.ToString = "1", 1, 0)) 'อัตราลดหย่อน ตาม สถานภาพ คู่สมรส 
                    .Cfg_ModRateReductionsBySingle = (IIf(R!FTMaritalCode.ToString <> "1", 1, 0)) 'อัตราลดหย่อน ตาม สถานภาพ โสด 

                    .Cfg_ModRMFChk = CDbl(Val(R!FCUnitRMF.ToString)) ' Val(R!FCUnitRMF.ToString) ' หักค่าซื้อหน่วยลงทุนในกองทุนรวมเพื่อการเลี้ยงชีพ (RMF) ไม่เกิน 
                    .FCDisabledDependents = CDbl(Val(R!FCDisabledDependents.ToString)) ' Val(R!FCDisabledDependents.ToString) 'ค่าอุปการะเลี้ยงดูคนพิการหรือทุพพลภาพ
                    .FCHealthInsurFatherMotherMate = CDbl(Val(R!FCHealthInsurFatherMotherMate.ToString)) '  Val(R!FCHealthInsurFatherMotherMate.ToString) 'เบี้ยประกันสุขภาพบิดามารดาของผู้มีเงินได้และคู่สมสร

                    .FCExceptAgeOver = CDbl(Val(R!FCExceptAgeOver.ToString)) '  Val(R!FCExceptAgeOver.ToString) ' 'รายการเงินได้ที่ได้รับยกเว้น ของผู้มีเงินได้ตั้งแต่ 65 ปีขึ้นไป 
                    .FCExceptAgeOverMate = CDbl(Val(R!FCExceptAgeOverMate.ToString)) '  Val(R!FCExceptAgeOverMate.ToString) 'รายการเงินได้ที่ได้รับยกเว้น ของคู่สมรสอายุตั้งแต่ 65 ปีขึ้นไป
                    '----------------------------------------------------
                End With
                '---------------------------------------- ลดหย่อน------------------------------------

                _Totalcaltax = (FCBaht.Value)
                Dim _FTOtherAdd_CalTAX As Double = 0

                For Each R2 As DataRow In CType(ogdIncome.DataSource, DataTable).Rows

                    If R2!FTStaTax.ToString = "1" Then
                        _FTOtherAdd_CalTAX = _FTOtherAdd_CalTAX + Val(R2!FTFinAmt.ToString)
                    End If

                Next

                For Each R2 As DataRow In CType(ogdDeduct.DataSource, DataTable).Rows

                    If R2!FTStaTax.ToString = "1" Then
                        _FTOtherAdd_CalTAX = _FTOtherAdd_CalTAX - Val(R2!FTFinAmt.ToString)
                    End If

                Next

                If R!FTCalTaxSta.ToString <> "1" Then

                    With _EmpDisTax

                        .FTSosial = _FCAccumulateSocial + FNSocial.Value + (FNSocial.Value * (_TotalInstalment - _Instalment))
                        .BaseSlary = (_Totalcaltax * (_TotalInstalment - _Instalment)) + _Totalcaltax
                        .OtherSlary = _FTOtherAdd_CalTAX + FCOt1_Baht.Value + FCOt15_Baht.Value + FCOt2_Baht.Value + FCOt3_Baht.Value + FCOt4_Baht.Value + FNPayLeaveOtherBaht.Value + FNPayLeaveVacationBaht.Value
                        .Cfg_ContributedDeducToTheFund = .Cfg_ContributedDeducToTheFund + FNContributedAmt.Value + (FNContributedAmt.Value * (_TotalInstalment - _Instalment))

                    End With

                    _Totalcaltax = _Totalcaltax + (Me.FCOt1_Baht.Value + FCOt15_Baht.Value + FCOt2_Baht.Value + FCOt3_Baht.Value + FCOt4_Baht.Value + FHolidayBaht.Value) + _FTOtherAdd_CalTAX + FNPayLeaveOtherBaht.Value + FNPayLeaveVacationBaht.Value

                    Dim _TaxOther As Double = _EmpDisTax.OtherSlary
                    Dim _TaxOtherAmt As Double = 0
                    Dim _Total As Double = HI.HRCAL.Calculate.GETnRecalDiscTax(_EmpDisTax, _EmpTaxYear)

                    _EmpTaxYear.FTSocial = _FCAccumulateSocial + FNSocial.Value   'ประกันสังคม

                    _EmpTaxYear.FTTotalCalTax = _Total
                    _TaxOtherAmt = 0
                    Dim _TotalTax As Double = HI.HRCAL.Calculate.GETnTax(_Total, _TaxOther, _TaxOtherAmt)

                    _EmpTaxYear.FTTotalTax = _TotalTax + _TaxOtherAmt 'ภาษีที่ต้องจ่าย

                    _TotalTax = CDbl(Format(_TotalTax - _EmpDisTax.BeforeTax, "0.00"))

                    If _TotalTax + _TaxOtherAmt > 0 Then
                        If _TotalTax > 0 Then
                            _TaxAmt = CDbl(Format((_TotalTax / ((_TotalInstalment - _Instalment) + 1)), "0.00"))
                        End If
                        _TaxAmt = _TaxAmt + _TaxOtherAmt
                    Else
                        _TaxAmt = 0
                    End If

                    _EmpTaxYear.FTTotalTaxPay = _FCAccumulateTax + _TaxAmt

                    If _FTEmpIdNo <> "" Then

                        '-----------------------------Tax -----------------------------------------------------
                        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTaxYear "
                        _Qry &= vbCrLf & "  WHERE FTYear='" & _PayYear & "' "
                        _Qry &= vbCrLf & "  AND  FTEmpIdNo='" & HI.UL.ULF.rpQuoted(_FTEmpIdNo) & "' AND FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "
                        _Qry &= vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTaxYear (FNHSysCmpId,FTYear, FTEmpIdNo, FNAmt, FNExpenses, FNNetAmt, "
                        _Qry &= vbCrLf & " FNModEmp, FNModMate, FNChildNotLern, FNChildLern, FNChildNotLernAmt, FNChildLernAmt, FNInsurance, FNProvidentfund, FNInterest, FNSocial, FNDonation, "
                        _Qry &= vbCrLf & " FNProvidentfundOver, FNGPF, FNSavingsFund, FNCommutation, FNUnitRMF, FNModFather, FNModMother, FNModFatherMate, FNModMotherMate, FNUnitLTF, "
                        _Qry &= vbCrLf & " FNDonationLern, FNParentsHealthInsurance, FNSupportSport, FNAcquisitionOfProperty, FNPension, FNTravel, FNTotalCalTax, FNTotalTax,FNTotalTaxPay )"
                        _Qry &= vbCrLf & " SELECT " & HI.ST.SysInfo.CmpID & ",'" & _PayYear & "','" & HI.UL.ULF.rpQuoted(_FTEmpIdNo) & "' "

                        With _EmpTaxYear
                            _Qry &= vbCrLf & "," & .FTAmt & " "
                            _Qry &= vbCrLf & "," & .FTExpenses & ""
                            _Qry &= vbCrLf & "," & .FTNetAmt & ""
                            _Qry &= vbCrLf & "," & .FTModEmp & ""
                            _Qry &= vbCrLf & "," & .FTModMate & ""
                            _Qry &= vbCrLf & "," & .FNChildNotLern & ""
                            _Qry &= vbCrLf & "," & .FNChildLern & " "
                            _Qry &= vbCrLf & "," & .FTChildNotLern & ""
                            _Qry &= vbCrLf & "," & .FTChildLern & ""
                            _Qry &= vbCrLf & "," & .FTInsurance & ""
                            _Qry &= vbCrLf & "," & .FTProvidentfund & ""
                            _Qry &= vbCrLf & "," & .FTInterest & ""
                            _Qry &= vbCrLf & "," & .FTSocial & ""
                            _Qry &= vbCrLf & "," & .FTDonation & ""
                            _Qry &= vbCrLf & "," & .FTProvidentfundOver & ""
                            _Qry &= vbCrLf & "," & .FTGPF & ""
                            _Qry &= vbCrLf & "," & .FTSavingsFund & ""
                            _Qry &= vbCrLf & "," & .FTCommutation & ""
                            _Qry &= vbCrLf & "," & .FTUnitRMF & ""
                            _Qry &= vbCrLf & "," & .FTModFather & ""
                            _Qry &= vbCrLf & "," & .FTModMother & ""
                            _Qry &= vbCrLf & "," & .FTModFatherMate & ""
                            _Qry &= vbCrLf & "," & .FTModMotherMate & ""
                            _Qry &= vbCrLf & "," & .FTUnitLTF & ""
                            _Qry &= vbCrLf & "," & .FTDonationLern & ""
                            _Qry &= vbCrLf & "," & .FTParentsHealthInsurance & ""
                            _Qry &= vbCrLf & "," & .FTSupportSport & ""
                            _Qry &= vbCrLf & "," & .FTAcquisitionOfProperty & ""
                            _Qry &= vbCrLf & "," & .FTPension & ""
                            _Qry &= vbCrLf & "," & .FTTravel & ""
                            _Qry &= vbCrLf & "," & .FTTotalCalTax & ""
                            _Qry &= vbCrLf & "," & .FTTotalTax & ""
                            _Qry &= vbCrLf & "," & .FTTotalTaxPay & ""
                        End With

                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

                        '----------------------------- Tax -----------------------------------------------------
                    End If

                Else

                    _Totalcaltax = 0
                    _TaxAmt = 0

                End If
                Exit For
            Next

            FNTax.Value = _TaxAmt

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim _AmtAdd As Double = 0
            Dim _AmtDeduct As Double = 0

            Dim FNTotalAdd, FNTotalAddOther, FNTotalExpense, FNTotalExpenseOther As Double

            FNTotalAdd = 0 : FNTotalAddOther = 0 : FNTotalExpense = 0 : FNTotalExpenseOther = 0

            For Each R As DataRow In CType(ogdIncome.DataSource, DataTable).Rows
                _AmtAdd = _AmtAdd + Val(R!FTFinAmt.ToString)

                Select Case True
                    Case (Val(R!FTFinAmt.ToString) >= (Val(R!FCFinAmt.ToString)))

                        FNTotalAdd = FNTotalAdd + Val(R!FCFinAmt.ToString)
                        FNTotalAddOther = FNTotalAddOther + (Val(R!FTFinAmt.ToString) - (Val(R!FCFinAmt.ToString)))

                    Case (Val(R!FTFinAmt.ToString) < (Val(R!FCFinAmt.ToString)))

                        FNTotalAdd = FNTotalAdd + Val(R!FTFinAmt.ToString)
                End Select

                _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin "
                _Qry &= vbCrLf & " SET FCTotalFinAmt=" & Val(R!FTFinAmt.ToString) & " "
                _Qry &= vbCrLf & " ,FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ,FTUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & " WHERE FTPayYear='" & Me.FTPayYear.Text & "' "
                _Qry &= vbCrLf & " AND FTPayTerm='" & Me.FTPayTerm.Text & "' "
                _Qry &= vbCrLf & " AND FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & " AND FTFinCode='" & HI.UL.ULF.rpQuoted(R!FTFinCode.ToString) & "' "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Qry = "INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin(  FTInsUser, FTInsDate, FTInsTime, FTPayYear, FTPayTerm, FNHSysEmpID, FTFinCode,  FCFinAmt,FCFinAmtOther, FCTotalFinAmt )"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & " ,'" & Me.FTPayYear.Text & "' "
                    _Qry &= vbCrLf & " ,'" & Me.FTPayTerm.Text & "' "
                    _Qry &= vbCrLf & " ," & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTFinCode.ToString) & "'"
                    _Qry &= vbCrLf & " ,0,0"
                    _Qry &= vbCrLf & " ," & Val(R!FTFinAmt.ToString) & ""

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                End If

            Next

            For Each R As DataRow In CType(ogdDeduct.DataSource, DataTable).Rows
                _AmtDeduct = _AmtDeduct + Val(R!FTFinAmt.ToString)

                If R!FTFinAmt.ToString = "106" Then
                    FNContributedAmt.Value = +Val(R!FTFinAmt.ToString)
                End If
                Select Case True
                    Case (Val(R!FTFinAmt.ToString) >= (Val(R!FCFinAmt.ToString)))

                        FNTotalExpense = FNTotalExpense + Val(R!FCFinAmt.ToString)
                        FNTotalExpenseOther = FNTotalExpenseOther + (Val(R!FTFinAmt.ToString) - (Val(R!FCFinAmt.ToString)))

                    Case (Val(R!FTFinAmt.ToString) < (Val(R!FCFinAmt.ToString)))

                        FNTotalExpense = FNTotalExpense + Val(R!FTFinAmt.ToString)
                End Select

                _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin "
                _Qry &= vbCrLf & " SET FCTotalFinAmt=" & Val(R!FTFinAmt.ToString) & " "
                _Qry &= vbCrLf & " ,FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ,FTUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & " WHERE FTPayYear='" & Me.FTPayYear.Text & "' "
                _Qry &= vbCrLf & " AND FTPayTerm='" & Me.FTPayTerm.Text & "' "
                _Qry &= vbCrLf & " AND FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & " AND FTFinCode='" & HI.UL.ULF.rpQuoted(R!FTFinCode.ToString) & "' "


                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Qry = "INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin(  FTInsUser, FTInsDate, FTInsTime, FTPayYear, FTPayTerm, FNHSysEmpID, FTFinCode,  FCFinAmt,FCFinAmtOther, FCTotalFinAmt )"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & " ,'" & Me.FTPayYear.Text & "' "
                    _Qry &= vbCrLf & " ,'" & Me.FTPayTerm.Text & "' "
                    _Qry &= vbCrLf & " ," & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTFinCode.ToString) & "'"
                    _Qry &= vbCrLf & " ,0,0"
                    _Qry &= vbCrLf & " ," & Val(R!FTFinAmt.ToString) & ""

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If
            Next

            _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll "
            _Qry &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & " ,FTUpdDate=" & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & " , FNOt1=" & FNOt1.Value & " "
            _Qry &= vbCrLf & " , FNOt15=" & FNOt15.Value & " "
            _Qry &= vbCrLf & " , FNOt2=" & FNOt2.Value & " "
            _Qry &= vbCrLf & " , FNOt3=" & FNOt3.Value & " "
            _Qry &= vbCrLf & " , FNOt4=" & FNOt4.Value & " "
            _Qry &= vbCrLf & " , FNOt1Min=Convert(numeric(18,0),((" & FNOt1.Value & " -( " & FNOt1.Value & " % 1)) *60) + ((" & FNOt1.Value & " % 1)*100))"
            _Qry &= vbCrLf & " , FNOt15Min=Convert(numeric(18,0),((" & FNOt15.Value & " -( " & FNOt15.Value & " % 1)) *60) + ((" & FNOt15.Value & " % 1)*100))"
            _Qry &= vbCrLf & " , FNOt2Min=Convert(numeric(18,0),((" & FNOt2.Value & " -( " & FNOt2.Value & " % 1)) *60) + ((" & FNOt2.Value & " % 1)*100))"
            _Qry &= vbCrLf & " , FNOt3Min=Convert(numeric(18,0),((" & FNOt3.Value & " -( " & FNOt3.Value & " % 1)) *60) + ((" & FNOt3.Value & " % 1)*100))"
            _Qry &= vbCrLf & " , FNOt4Min=Convert(numeric(18,0),((" & FNOt4.Value & " -( " & FNOt4.Value & " % 1)) *60) + ((" & FNOt4.Value & " % 1)*100))"
            _Qry &= vbCrLf & " , FCBaht=" & FCBaht.Value & " "
            _Qry &= vbCrLf & " , FCOt1_Baht=" & FCOt1_Baht.Value & ""
            _Qry &= vbCrLf & " , FCOt15_Baht=" & FCOt15_Baht.Value & " "
            _Qry &= vbCrLf & " , FCOt2_Baht=" & FCOt2_Baht.Value & " "
            _Qry &= vbCrLf & " , FCOt3_Baht=" & FCOt3_Baht.Value & " "
            _Qry &= vbCrLf & " , FCOt4_Baht=" & FCOt4_Baht.Value & " "
            _Qry &= vbCrLf & " , FCNetBaht=" & (FCBaht.Value + FCOt1_Baht.Value + FCOt15_Baht.Value + FCOt2_Baht.Value + FCOt3_Baht.Value + FCOt4_Baht.Value) & ""
            _Qry &= vbCrLf & " , FNSocial=" & FNSocial.Value & ""
            _Qry &= vbCrLf & " , FNTax=" & FNTax.Value & " "
            ''_Qry &= vbCrLf & " , FNNetpay=" & ((FCBaht.Value + FCOt1_Baht.Value + FCOt15_Baht.Value + FCOt2_Baht.Value + FCOt3_Baht.Value + FCOt4_Baht.Value + FHolidayBaht.Value) + _AmtAdd + (FNDiligentBaht.Value + FNPayLeaveVacationBaht.Value + FNPayLeaveOtherBaht.Value)) - (_AmtDeduct + FNSocial.Value + FNTax.Value + FNContributedAmt.Value) & ""
            _Qry &= vbCrLf & " , FNNetpay=" & ((FCBaht.Value + FCOt1_Baht.Value + FCOt15_Baht.Value + FCOt2_Baht.Value + FCOt3_Baht.Value + FCOt4_Baht.Value + FHolidayBaht.Value) + _AmtAdd + (FNDiligentBaht.Value + FNPayLeaveVacationBaht.Value + FNPayLeaveOtherBaht.Value)) - (_AmtDeduct + FNSocial.Value + FNTax.Value + 0) & ""
            _Qry &= vbCrLf & " , FNWorkingDay=" & FNWorkingDay.Value & ""
            _Qry &= vbCrLf & " , FNHoliday=" & FNHoliday.Value & ""
            _Qry &= vbCrLf & " , FHolidayBaht=" & FHolidayBaht.Value & ""
            _Qry &= vbCrLf & " , FNContributedAmt=" & FNContributedAmt.Value & ""
            _Qry &= vbCrLf & " , FNTotalAdd=" & FNTotalAdd & ""
            _Qry &= vbCrLf & " , FNTotalAddOther=" & FNTotalAddOther & ""
            _Qry &= vbCrLf & " , FNTotalExpense=" & FNTotalExpense & ""
            _Qry &= vbCrLf & " , FNTotalExpenseOther=" & FNTotalExpenseOther & ""
            _Qry &= vbCrLf & " , FNTotalIncome=" & ((FCBaht.Value + FCOt1_Baht.Value + FCOt15_Baht.Value + FCOt2_Baht.Value + FCOt3_Baht.Value + FCOt4_Baht.Value + FHolidayBaht.Value) + _AmtAdd + (FNDiligentBaht.Value + FNPayLeaveVacationBaht.Value + FNPayLeaveOtherBaht.Value)) - (_AmtDeduct) & ""
            _Qry &= vbCrLf & " ,FNTotalRecalSSO=" & FNTotalRecalSSO.Value & " "
            _Qry &= vbCrLf & " ,FNTotalRecalTAX=" & _Totalcaltax & " "
            _Qry &= vbCrLf & " WHERE FTPayYear='" & Me.FTPayYear.Text & "' "
            _Qry &= vbCrLf & " AND FTPayTerm='" & Me.FTPayTerm.Text & "' "
            _Qry &= vbCrLf & " AND FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & " "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
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

    Private Sub ocmsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click
        Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
        If Me.SaveData(_Spls) Then

            Call SetShowFinance(FNHSysEmpID.Properties.Tag.ToString)
            Call LoadPayroll()

            _Spls.Close()
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        Else
            _Spls.Close()
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        End If
    End Sub

    Private Sub FNWorkingDay_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNWorkingDay.EditValueChanged
        If Not (_InitLoad) Then
            FCBaht.Value = Format(Me.SalaryPerDay * FNWorkingDay.Value, "0.00")
        End If
    End Sub

    Private Sub FNOt1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNOt1.EditValueChanged
        If Not (_InitLoad) Then
            FCOt1_Baht.Value = Format(((Me.SalaryPerDay / 8) * _RateOT1) * FNOt1.Value, "0.00")
        End If
    End Sub

    Private Sub FNOt15_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNOt15.EditValueChanged
        If Not (_InitLoad) Then
            FCOt15_Baht.Value = Format(((Me.SalaryPerDay / 8) * _RateOT15) * FNOt15.Value, "0.00")
        End If
    End Sub

    Private Sub FNOt2_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNOt2.EditValueChanged
        If Not (_InitLoad) Then
            FCOt2_Baht.Value = Format(((Me.SalaryPerDay / 8) * _RateOT2) * FNOt2.Value, "0.00")
        End If
    End Sub

    Private Sub FNOt3_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNOt3.EditValueChanged
        If Not (_InitLoad) Then
            FCOt3_Baht.Value = Format(((Me.SalaryPerDay / 8) * _RateOT3) * FNOt3.Value, "0.00")
        End If
    End Sub

    Private Sub FNOt4_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNOt4.EditValueChanged
        If Not (_InitLoad) Then
            FCOt4_Baht.Value = Format(((Me.SalaryPerDay / 8) * _RateOT4) * FNOt4.Value, "0.00")
        End If
    End Sub

    Private Sub FNHoliday_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNHoliday.EditValueChanged
        If Not (_InitLoad) Then
            FHolidayBaht.Value = Format(Me.SalaryPerDay * FNHoliday.Value, "0.00")
        End If
    End Sub

    Private Sub wAdjustWeekend_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = "1"
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub

#End Region

End Class