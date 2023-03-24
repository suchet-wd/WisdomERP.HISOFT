<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wMapColorAndSizeImportOrderTarget
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbMapSizeImportOrder = New DevExpress.XtraEditors.GroupControl()
        Me.ogdMapSizeImport = New DevExpress.XtraGrid.GridControl()
        Me.ogvMapSizeImport = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTColor = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysMatColorId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNHSysMatColorId = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.FNHSysMatSizeId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNHSysMatSizeId = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.FNHSysMatColorId_Hide = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysMatSizeId_Hide = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogbConfirmMapSizeImportOrder = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbMapSizeImportOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbMapSizeImportOrder.SuspendLayout()
        CType(Me.ogdMapSizeImport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvMapSizeImport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNHSysMatColorId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNHSysMatSizeId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbConfirmMapSizeImportOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbConfirmMapSizeImportOrder.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbMapSizeImportOrder
        '
        Me.ogbMapSizeImportOrder.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbMapSizeImportOrder.Controls.Add(Me.ogdMapSizeImport)
        Me.ogbMapSizeImportOrder.Location = New System.Drawing.Point(5, 2)
        Me.ogbMapSizeImportOrder.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbMapSizeImportOrder.Name = "ogbMapSizeImportOrder"
        Me.ogbMapSizeImportOrder.Size = New System.Drawing.Size(701, 297)
        Me.ogbMapSizeImportOrder.TabIndex = 0
        Me.ogbMapSizeImportOrder.Text = "Mapping Color Way And Size Breakdown Import Order No"
        '
        'ogdMapSizeImport
        '
        Me.ogdMapSizeImport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdMapSizeImport.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdMapSizeImport.Location = New System.Drawing.Point(2, 24)
        Me.ogdMapSizeImport.MainView = Me.ogvMapSizeImport
        Me.ogdMapSizeImport.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdMapSizeImport.Name = "ogdMapSizeImport"
        Me.ogdMapSizeImport.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFNHSysMatColorId, Me.ReposFNHSysMatSizeId})
        Me.ogdMapSizeImport.Size = New System.Drawing.Size(697, 271)
        Me.ogdMapSizeImport.TabIndex = 0
        Me.ogdMapSizeImport.Tag = "3|"
        Me.ogdMapSizeImport.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvMapSizeImport})
        '
        'ogvMapSizeImport
        '
        Me.ogvMapSizeImport.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTColor, Me.FNHSysMatColorId, Me.FNHSysMatSizeId, Me.FNHSysMatColorId_Hide, Me.FNHSysMatSizeId_Hide})
        Me.ogvMapSizeImport.GridControl = Me.ogdMapSizeImport
        Me.ogvMapSizeImport.Name = "ogvMapSizeImport"
        Me.ogvMapSizeImport.OptionsView.ShowGroupPanel = False
        Me.ogvMapSizeImport.Tag = "3|"
        '
        'FTColor
        '
        Me.FTColor.AppearanceHeader.Options.UseTextOptions = True
        Me.FTColor.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTColor.Caption = "Color"
        Me.FTColor.FieldName = "FTColor"
        Me.FTColor.Name = "FTColor"
        Me.FTColor.OptionsColumn.AllowEdit = False
        Me.FTColor.Visible = True
        Me.FTColor.VisibleIndex = 0
        Me.FTColor.Width = 363
        '
        'FNHSysMatColorId
        '
        Me.FNHSysMatColorId.Caption = "Color Way"
        Me.FNHSysMatColorId.ColumnEdit = Me.ReposFNHSysMatColorId
        Me.FNHSysMatColorId.FieldName = "FNHSysMatColorId"
        Me.FNHSysMatColorId.Name = "FNHSysMatColorId"
        Me.FNHSysMatColorId.Visible = True
        Me.FNHSysMatColorId.VisibleIndex = 1
        Me.FNHSysMatColorId.Width = 157
        '
        'ReposFNHSysMatColorId
        '
        Me.ReposFNHSysMatColorId.AutoHeight = False
        Me.ReposFNHSysMatColorId.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "185", Nothing, True)})
        Me.ReposFNHSysMatColorId.Name = "ReposFNHSysMatColorId"
        Me.ReposFNHSysMatColorId.Tag = "185"
        '
        'FNHSysMatSizeId
        '
        Me.FNHSysMatSizeId.AppearanceHeader.Options.UseTextOptions = True
        Me.FNHSysMatSizeId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNHSysMatSizeId.Caption = "Size Breakdown"
        Me.FNHSysMatSizeId.ColumnEdit = Me.ReposFNHSysMatSizeId
        Me.FNHSysMatSizeId.FieldName = "FNHSysMatSizeId"
        Me.FNHSysMatSizeId.Name = "FNHSysMatSizeId"
        Me.FNHSysMatSizeId.Visible = True
        Me.FNHSysMatSizeId.VisibleIndex = 2
        Me.FNHSysMatSizeId.Width = 159
        '
        'ReposFNHSysMatSizeId
        '
        Me.ReposFNHSysMatSizeId.AutoHeight = False
        Me.ReposFNHSysMatSizeId.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "186", Nothing, True)})
        Me.ReposFNHSysMatSizeId.Name = "ReposFNHSysMatSizeId"
        Me.ReposFNHSysMatSizeId.Tag = "186"
        '
        'FNHSysMatColorId_Hide
        '
        Me.FNHSysMatColorId_Hide.Caption = "FNHSysMatColorId"
        Me.FNHSysMatColorId_Hide.FieldName = "FNHSysMatColorId_Hide"
        Me.FNHSysMatColorId_Hide.Name = "FNHSysMatColorId_Hide"
        '
        'FNHSysMatSizeId_Hide
        '
        Me.FNHSysMatSizeId_Hide.Caption = "FNHSysMatSizeId"
        Me.FNHSysMatSizeId_Hide.FieldName = "FNHSysMatSizeId_Hide"
        Me.FNHSysMatSizeId_Hide.Name = "FNHSysMatSizeId_Hide"
        '
        'ogbConfirmMapSizeImportOrder
        '
        Me.ogbConfirmMapSizeImportOrder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbConfirmMapSizeImportOrder.Controls.Add(Me.ocmcancel)
        Me.ogbConfirmMapSizeImportOrder.Controls.Add(Me.ocmok)
        Me.ogbConfirmMapSizeImportOrder.Location = New System.Drawing.Point(5, 303)
        Me.ogbConfirmMapSizeImportOrder.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbConfirmMapSizeImportOrder.Name = "ogbConfirmMapSizeImportOrder"
        Me.ogbConfirmMapSizeImportOrder.ShowCaption = False
        Me.ogbConfirmMapSizeImportOrder.Size = New System.Drawing.Size(701, 49)
        Me.ogbConfirmMapSizeImportOrder.TabIndex = 1
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(402, 9)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(169, 31)
        Me.ocmcancel.TabIndex = 3
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmok
        '
        Me.ocmok.Location = New System.Drawing.Point(129, 9)
        Me.ocmok.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(155, 31)
        Me.ocmok.TabIndex = 2
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'wMapColorAndSizeImportOrderTarget
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(712, 357)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogbConfirmMapSizeImportOrder)
        Me.Controls.Add(Me.ogbMapSizeImportOrder)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wMapColorAndSizeImportOrderTarget"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Mapping Color Way And Size Breakdown Import Order No"
        CType(Me.ogbMapSizeImportOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbMapSizeImportOrder.ResumeLayout(False)
        CType(Me.ogdMapSizeImport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvMapSizeImport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNHSysMatColorId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNHSysMatSizeId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbConfirmMapSizeImportOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbConfirmMapSizeImportOrder.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbMapSizeImportOrder As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbConfirmMapSizeImportOrder As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogdMapSizeImport As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvMapSizeImport As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTColor As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysMatSizeId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysMatColorId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNHSysMatColorId As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents ReposFNHSysMatSizeId As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents FNHSysMatColorId_Hide As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysMatSizeId_Hide As DevExpress.XtraGrid.Columns.GridColumn
End Class
