<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wInputHoldReason
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogcControl = New DevExpress.XtraEditors.GroupControl()
        Me.obtCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.obtOk = New DevExpress.XtraEditors.SimpleButton()
        Me.otbCancelReason = New DevExpress.XtraEditors.MemoEdit()
        Me.FNHSysPOHoldId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysPOHoldId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysPOHoldId_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.ogcControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogcControl.SuspendLayout()
        CType(Me.otbCancelReason.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysPOHoldId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysPOHoldId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcControl
        '
        Me.ogcControl.Controls.Add(Me.obtCancel)
        Me.ogcControl.Controls.Add(Me.obtOk)
        Me.ogcControl.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ogcControl.Location = New System.Drawing.Point(0, 204)
        Me.ogcControl.Name = "ogcControl"
        Me.ogcControl.ShowCaption = False
        Me.ogcControl.Size = New System.Drawing.Size(507, 30)
        Me.ogcControl.TabIndex = 218
        '
        'obtCancel
        '
        Me.obtCancel.Location = New System.Drawing.Point(369, 3)
        Me.obtCancel.Name = "obtCancel"
        Me.obtCancel.Size = New System.Drawing.Size(102, 25)
        Me.obtCancel.TabIndex = 1
        Me.obtCancel.Tag = "1|Cancel|ยกเลิก"
        Me.obtCancel.Text = "Cancel"
        '
        'obtOk
        '
        Me.obtOk.Location = New System.Drawing.Point(260, 3)
        Me.obtOk.Name = "obtOk"
        Me.obtOk.Size = New System.Drawing.Size(102, 25)
        Me.obtOk.TabIndex = 0
        Me.obtOk.Tag = "1|Ok|ตกลง"
        Me.obtOk.Text = "Ok"
        '
        'otbCancelReason
        '
        Me.otbCancelReason.Location = New System.Drawing.Point(0, 32)
        Me.otbCancelReason.Name = "otbCancelReason"
        Me.otbCancelReason.Size = New System.Drawing.Size(507, 172)
        Me.otbCancelReason.TabIndex = 0
        '
        'FNHSysPOHoldId_None
        '
        Me.FNHSysPOHoldId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysPOHoldId_None.EnterMoveNextControl = True
        Me.FNHSysPOHoldId_None.Location = New System.Drawing.Point(207, 6)
        Me.FNHSysPOHoldId_None.Name = "FNHSysPOHoldId_None"
        Me.FNHSysPOHoldId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysPOHoldId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysPOHoldId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysPOHoldId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysPOHoldId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysPOHoldId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysPOHoldId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysPOHoldId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysPOHoldId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysPOHoldId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysPOHoldId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysPOHoldId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysPOHoldId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysPOHoldId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysPOHoldId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysPOHoldId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysPOHoldId_None.Properties.ReadOnly = True
        Me.FNHSysPOHoldId_None.Size = New System.Drawing.Size(288, 20)
        Me.FNHSysPOHoldId_None.TabIndex = 281
        Me.FNHSysPOHoldId_None.TabStop = False
        Me.FNHSysPOHoldId_None.Tag = "2|"
        '
        'FNHSysPOHoldId
        '
        Me.FNHSysPOHoldId.EnterMoveNextControl = True
        Me.FNHSysPOHoldId.Location = New System.Drawing.Point(106, 6)
        Me.FNHSysPOHoldId.Name = "FNHSysPOHoldId"
        Me.FNHSysPOHoldId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysPOHoldId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysPOHoldId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysPOHoldId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysPOHoldId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysPOHoldId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysPOHoldId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysPOHoldId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysPOHoldId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysPOHoldId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysPOHoldId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysPOHoldId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysPOHoldId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysPOHoldId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysPOHoldId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "111157", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FNHSysPOHoldId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysPOHoldId.Properties.MaxLength = 30
        Me.FNHSysPOHoldId.Size = New System.Drawing.Size(99, 20)
        Me.FNHSysPOHoldId.TabIndex = 279
        Me.FNHSysPOHoldId.Tag = "2|"
        '
        'FNHSysPOHoldId_lbl
        '
        Me.FNHSysPOHoldId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysPOHoldId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysPOHoldId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysPOHoldId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysPOHoldId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysPOHoldId_lbl.Location = New System.Drawing.Point(3, 5)
        Me.FNHSysPOHoldId_lbl.Name = "FNHSysPOHoldId_lbl"
        Me.FNHSysPOHoldId_lbl.Size = New System.Drawing.Size(101, 19)
        Me.FNHSysPOHoldId_lbl.TabIndex = 280
        Me.FNHSysPOHoldId_lbl.Tag = "2|"
        Me.FNHSysPOHoldId_lbl.Text = "Hold PO Reason :"
        '
        'wInputHoldReason
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(507, 234)
        Me.Controls.Add(Me.FNHSysPOHoldId_None)
        Me.Controls.Add(Me.FNHSysPOHoldId)
        Me.Controls.Add(Me.FNHSysPOHoldId_lbl)
        Me.Controls.Add(Me.otbCancelReason)
        Me.Controls.Add(Me.ogcControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wInputHoldReason"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "1|Reason|Reason"
        Me.Text = "Hold PO Reason"
        CType(Me.ogcControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogcControl.ResumeLayout(False)
        CType(Me.otbCancelReason.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysPOHoldId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysPOHoldId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcControl As DevExpress.XtraEditors.GroupControl
    Friend WithEvents obtCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents obtOk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otbCancelReason As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FNHSysPOHoldId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysPOHoldId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysPOHoldId_lbl As DevExpress.XtraEditors.LabelControl
End Class
