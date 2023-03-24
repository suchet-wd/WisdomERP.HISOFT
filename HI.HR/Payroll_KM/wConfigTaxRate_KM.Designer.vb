<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wConfigTaxRate_KM
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbheader = New DevExpress.XtraEditors.GroupControl()
        Me.FNAllowance_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNAllowance = New DevExpress.XtraEditors.CalcEdit()
        Me.FTRemark_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.FTRemark = New DevExpress.XtraEditors.MemoEdit()
        Me.FCTaxRate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FCAmtEnd_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FCAmtBegin_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNSeqNo = New DevExpress.XtraEditors.CalcEdit()
        Me.FCTaxRate = New DevExpress.XtraEditors.CalcEdit()
        Me.FCAmtEnd = New DevExpress.XtraEditors.CalcEdit()
        Me.FCAmtBegin = New DevExpress.XtraEditors.CalcEdit()
        Me.ogd = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColFNSeqNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFCAmtBegin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFCAmtEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFCTaxRate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNAllowance = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTStaActive = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTStaActive = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit5 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit6 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit7 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.ogbheader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        CType(Me.FNAllowance.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNSeqNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FCTaxRate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FCAmtEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FCAmtBegin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTStaActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbheader
        '
        Me.ogbheader.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbheader.AppearanceCaption.Options.UseTextOptions = True
        Me.ogbheader.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogbheader.Controls.Add(Me.FNAllowance_lbl)
        Me.ogbheader.Controls.Add(Me.FNAllowance)
        Me.ogbheader.Controls.Add(Me.FTRemark_lbl)
        Me.ogbheader.Controls.Add(Me.ocmexit)
        Me.ogbheader.Controls.Add(Me.ocmclear)
        Me.ogbheader.Controls.Add(Me.ocmdelete)
        Me.ogbheader.Controls.Add(Me.ocmsave)
        Me.ogbheader.Controls.Add(Me.FTRemark)
        Me.ogbheader.Controls.Add(Me.FCTaxRate_lbl)
        Me.ogbheader.Controls.Add(Me.FCAmtEnd_lbl)
        Me.ogbheader.Controls.Add(Me.FCAmtBegin_lbl)
        Me.ogbheader.Controls.Add(Me.FNSeqNo)
        Me.ogbheader.Controls.Add(Me.FCTaxRate)
        Me.ogbheader.Controls.Add(Me.FCAmtEnd)
        Me.ogbheader.Controls.Add(Me.FCAmtBegin)
        Me.ogbheader.Controls.Add(Me.ogd)
        Me.ogbheader.Location = New System.Drawing.Point(2, 1)
        Me.ogbheader.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Size = New System.Drawing.Size(1023, 647)
        Me.ogbheader.TabIndex = 10
        Me.ogbheader.Text = "Tax Rate"
        '
        'FNAllowance_lbl
        '
        Me.FNAllowance_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNAllowance_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNAllowance_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNAllowance_lbl.Location = New System.Drawing.Point(349, 61)
        Me.FNAllowance_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNAllowance_lbl.Name = "FNAllowance_lbl"
        Me.FNAllowance_lbl.Size = New System.Drawing.Size(146, 25)
        Me.FNAllowance_lbl.TabIndex = 335
        Me.FNAllowance_lbl.Tag = "2|"
        Me.FNAllowance_lbl.Text = "Allowance"
        '
        'FNAllowance
        '
        Me.FNAllowance.EnterMoveNextControl = True
        Me.FNAllowance.Location = New System.Drawing.Point(498, 63)
        Me.FNAllowance.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNAllowance.Name = "FNAllowance"
        Me.FNAllowance.Properties.Appearance.Options.UseTextOptions = True
        Me.FNAllowance.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNAllowance.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNAllowance.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNAllowance.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNAllowance.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNAllowance.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNAllowance.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNAllowance.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNAllowance.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNAllowance.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNAllowance.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNAllowance.Properties.Precision = 2
        Me.FNAllowance.Size = New System.Drawing.Size(140, 22)
        Me.FNAllowance.TabIndex = 334
        Me.FNAllowance.Tag = "2|"
        '
        'FTRemark_lbl
        '
        Me.FTRemark_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTRemark_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRemark_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRemark_lbl.Location = New System.Drawing.Point(30, 92)
        Me.FTRemark_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark_lbl.Name = "FTRemark_lbl"
        Me.FTRemark_lbl.Size = New System.Drawing.Size(146, 25)
        Me.FTRemark_lbl.TabIndex = 333
        Me.FTRemark_lbl.Tag = "2|"
        Me.FTRemark_lbl.Text = "Note"
        '
        'ocmexit
        '
        Me.ocmexit.Location = New System.Drawing.Point(873, 128)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 27)
        Me.ocmexit.TabIndex = 332
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(873, 95)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(111, 27)
        Me.ocmclear.TabIndex = 331
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmdelete
        '
        Me.ocmdelete.Location = New System.Drawing.Point(873, 63)
        Me.ocmdelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(111, 27)
        Me.ocmdelete.TabIndex = 330
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "DELETE"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(873, 31)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(111, 27)
        Me.ocmsave.TabIndex = 4
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'FTRemark
        '
        Me.FTRemark.EditValue = ""
        Me.FTRemark.Location = New System.Drawing.Point(180, 91)
        Me.FTRemark.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Properties.MaxLength = 500
        Me.FTRemark.Size = New System.Drawing.Size(656, 65)
        Me.FTRemark.TabIndex = 3
        Me.FTRemark.Tag = "2|"
        Me.FTRemark.UseOptimizedRendering = True
        '
        'FCTaxRate_lbl
        '
        Me.FCTaxRate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FCTaxRate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FCTaxRate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FCTaxRate_lbl.Location = New System.Drawing.Point(31, 60)
        Me.FCTaxRate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FCTaxRate_lbl.Name = "FCTaxRate_lbl"
        Me.FCTaxRate_lbl.Size = New System.Drawing.Size(146, 25)
        Me.FCTaxRate_lbl.TabIndex = 327
        Me.FCTaxRate_lbl.Tag = "2|"
        Me.FCTaxRate_lbl.Text = "อัตรภาษี (%)"
        '
        'FCAmtEnd_lbl
        '
        Me.FCAmtEnd_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FCAmtEnd_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FCAmtEnd_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FCAmtEnd_lbl.Location = New System.Drawing.Point(330, 33)
        Me.FCAmtEnd_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FCAmtEnd_lbl.Name = "FCAmtEnd_lbl"
        Me.FCAmtEnd_lbl.Size = New System.Drawing.Size(161, 25)
        Me.FCAmtEnd_lbl.TabIndex = 326
        Me.FCAmtEnd_lbl.Tag = "2|"
        Me.FCAmtEnd_lbl.Text = "ถึง"
        '
        'FCAmtBegin_lbl
        '
        Me.FCAmtBegin_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FCAmtBegin_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FCAmtBegin_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FCAmtBegin_lbl.Location = New System.Drawing.Point(16, 30)
        Me.FCAmtBegin_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FCAmtBegin_lbl.Name = "FCAmtBegin_lbl"
        Me.FCAmtBegin_lbl.Size = New System.Drawing.Size(148, 25)
        Me.FCAmtBegin_lbl.TabIndex = 325
        Me.FCAmtBegin_lbl.Tag = "2|"
        Me.FCAmtBegin_lbl.Text = "รายได้ขั้นต้น"
        '
        'FNSeqNo
        '
        Me.FNSeqNo.EnterMoveNextControl = True
        Me.FNSeqNo.Location = New System.Drawing.Point(645, 33)
        Me.FNSeqNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNSeqNo.Name = "FNSeqNo"
        Me.FNSeqNo.Properties.Appearance.Options.UseTextOptions = True
        Me.FNSeqNo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSeqNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNSeqNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNSeqNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNSeqNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNSeqNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNSeqNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNSeqNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNSeqNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNSeqNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.FNSeqNo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSeqNo.Properties.Precision = 4
        Me.FNSeqNo.Properties.ReadOnly = True
        Me.FNSeqNo.Size = New System.Drawing.Size(23, 22)
        Me.FNSeqNo.TabIndex = 301
        Me.FNSeqNo.Tag = "2|"
        Me.FNSeqNo.Visible = False
        '
        'FCTaxRate
        '
        Me.FCTaxRate.EnterMoveNextControl = True
        Me.FCTaxRate.Location = New System.Drawing.Point(180, 62)
        Me.FCTaxRate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FCTaxRate.Name = "FCTaxRate"
        Me.FCTaxRate.Properties.Appearance.Options.UseTextOptions = True
        Me.FCTaxRate.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FCTaxRate.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FCTaxRate.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FCTaxRate.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FCTaxRate.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FCTaxRate.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FCTaxRate.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FCTaxRate.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FCTaxRate.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FCTaxRate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", Nothing, Nothing, True)})
        Me.FCTaxRate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FCTaxRate.Properties.Precision = 2
        Me.FCTaxRate.Size = New System.Drawing.Size(140, 22)
        Me.FCTaxRate.TabIndex = 2
        Me.FCTaxRate.Tag = "2|"
        '
        'FCAmtEnd
        '
        Me.FCAmtEnd.EnterMoveNextControl = True
        Me.FCAmtEnd.Location = New System.Drawing.Point(498, 33)
        Me.FCAmtEnd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FCAmtEnd.Name = "FCAmtEnd"
        Me.FCAmtEnd.Properties.Appearance.Options.UseTextOptions = True
        Me.FCAmtEnd.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FCAmtEnd.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FCAmtEnd.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FCAmtEnd.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FCAmtEnd.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FCAmtEnd.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FCAmtEnd.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FCAmtEnd.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FCAmtEnd.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FCAmtEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", Nothing, Nothing, True)})
        Me.FCAmtEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FCAmtEnd.Properties.Precision = 2
        Me.FCAmtEnd.Size = New System.Drawing.Size(140, 22)
        Me.FCAmtEnd.TabIndex = 1
        Me.FCAmtEnd.Tag = "2|"
        '
        'FCAmtBegin
        '
        Me.FCAmtBegin.EnterMoveNextControl = True
        Me.FCAmtBegin.Location = New System.Drawing.Point(180, 33)
        Me.FCAmtBegin.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FCAmtBegin.Name = "FCAmtBegin"
        Me.FCAmtBegin.Properties.Appearance.Options.UseTextOptions = True
        Me.FCAmtBegin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FCAmtBegin.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FCAmtBegin.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FCAmtBegin.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FCAmtBegin.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FCAmtBegin.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FCAmtBegin.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FCAmtBegin.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FCAmtBegin.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FCAmtBegin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, "", Nothing, Nothing, True)})
        Me.FCAmtBegin.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FCAmtBegin.Properties.Precision = 2
        Me.FCAmtBegin.Size = New System.Drawing.Size(140, 22)
        Me.FCAmtBegin.TabIndex = 0
        Me.FCAmtBegin.Tag = "2|"
        '
        'ogd
        '
        Me.ogd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogd.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogd.Location = New System.Drawing.Point(5, 165)
        Me.ogd.MainView = Me.ogv
        Me.ogd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogd.Name = "ogd"
        Me.ogd.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2, Me.RepositoryItemCheckEdit3, Me.RepositoryItemCheckEdit4, Me.RepositoryItemCheckEdit5, Me.RepositoryItemCheckEdit6, Me.RepositoryItemCheckEdit7, Me.RepFTStaActive})
        Me.ogd.Size = New System.Drawing.Size(1016, 476)
        Me.ogd.TabIndex = 3
        Me.ogd.TabStop = False
        Me.ogd.Tag = "2|"
        Me.ogd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColFNSeqNo, Me.ColFCAmtBegin, Me.ColFCAmtEnd, Me.ColFCTaxRate, Me.CFNAllowance, Me.ColFTRemark, Me.ColFTStaActive})
        Me.ogv.GridControl = Me.ogd
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowGroup = False
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'ColFNSeqNo
        '
        Me.ColFNSeqNo.Caption = "FNSeqNo"
        Me.ColFNSeqNo.FieldName = "FNSeqNo"
        Me.ColFNSeqNo.Name = "ColFNSeqNo"
        '
        'ColFCAmtBegin
        '
        Me.ColFCAmtBegin.AppearanceCell.Options.UseTextOptions = True
        Me.ColFCAmtBegin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.ColFCAmtBegin.AppearanceHeader.Options.UseTextOptions = True
        Me.ColFCAmtBegin.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColFCAmtBegin.Caption = "FCAmtBegin"
        Me.ColFCAmtBegin.FieldName = "FCAmtBegin"
        Me.ColFCAmtBegin.Name = "ColFCAmtBegin"
        Me.ColFCAmtBegin.OptionsColumn.AllowEdit = False
        Me.ColFCAmtBegin.OptionsColumn.AllowMove = False
        Me.ColFCAmtBegin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColFCAmtBegin.OptionsColumn.ReadOnly = True
        Me.ColFCAmtBegin.Visible = True
        Me.ColFCAmtBegin.VisibleIndex = 0
        Me.ColFCAmtBegin.Width = 148
        '
        'ColFCAmtEnd
        '
        Me.ColFCAmtEnd.AppearanceCell.Options.UseTextOptions = True
        Me.ColFCAmtEnd.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.ColFCAmtEnd.AppearanceHeader.Options.UseTextOptions = True
        Me.ColFCAmtEnd.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColFCAmtEnd.Caption = "FCAmtEnd"
        Me.ColFCAmtEnd.FieldName = "FCAmtEnd"
        Me.ColFCAmtEnd.Name = "ColFCAmtEnd"
        Me.ColFCAmtEnd.OptionsColumn.AllowEdit = False
        Me.ColFCAmtEnd.OptionsColumn.AllowMove = False
        Me.ColFCAmtEnd.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColFCAmtEnd.OptionsColumn.ReadOnly = True
        Me.ColFCAmtEnd.Visible = True
        Me.ColFCAmtEnd.VisibleIndex = 1
        Me.ColFCAmtEnd.Width = 165
        '
        'ColFCTaxRate
        '
        Me.ColFCTaxRate.AppearanceCell.Options.UseTextOptions = True
        Me.ColFCTaxRate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.ColFCTaxRate.AppearanceHeader.Options.UseTextOptions = True
        Me.ColFCTaxRate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColFCTaxRate.Caption = "FCTaxRate"
        Me.ColFCTaxRate.FieldName = "FCTaxRate"
        Me.ColFCTaxRate.Name = "ColFCTaxRate"
        Me.ColFCTaxRate.OptionsColumn.AllowEdit = False
        Me.ColFCTaxRate.OptionsColumn.AllowMove = False
        Me.ColFCTaxRate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColFCTaxRate.OptionsColumn.ReadOnly = True
        Me.ColFCTaxRate.Visible = True
        Me.ColFCTaxRate.VisibleIndex = 2
        Me.ColFCTaxRate.Width = 109
        '
        'CFNAllowance
        '
        Me.CFNAllowance.Caption = "Allowance"
        Me.CFNAllowance.DisplayFormat.FormatString = "{0:n2}"
        Me.CFNAllowance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNAllowance.FieldName = "FNAllowance"
        Me.CFNAllowance.Name = "CFNAllowance"
        Me.CFNAllowance.OptionsColumn.AllowEdit = False
        Me.CFNAllowance.OptionsColumn.AllowMove = False
        Me.CFNAllowance.OptionsColumn.AllowShowHide = False
        Me.CFNAllowance.OptionsColumn.ReadOnly = True
        Me.CFNAllowance.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNAllowance.Visible = True
        Me.CFNAllowance.VisibleIndex = 4
        '
        'ColFTRemark
        '
        Me.ColFTRemark.AppearanceHeader.Options.UseTextOptions = True
        Me.ColFTRemark.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ColFTRemark.Caption = "FTRemark"
        Me.ColFTRemark.FieldName = "FTRemark"
        Me.ColFTRemark.Name = "ColFTRemark"
        Me.ColFTRemark.OptionsColumn.AllowEdit = False
        Me.ColFTRemark.OptionsColumn.AllowMove = False
        Me.ColFTRemark.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.ColFTRemark.OptionsColumn.ReadOnly = True
        Me.ColFTRemark.Visible = True
        Me.ColFTRemark.VisibleIndex = 3
        Me.ColFTRemark.Width = 280
        '
        'ColFTStaActive
        '
        Me.ColFTStaActive.Caption = "FTStaActive"
        Me.ColFTStaActive.ColumnEdit = Me.RepFTStaActive
        Me.ColFTStaActive.FieldName = "FTStaActive"
        Me.ColFTStaActive.Name = "ColFTStaActive"
        '
        'RepFTStaActive
        '
        Me.RepFTStaActive.AutoHeight = False
        Me.RepFTStaActive.Caption = "Check"
        Me.RepFTStaActive.Name = "RepFTStaActive"
        Me.RepFTStaActive.ValueChecked = "1"
        Me.RepFTStaActive.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Caption = "Check"
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        Me.RepositoryItemCheckEdit2.ValueChecked = "1"
        Me.RepositoryItemCheckEdit2.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit3
        '
        Me.RepositoryItemCheckEdit3.AutoHeight = False
        Me.RepositoryItemCheckEdit3.Caption = "Check"
        Me.RepositoryItemCheckEdit3.Name = "RepositoryItemCheckEdit3"
        Me.RepositoryItemCheckEdit3.ValueChecked = "1"
        Me.RepositoryItemCheckEdit3.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit4
        '
        Me.RepositoryItemCheckEdit4.AutoHeight = False
        Me.RepositoryItemCheckEdit4.Caption = "Check"
        Me.RepositoryItemCheckEdit4.Name = "RepositoryItemCheckEdit4"
        Me.RepositoryItemCheckEdit4.ValueChecked = "1"
        Me.RepositoryItemCheckEdit4.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit5
        '
        Me.RepositoryItemCheckEdit5.AutoHeight = False
        Me.RepositoryItemCheckEdit5.Caption = "Check"
        Me.RepositoryItemCheckEdit5.Name = "RepositoryItemCheckEdit5"
        Me.RepositoryItemCheckEdit5.ValueChecked = "1"
        Me.RepositoryItemCheckEdit5.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit6
        '
        Me.RepositoryItemCheckEdit6.AutoHeight = False
        Me.RepositoryItemCheckEdit6.Caption = "Check"
        Me.RepositoryItemCheckEdit6.Name = "RepositoryItemCheckEdit6"
        Me.RepositoryItemCheckEdit6.ValueChecked = "1"
        Me.RepositoryItemCheckEdit6.ValueUnchecked = "0"
        '
        'RepositoryItemCheckEdit7
        '
        Me.RepositoryItemCheckEdit7.AutoHeight = False
        Me.RepositoryItemCheckEdit7.Caption = "Check"
        Me.RepositoryItemCheckEdit7.Name = "RepositoryItemCheckEdit7"
        Me.RepositoryItemCheckEdit7.ValueChecked = "1"
        Me.RepositoryItemCheckEdit7.ValueUnchecked = "0"
        '
        'wConfigTaxRate_KM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1027, 652)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wConfigTaxRate_KM"
        Me.Text = "Config Tax"
        CType(Me.ogbheader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        CType(Me.FNAllowance.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNSeqNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FCTaxRate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FCAmtEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FCAmtBegin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTStaActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbheader As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FCTaxRate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FCAmtEnd_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FCAmtBegin_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNSeqNo As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FCTaxRate As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FCAmtEnd As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FCAmtBegin As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents ogd As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit5 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit6 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCheckEdit7 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTRemark As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTRemark_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ColFNSeqNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFCAmtBegin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFCAmtEnd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFCTaxRate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTStaActive As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTStaActive As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FNAllowance_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNAllowance As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents CFNAllowance As DevExpress.XtraGrid.Columns.GridColumn
End Class
