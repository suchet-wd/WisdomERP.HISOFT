<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wMatQCSpare
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
        Me.components = New System.ComponentModel.Container()
        Me.DockManager = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmedit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmaddnew = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ogcDetail = New DevExpress.XtraGrid.GridControl()
        Me.ogvDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTMatTypeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTFTDescription = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSpareConditionTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSpareTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateActive = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTStateApp = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNStartQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNEndQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSpare = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTInsUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDInsDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTInsTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUpdUser = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDUpdDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUpdTime = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysMatTypeId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSpareConditionType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSpareType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFTStateSendApp = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryFTStateAppsdf = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.RepositoryItemFTStateSendApp = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.sFTMatGrpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTStateApp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTStateSendApp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFTStateAppsdf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTStateSendApp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DockManager
        '
        Me.DockManager.Form = Me
        Me.DockManager.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmedit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmaddnew)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(166, 222)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(744, 124)
        Me.ogbmainprocbutton.TabIndex = 393
        '
        'ocmedit
        '
        Me.ocmedit.Location = New System.Drawing.Point(187, 50)
        Me.ocmedit.Name = "ocmedit"
        Me.ocmedit.Size = New System.Drawing.Size(95, 25)
        Me.ocmedit.TabIndex = 100
        Me.ocmedit.TabStop = False
        Me.ocmedit.Tag = "2|"
        Me.ocmedit.Text = "EDIT"
        '
        'ocmaddnew
        '
        Me.ocmaddnew.Location = New System.Drawing.Point(17, 50)
        Me.ocmaddnew.Name = "ocmaddnew"
        Me.ocmaddnew.Size = New System.Drawing.Size(95, 25)
        Me.ocmaddnew.TabIndex = 99
        Me.ocmaddnew.TabStop = False
        Me.ocmaddnew.Tag = "2|"
        Me.ocmaddnew.Text = "NEW"
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(585, 7)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(95, 25)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(17, 7)
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
        Me.ogcDetail.Size = New System.Drawing.Size(1565, 574)
        Me.ogcDetail.TabIndex = 395
        Me.ogcDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDetail})
        '
        'ogvDetail
        '
        Me.ogvDetail.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.sFTMatGrpCode, Me.FTMatTypeCode, Me.FTFTDescription, Me.FNSpareConditionTypeName, Me.FNSpareTypeName, Me.FTStateActive, Me.FNSeq, Me.FNStartQty, Me.FNEndQty, Me.FNSpare, Me.FTRemark, Me.FTInsUser, Me.FDInsDate, Me.FTInsTime, Me.FTUpdUser, Me.FDUpdDate, Me.FTUpdTime, Me.FNHSysMatTypeId, Me.FNSpareConditionType, Me.FNSpareType})
        Me.ogvDetail.DetailHeight = 284
        Me.ogvDetail.GridControl = Me.ogcDetail
        Me.ogvDetail.Name = "ogvDetail"
        Me.ogvDetail.OptionsView.ColumnAutoWidth = False
        Me.ogvDetail.OptionsView.ShowGroupPanel = False
        '
        'FTMatTypeCode
        '
        Me.FTMatTypeCode.Caption = "Material Type Code"
        Me.FTMatTypeCode.FieldName = "FTMatTypeCode"
        Me.FTMatTypeCode.Name = "FTMatTypeCode"
        Me.FTMatTypeCode.OptionsColumn.AllowEdit = False
        Me.FTMatTypeCode.OptionsColumn.ReadOnly = True
        Me.FTMatTypeCode.Visible = True
        Me.FTMatTypeCode.VisibleIndex = 1
        Me.FTMatTypeCode.Width = 150
        '
        'FTFTDescription
        '
        Me.FTFTDescription.Caption = "Material  Type Name"
        Me.FTFTDescription.FieldName = "FTMainMatName"
        Me.FTFTDescription.Name = "FTFTDescription"
        Me.FTFTDescription.OptionsColumn.AllowEdit = False
        Me.FTFTDescription.OptionsColumn.ReadOnly = True
        Me.FTFTDescription.Visible = True
        Me.FTFTDescription.VisibleIndex = 2
        Me.FTFTDescription.Width = 250
        '
        'FNSpareConditionTypeName
        '
        Me.FNSpareConditionTypeName.Caption = "Calculate Type"
        Me.FNSpareConditionTypeName.FieldName = "FNSpareConditionTypeName"
        Me.FNSpareConditionTypeName.Name = "FNSpareConditionTypeName"
        Me.FNSpareConditionTypeName.OptionsColumn.AllowEdit = False
        Me.FNSpareConditionTypeName.OptionsColumn.ReadOnly = True
        Me.FNSpareConditionTypeName.Visible = True
        Me.FNSpareConditionTypeName.VisibleIndex = 3
        Me.FNSpareConditionTypeName.Width = 130
        '
        'FNSpareTypeName
        '
        Me.FNSpareTypeName.Caption = "FNSpareTypeName"
        Me.FNSpareTypeName.FieldName = "Spare Type"
        Me.FNSpareTypeName.Name = "FNSpareTypeName"
        Me.FNSpareTypeName.OptionsColumn.AllowEdit = False
        Me.FNSpareTypeName.OptionsColumn.ReadOnly = True
        Me.FNSpareTypeName.Visible = True
        Me.FNSpareTypeName.VisibleIndex = 4
        Me.FNSpareTypeName.Width = 107
        '
        'FTStateActive
        '
        Me.FTStateActive.Caption = "Active"
        Me.FTStateActive.ColumnEdit = Me.RepositoryFTStateApp
        Me.FTStateActive.FieldName = "FTStateActive"
        Me.FTStateActive.Name = "FTStateActive"
        Me.FTStateActive.OptionsColumn.AllowEdit = False
        Me.FTStateActive.OptionsColumn.ReadOnly = True
        Me.FTStateActive.Visible = True
        Me.FTStateActive.VisibleIndex = 5
        Me.FTStateActive.Width = 80
        '
        'RepositoryFTStateApp
        '
        Me.RepositoryFTStateApp.AutoHeight = False
        Me.RepositoryFTStateApp.Caption = "Check"
        Me.RepositoryFTStateApp.Name = "RepositoryFTStateApp"
        Me.RepositoryFTStateApp.ValueChecked = "1"
        Me.RepositoryFTStateApp.ValueUnchecked = "0"
        '
        'FNSeq
        '
        Me.FNSeq.Caption = "FNSeq"
        Me.FNSeq.FieldName = "FNSeq"
        Me.FNSeq.Name = "FNSeq"
        Me.FNSeq.OptionsColumn.AllowEdit = False
        Me.FNSeq.OptionsColumn.ReadOnly = True
        Me.FNSeq.Visible = True
        Me.FNSeq.VisibleIndex = 6
        '
        'FNStartQty
        '
        Me.FNStartQty.Caption = "Start Quantity"
        Me.FNStartQty.DisplayFormat.FormatString = "{0:n0}"
        Me.FNStartQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNStartQty.FieldName = "FNStartQty"
        Me.FNStartQty.Name = "FNStartQty"
        Me.FNStartQty.OptionsColumn.AllowEdit = False
        Me.FNStartQty.OptionsColumn.ReadOnly = True
        Me.FNStartQty.Visible = True
        Me.FNStartQty.VisibleIndex = 7
        Me.FNStartQty.Width = 90
        '
        'FNEndQty
        '
        Me.FNEndQty.Caption = "To Quantity"
        Me.FNEndQty.DisplayFormat.FormatString = "{0:n0}"
        Me.FNEndQty.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNEndQty.FieldName = "FNEndQty"
        Me.FNEndQty.Name = "FNEndQty"
        Me.FNEndQty.OptionsColumn.AllowEdit = False
        Me.FNEndQty.OptionsColumn.ReadOnly = True
        Me.FNEndQty.Visible = True
        Me.FNEndQty.VisibleIndex = 8
        Me.FNEndQty.Width = 92
        '
        'FNSpare
        '
        Me.FNSpare.Caption = "Spare"
        Me.FNSpare.DisplayFormat.FormatString = "{0:n2}"
        Me.FNSpare.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNSpare.FieldName = "FNSpare"
        Me.FNSpare.Name = "FNSpare"
        Me.FNSpare.OptionsColumn.AllowEdit = False
        Me.FNSpare.OptionsColumn.ReadOnly = True
        Me.FNSpare.Visible = True
        Me.FNSpare.VisibleIndex = 9
        '
        'FTRemark
        '
        Me.FTRemark.Caption = "Remark"
        Me.FTRemark.FieldName = "FTRemark"
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.OptionsColumn.AllowEdit = False
        Me.FTRemark.OptionsColumn.ReadOnly = True
        Me.FTRemark.Visible = True
        Me.FTRemark.VisibleIndex = 10
        '
        'FTInsUser
        '
        Me.FTInsUser.Caption = "FTInsUser"
        Me.FTInsUser.FieldName = "FTInsUser"
        Me.FTInsUser.Name = "FTInsUser"
        Me.FTInsUser.OptionsColumn.AllowEdit = False
        Me.FTInsUser.OptionsColumn.ReadOnly = True
        Me.FTInsUser.Visible = True
        Me.FTInsUser.VisibleIndex = 11
        Me.FTInsUser.Width = 120
        '
        'FDInsDate
        '
        Me.FDInsDate.Caption = "FDInsDate"
        Me.FDInsDate.FieldName = "FDInsDate"
        Me.FDInsDate.Name = "FDInsDate"
        Me.FDInsDate.OptionsColumn.AllowEdit = False
        Me.FDInsDate.OptionsColumn.ReadOnly = True
        Me.FDInsDate.Visible = True
        Me.FDInsDate.VisibleIndex = 12
        Me.FDInsDate.Width = 80
        '
        'FTInsTime
        '
        Me.FTInsTime.Caption = "FTInsTime"
        Me.FTInsTime.FieldName = "FTInsTime"
        Me.FTInsTime.Name = "FTInsTime"
        Me.FTInsTime.OptionsColumn.AllowEdit = False
        Me.FTInsTime.OptionsColumn.ReadOnly = True
        Me.FTInsTime.Visible = True
        Me.FTInsTime.VisibleIndex = 13
        Me.FTInsTime.Width = 80
        '
        'FTUpdUser
        '
        Me.FTUpdUser.Caption = "FTUpdUser"
        Me.FTUpdUser.FieldName = "FTUpdUser"
        Me.FTUpdUser.Name = "FTUpdUser"
        Me.FTUpdUser.OptionsColumn.AllowEdit = False
        Me.FTUpdUser.OptionsColumn.ReadOnly = True
        Me.FTUpdUser.Visible = True
        Me.FTUpdUser.VisibleIndex = 14
        Me.FTUpdUser.Width = 120
        '
        'FDUpdDate
        '
        Me.FDUpdDate.Caption = "FDUpdDate"
        Me.FDUpdDate.FieldName = "FDUpdDate"
        Me.FDUpdDate.Name = "FDUpdDate"
        Me.FDUpdDate.OptionsColumn.AllowEdit = False
        Me.FDUpdDate.OptionsColumn.ReadOnly = True
        Me.FDUpdDate.Visible = True
        Me.FDUpdDate.VisibleIndex = 15
        Me.FDUpdDate.Width = 80
        '
        'FTUpdTime
        '
        Me.FTUpdTime.Caption = "FTUpdTime"
        Me.FTUpdTime.FieldName = "FTUpdTime"
        Me.FTUpdTime.Name = "FTUpdTime"
        Me.FTUpdTime.OptionsColumn.AllowEdit = False
        Me.FTUpdTime.OptionsColumn.ReadOnly = True
        Me.FTUpdTime.Visible = True
        Me.FTUpdTime.VisibleIndex = 16
        Me.FTUpdTime.Width = 80
        '
        'FNHSysMatTypeId
        '
        Me.FNHSysMatTypeId.Caption = "FNHSysMatTypeId"
        Me.FNHSysMatTypeId.FieldName = "FNHSysMatTypeId"
        Me.FNHSysMatTypeId.Name = "FNHSysMatTypeId"
        Me.FNHSysMatTypeId.OptionsColumn.AllowEdit = False
        Me.FNHSysMatTypeId.OptionsColumn.ReadOnly = True
        '
        'FNSpareConditionType
        '
        Me.FNSpareConditionType.Caption = "FNSpareConditionType"
        Me.FNSpareConditionType.FieldName = "FNSpareConditionType"
        Me.FNSpareConditionType.Name = "FNSpareConditionType"
        Me.FNSpareConditionType.OptionsColumn.AllowEdit = False
        Me.FNSpareConditionType.OptionsColumn.ReadOnly = True
        '
        'FNSpareType
        '
        Me.FNSpareType.Caption = "FNSpareType"
        Me.FNSpareType.FieldName = "FNSpareType"
        Me.FNSpareType.Name = "FNSpareType"
        Me.FNSpareType.OptionsColumn.AllowEdit = False
        Me.FNSpareType.OptionsColumn.ReadOnly = True
        '
        'RepositoryFTStateSendApp
        '
        Me.RepositoryFTStateSendApp.AutoHeight = False
        Me.RepositoryFTStateSendApp.Caption = "Check"
        Me.RepositoryFTStateSendApp.Name = "RepositoryFTStateSendApp"
        Me.RepositoryFTStateSendApp.ValueChecked = "1"
        Me.RepositoryFTStateSendApp.ValueUnchecked = "0"
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
        'sFTMatGrpCode
        '
        Me.sFTMatGrpCode.Caption = "Mat Grp Code"
        Me.sFTMatGrpCode.FieldName = "FTMatGrpCode"
        Me.sFTMatGrpCode.Name = "sFTMatGrpCode"
        Me.sFTMatGrpCode.OptionsColumn.AllowEdit = False
        Me.sFTMatGrpCode.OptionsColumn.ReadOnly = True
        Me.sFTMatGrpCode.Visible = True
        Me.sFTMatGrpCode.VisibleIndex = 0
        Me.sFTMatGrpCode.Width = 88
        '
        'wMatQCSpare
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1565, 574)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogcDetail)
        Me.Name = "wMatQCSpare"
        Me.Text = "กำหนดรายการเผื่อการซื้อ Material"
        CType(Me.DockManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.ogcDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTStateApp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTStateSendApp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFTStateAppsdf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFTStateSendApp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DockManager As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogcDetail As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDetail As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryFTStateApp As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryFTStateSendApp As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryFTStateAppsdf As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepositoryItemFTStateSendApp As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTMatTypeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTFTDescription As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSpareConditionTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSpareTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateActive As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNStartQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNEndQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSpare As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTInsUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDInsDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTInsTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUpdUser As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDUpdDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUpdTime As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysMatTypeId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSpareConditionType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSpareType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmedit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmaddnew As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents sFTMatGrpCode As DevExpress.XtraGrid.Columns.GridColumn
End Class
