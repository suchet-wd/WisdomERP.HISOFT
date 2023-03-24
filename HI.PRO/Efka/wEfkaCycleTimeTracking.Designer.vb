<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wEfkaCycleTimeTracking
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wEfkaCycleTimeTracking))
        Me.ogbheader = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTStartEfkaDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTStartPO_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogcdetailcolorsizeline = New DevExpress.XtraTab.XtraTabControl()
        Me.otbdataefkadetail = New DevExpress.XtraTab.XtraTabPage()
        Me.ogdtime = New DevExpress.XtraGrid.GridControl()
        Me.ogvtime = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFTUnitSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTOperationName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTAssetCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTAssetName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNDataSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNTimeSewing = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNTotalStitches = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNAVGSpeed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNRunTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNStopTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNTimeBetweenStartEnd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNSewingfoot = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNTotalTimeMS = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNTotalTimeSec = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.CCFNTotalTimeAVG = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogbheader.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTStartEfkaDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartEfkaDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        CType(Me.ogcdetailcolorsizeline, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogcdetailcolorsizeline.SuspendLayout()
        Me.otbdataefkadetail.SuspendLayout()
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
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
        Me.ogbheader.OriginalSize = New System.Drawing.Size(1326, 75)
        Me.ogbheader.Size = New System.Drawing.Size(1326, 75)
        Me.ogbheader.TabStop = False
        Me.ogbheader.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTStartEfkaDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartPO_lbl)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(5, 27)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1316, 41)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTStartEfkaDate
        '
        Me.FTStartEfkaDate.EditValue = Nothing
        Me.FTStartEfkaDate.EnterMoveNextControl = True
        Me.FTStartEfkaDate.Location = New System.Drawing.Point(280, 10)
        Me.FTStartEfkaDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartEfkaDate.Name = "FTStartEfkaDate"
        Me.FTStartEfkaDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartEfkaDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartEfkaDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartEfkaDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartEfkaDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartEfkaDate.Properties.NullDate = ""
        Me.FTStartEfkaDate.Size = New System.Drawing.Size(152, 22)
        Me.FTStartEfkaDate.TabIndex = 268
        Me.FTStartEfkaDate.Tag = "2|"
        '
        'FTStartPO_lbl
        '
        Me.FTStartPO_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartPO_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartPO_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartPO_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartPO_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartPO_lbl.Location = New System.Drawing.Point(11, 9)
        Me.FTStartPO_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartPO_lbl.Name = "FTStartPO_lbl"
        Me.FTStartPO_lbl.Size = New System.Drawing.Size(266, 23)
        Me.FTStartPO_lbl.TabIndex = 269
        Me.FTStartPO_lbl.Tag = "2|"
        Me.FTStartPO_lbl.Text = "Data Efka Date:"
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ogcdetailcolorsizeline)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 75)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.ShowCaption = False
        Me.ogbdetail.Size = New System.Drawing.Size(1326, 704)
        Me.ogbdetail.TabIndex = 0
        '
        'ogcdetailcolorsizeline
        '
        Me.ogcdetailcolorsizeline.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetailcolorsizeline.Location = New System.Drawing.Point(2, 2)
        Me.ogcdetailcolorsizeline.Name = "ogcdetailcolorsizeline"
        Me.ogcdetailcolorsizeline.SelectedTabPage = Me.otbdataefkadetail
        Me.ogcdetailcolorsizeline.Size = New System.Drawing.Size(1322, 700)
        Me.ogcdetailcolorsizeline.TabIndex = 387
        Me.ogcdetailcolorsizeline.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otbdataefkadetail})
        '
        'otbdataefkadetail
        '
        Me.otbdataefkadetail.Controls.Add(Me.ogdtime)
        Me.otbdataefkadetail.Name = "otbdataefkadetail"
        Me.otbdataefkadetail.Size = New System.Drawing.Size(1315, 666)
        Me.otbdataefkadetail.Text = "Efka Data Detail"
        '
        'ogdtime
        '
        Me.ogdtime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdtime.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdtime.Location = New System.Drawing.Point(0, 0)
        Me.ogdtime.MainView = Me.ogvtime
        Me.ogdtime.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdtime.Name = "ogdtime"
        Me.ogdtime.Size = New System.Drawing.Size(1315, 666)
        Me.ogdtime.TabIndex = 0
        Me.ogdtime.TabStop = False
        Me.ogdtime.Tag = "2|"
        Me.ogdtime.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvtime})
        '
        'ogvtime
        '
        Me.ogvtime.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTUnitSectCode, Me.CFTStyleCode, Me.CFNSeq, Me.CFTOperationName, Me.CFTAssetCode, Me.CFTAssetName, Me.CFNDataSeq, Me.CFNTimeSewing, Me.CFNTotalStitches, Me.CFNAVGSpeed, Me.CFNRunTime, Me.CFNStopTime, Me.CFNTimeBetweenStartEnd, Me.CFNSewingfoot, Me.CFNTotalTimeMS, Me.CFNTotalTimeSec, Me.CCFNTotalTimeAVG})
        Me.ogvtime.GridControl = Me.ogdtime
        Me.ogvtime.Name = "ogvtime"
        Me.ogvtime.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvtime.OptionsView.AllowCellMerge = True
        Me.ogvtime.OptionsView.ColumnAutoWidth = False
        Me.ogvtime.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvtime.OptionsView.ShowGroupPanel = False
        Me.ogvtime.Tag = "2|"
        '
        'CFTUnitSectCode
        '
        Me.CFTUnitSectCode.Caption = "UnitSect Code"
        Me.CFTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.CFTUnitSectCode.Name = "CFTUnitSectCode"
        Me.CFTUnitSectCode.OptionsColumn.AllowEdit = False
        Me.CFTUnitSectCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTUnitSectCode.OptionsColumn.AllowMove = False
        Me.CFTUnitSectCode.OptionsColumn.AllowShowHide = False
        Me.CFTUnitSectCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTUnitSectCode.OptionsColumn.ReadOnly = True
        Me.CFTUnitSectCode.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTUnitSectCode.Visible = True
        Me.CFTUnitSectCode.VisibleIndex = 0
        '
        'CFTStyleCode
        '
        Me.CFTStyleCode.Caption = "Style Code"
        Me.CFTStyleCode.FieldName = "FTStyleCode"
        Me.CFTStyleCode.Name = "CFTStyleCode"
        Me.CFTStyleCode.OptionsColumn.AllowEdit = False
        Me.CFTStyleCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTStyleCode.OptionsColumn.AllowMove = False
        Me.CFTStyleCode.OptionsColumn.AllowShowHide = False
        Me.CFTStyleCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStyleCode.OptionsColumn.ReadOnly = True
        Me.CFTStyleCode.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTStyleCode.Visible = True
        Me.CFTStyleCode.VisibleIndex = 1
        '
        'CFNSeq
        '
        Me.CFNSeq.Caption = "ลำดับขั้นตอน"
        Me.CFNSeq.FieldName = "FNSeq"
        Me.CFNSeq.Name = "CFNSeq"
        Me.CFNSeq.OptionsColumn.AllowEdit = False
        Me.CFNSeq.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNSeq.OptionsColumn.AllowMove = False
        Me.CFNSeq.OptionsColumn.AllowShowHide = False
        Me.CFNSeq.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNSeq.OptionsColumn.ReadOnly = True
        Me.CFNSeq.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNSeq.Visible = True
        Me.CFNSeq.VisibleIndex = 2
        '
        'CFTOperationName
        '
        Me.CFTOperationName.Caption = "ชื่อขั้นตอน"
        Me.CFTOperationName.FieldName = "FTOperationName"
        Me.CFTOperationName.Name = "CFTOperationName"
        Me.CFTOperationName.OptionsColumn.AllowEdit = False
        Me.CFTOperationName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTOperationName.OptionsColumn.AllowMove = False
        Me.CFTOperationName.OptionsColumn.AllowShowHide = False
        Me.CFTOperationName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTOperationName.OptionsColumn.ReadOnly = True
        Me.CFTOperationName.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTOperationName.Visible = True
        Me.CFTOperationName.VisibleIndex = 3
        '
        'CFTAssetCode
        '
        Me.CFTAssetCode.Caption = "รหัสจักร"
        Me.CFTAssetCode.FieldName = "FTAssetCode"
        Me.CFTAssetCode.Name = "CFTAssetCode"
        Me.CFTAssetCode.OptionsColumn.AllowEdit = False
        Me.CFTAssetCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTAssetCode.OptionsColumn.AllowMove = False
        Me.CFTAssetCode.OptionsColumn.AllowShowHide = False
        Me.CFTAssetCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTAssetCode.OptionsColumn.ReadOnly = True
        Me.CFTAssetCode.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTAssetCode.Visible = True
        Me.CFTAssetCode.VisibleIndex = 4
        '
        'CFTAssetName
        '
        Me.CFTAssetName.Caption = "ชื่อจักร"
        Me.CFTAssetName.FieldName = "FTAssetName"
        Me.CFTAssetName.Name = "CFTAssetName"
        Me.CFTAssetName.OptionsColumn.AllowEdit = False
        Me.CFTAssetName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTAssetName.OptionsColumn.AllowMove = False
        Me.CFTAssetName.OptionsColumn.AllowShowHide = False
        Me.CFTAssetName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTAssetName.OptionsColumn.ReadOnly = True
        Me.CFTAssetName.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTAssetName.Visible = True
        Me.CFTAssetName.VisibleIndex = 5
        '
        'CFNDataSeq
        '
        Me.CFNDataSeq.Caption = "ชิ้นที่"
        Me.CFNDataSeq.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNDataSeq.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNDataSeq.FieldName = "FNDataSeq"
        Me.CFNDataSeq.Name = "CFNDataSeq"
        Me.CFNDataSeq.OptionsColumn.AllowEdit = False
        Me.CFNDataSeq.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNDataSeq.OptionsColumn.AllowMove = False
        Me.CFNDataSeq.OptionsColumn.AllowShowHide = False
        Me.CFNDataSeq.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNDataSeq.OptionsColumn.ReadOnly = True
        Me.CFNDataSeq.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNDataSeq.Visible = True
        Me.CFNDataSeq.VisibleIndex = 6
        '
        'CFNTimeSewing
        '
        Me.CFNTimeSewing.Caption = "Time Sewing"
        Me.CFNTimeSewing.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNTimeSewing.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNTimeSewing.FieldName = "FNTimeSewing"
        Me.CFNTimeSewing.Name = "CFNTimeSewing"
        Me.CFNTimeSewing.OptionsColumn.AllowEdit = False
        Me.CFNTimeSewing.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNTimeSewing.OptionsColumn.AllowMove = False
        Me.CFNTimeSewing.OptionsColumn.AllowShowHide = False
        Me.CFNTimeSewing.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNTimeSewing.OptionsColumn.ReadOnly = True
        Me.CFNTimeSewing.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNTimeSewing.Visible = True
        Me.CFNTimeSewing.VisibleIndex = 7
        '
        'CFNTotalStitches
        '
        Me.CFNTotalStitches.Caption = "Total Stitches"
        Me.CFNTotalStitches.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNTotalStitches.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNTotalStitches.FieldName = "FNTotalStitches"
        Me.CFNTotalStitches.Name = "CFNTotalStitches"
        Me.CFNTotalStitches.OptionsColumn.AllowEdit = False
        Me.CFNTotalStitches.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNTotalStitches.OptionsColumn.AllowMove = False
        Me.CFNTotalStitches.OptionsColumn.AllowShowHide = False
        Me.CFNTotalStitches.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNTotalStitches.OptionsColumn.ReadOnly = True
        Me.CFNTotalStitches.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNTotalStitches.Visible = True
        Me.CFNTotalStitches.VisibleIndex = 8
        '
        'CFNAVGSpeed
        '
        Me.CFNAVGSpeed.Caption = "AVG Speed"
        Me.CFNAVGSpeed.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNAVGSpeed.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNAVGSpeed.FieldName = "FNAVGSpeed"
        Me.CFNAVGSpeed.Name = "CFNAVGSpeed"
        Me.CFNAVGSpeed.OptionsColumn.AllowEdit = False
        Me.CFNAVGSpeed.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNAVGSpeed.OptionsColumn.AllowMove = False
        Me.CFNAVGSpeed.OptionsColumn.AllowShowHide = False
        Me.CFNAVGSpeed.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNAVGSpeed.OptionsColumn.ReadOnly = True
        Me.CFNAVGSpeed.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNAVGSpeed.Visible = True
        Me.CFNAVGSpeed.VisibleIndex = 9
        '
        'CFNRunTime
        '
        Me.CFNRunTime.Caption = "Run Time"
        Me.CFNRunTime.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNRunTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNRunTime.FieldName = "FNRunTime"
        Me.CFNRunTime.Name = "CFNRunTime"
        Me.CFNRunTime.OptionsColumn.AllowEdit = False
        Me.CFNRunTime.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNRunTime.OptionsColumn.AllowMove = False
        Me.CFNRunTime.OptionsColumn.AllowShowHide = False
        Me.CFNRunTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNRunTime.OptionsColumn.ReadOnly = True
        Me.CFNRunTime.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNRunTime.Visible = True
        Me.CFNRunTime.VisibleIndex = 10
        '
        'CFNStopTime
        '
        Me.CFNStopTime.Caption = "Stop Time"
        Me.CFNStopTime.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNStopTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNStopTime.FieldName = "FNStopTime"
        Me.CFNStopTime.Name = "CFNStopTime"
        Me.CFNStopTime.OptionsColumn.AllowEdit = False
        Me.CFNStopTime.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNStopTime.OptionsColumn.AllowMove = False
        Me.CFNStopTime.OptionsColumn.AllowShowHide = False
        Me.CFNStopTime.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNStopTime.OptionsColumn.ReadOnly = True
        Me.CFNStopTime.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNStopTime.Visible = True
        Me.CFNStopTime.VisibleIndex = 11
        '
        'CFNTimeBetweenStartEnd
        '
        Me.CFNTimeBetweenStartEnd.Caption = "Time Between Start To End"
        Me.CFNTimeBetweenStartEnd.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNTimeBetweenStartEnd.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNTimeBetweenStartEnd.FieldName = "FNTimeBetweenStartEnd"
        Me.CFNTimeBetweenStartEnd.Name = "CFNTimeBetweenStartEnd"
        Me.CFNTimeBetweenStartEnd.OptionsColumn.AllowEdit = False
        Me.CFNTimeBetweenStartEnd.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNTimeBetweenStartEnd.OptionsColumn.AllowMove = False
        Me.CFNTimeBetweenStartEnd.OptionsColumn.AllowShowHide = False
        Me.CFNTimeBetweenStartEnd.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNTimeBetweenStartEnd.OptionsColumn.ReadOnly = True
        Me.CFNTimeBetweenStartEnd.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNTimeBetweenStartEnd.Visible = True
        Me.CFNTimeBetweenStartEnd.VisibleIndex = 12
        '
        'CFNSewingfoot
        '
        Me.CFNSewingfoot.Caption = "Sewing foot"
        Me.CFNSewingfoot.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNSewingfoot.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNSewingfoot.FieldName = "FNSewingfoot"
        Me.CFNSewingfoot.Name = "CFNSewingfoot"
        Me.CFNSewingfoot.OptionsColumn.AllowEdit = False
        Me.CFNSewingfoot.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNSewingfoot.OptionsColumn.AllowMove = False
        Me.CFNSewingfoot.OptionsColumn.AllowShowHide = False
        Me.CFNSewingfoot.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNSewingfoot.OptionsColumn.ReadOnly = True
        Me.CFNSewingfoot.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNSewingfoot.Visible = True
        Me.CFNSewingfoot.VisibleIndex = 13
        '
        'CFNTotalTimeMS
        '
        Me.CFNTotalTimeMS.Caption = "Total Time (ms)"
        Me.CFNTotalTimeMS.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNTotalTimeMS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNTotalTimeMS.FieldName = "FNTotalTimeMS"
        Me.CFNTotalTimeMS.Name = "CFNTotalTimeMS"
        Me.CFNTotalTimeMS.OptionsColumn.AllowEdit = False
        Me.CFNTotalTimeMS.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNTotalTimeMS.OptionsColumn.AllowMove = False
        Me.CFNTotalTimeMS.OptionsColumn.AllowShowHide = False
        Me.CFNTotalTimeMS.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNTotalTimeMS.OptionsColumn.ReadOnly = True
        Me.CFNTotalTimeMS.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNTotalTimeMS.Visible = True
        Me.CFNTotalTimeMS.VisibleIndex = 14
        '
        'CFNTotalTimeSec
        '
        Me.CFNTotalTimeSec.Caption = "Total Time (s)"
        Me.CFNTotalTimeSec.DisplayFormat.FormatString = "{0:n2}"
        Me.CFNTotalTimeSec.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNTotalTimeSec.FieldName = "FNTotalTimeSec"
        Me.CFNTotalTimeSec.Name = "CFNTotalTimeSec"
        Me.CFNTotalTimeSec.OptionsColumn.AllowEdit = False
        Me.CFNTotalTimeSec.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNTotalTimeSec.OptionsColumn.AllowMove = False
        Me.CFNTotalTimeSec.OptionsColumn.AllowShowHide = False
        Me.CFNTotalTimeSec.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNTotalTimeSec.OptionsColumn.ReadOnly = True
        Me.CFNTotalTimeSec.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNTotalTimeSec.Visible = True
        Me.CFNTotalTimeSec.VisibleIndex = 15
        '
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.ogbheader})
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(183, 360)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(961, 58)
        Me.ogbmainprocbutton.TabIndex = 387
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(829, 14)
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
        Me.ocmload.Location = New System.Drawing.Point(133, 17)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(136, 28)
        Me.ocmload.TabIndex = 329
        Me.ocmload.Text = "Load Data"
        '
        'CCFNTotalTimeAVG
        '
        Me.CCFNTotalTimeAVG.Caption = "เวลาเฉลี่ย"
        Me.CCFNTotalTimeAVG.DisplayFormat.FormatString = "{0:n2}"
        Me.CCFNTotalTimeAVG.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CCFNTotalTimeAVG.FieldName = "FNTotalTimeAVG"
        Me.CCFNTotalTimeAVG.Name = "CCFNTotalTimeAVG"
        Me.CCFNTotalTimeAVG.OptionsColumn.AllowEdit = False
        Me.CCFNTotalTimeAVG.OptionsColumn.AllowMove = False
        Me.CCFNTotalTimeAVG.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
        Me.CCFNTotalTimeAVG.OptionsColumn.ReadOnly = True
        Me.CCFNTotalTimeAVG.OptionsColumn.ShowInCustomizationForm = False
        Me.CCFNTotalTimeAVG.Visible = True
        Me.CCFNTotalTimeAVG.VisibleIndex = 16
        '
        'wEfkaCycleTimeTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1326, 779)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbdetail)
        Me.Controls.Add(Me.ogbheader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wEfkaCycleTimeTracking"
        Me.Text = "Efka Cycle Time Tracking"
        Me.ogbheader.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTStartEfkaDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartEfkaDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        CType(Me.ogcdetailcolorsizeline, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogcdetailcolorsizeline.ResumeLayout(False)
        Me.otbdataefkadetail.ResumeLayout(False)
        CType(Me.ogdtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvtime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogdtime As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvtime As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents oDockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbheader As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents FTStartEfkaDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartPO_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogcdetailcolorsizeline As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otbdataefkadetail As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CFTUnitSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTOperationName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTAssetCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTAssetName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNDataSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNTimeSewing As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNTotalStitches As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNAVGSpeed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNRunTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNStopTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNTimeBetweenStartEnd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNSewingfoot As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNTotalTimeMS As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNTotalTimeSec As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CCFNTotalTimeAVG As DevExpress.XtraGrid.Columns.GridColumn
End Class
