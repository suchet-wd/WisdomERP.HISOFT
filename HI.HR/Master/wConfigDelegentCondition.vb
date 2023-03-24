Imports System
Imports System.Windows.Forms

Public Class wConfigDelegentCondition

#Region "Process"
    Dim DeType As Integer = 0
    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If FTDeligentCode.Text <> "" Then
            _Pass = True
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDeligentCode_lbl.Text)
            FTDeligentCode.Focus()
        End If

        Return _Pass
    End Function

    Private Function SaveData() As Boolean
        Try
            Dim _Qry As String

            CType(ogdL.DataSource, DataTable).AcceptChanges()

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try


                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigHD SET "
                _Qry &= vbCrLf & "  FTStateRightNow='" & FTStateRightNow.EditValue.ToString & "'"
                _Qry &= vbCrLf & ", FTAbsentOpt='" & FTAbsentOpt.EditValue.ToString & "'"
                _Qry &= vbCrLf & ", FTLateOpt='" & FTLateOpt.EditValue.ToString & "'"
                _Qry &= vbCrLf & ", FTLeaveOpt='" & FTLeaveOpt.EditValue.ToString & "'"
                _Qry &= vbCrLf & ", FTVacationOpt='" & FTVacationOpt.EditValue.ToString & "' "
                _Qry &= vbCrLf & ", FTStateScanCard='" & FTStateScanCard.EditValue.ToString & "' "
                _Qry &= vbCrLf & ", FTResetNewYearOpt='" & FTResetNewYearOpt.EditValue.ToString & "' "
                _Qry &= vbCrLf & ", FNStartDiligent=" & FNStartDiligent.Value & ""
                _Qry &= vbCrLf & ", FNWageRate=" & FNWageRate.Value & " "
                _Qry &= vbCrLf & ", FNDeligent=" & FNDeligent.SelectedIndex & " "
                _Qry &= vbCrLf & ", FNDeligentPeriod=" & FNDeligentPeriod.SelectedIndex & " "
                _Qry &= vbCrLf & ", FTResetOpt='" & FTResetOpt.EditValue.ToString & "' "
                _Qry &= vbCrLf & ", FNPayDeligent='" & FNPayDeligent.SelectedIndex & "' "
                _Qry &= vbCrLf & ", FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ", FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ", FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & " WHERE  FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' "

                If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigHD(FTDeligentCode, FTStateRightNow, FTAbsentOpt, FTLateOpt, "
                    _Qry &= vbCrLf & "  FTLeaveOpt, FTVacationOpt, FTStateScanCard, FTResetNewYearOpt, FNStartDiligent, FNWageRate, FNDeligent, FNDeligentPeriod, FTResetOpt,FNPayDeligent "
                    _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "','" & FTStateRightNow.EditValue.ToString & "'"
                    _Qry &= vbCrLf & ",'" & FTAbsentOpt.EditValue.ToString & "','" & FTLateOpt.EditValue.ToString & "' "
                    _Qry &= vbCrLf & ",'" & FTLeaveOpt.EditValue.ToString & "','" & FTVacationOpt.EditValue.ToString & "' "
                    _Qry &= vbCrLf & ",'" & FTStateScanCard.EditValue.ToString & "'"
                    _Qry &= vbCrLf & ",'" & FTResetNewYearOpt.EditValue.ToString & "'," & FNStartDiligent.Value & "," & FNWageRate.Value & " "
                    _Qry &= vbCrLf & "," & FNDeligent.SelectedIndex & "," & FNDeligentPeriod.SelectedIndex & ",'" & FTResetOpt.EditValue.ToString & "'," & FNPayDeligent.SelectedIndex & "  "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                    If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

                'ogv.Columns(0).Visible = True
                'ogv.Columns(0).VisibleIndex = 0

                If Me.FNMonth.Value > 0 Then

                    _Qry = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT SET "
                    _Qry &= vbCrLf & " FNPayRate=" & Me.FNRate.Value
                    'If (otab.SelectedTabPage Is otpnotscan) Then
                    '    _Qry &= vbCrLf & ",FNNotScanPer=" & Me.FNNotScanPer.Value
                    'End If
                    'If (otab.SelectedTabPage Is otplate) Then
                    '    _Qry &= vbCrLf & ",FNLatePer=" & Me.FNLatePer.Value
                    'End If
                    'If (otab.SelectedTabPage Is otplatemore) Then
                    '    _Qry &= vbCrLf & ",FNLatePer2=" & Me.FNLatePer2.Value
                    'End If
                    'If (otab.SelectedTabPage Is otpabsent) Then
                    '    _Qry &= vbCrLf & ",FNAbsentPer=" & Me.FNAbsentPer.Value
                    'End If
                    _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " WHERE  FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' "
                    'If (FNMonth.Value > 0) Then
                    _Qry &= vbCrLf & "  AND   FNMonthRate=" & Me.FNMonth.Value & " "
                    'End If
                    '_Qry &= vbCrLf & "  AND   FNFromTime=" & Me.FNFromTime.Value & " AND FNToTime=" & Me.FNToTime.Value & ""
                    'If (otab.SelectedTabPage Is otpnotscan) Then
                    '    _Qry &= vbCrLf & "  AND   FNFromTime=" & Me.FNFromTime.Value & " AND FNToTime=" & Me.FNToTime.Value & ""
                    'End If
                    'If (otab.SelectedTabPage Is otplate) Then
                    '    _Qry &= vbCrLf & "  AND   FNFromTime=" & Me.FNFromTimeL.Value & " AND FNToTime=" & Me.FNToTimeL.Value & ""
                    'End If
                    'If (otab.SelectedTabPage Is otplatemore) Then
                    '    _Qry &= vbCrLf & "  AND   FNFromTime=" & Me.FNFromTimeL2.Value & " AND FNToTime=" & Me.FNToTimeL2.Value & ""
                    'End If
                    'If (otab.SelectedTabPage Is otpabsent) Then
                    '    _Qry &= vbCrLf & "  AND   FNFromTime=" & Me.FNFromTimeA.Value & " AND FNToTime=" & Me.FNToTimeA.Value & ""
                    'End If

                    If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT(FTDeligentCode, FNMonthRate, FNPayRate"
                        ' _Qry &= vbCrLf & ", FNFromTime, FNToTime, FNNotScanPer, FNLatePer, FNLatePer2, FNAbsentPer"
                        _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "'," & Me.FNMonth.Value & "," & Me.FNRate.Value & " "

                        '_Qry &= vbCrLf & "," & Me.FNFromTime.Value & "," & Me.FNToTime.Value
                        'If (otab.SelectedTabPage Is otpnotscan) Or (otab.SelectedTabPage Is otppaystep) Then
                        '    _Qry &= vbCrLf & "," & Me.FNFromTime.Value & "," & Me.FNToTime.Value
                        'End If
                        'If (otab.SelectedTabPage Is otplate) Then
                        '    _Qry &= vbCrLf & "," & Me.FNFromTimeL.Value & "," & Me.FNToTimeL.Value
                        'End If
                        'If (otab.SelectedTabPage Is otplatemore) Then
                        '    _Qry &= vbCrLf & "," & Me.FNFromTimeL2.Value & "," & Me.FNToTimeL2.Value
                        'End If
                        'If (otab.SelectedTabPage Is otpabsent) Then
                        '    _Qry &= vbCrLf & "," & Me.FNFromTimeA.Value & "," & Me.FNToTimeA.Value
                        'End If

                        '  _Qry &= vbCrLf & "," & Me.FNNotScanPer.Value & "," & Me.FNLatePer.Value & "," & Me.FNLatePer2.Value & "," & Me.FNAbsentPer.Value
                        _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                        If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                    End If
                End If

                If DeType = 1 Then
                    'If (FNFromTime.Value > 0) Or (FNFromTimeL.Value > 0) Or (FNFromTimeL2.Value > 0) Or (FNFromTimeA.Value > 0) Then
                    _Qry = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct SET "
                    _Qry &= vbCrLf & " FNStartMinute=" & Me.FNFromTime.Value
                    _Qry &= vbCrLf & ",FNToMinute=" & Me.FNToTime.Value
                    _Qry &= vbCrLf & ",FNDeductPer=" & Me.FNNotScanPer.Value
                    _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " WHERE  FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' "
                    _Qry &= vbCrLf & "  AND   FNDeligenDeductType=" & DeType & " AND FNSeq=" & Me.FNSeq.Value

                    If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Qry = "SELECT MAX(Cast(ISNULL(FNSeq,0) AS numeric(18,0))) AS FNSeq FROM THRMDiligentConfigDT_Deduct WITH(NOLOCK) WHERE FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' AND FNDeligenDeductType=" & DeType

                        Dim tSeqNo As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                        tSeqNo = Val(tSeqNo) + 1

                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct(FTInsUser, FDInsDate, FTInsTime"
                        _Qry &= vbCrLf & ", FTDeligentCode, FNDeligenDeductType, FNSeq, FNStartMinute, FNToMinute, FNDeductPer)  "
                        _Qry &= vbCrLf & " SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "'," & DeType & "," & tSeqNo
                        _Qry &= vbCrLf & ", " & FNFromTime.Value & ", " & FNToTime.Value & ", " & FNNotScanPer.Value

                        If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If
                    End If

                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct SET FNSeq=FNNo"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct INNER JOIN "
                    _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNStartMinute,FNToMinute) AS FNNo, FNSeq"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct "
                    _Qry &= vbCrLf & "WHERE FNDeligenDeductType = " & DeType & ") T1 ON THRMDiligentConfigDT_Deduct.FNSeq=T1.FNSeq "
                    _Qry &= vbCrLf & "WHERE FNDeligenDeductType = " & DeType
                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                ElseIf DeType = 2 Then
                    _Qry = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct SET "
                    _Qry &= vbCrLf & " FNStartMinute=" & Me.FNFromTimeL.Value
                    _Qry &= vbCrLf & ",FNToMinute=" & Me.FNToTimeL.Value
                    _Qry &= vbCrLf & ",FNDeductPer=" & Me.FNLatePer.Value
                    _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " WHERE  FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' "
                    _Qry &= vbCrLf & "  AND   FNDeligenDeductType=" & DeType & " AND FNSeq=" & Me.FNSeqL.Value

                    If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Qry = "SELECT MAX(Cast(ISNULL(FNSeq,0) AS numeric(18,0))) AS FNSeq FROM THRMDiligentConfigDT_Deduct WITH(NOLOCK) WHERE FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' AND FNDeligenDeductType=" & DeType

                        Dim tSeqNo As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                        tSeqNo = Val(tSeqNo) + 1

                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct(FTInsUser, FDInsDate, FTInsTime"
                        _Qry &= vbCrLf & ", FTDeligentCode, FNDeligenDeductType, FNSeq, FNStartMinute, FNToMinute, FNDeductPer)  "
                        _Qry &= vbCrLf & " SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "'," & DeType & "," & tSeqNo
                        _Qry &= vbCrLf & ", " & FNFromTimeL.Value & ", " & FNToTimeL.Value & ", " & FNLatePer.Value

                        If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If
                    End If

                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct SET FNSeq=FNNo"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct INNER JOIN "
                    _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNStartMinute,FNToMinute) AS FNNo, FNSeq"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct "
                    _Qry &= vbCrLf & "WHERE FNDeligenDeductType = " & DeType & ") T1 ON THRMDiligentConfigDT_Deduct.FNSeq=T1.FNSeq "
                    _Qry &= vbCrLf & "WHERE FNDeligenDeductType = " & DeType
                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                ElseIf DeType = 3 Then
                    _Qry = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct SET "
                    _Qry &= vbCrLf & " FNStartMinute=" & Me.FNFromTimeL2.Value
                    _Qry &= vbCrLf & ",FNToMinute=" & Me.FNToTimeL2.Value
                    _Qry &= vbCrLf & ",FNDeductPer=" & Me.FNLatePer2.Value
                    _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " WHERE  FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' "
                    _Qry &= vbCrLf & "  AND   FNDeligenDeductType=" & DeType & " AND FNSeq=" & Me.FNSeqL2.Value

                    If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Qry = "SELECT MAX(Cast(ISNULL(FNSeq,0) AS numeric(18,0))) AS FNSeq FROM THRMDiligentConfigDT_Deduct WITH(NOLOCK) WHERE FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' AND FNDeligenDeductType=" & DeType

                        Dim tSeqNo As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                        tSeqNo = Val(tSeqNo) + 1

                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct(FTInsUser, FDInsDate, FTInsTime"
                        _Qry &= vbCrLf & ", FTDeligentCode, FNDeligenDeductType, FNSeq, FNStartMinute, FNToMinute, FNDeductPer)  "
                        _Qry &= vbCrLf & " SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "'," & DeType & "," & tSeqNo
                        _Qry &= vbCrLf & ", " & FNFromTimeL2.Value & ", " & FNToTimeL2.Value & ", " & FNLatePer2.Value

                        If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If
                    End If

                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct SET FNSeq=FNNo"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct INNER JOIN "
                    _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNStartMinute,FNToMinute) AS FNNo, FNSeq"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct "
                    _Qry &= vbCrLf & "WHERE FNDeligenDeductType = " & DeType & ") T1 ON THRMDiligentConfigDT_Deduct.FNSeq=T1.FNSeq "
                    _Qry &= vbCrLf & "WHERE FNDeligenDeductType = " & DeType
                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                ElseIf DeType = 4 Then
                    _Qry = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct SET "
                    _Qry &= vbCrLf & " FNStartMinute=" & Me.FNFromTimeA.Value
                    _Qry &= vbCrLf & ",FNToMinute=" & Me.FNToTimeA.Value
                    _Qry &= vbCrLf & ",FNDeductPer=" & Me.FNAbsentPer.Value
                    _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " WHERE  FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' "
                    _Qry &= vbCrLf & "  AND   FNDeligenDeductType=" & DeType & " AND FNSeq=" & Me.FNSeqA.Value

                    If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Qry = "SELECT MAX(Cast(ISNULL(FNSeq,0) AS numeric(18,0))) AS FNSeq FROM THRMDiligentConfigDT_Deduct WITH(NOLOCK) WHERE FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' AND FNDeligenDeductType=" & DeType

                        Dim tSeqNo As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                        tSeqNo = Val(tSeqNo) + 1

                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct(FTInsUser, FDInsDate, FTInsTime"
                        _Qry &= vbCrLf & ", FTDeligentCode, FNDeligenDeductType, FNSeq, FNStartMinute, FNToMinute, FNDeductPer)  "
                        _Qry &= vbCrLf & " SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "'," & DeType & "," & tSeqNo
                        _Qry &= vbCrLf & ", " & FNFromTimeA.Value & ", " & FNToTimeA.Value & ", " & FNAbsentPer.Value

                        If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If
                    End If

                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct SET FNSeq=FNNo"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct INNER JOIN "
                    _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNStartMinute,FNToMinute) AS FNNo, FNSeq"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct "
                    _Qry &= vbCrLf & "WHERE FNDeligenDeductType = " & DeType & ") T1 ON THRMDiligentConfigDT_Deduct.FNSeq=T1.FNSeq "
                    _Qry &= vbCrLf & "WHERE FNDeligenDeductType = " & DeType
                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                End If


                _Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfig_LeaveType WHERE  FNHSysCmpId = '" & HI.ST.SysInfo.CmpID & "' and  FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' "
                HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                For Each R As DataRow In CType(ogdL.DataSource, DataTable).Rows
                    _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfig_LeaveType( FTInsUser, FDInsDate, FTInsTime,  FTDeligentCode, FTLeaveTypeCode, FTLeaveOpt, FNHSysCmpId ) "
                    _Qry &= vbCrLf & "SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""

                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(R!FTDeligentCode.ToString) & "'"
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(R!FTLeaveTypeCode.ToString) & "'"
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(R!FTLeaveOpt.ToString) & "'"
                    _Qry &= vbCrLf & "," & HI.ST.SysInfo.CmpID & ""

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

                DeType = 0





                Return True

            Catch ex As Exception

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False

            End Try
        Catch ex As Exception

            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        If Me.FNMonth.Value >= 0 Then

            Dim _Qry As String = ""

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try

                If (Me.FNMonth.Value > 0) Then

                    _Qry = " Delete   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT  "
                    _Qry &= vbCrLf & " WHERE  FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' "
                    _Qry &= vbCrLf & "  AND   FNMonthRate=" & Me.FNMonth.Value & " " 'AND FNPayRate=" & FNRate.Value & ""

                    'If ((Me.FNFromTime.Value > 0) And (Me.FNToTime.Value > 0)) Then
                    '    _Qry &= vbCrLf & " AND FNFromTime = " & Me.FNFromTime.Value & " AND FNToTime = " & Me.FNToTime.Value & ""
                    'ElseIf ((Me.FNFromTimeL.Value > 0) And (Me.FNToTimeL.Value > 0)) Then
                    '    _Qry &= vbCrLf & " AND FNFromTime = " & Me.FNFromTimeL.Value & " AND FNToTime = " & Me.FNToTimeL.Value & ""
                    'ElseIf ((Me.FNFromTimeL2.Value > 0) And (Me.FNToTimeL2.Value > 0)) Then
                    '    _Qry &= vbCrLf & " AND FNFromTime = " & Me.FNFromTimeL2.Value & " AND FNToTime = " & Me.FNToTimeL2.Value & ""
                    'ElseIf ((Me.FNFromTimeA.Value > 0) And (Me.FNToTimeA.Value > 0)) Then
                    '    _Qry &= vbCrLf & " AND FNFromTime = " & Me.FNFromTimeA.Value & " AND FNToTime = " & Me.FNToTimeA.Value & ""
                    'End If

                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    Me.FNRate.Value = 0
                    Me.FNMonth.Value = 0
                    Me.FNFromTime.Value = 0
                    Me.FNToTime.Value = 0
                    Me.FNNotScanPer.Value = 0
                    Me.FNFromTimeL.Value = 0
                    Me.FNToTimeL.Value = 0
                    Me.FNLatePer.Value = 0
                    Me.FNFromTimeL2.Value = 0
                    Me.FNToTimeL2.Value = 0
                    Me.FNLatePer2.Value = 0
                    Me.FNFromTimeA.Value = 0
                    Me.FNToTimeA.Value = 0
                    Me.FNAbsentPer.Value = 0

                Else

                    _Qry = " Delete   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT  "
                    _Qry &= vbCrLf & " WHERE  FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' "

                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    _Qry = " Delete   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigHD  "
                    _Qry &= vbCrLf & " WHERE  FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' "

                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    _Qry = " Delete   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct  "
                    _Qry &= vbCrLf & " WHERE  FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' "

                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    HI.TL.HandlerControl.ClearControl(Me)

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
        Else
            Return False
        End If

    End Function

    Private Function DeleteTableData() As Boolean
        If Me.FNMonth.Value >= 0 Then

            Dim _Qry As String = ""
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try

                If (Me.FNMonth.Value > 0) Then
                    _Qry = " Delete   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT  "
                    _Qry &= vbCrLf & " WHERE  FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' "
                    _Qry &= vbCrLf & "  AND   FNMonthRate=" & Me.FNMonth.Value & " " ' AND FNPayRate=" & FNRate.Value & ""
                    'If (otab.SelectedTabPage Is otpnotscan) Then
                    '    _Qry &= vbCrLf & " AND FNFromTime = " & Me.FNFromTime.Value & " AND FNToTime = " & Me.FNToTime.Value & ""
                    'ElseIf (otab.SelectedTabPage Is otplate) Then
                    '    _Qry &= vbCrLf & " AND FNFromTime = " & Me.FNFromTimeL.Value & " AND FNToTime = " & Me.FNToTimeL.Value & ""
                    'ElseIf (otab.SelectedTabPage Is otplatemore) Then
                    '    _Qry &= vbCrLf & " AND FNFromTime = " & Me.FNFromTimeL2.Value & " AND FNToTime = " & Me.FNToTimeL2.Value & ""
                    'ElseIf (otab.SelectedTabPage Is otpabsent) Then
                    '    _Qry &= vbCrLf & " AND FNFromTime = " & Me.FNFromTimeA.Value & " AND FNToTime = " & Me.FNToTimeA.Value & ""
                    '    '    If ((Me.FNFromTime.Value > 0) And (Me.FNToTime.Value > 0)) Then
                    '    '    _Qry &= vbCrLf & " AND FNFromTime = " & Me.FNFromTime.Value & " AND FNToTime = " & Me.FNToTime.Value & ""
                    '    'ElseIf ((Me.FNFromTimeL.Value > 0) And (Me.FNToTimeL.Value > 0)) Then
                    '    '    _Qry &= vbCrLf & " AND FNFromTime = " & Me.FNFromTimeL.Value & " AND FNToTime = " & Me.FNToTimeL.Value & ""
                    '    'ElseIf ((Me.FNFromTimeL2.Value > 0) And (Me.FNToTimeL2.Value > 0)) Then
                    '    '    _Qry &= vbCrLf & " AND FNFromTime = " & Me.FNFromTimeL2.Value & " AND FNToTime = " & Me.FNToTimeL2.Value & ""
                    '    'ElseIf ((Me.FNFromTimeA.Value > 0) And (Me.FNToTimeA.Value > 0)) Then
                    '    '    _Qry &= vbCrLf & " AND FNFromTime = " & Me.FNFromTimeA.Value & " AND FNToTime = " & Me.FNToTimeA.Value & ""
                    'End If

                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                End If

                If (DeType = 1) Then
                    _Qry = " Delete   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct  "
                    _Qry &= vbCrLf & " WHERE  FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' "
                    _Qry &= vbCrLf & "  AND   FNDeligenDeductType=" & DeType & " AND FNSeq=" & FNSeq.Value

                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct SET FNSeq=FNNo"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct INNER JOIN "
                    _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNStartMinute,FNToMinute) AS FNNo, FNSeq"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct "
                    _Qry &= vbCrLf & "WHERE FNDeligenDeductType = " & DeType & ") T1 ON THRMDiligentConfigDT_Deduct.FNSeq=T1.FNSeq "
                    _Qry &= vbCrLf & "WHERE FNDeligenDeductType = " & DeType
                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                ElseIf (DeType = 2) Then
                    _Qry = " Delete   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct  "
                    _Qry &= vbCrLf & " WHERE  FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' "
                    _Qry &= vbCrLf & "  AND   FNDeligenDeductType=" & DeType & " AND FNSeq=" & FNSeqL.Value

                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct SET FNSeq=FNNo"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct INNER JOIN "
                    _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNStartMinute,FNToMinute) AS FNNo, FNSeq"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct "
                    _Qry &= vbCrLf & "WHERE FNDeligenDeductType = " & DeType & ") T1 ON THRMDiligentConfigDT_Deduct.FNSeq=T1.FNSeq "
                    _Qry &= vbCrLf & "WHERE FNDeligenDeductType = " & DeType
                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                ElseIf (DeType = 3) Then
                    _Qry = " Delete   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct  "
                    _Qry &= vbCrLf & " WHERE  FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' "
                    _Qry &= vbCrLf & "  AND   FNDeligenDeductType=" & DeType & " AND FNSeq=" & FNSeqL2.Value

                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct SET FNSeq=FNNo"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct INNER JOIN "
                    _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNStartMinute,FNToMinute) AS FNNo, FNSeq"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct "
                    _Qry &= vbCrLf & "WHERE FNDeligenDeductType = " & DeType & ") T1 ON THRMDiligentConfigDT_Deduct.FNSeq=T1.FNSeq "
                    _Qry &= vbCrLf & "WHERE FNDeligenDeductType = " & DeType
                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                ElseIf (DeType = 4) Then
                    _Qry = " Delete   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct  "
                    _Qry &= vbCrLf & " WHERE  FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' "
                    _Qry &= vbCrLf & "  AND   FNDeligenDeductType=" & DeType & " AND FNSeq=" & FNSeqA.Value

                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct SET FNSeq=FNNo"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct INNER JOIN "
                    _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNStartMinute,FNToMinute) AS FNNo, FNSeq"
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct "
                    _Qry &= vbCrLf & "WHERE FNDeligenDeductType = " & DeType & ") T1 ON THRMDiligentConfigDT_Deduct.FNSeq=T1.FNSeq "
                    _Qry &= vbCrLf & "WHERE FNDeligenDeductType = " & DeType
                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                End If

                DeType = 0
                Me.FNRate.Value = 0
                Me.FNMonth.Value = 0
                Me.FNFromTime.Value = 0
                Me.FNToTime.Value = 0
                Me.FNNotScanPer.Value = 0
                Me.FNFromTimeL.Value = 0
                Me.FNToTimeL.Value = 0
                Me.FNLatePer.Value = 0
                Me.FNFromTimeL2.Value = 0
                Me.FNToTimeL2.Value = 0
                Me.FNLatePer2.Value = 0
                Me.FNFromTimeA.Value = 0
                Me.FNToTimeA.Value = 0
                Me.FNAbsentPer.Value = 0
                Me.FNSeq.Value = 0
                Me.FNSeqL.Value = 0
                Me.FNSeqL2.Value = 0
                Me.FNSeqA.Value = 0

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
        Else
            Return False
        End If

    End Function

    Private Sub LoadData(ByVal Key As String)
        Dim _Qry As String
        Dim _Dt As DataTable

        _Qry = " SELECT    TOP 1   FTStateRightNow, FTAbsentOpt, FTLateOpt, "
        _Qry &= vbCrLf & "  FTLeaveOpt, FTVacationOpt, FTStateScanCard, FTResetNewYearOpt, FNStartDiligent, FNWageRate, FNDeligent, FNDeligentPeriod, FTResetOpt,FNPayDeligent "
        _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigHD WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE FTDeligentCode='" & HI.UL.ULF.rpQuoted(Key) & "'  "

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        If _Dt.Rows.Count <= 0 Then
            _Dt.Rows.Add()
        End If

        Dim _FieldName As String = ""

        For Each R As DataRow In _Dt.Rows
            For Each Col As DataColumn In _Dt.Columns
                _FieldName = Col.ColumnName.ToString

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                .Text = R.Item(Col).ToString
                            End With
                        Case ENM.Control.ControlType.CalcEdit
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Value = Val(R.Item(Col).ToString)
                            End With
                        Case ENM.Control.ControlType.ComboBoxEdit
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                Try
                                    .SelectedIndex = Val(R.Item(Col).ToString)
                                Catch ex As Exception
                                    .SelectedIndex = -1
                                End Try
                            End With
                        Case ENM.Control.ControlType.CheckEdit
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .EditValue = (Integer.Parse(Val(R.Item(Col).ToString))).ToString
                            End With
                        Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
                            Obj.Text = R.Item(Col).ToString
                        Case ENM.Control.ControlType.PictureEdit
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                Try
                                    .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString)
                                Catch ex As Exception
                                    .Image = Nothing
                                End Try
                            End With
                        Case ENM.Control.ControlType.DateEdit
                            Try
                                With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                    .Text = HI.UL.ULDate.ConvertEN(R.Item(Col).ToString)
                                End With
                            Catch ex As Exception
                            End Try
                        Case Else
                            Obj.Text = R.Item(Col).ToString
                    End Select
                Next
            Next
            Exit For
        Next


    End Sub

    Private Sub LoadDataGrid(ByVal Key As String)
        Dim _Qry As String
        _Qry = " SELECT FNMonthRate, FNPayRate"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT as A WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTDeligentCode='" & HI.UL.ULF.rpQuoted(Key) & "' AND FNMonthRate > 0"

        Me.ogd.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        _Qry = " SELECT FNDeligenDeductType, FNSeq, FNStartMinute, FNToMinute, FNDeductPer"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct as A WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTDeligentCode='" & HI.UL.ULF.rpQuoted(Key) & "' AND FNDeligenDeductType = 1"

        Me.ogdnotscan.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        _Qry = " SELECT FNDeligenDeductType, FNSeq, FNStartMinute, FNToMinute, FNDeductPer"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct as A WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTDeligentCode='" & HI.UL.ULF.rpQuoted(Key) & "' AND FNDeligenDeductType = 2"

        Me.ogdlate.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        _Qry = " SELECT FNDeligenDeductType, FNSeq, FNStartMinute, FNToMinute, FNDeductPer"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct as A WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTDeligentCode='" & HI.UL.ULF.rpQuoted(Key) & "' AND FNDeligenDeductType = 3"

        Me.ogdlatemore.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        _Qry = " SELECT FNDeligenDeductType, FNSeq, FNStartMinute, FNToMinute, FNDeductPer"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT_Deduct as A WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTDeligentCode='" & HI.UL.ULF.rpQuoted(Key) & "' AND FNDeligenDeductType = 4"

        Me.ogdabsent.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)



        _Qry = "SELECT  FTDeligentCode ,FTLeaveTypeCode, FTLeaveOpt,  FNHSysCmpId "
        _Qry &= vbCrLf & " " & IIf(HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal, ",LL.FTNameTH", ",LL.FTNameEN") & " AS FTLeaveDesc "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfig_LeaveType CL  "
        _Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 FNListIndex, FTNameTH, FTNameEN	  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].[dbo].[HSysListData] L "
        _Qry &= vbCrLf & "  where FTListName = 'fnleavetype' AND  CL.FTLeaveTypeCode=L.FNListIndex) LL "
        _Qry &= vbCrLf & " WHERE FNHSysCmpId = '" & HI.ST.SysInfo.CmpID & "' and  FTDeligentCode='" & HI.UL.ULF.rpQuoted(Key) & "' "

        'Me.ogdL.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        ogdL.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
    End Sub

#End Region

#Region "General"

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub ocmsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click
        If Me.VerifyData Then
            Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
            If Me.SaveData() Then
                _Spls.Close()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Me.LoadData(Me.FTDeligentCode.Text)
                Me.LoadDataGrid(Me.FTDeligentCode.Text)

                Me.FNRate.Value = 0
                Me.FNMonth.Value = 0
                Me.FNFromTime.Value = 0
                Me.FNToTime.Value = 0
                Me.FNNotScanPer.Value = 0
                Me.FNFromTimeL.Value = 0
                Me.FNToTimeL.Value = 0
                Me.FNLatePer.Value = 0
                Me.FNFromTimeL2.Value = 0
                Me.FNToTimeL2.Value = 0
                Me.FNLatePer2.Value = 0
                Me.FNFromTimeA.Value = 0
                Me.FNToTimeA.Value = 0
                Me.FNAbsentPer.Value = 0
                Me.FNSeq.Value = 0
                Me.FNSeqL.Value = 0
                Me.FNSeqL2.Value = 0
                Me.FNSeqA.Value = 0
            Else
                _Spls.Close()
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub FTDeligentCode_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FTDeligentCode.EditValueChanged
        Call LoadData(FTDeligentCode.Text)
        Call LoadDataGrid(FTDeligentCode.Text)
    End Sub

    Private Sub ocmdelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click

        If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, ) = True Then
            Call DeleteData()
            Me.LoadData(Me.FTDeligentCode.Text)
            Me.LoadDataGrid(Me.FTDeligentCode.Text)
        End If

    End Sub

    Private Sub wDelegentConfig_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        RemoveHandler FTDeligentCode.Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly
        RemoveHandler FTDeligentCode.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
    End Sub

    Private Sub FNRate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FNRate.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me.ocmsave.Enabled And Me.ocmsave.Visible Then
                ocmsave_Click(ocmsave, New System.EventArgs)

                FNMonth.Focus()
            End If
        End If
    End Sub

    Private Sub ogd_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogd.KeyDown
        Dim _Qry As String = ""
        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Select Case e.KeyCode
                Case System.Windows.Forms.Keys.Delete
                    With ogv
                        For Each i As Integer In .GetSelectedRows()
                            _Qry = " Delete   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMDiligentConfigDT  "
                            _Qry &= vbCrLf & " WHERE  FTDeligentCode='" & HI.UL.ULF.rpQuoted(FTDeligentCode.Text) & "' "
                            _Qry &= vbCrLf & "  AND   FNMonthRate=" & (Double.Parse(.GetRowCellValue(i, "FNMonthRate").ToString)) & " "

                        Next
                    End With
                    HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            End Select

            Me.LoadData(Me.FTDeligentCode.Text)
            Me.LoadDataGrid(Me.FTDeligentCode.Text)

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        End Try
    End Sub

    Private Sub FNNotScanPer_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FNNotScanPer.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me.ocmsave.Enabled And Me.ocmsave.Visible Then
                DeType = 1
                ocmsave_Click(ocmsave, New System.EventArgs)

                FNFromTime.Focus()
            End If
        End If
    End Sub

    Private Sub ogv_Click(sender As Object, e As EventArgs) Handles ogv.Click
        With ogvnotscan
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            FNMonth.Value = .GetRowCellValue(ogvnotscan.FocusedRowHandle, "FNMonthRate")
            FNRate.Value = .GetRowCellValue(ogvnotscan.FocusedRowHandle, "FNPayRate")
        End With
    End Sub

    Private Sub ogvnotscan_Click(sender As Object, e As EventArgs) Handles ogvnotscan.Click
        With ogvnotscan
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            FNFromTime.Value = .GetRowCellValue(ogvnotscan.FocusedRowHandle, "FNStartMinute")
            FNToTime.Value = .GetRowCellValue(ogvnotscan.FocusedRowHandle, "FNToMinute")
            FNNotScanPer.Value = .GetRowCellValue(ogvnotscan.FocusedRowHandle, "FNDeductPer")
            FNSeq.Value = .GetRowCellValue(ogvnotscan.FocusedRowHandle, "FNSeq")
        End With
    End Sub

    Private Sub ogvlate_Click(sender As Object, e As EventArgs) Handles ogvlate.Click
        With ogvlate
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            FNFromTimeL.Value = .GetRowCellValue(ogvlate.FocusedRowHandle, "FNStartMinute")
            FNToTimeL.Value = .GetRowCellValue(ogvlate.FocusedRowHandle, "FNToMinute")
            FNLatePer.Value = .GetRowCellValue(ogvlate.FocusedRowHandle, "FNDeductPer")
            FNSeqL.Value = .GetRowCellValue(ogvlate.FocusedRowHandle, "FNSeq")
        End With
    End Sub

    Private Sub ogvlatemore_Click(sender As Object, e As EventArgs) Handles ogvlatemore.Click
        With ogvlatemore
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            FNFromTimeL2.Value = .GetRowCellValue(ogvlatemore.FocusedRowHandle, "FNStartMinute")
            FNToTimeL2.Value = .GetRowCellValue(ogvlatemore.FocusedRowHandle, "FNToMinute")
            FNLatePer2.Value = .GetRowCellValue(ogvlatemore.FocusedRowHandle, "FNDeductPer")
            FNSeqL2.Value = .GetRowCellValue(ogvlatemore.FocusedRowHandle, "FNSeq")
        End With
    End Sub

    Private Sub ogvabsent_Click(sender As Object, e As EventArgs) Handles ogvabsent.Click
        With ogvabsent
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            FNFromTimeA.Value = .GetRowCellValue(ogvabsent.FocusedRowHandle, "FNStartMinute")
            FNToTimeA.Value = .GetRowCellValue(ogvabsent.FocusedRowHandle, "FNToMinute")
            FNAbsentPer.Value = .GetRowCellValue(ogvabsent.FocusedRowHandle, "FNDeductPer")
            FNSeqA.Value = .GetRowCellValue(ogvabsent.FocusedRowHandle, "FNSeq")
        End With
    End Sub

    Private Sub otab_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otab.SelectedPageChanged
        Me.FNRate.Value = 0
        Me.FNMonth.Value = 0
        Me.FNFromTime.Value = 0
        Me.FNToTime.Value = 0
        Me.FNNotScanPer.Value = 0
        Me.FNFromTimeL.Value = 0
        Me.FNToTimeL.Value = 0
        Me.FNLatePer.Value = 0
        Me.FNFromTimeL2.Value = 0
        Me.FNToTimeL2.Value = 0
        Me.FNLatePer2.Value = 0
        Me.FNFromTimeA.Value = 0
        Me.FNToTimeA.Value = 0
        Me.FNAbsentPer.Value = 0
        Me.FNSeq.Value = 0
        Me.FNSeqL.Value = 0
        Me.FNSeqL2.Value = 0
        Me.FNSeqA.Value = 0
        If (otab.SelectedTabPage Is otpnotscan) Then

        End If
        If (otab.SelectedTabPage Is otplate) Then

        End If
        If (otab.SelectedTabPage Is otplatemore) Then

        End If
        If (otab.SelectedTabPage Is otpabsent) Then

        End If
    End Sub

    Private Sub FNLatePer_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FNLatePer.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me.ocmsave.Enabled And Me.ocmsave.Visible Then
                DeType = 2
                ocmsave_Click(ocmsave, New System.EventArgs)

                FNFromTimeL.Focus()
            End If
        End If
    End Sub

    Private Sub FNLatePer2_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FNLatePer2.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me.ocmsave.Enabled And Me.ocmsave.Visible Then
                DeType = 3
                ocmsave_Click(ocmsave, New System.EventArgs)

                FNFromTimeL2.Focus()
            End If
        End If
    End Sub

    Private Sub FNAbsentPer_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FNAbsentPer.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me.ocmsave.Enabled And Me.ocmsave.Visible Then
                DeType = 4
                ocmsave_Click(ocmsave, New System.EventArgs)

                FNFromTimeA.Focus()
            End If
        End If
    End Sub

    Private Sub ogvnotscan_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvnotscan.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Delete Then
            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, ) = True Then
                DeType = 1
                Call DeleteTableData()
                Me.LoadData(Me.FTDeligentCode.Text)
                Me.LoadDataGrid(Me.FTDeligentCode.Text)

            End If
        End If
    End Sub

    Private Sub ogvlate_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvlate.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Delete Then
            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, ) = True Then
                DeType = 2
                Call DeleteTableData()
                Me.LoadData(Me.FTDeligentCode.Text)
                Me.LoadDataGrid(Me.FTDeligentCode.Text)

            End If
        End If
    End Sub

    Private Sub ogvlatemore_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvlatemore.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Delete Then
            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, ) = True Then
                DeType = 3
                Call DeleteTableData()
                Me.LoadData(Me.FTDeligentCode.Text)
                Me.LoadDataGrid(Me.FTDeligentCode.Text)

            End If
        End If
    End Sub

    Private Sub ogvabsent_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvabsent.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Delete Then
            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, ) = True Then
                DeType = 4
                Call DeleteTableData()
                Me.LoadData(Me.FTDeligentCode.Text)
                Me.LoadDataGrid(Me.FTDeligentCode.Text)

            End If
        End If
    End Sub

    Private Sub ogv_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogv.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Delete Then
            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, ) = True Then
                Call DeleteTableData()
                Me.LoadData(Me.FTDeligentCode.Text)
                Me.LoadDataGrid(Me.FTDeligentCode.Text)

            End If
        End If
    End Sub

    Private Sub FNFromTime_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FNFromTime.EditValueChanging
        'FNSeq.Value = 0
    End Sub

    Private Sub FNFromTimeL_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FNFromTimeL.EditValueChanging
        'FNSeqL.Value = 0
    End Sub

    Private Sub FNFromTimeL2_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FNFromTimeL2.EditValueChanging
        'FNSeqL2.Value = 0
    End Sub

    Private Sub FNFromTimeA_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles FNFromTimeA.EditValueChanging
        'FNSeqA.Value = 0
    End Sub

#End Region

End Class