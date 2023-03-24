<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wExportProductionCard
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ogcpurchase = New DevExpress.XtraGrid.GridControl()
        Me.ogvpurchase = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTPurchaseBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTPurchaseNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTPurchaseNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmloaddata = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ogcpurchase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvpurchase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPurchaseNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.ogcpurchase)
        Me.GroupControl1.Controls.Add(Me.FTPurchaseNo)
        Me.GroupControl1.Controls.Add(Me.FTPurchaseNo_lbl)
        Me.GroupControl1.Location = New System.Drawing.Point(1, 1)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.ShowCaption = False
        Me.GroupControl1.Size = New System.Drawing.Size(971, 164)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "Criteria Info"
        '
        'ogcpurchase
        '
        Me.ogcpurchase.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcpurchase.Location = New System.Drawing.Point(293, 5)
        Me.ogcpurchase.MainView = Me.ogvpurchase
        Me.ogcpurchase.Name = "ogcpurchase"
        Me.ogcpurchase.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect})
        Me.ogcpurchase.Size = New System.Drawing.Size(673, 159)
        Me.ogcpurchase.TabIndex = 312
        Me.ogcpurchase.TabStop = False
        Me.ogcpurchase.Tag = "3|"
        Me.ogcpurchase.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvpurchase})
        '
        'ogvpurchase
        '
        Me.ogvpurchase.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTPurchaseNo, Me.CFTPurchaseBy})
        Me.ogvpurchase.GridControl = Me.ogcpurchase
        Me.ogvpurchase.Name = "ogvpurchase"
        Me.ogvpurchase.OptionsCustomization.AllowGroup = False
        Me.ogvpurchase.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvpurchase.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        Me.ogvpurchase.OptionsView.ColumnAutoWidth = False
        Me.ogvpurchase.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvpurchase.OptionsView.ShowGroupPanel = False
        Me.ogvpurchase.Tag = "2|"
        '
        'CFTPurchaseNo
        '
        Me.CFTPurchaseNo.AppearanceHeader.Options.UseTextOptions = True
        Me.CFTPurchaseNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CFTPurchaseNo.Caption = "Purchase No"
        Me.CFTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.CFTPurchaseNo.Name = "CFTPurchaseNo"
        Me.CFTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.CFTPurchaseNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTPurchaseNo.OptionsColumn.AllowShowHide = False
        Me.CFTPurchaseNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTPurchaseNo.OptionsColumn.ReadOnly = True
        Me.CFTPurchaseNo.Visible = True
        Me.CFTPurchaseNo.VisibleIndex = 0
        Me.CFTPurchaseNo.Width = 190
        '
        'CFTPurchaseBy
        '
        Me.CFTPurchaseBy.Caption = "Purchase By"
        Me.CFTPurchaseBy.FieldName = "FTPurchaseBy"
        Me.CFTPurchaseBy.Name = "CFTPurchaseBy"
        Me.CFTPurchaseBy.OptionsColumn.AllowEdit = False
        Me.CFTPurchaseBy.OptionsColumn.ReadOnly = True
        Me.CFTPurchaseBy.Visible = True
        Me.CFTPurchaseBy.VisibleIndex = 1
        Me.CFTPurchaseBy.Width = 113
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Caption = "Check"
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'FTPurchaseNo
        '
        Me.FTPurchaseNo.EnterMoveNextControl = True
        Me.FTPurchaseNo.Location = New System.Drawing.Point(138, 11)
        Me.FTPurchaseNo.Name = "FTPurchaseNo"
        Me.FTPurchaseNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTPurchaseNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTPurchaseNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTPurchaseNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTPurchaseNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTPurchaseNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTPurchaseNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTPurchaseNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTPurchaseNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTPurchaseNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTPurchaseNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTPurchaseNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTPurchaseNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTPurchaseNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        SerializableAppearanceObject1.Options.UseTextOptions = True
        SerializableAppearanceObject1.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject2.Options.UseTextOptions = True
        SerializableAppearanceObject2.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject3.Options.UseTextOptions = True
        SerializableAppearanceObject3.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject4.Options.UseTextOptions = True
        SerializableAppearanceObject4.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPurchaseNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "...", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, "", New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "111", Nothing, True)})
        Me.FTPurchaseNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTPurchaseNo.Size = New System.Drawing.Size(149, 20)
        Me.FTPurchaseNo.TabIndex = 265
        Me.FTPurchaseNo.TabStop = False
        Me.FTPurchaseNo.Tag = "2|"
        '
        'FTPurchaseNo_lbl
        '
        Me.FTPurchaseNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPurchaseNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTPurchaseNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPurchaseNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPurchaseNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPurchaseNo_lbl.Location = New System.Drawing.Point(10, 10)
        Me.FTPurchaseNo_lbl.Name = "FTPurchaseNo_lbl"
        Me.FTPurchaseNo_lbl.Size = New System.Drawing.Size(128, 19)
        Me.FTPurchaseNo_lbl.TabIndex = 266
        Me.FTPurchaseNo_lbl.Tag = "2|"
        Me.FTPurchaseNo_lbl.Text = "Purchase Order No. :"
        '
        'ogbdetail
        '
        Me.ogbdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbdetail.Controls.Add(Me.otb)
        Me.ogbdetail.Location = New System.Drawing.Point(1, 171)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.Size = New System.Drawing.Size(971, 473)
        Me.ogbdetail.TabIndex = 143
        Me.ogbdetail.Text = "Data Export Info"
        '
        'otb
        '
        Me.otb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otb.Location = New System.Drawing.Point(2, 20)
        Me.otb.Name = "otb"
        Me.otb.Size = New System.Drawing.Size(967, 451)
        Me.otb.TabIndex = 1
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmloaddata)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(123, 298)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(767, 47)
        Me.ogbmainprocbutton.TabIndex = 144
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(336, 11)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(95, 25)
        Me.ocmclear.TabIndex = 97
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(654, 11)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmloaddata
        '
        Me.ocmloaddata.Location = New System.Drawing.Point(116, 11)
        Me.ocmloaddata.Name = "ocmloaddata"
        Me.ocmloaddata.Size = New System.Drawing.Size(100, 25)
        Me.ocmloaddata.TabIndex = 94
        Me.ocmloaddata.TabStop = False
        Me.ocmloaddata.Tag = "2|"
        Me.ocmloaddata.Text = "Load Data"
        '
        'wExportProductionCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(975, 642)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.GroupControl1)
        Me.Name = "wExportProductionCard"
        Me.Text = "Export Production Card"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ogcpurchase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvpurchase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPurchaseNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmloaddata As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcpurchase As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvpurchase As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CFTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTPurchaseBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents FTPurchaseNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTPurchaseNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
End Class
