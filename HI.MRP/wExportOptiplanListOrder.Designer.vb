<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wExportOptiplanListOrder
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
        Me.ogb = New DevExpress.XtraEditors.GroupControl()
        Me.ogclist = New DevExpress.XtraGrid.GridControl()
        Me.ogvlist = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTSizeBreakDown = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogb.SuspendLayout()
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogb
        '
        Me.ogb.Controls.Add(Me.ogclist)
        Me.ogb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogb.Location = New System.Drawing.Point(0, 0)
        Me.ogb.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogb.Name = "ogb"
        Me.ogb.Size = New System.Drawing.Size(579, 555)
        Me.ogb.TabIndex = 0
        Me.ogb.Text = "List Data"
        '
        'ogclist
        '
        Me.ogclist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogclist.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogclist.Location = New System.Drawing.Point(2, 24)
        Me.ogclist.MainView = Me.ogvlist
        Me.ogclist.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogclist.Name = "ogclist"
        Me.ogclist.Size = New System.Drawing.Size(575, 529)
        Me.ogclist.TabIndex = 0
        Me.ogclist.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvlist})
        '
        'ogvlist
        '
        Me.ogvlist.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTStyleCode, Me.CFTOrderNo, Me.CFTColorway, Me.CFTSizeBreakDown})
        Me.ogvlist.GridControl = Me.ogclist
        Me.ogvlist.Name = "ogvlist"
        Me.ogvlist.OptionsView.AllowCellMerge = True
        Me.ogvlist.OptionsView.ColumnAutoWidth = False
        Me.ogvlist.OptionsView.EnableAppearanceEvenRow = True
        Me.ogvlist.OptionsView.ShowGroupPanel = False
        '
        'CFTStyleCode
        '
        Me.CFTStyleCode.Caption = "FTStyleCode"
        Me.CFTStyleCode.FieldName = "FTStyleCode"
        Me.CFTStyleCode.Name = "CFTStyleCode"
        Me.CFTStyleCode.OptionsColumn.AllowEdit = False
        Me.CFTStyleCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTStyleCode.OptionsColumn.ReadOnly = True
        Me.CFTStyleCode.Visible = True
        Me.CFTStyleCode.VisibleIndex = 0
        Me.CFTStyleCode.Width = 159
        '
        'CFTOrderNo
        '
        Me.CFTOrderNo.Caption = "FTOrderNo"
        Me.CFTOrderNo.FieldName = "FTOrderNo"
        Me.CFTOrderNo.Name = "CFTOrderNo"
        Me.CFTOrderNo.OptionsColumn.AllowEdit = False
        Me.CFTOrderNo.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
        Me.CFTOrderNo.OptionsColumn.ReadOnly = True
        Me.CFTOrderNo.Visible = True
        Me.CFTOrderNo.VisibleIndex = 1
        Me.CFTOrderNo.Width = 132
        '
        'CFTColorway
        '
        Me.CFTColorway.Caption = "FTColorway"
        Me.CFTColorway.FieldName = "FTColorway"
        Me.CFTColorway.Name = "CFTColorway"
        Me.CFTColorway.OptionsColumn.AllowEdit = False
        Me.CFTColorway.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTColorway.OptionsColumn.ReadOnly = True
        Me.CFTColorway.Visible = True
        Me.CFTColorway.VisibleIndex = 2
        Me.CFTColorway.Width = 139
        '
        'CFTSizeBreakDown
        '
        Me.CFTSizeBreakDown.Caption = "FTSizeBreakDown"
        Me.CFTSizeBreakDown.FieldName = "FTSizeBreakDown"
        Me.CFTSizeBreakDown.Name = "CFTSizeBreakDown"
        Me.CFTSizeBreakDown.OptionsColumn.AllowEdit = False
        Me.CFTSizeBreakDown.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTSizeBreakDown.OptionsColumn.ReadOnly = True
        Me.CFTSizeBreakDown.Visible = True
        Me.CFTSizeBreakDown.VisibleIndex = 3
        Me.CFTSizeBreakDown.Width = 103
        '
        'wExportOptiplanListOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(579, 555)
        Me.Controls.Add(Me.ogb)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wExportOptiplanListOrder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List Order Size Data Problem"
        CType(Me.ogb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogb.ResumeLayout(False)
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogb As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogclist As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvlist As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTColorway As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTSizeBreakDown As DevExpress.XtraGrid.Columns.GridColumn
End Class
