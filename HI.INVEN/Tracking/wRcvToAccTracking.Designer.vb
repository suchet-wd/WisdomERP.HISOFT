<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wRcvToAccTracking
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wRcvToAccTracking))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.DockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerTop = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.oCriteria = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTPurchaseNoTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTPurchaseNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.EFTDateRcv = New DevExpress.XtraEditors.DateEdit()
        Me.FTPurchaseNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.SFTDateRcv_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.EFTDateInvoice = New DevExpress.XtraEditors.DateEdit()
        Me.SFTDateInvoice_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTPurchaseNoTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.EFTDateRcv_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.EFTDateDoc = New DevExpress.XtraEditors.DateEdit()
        Me.EFTDateInvoice_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.SFTDateRcv = New DevExpress.XtraEditors.DateEdit()
        Me.SFTDateDoc_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.SFTDateInvoice = New DevExpress.XtraEditors.DateEdit()
        Me.EFTDateDoc_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.SFTDateDoc = New DevExpress.XtraEditors.DateEdit()
        Me.ogcDetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFDInvoiceDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTInvoiceNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTSuplCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTSuplName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTReceiveNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDReceiveDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTRcvToAccNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDRcvToAccDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTRcvToAccBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTMailToAccountDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTMailToStockBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTMailToStockDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerTop.SuspendLayout()
        Me.oCriteria.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTPurchaseNoTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPurchaseNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EFTDateRcv.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EFTDateRcv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EFTDateInvoice.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EFTDateInvoice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EFTDateDoc.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EFTDateDoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SFTDateRcv.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SFTDateRcv.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SFTDateInvoice.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SFTDateInvoice.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SFTDateDoc.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SFTDateDoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.hideContainerTop.Size = New System.Drawing.Size(1247, 40)
        '
        'oCriteria
        '
        Me.oCriteria.Controls.Add(Me.DockPanel1_Container)
        Me.oCriteria.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.oCriteria.ID = New System.Guid("23a23c29-dc55-4c3f-a11d-ec0955455cc2")
        Me.oCriteria.Image = CType(resources.GetObject("oCriteria.Image"), System.Drawing.Image)
        Me.oCriteria.Location = New System.Drawing.Point(0, 0)
        Me.oCriteria.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oCriteria.Name = "oCriteria"
        Me.oCriteria.Options.AllowDockBottom = False
        Me.oCriteria.Options.AllowDockFill = False
        Me.oCriteria.Options.FloatOnDblClick = False
        Me.oCriteria.Options.ShowCloseButton = False
        Me.oCriteria.OriginalSize = New System.Drawing.Size(200, 128)
        Me.oCriteria.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.oCriteria.SavedIndex = 0
        Me.oCriteria.Size = New System.Drawing.Size(1247, 128)
        Me.oCriteria.Text = "Criteria"
        Me.oCriteria.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTPurchaseNoTo)
        Me.DockPanel1_Container.Controls.Add(Me.FTPurchaseNo)
        Me.DockPanel1_Container.Controls.Add(Me.EFTDateRcv)
        Me.DockPanel1_Container.Controls.Add(Me.FTPurchaseNo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.SFTDateRcv_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.EFTDateInvoice)
        Me.DockPanel1_Container.Controls.Add(Me.SFTDateInvoice_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTPurchaseNoTo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.EFTDateRcv_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.EFTDateDoc)
        Me.DockPanel1_Container.Controls.Add(Me.EFTDateInvoice_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.SFTDateRcv)
        Me.DockPanel1_Container.Controls.Add(Me.SFTDateDoc_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.SFTDateInvoice)
        Me.DockPanel1_Container.Controls.Add(Me.EFTDateDoc_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.SFTDateDoc)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(5, 27)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1237, 94)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTPurchaseNoTo
        '
        Me.FTPurchaseNoTo.Location = New System.Drawing.Point(526, 87)
        Me.FTPurchaseNoTo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPurchaseNoTo.Name = "FTPurchaseNoTo"
        Me.FTPurchaseNoTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "198", Nothing, True)})
        Me.FTPurchaseNoTo.Size = New System.Drawing.Size(173, 22)
        Me.FTPurchaseNoTo.TabIndex = 7
        '
        'FTPurchaseNo
        '
        Me.FTPurchaseNo.Location = New System.Drawing.Point(177, 87)
        Me.FTPurchaseNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPurchaseNo.Name = "FTPurchaseNo"
        Me.FTPurchaseNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "204", Nothing, True)})
        Me.FTPurchaseNo.Size = New System.Drawing.Size(166, 22)
        Me.FTPurchaseNo.TabIndex = 6
        '
        'EFTDateRcv
        '
        Me.EFTDateRcv.EditValue = Nothing
        Me.EFTDateRcv.EnterMoveNextControl = True
        Me.EFTDateRcv.Location = New System.Drawing.Point(526, 60)
        Me.EFTDateRcv.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.EFTDateRcv.Name = "EFTDateRcv"
        Me.EFTDateRcv.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.EFTDateRcv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.EFTDateRcv.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.EFTDateRcv.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.EFTDateRcv.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.EFTDateRcv.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.EFTDateRcv.Properties.NullDate = ""
        Me.EFTDateRcv.Size = New System.Drawing.Size(140, 22)
        Me.EFTDateRcv.TabIndex = 5
        Me.EFTDateRcv.Tag = "2|"
        '
        'FTPurchaseNo_lbl
        '
        Me.FTPurchaseNo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTPurchaseNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTPurchaseNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPurchaseNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPurchaseNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPurchaseNo_lbl.Location = New System.Drawing.Point(3, 87)
        Me.FTPurchaseNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPurchaseNo_lbl.Name = "FTPurchaseNo_lbl"
        Me.FTPurchaseNo_lbl.Size = New System.Drawing.Size(170, 25)
        Me.FTPurchaseNo_lbl.TabIndex = 511
        Me.FTPurchaseNo_lbl.Tag = "2|"
        Me.FTPurchaseNo_lbl.Text = "FTPurchaseNo :"
        '
        'SFTDateRcv_lbl
        '
        Me.SFTDateRcv_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.SFTDateRcv_lbl.Appearance.Options.UseForeColor = True
        Me.SFTDateRcv_lbl.Appearance.Options.UseTextOptions = True
        Me.SFTDateRcv_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFTDateRcv_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.SFTDateRcv_lbl.Location = New System.Drawing.Point(3, 60)
        Me.SFTDateRcv_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SFTDateRcv_lbl.Name = "SFTDateRcv_lbl"
        Me.SFTDateRcv_lbl.Size = New System.Drawing.Size(170, 25)
        Me.SFTDateRcv_lbl.TabIndex = 511
        Me.SFTDateRcv_lbl.Tag = "2|"
        Me.SFTDateRcv_lbl.Text = "SFTDateRcv :"
        '
        'EFTDateInvoice
        '
        Me.EFTDateInvoice.EditValue = Nothing
        Me.EFTDateInvoice.EnterMoveNextControl = True
        Me.EFTDateInvoice.Location = New System.Drawing.Point(526, 32)
        Me.EFTDateInvoice.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.EFTDateInvoice.Name = "EFTDateInvoice"
        Me.EFTDateInvoice.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.EFTDateInvoice.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.EFTDateInvoice.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.EFTDateInvoice.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.EFTDateInvoice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.EFTDateInvoice.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.EFTDateInvoice.Properties.NullDate = ""
        Me.EFTDateInvoice.Size = New System.Drawing.Size(140, 22)
        Me.EFTDateInvoice.TabIndex = 3
        Me.EFTDateInvoice.Tag = "2|"
        '
        'SFTDateInvoice_lbl
        '
        Me.SFTDateInvoice_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.SFTDateInvoice_lbl.Appearance.Options.UseForeColor = True
        Me.SFTDateInvoice_lbl.Appearance.Options.UseTextOptions = True
        Me.SFTDateInvoice_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFTDateInvoice_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.SFTDateInvoice_lbl.Location = New System.Drawing.Point(3, 33)
        Me.SFTDateInvoice_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SFTDateInvoice_lbl.Name = "SFTDateInvoice_lbl"
        Me.SFTDateInvoice_lbl.Size = New System.Drawing.Size(170, 25)
        Me.SFTDateInvoice_lbl.TabIndex = 511
        Me.SFTDateInvoice_lbl.Tag = "2|"
        Me.SFTDateInvoice_lbl.Text = "SFTDateInvoice :"
        '
        'FTPurchaseNoTo_lbl
        '
        Me.FTPurchaseNoTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTPurchaseNoTo_lbl.Appearance.Options.UseForeColor = True
        Me.FTPurchaseNoTo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPurchaseNoTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPurchaseNoTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPurchaseNoTo_lbl.Location = New System.Drawing.Point(357, 86)
        Me.FTPurchaseNoTo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPurchaseNoTo_lbl.Name = "FTPurchaseNoTo_lbl"
        Me.FTPurchaseNoTo_lbl.Size = New System.Drawing.Size(167, 25)
        Me.FTPurchaseNoTo_lbl.TabIndex = 513
        Me.FTPurchaseNoTo_lbl.Tag = "2|"
        Me.FTPurchaseNoTo_lbl.Text = "EFTDateRcv :"
        '
        'EFTDateRcv_lbl
        '
        Me.EFTDateRcv_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.EFTDateRcv_lbl.Appearance.Options.UseForeColor = True
        Me.EFTDateRcv_lbl.Appearance.Options.UseTextOptions = True
        Me.EFTDateRcv_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.EFTDateRcv_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.EFTDateRcv_lbl.Location = New System.Drawing.Point(356, 62)
        Me.EFTDateRcv_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.EFTDateRcv_lbl.Name = "EFTDateRcv_lbl"
        Me.EFTDateRcv_lbl.Size = New System.Drawing.Size(167, 25)
        Me.EFTDateRcv_lbl.TabIndex = 513
        Me.EFTDateRcv_lbl.Tag = "2|"
        Me.EFTDateRcv_lbl.Text = "EFTDateRcv :"
        '
        'EFTDateDoc
        '
        Me.EFTDateDoc.EditValue = Nothing
        Me.EFTDateDoc.EnterMoveNextControl = True
        Me.EFTDateDoc.Location = New System.Drawing.Point(526, 4)
        Me.EFTDateDoc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.EFTDateDoc.Name = "EFTDateDoc"
        Me.EFTDateDoc.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.EFTDateDoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.EFTDateDoc.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.EFTDateDoc.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.EFTDateDoc.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.EFTDateDoc.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.EFTDateDoc.Properties.NullDate = ""
        Me.EFTDateDoc.Size = New System.Drawing.Size(140, 22)
        Me.EFTDateDoc.TabIndex = 1
        Me.EFTDateDoc.Tag = "2|"
        '
        'EFTDateInvoice_lbl
        '
        Me.EFTDateInvoice_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.EFTDateInvoice_lbl.Appearance.Options.UseForeColor = True
        Me.EFTDateInvoice_lbl.Appearance.Options.UseTextOptions = True
        Me.EFTDateInvoice_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.EFTDateInvoice_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.EFTDateInvoice_lbl.Location = New System.Drawing.Point(356, 33)
        Me.EFTDateInvoice_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.EFTDateInvoice_lbl.Name = "EFTDateInvoice_lbl"
        Me.EFTDateInvoice_lbl.Size = New System.Drawing.Size(167, 25)
        Me.EFTDateInvoice_lbl.TabIndex = 513
        Me.EFTDateInvoice_lbl.Tag = "2|"
        Me.EFTDateInvoice_lbl.Text = "EFTDateInvoice :"
        '
        'SFTDateRcv
        '
        Me.SFTDateRcv.EditValue = Nothing
        Me.SFTDateRcv.EnterMoveNextControl = True
        Me.SFTDateRcv.Location = New System.Drawing.Point(177, 59)
        Me.SFTDateRcv.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SFTDateRcv.Name = "SFTDateRcv"
        Me.SFTDateRcv.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFTDateRcv.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SFTDateRcv.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.SFTDateRcv.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.SFTDateRcv.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.SFTDateRcv.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.SFTDateRcv.Properties.NullDate = ""
        Me.SFTDateRcv.Size = New System.Drawing.Size(131, 22)
        Me.SFTDateRcv.TabIndex = 4
        Me.SFTDateRcv.Tag = "2|"
        '
        'SFTDateDoc_lbl
        '
        Me.SFTDateDoc_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.SFTDateDoc_lbl.Appearance.Options.UseForeColor = True
        Me.SFTDateDoc_lbl.Appearance.Options.UseTextOptions = True
        Me.SFTDateDoc_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFTDateDoc_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.SFTDateDoc_lbl.Location = New System.Drawing.Point(3, 4)
        Me.SFTDateDoc_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SFTDateDoc_lbl.Name = "SFTDateDoc_lbl"
        Me.SFTDateDoc_lbl.Size = New System.Drawing.Size(170, 25)
        Me.SFTDateDoc_lbl.TabIndex = 511
        Me.SFTDateDoc_lbl.Tag = "2|"
        Me.SFTDateDoc_lbl.Text = "SFTDateDoc :"
        '
        'SFTDateInvoice
        '
        Me.SFTDateInvoice.EditValue = Nothing
        Me.SFTDateInvoice.EnterMoveNextControl = True
        Me.SFTDateInvoice.Location = New System.Drawing.Point(177, 32)
        Me.SFTDateInvoice.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SFTDateInvoice.Name = "SFTDateInvoice"
        Me.SFTDateInvoice.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFTDateInvoice.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SFTDateInvoice.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.SFTDateInvoice.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.SFTDateInvoice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.SFTDateInvoice.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.SFTDateInvoice.Properties.NullDate = ""
        Me.SFTDateInvoice.Size = New System.Drawing.Size(131, 22)
        Me.SFTDateInvoice.TabIndex = 2
        Me.SFTDateInvoice.Tag = "2|"
        '
        'EFTDateDoc_lbl
        '
        Me.EFTDateDoc_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.EFTDateDoc_lbl.Appearance.Options.UseForeColor = True
        Me.EFTDateDoc_lbl.Appearance.Options.UseTextOptions = True
        Me.EFTDateDoc_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.EFTDateDoc_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.EFTDateDoc_lbl.Location = New System.Drawing.Point(355, 4)
        Me.EFTDateDoc_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.EFTDateDoc_lbl.Name = "EFTDateDoc_lbl"
        Me.EFTDateDoc_lbl.Size = New System.Drawing.Size(167, 25)
        Me.EFTDateDoc_lbl.TabIndex = 513
        Me.EFTDateDoc_lbl.Tag = "2|"
        Me.EFTDateDoc_lbl.Text = "EFTDateDoc :"
        '
        'SFTDateDoc
        '
        Me.SFTDateDoc.EditValue = Nothing
        Me.SFTDateDoc.EnterMoveNextControl = True
        Me.SFTDateDoc.Location = New System.Drawing.Point(177, 4)
        Me.SFTDateDoc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SFTDateDoc.Name = "SFTDateDoc"
        Me.SFTDateDoc.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFTDateDoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SFTDateDoc.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.SFTDateDoc.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.SFTDateDoc.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.SFTDateDoc.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.SFTDateDoc.Properties.NullDate = ""
        Me.SFTDateDoc.Size = New System.Drawing.Size(131, 22)
        Me.SFTDateDoc.TabIndex = 0
        Me.SFTDateDoc.Tag = "2|"
        '
        'ogcDetail
        '
        Me.ogcDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcDetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDetail.Location = New System.Drawing.Point(0, 40)
        Me.ogcDetail.MainView = Me.ogvDetail
        Me.ogcDetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDetail.Name = "ogcDetail"
        Me.ogcDetail.Size = New System.Drawing.Size(1247, 666)
        Me.ogcDetail.TabIndex = 2
        Me.ogcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDetail})
        '
        'ogvDetail
        '
        Me.ogvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFDInvoiceDate, Me.cFTInvoiceNo, Me.cFTSuplCode, Me.cFTSuplName, Me.cFTReceiveNo, Me.cFDReceiveDate, Me.cFTRcvToAccNo, Me.cFDRcvToAccDate, Me.cFTPurchaseNo, Me.cFTRcvToAccBy, Me.cFTMailToAccountDate, Me.cFTMailToStockBy, Me.cFTMailToStockDate, Me.cFTRemark})
        Me.ogvDetail.GridControl = Me.ogcDetail
        Me.ogvDetail.Name = "ogvDetail"
        Me.ogvDetail.OptionsView.ColumnAutoWidth = False
        Me.ogvDetail.OptionsView.ShowGroupPanel = False
        '
        'cFDInvoiceDate
        '
        Me.cFDInvoiceDate.Caption = "FDInvoiceDate"
        Me.cFDInvoiceDate.FieldName = "FDInvoiceDate"
        Me.cFDInvoiceDate.Name = "cFDInvoiceDate"
        Me.cFDInvoiceDate.OptionsColumn.AllowEdit = False
        Me.cFDInvoiceDate.Visible = True
        Me.cFDInvoiceDate.VisibleIndex = 0
        Me.cFDInvoiceDate.Width = 108
        '
        'cFTInvoiceNo
        '
        Me.cFTInvoiceNo.Caption = "FTInvoiceNo"
        Me.cFTInvoiceNo.FieldName = "FTInvoiceNo"
        Me.cFTInvoiceNo.Name = "cFTInvoiceNo"
        Me.cFTInvoiceNo.OptionsColumn.AllowEdit = False
        Me.cFTInvoiceNo.Visible = True
        Me.cFTInvoiceNo.VisibleIndex = 1
        Me.cFTInvoiceNo.Width = 104
        '
        'cFTSuplCode
        '
        Me.cFTSuplCode.Caption = "FTSuplCode"
        Me.cFTSuplCode.FieldName = "FTSuplCode"
        Me.cFTSuplCode.Name = "cFTSuplCode"
        Me.cFTSuplCode.OptionsColumn.AllowEdit = False
        Me.cFTSuplCode.Visible = True
        Me.cFTSuplCode.VisibleIndex = 2
        Me.cFTSuplCode.Width = 99
        '
        'cFTSuplName
        '
        Me.cFTSuplName.Caption = "FTSuplName"
        Me.cFTSuplName.FieldName = "FTSuplName"
        Me.cFTSuplName.Name = "cFTSuplName"
        Me.cFTSuplName.OptionsColumn.AllowEdit = False
        Me.cFTSuplName.Visible = True
        Me.cFTSuplName.VisibleIndex = 3
        Me.cFTSuplName.Width = 182
        '
        'cFTReceiveNo
        '
        Me.cFTReceiveNo.Caption = "FTReceiveNo"
        Me.cFTReceiveNo.FieldName = "FTReceiveNo"
        Me.cFTReceiveNo.Name = "cFTReceiveNo"
        Me.cFTReceiveNo.OptionsColumn.AllowEdit = False
        Me.cFTReceiveNo.Visible = True
        Me.cFTReceiveNo.VisibleIndex = 4
        Me.cFTReceiveNo.Width = 126
        '
        'cFDReceiveDate
        '
        Me.cFDReceiveDate.Caption = "FDReceiveDate"
        Me.cFDReceiveDate.FieldName = "FDReceiveDate"
        Me.cFDReceiveDate.Name = "cFDReceiveDate"
        Me.cFDReceiveDate.OptionsColumn.AllowEdit = False
        Me.cFDReceiveDate.Visible = True
        Me.cFDReceiveDate.VisibleIndex = 5
        Me.cFDReceiveDate.Width = 90
        '
        'cFTRcvToAccNo
        '
        Me.cFTRcvToAccNo.Caption = "FTRcvToAccNo"
        Me.cFTRcvToAccNo.FieldName = "FTRcvToAccNo"
        Me.cFTRcvToAccNo.Name = "cFTRcvToAccNo"
        Me.cFTRcvToAccNo.OptionsColumn.AllowEdit = False
        Me.cFTRcvToAccNo.Visible = True
        Me.cFTRcvToAccNo.VisibleIndex = 6
        Me.cFTRcvToAccNo.Width = 145
        '
        'cFDRcvToAccDate
        '
        Me.cFDRcvToAccDate.Caption = "FDRcvToAccDate"
        Me.cFDRcvToAccDate.FieldName = "FDRcvToAccDate"
        Me.cFDRcvToAccDate.Name = "cFDRcvToAccDate"
        Me.cFDRcvToAccDate.OptionsColumn.AllowEdit = False
        Me.cFDRcvToAccDate.Visible = True
        Me.cFDRcvToAccDate.VisibleIndex = 7
        Me.cFDRcvToAccDate.Width = 90
        '
        'cFTPurchaseNo
        '
        Me.cFTPurchaseNo.Caption = "FTPurchaseNo"
        Me.cFTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.cFTPurchaseNo.Name = "cFTPurchaseNo"
        Me.cFTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.cFTPurchaseNo.Visible = True
        Me.cFTPurchaseNo.VisibleIndex = 8
        Me.cFTPurchaseNo.Width = 116
        '
        'cFTRcvToAccBy
        '
        Me.cFTRcvToAccBy.Caption = "FTRcvToAccBy"
        Me.cFTRcvToAccBy.FieldName = "FTRcvToAccBy"
        Me.cFTRcvToAccBy.Name = "cFTRcvToAccBy"
        Me.cFTRcvToAccBy.OptionsColumn.AllowEdit = False
        Me.cFTRcvToAccBy.Visible = True
        Me.cFTRcvToAccBy.VisibleIndex = 9
        Me.cFTRcvToAccBy.Width = 132
        '
        'cFTMailToAccountDate
        '
        Me.cFTMailToAccountDate.Caption = "FTMailToAccountDate"
        Me.cFTMailToAccountDate.FieldName = "FTMailToAccountDate"
        Me.cFTMailToAccountDate.Name = "cFTMailToAccountDate"
        Me.cFTMailToAccountDate.OptionsColumn.AllowEdit = False
        Me.cFTMailToAccountDate.Visible = True
        Me.cFTMailToAccountDate.VisibleIndex = 10
        Me.cFTMailToAccountDate.Width = 98
        '
        'cFTMailToStockBy
        '
        Me.cFTMailToStockBy.Caption = "FTMailToStockBy"
        Me.cFTMailToStockBy.FieldName = "FTMailToStockBy"
        Me.cFTMailToStockBy.Name = "cFTMailToStockBy"
        Me.cFTMailToStockBy.OptionsColumn.AllowEdit = False
        Me.cFTMailToStockBy.Visible = True
        Me.cFTMailToStockBy.VisibleIndex = 11
        Me.cFTMailToStockBy.Width = 97
        '
        'cFTMailToStockDate
        '
        Me.cFTMailToStockDate.Caption = "FTMailToStockDate"
        Me.cFTMailToStockDate.FieldName = "FTMailToStockDate"
        Me.cFTMailToStockDate.Name = "cFTMailToStockDate"
        Me.cFTMailToStockDate.OptionsColumn.AllowEdit = False
        Me.cFTMailToStockDate.Visible = True
        Me.cFTMailToStockDate.VisibleIndex = 12
        Me.cFTMailToStockDate.Width = 100
        '
        'cFTRemark
        '
        Me.cFTRemark.Caption = "FTRemark"
        Me.cFTRemark.FieldName = "FTRemark"
        Me.cFTRemark.Name = "cFTRemark"
        Me.cFTRemark.OptionsColumn.AllowEdit = False
        Me.cFTRemark.Visible = True
        Me.cFTRemark.VisibleIndex = 13
        Me.cFTRemark.Width = 295
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(272, 332)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(702, 42)
        Me.ogbmainprocbutton.TabIndex = 393
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(517, 9)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(20, 6)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(111, 31)
        Me.ocmload.TabIndex = 93
        Me.ocmload.TabStop = False
        Me.ocmload.Tag = "2|"
        Me.ocmload.Text = "LoadData"
        '
        'wRcvToAccTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1247, 706)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogcDetail)
        Me.Controls.Add(Me.hideContainerTop)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wRcvToAccTracking"
        Me.Text = "wRcvToAccTracking"
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerTop.ResumeLayout(False)
        Me.oCriteria.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTPurchaseNoTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPurchaseNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EFTDateRcv.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EFTDateRcv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EFTDateInvoice.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EFTDateInvoice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EFTDateDoc.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EFTDateDoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SFTDateRcv.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SFTDateRcv.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SFTDateInvoice.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SFTDateInvoice.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SFTDateDoc.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SFTDateDoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents oCriteria As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ogcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFDInvoiceDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTInvoiceNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTSuplCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTSuplName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTReceiveNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDReceiveDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTRcvToAccNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDRcvToAccDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTRcvToAccBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTMailToAccountDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTMailToStockBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTMailToStockDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents EFTDateInvoice As DevExpress.XtraEditors.DateEdit
    Friend WithEvents SFTDateInvoice_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents EFTDateDoc As DevExpress.XtraEditors.DateEdit
    Friend WithEvents EFTDateInvoice_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SFTDateDoc_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SFTDateInvoice As DevExpress.XtraEditors.DateEdit
    Friend WithEvents EFTDateDoc_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SFTDateDoc As DevExpress.XtraEditors.DateEdit
    Friend WithEvents EFTDateRcv As DevExpress.XtraEditors.DateEdit
    Friend WithEvents SFTDateRcv_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents EFTDateRcv_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SFTDateRcv As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTPurchaseNoTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTPurchaseNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTPurchaseNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPurchaseNoTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents hideContainerTop As DevExpress.XtraBars.Docking.AutoHideContainer
End Class
