<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wJobCostAll
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
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton2 = New DevExpress.XtraEditors.SimpleButton()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.SFTDateTrans = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.SFTDateTrans_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SFTDateTrans.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SFTDateTrans.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Location = New System.Drawing.Point(550, 55)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(75, 23)
        Me.SimpleButton1.TabIndex = 0
        Me.SimpleButton1.Text = "Save All"
        '
        'SimpleButton2
        '
        Me.SimpleButton2.Location = New System.Drawing.Point(663, 55)
        Me.SimpleButton2.Name = "SimpleButton2"
        Me.SimpleButton2.Size = New System.Drawing.Size(75, 23)
        Me.SimpleButton2.TabIndex = 0
        Me.SimpleButton2.Text = "Close"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(299, 13)
        Me.FNHSysCmpId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(453, 23)
        Me.FNHSysCmpId_None.TabIndex = 510
        Me.FNHSysCmpId_None.Tag = ""
        '
        'SFTDateTrans
        '
        Me.SFTDateTrans.EditValue = Nothing
        Me.SFTDateTrans.EnterMoveNextControl = True
        Me.SFTDateTrans.Location = New System.Drawing.Point(152, 39)
        Me.SFTDateTrans.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SFTDateTrans.Name = "SFTDateTrans"
        Me.SFTDateTrans.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFTDateTrans.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SFTDateTrans.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.SFTDateTrans.Properties.DisplayFormat.FormatString = "MM/yyyy"
        Me.SFTDateTrans.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.SFTDateTrans.Properties.EditFormat.FormatString = "MM/yyyy"
        Me.SFTDateTrans.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.SFTDateTrans.Properties.Mask.EditMask = "MM/yyyy"
        Me.SFTDateTrans.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.SFTDateTrans.Properties.NullDate = ""
        Me.SFTDateTrans.Size = New System.Drawing.Size(140, 23)
        Me.SFTDateTrans.TabIndex = 506
        Me.SFTDateTrans.Tag = "2|"
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(4, 13)
        Me.FNHSysCmpId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(143, 21)
        Me.FNHSysCmpId_lbl.TabIndex = 509
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'SFTDateTrans_lbl
        '
        Me.SFTDateTrans_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.SFTDateTrans_lbl.Appearance.Options.UseForeColor = True
        Me.SFTDateTrans_lbl.Appearance.Options.UseTextOptions = True
        Me.SFTDateTrans_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFTDateTrans_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.SFTDateTrans_lbl.Location = New System.Drawing.Point(20, 39)
        Me.SFTDateTrans_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SFTDateTrans_lbl.Name = "SFTDateTrans_lbl"
        Me.SFTDateTrans_lbl.Size = New System.Drawing.Size(128, 25)
        Me.SFTDateTrans_lbl.TabIndex = 507
        Me.SFTDateTrans_lbl.Tag = "2|"
        Me.SFTDateTrans_lbl.Text = "ประจำเดือน :"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(151, 13)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(141, 23)
        Me.FNHSysCmpId.TabIndex = 508
        Me.FNHSysCmpId.Tag = ""
        '
        'wJobCostAll
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(764, 103)
        Me.Controls.Add(Me.FNHSysCmpId_None)
        Me.Controls.Add(Me.SFTDateTrans)
        Me.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.Controls.Add(Me.SFTDateTrans_lbl)
        Me.Controls.Add(Me.FNHSysCmpId)
        Me.Controls.Add(Me.SimpleButton2)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Name = "wJobCostAll"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wJobCostAll"
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SFTDateTrans.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SFTDateTrans.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents SFTDateTrans As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SFTDateTrans_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
End Class
