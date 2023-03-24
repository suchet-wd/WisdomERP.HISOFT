<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wSMPStockCard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wSMPStockCard))
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTEndDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTStartDate_lbl = New DevExpress.XtraEditors.LabelControl()
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
        Me.FTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDocDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDocTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDocType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDocIn = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNDocInQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDocOut = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDocOutQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager()
        Me.FTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogbheader.OriginalSize = New System.Drawing.Size(1326, 130)
        Me.ogbheader.Size = New System.Drawing.Size(1826, 130)
        Me.ogbheader.TabStop = False
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTEndDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDate_lbl)
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
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1820, 96)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTEndDate
        '
        Me.FTEndDate.EditValue = Nothing
        Me.FTEndDate.EnterMoveNextControl = True
        Me.FTEndDate.Location = New System.Drawing.Point(533, 59)
        Me.FTEndDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEndDate.Name = "FTEndDate"
        Me.FTEndDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDate.Properties.NullDate = ""
        Me.FTEndDate.Size = New System.Drawing.Size(148, 23)
        Me.FTEndDate.TabIndex = 274
        Me.FTEndDate.Tag = "2|"
        '
        'FTEndDate_lbl
        '
        Me.FTEndDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndDate_lbl.Location = New System.Drawing.Point(387, 59)
        Me.FTEndDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEndDate_lbl.Name = "FTEndDate_lbl"
        Me.FTEndDate_lbl.Size = New System.Drawing.Size(142, 23)
        Me.FTEndDate_lbl.TabIndex = 275
        Me.FTEndDate_lbl.Tag = "2|"
        Me.FTEndDate_lbl.Text = "End Date :"
        '
        'FTStartDate
        '
        Me.FTStartDate.EditValue = Nothing
        Me.FTStartDate.EnterMoveNextControl = True
        Me.FTStartDate.Location = New System.Drawing.Point(232, 59)
        Me.FTStartDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartDate.Name = "FTStartDate"
        Me.FTStartDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.NullDate = ""
        Me.FTStartDate.Size = New System.Drawing.Size(148, 23)
        Me.FTStartDate.TabIndex = 272
        Me.FTStartDate.Tag = "2|"
        '
        'FTStartDate_lbl
        '
        Me.FTStartDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartDate_lbl.Location = New System.Drawing.Point(49, 59)
        Me.FTStartDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartDate_lbl.Name = "FTStartDate_lbl"
        Me.FTStartDate_lbl.Size = New System.Drawing.Size(174, 23)
        Me.FTStartDate_lbl.TabIndex = 273
        Me.FTStartDate_lbl.Tag = "2|"
        Me.FTStartDate_lbl.Text = "Start Date :"
        '
        'FTSMPOrderNoTo_lbl
        '
        Me.FTSMPOrderNoTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTSMPOrderNoTo_lbl.Appearance.Options.UseForeColor = True
        Me.FTSMPOrderNoTo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTSMPOrderNoTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSMPOrderNoTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSMPOrderNoTo_lbl.Location = New System.Drawing.Point(383, 32)
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
        Me.FTSMPOrderNoTo.Location = New System.Drawing.Point(533, 32)
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
        Me.FTSMPOrderNoTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "577", Nothing, True)})
        Me.FTSMPOrderNoTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTSMPOrderNoTo.Properties.MaxLength = 30
        Me.FTSMPOrderNoTo.Size = New System.Drawing.Size(148, 23)
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
        Me.FTSMPOrderNo_lbl.Location = New System.Drawing.Point(9, 29)
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
        Me.FNHSysWHId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "575", Nothing, True)})
        Me.FNHSysWHId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysWHId.Properties.MaxLength = 30
        Me.FNHSysWHId.Size = New System.Drawing.Size(148, 23)
        Me.FNHSysWHId.TabIndex = 283
        Me.FNHSysWHId.Tag = "2|"
        '
        'FTSMPOrderNo
        '
        Me.FTSMPOrderNo.EnterMoveNextControl = True
        Me.FTSMPOrderNo.Location = New System.Drawing.Point(232, 32)
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
        Me.FTSMPOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "568", Nothing, True)})
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
        Me.FNHSysWHId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.FNHSysWHId_None.Size = New System.Drawing.Size(1378, 23)
        Me.FNHSysWHId_None.TabIndex = 285
        Me.FNHSysWHId_None.TabStop = False
        Me.FNHSysWHId_None.Tag = "2|"
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.ogdtime)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 130)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.ShowCaption = False
        Me.ogbdetail.Size = New System.Drawing.Size(1826, 649)
        Me.ogbdetail.TabIndex = 0
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(606, 135)
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
        Me.ogdtime.Size = New System.Drawing.Size(1822, 645)
        Me.ogdtime.TabIndex = 0
        Me.ogdtime.TabStop = False
        Me.ogdtime.Tag = "2|"
        Me.ogdtime.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvtime})
        '
        'ogvtime
        '
        Me.ogvtime.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTWHCode, Me.FTRawMatCode, Me.FTRawMatName, Me.FTRawMatColorCode, Me.FTRawMatSizeCode, Me.CFTOrderNo, Me.FTUnitCode, Me.FTDocDate, Me.FTDocTime, Me.FTDocType, Me.FTDocIn, Me.FNDocInQty, Me.FTDocOut, Me.FTDocOutQty, Me.FNQuantity, Me.FTPurchaseNo})
        Me.ogvtime.GridControl = Me.ogdtime
        Me.ogvtime.Name = "ogvtime"
        Me.ogvtime.OptionsCustomization.AllowGroup = False
        Me.ogvtime.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvtime.OptionsCustomization.AllowSort = False
        Me.ogvtime.OptionsFilter.AllowColumnMRUFilterList = False
        Me.ogvtime.OptionsFilter.AllowMRUFilterList = False
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
        Me.FTWHCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTWHCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTWHCode.OptionsColumn.AllowMove = False
        Me.FTWHCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTWHCode.OptionsColumn.ReadOnly = True
        Me.FTWHCode.Visible = True
        Me.FTWHCode.VisibleIndex = 0
        Me.FTWHCode.Width = 79
        '
        'FTRawMatCode
        '
        Me.FTRawMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatCode.Caption = "FTRawMatCode"
        Me.FTRawMatCode.FieldName = "FTMatCode"
        Me.FTRawMatCode.Name = "FTRawMatCode"
        Me.FTRawMatCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTRawMatCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTRawMatCode.OptionsColumn.AllowMove = False
        Me.FTRawMatCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTRawMatCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatCode.Visible = True
        Me.FTRawMatCode.VisibleIndex = 1
        Me.FTRawMatCode.Width = 151
        '
        'FTRawMatName
        '
        Me.FTRawMatName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatName.Caption = "FTRawMatName"
        Me.FTRawMatName.FieldName = "FTDescription"
        Me.FTRawMatName.Name = "FTRawMatName"
        Me.FTRawMatName.OptionsColumn.AllowEdit = False
        Me.FTRawMatName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTRawMatName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTRawMatName.OptionsColumn.AllowMove = False
        Me.FTRawMatName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTRawMatName.OptionsColumn.ReadOnly = True
        Me.FTRawMatName.Visible = True
        Me.FTRawMatName.VisibleIndex = 2
        Me.FTRawMatName.Width = 333
        '
        'FTRawMatColorCode
        '
        Me.FTRawMatColorCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatColorCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatColorCode.Caption = "FTRawMatColorCode"
        Me.FTRawMatColorCode.FieldName = "FTMatColorCode"
        Me.FTRawMatColorCode.Name = "FTRawMatColorCode"
        Me.FTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTRawMatColorCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTRawMatColorCode.OptionsColumn.AllowMove = False
        Me.FTRawMatColorCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatColorCode.Visible = True
        Me.FTRawMatColorCode.VisibleIndex = 3
        Me.FTRawMatColorCode.Width = 83
        '
        'FTRawMatSizeCode
        '
        Me.FTRawMatSizeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatSizeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatSizeCode.Caption = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.FieldName = "FTMatSizeCode"
        Me.FTRawMatSizeCode.Name = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatSizeCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTRawMatSizeCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTRawMatSizeCode.OptionsColumn.AllowMove = False
        Me.FTRawMatSizeCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatSizeCode.Visible = True
        Me.FTRawMatSizeCode.VisibleIndex = 4
        Me.FTRawMatSizeCode.Width = 80
        '
        'CFTOrderNo
        '
        Me.CFTOrderNo.AppearanceHeader.Options.UseTextOptions = True
        Me.CFTOrderNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CFTOrderNo.Caption = "FTOrderNo"
        Me.CFTOrderNo.FieldName = "FTSMPOrderNo"
        Me.CFTOrderNo.Name = "CFTOrderNo"
        Me.CFTOrderNo.OptionsColumn.AllowEdit = False
        Me.CFTOrderNo.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTOrderNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTOrderNo.OptionsColumn.AllowMove = False
        Me.CFTOrderNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTOrderNo.OptionsColumn.ReadOnly = True
        Me.CFTOrderNo.OptionsFilter.AllowFilter = False
        Me.CFTOrderNo.Visible = True
        Me.CFTOrderNo.VisibleIndex = 6
        Me.CFTOrderNo.Width = 120
        '
        'FTUnitCode
        '
        Me.FTUnitCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUnitCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitCode.Caption = "FTUnitCode"
        Me.FTUnitCode.FieldName = "FTUnitCode"
        Me.FTUnitCode.Name = "FTUnitCode"
        Me.FTUnitCode.OptionsColumn.AllowEdit = False
        Me.FTUnitCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTUnitCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTUnitCode.OptionsColumn.AllowMove = False
        Me.FTUnitCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTUnitCode.OptionsColumn.ReadOnly = True
        Me.FTUnitCode.OptionsFilter.AllowFilter = False
        Me.FTUnitCode.Visible = True
        Me.FTUnitCode.VisibleIndex = 5
        Me.FTUnitCode.Width = 65
        '
        'FTDocDate
        '
        Me.FTDocDate.AppearanceCell.Options.UseTextOptions = True
        Me.FTDocDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTDocDate.AppearanceHeader.Options.UseTextOptions = True
        Me.FTDocDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTDocDate.Caption = "FTDocDate"
        Me.FTDocDate.FieldName = "FTDocumentDate"
        Me.FTDocDate.Name = "FTDocDate"
        Me.FTDocDate.OptionsColumn.AllowEdit = False
        Me.FTDocDate.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDocDate.OptionsColumn.AllowMove = False
        Me.FTDocDate.OptionsColumn.AllowShowHide = False
        Me.FTDocDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDocDate.OptionsColumn.ReadOnly = True
        Me.FTDocDate.OptionsFilter.AllowFilter = False
        Me.FTDocDate.Visible = True
        Me.FTDocDate.VisibleIndex = 7
        Me.FTDocDate.Width = 81
        '
        'FTDocTime
        '
        Me.FTDocTime.AppearanceCell.Options.UseTextOptions = True
        Me.FTDocTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTDocTime.AppearanceHeader.Options.UseTextOptions = True
        Me.FTDocTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTDocTime.Caption = "FTDocTime"
        Me.FTDocTime.FieldName = "FTDocumentTime"
        Me.FTDocTime.Name = "FTDocTime"
        Me.FTDocTime.OptionsColumn.AllowEdit = False
        Me.FTDocTime.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDocTime.OptionsColumn.AllowMove = False
        Me.FTDocTime.OptionsColumn.AllowShowHide = False
        Me.FTDocTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDocTime.OptionsColumn.ReadOnly = True
        Me.FTDocTime.OptionsFilter.AllowFilter = False
        Me.FTDocTime.Visible = True
        Me.FTDocTime.VisibleIndex = 8
        '
        'FTDocType
        '
        Me.FTDocType.AppearanceCell.Options.UseTextOptions = True
        Me.FTDocType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTDocType.Caption = "FTDocType"
        Me.FTDocType.FieldName = "FTDocType"
        Me.FTDocType.Name = "FTDocType"
        Me.FTDocType.OptionsColumn.AllowEdit = False
        Me.FTDocType.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDocType.OptionsColumn.AllowMove = False
        Me.FTDocType.OptionsColumn.AllowShowHide = False
        Me.FTDocType.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDocType.OptionsColumn.ReadOnly = True
        Me.FTDocType.OptionsFilter.AllowFilter = False
        Me.FTDocType.Visible = True
        Me.FTDocType.VisibleIndex = 9
        '
        'FTDocIn
        '
        Me.FTDocIn.AppearanceHeader.Options.UseTextOptions = True
        Me.FTDocIn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTDocIn.Caption = "FTDocIn"
        Me.FTDocIn.FieldName = "FTDocIn"
        Me.FTDocIn.Name = "FTDocIn"
        Me.FTDocIn.OptionsColumn.AllowEdit = False
        Me.FTDocIn.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDocIn.OptionsColumn.AllowMove = False
        Me.FTDocIn.OptionsColumn.AllowShowHide = False
        Me.FTDocIn.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDocIn.OptionsColumn.ReadOnly = True
        Me.FTDocIn.OptionsFilter.AllowFilter = False
        Me.FTDocIn.Visible = True
        Me.FTDocIn.VisibleIndex = 10
        Me.FTDocIn.Width = 120
        '
        'FNDocInQty
        '
        Me.FNDocInQty.AppearanceCell.Options.UseTextOptions = True
        Me.FNDocInQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNDocInQty.AppearanceHeader.Options.UseTextOptions = True
        Me.FNDocInQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNDocInQty.Caption = "FNDocInQty"
        Me.FNDocInQty.DisplayFormat.FormatString = "{0:n4}"
        Me.FNDocInQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNDocInQty.FieldName = "FNQuantityIn"
        Me.FNDocInQty.Name = "FNDocInQty"
        Me.FNDocInQty.OptionsColumn.AllowEdit = False
        Me.FNDocInQty.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNDocInQty.OptionsColumn.AllowMove = False
        Me.FNDocInQty.OptionsColumn.AllowShowHide = False
        Me.FNDocInQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNDocInQty.OptionsColumn.ReadOnly = True
        Me.FNDocInQty.OptionsFilter.AllowFilter = False
        Me.FNDocInQty.Visible = True
        Me.FNDocInQty.VisibleIndex = 11
        Me.FNDocInQty.Width = 100
        '
        'FTDocOut
        '
        Me.FTDocOut.AppearanceHeader.Options.UseTextOptions = True
        Me.FTDocOut.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTDocOut.Caption = "FTDocOut"
        Me.FTDocOut.FieldName = "FTDocOut"
        Me.FTDocOut.Name = "FTDocOut"
        Me.FTDocOut.OptionsColumn.AllowEdit = False
        Me.FTDocOut.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDocOut.OptionsColumn.AllowMove = False
        Me.FTDocOut.OptionsColumn.AllowShowHide = False
        Me.FTDocOut.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDocOut.OptionsColumn.ReadOnly = True
        Me.FTDocOut.OptionsFilter.AllowFilter = False
        Me.FTDocOut.Visible = True
        Me.FTDocOut.VisibleIndex = 12
        Me.FTDocOut.Width = 120
        '
        'FTDocOutQty
        '
        Me.FTDocOutQty.AppearanceCell.Options.UseTextOptions = True
        Me.FTDocOutQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTDocOutQty.AppearanceHeader.Options.UseTextOptions = True
        Me.FTDocOutQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTDocOutQty.Caption = "FTDocOutQty"
        Me.FTDocOutQty.DisplayFormat.FormatString = "{0:n4}"
        Me.FTDocOutQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FTDocOutQty.FieldName = "FNQuantityOut"
        Me.FTDocOutQty.Name = "FTDocOutQty"
        Me.FTDocOutQty.OptionsColumn.AllowEdit = False
        Me.FTDocOutQty.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDocOutQty.OptionsColumn.AllowMove = False
        Me.FTDocOutQty.OptionsColumn.AllowShowHide = False
        Me.FTDocOutQty.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDocOutQty.OptionsColumn.ReadOnly = True
        Me.FTDocOutQty.OptionsFilter.AllowFilter = False
        Me.FTDocOutQty.Visible = True
        Me.FTDocOutQty.VisibleIndex = 13
        Me.FTDocOutQty.Width = 100
        '
        'FNQuantity
        '
        Me.FNQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNQuantity.Caption = "FNQuantity"
        Me.FNQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNQuantityBal"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNQuantity.OptionsColumn.AllowMove = False
        Me.FNQuantity.OptionsColumn.AllowShowHide = False
        Me.FNQuantity.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNQuantity.OptionsColumn.ReadOnly = True
        Me.FNQuantity.OptionsFilter.AllowFilter = False
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 14
        Me.FNQuantity.Width = 100
        '
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.ogbheader})
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'FTPurchaseNo
        '
        Me.FTPurchaseNo.Caption = "FTPurchaseNo"
        Me.FTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.FTPurchaseNo.Name = "FTPurchaseNo"
        Me.FTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.FTPurchaseNo.Visible = True
        Me.FTPurchaseNo.VisibleIndex = 15
        Me.FTPurchaseNo.Width = 120
        '
        'wSMPStockCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1826, 779)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wSMPStockCard"
        Me.Text = "Stock Onhand"
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents FTSMPOrderNoTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSMPOrderNoTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTSMPOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSMPOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTWHCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysWHId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysWHId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysWHId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTDocDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDocTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDocIn As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNDocInQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDocOut As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDocOutQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDocType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
End Class
