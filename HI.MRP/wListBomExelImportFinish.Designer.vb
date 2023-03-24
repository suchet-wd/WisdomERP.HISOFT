<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wListBomExelImportFinish
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
        Me.ogclist = New DevExpress.XtraGrid.GridControl()
        Me.ogvlist = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.STYLE_NBR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.STYLE_NM = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SEASON_CD = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SEASON_YR = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSeason = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateReplace = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FNHSysStyleDevId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateImport = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogbcmd = New DevExpress.XtraEditors.GroupControl()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpmappingsupplier = New DevExpress.XtraTab.XtraTabPage()
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbcmd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbcmd.SuspendLayout()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpmappingsupplier.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogclist
        '
        Me.ogclist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogclist.Location = New System.Drawing.Point(0, 0)
        Me.ogclist.MainView = Me.ogvlist
        Me.ogclist.Name = "ogclist"
        Me.ogclist.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepFTSelect})
        Me.ogclist.Size = New System.Drawing.Size(740, 611)
        Me.ogclist.TabIndex = 302
        Me.ogclist.TabStop = False
        Me.ogclist.Tag = "2|"
        Me.ogclist.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvlist})
        '
        'ogvlist
        '
        Me.ogvlist.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.STYLE_NBR, Me.STYLE_NM, Me.SEASON_CD, Me.SEASON_YR, Me.FTSeason, Me.FTStateReplace, Me.FNHSysStyleDevId, Me.FTStateImport})
        Me.ogvlist.GridControl = Me.ogclist
        Me.ogvlist.Name = "ogvlist"
        Me.ogvlist.OptionsCustomization.AllowGroup = False
        Me.ogvlist.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvlist.OptionsView.ColumnAutoWidth = False
        Me.ogvlist.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvlist.OptionsView.ShowGroupPanel = False
        Me.ogvlist.Tag = "2|"
        '
        'STYLE_NBR
        '
        Me.STYLE_NBR.Caption = "STYLE NO"
        Me.STYLE_NBR.FieldName = "STYLE_NBR"
        Me.STYLE_NBR.Name = "STYLE_NBR"
        Me.STYLE_NBR.OptionsColumn.AllowEdit = False
        Me.STYLE_NBR.OptionsColumn.ReadOnly = True
        Me.STYLE_NBR.Visible = True
        Me.STYLE_NBR.VisibleIndex = 0
        Me.STYLE_NBR.Width = 117
        '
        'STYLE_NM
        '
        Me.STYLE_NM.Caption = "STYLE NAME"
        Me.STYLE_NM.FieldName = "STYLE_NM"
        Me.STYLE_NM.Name = "STYLE_NM"
        Me.STYLE_NM.OptionsColumn.AllowEdit = False
        Me.STYLE_NM.OptionsColumn.ReadOnly = True
        Me.STYLE_NM.Visible = True
        Me.STYLE_NM.VisibleIndex = 1
        Me.STYLE_NM.Width = 260
        '
        'SEASON_CD
        '
        Me.SEASON_CD.Caption = "SEASON"
        Me.SEASON_CD.FieldName = "SEASON_CD"
        Me.SEASON_CD.Name = "SEASON_CD"
        Me.SEASON_CD.OptionsColumn.AllowEdit = False
        Me.SEASON_CD.OptionsColumn.ReadOnly = True
        Me.SEASON_CD.Visible = True
        Me.SEASON_CD.VisibleIndex = 2
        Me.SEASON_CD.Width = 79
        '
        'SEASON_YR
        '
        Me.SEASON_YR.Caption = "YEAR"
        Me.SEASON_YR.FieldName = "SEASON_YR"
        Me.SEASON_YR.Name = "SEASON_YR"
        Me.SEASON_YR.OptionsColumn.AllowEdit = False
        Me.SEASON_YR.OptionsColumn.ReadOnly = True
        Me.SEASON_YR.Visible = True
        Me.SEASON_YR.VisibleIndex = 3
        Me.SEASON_YR.Width = 90
        '
        'FTSeason
        '
        Me.FTSeason.Caption = "FTSeason"
        Me.FTSeason.FieldName = "FTSeason"
        Me.FTSeason.Name = "FTSeason"
        Me.FTSeason.OptionsColumn.AllowEdit = False
        Me.FTSeason.OptionsColumn.ReadOnly = True
        '
        'FTStateReplace
        '
        Me.FTStateReplace.Caption = "Replace"
        Me.FTStateReplace.ColumnEdit = Me.RepFTSelect
        Me.FTStateReplace.FieldName = "FTStateReplace"
        Me.FTStateReplace.Name = "FTStateReplace"
        Me.FTStateReplace.OptionsColumn.AllowEdit = False
        Me.FTStateReplace.OptionsColumn.ReadOnly = True
        Me.FTStateReplace.Visible = True
        Me.FTStateReplace.VisibleIndex = 4
        '
        'RepFTSelect
        '
        Me.RepFTSelect.AutoHeight = False
        Me.RepFTSelect.Caption = "Check"
        Me.RepFTSelect.Name = "RepFTSelect"
        Me.RepFTSelect.ValueChecked = "1"
        Me.RepFTSelect.ValueUnchecked = "0"
        '
        'FNHSysStyleDevId
        '
        Me.FNHSysStyleDevId.Caption = "FNHSysStyleDevId"
        Me.FNHSysStyleDevId.FieldName = "FNHSysStyleDevId"
        Me.FNHSysStyleDevId.Name = "FNHSysStyleDevId"
        Me.FNHSysStyleDevId.OptionsColumn.AllowEdit = False
        Me.FNHSysStyleDevId.OptionsColumn.ReadOnly = True
        '
        'FTStateImport
        '
        Me.FTStateImport.Caption = "Import"
        Me.FTStateImport.ColumnEdit = Me.RepFTSelect
        Me.FTStateImport.FieldName = "FTStateImport"
        Me.FTStateImport.Name = "FTStateImport"
        Me.FTStateImport.Visible = True
        Me.FTStateImport.VisibleIndex = 5
        Me.FTStateImport.Width = 63
        '
        'ogbcmd
        '
        Me.ogbcmd.Controls.Add(Me.ocmexit)
        Me.ogbcmd.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ogbcmd.Location = New System.Drawing.Point(0, 617)
        Me.ogbcmd.Name = "ogbcmd"
        Me.ogbcmd.ShowCaption = False
        Me.ogbcmd.Size = New System.Drawing.Size(746, 43)
        Me.ogbcmd.TabIndex = 304
        Me.ogbcmd.Text = "GroupControl1"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(615, 13)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(100, 25)
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
        Me.otb.Size = New System.Drawing.Size(746, 617)
        Me.otb.TabIndex = 305
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpmappingsupplier})
        '
        'otpmappingsupplier
        '
        Me.otpmappingsupplier.Controls.Add(Me.ogclist)
        Me.otpmappingsupplier.Name = "otpmappingsupplier"
        Me.otpmappingsupplier.Size = New System.Drawing.Size(740, 611)
        Me.otpmappingsupplier.Text = "List Bom"
        '
        'wListBomExelImportFinish
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(746, 660)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.ogbcmd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "wListBomExelImportFinish"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ข้อมูลรายการนำเข้า BOM "
        CType(Me.ogclist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvlist, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbcmd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbcmd.ResumeLayout(False)
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpmappingsupplier.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RepFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ogbcmd As DevExpress.XtraEditors.GroupControl
    Public WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Public WithEvents ogclist As DevExpress.XtraGrid.GridControl
    Public WithEvents ogvlist As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpmappingsupplier As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents STYLE_NBR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents STYLE_NM As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SEASON_CD As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents SEASON_YR As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSeason As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateReplace As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysStyleDevId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateImport As DevExpress.XtraGrid.Columns.GridColumn
End Class
