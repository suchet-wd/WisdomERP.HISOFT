Imports System.Data.SqlClient

Public Class wCreateBomDev

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
        'If (HI.ST.UserInfo.UserName.ToUpper = FTUpdUser.ToUpper) Or (HI.ST.SysInfo.Admin) Or FTUpdUser.ToUpper = "" Then
        '    Return True
        'Else

        'Dim _Qry As String = ""
        'Dim _Qry2 As String = ""
        '_Qry = "SELECT TOP 1  FNHSysMerTeamId  "
        '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
        '_Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTUpdUser) & "' "

        '_Qry2 = "SELECT TOP 1  FNHSysMerTeamId  "
        '_Qry2 &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
        '_Qry2 &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "

        'If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "") = HI.Conn.SQLConn.GetField(_Qry2, Conn.DB.DataBaseName.DB_SECURITY, "") Then
        '    Return True
        'Else
        '    HI.MG.ShowMsg.mProcessError(1411200101, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข Style นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
        '    Return False
        'End If

        'End If
    End Function

    Private Sub ocmok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmok.Click
        If Verify() Then
            Me.ProcComplete = True
            Me.Close()

        End If

        'Dim ExistingStyle As String = ""

        'If FNHSysStyleDevId.Text = "" Or FNHSysSeasonIdF.Text = "" Or FNHSysStyleIdF.Text = "" Or FTSeason.Text = "" Then
        '    Exit Sub
        'End If

        'Dim _Str As String
        'Dim _FTUpdUser As String = ""

        '_Str = "SELECT TOP 1"
        '_Str &= vbCrLf & "  	ISNULL(FTUpdUser,FTInsUser) AS FTUpdUser"
        '_Str &= vbCrLf & "     FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle AS X WITH(NOLOCK)"
        '_Str &= vbCrLf & " WHERE (X.FNHSysStyleDevId  =" & Val(FNHSysStyleDevId.Properties.Tag.ToString) & ")"

        '_Str &= vbCrLf & "   ORDER BY ISNULL(FDUpdDate,FDInsDate) DESC,ISNULL(FTUpdTime,FTInsTime) DESC"


        '_FTUpdUser = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
        'If CheckOwner(_FTUpdUser) = False Then
        '    Exit Sub
        'End If

        '_Str = "SELECT TOP 1 FNHSysStyleDevId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat WITH(NOLOCK) WHERE FNHSysStyleDevId =" & Val(FNHSysStyleDevId.Properties.Tag.ToString) & ""
        'ExistingStyle = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")

        'If ExistingStyle.Trim() <> "" Then

        '    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        '        If MsgBox("StyleNo " & FNHSysStyleDevId.Text & " มีอยู่แล้ว คุณต้องการเขียนทับ Style No นี้ใช่หรือไม่?", MsgBoxStyle.YesNo, "ยืนยันการเขียนทับ") = vbNo Then Exit Sub
        '    Else
        '        If MsgBox("StyleNo " & FNHSysStyleDevId.Text & " is existing, Do you want to overwrite this StyleNo?", MsgBoxStyle.YesNo, "Comfirmation Orverwrite") = vbNo Then Exit Sub
        '    End If

        'End If

        'If Me.ProcessCopy(FNHSysStyleIdF.Properties.Tag, FNHSysStyleDevId.Properties.Tag, FNHSysStyleDevId.Text, FTSeason.Text) Then
        '    Me.ProcComplete = True
        '    'HI.MG.ShowMsg.mInfo("Copy Style completed.", 1, "Copy Style", "")

        '    If FTStateMergeData.Checked Then
        '        HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Copy not Delete Destination  (Style " & FNHSysStyleDevId.Text & " Season " & FTSeason.Text & " ")
        '    Else
        '        HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Copy And Delete Destination  (Style " & FNHSysStyleDevId.Text & " Season " & FTSeason.Text & "  ")
        '    End If

        '    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        '        MsgBox("คัดลอกสำเร็จ", vbOKOnly, "คัดลอก Style")
        '    Else
        '        MsgBox("Copy Style completed", vbOKOnly, "Copy Style")
        '    End If

        '    Me.Close()
        'Else
        '    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
        '        MsgBox("ไม่สามารถคัดลอกข้อมูลได้", vbOKOnly, "คัดลอก Style")
        '    Else
        '        MsgBox("Cannot Copy StyleNo", vbOKOnly, "Copy Style")
        '    End If

        'End If
    End Sub

    Private Function Verify() As Boolean
        If (FTStyle.Text = "") Then
            FTStyle.Focus()
            HI.MG.ShowMsg.mProcessError(1411200101, "กรุณาใส่ข้อมูล Style ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        Else
            If (FTStyleDetail.Text = "") Then
                FTStyleDetail.Focus()
                HI.MG.ShowMsg.mProcessError(1411200101, "กรุณาใส่ข้อมูลรายละเอียด Style ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Return False
            Else
                If (FTSeason.Text = "") Then
                    FTSeason.Focus()
                    HI.MG.ShowMsg.mProcessError(1411200101, "กรุณาเลือก BOM Type ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                    Return False
                Else
                    If (FNBomDevType.Text = "") Then
                        FNBomDevType.Focus()
                        HI.MG.ShowMsg.mProcessError(1411200101, "กรุณาเลือก BOM Type ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                        Return False
                    Else
                        'If (FTVersion.Text = "") Then
                        '    HI.MG.ShowMsg.mProcessError(1411200101, "กรุณาเลือก BOM Type ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                        '    Return False
                        'Else
                        Return True
                        'End If
                    End If
                End If
            End If
        End If
    End Function

    Private Sub ocmClear_Click(sender As Object, e As EventArgs) Handles ocmClear.Click
        ClearForm()
    End Sub

    Private Sub wCreateBomDev_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClearForm()
    End Sub
    Private Function ClearForm()
        FTSeason.Text = ""
        FTStyle.Text = ""
        FTStyleDetail.Text = ""
        FNBomDevType.Text = ""
    End Function

    Private Sub FTStyle_Leave(sender As Object, e As EventArgs) Handles FTStyle.Leave
        If FTStyle.Text <> "" Then
            Dim _Qry As String = ""
            _Qry = "SELECT ISNULL(s.FTStyleNameEN,'')  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS s WITH (NOLOCK) "
            _Qry &= vbCrLf & "   WHERE s.FTStyleCode = '" & HI.UL.ULF.rpQuoted(FTStyle.Text) & "' "

            FTStyleDetail.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")

        End If
    End Sub
End Class