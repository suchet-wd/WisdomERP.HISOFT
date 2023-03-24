<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPurchaseTrackingPIAddPayment
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
        Me.FTPIPayTypeRemark_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTPIPayTypeRemark = New DevExpress.XtraEditors.MemoEdit()
        Me.ocmadd = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.FTPIPayDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTPIPayDate = New DevExpress.XtraEditors.DateEdit()
        Me.FNPIPayType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNPIPayType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTVenderAccNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTVenderAccNo = New DevExpress.XtraEditors.TextEdit()
        CType(Me.FTPIPayTypeRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPIPayDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPIPayDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNPIPayType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTVenderAccNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FTPIPayTypeRemark_lbl
        '
        Me.FTPIPayTypeRemark_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPIPayTypeRemark_lbl.Appearance.Options.UseForeColor = True
        Me.FTPIPayTypeRemark_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPIPayTypeRemark_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPIPayTypeRemark_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPIPayTypeRemark_lbl.Location = New System.Drawing.Point(44, 86)
        Me.FTPIPayTypeRemark_lbl.Name = "FTPIPayTypeRemark_lbl"
        Me.FTPIPayTypeRemark_lbl.Size = New System.Drawing.Size(87, 19)
        Me.FTPIPayTypeRemark_lbl.TabIndex = 281
        Me.FTPIPayTypeRemark_lbl.Tag = "2|"
        Me.FTPIPayTypeRemark_lbl.Text = "Note :"
        '
        'FTPIPayTypeRemark
        '
        Me.FTPIPayTypeRemark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.FTPIPayTypeRemark.EditValue = ""
        Me.FTPIPayTypeRemark.Location = New System.Drawing.Point(137, 87)
        Me.FTPIPayTypeRemark.Name = "FTPIPayTypeRemark"
        Me.FTPIPayTypeRemark.Properties.MaxLength = 1000
        Me.FTPIPayTypeRemark.Size = New System.Drawing.Size(746, 231)
        Me.FTPIPayTypeRemark.TabIndex = 12
        Me.FTPIPayTypeRemark.Tag = "2|"
        '
        'ocmadd
        '
        Me.ocmadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmadd.Location = New System.Drawing.Point(137, 333)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(145, 27)
        Me.ocmadd.TabIndex = 13
        Me.ocmadd.Text = "ADD"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(661, 333)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(145, 27)
        Me.ocmcancel.TabIndex = 14
        Me.ocmcancel.Text = "CANCEL"
        '
        'FTPIPayDate_lbl
        '
        Me.FTPIPayDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPIPayDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTPIPayDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPIPayDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPIPayDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPIPayDate_lbl.Location = New System.Drawing.Point(15, 37)
        Me.FTPIPayDate_lbl.Name = "FTPIPayDate_lbl"
        Me.FTPIPayDate_lbl.Size = New System.Drawing.Size(116, 19)
        Me.FTPIPayDate_lbl.TabIndex = 571
        Me.FTPIPayDate_lbl.Tag = "2|"
        Me.FTPIPayDate_lbl.Text = "Plan Pay Date :"
        '
        'FTPIPayDate
        '
        Me.FTPIPayDate.EditValue = Nothing
        Me.FTPIPayDate.EnterMoveNextControl = True
        Me.FTPIPayDate.Location = New System.Drawing.Point(137, 38)
        Me.FTPIPayDate.Name = "FTPIPayDate"
        Me.FTPIPayDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTPIPayDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTPIPayDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTPIPayDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FTPIPayDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTPIPayDate.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.FTPIPayDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTPIPayDate.Properties.NullDate = ""
        Me.FTPIPayDate.Size = New System.Drawing.Size(166, 20)
        Me.FTPIPayDate.TabIndex = 570
        Me.FTPIPayDate.Tag = "2|"
        '
        'FNPIPayType
        '
        Me.FNPIPayType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNPIPayType.EditValue = ""
        Me.FNPIPayType.EnterMoveNextControl = True
        Me.FNPIPayType.Location = New System.Drawing.Point(137, 11)
        Me.FNPIPayType.Name = "FNPIPayType"
        Me.FNPIPayType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNPIPayType.Properties.Appearance.Options.UseBackColor = True
        Me.FNPIPayType.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNPIPayType.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNPIPayType.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNPIPayType.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNPIPayType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNPIPayType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNPIPayType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNPIPayType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNPIPayType.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNPIPayType.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNPIPayType.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNPIPayType.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNPIPayType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNPIPayType.Properties.Tag = "FNPIPayType"
        Me.FNPIPayType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNPIPayType.Size = New System.Drawing.Size(166, 20)
        Me.FNPIPayType.TabIndex = 572
        Me.FNPIPayType.Tag = "2|"
        '
        'FNPIPayType_lbl
        '
        Me.FNPIPayType_lbl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNPIPayType_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNPIPayType_lbl.Appearance.Options.UseForeColor = True
        Me.FNPIPayType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNPIPayType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPIPayType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNPIPayType_lbl.Location = New System.Drawing.Point(33, 11)
        Me.FNPIPayType_lbl.Name = "FNPIPayType_lbl"
        Me.FNPIPayType_lbl.Size = New System.Drawing.Size(102, 19)
        Me.FNPIPayType_lbl.TabIndex = 573
        Me.FNPIPayType_lbl.Tag = "2|"
        Me.FNPIPayType_lbl.Text = "Payment Type :"
        '
        'FTVenderAccNo_lbl
        '
        Me.FTVenderAccNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTVenderAccNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTVenderAccNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTVenderAccNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTVenderAccNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTVenderAccNo_lbl.Location = New System.Drawing.Point(22, 60)
        Me.FTVenderAccNo_lbl.Name = "FTVenderAccNo_lbl"
        Me.FTVenderAccNo_lbl.Size = New System.Drawing.Size(110, 19)
        Me.FTVenderAccNo_lbl.TabIndex = 595
        Me.FTVenderAccNo_lbl.Tag = "2|"
        Me.FTVenderAccNo_lbl.Text = "Account No :"
        '
        'FTVenderAccNo
        '
        Me.FTVenderAccNo.EnterMoveNextControl = True
        Me.FTVenderAccNo.Location = New System.Drawing.Point(137, 61)
        Me.FTVenderAccNo.Name = "FTVenderAccNo"
        Me.FTVenderAccNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTVenderAccNo.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTVenderAccNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTVenderAccNo.Properties.Appearance.Options.UseForeColor = True
        Me.FTVenderAccNo.Properties.Appearance.Options.UseTextOptions = True
        Me.FTVenderAccNo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTVenderAccNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTVenderAccNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTVenderAccNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTVenderAccNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTVenderAccNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTVenderAccNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTVenderAccNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTVenderAccNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTVenderAccNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTVenderAccNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTVenderAccNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTVenderAccNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTVenderAccNo.Properties.MaxLength = 200
        Me.FTVenderAccNo.Size = New System.Drawing.Size(351, 20)
        Me.FTVenderAccNo.TabIndex = 594
        Me.FTVenderAccNo.TabStop = False
        Me.FTVenderAccNo.Tag = "2|"
        '
        'wPurchaseTrackingPIAddPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(961, 371)
        Me.ControlBox = False
        Me.Controls.Add(Me.FTVenderAccNo_lbl)
        Me.Controls.Add(Me.FTVenderAccNo)
        Me.Controls.Add(Me.FNPIPayType)
        Me.Controls.Add(Me.FNPIPayType_lbl)
        Me.Controls.Add(Me.FTPIPayDate_lbl)
        Me.Controls.Add(Me.FTPIPayDate)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmadd)
        Me.Controls.Add(Me.FTPIPayTypeRemark_lbl)
        Me.Controls.Add(Me.FTPIPayTypeRemark)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wPurchaseTrackingPIAddPayment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add PI Payment"
        CType(Me.FTPIPayTypeRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPIPayDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPIPayDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNPIPayType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTVenderAccNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FTPIPayTypeRemark_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPIPayTypeRemark As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTPIPayDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPIPayDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FNPIPayType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNPIPayType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTVenderAccNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTVenderAccNo As DevExpress.XtraEditors.TextEdit
End Class
