<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wSewing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wSewing))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.DockManager = New DevExpress.XtraBars.Docking.DockManager()
        Me.oCriteria = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.oGrpInfo = New DevExpress.XtraEditors.GroupControl()
        Me.FNHSysSeasonId = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmcopy = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavedocument = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmReadDocumentfile = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.FNHSysSeasonId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysSeasonId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysStyleId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysStyleId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.oGrpdetail = New DevExpress.XtraEditors.GroupControl()
        Me.PdfBarController1 = New DevExpress.XtraPdfViewer.Bars.PdfBarController()
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.oCriteria.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.oGrpInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.oGrpInfo.SuspendLayout()
        CType(Me.FNHSysSeasonId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.FNHSysSeasonId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oGrpdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PdfBarController1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.oCriteria.ID = New System.Guid("e9f18b59-b75a-4422-b9c7-c371a6c1cf41")
        Me.oCriteria.Image = CType(resources.GetObject("oCriteria.Image"), System.Drawing.Image)
        Me.oCriteria.Location = New System.Drawing.Point(0, 0)
        Me.oCriteria.Name = "oCriteria"
        Me.oCriteria.Options.AllowDockBottom = False
        Me.oCriteria.Options.AllowDockFill = False
        Me.oCriteria.Options.AllowDockLeft = False
        Me.oCriteria.Options.AllowDockRight = False
        Me.oCriteria.Options.ShowCloseButton = False
        Me.oCriteria.OriginalSize = New System.Drawing.Size(200, 108)
        Me.oCriteria.Size = New System.Drawing.Size(1089, 108)
        Me.oCriteria.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.oGrpInfo)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 23)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1081, 80)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'oGrpInfo
        '
        Me.oGrpInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.oGrpInfo.Controls.Add(Me.FNHSysSeasonId)
        Me.oGrpInfo.Controls.Add(Me.ogbmainprocbutton)
        Me.oGrpInfo.Controls.Add(Me.FNHSysSeasonId_None)
        Me.oGrpInfo.Controls.Add(Me.FNHSysSeasonId_lbl)
        Me.oGrpInfo.Controls.Add(Me.FNHSysStyleId)
        Me.oGrpInfo.Controls.Add(Me.FNHSysStyleId_None)
        Me.oGrpInfo.Controls.Add(Me.FNHSysStyleId_lbl)
        Me.oGrpInfo.Location = New System.Drawing.Point(3, 1)
        Me.oGrpInfo.Name = "oGrpInfo"
        Me.oGrpInfo.ShowCaption = False
        Me.oGrpInfo.Size = New System.Drawing.Size(1055, 76)
        Me.oGrpInfo.TabIndex = 1
        Me.oGrpInfo.Text = "GroupControl1"
        '
        'FNHSysSeasonId
        '
        Me.FNHSysSeasonId.Location = New System.Drawing.Point(135, 31)
        Me.FNHSysSeasonId.Name = "FNHSysSeasonId"
        Me.FNHSysSeasonId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "94", Nothing, True)})
        Me.FNHSysSeasonId.Properties.Tag = ""
        Me.FNHSysSeasonId.Size = New System.Drawing.Size(125, 20)
        Me.FNHSysSeasonId.TabIndex = 543
        Me.FNHSysSeasonId.Tag = ""
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmcopy)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavedocument)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdelete)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmReadDocumentfile)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(337, 0)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(668, 60)
        Me.ogbmainprocbutton.TabIndex = 395
        '
        'ocmcopy
        '
        Me.ocmcopy.Location = New System.Drawing.Point(221, 4)
        Me.ocmcopy.Name = "ocmcopy"
        Me.ocmcopy.Size = New System.Drawing.Size(95, 25)
        Me.ocmcopy.TabIndex = 101
        Me.ocmcopy.TabStop = False
        Me.ocmcopy.Tag = "2|"
        Me.ocmcopy.Text = "COPY"
        '
        'ocmsavedocument
        '
        Me.ocmsavedocument.Location = New System.Drawing.Point(14, 4)
        Me.ocmsavedocument.Name = "ocmsavedocument"
        Me.ocmsavedocument.Size = New System.Drawing.Size(95, 25)
        Me.ocmsavedocument.TabIndex = 100
        Me.ocmsavedocument.TabStop = False
        Me.ocmsavedocument.Tag = "2|"
        Me.ocmsavedocument.Text = "SAVE"
        '
        'ocmdelete
        '
        Me.ocmdelete.Location = New System.Drawing.Point(120, 31)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(95, 25)
        Me.ocmdelete.TabIndex = 96
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "Delete"
        '
        'ocmReadDocumentfile
        '
        Me.ocmReadDocumentfile.Location = New System.Drawing.Point(14, 30)
        Me.ocmReadDocumentfile.Name = "ocmReadDocumentfile"
        Me.ocmReadDocumentfile.Size = New System.Drawing.Size(95, 25)
        Me.ocmReadDocumentfile.TabIndex = 96
        Me.ocmReadDocumentfile.TabStop = False
        Me.ocmReadDocumentfile.Tag = "2|"
        Me.ocmReadDocumentfile.Text = "Read File"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(120, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(95, 25)
        Me.ocmclear.TabIndex = 96
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "Clear"
        '
        'ocmexit
        '
        Me.ocmexit.Location = New System.Drawing.Point(221, 31)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'FNHSysSeasonId_None
        '
        Me.FNHSysSeasonId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysSeasonId_None.Location = New System.Drawing.Point(262, 32)
        Me.FNHSysSeasonId_None.Name = "FNHSysSeasonId_None"
        Me.FNHSysSeasonId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSeasonId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysSeasonId_None.Properties.ReadOnly = True
        Me.FNHSysSeasonId_None.Size = New System.Drawing.Size(557, 20)
        Me.FNHSysSeasonId_None.TabIndex = 453
        Me.FNHSysSeasonId_None.Tag = ""
        '
        'FNHSysSeasonId_lbl
        '
        Me.FNHSysSeasonId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSeasonId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysSeasonId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysSeasonId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysSeasonId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysSeasonId_lbl.Location = New System.Drawing.Point(5, 32)
        Me.FNHSysSeasonId_lbl.Name = "FNHSysSeasonId_lbl"
        Me.FNHSysSeasonId_lbl.Size = New System.Drawing.Size(124, 17)
        Me.FNHSysSeasonId_lbl.TabIndex = 452
        Me.FNHSysSeasonId_lbl.Tag = "2|"
        Me.FNHSysSeasonId_lbl.Text = "FNHSysSeasonId :"
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.EnterMoveNextControl = True
        Me.FNHSysStyleId.Location = New System.Drawing.Point(135, 4)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "89", Nothing, True)})
        Me.FNHSysStyleId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(125, 20)
        Me.FNHSysStyleId.TabIndex = 451
        Me.FNHSysStyleId.Tag = "2|"
        '
        'FNHSysStyleId_None
        '
        Me.FNHSysStyleId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysStyleId_None.Location = New System.Drawing.Point(262, 5)
        Me.FNHSysStyleId_None.Name = "FNHSysStyleId_None"
        Me.FNHSysStyleId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleId_None.Properties.ReadOnly = True
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(557, 20)
        Me.FNHSysStyleId_None.TabIndex = 450
        Me.FNHSysStyleId_None.Tag = ""
        '
        'FNHSysStyleId_lbl
        '
        Me.FNHSysStyleId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysStyleId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysStyleId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl.Location = New System.Drawing.Point(5, 5)
        Me.FNHSysStyleId_lbl.Name = "FNHSysStyleId_lbl"
        Me.FNHSysStyleId_lbl.Size = New System.Drawing.Size(124, 17)
        Me.FNHSysStyleId_lbl.TabIndex = 449
        Me.FNHSysStyleId_lbl.Tag = "2|"
        Me.FNHSysStyleId_lbl.Text = "Style No :"
        '
        'oGrpdetail
        '
        Me.oGrpdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.oGrpdetail.Location = New System.Drawing.Point(0, 108)
        Me.oGrpdetail.Name = "oGrpdetail"
        Me.oGrpdetail.Size = New System.Drawing.Size(1089, 464)
        Me.oGrpdetail.TabIndex = 3
        Me.oGrpdetail.Tag = "2|"
        Me.oGrpdetail.Text = "Detail"
        '
        'wSewing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1089, 572)
        Me.Controls.Add(Me.oGrpdetail)
        Me.Controls.Add(Me.oCriteria)
        Me.Name = "wSewing"
        Me.Text = "wSewing"
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.oCriteria.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.oGrpInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.oGrpInfo.ResumeLayout(False)
        CType(Me.FNHSysSeasonId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.FNHSysSeasonId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oGrpdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PdfBarController1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents oCriteria As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents oGrpdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents oGrpInfo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNHSysStyleId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysStyleId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PdfBarController1 As DevExpress.XtraPdfViewer.Bars.PdfBarController
    Friend WithEvents FNHSysStyleId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmsavedocument As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmReadDocumentfile As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysSeasonId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysSeasonId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ocmcopy As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysSeasonId As DevExpress.XtraEditors.ButtonEdit
End Class
