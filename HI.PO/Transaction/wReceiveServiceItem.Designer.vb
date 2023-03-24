<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wReceiveServiceItem
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
        Me.ogcrcv = New DevExpress.XtraGrid.GridControl()
        Me.ogvrcv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTStateSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ResFTStateSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CFNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ResFNQuantity = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FNRcvHisQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ResFNRcvHisQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FNPOBalQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ResFNPOBalQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FNRcvQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ResFNRcvQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.RepositoryItemCalcEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ReposFTFabricFrontSize = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.ogb = New DevExpress.XtraEditors.GroupControl()
        Me.FTStaReceiveAll = New DevExpress.XtraEditors.CheckEdit()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmreceive = New DevExpress.XtraEditors.SimpleButton()
        Me.FTNote = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogcrcv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvrcv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFTStateSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNRcvHisQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNPOBalQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNRcvQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTFabricFrontSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogb.SuspendLayout()
        CType(Me.FTStaReceiveAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcrcv
        '
        Me.ogcrcv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcrcv.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcrcv.Location = New System.Drawing.Point(6, 30)
        Me.ogcrcv.MainView = Me.ogvrcv
        Me.ogcrcv.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcrcv.Name = "ogcrcv"
        Me.ogcrcv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCalcEdit1, Me.ResFNPOBalQty, Me.ResFNQuantity, Me.ResFNRcvHisQty, Me.ResFNRcvQty, Me.ResFTStateSelect, Me.ReposFTFabricFrontSize})
        Me.ogcrcv.Size = New System.Drawing.Size(1263, 459)
        Me.ogcrcv.TabIndex = 3
        Me.ogcrcv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvrcv})
        '
        'ogvrcv
        '
        Me.ogvrcv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTStateSelect, Me.CFNSeq, Me.FTOrderNo, Me.FTDescription, Me.FNPrice, Me.FNQuantity, Me.FNRcvHisQty, Me.FNPOBalQty, Me.FNRcvQty, Me.FTNote})
        Me.ogvrcv.GridControl = Me.ogcrcv
        Me.ogvrcv.Name = "ogvrcv"
        Me.ogvrcv.OptionsCustomization.AllowGroup = False
        Me.ogvrcv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvrcv.OptionsView.ColumnAutoWidth = False
        Me.ogvrcv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvrcv.OptionsView.ShowAutoFilterRow = True
        Me.ogvrcv.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ogvrcv.OptionsView.ShowGroupPanel = False
        '
        'FTStateSelect
        '
        Me.FTStateSelect.AppearanceCell.Options.UseTextOptions = True
        Me.FTStateSelect.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStateSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStateSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStateSelect.Caption = " "
        Me.FTStateSelect.ColumnEdit = Me.ResFTStateSelect
        Me.FTStateSelect.FieldName = "FTStateSelect"
        Me.FTStateSelect.Name = "FTStateSelect"
        Me.FTStateSelect.OptionsColumn.AllowMove = False
        Me.FTStateSelect.OptionsColumn.ShowCaption = False
        Me.FTStateSelect.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTStateSelect.Visible = True
        Me.FTStateSelect.VisibleIndex = 0
        Me.FTStateSelect.Width = 30
        '
        'ResFTStateSelect
        '
        Me.ResFTStateSelect.AutoHeight = False
        Me.ResFTStateSelect.Caption = "Check"
        Me.ResFTStateSelect.Name = "ResFTStateSelect"
        Me.ResFTStateSelect.ValueChecked = "1"
        Me.ResFTStateSelect.ValueUnchecked = "0"
        '
        'CFNSeq
        '
        Me.CFNSeq.Caption = "FNSeq"
        Me.CFNSeq.FieldName = "FNSeq"
        Me.CFNSeq.Name = "CFNSeq"
        '
        'FTOrderNo
        '
        Me.FTOrderNo.Caption = "FTOrderNo"
        Me.FTOrderNo.FieldName = "FTOrderNo"
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.OptionsColumn.AllowEdit = False
        Me.FTOrderNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTOrderNo.OptionsColumn.ReadOnly = True
        Me.FTOrderNo.Visible = True
        Me.FTOrderNo.VisibleIndex = 1
        Me.FTOrderNo.Width = 114
        '
        'FTDescription
        '
        Me.FTDescription.AppearanceHeader.Options.UseTextOptions = True
        Me.FTDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTDescription.Caption = "FTDescription"
        Me.FTDescription.FieldName = "FTDescription"
        Me.FTDescription.Name = "FTDescription"
        Me.FTDescription.OptionsColumn.AllowEdit = False
        Me.FTDescription.OptionsColumn.AllowMove = False
        Me.FTDescription.OptionsColumn.ReadOnly = True
        Me.FTDescription.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTDescription.Visible = True
        Me.FTDescription.VisibleIndex = 2
        Me.FTDescription.Width = 467
        '
        'FNPrice
        '
        Me.FNPrice.Caption = "FNPrice"
        Me.FNPrice.FieldName = "FNPrice"
        Me.FNPrice.Name = "FNPrice"
        Me.FNPrice.OptionsColumn.AllowEdit = False
        Me.FNPrice.OptionsColumn.ReadOnly = True
        Me.FNPrice.Visible = True
        Me.FNPrice.VisibleIndex = 3
        Me.FNPrice.Width = 100
        '
        'FNQuantity
        '
        Me.FNQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNQuantity.Caption = "FNQuantity"
        Me.FNQuantity.ColumnEdit = Me.ResFNQuantity
        Me.FNQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNQuantity"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.OptionsColumn.AllowMove = False
        Me.FNQuantity.OptionsColumn.ReadOnly = True
        Me.FNQuantity.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 4
        Me.FNQuantity.Width = 97
        '
        'ResFNQuantity
        '
        Me.ResFNQuantity.AutoHeight = False
        Me.ResFNQuantity.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ResFNQuantity.Name = "ResFNQuantity"
        '
        'FNRcvHisQty
        '
        Me.FNRcvHisQty.AppearanceCell.Options.UseTextOptions = True
        Me.FNRcvHisQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNRcvHisQty.AppearanceHeader.Options.UseTextOptions = True
        Me.FNRcvHisQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNRcvHisQty.Caption = "FNRcvHisQty"
        Me.FNRcvHisQty.ColumnEdit = Me.ResFNRcvHisQty
        Me.FNRcvHisQty.DisplayFormat.FormatString = "{0:n4}"
        Me.FNRcvHisQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNRcvHisQty.FieldName = "FNRcvHisQty"
        Me.FNRcvHisQty.Name = "FNRcvHisQty"
        Me.FNRcvHisQty.OptionsColumn.AllowEdit = False
        Me.FNRcvHisQty.OptionsColumn.AllowMove = False
        Me.FNRcvHisQty.OptionsColumn.ReadOnly = True
        Me.FNRcvHisQty.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FNRcvHisQty.Visible = True
        Me.FNRcvHisQty.VisibleIndex = 5
        Me.FNRcvHisQty.Width = 85
        '
        'ResFNRcvHisQty
        '
        Me.ResFNRcvHisQty.AutoHeight = False
        Me.ResFNRcvHisQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ResFNRcvHisQty.Name = "ResFNRcvHisQty"
        '
        'FNPOBalQty
        '
        Me.FNPOBalQty.AppearanceCell.Options.UseTextOptions = True
        Me.FNPOBalQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPOBalQty.AppearanceHeader.Options.UseTextOptions = True
        Me.FNPOBalQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNPOBalQty.Caption = "FNPOBalQty"
        Me.FNPOBalQty.ColumnEdit = Me.ResFNPOBalQty
        Me.FNPOBalQty.DisplayFormat.FormatString = "{0:n4}"
        Me.FNPOBalQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPOBalQty.FieldName = "FNPOBalQty"
        Me.FNPOBalQty.Name = "FNPOBalQty"
        Me.FNPOBalQty.OptionsColumn.AllowEdit = False
        Me.FNPOBalQty.OptionsColumn.AllowMove = False
        Me.FNPOBalQty.OptionsColumn.ReadOnly = True
        Me.FNPOBalQty.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FNPOBalQty.Visible = True
        Me.FNPOBalQty.VisibleIndex = 6
        Me.FNPOBalQty.Width = 89
        '
        'ResFNPOBalQty
        '
        Me.ResFNPOBalQty.AutoHeight = False
        Me.ResFNPOBalQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ResFNPOBalQty.Name = "ResFNPOBalQty"
        '
        'FNRcvQty
        '
        Me.FNRcvQty.AppearanceCell.Options.UseTextOptions = True
        Me.FNRcvQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNRcvQty.AppearanceHeader.Options.UseTextOptions = True
        Me.FNRcvQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNRcvQty.Caption = "FNRcvQty"
        Me.FNRcvQty.ColumnEdit = Me.ResFNRcvQty
        Me.FNRcvQty.DisplayFormat.FormatString = "{0:n4}"
        Me.FNRcvQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNRcvQty.FieldName = "FNRcvQty"
        Me.FNRcvQty.Name = "FNRcvQty"
        Me.FNRcvQty.OptionsColumn.AllowMove = False
        Me.FNRcvQty.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FNRcvQty.Visible = True
        Me.FNRcvQty.VisibleIndex = 7
        Me.FNRcvQty.Width = 96
        '
        'ResFNRcvQty
        '
        Me.ResFNRcvQty.AutoHeight = False
        Me.ResFNRcvQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ResFNRcvQty.Name = "ResFNRcvQty"
        '
        'RepositoryItemCalcEdit1
        '
        Me.RepositoryItemCalcEdit1.AutoHeight = False
        Me.RepositoryItemCalcEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEdit1.Name = "RepositoryItemCalcEdit1"
        Me.RepositoryItemCalcEdit1.Precision = 4
        '
        'ReposFTFabricFrontSize
        '
        Me.ReposFTFabricFrontSize.AutoHeight = False
        Me.ReposFTFabricFrontSize.MaxLength = 50
        Me.ReposFTFabricFrontSize.Name = "ReposFTFabricFrontSize"
        '
        'ogb
        '
        Me.ogb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogb.Controls.Add(Me.FTStaReceiveAll)
        Me.ogb.Controls.Add(Me.ocmcancel)
        Me.ogb.Controls.Add(Me.ocmreceive)
        Me.ogb.Controls.Add(Me.ogcrcv)
        Me.ogb.Location = New System.Drawing.Point(2, 2)
        Me.ogb.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogb.Name = "ogb"
        Me.ogb.Size = New System.Drawing.Size(1275, 495)
        Me.ogb.TabIndex = 6
        Me.ogb.Tag = "2|"
        Me.ogb.Text = "Purchase Detail"
        '
        'FTStaReceiveAll
        '
        Me.FTStaReceiveAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTStaReceiveAll.Location = New System.Drawing.Point(694, 1)
        Me.FTStaReceiveAll.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStaReceiveAll.Name = "FTStaReceiveAll"
        Me.FTStaReceiveAll.Properties.Appearance.Options.UseTextOptions = True
        Me.FTStaReceiveAll.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStaReceiveAll.Properties.Caption = "Receive All"
        Me.FTStaReceiveAll.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStaReceiveAll.Properties.ValueChecked = "1"
        Me.FTStaReceiveAll.Properties.ValueUnchecked = "0"
        Me.FTStaReceiveAll.Size = New System.Drawing.Size(176, 20)
        Me.FTStaReceiveAll.TabIndex = 102
        Me.FTStaReceiveAll.Tag = "2|"
        Me.FTStaReceiveAll.Visible = False
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(1080, 1)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(187, 25)
        Me.ocmcancel.TabIndex = 101
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmreceive
        '
        Me.ocmreceive.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmreceive.Location = New System.Drawing.Point(884, 1)
        Me.ocmreceive.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmreceive.Name = "ocmreceive"
        Me.ocmreceive.Size = New System.Drawing.Size(187, 25)
        Me.ocmreceive.TabIndex = 100
        Me.ocmreceive.TabStop = False
        Me.ocmreceive.Tag = "2|"
        Me.ocmreceive.Text = "RECEIVE"
        '
        'FTNote
        '
        Me.FTNote.Caption = "FTNote"
        Me.FTNote.FieldName = "FTNote"
        Me.FTNote.Name = "FTNote"
        '
        'wReceiveServiceItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1279, 501)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wReceiveServiceItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wReceivePopup"
        CType(Me.ogcrcv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvrcv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFTStateSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNRcvHisQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNPOBalQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNRcvQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTFabricFrontSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogb.ResumeLayout(False)
        CType(Me.FTStaReceiveAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcrcv As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvrcv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CFNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ResFNPOBalQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FNRcvHisQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPOBalQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNRcvQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCalcEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ogb As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmreceive As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ResFNQuantity As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ResFNRcvHisQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ResFNRcvQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FTStaReceiveAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTStateSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ResFTStateSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ReposFTFabricFrontSize As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents FTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTNote As DevExpress.XtraGrid.Columns.GridColumn
End Class
