<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wMatQCSpareAdd
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.FTRemark_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRemark = New DevExpress.XtraEditors.MemoEdit()
        Me.FTStateActive = New DevExpress.XtraEditors.CheckEdit()
        Me.ogcDetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sFNStartQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCalcEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.sFNEndQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSpare = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCalcFNSpare = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.RepositoryFTStateSendApp = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryFTStateAppsdf = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.RepositoryFTStateApp = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemFTStateSendApp = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCalcEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FNSpareType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNSpareType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNSpareConditionType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.FNSpareConditionType_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysMatTypeId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysMatTypeId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysMatTypeId_None = New DevExpress.XtraEditors.TextEdit()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.btnExit = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateActive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcFNSpare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTStateSendApp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTStateAppsdf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTStateApp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTStateSendApp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNSpareType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNSpareConditionType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysMatTypeId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysMatTypeId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl2
        '
        Me.GroupControl2.Controls.Add(Me.FTRemark_lbl)
        Me.GroupControl2.Controls.Add(Me.FTRemark)
        Me.GroupControl2.Controls.Add(Me.FTStateActive)
        Me.GroupControl2.Controls.Add(Me.ogcDetail)
        Me.GroupControl2.Controls.Add(Me.FNSpareType_lbl)
        Me.GroupControl2.Controls.Add(Me.FNSpareType)
        Me.GroupControl2.Controls.Add(Me.FNSpareConditionType)
        Me.GroupControl2.Controls.Add(Me.FNSpareConditionType_lbl)
        Me.GroupControl2.Controls.Add(Me.FNHSysMatTypeId)
        Me.GroupControl2.Controls.Add(Me.FNHSysMatTypeId_lbl)
        Me.GroupControl2.Controls.Add(Me.FNHSysMatTypeId_None)
        Me.GroupControl2.Controls.Add(Me.btnSave)
        Me.GroupControl2.Controls.Add(Me.btnExit)
        Me.GroupControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl2.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupControl2.MinimumSize = New System.Drawing.Size(542, 191)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.ShowCaption = False
        Me.GroupControl2.Size = New System.Drawing.Size(723, 495)
        Me.GroupControl2.TabIndex = 309
        Me.GroupControl2.Text = "Finance Expend"
        '
        'FTRemark_lbl
        '
        Me.FTRemark_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTRemark_lbl.Appearance.Options.UseForeColor = True
        Me.FTRemark_lbl.Appearance.Options.UseTextOptions = True
        Me.FTRemark_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRemark_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRemark_lbl.Location = New System.Drawing.Point(48, 87)
        Me.FTRemark_lbl.Name = "FTRemark_lbl"
        Me.FTRemark_lbl.Size = New System.Drawing.Size(92, 19)
        Me.FTRemark_lbl.TabIndex = 510
        Me.FTRemark_lbl.Tag = "2|"
        Me.FTRemark_lbl.Text = "Note :"
        '
        'FTRemark
        '
        Me.FTRemark.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTRemark.EditValue = ""
        Me.FTRemark.Location = New System.Drawing.Point(147, 86)
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Properties.MaxLength = 500
        Me.FTRemark.Size = New System.Drawing.Size(511, 73)
        Me.FTRemark.TabIndex = 509
        Me.FTRemark.Tag = "2|"
        '
        'FTStateActive
        '
        Me.FTStateActive.EditValue = "0"
        Me.FTStateActive.Location = New System.Drawing.Point(416, 38)
        Me.FTStateActive.Name = "FTStateActive"
        Me.FTStateActive.Properties.Caption = "Active"
        Me.FTStateActive.Properties.ValueChecked = "1"
        Me.FTStateActive.Properties.ValueUnchecked = "0"
        Me.FTStateActive.Size = New System.Drawing.Size(242, 20)
        Me.FTStateActive.TabIndex = 508
        Me.FTStateActive.Tag = "2|"
        '
        'ogcDetail
        '
        Me.ogcDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcDetail.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.ogcDetail.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.ogcDetail.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.ogcDetail.EmbeddedNavigator.Buttons.First.Visible = False
        Me.ogcDetail.EmbeddedNavigator.Buttons.Last.Visible = False
        Me.ogcDetail.EmbeddedNavigator.Buttons.Next.Visible = False
        Me.ogcDetail.EmbeddedNavigator.Buttons.NextPage.Visible = False
        Me.ogcDetail.EmbeddedNavigator.Buttons.Prev.Visible = False
        Me.ogcDetail.EmbeddedNavigator.Buttons.PrevPage.Visible = False
        Me.ogcDetail.EmbeddedNavigator.TextStringFormat = ""
        Me.ogcDetail.Location = New System.Drawing.Point(5, 165)
        Me.ogcDetail.MainView = Me.ogvDetail
        Me.ogcDetail.Name = "ogcDetail"
        Me.ogcDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTStateSendApp, Me.RepositoryFTStateAppsdf, Me.RepositoryFTStateApp, Me.RepositoryItemFTStateSendApp, Me.RepositoryItemCalcFNSpare, Me.RepositoryItemCalcEdit1, Me.RepositoryItemCalcEdit2})
        Me.ogcDetail.Size = New System.Drawing.Size(714, 283)
        Me.ogcDetail.TabIndex = 507
        Me.ogcDetail.UseEmbeddedNavigator = True
        Me.ogcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDetail})
        '
        'ogvDetail
        '
        Me.ogvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNSeq, Me.sFNStartQty, Me.sFNEndQty, Me.FNSpare})
        Me.ogvDetail.DetailHeight = 284
        Me.ogvDetail.GridControl = Me.ogcDetail
        Me.ogvDetail.Name = "ogvDetail"
        Me.ogvDetail.OptionsView.ColumnAutoWidth = False
        Me.ogvDetail.OptionsView.ShowGroupPanel = False
        '
        'FNSeq
        '
        Me.FNSeq.Caption = "No."
        Me.FNSeq.DisplayFormat.FormatString = "{0:n0}"
        Me.FNSeq.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSeq.FieldName = "FNSeq"
        Me.FNSeq.Name = "FNSeq"
        Me.FNSeq.Visible = True
        Me.FNSeq.VisibleIndex = 0
        '
        'sFNStartQty
        '
        Me.sFNStartQty.Caption = "Start Quantity"
        Me.sFNStartQty.ColumnEdit = Me.RepositoryItemCalcEdit2
        Me.sFNStartQty.DisplayFormat.FormatString = "{0:n0}"
        Me.sFNStartQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.sFNStartQty.FieldName = "FNStartQty"
        Me.sFNStartQty.Name = "sFNStartQty"
        Me.sFNStartQty.Visible = True
        Me.sFNStartQty.VisibleIndex = 1
        Me.sFNStartQty.Width = 127
        '
        'RepositoryItemCalcEdit2
        '
        Me.RepositoryItemCalcEdit2.AutoHeight = False
        Me.RepositoryItemCalcEdit2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEdit2.DisplayFormat.FormatString = "{0:n0}"
        Me.RepositoryItemCalcEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemCalcEdit2.EditFormat.FormatString = "{0:n0}"
        Me.RepositoryItemCalcEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemCalcEdit2.Name = "RepositoryItemCalcEdit2"
        '
        'sFNEndQty
        '
        Me.sFNEndQty.Caption = "To Quantity"
        Me.sFNEndQty.ColumnEdit = Me.RepositoryItemCalcEdit2
        Me.sFNEndQty.DisplayFormat.FormatString = "{0:n0}"
        Me.sFNEndQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.sFNEndQty.FieldName = "FNEndQty"
        Me.sFNEndQty.Name = "sFNEndQty"
        Me.sFNEndQty.Visible = True
        Me.sFNEndQty.VisibleIndex = 2
        Me.sFNEndQty.Width = 157
        '
        'FNSpare
        '
        Me.FNSpare.Caption = "Spare"
        Me.FNSpare.ColumnEdit = Me.RepositoryItemCalcFNSpare
        Me.FNSpare.DisplayFormat.FormatString = "{0:n2}"
        Me.FNSpare.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSpare.FieldName = "FNSpare"
        Me.FNSpare.Name = "FNSpare"
        Me.FNSpare.Visible = True
        Me.FNSpare.VisibleIndex = 3
        Me.FNSpare.Width = 158
        '
        'RepositoryItemCalcFNSpare
        '
        Me.RepositoryItemCalcFNSpare.AutoHeight = False
        Me.RepositoryItemCalcFNSpare.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcFNSpare.DisplayFormat.FormatString = "{0:n2}"
        Me.RepositoryItemCalcFNSpare.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemCalcFNSpare.EditFormat.FormatString = "{0:n2}"
        Me.RepositoryItemCalcFNSpare.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemCalcFNSpare.Name = "RepositoryItemCalcFNSpare"
        '
        'RepositoryFTStateSendApp
        '
        Me.RepositoryFTStateSendApp.AutoHeight = False
        Me.RepositoryFTStateSendApp.Caption = "Check"
        Me.RepositoryFTStateSendApp.Name = "RepositoryFTStateSendApp"
        Me.RepositoryFTStateSendApp.ValueChecked = "1"
        Me.RepositoryFTStateSendApp.ValueUnchecked = "0"
        '
        'RepositoryFTStateAppsdf
        '
        Me.RepositoryFTStateAppsdf.AutoHeight = False
        Me.RepositoryFTStateAppsdf.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryFTStateAppsdf.Name = "RepositoryFTStateAppsdf"
        '
        'RepositoryFTStateApp
        '
        Me.RepositoryFTStateApp.AutoHeight = False
        Me.RepositoryFTStateApp.Caption = "Check"
        Me.RepositoryFTStateApp.Name = "RepositoryFTStateApp"
        Me.RepositoryFTStateApp.ValueChecked = "1"
        Me.RepositoryFTStateApp.ValueUnchecked = "0"
        '
        'RepositoryItemFTStateSendApp
        '
        Me.RepositoryItemFTStateSendApp.AutoHeight = False
        Me.RepositoryItemFTStateSendApp.Caption = "Check"
        Me.RepositoryItemFTStateSendApp.Name = "RepositoryItemFTStateSendApp"
        '
        'RepositoryItemCalcEdit1
        '
        Me.RepositoryItemCalcEdit1.AutoHeight = False
        Me.RepositoryItemCalcEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEdit1.Name = "RepositoryItemCalcEdit1"
        '
        'FNSpareType_lbl
        '
        Me.FNSpareType_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNSpareType_lbl.Appearance.Options.UseForeColor = True
        Me.FNSpareType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNSpareType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSpareType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNSpareType_lbl.Location = New System.Drawing.Point(10, 60)
        Me.FNSpareType_lbl.Name = "FNSpareType_lbl"
        Me.FNSpareType_lbl.Size = New System.Drawing.Size(133, 19)
        Me.FNSpareType_lbl.TabIndex = 506
        Me.FNSpareType_lbl.Tag = "2|"
        Me.FNSpareType_lbl.Text = "Spare Type :"
        '
        'FNSpareType
        '
        Me.FNSpareType.EditValue = ""
        Me.FNSpareType.EnterMoveNextControl = True
        Me.FNSpareType.Location = New System.Drawing.Point(147, 61)
        Me.FNSpareType.Name = "FNSpareType"
        Me.FNSpareType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNSpareType.Properties.Appearance.Options.UseBackColor = True
        Me.FNSpareType.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNSpareType.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNSpareType.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNSpareType.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNSpareType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNSpareType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNSpareType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNSpareType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNSpareType.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNSpareType.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNSpareType.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNSpareType.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNSpareType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNSpareType.Properties.Tag = "FNSpareType"
        Me.FNSpareType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNSpareType.Size = New System.Drawing.Size(214, 20)
        Me.FNSpareType.TabIndex = 505
        Me.FNSpareType.Tag = "2|"
        '
        'FNSpareConditionType
        '
        Me.FNSpareConditionType.EditValue = ""
        Me.FNSpareConditionType.EnterMoveNextControl = True
        Me.FNSpareConditionType.Location = New System.Drawing.Point(147, 36)
        Me.FNSpareConditionType.Name = "FNSpareConditionType"
        Me.FNSpareConditionType.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNSpareConditionType.Properties.Appearance.Options.UseBackColor = True
        Me.FNSpareConditionType.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNSpareConditionType.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNSpareConditionType.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNSpareConditionType.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNSpareConditionType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNSpareConditionType.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNSpareConditionType.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNSpareConditionType.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNSpareConditionType.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNSpareConditionType.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNSpareConditionType.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNSpareConditionType.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNSpareConditionType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FNSpareConditionType.Properties.Tag = "FNSpareConditionType"
        Me.FNSpareConditionType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.FNSpareConditionType.Size = New System.Drawing.Size(214, 20)
        Me.FNSpareConditionType.TabIndex = 503
        Me.FNSpareConditionType.Tag = "2|"
        '
        'FNSpareConditionType_lbl
        '
        Me.FNSpareConditionType_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNSpareConditionType_lbl.Appearance.Options.UseForeColor = True
        Me.FNSpareConditionType_lbl.Appearance.Options.UseTextOptions = True
        Me.FNSpareConditionType_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSpareConditionType_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNSpareConditionType_lbl.Location = New System.Drawing.Point(10, 35)
        Me.FNSpareConditionType_lbl.Name = "FNSpareConditionType_lbl"
        Me.FNSpareConditionType_lbl.Size = New System.Drawing.Size(133, 19)
        Me.FNSpareConditionType_lbl.TabIndex = 504
        Me.FNSpareConditionType_lbl.Tag = "2|"
        Me.FNSpareConditionType_lbl.Text = "Summary Condition :"
        '
        'FNHSysMatTypeId
        '
        Me.FNHSysMatTypeId.EnterMoveNextControl = True
        Me.FNHSysMatTypeId.Location = New System.Drawing.Point(147, 12)
        Me.FNHSysMatTypeId.Name = "FNHSysMatTypeId"
        Me.FNHSysMatTypeId.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysMatTypeId.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysMatTypeId.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysMatTypeId.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMatTypeId.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysMatTypeId.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysMatTypeId.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysMatTypeId.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMatTypeId.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysMatTypeId.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysMatTypeId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysMatTypeId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMatTypeId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysMatTypeId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysMatTypeId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", 15, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "109", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.FNHSysMatTypeId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysMatTypeId.Properties.MaxLength = 30
        Me.FNHSysMatTypeId.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysMatTypeId.TabIndex = 500
        Me.FNHSysMatTypeId.Tag = "2|"
        '
        'FNHSysMatTypeId_lbl
        '
        Me.FNHSysMatTypeId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMatTypeId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysMatTypeId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysMatTypeId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysMatTypeId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysMatTypeId_lbl.Location = New System.Drawing.Point(22, 12)
        Me.FNHSysMatTypeId_lbl.Name = "FNHSysMatTypeId_lbl"
        Me.FNHSysMatTypeId_lbl.Size = New System.Drawing.Size(122, 19)
        Me.FNHSysMatTypeId_lbl.TabIndex = 501
        Me.FNHSysMatTypeId_lbl.Tag = "2|"
        Me.FNHSysMatTypeId_lbl.Text = "Material Type :"
        '
        'FNHSysMatTypeId_None
        '
        Me.FNHSysMatTypeId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysMatTypeId_None.EnterMoveNextControl = True
        Me.FNHSysMatTypeId_None.Location = New System.Drawing.Point(281, 12)
        Me.FNHSysMatTypeId_None.Name = "FNHSysMatTypeId_None"
        Me.FNHSysMatTypeId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysMatTypeId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysMatTypeId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysMatTypeId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysMatTypeId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysMatTypeId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMatTypeId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysMatTypeId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysMatTypeId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysMatTypeId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMatTypeId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysMatTypeId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysMatTypeId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysMatTypeId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysMatTypeId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysMatTypeId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysMatTypeId_None.Properties.ReadOnly = True
        Me.FNHSysMatTypeId_None.Size = New System.Drawing.Size(377, 20)
        Me.FNHSysMatTypeId_None.TabIndex = 502
        Me.FNHSysMatTypeId_None.TabStop = False
        Me.FNHSysMatTypeId_None.Tag = "2|"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(158, 454)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(157, 29)
        Me.btnSave.TabIndex = 499
        Me.btnSave.Text = "Save"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(451, 454)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(147, 29)
        Me.btnExit.TabIndex = 311
        Me.btnExit.Text = "Exit"
        '
        'wMatQCSpareAdd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(723, 495)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupControl2)
        Me.Name = "wMatQCSpareAdd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "กำหนดรายการเผื่อการซื้อ Material"
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateActive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcFNSpare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTStateSendApp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTStateAppsdf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTStateApp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFTStateSendApp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNSpareType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNSpareConditionType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysMatTypeId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysMatTypeId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysMatTypeId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysMatTypeId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysMatTypeId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNSpareType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNSpareType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNSpareConditionType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents FNSpareConditionType_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sFNStartQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCalcEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents sFNEndQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSpare As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCalcFNSpare As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepositoryFTStateSendApp As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryFTStateAppsdf As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepositoryFTStateApp As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemFTStateSendApp As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCalcEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FTStateActive As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTRemark_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRemark As DevExpress.XtraEditors.MemoEdit
End Class
