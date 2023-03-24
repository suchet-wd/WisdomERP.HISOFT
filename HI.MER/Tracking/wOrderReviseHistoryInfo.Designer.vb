<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wOrderReviseHistoryInfo
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
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmclearclsr = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmrefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.FuncExcel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ogorder = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTUpdUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDUpdDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUpdTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFormName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRefDocKey = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTChangeObject = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTChangeFrom = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTChangeTo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTGUID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTTableName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTRefGUID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemNumN0 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.RepositoryItemCheck = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogorder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemNumN0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheck, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclearclsr)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmrefresh)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdelete)
        Me.ogbmainprocbutton.Controls.Add(Me.FuncExcel)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(40, 400)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1147, 57)
        Me.ogbmainprocbutton.TabIndex = 141
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmclearclsr
        '
        Me.ocmclearclsr.Location = New System.Drawing.Point(248, 14)
        Me.ocmclearclsr.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclearclsr.Name = "ocmclearclsr"
        Me.ocmclearclsr.Size = New System.Drawing.Size(111, 31)
        Me.ocmclearclsr.TabIndex = 109
        Me.ocmclearclsr.TabStop = False
        Me.ocmclearclsr.Tag = "2|"
        Me.ocmclearclsr.Text = "CLEAR"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(366, 14)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(111, 31)
        Me.ocmpreview.TabIndex = 108
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "PREVIEW"
        Me.ocmpreview.Visible = False
        '
        'ocmrefresh
        '
        Me.ocmrefresh.Location = New System.Drawing.Point(484, 14)
        Me.ocmrefresh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmrefresh.Name = "ocmrefresh"
        Me.ocmrefresh.Size = New System.Drawing.Size(111, 31)
        Me.ocmrefresh.TabIndex = 107
        Me.ocmrefresh.TabStop = False
        Me.ocmrefresh.Tag = "2|"
        Me.ocmrefresh.Text = "REFRESH"
        '
        'ocmdelete
        '
        Me.ocmdelete.Enabled = False
        Me.ocmdelete.Location = New System.Drawing.Point(129, 14)
        Me.ocmdelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(111, 31)
        Me.ocmdelete.TabIndex = 106
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "DELETE"
        Me.ocmdelete.Visible = False
        '
        'FuncExcel
        '
        Me.FuncExcel.Location = New System.Drawing.Point(12, 14)
        Me.FuncExcel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FuncExcel.Name = "FuncExcel"
        Me.FuncExcel.Size = New System.Drawing.Size(111, 31)
        Me.FuncExcel.TabIndex = 105
        Me.FuncExcel.TabStop = False
        Me.FuncExcel.Tag = "2|"
        Me.FuncExcel.Text = "Export to Excel"
        Me.FuncExcel.Visible = False
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(1059, 14)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(82, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ogorder
        '
        Me.ogorder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogorder.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogorder.Location = New System.Drawing.Point(0, 0)
        Me.ogorder.MainView = Me.GridView1
        Me.ogorder.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogorder.Name = "ogorder"
        Me.ogorder.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemNumN0, Me.RepositoryItemCheck})
        Me.ogorder.Size = New System.Drawing.Size(1259, 705)
        Me.ogorder.TabIndex = 140
        Me.ogorder.TabStop = False
        Me.ogorder.Tag = ""
        Me.ogorder.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTUpdUser, Me.FDUpdDate, Me.FTUpdTime, Me.FTFormName, Me.FTOrderNo, Me.FTRefDocKey, Me.FTChangeObject, Me.FTChangeFrom, Me.FTChangeTo, Me.oColFTGUID, Me.oColFTTableName, Me.oColFTRefGUID})
        Me.GridView1.CustomizationFormBounds = New System.Drawing.Rectangle(758, 512, 216, 178)
        Me.GridView1.GridControl = Me.ogorder
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsBehavior.Editable = False
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GridView1.OptionsView.ShowAutoFilterRow = True
        Me.GridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.GridView1.OptionsView.ShowFooter = True
        Me.GridView1.OptionsView.ShowGroupedColumns = True
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.Tag = "2|"
        '
        'FTUpdUser
        '
        Me.FTUpdUser.Caption = "FTUpdUser"
        Me.FTUpdUser.FieldName = "FTUpdUser"
        Me.FTUpdUser.Name = "FTUpdUser"
        Me.FTUpdUser.OptionsColumn.AllowEdit = False
        Me.FTUpdUser.OptionsColumn.ReadOnly = True
        Me.FTUpdUser.Visible = True
        Me.FTUpdUser.VisibleIndex = 0
        Me.FTUpdUser.Width = 99
        '
        'FDUpdDate
        '
        Me.FDUpdDate.Caption = "FDUpdDate"
        Me.FDUpdDate.DisplayFormat.FormatString = "d"
        Me.FDUpdDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDUpdDate.FieldName = "FDUpdDate"
        Me.FDUpdDate.Name = "FDUpdDate"
        Me.FDUpdDate.OptionsColumn.AllowEdit = False
        Me.FDUpdDate.OptionsColumn.ReadOnly = True
        Me.FDUpdDate.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value
        Me.FDUpdDate.Visible = True
        Me.FDUpdDate.VisibleIndex = 1
        '
        'FTUpdTime
        '
        Me.FTUpdTime.Caption = "FTUpdTime"
        Me.FTUpdTime.FieldName = "FTUpdTime"
        Me.FTUpdTime.Name = "FTUpdTime"
        Me.FTUpdTime.OptionsColumn.AllowEdit = False
        Me.FTUpdTime.OptionsColumn.ReadOnly = True
        Me.FTUpdTime.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value
        Me.FTUpdTime.Visible = True
        Me.FTUpdTime.VisibleIndex = 2
        '
        'FTFormName
        '
        Me.FTFormName.Caption = "FTFormName"
        Me.FTFormName.FieldName = "FTFormName"
        Me.FTFormName.Name = "FTFormName"
        Me.FTFormName.OptionsColumn.AllowEdit = False
        Me.FTFormName.OptionsColumn.ReadOnly = True
        Me.FTFormName.Visible = True
        Me.FTFormName.VisibleIndex = 3
        '
        'FTOrderNo
        '
        Me.FTOrderNo.Caption = "FTOrderNo"
        Me.FTOrderNo.FieldName = "FTOrderNo"
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.OptionsColumn.AllowEdit = False
        Me.FTOrderNo.OptionsColumn.ReadOnly = True
        Me.FTOrderNo.Visible = True
        Me.FTOrderNo.VisibleIndex = 4
        Me.FTOrderNo.Width = 113
        '
        'FTRefDocKey
        '
        Me.FTRefDocKey.Caption = "FTRefDocKey"
        Me.FTRefDocKey.FieldName = "FTRefDocKey"
        Me.FTRefDocKey.Name = "FTRefDocKey"
        Me.FTRefDocKey.OptionsColumn.AllowEdit = False
        Me.FTRefDocKey.OptionsColumn.ReadOnly = True
        Me.FTRefDocKey.Visible = True
        Me.FTRefDocKey.VisibleIndex = 5
        Me.FTRefDocKey.Width = 105
        '
        'FTChangeObject
        '
        Me.FTChangeObject.Caption = "FTChangeObject"
        Me.FTChangeObject.FieldName = "FTChangeObject"
        Me.FTChangeObject.Name = "FTChangeObject"
        Me.FTChangeObject.OptionsColumn.AllowEdit = False
        Me.FTChangeObject.OptionsColumn.ReadOnly = True
        Me.FTChangeObject.Visible = True
        Me.FTChangeObject.VisibleIndex = 6
        Me.FTChangeObject.Width = 180
        '
        'FTChangeFrom
        '
        Me.FTChangeFrom.Caption = "FTChangeFrom"
        Me.FTChangeFrom.FieldName = "FTChangeFrom"
        Me.FTChangeFrom.Name = "FTChangeFrom"
        Me.FTChangeFrom.OptionsColumn.AllowEdit = False
        Me.FTChangeFrom.OptionsColumn.ReadOnly = True
        Me.FTChangeFrom.Visible = True
        Me.FTChangeFrom.VisibleIndex = 7
        Me.FTChangeFrom.Width = 121
        '
        'FTChangeTo
        '
        Me.FTChangeTo.Caption = "FTChangeTo"
        Me.FTChangeTo.FieldName = "FTChangeTo"
        Me.FTChangeTo.Name = "FTChangeTo"
        Me.FTChangeTo.OptionsColumn.AllowEdit = False
        Me.FTChangeTo.OptionsColumn.ReadOnly = True
        Me.FTChangeTo.Visible = True
        Me.FTChangeTo.VisibleIndex = 8
        Me.FTChangeTo.Width = 93
        '
        'oColFTGUID
        '
        Me.oColFTGUID.Caption = "FTGUID"
        Me.oColFTGUID.FieldName = "GUID"
        Me.oColFTGUID.Name = "oColFTGUID"
        '
        'oColFTTableName
        '
        Me.oColFTTableName.Caption = "FTTableName"
        Me.oColFTTableName.FieldName = "FTTableName"
        Me.oColFTTableName.Name = "oColFTTableName"
        '
        'oColFTRefGUID
        '
        Me.oColFTRefGUID.Caption = "FTRefGUID"
        Me.oColFTRefGUID.FieldName = "FTRefGUID"
        Me.oColFTRefGUID.Name = "oColFTRefGUID"
        '
        'RepositoryItemNumN0
        '
        Me.RepositoryItemNumN0.AutoHeight = False
        Me.RepositoryItemNumN0.DisplayFormat.FormatString = "N0"
        Me.RepositoryItemNumN0.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemNumN0.EditFormat.FormatString = "N0"
        Me.RepositoryItemNumN0.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemNumN0.Name = "RepositoryItemNumN0"
        '
        'RepositoryItemCheck
        '
        Me.RepositoryItemCheck.AutoHeight = False
        Me.RepositoryItemCheck.Caption = "Check"
        Me.RepositoryItemCheck.Name = "RepositoryItemCheck"
        Me.RepositoryItemCheck.ValueChecked = "1"
        Me.RepositoryItemCheck.ValueUnchecked = "0"
        '
        'wOrderReviseHistoryInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1259, 705)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogorder)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wOrderReviseHistoryInfo"
        Me.Text = "wOrderReviseHistoryInfo"
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogorder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemNumN0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheck, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmclearclsr As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmrefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FuncExcel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogorder As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemNumN0 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents RepositoryItemCheck As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTRefDocKey As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTChangeObject As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTChangeFrom As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTChangeTo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUpdUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDUpdDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUpdTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFormName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTGUID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTTableName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTRefGUID As DevExpress.XtraGrid.Columns.GridColumn
End Class
