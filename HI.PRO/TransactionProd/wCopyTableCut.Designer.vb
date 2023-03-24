<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wCopyTableCut
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
        Me.FNHSysMarkId = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysMarkId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderProdNo = New DevExpress.XtraEditors.TextEdit()
        Me.FTOrderProdNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNTableNo = New DevExpress.XtraEditors.TextEdit()
        Me.FNTableNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNTotal = New DevExpress.XtraEditors.CalcEdit()
        Me.FNTotal_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcopy = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.FNHSysMarkId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderProdNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNTableNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNTotal.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FNHSysMarkId
        '
        Me.FNHSysMarkId.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysMarkId.Location = New System.Drawing.Point(194, 49)
        Me.FNHSysMarkId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysMarkId.Name = "FNHSysMarkId"
        Me.FNHSysMarkId.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysMarkId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysMarkId.Properties.ReadOnly = True
        Me.FNHSysMarkId.Size = New System.Drawing.Size(259, 22)
        Me.FNHSysMarkId.TabIndex = 304
        Me.FNHSysMarkId.Tag = "2|"
        '
        'FNHSysMarkId_lbl
        '
        Me.FNHSysMarkId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMarkId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysMarkId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysMarkId_lbl.Location = New System.Drawing.Point(10, 49)
        Me.FNHSysMarkId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysMarkId_lbl.Name = "FNHSysMarkId_lbl"
        Me.FNHSysMarkId_lbl.Size = New System.Drawing.Size(177, 25)
        Me.FNHSysMarkId_lbl.TabIndex = 305
        Me.FNHSysMarkId_lbl.Tag = "2|"
        Me.FNHSysMarkId_lbl.Text = "Mark :"
        '
        'FTOrderProdNo
        '
        Me.FTOrderProdNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTOrderProdNo.Location = New System.Drawing.Point(194, 22)
        Me.FTOrderProdNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTOrderProdNo.Name = "FTOrderProdNo"
        Me.FTOrderProdNo.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTOrderProdNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTOrderProdNo.Properties.ReadOnly = True
        Me.FTOrderProdNo.Size = New System.Drawing.Size(259, 22)
        Me.FTOrderProdNo.TabIndex = 302
        Me.FTOrderProdNo.Tag = "2|"
        '
        'FTOrderProdNo_lbl
        '
        Me.FTOrderProdNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderProdNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderProdNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderProdNo_lbl.Location = New System.Drawing.Point(11, 22)
        Me.FTOrderProdNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTOrderProdNo_lbl.Name = "FTOrderProdNo_lbl"
        Me.FTOrderProdNo_lbl.Size = New System.Drawing.Size(177, 25)
        Me.FTOrderProdNo_lbl.TabIndex = 303
        Me.FTOrderProdNo_lbl.Tag = "2|"
        Me.FTOrderProdNo_lbl.Text = "Order Production No :"
        '
        'FNTableNo
        '
        Me.FNTableNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNTableNo.Location = New System.Drawing.Point(194, 76)
        Me.FNTableNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNTableNo.Name = "FNTableNo"
        Me.FNTableNo.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNTableNo.Properties.Appearance.Options.UseBackColor = True
        Me.FNTableNo.Properties.ReadOnly = True
        Me.FNTableNo.Size = New System.Drawing.Size(259, 22)
        Me.FNTableNo.TabIndex = 300
        Me.FNTableNo.Tag = "2|"
        '
        'FNTableNo_lbl
        '
        Me.FNTableNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNTableNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTableNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNTableNo_lbl.Location = New System.Drawing.Point(9, 76)
        Me.FNTableNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNTableNo_lbl.Name = "FNTableNo_lbl"
        Me.FNTableNo_lbl.Size = New System.Drawing.Size(177, 25)
        Me.FNTableNo_lbl.TabIndex = 301
        Me.FNTableNo_lbl.Tag = "2|"
        Me.FNTableNo_lbl.Text = "Table :"
        '
        'FNTotal
        '
        Me.FNTotal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNTotal.EnterMoveNextControl = True
        Me.FNTotal.Location = New System.Drawing.Point(194, 105)
        Me.FNTotal.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNTotal.Name = "FNTotal"
        Me.FNTotal.Properties.Appearance.Options.UseTextOptions = True
        Me.FNTotal.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTotal.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNTotal.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNTotal.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNTotal.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNTotal.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNTotal.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNTotal.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNTotal.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNTotal.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNTotal.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotal.Properties.Precision = 0
        Me.FNTotal.Size = New System.Drawing.Size(258, 22)
        Me.FNTotal.TabIndex = 306
        Me.FNTotal.Tag = "2|"
        '
        'FNTotal_lbl
        '
        Me.FNTotal_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNTotal_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTotal_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNTotal_lbl.Location = New System.Drawing.Point(8, 106)
        Me.FNTotal_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNTotal_lbl.Name = "FNTotal_lbl"
        Me.FNTotal_lbl.Size = New System.Drawing.Size(180, 23)
        Me.FNTotal_lbl.TabIndex = 307
        Me.FNTotal_lbl.Tag = "2|"
        Me.FNTotal_lbl.Text = "จำนวน Copy:"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(347, 154)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(141, 31)
        Me.ocmcancel.TabIndex = 312
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmcopy
        '
        Me.ocmcopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcopy.Location = New System.Drawing.Point(106, 154)
        Me.ocmcopy.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcopy.Name = "ocmcopy"
        Me.ocmcopy.Size = New System.Drawing.Size(138, 31)
        Me.ocmcopy.TabIndex = 311
        Me.ocmcopy.TabStop = False
        Me.ocmcopy.Tag = "2|"
        Me.ocmcopy.Text = "Save"
        '
        'wCopyTableCut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(575, 209)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmcopy)
        Me.Controls.Add(Me.FNTotal)
        Me.Controls.Add(Me.FNTotal_lbl)
        Me.Controls.Add(Me.FNHSysMarkId)
        Me.Controls.Add(Me.FNHSysMarkId_lbl)
        Me.Controls.Add(Me.FTOrderProdNo)
        Me.Controls.Add(Me.FTOrderProdNo_lbl)
        Me.Controls.Add(Me.FNTableNo)
        Me.Controls.Add(Me.FNTableNo_lbl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wCopyTableCut"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Copy Table Cut"
        CType(Me.FNHSysMarkId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderProdNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNTableNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNTotal.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FNHSysMarkId As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysMarkId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOrderProdNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTOrderProdNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNTableNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNTableNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNTotal As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNTotal_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcopy As DevExpress.XtraEditors.SimpleButton
End Class
