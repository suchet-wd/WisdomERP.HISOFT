<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wSyncData
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wSyncData))
        Me.ogrpdetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsyncdatafromserver = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsyncdatatoserver = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsaveweightpack = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmSyncDataManual = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ProgressBarControl1 = New DevExpress.XtraEditors.ProgressBarControl()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.ogrpdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogrpdetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogrpdetail
        '
        Me.ogrpdetail.Controls.Add(Me.ogbmainprocbutton)
        Me.ogrpdetail.Controls.Add(Me.ProgressBarControl1)
        Me.ogrpdetail.Controls.Add(Me.ogcdetail)
        Me.ogrpdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogrpdetail.Location = New System.Drawing.Point(0, 0)
        Me.ogrpdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogrpdetail.Name = "ogrpdetail"
        Me.ogrpdetail.Size = New System.Drawing.Size(1020, 751)
        Me.ogrpdetail.TabIndex = 0
        Me.ogrpdetail.Text = "Detail"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsyncdatafromserver)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsyncdatatoserver)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsaveweightpack)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmSyncDataManual)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(385, 305)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(549, 171)
        Me.ogbmainprocbutton.TabIndex = 395
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmsyncdatafromserver
        '
        Me.ocmsyncdatafromserver.Location = New System.Drawing.Point(37, 54)
        Me.ocmsyncdatafromserver.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsyncdatafromserver.Name = "ocmsyncdatafromserver"
        Me.ocmsyncdatafromserver.Size = New System.Drawing.Size(165, 31)
        Me.ocmsyncdatafromserver.TabIndex = 106
        Me.ocmsyncdatafromserver.TabStop = False
        Me.ocmsyncdatafromserver.Tag = "2|"
        Me.ocmsyncdatafromserver.Text = "sync data From Server"
        '
        'ocmsyncdatatoserver
        '
        Me.ocmsyncdatatoserver.Location = New System.Drawing.Point(232, 54)
        Me.ocmsyncdatatoserver.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsyncdatatoserver.Name = "ocmsyncdatatoserver"
        Me.ocmsyncdatatoserver.Size = New System.Drawing.Size(226, 31)
        Me.ocmsyncdatatoserver.TabIndex = 106
        Me.ocmsyncdatatoserver.TabStop = False
        Me.ocmsyncdatatoserver.Tag = "2|"
        Me.ocmsyncdatatoserver.Text = "sync data To Server"
        '
        'ocmsaveweightpack
        '
        Me.ocmsaveweightpack.Location = New System.Drawing.Point(246, 16)
        Me.ocmsaveweightpack.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsaveweightpack.Name = "ocmsaveweightpack"
        Me.ocmsaveweightpack.Size = New System.Drawing.Size(97, 31)
        Me.ocmsaveweightpack.TabIndex = 105
        Me.ocmsaveweightpack.TabStop = False
        Me.ocmsaveweightpack.Tag = "2|"
        Me.ocmsaveweightpack.Text = "saveweight"
        Me.ocmsaveweightpack.Visible = False
        '
        'ocmSyncDataManual
        '
        Me.ocmSyncDataManual.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ocmSyncDataManual.Location = New System.Drawing.Point(58, 92)
        Me.ocmSyncDataManual.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmSyncDataManual.Name = "ocmSyncDataManual"
        Me.ocmSyncDataManual.Size = New System.Drawing.Size(111, 31)
        Me.ocmSyncDataManual.TabIndex = 96
        Me.ocmSyncDataManual.TabStop = False
        Me.ocmSyncDataManual.Tag = "2|"
        Me.ocmSyncDataManual.Text = "SyncDataManual"
        Me.ocmSyncDataManual.Visible = False
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ocmexit.Location = New System.Drawing.Point(105, 16)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ProgressBarControl1
        '
        Me.ProgressBarControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBarControl1.EditValue = "50"
        Me.ProgressBarControl1.Location = New System.Drawing.Point(28, 711)
        Me.ProgressBarControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ProgressBarControl1.Name = "ProgressBarControl1"
        Me.ProgressBarControl1.ShowProgressInTaskBar = True
        Me.ProgressBarControl1.Size = New System.Drawing.Size(961, 22)
        Me.ProgressBarControl1.TabIndex = 397
        Me.ProgressBarControl1.Visible = False
        '
        'ogcdetail
        '
        Me.ogcdetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Location = New System.Drawing.Point(6, 30)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit2})
        Me.ogcdetail.Size = New System.Drawing.Size(1008, 684)
        Me.ogcdetail.TabIndex = 396
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTDescription, Me.cFTStatus})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        '
        'cFTDescription
        '
        Me.cFTDescription.Caption = "FTDescription"
        Me.cFTDescription.FieldName = "FTDescription"
        Me.cFTDescription.Name = "cFTDescription"
        Me.cFTDescription.OptionsColumn.AllowEdit = False
        Me.cFTDescription.Visible = True
        Me.cFTDescription.VisibleIndex = 0
        Me.cFTDescription.Width = 386
        '
        'cFTStatus
        '
        Me.cFTStatus.Caption = "FTStatus"
        Me.cFTStatus.ColumnEdit = Me.RepositoryItemCheckEdit2
        Me.cFTStatus.FieldName = "FTStatus"
        Me.cFTStatus.Name = "cFTStatus"
        Me.cFTStatus.OptionsColumn.AllowEdit = False
        Me.cFTStatus.Visible = True
        Me.cFTStatus.VisibleIndex = 1
        Me.cFTStatus.Width = 310
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Caption = "Check"
        Me.RepositoryItemCheckEdit2.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        Me.RepositoryItemCheckEdit2.PictureChecked = CType(resources.GetObject("RepositoryItemCheckEdit2.PictureChecked"), System.Drawing.Image)
        Me.RepositoryItemCheckEdit2.PictureUnchecked = CType(resources.GetObject("RepositoryItemCheckEdit2.PictureUnchecked"), System.Drawing.Image)
        Me.RepositoryItemCheckEdit2.ReadOnly = True
        '
        'wSyncData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1020, 751)
        Me.Controls.Add(Me.ogrpdetail)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wSyncData"
        Me.Text = "wSyncData"
        CType(Me.ogrpdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogrpdetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogrpdetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmsyncdatafromserver As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsyncdatatoserver As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsaveweightpack As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents cFTDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ProgressBarControl1 As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents ocmSyncDataManual As DevExpress.XtraEditors.SimpleButton
End Class
