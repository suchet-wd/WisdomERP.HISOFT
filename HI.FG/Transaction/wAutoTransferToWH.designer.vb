<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAutoTransferToWH
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.TextEdit()
        Me.ocmauto = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbnote = New DevExpress.XtraEditors.GroupControl()
        Me.FNHSysWHIdFGTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysWHIdFGTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTRemark = New DevExpress.XtraEditors.MemoEdit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbnote, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbnote.SuspendLayout()
        CType(Me.FNHSysWHIdFGTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Caption = "Check"
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(10, 6)
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
        Me.FNHSysCmpId.Size = New System.Drawing.Size(65, 22)
        Me.FNHSysCmpId.TabIndex = 285
        Me.FNHSysCmpId.Tag = "|"
        Me.FNHSysCmpId.Visible = False
        '
        'ocmauto
        '
        Me.ocmauto.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmauto.Location = New System.Drawing.Point(23, 131)
        Me.ocmauto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmauto.Name = "ocmauto"
        Me.ocmauto.Size = New System.Drawing.Size(187, 31)
        Me.ocmauto.TabIndex = 144
        Me.ocmauto.TabStop = False
        Me.ocmauto.Tag = "2|"
        Me.ocmauto.Text = "AUTO"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(217, 131)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(187, 31)
        Me.ocmcancel.TabIndex = 145
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'ogbnote
        '
        Me.ogbnote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbnote.Controls.Add(Me.FTRemark)
        Me.ogbnote.Controls.Add(Me.FNHSysWHIdFGTo_lbl)
        Me.ogbnote.Controls.Add(Me.FNHSysWHIdFGTo)
        Me.ogbnote.Controls.Add(Me.FNHSysCmpId)
        Me.ogbnote.Controls.Add(Me.ocmcancel)
        Me.ogbnote.Controls.Add(Me.ocmauto)
        Me.ogbnote.Location = New System.Drawing.Point(3, 2)
        Me.ogbnote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbnote.Name = "ogbnote"
        Me.ogbnote.ShowCaption = False
        Me.ogbnote.Size = New System.Drawing.Size(454, 175)
        Me.ogbnote.TabIndex = 142
        Me.ogbnote.Text = "GroupControl1"
        '
        'FNHSysWHIdFGTo_lbl
        '
        Me.FNHSysWHIdFGTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysWHIdFGTo_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysWHIdFGTo_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysWHIdFGTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysWHIdFGTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysWHIdFGTo_lbl.Location = New System.Drawing.Point(37, 27)
        Me.FNHSysWHIdFGTo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHIdFGTo_lbl.Name = "FNHSysWHIdFGTo_lbl"
        Me.FNHSysWHIdFGTo_lbl.Size = New System.Drawing.Size(147, 23)
        Me.FNHSysWHIdFGTo_lbl.TabIndex = 443
        Me.FNHSysWHIdFGTo_lbl.Tag = "2|"
        Me.FNHSysWHIdFGTo_lbl.Text = "FNHSysWHIdFGTo :"
        '
        'FNHSysWHIdFGTo
        '
        Me.FNHSysWHIdFGTo.EnterMoveNextControl = True
        Me.FNHSysWHIdFGTo.Location = New System.Drawing.Point(191, 27)
        Me.FNHSysWHIdFGTo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHIdFGTo.Name = "FNHSysWHIdFGTo"
        Me.FNHSysWHIdFGTo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysWHIdFGTo.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysWHIdFGTo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHIdFGTo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHIdFGTo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysWHIdFGTo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysWHIdFGTo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysWHIdFGTo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHIdFGTo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysWHIdFGTo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysWHIdFGTo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHIdFGTo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHIdFGTo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysWHIdFGTo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysWHIdFGTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "289", Nothing, True)})
        Me.FNHSysWHIdFGTo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysWHIdFGTo.Properties.MaxLength = 30
        Me.FNHSysWHIdFGTo.Size = New System.Drawing.Size(199, 22)
        Me.FNHSysWHIdFGTo.TabIndex = 442
        Me.FNHSysWHIdFGTo.Tag = "2|"
        '
        'FTRemark
        '
        Me.FTRemark.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTRemark.EditValue = ""
        Me.FTRemark.Location = New System.Drawing.Point(9, 64)
        Me.FTRemark.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Properties.MaxLength = 500
        Me.FTRemark.Size = New System.Drawing.Size(432, 59)
        Me.FTRemark.TabIndex = 444
        Me.FTRemark.Tag = "2|"
        Me.FTRemark.ToolTip = "Remark"
        Me.FTRemark.ToolTipTitle = "Remark"
        '
        'wAutoTransferToWH
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 177)
        Me.Controls.Add(Me.ogbnote)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wAutoTransferToWH"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " Auto Transfer To WH"
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbnote, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbnote.ResumeLayout(False)
        CType(Me.FNHSysWHIdFGTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmauto As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ogbnote As DevExpress.XtraEditors.GroupControl
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FNHSysWHIdFGTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysWHIdFGTo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTRemark As DevExpress.XtraEditors.MemoEdit
End Class
