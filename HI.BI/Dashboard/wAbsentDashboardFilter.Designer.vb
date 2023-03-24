<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAbsentDashboardFilter
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
        Me.ogbProductionPerformanceChart = New DevExpress.XtraEditors.GroupControl()
        Me.FNProductionPerformanceChart = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ogbDefectAndDowntimeChart = New DevExpress.XtraEditors.GroupControl()
        Me.FNDefectAndDowntimeChart = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ogbDefectIssue = New DevExpress.XtraEditors.GroupControl()
        Me.FNDefectIssue = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ogbcommand = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcencel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbProductionPerformanceChart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbProductionPerformanceChart.SuspendLayout()
        CType(Me.FNProductionPerformanceChart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbDefectAndDowntimeChart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbDefectAndDowntimeChart.SuspendLayout()
        CType(Me.FNDefectAndDowntimeChart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbDefectIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbDefectIssue.SuspendLayout()
        CType(Me.FNDefectIssue.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbcommand, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbcommand.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbProductionPerformanceChart
        '
        Me.ogbProductionPerformanceChart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbProductionPerformanceChart.Controls.Add(Me.FNProductionPerformanceChart)
        Me.ogbProductionPerformanceChart.Location = New System.Drawing.Point(5, 2)
        Me.ogbProductionPerformanceChart.Name = "ogbProductionPerformanceChart"
        Me.ogbProductionPerformanceChart.Size = New System.Drawing.Size(344, 88)
        Me.ogbProductionPerformanceChart.TabIndex = 0
        Me.ogbProductionPerformanceChart.Text = "Production Performance Chart"
        '
        'FNProductionPerformanceChart
        '
        Me.FNProductionPerformanceChart.EditValue = ""
        Me.FNProductionPerformanceChart.EnterMoveNextControl = True
        Me.FNProductionPerformanceChart.Location = New System.Drawing.Point(48, 43)
        Me.FNProductionPerformanceChart.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNProductionPerformanceChart.Name = "FNProductionPerformanceChart"
        Me.FNProductionPerformanceChart.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNProductionPerformanceChart.Properties.Appearance.Options.UseBackColor = True
        Me.FNProductionPerformanceChart.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNProductionPerformanceChart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNProductionPerformanceChart.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNProductionPerformanceChart.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNProductionPerformanceChart.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNProductionPerformanceChart.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNProductionPerformanceChart.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNProductionPerformanceChart.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNProductionPerformanceChart.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNProductionPerformanceChart.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNProductionPerformanceChart.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNProductionPerformanceChart.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNProductionPerformanceChart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNProductionPerformanceChart.Properties.Tag = "FNDashBoardProdType"
        Me.FNProductionPerformanceChart.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNProductionPerformanceChart.Size = New System.Drawing.Size(252, 23)
        Me.FNProductionPerformanceChart.TabIndex = 254
        Me.FNProductionPerformanceChart.Tag = "2|"
        '
        'ogbDefectAndDowntimeChart
        '
        Me.ogbDefectAndDowntimeChart.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbDefectAndDowntimeChart.Controls.Add(Me.FNDefectAndDowntimeChart)
        Me.ogbDefectAndDowntimeChart.Location = New System.Drawing.Point(5, 95)
        Me.ogbDefectAndDowntimeChart.Name = "ogbDefectAndDowntimeChart"
        Me.ogbDefectAndDowntimeChart.Size = New System.Drawing.Size(344, 85)
        Me.ogbDefectAndDowntimeChart.TabIndex = 1
        Me.ogbDefectAndDowntimeChart.Text = "Defect And Downtime Chart"
        '
        'FNDefectAndDowntimeChart
        '
        Me.FNDefectAndDowntimeChart.EditValue = ""
        Me.FNDefectAndDowntimeChart.EnterMoveNextControl = True
        Me.FNDefectAndDowntimeChart.Location = New System.Drawing.Point(48, 37)
        Me.FNDefectAndDowntimeChart.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNDefectAndDowntimeChart.Name = "FNDefectAndDowntimeChart"
        Me.FNDefectAndDowntimeChart.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNDefectAndDowntimeChart.Properties.Appearance.Options.UseBackColor = True
        Me.FNDefectAndDowntimeChart.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNDefectAndDowntimeChart.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNDefectAndDowntimeChart.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNDefectAndDowntimeChart.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNDefectAndDowntimeChart.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNDefectAndDowntimeChart.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNDefectAndDowntimeChart.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNDefectAndDowntimeChart.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNDefectAndDowntimeChart.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNDefectAndDowntimeChart.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNDefectAndDowntimeChart.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNDefectAndDowntimeChart.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNDefectAndDowntimeChart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNDefectAndDowntimeChart.Properties.Tag = "FNDashBoardProdType"
        Me.FNDefectAndDowntimeChart.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNDefectAndDowntimeChart.Size = New System.Drawing.Size(252, 23)
        Me.FNDefectAndDowntimeChart.TabIndex = 255
        Me.FNDefectAndDowntimeChart.Tag = "2|"
        '
        'ogbDefectIssue
        '
        Me.ogbDefectIssue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbDefectIssue.Controls.Add(Me.FNDefectIssue)
        Me.ogbDefectIssue.Location = New System.Drawing.Point(5, 186)
        Me.ogbDefectIssue.Name = "ogbDefectIssue"
        Me.ogbDefectIssue.Size = New System.Drawing.Size(344, 82)
        Me.ogbDefectIssue.TabIndex = 2
        Me.ogbDefectIssue.Text = "Defect Issue"
        '
        'FNDefectIssue
        '
        Me.FNDefectIssue.EditValue = ""
        Me.FNDefectIssue.EnterMoveNextControl = True
        Me.FNDefectIssue.Location = New System.Drawing.Point(47, 40)
        Me.FNDefectIssue.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNDefectIssue.Name = "FNDefectIssue"
        Me.FNDefectIssue.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNDefectIssue.Properties.Appearance.Options.UseBackColor = True
        Me.FNDefectIssue.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNDefectIssue.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNDefectIssue.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNDefectIssue.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNDefectIssue.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNDefectIssue.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNDefectIssue.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNDefectIssue.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNDefectIssue.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNDefectIssue.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNDefectIssue.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNDefectIssue.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNDefectIssue.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNDefectIssue.Properties.Tag = "FNDashBoardProdDefectType"
        Me.FNDefectIssue.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNDefectIssue.Size = New System.Drawing.Size(252, 23)
        Me.FNDefectIssue.TabIndex = 256
        Me.FNDefectIssue.Tag = "2|"
        '
        'ogbcommand
        '
        Me.ogbcommand.Controls.Add(Me.ocmcencel)
        Me.ogbcommand.Controls.Add(Me.ocmok)
        Me.ogbcommand.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ogbcommand.Location = New System.Drawing.Point(0, 274)
        Me.ogbcommand.Name = "ogbcommand"
        Me.ogbcommand.ShowCaption = False
        Me.ogbcommand.Size = New System.Drawing.Size(356, 50)
        Me.ogbcommand.TabIndex = 3
        Me.ogbcommand.Text = "Defect Issue"
        '
        'ocmcencel
        '
        Me.ocmcencel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcencel.Location = New System.Drawing.Point(188, 12)
        Me.ocmcencel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcencel.Name = "ocmcencel"
        Me.ocmcencel.Size = New System.Drawing.Size(117, 25)
        Me.ocmcencel.TabIndex = 102
        Me.ocmcencel.TabStop = False
        Me.ocmcencel.Tag = "2|"
        Me.ocmcencel.Text = "CANCEL"
        '
        'ocmok
        '
        Me.ocmok.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmok.Location = New System.Drawing.Point(53, 12)
        Me.ocmok.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(117, 25)
        Me.ocmok.TabIndex = 101
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'wAbsentDashboardFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(356, 324)
        Me.Controls.Add(Me.ogbcommand)
        Me.Controls.Add(Me.ogbDefectIssue)
        Me.Controls.Add(Me.ogbDefectAndDowntimeChart)
        Me.Controls.Add(Me.ogbProductionPerformanceChart)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wAbsentDashboardFilter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Filter Data"
        CType(Me.ogbProductionPerformanceChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbProductionPerformanceChart.ResumeLayout(False)
        CType(Me.FNProductionPerformanceChart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbDefectAndDowntimeChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbDefectAndDowntimeChart.ResumeLayout(False)
        CType(Me.FNDefectAndDowntimeChart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbDefectIssue, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbDefectIssue.ResumeLayout(False)
        CType(Me.FNDefectIssue.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbcommand, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbcommand.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbProductionPerformanceChart As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbDefectAndDowntimeChart As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbDefectIssue As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbcommand As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNProductionPerformanceChart As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNDefectAndDowntimeChart As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNDefectIssue As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ocmcencel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
End Class
