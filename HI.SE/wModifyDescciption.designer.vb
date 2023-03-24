<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wModifyDescciption
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
        Me.ogbbutton = New DevExpress.XtraEditors.GroupControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ogb = New DevExpress.XtraEditors.GroupControl()
        Me.FTDescEN_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTDescTH_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTDescEN = New DevExpress.XtraEditors.TextEdit()
        Me.FTDescTH = New DevExpress.XtraEditors.TextEdit()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbbutton.SuspendLayout()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogb.SuspendLayout()
        CType(Me.FTDescEN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTDescTH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbbutton
        '
        Me.ogbbutton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbbutton.Controls.Add(Me.ocmexit)
        Me.ogbbutton.Controls.Add(Me.ocmsave)
        Me.ogbbutton.Location = New System.Drawing.Point(3, 105)
        Me.ogbbutton.Name = "ogbbutton"
        Me.ogbbutton.ShowCaption = False
        Me.ogbbutton.Size = New System.Drawing.Size(473, 42)
        Me.ogbbutton.TabIndex = 2
        Me.ogbbutton.Text = "GroupControl1"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(296, 9)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(146, 25)
        Me.ocmexit.TabIndex = 1
        Me.ocmexit.Text = "EXIT"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(36, 9)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(146, 25)
        Me.ocmsave.TabIndex = 0
        Me.ocmsave.Text = "SAVE"
        '
        'ogb
        '
        Me.ogb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogb.Controls.Add(Me.FTDescEN_lbl)
        Me.ogb.Controls.Add(Me.FTDescTH_lbl)
        Me.ogb.Controls.Add(Me.FTDescEN)
        Me.ogb.Controls.Add(Me.FTDescTH)
        Me.ogb.Location = New System.Drawing.Point(3, 2)
        Me.ogb.Name = "ogb"
        Me.ogb.ShowCaption = False
        Me.ogb.Size = New System.Drawing.Size(473, 97)
        Me.ogb.TabIndex = 3
        Me.ogb.Text = "GroupControl1"
        '
        'FTDescEN_lbl
        '
        Me.FTDescEN_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTDescEN_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTDescEN_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTDescEN_lbl.Location = New System.Drawing.Point(9, 50)
        Me.FTDescEN_lbl.Name = "FTDescEN_lbl"
        Me.FTDescEN_lbl.Size = New System.Drawing.Size(128, 19)
        Me.FTDescEN_lbl.TabIndex = 304
        Me.FTDescEN_lbl.Tag = "2|"
        Me.FTDescEN_lbl.Text = "ภาษาอังกฤษ :"
        '
        'FTDescTH_lbl
        '
        Me.FTDescTH_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTDescTH_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTDescTH_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTDescTH_lbl.Location = New System.Drawing.Point(9, 25)
        Me.FTDescTH_lbl.Name = "FTDescTH_lbl"
        Me.FTDescTH_lbl.Size = New System.Drawing.Size(128, 19)
        Me.FTDescTH_lbl.TabIndex = 303
        Me.FTDescTH_lbl.Tag = "2|"
        Me.FTDescTH_lbl.Text = "ภาษาไทย :"
        '
        'FTDescEN
        '
        Me.FTDescEN.Location = New System.Drawing.Point(140, 50)
        Me.FTDescEN.Name = "FTDescEN"
        Me.FTDescEN.Properties.MaxLength = 100
        Me.FTDescEN.Size = New System.Drawing.Size(310, 20)
        Me.FTDescEN.TabIndex = 302
        Me.FTDescEN.Tag = "2|"
        '
        'FTDescTH
        '
        Me.FTDescTH.Location = New System.Drawing.Point(140, 25)
        Me.FTDescTH.Name = "FTDescTH"
        Me.FTDescTH.Properties.MaxLength = 100
        Me.FTDescTH.Size = New System.Drawing.Size(310, 20)
        Me.FTDescTH.TabIndex = 301
        Me.FTDescTH.Tag = "2|"
        '
        'wModifyDescciption
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(478, 150)
        Me.Controls.Add(Me.ogb)
        Me.Controls.Add(Me.ogbbutton)
        Me.Name = "wModifyDescciption"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Modify Descciption"
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbbutton.ResumeLayout(False)
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogb.ResumeLayout(False)
        CType(Me.FTDescEN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTDescTH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbbutton As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogb As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTDescTH As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTDescEN As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTDescEN_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTDescTH_lbl As DevExpress.XtraEditors.LabelControl
End Class
