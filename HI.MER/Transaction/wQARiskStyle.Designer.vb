<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wQARiskStyle
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
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbcriteria = New DevExpress.XtraEditors.GroupControl()
        Me.FNHSysProdTypeId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysProdTypeId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysProdTypeId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysBuyId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysBuyId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysBuyId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ogddetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFTStateSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CFNHSysStyleId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNHSysSeasonId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStyleName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTSeasonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTNote = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTNote = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.CFTSelectORG = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTNoteORG = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oRepositoryItemCheckEdit = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmloaddata = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmSave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclearclsr = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmExit = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbcriteria, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbcriteria.SuspendLayout()
        CType(Me.FNHSysProdTypeId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysProdTypeId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysBuyId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysBuyId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ogddetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTStateSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTNote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oRepositoryItemCheckEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbcriteria
        '
        Me.ogbcriteria.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbcriteria.Controls.Add(Me.FNHSysProdTypeId_None)
        Me.ogbcriteria.Controls.Add(Me.FNHSysProdTypeId)
        Me.ogbcriteria.Controls.Add(Me.FNHSysProdTypeId_lbl)
        Me.ogbcriteria.Controls.Add(Me.FNHSysBuyId_None)
        Me.ogbcriteria.Controls.Add(Me.FNHSysBuyId)
        Me.ogbcriteria.Controls.Add(Me.FNHSysBuyId_lbl)
        Me.ogbcriteria.Location = New System.Drawing.Point(10, 4)
        Me.ogbcriteria.Name = "ogbcriteria"
        Me.ogbcriteria.Size = New System.Drawing.Size(1232, 98)
        Me.ogbcriteria.TabIndex = 0
        Me.ogbcriteria.Text = "GroupControl1"
        '
        'FNHSysProdTypeId_None
        '
        Me.FNHSysProdTypeId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysProdTypeId_None.Location = New System.Drawing.Point(372, 29)
        Me.FNHSysProdTypeId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysProdTypeId_None.Name = "FNHSysProdTypeId_None"
        Me.FNHSysProdTypeId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysProdTypeId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysProdTypeId_None.Properties.ReadOnly = True
        Me.FNHSysProdTypeId_None.Size = New System.Drawing.Size(822, 22)
        Me.FNHSysProdTypeId_None.TabIndex = 297
        Me.FNHSysProdTypeId_None.Tag = "2|"
        '
        'FNHSysProdTypeId
        '
        Me.FNHSysProdTypeId.Location = New System.Drawing.Point(219, 29)
        Me.FNHSysProdTypeId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysProdTypeId.Name = "FNHSysProdTypeId"
        Me.FNHSysProdTypeId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "88", Nothing, True)})
        Me.FNHSysProdTypeId.Properties.Tag = ""
        Me.FNHSysProdTypeId.Size = New System.Drawing.Size(152, 22)
        Me.FNHSysProdTypeId.TabIndex = 296
        Me.FNHSysProdTypeId.Tag = "2|"
        '
        'FNHSysProdTypeId_lbl
        '
        Me.FNHSysProdTypeId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysProdTypeId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysProdTypeId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysProdTypeId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysProdTypeId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysProdTypeId_lbl.Location = New System.Drawing.Point(16, 29)
        Me.FNHSysProdTypeId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysProdTypeId_lbl.Name = "FNHSysProdTypeId_lbl"
        Me.FNHSysProdTypeId_lbl.Size = New System.Drawing.Size(196, 25)
        Me.FNHSysProdTypeId_lbl.TabIndex = 298
        Me.FNHSysProdTypeId_lbl.Tag = "2|"
        Me.FNHSysProdTypeId_lbl.Text = "Product Type :"
        '
        'FNHSysBuyId_None
        '
        Me.FNHSysBuyId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysBuyId_None.Location = New System.Drawing.Point(372, 56)
        Me.FNHSysBuyId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysBuyId_None.Name = "FNHSysBuyId_None"
        Me.FNHSysBuyId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysBuyId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysBuyId_None.Properties.ReadOnly = True
        Me.FNHSysBuyId_None.Size = New System.Drawing.Size(822, 22)
        Me.FNHSysBuyId_None.TabIndex = 294
        Me.FNHSysBuyId_None.Tag = "2|"
        '
        'FNHSysBuyId
        '
        Me.FNHSysBuyId.Location = New System.Drawing.Point(218, 57)
        Me.FNHSysBuyId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysBuyId.Name = "FNHSysBuyId"
        Me.FNHSysBuyId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "113", Nothing, True)})
        Me.FNHSysBuyId.Properties.Tag = ""
        Me.FNHSysBuyId.Size = New System.Drawing.Size(152, 22)
        Me.FNHSysBuyId.TabIndex = 293
        Me.FNHSysBuyId.Tag = "2|"
        '
        'FNHSysBuyId_lbl
        '
        Me.FNHSysBuyId_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysBuyId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysBuyId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysBuyId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysBuyId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysBuyId_lbl.Location = New System.Drawing.Point(16, 59)
        Me.FNHSysBuyId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysBuyId_lbl.Name = "FNHSysBuyId_lbl"
        Me.FNHSysBuyId_lbl.Size = New System.Drawing.Size(196, 25)
        Me.FNHSysBuyId_lbl.TabIndex = 295
        Me.FNHSysBuyId_lbl.Tag = "2|"
        Me.FNHSysBuyId_lbl.Text = "Buy :"
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.Controls.Add(Me.ogddetail)
        Me.GroupControl1.Location = New System.Drawing.Point(11, 108)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1232, 596)
        Me.GroupControl1.TabIndex = 1
        Me.GroupControl1.Text = "Detail"
        '
        'ogddetail
        '
        Me.ogddetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogddetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogddetail.Location = New System.Drawing.Point(2, 25)
        Me.ogddetail.MainView = Me.ogvdetail
        Me.ogddetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogddetail.Name = "ogddetail"
        Me.ogddetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.oRepositoryItemCheckEdit, Me.ReposFTStateSelect, Me.RepositoryFTNote})
        Me.ogddetail.Size = New System.Drawing.Size(1228, 569)
        Me.ogddetail.TabIndex = 1
        Me.ogddetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTSelect, Me.CFNHSysStyleId, Me.CFNHSysSeasonId, Me.CFTStyleCode, Me.CFTStyleName, Me.CFTSeasonCode, Me.CFTNote, Me.CFTSelectORG, Me.CFTNoteORG})
        Me.ogvdetail.GridControl = Me.ogddetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'CFTSelect
        '
        Me.CFTSelect.Caption = "Risk Style"
        Me.CFTSelect.ColumnEdit = Me.ReposFTStateSelect
        Me.CFTSelect.FieldName = "FTSelect"
        Me.CFTSelect.Name = "CFTSelect"
        Me.CFTSelect.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSelect.OptionsColumn.AllowMove = False
        Me.CFTSelect.OptionsColumn.AllowShowHide = False
        Me.CFTSelect.OptionsColumn.FixedWidth = True
        Me.CFTSelect.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTSelect.Visible = True
        Me.CFTSelect.VisibleIndex = 0
        Me.CFTSelect.Width = 87
        '
        'ReposFTStateSelect
        '
        Me.ReposFTStateSelect.AutoHeight = False
        Me.ReposFTStateSelect.Caption = "Check"
        Me.ReposFTStateSelect.Name = "ReposFTStateSelect"
        Me.ReposFTStateSelect.ValueChecked = "1"
        Me.ReposFTStateSelect.ValueUnchecked = "0"
        '
        'CFNHSysStyleId
        '
        Me.CFNHSysStyleId.Caption = "FNHSysStyleId"
        Me.CFNHSysStyleId.FieldName = "FNHSysStyleId"
        Me.CFNHSysStyleId.Name = "CFNHSysStyleId"
        Me.CFNHSysStyleId.OptionsColumn.AllowEdit = False
        Me.CFNHSysStyleId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNHSysStyleId.OptionsColumn.AllowMove = False
        Me.CFNHSysStyleId.OptionsColumn.AllowShowHide = False
        Me.CFNHSysStyleId.OptionsColumn.ReadOnly = True
        Me.CFNHSysStyleId.OptionsColumn.ShowInCustomizationForm = False
        '
        'CFNHSysSeasonId
        '
        Me.CFNHSysSeasonId.Caption = "FNHSysSeasonId"
        Me.CFNHSysSeasonId.FieldName = "FNHSysSeasonId"
        Me.CFNHSysSeasonId.Name = "CFNHSysSeasonId"
        Me.CFNHSysSeasonId.OptionsColumn.AllowEdit = False
        Me.CFNHSysSeasonId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNHSysSeasonId.OptionsColumn.AllowMove = False
        Me.CFNHSysSeasonId.OptionsColumn.AllowShowHide = False
        Me.CFNHSysSeasonId.OptionsColumn.ReadOnly = True
        Me.CFNHSysSeasonId.OptionsColumn.ShowInCustomizationForm = False
        '
        'CFTStyleCode
        '
        Me.CFTStyleCode.Caption = "Style Code"
        Me.CFTStyleCode.FieldName = "FTStyleCode"
        Me.CFTStyleCode.Name = "CFTStyleCode"
        Me.CFTStyleCode.OptionsColumn.AllowEdit = False
        Me.CFTStyleCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStyleCode.OptionsColumn.AllowMove = False
        Me.CFTStyleCode.OptionsColumn.AllowShowHide = False
        Me.CFTStyleCode.OptionsColumn.ReadOnly = True
        Me.CFTStyleCode.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTStyleCode.Visible = True
        Me.CFTStyleCode.VisibleIndex = 1
        Me.CFTStyleCode.Width = 161
        '
        'CFTStyleName
        '
        Me.CFTStyleName.Caption = "Style Name"
        Me.CFTStyleName.FieldName = "FTStyleName"
        Me.CFTStyleName.Name = "CFTStyleName"
        Me.CFTStyleName.OptionsColumn.AllowEdit = False
        Me.CFTStyleName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStyleName.OptionsColumn.AllowMove = False
        Me.CFTStyleName.OptionsColumn.AllowShowHide = False
        Me.CFTStyleName.OptionsColumn.ReadOnly = True
        Me.CFTStyleName.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTStyleName.Visible = True
        Me.CFTStyleName.VisibleIndex = 2
        Me.CFTStyleName.Width = 312
        '
        'CFTSeasonCode
        '
        Me.CFTSeasonCode.Caption = "Season"
        Me.CFTSeasonCode.FieldName = "FTSeasonCode"
        Me.CFTSeasonCode.Name = "CFTSeasonCode"
        Me.CFTSeasonCode.OptionsColumn.AllowEdit = False
        Me.CFTSeasonCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSeasonCode.OptionsColumn.AllowMove = False
        Me.CFTSeasonCode.OptionsColumn.AllowShowHide = False
        Me.CFTSeasonCode.OptionsColumn.ReadOnly = True
        Me.CFTSeasonCode.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTSeasonCode.Visible = True
        Me.CFTSeasonCode.VisibleIndex = 3
        Me.CFTSeasonCode.Width = 110
        '
        'CFTNote
        '
        Me.CFTNote.Caption = "Note"
        Me.CFTNote.ColumnEdit = Me.RepositoryFTNote
        Me.CFTNote.FieldName = "FTNote"
        Me.CFTNote.Name = "CFTNote"
        Me.CFTNote.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTNote.OptionsColumn.AllowMove = False
        Me.CFTNote.OptionsColumn.AllowShowHide = False
        Me.CFTNote.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTNote.Visible = True
        Me.CFTNote.VisibleIndex = 4
        Me.CFTNote.Width = 500
        '
        'RepositoryFTNote
        '
        Me.RepositoryFTNote.AutoHeight = False
        Me.RepositoryFTNote.MaxLength = 500
        Me.RepositoryFTNote.Name = "RepositoryFTNote"
        '
        'CFTSelectORG
        '
        Me.CFTSelectORG.Caption = "FTSelectORG"
        Me.CFTSelectORG.FieldName = "FTSelectOrg"
        Me.CFTSelectORG.Name = "CFTSelectORG"
        '
        'CFTNoteORG
        '
        Me.CFTNoteORG.Caption = "FTNoteORG"
        Me.CFTNoteORG.FieldName = "FTNoteORG"
        Me.CFTNoteORG.Name = "CFTNoteORG"
        '
        'oRepositoryItemCheckEdit
        '
        Me.oRepositoryItemCheckEdit.AutoHeight = False
        Me.oRepositoryItemCheckEdit.Caption = "Check"
        Me.oRepositoryItemCheckEdit.Name = "oRepositoryItemCheckEdit"
        Me.oRepositoryItemCheckEdit.ValueChecked = "1"
        Me.oRepositoryItemCheckEdit.ValueUnchecked = "0"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmloaddata)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmSave)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclearclsr)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmExit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(341, 424)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(804, 53)
        Me.ogbmainprocbutton.TabIndex = 464
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmloaddata
        '
        Me.ocmloaddata.Location = New System.Drawing.Point(20, 10)
        Me.ocmloaddata.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmloaddata.Name = "ocmloaddata"
        Me.ocmloaddata.Size = New System.Drawing.Size(126, 31)
        Me.ocmloaddata.TabIndex = 101
        Me.ocmloaddata.TabStop = False
        Me.ocmloaddata.Tag = "2|"
        Me.ocmloaddata.Text = "LoadData"
        '
        'ocmSave
        '
        Me.ocmSave.Location = New System.Drawing.Point(163, 10)
        Me.ocmSave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmSave.Name = "ocmSave"
        Me.ocmSave.Size = New System.Drawing.Size(105, 31)
        Me.ocmSave.TabIndex = 99
        Me.ocmSave.TabStop = False
        Me.ocmSave.Tag = "2|"
        Me.ocmSave.Text = "SAVE"
        '
        'ocmclearclsr
        '
        Me.ocmclearclsr.Location = New System.Drawing.Point(580, 10)
        Me.ocmclearclsr.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclearclsr.Name = "ocmclearclsr"
        Me.ocmclearclsr.Size = New System.Drawing.Size(100, 31)
        Me.ocmclearclsr.TabIndex = 97
        Me.ocmclearclsr.TabStop = False
        Me.ocmclearclsr.Tag = "2|"
        Me.ocmclearclsr.Text = "CLEAR"
        '
        'ocmExit
        '
        Me.ocmExit.Location = New System.Drawing.Point(687, 10)
        Me.ocmExit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmExit.Name = "ocmExit"
        Me.ocmExit.Size = New System.Drawing.Size(98, 31)
        Me.ocmExit.TabIndex = 96
        Me.ocmExit.TabStop = False
        Me.ocmExit.Tag = "2|"
        Me.ocmExit.Text = "EXIT"
        '
        'wQARiskStyle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1256, 716)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.ogbcriteria)
        Me.Name = "wQARiskStyle"
        Me.Text = "wQARiskStyle"
        CType(Me.ogbcriteria, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbcriteria.ResumeLayout(False)
        CType(Me.FNHSysProdTypeId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysProdTypeId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysBuyId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysBuyId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ogddetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTStateSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTNote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oRepositoryItemCheckEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ogbcriteria As DevExpress.XtraEditors.GroupControl
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogddetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ReposFTStateSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents oRepositoryItemCheckEdit As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmloaddata As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclearclsr As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysProdTypeId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysProdTypeId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysProdTypeId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysBuyId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysBuyId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysBuyId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNHSysStyleId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNHSysSeasonId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStyleName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTSeasonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTNote As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTSelectORG As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTNote As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents CFTNoteORG As DevExpress.XtraGrid.Columns.GridColumn
End Class
