Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wDailyQualityAssurance
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wDailyQualityAssurance))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim XyDiagram1 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim Series1 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series()
        Dim Series2 As DevExpress.XtraCharts.Series = New DevExpress.XtraCharts.Series()
        Dim LineSeriesView1 As DevExpress.XtraCharts.LineSeriesView = New DevExpress.XtraCharts.LineSeriesView()
        Dim LineSeriesView2 As DevExpress.XtraCharts.LineSeriesView = New DevExpress.XtraCharts.LineSeriesView()
        Me.ocmdoc = New DevExpress.XtraBars.Docking.DockManager()
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTDate = New DevExpress.XtraEditors.DateEdit()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.pivotGriddata = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.CFTState = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTStateData = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTUnitSectCode = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTData = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpdata = New DevExpress.XtraTab.XtraTabPage()
        Me.pivotGridControl = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.CCFTUnitSectCode = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CCFTStateData = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CCFTData = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CCFTData2 = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.otpchart = New DevExpress.XtraTab.XtraTabPage()
        Me.chartControl = New DevExpress.XtraCharts.ChartControl()
        Me.panelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.seUpdateDelay = New DevExpress.XtraEditors.SpinEdit()
        Me.lblUpdateDelay = New DevExpress.XtraEditors.LabelControl()
        Me.ceShowRowGrandTotals = New DevExpress.XtraEditors.CheckEdit()
        Me.ceShowColumnGrandTotals = New DevExpress.XtraEditors.CheckEdit()
        Me.ceSelectionOnly = New DevExpress.XtraEditors.CheckEdit()
        Me.ceChartDataVertical = New DevExpress.XtraEditors.CheckEdit()
        Me.checkShowPointLabels = New DevExpress.XtraEditors.CheckEdit()
        Me.comboChartType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.otabchart = New DevExpress.XtraTab.XtraTabPage()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pivotGriddata, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpdata.SuspendLayout()
        CType(Me.pivotGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpchart.SuspendLayout()
        CType(Me.chartControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Series2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(LineSeriesView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(LineSeriesView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.panelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelControl1.SuspendLayout()
        CType(Me.seUpdateDelay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowRowGrandTotals.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceShowColumnGrandTotals.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceSelectionOnly.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceChartDataVertical.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.checkShowPointLabels.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comboChartType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmdoc
        '
        Me.ocmdoc.Form = Me
        Me.ocmdoc.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.ogbheader})
        Me.ocmdoc.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'ogbheader
        '
        Me.ogbheader.Controls.Add(Me.DockPanel1_Container)
        Me.ogbheader.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.ogbheader.ID = New System.Guid("6a838d1f-4f6f-4734-9f6d-4c809fcfd587")
        Me.ogbheader.Image = CType(resources.GetObject("ogbheader.Image"), System.Drawing.Image)
        Me.ogbheader.Location = New System.Drawing.Point(0, 0)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Options.AllowDockBottom = False
        Me.ogbheader.Options.AllowDockFill = False
        Me.ogbheader.Options.AllowDockLeft = False
        Me.ogbheader.Options.AllowDockRight = False
        Me.ogbheader.Options.AllowFloating = False
        Me.ogbheader.Options.FloatOnDblClick = False
        Me.ogbheader.Options.ShowCloseButton = False
        Me.ogbheader.Options.ShowMaximizeButton = False
        Me.ogbheader.OriginalSize = New System.Drawing.Size(200, 98)
        Me.ogbheader.Size = New System.Drawing.Size(869, 98)
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTDate)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_None)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(2, 28)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(865, 67)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTDate
        '
        Me.FTDate.EditValue = Nothing
        Me.FTDate.EnterMoveNextControl = True
        Me.FTDate.Location = New System.Drawing.Point(132, 25)
        Me.FTDate.Name = "FTDate"
        Me.FTDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FTDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FTDate.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.FTDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FTDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.FTDate.Properties.NullDate = ""
        Me.FTDate.Size = New System.Drawing.Size(113, 20)
        Me.FTDate.TabIndex = 395
        Me.FTDate.Tag = "2|"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(246, 3)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(468, 20)
        Me.FNHSysCmpId_None.TabIndex = 505
        Me.FNHSysCmpId_None.Tag = ""
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(6, 3)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(123, 17)
        Me.FNHSysCmpId_lbl.TabIndex = 504
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'FTDate_lbl
        '
        Me.FTDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTDate_lbl.Location = New System.Drawing.Point(8, 24)
        Me.FTDate_lbl.Name = "FTDate_lbl"
        Me.FTDate_lbl.Size = New System.Drawing.Size(121, 19)
        Me.FTDate_lbl.TabIndex = 479
        Me.FTDate_lbl.Tag = "2|"
        Me.FTDate_lbl.Text = "Date :"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(132, 3)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(113, 20)
        Me.FNHSysCmpId.TabIndex = 503
        Me.FNHSysCmpId.Tag = ""
        '
        'pivotGriddata
        '
        Me.pivotGriddata.Appearance.Cell.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.Cell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.ColumnHeaderArea.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.ColumnHeaderArea.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.CustomTotalCell.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.CustomTotalCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.DataHeaderArea.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.DataHeaderArea.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.Empty.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.Empty.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.ExpandButton.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.ExpandButton.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.FieldHeader.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.FieldHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.FieldValue.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.FieldValue.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.FieldValueGrandTotal.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.FieldValueGrandTotal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.FieldValueTotal.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.FieldValueTotal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.FilterHeaderArea.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.FilterHeaderArea.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.FilterSeparator.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.FilterSeparator.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.FocusedCell.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.FocusedCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.GrandTotalCell.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.GrandTotalCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.HeaderArea.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.HeaderArea.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.HeaderFilterButton.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.HeaderFilterButton.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.HeaderFilterButtonActive.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.HeaderFilterButtonActive.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.HeaderGroupLine.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.HeaderGroupLine.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.Lines.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.Lines.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.PrefilterPanel.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.PrefilterPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.RowHeaderArea.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.RowHeaderArea.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.SelectedCell.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.SelectedCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.Appearance.TotalCell.Options.UseTextOptions = True
        Me.pivotGriddata.Appearance.TotalCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.AppearancePrint.Cell.Options.UseTextOptions = True
        Me.pivotGriddata.AppearancePrint.Cell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.AppearancePrint.FieldHeader.Options.UseTextOptions = True
        Me.pivotGriddata.AppearancePrint.FieldHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.AppearancePrint.FieldValue.Options.UseTextOptions = True
        Me.pivotGriddata.AppearancePrint.FieldValue.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.pivotGriddata.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pivotGriddata.Cursor = System.Windows.Forms.Cursors.Default
        Me.pivotGriddata.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pivotGriddata.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.CFTState, Me.CFTStateData, Me.CFTUnitSectCode, Me.CFTData})
        Me.pivotGriddata.Location = New System.Drawing.Point(0, 319)
        Me.pivotGriddata.Name = "pivotGriddata"
        Me.pivotGriddata.OptionsChartDataSource.DataProvideMode = DevExpress.XtraPivotGrid.PivotChartDataProvideMode.UseCustomSettings
        Me.pivotGriddata.OptionsChartDataSource.ProvideDataByColumns = False
        Me.pivotGriddata.OptionsChartDataSource.UpdateDelay = 500
        Me.pivotGriddata.OptionsCustomization.AllowCustomizationForm = False
        Me.pivotGriddata.OptionsCustomization.AllowDrag = False
        Me.pivotGriddata.OptionsCustomization.AllowEdit = False
        Me.pivotGriddata.OptionsCustomization.AllowExpand = False
        Me.pivotGriddata.OptionsCustomization.AllowExpandOnDoubleClick = False
        Me.pivotGriddata.OptionsCustomization.AllowFilter = False
        Me.pivotGriddata.OptionsCustomization.AllowFilterBySummary = False
        Me.pivotGriddata.OptionsCustomization.AllowSort = False
        Me.pivotGriddata.OptionsCustomization.AllowSortBySummary = False
        Me.pivotGriddata.OptionsDataField.ColumnValueLineCount = 5
        Me.pivotGriddata.OptionsView.ShowColumnGrandTotalHeader = False
        Me.pivotGriddata.OptionsView.ShowColumnGrandTotals = False
        Me.pivotGriddata.OptionsView.ShowColumnHeaders = False
        Me.pivotGriddata.OptionsView.ShowColumnTotals = False
        Me.pivotGriddata.OptionsView.ShowDataHeaders = False
        Me.pivotGriddata.OptionsView.ShowFilterHeaders = False
        Me.pivotGriddata.OptionsView.ShowRowGrandTotalHeader = False
        Me.pivotGriddata.OptionsView.ShowRowGrandTotals = False
        Me.pivotGriddata.OptionsView.ShowRowHeaders = False
        Me.pivotGriddata.OptionsView.ShowRowTotals = False
        Me.pivotGriddata.Size = New System.Drawing.Size(869, 252)
        Me.pivotGriddata.TabIndex = 2
        '
        'CFTState
        '
        Me.CFTState.Appearance.Cell.Options.UseTextOptions = True
        Me.CFTState.Appearance.Cell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.CFTState.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CFTState.AreaIndex = 1
        Me.CFTState.FieldName = "FTState"
        Me.CFTState.Name = "CFTState"
        Me.CFTState.Width = 60
        '
        'CFTStateData
        '
        Me.CFTStateData.Appearance.Cell.Options.UseTextOptions = True
        Me.CFTStateData.Appearance.Cell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.CFTStateData.Appearance.Value.Options.UseTextOptions = True
        Me.CFTStateData.Appearance.Value.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.CFTStateData.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CFTStateData.AreaIndex = 0
        Me.CFTStateData.Caption = "หัวข้อ"
        Me.CFTStateData.FieldName = "FTStateData"
        Me.CFTStateData.Name = "CFTStateData"
        Me.CFTStateData.Width = 200
        '
        'CFTUnitSectCode
        '
        Me.CFTUnitSectCode.Appearance.Value.Options.UseTextOptions = True
        Me.CFTUnitSectCode.Appearance.Value.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.CFTUnitSectCode.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.CFTUnitSectCode.AreaIndex = 0
        Me.CFTUnitSectCode.Caption = "Line No"
        Me.CFTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.CFTUnitSectCode.Name = "CFTUnitSectCode"
        '
        'CFTData
        '
        Me.CFTData.Appearance.Cell.Options.UseTextOptions = True
        Me.CFTData.Appearance.Cell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.CFTData.Appearance.CellGrandTotal.Options.UseTextOptions = True
        Me.CFTData.Appearance.CellGrandTotal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.CFTData.Appearance.CellTotal.Options.UseTextOptions = True
        Me.CFTData.Appearance.CellTotal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.CFTData.Appearance.Header.Options.UseTextOptions = True
        Me.CFTData.Appearance.Header.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.CFTData.Appearance.Value.Options.UseTextOptions = True
        Me.CFTData.Appearance.Value.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.CFTData.Appearance.ValueGrandTotal.Options.UseTextOptions = True
        Me.CFTData.Appearance.ValueGrandTotal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.CFTData.Appearance.ValueTotal.Options.UseTextOptions = True
        Me.CFTData.Appearance.ValueTotal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.CFTData.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFTData.AreaIndex = 0
        Me.CFTData.Caption = "Data"
        Me.CFTData.ColumnValueLineCount = 5
        Me.CFTData.FieldName = "FTData"
        Me.CFTData.Name = "CFTData"
        Me.CFTData.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(205, 3)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(498, 34)
        Me.ogbmainprocbutton.TabIndex = 391
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(339, 7)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(17, 5)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(95, 25)
        Me.ocmload.TabIndex = 93
        Me.ocmload.TabStop = False
        Me.ocmload.Tag = "2|"
        Me.ocmload.Text = "LoadData"
        '
        'otb
        '
        Me.otb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otb.Location = New System.Drawing.Point(0, 98)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpdata
        Me.otb.Size = New System.Drawing.Size(869, 221)
        Me.otb.TabIndex = 396
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpchart, Me.otpdata, Me.otabchart})
        '
        'otpdata
        '
        Me.otpdata.Controls.Add(Me.pivotGridControl)
        Me.otpdata.Name = "otpdata"
        Me.otpdata.PageVisible = False
        Me.otpdata.Size = New System.Drawing.Size(861, 191)
        Me.otpdata.Text = "Data"
        '
        'pivotGridControl
        '
        Me.pivotGridControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pivotGridControl.Cursor = System.Windows.Forms.Cursors.Default
        Me.pivotGridControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pivotGridControl.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.CCFTUnitSectCode, Me.CCFTStateData, Me.CCFTData, Me.CCFTData2})
        Me.pivotGridControl.Location = New System.Drawing.Point(0, 0)
        Me.pivotGridControl.Name = "pivotGridControl"
        Me.pivotGridControl.OptionsChartDataSource.DataProvideMode = DevExpress.XtraPivotGrid.PivotChartDataProvideMode.UseCustomSettings
        Me.pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = False
        Me.pivotGridControl.OptionsChartDataSource.UpdateDelay = 500
        Me.pivotGridControl.Size = New System.Drawing.Size(861, 191)
        Me.pivotGridControl.TabIndex = 2
        '
        'CCFTUnitSectCode
        '
        Me.CCFTUnitSectCode.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.CCFTUnitSectCode.AreaIndex = 0
        Me.CCFTUnitSectCode.Caption = "Line No"
        Me.CCFTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.CCFTUnitSectCode.Name = "CCFTUnitSectCode"
        '
        'CCFTStateData
        '
        Me.CCFTStateData.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CCFTStateData.AreaIndex = 0
        Me.CCFTStateData.FieldName = "FTStateData"
        Me.CCFTStateData.Name = "CCFTStateData"
        '
        'CCFTData
        '
        Me.CCFTData.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CCFTData.AreaIndex = 0
        Me.CCFTData.CellFormat.FormatString = "{0:n0}"
        Me.CCFTData.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CCFTData.FieldName = "FTData"
        Me.CCFTData.Name = "CCFTData"
        '
        'CCFTData2
        '
        Me.CCFTData2.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CCFTData2.AreaIndex = 1
        Me.CCFTData2.CellFormat.FormatString = "N2"
        Me.CCFTData2.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CCFTData2.FieldName = "FTData2"
        Me.CCFTData2.Name = "CCFTData2"
        '
        'otpchart
        '
        Me.otpchart.Controls.Add(Me.chartControl)
        Me.otpchart.Controls.Add(Me.panelControl1)
        Me.otpchart.Name = "otpchart"
        Me.otpchart.Size = New System.Drawing.Size(863, 193)
        Me.otpchart.Text = "Chart"
        '
        'chartControl
        '
        Me.chartControl.DataBindings = Nothing
        XyDiagram1.AxisX.Label.Staggered = True
        XyDiagram1.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram1.AxisY.VisibleInPanesSerializable = "-1"
        Me.chartControl.Diagram = XyDiagram1
        Me.chartControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chartControl.Legend.MaxHorizontalPercentage = 30.0R
        Me.chartControl.Legend.Name = "Default Legend"
        Me.chartControl.Location = New System.Drawing.Point(0, 32)
        Me.chartControl.Name = "chartControl"
        Me.chartControl.SeriesDataMember = "Series"
        Series1.Name = "Series 4"
        Series2.Name = "Series 5"
        Series2.View = LineSeriesView1
        Me.chartControl.SeriesSerializable = New DevExpress.XtraCharts.Series() {Series1, Series2}
        Me.chartControl.SeriesTemplate.ArgumentDataMember = "Arguments"
        Me.chartControl.SeriesTemplate.CrosshairLabelPattern = "{S} : {V:c}"
        Me.chartControl.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.[False]
        Me.chartControl.SeriesTemplate.ValueDataMembersSerializable = "Values"
        Me.chartControl.SeriesTemplate.View = LineSeriesView2
        Me.chartControl.Size = New System.Drawing.Size(863, 161)
        Me.chartControl.TabIndex = 3
        '
        'panelControl1
        '
        Me.panelControl1.Controls.Add(Me.seUpdateDelay)
        Me.panelControl1.Controls.Add(Me.lblUpdateDelay)
        Me.panelControl1.Controls.Add(Me.ceShowRowGrandTotals)
        Me.panelControl1.Controls.Add(Me.ceShowColumnGrandTotals)
        Me.panelControl1.Controls.Add(Me.ceSelectionOnly)
        Me.panelControl1.Controls.Add(Me.ceChartDataVertical)
        Me.panelControl1.Controls.Add(Me.checkShowPointLabels)
        Me.panelControl1.Controls.Add(Me.comboChartType)
        Me.panelControl1.Controls.Add(Me.LabelControl1)
        Me.panelControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.panelControl1.Location = New System.Drawing.Point(0, 0)
        Me.panelControl1.Name = "panelControl1"
        Me.panelControl1.Padding = New System.Windows.Forms.Padding(5)
        Me.panelControl1.Size = New System.Drawing.Size(863, 32)
        Me.panelControl1.TabIndex = 3
        Me.panelControl1.Visible = False
        '
        'seUpdateDelay
        '
        Me.seUpdateDelay.EditValue = New Decimal(New Integer() {500, 0, 0, 0})
        Me.seUpdateDelay.Location = New System.Drawing.Point(697, 3)
        Me.seUpdateDelay.Name = "seUpdateDelay"
        Me.seUpdateDelay.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.seUpdateDelay.Properties.Increment = New Decimal(New Integer() {100, 0, 0, 0})
        Me.seUpdateDelay.Properties.IsFloatValue = False
        Me.seUpdateDelay.Properties.Mask.EditMask = "N00"
        Me.seUpdateDelay.Properties.MaxValue = New Decimal(New Integer() {5000, 0, 0, 0})
        Me.seUpdateDelay.Size = New System.Drawing.Size(48, 20)
        Me.seUpdateDelay.TabIndex = 10
        Me.seUpdateDelay.ToolTip = "Sets the Chart update delay when PivotGrid selection changes."
        Me.seUpdateDelay.Visible = False
        '
        'lblUpdateDelay
        '
        Me.lblUpdateDelay.Location = New System.Drawing.Point(605, 11)
        Me.lblUpdateDelay.Name = "lblUpdateDelay"
        Me.lblUpdateDelay.Size = New System.Drawing.Size(69, 13)
        Me.lblUpdateDelay.TabIndex = 13
        Me.lblUpdateDelay.Text = "Update Delay:"
        Me.lblUpdateDelay.Visible = False
        '
        'ceShowRowGrandTotals
        '
        Me.ceShowRowGrandTotals.Location = New System.Drawing.Point(479, 10)
        Me.ceShowRowGrandTotals.Name = "ceShowRowGrandTotals"
        Me.ceShowRowGrandTotals.Properties.AutoWidth = True
        Me.ceShowRowGrandTotals.Properties.Caption = "Show Row Grand Totals"
        Me.ceShowRowGrandTotals.Size = New System.Drawing.Size(137, 20)
        Me.ceShowRowGrandTotals.TabIndex = 7
        Me.ceShowRowGrandTotals.ToolTip = "Toggles whether row grand total values are shown in the Chart control"
        Me.ceShowRowGrandTotals.Visible = False
        '
        'ceShowColumnGrandTotals
        '
        Me.ceShowColumnGrandTotals.Location = New System.Drawing.Point(697, 8)
        Me.ceShowColumnGrandTotals.Name = "ceShowColumnGrandTotals"
        Me.ceShowColumnGrandTotals.Properties.AutoWidth = True
        Me.ceShowColumnGrandTotals.Properties.Caption = "Show Column Grand Totals"
        Me.ceShowColumnGrandTotals.Size = New System.Drawing.Size(151, 20)
        Me.ceShowColumnGrandTotals.TabIndex = 13
        Me.ceShowColumnGrandTotals.ToolTip = "Toggles whether column grand total values are shown in the Chart control"
        Me.ceShowColumnGrandTotals.Visible = False
        '
        'ceSelectionOnly
        '
        Me.ceSelectionOnly.Location = New System.Drawing.Point(621, 2)
        Me.ceSelectionOnly.Name = "ceSelectionOnly"
        Me.ceSelectionOnly.Properties.AutoWidth = True
        Me.ceSelectionOnly.Properties.Caption = "Selection Only"
        Me.ceSelectionOnly.Size = New System.Drawing.Size(91, 20)
        Me.ceSelectionOnly.TabIndex = 9
        Me.ceSelectionOnly.ToolTip = "Toggles whether all PivotGrid cells or selected cells only should be represented " &
    "in the Chart"
        Me.ceSelectionOnly.Visible = False
        '
        'ceChartDataVertical
        '
        Me.ceChartDataVertical.Location = New System.Drawing.Point(430, 4)
        Me.ceChartDataVertical.Name = "ceChartDataVertical"
        Me.ceChartDataVertical.Properties.AutoWidth = True
        Me.ceChartDataVertical.Properties.Caption = "Generate Series from Columns"
        Me.ceChartDataVertical.Size = New System.Drawing.Size(168, 20)
        Me.ceChartDataVertical.TabIndex = 12
        Me.ceChartDataVertical.ToolTip = "Toggles whether series in a chart control are created based on PivotGrid columns " &
    "or rows"
        Me.ceChartDataVertical.Visible = False
        '
        'checkShowPointLabels
        '
        Me.checkShowPointLabels.Location = New System.Drawing.Point(305, 10)
        Me.checkShowPointLabels.Name = "checkShowPointLabels"
        Me.checkShowPointLabels.Properties.AutoWidth = True
        Me.checkShowPointLabels.Properties.Caption = "Show Point Labels"
        Me.checkShowPointLabels.Size = New System.Drawing.Size(109, 20)
        Me.checkShowPointLabels.TabIndex = 4
        Me.checkShowPointLabels.ToolTip = "Toggles whether value labels are shown in the Chart control"
        Me.checkShowPointLabels.Visible = False
        '
        'comboChartType
        '
        Me.comboChartType.EditValue = "Line"
        Me.comboChartType.Location = New System.Drawing.Point(145, 6)
        Me.comboChartType.Name = "comboChartType"
        Me.comboChartType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.comboChartType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.comboChartType.Size = New System.Drawing.Size(154, 20)
        Me.comboChartType.TabIndex = 3
        Me.comboChartType.Visible = False
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(14, 6)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(125, 17)
        Me.LabelControl1.TabIndex = 2
        Me.LabelControl1.Text = "Chart Type:"
        Me.LabelControl1.Visible = False
        '
        'otabchart
        '
        Me.otabchart.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otabchart.Name = "otabchart"
        Me.otabchart.PageVisible = False
        Me.otabchart.Size = New System.Drawing.Size(863, 193)
        Me.otabchart.Text = "XtraTabPage1"
        '
        'wDailyQualityAssurance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(869, 571)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.pivotGriddata)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbheader)
        Me.Name = "wDailyQualityAssurance"
        Me.Text = "Daily Quality Assurance"
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pivotGriddata, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpdata.ResumeLayout(False)
        CType(Me.pivotGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpchart.ResumeLayout(False)
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(LineSeriesView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Series2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(LineSeriesView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.panelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelControl1.ResumeLayout(False)
        Me.panelControl1.PerformLayout()
        CType(Me.seUpdateDelay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowRowGrandTotals.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceShowColumnGrandTotals.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceSelectionOnly.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceChartDataVertical.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.checkShowPointLabels.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comboChartType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmdoc As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Private WithEvents pivotGriddata As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents CFTState As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTStateData As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTUnitSectCode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTData As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpdata As DevExpress.XtraTab.XtraTabPage
    Private WithEvents pivotGridControl As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents CCFTUnitSectCode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CCFTStateData As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CCFTData As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents otpchart As DevExpress.XtraTab.XtraTabPage
    Private WithEvents chartControl As DevExpress.XtraCharts.ChartControl
    Private WithEvents panelControl1 As DevExpress.XtraEditors.PanelControl
    Private WithEvents seUpdateDelay As DevExpress.XtraEditors.SpinEdit
    Private WithEvents lblUpdateDelay As DevExpress.XtraEditors.LabelControl
    Private WithEvents ceShowRowGrandTotals As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceShowColumnGrandTotals As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceSelectionOnly As DevExpress.XtraEditors.CheckEdit
    Private WithEvents ceChartDataVertical As DevExpress.XtraEditors.CheckEdit
    Private WithEvents checkShowPointLabels As DevExpress.XtraEditors.CheckEdit
    Private WithEvents comboChartType As DevExpress.XtraEditors.ComboBoxEdit
    Private WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CCFTData2 As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents otabchart As DevExpress.XtraTab.XtraTabPage
End Class
