Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPopupTruckWay
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
        Me.ReposFDShipDateTo = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.RepositoryItemFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemrFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ocmdoc = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmSAVE = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.FTPartyName = New DevExpress.XtraEditors.MemoEdit()
        Me.FTFreightPayName = New DevExpress.XtraEditors.MemoEdit()
        Me.FTPortOFDistation = New DevExpress.XtraEditors.MemoEdit()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpdetail = New DevExpress.XtraTab.XtraTabPage()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.FTInvoiceNo = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.FTTruckWayNo = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.FNORTruckBillsNo = New DevExpress.XtraEditors.TextEdit()
        CType(Me.ReposFDShipDateTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFDShipDateTo.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemrFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.FTPartyName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTFreightPayName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTPortOFDistation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpdetail.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.FTInvoiceNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTTruckWayNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNORTruckBillsNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ReposFDShipDateTo
        '
        Me.ReposFDShipDateTo.AutoHeight = False
        Me.ReposFDShipDateTo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFDShipDateTo.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFDShipDateTo.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.ReposFDShipDateTo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ReposFDShipDateTo.EditFormat.FormatString = "dd/MM/yyyy"
        Me.ReposFDShipDateTo.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ReposFDShipDateTo.Name = "ReposFDShipDateTo"
        '
        'RepositoryItemFTSelect
        '
        Me.RepositoryItemFTSelect.AutoHeight = False
        Me.RepositoryItemFTSelect.Name = "RepositoryItemFTSelect"
        Me.RepositoryItemFTSelect.ValueChecked = "1"
        Me.RepositoryItemFTSelect.ValueUnchecked = "0"
        '
        'RepositoryItemrFTSelect
        '
        Me.RepositoryItemrFTSelect.AutoHeight = False
        Me.RepositoryItemrFTSelect.Name = "RepositoryItemrFTSelect"
        Me.RepositoryItemrFTSelect.ValueChecked = "1"
        Me.RepositoryItemrFTSelect.ValueUnchecked = "0"
        '
        'ocmdoc
        '
        Me.ocmdoc.Form = Me
        Me.ocmdoc.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SimpleButton1.Location = New System.Drawing.Point(580, 20)
        Me.SimpleButton1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(111, 31)
        Me.SimpleButton1.TabIndex = 96
        Me.SimpleButton1.TabStop = False
        Me.SimpleButton1.Tag = "2|"
        Me.SimpleButton1.Text = "EXIT"
        '
        'ocmSAVE
        '
        Me.ocmSAVE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmSAVE.Location = New System.Drawing.Point(12, 20)
        Me.ocmSAVE.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmSAVE.Name = "ocmSAVE"
        Me.ocmSAVE.Size = New System.Drawing.Size(111, 31)
        Me.ocmSAVE.TabIndex = 97
        Me.ocmSAVE.TabStop = False
        Me.ocmSAVE.Tag = "2|"
        Me.ocmSAVE.Text = "save"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.SimpleButton1)
        Me.PanelControl1.Controls.Add(Me.ocmSAVE)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 297)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(703, 64)
        Me.PanelControl1.TabIndex = 395
        '
        'FTPartyName
        '
        Me.FTPartyName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FTPartyName.EditValue = ""
        Me.FTPartyName.Location = New System.Drawing.Point(0, 0)
        Me.FTPartyName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPartyName.Name = "FTPartyName"
        Me.FTPartyName.Properties.MaxLength = 500
        Me.FTPartyName.Size = New System.Drawing.Size(684, 190)
        Me.FTPartyName.TabIndex = 539
        Me.FTPartyName.Tag = "2|"
        '
        'FTFreightPayName
        '
        Me.FTFreightPayName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FTFreightPayName.EditValue = ""
        Me.FTFreightPayName.Location = New System.Drawing.Point(0, 0)
        Me.FTFreightPayName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTFreightPayName.Name = "FTFreightPayName"
        Me.FTFreightPayName.Properties.MaxLength = 500
        Me.FTFreightPayName.Size = New System.Drawing.Size(684, 160)
        Me.FTFreightPayName.TabIndex = 539
        Me.FTFreightPayName.Tag = "2|"
        '
        'FTPortOFDistation
        '
        Me.FTPortOFDistation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FTPortOFDistation.EditValue = ""
        Me.FTPortOFDistation.Location = New System.Drawing.Point(0, 0)
        Me.FTPortOFDistation.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTPortOFDistation.Name = "FTPortOFDistation"
        Me.FTPortOFDistation.Properties.MaxLength = 500
        Me.FTPortOFDistation.Size = New System.Drawing.Size(684, 190)
        Me.FTPortOFDistation.TabIndex = 539
        Me.FTPortOFDistation.Tag = "2|"
        '
        'otb
        '
        Me.otb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.otb.Location = New System.Drawing.Point(12, 95)
        Me.otb.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpdetail
        Me.otb.Size = New System.Drawing.Size(691, 194)
        Me.otb.TabIndex = 540
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpdetail, Me.XtraTabPage1, Me.XtraTabPage2})
        '
        'otpdetail
        '
        Me.otpdetail.Controls.Add(Me.FTPartyName)
        Me.otpdetail.Name = "otpdetail"
        Me.otpdetail.Size = New System.Drawing.Size(684, 190)
        Me.otpdetail.Text = "NOTIFY PARTY"
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.FTPortOFDistation)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(684, 190)
        Me.XtraTabPage1.Text = "PORT OF DISCHARGE"
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.FTFreightPayName)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(684, 160)
        Me.XtraTabPage2.Text = "FREIGHT PAYABLE AT"
        '
        'FTInvoiceNo
        '
        Me.FTInvoiceNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTInvoiceNo.Location = New System.Drawing.Point(181, 11)
        Me.FTInvoiceNo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.FTInvoiceNo.Name = "FTInvoiceNo"
        Me.FTInvoiceNo.Properties.ReadOnly = True
        Me.FTInvoiceNo.Size = New System.Drawing.Size(215, 22)
        Me.FTInvoiceNo.TabIndex = 541
        Me.FTInvoiceNo.Tag = "2|"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl3.Appearance.Options.UseForeColor = True
        Me.LabelControl3.Appearance.Options.UseTextOptions = True
        Me.LabelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl3.Location = New System.Drawing.Point(13, 9)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(164, 22)
        Me.LabelControl3.TabIndex = 542
        Me.LabelControl3.Tag = "2|"
        Me.LabelControl3.Text = "Invoice No :"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(13, 35)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(164, 22)
        Me.LabelControl1.TabIndex = 542
        Me.LabelControl1.Tag = "2|"
        Me.LabelControl1.Text = "Truck Way Bill :"
        '
        'FTTruckWayNo
        '
        Me.FTTruckWayNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTTruckWayNo.Location = New System.Drawing.Point(181, 37)
        Me.FTTruckWayNo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.FTTruckWayNo.Name = "FTTruckWayNo"
        Me.FTTruckWayNo.Properties.ReadOnly = True
        Me.FTTruckWayNo.Size = New System.Drawing.Size(215, 22)
        Me.FTTruckWayNo.TabIndex = 541
        Me.FTTruckWayNo.Tag = "2|"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.Black
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        Me.LabelControl2.Appearance.Options.UseTextOptions = True
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Location = New System.Drawing.Point(13, 62)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(164, 22)
        Me.LabelControl2.TabIndex = 542
        Me.LabelControl2.Tag = "2|"
        Me.LabelControl2.Text = "NO. OF ORIGINAL  :"
        '
        'FNORTruckBillsNo
        '
        Me.FNORTruckBillsNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNORTruckBillsNo.Location = New System.Drawing.Point(181, 64)
        Me.FNORTruckBillsNo.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.FNORTruckBillsNo.Name = "FNORTruckBillsNo"
        Me.FNORTruckBillsNo.Size = New System.Drawing.Size(215, 22)
        Me.FNORTruckBillsNo.TabIndex = 541
        Me.FNORTruckBillsNo.Tag = "2|"
        '
        'wPopupTruckWay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(703, 361)
        Me.Controls.Add(Me.FNORTruckBillsNo)
        Me.Controls.Add(Me.FTTruckWayNo)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.FTInvoiceNo)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.PanelControl1)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wPopupTruckWay"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Truck Way Infomation"
        CType(Me.ReposFDShipDateTo.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFDShipDateTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemrFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.FTPartyName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTFreightPayName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTPortOFDistation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpdetail.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.FTInvoiceNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTTruckWayNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNORTruckBillsNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmdoc As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ocmSAVE As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ReposFDShipDateTo As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents RepositoryItemFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemrFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTPortOFDistation As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FTFreightPayName As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FTPartyName As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpdetail As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents FTTruckWayNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTInvoiceNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNORTruckBillsNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
End Class
