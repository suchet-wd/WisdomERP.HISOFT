<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wListAutoPurchaseOrderNo
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
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTSupplier = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSupplierName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTItemRef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTDeliveryCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTDeliveryDescTH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogblist = New DevExpress.XtraEditors.GroupControl()
        Me.olbinfo = New DevExpress.XtraEditors.LabelControl()
        Me.ogbcmd = New DevExpress.XtraEditors.GroupControl()
        Me.ocmsendapprove = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
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
        Me.ogclist.Location = New System.Drawing.Point(2, 25)
        Me.ogclist.MainView = Me.ogvlist
        Me.ogclist.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogclist.Name = "ogclist"
        Me.ogclist.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect})
        Me.ogclist.Size = New System.Drawing.Size(1030, 496)
        Me.ogclist.TabIndex = 302
        Me.ogclist.TabStop = False
        Me.ogclist.Tag = "2|"
        Me.ogclist.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvlist})
        '
        'ogvlist
        '
        Me.ogvlist.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.FTSupplier, Me.FTSupplierName, Me.FTPurchaseNo, Me.FTItemRef, Me.CFTDeliveryCode, Me.CFTDeliveryDescTH})
        Me.ogvlist.GridControl = Me.ogclist
        Me.ogvlist.Name = "ogvlist"
        Me.ogvlist.OptionsCustomization.AllowGroup = False
        Me.ogvlist.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvlist.OptionsView.ColumnAutoWidth = False
        Me.ogvlist.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvlist.OptionsView.ShowGroupPanel = False
        Me.ogvlist.Tag = "2|"
        '
        'FTSelect
        '
        Me.FTSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSelect.Caption = "FTSelect"
        Me.FTSelect.ColumnEdit = Me.RepFTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowMove = False
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 43
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
        Me.FTSupplier.FieldName = "FTSupplier"
        Me.FTSupplier.Name = "FTSupplier"
        Me.FTSupplier.OptionsColumn.AllowEdit = False
        Me.FTSupplier.OptionsColumn.AllowMove = False
        Me.FTSupplier.OptionsColumn.ReadOnly = True
        Me.FTSupplier.Visible = True
        Me.FTSupplier.VisibleIndex = 1
        Me.FTSupplier.Width = 108
        '
        'FTSupplierName
        '
        Me.FTSupplierName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSupplierName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSupplierName.Caption = "FTSupplierName"
        Me.FTSupplierName.FieldName = "FTSupplierName"
        Me.FTSupplierName.Name = "FTSupplierName"
        Me.FTSupplierName.OptionsColumn.AllowEdit = False
        Me.FTSupplierName.OptionsColumn.AllowMove = False
        Me.FTSupplierName.OptionsColumn.ReadOnly = True
        Me.FTSupplierName.Visible = True
        Me.FTSupplierName.VisibleIndex = 2
        Me.FTSupplierName.Width = 213
        '
        'FTPurchaseNo
        '
        Me.FTPurchaseNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTPurchaseNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPurchaseNo.Caption = "FTPurchaseNo"
        Me.FTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.FTPurchaseNo.Name = "FTPurchaseNo"
        Me.FTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.FTPurchaseNo.OptionsColumn.AllowMove = False
        Me.FTPurchaseNo.OptionsColumn.ReadOnly = True
        Me.FTPurchaseNo.Visible = True
        Me.FTPurchaseNo.VisibleIndex = 3
        Me.FTPurchaseNo.Width = 135
        '
        'FTItemRef
        '
        Me.FTItemRef.Caption = "Item Ref"
        Me.FTItemRef.FieldName = "FTItemRef"
        Me.FTItemRef.Name = "FTItemRef"
        Me.FTItemRef.OptionsColumn.AllowEdit = False
        Me.FTItemRef.OptionsColumn.ReadOnly = True
        Me.FTItemRef.Visible = True
        Me.FTItemRef.VisibleIndex = 4
        Me.FTItemRef.Width = 250
        '
        'CFTDeliveryCode
        '
        Me.CFTDeliveryCode.Caption = "Delivery Code"
        Me.CFTDeliveryCode.FieldName = "FTDeliveryCode"
        Me.CFTDeliveryCode.Name = "CFTDeliveryCode"
        Me.CFTDeliveryCode.OptionsColumn.AllowEdit = False
        Me.CFTDeliveryCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTDeliveryCode.OptionsColumn.ReadOnly = True
        Me.CFTDeliveryCode.Visible = True
        Me.CFTDeliveryCode.VisibleIndex = 5
        Me.CFTDeliveryCode.Width = 102
        '
        'CFTDeliveryDescTH
        '
        Me.CFTDeliveryDescTH.Caption = "Delivery Name"
        Me.CFTDeliveryDescTH.FieldName = "FTDeliveryName"
        Me.CFTDeliveryDescTH.Name = "CFTDeliveryDescTH"
        Me.CFTDeliveryDescTH.OptionsColumn.AllowEdit = False
        Me.CFTDeliveryDescTH.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTDeliveryDescTH.OptionsColumn.ReadOnly = True
        Me.CFTDeliveryDescTH.Visible = True
        Me.CFTDeliveryDescTH.VisibleIndex = 6
        Me.CFTDeliveryDescTH.Width = 140
        '
        'ogblist
        '
        Me.ogblist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogblist.Controls.Add(Me.olbinfo)
        Me.ogblist.Controls.Add(Me.ogclist)
        Me.ogblist.Location = New System.Drawing.Point(0, 5)
        Me.ogblist.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogblist.Name = "ogblist"
        Me.ogblist.Size = New System.Drawing.Size(1034, 523)
        Me.ogblist.TabIndex = 303
        Me.ogblist.Text = "List Purchase Order No"
        '
        'olbinfo
        '
        Me.olbinfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.olbinfo.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.olbinfo.Appearance.Options.UseForeColor = True
        Me.olbinfo.Appearance.Options.UseTextOptions = True
        Me.olbinfo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.olbinfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbinfo.Location = New System.Drawing.Point(591, 0)
        Me.olbinfo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.olbinfo.Name = "olbinfo"
        Me.olbinfo.Size = New System.Drawing.Size(426, 23)
        Me.olbinfo.TabIndex = 305
        Me.olbinfo.Tag = "2|"
        Me.olbinfo.Text = "Double Click On Po No for link To Purchase Order Page   "
        '
        'ogbcmd
        '
        Me.ogbcmd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbcmd.Controls.Add(Me.ocmsendapprove)
        Me.ogbcmd.Controls.Add(Me.ocmexit)
        Me.ogbcmd.Location = New System.Drawing.Point(2, 533)
        Me.ogbcmd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbcmd.Name = "ogbcmd"
        Me.ogbcmd.ShowCaption = False
        Me.ogbcmd.Size = New System.Drawing.Size(1029, 53)
        Me.ogbcmd.TabIndex = 304
        Me.ogbcmd.Text = "GroupControl1"
        '
        'ocmsendapprove
        '
        Me.ocmsendapprove.Location = New System.Drawing.Point(163, 11)
        Me.ocmsendapprove.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsendapprove.Name = "ocmsendapprove"
        Me.ocmsendapprove.Size = New System.Drawing.Size(194, 31)
        Me.ocmsendapprove.TabIndex = 96
        Me.ocmsendapprove.TabStop = False
        Me.ocmsendapprove.Tag = "2|"
        Me.ocmsendapprove.Text = "SEND APPROVE"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(648, 11)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(194, 31)
        Me.ocmexit.TabIndex = 95
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'wListAutoPurchaseOrderNo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1034, 590)
        Me.Controls.Add(Me.ogbcmd)
        Me.Controls.Add(Me.ogblist)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wListAutoPurchaseOrderNo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List Auto Purchase Order No"
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogblist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogblist.ResumeLayout(False)
        CType(Me.ogbcmd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbcmd.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTSupplier As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSupplierName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogblist As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbcmd As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTItemRef As DevExpress.XtraGrid.Columns.GridColumn
    Public WithEvents ocmsendapprove As DevExpress.XtraEditors.SimpleButton
    Public WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Public WithEvents ogclist As DevExpress.XtraGrid.GridControl
    Public WithEvents ogvlist As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents olbinfo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CFTDeliveryCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTDeliveryDescTH As DevExpress.XtraGrid.Columns.GridColumn
End Class
