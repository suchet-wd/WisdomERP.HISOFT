Imports DevExpress.XtraBars
Imports DevExpress.XtraTreeList
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
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Controls

Public Class wDirectorApproved

    Private _ProcLoad As Boolean = False
    Private _SysPathImageSystem As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images\System"

    Private DT As DataTable
    Private DTFac As DataTable
    Private DTDoc As DataTable
    Private DTLeaveWaitApp As DataTable
    Friend Shared _CountApp As Integer = 0
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

    Friend Shared DTAppDocument As DataTable
    Friend Property Data_DTAppDocument As DataTable
        Get
            Return DTAppDocument
        End Get
        Set(ByVal value As DataTable)
            DTAppDocument = value
        End Set
    End Property

    Friend Shared DTAppOrderCost As DataTable
    Friend Property Data_DTAppOrderCost As DataTable
        Get
            Return DTAppOrderCost
        End Get
        Set(value As DataTable)
            DTAppOrderCost = value
        End Set
    End Property

    Sub New()
        _ProcLoad = True

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _Splash = New HI.TL.SplashScreen("WISDOM SYSTEM", "Loading Approve Purchase Order..." & "  ")

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
                    '.Width = 30

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
        Me.ocmpreview.Enabled = True


        Call Set_HeadGrid()
        Call SetSizeGrid()


    End Sub


    Private Sub MainRibbon_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            Application.Exit()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MainRibbon_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

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

        Try
            Call HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvDirectorApproved)
        Catch ex As Exception

        End Try

        Try
            Call HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvdirector)
        Catch ex As Exception

        End Try

        Call ToolBarFunctionActive(Me)
        Call ValidateApp()
        otmchkpo.Enabled = True

        'Try
        '    Call TabChange()
        'Catch ex As Exception
        'End Try

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


        '----------- Read Connecttion String From File XML
        '  HI.Conn.DB.GetXmlConnectionString()

        ' System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  ผ่าน HI.Conn.DB.GetXmlConnectionString")

        ' Log("ValidateApp")

        If FindComputerName(Environment.MachineName.ToString()) Then

            'Log(Environment.MachineName.ToString())

            DTPurchaseNo = Nothing
            DTPurchaseNo = LoadogcTPURTPurchase()
            If _CountApp > 0 Then
                'Me.otpapppo.PageVisible = True
                'Me.otpappfactory.PageVisible = False
                '    Log("_CountApp = " & _CountApp)

                If Not (DTPurchaseNo Is Nothing) Then


                    ogDirectorApproved.DataSource = DTPurchaseNo.Copy
                    Dim view As DevExpress.XtraGrid.Views.Grid.GridView
                    view = ogDirectorApproved.Views(0)
                    view.OptionsView.ShowAutoFilterRow = True
                    Me.ogDirectorApproved = view.GridControl
                    Me.ogDirectorApproved.Refresh()
                    Call SetSizeGrid()
                    'Me.Show()
                    ' Call WriteLog_Director(Date.Now.ToLongTimeString & " DT มีข้อมูล =" & DT.Rows.Count)

                Else
                    ogDirectorApproved.DataSource = Nothing
                    ' Call WriteLog_Director(Date.Now.ToLongTimeString & " DT Nothing")

                End If

                DTAppFactory = Nothing
                DTAppFactory = LoadfactoryApprove()

                If Not (DTAppFactory Is Nothing) Then

                    ogcdirector.DataSource = DTAppFactory.Copy
                    Dim view As DevExpress.XtraGrid.Views.Grid.GridView
                    view = ogcdirector.Views(0)
                    view.OptionsView.ShowAutoFilterRow = True
                    Me.ogcdirector = view.GridControl
                    Me.ogcdirector.Refresh()

                Else
                    ogcdirector.DataSource = Nothing

                End If

                DTAppDocument = Nothing
                DTAppDocument = LoadDocumentManagerApprove()

                If Not (DTAppDocument Is Nothing) Then

                    ogcdocument.DataSource = DTAppDocument.Copy
                    Dim view As DevExpress.XtraGrid.Views.Grid.GridView
                    view = ogcdocument.Views(0)
                    view.OptionsView.ShowAutoFilterRow = True
                    Me.ogcdocument = view.GridControl
                    Me.ogcdocument.Refresh()

                Else
                    ogcdocument.DataSource = Nothing

                End If

                DTAppOrderCost = Nothing
                DTAppOrderCost = LoadOrderCostApprove()

                If Not (DTAppOrderCost Is Nothing) Then

                    ogcordercost.DataSource = DTAppOrderCost.Copy
                    Dim view3 As DevExpress.XtraGrid.Views.Grid.GridView
                    view3 = ogcordercost.Views(0)
                    view3.OptionsView.ShowAutoFilterRow = True
                    Me.ogcordercost = view3.GridControl
                    Me.ogcordercost.Refresh()

                Else
                    ogcordercost.DataSource = Nothing

                End If

            Else

                DTAppFactory = Nothing
                DTAppFactory = LoadfactoryApprove()
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

                    DTAppDocument = Nothing
                    DTAppDocument = LoadDocumentManagerApprove()

                    If Not (DTAppDocument Is Nothing) Then

                        ogcdocument.DataSource = DTAppDocument.Copy
                        Dim view3 As DevExpress.XtraGrid.Views.Grid.GridView
                        view3 = ogcdocument.Views(0)
                        view3.OptionsView.ShowAutoFilterRow = True
                        Me.ogcdocument = view3.GridControl
                        Me.ogcdocument.Refresh()

                    Else
                        ogcdocument.DataSource = Nothing

                    End If

                    DTAppOrderCost = Nothing
                    DTAppOrderCost = LoadOrderCostApprove()

                    If Not (DTAppOrderCost Is Nothing) Then

                        ogcordercost.DataSource = DTAppOrderCost.Copy
                        Dim view3 As DevExpress.XtraGrid.Views.Grid.GridView
                        view3 = ogcordercost.Views(0)
                        view3.OptionsView.ShowAutoFilterRow = True
                        Me.ogcordercost = view3.GridControl
                        Me.ogcordercost.Refresh()

                    Else
                        ogcordercost.DataSource = Nothing

                    End If

                Else
                    ogDirectorApproved.DataSource = Nothing
                    ogcdirector.DataSource = Nothing
                    ' Call WriteLog_Director(Date.Now.ToLongTimeString & " DT Nothing")

                    DTAppDocument = Nothing
                    DTAppDocument = LoadDocumentManagerApprove()

                    If Not (DTAppDocument Is Nothing) Then

                        ogcdocument.DataSource = DTAppDocument.Copy
                        Dim view As DevExpress.XtraGrid.Views.Grid.GridView
                        view = ogcdocument.Views(0)
                        view.OptionsView.ShowAutoFilterRow = True
                        Me.ogcdocument = view.GridControl
                        Me.ogcdocument.Refresh()

                    Else
                        ogcdocument.DataSource = Nothing

                    End If

                    DTAppOrderCost = Nothing
                    DTAppOrderCost = LoadOrderCostApprove()

                    If Not (DTAppOrderCost Is Nothing) Then

                        ogcordercost.DataSource = DTAppOrderCost.Copy
                        Dim view3 As DevExpress.XtraGrid.Views.Grid.GridView
                        view3 = ogcordercost.Views(0)
                        view3.OptionsView.ShowAutoFilterRow = True
                        Me.ogcordercost = view3.GridControl
                        Me.ogcordercost.Refresh()

                    Else
                        ogcordercost.DataSource = Nothing

                    End If
                End If



            End If


            DTLeaveWaitApp = LoadLeaveWaitApp()
            If Not (DTLeaveWaitApp Is Nothing) Then
                Me.FTSelectAllleave.Checked = False
                Me.ogcleaveapp.DataSource = DTLeaveWaitApp
                Call LeaveBal()

            Else
                Me.ogcleaveapp.DataSource = Nothing
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
        ' System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  ผ่าน Set_HeadGrid")

        '  Call WriteLog_Director(Date.Now.ToLongTimeString & "   ผ่าน Set_HeadGrid")


        With ogvDirectorApproved
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With


        With ogvdirector
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

        With ogvapproveleave
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With

    End Sub


    Private Sub SetSizeGrid()

        'ogvDirectorApproved.Columns.ColumnByName("ColFTStateApproved").Width = 50
        'ogvDirectorApproved.Columns.ColumnByName("ColFTPurchaseBy").Width = 75
        'ogvDirectorApproved.Columns.ColumnByName("ColFTSuperVisorName").Width = 75
        'ogvDirectorApproved.Columns.ColumnByName("ColFTPurchaseNo").Width = 100
        'ogvDirectorApproved.Columns.ColumnByName("ColFDPurchaseDate").Width = 70
        'ogvDirectorApproved.Columns.ColumnByName("ColFTDeliveryCode").Width = 75
        'ogvDirectorApproved.Columns.ColumnByName("ColFTDeliveryDesc").Width = 100
        'ogvDirectorApproved.Columns.ColumnByName("ColFNExchangeRate").Width = 90
        'ogvDirectorApproved.Columns.ColumnByName("ColFNDisCountAmt").Width = 75
        'ogvDirectorApproved.Columns.ColumnByName("ColFNSurcharge").Width = 75
        'ogvDirectorApproved.Columns.ColumnByName("ColFNVatAmt").Width = 110
        'ogvDirectorApproved.Columns.ColumnByName("ColFNPOGrandAmt").Width = 110
        'ogvDirectorApproved.Columns.ColumnByName("ColFTRemark").Width = 100
        'ogvDirectorApproved.Columns.ColumnByName("ColFTTeamGrpCode").Width = 100
        'ogvDirectorApproved.Columns.ColumnByName("ColFTTeamGrpName").Width = 130

    End Sub

    Public Shared Function LoadogcTPURTPurchase() As DataTable
        Try

            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            '   Call WriteLog_Director(Date.Now.ToLongTimeString & " ก่อน  LoadogcTPURTPurchased")
            '   ระดับ Manager

            _str = ""
            _str = "SELECT * FROM ( "
            _str &= Environment.NewLine & " SELECT  isnull(A.FTStateManagerApp,0) as FTStateApproved, A.FTPurchaseNo,"
            _str &= Environment.NewLine & "  SUBSTRING(A.FDPurchaseDate,9,2) + '/'+ SUBSTRING(A.FDPurchaseDate,6,2) + '/' + SUBSTRING(A.FDPurchaseDate,1,4) as FDPurchaseDate,"
            _str &= Environment.NewLine & "  ISNULL( A.FTPurchaseBy,'') as FTPurchaseBy, "
            _str &= Environment.NewLine & "  ISNULL( A.FTSuperVisorName,'') as FTSuperVisorName, "
            _str &= Environment.NewLine & "  isnull(A.FTPurchaseState,'') as FTPurchaseState,"
            _str &= Environment.NewLine & "  ISNULL( l2.FTCmpRunCode,'') as FTCmpRunCode,"
            _str &= Environment.NewLine & "  L3.FTSuplCode,"
            _str &= Environment.NewLine & "  SUBSTRING(A.FDDeliveryDate,9,2) + '/'+ SUBSTRING(A.FDDeliveryDate,6,2) + '/' + SUBSTRING(A.FDDeliveryDate,1,4) as FDDeliveryDate,"
            _str &= Environment.NewLine & "  L4.FTCrTermCode,"
            _str &= Environment.NewLine & "  ISNULL( A.FNCreditDay,0) as FNCreditDay,"
            _str &= Environment.NewLine & "  l5.FTTermOfPMCode,"
            _str &= Environment.NewLine & "  L7.FTCurCode,ISNULL(A.FNExchangeRate,0) as FNExchangeRate,"
            _str &= Environment.NewLine & "  L1.FTDeliveryCode,"
            _str &= Environment.NewLine & "  ISNULL( A.FTContactPerson,'') as FTContactPerson ,ISNULL(A.FTRemark,'') as FTRemark,"
            _str &= Environment.NewLine & "  ISNULL( A.FNDisCountPer,0) as FNDisCountPer,ISNULL( A.FNDisCountAmt,0) as FNDisCountAmt,"
            _str &= Environment.NewLine & "  ISNULL(A.FNPONetAmt,0) as FNPONetAmt, ISNULL(A.FNVatPer,0) as FNVatPer,ISNULL(A.FNVatAmt,0) as FNVatAmt,"
            _str &= Environment.NewLine & "  ISNULL (A.FNSurcharge,0) as FNSurcharge,  ISNULL  (A.FNPOGrandAmt,0) as FNPOGrandAmt,"
            _str &= Environment.NewLine & "  l8.FTTeamGrpCode,"
            _str &= Environment.NewLine & "  ISNULL(C.FTUserName,'') as FTUserName,"
            _str &= Environment.NewLine & "  L9.FTPurGrpCode , "

            Select Case HI.ST.Lang.Language
                Case HI.ST.Lang.eLang.TH

                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameTH,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & "CASE WHEN A.FTPoTypeState ='3' THEN  A.FTBuyBranchNameTH ELSE isnull(l3.FTSuplNameTH,'') END as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescTH,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameTH,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescTH,'') as FTDeliveryDesc,"
                    _str &= Environment.NewLine & "isnull(l8.FTTeamGrpNameTH,'') as FTTeamGrpName,"
                    _str &= Environment.NewLine & "isnull(l9.FTPurGrpNameTH,'') as FTPurGrpName"

                Case Else

                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameEN,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & "CASE WHEN A.FTPoTypeState ='3' THEN  A.FTBuyBranchNameEN ELSE isnull(l3.FTSuplNameEN,'') END as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescEN,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameEN,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescEN,'') as FTDeliveryDesc,"
                    _str &= Environment.NewLine & "isnull(l8.FTTeamGrpNameEN,'') as FTTeamGrpName,"
                    _str &= Environment.NewLine & "isnull(l9.FTPurGrpNameEN,'') as FTPurGrpName"

            End Select

            _str &= Environment.NewLine & "  , [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.fn_Po_QuantityInfo(A.FTPurchaseNo) AS FTQuantityinfo"
            _str &= Environment.NewLine & "  , A.FTPoTypeState,A.FTStateFree"
            _str &= Environment.NewLine & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.V_TPURTPurchase AS A INNER JOIN "
            _str &= Environment.NewLine & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin as B	ON a.FTSuperVisorName = b.FTUserName LEFT JOIN"
            _str &= Environment.NewLine & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp as C ON B.FNHSysTeamGrpId = C.FNHSysTeamGrpId LEFT JOIN"
            _str &= Environment.NewLine & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDelivery as L1 with (nolock) ON a.FNHSysDeliveryId=L1.FNHSysDeliveryId  left join"
            _str &= Environment.NewLine & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmpRun as L2 with (nolock)  on a.FNHSysCmpRunId=L2.FNHSysCmpRunId Left join"
            _str &= Environment.NewLine & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMSupplier as L3 with (nolock) on a.FNHSysSuplId = L3.FNHSysSuplId Left join"
            _str &= Environment.NewLine & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCreditTerm as L4 with (nolock) on A.FNHSysCrTermId = L4.FNHSysCrTermId  left join"
            _str &= Environment.NewLine & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMPaymentTerm as L5 with (nolock) on a.FNHSysTermOfPMId = L5.FNHSysTermOfPMId left join"
            _str &= Environment.NewLine & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency as L7 with (nolock)  on a.FNHSysCurId = L7.FNHSysCurId left join"
            _str &= Environment.NewLine & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMTeamGrp as L8 with (nolock) on b.FNHSysTeamGrpId = L8.FNHSysTeamGrpId left join"
            _str &= Environment.NewLine & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TPURMPURGrp as L9 with (nolock) on a.FNHSysPurGrpId = L9.FNHSysPurGrpId "
            _str &= Environment.NewLine & "     INNER JOIN (SELECT U.FTUserName, T.FNHSysDirectorGrpId"
            _str &= Environment.NewLine & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U WITH(NOLOCK) INNER JOIN"
            _str &= Environment.NewLine & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp AS T WITH(NOLOCK) ON U.FNHSysTeamGrpId = T.FNHSysTeamGrpId"
            _str &= Environment.NewLine & "  WHERE  T.FNHSysDirectorGrpId IN (SELECT DISTINCT DG.FNHSysDirectorGrpId"
            _str &= Environment.NewLine & "	 FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDirectorGrpUser AS DGU WITH(NOLOCK)  INNER JOIN"
            _str &= Environment.NewLine & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDirectorGrp AS DG  WITH(NOLOCK) ON DGU.FNHSysDirectorGrpId = DG.FNHSysDirectorGrpId"
            _str &= Environment.NewLine & "	 WHERE  (DGU.FTUserName = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'))"
            _str &= Environment.NewLine & " ) AS MU ON A.FTPurchaseBy = MU.FTUserName"
            _str &= Environment.NewLine & "  WHERE (a.FTStateSendApp = '1') AND (a.FTStateSuperVisorApp = '1') AND (a.FTStateManagerApp ='0') AND A.FTPoTypeState <> '3' "
            _str &= Environment.NewLine & "  UNION "
            _str &= Environment.NewLine & "  SELECT  isnull(A.FTStateManagerApp,0) as FTStateApproved, A.FTPurchaseNo,"
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
                    _str &= Environment.NewLine & " CASE WHEN A.FTPoTypeState ='3' THEN  A.FTBuyBranchNameTH ELSE isnull(l3.FTSuplNameTH,'') END as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescTH,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameTH,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescTH,'') as FTDeliveryDesc,"
                    _str &= Environment.NewLine & "isnull(l8.FTTeamGrpNameTH,'') as FTTeamGrpName,"
                    _str &= Environment.NewLine & "isnull(l9.FTPurGrpNameTH,'') as FTPurGrpName"

                Case Else

                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameEN,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & " CASE WHEN A.FTPoTypeState ='3' THEN  A.FTBuyBranchNameEN ELSE isnull(l3.FTSuplNameEN,'') END as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescEN,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameEN,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescEN,'') as FTDeliveryDesc,"
                    _str &= Environment.NewLine & "isnull(l8.FTTeamGrpNameEN,'') as FTTeamGrpName,"
                    _str &= Environment.NewLine & "isnull(l9.FTPurGrpNameEN,'') as FTPurGrpName"

            End Select

            _str &= Environment.NewLine & "  , [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.fn_Po_QuantityInfo(A.FTPurchaseNo) AS FTQuantityinfo"
            _str &= Environment.NewLine & "  , A.FTPoTypeState,A.FTStateFree "
            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.V_TPURTPurchase AS A INNER JOIN "
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin as B	ON a.FTSuperVisorName = b.FTUserName LEFT JOIN"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp as C ON B.FNHSysTeamGrpId = C.FNHSysTeamGrpId LEFT JOIN"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDelivery as L1 with (nolock) ON a.FNHSysDeliveryId=L1.FNHSysDeliveryId  left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmpRun as L2 with (nolock)  on a.FNHSysCmpRunId=L2.FNHSysCmpRunId Left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMSupplier as L3 with (nolock) on a.FNHSysSuplId = L3.FNHSysSuplId Left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCreditTerm as L4 with (nolock) on A.FNHSysCrTermId = L4.FNHSysCrTermId  left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMPaymentTerm as L5 with (nolock) on a.FNHSysTermOfPMId = L5.FNHSysTermOfPMId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency as L7 with (nolock)  on a.FNHSysCurId = L7.FNHSysCurId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMTeamGrp as L8 with (nolock) on b.FNHSysTeamGrpId = L8.FNHSysTeamGrpId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TPURMPURGrp as L9 with (nolock) on a.FNHSysPurGrpId = L9.FNHSysPurGrpId "
            _str &= Environment.NewLine & "   INNER JOIN (SELECT U.FTUserName, T.FNHSysDirectorGrpId"
            _str &= Environment.NewLine & "   FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U WITH(NOLOCK) INNER JOIN"
            _str &= Environment.NewLine & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp AS T WITH(NOLOCK) ON U.FNHSysTeamGrpId = T.FNHSysTeamGrpId"
            _str &= Environment.NewLine & "   WHERE  T.FNHSysDirectorGrpIdTo IN (SELECT DISTINCT DG.FNHSysDirectorGrpId"
            _str &= Environment.NewLine & "	  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDirectorGrpUser AS DGU WITH(NOLOCK)  INNER JOIN"
            _str &= Environment.NewLine & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDirectorGrp AS DG  WITH(NOLOCK) ON DGU.FNHSysDirectorGrpId = DG.FNHSysDirectorGrpId"
            _str &= Environment.NewLine & "	WHERE  (DGU.FTUserName = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'))"
            _str &= Environment.NewLine & " ) AS MU ON A.FTPurchaseBy = MU.FTUserName"
            _str &= Environment.NewLine & " WHERE (a.FTStateSendApp = '1') AND (a.FTStateSuperVisorApp = '1') AND (a.FTStateManagerApp ='0') AND A.FTPoTypeState = '3' "

            _str &= Environment.NewLine & " ) AS A Order by A.FTPurchaseBy,a.FDPurchaseDate"

            '  _dt.Columns.Add("FTImageStatus", GetType(Object))
            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_PUR)

            ' Call WriteLog_Director(Date.Now.ToLongTimeString & " หลัง  LoadogcTPURTPurchased  Count = " & _dt.Rows.Count)

            If _dt.Rows.Count > 0 Then
                _CountApp = _dt.Rows.Count
                Return _dt
            Else
                _CountApp = 0
                Return Nothing
            End If

            _dt.Dispose()


        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Function

    Public Shared Function LoadfactoryApprove() As DataTable
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            Dim _LangDisPlay As String = "TH"

            If HI.ST.Lang.Language <> Lang.eLang.TH Then
                _LangDisPlay = "EN"
            End If

            _str = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_ORDER_LIST_DIRECTOR_APPROVE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_LangDisPlay) & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_PUR)

            If _dt.Rows.Count > 0 Then
                _CountApp = _dt.Rows.Count
                Return _dt
            Else
                _CountApp = 0
                Return Nothing
            End If

            _dt.Dispose()

        Catch ex As Exception

        End Try

        Return Nothing
    End Function

    Public Shared Function LoadDocumentManagerApprove() As DataTable
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            Dim _LangDisPlay As String = "TH"

            If HI.ST.Lang.Language <> Lang.eLang.TH Then
                _LangDisPlay = "EN"
            End If

            _str = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_DOC) & "].dbo.SP_GET_DIRECTOR_APP_DOCUMENT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_LangDisPlay) & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_DOC)

            If _dt.Rows.Count > 0 Then
                _CountApp = _dt.Rows.Count
                Return _dt
            Else
                _CountApp = 0
                Return Nothing
            End If

            _dt.Dispose()

        Catch ex As Exception
        End Try

        Return Nothing
    End Function


    Public Shared Function LoadOrderCostApprove() As DataTable
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            Dim _LangDisPlay As String = "TH"

            If HI.ST.Lang.Language <> Lang.eLang.TH Then
                _LangDisPlay = "EN"
            End If

            _str = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.SP_GET_ORDERCOST_APP '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_LangDisPlay) & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_ACCOUNT)

            If _dt.Rows.Count > 0 Then
                _CountApp = _dt.Rows.Count
                Return _dt
            Else
                _CountApp = 0
                Return Nothing
            End If

            _dt.Dispose()

        Catch ex As Exception
        End Try

        Return Nothing
    End Function


    Private Sub BindGrid()
        Try
            Dim _FocusRowInDex As Integer = -1
            Dim _FTPurchaseNo As String = ""

            'Me.Enabled = True
            'sBtnSave.Enabled = True
            'sBtnReject.Enabled = True
            'sBtnExit.Enabled = True

            ' System.IO.File.WriteAllText("E:\text.txt", " ผ่าน BindGrid")

            '   Call WriteLog_Director(Date.Now.ToLongTimeString & " ผ่าน BindGrid")


            Call Set_HeadGrid()

            '  HI.ST.SysInfo.StateDirector = True     ' true ทดสอบ Super     false ทดสอบ Manager
            ' กลับมาเรียก Function ของตัวเอง
            DT = Nothing
            DT = LoadogcTPURTPurchase()

            '  Call WriteLog_Director(Date.Now.ToLongTimeString & " ผ่าน BindGrid >> DT = LoadogcTPURTPurchase  Count = " & DT.Rows.Count)

            If Not (DT Is Nothing) Then

                'Me.otpapppo.PageVisible = True
                'Me.otpappfactory.PageVisible = False
                _FocusRowInDex = -1
                _FTPurchaseNo = ""

                Try

                    If ogvDirectorApproved.FocusedRowHandle >= 0 Then
                        _FocusRowInDex = ogvDirectorApproved.FocusedRowHandle
                        _FTPurchaseNo = ogvDirectorApproved.GetRowCellValue(ogvDirectorApproved.FocusedRowHandle, "FTPurchaseNo").ToString
                    End If

                Catch ex As Exception
                End Try

                ogDirectorApproved.DataSource = Nothing
                ochkselectall.Checked = False

                ogDirectorApproved.DataSource = DT.Copy
                Dim view As DevExpress.XtraGrid.Views.Grid.GridView
                view = ogDirectorApproved.Views(0)
                view.OptionsView.ShowAutoFilterRow = True
                Me.ogDirectorApproved = view.GridControl
                Me.ogDirectorApproved.Refresh()

                _FocusRowInDex = -1
                With Me.ogvDirectorApproved
                    If .RowCount > 0 And _FTPurchaseNo <> "" Then
                        Try
                            _FocusRowInDex = .LocateByValue("FTPurchaseNo", _FTPurchaseNo)
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



                DTFac = LoadfactoryApprove()

                If Not (DTFac Is Nothing) Then

                    'Me.otpapppo.PageVisible = False
                    'Me.otpappfactory.PageVisible = True

                    _FocusRowInDex = -1
                    _FTPurchaseNo = ""
                    Try

                        If ogvdirector.FocusedRowHandle >= 0 Then
                            _FocusRowInDex = ogvdirector.FocusedRowHandle
                            _FTPurchaseNo = ogvdirector.GetRowCellValue(ogvdirector.FocusedRowHandle, "FTOrderNo").ToString
                        End If

                    Catch ex As Exception
                    End Try
                    ogcdirector.DataSource = Nothing
                    ockdirectorselectall.Checked = False
                    ogcdirector.DataSource = DTFac.Copy

                    Dim view2 As DevExpress.XtraGrid.Views.Grid.GridView
                    view2 = ogcdirector.Views(0)
                    view2.OptionsView.ShowAutoFilterRow = True
                    Me.ogcdirector = view2.GridControl
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

                DTAppDocument = LoadDocumentManagerApprove()

                If Not (DTAppDocument Is Nothing) Then

                    'Me.otpapppo.PageVisible = False
                    'Me.otpappfactory.PageVisible = True
                    _FocusRowInDex = -1
                    _FTPurchaseNo = ""

                    Try

                        If ogvdocument.FocusedRowHandle >= 0 Then
                            _FocusRowInDex = ogvdirector.FocusedRowHandle
                            _FTPurchaseNo = ogvdirector.GetRowCellValue(ogvdirector.FocusedRowHandle, "FTDocumentNo").ToString
                        End If

                    Catch ex As Exception
                    End Try
                    ogcdocument.DataSource = Nothing
                    FTSelectAllDC.Checked = False
                    ogcdocument.DataSource = DTAppDocument.Copy

                    Dim view2 As DevExpress.XtraGrid.Views.Grid.GridView
                    view2 = ogcdocument.Views(0)
                    view2.OptionsView.ShowAutoFilterRow = True
                    Me.ogcdocument = view2.GridControl
                    Me.ogcdocument.Refresh()

                    _FocusRowInDex = -1
                    With Me.ogvdocument
                        If .RowCount > 0 And _FTPurchaseNo <> "" Then
                            Try
                                _FocusRowInDex = .LocateByValue("FTDocumentNo", _FTPurchaseNo)
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
                    ogcdocument.DataSource = Nothing
                End If

                DTAppOrderCost = LoadOrderCostApprove()
                If Not (DTAppOrderCost Is Nothing) Then
                    _FocusRowInDex = -1
                    _FTPurchaseNo = ""
                    Try

                        If ogvordercost.FocusedRowHandle >= 0 Then
                            _FocusRowInDex = ogvordercost.FocusedRowHandle
                            _FTPurchaseNo = ogvordercost.GetRowCellValue(ogvordercost.FocusedRowHandle, "FTOrderNo").ToString
                        End If

                    Catch ex As Exception
                    End Try
                    ogcordercost.DataSource = Nothing
                    FTSelectAllOrderCost.Checked = False
                    ogcordercost.DataSource = DTAppOrderCost.Copy

                    Dim view2 As DevExpress.XtraGrid.Views.Grid.GridView
                    view2 = ogcordercost.Views(0)
                    view2.OptionsView.ShowAutoFilterRow = True
                    Me.ogcordercost = view2.GridControl
                    Me.ogcordercost.Refresh()

                    _FocusRowInDex = -1
                    With Me.ogvordercost
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
                    ogcordercost.DataSource = Nothing
                End If


            Else
                ogDirectorApproved.DataSource = Nothing

                DTFac = LoadfactoryApprove()

                If Not (DTFac Is Nothing) Then

                    'Me.otpapppo.PageVisible = False
                    'Me.otpappfactory.PageVisible = True
                    _FocusRowInDex = -1
                    _FTPurchaseNo = ""
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

                    ogDirectorApproved.DataSource = Nothing
                    ogcdirector.DataSource = Nothing
                    ockdirectorselectall.Checked = False
                    ochkselectall.Checked = False
                    FTSelectAllDC.Checked = False

                    DTAppDocument = LoadDocumentManagerApprove()

                    If Not (DTAppDocument Is Nothing) Then

                        _FocusRowInDex = -1
                        _FTPurchaseNo = ""

                        Try

                            If ogvdocument.FocusedRowHandle >= 0 Then
                                _FocusRowInDex = ogvdirector.FocusedRowHandle
                                _FTPurchaseNo = ogvdirector.GetRowCellValue(ogvdirector.FocusedRowHandle, "FTDocumentNo").ToString
                            End If

                        Catch ex As Exception
                        End Try

                        ogcdocument.DataSource = Nothing
                        FTSelectAllDC.Checked = False
                        ogcdocument.DataSource = DTAppDocument.Copy

                        Dim view2 As DevExpress.XtraGrid.Views.Grid.GridView
                        view2 = ogcdocument.Views(0)
                        view2.OptionsView.ShowAutoFilterRow = True
                        Me.ogcdocument = view2.GridControl
                        Me.ogcdocument.Refresh()

                        _FocusRowInDex = -1
                        With Me.ogvdocument
                            If .RowCount > 0 And _FTPurchaseNo <> "" Then
                                Try
                                    _FocusRowInDex = .LocateByValue("FTDocumentNo", _FTPurchaseNo)
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

                        ogcdocument.DataSource = Nothing


                    End If

                    DTAppOrderCost = LoadOrderCostApprove()

                    If Not (DTAppOrderCost Is Nothing) Then
                        _FocusRowInDex = -1
                        _FTPurchaseNo = ""
                        Try

                            If ogvordercost.FocusedRowHandle >= 0 Then
                                _FocusRowInDex = ogvordercost.FocusedRowHandle
                                _FTPurchaseNo = ogvordercost.GetRowCellValue(ogvordercost.FocusedRowHandle, "FTOrderNo").ToString
                            End If

                        Catch ex As Exception
                        End Try
                        ogcordercost.DataSource = Nothing
                        FTSelectAllOrderCost.Checked = False
                        ogcordercost.DataSource = DTAppOrderCost.Copy

                        Dim view2 As DevExpress.XtraGrid.Views.Grid.GridView
                        view2 = ogcordercost.Views(0)
                        view2.OptionsView.ShowAutoFilterRow = True
                        Me.ogcordercost = view2.GridControl
                        Me.ogcordercost.Refresh()

                        _FocusRowInDex = -1
                        With Me.ogvordercost
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
                        ogcordercost.DataSource = Nothing
                    End If


                End If

                ' Call WriteLog_Director(Date.Now.ToLongTimeString & " DT Nothing")

            End If


            DTLeaveWaitApp = LoadLeaveWaitApp()
            If Not (DTLeaveWaitApp Is Nothing) Then
                Me.FTSelectAllleave.Checked = False
                Me.ogcleaveapp.DataSource = DTLeaveWaitApp
                Call LeaveBal()

            Else
                Me.ogcleaveapp.DataSource = Nothing
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

            'HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
            'HI.Conn.SQLConn.SqlConnectionOpen()
            'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            'HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For i = 0 To TempGrid.RowCount - 1

                If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then


                    _Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_UPDATEPO_MANAGER_APP '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()) & "','" & HI.UL.ULF.rpQuoted(TempStatus) & "','" & HI.UL.ULF.rpQuoted(TempGrid.GetRowCellValue(i, "FTPoTypeState").ToString()) & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Str, HI.Conn.DB.DataBaseName.DB_PUR)

                    'Select Case TempGrid.GetRowCellValue(i, "FTPoTypeState").ToString()
                    '    Case "2"
                    '        _Str = ""
                    '        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchaseService "
                    '        _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
                    '        _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                    '        _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                    '        _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    '        '_Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
                    '        '_Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
                    '        '_Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB
                    '        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTPurchaseService] "
                    '        _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"
                    '    Case "3"
                    '        _Str = ""
                    '        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase "
                    '        _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
                    '        _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                    '        _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                    '        _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    '        '_Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
                    '        '_Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
                    '        '_Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB
                    '        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTFacPurchase] "
                    '        _Str &= Environment.NewLine & " WHERE FTFacPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"
                    '    Case Else
                    '        _Str = ""
                    '        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                    '        _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
                    '        _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                    '        _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                    '        _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    '        '_Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
                    '        '_Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
                    '        '_Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB
                    '        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTPurchase] "
                    '        _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"
                    'End Select
                    'If TempGrid.GetRowCellValue(i, "FTPoTypeState").ToString() = "2" Then
                    '    _Str = ""
                    '    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchaseService "
                    '    _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
                    '    _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                    '    _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                    '    _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    '    '_Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
                    '    '_Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
                    '    '_Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB
                    '    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTPurchaseService] "
                    '    _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"

                    'Else
                    '    _Str = ""
                    '    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                    '    _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
                    '    _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                    '    _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                    '    _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    '    '_Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
                    '    '_Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
                    '    '_Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB
                    '    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTPurchase] "
                    '    _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"

                    'End If

                    'If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    '    HI.Conn.SQLConn.Tran.Rollback()
                    '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    '    Return False
                    'End If

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

                If HI.Conn.SQLConn.ExecuteNonQuery(_Str, HI.Conn.DB.DataBaseName.DB_PUR) Then
                    'HI.Conn.SQLConn.Tran.Rollback()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    'Return False
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

                If HI.Conn.SQLConn.ExecuteNonQuery(_Str, HI.Conn.DB.DataBaseName.DB_PUR) Then
                    'HI.Conn.SQLConn.Tran.Rollback()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    'Return False
                End If

                ' กรณีส่งหาตัวเอง
                'If TempGrid.GetRowCellValue(i, "FTSuperVisorName").ToString().Trim = HI.ST.UserInfo.UserName Then
                '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
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


            'HI.Conn.SQLConn.Tran.Commit()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

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

            'HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
            'HI.Conn.SQLConn.SqlConnectionOpen()
            'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            'HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For i = 0 To TempGrid.RowCount - 1

                If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then

                    _Str = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_UPDATEPO_MANAGER_APP '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()) & "','" & HI.UL.ULF.rpQuoted(TempStatus) & "','" & HI.UL.ULF.rpQuoted(TempGrid.GetRowCellValue(i, "FTPoTypeState").ToString()) & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Str, HI.Conn.DB.DataBaseName.DB_PUR)


                    'If TempGrid.GetRowCellValue(i, "FTPoTypeState").ToString() = "2" Then
                    '    _Str = ""
                    '    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchaseService "
                    '    _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
                    '    _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                    '    _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                    '    _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    '    ' _Str &= Environment.NewLine & ", [FTRemark] = '" & TempRemark & "'"
                    '    '_Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
                    '    '_Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
                    '    '_Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB
                    '    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTPurchaseService] "
                    '    _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"

                    'Else
                    '    _Str = ""
                    '    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                    '    _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
                    '    _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                    '    _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                    '    _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    '    ' _Str &= Environment.NewLine & ", [FTRemark] = '" & TempRemark & "'"
                    '    '_Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
                    '    '_Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
                    '    '_Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB
                    '    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTPurchase] "
                    '    _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"

                    'End If

                    'If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    '    HI.Conn.SQLConn.Tran.Rollback()
                    '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    '    Return False
                    'End If

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

                '_FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
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


                If HI.Conn.SQLConn.ExecuteNonQuery(_Str, HI.Conn.DB.DataBaseName.DB_PUR) Then
                    'HI.Conn.SQLConn.Tran.Rollback()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    'Return False
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

                If HI.Conn.SQLConn.ExecuteNonQuery(_Str, HI.Conn.DB.DataBaseName.DB_PUR) Then
                    'HI.Conn.SQLConn.Tran.Rollback()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    'Return False
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

                    If HI.Conn.SQLConn.ExecuteNonQuery(_Str, HI.Conn.DB.DataBaseName.DB_PUR) Then
                        'HI.Conn.SQLConn.Tran.Rollback()
                        'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        'Return False
                    End If

                End If

            Next

            'HI.Conn.SQLConn.Tran.Commit()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function

    Friend Function Update_ManagerFactoryApprove(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long

        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0

        Try
            Dim _dt As DataTable
            Dim _dtMail As New DataTable
            _dtMail.Columns.Add("FTUser", GetType(String))
            _dtMail.Columns.Add("FTMessage", GetType(String))

            Dim _Langht As Integer = 0
            With CType(Me.ogcdirector.DataSource, DataTable)
                .AcceptChanges()
                _Langht = .Select("FTSelect='1'").Length

                _dt = .Copy
            End With

            Dim _dtUpdate As New DataTable
            _dtUpdate.Columns.Add("FTOrderNo", GetType(String))
            _dtUpdate.Columns.Add("FTPORef", GetType(String))

            'HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
            'HI.Conn.SQLConn.SqlConnectionOpen()
            'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            'HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _dt.Select("FTSelect='1'")
                If R!FTSelect.ToString = "1" Then

                    If _dtUpdate.Select("FTOrderNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "' AND FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString()) & "' ").Length <= 0 Then

                        _dtUpdate.Rows.Add(R!FTOrderNo.ToString(), R!FTPORef.ToString())

                        _Str = ""
                        _Str = "UPDATE A  "
                        _Str &= Environment.NewLine & "SET  [FTStateDirectorApp] = '" & TempStatus & "'"
                        _Str &= Environment.NewLine & ", [FTStateDirectorAppBy] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FDStateDirectorAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTStateDirectorAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrder]  AS A "
                        _Str &= Environment.NewLine & " WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "' AND FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString()) & "' "
                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                        'If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        '    HI.Conn.SQLConn.Tran.Rollback()
                        '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        '    Return False
                        'End If

                        _Str = "UPDATE  A "
                        _Str &= Environment.NewLine & "SET  [FTStateDirectorApp] = '" & TempStatus & "'"
                        _Str &= Environment.NewLine & ", [FTStateDirectorAppBy] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FDStateDirectorAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTStateDirectorAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS A "
                        _Str &= vbCrLf & "  INNER JOIN ("
                        _Str &= vbCrLf & "  SELECT FTOrderNo, FTSubOrderNo, FTPOref"
                        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_AllDivert"
                        _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "' AND FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString()) & "' "
                        _Str &= vbCrLf & "  GROUP BY FTOrderNo, FTSubOrderNo, FTPOref"
                        _Str &= vbCrLf & "   ) AS B ON A.FTOrderNo=B.FTOrderNo AND A.FTSubOrderNo=B.FTSubOrderNo"


                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                        _Str = "UPDATE  A "
                        _Str &= Environment.NewLine & "SET  [FTStateDirectorApp] = '" & TempStatus & "'"
                        _Str &= Environment.NewLine & ", [FTStateDirectorAppBy] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FDStateDirectorAppDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTStateDirectorAppTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert AS A "
                        _Str &= vbCrLf & "  INNER JOIN ("
                        _Str &= vbCrLf & "  SELECT FTOrderNo, FTSubOrderNo, FTPOref"
                        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_AllDivert"
                        _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "' AND FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString()) & "' "
                        _Str &= vbCrLf & "  GROUP BY FTOrderNo, FTSubOrderNo, FTPOref"
                        _Str &= vbCrLf & "   ) AS B ON A.FTOrderNo=B.FTOrderNo AND (A.FTSubOrderNo + '-D' +Convert(nvarchar(30),A.FNDivertSeq ))=B.FTSubOrderNo"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                        If _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTStateSendDirectorBy.ToString()) & "'").Length > 0 Then

                            For Each Rx As DataRow In _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTStateSendDirectorBy.ToString()) & "'")
                                Rx!FTMessage = Rx!FTMessage.ToString & "," & R!FTOrderNo.ToString() & " PO : " & R!FTPORef.ToString() & " (" & R!FTCmpCodeTo.ToString & ")"
                                Exit For
                            Next
                        Else
                            _dtMail.Rows.Add(R!FTStateSendDirectorBy.ToString(), R!FTOrderNo.ToString() & " PO : " & R!FTPORef.ToString() & " (" & R!FTCmpCodeTo.ToString & ")")
                        End If
                    End If


                End If
            Next

            For Each R As DataRow In _dtMail.Rows
                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
                _Str &= ",'Approved Order Factory  ','" & HI.UL.ULF.rpQuoted(R!FTMessage.ToString) & "' ,0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"

                'If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                'End If
                HI.Conn.SQLConn.ExecuteNonQuery(_Str, HI.Conn.DB.DataBaseName.DB_MERCHAN)
                ' ส่งเมล Approved ไปหา SuperVisor   FNMailStateType = 1

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
                _Str &= ",'Approved Order Factory  ','" & HI.UL.ULF.rpQuoted(R!FTMessage.ToString) & "' ,0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',1)"

                'If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                'End If
                HI.Conn.SQLConn.ExecuteNonQuery(_Str, HI.Conn.DB.DataBaseName.DB_MERCHAN)
            Next


            'HI.Conn.SQLConn.Tran.Commit()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try


    End Function

    Friend Function Update_ManagerFactoryApproved(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String, ByVal TempRemark As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long
        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0


        Try

            Dim _dt As DataTable
            Dim _dtMail As New DataTable
            _dtMail.Columns.Add("FTUser", GetType(String))
            _dtMail.Columns.Add("FTMessage", GetType(String))

            Dim _Langht As Integer = 0
            With CType(Me.ogcdirector.DataSource, DataTable)
                .AcceptChanges()
                _Langht = .Select("FTSelect='1'").Length

                _dt = .Copy
            End With

            Dim _dtUpdate As New DataTable
            _dtUpdate.Columns.Add("FTOrderNo", GetType(String))
            _dtUpdate.Columns.Add("FTPORef", GetType(String))

            'HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
            'HI.Conn.SQLConn.SqlConnectionOpen()
            'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            'HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            For Each R As DataRow In _dt.Select("FTSelect='1'")
                If R!FTSelect.ToString = "1" Then

                    If _dtUpdate.Select("FTOrderNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "' AND FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString()) & "' ").Length <= 0 Then

                        _dtUpdate.Rows.Add(R!FTOrderNo.ToString(), R!FTPORef.ToString())

                        _Str = ""
                        _Str = "UPDATE A  "
                        _Str &= Environment.NewLine & "SET  [FTStateDirectorReject] = '1'"
                        _Str &= Environment.NewLine & ", [FTStateDirectorRejectBy] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FDStateDirectorRejectDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTStateDirectorRejectTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrder]  AS A "
                        _Str &= Environment.NewLine & " WHERE FTOrderNo = '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "' AND FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString()) & "' "

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                        'If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        '    HI.Conn.SQLConn.Tran.Rollback()
                        '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        '    Return False
                        'End If

                        _Str = "UPDATE  A "
                        _Str &= Environment.NewLine & "SET  [FTStateDirectorReject] = '1'"
                        _Str &= Environment.NewLine & ", [FTStateDirectorRejectBy] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FDStateDirectorRejectDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTStateDirectorRejectTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS A "
                        _Str &= vbCrLf & "  INNER JOIN ("
                        _Str &= vbCrLf & "  SELECT FTOrderNo, FTSubOrderNo, FTPOref"
                        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_AllDivert"
                        _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "' AND FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString()) & "' "
                        _Str &= vbCrLf & "  GROUP BY FTOrderNo, FTSubOrderNo, FTPOref"
                        _Str &= vbCrLf & "   ) AS B ON A.FTOrderNo=B.FTOrderNo AND A.FTSubOrderNo=B.FTSubOrderNo"


                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                        _Str = "UPDATE  A "
                        _Str &= Environment.NewLine & "SET  [FTStateDirectorReject] = '1'"
                        _Str &= Environment.NewLine & ", [FTStateDirectorRejectBy] = '" & HI.ST.UserInfo.UserName & "'"
                        _Str &= Environment.NewLine & ", [FDStateDirectorRejectDate] = " & HI.UL.ULDate.FormatDateDB
                        _Str &= Environment.NewLine & ", [FTStateDirectorRejectTime] = " & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert AS A "
                        _Str &= vbCrLf & "  INNER JOIN ("
                        _Str &= vbCrLf & "  SELECT FTOrderNo, FTSubOrderNo, FTPOref"
                        _Str &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_AllDivert"
                        _Str &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "' AND FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString()) & "' "
                        _Str &= vbCrLf & "  GROUP BY FTOrderNo, FTSubOrderNo, FTPOref"
                        _Str &= vbCrLf & "   ) AS B ON A.FTOrderNo=B.FTOrderNo AND (A.FTSubOrderNo + '-D' +Convert(nvarchar(30),A.FNDivertSeq ))=B.FTSubOrderNo"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, HI.Conn.DB.DataBaseName.DB_MERCHAN)


                        If _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTStateSendDirectorBy.ToString()) & "'").Length > 0 Then

                            For Each Rx As DataRow In _dtMail.Select("FTUser='" & HI.UL.ULF.rpQuoted(R!FTStateSendDirectorBy.ToString()) & "'")
                                Rx!FTMessage = Rx!FTMessage.ToString & "," & R!FTOrderNo.ToString() & " PO : " & R!FTPORef.ToString() & " (" & R!FTCmpCodeTo.ToString & ")"
                                Exit For
                            Next
                        Else
                            _dtMail.Rows.Add(R!FTStateSendDirectorBy.ToString(), R!FTOrderNo.ToString() & " PO : " & R!FTPORef.ToString() & " (" & R!FTCmpCodeTo.ToString & ")")
                        End If
                    End If


                End If
            Next

            For Each R As DataRow In _dtMail.Rows
                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
                _Str &= ",'Reject Order Factory',0,1,0,0,0,0,"
                _Str &= "'" & HI.UL.ULF.rpQuoted(TempRemark & vbCrLf & R!FTMessage.ToString) & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"

                'HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark
                HI.Conn.SQLConn.ExecuteNonQuery(_Str, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                'If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                'End If


                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTUser.ToString) & "'"
                _Str &= ",'Reject Order Factory' ,0,1,0,0,0,0,"
                _Str &= "'" & HI.UL.ULF.rpQuoted(TempRemark & vbCrLf & R!FTMessage.ToString) & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',1)"

                'HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark
                HI.Conn.SQLConn.ExecuteNonQuery(_Str, HI.Conn.DB.DataBaseName.DB_MERCHAN)
                'If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                'End If

            Next

            'HI.Conn.SQLConn.Tran.Commit()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function

    Private Function Update_ManagerAppprove_Document(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal StateApprove As String, Optional ByVal Reson As String = "") As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _dt As DataTable
            Dim _FTMailId As Long
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_DOC)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy
            End With

            For Each R As DataRow In _dt.Select("FTSelect='1'")

                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument"
                _Cmd &= vbCrLf & " Set  FTStateManagerApp='" & StateApprove & "'"
                _Cmd &= vbCrLf & ", FDManagerAppDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ", FTManagerAppTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ", FTManagerAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                _Cmd &= vbCrLf & " WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(R!FTDocumentNo.ToString) & "'"
                If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If


                If StateApprove = "1" Then

                    _Cmd = "UPDATE A "
                    _Cmd &= vbCrLf & "Set A.FBDocument = T.FBDocument "
                    _Cmd &= vbCrLf & ",A.FTStateManagerApp='" & StateApprove & "'"
                    _Cmd &= vbCrLf & ",A.FDManagerAppDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",A.FTManagerAppTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",A.FTManagerAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TDOCMDocumentTitle AS A INNER JOIN "
                    _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_DOC) & "].dbo.TDCDocument AS T ON A.FNHSysDocNameId = T.FNHSysDocNameId "
                    _Cmd &= vbCrLf & "Where  T.FTDocumentNo='" & HI.UL.ULF.rpQuoted(R!FTDocumentNo.ToString) & "'"

                    If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

                _FTMailId = GetRunNoID("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                If StateApprove = "1" Then
                    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                    _Cmd &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    _Cmd &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                    _Cmd &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    _Cmd &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                    _Cmd &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTSandApproveBy.ToString) & "'"
                    _Cmd &= ",'Director approve','Dear " & R!FTSandApproveBy.ToString & " Director Factory " & vbCr & "Director  Approve document Successfully.  " & "' ,0,1,0,0,0,0,"
                    _Cmd &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"
                    HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                Else
                    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                    _Cmd &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    _Cmd &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                    _Cmd &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    _Cmd &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                    _Cmd &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTSandApproveBy.ToString) & "'"
                    _Cmd &= ",'Director Reject','Dear " & R!FTSandApproveBy.ToString & " Director Factory " & vbCr & "I'm Reject document   Reason :" & Reson.ToString & "' ,0,1,0,0,0,0,"
                    _Cmd &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"
                    HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                End If
            Next
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function Update_ManagerAppprove_OrderCosting(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal StateApprove As String, Optional ByVal Reson As String = "") As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _dt As DataTable
            Dim _FTMailId As Long
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_DOC)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy
            End With

            For Each R As DataRow In _dt.Select("FTSelect='1'")
                _Cmd = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost "
                _Cmd &= vbCrLf & " Set FTStateDirectorApp ='" & StateApprove & "'"
                _Cmd &= vbCrLf & ", FTDirectorName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ", FTDirectorAppDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ", FTDirectorAppTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & " , FTStateApprovedApp ='" & StateApprove & "'"

                _Cmd &= vbCrLf & " , FTStateFactoryManagerApp='" & StateApprove & "'"

                _Cmd &= vbCrLf & " , FTStateInspectorApp ='" & StateApprove & "'"
                _Cmd &= vbCrLf & ",FTStateSendApp='" & StateApprove & "'"
                _Cmd &= vbCrLf & " WHERE FTOrderNo  in (Select FTOrderNo From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN "
                _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH (NOLOCK) ON O.FNHSysCmpId = C.FNHSysCmpId  "
                _Cmd &= vbCrLf & " Where C.FTCmpCode = '" & R!FTCmpName.ToString & "' ) "
                _Cmd &= vbCrLf & " and FTOrderNo in (Select FTOrderNo From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice where LEFT( FDInvoiceDate ,7) ='" & Microsoft.VisualBasic.Right(R!FDInvoiceDate.ToString, 4) & "/" & Microsoft.VisualBasic.Left(R!FDInvoiceDate.ToString, 2) & "')"


                If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

                _Cmd = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice "
                _Cmd &= vbCrLf & " Set FTStateDirectorApp ='" & StateApprove & "'"
                _Cmd &= vbCrLf & ", FTDirectorName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ", FTDirectorAppDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ", FTDirectorAppTime=" & HI.UL.ULDate.FormatTimeDB

                _Cmd &= vbCrLf & " , FTStateApprovedApp ='" & StateApprove & "'"

                _Cmd &= vbCrLf & " , FTStateFactoryManagerApp='" & StateApprove & "'"

                _Cmd &= vbCrLf & " , FTStateInspectorApp ='" & StateApprove & "'"
                _Cmd &= vbCrLf & ",FTStateSendApp='" & StateApprove & "'"

                _Cmd &= vbCrLf & " WHERE FTOrderNo  in (Select FTOrderNo From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN "
                _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH (NOLOCK) ON O.FNHSysCmpId = C.FNHSysCmpId  "
                _Cmd &= vbCrLf & " Where C.FTCmpCode = '" & R!FTCmpName.ToString & "' ) "
                _Cmd &= vbCrLf & " and  LEFT( FDInvoiceDate ,7) ='" & Microsoft.VisualBasic.Right(R!FDInvoiceDate.ToString, 4) & "/" & Microsoft.VisualBasic.Left(R!FDInvoiceDate.ToString, 2) & "'"
                If HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If



                _FTMailId = GetRunNoID("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                If StateApprove = "1" Then
                    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                    _Cmd &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    _Cmd &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                    _Cmd &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    _Cmd &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                    _Cmd &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Cmd &= ",'" & HI.UL.ULF.rpQuoted(R!FTSendAppBy.ToString) & ";" & HI.UL.ULF.rpQuoted(R!FTInspectorName.ToString) & ";" & HI.UL.ULF.rpQuoted(R!FTFactoryManagerName.ToString) & ";" & HI.UL.ULF.rpQuoted(R!FTApprovedName.ToString) & "'"
                    _Cmd &= ",'Director approve','Dear All,   " & vbCr & "   Director  Approve Order Costing Successfully.  " & "' ,0,1,0,0,0,0,"
                    _Cmd &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"
                    HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                Else
                    _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                    _Cmd &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    _Cmd &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"
                    _Cmd &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    _Cmd &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                    _Cmd &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= ",'" & HI.UL.ULF.rpQuoted(R!FTSendAppBy.ToString) & ";" & HI.UL.ULF.rpQuoted(R!FTInspectorName.ToString) & ";" & HI.UL.ULF.rpQuoted(R!FTFactoryManagerName.ToString) & ";" & HI.UL.ULF.rpQuoted(R!FTApprovedName.ToString) & "'"
                    _Cmd &= ",'Director Reject','Dear All, " & vbCr & "I'm Reject  Order Costing   Reason :" & Reson.ToString & "' ,0,1,0,0,0,0,"
                    _Cmd &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',0)"
                    HI.Conn.SQLConn.ExecuteTran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                End If
            Next
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Sub ochkselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ochkselectall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With ogDirectorApproved
                If Not (.DataSource Is Nothing) And ogvDirectorApproved.RowCount > 0 Then

                    With ogvDirectorApproved
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, "FTStateApproved", _State)
                            ' .SetRowCellValue(I, .Columns.ColumnByFieldName("FTStateSuperVisorApp"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sBtnSave_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click
        Try
            Select Case Me.otbmain.SelectedTabPage.Name
                Case otpapppo.Name
                    If ogvDirectorApproved.RowCount = 0 Then Exit Sub

                    With CType(ogDirectorApproved.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTStateApproved='1'").Length <= 0 Then
                            Exit Sub
                        End If

                    End With

                    Dim _Spls As New HI.TL.SplashScreen("Approving Purchase Order...,Pease wait.")
                    otmchkpo.Enabled = False
                    Try
                        Call Update_ManagerApproved(ogvDirectorApproved, 1)
                    Catch ex As Exception
                    End Try
                    otmchkpo.Enabled = True
                    _Spls.Close()
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
                        Call Update_ManagerFactoryApprove(ogvdirector, 1)
                    Catch ex As Exception
                    End Try
                    otmchkpo.Enabled = True
                    _Spls.Close()
                Case otpappdocument.Name
                    If ogvdocument.RowCount = 0 Then Exit Sub

                    With CType(ogcdocument.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTSelect='1'").Length <= 0 Then
                            Exit Sub
                        End If

                    End With

                    Dim _Spls As New HI.TL.SplashScreen("Approving Document Control...,Pease wait.")
                    otmchkpo.Enabled = False
                    Try
                        Call Update_ManagerAppprove_Document(ogvdocument, "1")
                    Catch ex As Exception
                    End Try
                    otmchkpo.Enabled = True
                    _Spls.Close()
                Case otpordercost.Name
                    If ogvordercost.RowCount = 0 Then Exit Sub
                    With CType(ogcordercost.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTSelect='1'").Length <= 0 Then
                            Exit Sub
                        End If
                    End With
                    Dim _Spls As New HI.TL.SplashScreen("Approving Order Costing...,Pease wait.")
                    otmchkpo.Enabled = False
                    Try
                        Call Update_ManagerAppprove_OrderCosting(ogvordercost, "1")
                    Catch ex As Exception
                    End Try
                    otmchkpo.Enabled = True
                    _Spls.Close()
                Case otpleaveapprove.Name


                    If Me.ogvapproveleave.RowCount = 0 Then Exit Sub
                    With CType(ogcleaveapp.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTSelect='1'").Length <= 0 Then
                            Exit Sub
                        End If
                    End With
                    Dim _Spls As New HI.TL.SplashScreen("Approving Leave...,Pease wait.")
                    otmchkpo.Enabled = False
                    Try
                        Call Update_SectApproveEmpLeave(ogvapproveleave, "1")
                    Catch ex As Exception
                    End Try
                    otmchkpo.Enabled = True
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
                Case otpapppo.Name
                    If ogvDirectorApproved.RowCount = 0 Then Exit Sub

                    With CType(ogDirectorApproved.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTStateApproved='1'").Length <= 0 Then
                            Exit Sub
                        End If

                    End With


                Case otpappfactory.Name
                    If ogvdirector.RowCount = 0 Then Exit Sub

                    With CType(ogcdirector.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTSelect='1'").Length <= 0 Then
                            Exit Sub
                        End If

                    End With

                Case otpappdocument.Name
                    If ogvdocument.RowCount = 0 Then Exit Sub

                    With CType(ogcdocument.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTSelect='1'").Length <= 0 Then
                            Exit Sub
                        End If

                    End With
                Case otpordercost.Name
                    If ogvordercost.RowCount = 0 Then Exit Sub

                    With CType(ogcordercost.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTSelect='1'").Length <= 0 Then
                            Exit Sub
                        End If
                    End With
                Case otpleaveapprove.Name
                    If Me.ogvapproveleave.RowCount = 0 Then Exit Sub

                    With CType(ogcleaveapp.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTSelect='1'").Length <= 0 Then
                            Exit Sub
                        End If
                    End With
            End Select

            Dim _frmshowReject As New wShowReject
            otmchkpo.Enabled = False

            ' _frmshowReject = New wShowReject

            _frmshowReject.ShowDialog()


            Select Case Me.otbmain.SelectedTabPage.Name
                Case otpapppo.Name
                    Dim _Spls As New HI.TL.SplashScreen("Rejecting Purchase Order...,Pease wait.")

                    Try
                        Call Update_ManagerApproved(ogvDirectorApproved, 2, wShowReject.Data_Reason)

                    Catch ex As Exception
                    End Try

                    _Spls.Close()


                Case otpappfactory.Name
                    Dim _Spls As New HI.TL.SplashScreen("Rejecting Factory Order...,Pease wait.")

                    Try
                        Call Update_ManagerFactoryApproved(ogvdirector, 2, wShowReject.Data_Reason)

                    Catch ex As Exception
                    End Try

                    _Spls.Close()

                Case otpappdocument.Name


                    Dim _Spls As New HI.TL.SplashScreen("Rejecting Document Control...,Pease wait.")
                    otmchkpo.Enabled = False
                    Try
                        Call Update_ManagerAppprove_Document(ogvdocument, "2", wShowReject.Data_Reason)
                    Catch ex As Exception
                    End Try
                    otmchkpo.Enabled = True
                    _Spls.Close()

                Case otpordercost.Name
                    Dim _Spls As New HI.TL.SplashScreen("Rejecting Order Costing...,Pease wait.")
                    otmchkpo.Enabled = False
                    Try
                        Call Update_ManagerAppprove_OrderCosting(ogvordercost, "2", wShowReject.Data_Reason)
                    Catch ex As Exception
                    End Try
                    otmchkpo.Enabled = True
                    _Spls.Close()
                Case otpleaveapprove.Name
                    Dim _Spls As New HI.TL.SplashScreen("Rejecting Leave Costing...,Pease wait.")
                    otmchkpo.Enabled = False
                    Try
                        Call Update_SectApproveEmpLeave(ogvapproveleave, "0", "")
                    Catch ex As Exception
                    End Try
                    otmchkpo.Enabled = True
                    _Spls.Close()
            End Select

            otmchkpo.Enabled = True

            Call BindGrid()

        Catch ex As Exception
            otmchkpo.Enabled = True
        End Try

    End Sub

    Private Sub otmchkpo_Tick(sender As Object, e As EventArgs) Handles otmchkpo.Tick

        '  Log("otmchkpo_Tick  Work")
        'Application.DoEvents()

        Try

            If ogDirectorApproved.DataSource Is Nothing Then
                With CType(ogDirectorApproved.DataSource, DataTable)
                    .AcceptChanges()

                    If .Select("FTStateApproved='1'").Length > 0 Then
                        Exit Sub
                    End If

                End With

            End If

        Catch ex As Exception
        End Try


        Call BindGrid()
        'Application.DoEvents()
        Me.Enabled = True
        ocmapprove.Enabled = True
        ocmreject.Enabled = True

        ocmpreview.Enabled = True
        If ogvDirectorApproved.RowCount > 0 Or ogvdirector.RowCount > 0 Or ogvapproveleave.RowCount > 0 Or ogvdocument.RowCount > 0 Or ogvordercost.RowCount > 0 Then

            Me.Show()

            If Me.WindowState = FormWindowState.Minimized Then
                Me.WindowState = FormWindowState.Maximized

            End If
        End If

        Call SetSizeGrid()

        '  Call WriteLog_Director(Date.Now.ToLongTimeString & "  ผ่าน otmchkpo_Tick")

        ' System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  ผ่าน otmchkpo_Tick")
    End Sub

    Private Sub ogvDirectorApproved_DoubleClick(sender As Object, e As EventArgs) Handles ogvDirectorApproved.DoubleClick
        With Me.ogvDirectorApproved

            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub

            ' _OldRow = .FocusedRowHandle

            Select Case .GetRowCellValue(.FocusedRowHandle, "FTPoTypeState").ToString()
                Case "2"
                    Dim _PurchaseNo As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTPurchaseNo").ToString()
                    Dim _WformPo As New wPurchaseService

                    With _WformPo
                        .ocmexit.Visible = False
                        .ocmclear.Visible = False
                        '.ocmAddDT.Visible = False
                        '.ocmRemoveDT.Visible = False
                        .FTPurchaseNo.Properties.ReadOnly = True
                        .FTPurchaseNo.Properties.Buttons(0).Enabled = False
                        .FTPurchaseNo.Properties.Buttons(1).Enabled = False
                    End With

                    ' Dim _WShow As New wShowData(_WformPo, _PurchaseNo)

                    Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                    HI.ST.SysInfo.MenuName = "MnuPurchaseService"
                    Dim _WShow As New wShowData(_WformPo, _PurchaseNo)
                    HI.ST.SysInfo.MenuName = _TmpMenu

                    With _WShow
                        .WindowState = System.Windows.Forms.FormWindowState.Maximized
                        .StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                        .ShowDialog()
                    End With

                Case "3"
                    Dim _PurchaseNo As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTPurchaseNo").ToString()
                    Dim _WformPo As New wPurchaseOrderFactory

                    With _WformPo
                        .ocmexit.Visible = False
                        .ocmclear.Visible = False
                        '.ocmadd.Visible = False
                        '.ocmremove.Visible = False
                        .FNHSysCmpId.Properties.Tag = 0

                        .FTFacPurchaseNo.Properties.ReadOnly = True
                        .FTFacPurchaseNo.Properties.Buttons(0).Enabled = False
                        .FTFacPurchaseNo.Properties.Buttons(1).Enabled = False
                    End With

                    ' Dim _WShow As New wShowData(_WformPo, _PurchaseNo)

                    Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                    HI.ST.SysInfo.MenuName = "MnuManualPurchaseFactory"
                    Dim _WShow As New wShowData(_WformPo, _PurchaseNo)
                    HI.ST.SysInfo.MenuName = _TmpMenu

                    With _WShow
                        .WindowState = System.Windows.Forms.FormWindowState.Maximized
                        .StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                        .ShowDialog()
                    End With
                Case "4"
                    Dim _PurchaseNo As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTPurchaseNo").ToString()
                    Dim _WformPo As New wSMPPurchase

                    With _WformPo
                        .ocmexit.Visible = False
                        .ocmclear.Visible = False

                        .FTPurchaseNo.Properties.ReadOnly = True
                        .FTPurchaseNo.Properties.Buttons(0).Enabled = False
                        .FTPurchaseNo.Properties.Buttons(1).Enabled = False
                    End With

                    ' Dim _WShow As New wShowData(_WformPo, _PurchaseNo)

                    Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                    HI.ST.SysInfo.MenuName = "MnuSmpPurchase"
                    Dim _WShow As New wShowData(_WformPo, _PurchaseNo)
                    HI.ST.SysInfo.MenuName = _TmpMenu

                    With _WShow
                        .WindowState = System.Windows.Forms.FormWindowState.Maximized
                        .StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                        .ShowDialog()
                    End With
                Case Else
                    Dim _PurchaseNo As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTPurchaseNo").ToString()
                    Dim _WformPo As New wPurchaseOrder

                    With _WformPo
                        .ocmexit.Visible = False
                        .ocmclear.Visible = False
                        .FTPurchaseNo.Properties.ReadOnly = True
                        .FTPurchaseNo.Properties.Buttons(0).Enabled = False
                        .FTPurchaseNo.Properties.Buttons(1).Enabled = False
                    End With

                    ' Dim _WShow As New wShowData(_WformPo, _PurchaseNo)

                    Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                    HI.ST.SysInfo.MenuName = "MnuManualPurchase"
                    Dim _WShow As New wShowData(_WformPo, _PurchaseNo)
                    HI.ST.SysInfo.MenuName = _TmpMenu

                    With _WShow
                        .WindowState = System.Windows.Forms.FormWindowState.Maximized
                        .StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                        .ShowDialog()
                    End With

            End Select


        End With
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Select Case Me.otbmain.SelectedTabPage.Name
            Case otpapppo.Name
                With Me.ogvDirectorApproved
                    If .RowCount <= 0 Then Exit Sub
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                    'Dim _PoNo As String = "" & .GetFocusedRowCellValue("FTPurchaseNo").ToString
                    'Dim _FNPoState As Integer = 0

                    'Dim _Qry As String = ""

                    '_Qry = "Select TOP 1 FNPoState   "
                    '_Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)"
                    '_Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_PoNo) & "' "

                    '_FNPoState = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, HI.Conn.DB.DataBaseName.DB_PUR, "0")))

                    'With New HI.RP.Report

                    '    Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language

                    '    If _FNPoState = 0 Then
                    '        HI.ST.Lang.Language = HI.ST.Lang.eLang.TH
                    '    Else
                    '        HI.ST.Lang.Language = HI.ST.Lang.eLang.EN
                    '    End If

                    '    .FormTitle = Me.Text
                    '    .ReportFolderName = "PurchaseOrder\"
                    '    .ReportName = "PurchaseOrder.rpt"
                    '    .AddParameter("Draft", "DRAFT")
                    '    .Formular = "{TPURTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(_PoNo) & "'"
                    '    .Preview()

                    '    HI.ST.Lang.Language = _tmplang
                    'End With

                    Dim FTPoTypeState As String = "" & .GetFocusedRowCellValue("FTPoTypeState").ToString()
                    Dim _PoNo As String = "" & .GetFocusedRowCellValue("FTPurchaseNo").ToString
                    Dim _FNPoState As Integer = 0

                    Dim _Qry As String = ""


                    _Qry = "Select TOP 1 FNPoState   "
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.V_TPURTPurchase AS A "
                    _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_PoNo) & "' "

                    _FNPoState = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, HI.Conn.DB.DataBaseName.DB_PUR, "0")))

                    With New HI.RP.Report

                        Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language

                        If _FNPoState = 0 Then
                            HI.ST.Lang.Language = HI.ST.Lang.eLang.TH
                        Else
                            HI.ST.Lang.Language = HI.ST.Lang.eLang.EN
                        End If

                        .FormTitle = Me.Text


                        Select Case FTPoTypeState
                            Case "2"

                                .ReportFolderName = "PurchaseOrder\"
                                .ReportName = "PurchaseService.rpt"
                                .Formular = "{TPURTPurchaseService.FTPurchaseNo} ='" & HI.UL.ULF.rpQuoted(_PoNo) & "' "

                            Case "3"

                                .ReportFolderName = "PurchaseOrder\"
                                .ReportName = "PurchaseOrderFactory.rpt"
                                .AddParameter("Draft", "DRAFT")
                                .Formular = "{TPURTFacPurchase.FTFacPurchaseNo}='" & HI.UL.ULF.rpQuoted(_PoNo) & "'"

                            Case Else

                                .ReportFolderName = "PurchaseOrder\"
                                .ReportName = "PurchaseOrder.rpt"
                                .AddParameter("Draft", "DRAFT")
                                .Formular = "{TPURTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(_PoNo) & "'"

                        End Select


                        .Preview()

                        HI.ST.Lang.Language = _tmplang
                    End With



                End With
            Case otpappfactory.Name

                With Me.ogvdirector
                    If .RowCount <= 0 Then Exit Sub
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                    Dim _DataOrderNo As String = "" & .GetFocusedRowCellValue("FTOrderNo").ToString
                    Dim _DataPoNoRef As String = "" & .GetFocusedRowCellValue("FTPORef").ToString

                    With New HI.RP.Report
                        .FormTitle = Me.Text
                        .ReportFolderName = "Merchandise Report\"
                        .ReportName = "FactoryPO.rpt"
                        '  .Formular = "{TMERTOrder.FTOrderNo}='" & HI.UL.ULF.rpQuoted(_PoNo) & "' "
                        .Formular = "{V_TMERTOrder.FTOrderNo}='" & HI.UL.ULF.rpQuoted(_DataOrderNo) & "' AND {V_TMERTOrder.FTPORef}='" & HI.UL.ULF.rpQuoted(_DataPoNoRef) & "' "
                        .Preview()
                    End With
                End With

        End Select
    End Sub

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvDirectorApproved)
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvdirector)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ogvDirectorApproved_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvDirectorApproved.RowCellStyle
        Try
            With ogvDirectorApproved
                Try
                    If .GetRowCellValue(e.RowHandle, "FTStateFree") = "1" Then
                        e.Appearance.ForeColor = System.Drawing.Color.Blue
                    End If
                Catch ex As Exception
                End Try
            End With
        Catch ex As Exception

        End Try
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
        ' Me.ToolBarFunctionActive(Me)
    End Sub

    Private Sub ogvDirectorApproved_RowCountChanged(sender As Object, e As EventArgs) Handles ogvDirectorApproved.RowCountChanged
        Try

            If Not (ogDirectorApproved.DataSource Is Nothing) Then
                Dim Dt As DataTable = CType(ogDirectorApproved.DataSource, DataTable)
                Me.otpapppo.PageVisible = (Dt.Rows.Count > 0)
            Else
                Me.otpapppo.PageVisible = False
            End If

        Catch ex As Exception
            Me.otpapppo.PageVisible = False
        End Try
        '  Me.ToolBarFunctionActive(Me)
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
        '  Call TabChange()
    End Sub

    Private Sub TabChange()

        ocmapprove.Enabled = True
        ocmreject.Enabled = True
        Me.ocmpreview.Enabled = True

        ' Me.ToolBarFunctionActive(Me)
    End Sub

    Private Sub otbmain_Click(sender As Object, e As EventArgs) Handles otbmain.Click

    End Sub

    Private Sub ogvdocument_RowCountChanged(sender As Object, e As EventArgs) Handles ogvdocument.RowCountChanged
        Try

            If Not (ogcdocument.DataSource Is Nothing) Then
                Dim Dt As DataTable = CType(ogcdocument.DataSource, DataTable)
                Me.otpappdocument.PageVisible = (Dt.Rows.Count > 0)
            Else
                Me.otpappdocument.PageVisible = False
            End If

        Catch ex As Exception
            Me.otpappdocument.PageVisible = False
        End Try
    End Sub

    Private Sub FTSelectAllDC_CheckedChanged(sender As Object, e As EventArgs) Handles FTSelectAllDC.CheckedChanged
        Try
            Dim _State As String = "0"
            If Me.FTSelectAllDC.Checked Then
                _State = "1"
            End If
            With ogcdocument
                If Not (.DataSource Is Nothing) And ogvdocument.RowCount > 0 Then
                    With ogvdocument
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

    Private Sub FTSelectAllOrderCost_CheckedChanged(sender As Object, e As EventArgs) Handles FTSelectAllOrderCost.CheckedChanged
        Try
            Dim _State As String = "0"
            If Me.FTSelectAllOrderCost.Checked Then
                _State = "1"
            End If
            With ogcordercost
                If Not (.DataSource Is Nothing) And ogvordercost.RowCount > 0 Then
                    With ogvordercost
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

    Private Sub ogvordercost_RowCountChanged(sender As Object, e As EventArgs) Handles ogvordercost.RowCountChanged
        Try

            If Not (ogcordercost.DataSource Is Nothing) Then
                Dim Dt As DataTable = CType(ogcordercost.DataSource, DataTable)
                Me.otpordercost.PageVisible = (Dt.Rows.Count > 0)

            Else
                Me.otpordercost.PageVisible = False
            End If

        Catch ex As Exception
            Me.otpordercost.PageVisible = False
        End Try
        '  Me.ToolBarFunctionActive(Me)
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

            With CType(ogvdirector.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Select("FTOrderNo='" & OrderNo & "'")
                    R!FTSelect = State
                Next

                .AcceptChanges()
            End With

        Catch ex As Exception

        End Try
    End Sub

#Region "Leave Approve"

    Public Shared Function LoadLeaveWaitApp() As DataTable
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _LangDisPlay As String = "TH"

            If HI.ST.Lang.Language <> HI.ST.Lang.eLang.TH Then
                _LangDisPlay = "EN"
            End If

            _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_LeaveApproved '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_LangDisPlay) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, HI.Conn.DB.DataBaseName.DB_HR)

            Return _oDt

        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Private Sub FTSelectAllLeave_CheckedChanged(sender As Object, e As EventArgs) Handles FTSelectAllleave.CheckedChanged
        Try
            Dim _State As String = "0"
            If Me.FTSelectAllleave.Checked Then
                _State = "1"
            End If
            With ogcleaveapp
                If Not (.DataSource Is Nothing) And ogvapproveleave.RowCount > 0 Then
                    With ogvapproveleave
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


    Protected Sub ShowLeaveInfo(ByVal EmpCode As String, ByRef _dt As DataTable)


        Dim _Qry As String
        Dim tResetLeave As String

        Try
            _Qry = "SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee"
            Dim tEmpType As String = HI.Conn.SQLConn.GetField(_Qry, HI.Conn.DB.DataBaseName.DB_HR)

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US", True)
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd"
            Dim LeaveVacation As Double = 0

            _Qry = "   SELECT  TOP 1  dbo.FN_Get_Emp_Vacation(FNHSysEmpID,FNHSysEmpTypeId,ISNULL(FDDateStart,N''),ISNULL(FDDateEnd,N''),ISNULL(FDDateProbation,N'')) AS FNEmpVacation"
            _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee  AS M WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID=" & Val(EmpCode) & " "

            LeaveVacation = Val(HI.Conn.SQLConn.GetField(_Qry, HI.Conn.DB.DataBaseName.DB_HR, "0"))

            _Qry = " SELECT CASE WHEN RiGHT(FTCurrenDate,5) >=FTLeaveReset THEN LEFT(FTCurrenDate,4) ELSE  LEFT(FTBefore,4)  END +'/' + FTLeaveReset AS FTLeaveReset"
            _Qry &= vbCrLf & "  FROM"
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT  TOP 1 Convert(varchar(10),GetDate(),111)  AS FTCurrenDate ,Convert(varchar(10),DateAdd(YEAR,-1,GetDate()),111) AS FTBefore,L.FTLeaveReset"
            _Qry &= vbCrLf & " FROM  THRMConfigLeave  AS L WITH (NOLOCK)  INNER JOIN THRMEmployee AS M WITH(NOLOCK )"
            _Qry &= vbCrLf & "  ON  L.FNHSysEmpTypeId=M.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "  WHERE   M.FNHSysEmpID=" & Val(EmpCode) & " "
            _Qry &= vbCrLf & " ) As T"

            tResetLeave = HI.Conn.SQLConn.GetField(_Qry, HI.Conn.DB.DataBaseName.DB_HR, "")


            _Qry = "SELECT FTLeaveCode,FTLeaveName"

            '_Qry &= vbCrLf & ", (Right('000' + Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveRight,0))/ 480.00))),3)"
            _Qry &= vbCrLf & ", (( Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveRight,0))/ 480.00))))"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((ISNULL(FNLeaveRight,0)) % 480.00) / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((ISNULL(FNLeaveRight,0)) % 480.00) % 60.00))),2))  AS FNLeaveRight"

            '_Qry &= vbCrLf & " , (Right('000' + Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveUsed,0))/ 480.00))),3)"
            _Qry &= vbCrLf & " , (( Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveUsed,0))/ 480.00))))"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((ISNULL(FNLeaveUsed,0)) % 480.00) / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((ISNULL(FNLeaveUsed,0)) % 480.00) % 60.00))),2))  AS FNLeaveUsed"

            '_Qry &= vbCrLf & " , (Right('000' + Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveBal,0))/ 480.00))),3)"
            _Qry &= vbCrLf & " , ((Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveBal,0))/ 480.00))))"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((ISNULL(FNLeaveBal,0)) % 480.00) / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((ISNULL(FNLeaveBal,0)) % 480.00) % 60.00))),2))  AS FNLeaveBal"

            _Qry &= vbCrLf & " FROM  (SELECT V_LeaveType.FTLeaveCode,FTLeaveName"

            _Qry &= vbCrLf & ",Cast((ISNULL(FNLeaveRight,0) * 480) AS numeric(18,0)) AS FNLeaveRight"
            _Qry &= vbCrLf & ",ISNULL(FNTotalMinute,0) AS FNLeaveUsed"
            _Qry &= vbCrLf & ",(Cast((ISNULL(FNLeaveRight,0) * 480) AS numeric(18,0))) - ISNULL(FNTotalMinute,0)   AS FNLeaveBal"

            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT CAST(FNListIndex AS varchar(3)) AS FTLeaveCode," & IIf(HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal, "FTNameTH", "FTNameEN") & " AS FTLeaveName "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.V_LeaveType WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNListIndex<>98 AND FNListIndex <2"
            _Qry &= vbCrLf & ") AS V_LeaveType"
            _Qry &= vbCrLf & " Left Join"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " Select THRMConfigLeave.FTLeaveCode"
            _Qry &= vbCrLf & ",CASE WHEN ISNULL(THRMEmployeeLeave.FNLeaveRight,-1)=-1 THEN Cast(ISNULL(THRMConfigLeave.FNLeaveRight,0) AS numeric(18,0)) ELSE Cast(ISNULL(THRMEmployeeLeave.FNLeaveRight,0) AS numeric(18,0)) END AS FNLeaveRight"
            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FTLeaveCode,FNLeaveRight "
            _Qry &= vbCrLf & " FROM THRMConfigLeave WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysEmpTypeId=" & Val(tEmpType) & " "
            _Qry &= vbCrLf & ") THRMConfigLeave"
            _Qry &= vbCrLf & " Left Join"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FTLeaveCode,Cast(ISNULL(FNLeaveRight,0) AS numeric(18,2)) AS FNLeaveRight "
            _Qry &= vbCrLf & " FROM THRMEmployeeLeave WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(EmpCode) & " "
            _Qry &= vbCrLf & ") THRMEmployeeLeave"
            _Qry &= vbCrLf & " ON THRMConfigLeave.FTLeaveCode=THRMEmployeeLeave.FTLeaveCode"
            _Qry &= vbCrLf & ") T ON V_LeaveType.FTLeaveCode=T.FTLeaveCode"
            _Qry &= vbCrLf & " LEFT JOIN "
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FTLeaveType,SUM(FNTotalMinute) AS FNTotalMinute "
            _Qry &= vbCrLf & " FROM THRTTransLeave  WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  FTLeaveType<>N'98'"
            _Qry &= vbCrLf & " AND FNHSysEmpID=" & Val(EmpCode) & " "
            _Qry &= vbCrLf & " AND FTDateTrans>=N'" & tResetLeave & "'"
            _Qry &= vbCrLf & " GROUP BY FTLeaveType"
            _Qry &= vbCrLf & ") AS THRTTransLeave"
            _Qry &= vbCrLf & " ON V_LeaveType.FTLeaveCode=THRTTransLeave.FTLeaveType) AS MM1"

            _Qry &= vbCrLf & " UNION "
            _Qry &= vbCrLf & "SELECT FTLeaveCode,FTLeaveName"

            _Qry &= vbCrLf & " , (Right( Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveRight,0))/ 480.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((ISNULL(FNLeaveRight,0)) % 480.00) / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((ISNULL(FNLeaveRight,0)) % 480.00) % 60.00))),2))  AS FNLeaveRight"

            _Qry &= vbCrLf & ", (Right( Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveUsed,0))/ 480.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((ISNULL(FNLeaveUsed,0)) % 480.00) / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((ISNULL(FNLeaveUsed,0)) % 480.00) % 60.00))),2))  AS FNLeaveUsed"

            _Qry &= vbCrLf & " , (Right( Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveBal,0))/ 480.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((ISNULL(FNLeaveBal,0)) % 480.00) / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((ISNULL(FNLeaveBal,0)) % 480.00) % 60.00))),2))  AS FNLeaveBal"


            _Qry &= vbCrLf & " FROM (SELECT  V_LeaveType.FTLeaveCode,FTLeaveName"

            _Qry &= vbCrLf & ",Cast((ISNULL(FNLeaveRight," & LeaveVacation & ") * 480)  AS numeric(18,0)) AS FNLeaveRight"
            _Qry &= vbCrLf & ",ISNULL(FNTotalMinute,0) AS FNLeaveUsed"
            _Qry &= vbCrLf & ",(Cast((ISNULL(FNLeaveRight," & LeaveVacation & ") * 480)  AS numeric(18,0))) -ISNULL(FNTotalMinute,0)   AS FNLeaveBal"


            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT CAST(FNListIndex AS varchar(3)) AS FTLeaveCode," & IIf(HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal, "FTNameTH", "FTNameEN") & " AS FTLeaveName "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.V_LeaveType WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNListIndex=98"
            _Qry &= vbCrLf & ") AS V_LeaveType"
            _Qry &= vbCrLf & " Left Join"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " Select THRMConfigLeave.FTLeaveCode"
            _Qry &= vbCrLf & "," & LeaveVacation & " AS FNLeaveRight"
            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FTLeaveCode,FNLeaveRight "
            _Qry &= vbCrLf & " FROM THRMConfigLeave WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  FNHSysEmpTypeId=" & Val(tEmpType) & " "
            _Qry &= vbCrLf & ") THRMConfigLeave"
            _Qry &= vbCrLf & " Left Join"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FTLeaveCode," & LeaveVacation & " AS FNLeaveRight "
            _Qry &= vbCrLf & " FROM THRMEmployeeLeave WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(EmpCode) & " "
            _Qry &= vbCrLf & ") THRMEmployeeLeave"
            _Qry &= vbCrLf & " ON THRMConfigLeave.FTLeaveCode=THRMEmployeeLeave.FTLeaveCode"
            _Qry &= vbCrLf & ") T ON V_LeaveType.FTLeaveCode=T.FTLeaveCode"
            _Qry &= vbCrLf & " LEFT JOIN "
            _Qry &= vbCrLf & "("

            _Qry &= vbCrLf & " SELECT FTLeaveType,SUM(FNTotalMinute) AS FNTotalMinute "
            _Qry &= vbCrLf & " FROM THRTTransLeave  WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  FTLeaveType=N'98'"
            _Qry &= vbCrLf & " AND FNHSysEmpID=" & Val(EmpCode) & ""
            _Qry &= vbCrLf & " AND FTDateTrans>=N'" & tResetLeave & "'"
            _Qry &= vbCrLf & " GROUP BY FTLeaveType"
            _Qry &= vbCrLf & ") AS THRTTransLeave"
            _Qry &= vbCrLf & " ON V_LeaveType.FTLeaveCode=THRTTransLeave.FTLeaveType) AS MM2"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, HI.Conn.DB.DataBaseName.DB_HR)

        Catch ex As Exception
        End Try

    End Sub

    Sub LeaveBal()
        Dim _dt As New DataTable
        For k As Integer = 0 To ogvapproveleave.RowCount - 1 Step 1
            Call ShowLeaveInfo(ogvapproveleave.GetRowCellValue(k, "FNHSysEmpID"), _dt)
            For Each A As DataRow In _dt.Select("FTLeaveCode = '0'")
                ogvapproveleave.SetRowCellValue(k, "SickLeave", A!FNLeaveBal.ToString)
            Next
            For Each B As DataRow In _dt.Select("FTLeaveCode = '1'")
                ogvapproveleave.SetRowCellValue(k, "BusinessLeave", B!FNLeaveBal.ToString)
            Next
            For Each C As DataRow In _dt.Select("FTLeaveCode = '98'")
                ogvapproveleave.SetRowCellValue(k, "VacationLeave", C!FNLeaveBal.ToString)
            Next

        Next

        _dt.Dispose()
    End Sub

    Private Sub ogvapproveleave_RowCountChanged(sender As Object, e As EventArgs) Handles ogvapproveleave.RowCountChanged
        Try

            If Not (ogcleaveapp.DataSource Is Nothing) Then
                Dim Dt As DataTable = CType(ogcleaveapp.DataSource, DataTable)
                Me.otpleaveapprove.PageVisible = (Dt.Rows.Count > 0)

            Else
                Me.otpleaveapprove.PageVisible = False
            End If

        Catch ex As Exception
            Me.otpleaveapprove.PageVisible = False
        End Try
        '  Me.ToolBarFunctionActive(Me)
    End Sub


    Friend Function Update_SectApproveEmpLeave(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal StateApprove As String, Optional ByVal Reson As String = "") As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _dt As DataTable
            Dim _FTMailId As Long

            With CType(TempGrid.GridControl.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy
            End With

            For Each R As DataRow In _dt.Select("FTSelect='1' and FTStateType='0'")

                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily"
                _Cmd &= vbCrLf & " Set FTDirApproveState='" & StateApprove & "'"
                _Cmd &= vbCrLf & ", FTDirApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ", FDDirApproveDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ", FTDirApproveTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & " WHERE FNHSysEmpId='" & HI.UL.ULF.rpQuoted(R!FNHSysEmpId.ToString) & "'"
                _Cmd &= vbCrLf & "and FTStartDate='" & HI.UL.ULDate.ConvertEnDB(R!FTStartDate.ToString) & "'"
                _Cmd &= vbCrLf & "and  FTEndDate='" & HI.UL.ULDate.ConvertEnDB(R!FTEndDate.ToString) & "'"

                If HI.Conn.SQLConn.ExecuteOnly(_Cmd, HI.Conn.DB.DataBaseName.DB_HR) = False Then
                    Return False
                End If

                'If StateApprove = "1" Then
                '    _Cmd = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave "
                '    _Cmd &= vbCrLf & " WHERE FNHSysEmpID = " & Val(R!FNHSysEmpId.ToString) & " "
                '    _Cmd &= vbCrLf & " AND FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(R!FTStartDate.ToString) & "'"
                '    _Cmd &= vbCrLf & " AND FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(R!FTEndDate.ToString) & "'"
                '    _Cmd &= vbCrLf & " AND FTLeaveType = '" & HI.TL.CboList.GetListValue("FNLeaveDay", R!FTLeaveType.ToString) & "'"
                '    HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_HR)

                '    ApproveDataLeave(R!FTEndDate.ToString, R!FTStartDate.ToString, R!FTLeaveType.ToString, R!FNLeaveTotalTime.ToString, R!FNLeaveTotalTimeMin.ToString, R!FTLeavePay.ToString _
                '                     , Val(R!FNHSysEmpId.ToString), R!FTStateMedicalCertificate.ToString, R!FTStaLeaveDay.ToString, R!FTLeaveStartTime.ToString, R!FTLeaveEndTime.ToString, R!FTStaCalSSO.ToString _
                '                     , R!FNLeaveTotalDay.ToString, R!FTStateDeductVacation.ToString)
                'End If

            Next

            For Each R As DataRow In _dt.Select("FTSelect='1' and FTStateType='1'")

                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily"
                _Cmd &= vbCrLf & " Set FTMngApproveState='" & StateApprove & "'"
                _Cmd &= vbCrLf & ", FTMngApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ", FDMngApproveDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ", FTMngApproveTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & " WHERE FNHSysEmpId='" & HI.UL.ULF.rpQuoted(R!FNHSysEmpId.ToString) & "'"
                _Cmd &= vbCrLf & "and FTStartDate='" & HI.UL.ULDate.ConvertEnDB(R!FTStartDate.ToString) & "'"
                _Cmd &= vbCrLf & "and  FTEndDate='" & HI.UL.ULDate.ConvertEnDB(R!FTEndDate.ToString) & "'"

                If HI.Conn.SQLConn.ExecuteOnly(_Cmd, HI.Conn.DB.DataBaseName.DB_HR) = False Then
                    Return False
                End If

            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region
End Class