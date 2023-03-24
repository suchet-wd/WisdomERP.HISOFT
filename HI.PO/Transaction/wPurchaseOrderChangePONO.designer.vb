<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPurchaseOrderChangePONO
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.ocmadd = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.FTPurchaseNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTPurchaseNo = New DevExpress.XtraEditors.TextEdit()
        Me.FTRemark_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRemark = New DevExpress.XtraEditors.MemoEdit()
        CType(Me.FTPurchaseNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmadd
        '
        Me.ocmadd.Location = New System.Drawing.Point(97, 201)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(121, 27)
        Me.ocmadd.TabIndex = 8
        Me.ocmadd.Text = "Confirm To Change"
        '
        'ocmcancel
        '
        Me.ocmcancel.Location = New System.Drawing.Point(459, 201)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(121, 27)
        Me.ocmcancel.TabIndex = 283
        Me.ocmcancel.Text = "CANCEL"
        '
        'FTPurchaseNo_lbl
        '
        Me.FTPurchaseNo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTPurchaseNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTPurchaseNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPurchaseNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPurchaseNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPurchaseNo_lbl.Location = New System.Drawing.Point(12, 13)
        Me.FTPurchaseNo_lbl.Name = "FTPurchaseNo_lbl"
        Me.FTPurchaseNo_lbl.Size = New System.Drawing.Size(147, 19)
        Me.FTPurchaseNo_lbl.TabIndex = 293
        Me.FTPurchaseNo_lbl.Tag = "2|"
        Me.FTPurchaseNo_lbl.Text = "New Purchase Format :"
        '
        'FTPurchaseNo
        '
        Me.FTPurchaseNo.EnterMoveNextControl = True
        Me.FTPurchaseNo.Location = New System.Drawing.Point(165, 13)
        Me.FTPurchaseNo.Name = "FTPurchaseNo"
        Me.FTPurchaseNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTPurchaseNo.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTPurchaseNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTPurchaseNo.Properties.Appearance.Options.UseForeColor = True
        Me.FTPurchaseNo.Properties.Appearance.Options.UseTextOptions = True
        Me.FTPurchaseNo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPurchaseNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTPurchaseNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTPurchaseNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTPurchaseNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTPurchaseNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTPurchaseNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTPurchaseNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTPurchaseNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTPurchaseNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTPurchaseNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTPurchaseNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTPurchaseNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTPurchaseNo.Properties.ReadOnly = True
        Me.FTPurchaseNo.Size = New System.Drawing.Size(231, 20)
        Me.FTPurchaseNo.TabIndex = 291
        Me.FTPurchaseNo.TabStop = False
        Me.FTPurchaseNo.Tag = "2|"
        '
        'FTRemark_lbl
        '
        Me.FTRemark_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTRemark_lbl.Appearance.Options.UseForeColor = True
        Me.FTRemark_lbl.Appearance.Options.UseTextOptions = True
        Me.FTRemark_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRemark_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRemark_lbl.Location = New System.Drawing.Point(61, 37)
        Me.FTRemark_lbl.Name = "FTRemark_lbl"
        Me.FTRemark_lbl.Size = New System.Drawing.Size(92, 19)
        Me.FTRemark_lbl.TabIndex = 295
        Me.FTRemark_lbl.Tag = "2|"
        Me.FTRemark_lbl.Text = "Note :"
        '
        'FTRemark
        '
        Me.FTRemark.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTRemark.EditValue = ""
        Me.FTRemark.Location = New System.Drawing.Point(165, 38)
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Properties.MaxLength = 500
        Me.FTRemark.Size = New System.Drawing.Size(395, 141)
        Me.FTRemark.TabIndex = 294
        Me.FTRemark.Tag = "2|"
        '
        'wPurchaseOrderChangePONO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(672, 245)
        Me.ControlBox = False
        Me.Controls.Add(Me.FTRemark_lbl)
        Me.Controls.Add(Me.FTRemark)
        Me.Controls.Add(Me.FTPurchaseNo_lbl)
        Me.Controls.Add(Me.FTPurchaseNo)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmadd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wPurchaseOrderChangePONO"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Change Purchse No"
        CType(Me.FTPurchaseNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTPurchaseNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPurchaseNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTRemark_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRemark As DevExpress.XtraEditors.MemoEdit
End Class
