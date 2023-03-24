Option Explicit On
Option Strict Off

Imports System
Imports System.Data
Imports System.Collections.Generic
Imports System.Linq
Imports Microsoft.VisualBasic

Public Class wWelfareVN

#Region "Variable Declaration"
    Private _TSQL As String

#End Region

#Region "PROC AND FUNCTION"
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Call PROC_INIT_GRIDVIEW()
    End Sub

    Private Sub PROC_INIT_GRIDVIEW()
        Try
            With Me.ogvWelfareVN
                oColFNAttendanceAllowance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                oColFNAttendanceAllowance.DisplayFormat.FormatString = "{0:n" & HI.ST.Config.AmtDigit & "}"
                oColFNCarAllowance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                oColFNCarAllowance.DisplayFormat.FormatString = "{0:n" & HI.ST.Config.AmtDigit & "}"
                oColFNMealAllowance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                oColFNMealAllowance.DisplayFormat.FormatString = "{0:n" & HI.ST.Config.AmtDigit & "}"
            End With
        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, Me.Text)
            End If
        End Try
    End Sub

    Private Function PROC_SHOWBROWSEDATA() As Boolean
        Dim bShowBrowseData As Boolean = False
        Try
            Dim oStrBuilder As New System.Text.StringBuilder()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("DECLARE @FNLang AS INT;")
            oStrBuilder.AppendLine("SET @FNLang = " & HI.ST.Lang.Language & ";")
            oStrBuilder.AppendLine("SELECT A.FNHSysWelfareId, A.FNHSysEmpTypeId, B.FTEmpTypeCode, CASE WHEN @FNLang = 1 THEN B.FTEmpTypeNameEN")
            oStrBuilder.AppendLine("                                                                   WHEN @FNLang = 2 THEN B.FTEmpTypeNameTH ELSE B.FTEmpTypeNameEN END AS FTEmpTypeName,")
            oStrBuilder.AppendLine("       A.FNAttendanceAllowance, A.FNMealAllowance, A.FNCarAllowance , A.FNChildCareAmt , A.FNChildCareStartAge , A.FNChildCareEndAge , A.FNChildCareMaxPeople")
            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRMCfgWelfareVN AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..THRMEmpType AS B (NOLOCK) ON A.FNHSysEmpTypeId = B.FNHSysEmpTypeId")
            oStrBuilder.AppendLine("WHERE B.FTStateActive = N'1'")
            oStrBuilder.AppendLine("ORDER BY B.FTEmpTypeCode ASC;")

            If oStrBuilder.Length > 0 Then
                _TSQL = ""
                _TSQL = oStrBuilder.ToString()
            End If

            Dim DTWelfareVN As System.Data.DataTable

            DTWelfareVN = HI.Conn.SQLConn.GetDataTable(_TSQL, HI.Conn.DB.DataBaseName.DB_HR)

            Me.ogdWelfareVN.DataSource = DTWelfareVN

            bShowBrowseData = True
        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, Me.Text)
            End If
        End Try
        Return bShowBrowseData
    End Function

    Private Sub PROC_SAVEDATA() Handles ocmSave.Click
        Dim oStrBuilder As New System.Text.StringBuilder()

        oStrBuilder.Remove(0, oStrBuilder.Length)

        Dim FNHSysWelfareId As Integer

        FNHSysWelfareId = HI.TL.RunID.GetRunNoID("THRMCfgWelfareVN", "FNHSysWelfareId", Conn.DB.DataBaseName.DB_HR)

        'Dim _Cmd As String = ""

        'Try
        '    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
        '    HI.Conn.SQLConn.SqlConnectionOpen()
        '    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        '    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        '    _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRMCfgWelfareVN]"

        '    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then



        '        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
        '            HI.Conn.SQLConn.Tran.Rollback()
        '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        '        End If
        '    End If



        '    HI.Conn.SQLConn.Tran.Commit()
        '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        'Catch ex As Exception
        '    HI.Conn.SQLConn.Tran.Rollback()
        '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
        '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        'End Try






        oStrBuilder.AppendLine("DECLARE @FNHSysEmpTypeId AS INT;")
        oStrBuilder.AppendLine("DECLARE @FNHSysWelfareId AS INT;")
        oStrBuilder.AppendLine(String.Format("SET @FNHSysEmpTypeId = {0};", Val(Me.FNHSysEmpTypeId.Properties.Tag)))
        oStrBuilder.AppendLine("IF NOT EXISTS(SELECT TOP 1 L1.FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRMCfgWelfareVN AS L1 (NOLOCK) WHERE L1.FNHSysEmpTypeId = @FNHSysEmpTypeId)")
        oStrBuilder.AppendLine("BEGIN")
        'oStrBuilder.AppendLine("--PRINT 'Add New Transaction'")
        'oStrBuilder.AppendLine("--DECLARE @FNHSysWelfareId AS INT;")
        oStrBuilder.AppendLine(String.Format("SET @FNHSysWelfareId = {0};", FNHSysWelfareId))
        oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRMCfgWelfareVN] ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser],[FDUpdDate],[FTUpdTime],")
        oStrBuilder.AppendLine("      [FNHSysWelfareId],[FNHSysEmpTypeId],[FNAttendanceAllowance],[FNMealAllowance],[FNCarAllowance],[FNChildCareAmt],[FNChildCareStartAge],[FNChildCareEndAge],[FNChildCareMaxPeople] )")
        oStrBuilder.AppendLine("SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', CONVERT(VARCHAR(10), GETDATE(), 111), CONVERT(VARCHAR(8), GETDATE(), 114), NULL, NULL, NULL,")
        oStrBuilder.AppendLine(String.Format("@FNHSysWelfareId, @FNHSysEmpTypeId, {0}, {1}, {2}, ", {Val(Me.FNAttendance.Value), Val(Me.FNMeal.Value), Val(Me.FNCar.Value)}))
        oStrBuilder.AppendLine(String.Format(" {0}, {1}, {2},{3}", {Me.FNChildCareAmt.Value, Me.FNChildCareStartAge.Value, Me.FNChildCareEndAge.Value, Me.FNChildCareMaxPeople.Value}))
        oStrBuilder.AppendLine("END")
        oStrBuilder.AppendLine("ELSE")
        oStrBuilder.AppendLine("BEGIN")
        'oStrBuilder.AppendLine("--PRINT 'Update Transaction'")
        oStrBuilder.AppendLine("UPDATE A")
        oStrBuilder.AppendLine(String.Format("SET A.FNAttendanceAllowance = {0},", Val(Me.FNAttendance.Value)))
        oStrBuilder.AppendLine(String.Format("                  A.FNCarAllowance = {0},", Val(Me.FNCar.Value)))
        oStrBuilder.AppendLine(String.Format("    A.FNMealAllowance = {0},", Val(Me.FNMeal.Value)))
        oStrBuilder.AppendLine(String.Format("    A.FTUpdUser = N'{0}',", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)))
        oStrBuilder.AppendLine("       A.FDUpdDate = CONVERT(VARCHAR(10), GETDATE(), 111),")
        oStrBuilder.AppendLine("       A.FTUpdTime = CONVERT(VARCHAR(8), GETDATE(), 114)")
        oStrBuilder.AppendLine(String.Format(", A.FNChildCareAmt ={0},A.FNChildCareStartAge ={1},A.FNChildCareEndAge={2},A.FNChildCareMaxPeople = {3}", {Me.FNChildCareAmt.Value, Me.FNChildCareStartAge.Value, Me.FNChildCareEndAge.Value, Me.FNChildCareMaxPeople.Value}))
        oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRMCfgWelfareVN AS A")
        oStrBuilder.AppendLine("WHERE A.FNHSysEmpTypeId = @FNHSysEmpTypeId")
        oStrBuilder.AppendLine("END;")

        'If oStrBuilder.Length > 0 Then
        '    _TSQL = ""
        '    _TSQL = oStrBuilder.ToString()
        'End If

        _TSQL = ""
        _TSQL = oStrBuilder.ToString()

        If HI.Conn.SQLConn.ExecuteNonQuery(_TSQL, HI.Conn.DB.DataBaseName.DB_HR) = True Then
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Call PROC_SHOWBROWSEDATA()
            Me.FNHSysEmpTypeId.Focus()
        End If

    End Sub

    Private Sub PROC_CLEARDATA() Handles ocmclearclsr.Click
        'HI.TL.HandlerControl.ClearControl(Me.ogbWelfareVN)
        Me.FNAttendance.Value = 0
        Me.FNCar.Value = 0
        Me.FNMeal.Value = 0
        Me.FNAttendance.Focus()
    End Sub

    Private Sub PROC_REFRESHDATA() Handles ocmRefresh.Click
        Call PROC_SHOWBROWSEDATA()
    End Sub

    Private Sub PROC_EXIT() Handles ocmExit.Click
        Me.Close()
    End Sub

#End Region

#Region "Event Handle"
    Private Sub wWelfareVN_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub ocmSave_Click(sender As Object, e As EventArgs) Handles ocmSave.Click

    End Sub

    Private Sub ocmclearclsr_Click(sender As Object, e As EventArgs) Handles ocmclearclsr.Click

    End Sub

    Private Sub ocmRefresh_Click(sender As Object, e As EventArgs) Handles ocmRefresh.Click

    End Sub

    Private Sub ocmExit_Click(sender As Object, e As EventArgs) Handles ocmExit.Click

    End Sub

    Private Sub FNHSysEmpTypeId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysEmpTypeId.EditValueChanged
        Try
            If Me.InvokeRequired = True Then
                Me.Invoke(New HI.Delegate.Dele.FNHSysEmpTypeId_ValueChanged(AddressOf FNHSysEmpTypeId_EditValueChanged), New Object() {sender, e})
            Else
                If Me.FNHSysEmpTypeId.Text.Trim <> "" Then

                    'If Me.FNHSysEmpTypeId.Properties.Tag.ToString = "" Then

                    'End If

                    _TSQL = ""
                    _TSQL = "SELECT TOP 1 A.FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..THRMEmpType AS A (NOLOCK) WHERE A.FTEmpTypeCode = N'" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text.Trim) & "';"
                    Me.FNHSysEmpTypeId.Properties.Tag = HI.Conn.SQLConn.GetField(_TSQL, HI.Conn.DB.DataBaseName.DB_MASTER, "")

                    Dim DTWelfareByEmpType As System.Data.DataTable
                    _TSQL = ""
                    _TSQL = "SELECT TOP 1 A.FNAttendanceAllowance, A.FNCarAllowance, A.FNMealAllowance, A.FNChildCareAmt , A.FNChildCareStartAge , A.FNChildCareEndAge , A.FNChildCareMaxPeople"
                    _TSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRMCfgWelfareVN AS A (NOLOCK)"
                    _TSQL &= Environment.NewLine & "WHERE A.FNHSysEmpTypeId = " & Val(Me.FNHSysEmpTypeId.Properties.Tag) & ";"

                    DTWelfareByEmpType = HI.Conn.SQLConn.GetDataTable(_TSQL, HI.Conn.DB.DataBaseName.DB_HR)

                    If Not DBNull.Value.Equals(DTWelfareByEmpType) And DTWelfareByEmpType.Rows.Count > 0 Then
                        Dim oDataRow As System.Data.DataRow
                        For Each oDataRow In DTWelfareByEmpType.Rows
                            Me.FNAttendance.Value = Val(oDataRow!FNAttendanceAllowance)
                            Me.FNCar.Value = Val(oDataRow!FNCarAllowance)
                            Me.FNMeal.Value = Val(oDataRow!FNMealAllowance)
                            Me.FNChildCareAmt.Value = Double.Parse("0" & oDataRow!FNChildCareAmt.ToString)
                            Me.FNChildCareStartAge.Value = Integer.Parse("0" & oDataRow!FNChildCareStartAge.ToString)
                            Me.FNChildCareEndAge.Value = Integer.Parse("0" & oDataRow!FNChildCareEndAge.ToString)
                            Me.FNChildCareMaxPeople.Value = Integer.Parse("0" & oDataRow!FNChildCareMaxPeople.ToString)
                        Next
                    Else
                        Me.FNAttendance.Value = 0
                        Me.FNCar.Value = 0
                        Me.FNMeal.Value = 0
                        Me.FNChildCareAmt.Value = 0
                        Me.FNChildCareStartAge.Value = 0
                        Me.FNChildCareEndAge.Value = 0
                        Me.FNChildCareMaxPeople.Value = 0
                    End If

                    'Me.FNHSysEmpTypeId.Focus()
                End If

            End If
        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, Me.Text)
            End If
        End Try
    End Sub

    Private Sub ogvWelfareVN_Click(sender As Object, e As EventArgs) Handles ogvWelfareVN.Click
        Try
            With Me.ogvWelfareVN
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub
                Me.FNHSysEmpTypeId.Text = .GetRowCellValue(.FocusedRowHandle, "FTEmpTypeCode").ToString

            End With
        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, Me.Text)
            End If
        End Try

    End Sub

#End Region

End Class