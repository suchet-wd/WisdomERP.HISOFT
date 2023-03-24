<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wSMPCreateOrderSampleBomItem
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
        Me.FTStateSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ResFTStateSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FNHSysRawMatId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMatDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRawMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTSysUnitId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxFTItemDataRef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogb = New DevExpress.XtraEditors.GroupControl()
        Me.FTStaReceiveAll = New DevExpress.XtraEditors.CheckEdit()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmreceive = New DevExpress.XtraEditors.SimpleButton()
        Me.cxFNHSysSuplId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxFTSuplCode = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogcrcv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvrcv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ResFTStateSelect, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogcrcv.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ResFTStateSelect})
        Me.ogcrcv.Size = New System.Drawing.Size(1030, 390)
        Me.ogcrcv.TabIndex = 3
        Me.ogcrcv.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvrcv})
        '
        'ogvrcv
        '
        Me.ogvrcv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTStateSelect, Me.FNHSysRawMatId, Me.FTRawMatCode, Me.FTMatDesc, Me.FTRawMatColorCode, Me.FTRawMatSizeCode, Me.FNTSysUnitId, Me.FTUnitCode, Me.cxFTItemDataRef, Me.cxFNHSysSuplId, Me.cxFTSuplCode})
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
        'FTStateSelect
        '
        Me.FTStateSelect.AppearanceCell.Options.UseTextOptions = True
        Me.FTStateSelect.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStateSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStateSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStateSelect.Caption = " "
        Me.FTStateSelect.ColumnEdit = Me.ResFTStateSelect
        Me.FTStateSelect.FieldName = "FTSelect"
        Me.FTStateSelect.Name = "FTStateSelect"
        Me.FTStateSelect.OptionsColumn.AllowMove = False
        Me.FTStateSelect.OptionsColumn.ShowCaption = False
        Me.FTStateSelect.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTStateSelect.Visible = True
        Me.FTStateSelect.VisibleIndex = 0
        Me.FTStateSelect.Width = 30
        '
        'ResFTStateSelect
        '
        Me.ResFTStateSelect.AutoHeight = False
        Me.ResFTStateSelect.Caption = "Check"
        Me.ResFTStateSelect.Name = "ResFTStateSelect"
        Me.ResFTStateSelect.ValueChecked = "1"
        Me.ResFTStateSelect.ValueUnchecked = "0"
        '
        'FNHSysRawMatId
        '
        Me.FNHSysRawMatId.Caption = "FNHSysRawMatId"
        Me.FNHSysRawMatId.FieldName = "FNHSysRawMatId"
        Me.FNHSysRawMatId.Name = "FNHSysRawMatId"
        Me.FNHSysRawMatId.OptionsColumn.AllowEdit = False
        Me.FNHSysRawMatId.OptionsColumn.ReadOnly = True
        '
        'FTRawMatCode
        '
        Me.FTRawMatCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatCode.Caption = "FTRawMatCode"
        Me.FTRawMatCode.FieldName = "FTRawMatCode"
        Me.FTRawMatCode.Name = "FTRawMatCode"
        Me.FTRawMatCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatCode.OptionsColumn.AllowMove = False
        Me.FTRawMatCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTRawMatCode.Visible = True
        Me.FTRawMatCode.VisibleIndex = 1
        Me.FTRawMatCode.Width = 104
        '
        'FTMatDesc
        '
        Me.FTMatDesc.AppearanceHeader.Options.UseTextOptions = True
        Me.FTMatDesc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTMatDesc.Caption = "FTMatDesc"
        Me.FTMatDesc.FieldName = "FTRawMatNameEN"
        Me.FTMatDesc.Name = "FTMatDesc"
        Me.FTMatDesc.OptionsColumn.AllowEdit = False
        Me.FTMatDesc.OptionsColumn.AllowMove = False
        Me.FTMatDesc.OptionsColumn.ReadOnly = True
        Me.FTMatDesc.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTMatDesc.Visible = True
        Me.FTMatDesc.VisibleIndex = 2
        Me.FTMatDesc.Width = 447
        '
        'FTRawMatColorCode
        '
        Me.FTRawMatColorCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatColorCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatColorCode.Caption = "FTRawMatColorCode"
        Me.FTRawMatColorCode.FieldName = "FTRawMatColorCode"
        Me.FTRawMatColorCode.Name = "FTRawMatColorCode"
        Me.FTRawMatColorCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatColorCode.OptionsColumn.AllowMove = False
        Me.FTRawMatColorCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatColorCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTRawMatColorCode.Visible = True
        Me.FTRawMatColorCode.VisibleIndex = 3
        Me.FTRawMatColorCode.Width = 104
        '
        'FTRawMatSizeCode
        '
        Me.FTRawMatSizeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRawMatSizeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRawMatSizeCode.Caption = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.FieldName = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.Name = "FTRawMatSizeCode"
        Me.FTRawMatSizeCode.OptionsColumn.AllowEdit = False
        Me.FTRawMatSizeCode.OptionsColumn.AllowMove = False
        Me.FTRawMatSizeCode.OptionsColumn.ReadOnly = True
        Me.FTRawMatSizeCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTRawMatSizeCode.Visible = True
        Me.FTRawMatSizeCode.VisibleIndex = 4
        Me.FTRawMatSizeCode.Width = 94
        '
        'FNTSysUnitId
        '
        Me.FNTSysUnitId.Caption = "FNTSysUnitId"
        Me.FNTSysUnitId.FieldName = "FNHSysUnitId"
        Me.FNTSysUnitId.Name = "FNTSysUnitId"
        Me.FNTSysUnitId.OptionsColumn.AllowEdit = False
        Me.FNTSysUnitId.OptionsColumn.ReadOnly = True
        '
        'FTUnitCode
        '
        Me.FTUnitCode.AppearanceCell.Options.UseTextOptions = True
        Me.FTUnitCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUnitCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitCode.Caption = "FTUnitCode"
        Me.FTUnitCode.FieldName = "FTUnitCode"
        Me.FTUnitCode.Name = "FTUnitCode"
        Me.FTUnitCode.OptionsColumn.AllowEdit = False
        Me.FTUnitCode.OptionsColumn.AllowMove = False
        Me.FTUnitCode.OptionsColumn.ReadOnly = True
        Me.FTUnitCode.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
        Me.FTUnitCode.Visible = True
        Me.FTUnitCode.VisibleIndex = 5
        Me.FTUnitCode.Width = 71
        '
        'cxFTItemDataRef
        '
        Me.cxFTItemDataRef.Caption = "FTItemDataRef"
        Me.cxFTItemDataRef.FieldName = "FTItemDataRef"
        Me.cxFTItemDataRef.Name = "cxFTItemDataRef"
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
        Me.ogb.Size = New System.Drawing.Size(1040, 419)
        Me.ogb.TabIndex = 6
        Me.ogb.Tag = "2|"
        '
        'FTStaReceiveAll
        '
        Me.FTStaReceiveAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTStaReceiveAll.Location = New System.Drawing.Point(542, 1)
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
        Me.ocmcancel.Location = New System.Drawing.Point(873, 1)
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
        Me.ocmreceive.Location = New System.Drawing.Point(705, 1)
        Me.ocmreceive.Name = "ocmreceive"
        Me.ocmreceive.Size = New System.Drawing.Size(160, 20)
        Me.ocmreceive.TabIndex = 100
        Me.ocmreceive.TabStop = False
        Me.ocmreceive.Tag = "2|"
        Me.ocmreceive.Text = "OK"
        '
        'cxFNHSysSuplId
        '
        Me.cxFNHSysSuplId.Caption = "FNHSysSuplId"
        Me.cxFNHSysSuplId.FieldName = "FNHSysSuplId"
        Me.cxFNHSysSuplId.Name = "cxFNHSysSuplId"
        '
        'cxFTSuplCode
        '
        Me.cxFTSuplCode.Caption = "Supplier"
        Me.cxFTSuplCode.FieldName = "FTSuplCode"
        Me.cxFTSuplCode.Name = "cxFTSuplCode"
        Me.cxFTSuplCode.OptionsColumn.AllowEdit = False
        Me.cxFTSuplCode.OptionsColumn.ReadOnly = True
        Me.cxFTSuplCode.Visible = True
        Me.cxFTSuplCode.VisibleIndex = 6
        '
        'wSMPCreateOrderSampleBomItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1043, 424)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wSMPCreateOrderSampleBomItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Item From Bom"
        CType(Me.ogcrcv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvrcv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ResFTStateSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogb.ResumeLayout(False)
        CType(Me.FTStaReceiveAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcrcv As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvrcv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FNHSysRawMatId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMatDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRawMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTSysUnitId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogb As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmreceive As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTStaReceiveAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FTStateSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ResFTStateSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cxFTItemDataRef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxFNHSysSuplId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxFTSuplCode As DevExpress.XtraGrid.Columns.GridColumn
End Class
