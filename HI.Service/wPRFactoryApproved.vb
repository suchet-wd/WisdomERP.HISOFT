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

Public Class wPRFactoryApproved

    Private _ProcLoad As Boolean = False
    Private _SysPathImageSystem As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images\System"
    ' Private olbhisoft As DevExpress.XtraEditors.LabelControl

    Private DT As DataTable
    Private DTFac As DataTable
    Private DTICM As DataTable
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

        _Splash = New HI.TL.SplashScreen("WISDOM SYSTEM", "Loading Approve Factory Order..." & "  ")

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
            ClsService.StateAppPR = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MainRibbon_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ClsService.StateAppPR = False
    End Sub

    Private Sub MainRibbon_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ocmapprove.Enabled = True
        ocmreject.Enabled = True
        'ocmpreview.Enabled = True
        ocmsavelayout.Enabled = True
        Call ToolBarFunctionActive(Me)
        Call BindGrid()
        Try
            Call HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvdetail)
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

    Private Sub Set_HeadGrid()
        Try
            With ogvdetail
                .OptionsView.ShowAutoFilterRow = False
                .OptionsSelection.MultiSelect = False
                .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
            End With
        Catch ex As Exception
        End Try
    End Sub


    Private Sub SetSizeGrid()
 
    End Sub

    Public Shared Function LoadDataApproveInfo() As DataTable
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _LangDisPlay As String = "TH"

            If HI.ST.Lang.Language <> ST.Lang.eLang.TH Then
                _LangDisPlay = "EN"
            End If

            _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.SP_GET_APPROVE_PR '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_LangDisPlay) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PUR)

            Return _oDt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub BindGrid()
        Try
            Call Set_HeadGrid()
            DTICM = LoadDataApproveInfo()
            If Not (DTICM Is Nothing) Then
                Me.FTSelectAllCMInv.Checked = False
                Me.ogcdetail.DataSource = DTICM
            Else
                Me.ogcdetail.DataSource = Nothing
            End If
            If DTICM.Rows.Count <= 0 Then
                ClsService.StateAppPR = False
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub sBtnSave_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click
        Try
            Select Case Me.otbmain.SelectedTabPage.Name
                Case otpappcpr.Name
                    If ogvdetail.RowCount = 0 Then Exit Sub

                    With CType(ogcdetail.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTSelect='1'").Length <= 0 Then Exit Sub
                    End With

                    Dim _Spls As New HI.TL.SplashScreen("Approving PR...,Pease wait...")

                    Try
                        Call _ClsService.Update_PRApprove(ogvdetail, "1", "")
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
                Case otpappcpr.Name
                    If ogvdetail.RowCount = 0 Then Exit Sub
                    With CType(ogcdetail.DataSource, DataTable)
                        .AcceptChanges()
                        If .Select("FTSelect='1'").Length <= 0 Then Exit Sub
                    End With
            End Select
            Dim _frmshowReject As New wShowReject
            otmchkpo.Enabled = False
            _frmshowReject.ShowDialog()
            Select Case Me.otbmain.SelectedTabPage.Name
                Case otpappcpr.Name
                    Dim _Spls As New HI.TL.SplashScreen("Rejecting PR...,Pease wait.")

                    Try
                        Call _ClsService.Update_PRApprove(ogvdetail, "2", wShowReject.Data_Reason)
                    Catch ex As Exception
                    End Try

                    _Spls.Close()
            End Select
            otmchkpo.Enabled = True
            Call BindGrid()
        Catch ex As Exception
            otmchkpo.Enabled = True
        End Try

    End Sub

    Private Sub otmchkpo_Tick(sender As Object, e As EventArgs) Handles otmchkpo.Tick
        Call BindGrid()
        Me.Enabled = True
        ocmapprove.Enabled = True
        ocmreject.Enabled = True
        'ocmpreview.Enabled = True
        If ogvdetail.RowCount > 0 Then
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
        ClsService.StateAppPR = False
        Me.Close()
    End Sub

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvdetail)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub otbmain_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbmain.SelectedPageChanged
        Call TabChange()
    End Sub

    Private Sub TabChange()
        'ocmpreview.Visible = False
        Me.ToolBarFunctionActive(Me)
    End Sub

    Private Sub FTSelectAllCMInv_CheckedChanged(sender As Object, e As EventArgs) Handles FTSelectAllCMInv.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.FTSelectAllCMInv.Checked Then
                _State = "1"
            End If

            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then
                    With ogvdetail
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

    Private Sub ogvdetail_DoubleClick(sender As Object, e As EventArgs) Handles ogvdetail.DoubleClick
        With Me.ogvdetail

            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub

            ' _OldRow = .FocusedRowHandle

            Dim _PurchaseNo As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTPRPurchaseNo").ToString()
            Dim _WformPo As New wPurchaseRequest

            With _WformPo
                .ocmexit.Visible = False
                .ocmclear.Visible = False
                .FTPRPurchaseNo.Properties.ReadOnly = True
                .FTPRPurchaseNo.Properties.Buttons(0).Enabled = False
                .FTPRPurchaseNo.Properties.Buttons(1).Enabled = False
            End With

            ' Dim _WShow As New wShowData(_WformPo, _PurchaseNo)

            Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
            HI.ST.SysInfo.MenuName = "MnuwPurchaseRequest"
            Dim _WShow As New wShowData(_WformPo, _PurchaseNo)
            HI.ST.SysInfo.MenuName = _TmpMenu

            With _WShow
                .WindowState = System.Windows.Forms.FormWindowState.Maximized
                .StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                .ShowDialog()
            End With

        End With
    End Sub
End Class