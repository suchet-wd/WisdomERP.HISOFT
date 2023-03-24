<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAssetModel
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
        Me.ogcAssetModel = New DevExpress.XtraGrid.GridControl()
        Me.ogvAssetModel = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gbModelDetail = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmedit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmrefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmaddnew = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogcAssetModel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvAssetModel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gbModelDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbModelDetail.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogcAssetModel
        '
        Me.ogcAssetModel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcAssetModel.Location = New System.Drawing.Point(2, 20)
        Me.ogcAssetModel.MainView = Me.ogvAssetModel
        Me.ogcAssetModel.Name = "ogcAssetModel"
        Me.ogcAssetModel.Size = New System.Drawing.Size(777, 580)
        Me.ogcAssetModel.TabIndex = 1
        Me.ogcAssetModel.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvAssetModel})
        '
        'ogvAssetModel
        '
        Me.ogvAssetModel.GridControl = Me.ogcAssetModel
        Me.ogvAssetModel.Name = "ogvAssetModel"
        Me.ogvAssetModel.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvAssetModel.OptionsView.ShowGroupPanel = False
        Me.ogvAssetModel.Tag = "2|"
        '
        'gbModelDetail
        '
        Me.gbModelDetail.Controls.Add(Me.ogbmainprocbutton)
        Me.gbModelDetail.Controls.Add(Me.ogcAssetModel)
        Me.gbModelDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbModelDetail.Location = New System.Drawing.Point(0, 0)
        Me.gbModelDetail.Name = "gbModelDetail"
        Me.gbModelDetail.Size = New System.Drawing.Size(781, 602)
        Me.gbModelDetail.TabIndex = 0
        Me.gbModelDetail.Text = "ModelDetail"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmedit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmpreview)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmrefresh)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmdelete)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmaddnew)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(41, 246)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(728, 47)
        Me.ogbmainprocbutton.TabIndex = 2
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmedit
        '
        Me.ocmedit.Location = New System.Drawing.Point(414, 11)
        Me.ocmedit.Name = "ocmedit"
        Me.ocmedit.Size = New System.Drawing.Size(95, 25)
        Me.ocmedit.TabIndex = 98
        Me.ocmedit.TabStop = False
        Me.ocmedit.Tag = "2|"
        Me.ocmedit.Text = "EDIT"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(313, 11)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(95, 25)
        Me.ocmpreview.TabIndex = 97
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "PREVIEW"
        Me.ocmpreview.Visible = False
        '
        'ocmexit
        '
        Me.ocmexit.Location = New System.Drawing.Point(620, 11)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmrefresh
        '
        Me.ocmrefresh.Location = New System.Drawing.Point(211, 11)
        Me.ocmrefresh.Name = "ocmrefresh"
        Me.ocmrefresh.Size = New System.Drawing.Size(95, 25)
        Me.ocmrefresh.TabIndex = 95
        Me.ocmrefresh.TabStop = False
        Me.ocmrefresh.Tag = "2|"
        Me.ocmrefresh.Text = "CLEAR"
        '
        'ocmdelete
        '
        Me.ocmdelete.Location = New System.Drawing.Point(110, 11)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(95, 25)
        Me.ocmdelete.TabIndex = 94
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "DELETE"
        '
        'ocmaddnew
        '
        Me.ocmaddnew.Location = New System.Drawing.Point(9, 11)
        Me.ocmaddnew.Name = "ocmaddnew"
        Me.ocmaddnew.Size = New System.Drawing.Size(95, 25)
        Me.ocmaddnew.TabIndex = 93
        Me.ocmaddnew.TabStop = False
        Me.ocmaddnew.Tag = "2|"
        Me.ocmaddnew.Text = "NEW"
        '
        'wAssetModel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(781, 602)
        Me.Controls.Add(Me.gbModelDetail)
        Me.Name = "wAssetModel"
        Me.Text = "wAssetModel"
        CType(Me.ogcAssetModel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvAssetModel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gbModelDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbModelDetail.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogcAssetModel As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvAssetModel As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gbModelDetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmedit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmrefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmaddnew As DevExpress.XtraEditors.SimpleButton
End Class
