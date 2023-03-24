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

Public Class wLeaveApproved

    Private _ProcLoad As Boolean = False
    Private _SysPathImageSystem As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images\System"
    ' Private olbhisoft As DevExpress.XtraEditors.LabelControl

    Private DT As DataTable
    Private DTFac As DataTable
    Private DTLeaveWaitApp As DataTable
    Private _ClsService As New ClsService
    Private _ReportPopup As wReportLeavePopup
    Private _LstReport As HI.RP.ListReport

    Friend Shared _CountApp As Integer = 0
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

        _Splash = New HI.TL.SplashScreen("HI SOFT SYSTEM", "Loading Approve Employee Leave..." & "  ")

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

        _LstReport = New HI.RP.ListReport("wEmployeeLeave")
        _ReportPopup = New wReportLeavePopup
        Dim oSysLang As New HI.ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, Me.Name.ToString.Trim, Me)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _ReportPopup.Name.ToString.Trim, _ReportPopup)
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
                    '' .Width = 30

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

        'Call Set_HeadGridLeave()
        ' Call SetSizeGrid()
    End Sub


    Private Sub MainRibbon_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            ClsService.StateLeaveApproved = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MainRibbon_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ClsService.StateLeaveApproved = False
    End Sub

    Private Sub MainRibbon_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ocmapprove.Enabled = True
        ocmreject.Enabled = True
        ocmpreview.Enabled = True
        ocmsavelayout.Enabled = True

        Call ToolBarFunctionActive(Me)
        Call BindGridLeave()
        Try
            Call HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvapproveleave)
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

            'DTAppFactory = Nothing
            'DTAppFactory = LoadFactoryManagerApprove()
            'If Not (DTAppFactory Is Nothing) Then

            '    'Me.otpapppo.PageVisible = False
            '    'Me.otpappfactory.PageVisible = True

            '    ogcdirector.DataSource = DTAppFactory
            '    Dim view As DevExpress.XtraGrid.Views.Grid.GridView
            '    view = ogcdirector.Views(0)
            '    view.OptionsView.ShowAutoFilterRow = True
            '    Me.ogcdirector = view.GridControl
            '    Me.ogcdirector.Refresh()
            '    Call SetSizeGrid()
            '    'Me.Show()
            '    ' Call WriteLog_Director(Date.Now.ToLongTimeString & " DT มีข้อมูล =" & DT.Rows.Count)

            'Else

            '    ogcdirector.DataSource = Nothing
            '    ' Call WriteLog_Director(Date.Now.ToLongTimeString & " DT Nothing")
            'End If


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

    'Public Shared Function LoadFactoryManagerApprove() As DataTable
    '    Try
    '        Dim _str As String = String.Empty
    '        Dim _dt As New DataTable

    '        Dim _LangDisPlay As String = "TH"

    '        If HI.ST.Lang.Language <> Lang.eLang.TH Then
    '            _LangDisPlay = "EN"
    '        End If

    '        _str = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_ORDER_LIST_FACTORY_MANAGER_APPROVE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_LangDisPlay) & "'"
    '        _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_PUR)

    '        If _dt.Rows.Count > 0 Then
    '            _CountApp = _dt.Rows.Count
    '            Return _dt
    '        Else
    '            _CountApp = 0
    '            Return Nothing
    '        End If

    '        _dt.Dispose()

    '    Catch ex As Exception

    '    End Try


    'End Function

    Private Sub Set_HeadGridLeave()
        Try
            With ogvapproveleave
                .OptionsView.ShowAutoFilterRow = False
                .OptionsSelection.MultiSelect = False
                .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
            End With
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Function LoadLeaveWaitApp() As DataTable
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _LangDisPlay As String = "TH"

            If HI.ST.Lang.Language <> ST.Lang.eLang.TH Then
                _LangDisPlay = "EN"
            End If

            _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_LeaveApproved '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_LangDisPlay) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR)

            Return _oDt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub BindGridLeave()
        Try
            Call Set_HeadGridLeave()
            DTLeaveWaitApp = LoadLeaveWaitApp()
            If Not (DTLeaveWaitApp Is Nothing) Then
                Me.FTSelectAllleave.Checked = False
                Me.ogcleaveapp.DataSource = DTLeaveWaitApp
                Call LeaveBal()

            Else
                Me.ogcleaveapp.DataSource = Nothing
            End If
            If DTLeaveWaitApp.Rows.Count <= 0 Then
                ClsService.StateShow = False
                ClsService.StateLeaveApproved = False
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub



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

    Private Sub sBtnSave_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click
        Try
            Select Case Me.otbmain.SelectedTabPage.Name
                Case otpappcminv.Name
                    If ogvapproveleave.RowCount = 0 Then Exit Sub
                    With CType(ogcleaveapp.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTSelect='1'").Length <= 0 Then Exit Sub
                    End With
                    Dim _Spls As New HI.TL.SplashScreen("Approving Employee Leave...,Pease wait...")
                    Try
                        'Dim _Cmd As String = ""
                        '_Cmd = "Select FTUserName From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMSect WITH(NOLOCK) "
                        '_Cmd &= vbCrLf & "Where FTUserName='" & HI.ST.UserInfo.UserName & "'"
                        'If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "") <> "" Then

                        '    'Else
                        '    '    Call _ClsService.UpdateLeave_DocApprove(ogv, "1", "")
                        'End If
                        Call _ClsService.Update_SectApproveEmpLeave(ogvapproveleave, "1", "")

                    Catch ex As Exception
                    End Try
                    _Spls.Close()
            End Select
            Call BindGridLeave()
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
                Case otpappcminv.Name
                    If ogvapproveleave.RowCount = 0 Then Exit Sub
                    With CType(ogcleaveapp.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTSelect='1'").Length <= 0 Then Exit Sub
                    End With
            End Select
            '  Dim _frmshowReject As New wShowRejectDC
            otmchkpo.Enabled = False
            ' _frmshowReject.ShowDialog()
            Select Case Me.otbmain.SelectedTabPage.Name
                Case otpappcminv.Name
                    Dim _Spls As New HI.TL.SplashScreen("Rejecting Employee Leave...,Pease wait.")
                    Try
                        If HI.ST.SysInfo.StateEmpLeaveApp Then
                            Call _ClsService.Update_SectApproveEmpLeave(ogvapproveleave, "0", "")
                        End If
                    Catch ex As Exception
                    End Try
                    _Spls.Close()
            End Select
            otmchkpo.Enabled = True
            Call BindGridLeave()
        Catch ex As Exception
            otmchkpo.Enabled = True
        End Try

    End Sub

    Private Sub otmchkpo_Tick(sender As Object, e As EventArgs) Handles otmchkpo.Tick
        Call BindGridLeave()
        Me.Enabled = True
        ocmapprove.Enabled = True
        ocmreject.Enabled = True
        'ocmpreview.Enabled = True
        If ogvapproveleave.RowCount > 0 Then
            Me.Show()
            If Me.WindowState = FormWindowState.Minimized Then
                Me.WindowState = FormWindowState.Maximized
            End If
        End If
        Call SetSizeGrid()
    End Sub

    'Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
    '    With Me.ogvdocument
    '        If .RowCount <= 0 Then Exit Sub
    '        If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

    '        Dim _Fm As String = ""
    '        _Fm = "{TACCTFactoryCMInvoice.FTCustomerPO}='" & HI.UL.ULF.rpQuoted(.GetRowCellValue(.FocusedRowHandle, "FTCustomerPO").ToString) & "' "
    '        _Fm &= " And {TACCTFactoryCMInvoice.FTInvoiceNo}='" & HI.UL.ULF.rpQuoted(.GetRowCellValue(.FocusedRowHandle, "FTInvoiceNo").ToString) & "' "
    '        With New HI.RP.Report

    '            Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language

    '            If HI.ST.Lang.Language = Lang.eLang.TH Then
    '                HI.ST.Lang.Language = HI.ST.Lang.eLang.TH
    '            Else
    '                HI.ST.Lang.Language = HI.ST.Lang.eLang.EN
    '            End If
    '            .FormTitle = Me.Text
    '            .ReportFolderName = "Account\"
    '            .ReportName = "ReportInvoiceCm.rpt"
    '            .Formular = _Fm
    '            .Preview()
    '            HI.ST.Lang.Language = _tmplang
    '        End With

    '    End With
    'End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        ClsService.StateLeaveApproved = False
        Me.Close()
    End Sub

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvapproveleave)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub


    Private Sub ogvcminv_RowCountChanged(sender As Object, e As EventArgs)
        Try
            Me.otpappcminv.PageVisible = (ogvapproveleave.RowCount > 0)
        Catch ex As Exception
            Me.otpappcminv.PageVisible = False
        End Try
    End Sub

    Private Sub otbmain_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbmain.SelectedPageChanged
        Call TabChange()
    End Sub

    Private Sub TabChange()
        'ocmpreview.Visible = False
        Me.ToolBarFunctionActive(Me)
    End Sub



    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Try
            Dim _oDt As DataTable
            With DirectCast(Me.ogcleaveapp.DataSource, DataTable)
                .AcceptChanges()
                _oDt = .Copy

            End With
            Dim _ReportList As Integer = 0
            With _ReportPopup
                .obtok.Enabled = True
                .obtcancel.Enabled = True
                .ShowDialog()
                If Not (.state) Then Exit Sub
                _ReportList = .List
            End With

            Dim _AllReportName As String = _LstReport.GetValue(_ReportList)
            For Each _ReportName As String In _AllReportName.Split(",")
                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .ReportFolderName = _LstReport.GetFolderReportValue(_ReportList)
                    .ReportName = _ReportName

                    Dim sumBusinessLeave As String = "0"
                    Dim sumSickLeave As String = "0"
                    Dim sumVacationLeave As String = "0"
                    Dim sumAbsent As String = "0"
                    Dim sumLate As String = "0"
                    Dim LeaveVacation As String = "0"
                    Dim strCon As String = ""
                    For Each R As DataRow In _oDt.Select("FTSelect='1'")

                        Call SumLeaveReport(R!FTStartDate.ToString, R!FNHSysEmpID.ToString, sumBusinessLeave, sumSickLeave, sumVacationLeave, sumAbsent, sumLate, LeaveVacation)

                        .AddParameter("sumBusinessLeave", sumBusinessLeave)
                        .AddParameter("sumSickLeave", sumSickLeave)
                        .AddParameter("sumVacationLeave", sumVacationLeave)
                        .AddParameter("sumAbsent", sumAbsent)
                        .AddParameter("sumLate", sumLate)
                        .AddParameter("TotVacation", LeaveVacation)

                        If strCon <> "" Then strCon &= " OR "
                        strCon += "( {THRTLeaveAdvanceDaily.FTStartDate}=  '" & HI.UL.ULDate.ConvertEnDB(R!FTStartDate.ToString) & "' "
                        strCon += "AND {THRTLeaveAdvanceDaily.FTEndDate}='" & HI.UL.ULDate.ConvertEnDB(R!FTEndDate.ToString) & "' "
                        strCon += "AND {THRMEmployee.FNHSysEmpID}=" & Integer.Parse(Val((R!FNHSysEmpID.ToString))) & " )"

                    Next

                    .Formular = strCon
                    .Preview()
                End With
            Next


        Catch ex As Exception

        End Try
    End Sub


    Private Sub SumLeaveReport(FTStartDate As String, FNHSysEmpID As Integer, ByRef sumBusinessLeave As String, ByRef sumSickLeave As String, ByRef sumVacationLeave As String, ByRef sumAbsent As String, ByRef sumLate As String, ByRef LeaveVacation As String)
        Try
            Dim _Qry As String
            Dim _dt As DataTable
            Dim _DateReset As String
            Dim _MsgRet As String = ""
            Dim _Msg As String = ""
            Dim _Month As Integer

            sumBusinessLeave = "0"
            sumSickLeave = "0"
            sumVacationLeave = "0"
            sumAbsent = "0"
            sumLate = "0"
            LeaveVacation = "0"

            If HI.UL.ULDate.CheckDate(FTStartDate) <> "" Then
                _Qry = "  Select  DATEDIFF(MONTH,CONVERT(Datetime,FDDateStart),CONVERT(Datetime,'" & HI.UL.ULDate.ConvertEnDB(FTStartDate) & "'))"
            Else
                _Qry = "  Select  DATEDIFF(MONTH,CONVERT(Datetime,FDDateStart),GetDate())"
            End If

            _Qry &= vbCrLf & " FROM THRMEmployee AS M WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID=" & Val(FNHSysEmpID) & " "

            _Month = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "-1"))

            If _Month >= 0 Then

                '_Qry = "    SELECT TOP 1  VC.FNLeaveRight, VC.FNAgeBegin, VC.FNAgeEnd,  VC.FNHSysEmpTypeId"
                '_Qry &= vbCrLf & " FROM     THRMConfigLeaveVacation AS VC WITH(NOLOCK) INNER JOIN  THRMEmployee AS M WITH(NOLOCK) ON  VC.FNHSysEmpTypeId=M.FNHSysEmpTypeId"
                '_Qry &= vbCrLf & "  WHERE   M.FNHSysEmpID='" & Val(Me.FNHSysEmpId.Properties.Tag.ToString) & "' "
                '_Qry &= vbCrLf & "  AND   VC.FNAgeBegin<=" & _Month & " "
                '_Qry &= vbCrLf & "  AND   VC.FNAgeEnd>=" & _Month & " "

                '_dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                'For Each R As DataRow In _dt.Rows
                '    LeaveVacation = Val(R!FNLeaveRight.ToString)

                'Next


                _Qry = "   SELECT  TOP 1  dbo.FN_Get_Emp_Vacation(FNHSysEmpID,FNHSysEmpTypeId,ISNULL(FDDateStart,''),ISNULL(FDDateEnd,''),ISNULL(FDDateProbation,'')) AS FNEmpVacation"
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee  AS M WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID=" & Val(FNHSysEmpID) & " "

                LeaveVacation = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))

            End If

            _Qry = " SELECT CASE WHEN RiGHT(FTCurrenDate,5) >=FTLeaveReset THEN LEFT(FTCurrenDate,4) ELSE  LEFT(FTBefore,4)  END +'/' + FTLeaveReset"
            _Qry &= vbCrLf & "  FROM"
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT  TOP 1 Convert(varchar(10),GetDate(),111)  AS FTCurrenDate ,Convert(varchar(10),DateAdd(YEAR,-1,GetDate()),111) AS FTBefore,L.FTLeaveReset"
            _Qry &= vbCrLf & " FROM            THRMConfigLeave  AS L WITH (NOLOCK)  INNER JOIN THRMEmployee AS M WITH(NOLOCK )"
            _Qry &= vbCrLf & "  ON  L.FNHSysEmpTypeId=M.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "  WHERE   M.FNHSysEmpID=" & Val(FNHSysEmpID) & " "
            _Qry &= vbCrLf & " ) As T"

            _DateReset = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

            _Qry = "  SELECT      FTLeaveType,  SUM(FNTotalMinute) AS FNTotalMinute "
            _Qry &= vbCrLf & "   FROM THRTTransLeave WITH (NOLOCK)"
            _Qry &= vbCrLf & " WHERE (FNHSysEmpID = " & Val(FNHSysEmpID) & ") "
            _Qry &= vbCrLf & " AND (FTDateTrans >= N'" & _DateReset & "')"
            _Qry &= vbCrLf & " Group By FTLeaveType "

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            sumBusinessLeave = "0"
            sumSickLeave = "0"
            sumVacationLeave = "0"
            Dim _Sum As Integer
            For Each R As DataRow In _dt.Rows

                _Sum = Integer.Parse(Val(R!FNTotalMinute.ToString))

                Select Case R!FTLeaveType.ToString
                    Case "0"
                        sumSickLeave = Format((_Sum \ 480), "00") & ":" & Format(((_Sum Mod 480) \ 60), "00") & ":" & Format(((_Sum Mod 480) Mod 60), "00") & ""
                    Case "1"
                        sumBusinessLeave = Format((_Sum \ 480), "00") & ":" & Format(((_Sum Mod 480) \ 60), "00") & ":" & Format(((_Sum Mod 480) Mod 60), "00") & ""
                    Case "98"
                        sumVacationLeave = Format((_Sum \ 480), "00") & ":" & Format(((_Sum Mod 480) \ 60), "00") & ":" & Format(((_Sum Mod 480) Mod 60), "00") & ""
                End Select
            Next

            _Qry = "  SELECT    SUM(ISNULL(FNLateNormalMin,0)) AS FNLateNormalMin,Sum(ISNULL(FNAbsent,0)) AS FNAbsent "
            _Qry &= vbCrLf & "   FROM THRTTrans WITH (NOLOCK)"
            _Qry &= vbCrLf & " WHERE (FNHSysEmpID = " & Val(FNHSysEmpID) & ") "
            _Qry &= vbCrLf & " AND (FTDateTrans >= N'" & _DateReset & "')"


            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each R As DataRow In _dt.Rows
                _Sum = Integer.Parse(Val(R!FNAbsent.ToString))
                If _Sum > 0 Then
                    sumAbsent = Format((_Sum \ 480), "00") & ":" & Format(((_Sum Mod 480) \ 60), "00") & ":" & Format(((_Sum Mod 480) Mod 60), "00") & ""
                End If

                _Sum = Integer.Parse(Val(R!FNLateNormalMin.ToString))
                If _Sum > 0 Then
                    sumLate = Format((_Sum \ 480), "00") & ":" & Format(((_Sum Mod 480) \ 60), "00") & ":" & Format(((_Sum Mod 480) Mod 60), "00") & ""
                End If

                Exit For
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvLeave_DoubleClick(sender As Object, e As EventArgs) Handles ogvapproveleave.DoubleClick
        Try
            Dim Data_FNHSysEmpID As Integer = 0

            Dim Data_FTLeaveType As String = ""
            Dim Data_FTStaLeaveDay As String = ""
            Dim Data_FTStartDate As String = ""
            Dim Data_FTEndDate As String = ""
            Dim Data_FTLeaveStartTime As String = ""
            Dim Data_FTLeaveEndTime As String = ""
            Dim Data_FTStaCalSSO As String = ""
            Dim Data_FTLeavePay As String = ""
            Dim Data_FTStateDeductVacation As String = ""
            Dim Data_FTLeaveNote As String = ""
            Dim Data_FTHoliday As String = ""

            With ogvapproveleave
                Data_FNHSysEmpID = .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID")

                Data_FTLeaveType = .GetRowCellValue(.FocusedRowHandle, "FTLeaveType")
                Data_FTStaLeaveDay = .GetRowCellValue(.FocusedRowHandle, "FTStaLeaveDay")

                Try
                    Data_FTStartDate = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTStartDate").ToString)
                Catch ex As Exception
                End Try

                Try
                    Data_FTEndDate = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTEndDate").ToString)
                Catch ex As Exception
                End Try

                Data_FTLeaveStartTime = "" & .GetRowCellValue(.FocusedRowHandle, "FTLeaveStartTime").ToString
                Data_FTLeaveEndTime = "" & .GetRowCellValue(.FocusedRowHandle, "FTLeaveEndTime").ToString

                Data_FTStaCalSSO = ("" & .GetRowCellValue(.FocusedRowHandle, "FTStaCalSSO").ToString = "1")
                Data_FTLeavePay = ("" & .GetRowCellValue(.FocusedRowHandle, "FTLeavePay").ToString = "1")
                Data_FTStateDeductVacation = ("" & .GetRowCellValue(.FocusedRowHandle, "FTStateDeductVacation").ToString = "1")

                Data_FTLeaveNote = "" & .GetRowCellValue(.FocusedRowHandle, "FTLeaveNote").ToString
                Data_FTHoliday = ("" & .GetRowCellValue(.FocusedRowHandle, "FTHoliday").ToString="1")

            End With

            Dim _Cmd As String = "Select top 1  FTEmpCode from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee with(nolock) where FNHSysEmpId=" & Data_FNHSysEmpID
            Dim empcode As String = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "")
            Dim _oDt As DataTable

            If HI.ST.Lang.Language = Lang.eLang.TH Then
                _Cmd = "SELECT  top 1    M.FTEmpCode, P.FTPreNameNameTH + ' ' +  M.FTEmpNameTH +' ' +  M.FTEmpSurnameTH AS FTDescription, CASE WHEN ISDAte(M.FDDateStart) = 1 THEN CONVERT(Varchar(10),Convert(Datetime,M.FDDateStart),103) ELSE '' END As  FDDateStart, M.FNHSysEmpID,CASE WHEN ISDAte(M.FDDateEnd) = 1 THEN CONVERT(Varchar(10),Convert(Datetime,M.FDDateEnd),103) ELSE '' END As  FDDateEnd,FTEmpCodeRefer FROM            [HITECH_HR].dbo.THRMEmployee  AS M WITH (NOLOCK) INNER JOIN HITECH_MASTER.dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId"
                _Cmd &= vbCrLf & " where M.FNHSysEmpId=" & Data_FNHSysEmpID
            Else
                _Cmd = "SELECT   Top 1   M.FTEmpCode, P.FTPreNameNameEN + ' ' +  M.FTEmpNameEN +' ' +  M.FTEmpSurnameEN AS FTDescription, CASE WHEN ISDAte(M.FDDateStart) = 1 THEN CONVERT(Varchar(10),Convert(Datetime,M.FDDateStart),103) ELSE '' END As  FDDateStart, M.FNHSysEmpID,CASE WHEN ISDAte(M.FDDateEnd) = 1 THEN CONVERT(Varchar(10),Convert(Datetime,M.FDDateEnd),103) ELSE '' END As  FDDateEnd,FTEmpCodeRefer FROM            [HITECH_HR].dbo.THRMEmployee  AS M WITH (NOLOCK) INNER JOIN HITECH_MASTER.dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId"
                _Cmd &= vbCrLf & " where M.FNHSysEmpId=" & Data_FNHSysEmpID
            End If
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR)

            ' Dim _WformLeave As New wEmployeeLeave

            'Call HI.TL.CboList.PrepareList()
            Dim _WformPo As New wEmployeeLeave

            'wEmployeeLeave_H = New wEmployeeLeave
            ''Call HI.TL.HandlerControl.AddHandlerObj(_WformPo)
            'With _WformPo

            '    .ocmexit.Visible = False
            '    .ocmclear.Visible = False
            '    .FNHSysEmpId.Properties.ReadOnly = True
            '    .FNHSysEmpId.Properties.Buttons(0).Enabled = False
            '    .FNHSysEmpId.Text = empcode
            '    .FNHSysEmpId_None.Text = _oDt.Rows(0).Item("FTDescription").ToString


            '    .FNLeaveType.SelectedIndex = HI.TL.CboList.GetIndexByValue(.FNLeaveType.Properties.Tag.ToString, "" & Data_FTLeaveType)
            '    .FNLeaveDay.SelectedIndex = HI.TL.CboList.GetIndexByValue(.FNLeaveDay.Properties.Tag.ToString, "" & Data_FTStaLeaveDay)


            '    .FTStartDate.DateTime = Data_FTStartDate
            '    .FTEndDate.DateTime = Data_FTEndDate

            '    .FTStateCalSSo.Checked = Data_FTStaCalSSO
            '    .FTStateLeavepay.Checked = Data_FTLeavePay
            '    .FTStateDeductVacation.Checked = Data_FTStateDeductVacation



            '    .FTRemark.Text = Data_FTLeaveNote

            '    .FNLeaveType_param.Text = Data_FTLeaveType
            '    .FTStartDate_param.Text = Data_FTStartDate
            '    .FTStateNotMergeHoliday.Checked = Data_FTHoliday
            '    .FTStateNotMergeHoliday_param.Checked = Data_FTHoliday
            '    '.FNHSysEmpId.Properties.Buttons(1).Enabled = False
            '    ' Call wLeave_Load




            'End With

            'Call HI.TL.HandlerControl.AddHandlerObj(_WformPo)


            ' Dim _WShow As New wShowData(_WformPo, _PurchaseNo)


            Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
            HI.ST.SysInfo.MenuName = "MnuLeave"
            Dim _WShow As New wShowData(_WformPo, empcode)
            HI.ST.SysInfo.MenuName = _TmpMenu

            With _WShow




                With DirectCast(.MdiChildren(0), wEmployeeLeave)


                    .ocmexit.Visible = False
                    .ocmclear.Visible = False
                    .FNHSysEmpId.Properties.ReadOnly = True
                    .FNHSysEmpId.Properties.Buttons(0).Enabled = False
                    .KeyCode = empcode
                    .LoadKey()
                    .FNHSysEmpId_None.Text = _oDt.Rows(0).Item("FTDescription").ToString


                    .FNLeaveType.SelectedIndex = HI.TL.CboList.GetIndexByValue(.FNLeaveType.Properties.Tag.ToString, "" & Data_FTLeaveType)
                    .FNLeaveDay.SelectedIndex = HI.TL.CboList.GetIndexByValue(.FNLeaveDay.Properties.Tag.ToString, "" & Data_FTStaLeaveDay)
                    .FNLeaveDay_param.Text = Data_FTStaLeaveDay

                    .FTSTime.Text = Data_FTLeaveStartTime
                    .FTETime.Text = Data_FTLeaveEndTime
                    .FTSTime_param.Text = Data_FTLeaveStartTime
                    .FTETime_param.Text = Data_FTLeaveEndTime

                    .FTStartDate.DateTime = Data_FTStartDate
                    .FTEndDate.DateTime = Data_FTEndDate


                    .FTStateCalSSo.Checked = Data_FTStaCalSSO
                    .FTStateLeavepay.Checked = Data_FTLeavePay
                    .FTStateDeductVacation.Checked = Data_FTStateDeductVacation



                    .FTRemark.Text = Data_FTLeaveNote

                    .FNLeaveType_param.Text = Data_FTLeaveType
                    .FTStartDate_param.Text = Data_FTStartDate


                    .FTStateNotMergeHoliday.Checked = Data_FTHoliday
                    .FTStateNotMergeHoliday_param.Checked = Data_FTHoliday
                End With
                .WindowState = System.Windows.Forms.FormWindowState.Maximized
                .StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                .ShowDialog()
            End With


        Catch ex As Exception
        End Try
    End Sub





    'Private Sub ogvdocument_DoubleClick(sender As Object, e As EventArgs)
    '    Try
    '        Dim data As Byte() : Dim _FileType As Integer = 0
    '        With ogv
    '            data = .GetRowCellValue(.FocusedRowHandle, "FBDocument")
    '            _FileType = CInt("0" & .GetRowCellValue(.FocusedRowHandle, "FNFileType").ToString)
    '        End With
    '        Dim _frm As New DevExpress.XtraEditors.XtraForm
    '        Dim _UsrC As New ucShowDoc(data, _FileType)
    '        _frm.Controls.Add(_UsrC)
    '        _UsrC.Dock = DockStyle.Fill
    '        With _frm
    '            .WindowState = FormWindowState.Maximized
    '            .ShowDialog()
    '        End With
    '    Catch ex As Exception
    '    End Try
    'End Sub
    Protected Sub ShowLeaveInfo(ByVal EmpCode As String)


        Dim _Qry As String
        Dim tResetLeave As String

        Try
            _Qry = "SELECT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee"
            Dim tEmpType As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR)

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US", True)
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd"
            Dim LeaveVacation As Double = 0

            _Qry = "   SELECT  TOP 1  dbo.FN_Get_Emp_Vacation(FNHSysEmpID,FNHSysEmpTypeId,ISNULL(FDDateStart,N''),ISNULL(FDDateEnd,N''),ISNULL(FDDateProbation,N'')) AS FNEmpVacation"
            _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee  AS M WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID=" & Val(EmpCode) & " "

            LeaveVacation = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))

            _Qry = " SELECT CASE WHEN RiGHT(FTCurrenDate,5) >=FTLeaveReset THEN LEFT(FTCurrenDate,4) ELSE  LEFT(FTBefore,4)  END +'/' + FTLeaveReset AS FTLeaveReset"
            _Qry &= vbCrLf & "  FROM"
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT  TOP 1 Convert(varchar(10),GetDate(),111)  AS FTCurrenDate ,Convert(varchar(10),DateAdd(YEAR,-1,GetDate()),111) AS FTBefore,L.FTLeaveReset"
            _Qry &= vbCrLf & " FROM  THRMConfigLeave  AS L WITH (NOLOCK)  INNER JOIN THRMEmployee AS M WITH(NOLOCK )"
            _Qry &= vbCrLf & "  ON  L.FNHSysEmpTypeId=M.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "  WHERE   M.FNHSysEmpID=" & Val(EmpCode) & " "
            _Qry &= vbCrLf & " ) As T"

            tResetLeave = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")


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
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_LeaveType WITH(NOLOCK)"
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
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_LeaveType WITH(NOLOCK)"
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

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Catch ex As Exception
        End Try

    End Sub
    Dim _dt As DataTable
    Sub LeaveBal()

        For k As Integer = 0 To ogvapproveleave.RowCount - 1 Step 1
            Call ShowLeaveInfo(ogvapproveleave.GetRowCellValue(k, "FNHSysEmpID"))
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
    End Sub


End Class