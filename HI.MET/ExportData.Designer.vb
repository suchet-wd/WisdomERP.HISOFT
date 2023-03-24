<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExportData
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
        Me.components = New System.ComponentModel.Container()
        Me.DockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.ogbmDDDDDD = New DevExpress.XtraEditors.PanelControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcDetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryFTStateSendApp = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryFTStateApp = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryFTStateAppsdf = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.RepositoryItemFTStateSendApp = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmDDDDDD, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmDDDDDD.SuspendLayout()
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTStateSendApp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTStateApp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTStateAppsdf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTStateSendApp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DockManager
        '
        Me.DockManager.Form = Me
        Me.DockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'ogbmDDDDDD
        '
        Me.ogbmDDDDDD.Controls.Add(Me.ocmexit)
        Me.ogbmDDDDDD.Controls.Add(Me.ocmload)
        Me.ogbmDDDDDD.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ogbmDDDDDD.Location = New System.Drawing.Point(0, 540)
        Me.ogbmDDDDDD.Name = "ogbmDDDDDD"
        Me.ogbmDDDDDD.Size = New System.Drawing.Size(1069, 34)
        Me.ogbmDDDDDD.TabIndex = 393
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(910, 7)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(17, 5)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(95, 25)
        Me.ocmload.TabIndex = 93
        Me.ocmload.TabStop = False
        Me.ocmload.Tag = "2|"
        Me.ocmload.Text = "LoadData"
        '
        'ogcDetail
        '
        Me.ogcDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcDetail.Location = New System.Drawing.Point(0, 0)
        Me.ogcDetail.MainView = Me.ogvDetail
        Me.ogcDetail.Name = "ogcDetail"
        Me.ogcDetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryFTStateSendApp, Me.RepositoryFTStateAppsdf, Me.RepositoryFTStateApp, Me.RepositoryItemFTStateSendApp})
        Me.ogcDetail.Size = New System.Drawing.Size(1069, 574)
        Me.ogcDetail.TabIndex = 395
        Me.ogcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDetail})
        '
        'ogvDetail
        '
        Me.ogvDetail.DetailHeight = 284
        Me.ogvDetail.GridControl = Me.ogcDetail
        Me.ogvDetail.Name = "ogvDetail"
        Me.ogvDetail.OptionsView.ShowGroupPanel = False
        '
        'RepositoryFTStateSendApp
        '
        Me.RepositoryFTStateSendApp.AutoHeight = False
        Me.RepositoryFTStateSendApp.Caption = "Check"
        Me.RepositoryFTStateSendApp.Name = "RepositoryFTStateSendApp"
        Me.RepositoryFTStateSendApp.ValueChecked = "1"
        Me.RepositoryFTStateSendApp.ValueUnchecked = "0"
        '
        'RepositoryFTStateApp
        '
        Me.RepositoryFTStateApp.AutoHeight = False
        Me.RepositoryFTStateApp.Caption = "Check"
        Me.RepositoryFTStateApp.Name = "RepositoryFTStateApp"
        Me.RepositoryFTStateApp.ValueChecked = "1"
        Me.RepositoryFTStateApp.ValueUnchecked = "0"
        '
        'RepositoryFTStateAppsdf
        '
        Me.RepositoryFTStateAppsdf.AutoHeight = False
        Me.RepositoryFTStateAppsdf.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryFTStateAppsdf.Name = "RepositoryFTStateAppsdf"
        '
        'RepositoryItemFTStateSendApp
        '
        Me.RepositoryItemFTStateSendApp.AutoHeight = False
        Me.RepositoryItemFTStateSendApp.Caption = "Check"
        Me.RepositoryItemFTStateSendApp.Name = "RepositoryItemFTStateSendApp"
        '
        'ExportData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1069, 574)
        Me.Controls.Add(Me.ogbmDDDDDD)
        Me.Controls.Add(Me.ogcDetail)
        Me.Name = "ExportData"
        Me.Text = "ExportData"
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmDDDDDD, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmDDDDDD.ResumeLayout(False)
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTStateSendApp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTStateApp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTStateAppsdf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFTStateSendApp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbmDDDDDD As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryFTStateApp As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryFTStateSendApp As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryFTStateAppsdf As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepositoryItemFTStateSendApp As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class
