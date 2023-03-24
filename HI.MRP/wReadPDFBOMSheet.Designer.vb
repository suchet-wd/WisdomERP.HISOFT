<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wReadPDFBOMSheet
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
        Me.ogbselectfile = New DevExpress.XtraEditors.GroupControl()
        Me.FTFilePath = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSeason = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFTRawMatDesc = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.FTStyleNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStyleName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSupplier = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPosittion = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNUsedQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpnormal = New DevExpress.XtraTab.XtraTabPage()
        Me.otpset = New DevExpress.XtraTab.XtraTabPage()
        Me.PdfViewer1 = New DevExpress.XtraPdfViewer.PdfViewer()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmimportbimpdf = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmrefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmloaddata = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbselectfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbselectfile.SuspendLayout()
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTRawMatDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpnormal.SuspendLayout()
        Me.otpset.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbselectfile
        '
        Me.ogbselectfile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbselectfile.Controls.Add(Me.FTFilePath)
        Me.ogbselectfile.Location = New System.Drawing.Point(3, 4)
        Me.ogbselectfile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbselectfile.Name = "ogbselectfile"
        Me.ogbselectfile.Size = New System.Drawing.Size(1318, 71)
        Me.ogbselectfile.TabIndex = 2
        Me.ogbselectfile.Text = "Select File"
        '
        'FTFilePath
        '
        Me.FTFilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTFilePath.Location = New System.Drawing.Point(43, 32)
        Me.FTFilePath.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTFilePath.Name = "FTFilePath"
        Me.FTFilePath.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTFilePath.Properties.Appearance.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTFilePath.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTFilePath.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTFilePath.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTFilePath.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTFilePath.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTFilePath.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTFilePath.Properties.ReadOnly = True
        Me.FTFilePath.Size = New System.Drawing.Size(1259, 23)
        Me.FTFilePath.TabIndex = 1
        Me.FTFilePath.Tag = "2|"
        '
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFTSelect, Me.ReposFTRawMatDesc})
        Me.ogcdetail.Size = New System.Drawing.Size(1308, 582)
        Me.ogcdetail.TabIndex = 3
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSeason, Me.FTStyleNo, Me.FTStyleName, Me.FTRawMatCode, Me.FTSupplier, Me.FTRawMatDesc, Me.FTPosittion, Me.FNUsedQuantity, Me.FTUnit})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowGroup = False
        Me.ogvdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetail.OptionsView.RowAutoHeight = True
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        Me.ogvdetail.Tag = "2|"
        '
        'FTSeason
        '
        Me.FTSeason.AppearanceCell.Options.UseTextOptions = True
        Me.FTSeason.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.FTSeason.Caption = "FTSeason"
        Me.FTSeason.ColumnEdit = Me.ReposFTRawMatDesc
        Me.FTSeason.FieldName = "FTSeason"
        Me.FTSeason.Name = "FTSeason"
        Me.FTSeason.OptionsColumn.AllowEdit = False
        Me.FTSeason.OptionsColumn.ReadOnly = True
        Me.FTSeason.Visible = True
        Me.FTSeason.VisibleIndex = 0
        Me.FTSeason.Width = 78
        '
        'ReposFTRawMatDesc
        '
        Me.ReposFTRawMatDesc.Name = "ReposFTRawMatDesc"
        '
        'FTStyleNo
        '
        Me.FTStyleNo.AppearanceCell.Options.UseTextOptions = True
        Me.FTStyleNo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.FTStyleNo.Caption = "FTStyleNo"
        Me.FTStyleNo.ColumnEdit = Me.ReposFTRawMatDesc
        Me.FTStyleNo.FieldName = "FTStyleNo"
        Me.FTStyleNo.Name = "FTStyleNo"
        Me.FTStyleNo.OptionsColumn.AllowEdit = False
        Me.FTStyleNo.OptionsColumn.ReadOnly = True
        Me.FTStyleNo.Visible = True
        Me.FTStyleNo.VisibleIndex = 1
        Me.FTStyleNo.Width = 108
        '
        'FTStyleName
        '
        Me.FTStyleName.AppearanceCell.Options.UseTextOptions = True
        Me.FTStyleName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.FTStyleName.Caption = "FTStyleName"
        Me.FTStyleName.ColumnEdit = Me.ReposFTRawMatDesc
        Me.FTStyleName.FieldName = "FTStyleName"
        Me.FTStyleName.Name = "FTStyleName"
        Me.FTStyleName.OptionsColumn.AllowEdit = False
        Me.FTStyleName.OptionsColumn.ReadOnly = True
        Me.FTStyleName.Visible = True
        Me.FTStyleName.VisibleIndex = 2
        Me.FTStyleName.Width = 130
        '
        'FTRawMatCode
        '
        Me.FTRawMatCode.AppearanceCell.Options.UseTextOptions = True
        Me.FTRawMatCode.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.FTRawMatCode.Caption = "FTRawMatCode"
        Me.FTRawMatCode.ColumnEdit = Me.ReposFTRawMatDesc
        Me.FTRawMatCode.FieldName = "FTRawMatCode"
        Me.FTRawMatCode.Name = "FTRawMatCode"
        Me.FTRawMatCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatCode.Visible = True
        Me.FTRawMatCode.VisibleIndex = 4
        Me.FTRawMatCode.Width = 126
        '
        'FTSupplier
        '
        Me.FTSupplier.AppearanceCell.Options.UseTextOptions = True
        Me.FTSupplier.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.FTSupplier.Caption = "FTSupplier"
        Me.FTSupplier.ColumnEdit = Me.ReposFTRawMatDesc
        Me.FTSupplier.FieldName = "FTSupplier"
        Me.FTSupplier.Name = "FTSupplier"
        Me.FTSupplier.OptionsColumn.AllowEdit = False
        Me.FTSupplier.OptionsColumn.ReadOnly = True
        Me.FTSupplier.Visible = True
        Me.FTSupplier.VisibleIndex = 3
        '
        'FTRawMatDesc
        '
        Me.FTRawMatDesc.AppearanceCell.Options.UseTextOptions = True
        Me.FTRawMatDesc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.FTRawMatDesc.Caption = "FTRawMatDesc"
        Me.FTRawMatDesc.ColumnEdit = Me.ReposFTRawMatDesc
        Me.FTRawMatDesc.FieldName = "FTRawMatDesc"
        Me.FTRawMatDesc.Name = "FTRawMatDesc"
        Me.FTRawMatDesc.OptionsColumn.AllowEdit = False
        Me.FTRawMatDesc.OptionsColumn.ReadOnly = True
        Me.FTRawMatDesc.Visible = True
        Me.FTRawMatDesc.VisibleIndex = 5
        Me.FTRawMatDesc.Width = 228
        '
        'FTPosittion
        '
        Me.FTPosittion.AppearanceCell.Options.UseTextOptions = True
        Me.FTPosittion.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.FTPosittion.Caption = "FTPosittion"
        Me.FTPosittion.ColumnEdit = Me.ReposFTRawMatDesc
        Me.FTPosittion.FieldName = "FTPosittion"
        Me.FTPosittion.Name = "FTPosittion"
        Me.FTPosittion.OptionsColumn.AllowEdit = False
        Me.FTPosittion.OptionsColumn.ReadOnly = True
        Me.FTPosittion.Visible = True
        Me.FTPosittion.VisibleIndex = 6
        Me.FTPosittion.Width = 103
        '
        'FNUsedQuantity
        '
        Me.FNUsedQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNUsedQuantity.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.FNUsedQuantity.Caption = "Consumsion"
        Me.FNUsedQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNUsedQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNUsedQuantity.FieldName = "FNUsedQuantity"
        Me.FNUsedQuantity.Name = "FNUsedQuantity"
        Me.FNUsedQuantity.OptionsColumn.AllowEdit = False
        Me.FNUsedQuantity.OptionsColumn.ReadOnly = True
        Me.FNUsedQuantity.Visible = True
        Me.FNUsedQuantity.VisibleIndex = 7
        Me.FNUsedQuantity.Width = 110
        '
        'FTUnit
        '
        Me.FTUnit.AppearanceCell.Options.UseTextOptions = True
        Me.FTUnit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.FTUnit.Caption = "FTUnit"
        Me.FTUnit.ColumnEdit = Me.ReposFTRawMatDesc
        Me.FTUnit.FieldName = "FTUnit"
        Me.FTUnit.Name = "FTUnit"
        Me.FTUnit.OptionsColumn.AllowEdit = False
        Me.FTUnit.OptionsColumn.ReadOnly = True
        Me.FTUnit.Visible = True
        Me.FTUnit.VisibleIndex = 8
        Me.FTUnit.Width = 90
        '
        'ReposFTSelect
        '
        Me.ReposFTSelect.AutoHeight = False
        Me.ReposFTSelect.Caption = "Check"
        Me.ReposFTSelect.Name = "ReposFTSelect"
        Me.ReposFTSelect.ValueChecked = "1"
        Me.ReposFTSelect.ValueUnchecked = "0"
        '
        'otb
        '
        Me.otb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.otb.Location = New System.Drawing.Point(3, 81)
        Me.otb.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpnormal
        Me.otb.Size = New System.Drawing.Size(1318, 619)
        Me.otb.TabIndex = 392
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpnormal, Me.otpset})
        '
        'otpnormal
        '
        Me.otpnormal.Controls.Add(Me.ogcdetail)
        Me.otpnormal.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otpnormal.Name = "otpnormal"
        Me.otpnormal.Size = New System.Drawing.Size(1308, 582)
        Me.otpnormal.Text = "PDF Detail"
        '
        'otpset
        '
        Me.otpset.Controls.Add(Me.PdfViewer1)
        Me.otpset.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otpset.Name = "otpset"
        Me.otpset.Size = New System.Drawing.Size(1308, 582)
        Me.otpset.Text = "PDF Viewer"
        '
        'PdfViewer1
        '
        Me.PdfViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PdfViewer1.Location = New System.Drawing.Point(0, 0)
        Me.PdfViewer1.Name = "PdfViewer1"
        Me.PdfViewer1.Size = New System.Drawing.Size(1308, 582)
        Me.PdfViewer1.TabIndex = 0
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmimportbimpdf)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmrefresh)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmloaddata)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(90, 330)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(859, 140)
        Me.ogbmainprocbutton.TabIndex = 393
        '
        'ocmimportbimpdf
        '
        Me.ocmimportbimpdf.Location = New System.Drawing.Point(248, 55)
        Me.ocmimportbimpdf.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmimportbimpdf.Name = "ocmimportbimpdf"
        Me.ocmimportbimpdf.Size = New System.Drawing.Size(237, 31)
        Me.ocmimportbimpdf.TabIndex = 336
        Me.ocmimportbimpdf.TabStop = False
        Me.ocmimportbimpdf.Tag = "2|"
        Me.ocmimportbimpdf.Text = "Importoptiplan"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(436, 7)
        Me.ocmsavelayout.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(136, 28)
        Me.ocmsavelayout.TabIndex = 335
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ocmrefresh
        '
        Me.ocmrefresh.Location = New System.Drawing.Point(329, 5)
        Me.ocmrefresh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmrefresh.Name = "ocmrefresh"
        Me.ocmrefresh.Size = New System.Drawing.Size(85, 31)
        Me.ocmrefresh.TabIndex = 334
        Me.ocmrefresh.TabStop = False
        Me.ocmrefresh.Tag = "2|"
        Me.ocmrefresh.Text = "Refresh"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(785, 4)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(64, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmloaddata
        '
        Me.ocmloaddata.Location = New System.Drawing.Point(6, 6)
        Me.ocmloaddata.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmloaddata.Name = "ocmloaddata"
        Me.ocmloaddata.Size = New System.Drawing.Size(92, 31)
        Me.ocmloaddata.TabIndex = 93
        Me.ocmloaddata.TabStop = False
        Me.ocmloaddata.Tag = "2|"
        Me.ocmloaddata.Text = "LoadData"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(100, 6)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(84, 31)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'wReadPDFBOMSheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1322, 703)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.ogbselectfile)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wReadPDFBOMSheet"
        Me.Text = "Read PDF Bom Sheet"
        CType(Me.ogbselectfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbselectfile.ResumeLayout(False)
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTRawMatDesc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpnormal.ResumeLayout(False)
        Me.otpset.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbselectfile As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTFilePath As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ReposFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTSeason As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpnormal As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otpset As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmrefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmloaddata As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PdfViewer1 As DevExpress.XtraPdfViewer.PdfViewer
    Friend WithEvents FTStyleNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStyleName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSupplier As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPosittion As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNUsedQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFTRawMatDesc As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents ocmimportbimpdf As DevExpress.XtraEditors.SimpleButton
End Class
