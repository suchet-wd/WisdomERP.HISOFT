<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAddConsul
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
        Me.ogbDesc = New DevExpress.XtraEditors.GroupControl()
        Me.ocmclose = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.FTGuid = New DevExpress.XtraEditors.MemoEdit()
        Me.FTSymptom = New DevExpress.XtraEditors.MemoEdit()
        Me.FTConsulDesc = New DevExpress.XtraEditors.MemoEdit()
        Me.FTTime = New DevExpress.XtraEditors.TimeEdit()
        Me.FTGuid_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTSymptom_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTTime_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTConsulDesc_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDDate_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.ogbDesc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbDesc.SuspendLayout()
        CType(Me.FTGuid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSymptom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTConsulDesc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbDesc
        '
        Me.ogbDesc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbDesc.Controls.Add(Me.ocmclose)
        Me.ogbDesc.Controls.Add(Me.ocmsave)
        Me.ogbDesc.Controls.Add(Me.FTGuid)
        Me.ogbDesc.Controls.Add(Me.FTSymptom)
        Me.ogbDesc.Controls.Add(Me.FTConsulDesc)
        Me.ogbDesc.Controls.Add(Me.FTTime)
        Me.ogbDesc.Controls.Add(Me.FTGuid_lbl)
        Me.ogbDesc.Controls.Add(Me.FDDate)
        Me.ogbDesc.Controls.Add(Me.FTSymptom_lbl)
        Me.ogbDesc.Controls.Add(Me.FTTime_lbl)
        Me.ogbDesc.Controls.Add(Me.FTConsulDesc_lbl)
        Me.ogbDesc.Controls.Add(Me.FDDate_lbl)
        Me.ogbDesc.Location = New System.Drawing.Point(3, 2)
        Me.ogbDesc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbDesc.Name = "ogbDesc"
        Me.ogbDesc.Size = New System.Drawing.Size(989, 244)
        Me.ogbDesc.TabIndex = 2
        Me.ogbDesc.Text = "Description"
        '
        'ocmclose
        '
        Me.ocmclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmclose.Location = New System.Drawing.Point(915, 0)
        Me.ocmclose.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclose.Name = "ocmclose"
        Me.ocmclose.Size = New System.Drawing.Size(75, 26)
        Me.ocmclose.TabIndex = 295
        Me.ocmclose.Text = "CLOSE"
        '
        'ocmsave
        '
        Me.ocmsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmsave.Location = New System.Drawing.Point(833, 0)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(75, 26)
        Me.ocmsave.TabIndex = 295
        Me.ocmsave.Text = "SAVE"
        '
        'FTGuid
        '
        Me.FTGuid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTGuid.Location = New System.Drawing.Point(139, 164)
        Me.FTGuid.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTGuid.Name = "FTGuid"
        Me.FTGuid.Size = New System.Drawing.Size(845, 75)
        Me.FTGuid.TabIndex = 5
        Me.FTGuid.Tag = "2|"
        Me.FTGuid.UseOptimizedRendering = True
        '
        'FTSymptom
        '
        Me.FTSymptom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTSymptom.Location = New System.Drawing.Point(139, 89)
        Me.FTSymptom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSymptom.Name = "FTSymptom"
        Me.FTSymptom.Size = New System.Drawing.Size(845, 68)
        Me.FTSymptom.TabIndex = 5
        Me.FTSymptom.Tag = "2|"
        Me.FTSymptom.UseOptimizedRendering = True
        '
        'FTConsulDesc
        '
        Me.FTConsulDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTConsulDesc.Location = New System.Drawing.Point(395, 28)
        Me.FTConsulDesc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTConsulDesc.Name = "FTConsulDesc"
        Me.FTConsulDesc.Size = New System.Drawing.Size(588, 53)
        Me.FTConsulDesc.TabIndex = 5
        Me.FTConsulDesc.Tag = "2|"
        Me.FTConsulDesc.UseOptimizedRendering = True
        '
        'FTTime
        '
        Me.FTTime.EditValue = New Date(2015, 5, 12, 0, 0, 0, 0)
        Me.FTTime.Location = New System.Drawing.Point(139, 57)
        Me.FTTime.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTTime.Name = "FTTime"
        Me.FTTime.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTTime.Properties.DisplayFormat.FormatString = "HH:mm:ss"
        Me.FTTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FTTime.Size = New System.Drawing.Size(125, 22)
        Me.FTTime.TabIndex = 1
        Me.FTTime.Tag = "2|"
        '
        'FTGuid_lbl
        '
        Me.FTGuid_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTGuid_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTGuid_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTGuid_lbl.Location = New System.Drawing.Point(13, 164)
        Me.FTGuid_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTGuid_lbl.Name = "FTGuid_lbl"
        Me.FTGuid_lbl.Size = New System.Drawing.Size(118, 23)
        Me.FTGuid_lbl.TabIndex = 275
        Me.FTGuid_lbl.Tag = "2|"
        Me.FTGuid_lbl.Text = "Description :"
        '
        'FDDate
        '
        Me.FDDate.EditValue = Nothing
        Me.FDDate.Location = New System.Drawing.Point(139, 30)
        Me.FDDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDDate.Name = "FDDate"
        Me.FDDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDDate.Size = New System.Drawing.Size(125, 22)
        Me.FDDate.TabIndex = 0
        Me.FDDate.Tag = "2|"
        '
        'FTSymptom_lbl
        '
        Me.FTSymptom_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTSymptom_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSymptom_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSymptom_lbl.Location = New System.Drawing.Point(17, 89)
        Me.FTSymptom_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTSymptom_lbl.Name = "FTSymptom_lbl"
        Me.FTSymptom_lbl.Size = New System.Drawing.Size(118, 23)
        Me.FTSymptom_lbl.TabIndex = 275
        Me.FTSymptom_lbl.Tag = "2|"
        Me.FTSymptom_lbl.Text = "Symptom :"
        '
        'FTTime_lbl
        '
        Me.FTTime_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTTime_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTTime_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTTime_lbl.Location = New System.Drawing.Point(13, 55)
        Me.FTTime_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTTime_lbl.Name = "FTTime_lbl"
        Me.FTTime_lbl.Size = New System.Drawing.Size(122, 23)
        Me.FTTime_lbl.TabIndex = 275
        Me.FTTime_lbl.Tag = "2|"
        Me.FTTime_lbl.Text = "Time :"
        '
        'FTConsulDesc_lbl
        '
        Me.FTConsulDesc_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTConsulDesc_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTConsulDesc_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTConsulDesc_lbl.Location = New System.Drawing.Point(269, 28)
        Me.FTConsulDesc_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTConsulDesc_lbl.Name = "FTConsulDesc_lbl"
        Me.FTConsulDesc_lbl.Size = New System.Drawing.Size(118, 23)
        Me.FTConsulDesc_lbl.TabIndex = 275
        Me.FTConsulDesc_lbl.Tag = "2|"
        Me.FTConsulDesc_lbl.Text = "Description :"
        '
        'FDDate_lbl
        '
        Me.FDDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FDDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDDate_lbl.Location = New System.Drawing.Point(13, 27)
        Me.FDDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDDate_lbl.Name = "FDDate_lbl"
        Me.FDDate_lbl.Size = New System.Drawing.Size(122, 23)
        Me.FDDate_lbl.TabIndex = 275
        Me.FDDate_lbl.Tag = "2|"
        Me.FDDate_lbl.Text = "Date :"
        '
        'wAddConsul
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(995, 247)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogbDesc)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wAddConsul"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wAddConsul"
        CType(Me.ogbDesc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbDesc.ResumeLayout(False)
        CType(Me.FTGuid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSymptom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTConsulDesc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbDesc As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTConsulDesc As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FTTime As DevExpress.XtraEditors.TimeEdit
    Friend WithEvents FDDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTTime_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTConsulDesc_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTGuid As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FTSymptom As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FTGuid_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSymptom_lbl As DevExpress.XtraEditors.LabelControl
End Class
