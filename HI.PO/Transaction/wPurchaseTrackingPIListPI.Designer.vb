<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPurchaseTrackingPIListPI
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
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTPINo = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.Size = New System.Drawing.Size(297, 379)
        Me.ogcdetail.TabIndex = 4
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "2|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTPINo})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowGroup = False
        Me.ogvdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetail.OptionsView.ShowAutoFilterRow = True
        Me.ogvdetail.OptionsView.ShowFooter = True
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        Me.ogvdetail.Tag = "2|"
        '
        'FTPINo
        '
        Me.FTPINo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTPINo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPINo.Caption = "P/I No."
        Me.FTPINo.FieldName = "FTPINo"
        Me.FTPINo.Name = "FTPINo"
        Me.FTPINo.OptionsColumn.AllowEdit = False
        Me.FTPINo.OptionsColumn.ReadOnly = True
        Me.FTPINo.Visible = True
        Me.FTPINo.VisibleIndex = 0
        Me.FTPINo.Width = 224
        '
        'wPurchaseTrackingPIListPI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(297, 379)
        Me.Controls.Add(Me.ogcdetail)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wPurchaseTrackingPIListPI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "P/I No"
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTPINo As DevExpress.XtraGrid.Columns.GridColumn
End Class
