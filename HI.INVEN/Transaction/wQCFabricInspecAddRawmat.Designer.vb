<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wQCFabricInspecAddRawmat
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
        Me.ogcrawmat = New DevExpress.XtraGrid.GridControl()
        Me.ogvrawmat = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTFactory = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysRawMatId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTItemNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTotalRcvQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTBatchNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDyeLotNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateQC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCalcEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.RepositoryItemCalcEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.RepositoryItemCalcEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.RepositoryItemCalcEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        CType(Me.ogcrawmat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvrawmat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcrawmat
        '
        Me.ogcrawmat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcrawmat.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcrawmat.Location = New System.Drawing.Point(0, 0)
        Me.ogcrawmat.MainView = Me.ogvrawmat
        Me.ogcrawmat.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcrawmat.Name = "ogcrawmat"
        Me.ogcrawmat.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCalcEdit1, Me.RepositoryItemComboBox1, Me.RepositoryItemCalcEdit2, Me.RepositoryItemCalcEdit3, Me.RepositoryItemCalcEdit4})
        Me.ogcrawmat.Size = New System.Drawing.Size(1268, 330)
        Me.ogcrawmat.TabIndex = 2
        Me.ogcrawmat.Tag = "3|"
        Me.ogcrawmat.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvrawmat})
        '
        'ogvrawmat
        '
        Me.ogvrawmat.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTFactory, Me.FNHSysRawMatId, Me.FTRawMatCode, Me.FTItemNo, Me.FTRawMatName, Me.FTRawMatColorCode, Me.FTRawMatColorName, Me.FTRawMatSizeCode, Me.FTUnitCode, Me.FNTotalRcvQty, Me.FTBatchNo, Me.FTDyeLotNo, Me.FTStateQC})
        Me.ogvrawmat.GridControl = Me.ogcrawmat
        Me.ogvrawmat.Name = "ogvrawmat"
        Me.ogvrawmat.OptionsView.ColumnAutoWidth = False
        Me.ogvrawmat.OptionsView.ShowGroupPanel = False
        '
        'FTFactory
        '
        Me.FTFactory.AppearanceHeader.Options.UseTextOptions = True
        Me.FTFactory.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTFactory.Caption = "Factory"
        Me.FTFactory.FieldName = "FTFactory"
        Me.FTFactory.Name = "FTFactory"
        Me.FTFactory.OptionsColumn.AllowEdit = False
        Me.FTFactory.OptionsColumn.AllowMove = False
        Me.FTFactory.OptionsColumn.AllowShowHide = False
        Me.FTFactory.OptionsColumn.ReadOnly = True
        Me.FTFactory.OptionsColumn.ShowInCustomizationForm = False
        Me.FTFactory.Width = 88
        '
        'FNHSysRawMatId
        '
        Me.FNHSysRawMatId.AppearanceHeader.Options.UseTextOptions = True
        Me.FNHSysRawMatId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNHSysRawMatId.Caption = "FNHSysRawMatId"
        Me.FNHSysRawMatId.FieldName = "FNHSysRawMatId"
        Me.FNHSysRawMatId.Name = "FNHSysRawMatId"
        Me.FNHSysRawMatId.OptionsColumn.AllowEdit = False
        Me.FNHSysRawMatId.OptionsColumn.AllowMove = False
        Me.FNHSysRawMatId.OptionsColumn.AllowShowHide = False
        Me.FNHSysRawMatId.OptionsColumn.ReadOnly = True
        Me.FNHSysRawMatId.OptionsColumn.ShowInCustomizationForm = False
        '
        'FTRawMatCode
        '
        Me.FTRawMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatCode.Caption = "FTRawMatCode"
        Me.FTRawMatCode.FieldName = "FTRawMatCode"
        Me.FTRawMatCode.Name = "FTRawMatCode"
        Me.FTRawMatCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatCode.OptionsColumn.AllowMove = False
        Me.FTRawMatCode.OptionsColumn.AllowShowHide = False
        Me.FTRawMatCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatCode.OptionsColumn.ShowInCustomizationForm = False
        '
        'FTItemNo
        '
        Me.FTItemNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTItemNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTItemNo.Caption = "Item Number"
        Me.FTItemNo.FieldName = "FTCusItemCodeRef"
        Me.FTItemNo.Name = "FTItemNo"
        Me.FTItemNo.OptionsColumn.AllowEdit = False
        Me.FTItemNo.OptionsColumn.AllowMove = False
        Me.FTItemNo.OptionsColumn.AllowShowHide = False
        Me.FTItemNo.OptionsColumn.ReadOnly = True
        Me.FTItemNo.OptionsColumn.ShowInCustomizationForm = False
        Me.FTItemNo.Visible = True
        Me.FTItemNo.VisibleIndex = 0
        Me.FTItemNo.Width = 111
        '
        'FTRawMatName
        '
        Me.FTRawMatName.AppearanceCell.Options.UseTextOptions = True
        Me.FTRawMatName.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.FTRawMatName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatName.Caption = "Item Description"
        Me.FTRawMatName.FieldName = "FTRawMatName"
        Me.FTRawMatName.Name = "FTRawMatName"
        Me.FTRawMatName.OptionsColumn.AllowEdit = False
        Me.FTRawMatName.OptionsColumn.AllowMove = False
        Me.FTRawMatName.OptionsColumn.AllowShowHide = False
        Me.FTRawMatName.OptionsColumn.ReadOnly = True
        Me.FTRawMatName.OptionsColumn.ShowInCustomizationForm = False
        Me.FTRawMatName.Visible = True
        Me.FTRawMatName.VisibleIndex = 1
        Me.FTRawMatName.Width = 333
        '
        'FTRawMatColorCode
        '
        Me.FTRawMatColorCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatColorCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatColorCode.Caption = "Color Code"
        Me.FTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.FTRawMatColorCode.Name = "FTRawMatColorCode"
        Me.FTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorCode.OptionsColumn.AllowMove = False
        Me.FTRawMatColorCode.OptionsColumn.AllowShowHide = False
        Me.FTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatColorCode.OptionsColumn.ShowInCustomizationForm = False
        Me.FTRawMatColorCode.Visible = True
        Me.FTRawMatColorCode.VisibleIndex = 2
        Me.FTRawMatColorCode.Width = 117
        '
        'FTRawMatColorName
        '
        Me.FTRawMatColorName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatColorName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatColorName.Caption = "Color Description"
        Me.FTRawMatColorName.FieldName = "FTRawMatColorName"
        Me.FTRawMatColorName.Name = "FTRawMatColorName"
        Me.FTRawMatColorName.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorName.OptionsColumn.AllowMove = False
        Me.FTRawMatColorName.OptionsColumn.AllowShowHide = False
        Me.FTRawMatColorName.OptionsColumn.ReadOnly = True
        Me.FTRawMatColorName.OptionsColumn.ShowInCustomizationForm = False
        Me.FTRawMatColorName.Visible = True
        Me.FTRawMatColorName.VisibleIndex = 3
        Me.FTRawMatColorName.Width = 180
        '
        'FTRawMatSizeCode
        '
        Me.FTRawMatSizeCode.Caption = "Size"
        Me.FTRawMatSizeCode.FieldName = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.Name = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatSizeCode.OptionsColumn.AllowMove = False
        Me.FTRawMatSizeCode.OptionsColumn.AllowShowHide = False
        Me.FTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatSizeCode.Visible = True
        Me.FTRawMatSizeCode.VisibleIndex = 4
        Me.FTRawMatSizeCode.Width = 109
        '
        'FTUnitCode
        '
        Me.FTUnitCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUnitCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitCode.Caption = "UOM"
        Me.FTUnitCode.FieldName = "FTUnitCode"
        Me.FTUnitCode.Name = "FTUnitCode"
        Me.FTUnitCode.OptionsColumn.AllowEdit = False
        Me.FTUnitCode.OptionsColumn.AllowMove = False
        Me.FTUnitCode.OptionsColumn.AllowShowHide = False
        Me.FTUnitCode.OptionsColumn.ReadOnly = True
        Me.FTUnitCode.OptionsColumn.ShowInCustomizationForm = False
        Me.FTUnitCode.Visible = True
        Me.FTUnitCode.VisibleIndex = 5
        '
        'FNTotalRcvQty
        '
        Me.FNTotalRcvQty.AppearanceHeader.Options.UseTextOptions = True
        Me.FNTotalRcvQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNTotalRcvQty.Caption = "Total Receive Quantity"
        Me.FNTotalRcvQty.DisplayFormat.FormatString = "{0:n4}"
        Me.FNTotalRcvQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotalRcvQty.FieldName = "FNTotalRcvQty"
        Me.FNTotalRcvQty.Name = "FNTotalRcvQty"
        Me.FNTotalRcvQty.OptionsColumn.AllowEdit = False
        Me.FNTotalRcvQty.OptionsColumn.AllowMove = False
        Me.FNTotalRcvQty.OptionsColumn.AllowShowHide = False
        Me.FNTotalRcvQty.OptionsColumn.ReadOnly = True
        Me.FNTotalRcvQty.OptionsColumn.ShowInCustomizationForm = False
        Me.FNTotalRcvQty.Visible = True
        Me.FNTotalRcvQty.VisibleIndex = 6
        Me.FNTotalRcvQty.Width = 112
        '
        'FTBatchNo
        '
        Me.FTBatchNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTBatchNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTBatchNo.Caption = "Batch Number"
        Me.FTBatchNo.FieldName = "FTBatchNo"
        Me.FTBatchNo.Name = "FTBatchNo"
        Me.FTBatchNo.OptionsColumn.AllowEdit = False
        Me.FTBatchNo.OptionsColumn.AllowMove = False
        Me.FTBatchNo.OptionsColumn.AllowShowHide = False
        Me.FTBatchNo.OptionsColumn.ReadOnly = True
        Me.FTBatchNo.OptionsColumn.ShowInCustomizationForm = False
        Me.FTBatchNo.Visible = True
        Me.FTBatchNo.VisibleIndex = 7
        Me.FTBatchNo.Width = 123
        '
        'FTDyeLotNo
        '
        Me.FTDyeLotNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTDyeLotNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTDyeLotNo.Caption = "Dye Lot Number"
        Me.FTDyeLotNo.FieldName = "FTDyeLotNo"
        Me.FTDyeLotNo.Name = "FTDyeLotNo"
        Me.FTDyeLotNo.OptionsColumn.AllowEdit = False
        Me.FTDyeLotNo.OptionsColumn.AllowMove = False
        Me.FTDyeLotNo.OptionsColumn.AllowShowHide = False
        Me.FTDyeLotNo.OptionsColumn.ReadOnly = True
        Me.FTDyeLotNo.OptionsColumn.ShowInCustomizationForm = False
        Me.FTDyeLotNo.Width = 131
        '
        'FTStateQC
        '
        Me.FTStateQC.Caption = "FTStateQC"
        Me.FTStateQC.FieldName = "FTStateQC"
        Me.FTStateQC.Name = "FTStateQC"
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.Tag = "FNStateQC"
        Me.RepositoryItemCheckEdit1.ValueChecked = 1.0!
        Me.RepositoryItemCheckEdit1.ValueUnchecked = 0.0!
        '
        'RepositoryItemCalcEdit1
        '
        Me.RepositoryItemCalcEdit1.AutoHeight = False
        Me.RepositoryItemCalcEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEdit1.Name = "RepositoryItemCalcEdit1"
        Me.RepositoryItemCalcEdit1.Precision = 4
        '
        'RepositoryItemComboBox1
        '
        Me.RepositoryItemComboBox1.AutoHeight = False
        Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
        Me.RepositoryItemComboBox1.Tag = "FNStateQC"
        '
        'RepositoryItemCalcEdit2
        '
        Me.RepositoryItemCalcEdit2.AutoHeight = False
        Me.RepositoryItemCalcEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEdit2.Name = "RepositoryItemCalcEdit2"
        Me.RepositoryItemCalcEdit2.Precision = 4
        '
        'RepositoryItemCalcEdit3
        '
        Me.RepositoryItemCalcEdit3.AutoHeight = False
        Me.RepositoryItemCalcEdit3.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEdit3.Name = "RepositoryItemCalcEdit3"
        Me.RepositoryItemCalcEdit3.Precision = 4
        '
        'RepositoryItemCalcEdit4
        '
        Me.RepositoryItemCalcEdit4.AllowFocused = False
        Me.RepositoryItemCalcEdit4.AutoHeight = False
        Me.RepositoryItemCalcEdit4.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEdit4.Name = "RepositoryItemCalcEdit4"
        Me.RepositoryItemCalcEdit4.Precision = 4
        '
        'wQCFabricInspecAddRawmat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1268, 330)
        Me.Controls.Add(Me.ogcrawmat)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wQCFabricInspecAddRawmat"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wQCFabricInspecAddRawmat"
        CType(Me.ogcrawmat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvrawmat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcrawmat As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvrawmat As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTFactory As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysRawMatId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTItemNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatColorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTotalRcvQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTBatchNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDyeLotNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCalcEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents RepositoryItemCalcEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepositoryItemCalcEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepositoryItemCalcEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FTStateQC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
End Class
