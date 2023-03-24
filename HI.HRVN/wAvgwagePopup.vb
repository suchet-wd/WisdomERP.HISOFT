Imports System.Data
Imports System.Windows.Forms

Public Class wAvgwagePopup
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If Me.FTDate.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTDate_lbl.Text)
                Me.FTDate.Focus()
                Exit Sub
            End If

            'If Me.FNHSysUnitSectId.Text = "" Then
            '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysUnitSectId_lbl.Text)
            '    Me.FNHSysUnitSectId.Focus()
            '    Exit Sub
            'End If

            If Me.FNIncentive.Value <= 0 Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNIncentive_lbl.Text)
                Me.FNIncentive.Focus()
                Exit Sub
            End If


            Call AvgWageIncentive()
        Catch ex As Exception

        End Try
    End Sub


    Private Function GetSumWorkingTime(ByVal _Date As String, ByVal _UnitSect As Integer) As Double
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT    sum(  T.FNTimeMin + T.FNOT1Min + T.FNOT1_5Min + T.FNOT2Min + T.FNOT3Min + T.FNOT4Min) AS FNTime"
            _Cmd &= vbCrLf & "        FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) ON T.FNHSysEmpID = E.FNHSysEmpID"
            _Cmd &= vbCrLf & " where FTDateTrans = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "'"
            _Cmd &= vbCrLf & " AND E.FNHSysUnitSectId =" & Integer.Parse(_UnitSect)
            Return Convert.ToDouble(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "0"))
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function AvgWageIncentive() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _IncentivePerMin As Double = 0
            _IncentivePerMin = Me.FNIncentive.Value / GetSumWorkingTime(Me.FTDate.Text, Me.FNHSysUnitSectId.Properties.Tag)

            _Cmd = "SELECT  T.FNHSysEmpID,   T.FTDateTrans,   T.FNTimeMin + T.FNOT1Min + T.FNOT1_5Min + T.FNOT2Min + T.FNOT3Min + T.FNOT4Min AS FNTime"
            _Cmd &= vbCrLf & "	,E.FNHSysEmpTypeId ,   E.FNHSysUnitSectId , T.FNTimeMin AS FNTimeMinNormal ,T.FNOT1Min,T.FNOT1_5Min , T.FNOT2Min ,T.FNOT3Min , T.FNOT4Min  "
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) ON T.FNHSysEmpID = E.FNHSysEmpID"
            _Cmd &= vbCrLf & "where FTDateTrans = '" & HI.UL.ULDate.ConvertEnDB(Me.FTDate.Text) & "'"
            _Cmd &= vbCrLf & "and  E.FNHSysUnitSectId =" & Integer.Parse(Me.FNHSysUnitSectId.Properties.Tag)


            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR)

            For Each R As DataRow In _oDt.Rows
                Dim _Incentive As Double = _IncentivePerMin * Double.Parse(R!FNTime.ToString)
                Dim _SalaryPerMin As Double = GetSalaryPerMin(R!FNHSysEmpID.ToString, R!FNHSysEmpTypeId.ToString)
                Dim _FCBaht As Double = _SalaryPerMin * Double.Parse(R!FNTimeMinNormal.ToString)
                Dim _FCBahtOT15 As Double = (_SalaryPerMin * Double.Parse(R!FNOT1Min.ToString)) * 1.5

                _Cmd = "  UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                _Cmd &= vbCrLf & " SET FNProNormal=" & _Incentive & ""
                _Cmd &= vbCrLf & ", FNProOther=" & _FCBaht + _FCBahtOT15 & ""
                _Cmd &= vbCrLf & ", FNNetProAmt=" & _FCBaht + _FCBahtOT15 + _Incentive & " "
                _Cmd &= vbCrLf & ", FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Cmd &= vbCrLf & ", FTUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Cmd &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Cmd &= vbCrLf & " WHERE  FNHSysEmpID=" & Integer.Parse(R!FNHSysEmpID.ToString) & " AND FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(Me.FTDate.Text) & "'  "

                _Cmd = "  INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                _Cmd &= vbCrLf & " (FTInsUser, FTInsDate, FTInsTime,  FNHSysEmpID, FTDateTrans, FNAmtNormal, FNAmtOT, FNNetAmt, FNProNormal, FNProOT,FNProOther, FNNetProAmt) "
                _Cmd &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Cmd &= vbCrLf & " ," & Integer.Parse(R!FNHSysEmpID.ToString) & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FTDate.Text) & "'"
                _Cmd &= vbCrLf & " ,0"
                _Cmd &= vbCrLf & " ,0"
                _Cmd &= vbCrLf & " ,0"
                _Cmd &= vbCrLf & " ," & _Incentive & ",0," & _FCBaht + _FCBahtOT15 & "," & _FCBaht + _FCBahtOT15 + _Incentive & ""


                HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_HR)
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Function GetSalaryPerMin(ByVal _EmpCode As Integer, ByVal _EmpType As Integer) As Double
        Try
            Dim _Cmd As String = ""
            Dim _oDt, tmpDTConfigAllowancePassProba As DataTable
            Dim _NewSlary, _FTSlary, _FTSlaryPerDay, _FTSlaryPerMin, _FTSlaryPerMonth, FNHarmfulBaht, FNSkillBaht, FNSkillRate, FNHarmfulRate As Double
            Dim _Probation As String
            Dim _DayPerMonth As Integer = GetDayPerMonth(Me.FTDate.Text, _EmpType)

            _Cmd = "SELECT    FNHSysEmpID, ISNULL(FDDateProbation, '') AS FDDateProbation, FNSalary"
            _Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WITH (NOLOCK)"
            _Cmd &= vbCrLf & "WHERE FNHSysEmpID=" & Integer.Parse(_EmpCode)
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR)
            For Each R As DataRow In _oDt.Rows
                _FTSlary = Double.Parse(R!FNSalary.ToString)
                _Probation = R!FDDateProbation.ToString
            Next

            _Cmd = "SELECT TOP 1  FNCurrentSlary  AS AMT"
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeMasterChangeSlary WITH(NOLOCK)"
            _Cmd &= vbCrLf & "WHERE (FTEffectiveDate > N'" & HI.UL.ULDate.ConvertEnDB(Me.FTDate.Text) & "') "
            _Cmd &= vbCrLf & "      AND  (FNHSysEmpID = " & Integer.Parse(_EmpCode) & ")"
            _Cmd &= vbCrLf & "ORDER BY FTEffectiveDate ASC"
            _NewSlary = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "0")
            If Double.Parse(_NewSlary) > 0 Then
                _FTSlary = Double.Parse(_NewSlary)
            End If

            tmpDTConfigAllowancePassProba = LoadConfigAllowanceProbation
            For Each oRow As DataRow In tmpDTConfigAllowancePassProba.Rows
                FNSkillRate = Val(oRow!FNSkillRate.ToString()) '...อัตราเปอร์เซ็นต์ การคิด ค่าทักษะ
                FNHarmfulRate = Val(oRow!FNHarmfulRate.ToString()) '...อัตราการเปอร์เซ็นต์ การคิด ค่าเสี่ยงภัย
                Exit For
            Next


            _Cmd = "SELECT TOP 1 Isnull(FTStatePayHarmful,'0') AS FTStatePayHarmful FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WHERE FNHSysEmpTypeId = " & Val(_EmpType) & "  "
            Dim _EmpTypePayHarmful As String = Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "0"))

            _Cmd = "SELECT TOP 1 Isnull(FTStatePaySkill,'0') AS FTStatePaySkill  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WHERE FNHSysEmpTypeId = " & Val(_EmpType) & "  "
            Dim _EmpTypePaySkill As String = Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "0"))


            If _Probation <= HI.UL.ULDate.ConvertEnDB(Me.FTDate.Text) Then
                If _EmpTypePayHarmful = "1" Then
                    FNHarmfulBaht = (_FTSlary * FNHarmfulRate) / 100
                End If
                If _EmpTypePaySkill = "1" Then
                    FNSkillBaht = ((_FTSlary + FNHarmfulBaht) * FNSkillRate) / 100
                End If
                _FTSlary = _FTSlary + FNHarmfulBaht + FNSkillBaht
            End If

            _FTSlaryPerMin = _FTSlary / _DayPerMonth

            Return _FTSlaryPerMin

        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function GetDayPerMonth(ByVal _Date As String, ByVal _EmpType As Integer) As Integer
        Try
            Dim _Cmd As String = ""
            _Cmd = "  SELECT    Isnull(FNWorkDay,0) AS FNWorkDay "
            _Cmd &= vbCrLf & "      FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT WITH(NOLOCK) "
            _Cmd &= vbCrLf & "where FDCalDateBegin <='" & HI.UL.ULDate.ConvertEnDB(_Date) & "' and FDCalDateEnd >= '" & HI.UL.ULDate.ConvertEnDB(_Date) & "'"
            _Cmd &= vbCrLf & "and FNHSysEmpTypeId =" & Integer.Parse(_EmpType)
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "0")
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Shared _ConfigAllowanceProbation As System.Data.DataTable = Nothing
    Private Shared ReadOnly Property LoadConfigAllowanceProbation() As System.Data.DataTable
        Get
            If _ConfigAllowanceProbation Is Nothing Then
                Dim sSQL As String
                sSQL = ""
                sSQL = "SELECT (SELECT L1.FTKeyValue"
                sSQL &= Environment.NewLine & "        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..[THRMConfig] AS L1 (NOLOCK)"
                sSQL &= Environment.NewLine & "        WHERE L1.FTKeyCode = N'Cfg_HarmfulRate')  AS FNHarmfulRate,"
                sSQL &= Environment.NewLine & "       (SELECT L2.FTKeyValue"
                sSQL &= Environment.NewLine & "        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..[THRMConfig] AS L2 (NOLOCK)"
                sSQL &= Environment.NewLine & "        WHERE L2.FTKeyCode = N'Cfg_SkillRate')  AS FNSkillRate,"
                sSQL &= Environment.NewLine & "       (SELECT L3.FTKeyValue"
                sSQL &= Environment.NewLine & "        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..[THRMConfig] AS L3 (NOLOCK)"
                sSQL &= Environment.NewLine & "        WHERE L3.FTKeyCode = N'Cfg_BasicSalaryMax') AS FNMaximumBasicSalaries,"
                sSQL &= Environment.NewLine & "       (SELECT L4.FTKeyValue"
                sSQL &= Environment.NewLine & "        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..[THRMConfig] AS L4 (NOLOCK)"
                sSQL &= Environment.NewLine & "        WHERE L4.FTKeyCode = N'Cfg_ModPersonRateVN') AS FNModPersonTaxRate,"
                sSQL &= Environment.NewLine & "       (SELECT L5.FTKeyValue"
                sSQL &= Environment.NewLine & "        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..[THRMConfig] AS L5 (NOLOCK)"
                sSQL &= Environment.NewLine & "        WHERE L5.FTKeyCode = N'Cfg_ModChildAllowanceRateVN') AS FNModChildAllowanceTaxRate,"
                sSQL &= Environment.NewLine & "       (SELECT L6.FTKeyValue"
                sSQL &= Environment.NewLine & "        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..[THRMConfig] AS L6 (NOLOCK)"
                sSQL &= Environment.NewLine & "        WHERE L6.FTKeyCode = N'Cfg_ThaiWorkerNoWorkpermitTaxRate') AS FNThaiWorkerNoWorkpermitTaxRate"

                _ConfigAllowanceProbation = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_HR)

            End If

            Return _ConfigAllowanceProbation

        End Get

    End Property

    Private m_DbDtEmp As New DataTable
    ReadOnly Property DbDtEmp As DataTable
        Get
            Return m_DbDtEmp
        End Get
    End Property

 

    Private Sub ocmclose_Click(sender As Object, e As EventArgs) Handles ocmclose.Click
        Me.Close()
    End Sub
 
End Class