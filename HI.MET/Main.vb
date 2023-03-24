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
Imports System.Data.SqlClient
Imports System.Drawing.Text
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils

Public Class Main

    Private StateUserLogOff As Boolean
    Private _ProcLoad As Boolean = False
    Private _SysPathImageSystem As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images\System"
    Private _BarManager As DevExpress.XtraBars.BarManager
    Private _AllFuncName As String
    Private _AppSystemPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _PathFileDll As String
    Private _Splash As HI.TL.SplashScreen
    Private _TimcCheckUserLogin As Integer = 60
    Private _TimcCountCheckUserLogin As Integer = 0
    Private _StateCheckPOApp As Boolean = False
    Private _StateCheckPOAppDirector As Boolean = False
    Private _StateCheckManagerFactory As Boolean = False
    Private _StateCheckWHAppCm As Boolean = False
    Private _StateCheckPOPDF As Boolean = False
    Private _StateCheckMailUser As Boolean = False
    Private _StateCheckUser As Boolean = False
    Private _StateCheckMerAppTVW As Boolean = False
    Private _StateCheckinvoicecharge As Boolean = False
    Private _StateCheckSewingLineLeader As Boolean = False
    Private _StateCheckQAFinalLeader As Boolean = False
    Private _StateUserDC As Boolean = False
    Private _StateUserAppPR As Boolean = False
    Private _ContextStripUserPicture As System.Windows.Forms.ContextMenuStrip
    Private _StateChekOrderCost As Boolean = False
    Private _StateFormLoadSucess As Boolean = False
    Private _StateEmpleaveApp As Boolean = False
    Private _StateCheckPOAppAsset As Boolean = False
    Private _StateCheckPRAppAsset As Boolean = False
    Private _StateCheckPOAppAssetDirector As Boolean = False
    Private _StateCheckPRAppAssetDirector As Boolean = False


    Sub New()
        _ProcLoad = True

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _Splash = New HI.TL.SplashScreen("Wisdom System", "Loading...")

        GetAllDevExpressPaintStyle()
        Call CreateManuStripPictureEdit()

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

        HI.ST.SysInfo.FTOptionMouseScoll = ""

        _AllFuncName = ""
        imagemenuList.Images.Clear()

        Me._BarManager = New DevExpress.XtraBars.BarManager
        With _BarManager
            .Form = Me
            .AllowCustomization = False
            .DockControls.Add(Me.StandaloneBarDockControl)
        End With

        Me.CreateSystemMainMenu(Me)

        Me.CreateToolBarFunction(Me._BarManager)

        Me.MdiManager = New XtraTabbedMdiManager()
        Me.MdiManager.HeaderButtons = CType((DevExpress.XtraTab.TabButtons.Prev Or DevExpress.XtraTab.TabButtons.[Next]), DevExpress.XtraTab.TabButtons)
        Me.MdiManager.MdiParent = Me

        Me.olbdate.Caption = Format(HI.UL.ULDate.GetOnServer(HI.Conn.DB.DataBaseName.DB_SYSTEM), "dd MMMM yyyy")
        Me.olbcopyright.Caption = HI.MG.Msg.CopyRight

        Dim fi As FileInfo = New FileInfo(System.Reflection.Assembly.GetExecutingAssembly.Location)
        _PathFileDll = fi.DirectoryName
        HI.ST.SysInfo.PathFileDLL = _PathFileDll
        FTUsername.Text = HI.ST.UserInfo.UserName

        If Not (HI.ST.UserInfo.UserImage Is Nothing) Then

            With Me.FPUserImage

                .Image = HI.ST.UserInfo.UserImage

                Dim _SuperToolTip As New DevExpress.Utils.SuperToolTip()
                Dim _ToolTipTitleItem As New DevExpress.Utils.ToolTipTitleItem()

                _ToolTipTitleItem.Appearance.Image = .Image
                _ToolTipTitleItem.Appearance.Options.UseImage = True
                _ToolTipTitleItem.Image = .Image
                _ToolTipTitleItem.Text = ""

                With _SuperToolTip
                    .Items.Add(_ToolTipTitleItem)
                End With

                .SuperTip = _SuperToolTip

            End With

        End If

        FTUserLogINIP.Caption = "Login IP :: " & HI.ST.UserInfo.UserLogInComputerIP

        Call ProcLoadLang(HI.ST.Lang.eLang.TH)

        _TimcCountCheckUserLogin = 0

        _StateCheckPOAppDirector = True
        _StateCheckManagerFactory = True
        _StateCheckWHAppCm = True
        _StateCheckMerAppTVW = True
        _StateCheckinvoicecharge = True
        _StateCheckPOApp = True
        _StateCheckPOPDF = True
        _StateCheckMailUser = True
        _StateCheckSewingLineLeader = True
        _StateCheckQAFinalLeader = True
        _StateUserDC = True
        _StateUserAppPR = True
        _StateChekOrderCost = True
        _StateEmpleaveApp = True


        _StateCheckUser = True
        otmchkuserlogin.Enabled = True
        ocmcheckapp.Enabled = False ' HI.ST.SysInfo.StateManager
        otmpdf.Enabled = False ' HI.ST.SysInfo.StateUserPurchaseTeam
        otmcheckmerapptvw.Enabled = False ' HI.ST.SysInfo.StateSuperVisorMer
        otbcheckmail.Enabled = HI.ST.SysInfo.StateMail
        ocmcheckappdirector.Enabled = False ' HI.ST.SysInfo.StateDirector
        ocmcheckmanagerfactoryapp.Enabled = False 'HI.ST.SysInfo.StateFactoryManager
        ocmcheckwhappcm.Enabled = False 'HI.ST.SysInfo.StateWHAppCM
        otmqafinalleader.Enabled = False 'HI.ST.SysInfo.StateUserQAFinalLeader
        otmsewinglineleader.Enabled = False 'HI.ST.SysInfo.StateUserSewingLineLeader
        Me.otmdctimer.Enabled = False ' HI.ST.SysInfo.StateUserDCControl
        Me.otmcheckappr.Enabled = False 'HI.ST.SysInfo.StateUserAppPR
        Me.otmChkOrderCostApp.Enabled = False 'HI.ST.SysInfo.StateOrderCostApp
        Me.otmChkEmpLeaveApp.Enabled = False 'HI.ST.SysInfo.StateEmpLeaveApp
        Me.otmqastyleriskcritical.Enabled = False ' HI.ST.SysInfo.StyleRiskCritical


        _StateCheckPOAppAsset = HI.ST.SysInfo.StateSuperVisorAssetPO
        _StateCheckPRAppAsset = HI.ST.SysInfo.StateSuperVisorPRAsset
        _StateCheckPOAppAssetDirector = HI.ST.SysInfo.StateDirectorAssetPO
        _StateCheckPRAppAssetDirector = HI.ST.SysInfo.StateDirectorAssetPR


        Me.otmcheckAssetPR.Enabled = False 'HI.ST.SysInfo.StateSuperVisorPRAsset
        Me.otmDirectorAssetPO.Enabled = False ' HI.ST.SysInfo.StateDirectorAssetPO
        Me.otmDirectorAssetPR.Enabled = False ' HI.ST.SysInfo.StateDirectorAssetPR
        Me.otmAssetPo.Enabled = False ' HI.ST.SysInfo.StateSuperVisorAssetPO


        otmcheckinvoicecharge.Interval = (60000 * HI.ST.SysInfo.StateUserInvoiceChargeTimeMin)
        otmcheckinvoicecharge.Enabled = False '((HI.ST.SysInfo.StateUserInvoiceCharge) And (HI.ST.SysInfo.StateUserInvoiceChargeDay > 0))

        Try

            Dim _Theme As String = HI.UL.AppRegistry.ReadRegistry(UL.AppRegistry.KeyName.Theme)

            If _Theme <> "" Then

                MainDefaultLookAndFeel.LookAndFeel.SetSkinStyle(_Theme)

            End If

        Catch ex As Exception
        End Try

        Dim _CmpName As String = ""
        Dim _Str As String = ""

        _Str = " SELECT TOP 1 "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " FTCmpNameTH  "
        Else
            _Str &= vbCrLf & " FTCmpNameEN  "
        End If

        _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS H WITH(NOLOCK)   "
        _Str &= vbCrLf & " WHERE FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "

        _CmpName = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "")

        'Dim _FrmScreen As New wFormScreen

        'With _FrmScreen
        '    .Text = ""

        '    Dim tPathImgDisBG As String = _SysPathImageSystem & "\" & "Background.png"
        '    If IO.File.Exists(tPathImgDisBG) Then
        '        .BackgroundImage = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDisBG)))
        '        .BackgroundImageLayout = ImageLayout.Center
        '    End If

        '    .olbhisoft.Text = "Wisdom SYSTEM"

        '    .olbcmp.Text = _CmpName
        '    .MdiParent = Me
        '    .WindowState = FormWindowState.Maximized
        '    .Show()
        'End With

        olbcmp.Caption = _CmpName



        Try
            Dim tPathImgDis As String = _SysPathImageSystem & "\" & "AppIcon.png"
            If IO.File.Exists(tPathImgDis) Then
                MainRibbonControl.ApplicationIcon = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis)))
            Else
                MainRibbonControl.ApplicationIcon = Nothing
            End If
        Catch ex As Exception
        End Try

        Me.FPUserImage.Properties.ContextMenuStrip = _ContextStripUserPicture

        Call LoadFontFromSystem()

        _Splash.Close()

        _ProcLoad = False
    End Sub

#Region "System Property"

    ReadOnly Property ActivedMdiForm() As Form
        Get
            Return Me.ActiveMdiChild
        End Get
    End Property

#End Region

#Region "Picture"

    Private Sub CreateManuStripPictureEdit()
        _ContextStripUserPicture = New System.Windows.Forms.ContextMenuStrip
        Dim _AddPicture As New System.Windows.Forms.ToolStripMenuItem

        With _AddPicture
            .Name = "ocmChangePicture"
            .Text = "Change Picture"
            AddHandler .Click, AddressOf PictureAdd_Click
        End With

        With _ContextStripUserPicture
            .Name = "ContextUserPicture"
            .Items.AddRange(New System.Windows.Forms.ToolStripItem() {_AddPicture})
        End With

    End Sub

    Public Shared Sub PictureAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim Op As New System.Windows.Forms.OpenFileDialog
            Op.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF"
            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then
                    With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraEditors.PictureEdit)

                        Try

                            .Image = HI.UL.ULImage.LoadImage(Op.FileName)

                            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_SECURITY)
                            HI.Conn.SQLConn.SqlConnectionOpen()

                            Dim _Str As String
                            _Str = " Update  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin "
                            _Str &= vbCrLf & "  SET FPUserImage=@FPUserImage"
                            _Str &= vbCrLf & " WHERE FTUserName=@FTUserName "

                            Dim cmd As New SqlCommand(_Str, HI.Conn.SQLConn.Cnn)
                            cmd.Parameters.AddWithValue("@FTUserName", HI.ST.UserInfo.UserName)
                            Dim data As Byte() = HI.UL.ULImage.ConvertImageToByteArray(.Image, UL.ULImage.PicType.Employee) ' ms.GetBuffer()

                            Dim p As New SqlParameter("@FPUserImage", SqlDbType.Image)
                            p.Value = data
                            cmd.Parameters.Add(p)
                            cmd.ExecuteNonQuery()

                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

                            Dim dt As DataTable

                            _Str = "SELECT TOP 1  *  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin WITH(NOLOCK) "
                            _Str &= vbCrLf & " WHERE FTUserName='" & HI.ST.UserInfo.UserName & "'"
                            dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)
                            If dt.Rows.Count > 0 Then
                                For Each R As DataRow In dt.Rows

                                    Try
                                        HI.ST.UserInfo.UserImage = HI.UL.ULImage.ConvertByteArrayToImmage(R!FPUserImage)
                                    Catch ex As Exception
                                        HI.ST.UserInfo.UserImage = Nothing
                                    End Try

                                    Exit For
                                Next
                            End If
                            dt.Dispose()

                            Dim _SuperToolTip As New DevExpress.Utils.SuperToolTip()
                            Dim _ToolTipTitleItem As New DevExpress.Utils.ToolTipTitleItem()

                            _ToolTipTitleItem.Appearance.Image = .Image
                            _ToolTipTitleItem.Appearance.Options.UseImage = True
                            _ToolTipTitleItem.Image = .Image
                            _ToolTipTitleItem.Text = ""

                            With _SuperToolTip
                                .Items.Add(_ToolTipTitleItem)
                            End With

                            .SuperTip = _SuperToolTip

                        Catch ex As Exception
                        End Try

                    End With
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Handler"
    'Here are the handlers
    'Private Sub MyPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
    '    e.Graphics.DrawImage(pbBackground, 0, 0, Me.ClientSize.Width,Me.ClientSize.Height)
    'End Sub

    'Private Sub MySizeChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Me.Invalidate()
    'End Sub

#End Region

#Region "System Menu"

    Public Function CreateSystemMainMenu(ByVal FormParent As Form) As Boolean
        Try
            Dim ModuleCode As String
            Dim ModuleID As Integer
            Dim _dtMdl As DataTable = LoadSystemModule()
            Dim _tmpdt As DataTable
            Dim _NodeID As Long = 1
            Dim _AddTree As Boolean

            onvbar.Controls.Clear()
            onvbar.Groups.Clear()

            onvbar.Font = New Font(HI.ST.SysInfo.SystemFontName, 10)

            For Each R As DataRow In _dtMdl.Rows

                ModuleCode = R!FTModuleCode.ToString
                ModuleID = Integer.Parse(Val(R!FNHSysModuleID.ToString))

                _AddTree = False

                Dim _Lst As New DevExpress.XtraTreeList.TreeList
                ' _Lst.Font = New Font(HI.ST.SysInfo.SystemFontName, 10)

                _Lst.StateImageList = imagemenuList
                ' _Lst.Width = Me.onvbar.Width
                _Lst.Dock = DockStyle.Fill

                Call CreateTreeSystemMenu(ModuleCode, _Lst)

                Dim _lstBlen As New DevExpress.XtraTreeList.Blending.XtraTreeListBlending

                _lstBlen.TreeListControl = _Lst

                _tmpdt = LoadSystemMenu(ModuleID)

                If IsNothing(_tmpdt) = False Then

                    Dim _DataRow() As DataRow
                    _DataRow = _tmpdt.Select("FNMnuIDParent=-1", "FNSeq")

                    If _DataRow.Length > 0 Then

                        _Lst.BeginUnboundLoad()

                        _AddTree = True
                        Call InitNodeMenu(_Lst, Nothing, -1, _tmpdt, ModuleCode)
                        _Lst.EndUnboundLoad()

                        AddHandler _Lst.Click, AddressOf Menu_Click

                    End If

                End If

                Dim objGrp As New DevExpress.XtraNavBar.NavBarGroup()
                onvbar.Groups.Add(objGrp)

                With objGrp

                    .Name = R!FTModuleCode.ToString
                    .Caption = ("|" & (R!FTModuleNameEN.ToString.Trim & "|" & R!FTModuleNameTH.ToString.Trim & "|" & R!FTModuleNameVT.ToString.Trim & "|" & R!FTModuleNameKM.ToString.Trim & "|" & R!FTModuleNameBM.ToString.Trim & "|" & R!FTModuleNameLAO.ToString.Trim & "|" & R!FTModuleNameCH.ToString.Trim)).Split("|")(HI.ST.Lang.Language).ToString '_DataRow("FTMnuName").ToString.Trim
                    .Tag = "|" & R!FTModuleNameEN.ToString.Trim & "|" & R!FTModuleNameTH.ToString.Trim & "|" & ModuleID.ToString
                    '.Appearance.Font = New Font("Tahoma", 10, FontStyle.Bold)
                    .Appearance.Font = New Font(HI.ST.SysInfo.SystemFontName, 10, FontStyle.Bold)

                    If R!FTImg.ToString.Trim <> "" Then

                        Dim tPathImgDis As String = _AppSystemPath & "\Module\" & R!FTImg.ToString.Trim

                        If IO.File.Exists(tPathImgDis) Then

                            .SmallImage = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis)))

                        End If

                    End If

                    If (_AddTree) Then
                        .GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer
                        .ControlContainer = New DevExpress.XtraNavBar.NavBarGroupControlContainer()
                        .ControlContainer.Controls.Add(_Lst)
                        .GroupClientHeight = 150 ' _Lst.Height + 3
                    End If

                End With

            Next

            Try
                _dtMdl.Dispose()
                _tmpdt.Dispose()
            Catch ex As Exception
            End Try

            Return True

        Catch ex As Exception
            Return True
        End Try
    End Function

    Private Sub CreateTreeSystemMenu(ByVal ModuleName As String, ByVal _Lst As DevExpress.XtraTreeList.TreeList)
        With _Lst

            For I As Integer = 0 To 18
                .Columns.Add()
            Next

            With .Columns.Item(0)
                .Name = ModuleName & "ColKey"
                .Caption = "Menu Name"
                .FieldName = "FTMnuName"
                .Visible = True
            End With

            With .Columns.Item(1)
                .Name = ModuleName & "FTCaptionEN"
                .Caption = "FTModuleCode"
                .FieldName = "FTCaptionEN"
                .Visible = False
            End With

            With .Columns.Item(2)
                .Name = ModuleName & "FTCaptionTH"
                .Caption = "FTModuleCode"
                .FieldName = "FTCaptionTH"
                .Visible = False
            End With

            With .Columns.Item(3)
                .Name = ModuleName & "FTModuleCode"
                .Caption = "FTModuleCode"
                .FieldName = "FTModuleCode"
                .Visible = False
            End With

            With .Columns.Item(4)
                .Name = ModuleName & "FTClassName"
                .Caption = "FTClassName"
                .FieldName = "FTClassName"
                .Visible = False
            End With

            With .Columns.Item(5)
                .Name = ModuleName & "FTFormName"
                .Caption = "FTFormName"
                .FieldName = "FTFormName"
                .Visible = False
            End With

            With .Columns.Item(6)
                .Name = ModuleName & "FTProcName"
                .Caption = "FTProcNamee"
                .FieldName = "FTProcName"
                .Visible = False
            End With

            With .Columns.Item(7)
                .Name = ModuleName & "FTProcType"
                .Caption = "FTProcType"
                .FieldName = "FTProcType"
                .Visible = False
            End With

            With .Columns.Item(8)
                .Name = ModuleName & "FTStaFormShow"
                .Caption = "FTStaFormShow"
                .FieldName = "FTStaFormShow"
                .Visible = False
            End With

            With .Columns.Item(9)
                .Name = ModuleName & "FTFTImg"
                .Caption = "FTImg"
                .FieldName = "FTImg"
                .Visible = False
            End With

            With .Columns.Item(10)
                .Name = ModuleName & "FTStaMasterDynamic"
                .Caption = "FTStaMasterDynamic"
                .FieldName = "FTStaMasterDynamic"
                .Visible = False
            End With

            With .Columns.Item(11)
                .Name = ModuleName & "FNHSysModuleID"
                .Caption = "FNHSysModuleID"
                .FieldName = "FNHSysModuleID"
                .Visible = False
            End With

            With .Columns.Item(12)
                .Name = ModuleName & "FTStateReport"
                .Caption = "FTStateReport"
                .FieldName = "FTStateReport"
                .Visible = False
            End With

            With .Columns.Item(13)
                .Name = ModuleName & "FTStateReportDynamic"
                .Caption = "FTStateReportDynamic"
                .FieldName = "FTStateReportDynamic"
                .Visible = False
            End With

            With .Columns.Item(14)
                .Name = ModuleName & "FTObjectName"
                .Caption = "FTObjectName"
                .FieldName = "FTObjectName"
                .Visible = False
            End With

            With .Columns.Item(15)
                .Name = ModuleName & "FTCallMnuName"
                .Caption = "FTCallMnuName"
                .FieldName = "FTCallMnuName"
                .Visible = False
            End With


            With .Columns.Item(16)
                .Name = ModuleName & "FTCallMethodName"
                .Caption = "FTCallMethodName"
                .FieldName = "FTCallMethodName"
                .Visible = False
            End With

            With .Columns.Item(17)
                .Name = ModuleName & "FTPram"
                .Caption = "FTPram"
                .FieldName = "FTPram"
                .Visible = False
            End With

            With .Columns.Item(18)
                .Name = ModuleName & "FTOptionMouseScoll"
                .Caption = "FTOptionMouseScoll"
                .FieldName = "FTOptionMouseScoll"
                .Visible = False
            End With


            With .OptionsView
                .ShowColumns = False
                .ShowHorzLines = False
                .ShowFocusedFrame = False
                .ShowIndicator = False
                .ShowVertLines = False
            End With

            With .OptionsPrint
                .PrintHorzLines = False
                .PrintVertLines = False
                .UsePrintStyles = True
            End With

            With .OptionsMenu
                .EnableFooterMenu = False
            End With

            With .OptionsBehavior
                .AutoNodeHeight = False
                .Editable = False
                .DragNodes = False
                .ResizeNodes = False
                .AllowExpandOnDblClick = True
            End With

            With .OptionsSelection
                .EnableAppearanceFocusedCell = False
                .EnableAppearanceFocusedRow = True
            End With

            With .Appearance
                With .SelectedRow
                    .BackColor = Color.GreenYellow
                    .ForeColor = Color.Blue
                End With
            End With

            .TreeLineStyle = DevExpress.XtraTreeList.LineStyle.None

        End With
    End Sub

    Private Sub InitNodeMenu(ByVal _Lst As DevExpress.XtraTreeList.TreeList, ByVal _Node As DevExpress.XtraTreeList.Nodes.TreeListNode, ByVal MnuParent As Double, ByVal _tmpdt As System.Data.DataTable, ByVal _MdlCode As String)
        Dim _DataRow() As DataRow
        _DataRow = _tmpdt.Select("FNMnuIDParent=" & MnuParent & "", "FNSeq")
        If _DataRow.Length = 0 Then Exit Sub

        Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode

        Try

            For Each R2 As DataRow In _DataRow
                Try



                    node = _Lst.AppendNode(New Object() {("|" & (R2!FTCaptionEN.ToString.Trim & "|" & R2!FTCaptionTH.ToString.Trim & "|" & R2!FTCaptionVT.ToString.Trim & "|" & R2!FTCaptionKM.ToString.Trim & "|" & R2!FTCaptionBM.ToString.Trim & "|" & R2!FTCaptionLAO.ToString.Trim & "|" & R2!FTCaptionCH.ToString.Trim)).Split("|")(HI.ST.Lang.Language).ToString,
                                                         R2!FTCaptionEN.ToString.Trim,
                                                         R2!FTCaptionTH.ToString.Trim,
                                                         _MdlCode,
                                                         R2!FTClassName.ToString,
                                                         R2!FTFormName.ToString,
                                                         R2!FTProcName.ToString,
                                                         R2!FTProcType.ToString,
                                                         R2!FTStaFormShow.ToString,
                                                         R2!FTImg.ToString,
                                                         R2!FTStaMasterDynamic.ToString,
                                                         R2!FNHSysModuleID.ToString,
                                                         R2!FTStateReport.ToString,
                                                         R2!FTStateReportDynamic.ToString,
                                                         R2!FTMnuName.ToString,
                                                         R2!FTCallMnuName.ToString,
                                                         R2!FTCallMethodName.ToString,
                                                         R2!FTPram.ToString,
                                                         R2!FTOptionMouseScoll.ToString}, _Node)


                    If R2!FTImg.ToString.Trim <> "" Then
                        Dim tPathImgDis As String = _AppSystemPath & "\Menu\" & R2!FTImg.ToString.Trim
                        If IO.File.Exists(tPathImgDis) Then
                            imagemenuList.Images.Add(Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis))))
                            node.StateImageIndex = imagemenuList.Images.Count - 1
                        End If
                    End If

                    If CheckExistsSubMenu(R2!FNMnuID.ToString, _tmpdt) Then

                        Try
                            node.HasChildren = True
                            node.Tag = True
                        Catch ex As Exception
                        End Try


                        InitNodeMenu(_Lst, node, R2!FNMnuID.ToString, _tmpdt, _MdlCode)
                    Else
                        node.HasChildren = False
                    End If

                Catch
                End Try
            Next
        Catch
        End Try

    End Sub

    Private Function LoadSystemMenu(ByVal ModuleID As Integer, Optional MenuName As String = "") As DataTable
        Try
            Dim _Qry As String
            _Qry = "SELECT DISTINCT A.* "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysMenu AS A With(NOLOCK) "

            If Not (HI.ST.SysInfo.Admin) Or (HI.ST.SysInfo.Admin And Not (HI.ST.SysInfo.AdminAllModule)) Then

                _Qry &= vbCrLf & " INNER JOIN ("
                _Qry &= vbCrLf & "  SELECT        A.FTUserName, B.FTMnuName"
                _Qry &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS A With(NOLOCK)  INNER JOIN"
                _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS B With(NOLOCK)  ON A.FNHSysPermissionID = B.FNHSysPermissionID"
                _Qry &= vbCrLf & " WHERE  A.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

                If MenuName <> "" Then
                    _Qry &= vbCrLf & " AND  B.FTMnuName='" & HI.UL.ULF.rpQuoted(MenuName) & "' "
                End If

                _Qry &= vbCrLf & "   ) AS B"
                _Qry &= vbCrLf & " ON A.FTMnuName = B.FTMnuName"
            Else

            End If

            _Qry &= vbCrLf & " WHERE ISNULL(A.FTStaActive,'0')='1'"

            If MenuName <> "" Then
                _Qry &= vbCrLf & " AND  A.FTMnuName='" & HI.UL.ULF.rpQuoted(MenuName) & "' "
            End If

            _Qry &= vbCrLf & " AND A.FNHSysModuleID=" & ModuleID & " AND A.FNMnuID <> A.FNMnuIDParent "

            Return HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return Nothing
    End Function

    Private Function LoadSystemModule() As DataTable
        Try
            Dim _Qry As String

            _Qry = "SELECT DISTINCT  A.* "
            _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysModule AS A With(NOLOCK) "

            If Not (HI.ST.SysInfo.Admin) Or (HI.ST.SysInfo.Admin And Not (HI.ST.SysInfo.AdminAllModule)) Then

                _Qry &= vbCrLf & " INNER JOIN ("
                _Qry &= vbCrLf & "  SELECT        A.FTUserName, B.FNHSysModuleID"
                _Qry &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS A With(NOLOCK)  INNER JOIN"
                _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionModule AS B With(NOLOCK)  ON A.FNHSysPermissionID = B.FNHSysPermissionID"
                _Qry &= vbCrLf & " WHERE  A.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Qry &= vbCrLf & "   ) AS B"
                _Qry &= vbCrLf & " ON A.FNHSysModuleID = B.FNHSysModuleID"

            End If

            _Qry &= vbCrLf & " WHERE ISNULL(A.FTStaActive,'0')='1'"
            _Qry &= vbCrLf & " ORDER BY A.FNSeq"

            Return HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return Nothing
    End Function

    Private Function CheckExistsSubMenu(ByVal MnuParent As Double, ByVal _tmpdt As System.Data.DataTable) As Boolean

        Try
            Return (_tmpdt.Select("FNMnuIDParent=" & MnuParent & "", "FNSeq").Length > 0)
        Catch ex As Exception
        End Try

        Return False
    End Function

#End Region

#Region "System Function"

    Private Sub GetAllDevExpressPaintStyle()

        Dim _dtSkin As DataTable = HI.Conn.SQLConn.GetDataTable("SELECT * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysSkin WITH(NOLOCK) ", Conn.DB.DataBaseName.DB_SYSTEM)
        Dim _Str As String = ""
        MainRibbonControl.ForceInitialize()

        For Each skin As DevExpress.Skins.SkinContainer In DevExpress.Skins.SkinManager.Default.Skins

            If _dtSkin.Rows.Count <= 0 Then

                _Str = " SELECT TOP 1 FTSkinName FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysSkin WHERE FTSkinName='" & HI.UL.ULF.rpQuoted(skin.SkinName) & "' "
                If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM, "") = "" Then
                    _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysSkin(FTSkinName,FTStateActive) "
                    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(skin.SkinName) & "','1'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

                End If

                Dim item As DevExpress.XtraBars.BarCheckItem = MainRibbonControl.Items.CreateCheckItem(skin.SkinName, False)
                item.Tag = skin.SkinName
                AddHandler item.ItemClick, AddressOf OnPaintStyleClick
                Me.MnuPainStyle.ItemLinks.Add(item)

            Else

                If _dtSkin.Select("FTSkinName='" & HI.UL.ULF.rpQuoted(skin.SkinName) & "' AND FTStateActive='1'").Length > 0 Then
                    Dim item As DevExpress.XtraBars.BarCheckItem = MainRibbonControl.Items.CreateCheckItem(skin.SkinName, False)
                    item.Tag = skin.SkinName
                    AddHandler item.ItemClick, AddressOf OnPaintStyleClick
                    Me.MnuPainStyle.ItemLinks.Add(item)
                Else
                    _Str = " SELECT TOP 1 FTSkinName FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysSkin WHERE FTSkinName='" & HI.UL.ULF.rpQuoted(skin.SkinName) & "' "
                    If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM, "") = "" Then
                        _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysSkin(FTSkinName,FTStateActive) "
                        _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(skin.SkinName) & "','1'"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

                        Dim item As DevExpress.XtraBars.BarCheckItem = MainRibbonControl.Items.CreateCheckItem(skin.SkinName, False)
                        item.Tag = skin.SkinName
                        AddHandler item.ItemClick, AddressOf OnPaintStyleClick
                        Me.MnuPainStyle.ItemLinks.Add(item)

                    End If


                End If

            End If

        Next skin
    End Sub

    Private Sub OnPaintStyleClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
        MainDefaultLookAndFeel.LookAndFeel.SetSkinStyle(e.Item.Tag.ToString())
        HI.UL.AppRegistry.WriteRegistry(UL.AppRegistry.KeyName.Theme, e.Item.Tag.ToString())
    End Sub

    Public Sub FuncItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
        Try

            Dim _DataTag As String = e.Item.Tag.ToString
            Const nCmdName As Integer = 3

            Dim _DTag() As String = _DataTag.Split("|")
            Dim tCmdName As String = ""

            If UBound(_DTag) >= nCmdName Then tCmdName = _DTag(nCmdName)

            If _DTag(0) <> "Y".ToUpper Then
                If IsNothing(Me.ActivedMdiForm) Then Exit Sub
                Dim _FoundOblect As Boolean = False
                For Each ctrl As Object In ActivedMdiForm.Controls.Find(tCmdName, True)

                    _FoundOblect = True

                    If TypeOf ctrl Is DevExpress.XtraEditors.SimpleButton Then

                        With CType(ctrl, DevExpress.XtraEditors.SimpleButton)
                            .PerformClick()
                        End With

                    ElseIf TypeOf ctrl Is System.Windows.Forms.Button Then

                        With CType(ctrl, System.Windows.Forms.Button)
                            .PerformClick()
                        End With

                    End If

                Next

                '----------------------- Layout---------------------------------
                Dim _FoundGrid As Boolean = False
                If e.Item.Name.ToString.ToUpper = "FuncSaveLayout".ToUpper Then
                    If _FoundOblect = False Then
                        Try
                            _FoundGrid = CheckFindControl(Of DevExpress.XtraGrid.GridControl)(ActivedMdiForm)
                        Catch ex As Exception
                        End Try

                        If _FoundGrid = True Then
                            SaveLayout(ActivedMdiForm, ActivedMdiForm)
                            HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
                        End If

                    End If
                ElseIf e.Item.Name.ToString.ToUpper = "FuncDeleteLayout".ToUpper Then
                    If _FoundOblect = False Then
                        Try
                            _FoundGrid = CheckFindControl(Of DevExpress.XtraGrid.GridControl)(ActivedMdiForm)
                        Catch ex As Exception
                        End Try

                        If _FoundGrid = True Then
                            DeleteLayoutGrid(ActivedMdiForm, ActivedMdiForm)
                            HI.MG.ShowMsg.mInfo("Delete Layout Grid Complete...", 1474241101, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
                        End If

                    End If
                End If
                '----------------------- Layout---------------------------------

            Else
                Select Case tCmdName.ToString.ToUpper
                    Case "FuncExitSystem".ToUpper
                        Me.Close()
                End Select
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Function CreateToolBarFunction(ByVal BarManager As DevExpress.XtraBars.BarManager) As Boolean
        Dim ModuleCode As String = ""
        Try

            Dim _tmpdt As DataTable = LoadSystemFunction()
            If IsNothing(_tmpdt) Then Return False

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

            For Each _DataRow As DataRow In _tmpdt.Rows

                oBarButtonItem = New DevExpress.XtraBars.BarLargeButtonItem
                With oBarButtonItem

                    .ItemAppearance.SetFont(New Font(HI.ST.SysInfo.SystemFontName, 10))

                    Dim _ShortCutKey As String = _DataRow("FTShortCut").ToString.Trim
                    Dim _ShortCutKeyDisPlay As String = ""

                    If _ShortCutKey <> "" Then
                        Try

                            Dim _ShortKey As System.Windows.Forms.Keys = New KeysConverter().ConvertFromString(_ShortCutKey)
                            _ShortCutKeyDisPlay = "(" & _ShortCutKey & ")"

                        Catch ex As Exception
                        End Try
                    End If

                    _AllFuncName = _AllFuncName & "|" & _DataRow("FTFuncName").ToString.Trim
                    .Name = _DataRow("FTFuncName").ToString.Trim
                    .Caption = ("|" & (_DataRow("FTFuncCaptionEN").ToString.Trim & "|" & _DataRow("FTFuncCaptionTH").ToString.Trim & "|" & _DataRow("FTFuncCaptionVT").ToString.Trim & "|" & _DataRow("FTFuncCaptionKM").ToString.Trim & "|" & _DataRow("FTFuncCaptionBM").ToString.Trim & "|" & _DataRow("FTFuncCaptionLAO").ToString.Trim & "|" & _DataRow("FTFuncCaptionCH").ToString.Trim)).Split("|")(HI.ST.Lang.Language).ToString & _ShortCutKeyDisPlay
                    .Tag = _DataRow("FNStateFixedShow").ToString.Trim.ToUpper & "|" & _DataRow("FTFuncCaptionEN").ToString.Trim & "|" & _DataRow("FTFuncCaptionTH").ToString.Trim & "|" & _DataRow("FTCommandName").ToString.Trim & "||" & _DataRow("FTShortCut").ToString.Trim
                    '.Width = 30

                    Dim tPathImg As String = _AppSystemPath & "\Func\" & _DataRow("FTFuncImg").ToString.Trim
                    If IO.File.Exists(tPathImg) Then
                        .Glyph = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))

                    End If

                    Dim tPathImgDis As String = _AppSystemPath & "\Func\" & _DataRow("FTFuncImgDisabled").ToString.Trim
                    If IO.File.Exists(tPathImgDis) Then
                        .GlyphDisabled = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImgDis)))

                    ElseIf IO.File.Exists(tPathImg) Then
                        .GlyphDisabled = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
                    End If

                    .Id = BarManager.GetNewItemId()
                    .MergeType = DevExpress.XtraBars.BarMenuMerge.MergeItems

                    Select Case _DataRow("FNStateFixedShow").ToString.Trim.ToUpper
                        Case "Y"
                            .Visibility = BarItemVisibility.Always
                            .Enabled = True
                        Case Else
                            .Visibility = BarItemVisibility.Never
                            .Enabled = False
                    End Select

                    If .Caption = "" Then .Enabled = False

                    Select Case Val(_DataRow("FNAlign").ToString)
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

                mainToolBarFunction.ItemLinks.Add(oBarButtonItem, _DataRow("FTFuncIsBeginGrp").ToString = "1")

                AddHandler oBarButtonItem.ItemClick, AddressOf Me.FuncItemClick
            Next

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ToolBarFunctionActive(Optional ByVal Form As Object = Nothing) As Boolean
        Try
            For Each _BarItem As Object In _BarManager.Items
                Select Case HI.ENM.Control.GeTypeControl(_BarItem)
                    Case ENM.Control.ControlType.BarButtonItem
                        With CType(_BarItem, DevExpress.XtraBars.BarButtonItem)
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

                                                    '----------------------- Layout---------------------------------
                                                    Dim _FoundGrid As Boolean = False
                                                    If .Name.ToString.ToUpper = "FuncSaveLayout".ToUpper Then

                                                        If .Visibility = BarItemVisibility.Never Then
                                                            Try
                                                                _FoundGrid = CheckFindControl(Of DevExpress.XtraGrid.GridControl)(Form)
                                                            Catch ex As Exception
                                                            End Try
                                                        Else
                                                            _FoundGrid = True
                                                        End If

                                                        .Enabled = _FoundGrid
                                                        If .Enabled Then
                                                            .Visibility = BarItemVisibility.Always
                                                            For Each Obj As Object In Form.controls.find(_Commandname, True)
                                                                If TypeOf Obj Is DevExpress.XtraEditors.SimpleButton Then
                                                                    CType(Obj, DevExpress.XtraEditors.SimpleButton).Enabled = True
                                                                ElseIf TypeOf Obj Is System.Windows.Forms.Button Then
                                                                    CType(Obj, System.Windows.Forms.Button).Enabled = True
                                                                Else
                                                                    CType(Obj, DevExpress.XtraEditors.SimpleButton).Enabled = True
                                                                End If
                                                            Next

                                                            If _ShortCutKey <> "" Then
                                                                Try
                                                                    Dim _ShortKey As System.Windows.Forms.Keys = New KeysConverter().ConvertFromString(_ShortCutKey)
                                                                    .ItemShortcut = New DevExpress.XtraBars.BarShortcut(_ShortKey)
                                                                Catch ex As Exception
                                                                End Try
                                                            End If
                                                        End If

                                                    End If

                                                    _FoundGrid = False
                                                    If .Name.ToString.ToUpper = "FuncDeleteLayout".ToUpper Then

                                                        If .Visibility = BarItemVisibility.Never Then
                                                            Try
                                                                _FoundGrid = CheckFindControl(Of DevExpress.XtraGrid.GridControl)(Form)
                                                            Catch ex As Exception
                                                            End Try
                                                        Else
                                                            _FoundGrid = True
                                                        End If

                                                        .Enabled = _FoundGrid
                                                        If .Enabled Then
                                                            .Visibility = BarItemVisibility.Always
                                                            For Each Obj As Object In Form.controls.find(_Commandname, True)
                                                                If TypeOf Obj Is DevExpress.XtraEditors.SimpleButton Then
                                                                    CType(Obj, DevExpress.XtraEditors.SimpleButton).Enabled = True
                                                                ElseIf TypeOf Obj Is System.Windows.Forms.Button Then
                                                                    CType(Obj, System.Windows.Forms.Button).Enabled = True
                                                                Else
                                                                    CType(Obj, DevExpress.XtraEditors.SimpleButton).Enabled = True
                                                                End If
                                                            Next

                                                            If _ShortCutKey <> "" Then
                                                                Try
                                                                    Dim _ShortKey As System.Windows.Forms.Keys = New KeysConverter().ConvertFromString(_ShortCutKey)
                                                                    .ItemShortcut = New DevExpress.XtraBars.BarShortcut(_ShortKey)
                                                                Catch ex As Exception
                                                                End Try
                                                            End If
                                                        End If

                                                    End If
                                                    '----------------------- Layout---------------------------------

                                                End If
                                            End If
                                        End If
                                End Select

                                If .Caption = "" Then .Enabled = False
                            Else
                                .Enabled = True
                            End If
                        End With
                    Case ENM.Control.ControlType.BarLargeButtonItem
                        With CType(_BarItem, DevExpress.XtraBars.BarLargeButtonItem)
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

                                                    '----------------------- Layout---------------------------------
                                                    Dim _FoundGrid As Boolean = False
                                                    If .Name.ToString.ToUpper = "FuncSaveLayout".ToUpper Then

                                                        If .Visibility = BarItemVisibility.Never Then
                                                            Try
                                                                _FoundGrid = CheckFindControl(Of DevExpress.XtraGrid.GridControl)(Form)
                                                            Catch ex As Exception
                                                            End Try
                                                        Else
                                                            _FoundGrid = True
                                                        End If

                                                        .Enabled = _FoundGrid
                                                        If .Enabled Then
                                                            .Visibility = BarItemVisibility.Always
                                                            For Each Obj As Object In Form.controls.find(_Commandname, True)
                                                                If TypeOf Obj Is DevExpress.XtraEditors.SimpleButton Then
                                                                    CType(Obj, DevExpress.XtraEditors.SimpleButton).Enabled = True
                                                                ElseIf TypeOf Obj Is System.Windows.Forms.Button Then
                                                                    CType(Obj, System.Windows.Forms.Button).Enabled = True
                                                                Else
                                                                    CType(Obj, DevExpress.XtraEditors.SimpleButton).Enabled = True
                                                                End If
                                                            Next

                                                            If _ShortCutKey <> "" Then
                                                                Try
                                                                    Dim _ShortKey As System.Windows.Forms.Keys = New KeysConverter().ConvertFromString(_ShortCutKey)
                                                                    .ItemShortcut = New DevExpress.XtraBars.BarShortcut(_ShortKey)
                                                                Catch ex As Exception
                                                                End Try
                                                            End If
                                                        End If

                                                    End If
                                                    _FoundGrid = False
                                                    If .Name.ToString.ToUpper = "FuncDeleteLayout".ToUpper Then

                                                        If .Visibility = BarItemVisibility.Never Then
                                                            Try
                                                                _FoundGrid = CheckFindControl(Of DevExpress.XtraGrid.GridControl)(Form)
                                                            Catch ex As Exception
                                                            End Try
                                                        Else
                                                            _FoundGrid = True
                                                        End If

                                                        .Enabled = _FoundGrid
                                                        If .Enabled Then
                                                            .Visibility = BarItemVisibility.Always
                                                            For Each Obj As Object In Form.controls.find(_Commandname, True)
                                                                If TypeOf Obj Is DevExpress.XtraEditors.SimpleButton Then
                                                                    CType(Obj, DevExpress.XtraEditors.SimpleButton).Enabled = True
                                                                ElseIf TypeOf Obj Is System.Windows.Forms.Button Then
                                                                    CType(Obj, System.Windows.Forms.Button).Enabled = True
                                                                Else
                                                                    CType(Obj, DevExpress.XtraEditors.SimpleButton).Enabled = True
                                                                End If
                                                            Next

                                                            If _ShortCutKey <> "" Then
                                                                Try
                                                                    Dim _ShortKey As System.Windows.Forms.Keys = New KeysConverter().ConvertFromString(_ShortCutKey)
                                                                    .ItemShortcut = New DevExpress.XtraBars.BarShortcut(_ShortKey)
                                                                Catch ex As Exception
                                                                End Try
                                                            End If
                                                        End If

                                                    End If

                                                    '----------------------- Layout---------------------------------

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

    Private Sub SaveLayout(ByVal ObjParent As Object, ByVal MainParent As Object)

        On Error Resume Next
        For Each Obj As Object In ObjParent.Controls
            Select Case True
                Case (TypeOf Obj Is DevExpress.XtraGrid.GridControl)
                    HI.UL.AppRegistry.SaveLayoutGridToRegistry(MainParent, CType(Obj, DevExpress.XtraGrid.GridControl).MainView)
                Case False
            End Select

            If Obj.Controls.count > 0 Then
                SaveLayout(Obj, MainParent)
            End If
        Next


    End Sub

    Private Sub DeleteLayoutGrid(ByVal ObjParent As Object, ByVal MainParent As Object)

        On Error Resume Next
        For Each Obj As Object In ObjParent.Controls
            Select Case True
                Case (TypeOf Obj Is DevExpress.XtraGrid.GridControl)
                    HI.UL.AppRegistry.DeleteLayoutGridToRegistry(MainParent, CType(Obj, DevExpress.XtraGrid.GridControl).MainView)
                Case False
            End Select

            If Obj.Controls.count > 0 Then
                DeleteLayoutGrid(Obj, MainParent)
            End If
        Next

    End Sub

    Private Sub LoadLayout(ByVal ObjParent As Object, ByVal MainParent As Object)

        On Error Resume Next
        For Each Obj As Object In ObjParent.Controls
            Select Case True
                Case (TypeOf Obj Is DevExpress.XtraGrid.GridControl)

                    Call HI.UL.AppRegistry.LoadLayoutGridFromRegistry(MainParent, CType(Obj, DevExpress.XtraGrid.GridControl).MainView)

                Case False
            End Select

            If Obj.Controls.count > 0 Then
                LoadLayout(Obj, MainParent)
            End If
        Next


    End Sub

    Private Function CheckFindControl(Of T)(ByVal ObjParent As Object, Optional ByVal StateLoop As Boolean = False) As Boolean

        On Error Resume Next
        For Each Obj As Object In ObjParent.Controls
            Select Case True
                Case (TypeOf Obj Is T)
                    Return True
                Case False
            End Select

            If Obj.Controls.count > 0 Then
                If CheckFindControl(Of T)(Obj, True) Then
                    Return True
                End If

            End If

        Next

        If StateLoop = False Then
            Return False
        End If
    End Function

    Private Function LoadSystemFunction() As DataTable
        Try
            Dim _Qry As String
            _Qry = "SELECT * "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysFunc AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE ISNULL(FTFuncStaActive,'0')='1'"
            _Qry &= vbCrLf & " ORDER BY FNAlign,FNFuncSeqNo"
            Return HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SYSTEM)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub ProcLoadLang(ByVal _lang As HI.ST.Lang.eLang)
        With Me.FNLang
            .Properties.Items.Clear()
            Dim _ImgIndex As Integer = 0
            Dim _arr(15) As String
            For Each Str As String In HI.TL.CboList.SetList("FNLang")
                Dim _Item As New DevExpress.XtraEditors.Controls.ImageComboBoxItem
                With _Item
                    .Description = Str
                    .ImageIndex = _ImgIndex
                End With

                _arr(_ImgIndex) = Str
                _ImgIndex = _ImgIndex + 1

            Next

            .Properties.Items.AddRange({New DevExpress.XtraEditors.Controls.ImageComboBoxItem(_arr(0), 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(_arr(1), 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(_arr(2), 2), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(_arr(3), 3), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(_arr(4), 4), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(_arr(5), 5), New DevExpress.XtraEditors.Controls.ImageComboBoxItem(_arr(6), 6)})
            .SelectedIndex = (HI.ST.Lang.Language - 1)

        End With

    End Sub

    Private Sub LoadFontFromSystem()

        FNFont.Properties.Items.Clear()
        Dim fonts As New InstalledFontCollection

        For Each one As FontFamily In fonts.Families

            FNFont.Properties.Items.Add(one.Name, one.Name, 0)

        Next

        FNFont.Text = HI.ST.SysInfo.SystemFontName
    End Sub

    Private Sub FuncExitSystem()
        Me.Close()
    End Sub

    Public Sub CallWindowForm(MenuName As String, Optional PropertyInFoName As String = Nothing, Optional Pram() As String = Nothing)
        Dim _Str As String = ""
        Dim dt As DataTable
        Dim _ModuleID As String = ""
        Dim _ModuleName As String = ""

        _Str = " SELECT       M.FTMnuName, M.FNHSysModuleID, MD.FTModuleCode"
        _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysMenu AS M WITH(NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysModule AS MD WITH(NOLOCK) ON M.FNHSysModuleID = MD.FNHSysModuleID"
        _Str &= vbCrLf & " WHERE M.FTMnuName='" & HI.UL.ULF.rpQuoted(MenuName) & "' "

        dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SYSTEM)

        For Each R As DataRow In dt.Rows
            _ModuleID = R!FNHSysModuleID.ToString
            _ModuleName = R!FTModuleCode.ToString

            Exit For
        Next

        If _ModuleID <> "" And _ModuleName <> "" Then
            dt = LoadSystemMenu(_ModuleID, MenuName)

            If dt.Rows.Count > 0 Then
                For Each R2 As DataRow In dt.Rows

                    Dim _MenuName As String = ("|" & (R2!FTCaptionEN.ToString.Trim & "|" & R2!FTCaptionTH.ToString.Trim & "|" & R2!FTCaptionEN.ToString.Trim & "|" & R2!FTCaptionEN.ToString.Trim)).Split("|")(HI.ST.Lang.Language).ToString
                    Dim _Assembly As String = R2!FTClassName.ToString
                    Dim _FormName As String = R2!FTFormName.ToString
                    Dim _PorcName As String = R2!FTProcName.ToString
                    Dim _PorcType As String = R2!FTProcType.ToString
                    Dim _FormShow As String = R2!FTStaFormShow.ToString
                    Dim _Image As String = R2!FTImg.ToString
                    Dim _DynamicForm As String = R2!FTStaMasterDynamic.ToString
                    Dim _FTStateReport As String = R2!FTStateReport.ToString
                    Dim _FTStateReportDynamic As String = R2!FTStateReportDynamic.ToString
                    Dim _SysMenuName As String = R2!FTMnuName.ToString
                    Dim _FTCallMnuName As String = R2!FTCallMnuName.ToString
                    Dim _FTCallMethodName As String = R2!FTCallMethodName.ToString
                    Dim _FTPram As String = R2!FTPram.ToString
                    Dim _FTOptionMouseScoll As String = R2!FTOptionMouseScoll.ToString

                    Call ShowWindowForm(_MenuName, _ModuleName, _Assembly, _FormName, _PorcName, _PorcType, _FormShow, _Image, _DynamicForm, _ModuleID, _FTStateReport, _FTStateReportDynamic, _SysMenuName, _FTCallMnuName, _FTCallMethodName, _FTPram, PropertyInFoName, Pram, _FTOptionMouseScoll)

                    Exit For
                Next
            Else
            End If
        End If

        dt.Dispose()

    End Sub

    Private Sub ShowWindowForm(_MenuName As String,
                               _ModuleName As String, _
                               _Assembly As String, _
                               _FormName As String, _
                               _PorcName As String, _
                               _PorcType As String, _
                               _FormShow As String, _
                               _Image As String, _
                               _DynamicForm As String, _
                               _ModuleID As String, _
                               _FTStateReport As String, _
                              _FTStateReportDynamic As String, _
                              _SysMenuName As String, _
                              _FTCallMnuName As String, _
                              _FTCallMethodName As String, _
                              _FTPram As String, _
                              Optional PropertyInFoName As String = Nothing, _
                              Optional Pram() As String = Nothing, Optional _FTOptionMouseScoll As String = "")

        Try

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"




            HI.ST.SysInfo.ModuleName = _ModuleName
            HI.ST.SysInfo.ModuleID = _ModuleID
            HI.ST.SysInfo.MenuName = _SysMenuName
            HI.ST.SysInfo.FTOptionMouseScoll = _FTOptionMouseScoll
            If _FormName.Trim <> "" Then 'OpenForm

                For Each obj As Object In Me.MdiChildren
                    If obj.Name = _FormName.Trim Then

                        ' Start Call Method 
                        If Not (PropertyInFoName Is Nothing) Then
                            Dim T As System.Type = obj.GetType()
                            Dim _minfo As MethodInfo
                            _minfo = T.GetMethod(PropertyInFoName)

                            If Not (_minfo Is Nothing) Then
                                _minfo.Invoke(obj, Pram)
                            End If
                        End If
                        ' End Call Method 

                        obj.Select()
                        Exit Sub
                    End If
                Next

                _Splash = New HI.TL.SplashScreen("Wisdom System 2015", "Loading...")
                _Splash.UpdateInformation("Prepare Form.... Please wait")

                Dim ObjForm As New Object
                HI.TL.HandlerControl.SetStateProcClear = True
                If _DynamicForm <> "" Then

                    Select Case Integer.Parse(Val(_DynamicForm))
                        Case 0
                            ObjForm = New HI.TL.wDynamicMaster(_FormName, _MenuName, _Image)
                        Case 1
                            ObjForm = New HI.TL.wDynamicMasterAddEdit(_FormName, _MenuName, _PathFileDll, _Image)
                        Case 2
                            ObjForm = New HI.TL.wDynamicMasterAddEditDynamic(_FormName, _MenuName, _PathFileDll, _Image)
                    End Select

                    HI.TL.HandlerControl.AddHandlerObj(ObjForm)

                Else
                    Dim FormReference As String = String.Concat(_Assembly & ".", _FormName)

                    If _Assembly = "" Then
                        _Assembly = "HI"
                        Dim oApp As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
                        ObjForm = oApp.CreateInstance(FormReference)
                    Else
                        '-----------------------Start generate Form Popup From Dll Reference------------------
                        Try

                            _Splash.UpdateInformation("Load Assembly System.... Please wait")

                            Dim _App As System.Reflection.Assembly = System.Reflection.Assembly.Load(System.IO.File.ReadAllBytes(_PathFileDll & "\" & _Assembly & ".dll"))


                            If _FTStateReport = "1" And _FTStateReportDynamic <> "" Then
                                ObjForm = _App.CreateInstance(FormReference, True, BindingFlags.CreateInstance, Nothing, New Object() {_FTStateReportDynamic}, Nothing, Nothing)
                            Else
                                ObjForm = _App.CreateInstance(FormReference)
                            End If

                        Catch ex As Exception

                        End Try
                        '-----------------------End generate Form Popup From Dll Reference------------------
                    End If

                    _Splash.UpdateInformation("Prepare Form.... Please wait")

                    Try
                        HI.TL.HandlerControl.AddHandlerObj(ObjForm)
                    Catch ex As Exception

                    End Try
                End If

                HI.TL.HandlerControl.SetStateProcClear = False
                HI.TL.HandlerControl.ClearControl(ObjForm)

                Dim oSysLang As New ST.SysLanguage
                With ObjForm

                    .Tag = _ModuleName & "|||" & _ModuleID.ToString

                    If _Image <> "" Then
                        Dim tPathImgDis As String = _AppSystemPath & "\Menu\" & _Image
                        If IO.File.Exists(tPathImgDis) Then
                            Me.Icon = Icon.FromHandle(DirectCast(Image.FromFile(tPathImgDis), Bitmap).GetHicon())
                            .Icon = Me.Icon
                        End If
                    End If

                    Try
                        Call oSysLang.LoadObjectLanguage(_ModuleID, ObjForm.Name.ToString.Trim, ObjForm)
                    Catch ex As Exception
                    Finally
                    End Try

                    If _FTCallMnuName <> "" Then
                        Dim T As System.Type = ObjForm.GetType()
                        Dim _pdbnameinfo As PropertyInfo = T.GetProperty("CallMenuName")

                        If Not (_pdbnameinfo Is Nothing) Then
                            _pdbnameinfo.SetValue(ObjForm, _FTCallMnuName, Nothing)

                            If _FTCallMethodName <> "" Then
                                _pdbnameinfo = T.GetProperty("CallMethodName")
                                If Not (_pdbnameinfo Is Nothing) Then
                                    _pdbnameinfo.SetValue(ObjForm, _FTCallMethodName, Nothing)
                                End If
                            End If

                            If _FTPram <> "" Then
                                _pdbnameinfo = T.GetProperty("CallMethodParm")
                                If Not (_pdbnameinfo Is Nothing) Then
                                    _pdbnameinfo.SetValue(ObjForm, _FTPram, Nothing)
                                End If
                            End If
                        End If
                    End If

                    Select Case _FormShow.Trim
                        Case 1
                            _Splash.Close()

                            .ControlBox = True
                            .MaximizeBox = False
                            .MinimizeBox = False
                            .FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
                            .StartPosition = FormStartPosition.CenterScreen
                            .ShowDialog()

                        Case 2

                            '----------------------- Layout---------------------------------
                            Dim _FoundGrid As Boolean = False

                            Try
                                _FoundGrid = CheckFindControl(Of DevExpress.XtraGrid.GridControl)(ObjForm)
                            Catch ex As Exception
                            End Try

                            If _FoundGrid = True Then
                                LoadLayout(ObjForm, ObjForm)
                            End If

                            '----------------------- Layout---------------------------------

                            .MdiParent = Me
                            .WindowState = FormWindowState.Maximized
                            .Show()
                            _Splash.Close()
                        Case Else
                            _Splash.Close()
                            .Show()
                    End Select

                    If Not (PropertyInFoName Is Nothing) Then
                        Dim T As System.Type = ObjForm.GetType()
                        Dim _minfo As MethodInfo
                        _minfo = T.GetMethod(PropertyInFoName)

                        If Not (_minfo Is Nothing) Then
                            _minfo.Invoke(ObjForm, Pram)
                        End If
                    End If


                End With
            End If
        Catch ex As Exception
            HI.TL.HandlerControl.SetStateProcClear = False
            Try
                _Splash.Close()
            Catch ex2 As Exception
            End Try
            _Splash.Close()

        End Try
    End Sub

#End Region

#Region "SystemStatusBar"

    Public Function CreateStatusBar(ByVal BarManager As DevExpress.XtraBars.BarManager) As Boolean
        Try
            Dim oStatusBar As New DevExpress.XtraBars.Bar(BarManager)
            For Each obj As DevExpress.XtraBars.Bar In BarManager.Bars
                If obj.IsStatusBar Then
                    BarManager.Bars.RemoveAt(BarManager.Bars.IndexOf(obj))
                End If
            Next
            BarManager.StatusBar = oStatusBar
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

#End Region

#Region "System Event"

    Private Sub MdiManager_SelectedPageChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MdiManager.SelectedPageChanged
        Try
            Dim _Lang As Integer = HI.ST.Lang.Language

            If IsNothing(Me.ActivedMdiForm) Then

                Me.ToolBarFunctionActive()
                Me.Text = Me.Tag.ToString.Split("|")(_Lang)

            Else

                Me.ToolBarFunctionActive(Me.ActivedMdiForm)

                If ActivedMdiForm.Tag.ToString.Split("|").Length >= 3 Then

                    Dim _ModuleName As String = ActivedMdiForm.Tag.ToString.Split("|")(0)
                    Dim _ModuleID As String = ActivedMdiForm.Tag.ToString.Split("|")(8)
                    Dim _MenuName As String = ActivedMdiForm.Tag.ToString.Split("|")(9)
                    Dim _FTOptionMouseScoll As String = ActivedMdiForm.Tag.ToString.Split("|")(10)

                    For Each ObjGrp As DevExpress.XtraNavBar.NavBarGroup In Me.onvbar.Groups
                        If ObjGrp.Name = _ModuleName Then
                            _ModuleName = ObjGrp.Caption
                            Exit For
                        End If
                    Next

                    If _ModuleName = "" Then
                        HI.ST.SysInfo.ModuleName = "WISDOM"
                        HI.ST.SysInfo.ModuleID = _ModuleID
                        HI.ST.SysInfo.MenuName = _MenuName
                        HI.ST.SysInfo.FTOptionMouseScoll = _FTOptionMouseScoll
                    Else
                        HI.ST.SysInfo.ModuleName = _ModuleName
                        HI.ST.SysInfo.ModuleID = _ModuleID
                        HI.ST.SysInfo.MenuName = _MenuName
                        HI.ST.SysInfo.FTOptionMouseScoll = _FTOptionMouseScoll
                    End If

                    Me.Text = Me.Tag.ToString.Split("|")(_Lang) & "  (  " & _ModuleName & "  )  "

                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub otmtime_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles otmtime.Tick
        Me.olbtime.Caption = Format(Date.Now, "HH:mm:ss")

        'Try
        '    Dim allScreens As Screen() = Screen.AllScreens
        '    Dim currentScreen As Screen = Screen.FromControl(Me)
        '    HI.ST.SysInfo.AppActScreen = Array.IndexOf(allScreens, currentScreen)
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub onvbar_ActiveGroupChanged(ByVal sender As Object, ByVal e As DevExpress.XtraNavBar.NavBarGroupEventArgs) Handles onvbar.ActiveGroupChanged
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

    Private Sub onvbar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles onvbar.MouseDown
        If e.Button = MouseButtons.Left Then
            Dim navBar As NavBarControl = TryCast(sender, NavBarControl)
            Dim hitInfo As NavBarHitInfo = navBar.CalcHitInfo(New Point(e.X, e.Y))
            If hitInfo.InGroupCaption AndAlso (Not hitInfo.InGroupButton) Then
                hitInfo.Group.Expanded = Not hitInfo.Group.Expanded
            End If
        End If
    End Sub

    Private Sub onvbar_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles onvbar.SizeChanged
        For Each ObjGrp As DevExpress.XtraNavBar.NavBarGroup In Me.onvbar.Groups
            Try
                If Not (ObjGrp.ControlContainer Is Nothing) Then
                    For Each Obj As Object In ObjGrp.ControlContainer.Controls
                        If TypeOf (Obj) Is DevExpress.XtraTreeList.TreeList Then
                            With CType(Obj, DevExpress.XtraTreeList.TreeList)
                                .Width = onvbar.Width
                            End With
                        End If
                    Next
                End If
            Catch ex As Exception
            End Try
        Next
    End Sub

    Private Sub Menu_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            With CType(sender, DevExpress.XtraTreeList.TreeList)
                Dim _hifo As TreeListHitInfo = .CalcHitInfo(.PointToClient(Control.MousePosition))
                If (_hifo.Node IsNot Nothing) Then
                    With _hifo.Node

                        If Convert.ToBoolean(.Tag) = False Then

                            Dim _MenuName As String = .GetValue(0).ToString
                            Dim _ModuleName As String = .GetValue(3).ToString
                            Dim _Assembly As String = .GetValue(4).ToString
                            Dim _FormName As String = .GetValue(5).ToString
                            Dim _PorcName As String = .GetValue(6).ToString
                            Dim _PorcType As String = .GetValue(7).ToString
                            Dim _FormShow As String = .GetValue(8).ToString
                            Dim _Image As String = .GetValue(9).ToString
                            Dim _DynamicForm As String = .GetValue(10).ToString
                            Dim _ModuleID As String = .GetValue(11).ToString
                            Dim _FTStateReport As String = .GetValue(12).ToString
                            Dim _FTStateReportDynamic As String = .GetValue(13).ToString
                            Dim _SysMenuName As String = .GetValue(14).ToString
                            Dim _FTCallMnuName As String = .GetValue(15).ToString
                            Dim _FTCallMethodName As String = .GetValue(16).ToString
                            Dim _FTPram As String = .GetValue(17).ToString
                            Dim _FTOptionMouseScoll As String = .GetValue(18).ToString

                            Call ShowWindowForm(_MenuName, _ModuleName, _Assembly, _FormName, _PorcName, _PorcType, _FormShow, _Image, _DynamicForm, _ModuleID, _FTStateReport, _FTStateReportDynamic, _SysMenuName, _FTCallMnuName, _FTCallMethodName, _FTPram, Nothing, Nothing, _FTOptionMouseScoll)

                        End If
                    End With
                End If
            End With
        Catch ex As Exception
            Try
                _Splash.Close()
            Catch ex2 As Exception
            End Try
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Main_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Try
            Dim allScreens As Screen() = Screen.AllScreens
            Dim currentScreen As Screen = Screen.FromControl(Me)
            HI.ST.SysInfo.AppActScreen = Array.IndexOf(allScreens, currentScreen)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MainRibbon_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not (StateUserLogOff) Then

            Try
                Dim ProcessAppName As Process() = Process.GetProcessesByName("WisdomService")
                Dim ProcessAppName2 As Process() = Process.GetProcessesByName("WisdomService.exe")

                If ProcessAppName.Length >= 1 Or ProcessAppName2.Length >= 1 Then
                    For Each pkill As Process In ProcessAppName
                        pkill.Kill()
                    Next

                    For Each pkill As Process In ProcessAppName2
                        pkill.Kill()
                    Next

                End If
            Catch ex As Exception

            End Try



            Application.Exit()
        End If
    End Sub

    Private Sub MainRibbon_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If Not (StateUserLogOff) Then
                If HI.MG.ShowMsg.mConfirmProcess("---------", 1000000010) = False Then
                    e.Cancel = True
                Else

                    Dim _Str As String

                    Try

                        _Str = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginState  "
                        _Str &= vbCrLf & " WHERE  FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
                        _Str &= vbCrLf & " AND  FTLogInCom='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserLogInComputer) & "' "

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

                    Catch ex As Exception
                    End Try

                    Try

                        _Str = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginLogOutHistory (FTUserName, FTIP, FTDate, FTTime, FTCom,FTStateStatus) "
                        _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserLogInComputerIP) & "'"
                        _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                        _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                        _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserLogInComputer) & "','1'"

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_SECURITY)

                    Catch ex As Exception
                    End Try

                End If

            Else
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub otmchkuserlogin_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles otmchkuserlogin.Tick
        'If _TimcCountCheckUserLogin < 60 Then
        '    _TimcCountCheckUserLogin = _TimcCountCheckUserLogin + 1

        '    If _TimcCountCheckUserLogin >= 60 Then
        '        Dim _Theard As New Thread(AddressOf CheckUserLogin)
        '        _Theard.Start(HI.ST.UserInfo.UserName)
        '    End If

        'End If

        If (_StateCheckUser) Then
            _StateCheckUser = False

            Dim _Theard As New Thread(AddressOf CheckUserLogin)
            _Theard.Start(HI.ST.UserInfo.UserName)

        End If

    End Sub

    Private Sub FNLang_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNLang.SelectedIndexChanged
        If Not (_ProcLoad) Then
            Dim _Spls As New HI.TL.SplashScreen("Switch Language... Please Wait..")

            HI.ST.Lang.Language = FNLang.SelectedIndex + 1

            HI.ST.Lang.SP_SETxLanguage(Me)
            Call CreateSystemMainMenu(Me)
            Me.CreateToolBarFunction(Me._BarManager)

            Dim _CmpName As String = ""

            Dim _Str As String = ""
            _Str = " SELECT TOP 1 "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Str &= vbCrLf & " FTCmpNameTH  "
            Else
                _Str &= vbCrLf & " FTCmpNameEN  "
            End If

            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS H WITH(NOLOCK)   "
            _Str &= vbCrLf & " WHERE FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "

            _CmpName = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "")

            olbcmp.Caption = _CmpName

            For Each oForm As Object In Me.MdiChildren

                Try

                    If oForm.name.ToString.ToUpper = "wFormScreen".ToUpper Then

                        For Each _ctrl As Object In oForm.Controls.Find("olbcmp", True)
                            _ctrl.Text = _CmpName
                            Exit For
                        Next

                        Continue For

                    End If

                Catch ex As Exception
                End Try

                HI.ST.Lang.SP_SETxLanguage(oForm)

            Next

            HI.UL.AppRegistry.WriteRegistry(UL.AppRegistry.KeyName.Language, HI.ST.Lang.Language)
            Me.ToolBarFunctionActive(Me.ActivedMdiForm)
            _Spls.Close()
        End If
    End Sub

    Private Sub MdiManager_PageRemoved(sender As System.Object, e As DevExpress.XtraTabbedMdi.MdiTabPageEventArgs) Handles MdiManager.PageRemoved
        Try
            If MdiManager.Pages.Count > 1 Then
                MdiManager.SelectedPage = MdiManager.Pages.Item(MdiManager.Pages.Count - 1)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Form_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        StateUserLogOff = False
        _StateCheckPOApp = True
        _StateCheckPOPDF = True
        _StateCheckPOAppDirector = True
        _StateCheckManagerFactory = True
        _StateCheckMerAppTVW = True
        _StateCheckinvoicecharge = True
        _StateFormLoadSucess = True

        otmwisdomservice.Enabled = True

    End Sub

    Private Sub FNLang_Spin(sender As Object, e As DevExpress.XtraEditors.Controls.SpinEventArgs) Handles FNLang.Spin
        e.Handled = False
    End Sub

    Private Sub ocmcheckapp_Tick(sender As Object, e As EventArgs) Handles ocmcheckapp.Tick

        If (_StateCheckPOApp) And (HI.ST.SysInfo.StateManager = True) Then
            _StateCheckPOApp = False
            Dim _Theard As New Thread(AddressOf CheckPOApp)
            _Theard.Start()
        End If

    End Sub

    Private Sub otmpdf_Tick(sender As Object, e As EventArgs) Handles otmpdf.Tick
        If (_StateCheckPOPDF) And HI.ST.SysInfo.StateUserPurchaseTeam Then
            _StateCheckPOPDF = False
            Dim _Theard As New Thread(AddressOf CheckPOPDF)
            _Theard.Start()
        End If
    End Sub

    Private Sub opmmail_Click(sender As Object, e As EventArgs) Handles opmmail.Click
        If opmmail.Visible Then
            opmmail.Visible = False
            Call CallWindowForm("mnuMail")
        End If
    End Sub

    Private Sub otbcheckmail_Tick(sender As Object, e As EventArgs) Handles otbcheckmail.Tick
        If (_StateCheckMailUser) And (HI.ST.SysInfo.StateUserPurchaseTeam = True OrElse HI.ST.SysInfo.StateUserStockTeam = True OrElse HI.ST.SysInfo.StateUserProdStaff) Then
            _StateCheckMailUser = False
            Dim _Theard As New Thread(AddressOf CheckMailUser)
            _Theard.Start()
        End If
    End Sub

    Private Sub ocmcheckappdirector_Tick(sender As Object, e As EventArgs) Handles ocmcheckappdirector.Tick
        If (_StateCheckPOAppDirector) And (HI.ST.SysInfo.StateDirector = True) Then
            _StateCheckPOAppDirector = False
            Dim _Theard As New Thread(AddressOf CheckPOAppDirector)
            _Theard.Start()
        End If
    End Sub

#End Region

#Region "User"

    Private Delegate Sub DelegateCheckUserLogin(ByVal _Username As String)
    Private Sub CheckUserLogin(ByVal _Username As String)
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckUserLogin(AddressOf CheckUserLogin), New Object() {_Username})
        Else
            If _Username <> "" Then
                If Not (HI.ST.SysInfo.Admin) Then
                    Dim _Str As String = ""
                    Dim _Dt As DataTable

                    _Str = "SELECT   TOP 1  FTUserName, FTLogInIP, FTLogInDate, FTLogInTime, FTLogInCom"
                    _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginState WITH(NOLOCK) "
                    _Str &= vbCrLf & " WHERE  FTUserName='" & HI.UL.ULF.rpQuoted(_Username) & "'  "


                    _Dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

                    If _Dt.Rows.Count > 0 Then
                        Dim _msg As String = "This User are Connected"
                        _msg &= vbCrLf & "By IP : " & _Dt.Rows(0)!FTLogInIP.ToString
                        _msg &= vbCrLf & "By Computername : " & _Dt.Rows(0)!FTLogInCom.ToString
                        _msg &= vbCrLf & " "
                        _msg &= vbCrLf & " Users will be removed to Connect. "

                        If _Dt.Rows(0)!FTLogInIP.ToString <> HI.ST.UserInfo.UserLogInComputerIP Then
                            _Dt.Dispose()
                            MsgBox(_msg)
                            StateUserLogOff = True
                            Application.Exit()

                        Else
                            _Dt.Dispose()
                        End If

                    Else

                        Try
                            If _Dt.Columns.Count > 0 Then
                                _Dt.Dispose()
                                MsgBox("Not Foud Stasus Login To System !!!")
                                StateUserLogOff = True
                                Application.Exit()
                            Else
                                _Dt.Dispose()
                            End If
                        Catch ex As Exception

                        End Try

                    End If



                End If
            End If

            _StateCheckUser = True
        End If
        _TimcCountCheckUserLogin = 0
    End Sub

#End Region

#Region "PO"

    Private Delegate Sub DelegateCheckPOApp()
    Private Sub CheckPOApp()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckPOApp(AddressOf CheckPOApp), New Object() {})
        Else

            HI.Service.ClsService.ValidateApp(HI.ST.SysInfo.AppActScreen)
            _StateCheckPOApp = True
        End If
    End Sub

    Private Delegate Sub DelegateCheckPOAppDirector()
    Private Sub CheckPOAppDirector()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckPOAppDirector(AddressOf CheckPOAppDirector), New Object() {})
        Else

            HI.Service.ClsService.ValidateAppDirector(HI.ST.SysInfo.AppActScreen)
            _StateCheckPOAppDirector = True
        End If
    End Sub

    Private Delegate Sub DelegateCheckManagerFactoryApprove()
    Private Sub CheckManagerFactoryApp()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckManagerFactoryApprove(AddressOf CheckManagerFactoryApp), New Object() {})
        Else

            HI.Service.ClsService.ValidateAppFactoryManager(HI.ST.SysInfo.AppActScreen)
            _StateCheckManagerFactory = True
        End If
    End Sub

    Private Delegate Sub DelegateCheckWHAppCM()
    Private Sub CheckWHAppCM()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckWHAppCM(AddressOf CheckWHAppCM), New Object() {})
        Else
            HI.Service.ClsService.ValidateAppWHCM(HI.ST.SysInfo.AppActScreen)
            _StateCheckWHAppCm = True
        End If
    End Sub


    Private Delegate Sub DelegateCheckMerAppTVW()
    Private Sub CheckMerAppTVW()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckMerAppTVW(AddressOf CheckMerAppTVW), New Object() {})
        Else
            HI.Service.ClsService.ValidateMerManagerApp(HI.ST.SysInfo.AppActScreen)
            _StateCheckMerAppTVW = True
        End If
    End Sub

    Private Delegate Sub DelegateCheckSewingLineLeaderApp()
    Private Sub CheckSewingLineLeaderApp()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckSewingLineLeaderApp(AddressOf CheckSewingLineLeaderApp), New Object() {})
        Else
            HI.Service.ClsService.ValidateLineLeaderApp(HI.ST.SysInfo.AppActScreen)
            _StateCheckSewingLineLeader = True
        End If
    End Sub

    Private Delegate Sub DelegateCheckQAFinalLeaderApp()
    Private Sub CheckQAFinalLeaderApp()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckQAFinalLeaderApp(AddressOf CheckQAFinalLeaderApp), New Object() {})
        Else
            HI.Service.ClsService.ValidateQAFinalLeaderApp(HI.ST.SysInfo.AppActScreen)
            _StateCheckQAFinalLeader = True
        End If
    End Sub

    Private Delegate Sub DelegateCheckUserDocumentControl()
    Private Sub CheckUserDocumentControl()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckUserDocumentControl(AddressOf CheckUserDocumentControl), New Object() {})
        Else
            HI.Service.ClsService.ValidateAppDocumentation(HI.ST.SysInfo.AppActScreen)
            _StateUserDC = True
        End If
    End Sub


    Private Delegate Sub DelegateCheckUserAppPRDataInfoo()
    Private Sub CheckUserAppPRDataInfo()

        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckUserAppPRDataInfoo(AddressOf CheckUserAppPRDataInfo), New Object() {})
        Else
            HI.Service.ClsService.ValidateAppPR(HI.ST.SysInfo.AppActScreen)
            _StateUserAppPR = True
        End If

    End Sub

    Private Delegate Sub DelegateCheckInvoiceCharge()
    Private Sub CheckCheckInvoiceCharge()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckInvoiceCharge(AddressOf CheckCheckInvoiceCharge), New Object() {})
        Else
            Dim _Lang As String = "TH"

            If HI.ST.Lang.Language = Lang.eLang.TH Then
                _Lang = "TH"
            Else
                _Lang = "EN"
            End If

            Dim _dtCharge As DataTable
            Dim _Str As String = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.SP_GETINVOICECHARGE_ALERT " & HI.ST.SysInfo.StateUserInvoiceChargeDay & ",'" & HI.UL.ULF.rpQuoted(_Lang) & "' "

            _dtCharge = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_ACCOUNT)

            If _dtCharge.Rows.Count > 0 Then

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                Dim _WInvoiceChargeAlert As New wListInvoiceChargeAlert

                HI.ST.SysInfo.MenuName = "mnuSecurity"

                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _WInvoiceChargeAlert.Name.ToString.Trim, _WInvoiceChargeAlert)
                Catch ex As Exception
                Finally
                End Try

                With _WInvoiceChargeAlert
                    .DataPO = _dtCharge.Copy
                    .RefreshDataPO()
                    .ShowDialog()
                End With

                HI.ST.SysInfo.MenuName = _TmpMenu

            End If

            _dtCharge.Dispose()

            _StateCheckinvoicecharge = True
        End If
    End Sub

    Private Delegate Sub DelegateCheckPOPDF()
    Private Sub CheckPOPDF()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckPOPDF(AddressOf CheckPOPDF), New Object() {})
        Else
            HI.Service.ClsConvertPDF.Validate_PDF()
            _StateCheckPOPDF = True
        End If
    End Sub

    Private Delegate Sub DelegateCheckMail()
    Private Sub CheckMailUser()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckMail(AddressOf CheckMailUser), New Object() {})
        Else

            If HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MAIL) <> "" Then
                Dim _Str As String = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MAIL) & "].dbo.SP_CHECKMAIL_USER '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

                opmmail.Visible = (HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MAIL, "", True) <> "")
            End If

            _StateCheckMailUser = True
        End If
    End Sub

    Private Delegate Sub DelegeteCheckOrderCost()
    Private Sub CheckOrderCost()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegeteCheckOrderCost(AddressOf CheckOrderCost), New Object() {})
        Else
            HI.Service.ClsService.ValidateAppOrderCost(HI.ST.SysInfo.AppActScreen)
            _StateChekOrderCost = True
        End If
    End Sub

#End Region

    Private Sub ocmcheckmanagerfactoryapp_Tick(sender As Object, e As EventArgs) Handles ocmcheckmanagerfactoryapp.Tick
        If (_StateCheckManagerFactory) And (HI.ST.SysInfo.StateFactoryManager = True) Then
            _StateCheckManagerFactory = False

            Dim _Theard As New Thread(AddressOf CheckManagerFactoryApp)
            _Theard.Start()

        End If
    End Sub

    Private Sub ocmcheckwhappcm_Tick(sender As Object, e As EventArgs) Handles ocmcheckwhappcm.Tick
        If (_StateCheckWHAppCm) And (HI.ST.SysInfo.StateWHAppCM = True) Then
            _StateCheckWHAppCm = False

            Dim _Theard As New Thread(AddressOf CheckWHAppCM)
            _Theard.Start()

        End If
    End Sub

    Private Sub otmcheckmerapptvw_Tick(sender As Object, e As EventArgs) Handles otmcheckmerapptvw.Tick
        If (_StateCheckMerAppTVW) And (HI.ST.SysInfo.StateSuperVisorMer = True) Then
            _StateCheckMerAppTVW = False

            Dim _Theard As New Thread(AddressOf CheckMerAppTVW)
            _Theard.Start()

        End If
    End Sub

    Private Sub otmcheckinvoicecharge_Tick(sender As Object, e As EventArgs) Handles otmcheckinvoicecharge.Tick
        If (_StateCheckinvoicecharge) And (HI.ST.SysInfo.StateUserInvoiceCharge = True) And HI.ST.SysInfo.StateUserInvoiceChargeDay > 0 Then
            _StateCheckinvoicecharge = False
            Dim _Theard As New Thread(AddressOf CheckCheckInvoiceCharge)
            _Theard.Start()
        End If
    End Sub

    Private Sub otmsewinglineleader_Tick(sender As Object, e As EventArgs) Handles otmsewinglineleader.Tick
        If (_StateCheckSewingLineLeader) And (HI.ST.SysInfo.StateUserSewingLineLeader = True) Then
            _StateCheckSewingLineLeader = False
            Dim _Theard As New Thread(AddressOf CheckSewingLineLeaderApp)
            _Theard.Start()
        End If
    End Sub

    Private Sub otmqafinalleader_Tick(sender As Object, e As EventArgs) Handles otmqafinalleader.Tick
        If (_StateCheckQAFinalLeader) And (HI.ST.SysInfo.StateUserQAFinalLeader = True) Then
            _StateCheckQAFinalLeader = False
            Dim _Theard As New Thread(AddressOf CheckQAFinalLeaderApp)
            _Theard.Start()
        End If
    End Sub

    Private Sub otmdctimer_Tick(sender As Object, e As EventArgs) Handles otmdctimer.Tick
        If (_StateUserDC) And (HI.ST.SysInfo.StateUserDCControl = True) Then
            _StateUserDC = False
            Dim _Theard As New Thread(AddressOf CheckUserDocumentControl)
            _Theard.Start()
        End If
    End Sub

    Private Sub otmcheckappr_Tick(sender As Object, e As EventArgs) Handles otmcheckappr.Tick
        If (_StateUserAppPR) And (HI.ST.SysInfo.StateUserAppPR = True) Then
            _StateUserAppPR = False
            Dim _Theard As New Thread(AddressOf CheckUserAppPRDataInfo)
            _Theard.Start()
        End If
    End Sub


    Private Sub otmChkOrderCostApp_Tick(sender As Object, e As EventArgs) Handles otmChkOrderCostApp.Tick
        If (_StateChekOrderCost) And (HI.ST.SysInfo.StateOrderCostApp = True) Then
            _StateChekOrderCost = False

            Dim _Theard As New Thread(AddressOf CheckOrderCost)
            _Theard.Start()

        End If
    End Sub

    Private Sub SetFont(FormObj As Object)
        For Each Obj As Object In FormObj.Controls

            Try

                Select Case HI.ENM.Control.GeTypeControl(Obj)
                    Case ENM.Control.ControlType.LabelControl
                        Dim FFont As System.Drawing.Font = Obj.Appearance.Font
                        Dim FFontSize As Double = FFont.Size
                        Obj.Appearance.Font = New System.Drawing.Font(HI.ST.SysInfo.SystemFontName, FFontSize)

                        FFont.Dispose()
                    Case ENM.Control.ControlType.GroupControl
                        Dim FFont As System.Drawing.Font = Obj.Appearance.Font
                        Dim FFontSize As Double = FFont.Size
                        Obj.Appearance.Font = New System.Drawing.Font(HI.ST.SysInfo.SystemFontName, FFontSize)

                        FFont = Obj.AppearanceCaption.Font
                        FFontSize = FFont.Size
                        Obj.AppearanceCaption.Font = New System.Drawing.Font(HI.ST.SysInfo.SystemFontName, FFontSize)
                        FFont.Dispose()
                End Select



                Call SetFontControl(Obj)

            Catch ex As Exception
            End Try

        Next
    End Sub
    Private Sub SetFontControl(Obj As Object)

        Try
            For Each Obj2 As Object In Obj.Controls

                Try

                    If Obj.Name = "onvbar" Then
                        Beep()
                    End If

                    Dim FFont As System.Drawing.Font = Obj2.Font
                    Dim FFontSize As Double = FFont.Size
                    Obj2.Font = New System.Drawing.Font(HI.ST.SysInfo.SystemFontName, FFontSize)

                    Try
                        FFont = Obj2.Appearance.Font
                        FFontSize = FFont.Size
                        Obj2.Appearance.Font = New System.Drawing.Font(HI.ST.SysInfo.SystemFontName, FFontSize)
                    Catch ex As Exception
                    End Try

                    FFont.Dispose()

                    Call SetFontControl(Obj2)
                Catch ex As Exception

                End Try
            Next
        Catch ex As Exception

        End Try

    End Sub

    Public Delegate Sub FNFont_SelectedIdxChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Private Sub FNFont_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNFont.SelectedIndexChanged
        If _StateFormLoadSucess Then
            Try
                'If Me.InvokeRequired Then
                '    Me.Invoke(New FNFont_SelectedIdxChanged(AddressOf FNFont_SelectedIndexChanged), New Object() {sender, e})

                'Else

                If Me.MdiChildren.Count > 0 Then
                    If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ เปลี่ยน Font ใช่หรือไม่ หากยืนยัน ระบบจะทำการปิดหน้าจอที่ท่านเปิดค้างไว้อยู่ กรุณาทำการตรวจสอบและทำการบันทึกข้อมูลให้เรียบร้อย", 1601524788) = False Then
                        Exit Sub
                    End If
                End If
                If HI.ST.SysInfo.FontInstalled(FNFont.Text) <> HI.ST.SysInfo.SystemFontName Then

                    Dim _Spls As New HI.TL.SplashScreen("Switch Language... Please Wait..")
                    HI.ST.SysInfo.SystemFontName = FNFont.Text


                    For Each oForm As Object In Me.MdiChildren

                        Try
                            CType(oForm, DevExpress.XtraEditors.XtraForm).Close()
                        Catch ex As Exception
                        End Try

                    Next

                    Dim FFont01 As System.Drawing.Font = DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont
                    Dim FFontSize01 As Double = FFont01.Size
                    DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = New System.Drawing.Font(HI.ST.SysInfo.SystemFontName, FFontSize01)

                    FFont01 = DevExpress.XtraEditors.WindowsFormsSettings.DefaultMenuFont
                    FFontSize01 = FFont01.Size
                    DevExpress.XtraEditors.WindowsFormsSettings.DefaultMenuFont = New System.Drawing.Font(HI.ST.SysInfo.SystemFontName, FFontSize01)

                    FFont01 = DevExpress.XtraEditors.WindowsFormsSettings.DefaultPrintFont
                    FFontSize01 = FFont01.Size
                    DevExpress.XtraEditors.WindowsFormsSettings.DefaultPrintFont = New System.Drawing.Font(HI.ST.SysInfo.SystemFontName, FFontSize01)
                    FFont01.Dispose()

                    'Try
                    '    Dim FFont0 As System.Drawing.Font = Me.Appearance.Font
                    '    Dim FFontSize0 As Double = FFont0.Size
                    '    Me.Appearance.Font = New System.Drawing.Font(HI.ST.SysInfo.SystemFontName, FFontSize0)
                    '    FFont0.Dispose()
                    'Catch ex As Exception

                    'End Try

                    'Call CreateSystemMainMenu(Me)
                    'Me.CreateToolBarFunction(Me._BarManager)

                    For Each objGrp As DevExpress.XtraNavBar.NavBarGroup In onvbar.Groups
                        Try
                            Dim FFont09 As System.Drawing.Font = objGrp.Appearance.Font
                            Dim FFontSize09 As Single = FFont09.Size
                            objGrp.Appearance.Font = New System.Drawing.Font(HI.ST.SysInfo.SystemFontName, FFontSize09, FontStyle.Bold)
                            FFont09.Dispose()

                        Catch ex As Exception

                        End Try

                        'Try
                        '    For Each Contrl In objGrp.ControlContainer.Controls

                        '        Select Case Contrl.GetType.FullName.ToString.ToUpper
                        '            Case "DevExpress.XtraTreeList.TreeList".ToUpper

                        '                Try

                        '                    With CType(Contrl, DevExpress.XtraTreeList.TreeList)

                        '                        Dim FFont02 As System.Drawing.Font = .Font
                        '                        Dim FFontSize02 As Double = FFont02.Size
                        '                        .Font = New System.Drawing.Font(HI.ST.SysInfo.SystemFontName, FFontSize02)
                        '                        FFont02.Dispose()

                        '                    End With

                        '                Catch ex As Exception
                        '                End Try

                        '        End Select

                        '    Next
                        'Catch ex As Exception

                        'End Try


                    Next
                    HI.UL.AppRegistry.WriteRegistry(UL.AppRegistry.KeyName.Font, HI.ST.SysInfo.SystemFontName)
                    _Spls.Close()

                End If

                ' End If

            Catch ex As Exception
            End Try

        End If
    End Sub

    Sub SetGridFont(view As GridView, font As Font)
        Dim ap As AppearanceObject
        For Each ap In view.Appearance
            ap.Font = font
        Next
    End Sub

    Private Sub otmChkEmpLeaveApp_Tick(sender As Object, e As EventArgs) Handles otmChkEmpLeaveApp.Tick
        If (_StateEmpleaveApp) And (HI.ST.SysInfo.StateEmpLeaveApp = True) Then
            _StateEmpleaveApp = False

            Dim _Theard As New Thread(AddressOf CheckEmpLeaveApp)
            _Theard.Start()

        End If
    End Sub

    Private Delegate Sub DelegateCheckEmpLeaveApp()
    Private Sub CheckEmpLeaveApp()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckEmpLeaveApp(AddressOf CheckEmpLeaveApp), New Object() {})
        Else
            HI.Service.ClsService.ValidateAppEmpleave(HI.ST.SysInfo.AppActScreen)
            _StateEmpleaveApp = True
        End If
    End Sub

    Private Sub otmqastyleriskcritical_Tick(sender As Object, e As EventArgs) Handles otmqastyleriskcritical.Tick
        otmqastyleriskcritical.Enabled = False

        Dim _Theard As New Thread(AddressOf CheckQAStyleRisk)
        _Theard.Start()

    End Sub


    Private Delegate Sub DelegateCheckQAStyleRisk()
    Private Sub CheckQAStyleRisk()
        If Me.InvokeRequired Then

            Me.Invoke(New DelegateCheckQAStyleRisk(AddressOf CheckQAStyleRisk), New Object() {})

        Else

            Dim cmdstring As String = ""
            cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_QACRITICAL "
            Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_PROD)

            If dt.Rows.Count > 0 Then
                dt.Dispose()

                Dim QARiskStyleCritical As New HI.Service.StyleRiskCritical
                QARiskStyleCritical.ShowDialog()

            Else
                dt.Dispose()
            End If

            otmqastyleriskcritical.Enabled = True
        End If
    End Sub

    Private Sub otmDirectorAssetPR_Tick(sender As Object, e As EventArgs) Handles otmDirectorAssetPR.Tick
        If (_StateCheckPRAppAssetDirector) And (HI.ST.SysInfo.StateDirectorAssetPR = True) Then
            _StateCheckPRAppAssetDirector = False
            Dim _Theard As New Thread(AddressOf CheckPRAppAssetDirector)
            _Theard.Start()
        End If

    End Sub

    Private Sub otmDirectorAssetPO_Tick(sender As Object, e As EventArgs) Handles otmDirectorAssetPO.Tick
        If (_StateCheckPOAppAssetDirector) And (HI.ST.SysInfo.StateDirectorAssetPO = True) Then
            _StateCheckPOAppAssetDirector = False
            Dim _Theard As New Thread(AddressOf CheckPOAppAssetDirector)
            _Theard.Start()
        End If

    End Sub

    Private Sub otmAssetPo_Tick(sender As Object, e As EventArgs) Handles otmAssetPo.Tick
        If (_StateCheckPOAppAsset) And (HI.ST.SysInfo.StateSuperVisorAssetPO = True) Then
            _StateCheckPOAppAsset = False

            Dim _Theard As New Thread(AddressOf CheckPOAppAsset)
            _Theard.Start()

        End If
    End Sub

    Private Sub otmAssetPR_Tick(sender As Object, e As EventArgs) Handles otmcheckAssetPR.Tick
        If (_StateCheckPRAppAsset) And (HI.ST.SysInfo.StateSuperVisorPRAsset = True) Then
            _StateCheckPRAppAsset = False

            Dim _Theard As New Thread(AddressOf CheckPRAppAsset)
            _Theard.Start()

        End If
    End Sub


    Private Delegate Sub DelegateCheckPRAppAsset()
    Private Sub CheckPRAppAsset()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckPRAppAsset(AddressOf CheckPRAppAsset), New Object() {})
        Else


            HI.Service.ClsService.ValidateAppAssetPR(HI.ST.SysInfo.AppActScreen)
            _StateCheckPRAppAsset = True
        End If
    End Sub

    Private Delegate Sub DelegateCheckPRAppAssetDirector()
    Private Sub CheckPRAppAssetDirector()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckPRAppAssetDirector(AddressOf CheckPRAppAssetDirector), New Object() {})
        Else

            HI.Service.ClsService.ValidateAppAssetPRDirector(HI.ST.SysInfo.AppActScreen)
            _StateCheckPOAppDirector = True
        End If
    End Sub


    Private Delegate Sub DelegateCheckPOAppAsset()
    Private Sub CheckPOAppAsset()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckPOAppAsset(AddressOf CheckPOAppAsset), New Object() {})
        Else

            HI.Service.ClsService.ValidateAppAssetPO(HI.ST.SysInfo.AppActScreen)
            _StateCheckPOAppAsset = True
        End If
    End Sub

    Private Delegate Sub DelegateCheckPOAppAssetDirector()
    Private Sub CheckPOAppAssetDirector()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckPOAppAssetDirector(AddressOf CheckPOAppAssetDirector), New Object() {})
        Else

            HI.Service.ClsService.ValidateAppAssetPODirector(HI.ST.SysInfo.AppActScreen)
            _StateCheckPOAppDirector = True
        End If
    End Sub

    Private Sub otmwisdomservice_Tick(sender As Object, e As EventArgs) Handles otmwisdomservice.Tick
        Try

            'Dim ProcessAppName As Process() = Process.GetProcessesByName("WisdomService" & HI.ST.UserInfo.UserName)
            'Dim ProcessAppName2 As Process() = Process.GetProcessesByName("WisdomService" & HI.ST.UserInfo.UserName & ".exe")
            'Dim ProcessAppName3 As Process() = Process.GetProcessesByName("WisdomService")
            'Dim ProcessAppName4 As Process() = Process.GetProcessesByName("WisdomService.exe")

            'If ProcessAppName.Length <= 0 And ProcessAppName2.Length <= 0 And ProcessAppName3.Length <= 0 And ProcessAppName4.Length <= 0 Then

            '    Dim p = New System.Diagnostics.Process()
            '    p.StartInfo.FileName = Application.StartupPath & "\WisdomService.exe"
            '    p.StartInfo.Arguments = HI.ST.UserInfo.UserName & " " & Integer.Parse(Val(HI.ST.Lang.Language)).ToString
            '    p.Start()

            'End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub opmmail_EditValueChanged(sender As Object, e As EventArgs) Handles opmmail.EditValueChanged

    End Sub

    Private Sub olbcmp_ItemClick(sender As Object, e As ItemClickEventArgs) Handles olbcmp.ItemClick

        Try
            Dim Scmp As New HI.ST.TitleSelectCompany


            With Scmp
                .SelecttileItem = Nothing
                .ShowDialog()


                If Not (.SelecttileItem Is Nothing) Then
                    If TypeOf .SelecttileItem.Tag Is HI.ST.Tag.TagCmpData Then
                        Try
                            Dim mtag As HI.ST.Tag.TagCmpData = CType(.SelecttileItem.Tag, HI.ST.Tag.TagCmpData)

                            If (HI.ST.SysInfo.CmpID <> mtag.CmpID) Then
                                HI.ST.SysInfo.CmpID = mtag.CmpID
                                HI.ST.SysInfo.CmpRunID = mtag.DocRun
                                HI.ST.SysInfo.CmpCode = mtag.CmpCode
                                HI.ST.UserInfo.UserCompany = mtag.CmpCode

                                HI.UL.AppRegistry.WriteRegistry(UL.AppRegistry.KeyName.Cmp, mtag.CmpCode)
                                If HI.ST.Lang.Language = Lang.eLang.TH Then
                                    olbcmp.Caption = mtag.LangTH
                                Else
                                    olbcmp.Caption = mtag.LangEN
                                End If


                                Try
                                    For Each obj As Form In Me.MdiChildren
                                        obj.Close()
                                    Next
                                Catch ex As Exception
                                End Try

                            End If
                        Catch ex As Exception

                        End Try
                    End If
                End If
            End With



        Catch ex As Exception

        End Try
    End Sub
End Class