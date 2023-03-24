<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wBOMListingListOrderNo
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
        Me.ogblist = New DevExpress.XtraEditors.GroupControl()
        Me.ogdorder = New DevExpress.XtraGrid.GridControl()
        Me.ogvorder = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CFTOrder = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTBuyCode = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogblist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogblist.SuspendLayout()
        CType(Me.ogdorder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvorder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogblist
        '
        Me.ogblist.Controls.Add(Me.ogdorder)
        Me.ogblist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogblist.Location = New System.Drawing.Point(0, 0)
        Me.ogblist.Name = "ogblist"
        Me.ogblist.Size = New System.Drawing.Size(568, 571)
        Me.ogblist.TabIndex = 303
        Me.ogblist.Text = "List Order No (F1 = Select All ,F2=Cancel Sselect All)"
        '
        'ogdorder
        '
        Me.ogdorder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdorder.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.ogdorder.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.ogdorder.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.ogdorder.EmbeddedNavigator.Buttons.First.Visible = False
        Me.ogdorder.EmbeddedNavigator.Buttons.Last.Visible = False
        Me.ogdorder.EmbeddedNavigator.Buttons.Next.Visible = False
        Me.ogdorder.EmbeddedNavigator.Buttons.NextPage.Visible = False
        Me.ogdorder.EmbeddedNavigator.Buttons.Prev.Visible = False
        Me.ogdorder.EmbeddedNavigator.Buttons.PrevPage.Visible = False
        Me.ogdorder.EmbeddedNavigator.Buttons.Remove.Visible = False
        Me.ogdorder.EmbeddedNavigator.TextStringFormat = ""
        Me.ogdorder.Location = New System.Drawing.Point(2, 23)
        Me.ogdorder.MainView = Me.ogvorder
        Me.ogdorder.Name = "ogdorder"
        Me.ogdorder.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit4})
        Me.ogdorder.Size = New System.Drawing.Size(564, 546)
        Me.ogdorder.TabIndex = 553
        Me.ogdorder.TabStop = False
        Me.ogdorder.Tag = "3|"
        Me.ogdorder.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvorder})
        '
        'ogvorder
        '
        Me.ogvorder.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xFTSelect, Me.CFTOrder, Me.FTCmpCode, Me.xFTBuyCode})
        Me.ogvorder.GridControl = Me.ogdorder
        Me.ogvorder.Name = "ogvorder"
        Me.ogvorder.OptionsCustomization.AllowGroup = False
        Me.ogvorder.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvorder.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        Me.ogvorder.OptionsView.ColumnAutoWidth = False
        Me.ogvorder.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvorder.OptionsView.ShowGroupPanel = False
        Me.ogvorder.Tag = "2|"
        '
        'xFTSelect
        '
        Me.xFTSelect.Caption = "Select"
        Me.xFTSelect.ColumnEdit = Me.RepositoryItemCheckEdit4
        Me.xFTSelect.FieldName = "FTSelect"
        Me.xFTSelect.Name = "xFTSelect"
        Me.xFTSelect.Visible = True
        Me.xFTSelect.VisibleIndex = 0
        Me.xFTSelect.Width = 55
        '
        'RepositoryItemCheckEdit4
        '
        Me.RepositoryItemCheckEdit4.AutoHeight = False
        Me.RepositoryItemCheckEdit4.Caption = "Check"
        Me.RepositoryItemCheckEdit4.Name = "RepositoryItemCheckEdit4"
        Me.RepositoryItemCheckEdit4.ValueChecked = "1"
        Me.RepositoryItemCheckEdit4.ValueUnchecked = "0"
        '
        'CFTOrder
        '
        Me.CFTOrder.AppearanceHeader.Options.UseTextOptions = True
        Me.CFTOrder.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CFTOrder.Caption = "Order No"
        Me.CFTOrder.FieldName = "FTOrderNo"
        Me.CFTOrder.Name = "CFTOrder"
        Me.CFTOrder.OptionsColumn.AllowEdit = False
        Me.CFTOrder.OptionsColumn.ReadOnly = True
        Me.CFTOrder.Visible = True
        Me.CFTOrder.VisibleIndex = 1
        Me.CFTOrder.Width = 120
        '
        'FTCmpCode
        '
        Me.FTCmpCode.Caption = "Factory"
        Me.FTCmpCode.FieldName = "FTCmpCode"
        Me.FTCmpCode.Name = "FTCmpCode"
        Me.FTCmpCode.OptionsColumn.AllowEdit = False
        Me.FTCmpCode.OptionsColumn.ReadOnly = True
        Me.FTCmpCode.Visible = True
        Me.FTCmpCode.VisibleIndex = 2
        Me.FTCmpCode.Width = 122
        '
        'xFTBuyCode
        '
        Me.xFTBuyCode.Caption = "Buy"
        Me.xFTBuyCode.FieldName = "FTBuyCode"
        Me.xFTBuyCode.Name = "xFTBuyCode"
        Me.xFTBuyCode.OptionsColumn.AllowEdit = False
        Me.xFTBuyCode.OptionsColumn.ReadOnly = True
        Me.xFTBuyCode.Visible = True
        Me.xFTBuyCode.VisibleIndex = 3
        Me.xFTBuyCode.Width = 190
        '
        'wBOMListingListOrderNo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(568, 571)
        Me.Controls.Add(Me.ogblist)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wBOMListingListOrderNo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List Order No"
        CType(Me.ogblist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogblist.ResumeLayout(False)
        CType(Me.ogdorder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvorder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogblist As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogdorder As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvorder As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CFTOrder As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTBuyCode As DevExpress.XtraGrid.Columns.GridColumn
End Class
