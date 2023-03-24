<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPurchaseByPRAsset
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
        Me.ogbDetail = New DevExpress.XtraEditors.GroupControl()
        Me.ockSelectAll = New DevExpress.XtraEditors.CheckEdit()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmopenPO = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcDetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTPRPurchaseBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTPRPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDPRRequestDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTAssetCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTAssetName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTAssetBrandName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTAssetModelName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNNetAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysUnitId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysFixedAssetId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPRState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNFixedAssetType = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogbDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbDetail.SuspendLayout()
        CType(Me.ockSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbDetail
        '
        Me.ogbDetail.Controls.Add(Me.ockSelectAll)
        Me.ogbDetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbDetail.Controls.Add(Me.ogcDetail)
        Me.ogbDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbDetail.Location = New System.Drawing.Point(0, 0)
        Me.ogbDetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbDetail.Name = "ogbDetail"
        Me.ogbDetail.Size = New System.Drawing.Size(1862, 732)
        Me.ogbDetail.TabIndex = 0
        Me.ogbDetail.Text = "Detail"
        '
        'ockSelectAll
        '
        Me.ockSelectAll.Location = New System.Drawing.Point(38, 28)
        Me.ockSelectAll.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ockSelectAll.Name = "ockSelectAll"
        Me.ockSelectAll.Properties.Caption = "Select All"
        Me.ockSelectAll.Size = New System.Drawing.Size(231, 20)
        Me.ockSelectAll.TabIndex = 140
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmopenPO)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(615, 314)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1163, 105)
        Me.ogbmainprocbutton.TabIndex = 139
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(477, 6)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(110, 31)
        Me.ocmpreview.TabIndex = 103
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "PREVIEW"
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
        'ogcDetail
        '
        Me.ogcDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcDetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDetail.Location = New System.Drawing.Point(6, 54)
        Me.ogcDetail.MainView = Me.ogvDetail
        Me.ogcDetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcDetail.Name = "ogcDetail"
        Me.ogcDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect})
        Me.ogcDetail.Size = New System.Drawing.Size(1854, 676)
        Me.ogcDetail.TabIndex = 0
        Me.ogcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDetail})
        '
        'ogvDetail
        '
        Me.ogvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTPRPurchaseBy, Me.FTSelect, Me.FTPRPurchaseNo, Me.FDPRRequestDate, Me.FTAssetCode, Me.FTAssetName, Me.FTAssetBrandName, Me.FTAssetModelName, Me.FTUnitCode, Me.FNPrice, Me.FNQuantity, Me.FNNetAmt, Me.FNHSysUnitId, Me.FNHSysFixedAssetId, Me.FNPRState, Me.FTDescription, Me.FNFixedAssetType})
        Me.ogvDetail.GridControl = Me.ogcDetail
        Me.ogvDetail.Name = "ogvDetail"
        Me.ogvDetail.OptionsView.ColumnAutoWidth = False
        Me.ogvDetail.OptionsView.ShowGroupPanel = False
        '
        'FTPRPurchaseBy
        '
        Me.FTPRPurchaseBy.Caption = "FTPRPurchaseBy"
        Me.FTPRPurchaseBy.FieldName = "FTPRPurchaseBy"
        Me.FTPRPurchaseBy.Name = "FTPRPurchaseBy"
        Me.FTPRPurchaseBy.OptionsColumn.AllowEdit = False
        Me.FTPRPurchaseBy.OptionsColumn.ReadOnly = True
        Me.FTPRPurchaseBy.Visible = True
        Me.FTPRPurchaseBy.VisibleIndex = 0
        '
        'FTSelect
        '
        Me.FTSelect.Caption = "FTSelect"
        Me.FTSelect.ColumnEdit = Me.RepFTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 1
        Me.FTSelect.Width = 50
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'FTPRPurchaseNo
        '
        Me.FTPRPurchaseNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTPRPurchaseNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPRPurchaseNo.Caption = "FTPRPurchaseNo"
        Me.FTPRPurchaseNo.FieldName = "FTPRPurchaseNo"
        Me.FTPRPurchaseNo.Name = "FTPRPurchaseNo"
        Me.FTPRPurchaseNo.OptionsColumn.AllowEdit = False
        Me.FTPRPurchaseNo.OptionsColumn.ReadOnly = True
        Me.FTPRPurchaseNo.Visible = True
        Me.FTPRPurchaseNo.VisibleIndex = 2
        Me.FTPRPurchaseNo.Width = 121
        '
        'FDPRRequestDate
        '
        Me.FDPRRequestDate.AppearanceHeader.Options.UseTextOptions = True
        Me.FDPRRequestDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDPRRequestDate.Caption = "FDPRRequestDate"
        Me.FDPRRequestDate.FieldName = "FDPRRequestDate"
        Me.FDPRRequestDate.Name = "FDPRRequestDate"
        Me.FDPRRequestDate.OptionsColumn.AllowEdit = False
        Me.FDPRRequestDate.OptionsColumn.ReadOnly = True
        Me.FDPRRequestDate.Visible = True
        Me.FDPRRequestDate.VisibleIndex = 3
        Me.FDPRRequestDate.Width = 105
        '
        'FTAssetCode
        '
        Me.FTAssetCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTAssetCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAssetCode.Caption = "FTAssetCode"
        Me.FTAssetCode.FieldName = "FTAssetCode"
        Me.FTAssetCode.Name = "FTAssetCode"
        Me.FTAssetCode.OptionsColumn.AllowEdit = False
        Me.FTAssetCode.OptionsColumn.ReadOnly = True
        Me.FTAssetCode.Visible = True
        Me.FTAssetCode.VisibleIndex = 7
        Me.FTAssetCode.Width = 85
        '
        'FTAssetName
        '
        Me.FTAssetName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTAssetName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAssetName.Caption = "FTAssetName"
        Me.FTAssetName.FieldName = "FTAssetName"
        Me.FTAssetName.Name = "FTAssetName"
        Me.FTAssetName.OptionsColumn.AllowEdit = False
        Me.FTAssetName.OptionsColumn.ReadOnly = True
        Me.FTAssetName.Visible = True
        Me.FTAssetName.VisibleIndex = 8
        Me.FTAssetName.Width = 125
        '
        'FTAssetBrandName
        '
        Me.FTAssetBrandName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTAssetBrandName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAssetBrandName.Caption = "FTAssetBrandName"
        Me.FTAssetBrandName.FieldName = "FTAssetBrandName"
        Me.FTAssetBrandName.Name = "FTAssetBrandName"
        Me.FTAssetBrandName.OptionsColumn.AllowEdit = False
        Me.FTAssetBrandName.OptionsColumn.ReadOnly = True
        Me.FTAssetBrandName.Visible = True
        Me.FTAssetBrandName.VisibleIndex = 9
        Me.FTAssetBrandName.Width = 138
        '
        'FTAssetModelName
        '
        Me.FTAssetModelName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTAssetModelName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAssetModelName.Caption = "FTAssetModelName"
        Me.FTAssetModelName.FieldName = "FTAssetModelName"
        Me.FTAssetModelName.Name = "FTAssetModelName"
        Me.FTAssetModelName.OptionsColumn.AllowEdit = False
        Me.FTAssetModelName.OptionsColumn.ReadOnly = True
        Me.FTAssetModelName.Visible = True
        Me.FTAssetModelName.VisibleIndex = 10
        Me.FTAssetModelName.Width = 135
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
        Me.FTUnitCode.VisibleIndex = 11
        Me.FTUnitCode.Width = 96
        '
        'FNPrice
        '
        Me.FNPrice.Caption = "FNPrice"
        Me.FNPrice.DisplayFormat.FormatString = "{0:n2}"
        Me.FNPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPrice.FieldName = "FNPrice"
        Me.FNPrice.Name = "FNPrice"
        Me.FNPrice.OptionsColumn.AllowEdit = False
        Me.FNPrice.OptionsColumn.ReadOnly = True
        Me.FNPrice.Visible = True
        Me.FNPrice.VisibleIndex = 12
        Me.FNPrice.Width = 58
        '
        'FNQuantity
        '
        Me.FNQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNQuantity.Caption = "FNQuantity"
        Me.FNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNQuantity"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.OptionsColumn.ReadOnly = True
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 13
        Me.FNQuantity.Width = 132
        '
        'FNNetAmt
        '
        Me.FNNetAmt.Caption = "FNNetAmt"
        Me.FNNetAmt.DisplayFormat.FormatString = "{0:n2}"
        Me.FNNetAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNNetAmt.FieldName = "FNNetAmt"
        Me.FNNetAmt.Name = "FNNetAmt"
        Me.FNNetAmt.OptionsColumn.AllowEdit = False
        Me.FNNetAmt.OptionsColumn.ReadOnly = True
        Me.FNNetAmt.Visible = True
        Me.FNNetAmt.VisibleIndex = 14
        Me.FNNetAmt.Width = 150
        '
        'FNHSysUnitId
        '
        Me.FNHSysUnitId.Caption = "FNHSysUnitId"
        Me.FNHSysUnitId.FieldName = "FNHSysUnitId"
        Me.FNHSysUnitId.Name = "FNHSysUnitId"
        '
        'FNHSysFixedAssetId
        '
        Me.FNHSysFixedAssetId.Caption = "FNHSysFixedAssetId"
        Me.FNHSysFixedAssetId.FieldName = "FNHSysFixedAssetId"
        Me.FNHSysFixedAssetId.Name = "FNHSysFixedAssetId"
        '
        'FNPRState
        '
        Me.FNPRState.Caption = "FNPRState"
        Me.FNPRState.FieldName = "FNPRState"
        Me.FNPRState.Name = "FNPRState"
        Me.FNPRState.OptionsColumn.AllowEdit = False
        Me.FNPRState.Visible = True
        Me.FNPRState.VisibleIndex = 4
        Me.FNPRState.Width = 86
        '
        'FTDescription
        '
        Me.FTDescription.Caption = "FTDescription"
        Me.FTDescription.FieldName = "FTDescription"
        Me.FTDescription.Name = "FTDescription"
        Me.FTDescription.OptionsColumn.AllowEdit = False
        Me.FTDescription.Visible = True
        Me.FTDescription.VisibleIndex = 6
        Me.FTDescription.Width = 123
        '
        'FNFixedAssetType
        '
        Me.FNFixedAssetType.Caption = "FNFixedAssetType"
        Me.FNFixedAssetType.FieldName = "FNFixedAssetType"
        Me.FNFixedAssetType.Name = "FNFixedAssetType"
        Me.FNFixedAssetType.OptionsColumn.AllowEdit = False
        Me.FNFixedAssetType.Visible = True
        Me.FNFixedAssetType.VisibleIndex = 5
        Me.FNFixedAssetType.Width = 126
        '
        'wPurchaseByPRAsset
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1862, 732)
        Me.Controls.Add(Me.ogbDetail)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wPurchaseByPRAsset"
        Me.Text = "wPurchaseByPRAsset"
        CType(Me.ogbDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbDetail.ResumeLayout(False)
        CType(Me.ockSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ogbDetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmopenPO As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FDPRRequestDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPRPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAssetCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAssetName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAssetBrandName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAssetModelName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ockSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FNHSysUnitId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPRPurchaseBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNNetAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysFixedAssetId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPRState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNFixedAssetType As DevExpress.XtraGrid.Columns.GridColumn
End Class
