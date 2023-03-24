<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wSMPCreatePriceMultiple
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
        Me.ogboperation = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmremove = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmadd = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmrefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcoperation = New DevExpress.XtraGrid.GridControl()
        Me.ogvoperation = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNEmpTeam = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFNEmpTeam = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.CFNStartQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.CFNEndQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNMultiple = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFNMultiple = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ReposFNHSysOperationId = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.ReposFNMockUpType = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.ReposFNHSysMarkId = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.ReposFNHSysOperationIdTo = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        CType(Me.ogboperation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogboperation.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogcoperation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvoperation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFNEmpTeam, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFNMultiple, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNHSysOperationId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNMockUpType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNHSysMarkId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNHSysOperationIdTo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogboperation
        '
        Me.ogboperation.Controls.Add(Me.ogbmainprocbutton)
        Me.ogboperation.Controls.Add(Me.ogcoperation)
        Me.ogboperation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogboperation.Location = New System.Drawing.Point(0, 0)
        Me.ogboperation.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogboperation.Name = "ogboperation"
        Me.ogboperation.Size = New System.Drawing.Size(1304, 793)
        Me.ogboperation.TabIndex = 3
        Me.ogboperation.Text = "Operation By Style"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmremove)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmadd)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmrefresh)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(93, 326)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(1120, 112)
        Me.ogbmainprocbutton.TabIndex = 302
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmremove
        '
        Me.ocmremove.Location = New System.Drawing.Point(641, 62)
        Me.ocmremove.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmremove.Name = "ocmremove"
        Me.ocmremove.Size = New System.Drawing.Size(111, 31)
        Me.ocmremove.TabIndex = 114
        Me.ocmremove.TabStop = False
        Me.ocmremove.Tag = "2|"
        Me.ocmremove.Text = "Refresh"
        '
        'ocmadd
        '
        Me.ocmadd.Location = New System.Drawing.Point(484, 53)
        Me.ocmadd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(111, 31)
        Me.ocmadd.TabIndex = 113
        Me.ocmadd.TabStop = False
        Me.ocmadd.Tag = "2|"
        Me.ocmadd.Text = "Refresh"
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
        Me.ogcoperation.Location = New System.Drawing.Point(2, 27)
        Me.ogcoperation.MainView = Me.ogvoperation
        Me.ogcoperation.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcoperation.Name = "ogcoperation"
        Me.ogcoperation.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFNHSysOperationId, Me.ReposFNMockUpType, Me.ReposFNHSysMarkId, Me.ReposFNHSysOperationIdTo, Me.RepQty, Me.RepFNMultiple, Me.RepFNEmpTeam})
        Me.ogcoperation.Size = New System.Drawing.Size(1300, 764)
        Me.ogcoperation.TabIndex = 301
        Me.ogcoperation.TabStop = False
        Me.ogcoperation.Tag = "2|"
        Me.ogcoperation.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvoperation})
        '
        'ogvoperation
        '
        Me.ogvoperation.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNSeq, Me.CFNEmpTeam, Me.CFNStartQty, Me.CFNEndQty, Me.CFNMultiple})
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
        Me.FNSeq.Width = 62
        '
        'CFNEmpTeam
        '
        Me.CFNEmpTeam.Caption = "จำนวนพนักงานในทีม"
        Me.CFNEmpTeam.ColumnEdit = Me.RepFNEmpTeam
        Me.CFNEmpTeam.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNEmpTeam.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNEmpTeam.FieldName = "FNEmpTeam"
        Me.CFNEmpTeam.Name = "CFNEmpTeam"
        Me.CFNEmpTeam.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNEmpTeam.OptionsColumn.AllowMove = False
        Me.CFNEmpTeam.OptionsColumn.AllowShowHide = False
        Me.CFNEmpTeam.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNEmpTeam.Visible = True
        Me.CFNEmpTeam.VisibleIndex = 0
        Me.CFNEmpTeam.Width = 153
        '
        'RepFNEmpTeam
        '
        Me.RepFNEmpTeam.AutoHeight = False
        Me.RepFNEmpTeam.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepFNEmpTeam.DisplayFormat.FormatString = "{0:n0}"
        Me.RepFNEmpTeam.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepFNEmpTeam.EditFormat.FormatString = "{0:n0}"
        Me.RepFNEmpTeam.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepFNEmpTeam.Name = "RepFNEmpTeam"
        '
        'CFNStartQty
        '
        Me.CFNStartQty.Caption = "FNStartQty"
        Me.CFNStartQty.ColumnEdit = Me.RepQty
        Me.CFNStartQty.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNStartQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNStartQty.FieldName = "FNStartQty"
        Me.CFNStartQty.Name = "CFNStartQty"
        Me.CFNStartQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNStartQty.OptionsColumn.AllowMove = False
        Me.CFNStartQty.OptionsColumn.AllowShowHide = False
        Me.CFNStartQty.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNStartQty.Visible = True
        Me.CFNStartQty.VisibleIndex = 1
        Me.CFNStartQty.Width = 134
        '
        'RepQty
        '
        Me.RepQty.AutoHeight = False
        Me.RepQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepQty.DisplayFormat.FormatString = "{0:n0}"
        Me.RepQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepQty.EditFormat.FormatString = "{0:n0}"
        Me.RepQty.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepQty.Name = "RepQty"
        Me.RepQty.Precision = 4
        '
        'CFNEndQty
        '
        Me.CFNEndQty.Caption = "FNEndQty"
        Me.CFNEndQty.ColumnEdit = Me.RepQty
        Me.CFNEndQty.DisplayFormat.FormatString = "{0:n0}"
        Me.CFNEndQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNEndQty.FieldName = "FNEndQty"
        Me.CFNEndQty.Name = "CFNEndQty"
        Me.CFNEndQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFNEndQty.OptionsColumn.AllowMove = False
        Me.CFNEndQty.OptionsColumn.AllowShowHide = False
        Me.CFNEndQty.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNEndQty.Visible = True
        Me.CFNEndQty.VisibleIndex = 2
        Me.CFNEndQty.Width = 124
        '
        'CFNMultiple
        '
        Me.CFNMultiple.Caption = "FNMultiple"
        Me.CFNMultiple.ColumnEdit = Me.RepFNMultiple
        Me.CFNMultiple.DisplayFormat.FormatString = "{0:n2}"
        Me.CFNMultiple.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNMultiple.FieldName = "FNMultiple"
        Me.CFNMultiple.Name = "CFNMultiple"
        Me.CFNMultiple.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNMultiple.OptionsColumn.AllowMove = False
        Me.CFNMultiple.OptionsColumn.AllowShowHide = False
        Me.CFNMultiple.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNMultiple.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNMultiple.Visible = True
        Me.CFNMultiple.VisibleIndex = 3
        Me.CFNMultiple.Width = 136
        '
        'RepFNMultiple
        '
        Me.RepFNMultiple.AutoHeight = False
        Me.RepFNMultiple.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepFNMultiple.DisplayFormat.FormatString = "{0:n2}"
        Me.RepFNMultiple.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepFNMultiple.EditFormat.FormatString = "{0:n2}"
        Me.RepFNMultiple.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepFNMultiple.Name = "RepFNMultiple"
        '
        'ReposFNHSysOperationId
        '
        Me.ReposFNHSysOperationId.AutoHeight = False
        Me.ReposFNHSysOperationId.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "155", Nothing, True)})
        Me.ReposFNHSysOperationId.Name = "ReposFNHSysOperationId"
        '
        'ReposFNMockUpType
        '
        Me.ReposFNMockUpType.AutoHeight = False
        Me.ReposFNMockUpType.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNMockUpType.Name = "ReposFNMockUpType"
        Me.ReposFNMockUpType.Tag = "FNMockUpType"
        Me.ReposFNMockUpType.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'ReposFNHSysMarkId
        '
        Me.ReposFNHSysMarkId.AutoHeight = False
        Me.ReposFNHSysMarkId.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "158", Nothing, True)})
        Me.ReposFNHSysMarkId.Name = "ReposFNHSysMarkId"
        '
        'ReposFNHSysOperationIdTo
        '
        Me.ReposFNHSysOperationIdTo.AutoHeight = False
        Me.ReposFNHSysOperationIdTo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "156", Nothing, True)})
        Me.ReposFNHSysOperationIdTo.Name = "ReposFNHSysOperationIdTo"
        '
        'wSMPCreatePriceMultiple
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1304, 793)
        Me.Controls.Add(Me.ogboperation)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wSMPCreatePriceMultiple"
        Me.Text = "กำหนด ตัวคูณ ราคา"
        CType(Me.ogboperation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogboperation.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogcoperation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvoperation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFNEmpTeam, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFNMultiple, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNHSysOperationId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNMockUpType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNHSysMarkId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNHSysOperationIdTo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogboperation As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcoperation As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvoperation As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNHSysOperationId As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents ReposFNHSysMarkId As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents ReposFNMockUpType As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents ReposFNHSysOperationIdTo As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmrefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CFNStartQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents CFNEndQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNMultiple As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFNMultiple As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ocmremove As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CFNEmpTeam As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFNEmpTeam As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
End Class
