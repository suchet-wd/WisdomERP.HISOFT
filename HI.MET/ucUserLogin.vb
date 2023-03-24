Imports HI.ST
Imports DevExpress.XtraBars.Docking2010.Views.WindowsUI
Imports System.Windows.Forms

Public Class ucUserLogin
    Inherits UserControl

    Private _ProcNew As Boolean = False
    Private view As WindowsUIView
    Private flyout As Flyout

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ProcNew = True

        Call ProcLoadLang()
        Call ProcLoadCmp()

        FNLang.SelectedIndex = (HI.ST.Lang.Language - 1)
        _ProcNew = False
    End Sub


    Private Sub ProcLoadLang()

        Dim _Strsql As String
        Dim _Dt As DataTable
        _Strsql = " SELECT     FTListName, FNListIndex, FTNameTH, FTNameEN"
        _Strsql &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK) "
        _Strsql &= vbCrLf & " WHERE FTListName='FNLang'  ORDER BY FTListName, FNListIndex "

        _Dt = HI.Conn.SQLConn.GetDataTable(_Strsql, Conn.DB.DataBaseName.DB_SYSTEM)
        With Me.FNLang
            .Properties.Items.Clear()
            Dim _ImgIndex As Integer = 0
            Dim _arr(4) As String

            For Each R As DataRow In _Dt.Rows

                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    _arr(_ImgIndex) = R!FTNameTH.ToString
                Else
                    _arr(_ImgIndex) = R!FTNameEN.ToString
                End If

                _ImgIndex = _ImgIndex + 1

            Next

            .Properties.Items.AddRange({New DevExpress.XtraEditors.Controls.ImageComboBoxItem(_arr(0), 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(_arr(1), 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(_arr(2), 2), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(_arr(3), 3)})
            .SelectedIndex = (HI.ST.Lang.Language - 1)

        End With

    End Sub


    Private _ListCmp As New List(Of ComBoList)()
    Private Sub ProcLoadCmp()
        _ListCmp.Clear()

        Dim _TmpStrTH As String = ""
        Dim _TmpStrEN As String = ""
        Dim _TmpStrValue As String = ""
        Dim _TmpStrRefer As String = ""

        Dim _Strsql As String
        Dim _Dt As DataTable
        _Strsql = " SELECT    FNHSysCmpId, FTCmpCode,FTDocRun "
        _Strsql &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK) "
        _Strsql &= vbCrLf & " Order By FTCmpCode"

        _Dt = HI.Conn.SQLConn.GetDataTable(_Strsql, Conn.DB.DataBaseName.DB_SYSTEM)
        With Me.FNHSysCmpId
            .Properties.Items.Clear()

            _TmpStrTH = "" : _TmpStrEN = "" : _TmpStrValue = "" : _TmpStrRefer = ""

            For Each Row In _Dt.Select
                If _TmpStrTH = "" Then
                    _TmpStrTH = Row!FTCmpCode.ToString
                    _TmpStrEN = Row!FTCmpCode.ToString
                    _TmpStrValue = Row!FNHSysCmpId.ToString
                    _TmpStrRefer = Row!FTDocRun.ToString
                Else
                    _TmpStrTH = _TmpStrTH & "|" & Row!FTCmpCode.ToString
                    _TmpStrEN = _TmpStrEN & "|" & Row!FTCmpCode.ToString
                    _TmpStrValue = _TmpStrValue & "|" & Row!FNHSysCmpId.ToString
                    _TmpStrRefer = _TmpStrRefer & "|" & Row!FTDocRun.ToString
                End If
            Next

            Dim M As New ComBoList
            M.ListName = "Cmp"
            M.ListEN = _TmpStrEN.Split("|")
            M.ListTH = _TmpStrTH.Split("|")
            M.ListValue = _TmpStrValue.Split("|")
            M.ListRefer = _TmpStrRefer.Split("|")

            _ListCmp.Add(M)

            .Properties.Items.AddRange(_ListCmp.Item(0).ListTH)

            If HI.ST.SysInfo.CmpDefualtCode <> "" Then
                Try
                    .Text = HI.ST.SysInfo.CmpDefualtCode
                Catch ex As Exception
                    .SelectedIndex = -1
                End Try
            Else
                Try
                    .SelectedIndex = 0
                Catch ex As Exception
                    .SelectedIndex = -1
                End Try
            End If


        End With
    End Sub

    Private Function GetListValue(ByVal Index As Integer) As String
        Dim Str As String = ""
        Try
            Str = _ListCmp.Item(0).ListValue(Index)
            Return Str
        Catch ex As Exception
        End Try
        If Str = "" Then Str = Index.ToString
        Return Str
    End Function

    Private Function GetListRefer(ByVal Index As Integer) As String
        Dim Str As String = ""
        Try
            Str = _ListCmp.Item(0).ListRefer(Index)
            Return Str
        Catch ex As Exception
        End Try
        If Str = "" Then Str = Index.ToString
        Return Str
    End Function


    Private Sub otbPassword_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles otbPassword.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Enter
                If otbLogin.Text.Trim = "" Then
                    Beep()
                    otbLogin.Focus()
                    Exit Sub
                End If

                If otbPassword.Text.Trim = "" Then
                    Beep()
                    otbPassword.Focus()
                    Exit Sub
                End If

                If Login() Then
                    view.HideFlyout()
                End If

            Case System.Windows.Forms.Keys.Escape
                view.HideFlyout()
        End Select
    End Sub

    Private Sub wLogin_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Enter
                If otbLogin.Text.Trim = "" Then
                    Beep()
                    otbLogin.Focus()
                    Exit Sub
                End If

                If otbPassword.Text.Trim = "" Then
                    Beep()
                    otbPassword.Focus()
                    Exit Sub
                End If

                If Login() Then
                    view.HideFlyout()
                End If

            Case System.Windows.Forms.Keys.Escape
                view.HideFlyout()
        End Select
    End Sub

    Private Sub FNLang_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FNLang.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Enter
                If otbLogin.Text.Trim = "" Then
                    Beep()
                    otbLogin.Focus()
                    Exit Sub
                End If

                If otbPassword.Text.Trim = "" Then
                    Beep()
                    otbPassword.Focus()
                    Exit Sub
                End If

                If Login() Then
                    view.HideFlyout()
                End If

            Case System.Windows.Forms.Keys.Escape
                view.HideFlyout()
        End Select
    End Sub

    Private Sub FNLang_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNLang.SelectedIndexChanged

        If Not (_ProcNew) Then
            HI.ST.Lang.Language = (FNLang.SelectedIndex + 1)

            If otbLogin.Text.Trim = "" Then
                Beep()
                otbLogin.Focus()
                Exit Sub
            End If

            If otbPassword.Text.Trim = "" Then
                Beep()
                otbPassword.Focus()
                Exit Sub
            End If

            HI.UL.AppRegistry.WriteRegistry(UL.AppRegistry.KeyName.Language, HI.ST.Lang.Language)

        End If
    End Sub

    Private Sub otbLogin_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles otbLogin.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Enter
                If otbLogin.Text.Trim = "" Then
                    Beep()
                    otbLogin.Focus()
                    Exit Sub
                End If

                If otbPassword.Text.Trim = "" Then
                    Beep()
                    otbPassword.Focus()
                    Exit Sub
                End If

                If Login() Then
                    view.HideFlyout()
                End If

            Case System.Windows.Forms.Keys.Escape
                view.HideFlyout()
        End Select
    End Sub

    Public Sub OnNavigatedFrom(args As INavigationArgs)
    End Sub

    Public Sub OnNavigatedTo(args As INavigationArgs)
        view = args.View
        flyout = TryCast(args.Target, Flyout)
    End Sub

    Private Class ComBoList
        Private _ListName As String
        Public Property ListName() As String
            Get
                Return _ListName
            End Get
            Set(ByVal value As String)
                _ListName = value
            End Set
        End Property

        Private _ListEN As String()
        Public Property ListEN() As String()
            Get
                Return _ListEN
            End Get
            Set(ByVal value As String())
                _ListEN = value
            End Set
        End Property

        Private _ListTH As String()
        Public Property ListTH() As String()
            Get
                Return _ListTH
            End Get
            Set(ByVal value As String())
                _ListTH = value
            End Set
        End Property

        Private _ListValue As String()
        Public Property ListValue() As String()
            Get
                Return _ListValue
            End Get

            Set(ByVal value As String())
                _ListValue = value
            End Set
        End Property

        Private _ListRef As String()
        Public Property ListRefer() As String()
            Get
                Return _ListRef
            End Get

            Set(ByVal value As String())
                _ListRef = value
            End Set
        End Property
    End Class

    Private Sub FNHSysCmpId_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysCmpId.SelectedIndexChanged

        Try

            HI.ST.SysInfo.CmpCode = FNHSysCmpId.Text
            HI.ST.SysInfo.CmpID = GetListValue(FNHSysCmpId.SelectedIndex)
            HI.ST.SysInfo.CmpRunID = GetListRefer(FNHSysCmpId.SelectedIndex)

        Catch ex As Exception
        End Try

    End Sub

    Private _UserName As String = ""
    Private _UserPassword As String = ""

    Public Function Login() As Boolean

        Dim _Confirm As Boolean = False

        Try

            HI.ST.UserInfo.UserName = otbLogin.Text
            HI.ST.UserInfo.UserPassword = otbPassword.Text

            Dim _dttmp As DataTable
            Dim _Str As String = ""
            _Confirm = VerifyLogin()
            If _Confirm = False Then
                Return False
            End If

            If Not (_Confirm) Then

                HI.ST.UserInfo.UserName = ""
                HI.ST.UserInfo.UserPassword = ""

            Else

                HI.ST.UserInfo.UserName = _UserName
                HI.ST.UserInfo.UserLogInComputer = System.Environment.MachineName
                HI.ST.UserInfo.UserLogInComputerIP = GetIP(HI.ST.UserInfo.UserLogInComputer)

                Dim _Date As DateTime = HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM)
                HI.ST.UserInfo.LogINDate = Format(_Date, "dd/MM/yyyy")
                HI.ST.UserInfo.LogINTime = Format(_Date, "HH:mm:ss")

                If Not (HI.ST.SysInfo.Admin) Then

                    _Str = "SELECT   TOP 1  FTUserName, FTLogInIP, FTLogInDate, FTLogInTime, FTLogInCom"
                    _Str &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginState "
                    _Str &= vbCrLf & " WHERE  FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

                    _dttmp = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)
                    If _dttmp.Rows.Count > 0 Then

                        Dim _msg As String = "User ared Connected"
                        _msg &= vbCrLf & "User Connect By IP : " & _dttmp.Rows(0)!FTLogInIP.ToString
                        _msg &= vbCrLf & "User Connect By Computername : " & _dttmp.Rows(0)!FTLogInCom.ToString
                        _msg &= vbCrLf & " "
                        _msg &= vbCrLf & "Yes to Connect By this Computer And Cancel previously active. "
                        _msg &= vbCrLf & "Cancel for previously active. "

                        If System.Windows.Forms.MessageBox.Show(_msg, "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                            _Confirm = False
                        Else

                            _Str = "Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginState SET "
                            _Str &= vbCrLf & "   FTLogInIP='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserLogInComputerIP) & "' "
                            _Str &= vbCrLf & ", FTLogInDate=" & HI.UL.ULDate.FormatDateDB
                            _Str &= vbCrLf & ", FTLogInTime=" & HI.UL.ULDate.FormatTimeDB
                            _Str &= vbCrLf & ", FTLogInCom='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserLogInComputer) & "' "
                            _Str &= vbCrLf & " WHERE  FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

                        End If

                    Else

                        _Str = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginState (FTUserName, FTLogInIP, FTLogInDate, FTLogInTime, FTLogInCom) "
                        _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserLogInComputerIP) & "'"
                        _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                        _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                        _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserLogInComputer) & "'"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

                    End If
                End If

                _Str = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginLogOutHistory (FTUserName, FTIP, FTDate, FTTime, FTCom,FTStateStatus) "
                _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserLogInComputerIP) & "'"
                _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserLogInComputer) & "','0'"

                HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

                Return True

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return _Confirm
    End Function

    Private Function GetIP(ByVal strHostName As String) As String
        Try
            Dim _GetIPv4Address As String = ""
            Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)
            For Each ipheal As System.Net.IPAddress In iphe.AddressList
                If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                    _GetIPv4Address = ipheal.ToString()
                    Exit For
                End If
            Next

            Return _GetIPv4Address

        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function VerifyLogin() As Boolean

        Dim _Verify As Boolean = False
        Try
            Dim _Sql As String
            _Sql = "SELECT TOP 1  *  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin "
            _Sql &= vbCrLf & " WHERE FTUserName='" & HI.ST.UserInfo.UserName & "'"

            Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Sql, Conn.DB.DataBaseName.DB_SECURITY)

            If _dt.Rows.Count > 0 Then
                For Each R As DataRow In _dt.Rows
                    If HI.Conn.DB.FuncDecryptData(R!FTPassword.ToString) = HI.ST.UserInfo.UserPassword Then
                        If R!FTStateActive.ToString = "1" Then
                            _Verify = True
                            HI.ST.SysInfo.Admin = (R!FTStateAdmin.ToString = "1")

                            Try
                                HI.ST.UserInfo.UserImage = HI.UL.ULImage.ConvertByteArrayToImmage(R!FPUserImage)
                            Catch ex As Exception
                                HI.ST.UserInfo.UserImage = Nothing
                            End Try

                        Else
                            MsgBox("Username is Not Active Please Contact System Admin !!!")
                        End If
                    Else
                        MsgBox("Password is incorrect !!!")
                    End If
                    Exit For
                Next
            Else
                MsgBox("Username is incorrect !!!")
            End If

            _dt.Dispose()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return _Verify
    End Function

End Class
