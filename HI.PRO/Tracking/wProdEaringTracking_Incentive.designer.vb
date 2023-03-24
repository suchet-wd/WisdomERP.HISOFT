<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wProdEaringTracking_Incentive
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wProdEaringTracking_Incentive))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTToDate = New DevExpress.XtraEditors.DateEdit()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.FTToDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FTStartDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTStartDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogcdetailcolorsizeline = New DevExpress.XtraTab.XtraTabControl()
        Me.otbEarning = New DevExpress.XtraTab.XtraTabPage()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysUnitSectId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTUnitSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCalDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNEEFQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNGradeLevel = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNIncentiveAmtPaid = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTToDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTToDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogcdetailcolorsizeline, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogcdetailcolorsizeline.SuspendLayout()
        Me.otbEarning.SuspendLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogbheader.OriginalSize = New System.Drawing.Size(1326, 108)
        Me.ogbheader.Size = New System.Drawing.Size(1326, 108)
        Me.ogbheader.TabStop = False
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTToDate)
        Me.DockPanel1_Container.Controls.Add(Me.ogbmainprocbutton)
        Me.DockPanel1_Container.Controls.Add(Me.FTToDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(5, 27)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1316, 74)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTToDate
        '
        Me.FTToDate.EditValue = Nothing
        Me.FTToDate.EnterMoveNextControl = True
        Me.FTToDate.Location = New System.Drawing.Point(418, 34)
        Me.FTToDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTToDate.Name = "FTToDate"
        Me.FTToDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTToDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTToDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTToDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTToDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTToDate.Properties.NullDate = ""
        Me.FTToDate.Size = New System.Drawing.Size(152, 22)
        Me.FTToDate.TabIndex = 397
        Me.FTToDate.Tag = "2|"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdelete)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(473, 4)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(836, 58)
        Me.ogbmainprocbutton.TabIndex = 387
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(433, 15)
        Me.ocmsavelayout.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(136, 28)
        Me.ocmsavelayout.TabIndex = 332
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(704, 14)
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
        'ocmdelete
        '
        Me.ocmdelete.Location = New System.Drawing.Point(589, 17)
        Me.ocmdelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(136, 28)
        Me.ocmdelete.TabIndex = 329
        Me.ocmdelete.Text = "Delete"
        Me.ocmdelete.Visible = False
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(275, 16)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(136, 28)
        Me.ocmsave.TabIndex = 329
        Me.ocmsave.Text = "SAVE"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(133, 17)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(136, 28)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'FTToDate_lbl
        '
        Me.FTToDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTToDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTToDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTToDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTToDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTToDate_lbl.Location = New System.Drawing.Point(310, 33)
        Me.FTToDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTToDate_lbl.Name = "FTToDate_lbl"
        Me.FTToDate_lbl.Size = New System.Drawing.Size(102, 23)
        Me.FTToDate_lbl.TabIndex = 399
        Me.FTToDate_lbl.Tag = "2|"
        Me.FTToDate_lbl.Text = "To :"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(311, 4)
        Me.FNHSysCmpId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(998, 22)
        Me.FNHSysCmpId_None.TabIndex = 515
        Me.FNHSysCmpId_None.Tag = ""
        '
        'FTStartDate
        '
        Me.FTStartDate.EditValue = Nothing
        Me.FTStartDate.EnterMoveNextControl = True
        Me.FTStartDate.Location = New System.Drawing.Point(163, 34)
        Me.FTStartDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartDate.Name = "FTStartDate"
        Me.FTStartDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.NullDate = ""
        Me.FTStartDate.Size = New System.Drawing.Size(141, 22)
        Me.FTStartDate.TabIndex = 396
        Me.FTStartDate.Tag = "2|"
        '
        'FTStartDate_lbl
        '
        Me.FTStartDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartDate_lbl.Location = New System.Drawing.Point(4, 33)
        Me.FTStartDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartDate_lbl.Name = "FTStartDate_lbl"
        Me.FTStartDate_lbl.Size = New System.Drawing.Size(155, 23)
        Me.FTStartDate_lbl.TabIndex = 398
        Me.FTStartDate_lbl.Tag = "2|"
        Me.FTStartDate_lbl.Text = "Start Date :"
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(16, 4)
        Me.FNHSysCmpId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(143, 21)
        Me.FNHSysCmpId_lbl.TabIndex = 514
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(163, 4)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(141, 22)
        Me.FNHSysCmpId.TabIndex = 513
        Me.FNHSysCmpId.Tag = ""
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ogcdetailcolorsizeline)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 108)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.ShowCaption = False
        Me.ogbdetail.Size = New System.Drawing.Size(1326, 671)
        Me.ogbdetail.TabIndex = 0
        '
        'ogcdetailcolorsizeline
        '
        Me.ogcdetailcolorsizeline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetailcolorsizeline.Location = New System.Drawing.Point(2, 2)
        Me.ogcdetailcolorsizeline.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogcdetailcolorsizeline.Name = "ogcdetailcolorsizeline"
        Me.ogcdetailcolorsizeline.SelectedTabPage = Me.otbEarning
        Me.ogcdetailcolorsizeline.Size = New System.Drawing.Size(1322, 667)
        Me.ogcdetailcolorsizeline.TabIndex = 387
        Me.ogcdetailcolorsizeline.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otbEarning})
        '
        'otbEarning
        '
        Me.otbEarning.Controls.Add(Me.ogcdetail)
        Me.otbEarning.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otbEarning.Name = "otbEarning"
        Me.otbEarning.Size = New System.Drawing.Size(1315, 633)
        Me.otbEarning.Text = "Earning"
        '
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.Size = New System.Drawing.Size(1315, 633)
        Me.ogcdetail.TabIndex = 0
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.cFNHSysUnitSectId, Me.cFTUnitSectCode, Me.cFTCalDate, Me.cFNEEFQty, Me.cFNGradeLevel, Me.cFNIncentiveAmtPaid})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "GridColumn1"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'cFNHSysUnitSectId
        '
        Me.cFNHSysUnitSectId.Caption = "FNHSysUnitSectId"
        Me.cFNHSysUnitSectId.FieldName = "FNHSysUnitSectId"
        Me.cFNHSysUnitSectId.Name = "cFNHSysUnitSectId"
        Me.cFNHSysUnitSectId.OptionsColumn.AllowEdit = False
        Me.cFNHSysUnitSectId.Width = 148
        '
        'cFTUnitSectCode
        '
        Me.cFTUnitSectCode.Caption = "FTUnitSectCode"
        Me.cFTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.cFTUnitSectCode.Name = "cFTUnitSectCode"
        Me.cFTUnitSectCode.OptionsColumn.AllowEdit = False
        Me.cFTUnitSectCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTUnitSectCode.Visible = True
        Me.cFTUnitSectCode.VisibleIndex = 1
        Me.cFTUnitSectCode.Width = 164
        '
        'cFTCalDate
        '
        Me.cFTCalDate.Caption = "FTCalDate"
        Me.cFTCalDate.FieldName = "FTCalDate"
        Me.cFTCalDate.Name = "cFTCalDate"
        Me.cFTCalDate.OptionsColumn.AllowEdit = False
        Me.cFTCalDate.Visible = True
        Me.cFTCalDate.VisibleIndex = 0
        Me.cFTCalDate.Width = 151
        '
        'cFNEEFQty
        '
        Me.cFNEEFQty.Caption = "FNEEFQty"
        Me.cFNEEFQty.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNEEFQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNEEFQty.FieldName = "FNEEFQty"
        Me.cFNEEFQty.Name = "cFNEEFQty"
        Me.cFNEEFQty.OptionsColumn.AllowEdit = False
        Me.cFNEEFQty.Visible = True
        Me.cFNEEFQty.VisibleIndex = 2
        Me.cFNEEFQty.Width = 158
        '
        'cFNGradeLevel
        '
        Me.cFNGradeLevel.Caption = "FNGradeLevel"
        Me.cFNGradeLevel.FieldName = "FNGradeLevel"
        Me.cFNGradeLevel.Name = "cFNGradeLevel"
        Me.cFNGradeLevel.OptionsColumn.AllowEdit = False
        Me.cFNGradeLevel.Visible = True
        Me.cFNGradeLevel.VisibleIndex = 3
        Me.cFNGradeLevel.Width = 135
        '
        'cFNIncentiveAmtPaid
        '
        Me.cFNIncentiveAmtPaid.Caption = "FNIncentiveAmtPaid"
        Me.cFNIncentiveAmtPaid.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNIncentiveAmtPaid.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNIncentiveAmtPaid.FieldName = "FNIncentiveAmtPaid"
        Me.cFNIncentiveAmtPaid.Name = "cFNIncentiveAmtPaid"
        Me.cFNIncentiveAmtPaid.OptionsColumn.AllowEdit = False
        Me.cFNIncentiveAmtPaid.Visible = True
        Me.cFNIncentiveAmtPaid.VisibleIndex = 4
        Me.cFNIncentiveAmtPaid.Width = 157
        '
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.ogbheader})
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'wProdEaringTracking_Incentive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1326, 779)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wProdEaringTracking_Incentive"
        Me.Text = "Production Tracking Earing"
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTToDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTToDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogcdetailcolorsizeline, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogcdetailcolorsizeline.ResumeLayout(False)
        Me.otbEarning.ResumeLayout(False)
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents oDockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ogcdetailcolorsizeline As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otbEarning As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFNHSysUnitSectId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTUnitSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTToDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTToDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCalDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNEEFQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNGradeLevel As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNIncentiveAmtPaid As DevExpress.XtraGrid.Columns.GridColumn
End Class
