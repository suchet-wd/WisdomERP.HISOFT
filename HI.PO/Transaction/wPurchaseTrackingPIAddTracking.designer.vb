<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPurchaseTrackingPIAddTracking
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
        Me.FTRemark_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRemark = New DevExpress.XtraEditors.MemoEdit()
        Me.ocmadd = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.FTContactName = New DevExpress.XtraEditors.TextEdit()
        Me.FTPINo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTTrackDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTTrackDate = New DevExpress.XtraEditors.DateEdit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTContactName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTTrackDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTTrackDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FTRemark_lbl
        '
        Me.FTRemark_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTRemark_lbl.Appearance.Options.UseForeColor = True
        Me.FTRemark_lbl.Appearance.Options.UseTextOptions = True
        Me.FTRemark_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRemark_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRemark_lbl.Location = New System.Drawing.Point(48, 57)
        Me.FTRemark_lbl.Name = "FTRemark_lbl"
        Me.FTRemark_lbl.Size = New System.Drawing.Size(87, 19)
        Me.FTRemark_lbl.TabIndex = 281
        Me.FTRemark_lbl.Tag = "2|"
        Me.FTRemark_lbl.Text = "Track Note :"
        '
        'FTRemark
        '
        Me.FTRemark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.FTRemark.EditValue = ""
        Me.FTRemark.Location = New System.Drawing.Point(137, 56)
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Properties.MaxLength = 1000
        Me.FTRemark.Size = New System.Drawing.Size(746, 255)
        Me.FTRemark.TabIndex = 12
        Me.FTRemark.Tag = "2|"
        '
        'ocmadd
        '
        Me.ocmadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmadd.Location = New System.Drawing.Point(137, 322)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(145, 27)
        Me.ocmadd.TabIndex = 13
        Me.ocmadd.Text = "ADD TRACKING"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(661, 322)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(145, 27)
        Me.ocmcancel.TabIndex = 14
        Me.ocmcancel.Text = "CANCEL"
        '
        'FTContactName
        '
        Me.FTContactName.EnterMoveNextControl = True
        Me.FTContactName.Location = New System.Drawing.Point(136, 4)
        Me.FTContactName.Name = "FTContactName"
        Me.FTContactName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTContactName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTContactName.Properties.Appearance.Options.UseBackColor = True
        Me.FTContactName.Properties.Appearance.Options.UseForeColor = True
        Me.FTContactName.Properties.Appearance.Options.UseTextOptions = True
        Me.FTContactName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTContactName.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTContactName.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTContactName.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTContactName.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTContactName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTContactName.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTContactName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTContactName.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTContactName.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTContactName.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTContactName.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTContactName.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTContactName.Properties.MaxLength = 200
        Me.FTContactName.Size = New System.Drawing.Size(467, 20)
        Me.FTContactName.TabIndex = 6
        Me.FTContactName.TabStop = False
        Me.FTContactName.Tag = "2|"
        '
        'FTPINo_lbl
        '
        Me.FTPINo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPINo_lbl.Appearance.Options.UseForeColor = True
        Me.FTPINo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPINo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPINo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPINo_lbl.Location = New System.Drawing.Point(14, 5)
        Me.FTPINo_lbl.Name = "FTPINo_lbl"
        Me.FTPINo_lbl.Size = New System.Drawing.Size(121, 19)
        Me.FTPINo_lbl.TabIndex = 285
        Me.FTPINo_lbl.Tag = "2|"
        Me.FTPINo_lbl.Text = "Contact Name  :"
        '
        'FTTrackDate_lbl
        '
        Me.FTTrackDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTTrackDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTTrackDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTTrackDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTTrackDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTTrackDate_lbl.Location = New System.Drawing.Point(14, 29)
        Me.FTTrackDate_lbl.Name = "FTTrackDate_lbl"
        Me.FTTrackDate_lbl.Size = New System.Drawing.Size(116, 19)
        Me.FTTrackDate_lbl.TabIndex = 571
        Me.FTTrackDate_lbl.Tag = "2|"
        Me.FTTrackDate_lbl.Text = " Date :"
        '
        'FTTrackDate
        '
        Me.FTTrackDate.EditValue = Nothing
        Me.FTTrackDate.EnterMoveNextControl = True
        Me.FTTrackDate.Location = New System.Drawing.Point(136, 30)
        Me.FTTrackDate.Name = "FTTrackDate"
        Me.FTTrackDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTTrackDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTTrackDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTTrackDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FTTrackDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTTrackDate.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.FTTrackDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTTrackDate.Properties.NullDate = ""
        Me.FTTrackDate.Size = New System.Drawing.Size(166, 20)
        Me.FTTrackDate.TabIndex = 570
        Me.FTTrackDate.Tag = "2|"
        '
        'wPurchaseTrackingPIAddTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(961, 360)
        Me.ControlBox = False
        Me.Controls.Add(Me.FTTrackDate_lbl)
        Me.Controls.Add(Me.FTTrackDate)
        Me.Controls.Add(Me.FTPINo_lbl)
        Me.Controls.Add(Me.FTContactName)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmadd)
        Me.Controls.Add(Me.FTRemark_lbl)
        Me.Controls.Add(Me.FTRemark)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wPurchaseTrackingPIAddTracking"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Tracking"
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTContactName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTTrackDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTTrackDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FTRemark_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRemark As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTContactName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTPINo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTTrackDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTTrackDate As DevExpress.XtraEditors.DateEdit
End Class
