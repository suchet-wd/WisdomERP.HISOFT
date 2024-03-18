<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wEmployeeEnablonHRM
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim FTUnitSectName As DevExpress.XtraGrid.Columns.GridColumn
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wEmployeeEnablonHRM))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.DockManager1 = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.hideContainerTop = New DevExpress.XtraBars.Docking.AutoHideContainer()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FNYear = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNYear_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDDateEnd = New DevExpress.XtraEditors.DateEdit()
        Me.FDDateEnd_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FDDate = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysCmpId_lbl_ofyear = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysEmpTypeId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysSectId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysDivisonId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysEmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysUnitSectId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbmainprocbutton1 = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit2 = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear5 = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload5 = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.DateEdit1 = New DevExpress.XtraEditors.DateEdit()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.XtraTabPage4 = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl6 = New DevExpress.XtraEditors.GroupControl()
        Me.ogcmanday = New DevExpress.XtraGrid.GridControl()
        Me.ogvmanday = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn51 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStartDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTInsDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.bFTUnitSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.bFTUnitSectName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.bFTEmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.bFNEmpSex = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Reason = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNLeaveTotalTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNLeaveTotalTimeMin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn66 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.bFTEmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView5 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl7 = New DevExpress.XtraEditors.GroupControl()
        Me.ogcsumleave = New DevExpress.XtraGrid.GridControl()
        Me.ogvsumleave = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn59 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTypeLeave = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.lFTMonthsum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.lFTToTalWork = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.lFTMalesum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.lFTFeMalesum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ogcsumnew = New DevExpress.XtraGrid.GridControl()
        Me.ogvsumnew = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.nFTMonthsum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.nFTToTalWork = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.nFTMalesum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.nFTFeMalesum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView8 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl4 = New DevExpress.XtraEditors.GroupControl()
        Me.ogcend = New DevExpress.XtraGrid.GridControl()
        Me.ogvend = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.dFTEmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dFTUnitSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dFTUnitSectName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dFTEmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dFNEmpSex = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dFDDateEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTResignName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn45 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView7 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl5 = New DevExpress.XtraEditors.GroupControl()
        Me.ogcsumrea = New DevExpress.XtraGrid.GridControl()
        Me.ogvsumrea = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn46 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rFTMonthsum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rFTToTalWork = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rFTMalesum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rFTFeMalesum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.ogcnew = New DevExpress.XtraGrid.GridControl()
        Me.ogvnew = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTEmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTUnitSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTUnitSectName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTEmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNEmpSex = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDDateStart = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView9 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        Me.otpsummary = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ogcTime = New DevExpress.XtraGrid.GridControl()
        Me.ogvtime = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTEmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNEmpSex = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn34 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDateTrans = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView11 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ogbpoamt = New DevExpress.XtraEditors.GroupControl()
        Me.ogcsumTime = New DevExpress.XtraGrid.GridControl()
        Me.ogvSumTime = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMonthsum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTToTalWork = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMalesum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFeMalesum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDaysum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView10 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.สรุปรายปี = New DevExpress.XtraTab.XtraTabPage()
        Me.GroupControl9 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupControl12 = New DevExpress.XtraEditors.GroupControl()
        Me.ogcsum = New DevExpress.XtraGrid.GridControl()
        Me.ogvsum = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.DetailS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTS1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTS2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTS3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTS4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTS5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTS6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTS7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTS8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTS9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTS10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTS11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTS12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView14 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl11 = New DevExpress.XtraEditors.GroupControl()
        Me.XtraTabPage3 = New DevExpress.XtraTab.XtraTabPage()
        Me.ogcsummary = New DevExpress.XtraGrid.GridControl()
        Me.ogvsummary = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.Detail = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFTMonthsum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFTMalesum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFTFeMalesum = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.mFTToTalWork = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView15 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.otbmain = New DevExpress.XtraTab.XtraTabControl()
        FTUnitSectName = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.hideContainerTop.SuspendLayout()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FNYear.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDDateEnd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FDDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton1.SuspendLayout()
        CType(Me.DateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.XtraTabPage4.SuspendLayout()
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl6.SuspendLayout()
        CType(Me.ogcmanday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvmanday, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl7.SuspendLayout()
        CType(Me.ogcsumleave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvsumleave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcsumnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvsumnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl4.SuspendLayout()
        CType(Me.ogcend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvend, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl5.SuspendLayout()
        CType(Me.ogcsumrea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvsumrea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.ogcnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvnew, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        Me.otpsummary.SuspendLayout()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ogcTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbpoamt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbpoamt.SuspendLayout()
        CType(Me.ogcsumTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvSumTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.สรุปรายปี.SuspendLayout()
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl9.SuspendLayout()
        CType(Me.GroupControl12, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl12.SuspendLayout()
        CType(Me.ogcsum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvsum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl11, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage3.SuspendLayout()
        CType(Me.ogcsummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvsummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otbmain.SuspendLayout()
        Me.SuspendLayout()
        '
        'FTUnitSectName
        '
        FTUnitSectName.AppearanceHeader.Options.UseTextOptions = True
        FTUnitSectName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        FTUnitSectName.Caption = "FTUnitSectName"
        FTUnitSectName.FieldName = "FTUnitSectName"
        FTUnitSectName.Name = "FTUnitSectName"
        FTUnitSectName.OptionsColumn.AllowEdit = False
        FTUnitSectName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        FTUnitSectName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        FTUnitSectName.OptionsColumn.ReadOnly = True
        FTUnitSectName.Visible = True
        FTUnitSectName.VisibleIndex = 3
        FTUnitSectName.Width = 157
        '
        'DockManager1
        '
        Me.DockManager1.AutoHideContainers.AddRange(New DevExpress.XtraBars.Docking.AutoHideContainer() {Me.hideContainerTop})
        Me.DockManager1.Form = Me
        Me.DockManager1.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'hideContainerTop
        '
        Me.hideContainerTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.hideContainerTop.Controls.Add(Me.ogbheader)
        Me.hideContainerTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.hideContainerTop.Location = New System.Drawing.Point(0, 0)
        Me.hideContainerTop.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.hideContainerTop.Name = "hideContainerTop"
        Me.hideContainerTop.Size = New System.Drawing.Size(1378, 44)
        '
        'ogbheader
        '
        Me.ogbheader.Controls.Add(Me.DockPanel1_Container)
        Me.ogbheader.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ogbheader.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.DockVertical = DevExpress.Utils.DefaultBoolean.[False]
        Me.ogbheader.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.ogbheader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.ogbheader.ID = New System.Guid("07aa44b9-5e74-47ef-b37d-869730080592")
        Me.ogbheader.ImageOptions.Image = CType(resources.GetObject("ogbheader.ImageOptions.Image"), System.Drawing.Image)
        Me.ogbheader.Location = New System.Drawing.Point(0, 44)
        Me.ogbheader.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.FloatOnDblClick = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.Options.ShowMaximizeButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(1564, 128)
        Me.ogbheader.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.SavedIndex = 0
        Me.ogbheader.Size = New System.Drawing.Size(1378, 128)
        Me.ogbheader.TabStop = False
        Me.ogbheader.Text = "Criteria"
        Me.ogbheader.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FNYear)
        Me.DockPanel1_Container.Controls.Add(Me.FNYear_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FDDateEnd)
        Me.DockPanel1_Container.Controls.Add(Me.FDDateEnd_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FDDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FDDate)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_lbl_ofyear)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(2, 31)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1374, 93)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FNYear
        '
        Me.FNYear.EditValue = ""
        Me.FNYear.EnterMoveNextControl = True
        Me.FNYear.Location = New System.Drawing.Point(1016, 35)
        Me.FNYear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNYear.Name = "FNYear"
        Me.FNYear.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNYear.Properties.Appearance.Options.UseBackColor = True
        Me.FNYear.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNYear.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNYear.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNYear.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNYear.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNYear.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNYear.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNYear.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNYear.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNYear.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNYear.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNYear.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNYear.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNYear.Properties.Tag = "FNPayYear"
        Me.FNYear.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNYear.Size = New System.Drawing.Size(155, 23)
        Me.FNYear.TabIndex = 507
        Me.FNYear.Tag = "2|"
        '
        'FNYear_lbl
        '
        Me.FNYear_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNYear_lbl.Appearance.Options.UseForeColor = True
        Me.FNYear_lbl.Appearance.Options.UseTextOptions = True
        Me.FNYear_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNYear_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNYear_lbl.Location = New System.Drawing.Point(885, 36)
        Me.FNYear_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNYear_lbl.Name = "FNYear_lbl"
        Me.FNYear_lbl.Size = New System.Drawing.Size(125, 21)
        Me.FNYear_lbl.TabIndex = 508
        Me.FNYear_lbl.Tag = "2|"
        Me.FNYear_lbl.Text = "Year :"
        '
        'FDDateEnd
        '
        Me.FDDateEnd.EditValue = Nothing
        Me.FDDateEnd.EnterMoveNextControl = True
        Me.FDDateEnd.Location = New System.Drawing.Point(686, 34)
        Me.FDDateEnd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDDateEnd.Name = "FDDateEnd"
        Me.FDDateEnd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDDateEnd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDDateEnd.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDDateEnd.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDDateEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDDateEnd.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FDDateEnd.Properties.NullDate = ""
        Me.FDDateEnd.Size = New System.Drawing.Size(131, 23)
        Me.FDDateEnd.TabIndex = 506
        Me.FDDateEnd.Tag = "2|"
        '
        'FDDateEnd_lbl
        '
        Me.FDDateEnd_lbl.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.FDDateEnd_lbl.Appearance.Options.UseForeColor = True
        Me.FDDateEnd_lbl.Appearance.Options.UseTextOptions = True
        Me.FDDateEnd_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDDateEnd_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDDateEnd_lbl.Location = New System.Drawing.Point(554, 34)
        Me.FDDateEnd_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDDateEnd_lbl.Name = "FDDateEnd_lbl"
        Me.FDDateEnd_lbl.Size = New System.Drawing.Size(125, 25)
        Me.FDDateEnd_lbl.TabIndex = 505
        Me.FDDateEnd_lbl.Tag = "2|"
        Me.FDDateEnd_lbl.Text = "End Date :"
        '
        'FDDate_lbl
        '
        Me.FDDate_lbl.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.FDDate_lbl.Appearance.Options.UseForeColor = True
        Me.FDDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FDDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FDDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FDDate_lbl.Location = New System.Drawing.Point(48, 34)
        Me.FDDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDDate_lbl.Name = "FDDate_lbl"
        Me.FDDate_lbl.Size = New System.Drawing.Size(125, 25)
        Me.FDDate_lbl.TabIndex = 504
        Me.FDDate_lbl.Tag = "2|"
        Me.FDDate_lbl.Text = "Start Date :"
        '
        'FDDate
        '
        Me.FDDate.EditValue = Nothing
        Me.FDDate.EnterMoveNextControl = True
        Me.FDDate.Location = New System.Drawing.Point(175, 36)
        Me.FDDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FDDate.Name = "FDDate"
        Me.FDDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FDDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FDDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FDDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FDDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FDDate.Properties.NullDate = ""
        Me.FDDate.Size = New System.Drawing.Size(131, 23)
        Me.FDDate.TabIndex = 503
        Me.FDDate.Tag = "2|"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(269, 4)
        Me.FNHSysCmpId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(665, 23)
        Me.FNHSysCmpId_None.TabIndex = 485
        Me.FNHSysCmpId_None.Tag = ""
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(135, 4)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "11", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FNHSysCmpId.Properties.ReadOnly = True
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(132, 23)
        Me.FNHSysCmpId.TabIndex = 472
        Me.FNHSysCmpId.Tag = ""
        '
        'FNHSysCmpId_lbl_ofyear
        '
        Me.FNHSysCmpId_lbl_ofyear.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(31, Byte), Integer), CType(CType(53, Byte), Integer))
        Me.FNHSysCmpId_lbl_ofyear.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl_ofyear.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl_ofyear.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl_ofyear.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl_ofyear.Location = New System.Drawing.Point(7, 1)
        Me.FNHSysCmpId_lbl_ofyear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_lbl_ofyear.Name = "FNHSysCmpId_lbl_ofyear"
        Me.FNHSysCmpId_lbl_ofyear.Size = New System.Drawing.Size(125, 21)
        Me.FNHSysCmpId_lbl_ofyear.TabIndex = 465
        Me.FNHSysCmpId_lbl_ofyear.Tag = "2|"
        Me.FNHSysCmpId_lbl_ofyear.Text = "Company :"
        '
        'FNHSysEmpTypeId_lbl
        '
        Me.FNHSysEmpTypeId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysEmpTypeId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysEmpTypeId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysEmpTypeId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysEmpTypeId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysEmpTypeId_lbl.Location = New System.Drawing.Point(14, 26)
        Me.FNHSysEmpTypeId_lbl.Name = "FNHSysEmpTypeId_lbl"
        Me.FNHSysEmpTypeId_lbl.Size = New System.Drawing.Size(107, 17)
        Me.FNHSysEmpTypeId_lbl.TabIndex = 418
        Me.FNHSysEmpTypeId_lbl.Tag = "2|"
        Me.FNHSysEmpTypeId_lbl.Text = "Employee Type :"
        '
        'FNHSysSectId_lbl
        '
        Me.FNHSysSectId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysSectId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysSectId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysSectId_lbl.Location = New System.Drawing.Point(13, 89)
        Me.FNHSysSectId_lbl.Name = "FNHSysSectId_lbl"
        Me.FNHSysSectId_lbl.Size = New System.Drawing.Size(107, 20)
        Me.FNHSysSectId_lbl.TabIndex = 395
        Me.FNHSysSectId_lbl.Tag = "2|"
        Me.FNHSysSectId_lbl.Text = "Start Sect :"
        '
        'FNHSysDivisonId_lbl
        '
        Me.FNHSysDivisonId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysDivisonId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysDivisonId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysDivisonId_lbl.Location = New System.Drawing.Point(13, 68)
        Me.FNHSysDivisonId_lbl.Name = "FNHSysDivisonId_lbl"
        Me.FNHSysDivisonId_lbl.Size = New System.Drawing.Size(107, 20)
        Me.FNHSysDivisonId_lbl.TabIndex = 393
        Me.FNHSysDivisonId_lbl.Tag = "2|"
        Me.FNHSysDivisonId_lbl.Text = "Start Devision :"
        '
        'FNHSysEmpId_lbl
        '
        Me.FNHSysEmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysEmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysEmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysEmpId_lbl.Location = New System.Drawing.Point(13, 131)
        Me.FNHSysEmpId_lbl.Name = "FNHSysEmpId_lbl"
        Me.FNHSysEmpId_lbl.Size = New System.Drawing.Size(107, 20)
        Me.FNHSysEmpId_lbl.TabIndex = 412
        Me.FNHSysEmpId_lbl.Tag = "2|"
        Me.FNHSysEmpId_lbl.Text = "Start Employee :"
        '
        'FNHSysUnitSectId_lbl
        '
        Me.FNHSysUnitSectId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysUnitSectId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysUnitSectId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysUnitSectId_lbl.Location = New System.Drawing.Point(13, 110)
        Me.FNHSysUnitSectId_lbl.Name = "FNHSysUnitSectId_lbl"
        Me.FNHSysUnitSectId_lbl.Size = New System.Drawing.Size(107, 20)
        Me.FNHSysUnitSectId_lbl.TabIndex = 397
        Me.FNHSysUnitSectId_lbl.Tag = "2|"
        Me.FNHSysUnitSectId_lbl.Text = "Start Unit Sect :"
        '
        'ogbmainprocbutton1
        '
        Me.ogbmainprocbutton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton1.Controls.Add(Me.ocmsavelayout)
        Me.ogbmainprocbutton1.Controls.Add(Me.ocmexit2)
        Me.ogbmainprocbutton1.Controls.Add(Me.ocmclear5)
        Me.ogbmainprocbutton1.Controls.Add(Me.ocmload5)
        Me.ogbmainprocbutton1.Location = New System.Drawing.Point(2980, 668)
        Me.ogbmainprocbutton1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton1.Name = "ogbmainprocbutton1"
        Me.ogbmainprocbutton1.Size = New System.Drawing.Size(889, 58)
        Me.ogbmainprocbutton1.TabIndex = 388
        Me.ogbmainprocbutton1.Tag = "2|"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Location = New System.Drawing.Point(377, 15)
        Me.ocmsavelayout.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(136, 28)
        Me.ocmsavelayout.TabIndex = 333
        Me.ocmsavelayout.Text = "savelayoutgrid"
        '
        'ocmexit2
        '
        Me.ocmexit2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit2.Location = New System.Drawing.Point(757, 14)
        Me.ocmexit2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit2.Name = "ocmexit2"
        Me.ocmexit2.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit2.TabIndex = 96
        Me.ocmexit2.TabStop = False
        Me.ocmexit2.Tag = "2|"
        Me.ocmexit2.Text = "EXIT"
        '
        'ocmclear5
        '
        Me.ocmclear5.Location = New System.Drawing.Point(246, 14)
        Me.ocmclear5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear5.Name = "ocmclear5"
        Me.ocmclear5.Size = New System.Drawing.Size(111, 31)
        Me.ocmclear5.TabIndex = 95
        Me.ocmclear5.TabStop = False
        Me.ocmclear5.Tag = "2|"
        Me.ocmclear5.Text = "CLEAR"
        '
        'ocmload5
        '
        Me.ocmload5.Location = New System.Drawing.Point(12, 34)
        Me.ocmload5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload5.Name = "ocmload5"
        Me.ocmload5.Size = New System.Drawing.Size(94, 30)
        Me.ocmload5.TabIndex = 329
        Me.ocmload5.Text = "Load Data"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl3.Appearance.Options.UseForeColor = True
        Me.LabelControl3.Appearance.Options.UseTextOptions = True
        Me.LabelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl3.Location = New System.Drawing.Point(6, 48)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(107, 20)
        Me.LabelControl3.TabIndex = 504
        Me.LabelControl3.Tag = "2|"
        Me.LabelControl3.Text = "Start Date :"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl4.Appearance.Options.UseForeColor = True
        Me.LabelControl4.Appearance.Options.UseTextOptions = True
        Me.LabelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl4.Location = New System.Drawing.Point(6, 48)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(107, 20)
        Me.LabelControl4.TabIndex = 504
        Me.LabelControl4.Tag = "2|"
        Me.LabelControl4.Text = "Start Date :"
        '
        'DateEdit1
        '
        Me.DateEdit1.EditValue = Nothing
        Me.DateEdit1.EnterMoveNextControl = True
        Me.DateEdit1.Location = New System.Drawing.Point(116, 48)
        Me.DateEdit1.Name = "DateEdit1"
        Me.DateEdit1.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.DateEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.DateEdit1.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.DateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.DateEdit1.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.DateEdit1.Properties.NullDate = ""
        Me.DateEdit1.Size = New System.Drawing.Size(112, 23)
        Me.DateEdit1.TabIndex = 503
        Me.DateEdit1.Tag = "2|"
        '
        'GridColumn25
        '
        Me.GridColumn25.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn25.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn25.Caption = "FNHSysEmpID"
        Me.GridColumn25.FieldName = "FNHSysEmpID"
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.OptionsColumn.AllowEdit = False
        Me.GridColumn25.OptionsColumn.AllowMove = False
        Me.GridColumn25.OptionsColumn.ReadOnly = True
        Me.GridColumn25.OptionsColumn.TabStop = False
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdelete)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Controls.Add(Me.SimpleButton1)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(719, 82)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(571, 73)
        Me.ogbmainprocbutton.TabIndex = 393
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmdelete
        '
        Me.ocmdelete.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmdelete.Location = New System.Drawing.Point(386, 18)
        Me.ocmdelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(0, 31)
        Me.ocmdelete.TabIndex = 334
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "DELETE"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(269, 18)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(111, 31)
        Me.ocmsave.TabIndex = 335
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Location = New System.Drawing.Point(602, 21)
        Me.SimpleButton1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(136, 28)
        Me.SimpleButton1.TabIndex = 333
        Me.SimpleButton1.Text = "savelayoutgrid"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(372, 13)
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
        Me.ocmclear.Location = New System.Drawing.Point(129, 16)
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
        Me.ocmload.Location = New System.Drawing.Point(14, 16)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(100, 28)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'XtraTabPage4
        '
        Me.XtraTabPage4.Controls.Add(Me.GroupControl6)
        Me.XtraTabPage4.Controls.Add(Me.GroupControl7)
        Me.XtraTabPage4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.XtraTabPage4.Name = "XtraTabPage4"
        Me.XtraTabPage4.Size = New System.Drawing.Size(1368, 828)
        Me.XtraTabPage4.Text = "วันที่บันทึกลา"
        '
        'GroupControl6
        '
        Me.GroupControl6.Controls.Add(Me.ogcmanday)
        Me.GroupControl6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl6.Location = New System.Drawing.Point(0, 105)
        Me.GroupControl6.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl6.Name = "GroupControl6"
        Me.GroupControl6.Size = New System.Drawing.Size(1368, 723)
        Me.GroupControl6.TabIndex = 15
        Me.GroupControl6.Text = "รายละเอียด"
        '
        'ogcmanday
        '
        Me.ogcmanday.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcmanday.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcmanday.Location = New System.Drawing.Point(2, 27)
        Me.ogcmanday.MainView = Me.ogvmanday
        Me.ogcmanday.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcmanday.Name = "ogcmanday"
        Me.ogcmanday.Size = New System.Drawing.Size(1364, 694)
        Me.ogcmanday.TabIndex = 3
        Me.ogcmanday.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvmanday, Me.GridView5})
        '
        'ogvmanday
        '
        Me.ogvmanday.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn51, Me.FTStartDate, Me.FTInsDate, Me.bFTUnitSectCode, Me.bFTUnitSectName, Me.bFTEmpCode, Me.bFNEmpSex, Me.Reason, Me.FNLeaveTotalTime, Me.FNLeaveTotalTimeMin, Me.GridColumn66, Me.bFTEmpName})
        Me.ogvmanday.GridControl = Me.ogcmanday
        Me.ogvmanday.Name = "ogvmanday"
        Me.ogvmanday.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvmanday.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.ogvmanday.OptionsView.ColumnAutoWidth = False
        Me.ogvmanday.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvmanday.OptionsView.ShowGroupPanel = False
        Me.ogvmanday.Tag = "2|"
        '
        'GridColumn51
        '
        Me.GridColumn51.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn51.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn51.Caption = "FNHSysEmpID"
        Me.GridColumn51.FieldName = "FNHSysEmpID"
        Me.GridColumn51.Name = "GridColumn51"
        Me.GridColumn51.OptionsColumn.AllowEdit = False
        Me.GridColumn51.OptionsColumn.AllowMove = False
        Me.GridColumn51.OptionsColumn.ReadOnly = True
        Me.GridColumn51.OptionsColumn.TabStop = False
        '
        'FTStartDate
        '
        Me.FTStartDate.Caption = "FTStartDate"
        Me.FTStartDate.FieldName = "FTStartDate"
        Me.FTStartDate.Name = "FTStartDate"
        Me.FTStartDate.OptionsColumn.AllowEdit = False
        Me.FTStartDate.Visible = True
        Me.FTStartDate.VisibleIndex = 0
        Me.FTStartDate.Width = 108
        '
        'FTInsDate
        '
        Me.FTInsDate.Caption = "FTInsDate"
        Me.FTInsDate.FieldName = "FTInsDate"
        Me.FTInsDate.MinWidth = 25
        Me.FTInsDate.Name = "FTInsDate"
        Me.FTInsDate.OptionsColumn.AllowEdit = False
        Me.FTInsDate.OptionsColumn.ReadOnly = True
        Me.FTInsDate.Visible = True
        Me.FTInsDate.VisibleIndex = 1
        Me.FTInsDate.Width = 121
        '
        'bFTUnitSectCode
        '
        Me.bFTUnitSectCode.AppearanceHeader.Options.UseTextOptions = True
        Me.bFTUnitSectCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.bFTUnitSectCode.Caption = "FTUnitSectCode"
        Me.bFTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.bFTUnitSectCode.Name = "bFTUnitSectCode"
        Me.bFTUnitSectCode.OptionsColumn.AllowEdit = False
        Me.bFTUnitSectCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.bFTUnitSectCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.bFTUnitSectCode.OptionsColumn.ReadOnly = True
        Me.bFTUnitSectCode.Visible = True
        Me.bFTUnitSectCode.VisibleIndex = 4
        Me.bFTUnitSectCode.Width = 140
        '
        'bFTUnitSectName
        '
        Me.bFTUnitSectName.AppearanceHeader.Options.UseTextOptions = True
        Me.bFTUnitSectName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.bFTUnitSectName.Caption = "FTUnitSectName"
        Me.bFTUnitSectName.FieldName = "FTUnitSectName"
        Me.bFTUnitSectName.Name = "bFTUnitSectName"
        Me.bFTUnitSectName.OptionsColumn.AllowEdit = False
        Me.bFTUnitSectName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.bFTUnitSectName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.bFTUnitSectName.OptionsColumn.ReadOnly = True
        Me.bFTUnitSectName.Visible = True
        Me.bFTUnitSectName.VisibleIndex = 5
        Me.bFTUnitSectName.Width = 118
        '
        'bFTEmpCode
        '
        Me.bFTEmpCode.AppearanceHeader.Options.UseTextOptions = True
        Me.bFTEmpCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.bFTEmpCode.Caption = "FTEmpCode"
        Me.bFTEmpCode.FieldName = "FTEmpCode"
        Me.bFTEmpCode.Name = "bFTEmpCode"
        Me.bFTEmpCode.OptionsColumn.AllowEdit = False
        Me.bFTEmpCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.bFTEmpCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.bFTEmpCode.OptionsColumn.ReadOnly = True
        Me.bFTEmpCode.OptionsColumn.TabStop = False
        Me.bFTEmpCode.Visible = True
        Me.bFTEmpCode.VisibleIndex = 2
        Me.bFTEmpCode.Width = 198
        '
        'bFNEmpSex
        '
        Me.bFNEmpSex.AppearanceHeader.Options.UseTextOptions = True
        Me.bFNEmpSex.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.bFNEmpSex.Caption = "FNEmpSex"
        Me.bFNEmpSex.FieldName = "FNEmpSex"
        Me.bFNEmpSex.Name = "bFNEmpSex"
        Me.bFNEmpSex.OptionsColumn.AllowEdit = False
        Me.bFNEmpSex.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.bFNEmpSex.OptionsColumn.ReadOnly = True
        Me.bFNEmpSex.OptionsColumn.TabStop = False
        Me.bFNEmpSex.Visible = True
        Me.bFNEmpSex.VisibleIndex = 6
        Me.bFNEmpSex.Width = 113
        '
        'Reason
        '
        Me.Reason.AppearanceHeader.Options.UseTextOptions = True
        Me.Reason.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.Reason.Caption = "Reason"
        Me.Reason.FieldName = "Reason"
        Me.Reason.Name = "Reason"
        Me.Reason.OptionsColumn.AllowEdit = False
        Me.Reason.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.Reason.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.Reason.OptionsColumn.ReadOnly = True
        Me.Reason.Visible = True
        Me.Reason.VisibleIndex = 7
        Me.Reason.Width = 156
        '
        'FNLeaveTotalTime
        '
        Me.FNLeaveTotalTime.AppearanceHeader.Options.UseTextOptions = True
        Me.FNLeaveTotalTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNLeaveTotalTime.Caption = "TotalLeave(Hour)"
        Me.FNLeaveTotalTime.FieldName = "FNLeaveTotalTime"
        Me.FNLeaveTotalTime.Name = "FNLeaveTotalTime"
        Me.FNLeaveTotalTime.OptionsColumn.AllowEdit = False
        Me.FNLeaveTotalTime.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.FNLeaveTotalTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.FNLeaveTotalTime.OptionsColumn.ReadOnly = True
        Me.FNLeaveTotalTime.Visible = True
        Me.FNLeaveTotalTime.VisibleIndex = 8
        Me.FNLeaveTotalTime.Width = 160
        '
        'FNLeaveTotalTimeMin
        '
        Me.FNLeaveTotalTimeMin.AppearanceHeader.Options.UseTextOptions = True
        Me.FNLeaveTotalTimeMin.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNLeaveTotalTimeMin.Caption = "TotalLeave(Min)"
        Me.FNLeaveTotalTimeMin.FieldName = "FNLeaveTotalTimeMin"
        Me.FNLeaveTotalTimeMin.Name = "FNLeaveTotalTimeMin"
        Me.FNLeaveTotalTimeMin.OptionsColumn.AllowEdit = False
        Me.FNLeaveTotalTimeMin.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.FNLeaveTotalTimeMin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.FNLeaveTotalTimeMin.OptionsColumn.ReadOnly = True
        Me.FNLeaveTotalTimeMin.Visible = True
        Me.FNLeaveTotalTimeMin.VisibleIndex = 9
        Me.FNLeaveTotalTimeMin.Width = 125
        '
        'GridColumn66
        '
        Me.GridColumn66.Caption = "FNEmpStatus"
        Me.GridColumn66.FieldName = "FNEmpStatus"
        Me.GridColumn66.Name = "GridColumn66"
        Me.GridColumn66.OptionsColumn.AllowEdit = False
        Me.GridColumn66.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn66.OptionsColumn.AllowMove = False
        Me.GridColumn66.OptionsColumn.ReadOnly = True
        '
        'bFTEmpName
        '
        Me.bFTEmpName.Caption = "FTEmpName"
        Me.bFTEmpName.FieldName = "FTEmpName"
        Me.bFTEmpName.MinWidth = 24
        Me.bFTEmpName.Name = "bFTEmpName"
        Me.bFTEmpName.OptionsColumn.AllowEdit = False
        Me.bFTEmpName.OptionsColumn.ReadOnly = True
        Me.bFTEmpName.Visible = True
        Me.bFTEmpName.VisibleIndex = 3
        Me.bFTEmpName.Width = 147
        '
        'GridView5
        '
        Me.GridView5.GridControl = Me.ogcmanday
        Me.GridView5.Name = "GridView5"
        '
        'GroupControl7
        '
        Me.GroupControl7.Controls.Add(Me.ogcsumleave)
        Me.GroupControl7.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl7.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl7.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl7.Name = "GroupControl7"
        Me.GroupControl7.Size = New System.Drawing.Size(1368, 105)
        Me.GroupControl7.TabIndex = 14
        Me.GroupControl7.Text = "summerry"
        '
        'ogcsumleave
        '
        Me.ogcsumleave.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcsumleave.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcsumleave.Location = New System.Drawing.Point(2, 27)
        Me.ogcsumleave.MainView = Me.ogvsumleave
        Me.ogcsumleave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcsumleave.Name = "ogcsumleave"
        Me.ogcsumleave.Size = New System.Drawing.Size(1364, 76)
        Me.ogcsumleave.TabIndex = 2
        Me.ogcsumleave.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvsumleave})
        '
        'ogvsumleave
        '
        Me.ogvsumleave.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn59, Me.FNTypeLeave, Me.lFTMonthsum, Me.lFTToTalWork, Me.lFTMalesum, Me.lFTFeMalesum})
        Me.ogvsumleave.GridControl = Me.ogcsumleave
        Me.ogvsumleave.Name = "ogvsumleave"
        Me.ogvsumleave.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvsumleave.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.ogvsumleave.OptionsView.ColumnAutoWidth = False
        Me.ogvsumleave.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvsumleave.OptionsView.ShowGroupPanel = False
        Me.ogvsumleave.Tag = "2|"
        '
        'GridColumn59
        '
        Me.GridColumn59.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn59.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn59.Caption = "FNHSysEmpID"
        Me.GridColumn59.FieldName = "FNHSysEmpID"
        Me.GridColumn59.Name = "GridColumn59"
        Me.GridColumn59.OptionsColumn.AllowEdit = False
        Me.GridColumn59.OptionsColumn.AllowMove = False
        Me.GridColumn59.OptionsColumn.ReadOnly = True
        Me.GridColumn59.OptionsColumn.TabStop = False
        '
        'FNTypeLeave
        '
        Me.FNTypeLeave.Caption = "FNTypeLeave"
        Me.FNTypeLeave.FieldName = "FNTypeLeave"
        Me.FNTypeLeave.Name = "FNTypeLeave"
        Me.FNTypeLeave.OptionsColumn.AllowEdit = False
        Me.FNTypeLeave.Visible = True
        Me.FNTypeLeave.VisibleIndex = 0
        Me.FNTypeLeave.Width = 108
        '
        'lFTMonthsum
        '
        Me.lFTMonthsum.AppearanceHeader.Options.UseTextOptions = True
        Me.lFTMonthsum.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lFTMonthsum.Caption = "FTMonthsum"
        Me.lFTMonthsum.FieldName = "FTMonthsum"
        Me.lFTMonthsum.Name = "lFTMonthsum"
        Me.lFTMonthsum.OptionsColumn.AllowEdit = False
        Me.lFTMonthsum.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.lFTMonthsum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.lFTMonthsum.OptionsColumn.ReadOnly = True
        Me.lFTMonthsum.Visible = True
        Me.lFTMonthsum.VisibleIndex = 1
        Me.lFTMonthsum.Width = 180
        '
        'lFTToTalWork
        '
        Me.lFTToTalWork.AppearanceHeader.Options.UseTextOptions = True
        Me.lFTToTalWork.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lFTToTalWork.Caption = "FTToTalWork"
        Me.lFTToTalWork.DisplayFormat.FormatString = "{0:n2}"
        Me.lFTToTalWork.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.lFTToTalWork.FieldName = "FTToTalWork"
        Me.lFTToTalWork.Name = "lFTToTalWork"
        Me.lFTToTalWork.OptionsColumn.AllowEdit = False
        Me.lFTToTalWork.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.lFTToTalWork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.lFTToTalWork.OptionsColumn.ReadOnly = True
        Me.lFTToTalWork.Visible = True
        Me.lFTToTalWork.VisibleIndex = 2
        Me.lFTToTalWork.Width = 157
        '
        'lFTMalesum
        '
        Me.lFTMalesum.AppearanceHeader.Options.UseTextOptions = True
        Me.lFTMalesum.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.lFTMalesum.Caption = "FTMalesum"
        Me.lFTMalesum.DisplayFormat.FormatString = "{0:n2}"
        Me.lFTMalesum.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.lFTMalesum.FieldName = "FTMalesum"
        Me.lFTMalesum.Name = "lFTMalesum"
        Me.lFTMalesum.OptionsColumn.AllowEdit = False
        Me.lFTMalesum.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.lFTMalesum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.lFTMalesum.OptionsColumn.ReadOnly = True
        Me.lFTMalesum.OptionsColumn.TabStop = False
        Me.lFTMalesum.Visible = True
        Me.lFTMalesum.VisibleIndex = 3
        Me.lFTMalesum.Width = 198
        '
        'lFTFeMalesum
        '
        Me.lFTFeMalesum.Caption = "FTFeMalesum"
        Me.lFTFeMalesum.DisplayFormat.FormatString = "{0:n2}"
        Me.lFTFeMalesum.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.lFTFeMalesum.FieldName = "FTFeMalesum"
        Me.lFTFeMalesum.MinWidth = 24
        Me.lFTFeMalesum.Name = "lFTFeMalesum"
        Me.lFTFeMalesum.OptionsColumn.AllowEdit = False
        Me.lFTFeMalesum.OptionsColumn.ReadOnly = True
        Me.lFTFeMalesum.Visible = True
        Me.lFTFeMalesum.VisibleIndex = 4
        Me.lFTFeMalesum.Width = 233
        '
        'GridView4
        '
        Me.GridView4.GridControl = Me.ogcsumnew
        Me.GridView4.Name = "GridView4"
        '
        'ogcsumnew
        '
        Me.ogcsumnew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcsumnew.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcsumnew.Location = New System.Drawing.Point(2, 27)
        Me.ogcsumnew.MainView = Me.ogvsumnew
        Me.ogcsumnew.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcsumnew.Name = "ogcsumnew"
        Me.ogcsumnew.Size = New System.Drawing.Size(1364, 69)
        Me.ogcsumnew.TabIndex = 2
        Me.ogcsumnew.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvsumnew, Me.GridView8, Me.GridView4})
        '
        'ogvsumnew
        '
        Me.ogvsumnew.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn31, Me.nFTMonthsum, Me.nFTToTalWork, Me.nFTMalesum, Me.nFTFeMalesum})
        Me.ogvsumnew.GridControl = Me.ogcsumnew
        Me.ogvsumnew.Name = "ogvsumnew"
        Me.ogvsumnew.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvsumnew.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.ogvsumnew.OptionsView.ColumnAutoWidth = False
        Me.ogvsumnew.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvsumnew.OptionsView.ShowGroupPanel = False
        Me.ogvsumnew.Tag = "2|"
        '
        'GridColumn31
        '
        Me.GridColumn31.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn31.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn31.Caption = "FNHSysEmpID"
        Me.GridColumn31.FieldName = "FNHSysEmpID"
        Me.GridColumn31.Name = "GridColumn31"
        Me.GridColumn31.OptionsColumn.AllowEdit = False
        Me.GridColumn31.OptionsColumn.AllowMove = False
        Me.GridColumn31.OptionsColumn.ReadOnly = True
        Me.GridColumn31.OptionsColumn.TabStop = False
        '
        'nFTMonthsum
        '
        Me.nFTMonthsum.Caption = "FTMonthsum"
        Me.nFTMonthsum.FieldName = "FTMonthsum"
        Me.nFTMonthsum.Name = "nFTMonthsum"
        Me.nFTMonthsum.OptionsColumn.AllowEdit = False
        Me.nFTMonthsum.Visible = True
        Me.nFTMonthsum.VisibleIndex = 0
        Me.nFTMonthsum.Width = 108
        '
        'nFTToTalWork
        '
        Me.nFTToTalWork.AppearanceHeader.Options.UseTextOptions = True
        Me.nFTToTalWork.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.nFTToTalWork.Caption = "FTToTalWork"
        Me.nFTToTalWork.FieldName = "FTToTalWork"
        Me.nFTToTalWork.Name = "nFTToTalWork"
        Me.nFTToTalWork.OptionsColumn.AllowEdit = False
        Me.nFTToTalWork.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.nFTToTalWork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.nFTToTalWork.OptionsColumn.ReadOnly = True
        Me.nFTToTalWork.Visible = True
        Me.nFTToTalWork.VisibleIndex = 1
        Me.nFTToTalWork.Width = 180
        '
        'nFTMalesum
        '
        Me.nFTMalesum.AppearanceHeader.Options.UseTextOptions = True
        Me.nFTMalesum.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.nFTMalesum.Caption = "FTMalesum"
        Me.nFTMalesum.FieldName = "FTMalesum"
        Me.nFTMalesum.Name = "nFTMalesum"
        Me.nFTMalesum.OptionsColumn.AllowEdit = False
        Me.nFTMalesum.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.nFTMalesum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.nFTMalesum.OptionsColumn.ReadOnly = True
        Me.nFTMalesum.Visible = True
        Me.nFTMalesum.VisibleIndex = 2
        Me.nFTMalesum.Width = 157
        '
        'nFTFeMalesum
        '
        Me.nFTFeMalesum.AppearanceHeader.Options.UseTextOptions = True
        Me.nFTFeMalesum.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.nFTFeMalesum.Caption = "FTFeMalesum"
        Me.nFTFeMalesum.FieldName = "FTFeMalesum"
        Me.nFTFeMalesum.Name = "nFTFeMalesum"
        Me.nFTFeMalesum.OptionsColumn.AllowEdit = False
        Me.nFTFeMalesum.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.nFTFeMalesum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.nFTFeMalesum.OptionsColumn.ReadOnly = True
        Me.nFTFeMalesum.OptionsColumn.TabStop = False
        Me.nFTFeMalesum.Visible = True
        Me.nFTFeMalesum.VisibleIndex = 3
        Me.nFTFeMalesum.Width = 198
        '
        'GridView8
        '
        Me.GridView8.GridControl = Me.ogcsumnew
        Me.GridView8.Name = "GridView8"
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.GroupControl4)
        Me.XtraTabPage2.Controls.Add(Me.GroupControl5)
        Me.XtraTabPage2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(1368, 828)
        Me.XtraTabPage2.Text = "วันที่ลาออก"
        '
        'GroupControl4
        '
        Me.GroupControl4.Controls.Add(Me.ogcend)
        Me.GroupControl4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl4.Location = New System.Drawing.Point(0, 99)
        Me.GroupControl4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl4.Name = "GroupControl4"
        Me.GroupControl4.Size = New System.Drawing.Size(1368, 729)
        Me.GroupControl4.TabIndex = 15
        Me.GroupControl4.Text = "รายละเอียด"
        '
        'ogcend
        '
        Me.ogcend.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcend.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcend.Location = New System.Drawing.Point(2, 27)
        Me.ogcend.MainView = Me.ogvend
        Me.ogcend.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcend.Name = "ogcend"
        Me.ogcend.Size = New System.Drawing.Size(1364, 700)
        Me.ogcend.TabIndex = 2
        Me.ogcend.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvend, Me.GridView7})
        '
        'ogvend
        '
        Me.ogvend.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.dFTEmpName, Me.dFTUnitSectCode, Me.dFTUnitSectName, Me.dFTEmpCode, Me.dFNEmpSex, Me.dFDDateEnd, Me.FTResignName, Me.GridColumn45})
        Me.ogvend.GridControl = Me.ogcend
        Me.ogvend.Name = "ogvend"
        Me.ogvend.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvend.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.ogvend.OptionsView.ColumnAutoWidth = False
        Me.ogvend.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvend.OptionsView.ShowGroupPanel = False
        Me.ogvend.Tag = "2|"
        '
        'dFTEmpName
        '
        Me.dFTEmpName.Caption = "FTEmpName"
        Me.dFTEmpName.FieldName = "FTEmpName"
        Me.dFTEmpName.Name = "dFTEmpName"
        Me.dFTEmpName.OptionsColumn.AllowEdit = False
        Me.dFTEmpName.Visible = True
        Me.dFTEmpName.VisibleIndex = 1
        Me.dFTEmpName.Width = 108
        '
        'dFTUnitSectCode
        '
        Me.dFTUnitSectCode.AppearanceHeader.Options.UseTextOptions = True
        Me.dFTUnitSectCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.dFTUnitSectCode.Caption = "FTUnitSectCode"
        Me.dFTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.dFTUnitSectCode.Name = "dFTUnitSectCode"
        Me.dFTUnitSectCode.OptionsColumn.AllowEdit = False
        Me.dFTUnitSectCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.dFTUnitSectCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.dFTUnitSectCode.OptionsColumn.ReadOnly = True
        Me.dFTUnitSectCode.Visible = True
        Me.dFTUnitSectCode.VisibleIndex = 2
        Me.dFTUnitSectCode.Width = 180
        '
        'dFTUnitSectName
        '
        Me.dFTUnitSectName.AppearanceHeader.Options.UseTextOptions = True
        Me.dFTUnitSectName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.dFTUnitSectName.Caption = "FTUnitSectName"
        Me.dFTUnitSectName.FieldName = "FTUnitSectName"
        Me.dFTUnitSectName.Name = "dFTUnitSectName"
        Me.dFTUnitSectName.OptionsColumn.AllowEdit = False
        Me.dFTUnitSectName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.dFTUnitSectName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.dFTUnitSectName.OptionsColumn.ReadOnly = True
        Me.dFTUnitSectName.Visible = True
        Me.dFTUnitSectName.VisibleIndex = 3
        Me.dFTUnitSectName.Width = 157
        '
        'dFTEmpCode
        '
        Me.dFTEmpCode.AppearanceHeader.Options.UseTextOptions = True
        Me.dFTEmpCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.dFTEmpCode.Caption = "FTEmpCode"
        Me.dFTEmpCode.FieldName = "FTEmpCode"
        Me.dFTEmpCode.Name = "dFTEmpCode"
        Me.dFTEmpCode.OptionsColumn.AllowEdit = False
        Me.dFTEmpCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.dFTEmpCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.dFTEmpCode.OptionsColumn.ReadOnly = True
        Me.dFTEmpCode.OptionsColumn.TabStop = False
        Me.dFTEmpCode.Visible = True
        Me.dFTEmpCode.VisibleIndex = 0
        Me.dFTEmpCode.Width = 198
        '
        'dFNEmpSex
        '
        Me.dFNEmpSex.AppearanceHeader.Options.UseTextOptions = True
        Me.dFNEmpSex.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.dFNEmpSex.Caption = "FNEmpSex"
        Me.dFNEmpSex.FieldName = "FNEmpSex"
        Me.dFNEmpSex.Name = "dFNEmpSex"
        Me.dFNEmpSex.OptionsColumn.AllowEdit = False
        Me.dFNEmpSex.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.dFNEmpSex.OptionsColumn.ReadOnly = True
        Me.dFNEmpSex.OptionsColumn.TabStop = False
        Me.dFNEmpSex.Visible = True
        Me.dFNEmpSex.VisibleIndex = 4
        Me.dFNEmpSex.Width = 171
        '
        'dFDDateEnd
        '
        Me.dFDDateEnd.AppearanceHeader.Options.UseTextOptions = True
        Me.dFDDateEnd.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.dFDDateEnd.Caption = "FDDateEnd"
        Me.dFDDateEnd.FieldName = "FDDateEnd"
        Me.dFDDateEnd.Name = "dFDDateEnd"
        Me.dFDDateEnd.OptionsColumn.AllowEdit = False
        Me.dFDDateEnd.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.dFDDateEnd.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.dFDDateEnd.OptionsColumn.ReadOnly = True
        Me.dFDDateEnd.Visible = True
        Me.dFDDateEnd.VisibleIndex = 5
        Me.dFDDateEnd.Width = 160
        '
        'FTResignName
        '
        Me.FTResignName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTResignName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTResignName.Caption = "FTResignName"
        Me.FTResignName.FieldName = "FTResignName"
        Me.FTResignName.Name = "FTResignName"
        Me.FTResignName.OptionsColumn.AllowEdit = False
        Me.FTResignName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTResignName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTResignName.OptionsColumn.ReadOnly = True
        Me.FTResignName.Visible = True
        Me.FTResignName.VisibleIndex = 6
        Me.FTResignName.Width = 125
        '
        'GridColumn45
        '
        Me.GridColumn45.Caption = "FNEmpStatus"
        Me.GridColumn45.FieldName = "FNEmpStatus"
        Me.GridColumn45.Name = "GridColumn45"
        Me.GridColumn45.OptionsColumn.AllowEdit = False
        Me.GridColumn45.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn45.OptionsColumn.AllowMove = False
        Me.GridColumn45.OptionsColumn.ReadOnly = True
        '
        'GridView7
        '
        Me.GridView7.GridControl = Me.ogcend
        Me.GridView7.Name = "GridView7"
        '
        'GroupControl5
        '
        Me.GroupControl5.Controls.Add(Me.ogcsumrea)
        Me.GroupControl5.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl5.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl5.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl5.Name = "GroupControl5"
        Me.GroupControl5.Size = New System.Drawing.Size(1368, 99)
        Me.GroupControl5.TabIndex = 14
        Me.GroupControl5.Text = "summerry"
        '
        'ogcsumrea
        '
        Me.ogcsumrea.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcsumrea.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcsumrea.Location = New System.Drawing.Point(2, 27)
        Me.ogcsumrea.MainView = Me.ogvsumrea
        Me.ogcsumrea.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcsumrea.Name = "ogcsumrea"
        Me.ogcsumrea.Size = New System.Drawing.Size(1364, 70)
        Me.ogcsumrea.TabIndex = 2
        Me.ogcsumrea.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvsumrea, Me.GridView6})
        '
        'ogvsumrea
        '
        Me.ogvsumrea.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn46, Me.rFTMonthsum, Me.rFTToTalWork, Me.rFTMalesum, Me.rFTFeMalesum})
        Me.ogvsumrea.GridControl = Me.ogcsumrea
        Me.ogvsumrea.Name = "ogvsumrea"
        Me.ogvsumrea.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvsumrea.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.ogvsumrea.OptionsView.ColumnAutoWidth = False
        Me.ogvsumrea.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvsumrea.OptionsView.ShowGroupPanel = False
        Me.ogvsumrea.Tag = "2|"
        '
        'GridColumn46
        '
        Me.GridColumn46.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn46.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn46.Caption = "FNHSysEmpID"
        Me.GridColumn46.FieldName = "FNHSysEmpID"
        Me.GridColumn46.Name = "GridColumn46"
        Me.GridColumn46.OptionsColumn.AllowEdit = False
        Me.GridColumn46.OptionsColumn.AllowMove = False
        Me.GridColumn46.OptionsColumn.ReadOnly = True
        Me.GridColumn46.OptionsColumn.TabStop = False
        '
        'rFTMonthsum
        '
        Me.rFTMonthsum.Caption = "FTMonthsum"
        Me.rFTMonthsum.FieldName = "FTMonthsum"
        Me.rFTMonthsum.Name = "rFTMonthsum"
        Me.rFTMonthsum.OptionsColumn.AllowEdit = False
        Me.rFTMonthsum.Visible = True
        Me.rFTMonthsum.VisibleIndex = 0
        Me.rFTMonthsum.Width = 108
        '
        'rFTToTalWork
        '
        Me.rFTToTalWork.AppearanceHeader.Options.UseTextOptions = True
        Me.rFTToTalWork.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.rFTToTalWork.Caption = "FTToTalWork"
        Me.rFTToTalWork.FieldName = "FTToTalWork"
        Me.rFTToTalWork.Name = "rFTToTalWork"
        Me.rFTToTalWork.OptionsColumn.AllowEdit = False
        Me.rFTToTalWork.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.rFTToTalWork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.rFTToTalWork.OptionsColumn.ReadOnly = True
        Me.rFTToTalWork.Visible = True
        Me.rFTToTalWork.VisibleIndex = 1
        Me.rFTToTalWork.Width = 180
        '
        'rFTMalesum
        '
        Me.rFTMalesum.AppearanceHeader.Options.UseTextOptions = True
        Me.rFTMalesum.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.rFTMalesum.Caption = "FTMalesum"
        Me.rFTMalesum.FieldName = "FTMalesum"
        Me.rFTMalesum.Name = "rFTMalesum"
        Me.rFTMalesum.OptionsColumn.AllowEdit = False
        Me.rFTMalesum.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.rFTMalesum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.rFTMalesum.OptionsColumn.ReadOnly = True
        Me.rFTMalesum.Visible = True
        Me.rFTMalesum.VisibleIndex = 2
        Me.rFTMalesum.Width = 157
        '
        'rFTFeMalesum
        '
        Me.rFTFeMalesum.AppearanceHeader.Options.UseTextOptions = True
        Me.rFTFeMalesum.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.rFTFeMalesum.Caption = "FTFeMalesum"
        Me.rFTFeMalesum.FieldName = "FTFeMalesum"
        Me.rFTFeMalesum.Name = "rFTFeMalesum"
        Me.rFTFeMalesum.OptionsColumn.AllowEdit = False
        Me.rFTFeMalesum.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.rFTFeMalesum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.rFTFeMalesum.OptionsColumn.ReadOnly = True
        Me.rFTFeMalesum.OptionsColumn.TabStop = False
        Me.rFTFeMalesum.Visible = True
        Me.rFTFeMalesum.VisibleIndex = 3
        Me.rFTFeMalesum.Width = 198
        '
        'GridView6
        '
        Me.GridView6.GridControl = Me.ogcsumrea
        Me.GridView6.Name = "GridView6"
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.GroupControl2)
        Me.XtraTabPage1.Controls.Add(Me.GroupControl3)
        Me.XtraTabPage1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1368, 828)
        Me.XtraTabPage1.Text = "วันที่เริ่มงาน(พนงใหม่)"
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.ogcnew)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 98)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(1368, 730)
        Me.GroupControl2.TabIndex = 15
        Me.GroupControl2.Text = "รายละเอียด"
        '
        'ogcnew
        '
        Me.ogcnew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcnew.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcnew.Location = New System.Drawing.Point(2, 27)
        Me.ogcnew.MainView = Me.ogvnew
        Me.ogcnew.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcnew.Name = "ogcnew"
        Me.ogcnew.Size = New System.Drawing.Size(1364, 701)
        Me.ogcnew.TabIndex = 2
        Me.ogcnew.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvnew, Me.GridView9})
        '
        'ogvnew
        '
        Me.ogvnew.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTEmpName, Me.cFTUnitSectCode, Me.cFTUnitSectName, Me.cFTEmpCode, Me.cFNEmpSex, Me.FDDateStart, Me.GridColumn24})
        Me.ogvnew.GridControl = Me.ogcnew
        Me.ogvnew.Name = "ogvnew"
        Me.ogvnew.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvnew.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.ogvnew.OptionsView.ColumnAutoWidth = False
        Me.ogvnew.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvnew.OptionsView.ShowGroupPanel = False
        Me.ogvnew.Tag = "2|"
        '
        'cFTEmpName
        '
        Me.cFTEmpName.Caption = "FTEmpName"
        Me.cFTEmpName.FieldName = "FTEmpName"
        Me.cFTEmpName.Name = "cFTEmpName"
        Me.cFTEmpName.OptionsColumn.AllowEdit = False
        Me.cFTEmpName.Visible = True
        Me.cFTEmpName.VisibleIndex = 1
        Me.cFTEmpName.Width = 108
        '
        'cFTUnitSectCode
        '
        Me.cFTUnitSectCode.AppearanceHeader.Options.UseTextOptions = True
        Me.cFTUnitSectCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFTUnitSectCode.Caption = "FTUnitSectCode"
        Me.cFTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.cFTUnitSectCode.Name = "cFTUnitSectCode"
        Me.cFTUnitSectCode.OptionsColumn.AllowEdit = False
        Me.cFTUnitSectCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTUnitSectCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTUnitSectCode.OptionsColumn.ReadOnly = True
        Me.cFTUnitSectCode.Visible = True
        Me.cFTUnitSectCode.VisibleIndex = 2
        Me.cFTUnitSectCode.Width = 180
        '
        'cFTUnitSectName
        '
        Me.cFTUnitSectName.AppearanceHeader.Options.UseTextOptions = True
        Me.cFTUnitSectName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFTUnitSectName.Caption = "FTUnitSectName"
        Me.cFTUnitSectName.FieldName = "FTUnitSectName"
        Me.cFTUnitSectName.Name = "cFTUnitSectName"
        Me.cFTUnitSectName.OptionsColumn.AllowEdit = False
        Me.cFTUnitSectName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTUnitSectName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTUnitSectName.OptionsColumn.ReadOnly = True
        Me.cFTUnitSectName.Visible = True
        Me.cFTUnitSectName.VisibleIndex = 3
        Me.cFTUnitSectName.Width = 157
        '
        'cFTEmpCode
        '
        Me.cFTEmpCode.AppearanceHeader.Options.UseTextOptions = True
        Me.cFTEmpCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFTEmpCode.Caption = "FTEmpCode"
        Me.cFTEmpCode.FieldName = "FTEmpCode"
        Me.cFTEmpCode.Name = "cFTEmpCode"
        Me.cFTEmpCode.OptionsColumn.AllowEdit = False
        Me.cFTEmpCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTEmpCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFTEmpCode.OptionsColumn.ReadOnly = True
        Me.cFTEmpCode.OptionsColumn.TabStop = False
        Me.cFTEmpCode.Visible = True
        Me.cFTEmpCode.VisibleIndex = 0
        Me.cFTEmpCode.Width = 198
        '
        'cFNEmpSex
        '
        Me.cFNEmpSex.AppearanceHeader.Options.UseTextOptions = True
        Me.cFNEmpSex.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.cFNEmpSex.Caption = "FNEmpSex"
        Me.cFNEmpSex.FieldName = "FNEmpSex"
        Me.cFNEmpSex.Name = "cFNEmpSex"
        Me.cFNEmpSex.OptionsColumn.AllowEdit = False
        Me.cFNEmpSex.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.cFNEmpSex.OptionsColumn.ReadOnly = True
        Me.cFNEmpSex.OptionsColumn.TabStop = False
        Me.cFNEmpSex.Visible = True
        Me.cFNEmpSex.VisibleIndex = 4
        Me.cFNEmpSex.Width = 171
        '
        'FDDateStart
        '
        Me.FDDateStart.AppearanceHeader.Options.UseTextOptions = True
        Me.FDDateStart.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDDateStart.Caption = "FDDateStart"
        Me.FDDateStart.FieldName = "FDDateStart"
        Me.FDDateStart.Name = "FDDateStart"
        Me.FDDateStart.OptionsColumn.AllowEdit = False
        Me.FDDateStart.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.FDDateStart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.FDDateStart.OptionsColumn.ReadOnly = True
        Me.FDDateStart.Visible = True
        Me.FDDateStart.VisibleIndex = 5
        Me.FDDateStart.Width = 160
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "FNEmpStatus"
        Me.GridColumn24.FieldName = "FNEmpStatus"
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.OptionsColumn.AllowEdit = False
        Me.GridColumn24.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn24.OptionsColumn.AllowMove = False
        Me.GridColumn24.OptionsColumn.ReadOnly = True
        '
        'GridView9
        '
        Me.GridView9.GridControl = Me.ogcnew
        Me.GridView9.Name = "GridView9"
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.ogcsumnew)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupControl3.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.Size = New System.Drawing.Size(1368, 98)
        Me.GroupControl3.TabIndex = 14
        Me.GroupControl3.Text = "summerry"
        '
        'otpsummary
        '
        Me.otpsummary.Controls.Add(Me.GroupControl1)
        Me.otpsummary.Controls.Add(Me.ogbpoamt)
        Me.otpsummary.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otpsummary.Name = "otpsummary"
        Me.otpsummary.Size = New System.Drawing.Size(1368, 828)
        Me.otpsummary.Text = "วันทำงานแรกของเดือน"
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.ogcTime)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 164)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1368, 664)
        Me.GroupControl1.TabIndex = 13
        Me.GroupControl1.Text = "รายละเอียด"
        '
        'ogcTime
        '
        Me.ogcTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcTime.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcTime.Location = New System.Drawing.Point(2, 27)
        Me.ogcTime.MainView = Me.ogvtime
        Me.ogcTime.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcTime.Name = "ogcTime"
        Me.ogcTime.Size = New System.Drawing.Size(1364, 635)
        Me.ogcTime.TabIndex = 2
        Me.ogcTime.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvtime, Me.GridView11})
        '
        'ogvtime
        '
        Me.ogvtime.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTEmpName, Me.FTUnitSectCode, FTUnitSectName, Me.GridColumn29, Me.FNEmpSex, Me.FNTime, Me.GridColumn34, Me.FTDateTrans})
        Me.ogvtime.GridControl = Me.ogcTime
        Me.ogvtime.Name = "ogvtime"
        Me.ogvtime.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvtime.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.ogvtime.OptionsView.ColumnAutoWidth = False
        Me.ogvtime.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvtime.OptionsView.ShowGroupPanel = False
        Me.ogvtime.Tag = "2|"
        '
        'FTEmpName
        '
        Me.FTEmpName.Caption = "FTEmpName"
        Me.FTEmpName.FieldName = "FTEmpName"
        Me.FTEmpName.Name = "FTEmpName"
        Me.FTEmpName.OptionsColumn.AllowEdit = False
        Me.FTEmpName.Visible = True
        Me.FTEmpName.VisibleIndex = 1
        Me.FTEmpName.Width = 241
        '
        'FTUnitSectCode
        '
        Me.FTUnitSectCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUnitSectCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitSectCode.Caption = "FTUnitSectCode"
        Me.FTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.FTUnitSectCode.Name = "FTUnitSectCode"
        Me.FTUnitSectCode.OptionsColumn.AllowEdit = False
        Me.FTUnitSectCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTUnitSectCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTUnitSectCode.OptionsColumn.ReadOnly = True
        Me.FTUnitSectCode.Visible = True
        Me.FTUnitSectCode.VisibleIndex = 2
        Me.FTUnitSectCode.Width = 180
        '
        'GridColumn29
        '
        Me.GridColumn29.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn29.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn29.Caption = "FTEmpCode"
        Me.GridColumn29.FieldName = "FTEmpCode"
        Me.GridColumn29.Name = "GridColumn29"
        Me.GridColumn29.OptionsColumn.AllowEdit = False
        Me.GridColumn29.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn29.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn29.OptionsColumn.ReadOnly = True
        Me.GridColumn29.OptionsColumn.TabStop = False
        Me.GridColumn29.Visible = True
        Me.GridColumn29.VisibleIndex = 0
        Me.GridColumn29.Width = 198
        '
        'FNEmpSex
        '
        Me.FNEmpSex.AppearanceHeader.Options.UseTextOptions = True
        Me.FNEmpSex.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNEmpSex.Caption = "FNEmpSex"
        Me.FNEmpSex.FieldName = "FNEmpSex"
        Me.FNEmpSex.Name = "FNEmpSex"
        Me.FNEmpSex.OptionsColumn.AllowEdit = False
        Me.FNEmpSex.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.FNEmpSex.OptionsColumn.ReadOnly = True
        Me.FNEmpSex.OptionsColumn.TabStop = False
        Me.FNEmpSex.Visible = True
        Me.FNEmpSex.VisibleIndex = 4
        Me.FNEmpSex.Width = 171
        '
        'FNTime
        '
        Me.FNTime.AppearanceHeader.Options.UseTextOptions = True
        Me.FNTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNTime.Caption = "FNTime"
        Me.FNTime.FieldName = "FNTime"
        Me.FNTime.Name = "FNTime"
        Me.FNTime.OptionsColumn.AllowEdit = False
        Me.FNTime.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.FNTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.FNTime.OptionsColumn.ReadOnly = True
        Me.FNTime.Visible = True
        Me.FNTime.VisibleIndex = 5
        Me.FNTime.Width = 125
        '
        'GridColumn34
        '
        Me.GridColumn34.Caption = "FNEmpStatus"
        Me.GridColumn34.FieldName = "FNEmpStatus"
        Me.GridColumn34.Name = "GridColumn34"
        Me.GridColumn34.OptionsColumn.AllowEdit = False
        Me.GridColumn34.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.GridColumn34.OptionsColumn.AllowMove = False
        Me.GridColumn34.OptionsColumn.ReadOnly = True
        '
        'FTDateTrans
        '
        Me.FTDateTrans.Caption = "FTDateTrans"
        Me.FTDateTrans.FieldName = "FTDateTrans"
        Me.FTDateTrans.MinWidth = 24
        Me.FTDateTrans.Name = "FTDateTrans"
        Me.FTDateTrans.OptionsColumn.AllowEdit = False
        Me.FTDateTrans.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTDateTrans.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTDateTrans.OptionsColumn.ReadOnly = True
        Me.FTDateTrans.Visible = True
        Me.FTDateTrans.VisibleIndex = 6
        Me.FTDateTrans.Width = 161
        '
        'GridView11
        '
        Me.GridView11.GridControl = Me.ogcTime
        Me.GridView11.Name = "GridView11"
        '
        'ogbpoamt
        '
        Me.ogbpoamt.Controls.Add(Me.ogcsumTime)
        Me.ogbpoamt.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogbpoamt.Location = New System.Drawing.Point(0, 0)
        Me.ogbpoamt.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbpoamt.Name = "ogbpoamt"
        Me.ogbpoamt.Size = New System.Drawing.Size(1368, 164)
        Me.ogbpoamt.TabIndex = 12
        Me.ogbpoamt.Text = "summerry"
        '
        'ogcsumTime
        '
        Me.ogcsumTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcsumTime.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcsumTime.Location = New System.Drawing.Point(2, 27)
        Me.ogcsumTime.MainView = Me.ogvSumTime
        Me.ogcsumTime.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcsumTime.Name = "ogcsumTime"
        Me.ogcsumTime.Size = New System.Drawing.Size(1364, 135)
        Me.ogcsumTime.TabIndex = 2
        Me.ogcsumTime.TabStop = False
        Me.ogcsumTime.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvSumTime, Me.GridView10})
        '
        'ogvSumTime
        '
        Me.ogvSumTime.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn15, Me.FTMonthsum, Me.FTToTalWork, Me.FTMalesum, Me.FTFeMalesum, Me.FTDaysum})
        Me.ogvSumTime.GridControl = Me.ogcsumTime
        Me.ogvSumTime.Name = "ogvSumTime"
        Me.ogvSumTime.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvSumTime.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.ogvSumTime.OptionsView.ColumnAutoWidth = False
        Me.ogvSumTime.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvSumTime.OptionsView.ShowGroupPanel = False
        Me.ogvSumTime.Tag = "2|"
        '
        'GridColumn15
        '
        Me.GridColumn15.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn15.Caption = "FNHSysEmpID"
        Me.GridColumn15.FieldName = "FNHSysEmpID"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.OptionsColumn.AllowEdit = False
        Me.GridColumn15.OptionsColumn.AllowMove = False
        Me.GridColumn15.OptionsColumn.ReadOnly = True
        Me.GridColumn15.OptionsColumn.TabStop = False
        '
        'FTMonthsum
        '
        Me.FTMonthsum.Caption = "FTMonthsum"
        Me.FTMonthsum.FieldName = "FTMonthsum"
        Me.FTMonthsum.Name = "FTMonthsum"
        Me.FTMonthsum.OptionsColumn.AllowEdit = False
        Me.FTMonthsum.Visible = True
        Me.FTMonthsum.VisibleIndex = 0
        Me.FTMonthsum.Width = 108
        '
        'FTToTalWork
        '
        Me.FTToTalWork.AppearanceHeader.Options.UseTextOptions = True
        Me.FTToTalWork.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTToTalWork.Caption = "FTToTalWork"
        Me.FTToTalWork.FieldName = "FTToTalWork"
        Me.FTToTalWork.Name = "FTToTalWork"
        Me.FTToTalWork.OptionsColumn.AllowEdit = False
        Me.FTToTalWork.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTToTalWork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTToTalWork.OptionsColumn.ReadOnly = True
        Me.FTToTalWork.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ToTalWork", "{0:n0}")})
        Me.FTToTalWork.Visible = True
        Me.FTToTalWork.VisibleIndex = 1
        Me.FTToTalWork.Width = 180
        '
        'FTMalesum
        '
        Me.FTMalesum.AppearanceHeader.Options.UseTextOptions = True
        Me.FTMalesum.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTMalesum.Caption = "FTMalesum"
        Me.FTMalesum.FieldName = "FTMalesum"
        Me.FTMalesum.Name = "FTMalesum"
        Me.FTMalesum.OptionsColumn.AllowEdit = False
        Me.FTMalesum.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTMalesum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTMalesum.OptionsColumn.ReadOnly = True
        Me.FTMalesum.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "FTUnitSectName", "{0}")})
        Me.FTMalesum.Visible = True
        Me.FTMalesum.VisibleIndex = 2
        Me.FTMalesum.Width = 157
        '
        'FTFeMalesum
        '
        Me.FTFeMalesum.AppearanceHeader.Options.UseTextOptions = True
        Me.FTFeMalesum.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTFeMalesum.Caption = "FTFeMalesum"
        Me.FTFeMalesum.FieldName = "FTFeMalesum"
        Me.FTFeMalesum.Name = "FTFeMalesum"
        Me.FTFeMalesum.OptionsColumn.AllowEdit = False
        Me.FTFeMalesum.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTFeMalesum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTFeMalesum.OptionsColumn.ReadOnly = True
        Me.FTFeMalesum.OptionsColumn.TabStop = False
        Me.FTFeMalesum.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "FTEmpCode", "{0}")})
        Me.FTFeMalesum.Visible = True
        Me.FTFeMalesum.VisibleIndex = 3
        Me.FTFeMalesum.Width = 198
        '
        'FTDaysum
        '
        Me.FTDaysum.Caption = "FTDaysum"
        Me.FTDaysum.FieldName = "FTDaysum"
        Me.FTDaysum.MinWidth = 24
        Me.FTDaysum.Name = "FTDaysum"
        Me.FTDaysum.OptionsColumn.AllowEdit = False
        Me.FTDaysum.OptionsColumn.ReadOnly = True
        Me.FTDaysum.Visible = True
        Me.FTDaysum.VisibleIndex = 4
        Me.FTDaysum.Width = 233
        '
        'GridView10
        '
        Me.GridView10.GridControl = Me.ogcsumTime
        Me.GridView10.Name = "GridView10"
        '
        'สรุปรายปี
        '
        Me.สรุปรายปี.Controls.Add(Me.GroupControl9)
        Me.สรุปรายปี.Name = "สรุปรายปี"
        Me.สรุปรายปี.Size = New System.Drawing.Size(1368, 828)
        Me.สรุปรายปี.Text = "สรุปรายปี"
        '
        'GroupControl9
        '
        Me.GroupControl9.Controls.Add(Me.GroupControl12)
        Me.GroupControl9.Controls.Add(Me.GroupControl11)
        Me.GroupControl9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl9.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl9.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl9.Name = "GroupControl9"
        Me.GroupControl9.ShowCaption = False
        Me.GroupControl9.Size = New System.Drawing.Size(1368, 828)
        Me.GroupControl9.TabIndex = 14
        Me.GroupControl9.Text = "summerryFemale"
        '
        'GroupControl12
        '
        Me.GroupControl12.AutoSize = True
        Me.GroupControl12.Controls.Add(Me.ogcsum)
        Me.GroupControl12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl12.Location = New System.Drawing.Point(2, 2)
        Me.GroupControl12.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl12.Name = "GroupControl12"
        Me.GroupControl12.Size = New System.Drawing.Size(1364, 824)
        Me.GroupControl12.TabIndex = 16
        Me.GroupControl12.Text = "summerry"
        '
        'ogcsum
        '
        Me.ogcsum.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcsum.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcsum.Location = New System.Drawing.Point(2, 27)
        Me.ogcsum.MainView = Me.ogvsum
        Me.ogcsum.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcsum.Name = "ogcsum"
        Me.ogcsum.Size = New System.Drawing.Size(1360, 795)
        Me.ogcsum.TabIndex = 2
        Me.ogcsum.TabStop = False
        Me.ogcsum.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvsum, Me.GridView14})
        '
        'ogvsum
        '
        Me.ogvsum.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn17, Me.DetailS, Me.FTS1, Me.FTS2, Me.FTS3, Me.FTS4, Me.FTS5, Me.FTS6, Me.FTS7, Me.FTS8, Me.FTS9, Me.FTS10, Me.FTS11, Me.FTS12})
        Me.ogvsum.GridControl = Me.ogcsum
        Me.ogvsum.Name = "ogvsum"
        Me.ogvsum.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvsum.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.ogvsum.OptionsView.ColumnAutoWidth = False
        Me.ogvsum.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvsum.OptionsView.ShowGroupPanel = False
        Me.ogvsum.Tag = "2|"
        '
        'GridColumn17
        '
        Me.GridColumn17.AppearanceHeader.Options.UseTextOptions = True
        Me.GridColumn17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GridColumn17.Caption = "FNHSysEmpID"
        Me.GridColumn17.FieldName = "FNHSysEmpID"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.OptionsColumn.AllowEdit = False
        Me.GridColumn17.OptionsColumn.AllowMove = False
        Me.GridColumn17.OptionsColumn.ReadOnly = True
        Me.GridColumn17.OptionsColumn.TabStop = False
        '
        'DetailS
        '
        Me.DetailS.Caption = "Detail"
        Me.DetailS.FieldName = "Detail"
        Me.DetailS.Name = "DetailS"
        Me.DetailS.OptionsColumn.AllowEdit = False
        Me.DetailS.Visible = True
        Me.DetailS.VisibleIndex = 0
        Me.DetailS.Width = 270
        '
        'FTS1
        '
        Me.FTS1.AppearanceHeader.Options.UseTextOptions = True
        Me.FTS1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTS1.Caption = "มกราคม"
        Me.FTS1.DisplayFormat.FormatString = "{0:n2}"
        Me.FTS1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FTS1.FieldName = "1"
        Me.FTS1.Name = "FTS1"
        Me.FTS1.OptionsColumn.AllowEdit = False
        Me.FTS1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTS1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTS1.OptionsColumn.ReadOnly = True
        Me.FTS1.OptionsColumn.TabStop = False
        Me.FTS1.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "FTEmpCode", "{0}")})
        Me.FTS1.Visible = True
        Me.FTS1.VisibleIndex = 1
        Me.FTS1.Width = 124
        '
        'FTS2
        '
        Me.FTS2.Caption = "กุมภาพันธ์"
        Me.FTS2.DisplayFormat.FormatString = "{0:n2}"
        Me.FTS2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FTS2.FieldName = "2"
        Me.FTS2.MinWidth = 25
        Me.FTS2.Name = "FTS2"
        Me.FTS2.OptionsColumn.AllowEdit = False
        Me.FTS2.OptionsColumn.ReadOnly = True
        Me.FTS2.Visible = True
        Me.FTS2.VisibleIndex = 2
        Me.FTS2.Width = 94
        '
        'FTS3
        '
        Me.FTS3.Caption = "มีนาคม"
        Me.FTS3.DisplayFormat.FormatString = "{0:n2}"
        Me.FTS3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FTS3.FieldName = "3"
        Me.FTS3.MinWidth = 25
        Me.FTS3.Name = "FTS3"
        Me.FTS3.OptionsColumn.AllowEdit = False
        Me.FTS3.OptionsColumn.ReadOnly = True
        Me.FTS3.Visible = True
        Me.FTS3.VisibleIndex = 3
        Me.FTS3.Width = 94
        '
        'FTS4
        '
        Me.FTS4.Caption = "เมษายน"
        Me.FTS4.DisplayFormat.FormatString = "{0:n2}"
        Me.FTS4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FTS4.FieldName = "4"
        Me.FTS4.MinWidth = 25
        Me.FTS4.Name = "FTS4"
        Me.FTS4.OptionsColumn.AllowEdit = False
        Me.FTS4.OptionsColumn.ReadOnly = True
        Me.FTS4.Visible = True
        Me.FTS4.VisibleIndex = 4
        Me.FTS4.Width = 94
        '
        'FTS5
        '
        Me.FTS5.Caption = "พฤษภาคม"
        Me.FTS5.DisplayFormat.FormatString = "{0:n2}"
        Me.FTS5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FTS5.FieldName = "5"
        Me.FTS5.MinWidth = 25
        Me.FTS5.Name = "FTS5"
        Me.FTS5.OptionsColumn.AllowEdit = False
        Me.FTS5.OptionsColumn.ReadOnly = True
        Me.FTS5.Visible = True
        Me.FTS5.VisibleIndex = 5
        Me.FTS5.Width = 94
        '
        'FTS6
        '
        Me.FTS6.Caption = "มิถุนายน"
        Me.FTS6.DisplayFormat.FormatString = "{0:n2}"
        Me.FTS6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FTS6.FieldName = "6"
        Me.FTS6.MinWidth = 25
        Me.FTS6.Name = "FTS6"
        Me.FTS6.OptionsColumn.AllowEdit = False
        Me.FTS6.OptionsColumn.ReadOnly = True
        Me.FTS6.Visible = True
        Me.FTS6.VisibleIndex = 6
        Me.FTS6.Width = 94
        '
        'FTS7
        '
        Me.FTS7.Caption = "กรกฎาคม"
        Me.FTS7.DisplayFormat.FormatString = "{0:n2}"
        Me.FTS7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FTS7.FieldName = "7"
        Me.FTS7.MinWidth = 25
        Me.FTS7.Name = "FTS7"
        Me.FTS7.OptionsColumn.AllowEdit = False
        Me.FTS7.OptionsColumn.ReadOnly = True
        Me.FTS7.Visible = True
        Me.FTS7.VisibleIndex = 7
        Me.FTS7.Width = 94
        '
        'FTS8
        '
        Me.FTS8.Caption = "สิงหาคม"
        Me.FTS8.DisplayFormat.FormatString = "{0:n2}"
        Me.FTS8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FTS8.FieldName = "8"
        Me.FTS8.MinWidth = 25
        Me.FTS8.Name = "FTS8"
        Me.FTS8.OptionsColumn.AllowEdit = False
        Me.FTS8.OptionsColumn.ReadOnly = True
        Me.FTS8.Visible = True
        Me.FTS8.VisibleIndex = 8
        Me.FTS8.Width = 94
        '
        'FTS9
        '
        Me.FTS9.Caption = "กันยายน"
        Me.FTS9.DisplayFormat.FormatString = "{0:n2}"
        Me.FTS9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FTS9.FieldName = "9"
        Me.FTS9.MinWidth = 25
        Me.FTS9.Name = "FTS9"
        Me.FTS9.OptionsColumn.AllowEdit = False
        Me.FTS9.OptionsColumn.ReadOnly = True
        Me.FTS9.Visible = True
        Me.FTS9.VisibleIndex = 9
        Me.FTS9.Width = 94
        '
        'FTS10
        '
        Me.FTS10.Caption = "ตุลาคม"
        Me.FTS10.DisplayFormat.FormatString = "{0:n2}"
        Me.FTS10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FTS10.FieldName = "10"
        Me.FTS10.MinWidth = 25
        Me.FTS10.Name = "FTS10"
        Me.FTS10.OptionsColumn.AllowEdit = False
        Me.FTS10.OptionsColumn.ReadOnly = True
        Me.FTS10.Visible = True
        Me.FTS10.VisibleIndex = 10
        Me.FTS10.Width = 94
        '
        'FTS11
        '
        Me.FTS11.Caption = "พฤศจิกายน"
        Me.FTS11.DisplayFormat.FormatString = "{0:n2}"
        Me.FTS11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FTS11.FieldName = "11"
        Me.FTS11.MinWidth = 25
        Me.FTS11.Name = "FTS11"
        Me.FTS11.OptionsColumn.AllowEdit = False
        Me.FTS11.OptionsColumn.ReadOnly = True
        Me.FTS11.Visible = True
        Me.FTS11.VisibleIndex = 11
        Me.FTS11.Width = 94
        '
        'FTS12
        '
        Me.FTS12.Caption = "ธันวาคม"
        Me.FTS12.DisplayFormat.FormatString = "{0:n2}"
        Me.FTS12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FTS12.FieldName = "12"
        Me.FTS12.MinWidth = 25
        Me.FTS12.Name = "FTS12"
        Me.FTS12.OptionsColumn.AllowEdit = False
        Me.FTS12.OptionsColumn.ReadOnly = True
        Me.FTS12.Visible = True
        Me.FTS12.VisibleIndex = 12
        Me.FTS12.Width = 94
        '
        'GridView14
        '
        Me.GridView14.GridControl = Me.ogcsum
        Me.GridView14.Name = "GridView14"
        '
        'GroupControl11
        '
        Me.GroupControl11.Location = New System.Drawing.Point(13, 1638)
        Me.GroupControl11.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl11.Name = "GroupControl11"
        Me.GroupControl11.ShowCaption = False
        Me.GroupControl11.Size = New System.Drawing.Size(2966, 976)
        Me.GroupControl11.TabIndex = 15
        Me.GroupControl11.Text = "summerry"
        '
        'XtraTabPage3
        '
        Me.XtraTabPage3.Controls.Add(Me.ogcsummary)
        Me.XtraTabPage3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.XtraTabPage3.Name = "XtraTabPage3"
        Me.XtraTabPage3.Size = New System.Drawing.Size(1368, 828)
        Me.XtraTabPage3.Text = "สรุปรายเดือน"
        '
        'ogcsummary
        '
        Me.ogcsummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcsummary.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcsummary.Location = New System.Drawing.Point(0, 0)
        Me.ogcsummary.MainView = Me.ogvsummary
        Me.ogcsummary.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcsummary.Name = "ogcsummary"
        Me.ogcsummary.Size = New System.Drawing.Size(1368, 828)
        Me.ogcsummary.TabIndex = 3
        Me.ogcsummary.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvsummary, Me.GridView15})
        '
        'ogvsummary
        '
        Me.ogvsummary.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.Detail, Me.mFTMonthsum, Me.mFTMalesum, Me.mFTFeMalesum, Me.mFTToTalWork})
        Me.ogvsummary.GridControl = Me.ogcsummary
        Me.ogvsummary.Name = "ogvsummary"
        Me.ogvsummary.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvsummary.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.ogvsummary.OptionsView.ColumnAutoWidth = False
        Me.ogvsummary.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvsummary.OptionsView.ShowGroupPanel = False
        Me.ogvsummary.Tag = "2|"
        '
        'Detail
        '
        Me.Detail.Caption = "Detail"
        Me.Detail.FieldName = "Detail"
        Me.Detail.Name = "Detail"
        Me.Detail.OptionsColumn.AllowEdit = False
        Me.Detail.Visible = True
        Me.Detail.VisibleIndex = 0
        Me.Detail.Width = 108
        '
        'mFTMonthsum
        '
        Me.mFTMonthsum.AppearanceHeader.Options.UseTextOptions = True
        Me.mFTMonthsum.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFTMonthsum.Caption = "FTMonthsum"
        Me.mFTMonthsum.FieldName = "FTMonthsum"
        Me.mFTMonthsum.Name = "mFTMonthsum"
        Me.mFTMonthsum.OptionsColumn.AllowEdit = False
        Me.mFTMonthsum.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.mFTMonthsum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.mFTMonthsum.OptionsColumn.ReadOnly = True
        Me.mFTMonthsum.Visible = True
        Me.mFTMonthsum.VisibleIndex = 1
        Me.mFTMonthsum.Width = 180
        '
        'mFTMalesum
        '
        Me.mFTMalesum.AppearanceHeader.Options.UseTextOptions = True
        Me.mFTMalesum.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFTMalesum.Caption = "FTMalesum"
        Me.mFTMalesum.DisplayFormat.FormatString = "{0:n2}"
        Me.mFTMalesum.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFTMalesum.FieldName = "FTMalesum"
        Me.mFTMalesum.Name = "mFTMalesum"
        Me.mFTMalesum.OptionsColumn.AllowEdit = False
        Me.mFTMalesum.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.mFTMalesum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.mFTMalesum.OptionsColumn.ReadOnly = True
        Me.mFTMalesum.Visible = True
        Me.mFTMalesum.VisibleIndex = 2
        Me.mFTMalesum.Width = 157
        '
        'mFTFeMalesum
        '
        Me.mFTFeMalesum.AppearanceHeader.Options.UseTextOptions = True
        Me.mFTFeMalesum.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFTFeMalesum.Caption = "FTFeMalesum"
        Me.mFTFeMalesum.DisplayFormat.FormatString = "{0:n2}"
        Me.mFTFeMalesum.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFTFeMalesum.FieldName = "FTFeMalesum"
        Me.mFTFeMalesum.Name = "mFTFeMalesum"
        Me.mFTFeMalesum.OptionsColumn.AllowEdit = False
        Me.mFTFeMalesum.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.mFTFeMalesum.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.mFTFeMalesum.OptionsColumn.ReadOnly = True
        Me.mFTFeMalesum.OptionsColumn.TabStop = False
        Me.mFTFeMalesum.Visible = True
        Me.mFTFeMalesum.VisibleIndex = 3
        Me.mFTFeMalesum.Width = 198
        '
        'mFTToTalWork
        '
        Me.mFTToTalWork.AppearanceHeader.Options.UseTextOptions = True
        Me.mFTToTalWork.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.mFTToTalWork.Caption = "FTToTalWork"
        Me.mFTToTalWork.DisplayFormat.FormatString = "{0:n2}"
        Me.mFTToTalWork.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.mFTToTalWork.FieldName = "FTToTalWork"
        Me.mFTToTalWork.Name = "mFTToTalWork"
        Me.mFTToTalWork.OptionsColumn.AllowEdit = False
        Me.mFTToTalWork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.mFTToTalWork.OptionsColumn.ReadOnly = True
        Me.mFTToTalWork.OptionsColumn.TabStop = False
        Me.mFTToTalWork.Visible = True
        Me.mFTToTalWork.VisibleIndex = 4
        Me.mFTToTalWork.Width = 171
        '
        'GridView15
        '
        Me.GridView15.GridControl = Me.ogcsummary
        Me.GridView15.Name = "GridView15"
        '
        'otbmain
        '
        Me.otbmain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otbmain.Location = New System.Drawing.Point(0, 44)
        Me.otbmain.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otbmain.Name = "otbmain"
        Me.otbmain.SelectedTabPage = Me.otpsummary
        Me.otbmain.Size = New System.Drawing.Size(1378, 865)
        Me.otbmain.TabIndex = 391
        Me.otbmain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage3, Me.สรุปรายปี, Me.otpsummary, Me.XtraTabPage1, Me.XtraTabPage2, Me.XtraTabPage4})
        '
        'wEmployeeEnablonHRM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1378, 909)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otbmain)
        Me.Controls.Add(Me.ogbmainprocbutton1)
        Me.Controls.Add(Me.hideContainerTop)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wEmployeeEnablonHRM"
        Me.Text = "wEmployeeEnablonHRM"
        CType(Me.DockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.hideContainerTop.ResumeLayout(False)
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FNYear.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDDateEnd.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDDateEnd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FDDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton1.ResumeLayout(False)
        CType(Me.DateEdit1.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.XtraTabPage4.ResumeLayout(False)
        CType(Me.GroupControl6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl6.ResumeLayout(False)
        CType(Me.ogcmanday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvmanday, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl7.ResumeLayout(False)
        CType(Me.ogcsumleave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvsumleave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcsumnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvsumnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.GroupControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl4.ResumeLayout(False)
        CType(Me.ogcend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl5.ResumeLayout(False)
        CType(Me.ogcsumrea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvsumrea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.ogcnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvnew, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.otpsummary.ResumeLayout(False)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ogcTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbpoamt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbpoamt.ResumeLayout(False)
        CType(Me.ogcsumTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvSumTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView10, System.ComponentModel.ISupportInitialize).EndInit()
        Me.สรุปรายปี.ResumeLayout(False)
        CType(Me.GroupControl9, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl9.ResumeLayout(False)
        Me.GroupControl9.PerformLayout()
        CType(Me.GroupControl12, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl12.ResumeLayout(False)
        CType(Me.ogcsum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvsum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl11, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage3.ResumeLayout(False)
        CType(Me.ogcsummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvsummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otbmain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager1 As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents FNHSysCmpId_lbl_ofyear As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysEmpTypeId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysSectId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysDivisonId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysEmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysUnitSectId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents ogbmainprocbutton1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear5 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload5 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FDDateEnd As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FDDateEnd_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FDDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents DateEdit1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents hideContainerTop As DevExpress.XtraBars.Docking.AutoHideContainer
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNYear As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNYear_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents otbmain As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpsummary As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcTime As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvtime As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTEmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNEmpSex As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn34 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDateTrans As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogbpoamt As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcsumTime As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvSumTime As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMonthsum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTToTalWork As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMalesum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFeMalesum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDaysum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents XtraTabPage3 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogcsummary As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvsummary As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents Detail As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFTMonthsum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFTMalesum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFTFeMalesum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents mFTToTalWork As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents สรุปรายปี As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl9 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl12 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcsum As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvsum As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DetailS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTS1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTS2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTS3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTS4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTS5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTS6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTS7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTS8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTS9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTS10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTS11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTS12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GroupControl11 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcnew As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvnew As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTEmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTUnitSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTUnitSectName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTEmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNEmpSex As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDDateStart As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcsumnew As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvsumnew As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents nFTMonthsum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents nFTToTalWork As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents nFTMalesum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents nFTFeMalesum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl4 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcend As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvend As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents dFTEmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dFTUnitSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dFTUnitSectName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dFTEmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dFNEmpSex As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dFDDateEnd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTResignName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn45 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GroupControl5 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcsumrea As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvsumrea As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn46 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rFTMonthsum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rFTToTalWork As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rFTMalesum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rFTFeMalesum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents XtraTabPage4 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents GroupControl6 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcmanday As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvmanday As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn51 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStartDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents bFTUnitSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents bFTUnitSectName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents bFTEmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents bFNEmpSex As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Reason As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNLeaveTotalTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNLeaveTotalTimeMin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn66 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents bFTEmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GroupControl7 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcsumleave As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvsumleave As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn59 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTypeLeave As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lFTMonthsum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lFTToTalWork As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lFTMalesum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lFTFeMalesum As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridView5 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView7 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView8 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView9 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView10 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView11 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView14 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView15 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTInsDate As DevExpress.XtraGrid.Columns.GridColumn
End Class
