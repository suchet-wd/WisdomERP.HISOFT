<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wExchangeRateMI
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
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmedit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmrefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmaddnew = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFNHSysCurId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCurCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNBuyingRate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSellingRate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.ogcdetail)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.Size = New System.Drawing.Size(1065, 703)
        Me.ogbdetail.TabIndex = 13
        Me.ogbdetail.Tag = "2|"
        Me.ogbdetail.Text = "Daily Exchange Rate"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmedit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmrefresh)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdelete)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmaddnew)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(154, 188)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(878, 58)
        Me.ogbmainprocbutton.TabIndex = 136
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmedit
        '
        Me.ocmedit.Location = New System.Drawing.Point(483, 14)
        Me.ocmedit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmedit.Name = "ocmedit"
        Me.ocmedit.Size = New System.Drawing.Size(111, 31)
        Me.ocmedit.TabIndex = 98
        Me.ocmedit.TabStop = False
        Me.ocmedit.Tag = "2|"
        Me.ocmedit.Text = "EDIT"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(365, 14)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(111, 31)
        Me.ocmpreview.TabIndex = 97
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "PREVIEW"
        Me.ocmpreview.Visible = False
        '
        'ocmexit
        '
        Me.ocmexit.Location = New System.Drawing.Point(723, 14)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmrefresh
        '
        Me.ocmrefresh.Location = New System.Drawing.Point(246, 14)
        Me.ocmrefresh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmrefresh.Name = "ocmrefresh"
        Me.ocmrefresh.Size = New System.Drawing.Size(111, 31)
        Me.ocmrefresh.TabIndex = 95
        Me.ocmrefresh.TabStop = False
        Me.ocmrefresh.Tag = "2|"
        Me.ocmrefresh.Text = "CLEAR"
        '
        'ocmdelete
        '
        Me.ocmdelete.Location = New System.Drawing.Point(128, 14)
        Me.ocmdelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(111, 31)
        Me.ocmdelete.TabIndex = 94
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "DELETE"
        '
        'ocmaddnew
        '
        Me.ocmaddnew.Location = New System.Drawing.Point(10, 14)
        Me.ocmaddnew.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmaddnew.Name = "ocmaddnew"
        Me.ocmaddnew.Size = New System.Drawing.Size(111, 31)
        Me.ocmaddnew.TabIndex = 93
        Me.ocmaddnew.TabStop = False
        Me.ocmaddnew.Tag = "2|"
        Me.ocmaddnew.Text = "NEW"
        '
        'ogcdetail
        '
        Me.ogcdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Location = New System.Drawing.Point(3, 29)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.Size = New System.Drawing.Size(1057, 669)
        Me.ogcdetail.TabIndex = 1
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFNHSysCurId, Me.FDDate, Me.FTCurCode, Me.FNBuyingRate, Me.FNSellingRate, Me.FTRemark})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowGroup = False
        Me.ogvdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        Me.ogvdetail.Tag = "2|"
        '
        'CFNHSysCurId
        '
        Me.CFNHSysCurId.Caption = "FNHSysCurId"
        Me.CFNHSysCurId.FieldName = "FNHSysCurId"
        Me.CFNHSysCurId.Name = "CFNHSysCurId"
        Me.CFNHSysCurId.OptionsColumn.AllowEdit = False
        Me.CFNHSysCurId.OptionsColumn.ReadOnly = True
        '
        'FDDate
        '
        Me.FDDate.Caption = "Date"
        Me.FDDate.FieldName = "FDDate"
        Me.FDDate.Name = "FDDate"
        Me.FDDate.OptionsColumn.AllowEdit = False
        Me.FDDate.OptionsColumn.ReadOnly = True
        Me.FDDate.Visible = True
        Me.FDDate.VisibleIndex = 0
        Me.FDDate.Width = 100
        '
        'FTCurCode
        '
        Me.FTCurCode.Caption = "FTCurCode"
        Me.FTCurCode.FieldName = "FTCurCode"
        Me.FTCurCode.Name = "FTCurCode"
        Me.FTCurCode.OptionsColumn.AllowEdit = False
        Me.FTCurCode.OptionsColumn.ReadOnly = True
        Me.FTCurCode.Visible = True
        Me.FTCurCode.VisibleIndex = 1
        Me.FTCurCode.Width = 104
        '
        'FNBuyingRate
        '
        Me.FNBuyingRate.AppearanceCell.Options.UseTextOptions = True
        Me.FNBuyingRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNBuyingRate.Caption = "FNBuyingRate"
        Me.FNBuyingRate.DisplayFormat.FormatString = "{0:n4}"
        Me.FNBuyingRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNBuyingRate.FieldName = "FNBuyingRate"
        Me.FNBuyingRate.Name = "FNBuyingRate"
        Me.FNBuyingRate.OptionsColumn.AllowEdit = False
        Me.FNBuyingRate.OptionsColumn.ReadOnly = True
        Me.FNBuyingRate.Visible = True
        Me.FNBuyingRate.VisibleIndex = 2
        Me.FNBuyingRate.Width = 151
        '
        'FNSellingRate
        '
        Me.FNSellingRate.AppearanceCell.Options.UseTextOptions = True
        Me.FNSellingRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSellingRate.Caption = "FNSellingRate"
        Me.FNSellingRate.DisplayFormat.FormatString = "{0:n4}"
        Me.FNSellingRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSellingRate.FieldName = "FNSellingRate"
        Me.FNSellingRate.Name = "FNSellingRate"
        Me.FNSellingRate.OptionsColumn.AllowEdit = False
        Me.FNSellingRate.OptionsColumn.ReadOnly = True
        Me.FNSellingRate.Visible = True
        Me.FNSellingRate.VisibleIndex = 3
        Me.FNSellingRate.Width = 179
        '
        'FTRemark
        '
        Me.FTRemark.Caption = "FTRemark"
        Me.FTRemark.FieldName = "FTRemark"
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.OptionsColumn.AllowEdit = False
        Me.FTRemark.OptionsColumn.ReadOnly = True
        Me.FTRemark.Visible = True
        Me.FTRemark.VisibleIndex = 4
        Me.FTRemark.Width = 250
        '
        'wExchangeRate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1065, 703)
        Me.Controls.Add(Me.ogbdetail)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wExchangeRateMI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Daily Exchange Rate (MI)"
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmedit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmrefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmaddnew As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CFNHSysCurId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCurCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNBuyingRate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSellingRate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRemark As DevExpress.XtraGrid.Columns.GridColumn
End Class
