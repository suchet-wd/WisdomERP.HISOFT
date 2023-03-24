<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wWIPTemplate
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
        Dim XyDiagram1 As DevExpress.XtraCharts.XYDiagram = New DevExpress.XtraCharts.XYDiagram()
        Dim SideBySideBarSeriesLabel1 As DevExpress.XtraCharts.SideBySideBarSeriesLabel = New DevExpress.XtraCharts.SideBySideBarSeriesLabel()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager()
        Me.pivotGridControl = New DevExpress.XtraPivotGrid.PivotGridControl()
        Me.C1FTUnitSectCode = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFTMarkCode = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.CFNQuantity = New DevExpress.XtraPivotGrid.PivotGridField()
        Me.chartControl = New DevExpress.XtraCharts.ChartControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.otmtime = New System.Windows.Forms.Timer()
        Me.olbtitle = New DevExpress.XtraEditors.LabelControl()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pivotGridControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chartControl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(SideBySideBarSeriesLabel1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.SuspendLayout()
        '
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'pivotGridControl
        '
        Me.pivotGridControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pivotGridControl.Cursor = System.Windows.Forms.Cursors.Default
        Me.pivotGridControl.Fields.AddRange(New DevExpress.XtraPivotGrid.PivotGridField() {Me.C1FTUnitSectCode, Me.CFTMarkCode, Me.CFNQuantity})
        Me.pivotGridControl.Location = New System.Drawing.Point(127, 43)
        Me.pivotGridControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pivotGridControl.Name = "pivotGridControl"
        Me.pivotGridControl.OptionsChartDataSource.DataProvideMode = DevExpress.XtraPivotGrid.PivotChartDataProvideMode.UseCustomSettings
        Me.pivotGridControl.OptionsChartDataSource.ProvideDataByColumns = False
        Me.pivotGridControl.OptionsChartDataSource.UpdateDelay = 500
        Me.pivotGridControl.Size = New System.Drawing.Size(1539, 172)
        Me.pivotGridControl.TabIndex = 2
        Me.pivotGridControl.Visible = False
        '
        'C1FTUnitSectCode
        '
        Me.C1FTUnitSectCode.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
        Me.C1FTUnitSectCode.AreaIndex = 0
        Me.C1FTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.C1FTUnitSectCode.Name = "C1FTUnitSectCode"
        '
        'CFTMarkCode
        '
        Me.CFTMarkCode.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.CFTMarkCode.AreaIndex = 0
        Me.CFTMarkCode.FieldName = "FTMarkCode"
        Me.CFTMarkCode.Name = "CFTMarkCode"
        '
        'CFNQuantity
        '
        Me.CFNQuantity.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
        Me.CFNQuantity.AreaIndex = 0
        Me.CFNQuantity.CellFormat.FormatString = "{0:n0}"
        Me.CFNQuantity.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNQuantity.FieldName = "FNQuantity"
        Me.CFNQuantity.Name = "CFNQuantity"
        '
        'chartControl
        '
        Me.chartControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chartControl.BackColor = System.Drawing.Color.Transparent
        XyDiagram1.AxisX.Label.Staggered = True
        XyDiagram1.AxisX.VisibleInPanesSerializable = "-1"
        XyDiagram1.AxisY.VisibleInPanesSerializable = "-1"
        XyDiagram1.DefaultPane.BackColor = System.Drawing.Color.Transparent
        XyDiagram1.PaneDistance = 2
        Me.chartControl.Diagram = XyDiagram1
        Me.chartControl.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Left
        Me.chartControl.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside
        Me.chartControl.Legend.MaxHorizontalPercentage = 30.0R
        Me.chartControl.Location = New System.Drawing.Point(5, 72)
        Me.chartControl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chartControl.Name = "chartControl"
        Me.chartControl.SeriesDataMember = "Series"
        Me.chartControl.SeriesSerializable = New DevExpress.XtraCharts.Series(-1) {}
        Me.chartControl.SeriesTemplate.ArgumentDataMember = "Arguments"
        Me.chartControl.SeriesTemplate.CrosshairLabelPattern = "{S} : {V:c}"
        SideBySideBarSeriesLabel1.Position = DevExpress.XtraCharts.BarSeriesLabelPosition.TopInside
        Me.chartControl.SeriesTemplate.Label = SideBySideBarSeriesLabel1
        Me.chartControl.SeriesTemplate.LabelsVisibility = DevExpress.Utils.DefaultBoolean.[True]
        Me.chartControl.SeriesTemplate.ValueDataMembersSerializable = "Values"
        Me.chartControl.Size = New System.Drawing.Size(1826, 696)
        Me.chartControl.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(1825, 414)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(10, 76)
        Me.Panel1.TabIndex = 4
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(523, 364)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(736, 42)
        Me.ogbmainprocbutton.TabIndex = 392
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(551, 9)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'otmtime
        '
        Me.otmtime.Interval = 60000
        '
        'olbtitle
        '
        Me.olbtitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.olbtitle.Appearance.Font = New System.Drawing.Font("Tahoma", 25.0!, System.Drawing.FontStyle.Bold)
        Me.olbtitle.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.olbtitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.olbtitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.olbtitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.olbtitle.Location = New System.Drawing.Point(5, 2)
        Me.olbtitle.Name = "olbtitle"
        Me.olbtitle.Size = New System.Drawing.Size(1826, 63)
        Me.olbtitle.TabIndex = 393
        Me.olbtitle.Text = "Cutting"
        '
        'wWIPTemplate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1843, 791)
        Me.Controls.Add(Me.olbtitle)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pivotGridControl)
        Me.Controls.Add(Me.chartControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wWIPTemplate"
        Me.Text = "WIP Template"
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pivotGridControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(XyDiagram1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(SideBySideBarSeriesLabel1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chartControl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents oDockManager As DevExpress.XtraBars.Docking.DockManager
    Private WithEvents pivotGridControl As DevExpress.XtraPivotGrid.PivotGridControl
    Private WithEvents chartControl As DevExpress.XtraCharts.ChartControl
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents C1FTUnitSectCode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFTMarkCode As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents CFNQuantity As DevExpress.XtraPivotGrid.PivotGridField
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otmtime As System.Windows.Forms.Timer
    Friend WithEvents olbtitle As DevExpress.XtraEditors.LabelControl
End Class
