<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAvgwagePopup
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
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ocmclose = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.FNHSysUnitSectId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysUnitSectId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysUnitSectId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNIncentive = New DevExpress.XtraEditors.CalcEdit()
        Me.FTDate = New DevExpress.XtraEditors.DateEdit()
        Me.FNIncentive_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNIncentiveOT_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNIncentiveOT = New DevExpress.XtraEditors.CalcEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.FNHSysUnitSectId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysUnitSectId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNIncentive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNIncentiveOT.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.ocmclose)
        Me.GroupControl1.Controls.Add(Me.ocmsave)
        Me.GroupControl1.Controls.Add(Me.FNHSysUnitSectId)
        Me.GroupControl1.Controls.Add(Me.FNHSysUnitSectId_lbl)
        Me.GroupControl1.Controls.Add(Me.FNHSysUnitSectId_None)
        Me.GroupControl1.Controls.Add(Me.FNIncentiveOT)
        Me.GroupControl1.Controls.Add(Me.FNIncentive)
        Me.GroupControl1.Controls.Add(Me.FNIncentiveOT_lbl)
        Me.GroupControl1.Controls.Add(Me.FTDate)
        Me.GroupControl1.Controls.Add(Me.FNIncentive_lbl)
        Me.GroupControl1.Controls.Add(Me.FTDate_lbl)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.ShowCaption = False
        Me.GroupControl1.Size = New System.Drawing.Size(489, 133)
        Me.GroupControl1.TabIndex = 0
        Me.GroupControl1.Text = "GroupControl1"
        '
        'ocmclose
        '
        Me.ocmclose.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmclose.Location = New System.Drawing.Point(219, 105)
        Me.ocmclose.Name = "ocmclose"
        Me.ocmclose.Size = New System.Drawing.Size(75, 23)
        Me.ocmclose.TabIndex = 314
        Me.ocmclose.Text = "Close"
        '
        'ocmsave
        '
        Me.ocmsave.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmsave.Location = New System.Drawing.Point(138, 105)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(75, 23)
        Me.ocmsave.TabIndex = 314
        Me.ocmsave.Text = "AVG Wage"
        '
        'FNHSysUnitSectId
        '
        Me.FNHSysUnitSectId.Location = New System.Drawing.Point(123, 5)
        Me.FNHSysUnitSectId.Name = "FNHSysUnitSectId"
        Me.FNHSysUnitSectId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "57", Nothing, True)})
        Me.FNHSysUnitSectId.Properties.Tag = "57"
        Me.FNHSysUnitSectId.Size = New System.Drawing.Size(112, 20)
        Me.FNHSysUnitSectId.TabIndex = 311
        Me.FNHSysUnitSectId.Tag = "2|"
        '
        'FNHSysUnitSectId_lbl
        '
        Me.FNHSysUnitSectId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysUnitSectId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysUnitSectId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysUnitSectId_lbl.Location = New System.Drawing.Point(0, 5)
        Me.FNHSysUnitSectId_lbl.Name = "FNHSysUnitSectId_lbl"
        Me.FNHSysUnitSectId_lbl.Size = New System.Drawing.Size(122, 20)
        Me.FNHSysUnitSectId_lbl.TabIndex = 312
        Me.FNHSysUnitSectId_lbl.Tag = "2|"
        Me.FNHSysUnitSectId_lbl.Text = "Start Unit Sect :"
        '
        'FNHSysUnitSectId_None
        '
        Me.FNHSysUnitSectId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysUnitSectId_None.Location = New System.Drawing.Point(237, 5)
        Me.FNHSysUnitSectId_None.Name = "FNHSysUnitSectId_None"
        Me.FNHSysUnitSectId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysUnitSectId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysUnitSectId_None.Properties.ReadOnly = True
        Me.FNHSysUnitSectId_None.Size = New System.Drawing.Size(247, 20)
        Me.FNHSysUnitSectId_None.TabIndex = 313
        Me.FNHSysUnitSectId_None.Tag = "2|"
        '
        'FNIncentive
        '
        Me.FNIncentive.Location = New System.Drawing.Point(123, 49)
        Me.FNIncentive.Name = "FNIncentive"
        Me.FNIncentive.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNIncentive.Properties.Precision = 0
        Me.FNIncentive.Size = New System.Drawing.Size(112, 20)
        Me.FNIncentive.TabIndex = 289
        '
        'FTDate
        '
        Me.FTDate.EditValue = Nothing
        Me.FTDate.Location = New System.Drawing.Point(123, 27)
        Me.FTDate.Name = "FTDate"
        Me.FTDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTDate.Size = New System.Drawing.Size(112, 20)
        Me.FTDate.TabIndex = 282
        '
        'FNIncentive_lbl
        '
        Me.FNIncentive_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNIncentive_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNIncentive_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNIncentive_lbl.Location = New System.Drawing.Point(5, 48)
        Me.FNIncentive_lbl.Name = "FNIncentive_lbl"
        Me.FNIncentive_lbl.Size = New System.Drawing.Size(118, 19)
        Me.FNIncentive_lbl.TabIndex = 281
        Me.FNIncentive_lbl.Tag = "2|"
        Me.FNIncentive_lbl.Text = "Incentive :"
        '
        'FTDate_lbl
        '
        Me.FTDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTDate_lbl.Location = New System.Drawing.Point(0, 25)
        Me.FTDate_lbl.Name = "FTDate_lbl"
        Me.FTDate_lbl.Size = New System.Drawing.Size(123, 19)
        Me.FTDate_lbl.TabIndex = 281
        Me.FTDate_lbl.Tag = "2|"
        Me.FTDate_lbl.Text = "Date :"
        '
        'FNIncentiveOT_lbl
        '
        Me.FNIncentiveOT_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNIncentiveOT_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNIncentiveOT_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNIncentiveOT_lbl.Location = New System.Drawing.Point(5, 70)
        Me.FNIncentiveOT_lbl.Name = "FNIncentiveOT_lbl"
        Me.FNIncentiveOT_lbl.Size = New System.Drawing.Size(118, 19)
        Me.FNIncentiveOT_lbl.TabIndex = 281
        Me.FNIncentiveOT_lbl.Tag = "2|"
        Me.FNIncentiveOT_lbl.Text = "Incentive OT :"
        '
        'FNIncentiveOT
        '
        Me.FNIncentiveOT.Location = New System.Drawing.Point(123, 71)
        Me.FNIncentiveOT.Name = "FNIncentiveOT"
        Me.FNIncentiveOT.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNIncentiveOT.Properties.Precision = 0
        Me.FNIncentiveOT.Size = New System.Drawing.Size(112, 20)
        Me.FNIncentiveOT.TabIndex = 289
        '
        'LabelControl1
        '
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(242, 52)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(101, 13)
        Me.LabelControl1.TabIndex = 315
        Me.LabelControl1.Text = "VND"
        '
        'LabelControl2
        '
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Location = New System.Drawing.Point(242, 76)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(101, 13)
        Me.LabelControl2.TabIndex = 315
        Me.LabelControl2.Text = "VND"
        '
        'wAvgwagePopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(489, 133)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupControl1)
        Me.Name = "wAvgwagePopup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wAvgwagePopup"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.FNHSysUnitSectId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysUnitSectId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNIncentive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNIncentiveOT.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FNIncentive_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNIncentive As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents ocmclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysUnitSectId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysUnitSectId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysUnitSectId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNIncentiveOT As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNIncentiveOT_lbl As DevExpress.XtraEditors.LabelControl
End Class
