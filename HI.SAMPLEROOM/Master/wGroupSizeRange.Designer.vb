<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wGroupSizeRange
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject7 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject8 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.FNHSysSeasonId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.RepositoryItemButtonFNHSysStyleId = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.RepositoryItemButtonFNHSysVenderPramId = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.TextEdit1 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit2 = New DevExpress.XtraEditors.TextEdit()
        Me.TextEdit3 = New DevExpress.XtraEditors.TextEdit()
        Me.ceStatusActive = New DevExpress.XtraEditors.CheckEdit()
        Me.FTSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSizeNameEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSizeNameTH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemCheckEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemButtonFNHSysStyleId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemButtonFNHSysVenderPramId, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TextEdit3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ceStatusActive.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.ceStatusActive)
        Me.GroupControl1.Controls.Add(Me.TextEdit3)
        Me.GroupControl1.Controls.Add(Me.TextEdit2)
        Me.GroupControl1.Controls.Add(Me.TextEdit1)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.FNHSysSeasonId_lbl)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Controls.Add(Me.FNHSysStyleId_lbl)
        Me.GroupControl1.Controls.Add(Me.ocmdelete)
        Me.GroupControl1.Controls.Add(Me.ocmsave)
        Me.GroupControl1.Controls.Add(Me.ogc)
        Me.GroupControl1.Controls.Add(Me.ocmexit)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(800, 450)
        Me.GroupControl1.TabIndex = 4
        Me.GroupControl1.Text = "Group Size Range Detail"
        '
        'FNHSysSeasonId_lbl
        '
        Me.FNHSysSeasonId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSeasonId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysSeasonId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysSeasonId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysSeasonId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysSeasonId_lbl.Location = New System.Drawing.Point(19, 26)
        Me.FNHSysSeasonId_lbl.Name = "FNHSysSeasonId_lbl"
        Me.FNHSysSeasonId_lbl.Size = New System.Drawing.Size(127, 19)
        Me.FNHSysSeasonId_lbl.TabIndex = 545
        Me.FNHSysSeasonId_lbl.Tag = "2|"
        Me.FNHSysSeasonId_lbl.Text = "FTSizeRangeCode :"
        '
        'LabelControl1
        '
        Me.LabelControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 7.0!)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.Tomato
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl1.Location = New System.Drawing.Point(5, 389)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(786, 19)
        Me.LabelControl1.TabIndex = 505
        Me.LabelControl1.Tag = "2|"
        Me.LabelControl1.Text = "* ลบรายการ คลิกที่ แถวแล้วกดปุ่ม Delete บนแป้นพิมพ์"
        '
        'FNHSysStyleId_lbl
        '
        Me.FNHSysStyleId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysStyleId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysStyleId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl.Location = New System.Drawing.Point(19, 51)
        Me.FNHSysStyleId_lbl.Name = "FNHSysStyleId_lbl"
        Me.FNHSysStyleId_lbl.Size = New System.Drawing.Size(127, 19)
        Me.FNHSysStyleId_lbl.TabIndex = 504
        Me.FNHSysStyleId_lbl.Tag = "2|"
        Me.FNHSysStyleId_lbl.Text = "FTSizeRangeNameEN :"
        '
        'ocmdelete
        '
        Me.ocmdelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmdelete.Location = New System.Drawing.Point(145, 414)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(95, 25)
        Me.ocmdelete.TabIndex = 100
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "Delete"
        '
        'ocmsave
        '
        Me.ocmsave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmsave.Location = New System.Drawing.Point(19, 414)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(95, 25)
        Me.ocmsave.TabIndex = 100
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'ogc
        '
        Me.ogc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogc.Location = New System.Drawing.Point(3, 105)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemButtonFNHSysStyleId, Me.RepositoryItemButtonFNHSysVenderPramId, Me.RepositoryItemCheckEdit1, Me.RepositoryItemCheckEdit2})
        Me.ogc.Size = New System.Drawing.Size(793, 279)
        Me.ogc.TabIndex = 0
        Me.ogc.Tag = "2|"
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTSelect, Me.FTSizeCode, Me.FTSizeNameEN, Me.FTSizeNameTH, Me.FTStatus})
        Me.ogv.DetailHeight = 284
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'RepositoryItemButtonFNHSysStyleId
        '
        Me.RepositoryItemButtonFNHSysStyleId.AutoHeight = False
        Me.RepositoryItemButtonFNHSysStyleId.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, EditorButtonImageOptions1, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, SerializableAppearanceObject2, SerializableAppearanceObject3, SerializableAppearanceObject4, "", "506", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.RepositoryItemButtonFNHSysStyleId.Name = "RepositoryItemButtonFNHSysStyleId"
        Me.RepositoryItemButtonFNHSysStyleId.Tag = ""
        '
        'RepositoryItemButtonFNHSysVenderPramId
        '
        Me.RepositoryItemButtonFNHSysVenderPramId.AutoHeight = False
        Me.RepositoryItemButtonFNHSysVenderPramId.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, EditorButtonImageOptions2, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, SerializableAppearanceObject6, SerializableAppearanceObject7, SerializableAppearanceObject8, "", "168", Nothing, DevExpress.Utils.ToolTipAnchor.[Default])})
        Me.RepositoryItemButtonFNHSysVenderPramId.Name = "RepositoryItemButtonFNHSysVenderPramId"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(677, 414)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        Me.LabelControl2.Appearance.Options.UseTextOptions = True
        Me.LabelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.LabelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.LabelControl2.Location = New System.Drawing.Point(19, 76)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(127, 19)
        Me.LabelControl2.TabIndex = 546
        Me.LabelControl2.Tag = "2|"
        Me.LabelControl2.Text = "FTSizeRangeNameTH :"
        '
        'TextEdit1
        '
        Me.TextEdit1.Location = New System.Drawing.Point(152, 26)
        Me.TextEdit1.Name = "TextEdit1"
        Me.TextEdit1.Size = New System.Drawing.Size(150, 20)
        Me.TextEdit1.TabIndex = 547
        '
        'TextEdit2
        '
        Me.TextEdit2.Location = New System.Drawing.Point(152, 51)
        Me.TextEdit2.Name = "TextEdit2"
        Me.TextEdit2.Size = New System.Drawing.Size(300, 20)
        Me.TextEdit2.TabIndex = 548
        '
        'TextEdit3
        '
        Me.TextEdit3.Location = New System.Drawing.Point(152, 77)
        Me.TextEdit3.Name = "TextEdit3"
        Me.TextEdit3.Size = New System.Drawing.Size(300, 20)
        Me.TextEdit3.TabIndex = 549
        '
        'ceStatusActive
        '
        Me.ceStatusActive.EditValue = True
        Me.ceStatusActive.Location = New System.Drawing.Point(377, 26)
        Me.ceStatusActive.Name = "ceStatusActive"
        Me.ceStatusActive.Properties.Caption = "Active"
        Me.ceStatusActive.Size = New System.Drawing.Size(75, 20)
        Me.ceStatusActive.TabIndex = 550
        '
        'FTSizeCode
        '
        Me.FTSizeCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSizeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSizeCode.Caption = "FTSizeCode"
        Me.FTSizeCode.Name = "FTSizeCode"
        Me.FTSizeCode.Visible = True
        Me.FTSizeCode.VisibleIndex = 1
        '
        'FTSizeNameEN
        '
        Me.FTSizeNameEN.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSizeNameEN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSizeNameEN.Caption = "FTSizeNameEN"
        Me.FTSizeNameEN.Name = "FTSizeNameEN"
        Me.FTSizeNameEN.Visible = True
        Me.FTSizeNameEN.VisibleIndex = 2
        '
        'FTSizeNameTH
        '
        Me.FTSizeNameTH.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSizeNameTH.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSizeNameTH.Caption = "FTSizeNameTH"
        Me.FTSizeNameTH.Name = "FTSizeNameTH"
        Me.FTSizeNameTH.Visible = True
        Me.FTSizeNameTH.VisibleIndex = 3
        '
        'FTStatus
        '
        Me.FTStatus.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStatus.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStatus.Caption = "FTStatus"
        Me.FTStatus.ColumnEdit = Me.RepositoryItemCheckEdit2
        Me.FTStatus.Name = "FTStatus"
        Me.FTStatus.Visible = True
        Me.FTStatus.VisibleIndex = 4
        '
        'FTSelect
        '
        Me.FTSelect.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSelect.Caption = "FTSelect"
        Me.FTSelect.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.FTSelect.Name = "FTSelect"
        Me.FTSelect.Visible = True
        Me.FTSelect.VisibleIndex = 0
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        '
        'RepositoryItemCheckEdit2
        '
        Me.RepositoryItemCheckEdit2.AutoHeight = False
        Me.RepositoryItemCheckEdit2.Name = "RepositoryItemCheckEdit2"
        '
        'wGroupSizeRange
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.GroupControl1)
        Me.Name = "wGroupSizeRange"
        Me.Text = "wGroupSizeRange"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemButtonFNHSysStyleId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemButtonFNHSysVenderPramId, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TextEdit3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ceStatusActive.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNHSysSeasonId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemButtonFNHSysStyleId As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents RepositoryItemButtonFNHSysVenderPramId As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ceStatusActive As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents TextEdit3 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents TextEdit1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSizeNameEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSizeNameTH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
End Class
