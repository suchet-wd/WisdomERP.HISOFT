<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wMerteamPermission
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
        Dim RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ogbbutton = New DevExpress.XtraEditors.GroupControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.FTUserName = New DevExpress.XtraEditors.TextEdit()
        Me.FTUserName_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ogd = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysPermissionID = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTCustCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTCustName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUserDescriptionTH_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTUserDescriptionTH = New DevExpress.XtraEditors.TextEdit()
        Me.FTUserDescriptionEN_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTUserDescriptionEN = New DevExpress.XtraEditors.TextEdit()
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbbutton.SuspendLayout()
        CType(Me.FTUserName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTUserDescriptionTH.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTUserDescriptionEN.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbbutton
        '
        Me.ogbbutton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbbutton.Controls.Add(Me.ocmexit)
        Me.ogbbutton.Controls.Add(Me.ocmsave)
        Me.ogbbutton.Location = New System.Drawing.Point(1, 638)
        Me.ogbbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbbutton.Name = "ogbbutton"
        Me.ogbbutton.ShowCaption = False
        Me.ogbbutton.Size = New System.Drawing.Size(779, 52)
        Me.ogbbutton.TabIndex = 17
        Me.ogbbutton.Text = "GroupControl9"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(480, 11)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(160, 31)
        Me.ocmexit.TabIndex = 103
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(124, 11)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(160, 31)
        Me.ocmsave.TabIndex = 0
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'FTUserName
        '
        Me.FTUserName.Location = New System.Drawing.Point(200, 7)
        Me.FTUserName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTUserName.Name = "FTUserName"
        Me.FTUserName.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTUserName.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTUserName.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTUserName.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTUserName.Properties.MaxLength = 30
        Me.FTUserName.Properties.ReadOnly = True
        Me.FTUserName.Size = New System.Drawing.Size(229, 22)
        Me.FTUserName.TabIndex = 0
        Me.FTUserName.Tag = "2|"
        '
        'FTUserName_lbl
        '
        Me.FTUserName_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTUserName_lbl.Appearance.Options.UseForeColor = True
        Me.FTUserName_lbl.Appearance.Options.UseTextOptions = True
        Me.FTUserName_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTUserName_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTUserName_lbl.Location = New System.Drawing.Point(45, 7)
        Me.FTUserName_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTUserName_lbl.Name = "FTUserName_lbl"
        Me.FTUserName_lbl.Size = New System.Drawing.Size(152, 23)
        Me.FTUserName_lbl.TabIndex = 277
        Me.FTUserName_lbl.Tag = "2|"
        Me.FTUserName_lbl.Text = "Mer Team :"
        '
        'ogd
        '
        Me.ogd.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogd.Location = New System.Drawing.Point(1, 93)
        Me.ogd.MainView = Me.ogv
        Me.ogd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogd.Name = "ogd"
        Me.ogd.Size = New System.Drawing.Size(770, 537)
        Me.ogd.TabIndex = 4
        Me.ogd.TabStop = False
        Me.ogd.Tag = "2|"
        Me.ogd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.FNHSysPermissionID, Me.CFTCustCode, Me.CFTCustName})
        Me.ogv.GridControl = Me.ogd
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowGroup = False
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'FTSelect
        '
        Me.FTSelect.AppearanceCell.BackColor = System.Drawing.Color.LightCyan
        Me.FTSelect.AppearanceCell.Options.UseBackColor = True
        Me.FTSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSelect.Caption = "FTSelect"
        RepositoryItemCheckEdit1.AutoHeight = False
        RepositoryItemCheckEdit1.Caption = "Check"
        RepositoryItemCheckEdit1.Name = "RepFTSelect"
        RepositoryItemCheckEdit1.ValueChecked = "1"
        RepositoryItemCheckEdit1.ValueUnchecked = "0"
        Me.FTSelect.ColumnEdit = RepositoryItemCheckEdit1
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowMove = False
        Me.FTSelect.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        '
        'FNHSysPermissionID
        '
        Me.FNHSysPermissionID.AppearanceHeader.Options.UseTextOptions = True
        Me.FNHSysPermissionID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNHSysPermissionID.Caption = "FNHSysCustId"
        Me.FNHSysPermissionID.FieldName = "FNHSysCustId"
        Me.FNHSysPermissionID.Name = "FNHSysPermissionID"
        Me.FNHSysPermissionID.OptionsColumn.AllowEdit = False
        Me.FNHSysPermissionID.OptionsColumn.AllowMove = False
        Me.FNHSysPermissionID.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysPermissionID.OptionsColumn.ReadOnly = True
        '
        'CFTCustCode
        '
        Me.CFTCustCode.Caption = "Code"
        Me.CFTCustCode.FieldName = "FTCustCode"
        Me.CFTCustCode.Name = "CFTCustCode"
        Me.CFTCustCode.OptionsColumn.AllowEdit = False
        Me.CFTCustCode.OptionsColumn.ReadOnly = True
        Me.CFTCustCode.Visible = True
        Me.CFTCustCode.VisibleIndex = 1
        Me.CFTCustCode.Width = 100
        '
        'CFTCustName
        '
        Me.CFTCustName.AppearanceHeader.Options.UseTextOptions = True
        Me.CFTCustName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CFTCustName.Caption = "Customer Name"
        Me.CFTCustName.FieldName = "FTCustName"
        Me.CFTCustName.Name = "CFTCustName"
        Me.CFTCustName.OptionsColumn.AllowEdit = False
        Me.CFTCustName.OptionsColumn.AllowMove = False
        Me.CFTCustName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTCustName.OptionsColumn.ReadOnly = True
        Me.CFTCustName.Visible = True
        Me.CFTCustName.VisibleIndex = 2
        Me.CFTCustName.Width = 546
        '
        'FTUserDescriptionTH_lbl
        '
        Me.FTUserDescriptionTH_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTUserDescriptionTH_lbl.Appearance.Options.UseForeColor = True
        Me.FTUserDescriptionTH_lbl.Appearance.Options.UseTextOptions = True
        Me.FTUserDescriptionTH_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTUserDescriptionTH_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTUserDescriptionTH_lbl.Location = New System.Drawing.Point(8, 34)
        Me.FTUserDescriptionTH_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTUserDescriptionTH_lbl.Name = "FTUserDescriptionTH_lbl"
        Me.FTUserDescriptionTH_lbl.Size = New System.Drawing.Size(189, 23)
        Me.FTUserDescriptionTH_lbl.TabIndex = 279
        Me.FTUserDescriptionTH_lbl.Tag = "2|"
        Me.FTUserDescriptionTH_lbl.Text = "Mer Team Description TH :"
        '
        'FTUserDescriptionTH
        '
        Me.FTUserDescriptionTH.Location = New System.Drawing.Point(200, 35)
        Me.FTUserDescriptionTH.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTUserDescriptionTH.Name = "FTUserDescriptionTH"
        Me.FTUserDescriptionTH.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTUserDescriptionTH.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTUserDescriptionTH.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTUserDescriptionTH.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTUserDescriptionTH.Properties.MaxLength = 200
        Me.FTUserDescriptionTH.Properties.ReadOnly = True
        Me.FTUserDescriptionTH.Size = New System.Drawing.Size(390, 22)
        Me.FTUserDescriptionTH.TabIndex = 1
        Me.FTUserDescriptionTH.Tag = "2|"
        '
        'FTUserDescriptionEN_lbl
        '
        Me.FTUserDescriptionEN_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTUserDescriptionEN_lbl.Appearance.Options.UseForeColor = True
        Me.FTUserDescriptionEN_lbl.Appearance.Options.UseTextOptions = True
        Me.FTUserDescriptionEN_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTUserDescriptionEN_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTUserDescriptionEN_lbl.Location = New System.Drawing.Point(9, 62)
        Me.FTUserDescriptionEN_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTUserDescriptionEN_lbl.Name = "FTUserDescriptionEN_lbl"
        Me.FTUserDescriptionEN_lbl.Size = New System.Drawing.Size(189, 23)
        Me.FTUserDescriptionEN_lbl.TabIndex = 281
        Me.FTUserDescriptionEN_lbl.Tag = "2|"
        Me.FTUserDescriptionEN_lbl.Text = "Mer Team Description EN :"
        '
        'FTUserDescriptionEN
        '
        Me.FTUserDescriptionEN.Location = New System.Drawing.Point(200, 62)
        Me.FTUserDescriptionEN.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTUserDescriptionEN.Name = "FTUserDescriptionEN"
        Me.FTUserDescriptionEN.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTUserDescriptionEN.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTUserDescriptionEN.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTUserDescriptionEN.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTUserDescriptionEN.Properties.MaxLength = 200
        Me.FTUserDescriptionEN.Properties.ReadOnly = True
        Me.FTUserDescriptionEN.Size = New System.Drawing.Size(390, 22)
        Me.FTUserDescriptionEN.TabIndex = 2
        Me.FTUserDescriptionEN.Tag = "2|"
        '
        'wMerteamPermission
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(783, 692)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogd)
        Me.Controls.Add(Me.FTUserDescriptionEN)
        Me.Controls.Add(Me.FTUserDescriptionEN_lbl)
        Me.Controls.Add(Me.ogbbutton)
        Me.Controls.Add(Me.FTUserDescriptionTH)
        Me.Controls.Add(Me.FTUserName_lbl)
        Me.Controls.Add(Me.FTUserDescriptionTH_lbl)
        Me.Controls.Add(Me.FTUserName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wMerteamPermission"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Merteam Permission"
        CType(Me.ogbbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbbutton.ResumeLayout(False)
        CType(Me.FTUserName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTUserDescriptionTH.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTUserDescriptionEN.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbbutton As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTUserName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTUserName_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysPermissionID As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTCustName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogd As DevExpress.XtraGrid.GridControl
    Friend WithEvents CFTCustCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUserDescriptionTH_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTUserDescriptionTH As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTUserDescriptionEN_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTUserDescriptionEN As DevExpress.XtraEditors.TextEdit
End Class
