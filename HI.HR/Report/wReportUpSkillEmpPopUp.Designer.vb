<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wReportUpSkillEmpPopUp
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
        Me.ogcSkill = New DevExpress.XtraGrid.GridControl()
        Me.ogbvSkill = New DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView()
        Me.BandedGridColumn1 = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
        CType(Me.ogcSkill, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbvSkill, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogcSkill
        '
        Me.ogcSkill.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcSkill.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcSkill.Location = New System.Drawing.Point(0, 0)
        Me.ogcSkill.MainView = Me.ogbvSkill
        Me.ogcSkill.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcSkill.Name = "ogcSkill"
        Me.ogcSkill.Size = New System.Drawing.Size(1430, 744)
        Me.ogcSkill.TabIndex = 1
        Me.ogcSkill.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogbvSkill})
        '
        'ogbvSkill
        '
        Me.ogbvSkill.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() {Me.BandedGridColumn1})
        Me.ogbvSkill.GridControl = Me.ogcSkill
        Me.ogbvSkill.Name = "ogbvSkill"
        Me.ogbvSkill.OptionsView.ShowColumnHeaders = False
        Me.ogbvSkill.OptionsView.ShowGroupPanel = False
        '
        'BandedGridColumn1
        '
        Me.BandedGridColumn1.Caption = "BandedGridColumn1"
        Me.BandedGridColumn1.Name = "BandedGridColumn1"
        Me.BandedGridColumn1.Visible = True
        '
        'wReportUpSkillEmpPopUp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1430, 744)
        Me.Controls.Add(Me.ogcSkill)
        Me.Name = "wReportUpSkillEmpPopUp"
        Me.Text = "wReportUpSkillEmpPopUp"
        CType(Me.ogcSkill, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbvSkill, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ogcSkill As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogbvSkill As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
    Friend WithEvents BandedGridColumn1 As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
End Class
