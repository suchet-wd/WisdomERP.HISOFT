Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraTreeList
Imports System.IO

Public Class wMainSecurity

    Private _SysPathImageSystem As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images\System"
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private imagemenuList As New System.Windows.Forms.ImageList
    Private imageListSub As New System.Windows.Forms.ImageList
    Private imageListuserpermission As New System.Windows.Forms.ImageList
    Private FoundImgPermission As Boolean = False
    Private _User As wUser
    Private _Permission As wPermission
    Private _MerTeammission As wMerteamPermission
    Private _CopyPermission As CopyPermission
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.



        Dim oSysLang As New HI.ST.SysLanguage

        _User = New wUser
        HI.TL.HandlerControl.AddHandlerObj(_User)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _User.Name.ToString.Trim, _User)
        Catch ex As Exception
        Finally
        End Try

        _Permission = New wPermission
        HI.TL.HandlerControl.AddHandlerObj(_Permission)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _Permission.Name.ToString.Trim, _Permission)
        Catch ex As Exception
        Finally
        End Try

        _MerTeammission = New wMerteamPermission
        HI.TL.HandlerControl.AddHandlerObj(_MerTeammission)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _MerTeammission.Name.ToString.Trim, _MerTeammission)
        Catch ex As Exception
        Finally
        End Try


        _CopyPermission = New CopyPermission
        HI.TL.HandlerControl.AddHandlerObj(_CopyPermission)
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _CopyPermission.Name.ToString.Trim, _CopyPermission)
        Catch ex As Exception
        Finally
        End Try

        ' AddHandler _LstDetail.DoubleClick, AddressOf SubMenu_Click

    End Sub

    Enum MenuType As Integer
        [User] = 0
        [Permission] = 1
        [Merteam] = 2
    End Enum

    Enum ListType As Integer
        _Node = 0
        _Module = 1
        _Menu = 2
        _Form = 3
        _Object = 4
        _User = 5
        _UserPermission = 6
        _UserPermissionModule = 7
        _Permission = 8
        _PermissionModule = 9
        _PermissionModuleMenu = 10
    End Enum

#Region "Menu"


    Private Function W_GEToSysMenu(ByVal ModuleID As Integer, _PermissionID As Integer) As DataTable
        Try
            Dim tSql As String
            tSql = "SELECT M.*  "
            tSql &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysMenu AS M With(NOLOCK) "
            tSql &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS PM With(NOLOCK) "
            tSql &= vbCrLf & " ON M.FTMnuName=PM.FTMnuName"
            tSql &= vbCrLf & " WHERE ISNULL(M.FTStaActive,'0')='1' AND PM.FNHSysPermissionID=" & Val(_PermissionID) & " "
            tSql &= vbCrLf & " AND M.FNHSysModuleID=" & ModuleID & " AND M.FNMnuID <> M.FNMnuIDParent "

            Return HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_SYSTEM)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Function W_GEToSysModule(_PermissionID As Integer) As DataTable
        Try
            Dim tSql As String

            tSql = "SELECT M.*  "
            tSql &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysModule AS M With(NOLOCK) "
            tSql &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionModule AS PM With(NOLOCK) "
            tSql &= vbCrLf & " ON M.FNHSysModuleID=PM.FNHSysModuleID"
            tSql &= vbCrLf & " WHERE ISNULL(M.FTStaActive,'0')='1'  AND PM.FNHSysPermissionID=" & Val(_PermissionID) & " "
            tSql &= vbCrLf & " ORDER BY M.FNSeq"

            Return HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_SYSTEM)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Function W_GEToSysUserPermission(_PermissionID As Integer) As DataTable
        Try
            Dim tSql As String

            tSql = "SELECT M.*  "
            tSql &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.TSEUserLogin AS M With(NOLOCK) "
            tSql &= vbCrLf & " INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS PM With(NOLOCK) "
            tSql &= vbCrLf & " ON M.FTUserName=PM.FTUserName"
            tSql &= vbCrLf & " WHERE  PM.FNHSysPermissionID=" & Val(_PermissionID) & " "
            tSql &= vbCrLf & " ORDER BY M.FTUserName"

            Return HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_SYSTEM)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Function CheckExistsSubMenu(ByVal MnuParent As Double, ByVal oDbdt As System.Data.DataTable) As Boolean
        Try
            Dim aDbRow() As DataRow
            aDbRow = oDbdt.Select("FNMnuIDParent=" & MnuParent & "", "FNSeq")
            Return (aDbRow.Length > 0)
        Catch ex As Exception
            ' MsgBox(ex.Message)
            Throw New Exception(ex.Message)
        End Try
    End Function

#End Region


#Region "Innit Treelist"



#End Region

    Private Sub LoadUser()
        Dim _Str As String = ""
        Dim _dt As DataTable

        Dim tPathImgDis As String
        FoundImgPermission = False

        tPathImgDis = _SystemFilePath & "\Security\Permission.jpg"
        If IO.File.Exists(tPathImgDis) Then
            FoundImgPermission = True
            imageListSub.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
        End If

        _Str = " SELECT      FTUserName AS ColKey, FTUserName AS FTCode, FTUserDescriptionTH AS FTNameTH, FTUserDescriptionEN AS FTNameEN , FPUserImage AS FTImg,FTUserAD "
        _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS A WITH (NOLOCK) WHERE ISNULL(FTStateAdmin,'') <>'1' "
        _Str &= vbCrLf & " ORDER BY FTUserName "

        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

        ogcdetail.DataSource = _dt.Copy

        _dt.Dispose()
    End Sub

    Private Sub LoadPermission()
        Dim _Str As String = ""
        Dim _dt As DataTable
        Dim _dtpermission As New DataTable
        Dim _dtpermissionuser As New DataTable
        Dim tPathImgDis As String

        tPathImgDis = _SystemFilePath & "\Security\Permission.jpg"
        If IO.File.Exists(tPathImgDis) Then
            FoundImgPermission = True
            imageListSub.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
        End If

        _Str = "   SELECT     FNHSysPermissionID AS ColKey, FTPermissionCode AS FTCode, FTPermissionNameTH AS FTNameTH, FTPermissionNameEN AS FTNameEN  "
        _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo. TSEPermission AS M WITH(NOLOCK)"
        _Str &= vbCrLf & " ORDER BY M.FTPermissionCode"

        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

        ogcpermissiondetail.DataSource = _dt.Copy
        _dt.Dispose()
    End Sub

    Private Sub SubMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            With CType(sender, DevExpress.XtraTreeList.TreeList)
                Dim _hifo As TreeListHitInfo = .CalcHitInfo(.PointToClient(Control.MousePosition))
                If (_hifo.Node IsNot Nothing) Then
                    With _hifo.Node

                        Select Case otbsecurity.SelectedTabPage.Name.ToString
                            Case otpuser.Name.ToString
                                If (ocmedituser.Enabled) Then
                                    Call ocmedituser_Click(ocmedituser, New System.EventArgs)
                                End If

                            Case otppermission.Name.ToString
                                If (ocmeditpermission.Enabled) Then
                                    Call ocmeditpermission_Click(ocmeditpermission, New System.EventArgs)
                                End If
                        End Select
                    End With
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadMerTeamPermission()
        Dim _Str As String = ""
        Dim _dt As DataTable
        Dim _dtpermission As New DataTable
        Dim _dtpermissionuser As New DataTable
        Dim tPathImgDis As String

        tPathImgDis = _SystemFilePath & "\Security\Permission.jpg"
        If IO.File.Exists(tPathImgDis) Then
            FoundImgPermission = True
            imageListSub.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
        End If

        _Str = "   SELECT     FNHSysMerTeamId AS ColKey, FTMerTeamCode AS FTCode, FTMerTeamNameTH AS FTNameTH, FTMerTeamNameEN AS FTNameEN  "
        _Str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo. TMERMMerTeam AS M WITH(NOLOCK)"
        _Str &= vbCrLf & " WHERE FTStateActive='1' "
        _Str &= vbCrLf & " ORDER BY M.FTMerTeamCode"

        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

        ogcmerteam.DataSource = _dt.Copy
        _dt.Dispose()
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub SetButton(_MenuType As MenuType)

        ocmaddnewuser.Visible = False
        ocmdeleteuser.Visible = False
        ocmedituser.Visible = False

        ocmaddnewpermission.Visible = False
        ocmdeletepermission.Visible = False
        ocmeditpermission.Visible = False
        ocmcopy.Visible = False
        Select Case _MenuType

            Case MenuType.User
                ocmaddnewuser.Visible = True
                ocmdeleteuser.Visible = True
                ocmedituser.Visible = True
            Case MenuType.Permission
                ocmaddnewpermission.Visible = True
                ocmdeletepermission.Visible = True
                ocmeditpermission.Visible = True
                ocmcopy.Visible = True
        End Select


        HI.TL.METHOD.CallActiveToolBarFunction(Me)
    End Sub

#Region "User"
    Private Sub ocmaddnewuser_Click(sender As System.Object, e As System.EventArgs) Handles ocmaddnewuser.Click
        With _User
            HI.TL.HandlerControl.ClearControl(_User)
            HI.ST.Lang.SP_SETxLanguage(_User)
            .FTUserName.Text = ""
            .FTUserName.Properties.ReadOnly = False
            .FTStateActive.Checked = True
            .ProcComplete = False
            .ShowDialog()

            If (.ProcComplete) Then
                Call LoadUser()
            End If


        End With
    End Sub

    Private Sub ocmedituser_Click(sender As System.Object, e As System.EventArgs) Handles ocmedituser.Click
        If ogcdetail.DataSource Is Nothing Then Exit Sub
        With _User
            HI.TL.HandlerControl.ClearControl(_User)
            HI.ST.Lang.SP_SETxLanguage(_User)
            .FTUserName.Text = _LstDetail.GetFocusedRowCellValue("ColKey").ToString
            .FTUserName.Properties.ReadOnly = True
            .ProcComplete = False
            .ShowDialog()

            If (.ProcComplete) Then
                Call LoadUser()
            End If
        End With
    End Sub

    Private Sub ocmdeleteuser_Click(sender As System.Object, e As System.EventArgs) Handles ocmdeleteuser.Click
        If ogcdetail.DataSource Is Nothing Then Exit Sub

        Dim datausername As String = _LstDetail.GetFocusedRowCellValue("ColKey").ToString
        If HI.MG.ShowMsg.mConfirmProcess("ลบ User", 1403110002, datausername) Then
            Dim _Str As String

            _Str = "Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin WHERE FTUserName='" & HI.UL.ULF.rpQuoted(datausername) & "'"
            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

            _Str = "Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission WHERE FTUserName='" & HI.UL.ULF.rpQuoted(datausername) & "'"
            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

            Call LoadUser()
        End If
    End Sub


#End Region
  

    Private Sub ocmaddnewpermission_Click(sender As System.Object, e As System.EventArgs) Handles ocmaddnewpermission.Click
        With _Permission
            HI.TL.HandlerControl.ClearControl(_Permission)
            HI.ST.Lang.SP_SETxLanguage(_Permission)
            .HSysID = 0
            .ProcComplete = False
            .ShowDialog()

            If (.ProcComplete) Then
                Call LoadPermission()
            End If

        End With
    End Sub

    Private Sub ocmeditpermission_Click(sender As System.Object, e As System.EventArgs) Handles ocmeditpermission.Click
        If ogcdetail.DataSource Is Nothing Then Exit Sub
        With _Permission
            HI.TL.HandlerControl.ClearControl(_Permission)
            HI.ST.Lang.SP_SETxLanguage(_Permission)
            .HSysID = Val(ogvpermissiondetail.GetFocusedRowCellValue("ColKey").ToString)
            ._LstDetail.ClearNodes()
            .ProcComplete = False
            .ShowDialog()

            If (.ProcComplete) Then
                Call LoadPermission()
            End If

        End With
    End Sub

    Private Sub ocmdeletepermission_Click(sender As System.Object, e As System.EventArgs) Handles ocmdeletepermission.Click
        If ogcdetail.DataSource Is Nothing Then Exit Sub

        Dim PermissionID As Integer = Val(ogvpermissiondetail.GetFocusedRowCellValue("ColKey").ToString())

        If HI.MG.ShowMsg.mConfirmProcess("ลบ Permission", 1403110001, PermissionID.ToString) Then

            Dim _Str As String
            _Str = "Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermission WHERE FNHSysPermissionID=" & Val(PermissionID) & ""
            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

            _Str = "Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType WHERE FNHSysPermissionID=" & Val(PermissionID) & ""
            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

            _Str = "Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu WHERE FNHSysPermissionID=" & Val(PermissionID) & ""
            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

            _Str = "Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionModule WHERE FNHSysPermissionID=" & Val(PermissionID) & ""
            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

            _Str = "Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionObjectControl WHERE FNHSysPermissionID=" & Val(PermissionID) & ""
            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

            _Str = "Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission WHERE FNHSysPermissionID=" & Val(PermissionID) & ""
            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

            Call LoadPermission()
        End If

    End Sub

    Private Sub otbsecurity_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbsecurity.SelectedPageChanged

        Select Case e.Page.Name.ToString
            Case otppermission.Name.ToString

                Call SetButton(MenuType.Permission)
                Call LoadPermission()

            Case otpuser.Name.ToString

                Call SetButton(MenuType.User)
                Call LoadUser()
            Case otpmerteamcuspermission.Name.ToString()
                Call SetButton(MenuType.Merteam)
                Call LoadMerTeamPermission()
        End Select

    End Sub

    Private Sub wMainSecurity_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Call otbsecurity_SelectedPageChanged(otbsecurity, New DevExpress.XtraTab.TabPageChangingEventArgs(otppermission, otpuser))
    End Sub

    Private Sub otbsecurity_Click(sender As Object, e As EventArgs) Handles otbsecurity.Click
    End Sub

    Private Sub _LstDetail_DoubleClick(sender As Object, e As EventArgs) Handles _LstDetail.DoubleClick
        'Select Case otbsecurity.SelectedTabPage.Name.ToString
        '    Case otpuser.Name.ToString
        If (ocmedituser.Enabled) Then
                    Call ocmedituser_Click(ocmedituser, New System.EventArgs)
                End If

        '    Case otppermission.Name.ToString
        '        If (ocmeditpermission.Enabled) Then
        '            Call ocmeditpermission_Click(ocmeditpermission, New System.EventArgs)
        '        End If
        'End Select
    End Sub

    Private Sub ogvpermissiondetail_DoubleClick(sender As Object, e As EventArgs) Handles ogvpermissiondetail.DoubleClick
        If (ocmeditpermission.Enabled) Then
            Call ocmeditpermission_Click(ocmeditpermission, New System.EventArgs)
        End If
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click

    End Sub

    Private Sub ogvmerteam_DoubleClick(sender As Object, e As EventArgs) Handles ogvmerteam.DoubleClick
        With _MerTeammission
            HI.TL.HandlerControl.ClearControl(_MerTeammission)
            HI.ST.Lang.SP_SETxLanguage(_MerTeammission)
            .HSysID = Val(ogvmerteam.GetFocusedRowCellValue("ColKey").ToString)
            .FTUserName.Text = ogvmerteam.GetFocusedRowCellValue("FTCode").ToString()
            .FTUserDescriptionTH.Text = ogvmerteam.GetFocusedRowCellValue("FTNameTH").ToString()
            .FTUserDescriptionEN.Text = ogvmerteam.GetFocusedRowCellValue("FTNameEN").ToString()
            .ProcComplete = False
            .ShowDialog()

            If (.ProcComplete) Then
                Call LoadMerTeamPermission()
            End If

        End With
    End Sub

    Private Sub ocmcopy_Click(sender As Object, e As EventArgs) Handles ocmcopy.Click

        With _CopyPermission
            HI.TL.HandlerControl.ClearControl(_CopyPermission)
            HI.ST.Lang.SP_SETxLanguage(_CopyPermission)
            .FRID = Val(ogvpermissiondetail.GetFocusedRowCellValue("ColKey").ToString)
            .otboldpermissionname.Text = ogvpermissiondetail.GetFocusedRowCellValue("FTCode").ToString
            .SaveProces = False
            .ocmexit.Enabled = True
            .ocmsave.Enabled = True
            .StartPosition = FormStartPosition.CenterScreen
            .ShowDialog()

            If .SaveProces Then
                Call LoadPermission()
            End If

        End With
    End Sub
End Class