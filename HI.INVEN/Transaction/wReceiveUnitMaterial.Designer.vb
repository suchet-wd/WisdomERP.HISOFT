<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wReceiveUnitMaterial
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogcrcv = New DevExpress.XtraGrid.GridControl()
        Me.ogvrcv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitCodePurchase = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysUnitId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNHSysUnitId = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.FNHSysUnitId_Hide = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCalcEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ResFNPOBalQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ResFNQuantity = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ResFNRcvHisQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ResFNRcvQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ResFTStateSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ReposFTFabricFrontSize = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.ogb = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmreceive = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogcrcv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvrcv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNHSysUnitId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNPOBalQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNRcvHisQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFNRcvQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFTStateSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTFabricFrontSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogb.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogcrcv
        '
        Me.ogcrcv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcrcv.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcrcv.Location = New System.Drawing.Point(6, 34)
        Me.ogcrcv.MainView = Me.ogvrcv
        Me.ogcrcv.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcrcv.Name = "ogcrcv"
        Me.ogcrcv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCalcEdit1, Me.ResFNPOBalQty, Me.ResFNQuantity, Me.ResFNRcvHisQty, Me.ResFNRcvQty, Me.ResFTStateSelect, Me.ReposFTFabricFrontSize, Me.ReposFNHSysUnitId})
        Me.ogcrcv.Size = New System.Drawing.Size(828, 327)
        Me.ogcrcv.TabIndex = 3
        Me.ogcrcv.Tag = "3|"
        Me.ogcrcv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvrcv})
        '
        'ogvrcv
        '
        Me.ogvrcv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTRawMatCode, Me.FTRawMatName, Me.FTUnitCodePurchase, Me.FNHSysUnitId, Me.FNHSysUnitId_Hide})
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
        'FTRawMatCode
        '
        Me.FTRawMatCode.Caption = "FTRawMatCode"
        Me.FTRawMatCode.FieldName = "FTRawMatCode"
        Me.FTRawMatCode.Name = "FTRawMatCode"
        Me.FTRawMatCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatCode.Visible = True
        Me.FTRawMatCode.VisibleIndex = 0
        Me.FTRawMatCode.Width = 109
        '
        'FTRawMatName
        '
        Me.FTRawMatName.Caption = "FTRawMatName"
        Me.FTRawMatName.FieldName = "FTRawMatName"
        Me.FTRawMatName.Name = "FTRawMatName"
        Me.FTRawMatName.OptionsColumn.AllowEdit = False
        Me.FTRawMatName.OptionsColumn.ReadOnly = True
        Me.FTRawMatName.Visible = True
        Me.FTRawMatName.VisibleIndex = 1
        Me.FTRawMatName.Width = 276
        '
        'FTUnitCodePurchase
        '
        Me.FTUnitCodePurchase.Caption = "หน่วยสั่งซื้อ"
        Me.FTUnitCodePurchase.FieldName = "FTUnitCode"
        Me.FTUnitCodePurchase.Name = "FTUnitCodePurchase"
        Me.FTUnitCodePurchase.OptionsColumn.AllowEdit = False
        Me.FTUnitCodePurchase.OptionsColumn.ReadOnly = True
        Me.FTUnitCodePurchase.Visible = True
        Me.FTUnitCodePurchase.VisibleIndex = 2
        Me.FTUnitCodePurchase.Width = 115
        '
        'FNHSysUnitId
        '
        Me.FNHSysUnitId.Caption = "หน่วยสต๊อก"
        Me.FNHSysUnitId.ColumnEdit = Me.ReposFNHSysUnitId
        Me.FNHSysUnitId.FieldName = "FNHSysUnitId"
        Me.FNHSysUnitId.Name = "FNHSysUnitId"
        Me.FNHSysUnitId.Visible = True
        Me.FNHSysUnitId.VisibleIndex = 3
        Me.FNHSysUnitId.Width = 115
        '
        'ReposFNHSysUnitId
        '
        Me.ReposFNHSysUnitId.AutoHeight = False
        Me.ReposFNHSysUnitId.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "120", Nothing, True)})
        Me.ReposFNHSysUnitId.Name = "ReposFNHSysUnitId"
        '
        'FNHSysUnitId_Hide
        '
        Me.FNHSysUnitId_Hide.Caption = "FNHSysUnitId_Hide"
        Me.FNHSysUnitId_Hide.FieldName = "FNHSysUnitId_Hide"
        Me.FNHSysUnitId_Hide.Name = "FNHSysUnitId_Hide"
        Me.FNHSysUnitId_Hide.OptionsColumn.AllowEdit = False
        Me.FNHSysUnitId_Hide.OptionsColumn.ReadOnly = True
        '
        'RepositoryItemCalcEdit1
        '
        Me.RepositoryItemCalcEdit1.AutoHeight = False
        Me.RepositoryItemCalcEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEdit1.Name = "RepositoryItemCalcEdit1"
        Me.RepositoryItemCalcEdit1.Precision = 4
        '
        'ResFNPOBalQty
        '
        Me.ResFNPOBalQty.AutoHeight = False
        Me.ResFNPOBalQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ResFNPOBalQty.Name = "ResFNPOBalQty"
        '
        'ResFNQuantity
        '
        Me.ResFNQuantity.AutoHeight = False
        Me.ResFNQuantity.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ResFNQuantity.Name = "ResFNQuantity"
        '
        'ResFNRcvHisQty
        '
        Me.ResFNRcvHisQty.AutoHeight = False
        Me.ResFNRcvHisQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ResFNRcvHisQty.Name = "ResFNRcvHisQty"
        '
        'ResFNRcvQty
        '
        Me.ResFNRcvQty.AutoHeight = False
        Me.ResFNRcvQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ResFNRcvQty.Name = "ResFNRcvQty"
        '
        'ResFTStateSelect
        '
        Me.ResFTStateSelect.AutoHeight = False
        Me.ResFTStateSelect.Caption = "Check"
        Me.ResFTStateSelect.Name = "ResFTStateSelect"
        Me.ResFTStateSelect.ValueChecked = "1"
        Me.ResFTStateSelect.ValueUnchecked = "0"
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
        Me.ogb.Controls.Add(Me.ocmcancel)
        Me.ogb.Controls.Add(Me.ocmreceive)
        Me.ogb.Controls.Add(Me.ogcrcv)
        Me.ogb.Location = New System.Drawing.Point(2, 2)
        Me.ogb.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogb.Name = "ogb"
        Me.ogb.Size = New System.Drawing.Size(840, 367)
        Me.ogb.TabIndex = 6
        Me.ogb.Tag = "2|"
        Me.ogb.Text = "Detail"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(645, 2)
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
        Me.ocmreceive.Location = New System.Drawing.Point(449, 2)
        Me.ocmreceive.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmreceive.Name = "ocmreceive"
        Me.ocmreceive.Size = New System.Drawing.Size(187, 25)
        Me.ocmreceive.TabIndex = 100
        Me.ocmreceive.TabStop = False
        Me.ocmreceive.Tag = "2|"
        Me.ocmreceive.Text = "SAVE"
        '
        'wReceiveUnitMaterial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(843, 373)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wReceiveUnitMaterial"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "กำหนดหน่วยวัตถุดิบคงคลัง"
        CType(Me.ogcrcv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvrcv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNHSysUnitId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNPOBalQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNRcvHisQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFNRcvQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFTStateSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTFabricFrontSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogb.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcrcv As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvrcv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ResFNPOBalQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepositoryItemCalcEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ogb As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmreceive As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ResFNQuantity As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ResFNRcvHisQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ResFNRcvQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ResFTStateSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ReposFTFabricFrontSize As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents FTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitCodePurchase As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysUnitId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNHSysUnitId As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents FNHSysUnitId_Hide As DevExpress.XtraGrid.Columns.GridColumn
End Class
