﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wFactoryHubStylePopup
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
        Me.ogbMapSizeImportOrder = New DevExpress.XtraEditors.GroupControl()
        Me.ogcstyle = New DevExpress.XtraGrid.GridControl()
        Me.ogvMapSizeImport = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTStyle = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemFTMapSizeExtend = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.ogbConfirmMapSizeImportOrder = New DevExpress.XtraEditors.GroupControl()
        Me.ocmok = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbMapSizeImportOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbMapSizeImportOrder.SuspendLayout()
        CType(Me.ogcstyle, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.ogbMapSizeImportOrder.Controls.Add(Me.ogcstyle)
        Me.ogbMapSizeImportOrder.Location = New System.Drawing.Point(4, 2)
        Me.ogbMapSizeImportOrder.Name = "ogbMapSizeImportOrder"
        Me.ogbMapSizeImportOrder.ShowCaption = False
        Me.ogbMapSizeImportOrder.Size = New System.Drawing.Size(335, 422)
        Me.ogbMapSizeImportOrder.TabIndex = 0
        Me.ogbMapSizeImportOrder.Text = "Map Size (cause size code not match in system master file)"
        '
        'ogcstyle
        '
        Me.ogcstyle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcstyle.Location = New System.Drawing.Point(2, 2)
        Me.ogcstyle.MainView = Me.ogvMapSizeImport
        Me.ogcstyle.Name = "ogcstyle"
        Me.ogcstyle.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemFTMapSizeExtend})
        Me.ogcstyle.Size = New System.Drawing.Size(331, 418)
        Me.ogcstyle.TabIndex = 0
        Me.ogcstyle.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvMapSizeImport})
        '
        'ogvMapSizeImport
        '
        Me.ogvMapSizeImport.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTStyle})
        Me.ogvMapSizeImport.GridControl = Me.ogcstyle
        Me.ogvMapSizeImport.Name = "ogvMapSizeImport"
        Me.ogvMapSizeImport.OptionsView.ColumnAutoWidth = False
        Me.ogvMapSizeImport.OptionsView.ShowGroupPanel = False
        '
        'FTStyle
        '
        Me.FTStyle.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStyle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStyle.Caption = "Style Code"
        Me.FTStyle.FieldName = "FTStyle"
        Me.FTStyle.Name = "FTStyle"
        Me.FTStyle.OptionsColumn.AllowEdit = False
        Me.FTStyle.OptionsColumn.ReadOnly = True
        Me.FTStyle.Visible = True
        Me.FTStyle.VisibleIndex = 0
        Me.FTStyle.Width = 197
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
        Me.ogbConfirmMapSizeImportOrder.Controls.Add(Me.ocmok)
        Me.ogbConfirmMapSizeImportOrder.Location = New System.Drawing.Point(4, 427)
        Me.ogbConfirmMapSizeImportOrder.Name = "ogbConfirmMapSizeImportOrder"
        Me.ogbConfirmMapSizeImportOrder.ShowCaption = False
        Me.ogbConfirmMapSizeImportOrder.Size = New System.Drawing.Size(335, 40)
        Me.ogbConfirmMapSizeImportOrder.TabIndex = 1
        '
        'ocmok
        '
        Me.ocmok.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ocmok.Location = New System.Drawing.Point(2, 2)
        Me.ocmok.Name = "ocmok"
        Me.ocmok.Size = New System.Drawing.Size(331, 36)
        Me.ocmok.TabIndex = 2
        Me.ocmok.TabStop = False
        Me.ocmok.Tag = "2|"
        Me.ocmok.Text = "OK"
        '
        'wFactoryHubStylePopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(344, 471)
        Me.ControlBox = False
        Me.Controls.Add(Me.ogbConfirmMapSizeImportOrder)
        Me.Controls.Add(Me.ogbMapSizeImportOrder)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wFactoryHubStylePopup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "List of Style Data Not Found"
        CType(Me.ogbMapSizeImportOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbMapSizeImportOrder.ResumeLayout(False)
        CType(Me.ogcstyle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvMapSizeImport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFTMapSizeExtend, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbConfirmMapSizeImportOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbConfirmMapSizeImportOrder.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbMapSizeImportOrder As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbConfirmMapSizeImportOrder As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ocmok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcstyle As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvMapSizeImport As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTStyle As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemFTMapSizeExtend As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
End Class
