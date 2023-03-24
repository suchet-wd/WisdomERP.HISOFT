<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wFGStockCard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wFGStockCard))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.DockManager = New DevExpress.XtraBars.Docking.DockManager()
        Me.oCriteria = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysWHFGIdTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTEndDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTOrderNoTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNoTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysWHIdFG = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysWHFGId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysWHIdFGTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsaveweightpack = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ogrpdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysWHFGId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTWHFGCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTWHFGName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDTransection = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTColorWay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTSizeBreakDown = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantityIssue = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantityOut = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantityBal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDocumentNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.oCriteria.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNoTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHIdFG.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHIdFGTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogrpdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpdetail.SuspendLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DockManager
        '
        Me.DockManager.Form = Me
        Me.DockManager.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.oCriteria})
        Me.DockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'oCriteria
        '
        Me.oCriteria.Controls.Add(Me.DockPanel1_Container)
        Me.oCriteria.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.oCriteria.ID = New System.Guid("54c01734-5e68-4b35-88a2-338d6629fb69")
        Me.oCriteria.Image = CType(resources.GetObject("oCriteria.Image"), System.Drawing.Image)
        Me.oCriteria.Location = New System.Drawing.Point(0, 0)
        Me.oCriteria.Name = "oCriteria"
        Me.oCriteria.Options.AllowDockBottom = False
        Me.oCriteria.Options.AllowDockFill = False
        Me.oCriteria.Options.AllowDockLeft = False
        Me.oCriteria.Options.AllowDockRight = False
        Me.oCriteria.Options.ShowCloseButton = False
        Me.oCriteria.Options.ShowMaximizeButton = False
        Me.oCriteria.OriginalSize = New System.Drawing.Size(200, 141)
        Me.oCriteria.Size = New System.Drawing.Size(1059, 141)
        Me.oCriteria.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTEndDate)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysWHFGIdTo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNoTo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNoTo)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysWHIdFG)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysWHFGId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysWHIdFGTo)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 23)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1051, 114)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTEndDate
        '
        Me.FTEndDate.EditValue = Nothing
        Me.FTEndDate.EnterMoveNextControl = True
        Me.FTEndDate.Location = New System.Drawing.Point(456, 72)
        Me.FTEndDate.Name = "FTEndDate"
        Me.FTEndDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndDate.Properties.DisplayFormat.FormatString = "d"
        Me.FTEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDate.Properties.EditFormat.FormatString = "d"
        Me.FTEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDate.Properties.NullDate = ""
        Me.FTEndDate.Size = New System.Drawing.Size(131, 20)
        Me.FTEndDate.TabIndex = 278
        Me.FTEndDate.Tag = "2|"
        '
        'FNHSysWHFGIdTo_lbl
        '
        Me.FNHSysWHFGIdTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysWHFGIdTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysWHFGIdTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysWHFGIdTo_lbl.Location = New System.Drawing.Point(68, 27)
        Me.FNHSysWHFGIdTo_lbl.Name = "FNHSysWHFGIdTo_lbl"
        Me.FNHSysWHFGIdTo_lbl.Size = New System.Drawing.Size(126, 19)
        Me.FNHSysWHFGIdTo_lbl.TabIndex = 405
        Me.FNHSysWHFGIdTo_lbl.Tag = "2|"
        Me.FNHSysWHFGIdTo_lbl.Text = "FNHSysWHFGIdTo :"
        '
        'FTEndDate_lbl
        '
        Me.FTEndDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndDate_lbl.Location = New System.Drawing.Point(335, 72)
        Me.FTEndDate_lbl.Name = "FTEndDate_lbl"
        Me.FTEndDate_lbl.Size = New System.Drawing.Size(118, 19)
        Me.FTEndDate_lbl.TabIndex = 279
        Me.FTEndDate_lbl.Tag = "2|"
        Me.FTEndDate_lbl.Text = "End Date :"
        '
        'FTOrderNo_lbl
        '
        Me.FTOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNo_lbl.Location = New System.Drawing.Point(7, 47)
        Me.FTOrderNo_lbl.Name = "FTOrderNo_lbl"
        Me.FTOrderNo_lbl.Size = New System.Drawing.Size(188, 19)
        Me.FTOrderNo_lbl.TabIndex = 402
        Me.FTOrderNo_lbl.Tag = "2|"
        Me.FTOrderNo_lbl.Text = "From Order No :"
        '
        'FTStartDate
        '
        Me.FTStartDate.EditValue = Nothing
        Me.FTStartDate.EnterMoveNextControl = True
        Me.FTStartDate.Location = New System.Drawing.Point(197, 72)
        Me.FTStartDate.Name = "FTStartDate"
        Me.FTStartDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartDate.Properties.DisplayFormat.FormatString = "d"
        Me.FTStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.EditFormat.FormatString = "d"
        Me.FTStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.NullDate = ""
        Me.FTStartDate.Size = New System.Drawing.Size(130, 20)
        Me.FTStartDate.TabIndex = 276
        Me.FTStartDate.Tag = "2|"
        '
        'FTOrderNoTo_lbl
        '
        Me.FTOrderNoTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTOrderNoTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNoTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNoTo_lbl.Location = New System.Drawing.Point(327, 49)
        Me.FTOrderNoTo_lbl.Name = "FTOrderNoTo_lbl"
        Me.FTOrderNoTo_lbl.Size = New System.Drawing.Size(126, 19)
        Me.FTOrderNoTo_lbl.TabIndex = 406
        Me.FTOrderNoTo_lbl.Tag = "2|"
        Me.FTOrderNoTo_lbl.Text = "To Order No :"
        '
        'FTStartDate_lbl
        '
        Me.FTStartDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartDate_lbl.Location = New System.Drawing.Point(45, 72)
        Me.FTStartDate_lbl.Name = "FTStartDate_lbl"
        Me.FTStartDate_lbl.Size = New System.Drawing.Size(149, 19)
        Me.FTStartDate_lbl.TabIndex = 277
        Me.FTStartDate_lbl.Tag = "2|"
        Me.FTStartDate_lbl.Text = "Start Date :"
        '
        'FTOrderNoTo
        '
        Me.FTOrderNoTo.EnterMoveNextControl = True
        Me.FTOrderNoTo.Location = New System.Drawing.Point(456, 49)
        Me.FTOrderNoTo.Name = "FTOrderNoTo"
        Me.FTOrderNoTo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTOrderNoTo.Properties.Appearance.Options.UseBackColor = True
        Me.FTOrderNoTo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderNoTo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNoTo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTOrderNoTo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTOrderNoTo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTOrderNoTo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNoTo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTOrderNoTo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTOrderNoTo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderNoTo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNoTo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTOrderNoTo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTOrderNoTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "302", Nothing, True)})
        Me.FTOrderNoTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTOrderNoTo.Properties.MaxLength = 30
        Me.FTOrderNoTo.Size = New System.Drawing.Size(130, 20)
        Me.FTOrderNoTo.TabIndex = 404
        Me.FTOrderNoTo.Tag = "2|"
        '
        'FNHSysWHIdFG
        '
        Me.FNHSysWHIdFG.EnterMoveNextControl = True
        Me.FNHSysWHIdFG.Location = New System.Drawing.Point(197, 4)
        Me.FNHSysWHIdFG.Name = "FNHSysWHIdFG"
        Me.FNHSysWHIdFG.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysWHIdFG.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysWHIdFG.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHIdFG.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHIdFG.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysWHIdFG.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysWHIdFG.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysWHIdFG.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHIdFG.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysWHIdFG.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysWHIdFG.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHIdFG.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHIdFG.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysWHIdFG.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysWHIdFG.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "290", Nothing, True)})
        Me.FNHSysWHIdFG.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysWHIdFG.Properties.MaxLength = 30
        Me.FNHSysWHIdFG.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysWHIdFG.TabIndex = 399
        Me.FNHSysWHIdFG.Tag = "2|"
        '
        'FNHSysWHFGId_lbl
        '
        Me.FNHSysWHFGId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysWHFGId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysWHFGId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysWHFGId_lbl.Location = New System.Drawing.Point(7, 2)
        Me.FNHSysWHFGId_lbl.Name = "FNHSysWHFGId_lbl"
        Me.FNHSysWHFGId_lbl.Size = New System.Drawing.Size(188, 19)
        Me.FNHSysWHFGId_lbl.TabIndex = 401
        Me.FNHSysWHFGId_lbl.Tag = "2|"
        Me.FNHSysWHFGId_lbl.Text = "FNHSysWHFGId :"
        '
        'FNHSysWHIdFGTo
        '
        Me.FNHSysWHIdFGTo.EnterMoveNextControl = True
        Me.FNHSysWHIdFGTo.Location = New System.Drawing.Point(197, 27)
        Me.FNHSysWHIdFGTo.Name = "FNHSysWHIdFGTo"
        Me.FNHSysWHIdFGTo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysWHIdFGTo.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysWHIdFGTo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHIdFGTo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHIdFGTo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysWHIdFGTo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysWHIdFGTo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysWHIdFGTo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHIdFGTo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysWHIdFGTo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysWHIdFGTo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHIdFGTo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHIdFGTo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysWHIdFGTo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysWHIdFGTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "289", Nothing, True)})
        Me.FNHSysWHIdFGTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysWHIdFGTo.Properties.MaxLength = 30
        Me.FNHSysWHIdFGTo.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysWHIdFGTo.TabIndex = 403
        Me.FNHSysWHIdFGTo.Tag = "2|"
        '
        'FTOrderNo
        '
        Me.FTOrderNo.EnterMoveNextControl = True
        Me.FTOrderNo.Location = New System.Drawing.Point(197, 49)
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTOrderNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTOrderNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTOrderNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTOrderNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTOrderNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "301", Nothing, True)})
        Me.FTOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTOrderNo.Properties.MaxLength = 30
        Me.FTOrderNo.Size = New System.Drawing.Size(130, 20)
        Me.FTOrderNo.TabIndex = 400
        Me.FTOrderNo.Tag = "2|"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsaveweightpack)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(625, 0)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(355, 74)
        Me.ogbmainprocbutton.TabIndex = 395
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(23, 44)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(117, 23)
        Me.ocmload.TabIndex = 330
        Me.ocmload.Text = "Load Data"
        '
        'ocmsaveweightpack
        '
        Me.ocmsaveweightpack.Location = New System.Drawing.Point(211, 13)
        Me.ocmsaveweightpack.Name = "ocmsaveweightpack"
        Me.ocmsaveweightpack.Size = New System.Drawing.Size(83, 25)
        Me.ocmsaveweightpack.TabIndex = 105
        Me.ocmsaveweightpack.TabStop = False
        Me.ocmsaveweightpack.Tag = "2|"
        Me.ocmsaveweightpack.Text = "saveweight"
        Me.ocmsaveweightpack.Visible = False
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ocmexit.Location = New System.Drawing.Point(32, 13)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(151, 13)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(54, 25)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ogrpdetail
        '
        Me.ogrpdetail.Controls.Add(Me.ogcdetail)
        Me.ogrpdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrpdetail.Location = New System.Drawing.Point(0, 141)
        Me.ogrpdetail.Name = "ogrpdetail"
        Me.ogrpdetail.Size = New System.Drawing.Size(1059, 417)
        Me.ogrpdetail.TabIndex = 397
        Me.ogrpdetail.Text = "Detail"
        '
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetail.Location = New System.Drawing.Point(2, 21)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.Size = New System.Drawing.Size(1055, 394)
        Me.ogcdetail.TabIndex = 0
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTStyleCode, Me.cFNHSysWHFGId, Me.cFTWHFGCode, Me.cFTWHFGName, Me.cFDTransection, Me.cFTOrderNo, Me.cFTColorWay, Me.cFTSizeBreakDown, Me.cFNQuantity, Me.cFNQuantityIssue, Me.cFNQuantityOut, Me.cFNQuantityBal, Me.cFTDocumentNo, Me.cFTPORef})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowFilter = False
        Me.ogvdetail.OptionsCustomization.AllowSort = False
        Me.ogvdetail.OptionsFilter.AllowColumnMRUFilterList = False
        Me.ogvdetail.OptionsFilter.AllowMRUFilterList = False
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        Me.ogvdetail.Tag = ""
        '
        'cFTStyleCode
        '
        Me.cFTStyleCode.Caption = "FTStyleCode"
        Me.cFTStyleCode.FieldName = "FTStyleCode"
        Me.cFTStyleCode.Name = "cFTStyleCode"
        Me.cFTStyleCode.OptionsColumn.AllowEdit = False
        Me.cFTStyleCode.Visible = True
        Me.cFTStyleCode.VisibleIndex = 6
        Me.cFTStyleCode.Width = 98
        '
        'cFNHSysWHFGId
        '
        Me.cFNHSysWHFGId.Caption = "FNHSysWHFGId"
        Me.cFNHSysWHFGId.FieldName = "FNHSysWHFGId"
        Me.cFNHSysWHFGId.Name = "cFNHSysWHFGId"
        Me.cFNHSysWHFGId.OptionsColumn.AllowEdit = False
        '
        'cFTWHFGCode
        '
        Me.cFTWHFGCode.Caption = "FTWHFGCode"
        Me.cFTWHFGCode.FieldName = "FTWHFGCode"
        Me.cFTWHFGCode.Name = "cFTWHFGCode"
        Me.cFTWHFGCode.OptionsColumn.AllowEdit = False
        Me.cFTWHFGCode.Visible = True
        Me.cFTWHFGCode.VisibleIndex = 0
        Me.cFTWHFGCode.Width = 108
        '
        'cFTWHFGName
        '
        Me.cFTWHFGName.Caption = "FTWHFGName"
        Me.cFTWHFGName.FieldName = "FTWHFGName"
        Me.cFTWHFGName.Name = "cFTWHFGName"
        Me.cFTWHFGName.OptionsColumn.AllowEdit = False
        Me.cFTWHFGName.Visible = True
        Me.cFTWHFGName.VisibleIndex = 1
        Me.cFTWHFGName.Width = 297
        '
        'cFDTransection
        '
        Me.cFDTransection.Caption = "FDTransection"
        Me.cFDTransection.FieldName = "FDTransection"
        Me.cFDTransection.Name = "cFDTransection"
        Me.cFDTransection.OptionsColumn.AllowEdit = False
        Me.cFDTransection.Visible = True
        Me.cFDTransection.VisibleIndex = 3
        Me.cFDTransection.Width = 147
        '
        'cFTOrderNo
        '
        Me.cFTOrderNo.Caption = "FTOrderNo"
        Me.cFTOrderNo.FieldName = "FTOrderNo"
        Me.cFTOrderNo.Name = "cFTOrderNo"
        Me.cFTOrderNo.OptionsColumn.AllowEdit = False
        Me.cFTOrderNo.Visible = True
        Me.cFTOrderNo.VisibleIndex = 5
        Me.cFTOrderNo.Width = 103
        '
        'cFTColorWay
        '
        Me.cFTColorWay.Caption = "FTColorWay"
        Me.cFTColorWay.FieldName = "FTColorWay"
        Me.cFTColorWay.Name = "cFTColorWay"
        Me.cFTColorWay.OptionsColumn.AllowEdit = False
        Me.cFTColorWay.Visible = True
        Me.cFTColorWay.VisibleIndex = 7
        Me.cFTColorWay.Width = 84
        '
        'cFTSizeBreakDown
        '
        Me.cFTSizeBreakDown.Caption = "FTSizeBreakDown"
        Me.cFTSizeBreakDown.FieldName = "FTSizeBreakDown"
        Me.cFTSizeBreakDown.Name = "cFTSizeBreakDown"
        Me.cFTSizeBreakDown.OptionsColumn.AllowEdit = False
        Me.cFTSizeBreakDown.Visible = True
        Me.cFTSizeBreakDown.VisibleIndex = 8
        Me.cFTSizeBreakDown.Width = 100
        '
        'cFNQuantity
        '
        Me.cFNQuantity.Caption = "FNQuantity"
        Me.cFNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.cFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQuantity.FieldName = "FNQuantity"
        Me.cFNQuantity.Name = "cFNQuantity"
        Me.cFNQuantity.OptionsColumn.AllowEdit = False
        Me.cFNQuantity.Visible = True
        Me.cFNQuantity.VisibleIndex = 9
        Me.cFNQuantity.Width = 93
        '
        'cFNQuantityIssue
        '
        Me.cFNQuantityIssue.Caption = "FNQuantityIssue"
        Me.cFNQuantityIssue.DisplayFormat.FormatString = "{0:n0}"
        Me.cFNQuantityIssue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQuantityIssue.FieldName = "FNQuantityIssue"
        Me.cFNQuantityIssue.Name = "cFNQuantityIssue"
        Me.cFNQuantityIssue.OptionsColumn.AllowEdit = False
        Me.cFNQuantityIssue.Visible = True
        Me.cFNQuantityIssue.VisibleIndex = 10
        Me.cFNQuantityIssue.Width = 77
        '
        'cFNQuantityOut
        '
        Me.cFNQuantityOut.Caption = "FNQuantityOut"
        Me.cFNQuantityOut.DisplayFormat.FormatString = "{0:n0}"
        Me.cFNQuantityOut.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQuantityOut.FieldName = "FNQuantityOut"
        Me.cFNQuantityOut.Name = "cFNQuantityOut"
        Me.cFNQuantityOut.OptionsColumn.AllowEdit = False
        Me.cFNQuantityOut.Visible = True
        Me.cFNQuantityOut.VisibleIndex = 11
        Me.cFNQuantityOut.Width = 79
        '
        'cFNQuantityBal
        '
        Me.cFNQuantityBal.Caption = "FNQuantityBal"
        Me.cFNQuantityBal.FieldName = "FNQuantityBal"
        Me.cFNQuantityBal.Name = "cFNQuantityBal"
        Me.cFNQuantityBal.OptionsColumn.AllowEdit = False
        Me.cFNQuantityBal.Visible = True
        Me.cFNQuantityBal.VisibleIndex = 12
        Me.cFNQuantityBal.Width = 87
        '
        'cFTDocumentNo
        '
        Me.cFTDocumentNo.Caption = "FTDocumentNo"
        Me.cFTDocumentNo.FieldName = "FTDocumentNo"
        Me.cFTDocumentNo.Name = "cFTDocumentNo"
        Me.cFTDocumentNo.OptionsColumn.AllowEdit = False
        Me.cFTDocumentNo.Visible = True
        Me.cFTDocumentNo.VisibleIndex = 2
        Me.cFTDocumentNo.Width = 109
        '
        'cFTPORef
        '
        Me.cFTPORef.Caption = "FTPORef"
        Me.cFTPORef.FieldName = "FTPORef"
        Me.cFTPORef.Name = "cFTPORef"
        Me.cFTPORef.OptionsColumn.AllowEdit = False
        Me.cFTPORef.Visible = True
        Me.cFTPORef.VisibleIndex = 4
        Me.cFTPORef.Width = 122
        '
        'wFGStockCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1059, 558)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogrpdetail)
        Me.Controls.Add(Me.oCriteria)
        Me.Name = "wFGStockCard"
        Me.Text = "wFGStockCard"
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.oCriteria.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNoTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHIdFG.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHIdFGTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogrpdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpdetail.ResumeLayout(False)
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents oCriteria As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents FNHSysWHFGIdTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNoTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNoTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysWHIdFG As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysWHFGId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysWHIdFGTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsaveweightpack As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogrpdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFNHSysWHFGId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTWHFGCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTWHFGName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDTransection As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTColorWay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTSizeBreakDown As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantityOut As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantityBal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cFNQuantityIssue As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDocumentNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTPORef As DevExpress.XtraGrid.Columns.GridColumn
End Class
