<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wDashBoardAddName
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
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.FTDashboardName = New DevExpress.XtraEditors.TextEdit()
        CType(Me.FTDashboardName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmok
        '
        Me.ocmok.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmok.Location = New System.Drawing.Point(63, 57)
        Me.ocmok.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(150, 31)
        Me.ocmok.TabIndex = 7
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(281, 57)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(150, 31)
        Me.ocmcancel.TabIndex = 8
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'FTDashboardName
        '
        Me.FTDashboardName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTDashboardName.EnterMoveNextControl = True
        Me.FTDashboardName.Location = New System.Drawing.Point(63, 18)
        Me.FTDashboardName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTDashboardName.Name = "FTDashboardName"
        Me.FTDashboardName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTDashboardName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTDashboardName.Properties.Appearance.Options.UseBackColor = True
        Me.FTDashboardName.Properties.Appearance.Options.UseForeColor = True
        Me.FTDashboardName.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTDashboardName.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTDashboardName.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTDashboardName.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTDashboardName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTDashboardName.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTDashboardName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTDashboardName.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTDashboardName.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTDashboardName.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTDashboardName.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTDashboardName.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTDashboardName.Properties.MaxLength = 100
        Me.FTDashboardName.Size = New System.Drawing.Size(368, 23)
        Me.FTDashboardName.TabIndex = 271
        Me.FTDashboardName.TabStop = False
        Me.FTDashboardName.Tag = "2|"
        '
        'wDashBoardAddName
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(496, 103)
        Me.ControlBox = False
        Me.Controls.Add(Me.FTDashboardName)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmok)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wDashBoardAddName"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Save Dashboard Name"
        CType(Me.FTDashboardName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTDashboardName As DevExpress.XtraEditors.TextEdit
End Class
