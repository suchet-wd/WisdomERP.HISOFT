Option Explicit On
Option Strict Off

Imports System
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic
Imports Microsoft.VisualBasic

Public Class wInsuranceRate

#Region "Variable Declaration"
    Private tSql As String
    Private bProcLoad As Boolean = False
#End Region

#Region "Procedure And Function"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Call PROC_INITIAL_GRIDVIEW()

    End Sub

    Private Sub PROC_INITIAL_GRIDVIEW()
        With Me.ogvInsuranceRate
            oColFNEmployeeRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            oColFNEmployeeRate.DisplayFormat.FormatString = "{0:n" & HI.ST.Config.AmtDigit & "}"
            oColFNEmployerRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            oColFNEmployerRate.DisplayFormat.FormatString = "{0:n" & HI.ST.Config.AmtDigit & "}"
        End With
    End Sub

    Private Sub PROC_SAVE() Handles ocmSave.Click
        'If Not PROC_VALIDATEDATA() Then Exit Sub
        Dim oStrBuilder As New System.Text.StringBuilder()

        oStrBuilder.Remove(0, oStrBuilder.Length)

        oStrBuilder.AppendLine("UPDATE A")
        oStrBuilder.AppendLine("SET    A.[FTUpdUser] = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',")
        oStrBuilder.AppendLine(String.Format("       A.[FDUpdDate] = {0},", HI.UL.ULDate.FormatDateDB))
        oStrBuilder.AppendLine(String.Format("       A.[FTUpdTime] = {0},", HI.UL.ULDate.FormatTimeDB))
        oStrBuilder.AppendLine(String.Format("       A.[FNEmployeeRate] = {0},", Val(Me.FNEmployeeRate.Value)))
        oStrBuilder.AppendLine(String.Format("       A.[FNEmployerRate] = {0},", Val(Me.FNEmployerRate.Value)))
        oStrBuilder.AppendLine("       A.[FTStateActive] = '" & IIf(Me.FTStateActive.Checked = True, "1", "0") & "'")
        oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..[THRMInsuranceVN] AS A")
        oStrBuilder.AppendLine("WHERE A.FNInsuranceVN = " & HI.TL.CboList.GetListValue("FNInsuranceVN", Me.FNInsuranceVN.SelectedIndex))

        tSql = ""
        tSql = oStrBuilder.ToString()

        If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_HR) = False Then

            Dim FNHSysInsuranceId As Integer

            FNHSysInsuranceId = HI.TL.RunID.GetRunNoID("THRMInsuranceVN", "FNHSysInsuranceId", Conn.DB.DataBaseName.DB_HR)

            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRMInsuranceVN]([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser],[FDUpdDate],[FTUpdTime],")
            oStrBuilder.AppendLine("                  [FNHSysInsuranceId],[FNInsuranceVN],[FNEmployeeRate],[FNEmployerRate],[FTStateActive])")
            oStrBuilder.AppendLine("VALUES (N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",")
            oStrBuilder.AppendLine("        N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatTimeDB & "," & HI.UL.ULDate.FormatTimeDB & ",")
            oStrBuilder.AppendLine(String.Format("        {0},", FNHSysInsuranceId))
            oStrBuilder.AppendLine("        " & HI.TL.CboList.GetListValue("FNInsuranceVN", Me.FNInsuranceVN.SelectedIndex) & "," & Val(Me.FNEmployeeRate.Value) & "," & Val(Me.FNEmployerRate.Value) & ",")
            oStrBuilder.AppendLine("        N'" & IIf(Me.FTStateActive.Checked = True, "1", "0") & "')")

            tSql = ""
            tSql = oStrBuilder.ToString()

            HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_HR)

        End If

        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        Call PROC_SHOWBROWSEDATA()
        Me.FNInsuranceVN.Focus()

    End Sub

    Private Sub PROC_CLEAR() Handles ocmclearclsr.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub PROC_REFRESH() Handles ocmRefresh.Click
        Call PROC_SHOWBROWSEDATA()
    End Sub

    Private Sub PROC_EXIT() Handles ocmExit.Click
        Me.Close()
    End Sub

    Private Function PROC_VALIDATEDATA() As Boolean
        Dim bPass As Boolean = False
        Try

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly, Me.Text)
            End If
        End Try

        Return bPass

    End Function

    Private Function PROC_LOADDATA(ByVal pnListIndex As Integer) As Boolean
        Dim bPass As Boolean = False
        Try
            Dim oDBdt As System.Data.DataTable
            Dim oStrBuilder As New System.Text.StringBuilder()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("SELECT A.FNEmployeeRate, A.FNEmployerRate, ISNULL(A.FTStateActive, '0') AS FTStateActive")
            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRMInsuranceVN AS A (NOLOCK) INNER JOIN (SELECT L1.*")
            oStrBuilder.AppendLine("                                                                                                                    FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData AS L1 (NOLOCK)")
            oStrBuilder.AppendLine("                                                                                                                    WHERE L1.FTListName = N'FNInsuranceVN') AS B ON A.FNInsuranceVN = B.FNListIndex")
            oStrBuilder.AppendLine("WHERE A.FNInsuranceVN = " & HI.TL.CboList.GetListValue("FNInsuranceVN", pnListIndex) & ";")

            tSql = ""
            tSql = oStrBuilder.ToString()

            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_HR)

            Me.FNEmployeeRate.Value = 0
            Me.FNEmployerRate.Value = 0
            Me.FTStateActive.Checked = False

            For Each oDataRow As System.Data.DataRow In oDBdt.Rows
                Me.FNEmployeeRate.Value = oDataRow!FNEmployeeRate
                Me.FNEmployerRate.Value = oDataRow!FNEmployerRate
                Me.FTStateActive.Checked = oDataRow!FTStateActive
            Next

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message.ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly, Me.Text)
            End If
        End Try

        Return bPass

    End Function

    Private Function PROC_SHOWBROWSEDATA() As Boolean
        Dim bPass As Boolean = False
        Dim oDBdtInsuranceRate As System.Data.DataTable
        Try
            Dim oStrBuilder As New System.Text.StringBuilder()

            oStrBuilder.Remove(0, oStrBuilder.Length)
            oStrBuilder.AppendLine("DECLARE @FNLang AS INT;")
            oStrBuilder.AppendLine("SET @FNLang = " & HI.ST.Lang.Language & ";")
            oStrBuilder.AppendLine("SELECT A.FNHSysInsuranceId, B.FNListIndex AS FNInsuranceVN, CASE WHEN @FNLang = 1 THEN B.FTNameEN ELSE B.FTNameTH END AS FTInsuranceDesc,")
            oStrBuilder.AppendLine("       A.FNEmployeeRate, A.FNEmployerRate, ISNULL(A.FTStateActive, '0') AS FTStateActive")
            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRMInsuranceVN AS A (NOLOCK) INNER JOIN (SELECT L1.*")
            oStrBuilder.AppendLine("                                                                                                                    FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "]..HSysListData AS L1 (NOLOCK)")
            oStrBuilder.AppendLine("                                                                                                                    WHERE L1.FTListName = N'FNInsuranceVN') AS B ON A.FNInsuranceVN = B.FNListIndex")
            oStrBuilder.AppendLine("ORDER BY B.FNListIndex ASC;")

            If oStrBuilder.Length > 0 Then
                tSql = ""
                tSql = oStrBuilder.ToString()
                oDBdtInsuranceRate = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_HR)
                Me.ogdInsuranceRate.DataSource = oDBdtInsuranceRate
            Else
                Me.ogdInsuranceRate.DataSource = Nothing
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message.ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly, Me.Text)
            End If
        End Try

        Return bPass
    End Function

#End Region

#Region "Event Handle"
    Private Sub FNInsuranceVN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNInsuranceVN.SelectedIndexChanged
        Try
            If bProcLoad = True Then
                If Me.FNInsuranceVN.SelectedIndex > -1 Then
                    Call PROC_LOADDATA(Me.FNInsuranceVN.SelectedIndex)
                End If
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.OkOnly, Me.Text)
            End If
        End Try
    End Sub

    Private Sub FNEmployerRate_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles FNEmployerRate.KeyPress
        Try
            If Asc(e.KeyChar) = 13 Then
                Me.ocmSave.PerformClick()
            End If
        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly, Me.Text)
            End If
        End Try
    End Sub

    Private Sub ogvInsuranceRate_Click(sender As Object, e As EventArgs) Handles ogvInsuranceRate.Click
        With Me.ogvInsuranceRate
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub
            Me.FNInsuranceVN.SelectedIndex = HI.TL.CboList.GetIndexByValue("FNInsuranceVN", .GetRowCellValue(.FocusedRowHandle, "FNInsuranceVN").ToString)
            Me.FNEmployeeRate.Focus()
        End With
    End Sub

    Private Sub wInsuranceRate_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            bProcLoad = True
            Call PROC_LOADDATA(0)
            Call PROC_SHOWBROWSEDATA()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmSave_Click(sender As Object, e As EventArgs) Handles ocmSave.Click
    End Sub

    Private Sub ocmclearclsr_Click(sender As Object, e As EventArgs) Handles ocmclearclsr.Click
    End Sub

    Private Sub ocmRefresh_Click(sender As Object, e As EventArgs) Handles ocmRefresh.Click
    End Sub

    Private Sub ocmExit_Click(sender As Object, e As EventArgs) Handles ocmExit.Click
    End Sub

#End Region

End Class