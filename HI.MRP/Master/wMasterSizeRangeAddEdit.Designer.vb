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
        Me.FNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSize = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.PanelControl2 = New DevExpress.XtraEditors.PanelControl()
        Me.RepositoryFNHSysRawmatId = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CXFTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CSFTDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CSFTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CSFTRawMatColorName = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbbutton.SuspendLayout()
        CType(Me.olymain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lyg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.ogcSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvSize, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl2.SuspendLayout()
        CType(Me.RepositoryFNHSysRawmatId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmsavelayout.Location = New System.Drawing.Point(552, 4)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(46, 38)
        Me.ocmsavelayout.TabIndex = 1
        Me.ocmsavelayout.TabStop = False
        Me.ocmsavelayout.Text = "Save" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Layout"
        '
        'sbCustomization
        '
        Me.sbCustomization.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.sbCustomization.Location = New System.Drawing.Point(497, 4)
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
        Me.ogbbutton.Location = New System.Drawing.Point(0, 527)
        Me.ogbbutton.Name = "ogbbutton"
        Me.ogbbutton.Size = New System.Drawing.Size(756, 43)
        Me.ogbbutton.TabIndex = 4
        '
        'ocmdeletelayout
        '
        Me.ocmdeletelayout.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmdeletelayout.Location = New System.Drawing.Point(601, 4)
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
        Me.ocmexit.Location = New System.Drawing.Point(649, 10)
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
        Me.PanelControl1.Location = New System.Drawing.Point(0, 338)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(756, 189)
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
        Me.ogcSize.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1, Me.RepositoryItemTextEdit1})
        Me.ogcSize.Size = New System.Drawing.Size(752, 185)
        Me.ogcSize.TabIndex = 529
        Me.ogcSize.Tag = "3|"
        Me.ogcSize.UseEmbeddedNavigator = True
        Me.ogcSize.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvSize})
        '
        'ogvSize
        '
        Me.ogvSize.Appearance.EvenRow.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ogvSize.Appearance.EvenRow.Options.UseBackColor = True
        Me.ogvSize.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.FNSeq, Me.FTSize})
        Me.ogvSize.GridControl = Me.ogcSize
        Me.ogvSize.Name = "ogvSize"
        Me.ogvSize.OptionsView.ColumnAutoWidth = False
        Me.ogvSize.OptionsView.ShowGroupPanel = False
        Me.ogvSize.Tag = "3|"
        '
        'FTSelect
        '
        Me.FTSelect.Caption = "Select"
        Me.FTSelect.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 50
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        '
        'FNSeq
        '
        Me.FNSeq.Caption = "ลำดับที่"
        Me.FNSeq.FieldName = "FNSeq"
        Me.FNSeq.Name = "FNSeq"
        Me.FNSeq.OptionsColumn.AllowEdit = False
        Me.FNSeq.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSeq.OptionsColumn.AllowMove = False
        Me.FNSeq.OptionsColumn.AllowShowHide = False
        Me.FNSeq.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNSeq.OptionsColumn.ReadOnly = True
        Me.FNSeq.OptionsColumn.ShowInCustomizationForm = False
        Me.FNSeq.Visible = True
        Me.FNSeq.VisibleIndex = 1
        Me.FNSeq.Width = 106
        '
        'FTSize
        '
        Me.FTSize.AppearanceHeader.ForeColor = System.Drawing.Color.Blue
        Me.FTSize.AppearanceHeader.Options.UseForeColor = True
        Me.FTSize.Caption = "Size"
        Me.FTSize.ColumnEdit = Me.RepositoryItemTextEdit1
        Me.FTSize.FieldName = "FTSize"
        Me.FTSize.Name = "FTSize"
        Me.FTSize.Visible = True
        Me.FTSize.VisibleIndex = 2
        Me.FTSize.Width = 561
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'PanelControl2
        '
        Me.PanelControl2.Controls.Add(Me.olymain)
        Me.PanelControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl2.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl2.Name = "PanelControl2"
        Me.PanelControl2.Size = New System.Drawing.Size(756, 338)
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
        'wMasterSizeRangeAddEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(756, 570)
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
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl2.ResumeLayout(False)
        CType(Me.RepositoryFNHSysRawmatId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSize As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFNHSysRawmatId As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CXFTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CSFTDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CSFTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CSFTRawMatColorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
End Class
