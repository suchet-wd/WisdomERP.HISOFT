<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wMEDStockCard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wMEDStockCard))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTEDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTSDate = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysDrugIdTo_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysDrugId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTEDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTSDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysDrugIdTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysDrugId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysDrugIdTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysDrugId = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogdtime = New DevExpress.XtraGrid.GridControl()
        Me.ogvtime = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTDrugCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDrugName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDrugUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFNHSysDrugId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDMEDRcvDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTMEDRcvNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDocumentRefNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantityBal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTMEDIssNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantityIss = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager()
        Me.hideContainerTop = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTEDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysDrugIdTo_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysDrugId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysDrugIdTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysDrugId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerTop.SuspendLayout()
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
        Me.ogbheader.Location = New System.Drawing.Point(0, 34)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.FloatOnDblClick = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.Options.ShowMaximizeButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(1137, 112)
        Me.ogbheader.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.SavedIndex = 0
        Me.ogbheader.Size = New System.Drawing.Size(1137, 112)
        Me.ogbheader.TabStop = False
        Me.ogbheader.Text = "Criteria"
        Me.ogbheader.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTEDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTSDate)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysDrugIdTo_None)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysDrugId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTEDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTSDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysDrugIdTo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysDrugId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysDrugIdTo)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysDrugId)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(2, 25)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1133, 85)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTEDate
        '
        Me.FTEDate.EditValue = Nothing
        Me.FTEDate.Location = New System.Drawing.Point(353, 53)
        Me.FTEDate.Name = "FTEDate"
        Me.FTEDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEDate.Size = New System.Drawing.Size(132, 20)
        Me.FTEDate.TabIndex = 390
        '
        'FTSDate
        '
        Me.FTSDate.EditValue = Nothing
        Me.FTSDate.Location = New System.Drawing.Point(113, 52)
        Me.FTSDate.Name = "FTSDate"
        Me.FTSDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTSDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTSDate.Size = New System.Drawing.Size(132, 20)
        Me.FTSDate.TabIndex = 390
        '
        'FNHSysDrugIdTo_None
        '
        Me.FNHSysDrugIdTo_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysDrugIdTo_None.EnterMoveNextControl = True
        Me.FNHSysDrugIdTo_None.Location = New System.Drawing.Point(246, 28)
        Me.FNHSysDrugIdTo_None.Name = "FNHSysDrugIdTo_None"
        Me.FNHSysDrugIdTo_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysDrugIdTo_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysDrugIdTo_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysDrugIdTo_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysDrugIdTo_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDrugIdTo_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugIdTo_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysDrugIdTo_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysDrugIdTo_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysDrugIdTo_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugIdTo_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysDrugIdTo_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysDrugIdTo_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDrugIdTo_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugIdTo_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysDrugIdTo_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysDrugIdTo_None.Properties.ReadOnly = True
        Me.FNHSysDrugIdTo_None.Size = New System.Drawing.Size(886, 20)
        Me.FNHSysDrugIdTo_None.TabIndex = 389
        Me.FNHSysDrugIdTo_None.TabStop = False
        Me.FNHSysDrugIdTo_None.Tag = "2|"
        '
        'FNHSysDrugId_None
        '
        Me.FNHSysDrugId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysDrugId_None.EnterMoveNextControl = True
        Me.FNHSysDrugId_None.Location = New System.Drawing.Point(246, 6)
        Me.FNHSysDrugId_None.Name = "FNHSysDrugId_None"
        Me.FNHSysDrugId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysDrugId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysDrugId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysDrugId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysDrugId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDrugId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysDrugId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysDrugId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysDrugId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysDrugId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysDrugId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDrugId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysDrugId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysDrugId_None.Properties.ReadOnly = True
        Me.FNHSysDrugId_None.Size = New System.Drawing.Size(886, 20)
        Me.FNHSysDrugId_None.TabIndex = 389
        Me.FNHSysDrugId_None.TabStop = False
        Me.FNHSysDrugId_None.Tag = "2|"
        '
        'FTEDate_lbl
        '
        Me.FTEDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTEDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEDate_lbl.Location = New System.Drawing.Point(246, 54)
        Me.FTEDate_lbl.Name = "FTEDate_lbl"
        Me.FTEDate_lbl.Size = New System.Drawing.Size(101, 19)
        Me.FTEDate_lbl.TabIndex = 388
        Me.FTEDate_lbl.Tag = "2|"
        Me.FTEDate_lbl.Text = "Date  To :"
        '
        'FTSDate_lbl
        '
        Me.FTSDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTSDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSDate_lbl.Location = New System.Drawing.Point(10, 53)
        Me.FTSDate_lbl.Name = "FTSDate_lbl"
        Me.FTSDate_lbl.Size = New System.Drawing.Size(101, 19)
        Me.FTSDate_lbl.TabIndex = 388
        Me.FTSDate_lbl.Tag = "2|"
        Me.FTSDate_lbl.Text = "Date  :"
        '
        'FNHSysDrugIdTo_lbl
        '
        Me.FNHSysDrugIdTo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugIdTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysDrugIdTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysDrugIdTo_lbl.Location = New System.Drawing.Point(10, 28)
        Me.FNHSysDrugIdTo_lbl.Name = "FNHSysDrugIdTo_lbl"
        Me.FNHSysDrugIdTo_lbl.Size = New System.Drawing.Size(101, 19)
        Me.FNHSysDrugIdTo_lbl.TabIndex = 388
        Me.FNHSysDrugIdTo_lbl.Tag = "2|"
        Me.FNHSysDrugIdTo_lbl.Text = "Drug Code To :"
        '
        'FNHSysDrugId_lbl
        '
        Me.FNHSysDrugId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysDrugId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysDrugId_lbl.Location = New System.Drawing.Point(10, 6)
        Me.FNHSysDrugId_lbl.Name = "FNHSysDrugId_lbl"
        Me.FNHSysDrugId_lbl.Size = New System.Drawing.Size(101, 19)
        Me.FNHSysDrugId_lbl.TabIndex = 388
        Me.FNHSysDrugId_lbl.Tag = "2|"
        Me.FNHSysDrugId_lbl.Text = "Drug Code :"
        '
        'FNHSysDrugIdTo
        '
        Me.FNHSysDrugIdTo.EnterMoveNextControl = True
        Me.FNHSysDrugIdTo.Location = New System.Drawing.Point(113, 28)
        Me.FNHSysDrugIdTo.Name = "FNHSysDrugIdTo"
        Me.FNHSysDrugIdTo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysDrugIdTo.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysDrugIdTo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDrugIdTo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugIdTo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysDrugIdTo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysDrugIdTo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysDrugIdTo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugIdTo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysDrugIdTo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysDrugIdTo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDrugIdTo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugIdTo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysDrugIdTo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysDrugIdTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "270", Nothing, True)})
        Me.FNHSysDrugIdTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysDrugIdTo.Properties.MaxLength = 30
        Me.FNHSysDrugIdTo.Size = New System.Drawing.Size(132, 20)
        Me.FNHSysDrugIdTo.TabIndex = 1
        Me.FNHSysDrugIdTo.Tag = "2|"
        '
        'FNHSysDrugId
        '
        Me.FNHSysDrugId.EnterMoveNextControl = True
        Me.FNHSysDrugId.Location = New System.Drawing.Point(113, 6)
        Me.FNHSysDrugId.Name = "FNHSysDrugId"
        Me.FNHSysDrugId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysDrugId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysDrugId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDrugId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysDrugId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysDrugId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysDrugId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysDrugId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysDrugId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysDrugId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysDrugId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysDrugId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysDrugId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "263", Nothing, True)})
        Me.FNHSysDrugId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysDrugId.Properties.MaxLength = 30
        Me.FNHSysDrugId.Size = New System.Drawing.Size(132, 20)
        Me.FNHSysDrugId.TabIndex = 0
        Me.FNHSysDrugId.Tag = "2|"
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.ogdtime)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 34)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.ShowCaption = False
        Me.ogbdetail.Size = New System.Drawing.Size(1137, 599)
        Me.ogbdetail.TabIndex = 0
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(91, 110)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(935, 47)
        Me.ogbmainprocbutton.TabIndex = 386
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(409, 12)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(117, 23)
        Me.ocmsavelayout.TabIndex = 332
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(822, 11)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(14, 10)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(95, 25)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(115, 12)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(117, 23)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'ogdtime
        '
        Me.ogdtime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdtime.Location = New System.Drawing.Point(2, 2)
        Me.ogdtime.MainView = Me.ogvtime
        Me.ogdtime.Name = "ogdtime"
        Me.ogdtime.Size = New System.Drawing.Size(1133, 595)
        Me.ogdtime.TabIndex = 0
        Me.ogdtime.TabStop = False
        Me.ogdtime.Tag = "2|"
        Me.ogdtime.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvtime})
        '
        'ogvtime
        '
        Me.ogvtime.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTDrugCode, Me.FTDrugName, Me.FTDrugUnitCode, Me.FNQuantity, Me.GFNHSysDrugId, Me.cFDMEDRcvDate, Me.cFTMEDRcvNo, Me.cFTDocumentRefNo, Me.cFNQuantityBal, Me.cFTMEDIssNo, Me.cFNQuantityIss})
        Me.ogvtime.GridControl = Me.ogdtime
        Me.ogvtime.Name = "ogvtime"
        Me.ogvtime.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvtime.OptionsView.ColumnAutoWidth = False
        Me.ogvtime.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvtime.OptionsView.ShowGroupPanel = False
        Me.ogvtime.Tag = "2|"
        '
        'FTDrugCode
        '
        Me.FTDrugCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTDrugCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTDrugCode.Caption = "FTDrugCode"
        Me.FTDrugCode.FieldName = "FTDrugCode"
        Me.FTDrugCode.Name = "FTDrugCode"
        Me.FTDrugCode.OptionsColumn.AllowEdit = False
        Me.FTDrugCode.OptionsColumn.ReadOnly = True
        Me.FTDrugCode.Visible = True
        Me.FTDrugCode.VisibleIndex = 0
        Me.FTDrugCode.Width = 151
        '
        'FTDrugName
        '
        Me.FTDrugName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTDrugName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTDrugName.Caption = "FTDrugName"
        Me.FTDrugName.FieldName = "FTDrugName"
        Me.FTDrugName.Name = "FTDrugName"
        Me.FTDrugName.OptionsColumn.AllowEdit = False
        Me.FTDrugName.OptionsColumn.ReadOnly = True
        Me.FTDrugName.Visible = True
        Me.FTDrugName.VisibleIndex = 1
        Me.FTDrugName.Width = 250
        '
        'FTDrugUnitCode
        '
        Me.FTDrugUnitCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTDrugUnitCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTDrugUnitCode.Caption = "FTDrugUnitCode"
        Me.FTDrugUnitCode.FieldName = "FTDrugUnitCode"
        Me.FTDrugUnitCode.Name = "FTDrugUnitCode"
        Me.FTDrugUnitCode.OptionsColumn.AllowEdit = False
        Me.FTDrugUnitCode.OptionsColumn.ReadOnly = True
        Me.FTDrugUnitCode.Visible = True
        Me.FTDrugUnitCode.VisibleIndex = 2
        Me.FTDrugUnitCode.Width = 92
        '
        'FNQuantity
        '
        Me.FNQuantity.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.FNQuantity.AppearanceCell.Options.UseForeColor = True
        Me.FNQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNQuantity.Caption = "FNQuantity"
        Me.FNQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNQuantity"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.OptionsColumn.ReadOnly = True
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 5
        Me.FNQuantity.Width = 111
        '
        'GFNHSysDrugId
        '
        Me.GFNHSysDrugId.Caption = "FNHSysDrugId"
        Me.GFNHSysDrugId.FieldName = "FNHSysDrugId"
        Me.GFNHSysDrugId.Name = "GFNHSysDrugId"
        Me.GFNHSysDrugId.OptionsColumn.AllowEdit = False
        '
        'cFDMEDRcvDate
        '
        Me.cFDMEDRcvDate.Caption = "FDMEDRcvDate"
        Me.cFDMEDRcvDate.FieldName = "FDMEDRcvDate"
        Me.cFDMEDRcvDate.Name = "cFDMEDRcvDate"
        Me.cFDMEDRcvDate.OptionsColumn.AllowEdit = False
        Me.cFDMEDRcvDate.Visible = True
        Me.cFDMEDRcvDate.VisibleIndex = 3
        Me.cFDMEDRcvDate.Width = 84
        '
        'cFTMEDRcvNo
        '
        Me.cFTMEDRcvNo.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cFTMEDRcvNo.AppearanceCell.Options.UseForeColor = True
        Me.cFTMEDRcvNo.Caption = "FTMEDRcvNo"
        Me.cFTMEDRcvNo.FieldName = "FTMEDRcvNo"
        Me.cFTMEDRcvNo.Name = "cFTMEDRcvNo"
        Me.cFTMEDRcvNo.OptionsColumn.AllowEdit = False
        Me.cFTMEDRcvNo.Visible = True
        Me.cFTMEDRcvNo.VisibleIndex = 4
        Me.cFTMEDRcvNo.Width = 107
        '
        'cFTDocumentRefNo
        '
        Me.cFTDocumentRefNo.Caption = "FTDocumentRefNo"
        Me.cFTDocumentRefNo.FieldName = "FTDocumentRefNo"
        Me.cFTDocumentRefNo.Name = "cFTDocumentRefNo"
        Me.cFTDocumentRefNo.OptionsColumn.AllowEdit = False
        Me.cFTDocumentRefNo.Width = 93
        '
        'cFNQuantityBal
        '
        Me.cFNQuantityBal.Caption = "FNQuantityBal"
        Me.cFNQuantityBal.DisplayFormat.FormatString = "{0:n4}"
        Me.cFNQuantityBal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQuantityBal.FieldName = "FNQuantityBal"
        Me.cFNQuantityBal.Name = "cFNQuantityBal"
        Me.cFNQuantityBal.OptionsColumn.AllowEdit = False
        Me.cFNQuantityBal.Visible = True
        Me.cFNQuantityBal.VisibleIndex = 8
        Me.cFNQuantityBal.Width = 105
        '
        'cFTMEDIssNo
        '
        Me.cFTMEDIssNo.AppearanceCell.ForeColor = System.Drawing.Color.Red
        Me.cFTMEDIssNo.AppearanceCell.Options.UseForeColor = True
        Me.cFTMEDIssNo.Caption = "FTMEDIssNo"
        Me.cFTMEDIssNo.FieldName = "FTMEDIssNo"
        Me.cFTMEDIssNo.Name = "cFTMEDIssNo"
        Me.cFTMEDIssNo.OptionsColumn.AllowEdit = False
        Me.cFTMEDIssNo.Visible = True
        Me.cFTMEDIssNo.VisibleIndex = 6
        Me.cFTMEDIssNo.Width = 100
        '
        'cFNQuantityIss
        '
        Me.cFNQuantityIss.AppearanceCell.ForeColor = System.Drawing.Color.Red
        Me.cFNQuantityIss.AppearanceCell.Options.UseForeColor = True
        Me.cFNQuantityIss.Caption = "FNQuantityIss"
        Me.cFNQuantityIss.FieldName = "FNQuantityIss"
        Me.cFNQuantityIss.Name = "cFNQuantityIss"
        Me.cFNQuantityIss.OptionsColumn.AllowEdit = False
        Me.cFNQuantityIss.Visible = True
        Me.cFNQuantityIss.VisibleIndex = 7
        Me.cFNQuantityIss.Width = 86
        '
        'oDockManager
        '
        Me.oDockManager.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerTop})
        Me.oDockManager.Form = Me
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'hideContainerTop
        '
        Me.hideContainerTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.hideContainerTop.Controls.Add(Me.ogbheader)
        Me.hideContainerTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.hideContainerTop.Location = New System.Drawing.Point(0, 0)
        Me.hideContainerTop.Name = "hideContainerTop"
        Me.hideContainerTop.Size = New System.Drawing.Size(1137, 34)
        '
        'wMEDStockCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1137, 633)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.hideContainerTop)
        Me.Name = "wMEDStockCard"
        Me.Text = "Drug Stock Card"
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTEDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysDrugIdTo_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysDrugId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysDrugIdTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysDrugId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerTop.ResumeLayout(False)
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
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTDrugCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDrugName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDrugUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents hideContainerTop As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents GFNHSysDrugId As DevExpress.XtraGrid.Columns.GridColumn


    Friend WithEvents FNHSysDrugIdTo_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysDrugId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysDrugIdTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysDrugId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysDrugIdTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysDrugId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents cFDMEDRcvDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTMEDRcvNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDocumentRefNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantityBal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTMEDIssNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantityIss As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTSDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSDate_lbl As DevExpress.XtraEditors.LabelControl
End Class
