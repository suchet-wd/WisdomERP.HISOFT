Imports System.Data

Public Class wAdjustIncentive_bak

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If (VerrifyData()) Then
                Call LoadData()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function VerrifyData() As Boolean
        Try
            If Me.FNHSysUnitSectId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysUnitSectId_lbl.Text)
                Me.FNHSysUnitSectId.Focus()
                Return False
            End If
            If Me.FNHSysUnitSectIdTo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysUnitSectIdTo_lbl.Text)
                Me.FNHSysUnitSectIdTo.Focus()
                Return False
            End If
            If Me.FDDTrans.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FDDTrans_lbl.Text)
                Me.FDDTrans.Focus()
                Return False
            End If
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub LoadData()
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT  '0' AS FTSelect ,   S.FNHSysUnitSectId, S.FTUnitSectCode, S.FTStateActive,   I.FNNomalIncentive, I.FNOTIncentive"
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", S.FTUnitSectNameTH as FTUnitSectName"
            Else
                _Cmd &= vbCrLf & ", S.FTUnitSectNameEN as FTUnitSectName"
            End If
            _Cmd &= vbCrLf & ",CASE WHEN Isdate(I.FDDateTrans) = 1 Then convert(varchar(10),convert(datetime,I.FDDateTrans),103) Else '' END AS FDDateTrans "
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS S WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "(Select * From "
            _Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPRODIncentiveBU   WITH (NOLOCK) "
            _Cmd &= vbCrLf & "WHERE  (ISNULL(FDDateTrans,'') = '" & HI.UL.ULDate.ConvertEnDB(Me.FDDTrans.Text) & "' OR ISNULL(FDDateTrans,'') = '') ) AS I "
            _Cmd &= vbCrLf & " ON S.FNHSysUnitSectId = I.FNHSysUnitSectId"
            _Cmd &= vbCrLf & "WHERE S.FTStateActive = '1' and S.FNHSysUnitSectId <> 0"
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Cmd &= vbCrLf & "And S.FTUnitSectCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "'"
            End If
            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Cmd &= vbCrLf & "AND S.FTUnitSectCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "'"
            End If
            'If Me.FDDTrans.Text <> "" Then
            '    _Cmd &= vbCrLf & "AND (ISNULL(I.FDDateTrans,'') = '" & HI.UL.ULDate.ConvertEnDB(Me.FDDTrans.Text) & "' OR ISNULL(I.FDDateTrans,'') = '')"
            'End If
            Me.ogcDetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR)
        Catch ex As Exception
        End Try
    End Sub

    Private Function SaveData() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            With CType(Me.ogcDetail.DataSource, DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With
            For Each R As DataRow In _oDt.Select("FTSelect='1'")
                _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPRODIncentiveBU"
                _Cmd &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",FNNomalIncentive=" & Me.FNIncentive.Value
                _Cmd &= vbCrLf & ",FNOTIncentive=" & Me.FNIncentiveOT.Value
                _Cmd &= vbCrLf & "WHERE FNHSysUnitSectId=" & Integer.Parse(R!FNHSysUnitSectId.ToString)
                _Cmd &= vbCrLf & "And FDDateTrans='" & HI.UL.ULDate.ConvertEnDB(Me.FDDTrans.Text) & "'"
                If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_HR) = False Then
                    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPRODIncentiveBU (FTInsUser, FDInsDate, FTInsTime, FNHSysUnitSectId, FDDateTrans, FNNomalIncentive, FNOTIncentive)"
                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & "," & Integer.Parse(R!FNHSysUnitSectId.ToString)
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FDDTrans.Text) & "'"
                    _Cmd &= vbCrLf & "," & Me.FNIncentive.Value
                    _Cmd &= vbCrLf & "," & Me.FNIncentiveOT.Value
                    If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_HR) = False Then
                        Return False
                    End If
                End If
                Call AvgWageIncentive(Me.FDDTrans.Text, Integer.Parse(R!FNHSysUnitSectId.ToString))
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If (VerrifyData()) Then
                If SaveData() Then
                    If Me.FNIncentive.Value <= 0 Then
                        Me.FNIncentive.Focus()
                        Exit Sub
                    End If

                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Call LoadData()
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Call LoadData()

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If VerrifyData() Then
                If DeleteData() Then
                    Call LoadData()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function DeleteData() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            With CType(Me.ogcDetail.DataSource, DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With
            For Each R As DataRow In _oDt.Select("FTSelect='1'")
                _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPRODIncentiveBU"
                _Cmd &= vbCrLf & "WHERE FNHSysUnitSectId=" & Integer.Parse(R!FNHSysUnitSectId.ToString)
                _Cmd &= vbCrLf & "And FDDateTrans='" & HI.UL.ULDate.ConvertEnDB(R!FDDateTrans.ToString) & "'"
                HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_HR)
                Call DeleteWageIncentive(Me.FDDTrans.Text, Integer.Parse(R!FNHSysUnitSectId.ToString))
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub wAdjustIncentive_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
            Me.FNHSysCmpId.Properties.Tag = HI.ST.SysInfo.CmpID
        Catch ex As Exception
        End Try
    End Sub


    Private Function AvgWageIncentive(ByVal _DateTrans As String, ByVal _UnitSectId As Integer) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _IncentivePerMin As Double = 0
            Dim _IncentiveOTPerMin As Double = 0
            _IncentivePerMin = (Me.FNIncentive.Value + Me.FNIncentiveOT.Value) / (GetSumWorkingTime(_DateTrans, _UnitSectId) + GetSumWorkingTimeOT(_DateTrans, _UnitSectId))
            '_IncentiveOTPerMin = Me.FNIncentiveOT.Value / GetSumWorkingTimeOT(_DateTrans, _UnitSectId)
            _Cmd = "Select   FNHSysEmpID, FTDateTrans, SUM(FNTimeOT) AS FNTimeOT,SUM(FNTimeOTNormal) AS FNTimeOTNormal ,  FNHSysUnitSectId,FNHSysEmpTypeId, SUM(FNTimeMinNormal) AS FNTimeMinNormal, SUM(FNTimeMin) AS FNTimeMin"
            _Cmd &= vbCrLf & "From (SELECT  T.FNHSysEmpID,   T.FTDateTrans,   Isnull(T.FNOT1Min,0) + Isnull(T.FNOT1_5Min,0) + Isnull(T.FNOT2Min,0) + Isnull(T.FNOT3Min,0) + Isnull(T.FNOT4Min,0) AS FNTimeOT"
            _Cmd &= vbCrLf & ",   Isnull(T.FNOT1Min,0) + Isnull(T.FNOT1_5Min,0) + Isnull(T.FNOT2Min,0) + Isnull(T.FNOT3Min,0) + Isnull(T.FNOT4Min,0) AS FNTimeOTNormal"
            _Cmd &= vbCrLf & "	,E.FNHSysEmpTypeId ,   E.FNHSysUnitSectId , (T.FNTimeMin + T.FNLateNormalMin) - T.FNLateNormalCut  AS FNTimeMinNormal ,T.FNTimeMin  "
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) ON T.FNHSysEmpID = E.FNHSysEmpID"
            _Cmd &= vbCrLf & "where FTDateTrans = '" & HI.UL.ULDate.ConvertEnDB(_DateTrans) & "'"
            _Cmd &= vbCrLf & "and ( E.FNHSysUnitSectId =" & Integer.Parse(_UnitSectId) & ")"
            _Cmd &= vbCrLf & "UNION ALL "
            _Cmd &= vbCrLf & " SELECT     M.FNHSysEmpID, M.FDDate   "
            _Cmd &= vbCrLf & "	,Isnull((CASE WHEN  M.FTEndTime >= T.FTIn3 and M.FTEndTime <= T.FTOut3 THEN "
            _Cmd &= vbCrLf & "							CASE WHEN M.FTStartTime >= T.FTIn3 THEN M.FNTotalMinute"
            _Cmd &= vbCrLf & "								 WHEN M.FTStartTime < T.FTIn3  THEN DATEDIFF(MINUTE,CONVERT(datetime,T.FTIn3),CONVERT(datetime,M.FTEndTime))"
            _Cmd &= vbCrLf & "                          End"
            _Cmd &= vbCrLf & "		 END),0) AS FNTimeOTMin	"
            _Cmd &= vbCrLf & ",   Isnull(T.FNOT1Min,0) + Isnull(T.FNOT1_5Min,0) + Isnull(T.FNOT2Min,0) + Isnull(T.FNOT3Min,0) + Isnull(T.FNOT4Min,0) AS FNTimeOTNormal"
            _Cmd &= vbCrLf & " , Case when M.FNHSysEmpTypeId <> 0  Or M.FNHSysEmpTypeId is not null Then  M.FNHSysEmpTypeId Else  T.FNHSysEmpTypeId  END AS FNHSysEmpTypeId, M.FNHSysUnitSectIdTo "
            _Cmd &= vbCrLf & ", (T.FNTimeMin + T.FNLateNormalMin) - T.FNLateNormalCut  AS FNTimeMinNormal"
            _Cmd &= vbCrLf & "   ,ISNULL((CASE WHEN M.FTStartTime >= T.FTIn1 and M.FTStartTime <= T.FTOut1 THEN "
            _Cmd &= vbCrLf & "							CASE WHEN M.FTEndTime <= T.FTOut1 THEN M.FNTotalMinute"
            _Cmd &= vbCrLf & "								 WHEN M.FTEndTime <= T.FTOut2 THEN DATEDIFF(MINUTE,CONVERT(datetime,M.FTStartTime),CONVERT(datetime,T.FTOut1))"
            _Cmd &= vbCrLf & "																	+ DATEDIFF(MINUTE,CONVERT(datetime,T.FTIn2),CONVERT(datetime,M.FTEndTime))"
            _Cmd &= vbCrLf & "								 WHEN M.FTEndTime >= T.FTOut2 THEN DATEDIFF(MINUTE,CONVERT(datetime,M.FTStartTime),CONVERT(datetime,T.FTOut1))"
            _Cmd &= vbCrLf & "																	+ DATEDIFF(MINUTE,CONVERT(datetime,T.FTIn2),CONVERT(datetime,T.FTOut2))  "
            _Cmd &= vbCrLf & "                          End"
            _Cmd &= vbCrLf & "		 WHEN M.FTStartTime >= T.FTIn2 and M.FTStartTime <= T.FTOut2 THEN "
            _Cmd &= vbCrLf & "							CASE WHEN M.FTEndTime <= T.FTOut2 THEN M.FNTotalMinute"
            _Cmd &= vbCrLf & "								 WHEN M.FTEndTime > T.FTOut2  THEN  DATEDIFF(MINUTE,CONVERT(datetime,M.FTStartTime),CONVERT(datetime,T.FTOut2))"
            _Cmd &= vbCrLf & "                          End"
            _Cmd &= vbCrLf & "	 END),0) AS FNTimeMin"
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS M WITH(NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH(NOLOCK)  ON M.FNHSysEmpID = T.FNHSysEmpID AND M.FDDate = T.FTDateTrans"
            _Cmd &= vbCrLf & "WHERE     (M.FNHSysUnitSectIdTo = " & Integer.Parse(_UnitSectId) & ") AND (M.FDDate = '" & HI.UL.ULDate.ConvertEnDB(_DateTrans) & "')"
            _Cmd &= vbCrLf & "UNION ALL "
            _Cmd &= vbCrLf & " SELECT     M.FNHSysEmpID, M.FDDate   "
            _Cmd &= vbCrLf & "	,-Isnull((CASE WHEN  M.FTEndTime >= T.FTIn3 and M.FTEndTime <= T.FTOut3 THEN "
            _Cmd &= vbCrLf & "							CASE WHEN M.FTStartTime >= T.FTIn3 THEN M.FNTotalMinute"
            _Cmd &= vbCrLf & "								 WHEN M.FTStartTime < T.FTIn3  THEN DATEDIFF(MINUTE,CONVERT(datetime,T.FTIn3),CONVERT(datetime,M.FTEndTime))"
            _Cmd &= vbCrLf & "                          End"
            _Cmd &= vbCrLf & "		 END),0) AS FNTimeOTMin	"
            _Cmd &= vbCrLf & ",   0 AS FNTimeOTNormal"
            _Cmd &= vbCrLf & " , Case when M.FNHSysEmpTypeId <> 0  Or M.FNHSysEmpTypeId is not null Then  M.FNHSysEmpTypeId Else  T.FNHSysEmpTypeId  END AS FNHSysEmpTypeId , M.FNHSysUnitSectId "
            _Cmd &= vbCrLf & ", 0  AS FNTimeMinNormal" ''--((T.FNTimeMin + T.FNLateNormalMin) - T.FNLateNormalCut) 
            _Cmd &= vbCrLf & "   ,-ISNULL((CASE WHEN M.FTStartTime >= T.FTIn1 and M.FTStartTime <= T.FTOut1 THEN "
            _Cmd &= vbCrLf & "							CASE WHEN M.FTEndTime <= T.FTOut1 THEN M.FNTotalMinute"
            _Cmd &= vbCrLf & "								 WHEN M.FTEndTime <= T.FTOut2 THEN DATEDIFF(MINUTE,CONVERT(datetime,M.FTStartTime),CONVERT(datetime,T.FTOut1))"
            _Cmd &= vbCrLf & "																	+ DATEDIFF(MINUTE,CONVERT(datetime,T.FTIn2),CONVERT(datetime,M.FTEndTime))"
            _Cmd &= vbCrLf & "								 WHEN M.FTEndTime >= T.FTOut2 THEN DATEDIFF(MINUTE,CONVERT(datetime,M.FTStartTime),CONVERT(datetime,T.FTOut1))"
            _Cmd &= vbCrLf & "																	+ DATEDIFF(MINUTE,CONVERT(datetime,T.FTIn2),CONVERT(datetime,T.FTOut2))  "
            _Cmd &= vbCrLf & "                          End"
            _Cmd &= vbCrLf & "		 WHEN M.FTStartTime >= T.FTIn2 and M.FTStartTime <= T.FTOut2 THEN "
            _Cmd &= vbCrLf & "							CASE WHEN M.FTEndTime <= T.FTOut2 THEN M.FNTotalMinute"
            _Cmd &= vbCrLf & "								 WHEN M.FTEndTime > T.FTOut2  THEN  DATEDIFF(MINUTE,CONVERT(datetime,M.FTStartTime),CONVERT(datetime,T.FTOut2))"
            _Cmd &= vbCrLf & "                          End"
            _Cmd &= vbCrLf & "	 END),0) AS FNTimeMin"
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS M WITH(NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH(NOLOCK)  ON M.FNHSysEmpID = T.FNHSysEmpID AND M.FDDate = T.FTDateTrans"
            _Cmd &= vbCrLf & "WHERE     (M.FNHSysUnitSectId = " & Integer.Parse(_UnitSectId) & ") AND (M.FDDate = '" & HI.UL.ULDate.ConvertEnDB(_DateTrans) & "') ) AS T"
            _Cmd &= vbCrLf & "Group by FNHSysEmpID, FTDateTrans,  FNHSysUnitSectId,FNHSysEmpTypeId"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR)




            For Each R As DataRow In _oDt.Rows
                Dim _Incentive As Double = _IncentivePerMin * (Double.Parse(R!FNTimeMin.ToString) + Double.Parse(R!FNTimeOT.ToString))
                Dim _SalaryPerMin As Double = GetSalaryPerMin(R!FNHSysEmpID.ToString, R!FNHSysEmpTypeId.ToString)
                Dim _FCBaht As Double = _SalaryPerMin * Double.Parse(R!FNTimeMinNormal.ToString)
                Dim _FCBahtOT15 As Double = (_SalaryPerMin * Double.Parse(R!FNTimeOTNormal.ToString)) * 1.5
                Dim _IncentiveOT As Double = _IncentiveOTPerMin * Double.Parse(R!FNTimeOT.ToString)
                Dim _incentiveMoveTeam As Double = GetIncentiveMoveTeam(Integer.Parse(R!FNHSysEmpID.ToString), _DateTrans)
                '***********************
                _IncentiveOTPerMin = 0
                _IncentiveOT = 0

                _Cmd = "  UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                _Cmd &= vbCrLf & " SET FNProNormal=(" & _Incentive + _FCBaht + _FCBahtOT15 & ")+" & _incentiveMoveTeam
                _Cmd &= vbCrLf & ", FNNetProAmt=" & _IncentiveOT + _Incentive + _FCBaht + _FCBahtOT15 & " + FNProOther"
                _Cmd &= vbCrLf & ",FNProOT=" & (_IncentiveOT)
                _Cmd &= vbCrLf & ",FNAmtNormal=" & _FCBaht
                _Cmd &= vbCrLf & ",FNAmtOT=" & _FCBahtOT15
                _Cmd &= vbCrLf & ",FNNetAmt=" & _FCBaht + _FCBahtOT15
                _Cmd &= vbCrLf & ", FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Cmd &= vbCrLf & ", FTUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Cmd &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Cmd &= vbCrLf & " WHERE  FNHSysEmpID=" & Integer.Parse(R!FNHSysEmpID.ToString) & " AND FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(_DateTrans) & "'  "
                If Not (HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_HR)) Then
                    _Cmd = "  INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                    _Cmd &= vbCrLf & " (FTInsUser, FTInsDate, FTInsTime,  FNHSysEmpID, FTDateTrans, FNAmtNormal, FNAmtOT, FNNetAmt, FNProNormal, FNProOther, FNNetProAmt , FNProOT) "
                    _Cmd &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Cmd &= vbCrLf & " ," & Integer.Parse(R!FNHSysEmpID.ToString) & ",'" & HI.UL.ULDate.ConvertEnDB(_DateTrans) & "'"
                    _Cmd &= vbCrLf & " , " & _FCBaht & ""
                    _Cmd &= vbCrLf & " ," & _FCBahtOT15 & ""
                    _Cmd &= vbCrLf & " ," & _FCBaht + _FCBahtOT15 & ""
                    _Cmd &= vbCrLf & " ," & _Incentive + _FCBaht + _FCBahtOT15 + _incentiveMoveTeam & ",0"
                    _Cmd &= vbCrLf & "," & _IncentiveOT + _Incentive + _FCBaht + _FCBahtOT15 + _incentiveMoveTeam & ""
                    _Cmd &= vbCrLf & ", " & (_IncentiveOT)
                    If Not (HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_HR)) Then
                        Return False
                    End If

                End If
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#Region "Cal Incentive"
    Private Function GetIncentiveMoveTeam(ByVal _EmpId As Integer, ByVal _DateTrans As String) As Double
        Try
            Dim _Cmd As String = "" : Dim _Incentive As Double = 0 : Dim _TotalTime As Double = 0 : Dim _IncentiveByLine As Double = 0 : Dim _IncentivePerMin As Double = 0
            Dim _oDt As DataTable
            _Cmd = " SELECT     M.FNHSysEmpID, M.FDDate   "
            _Cmd &= vbCrLf & "	,Isnull((CASE WHEN  M.FTEndTime >= T.FTIn3 and M.FTEndTime <= T.FTOut3 THEN "
            _Cmd &= vbCrLf & "							CASE WHEN M.FTStartTime >= T.FTIn3 THEN M.FNTotalMinute"
            _Cmd &= vbCrLf & "								 WHEN M.FTStartTime < T.FTIn3  THEN DATEDIFF(MINUTE,CONVERT(datetime,T.FTIn3),CONVERT(datetime,M.FTEndTime))"
            _Cmd &= vbCrLf & "                          End"
            _Cmd &= vbCrLf & "		 END),0) AS FNTimeOTMin	"
            _Cmd &= vbCrLf & ",   Isnull(T.FNOT1Min,0) + Isnull(T.FNOT1_5Min,0) + Isnull(T.FNOT2Min,0) + Isnull(T.FNOT3Min,0) + Isnull(T.FNOT4Min,0) AS FNTimeOTNormal"
            _Cmd &= vbCrLf & " , Case when M.FNHSysEmpTypeId <> 0  Or M.FNHSysEmpTypeId is not null Then  M.FNHSysEmpTypeId Else  T.FNHSysEmpTypeId  END AS FNHSysEmpTypeId, M.FNHSysUnitSectIdTo "
            _Cmd &= vbCrLf & ", (T.FNTimeMin + T.FNLateNormalMin) - T.FNLateNormalCut  AS FNTimeMinNormal"
            _Cmd &= vbCrLf & "   ,ISNULL((CASE WHEN M.FTStartTime >= T.FTIn1 and M.FTStartTime <= T.FTOut1 THEN "
            _Cmd &= vbCrLf & "							CASE WHEN M.FTEndTime <= T.FTOut1 THEN M.FNTotalMinute"
            _Cmd &= vbCrLf & "								 WHEN M.FTEndTime <= T.FTOut2 THEN DATEDIFF(MINUTE,CONVERT(datetime,M.FTStartTime),CONVERT(datetime,T.FTOut1))"
            _Cmd &= vbCrLf & "																	+ DATEDIFF(MINUTE,CONVERT(datetime,T.FTIn2),CONVERT(datetime,M.FTEndTime))"
            _Cmd &= vbCrLf & "								 WHEN M.FTEndTime >= T.FTOut2 THEN DATEDIFF(MINUTE,CONVERT(datetime,M.FTStartTime),CONVERT(datetime,T.FTOut1))"
            _Cmd &= vbCrLf & "																	+ DATEDIFF(MINUTE,CONVERT(datetime,T.FTIn2),CONVERT(datetime,T.FTOut2))  "
            _Cmd &= vbCrLf & "                          End"
            _Cmd &= vbCrLf & "		 WHEN M.FTStartTime >= T.FTIn2 and M.FTStartTime <= T.FTOut2 THEN "
            _Cmd &= vbCrLf & "							CASE WHEN M.FTEndTime <= T.FTOut2 THEN M.FNTotalMinute"
            _Cmd &= vbCrLf & "								 WHEN M.FTEndTime > T.FTOut2  THEN  DATEDIFF(MINUTE,CONVERT(datetime,M.FTStartTime),CONVERT(datetime,T.FTOut2))"
            _Cmd &= vbCrLf & "                          End"
            _Cmd &= vbCrLf & "	 END),0) AS FNTimeMin"
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS M WITH(NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH(NOLOCK)  ON M.FNHSysEmpID = T.FNHSysEmpID AND M.FDDate = T.FTDateTrans"
            _Cmd &= vbCrLf & "WHERE     (M.FDDate = '" & HI.UL.ULDate.ConvertEnDB(_DateTrans) & "')"
            _Cmd &= vbCrLf & "AND M.FNHSysEmpID=" & Integer.Parse(_EmpId)
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR)
            For Each R As DataRow In _oDt.Rows
                _TotalTime = GetSumWorkingTime(_DateTrans, R!FNHSysUnitSectIdTo.ToString) + GetSumWorkingTimeOT(_DateTrans, R!FNHSysUnitSectIdTo.ToString) + Double.Parse(R!FNTimeMin.ToString)
                _IncentiveByLine = GetIncentiveByline(R!FNHSysUnitSectIdTo.ToString, _DateTrans)
                _IncentivePerMin = _IncentiveByLine / _TotalTime
                _Incentive += +(_IncentivePerMin * Double.Parse(R!FNTimeMin.ToString))
            Next
            Return _Incentive
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function GetIncentiveByline(_UnitSectId As Integer, _Date As String) As Double
        Try
            Dim _Cmd As String = ""
            _Cmd = " SELECT  sum(FNNomalIncentive)+ sum(FNOTIncentive) as FNAmtIncentive"
            _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPRODIncentiveBU WITH(NOLOCK) "
            _Cmd &= vbCrLf & " where FNHSysUnitSectId =" & Integer.Parse(_UnitSectId)
            _Cmd &= vbCrLf & " and FDDateTrans = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "'"
            Return Double.Parse(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "0"))
        Catch ex As Exception
            Return 0
        End Try
    End Function
#End Region

    Private Function GetEmpIdMoveCome(ByVal _Date As String, ByVal _UnitSect As Integer) As String
        Try
            Dim _Cmd As String = "" : Dim _Return As String = ""
            Dim _oDt As DataTable
            _Cmd = " SELECT  FNHSysEmpID"
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType WITH(NOLOCK) "
            _Cmd &= vbCrLf & "Where FDDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "'"
            _Cmd &= vbCrLf & "AND FNHSysUnitSectIdTo =" & Integer.Parse(_UnitSect)
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            For Each R As DataRow In _oDt.Rows
                If _Return <> "" Then _Return &= ","
                _Return &= R!FNHSysEmpID.ToString
            Next
            If _Return = "" Then _Return = "0"
            Return _Return
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function GetSumWorkingTime(ByVal _Date As String, ByVal _UnitSect As Integer) As Double
        Try
            Dim _Cmd As String = ""
            Dim _FNTimeNormal As Double = 0 : Dim _FNTimeMoveCome As Double = 0 : Dim _FNTimeMoveTo As Double = 0
            _Cmd = "SELECT    sum(T.FNTimeMin) AS FNTime"
            _Cmd &= vbCrLf & "        FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) ON T.FNHSysEmpID = E.FNHSysEmpID"
            _Cmd &= vbCrLf & " where FTDateTrans = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "'"
            _Cmd &= vbCrLf & " AND E.FNHSysUnitSectId =" & Integer.Parse(_UnitSect)
            _FNTimeNormal = Convert.ToDouble(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "0"))


            _Cmd = "   SELECT    Sum(ISNULL((CASE WHEN M.FTStartTime >= T.FTIn1 and M.FTStartTime <= T.FTOut1 THEN "
            _Cmd &= vbCrLf & "							CASE WHEN M.FTEndTime <= T.FTOut1 THEN M.FNTotalMinute"
            _Cmd &= vbCrLf & "								 WHEN M.FTEndTime <= T.FTOut2 THEN DATEDIFF(MINUTE,CONVERT(datetime,M.FTStartTime),CONVERT(datetime,T.FTOut1))"
            _Cmd &= vbCrLf & "																	+ DATEDIFF(MINUTE,CONVERT(datetime,T.FTIn2),CONVERT(datetime,M.FTEndTime))"
            _Cmd &= vbCrLf & "								 WHEN M.FTEndTime >= T.FTOut2 THEN DATEDIFF(MINUTE,CONVERT(datetime,M.FTStartTime),CONVERT(datetime,T.FTOut1))"
            _Cmd &= vbCrLf & "																	+ DATEDIFF(MINUTE,CONVERT(datetime,T.FTIn2),CONVERT(datetime,T.FTOut2))  "
            _Cmd &= vbCrLf & "   End"
            _Cmd &= vbCrLf & "		 WHEN M.FTStartTime >= T.FTIn2 and M.FTStartTime <= T.FTOut2 THEN "
            _Cmd &= vbCrLf & "							CASE WHEN M.FTEndTime <= T.FTOut2 THEN M.FNTotalMinute"
            _Cmd &= vbCrLf & "								 WHEN M.FTEndTime > T.FTOut2  THEN  DATEDIFF(MINUTE,CONVERT(datetime,M.FTStartTime),CONVERT(datetime,T.FTOut2))"
            _Cmd &= vbCrLf & "    End"
            _Cmd &= vbCrLf & "	 END),0)) AS FNTimeMin"
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS M INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T ON M.FNHSysEmpID = T.FNHSysEmpID AND M.FDDate = T.FTDateTrans"
            _Cmd &= vbCrLf & "WHERE     (M.FNHSysUnitSectIdTo = " & Integer.Parse(_UnitSect) & ") AND (M.FDDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "')"
            _FNTimeMoveCome = Convert.ToDouble(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "0"))


            _Cmd = "   SELECT    Sum(ISNULL((CASE WHEN M.FTStartTime >= T.FTIn1 and M.FTStartTime <= T.FTOut1 THEN "
            _Cmd &= vbCrLf & "							CASE WHEN M.FTEndTime <= T.FTOut1 THEN M.FNTotalMinute"
            _Cmd &= vbCrLf & "								 WHEN M.FTEndTime <= T.FTOut2 THEN DATEDIFF(MINUTE,CONVERT(datetime,M.FTStartTime),CONVERT(datetime,T.FTOut1))"
            _Cmd &= vbCrLf & "																	+ DATEDIFF(MINUTE,CONVERT(datetime,T.FTIn2),CONVERT(datetime,M.FTEndTime))"
            _Cmd &= vbCrLf & "								 WHEN M.FTEndTime >= T.FTOut2 THEN DATEDIFF(MINUTE,CONVERT(datetime,M.FTStartTime),CONVERT(datetime,T.FTOut1))"
            _Cmd &= vbCrLf & "																	+ DATEDIFF(MINUTE,CONVERT(datetime,T.FTIn2),CONVERT(datetime,T.FTOut2))  "
            _Cmd &= vbCrLf & "   End"
            _Cmd &= vbCrLf & "		 WHEN M.FTStartTime >= T.FTIn2 and M.FTStartTime <= T.FTOut2 THEN "
            _Cmd &= vbCrLf & "							CASE WHEN M.FTEndTime <= T.FTOut2 THEN M.FNTotalMinute"
            _Cmd &= vbCrLf & "								 WHEN M.FTEndTime > T.FTOut2  THEN  DATEDIFF(MINUTE,CONVERT(datetime,M.FTStartTime),CONVERT(datetime,T.FTOut2))"
            _Cmd &= vbCrLf & "    End"
            _Cmd &= vbCrLf & "	 END),0)) AS FNTimeMin"
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType AS M INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T ON M.FNHSysEmpID = T.FNHSysEmpID AND M.FDDate = T.FTDateTrans"
            _Cmd &= vbCrLf & "WHERE     (M.FNHSysUnitSectId = " & Integer.Parse(_UnitSect) & ") AND (M.FDDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "')"
            _FNTimeMoveTo = Convert.ToDouble(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "0"))


            Return (_FNTimeNormal + _FNTimeMoveCome) - _FNTimeMoveTo
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function GetSumWorkingTimeOT(ByVal _Date As String, ByVal _UnitSect As Integer) As Double
        Try
            Dim _Cmd As String = ""
            Dim _FNTimeOTNormal As Double = 0 : Dim _FNTimeOTMoveCome As Double = 0 : Dim _FNTimeOTMoveTo As Double = 0
            _Cmd = "SELECT    sum( T.FNOT1Min + T.FNOT1_5Min + T.FNOT2Min + T.FNOT3Min + T.FNOT4Min) AS FNTime"
            _Cmd &= vbCrLf & "        FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) ON T.FNHSysEmpID = E.FNHSysEmpID"
            _Cmd &= vbCrLf & " where FTDateTrans = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "'"
            _Cmd &= vbCrLf & " AND E.FNHSysUnitSectId =" & Integer.Parse(_UnitSect)
            _FNTimeOTNormal = Convert.ToDouble(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "0"))

            _Cmd = "SELECT  Sum(Isnull((CASE WHEN  M.FTEndTime >= T.FTIn3 and M.FTEndTime <= T.FTOut3 THEN "
            _Cmd &= vbCrLf & "						CASE WHEN M.FTStartTime >= T.FTIn3 THEN M.FNTotalMinute"
            _Cmd &= vbCrLf & "							 WHEN M.FTStartTime < T.FTIn3  THEN DATEDIFF(MINUTE,CONVERT(datetime,T.FTIn3),CONVERT(datetime,M.FTEndTime))"
            _Cmd &= vbCrLf & "  End"
            _Cmd &= vbCrLf & " END),0)) AS FNTimeOTMin	 "
            _Cmd &= vbCrLf & "FROM         HITECH_PRODUCTION.dbo.TPRODTMoveTeamMoveType AS M INNER JOIN"
            _Cmd &= vbCrLf & "            THRTTrans AS T ON M.FNHSysEmpID = T.FNHSysEmpID AND M.FDDate = T.FTDateTrans"
            _Cmd &= vbCrLf & "WHERE     (M.FNHSysUnitSectIdTo = " & Integer.Parse(_UnitSect) & ") AND (M.FDDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "')"
            _FNTimeOTMoveCome = Convert.ToDouble(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "0"))

            _Cmd = "SELECT  Sum(Isnull((CASE WHEN  M.FTEndTime >= T.FTIn3 and M.FTEndTime <= T.FTOut3 THEN "
            _Cmd &= vbCrLf & "						CASE WHEN M.FTStartTime >= T.FTIn3 THEN M.FNTotalMinute"
            _Cmd &= vbCrLf & "							 WHEN M.FTStartTime < T.FTIn3  THEN DATEDIFF(MINUTE,CONVERT(datetime,T.FTIn3),CONVERT(datetime,M.FTEndTime))"
            _Cmd &= vbCrLf & "  End"
            _Cmd &= vbCrLf & " END),0)) AS FNTimeOTMin	 "
            _Cmd &= vbCrLf & "FROM         HITECH_PRODUCTION.dbo.TPRODTMoveTeamMoveType AS M INNER JOIN"
            _Cmd &= vbCrLf & "            THRTTrans AS T ON M.FNHSysEmpID = T.FNHSysEmpID AND M.FDDate = T.FTDateTrans"
            _Cmd &= vbCrLf & "WHERE     (M.FNHSysUnitSectId = " & Integer.Parse(_UnitSect) & ") AND (M.FDDate = '" & HI.UL.ULDate.ConvertEnDB(_Date) & "')"
            _FNTimeOTMoveTo = Convert.ToDouble(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "0"))

            Return (_FNTimeOTNormal + _FNTimeOTMoveCome) - _FNTimeOTMoveTo
        Catch ex As Exception
            Return 0
        End Try
    End Function



    Private Function GetSalaryPerMin(ByVal _EmpCode As Integer, ByVal _EmpType As Integer) As Double
        Try
            Dim _Cmd As String = ""
            Dim _oDt, tmpDTConfigAllowancePassProba As DataTable
            Dim _NewSlary, _FTSlary, _FTSlaryPerMin, FNHarmfulBaht, FNSkillBaht, FNSkillRate, FNHarmfulRate As Double
            Dim _Probation As String
            Dim _DayPerMonth As Integer = GetDayPerMonth(Me.FDDTrans.Text, _EmpType)

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
            _Cmd &= vbCrLf & "WHERE (FTEffectiveDate > N'" & HI.UL.ULDate.ConvertEnDB(Me.FDDTrans.Text) & "') "
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


            If _Probation <= HI.UL.ULDate.ConvertEnDB(Me.FDDTrans.Text) Then
                If _EmpTypePayHarmful = "1" Then
                    FNHarmfulBaht = (_FTSlary * FNHarmfulRate) / 100
                End If
                If _EmpTypePaySkill = "1" Then
                    FNSkillBaht = ((_FTSlary + FNHarmfulBaht) * FNSkillRate) / 100
                End If
                _FTSlary = _FTSlary + FNHarmfulBaht + FNSkillBaht
            End If

            _FTSlaryPerMin = _FTSlary / (_DayPerMonth * 480)

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


    Private Function DeleteWageIncentive(ByVal _DateTrans As String, ByVal _UnitSectId As Integer) As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _IncentivePerMin As Double = 0
            Dim _IncentiveOTPerMin As Double = 0


            _Cmd = "SELECT  T.FNHSysEmpID,   T.FTDateTrans,   Isnull(T.FNOT1Min,0) + Isnull(T.FNOT1_5Min,0) + Isnull(T.FNOT2Min,0) + Isnull(T.FNOT3Min,0) + Isnull(T.FNOT4Min,0) AS FNTimeOT"
            _Cmd &= vbCrLf & "	,E.FNHSysEmpTypeId ,   E.FNHSysUnitSectId , T.FNTimeMin AS FNTimeMinNormal ,T.FNOT1Min,T.FNOT1_5Min , T.FNOT2Min ,T.FNOT3Min , T.FNOT4Min  "
            _Cmd &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK) INNER JOIN"
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) ON T.FNHSysEmpID = E.FNHSysEmpID"
            _Cmd &= vbCrLf & "where FTDateTrans = '" & HI.UL.ULDate.ConvertEnDB(_DateTrans) & "'"
            _Cmd &= vbCrLf & "and  E.FNHSysUnitSectId =" & Integer.Parse(_UnitSectId)


            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR)

            For Each R As DataRow In _oDt.Rows
                Dim _Incentive As Double = 0
                Dim _SalaryPerMin As Double = GetSalaryPerMin(R!FNHSysEmpID.ToString, R!FNHSysEmpTypeId.ToString)
                Dim _FCBaht As Double = _SalaryPerMin * Double.Parse(R!FNTimeMinNormal.ToString)
                Dim _FCBahtOT15 As Double = (_SalaryPerMin * Double.Parse(R!FNTimeOT.ToString)) * 1.5
                Dim _IncentiveOT As Double = 0

                _Cmd = "  UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                _Cmd &= vbCrLf & " SET FNProNormal=" & _Incentive & ""
                _Cmd &= vbCrLf & ", FNNetProAmt=" & _Incentive + _IncentiveOT & " + FNProOther "
                _Cmd &= vbCrLf & ",FNProOT=" & (_IncentiveOT)
                _Cmd &= vbCrLf & ", FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Cmd &= vbCrLf & ", FTUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                _Cmd &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Cmd &= vbCrLf & " WHERE  FNHSysEmpID=" & Integer.Parse(R!FNHSysEmpID.ToString) & " AND FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(_DateTrans) & "'  "
                If Not (HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_HR)) Then
                    _Cmd = "  INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                    _Cmd &= vbCrLf & " (FTInsUser, FTInsDate, FTInsTime,  FNHSysEmpID, FTDateTrans, FNAmtNormal, FNAmtOT, FNNetAmt, FNProNormal, FNProOther, FNNetProAmt , FNProOT) "
                    _Cmd &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Cmd &= vbCrLf & " ," & Integer.Parse(R!FNHSysEmpID.ToString) & ",'" & HI.UL.ULDate.ConvertEnDB(_DateTrans) & "'"
                    _Cmd &= vbCrLf & " , " & _FCBaht & ""
                    _Cmd &= vbCrLf & " ," & _FCBahtOT15 & ""
                    _Cmd &= vbCrLf & " ," & _FCBaht + _FCBahtOT15 & ""
                    _Cmd &= vbCrLf & " ," & _Incentive & ",0," & _IncentiveOT + _Incentive & ""
                    _Cmd &= vbCrLf & ", " & (_IncentiveOT)
                    If Not (HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_HR)) Then
                        Return False
                    End If
                End If

            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class