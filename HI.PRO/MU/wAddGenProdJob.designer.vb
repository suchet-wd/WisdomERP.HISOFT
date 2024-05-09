<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAddGenProdJob
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
        Dim EditorButtonImageOptions3 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject9 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject10 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject11 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject12 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions4 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject13 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject14 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject15 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject16 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.grpTableType = New DevExpress.XtraEditors.GroupControl()
        Me.ockTableManual = New DevExpress.XtraEditors.CheckEdit()
        Me.ockTableAuto = New DevExpress.XtraEditors.CheckEdit()
        Me.ogcMark = New DevExpress.XtraEditors.GroupControl()
        Me.ogcpart = New DevExpress.XtraGrid.GridControl()
        Me.ogvpart = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEditFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNLayerPerTable = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCalcEditFNLayerPerTable = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCalcEditFNTotalTable = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposCaleditWeight = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.ReposPartCode = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.ReposColorway = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl3 = New DevExpress.XtraEditors.GroupControl()
        CType(Me.grpTableType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTableType.SuspendLayout()
        CType(Me.ockTableManual.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ockTableAuto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcMark, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogcMark.SuspendLayout()
        CType(Me.ogcpart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvpart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEditFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEditFNLayerPerTable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEditFNTotalTable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposCaleditWeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposPartCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposColorway, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl3.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpTableType
        '
        Me.grpTableType.Controls.Add(Me.ockTableManual)
        Me.grpTableType.Controls.Add(Me.ockTableAuto)
        Me.grpTableType.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpTableType.Location = New System.Drawing.Point(0, 0)
        Me.grpTableType.Name = "grpTableType"
        Me.grpTableType.Size = New System.Drawing.Size(967, 86)
        Me.grpTableType.TabIndex = 0
        Me.grpTableType.Text = "ข้อมูลโต๊ะ"
        '
        'ockTableManual
        '
        Me.ockTableManual.Location = New System.Drawing.Point(375, 43)
        Me.ockTableManual.Name = "ockTableManual"
        Me.ockTableManual.Properties.Caption = "Manual"
        Me.ockTableManual.Size = New System.Drawing.Size(240, 24)
        Me.ockTableManual.TabIndex = 0
        '
        'ockTableAuto
        '
        Me.ockTableAuto.Location = New System.Drawing.Point(84, 43)
        Me.ockTableAuto.Name = "ockTableAuto"
        Me.ockTableAuto.Properties.Caption = "Auto"
        Me.ockTableAuto.Size = New System.Drawing.Size(240, 24)
        Me.ockTableAuto.TabIndex = 0
        '
        'ogcMark
        '
        Me.ogcMark.Controls.Add(Me.ogcpart)
        Me.ogcMark.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcMark.Location = New System.Drawing.Point(0, 86)
        Me.ogcMark.Name = "ogcMark"
        Me.ogcMark.Size = New System.Drawing.Size(967, 348)
        Me.ogcMark.TabIndex = 1
        Me.ogcMark.Text = "Mark"
        '
        'ogcpart
        '
        Me.ogcpart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcpart.Location = New System.Drawing.Point(2, 28)
        Me.ogcpart.MainView = Me.ogvpart
        Me.ogcpart.Name = "ogcpart"
        Me.ogcpart.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposCaleditWeight, Me.ReposPartCode, Me.ReposColorway, Me.RepositoryItemCheckEditFTSelect, Me.RepositoryItemCalcEditFNTotalTable, Me.RepositoryItemCalcEditFNLayerPerTable})
        Me.ogcpart.Size = New System.Drawing.Size(963, 318)
        Me.ogcpart.TabIndex = 0
        Me.ogcpart.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvpart})
        '
        'ogvpart
        '
        Me.ogvpart.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.FNLayerPerTable, Me.GridColumn5, Me.GridColumn4, Me.GridColumn6, Me.GridColumn7})
        Me.ogvpart.GridControl = Me.ogcpart
        Me.ogvpart.Name = "ogvpart"
        Me.ogvpart.OptionsView.ColumnAutoWidth = False
        Me.ogvpart.OptionsView.ShowGroupPanel = False
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "#"
        Me.GridColumn1.ColumnEdit = Me.RepositoryItemCheckEditFTSelect
        Me.GridColumn1.FieldName = "FTSelect"
        Me.GridColumn1.MinWidth = 25
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 56
        '
        'RepositoryItemCheckEditFTSelect
        '
        Me.RepositoryItemCheckEditFTSelect.AutoHeight = False
        Me.RepositoryItemCheckEditFTSelect.Name = "RepositoryItemCheckEditFTSelect"
        Me.RepositoryItemCheckEditFTSelect.ValueChecked = "1"
        Me.RepositoryItemCheckEditFTSelect.ValueUnchecked = "0"
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Mark"
        Me.GridColumn2.FieldName = "FTMarkCode"
        Me.GridColumn2.MinWidth = 25
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.OptionsColumn.AllowEdit = False
        Me.GridColumn2.Visible = True
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 189
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Layer"
        Me.GridColumn3.DisplayFormat.FormatString = "N0"
        Me.GridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn3.FieldName = "FNLayerQty"
        Me.GridColumn3.MinWidth = 25
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.OptionsColumn.AllowEdit = False
        Me.GridColumn3.Visible = True
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 180
        '
        'FNLayerPerTable
        '
        Me.FNLayerPerTable.Caption = "Layer Table"
        Me.FNLayerPerTable.ColumnEdit = Me.RepositoryItemCalcEditFNLayerPerTable
        Me.FNLayerPerTable.FieldName = "FNLayerPerTable"
        Me.FNLayerPerTable.MinWidth = 25
        Me.FNLayerPerTable.Name = "FNLayerPerTable"
        Me.FNLayerPerTable.Visible = True
        Me.FNLayerPerTable.VisibleIndex = 3
        Me.FNLayerPerTable.Width = 191
        '
        'RepositoryItemCalcEditFNLayerPerTable
        '
        Me.RepositoryItemCalcEditFNLayerPerTable.AutoHeight = False
        Me.RepositoryItemCalcEditFNLayerPerTable.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEditFNLayerPerTable.Name = "RepositoryItemCalcEditFNLayerPerTable"
        Me.RepositoryItemCalcEditFNLayerPerTable.Precision = 0
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Total Table"
        Me.GridColumn5.ColumnEdit = Me.RepositoryItemCalcEditFNTotalTable
        Me.GridColumn5.FieldName = "FNTotalTable"
        Me.GridColumn5.MinWidth = 25
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Visible = True
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 155
        '
        'RepositoryItemCalcEditFNTotalTable
        '
        Me.RepositoryItemCalcEditFNTotalTable.AutoHeight = False
        Me.RepositoryItemCalcEditFNTotalTable.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEditFNTotalTable.Name = "RepositoryItemCalcEditFNTotalTable"
        Me.RepositoryItemCalcEditFNTotalTable.Precision = 0
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "GridColumn4"
        Me.GridColumn4.FieldName = "FNActuallong"
        Me.GridColumn4.MinWidth = 25
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Width = 94
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "GridColumn6"
        Me.GridColumn6.FieldName = "FNInc"
        Me.GridColumn6.MinWidth = 25
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Width = 94
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "GridColumn7"
        Me.GridColumn7.FieldName = "FNYard"
        Me.GridColumn7.MinWidth = 25
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Width = 94
        '
        'ReposCaleditWeight
        '
        Me.ReposCaleditWeight.AutoHeight = False
        Me.ReposCaleditWeight.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposCaleditWeight.DisplayFormat.FormatString = "N0"
        Me.ReposCaleditWeight.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ReposCaleditWeight.Name = "ReposCaleditWeight"
        '
        'ReposPartCode
        '
        Me.ReposPartCode.AutoHeight = False
        Me.ReposPartCode.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, EditorButtonImageOptions3, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject9, SerializableAppearanceObject10, SerializableAppearanceObject11, SerializableAppearanceObject12, "", "596", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.ReposPartCode.Name = "ReposPartCode"
        '
        'ReposColorway
        '
        Me.ReposColorway.AutoHeight = False
        Me.ReposColorway.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, EditorButtonImageOptions4, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject13, SerializableAppearanceObject14, SerializableAppearanceObject15, SerializableAppearanceObject16, "", "597", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.ReposColorway.Name = "ReposColorway"
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(824, 9)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(103, 31)
        Me.ocmcancel.TabIndex = 5
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmok
        '
        Me.ocmok.Location = New System.Drawing.Point(670, 9)
        Me.ocmok.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(103, 31)
        Me.ocmok.TabIndex = 4
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'GroupControl3
        '
        Me.GroupControl3.Controls.Add(Me.ocmcancel)
        Me.GroupControl3.Controls.Add(Me.ocmok)
        Me.GroupControl3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupControl3.Location = New System.Drawing.Point(0, 434)
        Me.GroupControl3.Name = "GroupControl3"
        Me.GroupControl3.ShowCaption = False
        Me.GroupControl3.Size = New System.Drawing.Size(967, 46)
        Me.GroupControl3.TabIndex = 2
        Me.GroupControl3.Text = "GroupControl3"
        '
        'wAddGenProdJob
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(967, 480)
        Me.Controls.Add(Me.ogcMark)
        Me.Controls.Add(Me.GroupControl3)
        Me.Controls.Add(Me.grpTableType)
        Me.Name = "wAddGenProdJob"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Gen Prod Job"
        CType(Me.grpTableType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTableType.ResumeLayout(False)
        CType(Me.ockTableManual.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ockTableAuto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcMark, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogcMark.ResumeLayout(False)
        CType(Me.ogcpart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvpart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEditFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEditFNLayerPerTable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEditFNTotalTable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposCaleditWeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposPartCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposColorway, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents grpTableType As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcMark As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcpart As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvpart As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ReposCaleditWeight As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ReposPartCode As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents ReposColorway As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents GroupControl3 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ockTableManual As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ockTableAuto As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNLayerPerTable As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEditFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemCalcEditFNLayerPerTable As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepositoryItemCalcEditFNTotalTable As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
End Class
