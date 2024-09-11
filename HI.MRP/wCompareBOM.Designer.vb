<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wCompareBOM
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
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.gcCompare = New DevExpress.XtraEditors.GroupControl()
        Me.ogcCompare = New DevExpress.XtraGrid.GridControl()
        Me.ogvlisting = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepCheckEdit = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.gcNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcColorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcUnit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcStyle = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcFabricWidth = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcVendor = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcSeason = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.gcCompare, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gcCompare.SuspendLayout()
        CType(Me.ogcCompare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvlisting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepCheckEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl2
        '
        Me.GroupControl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl2.Controls.Add(Me.ocmcancel)
        Me.GroupControl2.Controls.Add(Me.ocmok)
        Me.GroupControl2.Location = New System.Drawing.Point(3, 532)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.ShowCaption = False
        Me.GroupControl2.Size = New System.Drawing.Size(1182, 41)
        Me.GroupControl2.TabIndex = 288
        Me.GroupControl2.Text = "GroupControl2"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(911, 9)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(145, 25)
        Me.ocmcancel.TabIndex = 107
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmok
        '
        Me.ocmok.Location = New System.Drawing.Point(139, 9)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(133, 25)
        Me.ocmok.TabIndex = 106
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'gcCompare
        '
        Me.gcCompare.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gcCompare.Controls.Add(Me.ogcCompare)
        Me.gcCompare.Location = New System.Drawing.Point(3, 6)
        Me.gcCompare.Name = "gcCompare"
        Me.gcCompare.Size = New System.Drawing.Size(1182, 523)
        Me.gcCompare.TabIndex = 287
        Me.gcCompare.Text = "Style"
        '
        'ogcCompare
        '
        Me.ogcCompare.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcCompare.Location = New System.Drawing.Point(2, 23)
        Me.ogcCompare.MainView = Me.ogvlisting
        Me.ogcCompare.Name = "ogcCompare"
        Me.ogcCompare.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepCheckEdit})
        Me.ogcCompare.Size = New System.Drawing.Size(1178, 498)
        Me.ogcCompare.TabIndex = 1
        Me.ogcCompare.TabStop = False
        Me.ogcCompare.Tag = "2|"
        Me.ogcCompare.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvlisting})
        '
        'ogvlisting
        '
        Me.ogvlisting.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gcNo, Me.gcRemark, Me.gcItem, Me.gcDescription, Me.gcFabricWidth, Me.gcVendor, Me.gcColorCode, Me.gcColorName, Me.gcUnit, Me.gcStyle, Me.gcSeason})
        Me.ogvlisting.GridControl = Me.ogcCompare
        Me.ogvlisting.Name = "ogvlisting"
        Me.ogvlisting.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvlisting.OptionsView.ColumnAutoWidth = False
        Me.ogvlisting.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvlisting.OptionsView.ShowGroupPanel = False
        Me.ogvlisting.Tag = "2|"
        '
        'RepCheckEdit
        '
        Me.RepCheckEdit.AutoHeight = False
        Me.RepCheckEdit.Caption = "Check"
        Me.RepCheckEdit.Name = "RepCheckEdit"
        Me.RepCheckEdit.ValueChecked = "1"
        Me.RepCheckEdit.ValueUnchecked = "0"
        '
        'gcNo
        '
        Me.gcNo.Caption = "gcNo"
        Me.gcNo.FieldName = "gcNo"
        Me.gcNo.Name = "gcNo"
        Me.gcNo.OptionsColumn.AllowEdit = False
        Me.gcNo.Visible = True
        Me.gcNo.VisibleIndex = 0
        '
        'gcRemark
        '
        Me.gcRemark.Caption = "gcRemark"
        Me.gcRemark.FieldName = "gcRemark"
        Me.gcRemark.Name = "gcRemark"
        Me.gcRemark.OptionsColumn.AllowEdit = False
        Me.gcRemark.Visible = True
        Me.gcRemark.VisibleIndex = 1
        Me.gcRemark.Width = 71
        '
        'gcItem
        '
        Me.gcItem.Caption = "gcItem"
        Me.gcItem.FieldName = "gcItem"
        Me.gcItem.Name = "gcItem"
        Me.gcItem.OptionsColumn.AllowEdit = False
        Me.gcItem.Visible = True
        Me.gcItem.VisibleIndex = 2
        '
        'gcDescription
        '
        Me.gcDescription.Caption = "gcDescription"
        Me.gcDescription.FieldName = "gcDescription"
        Me.gcDescription.Name = "gcDescription"
        Me.gcDescription.OptionsColumn.AllowEdit = False
        Me.gcDescription.Visible = True
        Me.gcDescription.VisibleIndex = 3
        Me.gcDescription.Width = 147
        '
        'gcColorCode
        '
        Me.gcColorCode.Caption = "gcColorCode"
        Me.gcColorCode.FieldName = "gcColorCode"
        Me.gcColorCode.Name = "gcColorCode"
        Me.gcColorCode.OptionsColumn.AllowEdit = False
        Me.gcColorCode.Visible = True
        Me.gcColorCode.VisibleIndex = 6
        Me.gcColorCode.Width = 101
        '
        'gcColorName
        '
        Me.gcColorName.Caption = "gcColorName"
        Me.gcColorName.FieldName = "gcColorName"
        Me.gcColorName.Name = "gcColorName"
        Me.gcColorName.OptionsColumn.AllowEdit = False
        Me.gcColorName.Visible = True
        Me.gcColorName.VisibleIndex = 7
        Me.gcColorName.Width = 215
        '
        'gcUnit
        '
        Me.gcUnit.Caption = "gcUnit"
        Me.gcUnit.FieldName = "gcUnit"
        Me.gcUnit.Name = "gcUnit"
        Me.gcUnit.OptionsColumn.AllowEdit = False
        Me.gcUnit.Visible = True
        Me.gcUnit.VisibleIndex = 8
        '
        'gcStyle
        '
        Me.gcStyle.Caption = "gcStyle"
        Me.gcStyle.FieldName = "gcStyle"
        Me.gcStyle.Name = "gcStyle"
        Me.gcStyle.OptionsColumn.AllowEdit = False
        Me.gcStyle.Visible = True
        Me.gcStyle.VisibleIndex = 9
        '
        'gcFabricWidth
        '
        Me.gcFabricWidth.Caption = "gcFabricWidth"
        Me.gcFabricWidth.FieldName = "gcFabricWidth"
        Me.gcFabricWidth.Name = "gcFabricWidth"
        Me.gcFabricWidth.OptionsColumn.AllowEdit = False
        Me.gcFabricWidth.Visible = True
        Me.gcFabricWidth.VisibleIndex = 4
        '
        'gcVendor
        '
        Me.gcVendor.Caption = "gcVendor"
        Me.gcVendor.FieldName = "gcVendor"
        Me.gcVendor.Name = "gcVendor"
        Me.gcVendor.OptionsColumn.AllowEdit = False
        Me.gcVendor.Visible = True
        Me.gcVendor.VisibleIndex = 5
        '
        'gcSeason
        '
        Me.gcSeason.Caption = "gcSeason"
        Me.gcSeason.FieldName = "gcSeason"
        Me.gcSeason.Name = "gcSeason"
        Me.gcSeason.OptionsColumn.AllowEdit = False
        Me.gcSeason.Visible = True
        Me.gcSeason.VisibleIndex = 10
        '
        'wCompareBOM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1189, 579)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.gcCompare)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wCompareBOM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Show Compare BOM"
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.gcCompare, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gcCompare.ResumeLayout(False)
        CType(Me.ogcCompare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvlisting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepCheckEdit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gcCompare As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcCompare As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvlisting As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepCheckEdit As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents gcNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcFabricWidth As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcVendor As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcColorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcUnit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcStyle As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcSeason As DevExpress.XtraGrid.Columns.GridColumn
End Class
