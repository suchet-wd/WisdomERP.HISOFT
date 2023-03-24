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
Imports DevExpress.XtraEditors.Controls

Public Class wRDSamApproved

    Private _ProcLoad As Boolean = False
    Private _SysPathImageSystem As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images\System"
    ' Private olbhisoft As DevExpress.XtraEditors.LabelControl

    Private DT As DataTable
    Private _ClsService As New ClsService
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

    Sub New()
        _ProcLoad = True

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _Splash = New HI.TL.SplashScreen("Wisdom System", "Loading Approve RD Sam..." & "  ")

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
                        ClsService.StateShowSMPMGR = False
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
        ClsService.StateShowSMPMGR = False
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

        Call Set_HeadGrid()
        Call SetSizeGrid()
    End Sub


    Private Sub MainRibbon_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            ClsService.StateShowSMPMGR = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MainRibbon_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ClsService.StateShowSMPMGR = False
    End Sub

    Private Sub MainRibbon_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Me.Enabled = True


        ocmapprove.Enabled = True

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
            DTPurchaseNo = LoadogcRDSame()

            If _CountApp > 0 Then

                '    Log("_CountApp = " & _CountApp)

                If Not (DTPurchaseNo Is Nothing) Then


                    ogSupervisorApproved.DataSource = DTPurchaseNo

                    ogvSupervisorApproved.OptionsView.ShowAutoFilterRow = True

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
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFNPOGrandAmt").Width = 110
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFTRemark").Width = 100
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFTTeamGrpCode").Width = 100
        '    ogvSupervisorApproved.Columns.ColumnByName("ColFTTeamGrpName").Width = 130
        'Catch ex As Exception

        'End Try
    End Sub

    Public Shared Function LoadogcRDSame() As DataTable
        Try

            Dim _Qry As String = String.Empty
            Dim _dt As New DataTable

            _Qry = " EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.USP_CHECKAPP_RDSAM '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','Y'  "

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, HI.Conn.DB.DataBaseName.DB_PUR)

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

            'Me.Enabled = True
            'sBtnSave.Enabled = True
            'sBtnReject.Enabled = True
            'sBtnExit.Enabled = True

            ' System.IO.File.WriteAllText("E:\text.txt", " ผ่าน BindGrid")
            'Call WriteLog_Director(Date.Now.ToLongTimeString & " ผ่าน BindGrid")

            Call Set_HeadGrid()

            '  HI.ST.SysInfo.StateDirector = True     ' true ทดสอบ Super     false ทดสอบ Manager
            ' กลับมาเรียก Function ของตัวเอง
            DT = Nothing
            DT = LoadogcRDSame()

            '  Call WriteLog_Director(Date.Now.ToLongTimeString & " ผ่าน BindGrid >> DT = LoadogcTPURTPurchase  Count = " & DT.Rows.Count)

            If Not (DT Is Nothing) Then

                Dim _FocusRowInDex As Integer = -1
                Dim _FTPurchaseNo As String = ""


                Try

                    If ogvSupervisorApproved.FocusedRowHandle > 0 Then

                        _FocusRowInDex = ogvSupervisorApproved.FocusedRowHandle
                        _FTPurchaseNo = ogvSupervisorApproved.GetRowCellValue(ogvSupervisorApproved.FocusedRowHandle, "FTPurchaseNo").ToString

                    End If

                Catch ex As Exception
                End Try

                ogSupervisorApproved.DataSource = DT
                ogvSupervisorApproved.OptionsView.ShowAutoFilterRow = False
                Me.ogSupervisorApproved.Refresh()

                _FocusRowInDex = -1
                With Me.ogvSupervisorApproved
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

                Call SetSizeGrid()


            Else
                ogSupervisorApproved.DataSource = Nothing
                ClsService.StateShowSMPMGR = False
                Me.Close()


            End If


        Catch ex As Exception

        End Try

    End Sub


    Private Sub sBtnSave_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click
        Try

            If ogvSupervisorApproved.RowCount = 0 Then Exit Sub

            Dim _Qry As String


            Dim dt As DataTable = New DataTable


            With CType(Me.ogSupervisorApproved.DataSource, DataTable)
                .AcceptChanges()

                dt = .Copy

            End With


            If dt.Select("FTSelect='1'").Length <> 1 Then
                Exit Sub
            End If


            Dim dataSMPOrderNo As String = ""
            Dim FTSMPOrderNoRef As String = ""
            Dim StyleId As Integer = 0
            Dim SeasonId As Integer = 0
            Dim _Cmd As String = ""

            For Each R As DataRow In dt.Rows

                dataSMPOrderNo = R!FTSMPOrderNo.ToString()
                FTSMPOrderNoRef = R!FTSMPOrderNoRef.ToString()
                StyleId = Val(R!FNHSysStyleId.ToString)
                SeasonId = Val(R!FNHSysSeasonId.ToString)


                If R!FTSelect.ToString() = "1" Then

                    _Cmd = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam SET "
                    _Cmd &= vbCrLf & " FTStateMerAppBy= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Cmd &= vbCrLf & " ,FTStateMerAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Cmd &= vbCrLf & " ,FTStateMerAppTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    _Cmd &= vbCrLf & " ,FTStateMerApp='1' "
                    _Cmd &= vbCrLf & " ,FTStateToGE='1' "
                    _Cmd &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(dataSMPOrderNo) & "'"

                    _Cmd &= vbCrLf & " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam SET "
                    _Cmd &= vbCrLf & " FTStateMerAppBy= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Cmd &= vbCrLf & " ,FTStateMerAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Cmd &= vbCrLf & " ,FTStateMerAppTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    _Cmd &= vbCrLf & " ,FTStateMerApp='1' "
                    _Cmd &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(FTSMPOrderNoRef) & "'"


                    _Cmd &= vbCrLf & "  INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGESam ( "
                    _Cmd &= vbCrLf & "  FNHSysCmpId, FNHSysStyleId, FNHSysSeasonId, FTRemark, FNSam, FNOperater, FNCost, FNMinuteHour, FNProdPersonPerDay, FNWorkingTimeMinuteDay, FNTargetPerDay, FTStateNewFromRD, FTStateFromRDBy "
                    _Cmd &= vbCrLf & " ,FTStateFromRDDate, FTStateFromRDTime, FNRDSam, FNSamCut, FNCostCut, FNNetCostCut, FNSamPack, FNCostPack, FNNetCostPack "
                    _Cmd &= vbCrLf & "  ) "
                    _Cmd &= vbCrLf & "  SELECT TOP 1 0 AS FNHSysCmpId, FNHSysStyleId, FNHSysSeasonId, FTRemark, FNSam, FNOperater, FNCost, FNMinuteHour, FNProdPersonPerDay, FNWorkingTimeMinuteDay, FNTargetPerDay, FTStateNewFromRD, FTStateFromRDBy "
                    _Cmd &= vbCrLf & " ,FTStateFromRDDate, FTStateFromRDTime, FNSam, FNSamCut, FNCostCut, FNNetCostCut, FNSamPack, FNCostPack, FNNetCostPack "
                    _Cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam "
                    _Cmd &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(dataSMPOrderNo) & "'"
                    _Cmd &= vbCrLf & "  INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTGESam_Detail  ( "
                    _Cmd &= vbCrLf & "  FNHSysCmpId, FNHSysStyleId, FNHSysSeasonId, FNSeq, FNHSysRDOperationId, FNHSysRDMachineTypeId, FNSam, FNOperater, FNCost, FNOutputPer1Hour, FNOutputPer8Hour, FTRemark, FTStateNoSew, FNHSysRDPositionPartId"
                    _Cmd &= vbCrLf & "  ) "
                    _Cmd &= vbCrLf & "  SELECT 0 AS FNHSysCmpId, FNHSysStyleId, FNHSysSeasonId, FNSeq, FNHSysRDOperationId, FNHSysRDMachineTypeId, FNSam, FNOperater, FNCost, FNOutputPer1Hour, FNOutputPer8Hour, FTRemark, FTStateNoSew, 0 AS FNHSysRDPositionPartId"
                    _Cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam_Detail "
                    _Cmd &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(dataSMPOrderNo) & "'"


                    If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PLANNING) Then

                    End If

                Else

                    _Cmd = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TRDTRDSam SET "
                    _Cmd &= vbCrLf & " FTStateMerAppBy= '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Cmd &= vbCrLf & " ,FTStateMerAppDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Cmd &= vbCrLf & " ,FTStateMerAppTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    _Cmd &= vbCrLf & " ,FTStateMerApp='1' "
                    _Cmd &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(dataSMPOrderNo) & "'"

                    If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PLANNING) Then

                    End If

                End If


            Next

            Call BindGrid()

        Catch ex As Exception

        End Try
    End Sub


    Private Sub otmchkpo_Tick(sender As Object, e As EventArgs) Handles otmchkpo.Tick

        'Log("otmchkpo_Tick  Work")
        'Application.DoEvents()
        Call BindGrid()
        'Application.DoEvents()
        Me.Enabled = True
        ocmapprove.Enabled = True


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


    Private Sub ocmpreview_Click(sender As Object, e As EventArgs)
        With Me.ogvSupervisorApproved
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim dataSMPOrderNo As String = "" & .GetFocusedRowCellValue("FTSMPOrderNo").ToString()
            Dim dataTeam As String = "" & .GetFocusedRowCellValue("FTTeam").ToString
            Dim _FNPoState As Integer = 0

            Dim _Qry As String = ""


            Dim _FM As String = ""
            _FM = " {TSMPCalculate.FTSMPOrderNo}='" & HI.UL.ULF.rpQuoted(dataSMPOrderNo) & "' AND  {TSMPCalculate.FTTeam}='" & HI.UL.ULF.rpQuoted(dataTeam) & "'"


            With New HI.RP.Report

                Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language

                If _FNPoState = 0 Then
                    HI.ST.Lang.Language = HI.ST.Lang.eLang.TH
                Else
                    HI.ST.Lang.Language = HI.ST.Lang.eLang.EN
                End If

                .FormTitle = Me.Text

                .ReportFolderName = "Human Report\"
                .ReportName = "SMPIncentiveV.rpt"
                .Formular = _FM

                .Preview()

                HI.ST.Lang.Language = _tmplang
            End With

        End With
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        ClsService.StateShowSMPMGR = False
        Me.Close()
    End Sub

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvSupervisorApproved)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub RepositoryItemCheckEdit1_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCheckEdit1.EditValueChanging
        Try
            With Me.ogvSupervisorApproved
                If .FocusedRowHandle < 0 Then
                    e.Cancel = False
                Else

                    Dim SMPOrderNo As String = "" & .GetFocusedRowCellValue("FTSMPOrderNo").ToString()

                    Dim StateSelect As String = "0"
                    If e.NewValue.ToString = "1" Then
                        StateSelect = "1"
                    End If

                    .SetFocusedRowCellValue("FTSelect", StateSelect)
                    With CType(ogSupervisorApproved.DataSource, DataTable)
                        .AcceptChanges()

                        For Each R As DataRow In .Select("FTSMPOrderNo<>'" & HI.UL.ULF.rpQuoted(SMPOrderNo) & "'")
                            R!FTSelect = "0"

                        Next
                        .AcceptChanges()

                    End With

                End If
            End With
        Catch ex As Exception

        End Try
    End Sub
End Class