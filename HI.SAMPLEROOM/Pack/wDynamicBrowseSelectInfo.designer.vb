<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wDynamicBrowseSelectInfo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wDynamicBrowseSelectInfo))
        Me.ogcbrowse = New DevExpress.XtraGrid.GridControl()
        Me.ogvbrowse = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.obtSelect = New DevExpress.XtraEditors.SimpleButton()
        Me.otbClose = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogcbrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvbrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcbrowse
        '
        Me.ogcbrowse.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcbrowse.Location = New System.Drawing.Point(0, 29)
        Me.ogcbrowse.MainView = Me.ogvbrowse
        Me.ogcbrowse.Name = "ogcbrowse"
        Me.ogcbrowse.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect})
        Me.ogcbrowse.Size = New System.Drawing.Size(304, 367)
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
        'RepositoryFTSelect
        '
        Me.RepositoryFTSelect.AutoHeight = False
        Me.RepositoryFTSelect.Caption = "Check"
        Me.RepositoryFTSelect.Name = "RepositoryFTSelect"
        Me.RepositoryFTSelect.ValueChecked = "1"
        Me.RepositoryFTSelect.ValueUnchecked = "0"
        '
        'obtSelect
        '
        Me.obtSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.obtSelect.Location = New System.Drawing.Point(144, 1)
        Me.obtSelect.Name = "obtSelect"
        Me.obtSelect.Size = New System.Drawing.Size(75, 25)
        Me.obtSelect.TabIndex = 3
        Me.obtSelect.Text = "Select"
        '
        'otbClose
        '
        Me.otbClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.otbClose.Location = New System.Drawing.Point(225, 1)
        Me.otbClose.Name = "otbClose"
        Me.otbClose.Size = New System.Drawing.Size(75, 25)
        Me.otbClose.TabIndex = 3
        Me.otbClose.Text = "Close"
        '
        'wDynamicBrowseSelectInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(304, 396)
        Me.ControlBox = False
        Me.Controls.Add(Me.otbClose)
        Me.Controls.Add(Me.obtSelect)
        Me.Controls.Add(Me.ogcbrowse)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wDynamicBrowseSelectInfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wBrowseItemInfo"
        CType(Me.ogcbrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvbrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcbrowse As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvbrowse As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents obtSelect As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otbClose As DevExpress.XtraEditors.SimpleButton
End Class
