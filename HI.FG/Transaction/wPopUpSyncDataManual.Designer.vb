<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wPopUpSyncDataManual
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wPopUpSyncDataManual))
        Me.FNDBName = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTTableName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ocmOK = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclose = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.FNDBName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FNDBName
        '
        Me.FNDBName.Location = New System.Drawing.Point(131, 12)
        Me.FNDBName.Name = "FNDBName"
        Me.FNDBName.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "FNDBName", Nothing, True)})
        Me.FNDBName.Properties.Tag = "FNDBName"
        Me.FNDBName.Size = New System.Drawing.Size(204, 20)
        Me.FNDBName.TabIndex = 0
        '
        'ogcdetail
        '
        Me.ogcdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcdetail.Location = New System.Drawing.Point(2, 38)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.ogcdetail.Size = New System.Drawing.Size(465, 386)
        Me.ogcdetail.TabIndex = 1
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTState, Me.cFTTableName})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'cFTState
        '
        Me.cFTState.Caption = "FTState"
        Me.cFTState.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.cFTState.FieldName = "FTState"
        Me.cFTState.Name = "cFTState"
        Me.cFTState.Visible = True
        Me.cFTState.VisibleIndex = 0
        Me.cFTState.Width = 126
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.PictureChecked = CType(resources.GetObject("RepositoryItemCheckEdit1.PictureChecked"), System.Drawing.Image)
        Me.RepositoryItemCheckEdit1.PictureUnchecked = CType(resources.GetObject("RepositoryItemCheckEdit1.PictureUnchecked"), System.Drawing.Image)
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'cFTTableName
        '
        Me.cFTTableName.Caption = "FTTableName"
        Me.cFTTableName.FieldName = "FTTableName"
        Me.cFTTableName.Name = "cFTTableName"
        Me.cFTTableName.OptionsColumn.AllowEdit = False
        Me.cFTTableName.Visible = True
        Me.cFTTableName.VisibleIndex = 1
        Me.cFTTableName.Width = 409
        '
        'ocmOK
        '
        Me.ocmOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmOK.Location = New System.Drawing.Point(307, 430)
        Me.ocmOK.Name = "ocmOK"
        Me.ocmOK.Size = New System.Drawing.Size(75, 23)
        Me.ocmOK.TabIndex = 2
        Me.ocmOK.Text = "OK"
        '
        'ocmclose
        '
        Me.ocmclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmclose.Location = New System.Drawing.Point(388, 430)
        Me.ocmclose.Name = "ocmclose"
        Me.ocmclose.Size = New System.Drawing.Size(75, 23)
        Me.ocmclose.TabIndex = 2
        Me.ocmclose.Text = "CLOSE"
        '
        'wPopUpSyncDataManual
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(468, 462)
        Me.Controls.Add(Me.ocmclose)
        Me.Controls.Add(Me.ocmOK)
        Me.Controls.Add(Me.ogcdetail)
        Me.Controls.Add(Me.FNDBName)
        Me.Name = "wPopUpSyncDataManual"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "wPopUpSyncDataManual"
        CType(Me.FNDBName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FNDBName As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTTableName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclose As DevExpress.XtraEditors.SimpleButton
End Class
