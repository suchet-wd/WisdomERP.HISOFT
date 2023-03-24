<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wDCPreparePackingListAddItems
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
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbnote = New DevExpress.XtraEditors.GroupControl()
        Me.FTSubOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTSubOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTOrderNo = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTOrderNo_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTStaSelectAll = New DevExpress.XtraEditors.CheckEdit()
        Me.ogcbarcode = New DevExpress.XtraGrid.GridControl()
        Me.ogvbarcode = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.BFTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.BFTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CXFNHSysRawMatId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNQuantity = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ocmcreatepl = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.CXFNBal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CXFNConSmp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CXFNTotalUsed = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogbnote, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbnote.SuspendLayout()
        CType(Me.FTSubOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FTStaSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNQuantity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbnote
        '
        Me.ogbnote.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbnote.Controls.Add(Me.FTSubOrderNo)
        Me.ogbnote.Controls.Add(Me.FTSubOrderNo_lbl)
        Me.ogbnote.Controls.Add(Me.FTOrderNo)
        Me.ogbnote.Controls.Add(Me.FTOrderNo_lbl)
        Me.ogbnote.Controls.Add(Me.FTStaSelectAll)
        Me.ogbnote.Location = New System.Drawing.Point(3, 2)
        Me.ogbnote.Name = "ogbnote"
        Me.ogbnote.ShowCaption = False
        Me.ogbnote.Size = New System.Drawing.Size(815, 52)
        Me.ogbnote.TabIndex = 142
        Me.ogbnote.Text = "GroupControl1"
        '
        'FTSubOrderNo
        '
        Me.FTSubOrderNo.Location = New System.Drawing.Point(461, 10)
        Me.FTSubOrderNo.Name = "FTSubOrderNo"
        Me.FTSubOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "133", Nothing, True), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "", Nothing, True)})
        Me.FTSubOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTSubOrderNo.Properties.Tag = ""
        Me.FTSubOrderNo.Size = New System.Drawing.Size(145, 20)
        Me.FTSubOrderNo.TabIndex = 424
        Me.FTSubOrderNo.Tag = "2|"
        '
        'FTSubOrderNo_lbl
        '
        Me.FTSubOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTSubOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTSubOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTSubOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTSubOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTSubOrderNo_lbl.Location = New System.Drawing.Point(329, 9)
        Me.FTSubOrderNo_lbl.Name = "FTSubOrderNo_lbl"
        Me.FTSubOrderNo_lbl.Size = New System.Drawing.Size(130, 18)
        Me.FTSubOrderNo_lbl.TabIndex = 425
        Me.FTSubOrderNo_lbl.Tag = "2|"
        Me.FTSubOrderNo_lbl.Text = "FTSubOrderNo :"
        '
        'FTOrderNo
        '
        Me.FTOrderNo.Location = New System.Drawing.Point(144, 7)
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "121", Nothing, True), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F2), SerializableAppearanceObject4, "", "", Nothing, True)})
        Me.FTOrderNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FTOrderNo.Properties.ReadOnly = True
        Me.FTOrderNo.Properties.Tag = ""
        Me.FTOrderNo.Size = New System.Drawing.Size(145, 20)
        Me.FTOrderNo.TabIndex = 422
        Me.FTOrderNo.Tag = "2|"
        '
        'FTOrderNo_lbl
        '
        Me.FTOrderNo_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTOrderNo_lbl.Appearance.Options.UseForeColor = True
        Me.FTOrderNo_lbl.Appearance.Options.UseTextOptions = True
        Me.FTOrderNo_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTOrderNo_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTOrderNo_lbl.Location = New System.Drawing.Point(14, 8)
        Me.FTOrderNo_lbl.Name = "FTOrderNo_lbl"
        Me.FTOrderNo_lbl.Size = New System.Drawing.Size(127, 18)
        Me.FTOrderNo_lbl.TabIndex = 423
        Me.FTOrderNo_lbl.Tag = "2|"
        Me.FTOrderNo_lbl.Text = "FTOrderNo :"
        '
        'FTStaSelectAll
        '
        Me.FTStaSelectAll.Location = New System.Drawing.Point(11, 30)
        Me.FTStaSelectAll.Name = "FTStaSelectAll"
        Me.FTStaSelectAll.Properties.Appearance.Options.UseTextOptions = True
        Me.FTStaSelectAll.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.FTStaSelectAll.Properties.Caption = "Select All"
        Me.FTStaSelectAll.Properties.ValueChecked = "1"
        Me.FTStaSelectAll.Properties.ValueUnchecked = "0"
        Me.FTStaSelectAll.Size = New System.Drawing.Size(123, 19)
        Me.FTStaSelectAll.TabIndex = 286
        Me.FTStaSelectAll.Tag = "2|"
        '
        'ogcbarcode
        '
        Me.ogcbarcode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcbarcode.Location = New System.Drawing.Point(3, 60)
        Me.ogcbarcode.MainView = Me.ogvbarcode
        Me.ogcbarcode.Name = "ogcbarcode"
        Me.ogcbarcode.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect, Me.ReposFNQuantity})
        Me.ogcbarcode.Size = New System.Drawing.Size(1156, 559)
        Me.ogcbarcode.TabIndex = 143
        Me.ogcbarcode.TabStop = False
        Me.ogcbarcode.Tag = "2|"
        Me.ogcbarcode.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvbarcode})
        '
        'ogvbarcode
        '
        Me.ogvbarcode.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.BFTRawMatCode, Me.BFTDescription, Me.BFTRawMatColorCode, Me.BFTRawMatSizeCode, Me.CXFNHSysRawMatId, Me.CXFNConSmp, Me.CXFNBal, Me.CXFNTotalUsed})
        Me.ogvbarcode.GridControl = Me.ogcbarcode
        Me.ogvbarcode.Name = "ogvbarcode"
        Me.ogvbarcode.OptionsCustomization.AllowGroup = False
        Me.ogvbarcode.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvbarcode.OptionsView.ColumnAutoWidth = False
        Me.ogvbarcode.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvbarcode.OptionsView.ShowGroupPanel = False
        Me.ogvbarcode.Tag = "2|"
        '
        'FTSelect
        '
        Me.FTSelect.Caption = "Select"
        Me.FTSelect.ColumnEdit = Me.RepFTSelect
        Me.FTSelect.FieldName = "FTStateSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowShowHide = False
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 56
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Caption = "Check"
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'BFTRawMatCode
        '
        Me.BFTRawMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTRawMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTRawMatCode.Caption = "FTRawMatCode"
        Me.BFTRawMatCode.FieldName = "FTRawMatCode"
        Me.BFTRawMatCode.Name = "BFTRawMatCode"
        Me.BFTRawMatCode.OptionsColumn.AllowEdit = False
        Me.BFTRawMatCode.OptionsColumn.ReadOnly = True
        Me.BFTRawMatCode.Visible = True
        Me.BFTRawMatCode.VisibleIndex = 1
        Me.BFTRawMatCode.Width = 161
        '
        'BFTDescription
        '
        Me.BFTDescription.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTDescription.Caption = "FTDescription"
        Me.BFTDescription.FieldName = "FTRawMatName"
        Me.BFTDescription.Name = "BFTDescription"
        Me.BFTDescription.OptionsColumn.AllowEdit = False
        Me.BFTDescription.OptionsColumn.ReadOnly = True
        Me.BFTDescription.Visible = True
        Me.BFTDescription.VisibleIndex = 2
        Me.BFTDescription.Width = 240
        '
        'BFTRawMatColorCode
        '
        Me.BFTRawMatColorCode.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTRawMatColorCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTRawMatColorCode.Caption = "FTRawMatColorCode"
        Me.BFTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.BFTRawMatColorCode.Name = "BFTRawMatColorCode"
        Me.BFTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.BFTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.BFTRawMatColorCode.Visible = True
        Me.BFTRawMatColorCode.VisibleIndex = 3
        Me.BFTRawMatColorCode.Width = 132
        '
        'BFTRawMatSizeCode
        '
        Me.BFTRawMatSizeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.BFTRawMatSizeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.BFTRawMatSizeCode.Caption = "FTRawMatSizeCode"
        Me.BFTRawMatSizeCode.FieldName = "FTRawMatSizeCode"
        Me.BFTRawMatSizeCode.Name = "BFTRawMatSizeCode"
        Me.BFTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.BFTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.BFTRawMatSizeCode.Visible = True
        Me.BFTRawMatSizeCode.VisibleIndex = 4
        Me.BFTRawMatSizeCode.Width = 105
        '
        'CXFNHSysRawMatId
        '
        Me.CXFNHSysRawMatId.AppearanceCell.Options.UseTextOptions = True
        Me.CXFNHSysRawMatId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.CXFNHSysRawMatId.AppearanceHeader.Options.UseTextOptions = True
        Me.CXFNHSysRawMatId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.CXFNHSysRawMatId.Caption = "FNHSysRawMatId"
        Me.CXFNHSysRawMatId.DisplayFormat.FormatString = "{0:n0}"
        Me.CXFNHSysRawMatId.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CXFNHSysRawMatId.FieldName = "FNHSysRawMatId"
        Me.CXFNHSysRawMatId.Name = "CXFNHSysRawMatId"
        Me.CXFNHSysRawMatId.OptionsColumn.AllowEdit = False
        Me.CXFNHSysRawMatId.OptionsColumn.ReadOnly = True
        Me.CXFNHSysRawMatId.Width = 101
        '
        'ReposFNQuantity
        '
        Me.ReposFNQuantity.AutoHeight = False
        Me.ReposFNQuantity.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.ReposFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNQuantity.EditFormat.FormatString = "{0:n4}"
        Me.ReposFNQuantity.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNQuantity.Name = "ReposFNQuantity"
        '
        'ocmcreatepl
        '
        Me.ocmcreatepl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcreatepl.Location = New System.Drawing.Point(827, 8)
        Me.ocmcreatepl.Name = "ocmcreatepl"
        Me.ocmcreatepl.Size = New System.Drawing.Size(160, 25)
        Me.ocmcreatepl.TabIndex = 144
        Me.ocmcreatepl.TabStop = False
        Me.ocmcreatepl.Tag = "2|"
        Me.ocmcreatepl.Text = "CREATE PL"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(993, 8)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(160, 25)
        Me.ocmcancel.TabIndex = 145
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'CXFNBal
        '
        Me.CXFNBal.Caption = "จำนวนสต๊อก"
        Me.CXFNBal.DisplayFormat.FormatString = "{0:n4}"
        Me.CXFNBal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CXFNBal.FieldName = "FNBal"
        Me.CXFNBal.Name = "CXFNBal"
        Me.CXFNBal.OptionsColumn.AllowEdit = False
        Me.CXFNBal.OptionsColumn.ReadOnly = True
        Me.CXFNBal.Visible = True
        Me.CXFNBal.VisibleIndex = 5
        Me.CXFNBal.Width = 100
        '
        'CXFNConSmp
        '
        Me.CXFNConSmp.Caption = "Con Sump."
        Me.CXFNConSmp.DisplayFormat.FormatString = "{0:n4}"
        Me.CXFNConSmp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CXFNConSmp.FieldName = "FNConSmp"
        Me.CXFNConSmp.Name = "CXFNConSmp"
        Me.CXFNConSmp.OptionsColumn.AllowEdit = False
        Me.CXFNConSmp.OptionsColumn.ReadOnly = True
        Me.CXFNConSmp.Width = 100
        '
        'CXFNTotalUsed
        '
        Me.CXFNTotalUsed.Caption = "Total Used"
        Me.CXFNTotalUsed.DisplayFormat.FormatString = "{0:n4}"
        Me.CXFNTotalUsed.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CXFNTotalUsed.FieldName = "FNTotalUsed"
        Me.CXFNTotalUsed.Name = "CXFNTotalUsed"
        Me.CXFNTotalUsed.OptionsColumn.AllowEdit = False
        Me.CXFNTotalUsed.OptionsColumn.ReadOnly = True
        Me.CXFNTotalUsed.Visible = True
        Me.CXFNTotalUsed.VisibleIndex = 6
        Me.CXFNTotalUsed.Width = 100
        '
        'wDCPreparePackingListAddItems
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1162, 622)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmcreatepl)
        Me.Controls.Add(Me.ogcbarcode)
        Me.Controls.Add(Me.ogbnote)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wDCPreparePackingListAddItems"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DC Prepare Packing List Add Items"
        CType(Me.ogbnote, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbnote.ResumeLayout(False)
        CType(Me.FTSubOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTOrderNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FTStaSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNQuantity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbnote As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcbarcode As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvbarcode As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BFTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents BFTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CXFNHSysRawMatId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmcreatepl As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTStaSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ReposFNQuantity As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FTOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSubOrderNo As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTSubOrderNo_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents CXFNBal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CXFNConSmp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CXFNTotalUsed As DevExpress.XtraGrid.Columns.GridColumn
End Class
