<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wMainSecurity
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wMainSecurity))
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmeditpermission = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdeletepermission = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmaddnewpermission = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmedituser = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmrefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdeleteuser = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmaddnewuser = New DevExpress.XtraEditors.SimpleButton()
        Me.otbsecurity = New DevExpress.XtraTab.XtraTabControl()
        Me.otpuser = New DevExpress.XtraTab.XtraTabPage()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me._LstDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTImg = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPictureEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.ColKey = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTNameTH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTNameEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUserAD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.otppermission = New DevExpress.XtraTab.XtraTabPage()
        Me.ogcpermissiondetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvpermissiondetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPictureEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.otpmerteamcuspermission = New DevExpress.XtraTab.XtraTabPage()
        Me.ogcmerteam = New DevExpress.XtraGrid.GridControl()
        Me.ogvmerteam = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemPictureEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit()
        Me.ocmcopy = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.otbsecurity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otbsecurity.SuspendLayout()
        Me.otpuser.SuspendLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me._LstDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otppermission.SuspendLayout()
        CType(Me.ogcpermissiondetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvpermissiondetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpmerteamcuspermission.SuspendLayout()
        CType(Me.ogcmerteam, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvmerteam, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemPictureEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmcopy)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmeditpermission)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdeletepermission)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmaddnewpermission)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmedituser)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmrefresh)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdeleteuser)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmaddnewuser)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(57, 137)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(873, 91)
        Me.ogbmainprocbutton.TabIndex = 0
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmeditpermission
        '
        Me.ocmeditpermission.Location = New System.Drawing.Point(308, 42)
        Me.ocmeditpermission.Name = "ocmeditpermission"
        Me.ocmeditpermission.Size = New System.Drawing.Size(95, 25)
        Me.ocmeditpermission.TabIndex = 100
        Me.ocmeditpermission.TabStop = False
        Me.ocmeditpermission.Tag = "2|"
        Me.ocmeditpermission.Text = "edit role"
        Me.ocmeditpermission.Visible = False
        '
        'ocmdeletepermission
        '
        Me.ocmdeletepermission.Location = New System.Drawing.Point(207, 42)
        Me.ocmdeletepermission.Name = "ocmdeletepermission"
        Me.ocmdeletepermission.Size = New System.Drawing.Size(95, 25)
        Me.ocmdeletepermission.TabIndex = 99
        Me.ocmdeletepermission.TabStop = False
        Me.ocmdeletepermission.Tag = "2|"
        Me.ocmdeletepermission.Text = "delete role"
        Me.ocmdeletepermission.Visible = False
        '
        'ocmaddnewpermission
        '
        Me.ocmaddnewpermission.Location = New System.Drawing.Point(106, 42)
        Me.ocmaddnewpermission.Name = "ocmaddnewpermission"
        Me.ocmaddnewpermission.Size = New System.Drawing.Size(95, 25)
        Me.ocmaddnewpermission.TabIndex = 98
        Me.ocmaddnewpermission.TabStop = False
        Me.ocmaddnewpermission.Tag = "2|"
        Me.ocmaddnewpermission.Text = "add new role"
        Me.ocmaddnewpermission.Visible = False
        '
        'ocmedituser
        '
        Me.ocmedituser.Location = New System.Drawing.Point(308, 11)
        Me.ocmedituser.Name = "ocmedituser"
        Me.ocmedituser.Size = New System.Drawing.Size(95, 25)
        Me.ocmedituser.TabIndex = 97
        Me.ocmedituser.TabStop = False
        Me.ocmedituser.Tag = "2|"
        Me.ocmedituser.Text = "edit user"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(760, 11)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmrefresh
        '
        Me.ocmrefresh.Location = New System.Drawing.Point(5, 5)
        Me.ocmrefresh.Name = "ocmrefresh"
        Me.ocmrefresh.Size = New System.Drawing.Size(95, 25)
        Me.ocmrefresh.TabIndex = 95
        Me.ocmrefresh.TabStop = False
        Me.ocmrefresh.Tag = "2|"
        Me.ocmrefresh.Text = "CLEAR"
        Me.ocmrefresh.Visible = False
        '
        'ocmdeleteuser
        '
        Me.ocmdeleteuser.Location = New System.Drawing.Point(207, 11)
        Me.ocmdeleteuser.Name = "ocmdeleteuser"
        Me.ocmdeleteuser.Size = New System.Drawing.Size(95, 25)
        Me.ocmdeleteuser.TabIndex = 94
        Me.ocmdeleteuser.TabStop = False
        Me.ocmdeleteuser.Tag = "2|"
        Me.ocmdeleteuser.Text = "delete user"
        '
        'ocmaddnewuser
        '
        Me.ocmaddnewuser.Location = New System.Drawing.Point(106, 11)
        Me.ocmaddnewuser.Name = "ocmaddnewuser"
        Me.ocmaddnewuser.Size = New System.Drawing.Size(95, 25)
        Me.ocmaddnewuser.TabIndex = 93
        Me.ocmaddnewuser.TabStop = False
        Me.ocmaddnewuser.Tag = "2|"
        Me.ocmaddnewuser.Text = "add new user"
        '
        'otbsecurity
        '
        Me.otbsecurity.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otbsecurity.Location = New System.Drawing.Point(0, 0)
        Me.otbsecurity.Name = "otbsecurity"
        Me.otbsecurity.SelectedTabPage = Me.otpuser
        Me.otbsecurity.Size = New System.Drawing.Size(1050, 662)
        Me.otbsecurity.TabIndex = 0
        Me.otbsecurity.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpuser, Me.otppermission, Me.otpmerteamcuspermission})
        '
        'otpuser
        '
        Me.otpuser.Controls.Add(Me.ogcdetail)
        Me.otpuser.Name = "otpuser"
        Me.otpuser.Size = New System.Drawing.Size(1044, 634)
        Me.otpuser.Text = "User"
        '
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogcdetail.MainView = Me._LstDetail
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPictureEdit1})
        Me.ogcdetail.Size = New System.Drawing.Size(1044, 634)
        Me.ogcdetail.TabIndex = 3
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me._LstDetail})
        '
        '_LstDetail
        '
        Me._LstDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTImg, Me.ColKey, Me.FTCode, Me.FTNameTH, Me.FTNameEN, Me.FTUserAD})
        Me._LstDetail.GridControl = Me.ogcdetail
        Me._LstDetail.Name = "_LstDetail"
        Me._LstDetail.OptionsCustomization.AllowGroup = False
        Me._LstDetail.OptionsCustomization.AllowQuickHideColumns = False
        Me._LstDetail.OptionsView.ColumnAutoWidth = False
        Me._LstDetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me._LstDetail.OptionsView.ShowGroupPanel = False
        Me._LstDetail.Tag = "2|"
        '
        'FTImg
        '
        Me.FTImg.Caption = " "
        Me.FTImg.ColumnEdit = Me.RepositoryItemPictureEdit1
        Me.FTImg.FieldName = "FTImg"
        Me.FTImg.Name = "FTImg"
        Me.FTImg.OptionsColumn.AllowEdit = False
        Me.FTImg.OptionsColumn.FixedWidth = True
        Me.FTImg.OptionsColumn.ReadOnly = True
        Me.FTImg.Visible = True
        Me.FTImg.VisibleIndex = 0
        Me.FTImg.Width = 36
        '
        'RepositoryItemPictureEdit1
        '
        Me.RepositoryItemPictureEdit1.ErrorImage = CType(resources.GetObject("RepositoryItemPictureEdit1.ErrorImage"), System.Drawing.Image)
        Me.RepositoryItemPictureEdit1.Name = "RepositoryItemPictureEdit1"
        Me.RepositoryItemPictureEdit1.ReadOnly = True
        Me.RepositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.RepositoryItemPictureEdit1.ZoomAccelerationFactor = 1.0R
        '
        'ColKey
        '
        Me.ColKey.Caption = "FTMnuName"
        Me.ColKey.FieldName = "ColKey"
        Me.ColKey.Name = "ColKey"
        Me.ColKey.OptionsColumn.AllowEdit = False
        Me.ColKey.OptionsColumn.ReadOnly = True
        '
        'FTCode
        '
        Me.FTCode.Caption = "Code"
        Me.FTCode.FieldName = "FTCode"
        Me.FTCode.Name = "FTCode"
        Me.FTCode.OptionsColumn.AllowEdit = False
        Me.FTCode.OptionsColumn.AllowShowHide = False
        Me.FTCode.OptionsColumn.ReadOnly = True
        Me.FTCode.OptionsColumn.ShowInCustomizationForm = False
        Me.FTCode.Visible = True
        Me.FTCode.VisibleIndex = 1
        Me.FTCode.Width = 146
        '
        'FTNameTH
        '
        Me.FTNameTH.Caption = "Name TH"
        Me.FTNameTH.FieldName = "FTNameTH"
        Me.FTNameTH.Name = "FTNameTH"
        Me.FTNameTH.OptionsColumn.AllowEdit = False
        Me.FTNameTH.OptionsColumn.AllowShowHide = False
        Me.FTNameTH.OptionsColumn.ReadOnly = True
        Me.FTNameTH.OptionsColumn.ShowInCustomizationForm = False
        Me.FTNameTH.Visible = True
        Me.FTNameTH.VisibleIndex = 2
        Me.FTNameTH.Width = 250
        '
        'FTNameEN
        '
        Me.FTNameEN.Caption = "FTNameEN"
        Me.FTNameEN.FieldName = "FTNameEN"
        Me.FTNameEN.Name = "FTNameEN"
        Me.FTNameEN.OptionsColumn.AllowEdit = False
        Me.FTNameEN.OptionsColumn.AllowShowHide = False
        Me.FTNameEN.OptionsColumn.ReadOnly = True
        Me.FTNameEN.OptionsColumn.ShowInCustomizationForm = False
        Me.FTNameEN.Visible = True
        Me.FTNameEN.VisibleIndex = 3
        Me.FTNameEN.Width = 250
        '
        'FTUserAD
        '
        Me.FTUserAD.Caption = "AD User"
        Me.FTUserAD.FieldName = "FTUserAD"
        Me.FTUserAD.Name = "FTUserAD"
        Me.FTUserAD.OptionsColumn.AllowEdit = False
        Me.FTUserAD.OptionsColumn.ReadOnly = True
        Me.FTUserAD.Visible = True
        Me.FTUserAD.VisibleIndex = 4
        Me.FTUserAD.Width = 150
        '
        'otppermission
        '
        Me.otppermission.Controls.Add(Me.ogcpermissiondetail)
        Me.otppermission.Name = "otppermission"
        Me.otppermission.Size = New System.Drawing.Size(1044, 634)
        Me.otppermission.Text = "Permission"
        '
        'ogcpermissiondetail
        '
        Me.ogcpermissiondetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcpermissiondetail.Location = New System.Drawing.Point(0, 0)
        Me.ogcpermissiondetail.MainView = Me.ogvpermissiondetail
        Me.ogcpermissiondetail.Name = "ogcpermissiondetail"
        Me.ogcpermissiondetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPictureEdit2})
        Me.ogcpermissiondetail.Size = New System.Drawing.Size(1044, 634)
        Me.ogcpermissiondetail.TabIndex = 4
        Me.ogcpermissiondetail.TabStop = False
        Me.ogcpermissiondetail.Tag = "2|"
        Me.ogcpermissiondetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvpermissiondetail})
        '
        'ogvpermissiondetail
        '
        Me.ogvpermissiondetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5})
        Me.ogvpermissiondetail.GridControl = Me.ogcpermissiondetail
        Me.ogvpermissiondetail.Name = "ogvpermissiondetail"
        Me.ogvpermissiondetail.OptionsCustomization.AllowGroup = False
        Me.ogvpermissiondetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvpermissiondetail.OptionsView.ColumnAutoWidth = False
        Me.ogvpermissiondetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvpermissiondetail.OptionsView.ShowGroupPanel = False
        Me.ogvpermissiondetail.Tag = "2|"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = " "
        Me.GridColumn1.ColumnEdit = Me.RepositoryItemPictureEdit2
        Me.GridColumn1.FieldName = "FTImg"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.FixedWidth = True
        Me.GridColumn1.OptionsColumn.ReadOnly = True
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 36
        '
        'RepositoryItemPictureEdit2
        '
        Me.RepositoryItemPictureEdit2.ErrorImage = CType(resources.GetObject("RepositoryItemPictureEdit2.ErrorImage"), System.Drawing.Image)
        Me.RepositoryItemPictureEdit2.Name = "RepositoryItemPictureEdit2"
        Me.RepositoryItemPictureEdit2.ReadOnly = True
        Me.RepositoryItemPictureEdit2.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.RepositoryItemPictureEdit2.ZoomAccelerationFactor = 1.0R
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "FTMnuName"
        Me.GridColumn2.FieldName = "ColKey"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.OptionsColumn.ReadOnly = True
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Code"
        Me.GridColumn3.FieldName = "FTCode"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.OptionsColumn.AllowShowHide = False
        Me.GridColumn3.OptionsColumn.ReadOnly = True
        Me.GridColumn3.OptionsColumn.ShowInCustomizationForm = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 1
        Me.GridColumn3.Width = 146
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Name TH"
        Me.GridColumn4.FieldName = "FTNameTH"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.OptionsColumn.AllowEdit = False
        Me.GridColumn4.OptionsColumn.AllowShowHide = False
        Me.GridColumn4.OptionsColumn.ReadOnly = True
        Me.GridColumn4.OptionsColumn.ShowInCustomizationForm = False
        Me.GridColumn4.Visible = True
        Me.GridColumn4.VisibleIndex = 2
        Me.GridColumn4.Width = 329
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "FTNameEN"
        Me.GridColumn5.FieldName = "FTNameEN"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.OptionsColumn.AllowEdit = False
        Me.GridColumn5.OptionsColumn.AllowShowHide = False
        Me.GridColumn5.OptionsColumn.ReadOnly = True
        Me.GridColumn5.OptionsColumn.ShowInCustomizationForm = False
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 3
        Me.GridColumn5.Width = 566
        '
        'otpmerteamcuspermission
        '
        Me.otpmerteamcuspermission.Controls.Add(Me.ogcmerteam)
        Me.otpmerteamcuspermission.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otpmerteamcuspermission.Name = "otpmerteamcuspermission"
        Me.otpmerteamcuspermission.Size = New System.Drawing.Size(1044, 634)
        Me.otpmerteamcuspermission.Text = "Merteam Permission Access Customer"
        '
        'ogcmerteam
        '
        Me.ogcmerteam.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcmerteam.Location = New System.Drawing.Point(0, 0)
        Me.ogcmerteam.MainView = Me.ogvmerteam
        Me.ogcmerteam.Name = "ogcmerteam"
        Me.ogcmerteam.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemPictureEdit3})
        Me.ogcmerteam.Size = New System.Drawing.Size(1044, 634)
        Me.ogcmerteam.TabIndex = 4
        Me.ogcmerteam.TabStop = False
        Me.ogcmerteam.Tag = "2|"
        Me.ogcmerteam.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvmerteam})
        '
        'ogvmerteam
        '
        Me.ogvmerteam.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn8, Me.GridColumn9, Me.GridColumn10})
        Me.ogvmerteam.GridControl = Me.ogcmerteam
        Me.ogvmerteam.Name = "ogvmerteam"
        Me.ogvmerteam.OptionsCustomization.AllowGroup = False
        Me.ogvmerteam.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvmerteam.OptionsView.ColumnAutoWidth = False
        Me.ogvmerteam.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvmerteam.OptionsView.ShowGroupPanel = False
        Me.ogvmerteam.Tag = "2|"
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "FTMnuName"
        Me.GridColumn7.FieldName = "ColKey"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.OptionsColumn.AllowEdit = False
        Me.GridColumn7.OptionsColumn.ReadOnly = True
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Code"
        Me.GridColumn8.FieldName = "FTCode"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.OptionsColumn.AllowEdit = False
        Me.GridColumn8.OptionsColumn.AllowShowHide = False
        Me.GridColumn8.OptionsColumn.ReadOnly = True
        Me.GridColumn8.OptionsColumn.ShowInCustomizationForm = False
        Me.GridColumn8.Visible = True
        Me.GridColumn8.VisibleIndex = 0
        Me.GridColumn8.Width = 146
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Name TH"
        Me.GridColumn9.FieldName = "FTNameTH"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.OptionsColumn.AllowEdit = False
        Me.GridColumn9.OptionsColumn.AllowShowHide = False
        Me.GridColumn9.OptionsColumn.ReadOnly = True
        Me.GridColumn9.OptionsColumn.ShowInCustomizationForm = False
        Me.GridColumn9.Visible = True
        Me.GridColumn9.VisibleIndex = 1
        Me.GridColumn9.Width = 329
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "FTNameEN"
        Me.GridColumn10.FieldName = "FTNameEN"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.OptionsColumn.AllowEdit = False
        Me.GridColumn10.OptionsColumn.AllowShowHide = False
        Me.GridColumn10.OptionsColumn.ReadOnly = True
        Me.GridColumn10.OptionsColumn.ShowInCustomizationForm = False
        Me.GridColumn10.Visible = True
        Me.GridColumn10.VisibleIndex = 2
        Me.GridColumn10.Width = 566
        '
        'RepositoryItemPictureEdit3
        '
        Me.RepositoryItemPictureEdit3.ErrorImage = CType(resources.GetObject("RepositoryItemPictureEdit3.ErrorImage"), System.Drawing.Image)
        Me.RepositoryItemPictureEdit3.Name = "RepositoryItemPictureEdit3"
        Me.RepositoryItemPictureEdit3.ReadOnly = True
        Me.RepositoryItemPictureEdit3.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.RepositoryItemPictureEdit3.ZoomAccelerationFactor = 1.0R
        '
        'ocmcopy
        '
        Me.ocmcopy.Location = New System.Drawing.Point(389, 33)
        Me.ocmcopy.Name = "ocmcopy"
        Me.ocmcopy.Size = New System.Drawing.Size(95, 25)
        Me.ocmcopy.TabIndex = 102
        Me.ocmcopy.TabStop = False
        Me.ocmcopy.Tag = "2|"
        Me.ocmcopy.Text = "copy role"
        Me.ocmcopy.Visible = False
        '
        'wMainSecurity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1050, 662)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otbsecurity)
        Me.Name = "wMainSecurity"
        Me.Text = "Main Security"
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.otbsecurity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otbsecurity.ResumeLayout(False)
        Me.otpuser.ResumeLayout(False)
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me._LstDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otppermission.ResumeLayout(False)
        CType(Me.ogcpermissiondetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvpermissiondetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpmerteamcuspermission.ResumeLayout(False)
        CType(Me.ogcmerteam, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvmerteam, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemPictureEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmeditpermission As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdeletepermission As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmaddnewpermission As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmedituser As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmrefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdeleteuser As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmaddnewuser As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otbsecurity As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpuser As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otppermission As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents _LstDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTImg As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPictureEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents ColKey As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTNameTH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTNameEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogcpermissiondetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvpermissiondetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemPictureEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents otpmerteamcuspermission As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogcmerteam As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvmerteam As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemPictureEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUserAD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmcopy As DevExpress.XtraEditors.SimpleButton
End Class
