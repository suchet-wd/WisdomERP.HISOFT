<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StockCardAsset
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
        Me.DockPanel = New DevExpress.XtraBars.Docking.DockPanel()
        Me.DockPanel1_Container = New DevExpress.XtraBars.Docking.ControlContainer()
        Me.ButtonEdit1 = New DevExpress.XtraEditors.ButtonEdit()
        Me.ButtonEdit2 = New DevExpress.XtraEditors.ButtonEdit()
        Me.ButtonEdit3 = New DevExpress.XtraEditors.ButtonEdit()
        Me.ButtonEdit4 = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogrp = New DevExpress.XtraEditors.GroupControl()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DockPanel.SuspendLayout()
        Me.DockPanel1_Container.SuspendLayout()
        CType(Me.ButtonEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonEdit3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ButtonEdit4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogrp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrp.SuspendLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DockManager
        '
        Me.DockManager.Form = Me
        Me.DockManager.RootPanels.AddRange(New DevExpress.XtraBars.Docking.DockPanel() {Me.DockPanel})
        Me.DockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane", "DevExpress.XtraBars.TabFormControl"})
        '
        'DockPanel
        '
        Me.DockPanel.Controls.Add(Me.DockPanel1_Container)
        Me.DockPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top
        Me.DockPanel.ID = New System.Guid("34f192e3-7569-4344-80cd-be2e8d5b9f91")
        Me.DockPanel.Location = New System.Drawing.Point(0, 0)
        Me.DockPanel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel.Name = "DockPanel"
        Me.DockPanel.Options.FloatOnDblClick = False
        Me.DockPanel.Options.ShowCloseButton = False
        Me.DockPanel.Options.ShowMaximizeButton = False
        Me.DockPanel.OriginalSize = New System.Drawing.Size(200, 120)
        Me.DockPanel.Size = New System.Drawing.Size(1177, 120)
        Me.DockPanel.Text = "Criteria"
        '
        'DockPanel1_Container
        '
        Me.DockPanel1_Container.Controls.Add(Me.ButtonEdit4)
        Me.DockPanel1_Container.Controls.Add(Me.ButtonEdit3)
        Me.DockPanel1_Container.Controls.Add(Me.ButtonEdit2)
        Me.DockPanel1_Container.Controls.Add(Me.ButtonEdit1)
        Me.DockPanel1_Container.Location = New System.Drawing.Point(5, 27)
        Me.DockPanel1_Container.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DockPanel1_Container.Name = "DockPanel1_Container"
        Me.DockPanel1_Container.Size = New System.Drawing.Size(1167, 86)
        Me.DockPanel1_Container.TabIndex = 0
        '
        'ButtonEdit1
        '
        Me.ButtonEdit1.Location = New System.Drawing.Point(139, 16)
        Me.ButtonEdit1.Name = "ButtonEdit1"
        Me.ButtonEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ButtonEdit1.Size = New System.Drawing.Size(120, 22)
        Me.ButtonEdit1.TabIndex = 0
        '
        'ButtonEdit2
        '
        Me.ButtonEdit2.Location = New System.Drawing.Point(545, 16)
        Me.ButtonEdit2.Name = "ButtonEdit2"
        Me.ButtonEdit2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ButtonEdit2.Size = New System.Drawing.Size(120, 22)
        Me.ButtonEdit2.TabIndex = 1
        '
        'ButtonEdit3
        '
        Me.ButtonEdit3.Location = New System.Drawing.Point(139, 44)
        Me.ButtonEdit3.Name = "ButtonEdit3"
        Me.ButtonEdit3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ButtonEdit3.Size = New System.Drawing.Size(120, 22)
        Me.ButtonEdit3.TabIndex = 1
        '
        'ButtonEdit4
        '
        Me.ButtonEdit4.Location = New System.Drawing.Point(545, 44)
        Me.ButtonEdit4.Name = "ButtonEdit4"
        Me.ButtonEdit4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.ButtonEdit4.Size = New System.Drawing.Size(120, 22)
        Me.ButtonEdit4.TabIndex = 2
        '
        'ogrp
        '
        Me.ogrp.Controls.Add(Me.ogcdetail)
        Me.ogrp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrp.Location = New System.Drawing.Point(0, 120)
        Me.ogrp.Name = "ogrp"
        Me.ogrp.Size = New System.Drawing.Size(1177, 586)
        Me.ogrp.TabIndex = 1
        Me.ogrp.Text = "GroupControl1"
        '
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetail.Location = New System.Drawing.Point(2, 25)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.Size = New System.Drawing.Size(1173, 559)
        Me.ogcdetail.TabIndex = 0
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsView.ShowFooter = True
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'StockCardAsset
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1177, 706)
        Me.Controls.Add(Me.ogrp)
        Me.Controls.Add(Me.DockPanel)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "StockCardAsset"
        Me.Text = "StockCardAsset"
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DockPanel.ResumeLayout(False)
        Me.DockPanel1_Container.ResumeLayout(False)
        CType(Me.ButtonEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonEdit3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ButtonEdit4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogrp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrp.ResumeLayout(False)
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents DockPanel As DevExpress.XtraBars.Docking.DockPanel
    Friend WithEvents DockPanel1_Container As DevExpress.XtraBars.Docking.ControlContainer
    Friend WithEvents ButtonEdit4 As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ButtonEdit3 As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ButtonEdit2 As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ButtonEdit1 As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogrp As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
End Class
