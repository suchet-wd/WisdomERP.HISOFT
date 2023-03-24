<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wCalculateExcelForecastFinish
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
        Me.ogbcmd = New DevExpress.XtraEditors.GroupControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpmappingsupplier = New DevExpress.XtraTab.XtraTabPage()
        Me.oxtab = New DevExpress.XtraTab.XtraTabControl()
        Me.otp1 = New DevExpress.XtraTab.XtraTabPage()
        Me.ogc1 = New DevExpress.XtraGrid.GridControl()
        Me.ogv1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.otp2 = New DevExpress.XtraTab.XtraTabPage()
        Me.ogc2 = New DevExpress.XtraGrid.GridControl()
        Me.ogv2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.otp3 = New DevExpress.XtraTab.XtraTabPage()
        Me.ogc3 = New DevExpress.XtraGrid.GridControl()
        Me.ogv3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.ogbcmd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbcmd.SuspendLayout()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpmappingsupplier.SuspendLayout()
        CType(Me.oxtab, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.oxtab.SuspendLayout()
        Me.otp1.SuspendLayout()
        CType(Me.ogc1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otp2.SuspendLayout()
        CType(Me.ogc2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otp3.SuspendLayout()
        CType(Me.ogc3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbcmd
        '
        Me.ogbcmd.Controls.Add(Me.ocmexit)
        Me.ogbcmd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ogbcmd.Location = New System.Drawing.Point(0, 549)
        Me.ogbcmd.Name = "ogbcmd"
        Me.ogbcmd.ShowCaption = False
        Me.ogbcmd.Size = New System.Drawing.Size(1381, 37)
        Me.ogbcmd.TabIndex = 304
        Me.ogbcmd.Text = "GroupControl1"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(1217, 5)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(154, 25)
        Me.ocmexit.TabIndex = 95
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "OK"
        '
        'otb
        '
        Me.otb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otb.Location = New System.Drawing.Point(0, 0)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpmappingsupplier
        Me.otb.ShowTabHeader = DevExpress.Utils.DefaultBoolean.[False]
        Me.otb.Size = New System.Drawing.Size(1381, 549)
        Me.otb.TabIndex = 305
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpmappingsupplier})
        '
        'otpmappingsupplier
        '
        Me.otpmappingsupplier.Controls.Add(Me.oxtab)
        Me.otpmappingsupplier.Name = "otpmappingsupplier"
        Me.otpmappingsupplier.Size = New System.Drawing.Size(1379, 547)
        Me.otpmappingsupplier.Text = "List Bom"
        '
        'oxtab
        '
        Me.oxtab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.oxtab.Location = New System.Drawing.Point(0, 0)
        Me.oxtab.Name = "oxtab"
        Me.oxtab.SelectedTabPage = Me.otp1
        Me.oxtab.Size = New System.Drawing.Size(1379, 547)
        Me.oxtab.TabIndex = 0
        Me.oxtab.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otp1, Me.otp3, Me.otp2})
        '
        'otp1
        '
        Me.otp1.Controls.Add(Me.ogc1)
        Me.otp1.Name = "otp1"
        Me.otp1.Size = New System.Drawing.Size(1377, 522)
        Me.otp1.Text = "Fore Cast Calculate Data"
        '
        'ogc1
        '
        Me.ogc1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogc1.Location = New System.Drawing.Point(0, 0)
        Me.ogc1.MainView = Me.ogv1
        Me.ogc1.Name = "ogc1"
        Me.ogc1.Size = New System.Drawing.Size(1377, 522)
        Me.ogc1.TabIndex = 398
        Me.ogc1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv1})
        '
        'ogv1
        '
        Me.ogv1.GridControl = Me.ogc1
        Me.ogv1.Name = "ogv1"
        Me.ogv1.OptionsBehavior.ReadOnly = True
        Me.ogv1.OptionsView.ColumnAutoWidth = False
        Me.ogv1.OptionsView.ShowGroupPanel = False
        '
        'otp2
        '
        Me.otp2.Controls.Add(Me.ogc2)
        Me.otp2.Name = "otp2"
        Me.otp2.Size = New System.Drawing.Size(1377, 522)
        Me.otp2.Text = "Invalid Data (System Can not Mapping Stye-Season-Colorway"
        '
        'ogc2
        '
        Me.ogc2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogc2.Location = New System.Drawing.Point(0, 0)
        Me.ogc2.MainView = Me.ogv2
        Me.ogc2.Name = "ogc2"
        Me.ogc2.Size = New System.Drawing.Size(1377, 522)
        Me.ogc2.TabIndex = 398
        Me.ogc2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv2})
        '
        'ogv2
        '
        Me.ogv2.GridControl = Me.ogc2
        Me.ogv2.Name = "ogv2"
        Me.ogv2.OptionsBehavior.ReadOnly = True
        Me.ogv2.OptionsView.ColumnAutoWidth = False
        Me.ogv2.OptionsView.ShowGroupPanel = False
        '
        'otp3
        '
        Me.otp3.Controls.Add(Me.ogc3)
        Me.otp3.Name = "otp3"
        Me.otp3.Size = New System.Drawing.Size(1377, 522)
        Me.otp3.Text = "Fore Cast Calculate Data (By Row)"
        '
        'ogc3
        '
        Me.ogc3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogc3.Location = New System.Drawing.Point(0, 0)
        Me.ogc3.MainView = Me.ogv3
        Me.ogc3.Name = "ogc3"
        Me.ogc3.Size = New System.Drawing.Size(1377, 522)
        Me.ogc3.TabIndex = 399
        Me.ogc3.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv3})
        '
        'ogv3
        '
        Me.ogv3.GridControl = Me.ogc3
        Me.ogv3.Name = "ogv3"
        Me.ogv3.OptionsBehavior.ReadOnly = True
        Me.ogv3.OptionsView.ColumnAutoWidth = False
        Me.ogv3.OptionsView.ShowGroupPanel = False
        '
        'wCalculateExcelForecastFinish
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1381, 586)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.ogbcmd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wCalculateExcelForecastFinish"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Calculate Excel Forecast Finish "
        CType(Me.ogbcmd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbcmd.ResumeLayout(False)
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpmappingsupplier.ResumeLayout(False)
        CType(Me.oxtab, System.ComponentModel.ISupportInitialize).EndInit()
        Me.oxtab.ResumeLayout(False)
        Me.otp1.ResumeLayout(False)
        CType(Me.ogc1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otp2.ResumeLayout(False)
        CType(Me.ogc2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otp3.ResumeLayout(False)
        CType(Me.ogc3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbcmd As DevExpress.XtraEditors.GroupControl
    Public WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpmappingsupplier As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents oxtab As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otp1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents otp2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogc1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogc2 As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents otp3 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogc3 As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv3 As DevExpress.XtraGrid.Views.Grid.GridView
End Class
