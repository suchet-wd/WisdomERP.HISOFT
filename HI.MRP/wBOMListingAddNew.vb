Public Class wBOMListingAddNew

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

        If FNHSysStyleId2.Text = "" Or FNHSysSeasonId.Text = "" Or FNHSysSeasonId.Properties.Tag.ToString = "" Then
            Exit Sub
        End If


        Me.ProcComplete = True
        Me.Close()

    End Sub

    Private Function ProcessCopy(ByVal _FNHSysStyleId1 As String, ByVal _FNHSysStyleId2 As String, _FNHSysSeasonId1 As String, _FNHSysSeasonId2 As String) As Boolean
        Dim ret As Boolean = True
        Dim retStr As String = ""


        Try




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

    Private Sub GroupControl1_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles GroupControl1.Paint

    End Sub

    Private Sub wBOMListingAddNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class