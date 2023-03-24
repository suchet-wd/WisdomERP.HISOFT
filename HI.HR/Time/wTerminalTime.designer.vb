<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wTerminalTime
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
        Me.components = New System.ComponentModel.Container()
        Dim QrCodeGenerator3 As DevExpress.XtraPrinting.BarCode.QRCodeGenerator = New DevExpress.XtraPrinting.BarCode.QRCodeGenerator()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.BarCodeControl1 = New DevExpress.XtraEditors.BarCodeControl()
        Me.ogbemployee = New DevExpress.XtraEditors.GroupControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.textinput = New DevExpress.XtraEditors.TextEdit()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ogbemployee, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbemployee.SuspendLayout()
        CType(Me.textinput.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.BarCodeControl1)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1421, 671)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "Time"
        '
        'BarCodeControl1
        '
        Me.BarCodeControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.BarCodeControl1.Appearance.Options.UseFont = True
        Me.BarCodeControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BarCodeControl1.HorizontalAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BarCodeControl1.HorizontalTextAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BarCodeControl1.Location = New System.Drawing.Point(2, 28)
        Me.BarCodeControl1.Module = 20.0R
        Me.BarCodeControl1.Name = "BarCodeControl1"
        Me.BarCodeControl1.Padding = New System.Windows.Forms.Padding(10, 2, 10, 0)
        Me.BarCodeControl1.ShowText = False
        Me.BarCodeControl1.Size = New System.Drawing.Size(1417, 641)
        QrCodeGenerator3.Version = DevExpress.XtraPrinting.BarCode.QRCodeVersion.Version3
        Me.BarCodeControl1.Symbology = QrCodeGenerator3
        Me.BarCodeControl1.TabIndex = 0
        Me.BarCodeControl1.Text = "20220324113601SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS"
        Me.BarCodeControl1.VerticalAlignment = DevExpress.Utils.VertAlignment.Center
        Me.BarCodeControl1.VerticalTextAlignment = DevExpress.Utils.VertAlignment.Center
        '
        'ogbemployee
        '
        Me.ogbemployee.AppearanceCaption.Options.UseTextOptions = True
        Me.ogbemployee.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogbemployee.Controls.Add(Me.LabelControl1)
        Me.ogbemployee.Controls.Add(Me.textinput)
        Me.ogbemployee.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ogbemployee.Location = New System.Drawing.Point(0, 671)
        Me.ogbemployee.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbemployee.Name = "ogbemployee"
        Me.ogbemployee.ShowCaption = False
        Me.ogbemployee.Size = New System.Drawing.Size(1421, 155)
        Me.ogbemployee.TabIndex = 0
        Me.ogbemployee.Text = "Change Position History"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 60.0!, System.Drawing.FontStyle.Bold)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.LabelControl1.Location = New System.Drawing.Point(2, 2)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(1417, 203)
        Me.LabelControl1.TabIndex = 458
        Me.LabelControl1.Tag = "2|"
        Me.LabelControl1.Text = "08:00:00"
        '
        'textinput
        '
        Me.textinput.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textinput.EditValue = ""
        Me.textinput.EnterMoveNextControl = True
        Me.textinput.Location = New System.Drawing.Point(1255, 156)
        Me.textinput.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.textinput.Name = "textinput"
        Me.textinput.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 2.0!, System.Drawing.FontStyle.Bold)
        Me.textinput.Properties.Appearance.Options.UseFont = True
        Me.textinput.Size = New System.Drawing.Size(165, 22)
        Me.textinput.TabIndex = 0
        Me.textinput.Tag = ""
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'wTerminalTime
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1421, 826)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.ogbemployee)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wTerminalTime"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Time"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ogbemployee, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbemployee.ResumeLayout(False)
        CType(Me.textinput.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbemployee As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents textinput As DevExpress.XtraEditors.TextEdit
    Friend WithEvents BarCodeControl1 As DevExpress.XtraEditors.BarCodeControl
End Class
