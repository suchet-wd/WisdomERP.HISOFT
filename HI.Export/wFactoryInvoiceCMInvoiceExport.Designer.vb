<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wFactoryInvoiceCMInvoiceExport
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
        Me.FDInvoiceExportDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDInvoiceExportDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTInvoiceExportNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRemark_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRemark = New DevExpress.XtraEditors.MemoEdit()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.FTInvoiceExportNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTPORef = New DevExpress.XtraEditors.TextEdit()
        CType(Me.FDInvoiceExportDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDInvoiceExportDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTInvoiceExportNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPORef.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FDInvoiceExportDate_lbl
        '
        Me.FDInvoiceExportDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FDInvoiceExportDate_lbl.Appearance.Options.UseForeColor = True
        Me.FDInvoiceExportDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FDInvoiceExportDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDInvoiceExportDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDInvoiceExportDate_lbl.Location = New System.Drawing.Point(423, 26)
        Me.FDInvoiceExportDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDInvoiceExportDate_lbl.Name = "FDInvoiceExportDate_lbl"
        Me.FDInvoiceExportDate_lbl.Size = New System.Drawing.Size(161, 23)
        Me.FDInvoiceExportDate_lbl.TabIndex = 529
        Me.FDInvoiceExportDate_lbl.Tag = "2|"
        Me.FDInvoiceExportDate_lbl.Text = "Invoice Export Date :"
        '
        'FDInvoiceExportDate
        '
        Me.FDInvoiceExportDate.EditValue = Nothing
        Me.FDInvoiceExportDate.EnterMoveNextControl = True
        Me.FDInvoiceExportDate.Location = New System.Drawing.Point(588, 27)
        Me.FDInvoiceExportDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDInvoiceExportDate.Name = "FDInvoiceExportDate"
        Me.FDInvoiceExportDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDInvoiceExportDate.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FDInvoiceExportDate.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FDInvoiceExportDate.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FDInvoiceExportDate.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FDInvoiceExportDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDInvoiceExportDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDInvoiceExportDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDInvoiceExportDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDInvoiceExportDate.Properties.NullDate = ""
        Me.FDInvoiceExportDate.Size = New System.Drawing.Size(126, 23)
        Me.FDInvoiceExportDate.TabIndex = 1
        Me.FDInvoiceExportDate.Tag = "2|"
        '
        'FTInvoiceExportNo_lbl
        '
        Me.FTInvoiceExportNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTInvoiceExportNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTInvoiceExportNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTInvoiceExportNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTInvoiceExportNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTInvoiceExportNo_lbl.Location = New System.Drawing.Point(19, 27)
        Me.FTInvoiceExportNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTInvoiceExportNo_lbl.Name = "FTInvoiceExportNo_lbl"
        Me.FTInvoiceExportNo_lbl.Size = New System.Drawing.Size(157, 23)
        Me.FTInvoiceExportNo_lbl.TabIndex = 528
        Me.FTInvoiceExportNo_lbl.Tag = "2|"
        Me.FTInvoiceExportNo_lbl.Text = "Invoice Export No :"
        '
        'FTRemark_lbl
        '
        Me.FTRemark_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTRemark_lbl.Appearance.Options.UseForeColor = True
        Me.FTRemark_lbl.Appearance.Options.UseTextOptions = True
        Me.FTRemark_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRemark_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRemark_lbl.Location = New System.Drawing.Point(62, 56)
        Me.FTRemark_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark_lbl.Name = "FTRemark_lbl"
        Me.FTRemark_lbl.Size = New System.Drawing.Size(107, 23)
        Me.FTRemark_lbl.TabIndex = 531
        Me.FTRemark_lbl.Tag = "2|"
        Me.FTRemark_lbl.Text = "Note :"
        '
        'FTRemark
        '
        Me.FTRemark.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTRemark.EditValue = ""
        Me.FTRemark.Location = New System.Drawing.Point(183, 56)
        Me.FTRemark.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Properties.MaxLength = 500
        Me.FTRemark.Size = New System.Drawing.Size(536, 127)
        Me.FTRemark.TabIndex = 2
        Me.FTRemark.Tag = "2|"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(545, 197)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(169, 33)
        Me.ocmcancel.TabIndex = 533
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmsave
        '
        Me.ocmsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmsave.Location = New System.Drawing.Point(183, 197)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(169, 33)
        Me.ocmsave.TabIndex = 532
        Me.ocmsave.Text = "SAVE INVOICE"
        '
        'FTInvoiceExportNo
        '
        Me.FTInvoiceExportNo.Location = New System.Drawing.Point(183, 27)
        Me.FTInvoiceExportNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTInvoiceExportNo.Name = "FTInvoiceExportNo"
        Me.FTInvoiceExportNo.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTInvoiceExportNo.Properties.Appearance.Options.UseForeColor = True
        Me.FTInvoiceExportNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTInvoiceExportNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTInvoiceExportNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTInvoiceExportNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTInvoiceExportNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "676", Nothing, True)})
        Me.FTInvoiceExportNo.Properties.MaxLength = 30
        Me.FTInvoiceExportNo.Size = New System.Drawing.Size(186, 23)
        Me.FTInvoiceExportNo.TabIndex = 0
        Me.FTInvoiceExportNo.Tag = "2|"
        '
        'FTPORef
        '
        Me.FTPORef.Location = New System.Drawing.Point(69, 113)
        Me.FTPORef.Name = "FTPORef"
        Me.FTPORef.Size = New System.Drawing.Size(100, 23)
        Me.FTPORef.TabIndex = 534
        Me.FTPORef.Visible = False
        '
        'wFactoryInvoiceCMInvoiceExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(797, 243)
        Me.ControlBox = False
        Me.Controls.Add(Me.FTPORef)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmsave)
        Me.Controls.Add(Me.FTRemark_lbl)
        Me.Controls.Add(Me.FTRemark)
        Me.Controls.Add(Me.FDInvoiceExportDate_lbl)
        Me.Controls.Add(Me.FDInvoiceExportDate)
        Me.Controls.Add(Me.FTInvoiceExportNo_lbl)
        Me.Controls.Add(Me.FTInvoiceExportNo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wFactoryInvoiceCMInvoiceExport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Invoice Export"
        CType(Me.FDInvoiceExportDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDInvoiceExportDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTInvoiceExportNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPORef.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FDInvoiceExportDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDInvoiceExportDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTInvoiceExportNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRemark_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRemark As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTInvoiceExportNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTPORef As DevExpress.XtraEditors.TextEdit
End Class
