<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wPatientStatic
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wPatientStatic))
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager()
        Me.hideContainerTop = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.oCriteria = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FNType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTYear_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNRptType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FTYear = New DevExpress.XtraEditors.DateEdit()
        Me.ogcDetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTGrpType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTTypeofDiseaseCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTTypeofDiseaseName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.c01 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.c02 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.c03 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.c04 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.c05 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.c06 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.c07 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.c08 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.c09 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.c10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.c11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.c12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.FTTypeofDiseaseCode = New DevExpress.XtraEditors.LabelControl()
        Me.FTOpinionCode = New DevExpress.XtraEditors.LabelControl()
        Me.FTAccident = New DevExpress.XtraEditors.LabelControl()
        Me.FTConsulting = New DevExpress.XtraEditors.LabelControl()
        Me.cFNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerTop.SuspendLayout()
        Me.oCriteria.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FNRptType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTYear.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTYear.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
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
        Me.hideContainerTop.Size = New System.Drawing.Size(982, 38)
        '
        'oCriteria
        '
        Me.oCriteria.Controls.Add(Me.DockPanel1_Container)
        Me.oCriteria.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.oCriteria.ID = New System.Guid("e63d9526-f159-46cd-9054-3f217aaf41b6")
        Me.oCriteria.Image = CType(resources.GetObject("oCriteria.Image"), System.Drawing.Image)
        Me.oCriteria.Location = New System.Drawing.Point(0, 0)
        Me.oCriteria.Name = "oCriteria"
        Me.oCriteria.Options.AllowDockBottom = False
        Me.oCriteria.Options.AllowDockFill = False
        Me.oCriteria.Options.FloatOnDblClick = False
        Me.oCriteria.Options.ShowCloseButton = False
        Me.oCriteria.OriginalSize = New System.Drawing.Size(200, 64)
        Me.oCriteria.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.oCriteria.SavedIndex = 0
        Me.oCriteria.Size = New System.Drawing.Size(982, 64)
        Me.oCriteria.Text = "Criteria"
        Me.oCriteria.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FNType_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTYear_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNRptType)
        Me.DockPanel1_Container.Controls.Add(Me.FTYear)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(4, 23)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(974, 37)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FNType_lbl
        '
        Me.FNType_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNType_lbl.Location = New System.Drawing.Point(284, 3)
        Me.FNType_lbl.Name = "FNType_lbl"
        Me.FNType_lbl.Size = New System.Drawing.Size(139, 19)
        Me.FNType_lbl.TabIndex = 390
        Me.FNType_lbl.Tag = "2|"
        Me.FNType_lbl.Text = "Report Type  :"
        '
        'FTYear_lbl
        '
        Me.FTYear_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTYear_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTYear_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTYear_lbl.Location = New System.Drawing.Point(8, 3)
        Me.FTYear_lbl.Name = "FTYear_lbl"
        Me.FTYear_lbl.Size = New System.Drawing.Size(121, 19)
        Me.FTYear_lbl.TabIndex = 390
        Me.FTYear_lbl.Tag = "2|"
        Me.FTYear_lbl.Text = "Of Year :"
        '
        'FNRptType
        '
        Me.FNRptType.Location = New System.Drawing.Point(429, 3)
        Me.FNRptType.Name = "FNRptType"
        Me.FNRptType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNRptType.Properties.Tag = "FNRptType"
        Me.FNRptType.Size = New System.Drawing.Size(117, 20)
        Me.FNRptType.TabIndex = 1
        Me.FNRptType.Tag = "2|"
        '
        'FTYear
        '
        Me.FTYear.EditValue = Nothing
        Me.FTYear.Location = New System.Drawing.Point(135, 3)
        Me.FTYear.Name = "FTYear"
        Me.FTYear.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTYear.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTYear.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI
        Me.FTYear.Properties.DisplayFormat.FormatString = "yyyy"
        Me.FTYear.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FTYear.Properties.EditFormat.FormatString = "yyyy"
        Me.FTYear.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FTYear.Properties.Mask.EditMask = "yyyy"
        Me.FTYear.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTYear.Size = New System.Drawing.Size(128, 20)
        Me.FTYear.TabIndex = 0
        Me.FTYear.Tag = ""
        '
        'ogcDetail
        '
        Me.ogcDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcDetail.Location = New System.Drawing.Point(0, 38)
        Me.ogcDetail.MainView = Me.ogvDetail
        Me.ogcDetail.Name = "ogcDetail"
        Me.ogcDetail.Size = New System.Drawing.Size(982, 528)
        Me.ogcDetail.TabIndex = 2
        Me.ogcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDetail})
        '
        'ogvDetail
        '
        Me.ogvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTGrpType, Me.cFTTypeofDiseaseCode, Me.cFTTypeofDiseaseName, Me.c01, Me.c02, Me.c03, Me.c04, Me.c05, Me.c06, Me.c07, Me.c08, Me.c09, Me.c10, Me.c11, Me.c12, Me.cFNSeq})
        Me.ogvDetail.GridControl = Me.ogcDetail
        Me.ogvDetail.Name = "ogvDetail"
        Me.ogvDetail.OptionsView.ShowGroupPanel = False
        '
        'cFTGrpType
        '
        Me.cFTGrpType.Caption = "FTGrpType"
        Me.cFTGrpType.FieldName = "FTGrpType"
        Me.cFTGrpType.Name = "cFTGrpType"
        Me.cFTGrpType.OptionsColumn.AllowEdit = False
        Me.cFTGrpType.Visible = True
        Me.cFTGrpType.VisibleIndex = 0
        '
        'cFTTypeofDiseaseCode
        '
        Me.cFTTypeofDiseaseCode.Caption = "FTTypeofDiseaseCode"
        Me.cFTTypeofDiseaseCode.FieldName = "FTTypeofDiseaseCode"
        Me.cFTTypeofDiseaseCode.Name = "cFTTypeofDiseaseCode"
        Me.cFTTypeofDiseaseCode.OptionsColumn.AllowEdit = False
        Me.cFTTypeofDiseaseCode.Visible = True
        Me.cFTTypeofDiseaseCode.VisibleIndex = 1
        Me.cFTTypeofDiseaseCode.Width = 93
        '
        'cFTTypeofDiseaseName
        '
        Me.cFTTypeofDiseaseName.Caption = "FTTypeofDiseaseName"
        Me.cFTTypeofDiseaseName.FieldName = "FTTypeofDiseaseName"
        Me.cFTTypeofDiseaseName.Name = "cFTTypeofDiseaseName"
        Me.cFTTypeofDiseaseName.OptionsColumn.AllowEdit = False
        Me.cFTTypeofDiseaseName.Visible = True
        Me.cFTTypeofDiseaseName.VisibleIndex = 2
        Me.cFTTypeofDiseaseName.Width = 93
        '
        'c01
        '
        Me.c01.Caption = "01"
        Me.c01.DisplayFormat.FormatString = "{0:n0}"
        Me.c01.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.c01.FieldName = "01"
        Me.c01.Name = "c01"
        Me.c01.OptionsColumn.AllowEdit = False
        Me.c01.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "01", "{0:n0}")})
        Me.c01.Visible = True
        Me.c01.VisibleIndex = 3
        Me.c01.Width = 93
        '
        'c02
        '
        Me.c02.Caption = "02"
        Me.c02.DisplayFormat.FormatString = "{0:n0}"
        Me.c02.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.c02.FieldName = "02"
        Me.c02.Name = "c02"
        Me.c02.OptionsColumn.AllowEdit = False
        Me.c02.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "02", "{0:n0}")})
        Me.c02.Visible = True
        Me.c02.VisibleIndex = 4
        Me.c02.Width = 93
        '
        'c03
        '
        Me.c03.Caption = "03"
        Me.c03.DisplayFormat.FormatString = "{0:n0}"
        Me.c03.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.c03.FieldName = "03"
        Me.c03.Name = "c03"
        Me.c03.OptionsColumn.AllowEdit = False
        Me.c03.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "03", "{0:n0}")})
        Me.c03.Visible = True
        Me.c03.VisibleIndex = 5
        Me.c03.Width = 93
        '
        'c04
        '
        Me.c04.Caption = "04"
        Me.c04.DisplayFormat.FormatString = "{0:n0}"
        Me.c04.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.c04.FieldName = "04"
        Me.c04.Name = "c04"
        Me.c04.OptionsColumn.AllowEdit = False
        Me.c04.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "04", "{0:n0}")})
        Me.c04.Visible = True
        Me.c04.VisibleIndex = 6
        Me.c04.Width = 93
        '
        'c05
        '
        Me.c05.Caption = "05"
        Me.c05.DisplayFormat.FormatString = "{0:n0}"
        Me.c05.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.c05.FieldName = "05"
        Me.c05.Name = "c05"
        Me.c05.OptionsColumn.AllowEdit = False
        Me.c05.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "05", "{0:n0}")})
        Me.c05.Visible = True
        Me.c05.VisibleIndex = 7
        Me.c05.Width = 93
        '
        'c06
        '
        Me.c06.Caption = "06"
        Me.c06.DisplayFormat.FormatString = "{0:n0}"
        Me.c06.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.c06.FieldName = "06"
        Me.c06.Name = "c06"
        Me.c06.OptionsColumn.AllowEdit = False
        Me.c06.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "06", "{0:n0}")})
        Me.c06.Visible = True
        Me.c06.VisibleIndex = 8
        Me.c06.Width = 93
        '
        'c07
        '
        Me.c07.Caption = "07"
        Me.c07.DisplayFormat.FormatString = "{0:n0}"
        Me.c07.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.c07.FieldName = "07"
        Me.c07.Name = "c07"
        Me.c07.OptionsColumn.AllowEdit = False
        Me.c07.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "07", "{0:n0}")})
        Me.c07.Visible = True
        Me.c07.VisibleIndex = 9
        Me.c07.Width = 93
        '
        'c08
        '
        Me.c08.Caption = "08"
        Me.c08.DisplayFormat.FormatString = "{0:n0}"
        Me.c08.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.c08.FieldName = "08"
        Me.c08.Name = "c08"
        Me.c08.OptionsColumn.AllowEdit = False
        Me.c08.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "08", "{0:n0}")})
        Me.c08.Visible = True
        Me.c08.VisibleIndex = 10
        Me.c08.Width = 93
        '
        'c09
        '
        Me.c09.Caption = "09"
        Me.c09.DisplayFormat.FormatString = "{0:n0}"
        Me.c09.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.c09.FieldName = "09"
        Me.c09.Name = "c09"
        Me.c09.OptionsColumn.AllowEdit = False
        Me.c09.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "09", "{0:n0}")})
        Me.c09.Visible = True
        Me.c09.VisibleIndex = 11
        Me.c09.Width = 93
        '
        'c10
        '
        Me.c10.Caption = "10"
        Me.c10.DisplayFormat.FormatString = "{0:n0}"
        Me.c10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.c10.FieldName = "10"
        Me.c10.Name = "c10"
        Me.c10.OptionsColumn.AllowEdit = False
        Me.c10.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "10", "{0:n0}")})
        Me.c10.Visible = True
        Me.c10.VisibleIndex = 12
        Me.c10.Width = 93
        '
        'c11
        '
        Me.c11.Caption = "11"
        Me.c11.DisplayFormat.FormatString = "{0:n0}"
        Me.c11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.c11.FieldName = "11"
        Me.c11.Name = "c11"
        Me.c11.OptionsColumn.AllowEdit = False
        Me.c11.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "11", "{0:n0}")})
        Me.c11.Visible = True
        Me.c11.VisibleIndex = 13
        Me.c11.Width = 93
        '
        'c12
        '
        Me.c12.Caption = "12"
        Me.c12.DisplayFormat.FormatString = "{0:n0}"
        Me.c12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.c12.FieldName = "12"
        Me.c12.Name = "c12"
        Me.c12.OptionsColumn.AllowEdit = False
        Me.c12.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "12", "{0:n0}")})
        Me.c12.Visible = True
        Me.c12.VisibleIndex = 14
        Me.c12.Width = 95
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(24, 260)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(935, 47)
        Me.ogbmainprocbutton.TabIndex = 387
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(409, 12)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(117, 23)
        Me.ocmsavelayout.TabIndex = 332
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(822, 11)
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
        'FTTypeofDiseaseCode
        '
        Me.FTTypeofDiseaseCode.Location = New System.Drawing.Point(57, 172)
        Me.FTTypeofDiseaseCode.Name = "FTTypeofDiseaseCode"
        Me.FTTypeofDiseaseCode.Size = New System.Drawing.Size(108, 13)
        Me.FTTypeofDiseaseCode.TabIndex = 389
        Me.FTTypeofDiseaseCode.Text = "FTTypeofDiseaseCode"
        Me.FTTypeofDiseaseCode.Visible = False
        '
        'FTOpinionCode
        '
        Me.FTOpinionCode.Location = New System.Drawing.Point(177, 172)
        Me.FTOpinionCode.Name = "FTOpinionCode"
        Me.FTOpinionCode.Size = New System.Drawing.Size(73, 13)
        Me.FTOpinionCode.TabIndex = 389
        Me.FTOpinionCode.Text = "FTOpinionCode"
        Me.FTOpinionCode.Visible = False
        '
        'FTAccident
        '
        Me.FTAccident.Location = New System.Drawing.Point(278, 172)
        Me.FTAccident.Name = "FTAccident"
        Me.FTAccident.Size = New System.Drawing.Size(41, 13)
        Me.FTAccident.TabIndex = 389
        Me.FTAccident.Text = "Accident"
        Me.FTAccident.Visible = False
        '
        'FTConsulting
        '
        Me.FTConsulting.Location = New System.Drawing.Point(339, 172)
        Me.FTConsulting.Name = "FTConsulting"
        Me.FTConsulting.Size = New System.Drawing.Size(50, 13)
        Me.FTConsulting.TabIndex = 389
        Me.FTConsulting.Text = "Consulting"
        Me.FTConsulting.Visible = False
        '
        'cFNSeq
        '
        Me.cFNSeq.Caption = "FNSeq"
        Me.cFNSeq.FieldName = "Seq"
        Me.cFNSeq.Name = "cFNSeq"
        '
        'wPatientStatic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(982, 566)
        Me.Controls.Add(Me.FTConsulting)
        Me.Controls.Add(Me.FTAccident)
        Me.Controls.Add(Me.FTOpinionCode)
        Me.Controls.Add(Me.FTTypeofDiseaseCode)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogcDetail)
        Me.Controls.Add(Me.hideContainerTop)
        Me.Name = "wPatientStatic"
        Me.Text = "wPatientStatic"
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerTop.ResumeLayout(False)
        Me.oCriteria.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FNRptType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTYear.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTYear.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents hideContainerTop As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents oCriteria As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ogcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cFTTypeofDiseaseCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTTypeofDiseaseName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents c01 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents c02 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents c03 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents c04 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents c05 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents c06 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents c07 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents c08 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents c09 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents c10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents c11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents c12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNRptType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FTYear As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FNType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTYear_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTOpinionCode As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTTypeofDiseaseCode As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cFTGrpType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAccident As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTConsulting As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cFNSeq As DevExpress.XtraGrid.Columns.GridColumn
End Class
