<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wCopyOrderSub
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
        Me.ogbCopySubOrderHeader = New DevExpress.XtraEditors.GroupControl()
        Me.FTSubOrderNo = New DevExpress.XtraEditors.TextEdit()
        Me.FTSubOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNCopySubOrderNo_Desc = New DevExpress.XtraEditors.LabelControl()
        Me.FNCopySubOrderNo = New DevExpress.XtraEditors.CalcEdit()
        Me.FNCopySubOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbCopyOrderNoConfirm = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbCopySubOrderHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbCopySubOrderHeader.SuspendLayout()
        CType(Me.FTSubOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNCopySubOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbCopyOrderNoConfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbCopyOrderNoConfirm.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbCopySubOrderHeader
        '
        Me.ogbCopySubOrderHeader.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbCopySubOrderHeader.Controls.Add(Me.FTSubOrderNo)
        Me.ogbCopySubOrderHeader.Controls.Add(Me.FTSubOrderNo_lbl)
        Me.ogbCopySubOrderHeader.Controls.Add(Me.FNCopySubOrderNo_Desc)
        Me.ogbCopySubOrderHeader.Controls.Add(Me.FNCopySubOrderNo)
        Me.ogbCopySubOrderHeader.Controls.Add(Me.FNCopySubOrderNo_lbl)
        Me.ogbCopySubOrderHeader.Location = New System.Drawing.Point(5, 5)
        Me.ogbCopySubOrderHeader.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbCopySubOrderHeader.Name = "ogbCopySubOrderHeader"
        Me.ogbCopySubOrderHeader.Size = New System.Drawing.Size(602, 116)
        Me.ogbCopySubOrderHeader.TabIndex = 0
        Me.ogbCopySubOrderHeader.Text = "Source Factory Sub Order No."
        '
        'FTSubOrderNo
        '
        Me.FTSubOrderNo.Location = New System.Drawing.Point(222, 39)
        Me.FTSubOrderNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSubOrderNo.Name = "FTSubOrderNo"
        Me.FTSubOrderNo.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FTSubOrderNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTSubOrderNo.Properties.ReadOnly = True
        Me.FTSubOrderNo.Size = New System.Drawing.Size(155, 23)
        Me.FTSubOrderNo.TabIndex = 0
        Me.FTSubOrderNo.TabStop = False
        Me.FTSubOrderNo.Tag = ""
        '
        'FTSubOrderNo_lbl
        '
        Me.FTSubOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTSubOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTSubOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTSubOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSubOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSubOrderNo_lbl.Location = New System.Drawing.Point(6, 41)
        Me.FTSubOrderNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSubOrderNo_lbl.Name = "FTSubOrderNo_lbl"
        Me.FTSubOrderNo_lbl.Size = New System.Drawing.Size(209, 21)
        Me.FTSubOrderNo_lbl.TabIndex = 527
        Me.FTSubOrderNo_lbl.Tag = "2|"
        Me.FTSubOrderNo_lbl.Text = "Source Factory Sub Order No :"
        '
        'FNCopySubOrderNo_Desc
        '
        Me.FNCopySubOrderNo_Desc.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNCopySubOrderNo_Desc.Appearance.Options.UseForeColor = True
        Me.FNCopySubOrderNo_Desc.Appearance.Options.UseTextOptions = True
        Me.FNCopySubOrderNo_Desc.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FNCopySubOrderNo_Desc.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNCopySubOrderNo_Desc.Location = New System.Drawing.Point(299, 75)
        Me.FNCopySubOrderNo_Desc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNCopySubOrderNo_Desc.Name = "FNCopySubOrderNo_Desc"
        Me.FNCopySubOrderNo_Desc.Size = New System.Drawing.Size(286, 21)
        Me.FNCopySubOrderNo_Desc.TabIndex = 526
        Me.FNCopySubOrderNo_Desc.Tag = "2|"
        Me.FNCopySubOrderNo_Desc.Text = "เลขที่ใบสั่งผลิตย่อย / Factory Sub Order No."
        '
        'FNCopySubOrderNo
        '
        Me.FNCopySubOrderNo.EnterMoveNextControl = True
        Me.FNCopySubOrderNo.Location = New System.Drawing.Point(222, 71)
        Me.FNCopySubOrderNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNCopySubOrderNo.Name = "FNCopySubOrderNo"
        Me.FNCopySubOrderNo.Properties.Appearance.Options.UseTextOptions = True
        Me.FNCopySubOrderNo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNCopySubOrderNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNCopySubOrderNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNCopySubOrderNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNCopySubOrderNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNCopySubOrderNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNCopySubOrderNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNCopySubOrderNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNCopySubOrderNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNCopySubOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNCopySubOrderNo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCopySubOrderNo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCopySubOrderNo.Properties.Precision = 0
        Me.FNCopySubOrderNo.Size = New System.Drawing.Size(70, 23)
        Me.FNCopySubOrderNo.TabIndex = 1
        Me.FNCopySubOrderNo.Tag = "2|"
        '
        'FNCopySubOrderNo_lbl
        '
        Me.FNCopySubOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNCopySubOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FNCopySubOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FNCopySubOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNCopySubOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNCopySubOrderNo_lbl.Location = New System.Drawing.Point(65, 73)
        Me.FNCopySubOrderNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNCopySubOrderNo_lbl.Name = "FNCopySubOrderNo_lbl"
        Me.FNCopySubOrderNo_lbl.Size = New System.Drawing.Size(149, 21)
        Me.FNCopySubOrderNo_lbl.TabIndex = 524
        Me.FNCopySubOrderNo_lbl.Tag = "2|"
        Me.FNCopySubOrderNo_lbl.Text = "จำนวน  :"
        '
        'ogbCopyOrderNoConfirm
        '
        Me.ogbCopyOrderNoConfirm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbCopyOrderNoConfirm.Controls.Add(Me.ocmcancel)
        Me.ogbCopyOrderNoConfirm.Controls.Add(Me.ocmok)
        Me.ogbCopyOrderNoConfirm.Location = New System.Drawing.Point(5, 128)
        Me.ogbCopyOrderNoConfirm.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbCopyOrderNoConfirm.Name = "ogbCopyOrderNoConfirm"
        Me.ogbCopyOrderNoConfirm.ShowCaption = False
        Me.ogbCopyOrderNoConfirm.Size = New System.Drawing.Size(602, 50)
        Me.ogbCopyOrderNoConfirm.TabIndex = 290
        Me.ogbCopyOrderNoConfirm.Text = "GroupControl2"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(353, 11)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(169, 31)
        Me.ocmcancel.TabIndex = 1
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmok
        '
        Me.ocmok.Location = New System.Drawing.Point(80, 11)
        Me.ocmok.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(155, 31)
        Me.ocmok.TabIndex = 0
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'wCopyOrderSub
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(611, 186)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogbCopyOrderNoConfirm)
        Me.Controls.Add(Me.ogbCopySubOrderHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wCopyOrderSub"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Copy Factory Sub Order No."
        CType(Me.ogbCopySubOrderHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbCopySubOrderHeader.ResumeLayout(False)
        CType(Me.FTSubOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNCopySubOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbCopyOrderNoConfirm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbCopyOrderNoConfirm.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbCopySubOrderHeader As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTSubOrderNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTSubOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNCopySubOrderNo_Desc As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNCopySubOrderNo As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNCopySubOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbCopyOrderNoConfirm As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
End Class
