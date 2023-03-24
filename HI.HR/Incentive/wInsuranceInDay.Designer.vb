<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wInsuranceInDay
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
        Me.FTDateRequest_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTDateRequest = New DevExpress.XtraEditors.DateEdit()
        Me.ogbemployee = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNHSysUnitSectId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitSectName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateInsuranceInDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTStateInsuranceInH1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateInsuranceInH2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateInsuranceInH3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateInsuranceInH4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateInsuranceInH5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateInsuranceInH6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateInsuranceInH7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateInsuranceInH8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateInsuranceInH9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateInsuranceInH10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateInsuranceInH11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateInsuranceInH12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateInsuranceInH13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTApproveState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ReposFTStateDaily = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ogbInsurance = New DevExpress.XtraEditors.GroupControl()
        Me.oDockManager = New DevExpress.XtraBars.Docking.DockManager()
        Me.ogbdetail = New DevExpress.XtraEditors.GroupControl()
        CType(Me.FTDateRequest.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTDateRequest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbemployee, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbemployee.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTStateDaily, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbInsurance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbInsurance.SuspendLayout()
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbdetail.SuspendLayout()
        Me.SuspendLayout()
        '
        'FTDateRequest_lbl
        '
        Me.FTDateRequest_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTDateRequest_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTDateRequest_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTDateRequest_lbl.Location = New System.Drawing.Point(132, 29)
        Me.FTDateRequest_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTDateRequest_lbl.Name = "FTDateRequest_lbl"
        Me.FTDateRequest_lbl.Size = New System.Drawing.Size(122, 23)
        Me.FTDateRequest_lbl.TabIndex = 280
        Me.FTDateRequest_lbl.Tag = "2|"
        Me.FTDateRequest_lbl.Text = "Date :"
        '
        'FTDateRequest
        '
        Me.FTDateRequest.EditValue = Nothing
        Me.FTDateRequest.EnterMoveNextControl = True
        Me.FTDateRequest.Location = New System.Drawing.Point(255, 29)
        Me.FTDateRequest.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTDateRequest.Name = "FTDateRequest"
        Me.FTDateRequest.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTDateRequest.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FTDateRequest.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTDateRequest.Properties.DisplayFormat.FormatString = "d"
        Me.FTDateRequest.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTDateRequest.Properties.EditFormat.FormatString = "d"
        Me.FTDateRequest.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom
        Me.FTDateRequest.Properties.NullDate = ""
        Me.FTDateRequest.Size = New System.Drawing.Size(132, 22)
        Me.FTDateRequest.TabIndex = 0
        Me.FTDateRequest.Tag = "2|"
        '
        'ogbemployee
        '
        Me.ogbemployee.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbemployee.AppearanceCaption.Options.UseTextOptions = True
        Me.ogbemployee.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.ogbemployee.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbemployee.Controls.Add(Me.ogc)
        Me.ogbemployee.Location = New System.Drawing.Point(5, 82)
        Me.ogbemployee.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbemployee.Name = "ogbemployee"
        Me.ogbemployee.Size = New System.Drawing.Size(1382, 703)
        Me.ogbemployee.TabIndex = 4
        Me.ogbemployee.Text = "Unit Sect"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdelete)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(75, 126)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1132, 58)
        Me.ogbmainprocbutton.TabIndex = 100000
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(1000, 14)
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
        Me.ocmclear.Location = New System.Drawing.Point(246, 14)
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
        Me.ocmdelete.Location = New System.Drawing.Point(128, 14)
        Me.ocmdelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(111, 31)
        Me.ocmdelete.TabIndex = 94
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "DELETE"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(10, 14)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(111, 31)
        Me.ocmsave.TabIndex = 93
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'ogc
        '
        Me.ogc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Location = New System.Drawing.Point(5, 29)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect, Me.RepositoryFTApproveState, Me.ReposFTStateDaily})
        Me.ogc.Size = New System.Drawing.Size(1375, 667)
        Me.ogc.TabIndex = 3
        Me.ogc.TabStop = False
        Me.ogc.Tag = "2|"
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNHSysUnitSectId, Me.FTUnitSectCode, Me.FTUnitSectName, Me.FTStateInsuranceInDay, Me.FTStateInsuranceInH1, Me.FTStateInsuranceInH2, Me.FTStateInsuranceInH3, Me.FTStateInsuranceInH4, Me.FTStateInsuranceInH5, Me.FTStateInsuranceInH6, Me.FTStateInsuranceInH7, Me.FTStateInsuranceInH8, Me.FTStateInsuranceInH9, Me.FTStateInsuranceInH10, Me.FTStateInsuranceInH11, Me.FTStateInsuranceInH12, Me.FTStateInsuranceInH13})
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowGroup = False
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'FNHSysUnitSectId
        '
        Me.FNHSysUnitSectId.Caption = "FNHSysUnitSectId"
        Me.FNHSysUnitSectId.FieldName = "FNHSysUnitSectId"
        Me.FNHSysUnitSectId.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.FNHSysUnitSectId.Name = "FNHSysUnitSectId"
        Me.FNHSysUnitSectId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysUnitSectId.OptionsColumn.AllowMove = False
        Me.FNHSysUnitSectId.OptionsColumn.AllowShowHide = False
        Me.FNHSysUnitSectId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysUnitSectId.OptionsColumn.ShowInCustomizationForm = False
        '
        'FTUnitSectCode
        '
        Me.FTUnitSectCode.Caption = "UnitSect Code"
        Me.FTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.FTUnitSectCode.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.FTUnitSectCode.Name = "FTUnitSectCode"
        Me.FTUnitSectCode.OptionsColumn.AllowEdit = False
        Me.FTUnitSectCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTUnitSectCode.OptionsColumn.AllowMove = False
        Me.FTUnitSectCode.OptionsColumn.AllowShowHide = False
        Me.FTUnitSectCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTUnitSectCode.OptionsColumn.ReadOnly = True
        Me.FTUnitSectCode.OptionsColumn.ShowInCustomizationForm = False
        Me.FTUnitSectCode.Visible = True
        Me.FTUnitSectCode.VisibleIndex = 0
        Me.FTUnitSectCode.Width = 142
        '
        'FTUnitSectName
        '
        Me.FTUnitSectName.Caption = "UnitSect Name"
        Me.FTUnitSectName.FieldName = "FTUnitSectName"
        Me.FTUnitSectName.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.FTUnitSectName.Name = "FTUnitSectName"
        Me.FTUnitSectName.OptionsColumn.AllowEdit = False
        Me.FTUnitSectName.OptionsColumn.AllowMove = False
        Me.FTUnitSectName.OptionsColumn.AllowShowHide = False
        Me.FTUnitSectName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTUnitSectName.OptionsColumn.ReadOnly = True
        Me.FTUnitSectName.OptionsColumn.ShowInCustomizationForm = False
        Me.FTUnitSectName.Visible = True
        Me.FTUnitSectName.VisibleIndex = 1
        Me.FTUnitSectName.Width = 168
        '
        'FTStateInsuranceInDay
        '
        Me.FTStateInsuranceInDay.Caption = "Day"
        Me.FTStateInsuranceInDay.ColumnEdit = Me.RepositoryFTSelect
        Me.FTStateInsuranceInDay.FieldName = "FTStateInsuranceInDay"
        Me.FTStateInsuranceInDay.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.FTStateInsuranceInDay.Name = "FTStateInsuranceInDay"
        Me.FTStateInsuranceInDay.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInDay.OptionsColumn.AllowMove = False
        Me.FTStateInsuranceInDay.OptionsColumn.AllowShowHide = False
        Me.FTStateInsuranceInDay.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInDay.OptionsColumn.ShowInCustomizationForm = False
        Me.FTStateInsuranceInDay.Visible = True
        Me.FTStateInsuranceInDay.VisibleIndex = 2
        '
        'RepositoryFTSelect
        '
        Me.RepositoryFTSelect.AutoHeight = False
        Me.RepositoryFTSelect.Caption = "Check"
        Me.RepositoryFTSelect.Name = "RepositoryFTSelect"
        Me.RepositoryFTSelect.ValueChecked = "1"
        Me.RepositoryFTSelect.ValueUnchecked = "0"
        '
        'FTStateInsuranceInH1
        '
        Me.FTStateInsuranceInH1.Caption = "Hour 1"
        Me.FTStateInsuranceInH1.ColumnEdit = Me.RepositoryFTSelect
        Me.FTStateInsuranceInH1.FieldName = "FTStateInsuranceInH1"
        Me.FTStateInsuranceInH1.Name = "FTStateInsuranceInH1"
        Me.FTStateInsuranceInH1.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH1.OptionsColumn.AllowMove = False
        Me.FTStateInsuranceInH1.OptionsColumn.AllowShowHide = False
        Me.FTStateInsuranceInH1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH1.OptionsColumn.ShowInCustomizationForm = False
        Me.FTStateInsuranceInH1.Visible = True
        Me.FTStateInsuranceInH1.VisibleIndex = 3
        '
        'FTStateInsuranceInH2
        '
        Me.FTStateInsuranceInH2.Caption = "Hour 2"
        Me.FTStateInsuranceInH2.ColumnEdit = Me.RepositoryFTSelect
        Me.FTStateInsuranceInH2.FieldName = "FTStateInsuranceInH2"
        Me.FTStateInsuranceInH2.Name = "FTStateInsuranceInH2"
        Me.FTStateInsuranceInH2.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH2.OptionsColumn.AllowMove = False
        Me.FTStateInsuranceInH2.OptionsColumn.AllowShowHide = False
        Me.FTStateInsuranceInH2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH2.OptionsColumn.ShowInCustomizationForm = False
        Me.FTStateInsuranceInH2.Visible = True
        Me.FTStateInsuranceInH2.VisibleIndex = 4
        '
        'FTStateInsuranceInH3
        '
        Me.FTStateInsuranceInH3.Caption = "Hour 3"
        Me.FTStateInsuranceInH3.ColumnEdit = Me.RepositoryFTSelect
        Me.FTStateInsuranceInH3.FieldName = "FTStateInsuranceInH3"
        Me.FTStateInsuranceInH3.Name = "FTStateInsuranceInH3"
        Me.FTStateInsuranceInH3.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH3.OptionsColumn.AllowMove = False
        Me.FTStateInsuranceInH3.OptionsColumn.AllowShowHide = False
        Me.FTStateInsuranceInH3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH3.OptionsColumn.ShowInCustomizationForm = False
        Me.FTStateInsuranceInH3.Visible = True
        Me.FTStateInsuranceInH3.VisibleIndex = 5
        '
        'FTStateInsuranceInH4
        '
        Me.FTStateInsuranceInH4.Caption = "Hour 4"
        Me.FTStateInsuranceInH4.ColumnEdit = Me.RepositoryFTSelect
        Me.FTStateInsuranceInH4.FieldName = "FTStateInsuranceInH4"
        Me.FTStateInsuranceInH4.Name = "FTStateInsuranceInH4"
        Me.FTStateInsuranceInH4.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH4.OptionsColumn.AllowMove = False
        Me.FTStateInsuranceInH4.OptionsColumn.AllowShowHide = False
        Me.FTStateInsuranceInH4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH4.OptionsColumn.ShowInCustomizationForm = False
        Me.FTStateInsuranceInH4.Visible = True
        Me.FTStateInsuranceInH4.VisibleIndex = 6
        '
        'FTStateInsuranceInH5
        '
        Me.FTStateInsuranceInH5.Caption = "Hour 5"
        Me.FTStateInsuranceInH5.ColumnEdit = Me.RepositoryFTSelect
        Me.FTStateInsuranceInH5.FieldName = "FTStateInsuranceInH5"
        Me.FTStateInsuranceInH5.Name = "FTStateInsuranceInH5"
        Me.FTStateInsuranceInH5.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH5.OptionsColumn.AllowMove = False
        Me.FTStateInsuranceInH5.OptionsColumn.AllowShowHide = False
        Me.FTStateInsuranceInH5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH5.OptionsColumn.ShowInCustomizationForm = False
        Me.FTStateInsuranceInH5.Visible = True
        Me.FTStateInsuranceInH5.VisibleIndex = 7
        '
        'FTStateInsuranceInH6
        '
        Me.FTStateInsuranceInH6.Caption = "Hour 6"
        Me.FTStateInsuranceInH6.ColumnEdit = Me.RepositoryFTSelect
        Me.FTStateInsuranceInH6.FieldName = "FTStateInsuranceInH6"
        Me.FTStateInsuranceInH6.Name = "FTStateInsuranceInH6"
        Me.FTStateInsuranceInH6.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH6.OptionsColumn.AllowMove = False
        Me.FTStateInsuranceInH6.OptionsColumn.AllowShowHide = False
        Me.FTStateInsuranceInH6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH6.OptionsColumn.ShowInCustomizationForm = False
        Me.FTStateInsuranceInH6.Visible = True
        Me.FTStateInsuranceInH6.VisibleIndex = 8
        '
        'FTStateInsuranceInH7
        '
        Me.FTStateInsuranceInH7.Caption = "Hour 7"
        Me.FTStateInsuranceInH7.ColumnEdit = Me.RepositoryFTSelect
        Me.FTStateInsuranceInH7.FieldName = "FTStateInsuranceInH7"
        Me.FTStateInsuranceInH7.Name = "FTStateInsuranceInH7"
        Me.FTStateInsuranceInH7.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH7.OptionsColumn.AllowMove = False
        Me.FTStateInsuranceInH7.OptionsColumn.AllowShowHide = False
        Me.FTStateInsuranceInH7.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH7.OptionsColumn.ShowInCustomizationForm = False
        Me.FTStateInsuranceInH7.Visible = True
        Me.FTStateInsuranceInH7.VisibleIndex = 9
        '
        'FTStateInsuranceInH8
        '
        Me.FTStateInsuranceInH8.Caption = "Hour 8"
        Me.FTStateInsuranceInH8.ColumnEdit = Me.RepositoryFTSelect
        Me.FTStateInsuranceInH8.FieldName = "FTStateInsuranceInH8"
        Me.FTStateInsuranceInH8.Name = "FTStateInsuranceInH8"
        Me.FTStateInsuranceInH8.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH8.OptionsColumn.AllowMove = False
        Me.FTStateInsuranceInH8.OptionsColumn.AllowShowHide = False
        Me.FTStateInsuranceInH8.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH8.OptionsColumn.ShowInCustomizationForm = False
        Me.FTStateInsuranceInH8.Visible = True
        Me.FTStateInsuranceInH8.VisibleIndex = 10
        '
        'FTStateInsuranceInH9
        '
        Me.FTStateInsuranceInH9.Caption = "Hour 9"
        Me.FTStateInsuranceInH9.ColumnEdit = Me.RepositoryFTSelect
        Me.FTStateInsuranceInH9.FieldName = "FTStateInsuranceInH9"
        Me.FTStateInsuranceInH9.Name = "FTStateInsuranceInH9"
        Me.FTStateInsuranceInH9.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH9.OptionsColumn.AllowMove = False
        Me.FTStateInsuranceInH9.OptionsColumn.AllowShowHide = False
        Me.FTStateInsuranceInH9.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH9.OptionsColumn.ShowInCustomizationForm = False
        Me.FTStateInsuranceInH9.Visible = True
        Me.FTStateInsuranceInH9.VisibleIndex = 11
        '
        'FTStateInsuranceInH10
        '
        Me.FTStateInsuranceInH10.Caption = "Hour 10"
        Me.FTStateInsuranceInH10.ColumnEdit = Me.RepositoryFTSelect
        Me.FTStateInsuranceInH10.FieldName = "FTStateInsuranceInH10"
        Me.FTStateInsuranceInH10.Name = "FTStateInsuranceInH10"
        Me.FTStateInsuranceInH10.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH10.OptionsColumn.AllowMove = False
        Me.FTStateInsuranceInH10.OptionsColumn.AllowShowHide = False
        Me.FTStateInsuranceInH10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH10.OptionsColumn.ShowInCustomizationForm = False
        Me.FTStateInsuranceInH10.Visible = True
        Me.FTStateInsuranceInH10.VisibleIndex = 12
        '
        'FTStateInsuranceInH11
        '
        Me.FTStateInsuranceInH11.Caption = "Hour 11"
        Me.FTStateInsuranceInH11.ColumnEdit = Me.RepositoryFTSelect
        Me.FTStateInsuranceInH11.FieldName = "FTStateInsuranceInH11"
        Me.FTStateInsuranceInH11.Name = "FTStateInsuranceInH11"
        Me.FTStateInsuranceInH11.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH11.OptionsColumn.AllowMove = False
        Me.FTStateInsuranceInH11.OptionsColumn.AllowShowHide = False
        Me.FTStateInsuranceInH11.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH11.OptionsColumn.ShowInCustomizationForm = False
        Me.FTStateInsuranceInH11.Visible = True
        Me.FTStateInsuranceInH11.VisibleIndex = 13
        '
        'FTStateInsuranceInH12
        '
        Me.FTStateInsuranceInH12.Caption = "Hour 12"
        Me.FTStateInsuranceInH12.ColumnEdit = Me.RepositoryFTSelect
        Me.FTStateInsuranceInH12.FieldName = "FTStateInsuranceInH12"
        Me.FTStateInsuranceInH12.Name = "FTStateInsuranceInH12"
        Me.FTStateInsuranceInH12.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH12.OptionsColumn.AllowMove = False
        Me.FTStateInsuranceInH12.OptionsColumn.AllowShowHide = False
        Me.FTStateInsuranceInH12.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH12.OptionsColumn.ShowInCustomizationForm = False
        Me.FTStateInsuranceInH12.Visible = True
        Me.FTStateInsuranceInH12.VisibleIndex = 14
        '
        'FTStateInsuranceInH13
        '
        Me.FTStateInsuranceInH13.Caption = "Hour 13"
        Me.FTStateInsuranceInH13.ColumnEdit = Me.RepositoryFTSelect
        Me.FTStateInsuranceInH13.FieldName = "FTStateInsuranceInH13"
        Me.FTStateInsuranceInH13.Name = "FTStateInsuranceInH13"
        Me.FTStateInsuranceInH13.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH13.OptionsColumn.AllowMove = False
        Me.FTStateInsuranceInH13.OptionsColumn.AllowShowHide = False
        Me.FTStateInsuranceInH13.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTStateInsuranceInH13.OptionsColumn.ShowInCustomizationForm = False
        Me.FTStateInsuranceInH13.Visible = True
        Me.FTStateInsuranceInH13.VisibleIndex = 15
        '
        'RepositoryFTApproveState
        '
        Me.RepositoryFTApproveState.AutoHeight = False
        Me.RepositoryFTApproveState.Caption = "Check"
        Me.RepositoryFTApproveState.Name = "RepositoryFTApproveState"
        Me.RepositoryFTApproveState.ValueChecked = "1"
        Me.RepositoryFTApproveState.ValueUnchecked = "0"
        '
        'ReposFTStateDaily
        '
        Me.ReposFTStateDaily.AutoHeight = False
        Me.ReposFTStateDaily.Caption = "Check"
        Me.ReposFTStateDaily.Name = "ReposFTStateDaily"
        Me.ReposFTStateDaily.ValueChecked = "1"
        Me.ReposFTStateDaily.ValueUnchecked = "0"
        '
        'ogbInsurance
        '
        Me.ogbInsurance.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbInsurance.Controls.Add(Me.FTDateRequest)
        Me.ogbInsurance.Controls.Add(Me.FTDateRequest_lbl)
        Me.ogbInsurance.Location = New System.Drawing.Point(5, 6)
        Me.ogbInsurance.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbInsurance.Name = "ogbInsurance"
        Me.ogbInsurance.Size = New System.Drawing.Size(1382, 68)
        Me.ogbInsurance.TabIndex = 5
        Me.ogbInsurance.Text = "Insurance Date"
        '
        'oDockManager
        '
        Me.oDockManager.Form = Me
        Me.oDockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'ogbdetail
        '
        Me.ogbdetail.Controls.Add(Me.ogbInsurance)
        Me.ogbdetail.Controls.Add(Me.ogbemployee)
        Me.ogbdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogbdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbdetail.Name = "ogbdetail"
        Me.ogbdetail.ShowCaption = False
        Me.ogbdetail.Size = New System.Drawing.Size(1392, 791)
        Me.ogbdetail.TabIndex = 100001
        Me.ogbdetail.Text = "GroupControl1"
        '
        'wInsuranceInDay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1392, 791)
        Me.Controls.Add(Me.ogbdetail)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wInsuranceInDay"
        Me.Text = "Insurance In Day"
        CType(Me.FTDateRequest.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTDateRequest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbemployee, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbemployee.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTApproveState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTStateDaily, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbInsurance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbInsurance.ResumeLayout(False)
        CType(Me.oDockManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbdetail.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FTDateRequest_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTDateRequest As DevExpress.XtraEditors.DateEdit
    Friend WithEvents ogbemployee As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbInsurance As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryFTApproveState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents oDockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogbdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ReposFTStateDaily As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FNHSysUnitSectId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitSectName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateInsuranceInDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateInsuranceInH1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateInsuranceInH2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateInsuranceInH3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateInsuranceInH4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateInsuranceInH5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateInsuranceInH6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateInsuranceInH7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateInsuranceInH8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateInsuranceInH9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateInsuranceInH10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateInsuranceInH11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateInsuranceInH12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateInsuranceInH13 As DevExpress.XtraGrid.Columns.GridColumn
End Class
