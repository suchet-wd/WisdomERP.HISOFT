<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wListInvoiceChargeAlert
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
        Me.FTSupplier = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSupplierName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTotalDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogblist = New DevExpress.XtraEditors.GroupControl()
        Me.ogbcmd = New DevExpress.XtraEditors.GroupControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.FTInvoiceNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDInvoiceDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysSuplId = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogblist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogblist.SuspendLayout()
        CType(Me.ogbcmd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbcmd.SuspendLayout()
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
        Me.ogclist.Size = New System.Drawing.Size(766, 552)
        Me.ogclist.TabIndex = 302
        Me.ogclist.TabStop = False
        Me.ogclist.Tag = "2|"
        Me.ogclist.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvlist})
        '
        'ogvlist
        '
        Me.ogvlist.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTInvoiceNo, Me.FDInvoiceDate, Me.FTSupplier, Me.FTSupplierName, Me.FNTotalDay, Me.FNHSysSuplId})
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
        'FTSupplier
        '
        Me.FTSupplier.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSupplier.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSupplier.Caption = "FTSupplier"
        Me.FTSupplier.FieldName = "FTSuplCode"
        Me.FTSupplier.Name = "FTSupplier"
        Me.FTSupplier.OptionsColumn.AllowEdit = False
        Me.FTSupplier.OptionsColumn.AllowMove = False
        Me.FTSupplier.OptionsColumn.ReadOnly = True
        Me.FTSupplier.Visible = True
        Me.FTSupplier.VisibleIndex = 2
        Me.FTSupplier.Width = 110
        '
        'FTSupplierName
        '
        Me.FTSupplierName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSupplierName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSupplierName.Caption = "FTSupplierName"
        Me.FTSupplierName.FieldName = "FTSuplName"
        Me.FTSupplierName.Name = "FTSupplierName"
        Me.FTSupplierName.OptionsColumn.AllowEdit = False
        Me.FTSupplierName.OptionsColumn.AllowMove = False
        Me.FTSupplierName.OptionsColumn.ReadOnly = True
        Me.FTSupplierName.Visible = True
        Me.FTSupplierName.VisibleIndex = 3
        Me.FTSupplierName.Width = 213
        '
        'FNTotalDay
        '
        Me.FNTotalDay.AppearanceHeader.Options.UseTextOptions = True
        Me.FNTotalDay.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNTotalDay.Caption = "จำนวนวันล่าช้า"
        Me.FNTotalDay.DisplayFormat.FormatString = "{0:n0}"
        Me.FNTotalDay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotalDay.FieldName = "FNTotalDay"
        Me.FNTotalDay.Name = "FNTotalDay"
        Me.FNTotalDay.OptionsColumn.AllowEdit = False
        Me.FNTotalDay.OptionsColumn.AllowMove = False
        Me.FNTotalDay.OptionsColumn.ReadOnly = True
        Me.FNTotalDay.Visible = True
        Me.FNTotalDay.VisibleIndex = 4
        Me.FNTotalDay.Width = 101
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
        Me.ogblist.Size = New System.Drawing.Size(770, 578)
        Me.ogblist.TabIndex = 303
        Me.ogblist.Text = "List Invoice Charge Alert"
        '
        'ogbcmd
        '
        Me.ogbcmd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbcmd.Controls.Add(Me.ocmexit)
        Me.ogbcmd.Location = New System.Drawing.Point(2, 588)
        Me.ogbcmd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbcmd.Name = "ogbcmd"
        Me.ogbcmd.ShowCaption = False
        Me.ogbcmd.Size = New System.Drawing.Size(765, 53)
        Me.ogbcmd.TabIndex = 304
        Me.ogbcmd.Text = "GroupControl1"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(22, 11)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(721, 31)
        Me.ocmexit.TabIndex = 95
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'FTInvoiceNo
        '
        Me.FTInvoiceNo.Caption = "Invoice No"
        Me.FTInvoiceNo.FieldName = "FTInvoiceNo"
        Me.FTInvoiceNo.Name = "FTInvoiceNo"
        Me.FTInvoiceNo.OptionsColumn.AllowEdit = False
        Me.FTInvoiceNo.OptionsColumn.AllowMove = False
        Me.FTInvoiceNo.OptionsColumn.ReadOnly = True
        Me.FTInvoiceNo.Visible = True
        Me.FTInvoiceNo.VisibleIndex = 0
        Me.FTInvoiceNo.Width = 113
        '
        'FDInvoiceDate
        '
        Me.FDInvoiceDate.Caption = "Invoice Date"
        Me.FDInvoiceDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDInvoiceDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDInvoiceDate.FieldName = "FDInvoiceDate"
        Me.FDInvoiceDate.Name = "FDInvoiceDate"
        Me.FDInvoiceDate.OptionsColumn.AllowEdit = False
        Me.FDInvoiceDate.OptionsColumn.AllowMove = False
        Me.FDInvoiceDate.OptionsColumn.ReadOnly = True
        Me.FDInvoiceDate.Visible = True
        Me.FDInvoiceDate.VisibleIndex = 1
        Me.FDInvoiceDate.Width = 115
        '
        'FNHSysSuplId
        '
        Me.FNHSysSuplId.Caption = "FNHSysSuplId"
        Me.FNHSysSuplId.FieldName = "FNHSysSuplId"
        Me.FNHSysSuplId.Name = "FNHSysSuplId"
        '
        'wListInvoiceChargeAlert
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(770, 645)
        Me.Controls.Add(Me.ogbcmd)
        Me.Controls.Add(Me.ogblist)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wListInvoiceChargeAlert"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List Invoice Charge Alert"
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogblist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogblist.ResumeLayout(False)
        CType(Me.ogbcmd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbcmd.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTSupplier As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSupplierName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTotalDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogblist As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbcmd As DevExpress.XtraEditors.GroupControl
    Public WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Public WithEvents ogclist As DevExpress.XtraGrid.GridControl
    Public WithEvents ogvlist As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTInvoiceNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDInvoiceDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysSuplId As DevExpress.XtraGrid.Columns.GridColumn
End Class
