<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wCopyOperationByStyle
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
        Me.FNHSysStyleId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysStyleId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysStyleIdTo_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysStyleIdTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleIdTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.ocmcopy = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleIdTo_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleIdTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FNHSysStyleId_None
        '
        Me.FNHSysStyleId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysStyleId_None.Location = New System.Drawing.Point(384, 58)
        Me.FNHSysStyleId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId_None.Name = "FNHSysStyleId_None"
        Me.FNHSysStyleId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleId_None.Properties.ReadOnly = True
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(323, 22)
        Me.FNHSysStyleId_None.TabIndex = 289
        Me.FNHSysStyleId_None.Tag = "2|"
        '
        'FNHSysStyleId_lbl
        '
        Me.FNHSysStyleId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl.Location = New System.Drawing.Point(28, 58)
        Me.FNHSysStyleId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId_lbl.Name = "FNHSysStyleId_lbl"
        Me.FNHSysStyleId_lbl.Size = New System.Drawing.Size(196, 25)
        Me.FNHSysStyleId_lbl.TabIndex = 290
        Me.FNHSysStyleId_lbl.Tag = "2|"
        Me.FNHSysStyleId_lbl.Text = "Copy To Style No :"
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(231, 58)
        Me.FNHSysStyleId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "157", Nothing, True)})
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(152, 22)
        Me.FNHSysStyleId.TabIndex = 288
        Me.FNHSysStyleId.Tag = "2|"
        '
        'FNHSysStyleIdTo_None
        '
        Me.FNHSysStyleIdTo_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysStyleIdTo_None.Location = New System.Drawing.Point(384, 26)
        Me.FNHSysStyleIdTo_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleIdTo_None.Name = "FNHSysStyleIdTo_None"
        Me.FNHSysStyleIdTo_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleIdTo_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleIdTo_None.Properties.ReadOnly = True
        Me.FNHSysStyleIdTo_None.Size = New System.Drawing.Size(323, 22)
        Me.FNHSysStyleIdTo_None.TabIndex = 292
        Me.FNHSysStyleIdTo_None.Tag = "2|"
        '
        'FNHSysStyleIdTo_lbl
        '
        Me.FNHSysStyleIdTo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleIdTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleIdTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleIdTo_lbl.Location = New System.Drawing.Point(28, 26)
        Me.FNHSysStyleIdTo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleIdTo_lbl.Name = "FNHSysStyleIdTo_lbl"
        Me.FNHSysStyleIdTo_lbl.Size = New System.Drawing.Size(196, 25)
        Me.FNHSysStyleIdTo_lbl.TabIndex = 293
        Me.FNHSysStyleIdTo_lbl.Tag = "2|"
        Me.FNHSysStyleIdTo_lbl.Text = "Copy From Style No :"
        '
        'FNHSysStyleIdTo
        '
        Me.FNHSysStyleIdTo.Enabled = False
        Me.FNHSysStyleIdTo.Location = New System.Drawing.Point(231, 26)
        Me.FNHSysStyleIdTo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleIdTo.Name = "FNHSysStyleIdTo"
        Me.FNHSysStyleIdTo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleIdTo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleIdTo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysStyleIdTo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysStyleIdTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "159", Nothing, True)})
        Me.FNHSysStyleIdTo.Properties.ReadOnly = True
        Me.FNHSysStyleIdTo.Properties.Tag = ""
        Me.FNHSysStyleIdTo.Size = New System.Drawing.Size(152, 22)
        Me.FNHSysStyleIdTo.TabIndex = 291
        Me.FNHSysStyleIdTo.Tag = "2|"
        '
        'ocmcopy
        '
        Me.ocmcopy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcopy.Location = New System.Drawing.Point(233, 121)
        Me.ocmcopy.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcopy.Name = "ocmcopy"
        Me.ocmcopy.Size = New System.Drawing.Size(152, 31)
        Me.ocmcopy.TabIndex = 294
        Me.ocmcopy.TabStop = False
        Me.ocmcopy.Tag = "2|"
        Me.ocmcopy.Text = "Copy"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(565, 121)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(142, 31)
        Me.ocmcancel.TabIndex = 295
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'wCopyOperationByStyle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(890, 165)
        Me.ControlBox = False
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmcopy)
        Me.Controls.Add(Me.FNHSysStyleIdTo_None)
        Me.Controls.Add(Me.FNHSysStyleIdTo_lbl)
        Me.Controls.Add(Me.FNHSysStyleIdTo)
        Me.Controls.Add(Me.FNHSysStyleId_None)
        Me.Controls.Add(Me.FNHSysStyleId_lbl)
        Me.Controls.Add(Me.FNHSysStyleId)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wCopyOperationByStyle"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Copy Operation By Style"
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleIdTo_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleIdTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FNHSysStyleId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysStyleId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysStyleIdTo_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysStyleIdTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleIdTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ocmcopy As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
End Class
