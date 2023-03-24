<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAddEditConfigPayRoll
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
        Me.ocmdeletelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmedit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmaddnew = New DevExpress.XtraEditors.SimpleButton()
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
        Me.ocmsavelayuot.Location = New System.Drawing.Point(488, 5)
        Me.ocmsavelayuot.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsavelayuot.Name = "ocmsavelayuot"
        Me.ocmsavelayuot.Size = New System.Drawing.Size(54, 47)
        Me.ocmsavelayuot.TabIndex = 1
        Me.ocmsavelayuot.Text = "Save" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Layout"
        '
        'sbCustomization
        '
        Me.sbCustomization.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.sbCustomization.Location = New System.Drawing.Point(423, 5)
        Me.sbCustomization.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.sbCustomization.Name = "sbCustomization"
        Me.sbCustomization.Size = New System.Drawing.Size(62, 47)
        Me.sbCustomization.TabIndex = 0
        Me.sbCustomization.Text = "Custom" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'ogbbutton
        '
        Me.ogbbutton.Controls.Add(Me.ocmdeletelayout)
        Me.ogbbutton.Controls.Add(Me.ocmsavelayuot)
        Me.ogbbutton.Controls.Add(Me.ocmedit)
        Me.ogbbutton.Controls.Add(Me.sbCustomization)
        Me.ogbbutton.Controls.Add(Me.ocmexit)
        Me.ogbbutton.Controls.Add(Me.ocmclear)
        Me.ogbbutton.Controls.Add(Me.ocmdelete)
        Me.ogbbutton.Controls.Add(Me.ocmaddnew)
        Me.ogbbutton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ogbbutton.Location = New System.Drawing.Point(0, 532)
        Me.ogbbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbbutton.Name = "ogbbutton"
        Me.ogbbutton.Size = New System.Drawing.Size(726, 53)
        Me.ogbbutton.TabIndex = 4
        '
        'ocmdeletelayout
        '
        Me.ocmdeletelayout.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmdeletelayout.Location = New System.Drawing.Point(545, 5)
        Me.ocmdeletelayout.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdeletelayout.Name = "ocmdeletelayout"
        Me.ocmdeletelayout.Size = New System.Drawing.Size(54, 47)
        Me.ocmdeletelayout.TabIndex = 102
        Me.ocmdeletelayout.Text = "Delete" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Layout"
        '
        'ocmedit
        '
        Me.ocmedit.Location = New System.Drawing.Point(10, 12)
        Me.ocmedit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmedit.Name = "ocmedit"
        Me.ocmedit.Size = New System.Drawing.Size(111, 31)
        Me.ocmedit.TabIndex = 101
        Me.ocmedit.TabStop = False
        Me.ocmedit.Tag = "2|"
        Me.ocmedit.Text = "EDIT"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(601, 12)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 100
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(125, 12)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(111, 31)
        Me.ocmclear.TabIndex = 99
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmdelete
        '
        Me.ocmdelete.Location = New System.Drawing.Point(239, 12)
        Me.ocmdelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(111, 31)
        Me.ocmdelete.TabIndex = 98
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "DELETE"
        '
        'ocmaddnew
        '
        Me.ocmaddnew.Location = New System.Drawing.Point(10, 12)
        Me.ocmaddnew.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmaddnew.Name = "ocmaddnew"
        Me.ocmaddnew.Size = New System.Drawing.Size(111, 31)
        Me.ocmaddnew.TabIndex = 97
        Me.ocmaddnew.TabStop = False
        Me.ocmaddnew.Tag = "2|"
        Me.ocmaddnew.Text = "NEW"
        '
        'olymain
        '
        Me.olymain.Location = New System.Drawing.Point(98, 64)
        Me.olymain.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.olymain.Name = "olymain"
        Me.olymain.Root = Me.lyg
        Me.olymain.Size = New System.Drawing.Size(551, 345)
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
        Me.lyg.Size = New System.Drawing.Size(551, 345)
        Me.lyg.TextVisible = False
        '
        'wAddEditConfigPayRoll
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(726, 585)
        Me.ControlBox = False
        Me.Controls.Add(Me.olymain)
        Me.Controls.Add(Me.ogbbutton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wAddEditConfigPayRoll"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wAddEditDynamic"
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
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmaddnew As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents olymain As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents lyg As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents ocmedit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdeletelayout As DevExpress.XtraEditors.SimpleButton
End Class
