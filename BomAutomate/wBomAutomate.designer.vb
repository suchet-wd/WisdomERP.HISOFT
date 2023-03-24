<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wBomAutomate
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
        Me.ogbheader = New DevExpress.XtraEditors.GroupControl()
        Me.FNRecord = New DevExpress.XtraEditors.CalcEdit()
        Me.FTFixrecord = New DevExpress.XtraEditors.CheckEdit()
        Me.ocmrefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpdata = New DevExpress.XtraTab.XtraTabPage()
        CType(Me.ogbheader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        CType(Me.FNRecord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTFixrecord.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbheader
        '
        Me.ogbheader.Controls.Add(Me.FNRecord)
        Me.ogbheader.Controls.Add(Me.FTFixrecord)
        Me.ogbheader.Controls.Add(Me.ocmrefresh)
        Me.ogbheader.Controls.Add(Me.ocmexit)
        Me.ogbheader.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogbheader.Location = New System.Drawing.Point(0, 0)
        Me.ogbheader.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Size = New System.Drawing.Size(1241, 70)
        Me.ogbheader.TabIndex = 139
        '
        'FNRecord
        '
        Me.FNRecord.EditValue = New Decimal(New Integer() {2, 0, 0, 0})
        Me.FNRecord.EnterMoveNextControl = True
        Me.FNRecord.Location = New System.Drawing.Point(247, 32)
        Me.FNRecord.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNRecord.Name = "FNRecord"
        Me.FNRecord.Properties.Appearance.Options.UseTextOptions = True
        Me.FNRecord.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNRecord.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNRecord.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNRecord.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNRecord.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNRecord.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNRecord.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNRecord.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNRecord.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNRecord.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNRecord.Properties.DisplayFormat.FormatString = "{0:n0}"
        Me.FNRecord.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNRecord.Properties.EditFormat.FormatString = "{0:n0}"
        Me.FNRecord.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNRecord.Properties.Precision = 0
        Me.FNRecord.Size = New System.Drawing.Size(68, 22)
        Me.FNRecord.TabIndex = 109
        Me.FNRecord.TabStop = False
        Me.FNRecord.Tag = "2|"
        Me.FNRecord.Visible = False
        '
        'FTFixrecord
        '
        Me.FTFixrecord.EditValue = "0"
        Me.FTFixrecord.Location = New System.Drawing.Point(42, 35)
        Me.FTFixrecord.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTFixrecord.Name = "FTFixrecord"
        Me.FTFixrecord.Properties.Caption = "set the number of records"
        Me.FTFixrecord.Properties.ValueChecked = "1"
        Me.FTFixrecord.Properties.ValueUnchecked = "0"
        Me.FTFixrecord.Size = New System.Drawing.Size(199, 20)
        Me.FTFixrecord.TabIndex = 108
        Me.FTFixrecord.Tag = "2|"
        '
        'ocmrefresh
        '
        Me.ocmrefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmrefresh.Location = New System.Drawing.Point(853, 29)
        Me.ocmrefresh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmrefresh.Name = "ocmrefresh"
        Me.ocmrefresh.Size = New System.Drawing.Size(152, 31)
        Me.ocmrefresh.TabIndex = 107
        Me.ocmrefresh.TabStop = False
        Me.ocmrefresh.Tag = "2|"
        Me.ocmrefresh.Text = "GET"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(1044, 29)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(185, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'otb
        '
        Me.otb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otb.Location = New System.Drawing.Point(0, 70)
        Me.otb.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpdata
        Me.otb.Size = New System.Drawing.Size(1241, 680)
        Me.otb.TabIndex = 395
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpdata})
        '
        'otpdata
        '
        Me.otpdata.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otpdata.Name = "otpdata"
        Me.otpdata.Size = New System.Drawing.Size(1234, 646)
        Me.otpdata.Text = "Data"
        '
        'wBomAutomate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1241, 750)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wBomAutomate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bom OKTA Token"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.ogbheader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        CType(Me.FNRecord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTFixrecord.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbheader As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmrefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpdata As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents FTFixrecord As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FNRecord As DevExpress.XtraEditors.CalcEdit
End Class
