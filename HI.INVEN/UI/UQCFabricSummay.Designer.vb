<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UQCFabricSummay
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim XyDiagram3 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim SideBySideBarSeriesView3 As DevExpress.XtraCharts.SideBySideBarSeriesView = New DevExpress.XtraCharts.SideBySideBarSeriesView()
        Dim ChartTitle3 As DevExpress.XtraCharts.ChartTitle = New DevExpress.XtraCharts.ChartTitle()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView()
        Me.otbmain = New DevExpress.XtraTab.XtraTabControl()
        Me.otpdetail = New DevExpress.XtraTab.XtraTabPage()
        Me.otpgraph = New DevExpress.XtraTab.XtraTabPage()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.pivotGridControl = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.CFTState = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTFabricGrpName = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CDataDefect = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFNListIndex = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CCFNGrpType = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.ogbchart = New DevExpress.XtraEditors.GroupControl()
        Me.comboChartType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.chartControl = New DevExpress.XtraCharts.ChartControl()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otbmain.SuspendLayout()
        Me.otpdetail.SuspendLayout()
        Me.otpgraph.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.pivotGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbchart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.comboChartType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(SideBySideBarSeriesView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridColumn1
        '
        Me.GridColumn1.Name = "GridColumn1"
        '
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ogcdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.Size = New System.Drawing.Size(1004, 559)
        Me.ogcdetail.TabIndex = 396
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.CustomizationFormBounds = New System.Drawing.Rectangle(0, 615, 219, 254)
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetail.OptionsPrint.PrintHeader = False
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetail.OptionsView.ShowColumnHeaders = False
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'otbmain
        '
        Me.otbmain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otbmain.Location = New System.Drawing.Point(0, 0)
        Me.otbmain.Margin = New System.Windows.Forms.Padding(2)
        Me.otbmain.Name = "otbmain"
        Me.otbmain.SelectedTabPage = Me.otpdetail
        Me.otbmain.Size = New System.Drawing.Size(1010, 587)
        Me.otbmain.TabIndex = 397
        Me.otbmain.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpdetail, Me.otpgraph})
        '
        'otpdetail
        '
        Me.otpdetail.Controls.Add(Me.ogcdetail)
        Me.otpdetail.Margin = New System.Windows.Forms.Padding(2)
        Me.otpdetail.Name = "otpdetail"
        Me.otpdetail.Size = New System.Drawing.Size(1004, 559)
        Me.otpdetail.Text = "QC Fabric Detail"
        '
        'otpgraph
        '
        Me.otpgraph.Controls.Add(Me.PanelControl1)
        Me.otpgraph.Controls.Add(Me.comboChartType)
        Me.otpgraph.Controls.Add(Me.LabelControl1)
        Me.otpgraph.Controls.Add(Me.chartControl)
        Me.otpgraph.Margin = New System.Windows.Forms.Padding(2)
        Me.otpgraph.Name = "otpgraph"
        Me.otpgraph.Size = New System.Drawing.Size(1004, 559)
        Me.otpgraph.Text = "QC Fabric Graph"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.pivotGridControl)
        Me.PanelControl1.Controls.Add(Me.ogbchart)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1004, 559)
        Me.PanelControl1.TabIndex = 9
        '
        'pivotGridControl
        '
        Me.pivotGridControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pivotGridControl.Cursor = System.Windows.Forms.Cursors.Default
        Me.pivotGridControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pivotGridControl.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.CFTState, Me.CFTFabricGrpName, Me.CDataDefect, Me.CFNListIndex, Me.CCFNGrpType})
        Me.pivotGridControl.Location = New System.Drawing.Point(2, 388)
        Me.pivotGridControl.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pivotGridControl.Name = "pivotGridControl"
        Me.pivotGridControl.OptionsChartDataSource.DataProvideMode = DevExpress.XtraPivotGrid.PivotChartDataProvideMode.UseCustomSettings
        Me.pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = False
        Me.pivotGridControl.OptionsChartDataSource.UpdateDelay = 500
        Me.pivotGridControl.OptionsCustomization.AllowEdit = False
        Me.pivotGridControl.OptionsCustomization.AllowFilter = False
        Me.pivotGridControl.OptionsCustomization.AllowFilterBySummary = False
        Me.pivotGridControl.OptionsCustomization.AllowSort = False
        Me.pivotGridControl.OptionsView.ShowRowGrandTotals = False
        Me.pivotGridControl.OptionsView.ShowRowTotals = False
        Me.pivotGridControl.Size = New System.Drawing.Size(1000, 169)
        Me.pivotGridControl.TabIndex = 6
        '
        'CFTState
        '
        Me.CFTState.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CFTState.AreaIndex = 0
        Me.CFTState.Caption = "Type"
        Me.CFTState.FieldName = "Type"
        Me.CFTState.Name = "CFTState"
        Me.CFTState.Width = 160
        '
        'CFTFabricGrpName
        '
        Me.CFTFabricGrpName.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.CFTFabricGrpName.AreaIndex = 1
        Me.CFTFabricGrpName.Caption = "FTFabricGrpName"
        Me.CFTFabricGrpName.FieldName = "FTFabricGrpName"
        Me.CFTFabricGrpName.Name = "CFTFabricGrpName"
        '
        'CDataDefect
        '
        Me.CDataDefect.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CDataDefect.AreaIndex = 0
        Me.CDataDefect.CellFormat.FormatString = "{0:n2}"
        Me.CDataDefect.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CDataDefect.FieldName = "DataDefect"
        Me.CDataDefect.Name = "CDataDefect"
        '
        'CFNListIndex
        '
        Me.CFNListIndex.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.CFNListIndex.AreaIndex = 0
        Me.CFNListIndex.Caption = "No"
        Me.CFNListIndex.FieldName = "FNListIndex"
        Me.CFNListIndex.Name = "CFNListIndex"
        Me.CFNListIndex.Visible = False
        '
        'CCFNGrpType
        '
        Me.CCFNGrpType.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.CCFNGrpType.AreaIndex = 1
        Me.CCFNGrpType.Caption = "Type No."
        Me.CCFNGrpType.FieldName = "FNGrpType"
        Me.CCFNGrpType.Name = "CCFNGrpType"
        Me.CCFNGrpType.Visible = False
        Me.CCFNGrpType.Width = 50
        '
        'ogbchart
        '
        Me.ogbchart.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogbchart.Location = New System.Drawing.Point(2, 2)
        Me.ogbchart.Name = "ogbchart"
        Me.ogbchart.ShowCaption = False
        Me.ogbchart.Size = New System.Drawing.Size(1000, 386)
        Me.ogbchart.TabIndex = 0
        Me.ogbchart.Text = "GroupControl1"
        '
        'comboChartType
        '
        Me.comboChartType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.comboChartType.EditValue = "Line"
        Me.comboChartType.Location = New System.Drawing.Point(847, 3)
        Me.comboChartType.Name = "comboChartType"
        Me.comboChartType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.comboChartType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.comboChartType.Size = New System.Drawing.Size(154, 20)
        Me.comboChartType.TabIndex = 8
        Me.comboChartType.Visible = False
        '
        'LabelControl1
        '
        Me.LabelControl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl1.Appearance.Options.UseTextOptions = True
        Me.LabelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(716, 5)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(125, 17)
        Me.LabelControl1.TabIndex = 7
        Me.LabelControl1.Text = "Chart Type:"
        Me.LabelControl1.Visible = False
        '
        'chartControl
        '
        Me.chartControl.DataBindings = Nothing
        XyDiagram3.AxisX.Label.Font = New System.Drawing.Font("Tahoma", 7.0!)
        XyDiagram3.AxisX.Label.Staggered = True
        XyDiagram3.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram3.AxisY.VisibleInPanesSerializable = "-1"
        XyDiagram3.EnableAxisXScrolling = True
        Me.chartControl.Diagram = XyDiagram3
        Me.chartControl.Legend.MaxHorizontalPercentage = 30.0R
        Me.chartControl.Legend.Name = "Default Legend"
        Me.chartControl.Location = New System.Drawing.Point(0, 0)
        Me.chartControl.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.chartControl.Name = "chartControl"
        Me.chartControl.SeriesDataMember = "Series"
        Me.chartControl.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.chartControl.SeriesTemplate.ArgumentDataMember = "Arguments"
        Me.chartControl.SeriesTemplate.CrosshairLabelPattern = "{S} : {V:c}"
        Me.chartControl.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.[False]
        Me.chartControl.SeriesTemplate.ValueDataMembersSerializable = "Values"
        SideBySideBarSeriesView3.BarWidth = 0.5R
        Me.chartControl.SeriesTemplate.View = SideBySideBarSeriesView3
        Me.chartControl.SideBySideBarDistanceFixed = 2
        Me.chartControl.SideBySideBarDistanceVariable = 1.0R
        Me.chartControl.SideBySideEqualBarWidth = False
        Me.chartControl.Size = New System.Drawing.Size(244, 104)
        Me.chartControl.TabIndex = 5
        ChartTitle3.Text = "222"
        Me.chartControl.Titles.AddRange(New DevExpress.XtraCharts.ChartTitle() {ChartTitle3})
        Me.chartControl.Visible = False
        '
        'UQCFabricSummay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.otbmain)
        Me.Name = "UQCFabricSummay"
        Me.Size = New System.Drawing.Size(1010, 587)
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otbmain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otbmain.ResumeLayout(False)
        Me.otpdetail.ResumeLayout(False)
        Me.otpgraph.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.pivotGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbchart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.comboChartType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(XyDiagram3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(SideBySideBarSeriesView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartControl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
    Friend WithEvents otbmain As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpdetail As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otpgraph As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents CFTState As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTFabricGrpName As DevExpress.XtraPivotGrid.PivotGridField
    Private WithEvents CDataDefect As DevExpress.XtraPivotGrid.PivotGridField
    Public WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents CFNListIndex As DevExpress.XtraPivotGrid.PivotGridField
    Public WithEvents chartControl As DevExpress.XtraCharts.ChartControl
    Public WithEvents pivotGridControl As DevExpress.XtraPivotGrid.PivotGridControl
    Private WithEvents comboChartType As DevExpress.XtraEditors.ComboBoxEdit
    Private WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CCFNGrpType As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ogbchart As DevExpress.XtraEditors.GroupControl
End Class
