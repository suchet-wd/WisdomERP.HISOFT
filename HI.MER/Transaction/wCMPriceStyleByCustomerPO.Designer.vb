<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wCMPriceStyleByCustomerPO
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
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
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
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFNHSysStyleId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPOref = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNCM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepCMP = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FNCMDisPer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNCMDisAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNNetCM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNHSysOperationId = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.ReposFNOperationState = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.ReposFNHSysMarkId = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.ReposFNHSysOperationIdTo = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        CType(Me.ogbStyleHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbStyleHeader.SuspendLayout()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogboperation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogboperation.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepCMP, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, "", "157", Nothing, True)})
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(152, 22)
        Me.FNHSysStyleId.TabIndex = 2
        Me.FNHSysStyleId.Tag = "2|"
        '
        'ogboperation
        '
        Me.ogboperation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogboperation.Controls.Add(Me.ogbmainprocbutton)
        Me.ogboperation.Controls.Add(Me.ogcdetail)
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
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Location = New System.Drawing.Point(2, 25)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFNHSysOperationId, Me.ReposFNOperationState, Me.ReposFNHSysMarkId, Me.ReposFNHSysOperationIdTo, Me.RepCMP})
        Me.ogcdetail.Size = New System.Drawing.Size(1298, 682)
        Me.ogcdetail.TabIndex = 301
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "3|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFNHSysStyleId, Me.FTPOref, Me.FNCM, Me.FNCMDisPer, Me.FNCMDisAmt, Me.FNNetCM})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowGroup = False
        Me.ogvdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        Me.ogvdetail.Tag = "2|"
        '
        'CFNHSysStyleId
        '
        Me.CFNHSysStyleId.Caption = "FNHSysStyleId"
        Me.CFNHSysStyleId.FieldName = "FNHSysStyleId"
        Me.CFNHSysStyleId.Name = "CFNHSysStyleId"
        Me.CFNHSysStyleId.OptionsColumn.AllowEdit = False
        Me.CFNHSysStyleId.OptionsColumn.AllowFocus = False
        Me.CFNHSysStyleId.OptionsColumn.ReadOnly = True
        '
        'FTPOref
        '
        Me.FTPOref.Caption = "Customer PO"
        Me.FTPOref.FieldName = "FTPOref"
        Me.FTPOref.Name = "FTPOref"
        Me.FTPOref.OptionsColumn.AllowEdit = False
        Me.FTPOref.OptionsColumn.AllowFocus = False
        Me.FTPOref.OptionsColumn.ReadOnly = True
        Me.FTPOref.Visible = True
        Me.FTPOref.VisibleIndex = 0
        Me.FTPOref.Width = 152
        '
        'FNCM
        '
        Me.FNCM.Caption = "CMP"
        Me.FNCM.ColumnEdit = Me.RepCMP
        Me.FNCM.DisplayFormat.FormatString = "{0:n4}"
        Me.FNCM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCM.FieldName = "FNCM"
        Me.FNCM.Name = "FNCM"
        Me.FNCM.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNCM.OptionsColumn.AllowMove = False
        Me.FNCM.Visible = True
        Me.FNCM.VisibleIndex = 1
        Me.FNCM.Width = 103
        '
        'RepCMP
        '
        Me.RepCMP.AutoHeight = False
        Me.RepCMP.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepCMP.DisplayFormat.FormatString = "{0:n4}"
        Me.RepCMP.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepCMP.EditFormat.FormatString = "{0:n4}"
        Me.RepCMP.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepCMP.Name = "RepCMP"
        '
        'FNCMDisPer
        '
        Me.FNCMDisPer.Caption = "CMP Dis Per"
        Me.FNCMDisPer.DisplayFormat.FormatString = "{0:n2}"
        Me.FNCMDisPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCMDisPer.FieldName = "FNCMDisPer"
        Me.FNCMDisPer.Name = "FNCMDisPer"
        Me.FNCMDisPer.OptionsColumn.AllowEdit = False
        Me.FNCMDisPer.OptionsColumn.AllowFocus = False
        Me.FNCMDisPer.OptionsColumn.ReadOnly = True
        Me.FNCMDisPer.Visible = True
        Me.FNCMDisPer.VisibleIndex = 2
        Me.FNCMDisPer.Width = 125
        '
        'FNCMDisAmt
        '
        Me.FNCMDisAmt.Caption = "CMP Dis Amt"
        Me.FNCMDisAmt.DisplayFormat.FormatString = "{0:n4}"
        Me.FNCMDisAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNCMDisAmt.FieldName = "FNCMDisAmt"
        Me.FNCMDisAmt.Name = "FNCMDisAmt"
        Me.FNCMDisAmt.OptionsColumn.AllowEdit = False
        Me.FNCMDisAmt.OptionsColumn.AllowFocus = False
        Me.FNCMDisAmt.OptionsColumn.ReadOnly = True
        Me.FNCMDisAmt.Visible = True
        Me.FNCMDisAmt.VisibleIndex = 3
        Me.FNCMDisAmt.Width = 117
        '
        'FNNetCM
        '
        Me.FNNetCM.Caption = "Net CMP"
        Me.FNNetCM.DisplayFormat.FormatString = "{0:n4}"
        Me.FNNetCM.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNNetCM.FieldName = "FNNetCM"
        Me.FNNetCM.Name = "FNNetCM"
        Me.FNNetCM.OptionsColumn.AllowEdit = False
        Me.FNNetCM.OptionsColumn.AllowFocus = False
        Me.FNNetCM.OptionsColumn.ReadOnly = True
        Me.FNNetCM.Visible = True
        Me.FNNetCM.VisibleIndex = 4
        Me.FNNetCM.Width = 120
        '
        'ReposFNHSysOperationId
        '
        Me.ReposFNHSysOperationId.AutoHeight = False
        Me.ReposFNHSysOperationId.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "155", Nothing, True)})
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
        Me.ReposFNHSysMarkId.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject6, "", "158", Nothing, True)})
        Me.ReposFNHSysMarkId.Name = "ReposFNHSysMarkId"
        '
        'ReposFNHSysOperationIdTo
        '
        Me.ReposFNHSysOperationIdTo.AutoHeight = False
        Me.ReposFNHSysOperationIdTo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject7, "", "156", Nothing, True)})
        Me.ReposFNHSysOperationIdTo.Name = "ReposFNHSysOperationIdTo"
        '
        'wCMPriceStyleByCustomerPO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1304, 793)
        Me.Controls.Add(Me.ogboperation)
        Me.Controls.Add(Me.ogbStyleHeader)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wCMPriceStyleByCustomerPO"
        Me.Text = "CM Price By Style And Customer PO"
        CType(Me.ogbStyleHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbStyleHeader.ResumeLayout(False)
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogboperation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogboperation.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepCMP, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ReposFNHSysOperationId As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents ReposFNHSysMarkId As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents ReposFNOperationState As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents ReposFNHSysOperationIdTo As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmrefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents CFNHSysStyleId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPOref As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNCM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepCMP As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FNCMDisPer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNCMDisAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNNetCM As DevExpress.XtraGrid.Columns.GridColumn
End Class
