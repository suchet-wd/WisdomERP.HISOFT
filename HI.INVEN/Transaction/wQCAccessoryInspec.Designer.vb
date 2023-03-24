<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wQCAccessoryInspec
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wQCAccessoryInspec))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogrpSubmenu = New DevExpress.XtraEditors.GroupControl()
        Me.TileControl = New DevExpress.XtraEditors.TileControl()
        Me.ocmclose = New DevExpress.XtraEditors.SimpleButton()
        Me.FNActQuantity_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNActQuantity = New DevExpress.XtraEditors.CalcEdit()
        Me.FNQuantity_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNQuantity = New DevExpress.XtraEditors.CalcEdit()
        Me.ocmqc = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmreject = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpass = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogrpSubmenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpSubmenu.SuspendLayout()
        CType(Me.FNActQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNQuantity.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogrpSubmenu
        '
        Me.ogrpSubmenu.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogrpSubmenu.Controls.Add(Me.TileControl)
        Me.ogrpSubmenu.Location = New System.Drawing.Point(0, 89)
        Me.ogrpSubmenu.Name = "ogrpSubmenu"
        Me.ogrpSubmenu.ShowCaption = False
        Me.ogrpSubmenu.Size = New System.Drawing.Size(1243, 492)
        Me.ogrpSubmenu.TabIndex = 3
        Me.ogrpSubmenu.Text = "Sub Menu"
        '
        'TileControl
        '
        Me.TileControl.BackColor = System.Drawing.Color.DarkGray
        Me.TileControl.Cursor = System.Windows.Forms.Cursors.Default
        Me.TileControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TileControl.DragSize = New System.Drawing.Size(0, 0)
        Me.TileControl.HorizontalContentAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.TileControl.ItemSize = 70
        Me.TileControl.Location = New System.Drawing.Point(2, 2)
        Me.TileControl.MaxId = 25
        Me.TileControl.Name = "TileControl"
        Me.TileControl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TileControl.RowCount = 6
        Me.TileControl.ScrollMode = DevExpress.XtraEditors.TileControlScrollMode.ScrollBar
        Me.TileControl.ShowGroupText = True
        Me.TileControl.Size = New System.Drawing.Size(1239, 488)
        Me.TileControl.TabIndex = 0
        Me.TileControl.Text = "TileControlSubmenu"
        Me.TileControl.VerticalContentAlignment = DevExpress.Utils.VertAlignment.Top
        '
        'ocmclose
        '
        Me.ocmclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmclose.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ocmclose.Appearance.BorderColor = System.Drawing.Color.Black
        Me.ocmclose.Appearance.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.ocmclose.Appearance.Options.UseBackColor = True
        Me.ocmclose.Appearance.Options.UseBorderColor = True
        Me.ocmclose.Appearance.Options.UseFont = True
        Me.ocmclose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat
        Me.ocmclose.Image = CType(resources.GetObject("ocmclose.Image"), System.Drawing.Image)
        Me.ocmclose.Location = New System.Drawing.Point(1094, 18)
        Me.ocmclose.Name = "ocmclose"
        Me.ocmclose.Size = New System.Drawing.Size(133, 54)
        Me.ocmclose.TabIndex = 97
        Me.ocmclose.TabStop = False
        Me.ocmclose.Tag = "2|"
        Me.ocmclose.Text = "CLOSE"
        '
        'FNActQuantity_lbl
        '
        Me.FNActQuantity_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.FNActQuantity_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNActQuantity_lbl.Appearance.Options.UseFont = True
        Me.FNActQuantity_lbl.Appearance.Options.UseForeColor = True
        Me.FNActQuantity_lbl.Appearance.Options.UseTextOptions = True
        Me.FNActQuantity_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNActQuantity_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNActQuantity_lbl.Location = New System.Drawing.Point(13, 54)
        Me.FNActQuantity_lbl.Name = "FNActQuantity_lbl"
        Me.FNActQuantity_lbl.Size = New System.Drawing.Size(148, 18)
        Me.FNActQuantity_lbl.TabIndex = 460
        Me.FNActQuantity_lbl.Tag = "2|"
        Me.FNActQuantity_lbl.Text = "Actual  :"
        '
        'FNActQuantity
        '
        Me.FNActQuantity.Location = New System.Drawing.Point(166, 47)
        Me.FNActQuantity.Name = "FNActQuantity"
        Me.FNActQuantity.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.FNActQuantity.Properties.Appearance.Options.UseFont = True
        Me.FNActQuantity.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNActQuantity.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNActQuantity.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNActQuantity.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNActQuantity.Properties.AutoHeight = False
        Me.FNActQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNActQuantity.Properties.DisplayFormat.FormatString = "{0:n2}"
        Me.FNActQuantity.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNActQuantity.Properties.EditFormat.FormatString = "{0:n2}"
        Me.FNActQuantity.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNActQuantity.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Style3D
        Me.FNActQuantity.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.LiveResize
        Me.FNActQuantity.Properties.Precision = 2
        Me.FNActQuantity.Size = New System.Drawing.Size(149, 31)
        Me.FNActQuantity.TabIndex = 459
        '
        'FNQuantity_lbl
        '
        Me.FNQuantity_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.FNQuantity_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNQuantity_lbl.Appearance.Options.UseFont = True
        Me.FNQuantity_lbl.Appearance.Options.UseForeColor = True
        Me.FNQuantity_lbl.Appearance.Options.UseTextOptions = True
        Me.FNQuantity_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNQuantity_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNQuantity_lbl.Location = New System.Drawing.Point(15, 18)
        Me.FNQuantity_lbl.Name = "FNQuantity_lbl"
        Me.FNQuantity_lbl.Size = New System.Drawing.Size(149, 18)
        Me.FNQuantity_lbl.TabIndex = 462
        Me.FNQuantity_lbl.Tag = "2|"
        Me.FNQuantity_lbl.Text = "Ticketed  :"
        '
        'FNQuantity
        '
        Me.FNQuantity.Location = New System.Drawing.Point(166, 15)
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.FNQuantity.Properties.Appearance.Options.UseFont = True
        Me.FNQuantity.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNQuantity.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNQuantity.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNQuantity.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNQuantity.Properties.AutoHeight = False
        Me.FNQuantity.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.FNQuantity.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Style3D
        Me.FNQuantity.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.LiveResize
        Me.FNQuantity.Properties.Precision = 0
        Me.FNQuantity.Properties.ReadOnly = True
        Me.FNQuantity.Size = New System.Drawing.Size(106, 31)
        Me.FNQuantity.TabIndex = 461
        '
        'ocmqc
        '
        Me.ocmqc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmqc.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ocmqc.Appearance.BorderColor = System.Drawing.Color.Black
        Me.ocmqc.Appearance.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Bold)
        Me.ocmqc.Appearance.Options.UseBackColor = True
        Me.ocmqc.Appearance.Options.UseBorderColor = True
        Me.ocmqc.Appearance.Options.UseFont = True
        Me.ocmqc.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat
        Me.ocmqc.Location = New System.Drawing.Point(677, 18)
        Me.ocmqc.Name = "ocmqc"
        Me.ocmqc.Size = New System.Drawing.Size(133, 54)
        Me.ocmqc.TabIndex = 479
        Me.ocmqc.TabStop = False
        Me.ocmqc.Tag = "2|"
        Me.ocmqc.Text = "QC"
        '
        'ocmreject
        '
        Me.ocmreject.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmreject.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ocmreject.Appearance.BorderColor = System.Drawing.Color.Black
        Me.ocmreject.Appearance.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Bold)
        Me.ocmreject.Appearance.Options.UseBackColor = True
        Me.ocmreject.Appearance.Options.UseBorderColor = True
        Me.ocmreject.Appearance.Options.UseFont = True
        Me.ocmreject.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat
        Me.ocmreject.Image = CType(resources.GetObject("ocmreject.Image"), System.Drawing.Image)
        Me.ocmreject.Location = New System.Drawing.Point(956, 18)
        Me.ocmreject.Name = "ocmreject"
        Me.ocmreject.Size = New System.Drawing.Size(133, 54)
        Me.ocmreject.TabIndex = 480
        Me.ocmreject.TabStop = False
        Me.ocmreject.Tag = "2|"
        Me.ocmreject.Text = "REJECT"
        '
        'ocmpass
        '
        Me.ocmpass.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmpass.Appearance.BackColor = System.Drawing.Color.Lime
        Me.ocmpass.Appearance.BorderColor = System.Drawing.Color.Black
        Me.ocmpass.Appearance.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Bold)
        Me.ocmpass.Appearance.Options.UseBackColor = True
        Me.ocmpass.Appearance.Options.UseBorderColor = True
        Me.ocmpass.Appearance.Options.UseFont = True
        Me.ocmpass.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat
        Me.ocmpass.Image = CType(resources.GetObject("ocmpass.Image"), System.Drawing.Image)
        Me.ocmpass.Location = New System.Drawing.Point(815, 18)
        Me.ocmpass.Name = "ocmpass"
        Me.ocmpass.Size = New System.Drawing.Size(133, 54)
        Me.ocmpass.TabIndex = 481
        Me.ocmpass.TabStop = False
        Me.ocmpass.Tag = "2|"
        Me.ocmpass.Text = "PASS"
        '
        'wQCAccessoryInspec
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1246, 584)
        Me.Controls.Add(Me.ocmpass)
        Me.Controls.Add(Me.ocmreject)
        Me.Controls.Add(Me.ocmqc)
        Me.Controls.Add(Me.FNQuantity_lbl)
        Me.Controls.Add(Me.FNQuantity)
        Me.Controls.Add(Me.FNActQuantity_lbl)
        Me.Controls.Add(Me.FNActQuantity)
        Me.Controls.Add(Me.ocmclose)
        Me.Controls.Add(Me.ogrpSubmenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wQCAccessoryInspec"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "QC Acc Detail"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.ogrpSubmenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpSubmenu.ResumeLayout(False)
        CType(Me.FNActQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNQuantity.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogrpSubmenu As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TileControl As DevExpress.XtraEditors.TileControl
    Friend WithEvents ocmclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNActQuantity_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNActQuantity As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNQuantity_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNQuantity As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents ocmqc As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmreject As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmpass As DevExpress.XtraEditors.SimpleButton
End Class
