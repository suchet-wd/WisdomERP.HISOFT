<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wPOSPopUpSetServerName
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
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.FTComputerName = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FTComputerName_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.FTComputerName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.ocmcancel)
        Me.GroupControl1.Controls.Add(Me.ocmok)
        Me.GroupControl1.Controls.Add(Me.FTComputerName)
        Me.GroupControl1.Controls.Add(Me.FTComputerName_lbl)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(357, 107)
        Me.GroupControl1.TabIndex = 0
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(243, 63)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(87, 32)
        Me.ocmcancel.TabIndex = 3
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmok
        '
        Me.ocmok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmok.Location = New System.Drawing.Point(150, 63)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(87, 32)
        Me.ocmok.TabIndex = 2
        Me.ocmok.Text = "OK"
        '
        'FTComputerName
        '
        Me.FTComputerName.Location = New System.Drawing.Point(150, 26)
        Me.FTComputerName.Name = "FTComputerName"
        Me.FTComputerName.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTComputerName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTComputerName.Size = New System.Drawing.Size(178, 20)
        Me.FTComputerName.TabIndex = 1
        Me.FTComputerName.Tag = "2|"
        '
        'FTComputerName_lbl
        '
        Me.FTComputerName_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTComputerName_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTComputerName_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTComputerName_lbl.Location = New System.Drawing.Point(5, 23)
        Me.FTComputerName_lbl.Name = "FTComputerName_lbl"
        Me.FTComputerName_lbl.Size = New System.Drawing.Size(139, 21)
        Me.FTComputerName_lbl.TabIndex = 2
        Me.FTComputerName_lbl.Text = "Computer Name :"
        '
        'wPOSPopUpSetServerName
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(357, 107)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupControl1)
        Me.Name = "wPOSPopUpSetServerName"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wPOSPopUpSetServerName"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.FTComputerName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTComputerName As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FTComputerName_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
End Class
