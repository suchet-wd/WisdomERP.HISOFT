<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wReadDataTimeCardFromFile
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmreceive = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.FTDateEnd = New DevExpress.XtraEditors.DateEdit()
        Me.FTDateEnd_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTDateStart = New DevExpress.XtraEditors.DateEdit()
        Me.FTDateStart_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ocmrepull = New DevExpress.XtraEditors.SimpleButton()
        Me.olbfilename = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTFilePaht = New DevExpress.XtraEditors.ButtonEdit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.FTDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTDateEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTDateStart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTFilePaht.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.ocmcancel)
        Me.GroupControl1.Controls.Add(Me.ocmreceive)
        Me.GroupControl1.Location = New System.Drawing.Point(1, 133)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.ShowCaption = False
        Me.GroupControl1.Size = New System.Drawing.Size(803, 52)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "GroupControl1"
        '
        'ocmcancel
        '
        Me.ocmcancel.Location = New System.Drawing.Point(472, 14)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(140, 31)
        Me.ocmcancel.TabIndex = 103
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmreceive
        '
        Me.ocmreceive.Location = New System.Drawing.Point(168, 14)
        Me.ocmreceive.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmreceive.Name = "ocmreceive"
        Me.ocmreceive.Size = New System.Drawing.Size(140, 31)
        Me.ocmreceive.TabIndex = 102
        Me.ocmreceive.TabStop = False
        Me.ocmreceive.Tag = "2|"
        Me.ocmreceive.Text = "CALCULATE"
        '
        'GroupControl2
        '
        Me.GroupControl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl2.Controls.Add(Me.FTDateEnd)
        Me.GroupControl2.Controls.Add(Me.FTDateEnd_lbl)
        Me.GroupControl2.Controls.Add(Me.FTDateStart)
        Me.GroupControl2.Controls.Add(Me.FTDateStart_lbl)
        Me.GroupControl2.Controls.Add(Me.ocmrepull)
        Me.GroupControl2.Controls.Add(Me.olbfilename)
        Me.GroupControl2.Controls.Add(Me.FNHSysCmpId_None)
        Me.GroupControl2.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.GroupControl2.Controls.Add(Me.FNHSysCmpId)
        Me.GroupControl2.Controls.Add(Me.FTFilePaht)
        Me.GroupControl2.Location = New System.Drawing.Point(1, 5)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(803, 127)
        Me.GroupControl2.TabIndex = 2
        '
        'FTDateEnd
        '
        Me.FTDateEnd.EditValue = Nothing
        Me.FTDateEnd.EnterMoveNextControl = True
        Me.FTDateEnd.Location = New System.Drawing.Point(528, 149)
        Me.FTDateEnd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTDateEnd.Name = "FTDateEnd"
        Me.FTDateEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDateEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTDateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTDateEnd.Properties.DisplayFormat.FormatString = "d"
        Me.FTDateEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTDateEnd.Properties.EditFormat.FormatString = "d"
        Me.FTDateEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTDateEnd.Properties.NullDate = ""
        Me.FTDateEnd.Size = New System.Drawing.Size(132, 22)
        Me.FTDateEnd.TabIndex = 436
        Me.FTDateEnd.Tag = "2|"
        '
        'FTDateEnd_lbl
        '
        Me.FTDateEnd_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTDateEnd_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTDateEnd_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTDateEnd_lbl.Location = New System.Drawing.Point(405, 149)
        Me.FTDateEnd_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTDateEnd_lbl.Name = "FTDateEnd_lbl"
        Me.FTDateEnd_lbl.Size = New System.Drawing.Size(122, 23)
        Me.FTDateEnd_lbl.TabIndex = 437
        Me.FTDateEnd_lbl.Tag = "2|"
        Me.FTDateEnd_lbl.Text = "To Date :"
        '
        'FTDateStart
        '
        Me.FTDateStart.EditValue = Nothing
        Me.FTDateStart.EnterMoveNextControl = True
        Me.FTDateStart.Location = New System.Drawing.Point(168, 149)
        Me.FTDateStart.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTDateStart.Name = "FTDateStart"
        Me.FTDateStart.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDateStart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTDateStart.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTDateStart.Properties.DisplayFormat.FormatString = "d"
        Me.FTDateStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTDateStart.Properties.EditFormat.FormatString = "d"
        Me.FTDateStart.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTDateStart.Properties.NullDate = ""
        Me.FTDateStart.Size = New System.Drawing.Size(132, 22)
        Me.FTDateStart.TabIndex = 434
        Me.FTDateStart.Tag = "2|"
        '
        'FTDateStart_lbl
        '
        Me.FTDateStart_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTDateStart_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTDateStart_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTDateStart_lbl.Location = New System.Drawing.Point(45, 149)
        Me.FTDateStart_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTDateStart_lbl.Name = "FTDateStart_lbl"
        Me.FTDateStart_lbl.Size = New System.Drawing.Size(122, 23)
        Me.FTDateStart_lbl.TabIndex = 435
        Me.FTDateStart_lbl.Tag = "2|"
        Me.FTDateStart_lbl.Text = "Start Date :"
        '
        'ocmrepull
        '
        Me.ocmrepull.Location = New System.Drawing.Point(168, 203)
        Me.ocmrepull.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmrepull.Name = "ocmrepull"
        Me.ocmrepull.Size = New System.Drawing.Size(492, 31)
        Me.ocmrepull.TabIndex = 433
        Me.ocmrepull.TabStop = False
        Me.ocmrepull.Tag = "2|"
        Me.ocmrepull.Text = "ดึงซ้ำ"
        '
        'olbfilename
        '
        Me.olbfilename.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.olbfilename.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.olbfilename.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbfilename.Location = New System.Drawing.Point(13, 70)
        Me.olbfilename.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.olbfilename.Name = "olbfilename"
        Me.olbfilename.Size = New System.Drawing.Size(148, 22)
        Me.olbfilename.TabIndex = 432
        Me.olbfilename.Tag = "2|"
        Me.olbfilename.Text = "File Name :"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(301, 41)
        Me.FNHSysCmpId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(359, 22)
        Me.FNHSysCmpId_None.TabIndex = 431
        Me.FNHSysCmpId_None.Tag = ""
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(19, 42)
        Me.FNHSysCmpId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(142, 22)
        Me.FNHSysCmpId_lbl.TabIndex = 430
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(168, 41)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.ReadOnly = True
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(132, 22)
        Me.FNHSysCmpId.TabIndex = 429
        Me.FNHSysCmpId.Tag = ""
        '
        'FTFilePaht
        '
        Me.FTFilePaht.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTFilePaht.EnterMoveNextControl = True
        Me.FTFilePaht.Location = New System.Drawing.Point(168, 73)
        Me.FTFilePaht.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTFilePaht.Name = "FTFilePaht"
        Me.FTFilePaht.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTFilePaht.Properties.Appearance.Options.UseBackColor = True
        Me.FTFilePaht.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTFilePaht.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePaht.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTFilePaht.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTFilePaht.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTFilePaht.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePaht.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTFilePaht.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTFilePaht.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTFilePaht.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePaht.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTFilePaht.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTFilePaht.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 35, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "", Nothing, True)})
        Me.FTFilePaht.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTFilePaht.Properties.MaxLength = 30
        Me.FTFilePaht.Properties.ReadOnly = True
        Me.FTFilePaht.Size = New System.Drawing.Size(492, 22)
        Me.FTFilePaht.TabIndex = 4
        Me.FTFilePaht.Tag = "2|"
        '
        'wReadDataTimeCardFromFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(806, 187)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.GroupControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wReadDataTimeCardFromFile"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Read Time Card"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.FTDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTDateEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTDateStart.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTDateStart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTFilePaht.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmreceive As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTFilePaht As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents olbfilename As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ocmrepull As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTDateEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTDateEnd_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTDateStart As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTDateStart_lbl As DevExpress.XtraEditors.LabelControl
End Class
