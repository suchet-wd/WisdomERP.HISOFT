<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wRequestItemPopup
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogcbarcode = New DevExpress.XtraGrid.GridControl()
        Me.ogvbarcode = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTAssetCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTAssetName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposReposFNReserveQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FNPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNPrice = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FNHSysUnitAssetId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepoFNHSysUnitAssetId = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.FNDisPer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNDisPer = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FNDisAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNDisAmt = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDocumentNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNNetAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNNetAmt = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ReposFNReserveQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ogb = New DevExpress.XtraEditors.GroupControl()
        Me.FTStaReceiveAll = New DevExpress.XtraEditors.CheckEdit()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogcbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposReposFNReserveQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepoFNHSysUnitAssetId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNDisPer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNDisAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNNetAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNReserveQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogb.SuspendLayout()
        CType(Me.FTStaReceiveAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcbarcode
        '
        Me.ogcbarcode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcbarcode.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcbarcode.Location = New System.Drawing.Point(6, 30)
        Me.ogcbarcode.MainView = Me.ogvbarcode
        Me.ogcbarcode.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcbarcode.Name = "ogcbarcode"
        Me.ogcbarcode.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFTSelect, Me.ReposFNReserveQty, Me.ReposReposFNReserveQty, Me.ReposFNPrice, Me.RepoFNHSysUnitAssetId, Me.ReposFNDisPer, Me.ReposFNDisAmt, Me.ReposFNNetAmt})
        Me.ogcbarcode.Size = New System.Drawing.Size(1397, 498)
        Me.ogcbarcode.TabIndex = 3
        Me.ogcbarcode.TabStop = False
        Me.ogcbarcode.Tag = "2|"
        Me.ogcbarcode.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvbarcode})
        '
        'ogvbarcode
        '
        Me.ogvbarcode.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.FTAssetCode, Me.FTAssetName, Me.FNQuantity, Me.FNPrice, Me.FNHSysUnitAssetId, Me.FNDisPer, Me.FNDisAmt, Me.FTRemark, Me.FTDocumentNo, Me.FNNetAmt})
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
        Me.FTSelect.AppearanceCell.BackColor = System.Drawing.Color.LightCyan
        Me.FTSelect.AppearanceCell.Options.UseBackColor = True
        Me.FTSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSelect.Caption = " "
        Me.FTSelect.ColumnEdit = Me.ReposFTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.OptionsColumn.AllowMove = False
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 33
        '
        'ReposFTSelect
        '
        Me.ReposFTSelect.AutoHeight = False
        Me.ReposFTSelect.Caption = "Check"
        Me.ReposFTSelect.Name = "ReposFTSelect"
        Me.ReposFTSelect.ValueChecked = "1"
        Me.ReposFTSelect.ValueUnchecked = "0"
        '
        'FTAssetCode
        '
        Me.FTAssetCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTAssetCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAssetCode.Caption = "FTAssetCode"
        Me.FTAssetCode.FieldName = "FTAssetCode"
        Me.FTAssetCode.Name = "FTAssetCode"
        Me.FTAssetCode.OptionsColumn.AllowEdit = False
        Me.FTAssetCode.OptionsColumn.AllowMove = False
        Me.FTAssetCode.OptionsColumn.ReadOnly = True
        Me.FTAssetCode.Visible = True
        Me.FTAssetCode.VisibleIndex = 1
        Me.FTAssetCode.Width = 122
        '
        'FTAssetName
        '
        Me.FTAssetName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTAssetName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAssetName.Caption = "FTAssetName"
        Me.FTAssetName.FieldName = "FTAssetName"
        Me.FTAssetName.Name = "FTAssetName"
        Me.FTAssetName.OptionsColumn.AllowEdit = False
        Me.FTAssetName.OptionsColumn.AllowMove = False
        Me.FTAssetName.OptionsColumn.FixedWidth = True
        Me.FTAssetName.OptionsColumn.ReadOnly = True
        Me.FTAssetName.Visible = True
        Me.FTAssetName.VisibleIndex = 2
        Me.FTAssetName.Width = 242
        '
        'FNQuantity
        '
        Me.FNQuantity.Caption = "FNQuantity"
        Me.FNQuantity.ColumnEdit = Me.ReposReposFNReserveQty
        Me.FNQuantity.DisplayFormat.FormatString = "N4"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNQuantity"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowMove = False
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 3
        Me.FNQuantity.Width = 90
        '
        'ReposReposFNReserveQty
        '
        Me.ReposReposFNReserveQty.AutoHeight = False
        Me.ReposReposFNReserveQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposReposFNReserveQty.DisplayFormat.FormatString = "N4"
        Me.ReposReposFNReserveQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposReposFNReserveQty.EditFormat.FormatString = "N4"
        Me.ReposReposFNReserveQty.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposReposFNReserveQty.Name = "ReposReposFNReserveQty"
        '
        'FNPrice
        '
        Me.FNPrice.AppearanceHeader.Options.UseTextOptions = True
        Me.FNPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNPrice.Caption = "FNPrice"
        Me.FNPrice.ColumnEdit = Me.ReposFNPrice
        Me.FNPrice.DisplayFormat.FormatString = "N4"
        Me.FNPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPrice.FieldName = "FNPrice"
        Me.FNPrice.Name = "FNPrice"
        Me.FNPrice.OptionsColumn.AllowMove = False
        Me.FNPrice.OptionsColumn.FixedWidth = True
        Me.FNPrice.Visible = True
        Me.FNPrice.VisibleIndex = 4
        Me.FNPrice.Width = 80
        '
        'ReposFNPrice
        '
        Me.ReposFNPrice.AutoHeight = False
        Me.ReposFNPrice.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNPrice.DisplayFormat.FormatString = "N4"
        Me.ReposFNPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNPrice.EditFormat.FormatString = "N4"
        Me.ReposFNPrice.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNPrice.Name = "ReposFNPrice"
        '
        'FNHSysUnitAssetId
        '
        Me.FNHSysUnitAssetId.AppearanceHeader.Options.UseTextOptions = True
        Me.FNHSysUnitAssetId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNHSysUnitAssetId.Caption = "FNHSysUnitAssetId"
        Me.FNHSysUnitAssetId.ColumnEdit = Me.RepoFNHSysUnitAssetId
        Me.FNHSysUnitAssetId.FieldName = "FNHSysUnitAssetId"
        Me.FNHSysUnitAssetId.Name = "FNHSysUnitAssetId"
        Me.FNHSysUnitAssetId.Visible = True
        Me.FNHSysUnitAssetId.VisibleIndex = 5
        Me.FNHSysUnitAssetId.Width = 115
        '
        'RepoFNHSysUnitAssetId
        '
        Me.RepoFNHSysUnitAssetId.AutoHeight = False
        Me.RepoFNHSysUnitAssetId.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "449", Nothing, True)})
        Me.RepoFNHSysUnitAssetId.Name = "RepoFNHSysUnitAssetId"
        '
        'FNDisPer
        '
        Me.FNDisPer.Caption = "FNDisPer"
        Me.FNDisPer.ColumnEdit = Me.ReposFNDisPer
        Me.FNDisPer.DisplayFormat.FormatString = "N4"
        Me.FNDisPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNDisPer.FieldName = "FNDisPer"
        Me.FNDisPer.Name = "FNDisPer"
        Me.FNDisPer.OptionsColumn.AllowMove = False
        Me.FNDisPer.Visible = True
        Me.FNDisPer.VisibleIndex = 6
        '
        'ReposFNDisPer
        '
        Me.ReposFNDisPer.AutoHeight = False
        Me.ReposFNDisPer.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNDisPer.DisplayFormat.FormatString = "N4"
        Me.ReposFNDisPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNDisPer.EditFormat.FormatString = "N4"
        Me.ReposFNDisPer.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNDisPer.Name = "ReposFNDisPer"
        '
        'FNDisAmt
        '
        Me.FNDisAmt.Caption = "FNDisAmt"
        Me.FNDisAmt.ColumnEdit = Me.ReposFNDisAmt
        Me.FNDisAmt.DisplayFormat.FormatString = "N4"
        Me.FNDisAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNDisAmt.FieldName = "FNDisAmt"
        Me.FNDisAmt.MaxWidth = 70
        Me.FNDisAmt.Name = "FNDisAmt"
        Me.FNDisAmt.OptionsColumn.AllowMove = False
        Me.FNDisAmt.Visible = True
        Me.FNDisAmt.VisibleIndex = 7
        Me.FNDisAmt.Width = 70
        '
        'ReposFNDisAmt
        '
        Me.ReposFNDisAmt.AutoHeight = False
        Me.ReposFNDisAmt.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNDisAmt.DisplayFormat.FormatString = "N4"
        Me.ReposFNDisAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNDisAmt.EditFormat.FormatString = "N4"
        Me.ReposFNDisAmt.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNDisAmt.Name = "ReposFNDisAmt"
        '
        'FTRemark
        '
        Me.FTRemark.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRemark.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRemark.Caption = "FTRemark"
        Me.FTRemark.FieldName = "FTRemark"
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.OptionsColumn.AllowMove = False
        Me.FTRemark.OptionsColumn.FixedWidth = True
        Me.FTRemark.Visible = True
        Me.FTRemark.VisibleIndex = 9
        Me.FTRemark.Width = 199
        '
        'FTDocumentNo
        '
        Me.FTDocumentNo.Caption = "FTDocumentNo"
        Me.FTDocumentNo.FieldName = "FTDocumentNo"
        Me.FTDocumentNo.Name = "FTDocumentNo"
        '
        'FNNetAmt
        '
        Me.FNNetAmt.Caption = "FNNetAmt"
        Me.FNNetAmt.ColumnEdit = Me.ReposFNNetAmt
        Me.FNNetAmt.DisplayFormat.FormatString = "N4"
        Me.FNNetAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNNetAmt.FieldName = "FNNetAmt"
        Me.FNNetAmt.Name = "FNNetAmt"
        Me.FNNetAmt.OptionsColumn.AllowMove = False
        Me.FNNetAmt.Visible = True
        Me.FNNetAmt.VisibleIndex = 8
        Me.FNNetAmt.Width = 120
        '
        'ReposFNNetAmt
        '
        Me.ReposFNNetAmt.AutoHeight = False
        Me.ReposFNNetAmt.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNNetAmt.Name = "ReposFNNetAmt"
        '
        'ReposFNReserveQty
        '
        Me.ReposFNReserveQty.AutoHeight = False
        Me.ReposFNReserveQty.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNReserveQty.DisplayFormat.FormatString = "N4"
        Me.ReposFNReserveQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNReserveQty.EditFormat.FormatString = "N4"
        Me.ReposFNReserveQty.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposFNReserveQty.Name = "ReposFNReserveQty"
        '
        'ogb
        '
        Me.ogb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogb.Controls.Add(Me.FTStaReceiveAll)
        Me.ogb.Controls.Add(Me.ocmcancel)
        Me.ogb.Controls.Add(Me.ocmok)
        Me.ogb.Controls.Add(Me.ogcbarcode)
        Me.ogb.Location = New System.Drawing.Point(1, 2)
        Me.ogb.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogb.Name = "ogb"
        Me.ogb.Size = New System.Drawing.Size(1403, 533)
        Me.ogb.TabIndex = 4
        Me.ogb.Text = "Detail"
        '
        'FTStaReceiveAll
        '
        Me.FTStaReceiveAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTStaReceiveAll.Location = New System.Drawing.Point(805, 1)
        Me.FTStaReceiveAll.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FTStaReceiveAll.Name = "FTStaReceiveAll"
        Me.FTStaReceiveAll.Properties.Appearance.Options.UseTextOptions = True
        Me.FTStaReceiveAll.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStaReceiveAll.Properties.Caption = "Receive All"
        Me.FTStaReceiveAll.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTStaReceiveAll.Properties.ValueChecked = "1"
        Me.FTStaReceiveAll.Properties.ValueUnchecked = "0"
        Me.FTStaReceiveAll.Size = New System.Drawing.Size(176, 20)
        Me.FTStaReceiveAll.TabIndex = 105
        Me.FTStaReceiveAll.Tag = "2|"
        Me.FTStaReceiveAll.Visible = False
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(1199, 1)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(187, 25)
        Me.ocmcancel.TabIndex = 104
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmok
        '
        Me.ocmok.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmok.Location = New System.Drawing.Point(995, 1)
        Me.ocmok.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(187, 25)
        Me.ocmok.TabIndex = 103
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'wRequestItemPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1407, 538)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wRequestItemPopup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Request Item"
        CType(Me.ogcbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposReposFNReserveQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepoFNHSysUnitAssetId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNDisPer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNDisAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNNetAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNReserveQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogb.ResumeLayout(False)
        CType(Me.FTStaReceiveAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcbarcode As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvbarcode As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FNHSysUnitAssetId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAssetCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAssetName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNReserveQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ogb As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTStaReceiveAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTDocumentNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNNetAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNDisPer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNDisAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposReposFNReserveQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ReposFNPrice As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepoFNHSysUnitAssetId As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents ReposFNDisPer As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ReposFNDisAmt As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ReposFNNetAmt As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
End Class
