<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPopUpAddPartCode
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.SBtnOK = New DevExpress.XtraEditors.SimpleButton()
        Me.SBtnExit = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcpart = New DevExpress.XtraGrid.GridControl()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTPartCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysPartId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.cFTPartName = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogcpart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SBtnOK
        '
        Me.SBtnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SBtnOK.Location = New System.Drawing.Point(388, 163)
        Me.SBtnOK.Margin = New System.Windows.Forms.Padding(4)
        Me.SBtnOK.Name = "SBtnOK"
        Me.SBtnOK.Size = New System.Drawing.Size(96, 33)
        Me.SBtnOK.TabIndex = 3
        Me.SBtnOK.Text = "OK"
        '
        'SBtnExit
        '
        Me.SBtnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SBtnExit.Location = New System.Drawing.Point(492, 163)
        Me.SBtnExit.Margin = New System.Windows.Forms.Padding(4)
        Me.SBtnExit.Name = "SBtnExit"
        Me.SBtnExit.Size = New System.Drawing.Size(96, 33)
        Me.SBtnExit.TabIndex = 4
        Me.SBtnExit.Text = "Exit"
        '
        'ogcpart
        '
        Me.ogcpart.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcpart.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcpart.Location = New System.Drawing.Point(2, 0)
        Me.ogcpart.MainView = Me.GridView1
        Me.ogcpart.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcpart.Name = "ogcpart"
        Me.ogcpart.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.ogcpart.Size = New System.Drawing.Size(619, 155)
        Me.ogcpart.TabIndex = 392
        Me.ogcpart.TabStop = False
        Me.ogcpart.Tag = "2|"
        Me.ogcpart.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView1})
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTPartCode, Me.cFNHSysPartId, Me.GridColumn1, Me.cFTPartName})
        Me.GridView1.DetailHeight = 431
        Me.GridView1.GridControl = Me.ogcpart
        Me.GridView1.Name = "GridView1"
        Me.GridView1.OptionsCustomization.AllowQuickHideColumns = False
        Me.GridView1.OptionsView.ColumnAutoWidth = False
        Me.GridView1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GridView1.OptionsView.ShowGroupPanel = False
        Me.GridView1.Tag = "2|"
        '
        'cFTPartCode
        '
        Me.cFTPartCode.Caption = "FTPartCode"
        Me.cFTPartCode.FieldName = "FTPartCode"
        Me.cFTPartCode.MinWidth = 25
        Me.cFTPartCode.Name = "cFTPartCode"
        Me.cFTPartCode.Visible = True
        Me.cFTPartCode.VisibleIndex = 1
        Me.cFTPartCode.Width = 198
        '
        'cFNHSysPartId
        '
        Me.cFNHSysPartId.Caption = "FNHSysPartId"
        Me.cFNHSysPartId.FieldName = "FNHSysPartId"
        Me.cFNHSysPartId.MinWidth = 25
        Me.cFNHSysPartId.Name = "cFNHSysPartId"
        Me.cFNHSysPartId.Width = 94
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "#"
        Me.GridColumn1.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.GridColumn1.FieldName = "FTSelect"
        Me.GridColumn1.MinWidth = 25
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Visible = True
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 52
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'cFTPartName
        '
        Me.cFTPartName.Caption = "FTPartName"
        Me.cFTPartName.FieldName = "FTPartName"
        Me.cFTPartName.MinWidth = 25
        Me.cFTPartName.Name = "cFTPartName"
        Me.cFTPartName.OptionsColumn.AllowEdit = False
        Me.cFTPartName.Visible = True
        Me.cFTPartName.VisibleIndex = 2
        Me.cFTPartName.Width = 326
        '
        'wPopUpAddPartCode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(622, 206)
        Me.Controls.Add(Me.ogcpart)
        Me.Controls.Add(Me.SBtnExit)
        Me.Controls.Add(Me.SBtnOK)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wPopUpAddPartCode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Part Select"
        CType(Me.ogcpart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SBtnOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SBtnExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcpart As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTPartCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNHSysPartId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTPartName As DevExpress.XtraGrid.Columns.GridColumn
End Class
