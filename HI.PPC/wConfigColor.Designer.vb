<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wConfigColor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(wConfigColor))
        Me.ogbconfigcolor = New DevExpress.XtraEditors.GroupControl()
        Me.ogcdetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FNHSysCmpRunId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCmpRunCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNGraphImageIndex = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFNGraphImageIndex = New DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox()
        Me.ImgBarbg = New System.Windows.Forms.ImageList(Me.components)
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmrefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogbconfigcolor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbconfigcolor.SuspendLayout()
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFNGraphImageIndex, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbconfigcolor
        '
        Me.ogbconfigcolor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbconfigcolor.Controls.Add(Me.ogcdetail)
        Me.ogbconfigcolor.Location = New System.Drawing.Point(5, 4)
        Me.ogbconfigcolor.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbconfigcolor.Name = "ogbconfigcolor"
        Me.ogbconfigcolor.Size = New System.Drawing.Size(1131, 784)
        Me.ogbconfigcolor.TabIndex = 4
        Me.ogbconfigcolor.Text = "Sam By Sub FO."
        '
        'ogcdetail
        '
        Me.ogcdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetail.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Location = New System.Drawing.Point(2, 24)
        Me.ogcdetail.MainView = Me.ogvdetail
        Me.ogcdetail.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetail.Name = "ogcdetail"
        Me.ogcdetail.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFNGraphImageIndex})
        Me.ogcdetail.Size = New System.Drawing.Size(1127, 758)
        Me.ogcdetail.TabIndex = 301
        Me.ogcdetail.TabStop = False
        Me.ogcdetail.Tag = "3|"
        Me.ogcdetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetail})
        '
        'ogvdetail
        '
        Me.ogvdetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FNHSysCmpRunId, Me.FTCmpRunCode, Me.FNGraphImageIndex})
        Me.ogvdetail.GridControl = Me.ogcdetail
        Me.ogvdetail.Images = Me.ImgBarbg
        Me.ogvdetail.Name = "ogvdetail"
        Me.ogvdetail.OptionsCustomization.AllowGroup = False
        Me.ogvdetail.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetail.OptionsView.AllowCellMerge = True
        Me.ogvdetail.OptionsView.ColumnAutoWidth = False
        Me.ogvdetail.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetail.OptionsView.ShowGroupPanel = False
        Me.ogvdetail.Tag = "2|"
        '
        'FNHSysCmpRunId
        '
        Me.FNHSysCmpRunId.Caption = "FNHSysCmpRunId"
        Me.FNHSysCmpRunId.FieldName = "FNHSysCmpRunId"
        Me.FNHSysCmpRunId.Name = "FNHSysCmpRunId"
        '
        'FTCmpRunCode
        '
        Me.FTCmpRunCode.Caption = "FTCmpRunCode"
        Me.FTCmpRunCode.FieldName = "FTCmpRunCode"
        Me.FTCmpRunCode.Name = "FTCmpRunCode"
        Me.FTCmpRunCode.OptionsColumn.AllowEdit = False
        Me.FTCmpRunCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTCmpRunCode.OptionsColumn.AllowMove = False
        Me.FTCmpRunCode.OptionsColumn.ReadOnly = True
        Me.FTCmpRunCode.Visible = True
        Me.FTCmpRunCode.VisibleIndex = 0
        Me.FTCmpRunCode.Width = 174
        '
        'FNGraphImageIndex
        '
        Me.FNGraphImageIndex.Caption = "Image"
        Me.FNGraphImageIndex.ColumnEdit = Me.ReposFNGraphImageIndex
        Me.FNGraphImageIndex.FieldName = "FNGraphImageIndex"
        Me.FNGraphImageIndex.Name = "FNGraphImageIndex"
        Me.FNGraphImageIndex.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNGraphImageIndex.OptionsColumn.AllowMove = False
        Me.FNGraphImageIndex.Visible = True
        Me.FNGraphImageIndex.VisibleIndex = 1
        Me.FNGraphImageIndex.Width = 116
        '
        'ReposFNGraphImageIndex
        '
        Me.ReposFNGraphImageIndex.AutoHeight = False
        Me.ReposFNGraphImageIndex.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFNGraphImageIndex.Items.AddRange(New DevExpress.XtraEditors.Controls.ImageComboBoxItem() {New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "A0", 0), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "A1", 1), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "A2", 2), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "A3", 3), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "A4", 4), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "A5", 5), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "A6", 6), New DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "A7", 7)})
        Me.ReposFNGraphImageIndex.Name = "ReposFNGraphImageIndex"
        Me.ReposFNGraphImageIndex.SmallImages = Me.ImgBarbg
        '
        'ImgBarbg
        '
        Me.ImgBarbg.ImageStream = CType(resources.GetObject("ImgBarbg.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgBarbg.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgBarbg.Images.SetKeyName(0, "01Sky.png")
        Me.ImgBarbg.Images.SetKeyName(1, "02Greenlight.png")
        Me.ImgBarbg.Images.SetKeyName(2, "03Bule.png")
        Me.ImgBarbg.Images.SetKeyName(3, "04Green.png")
        Me.ImgBarbg.Images.SetKeyName(4, "05Yellow.png")
        Me.ImgBarbg.Images.SetKeyName(5, "06Orange.png")
        Me.ImgBarbg.Images.SetKeyName(6, "07Gray.png")
        Me.ImgBarbg.Images.SetKeyName(7, "08BlueBlack.png")
        Me.ImgBarbg.Images.SetKeyName(8, "09Red.png")
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmrefresh)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(121, 366)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(899, 58)
        Me.ogbmainprocbutton.TabIndex = 303
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmrefresh
        '
        Me.ocmrefresh.Location = New System.Drawing.Point(505, 14)
        Me.ocmrefresh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmrefresh.Name = "ocmrefresh"
        Me.ocmrefresh.Size = New System.Drawing.Size(111, 31)
        Me.ocmrefresh.TabIndex = 112
        Me.ocmrefresh.TabStop = False
        Me.ocmrefresh.Tag = "2|"
        Me.ocmrefresh.Text = "Refresh"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(767, 14)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(128, 14)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(111, 31)
        Me.ocmclear.TabIndex = 95
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(10, 14)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(111, 31)
        Me.ocmsave.TabIndex = 93
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'wConfigColor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1140, 791)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbconfigcolor)
        Me.Name = "wConfigColor"
        Me.Text = "Config Color"
        CType(Me.ogbconfigcolor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbconfigcolor.ResumeLayout(False)
        CType(Me.ogcdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFNGraphImageIndex, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbconfigcolor As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcdetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmrefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FNHSysCmpRunId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCmpRunCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNGraphImageIndex As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFNGraphImageIndex As DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox
    Friend WithEvents ImgBarbg As System.Windows.Forms.ImageList
End Class
