<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wPopUpBarcode
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SBtnOK = New DevExpress.XtraEditors.SimpleButton()
        Me.SBtnExit = New DevExpress.XtraEditors.SimpleButton()
        Me.FTCTNS = New DevExpress.XtraEditors.TextEdit()
        Me.FTCTNS_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNQtyCarton_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNQtyCarton = New DevExpress.XtraEditors.CalcEdit()
        Me.FNCartonNoBegin_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNCartonNoBegin = New DevExpress.XtraEditors.CalcEdit()
        CType(Me.FTCTNS.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNQtyCarton.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNCartonNoBegin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SBtnOK
        '
        Me.SBtnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SBtnOK.Location = New System.Drawing.Point(387, 80)
        Me.SBtnOK.Margin = New System.Windows.Forms.Padding(4)
        Me.SBtnOK.Name = "SBtnOK"
        Me.SBtnOK.Size = New System.Drawing.Size(96, 33)
        Me.SBtnOK.TabIndex = 3
        Me.SBtnOK.Text = "OK"
        '
        'SBtnExit
        '
        Me.SBtnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SBtnExit.Location = New System.Drawing.Point(491, 80)
        Me.SBtnExit.Margin = New System.Windows.Forms.Padding(4)
        Me.SBtnExit.Name = "SBtnExit"
        Me.SBtnExit.Size = New System.Drawing.Size(96, 33)
        Me.SBtnExit.TabIndex = 4
        Me.SBtnExit.Text = "Exit"
        '
        'FTCTNS
        '
        Me.FTCTNS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTCTNS.Location = New System.Drawing.Point(216, 15)
        Me.FTCTNS.Margin = New System.Windows.Forms.Padding(4)
        Me.FTCTNS.Name = "FTCTNS"
        Me.FTCTNS.Size = New System.Drawing.Size(368, 22)
        Me.FTCTNS.TabIndex = 0
        Me.FTCTNS.Tag = "2|"
        '
        'FTCTNS_lbl
        '
        Me.FTCTNS_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTCTNS_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTCTNS_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTCTNS_lbl.Location = New System.Drawing.Point(4, 15)
        Me.FTCTNS_lbl.Margin = New System.Windows.Forms.Padding(4)
        Me.FTCTNS_lbl.Name = "FTCTNS_lbl"
        Me.FTCTNS_lbl.Size = New System.Drawing.Size(204, 25)
        Me.FTCTNS_lbl.TabIndex = 6
        Me.FTCTNS_lbl.Text = "FTCTNS"
        '
        'FNQtyCarton_lbl
        '
        Me.FNQtyCarton_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNQtyCarton_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQtyCarton_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNQtyCarton_lbl.Location = New System.Drawing.Point(316, 42)
        Me.FNQtyCarton_lbl.Margin = New System.Windows.Forms.Padding(4)
        Me.FNQtyCarton_lbl.Name = "FNQtyCarton_lbl"
        Me.FNQtyCarton_lbl.Size = New System.Drawing.Size(160, 25)
        Me.FNQtyCarton_lbl.TabIndex = 6
        Me.FNQtyCarton_lbl.Text = "To"
        '
        'FNQtyCarton
        '
        Me.FNQtyCarton.Location = New System.Drawing.Point(481, 42)
        Me.FNQtyCarton.Margin = New System.Windows.Forms.Padding(4)
        Me.FNQtyCarton.Name = "FNQtyCarton"
        Me.FNQtyCarton.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNQtyCarton.Properties.Precision = 0
        Me.FNQtyCarton.Size = New System.Drawing.Size(103, 22)
        Me.FNQtyCarton.TabIndex = 7
        '
        'FNCartonNoBegin_lbl
        '
        Me.FNCartonNoBegin_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNCartonNoBegin_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNCartonNoBegin_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNCartonNoBegin_lbl.Location = New System.Drawing.Point(4, 43)
        Me.FNCartonNoBegin_lbl.Margin = New System.Windows.Forms.Padding(4)
        Me.FNCartonNoBegin_lbl.Name = "FNCartonNoBegin_lbl"
        Me.FNCartonNoBegin_lbl.Size = New System.Drawing.Size(204, 25)
        Me.FNCartonNoBegin_lbl.TabIndex = 6
        Me.FNCartonNoBegin_lbl.Text = "CartonNo Begin"
        '
        'FNCartonNoBegin
        '
        Me.FNCartonNoBegin.Location = New System.Drawing.Point(216, 42)
        Me.FNCartonNoBegin.Margin = New System.Windows.Forms.Padding(4)
        Me.FNCartonNoBegin.Name = "FNCartonNoBegin"
        Me.FNCartonNoBegin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNCartonNoBegin.Properties.Precision = 0
        Me.FNCartonNoBegin.Size = New System.Drawing.Size(92, 22)
        Me.FNCartonNoBegin.TabIndex = 6
        '
        'wPopUpBarcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(621, 123)
        Me.Controls.Add(Me.FNCartonNoBegin)
        Me.Controls.Add(Me.FNCartonNoBegin_lbl)
        Me.Controls.Add(Me.FNQtyCarton)
        Me.Controls.Add(Me.FNQtyCarton_lbl)
        Me.Controls.Add(Me.FTCTNS_lbl)
        Me.Controls.Add(Me.FTCTNS)
        Me.Controls.Add(Me.SBtnExit)
        Me.Controls.Add(Me.SBtnOK)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wPopUpBarcode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wPopUpBarcode"
        CType(Me.FTCTNS.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNQtyCarton.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNCartonNoBegin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SBtnOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SBtnExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTCTNS As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTCTNS_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNQtyCarton_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNQtyCarton As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNCartonNoBegin_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNCartonNoBegin As DevExpress.XtraEditors.CalcEdit
End Class
