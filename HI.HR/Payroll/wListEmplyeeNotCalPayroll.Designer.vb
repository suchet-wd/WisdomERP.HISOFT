<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wListEmplyeeNotCalPayroll
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
        Me.ogclist = New DevExpress.XtraGrid.GridControl()
        Me.ogvlist = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTEmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTEmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogblist = New DevExpress.XtraEditors.GroupControl()
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogblist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogblist.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogclist
        '
        Me.ogclist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogclist.Location = New System.Drawing.Point(2, 2)
        Me.ogclist.MainView = Me.ogvlist
        Me.ogclist.Name = "ogclist"
        Me.ogclist.Size = New System.Drawing.Size(500, 531)
        Me.ogclist.TabIndex = 302
        Me.ogclist.TabStop = False
        Me.ogclist.Tag = "2|"
        Me.ogclist.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvlist})
        '
        'ogvlist
        '
        Me.ogvlist.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTEmpCode, Me.FTEmpName})
        Me.ogvlist.GridControl = Me.ogclist
        Me.ogvlist.Name = "ogvlist"
        Me.ogvlist.OptionsCustomization.AllowGroup = False
        Me.ogvlist.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvlist.OptionsView.ColumnAutoWidth = False
        Me.ogvlist.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvlist.OptionsView.ShowGroupPanel = False
        Me.ogvlist.Tag = "2|"
        '
        'FTEmpCode
        '
        Me.FTEmpCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTEmpCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTEmpCode.Caption = "รหัสพนักงาน"
        Me.FTEmpCode.FieldName = "FTEmpCode"
        Me.FTEmpCode.Name = "FTEmpCode"
        Me.FTEmpCode.OptionsColumn.AllowEdit = False
        Me.FTEmpCode.OptionsColumn.ReadOnly = True
        Me.FTEmpCode.Visible = True
        Me.FTEmpCode.VisibleIndex = 0
        Me.FTEmpCode.Width = 146
        '
        'FTEmpName
        '
        Me.FTEmpName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTEmpName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTEmpName.Caption = "ชื่อพนักงาน"
        Me.FTEmpName.FieldName = "FTEmpName"
        Me.FTEmpName.Name = "FTEmpName"
        Me.FTEmpName.OptionsColumn.AllowEdit = False
        Me.FTEmpName.OptionsColumn.ReadOnly = True
        Me.FTEmpName.Visible = True
        Me.FTEmpName.VisibleIndex = 1
        Me.FTEmpName.Width = 284
        '
        'ogblist
        '
        Me.ogblist.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogblist.Controls.Add(Me.ogclist)
        Me.ogblist.Location = New System.Drawing.Point(0, 4)
        Me.ogblist.Name = "ogblist"
        Me.ogblist.ShowCaption = False
        Me.ogblist.Size = New System.Drawing.Size(504, 535)
        Me.ogblist.TabIndex = 303
        '
        'wListEmplyeeNotCalPayroll
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(504, 537)
        Me.Controls.Add(Me.ogblist)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wListEmplyeeNotCalPayroll"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List Employee Not Cal"
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogblist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogblist.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogclist As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvlist As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogblist As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTEmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEmpCode As DevExpress.XtraGrid.Columns.GridColumn
End Class
