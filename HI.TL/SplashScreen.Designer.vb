<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SplashScreen
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SplashScreen))
        Me.olbcaption = New DevExpress.XtraEditors.LabelControl()
        Me.labelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.opic = New DevExpress.XtraEditors.PictureEdit()
        Me.pgr1 = New DevExpress.XtraEditors.MarqueeProgressBarControl()
        Me.olbtitle = New DevExpress.XtraEditors.LabelControl()
        Me.pgr2 = New DevExpress.XtraEditors.ProgressBarControl()
        Me.FTTimeStart = New DevExpress.XtraEditors.LabelControl()
        Me.FTTime = New DevExpress.XtraEditors.LabelControl()
        CType(Me.opic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pgr1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pgr2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'olbcaption
        '
        Me.olbcaption.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.olbcaption.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.olbcaption.Appearance.Font = New System.Drawing.Font("Tahoma", 6.0!)
        Me.olbcaption.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.olbcaption.Appearance.Options.UseBackColor = True
        Me.olbcaption.Appearance.Options.UseFont = True
        Me.olbcaption.Appearance.Options.UseForeColor = True
        Me.olbcaption.Appearance.Options.UseTextOptions = True
        Me.olbcaption.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.olbcaption.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.olbcaption.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbcaption.Location = New System.Drawing.Point(16, 131)
        Me.olbcaption.Margin = New System.Windows.Forms.Padding(4)
        Me.olbcaption.Name = "olbcaption"
        Me.olbcaption.Size = New System.Drawing.Size(200, 105)
        Me.olbcaption.TabIndex = 12
        Me.olbcaption.Text = "Starting ..."
        '
        'labelControl1
        '
        Me.labelControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 4.0!, System.Drawing.FontStyle.Bold)
        Me.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.labelControl1.Appearance.Options.UseFont = True
        Me.labelControl1.Appearance.Options.UseForeColor = True
        Me.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.labelControl1.Location = New System.Drawing.Point(10, 270)
        Me.labelControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.labelControl1.Name = "labelControl1"
        Me.labelControl1.Size = New System.Drawing.Size(207, 8)
        Me.labelControl1.TabIndex = 11
        Me.labelControl1.Text = "Copyright © 2015, Wisdom ERP Co., Ltd. All rights reserved. "
        '
        'opic
        '
        Me.opic.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.opic.Cursor = System.Windows.Forms.Cursors.Default
        Me.opic.EditValue = Global.HI.TL.My.Resources.Resources.Wait
        Me.opic.Location = New System.Drawing.Point(16, 55)
        Me.opic.Margin = New System.Windows.Forms.Padding(4)
        Me.opic.Name = "opic"
        Me.opic.Properties.AllowFocused = False
        Me.opic.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.opic.Properties.Appearance.Options.UseBackColor = True
        Me.opic.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.opic.Properties.ShowMenu = False
        Me.opic.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze
        Me.opic.Properties.ZoomAccelerationFactor = 1.0R
        Me.opic.Size = New System.Drawing.Size(200, 68)
        Me.opic.TabIndex = 14
        '
        'pgr1
        '
        Me.pgr1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgr1.EditValue = 0
        Me.pgr1.Location = New System.Drawing.Point(16, 240)
        Me.pgr1.Margin = New System.Windows.Forms.Padding(4)
        Me.pgr1.Name = "pgr1"
        Me.pgr1.Size = New System.Drawing.Size(200, 15)
        Me.pgr1.TabIndex = 15
        '
        'olbtitle
        '
        Me.olbtitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.olbtitle.Appearance.Font = New System.Drawing.Font("AngsanaUPC", 8.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle))
        Me.olbtitle.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.olbtitle.Appearance.Options.UseFont = True
        Me.olbtitle.Appearance.Options.UseForeColor = True
        Me.olbtitle.Appearance.Options.UseTextOptions = True
        Me.olbtitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.olbtitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbtitle.Location = New System.Drawing.Point(13, 6)
        Me.olbtitle.Margin = New System.Windows.Forms.Padding(4)
        Me.olbtitle.Name = "olbtitle"
        Me.olbtitle.Size = New System.Drawing.Size(195, 31)
        Me.olbtitle.TabIndex = 20
        Me.olbtitle.Text = "Language"
        '
        'pgr2
        '
        Me.pgr2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgr2.Location = New System.Drawing.Point(16, 245)
        Me.pgr2.Margin = New System.Windows.Forms.Padding(4)
        Me.pgr2.Name = "pgr2"
        Me.pgr2.Size = New System.Drawing.Size(200, 15)
        Me.pgr2.TabIndex = 21
        Me.pgr2.Visible = False
        '
        'FTTimeStart
        '
        Me.FTTimeStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.FTTimeStart.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTTimeStart.Appearance.Options.UseForeColor = True
        Me.FTTimeStart.Appearance.Options.UseTextOptions = True
        Me.FTTimeStart.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTTimeStart.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
        Me.FTTimeStart.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTTimeStart.Location = New System.Drawing.Point(16, 45)
        Me.FTTimeStart.Margin = New System.Windows.Forms.Padding(4)
        Me.FTTimeStart.Name = "FTTimeStart"
        Me.FTTimeStart.Size = New System.Drawing.Size(184, 22)
        Me.FTTimeStart.TabIndex = 22
        '
        'FTTime
        '
        Me.FTTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTTime.Appearance.ForeColor = System.Drawing.Color.Lime
        Me.FTTime.Appearance.Options.UseForeColor = True
        Me.FTTime.Appearance.Options.UseTextOptions = True
        Me.FTTime.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTTime.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.FTTime.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTTime.Location = New System.Drawing.Point(72, 1)
        Me.FTTime.Margin = New System.Windows.Forms.Padding(4)
        Me.FTTime.Name = "FTTime"
        Me.FTTime.Size = New System.Drawing.Size(149, 22)
        Me.FTTime.TabIndex = 23
        '
        'SplashScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(232, 297)
        Me.ControlBox = False
        Me.Controls.Add(Me.FTTime)
        Me.Controls.Add(Me.FTTimeStart)
        Me.Controls.Add(Me.pgr2)
        Me.Controls.Add(Me.olbtitle)
        Me.Controls.Add(Me.pgr1)
        Me.Controls.Add(Me.opic)
        Me.Controls.Add(Me.olbcaption)
        Me.Controls.Add(Me.labelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "SplashScreen"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Form1"
        CType(Me.opic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pgr1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pgr2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents opic As DevExpress.XtraEditors.PictureEdit
    Private WithEvents olbcaption As DevExpress.XtraEditors.LabelControl
    Private WithEvents labelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pgr1 As DevExpress.XtraEditors.MarqueeProgressBarControl
    Friend WithEvents olbtitle As DevExpress.XtraEditors.LabelControl
    Friend WithEvents pgr2 As DevExpress.XtraEditors.ProgressBarControl
    Private WithEvents FTTimeStart As DevExpress.XtraEditors.LabelControl
    Private WithEvents FTTime As DevExpress.XtraEditors.LabelControl
End Class
