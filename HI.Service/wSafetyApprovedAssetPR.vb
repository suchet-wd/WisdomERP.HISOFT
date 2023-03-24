﻿Imports DevExpress.XtraBars

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

Public Class wSafetyApprovedAssetPR

    Private _ProcLoad As Boolean = False
    Private _SysPathImageSystem As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images\System"
    ' Private olbhisoft As DevExpress.XtraEditors.LabelControl

    Private DT As DataTable
    Private _ClsService As New ClsService

    Friend Shared _CountAppAssetPRSa As Integer = 0
    '  Private Shared _frmApp As New wDirectorApproved ' = Nothing.000000

    Friend Shared DTPRPurchaseAssetNoSa As DataTable
    Friend Property Data_DTPRPurchaseAssetNoSa As DataTable
        Get
            Return DTPRPurchaseAssetNoSa
        End Get
        Set(ByVal value As DataTable)
            DTPRPurchaseAssetNoSa = value
        End Set
    End Property

    Sub New()
        _ProcLoad = True

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _Splash = New HI.TL.SplashScreen("Wisdom System", "Loading Approve Purchase Order..." & "  ")

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
    Private tW_SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
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
                        ClsService.StateAssetPRShow = False
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

                    Dim tPathImg As String = tW_SysPath & "\Func\" & oDbRow("FTFuncImg").ToString.Trim
                    If IO.File.Exists(tPathImg) Then
                        .Glyph = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg))) ' Image.FromFile(tPathImg)
                        '.GlyphDisabled = Image.FromFile(tPathImg)
                    End If

                    Dim tPathImgDis As String = tW_SysPath & "\Func\" & oDbRow("FTFuncImgDisabled").ToString.Trim
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
        ClsService.StateAssetPRShowSa = False
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
            ClsService.StateAssetPRShowSa = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MainRibbon_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ClsService.StateAssetPRShowSa = False
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
            Call HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvSupervisorApproved)
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


        '----------- Read Connecttion String From File XML
        '  HI.Conn.DB.GetXmlConnectionString()

        ' System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  ผ่าน HI.Conn.DB.GetXmlConnectionString")

        ' Log("ValidateApp")

        If FindComputerName(Environment.MachineName.ToString()) Then

            'Log(Environment.MachineName.ToString())
            'Dim _App As String = HI.Conn.SQLConn.GetField("Select isnull(FTStateApp,'0')as FTStateApp  from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request As P where P.FTStateApp = '1'", Conn.DB.DataBaseName.DB_SYSTEM, "")
            'Dim _MApp As String = HI.Conn.SQLConn.GetField("Select isnull(FTStateManagerApp,'0')as FTStateApp  from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request As P where P.FTStateManagerApp = '0' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
            '  HI.ST.SysInfo.StateDirector = True     ' true ทดสอบ Super     false ทดสอบ Manager
            ' กลับมาเรียก Function ของตัวเอง




            DTPRPurchaseAssetNoSa = Nothing
            'If _App = _MApp Then




            DTPRPurchaseAssetNoSa = LoadogcTPURTPurchase()
            'Else 
            '    DTPurchaseNo = LoadogcTPURTPurchaseDi()
            'End If

            'Dim _dt As DataTable
            'DT = Nothing
            'With CType(ogSupervisorApproved.DataSource, DataTable)
            '    .AcceptChanges()
            '    _dt = .Copy
            '    For Each R As DataRow In DT.Select("FTPRPurchaseNo <> '' ", "FTStateApproved")
            '        Dim _App As String = HI.Conn.SQLConn.GetField("Select isnull(FTStateApp,'0')as FTStateApp  from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request As P where P.FTPRPurchaseNo = '" & R!FTPRPurchaseNo.ToString & "'", Conn.DB.DataBaseName.DB_SYSTEM, "")


            '        If _App = "0" Then
            '            DT = LoadogcTPURTPurchase()
            '        Else
            '            DT = LoadogcTPURTPurchaseDi()
            '        End If

            '    Next

            'End With

            '  DTPurchaseNo = LoadogcTPURTPurchase()
            If _CountAppAssetPRSa > 0 Then

                '    Log("_CountApp = " & _CountApp)

                If Not (DTPRPurchaseAssetNoSa Is Nothing) Then


                    ogSupervisorApproved.DataSource = DTPRPurchaseAssetNoSa
                    Dim view As DevExpress.XtraGrid.Views.Grid.GridView
                    view = ogSupervisorApproved.Views(0)
                    view.OptionsView.ShowAutoFilterRow = True
                    Me.ogSupervisorApproved = view.GridControl
                    Me.ogSupervisorApproved.Refresh()
                    Call SetSizeGrid()
                    'Me.Show()
                    ' Call WriteLog_Director(Date.Now.ToLongTimeString & " DT มีข้อมูล =" & DT.Rows.Count)

                Else
                    ogSupervisorApproved.DataSource = Nothing
                    ' Call WriteLog_Director(Date.Now.ToLongTimeString & " DT Nothing")

                End If

                'Shell("", AppWinStyle.Hide)


                'If _frmApp Is Nothing Then
                '    _frmApp = New wDirectorApproved
                'ElseIf _frmApp.IsDisposed Then
                '    _frmApp = New wDirectorApproved
                'End If

                ' HI.TL.HandlerControl.AddHandlerObj(_frmApp)
                'HI.TL.HandlerControl.AddHandlerObj(Me.Name)


                'Dim oSysLang As New HI.ST.SysLanguage
                'Try
                '    ' Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmApp.Name.ToString.Trim, _frmApp)
                '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name, Me)


                '    ' MsgBox(" ผ่าน oSysLang.LoadObjectLanguage")

                'Catch ex As Exception
                'Finally
                'End Try

                'Try
                '    '_frmApp.Show()
                '    '_frmApp.BringToFront()
                '    Me.Show()
                '    Me.BringToFront()

                'Catch ex As Exception
                'End Try
            Else
                '    Log("_CountApp = " & _CountApp)
                ' Log("Application.Exit()")
                '     Log(" Me.Hide()")
                ' Application.Exit()


                '  Me.Hide()

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

        Try
            With ogvSupervisorApproved
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
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFNPOGrandAmt").Width = 1100
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFTRemark").Width = 100
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFTTeamGrpCode").Width = 100
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFTTeamGrpName").Width = 130
        'Catch ex As Exception

        'End Try
    End Sub

    Public Shared Function LoadogcTPURTPurchase() As DataTable



        Try

            Dim _str As String = String.Empty
            Dim _dt As New DataTable


            _str = ""
            _str = "SELECT  isnull(A.FTStateApp,0) as FTStateApproved, A.FTPRPurchaseNo,"
            _str &= Environment.NewLine & "  SUBSTRING(A.FDPRPurchaseDate,9,2) + '/'+ SUBSTRING(A.FDPRPurchaseDate,6,2) + '/' + SUBSTRING(A.FDPRPurchaseDate,1,4) as FDPRPurchaseDate,"
            _str &= Environment.NewLine & " ISNULL( A.FTPRPurchaseBy,'') as FTPRPurchaseBy, "
            _str &= Environment.NewLine & " ISNULL( A.FTAppName,'') as FTAppName, "
            _str &= Environment.NewLine & " isnull(LD.FTNameTH,'') as FNPRState,"
            _str &= Environment.NewLine & " ISNULL(A.FTRemark,'') as FTRemark,"
            _str &= Environment.NewLine & " ISNULL(Convert(numeric(18,2),A.FNNetAmt),0) as FNNetAmt, ISNULL(Convert(numeric(18,2),A.FNQuantity),0) as FNQuantity,"
            _str &= Environment.NewLine & " ISNULL(S.FTUserName,'') as FTSafetyName, A.FNFixedAssetType"
            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetSafety as S with (nolock)  "
            _str &= Environment.NewLine & "LEFT OUTER JOIN (SELECT A.FNFixedAssetType,A.FNHSysAssetTyped,A.FNHSysFixedAssetId,A.FTAssetNameTH"
            _str &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS A "
            _str &= Environment.NewLine & "UNION "
            _str &= Environment.NewLine & "SELECT '1'AS FNFixedAssetType,A.FNHSysAssetPartTyped,A.FNHSysAssetPartId,A.FTAssetPartNameTH"
            _str &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart AS A)AS T ON S.FNFixedAssetType=T.FNFixedAssetType AND S.FNHSysAssetTyped=T.FNHSysAssetTyped"
            _str &= Environment.NewLine & "LEFT OUTER JOIN[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.V_TFIXEDTPurchase_Request AS A  with (nolock) ON T.FNFixedAssetType=A.FNFixedAssetType AND T.FNHSysFixedAssetId=A.FNHSysFixedAssetId "
            ' _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin as U with (nolock) ON A.FTPRPurchaseBy =U.FTUserName  "
            '_str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetConfigLevel AS L with (nolock) ON A.FNFixedAssetType=L.FNFixedAssetType AND A.FNNetAmt>=L.FNStartQty  AND U.FTManagerUserName=L.FTUserName AND A.FNHSysCmpId=L.FNHSysCmpId "
            _str &= Environment.NewLine & "LEFT OUTER JOIN  (select LD.FTNameTH,LD.FNListIndex"
            _str &= Environment.NewLine & " from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS LD with (nolock) "
            _str &= Environment.NewLine & " where LD.FTListName='FNPRState'  )as LD ON A.FNPRState=LD.FNListIndex"
            _str &= Environment.NewLine & " WHERE (S.FTUserName = '" & HI.ST.UserInfo.UserName & "')"
            _str &= Environment.NewLine & " AND (a.FTStateSendApp = '1') AND (A.FTStateSafety='0') "
            _str &= Environment.NewLine & " Order by A.FTPRPurchaseBy, a.FDPRPurchaseDate"
            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_MASTER)
            ' _dt.FNNetAmt = Format(_str + Double.Parse("0" & Me.LabelControl3.Text), "#,##0.00")
            If _dt.Rows.Count > 0 Then
                _CountAppAssetPRSa = _dt.Rows.Count
                Return _dt
            Else
                _CountAppAssetPRSa = 0
                Return Nothing
            End If

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Function

    'Public Shared Function LoadogcTFIXEDTPurchase_RequestFA() As DataTable


    '    Try
    '        Dim _str As String = String.Empty
    '        Dim _dt As New DataTable

    '        _str = ""
    '        _str = "SELECT  isnull(A.FTStateApp,0) as FTStateApproved, A.FTPRPurchaseNo,A.FNHSysCmpId,"
    '        _str &= Environment.NewLine & "  SUBSTRING(A.FDPRPurchaseDate,9,2) + '/'+ SUBSTRING(A.FDPRPurchaseDate,6,2) + '/' + SUBSTRING(A.FDPRPurchaseDate,1,4) as FDPRPurchaseDate,"
    '        _str &= Environment.NewLine & " ISNULL( A.FTPRPurchaseBy,'') as FTPRPurchaseBy, "
    '        _str &= Environment.NewLine & " ISNULL( A.FTAppName,'') as FTAppName, "
    '        _str &= Environment.NewLine & " isnull(LD.FTNameTH,'') as FNPRState,"
    '        _str &= Environment.NewLine & " ISNULL(A.FTRemark,'') as FTRemark,"
    '        _str &= Environment.NewLine & " ISNULL(Convert(numeric(18,2),A.FNNetAmt),0) as FNNetAmt, ISNULL(Convert(numeric(18,2),A.FNQuantity),0) as FNQuantity,"
    '        _str &= Environment.NewLine & " ISNULL(L.FTUserName,'') as FTUserName, A.FNFixedAssetType"
    '        _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.V_TFIXEDTPurchase_Request AS A  with (nolock) LEFT OUTER JOIN "
    '        _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C with (nolock) ON A.FNHSysCmpId=C.FNHSysCmpId   LEFT OUTER JOIN"
    '        _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetConfigLevel AS L with (nolock) ON A.FNFixedAssetType=L.FNFixedAssetType AND A.FNNetAmt>=L.FNStartQty AND C.FTUserName=L.FTUserName  AND A.FNHSysCmpId=L.FNHSysCmpId "
    '        '_str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp as C ON  B.FNHSysTeamGrpId = C.FNHSysTeamGrpId " 'L.FTUserName=C.FTUserName" '  "
    '        _str &= Environment.NewLine & "LEFT OUTER JOIN  (select LD.FTNameTH,LD.FNListIndex"
    '        _str &= Environment.NewLine & " from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS LD with (nolock) "
    '        _str &= Environment.NewLine & " where LD.FTListName='FNPRState'  )as LD ON A.FNPRState=LD.FNListIndex"
    '        _str &= Environment.NewLine & " WHERE (L.FTUserName = '" & HI.ST.UserInfo.UserName & "')"
    '        _str &= Environment.NewLine & " AND (a.FTStateSendApp = '1') AND (a.FTStateApp = '0')  " 'AND L.FTStateFactory='1'" ' or (a.FTStateApp = '3'))"
    '        _str &= Environment.NewLine & "group by A.FTStateApp,A.FTPRPurchaseNo,A.FDPRPurchaseDate,A.FTPRPurchaseBy,A.FTAppName,LD.FTNameTH,A.FTRemark,A.FNQuantity,L.FTUserName, A.FNFixedAssetType,A.FNNetAmt,A.FNHSysCmpId"
    '        _str &= Environment.NewLine & " Order by A.FTPRPurchaseBy, a.FDPRPurchaseDate"


    '        _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_MASTER)

    '        If _dt.Rows.Count > 0 Then
    '            _CountAppAssetPRSa = _dt.Rows.Count
    '            Return _dt
    '        Else
    '            _CountAppAssetPRSa = 0
    '            Return Nothing
    '        End If

    '        _dt.Dispose()


    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Function

    Private Sub BindGrid()
        Try



            Call Set_HeadGrid()

            DT = Nothing

            'Dim _dtcheck As DataTable
            'Dim _str As String
            '_str = "SELECT PR.FNFixedAssetType,PR.FTPRPurchaseNo,U.FTManagerUserName as FTUserName"
            '_str &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.V_TFIXEDTPurchase_Request AS PR WITH(NOLOCK) LEFT OUTER JOIN "
            '_str &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U  WITH(NOLOCK) ON  PR.FTPRPurchaseBy =U.FTUserName  "
            '_str &= vbCrLf & " WHERE U.FTManagerUserName='" & HI.ST.UserInfo.UserName & "'"

            '_dtcheck = HI.Conn.SQLConn.GetDataTable(_str, Conn.DB.DataBaseName.DB_MASTER)


            'For Each Rx As DataRow In _dtcheck.Rows


            '    Dim _USER As String = Rx!FTUserName.ToString


            DT = LoadogcTPURTPurchase()


            'Exit For
            'Next



            If Not (DT Is Nothing) Then

                Dim _FocusRowInDex As Integer = -1
                Dim _FTPurchaseNo As String = ""


                Try

                    If ogvSupervisorApproved.FocusedRowHandle > 0 Then

                        _FocusRowInDex = ogvSupervisorApproved.FocusedRowHandle
                        _FTPurchaseNo = ogvSupervisorApproved.GetRowCellValue(ogvSupervisorApproved.FocusedRowHandle, "FTPRPurchaseNo").ToString

                    End If

                Catch ex As Exception
                End Try

                ogSupervisorApproved.DataSource = DT
                Dim view As DevExpress.XtraGrid.Views.Grid.GridView
                view = ogSupervisorApproved.Views(0)
                view.OptionsView.ShowAutoFilterRow = True
                Me.ogSupervisorApproved = view.GridControl
                Me.ogSupervisorApproved.Refresh()

                _FocusRowInDex = -1
                With Me.ogvSupervisorApproved
                    If .RowCount > 0 And _FTPurchaseNo <> "" Then

                        Try
                            _FocusRowInDex = .LocateByValue("FTPRPurchaseNo", _FTPurchaseNo)
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

                Call SetSizeGrid()
                ochkselectall.Checked = False

                ' Call WriteLog_Director(Date.Now.ToLongTimeString & " DT มีข้อมูล =" & DT.Rows.Count)

            Else
                ogSupervisorApproved.DataSource = Nothing
                ClsService.StateAssetPRShowSa = False
                Me.Close()
                ' Call WriteLog_Director(Date.Now.ToLongTimeString & " DT Nothing")

            End If


        Catch ex As Exception

        End Try

    End Sub

    'Friend Function Update_ManagerApproved(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String) As Boolean

    '    Dim _Str As String = String.Empty
    '    Dim _FTMailId As Long

    '    Dim _aPurchaseBy() As String
    '    Dim _atPurchaseNo() As String
    '    Dim _IntCount As Integer = 0

    '    Try

    '        ReDim _aPurchaseBy(TempGrid.RowCount - 1)
    '        ReDim _atPurchaseNo(TempGrid.RowCount - 1)

    '        For k = 0 To TempGrid.RowCount - 1
    '            _aPurchaseBy(k) = ""
    '            _atPurchaseNo(k) = ""
    '        Next

    '        _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(_IntCount, "FTPurchaseBy").ToString()

    '        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '        For i = 0 To TempGrid.RowCount - 1

    '            If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then
    '                _Str = ""
    '                _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
    '                _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
    '                _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
    '                _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
    '                _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
    '                _Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
    '                _Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
    '                _Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB
    '                _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTPurchase] "
    '                _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"

    '                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                    HI.Conn.SQLConn.Tran.Rollback()
    '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                    Return False
    '                End If

    '                If _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString() Then

    '                    If _atPurchaseNo(_IntCount) = String.Empty Then
    '                        _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
    '                    Else
    '                        _atPurchaseNo(_IntCount) &= " ;" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
    '                    End If

    '                Else
    '                    _IntCount = _IntCount + 1
    '                    _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString()
    '                    _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()

    '                End If

    '            End If
    '        Next

    '        For j = 0 To _IntCount

    '            ' ส่งเมล Approved ไปหา SuperVisor   FNMailStateType = 0

    '            _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
    '            _Str = ""
    '            _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
    '            _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
    '            _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
    '            _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
    '            _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
    '            _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
    '            _Str &= ",'Approved PurchaseNo   << Converter File to PDF >> ','" & _atPurchaseNo(j) & "' ,0,1,0,0,0,0,"
    '            _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"

    '            If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                HI.Conn.SQLConn.Tran.Rollback()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                Return False
    '            End If

    '            ' ส่งเมล Approved ไปหา SuperVisor   FNMailStateType = 1

    '            _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
    '            _Str = ""
    '            _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
    '            _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
    '            _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
    '            _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
    '            _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
    '            _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
    '            _Str &= ",'Approved PurchaseNo  << Converter File to PDF >> ','" & _atPurchaseNo(j) & "' ,0,1,0,0,0,0,"
    '            _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"

    '            If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                HI.Conn.SQLConn.Tran.Rollback()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                Return False
    '            End If

    '            ' กรณีส่งหาตัวเอง
    '            'If TempGrid.GetRowCellValue(i, "FTSuperVisorName").ToString().Trim = HI.ST.UserInfo.UserName Then
    '            '     _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
    '            '    _Str = ""
    '            '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
    '            '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
    '            '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
    '            '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
    '            '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
    '            '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & TempGrid.GetRowCellValue(i, "FTSuperVisorName").ToString() & "'"
    '            '    _Str &= ",'Approved  " & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "  << Converter File to PDF >>' ,0,0,1,0,0,0,"
    '            '    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

    '            '    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '            '        HI.Conn.SQLConn.Tran.Rollback()
    '            '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '            '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '            '        Return False
    '            '    End If

    '            'End If


    '        Next


    '        HI.Conn.SQLConn.Tran.Commit()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

    '        Return True
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Return False
    '    End Try


    'End Function

    'Friend Function Update_ManagerApproved(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String, ByVal TempRemark As String) As Boolean

    '    Dim _Str As String = String.Empty
    '    Dim _FTMailId As Long
    '    Dim _aPurchaseBy() As String
    '    Dim _atPurchaseNo() As String
    '    Dim _IntCount As Integer = 0


    '    Try

    '        ReDim _aPurchaseBy(TempGrid.RowCount - 1)
    '        ReDim _atPurchaseNo(TempGrid.RowCount - 1)

    '        For k = 0 To TempGrid.RowCount - 1
    '            _aPurchaseBy(k) = ""
    '            _atPurchaseNo(k) = ""
    '        Next

    '        _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(_IntCount, "FTPurchaseBy").ToString()

    '        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
    '        HI.Conn.SQLConn.SqlConnectionOpen()
    '        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
    '        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

    '        For i = 0 To TempGrid.RowCount - 1

    '            If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then
    '                _Str = ""
    '                _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
    '                _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
    '                _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
    '                _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
    '                _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
    '                ' _Str &= Environment.NewLine & ", [FTRemark] = '" & TempRemark & "'"
    '                _Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
    '                _Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
    '                _Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB
    '                _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTPurchase] "
    '                _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"

    '                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                    HI.Conn.SQLConn.Tran.Rollback()
    '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                    Return False
    '                End If

    '                If _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString() Then

    '                    If _atPurchaseNo(_IntCount) = String.Empty Then
    '                        _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
    '                    Else
    '                        _atPurchaseNo(_IntCount) &= " ;" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
    '                    End If

    '                Else
    '                    _IntCount = _IntCount + 1
    '                    _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString()
    '                    _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()

    '                End If


    '            End If

    '        Next



    '        For j = 0 To _IntCount
    '            ' ส่งเมลกลับกรณี Reject

    '            ' _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
    '            '_Str = ""
    '            '_Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
    '            '_Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
    '            '_Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
    '            '_Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
    '            '_Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
    '            '_Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString() & "'"
    '            '_Str &= ",'Reject  " & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "' ,0,1,0,0,0,0,"
    '            '_Str &= "@FTMailText,'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

    '            'HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark



    '            _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
    '            _Str = ""
    '            _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
    '            _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
    '            _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
    '            _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
    '            _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
    '            _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
    '            _Str &= ",'Reject PurchaseNo',0,1,0,0,0,0,"
    '            _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"

    '            'HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark


    '            If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                HI.Conn.SQLConn.Tran.Rollback()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                Return False
    '            End If


    '            _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
    '            _Str = ""
    '            _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
    '            _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
    '            _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
    '            _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
    '            _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
    '            _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
    '            _Str &= ",'Reject PurchaseNo' ,0,1,0,0,0,0,"
    '            _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"

    '            'HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark

    '            If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                HI.Conn.SQLConn.Tran.Rollback()
    '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                Return False
    '            End If

    '            ' กรณีส่ง Mail ให้ตัวเอง
    '            If _atPurchaseNo(j) = HI.ST.UserInfo.UserName Then
    '                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
    '                _Str = ""
    '                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
    '                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
    '                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
    '                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
    '                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
    '                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
    '                _Str &= ",'Reject PurchaseNo',1,0,0,0,0,0,"
    '                _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"
    '                ' _Str &= "@FTMailText,'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

    '                ' HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark

    '                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
    '                    HI.Conn.SQLConn.Tran.Rollback()
    '                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    '                    Return False
    '                End If

    '            End If

    '        Next

    '        HI.Conn.SQLConn.Tran.Commit()
    '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
    '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

    '        Return True
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Return False
    '    End Try

    'End Function


    Private Sub ochkselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ochkselectall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With ogSupervisorApproved
                If Not (.DataSource Is Nothing) And ogvSupervisorApproved.RowCount > 0 Then

                    With ogvSupervisorApproved
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
            Dim dt As DataTable
            With CType(ogSupervisorApproved.DataSource, DataTable)
                .AcceptChanges()
                dt = .Copy
                For Each R As DataRow In dt.Select("FTPRPurchaseNo <> '' ", "FTStateApproved")
                    If R!FTStateApproved.ToString = "1" Then
                        'Dim _App As Double = HI.Conn.SQLConn.GetField("Select (Convert(numeric(18, 0),FNEndQty)) as FNEndQty  from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetConfigLevel As P where P.FTUserName = '" & HI.ST.UserInfo.UserName & "' and P.FNFixedAssetType ='" & R!FNFixedAssetType.ToString & "'  and P.FNHSysCmpId ='" & R!FNHSysCmpId.ToString & "'", Conn.DB.DataBaseName.DB_SYSTEM, "")
                        'Dim _Pur As Double = HI.Conn.SQLConn.GetField("Select sum(Convert(numeric(18, 0),FNNetAmt)) as FNNetAmt from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail As P where P.FTPRPurchaseNo = '" & R!FTPRPurchaseNo.ToString & "'", Conn.DB.DataBaseName.DB_SYSTEM, "")
                        'Dim _App1 As Double = HI.Conn.SQLConn.GetField("Select (Convert(numeric(18, 0),FNStartQty)) as FNStartQty  from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetConfigLevel As P where P.FTUserName = '" & HI.ST.UserInfo.UserName & "'", Conn.DB.DataBaseName.DB_SYSTEM, "")




                        If ogvSupervisorApproved.RowCount = 0 Then Exit Sub


                        '   If _Pur >= _App1 Then
                        'If _Pur <= _App Then
                        Call _ClsService.Update_SafetyApprovedAssetPR(ogvSupervisorApproved, 1)
                        '        Else
                        '            Call _ClsService.Update_SupervisorApprovedAssetPRDi(ogvSupervisorApproved, 1)                
                        Call BindGrid()
                        'Else
                        '    ' HI.MG.ShowMsg.mInfo("ไม่สามารถ อนุมัติ ได้ เนื่องจากเกินวงเงินที่กำหนดไว้ !!!!", 1802050001, Me.Text)


                        '    If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mSave, "เนื่องจากเกินวงเงินที่กำหนดไว้ ต้องการส่งต่อไปให้ Director หรือไม่!!!!", Me.Text) = True Then

                        '        '  HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                        '        '   HI.MG.ShowMsg.mInfo ("ไม่สามารถ อนุมัติ ได้ เนื่องจากเกินวงเงินที่กำหนดไว้ !!!!", 1802050001, Me.Text)
                        '        'Call FormRefresh()
                        '        Call Update_SupervisorApprovedAssetPRReturn(ogvSupervisorApproved, 1)
                        '    Else
                        '        Call Update_SupervisorApprovedAssetPRReturn1(ogvSupervisorApproved, 0)
                        '        'Exit Sub
                        '    End If






                        Close()
                        'End If
                    End If
                    ' End If
                Next

            End With

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

            With ogSupervisorApproved
                If Not (.DataSource Is Nothing) And ogvSupervisorApproved.RowCount > 0 Then

                    With ogvSupervisorApproved
                        For I As Integer = 0 To .RowCount - 1

                            If .GetRowCellValue(I, "FTStateApproved").ToString() = 1 Then
                                BolReject = True
                                Exit For
                            Else
                                BolReject = False

                            End If

                        Next
                    End With
                End If

            End With

            If BolReject = False Then Exit Sub
            Dim _frmshowReject As New wShowReject
            otmchkpo.Enabled = False

            ' _frmshowReject = New wShowReject

            _frmshowReject.ShowDialog()

            'Dim dt As DataTable
            'With CType(ogSupervisorApproved.DataSource, DataTable)
            '    .AcceptChanges()
            '    dt = .Copy
            '    For Each R As DataRow In dt.Select("FTPRPurchaseNo <> '' ", "FTStateApproved")
            '        Dim _App As String = HI.Conn.SQLConn.GetField("Select isnull(FTStateApp,'0')as FTStateApp  from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request As P where P.FTPRPurchaseNo = '" & R!FTPRPurchaseNo.ToString & "'", Conn.DB.DataBaseName.DB_SYSTEM, "")

            If ogvSupervisorApproved.RowCount = 0 Then Exit Sub

            '        If _App = "0" Then
            Call _ClsService.Update_SupervisorApprovedAssetPR(ogvSupervisorApproved, 2, wShowReject.Data_Reason)
            '        Else
            '            Call _ClsService.Update_SupervisorApprovedAssetPRDi(ogvSupervisorApproved, 2, wShowReject.Data_Reason)
            '        End If

            '    Next

            'End With




            ' Call Update_ManagerApproved(ogvDirectorApproved, 2, wShowReject.Data_Reason)
            'Dim _App As String = HI.Conn.SQLConn.GetField("Select isnull(FTStateApp,'0')as FTStateApp  from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request As P where P.FTStateApp = '1'", Conn.DB.DataBaseName.DB_SYSTEM, "")
            'If _App = "" Then
            '    Call _ClsService.Update_SupervisorApprovedAssetPR(ogvSupervisorApproved, 2, wShowReject.Data_Reason)
            'Else
            '    Call _ClsService.Update_SupervisorApprovedAssetPRDi(ogvSupervisorApproved, 2, wShowReject.Data_Reason)
            'End If

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
        If ogvSupervisorApproved.RowCount > 0 Then

            Me.Show()

            If Me.WindowState = FormWindowState.Minimized Then
                Me.WindowState = FormWindowState.Maximized
            End If

        End If

        Call SetSizeGrid()

        '  Call WriteLog_Director(Date.Now.ToLongTimeString & "  ผ่าน otmchkpo_Tick")

        ' System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  ผ่าน otmchkpo_Tick")
    End Sub

    Private Sub ogvDirectorApproved_DoubleClick(sender As Object, e As EventArgs) Handles ogvSupervisorApproved.DoubleClick
        With Me.ogvSupervisorApproved

            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub

            ' _OldRow = .FocusedRowHandle

            Dim _PurchaseNo As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTPRPurchaseNo").ToString()
            Dim _WformPo As New wPurchaseRequestAsset

            With _WformPo
                .ocmexit.Visible = False
                .ocmclear.Visible = False
                .FTPRPurchaseNo.Properties.ReadOnly = True
                .FTPRPurchaseNo.Properties.Buttons(0).Enabled = False
                .FTPRPurchaseNo.Properties.Buttons(1).Enabled = False
            End With

            ' Dim _WShow As New wShowData(_WformPo, _PurchaseNo)

            Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
            HI.ST.SysInfo.MenuName = "MnuManualPurchasePR"
            Dim _WShow As New wShowData(_WformPo, _PurchaseNo)
            HI.ST.SysInfo.MenuName = _TmpMenu

            With _WShow
                .WindowState = System.Windows.Forms.FormWindowState.Maximized
                .StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                .ShowDialog()
            End With

        End With
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        With Me.ogvSupervisorApproved
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _PoNo As String = "" & .GetFocusedRowCellValue("FTPRPurchaseNo").ToString
            Dim _FNPoState As Integer = 0

            Dim _Qry As String = ""

            _Qry = "Select TOP 1 FNPRState   "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPRPurchaseNo='" & HI.UL.ULF.rpQuoted(_PoNo) & "' "

            _FNPoState = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, HI.Conn.DB.DataBaseName.DB_FIXED, "0")))

            With New HI.RP.Report

                Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language

                If _FNPoState = 0 Then
                    HI.ST.Lang.Language = HI.ST.Lang.eLang.TH
                Else
                    HI.ST.Lang.Language = HI.ST.Lang.eLang.EN
                End If

                .FormTitle = Me.Text
                .ReportFolderName = "PurchaseAsset\"
                .ReportName = "PurchaseRequestAsset.rpt"
                .AddParameter("Draft", "DRAFT")
                .Formular = "{TFIXEDTPurchase_Request.FTPRPurchaseNo}='" & HI.UL.ULF.rpQuoted(_PoNo) & "'"
                .Preview()

                HI.ST.Lang.Language = _tmplang
            End With

        End With
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        ClsService.StateAssetPRShow = False
        Me.Close()
    End Sub

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvSupervisorApproved)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ogvSupervisorApproved_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvSupervisorApproved.RowCellStyle
        Try
            With ogvSupervisorApproved
                Try
                    If .GetRowCellValue(e.RowHandle, "FTStateFree") = "1" Then
                        e.Appearance.ForeColor = Drawing.Color.Blue
                    End If
                Catch ex As Exception
                End Try
            End With
        Catch ex As Exception

        End Try
    End Sub


    Friend Function Update_SupervisorApprovedAssetPRReturn(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String) As Boolean

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

            _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(_IntCount, "FTPRPurchaseBy").ToString()


            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_FIXED)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For i = 0 To TempGrid.RowCount - 1
                'Dim _Pur As Double = HI.Conn.SQLConn.GetField("Select (Convert(numeric(18, 0),FNNetAmt)) as FNNetAmt from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail As P where P.FTPRPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString() & "'", Conn.DB.DataBaseName.DB_SYSTEM, "")
                'Dim _End As Double = HI.Conn.SQLConn.GetField("Select (Convert(numeric(18, 0),L.FNEndQty)) as FNEndQty from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request As P LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetConfigLevel L ON P.FNFixedAssetType=L.FNFixedAssetType where P.FTPRPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString() & "'", Conn.DB.DataBaseName.DB_SYSTEM, "")


                'If _Pur <= _End Then
                If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then

                    _Str = ""
                    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request "
                    _Str &= Environment.NewLine & "SET  [FTStateSafety] = '" & 1 & "'"
                    _Str &= Environment.NewLine & ", [FTSafetyName] = '" & HI.ST.UserInfo.UserName & "'"
                    _Str &= Environment.NewLine & ", [FTSafetyDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & ", [FTSafetyTime] = '" & 0 & "'"
                    '_Str &= Environment.NewLine & ", [FTManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                    '_Str &= Environment.NewLine & ", [FTManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                    '_Str &= Environment.NewLine & ", [FTManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    '_Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
                    '_Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
                    '_Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate. FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchase_Request] "
                    _Str &= Environment.NewLine & " WHERE FTPRPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString() & "'"

                    'Else
                    '    _Str = ""
                    '    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request "
                    '    _Str &= Environment.NewLine & "SET  [FTStateApp] = '" & TempStatus & "'"
                    '    _Str &= Environment.NewLine & ", [FTAppName] = '" & HI.ST.UserInfo.UserName & "'"
                    '    _Str &= Environment.NewLine & ", [FTAppDate] = " & HI.UL.ULDate.FormatDateDB
                    '    _Str &= Environment.NewLine & ", [FTAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    '    '_Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
                    '    '_Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
                    '    '_Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate. FormatTimeDB
                    '    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchase_Request] "
                    '    _Str &= Environment.NewLine & " WHERE FTPRPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString() & "'"
                    'End If

                    'End If


                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                End If

                If _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseBy").ToString() Then

                    If _atPurchaseNo(_IntCount) = String.Empty Then
                        _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()
                    Else
                        _atPurchaseNo(_IntCount) &= " ;" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()
                    End If

                Else
                    _IntCount = _IntCount + 1
                    _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseBy").ToString()
                    _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()

                End If


            Next

            For j = 0 To _IntCount


                ' ส่งเมลกลับกรณี Approved  FNMailStateType = 0

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                '_Str &= ",'Approved PurchaseNo : " & TempGrid.GetRowCellValue(0, "FTPurchaseNo").ToString() & "' ,0,1,0,0,0,0,"
                _Str &= ",'Approved PurchaseNo','" & _atPurchaseNo(j) & "',0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    'HI.Conn.SQLConn.Tran.Rollback()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    'Return False
                End If

                ' ส่งเมลกลับกรณี Approved  FNMailStateType = 1

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Approved PurchaseNO','" & _atPurchaseNo(j) & "',0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    'HI.Conn.SQLConn.Tran.Rollback()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    'Return False
                End If

                ' กรณีส่งหาตัวเอง
                If _aPurchaseBy(j) = HI.ST.UserInfo.UserName Then
                    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                    _Str = ""
                    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
                    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                    _Str &= ",'Approved PurchaseNo ','" & _atPurchaseNo(j) & "' ,0,0,1,0,0,0,"
                    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        'HI.Conn.SQLConn.Tran.Rollback()
                        'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
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

    Friend Function Update_SupervisorApprovedAssetPRReturn1(ByVal TempGrid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal TempStatus As String) As Boolean

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

            _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(_IntCount, "FTPRPurchaseBy").ToString()


            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_FIXED)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For i = 0 To TempGrid.RowCount - 1
                'Dim _Pur As Double = HI.Conn.SQLConn.GetField("Select (Convert(numeric(18, 0),FNNetAmt)) as FNNetAmt from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request_Detail As P where P.FTPRPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString() & "'", Conn.DB.DataBaseName.DB_SYSTEM, "")
                'Dim _End As Double = HI.Conn.SQLConn.GetField("Select (Convert(numeric(18, 0),L.FNEndQty)) as FNEndQty from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request As P LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetConfigLevel L ON P.FNFixedAssetType=L.FNFixedAssetType where P.FTPRPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString() & "'", Conn.DB.DataBaseName.DB_SYSTEM, "")


                'If _Pur <= _End Then
                If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then

                    _Str = ""
                    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request "
                    _Str &= Environment.NewLine & "SET  [FTStateApp] = '" & 0 & "'"
                    _Str &= Environment.NewLine & ", [FTAppName] = '" & HI.ST.UserInfo.UserName & "'"
                    _Str &= Environment.NewLine & ", [FTAppDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & ", [FTStateManagerApp] = '" & 0 & "'"
                    _Str &= Environment.NewLine & ", [FTManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                    _Str &= Environment.NewLine & ", [FTManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & ", [FTStateRe] = '" & 1 & "'"
                    _Str &= Environment.NewLine & ", [FTStateSendApp] = '" & 0 & "'"
                    '_Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
                    '_Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
                    '_Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate. FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchase_Request] "
                    _Str &= Environment.NewLine & " WHERE FTPRPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString() & "'"

                    'Else
                    '    _Str = ""
                    '    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Request "
                    '    _Str &= Environment.NewLine & "SET  [FTStateApp] = '" & TempStatus & "'"
                    '    _Str &= Environment.NewLine & ", [FTAppName] = '" & HI.ST.UserInfo.UserName & "'"
                    '    _Str &= Environment.NewLine & ", [FTAppDate] = " & HI.UL.ULDate.FormatDateDB
                    '    _Str &= Environment.NewLine & ", [FTAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    '    '_Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
                    '    '_Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
                    '    '_Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate. FormatTimeDB
                    '    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FIXED) & "].[dbo].[TFIXEDTPurchase_Request] "
                    '    _Str &= Environment.NewLine & " WHERE FTPRPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString() & "'"
                    'End If

                    'End If


                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If
                End If

                If _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseBy").ToString() Then

                    If _atPurchaseNo(_IntCount) = String.Empty Then
                        _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()
                    Else
                        _atPurchaseNo(_IntCount) &= " ;" & TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()
                    End If

                Else
                    _IntCount = _IntCount + 1
                    _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseBy").ToString()
                    _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPRPurchaseNo").ToString()

                End If


            Next

            For j = 0 To _IntCount


                ' ส่งเมลกลับกรณี Approved  FNMailStateType = 0

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                '_Str &= ",'Approved PurchaseNo : " & TempGrid.GetRowCellValue(0, "FTPurchaseNo").ToString() & "' ,0,1,0,0,0,0,"
                _Str &= ",'Approved PurchaseNo','" & _atPurchaseNo(j) & "',0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    'HI.Conn.SQLConn.Tran.Rollback()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    'Return False
                End If

                ' ส่งเมลกลับกรณี Approved  FNMailStateType = 1

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Approved PurchaseNO','" & _atPurchaseNo(j) & "',0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    'HI.Conn.SQLConn.Tran.Rollback()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    'Return False
                End If

                ' กรณีส่งหาตัวเอง
                If _aPurchaseBy(j) = HI.ST.UserInfo.UserName Then
                    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                    _Str = ""
                    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
                    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                    _Str &= ",'Approved PurchaseNo ','" & _atPurchaseNo(j) & "' ,0,0,1,0,0,0,"
                    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        'HI.Conn.SQLConn.Tran.Rollback()
                        'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
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


End Class