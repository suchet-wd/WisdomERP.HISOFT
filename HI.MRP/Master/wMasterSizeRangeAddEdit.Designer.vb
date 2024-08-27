<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wMasterSizeRangeAddEdit
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
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.sbCustomization = New DevExpress.XtraEditors.SimpleButton()
        Me.ogbbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmdeletelayout = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmedit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmaddnew = New DevExpress.XtraEditors.SimpleButton()
        Me.olymain = New DevExpress.XtraLayout.LayoutControl()
        Me.lyg = New DevExpress.XtraLayout.LayoutControlGroup()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.ogcSize = New DevExpress.XtraGrid.GridControl()
        Me.ogvSize = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTGroupRangeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTGroupRangeNameEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTGroupRangeNameTH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateActive = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.rpState = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FNHSysMatSizeId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.RepositoryFNHSysRawmatId = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CXFTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CSFTDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CSFTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CSFTRawMatColorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUsed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbbutton.SuspendLayout()
        CType(Me.olymain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lyg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.ogcSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rpState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.RepositoryFNHSysRawmatId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmsavelayout.Location = New System.Drawing.Point(566, 4)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(46, 38)
        Me.ocmsavelayout.TabIndex = 1
        Me.ocmsavelayout.TabStop = False
        Me.ocmsavelayout.Text = "Save" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Layout"
        '
        'sbCustomization
        '
        Me.sbCustomization.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.sbCustomization.Location = New System.Drawing.Point(511, 4)
        Me.sbCustomization.Name = "sbCustomization"
        Me.sbCustomization.Size = New System.Drawing.Size(53, 38)
        Me.sbCustomization.TabIndex = 0
        Me.sbCustomization.TabStop = False
        Me.sbCustomization.Text = "Custom" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'ogbbutton
        '
        Me.ogbbutton.Controls.Add(Me.ocmdeletelayout)
        Me.ogbbutton.Controls.Add(Me.ocmsavelayout)
        Me.ogbbutton.Controls.Add(Me.ocmedit)
        Me.ogbbutton.Controls.Add(Me.sbCustomization)
        Me.ogbbutton.Controls.Add(Me.ocmexit)
        Me.ogbbutton.Controls.Add(Me.ocmclear)
        Me.ogbbutton.Controls.Add(Me.ocmdelete)
        Me.ogbbutton.Controls.Add(Me.ocmaddnew)
        Me.ogbbutton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ogbbutton.Location = New System.Drawing.Point(0, 590)
        Me.ogbbutton.Name = "ogbbutton"
        Me.ogbbutton.Size = New System.Drawing.Size(770, 39)
        Me.ogbbutton.TabIndex = 4
        '
        'ocmdeletelayout
        '
        Me.ocmdeletelayout.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmdeletelayout.Location = New System.Drawing.Point(615, 4)
        Me.ocmdeletelayout.Name = "ocmdeletelayout"
        Me.ocmdeletelayout.Size = New System.Drawing.Size(46, 38)
        Me.ocmdeletelayout.TabIndex = 102
        Me.ocmdeletelayout.TabStop = False
        Me.ocmdeletelayout.Text = "Delete" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Layout"
        '
        'ocmedit
        '
        Me.ocmedit.Location = New System.Drawing.Point(9, 10)
        Me.ocmedit.Name = "ocmedit"
        Me.ocmedit.Size = New System.Drawing.Size(95, 25)
        Me.ocmedit.TabIndex = 1001
        Me.ocmedit.Tag = "2|"
        Me.ocmedit.Text = "EDIT"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(663, 10)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 100
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(107, 10)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(95, 25)
        Me.ocmclear.TabIndex = 1002
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmdelete
        '
        Me.ocmdelete.Location = New System.Drawing.Point(205, 10)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(95, 25)
        Me.ocmdelete.TabIndex = 1003
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "DELETE"
        Me.ocmdelete.Visible = False
        '
        'ocmaddnew
        '
        Me.ocmaddnew.Location = New System.Drawing.Point(9, 10)
        Me.ocmaddnew.Name = "ocmaddnew"
        Me.ocmaddnew.Size = New System.Drawing.Size(95, 25)
        Me.ocmaddnew.TabIndex = 1000
        Me.ocmaddnew.Tag = "2|"
        Me.ocmaddnew.Text = "NEW"
        '
        'olymain
        '
        Me.olymain.Location = New System.Drawing.Point(63, 33)
        Me.olymain.Name = "olymain"
        Me.olymain.Root = Me.lyg
        Me.olymain.Size = New System.Drawing.Size(472, 280)
        Me.olymain.TabIndex = 6
        Me.olymain.Text = "LayoutControl1"
        '
        'lyg
        '
        Me.lyg.CustomizationFormText = "LayoutControlGroup1"
        Me.lyg.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
        Me.lyg.GroupBordersVisible = False
        Me.lyg.Name = "lyg"
        Me.lyg.Size = New System.Drawing.Size(472, 280)
        Me.lyg.TextVisible = False
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.ogcSize)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 401)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(770, 189)
        Me.PanelControl1.TabIndex = 7
        '
        'ogcSize
        '
        Me.ogcSize.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcSize.EmbeddedNavigator.Buttons.CancelEdit.Visible = False
        Me.ogcSize.EmbeddedNavigator.Buttons.Edit.Visible = False
        Me.ogcSize.EmbeddedNavigator.Buttons.EndEdit.Visible = False
        Me.ogcSize.EmbeddedNavigator.Buttons.First.Visible = False
        Me.ogcSize.EmbeddedNavigator.Buttons.Last.Visible = False
        Me.ogcSize.EmbeddedNavigator.Buttons.Next.Visible = False
        Me.ogcSize.EmbeddedNavigator.Buttons.NextPage.Visible = False
        Me.ogcSize.EmbeddedNavigator.Buttons.Prev.Visible = False
        Me.ogcSize.EmbeddedNavigator.Buttons.PrevPage.Visible = False
        Me.ogcSize.EmbeddedNavigator.TextLocation = DevExpress.XtraEditors.NavigatorButtonsTextLocation.None
        Me.ogcSize.EmbeddedNavigator.TextStringFormat = ""
        Me.ogcSize.Location = New System.Drawing.Point(2, 2)
        Me.ogcSize.MainView = Me.ogvSize
        Me.ogcSize.Name = "ogcSize"
        Me.ogcSize.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.rpState, Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2})
        Me.ogcSize.Size = New System.Drawing.Size(766, 185)
        Me.ogcSize.TabIndex = 529
        Me.ogcSize.Tag = "3|"
        Me.ogcSize.UseEmbeddedNavigator = True
        Me.ogcSize.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvSize})
        '
        'ogvSize
        '
        Me.ogvSize.Appearance.EvenRow.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ogvSize.Appearance.EvenRow.Options.UseBackColor = True
        Me.ogvSize.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.FTGroupRangeCode, Me.FTGroupRangeNameEN, Me.FTGroupRangeNameTH, Me.FTStateActive, Me.FTUsed, Me.FNHSysMatSizeId})
        Me.ogvSize.DetailHeight = 406
        Me.ogvSize.GridControl = Me.ogcSize
        Me.ogvSize.Name = "ogvSize"
        Me.ogvSize.OptionsView.ColumnAutoWidth = False
        Me.ogvSize.OptionsView.ShowGroupPanel = False
        Me.ogvSize.Tag = "3|"
        '
        'FTSelect
        '
        Me.FTSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSelect.Caption = "FTSelect"
        Me.FTSelect.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.MinWidth = 21
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[True]
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 81
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'FTGroupRangeCode
        '
        Me.FTGroupRangeCode.AppearanceHeader.ForeColor = System.Drawing.Color.Blue
        Me.FTGroupRangeCode.AppearanceHeader.Options.UseForeColor = True
        Me.FTGroupRangeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTGroupRangeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTGroupRangeCode.Caption = "FTGroupRangeCode"
        Me.FTGroupRangeCode.FieldName = "FTMatSizeCode"
        Me.FTGroupRangeCode.Name = "FTGroupRangeCode"
        Me.FTGroupRangeCode.OptionsColumn.AllowEdit = False
        Me.FTGroupRangeCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTGroupRangeCode.OptionsColumn.AllowMove = False
        Me.FTGroupRangeCode.OptionsColumn.AllowShowHide = False
        Me.FTGroupRangeCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTGroupRangeCode.OptionsColumn.ReadOnly = True
        Me.FTGroupRangeCode.OptionsColumn.ShowInCustomizationForm = False
        Me.FTGroupRangeCode.Visible = True
        Me.FTGroupRangeCode.VisibleIndex = 1
        Me.FTGroupRangeCode.Width = 116
        '
        'FTGroupRangeNameEN
        '
        Me.FTGroupRangeNameEN.AppearanceHeader.ForeColor = System.Drawing.Color.Blue
        Me.FTGroupRangeNameEN.AppearanceHeader.Options.UseForeColor = True
        Me.FTGroupRangeNameEN.AppearanceHeader.Options.UseTextOptions = True
        Me.FTGroupRangeNameEN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTGroupRangeNameEN.Caption = "FTGroupRangeNameEN"
        Me.FTGroupRangeNameEN.FieldName = "FTMatSizeNameEN"
        Me.FTGroupRangeNameEN.Name = "FTGroupRangeNameEN"
        Me.FTGroupRangeNameEN.OptionsColumn.AllowEdit = False
        Me.FTGroupRangeNameEN.OptionsColumn.ReadOnly = True
        Me.FTGroupRangeNameEN.Visible = True
        Me.FTGroupRangeNameEN.VisibleIndex = 2
        Me.FTGroupRangeNameEN.Width = 184
        '
        'FTGroupRangeNameTH
        '
        Me.FTGroupRangeNameTH.AppearanceHeader.ForeColor = System.Drawing.Color.Blue
        Me.FTGroupRangeNameTH.AppearanceHeader.Options.UseForeColor = True
        Me.FTGroupRangeNameTH.AppearanceHeader.Options.UseTextOptions = True
        Me.FTGroupRangeNameTH.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTGroupRangeNameTH.Caption = "FTGroupRangeNameTH"
        Me.FTGroupRangeNameTH.FieldName = "FTMatSizeNameTH"
        Me.FTGroupRangeNameTH.MinWidth = 21
        Me.FTGroupRangeNameTH.Name = "FTGroupRangeNameTH"
        Me.FTGroupRangeNameTH.OptionsColumn.AllowEdit = False
        Me.FTGroupRangeNameTH.OptionsColumn.ReadOnly = True
        Me.FTGroupRangeNameTH.Visible = True
        Me.FTGroupRangeNameTH.VisibleIndex = 3
        Me.FTGroupRangeNameTH.Width = 184
        '
        'FTStateActive
        '
        Me.FTStateActive.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStateActive.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStateActive.Caption = "FTStateActive"
        Me.FTStateActive.ColumnEdit = Me.rpState
        Me.FTStateActive.FieldName = "FTStateActive"
        Me.FTStateActive.MinWidth = 21
        Me.FTStateActive.Name = "FTStateActive"
        Me.FTStateActive.OptionsColumn.AllowEdit = False
        Me.FTStateActive.OptionsColumn.ReadOnly = True
        Me.FTStateActive.Visible = True
        Me.FTStateActive.VisibleIndex = 4
        Me.FTStateActive.Width = 51
        '
        'rpState
        '
        Me.rpState.AutoHeight = False
        Me.rpState.Name = "rpState"
        Me.rpState.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.rpState.ValueChecked = "1"
        Me.rpState.ValueUnchecked = "0"
        '
        'FNHSysMatSizeId
        '
        Me.FNHSysMatSizeId.Caption = "FNHSysMatSizeId"
        Me.FNHSysMatSizeId.FieldName = "FNHSysMatSizeId"
        Me.FNHSysMatSizeId.MinWidth = 21
        Me.FNHSysMatSizeId.Name = "FNHSysMatSizeId"
        Me.FNHSysMatSizeId.Width = 81
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.olymain)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(770, 401)
        Me.PanelControl2.TabIndex = 8
        '
        'RepositoryFNHSysRawmatId
        '
        Me.RepositoryFNHSysRawmatId.AllowNullInput = DevExpress.Utils.DefaultBoolean.[True]
        Me.RepositoryFNHSysRawmatId.AutoHeight = False
        Me.RepositoryFNHSysRawmatId.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryFNHSysRawmatId.DisplayMember = "FTRawMatCode"
        Me.RepositoryFNHSysRawmatId.Name = "RepositoryFNHSysRawmatId"
        Me.RepositoryFNHSysRawmatId.NullText = ""
        Me.RepositoryFNHSysRawmatId.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
        Me.RepositoryFNHSysRawmatId.PopupFormSize = New System.Drawing.Size(650, 0)
        Me.RepositoryFNHSysRawmatId.PopupView = Me.RepositoryItemGridLookUpEdit1View
        Me.RepositoryFNHSysRawmatId.ValueMember = "FTItemDataRef"
        '
        'RepositoryItemGridLookUpEdit1View
        '
        Me.RepositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemGridLookUpEdit1View.Name = "RepositoryItemGridLookUpEdit1View"
        Me.RepositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ColumnAutoWidth = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'CXFTRawMatCode
        '
        Me.CXFTRawMatCode.Caption = "RawMat Code"
        Me.CXFTRawMatCode.FieldName = "FTRawMatCode"
        Me.CXFTRawMatCode.Name = "CXFTRawMatCode"
        Me.CXFTRawMatCode.OptionsColumn.AllowEdit = False
        Me.CXFTRawMatCode.OptionsColumn.ReadOnly = True
        Me.CXFTRawMatCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.CXFTRawMatCode.Visible = True
        Me.CXFTRawMatCode.VisibleIndex = 0
        Me.CXFTRawMatCode.Width = 120
        '
        'CSFTDescription
        '
        Me.CSFTDescription.Caption = "Description"
        Me.CSFTDescription.FieldName = "FTDescription"
        Me.CSFTDescription.Name = "CSFTDescription"
        Me.CSFTDescription.OptionsColumn.AllowEdit = False
        Me.CSFTDescription.OptionsColumn.ReadOnly = True
        Me.CSFTDescription.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.CSFTDescription.Visible = True
        Me.CSFTDescription.VisibleIndex = 1
        Me.CSFTDescription.Width = 250
        '
        'CSFTRawMatColorCode
        '
        Me.CSFTRawMatColorCode.Caption = "Color Code"
        Me.CSFTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.CSFTRawMatColorCode.Name = "CSFTRawMatColorCode"
        Me.CSFTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.CSFTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.CSFTRawMatColorCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.CSFTRawMatColorCode.Visible = True
        Me.CSFTRawMatColorCode.VisibleIndex = 2
        Me.CSFTRawMatColorCode.Width = 100
        '
        'CSFTRawMatColorName
        '
        Me.CSFTRawMatColorName.Caption = "FTRawMatColorName"
        Me.CSFTRawMatColorName.FieldName = "FTRawMatColorName"
        Me.CSFTRawMatColorName.Name = "CSFTRawMatColorName"
        Me.CSFTRawMatColorName.OptionsColumn.AllowEdit = False
        Me.CSFTRawMatColorName.OptionsColumn.ReadOnly = True
        Me.CSFTRawMatColorName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        '
        'FTUsed
        '
        Me.FTUsed.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUsed.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUsed.Caption = "FTUsed"
        Me.FTUsed.ColumnEdit = Me.RepositoryItemCheckEdit2
        Me.FTUsed.FieldName = "FTUsed"
        Me.FTUsed.Name = "FTUsed"
        Me.FTUsed.OptionsColumn.AllowEdit = False
        Me.FTUsed.OptionsColumn.ReadOnly = True
        Me.FTUsed.Visible = True
        Me.FTUsed.VisibleIndex = 5
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        Me.RepositoryItemCheckEdit2.ValueChecked = "1"
        Me.RepositoryItemCheckEdit2.ValueUnchecked = "0"
        '
        'wMasterSizeRangeAddEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(770, 629)
        Me.ControlBox = False
        Me.Controls.Add(Me.PanelControl2)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.ogbbutton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wMasterSizeRangeAddEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Size Range Master [Add / Edit]"
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbbutton.ResumeLayout(False)
        CType(Me.olymain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lyg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.ogcSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvSize, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rpState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.RepositoryFNHSysRawmatId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents sbCustomization As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogbbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmaddnew As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents olymain As DevExpress.XtraLayout.LayoutControl
    Friend WithEvents lyg As DevExpress.XtraLayout.LayoutControlGroup
    Friend WithEvents ocmedit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdeletelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents PanelControl2 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ogcSize As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvSize As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTGroupRangeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTGroupRangeNameEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFNHSysRawmatId As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CXFTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CSFTDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CSFTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CSFTRawMatColorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTGroupRangeNameTH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateActive As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents rpState As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FNHSysMatSizeId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUsed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class
