Imports System.Data.SqlClient

Public Class wCopyStyle

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(ByVal value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private Sub ocmcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmcancel.Click
        Me.ProcComplete = False
        Me.Close()
    End Sub

    Private Sub ocmok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmok.Click
        Dim ExistingStyle As String = ""
        If FNHSysStyleId2.Text = "" Or FNHSysStyleId.Text = "" Then
            Exit Sub
        End If

        Dim _Str As String = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle WITH(NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId2.Text) & "' "
        ExistingStyle = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")

        If ExistingStyle.Trim() <> "" Then
            If HI.ST.Lang.Language = ST.Lang.Lang.TH Then
                If MsgBox("StyleNo " & FNHSysStyleId2.Text & " มีอยู่แล้ว คุณต้องการเขียนทับ Style No นี้ใช่หรือไม่?", MsgBoxStyle.YesNo, "ยืนยันการเขียนทับ") = vbNo Then Exit Sub
            Else
                If MsgBox("StyleNo " & FNHSysStyleId2.Text & " is existing, Do you want to overwrite this StyleNo?", MsgBoxStyle.YesNo, "Comfirmation Orverwrite") = vbNo Then Exit Sub
            End If

        End If

        If Me.ProcessCopy(FNHSysStyleId.Properties.Tag, FNHSysStyleId2.Properties.Tag) Then
            Me.ProcComplete = True
            'HI.MG.ShowMsg.mInfo("Copy Style completed.", 1, "Copy Style", "")
            If HI.ST.Lang.Language = ST.Lang.Lang.TH Then
                MsgBox("คัดลอกสำเร็จ", vbOKOnly, "คัดลอก Style")
            Else
                MsgBox("Copy Style completed", vbOKOnly, "Copy Style")
            End If

            Me.Close()
        Else
            If HI.ST.Lang.Language = ST.Lang.Lang.TH Then
                MsgBox("ไม่สามารถคัดลอกข้อมูลได้", vbOKOnly, "คัดลอก Style")
            Else
                MsgBox("Cannot Copy StyleNo", vbOKOnly, "Copy Style")
            End If

        End If
    End Sub

    Private Function ProcessCopy(ByVal _FNHSysStyleId1 As String, ByVal _FNHSysStyleId2 As String) As Boolean
        Dim ret As Boolean = True
        Dim retStr As String = ""
        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Try
            Dim sqlCmd As New SqlCommand
            sqlCmd.Connection = HI.Conn.SQLConn.Cnn
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_COPYSTYLE]"
            sqlCmd.Parameters.AddWithValue("@FNHSysStyleId1", _FNHSysStyleId1)
            sqlCmd.Parameters.AddWithValue("@FNHSysStyleId2", _FNHSysStyleId2)
            sqlCmd.Parameters.AddWithValue("@UserName", HI.ST.UserInfo.UserName)

            'Dim sqlDA As New SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
            'sqlDA.SelectCommand = sqlCmd
            ret = sqlCmd.ExecuteScalar()

        Catch ex As Exception
            ret = False
        End Try

        Return ret
    End Function


    Private Sub FNHSysStyleId2_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysStyleId2.EditValueChanged
        If FNHSysStyleId2.Text <> "" Then
            Dim _Str As String = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId2.Text) & "' "
            FNHSysStyleId2.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
        End If
    End Sub
End Class