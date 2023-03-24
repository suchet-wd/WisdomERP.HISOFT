<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wListDashboardTemplate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wListDashboardTemplate))
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepCheckEdit = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ocmuploaddashboard = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmcancel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdownloaddashboard = New DevExpress.XtraEditors.SimpleButton()
        Me.CFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTDashBoardName = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepCheckEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.ocmdownloaddashboard)
        Me.PanelControl1.Controls.Add(Me.ocmcancel)
        Me.PanelControl1.Controls.Add(Me.ocmuploaddashboard)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 626)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(531, 54)
        Me.PanelControl1.TabIndex = 0
        '
        'ogc
        '
        Me.ogc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Location = New System.Drawing.Point(0, 0)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepCheckEdit})
        Me.ogc.Size = New System.Drawing.Size(531, 626)
        Me.ogc.TabIndex = 3
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CFTSelect, Me.CFTDashBoardName})
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowGroup = False
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        '
        'RepCheckEdit
        '
        Me.RepCheckEdit.AutoHeight = False
        Me.RepCheckEdit.Caption = "Check"
        Me.RepCheckEdit.Name = "RepCheckEdit"
        Me.RepCheckEdit.ValueChecked = "1"
        Me.RepCheckEdit.ValueUnchecked = "0"
        '
        'ocmuploaddashboard
        '
        Me.ocmuploaddashboard.Image = CType(resources.GetObject("ocmuploaddashboard.Image"), System.Drawing.Image)
        Me.ocmuploaddashboard.Location = New System.Drawing.Point(29, 9)
        Me.ocmuploaddashboard.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmuploaddashboard.Name = "ocmuploaddashboard"
        Me.ocmuploaddashboard.Size = New System.Drawing.Size(180, 36)
        Me.ocmuploaddashboard.TabIndex = 5
        Me.ocmuploaddashboard.Text = "Upload to Server"
        Me.ocmuploaddashboard.Visible = False
        '
        'ocmcancel
        '
        Me.ocmcancel.Image = CType(resources.GetObject("ocmcancel.Image"), System.Drawing.Image)
        Me.ocmcancel.Location = New System.Drawing.Point(314, 9)
        Me.ocmcancel.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmcancel.Name = "ocmcancel"
        Me.ocmcancel.Size = New System.Drawing.Size(180, 36)
        Me.ocmcancel.TabIndex = 6
        Me.ocmcancel.Text = "Cancel"
        '
        'ocmdownloaddashboard
        '
        Me.ocmdownloaddashboard.Image = CType(resources.GetObject("ocmdownloaddashboard.Image"), System.Drawing.Image)
        Me.ocmdownloaddashboard.Location = New System.Drawing.Point(30, 9)
        Me.ocmdownloaddashboard.Margin = New System.Windows.Forms.Padding(4)
        Me.ocmdownloaddashboard.Name = "ocmdownloaddashboard"
        Me.ocmdownloaddashboard.Size = New System.Drawing.Size(180, 36)
        Me.ocmdownloaddashboard.TabIndex = 7
        Me.ocmdownloaddashboard.Text = "Download from Server"
        Me.ocmdownloaddashboard.Visible = False
        '
        'CFTSelect
        '
        Me.CFTSelect.Caption = " "
        Me.CFTSelect.ColumnEdit = Me.RepCheckEdit
        Me.CFTSelect.FieldName = "FTSelect"
        Me.CFTSelect.Name = "CFTSelect"
        Me.CFTSelect.Visible = True
        Me.CFTSelect.VisibleIndex = 0
        Me.CFTSelect.Width = 50
        '
        'CFTDashBoardName
        '
        Me.CFTDashBoardName.Caption = "Dashboard Name"
        Me.CFTDashBoardName.FieldName = "FTDashBoardName"
        Me.CFTDashBoardName.Name = "CFTDashBoardName"
        Me.CFTDashBoardName.OptionsColumn.AllowEdit = False
        Me.CFTDashBoardName.OptionsColumn.ReadOnly = True
        Me.CFTDashBoardName.Visible = True
        Me.CFTDashBoardName.VisibleIndex = 1
        Me.CFTDashBoardName.Width = 400
        '
        'wListDashboardTemplate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(531, 680)
        Me.Controls.Add(Me.ogc)
        Me.Controls.Add(Me.PanelControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wListDashboardTemplate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "List Dashboard Template"
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepCheckEdit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepCheckEdit As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents CFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTDashBoardName As DevExpress.XtraGrid.Columns.GridColumn
    Public WithEvents ocmdownloaddashboard As DevExpress.XtraEditors.SimpleButton
    Public WithEvents ocmcancel As DevExpress.XtraEditors.SimpleButton
    Public WithEvents ocmuploaddashboard As DevExpress.XtraEditors.SimpleButton
End Class
