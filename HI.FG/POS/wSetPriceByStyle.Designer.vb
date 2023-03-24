<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wSetPriceByStyle
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogborderinfo = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmSave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmrefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.XtraScrollableControl1 = New DevExpress.XtraEditors.XtraScrollableControl()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTManualS = New DevExpress.XtraEditors.LabelControl()
        Me.FTManualC = New DevExpress.XtraEditors.LabelControl()
        Me.FTManual = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ogbbreadown = New DevExpress.XtraEditors.GroupControl()
        Me.ogcsub = New DevExpress.XtraGrid.GridControl()
        Me.ogvsub = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GFNHSysStyleId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFTColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCalcEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysStyleId_None = New DevExpress.XtraEditors.TextEdit()
        CType(Me.ogborderinfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogborderinfo.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.XtraScrollableControl1.SuspendLayout()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbbreadown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbbreadown.SuspendLayout()
        CType(Me.ogcsub, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvsub, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogborderinfo
        '
        Me.ogborderinfo.Controls.Add(Me.XtraScrollableControl1)
        Me.ogborderinfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogborderinfo.Location = New System.Drawing.Point(0, 0)
        Me.ogborderinfo.Name = "ogborderinfo"
        Me.ogborderinfo.Size = New System.Drawing.Size(1148, 101)
        Me.ogborderinfo.TabIndex = 1
        Me.ogborderinfo.Text = "Order Information"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmSave)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmrefresh)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(197, 6)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(714, 54)
        Me.ogbmainprocbutton.TabIndex = 303
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmclear
        '
        Me.ocmclear.Location = New System.Drawing.Point(311, 11)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(95, 25)
        Me.ocmclear.TabIndex = 110
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "CLEAR"
        '
        'ocmSave
        '
        Me.ocmSave.Location = New System.Drawing.Point(86, 11)
        Me.ocmSave.Name = "ocmSave"
        Me.ocmSave.Size = New System.Drawing.Size(95, 25)
        Me.ocmSave.TabIndex = 109
        Me.ocmSave.TabStop = False
        Me.ocmSave.Tag = "2|"
        Me.ocmSave.Text = "Save"
        '
        'ocmrefresh
        '
        Me.ocmrefresh.Location = New System.Drawing.Point(210, 11)
        Me.ocmrefresh.Name = "ocmrefresh"
        Me.ocmrefresh.Size = New System.Drawing.Size(95, 25)
        Me.ocmrefresh.TabIndex = 109
        Me.ocmrefresh.TabStop = False
        Me.ocmrefresh.Tag = "2|"
        Me.ocmrefresh.Text = "Refresh"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(601, 11)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'XtraScrollableControl1
        '
        Me.XtraScrollableControl1.Controls.Add(Me.ogbmainprocbutton)
        Me.XtraScrollableControl1.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.XtraScrollableControl1.Controls.Add(Me.FTManualS)
        Me.XtraScrollableControl1.Controls.Add(Me.FTManualC)
        Me.XtraScrollableControl1.Controls.Add(Me.FTManual)
        Me.XtraScrollableControl1.Controls.Add(Me.FNHSysStyleId_lbl)
        Me.XtraScrollableControl1.Controls.Add(Me.FNHSysStyleId)
        Me.XtraScrollableControl1.Controls.Add(Me.FNHSysStyleId_None)
        Me.XtraScrollableControl1.Controls.Add(Me.FNHSysCmpId)
        Me.XtraScrollableControl1.Controls.Add(Me.FNHSysCmpId_None)
        Me.XtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.XtraScrollableControl1.Location = New System.Drawing.Point(2, 21)
        Me.XtraScrollableControl1.Name = "XtraScrollableControl1"
        Me.XtraScrollableControl1.Size = New System.Drawing.Size(1144, 78)
        Me.XtraScrollableControl1.TabIndex = 485
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(32, 3)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(107, 17)
        Me.FNHSysCmpId_lbl.TabIndex = 467
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'FTManualS
        '
        Me.FTManualS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTManualS.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTManualS.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTManualS.Location = New System.Drawing.Point(682, 57)
        Me.FTManualS.Name = "FTManualS"
        Me.FTManualS.Size = New System.Drawing.Size(459, 18)
        Me.FTManualS.TabIndex = 439
        Me.FTManualS.Tag = "2|"
        Me.FTManualS.Text = "**  C Please keydown ( S ) For Default Price By Size"
        '
        'FTManualC
        '
        Me.FTManualC.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTManualC.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTManualC.Location = New System.Drawing.Point(320, 57)
        Me.FTManualC.Name = "FTManualC"
        Me.FTManualC.Size = New System.Drawing.Size(356, 18)
        Me.FTManualC.TabIndex = 439
        Me.FTManualC.Tag = "2|"
        Me.FTManualC.Text = "**  C Please keydown ( C) For Default Price By Color Way"
        '
        'FTManual
        '
        Me.FTManual.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTManual.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTManual.Location = New System.Drawing.Point(10, 57)
        Me.FTManual.Name = "FTManual"
        Me.FTManual.Size = New System.Drawing.Size(304, 18)
        Me.FTManual.TabIndex = 439
        Me.FTManual.Tag = "2|"
        Me.FTManual.Text = "**  A  Please keydown ( A ) For Default Price"
        '
        'FNHSysStyleId_lbl
        '
        Me.FNHSysStyleId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl.Location = New System.Drawing.Point(0, 27)
        Me.FNHSysStyleId_lbl.Name = "FNHSysStyleId_lbl"
        Me.FNHSysStyleId_lbl.Size = New System.Drawing.Size(141, 18)
        Me.FNHSysStyleId_lbl.TabIndex = 439
        Me.FNHSysStyleId_lbl.Tag = "2|"
        Me.FNHSysStyleId_lbl.Text = "FNHSysStyleId :"
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(143, 26)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysStyleId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "89", Nothing, True)})
        Me.FNHSysStyleId.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysStyleId.TabIndex = 440
        Me.FNHSysStyleId.Tag = "2|"
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(142, 3)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FNHSysCmpId.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.ReadOnly = True
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysCmpId.TabIndex = 466
        Me.FNHSysCmpId.Tag = ""
        '
        'GridView1
        '
        Me.GridView1.Name = "GridView1"
        '
        'ogbbreadown
        '
        Me.ogbbreadown.Controls.Add(Me.ogcsub)
        Me.ogbbreadown.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogbbreadown.Location = New System.Drawing.Point(0, 101)
        Me.ogbbreadown.Name = "ogbbreadown"
        Me.ogbbreadown.Size = New System.Drawing.Size(1148, 470)
        Me.ogbbreadown.TabIndex = 2
        Me.ogbbreadown.Text = "Break Down Info"
        '
        'ogcsub
        '
        Me.ogcsub.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcsub.Location = New System.Drawing.Point(2, 21)
        Me.ogcsub.MainView = Me.ogvsub
        Me.ogcsub.Name = "ogcsub"
        Me.ogcsub.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCalcEdit1})
        Me.ogcsub.Size = New System.Drawing.Size(1144, 447)
        Me.ogcsub.TabIndex = 308
        Me.ogcsub.TabStop = False
        Me.ogcsub.Tag = "3|"
        Me.ogcsub.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvsub})
        '
        'ogvsub
        '
        Me.ogvsub.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GFNHSysStyleId, Me.GFTStyleCode, Me.GFTColorway})
        Me.ogvsub.GridControl = Me.ogcsub
        Me.ogvsub.Name = "ogvsub"
        Me.ogvsub.OptionsCustomization.AllowGroup = False
        Me.ogvsub.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvsub.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        Me.ogvsub.OptionsView.ColumnAutoWidth = False
        Me.ogvsub.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvsub.OptionsView.ShowFooter = True
        Me.ogvsub.OptionsView.ShowGroupPanel = False
        Me.ogvsub.Tag = "2|"
        '
        'GFNHSysStyleId
        '
        Me.GFNHSysStyleId.Caption = "FNHSysStyleId"
        Me.GFNHSysStyleId.FieldName = "FNHSysStyleId"
        Me.GFNHSysStyleId.Name = "GFNHSysStyleId"
        '
        'GFTStyleCode
        '
        Me.GFTStyleCode.Caption = "FTStyleCode"
        Me.GFTStyleCode.FieldName = "FTStyleCode"
        Me.GFTStyleCode.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.GFTStyleCode.Name = "GFTStyleCode"
        Me.GFTStyleCode.OptionsColumn.AllowEdit = False
        Me.GFTStyleCode.OptionsColumn.AllowMove = False
        Me.GFTStyleCode.OptionsColumn.AllowShowHide = False
        Me.GFTStyleCode.OptionsColumn.AllowSize = False
        Me.GFTStyleCode.OptionsColumn.ReadOnly = True
        Me.GFTStyleCode.OptionsColumn.ShowInCustomizationForm = False
        Me.GFTStyleCode.Visible = True
        Me.GFTStyleCode.VisibleIndex = 0
        Me.GFTStyleCode.Width = 164
        '
        'GFTColorway
        '
        Me.GFTColorway.Caption = "FTColorway"
        Me.GFTColorway.FieldName = "FTColorway"
        Me.GFTColorway.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.GFTColorway.Name = "GFTColorway"
        Me.GFTColorway.OptionsColumn.AllowEdit = False
        Me.GFTColorway.OptionsColumn.AllowFocus = False
        Me.GFTColorway.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
        Me.GFTColorway.OptionsColumn.AllowMove = False
        Me.GFTColorway.OptionsColumn.AllowShowHide = False
        Me.GFTColorway.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.GFTColorway.OptionsFilter.AllowAutoFilter = False
        Me.GFTColorway.OptionsFilter.AllowFilter = False
        Me.GFTColorway.Visible = True
        Me.GFTColorway.VisibleIndex = 1
        Me.GFTColorway.Width = 134
        '
        'RepositoryItemCalcEdit1
        '
        Me.RepositoryItemCalcEdit1.AutoHeight = False
        Me.RepositoryItemCalcEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEdit1.Name = "RepositoryItemCalcEdit1"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(274, 3)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(867, 20)
        Me.FNHSysCmpId_None.TabIndex = 468
        Me.FNHSysCmpId_None.Tag = ""
        '
        'FNHSysStyleId_None
        '
        Me.FNHSysStyleId_None.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FNHSysStyleId_None.Location = New System.Drawing.Point(274, 26)
        Me.FNHSysStyleId_None.Name = "FNHSysStyleId_None"
        Me.FNHSysStyleId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleId_None.Properties.ReadOnly = True
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(867, 20)
        Me.FNHSysStyleId_None.TabIndex = 441
        Me.FNHSysStyleId_None.Tag = "2|"
        '
        'wSetPriceByStyle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1148, 571)
        Me.Controls.Add(Me.ogbbreadown)
        Me.Controls.Add(Me.ogborderinfo)
        Me.Name = "wSetPriceByStyle"
        Me.Text = "wSetPriceByStyle"
        CType(Me.ogborderinfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogborderinfo.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.XtraScrollableControl1.ResumeLayout(False)
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbbreadown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbbreadown.ResumeLayout(False)
        CType(Me.ogcsub, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvsub, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogborderinfo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents XtraScrollableControl1 As DevExpress.XtraEditors.XtraScrollableControl
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmrefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogbbreadown As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogcsub As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvsub As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GFNHSysStyleId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFTColorway As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCalcEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FTManual As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTManualC As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FTManualS As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
End Class
