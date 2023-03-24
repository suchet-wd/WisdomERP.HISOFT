<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wTrackingDoc
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wTrackingDoc))
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.DockManager = New DevExpress.XtraBars.Docking.DockManager()
        Me.oCriteria = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.FTEndDate = New DevExpress.XtraEditors.DateEdit()
        Me.FNFileType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FTEndDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpIdTo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStartDate = New DevExpress.XtraEditors.DateEdit()
        Me.FTStartDate_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNFileType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNDocType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNDocType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCmpIdTo_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCmpIdTo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ogrpdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogcdocument = New DevExpress.XtraGrid.GridControl()
        Me.ogvdocument = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFDDocAge = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDocumentNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDDocumentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDocumentBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDocumentTitle = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTNote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTBenefit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNOperActivity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTOperActivityName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDocName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDocRefCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDocTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTFileTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFBDocument = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNDocType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysCmpId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFT91StateApprove = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFT91StateApprove = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFT70StateApprove = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFT70StateApprove = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTC1StateApprove = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTC1StateApprove = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTC2StateApprove = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTC2StateApprove = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTC3StateApprove = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTC3StateApprove = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTSRStateApprove = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSRStateApprove = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTSPStateApprove = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSPStateApprove = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTCDStateApprove = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTCDStateApprove = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTVNStateApprove = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTVNStateApprove = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTFGStateApprove = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTFGStateApprove = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTStateMNGDepApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTMngStateApp = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTStateManagerApp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTStateManagerApp = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFNReviseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDUpdDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTUnitSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTUnitSectName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTUnitSectCodeCreate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTUnitSectNameCreate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryFTSandApprove = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.oCriteria.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.FTEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNFileType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStartDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNDocType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpIdTo_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpIdTo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogrpdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpdetail.SuspendLayout()
        CType(Me.ogcdocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFT91StateApprove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFT70StateApprove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTC1StateApprove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTC2StateApprove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTC3StateApprove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSRStateApprove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSPStateApprove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTCDStateApprove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTVNStateApprove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTFGStateApprove, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTMngStateApp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTStateManagerApp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSandApprove, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DockManager
        '
        Me.DockManager.Form = Me
        Me.DockManager.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.oCriteria})
        Me.DockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'oCriteria
        '
        Me.oCriteria.Controls.Add(Me.DockPanel1_Container)
        Me.oCriteria.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.oCriteria.ID = New System.Guid("e9036058-e1b7-4a69-ae39-0e5937096689")
        Me.oCriteria.Image = CType(resources.GetObject("oCriteria.Image"), System.Drawing.Image)
        Me.oCriteria.Location = New System.Drawing.Point(0, 0)
        Me.oCriteria.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.oCriteria.Name = "oCriteria"
        Me.oCriteria.Options.AllowDockBottom = False
        Me.oCriteria.Options.AllowDockFill = False
        Me.oCriteria.Options.AllowDockLeft = False
        Me.oCriteria.Options.ShowCloseButton = False
        Me.oCriteria.OriginalSize = New System.Drawing.Size(200, 137)
        Me.oCriteria.Size = New System.Drawing.Size(1213, 137)
        Me.oCriteria.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.FTEndDate)
        Me.DockPanel1_Container.Controls.Add(Me.FNFileType)
        Me.DockPanel1_Container.Controls.Add(Me.FTEndDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpIdTo_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDate)
        Me.DockPanel1_Container.Controls.Add(Me.FTStartDate_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNFileType_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNDocType)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId)
        Me.DockPanel1_Container.Controls.Add(Me.FNDocType_lbl)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpIdTo_None)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpIdTo)
        Me.DockPanel1_Container.Controls.Add(Me.FNHSysCmpId_None)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(5, 27)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1203, 103)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'FTEndDate
        '
        Me.FTEndDate.EditValue = Nothing
        Me.FTEndDate.EnterMoveNextControl = True
        Me.FTEndDate.Location = New System.Drawing.Point(591, 105)
        Me.FTEndDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEndDate.Name = "FTEndDate"
        Me.FTEndDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTEndDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTEndDate.Properties.NullDate = ""
        Me.FTEndDate.Size = New System.Drawing.Size(152, 22)
        Me.FTEndDate.TabIndex = 274
        Me.FTEndDate.Tag = "2|"
        '
        'FNFileType
        '
        Me.FNFileType.Location = New System.Drawing.Point(225, 53)
        Me.FNFileType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNFileType.Name = "FNFileType"
        Me.FNFileType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNFileType.Properties.Tag = "FTFileTypeCriteria"
        Me.FNFileType.Size = New System.Drawing.Size(152, 22)
        Me.FNFileType.TabIndex = 6
        Me.FNFileType.Tag = "2|"
        '
        'FTEndDate_lbl
        '
        Me.FTEndDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTEndDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTEndDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTEndDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTEndDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTEndDate_lbl.Location = New System.Drawing.Point(377, 103)
        Me.FTEndDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTEndDate_lbl.Name = "FTEndDate_lbl"
        Me.FTEndDate_lbl.Size = New System.Drawing.Size(211, 23)
        Me.FTEndDate_lbl.TabIndex = 275
        Me.FTEndDate_lbl.Tag = "2|"
        Me.FTEndDate_lbl.Text = "To Date :"
        '
        'FNHSysCmpIdTo_lbl
        '
        Me.FNHSysCmpIdTo_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysCmpIdTo_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpIdTo_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpIdTo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpIdTo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpIdTo_lbl.Location = New System.Drawing.Point(3, 30)
        Me.FNHSysCmpIdTo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpIdTo_lbl.Name = "FNHSysCmpIdTo_lbl"
        Me.FNHSysCmpIdTo_lbl.Size = New System.Drawing.Size(220, 23)
        Me.FNHSysCmpIdTo_lbl.TabIndex = 401
        Me.FNHSysCmpIdTo_lbl.Tag = "2|"
        Me.FNHSysCmpIdTo_lbl.Text = "Destination Company :"
        '
        'FTStartDate
        '
        Me.FTStartDate.EditValue = Nothing
        Me.FTStartDate.EnterMoveNextControl = True
        Me.FTStartDate.Location = New System.Drawing.Point(225, 105)
        Me.FTStartDate.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartDate.Name = "FTStartDate"
        Me.FTStartDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStartDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTStartDate.Properties.NullDate = ""
        Me.FTStartDate.Size = New System.Drawing.Size(152, 22)
        Me.FTStartDate.TabIndex = 272
        Me.FTStartDate.Tag = "2|"
        '
        'FTStartDate_lbl
        '
        Me.FTStartDate_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTStartDate_lbl.Appearance.Options.UseForeColor = True
        Me.FTStartDate_lbl.Appearance.Options.UseTextOptions = True
        Me.FTStartDate_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStartDate_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTStartDate_lbl.Location = New System.Drawing.Point(3, 103)
        Me.FTStartDate_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStartDate_lbl.Name = "FTStartDate_lbl"
        Me.FTStartDate_lbl.Size = New System.Drawing.Size(220, 23)
        Me.FTStartDate_lbl.TabIndex = 273
        Me.FTStartDate_lbl.Tag = "2|"
        Me.FTStartDate_lbl.Text = "Start Used Document Date :"
        '
        'FNFileType_lbl
        '
        Me.FNFileType_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNFileType_lbl.Appearance.Options.UseForeColor = True
        Me.FNFileType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNFileType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNFileType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNFileType_lbl.Location = New System.Drawing.Point(12, 52)
        Me.FNFileType_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNFileType_lbl.Name = "FNFileType_lbl"
        Me.FNFileType_lbl.Size = New System.Drawing.Size(210, 23)
        Me.FNFileType_lbl.TabIndex = 7
        Me.FNFileType_lbl.Tag = "2|"
        Me.FNFileType_lbl.Text = "FTFileType :"
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(3, 1)
        Me.FNHSysCmpId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(220, 23)
        Me.FNHSysCmpId_lbl.TabIndex = 398
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Source Company :"
        '
        'FNDocType
        '
        Me.FNDocType.Location = New System.Drawing.Point(225, 79)
        Me.FNDocType.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNDocType.Name = "FNDocType"
        Me.FNDocType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNDocType.Properties.Tag = "FTDocTypeCriteria"
        Me.FNDocType.Size = New System.Drawing.Size(152, 22)
        Me.FNDocType.TabIndex = 4
        Me.FNDocType.Tag = "2|"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(225, 0)
        Me.FNHSysCmpId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Size = New System.Drawing.Size(152, 22)
        Me.FNHSysCmpId.TabIndex = 397
        '
        'FNDocType_lbl
        '
        Me.FNDocType_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNDocType_lbl.Appearance.Options.UseForeColor = True
        Me.FNDocType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNDocType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNDocType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNDocType_lbl.Location = New System.Drawing.Point(3, 78)
        Me.FNDocType_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNDocType_lbl.Name = "FNDocType_lbl"
        Me.FNDocType_lbl.Size = New System.Drawing.Size(218, 23)
        Me.FNDocType_lbl.TabIndex = 5
        Me.FNDocType_lbl.Tag = "2|"
        Me.FNDocType_lbl.Text = "FNDocType :"
        '
        'FNHSysCmpIdTo_None
        '
        Me.FNHSysCmpIdTo_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpIdTo_None.EnterMoveNextControl = True
        Me.FNHSysCmpIdTo_None.Location = New System.Drawing.Point(378, 26)
        Me.FNHSysCmpIdTo_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpIdTo_None.Name = "FNHSysCmpIdTo_None"
        Me.FNHSysCmpIdTo_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysCmpIdTo_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysCmpIdTo_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpIdTo_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpIdTo_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpIdTo_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpIdTo_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysCmpIdTo_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysCmpIdTo_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysCmpIdTo_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpIdTo_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysCmpIdTo_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysCmpIdTo_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpIdTo_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpIdTo_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpIdTo_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpIdTo_None.Properties.ReadOnly = True
        Me.FNHSysCmpIdTo_None.Size = New System.Drawing.Size(822, 22)
        Me.FNHSysCmpIdTo_None.TabIndex = 402
        Me.FNHSysCmpIdTo_None.TabStop = False
        Me.FNHSysCmpIdTo_None.Tag = "2|"
        '
        'FNHSysCmpIdTo
        '
        Me.FNHSysCmpIdTo.Location = New System.Drawing.Point(225, 26)
        Me.FNHSysCmpIdTo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpIdTo.Name = "FNHSysCmpIdTo"
        Me.FNHSysCmpIdTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "234", Nothing, True)})
        Me.FNHSysCmpIdTo.Size = New System.Drawing.Size(152, 22)
        Me.FNHSysCmpIdTo.TabIndex = 400
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.EnterMoveNextControl = True
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(378, 0)
        Me.FNHSysCmpId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysCmpId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysCmpId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysCmpId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysCmpId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(822, 22)
        Me.FNHSysCmpId_None.TabIndex = 399
        Me.FNHSysCmpId_None.TabStop = False
        Me.FNHSysCmpId_None.Tag = "2|"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(590, -5)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(496, 74)
        Me.ogbmainprocbutton.TabIndex = 396
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(178, 12)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(111, 31)
        Me.ocmload.TabIndex = 96
        Me.ocmload.TabStop = False
        Me.ocmload.Tag = "2|"
        Me.ocmload.Text = "Load"
        '
        'ocmexit
        '
        Me.ocmexit.Location = New System.Drawing.Point(306, 12)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ogrpdetail
        '
        Me.ogrpdetail.Controls.Add(Me.ogcdocument)
        Me.ogrpdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrpdetail.Location = New System.Drawing.Point(0, 137)
        Me.ogrpdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpdetail.Name = "ogrpdetail"
        Me.ogrpdetail.Size = New System.Drawing.Size(1213, 549)
        Me.ogrpdetail.TabIndex = 3
        Me.ogrpdetail.Text = "Detail"
        '
        'ogcdocument
        '
        Me.ogcdocument.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdocument.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdocument.Location = New System.Drawing.Point(2, 25)
        Me.ogcdocument.MainView = Me.ogvdocument
        Me.ogcdocument.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdocument.Name = "ogcdocument"
        Me.ogcdocument.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect, Me.RepositoryFTSandApprove, Me.RepositoryFT91StateApprove, Me.RepositoryFT70StateApprove, Me.RepositoryFTC1StateApprove, Me.RepositoryFTC2StateApprove, Me.RepositoryFTC3StateApprove, Me.RepositoryFTSRStateApprove, Me.RepositoryFTSPStateApprove, Me.RepositoryFTCDStateApprove, Me.RepositoryFTVNStateApprove, Me.RepositoryFTFGStateApprove, Me.RepositoryFTStateManagerApp, Me.RepositoryFTMngStateApp})
        Me.ogcdocument.Size = New System.Drawing.Size(1209, 522)
        Me.ogcdocument.TabIndex = 3
        Me.ogcdocument.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdocument})
        '
        'ogvdocument
        '
        Me.ogvdocument.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFDDocAge, Me.cFTDocumentNo, Me.cFDDocumentDate, Me.cFTDocumentBy, Me.cFTCmpCode, Me.cFTCmpName, Me.cFTDocumentTitle, Me.cFTNote, Me.cFTBenefit, Me.cFNOperActivity, Me.cFTOperActivityName, Me.cFTDocName, Me.cFTDocRefCode, Me.cFTDocTypeName, Me.cFTFileTypeName, Me.cFBDocument, Me.cFNDocType, Me.cFNHSysCmpId, Me.cFT91StateApprove, Me.cFT70StateApprove, Me.cFTC1StateApprove, Me.cFTC2StateApprove, Me.cFTC3StateApprove, Me.cFTSRStateApprove, Me.cFTSPStateApprove, Me.cFTCDStateApprove, Me.cFTVNStateApprove, Me.cFTFGStateApprove, Me.cFTStateMNGDepApp, Me.cFTStateManagerApp, Me.cFNReviseNo, Me.cFDUpdDate, Me.cFTUnitSectCode, Me.cFTUnitSectName, Me.cFTUnitSectCodeCreate, Me.cFTUnitSectNameCreate})
        Me.ogvdocument.GridControl = Me.ogcdocument
        Me.ogvdocument.Name = "ogvdocument"
        Me.ogvdocument.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.[True]
        Me.ogvdocument.OptionsView.ColumnAutoWidth = False
        Me.ogvdocument.OptionsView.ShowGroupPanel = False
        '
        'cFDDocAge
        '
        Me.cFDDocAge.Caption = "FDDocAge"
        Me.cFDDocAge.FieldName = "FDDocAge"
        Me.cFDDocAge.Name = "cFDDocAge"
        Me.cFDDocAge.OptionsColumn.AllowEdit = False
        Me.cFDDocAge.Visible = True
        Me.cFDDocAge.VisibleIndex = 0
        Me.cFDDocAge.Width = 131
        '
        'cFTDocumentNo
        '
        Me.cFTDocumentNo.Caption = "FTDocumentNo"
        Me.cFTDocumentNo.FieldName = "FTDocumentNo"
        Me.cFTDocumentNo.Name = "cFTDocumentNo"
        Me.cFTDocumentNo.OptionsColumn.AllowEdit = False
        Me.cFTDocumentNo.Visible = True
        Me.cFTDocumentNo.VisibleIndex = 3
        Me.cFTDocumentNo.Width = 149
        '
        'cFDDocumentDate
        '
        Me.cFDDocumentDate.Caption = "FDDocumentDate"
        Me.cFDDocumentDate.FieldName = "FDDocumentDate"
        Me.cFDDocumentDate.Name = "cFDDocumentDate"
        Me.cFDDocumentDate.OptionsColumn.AllowEdit = False
        Me.cFDDocumentDate.Visible = True
        Me.cFDDocumentDate.VisibleIndex = 4
        Me.cFDDocumentDate.Width = 107
        '
        'cFTDocumentBy
        '
        Me.cFTDocumentBy.Caption = "FTDocumentBy"
        Me.cFTDocumentBy.FieldName = "FTDocumentBy"
        Me.cFTDocumentBy.Name = "cFTDocumentBy"
        Me.cFTDocumentBy.OptionsColumn.AllowEdit = False
        Me.cFTDocumentBy.Width = 104
        '
        'cFTCmpCode
        '
        Me.cFTCmpCode.Caption = "FTCmpCode"
        Me.cFTCmpCode.FieldName = "FTCmpCode"
        Me.cFTCmpCode.Name = "cFTCmpCode"
        Me.cFTCmpCode.OptionsColumn.AllowEdit = False
        Me.cFTCmpCode.Visible = True
        Me.cFTCmpCode.VisibleIndex = 1
        Me.cFTCmpCode.Width = 88
        '
        'cFTCmpName
        '
        Me.cFTCmpName.Caption = "FTCmpName"
        Me.cFTCmpName.FieldName = "FTCmpName"
        Me.cFTCmpName.Name = "cFTCmpName"
        Me.cFTCmpName.OptionsColumn.AllowEdit = False
        Me.cFTCmpName.Visible = True
        Me.cFTCmpName.VisibleIndex = 2
        Me.cFTCmpName.Width = 149
        '
        'cFTDocumentTitle
        '
        Me.cFTDocumentTitle.Caption = "FTDocumentTitle"
        Me.cFTDocumentTitle.FieldName = "FTDocumentTitle"
        Me.cFTDocumentTitle.Name = "cFTDocumentTitle"
        Me.cFTDocumentTitle.OptionsColumn.AllowEdit = False
        Me.cFTDocumentTitle.Visible = True
        Me.cFTDocumentTitle.VisibleIndex = 6
        Me.cFTDocumentTitle.Width = 133
        '
        'cFTNote
        '
        Me.cFTNote.Caption = "FTNote"
        Me.cFTNote.FieldName = "FTNote"
        Me.cFTNote.Name = "cFTNote"
        Me.cFTNote.OptionsColumn.AllowEdit = False
        Me.cFTNote.Width = 181
        '
        'cFTBenefit
        '
        Me.cFTBenefit.Caption = "FTBenefit"
        Me.cFTBenefit.FieldName = "FTBenefit"
        Me.cFTBenefit.Name = "cFTBenefit"
        Me.cFTBenefit.OptionsColumn.AllowEdit = False
        Me.cFTBenefit.Visible = True
        Me.cFTBenefit.VisibleIndex = 9
        Me.cFTBenefit.Width = 261
        '
        'cFNOperActivity
        '
        Me.cFNOperActivity.Caption = "FNOperActivity"
        Me.cFNOperActivity.FieldName = "FNOperActivity"
        Me.cFNOperActivity.Name = "cFNOperActivity"
        Me.cFNOperActivity.OptionsColumn.AllowEdit = False
        Me.cFNOperActivity.Visible = True
        Me.cFNOperActivity.VisibleIndex = 5
        Me.cFNOperActivity.Width = 128
        '
        'cFTOperActivityName
        '
        Me.cFTOperActivityName.Caption = "FTOperActivityName"
        Me.cFTOperActivityName.FieldName = "FTOperActivityName"
        Me.cFTOperActivityName.Name = "cFTOperActivityName"
        Me.cFTOperActivityName.OptionsColumn.AllowEdit = False
        Me.cFTOperActivityName.Visible = True
        Me.cFTOperActivityName.VisibleIndex = 16
        Me.cFTOperActivityName.Width = 201
        '
        'cFTDocName
        '
        Me.cFTDocName.Caption = "FTDocName"
        Me.cFTDocName.FieldName = "FTDocName"
        Me.cFTDocName.Name = "cFTDocName"
        Me.cFTDocName.OptionsColumn.AllowEdit = False
        Me.cFTDocName.Visible = True
        Me.cFTDocName.VisibleIndex = 7
        Me.cFTDocName.Width = 194
        '
        'cFTDocRefCode
        '
        Me.cFTDocRefCode.Caption = "FTDocRefCode"
        Me.cFTDocRefCode.FieldName = "FTDocRefCode"
        Me.cFTDocRefCode.Name = "cFTDocRefCode"
        Me.cFTDocRefCode.OptionsColumn.AllowEdit = False
        Me.cFTDocRefCode.Visible = True
        Me.cFTDocRefCode.VisibleIndex = 8
        Me.cFTDocRefCode.Width = 139
        '
        'cFTDocTypeName
        '
        Me.cFTDocTypeName.Caption = "FTDocTypeName"
        Me.cFTDocTypeName.FieldName = "FTDocTypeName"
        Me.cFTDocTypeName.Name = "cFTDocTypeName"
        Me.cFTDocTypeName.OptionsColumn.AllowEdit = False
        Me.cFTDocTypeName.Visible = True
        Me.cFTDocTypeName.VisibleIndex = 10
        Me.cFTDocTypeName.Width = 100
        '
        'cFTFileTypeName
        '
        Me.cFTFileTypeName.Caption = "FTFileTypeName"
        Me.cFTFileTypeName.FieldName = "FTFileTypeName"
        Me.cFTFileTypeName.Name = "cFTFileTypeName"
        Me.cFTFileTypeName.OptionsColumn.AllowEdit = False
        Me.cFTFileTypeName.Visible = True
        Me.cFTFileTypeName.VisibleIndex = 11
        Me.cFTFileTypeName.Width = 84
        '
        'cFBDocument
        '
        Me.cFBDocument.Caption = "GridColumn1"
        Me.cFBDocument.FieldName = "FBDocument"
        Me.cFBDocument.Name = "cFBDocument"
        Me.cFBDocument.OptionsColumn.AllowEdit = False
        '
        'cFNDocType
        '
        Me.cFNDocType.Caption = "FNDocType"
        Me.cFNDocType.FieldName = "FNDocType"
        Me.cFNDocType.Name = "cFNDocType"
        Me.cFNDocType.OptionsColumn.AllowEdit = False
        '
        'cFNHSysCmpId
        '
        Me.cFNHSysCmpId.Caption = "FNHSysCmpId"
        Me.cFNHSysCmpId.FieldName = "FNHSysCmpId"
        Me.cFNHSysCmpId.Name = "cFNHSysCmpId"
        '
        'cFT91StateApprove
        '
        Me.cFT91StateApprove.Caption = "FT91StateApprove"
        Me.cFT91StateApprove.ColumnEdit = Me.RepositoryFT91StateApprove
        Me.cFT91StateApprove.FieldName = "FT91StateApprove"
        Me.cFT91StateApprove.Name = "cFT91StateApprove"
        Me.cFT91StateApprove.OptionsColumn.AllowEdit = False
        Me.cFT91StateApprove.Width = 50
        '
        'RepositoryFT91StateApprove
        '
        Me.RepositoryFT91StateApprove.AutoHeight = False
        Me.RepositoryFT91StateApprove.Caption = "Check"
        Me.RepositoryFT91StateApprove.Name = "RepositoryFT91StateApprove"
        Me.RepositoryFT91StateApprove.ValueChecked = "1"
        Me.RepositoryFT91StateApprove.ValueUnchecked = "0"
        '
        'cFT70StateApprove
        '
        Me.cFT70StateApprove.Caption = "FT70StateApprove"
        Me.cFT70StateApprove.ColumnEdit = Me.RepositoryFT70StateApprove
        Me.cFT70StateApprove.FieldName = "FT70StateApprove"
        Me.cFT70StateApprove.Name = "cFT70StateApprove"
        Me.cFT70StateApprove.OptionsColumn.AllowEdit = False
        Me.cFT70StateApprove.Width = 42
        '
        'RepositoryFT70StateApprove
        '
        Me.RepositoryFT70StateApprove.AutoHeight = False
        Me.RepositoryFT70StateApprove.Caption = "Check"
        Me.RepositoryFT70StateApprove.Name = "RepositoryFT70StateApprove"
        Me.RepositoryFT70StateApprove.ValueChecked = "1"
        Me.RepositoryFT70StateApprove.ValueUnchecked = "0"
        '
        'cFTC1StateApprove
        '
        Me.cFTC1StateApprove.Caption = "FTC1StateApprove"
        Me.cFTC1StateApprove.ColumnEdit = Me.RepositoryFTC1StateApprove
        Me.cFTC1StateApprove.FieldName = "FTC1StateApprove"
        Me.cFTC1StateApprove.Name = "cFTC1StateApprove"
        Me.cFTC1StateApprove.OptionsColumn.AllowEdit = False
        Me.cFTC1StateApprove.Width = 44
        '
        'RepositoryFTC1StateApprove
        '
        Me.RepositoryFTC1StateApprove.AutoHeight = False
        Me.RepositoryFTC1StateApprove.Caption = "Check"
        Me.RepositoryFTC1StateApprove.Name = "RepositoryFTC1StateApprove"
        Me.RepositoryFTC1StateApprove.ValueChecked = "1"
        Me.RepositoryFTC1StateApprove.ValueUnchecked = "0"
        '
        'cFTC2StateApprove
        '
        Me.cFTC2StateApprove.Caption = "FTC2StateApprove"
        Me.cFTC2StateApprove.ColumnEdit = Me.RepositoryFTC2StateApprove
        Me.cFTC2StateApprove.FieldName = "FTC2StateApprove"
        Me.cFTC2StateApprove.Name = "cFTC2StateApprove"
        Me.cFTC2StateApprove.OptionsColumn.AllowEdit = False
        Me.cFTC2StateApprove.Width = 44
        '
        'RepositoryFTC2StateApprove
        '
        Me.RepositoryFTC2StateApprove.AutoHeight = False
        Me.RepositoryFTC2StateApprove.Caption = "Check"
        Me.RepositoryFTC2StateApprove.Name = "RepositoryFTC2StateApprove"
        Me.RepositoryFTC2StateApprove.ValueChecked = "1"
        Me.RepositoryFTC2StateApprove.ValueUnchecked = "0"
        '
        'cFTC3StateApprove
        '
        Me.cFTC3StateApprove.Caption = "FTC3StateApprove"
        Me.cFTC3StateApprove.ColumnEdit = Me.RepositoryFTC3StateApprove
        Me.cFTC3StateApprove.FieldName = "FTC3StateApprove"
        Me.cFTC3StateApprove.Name = "cFTC3StateApprove"
        Me.cFTC3StateApprove.OptionsColumn.AllowEdit = False
        Me.cFTC3StateApprove.Width = 46
        '
        'RepositoryFTC3StateApprove
        '
        Me.RepositoryFTC3StateApprove.AutoHeight = False
        Me.RepositoryFTC3StateApprove.Caption = "Check"
        Me.RepositoryFTC3StateApprove.Name = "RepositoryFTC3StateApprove"
        Me.RepositoryFTC3StateApprove.ValueChecked = "1"
        Me.RepositoryFTC3StateApprove.ValueUnchecked = "0"
        '
        'cFTSRStateApprove
        '
        Me.cFTSRStateApprove.Caption = "FTSRStateApprove"
        Me.cFTSRStateApprove.ColumnEdit = Me.RepositoryFTSRStateApprove
        Me.cFTSRStateApprove.FieldName = "FTSRStateApprove"
        Me.cFTSRStateApprove.Name = "cFTSRStateApprove"
        Me.cFTSRStateApprove.OptionsColumn.AllowEdit = False
        Me.cFTSRStateApprove.Width = 47
        '
        'RepositoryFTSRStateApprove
        '
        Me.RepositoryFTSRStateApprove.AutoHeight = False
        Me.RepositoryFTSRStateApprove.Caption = "Check"
        Me.RepositoryFTSRStateApprove.Name = "RepositoryFTSRStateApprove"
        Me.RepositoryFTSRStateApprove.ValueChecked = "1"
        Me.RepositoryFTSRStateApprove.ValueUnchecked = "0"
        '
        'cFTSPStateApprove
        '
        Me.cFTSPStateApprove.Caption = "FTSPStateApprove"
        Me.cFTSPStateApprove.ColumnEdit = Me.RepositoryFTSPStateApprove
        Me.cFTSPStateApprove.FieldName = "FTSPStateApprove"
        Me.cFTSPStateApprove.Name = "cFTSPStateApprove"
        Me.cFTSPStateApprove.OptionsColumn.AllowEdit = False
        Me.cFTSPStateApprove.Width = 53
        '
        'RepositoryFTSPStateApprove
        '
        Me.RepositoryFTSPStateApprove.AutoHeight = False
        Me.RepositoryFTSPStateApprove.Caption = "Check"
        Me.RepositoryFTSPStateApprove.Name = "RepositoryFTSPStateApprove"
        Me.RepositoryFTSPStateApprove.ValueChecked = "1"
        Me.RepositoryFTSPStateApprove.ValueUnchecked = "0"
        '
        'cFTCDStateApprove
        '
        Me.cFTCDStateApprove.Caption = "FTCDStateApprove"
        Me.cFTCDStateApprove.ColumnEdit = Me.RepositoryFTCDStateApprove
        Me.cFTCDStateApprove.FieldName = "FTCDStateApprove"
        Me.cFTCDStateApprove.Name = "cFTCDStateApprove"
        Me.cFTCDStateApprove.OptionsColumn.AllowEdit = False
        Me.cFTCDStateApprove.Width = 44
        '
        'RepositoryFTCDStateApprove
        '
        Me.RepositoryFTCDStateApprove.AutoHeight = False
        Me.RepositoryFTCDStateApprove.Caption = "Check"
        Me.RepositoryFTCDStateApprove.Name = "RepositoryFTCDStateApprove"
        Me.RepositoryFTCDStateApprove.ValueChecked = "1"
        Me.RepositoryFTCDStateApprove.ValueUnchecked = "0"
        '
        'cFTVNStateApprove
        '
        Me.cFTVNStateApprove.Caption = "FTVNStateApprove"
        Me.cFTVNStateApprove.ColumnEdit = Me.RepositoryFTVNStateApprove
        Me.cFTVNStateApprove.FieldName = "FTVNStateApprove"
        Me.cFTVNStateApprove.Name = "cFTVNStateApprove"
        Me.cFTVNStateApprove.OptionsColumn.AllowEdit = False
        Me.cFTVNStateApprove.Width = 52
        '
        'RepositoryFTVNStateApprove
        '
        Me.RepositoryFTVNStateApprove.AutoHeight = False
        Me.RepositoryFTVNStateApprove.Caption = "Check"
        Me.RepositoryFTVNStateApprove.Name = "RepositoryFTVNStateApprove"
        Me.RepositoryFTVNStateApprove.ValueChecked = "1"
        Me.RepositoryFTVNStateApprove.ValueUnchecked = "0"
        '
        'cFTFGStateApprove
        '
        Me.cFTFGStateApprove.Caption = "FTFGStateApprove"
        Me.cFTFGStateApprove.ColumnEdit = Me.RepositoryFTFGStateApprove
        Me.cFTFGStateApprove.FieldName = "FTFGStateApprove"
        Me.cFTFGStateApprove.Name = "cFTFGStateApprove"
        Me.cFTFGStateApprove.OptionsColumn.AllowEdit = False
        Me.cFTFGStateApprove.Width = 55
        '
        'RepositoryFTFGStateApprove
        '
        Me.RepositoryFTFGStateApprove.AutoHeight = False
        Me.RepositoryFTFGStateApprove.Caption = "Check"
        Me.RepositoryFTFGStateApprove.Name = "RepositoryFTFGStateApprove"
        Me.RepositoryFTFGStateApprove.ValueChecked = "1"
        Me.RepositoryFTFGStateApprove.ValueUnchecked = "0"
        '
        'cFTStateMNGDepApp
        '
        Me.cFTStateMNGDepApp.Caption = "FTStateMNGDepApp"
        Me.cFTStateMNGDepApp.ColumnEdit = Me.RepositoryFTMngStateApp
        Me.cFTStateMNGDepApp.FieldName = "FTStateMNGDepApp"
        Me.cFTStateMNGDepApp.Name = "cFTStateMNGDepApp"
        Me.cFTStateMNGDepApp.OptionsColumn.AllowEdit = False
        Me.cFTStateMNGDepApp.Visible = True
        Me.cFTStateMNGDepApp.VisibleIndex = 12
        Me.cFTStateMNGDepApp.Width = 103
        '
        'RepositoryFTMngStateApp
        '
        Me.RepositoryFTMngStateApp.AutoHeight = False
        Me.RepositoryFTMngStateApp.Caption = "Check"
        Me.RepositoryFTMngStateApp.Name = "RepositoryFTMngStateApp"
        Me.RepositoryFTMngStateApp.ValueChecked = "1"
        Me.RepositoryFTMngStateApp.ValueUnchecked = "0"
        '
        'cFTStateManagerApp
        '
        Me.cFTStateManagerApp.Caption = "FTStateManagerApp"
        Me.cFTStateManagerApp.ColumnEdit = Me.RepositoryFTStateManagerApp
        Me.cFTStateManagerApp.FieldName = "FTStateManagerApp"
        Me.cFTStateManagerApp.Name = "cFTStateManagerApp"
        Me.cFTStateManagerApp.OptionsColumn.AllowEdit = False
        Me.cFTStateManagerApp.Visible = True
        Me.cFTStateManagerApp.VisibleIndex = 13
        Me.cFTStateManagerApp.Width = 80
        '
        'RepositoryFTStateManagerApp
        '
        Me.RepositoryFTStateManagerApp.AutoHeight = False
        Me.RepositoryFTStateManagerApp.Caption = "Check"
        Me.RepositoryFTStateManagerApp.Name = "RepositoryFTStateManagerApp"
        Me.RepositoryFTStateManagerApp.ValueChecked = "1"
        Me.RepositoryFTStateManagerApp.ValueUnchecked = "0"
        '
        'cFNReviseNo
        '
        Me.cFNReviseNo.Caption = "FNReviseNo"
        Me.cFNReviseNo.FieldName = "FNReviseNo"
        Me.cFNReviseNo.Name = "cFNReviseNo"
        Me.cFNReviseNo.OptionsColumn.AllowEdit = False
        Me.cFNReviseNo.Visible = True
        Me.cFNReviseNo.VisibleIndex = 14
        Me.cFNReviseNo.Width = 91
        '
        'cFDUpdDate
        '
        Me.cFDUpdDate.Caption = "FDUpdDate"
        Me.cFDUpdDate.FieldName = "FDUpdDate"
        Me.cFDUpdDate.Name = "cFDUpdDate"
        Me.cFDUpdDate.OptionsColumn.AllowEdit = False
        Me.cFDUpdDate.Visible = True
        Me.cFDUpdDate.VisibleIndex = 15
        Me.cFDUpdDate.Width = 101
        '
        'cFTUnitSectCode
        '
        Me.cFTUnitSectCode.Caption = "FTUnitSectCode"
        Me.cFTUnitSectCode.Name = "cFTUnitSectCode"
        Me.cFTUnitSectCode.OptionsColumn.AllowEdit = False
        Me.cFTUnitSectCode.Visible = True
        Me.cFTUnitSectCode.VisibleIndex = 17
        Me.cFTUnitSectCode.Width = 104
        '
        'cFTUnitSectName
        '
        Me.cFTUnitSectName.Caption = "FTUnitSectName"
        Me.cFTUnitSectName.FieldName = "FTUnitSectName"
        Me.cFTUnitSectName.Name = "cFTUnitSectName"
        Me.cFTUnitSectName.OptionsColumn.AllowEdit = False
        Me.cFTUnitSectName.Visible = True
        Me.cFTUnitSectName.VisibleIndex = 18
        Me.cFTUnitSectName.Width = 95
        '
        'cFTUnitSectCodeCreate
        '
        Me.cFTUnitSectCodeCreate.Caption = "FTUnitSectCodeCreate"
        Me.cFTUnitSectCodeCreate.FieldName = "FTUnitSectCodeCreate"
        Me.cFTUnitSectCodeCreate.Name = "cFTUnitSectCodeCreate"
        Me.cFTUnitSectCodeCreate.OptionsColumn.AllowEdit = False
        Me.cFTUnitSectCodeCreate.Visible = True
        Me.cFTUnitSectCodeCreate.VisibleIndex = 19
        Me.cFTUnitSectCodeCreate.Width = 93
        '
        'cFTUnitSectNameCreate
        '
        Me.cFTUnitSectNameCreate.Caption = "FTUnitSectNameCreate"
        Me.cFTUnitSectNameCreate.FieldName = "FTUnitSectNameCreate"
        Me.cFTUnitSectNameCreate.Name = "cFTUnitSectNameCreate"
        Me.cFTUnitSectNameCreate.OptionsColumn.AllowEdit = False
        Me.cFTUnitSectNameCreate.Visible = True
        Me.cFTUnitSectNameCreate.VisibleIndex = 20
        Me.cFTUnitSectNameCreate.Width = 133
        '
        'RepositoryFTSelect
        '
        Me.RepositoryFTSelect.AutoHeight = False
        Me.RepositoryFTSelect.Caption = "Check"
        Me.RepositoryFTSelect.Name = "RepositoryFTSelect"
        Me.RepositoryFTSelect.ValueChecked = "1"
        Me.RepositoryFTSelect.ValueUnchecked = "0"
        '
        'RepositoryFTSandApprove
        '
        Me.RepositoryFTSandApprove.AutoHeight = False
        Me.RepositoryFTSandApprove.Caption = "Check"
        Me.RepositoryFTSandApprove.Name = "RepositoryFTSandApprove"
        Me.RepositoryFTSandApprove.ValueChecked = "1"
        Me.RepositoryFTSandApprove.ValueUnchecked = "0"
        '
        'wTrackingDoc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1213, 686)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogrpdetail)
        Me.Controls.Add(Me.oCriteria)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wTrackingDoc"
        Me.Text = "wTrackingDoc"
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.oCriteria.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.FTEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNFileType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStartDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNDocType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpIdTo_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpIdTo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogrpdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpdetail.ResumeLayout(False)
        CType(Me.ogcdocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdocument, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFT91StateApprove, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFT70StateApprove, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTC1StateApprove, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTC2StateApprove, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTC3StateApprove, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSRStateApprove, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSPStateApprove, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTCDStateApprove, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTVNStateApprove, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTFGStateApprove, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTMngStateApp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTStateManagerApp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSandApprove, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents oCriteria As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ogrpdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcdocument As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdocument As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTDocumentNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDDocumentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDocumentBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDocumentTitle As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTNote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTBenefit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNOperActivity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTOperActivityName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDocName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDocRefCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDocTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTFileTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFBDocument As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNDocType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNHSysCmpId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFT91StateApprove As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFT91StateApprove As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFT70StateApprove As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFT70StateApprove As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTC1StateApprove As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTC1StateApprove As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTC2StateApprove As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTC2StateApprove As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTC3StateApprove As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTC3StateApprove As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTSRStateApprove As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTSRStateApprove As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTSPStateApprove As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTSPStateApprove As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTCDStateApprove As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTCDStateApprove As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTVNStateApprove As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTVNStateApprove As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTFGStateApprove As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTFGStateApprove As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryFTSandApprove As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FNHSysCmpIdTo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Public WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Public WithEvents FNHSysCmpIdTo_None As DevExpress.XtraEditors.TextEdit
    Public WithEvents FNHSysCmpIdTo As DevExpress.XtraEditors.ButtonEdit
    Public WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNFileType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNFileType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNDocType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNDocType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTEndDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTEndDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTStartDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FTStartDate_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cFDDocAge As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStateManagerApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTStateManagerApp As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTStateMNGDepApp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTMngStateApp As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFNReviseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFDUpdDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTUnitSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTUnitSectName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTUnitSectCodeCreate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTUnitSectNameCreate As DevExpress.XtraGrid.Columns.GridColumn
End Class
