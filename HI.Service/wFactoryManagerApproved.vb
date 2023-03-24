Imports DevExpress.XtraBars

Imports System.IO
Imports DevExpress.XtraTabbedMdi
Imports System.Reflection
Imports System.Globalization
Imports System.Threading
Imports HI.ST
Imports DevExpress.XtraNavBar
Imports System.Windows.Forms
Imports System.Drawing
Imports HI.TL.RunID
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid

Public Class wFactoryManagerApproved

    Private _ProcLoad As Boolean = False
    Private _SysPathImageSystem As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images\System"
    ' Private olbhisoft As DevExpress.XtraEditors.LabelControl

    Private DT As DataTable
    Private DTFac As DataTable
    Private DTICM As DataTable
    Private DT_App_CM As DataTable
    Private _ClsService As New ClsService

    Friend Shared _CountApp As Integer = 0
    Friend Shared _CountApp_CM As Integer = 0
    '  Private Shared _frmApp As New wDirectorApproved ' = Nothing

    Friend Shared DTPurchaseNo As DataTable
    Friend Property Data_DTPurchaseNo As DataTable
        Get
            Return DTPurchaseNo
        End Get
        Set(ByVal value As DataTable)
            DTPurchaseNo = value
        End Set
    End Property

    Friend Shared DTAppFactory As DataTable
    Friend Property Data_DTAppFactory As DataTable
        Get
            Return DTAppFactory
        End Get
        Set(ByVal value As DataTable)
            DTAppFactory = value
        End Set
    End Property

    Sub New()
        _ProcLoad = True

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _Splash = New HI.TL.SplashScreen("Wisdom System", "Loading Approve Factory Order..." & "  ")

        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US", True)
        Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"

        _AllFuncName = ""

        Me._BarManager = New DevExpress.XtraBars.BarManager
        With _BarManager
            .Form = Me
            .AllowCustomization = False
            .DockControls.Add(Me.StandaloneBarDockControl)
        End With

        Me.CreateToolBarFunction(Me._BarManager)

        Dim fi As FileInfo = New FileInfo(System.Reflection.Assembly.GetExecutingAssembly.Location)
        _PathFileDll = fi.DirectoryName
        HI.ST.SysInfo.PathFileDLL = _PathFileDll

        FTUserLogINIP.Caption = "User IP :: " & HI.ST.UserInfo.UserLogInComputerIP

        Try
            Dim _Theme As String = HI.UL.AppRegistry.ReadRegistry(HI.UL.AppRegistry.KeyName.Theme)

            If _Theme <> "" Then
                MainDefaultLookAndFeel.LookAndFeel.SetSkinStyle(_Theme)
            End If
        Catch ex As Exception
        End Try

        Dim tPathImgDis As String = _SysPathImageSystem & "\" & "AppIcon.png"
        If IO.File.Exists(tPathImgDis) Then
            ' Me.Icon = Icon.FromHandle(DirectCast(Image.FromFile(tPathImgDis), Bitmap).GetHicon()) 'Icon.FromHandle(hIcon)
            MainRibbonControl.ApplicationIcon = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis)))
        Else
            MainRibbonControl.ApplicationIcon = Nothing
        End If

        Dim oSysLang As New HI.ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, Me.Name.ToString.Trim, Me)
        Catch ex As Exception
        Finally
        End Try

        HI.TL.HandlerControl.AddHandlerObj(Me)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, Me.Name.ToString.Trim, Me, False)
        Catch ex As Exception
        Finally
        End Try

        _Splash.Close()

        _ProcLoad = False
    End Sub

#Region "Handler"
    'Here are the handlers
    'Private Sub MyPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
    '    e.Graphics.DrawImage(pbBackground, 0, 0, Me.ClientSize.Width,Me.ClientSize.Height)
    'End Sub

    'Private Sub MySizeChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Me.Invalidate()
    'End Sub

#End Region

    Private _BarManager As DevExpress.XtraBars.BarManager
    Private _AllFuncName As String
    Private _SystemFilePath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _PathFileDll As String
    Private _Splash As HI.TL.SplashScreen
    Private _TimcCheckUserLogin As Integer = 60
    Private _TimcCountCheckUserLogin As Integer = 0

    ReadOnly Property ActiveMdiForm() As Form
        Get
            Return Me.ActiveMdiChild
        End Get
    End Property


    Private Shared _PathAppName As String = ""
    Private Shared _AppName As String = ""
    Private Shared _AppServiceName As String = ""

    Public Shared Sub ReadPathAppName()

        While _PathAppName = ""
            Try
                For I As Integer = 1 To 900000000

                Next
                Dim _Qry As String = "Select Top 3  FTCfgName,FTCfgData  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS A WITH(NOLOCK) WHERE FTCfgName in ('AppPath','AppName','AppSevice') "
                Dim dt As DataTable

                dt = HI.Conn.SQLConn.GetDataTable(_Qry, HI.Conn.DB.DataBaseName.DB_SECURITY)

                For Each R As DataRow In dt.Rows
                    Select Case R!FTCfgName.ToString.Trim.ToUpper
                        Case "AppPath".ToUpper
                            _PathAppName = R!FTCfgData.ToString
                        Case "AppName".ToUpper
                            _AppName = R!FTCfgData.ToString
                        Case "AppSevice".ToUpper
                            _AppServiceName = R!FTCfgData.ToString
                    End Select
                Next

            Catch ex As Exception
            End Try
        End While


    End Sub

    Public Sub FuncItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
        Try

            ' Exit Sub

            ' MessageBox.Show("FuncItemClick")
            Dim tTag As String = e.Item.Tag.ToString

            ' MessageBox.Show(tTag)

            Const nCmdName As Integer = 3

            Dim aTag() As String = tTag.Split("|")
            Dim tCmdName As String = ""

            If UBound(aTag) >= nCmdName Then tCmdName = aTag(nCmdName)

            ' MessageBox.Show(tCmdName)

            If aTag(0) <> "Y".ToUpper Then

                '    MessageBox.Show(Me.Name)

                'If IsNothing(Me.ActiveMdiForm) Then Exit Sub

                For Each ctrl As Object In Me.Controls.Find(tCmdName, True)
                    If TypeOf ctrl Is DevExpress.XtraEditors.SimpleButton Then
                        With CType(ctrl, DevExpress.XtraEditors.SimpleButton)

                            '   MessageBox.Show("ก่อน  " & tCmdName)

                            .PerformClick()

                            '   MessageBox.Show("หลัง  " & tCmdName)
                        End With
                    ElseIf TypeOf ctrl Is System.Windows.Forms.Button Then
                        With CType(ctrl, System.Windows.Forms.Button)
                            .PerformClick()
                        End With
                    End If
                Next
            Else
                Select Case tCmdName.ToString.ToUpper
                    Case "FuncExit".ToUpper
                        ClsService.StateShow = False
                        Me.Close()
                End Select
            End If
        Catch ex As Exception

            ' MessageBox.Show(ex.Message)

        End Try
    End Sub


#Region "SystemFuncBar"

    Public Function CreateToolBarFunction(ByVal BarManager As DevExpress.XtraBars.BarManager, Optional ByVal MenuName As String = "MainMenu") As Boolean
        Dim ModuleCode As String = ""
        Try

            Dim oDbdt As DataTable = GETSysFunction(MenuName) 'ไม่พบข้อมูล SysMenu
            If IsNothing(oDbdt) Then Return False

            Dim mainToolBarFunction As DevExpress.XtraBars.Bar = Nothing
            For Each obj As DevExpress.XtraBars.Bar In BarManager.Bars
                If obj.BarName = "mainToolBarFunction" Then
                    mainToolBarFunction = obj
                    Exit For
                End If
            Next

            If IsNothing(mainToolBarFunction) Then mainToolBarFunction = New DevExpress.XtraBars.Bar(BarManager)
            With mainToolBarFunction
                .BarName = "mainToolBarFunction"
                .Text = "mainToolBarFunction"
                .DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone
                .OptionsBar.AllowQuickCustomization = False
                .OptionsBar.DrawDragBorder = False
                .OptionsBar.UseWholeRow = True
                .StandaloneBarDockControl = StandaloneBarDockControl
                .Visible = True
                .ItemLinks.Clear()
            End With

            Dim oBarButtonItem As DevExpress.XtraBars.BarLargeButtonItem
            'Dim oBarButtonItem As DevExpress.XtraBars.BarButtonItem
            For Each oDbRow As DataRow In oDbdt.Rows
                'oBarButtonItem = New DevExpress.XtraBars.BarButtonItem
                oBarButtonItem = New DevExpress.XtraBars.BarLargeButtonItem
                With oBarButtonItem

                    Dim _ShortCutKey As String = oDbRow("FTShortCut").ToString.Trim
                    Dim _ShortCutKeyDisPlay As String = ""

                    If _ShortCutKey <> "" Then
                        Try

                            Dim _ShortKey As System.Windows.Forms.Keys = New KeysConverter().ConvertFromString(_ShortCutKey)
                            _ShortCutKeyDisPlay = "(" & _ShortCutKey & ")"

                        Catch ex As Exception
                        End Try
                    End If

                    _AllFuncName = _AllFuncName & "|" & oDbRow("FTFuncName").ToString.Trim
                    .Name = oDbRow("FTFuncName").ToString.Trim
                    .Caption = ("|" & (oDbRow("FTFuncCaptionEN").ToString.Trim & "|" & oDbRow("FTFuncCaptionTH").ToString.Trim & "|" & oDbRow("FTFuncCaptionVT").ToString.Trim & "|" & oDbRow("FTFuncCaptionKM").ToString.Trim)).Split("|")(HI.ST.Lang.Language).ToString & _ShortCutKeyDisPlay
                    .Tag = oDbRow("FNStateFixedShow").ToString.Trim.ToUpper & "|" & oDbRow("FTFuncCaptionEN").ToString.Trim & "|" & oDbRow("FTFuncCaptionTH").ToString.Trim & "|" & oDbRow("FTCommandName").ToString.Trim & "||" & oDbRow("FTShortCut").ToString.Trim
                    ' .Width = 30

                    Dim tPathImg As String = _SystemFilePath & "\Func\" & oDbRow("FTFuncImg").ToString.Trim
                    If IO.File.Exists(tPathImg) Then
                        .Glyph = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg))) ' Image.FromFile(tPathImg)
                        '.GlyphDisabled = Image.FromFile(tPathImg)
                    End If

                    Dim tPathImgDis As String = _SystemFilePath & "\Func\" & oDbRow("FTFuncImgDisabled").ToString.Trim
                    If IO.File.Exists(tPathImgDis) Then
                        .GlyphDisabled = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))) 'Image.FromFile(tPathImg)

                    ElseIf IO.File.Exists(tPathImg) Then
                        .GlyphDisabled = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg))) 'Image.FromFile(tPathImg)
                    End If

                    .Id = BarManager.GetNewItemId()
                    .MergeType = DevExpress.XtraBars.BarMenuMerge.MergeItems

                    Select Case oDbRow("FNStateFixedShow").ToString.Trim.ToUpper
                        Case "Y"
                            .Visibility = BarItemVisibility.Always
                            .Enabled = True
                        Case Else
                            .Visibility = BarItemVisibility.Never
                            .Enabled = False
                    End Select

                    If .Caption = "" Then .Enabled = False
                    Select Case Val(oDbRow("FNAlign").ToString)
                        Case 0, 1
                            .Alignment = BarItemLinkAlignment.Left
                        Case Else
                            .Alignment = BarItemLinkAlignment.Right
                    End Select
                    .PaintStyle = BarItemPaintStyle.CaptionGlyph
                    .ShowCaptionOnBar = True
                    .CaptionAlignment = BarItemCaptionAlignment.Bottom
                    .ShortcutKeyDisplayString = True

                End With

                mainToolBarFunction.ItemLinks.Add(oBarButtonItem, oDbRow("FTFuncIsBeginGrp").ToString = "1")

                AddHandler oBarButtonItem.ItemClick, AddressOf Me.FuncItemClick
            Next


            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ToolBarFunctionActive(Optional ByVal Form As Object = Nothing) As Boolean
        Try
            For Each oBarItem As Object In _BarManager.Items
                Select Case oBarItem.GetType.Name.ToString.ToUpper
                    Case "BarButtonItem".ToUpper
                        With CType(oBarItem, DevExpress.XtraBars.BarButtonItem)
                            If InStr(_AllFuncName, .Name.ToString) > 1 Then

                                Select Case .Tag.ToString.Split("|")(0).ToUpper
                                    Case "Y"

                                        .Visibility = BarItemVisibility.Always
                                        .Enabled = True
                                        .Visibility = BarItemVisibility.Always
                                        .Enabled = True
                                        Dim _ShortCutKey As String = .Tag.ToString.Split("|")(5).ToString

                                        If _ShortCutKey <> "" Then
                                            Try
                                                Dim _ShortKey As System.Windows.Forms.Keys = New KeysConverter().ConvertFromString(_ShortCutKey)
                                                .ItemShortcut = New DevExpress.XtraBars.BarShortcut(_ShortKey)
                                            Catch ex As Exception
                                            End Try
                                        End If
                                    Case Else

                                        .Enabled = False
                                        .Visibility = BarItemVisibility.Never
                                        .ItemShortcut = Nothing

                                        If .Tag.ToString.Split("|").Length > 3 Then
                                            Dim _Commandname As String = .Tag.ToString.Split("|")(3).ToString

                                            Dim _ShortCutKey As String = .Tag.ToString.Split("|")(5).ToString

                                            If _Commandname <> "" Then
                                                If Not (Form Is Nothing) Then
                                                    For Each Obj As Object In Form.controls.find(_Commandname, True)
                                                        If Obj.visible Then
                                                            .Visibility = BarItemVisibility.Always

                                                            If TypeOf Obj Is DevExpress.XtraEditors.SimpleButton Then
                                                                .Enabled = CType(Obj, DevExpress.XtraEditors.SimpleButton).Enabled
                                                            ElseIf TypeOf Obj Is System.Windows.Forms.Button Then
                                                                .Enabled = CType(Obj, System.Windows.Forms.Button).Enabled
                                                            Else
                                                                .Enabled = CType(Obj, DevExpress.XtraEditors.SimpleButton).Enabled
                                                            End If


                                                            If _ShortCutKey <> "" Then
                                                                Try
                                                                    Dim _ShortKey As System.Windows.Forms.Keys = New KeysConverter().ConvertFromString(_ShortCutKey)
                                                                    .ItemShortcut = New DevExpress.XtraBars.BarShortcut(_ShortKey)
                                                                Catch ex As Exception
                                                                End Try
                                                            End If


                                                        End If
                                                    Next
                                                End If
                                            End If
                                        End If
                                End Select

                                If .Caption = "" Then .Enabled = False
                            Else
                                .Enabled = True
                            End If
                        End With
                    Case "BarLargeButtonItem".ToUpper
                        With CType(oBarItem, DevExpress.XtraBars.BarLargeButtonItem)
                            If InStr(_AllFuncName, .Name.ToString) > 1 Then

                                Select Case .Tag.ToString.Split("|")(0).ToUpper
                                    Case "Y"

                                        .Visibility = BarItemVisibility.Always
                                        .Enabled = True
                                        .Visibility = BarItemVisibility.Always
                                        .Enabled = True
                                        Dim _ShortCutKey As String = .Tag.ToString.Split("|")(5).ToString

                                        If _ShortCutKey <> "" Then
                                            Try
                                                Dim _ShortKey As System.Windows.Forms.Keys = New KeysConverter().ConvertFromString(_ShortCutKey)
                                                .ItemShortcut = New DevExpress.XtraBars.BarShortcut(_ShortKey)
                                            Catch ex As Exception
                                            End Try
                                        End If
                                    Case Else

                                        .Enabled = False
                                        .Visibility = BarItemVisibility.Never
                                        .ItemShortcut = Nothing

                                        If .Tag.ToString.Split("|").Length > 3 Then
                                            Dim _Commandname As String = .Tag.ToString.Split("|")(3).ToString

                                            Dim _ShortCutKey As String = .Tag.ToString.Split("|")(5).ToString

                                            If _Commandname <> "" Then
                                                If Not (Form Is Nothing) Then
                                                    For Each Obj As Object In Form.controls.find(_Commandname, True)
                                                        If Obj.visible Then
                                                            .Visibility = BarItemVisibility.Always

                                                            If TypeOf Obj Is DevExpress.XtraEditors.SimpleButton Then
                                                                .Enabled = CType(Obj, DevExpress.XtraEditors.SimpleButton).Enabled
                                                            ElseIf TypeOf Obj Is System.Windows.Forms.Button Then
                                                                .Enabled = CType(Obj, System.Windows.Forms.Button).Enabled
                                                            Else
                                                                .Enabled = CType(Obj, DevExpress.XtraEditors.SimpleButton).Enabled
                                                            End If

                                                            If _ShortCutKey <> "" Then
                                                                Try
                                                                    Dim _ShortKey As System.Windows.Forms.Keys = New KeysConverter().ConvertFromString(_ShortCutKey)
                                                                    .ItemShortcut = New DevExpress.XtraBars.BarShortcut(_ShortKey)
                                                                Catch ex As Exception
                                                                End Try
                                                            End If


                                                        End If
                                                    Next
                                                End If
                                            End If
                                        End If
                                End Select

                                If .Caption = "" Then .Enabled = False
                            Else
                                .Enabled = True
                            End If
                        End With
                    Case Else
                End Select
            Next
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Function GETSysFunction(ByVal MenuName As String) As DataTable
        Try
            Dim tSql As String
            tSql = "SELECT * FROM HSysFunc "
            tSql &= vbCrLf & " WHERE ISNULL(FTFuncStaActive,'0')='1'"
            tSql &= vbCrLf & " AND FTMnuName='" & MenuName & "'"

            If Not (HI.ST.UserInfo.UserName.ToUpper Like "*ADMIN*") Then
                tSql &= vbCrLf & " AND FTFuncName<>'FuncInsLang'"
            End If

            tSql &= vbCrLf & " ORDER BY FNAlign,FNFuncSeqNo"
            Return HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_SYSTEM)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
#End Region


    Private Sub MdiManager_SelectedPageChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim _Lang As Integer = HI.ST.Lang.Language

            If IsNothing(Me.ActiveMdiForm) Then
                Me.ToolBarFunctionActive()

                Me.Text = Me.Tag.ToString.Split("|")(_Lang)
            Else
                Me.ToolBarFunctionActive(Me.ActiveMdiForm)

                If ActiveMdiForm.Tag.ToString.Split("|").Length >= 3 Then

                    Dim _ModuleName As String = ActiveMdiForm.Tag.ToString.Split("|")(0)
                    Dim _ModuleID As String = ActiveMdiForm.Tag.ToString.Split("|")(5)



                    Me.Text = Me.Tag.ToString.Split("|")(_Lang) & "  (  " & _ModuleName & "  )  "
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FuncExit()
        ClsService.StateShow = False
        Me.Close()
    End Sub

    Public Sub LoadObjectLanguage()
        Dim oSysLang As New HI.ST.SysLanguage()
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.ToString.Trim, Me)
            For Each oForm As Object In Me.MdiChildren
                If TypeOf oForm Is DevExpress.XtraEditors.XtraForm Then
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, oForm.Name.ToString.Trim, oForm)
                End If
            Next

            MessageBox.Show("Load Object Language Complete.", Me.Text)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            oSysLang = Nothing
        End Try
    End Sub


    Private Sub onvbar_ActiveGroupChanged(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarGroupEventArgs)
        Try
            With CType(sender, DevExpress.XtraNavBar.NavBarGroup)
                If .Expanded Then
                    .Expanded = False
                Else
                    .Expanded = True
                End If
            End With

        Catch ex As Exception
        End Try

    End Sub

    Private Sub onvbar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim K As String = ""
        Catch ex As Exception

        End Try
    End Sub

    Private Sub wApprovePurchaseOrder_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.Enabled = True
        ocmapprove.Enabled = True
        ocmreject.Enabled = True



        Call Set_HeadGrid()
        Call SetSizeGrid()
    End Sub


    Private Sub MainRibbon_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            ClsService.StateFactoryManagerShow = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MainRibbon_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ClsService.StateFactoryManagerShow = False
    End Sub

    Private Sub MainRibbon_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Me.Enabled = True


        ocmapprove.Enabled = True
        ocmreject.Enabled = True

        ocmpreview.Enabled = True
        ocmsavelayout.Enabled = True
        ' System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  ผ่าน wDirectorApproved_Load")
        '  Call WriteLog_Director(Date.Now.ToLongTimeString & "   ผ่าน wDirectorApproved_Load")

        ' MsgBox(" ผ่าน wDirectorApproved_Load")

        '  Log("wDirectorApproved_Load")
        '  Call BindGrid()
        Call ToolBarFunctionActive(Me)
        ' Call ValidateApp()
        Call BindGrid()
        Try
            Call HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvdirector)
        Catch ex As Exception

        End Try

        otmchkpo.Enabled = True
    End Sub

    Private Sub otmchkuserlogin_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If _TimcCountCheckUserLogin < 60 Then
            _TimcCountCheckUserLogin = _TimcCountCheckUserLogin + 1

            If _TimcCountCheckUserLogin >= 60 Then
                Dim _Theard As New Thread(AddressOf CheckUserLogin)
                _Theard.Start(HI.ST.UserInfo.UserName)
            End If

        End If

    End Sub

    Private Delegate Sub DelegateCheckUserLogin(ByVal _Username As String)
    Private Sub CheckUserLogin(ByVal _Username As String)
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckUserLogin(AddressOf CheckUserLogin), New Object() {_Username})
        Else
            If _Username <> "" Then
                If Not (_Username.ToUpper Like "*ADMIN*") Then
                    Dim _Str As String = ""
                    Dim _Dt As DataTable
                    _Str = "SELECT   TOP 1  FTUserName, FTLogInIP, FTLogInDate, FTLogInTime, FTLogInCom"
                    _Str &= vbCrLf & " FROM  TSEUserLoginState "
                    _Str &= vbCrLf & " WHERE  FTUserName='" & HI.UL.ULF.rpQuoted(_Username) & "'  "
                    _Dt = HI.Conn.SQLConn.GetDataTable(_Str, HI.Conn.DB.DataBaseName.DB_SECURITY)

                    If _Dt.Rows.Count > 0 Then
                        Dim _msg As String = "This User are Connected"
                        _msg &= vbCrLf & "By IP : " & _Dt.Rows(0)!FTLogInIP.ToString
                        _msg &= vbCrLf & "By Computername : " & _Dt.Rows(0)!FTLogInCom.ToString
                        _msg &= vbCrLf & " "
                        _msg &= vbCrLf & " Users will be removed to Connect. "

                        If _Dt.Rows(0)!FTLogInIP.ToString <> HI.ST.UserInfo.UserLogInComputerIP Then
                            '  MsgBox(_msg)
                            Application.Exit()
                        End If

                    Else
                        ' MsgBox("Not Foud Stasus Login To System !!!")
                        Application.Exit()
                    End If
                End If
            End If
        End If
        _TimcCountCheckUserLogin = 0
    End Sub


    Private Shared Function FindComputerName(ByVal TempComName As String) As Boolean
        Dim _str As String = String.Empty
        Dim _dt As New DataTable

        Try
            _str = "SELECT  isnull(FTComputerName,'') as FTComputerName,isnull(FTUserName,'') as FTUserName  "
            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSECinfigDirector  WITH(NOLOCK) "
            _str &= Environment.NewLine & " WHERE FTComputerName = '" & TempComName & "'"

            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_SECURITY)

            ' MsgBox(" Director = " & _dt.Rows.Count)

            '  System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  เข้า FindComputerName มี =  " & _dt.Rows.Count)

            If _dt.Rows.Count > 0 Then
                HI.ST.UserInfo.UserName = _dt.Rows(0)!FTUserName.ToString
                Return True
            Else
                Return False
            End If

            _dt.Dispose()

        Catch ex As Exception

        End Try


    End Function

    Private Sub ValidateApp()


        If FindComputerName(Environment.MachineName.ToString()) Then

            DTAppFactory = Nothing
            DTAppFactory = LoadFactoryManagerApprove()
            If Not (DTAppFactory Is Nothing) Then

                'Me.otpapppo.PageVisible = False
                'Me.otpappfactory.PageVisible = True

                ogcdirector.DataSource = DTAppFactory
                Dim view As DevExpress.XtraGrid.Views.Grid.GridView
                view = ogcdirector.Views(0)
                view.OptionsView.ShowAutoFilterRow = True
                Me.ogcdirector = view.GridControl
                Me.ogcdirector.Refresh()
                Call SetSizeGrid()
                'Me.Show()
                ' Call WriteLog_Director(Date.Now.ToLongTimeString & " DT มีข้อมูล =" & DT.Rows.Count)

            Else

                ogcdirector.DataSource = Nothing
                ' Call WriteLog_Director(Date.Now.ToLongTimeString & " DT Nothing")
            End If


        End If

    End Sub

    Public Shared Sub Log(str As String)
        Try
            Dim fileWritter As StreamWriter = File.AppendText("C:\Test_Service.txt")
            fileWritter.WriteLine(DateTime.Now.ToString() + " " + str)
            fileWritter.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Set_HeadGrid()
        Try
            With ogvdirector
                .OptionsView.ShowAutoFilterRow = False
                .OptionsSelection.MultiSelect = False
                .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
            End With
            With ogvcminv
                .OptionsView.ShowAutoFilterRow = False
                .OptionsSelection.MultiSelect = False
                .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
            End With
        Catch ex As Exception
        End Try
    End Sub


    Private Sub SetSizeGrid()
        'Try
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFTStateApproved").Width = 50
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFTPurchaseBy").Width = 75
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFTSuperVisorName").Width = 75
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFTPurchaseNo").Width = 100
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFDPurchaseDate").Width = 70
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFTDeliveryCode").Width = 75
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFTDeliveryDesc").Width = 100
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFNExchangeRate").Width = 90
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFNDisCountAmt").Width = 75
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFNSurcharge").Width = 75
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFNVatAmt").Width = 110
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFNPOGrandAmt").Width = 110
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFTRemark").Width = 100
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFTTeamGrpCode").Width = 100
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFTTeamGrpName").Width = 130
        'Catch ex As Exception

        'End Try


    End Sub

    Public Shared Function LoadFactoryManagerApprove() As DataTable
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            Dim _LangDisPlay As String = "TH"

            If HI.ST.Lang.Language <> Lang.eLang.TH Then
                _LangDisPlay = "EN"
            End If

            _str = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_ORDER_LIST_FACTORY_MANAGER_APPROVE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_LangDisPlay) & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_PUR)

            If _dt.Rows.Count > 0 Then
                _CountApp = _dt.Rows.Count
                Return _dt
            Else
                _CountApp = 0
                Return _dt
            End If

            _dt.Dispose()

        Catch ex As Exception

        End Try


    End Function

    Public Shared Function LoadFactoryManagerApproveCM() As DataTable
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            Dim _LangDisPlay As String = "TH"

            If HI.ST.Lang.Language <> Lang.eLang.TH Then
                _LangDisPlay = "EN"
            End If

            _str = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_ORDER_LISTING_CHANGE_FACTORY_CM_APPROVE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_LangDisPlay) & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If _dt.Rows.Count > 0 Then
                _CountApp_CM = _dt.Rows.Count
                Return _dt
            Else
                _CountApp_CM = 0
                Return _dt
            End If

            _dt.Dispose()

        Catch ex As Exception

        End Try


    End Function



    Public Shared Function LoadCommercialInvoice(Optional State As Boolean = False) As DataTable
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            '_Cmd = "SELECT    '0' AS FTSelect , I.FTCustomerPO, I.FTInvoiceNo,  I.FNHsysCmpID, I.FTStateSendApp, I.FTStateApp, I.FTStateWHApp, sum(Isnull(D.FNQuantity,0)) AS FNQuantity , MAX(Isnull(S.FNNetCM,0)) AS FNCM , S.FTStyleCode, C.FTCmpCode  "
            '_Cmd &= vbCrLf & "        ,sum(Isnull(D.FNQuantity,0) *Isnull(S.FNNetCM,0)) AS FNAmount , I.FTStateSendBy ,I.FTStateAppBy , I.FTStateRejectBy ,I.FTStateWHRejectBy"

            'If HI.ST.Lang.Language = Lang.eLang.TH Then
            '    _Cmd &= vbCrLf & ", max(C.FTCmpNameEN) as FTCmpName "
            'Else
            '    _Cmd &= vbCrLf & ", max(C.FTCmpNameTH) as FTCmpName"
            'End If

            '_Cmd &= vbCrLf & " , CASE WHEN ISDATE(I.FDInvoiceDate) = 1 Then Convert(nvarchar(10),convert(datetime,I.FDInvoiceDate),103) Else '' END AS FDInvoiceDate "
            '_Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice AS I WITH (NOLOCK) LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTFactoryCMInvoice_D AS D WITH (NOLOCK) ON I.FTCustomerPO = D.FTCustomerPO AND I.FTInvoiceNo = D.FTInvoiceNo LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON I.FTCustomerPO = O.FTPORef LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS S WITH (NOLOCK) ON O.FNHSysStyleId = S.FNHSysStyleId LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH (NOLOCK) ON I.FNHsysCmpID = C.FNHSysCmpId"
            '_Cmd &= vbCrLf & "   WHERE  (I.FNHsysCmpID in ("
            '_Cmd &= vbCrLf & "           Select FNHSysCmpId"
            '_Cmd &= vbCrLf & "	         FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS CC WITH(NOLOCK)"
            '_Cmd &= vbCrLf & "	         WHERE FTUserName ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '_Cmd &= vbCrLf & "	))"
            '_Cmd &= vbCrLf & " AND ISNULL(I.FTStateSendApp,'0')  = '1' "
            '_Cmd &= vbCrLf & " and (ISNULL(I.FTStateApp,'0') = '0') "
            '_Cmd &= vbCrLf & " and (ISNULL(I.FTStateReject,'0') = '0') "
            '_Cmd &= vbCrLf & "group by I.FTCustomerPO, I.FTInvoiceNo, I.FDInvoiceDate, I.FNHsysCmpID, I.FTStateSendApp, I.FTStateApp, I.FTStateWHApp,  S.FTStyleCode, C.FTCmpCode ,I.FTStateSendBy,I.FTStateAppBy, I.FTStateRejectBy ,I.FTStateWHRejectBy"

            Dim _LangDisPlay As String = "TH"

            If HI.ST.Lang.Language <> ST.Lang.eLang.TH Then
                _LangDisPlay = "EN"
            End If

            _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.SP_GET_FACTORY_MANAGER_APP_MI '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_LangDisPlay) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

            Return _oDt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function LoadogcTPURTPurchase() As DataTable
        Try

            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            _str = ""
            _str = "SELECT * FROM (SELECT  isnull(A.FTStateSuperVisorApp,0) as FTStateApproved, A.FTPurchaseNo,"
            _str &= Environment.NewLine & "  SUBSTRING(A.FDPurchaseDate,9,2) + '/'+ SUBSTRING(A.FDPurchaseDate,6,2) + '/' + SUBSTRING(A.FDPurchaseDate,1,4) as FDPurchaseDate,"
            _str &= Environment.NewLine & " ISNULL( A.FTPurchaseBy,'') as FTPurchaseBy, "
            _str &= Environment.NewLine & " ISNULL( A.FTSuperVisorName,'') as FTSuperVisorName, "
            _str &= Environment.NewLine & " isnull(A.FTPurchaseState,'') as FTPurchaseState,"
            _str &= Environment.NewLine & " ISNULL( l2.FTCmpRunCode,'') as FTCmpRunCode,"
            _str &= Environment.NewLine & " L3.FTSuplCode,"
            _str &= Environment.NewLine & " SUBSTRING(A.FDDeliveryDate,9,2) + '/'+ SUBSTRING(A.FDDeliveryDate,6,2) + '/' + SUBSTRING(A.FDDeliveryDate,1,4) as FDDeliveryDate,"
            _str &= Environment.NewLine & " L4.FTCrTermCode,"
            _str &= Environment.NewLine & " ISNULL( A.FNCreditDay,0) as FNCreditDay,"
            _str &= Environment.NewLine & " l5.FTTermOfPMCode,"
            _str &= Environment.NewLine & " L7.FTCurCode,ISNULL(A.FNExchangeRate,0) as FNExchangeRate,"
            _str &= Environment.NewLine & " L1.FTDeliveryCode,"
            _str &= Environment.NewLine & " ISNULL( A.FTContactPerson,'') as FTContactPerson ,ISNULL(A.FTRemark,'') as FTRemark,"
            _str &= Environment.NewLine & " ISNULL( A.FNDisCountPer,0) as FNDisCountPer,ISNULL( A.FNDisCountAmt,0) as FNDisCountAmt,"
            _str &= Environment.NewLine & " ISNULL(A.FNPONetAmt,0) as FNPONetAmt, ISNULL(A.FNVatPer,0) as FNVatPer,ISNULL(A.FNVatAmt,0) as FNVatAmt,"
            _str &= Environment.NewLine & " ISNULL (A.FNSurcharge,0) as FNSurcharge,  ISNULL  (A.FNPOGrandAmt,0) as FNPOGrandAmt,"
            _str &= Environment.NewLine & " l8.FTTeamGrpCode,"
            _str &= Environment.NewLine & " ISNULL(C.FTUserName,'') as FTUserName,"
            _str &= Environment.NewLine & " L9.FTPurGrpCode,"


            Select Case HI.ST.Lang.Language

                Case HI.ST.Lang.eLang.TH
                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameTH,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & "isnull(l3.FTSuplNameTH,'') as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescTH,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameTH,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescTH,'') as FTDeliveryDesc,"
                    _str &= Environment.NewLine & "isnull(l8.FTTeamGrpNameTH,'') as FTTeamGrpName,"
                    _str &= Environment.NewLine & "isnull(l9.FTPurGrpNameTH,'') as FTPurGrpName"
                Case Else
                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameEN,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & "isnull(l3.FTSuplNameEN,'') as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescEN,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameEN,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescEN,'') as FTDeliveryDesc,"
                    _str &= Environment.NewLine & "isnull(l8.FTTeamGrpNameEN,'') as FTTeamGrpName,"
                    _str &= Environment.NewLine & "isnull(l9.FTPurGrpNameEN,'') as FTPurGrpName"
            End Select

            _str &= Environment.NewLine & "  ,dbo.fn_Po_QuantityInfo(A.FTPurchaseNo) AS FTQuantityinfo"
            _str &= Environment.NewLine & "  ,A.FTPoTypeState,A.FTStateFree"
            _str &= Environment.NewLine & "  ,[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.fn_PoHeader_StyleNo(A.FTPurchaseNo) AS FTStyleCode"
            ' _str &= Environment.NewLine & " FROM TPURTPurchase as A with(nolock) INNER JOIN "
            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.V_TPURTPurchase AS A INNER JOIN "
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin as B	ON a.FTPurchaseBy = b.FTUserName INNER JOIN"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp as C ON B.FNHSysTeamGrpId = C.FNHSysTeamGrpId LEFT JOIN"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDelivery as L1 with (nolock) ON a.FNHSysDeliveryId=L1.FNHSysDeliveryId  left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmpRun as L2 with (nolock)  on a.FNHSysCmpRunId=L2.FNHSysCmpRunId Left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMSupplier as L3 with (nolock) on a.FNHSysSuplId = L3.FNHSysSuplId Left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCreditTerm as L4 with (nolock) on A.FNHSysCrTermId = L4.FNHSysCrTermId  left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMPaymentTerm as L5 with (nolock) on a.FNHSysTermOfPMId = L5.FNHSysTermOfPMId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency as L7 with (nolock)  on a.FNHSysCurId = L7.FNHSysCurId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMTeamGrp as L8 with (nolock) on b.FNHSysTeamGrpId = L8.FNHSysTeamGrpId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TPURMPURGrp as L9 with (nolock) on a.FNHSysPurGrpId = L9.FNHSysPurGrpId "
            _str &= Environment.NewLine & " WHERE (C.FTUserName = '" & HI.ST.UserInfo.UserName & "')"
            ' _str &= Environment.NewLine & " AND (a.FTStateSendApp = '1') AND  (isnull(a.FTStateSuperVisorApp,'0') = '0')"
            _str &= Environment.NewLine & " AND (a.FTStateSendApp = '1') AND (a.FTStateSuperVisorApp = '0')"
            _str &= Environment.NewLine & " ) AS A  Order by  A.FTPurchaseBy, a.FDPurchaseDate"

            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_PUR)

            If _dt.Rows.Count > 0 Then
                _CountApp = _dt.Rows.Count
                Return _dt
            Else
                _CountApp = 0
                Return Nothing
            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Function
    Private Sub BindGrid()
        Try

            Call Set_HeadGrid()
            DTFac = LoadFactoryManagerApprove()
            If Not (DTFac Is Nothing) Then
                Dim _FocusRowInDex As Integer = -1
                Dim _FTPurchaseNo As String = ""
                Try
                    If ogvdirector.FocusedRowHandle >= 0 Then
                        _FocusRowInDex = ogvdirector.FocusedRowHandle
                        _FTPurchaseNo = ogvdirector.GetRowCellValue(ogvdirector.FocusedRowHandle, "FTOrderNo").ToString
                    End If
                Catch ex As Exception
                End Try
                ogcdirector.DataSource = Nothing
                ockdirectorselectall.Checked = False
                ogcdirector.DataSource = DTFac
                Dim view As DevExpress.XtraGrid.Views.Grid.GridView
                view = ogcdirector.Views(0)
                view.OptionsView.ShowAutoFilterRow = True
                Me.ogcdirector = view.GridControl
                Me.ogcdirector.Refresh()
                _FocusRowInDex = -1
                With Me.ogvdirector
                    If .RowCount > 0 And _FTPurchaseNo <> "" Then
                        Try
                            _FocusRowInDex = .LocateByValue("FTOrderNo", _FTPurchaseNo)
                        Catch ex As Exception
                        End Try
                        Try
                            If _FocusRowInDex <> -1 Then
                                .FocusedRowHandle = _FocusRowInDex
                            End If
                        Catch ex As Exception
                        End Try
                    End If
                End With
            Else
                ogcdirector.DataSource = Nothing
                ockdirectorselectall.Checked = False
            End If

            DTICM = LoadCommercialInvoice()
            If Not (DTICM Is Nothing) Then
                Me.FTSelectAllCMInv.Checked = False
                Me.ogccminv.DataSource = DTICM
            Else
                Me.ogccminv.DataSource = Nothing
            End If


            DT_App_CM = LoadFactoryManagerApproveCM()

            Me.ogcAppFactoryCM.DataSource = Nothing
            Me.ockAppCMselectall.Checked = False
            Me.ogcAppFactoryCM.DataSource = DT_App_CM



            If DTFac.Rows.Count <= 0 And DTICM.Rows.Count <= 0 And DT_App_CM.Rows.Count <= 0 Then
                ClsService.StateShow = False
                Me.Close()
            End If



        Catch ex As Exception
        End Try
    End Sub



    Friend Function Update_ManagerApproved(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long

        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0


        Try

            ReDim _aPurchaseBy(TempGrid.RowCount - 1)
            ReDim _atPurchaseNo(TempGrid.RowCount - 1)

            For k = 0 To TempGrid.RowCount - 1
                _aPurchaseBy(k) = ""
                _atPurchaseNo(k) = ""
            Next

            _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(_IntCount, "FTPurchaseBy").ToString()

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction



            For i = 0 To TempGrid.RowCount - 1

                If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then
                    _Str = ""
                    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                    _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
                    _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                    _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
                    _Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTPurchase] "
                    _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    If _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString() Then

                        If _atPurchaseNo(_IntCount) = String.Empty Then
                            _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
                        Else
                            _atPurchaseNo(_IntCount) &= " ;" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
                        End If

                    Else
                        _IntCount = _IntCount + 1
                        _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString()
                        _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()

                    End If

                End If
            Next



            For j = 0 To _IntCount

                ' ส่งเมล Approved ไปหา SuperVisor   FNMailStateType = 0

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Approved PurchaseNo   << Converter File to PDF >> ','" & _atPurchaseNo(j) & "' ,0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If



                ' ส่งเมล Approved ไปหา SuperVisor   FNMailStateType = 1

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Approved PurchaseNo  << Converter File to PDF >> ','" & _atPurchaseNo(j) & "' ,0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

                ' กรณีส่งหาตัวเอง
                'If TempGrid.GetRowCellValue(i, "FTSuperVisorName").ToString().Trim = HI.ST.UserInfo.UserName Then
                '     _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                '    _Str = ""
                '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
                '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & TempGrid.GetRowCellValue(i, "FTSuperVisorName").ToString() & "'"
                '    _Str &= ",'Approved  " & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "  << Converter File to PDF >>' ,0,0,1,0,0,0,"
                '    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

                '    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                '        HI.Conn.SQLConn.Tran.Rollback()
                '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                '        Return False
                '    End If

                'End If


            Next


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try


    End Function

    Friend Function Update_ManagerApproved(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String, ByVal TempRemark As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long
        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0


        Try

            ReDim _aPurchaseBy(TempGrid.RowCount - 1)
            ReDim _atPurchaseNo(TempGrid.RowCount - 1)

            For k = 0 To TempGrid.RowCount - 1
                _aPurchaseBy(k) = ""
                _atPurchaseNo(k) = ""
            Next

            _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(_IntCount, "FTPurchaseBy").ToString()

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For i = 0 To TempGrid.RowCount - 1

                If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then
                    _Str = ""
                    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                    _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
                    _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                    _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    ' _Str &= Environment.NewLine & ", [FTRemark] = '" & TempRemark & "'"
                    _Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
                    _Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTPurchase] "
                    _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    If _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString() Then

                        If _atPurchaseNo(_IntCount) = String.Empty Then
                            _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
                        Else
                            _atPurchaseNo(_IntCount) &= " ;" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
                        End If

                    Else
                        _IntCount = _IntCount + 1
                        _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString()
                        _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()

                    End If


                End If

            Next



            For j = 0 To _IntCount
                ' ส่งเมลกลับกรณี Reject

                ' _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                '_Str = ""
                '_Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                '_Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                '_Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                '_Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                '_Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
                '_Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString() & "'"
                '_Str &= ",'Reject  " & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "' ,0,1,0,0,0,0,"
                '_Str &= "@FTMailText,'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

                'HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark



                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Reject PurchaseNo',0,1,0,0,0,0,"
                _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"

                'HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark


                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If


                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Reject PurchaseNo' ,0,1,0,0,0,0,"
                _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"

                'HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

                ' กรณีส่ง Mail ให้ตัวเอง
                If _atPurchaseNo(j) = HI.ST.UserInfo.UserName Then
                    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                    _Str = ""
                    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
                    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                    _Str &= ",'Reject PurchaseNo',1,0,0,0,0,0,"
                    _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"
                    ' _Str &= "@FTMailText,'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

                    ' HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function



    Private Sub sBtnSave_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click

        Try
            Select Case Me.otbmain.SelectedTabPage.Name

                Case otpappfactory.Name
                    If ogvdirector.RowCount = 0 Then Exit Sub

                    With CType(ogcdirector.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTSelect='1'").Length <= 0 Then
                            Exit Sub
                        End If

                    End With

                    Dim _Spls As New HI.TL.SplashScreen("Approving Factory Order...,Pease wait.")
                    otmchkpo.Enabled = False
                    Try
                        Call _ClsService.Update_FactoryApprove(ogvdirector, 1)
                    Catch ex As Exception
                    End Try
                    otmchkpo.Enabled = True
                    _Spls.Close()
                Case otpappcminv.Name
                    If ogvcminv.RowCount = 0 Then Exit Sub
                    With CType(ogccminv.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTSelect='1'").Length <= 0 Then Exit Sub
                    End With
                    Dim _Spls As New HI.TL.SplashScreen("Approving Factory Commercial Invoice...,Pease wait...")
                    Try
                        Call _ClsService.Update_CMInvApprove(ogvcminv, 1)
                    Catch ex As Exception
                    End Try
                    _Spls.Close()
                Case otpappfactoryCM.Name
                    If ogvAppFactoryCM.RowCount = 0 Then Exit Sub
                    With CType(ogcAppFactoryCM.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTSelect='1'").Length <= 0 Then Exit Sub
                    End With
                    Dim _Spls As New HI.TL.SplashScreen("Approving Factory CM...,Pease wait...")
                    Try
                        Call _ClsService.Update_FactoryCMApprove(ogvAppFactoryCM, 1)
                    Catch ex As Exception
                    End Try
                    _Spls.Close()
            End Select

            Call BindGrid()

        Catch ex As Exception

        End Try
    End Sub

    'Private Sub sBtnExit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
    '    Me.Hide()
    'End Sub

    Private Sub sBtnReject_Click(sender As Object, e As EventArgs) Handles ocmreject.Click
        Dim BolReject As Boolean = False
        Try
            ' ตรวจสอบก่อนมีการ Check หรือไม่
            Select Case Me.otbmain.SelectedTabPage.Name
                Case otpappfactory.Name
                    If ogvdirector.RowCount = 0 Then Exit Sub
                    With CType(ogcdirector.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTSelect='1'").Length <= 0 Then
                            Exit Sub
                        End If
                    End With
                Case otpappcminv.Name
                    If ogvcminv.RowCount = 0 Then Exit Sub
                    With CType(ogccminv.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTSelect='1'").Length <= 0 Then Exit Sub
                    End With
                Case otpappfactoryCM.Name
                    If ogvAppFactoryCM.RowCount = 0 Then Exit Sub
                    With CType(ogcAppFactoryCM.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTSelect='1'").Length <= 0 Then Exit Sub
                    End With

            End Select
            Dim _frmshowReject As New wShowReject
            otmchkpo.Enabled = False
            _frmshowReject.ShowDialog()
            Select Case Me.otbmain.SelectedTabPage.Name
                Case otpappfactory.Name
                    Try
                        Dim _Spls As New HI.TL.SplashScreen("Rejecting Factory Order...,Pease wait.")
                        Call _ClsService.Update_FactoryApproved(ogvdirector, 2, wShowReject.Data_Reason)
                        _Spls.Close()
                    Catch ex As Exception
                    End Try
                Case otpappcminv.Name
                    Try
                        Dim _Spls As New HI.TL.SplashScreen("Rejecting Commercial Invoice...,Pease wait.")
                        Call _ClsService.Update_CMInvApprove(ogvcminv, "0", wShowReject.Data_Reason)
                        _Spls.Close()
                    Catch ex As Exception
                    End Try
                Case otpappfactoryCM.Name
                    Try
                        Dim _Spls As New HI.TL.SplashScreen("Rejecting Factory CM...,Pease wait.")
                        Call _ClsService.Update_FactoryCMApprove(ogvAppFactoryCM, "0", wShowReject.Data_Reason)
                        _Spls.Close()
                    Catch ex As Exception
                    End Try
            End Select
            otmchkpo.Enabled = True
            Call BindGrid()
        Catch ex As Exception
            otmchkpo.Enabled = True
        End Try
    End Sub

    Private Sub otmchkpo_Tick(sender As Object, e As EventArgs) Handles otmchkpo.Tick

        'Log("otmchkpo_Tick  Work")
        'Application.DoEvents()
        Call BindGrid()
        'Application.DoEvents()
        Me.Enabled = True
        ocmapprove.Enabled = True
        ocmreject.Enabled = True

        ocmpreview.Enabled = True
        If ogvdirector.RowCount > 0 Or ogvcminv.RowCount > 0 Or ogvAppFactoryCM.RowCount > 0 Then

            Me.Show()

            If Me.WindowState = FormWindowState.Minimized Then
                Me.WindowState = FormWindowState.Maximized
            End If

        End If

        Call SetSizeGrid()

        '  Call WriteLog_Director(Date.Now.ToLongTimeString & "  ผ่าน otmchkpo_Tick")

        ' System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  ผ่าน otmchkpo_Tick")
    End Sub


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        ClsService.StateShow = False
        Me.Close()
    End Sub

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvdirector)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ogvdirector_RowCountChanged(sender As Object, e As EventArgs) Handles ogvdirector.RowCountChanged
        Try
            If Not (ogcdirector.DataSource Is Nothing) Then

                Dim Dt As DataTable = CType(ogcdirector.DataSource, DataTable)
                Me.otpappfactory.PageVisible = (Dt.Rows.Count > 0)
            Else
                Me.otpappfactory.PageVisible = False
            End If

        Catch ex As Exception
            Me.otpappfactory.PageVisible = False
        End Try
    End Sub

    Private Sub ogvcminv_RowCountChanged(sender As Object, e As EventArgs) Handles ogvcminv.RowCountChanged
        Try
            If Not (ogccminv.DataSource Is Nothing) Then
                Dim Dt As DataTable = CType(ogccminv.DataSource, DataTable)
                Me.otpappcminv.PageVisible = (Dt.Rows.Count > 0)
            Else
                Me.otpappcminv.PageVisible = False
            End If

        Catch ex As Exception
            Me.otpappcminv.PageVisible = False
        End Try
    End Sub

    Private Sub ogvAppFactoryCM_RowCountChanged(sender As Object, e As EventArgs) Handles ogvAppFactoryCM.RowCountChanged
        Try
            If Not (ogcAppFactoryCM.DataSource Is Nothing) Then
                Dim Dt As DataTable = CType(ogcAppFactoryCM.DataSource, DataTable)
                Me.otpappfactoryCM.PageVisible = (Dt.Rows.Count > 0)
            Else
                Me.otpappfactoryCM.PageVisible = False
            End If

        Catch ex As Exception
            Me.otpappfactoryCM.PageVisible = False
        End Try
    End Sub

    Private Sub ockdirectorselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ockdirectorselectall.CheckedChanged
        Try
            Dim _State As String = "0"
            If Me.ockdirectorselectall.Checked Then
                _State = "1"
            End If
            With ogcdirector
                If Not (.DataSource Is Nothing) And ogvdirector.RowCount > 0 Then

                    With ogvdirector
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, "FTSelect", _State)
                        Next
                    End With
                    CType(.DataSource, DataTable).AcceptChanges()
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub otbmain_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbmain.SelectedPageChanged
        Call TabChange()
    End Sub

    Private Sub TabChange()
        ocmpreview.Visible = False
        Me.ToolBarFunctionActive(Me)
    End Sub

    Private Sub FTSelectAllCMInv_CheckedChanged(sender As Object, e As EventArgs) Handles FTSelectAllCMInv.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.FTSelectAllCMInv.Checked Then
                _State = "1"
            End If

            With ogccminv
                If Not (.DataSource Is Nothing) And ogvcminv.RowCount > 0 Then

                    With ogvcminv
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, "FTSelect", _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Select Case Me.otbmain.SelectedTabPage.Name
            Case otpappfactory.Name
                With Me.ogvcminv
                    If .RowCount <= 0 Then Exit Sub
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                    Dim _DocNo As String = "" & .GetFocusedRowCellValue("FTOrderNo").ToString

                    With New HI.RP.Report
                        .FormTitle = Me.Text
                        .ReportFolderName = "Merchandise Report\"
                        .ReportName = "FactoryPO.rpt"
                        .Formular = "{TMERTOrder.FTOrderNo}='" & HI.UL.ULF.rpQuoted(_DocNo) & "' "
                        .Preview()
                    End With

                End With
            Case otpappcminv.Name
                With Me.ogvcminv
                    If .RowCount <= 0 Then Exit Sub
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                    Dim _Fm As String = ""
                    _Fm = "{TACCTFactoryCMInvoice.FTCustomerPO}='" & HI.UL.ULF.rpQuoted("" & .GetFocusedRowCellValue("FTCustomerPO").ToString) & "' "
                    _Fm &= " And {TACCTFactoryCMInvoice.FTInvoiceNo}='" & HI.UL.ULF.rpQuoted("" & .GetFocusedRowCellValue("FTInvoiceNo").ToString) & "' "

                    With New HI.RP.Report
                        .FormTitle = Me.Text
                        .ReportFolderName = "Account\"
                        .ReportName = "ReportInvoiceCm.rpt"
                        .Formular = _Fm
                        .Preview()
                    End With
                End With
        End Select
    End Sub

    Private Sub RepositorySelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositorySelect.EditValueChanging
        Try
            Dim State As String = "0"
            If e.NewValue.ToString = "1" Then
                State = "1"
            Else
                State = "0"
            End If

            Dim OrderNo As String = ""

            With ogvdirector
                OrderNo = "" & .GetFocusedRowCellValue("FTOrderNo").ToString()
            End With


            With CType(ogcdirector.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Select("FTOrderNo='" & OrderNo & "'")
                    R!FTSelect = State
                Next

                .AcceptChanges()
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvdirector_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvdirector.CellMerge
        Try
            With Me.ogvdirector
                If "" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString _
                                And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                    e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                    e.Handled = True

                Else
                    e.Merge = False
                    e.Handled = True
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvcminv_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvcminv.CellMerge
        Try
            With Me.ogvcminv
                If "" & .GetRowCellValue(e.RowHandle1, "FTInvoiceNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTInvoiceNo").ToString _
                                And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                    e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                    e.Handled = True

                Else
                    e.Merge = False
                    e.Handled = True
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryItemCheckEdit1_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCheckEdit1.EditValueChanging
        Try
            Dim State As String = "0"
            If e.NewValue.ToString = "1" Then
                State = "1"
            Else
                State = "0"
            End If

            Dim OrderNo As String = ""
            With ogvcminv
                OrderNo = "" & .GetFocusedRowCellValue("FTInvoiceNo").ToString()
            End With

            With CType(ogccminv.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Select("FTInvoiceNo='" & OrderNo & "'")
                    R!FTSelect = State
                Next

                .AcceptChanges()
            End With

        Catch ex As Exception

        End Try
    End Sub



    Private Sub ockAppCMselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ockAppCMselectall.CheckedChanged
        Try
            Dim _State As String = "0"
            If Me.ockAppCMselectall.Checked Then
                _State = "1"
            End If
            With ogcAppFactoryCM
                If Not (.DataSource Is Nothing) And ogvAppFactoryCM.RowCount > 0 Then

                    With ogvAppFactoryCM
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, "FTSelect", _State)
                        Next
                    End With
                    CType(.DataSource, DataTable).AcceptChanges()
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub
End Class