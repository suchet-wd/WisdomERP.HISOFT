<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wListPackOrderNo
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
        Me.ogclist = New DevExpress.XtraGrid.GridControl()
        Me.ogvlist = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTPackNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogblist = New DevExpress.XtraEditors.GroupControl()
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogblist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogblist.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogclist
        '
        Me.ogclist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogclist.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogclist.Location = New System.Drawing.Point(2, 24)
        Me.ogclist.MainView = Me.ogvlist
        Me.ogclist.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogclist.Name = "ogclist"
        Me.ogclist.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect})
        Me.ogclist.Size = New System.Drawing.Size(485, 378)
        Me.ogclist.TabIndex = 302
        Me.ogclist.TabStop = False
        Me.ogclist.Tag = "2|"
        Me.ogclist.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvlist})
        '
        'ogvlist
        '
        Me.ogvlist.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTPackNo, Me.FTOrderNo})
        Me.ogvlist.GridControl = Me.ogclist
        Me.ogvlist.Name = "ogvlist"
        Me.ogvlist.OptionsCustomization.AllowGroup = False
        Me.ogvlist.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvlist.OptionsView.ColumnAutoWidth = False
        Me.ogvlist.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvlist.OptionsView.ShowGroupPanel = False
        Me.ogvlist.Tag = "2|"
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Caption = "Check"
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'FTPackNo
        '
        Me.FTPackNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTPackNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPackNo.Caption = "FTPackNo"
        Me.FTPackNo.FieldName = "FTPackNo"
        Me.FTPackNo.Name = "FTPackNo"
        Me.FTPackNo.OptionsColumn.AllowEdit = False
        Me.FTPackNo.OptionsColumn.AllowMove = False
        Me.FTPackNo.OptionsColumn.ReadOnly = True
        Me.FTPackNo.Visible = True
        Me.FTPackNo.VisibleIndex = 0
        Me.FTPackNo.Width = 180
        '
        'FTOrderNo
        '
        Me.FTOrderNo.Caption = "FTOrderNo"
        Me.FTOrderNo.FieldName = "FTOrderNo"
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.OptionsColumn.AllowEdit = False
        Me.FTOrderNo.OptionsColumn.ReadOnly = True
        Me.FTOrderNo.Visible = True
        Me.FTOrderNo.VisibleIndex = 1
        Me.FTOrderNo.Width = 207
        '
        'ogblist
        '
        Me.ogblist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogblist.Controls.Add(Me.ogclist)
        Me.ogblist.Location = New System.Drawing.Point(0, 5)
        Me.ogblist.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogblist.Name = "ogblist"
        Me.ogblist.Size = New System.Drawing.Size(489, 404)
        Me.ogblist.TabIndex = 303
        Me.ogblist.Text = "List PackOrderNo"
        '
        'wListPackOrderNo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(489, 411)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogblist)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wListPackOrderNo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List PackOrderNo"
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogblist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogblist.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogclist As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvlist As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTPackNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogblist As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
End Class
