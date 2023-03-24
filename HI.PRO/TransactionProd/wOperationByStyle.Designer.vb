<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wOperationByStyle
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbStyleHeader = New DevExpress.XtraEditors.GroupControl()
        Me.FNHSysStyleId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysStyleId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogboperation = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmrefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcopy = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcoperation = New DevExpress.XtraGrid.GridControl()
        Me.ogvoperation = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysOperationId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNHSysOperationId = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.FNHSysOperationId_None = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysOperationId_Hide = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysMarkId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNHSysMarkId = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.FNHSysMarkId_None = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNOperationState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNOperationState = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.FNOperationState_Hide = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysMarkId_Hide = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysOperationIdTo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNHSysOperationIdTo = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.FNHSysOperationIdTo_None = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysOperationIdTo_Hide = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNPrice = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        CType(Me.ogbStyleHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbStyleHeader.SuspendLayout()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogboperation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogboperation.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogcoperation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvoperation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNHSysOperationId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNHSysMarkId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNOperationState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNHSysOperationIdTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbStyleHeader
        '
        Me.ogbStyleHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbStyleHeader.Controls.Add(Me.FNHSysStyleId_None)
        Me.ogbStyleHeader.Controls.Add(Me.FNHSysStyleId_lbl)
        Me.ogbStyleHeader.Controls.Add(Me.FNHSysStyleId)
        Me.ogbStyleHeader.Location = New System.Drawing.Point(2, 0)
        Me.ogbStyleHeader.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbStyleHeader.Name = "ogbStyleHeader"
        Me.ogbStyleHeader.Size = New System.Drawing.Size(1302, 74)
        Me.ogbStyleHeader.TabIndex = 2
        Me.ogbStyleHeader.Text = "Style Info"
        '
        'FNHSysStyleId_None
        '
        Me.FNHSysStyleId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysStyleId_None.Location = New System.Drawing.Point(362, 33)
        Me.FNHSysStyleId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId_None.Name = "FNHSysStyleId_None"
        Me.FNHSysStyleId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleId_None.Properties.ReadOnly = True
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(906, 22)
        Me.FNHSysStyleId_None.TabIndex = 3
        Me.FNHSysStyleId_None.Tag = "2|"
        '
        'FNHSysStyleId_lbl
        '
        Me.FNHSysStyleId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysStyleId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysStyleId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl.Location = New System.Drawing.Point(6, 33)
        Me.FNHSysStyleId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId_lbl.Name = "FNHSysStyleId_lbl"
        Me.FNHSysStyleId_lbl.Size = New System.Drawing.Size(196, 25)
        Me.FNHSysStyleId_lbl.TabIndex = 287
        Me.FNHSysStyleId_lbl.Tag = "2|"
        Me.FNHSysStyleId_lbl.Text = "Style No :"
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(209, 33)
        Me.FNHSysStyleId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "157", Nothing, True)})
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(152, 22)
        Me.FNHSysStyleId.TabIndex = 2
        Me.FNHSysStyleId.Tag = "2|"
        '
        'ogboperation
        '
        Me.ogboperation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogboperation.Controls.Add(Me.ogbmainprocbutton)
        Me.ogboperation.Controls.Add(Me.ogcoperation)
        Me.ogboperation.Location = New System.Drawing.Point(2, 79)
        Me.ogboperation.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogboperation.Name = "ogboperation"
        Me.ogboperation.Size = New System.Drawing.Size(1302, 709)
        Me.ogboperation.TabIndex = 3
        Me.ogboperation.Text = "Operation By Style"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmrefresh)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmcopy)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(91, 326)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1120, 58)
        Me.ogbmainprocbutton.TabIndex = 302
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmrefresh
        '
        Me.ocmrefresh.Location = New System.Drawing.Point(505, 14)
        Me.ocmrefresh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmrefresh.Name = "ocmrefresh"
        Me.ocmrefresh.Size = New System.Drawing.Size(111, 31)
        Me.ocmrefresh.TabIndex = 112
        Me.ocmrefresh.TabStop = False
        Me.ocmrefresh.Tag = "2|"
        Me.ocmrefresh.Text = "Refresh"
        '
        'ocmcopy
        '
        Me.ocmcopy.Location = New System.Drawing.Point(246, 14)
        Me.ocmcopy.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcopy.Name = "ocmcopy"
        Me.ocmcopy.Size = New System.Drawing.Size(111, 31)
        Me.ocmcopy.TabIndex = 97
        Me.ocmcopy.TabStop = False
        Me.ocmcopy.Tag = "2|"
        Me.ocmcopy.Text = "Copy"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(988, 14)
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
        Me.ocmclear.Location = New System.Drawing.Point(128, 14)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(111, 31)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
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
        'ogcoperation
        '
        Me.ogcoperation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcoperation.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcoperation.Location = New System.Drawing.Point(2, 25)
        Me.ogcoperation.MainView = Me.ogvoperation
        Me.ogcoperation.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcoperation.Name = "ogcoperation"
        Me.ogcoperation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFNHSysOperationId, Me.ReposFNOperationState, Me.ReposFNHSysMarkId, Me.ReposFNHSysOperationIdTo, Me.ReposFNPrice})
        Me.ogcoperation.Size = New System.Drawing.Size(1298, 682)
        Me.ogcoperation.TabIndex = 301
        Me.ogcoperation.TabStop = False
        Me.ogcoperation.Tag = "3|"
        Me.ogcoperation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvoperation})
        '
        'ogvoperation
        '
        Me.ogvoperation.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNSeq, Me.FNHSysOperationId, Me.FNHSysOperationId_None, Me.FNHSysOperationId_Hide, Me.FNHSysMarkId, Me.FNHSysMarkId_None, Me.FNOperationState, Me.FNOperationState_Hide, Me.FNHSysMarkId_Hide, Me.FNHSysOperationIdTo, Me.FNHSysOperationIdTo_None, Me.FNHSysOperationIdTo_Hide, Me.FNPrice})
        Me.ogvoperation.GridControl = Me.ogcoperation
        Me.ogvoperation.Name = "ogvoperation"
        Me.ogvoperation.OptionsCustomization.AllowGroup = False
        Me.ogvoperation.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvoperation.OptionsView.ColumnAutoWidth = False
        Me.ogvoperation.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvoperation.OptionsView.ShowGroupPanel = False
        Me.ogvoperation.Tag = "2|"
        '
        'FNSeq
        '
        Me.FNSeq.AppearanceCell.Options.UseTextOptions = True
        Me.FNSeq.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSeq.AppearanceHeader.Options.UseTextOptions = True
        Me.FNSeq.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNSeq.Caption = "FNSeq"
        Me.FNSeq.DisplayFormat.FormatString = "{0:n0}"
        Me.FNSeq.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSeq.FieldName = "FNSeq"
        Me.FNSeq.Name = "FNSeq"
        Me.FNSeq.OptionsColumn.AllowEdit = False
        Me.FNSeq.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSeq.OptionsColumn.AllowMove = False
        Me.FNSeq.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSeq.OptionsColumn.ReadOnly = True
        Me.FNSeq.Visible = True
        Me.FNSeq.VisibleIndex = 0
        Me.FNSeq.Width = 62
        '
        'FNHSysOperationId
        '
        Me.FNHSysOperationId.AppearanceHeader.Options.UseTextOptions = True
        Me.FNHSysOperationId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNHSysOperationId.Caption = "FNHSysOperationId"
        Me.FNHSysOperationId.ColumnEdit = Me.ReposFNHSysOperationId
        Me.FNHSysOperationId.FieldName = "FNHSysOperationId"
        Me.FNHSysOperationId.Name = "FNHSysOperationId"
        Me.FNHSysOperationId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysOperationId.OptionsColumn.AllowMove = False
        Me.FNHSysOperationId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysOperationId.Visible = True
        Me.FNHSysOperationId.VisibleIndex = 1
        Me.FNHSysOperationId.Width = 132
        '
        'ReposFNHSysOperationId
        '
        Me.ReposFNHSysOperationId.AutoHeight = False
        Me.ReposFNHSysOperationId.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "155", Nothing, True)})
        Me.ReposFNHSysOperationId.Name = "ReposFNHSysOperationId"
        '
        'FNHSysOperationId_None
        '
        Me.FNHSysOperationId_None.AppearanceHeader.Options.UseTextOptions = True
        Me.FNHSysOperationId_None.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNHSysOperationId_None.Caption = "FNHSysOperationId_None"
        Me.FNHSysOperationId_None.FieldName = "FNHSysOperationId_None"
        Me.FNHSysOperationId_None.Name = "FNHSysOperationId_None"
        Me.FNHSysOperationId_None.OptionsColumn.AllowEdit = False
        Me.FNHSysOperationId_None.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysOperationId_None.OptionsColumn.AllowMove = False
        Me.FNHSysOperationId_None.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysOperationId_None.OptionsColumn.ReadOnly = True
        Me.FNHSysOperationId_None.Visible = True
        Me.FNHSysOperationId_None.VisibleIndex = 2
        Me.FNHSysOperationId_None.Width = 213
        '
        'FNHSysOperationId_Hide
        '
        Me.FNHSysOperationId_Hide.Caption = "FNHSysOperationId_Hide"
        Me.FNHSysOperationId_Hide.FieldName = "FNHSysOperationId_Hide"
        Me.FNHSysOperationId_Hide.Name = "FNHSysOperationId_Hide"
        Me.FNHSysOperationId_Hide.OptionsColumn.AllowEdit = False
        Me.FNHSysOperationId_Hide.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysOperationId_Hide.OptionsColumn.AllowMove = False
        Me.FNHSysOperationId_Hide.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysOperationId_Hide.OptionsColumn.ReadOnly = True
        '
        'FNHSysMarkId
        '
        Me.FNHSysMarkId.AppearanceHeader.Options.UseTextOptions = True
        Me.FNHSysMarkId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNHSysMarkId.Caption = "FNHSysMarkId"
        Me.FNHSysMarkId.ColumnEdit = Me.ReposFNHSysMarkId
        Me.FNHSysMarkId.FieldName = "FNHSysMarkId"
        Me.FNHSysMarkId.Name = "FNHSysMarkId"
        Me.FNHSysMarkId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysMarkId.OptionsColumn.AllowMove = False
        Me.FNHSysMarkId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysMarkId.Width = 117
        '
        'ReposFNHSysMarkId
        '
        Me.ReposFNHSysMarkId.AutoHeight = False
        Me.ReposFNHSysMarkId.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "158", Nothing, True)})
        Me.ReposFNHSysMarkId.Name = "ReposFNHSysMarkId"
        '
        'FNHSysMarkId_None
        '
        Me.FNHSysMarkId_None.AppearanceHeader.Options.UseTextOptions = True
        Me.FNHSysMarkId_None.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNHSysMarkId_None.Caption = "FNHSysMarkId_None"
        Me.FNHSysMarkId_None.FieldName = "FNHSysMarkId_None"
        Me.FNHSysMarkId_None.Name = "FNHSysMarkId_None"
        Me.FNHSysMarkId_None.OptionsColumn.AllowEdit = False
        Me.FNHSysMarkId_None.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysMarkId_None.OptionsColumn.AllowMove = False
        Me.FNHSysMarkId_None.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysMarkId_None.OptionsColumn.ReadOnly = True
        Me.FNHSysMarkId_None.Width = 191
        '
        'FNOperationState
        '
        Me.FNOperationState.AppearanceHeader.Options.UseTextOptions = True
        Me.FNOperationState.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNOperationState.Caption = "FNOperationState"
        Me.FNOperationState.ColumnEdit = Me.ReposFNOperationState
        Me.FNOperationState.FieldName = "FNOperationState"
        Me.FNOperationState.Name = "FNOperationState"
        Me.FNOperationState.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNOperationState.OptionsColumn.AllowMove = False
        Me.FNOperationState.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNOperationState.Visible = True
        Me.FNOperationState.VisibleIndex = 3
        Me.FNOperationState.Width = 161
        '
        'ReposFNOperationState
        '
        Me.ReposFNOperationState.AutoHeight = False
        Me.ReposFNOperationState.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNOperationState.Name = "ReposFNOperationState"
        Me.ReposFNOperationState.Tag = "FNOperationState"
        '
        'FNOperationState_Hide
        '
        Me.FNOperationState_Hide.Caption = "FNOperationState_Hide"
        Me.FNOperationState_Hide.FieldName = "FNOperationState_Hide"
        Me.FNOperationState_Hide.Name = "FNOperationState_Hide"
        Me.FNOperationState_Hide.OptionsColumn.AllowEdit = False
        Me.FNOperationState_Hide.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNOperationState_Hide.OptionsColumn.AllowMove = False
        Me.FNOperationState_Hide.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNOperationState_Hide.OptionsColumn.ReadOnly = True
        '
        'FNHSysMarkId_Hide
        '
        Me.FNHSysMarkId_Hide.Caption = "FNHSysMarkId_Hide"
        Me.FNHSysMarkId_Hide.FieldName = "FNHSysMarkId_Hide"
        Me.FNHSysMarkId_Hide.Name = "FNHSysMarkId_Hide"
        Me.FNHSysMarkId_Hide.OptionsColumn.AllowEdit = False
        Me.FNHSysMarkId_Hide.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysMarkId_Hide.OptionsColumn.AllowMove = False
        Me.FNHSysMarkId_Hide.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysMarkId_Hide.OptionsColumn.ReadOnly = True
        '
        'FNHSysOperationIdTo
        '
        Me.FNHSysOperationIdTo.AppearanceHeader.Options.UseTextOptions = True
        Me.FNHSysOperationIdTo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNHSysOperationIdTo.Caption = "FNHSysOperationIdTo"
        Me.FNHSysOperationIdTo.ColumnEdit = Me.ReposFNHSysOperationIdTo
        Me.FNHSysOperationIdTo.FieldName = "FNHSysOperationIdTo"
        Me.FNHSysOperationIdTo.Name = "FNHSysOperationIdTo"
        Me.FNHSysOperationIdTo.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysOperationIdTo.OptionsColumn.AllowMove = False
        Me.FNHSysOperationIdTo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysOperationIdTo.Visible = True
        Me.FNHSysOperationIdTo.VisibleIndex = 4
        Me.FNHSysOperationIdTo.Width = 108
        '
        'ReposFNHSysOperationIdTo
        '
        Me.ReposFNHSysOperationIdTo.AutoHeight = False
        Me.ReposFNHSysOperationIdTo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "156", Nothing, True)})
        Me.ReposFNHSysOperationIdTo.Name = "ReposFNHSysOperationIdTo"
        '
        'FNHSysOperationIdTo_None
        '
        Me.FNHSysOperationIdTo_None.AppearanceHeader.Options.UseTextOptions = True
        Me.FNHSysOperationIdTo_None.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNHSysOperationIdTo_None.Caption = "FNHSysOperationIdTo_None"
        Me.FNHSysOperationIdTo_None.FieldName = "FNHSysOperationIdTo_None"
        Me.FNHSysOperationIdTo_None.Name = "FNHSysOperationIdTo_None"
        Me.FNHSysOperationIdTo_None.OptionsColumn.AllowEdit = False
        Me.FNHSysOperationIdTo_None.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysOperationIdTo_None.OptionsColumn.AllowMove = False
        Me.FNHSysOperationIdTo_None.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysOperationIdTo_None.OptionsColumn.ReadOnly = True
        Me.FNHSysOperationIdTo_None.Visible = True
        Me.FNHSysOperationIdTo_None.VisibleIndex = 5
        Me.FNHSysOperationIdTo_None.Width = 167
        '
        'FNHSysOperationIdTo_Hide
        '
        Me.FNHSysOperationIdTo_Hide.Caption = "FNHSysOperationIdTo_Hide"
        Me.FNHSysOperationIdTo_Hide.FieldName = "FNHSysOperationIdTo_Hide"
        Me.FNHSysOperationIdTo_Hide.Name = "FNHSysOperationIdTo_Hide"
        Me.FNHSysOperationIdTo_Hide.OptionsColumn.AllowEdit = False
        Me.FNHSysOperationIdTo_Hide.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysOperationIdTo_Hide.OptionsColumn.AllowMove = False
        Me.FNHSysOperationIdTo_Hide.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysOperationIdTo_Hide.OptionsColumn.ReadOnly = True
        '
        'FNPrice
        '
        Me.FNPrice.Caption = "FNPrice"
        Me.FNPrice.ColumnEdit = Me.ReposFNPrice
        Me.FNPrice.DisplayFormat.FormatString = "{0:n4}"
        Me.FNPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPrice.FieldName = "FNPrice"
        Me.FNPrice.Name = "FNPrice"
        Me.FNPrice.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNPrice.OptionsColumn.AllowShowHide = False
        Me.FNPrice.OptionsColumn.ShowInCustomizationForm = False
        Me.FNPrice.Visible = True
        Me.FNPrice.VisibleIndex = 6
        '
        'ReposFNPrice
        '
        Me.ReposFNPrice.AutoHeight = False
        Me.ReposFNPrice.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNPrice.DisplayFormat.FormatString = "{0:n4}"
        Me.ReposFNPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNPrice.EditFormat.FormatString = "{0:n4}"
        Me.ReposFNPrice.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNPrice.Name = "ReposFNPrice"
        Me.ReposFNPrice.Precision = 4
        '
        'wOperationByStyle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1304, 793)
        Me.Controls.Add(Me.ogboperation)
        Me.Controls.Add(Me.ogbStyleHeader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wOperationByStyle"
        Me.Text = "wOperationByStyle"
        CType(Me.ogbStyleHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbStyleHeader.ResumeLayout(False)
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogboperation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogboperation.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogcoperation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvoperation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNHSysOperationId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNHSysMarkId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNOperationState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNHSysOperationIdTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNPrice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbStyleHeader As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNHSysStyleId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysStyleId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogboperation As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcoperation As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvoperation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysOperationId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNHSysOperationId As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents FNHSysOperationId_None As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysOperationId_Hide As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysMarkId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNHSysMarkId As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents FNHSysMarkId_None As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNOperationState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNOperationState As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents FNOperationState_Hide As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysMarkId_Hide As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysOperationIdTo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNHSysOperationIdTo As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents FNHSysOperationIdTo_None As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysOperationIdTo_Hide As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcopy As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmrefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNPrice As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
End Class
