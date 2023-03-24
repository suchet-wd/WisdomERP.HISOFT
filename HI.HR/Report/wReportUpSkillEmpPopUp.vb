Imports System.Windows.Forms
Imports System.Windows.Forms.Control
Imports System.Drawing
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid.Columns
Imports System.IO
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports System.Globalization
Public Class wReportUpSkillEmpPopUp

    Private _FormAddEmp As wAddEmployeeTrainning
    Private _Timstr As String
    Private _FormLoad As Boolean = True
    Private _T1 As Integer
    Private _TimeGrid As Integer
    Private ChkBtn As Integer = 1
    Private _Time1 As String = ""
    Private _Time2 As String = ""
    Private GTrainNameActiveRemoveList As Boolean
    Private GEmpNameActiveRemoveList As Boolean
    Private GBMultiActiveRemoveList As Boolean
    Private _ColCount As Integer = 0
    Private _ColCountMulti As Integer = 0
    Private _LoadLang As Boolean = False
    Private _ChkDatatableMaster As Boolean = False
    Private _ChkAddEmpMulti As Boolean = False
    Private _Gencol As Boolean = False
    Private _colfocus As String = ""
    Private _colSkip As String = ""
    Private _colSkipTopic As String = ""
    Private dtHead As DataTable
    Private dtcol As DataTable
    Private dtRank As DataTable
    Private _dtMulti As DataTable
    Private _StatePass As Integer
    Private _DateLec As String = ""
    Private _FormLoadDt As Boolean
    Private _Transac As Boolean = False
    Private _SectType As Boolean = False
    Private Sub wReportUpSkillEmpPopUp_Load(sender As Object, e As EventArgs) Handles Me.Load
        _dtMulti = Nothing

        HI.TL.METHOD.CallActiveToolBarFunction(Me)
        ChkBtn = 0
        _ChkAddEmpMulti = True
        'Call LoadTrainName()
        Call RemoveColMultiSkill()
        dtHead = CreatedataTableMasterHeadgb()
        dtcol = CreatedataTableMasterMultiSkill()
        dtRank = CreatedatatableRanking()
        If dtcol.Rows.Count > 0 Then
            _ChkDatatableMaster = True
        Else
            _ChkDatatableMaster = False
            Exit Sub
        End If
        Call GenGridBanedMultiSkill(dtcol, dtHead, dtRank)
        _Gencol = False
        _dtMulti = LoadDatatablePrepareMultiSkill()
        ogcSkill.DataSource = _dtMulti
        'LoadDataMultiSkill()
        'If CType(ogcTrainer.DataSource, DataTable).Rows.Count <= 0 Then
        '    Call GenrowTrainName()
        'End If

        LoadDataMultiSkill()


    End Sub


#Region "Property"
    'Private _AddComplete As Boolean = False
    'Public Property AddComplete As Boolean
    '    Get
    '        Return _AddComplete
    '    End Get
    '    Set(value As Boolean)
    '        _AddComplete = value
    '    End Set
    'End Property
    'Private _PONO As String = ""
    'Public Property PONO As String
    '    Get
    '        Return _PONO
    '    End Get
    '    Set(value As String)
    '        _PONO = value
    '    End Set
    'End Property
    Private _Qry As String = ""
    Public Property __Qry As String
        Get
            Return _Qry
        End Get
        Set(value As String)
            _Qry = value
        End Set
    End Property
    Private _Str As String = ""
    Public Property Str As String
        Get
            Return _Str
        End Get
        Set(value As String)
            _Str = value
        End Set
    End Property
#End Region

    Private Sub LoadDataMultiSkill()
        Dim Qry As String = ""
        Dim dtCountEmp As DataTable
        Dim dtDataEmpMulti As DataTable
        Dim dtList As DataTable
        Dim dtListRank As DataTable
        Dim ArrDy(_ColCountMulti - 1)
        'Dim i As Integer = 0
        Dim Indx As Integer = 0
        Dim _Val As Integer = 0
        Dim _colDivi As Integer = 0
        Dim _cal As Integer = 0
        Dim _Sum As Integer = 0
        Dim _NetSum As Integer = 0
        Dim _SumAll As Integer = 0
        Dim _NetAll As Integer = 0
        Dim _colDiviAll As Integer = 0
        'Dim _Str As String = ""
        Dim _Val25 As Integer = 0
        Dim _Val50 As Integer = 0
        Dim _Val75 As Integer = 0
        Dim _Val100 As Integer = 0
        Dim _ChkValCount As Integer = 0
        Dim _PositMaster As Integer = 0
        Dim _PositEmp As Integer = 0
        'Dim _Val25 As Integer = 0 : Dim _Val50 As Integer = 0 : Dim _Val75 As Integer = 0 : Dim _Val100 As Integer = 0
        Dim _C25 As Integer = 0 : Dim _C50 As Integer = 0 : Dim _C75 As Integer = 0 : Dim _C100 As Integer = 0
        Dim _V25 As Integer = 0 : Dim _V50 As Integer = 0 : Dim _V75 As Integer = 0 : Dim _V100 As Integer = 0
        Try

            Qry = "select JJ.FTDocNo,JJ.FNHSysEmpID,JJ.FTEmpName,JJ.WorkingAge,JJ.LeavePlan,JJ.LeaveOutPlan,JJ.FNAbsent,JJ.Late,JJ.TotalStopWorking from"
            Qry &= vbCrLf & "(select YY.FTSelect,YY.FNHSysEmpID,YY.FTEmpCode,YY.FTEmpName,YY.Working as WorkingAge"
            Qry &= vbCrLf & ",Case WHEN YY.LeaveBusiOutPlanHrs<10 and YY.LeaveBusiOutPlanHrs>0 THEN '0'+convert(varchar(2),YY.LeaveBusiOutPlanHrs) ELSE"
            Qry &= vbCrLf & "Case WHEN YY.LeaveBusiOutPlanHrs=0 THEn '00' Else convert(varchar(5),YY.LeaveBusiOutPlanHrs)END END+':'+"
            Qry &= vbCrLf & "Case WHEN YY.LeaveBusiOutPlanMin<10 and YY.LeaveBusiOutPlanMin>0 THEN '0'+convert(varchar(2),YY.LeaveBusiOutPlanMin) ELSE"
            Qry &= vbCrLf & "Case WHEN YY.LeaveBusiOutPlanMin=0 THEN '00' Else convert(varchar(2),YY.LeaveBusiOutPlanMin) END END AS LeaveOutPlan"
            Qry &= vbCrLf & ",Case WHEN YY.LeaveBusiPlanHrs<10 and YY.LeaveBusiPlanHrs>0 THEN '0'+convert(varchar(5),LeaveBusiPlanHrs)else "
            Qry &= vbCrLf & "Case WHEN YY.LeaveBusiPlanHrs=0 THEN '00' ELSE convert(varchar(5),YY.LeaveBusiPlanHrs)END END+':'+"
            Qry &= vbCrLf & "Case WHEN YY.LeaveBusiPlanMin<10 and YY.LeaveBusiPlanMin>0 THEN '0'+convert(varchar(2),YY.LeaveBusiPlanMin)else"
            Qry &= vbCrLf & "Case WHEN YY.LeaveBusiPlanMin=0 THEN '00' ELSE convert(varchar(2),YY.LeaveBusiPlanMin) END END AS LeavePlan"
            Qry &= vbCrLf & ",Case WHEn YY.LateHrs<10 and YY.LateHrs>0  THEN '0'+convert(varchar(2),YY.LateHrs) else Case WHEN YY.LateHrs=0 THEn '00' Else convert(varchar(5),YY.LateHrs)end  end+':'+"
            Qry &= vbCrLf & "Case WHEN YY.LateMin<10 and YY.LateMin>0 THEN '0'+convert(varchar(2),YY.LateMin)else Case WHEN YY.LateMin=0 THEN '00' Else convert(varchar(2),YY.LateMin) END END AS Late"

            Qry &= vbCrLf & ",Case WHEN YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs<10 and YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs>0"
            Qry &= vbCrLf & "then'0'+convert(varchar(5),YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs)else "
            Qry &= vbCrLf & "Case WHEN YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs=0 THEN '00' else convert(varchar(5),YY.LateHrs+YY.LeaveBusiOutPlanHrs+YY.LeaveBusiPlanHrs+YY.FNAbsentHrs) END END +':'+"

            Qry &= vbCrLf & "Case WHEN (YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin)%60 <10 and (YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin)%60 >0"
            Qry &= vbCrLf & "then'0'+convert(varchar(5),((YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin)%60))else "
            Qry &= vbCrLf & "Case WHEN (YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin)%60 =0 THEN '00' else convert(varchar(5),((YY.LateMin+YY.LeaveBusiOutPlanMin+YY.LeaveBusiPlanMin+YY.FNAbsentMin)%60)) END END AS TotalStopWorking"

            Qry &= vbCrLf & ",Case WHEN YY.FNAbsentHrs<10 and YY.FNAbsentHrs>0 THEN '0'+convert(varchar(2),YY.FNAbsentHrs) else Case WHEN YY.FNAbsentHrs=0 THEN '00' else convert(varchar(5),YY.FNAbsentHrs)END ENd+':'+"
            Qry &= vbCrLf & "Case WHEN YY.FNAbsentMin<10 and YY.FNAbsentMin>0 THEN '0'+convert(varchar(2),YY.FNAbsentMin)else Case WHEN YY.FNAbsentMin=0 THEN '00' Else convert(varchar(2),YY.FNAbsentMin) END END AS FNAbsent"
            Qry &= vbCrLf & ",ZZ.FTDocNo"
            Qry &= vbCrLf & "from"
            Qry &= vbCrLf & "(select XX.FTSelect,XX.FNHSysEmpID,XX.FTEmpCode,XX.FTEmpName,XX.Working"
            Qry &= vbCrLf & ",floor(Sum(XX.FNLateNormalMin)/60) AS LateHrs"
            Qry &= vbCrLf & ",floor(Sum(XX.FNLateNormalMin)%60) AS LateMin"
            Qry &= vbCrLf & ",floor(Sum(XX.LeaveBusiPlan)/60) AS LeaveBusiPlanHrs"
            Qry &= vbCrLf & ",floor(Sum(XX.LeaveBusiPlan)%60) AS LeaveBusiPlanMin"
            Qry &= vbCrLf & ",floor(SUM(XX.LeaveBusiOutPlan)/60) AS LeaveBusiOutPlanHrs"
            Qry &= vbCrLf & ",floor(SUM(XX.LeaveBusiOutPlan)%60) AS LeaveBusiOutPlanMin"
            Qry &= vbCrLf & ",floor(SUM(XX.FNAbsent)/60) AS FNAbsentHrs"
            Qry &= vbCrLf & ",floor(SUM(XX.FNAbsent)%60) AS FNAbsentMin"
            Qry &= vbCrLf & "from"
            Qry &= vbCrLf & "(select  KK.FTSelect,KK.FNHSysEmpID,KK.FTEmpCode,KK.FTEmpName"
            Qry &= vbCrLf & ",convert(varchar(2),KK.FNEmpWorkAgeYear)+':'+convert(varchar(2),KK.FNEmpWorkAgeMoth) AS Working"
            Qry &= vbCrLf & ",CASE WHEN ISNULL(KK.FNTimeMin,0)<=0 THEN ISNULL(KK.LeaveBusi,0) ELSE 0 END + KK.LeaveVacation+KK.LeaveMaternity  AS LeaveBusiPlan"
            Qry &= vbCrLf & ",CASE WHEN ISNULL(KK.FNTimeMin,0)>0 THEN ISNULL(KK.LeaveBusi,0) ELSE 0 END + KK.LeaveSick + KK.FNAbsent AS LeaveBusiOutPlan"
            Qry &= vbCrLf & ",KK.FNLateNormalMin"
            Qry &= vbCrLf & ",KK.FNAbsent"
            Qry &= vbCrLf & "from"
            Qry &= vbCrLf & "(Select distinct '0' AS FTSelect,  M.FNHSysEmpID, M.FTEmpCode"
            Qry &= vbCrLf & ", P.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"
            Qry &= vbCrLf & ",Floor([" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd)/12) AS FNEmpWorkAgeYear"
            Qry &= vbCrLf & ",Floor([" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd)%12) AS FNEmpWorkAgeMoth"
            Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute)"
            Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS TL WITH(NOLOCK)"
            Qry &= vbCrLf & "WHERE FNHSysEmpID = T.FNHSysEmpID"
            Qry &= vbCrLf & "AND FTDateTrans=T.FTDateTrans"
            Qry &= vbCrLf & "AND FTLeaveType='1') ,0) AS LeaveBusi"
            Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute)"
            Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS TL WITH(NOLOCK)"
            Qry &= vbCrLf & "WHERE FNHSysEmpID = T.FNHSysEmpID"
            Qry &= vbCrLf & "AND FTDateTrans=T.FTDateTrans"
            Qry &= vbCrLf & "AND FTLeaveType='98'),0) AS LeaveVacation"
            Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute)"
            Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS TL WITH(NOLOCK)"
            Qry &= vbCrLf & "WHERE FNHSysEmpID = T.FNHSysEmpID"
            Qry &= vbCrLf & "AND FTDateTrans=T.FTDateTrans"
            Qry &= vbCrLf & "AND FTLeaveType='97'),0) AS LeaveMaternity"
            Qry &= vbCrLf & ",ISNULL((SELECT SUM(FNTotalMinute)"
            Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave AS TL WITH(NOLOCK)"
            Qry &= vbCrLf & "WHERE FNHSysEmpID = T.FNHSysEmpID"
            Qry &= vbCrLf & "AND FTDateTrans=T.FTDateTrans"
            Qry &= vbCrLf & "AND FTLeaveType='0'),0) AS LeaveSick"
            Qry &= vbCrLf & ",T.FNTimeMin,T.FNAbsent,T.FNLateNormalMin"
            Qry &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN  "
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH(NOLOCK) ON M.FNHSysEmpID=T.FNHSysEmpID LEFT OUTER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH(NOLOCK)  ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN "
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect As U With(NOLOCK)  On M.FNHSysUnitSectId = U.FNHSysUnitSectId LEFT OUTER JOIN "
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS D WITH(NOLOCK)  ON M.FNHSysDivisonId = D.FNHSysDivisonId LEFT OUTER JOIN "
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS DP WITH(NOLOCK)  ON M.FNHSysDeptId = DP.FNHSysDeptId"
            Qry &= vbCrLf & "WHERE  M.FTEmpCode <> '' AND M.FNEmpStatus <> '2' "
            Qry &= vbCrLf & "" & _Str & ""
            Qry &= vbCrLf & ") as KK"
            Qry &= vbCrLf & ") AS XX"
            Qry &= vbCrLf & "group by XX.FTSelect,XX.FNHSysEmpID,XX.FTEmpCode,XX.Working,XX.FTEmpName ) AS YY INNER JOIN"
            Qry &= vbCrLf & "(select distinct Multi.FNHSysEmpID,Multi.FTDocNo"
            Qry &= vbCrLf & "from"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpSkillMatrix AS Multi with(nolock) LEFT OUTER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMSkillMatrix As Mas With(nolock) On Multi.FNHSysSkillMatrixId = Mas.FNHSysSkillMatrixId Left OUTER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData AS L with(nolock) ON Mas.FNSectType = L.FNListIndex"
            Qry &= vbCrLf & "WHERE Multi.FTDocNo <> '' AND L.FTListName = 'FNSectType'"
            Qry &= vbCrLf & "" & _Qry & ""
            Qry &= vbCrLf & ") AS ZZ ON YY.FNHSysEmpID=ZZ.FNHSysEmpID"
            Qry &= vbCrLf & ") AS JJ"
            dtCountEmp = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)

            Qry = "select  Mul.FNHSysEmpID,Mul.FTDocNo,Mul.FNPoint,Mas.FNSkillMatrix,Mul.FNHSysSkillMatrixId from"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMSkillMatrix AS Mas with(nolock) Left OUTER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTTrainEmpSkillMatrix AS Mul with(nolock) ON Mas.FNHSysSkillMatrixId=Mul.FNHSysSkillMatrixId LEFT OUTER JOIN"
            Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData AS L with(nolock) ON Mas.FNSectType = L.FNListIndex"
            Qry &= vbCrLf & "WHERE Mul.FTDocNo <> '' AND L.FTListName = 'FNSectType'"
            Qry &= vbCrLf & "" & _Qry & ""
            dtDataEmpMulti = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_HR)

            Qry = "SELECT DISTINCT FNSkillMatrix AS FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMSkillMatrix"
            'Qry = "Select FNListIndex"
            'Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData WHERE FTListName='FNSkillMatrix'"
            dtList = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_SYSTEM)

            Qry = "SELECT FNListIndex"
            Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData WHERE FTListName='FNRankingMultiSkill'"
            dtListRank = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_SYSTEM)

            If _dtMulti.Rows.Count <= 0 Then
                For Each R As DataRow In dtCountEmp.Rows
                    Dim ArrFix() = {R!FNHSysEmpID, R!FTEmpName.ToString, R!FTDocNo.ToString, R!WorkingAge.ToString, R!LeavePlan.ToString, R!LeaveOutPlan.ToString, R!FNAbsent.ToString, R!Late.ToString, R!TotalStopWorking.ToString}
                    For Each X As DataRow In dtList.Rows
                        For Each Z As DataRow In dtcol.Select("FNSkillMatrix=" & X!FNListIndex & "")
                            _PositEmp = 0
                            _PositMaster = 0
                            _PositMaster += 1
                            For Each Rs As DataRow In dtDataEmpMulti.Select("FTDocNo='" & R!FTDocNo.ToString & "' AND FNHSysEmpID=" & R!FNHSysEmpID & "AND FNSkillMatrix=" & X!FNListIndex & " AND FNHSysSkillMatrixId=" & Val(Z!FNHSysSkillMatrixId.ToString.Substring(0, 10)) & "")
                                ArrDy(Indx) = Rs!FNPoint
                                Indx += 1
                                _PositEmp += 1
                                Exit For
                            Next
                            If _PositEmp <> _PositMaster Then
                                ArrDy(Indx) = 0
                                Indx += 1
                            End If
                        Next
                        ArrDy(Indx) = 0
                        Indx += 1
                    Next
                    Indx -= 1

                    For i As Integer = 0 To dtListRank.Rows.Count - 1 Step 1
                        ArrDy(Indx) = 0
                        Indx += 1
                    Next
                    For Each Y As DataColumn In CType(ogcSkill.DataSource, DataTable).Columns
                        Select Case Y.ColumnName.ToString
                            Case "FTSummerySkillEMP25s", "FTSummerySkillEMP50s", "FNSummerySkillUP75s", "FTSummerySkillEMP100s"
                                ArrDy(Indx) = 0
                                Indx += 1
                        End Select
                    Next
                    Dim LoadDataEmpMultiArrFulls((ArrFix.Length) + (ArrDy.Length - 1))
                    ArrFix.CopyTo(LoadDataEmpMultiArrFulls, 0)
                    ArrDy.CopyTo(LoadDataEmpMultiArrFulls, ArrFix.Length)
                    _dtMulti.Rows.Add(LoadDataEmpMultiArrFulls)
                    Indx = 0
                Next
                ogcSkill.DataSource = _dtMulti
            End If


            With ogbvSkill
                For Row As Integer = 0 To .RowCount - 1 Step 1
                    _C100 = 0 : _C25 = 0 : _C50 = 0 : _C75 = 0 : _V100 = 0 : _V25 = 0 : _V50 = 0 : _V75 = 0
                    For a As Integer = 0 To dtList.Rows.Count - 1 Step 1
                        _Val = 0
                        _colDivi = 0
                        For k As Integer = 0 To .Columns.Count - 1 Step 1
                            If Microsoft.VisualBasic.Left(.Columns(k).Name.ToString, 2) = "FN" And (Microsoft.VisualBasic.Left(.Columns(k).FieldName.ToString, 5)) <> "FNSum" And (Microsoft.VisualBasic.Left(.Columns(k).FieldName.ToString, 5)) <> "FTSum" And
                                Microsoft.VisualBasic.Right(.Columns(k).FieldName.ToString, 1) = a.ToString Then
                                If .Columns(k).Name.ToString <> "FNSum" & a.ToString Then
                                    '_Val += .GetRowCellValue(Row, .Columns(k).FieldName.ToString)
                                    '_colDivi += 1
                                    If .GetRowCellValue(Row, .Columns(k).FieldName.ToString) = 25 Then
                                        _C25 += 1
                                    ElseIf .GetRowCellValue(Row, .Columns(k).FieldName.ToString) = 50 Then
                                        _C50 += 1
                                    ElseIf .GetRowCellValue(Row, .Columns(k).FieldName.ToString) = 75 Then
                                        _C75 += 1
                                    ElseIf .GetRowCellValue(Row, .Columns(k).FieldName.ToString) = 100 Then
                                        _C100 += 1
                                    End If
                                Else
                                End If
                            End If
                            If .Columns(k).Name.ToString = "FNSum" & a.ToString Then
                                If _C25 > 0 Then
                                    _V25 = _C25 * 1
                                End If
                                If _C50 > 0 Then
                                    _V50 = _C50 * 2
                                End If
                                If _C75 > 0 Then
                                    _V75 = _C75 * 3
                                End If
                                If _C100 > 0 Then
                                    _V100 = _C100 * 4
                                End If
                                If a.ToString = "0" Then
                                    _NetSum = (_V25 + _V50 + _V75 + _V100) * 3
                                ElseIf a.ToString = "1" Then
                                    _NetSum = (_V25 + _V50 + _V75 + _V100) * 2
                                ElseIf a.ToString = "2" Then
                                    _NetSum = (_V25 + _V50 + _V75 + _V100) * 1
                                End If
                                .SetRowCellValue(Row, "FNSum" & a.ToString, _NetSum)
                                _C100 = 0 : _C25 = 0 : _C50 = 0 : _C75 = 0 : _V100 = 0 : _V25 = 0 : _V50 = 0 : _V75 = 0
                                Exit For
                            End If
                        Next
                        '_Sum = _Val / _colDivi

                        'If _Sum >= 0 And _Sum < 25 Then
                        '    _NetSum = 0
                        'ElseIf _Sum >= 25 And _Sum <= 49 Then
                        '    _NetSum = 25
                        'ElseIf _Sum >= 50 And _Sum <= 74 Then
                        '    _NetSum = 50
                        'ElseIf _Sum >= 75 And _Sum <= 99 Then
                        '    _NetSum = 75
                        'Else
                        '    _NetSum = 100
                        'End If

                    Next

                    _SumAll = 0
                    _colDiviAll = 0
                    For Each R As DataRow In dtList.Rows
                        For c As Integer = .Columns.Count - 1 To 0 Step -1
                            If Microsoft.VisualBasic.Right(.Columns(c).FieldName.ToString, 1) = R!FNListIndex.ToString And (Microsoft.VisualBasic.Left(.Columns(c).FieldName.ToString, 5)) <> "FNSum" And (Microsoft.VisualBasic.Left(.Columns(c).FieldName.ToString, 5)) <> "FTSum" Then
                                If .Columns(c).Name.ToString <> "FNSum" & R!FNListIndex.ToString Then
                                    _SumAll += .GetRowCellValue(Row, .Columns(c).FieldName.ToString)
                                    _colDiviAll += 1
                                End If
                            End If
                        Next
                    Next
                    _NetAll = _SumAll / _colDiviAll
                    If _NetAll >= 76 And _NetAll <= 100 Then
                        .SetRowCellValue(Row, "cFNExcellence", "1")
                        .SetRowCellValue(Row, "cFNGood", "0")
                        .SetRowCellValue(Row, "cFNFair", "0")
                        .SetRowCellValue(Row, "cFNBeginner", "0")
                    ElseIf _NetAll >= 51 And _NetAll <= 75 Then
                        .SetRowCellValue(Row, "cFNGood", "1")
                        .SetRowCellValue(Row, "cFNExcellence", "0")
                        .SetRowCellValue(Row, "cFNFair", "0")
                        .SetRowCellValue(Row, "cFNBeginner", "0")
                    ElseIf _NetAll >= 26 And _NetAll <= 50 Then
                        .SetRowCellValue(Row, "cFNFair", "1")
                        .SetRowCellValue(Row, "cFNGood", "0")
                        .SetRowCellValue(Row, "cFNExcellence", "0")
                        .SetRowCellValue(Row, "cFNBeginner", "0")
                    ElseIf _NetAll >= 0 And _NetAll <= 25 Then
                        .SetRowCellValue(Row, "cFNBeginner", "1")
                        .SetRowCellValue(Row, "cFNFair", "0")
                        .SetRowCellValue(Row, "cFNGood", "0")
                        .SetRowCellValue(Row, "cFNExcellence", "0")
                    End If
                    _Val25 = 0
                    _Val50 = 0
                    _Val75 = 0
                    _Val100 = 0
                    For Each RR As DataRow In dtList.Rows
                        For i As Integer = .Columns.Count - 1 To 0 Step -1
                            If Microsoft.VisualBasic.Right(.Columns(i).FieldName.ToString, 1) = RR!FNListIndex.ToString And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FNSum" _
                                And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FTSum" Then
                                _ChkValCount = .GetRowCellValue(Row, .Columns(i).FieldName.ToString)
                                If _ChkValCount = 25 Then
                                    _Val25 += 1
                                ElseIf _ChkValCount = 50 Then
                                    _Val50 += 1
                                ElseIf _ChkValCount = 75 Then
                                    _Val75 += 1
                                ElseIf _ChkValCount = 100 Then
                                    _Val100 += 1
                                End If
                            End If
                        Next
                    Next
                    .SetRowCellValue(Row, "FTSummerySkillEMP25s", _Val25)
                    .SetRowCellValue(Row, "FTSummerySkillEMP50s", _Val50)
                    .SetRowCellValue(Row, "FNSummerySkillUP75s", _Val75)
                    .SetRowCellValue(Row, "FTSummerySkillEMP100s", _Val100)
                Next
            End With
        Catch ex As Exception

        End Try
    End Sub



    Private Sub GenrowTrainName()
        'Try
        '    With CType(ogcTrainer.DataSource, DataTable)
        '        .Rows.Add()
        '        .Rows(.Rows.Count - 1)!FNSeq = .Rows.Count
        '        .Rows(.Rows.Count - 1)!FTTrainerName = ""
        '        .Rows(.Rows.Count - 1)!FDStartDate = ""
        '        .Rows(.Rows.Count - 1)!FDEndDate = ""
        '        .Rows(.Rows.Count - 1)!FTStartTime = ""
        '        .Rows(.Rows.Count - 1)!FTEndTime = ""
        '        .Rows(.Rows.Count - 1)!FTTotalHour = ""
        '        .Rows(.Rows.Count - 1)!FNTotalMinute = 0
        '        .AcceptChanges()
        '    End With
        'Catch ex As Exception

        'End Try

    End Sub
    Private Function LoadDatatablePrepareMultiSkill() As DataTable
        Dim Qry As String = ""
        Dim dt As New DataTable

        With ogbvSkill
            For i As Integer = 0 To .Columns.Count - 1 Step 1
                dt.Columns.Add(.Columns(i).FieldName.ToString)
            Next
        End With
        Return dt
    End Function
    Private Sub RemoveColMultiSkill()
        Try
            Dim x As Integer = 0
            With ogbvSkill
                For i As Integer = 0 To .Columns.Count - 1 Step 1
                    .Columns.RemoveAt(0)
                Next
            End With

        Catch ex As Exception

        End Try
    End Sub






    Private Sub RepChanging(sender As Object, e As System.EventArgs)
        Dim _Str As String = ""
        Dim _cal As Integer = 0
        'Dim _Val As Integer = 0
        Dim _Val25 As Integer = 0 : Dim _Val50 As Integer = 0 : Dim _Val75 As Integer = 0 : Dim _Val100 As Integer = 0
        Dim _C25 As Integer = 0 : Dim _C50 As Integer = 0 : Dim _C75 As Integer = 0 : Dim _C100 As Integer = 0
        Dim _Sum As Integer = 0
        Dim _colDivi As Integer = 0
        Dim _colDiviAll As Integer = 0
        Dim _SumAll As Integer = 0
        Dim _NetSum As Integer = 0
        Dim _NetAll As Integer = 0
        Dim _Listindex As Integer = 0
        Try
            With ogbvSkill
                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                    _cal = CType(sender, DevExpress.XtraEditors.CalcEdit).Value
                    _Str = Microsoft.VisualBasic.Right(CType(sender, DevExpress.XtraEditors.CalcEdit).Properties.Name, 1)

                    For i As Integer = .Columns.Count - 1 To 0 Step -1
                        If Microsoft.VisualBasic.Right(.Columns(i).FieldName.ToString, 1) = _colfocus And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FNSum" And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FTSum" Then
                            If .Columns(i).Name.ToString <> _colSkip Then
                                '_Val += .GetRowCellValue(.FocusedRowHandle, .Columns(i).FieldName.ToString)

                                If .GetRowCellValue(.FocusedRowHandle, .Columns(i).FieldName.ToString) = 25 Then
                                    _C25 += 1
                                ElseIf .GetRowCellValue(.FocusedRowHandle, .Columns(i).FieldName.ToString) = 50 Then
                                    _C50 += 1
                                ElseIf .GetRowCellValue(.FocusedRowHandle, .Columns(i).FieldName.ToString) = 75 Then
                                    _C75 += 1
                                ElseIf .GetRowCellValue(.FocusedRowHandle, .Columns(i).FieldName.ToString) = 100 Then
                                    _C100 += 1
                                End If
                            End If
                            '_colDivi += 1
                        End If
                    Next
                    If _cal = 25 Then
                        _C25 += 1
                    ElseIf _cal = 50 Then
                        _C50 += 1
                    ElseIf _cal = 75 Then
                        _C75 += 1
                    ElseIf _cal = 100 Then
                        _C100 += 1
                    End If
                End With
                If _C25 > 0 Then
                    _Val25 = _C25 * 1
                End If
                If _C50 > 0 Then
                    _Val50 = _C50 * 2
                End If
                If _C75 > 0 Then
                    _Val75 = _C75 * 3
                End If
                If _C100 > 0 Then
                    _Val100 = _C100 * 4
                End If
                If _Str = "0" Then
                    _NetSum = (_Val25 + _Val50 + _Val75 + _Val100) * 3
                ElseIf _Str = "1" Then
                    _NetSum = (_Val25 + _Val50 + _Val75 + _Val100) * 2
                ElseIf _Str = "2" Then
                    _NetSum = (_Val25 + _Val50 + _Val75 + _Val100) * 1
                End If
                '_Sum = (_cal + _Val) / _colDivi

                'If _Sum >= 0 And _Sum < 25 Then
                '    _NetSum = 0
                'ElseIf _Sum >= 25 And _Sum <= 49 Then
                '    _NetSum = 25
                'ElseIf _Sum >= 50 And _Sum <= 74 Then
                '    _NetSum = 50
                'ElseIf _Sum >= 75 And _Sum <= 99 Then
                '    _NetSum = 75
                'Else
                '    _NetSum = 100
                'End If
                .SetRowCellValue(.FocusedRowHandle, "FNSum" & _Str, _NetSum)
                For Each R As DataRow In dtHead.Rows
                    For i As Integer = .Columns.Count - 1 To 0 Step -1
                        'If Microsoft.VisualBasic.Right(.Columns(i).FieldName.ToString, 1) = R!FNListIndex.ToString And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FNSum" And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FTSum" Then
                        If .Columns(i).FieldName.ToString = "FNSum" & R!FNListIndex.ToString Then
                            'If .Columns(i).Name.ToString <> _colSkip Then
                            _SumAll += .GetRowCellValue(.FocusedRowHandle, .Columns(i).FieldName.ToString)
                            'End If
                        End If
                        '_colDiviAll += 1
                        'End If
                    Next
                Next
                '_NetAll = (_SumAll + _cal) / _colDiviAll
                If _SumAll = 0 Then
                    .SetRowCellValue(.FocusedRowHandle, "cFNExcellence", "0")
                    .SetRowCellValue(.FocusedRowHandle, "cFNGood", "0")
                    .SetRowCellValue(.FocusedRowHandle, "cFNFair", "0")
                    .SetRowCellValue(.FocusedRowHandle, "cFNBeginner", "0")
                Else
                    _NetAll = (_SumAll / 148) * 100

                    If _NetAll >= 76 And _NetAll <= 100 Then
                        .SetRowCellValue(.FocusedRowHandle, "cFNExcellence", "1")
                        .SetRowCellValue(.FocusedRowHandle, "cFNGood", "0")
                        .SetRowCellValue(.FocusedRowHandle, "cFNFair", "0")
                        .SetRowCellValue(.FocusedRowHandle, "cFNBeginner", "0")
                    ElseIf _NetAll >= 51 And _NetAll <= 75 Then
                        .SetRowCellValue(.FocusedRowHandle, "cFNGood", "1")
                        .SetRowCellValue(.FocusedRowHandle, "cFNExcellence", "0")
                        .SetRowCellValue(.FocusedRowHandle, "cFNFair", "0")
                        .SetRowCellValue(.FocusedRowHandle, "cFNBeginner", "0")
                    ElseIf _NetAll >= 26 And _NetAll <= 50 Then
                        .SetRowCellValue(.FocusedRowHandle, "cFNFair", "1")
                        .SetRowCellValue(.FocusedRowHandle, "cFNGood", "0")
                        .SetRowCellValue(.FocusedRowHandle, "cFNExcellence", "0")
                        .SetRowCellValue(.FocusedRowHandle, "cFNBeginner", "0")
                    ElseIf _NetAll >= 0 And _NetAll <= 25 Then
                        .SetRowCellValue(.FocusedRowHandle, "cFNBeginner", "1")
                        .SetRowCellValue(.FocusedRowHandle, "cFNFair", "0")
                        .SetRowCellValue(.FocusedRowHandle, "cFNGood", "0")
                        .SetRowCellValue(.FocusedRowHandle, "cFNExcellence", "0")
                    End If
                End If


                'For Each RR As DataRow In dtHead.Rows
                '    For i As Integer = .Columns.Count - 1 To 0 Step -1
                '        If Microsoft.VisualBasic.Right(.Columns(i).FieldName.ToString, 1) = RR!FNListIndex.ToString And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FNSum" And (Microsoft.VisualBasic.Left(.Columns(i).FieldName.ToString, 5)) <> "FTSum" Then
                '            _ChkValCount = .GetRowCellValue(.FocusedRowHandle, .Columns(i).FieldName.ToString)
                '            If _ChkValCount = 25 Or _cal = 25 Then
                '                _Val25 += 1
                '            ElseIf _ChkValCount = 50 Or _cal = 50 Then
                '                _Val50 += 1
                '            ElseIf _ChkValCount = 75 Or _cal = 75 Then
                '                _Val75 += 1
                '            ElseIf _ChkValCount = 100 Or _cal = 100 Then
                '                _Val100 += 1
                '            End If
                '        End If
                '    Next
                'Next
                '.SetRowCellValue(.FocusedRowHandle, "FTSummerySkillEMP25s", _Val25)
                '.SetRowCellValue(.FocusedRowHandle, "FTSummerySkillEMP50s", _Val50)
                '.SetRowCellValue(.FocusedRowHandle, "FNSummerySkillUP75s", _Val75)
                '.SetRowCellValue(.FocusedRowHandle, "FTSummerySkillEMP100s", _Val100)

            End With
        Catch ex As Exception

        End Try
    End Sub


#Region "SETGridBaned MultiSkill"

    Private Function CreatedataTableMasterMultiSkill() As DataTable
        Dim Qry As String = ""

        Try
            'If _SectType = False Then
            Qry = "SELECT SK.FNSkillMatrix,SK.FTSkillMatrixCode,SK.FTSkillMatrixNameTH,SK.FTSkillMatrixNameEN,sys.FTNameTH,sys.FTNameEN,Sk.FNHSysSkillMatrixId+''+SK.FNSkillMatrix AS FNHSysSkillMatrixId "
            Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMSkillMatrix AS SK LEFT OUtER JOIN"
            Qry &= vbCrLf & "(SELECT FNListIndex,FTNameTH,FTNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData "
            Qry &= vbCrLf & "WHERE FTListName='FNSkillMatrix' ) AS Sys ON SK.FNSkillMatrix=Sys.FNListIndex"

            Qry = "SELECt AA.FTSkillMatrixCode+'_'+AA.FNSkillMatrix as FTSkillMatrixCode,AA.FTSkillMatrixNameTH,AA.FTSkillMatrixNameEN"
            Qry &= vbCrLf & ",AA.FTNameTH,AA.FTNameEN,AA.FNHSysSkillMatrixId+''+AA.FNSkillMatrix AS FNHSysSkillMatrixId,AA.FNSkillMatrix"
            Qry &= vbCrLf & "FROM"
            Qry &= vbCrLf & "(SELECT SK.FTSkillMatrixCode,SK.FTSkillMatrixNameTH,SK.FTSkillMatrixNameEN,sys.FTNameTH,sys.FTNameEN"
            Qry &= vbCrLf & ",convert(varchar(13),Sk.FNHSysSkillMatrixId) AS FNHSysSkillMatrixId"
            Qry &= vbCrLf & ",convert(varchar(2),SK.FNSkillMatrix) AS FNSkillMatrix"
            Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMSkillMatrix AS SK LEFT OUtER JOIN"
            Qry &= vbCrLf & "(SELECT FNListIndex,FTNameTH,FTNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData "
            Qry &= vbCrLf & "WHERE FTListName='FNSkillMatrix' ) AS Sys ON SK.FNSkillMatrix=Sys.FNListIndex LEFT OUTER JOIN"
            Qry &= vbCrLf & "(SELECT FNListIndex,FTNameTH,FTNameEN FROM HITECH_SYSTEM.dbo.HSysListData WHERE FTListName = 'FNSectType') AS L  ON SK.FNSectType = L.FNListIndex "
            Qry &= vbCrLf & "WHERE SK.FTStateActive='1'"
            Qry &= vbCrLf & "" & _Qry & ""
            Qry &= vbCrLf & ") AS AA"


            'Else
            'Qry = "SELECt AA.FTSkillMatrixCode+'_'+AA.FNSkillMatrix as FTSkillMatrixCode,AA.FTSkillMatrixNameTH,AA.FTSkillMatrixNameEN"
            'Qry &= vbCrLf & ",AA.FTNameTH,AA.FTNameEN,AA.FNHSysSkillMatrixId+''+AA.FNSkillMatrix AS FNHSysSkillMatrixId,AA.FNSkillMatrix"
            'Qry &= vbCrLf & "FROM"
            ''Qry &= vbCrLf & "(SELECT FNSectType FROM HITECH_HR.dbo.THRMTrain WHERE FTTrainCode = '" & FTTrainCode.Text & "') AS T INNER JOIN"
            'Qry &= vbCrLf & "(SELECT FNSectType FROM HITECH_HR.dbo.THRMTrain WHERE FTTrainCode <> '') AS T INNER JOIN"
            'Qry &= vbCrLf & "(SELECT SK.FTSkillMatrixCode,SK.FTSkillMatrixNameTH,SK.FTSkillMatrixNameEN,sys.FTNameTH,sys.FTNameEN,SK.FNSectType"
            'Qry &= vbCrLf & ",convert(varchar(13),Sk.FNHSysSkillMatrixId) AS FNHSysSkillMatrixId"
            'Qry &= vbCrLf & ",convert(varchar(2),SK.FNSkillMatrix) AS FNSkillMatrix"
            'Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMSkillMatrix AS SK LEFT OUtER JOIN"
            'Qry &= vbCrLf & "(SELECT FNListIndex,FTNameTH,FTNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData "
            'Qry &= vbCrLf & "WHERE FTListName='FNSkillMatrix' ) AS Sys ON SK.FNSkillMatrix=Sys.FNListIndex "
            'Qry &= vbCrLf & "WHERE SK.FTStateActive='1') AS AA ON T.FNSectType = AA.FNSectType"
            'End If
            _SectType = False
            Return HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MASTER)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'Private Function CreatedataTableDistinctMasMulti()
    '    Dim Qry As String = ""

    '    Try
    '        Qry = "SELECT distinct SK.FTSkillMatrixCode,SK.FTSkillMatrixNameTH,SK.FTSkillMatrixNameEN,sys.FTNameTH,sys.FTNameEN,Sk.FNHSysSkillMatrixId "
    '        Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMSkillMatrix AS SK LEFT OUtER JOIN"
    '        Qry &= vbCrLf & "(SELECT FNListIndex,FTNameTH,FTNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData "
    '        Qry &= vbCrLf & "WHERE FTListName='FNSkillMatrix' ) AS Sys ON SK.FNSkillMatrix=Sys.FNListIndex"
    '        Return HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MASTER)
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

    Private Function CreatedataTableMasterHeadgb() As DataTable
        Dim Qry As String = ""

        Try
            Qry = "SELECT distinct sys.FTNameTH,sys.FTNameEN, sys.FNListIndex "
            Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..THRMSkillMatrix AS SK LEFT OUtER JOIN"
            Qry &= vbCrLf & "(SELECT FNListIndex,FTNameTH,FTNameEN FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData "
            Qry &= vbCrLf & "WHERE FTListName='FNSkillMatrix' ) AS Sys ON SK.FNSkillMatrix=Sys.FNListIndex"
            Qry &= vbCrLf & "order by FNListIndex asc"
            Return HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MASTER)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function CreatedatatableRanking() As DataTable
        Dim Qry As String = ""
        Try
            Qry = "SELECT FTListName, FNListIndex, FTNameTH, FTNameEN"
            Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData WHERE FTListName='FNRankingMultiSkill'"
            Return HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_SYSTEM)
        Catch ex As Exception
            Return Nothing
        End Try


    End Function



    Private Sub GenGridBanedMultiSkill(ByVal _dtMaster As DataTable, ByVal _dtHead As DataTable, _dtRank As DataTable)
        'Dim _dtMaster As DataTable = CreatedataTableMasterMultiSkill()
        'Dim _dtHead As DataTable = CreatedataTableMasterHeadgb()
        Dim _Str As String = "FNHSysEmpID|FTEmpName|FTDocNo|FTYearWork|FTLeaveFlowPlan|FTLeaveOutFlowPlan|FTAbsent|FTLate|FTSumWorkStop"
        Dim StageCreateBaned1 As Boolean = False
        Dim StgGbHead As Boolean = False
        Dim _countSummary As Integer = 0
        Dim _TextckSum As String = ""

        Call CreateColgbMultiSkill(_dtMaster, _dtHead, _dtRank)
        Dim val As Integer = 0
        Try
            With ogbvSkill
                For i As Integer = .Bands.Count - 1 To 0 Step -1
                    .Bands.RemoveAt(i)
                Next

                For Each Str As String In _Str.Split("|")
                    Dim gBaned As New GridBand
                    With gBaned
                        Select Case Str.ToString
                            Case "FNHSysEmpID"
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .Caption = Str
                                .Columns.Add(ogbvSkill.Columns.ColumnByFieldName(Str))
                                .Name = ogbvSkill.Name.ToString & "gb" & Str
                                .RowCount = 2
                                .Visible = False
                            Case Else
                                .AppearanceHeader.Options.UseTextOptions = True
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .Caption = Str
                                .Columns.Add(ogbvSkill.Columns.ColumnByFieldName(Str))
                                .Name = ogbvSkill.Name.ToString & "gb" & Str
                                .RowCount = 2
                                .Visible = True
                        End Select
                    End With
                    .Bands.Add(gBaned)
                Next

                If Not (_dtHead Is Nothing) Then
                    For Each R As DataRow In _dtHead.Rows
                        Dim gBanedHead As New GridBand
                        'If StgGbHead = False Then
                        With gBanedHead
                            With .AppearanceHeader
                                .Options.UseTextOptions = True
                                .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            End With
                            .Name = "ColHead" & R!FNListIndex.ToString
                            If ST.Lang.Language = ST.Lang.eLang.TH Then
                                .Caption = R!FTNameTH.ToString
                            Else
                                .Caption = R!FTNameEN.ToString
                            End If
                            .RowCount = 1
                            .Visible = True
                        End With
                        .Bands.Add(gBanedHead)
                        'End If
                        If Not (_dtMaster Is Nothing) Then
                            For Each RowC As DataRow In _dtMaster.Select("FTNameEN='" & R!FTNameEN.ToString & "'")
                                Dim gBanedChil As New GridBand
                                With gBanedChil
                                    With .AppearanceHeader
                                        .Options.UseTextOptions = True
                                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    End With
                                    '.Name = "C" & RowC!FTSkillMatrixCode.ToString
                                    If ST.Lang.Language = ST.Lang.eLang.TH Then
                                        Dim QryTH As String = ""
                                        Try
                                            QryTH = "Delete [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) & "]..HSysLanguage WHERE FTFormName='" & Me.Name & "' AND FTObjectName='" & "CTH" & RowC!FTSkillMatrixCode.ToString & "'"
                                            HI.Conn.SQLConn.ExecuteNonQuery(QryTH, Conn.DB.DataBaseName.DB_LANG)
                                        Catch ex As Exception
                                        End Try
                                        .Name = "CTH" & RowC!FTSkillMatrixCode.ToString
                                        .Caption = RowC!FTSkillMatrixNameTH.ToString
                                    Else
                                        Dim QryEN As String = ""
                                        Try
                                            QryEN = "Delete [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LANG) & "]..HSysLanguage WHERE FTFormName='" & Me.Name & "' AND FTObjectName='" & "CEN" & RowC!FTSkillMatrixCode.ToString & "'"
                                            HI.Conn.SQLConn.ExecuteNonQuery(QryEN, Conn.DB.DataBaseName.DB_LANG)
                                        Catch ex As Exception
                                        End Try
                                        .Name = "CEN" & RowC!FTSkillMatrixCode.ToString
                                        .Caption = RowC!FTSkillMatrixNameEN.ToString
                                    End If
                                    .Columns.Add(ogbvSkill.Columns.ColumnByFieldName(RowC!FNHSysSkillMatrixId.ToString))
                                    .RowCount = 1
                                    .Visible = True
                                End With
                                _TextckSum = R!FTNameEN.ToString
                                gBanedHead.Children.Add(gBanedChil)
                            Next

                        End If
                        If _TextckSum <> "Other Skill" Then
                            Dim gBanedChilSum As New GridBand
                            With gBanedChilSum
                                With .AppearanceHeader
                                    .Options.UseTextOptions = True
                                    .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                End With
                                .Name = "CSum" & _countSummary.ToString
                                .Caption = "Summary"
                                .Columns.Add(ogbvSkill.Columns.ColumnByFieldName("FNSum" & _countSummary.ToString))
                                .RowCount = 1
                                .Visible = True
                            End With
                            gBanedHead.Children.Add(gBanedChilSum)
                            _countSummary += 1
                        End If
                    Next

                    If Not (_dtRank Is Nothing) Then
                        Dim gbRankHead As New GridBand
                        With gbRankHead
                            With .AppearanceHeader
                                .Options.UseTextOptions = True
                                .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            End With
                            .Name = "RankHead"
                            .Caption = "Ranking"
                            .RowCount = 1
                            .Visible = True
                        End With
                        .Bands.Add(gbRankHead)
                        For Each K As DataRow In _dtRank.Rows
                            Dim gbRank As New GridBand
                            With gbRank
                                With .AppearanceHeader
                                    .Options.UseTextOptions = True
                                    .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                End With
                                .Name = "gb" & K!FTNameEN.ToString
                                .Caption = "FN" & K!FTNameEN.ToString
                                .Columns.Add(ogbvSkill.Columns.ColumnByFieldName("cFN" & K!FTNameEN.ToString))
                                .RowCount = 1
                                .Visible = True
                            End With
                            gbRankHead.Children.Add(gbRank)
                        Next
                    End If

                    Dim gbSum75 As New GridBand
                    With gbSum75
                        With .AppearanceHeader
                            .Options.UseTextOptions = True
                            .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            .TextOptions.WordWrap = True
                        End With
                        .Name = "gbFNSummerySkillUP75"
                        .Caption = "สรุปความสามารถของพนักงาน ที่ 75%"
                        .Columns.Add(ogbvSkill.Columns.ColumnByFieldName("FNSummerySkillUP75s"))
                        .RowCount = 2
                        .Visible = True
                    End With
                    .Bands.Add(gbSum75)

                    Dim gbLastCol As New GridBand
                    With gbLastCol
                        With .AppearanceHeader
                            .Options.UseTextOptions = True
                            .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        End With
                        .Name = "aggregate"
                        .Caption = "สรุปจำนวนทักษะของพนักงาน"
                        .RowCount = 1
                    End With
                    .Bands.Add(gbLastCol)
                    For i As Integer = 1 To 3 Step 1
                        Dim gbLastCol1 As New GridBand
                        With gbLastCol1
                            With .AppearanceHeader
                                .Options.UseTextOptions = True
                                .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            End With
                            If i = 1 Then
                                .Name = "FTSummerySkillEMP25"
                                .Caption = "25%"
                                .Columns.Add(ogbvSkill.Columns.ColumnByFieldName("FTSummerySkillEMP25s"))
                            ElseIf i = 2 Then
                                .Name = "FTSummerySkillEMP50"
                                .Caption = "50%"
                                .Columns.Add(ogbvSkill.Columns.ColumnByFieldName("FTSummerySkillEMP50s"))
                            ElseIf i = 3 Then
                                .Name = "FTSummerySkillEMP100"
                                .Caption = "100%"
                                .Columns.Add(ogbvSkill.Columns.ColumnByFieldName("FTSummerySkillEMP100s"))
                            End If
                            .RowCount = 1
                            .Visible = True
                        End With
                        gbLastCol.Children.Add(gbLastCol1)
                    Next
                End If
            End With
            Dim oSysLang As New HI.ST.SysLanguage
            Call oSysLang.InsertObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.ToString, Me)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.ToString.Trim, Me)
        Catch ex As Exception

        End Try
    End Sub



#End Region
    Private Sub CreateColgbMultiSkill(ByVal dtCol As DataTable, dtSum As DataTable, dt As DataTable)
        'Dim dtCol As DataTable = CreatedataTableMasterMultiSkill()
        'Dim dtSum As DataTable = CreatedataTableMasterHeadgb()
        _Gencol = True
        Dim _coli As Integer = 0
        Dim i As Integer = 0
        _ColCountMulti = 0
        Try
            With ogbvSkill
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FNHSysEmpID"
                    .FieldName = "FNHSysEmpID"
                    .Caption = "FNHSysEmpID"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    _coli += 1
                    '_ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTEmpName"
                    .FieldName = "FTEmpName"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 250
                    _coli += 1
                    '_ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTDocNo"
                    .FieldName = "FTDocNo"
                    .Visible = True
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 250
                    _coli += 1
                    '_ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTYearWork"
                    .Caption = "FTYearWork"
                    .FieldName = "FTYearWork"
                    .Visible = True
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    '.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    '.DisplayFormat.FormatString = "HH:mm"
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    '_ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTLeaveFlowPlan"
                    .Caption = "FTLeaveFlowPlan"
                    .FieldName = "FTLeaveFlowPlan"
                    .Visible = True
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    .DisplayFormat.FormatString = "HH:mm"
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    '_ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTLeaveOutFlowPlan"
                    .Caption = "FTLeaveOutFlowPlan"
                    .FieldName = "FTLeaveOutFlowPlan"
                    .Visible = True
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    .DisplayFormat.FormatString = "HH:mm"
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    '_ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTAbsent"
                    .Caption = "FTAbsent"
                    .FieldName = "FTAbsent"
                    .Visible = True
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    .DisplayFormat.FormatString = "HH:mm"
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    '_ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTLate"
                    .Caption = "FTLate"
                    .FieldName = "FTLate"
                    .Visible = True
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    .DisplayFormat.FormatString = "HH:mm"
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    '_ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTSumWorkStop"
                    .Caption = "FTSumWorkStop"
                    .FieldName = "FTSumWorkStop"
                    .Visible = True
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    .DisplayFormat.FormatString = "HH:mm"
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    '_ColCountMulti += 1
                End With



                For Each V As DataRow In dtSum.Rows
                    For Each R As DataRow In dtCol.Select("FTNameEN='" & V!FTNameEN.ToString & "'")
                        'Dim _FieldNameStr = R!FNHSysSkillMatrixId.ToString & R!FNSkillMatrix.ToString
                        .Columns.Add()
                        With .Columns(_coli)
                            .AppearanceCell.BackColor = Color.LightCyan
                            .Name = "FN" & R!FTSkillMatrixCode.ToString
                            .Caption = "FN" & R!FTSkillMatrixCode.ToString
                            .FieldName = R!FNHSysSkillMatrixId
                            'R!FNHSysSkillMatrixId.ToString
                            '& R!FNSkillMatrix.ToString
                            .Visible = True
                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .DisplayFormat.FormatString = "0:n0"
                            With .OptionsColumn
                                .AllowEdit = True
                                .AllowMove = False
                                .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                .ReadOnly = False
                            End With
                            .Width = 75
                            Dim Rep As RepositoryItemCalcEdit = New RepositoryItemCalcEdit
                            Rep.Name = "Rep" & .FieldName.ToString
                            Rep.AllowMouseWheel = False
                            .ColumnEdit = Rep
                            Rep.Buttons(0).Visible = False
                            Rep.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            Rep.DisplayFormat.FormatString = "0:n0"
                            Rep.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
                            Rep.Tag = "9"
                            With Rep
                                AddHandler .EditValueChanged, AddressOf RepChanging
                            End With
                            _coli += 1
                            _ColCountMulti += 1
                        End With
                    Next
                    If V!FTNameEN.ToString <> "Other Skill" Then
                        .Columns.Add()
                        With .Columns(_coli)
                            .Name = "FNSum" & V!FNListIndex.ToString
                            .Caption = "FNSum" & V!FNListIndex.ToString
                            .FieldName = "FNSum" & V!FNListIndex.ToString
                            .Visible = True
                            With .AppearanceCell
                                .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                            End With
                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .DisplayFormat.FormatString = "0:n0"
                            With .OptionsColumn
                                .AllowEdit = False
                                .AllowMove = False
                                .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                .ReadOnly = True
                            End With
                            .Width = 75
                            _coli += 1
                            _ColCountMulti += 1
                        End With
                    End If
                Next

                'For Each Row As DataRow In dtSum.Rows
                '    .Columns.Add()
                '    With .Columns(_coli)
                '        .Name = "FTSum" & Row!FNListIndex.ToString
                '        .Caption = "FTSum" & Row!FNListIndex.ToString
                '        .FieldName = "FTSum" & Row!FNListIndex.ToString
                '        .Visible = True
                '        .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                '        .DisplayFormat.FormatString = "0:n0"
                '        With .OptionsColumn
                '            .AllowEdit = False
                '            .AllowMove = False
                '            .AllowSort = DevExpress.Utils.DefaultBoolean.False
                '            .ReadOnly = True
                '        End With
                '        .Width = 75
                '        _coli += 1
                '        _ColCountMulti += 1
                '    End With
                'Next

                For Each Rs As DataRow In dt.Rows
                    .Columns.Add()
                    With .Columns(_coli)
                        With .AppearanceCell
                            If Rs!FNListIndex.ToString = "0" Then
                                .BackColor = Color.Gold
                            ElseIf Rs!FNListIndex.ToString = "1" Then
                                .BackColor = Color.Silver
                            ElseIf Rs!FNListIndex.ToString = "2" Then
                                .BackColor = Color.DarkRed
                                .ForeColor = Color.White
                            End If
                        End With
                        .Name = "cFN" & Rs!FTNameEN.ToString
                        If ST.Lang.Language = ST.Lang.eLang.TH Then
                            .Caption = Rs!FTNameTH.ToString
                        Else
                            .Caption = Rs!FTNameEN.ToString
                        End If
                        .FieldName = "cFN" & Rs!FTNameEN.ToString
                        .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        .DisplayFormat.FormatString = "0:n0"
                        With .OptionsColumn
                            .AllowEdit = False
                            .AllowMove = False
                            .AllowSort = DevExpress.Utils.DefaultBoolean.False
                            .ReadOnly = True
                        End With
                        .Visible = True
                        .Width = 75
                        Dim Rep As New RepositoryItemCheckEdit
                        Rep.Name = "Rep" & .FieldName.ToString
                        .ColumnEdit = Rep
                        'Rep.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
                        Rep.Caption = "Check"
                        Rep.ValueChecked = "1"
                        Rep.ValueUnchecked = "0"
                        _coli += 1
                        _ColCountMulti += 1
                    End With
                Next

                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FNSummerySkillUP75s"
                    .Caption = "FNSummerySkillUP75s"
                    .FieldName = "FNSummerySkillUP75s"
                    .Visible = True
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    .DisplayFormat.FormatString = "0:n0"
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    _ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTSummerySkillEMP25s"
                    .Caption = "FTSummerySkillEMP25s"
                    .FieldName = "FTSummerySkillEMP25s"
                    .Visible = True
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    .DisplayFormat.FormatString = "0:n0"
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    _ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTSummerySkillEMP50s"
                    .Caption = "FTSummerySkillEMP50s"
                    .FieldName = "FTSummerySkillEMP50s"
                    .Visible = True
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    .DisplayFormat.FormatString = "0:n0"
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    _ColCountMulti += 1
                End With
                .Columns.Add()
                With .Columns(_coli)
                    .Name = "FTSummerySkillEMP100s"
                    .Caption = "FTSummerySkillEMP100s"
                    .FieldName = "FTSummerySkillEMP100s"
                    .Visible = True
                    With .AppearanceCell
                        .TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    End With
                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    .DisplayFormat.FormatString = "0:n0"
                    With .OptionsColumn
                        .AllowEdit = False
                        .AllowMove = False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .ReadOnly = True
                    End With
                    .Width = 75
                    _coli += 1
                    _ColCountMulti += 1
                End With
            End With
        Catch ex As Exception

        End Try

    End Sub
End Class