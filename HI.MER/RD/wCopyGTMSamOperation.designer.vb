<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wCopyGTMSamOperation
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
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbCopyOrderHeader = New DevExpress.XtraEditors.GroupControl()
        Me.FNHSysSeasonId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysSeasonId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysStyleId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogbCopyOrderNoConfirm = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbCopyOrderHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbCopyOrderHeader.SuspendLayout()
        CType(Me.FNHSysSeasonId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbCopyOrderNoConfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbCopyOrderNoConfirm.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbCopyOrderHeader
        '
        Me.ogbCopyOrderHeader.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNHSysSeasonId)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNHSysSeasonId_lbl)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNHSysStyleId_None)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNHSysStyleId_lbl)
        Me.ogbCopyOrderHeader.Controls.Add(Me.FNHSysStyleId)
        Me.ogbCopyOrderHeader.Location = New System.Drawing.Point(4, 4)
        Me.ogbCopyOrderHeader.Name = "ogbCopyOrderHeader"
        Me.ogbCopyOrderHeader.Size = New System.Drawing.Size(659, 101)
        Me.ogbCopyOrderHeader.TabIndex = 288
        Me.ogbCopyOrderHeader.Text = "Source Style No."
        '
        'FNHSysSeasonId
        '
        Me.FNHSysSeasonId.Location = New System.Drawing.Point(145, 58)
        Me.FNHSysSeasonId.Name = "FNHSysSeasonId"
        Me.FNHSysSeasonId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSeasonId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSeasonId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysSeasonId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysSeasonId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "94", Nothing, True)})
        Me.FNHSysSeasonId.Properties.ReadOnly = True
        Me.FNHSysSeasonId.Properties.Tag = ""
        Me.FNHSysSeasonId.Size = New System.Drawing.Size(162, 20)
        Me.FNHSysSeasonId.TabIndex = 568
        Me.FNHSysSeasonId.Tag = "2|"
        '
        'FNHSysSeasonId_lbl
        '
        Me.FNHSysSeasonId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSeasonId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysSeasonId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysSeasonId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysSeasonId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysSeasonId_lbl.Location = New System.Drawing.Point(47, 58)
        Me.FNHSysSeasonId_lbl.Name = "FNHSysSeasonId_lbl"
        Me.FNHSysSeasonId_lbl.Size = New System.Drawing.Size(96, 19)
        Me.FNHSysSeasonId_lbl.TabIndex = 567
        Me.FNHSysSeasonId_lbl.Tag = "2|"
        Me.FNHSysSeasonId_lbl.Text = "Season :"
        '
        'FNHSysStyleId_None
        '
        Me.FNHSysStyleId_None.Location = New System.Drawing.Point(310, 34)
        Me.FNHSysStyleId_None.Name = "FNHSysStyleId_None"
        Me.FNHSysStyleId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleId_None.Properties.ReadOnly = True
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(269, 20)
        Me.FNHSysStyleId_None.TabIndex = 566
        Me.FNHSysStyleId_None.Tag = "2|"
        '
        'FNHSysStyleId_lbl
        '
        Me.FNHSysStyleId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysStyleId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysStyleId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl.Location = New System.Drawing.Point(16, 34)
        Me.FNHSysStyleId_lbl.Name = "FNHSysStyleId_lbl"
        Me.FNHSysStyleId_lbl.Size = New System.Drawing.Size(127, 18)
        Me.FNHSysStyleId_lbl.TabIndex = 564
        Me.FNHSysStyleId_lbl.Tag = "2|"
        Me.FNHSysStyleId_lbl.Text = "Style :"
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(145, 34)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysStyleId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "671", Nothing, True), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "d", Nothing, True)})
        Me.FNHSysStyleId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysStyleId.Properties.ReadOnly = True
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(162, 20)
        Me.FNHSysStyleId.TabIndex = 565
        Me.FNHSysStyleId.Tag = "2|"
        '
        'ogbCopyOrderNoConfirm
        '
        Me.ogbCopyOrderNoConfirm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbCopyOrderNoConfirm.Controls.Add(Me.ocmcancel)
        Me.ogbCopyOrderNoConfirm.Controls.Add(Me.ocmok)
        Me.ogbCopyOrderNoConfirm.Location = New System.Drawing.Point(4, 110)
        Me.ogbCopyOrderNoConfirm.Name = "ogbCopyOrderNoConfirm"
        Me.ogbCopyOrderNoConfirm.ShowCaption = False
        Me.ogbCopyOrderNoConfirm.Size = New System.Drawing.Size(659, 41)
        Me.ogbCopyOrderNoConfirm.TabIndex = 289
        Me.ogbCopyOrderNoConfirm.Text = "GroupControl2"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(382, 9)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(145, 25)
        Me.ocmcancel.TabIndex = 1
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmok
        '
        Me.ocmok.Location = New System.Drawing.Point(133, 9)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(133, 25)
        Me.ocmok.TabIndex = 0
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'wCopyGTMSamOperation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(667, 157)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogbCopyOrderNoConfirm)
        Me.Controls.Add(Me.ogbCopyOrderHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wCopyGTMSamOperation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Copy GTM Sam Operation"
        CType(Me.ogbCopyOrderHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbCopyOrderHeader.ResumeLayout(False)
        CType(Me.FNHSysSeasonId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbCopyOrderNoConfirm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbCopyOrderNoConfirm.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbCopyOrderHeader As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbCopyOrderNoConfirm As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysSeasonId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysSeasonId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysStyleId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId As DevExpress.XtraEditors.ButtonEdit
End Class
