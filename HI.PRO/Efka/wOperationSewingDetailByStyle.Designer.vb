<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wOperationSewingDetailByStyle
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
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcoperation = New DevExpress.XtraGrid.GridControl()
        Me.ogvoperation = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTOperationName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFTOperationName = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.CFNSam = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNPrice = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.CFNStartSewingfoot = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNStartSewingfoot = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.CFNEndSewingfoot = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNStartStitches = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNEndStitches = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNHSysOperationId = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.ReposFNOperationState = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.ReposFNHSysMarkId = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.ReposFNHSysOperationIdTo = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.CFNStartBetweenTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNEndBetweenTime = New DevExpress.XtraGrid.Columns.GridColumn()
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
        CType(Me.ReposFTOperationName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNStartSewingfoot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNHSysOperationId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNOperationState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNHSysMarkId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNHSysOperationIdTo, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogboperation.Text = "Operation Sewing DetailBy Style"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmrefresh)
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
        Me.ogcoperation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFNHSysOperationId, Me.ReposFNOperationState, Me.ReposFNHSysMarkId, Me.ReposFNHSysOperationIdTo, Me.ReposFNPrice, Me.ReposFTOperationName, Me.ReposFNStartSewingfoot})
        Me.ogcoperation.Size = New System.Drawing.Size(1298, 682)
        Me.ogcoperation.TabIndex = 301
        Me.ogcoperation.TabStop = False
        Me.ogcoperation.Tag = "3|"
        Me.ogcoperation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvoperation})
        '
        'ogvoperation
        '
        Me.ogvoperation.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNSeq, Me.CFTOperationName, Me.CFNSam, Me.CFNStartSewingfoot, Me.CFNEndSewingfoot, Me.CFNStartStitches, Me.CFNEndStitches, Me.CFNStartBetweenTime, Me.CFNEndBetweenTime})
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
        'CFTOperationName
        '
        Me.CFTOperationName.Caption = "ขั้นตอนการเย็บ"
        Me.CFTOperationName.ColumnEdit = Me.ReposFTOperationName
        Me.CFTOperationName.FieldName = "FTOperationName"
        Me.CFTOperationName.Name = "CFTOperationName"
        Me.CFTOperationName.Visible = True
        Me.CFTOperationName.VisibleIndex = 1
        Me.CFTOperationName.Width = 468
        '
        'ReposFTOperationName
        '
        Me.ReposFTOperationName.AutoHeight = False
        Me.ReposFTOperationName.MaxLength = 500
        Me.ReposFTOperationName.Name = "ReposFTOperationName"
        '
        'CFNSam
        '
        Me.CFNSam.Caption = "Sam"
        Me.CFNSam.ColumnEdit = Me.ReposFNPrice
        Me.CFNSam.DisplayFormat.FormatString = "{0:n4}"
        Me.CFNSam.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNSam.FieldName = "FNSam"
        Me.CFNSam.Name = "CFNSam"
        Me.CFNSam.Visible = True
        Me.CFNSam.VisibleIndex = 2
        Me.CFNSam.Width = 103
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
        'CFNStartSewingfoot
        '
        Me.CFNStartSewingfoot.Caption = "ตีนผีเริ่มต้น"
        Me.CFNStartSewingfoot.ColumnEdit = Me.ReposFNStartSewingfoot
        Me.CFNStartSewingfoot.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNStartSewingfoot.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNStartSewingfoot.FieldName = "FNStartSewingfoot"
        Me.CFNStartSewingfoot.Name = "CFNStartSewingfoot"
        Me.CFNStartSewingfoot.Visible = True
        Me.CFNStartSewingfoot.VisibleIndex = 3
        Me.CFNStartSewingfoot.Width = 133
        '
        'ReposFNStartSewingfoot
        '
        Me.ReposFNStartSewingfoot.AutoHeight = False
        Me.ReposFNStartSewingfoot.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNStartSewingfoot.DisplayFormat.FormatString = "{0:n0}"
        Me.ReposFNStartSewingfoot.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNStartSewingfoot.EditFormat.FormatString = "{0:n0}"
        Me.ReposFNStartSewingfoot.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNStartSewingfoot.Name = "ReposFNStartSewingfoot"
        '
        'CFNEndSewingfoot
        '
        Me.CFNEndSewingfoot.Caption = "ตีนผีสิ้นสุด"
        Me.CFNEndSewingfoot.ColumnEdit = Me.ReposFNStartSewingfoot
        Me.CFNEndSewingfoot.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNEndSewingfoot.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNEndSewingfoot.FieldName = "FNEndSewingfoot"
        Me.CFNEndSewingfoot.Name = "CFNEndSewingfoot"
        Me.CFNEndSewingfoot.Visible = True
        Me.CFNEndSewingfoot.VisibleIndex = 4
        Me.CFNEndSewingfoot.Width = 147
        '
        'CFNStartStitches
        '
        Me.CFNStartStitches.Caption = "ฝีเข็มเริ่มต้น"
        Me.CFNStartStitches.ColumnEdit = Me.ReposFNStartSewingfoot
        Me.CFNStartStitches.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNStartStitches.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNStartStitches.FieldName = "FNStartStitches"
        Me.CFNStartStitches.Name = "CFNStartStitches"
        Me.CFNStartStitches.Visible = True
        Me.CFNStartStitches.VisibleIndex = 5
        Me.CFNStartStitches.Width = 130
        '
        'CFNEndStitches
        '
        Me.CFNEndStitches.Caption = "ฝีเข็มสิ้นสุด"
        Me.CFNEndStitches.ColumnEdit = Me.ReposFNStartSewingfoot
        Me.CFNEndStitches.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNEndStitches.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNEndStitches.FieldName = "FNEndStitches"
        Me.CFNEndStitches.Name = "CFNEndStitches"
        Me.CFNEndStitches.Visible = True
        Me.CFNEndStitches.VisibleIndex = 6
        Me.CFNEndStitches.Width = 134
        '
        'ReposFNHSysOperationId
        '
        Me.ReposFNHSysOperationId.AutoHeight = False
        Me.ReposFNHSysOperationId.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "155", Nothing, True)})
        Me.ReposFNHSysOperationId.Name = "ReposFNHSysOperationId"
        '
        'ReposFNOperationState
        '
        Me.ReposFNOperationState.AutoHeight = False
        Me.ReposFNOperationState.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNOperationState.Name = "ReposFNOperationState"
        Me.ReposFNOperationState.Tag = "FNOperationState"
        '
        'ReposFNHSysMarkId
        '
        Me.ReposFNHSysMarkId.AutoHeight = False
        Me.ReposFNHSysMarkId.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "158", Nothing, True)})
        Me.ReposFNHSysMarkId.Name = "ReposFNHSysMarkId"
        '
        'ReposFNHSysOperationIdTo
        '
        Me.ReposFNHSysOperationIdTo.AutoHeight = False
        Me.ReposFNHSysOperationIdTo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "156", Nothing, True)})
        Me.ReposFNHSysOperationIdTo.Name = "ReposFNHSysOperationIdTo"
        '
        'CFNStartBetweenTime
        '
        Me.CFNStartBetweenTime.Caption = "Start Between Time"
        Me.CFNStartBetweenTime.ColumnEdit = Me.ReposFNStartSewingfoot
        Me.CFNStartBetweenTime.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNStartBetweenTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNStartBetweenTime.FieldName = "FNStartBetweenTime"
        Me.CFNStartBetweenTime.Name = "CFNStartBetweenTime"
        Me.CFNStartBetweenTime.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNStartBetweenTime.OptionsColumn.AllowMove = False
        Me.CFNStartBetweenTime.OptionsColumn.AllowShowHide = False
        Me.CFNStartBetweenTime.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNStartBetweenTime.Visible = True
        Me.CFNStartBetweenTime.VisibleIndex = 7
        '
        'CFNEndBetweenTime
        '
        Me.CFNEndBetweenTime.Caption = "End Between Time"
        Me.CFNEndBetweenTime.ColumnEdit = Me.ReposFNStartSewingfoot
        Me.CFNEndBetweenTime.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNEndBetweenTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNEndBetweenTime.FieldName = "FNEndBetweenTime"
        Me.CFNEndBetweenTime.Name = "CFNEndBetweenTime"
        Me.CFNEndBetweenTime.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNEndBetweenTime.OptionsColumn.AllowMove = False
        Me.CFNEndBetweenTime.OptionsColumn.AllowShowHide = False
        Me.CFNEndBetweenTime.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNEndBetweenTime.Visible = True
        Me.CFNEndBetweenTime.VisibleIndex = 8
        '
        'wOperationSewingDetailByStyle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1304, 793)
        Me.Controls.Add(Me.ogboperation)
        Me.Controls.Add(Me.ogbStyleHeader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wOperationSewingDetailByStyle"
        Me.Text = "Operation Sewing Detail By Style"
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
        CType(Me.ReposFTOperationName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNStartSewingfoot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNHSysOperationId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNOperationState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNHSysMarkId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNHSysOperationIdTo, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents ReposFNHSysOperationId As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents ReposFNHSysMarkId As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents ReposFNOperationState As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents ReposFNHSysOperationIdTo As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmrefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ReposFNPrice As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents CFTOperationName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFTOperationName As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents CFNSam As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNStartSewingfoot As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNStartSewingfoot As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents CFNEndSewingfoot As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNStartStitches As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNEndStitches As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNStartBetweenTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNEndBetweenTime As DevExpress.XtraGrid.Columns.GridColumn
End Class
