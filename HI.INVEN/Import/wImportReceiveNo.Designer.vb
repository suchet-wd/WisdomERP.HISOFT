<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wImportReceiveNo
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
        Me.ogbcmd = New DevExpress.XtraEditors.GroupControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ogblist = New DevExpress.XtraEditors.GroupControl()
        Me.ogclist = New DevExpress.XtraGrid.GridControl()
        Me.ogvlist = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTReceiveNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTInvoiceNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.ogbcmd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbcmd.SuspendLayout()
        CType(Me.ogblist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogblist.SuspendLayout()
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbcmd
        '
        Me.ogbcmd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbcmd.Controls.Add(Me.ocmexit)
        Me.ogbcmd.Location = New System.Drawing.Point(1, 487)
        Me.ogbcmd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbcmd.Name = "ogbcmd"
        Me.ogbcmd.ShowCaption = False
        Me.ogbcmd.Size = New System.Drawing.Size(598, 53)
        Me.ogbcmd.TabIndex = 306
        Me.ogbcmd.Text = "GroupControl1"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(6, 11)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(583, 31)
        Me.ocmexit.TabIndex = 95
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ogblist
        '
        Me.ogblist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogblist.Controls.Add(Me.ogclist)
        Me.ogblist.Location = New System.Drawing.Point(-1, 2)
        Me.ogblist.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogblist.Name = "ogblist"
        Me.ogblist.Size = New System.Drawing.Size(603, 478)
        Me.ogblist.TabIndex = 305
        Me.ogblist.Text = "List receive No"
        '
        'ogclist
        '
        Me.ogclist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogclist.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogclist.Location = New System.Drawing.Point(2, 25)
        Me.ogclist.MainView = Me.ogvlist
        Me.ogclist.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogclist.Name = "ogclist"
        Me.ogclist.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect})
        Me.ogclist.Size = New System.Drawing.Size(599, 451)
        Me.ogclist.TabIndex = 302
        Me.ogclist.TabStop = False
        Me.ogclist.Tag = "2|"
        Me.ogclist.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvlist})
        '
        'ogvlist
        '
        Me.ogvlist.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTReceiveNo, Me.FTPurchaseNo, Me.FTInvoiceNo})
        Me.ogvlist.GridControl = Me.ogclist
        Me.ogvlist.Name = "ogvlist"
        Me.ogvlist.OptionsCustomization.AllowGroup = False
        Me.ogvlist.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvlist.OptionsView.ColumnAutoWidth = False
        Me.ogvlist.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvlist.OptionsView.ShowGroupPanel = False
        Me.ogvlist.Tag = "2|"
        '
        'FTReceiveNo
        '
        Me.FTReceiveNo.Caption = "FTReceiveNo"
        Me.FTReceiveNo.FieldName = "FTReceiveNo"
        Me.FTReceiveNo.Name = "FTReceiveNo"
        Me.FTReceiveNo.OptionsColumn.AllowEdit = False
        Me.FTReceiveNo.OptionsColumn.ReadOnly = True
        Me.FTReceiveNo.Visible = True
        Me.FTReceiveNo.VisibleIndex = 0
        Me.FTReceiveNo.Width = 151
        '
        'FTPurchaseNo
        '
        Me.FTPurchaseNo.Caption = "FTPurchaseNo"
        Me.FTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.FTPurchaseNo.Name = "FTPurchaseNo"
        Me.FTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.FTPurchaseNo.OptionsColumn.ReadOnly = True
        Me.FTPurchaseNo.Visible = True
        Me.FTPurchaseNo.VisibleIndex = 1
        Me.FTPurchaseNo.Width = 167
        '
        'FTInvoiceNo
        '
        Me.FTInvoiceNo.Caption = "FTInvoiceNo"
        Me.FTInvoiceNo.FieldName = "FTInvoiceNo"
        Me.FTInvoiceNo.Name = "FTInvoiceNo"
        Me.FTInvoiceNo.OptionsColumn.AllowEdit = False
        Me.FTInvoiceNo.OptionsColumn.ReadOnly = True
        Me.FTInvoiceNo.Visible = True
        Me.FTInvoiceNo.VisibleIndex = 2
        Me.FTInvoiceNo.Width = 151
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Caption = "Check"
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'wImportReceiveNo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(607, 544)
        Me.Controls.Add(Me.ogbcmd)
        Me.Controls.Add(Me.ogblist)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wImportReceiveNo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wImportReceiveNo"
        CType(Me.ogbcmd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbcmd.ResumeLayout(False)
        CType(Me.ogblist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogblist.ResumeLayout(False)
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbcmd As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogblist As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogclist As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvlist As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTReceiveNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTInvoiceNo As DevExpress.XtraGrid.Columns.GridColumn
End Class
