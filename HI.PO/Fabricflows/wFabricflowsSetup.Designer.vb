Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wFabricflowsSetup
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
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogdpo = New DevExpress.XtraGrid.GridControl()
        Me.ogvpo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xVenderGrp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xVender = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xVenderName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateSendMail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEditFTNeedConfirmAfter = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.xFNMailTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemComboBox1 = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.MailTime1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTimeEditTime = New DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit()
        Me.MailTime2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.MailTime3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTNeedConfirmAfter = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.otbmain = New DevExpress.XtraTab.XtraTabControl()
        Me.otpnew = New DevExpress.XtraTab.XtraTabPage()
        Me.otpworking = New DevExpress.XtraTab.XtraTabPage()
        Me.ogdwpo = New DevExpress.XtraGrid.GridControl()
        Me.ogvwpo = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xVenderMailLogIn = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FirstName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xLastName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNVenderType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTVendor = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.otppayment = New DevExpress.XtraTab.XtraTabPage()
        Me.ogdpopayment = New DevExpress.XtraGrid.GridControl()
        Me.ogvpopayment = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xFNStateMail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMailType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTCCMail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogdpo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvpo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEditFTNeedConfirmAfter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTimeEditTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otbmain.SuspendLayout()
        Me.otpnew.SuspendLayout()
        Me.otpworking.SuspendLayout()
        CType(Me.ogdwpo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvwpo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otppayment.SuspendLayout()
        CType(Me.ogdpopayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvpopayment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(149, 296)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(607, 99)
        Me.ogbmainprocbutton.TabIndex = 386
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(494, 11)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(14, 10)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(95, 25)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(115, 12)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(117, 23)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'ogdpo
        '
        Me.ogdpo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdpo.Location = New System.Drawing.Point(0, 0)
        Me.ogdpo.MainView = Me.ogvpo
        Me.ogdpo.Name = "ogdpo"
        Me.ogdpo.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEditFTNeedConfirmAfter, Me.RepositoryItemComboBox1, Me.RepositoryItemTimeEditTime})
        Me.ogdpo.Size = New System.Drawing.Size(1135, 417)
        Me.ogdpo.TabIndex = 0
        Me.ogdpo.TabStop = False
        Me.ogdpo.Tag = "2|"
        Me.ogdpo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvpo})
        '
        'ogvpo
        '
        Me.ogvpo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xVenderGrp, Me.xVender, Me.xVenderName, Me.FTStateSendMail, Me.xFNMailTime, Me.MailTime1, Me.MailTime2, Me.MailTime3, Me.xFTNeedConfirmAfter})
        Me.ogvpo.GridControl = Me.ogdpo
        Me.ogvpo.Name = "ogvpo"
        Me.ogvpo.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvpo.OptionsView.ColumnAutoWidth = False
        Me.ogvpo.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvpo.OptionsView.ShowGroupPanel = False
        Me.ogvpo.Tag = "2|"
        '
        'xVenderGrp
        '
        Me.xVenderGrp.Caption = "Vendor Group Code"
        Me.xVenderGrp.FieldName = "VenderGrp"
        Me.xVenderGrp.MinWidth = 22
        Me.xVenderGrp.Name = "xVenderGrp"
        Me.xVenderGrp.Visible = True
        Me.xVenderGrp.VisibleIndex = 0
        Me.xVenderGrp.Width = 112
        '
        'xVender
        '
        Me.xVender.Caption = "Wisdom Vendor Code"
        Me.xVender.FieldName = "Vender"
        Me.xVender.MinWidth = 22
        Me.xVender.Name = "xVender"
        Me.xVender.Visible = True
        Me.xVender.VisibleIndex = 1
        Me.xVender.Width = 121
        '
        'xVenderName
        '
        Me.xVenderName.Caption = "Vendor Name"
        Me.xVenderName.FieldName = "VenderName"
        Me.xVenderName.MinWidth = 22
        Me.xVenderName.Name = "xVenderName"
        Me.xVenderName.Visible = True
        Me.xVenderName.VisibleIndex = 2
        Me.xVenderName.Width = 276
        '
        'FTStateSendMail
        '
        Me.FTStateSendMail.Caption = "Send Mail"
        Me.FTStateSendMail.ColumnEdit = Me.RepositoryItemCheckEditFTNeedConfirmAfter
        Me.FTStateSendMail.FieldName = "FTStateSendMail"
        Me.FTStateSendMail.MinWidth = 22
        Me.FTStateSendMail.Name = "FTStateSendMail"
        Me.FTStateSendMail.Visible = True
        Me.FTStateSendMail.VisibleIndex = 3
        Me.FTStateSendMail.Width = 93
        '
        'RepositoryItemCheckEditFTNeedConfirmAfter
        '
        Me.RepositoryItemCheckEditFTNeedConfirmAfter.AutoHeight = False
        Me.RepositoryItemCheckEditFTNeedConfirmAfter.Name = "RepositoryItemCheckEditFTNeedConfirmAfter"
        Me.RepositoryItemCheckEditFTNeedConfirmAfter.ValueChecked = "1"
        Me.RepositoryItemCheckEditFTNeedConfirmAfter.ValueUnchecked = "0"
        '
        'xFNMailTime
        '
        Me.xFNMailTime.Caption = "Mail Time"
        Me.xFNMailTime.ColumnEdit = Me.RepositoryItemComboBox1
        Me.xFNMailTime.FieldName = "FNMailTime"
        Me.xFNMailTime.MinWidth = 22
        Me.xFNMailTime.Name = "xFNMailTime"
        Me.xFNMailTime.Visible = True
        Me.xFNMailTime.VisibleIndex = 4
        Me.xFNMailTime.Width = 90
        '
        'RepositoryItemComboBox1
        '
        Me.RepositoryItemComboBox1.AutoHeight = False
        Me.RepositoryItemComboBox1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemComboBox1.Items.AddRange(New Object() {"By Time", "Every 5 Minute"})
        Me.RepositoryItemComboBox1.Name = "RepositoryItemComboBox1"
        Me.RepositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'MailTime1
        '
        Me.MailTime1.Caption = "Mail Time 1"
        Me.MailTime1.ColumnEdit = Me.RepositoryItemTimeEditTime
        Me.MailTime1.FieldName = "MailTime1"
        Me.MailTime1.MinWidth = 22
        Me.MailTime1.Name = "MailTime1"
        Me.MailTime1.Visible = True
        Me.MailTime1.VisibleIndex = 5
        Me.MailTime1.Width = 90
        '
        'RepositoryItemTimeEditTime
        '
        Me.RepositoryItemTimeEditTime.AutoHeight = False
        Me.RepositoryItemTimeEditTime.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemTimeEditTime.DisplayFormat.FormatString = "HH:mm"
        Me.RepositoryItemTimeEditTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.RepositoryItemTimeEditTime.EditFormat.FormatString = "HH:mm"
        Me.RepositoryItemTimeEditTime.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.RepositoryItemTimeEditTime.Mask.EditMask = "HH:mm"
        Me.RepositoryItemTimeEditTime.Name = "RepositoryItemTimeEditTime"
        '
        'MailTime2
        '
        Me.MailTime2.Caption = "Mail Time 2"
        Me.MailTime2.ColumnEdit = Me.RepositoryItemTimeEditTime
        Me.MailTime2.FieldName = "MailTime2"
        Me.MailTime2.MinWidth = 22
        Me.MailTime2.Name = "MailTime2"
        Me.MailTime2.Visible = True
        Me.MailTime2.VisibleIndex = 6
        Me.MailTime2.Width = 82
        '
        'MailTime3
        '
        Me.MailTime3.Caption = "Mail Time 3"
        Me.MailTime3.ColumnEdit = Me.RepositoryItemTimeEditTime
        Me.MailTime3.FieldName = "MailTime3"
        Me.MailTime3.MinWidth = 22
        Me.MailTime3.Name = "MailTime3"
        Me.MailTime3.Visible = True
        Me.MailTime3.VisibleIndex = 7
        Me.MailTime3.Width = 83
        '
        'xFTNeedConfirmAfter
        '
        Me.xFTNeedConfirmAfter.Caption = "Confirm Date After"
        Me.xFTNeedConfirmAfter.ColumnEdit = Me.RepositoryItemCheckEditFTNeedConfirmAfter
        Me.xFTNeedConfirmAfter.FieldName = "FTNeedConfirmAfter"
        Me.xFTNeedConfirmAfter.MinWidth = 22
        Me.xFTNeedConfirmAfter.Name = "xFTNeedConfirmAfter"
        Me.xFTNeedConfirmAfter.Visible = True
        Me.xFTNeedConfirmAfter.VisibleIndex = 8
        Me.xFTNeedConfirmAfter.Width = 115
        '
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'otbmain
        '
        Me.otbmain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otbmain.Location = New System.Drawing.Point(0, 0)
        Me.otbmain.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otbmain.Name = "otbmain"
        Me.otbmain.SelectedTabPage = Me.otpnew
        Me.otbmain.Size = New System.Drawing.Size(1137, 442)
        Me.otbmain.TabIndex = 387
        Me.otbmain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpnew, Me.otpworking, Me.otppayment})
        '
        'otpnew
        '
        Me.otpnew.Controls.Add(Me.ogdpo)
        Me.otpnew.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otpnew.Name = "otpnew"
        Me.otpnew.Size = New System.Drawing.Size(1135, 417)
        Me.otpnew.Text = "Vendor List"
        '
        'otpworking
        '
        Me.otpworking.Controls.Add(Me.ogdwpo)
        Me.otpworking.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otpworking.Name = "otpworking"
        Me.otpworking.PageVisible = False
        Me.otpworking.Size = New System.Drawing.Size(1135, 417)
        Me.otpworking.Text = "User "
        '
        'ogdwpo
        '
        Me.ogdwpo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdwpo.Location = New System.Drawing.Point(0, 0)
        Me.ogdwpo.MainView = Me.ogvwpo
        Me.ogdwpo.Name = "ogdwpo"
        Me.ogdwpo.Size = New System.Drawing.Size(1135, 417)
        Me.ogdwpo.TabIndex = 1
        Me.ogdwpo.TabStop = False
        Me.ogdwpo.Tag = "2|"
        Me.ogdwpo.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvwpo})
        '
        'ogvwpo
        '
        Me.ogvwpo.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xVenderMailLogIn, Me.FirstName, Me.xLastName, Me.FNVenderType, Me.xFTVendor})
        Me.ogvwpo.GridControl = Me.ogdwpo
        Me.ogvwpo.Name = "ogvwpo"
        Me.ogvwpo.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvwpo.OptionsView.ColumnAutoWidth = False
        Me.ogvwpo.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvwpo.OptionsView.ShowGroupPanel = False
        Me.ogvwpo.Tag = "2|"
        '
        'xVenderMailLogIn
        '
        Me.xVenderMailLogIn.Caption = "Vendor Mail Log In"
        Me.xVenderMailLogIn.FieldName = "VenderMailLogIn"
        Me.xVenderMailLogIn.MinWidth = 22
        Me.xVenderMailLogIn.Name = "xVenderMailLogIn"
        Me.xVenderMailLogIn.Visible = True
        Me.xVenderMailLogIn.VisibleIndex = 0
        Me.xVenderMailLogIn.Width = 200
        '
        'FirstName
        '
        Me.FirstName.Caption = "First Name"
        Me.FirstName.FieldName = "FirstName"
        Me.FirstName.MinWidth = 22
        Me.FirstName.Name = "FirstName"
        Me.FirstName.Visible = True
        Me.FirstName.VisibleIndex = 1
        Me.FirstName.Width = 220
        '
        'xLastName
        '
        Me.xLastName.Caption = "Last Name"
        Me.xLastName.FieldName = "LastName"
        Me.xLastName.MinWidth = 22
        Me.xLastName.Name = "xLastName"
        Me.xLastName.Visible = True
        Me.xLastName.VisibleIndex = 2
        Me.xLastName.Width = 231
        '
        'FNVenderType
        '
        Me.FNVenderType.Caption = "Vendor Type"
        Me.FNVenderType.FieldName = "FNVenderType"
        Me.FNVenderType.MinWidth = 22
        Me.FNVenderType.Name = "FNVenderType"
        Me.FNVenderType.Visible = True
        Me.FNVenderType.VisibleIndex = 3
        Me.FNVenderType.Width = 113
        '
        'xFTVendor
        '
        Me.xFTVendor.Caption = "Vendor"
        Me.xFTVendor.FieldName = "FTVendor"
        Me.xFTVendor.MinWidth = 22
        Me.xFTVendor.Name = "xFTVendor"
        Me.xFTVendor.Visible = True
        Me.xFTVendor.VisibleIndex = 4
        Me.xFTVendor.Width = 250
        '
        'otppayment
        '
        Me.otppayment.Controls.Add(Me.ogdpopayment)
        Me.otppayment.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otppayment.Name = "otppayment"
        Me.otppayment.PageVisible = False
        Me.otppayment.Size = New System.Drawing.Size(1135, 417)
        Me.otppayment.Text = "System Mail"
        '
        'ogdpopayment
        '
        Me.ogdpopayment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdpopayment.Location = New System.Drawing.Point(0, 0)
        Me.ogdpopayment.MainView = Me.ogvpopayment
        Me.ogdpopayment.Name = "ogdpopayment"
        Me.ogdpopayment.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit1})
        Me.ogdpopayment.Size = New System.Drawing.Size(1135, 417)
        Me.ogdpopayment.TabIndex = 1
        Me.ogdpopayment.TabStop = False
        Me.ogdpopayment.Tag = "2|"
        Me.ogdpopayment.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvpopayment})
        '
        'ogvpopayment
        '
        Me.ogvpopayment.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xFNStateMail, Me.FTMailType, Me.xFTCCMail})
        Me.ogvpopayment.GridControl = Me.ogdpopayment
        Me.ogvpopayment.Name = "ogvpopayment"
        Me.ogvpopayment.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvpopayment.OptionsView.ColumnAutoWidth = False
        Me.ogvpopayment.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvpopayment.OptionsView.ShowGroupPanel = False
        Me.ogvpopayment.Tag = "2|"
        '
        'xFNStateMail
        '
        Me.xFNStateMail.Caption = "FNStateMail"
        Me.xFNStateMail.FieldName = "FNStateMail"
        Me.xFNStateMail.MinWidth = 22
        Me.xFNStateMail.Name = "xFNStateMail"
        Me.xFNStateMail.Width = 83
        '
        'FTMailType
        '
        Me.FTMailType.Caption = "Mail Type"
        Me.FTMailType.FieldName = "FTMailType"
        Me.FTMailType.MinWidth = 22
        Me.FTMailType.Name = "FTMailType"
        Me.FTMailType.Visible = True
        Me.FTMailType.VisibleIndex = 0
        Me.FTMailType.Width = 120
        '
        'xFTCCMail
        '
        Me.xFTCCMail.Caption = "CC Mail"
        Me.xFTCCMail.ColumnEdit = Me.RepositoryItemTextEdit1
        Me.xFTCCMail.FieldName = "FTCCMail"
        Me.xFTCCMail.MinWidth = 22
        Me.xFTCCMail.Name = "xFTCCMail"
        Me.xFTCCMail.Visible = True
        Me.xFTCCMail.VisibleIndex = 1
        Me.xFTCCMail.Width = 3000
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.MaxLength = 1000
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'wFabricflowsSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1137, 442)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otbmain)
        Me.Name = "wFabricflowsSetup"
        Me.Text = "Fabric flows Setup"
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogdpo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvpo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEditFTNeedConfirmAfter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemComboBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTimeEditTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otbmain.ResumeLayout(False)
        Me.otpnew.ResumeLayout(False)
        Me.otpworking.ResumeLayout(False)
        CType(Me.ogdwpo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvwpo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otppayment.ResumeLayout(False)
        CType(Me.ogdpopayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvpopayment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogdpo As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvpo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents oDockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents otbmain As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpnew As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otpworking As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogdwpo As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvwpo As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents otppayment As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogdpopayment As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvpopayment As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xVenderGrp As GridColumn
    Friend WithEvents xVender As GridColumn
    Friend WithEvents xVenderName As GridColumn
    Friend WithEvents FTStateSendMail As GridColumn
    Friend WithEvents xFNMailTime As GridColumn
    Friend WithEvents MailTime1 As GridColumn
    Friend WithEvents MailTime2 As GridColumn
    Friend WithEvents MailTime3 As GridColumn
    Friend WithEvents xFTNeedConfirmAfter As GridColumn
    Friend WithEvents xVenderMailLogIn As GridColumn
    Friend WithEvents FirstName As GridColumn
    Friend WithEvents xLastName As GridColumn
    Friend WithEvents FNVenderType As GridColumn
    Friend WithEvents xFTVendor As GridColumn
    Friend WithEvents xFNStateMail As GridColumn
    Friend WithEvents FTMailType As GridColumn
    Friend WithEvents xFTCCMail As GridColumn
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents RepositoryItemCheckEditFTNeedConfirmAfter As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemComboBox1 As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents RepositoryItemTimeEditTime As DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit
End Class
