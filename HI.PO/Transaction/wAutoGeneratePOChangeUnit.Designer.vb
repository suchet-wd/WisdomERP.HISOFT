<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAutoGeneratePOChangeUnit
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
        Me.FNHSysCurId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysCurId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysUnitId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysUnitIdPO = New DevExpress.XtraEditors.ButtonEdit()
        Me.ocmchange = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.FNHSysCurId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysUnitIdPO.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FNHSysCurId
        '
        Me.FNHSysCurId.EnterMoveNextControl = True
        Me.FNHSysCurId.Location = New System.Drawing.Point(156, 23)
        Me.FNHSysCurId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCurId.Name = "FNHSysCurId"
        Me.FNHSysCurId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysCurId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCurId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCurId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCurId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysCurId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysCurId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysCurId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCurId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysCurId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysCurId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCurId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCurId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCurId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCurId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "16", Nothing, True)})
        Me.FNHSysCurId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysCurId.Properties.MaxLength = 30
        Me.FNHSysCurId.Size = New System.Drawing.Size(167, 22)
        Me.FNHSysCurId.TabIndex = 265
        Me.FNHSysCurId.Tag = "2|"
        '
        'FNHSysCurId_lbl
        '
        Me.FNHSysCurId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCurId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCurId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCurId_lbl.Location = New System.Drawing.Point(43, 22)
        Me.FNHSysCurId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCurId_lbl.Name = "FNHSysCurId_lbl"
        Me.FNHSysCurId_lbl.Size = New System.Drawing.Size(108, 23)
        Me.FNHSysCurId_lbl.TabIndex = 266
        Me.FNHSysCurId_lbl.Tag = "2|"
        Me.FNHSysCurId_lbl.Text = "Currency :"
        '
        'FNHSysUnitId_lbl
        '
        Me.FNHSysUnitId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysUnitId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysUnitId_lbl.Location = New System.Drawing.Point(46, 57)
        Me.FNHSysUnitId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysUnitId_lbl.Name = "FNHSysUnitId_lbl"
        Me.FNHSysUnitId_lbl.Size = New System.Drawing.Size(107, 23)
        Me.FNHSysUnitId_lbl.TabIndex = 268
        Me.FNHSysUnitId_lbl.Tag = "2|"
        Me.FNHSysUnitId_lbl.Text = "Unit :"
        '
        'FNHSysUnitIdPO
        '
        Me.FNHSysUnitIdPO.EnterMoveNextControl = True
        Me.FNHSysUnitIdPO.Location = New System.Drawing.Point(156, 57)
        Me.FNHSysUnitIdPO.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysUnitIdPO.Name = "FNHSysUnitIdPO"
        Me.FNHSysUnitIdPO.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysUnitIdPO.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysUnitIdPO.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysUnitIdPO.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitIdPO.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysUnitIdPO.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysUnitIdPO.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysUnitIdPO.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitIdPO.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysUnitIdPO.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysUnitIdPO.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysUnitIdPO.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysUnitIdPO.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysUnitIdPO.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysUnitIdPO.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "144", Nothing, True)})
        Me.FNHSysUnitIdPO.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysUnitIdPO.Properties.MaxLength = 30
        Me.FNHSysUnitIdPO.Size = New System.Drawing.Size(167, 22)
        Me.FNHSysUnitIdPO.TabIndex = 267
        Me.FNHSysUnitIdPO.Tag = "2|"
        '
        'ocmchange
        '
        Me.ocmchange.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmchange.Location = New System.Drawing.Point(89, 103)
        Me.ocmchange.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmchange.Name = "ocmchange"
        Me.ocmchange.Size = New System.Drawing.Size(112, 33)
        Me.ocmchange.TabIndex = 269
        Me.ocmchange.Text = "Change"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(249, 103)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(112, 33)
        Me.ocmcancel.TabIndex = 270
        Me.ocmcancel.Text = "Cancel"
        '
        'wAutoGeneratePOChangeUnit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(448, 154)
        Me.ControlBox = False
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmchange)
        Me.Controls.Add(Me.FNHSysUnitId_lbl)
        Me.Controls.Add(Me.FNHSysUnitIdPO)
        Me.Controls.Add(Me.FNHSysCurId)
        Me.Controls.Add(Me.FNHSysCurId_lbl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wAutoGeneratePOChangeUnit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Currency & Unit"
        CType(Me.FNHSysCurId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysUnitIdPO.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FNHSysCurId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysCurId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysUnitId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysUnitIdPO As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ocmchange As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
End Class
