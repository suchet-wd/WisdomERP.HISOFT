<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wCopySizeSpec
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
        Me.obtcopy = New DevExpress.XtraEditors.SimpleButton()
        Me.obtcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.FTExpCode = New DevExpress.XtraEditors.TextEdit()
        Me.FTSeasonCode = New DevExpress.XtraEditors.TextEdit()
        Me.FTDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTExpCode_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTSeasonCode_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleSSPId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleSSPId = New DevExpress.XtraEditors.ButtonEdit()
        CType(Me.FTExpCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSeasonCode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleSSPId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'obtcopy
        '
        Me.obtcopy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.obtcopy.Location = New System.Drawing.Point(146, 137)
        Me.obtcopy.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.obtcopy.Name = "obtcopy"
        Me.obtcopy.Size = New System.Drawing.Size(87, 28)
        Me.obtcopy.TabIndex = 0
        Me.obtcopy.Text = "Copy"
        '
        'obtcancel
        '
        Me.obtcancel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.obtcancel.Location = New System.Drawing.Point(253, 137)
        Me.obtcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.obtcancel.Name = "obtcancel"
        Me.obtcancel.Size = New System.Drawing.Size(87, 28)
        Me.obtcancel.TabIndex = 0
        Me.obtcancel.Text = "Cancel"
        '
        'FTExpCode
        '
        Me.FTExpCode.Location = New System.Drawing.Point(159, 94)
        Me.FTExpCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTExpCode.Name = "FTExpCode"
        Me.FTExpCode.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTExpCode.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTExpCode.Size = New System.Drawing.Size(184, 23)
        Me.FTExpCode.TabIndex = 495
        '
        'FTSeasonCode
        '
        Me.FTSeasonCode.Location = New System.Drawing.Point(159, 37)
        Me.FTSeasonCode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSeasonCode.Name = "FTSeasonCode"
        Me.FTSeasonCode.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTSeasonCode.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTSeasonCode.Size = New System.Drawing.Size(184, 23)
        Me.FTSeasonCode.TabIndex = 496
        Me.FTSeasonCode.Visible = False
        '
        'FTDate
        '
        Me.FTDate.EditValue = Nothing
        Me.FTDate.Location = New System.Drawing.Point(159, 65)
        Me.FTDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTDate.Name = "FTDate"
        Me.FTDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTDate.Size = New System.Drawing.Size(184, 23)
        Me.FTDate.TabIndex = 494
        '
        'FTExpCode_lbl
        '
        Me.FTExpCode_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTExpCode_lbl.Appearance.Options.UseForeColor = True
        Me.FTExpCode_lbl.Appearance.Options.UseTextOptions = True
        Me.FTExpCode_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTExpCode_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTExpCode_lbl.Location = New System.Drawing.Point(33, 90)
        Me.FTExpCode_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTExpCode_lbl.Name = "FTExpCode_lbl"
        Me.FTExpCode_lbl.Size = New System.Drawing.Size(125, 23)
        Me.FTExpCode_lbl.TabIndex = 489
        Me.FTExpCode_lbl.Tag = "2|"
        Me.FTExpCode_lbl.Text = "FTExpCode  :"
        '
        'FTDate_lbl
        '
        Me.FTDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTDate_lbl.Location = New System.Drawing.Point(10, 66)
        Me.FTDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTDate_lbl.Name = "FTDate_lbl"
        Me.FTDate_lbl.Size = New System.Drawing.Size(147, 23)
        Me.FTDate_lbl.TabIndex = 490
        Me.FTDate_lbl.Tag = "2|"
        Me.FTDate_lbl.Text = "FTDate  :"
        '
        'FTSeasonCode_lbl
        '
        Me.FTSeasonCode_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTSeasonCode_lbl.Appearance.Options.UseForeColor = True
        Me.FTSeasonCode_lbl.Appearance.Options.UseTextOptions = True
        Me.FTSeasonCode_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSeasonCode_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSeasonCode_lbl.Location = New System.Drawing.Point(10, 37)
        Me.FTSeasonCode_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSeasonCode_lbl.Name = "FTSeasonCode_lbl"
        Me.FTSeasonCode_lbl.Size = New System.Drawing.Size(147, 23)
        Me.FTSeasonCode_lbl.TabIndex = 491
        Me.FTSeasonCode_lbl.Tag = "2|"
        Me.FTSeasonCode_lbl.Text = "FTSeasonCode  :"
        Me.FTSeasonCode_lbl.Visible = False
        '
        'FNHSysStyleSSPId_lbl
        '
        Me.FNHSysStyleSSPId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleSSPId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysStyleSSPId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysStyleSSPId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleSSPId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleSSPId_lbl.Location = New System.Drawing.Point(33, 9)
        Me.FNHSysStyleSSPId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleSSPId_lbl.Name = "FNHSysStyleSSPId_lbl"
        Me.FNHSysStyleSSPId_lbl.Size = New System.Drawing.Size(125, 23)
        Me.FNHSysStyleSSPId_lbl.TabIndex = 492
        Me.FNHSysStyleSSPId_lbl.Tag = "2|"
        Me.FNHSysStyleSSPId_lbl.Text = "FNHSysStyleSSPId  :"
        '
        'FNHSysStyleSSPId
        '
        Me.FNHSysStyleSSPId.Location = New System.Drawing.Point(159, 9)
        Me.FNHSysStyleSSPId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleSSPId.Name = "FNHSysStyleSSPId"
        Me.FNHSysStyleSSPId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "450", Nothing, True)})
        Me.FNHSysStyleSSPId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysStyleSSPId.Properties.Tag = ""
        Me.FNHSysStyleSSPId.Size = New System.Drawing.Size(184, 23)
        Me.FNHSysStyleSSPId.TabIndex = 493
        Me.FNHSysStyleSSPId.Tag = "2|"
        '
        'wCopySizeSpec
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(357, 171)
        Me.ControlBox = False
        Me.Controls.Add(Me.FTExpCode)
        Me.Controls.Add(Me.FTSeasonCode)
        Me.Controls.Add(Me.FTDate)
        Me.Controls.Add(Me.FTExpCode_lbl)
        Me.Controls.Add(Me.FTDate_lbl)
        Me.Controls.Add(Me.FTSeasonCode_lbl)
        Me.Controls.Add(Me.FNHSysStyleSSPId_lbl)
        Me.Controls.Add(Me.FNHSysStyleSSPId)
        Me.Controls.Add(Me.obtcancel)
        Me.Controls.Add(Me.obtcopy)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wCopySizeSpec"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wCopySizeSpec"
        CType(Me.FTExpCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSeasonCode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleSSPId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents obtcopy As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents obtcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTExpCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTSeasonCode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTExpCode_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSeasonCode_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleSSPId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleSSPId As DevExpress.XtraEditors.ButtonEdit
End Class
