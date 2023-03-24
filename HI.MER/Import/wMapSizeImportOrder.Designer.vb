<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wMapSizeImportOrder
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
        Me.ogbMapSizeImportOrder = New DevExpress.XtraEditors.GroupControl()
        Me.ogdMapSizeImport = New DevExpress.XtraGrid.GridControl()
        Me.ogvMapSizeImport = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.oColFTSizeCodeNotExists = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTMapSizeExtend = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemFTMapSizeExtend = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.ogbConfirmMapSizeImportOrder = New DevExpress.XtraEditors.GroupControl()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbMapSizeImportOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbMapSizeImportOrder.SuspendLayout()
        CType(Me.ogdMapSizeImport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvMapSizeImport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTMapSizeExtend, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogbMapSizeImportOrder.Location = New System.Drawing.Point(4, 2)
        Me.ogbMapSizeImportOrder.Name = "ogbMapSizeImportOrder"
        Me.ogbMapSizeImportOrder.Size = New System.Drawing.Size(601, 241)
        Me.ogbMapSizeImportOrder.TabIndex = 0
        Me.ogbMapSizeImportOrder.Text = "Map Size (cause size code not match in system master file)"
        '
        'ogdMapSizeImport
        '
        Me.ogdMapSizeImport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdMapSizeImport.Location = New System.Drawing.Point(2, 20)
        Me.ogdMapSizeImport.MainView = Me.ogvMapSizeImport
        Me.ogdMapSizeImport.Name = "ogdMapSizeImport"
        Me.ogdMapSizeImport.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemFTMapSizeExtend})
        Me.ogdMapSizeImport.Size = New System.Drawing.Size(597, 219)
        Me.ogdMapSizeImport.TabIndex = 0
        Me.ogdMapSizeImport.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvMapSizeImport})
        '
        'ogvMapSizeImport
        '
        Me.ogvMapSizeImport.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.oColFTSizeCodeNotExists, Me.oColFTMapSizeExtend})
        Me.ogvMapSizeImport.GridControl = Me.ogdMapSizeImport
        Me.ogvMapSizeImport.Name = "ogvMapSizeImport"
        Me.ogvMapSizeImport.OptionsView.ShowGroupPanel = False
        '
        'oColFTSizeCodeNotExists
        '
        Me.oColFTSizeCodeNotExists.AppearanceHeader.Options.UseTextOptions = True
        Me.oColFTSizeCodeNotExists.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.oColFTSizeCodeNotExists.Caption = "FTSizeCodeNotExists"
        Me.oColFTSizeCodeNotExists.FieldName = "FTSizeCodeNotExists"
        Me.oColFTSizeCodeNotExists.Name = "oColFTSizeCodeNotExists"
        Me.oColFTSizeCodeNotExists.OptionsColumn.AllowEdit = False
        Me.oColFTSizeCodeNotExists.Visible = True
        Me.oColFTSizeCodeNotExists.VisibleIndex = 0
        '
        'oColFTMapSizeExtend
        '
        Me.oColFTMapSizeExtend.AppearanceHeader.Options.UseTextOptions = True
        Me.oColFTMapSizeExtend.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.oColFTMapSizeExtend.Caption = "FTMapSizeExtend"
        Me.oColFTMapSizeExtend.ColumnEdit = Me.RepositoryItemFTMapSizeExtend
        Me.oColFTMapSizeExtend.FieldName = "FTMapSizeExtend"
        Me.oColFTMapSizeExtend.Name = "oColFTMapSizeExtend"
        Me.oColFTMapSizeExtend.Visible = True
        Me.oColFTMapSizeExtend.VisibleIndex = 1
        '
        'RepositoryItemFTMapSizeExtend
        '
        Me.RepositoryItemFTMapSizeExtend.AutoHeight = False
        Me.RepositoryItemFTMapSizeExtend.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemFTMapSizeExtend.Name = "RepositoryItemFTMapSizeExtend"
        Me.RepositoryItemFTMapSizeExtend.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        '
        'ogbConfirmMapSizeImportOrder
        '
        Me.ogbConfirmMapSizeImportOrder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbConfirmMapSizeImportOrder.Controls.Add(Me.ocmcancel)
        Me.ogbConfirmMapSizeImportOrder.Controls.Add(Me.ocmok)
        Me.ogbConfirmMapSizeImportOrder.Location = New System.Drawing.Point(4, 246)
        Me.ogbConfirmMapSizeImportOrder.Name = "ogbConfirmMapSizeImportOrder"
        Me.ogbConfirmMapSizeImportOrder.ShowCaption = False
        Me.ogbConfirmMapSizeImportOrder.Size = New System.Drawing.Size(601, 40)
        Me.ogbConfirmMapSizeImportOrder.TabIndex = 1
        '
        'ocmcancel
        '
        Me.ocmcancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmcancel.Location = New System.Drawing.Point(345, 7)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(145, 25)
        Me.ocmcancel.TabIndex = 3
        Me.ocmcancel.TabStop = False
        Me.ocmcancel.Tag = "2|"
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmok
        '
        Me.ocmok.Location = New System.Drawing.Point(111, 7)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(133, 25)
        Me.ocmok.TabIndex = 2
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'wMapSizeImportOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(610, 290)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogbConfirmMapSizeImportOrder)
        Me.Controls.Add(Me.ogbMapSizeImportOrder)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wMapSizeImportOrder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Map Size Import Order No"
        CType(Me.ogbMapSizeImportOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbMapSizeImportOrder.ResumeLayout(False)
        CType(Me.ogdMapSizeImport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvMapSizeImport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFTMapSizeExtend, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents oColFTSizeCodeNotExists As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTMapSizeExtend As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemFTMapSizeExtend As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
End Class
