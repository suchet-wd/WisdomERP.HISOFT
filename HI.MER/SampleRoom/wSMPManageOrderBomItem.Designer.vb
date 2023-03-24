<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wSMPManageOrderBomItem
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
        Me.ogcrcv = New DevExpress.XtraGrid.GridControl()
        Me.ogvrcv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FixFTStateSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ResFTStateSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FixFNHSysRawMatId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FixFTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FixFTMatDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FixFTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FixFTRawMatColorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FixFTRawMatSizeCodex = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FixFNTSysUnitId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FixFTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FixcxFTItemDataRef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FixcxFNHSysSuplId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FixcxFTSuplCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemGridLookUpEdit = New DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit()
        Me.RepositoryItemGridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.xFTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.xFTRawMatColorNameEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextColorname = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.RepositoryItemUsed = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ogb = New DevExpress.XtraEditors.GroupControl()
        Me.FTStaReceiveAll = New DevExpress.XtraEditors.CheckEdit()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmreceive = New DevExpress.XtraEditors.SimpleButton()
        Me.FixFTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogcrcv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvrcv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFTStateSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextColorname, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemUsed, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogb.SuspendLayout()
        CType(Me.FTStaReceiveAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcrcv
        '
        Me.ogcrcv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcrcv.Location = New System.Drawing.Point(5, 24)
        Me.ogcrcv.MainView = Me.ogvrcv
        Me.ogcrcv.Name = "ogcrcv"
        Me.ogcrcv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ResFTStateSelect, Me.RepositoryItemGridLookUpEdit, Me.RepositoryItemTextColorname, Me.RepositoryItemUsed})
        Me.ogcrcv.Size = New System.Drawing.Size(1384, 580)
        Me.ogcrcv.TabIndex = 3
        Me.ogcrcv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvrcv})
        '
        'ogvrcv
        '
        Me.ogvrcv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FixFTStateSelect, Me.FixFNHSysRawMatId, Me.FixFTRawMatCode, Me.FixFTMatDesc, Me.FixFTRawMatColorCode, Me.FixFTRawMatColorName, Me.FixFTRawMatSizeCodex, Me.FixFNTSysUnitId, Me.FixFTUnitCode, Me.FixcxFTItemDataRef, Me.FixcxFNHSysSuplId, Me.FixcxFTSuplCode})
        Me.ogvrcv.GridControl = Me.ogcrcv
        Me.ogvrcv.Name = "ogvrcv"
        Me.ogvrcv.OptionsCustomization.AllowGroup = False
        Me.ogvrcv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvrcv.OptionsView.ColumnAutoWidth = False
        Me.ogvrcv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvrcv.OptionsView.ShowAutoFilterRow = True
        Me.ogvrcv.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.ogvrcv.OptionsView.ShowGroupPanel = False
        '
        'FixFTStateSelect
        '
        Me.FixFTStateSelect.AppearanceCell.Options.UseTextOptions = True
        Me.FixFTStateSelect.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FixFTStateSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.FixFTStateSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FixFTStateSelect.Caption = " "
        Me.FixFTStateSelect.ColumnEdit = Me.ResFTStateSelect
        Me.FixFTStateSelect.FieldName = "FTSelect"
        Me.FixFTStateSelect.Name = "FixFTStateSelect"
        Me.FixFTStateSelect.OptionsColumn.AllowMove = False
        Me.FixFTStateSelect.OptionsColumn.ShowCaption = False
        Me.FixFTStateSelect.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FixFTStateSelect.Visible = True
        Me.FixFTStateSelect.VisibleIndex = 0
        Me.FixFTStateSelect.Width = 30
        '
        'ResFTStateSelect
        '
        Me.ResFTStateSelect.AutoHeight = False
        Me.ResFTStateSelect.Caption = "Check"
        Me.ResFTStateSelect.Name = "ResFTStateSelect"
        Me.ResFTStateSelect.ValueChecked = "1"
        Me.ResFTStateSelect.ValueUnchecked = "0"
        '
        'FixFNHSysRawMatId
        '
        Me.FixFNHSysRawMatId.Caption = "FNHSysRawMatId"
        Me.FixFNHSysRawMatId.FieldName = "FNHSysRawMatId"
        Me.FixFNHSysRawMatId.Name = "FixFNHSysRawMatId"
        Me.FixFNHSysRawMatId.OptionsColumn.AllowEdit = False
        Me.FixFNHSysRawMatId.OptionsColumn.ReadOnly = True
        '
        'FixFTRawMatCode
        '
        Me.FixFTRawMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FixFTRawMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FixFTRawMatCode.Caption = "FTRawMatCode"
        Me.FixFTRawMatCode.FieldName = "FTRawMatCode"
        Me.FixFTRawMatCode.Name = "FixFTRawMatCode"
        Me.FixFTRawMatCode.OptionsColumn.AllowEdit = False
        Me.FixFTRawMatCode.OptionsColumn.AllowMove = False
        Me.FixFTRawMatCode.OptionsColumn.ReadOnly = True
        Me.FixFTRawMatCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FixFTRawMatCode.Visible = True
        Me.FixFTRawMatCode.VisibleIndex = 1
        Me.FixFTRawMatCode.Width = 104
        '
        'FixFTMatDesc
        '
        Me.FixFTMatDesc.AppearanceHeader.Options.UseTextOptions = True
        Me.FixFTMatDesc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FixFTMatDesc.Caption = "FTMatDesc"
        Me.FixFTMatDesc.FieldName = "FTRawMatNameEN"
        Me.FixFTMatDesc.Name = "FixFTMatDesc"
        Me.FixFTMatDesc.OptionsColumn.AllowEdit = False
        Me.FixFTMatDesc.OptionsColumn.AllowMove = False
        Me.FixFTMatDesc.OptionsColumn.ReadOnly = True
        Me.FixFTMatDesc.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FixFTMatDesc.Visible = True
        Me.FixFTMatDesc.VisibleIndex = 2
        Me.FixFTMatDesc.Width = 300
        '
        'FixFTRawMatColorCode
        '
        Me.FixFTRawMatColorCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FixFTRawMatColorCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FixFTRawMatColorCode.Caption = "FTRawMatColorCode"
        Me.FixFTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.FixFTRawMatColorCode.Name = "FixFTRawMatColorCode"
        Me.FixFTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.FixFTRawMatColorCode.OptionsColumn.AllowMove = False
        Me.FixFTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.FixFTRawMatColorCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FixFTRawMatColorCode.Visible = True
        Me.FixFTRawMatColorCode.VisibleIndex = 3
        Me.FixFTRawMatColorCode.Width = 104
        '
        'FixFTRawMatColorName
        '
        Me.FixFTRawMatColorName.Caption = "Color Name"
        Me.FixFTRawMatColorName.FieldName = "FTRawMatColorName"
        Me.FixFTRawMatColorName.Name = "FixFTRawMatColorName"
        Me.FixFTRawMatColorName.OptionsColumn.AllowEdit = False
        Me.FixFTRawMatColorName.OptionsColumn.ReadOnly = True
        Me.FixFTRawMatColorName.Visible = True
        Me.FixFTRawMatColorName.VisibleIndex = 4
        Me.FixFTRawMatColorName.Width = 100
        '
        'FixFTRawMatSizeCodex
        '
        Me.FixFTRawMatSizeCodex.Caption = "Size Code"
        Me.FixFTRawMatSizeCodex.FieldName = "FTRawMatSizeCode"
        Me.FixFTRawMatSizeCodex.Name = "FixFTRawMatSizeCodex"
        Me.FixFTRawMatSizeCodex.OptionsColumn.AllowEdit = False
        Me.FixFTRawMatSizeCodex.OptionsColumn.ReadOnly = True
        Me.FixFTRawMatSizeCodex.Visible = True
        Me.FixFTRawMatSizeCodex.VisibleIndex = 5
        '
        'FixFNTSysUnitId
        '
        Me.FixFNTSysUnitId.Caption = "FNTSysUnitId"
        Me.FixFNTSysUnitId.FieldName = "FNHSysUnitId"
        Me.FixFNTSysUnitId.Name = "FixFNTSysUnitId"
        Me.FixFNTSysUnitId.OptionsColumn.AllowEdit = False
        Me.FixFNTSysUnitId.OptionsColumn.ReadOnly = True
        '
        'FixFTUnitCode
        '
        Me.FixFTUnitCode.AppearanceCell.Options.UseTextOptions = True
        Me.FixFTUnitCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FixFTUnitCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FixFTUnitCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FixFTUnitCode.Caption = "FTUnitCode"
        Me.FixFTUnitCode.FieldName = "FTUnitCode"
        Me.FixFTUnitCode.Name = "FixFTUnitCode"
        Me.FixFTUnitCode.OptionsColumn.AllowEdit = False
        Me.FixFTUnitCode.OptionsColumn.AllowMove = False
        Me.FixFTUnitCode.OptionsColumn.ReadOnly = True
        Me.FixFTUnitCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FixFTUnitCode.Visible = True
        Me.FixFTUnitCode.VisibleIndex = 6
        Me.FixFTUnitCode.Width = 71
        '
        'FixcxFTItemDataRef
        '
        Me.FixcxFTItemDataRef.Caption = "FTItemDataRef"
        Me.FixcxFTItemDataRef.FieldName = "FTItemDataRef"
        Me.FixcxFTItemDataRef.Name = "FixcxFTItemDataRef"
        '
        'FixcxFNHSysSuplId
        '
        Me.FixcxFNHSysSuplId.Caption = "FNHSysSuplId"
        Me.FixcxFNHSysSuplId.FieldName = "FNHSysSuplId"
        Me.FixcxFNHSysSuplId.Name = "FixcxFNHSysSuplId"
        '
        'FixcxFTSuplCode
        '
        Me.FixcxFTSuplCode.Caption = "Supplier"
        Me.FixcxFTSuplCode.FieldName = "FTSuplCode"
        Me.FixcxFTSuplCode.Name = "FixcxFTSuplCode"
        Me.FixcxFTSuplCode.OptionsColumn.AllowEdit = False
        Me.FixcxFTSuplCode.OptionsColumn.ReadOnly = True
        Me.FixcxFTSuplCode.Visible = True
        Me.FixcxFTSuplCode.VisibleIndex = 7
        '
        'RepositoryItemGridLookUpEdit
        '
        Me.RepositoryItemGridLookUpEdit.AutoHeight = False
        Me.RepositoryItemGridLookUpEdit.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemGridLookUpEdit.DisplayMember = "FTRawMatColorCode"
        Me.RepositoryItemGridLookUpEdit.Name = "RepositoryItemGridLookUpEdit"
        Me.RepositoryItemGridLookUpEdit.NullText = ""
        Me.RepositoryItemGridLookUpEdit.PopupView = Me.RepositoryItemGridLookUpEdit1View
        Me.RepositoryItemGridLookUpEdit.SearchMode = DevExpress.XtraEditors.Repository.GridLookUpSearchMode.None
        Me.RepositoryItemGridLookUpEdit.ValueMember = "FTRawMatColorCode"
        '
        'RepositoryItemGridLookUpEdit1View
        '
        Me.RepositoryItemGridLookUpEdit1View.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xFTRawMatColorCode, Me.xFTRawMatColorNameEN})
        Me.RepositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.RepositoryItemGridLookUpEdit1View.Name = "RepositoryItemGridLookUpEdit1View"
        Me.RepositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ColumnAutoWidth = False
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowAutoFilterRow = True
        Me.RepositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        Me.RepositoryItemGridLookUpEdit1View.Tag = ""
        '
        'xFTRawMatColorCode
        '
        Me.xFTRawMatColorCode.Caption = "Color Code"
        Me.xFTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.xFTRawMatColorCode.Name = "xFTRawMatColorCode"
        Me.xFTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.xFTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.xFTRawMatColorCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.xFTRawMatColorCode.Visible = True
        Me.xFTRawMatColorCode.VisibleIndex = 0
        Me.xFTRawMatColorCode.Width = 100
        '
        'xFTRawMatColorNameEN
        '
        Me.xFTRawMatColorNameEN.Caption = "ColorName"
        Me.xFTRawMatColorNameEN.FieldName = "FTRawMatColorName"
        Me.xFTRawMatColorNameEN.Name = "xFTRawMatColorNameEN"
        Me.xFTRawMatColorNameEN.OptionsColumn.AllowEdit = False
        Me.xFTRawMatColorNameEN.OptionsColumn.ReadOnly = True
        Me.xFTRawMatColorNameEN.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.xFTRawMatColorNameEN.Visible = True
        Me.xFTRawMatColorNameEN.VisibleIndex = 1
        Me.xFTRawMatColorNameEN.Width = 277
        '
        'RepositoryItemTextColorname
        '
        Me.RepositoryItemTextColorname.AutoHeight = False
        Me.RepositoryItemTextColorname.MaxLength = 200
        Me.RepositoryItemTextColorname.Name = "RepositoryItemTextColorname"
        '
        'RepositoryItemUsed
        '
        Me.RepositoryItemUsed.AutoHeight = False
        Me.RepositoryItemUsed.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemUsed.DisplayFormat.FormatString = "{0:n4}"
        Me.RepositoryItemUsed.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemUsed.EditFormat.FormatString = "{0:n4}"
        Me.RepositoryItemUsed.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemUsed.Name = "RepositoryItemUsed"
        '
        'ogb
        '
        Me.ogb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogb.Controls.Add(Me.FTStaReceiveAll)
        Me.ogb.Controls.Add(Me.ocmcancel)
        Me.ogb.Controls.Add(Me.ocmreceive)
        Me.ogb.Controls.Add(Me.ogcrcv)
        Me.ogb.Location = New System.Drawing.Point(2, 2)
        Me.ogb.Name = "ogb"
        Me.ogb.Size = New System.Drawing.Size(1394, 609)
        Me.ogb.TabIndex = 6
        Me.ogb.Tag = "2|"
        '
        'FTStaReceiveAll
        '
        Me.FTStaReceiveAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTStaReceiveAll.Location = New System.Drawing.Point(896, 1)
        Me.FTStaReceiveAll.Name = "FTStaReceiveAll"
        Me.FTStaReceiveAll.Properties.Appearance.Options.UseTextOptions = True
        Me.FTStaReceiveAll.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStaReceiveAll.Properties.Caption = "Receive All"
        Me.FTStaReceiveAll.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStaReceiveAll.Properties.ValueChecked = "1"
        Me.FTStaReceiveAll.Properties.ValueUnchecked = "0"
        Me.FTStaReceiveAll.Size = New System.Drawing.Size(151, 20)
        Me.FTStaReceiveAll.TabIndex = 102
        Me.FTStaReceiveAll.Tag = "2|"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(1227, 1)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(160, 20)
        Me.ocmcancel.TabIndex = 101
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmreceive
        '
        Me.ocmreceive.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmreceive.Location = New System.Drawing.Point(1059, 1)
        Me.ocmreceive.Name = "ocmreceive"
        Me.ocmreceive.Size = New System.Drawing.Size(160, 20)
        Me.ocmreceive.TabIndex = 100
        Me.ocmreceive.TabStop = False
        Me.ocmreceive.Tag = "2|"
        Me.ocmreceive.Text = "OK"
        '
        'FixFTRawMatSizeCode
        '
        Me.FixFTRawMatSizeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FixFTRawMatSizeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FixFTRawMatSizeCode.Caption = "FTRawMatSizeCode"
        Me.FixFTRawMatSizeCode.FieldName = "FTRawMatSizeCode"
        Me.FixFTRawMatSizeCode.Name = "FixFTRawMatSizeCode"
        Me.FixFTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.FixFTRawMatSizeCode.OptionsColumn.AllowMove = False
        Me.FixFTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.FixFTRawMatSizeCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FixFTRawMatSizeCode.Width = 94
        '
        'wSMPManageOrderBomItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1397, 614)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogb)
        Me.Name = "wSMPManageOrderBomItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Item From Bom"
        CType(Me.ogcrcv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvrcv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFTStateSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemGridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextColorname, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemUsed, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogb.ResumeLayout(False)
        CType(Me.FTStaReceiveAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcrcv As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvrcv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FixFNHSysRawMatId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FixFTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FixFTMatDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FixFTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FixFNTSysUnitId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FixFTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogb As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmreceive As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTStaReceiveAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FixFTStateSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ResFTStateSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FixcxFTItemDataRef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FixcxFNHSysSuplId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FixcxFTSuplCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemGridLookUpEdit As DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit
    Friend WithEvents RepositoryItemGridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents xFTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents xFTRawMatColorNameEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTextColorname As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents RepositoryItemUsed As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FixFTRawMatColorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FixFTRawMatSizeCodex As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FixFTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
End Class
