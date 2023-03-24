<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wDynamicBrowseInfo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wDynamicBrowseInfo))
        Me.ogcbrowse = New DevExpress.XtraGrid.GridControl()
        Me.ogvbrowse = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepCheckEdit = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.ogcbrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvbrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepCheckEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcbrowse
        '
        Me.ogcbrowse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcbrowse.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcbrowse.Location = New System.Drawing.Point(0, 0)
        Me.ogcbrowse.MainView = Me.ogvbrowse
        Me.ogcbrowse.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcbrowse.Name = "ogcbrowse"
        Me.ogcbrowse.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepCheckEdit})
        Me.ogcbrowse.Size = New System.Drawing.Size(355, 487)
        Me.ogcbrowse.TabIndex = 2
        Me.ogcbrowse.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvbrowse})
        '
        'ogvbrowse
        '
        Me.ogvbrowse.GridControl = Me.ogcbrowse
        Me.ogvbrowse.Name = "ogvbrowse"
        Me.ogvbrowse.OptionsCustomization.AllowGroup = False
        Me.ogvbrowse.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvbrowse.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvbrowse.OptionsView.ShowGroupPanel = False
        '
        'RepCheckEdit
        '
        Me.RepCheckEdit.AutoHeight = False
        Me.RepCheckEdit.Caption = "Check"
        Me.RepCheckEdit.Name = "RepCheckEdit"
        Me.RepCheckEdit.ValueChecked = "1"
        Me.RepCheckEdit.ValueUnchecked = "0"
        '
        'wDynamicBrowseInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(355, 487)
        Me.Controls.Add(Me.ogcbrowse)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wDynamicBrowseInfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "wBrowseItemInfo"
        CType(Me.ogcbrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvbrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepCheckEdit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcbrowse As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvbrowse As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepCheckEdit As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class
