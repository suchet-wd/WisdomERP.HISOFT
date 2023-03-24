<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wListCarton
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
        Me.ogclist = New DevExpress.XtraGrid.GridControl()
        Me.ogvlist = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTPackNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositorySelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTCartonNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSubOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSizeBreakDown = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTNikePOLineItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTBarCodeCarton = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ogblist = New DevExpress.XtraEditors.GroupControl()
        Me.ocmCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmSave = New DevExpress.XtraEditors.SimpleButton()
        Me.oSelectAll = New DevExpress.XtraEditors.CheckEdit()
        Me.FNHSysWHFGId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysWHFGId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysWHFGId_lbl = New DevExpress.XtraEditors.LabelControl()
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositorySelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogblist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogblist.SuspendLayout()
        CType(Me.oSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHFGId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysWHFGId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogclist
        '
        Me.ogclist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogclist.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogclist.Location = New System.Drawing.Point(2, 25)
        Me.ogclist.MainView = Me.ogvlist
        Me.ogclist.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogclist.Name = "ogclist"
        Me.ogclist.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect, Me.RepositorySelect})
        Me.ogclist.Size = New System.Drawing.Size(1257, 783)
        Me.ogclist.TabIndex = 302
        Me.ogclist.TabStop = False
        Me.ogclist.Tag = "2|"
        Me.ogclist.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvlist})
        '
        'ogvlist
        '
        Me.ogvlist.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTPackNo, Me.FTSelect, Me.FTCartonNo, Me.FTPORef, Me.FTOrderNo, Me.FTSubOrderNo, Me.FTColorway, Me.FTSizeBreakDown, Me.FTNikePOLineItem, Me.FNQuantity, Me.FTBarCodeCarton})
        Me.ogvlist.GridControl = Me.ogclist
        Me.ogvlist.Name = "ogvlist"
        Me.ogvlist.OptionsCustomization.AllowGroup = False
        Me.ogvlist.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvlist.OptionsSelection.MultiSelect = True
        Me.ogvlist.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = True
        Me.ogvlist.OptionsSelection.UseIndicatorForSelection = False
        Me.ogvlist.OptionsView.ColumnAutoWidth = False
        Me.ogvlist.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvlist.OptionsView.ShowGroupPanel = False
        Me.ogvlist.Tag = "2|"
        '
        'FTPackNo
        '
        Me.FTPackNo.Caption = "FTPackNo"
        Me.FTPackNo.FieldName = "FTPackNo"
        Me.FTPackNo.Name = "FTPackNo"
        Me.FTPackNo.OptionsColumn.AllowEdit = False
        Me.FTPackNo.Visible = True
        Me.FTPackNo.VisibleIndex = 1
        Me.FTPackNo.Width = 154
        '
        'FTSelect
        '
        Me.FTSelect.Caption = "Select"
        Me.FTSelect.ColumnEdit = Me.RepositorySelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        '
        'RepositorySelect
        '
        Me.RepositorySelect.AutoHeight = False
        Me.RepositorySelect.Caption = "Check"
        Me.RepositorySelect.Name = "RepositorySelect"
        Me.RepositorySelect.ValueChecked = "1"
        Me.RepositorySelect.ValueUnchecked = "0"
        '
        'FTCartonNo
        '
        Me.FTCartonNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTCartonNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTCartonNo.Caption = "FTCartonNo"
        Me.FTCartonNo.FieldName = "FTCartonNo"
        Me.FTCartonNo.Name = "FTCartonNo"
        Me.FTCartonNo.OptionsColumn.AllowEdit = False
        Me.FTCartonNo.OptionsColumn.AllowMove = False
        Me.FTCartonNo.OptionsColumn.ReadOnly = True
        Me.FTCartonNo.Visible = True
        Me.FTCartonNo.VisibleIndex = 2
        Me.FTCartonNo.Width = 148
        '
        'FTPORef
        '
        Me.FTPORef.Caption = "FTPORef"
        Me.FTPORef.FieldName = "FTPORef"
        Me.FTPORef.Name = "FTPORef"
        Me.FTPORef.OptionsColumn.AllowEdit = False
        Me.FTPORef.Visible = True
        Me.FTPORef.VisibleIndex = 3
        Me.FTPORef.Width = 177
        '
        'FTOrderNo
        '
        Me.FTOrderNo.Caption = "FTOrderNo"
        Me.FTOrderNo.FieldName = "FTOrderNo"
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.OptionsColumn.AllowEdit = False
        Me.FTOrderNo.Visible = True
        Me.FTOrderNo.VisibleIndex = 4
        Me.FTOrderNo.Width = 122
        '
        'FTSubOrderNo
        '
        Me.FTSubOrderNo.Caption = "FTSubOrderNo"
        Me.FTSubOrderNo.FieldName = "FTSubOrderNo"
        Me.FTSubOrderNo.Name = "FTSubOrderNo"
        Me.FTSubOrderNo.OptionsColumn.AllowEdit = False
        Me.FTSubOrderNo.Visible = True
        Me.FTSubOrderNo.VisibleIndex = 5
        Me.FTSubOrderNo.Width = 118
        '
        'FTColorway
        '
        Me.FTColorway.Caption = "FTColorway"
        Me.FTColorway.FieldName = "FTColorway"
        Me.FTColorway.Name = "FTColorway"
        Me.FTColorway.OptionsColumn.AllowEdit = False
        Me.FTColorway.Visible = True
        Me.FTColorway.VisibleIndex = 7
        Me.FTColorway.Width = 100
        '
        'FTSizeBreakDown
        '
        Me.FTSizeBreakDown.Caption = "FTSizeBreakDown"
        Me.FTSizeBreakDown.FieldName = "FTSizeBreakDown"
        Me.FTSizeBreakDown.Name = "FTSizeBreakDown"
        Me.FTSizeBreakDown.OptionsColumn.AllowEdit = False
        Me.FTSizeBreakDown.Visible = True
        Me.FTSizeBreakDown.VisibleIndex = 8
        Me.FTSizeBreakDown.Width = 131
        '
        'FTNikePOLineItem
        '
        Me.FTNikePOLineItem.Caption = "FTNikePOLineItem"
        Me.FTNikePOLineItem.FieldName = "FTNikePOLineItem"
        Me.FTNikePOLineItem.Name = "FTNikePOLineItem"
        Me.FTNikePOLineItem.OptionsColumn.AllowEdit = False
        Me.FTNikePOLineItem.Visible = True
        Me.FTNikePOLineItem.VisibleIndex = 6
        Me.FTNikePOLineItem.Width = 86
        '
        'FNQuantity
        '
        Me.FNQuantity.Caption = "FNQuantity"
        Me.FNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNQuantity"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 9
        Me.FNQuantity.Width = 148
        '
        'FTBarCodeCarton
        '
        Me.FTBarCodeCarton.Caption = "FTBarCodeCarton"
        Me.FTBarCodeCarton.FieldName = "FTBarCodeCarton"
        Me.FTBarCodeCarton.Name = "FTBarCodeCarton"
        Me.FTBarCodeCarton.OptionsColumn.AllowEdit = False
        Me.FTBarCodeCarton.Visible = True
        Me.FTBarCodeCarton.VisibleIndex = 10
        Me.FTBarCodeCarton.Width = 193
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Caption = "Check"
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'ogblist
        '
        Me.ogblist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogblist.Controls.Add(Me.ogclist)
        Me.ogblist.Location = New System.Drawing.Point(0, 0)
        Me.ogblist.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogblist.Name = "ogblist"
        Me.ogblist.Size = New System.Drawing.Size(1261, 810)
        Me.ogblist.TabIndex = 303
        Me.ogblist.Text = "List Carton"
        '
        'ocmCancel
        '
        Me.ocmCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmCancel.Location = New System.Drawing.Point(1159, 815)
        Me.ocmCancel.Name = "ocmCancel"
        Me.ocmCancel.Size = New System.Drawing.Size(83, 26)
        Me.ocmCancel.TabIndex = 304
        Me.ocmCancel.Text = "Cancel"
        '
        'ocmSave
        '
        Me.ocmSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmSave.Location = New System.Drawing.Point(1064, 815)
        Me.ocmSave.Name = "ocmSave"
        Me.ocmSave.Size = New System.Drawing.Size(83, 26)
        Me.ocmSave.TabIndex = 304
        Me.ocmSave.Text = "Save"
        '
        'oSelectAll
        '
        Me.oSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.oSelectAll.Location = New System.Drawing.Point(13, 820)
        Me.oSelectAll.Name = "oSelectAll"
        Me.oSelectAll.Properties.Caption = "Select All"
        Me.oSelectAll.Size = New System.Drawing.Size(171, 20)
        Me.oSelectAll.TabIndex = 305
        '
        'FNHSysWHFGId_None
        '
        Me.FNHSysWHFGId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysWHFGId_None.EnterMoveNextControl = True
        Me.FNHSysWHFGId_None.Location = New System.Drawing.Point(457, 819)
        Me.FNHSysWHFGId_None.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHFGId_None.Name = "FNHSysWHFGId_None"
        Me.FNHSysWHFGId_None.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FNHSysWHFGId_None.Properties.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNHSysWHFGId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysWHFGId_None.Properties.Appearance.Options.UseForeColor = True
        Me.FNHSysWHFGId_None.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHFGId_None.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHFGId_None.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FNHSysWHFGId_None.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FNHSysWHFGId_None.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FNHSysWHFGId_None.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHFGId_None.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FNHSysWHFGId_None.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FNHSysWHFGId_None.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysWHFGId_None.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHFGId_None.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysWHFGId_None.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysWHFGId_None.Properties.ReadOnly = True
        Me.FNHSysWHFGId_None.Size = New System.Drawing.Size(571, 22)
        Me.FNHSysWHFGId_None.TabIndex = 308
        Me.FNHSysWHFGId_None.TabStop = False
        Me.FNHSysWHFGId_None.Tag = "2|"
        '
        'FNHSysWHFGId
        '
        Me.FNHSysWHFGId.Location = New System.Drawing.Point(308, 819)
        Me.FNHSysWHFGId.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHFGId.Name = "FNHSysWHFGId"
        Me.FNHSysWHFGId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "282", Nothing, True)})
        Me.FNHSysWHFGId.Size = New System.Drawing.Size(146, 22)
        Me.FNHSysWHFGId.TabIndex = 307
        Me.FNHSysWHFGId.Tag = "2|"
        '
        'FNHSysWHFGId_lbl
        '
        Me.FNHSysWHFGId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysWHFGId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysWHFGId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysWHFGId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysWHFGId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysWHFGId_lbl.Location = New System.Drawing.Point(165, 819)
        Me.FNHSysWHFGId_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNHSysWHFGId_lbl.Name = "FNHSysWHFGId_lbl"
        Me.FNHSysWHFGId_lbl.Size = New System.Drawing.Size(135, 23)
        Me.FNHSysWHFGId_lbl.TabIndex = 306
        Me.FNHSysWHFGId_lbl.Tag = "2|"
        Me.FNHSysWHFGId_lbl.Text = "FNHSysWHFGId :"
        '
        'wListCarton
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1261, 853)
        Me.ControlBox = False
        Me.Controls.Add(Me.FNHSysWHFGId_None)
        Me.Controls.Add(Me.FNHSysWHFGId)
        Me.Controls.Add(Me.FNHSysWHFGId_lbl)
        Me.Controls.Add(Me.oSelectAll)
        Me.Controls.Add(Me.ocmSave)
        Me.Controls.Add(Me.ocmCancel)
        Me.Controls.Add(Me.ogblist)
        Me.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wListCarton"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List Carton"
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositorySelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogblist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogblist.ResumeLayout(False)
        CType(Me.oSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHFGId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysWHFGId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogclist As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvlist As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTCartonNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogblist As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositorySelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents oSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTColorway As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSizeBreakDown As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTNikePOLineItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSubOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPackNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysWHFGId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysWHFGId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysWHFGId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTBarCodeCarton As DevExpress.XtraGrid.Columns.GridColumn
End Class
