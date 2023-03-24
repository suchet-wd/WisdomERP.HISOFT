Imports DevExpress.XtraGrid.Columns
Imports System.Data
Imports System.String
Public Class wAddEmployeeTrainning

    Private _ProdAdd As Boolean = False
    Private _StageSetCol As String = "N"
    Public Property ProdAdd As Boolean
        Get
            Return _ProdAdd
        End Get
        Set(value As Boolean)
            _ProdAdd = value
        End Set
    End Property

#Region "General"

    Private Sub ocmload_Click(sender As System.Object, e As System.EventArgs) Handles ocmload.Click
        Dim _Dt As DataTable
        Dim _Qry As String = ""

        _Qry = " SELECT  '0' AS FTSelect,  M.FNHSysEmpID, M.FTEmpCode"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & " , P.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

        Else
            _Qry &= vbCrLf & " , P.FTPreNameNameEN + ' ' +  M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
        End If

        _Qry &= vbCrLf & " FROM        THRMEmployee AS M WITH (NOLOCK)"
        _Qry &= vbCrLf & "    INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId "



        '_Qry = "select YY.FTSelect,YY.FNHSysEmpID,YY.FTEmpCode,YY.FTEmpName,YY.Working as WorkingAge"
        '_Qry &= vbCrLf & ",Case WHEN YY.LeaveBusiOutPlanHrs<10 and YY.LeaveBusiOutPlanHrs>0 THEN '0'+convert(varchar(2),YY.LeaveBusiOutPlanHrs) ELSE"
        '_Qry &= vbCrLf & "Case WHEN YY.LeaveBusiOutPlanHrs=0 THEn '00' Else convert(varchar(5),YY.LeaveBusiOutPlanHrs)END END+':'+"
        '_Qry &= vbCrLf & "Case WHEN YY.LeaveBusiOutPlanMin<10 and YY.LeaveBusiOutPlanMin>0 THEN '0'+convert(varchar(2),YY.LeaveBusiOutPlanMin) ELSE"
        '_Qry &= vbCrLf & "Case WHEN YY.LeaveBusiOutPlanMin=0 THEN '00' Else convert(varchar(2),YY.LeaveBusiOutPlanMin) END END AS LeaveOutPlan"

        '_Qry &= vbCrLf & ",Case WHEN YY.LeaveBusiPlanHrs<10 and YY.LeaveBusiPlanHrs>0 THEN '0'+convert(varchar(5),LeaveBusiPlanHrs)else "
        '_Qry &= vbCrLf & "Case WHEN YY.LeaveBusiPlanHrs=0 THEN '00' ELSE convert(varchar(5),YY.LeaveBusiPlanHrs)END END+':'+"
        '_Qry &= vbCrLf & "Case WHEN YY.LeaveBusiPlanMin<10 and YY.LeaveBusiPlanMin>0 THEN '0'+convert(varchar(2),YY.LeaveBusiPlanMin)else"
        '_Qry &= vbCrLf & "Case WHEN YY.LeaveBusiPlanMin=0 THEN '00' ELSE convert(varchar(2),YY.LeaveBusiPlanMin) END END AS LeavePlan"

        '_Qry &= vbCrLf & ",Case WHEn YY.LateHrs<10 and YY.LateHrs>0  THEN '0'+convert(varchar(2),YY.LateHrs) else Case WHEN YY.LateHrs=0 THEn '00' Else convert(varchar(5),YY.LateHrs)end  end+':'+"
        '_Qry &= vbCrLf & "Case WHEN YY.LateMin<10 and YY.LateMin>0 THEN '0'+convert(varchar(2),YY.LateMin)else Case WHEN YY.LateMin=0 THEN '00' Else convert(varchar(2),YY.LateMin) END END AS Late"

        ''_Qry &= vbCrLf & ",Case WHEN YY.TotalHrs<10 and YY.TotalHrs>0 THEN '0'+convert(varchar(2),YY.TotalHrs) else Case WHEN YY.TotalHrs=0 THEN '00' else convert(varchar(5),YY.TotalHrs)END ENd+':'+"
        ''_Qry &= vbCrLf & "Case WHEN YY.TotalMin<10 and YY.TotalMin>0 THEN '0'+convert(varchar(2),YY.TotalMin)else Case WHEN YY.TotalMin=0 THEN '00' Else convert(varchar(2),YY.TotalMin) END END AS TotalStopWorking"
        '_Qry &= vbCrLf & ",Case WHEN YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs<10 and YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs>0"
        '_Qry &= vbCrLf & "then'0'+convert(varchar(5),YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs)else "
        '_Qry &= vbCrLf & "Case WHEN YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs=0 THEN '00' else convert(varchar(5),YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs) END END +':'+"
        '_Qry &= vbCrLf & "Case WHEN (YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin)%60 <10 and (YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin)%60 >0"
        '_Qry &= vbCrLf & "then'0'+convert(varchar(5),(YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin)%60)else "
        '_Qry &= vbCrLf & "Case WHEN (YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin)%60=0 THEN '00' else convert(varchar(5),(YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin)%60) END END AS TotalStopWorking"

        '_Qry &= vbCrLf & ",Case WHEN YY.FNAbsentHrs<10 and YY.FNAbsentHrs>0 THEN '0'+convert(varchar(2),YY.FNAbsentHrs) else Case WHEN YY.FNAbsentHrs=0 THEN '00' else convert(varchar(5),YY.FNAbsentHrs)END ENd+':'+"
        '_Qry &= vbCrLf & "Case WHEN YY.FNAbsentMin<10 and YY.FNAbsentMin>0 THEN '0'+convert(varchar(2),YY.FNAbsentMin)else Case WHEN YY.FNAbsentMin=0 THEN '00' Else convert(varchar(2),YY.FNAbsentMin) END END AS FNAbsent"

        '_Qry &= vbCrLf & "from"
        '_Qry &= vbCrLf & "(select XX.FTSelect,XX.FNHSysEmpID,XX.FTEmpCode,XX.FTEmpName,XX.Working"
        '_Qry &= vbCrLf & ",floor(Sum(XX.FNLateNormalMin)/60) AS LateHrs"
        '_Qry &= vbCrLf & ",floor(Sum(XX.FNLateNormalMin)%60) AS LateMin"
        '_Qry &= vbCrLf & ",floor(Sum(XX.LeaveBusiPlan)/60) AS LeaveBusiPlanHrs"
        '_Qry &= vbCrLf & ",floor(Sum(XX.LeaveBusiPlan)%60) AS LeaveBusiPlanMin"
        '_Qry &= vbCrLf & ",floor(SUM(XX.LeaveBusiOutPlan)/60) AS LeaveBusiOutPlanHrs"
        '_Qry &= vbCrLf & ",floor(SUM(XX.LeaveBusiOutPlan)%60) AS LeaveBusiOutPlanMin"
        '_Qry &= vbCrLf & ",floor(SUM(XX.FNAbsent)/60) AS FNAbsentHrs"
        '_Qry &= vbCrLf & ",floor(SUM(XX.FNAbsent)%60) AS FNAbsentMin"
        ''_Qry &= vbCrLf & ",floor(SUM(XX.TotalTime)/60) AS TotalHrs"
        ''_Qry &= vbCrLf & ",floor(SUM(XX.TotalTime)%60) AS TotalMin"
        '_Qry &= vbCrLf & "from "
        '_Qry &= vbCrLf & "(select  KK.FTSelect,KK.FNHSysEmpID,KK.FTEmpCode,KK.FTEmpName"
        '_Qry &= vbCrLf & ",convert(varchar(2),KK.FNEmpWorkAgeYear)+':'+convert(varchar(2),KK.FNEmpWorkAgeMoth) AS Working"
        '_Qry &= vbCrLf & ",CASE WHEN ISNULL(KK.FNTimeMin,0)<=0 THEN ISNULL(KK.LeaveBusi,0) ELSE 0 END + KK.LeaveVacation+KK.LeaveMaternity  AS LeaveBusiPlan"
        '_Qry &= vbCrLf & ",CASE WHEN ISNULL(KK.FNTimeMin,0)>0 THEN ISNULL(KK.LeaveBusi,0) ELSE 0 END + KK.LeaveSick + KK.FNAbsent AS LeaveBusiOutPlan"
        '_Qry &= vbCrLf & ",KK.FNLateNormalMin"
        '_Qry &= vbCrLf & ",KK.FNAbsent"
        ''_Qry &= vbCrLf & ",CASE WHEN ISNULL(KK.FNTimeMin,0)<=0 THEN ISNULL(KK.LeaveBusi,0) ELSE 0 END + KK.LeaveVacation+KK.LeaveMaternity+"
        ''_Qry &= vbCrLf & "CASE WHEN ISNULL(KK.FNTimeMin,0)>0 THEN ISNULL(KK.LeaveBusi,0) ELSE 0 END + KK.LeaveSick + KK.FNAbsent+KK.FNLateNormalMin AS TotalTime"

        '_Qry &= vbCrLf & "from"
        '_Qry &= vbCrLf & "(Select distinct '0' AS FTSelect,  M.FNHSysEmpID, M.FTEmpCode"
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & " , P.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

        'Else
        '    _Qry &= vbCrLf & " , P.FTPreNameNameEN + ' ' +  M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
        'End If
        '_Qry &= vbCrLf & ",Floor([" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd)/12) AS FNEmpWorkAgeYear"
        '_Qry &= vbCrLf & ",Floor([" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd)%12) AS FNEmpWorkAgeMoth"
        '_Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute)"
        '_Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS TL WITH(NOLOCK)"
        '_Qry &= vbCrLf & "WHERE FNHSysEmpID=T.FNHSysEmpID"
        '_Qry &= vbCrLf & "AND FTDateTrans=T.FTDateTrans"
        '_Qry &= vbCrLf & "AND FTLeaveType='1') ,0) AS LeaveBusi"
        '_Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute)"
        '_Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS TL WITH(NOLOCK)"
        '_Qry &= vbCrLf & "WHERE FNHSysEmpID=T.FNHSysEmpID"
        '_Qry &= vbCrLf & "AND FTDateTrans=T.FTDateTrans"
        '_Qry &= vbCrLf & "AND FTLeaveType='98'),0) AS LeaveVacation"
        '_Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute)"
        '_Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS TL WITH(NOLOCK)"
        '_Qry &= vbCrLf & "WHERE FNHSysEmpID=T.FNHSysEmpID"
        '_Qry &= vbCrLf & "AND FTDateTrans=T.FTDateTrans"
        '_Qry &= vbCrLf & "AND FTLeaveType='97'),0) AS LeaveMaternity"
        '_Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute)"
        '_Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS TL WITH(NOLOCK)"
        '_Qry &= vbCrLf & "WHERE FNHSysEmpID=T.FNHSysEmpID"
        '_Qry &= vbCrLf & "AND FTDateTrans=T.FTDateTrans"
        '_Qry &= vbCrLf & "AND FTLeaveType='0'),0) AS LeaveSick"
        '_Qry &= vbCrLf & ",T.FNTimeMin,T.FNAbsent,T.FNLateNormalMin"

        '_Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK)"
        '_Qry &= vbCrLf & "INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
        '_Qry &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH(NOLOCK) ON M.FNHSysEmpID=T.FNHSysEmpID"
        '_Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
        '_Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId "
        '_Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId "
        '_Qry &= vbCrLf & "LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId "




        _Qry &= vbCrLf & " WHERE  M.FTEmpCode <> '' AND M.FNEmpStatus <> '2' "

        _Qry &= vbCrLf & " AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " "

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
        _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)

        '_Qry &= vbCrLf & ") as KK"
        '_Qry &= vbCrLf & ") AS XX"
        '_Qry &= vbCrLf & "group by XX.FTSelect,XX.FNHSysEmpID,XX.FTEmpCode,XX.Working,XX.FTEmpName ) AS YY"
        _Qry &= vbCrLf & "  ORDER BY FNHSysEmpID ASC "

        '_Qry &= vbCrLf & "  ORDER BY M.FTEmpCode ASC "

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.ogc.DataSource = _Dt
    End Sub

    Private Sub ochkselectall_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ochkselectall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With ogc
                If Not (.DataSource Is Nothing) Then
                    With ogv
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.ProdAdd = False
        Me.Close()
    End Sub

    Private Sub ocmOK_Click(sender As System.Object, e As System.EventArgs) Handles ocmOK.Click
        'If CType(Me.ogc.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then
        'If CType(Me.ogcTrain.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then
        Me.ProdAdd = True
        Me.Close()
        'End If
        'End If
    End Sub

    Private Sub wAddEmployeeTrainning_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        HI.ST.Lang.SP_SETxLanguage(Me)
    End Sub

#End Region

    'Private Sub ocmLoadTrainer_Click(sender As Object, e As EventArgs) Handles ocmLoadTrainer.Click
    '    Dim Qry As String = ""
    '    Dim dt As DataTable
    '    Dim Name As String = ""
    '    Dim EmpId As Integer = 0
    '    'Dim _SourceRow As DataTable = ogcTrain.DataSource
    '    'Dim row As DataRow

    '    Try
    '        'With ogvTrain
    '        '    For i As Integer = .Columns.Count - 1 To 0 Step -1
    '        '        Select Case .Columns(i).FieldName.ToString
    '        '            Case "FTTrainer"
    '        '                With .Columns(i)
    '        '                    .OptionsColumn.ReadOnly = True
    '        '                    .OptionsColumn.AllowEdit = False
    '        '                End With
    '        '        End Select
    '        '    Next
    '        'End With

    '        If Me.FNHSysEmpIdTrain.Text <> "" Then
    '            Qry = "select '0' as FTSelect"
    '            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
    '                Qry &= vbCrLf & " , P.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTTrainer"
    '            Else
    '                Qry &= vbCrLf & " , P.FTPreNameNameEN + ' ' +  M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTTrainer"
    '            End If

    '            Qry &= vbCrLf & ",M.FNHSysEmpID FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK)"
    '            Qry &= vbCrLf & "    INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
    '            Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
    '            Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
    '            Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId "
    '            Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId "
    '            Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId "
    '            Qry &= vbCrLf & " WHERE  M.FTEmpCode <> '' AND M.FNEmpStatus <> '2' "
    '            Qry &= vbCrLf & " AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " "
    '            Qry &= vbCrLf & "AND M.FTEmpCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTrain.Text) & "' "

    '            dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)
    '            'If Me.ogcTrain.DataSource Is Nothing Then
    '            Me.ogcTrain.DataSource = dt
    '            'Else
    '            '    For Each R As DataRow In dt.Rows
    '            '        Name = R!FTTrainer.ToString
    '            '        EmpId = R!FNHSysEmpID.ToString
    '            '        CType(Me.ogcTrain.DataSource, DataTable).Rows.Add("0", Name, EmpId)
    '            '    Next
    '            'End If
    '        End If
    '    Catch ex As Exception

    '    End Try


    'End Sub

    'Private Sub ocmAddTrainerExternal_Click(sender As Object, e As EventArgs) Handles ocmAddTrainerExternal.Click
    '    'Dim _row As Integer
    '    '_row = CType(ogcTrain.DataSource, DataTable).Rows.Count
    '    Dim dt As DataTable = Getdatatable()
    '    Try
    '        If _StageSetCol = "N" Then
    '            With ogvTrain
    '                For i As Integer = .Columns.Count - 1 To 0 Step -1
    '                    Select Case .Columns(i).FieldName.ToString
    '                        Case "FTTrainer"
    '                            With .Columns(i)
    '                                .OptionsColumn.ReadOnly = False
    '                                .OptionsColumn.AllowEdit = True
    '                            End With
    '                            Exit For
    '                    End Select
    '                Next
    '            End With
    '            _StageSetCol = "Y"
    '        End If
    '        If Me.ogcTrain.DataSource Is Nothing Then
    '            Me.ogcTrain.DataSource = dt
    '        Else
    '            CType(ogcTrain.DataSource, DataTable).Rows.Add("0", "", 0)
    '        End If

    '    Catch ex As Exception
    '        Me.Close()
    '    End Try
    'End Sub

    'Private Function Getdatatable()
    '    Dim dt As New DataTable
    '    dt.Columns.Add("FTSelect")
    '    dt.Columns.Add("FTTrainer")
    '    dt.Columns.Add("FNHSysEmpID")
    '    dt.Rows.Add("0", "", 0)
    '    Return dt
    'End Function
End Class