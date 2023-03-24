<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wPurchaseOrderByPR
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
        Me.ogrpdetail = New DevExpress.XtraEditors.GroupControl()
        Me.oSelectAll = New DevExpress.XtraEditors.CheckEdit()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmopenPO = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTPRPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDPRRequestDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTPRPurchaseBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMatDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFabricFrontSize = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysRawMatId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysUnitId = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogrpdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpdetail.SuspendLayout()
        CType(Me.oSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogrpdetail
        '
        Me.ogrpdetail.Controls.Add(Me.oSelectAll)
        Me.ogrpdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogrpdetail.Controls.Add(Me.ogcdetail)
        Me.ogrpdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrpdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogrpdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpdetail.Name = "ogrpdetail"
        Me.ogrpdetail.Size = New System.Drawing.Size(1331, 678)
        Me.ogrpdetail.TabIndex = 0
        Me.ogrpdetail.Text = "Purchase Order Request List"
        '
        'oSelectAll
        '
        Me.oSelectAll.Location = New System.Drawing.Point(6, 27)
        Me.oSelectAll.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oSelectAll.Name = "oSelectAll"
        Me.oSelectAll.Properties.Caption = "FTSelectAll"
        Me.oSelectAll.Size = New System.Drawing.Size(359, 20)
        Me.oSelectAll.TabIndex = 139
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdelete)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmopenPO)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(84, 287)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1163, 105)
        Me.ogbmainprocbutton.TabIndex = 138
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmdelete
        '
        Me.ocmdelete.Location = New System.Drawing.Point(526, 37)
        Me.ocmdelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(111, 31)
        Me.ocmdelete.TabIndex = 100
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "Delete"
        '
        'ocmopenPO
        '
        Me.ocmopenPO.Location = New System.Drawing.Point(359, 6)
        Me.ocmopenPO.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmopenPO.Name = "ocmopenPO"
        Me.ocmopenPO.Size = New System.Drawing.Size(111, 31)
        Me.ocmopenPO.TabIndex = 99
        Me.ocmopenPO.TabStop = False
        Me.ocmopenPO.Tag = "2|"
        Me.ocmopenPO.Text = "Open PO"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(241, 6)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(111, 31)
        Me.ocmload.TabIndex = 99
        Me.ocmload.TabStop = False
        Me.ocmload.Tag = "2|"
        Me.ocmload.Text = "LOAD"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(1031, 14)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ogcdetail
        '
        Me.ogcdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcdetail.EmbeddedNavigator.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Location = New System.Drawing.Point(2, 58)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect})
        Me.ogcdetail.Size = New System.Drawing.Size(1326, 618)
        Me.ogcdetail.TabIndex = 121
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTSelect, Me.cFTPRPurchaseNo, Me.cFDPRRequestDate, Me.cFTPRPurchaseBy, Me.FTRawMatCode, Me.FTMatDesc, Me.FTRawMatColorCode, Me.FTRawMatSizeCode, Me.cFTOrderNo, Me.FTFabricFrontSize, Me.FTUnitCode, Me.FNQuantity, Me.cFTRemark, Me.FNHSysRawMatId, Me.FNHSysUnitId})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'cFTSelect
        '
        Me.cFTSelect.Caption = "FTSelect"
        Me.cFTSelect.ColumnEdit = Me.RepositoryFTSelect
        Me.cFTSelect.FieldName = "FTSelect"
        Me.cFTSelect.Name = "cFTSelect"
        Me.cFTSelect.Visible = True
        Me.cFTSelect.VisibleIndex = 0
        Me.cFTSelect.Width = 42
        '
        'RepositoryFTSelect
        '
        Me.RepositoryFTSelect.AutoHeight = False
        Me.RepositoryFTSelect.Caption = "Check"
        Me.RepositoryFTSelect.Name = "RepositoryFTSelect"
        Me.RepositoryFTSelect.ValueChecked = "1"
        Me.RepositoryFTSelect.ValueUnchecked = "0"
        '
        'cFTPRPurchaseNo
        '
        Me.cFTPRPurchaseNo.AppearanceHeader.Options.UseTextOptions = True
        Me.cFTPRPurchaseNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFTPRPurchaseNo.Caption = "FTPRPurchaseNo"
        Me.cFTPRPurchaseNo.FieldName = "FTPRPurchaseNo"
        Me.cFTPRPurchaseNo.Name = "cFTPRPurchaseNo"
        Me.cFTPRPurchaseNo.OptionsColumn.AllowEdit = False
        Me.cFTPRPurchaseNo.OptionsColumn.ReadOnly = True
        Me.cFTPRPurchaseNo.Visible = True
        Me.cFTPRPurchaseNo.VisibleIndex = 1
        Me.cFTPRPurchaseNo.Width = 132
        '
        'cFDPRRequestDate
        '
        Me.cFDPRRequestDate.AppearanceHeader.Options.UseTextOptions = True
        Me.cFDPRRequestDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFDPRRequestDate.Caption = "FDPRRequestDate"
        Me.cFDPRRequestDate.FieldName = "FDPRRequestDate"
        Me.cFDPRRequestDate.Name = "cFDPRRequestDate"
        Me.cFDPRRequestDate.OptionsColumn.AllowEdit = False
        Me.cFDPRRequestDate.OptionsColumn.ReadOnly = True
        Me.cFDPRRequestDate.Visible = True
        Me.cFDPRRequestDate.VisibleIndex = 2
        Me.cFDPRRequestDate.Width = 99
        '
        'cFTPRPurchaseBy
        '
        Me.cFTPRPurchaseBy.Caption = "FTPRPurchaseBy"
        Me.cFTPRPurchaseBy.FieldName = "FTPRPurchaseBy"
        Me.cFTPRPurchaseBy.Name = "cFTPRPurchaseBy"
        Me.cFTPRPurchaseBy.OptionsColumn.AllowEdit = False
        Me.cFTPRPurchaseBy.Width = 112
        '
        'FTRawMatCode
        '
        Me.FTRawMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatCode.Caption = "FTRawMatCode"
        Me.FTRawMatCode.FieldName = "FTRawMatCode"
        Me.FTRawMatCode.Name = "FTRawMatCode"
        Me.FTRawMatCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatCode.Visible = True
        Me.FTRawMatCode.VisibleIndex = 3
        Me.FTRawMatCode.Width = 125
        '
        'FTMatDesc
        '
        Me.FTMatDesc.AppearanceHeader.Options.UseTextOptions = True
        Me.FTMatDesc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTMatDesc.Caption = "FTMatDesc"
        Me.FTMatDesc.FieldName = "FTMatDesc"
        Me.FTMatDesc.Name = "FTMatDesc"
        Me.FTMatDesc.OptionsColumn.AllowEdit = False
        Me.FTMatDesc.OptionsColumn.ReadOnly = True
        Me.FTMatDesc.Visible = True
        Me.FTMatDesc.VisibleIndex = 4
        Me.FTMatDesc.Width = 203
        '
        'FTRawMatColorCode
        '
        Me.FTRawMatColorCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatColorCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatColorCode.Caption = "FTRawMatColorCode"
        Me.FTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.FTRawMatColorCode.Name = "FTRawMatColorCode"
        Me.FTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatColorCode.Visible = True
        Me.FTRawMatColorCode.VisibleIndex = 5
        Me.FTRawMatColorCode.Width = 94
        '
        'FTRawMatSizeCode
        '
        Me.FTRawMatSizeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatSizeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatSizeCode.Caption = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.FieldName = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.Name = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatSizeCode.Visible = True
        Me.FTRawMatSizeCode.VisibleIndex = 6
        Me.FTRawMatSizeCode.Width = 86
        '
        'cFTOrderNo
        '
        Me.cFTOrderNo.AppearanceHeader.Options.UseTextOptions = True
        Me.cFTOrderNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFTOrderNo.Caption = "FTOrderNo"
        Me.cFTOrderNo.FieldName = "FTOrderNo"
        Me.cFTOrderNo.Name = "cFTOrderNo"
        Me.cFTOrderNo.OptionsColumn.AllowEdit = False
        Me.cFTOrderNo.OptionsColumn.ReadOnly = True
        Me.cFTOrderNo.Visible = True
        Me.cFTOrderNo.VisibleIndex = 7
        Me.cFTOrderNo.Width = 104
        '
        'FTFabricFrontSize
        '
        Me.FTFabricFrontSize.AppearanceHeader.Options.UseTextOptions = True
        Me.FTFabricFrontSize.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTFabricFrontSize.Caption = "FTFabricFrontSize"
        Me.FTFabricFrontSize.FieldName = "FTFabricFrontSize"
        Me.FTFabricFrontSize.Name = "FTFabricFrontSize"
        Me.FTFabricFrontSize.OptionsColumn.AllowEdit = False
        Me.FTFabricFrontSize.OptionsColumn.ReadOnly = True
        Me.FTFabricFrontSize.Visible = True
        Me.FTFabricFrontSize.VisibleIndex = 8
        Me.FTFabricFrontSize.Width = 107
        '
        'FTUnitCode
        '
        Me.FTUnitCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUnitCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitCode.Caption = "FTUnitCode"
        Me.FTUnitCode.FieldName = "FTUnitCode"
        Me.FTUnitCode.Name = "FTUnitCode"
        Me.FTUnitCode.OptionsColumn.AllowEdit = False
        Me.FTUnitCode.OptionsColumn.ReadOnly = True
        Me.FTUnitCode.Visible = True
        Me.FTUnitCode.VisibleIndex = 9
        Me.FTUnitCode.Width = 90
        '
        'FNQuantity
        '
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
        Me.FNQuantity.VisibleIndex = 10
        Me.FNQuantity.Width = 109
        '
        'cFTRemark
        '
        Me.cFTRemark.AppearanceHeader.Options.UseTextOptions = True
        Me.cFTRemark.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFTRemark.Caption = "FTRemark"
        Me.cFTRemark.FieldName = "FTRemark"
        Me.cFTRemark.Name = "cFTRemark"
        Me.cFTRemark.OptionsColumn.AllowEdit = False
        Me.cFTRemark.OptionsColumn.ReadOnly = True
        Me.cFTRemark.Visible = True
        Me.cFTRemark.VisibleIndex = 11
        Me.cFTRemark.Width = 207
        '
        'FNHSysRawMatId
        '
        Me.FNHSysRawMatId.Caption = "FNHSysRawMatId"
        Me.FNHSysRawMatId.FieldName = "FNHSysRawMatId"
        Me.FNHSysRawMatId.Name = "FNHSysRawMatId"
        '
        'FNHSysUnitId
        '
        Me.FNHSysUnitId.Caption = "FNHSysUnitId"
        Me.FNHSysUnitId.FieldName = "FNHSysUnitId"
        Me.FNHSysUnitId.Name = "FNHSysUnitId"
        '
        'wPurchaseOrderByPR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1331, 678)
        Me.Controls.Add(Me.ogrpdetail)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wPurchaseOrderByPR"
        Me.Text = "wPurchaseOrderByPR"
        CType(Me.ogrpdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpdetail.ResumeLayout(False)
        CType(Me.oSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogrpdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTPRPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDPRRequestDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTPRPurchaseBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMatDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFabricFrontSize As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysRawMatId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysUnitId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmopenPO As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents oSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
End Class
