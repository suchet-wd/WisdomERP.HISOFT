<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wProdEaringTracking
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wProdEaringTracking))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartDate = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogcdetailcolorsizeline = New DevExpress.XtraTab.XtraTabControl()
        Me.otbEarning = New DevExpress.XtraTab.XtraTabPage()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cEmpCount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysSectId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysUnitSectId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTUnitSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNSam = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCustCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cOTQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNOT1Min = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNTimeMin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTSeasonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNTarget = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNAVEFF = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNNetCM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNBudget = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cEarningperline = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNLostEarnning = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNLostEarnningPer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNTargetOT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
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
        Me.DockPanel1_Container.Controls.Add(Me.ogbmainprocbutton)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDate)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(5, 27)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1316, 74)
        Me.DockPanel1_Container.TabIndex = 0
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
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(427, 4)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(882, 58)
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
        Me.ocmexit.Location = New System.Drawing.Point(750, 14)
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
        'FTStartDate_lbl
        '
        Me.FTStartDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartDate_lbl.Location = New System.Drawing.Point(4, 29)
        Me.FTStartDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartDate_lbl.Name = "FTStartDate_lbl"
        Me.FTStartDate_lbl.Size = New System.Drawing.Size(155, 23)
        Me.FTStartDate_lbl.TabIndex = 511
        Me.FTStartDate_lbl.Tag = "2|"
        Me.FTStartDate_lbl.Text = "AS Of Date :"
        '
        'FTStartDate
        '
        Me.FTStartDate.EditValue = Nothing
        Me.FTStartDate.EnterMoveNextControl = True
        Me.FTStartDate.Location = New System.Drawing.Point(163, 30)
        Me.FTStartDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartDate.Name = "FTStartDate"
        Me.FTStartDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.NullDate = ""
        Me.FTStartDate.Size = New System.Drawing.Size(141, 22)
        Me.FTStartDate.TabIndex = 509
        Me.FTStartDate.Tag = "2|"
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
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cEmpCount, Me.cFTCmpCode, Me.cFNHSysSectId, Me.cFTSectCode, Me.cFNHSysUnitSectId, Me.cFTUnitSectCode, Me.cFNSam, Me.cFTStyleCode, Me.cFTCustCode, Me.cFTPORef, Me.cOTQuantity, Me.cFNQuantity, Me.cFNOT1Min, Me.cFNTimeMin, Me.cFTSeasonCode, Me.cFNTarget, Me.cFNAVEFF, Me.cFNNetCM, Me.cFNBudget, Me.cEarningperline, Me.cFNLostEarnning, Me.cFNLostEarnningPer, Me.cFNTargetOT})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsView.AllowCellMerge = True
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'cEmpCount
        '
        Me.cEmpCount.Caption = "EmpCount"
        Me.cEmpCount.DisplayFormat.FormatString = "{0:n0}"
        Me.cEmpCount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cEmpCount.FieldName = "EmpCount"
        Me.cEmpCount.Name = "cEmpCount"
        Me.cEmpCount.OptionsColumn.AllowEdit = False
        Me.cEmpCount.Visible = True
        Me.cEmpCount.VisibleIndex = 2
        Me.cEmpCount.Width = 97
        '
        'cFTCmpCode
        '
        Me.cFTCmpCode.Caption = "FTCmpCode"
        Me.cFTCmpCode.FieldName = "FTCmpCode"
        Me.cFTCmpCode.Name = "cFTCmpCode"
        Me.cFTCmpCode.OptionsColumn.AllowEdit = False
        Me.cFTCmpCode.Visible = True
        Me.cFTCmpCode.VisibleIndex = 0
        Me.cFTCmpCode.Width = 126
        '
        'cFNHSysSectId
        '
        Me.cFNHSysSectId.Caption = "FNHSysSectId"
        Me.cFNHSysSectId.FieldName = "FNHSysSectId"
        Me.cFNHSysSectId.Name = "cFNHSysSectId"
        Me.cFNHSysSectId.OptionsColumn.AllowEdit = False
        Me.cFNHSysSectId.Width = 124
        '
        'cFTSectCode
        '
        Me.cFTSectCode.Caption = "FTSectCode"
        Me.cFTSectCode.FieldName = "FTSectCode"
        Me.cFTSectCode.Name = "cFTSectCode"
        Me.cFTSectCode.OptionsColumn.AllowEdit = False
        Me.cFTSectCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTSectCode.Width = 124
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
        Me.cFTUnitSectCode.Width = 127
        '
        'cFNSam
        '
        Me.cFNSam.Caption = "FNSam"
        Me.cFNSam.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNSam.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNSam.FieldName = "FNSam"
        Me.cFNSam.Name = "cFNSam"
        Me.cFNSam.OptionsColumn.AllowEdit = False
        Me.cFNSam.Visible = True
        Me.cFNSam.VisibleIndex = 8
        Me.cFNSam.Width = 131
        '
        'cFTStyleCode
        '
        Me.cFTStyleCode.Caption = "FTStyleCode"
        Me.cFTStyleCode.FieldName = "FTStyleCode"
        Me.cFTStyleCode.Name = "cFTStyleCode"
        Me.cFTStyleCode.OptionsColumn.AllowEdit = False
        Me.cFTStyleCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTStyleCode.Visible = True
        Me.cFTStyleCode.VisibleIndex = 7
        Me.cFTStyleCode.Width = 140
        '
        'cFTCustCode
        '
        Me.cFTCustCode.Caption = "FTCustCode"
        Me.cFTCustCode.FieldName = "FTCustCode"
        Me.cFTCustCode.Name = "cFTCustCode"
        Me.cFTCustCode.OptionsColumn.AllowEdit = False
        Me.cFTCustCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTCustCode.Visible = True
        Me.cFTCustCode.VisibleIndex = 5
        Me.cFTCustCode.Width = 127
        '
        'cFTPORef
        '
        Me.cFTPORef.Caption = "FTPORef"
        Me.cFTPORef.FieldName = "FTPORef"
        Me.cFTPORef.Name = "cFTPORef"
        Me.cFTPORef.OptionsColumn.AllowEdit = False
        Me.cFTPORef.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTPORef.Visible = True
        Me.cFTPORef.VisibleIndex = 6
        Me.cFTPORef.Width = 127
        '
        'cOTQuantity
        '
        Me.cOTQuantity.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cOTQuantity.AppearanceCell.Options.UseBackColor = True
        Me.cOTQuantity.Caption = "OTQuantity"
        Me.cOTQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.cOTQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cOTQuantity.FieldName = "OTQuantity"
        Me.cOTQuantity.Name = "cOTQuantity"
        Me.cOTQuantity.OptionsColumn.AllowEdit = False
        Me.cOTQuantity.Visible = True
        Me.cOTQuantity.VisibleIndex = 12
        Me.cOTQuantity.Width = 124
        '
        'cFNQuantity
        '
        Me.cFNQuantity.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cFNQuantity.AppearanceCell.Options.UseBackColor = True
        Me.cFNQuantity.Caption = "FNQuantity"
        Me.cFNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.cFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQuantity.FieldName = "FNQuantity"
        Me.cFNQuantity.Name = "cFNQuantity"
        Me.cFNQuantity.OptionsColumn.AllowEdit = False
        Me.cFNQuantity.Visible = True
        Me.cFNQuantity.VisibleIndex = 11
        Me.cFNQuantity.Width = 124
        '
        'cFNOT1Min
        '
        Me.cFNOT1Min.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.cFNOT1Min.AppearanceCell.Options.UseBackColor = True
        Me.cFNOT1Min.Caption = "FNOT1Min"
        Me.cFNOT1Min.DisplayFormat.FormatString = "{0:n0}"
        Me.cFNOT1Min.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNOT1Min.FieldName = "FNOT1Min"
        Me.cFNOT1Min.Name = "cFNOT1Min"
        Me.cFNOT1Min.OptionsColumn.AllowEdit = False
        Me.cFNOT1Min.Visible = True
        Me.cFNOT1Min.VisibleIndex = 4
        Me.cFNOT1Min.Width = 98
        '
        'cFNTimeMin
        '
        Me.cFNTimeMin.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.cFNTimeMin.AppearanceCell.Options.UseBackColor = True
        Me.cFNTimeMin.Caption = "FNTimeMin"
        Me.cFNTimeMin.DisplayFormat.FormatString = "{0:n0}"
        Me.cFNTimeMin.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNTimeMin.FieldName = "FNTimeMin"
        Me.cFNTimeMin.Name = "cFNTimeMin"
        Me.cFNTimeMin.OptionsColumn.AllowEdit = False
        Me.cFNTimeMin.Visible = True
        Me.cFNTimeMin.VisibleIndex = 3
        Me.cFNTimeMin.Width = 100
        '
        'cFTSeasonCode
        '
        Me.cFTSeasonCode.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cFTSeasonCode.AppearanceCell.Options.UseBackColor = True
        Me.cFTSeasonCode.Caption = "FTSeasonCode"
        Me.cFTSeasonCode.FieldName = "FTSeasonCode"
        Me.cFTSeasonCode.Name = "cFTSeasonCode"
        Me.cFTSeasonCode.OptionsColumn.AllowEdit = False
        Me.cFTSeasonCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTSeasonCode.Visible = True
        Me.cFTSeasonCode.VisibleIndex = 19
        Me.cFTSeasonCode.Width = 114
        '
        'cFNTarget
        '
        Me.cFNTarget.Caption = "FNTarget"
        Me.cFNTarget.DisplayFormat.FormatString = "{0:n0}"
        Me.cFNTarget.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNTarget.FieldName = "FNTarget"
        Me.cFNTarget.Name = "cFNTarget"
        Me.cFNTarget.OptionsColumn.AllowEdit = False
        Me.cFNTarget.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFNTarget.Visible = True
        Me.cFNTarget.VisibleIndex = 9
        Me.cFNTarget.Width = 95
        '
        'cFNAVEFF
        '
        Me.cFNAVEFF.Caption = "FNAVEFF"
        Me.cFNAVEFF.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNAVEFF.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNAVEFF.FieldName = "FNAVEFF"
        Me.cFNAVEFF.Name = "cFNAVEFF"
        Me.cFNAVEFF.OptionsColumn.AllowEdit = False
        Me.cFNAVEFF.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFNAVEFF.Visible = True
        Me.cFNAVEFF.VisibleIndex = 17
        Me.cFNAVEFF.Width = 155
        '
        'cFNNetCM
        '
        Me.cFNNetCM.Caption = "FNNetCM"
        Me.cFNNetCM.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNNetCM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNNetCM.FieldName = "FNNetCM"
        Me.cFNNetCM.Name = "cFNNetCM"
        Me.cFNNetCM.OptionsColumn.AllowEdit = False
        Me.cFNNetCM.Visible = True
        Me.cFNNetCM.VisibleIndex = 13
        Me.cFNNetCM.Width = 91
        '
        'cFNBudget
        '
        Me.cFNBudget.Caption = "FNBudget"
        Me.cFNBudget.FieldName = "FNBudget"
        Me.cFNBudget.Name = "cFNBudget"
        Me.cFNBudget.OptionsColumn.AllowEdit = False
        Me.cFNBudget.Visible = True
        Me.cFNBudget.VisibleIndex = 14
        Me.cFNBudget.Width = 87
        '
        'cEarningperline
        '
        Me.cEarningperline.Caption = "Earningperline"
        Me.cEarningperline.DisplayFormat.FormatString = "{0:n2}"
        Me.cEarningperline.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cEarningperline.FieldName = "Earningperline"
        Me.cEarningperline.Name = "cEarningperline"
        Me.cEarningperline.OptionsColumn.AllowEdit = False
        Me.cEarningperline.Visible = True
        Me.cEarningperline.VisibleIndex = 15
        Me.cEarningperline.Width = 87
        '
        'cFNLostEarnning
        '
        Me.cFNLostEarnning.Caption = "FNLostEarnning"
        Me.cFNLostEarnning.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNLostEarnning.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNLostEarnning.FieldName = "FNLostEarnning"
        Me.cFNLostEarnning.Name = "cFNLostEarnning"
        Me.cFNLostEarnning.OptionsColumn.AllowEdit = False
        Me.cFNLostEarnning.Visible = True
        Me.cFNLostEarnning.VisibleIndex = 16
        Me.cFNLostEarnning.Width = 87
        '
        'cFNLostEarnningPer
        '
        Me.cFNLostEarnningPer.Caption = "FNLostEarnningPer"
        Me.cFNLostEarnningPer.DisplayFormat.FormatString = "{0:n2}"
        Me.cFNLostEarnningPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNLostEarnningPer.FieldName = "FNLostEarnningPer"
        Me.cFNLostEarnningPer.Name = "cFNLostEarnningPer"
        Me.cFNLostEarnningPer.OptionsColumn.AllowEdit = False
        Me.cFNLostEarnningPer.Visible = True
        Me.cFNLostEarnningPer.VisibleIndex = 18
        Me.cFNLostEarnningPer.Width = 82
        '
        'cFNTargetOT
        '
        Me.cFNTargetOT.Caption = "FNTargetOT"
        Me.cFNTargetOT.DisplayFormat.FormatString = "{0:n0}"
        Me.cFNTargetOT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNTargetOT.FieldName = "FNTargetOT"
        Me.cFNTargetOT.Name = "cFNTargetOT"
        Me.cFNTargetOT.OptionsColumn.AllowEdit = False
        Me.cFNTargetOT.Visible = True
        Me.cFNTargetOT.VisibleIndex = 10
        '
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.ogbheader})
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'wProdEaringTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1326, 779)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wProdEaringTracking"
        Me.Text = "Production Tracking Earing"
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
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
    Friend WithEvents FTStartDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cEmpCount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNHSysSectId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNHSysUnitSectId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTUnitSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNSam As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCustCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cOTQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNOT1Min As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNTimeMin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTSeasonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNTarget As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNAVEFF As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNNetCM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNBudget As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cEarningperline As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNLostEarnning As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNLostEarnningPer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cFNTargetOT As DevExpress.XtraGrid.Columns.GridColumn
End Class
