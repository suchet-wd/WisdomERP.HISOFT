<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wImportCustomerPOSaleMan
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
        Me.ogbBrowseFile = New DevExpress.XtraEditors.GroupControl()
        Me.FTFilePath_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTFilePath = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogbViewDetail = New DevExpress.XtraEditors.GroupControl()
        Me.otcImportOrderNo = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.ogdImportOrder = New DevExpress.XtraGrid.GridControl()
        Me.ogvImportOrder = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmImportOrder = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmReadExcel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclearclsr = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmExit = New DevExpress.XtraEditors.SimpleButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.olbexcelinfo = New DevExpress.XtraEditors.LabelControl()
        CType(Me.ogbBrowseFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbBrowseFile.SuspendLayout()
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbViewDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbViewDetail.SuspendLayout()
        CType(Me.otcImportOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otcImportOrderNo.SuspendLayout()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.ogdImportOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvImportOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbBrowseFile
        '
        Me.ogbBrowseFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbBrowseFile.Controls.Add(Me.olbexcelinfo)
        Me.ogbBrowseFile.Controls.Add(Me.FTFilePath_lbl)
        Me.ogbBrowseFile.Controls.Add(Me.FTFilePath)
        Me.ogbBrowseFile.Location = New System.Drawing.Point(0, 4)
        Me.ogbBrowseFile.Name = "ogbBrowseFile"
        Me.ogbBrowseFile.Size = New System.Drawing.Size(1023, 79)
        Me.ogbBrowseFile.TabIndex = 0
        Me.ogbBrowseFile.Text = "Browse Source File"
        '
        'FTFilePath_lbl
        '
        Me.FTFilePath_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath_lbl.Appearance.Options.UseForeColor = True
        Me.FTFilePath_lbl.Appearance.Options.UseTextOptions = True
        Me.FTFilePath_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTFilePath_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTFilePath_lbl.Location = New System.Drawing.Point(14, 30)
        Me.FTFilePath_lbl.Name = "FTFilePath_lbl"
        Me.FTFilePath_lbl.Size = New System.Drawing.Size(128, 19)
        Me.FTFilePath_lbl.TabIndex = 432
        Me.FTFilePath_lbl.Tag = "2|"
        Me.FTFilePath_lbl.Text = "File Path :"
        '
        'FTFilePath
        '
        Me.FTFilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTFilePath.Location = New System.Drawing.Point(147, 30)
        Me.FTFilePath.Name = "FTFilePath"
        Me.FTFilePath.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTFilePath.Properties.Appearance.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTFilePath.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTFilePath.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTFilePath.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTFilePath.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTFilePath.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTFilePath.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTFilePath.Properties.ReadOnly = True
        Me.FTFilePath.Size = New System.Drawing.Size(869, 20)
        Me.FTFilePath.TabIndex = 0
        Me.FTFilePath.Tag = "2|"
        '
        'ogbViewDetail
        '
        Me.ogbViewDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbViewDetail.Controls.Add(Me.otcImportOrderNo)
        Me.ogbViewDetail.Location = New System.Drawing.Point(0, 89)
        Me.ogbViewDetail.Name = "ogbViewDetail"
        Me.ogbViewDetail.Size = New System.Drawing.Size(1023, 537)
        Me.ogbViewDetail.TabIndex = 1
        Me.ogbViewDetail.Text = "Factory Order Detail From Customer And Merchandiser"
        '
        'otcImportOrderNo
        '
        Me.otcImportOrderNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otcImportOrderNo.Location = New System.Drawing.Point(2, 23)
        Me.otcImportOrderNo.Name = "otcImportOrderNo"
        Me.otcImportOrderNo.SelectedTabPage = Me.XtraTabPage2
        Me.otcImportOrderNo.Size = New System.Drawing.Size(1019, 512)
        Me.otcImportOrderNo.TabIndex = 0
        Me.otcImportOrderNo.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage2})
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.ogdImportOrder)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(1017, 487)
        Me.XtraTabPage2.Text = "Raw Data Import Order"
        '
        'ogdImportOrder
        '
        Me.ogdImportOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdImportOrder.Location = New System.Drawing.Point(0, 0)
        Me.ogdImportOrder.MainView = Me.ogvImportOrder
        Me.ogdImportOrder.Name = "ogdImportOrder"
        Me.ogdImportOrder.Size = New System.Drawing.Size(1017, 487)
        Me.ogdImportOrder.TabIndex = 0
        Me.ogdImportOrder.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvImportOrder})
        '
        'ogvImportOrder
        '
        Me.ogvImportOrder.GridControl = Me.ogdImportOrder
        Me.ogvImportOrder.Name = "ogvImportOrder"
        Me.ogvImportOrder.OptionsView.ShowGroupPanel = False
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmImportOrder)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmReadExcel)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclearclsr)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmExit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(123, 161)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(660, 43)
        Me.ogbmainprocbutton.TabIndex = 462
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmImportOrder
        '
        Me.ocmImportOrder.Location = New System.Drawing.Point(111, 8)
        Me.ocmImportOrder.Name = "ocmImportOrder"
        Me.ocmImportOrder.Size = New System.Drawing.Size(126, 25)
        Me.ocmImportOrder.TabIndex = 99
        Me.ocmImportOrder.TabStop = False
        Me.ocmImportOrder.Tag = "2|"
        Me.ocmImportOrder.Text = "IMPORT ORDER NO."
        '
        'ocmReadExcel
        '
        Me.ocmReadExcel.Location = New System.Drawing.Point(10, 8)
        Me.ocmReadExcel.Name = "ocmReadExcel"
        Me.ocmReadExcel.Size = New System.Drawing.Size(95, 25)
        Me.ocmReadExcel.TabIndex = 98
        Me.ocmReadExcel.TabStop = False
        Me.ocmReadExcel.Tag = "2|"
        Me.ocmReadExcel.Text = "READ EXCEL FILE"
        '
        'ocmclearclsr
        '
        Me.ocmclearclsr.Location = New System.Drawing.Point(344, 8)
        Me.ocmclearclsr.Name = "ocmclearclsr"
        Me.ocmclearclsr.Size = New System.Drawing.Size(95, 25)
        Me.ocmclearclsr.TabIndex = 97
        Me.ocmclearclsr.TabStop = False
        Me.ocmclearclsr.Tag = "2|"
        Me.ocmclearclsr.Text = "CLEAR"
        '
        'ocmExit
        '
        Me.ocmExit.Location = New System.Drawing.Point(445, 8)
        Me.ocmExit.Name = "ocmExit"
        Me.ocmExit.Size = New System.Drawing.Size(95, 25)
        Me.ocmExit.TabIndex = 96
        Me.ocmExit.TabStop = False
        Me.ocmExit.Tag = "2|"
        Me.ocmExit.Text = "EXIT"
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'olbexcelinfo
        '
        Me.olbexcelinfo.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.olbexcelinfo.Appearance.Options.UseForeColor = True
        Me.olbexcelinfo.Appearance.Options.UseTextOptions = True
        Me.olbexcelinfo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.olbexcelinfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbexcelinfo.Location = New System.Drawing.Point(12, 55)
        Me.olbexcelinfo.Name = "olbexcelinfo"
        Me.olbexcelinfo.Size = New System.Drawing.Size(1004, 19)
        Me.olbexcelinfo.TabIndex = 433
        Me.olbexcelinfo.Tag = "2|"
        Me.olbexcelinfo.Text = "Excel Column D= Sub Job,E= Customer PO,F= Style No,G= Color Way,H= PO Line"
        '
        'wImportCustomerPOSaleMan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1023, 634)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbViewDetail)
        Me.Controls.Add(Me.ogbBrowseFile)
        Me.Name = "wImportCustomerPOSaleMan"
        Me.Text = "Import Customer PO Sale Man"
        CType(Me.ogbBrowseFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbBrowseFile.ResumeLayout(False)
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbViewDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbViewDetail.ResumeLayout(False)
        CType(Me.otcImportOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otcImportOrderNo.ResumeLayout(False)
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.ogdImportOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvImportOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbBrowseFile As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTFilePath As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogbViewDetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogdImportOrder As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvImportOrder As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTFilePath_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmImportOrder As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmReadExcel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclearclsr As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otcImportOrderNo As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents olbexcelinfo As DevExpress.XtraEditors.LabelControl
End Class
