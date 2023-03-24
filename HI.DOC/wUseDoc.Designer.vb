<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wUseDoc
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wUseDoc))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.DockManager = New DevExpress.XtraBars.Docking.DockManager()
        Me.oCriteria = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTReviseNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDDocAge_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTReviseNo = New DevExpress.XtraEditors.TextEdit()
        Me.FDDocAge = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTDocumentName_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysDocNameId = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogrpdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmdownloadfile = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.ogrpcover = New DevExpress.XtraEditors.GroupControl()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.oCriteria.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTReviseNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDDocAge.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysDocNameId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogrpdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.ogrpcover, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DockManager
        '
        Me.DockManager.Form = Me
        Me.DockManager.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.oCriteria})
        Me.DockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'oCriteria
        '
        Me.oCriteria.Controls.Add(Me.DockPanel1_Container)
        Me.oCriteria.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.oCriteria.ID = New System.Guid("00ac1da3-6cab-4a41-83e8-538884691971")
        Me.oCriteria.Image = CType(resources.GetObject("oCriteria.Image"), System.Drawing.Image)
        Me.oCriteria.Location = New System.Drawing.Point(0, 0)
        Me.oCriteria.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oCriteria.Name = "oCriteria"
        Me.oCriteria.Options.AllowDockBottom = False
        Me.oCriteria.Options.AllowDockFill = False
        Me.oCriteria.Options.ShowCloseButton = False
        Me.oCriteria.OriginalSize = New System.Drawing.Size(200, 90)
        Me.oCriteria.Size = New System.Drawing.Size(1280, 90)
        Me.oCriteria.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTReviseNo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FDDocAge_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTReviseNo)
        Me.DockPanel1_Container.Controls.Add(Me.FDDocAge)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTDocumentName_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysDocNameId)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(5, 27)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1270, 56)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTReviseNo_lbl
        '
        Me.FTReviseNo_lbl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTReviseNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTReviseNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTReviseNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTReviseNo_lbl.Location = New System.Drawing.Point(906, 36)
        Me.FTReviseNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTReviseNo_lbl.Name = "FTReviseNo_lbl"
        Me.FTReviseNo_lbl.Size = New System.Drawing.Size(188, 16)
        Me.FTReviseNo_lbl.TabIndex = 455
        Me.FTReviseNo_lbl.Text = "Revise No :"
        '
        'FDDocAge_lbl
        '
        Me.FDDocAge_lbl.Appearance.Options.UseTextOptions = True
        Me.FDDocAge_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDDocAge_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDDocAge_lbl.Location = New System.Drawing.Point(406, 38)
        Me.FDDocAge_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDDocAge_lbl.Name = "FDDocAge_lbl"
        Me.FDDocAge_lbl.Size = New System.Drawing.Size(188, 16)
        Me.FDDocAge_lbl.TabIndex = 455
        Me.FDDocAge_lbl.Text = "Document Age :"
        '
        'FTReviseNo
        '
        Me.FTReviseNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTReviseNo.Location = New System.Drawing.Point(1100, 33)
        Me.FTReviseNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTReviseNo.Name = "FTReviseNo"
        Me.FTReviseNo.Size = New System.Drawing.Size(167, 22)
        Me.FTReviseNo.TabIndex = 454
        '
        'FDDocAge
        '
        Me.FDDocAge.Location = New System.Drawing.Point(601, 34)
        Me.FDDocAge.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDDocAge.Name = "FDDocAge"
        Me.FDDocAge.Size = New System.Drawing.Size(259, 22)
        Me.FDDocAge.TabIndex = 454
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(315, 4)
        Me.FNHSysCmpId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(952, 22)
        Me.FNHSysCmpId_None.TabIndex = 453
        Me.FNHSysCmpId_None.Tag = ""
        '
        'FTDocumentName_lbl
        '
        Me.FTDocumentName_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTDocumentName_lbl.Appearance.Options.UseForeColor = True
        Me.FTDocumentName_lbl.Appearance.Options.UseTextOptions = True
        Me.FTDocumentName_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTDocumentName_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTDocumentName_lbl.Location = New System.Drawing.Point(1, 33)
        Me.FTDocumentName_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTDocumentName_lbl.Name = "FTDocumentName_lbl"
        Me.FTDocumentName_lbl.Size = New System.Drawing.Size(153, 21)
        Me.FTDocumentName_lbl.TabIndex = 317
        Me.FTDocumentName_lbl.Tag = "2|"
        Me.FTDocumentName_lbl.Text = "Document Name :"
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(10, 4)
        Me.FNHSysCmpId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(145, 21)
        Me.FNHSysCmpId_lbl.TabIndex = 452
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(157, 4)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.ReadOnly = True
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(156, 22)
        Me.FNHSysCmpId.TabIndex = 451
        Me.FNHSysCmpId.Tag = ""
        '
        'FNHSysDocNameId
        '
        Me.FNHSysDocNameId.Location = New System.Drawing.Point(157, 33)
        Me.FNHSysDocNameId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysDocNameId.Name = "FNHSysDocNameId"
        Me.FNHSysDocNameId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "322", Nothing, True)})
        Me.FNHSysDocNameId.Properties.Tag = ""
        Me.FNHSysDocNameId.Size = New System.Drawing.Size(240, 22)
        Me.FNHSysDocNameId.TabIndex = 316
        Me.FNHSysDocNameId.Tag = "2|"
        '
        'ogrpdetail
        '
        Me.ogrpdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrpdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogrpdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpdetail.Name = "ogrpdetail"
        Me.ogrpdetail.Size = New System.Drawing.Size(1273, 687)
        Me.ogrpdetail.TabIndex = 3
        Me.ogrpdetail.Text = "Detail"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdownloadfile)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(279, 13)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(444, 61)
        Me.ogbmainprocbutton.TabIndex = 395
        '
        'ocmdownloadfile
        '
        Me.ocmdownloadfile.Location = New System.Drawing.Point(55, 14)
        Me.ocmdownloadfile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdownloadfile.Name = "ocmdownloadfile"
        Me.ocmdownloadfile.Size = New System.Drawing.Size(111, 31)
        Me.ocmdownloadfile.TabIndex = 100
        Me.ocmdownloadfile.TabStop = False
        Me.ocmdownloadfile.Tag = "2|"
        Me.ocmdownloadfile.Text = "Download File"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(178, 12)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(111, 31)
        Me.ocmload.TabIndex = 96
        Me.ocmload.TabStop = False
        Me.ocmload.Tag = "2|"
        Me.ocmload.Text = "Load"
        '
        'ocmexit
        '
        Me.ocmexit.Location = New System.Drawing.Point(306, 12)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraTabControl1.Location = New System.Drawing.Point(0, 90)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage1
        Me.XtraTabControl1.Size = New System.Drawing.Size(1280, 721)
        Me.XtraTabControl1.TabIndex = 397
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.ogrpdetail)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1273, 687)
        Me.XtraTabPage1.Text = "Document Attach file"
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.ogrpcover)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(1273, 687)
        Me.XtraTabPage2.Text = "Cover Document"
        '
        'ogrpcover
        '
        Me.ogrpcover.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrpcover.Location = New System.Drawing.Point(0, 0)
        Me.ogrpcover.Name = "ogrpcover"
        Me.ogrpcover.Size = New System.Drawing.Size(1273, 687)
        Me.ogrpcover.TabIndex = 0
        Me.ogrpcover.Text = "Conver document"
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
        Me.ReportViewer1.Name = "ReportViewer"
        Me.ReportViewer1.Size = New System.Drawing.Size(396, 246)
        Me.ReportViewer1.TabIndex = 0
        '
        'wUseDoc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1280, 811)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.oCriteria)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wUseDoc"
        Me.Text = "wUseDoc"
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.oCriteria.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTReviseNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDDocAge.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysDocNameId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogrpdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.ogrpcover, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents oCriteria As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ogrpdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTDocumentName_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysDocNameId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmdownloadfile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FDDocAge As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FDDocAge_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogrpcover As DevExpress.XtraEditors.GroupControl
    Friend WithEvents PrintDocument1 As Drawing.Printing.PrintDocument
    Private WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents FTReviseNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTReviseNo As DevExpress.XtraEditors.TextEdit
End Class
