<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wStockBalanceMonthlyAcc
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wStockBalanceMonthlyAcc))
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTYear = New DevExpress.XtraEditors.DateEdit()
        Me.IcAppCondition1 = New HI.UCTR.ICAppCondition()
        Me.FTYear_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogdtime = New DevExpress.XtraGrid.GridControl()
        Me.ogvtime = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTDocType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNGrpSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMonth01 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMonth02 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMonth03 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMonth04 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMonth05 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMonth06 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMonth07 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMonth08 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMonth09 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMonth10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMonth11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMonth12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTotal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTYear.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTYear.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbheader
        '
        Me.ogbheader.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.ogbheader.Appearance.Options.UseForeColor = True
        Me.ogbheader.Appearance.Options.UseTextOptions = True
        Me.ogbheader.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogbheader.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.ogbheader.Controls.Add(Me.DockPanel1_Container)
        Me.ogbheader.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ogbheader.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.DockVertical = DevExpress.Utils.DefaultBoolean.[False]
        Me.ogbheader.ID = New System.Guid("77b9346d-8d15-4323-af1e-af82afa9902a")
        Me.ogbheader.Image = CType(resources.GetObject("ogbheader.Image"), System.Drawing.Image)
        Me.ogbheader.Location = New System.Drawing.Point(0, 0)
        Me.ogbheader.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.FloatOnDblClick = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.Options.ShowMaximizeButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(1326, 249)
        Me.ogbheader.Size = New System.Drawing.Size(1326, 249)
        Me.ogbheader.TabStop = False
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTYear)
        Me.DockPanel1_Container.Controls.Add(Me.IcAppCondition1)
        Me.DockPanel1_Container.Controls.Add(Me.FTYear_lbl)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(3, 32)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1320, 212)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTYear
        '
        Me.FTYear.EditValue = Nothing
        Me.FTYear.EnterMoveNextControl = True
        Me.FTYear.Location = New System.Drawing.Point(180, 5)
        Me.FTYear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTYear.Name = "FTYear"
        Me.FTYear.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTYear.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTYear.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTYear.Properties.DisplayFormat.FormatString = "MM/yyyy"
        Me.FTYear.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTYear.Properties.EditFormat.FormatString = "MM/yyyy"
        Me.FTYear.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTYear.Properties.Mask.EditMask = "MM/yyyy"
        Me.FTYear.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FTYear.Properties.NullDate = ""
        Me.FTYear.Size = New System.Drawing.Size(131, 23)
        Me.FTYear.TabIndex = 478
        Me.FTYear.Tag = "2|"
        '
        'IcAppCondition1
        '
        Me.IcAppCondition1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IcAppCondition1.Location = New System.Drawing.Point(341, 1)
        Me.IcAppCondition1.Margin = New System.Windows.Forms.Padding(4)
        Me.IcAppCondition1.Name = "IcAppCondition1"
        Me.IcAppCondition1.ShowmColorCode = False
        Me.IcAppCondition1.ShowmItemCode = False
        Me.IcAppCondition1.ShowmSizeCode = False
        Me.IcAppCondition1.ShowmSupl = False
        Me.IcAppCondition1.ShowWHNo = True
        Me.IcAppCondition1.Size = New System.Drawing.Size(975, 206)
        Me.IcAppCondition1.TabIndex = 483
        '
        'FTYear_lbl
        '
        Me.FTYear_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTYear_lbl.Appearance.Options.UseForeColor = True
        Me.FTYear_lbl.Appearance.Options.UseTextOptions = True
        Me.FTYear_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTYear_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTYear_lbl.Location = New System.Drawing.Point(8, 4)
        Me.FTYear_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTYear_lbl.Name = "FTYear_lbl"
        Me.FTYear_lbl.Size = New System.Drawing.Size(166, 23)
        Me.FTYear_lbl.TabIndex = 482
        Me.FTYear_lbl.Tag = "2|"
        Me.FTYear_lbl.Text = "As Of Monthly :"
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbdetail.Controls.Add(Me.ogdtime)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 249)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.ShowCaption = False
        Me.ogbdetail.Size = New System.Drawing.Size(1326, 530)
        Me.ogbdetail.TabIndex = 0
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(106, 135)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1091, 58)
        Me.ogbmainprocbutton.TabIndex = 386
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(477, 15)
        Me.ocmsavelayout.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(136, 28)
        Me.ocmsavelayout.TabIndex = 332
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(959, 14)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(16, 12)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(111, 31)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(134, 15)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(136, 28)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'ogdtime
        '
        Me.ogdtime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdtime.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdtime.Location = New System.Drawing.Point(2, 2)
        Me.ogdtime.MainView = Me.ogvtime
        Me.ogdtime.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdtime.Name = "ogdtime"
        Me.ogdtime.Size = New System.Drawing.Size(1322, 526)
        Me.ogdtime.TabIndex = 0
        Me.ogdtime.TabStop = False
        Me.ogdtime.Tag = "2|"
        Me.ogdtime.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvtime})
        '
        'ogvtime
        '
        Me.ogvtime.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTDocType, Me.FNGrpSeq, Me.FNSeq, Me.FNMonth01, Me.FNMonth02, Me.FNMonth03, Me.FNMonth04, Me.FNMonth05, Me.FNMonth06, Me.FNMonth07, Me.FNMonth08, Me.FNMonth09, Me.FNMonth10, Me.FNMonth11, Me.FNMonth12, Me.FNTotal})
        Me.ogvtime.GridControl = Me.ogdtime
        Me.ogvtime.Name = "ogvtime"
        Me.ogvtime.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvtime.OptionsView.ColumnAutoWidth = False
        Me.ogvtime.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvtime.OptionsView.ShowGroupPanel = False
        Me.ogvtime.Tag = "2|"
        '
        'FTDocType
        '
        Me.FTDocType.Caption = "รายการ"
        Me.FTDocType.FieldName = "FTDocType"
        Me.FTDocType.Name = "FTDocType"
        Me.FTDocType.OptionsColumn.AllowEdit = False
        Me.FTDocType.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDocType.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDocType.OptionsColumn.AllowMove = False
        Me.FTDocType.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDocType.OptionsColumn.ReadOnly = True
        Me.FTDocType.Visible = True
        Me.FTDocType.VisibleIndex = 0
        Me.FTDocType.Width = 130
        '
        'FNGrpSeq
        '
        Me.FNGrpSeq.Caption = "FNGrpSeq"
        Me.FNGrpSeq.FieldName = "FNGrpSeq"
        Me.FNGrpSeq.Name = "FNGrpSeq"
        Me.FNGrpSeq.OptionsColumn.AllowEdit = False
        Me.FNGrpSeq.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNGrpSeq.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNGrpSeq.OptionsColumn.AllowMove = False
        Me.FNGrpSeq.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNGrpSeq.OptionsColumn.ReadOnly = True
        '
        'FNSeq
        '
        Me.FNSeq.Caption = "FNSeq"
        Me.FNSeq.FieldName = "FNSeq"
        Me.FNSeq.Name = "FNSeq"
        Me.FNSeq.OptionsColumn.AllowEdit = False
        Me.FNSeq.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSeq.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSeq.OptionsColumn.AllowMove = False
        Me.FNSeq.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSeq.OptionsColumn.ReadOnly = True
        '
        'FNMonth01
        '
        Me.FNMonth01.Caption = "มกราคม"
        Me.FNMonth01.DisplayFormat.FormatString = "{0:n2}"
        Me.FNMonth01.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNMonth01.FieldName = "FNMonth01"
        Me.FNMonth01.Name = "FNMonth01"
        Me.FNMonth01.OptionsColumn.AllowEdit = False
        Me.FNMonth01.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth01.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth01.OptionsColumn.AllowMove = False
        Me.FNMonth01.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth01.OptionsColumn.ReadOnly = True
        Me.FNMonth01.Visible = True
        Me.FNMonth01.VisibleIndex = 1
        Me.FNMonth01.Width = 100
        '
        'FNMonth02
        '
        Me.FNMonth02.Caption = "กุมภาพันธ์"
        Me.FNMonth02.DisplayFormat.FormatString = "{0:n2}"
        Me.FNMonth02.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNMonth02.FieldName = "FNMonth02"
        Me.FNMonth02.Name = "FNMonth02"
        Me.FNMonth02.OptionsColumn.AllowEdit = False
        Me.FNMonth02.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth02.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth02.OptionsColumn.AllowMove = False
        Me.FNMonth02.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth02.OptionsColumn.ReadOnly = True
        Me.FNMonth02.Visible = True
        Me.FNMonth02.VisibleIndex = 2
        Me.FNMonth02.Width = 100
        '
        'FNMonth03
        '
        Me.FNMonth03.Caption = "มีนาคม"
        Me.FNMonth03.DisplayFormat.FormatString = "{0:n2}"
        Me.FNMonth03.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNMonth03.FieldName = "FNMonth03"
        Me.FNMonth03.Name = "FNMonth03"
        Me.FNMonth03.OptionsColumn.AllowEdit = False
        Me.FNMonth03.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth03.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth03.OptionsColumn.AllowMove = False
        Me.FNMonth03.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth03.OptionsColumn.ReadOnly = True
        Me.FNMonth03.Visible = True
        Me.FNMonth03.VisibleIndex = 3
        Me.FNMonth03.Width = 100
        '
        'FNMonth04
        '
        Me.FNMonth04.Caption = "เมษายน"
        Me.FNMonth04.DisplayFormat.FormatString = "{0:n2}"
        Me.FNMonth04.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNMonth04.FieldName = "FNMonth04"
        Me.FNMonth04.Name = "FNMonth04"
        Me.FNMonth04.OptionsColumn.AllowEdit = False
        Me.FNMonth04.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth04.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth04.OptionsColumn.AllowMove = False
        Me.FNMonth04.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth04.OptionsColumn.ReadOnly = True
        Me.FNMonth04.Visible = True
        Me.FNMonth04.VisibleIndex = 4
        Me.FNMonth04.Width = 100
        '
        'FNMonth05
        '
        Me.FNMonth05.Caption = "พฤษภาคม"
        Me.FNMonth05.DisplayFormat.FormatString = "{0:n2}"
        Me.FNMonth05.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNMonth05.FieldName = "FNMonth05"
        Me.FNMonth05.Name = "FNMonth05"
        Me.FNMonth05.OptionsColumn.AllowEdit = False
        Me.FNMonth05.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth05.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth05.OptionsColumn.AllowMove = False
        Me.FNMonth05.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth05.OptionsColumn.ReadOnly = True
        Me.FNMonth05.Visible = True
        Me.FNMonth05.VisibleIndex = 5
        Me.FNMonth05.Width = 100
        '
        'FNMonth06
        '
        Me.FNMonth06.Caption = "มิถุนายน"
        Me.FNMonth06.DisplayFormat.FormatString = "{0:n2}"
        Me.FNMonth06.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNMonth06.FieldName = "FNMonth06"
        Me.FNMonth06.Name = "FNMonth06"
        Me.FNMonth06.OptionsColumn.AllowEdit = False
        Me.FNMonth06.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth06.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth06.OptionsColumn.AllowMove = False
        Me.FNMonth06.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth06.OptionsColumn.ReadOnly = True
        Me.FNMonth06.Visible = True
        Me.FNMonth06.VisibleIndex = 6
        Me.FNMonth06.Width = 100
        '
        'FNMonth07
        '
        Me.FNMonth07.Caption = "กรกฎาคม"
        Me.FNMonth07.DisplayFormat.FormatString = "{0:n2}"
        Me.FNMonth07.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNMonth07.FieldName = "FNMonth07"
        Me.FNMonth07.Name = "FNMonth07"
        Me.FNMonth07.OptionsColumn.AllowEdit = False
        Me.FNMonth07.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth07.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth07.OptionsColumn.AllowMove = False
        Me.FNMonth07.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth07.OptionsColumn.ReadOnly = True
        Me.FNMonth07.Visible = True
        Me.FNMonth07.VisibleIndex = 7
        Me.FNMonth07.Width = 100
        '
        'FNMonth08
        '
        Me.FNMonth08.Caption = "สิงหาคม"
        Me.FNMonth08.DisplayFormat.FormatString = "{0:n2}"
        Me.FNMonth08.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNMonth08.FieldName = "FNMonth08"
        Me.FNMonth08.Name = "FNMonth08"
        Me.FNMonth08.OptionsColumn.AllowEdit = False
        Me.FNMonth08.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth08.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth08.OptionsColumn.AllowMove = False
        Me.FNMonth08.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth08.OptionsColumn.ReadOnly = True
        Me.FNMonth08.Visible = True
        Me.FNMonth08.VisibleIndex = 8
        Me.FNMonth08.Width = 100
        '
        'FNMonth09
        '
        Me.FNMonth09.Caption = "กันยายน"
        Me.FNMonth09.DisplayFormat.FormatString = "{0:n2}"
        Me.FNMonth09.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNMonth09.FieldName = "FNMonth09"
        Me.FNMonth09.Name = "FNMonth09"
        Me.FNMonth09.OptionsColumn.AllowEdit = False
        Me.FNMonth09.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth09.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth09.OptionsColumn.AllowMove = False
        Me.FNMonth09.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth09.OptionsColumn.ReadOnly = True
        Me.FNMonth09.Visible = True
        Me.FNMonth09.VisibleIndex = 9
        Me.FNMonth09.Width = 100
        '
        'FNMonth10
        '
        Me.FNMonth10.Caption = "ตุลาคม"
        Me.FNMonth10.DisplayFormat.FormatString = "{0:n2}"
        Me.FNMonth10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNMonth10.FieldName = "FNMonth10"
        Me.FNMonth10.Name = "FNMonth10"
        Me.FNMonth10.OptionsColumn.AllowEdit = False
        Me.FNMonth10.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth10.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth10.OptionsColumn.AllowMove = False
        Me.FNMonth10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth10.OptionsColumn.ReadOnly = True
        Me.FNMonth10.Visible = True
        Me.FNMonth10.VisibleIndex = 10
        Me.FNMonth10.Width = 100
        '
        'FNMonth11
        '
        Me.FNMonth11.Caption = "พฤศจิกายน"
        Me.FNMonth11.DisplayFormat.FormatString = "{0:n2}"
        Me.FNMonth11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNMonth11.FieldName = "FNMonth11"
        Me.FNMonth11.Name = "FNMonth11"
        Me.FNMonth11.OptionsColumn.AllowEdit = False
        Me.FNMonth11.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth11.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth11.OptionsColumn.AllowMove = False
        Me.FNMonth11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth11.OptionsColumn.ReadOnly = True
        Me.FNMonth11.Visible = True
        Me.FNMonth11.VisibleIndex = 11
        Me.FNMonth11.Width = 100
        '
        'FNMonth12
        '
        Me.FNMonth12.Caption = "ธันวาคม"
        Me.FNMonth12.DisplayFormat.FormatString = "{0:n2}"
        Me.FNMonth12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNMonth12.FieldName = "FNMonth12"
        Me.FNMonth12.Name = "FNMonth12"
        Me.FNMonth12.OptionsColumn.AllowEdit = False
        Me.FNMonth12.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth12.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth12.OptionsColumn.AllowMove = False
        Me.FNMonth12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNMonth12.OptionsColumn.ReadOnly = True
        Me.FNMonth12.Visible = True
        Me.FNMonth12.VisibleIndex = 12
        Me.FNMonth12.Width = 100
        '
        'FNTotal
        '
        Me.FNTotal.Caption = "รวม"
        Me.FNTotal.DisplayFormat.FormatString = "{0:n2}"
        Me.FNTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNTotal.FieldName = "FNTotal"
        Me.FNTotal.Name = "FNTotal"
        Me.FNTotal.OptionsColumn.AllowEdit = False
        Me.FNTotal.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNTotal.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNTotal.OptionsColumn.AllowMove = False
        Me.FNTotal.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNTotal.OptionsColumn.ReadOnly = True
        Me.FNTotal.Visible = True
        Me.FNTotal.VisibleIndex = 13
        Me.FNTotal.Width = 120
        '
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.ogbheader})
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'wStockBalanceMonthlyAcc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1326, 779)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wStockBalanceMonthlyAcc"
        Me.Text = "Stock Balance Monthly For Account"
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTYear.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTYear.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogdtime As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvtime As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents oDockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTYear_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents IcAppCondition1 As HI.UCTR.ICAppCondition
    Friend WithEvents FTYear As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTDocType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNGrpSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMonth01 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMonth02 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMonth03 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMonth04 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMonth05 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMonth06 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMonth07 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMonth08 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMonth09 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMonth10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMonth11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMonth12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTotal As DevExpress.XtraGrid.Columns.GridColumn
End Class
