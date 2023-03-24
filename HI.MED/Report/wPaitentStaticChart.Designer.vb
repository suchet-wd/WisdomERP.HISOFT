<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wPaitentStaticChart
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wPaitentStaticChart))
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager()
        Me.hideContainerTop = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.oCriteria = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FNType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTYear = New DevExpress.XtraEditors.DateEdit()
        Me.FTYear_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNRptType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogrpchartBar = New DevExpress.XtraEditors.GroupControl()
        Me.oGrpChartPie = New DevExpress.XtraEditors.GroupControl()
        Me.ogrpTop = New DevExpress.XtraEditors.GroupControl()
        Me.ogvTop = New DevExpress.XtraGrid.GridControl()
        Me.ogcTop = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTTypeofDiseaseName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerTop.SuspendLayout()
        Me.oCriteria.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTYear.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTYear.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNRptType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogrpchartBar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oGrpChartPie, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogrpTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpTop.SuspendLayout()
        CType(Me.ogvTop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcTop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DockManager1
        '
        Me.DockManager1.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerTop})
        Me.DockManager1.Form = Me
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'hideContainerTop
        '
        Me.hideContainerTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.hideContainerTop.Controls.Add(Me.oCriteria)
        Me.hideContainerTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.hideContainerTop.Location = New System.Drawing.Point(0, 0)
        Me.hideContainerTop.Name = "hideContainerTop"
        Me.hideContainerTop.Size = New System.Drawing.Size(1014, 38)
        '
        'oCriteria
        '
        Me.oCriteria.Controls.Add(Me.DockPanel1_Container)
        Me.oCriteria.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.oCriteria.ID = New System.Guid("fd36e52e-cf4b-4f4b-8136-8c5cc000a1b6")
        Me.oCriteria.Image = CType(resources.GetObject("oCriteria.Image"), System.Drawing.Image)
        Me.oCriteria.Location = New System.Drawing.Point(0, 0)
        Me.oCriteria.Name = "oCriteria"
        Me.oCriteria.Options.AllowDockBottom = False
        Me.oCriteria.Options.AllowDockFill = False
        Me.oCriteria.Options.FloatOnDblClick = False
        Me.oCriteria.Options.ShowCloseButton = False
        Me.oCriteria.OriginalSize = New System.Drawing.Size(200, 62)
        Me.oCriteria.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.oCriteria.SavedIndex = 0
        Me.oCriteria.Size = New System.Drawing.Size(1014, 62)
        Me.oCriteria.Text = "Criteria"
        Me.oCriteria.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FNType_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTYear)
        Me.DockPanel1_Container.Controls.Add(Me.FTYear_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNRptType)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 23)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1006, 35)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FNType_lbl
        '
        Me.FNType_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNType_lbl.Location = New System.Drawing.Point(280, 3)
        Me.FNType_lbl.Name = "FNType_lbl"
        Me.FNType_lbl.Size = New System.Drawing.Size(139, 19)
        Me.FNType_lbl.TabIndex = 393
        Me.FNType_lbl.Tag = "2|"
        Me.FNType_lbl.Text = "Report Type  :"
        '
        'FTYear
        '
        Me.FTYear.EditValue = Nothing
        Me.FTYear.Location = New System.Drawing.Point(131, 3)
        Me.FTYear.Name = "FTYear"
        Me.FTYear.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTYear.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTYear.Properties.CalendarTimeProperties.HideSelection = False
        Me.FTYear.Properties.CalendarTimeProperties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.DoubleClick
        Me.FTYear.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI
        Me.FTYear.Properties.DisplayFormat.FormatString = "MM/yyyy"
        Me.FTYear.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FTYear.Properties.EditFormat.FormatString = "MM/yyyy"
        Me.FTYear.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FTYear.Properties.Mask.EditMask = "MM/yyyy"
        Me.FTYear.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.DoubleClick
        Me.FTYear.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTYear.Size = New System.Drawing.Size(128, 20)
        Me.FTYear.TabIndex = 391
        Me.FTYear.Tag = ""
        '
        'FTYear_lbl
        '
        Me.FTYear_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTYear_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTYear_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTYear_lbl.Location = New System.Drawing.Point(4, 3)
        Me.FTYear_lbl.Name = "FTYear_lbl"
        Me.FTYear_lbl.Size = New System.Drawing.Size(121, 19)
        Me.FTYear_lbl.TabIndex = 394
        Me.FTYear_lbl.Tag = "2|"
        Me.FTYear_lbl.Text = "Of Year :"
        '
        'FNRptType
        '
        Me.FNRptType.Location = New System.Drawing.Point(425, 3)
        Me.FNRptType.Name = "FNRptType"
        Me.FNRptType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNRptType.Properties.Tag = "FNRptType"
        Me.FNRptType.Size = New System.Drawing.Size(117, 20)
        Me.FNRptType.TabIndex = 392
        Me.FNRptType.Tag = "2|"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(24, 57)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(185, 65)
        Me.ogbmainprocbutton.TabIndex = 0
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(87, 35)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(106, 25)
        Me.ocmpreview.TabIndex = 336
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "Preview"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(238, 11)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(61, 23)
        Me.ocmsavelayout.TabIndex = 332
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(14, 35)
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
        'ogrpchartBar
        '
        Me.ogrpchartBar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrpchartBar.Location = New System.Drawing.Point(217, 2)
        Me.ogrpchartBar.Name = "ogrpchartBar"
        Me.ogrpchartBar.ShowCaption = False
        Me.ogrpchartBar.Size = New System.Drawing.Size(791, 270)
        Me.ogrpchartBar.TabIndex = 2
        Me.ogrpchartBar.Text = "GroupControl1"
        '
        'oGrpChartPie
        '
        Me.oGrpChartPie.Dock = System.Windows.Forms.DockStyle.Fill
        Me.oGrpChartPie.Location = New System.Drawing.Point(2, 276)
        Me.oGrpChartPie.Name = "oGrpChartPie"
        Me.oGrpChartPie.ShowCaption = False
        Me.oGrpChartPie.Size = New System.Drawing.Size(1010, 293)
        Me.oGrpChartPie.TabIndex = 3
        Me.oGrpChartPie.Text = "GroupControl2"
        '
        'ogrpTop
        '
        Me.ogrpTop.Controls.Add(Me.ogbmainprocbutton)
        Me.ogrpTop.Controls.Add(Me.ogvTop)
        Me.ogrpTop.Dock = System.Windows.Forms.DockStyle.Left
        Me.ogrpTop.Location = New System.Drawing.Point(2, 2)
        Me.ogrpTop.Name = "ogrpTop"
        Me.ogrpTop.Size = New System.Drawing.Size(215, 270)
        Me.ogrpTop.TabIndex = 5
        Me.ogrpTop.Text = "Top Three Of Month"
        '
        'ogvTop
        '
        Me.ogvTop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogvTop.Location = New System.Drawing.Point(2, 21)
        Me.ogvTop.MainView = Me.ogcTop
        Me.ogvTop.Name = "ogvTop"
        Me.ogvTop.Size = New System.Drawing.Size(211, 247)
        Me.ogvTop.TabIndex = 0
        Me.ogvTop.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogcTop})
        '
        'ogcTop
        '
        Me.ogcTop.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFNSeq, Me.cFTTypeofDiseaseName})
        Me.ogcTop.GridControl = Me.ogvTop
        Me.ogcTop.Name = "ogcTop"
        Me.ogcTop.OptionsFilter.AllowFilterEditor = False
        Me.ogcTop.OptionsView.ShowGroupPanel = False
        '
        'cFNSeq
        '
        Me.cFNSeq.Caption = "FNSeq"
        Me.cFNSeq.FieldName = "FNSeq"
        Me.cFNSeq.Name = "cFNSeq"
        Me.cFNSeq.OptionsColumn.AllowEdit = False
        Me.cFNSeq.Width = 20
        '
        'cFTTypeofDiseaseName
        '
        Me.cFTTypeofDiseaseName.AppearanceCell.ForeColor = System.Drawing.Color.RoyalBlue
        Me.cFTTypeofDiseaseName.AppearanceCell.Options.UseForeColor = True
        Me.cFTTypeofDiseaseName.Caption = "FTTypeofDiseaseName"
        Me.cFTTypeofDiseaseName.FieldName = "FTTypeofDiseaseName"
        Me.cFTTypeofDiseaseName.Name = "cFTTypeofDiseaseName"
        Me.cFTTypeofDiseaseName.OptionsColumn.AllowEdit = False
        Me.cFTTypeofDiseaseName.Visible = True
        Me.cFTTypeofDiseaseName.VisibleIndex = 0
        Me.cFTTypeofDiseaseName.Width = 175
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.ogrpchartBar)
        Me.GroupControl1.Controls.Add(Me.ogrpTop)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl1.Location = New System.Drawing.Point(2, 2)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.ShowCaption = False
        Me.GroupControl1.Size = New System.Drawing.Size(1010, 274)
        Me.GroupControl1.TabIndex = 7
        Me.GroupControl1.Text = "GroupControl1"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.oGrpChartPie)
        Me.GroupControl2.Controls.Add(Me.GroupControl1)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 38)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.ShowCaption = False
        Me.GroupControl2.Size = New System.Drawing.Size(1014, 571)
        Me.GroupControl2.TabIndex = 9
        Me.GroupControl2.Text = "GroupControl2"
        '
        'wPaitentStaticChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1014, 609)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.hideContainerTop)
        Me.Name = "wPaitentStaticChart"
        Me.Text = "wPaitentStaticChart"
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerTop.ResumeLayout(False)
        Me.oCriteria.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTYear.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTYear.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNRptType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogrpchartBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oGrpChartPie, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogrpTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpTop.ResumeLayout(False)
        CType(Me.ogvTop, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcTop, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents oCriteria As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents hideContainerTop As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents oGrpChartPie As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogrpchartBar As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTYear As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTYear_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNRptType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogrpTop As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogvTop As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogcTop As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTTypeofDiseaseName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
End Class
