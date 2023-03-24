<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wCopyCostSheet
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions3 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject9 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject10 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject11 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject12 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbdocinfo = New DevExpress.XtraEditors.GroupControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.TextEdit()
        Me.FNRevised = New DevExpress.XtraEditors.CalcEdit()
        Me.FTCostSheetNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTCostSheetNoSrc = New DevExpress.XtraEditors.TextEdit()
        Me.FTNewCostSheetNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTCostSheetNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbdocinfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdocinfo.SuspendLayout()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNRevised.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTCostSheetNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTCostSheetNoSrc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbdocinfo
        '
        Me.ogbdocinfo.Controls.Add(Me.FNHSysCmpId)
        Me.ogbdocinfo.Controls.Add(Me.FNRevised)
        Me.ogbdocinfo.Controls.Add(Me.FTCostSheetNo)
        Me.ogbdocinfo.Controls.Add(Me.FTCostSheetNoSrc)
        Me.ogbdocinfo.Controls.Add(Me.FTNewCostSheetNo_lbl)
        Me.ogbdocinfo.Controls.Add(Me.FTCostSheetNo_lbl)
        Me.ogbdocinfo.Location = New System.Drawing.Point(2, 4)
        Me.ogbdocinfo.Name = "ogbdocinfo"
        Me.ogbdocinfo.Size = New System.Drawing.Size(383, 141)
        Me.ogbdocinfo.TabIndex = 0
        Me.ogbdocinfo.Text = "Document"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(56, 113)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Size = New System.Drawing.Size(100, 20)
        Me.FNHSysCmpId.TabIndex = 145
        Me.FNHSysCmpId.Visible = False
        '
        'FNRevised
        '
        Me.FNRevised.Location = New System.Drawing.Point(177, 113)
        Me.FNRevised.Name = "FNRevised"
        Me.FNRevised.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNRevised.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNRevised.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FNRevised.Properties.Precision = 0
        Me.FNRevised.Properties.ReadOnly = True
        Me.FNRevised.Size = New System.Drawing.Size(161, 20)
        Me.FNRevised.TabIndex = 7
        Me.FNRevised.Visible = False
        '
        'FTCostSheetNo
        '
        Me.FTCostSheetNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTCostSheetNo.EnterMoveNextControl = True
        Me.FTCostSheetNo.Location = New System.Drawing.Point(177, 89)
        Me.FTCostSheetNo.Name = "FTCostSheetNo"
        SerializableAppearanceObject5.Options.UseTextOptions = True
        SerializableAppearanceObject5.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject6.Options.UseTextOptions = True
        SerializableAppearanceObject6.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject7.Options.UseTextOptions = True
        SerializableAppearanceObject7.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject8.Options.UseTextOptions = True
        SerializableAppearanceObject8.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject9.Options.UseTextOptions = True
        SerializableAppearanceObject9.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject10.Options.UseTextOptions = True
        SerializableAppearanceObject10.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject11.Options.UseTextOptions = True
        SerializableAppearanceObject11.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        SerializableAppearanceObject12.Options.UseTextOptions = True
        SerializableAppearanceObject12.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTCostSheetNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "...", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", "472", Nothing, DevExpress.Utils.ToolTipAnchor.[Default]), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "New", -1, True, True, False, EditorButtonImageOptions3, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F2), SerializableAppearanceObject9, SerializableAppearanceObject10, SerializableAppearanceObject11, SerializableAppearanceObject12, "", "d", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FTCostSheetNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTCostSheetNo.Size = New System.Drawing.Size(161, 20)
        Me.FTCostSheetNo.TabIndex = 5
        Me.FTCostSheetNo.TabStop = False
        Me.FTCostSheetNo.Tag = "2|"
        '
        'FTCostSheetNoSrc
        '
        Me.FTCostSheetNoSrc.Location = New System.Drawing.Point(177, 46)
        Me.FTCostSheetNoSrc.Name = "FTCostSheetNoSrc"
        Me.FTCostSheetNoSrc.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTCostSheetNoSrc.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTCostSheetNoSrc.Properties.ReadOnly = True
        Me.FTCostSheetNoSrc.Size = New System.Drawing.Size(161, 20)
        Me.FTCostSheetNoSrc.TabIndex = 2
        '
        'FTNewCostSheetNo_lbl
        '
        Me.FTNewCostSheetNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTNewCostSheetNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTNewCostSheetNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTNewCostSheetNo_lbl.Location = New System.Drawing.Point(5, 91)
        Me.FTNewCostSheetNo_lbl.Name = "FTNewCostSheetNo_lbl"
        Me.FTNewCostSheetNo_lbl.Size = New System.Drawing.Size(166, 13)
        Me.FTNewCostSheetNo_lbl.TabIndex = 1
        Me.FTNewCostSheetNo_lbl.Text = "To New Cost Sheet No. :"
        '
        'FTCostSheetNo_lbl
        '
        Me.FTCostSheetNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTCostSheetNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTCostSheetNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTCostSheetNo_lbl.Location = New System.Drawing.Point(5, 49)
        Me.FTCostSheetNo_lbl.Name = "FTCostSheetNo_lbl"
        Me.FTCostSheetNo_lbl.Size = New System.Drawing.Size(166, 13)
        Me.FTCostSheetNo_lbl.TabIndex = 0
        Me.FTCostSheetNo_lbl.Text = "From Cost Sheet No. :"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.ocmcancel)
        Me.GroupControl2.Controls.Add(Me.ocmok)
        Me.GroupControl2.Location = New System.Drawing.Point(2, 147)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.ShowCaption = False
        Me.GroupControl2.Size = New System.Drawing.Size(383, 36)
        Me.GroupControl2.TabIndex = 1
        Me.GroupControl2.Text = "GroupControl2"
        '
        'ocmcancel
        '
        Me.ocmcancel.Location = New System.Drawing.Point(231, 5)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(116, 26)
        Me.ocmcancel.TabIndex = 1
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmok
        '
        Me.ocmok.Location = New System.Drawing.Point(40, 5)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(116, 26)
        Me.ocmok.TabIndex = 0
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'wCopyCostSheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(388, 190)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.ogbdocinfo)
        Me.Name = "wCopyCostSheet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wCopyCostSheet"
        CType(Me.ogbdocinfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdocinfo.ResumeLayout(False)
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNRevised.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTCostSheetNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTCostSheetNoSrc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ogbdocinfo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTNewCostSheetNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTCostSheetNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTCostSheetNoSrc As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTCostSheetNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNRevised As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.TextEdit
End Class
