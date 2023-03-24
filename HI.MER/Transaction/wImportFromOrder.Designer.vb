<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wImportFromOrder
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
        Me.FNHSysStyleId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysStyleId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId = New DevExpress.XtraEditors.ButtonEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'FNHSysStyleId_None
        '
        Me.FNHSysStyleId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysStyleId_None.Location = New System.Drawing.Point(318, 46)
        Me.FNHSysStyleId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId_None.Name = "FNHSysStyleId_None"
        Me.FNHSysStyleId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleId_None.Properties.ReadOnly = True
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(399, 22)
        Me.FNHSysStyleId_None.TabIndex = 283
        Me.FNHSysStyleId_None.Tag = "2|"
        '
        'FNHSysStyleId_lbl
        '
        Me.FNHSysStyleId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl.Location = New System.Drawing.Point(36, 46)
        Me.FNHSysStyleId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId_lbl.Name = "FNHSysStyleId_lbl"
        Me.FNHSysStyleId_lbl.Size = New System.Drawing.Size(125, 21)
        Me.FNHSysStyleId_lbl.TabIndex = 282
        Me.FNHSysStyleId_lbl.Tag = "2|"
        Me.FNHSysStyleId_lbl.Text = "Order No :"
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(162, 46)
        Me.FNHSysStyleId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "89", Nothing, True)})
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(155, 22)
        Me.FNHSysStyleId.TabIndex = 281
        Me.FNHSysStyleId.Tag = "2|"
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.FNHSysStyleId_None)
        Me.GroupControl1.Controls.Add(Me.FNHSysStyleId)
        Me.GroupControl1.Controls.Add(Me.FNHSysStyleId_lbl)
        Me.GroupControl1.Location = New System.Drawing.Point(3, 7)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(751, 97)
        Me.GroupControl1.TabIndex = 287
        Me.GroupControl1.Text = "Order No"
        '
        'GroupControl2
        '
        Me.GroupControl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl2.Controls.Add(Me.ocmcancel)
        Me.GroupControl2.Controls.Add(Me.ocmok)
        Me.GroupControl2.Location = New System.Drawing.Point(3, 110)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.ShowCaption = False
        Me.GroupControl2.Size = New System.Drawing.Size(751, 50)
        Me.GroupControl2.TabIndex = 288
        Me.GroupControl2.Text = "GroupControl2"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(435, 11)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(169, 31)
        Me.ocmcancel.TabIndex = 107
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmok
        '
        Me.ocmok.Location = New System.Drawing.Point(162, 11)
        Me.ocmok.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(155, 31)
        Me.ocmok.TabIndex = 106
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "Import"
        '
        'wImportFromOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(759, 166)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wImportFromOrder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import Color Way From Order"
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FNHSysStyleId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysStyleId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
End Class
