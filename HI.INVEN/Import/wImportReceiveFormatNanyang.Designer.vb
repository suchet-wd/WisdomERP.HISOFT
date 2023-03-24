<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wImportReceiveFormatNanyang
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbselectfile = New DevExpress.XtraEditors.GroupControl()
        Me.FTFilePath = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTInvoiceNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTInvoiceDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMaterialCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMaterialColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateComplete = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmimportexcel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbreceivedetail = New DevExpress.XtraEditors.GroupControl()
        Me.FTStateSelectAll = New DevExpress.XtraEditors.CheckEdit()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.TextEdit()
        Me.FTRemark_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRemark = New DevExpress.XtraEditors.MemoEdit()
        Me.FNHSysWHId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysWHId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysWHId_None = New DevExpress.XtraEditors.TextEdit()
        CType(Me.ogbselectfile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbselectfile.SuspendLayout()
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogbreceivedetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbreceivedetail.SuspendLayout()
        CType(Me.FTStateSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbselectfile
        '
        Me.ogbselectfile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbselectfile.Controls.Add(Me.FTFilePath)
        Me.ogbselectfile.Location = New System.Drawing.Point(0, 1)
        Me.ogbselectfile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbselectfile.Name = "ogbselectfile"
        Me.ogbselectfile.Size = New System.Drawing.Size(1142, 69)
        Me.ogbselectfile.TabIndex = 1
        Me.ogbselectfile.Text = "Select File"
        '
        'FTFilePath
        '
        Me.FTFilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTFilePath.Location = New System.Drawing.Point(43, 32)
        Me.FTFilePath.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTFilePath.Name = "FTFilePath"
        Me.FTFilePath.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTFilePath.Properties.Appearance.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTFilePath.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTFilePath.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTFilePath.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTFilePath.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTFilePath.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTFilePath.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTFilePath.Properties.ReadOnly = True
        Me.FTFilePath.Size = New System.Drawing.Size(1084, 22)
        Me.FTFilePath.TabIndex = 1
        Me.FTFilePath.Tag = "2|"
        '
        'ogcdetail
        '
        Me.ogcdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Location = New System.Drawing.Point(3, 193)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFTSelect})
        Me.ogcdetail.Size = New System.Drawing.Size(1139, 508)
        Me.ogcdetail.TabIndex = 2
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.FTPurchaseNo, Me.FTInvoiceNo, Me.FTInvoiceDate, Me.FTMaterialCode, Me.FTMaterialColorCode, Me.FTUnitCode, Me.FQuantity, Me.FTStateComplete})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowGroup = False
        Me.ogvdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        Me.ogvdetail.Tag = "2|"
        '
        'FTSelect
        '
        Me.FTSelect.Caption = "FTSelect"
        Me.FTSelect.ColumnEdit = Me.ReposFTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 50
        '
        'ReposFTSelect
        '
        Me.ReposFTSelect.AutoHeight = False
        Me.ReposFTSelect.Caption = "Check"
        Me.ReposFTSelect.Name = "ReposFTSelect"
        Me.ReposFTSelect.ValueChecked = "1"
        Me.ReposFTSelect.ValueUnchecked = "0"
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
        Me.FTPurchaseNo.Width = 149
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
        Me.FTInvoiceNo.Width = 143
        '
        'FTInvoiceDate
        '
        Me.FTInvoiceDate.AppearanceCell.Options.UseTextOptions = True
        Me.FTInvoiceDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTInvoiceDate.Caption = "FTInvoiceDate"
        Me.FTInvoiceDate.FieldName = "FTInvoiceDate"
        Me.FTInvoiceDate.Name = "FTInvoiceDate"
        Me.FTInvoiceDate.OptionsColumn.AllowEdit = False
        Me.FTInvoiceDate.OptionsColumn.ReadOnly = True
        Me.FTInvoiceDate.Visible = True
        Me.FTInvoiceDate.VisibleIndex = 3
        Me.FTInvoiceDate.Width = 110
        '
        'FTMaterialCode
        '
        Me.FTMaterialCode.Caption = "FTMaterialCode"
        Me.FTMaterialCode.FieldName = "FTMaterialCode"
        Me.FTMaterialCode.Name = "FTMaterialCode"
        Me.FTMaterialCode.OptionsColumn.AllowEdit = False
        Me.FTMaterialCode.OptionsColumn.ReadOnly = True
        Me.FTMaterialCode.Visible = True
        Me.FTMaterialCode.VisibleIndex = 4
        Me.FTMaterialCode.Width = 161
        '
        'FTMaterialColorCode
        '
        Me.FTMaterialColorCode.Caption = "FTMaterialColorCode"
        Me.FTMaterialColorCode.FieldName = "FTMaterialColorCode"
        Me.FTMaterialColorCode.Name = "FTMaterialColorCode"
        Me.FTMaterialColorCode.OptionsColumn.AllowEdit = False
        Me.FTMaterialColorCode.OptionsColumn.ReadOnly = True
        Me.FTMaterialColorCode.Visible = True
        Me.FTMaterialColorCode.VisibleIndex = 5
        Me.FTMaterialColorCode.Width = 101
        '
        'FTUnitCode
        '
        Me.FTUnitCode.Caption = "FTUnitCode"
        Me.FTUnitCode.FieldName = "FTUnitCode"
        Me.FTUnitCode.Name = "FTUnitCode"
        Me.FTUnitCode.OptionsColumn.AllowEdit = False
        Me.FTUnitCode.OptionsColumn.ReadOnly = True
        Me.FTUnitCode.Visible = True
        Me.FTUnitCode.VisibleIndex = 6
        Me.FTUnitCode.Width = 84
        '
        'FQuantity
        '
        Me.FQuantity.AppearanceCell.Options.UseTextOptions = True
        Me.FQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FQuantity.Caption = "FQuantity"
        Me.FQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.FQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FQuantity.FieldName = "FQuantity"
        Me.FQuantity.Name = "FQuantity"
        Me.FQuantity.OptionsColumn.AllowEdit = False
        Me.FQuantity.OptionsColumn.ReadOnly = True
        Me.FQuantity.Visible = True
        Me.FQuantity.VisibleIndex = 7
        Me.FQuantity.Width = 129
        '
        'FTStateComplete
        '
        Me.FTStateComplete.Caption = "FTStateComplete"
        Me.FTStateComplete.FieldName = "FTStateComplete"
        Me.FTStateComplete.Name = "FTStateComplete"
        Me.FTStateComplete.OptionsColumn.AllowEdit = False
        Me.FTStateComplete.OptionsColumn.ReadOnly = True
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmimportexcel)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(86, 304)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(870, 137)
        Me.ogbmainprocbutton.TabIndex = 392
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(738, 14)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmimportexcel
        '
        Me.ocmimportexcel.Location = New System.Drawing.Point(6, 14)
        Me.ocmimportexcel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmimportexcel.Name = "ocmimportexcel"
        Me.ocmimportexcel.Size = New System.Drawing.Size(111, 31)
        Me.ocmimportexcel.TabIndex = 93
        Me.ocmimportexcel.TabStop = False
        Me.ocmimportexcel.Tag = "2|"
        Me.ocmimportexcel.Text = "Importdata"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(124, 14)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(111, 31)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ogbreceivedetail
        '
        Me.ogbreceivedetail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbreceivedetail.Controls.Add(Me.FTStateSelectAll)
        Me.ogbreceivedetail.Controls.Add(Me.FNHSysCmpId)
        Me.ogbreceivedetail.Controls.Add(Me.FTRemark_lbl)
        Me.ogbreceivedetail.Controls.Add(Me.FTRemark)
        Me.ogbreceivedetail.Controls.Add(Me.FNHSysWHId)
        Me.ogbreceivedetail.Controls.Add(Me.FNHSysWHId_lbl)
        Me.ogbreceivedetail.Controls.Add(Me.FNHSysWHId_None)
        Me.ogbreceivedetail.Location = New System.Drawing.Point(0, 73)
        Me.ogbreceivedetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbreceivedetail.Name = "ogbreceivedetail"
        Me.ogbreceivedetail.Size = New System.Drawing.Size(1142, 112)
        Me.ogbreceivedetail.TabIndex = 393
        Me.ogbreceivedetail.Text = "Receive Detail"
        '
        'FTStateSelectAll
        '
        Me.FTStateSelectAll.Location = New System.Drawing.Point(41, 84)
        Me.FTStateSelectAll.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStateSelectAll.Name = "FTStateSelectAll"
        Me.FTStateSelectAll.Properties.Appearance.Options.UseTextOptions = True
        Me.FTStateSelectAll.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTStateSelectAll.Properties.Caption = "Select All"
        Me.FTStateSelectAll.Properties.ValueChecked = "1"
        Me.FTStateSelectAll.Properties.ValueUnchecked = "0"
        Me.FTStateSelectAll.Size = New System.Drawing.Size(157, 20)
        Me.FTStateSelectAll.TabIndex = 287
        Me.FTStateSelectAll.Tag = "2|"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(14, 57)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysCmpId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysCmpId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysCmpId.Properties.MaxLength = 30
        Me.FNHSysCmpId.Size = New System.Drawing.Size(44, 22)
        Me.FNHSysCmpId.TabIndex = 286
        Me.FNHSysCmpId.Tag = "|"
        Me.FNHSysCmpId.Visible = False
        '
        'FTRemark_lbl
        '
        Me.FTRemark_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTRemark_lbl.Appearance.Options.UseForeColor = True
        Me.FTRemark_lbl.Appearance.Options.UseTextOptions = True
        Me.FTRemark_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRemark_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRemark_lbl.Location = New System.Drawing.Point(14, 58)
        Me.FTRemark_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark_lbl.Name = "FTRemark_lbl"
        Me.FTRemark_lbl.Size = New System.Drawing.Size(188, 22)
        Me.FTRemark_lbl.TabIndex = 281
        Me.FTRemark_lbl.Tag = "2|"
        Me.FTRemark_lbl.Text = "Note :"
        '
        'FTRemark
        '
        Me.FTRemark.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTRemark.EditValue = ""
        Me.FTRemark.Location = New System.Drawing.Point(205, 57)
        Me.FTRemark.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Properties.MaxLength = 500
        Me.FTRemark.Size = New System.Drawing.Size(922, 49)
        Me.FTRemark.TabIndex = 280
        Me.FTRemark.Tag = "2|"
        '
        'FNHSysWHId
        '
        Me.FNHSysWHId.EnterMoveNextControl = True
        Me.FNHSysWHId.Location = New System.Drawing.Point(205, 28)
        Me.FNHSysWHId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHId.Name = "FNHSysWHId"
        Me.FNHSysWHId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysWHId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysWHId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysWHId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysWHId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysWHId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysWHId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysWHId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysWHId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysWHId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "110", Nothing, True)})
        Me.FNHSysWHId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysWHId.Properties.MaxLength = 30
        Me.FNHSysWHId.Size = New System.Drawing.Size(148, 22)
        Me.FNHSysWHId.TabIndex = 277
        Me.FNHSysWHId.Tag = "2|"
        '
        'FNHSysWHId_lbl
        '
        Me.FNHSysWHId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysWHId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysWHId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysWHId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysWHId_lbl.Location = New System.Drawing.Point(68, 28)
        Me.FNHSysWHId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHId_lbl.Name = "FNHSysWHId_lbl"
        Me.FNHSysWHId_lbl.Size = New System.Drawing.Size(134, 23)
        Me.FNHSysWHId_lbl.TabIndex = 278
        Me.FNHSysWHId_lbl.Tag = "2|"
        Me.FNHSysWHId_lbl.Text = "Warehouse No :"
        '
        'FNHSysWHId_None
        '
        Me.FNHSysWHId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysWHId_None.EnterMoveNextControl = True
        Me.FNHSysWHId_None.Location = New System.Drawing.Point(357, 28)
        Me.FNHSysWHId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHId_None.Name = "FNHSysWHId_None"
        Me.FNHSysWHId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysWHId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysWHId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysWHId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysWHId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysWHId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysWHId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysWHId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysWHId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysWHId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysWHId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysWHId_None.Properties.ReadOnly = True
        Me.FNHSysWHId_None.Size = New System.Drawing.Size(770, 22)
        Me.FNHSysWHId_None.TabIndex = 279
        Me.FNHSysWHId_None.TabStop = False
        Me.FNHSysWHId_None.Tag = "2|"
        '
        'wImportReceiveFormatNanyang
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1146, 703)
        Me.Controls.Add(Me.ogbreceivedetail)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogcdetail)
        Me.Controls.Add(Me.ogbselectfile)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wImportReceiveFormatNanyang"
        Me.Text = "Import Receive Format Nanyang"
        CType(Me.ogbselectfile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbselectfile.ResumeLayout(False)
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogbreceivedetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbreceivedetail.ResumeLayout(False)
        CType(Me.FTStateSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbselectfile As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTFilePath As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTInvoiceNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTInvoiceDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMaterialCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMaterialColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateComplete As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmimportexcel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogbreceivedetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNHSysWHId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysWHId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysWHId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTRemark As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FTRemark_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTStateSelectAll As DevExpress.XtraEditors.CheckEdit
End Class
