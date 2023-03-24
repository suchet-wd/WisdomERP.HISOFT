<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wAddMainMat
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
        Me.FNHSysStyleId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysSeasonId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSeasonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMainMatSpecCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMainMatSpecTH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMainMatSpecEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.oSelectAll = New DevExpress.XtraEditors.CheckEdit()
        CType(Me.ogcref, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvref, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.oSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(875, 663)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(169, 33)
        Me.ocmcancel.TabIndex = 533
        Me.ocmcancel.Text = "CLOSE"
        '
        'ocmsave
        '
        Me.ocmsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmsave.Location = New System.Drawing.Point(62, 663)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(169, 33)
        Me.ocmsave.TabIndex = 532
        Me.ocmsave.Text = "SAVE"
        '
        'ogcref
        '
        Me.ogcref.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcref.Location = New System.Drawing.Point(3, 4)
        Me.ogcref.MainView = Me.ogvref
        Me.ogcref.Name = "ogcref"
        Me.ogcref.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemFTSelect})
        Me.ogcref.Size = New System.Drawing.Size(1094, 646)
        Me.ogcref.TabIndex = 534
        Me.ogcref.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvref})
        '
        'ogvref
        '
        Me.ogvref.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNHSysStyleId, Me.FNHSysSeasonId, Me.FTStyleCode, Me.FTSeasonCode, Me.FTMainMatSpecCode, Me.FTMainMatSpecTH, Me.FTMainMatSpecEN, Me.cFTRemark})
        Me.ogvref.GridControl = Me.ogcref
        Me.ogvref.Name = "ogvref"
        Me.ogvref.OptionsView.ColumnAutoWidth = False
        Me.ogvref.OptionsView.ShowGroupPanel = False
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Caption = "FNHSysStyleId"
        Me.FNHSysStyleId.FieldName = "FNHSysStyleId"
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.OptionsColumn.AllowEdit = False
        '
        'FNHSysSeasonId
        '
        Me.FNHSysSeasonId.Caption = "FNHSysSeasonId"
        Me.FNHSysSeasonId.FieldName = "FNHSysSeasonId"
        Me.FNHSysSeasonId.Name = "FNHSysSeasonId"
        '
        'FTStyleCode
        '
        Me.FTStyleCode.Caption = "FTStyleCode"
        Me.FTStyleCode.FieldName = "FTStyleCode"
        Me.FTStyleCode.Name = "FTStyleCode"
        Me.FTStyleCode.OptionsColumn.AllowEdit = False
        Me.FTStyleCode.Visible = True
        Me.FTStyleCode.VisibleIndex = 0
        Me.FTStyleCode.Width = 111
        '
        'FTSeasonCode
        '
        Me.FTSeasonCode.Caption = "FTSeasonCode"
        Me.FTSeasonCode.FieldName = "FTSeasonCode"
        Me.FTSeasonCode.Name = "FTSeasonCode"
        Me.FTSeasonCode.OptionsColumn.AllowEdit = False
        Me.FTSeasonCode.Width = 109
        '
        'FTMainMatSpecCode
        '
        Me.FTMainMatSpecCode.Caption = "FTMainMatSpecCode"
        Me.FTMainMatSpecCode.FieldName = "FTMainMatSpecCode"
        Me.FTMainMatSpecCode.Name = "FTMainMatSpecCode"
        Me.FTMainMatSpecCode.Visible = True
        Me.FTMainMatSpecCode.VisibleIndex = 1
        Me.FTMainMatSpecCode.Width = 190
        '
        'FTMainMatSpecTH
        '
        Me.FTMainMatSpecTH.Caption = "FTMainMatSpecTH"
        Me.FTMainMatSpecTH.FieldName = "FTMainMatSpecTH"
        Me.FTMainMatSpecTH.Name = "FTMainMatSpecTH"
        Me.FTMainMatSpecTH.Visible = True
        Me.FTMainMatSpecTH.VisibleIndex = 2
        Me.FTMainMatSpecTH.Width = 285
        '
        'FTMainMatSpecEN
        '
        Me.FTMainMatSpecEN.Caption = "FTMainMatSpecEN"
        Me.FTMainMatSpecEN.FieldName = "FTMainMatSpecEN"
        Me.FTMainMatSpecEN.Name = "FTMainMatSpecEN"
        Me.FTMainMatSpecEN.Visible = True
        Me.FTMainMatSpecEN.VisibleIndex = 3
        Me.FTMainMatSpecEN.Width = 292
        '
        'cFTRemark
        '
        Me.cFTRemark.Caption = "H.S.Code"
        Me.cFTRemark.FieldName = "FTNote"
        Me.cFTRemark.Name = "cFTRemark"
        Me.cFTRemark.Visible = True
        Me.cFTRemark.VisibleIndex = 4
        Me.cFTRemark.Width = 115
        '
        'RepositoryItemFTSelect
        '
        Me.RepositoryItemFTSelect.AutoHeight = False
        Me.RepositoryItemFTSelect.Name = "RepositoryItemFTSelect"
        Me.RepositoryItemFTSelect.ValueChecked = "1"
        Me.RepositoryItemFTSelect.ValueUnchecked = "0"
        '
        'oSelectAll
        '
        Me.oSelectAll.Location = New System.Drawing.Point(261, 670)
        Me.oSelectAll.Name = "oSelectAll"
        Me.oSelectAll.Properties.AutoHeight = False
        Me.oSelectAll.Properties.Caption = "Select All"
        Me.oSelectAll.Size = New System.Drawing.Size(240, 20)
        Me.oSelectAll.TabIndex = 535
        Me.oSelectAll.Visible = False
        '
        'wAddMainMat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1099, 703)
        Me.ControlBox = False
        Me.Controls.Add(Me.oSelectAll)
        Me.Controls.Add(Me.ogcref)
        Me.Controls.Add(Me.ocmcancel)
        Me.Controls.Add(Me.ocmsave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "wAddMainMat"
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
    Friend WithEvents RepositoryItemFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents oSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents FNHSysStyleId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysSeasonId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSeasonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMainMatSpecCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMainMatSpecTH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMainMatSpecEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTRemark As DevExpress.XtraGrid.Columns.GridColumn
End Class
