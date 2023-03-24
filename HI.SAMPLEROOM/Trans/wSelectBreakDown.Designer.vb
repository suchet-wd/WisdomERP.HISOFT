<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wSelectBreakDown
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
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcopy = New DevExpress.XtraEditors.SimpleButton()
        Me.ogborderprod = New DevExpress.XtraEditors.GroupControl()
        Me.ogdBreakdown = New DevExpress.XtraGrid.GridControl()
        Me.ogvBreakdown = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFTSizeBreakDown = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFNQuantity = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.CFTColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTColorway = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.CFTDeliveryDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTDeliveryDate = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.CFNSEQ = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.ogborderprod, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogborderprod.SuspendLayout()
        CType(Me.ogdBreakdown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvBreakdown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFNQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTColorway, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTDeliveryDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTDeliveryDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(394, 340)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(141, 31)
        Me.ocmcancel.TabIndex = 312
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmcopy
        '
        Me.ocmcopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcopy.Location = New System.Drawing.Point(149, 340)
        Me.ocmcopy.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcopy.Name = "ocmcopy"
        Me.ocmcopy.Size = New System.Drawing.Size(138, 31)
        Me.ocmcopy.TabIndex = 311
        Me.ocmcopy.TabStop = False
        Me.ocmcopy.Tag = "2|"
        Me.ocmcopy.Text = "Save"
        '
        'ogborderprod
        '
        Me.ogborderprod.Controls.Add(Me.ogdBreakdown)
        Me.ogborderprod.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogborderprod.Location = New System.Drawing.Point(0, 0)
        Me.ogborderprod.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogborderprod.Name = "ogborderprod"
        Me.ogborderprod.Size = New System.Drawing.Size(623, 323)
        Me.ogborderprod.TabIndex = 313
        Me.ogborderprod.Text = "Breakdown Detail"
        '
        'ogdBreakdown
        '
        Me.ogdBreakdown.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdBreakdown.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdBreakdown.Location = New System.Drawing.Point(2, 28)
        Me.ogdBreakdown.MainView = Me.ogvBreakdown
        Me.ogdBreakdown.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdBreakdown.Name = "ogdBreakdown"
        Me.ogdBreakdown.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect, Me.RepFNQuantity, Me.RepFTColorway, Me.RepFTDeliveryDate, Me.RepositoryItemTextEdit1})
        Me.ogdBreakdown.Size = New System.Drawing.Size(619, 293)
        Me.ogdBreakdown.TabIndex = 528
        Me.ogdBreakdown.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvBreakdown})
        '
        'ogvBreakdown
        '
        Me.ogvBreakdown.Appearance.EvenRow.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ogvBreakdown.Appearance.EvenRow.Options.UseBackColor = True
        Me.ogvBreakdown.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTSizeBreakDown, Me.CFNQuantity, Me.CFTColorway, Me.CFTDeliveryDate, Me.GridColumn1, Me.CFNSEQ, Me.FTSelect})
        Me.ogvBreakdown.GridControl = Me.ogdBreakdown
        Me.ogvBreakdown.Name = "ogvBreakdown"
        Me.ogvBreakdown.OptionsView.ColumnAutoWidth = False
        Me.ogvBreakdown.OptionsView.ShowGroupPanel = False
        '
        'CFTSizeBreakDown
        '
        Me.CFTSizeBreakDown.Caption = "Size"
        Me.CFTSizeBreakDown.FieldName = "FTSizeBreakDown"
        Me.CFTSizeBreakDown.MaxWidth = 100
        Me.CFTSizeBreakDown.MinWidth = 100
        Me.CFTSizeBreakDown.Name = "CFTSizeBreakDown"
        Me.CFTSizeBreakDown.OptionsColumn.AllowEdit = False
        Me.CFTSizeBreakDown.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSizeBreakDown.OptionsColumn.AllowMove = False
        Me.CFTSizeBreakDown.OptionsColumn.AllowShowHide = False
        Me.CFTSizeBreakDown.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSizeBreakDown.OptionsColumn.FixedWidth = True
        Me.CFTSizeBreakDown.OptionsColumn.ReadOnly = True
        Me.CFTSizeBreakDown.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTSizeBreakDown.Visible = True
        Me.CFTSizeBreakDown.VisibleIndex = 1
        Me.CFTSizeBreakDown.Width = 100
        '
        'CFNQuantity
        '
        Me.CFNQuantity.Caption = "จำนวน"
        Me.CFNQuantity.ColumnEdit = Me.RepFNQuantity
        Me.CFNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQuantity.FieldName = "FNQuantity"
        Me.CFNQuantity.MaxWidth = 100
        Me.CFNQuantity.MinWidth = 100
        Me.CFNQuantity.Name = "CFNQuantity"
        Me.CFNQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNQuantity.OptionsColumn.AllowMove = False
        Me.CFNQuantity.OptionsColumn.AllowShowHide = False
        Me.CFNQuantity.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNQuantity.OptionsColumn.FixedWidth = True
        Me.CFNQuantity.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNQuantity.Visible = True
        Me.CFNQuantity.VisibleIndex = 3
        Me.CFNQuantity.Width = 100
        '
        'RepFNQuantity
        '
        Me.RepFNQuantity.AutoHeight = False
        Me.RepFNQuantity.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepFNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.RepFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepFNQuantity.EditFormat.FormatString = "{0:n0}"
        Me.RepFNQuantity.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepFNQuantity.Name = "RepFNQuantity"
        Me.RepFNQuantity.Precision = 0
        '
        'CFTColorway
        '
        Me.CFTColorway.Caption = "Colorway"
        Me.CFTColorway.ColumnEdit = Me.RepFTColorway
        Me.CFTColorway.FieldName = "FTColorway"
        Me.CFTColorway.Name = "CFTColorway"
        Me.CFTColorway.OptionsColumn.AllowEdit = False
        Me.CFTColorway.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTColorway.OptionsColumn.AllowMove = False
        Me.CFTColorway.OptionsColumn.AllowShowHide = False
        Me.CFTColorway.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTColorway.OptionsColumn.ReadOnly = True
        Me.CFTColorway.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTColorway.Visible = True
        Me.CFTColorway.VisibleIndex = 2
        Me.CFTColorway.Width = 127
        '
        'RepFTColorway
        '
        Me.RepFTColorway.AutoHeight = False
        Me.RepFTColorway.MaxLength = 30
        Me.RepFTColorway.Name = "RepFTColorway"
        '
        'CFTDeliveryDate
        '
        Me.CFTDeliveryDate.Caption = "กำหนดส่ง"
        Me.CFTDeliveryDate.ColumnEdit = Me.RepFTDeliveryDate
        Me.CFTDeliveryDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.CFTDeliveryDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.CFTDeliveryDate.FieldName = "FTDeliveryDate"
        Me.CFTDeliveryDate.Name = "CFTDeliveryDate"
        Me.CFTDeliveryDate.OptionsColumn.AllowEdit = False
        Me.CFTDeliveryDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTDeliveryDate.OptionsColumn.AllowMove = False
        Me.CFTDeliveryDate.OptionsColumn.AllowShowHide = False
        Me.CFTDeliveryDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTDeliveryDate.OptionsColumn.ReadOnly = True
        Me.CFTDeliveryDate.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTDeliveryDate.Width = 135
        '
        'RepFTDeliveryDate
        '
        Me.RepFTDeliveryDate.AutoHeight = False
        Me.RepFTDeliveryDate.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepFTDeliveryDate.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepFTDeliveryDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.RepFTDeliveryDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepFTDeliveryDate.EditFormat.FormatString = "dd/MM/yyyy"
        Me.RepFTDeliveryDate.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepFTDeliveryDate.Name = "RepFTDeliveryDate"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Remark"
        Me.GridColumn1.ColumnEdit = Me.RepositoryItemTextEdit1
        Me.GridColumn1.FieldName = "FTRemark"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        Me.GridColumn1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn1.OptionsColumn.AllowMove = False
        Me.GridColumn1.OptionsColumn.AllowShowHide = False
        Me.GridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GridColumn1.OptionsColumn.ReadOnly = True
        Me.GridColumn1.OptionsColumn.ShowInCustomizationForm = False
        Me.GridColumn1.Width = 665
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.MaxLength = 500
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'CFNSEQ
        '
        Me.CFNSEQ.Caption = "FNSeq"
        Me.CFNSEQ.FieldName = "FNSeq"
        Me.CFNSEQ.Name = "CFNSEQ"
        Me.CFNSEQ.OptionsColumn.AllowEdit = False
        Me.CFNSEQ.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNSEQ.OptionsColumn.AllowMove = False
        Me.CFNSEQ.OptionsColumn.AllowShowHide = False
        Me.CFNSEQ.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNSEQ.OptionsColumn.ReadOnly = True
        Me.CFNSEQ.OptionsColumn.ShowInCustomizationForm = False
        '
        'FTSelect
        '
        Me.FTSelect.Caption = "#"
        Me.FTSelect.ColumnEdit = Me.RepFTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.MinWidth = 25
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 64
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'wSelectBreakDown
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(623, 384)
        Me.Controls.Add(Me.ogborderprod)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmcopy)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wSelectBreakDown"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select BreakDown"
        CType(Me.ogborderprod, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogborderprod.ResumeLayout(False)
        CType(Me.ogdBreakdown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvBreakdown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFNQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTColorway, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTDeliveryDate.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTDeliveryDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcopy As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogborderprod As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogdBreakdown As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvBreakdown As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CFTSizeBreakDown As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFNQuantity As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents CFTColorway As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTColorway As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents CFTDeliveryDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTDeliveryDate As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents CFNSEQ As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
End Class
