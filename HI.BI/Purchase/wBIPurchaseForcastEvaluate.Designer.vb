<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wBIPurchaseForcastEvaluate
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
        Me.ogbheader = New DevExpress.XtraEditors.GroupControl()
        Me.FTEDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTEDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTSDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTSDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbconsolidation = New DevExpress.XtraEditors.GroupControl()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpdata = New DevExpress.XtraTab.XtraTabPage()
        Me.pivotGridControl = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.x1stAllocation = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xNewFCTY = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xOrderBuy = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xAPOProdDesc = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xStylDsplyCd = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xGblCatSumDesc = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xLgDesc = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xGndrAgeDesc = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xProdTypeGrpNm = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xSilhDesc = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xProdInitSesnYr = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xDmndSesn = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xTotal = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xGapBuyDate = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xGapBuyDay = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xGapBuyMonth = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xGapBuyYear = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xDemandSeason = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xDemandYear = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xProdQuartile = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xProdYear = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xVander = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xVendorName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xItemNo = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xFNDataType = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xFTDataBy = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xQtyorderyds = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xColorName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.xColor = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbheader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbheader.SuspendLayout()
        CType(Me.FTEDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTSDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbconsolidation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbconsolidation.SuspendLayout()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpdata.SuspendLayout()
        CType(Me.pivotGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbheader
        '
        Me.ogbheader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbheader.Controls.Add(Me.FTEDate)
        Me.ogbheader.Controls.Add(Me.FTEDate_lbl)
        Me.ogbheader.Controls.Add(Me.FTSDate)
        Me.ogbheader.Controls.Add(Me.FTSDate_lbl)
        Me.ogbheader.Location = New System.Drawing.Point(2, 2)
        Me.ogbheader.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogbheader.Name = "ogbheader"
        Me.ogbheader.Size = New System.Drawing.Size(1227, 70)
        Me.ogbheader.TabIndex = 0
        Me.ogbheader.Text = "Criteria Info"
        '
        'FTEDate
        '
        Me.FTEDate.EditValue = Nothing
        Me.FTEDate.EnterMoveNextControl = True
        Me.FTEDate.Location = New System.Drawing.Point(411, 36)
        Me.FTEDate.Name = "FTEDate"
        Me.FTEDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FTEDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEDate.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.FTEDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.FTEDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FTEDate.Properties.NullDate = ""
        Me.FTEDate.Size = New System.Drawing.Size(112, 20)
        Me.FTEDate.TabIndex = 559
        Me.FTEDate.Tag = "2|"
        '
        'FTEDate_lbl
        '
        Me.FTEDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTEDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEDate_lbl.Location = New System.Drawing.Point(303, 35)
        Me.FTEDate_lbl.Name = "FTEDate_lbl"
        Me.FTEDate_lbl.Size = New System.Drawing.Size(105, 19)
        Me.FTEDate_lbl.TabIndex = 561
        Me.FTEDate_lbl.Tag = "2|"
        Me.FTEDate_lbl.Text = "To :"
        '
        'FTSDate
        '
        Me.FTSDate.EditValue = Nothing
        Me.FTSDate.EnterMoveNextControl = True
        Me.FTSDate.Location = New System.Drawing.Point(169, 36)
        Me.FTSDate.Name = "FTSDate"
        Me.FTSDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTSDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTSDate.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FTSDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTSDate.Properties.EditFormat.FormatString = "dd/MM/yyyy"
        Me.FTSDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTSDate.Properties.Mask.EditMask = "dd/MM/yyyy"
        Me.FTSDate.Properties.Mask.UseMaskAsDisplayFormat = True
        Me.FTSDate.Properties.NullDate = ""
        Me.FTSDate.Size = New System.Drawing.Size(112, 20)
        Me.FTSDate.TabIndex = 558
        Me.FTSDate.Tag = "2|"
        '
        'FTSDate_lbl
        '
        Me.FTSDate_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTSDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTSDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTSDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSDate_lbl.Location = New System.Drawing.Point(45, 35)
        Me.FTSDate_lbl.Name = "FTSDate_lbl"
        Me.FTSDate_lbl.Size = New System.Drawing.Size(121, 19)
        Me.FTSDate_lbl.TabIndex = 560
        Me.FTSDate_lbl.Tag = "2|"
        Me.FTSDate_lbl.Text = "Forcast Start Date :"
        '
        'ogbconsolidation
        '
        Me.ogbconsolidation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbconsolidation.Controls.Add(Me.otb)
        Me.ogbconsolidation.Location = New System.Drawing.Point(2, 76)
        Me.ogbconsolidation.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogbconsolidation.Name = "ogbconsolidation"
        Me.ogbconsolidation.ShowCaption = False
        Me.ogbconsolidation.Size = New System.Drawing.Size(1230, 494)
        Me.ogbconsolidation.TabIndex = 2
        Me.ogbconsolidation.Text = "Purchase Consolidation"
        '
        'otb
        '
        Me.otb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otb.Location = New System.Drawing.Point(2, 2)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpdata
        Me.otb.Size = New System.Drawing.Size(1226, 490)
        Me.otb.TabIndex = 395
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpdata})
        '
        'otpdata
        '
        Me.otpdata.Controls.Add(Me.pivotGridControl)
        Me.otpdata.Name = "otpdata"
        Me.otpdata.Size = New System.Drawing.Size(1224, 465)
        Me.otpdata.Text = "Data"
        '
        'pivotGridControl
        '
        Me.pivotGridControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pivotGridControl.Cursor = System.Windows.Forms.Cursors.Default
        Me.pivotGridControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pivotGridControl.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.x1stAllocation, Me.xNewFCTY, Me.xOrderBuy, Me.xAPOProdDesc, Me.xStylDsplyCd, Me.xGblCatSumDesc, Me.xLgDesc, Me.xGndrAgeDesc, Me.xProdTypeGrpNm, Me.xSilhDesc, Me.xProdInitSesnYr, Me.xDmndSesn, Me.xTotal, Me.xGapBuyDate, Me.xGapBuyDay, Me.xGapBuyMonth, Me.xGapBuyYear, Me.xDemandSeason, Me.xDemandYear, Me.xProdQuartile, Me.xProdYear, Me.xVander, Me.xVendorName, Me.xItemNo, Me.xFNDataType, Me.xFTDataBy, Me.xQtyorderyds, Me.xColorName, Me.xColor})
        Me.pivotGridControl.Location = New System.Drawing.Point(0, 0)
        Me.pivotGridControl.Name = "pivotGridControl"
        Me.pivotGridControl.OptionsChartDataSource.DataProvideMode = DevExpress.XtraPivotGrid.PivotChartDataProvideMode.UseCustomSettings
        Me.pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = False
        Me.pivotGridControl.OptionsChartDataSource.UpdateDelay = 500
        Me.pivotGridControl.OptionsData.CaseSensitive = False
        Me.pivotGridControl.OptionsData.DataProcessingEngine = DevExpress.XtraPivotGrid.PivotDataProcessingEngine.LegacyOptimized
        Me.pivotGridControl.OptionsDataField.RowHeaderWidth = 86
        Me.pivotGridControl.OptionsView.RowTreeOffset = 18
        Me.pivotGridControl.OptionsView.RowTreeWidth = 86
        Me.pivotGridControl.OptionsView.ShowColumnGrandTotalHeader = False
        Me.pivotGridControl.OptionsView.ShowColumnGrandTotals = False
        Me.pivotGridControl.Size = New System.Drawing.Size(1224, 465)
        Me.pivotGridControl.TabIndex = 2
        '
        'x1stAllocation
        '
        Me.x1stAllocation.AreaIndex = 0
        Me.x1stAllocation.Caption = "1stAllocation"
        Me.x1stAllocation.FieldName = "1stAllocation"
        Me.x1stAllocation.Name = "x1stAllocation"
        '
        'xNewFCTY
        '
        Me.xNewFCTY.AreaIndex = 20
        Me.xNewFCTY.Caption = "NewFCTY"
        Me.xNewFCTY.FieldName = "NewFCTY"
        Me.xNewFCTY.Name = "xNewFCTY"
        '
        'xOrderBuy
        '
        Me.xOrderBuy.AreaIndex = 1
        Me.xOrderBuy.Caption = "Order Buy"
        Me.xOrderBuy.FieldName = "OrderBuy"
        Me.xOrderBuy.Name = "xOrderBuy"
        '
        'xAPOProdDesc
        '
        Me.xAPOProdDesc.AreaIndex = 2
        Me.xAPOProdDesc.Caption = "APOProdDesc"
        Me.xAPOProdDesc.FieldName = "APOProdDesc"
        Me.xAPOProdDesc.Name = "xAPOProdDesc"
        '
        'xStylDsplyCd
        '
        Me.xStylDsplyCd.AreaIndex = 3
        Me.xStylDsplyCd.Caption = "StylDsplyCd"
        Me.xStylDsplyCd.FieldName = "StylDsplyCd"
        Me.xStylDsplyCd.Name = "xStylDsplyCd"
        '
        'xGblCatSumDesc
        '
        Me.xGblCatSumDesc.AreaIndex = 4
        Me.xGblCatSumDesc.Caption = "GblCatSumDesc"
        Me.xGblCatSumDesc.FieldName = "GblCatSumDesc"
        Me.xGblCatSumDesc.Name = "xGblCatSumDesc"
        '
        'xLgDesc
        '
        Me.xLgDesc.AreaIndex = 5
        Me.xLgDesc.Caption = "LgDesc"
        Me.xLgDesc.FieldName = "LgDesc"
        Me.xLgDesc.Name = "xLgDesc"
        '
        'xGndrAgeDesc
        '
        Me.xGndrAgeDesc.AreaIndex = 6
        Me.xGndrAgeDesc.Caption = "GndrAgeDesc"
        Me.xGndrAgeDesc.FieldName = "GndrAgeDesc"
        Me.xGndrAgeDesc.Name = "xGndrAgeDesc"
        '
        'xProdTypeGrpNm
        '
        Me.xProdTypeGrpNm.AreaIndex = 7
        Me.xProdTypeGrpNm.Caption = "ProdTypeGrpNm"
        Me.xProdTypeGrpNm.FieldName = "ProdTypeGrpNm"
        Me.xProdTypeGrpNm.Name = "xProdTypeGrpNm"
        '
        'xSilhDesc
        '
        Me.xSilhDesc.AreaIndex = 8
        Me.xSilhDesc.Caption = "SilhDesc"
        Me.xSilhDesc.FieldName = "SilhDesc"
        Me.xSilhDesc.Name = "xSilhDesc"
        '
        'xProdInitSesnYr
        '
        Me.xProdInitSesnYr.AreaIndex = 9
        Me.xProdInitSesnYr.Caption = "ProdInitSesnYr"
        Me.xProdInitSesnYr.FieldName = "ProdInitSesnYr"
        Me.xProdInitSesnYr.Name = "xProdInitSesnYr"
        '
        'xDmndSesn
        '
        Me.xDmndSesn.AreaIndex = 10
        Me.xDmndSesn.Caption = "DmndSesn"
        Me.xDmndSesn.FieldName = "DmndSesn"
        Me.xDmndSesn.Name = "xDmndSesn"
        '
        'xTotal
        '
        Me.xTotal.AreaIndex = 11
        Me.xTotal.Caption = "Gap Order Total"
        Me.xTotal.CellFormat.FormatString = "{0:n0}"
        Me.xTotal.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.xTotal.FieldName = "Total"
        Me.xTotal.Name = "xTotal"
        '
        'xGapBuyDate
        '
        Me.xGapBuyDate.AreaIndex = 12
        Me.xGapBuyDate.Caption = "GapBuyDate"
        Me.xGapBuyDate.FieldName = "GapBuyDate"
        Me.xGapBuyDate.Name = "xGapBuyDate"
        '
        'xGapBuyDay
        '
        Me.xGapBuyDay.AreaIndex = 13
        Me.xGapBuyDay.Caption = "GapBuyDay"
        Me.xGapBuyDay.FieldName = "GapBuyDay"
        Me.xGapBuyDay.Name = "xGapBuyDay"
        '
        'xGapBuyMonth
        '
        Me.xGapBuyMonth.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.xGapBuyMonth.AreaIndex = 3
        Me.xGapBuyMonth.Caption = "GapBuyMonth"
        Me.xGapBuyMonth.FieldName = "GapBuyMonth"
        Me.xGapBuyMonth.Name = "xGapBuyMonth"
        '
        'xGapBuyYear
        '
        Me.xGapBuyYear.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.xGapBuyYear.AreaIndex = 0
        Me.xGapBuyYear.Caption = "GapBuyYear"
        Me.xGapBuyYear.FieldName = "GapBuyYear"
        Me.xGapBuyYear.Name = "xGapBuyYear"
        '
        'xDemandSeason
        '
        Me.xDemandSeason.AreaIndex = 14
        Me.xDemandSeason.Caption = "DemandSeason"
        Me.xDemandSeason.FieldName = "DemandSeason"
        Me.xDemandSeason.Name = "xDemandSeason"
        '
        'xDemandYear
        '
        Me.xDemandYear.AreaIndex = 15
        Me.xDemandYear.Caption = "DemandYear"
        Me.xDemandYear.FieldName = "DemandYear"
        Me.xDemandYear.Name = "xDemandYear"
        '
        'xProdQuartile
        '
        Me.xProdQuartile.AreaIndex = 16
        Me.xProdQuartile.Caption = "ProdQuartile"
        Me.xProdQuartile.FieldName = "ProdQuartile"
        Me.xProdQuartile.Name = "xProdQuartile"
        '
        'xProdYear
        '
        Me.xProdYear.AreaIndex = 19
        Me.xProdYear.Caption = "ProdYear"
        Me.xProdYear.FieldName = "ProdYear"
        Me.xProdYear.Name = "xProdYear"
        '
        'xVander
        '
        Me.xVander.AreaIndex = 17
        Me.xVander.Caption = "Vander"
        Me.xVander.FieldName = "Vander"
        Me.xVander.Name = "xVander"
        '
        'xVendorName
        '
        Me.xVendorName.AreaIndex = 18
        Me.xVendorName.Caption = "VendorName"
        Me.xVendorName.FieldName = "VendorName"
        Me.xVendorName.Name = "xVendorName"
        '
        'xItemNo
        '
        Me.xItemNo.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.xItemNo.AreaIndex = 0
        Me.xItemNo.Caption = "ItemNo"
        Me.xItemNo.FieldName = "ItemNo"
        Me.xItemNo.Name = "xItemNo"
        '
        'xFNDataType
        '
        Me.xFNDataType.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.xFNDataType.AreaIndex = 1
        Me.xFNDataType.Caption = "Data Type"
        Me.xFNDataType.FieldName = "FNDataType"
        Me.xFNDataType.Name = "xFNDataType"
        '
        'xFTDataBy
        '
        Me.xFTDataBy.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.xFTDataBy.AreaIndex = 2
        Me.xFTDataBy.Caption = "Cmp."
        Me.xFTDataBy.FieldName = "FTDataBy"
        Me.xFTDataBy.Name = "xFTDataBy"
        '
        'xQtyorderyds
        '
        Me.xQtyorderyds.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.xQtyorderyds.AreaIndex = 0
        Me.xQtyorderyds.Caption = "Gap Qty"
        Me.xQtyorderyds.CellFormat.FormatString = "{0:n4}"
        Me.xQtyorderyds.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.xQtyorderyds.FieldName = "GabQty"
        Me.xQtyorderyds.Name = "xQtyorderyds"
        '
        'xColorName
        '
        Me.xColorName.AreaIndex = 21
        Me.xColorName.Caption = "ColorName"
        Me.xColorName.FieldName = "ColorName"
        Me.xColorName.Name = "xColorName"
        '
        'xColor
        '
        Me.xColor.AreaIndex = 22
        Me.xColor.Caption = "Color"
        Me.xColor.FieldName = "Color"
        Me.xColor.Name = "xColor"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(231, 336)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(835, 68)
        Me.ogbmainprocbutton.TabIndex = 138
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(24, 20)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(101, 25)
        Me.ocmload.TabIndex = 101
        Me.ocmload.TabStop = False
        Me.ocmload.Tag = "2|"
        Me.ocmload.Text = "Load"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(722, 11)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(130, 20)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(92, 25)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'wBIPurchaseForcastEvaluate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1233, 572)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbconsolidation)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "wBIPurchaseForcastEvaluate"
        Me.Text = "Forcast Material Purchased Evaluate"
        CType(Me.ogbheader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbheader.ResumeLayout(False)
        CType(Me.FTEDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTSDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbconsolidation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbconsolidation.ResumeLayout(False)
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpdata.ResumeLayout(False)
        CType(Me.pivotGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbheader As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbconsolidation As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTEDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTSDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpdata As DevExpress.XtraTab.XtraTabPage
    Private WithEvents pivotGridControl As DevExpress.XtraPivotGrid.PivotGridControl
    Friend WithEvents x1stAllocation As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xOrderBuy As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xAPOProdDesc As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xStylDsplyCd As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xGblCatSumDesc As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xLgDesc As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xGndrAgeDesc As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xProdTypeGrpNm As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xSilhDesc As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xProdInitSesnYr As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xDmndSesn As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xTotal As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xGapBuyDate As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xGapBuyDay As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xGapBuyMonth As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xGapBuyYear As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xDemandSeason As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xDemandYear As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xProdQuartile As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xProdYear As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xVander As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xVendorName As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xItemNo As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xFNDataType As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xFTDataBy As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xQtyorderyds As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xNewFCTY As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xColorName As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents xColor As DevExpress.XtraPivotGrid.PivotGridField
End Class
