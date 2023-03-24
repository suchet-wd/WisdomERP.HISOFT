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
        Me.cFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTDocumentNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.obtSelect = New DevExpress.XtraEditors.SimpleButton()
        Me.otbClose = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogcbrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvbrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogcbrowse.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect, Me.RepositoryItemFTSelect})
        Me.ogcbrowse.Size = New System.Drawing.Size(304, 367)
        Me.ogcbrowse.TabIndex = 2
        Me.ogcbrowse.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvbrowse})
        '
        'ogvbrowse
        '
        Me.ogvbrowse.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTSelect, Me.cFTDocumentNo})
        Me.ogvbrowse.GridControl = Me.ogcbrowse
        Me.ogvbrowse.Name = "ogvbrowse"
        Me.ogvbrowse.OptionsCustomization.AllowGroup = False
        Me.ogvbrowse.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvbrowse.OptionsView.ColumnAutoWidth = False
        Me.ogvbrowse.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvbrowse.OptionsView.ShowGroupPanel = False
        '
        'cFTSelect
        '
        Me.cFTSelect.Caption = "FTSelect"
        Me.cFTSelect.ColumnEdit = Me.RepositoryItemFTSelect
        Me.cFTSelect.FieldName = "FTSelect"
        Me.cFTSelect.Name = "cFTSelect"
        Me.cFTSelect.Visible = True
        Me.cFTSelect.VisibleIndex = 0
        Me.cFTSelect.Width = 41
        '
        'RepositoryItemFTSelect
        '
        Me.RepositoryItemFTSelect.AutoHeight = False
        Me.RepositoryItemFTSelect.Caption = "Check"
        Me.RepositoryItemFTSelect.Name = "RepositoryItemFTSelect"
        Me.RepositoryItemFTSelect.ValueChecked = "1"
        Me.RepositoryItemFTSelect.ValueUnchecked = "0"
        '
        'cFTDocumentNo
        '
        Me.cFTDocumentNo.Caption = "FTDocumentNo"
        Me.cFTDocumentNo.FieldName = "FTDocumentNo"
        Me.cFTDocumentNo.Name = "cFTDocumentNo"
        Me.cFTDocumentNo.OptionsColumn.AllowEdit = False
        Me.cFTDocumentNo.Visible = True
        Me.cFTDocumentNo.VisibleIndex = 1
        Me.cFTDocumentNo.Width = 232
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
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcbrowse As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvbrowse As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents obtSelect As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otbClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTDocumentNo As DevExpress.XtraGrid.Columns.GridColumn
End Class
