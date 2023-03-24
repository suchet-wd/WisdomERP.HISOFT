Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wCountryForwarderMaster
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ReposFDShipDateTo = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.RepositoryItemFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemrFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ocmdoc = New DevExpress.XtraBars.Docking.DockManager()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmLoad = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclear = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysCountryId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemButtonFNHSysSuplId = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.cFTCountryCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCountryName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ReposFDShipDateTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFDShipDateTo.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemrFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemButtonFNHSysSuplId, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ReposFDShipDateTo
        '
        Me.ReposFDShipDateTo.AutoHeight = False
        Me.ReposFDShipDateTo.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFDShipDateTo.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ReposFDShipDateTo.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.ReposFDShipDateTo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ReposFDShipDateTo.EditFormat.FormatString = "dd/MM/yyyy"
        Me.ReposFDShipDateTo.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.ReposFDShipDateTo.Name = "ReposFDShipDateTo"
        '
        'RepositoryItemFTSelect
        '
        Me.RepositoryItemFTSelect.AutoHeight = False
        Me.RepositoryItemFTSelect.Name = "RepositoryItemFTSelect"
        Me.RepositoryItemFTSelect.ValueChecked = "1"
        Me.RepositoryItemFTSelect.ValueUnchecked = "0"
        '
        'RepositoryItemrFTSelect
        '
        Me.RepositoryItemrFTSelect.AutoHeight = False
        Me.RepositoryItemrFTSelect.Name = "RepositoryItemrFTSelect"
        Me.RepositoryItemrFTSelect.ValueChecked = "1"
        Me.RepositoryItemrFTSelect.ValueUnchecked = "0"
        '
        'ocmdoc
        '
        Me.ocmdoc.Form = Me
        Me.ocmdoc.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.ogbmainprocbutton)
        Me.GroupControl1.Controls.Add(Me.ogc)
        Me.GroupControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupControl1.Location = New System.Drawing.Point(0, 0)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1026, 842)
        Me.GroupControl1.TabIndex = 3
        Me.GroupControl1.Text = "DETAIL"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmAdd)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmLoad)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclear)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(86, 440)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(888, 64)
        Me.ogbmainprocbutton.TabIndex = 394
        '
        'ocmAdd
        '
        Me.ocmAdd.Location = New System.Drawing.Point(318, 6)
        Me.ocmAdd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmAdd.Name = "ocmAdd"
        Me.ocmAdd.Size = New System.Drawing.Size(111, 31)
        Me.ocmAdd.TabIndex = 100
        Me.ocmAdd.TabStop = False
        Me.ocmAdd.Tag = "2|"
        Me.ocmAdd.Text = "Add"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(16, 5)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(111, 31)
        Me.ocmsave.TabIndex = 100
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        '
        'ocmLoad
        '
        Me.ocmLoad.Location = New System.Drawing.Point(154, 5)
        Me.ocmLoad.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmLoad.Name = "ocmLoad"
        Me.ocmLoad.Size = New System.Drawing.Size(111, 31)
        Me.ocmLoad.TabIndex = 99
        Me.ocmLoad.TabStop = False
        Me.ocmLoad.Tag = "2|"
        Me.ocmLoad.Text = "Load"
        '
        'ocmclear
        '
        Me.ocmclear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmclear.Location = New System.Drawing.Point(569, 9)
        Me.ocmclear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmclear.Name = "ocmclear"
        Me.ocmclear.Size = New System.Drawing.Size(111, 31)
        Me.ocmclear.TabIndex = 96
        Me.ocmclear.TabStop = False
        Me.ocmclear.Tag = "2|"
        Me.ocmclear.Text = "Clear"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(703, 9)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ogc
        '
        Me.ogc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogc.Location = New System.Drawing.Point(2, 25)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemButtonFNHSysSuplId})
        Me.ogc.Size = New System.Drawing.Size(1022, 815)
        Me.ogc.TabIndex = 0
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.cFNHSysCountryId, Me.cFTCountryCode, Me.cFTCountryName, Me.cFTRemark})
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsMenu.ShowGroupSummaryEditorItem = True
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupedColumns = True
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "FTSelect"
        Me.GridColumn1.FieldName = "FTSelect"
        Me.GridColumn1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.OptionsColumn.AllowEdit = False
        '
        'cFNHSysCountryId
        '
        Me.cFNHSysCountryId.Caption = "FNHSysCountryId"
        Me.cFNHSysCountryId.ColumnEdit = Me.RepositoryItemButtonFNHSysSuplId
        Me.cFNHSysCountryId.FieldName = "FNHSysCountryId"
        Me.cFNHSysCountryId.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.cFNHSysCountryId.Name = "cFNHSysCountryId"
        Me.cFNHSysCountryId.OptionsColumn.AllowEdit = False
        Me.cFNHSysCountryId.Width = 119
        '
        'RepositoryItemButtonFNHSysSuplId
        '
        Me.RepositoryItemButtonFNHSysSuplId.AutoHeight = False
        Me.RepositoryItemButtonFNHSysSuplId.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "506", Nothing, True)})
        Me.RepositoryItemButtonFNHSysSuplId.Name = "RepositoryItemButtonFNHSysSuplId"
        Me.RepositoryItemButtonFNHSysSuplId.Tag = ""
        '
        'cFTCountryCode
        '
        Me.cFTCountryCode.Caption = "FTCountryCode"
        Me.cFTCountryCode.FieldName = "FTCountryCode"
        Me.cFTCountryCode.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.cFTCountryCode.Name = "cFTCountryCode"
        Me.cFTCountryCode.OptionsColumn.AllowEdit = False
        Me.cFTCountryCode.Visible = True
        Me.cFTCountryCode.VisibleIndex = 0
        Me.cFTCountryCode.Width = 136
        '
        'cFTCountryName
        '
        Me.cFTCountryName.Caption = "FTCountryName"
        Me.cFTCountryName.FieldName = "FTCountryName"
        Me.cFTCountryName.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.cFTCountryName.Name = "cFTCountryName"
        Me.cFTCountryName.OptionsColumn.AllowEdit = False
        Me.cFTCountryName.Visible = True
        Me.cFTCountryName.VisibleIndex = 1
        Me.cFTCountryName.Width = 194
        '
        'cFTRemark
        '
        Me.cFTRemark.Caption = "FTRemark"
        Me.cFTRemark.FieldName = "FTRemark"
        Me.cFTRemark.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
        Me.cFTRemark.Name = "cFTRemark"
        Me.cFTRemark.OptionsColumn.AllowEdit = False
        Me.cFTRemark.Visible = True
        Me.cFTRemark.VisibleIndex = 2
        Me.cFTRemark.Width = 254
        '
        'wCountryForwarderMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1026, 842)
        Me.Controls.Add(Me.GroupControl1)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wCountryForwarderMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Country Frowarder"
        CType(Me.ReposFDShipDateTo.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFDShipDateTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemrFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemButtonFNHSysSuplId, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmdoc As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ReposFDShipDateTo As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents RepositoryItemFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemrFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmLoad As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclear As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNHSysCountryId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCountryCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCountryName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemButtonFNHSysSuplId As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents cFTRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmAdd As DevExpress.XtraEditors.SimpleButton
End Class
