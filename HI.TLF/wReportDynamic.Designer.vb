<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wReportDynamic
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
        Me.ocmsavelayuot = New DevExpress.XtraEditors.SimpleButton()
        Me.sbCustomization = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdeletelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.olymain = New DevExpress.XtraLayout.LayoutControl()
        Me.lyg = New DevExpress.XtraLayout.LayoutControlGroup()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbbutton.SuspendLayout()
        CType(Me.olymain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lyg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmsavelayuot
        '
        Me.ocmsavelayuot.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmsavelayuot.Location = New System.Drawing.Point(314, 5)
        Me.ocmsavelayuot.Name = "ocmsavelayuot"
        Me.ocmsavelayuot.Size = New System.Drawing.Size(40, 38)
        Me.ocmsavelayuot.TabIndex = 1
        Me.ocmsavelayuot.TabStop = False
        Me.ocmsavelayuot.Text = "Save" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Layout"
        '
        'sbCustomization
        '
        Me.sbCustomization.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.sbCustomization.Location = New System.Drawing.Point(264, 5)
        Me.sbCustomization.Name = "sbCustomization"
        Me.sbCustomization.Size = New System.Drawing.Size(42, 38)
        Me.sbCustomization.TabIndex = 0
        Me.sbCustomization.TabStop = False
        Me.sbCustomization.Text = "Custom" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'ogbbutton
        '
        Me.ogbbutton.Controls.Add(Me.ocmpreview)
        Me.ogbbutton.Controls.Add(Me.ocmdeletelayout)
        Me.ogbbutton.Controls.Add(Me.ocmsavelayuot)
        Me.ogbbutton.Controls.Add(Me.sbCustomization)
        Me.ogbbutton.Controls.Add(Me.ocmexit)
        Me.ogbbutton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ogbbutton.Location = New System.Drawing.Point(0, 432)
        Me.ogbbutton.Name = "ogbbutton"
        Me.ogbbutton.Size = New System.Drawing.Size(649, 43)
        Me.ogbbutton.TabIndex = 4
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(80, 11)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(155, 25)
        Me.ocmpreview.TabIndex = 103
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "Preview"
        '
        'ocmdeletelayout
        '
        Me.ocmdeletelayout.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmdeletelayout.Location = New System.Drawing.Point(360, 5)
        Me.ocmdeletelayout.Name = "ocmdeletelayout"
        Me.ocmdeletelayout.Size = New System.Drawing.Size(46, 38)
        Me.ocmdeletelayout.TabIndex = 102
        Me.ocmdeletelayout.TabStop = False
        Me.ocmdeletelayout.Text = "Delete" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Layout"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(411, 11)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(155, 25)
        Me.ocmexit.TabIndex = 100
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'olymain
        '
        Me.olymain.Location = New System.Drawing.Point(84, 52)
        Me.olymain.Name = "olymain"
        Me.olymain.Root = Me.lyg
        Me.olymain.Size = New System.Drawing.Size(472, 280)
        Me.olymain.TabIndex = 6
        Me.olymain.Text = "LayoutControl1"
        '
        'lyg
        '
        Me.lyg.CustomizationFormText = "LayoutControlGroup1"
        Me.lyg.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.lyg.GroupBordersVisible = False
        Me.lyg.Location = New System.Drawing.Point(0, 0)
        Me.lyg.Name = "lyg"
        Me.lyg.Size = New System.Drawing.Size(472, 280)
        Me.lyg.Text = "lyg"
        Me.lyg.TextVisible = False
        '
        'wReportDynamic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(649, 475)
        Me.ControlBox = False
        Me.Controls.Add(Me.olymain)
        Me.Controls.Add(Me.ogbbutton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wReportDynamic"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wReportDynamic"
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbbutton.ResumeLayout(False)
        CType(Me.olymain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lyg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmsavelayuot As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents sbCustomization As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogbbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents olymain As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents lyg As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents ocmdeletelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
End Class
