<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPurchaseTrackingPIAddPaid
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
        Me.FTPaidNote_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTPaidNote = New DevExpress.XtraEditors.MemoEdit()
        Me.ocmadd = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.FTPaidDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTPaidDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTFilePath = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTFilePath_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTPDFName = New DevExpress.XtraEditors.TextEdit()
        Me.FTLCNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTLCNo = New DevExpress.XtraEditors.TextEdit()
        CType(Me.FTPaidNote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPaidDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPaidDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPDFName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTLCNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FTPaidNote_lbl
        '
        Me.FTPaidNote_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPaidNote_lbl.Appearance.Options.UseForeColor = True
        Me.FTPaidNote_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPaidNote_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPaidNote_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPaidNote_lbl.Location = New System.Drawing.Point(45, 72)
        Me.FTPaidNote_lbl.Name = "FTPaidNote_lbl"
        Me.FTPaidNote_lbl.Size = New System.Drawing.Size(111, 19)
        Me.FTPaidNote_lbl.TabIndex = 281
        Me.FTPaidNote_lbl.Tag = "2|"
        Me.FTPaidNote_lbl.Text = "Note :"
        '
        'FTPaidNote
        '
        Me.FTPaidNote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.FTPaidNote.EditValue = ""
        Me.FTPaidNote.Location = New System.Drawing.Point(161, 72)
        Me.FTPaidNote.Name = "FTPaidNote"
        Me.FTPaidNote.Properties.MaxLength = 1000
        Me.FTPaidNote.Size = New System.Drawing.Size(722, 156)
        Me.FTPaidNote.TabIndex = 12
        Me.FTPaidNote.Tag = "2|"
        '
        'ocmadd
        '
        Me.ocmadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmadd.Location = New System.Drawing.Point(235, 250)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(145, 27)
        Me.ocmadd.TabIndex = 13
        Me.ocmadd.Text = "ADD"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(597, 250)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(145, 27)
        Me.ocmcancel.TabIndex = 14
        Me.ocmcancel.Text = "CANCEL"
        '
        'FTPaidDate_lbl
        '
        Me.FTPaidDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPaidDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTPaidDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPaidDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPaidDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPaidDate_lbl.Location = New System.Drawing.Point(14, 1)
        Me.FTPaidDate_lbl.Name = "FTPaidDate_lbl"
        Me.FTPaidDate_lbl.Size = New System.Drawing.Size(141, 19)
        Me.FTPaidDate_lbl.TabIndex = 571
        Me.FTPaidDate_lbl.Tag = "2|"
        Me.FTPaidDate_lbl.Text = " Paid Date :"
        '
        'FTPaidDate
        '
        Me.FTPaidDate.EditValue = Nothing
        Me.FTPaidDate.EnterMoveNextControl = True
        Me.FTPaidDate.Location = New System.Drawing.Point(161, 2)
        Me.FTPaidDate.Name = "FTPaidDate"
        Me.FTPaidDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTPaidDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTPaidDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTPaidDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FTPaidDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTPaidDate.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.FTPaidDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTPaidDate.Properties.NullDate = ""
        Me.FTPaidDate.Size = New System.Drawing.Size(166, 20)
        Me.FTPaidDate.TabIndex = 570
        Me.FTPaidDate.Tag = "2|"
        '
        'FTFilePath
        '
        Me.FTFilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTFilePath.Location = New System.Drawing.Point(161, 48)
        Me.FTFilePath.Name = "FTFilePath"
        Me.FTFilePath.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTFilePath.Properties.Appearance.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTFilePath.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTFilePath.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTFilePath.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTFilePath.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTFilePath.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTFilePath.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTFilePath.Properties.ReadOnly = True
        Me.FTFilePath.Size = New System.Drawing.Size(722, 20)
        Me.FTFilePath.TabIndex = 572
        Me.FTFilePath.Tag = "2|"
        '
        'FTFilePath_lbl
        '
        Me.FTFilePath_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTFilePath_lbl.Appearance.Options.UseForeColor = True
        Me.FTFilePath_lbl.Appearance.Options.UseTextOptions = True
        Me.FTFilePath_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTFilePath_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTFilePath_lbl.Location = New System.Drawing.Point(16, 48)
        Me.FTFilePath_lbl.Name = "FTFilePath_lbl"
        Me.FTFilePath_lbl.Size = New System.Drawing.Size(141, 19)
        Me.FTFilePath_lbl.TabIndex = 573
        Me.FTFilePath_lbl.Tag = "2|"
        Me.FTFilePath_lbl.Text = "Attach File Payment (PDF) :"
        '
        'FTPDFName
        '
        Me.FTPDFName.EnterMoveNextControl = True
        Me.FTPDFName.Location = New System.Drawing.Point(391, 2)
        Me.FTPDFName.Name = "FTPDFName"
        Me.FTPDFName.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTPDFName.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTPDFName.Properties.Appearance.Options.UseBackColor = True
        Me.FTPDFName.Properties.Appearance.Options.UseForeColor = True
        Me.FTPDFName.Properties.Appearance.Options.UseTextOptions = True
        Me.FTPDFName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTPDFName.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTPDFName.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTPDFName.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTPDFName.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTPDFName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTPDFName.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTPDFName.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTPDFName.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTPDFName.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTPDFName.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTPDFName.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTPDFName.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTPDFName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTPDFName.Properties.MaxLength = 50
        Me.FTPDFName.Size = New System.Drawing.Size(213, 20)
        Me.FTPDFName.TabIndex = 595
        Me.FTPDFName.TabStop = False
        Me.FTPDFName.Tag = "2|"
        Me.FTPDFName.Visible = False
        '
        'FTLCNo_lbl
        '
        Me.FTLCNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTLCNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTLCNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTLCNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTLCNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTLCNo_lbl.Location = New System.Drawing.Point(37, 25)
        Me.FTLCNo_lbl.Name = "FTLCNo_lbl"
        Me.FTLCNo_lbl.Size = New System.Drawing.Size(121, 19)
        Me.FTLCNo_lbl.TabIndex = 597
        Me.FTLCNo_lbl.Tag = "2|"
        Me.FTLCNo_lbl.Text = "LC No.  :"
        '
        'FTLCNo
        '
        Me.FTLCNo.EnterMoveNextControl = True
        Me.FTLCNo.Location = New System.Drawing.Point(161, 25)
        Me.FTLCNo.Name = "FTLCNo"
        Me.FTLCNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTLCNo.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTLCNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTLCNo.Properties.Appearance.Options.UseForeColor = True
        Me.FTLCNo.Properties.Appearance.Options.UseTextOptions = True
        Me.FTLCNo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTLCNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTLCNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTLCNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTLCNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTLCNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTLCNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTLCNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTLCNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTLCNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTLCNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTLCNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTLCNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTLCNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTLCNo.Properties.MaxLength = 50
        Me.FTLCNo.Size = New System.Drawing.Size(213, 20)
        Me.FTLCNo.TabIndex = 596
        Me.FTLCNo.TabStop = False
        Me.FTLCNo.Tag = "2|"
        '
        'wPurchaseTrackingPIAddPaid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(961, 288)
        Me.ControlBox = False
        Me.Controls.Add(Me.FTLCNo_lbl)
        Me.Controls.Add(Me.FTLCNo)
        Me.Controls.Add(Me.FTPDFName)
        Me.Controls.Add(Me.FTFilePath_lbl)
        Me.Controls.Add(Me.FTFilePath)
        Me.Controls.Add(Me.FTPaidDate_lbl)
        Me.Controls.Add(Me.FTPaidDate)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmadd)
        Me.Controls.Add(Me.FTPaidNote_lbl)
        Me.Controls.Add(Me.FTPaidNote)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wPurchaseTrackingPIAddPaid"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Paid"
        CType(Me.FTPaidNote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPaidDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPaidDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPDFName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTLCNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FTPaidNote_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPaidNote As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTPaidDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPaidDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTFilePath As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTFilePath_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPDFName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTLCNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTLCNo As DevExpress.XtraEditors.TextEdit
End Class
