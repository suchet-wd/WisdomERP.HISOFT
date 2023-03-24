<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPopupQCFabricBarcode
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.oBtnEnter = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.FNSize_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNSize = New DevExpress.XtraEditors.CalcEdit()
        Me.FNYardNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNYardNo = New DevExpress.XtraEditors.CalcEdit()
        Me.FNPoint_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNPoint = New DevExpress.XtraEditors.CalcEdit()
        Me.FTRemark_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRemark = New DevExpress.XtraEditors.MemoEdit()
        CType(Me.FNSize.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNYardNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNPoint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'oBtnEnter
        '
        Me.oBtnEnter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.oBtnEnter.Appearance.Font = New System.Drawing.Font("Tahoma", 20.0!, System.Drawing.FontStyle.Bold)
        Me.oBtnEnter.Appearance.Options.UseFont = True
        Me.oBtnEnter.Location = New System.Drawing.Point(147, 280)
        Me.oBtnEnter.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oBtnEnter.Name = "oBtnEnter"
        Me.oBtnEnter.Size = New System.Drawing.Size(183, 51)
        Me.oBtnEnter.TabIndex = 1
        Me.oBtnEnter.Text = "ENTER"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SimpleButton1.Appearance.Font = New System.Drawing.Font("Tahoma", 20.0!, System.Drawing.FontStyle.Bold)
        Me.SimpleButton1.Appearance.Options.UseFont = True
        Me.SimpleButton1.Location = New System.Drawing.Point(478, 280)
        Me.SimpleButton1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(183, 51)
        Me.SimpleButton1.TabIndex = 2
        Me.SimpleButton1.Text = "CANCEL"
        '
        'FNSize_lbl
        '
        Me.FNSize_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.FNSize_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNSize_lbl.Appearance.Options.UseFont = True
        Me.FNSize_lbl.Appearance.Options.UseForeColor = True
        Me.FNSize_lbl.Appearance.Options.UseTextOptions = True
        Me.FNSize_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSize_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNSize_lbl.Location = New System.Drawing.Point(336, 26)
        Me.FNSize_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNSize_lbl.Name = "FNSize_lbl"
        Me.FNSize_lbl.Size = New System.Drawing.Size(133, 22)
        Me.FNSize_lbl.TabIndex = 480
        Me.FNSize_lbl.Tag = "2|"
        Me.FNSize_lbl.Text = "ขนาด :"
        '
        'FNSize
        '
        Me.FNSize.Location = New System.Drawing.Point(485, 21)
        Me.FNSize.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNSize.Name = "FNSize"
        Me.FNSize.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.FNSize.Properties.Appearance.Options.UseFont = True
        Me.FNSize.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNSize.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNSize.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNSize.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNSize.Properties.AutoHeight = False
        Me.FNSize.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNSize.Properties.DisplayFormat.FormatString = "{0:n2}"
        Me.FNSize.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSize.Properties.EditFormat.FormatString = "{0:n2}"
        Me.FNSize.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSize.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Style3D
        Me.FNSize.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.LiveResize
        Me.FNSize.Properties.Precision = 2
        Me.FNSize.Size = New System.Drawing.Size(119, 31)
        Me.FNSize.TabIndex = 479
        '
        'FNYardNo_lbl
        '
        Me.FNYardNo_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.FNYardNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNYardNo_lbl.Appearance.Options.UseFont = True
        Me.FNYardNo_lbl.Appearance.Options.UseForeColor = True
        Me.FNYardNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FNYardNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNYardNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNYardNo_lbl.Location = New System.Drawing.Point(27, 27)
        Me.FNYardNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNYardNo_lbl.Name = "FNYardNo_lbl"
        Me.FNYardNo_lbl.Size = New System.Drawing.Size(185, 22)
        Me.FNYardNo_lbl.TabIndex = 478
        Me.FNYardNo_lbl.Tag = "2|"
        Me.FNYardNo_lbl.Text = "หลาที่ :"
        '
        'FNYardNo
        '
        Me.FNYardNo.Location = New System.Drawing.Point(228, 21)
        Me.FNYardNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNYardNo.Name = "FNYardNo"
        Me.FNYardNo.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.FNYardNo.Properties.Appearance.Options.UseFont = True
        Me.FNYardNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNYardNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNYardNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNYardNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNYardNo.Properties.AutoHeight = False
        Me.FNYardNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.FNYardNo.Properties.DisplayFormat.FormatString = "{0:n2}"
        Me.FNYardNo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNYardNo.Properties.EditFormat.FormatString = "{0:n2}"
        Me.FNYardNo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNYardNo.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Style3D
        Me.FNYardNo.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.LiveResize
        Me.FNYardNo.Properties.Precision = 2
        Me.FNYardNo.Size = New System.Drawing.Size(102, 31)
        Me.FNYardNo.TabIndex = 477
        '
        'FNPoint_lbl
        '
        Me.FNPoint_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.FNPoint_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNPoint_lbl.Appearance.Options.UseFont = True
        Me.FNPoint_lbl.Appearance.Options.UseForeColor = True
        Me.FNPoint_lbl.Appearance.Options.UseTextOptions = True
        Me.FNPoint_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNPoint_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNPoint_lbl.Location = New System.Drawing.Point(27, 65)
        Me.FNPoint_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNPoint_lbl.Name = "FNPoint_lbl"
        Me.FNPoint_lbl.Size = New System.Drawing.Size(185, 22)
        Me.FNPoint_lbl.TabIndex = 482
        Me.FNPoint_lbl.Tag = "2|"
        Me.FNPoint_lbl.Text = "คะแนน :"
        '
        'FNPoint
        '
        Me.FNPoint.Location = New System.Drawing.Point(228, 60)
        Me.FNPoint.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNPoint.Name = "FNPoint"
        Me.FNPoint.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.FNPoint.Properties.Appearance.Options.UseFont = True
        Me.FNPoint.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNPoint.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNPoint.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNPoint.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNPoint.Properties.AutoHeight = False
        Me.FNPoint.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", Nothing, Nothing, True)})
        Me.FNPoint.Properties.DisplayFormat.FormatString = "{0:n2}"
        Me.FNPoint.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPoint.Properties.EditFormat.FormatString = "{0:n2}"
        Me.FNPoint.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPoint.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Style3D
        Me.FNPoint.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.LiveResize
        Me.FNPoint.Properties.Precision = 2
        Me.FNPoint.Size = New System.Drawing.Size(102, 31)
        Me.FNPoint.TabIndex = 481
        '
        'FTRemark_lbl
        '
        Me.FTRemark_lbl.Appearance.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.FTRemark_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTRemark_lbl.Appearance.Options.UseFont = True
        Me.FTRemark_lbl.Appearance.Options.UseForeColor = True
        Me.FTRemark_lbl.Appearance.Options.UseTextOptions = True
        Me.FTRemark_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRemark_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRemark_lbl.Location = New System.Drawing.Point(12, 105)
        Me.FTRemark_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark_lbl.Name = "FTRemark_lbl"
        Me.FTRemark_lbl.Size = New System.Drawing.Size(200, 22)
        Me.FTRemark_lbl.TabIndex = 483
        Me.FTRemark_lbl.Tag = "2|"
        Me.FTRemark_lbl.Text = "กลางม้วน :"
        '
        'FTRemark
        '
        Me.FTRemark.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTRemark.EditValue = ""
        Me.FTRemark.Location = New System.Drawing.Point(228, 99)
        Me.FTRemark.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.FTRemark.Properties.Appearance.Options.UseFont = True
        Me.FTRemark.Properties.MaxLength = 500
        Me.FTRemark.Size = New System.Drawing.Size(539, 161)
        Me.FTRemark.TabIndex = 484
        Me.FTRemark.Tag = "2|"
        '
        'wPopupQCFabricBarcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(826, 335)
        Me.ControlBox = False
        Me.Controls.Add(Me.FTRemark)
        Me.Controls.Add(Me.FTRemark_lbl)
        Me.Controls.Add(Me.FNPoint_lbl)
        Me.Controls.Add(Me.FNPoint)
        Me.Controls.Add(Me.FNSize_lbl)
        Me.Controls.Add(Me.FNSize)
        Me.Controls.Add(Me.FNYardNo_lbl)
        Me.Controls.Add(Me.FNYardNo)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.oBtnEnter)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wPopupQCFabricBarcode"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "QC Fabric Defect"
        CType(Me.FNSize.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNYardNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNPoint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents oBtnEnter As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNSize_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNSize As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNYardNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNYardNo As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNPoint_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNPoint As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FTRemark_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRemark As DevExpress.XtraEditors.MemoEdit
End Class
