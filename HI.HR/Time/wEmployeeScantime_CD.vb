Imports System.IO
Imports System.Windows.Forms

Public Class wEmployeeScantime_CD

#Region "Property"

    Private _CallMenuName As String = ""
    Private _CallServerParm As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

    Public Property CallTimeSeverParm As String
        Get
            Return _CallServerParm
        End Get
        Set(value As String)
            _CallServerParm = value
        End Set
    End Property

#End Region

#Region "Procedure"
    Private Sub LoadEmpData(FNHSysEmpID As String)

        Dim _PathEmpPic As String
        _PathEmpPic = ""
        Dim cmdstring As String = "Select Top 1 FTCfgData FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE FTCfgName='PathEmpPic'"

        _PathEmpPic = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "")

        Dim _dt As DataTable
        Dim _Qry As String = ""
        _Qry = " SELECT    TOP 1     M.FTEmpCode, M.FTEmpCodeRefer, M.FTEmpNameTH, M.FTEmpSurnameTH, M.FTEmpNicknameTH, M.FTEmpNameEN, M.FTEmpSurnameEN, M.FNHSysEmpTypeId, M.FNHSysDeptId, "
        _Qry &= vbCrLf & "   D.FTDeptCode, Di.FTDivisonCode, M.FNHSysDivisonId, M.FNHSysSectId, S.FTSectCode, ET.FTEmpTypeCode, M.FNHSysUnitSectId, US.FTUnitSectCode,"
        _Qry &= vbCrLf & "  M.FNHSysEmpID, M.FTEmpPicName, M.FNHSysPositId, P.FTPositCode"
        _Qry &= vbCrLf & "  FROM            THRMEmployee AS M WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId = P.FNHSysPositId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
        _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID  =" & Val(FNHSysEmpID) & " "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        FTEmpPicName.Image = Nothing
        If _dt.Rows.Count > 0 Then
            For Each R As DataRow In _dt.Rows
                FNHSysEmpID_None.Text = R!FTEmpNameEN.ToString & " " & R!FTEmpSurnameEN.ToString

                If _PathEmpPic = "" Then
                    FTEmpPicName.Image = HI.UL.ULImage.LoadImage(HI.ST.SysInfo.SysPath & "EmpPicture\" & R!FTEmpPicName.ToString)
                Else
                    FTEmpPicName.Image = HI.UL.ULImage.LoadImage(_PathEmpPic & R!FTEmpPicName.ToString)
                End If
                FNHSysEmpTypeId.Text = R!FTEmpTypeCode.ToString
                FNHSysDeptId.Text = R!FTDeptCode.ToString
                FNHSysDivisonId.Text = R!FTDivisonCode.ToString
                FNHSysSectId.Text = R!FTSectCode.ToString
                FNHSysUnitSectId.Text = R!FTUnitSectCode.ToString
                FNHSysPositId.Text = R!FTPositCode.ToString

            Next
        Else
            FNHSysEmpID_None.Text = ""
            FNHSysEmpTypeId.Text = ""
            FNHSysDeptId.Text = ""
            FNHSysDivisonId.Text = ""
            FNHSysSectId.Text = ""
            FNHSysUnitSectId.Text = ""
            FNHSysPositId.Text = ""

        End If

    End Sub

    Private Sub FNHSysEmpID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNHSysEmpID.EditValueChanged
        If FNHSysEmpID.Text <> "" Then
            ' If FNHSysEmpID.Properties.Tag.ToString = "" Then
            Dim _Qry As String = "SELECT TOP 1 FNHSysEmpID  FROM THRMEmployee WITH(NOLOCK) WHERE FTEmpCode ='" & HI.UL.ULF.rpQuoted(FNHSysEmpID.Text) & "' "
            FNHSysEmpID.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")
            'End If

            Call LoadEmpData(FNHSysEmpID.Properties.Tag.ToString)

        End If
    End Sub


    Private Sub ocmexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub


#End Region

#Region "General"
    Private Sub wChangePosition_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        FNHSysEmpID.Visible = False
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        FTBarCodeCarton.EnterMoveNextControl = False
        ReStoreDBFG()
        textinput.Focus()
        textinput.SelectAll()

        '_CallServerParm = "Y"
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub


    Private Sub Scan_KeyDown(sender As Object, e As KeyEventArgs) Handles FTBarCodeCarton.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Me.FTBarCodeCarton.Text = "" Then Exit Sub



                Me.TextBox_FNHSysEmpID.Text = FTBarCodeCarton.Text.Trim

                Me.LabelControl1.Text = HI.Conn.SQLConn.GetField("Select CONVERT(varchar(8),  GETDATE() , 114)", Conn.DB.DataBaseName.DB_SYSTEM, "")
                Me.textinput.Focus()
                Me.textinput.SelectAll()
                Dim _Cmd As String = ""
                _Cmd = "exec  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.sp_scantimeinout '" & Me.TextBox_FNHSysEmpID.Text & "'"
                If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "0") = "1" Then
                    LabelControl2.Text = "Scan Time Successfully"
                    LabelControl2.Appearance.ForeColor = Drawing.Color.Blue
                    LabelControl2.Visible = True
                    Me.textinput.Focus()
                    Me.textinput.SelectAll()
                    Try
                        Dim player As New System.Media.SoundPlayer("C:\wisdom\sound\scantimeinout.wav")
                        player.Play()
                    Catch ex As Exception

                    End Try


                Else
                    LabelControl2.Text = "Scan Time unsuccessful"
                    LabelControl2.Visible = True
                    LabelControl2.Appearance.ForeColor = Drawing.Color.Red
                    Me.textinput.Focus()
                    Me.textinput.SelectAll()

                    FNHSysEmpID_None.Text = ""
                    FNHSysEmpTypeId.Text = ""
                    FNHSysDeptId.Text = ""
                    FNHSysDivisonId.Text = ""
                    FNHSysSectId.Text = ""
                    FNHSysUnitSectId.Text = ""
                    FNHSysPositId.Text = ""
                End If


                Me.textinput.Focus()
                Me.textinput.SelectAll()
            End If


        Catch ex As Exception
        End Try
    End Sub

    Private Function ReStoreDBFG() As Boolean
        Try
            Dim _PathServer As String = "" : Dim _PathCopy As String = ""

            Dim _Cmd As String = ""
            Dim _Path As String = Application.StartupPath & "\sound\"
            Dim _PathLocal As String = "C:\wisdom\sound\"
            If Not (Directory.Exists(_PathLocal)) Then
                My.Computer.FileSystem.CreateDirectory(_PathLocal)
                Try
                    'System.IO.File.Copy(_PathCopy & "\" & foundFile.Name.ToString, _Path & "\" & foundFile.Name.ToString)
                    System.IO.File.Copy(_Path & "scantimeinout.wav", _PathLocal & "scantimeinout.wav")

                Catch ex As Exception
                    'My.Computer.FileSystem.CopyFile(_PathCopy & "\" & foundFile.Name.ToString, _Path & "\" & foundFile.Name.ToString)
                    ' My.Computer.FileSystem.CopyFile(.FTPathName.Text, _Path & "\" & foundFile.Name.ToString)
                Finally
                    '  MsgBox(_PathCopy & "\" & foundFile.Name.ToString & " TO " & _Path & "\" & foundFile.Name.ToString & " succsess", MsgBoxStyle.OkOnly)
                End Try
            Else
                System.IO.File.Copy(_Path & "scantimeinout.wav", _PathLocal & "scantimeinout.wav")
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ocmexit_Click_1(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click_1(sender As Object, e As EventArgs) Handles ocmclear.Click
        Me.FTBarCodeCarton.Text = ""
        Me.FNHSysEmpID_None.Text = ""
        Me.FNHSysEmpTypeId.Text = ""
        Me.FNHSysDeptId.Text = ""
        Me.FNHSysDivisonId.Text = ""
        Me.FNHSysSectId.Text = ""
        Me.FNHSysUnitSectId.Text = ""
        Me.FNHSysPositId.Text = ""
    End Sub

    Private _Count As Integer = 0
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick


        Dim CurrentTime As String = ""

        'If _CallServerParm = "" Then

        'End If

        CurrentTime = TimeOfDay.ToString("HH:mm:ss")
        LabelControl1.Text = CurrentTime

        If _Count >= 25 Then
            textinput.Focus()
            textinput.SelectAll()
            _Count = 0
        End If



        _Count += +1
    End Sub

    Private Sub TextBox_FNHSysEmpID_TextChanged(sender As Object, e As EventArgs) Handles TextBox_FNHSysEmpID.TextChanged
        If TextBox_FNHSysEmpID.Text <> "" Then
            ' If FNHSysEmpID.Properties.Tag.ToString = "" Then
            Dim _FNHSysEmpID As String = ""
            Dim _Qry As String = "SELECT TOP 1 FNHSysEmpID  FROM THRMEmployee WITH(NOLOCK) WHERE FTEmpCode ='" & HI.UL.ULF.rpQuoted(TextBox_FNHSysEmpID.Text) & "' "
            _FNHSysEmpID = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")
            'End If

            Call LoadEmpData(_FNHSysEmpID)

        End If
    End Sub

    Private Sub textinput_KeyDown(sender As Object, e As KeyEventArgs) Handles textinput.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Me.FTBarCodeCarton.Text = Me.textinput.Text
                _Count = 0
                Scan_KeyDown(sender, e)
                textinput.Focus()
                textinput.SelectAll()
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class