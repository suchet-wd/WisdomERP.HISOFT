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
        CType(Me.FTPaidNote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPaidDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPaidDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FTPaidNote_lbl
        '
        Me.FTPaidNote_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPaidNote_lbl.Appearance.Options.UseForeColor = True
        Me.FTPaidNote_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPaidNote_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPaidNote_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPaidNote_lbl.Location = New System.Drawing.Point(48, 29)
        Me.FTPaidNote_lbl.Name = "FTPaidNote_lbl"
        Me.FTPaidNote_lbl.Size = New System.Drawing.Size(87, 19)
        Me.FTPaidNote_lbl.TabIndex = 281
        Me.FTPaidNote_lbl.Tag = "2|"
        Me.FTPaidNote_lbl.Text = "Note :"
        '
        'FTPaidNote
        '
        Me.FTPaidNote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.FTPaidNote.EditValue = ""
        Me.FTPaidNote.Location = New System.Drawing.Point(137, 28)
        Me.FTPaidNote.Name = "FTPaidNote"
        Me.FTPaidNote.Properties.MaxLength = 1000
        Me.FTPaidNote.Size = New System.Drawing.Size(746, 221)
        Me.FTPaidNote.TabIndex = 12
        Me.FTPaidNote.Tag = "2|"
        '
        'ocmadd
        '
        Me.ocmadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmadd.Location = New System.Drawing.Point(137, 264)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(145, 27)
        Me.ocmadd.TabIndex = 13
        Me.ocmadd.Text = "ADD"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(661, 264)
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
        Me.FTPaidDate_lbl.Size = New System.Drawing.Size(116, 19)
        Me.FTPaidDate_lbl.TabIndex = 571
        Me.FTPaidDate_lbl.Tag = "2|"
        Me.FTPaidDate_lbl.Text = " Paid Date :"
        '
        'FTPaidDate
        '
        Me.FTPaidDate.EditValue = Nothing
        Me.FTPaidDate.EnterMoveNextControl = True
        Me.FTPaidDate.Location = New System.Drawing.Point(136, 2)
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
        'wPurchaseTrackingPIAddPaid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(961, 302)
        Me.ControlBox = False
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
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FTPaidNote_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPaidNote As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTPaidDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTPaidDate As DevExpress.XtraEditors.DateEdit
End Class
