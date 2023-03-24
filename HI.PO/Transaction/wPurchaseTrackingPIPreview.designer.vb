<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wPurchaseTrackingPIPreview
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
        Me.ogbCopySubOrderHeader = New DevExpress.XtraEditors.GroupControl()
        Me.ogbCopyOrderNoConfirm = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.SFTDateTrans = New DevExpress.XtraEditors.DateEdit()
        Me.SFTDateTrans_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.EFTDateTrans = New DevExpress.XtraEditors.DateEdit()
        Me.EFTDateTrans_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.ogbCopySubOrderHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbCopySubOrderHeader.SuspendLayout()
        CType(Me.ogbCopyOrderNoConfirm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbCopyOrderNoConfirm.SuspendLayout()
        CType(Me.SFTDateTrans.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SFTDateTrans.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EFTDateTrans.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EFTDateTrans.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbCopySubOrderHeader
        '
        Me.ogbCopySubOrderHeader.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbCopySubOrderHeader.Controls.Add(Me.EFTDateTrans)
        Me.ogbCopySubOrderHeader.Controls.Add(Me.EFTDateTrans_lbl)
        Me.ogbCopySubOrderHeader.Controls.Add(Me.SFTDateTrans)
        Me.ogbCopySubOrderHeader.Controls.Add(Me.SFTDateTrans_lbl)
        Me.ogbCopySubOrderHeader.Location = New System.Drawing.Point(4, 4)
        Me.ogbCopySubOrderHeader.Name = "ogbCopySubOrderHeader"
        Me.ogbCopySubOrderHeader.ShowCaption = False
        Me.ogbCopySubOrderHeader.Size = New System.Drawing.Size(601, 73)
        Me.ogbCopySubOrderHeader.TabIndex = 0
        '
        'ogbCopyOrderNoConfirm
        '
        Me.ogbCopyOrderNoConfirm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbCopyOrderNoConfirm.Controls.Add(Me.ocmcancel)
        Me.ogbCopyOrderNoConfirm.Controls.Add(Me.ocmpreview)
        Me.ogbCopyOrderNoConfirm.Location = New System.Drawing.Point(4, 83)
        Me.ogbCopyOrderNoConfirm.Name = "ogbCopyOrderNoConfirm"
        Me.ogbCopyOrderNoConfirm.ShowCaption = False
        Me.ogbCopyOrderNoConfirm.Size = New System.Drawing.Size(601, 41)
        Me.ogbCopyOrderNoConfirm.TabIndex = 290
        Me.ogbCopyOrderNoConfirm.Text = "GroupControl2"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(355, 9)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(145, 25)
        Me.ocmcancel.TabIndex = 1
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(125, 9)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(133, 25)
        Me.ocmpreview.TabIndex = 0
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "Preview"
        '
        'SFTDateTrans
        '
        Me.SFTDateTrans.EditValue = Nothing
        Me.SFTDateTrans.EnterMoveNextControl = True
        Me.SFTDateTrans.Location = New System.Drawing.Point(138, 25)
        Me.SFTDateTrans.Name = "SFTDateTrans"
        Me.SFTDateTrans.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.SFTDateTrans.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.SFTDateTrans.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.SFTDateTrans.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.SFTDateTrans.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.SFTDateTrans.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.SFTDateTrans.Properties.NullDate = ""
        Me.SFTDateTrans.Size = New System.Drawing.Size(120, 20)
        Me.SFTDateTrans.TabIndex = 279
        Me.SFTDateTrans.Tag = "2|"
        '
        'SFTDateTrans_lbl
        '
        Me.SFTDateTrans_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.SFTDateTrans_lbl.Appearance.Options.UseForeColor = True
        Me.SFTDateTrans_lbl.Appearance.Options.UseTextOptions = True
        Me.SFTDateTrans_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.SFTDateTrans_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.SFTDateTrans_lbl.Location = New System.Drawing.Point(25, 25)
        Me.SFTDateTrans_lbl.Name = "SFTDateTrans_lbl"
        Me.SFTDateTrans_lbl.Size = New System.Drawing.Size(110, 20)
        Me.SFTDateTrans_lbl.TabIndex = 280
        Me.SFTDateTrans_lbl.Tag = "2|"
        Me.SFTDateTrans_lbl.Text = "วันที่รับเอกสาร :"
        '
        'EFTDateTrans
        '
        Me.EFTDateTrans.EditValue = Nothing
        Me.EFTDateTrans.EnterMoveNextControl = True
        Me.EFTDateTrans.Location = New System.Drawing.Point(378, 25)
        Me.EFTDateTrans.Name = "EFTDateTrans"
        Me.EFTDateTrans.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.EFTDateTrans.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.EFTDateTrans.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.EFTDateTrans.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.EFTDateTrans.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.EFTDateTrans.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.EFTDateTrans.Properties.NullDate = ""
        Me.EFTDateTrans.Size = New System.Drawing.Size(120, 20)
        Me.EFTDateTrans.TabIndex = 281
        Me.EFTDateTrans.Tag = "2|"
        '
        'EFTDateTrans_lbl
        '
        Me.EFTDateTrans_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.EFTDateTrans_lbl.Appearance.Options.UseForeColor = True
        Me.EFTDateTrans_lbl.Appearance.Options.UseTextOptions = True
        Me.EFTDateTrans_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.EFTDateTrans_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.EFTDateTrans_lbl.Location = New System.Drawing.Point(265, 25)
        Me.EFTDateTrans_lbl.Name = "EFTDateTrans_lbl"
        Me.EFTDateTrans_lbl.Size = New System.Drawing.Size(110, 20)
        Me.EFTDateTrans_lbl.TabIndex = 282
        Me.EFTDateTrans_lbl.Tag = "2|"
        Me.EFTDateTrans_lbl.Text = "ถึงวันที่ :"
        '
        'wPurchaseTrackingPIPreview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(609, 130)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogbCopyOrderNoConfirm)
        Me.Controls.Add(Me.ogbCopySubOrderHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wPurchaseTrackingPIPreview"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "P/I Payment Summary Report"
        CType(Me.ogbCopySubOrderHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbCopySubOrderHeader.ResumeLayout(False)
        CType(Me.ogbCopyOrderNoConfirm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbCopyOrderNoConfirm.ResumeLayout(False)
        CType(Me.SFTDateTrans.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SFTDateTrans.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EFTDateTrans.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EFTDateTrans.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbCopySubOrderHeader As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbCopyOrderNoConfirm As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents EFTDateTrans As DevExpress.XtraEditors.DateEdit
    Friend WithEvents EFTDateTrans_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SFTDateTrans As DevExpress.XtraEditors.DateEdit
    Friend WithEvents SFTDateTrans_lbl As DevExpress.XtraEditors.LabelControl
End Class
