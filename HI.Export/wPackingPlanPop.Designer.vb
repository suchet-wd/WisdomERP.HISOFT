<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPackingPlanPop
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
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcref = New DevExpress.XtraGrid.GridControl()
        Me.ogvref = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTPckPlanNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTShipCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysStyleId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oSelectAll = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.ogcref, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvref, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(545, 655)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(169, 33)
        Me.ocmcancel.TabIndex = 533
        Me.ocmcancel.Text = "CANCEL"
        '
        'ocmsave
        '
        Me.ocmsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmsave.Location = New System.Drawing.Point(62, 655)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(169, 33)
        Me.ocmsave.TabIndex = 532
        Me.ocmsave.Text = "Select"
        '
        'ogcref
        '
        Me.ogcref.Location = New System.Drawing.Point(3, 40)
        Me.ogcref.MainView = Me.ogvref
        Me.ogcref.Name = "ogcref"
        Me.ogcref.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemFTSelect})
        Me.ogcref.Size = New System.Drawing.Size(764, 608)
        Me.ogcref.TabIndex = 534
        Me.ogcref.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvref})
        '
        'ogvref
        '
        Me.ogvref.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.FTPckPlanNo, Me.FTPORef, Me.FTShipCode, Me.FTDescription, Me.FNHSysStyleId})
        Me.ogvref.GridControl = Me.ogcref
        Me.ogvref.Name = "ogvref"
        Me.ogvref.OptionsView.ColumnAutoWidth = False
        Me.ogvref.OptionsView.ShowGroupPanel = False
        '
        'FTSelect
        '
        Me.FTSelect.Caption = "FTSelect"
        Me.FTSelect.ColumnEdit = Me.RepositoryItemFTSelect
        Me.FTSelect.FieldName = "FTSelect"
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        Me.FTSelect.Width = 65
        '
        'RepositoryItemFTSelect
        '
        Me.RepositoryItemFTSelect.AutoHeight = False
        Me.RepositoryItemFTSelect.Name = "RepositoryItemFTSelect"
        Me.RepositoryItemFTSelect.ValueChecked = "1"
        Me.RepositoryItemFTSelect.ValueUnchecked = "0"
        '
        'FTPckPlanNo
        '
        Me.FTPckPlanNo.Caption = "FTPckPlanNo"
        Me.FTPckPlanNo.FieldName = "FTPckPlanNo"
        Me.FTPckPlanNo.Name = "FTPckPlanNo"
        Me.FTPckPlanNo.OptionsColumn.AllowEdit = False
        Me.FTPckPlanNo.Visible = True
        Me.FTPckPlanNo.VisibleIndex = 1
        Me.FTPckPlanNo.Width = 128
        '
        'FTPORef
        '
        Me.FTPORef.Caption = "FTPORef"
        Me.FTPORef.FieldName = "FTPORef"
        Me.FTPORef.Name = "FTPORef"
        Me.FTPORef.OptionsColumn.AllowEdit = False
        Me.FTPORef.Visible = True
        Me.FTPORef.VisibleIndex = 2
        Me.FTPORef.Width = 116
        '
        'FTShipCode
        '
        Me.FTShipCode.Caption = "FTShipCode"
        Me.FTShipCode.FieldName = "FTProvinceCode"
        Me.FTShipCode.Name = "FTShipCode"
        Me.FTShipCode.OptionsColumn.AllowEdit = False
        Me.FTShipCode.Visible = True
        Me.FTShipCode.VisibleIndex = 3
        Me.FTShipCode.Width = 116
        '
        'FTDescription
        '
        Me.FTDescription.Caption = "FTDescription"
        Me.FTDescription.FieldName = "FTDescription"
        Me.FTDescription.Name = "FTDescription"
        Me.FTDescription.OptionsColumn.AllowEdit = False
        Me.FTDescription.Visible = True
        Me.FTDescription.VisibleIndex = 4
        Me.FTDescription.Width = 243
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Caption = "FNHSysStyleId"
        Me.FNHSysStyleId.FieldName = "FNHSysStyleId"
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.OptionsColumn.AllowEdit = False
        '
        'oSelectAll
        '
        Me.oSelectAll.Location = New System.Drawing.Point(13, 9)
        Me.oSelectAll.Name = "oSelectAll"
        Me.oSelectAll.Properties.AutoHeight = False
        Me.oSelectAll.Properties.Caption = "Select All"
        Me.oSelectAll.Size = New System.Drawing.Size(240, 20)
        Me.oSelectAll.TabIndex = 535
        '
        'wPackingPlanPop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(769, 701)
        Me.ControlBox = False
        Me.Controls.Add(Me.oSelectAll)
        Me.Controls.Add(Me.ogcref)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmsave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wPackingPlanPop"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Packing Pland Refer"
        CType(Me.ogcref, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvref, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.oSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcref As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvref As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTPckPlanNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTShipCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FNHSysStyleId As DevExpress.XtraGrid.Columns.GridColumn
End Class
