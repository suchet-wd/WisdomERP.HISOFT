<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wDCPrepareCFMToCarAppWH
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogclistdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvlistdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ReposFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ReposFNReserveQty = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ogb = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.cxFNHSysWHIdSC = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxFTWHCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxFNHSysCmpId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxFTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cxFNHSysWHIdTo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNHSysWHIdTo = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.cxFNHSysWHIdTo_Hide = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogclistdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvlistdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNReserveQty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogb.SuspendLayout()
        CType(Me.ReposFNHSysWHIdTo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogclistdetail
        '
        Me.ogclistdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogclistdetail.Location = New System.Drawing.Point(5, 24)
        Me.ogclistdetail.MainView = Me.ogvlistdetail
        Me.ogclistdetail.Name = "ogclistdetail"
        Me.ogclistdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFTSelect, Me.ReposFNReserveQty, Me.ReposFNHSysWHIdTo})
        Me.ogclistdetail.Size = New System.Drawing.Size(499, 468)
        Me.ogclistdetail.TabIndex = 3
        Me.ogclistdetail.TabStop = False
        Me.ogclistdetail.Tag = "3|"
        Me.ogclistdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvlistdetail})
        '
        'ogvlistdetail
        '
        Me.ogvlistdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cxFNHSysWHIdSC, Me.cxFTWHCode, Me.cxFNHSysCmpId, Me.cxFTCmpCode, Me.cxFNHSysWHIdTo, Me.cxFNHSysWHIdTo_Hide})
        Me.ogvlistdetail.GridControl = Me.ogclistdetail
        Me.ogvlistdetail.Name = "ogvlistdetail"
        Me.ogvlistdetail.OptionsCustomization.AllowGroup = False
        Me.ogvlistdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvlistdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvlistdetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvlistdetail.OptionsView.ShowGroupPanel = False
        Me.ogvlistdetail.Tag = "2|"
        '
        'ReposFTSelect
        '
        Me.ReposFTSelect.AutoHeight = False
        Me.ReposFTSelect.Caption = "Check"
        Me.ReposFTSelect.Name = "ReposFTSelect"
        Me.ReposFTSelect.ValueChecked = "1"
        Me.ReposFTSelect.ValueUnchecked = "0"
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
        Me.ogb.Controls.Add(Me.ocmcancel)
        Me.ogb.Controls.Add(Me.ocmok)
        Me.ogb.Controls.Add(Me.ogclistdetail)
        Me.ogb.Location = New System.Drawing.Point(1, 2)
        Me.ogb.Name = "ogb"
        Me.ogb.Size = New System.Drawing.Size(504, 496)
        Me.ogb.TabIndex = 4
        Me.ogb.Text = "List Detail"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(369, 1)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(120, 20)
        Me.ocmcancel.TabIndex = 104
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmok
        '
        Me.ocmok.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmok.Location = New System.Drawing.Point(234, 1)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(117, 20)
        Me.ocmok.TabIndex = 103
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'cxFNHSysWHIdSC
        '
        Me.cxFNHSysWHIdSC.Caption = "คลังต้นทาง"
        Me.cxFNHSysWHIdSC.FieldName = "FNHSysWHIdSC"
        Me.cxFNHSysWHIdSC.Name = "cxFNHSysWHIdSC"
        Me.cxFNHSysWHIdSC.OptionsColumn.AllowEdit = False
        Me.cxFNHSysWHIdSC.OptionsColumn.ReadOnly = True
        Me.cxFNHSysWHIdSC.Width = 117
        '
        'cxFTWHCode
        '
        Me.cxFTWHCode.Caption = "คลังต้นทาง"
        Me.cxFTWHCode.FieldName = "FTWHCode"
        Me.cxFTWHCode.Name = "cxFTWHCode"
        Me.cxFTWHCode.OptionsColumn.AllowEdit = False
        Me.cxFTWHCode.OptionsColumn.ReadOnly = True
        Me.cxFTWHCode.Visible = True
        Me.cxFTWHCode.VisibleIndex = 0
        Me.cxFTWHCode.Width = 97
        '
        'cxFNHSysCmpId
        '
        Me.cxFNHSysCmpId.Caption = "FNHSysCmpId"
        Me.cxFNHSysCmpId.FieldName = "FNHSysCmpId"
        Me.cxFNHSysCmpId.Name = "cxFNHSysCmpId"
        Me.cxFNHSysCmpId.OptionsColumn.AllowEdit = False
        Me.cxFNHSysCmpId.OptionsColumn.ReadOnly = True
        '
        'cxFTCmpCode
        '
        Me.cxFTCmpCode.Caption = "สาขาปลายทาง"
        Me.cxFTCmpCode.FieldName = "FTCmpCode"
        Me.cxFTCmpCode.Name = "cxFTCmpCode"
        Me.cxFTCmpCode.OptionsColumn.AllowEdit = False
        Me.cxFTCmpCode.OptionsColumn.ReadOnly = True
        Me.cxFTCmpCode.Visible = True
        Me.cxFTCmpCode.VisibleIndex = 1
        Me.cxFTCmpCode.Width = 128
        '
        'cxFNHSysWHIdTo
        '
        Me.cxFNHSysWHIdTo.Caption = "คลังปลายทาง"
        Me.cxFNHSysWHIdTo.ColumnEdit = Me.ReposFNHSysWHIdTo
        Me.cxFNHSysWHIdTo.FieldName = "FNHSysWHIdTo"
        Me.cxFNHSysWHIdTo.Name = "cxFNHSysWHIdTo"
        Me.cxFNHSysWHIdTo.Visible = True
        Me.cxFNHSysWHIdTo.VisibleIndex = 2
        Me.cxFNHSysWHIdTo.Width = 161
        '
        'ReposFNHSysWHIdTo
        '
        Me.ReposFNHSysWHIdTo.AutoHeight = False
        Me.ReposFNHSysWHIdTo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "124", Nothing, True)})
        Me.ReposFNHSysWHIdTo.Name = "ReposFNHSysWHIdTo"
        '
        'cxFNHSysWHIdTo_Hide
        '
        Me.cxFNHSysWHIdTo_Hide.Caption = "FNHSysWHIdTo_Hide"
        Me.cxFNHSysWHIdTo_Hide.FieldName = "FNHSysWHIdTo_Hide"
        Me.cxFNHSysWHIdTo_Hide.Name = "cxFNHSysWHIdTo_Hide"
        Me.cxFNHSysWHIdTo_Hide.OptionsColumn.AllowEdit = False
        Me.cxFNHSysWHIdTo_Hide.OptionsColumn.ReadOnly = True
        '
        'wDCPrepareCFMToCarAppWH
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(507, 500)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wDCPrepareCFMToCarAppWH"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Destination Warehouse"
        CType(Me.ogclistdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvlistdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNReserveQty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogb.ResumeLayout(False)
        CType(Me.ReposFNHSysWHIdTo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogclistdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvlistdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ReposFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ReposFNReserveQty As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ogb As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cxFNHSysWHIdSC As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxFTWHCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxFNHSysCmpId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxFTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cxFNHSysWHIdTo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNHSysWHIdTo As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents cxFNHSysWHIdTo_Hide As DevExpress.XtraGrid.Columns.GridColumn
End Class
