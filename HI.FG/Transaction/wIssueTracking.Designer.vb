<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wIssueTracking
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wIssueTracking))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.DockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerTop = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.oCriteria = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FNHSysWHFGId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysWHFGIdTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysWHIdFGTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysWHIdFG = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTOrderNoTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTOrderNoTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogrpdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTColorWay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTSizeBreakDown = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantityIss = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantitySale = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantityBal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTWHFGCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsaveweightpack = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerTop.SuspendLayout()
        Me.oCriteria.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHIdFGTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHIdFG.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNoTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogrpdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpdetail.SuspendLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.SuspendLayout()
        '
        'DockManager
        '
        Me.DockManager.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerTop})
        Me.DockManager.Form = Me
        Me.DockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'hideContainerTop
        '
        Me.hideContainerTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.hideContainerTop.Controls.Add(Me.oCriteria)
        Me.hideContainerTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.hideContainerTop.Location = New System.Drawing.Point(0, 0)
        Me.hideContainerTop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.hideContainerTop.Name = "hideContainerTop"
        Me.hideContainerTop.Size = New System.Drawing.Size(1261, 40)
        '
        'oCriteria
        '
        Me.oCriteria.Controls.Add(Me.DockPanel1_Container)
        Me.oCriteria.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.oCriteria.ID = New System.Guid("15ad2cfa-a8b3-4975-b89d-1ba620b1f951")
        Me.oCriteria.Image = CType(resources.GetObject("oCriteria.Image"), System.Drawing.Image)
        Me.oCriteria.Location = New System.Drawing.Point(0, 0)
        Me.oCriteria.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oCriteria.Name = "oCriteria"
        Me.oCriteria.OriginalSize = New System.Drawing.Size(200, 111)
        Me.oCriteria.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.oCriteria.SavedIndex = 0
        Me.oCriteria.Size = New System.Drawing.Size(1261, 111)
        Me.oCriteria.Text = "Criteria"
        Me.oCriteria.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysWHFGId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysWHFGIdTo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysWHIdFGTo)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysWHIdFG)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNoTo)
        Me.DockPanel1_Container.Controls.Add(Me.FTOrderNoTo_lbl)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(5, 27)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1251, 77)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FNHSysWHFGId_lbl
        '
        Me.FNHSysWHFGId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysWHFGId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysWHFGId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysWHFGId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysWHFGId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysWHFGId_lbl.Location = New System.Drawing.Point(9, 4)
        Me.FNHSysWHFGId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHFGId_lbl.Name = "FNHSysWHFGId_lbl"
        Me.FNHSysWHFGId_lbl.Size = New System.Drawing.Size(219, 23)
        Me.FNHSysWHFGId_lbl.TabIndex = 413
        Me.FNHSysWHFGId_lbl.Tag = "2|"
        Me.FNHSysWHFGId_lbl.Text = "FNHSysWHFGId :"
        '
        'FNHSysWHFGIdTo_lbl
        '
        Me.FNHSysWHFGIdTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysWHFGIdTo_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysWHFGIdTo_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysWHFGIdTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysWHFGIdTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysWHFGIdTo_lbl.Location = New System.Drawing.Point(9, 34)
        Me.FNHSysWHFGIdTo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHFGIdTo_lbl.Name = "FNHSysWHFGIdTo_lbl"
        Me.FNHSysWHFGIdTo_lbl.Size = New System.Drawing.Size(218, 23)
        Me.FNHSysWHFGIdTo_lbl.TabIndex = 417
        Me.FNHSysWHFGIdTo_lbl.Tag = "2|"
        Me.FNHSysWHFGIdTo_lbl.Text = "FNHSysWHFGIdTo :"
        '
        'FTOrderNo
        '
        Me.FTOrderNo.EnterMoveNextControl = True
        Me.FTOrderNo.Location = New System.Drawing.Point(231, 62)
        Me.FTOrderNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.FTOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "301", Nothing, True)})
        Me.FTOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTOrderNo.Properties.MaxLength = 30
        Me.FTOrderNo.Size = New System.Drawing.Size(152, 22)
        Me.FTOrderNo.TabIndex = 412
        Me.FTOrderNo.Tag = "2|"
        '
        'FNHSysWHIdFGTo
        '
        Me.FNHSysWHIdFGTo.EnterMoveNextControl = True
        Me.FNHSysWHIdFGTo.Location = New System.Drawing.Point(231, 34)
        Me.FNHSysWHIdFGTo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.FNHSysWHIdFGTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "289", Nothing, True)})
        Me.FNHSysWHIdFGTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysWHIdFGTo.Properties.MaxLength = 30
        Me.FNHSysWHIdFGTo.Size = New System.Drawing.Size(152, 22)
        Me.FNHSysWHIdFGTo.TabIndex = 415
        Me.FNHSysWHIdFGTo.Tag = "2|"
        '
        'FTOrderNo_lbl
        '
        Me.FTOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNo_lbl.Location = New System.Drawing.Point(9, 59)
        Me.FTOrderNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTOrderNo_lbl.Name = "FTOrderNo_lbl"
        Me.FTOrderNo_lbl.Size = New System.Drawing.Size(219, 23)
        Me.FTOrderNo_lbl.TabIndex = 414
        Me.FTOrderNo_lbl.Tag = "2|"
        Me.FTOrderNo_lbl.Text = "From Order No :"
        '
        'FNHSysWHIdFG
        '
        Me.FNHSysWHIdFG.EnterMoveNextControl = True
        Me.FNHSysWHIdFG.Location = New System.Drawing.Point(231, 6)
        Me.FNHSysWHIdFG.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.FNHSysWHIdFG.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "290", Nothing, True)})
        Me.FNHSysWHIdFG.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysWHIdFG.Properties.MaxLength = 30
        Me.FNHSysWHIdFG.Size = New System.Drawing.Size(152, 22)
        Me.FNHSysWHIdFG.TabIndex = 411
        Me.FNHSysWHIdFG.Tag = "2|"
        '
        'FTOrderNoTo
        '
        Me.FTOrderNoTo.EnterMoveNextControl = True
        Me.FTOrderNoTo.Location = New System.Drawing.Point(533, 62)
        Me.FTOrderNoTo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
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
        Me.FTOrderNoTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "302", Nothing, True)})
        Me.FTOrderNoTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTOrderNoTo.Properties.MaxLength = 30
        Me.FTOrderNoTo.Size = New System.Drawing.Size(152, 22)
        Me.FTOrderNoTo.TabIndex = 416
        Me.FTOrderNoTo.Tag = "2|"
        '
        'FTOrderNoTo_lbl
        '
        Me.FTOrderNoTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTOrderNoTo_lbl.Appearance.Options.UseForeColor = True
        Me.FTOrderNoTo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTOrderNoTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNoTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNoTo_lbl.Location = New System.Drawing.Point(383, 62)
        Me.FTOrderNoTo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTOrderNoTo_lbl.Name = "FTOrderNoTo_lbl"
        Me.FTOrderNoTo_lbl.Size = New System.Drawing.Size(147, 23)
        Me.FTOrderNoTo_lbl.TabIndex = 418
        Me.FTOrderNoTo_lbl.Tag = "2|"
        Me.FTOrderNoTo_lbl.Text = "To Order No :"
        '
        'ogrpdetail
        '
        Me.ogrpdetail.Controls.Add(Me.ogcdetail)
        Me.ogrpdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrpdetail.Location = New System.Drawing.Point(0, 40)
        Me.ogrpdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpdetail.Name = "ogrpdetail"
        Me.ogrpdetail.Size = New System.Drawing.Size(1261, 686)
        Me.ogrpdetail.TabIndex = 3
        Me.ogrpdetail.Text = "Detail"
        '
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Location = New System.Drawing.Point(2, 25)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.Size = New System.Drawing.Size(1257, 659)
        Me.ogcdetail.TabIndex = 0
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTOrderNo, Me.cFTStyleCode, Me.cFTColorWay, Me.cFTSizeBreakDown, Me.cFNQuantityIss, Me.cFNQuantitySale, Me.cFNQuantityBal, Me.cFTWHFGCode, Me.cFTPORef})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.ShowFooter = True
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'cFTOrderNo
        '
        Me.cFTOrderNo.Caption = "FTOrderNo"
        Me.cFTOrderNo.FieldName = "FTOrderNo"
        Me.cFTOrderNo.Name = "cFTOrderNo"
        Me.cFTOrderNo.OptionsColumn.AllowEdit = False
        Me.cFTOrderNo.Visible = True
        Me.cFTOrderNo.VisibleIndex = 2
        Me.cFTOrderNo.Width = 125
        '
        'cFTStyleCode
        '
        Me.cFTStyleCode.Caption = "FTStyleCode"
        Me.cFTStyleCode.FieldName = "FTStyleCode"
        Me.cFTStyleCode.Name = "cFTStyleCode"
        Me.cFTStyleCode.OptionsColumn.AllowEdit = False
        Me.cFTStyleCode.Visible = True
        Me.cFTStyleCode.VisibleIndex = 3
        Me.cFTStyleCode.Width = 100
        '
        'cFTColorWay
        '
        Me.cFTColorWay.Caption = "FTColorWay"
        Me.cFTColorWay.FieldName = "FTColorway"
        Me.cFTColorWay.Name = "cFTColorWay"
        Me.cFTColorWay.OptionsColumn.AllowEdit = False
        Me.cFTColorWay.Visible = True
        Me.cFTColorWay.VisibleIndex = 4
        Me.cFTColorWay.Width = 87
        '
        'cFTSizeBreakDown
        '
        Me.cFTSizeBreakDown.Caption = "FTSizeBreakDown"
        Me.cFTSizeBreakDown.FieldName = "FTSizeBreakDown"
        Me.cFTSizeBreakDown.Name = "cFTSizeBreakDown"
        Me.cFTSizeBreakDown.OptionsColumn.AllowEdit = False
        Me.cFTSizeBreakDown.Visible = True
        Me.cFTSizeBreakDown.VisibleIndex = 5
        Me.cFTSizeBreakDown.Width = 100
        '
        'cFNQuantityIss
        '
        Me.cFNQuantityIss.Caption = "FNQuantityIss"
        Me.cFNQuantityIss.DisplayFormat.FormatString = "{0:n0}"
        Me.cFNQuantityIss.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQuantityIss.FieldName = "FNQuantityIss"
        Me.cFNQuantityIss.Name = "cFNQuantityIss"
        Me.cFNQuantityIss.OptionsColumn.AllowEdit = False
        Me.cFNQuantityIss.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantityIss", "{0:n0}")})
        Me.cFNQuantityIss.Visible = True
        Me.cFNQuantityIss.VisibleIndex = 6
        Me.cFNQuantityIss.Width = 84
        '
        'cFNQuantitySale
        '
        Me.cFNQuantitySale.Caption = "FNQuantitySale"
        Me.cFNQuantitySale.DisplayFormat.FormatString = "{0:n0}"
        Me.cFNQuantitySale.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQuantitySale.FieldName = "FNQuantitySale"
        Me.cFNQuantitySale.Name = "cFNQuantitySale"
        Me.cFNQuantitySale.OptionsColumn.AllowEdit = False
        Me.cFNQuantitySale.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantitySale", "{0:n0}")})
        Me.cFNQuantitySale.Visible = True
        Me.cFNQuantitySale.VisibleIndex = 7
        Me.cFNQuantitySale.Width = 92
        '
        'cFNQuantityBal
        '
        Me.cFNQuantityBal.Caption = "FNQuantityBal"
        Me.cFNQuantityBal.DisplayFormat.FormatString = "{0:n0}"
        Me.cFNQuantityBal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQuantityBal.FieldName = "FNQuantityBal"
        Me.cFNQuantityBal.Name = "cFNQuantityBal"
        Me.cFNQuantityBal.OptionsColumn.AllowEdit = False
        Me.cFNQuantityBal.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantityBal", "{0:n0}")})
        Me.cFNQuantityBal.Visible = True
        Me.cFNQuantityBal.VisibleIndex = 8
        Me.cFNQuantityBal.Width = 93
        '
        'cFTWHFGCode
        '
        Me.cFTWHFGCode.Caption = "FTWHFGCode"
        Me.cFTWHFGCode.FieldName = "FTWHFGCode"
        Me.cFTWHFGCode.Name = "cFTWHFGCode"
        Me.cFTWHFGCode.OptionsColumn.AllowEdit = False
        Me.cFTWHFGCode.Visible = True
        Me.cFTWHFGCode.VisibleIndex = 0
        Me.cFTWHFGCode.Width = 106
        '
        'cFTPORef
        '
        Me.cFTPORef.Caption = "FTPORef"
        Me.cFTPORef.FieldName = "FTPORef"
        Me.cFTPORef.Name = "cFTPORef"
        Me.cFTPORef.OptionsColumn.AllowEdit = False
        Me.cFTPORef.Visible = True
        Me.cFTPORef.VisibleIndex = 1
        Me.cFTPORef.Width = 94
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsaveweightpack)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(740, 169)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(414, 91)
        Me.ogbmainprocbutton.TabIndex = 396
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(37, 54)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(111, 28)
        Me.ocmload.TabIndex = 330
        Me.ocmload.Text = "Load Data"
        '
        'ocmsaveweightpack
        '
        Me.ocmsaveweightpack.Location = New System.Drawing.Point(246, 16)
        Me.ocmsaveweightpack.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsaveweightpack.Name = "ocmsaveweightpack"
        Me.ocmsaveweightpack.Size = New System.Drawing.Size(97, 31)
        Me.ocmsaveweightpack.TabIndex = 105
        Me.ocmsaveweightpack.TabStop = False
        Me.ocmsaveweightpack.Tag = "2|"
        Me.ocmsaveweightpack.Text = "saveweight"
        Me.ocmsaveweightpack.Visible = False
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ocmexit.Location = New System.Drawing.Point(37, 16)
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
        Me.ocmclear.Location = New System.Drawing.Point(176, 16)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(63, 31)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'wIssueTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1261, 726)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogrpdetail)
        Me.Controls.Add(Me.hideContainerTop)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wIssueTracking"
        Me.Text = "wIssueTracking"
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerTop.ResumeLayout(False)
        Me.oCriteria.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHIdFGTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHIdFG.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNoTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogrpdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpdetail.ResumeLayout(False)
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents oCriteria As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ogrpdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTColorWay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTSizeBreakDown As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantityIss As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantitySale As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantityBal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysWHFGId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysWHFGIdTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysWHIdFGTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysWHIdFG As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTOrderNoTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTOrderNoTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsaveweightpack As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cFTWHFGCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents hideContainerTop As DevExpress.XtraBars.Docking.AutoHideContainer
End Class
