<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wRecievePopup
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ogcDetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTMECPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysDrugId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysDrugUnitId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDrugCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDrugName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDrugUnitCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTDrugUnitName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNRcvQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCalcEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.cFNRcvHistory = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNRcvBal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNHSysDrugUnitIdTo = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmselect = New DevExpress.XtraEditors.SimpleButton()
        Me.oSelectAll = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNHSysDrugUnitIdTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.ogcDetail)
        Me.GroupControl1.Controls.Add(Me.ocmcancel)
        Me.GroupControl1.Controls.Add(Me.ocmselect)
        Me.GroupControl1.Controls.Add(Me.oSelectAll)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(948, 380)
        Me.GroupControl1.TabIndex = 0
        '
        'ogcDetail
        '
        Me.ogcDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcDetail.Location = New System.Drawing.Point(2, 21)
        Me.ogcDetail.MainView = Me.ogvDetail
        Me.ogcDetail.Name = "ogcDetail"
        Me.ogcDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTSelect, Me.ReposFNHSysDrugUnitIdTo, Me.RepositoryItemCalcEdit1})
        Me.ogcDetail.Size = New System.Drawing.Size(944, 357)
        Me.ogcDetail.TabIndex = 2
        Me.ogcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDetail})
        '
        'ogvDetail
        '
        Me.ogvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTSelect, Me.cFTMECPurchaseNo, Me.cFNHSysDrugId, Me.cFNHSysDrugUnitId, Me.cFNQuantity, Me.cFTDrugCode, Me.cFTDrugName, Me.cFTDrugUnitCode, Me.cFTDrugUnitName, Me.cFNRcvQty, Me.cFNRcvHistory, Me.cFNRcvBal})
        Me.ogvDetail.GridControl = Me.ogcDetail
        Me.ogvDetail.Name = "ogvDetail"
        '
        'cFTSelect
        '
        Me.cFTSelect.Caption = "FTSelect"
        Me.cFTSelect.ColumnEdit = Me.RepositoryFTSelect
        Me.cFTSelect.FieldName = "FTSelect"
        Me.cFTSelect.Name = "cFTSelect"
        Me.cFTSelect.Visible = True
        Me.cFTSelect.VisibleIndex = 0
        Me.cFTSelect.Width = 63
        '
        'RepositoryFTSelect
        '
        Me.RepositoryFTSelect.AutoHeight = False
        Me.RepositoryFTSelect.Caption = "Check"
        Me.RepositoryFTSelect.Name = "RepositoryFTSelect"
        Me.RepositoryFTSelect.ValueChecked = "1"
        Me.RepositoryFTSelect.ValueUnchecked = "0"
        '
        'cFTMECPurchaseNo
        '
        Me.cFTMECPurchaseNo.Caption = "FTMECPurchaseNo"
        Me.cFTMECPurchaseNo.FieldName = "FTMECPurchaseNo"
        Me.cFTMECPurchaseNo.Name = "cFTMECPurchaseNo"
        Me.cFTMECPurchaseNo.OptionsColumn.AllowEdit = False
        Me.cFTMECPurchaseNo.Width = 122
        '
        'cFNHSysDrugId
        '
        Me.cFNHSysDrugId.Caption = "FNHSysDrugId"
        Me.cFNHSysDrugId.FieldName = "FNHSysDrugId"
        Me.cFNHSysDrugId.Name = "cFNHSysDrugId"
        Me.cFNHSysDrugId.OptionsColumn.AllowEdit = False
        Me.cFNHSysDrugId.Width = 114
        '
        'cFNHSysDrugUnitId
        '
        Me.cFNHSysDrugUnitId.Caption = "FNHSysDrugUnitId"
        Me.cFNHSysDrugUnitId.FieldName = "FNHSysDrugUnitId"
        Me.cFNHSysDrugUnitId.Name = "cFNHSysDrugUnitId"
        Me.cFNHSysDrugUnitId.OptionsColumn.AllowEdit = False
        Me.cFNHSysDrugUnitId.Width = 114
        '
        'cFNQuantity
        '
        Me.cFNQuantity.Caption = "FNQuantity"
        Me.cFNQuantity.DisplayFormat.FormatString = "{0:n4}"
        Me.cFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQuantity.FieldName = "FNQuantity"
        Me.cFNQuantity.Name = "cFNQuantity"
        Me.cFNQuantity.OptionsColumn.AllowEdit = False
        Me.cFNQuantity.Visible = True
        Me.cFNQuantity.VisibleIndex = 5
        Me.cFNQuantity.Width = 128
        '
        'cFTDrugCode
        '
        Me.cFTDrugCode.Caption = "FTDrugCode"
        Me.cFTDrugCode.FieldName = "FTDrugCode"
        Me.cFTDrugCode.Name = "cFTDrugCode"
        Me.cFTDrugCode.OptionsColumn.AllowEdit = False
        Me.cFTDrugCode.Visible = True
        Me.cFTDrugCode.VisibleIndex = 1
        Me.cFTDrugCode.Width = 143
        '
        'cFTDrugName
        '
        Me.cFTDrugName.Caption = "FTDrugName"
        Me.cFTDrugName.FieldName = "FTDrugName"
        Me.cFTDrugName.Name = "cFTDrugName"
        Me.cFTDrugName.OptionsColumn.AllowEdit = False
        Me.cFTDrugName.Visible = True
        Me.cFTDrugName.VisibleIndex = 2
        Me.cFTDrugName.Width = 206
        '
        'cFTDrugUnitCode
        '
        Me.cFTDrugUnitCode.Caption = "FTDrugUnitCode"
        Me.cFTDrugUnitCode.FieldName = "FTDrugUnitCode"
        Me.cFTDrugUnitCode.Name = "cFTDrugUnitCode"
        Me.cFTDrugUnitCode.OptionsColumn.AllowEdit = False
        Me.cFTDrugUnitCode.Visible = True
        Me.cFTDrugUnitCode.VisibleIndex = 3
        Me.cFTDrugUnitCode.Width = 133
        '
        'cFTDrugUnitName
        '
        Me.cFTDrugUnitName.Caption = "FTDrugUnitName"
        Me.cFTDrugUnitName.FieldName = "FTDrugUnitName"
        Me.cFTDrugUnitName.Name = "cFTDrugUnitName"
        Me.cFTDrugUnitName.OptionsColumn.AllowEdit = False
        Me.cFTDrugUnitName.Visible = True
        Me.cFTDrugUnitName.VisibleIndex = 4
        Me.cFTDrugUnitName.Width = 193
        '
        'cFNRcvQty
        '
        Me.cFNRcvQty.Caption = "FNRcvQty"
        Me.cFNRcvQty.ColumnEdit = Me.RepositoryItemCalcEdit1
        Me.cFNRcvQty.DisplayFormat.FormatString = "{0:n4}"
        Me.cFNRcvQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNRcvQty.FieldName = "FNRcvQty"
        Me.cFNRcvQty.Name = "cFNRcvQty"
        Me.cFNRcvQty.Visible = True
        Me.cFNRcvQty.VisibleIndex = 8
        Me.cFNRcvQty.Width = 178
        '
        'RepositoryItemCalcEdit1
        '
        Me.RepositoryItemCalcEdit1.AutoHeight = False
        Me.RepositoryItemCalcEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEdit1.Name = "RepositoryItemCalcEdit1"
        Me.RepositoryItemCalcEdit1.Precision = 4
        '
        'cFNRcvHistory
        '
        Me.cFNRcvHistory.Caption = "FNRcvHistory"
        Me.cFNRcvHistory.DisplayFormat.FormatString = "{0:n4}"
        Me.cFNRcvHistory.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNRcvHistory.FieldName = "FNRcvHistory"
        Me.cFNRcvHistory.Name = "cFNRcvHistory"
        Me.cFNRcvHistory.OptionsColumn.AllowEdit = False
        Me.cFNRcvHistory.Visible = True
        Me.cFNRcvHistory.VisibleIndex = 6
        Me.cFNRcvHistory.Width = 126
        '
        'cFNRcvBal
        '
        Me.cFNRcvBal.Caption = "FNRcvBal"
        Me.cFNRcvBal.DisplayFormat.FormatString = "{0:n4}"
        Me.cFNRcvBal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNRcvBal.FieldName = "FNRcvBal"
        Me.cFNRcvBal.Name = "cFNRcvBal"
        Me.cFNRcvBal.OptionsColumn.AllowEdit = False
        Me.cFNRcvBal.Visible = True
        Me.cFNRcvBal.VisibleIndex = 7
        Me.cFNRcvBal.Width = 134
        '
        'ReposFNHSysDrugUnitIdTo
        '
        Me.ReposFNHSysDrugUnitIdTo.AutoHeight = False
        Me.ReposFNHSysDrugUnitIdTo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "261", Nothing, True)})
        Me.ReposFNHSysDrugUnitIdTo.Name = "ReposFNHSysDrugUnitIdTo"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(869, 0)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(75, 19)
        Me.ocmcancel.TabIndex = 1
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmselect
        '
        Me.ocmselect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmselect.Location = New System.Drawing.Point(788, 0)
        Me.ocmselect.Name = "ocmselect"
        Me.ocmselect.Size = New System.Drawing.Size(75, 19)
        Me.ocmselect.TabIndex = 1
        Me.ocmselect.Text = "OK"
        '
        'oSelectAll
        '
        Me.oSelectAll.Location = New System.Drawing.Point(0, 0)
        Me.oSelectAll.Name = "oSelectAll"
        Me.oSelectAll.Properties.Caption = "CheckEdit1"
        Me.oSelectAll.Size = New System.Drawing.Size(209, 19)
        Me.oSelectAll.TabIndex = 0
        Me.oSelectAll.Visible = False
        '
        'wRecievePopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(948, 380)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupControl1)
        Me.Name = "wRecievePopup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wRecievePopup"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNHSysDrugUnitIdTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmselect As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents oSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTMECPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNHSysDrugId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNHSysDrugUnitId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDrugCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDrugName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDrugUnitCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTDrugUnitName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNRcvQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNRcvHistory As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNRcvBal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNHSysDrugUnitIdTo As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents RepositoryItemCalcEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
End Class
