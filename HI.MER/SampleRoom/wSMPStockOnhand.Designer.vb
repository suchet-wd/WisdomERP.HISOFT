<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wSMPStockOnhand
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wSMPStockOnhand))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTSMPOrderNoTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTSMPOrderNoTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTSMPOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysWHId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTSMPOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysWHId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysWHId_None = New DevExpress.XtraEditors.TextEdit()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogdtime = New DevExpress.XtraGrid.GridControl()
        Me.ogvtime = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTWHCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTSMPOrderNoTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSMPOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbheader
        '
        Me.ogbheader.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.ogbheader.Appearance.Options.UseForeColor = True
        Me.ogbheader.Appearance.Options.UseTextOptions = True
        Me.ogbheader.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogbheader.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ogbheader.Controls.Add(Me.DockPanel1_Container)
        Me.ogbheader.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ogbheader.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.DockVertical = DevExpress.Utils.DefaultBoolean.[False]
        Me.ogbheader.ID = New System.Guid("77b9346d-8d15-4323-af1e-af82afa9902a")
        Me.ogbheader.Image = CType(resources.GetObject("ogbheader.Image"), System.Drawing.Image)
        Me.ogbheader.Location = New System.Drawing.Point(0, 0)
        Me.ogbheader.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.FloatOnDblClick = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.Options.ShowMaximizeButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(1326, 104)
        Me.ogbheader.Size = New System.Drawing.Size(1326, 104)
        Me.ogbheader.TabStop = False
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTSMPOrderNoTo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTSMPOrderNoTo)
        Me.DockPanel1_Container.Controls.Add(Me.FTSMPOrderNo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysWHId)
        Me.DockPanel1_Container.Controls.Add(Me.FTSMPOrderNo)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysWHId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysWHId_None)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 29)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1320, 70)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTSMPOrderNoTo_lbl
        '
        Me.FTSMPOrderNoTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTSMPOrderNoTo_lbl.Appearance.Options.UseForeColor = True
        Me.FTSMPOrderNoTo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTSMPOrderNoTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSMPOrderNoTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSMPOrderNoTo_lbl.Location = New System.Drawing.Point(385, 32)
        Me.FTSMPOrderNoTo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSMPOrderNoTo_lbl.Name = "FTSMPOrderNoTo_lbl"
        Me.FTSMPOrderNoTo_lbl.Size = New System.Drawing.Size(147, 23)
        Me.FTSMPOrderNoTo_lbl.TabIndex = 287
        Me.FTSMPOrderNoTo_lbl.Tag = "2|"
        Me.FTSMPOrderNoTo_lbl.Text = "To Order No :"
        '
        'FTSMPOrderNoTo
        '
        Me.FTSMPOrderNoTo.EnterMoveNextControl = True
        Me.FTSMPOrderNoTo.Location = New System.Drawing.Point(535, 32)
        Me.FTSMPOrderNoTo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSMPOrderNoTo.Name = "FTSMPOrderNoTo"
        Me.FTSMPOrderNoTo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTSMPOrderNoTo.Properties.Appearance.Options.UseBackColor = True
        Me.FTSMPOrderNoTo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTSMPOrderNoTo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTSMPOrderNoTo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTSMPOrderNoTo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTSMPOrderNoTo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTSMPOrderNoTo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTSMPOrderNoTo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTSMPOrderNoTo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTSMPOrderNoTo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTSMPOrderNoTo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTSMPOrderNoTo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTSMPOrderNoTo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTSMPOrderNoTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "577", Nothing, True)})
        Me.FTSMPOrderNoTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTSMPOrderNoTo.Properties.MaxLength = 30
        Me.FTSMPOrderNoTo.Size = New System.Drawing.Size(152, 23)
        Me.FTSMPOrderNoTo.TabIndex = 286
        Me.FTSMPOrderNoTo.Tag = "2|"
        '
        'FTSMPOrderNo_lbl
        '
        Me.FTSMPOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTSMPOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTSMPOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTSMPOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSMPOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSMPOrderNo_lbl.Location = New System.Drawing.Point(11, 29)
        Me.FTSMPOrderNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSMPOrderNo_lbl.Name = "FTSMPOrderNo_lbl"
        Me.FTSMPOrderNo_lbl.Size = New System.Drawing.Size(219, 23)
        Me.FTSMPOrderNo_lbl.TabIndex = 285
        Me.FTSMPOrderNo_lbl.Tag = "2|"
        Me.FTSMPOrderNo_lbl.Text = "From Order No :"
        '
        'FNHSysWHId
        '
        Me.FNHSysWHId.EnterMoveNextControl = True
        Me.FNHSysWHId.Location = New System.Drawing.Point(232, 4)
        Me.FNHSysWHId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHId.Name = "FNHSysWHId"
        Me.FNHSysWHId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysWHId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysWHId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysWHId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysWHId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysWHId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysWHId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysWHId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysWHId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysWHId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "575", Nothing, True)})
        Me.FNHSysWHId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysWHId.Properties.MaxLength = 30
        Me.FNHSysWHId.Size = New System.Drawing.Size(148, 23)
        Me.FNHSysWHId.TabIndex = 283
        Me.FNHSysWHId.Tag = "2|"
        '
        'FTSMPOrderNo
        '
        Me.FTSMPOrderNo.EnterMoveNextControl = True
        Me.FTSMPOrderNo.Location = New System.Drawing.Point(231, 33)
        Me.FTSMPOrderNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSMPOrderNo.Name = "FTSMPOrderNo"
        Me.FTSMPOrderNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTSMPOrderNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTSMPOrderNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTSMPOrderNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTSMPOrderNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTSMPOrderNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTSMPOrderNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTSMPOrderNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTSMPOrderNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTSMPOrderNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTSMPOrderNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTSMPOrderNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTSMPOrderNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTSMPOrderNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTSMPOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "568", Nothing, True)})
        Me.FTSMPOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTSMPOrderNo.Properties.MaxLength = 30
        Me.FTSMPOrderNo.Size = New System.Drawing.Size(148, 23)
        Me.FTSMPOrderNo.TabIndex = 284
        Me.FTSMPOrderNo.Tag = "2|"
        '
        'FNHSysWHId_lbl
        '
        Me.FNHSysWHId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysWHId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysWHId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysWHId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysWHId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysWHId_lbl.Location = New System.Drawing.Point(34, 4)
        Me.FNHSysWHId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHId_lbl.Name = "FNHSysWHId_lbl"
        Me.FNHSysWHId_lbl.Size = New System.Drawing.Size(197, 23)
        Me.FNHSysWHId_lbl.TabIndex = 284
        Me.FNHSysWHId_lbl.Tag = "2|"
        Me.FNHSysWHId_lbl.Text = "From Warehouse No :"
        '
        'FNHSysWHId_None
        '
        Me.FNHSysWHId_None.EnterMoveNextControl = True
        Me.FNHSysWHId_None.Location = New System.Drawing.Point(384, 4)
        Me.FNHSysWHId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHId_None.Name = "FNHSysWHId_None"
        Me.FNHSysWHId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysWHId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysWHId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysWHId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysWHId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysWHId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysWHId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysWHId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysWHId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysWHId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysWHId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysWHId_None.Properties.ReadOnly = True
        Me.FNHSysWHId_None.Size = New System.Drawing.Size(303, 23)
        Me.FNHSysWHId_None.TabIndex = 285
        Me.FNHSysWHId_None.TabStop = False
        Me.FNHSysWHId_None.Tag = "2|"
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.ogdtime)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 104)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.ShowCaption = False
        Me.ogbdetail.Size = New System.Drawing.Size(1326, 675)
        Me.ogbdetail.TabIndex = 0
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(106, 135)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1091, 58)
        Me.ogbmainprocbutton.TabIndex = 386
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(959, 14)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(16, 12)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(111, 31)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(134, 15)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(136, 28)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'ogdtime
        '
        Me.ogdtime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdtime.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdtime.Location = New System.Drawing.Point(2, 2)
        Me.ogdtime.MainView = Me.ogvtime
        Me.ogdtime.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdtime.Name = "ogdtime"
        Me.ogdtime.Size = New System.Drawing.Size(1322, 671)
        Me.ogdtime.TabIndex = 0
        Me.ogdtime.TabStop = False
        Me.ogdtime.Tag = "2|"
        Me.ogdtime.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvtime})
        '
        'ogvtime
        '
        Me.ogvtime.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTWHCode, Me.CFTOrderNo, Me.FTRawMatCode, Me.FTRawMatName, Me.FTRawMatColorCode, Me.FTRawMatSizeCode, Me.FTUnitCode, Me.FNQuantity})
        Me.ogvtime.GridControl = Me.ogdtime
        Me.ogvtime.Name = "ogvtime"
        Me.ogvtime.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvtime.OptionsView.ColumnAutoWidth = False
        Me.ogvtime.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvtime.OptionsView.ShowGroupPanel = False
        Me.ogvtime.Tag = "2|"
        '
        'FTWHCode
        '
        Me.FTWHCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTWHCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTWHCode.Caption = "FTWHCode"
        Me.FTWHCode.FieldName = "FTWHCode"
        Me.FTWHCode.Name = "FTWHCode"
        Me.FTWHCode.OptionsColumn.AllowEdit = False
        Me.FTWHCode.OptionsColumn.ReadOnly = True
        Me.FTWHCode.Visible = True
        Me.FTWHCode.VisibleIndex = 0
        Me.FTWHCode.Width = 79
        '
        'CFTOrderNo
        '
        Me.CFTOrderNo.AppearanceHeader.Options.UseTextOptions = True
        Me.CFTOrderNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CFTOrderNo.Caption = "FTSMPOrderNo"
        Me.CFTOrderNo.FieldName = "FTSMPOrderNo"
        Me.CFTOrderNo.Name = "CFTOrderNo"
        Me.CFTOrderNo.OptionsColumn.AllowEdit = False
        Me.CFTOrderNo.OptionsColumn.ReadOnly = True
        Me.CFTOrderNo.Visible = True
        Me.CFTOrderNo.VisibleIndex = 1
        Me.CFTOrderNo.Width = 120
        '
        'FTRawMatCode
        '
        Me.FTRawMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatCode.Caption = "FTMatCode"
        Me.FTRawMatCode.FieldName = "FTMatCode"
        Me.FTRawMatCode.Name = "FTRawMatCode"
        Me.FTRawMatCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatCode.Visible = True
        Me.FTRawMatCode.VisibleIndex = 2
        Me.FTRawMatCode.Width = 151
        '
        'FTRawMatName
        '
        Me.FTRawMatName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatName.Caption = "FTDescription"
        Me.FTRawMatName.FieldName = "FTDescription"
        Me.FTRawMatName.Name = "FTRawMatName"
        Me.FTRawMatName.OptionsColumn.AllowEdit = False
        Me.FTRawMatName.OptionsColumn.ReadOnly = True
        Me.FTRawMatName.Visible = True
        Me.FTRawMatName.VisibleIndex = 3
        Me.FTRawMatName.Width = 300
        '
        'FTRawMatColorCode
        '
        Me.FTRawMatColorCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatColorCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatColorCode.Caption = "FTMatColorCode"
        Me.FTRawMatColorCode.FieldName = "FTMatColorCode"
        Me.FTRawMatColorCode.Name = "FTRawMatColorCode"
        Me.FTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatColorCode.Visible = True
        Me.FTRawMatColorCode.VisibleIndex = 4
        Me.FTRawMatColorCode.Width = 83
        '
        'FTRawMatSizeCode
        '
        Me.FTRawMatSizeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatSizeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatSizeCode.Caption = "FTMatSizeCode"
        Me.FTRawMatSizeCode.FieldName = "FTMatSizeCode"
        Me.FTRawMatSizeCode.Name = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatSizeCode.Visible = True
        Me.FTRawMatSizeCode.VisibleIndex = 5
        Me.FTRawMatSizeCode.Width = 80
        '
        'FTUnitCode
        '
        Me.FTUnitCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUnitCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitCode.Caption = "UnitCode"
        Me.FTUnitCode.FieldName = "FTUnitCode"
        Me.FTUnitCode.Name = "FTUnitCode"
        Me.FTUnitCode.OptionsColumn.AllowEdit = False
        Me.FTUnitCode.OptionsColumn.ReadOnly = True
        Me.FTUnitCode.Visible = True
        Me.FTUnitCode.VisibleIndex = 6
        Me.FTUnitCode.Width = 80
        '
        'FNQuantity
        '
        Me.FNQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNQuantity.Caption = "Quantity"
        Me.FNQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNBalQuantity"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.OptionsColumn.ReadOnly = True
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 7
        Me.FNQuantity.Width = 120
        '
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.ogbheader})
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'wSMPStockOnhand
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1326, 779)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wSMPStockOnhand"
        Me.Text = "Stock Onhand"
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTSMPOrderNoTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSMPOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogdtime As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvtime As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents oDockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents CFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSMPOrderNoTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSMPOrderNoTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTSMPOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSMPOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTWHCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysWHId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysWHId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysWHId_None As DevExpress.XtraEditors.TextEdit
End Class
