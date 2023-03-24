<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wScanBarcodeMergeTracking
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
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmremove = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsaveweightpack = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ogrpbarcode = New DevExpress.XtraEditors.GroupControl()
        Me.FNHSysWHLocId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysWHLocId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysWHFGId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysWHLocId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysWHFGId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysWHFGId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogrpfinishgoods = New DevExpress.XtraEditors.GroupControl()
        Me.oGFTSelectAll = New DevExpress.XtraEditors.CheckEdit()
        Me.ogcwarehouse = New DevExpress.XtraGrid.GridControl()
        Me.ogvwarehouse = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryGFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GFTBarCodePallet = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFTCustomerPO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFTPOLine = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogrpbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpbarcode.SuspendLayout()
        CType(Me.FNHSysWHLocId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHLocId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHFGId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHFGId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogrpfinishgoods, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpfinishgoods.SuspendLayout()
        CType(Me.oGFTSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcwarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvwarehouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryGFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmremove)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsaveweightpack)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(318, 111)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(568, 141)
        Me.ogbmainprocbutton.TabIndex = 393
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(250, 55)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(68, 31)
        Me.ocmpreview.TabIndex = 331
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "PREVIEW"
        '
        'ocmremove
        '
        Me.ocmremove.Location = New System.Drawing.Point(27, 90)
        Me.ocmremove.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmremove.Name = "ocmremove"
        Me.ocmremove.Size = New System.Drawing.Size(136, 28)
        Me.ocmremove.TabIndex = 330
        Me.ocmremove.Text = "Delete"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(27, 54)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(136, 28)
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
        Me.ocmexit.Location = New System.Drawing.Point(52, 16)
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
        'ogrpbarcode
        '
        Me.ogrpbarcode.Controls.Add(Me.FNHSysWHLocId_None)
        Me.ogrpbarcode.Controls.Add(Me.FNHSysWHLocId)
        Me.ogrpbarcode.Controls.Add(Me.FNHSysWHFGId_None)
        Me.ogrpbarcode.Controls.Add(Me.FNHSysWHLocId_lbl)
        Me.ogrpbarcode.Controls.Add(Me.FNHSysWHFGId)
        Me.ogrpbarcode.Controls.Add(Me.FNHSysWHFGId_lbl)
        Me.ogrpbarcode.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogrpbarcode.Location = New System.Drawing.Point(0, 0)
        Me.ogrpbarcode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpbarcode.Name = "ogrpbarcode"
        Me.ogrpbarcode.Size = New System.Drawing.Size(1277, 106)
        Me.ogrpbarcode.TabIndex = 0
        Me.ogrpbarcode.Text = "Scan Barcode In Warehouse Finish Goods"
        '
        'FNHSysWHLocId_None
        '
        Me.FNHSysWHLocId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysWHLocId_None.EnterMoveNextControl = True
        Me.FNHSysWHLocId_None.Location = New System.Drawing.Point(371, 54)
        Me.FNHSysWHLocId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHLocId_None.Name = "FNHSysWHLocId_None"
        Me.FNHSysWHLocId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysWHLocId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysWHLocId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysWHLocId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysWHLocId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHLocId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHLocId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysWHLocId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysWHLocId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysWHLocId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHLocId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysWHLocId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysWHLocId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHLocId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHLocId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysWHLocId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysWHLocId_None.Properties.ReadOnly = True
        Me.FNHSysWHLocId_None.Size = New System.Drawing.Size(891, 22)
        Me.FNHSysWHLocId_None.TabIndex = 303
        Me.FNHSysWHLocId_None.TabStop = False
        Me.FNHSysWHLocId_None.Tag = "2|"
        '
        'FNHSysWHLocId
        '
        Me.FNHSysWHLocId.Location = New System.Drawing.Point(222, 54)
        Me.FNHSysWHLocId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHLocId.Name = "FNHSysWHLocId"
        Me.FNHSysWHLocId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "492", Nothing, True)})
        Me.FNHSysWHLocId.Size = New System.Drawing.Size(146, 22)
        Me.FNHSysWHLocId.TabIndex = 302
        Me.FNHSysWHLocId.Tag = "2|"
        '
        'FNHSysWHFGId_None
        '
        Me.FNHSysWHFGId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysWHFGId_None.EnterMoveNextControl = True
        Me.FNHSysWHFGId_None.Location = New System.Drawing.Point(371, 28)
        Me.FNHSysWHFGId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHFGId_None.Name = "FNHSysWHFGId_None"
        Me.FNHSysWHFGId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysWHFGId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysWHFGId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysWHFGId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysWHFGId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHFGId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHFGId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysWHFGId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysWHFGId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysWHFGId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHFGId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysWHFGId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysWHFGId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHFGId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHFGId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysWHFGId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysWHFGId_None.Properties.ReadOnly = True
        Me.FNHSysWHFGId_None.Size = New System.Drawing.Size(891, 22)
        Me.FNHSysWHFGId_None.TabIndex = 303
        Me.FNHSysWHFGId_None.TabStop = False
        Me.FNHSysWHFGId_None.Tag = "2|"
        '
        'FNHSysWHLocId_lbl
        '
        Me.FNHSysWHLocId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHLocId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysWHLocId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysWHLocId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysWHLocId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysWHLocId_lbl.Location = New System.Drawing.Point(10, 54)
        Me.FNHSysWHLocId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHLocId_lbl.Name = "FNHSysWHLocId_lbl"
        Me.FNHSysWHLocId_lbl.Size = New System.Drawing.Size(204, 23)
        Me.FNHSysWHLocId_lbl.TabIndex = 301
        Me.FNHSysWHLocId_lbl.Tag = "2|"
        Me.FNHSysWHLocId_lbl.Text = "FNHSysWHLocFGId :"
        '
        'FNHSysWHFGId
        '
        Me.FNHSysWHFGId.Location = New System.Drawing.Point(222, 28)
        Me.FNHSysWHFGId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHFGId.Name = "FNHSysWHFGId"
        Me.FNHSysWHFGId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "282", Nothing, True)})
        Me.FNHSysWHFGId.Size = New System.Drawing.Size(146, 22)
        Me.FNHSysWHFGId.TabIndex = 302
        Me.FNHSysWHFGId.Tag = "2|"
        '
        'FNHSysWHFGId_lbl
        '
        Me.FNHSysWHFGId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHFGId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysWHFGId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysWHFGId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysWHFGId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysWHFGId_lbl.Location = New System.Drawing.Point(10, 28)
        Me.FNHSysWHFGId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHFGId_lbl.Name = "FNHSysWHFGId_lbl"
        Me.FNHSysWHFGId_lbl.Size = New System.Drawing.Size(204, 23)
        Me.FNHSysWHFGId_lbl.TabIndex = 301
        Me.FNHSysWHFGId_lbl.Tag = "2|"
        Me.FNHSysWHFGId_lbl.Text = "FNHSysWHFGId :"
        '
        'ogrpfinishgoods
        '
        Me.ogrpfinishgoods.AppearanceCaption.Options.UseTextOptions = True
        Me.ogrpfinishgoods.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogrpfinishgoods.Controls.Add(Me.oGFTSelectAll)
        Me.ogrpfinishgoods.Controls.Add(Me.ogbmainprocbutton)
        Me.ogrpfinishgoods.Controls.Add(Me.ogcwarehouse)
        Me.ogrpfinishgoods.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrpfinishgoods.Location = New System.Drawing.Point(0, 106)
        Me.ogrpfinishgoods.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpfinishgoods.Name = "ogrpfinishgoods"
        Me.ogrpfinishgoods.Size = New System.Drawing.Size(1277, 640)
        Me.ogrpfinishgoods.TabIndex = 0
        Me.ogrpfinishgoods.Text = "Finish Goods In Warehouse To Day"
        '
        'oGFTSelectAll
        '
        Me.oGFTSelectAll.Location = New System.Drawing.Point(7, 2)
        Me.oGFTSelectAll.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oGFTSelectAll.Name = "oGFTSelectAll"
        Me.oGFTSelectAll.Properties.Caption = "Select All"
        Me.oGFTSelectAll.Size = New System.Drawing.Size(173, 20)
        Me.oGFTSelectAll.TabIndex = 1
        Me.oGFTSelectAll.Visible = False
        '
        'ogcwarehouse
        '
        Me.ogcwarehouse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcwarehouse.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcwarehouse.Location = New System.Drawing.Point(2, 25)
        Me.ogcwarehouse.MainView = Me.ogvwarehouse
        Me.ogcwarehouse.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcwarehouse.Name = "ogcwarehouse"
        Me.ogcwarehouse.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryGFTSelect})
        Me.ogcwarehouse.Size = New System.Drawing.Size(1273, 613)
        Me.ogcwarehouse.TabIndex = 0
        Me.ogcwarehouse.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvwarehouse})
        '
        'ogvwarehouse
        '
        Me.ogvwarehouse.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GFTBarCodePallet, Me.GFTCustomerPO, Me.GFTPOLine})
        Me.ogvwarehouse.GridControl = Me.ogcwarehouse
        Me.ogvwarehouse.Name = "ogvwarehouse"
        Me.ogvwarehouse.OptionsView.ColumnAutoWidth = False
        Me.ogvwarehouse.OptionsView.ShowGroupPanel = False
        '
        'RepositoryGFTSelect
        '
        Me.RepositoryGFTSelect.AutoHeight = False
        Me.RepositoryGFTSelect.Caption = "Check"
        Me.RepositoryGFTSelect.Name = "RepositoryGFTSelect"
        Me.RepositoryGFTSelect.ValueChecked = "1"
        Me.RepositoryGFTSelect.ValueUnchecked = "0"
        '
        'GFTBarCodePallet
        '
        Me.GFTBarCodePallet.Caption = "FTBarCodePallet"
        Me.GFTBarCodePallet.FieldName = "FTBarCodePallet"
        Me.GFTBarCodePallet.Name = "GFTBarCodePallet"
        Me.GFTBarCodePallet.OptionsColumn.AllowEdit = False
        Me.GFTBarCodePallet.Visible = True
        Me.GFTBarCodePallet.VisibleIndex = 0
        Me.GFTBarCodePallet.Width = 266
        '
        'GFTCustomerPO
        '
        Me.GFTCustomerPO.Caption = "FTCustomerPO"
        Me.GFTCustomerPO.FieldName = "FTFTCustomerPO"
        Me.GFTCustomerPO.Name = "GFTCustomerPO"
        Me.GFTCustomerPO.OptionsColumn.AllowEdit = False
        Me.GFTCustomerPO.Visible = True
        Me.GFTCustomerPO.VisibleIndex = 1
        Me.GFTCustomerPO.Width = 133
        '
        'GFTPOLine
        '
        Me.GFTPOLine.Caption = "FTPOLine"
        Me.GFTPOLine.FieldName = "FTPOLine"
        Me.GFTPOLine.Name = "GFTPOLine"
        Me.GFTPOLine.OptionsColumn.AllowEdit = False
        Me.GFTPOLine.Visible = True
        Me.GFTPOLine.VisibleIndex = 2
        Me.GFTPOLine.Width = 135
        '
        'wScanBarcodeMergeTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1277, 746)
        Me.Controls.Add(Me.ogrpfinishgoods)
        Me.Controls.Add(Me.ogrpbarcode)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wScanBarcodeMergeTracking"
        Me.Text = "wScanBarcodeMergeTracking"
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogrpbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpbarcode.ResumeLayout(False)
        CType(Me.FNHSysWHLocId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHLocId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHFGId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHFGId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogrpfinishgoods, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpfinishgoods.ResumeLayout(False)
        CType(Me.oGFTSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcwarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvwarehouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryGFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogrpbarcode As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogrpfinishgoods As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNHSysWHFGId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysWHFGId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysWHFGId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmsaveweightpack As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcwarehouse As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvwarehouse As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GFTBarCodePallet As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmremove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RepositoryGFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents oGFTSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GFTCustomerPO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFTPOLine As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysWHLocId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysWHLocId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysWHLocId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
End Class
