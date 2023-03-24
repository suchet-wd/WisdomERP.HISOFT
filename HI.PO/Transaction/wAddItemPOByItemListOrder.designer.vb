<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wAddItemPOByItemListOrder
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
        Me.ocmadd = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcjob = New DevExpress.XtraGrid.GridControl()
        Me.ogvjob = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTOrderNo = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.XFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryQuantity = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTStateSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogcjob, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvjob, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmadd
        '
        Me.ocmadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmadd.Location = New System.Drawing.Point(34, 439)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(115, 27)
        Me.ocmadd.TabIndex = 8
        Me.ocmadd.Text = "ADD ORDER"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(213, 439)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(115, 27)
        Me.ocmcancel.TabIndex = 283
        Me.ocmcancel.Text = "CANCEL"
        '
        'ogcjob
        '
        Me.ogcjob.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogcjob.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.ogcjob.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.ogcjob.EmbeddedNavigator.Buttons.First.Visible = False
        Me.ogcjob.EmbeddedNavigator.Buttons.Last.Visible = False
        Me.ogcjob.EmbeddedNavigator.Buttons.Next.Visible = False
        Me.ogcjob.EmbeddedNavigator.Buttons.NextPage.Visible = False
        Me.ogcjob.EmbeddedNavigator.Buttons.Prev.Visible = False
        Me.ogcjob.EmbeddedNavigator.Buttons.PrevPage.Visible = False
        Me.ogcjob.EmbeddedNavigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.None
        Me.ogcjob.EmbeddedNavigator.TextStringFormat = ""
        Me.ogcjob.Location = New System.Drawing.Point(0, 0)
        Me.ogcjob.MainView = Me.ogvjob
        Me.ogcjob.Name = "ogcjob"
        Me.ogcjob.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryFTOrderNo, Me.RepositoryQuantity})
        Me.ogcjob.Size = New System.Drawing.Size(370, 430)
        Me.ogcjob.TabIndex = 565
        Me.ogcjob.Tag = "3|"
        Me.ogcjob.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvjob})
        '
        'ogvjob
        '
        Me.ogvjob.Appearance.EvenRow.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ogvjob.Appearance.EvenRow.Options.UseBackColor = True
        Me.ogvjob.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTStateSelect, Me.CFTOrderNo, Me.CFNQuantity})
        Me.ogvjob.GridControl = Me.ogcjob
        Me.ogvjob.Name = "ogvjob"
        Me.ogvjob.OptionsView.ColumnAutoWidth = False
        Me.ogvjob.OptionsView.ShowGroupPanel = False
        Me.ogvjob.Tag = "3|"
        '
        'CFTOrderNo
        '
        Me.CFTOrderNo.Caption = "Order No"
        Me.CFTOrderNo.FieldName = "FTOrderNo"
        Me.CFTOrderNo.Name = "CFTOrderNo"
        Me.CFTOrderNo.OptionsColumn.AllowEdit = False
        Me.CFTOrderNo.OptionsColumn.ReadOnly = True
        Me.CFTOrderNo.Visible = True
        Me.CFTOrderNo.VisibleIndex = 1
        Me.CFTOrderNo.Width = 115
        '
        'RepositoryFTOrderNo
        '
        Me.RepositoryFTOrderNo.AllowNullInput = DevExpress.Utils.DefaultBoolean.[True]
        Me.RepositoryFTOrderNo.AutoHeight = False
        Me.RepositoryFTOrderNo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryFTOrderNo.DisplayMember = "FTOrderNo"
        Me.RepositoryFTOrderNo.Name = "RepositoryFTOrderNo"
        Me.RepositoryFTOrderNo.NullText = ""
        Me.RepositoryFTOrderNo.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.RepositoryFTOrderNo.PopupFormSize = New System.Drawing.Size(120, 0)
        Me.RepositoryFTOrderNo.ValueMember = "FTOrderNo"
        Me.RepositoryFTOrderNo.View = Me.GridView1
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.XFTOrderNo})
        Me.GridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        '
        'XFTOrderNo
        '
        Me.XFTOrderNo.Caption = "OrderNo"
        Me.XFTOrderNo.FieldName = "FTOrderNo"
        Me.XFTOrderNo.Name = "XFTOrderNo"
        Me.XFTOrderNo.OptionsColumn.AllowEdit = False
        Me.XFTOrderNo.OptionsColumn.ReadOnly = True
        Me.XFTOrderNo.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.XFTOrderNo.Visible = True
        Me.XFTOrderNo.VisibleIndex = 0
        Me.XFTOrderNo.Width = 120
        '
        'CFNQuantity
        '
        Me.CFNQuantity.Caption = "Quantity"
        Me.CFNQuantity.ColumnEdit = Me.RepositoryQuantity
        Me.CFNQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.CFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQuantity.FieldName = "FNQuantity"
        Me.CFNQuantity.Name = "CFNQuantity"
        Me.CFNQuantity.Visible = True
        Me.CFNQuantity.VisibleIndex = 2
        Me.CFNQuantity.Width = 150
        '
        'RepositoryQuantity
        '
        Me.RepositoryQuantity.AutoHeight = False
        Me.RepositoryQuantity.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.RepositoryQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryQuantity.EditFormat.FormatString = "{0:n4}"
        Me.RepositoryQuantity.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryQuantity.Name = "RepositoryQuantity"
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'FTStateSelect
        '
        Me.FTStateSelect.Caption = "Select"
        Me.FTStateSelect.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.FTStateSelect.FieldName = "FTStateSelect"
        Me.FTStateSelect.Name = "FTStateSelect"
        Me.FTStateSelect.Visible = True
        Me.FTStateSelect.VisibleIndex = 0
        Me.FTStateSelect.Width = 57
        '
        'wAddItemPOByItemListOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(370, 478)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogcjob)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmadd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wAddItemPOByItemListOrder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Order"
        CType(Me.ogcjob, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvjob, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcjob As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvjob As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTOrderNo As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents XFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryQuantity As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTStateSelect As DevExpress.XtraGrid.Columns.GridColumn
End Class
