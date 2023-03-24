<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wReserveAutoPO
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbcmd = New DevExpress.XtraEditors.GroupControl()
        Me.cmdok = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.FNHSysPurGrpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysPurGrpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysPurGrpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCmpRunId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysCmpRunId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpRunId_None = New DevExpress.XtraEditors.TextEdit()
        CType(Me.ogbcmd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbcmd.SuspendLayout()
        CType(Me.FNHSysPurGrpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysPurGrpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpRunId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpRunId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbcmd
        '
        Me.ogbcmd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbcmd.Controls.Add(Me.cmdok)
        Me.ogbcmd.Controls.Add(Me.ocmexit)
        Me.ogbcmd.Location = New System.Drawing.Point(2, 109)
        Me.ogbcmd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbcmd.Name = "ogbcmd"
        Me.ogbcmd.ShowCaption = False
        Me.ogbcmd.Size = New System.Drawing.Size(645, 53)
        Me.ogbcmd.TabIndex = 304
        Me.ogbcmd.Text = "GroupControl1"
        '
        'cmdok
        '
        Me.cmdok.Location = New System.Drawing.Point(147, 11)
        Me.cmdok.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(120, 31)
        Me.cmdok.TabIndex = 96
        Me.cmdok.TabStop = False
        Me.cmdok.Tag = "2|"
        Me.cmdok.Text = "OK"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(400, 11)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(120, 31)
        Me.ocmexit.TabIndex = 95
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'FNHSysPurGrpId
        '
        Me.FNHSysPurGrpId.EnterMoveNextControl = True
        Me.FNHSysPurGrpId.Location = New System.Drawing.Point(150, 58)
        Me.FNHSysPurGrpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysPurGrpId.Name = "FNHSysPurGrpId"
        Me.FNHSysPurGrpId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysPurGrpId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysPurGrpId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysPurGrpId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysPurGrpId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysPurGrpId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysPurGrpId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysPurGrpId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysPurGrpId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysPurGrpId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysPurGrpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysPurGrpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysPurGrpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysPurGrpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysPurGrpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "102", Nothing, True)})
        Me.FNHSysPurGrpId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysPurGrpId.Properties.MaxLength = 30
        Me.FNHSysPurGrpId.Size = New System.Drawing.Size(142, 23)
        Me.FNHSysPurGrpId.TabIndex = 305
        Me.FNHSysPurGrpId.Tag = "2|"
        '
        'FNHSysPurGrpId_lbl
        '
        Me.FNHSysPurGrpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysPurGrpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysPurGrpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysPurGrpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysPurGrpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysPurGrpId_lbl.Location = New System.Drawing.Point(2, 58)
        Me.FNHSysPurGrpId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysPurGrpId_lbl.Name = "FNHSysPurGrpId_lbl"
        Me.FNHSysPurGrpId_lbl.Size = New System.Drawing.Size(146, 23)
        Me.FNHSysPurGrpId_lbl.TabIndex = 309
        Me.FNHSysPurGrpId_lbl.Tag = "2|"
        Me.FNHSysPurGrpId_lbl.Text = "Group :"
        '
        'FNHSysPurGrpId_None
        '
        Me.FNHSysPurGrpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysPurGrpId_None.EnterMoveNextControl = True
        Me.FNHSysPurGrpId_None.Location = New System.Drawing.Point(298, 58)
        Me.FNHSysPurGrpId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysPurGrpId_None.Name = "FNHSysPurGrpId_None"
        Me.FNHSysPurGrpId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysPurGrpId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysPurGrpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysPurGrpId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysPurGrpId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysPurGrpId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysPurGrpId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysPurGrpId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysPurGrpId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysPurGrpId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysPurGrpId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysPurGrpId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysPurGrpId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysPurGrpId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysPurGrpId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysPurGrpId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysPurGrpId_None.Properties.ReadOnly = True
        Me.FNHSysPurGrpId_None.Size = New System.Drawing.Size(340, 23)
        Me.FNHSysPurGrpId_None.TabIndex = 310
        Me.FNHSysPurGrpId_None.TabStop = False
        Me.FNHSysPurGrpId_None.Tag = "2|"
        '
        'FNHSysCmpRunId
        '
        Me.FNHSysCmpRunId.EnterMoveNextControl = True
        Me.FNHSysCmpRunId.Location = New System.Drawing.Point(150, 28)
        Me.FNHSysCmpRunId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpRunId.Name = "FNHSysCmpRunId"
        Me.FNHSysCmpRunId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysCmpRunId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpRunId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpRunId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpRunId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysCmpRunId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysCmpRunId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysCmpRunId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpRunId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysCmpRunId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysCmpRunId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpRunId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpRunId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpRunId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpRunId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "87", Nothing, True)})
        Me.FNHSysCmpRunId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysCmpRunId.Properties.MaxLength = 30
        Me.FNHSysCmpRunId.Size = New System.Drawing.Size(144, 23)
        Me.FNHSysCmpRunId.TabIndex = 306
        Me.FNHSysCmpRunId.Tag = "2|"
        '
        'FNHSysCmpRunId_lbl
        '
        Me.FNHSysCmpRunId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpRunId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpRunId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpRunId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpRunId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpRunId_lbl.Location = New System.Drawing.Point(13, 27)
        Me.FNHSysCmpRunId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpRunId_lbl.Name = "FNHSysCmpRunId_lbl"
        Me.FNHSysCmpRunId_lbl.Size = New System.Drawing.Size(134, 23)
        Me.FNHSysCmpRunId_lbl.TabIndex = 307
        Me.FNHSysCmpRunId_lbl.Tag = "2|"
        Me.FNHSysCmpRunId_lbl.Text = "Company :"
        '
        'FNHSysCmpRunId_None
        '
        Me.FNHSysCmpRunId_None.EnterMoveNextControl = True
        Me.FNHSysCmpRunId_None.Location = New System.Drawing.Point(298, 29)
        Me.FNHSysCmpRunId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpRunId_None.Name = "FNHSysCmpRunId_None"
        Me.FNHSysCmpRunId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysCmpRunId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysCmpRunId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpRunId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpRunId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpRunId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpRunId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysCmpRunId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysCmpRunId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysCmpRunId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpRunId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysCmpRunId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysCmpRunId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpRunId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpRunId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpRunId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpRunId_None.Properties.ReadOnly = True
        Me.FNHSysCmpRunId_None.Size = New System.Drawing.Size(340, 23)
        Me.FNHSysCmpRunId_None.TabIndex = 308
        Me.FNHSysCmpRunId_None.TabStop = False
        Me.FNHSysCmpRunId_None.Tag = "2|"
        '
        'wReserveAutoPO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(650, 166)
        Me.Controls.Add(Me.FNHSysPurGrpId)
        Me.Controls.Add(Me.FNHSysPurGrpId_lbl)
        Me.Controls.Add(Me.FNHSysPurGrpId_None)
        Me.Controls.Add(Me.FNHSysCmpRunId)
        Me.Controls.Add(Me.FNHSysCmpRunId_lbl)
        Me.Controls.Add(Me.FNHSysCmpRunId_None)
        Me.Controls.Add(Me.ogbcmd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wReserveAutoPO"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reserve Auto PO"
        CType(Me.ogbcmd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbcmd.ResumeLayout(False)
        CType(Me.FNHSysPurGrpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysPurGrpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpRunId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpRunId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbcmd As DevExpress.XtraEditors.GroupControl
    Public WithEvents cmdok As DevExpress.XtraEditors.SimpleButton
    Public WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysPurGrpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysPurGrpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysPurGrpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpRunId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysCmpRunId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpRunId_None As DevExpress.XtraEditors.TextEdit
End Class
