Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wCountryForwarderMasterAdd
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
        Me.ReposFDShipDateTo = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.RepositoryItemFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemrFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ocmdoc = New DevExpress.XtraBars.Docking.DockManager()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.FTRemark_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTRemark = New DevExpress.XtraEditors.MemoEdit()
        Me.FNHSysCountryId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCountryId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCountryId = New DevExpress.XtraEditors.ButtonEdit()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNHSysSuplId_Hide = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysSuplId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemButtonFNHSysSuplId = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.FNHSysSuplId_None = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysVenderPramId_Hide = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysVenderPramId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemButtonFNHSysVenderPramId = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.FNHSysVenderPramId_None = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ReposFDShipDateTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFDShipDateTo.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemrFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCountryId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCountryId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemButtonFNHSysSuplId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemButtonFNHSysVenderPramId, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ReposFDShipDateTo
        '
        Me.ReposFDShipDateTo.AutoHeight = False
        Me.ReposFDShipDateTo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFDShipDateTo.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFDShipDateTo.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.ReposFDShipDateTo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ReposFDShipDateTo.EditFormat.FormatString = "dd/MM/yyyy"
        Me.ReposFDShipDateTo.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ReposFDShipDateTo.Name = "ReposFDShipDateTo"
        '
        'RepositoryItemFTSelect
        '
        Me.RepositoryItemFTSelect.AutoHeight = False
        Me.RepositoryItemFTSelect.Name = "RepositoryItemFTSelect"
        Me.RepositoryItemFTSelect.ValueChecked = "1"
        Me.RepositoryItemFTSelect.ValueUnchecked = "0"
        '
        'RepositoryItemrFTSelect
        '
        Me.RepositoryItemrFTSelect.AutoHeight = False
        Me.RepositoryItemrFTSelect.Name = "RepositoryItemrFTSelect"
        Me.RepositoryItemrFTSelect.ValueChecked = "1"
        Me.RepositoryItemrFTSelect.ValueUnchecked = "0"
        '
        'ocmdoc
        '
        Me.ocmdoc.Form = Me
        Me.ocmdoc.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.FTRemark_lbl)
        Me.GroupControl1.Controls.Add(Me.FTRemark)
        Me.GroupControl1.Controls.Add(Me.FNHSysCountryId_lbl)
        Me.GroupControl1.Controls.Add(Me.FNHSysCountryId_None)
        Me.GroupControl1.Controls.Add(Me.FNHSysCountryId)
        Me.GroupControl1.Controls.Add(Me.ocmdelete)
        Me.GroupControl1.Controls.Add(Me.ocmsave)
        Me.GroupControl1.Controls.Add(Me.ogc)
        Me.GroupControl1.Controls.Add(Me.ocmexit)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(685, 447)
        Me.GroupControl1.TabIndex = 3
        Me.GroupControl1.Text = "DETAIL"
        '
        'FTRemark_lbl
        '
        Me.FTRemark_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTRemark_lbl.Appearance.Options.UseForeColor = True
        Me.FTRemark_lbl.Appearance.Options.UseTextOptions = True
        Me.FTRemark_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTRemark_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTRemark_lbl.Location = New System.Drawing.Point(5, 60)
        Me.FTRemark_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark_lbl.Name = "FTRemark_lbl"
        Me.FTRemark_lbl.Size = New System.Drawing.Size(131, 22)
        Me.FTRemark_lbl.TabIndex = 540
        Me.FTRemark_lbl.Tag = "2|"
        Me.FTRemark_lbl.Text = "Note :"
        '
        'FTRemark
        '
        Me.FTRemark.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTRemark.EditValue = ""
        Me.FTRemark.Location = New System.Drawing.Point(140, 59)
        Me.FTRemark.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.Properties.MaxLength = 500
        Me.FTRemark.Size = New System.Drawing.Size(540, 64)
        Me.FTRemark.TabIndex = 539
        Me.FTRemark.Tag = "2|"
        '
        'FNHSysCountryId_lbl
        '
        Me.FNHSysCountryId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCountryId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCountryId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCountryId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCountryId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCountryId_lbl.Location = New System.Drawing.Point(12, 28)
        Me.FNHSysCountryId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCountryId_lbl.Name = "FNHSysCountryId_lbl"
        Me.FNHSysCountryId_lbl.Size = New System.Drawing.Size(124, 23)
        Me.FNHSysCountryId_lbl.TabIndex = 504
        Me.FNHSysCountryId_lbl.Tag = "2|"
        Me.FNHSysCountryId_lbl.Text = "FNHSysCountryId :"
        '
        'FNHSysCountryId_None
        '
        Me.FNHSysCountryId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCountryId_None.Location = New System.Drawing.Point(316, 29)
        Me.FNHSysCountryId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCountryId_None.Name = "FNHSysCountryId_None"
        Me.FNHSysCountryId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCountryId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCountryId_None.Properties.ReadOnly = True
        Me.FNHSysCountryId_None.Size = New System.Drawing.Size(364, 23)
        Me.FNHSysCountryId_None.TabIndex = 503
        Me.FNHSysCountryId_None.Tag = "2|"
        '
        'FNHSysCountryId
        '
        Me.FNHSysCountryId.Location = New System.Drawing.Point(140, 29)
        Me.FNHSysCountryId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysCountryId.Name = "FNHSysCountryId"
        Me.FNHSysCountryId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "3", Nothing, True)})
        Me.FNHSysCountryId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysCountryId.Properties.Tag = ""
        Me.FNHSysCountryId.Size = New System.Drawing.Size(170, 23)
        Me.FNHSysCountryId.TabIndex = 486
        Me.FNHSysCountryId.Tag = "2|"
        '
        'ocmdelete
        '
        Me.ocmdelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmdelete.Location = New System.Drawing.Point(169, 403)
        Me.ocmdelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(111, 31)
        Me.ocmdelete.TabIndex = 100
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "Delete"
        '
        'ocmsave
        '
        Me.ocmsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmsave.Location = New System.Drawing.Point(22, 403)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(111, 31)
        Me.ocmsave.TabIndex = 100
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'ogc
        '
        Me.ogc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogc.Location = New System.Drawing.Point(-1, 130)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemButtonFNHSysSuplId, Me.RepositoryItemButtonFNHSysVenderPramId})
        Me.ogc.Size = New System.Drawing.Size(681, 257)
        Me.ogc.TabIndex = 0
        Me.ogc.Tag = "2|"
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNHSysSuplId_Hide, Me.FNHSysSuplId, Me.FNHSysSuplId_None, Me.FNHSysVenderPramId_Hide, Me.FNHSysVenderPramId, Me.FNHSysVenderPramId_None})
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'FNHSysSuplId_Hide
        '
        Me.FNHSysSuplId_Hide.Caption = "FNHSysSuplId_Hide"
        Me.FNHSysSuplId_Hide.FieldName = "FNHSysSuplId_Hide"
        Me.FNHSysSuplId_Hide.Name = "FNHSysSuplId_Hide"
        '
        'FNHSysSuplId
        '
        Me.FNHSysSuplId.Caption = "FNHSysSuplId"
        Me.FNHSysSuplId.ColumnEdit = Me.RepositoryItemButtonFNHSysSuplId
        Me.FNHSysSuplId.FieldName = "FNHSysSuplId"
        Me.FNHSysSuplId.Name = "FNHSysSuplId"
        Me.FNHSysSuplId.Visible = True
        Me.FNHSysSuplId.VisibleIndex = 1
        Me.FNHSysSuplId.Width = 178
        '
        'RepositoryItemButtonFNHSysSuplId
        '
        Me.RepositoryItemButtonFNHSysSuplId.AutoHeight = False
        Me.RepositoryItemButtonFNHSysSuplId.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "506", Nothing, True)})
        Me.RepositoryItemButtonFNHSysSuplId.Name = "RepositoryItemButtonFNHSysSuplId"
        Me.RepositoryItemButtonFNHSysSuplId.Tag = ""
        '
        'FNHSysSuplId_None
        '
        Me.FNHSysSuplId_None.Caption = "FNHSysSuplId_None"
        Me.FNHSysSuplId_None.FieldName = "FNHSysSuplId_None"
        Me.FNHSysSuplId_None.Name = "FNHSysSuplId_None"
        Me.FNHSysSuplId_None.OptionsColumn.AllowEdit = False
        Me.FNHSysSuplId_None.Visible = True
        Me.FNHSysSuplId_None.VisibleIndex = 2
        Me.FNHSysSuplId_None.Width = 363
        '
        'FNHSysVenderPramId_Hide
        '
        Me.FNHSysVenderPramId_Hide.Caption = "FNHSysVenderPramId_Hide"
        Me.FNHSysVenderPramId_Hide.FieldName = "FNHSysVenderPramId_Hide"
        Me.FNHSysVenderPramId_Hide.Name = "FNHSysVenderPramId_Hide"
        '
        'FNHSysVenderPramId
        '
        Me.FNHSysVenderPramId.Caption = "FNHSysVenderPramId"
        Me.FNHSysVenderPramId.ColumnEdit = Me.RepositoryItemButtonFNHSysVenderPramId
        Me.FNHSysVenderPramId.FieldName = "FNHSysVenderPramId"
        Me.FNHSysVenderPramId.Name = "FNHSysVenderPramId"
        Me.FNHSysVenderPramId.OptionsColumn.AllowEdit = False
        Me.FNHSysVenderPramId.Visible = True
        Me.FNHSysVenderPramId.VisibleIndex = 0
        Me.FNHSysVenderPramId.Width = 116
        '
        'RepositoryItemButtonFNHSysVenderPramId
        '
        Me.RepositoryItemButtonFNHSysVenderPramId.AutoHeight = False
        Me.RepositoryItemButtonFNHSysVenderPramId.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "168", Nothing, True)})
        Me.RepositoryItemButtonFNHSysVenderPramId.Name = "RepositoryItemButtonFNHSysVenderPramId"
        '
        'FNHSysVenderPramId_None
        '
        Me.FNHSysVenderPramId_None.Caption = "FNHSysVenderPramId_None"
        Me.FNHSysVenderPramId_None.FieldName = "FNHSysVenderPramId_None"
        Me.FNHSysVenderPramId_None.Name = "FNHSysVenderPramId_None"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(541, 403)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'wCountryForwarderMasterAdd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(685, 447)
        Me.Controls.Add(Me.GroupControl1)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wCountryForwarderMasterAdd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Country Frowarder add"
        CType(Me.ReposFDShipDateTo.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFDShipDateTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemrFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.FTRemark.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCountryId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCountryId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemButtonFNHSysSuplId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemButtonFNHSysVenderPramId, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmdoc As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ReposFDShipDateTo As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents RepositoryItemFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemrFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RepositoryItemButtonFNHSysSuplId As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents FNHSysSuplId_Hide As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysSuplId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysSuplId_None As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysCountryId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysCountryId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCountryId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTRemark As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents FTRemark_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysVenderPramId_Hide As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysVenderPramId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysVenderPramId_None As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemButtonFNHSysVenderPramId As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
End Class
