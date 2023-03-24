Imports System

Public Class wAdjustPayrollBefore_VN

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

#Region "Procedure"

    Private Sub LoadEmpInfo(FNHSysEmpID As String)

        Dim _dt As System.Data.DataTable
        Dim _Dt2 As System.Data.DataTable

        Dim _Qry As String = ""
        _Qry = " SELECT    TOP 1     M.FTEmpCode, M.FTEmpCodeRefer, M.FTEmpNameTH, M.FTEmpSurnameTH, M.FTEmpNicknameTH, M.FTEmpNameEN, M.FNHSysEmpTypeId, M.FNHSysDeptId, "
        _Qry &= vbCrLf & "   D.FTDeptCode, Di.FTDivisonCode, M.FNHSysDivisonId, M.FNHSysSectId, S.FTSectCode, ET.FTEmpTypeCode, M.FNHSysUnitSectId, US.FTUnitSectCode,"
        _Qry &= vbCrLf & "  M.FNHSysEmpID, M.FTEmpPicName, M.FNHSysPositId, P.FTPositCode"
        _Qry &= vbCrLf & "  FROM            THRMEmployee AS M WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId = P.FNHSysPositId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
        _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID  =" & Val(FNHSysEmpID) & ""
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        FTEmpPicName.Image = Nothing
        If _dt.Rows.Count > 0 Then
            For Each R As DataRow In _dt.Rows
                FTEmpPicName.Image = HI.UL.ULImage.LoadImage(HI.ST.SysInfo.SysPath & "EmpPicture\" & R!FTEmpPicName.ToString)
                FNHSysEmpTypeId.Text = R!FTEmpTypeCode.ToString
                FNHSysDeptId.Text = R!FTDeptCode.ToString
                FNHSysDivisonId.Text = R!FTDivisonCode.ToString
                FNHSysSectId.Text = R!FTSectCode.ToString
                FNHSysUnitSectId.Text = R!FTUnitSectCode.ToString
                FNHSysPositId.Text = R!FTPositCode.ToString

                _Qry = " SELECT        D.FTPayTerm, D.FTPayYear, D.FNHSysEmpTypeId, D.FNMonth, D.FTTermOfMonth"
                _Qry &= vbCrLf & " , D.FDPayDate, D.FDCalDateBegin, D.FDCalDateEnd, D.FDDateClose, "
                _Qry &= vbCrLf & "  D.FTStateTermEndOfYear"

                If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                    _Qry &= vbCrLf & " , V_ListMonth.FTNameTH  AS FTMonth "
                Else
                    _Qry &= vbCrLf & " , V_ListMonth.FTNameEN AS FTMonth "
                End If

                _Qry &= vbCrLf & "  FROM   THRMCfgPayHD AS H WITH (NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "    THRMCfgPayDT AS D WITH (NOLOCK) ON H.FTPayTerm = D.FTPayTerm "
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
        End If

        Call SetShowFinance(FNHSysEmpID)

    End Sub

    Private Sub SetShowFinance(ByVal EmpCode As String)

        Dim oDbdt As New System.Data.DataTable
        Dim _Qry As String
        Try
            _Qry = "SELECT FTFinCode,FTFinDesc,FTFinAmt "
            _Qry &= vbCrLf & " FROM ("
            _Qry &= vbCrLf & " SELECT THRMFinanceSet.FTFinCode,FNFinSeqNo"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & ",FTFinDescTH AS FTFinDesc"
            Else
                _Qry &= vbCrLf & ",FTFinDescEN AS FTFinDesc"
            End If

            _Qry &= vbCrLf & ",CASE WHEN ISNULL(THRMEmployeeFin.FTFinCode,'')='' THEN   Convert(numeric(18,2),0.00) ELSE   FTFinAmt  END AS FTFinAmt"
            _Qry &= vbCrLf & " FROM "
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT FTFinCode,FTType "
            _Qry &= vbCrLf & " FROM THRMFinanceSet WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE   ISNULL(FTStaActive,'')='1' "
            _Qry &= vbCrLf & ") THRMFinanceSet"
            _Qry &= vbCrLf & " INNER JOIN THRMFinance WITH(NOLOCK) ON THRMFinanceSet.FTFinCode=THRMFinance.FTFinCode AND FTFinType='1'"
            _Qry &= vbCrLf & " Left JOIN"
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT FTFinCode,FCFinAmt As FTFinAmt "
            _Qry &= vbCrLf & " FROM THRTManage  WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTPayYear='" & FTPayYear.Text & "' AND FTPayTerm='" & FTPayTerm.Text & "' AND FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & ") THRMEmployeeFin"
            _Qry &= vbCrLf & " ON THRMFinanceSet.FTFinCode=THRMEmployeeFin.FTFinCode"
            _Qry &= vbCrLf & " ) T  "
            _Qry &= vbCrLf & "  WHERE ISNULL(FTFinDesc,'') <> '' "
            _Qry &= vbCrLf & " ORDER BY FNFinSeqNo"
            ogdIncome.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            _Qry = "SELECT FTFinCode,FTFinDesc,FTFinAmt "
            _Qry &= vbCrLf & " FROM ("
            _Qry &= vbCrLf & " SELECT THRMFinanceSet.FTFinCode,FNFinSeqNo"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & ",FTFinDescTH AS FTFinDesc"
            Else
                _Qry &= vbCrLf & ",FTFinDescEN AS FTFinDesc"
            End If

            _Qry &= vbCrLf & ",CASE WHEN ISNULL(THRMEmployeeFin.FTFinCode,'')='' THEN   Convert(numeric(18,2),0.00) ELSE   FTFinAmt END AS FTFinAmt"
            _Qry &= vbCrLf & " FROM "
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT FTFinCode,FTType "
            _Qry &= vbCrLf & " FROM THRMFinanceSet WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE   ISNULL(FTStaActive,'')='1' "
            _Qry &= vbCrLf & ") THRMFinanceSet"
            _Qry &= vbCrLf & " INNER JOIN THRMFinance WITH(NOLOCK) ON THRMFinanceSet.FTFinCode=THRMFinance.FTFinCode AND FTFinType='2'"
            _Qry &= vbCrLf & " Left JOIN"
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT FTFinCode,FCFinAmt AS FTFinAmt"
            _Qry &= vbCrLf & " FROM THRTManage WITH(NOLOCK)"
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

    Private Function DeleteData(ByVal oSpls As HI.TL.SplashScreen) As Boolean
        Try

            Dim _Qry As String = ""
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Qry = "DELETE FROM THRTManage WHERE FTPayYear='" & FTPayYear.Text & "' AND FTPayTerm='" & FTPayTerm.Text & "' AND FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & ""
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Dim _ReturnVacation As Integer

            If Integer.Parse(FTPayYear.Text) >= 2014 Then
                _Qry = " SELECT   TOP 1 FCCfgRetValue"
                _Qry &= vbCrLf & "  FROM THRMConfigReturnVacationSet WITH(NOLOCK) "
                _Qry &= vbCrLf & "  WHERE      (FNCalType =" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & ")"
                _Qry &= vbCrLf & "  AND (FTCfgRetTerm = '" & HI.UL.ULF.rpQuoted(FTPayTerm.Text) & "')"
                _ReturnVacation = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))
            Else
                _ReturnVacation = 0
            End If

            Dim _CalType As String = ""

            _Qry = "SELECt TOP 1 FNCalType FROM THRMEmpType WITH(NOLOCK) WHERE FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "
            _CalType = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")

            oSpls.UpdateInformation("Calculate Weekend...")
            Calculate.CalculateWeekEnd_VN(HI.ST.UserInfo.UserName, Me.FNHSysEmpID.Properties.Tag.ToString, Me.FNHSysEmpTypeId.Properties.Tag.ToString, HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text), HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text), Me.FTPayYear.Text, Me.FTPayTerm.Text, Me.FDPayDate.Text, "0", _CalType, False, _ReturnVacation)

            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return False
        End Try
    End Function

    Private Function SaveData(ByVal oSpls As HI.TL.SplashScreen) As Boolean
        Try
            CType(ogdIncome.DataSource, System.Data.DataTable).AcceptChanges()
            CType(ogdDeduct.DataSource, System.Data.DataTable).AcceptChanges()

            Calculate.LoadSocialRate()
            Calculate.LoadTaxRate()
            Calculate.LoadDiscountTax()

            '...Factory Vietnam
            Calculate.LoadInsuranceVNRate()

            Dim _Qry As String = ""
            Dim _FNExchangeRate As Double
            Dim _FNExchangeRateTHB As Double

            Dim dtemptype As System.Data.DataTable
            Dim _FTStaDeductAbsent As Integer = 0
            Dim _FTStaCalPayRoll As Integer = 0
            Dim _FNStateSalaryType As Integer = 0
            _Qry = "SELECT TOP 1 FNCalType,FTStaDeductAbsent,FTStaCalPayRoll,FNStateSalaryType FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType  WITH(NOLOCK) WHERE FNHSysEmpTypeId=" & Integer.Parse(Val(FNHSysEmpTypeId.Properties.Tag.ToString)) & "  "
            dtemptype = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

            Dim _TmpCalType As Integer = 0 'Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")))

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

            _Qry = " SELECT        FNExchangeRate"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS P WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTPayYear=N'" & Me.FTPayYear.Text & "' AND  FTPayTerm=N'" & Me.FTPayTerm.Text & "' AND  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
            _FNExchangeRate = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "-1")))

            _Qry = " SELECT      FNExchangeRateTHB"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS P WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTPayYear=N'" & Me.FTPayYear.Text & "' AND  FTPayTerm=N'" & Me.FTPayTerm.Text & "' AND  FNHSysEmpID=" & Val(FNHSysEmpID.Properties.Tag.ToString) & " "
            _FNExchangeRateTHB = Double.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "-1")))

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTManage WHERE FTPayYear='" & FTPayYear.Text & "' AND FTPayTerm='" & FTPayTerm.Text & "' AND FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & ""
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            For Each R As DataRow In CType(ogdIncome.DataSource, System.Data.DataTable).Rows
                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTManage( FTInsUser, FTInsDate, FTInsTime,  FTPayYear, FTPayTerm, FNHSysEmpID, FTFinCode,  FCFinAmt, FNDay) "
                _Qry &= vbCrLf & "SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ",'" & FTPayYear.Text & "' "
                _Qry &= vbCrLf & ",'" & FTPayTerm.Text & "' "
                _Qry &= vbCrLf & "," & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & ""
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(R!FTFinCode.ToString) & "'," & Val(R!FTFinAmt.ToString) & ",0 "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            Next

            For Each R As DataRow In CType(ogdDeduct.DataSource, System.Data.DataTable).Rows

                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTManage( FTInsUser, FTInsDate, FTInsTime,  FTPayYear, FTPayTerm, FNHSysEmpID, FTFinCode,  FCFinAmt, FNDay) "
                _Qry &= vbCrLf & "SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ",'" & FTPayYear.Text & "' "
                _Qry &= vbCrLf & ",'" & FTPayTerm.Text & "' "
                _Qry &= vbCrLf & "," & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & ""
                _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(R!FTFinCode.ToString) & "'," & Val(R!FTFinAmt.ToString) & ",0 "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Dim _ReturnVacation As Integer

            If Integer.Parse(FTPayYear.Text) >= 2014 Then
                _Qry = " SELECT   TOP 1 FCCfgRetValue"
                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigReturnVacationSet WITH(NOLOCK) "
                _Qry &= vbCrLf & "  WHERE      (FNCalType =" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & ")"
                _Qry &= vbCrLf & "  AND (FTCfgRetTerm = '" & HI.UL.ULF.rpQuoted(FTPayTerm.Text) & "')"
                _ReturnVacation = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))
            Else
                _ReturnVacation = 0
            End If

            Dim _CalType As String = ""

            _Qry = "SELECt TOP 1 FNCalType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WITH(NOLOCK) WHERE FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "
            _CalType = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")

            Dim _WorkDay As Integer = 0
            _Qry = "Select Isnull(FNWorkDay,0) AS FNWorkDay From [HITECH_HR]..THRMCfgPayDT"
            _Qry &= vbCrLf & " Where FTPayTerm ='" & HI.UL.ULF.rpQuoted(Me.FTPayTerm.Text) & "'"
            _Qry &= vbCrLf & "and FTPayYear ='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "'"
            _Qry &= vbCrLf & "and FNHSysEmpTypeId =" & Integer.Parse(Val(FNHSysEmpTypeId.Properties.Tag.ToString))
            _WorkDay = HI.HRVN.Calculate.GetDayPerMonth(Me.FTPayTerm.Text, Me.FTPayYear.Text, Integer.Parse(Val(FNHSysEmpTypeId.Properties.Tag.ToString)))

            If _FNExchangeRate > 0 Then
                oSpls.UpdateInformation("Calculate Weekend...")
                Calculate.CalculateWeekEnd_VN(HI.ST.UserInfo.UserName, Me.FNHSysEmpID.Properties.Tag.ToString,
                                Me.FNHSysEmpTypeId.Properties.Tag.ToString, HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text), HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text),
                                Me.FTPayYear.Text, Me.FTPayTerm.Text, Me.FDPayDate.Text, "", _CalType, _WorkDay,
                                 False, _FTStaDeductAbsent, _FTStaCalPayRoll, _FNStateSalaryType, _FNExchangeRate, _FNExchangeRateTHB)
            End If

            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return False
        End Try

    End Function

#End Region

#Region "General"

    Private Sub FNHSysEmpID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNHSysEmpID.EditValueChanged
        If Me.InvokeRequired Then
            Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FNHSysEmpID_EditValueChanged), New Object() {sender, e})
        Else
            If FNHSysEmpID.Text <> "" Then
                Dim _Qry As String = "SELECT TOP 1 FNHSysEmpID  FROM THRMEmployee WITH(NOLOCK) WHERE FTEmpCode ='" & HI.UL.ULF.rpQuoted(FNHSysEmpID.Text) & "' "
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

    Private Sub ocmdelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If Me.FNHSysEmpID.Text <> "" And Me.FNHSysEmpID.Properties.Tag.ToString <> "" Then
            Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
            If Me.DeleteData(_Spls) Then
                _Spls.Close()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                HI.TL.HandlerControl.ClearControl(Me)
            Else
                _Spls.Close()
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub ocmsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click
        Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
        If Me.SaveData(_Spls) Then
            _Spls.Close()
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        Else
            _Spls.Close()
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        End If
    End Sub

    Private Sub wAdjustBeforeCalculate_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = "1"
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub

#End Region

End Class