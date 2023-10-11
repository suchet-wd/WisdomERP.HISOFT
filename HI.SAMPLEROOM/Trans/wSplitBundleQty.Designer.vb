<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wSplitBundleQty
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcopy = New DevExpress.XtraEditors.SimpleButton()
        Me.ogborderprod = New DevExpress.XtraEditors.GroupControl()
        Me.oTxtValue = New DevExpress.XtraEditors.CalcEdit()
        CType(Me.ogborderprod, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogborderprod.SuspendLayout()
        CType(Me.oTxtValue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(309, 121)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(141, 31)
        Me.ocmcancel.TabIndex = 312
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmcopy
        '
        Me.ocmcopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcopy.Location = New System.Drawing.Point(119, 121)
        Me.ocmcopy.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcopy.Name = "ocmcopy"
        Me.ocmcopy.Size = New System.Drawing.Size(138, 31)
        Me.ocmcopy.TabIndex = 311
        Me.ocmcopy.TabStop = False
        Me.ocmcopy.Tag = "2|"
        Me.ocmcopy.Text = "Save"
        '
        'ogborderprod
        '
        Me.ogborderprod.Controls.Add(Me.oTxtValue)
        Me.ogborderprod.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogborderprod.Location = New System.Drawing.Point(0, 0)
        Me.ogborderprod.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogborderprod.Name = "ogborderprod"
        Me.ogborderprod.Size = New System.Drawing.Size(484, 104)
        Me.ogborderprod.TabIndex = 313
        Me.ogborderprod.Text = "Split Bundle Qty"
        '
        'oTxtValue
        '
        Me.oTxtValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.oTxtValue.EditValue = "0"
        Me.oTxtValue.Location = New System.Drawing.Point(17, 39)
        Me.oTxtValue.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oTxtValue.Name = "oTxtValue"
        Me.oTxtValue.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 20.0!, System.Drawing.FontStyle.Bold)
        Me.oTxtValue.Properties.Appearance.Options.UseFont = True
        Me.oTxtValue.Properties.AutoHeight = False
        Me.oTxtValue.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", Nothing, Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.oTxtValue.Properties.DisplayFormat.FormatString = "{0:n0}"
        Me.oTxtValue.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.oTxtValue.Properties.EditFormat.FormatString = "{0:n0}"
        Me.oTxtValue.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.oTxtValue.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.oTxtValue.Properties.Precision = 0
        Me.oTxtValue.Properties.ReadOnly = True
        Me.oTxtValue.Size = New System.Drawing.Size(433, 50)
        Me.oTxtValue.TabIndex = 1
        '
        'wSplitBundleQty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 168)
        Me.Controls.Add(Me.ogborderprod)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmcopy)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wSplitBundleQty"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wSplitBundleQty"
        CType(Me.ogborderprod, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogborderprod.ResumeLayout(False)
        CType(Me.oTxtValue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcopy As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogborderprod As DevExpress.XtraEditors.GroupControl
    Friend WithEvents oTxtValue As DevExpress.XtraEditors.CalcEdit
End Class
