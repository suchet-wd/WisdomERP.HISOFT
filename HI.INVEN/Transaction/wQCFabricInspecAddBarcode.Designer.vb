<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wQCFabricInspecAddBarcode
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
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTRollNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysRawMatId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTBatchNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDocumentNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTFabricFrontSize = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCalcEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.RepositoryItemCalcEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.RepositoryItemCalcEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.RepositoryItemCalcEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.FTStateSelectall = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.ogcrawmat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvrawmat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateSelectall.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcrawmat
        '
        Me.ogcrawmat.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcrawmat.Location = New System.Drawing.Point(3, 3)
        Me.ogcrawmat.MainView = Me.ogvrawmat
        Me.ogcrawmat.Name = "ogcrawmat"
        Me.ogcrawmat.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCalcEdit1, Me.RepositoryItemComboBox1, Me.RepositoryItemCalcEdit2, Me.RepositoryItemCalcEdit3, Me.RepositoryItemCalcEdit4, Me.RepFTSelect})
        Me.ogcrawmat.Size = New System.Drawing.Size(421, 427)
        Me.ogcrawmat.TabIndex = 3
        Me.ogcrawmat.Tag = "3|"
        Me.ogcrawmat.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvrawmat})
        '
        'ogvrawmat
        '
        Me.ogvrawmat.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.FTRollNo, Me.FNHSysRawMatId, Me.FNQuantity, Me.FTBatchNo, Me.FTDocumentNo, Me.CFTFabricFrontSize})
        Me.ogvrawmat.GridControl = Me.ogcrawmat
        Me.ogvrawmat.Name = "ogvrawmat"
        Me.ogvrawmat.OptionsView.ColumnAutoWidth = False
        Me.ogvrawmat.OptionsView.ShowGroupPanel = False
        '
        'FTSelect
        '
        Me.FTSelect.Caption = " "
        Me.FTSelect.ColumnEdit = Me.RepFTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Caption = "Check"
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'FTRollNo
        '
        Me.FTRollNo.AppearanceCell.Options.UseTextOptions = True
        Me.FTRollNo.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRollNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRollNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRollNo.Caption = "Roll#"
        Me.FTRollNo.FieldName = "FTRollNo"
        Me.FTRollNo.Name = "FTRollNo"
        Me.FTRollNo.OptionsColumn.AllowEdit = False
        Me.FTRollNo.OptionsColumn.ReadOnly = True
        Me.FTRollNo.Visible = True
        Me.FTRollNo.VisibleIndex = 1
        Me.FTRollNo.Width = 97
        '
        'FNHSysRawMatId
        '
        Me.FNHSysRawMatId.AppearanceHeader.Options.UseTextOptions = True
        Me.FNHSysRawMatId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNHSysRawMatId.Caption = "FNHSysRawMatId"
        Me.FNHSysRawMatId.FieldName = "FNHSysRawMatId"
        Me.FNHSysRawMatId.Name = "FNHSysRawMatId"
        Me.FNHSysRawMatId.OptionsColumn.AllowEdit = False
        Me.FNHSysRawMatId.OptionsColumn.ReadOnly = True
        '
        'FNQuantity
        '
        Me.FNQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantity.AppearanceHeader.Options.UseTextOptions = True
        Me.FNQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNQuantity.Caption = "Quantity"
        Me.FNQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNQuantity"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.OptionsColumn.ReadOnly = True
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 2
        Me.FNQuantity.Width = 166
        '
        'FTBatchNo
        '
        Me.FTBatchNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTBatchNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTBatchNo.Caption = "FTBatchNo"
        Me.FTBatchNo.FieldName = "FTBatchNo"
        Me.FTBatchNo.Name = "FTBatchNo"
        Me.FTBatchNo.OptionsColumn.AllowEdit = False
        Me.FTBatchNo.OptionsColumn.ReadOnly = True
        '
        'FTDocumentNo
        '
        Me.FTDocumentNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTDocumentNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTDocumentNo.Caption = "FTDocumentNo"
        Me.FTDocumentNo.FieldName = "FTDocumentNo"
        Me.FTDocumentNo.Name = "FTDocumentNo"
        Me.FTDocumentNo.OptionsColumn.AllowEdit = False
        Me.FTDocumentNo.OptionsColumn.ReadOnly = True
        '
        'CFTFabricFrontSize
        '
        Me.CFTFabricFrontSize.Caption = "Fabric Front Size"
        Me.CFTFabricFrontSize.FieldName = "FTFabricFrontSize"
        Me.CFTFabricFrontSize.Name = "CFTFabricFrontSize"
        Me.CFTFabricFrontSize.OptionsColumn.AllowEdit = False
        Me.CFTFabricFrontSize.OptionsColumn.ReadOnly = True
        Me.CFTFabricFrontSize.Visible = True
        Me.CFTFabricFrontSize.VisibleIndex = 3
        Me.CFTFabricFrontSize.Width = 107
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.Tag = "FNStateQC"
        Me.RepositoryItemCheckEdit1.ValueChecked = 1.0!
        Me.RepositoryItemCheckEdit1.ValueUnchecked = 0!
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
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(242, 455)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(160, 24)
        Me.ocmcancel.TabIndex = 103
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmok
        '
        Me.ocmok.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmok.Location = New System.Drawing.Point(24, 456)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(160, 24)
        Me.ocmok.TabIndex = 102
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'FTStateSelectall
        '
        Me.FTStateSelectall.EditValue = "0"
        Me.FTStateSelectall.Location = New System.Drawing.Point(22, 433)
        Me.FTStateSelectall.Name = "FTStateSelectall"
        Me.FTStateSelectall.Properties.Caption = "เลือกทั้งหมด"
        Me.FTStateSelectall.Properties.ValueChecked = "1"
        Me.FTStateSelectall.Properties.ValueUnchecked = "0"
        Me.FTStateSelectall.Size = New System.Drawing.Size(380, 20)
        Me.FTStateSelectall.TabIndex = 104
        Me.FTStateSelectall.Tag = "2|"
        '
        'wQCFabricInspecAddBarcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(428, 489)
        Me.Controls.Add(Me.FTStateSelectall)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmok)
        Me.Controls.Add(Me.ogcrawmat)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wQCFabricInspecAddBarcode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wQCFabricInspecAddBarcode"
        CType(Me.ogcrawmat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvrawmat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateSelectall.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcrawmat As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvrawmat As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTRollNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysRawMatId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTBatchNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDocumentNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCalcEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents RepositoryItemCalcEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepositoryItemCalcEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepositoryItemCalcEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTStateSelectall As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents CFTFabricFrontSize As DevExpress.XtraGrid.Columns.GridColumn
End Class
