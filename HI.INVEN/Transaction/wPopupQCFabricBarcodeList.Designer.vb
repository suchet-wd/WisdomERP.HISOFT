<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPopupQCFabricBarcodeList
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
        Me.oBtnEnter = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNYardNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNSize = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNPoint = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'oBtnEnter
        '
        Me.oBtnEnter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.oBtnEnter.Appearance.Font = New System.Drawing.Font("Tahoma", 20.0!, System.Drawing.FontStyle.Bold)
        Me.oBtnEnter.Appearance.Options.UseFont = True
        Me.oBtnEnter.Location = New System.Drawing.Point(237, 367)
        Me.oBtnEnter.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oBtnEnter.Name = "oBtnEnter"
        Me.oBtnEnter.Size = New System.Drawing.Size(183, 51)
        Me.oBtnEnter.TabIndex = 1
        Me.oBtnEnter.Text = "DELETE"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SimpleButton1.Appearance.Font = New System.Drawing.Font("Tahoma", 20.0!, System.Drawing.FontStyle.Bold)
        Me.SimpleButton1.Appearance.Options.UseFont = True
        Me.SimpleButton1.Location = New System.Drawing.Point(597, 367)
        Me.SimpleButton1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(183, 51)
        Me.SimpleButton1.TabIndex = 2
        Me.SimpleButton1.Text = "CANCEL"
        '
        'ogc
        '
        Me.ogc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogc.Location = New System.Drawing.Point(3, 4)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect})
        Me.ogc.Size = New System.Drawing.Size(978, 351)
        Me.ogc.TabIndex = 5
        Me.ogc.TabStop = False
        Me.ogc.Tag = "2|"
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Appearance.ColumnFilterButton.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.ColumnFilterButton.Options.UseFont = True
        Me.ogv.Appearance.ColumnFilterButtonActive.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.ColumnFilterButtonActive.Options.UseFont = True
        Me.ogv.Appearance.CustomizationFormHint.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.CustomizationFormHint.Options.UseFont = True
        Me.ogv.Appearance.DetailTip.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.DetailTip.Options.UseFont = True
        Me.ogv.Appearance.Empty.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.Empty.Options.UseFont = True
        Me.ogv.Appearance.EvenRow.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.ogv.Appearance.EvenRow.Options.UseFont = True
        Me.ogv.Appearance.FilterCloseButton.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.FilterCloseButton.Options.UseFont = True
        Me.ogv.Appearance.FilterPanel.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.FilterPanel.Options.UseFont = True
        Me.ogv.Appearance.FixedLine.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.ogv.Appearance.FixedLine.Options.UseFont = True
        Me.ogv.Appearance.FocusedCell.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.ogv.Appearance.FocusedCell.Options.UseFont = True
        Me.ogv.Appearance.FocusedRow.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!)
        Me.ogv.Appearance.FocusedRow.Options.UseFont = True
        Me.ogv.Appearance.FooterPanel.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.FooterPanel.Options.UseFont = True
        Me.ogv.Appearance.GroupButton.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.GroupButton.Options.UseFont = True
        Me.ogv.Appearance.GroupFooter.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.GroupFooter.Options.UseFont = True
        Me.ogv.Appearance.GroupPanel.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.GroupPanel.Options.UseFont = True
        Me.ogv.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.GroupRow.Options.UseFont = True
        Me.ogv.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.HeaderPanel.Options.UseFont = True
        Me.ogv.Appearance.HideSelectionRow.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.HideSelectionRow.Options.UseFont = True
        Me.ogv.Appearance.HorzLine.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.HorzLine.Options.UseFont = True
        Me.ogv.Appearance.OddRow.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.ogv.Appearance.OddRow.Options.UseFont = True
        Me.ogv.Appearance.Preview.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.Preview.Options.UseFont = True
        Me.ogv.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ogv.Appearance.Row.Options.UseFont = True
        Me.ogv.Appearance.RowSeparator.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.RowSeparator.Options.UseFont = True
        Me.ogv.Appearance.SelectedRow.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.SelectedRow.Options.UseFont = True
        Me.ogv.Appearance.TopNewRow.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.TopNewRow.Options.UseFont = True
        Me.ogv.Appearance.VertLine.Font = New System.Drawing.Font("Tahoma", 10.2!)
        Me.ogv.Appearance.VertLine.Options.UseFont = True
        Me.ogv.Appearance.ViewCaption.Font = New System.Drawing.Font("Tahoma", 15.0!)
        Me.ogv.Appearance.ViewCaption.Options.UseFont = True
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFNSeq, Me.CFNYardNo, Me.CFNSize, Me.CFNPoint, Me.CFTRemark})
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowGroup = False
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'CFNSeq
        '
        Me.CFNSeq.Caption = "ลำดับที่"
        Me.CFNSeq.FieldName = "FNSeq"
        Me.CFNSeq.Name = "CFNSeq"
        Me.CFNSeq.OptionsColumn.AllowEdit = False
        Me.CFNSeq.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNSeq.OptionsColumn.AllowMove = False
        Me.CFNSeq.OptionsColumn.AllowShowHide = False
        Me.CFNSeq.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNSeq.OptionsColumn.ReadOnly = True
        Me.CFNSeq.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNSeq.Visible = True
        Me.CFNSeq.VisibleIndex = 0
        Me.CFNSeq.Width = 121
        '
        'CFNYardNo
        '
        Me.CFNYardNo.Caption = "หลาที่"
        Me.CFNYardNo.FieldName = "FNYardNo"
        Me.CFNYardNo.Name = "CFNYardNo"
        Me.CFNYardNo.OptionsColumn.AllowEdit = False
        Me.CFNYardNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNYardNo.OptionsColumn.AllowMove = False
        Me.CFNYardNo.OptionsColumn.AllowShowHide = False
        Me.CFNYardNo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNYardNo.OptionsColumn.ReadOnly = True
        Me.CFNYardNo.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNYardNo.Visible = True
        Me.CFNYardNo.VisibleIndex = 1
        Me.CFNYardNo.Width = 158
        '
        'CFNSize
        '
        Me.CFNSize.Caption = "ขนาด"
        Me.CFNSize.FieldName = "FNSize"
        Me.CFNSize.Name = "CFNSize"
        Me.CFNSize.OptionsColumn.AllowEdit = False
        Me.CFNSize.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNSize.OptionsColumn.AllowMove = False
        Me.CFNSize.OptionsColumn.AllowShowHide = False
        Me.CFNSize.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNSize.OptionsColumn.ReadOnly = True
        Me.CFNSize.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNSize.Visible = True
        Me.CFNSize.VisibleIndex = 2
        Me.CFNSize.Width = 170
        '
        'CFNPoint
        '
        Me.CFNPoint.Caption = "คะแนน"
        Me.CFNPoint.FieldName = "FNPoint"
        Me.CFNPoint.Name = "CFNPoint"
        Me.CFNPoint.OptionsColumn.AllowEdit = False
        Me.CFNPoint.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNPoint.OptionsColumn.AllowMove = False
        Me.CFNPoint.OptionsColumn.AllowShowHide = False
        Me.CFNPoint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNPoint.OptionsColumn.ReadOnly = True
        Me.CFNPoint.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNPoint.Visible = True
        Me.CFNPoint.VisibleIndex = 3
        Me.CFNPoint.Width = 198
        '
        'CFTRemark
        '
        Me.CFTRemark.Caption = "หมายเหตุ"
        Me.CFTRemark.FieldName = "FTRemark"
        Me.CFTRemark.Name = "CFTRemark"
        Me.CFTRemark.OptionsColumn.AllowEdit = False
        Me.CFTRemark.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTRemark.OptionsColumn.AllowMove = False
        Me.CFTRemark.OptionsColumn.AllowShowHide = False
        Me.CFTRemark.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTRemark.OptionsColumn.ReadOnly = True
        Me.CFTRemark.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTRemark.Visible = True
        Me.CFTRemark.VisibleIndex = 4
        Me.CFTRemark.Width = 479
        '
        'RepositoryFTSelect
        '
        Me.RepositoryFTSelect.AutoHeight = False
        Me.RepositoryFTSelect.Caption = "Check"
        Me.RepositoryFTSelect.Name = "RepositoryFTSelect"
        Me.RepositoryFTSelect.ValueChecked = "1"
        Me.RepositoryFTSelect.ValueUnchecked = "0"
        '
        'wPopupQCFabricBarcodeList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(978, 431)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogc)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.oBtnEnter)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wPopupQCFabricBarcodeList"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "QC Fabric Defect List"
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents oBtnEnter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CFNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNYardNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNSize As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNPoint As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTRemark As DevExpress.XtraGrid.Columns.GridColumn
End Class
