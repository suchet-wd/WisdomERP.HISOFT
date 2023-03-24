Imports System.Data.SqlClient

Public Class wCopyPacking

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

    Private Function CheckOwner(FTUpdUser As String) As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = FTUpdUser.ToUpper) Or (HI.ST.SysInfo.Admin) Or FTUpdUser.ToUpper = "" Then
            Return True
        Else

            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            _Qry = "SELECT TOP 1  FNHSysMerTeamId  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTUpdUser) & "' "

            _Qry2 = "SELECT TOP 1  FNHSysMerTeamId  "
            _Qry2 &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry2 &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "") = HI.Conn.SQLConn.GetField(_Qry2, Conn.DB.DataBaseName.DB_SECURITY, "") Then
                Return True
            Else
                HI.MG.ShowMsg.mProcessError(1411200101, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข Style นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Return False
            End If

        End If
    End Function

    Private Sub ocmok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmok.Click
        Dim ExistingStyle As String = ""

        If FNHSysStyleId2.Text = "" Or FNHSysSeasonIdS.Text = "" Or FNHSysStyleId.Text = "" Or FNHSysSeasonId.Text = "" Or FNHSysSeasonId.Properties.Tag.ToString = "" Or FNHSysSeasonIdS.Properties.Tag.ToString = "" Then
            Exit Sub
        End If

        Dim _Str As String
        Dim _FTUpdUser As String = ""

        _Str = "SELECT TOP 1 T1.FTUpdUser "
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS MS WITH(NOLOCK)  "
        _Str &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle AS T1 WITH(NOLOCK) ON MS.FNHSysStyleId = T1.FNHSysStyleId"
        _Str &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason T7 WITH(NOLOCK) ON T1.FNHSysSeasonId = T7.FNHSysSeasonId"
        _Str &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer T8 WITH(NOLOCK) ON T1.FNHSysCustId = T8.FNHSysCustId"
        _Str &= vbCrLf & " WHERE(MS.FNHSysStyleId  =" & Val(FNHSysStyleId2.Properties.Tag.ToString) & ")"
        _Str &= vbCrLf & " ORDER BY MS.FNHSysStyleId"

        _FTUpdUser = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
        If CheckOwner(_FTUpdUser) = False Then
            Exit Sub
        End If
        Dim _style As String = HI.Conn.SQLConn.GetField("SELECT FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WHERE FTStyleCode='" & Me.FNHSysStyleId2.Text & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
        _Str = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Packing WITH(NOLOCK) WHERE FNHSysStyleId ='" & _style & "' "
        ExistingStyle = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")

        If ExistingStyle.Trim() <> "" Then

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                If MsgBox("StyleNo " & FNHSysStyleId2.Text & " มีอยู่แล้ว คุณต้องการเขียนทับ Style No นี้ใช่หรือไม่?", MsgBoxStyle.YesNo, "ยืนยันการเขียนทับ") = vbNo Then Exit Sub
            Else
                If MsgBox("StyleNo " & FNHSysStyleId2.Text & " is existing, Do you want to overwrite this StyleNo?", MsgBoxStyle.YesNo, "Comfirmation Orverwrite") = vbNo Then Exit Sub
            End If

        End If

        If Me.ProcessCopy(FNHSysStyleId.Properties.Tag, FNHSysStyleId2.Properties.Tag, FNHSysSeasonIdS.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString) Then
            Me.ProcComplete = True
            'HI.MG.ShowMsg.mInfo("Copy Style completed.", 1, "Copy Style", "")

            If FTStateMergeData.Checked Then
                HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Copy not Delete Destination  (Style " & FNHSysStyleId.Text & " Season " & FNHSysSeasonId.Text & " ")

            Else
                HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Copy And Delete Destination  (Style " & FNHSysStyleId.Text & " Season " & FNHSysSeasonId.Text & "  ")

            End If

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                MsgBox("คัดลอกสำเร็จ", vbOKOnly, "คัดลอก Style")
            Else
                MsgBox("Copy Style completed", vbOKOnly, "Copy Style")

            End If

            Me.Close()
        Else
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                MsgBox("ไม่สามารถคัดลอกข้อมูลได้", vbOKOnly, "คัดลอก Style")
            Else
                MsgBox("Cannot Copy StyleNo", vbOKOnly, "Copy Style")
            End If

        End If
    End Sub

    Private Function ProcessCopy(ByVal _FNHSysStyleId1 As String, ByVal _FNHSysStyleId2 As String, _FNHSysSeasonId1 As String, _FNHSysSeasonId2 As String) As Boolean
        Dim ret As Boolean = True
        Dim retStr As String = ""
        'HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        'HI.Conn.SQLConn.SqlConnectionOpen()
        'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Try

            'Dim sqlCmd As New SqlCommand
            'sqlCmd.Connection = HI.Conn.SQLConn.Cnn
            'sqlCmd.CommandType = CommandType.StoredProcedure
            'sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_COPYSTYLE_NEW_BOMSHEET]"
            'sqlCmd.Parameters.AddWithValue("@FNHSysStyleId1", _FNHSysStyleId1)
            'sqlCmd.Parameters.AddWithValue("@FNHSysStyleId2", _FNHSysStyleId2)
            'sqlCmd.Parameters.AddWithValue("@FNHSysSeasonId1", _FNHSysSeasonId1)
            'sqlCmd.Parameters.AddWithValue("@FNHSysSeasonId2", _FNHSysSeasonId2)
            'sqlCmd.Parameters.AddWithValue("@UserName", HI.ST.UserInfo.UserName)
            'sqlCmd.Parameters.AddWithValue("@FTStateMergeData", FTStateMergeData.EditValue.ToString())

            Dim _Qry As String = ""
            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_COPYSTYLE_NEW_BOMSHEET] " & _FNHSysStyleId1 & "," & _FNHSysStyleId2 & "," & _FNHSysSeasonId1 & "," & _FNHSysSeasonId2 & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & FTStateMergeData.EditValue.ToString() & "'"
            'Dim sqlDA As New SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
            'sqlDA.SelectCommand = sqlCmd
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
            ' If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN) = True Then
            ret = True
            'Else
            '    ret = False
            'End If
            ' ret = sqlCmd.ExecuteScalar()

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


    Private Sub wCopyPacking_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class