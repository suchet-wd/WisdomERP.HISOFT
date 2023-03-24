<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wAddSkillByStyle
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
        Me.ocmadd = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.CFTGraphNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTJobNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTSubJobNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStyleNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNSkillByStyle = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNSkillByStyle = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FTLineNo = New DevExpress.XtraEditors.TextEdit()
        Me.FTLineNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNSkillByStyle_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNSkillByStyle = New DevExpress.XtraEditors.CalcEdit()
        Me.FTStateSelectAll = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNSkillByStyle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTLineNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNSkillByStyle.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStateSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmadd
        '
        Me.ocmadd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmadd.Location = New System.Drawing.Point(159, 630)
        Me.ocmadd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmadd.Name = "ocmadd"
        Me.ocmadd.Size = New System.Drawing.Size(169, 33)
        Me.ocmadd.TabIndex = 12
        Me.ocmadd.Text = "ADD SKILL BY STYLE"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(642, 630)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(169, 33)
        Me.ocmcancel.TabIndex = 13
        Me.ocmcancel.Text = "CANCEL"
        '
        'ogcdetail
        '
        Me.ogcdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Location = New System.Drawing.Point(-3, 90)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFNSkillByStyle, Me.ReposFTSelect})
        Me.ogcdetail.Size = New System.Drawing.Size(999, 521)
        Me.ogcdetail.TabIndex = 14
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTSelect, Me.CFTGraphNo, Me.CFTJobNo, Me.CFTSubJobNo, Me.CFTStyleNo, Me.CFNSkillByStyle})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowGroup = False
        Me.ogvdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        Me.ogvdetail.Tag = "2|"
        '
        'CFTSelect
        '
        Me.CFTSelect.Caption = "Select"
        Me.CFTSelect.ColumnEdit = Me.ReposFTSelect
        Me.CFTSelect.FieldName = "FTSelect"
        Me.CFTSelect.Name = "CFTSelect"
        Me.CFTSelect.OptionsColumn.AllowMove = False
        Me.CFTSelect.OptionsColumn.AllowShowHide = False
        Me.CFTSelect.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTSelect.Visible = True
        Me.CFTSelect.VisibleIndex = 0
        Me.CFTSelect.Width = 60
        '
        'ReposFTSelect
        '
        Me.ReposFTSelect.AutoHeight = False
        Me.ReposFTSelect.Name = "ReposFTSelect"
        Me.ReposFTSelect.ValueChecked = "1"
        Me.ReposFTSelect.ValueUnchecked = "0"
        '
        'CFTGraphNo
        '
        Me.CFTGraphNo.Caption = "Graph No"
        Me.CFTGraphNo.FieldName = "FTGraphNo"
        Me.CFTGraphNo.Name = "CFTGraphNo"
        Me.CFTGraphNo.OptionsColumn.AllowEdit = False
        Me.CFTGraphNo.OptionsColumn.AllowMove = False
        Me.CFTGraphNo.OptionsColumn.AllowShowHide = False
        Me.CFTGraphNo.OptionsColumn.ReadOnly = True
        Me.CFTGraphNo.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTGraphNo.Visible = True
        Me.CFTGraphNo.VisibleIndex = 1
        Me.CFTGraphNo.Width = 138
        '
        'CFTJobNo
        '
        Me.CFTJobNo.Caption = "FO. No"
        Me.CFTJobNo.FieldName = "FTJobNo"
        Me.CFTJobNo.Name = "CFTJobNo"
        Me.CFTJobNo.OptionsColumn.AllowEdit = False
        Me.CFTJobNo.OptionsColumn.AllowMove = False
        Me.CFTJobNo.OptionsColumn.AllowShowHide = False
        Me.CFTJobNo.OptionsColumn.ReadOnly = True
        Me.CFTJobNo.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTJobNo.Visible = True
        Me.CFTJobNo.VisibleIndex = 2
        Me.CFTJobNo.Width = 121
        '
        'CFTSubJobNo
        '
        Me.CFTSubJobNo.Caption = "Sub FO. No"
        Me.CFTSubJobNo.FieldName = "FTSubJobNo"
        Me.CFTSubJobNo.Name = "CFTSubJobNo"
        Me.CFTSubJobNo.OptionsColumn.AllowEdit = False
        Me.CFTSubJobNo.OptionsColumn.AllowMove = False
        Me.CFTSubJobNo.OptionsColumn.AllowShowHide = False
        Me.CFTSubJobNo.OptionsColumn.ReadOnly = True
        Me.CFTSubJobNo.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTSubJobNo.Visible = True
        Me.CFTSubJobNo.VisibleIndex = 3
        Me.CFTSubJobNo.Width = 125
        '
        'CFTStyleNo
        '
        Me.CFTStyleNo.Caption = "Style No"
        Me.CFTStyleNo.FieldName = "FTStyleNo"
        Me.CFTStyleNo.Name = "CFTStyleNo"
        Me.CFTStyleNo.OptionsColumn.AllowEdit = False
        Me.CFTStyleNo.OptionsColumn.AllowMove = False
        Me.CFTStyleNo.OptionsColumn.AllowShowHide = False
        Me.CFTStyleNo.OptionsColumn.ReadOnly = True
        Me.CFTStyleNo.OptionsColumn.ShowInCustomizationForm = False
        Me.CFTStyleNo.Visible = True
        Me.CFTStyleNo.VisibleIndex = 4
        Me.CFTStyleNo.Width = 165
        '
        'CFNSkillByStyle
        '
        Me.CFNSkillByStyle.AppearanceCell.BackColor = System.Drawing.Color.LightCyan
        Me.CFNSkillByStyle.AppearanceCell.ForeColor = System.Drawing.Color.Blue
        Me.CFNSkillByStyle.AppearanceCell.Options.UseBackColor = True
        Me.CFNSkillByStyle.AppearanceCell.Options.UseForeColor = True
        Me.CFNSkillByStyle.Caption = "Skil lBy Style"
        Me.CFNSkillByStyle.ColumnEdit = Me.ReposFNSkillByStyle
        Me.CFNSkillByStyle.FieldName = "FNSkillByStyle"
        Me.CFNSkillByStyle.Name = "CFNSkillByStyle"
        Me.CFNSkillByStyle.OptionsColumn.AllowMove = False
        Me.CFNSkillByStyle.OptionsColumn.AllowShowHide = False
        Me.CFNSkillByStyle.OptionsColumn.ShowInCustomizationForm = False
        Me.CFNSkillByStyle.Visible = True
        Me.CFNSkillByStyle.VisibleIndex = 5
        Me.CFNSkillByStyle.Width = 133
        '
        'ReposFNSkillByStyle
        '
        Me.ReposFNSkillByStyle.AutoHeight = False
        Me.ReposFNSkillByStyle.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNSkillByStyle.DisplayFormat.FormatString = "{0:n2}"
        Me.ReposFNSkillByStyle.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNSkillByStyle.EditFormat.FormatString = "{0:n2}"
        Me.ReposFNSkillByStyle.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNSkillByStyle.Name = "ReposFNSkillByStyle"
        Me.ReposFNSkillByStyle.Precision = 2
        '
        'FTLineNo
        '
        Me.FTLineNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTLineNo.EnterMoveNextControl = True
        Me.FTLineNo.Location = New System.Drawing.Point(180, 13)
        Me.FTLineNo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTLineNo.Name = "FTLineNo"
        Me.FTLineNo.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTLineNo.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FTLineNo.Properties.Appearance.Options.UseBackColor = True
        Me.FTLineNo.Properties.Appearance.Options.UseForeColor = True
        Me.FTLineNo.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTLineNo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTLineNo.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTLineNo.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTLineNo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTLineNo.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTLineNo.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTLineNo.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTLineNo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTLineNo.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTLineNo.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTLineNo.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTLineNo.Properties.ReadOnly = True
        Me.FTLineNo.Size = New System.Drawing.Size(792, 22)
        Me.FTLineNo.TabIndex = 277
        Me.FTLineNo.TabStop = False
        Me.FTLineNo.Tag = "2|"
        '
        'FTLineNo_lbl
        '
        Me.FTLineNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTLineNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTLineNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTLineNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTLineNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTLineNo_lbl.Location = New System.Drawing.Point(12, 12)
        Me.FTLineNo_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTLineNo_lbl.Name = "FTLineNo_lbl"
        Me.FTLineNo_lbl.Size = New System.Drawing.Size(164, 23)
        Me.FTLineNo_lbl.TabIndex = 276
        Me.FTLineNo_lbl.Tag = "2|"
        Me.FTLineNo_lbl.Text = "Line No :"
        '
        'FNSkillByStyle_lbl
        '
        Me.FNSkillByStyle_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNSkillByStyle_lbl.Appearance.Options.UseForeColor = True
        Me.FNSkillByStyle_lbl.Appearance.Options.UseTextOptions = True
        Me.FNSkillByStyle_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSkillByStyle_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNSkillByStyle_lbl.Location = New System.Drawing.Point(12, 42)
        Me.FNSkillByStyle_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNSkillByStyle_lbl.Name = "FNSkillByStyle_lbl"
        Me.FNSkillByStyle_lbl.Size = New System.Drawing.Size(164, 23)
        Me.FNSkillByStyle_lbl.TabIndex = 278
        Me.FNSkillByStyle_lbl.Tag = "2|"
        Me.FNSkillByStyle_lbl.Text = "Skill By Style :"
        '
        'FNSkillByStyle
        '
        Me.FNSkillByStyle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNSkillByStyle.EnterMoveNextControl = True
        Me.FNSkillByStyle.Location = New System.Drawing.Point(180, 43)
        Me.FNSkillByStyle.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNSkillByStyle.Name = "FNSkillByStyle"
        Me.FNSkillByStyle.Properties.Appearance.Options.UseTextOptions = True
        Me.FNSkillByStyle.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNSkillByStyle.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNSkillByStyle.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNSkillByStyle.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNSkillByStyle.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNSkillByStyle.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNSkillByStyle.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNSkillByStyle.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNSkillByStyle.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNSkillByStyle.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNSkillByStyle.Properties.DisplayFormat.FormatString = "{0:n2}"
        Me.FNSkillByStyle.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSkillByStyle.Properties.EditFormat.FormatString = "{0:n2}"
        Me.FNSkillByStyle.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSkillByStyle.Properties.Precision = 2
        Me.FNSkillByStyle.Size = New System.Drawing.Size(162, 22)
        Me.FNSkillByStyle.TabIndex = 279
        Me.FNSkillByStyle.Tag = "2|"
        '
        'FTStateSelectAll
        '
        Me.FTStateSelectAll.EditValue = "0"
        Me.FTStateSelectAll.Location = New System.Drawing.Point(4, 68)
        Me.FTStateSelectAll.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStateSelectAll.Name = "FTStateSelectAll"
        Me.FTStateSelectAll.Properties.Caption = "เลือกทั้งหมด"
        Me.FTStateSelectAll.Properties.ValueChecked = "1"
        Me.FTStateSelectAll.Properties.ValueUnchecked = "0"
        Me.FTStateSelectAll.Size = New System.Drawing.Size(178, 21)
        Me.FTStateSelectAll.TabIndex = 419
        Me.FTStateSelectAll.Tag = "2|"
        '
        'wAddSkillByStyle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(996, 676)
        Me.ControlBox = False
        Me.Controls.Add(Me.FTStateSelectAll)
        Me.Controls.Add(Me.FNSkillByStyle)
        Me.Controls.Add(Me.FNSkillByStyle_lbl)
        Me.Controls.Add(Me.FTLineNo)
        Me.Controls.Add(Me.FTLineNo_lbl)
        Me.Controls.Add(Me.ogcdetail)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmadd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wAddSkillByStyle"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Skill By Style"
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNSkillByStyle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTLineNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNSkillByStyle.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStateSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ocmadd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTLineNo As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FTLineNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNSkillByStyle_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNSkillByStyle As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents CFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CFTGraphNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTJobNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTSubJobNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStyleNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNSkillByStyle As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNSkillByStyle As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FTStateSelectAll As DevExpress.XtraEditors.CheckEdit
End Class
