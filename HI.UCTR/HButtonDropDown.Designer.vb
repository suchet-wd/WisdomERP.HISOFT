<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HButtonDropDown
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ogcbrowse = New DevExpress.XtraGrid.GridControl()
        Me.ogvbrowse = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.ogcbrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvbrowse, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcbrowse
        '
        Me.ogcbrowse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcbrowse.Location = New System.Drawing.Point(0, 0)
        Me.ogcbrowse.MainView = Me.ogvbrowse
        Me.ogcbrowse.Name = "ogcbrowse"
        Me.ogcbrowse.Size = New System.Drawing.Size(328, 318)
        Me.ogcbrowse.TabIndex = 3
        Me.ogcbrowse.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvbrowse})
        '
        'ogvbrowse
        '
        Me.ogvbrowse.GridControl = Me.ogcbrowse
        Me.ogvbrowse.Name = "ogvbrowse"
        Me.ogvbrowse.OptionsCustomization.AllowGroup = False
        Me.ogvbrowse.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvbrowse.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvbrowse.OptionsView.ShowGroupPanel = False
        '
        'HButtonDropDown
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ogcbrowse)
        Me.Name = "HButtonDropDown"
        Me.Size = New System.Drawing.Size(328, 318)
        CType(Me.ogcbrowse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvbrowse, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcbrowse As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvbrowse As DevExpress.XtraGrid.Views.Grid.GridView

End Class
