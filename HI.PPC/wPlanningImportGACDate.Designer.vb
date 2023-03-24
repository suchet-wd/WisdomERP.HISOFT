Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPlanningImportGACDate
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
        Me.ocmdoc = New DevExpress.XtraBars.Docking.DockManager(Me.components)
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otphistory = New DevExpress.XtraTab.XtraTabPage()
        Me.ogcdetailhistory = New DevExpress.XtraGrid.GridControl()
        Me.ogvdetailhistory = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.G2FTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2FTCmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2FTCustCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2FTCustName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2FTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2FTStyleName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2FTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2FTSubOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2FNQuantityExtra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2FNGarmentQtyTest = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2FNGrandQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2FDOriginalShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2FDShipDateOrginalO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNSeq = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2FDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2FDShipDateTo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.G2FDShipDateOrginal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GGFDShipDateOrginal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemDateEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.otpdetail = New DevExpress.XtraTab.XtraTabPage()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCustCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCustName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStyleName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSubOrderNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTPORef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTNikePOLineItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNQuantityExtra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNGarmentQtyTest = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNGrandQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDOriginalShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFDShipDateOrginalO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNTotalRevised = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDShipDateTo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFDShipDateTo = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.FDShipDateOrginal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFDShipDateOrginalTo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDOriginalShipDateT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDShipDateT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDShipDateToT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateChange = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFDShipDateOrginalToT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFDShipDateOrginalT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFTStateChangeO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFDShipDateOrginalOT = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GFNHSysCmpId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDCfmShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDORShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTReasonDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTContinentCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTContinentName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCountryCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCountryName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTShipModeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTShipModeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTProvinceCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTProvinceName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmsavedocument = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmsave = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmload = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmReadExcel = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otphistory.SuspendLayout()
        CType(Me.ogcdetailhistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvdetailhistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otpdetail.SuspendLayout()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFDShipDateTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFDShipDateTo.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.SuspendLayout()
        '
        'ocmdoc
        '
        Me.ocmdoc.Form = Me
        Me.ocmdoc.TopZIndexControls.AddRange(New String() {"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl"})
        '
        'otb
        '
        Me.otb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otb.Location = New System.Drawing.Point(0, 0)
        Me.otb.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otphistory
        Me.otb.Size = New System.Drawing.Size(1283, 703)
        Me.otb.TabIndex = 394
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpdetail, Me.otphistory})
        '
        'otphistory
        '
        Me.otphistory.Controls.Add(Me.ogcdetailhistory)
        Me.otphistory.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otphistory.Name = "otphistory"
        Me.otphistory.PageVisible = False
        Me.otphistory.Size = New System.Drawing.Size(1273, 666)
        Me.otphistory.Text = "Detail History"
        '
        'ogcdetailhistory
        '
        Me.ogcdetailhistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcdetailhistory.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetailhistory.Location = New System.Drawing.Point(0, 0)
        Me.ogcdetailhistory.MainView = Me.ogvdetailhistory
        Me.ogcdetailhistory.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcdetailhistory.Name = "ogcdetailhistory"
        Me.ogcdetailhistory.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemDateEdit1})
        Me.ogcdetailhistory.Size = New System.Drawing.Size(1273, 666)
        Me.ogcdetailhistory.TabIndex = 2
        Me.ogcdetailhistory.TabStop = False
        Me.ogcdetailhistory.Tag = "2|"
        Me.ogcdetailhistory.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvdetailhistory})
        '
        'ogvdetailhistory
        '
        Me.ogvdetailhistory.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.G2FTCmpCode, Me.G2FTCmpName, Me.G2FTCustCode, Me.G2FTCustName, Me.G2FTStyleCode, Me.G2FTStyleName, Me.G2FTOrderNo, Me.G2FTSubOrderNo, Me.G2FNQuantity, Me.G2FNQuantityExtra, Me.G2FNGarmentQtyTest, Me.G2FNGrandQuantity, Me.G2FDOriginalShipDate, Me.G2FDShipDateOrginalO, Me.FNSeq, Me.G2FDShipDate, Me.G2FDShipDateTo, Me.G2FDShipDateOrginal, Me.GGFDShipDateOrginal})
        Me.ogvdetailhistory.CustomizationFormBounds = New System.Drawing.Rectangle(1326, 384, 210, 194)
        Me.ogvdetailhistory.GridControl = Me.ogcdetailhistory
        Me.ogvdetailhistory.Name = "ogvdetailhistory"
        Me.ogvdetailhistory.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvdetailhistory.OptionsSelection.MultiSelect = True
        Me.ogvdetailhistory.OptionsView.ColumnAutoWidth = False
        Me.ogvdetailhistory.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvdetailhistory.OptionsView.ShowGroupPanel = False
        Me.ogvdetailhistory.Tag = "2|"
        '
        'G2FTCmpCode
        '
        Me.G2FTCmpCode.Caption = "FTCmpCode"
        Me.G2FTCmpCode.FieldName = "FTCmpCode"
        Me.G2FTCmpCode.Name = "G2FTCmpCode"
        Me.G2FTCmpCode.OptionsColumn.AllowEdit = False
        Me.G2FTCmpCode.OptionsColumn.ReadOnly = True
        Me.G2FTCmpCode.Visible = True
        Me.G2FTCmpCode.VisibleIndex = 0
        Me.G2FTCmpCode.Width = 100
        '
        'G2FTCmpName
        '
        Me.G2FTCmpName.Caption = "FTCmpName"
        Me.G2FTCmpName.FieldName = "FTCmpName"
        Me.G2FTCmpName.Name = "G2FTCmpName"
        Me.G2FTCmpName.OptionsColumn.AllowEdit = False
        Me.G2FTCmpName.OptionsColumn.ReadOnly = True
        Me.G2FTCmpName.Width = 150
        '
        'G2FTCustCode
        '
        Me.G2FTCustCode.Caption = "Customer Code"
        Me.G2FTCustCode.FieldName = "FTCustCode"
        Me.G2FTCustCode.Name = "G2FTCustCode"
        Me.G2FTCustCode.OptionsColumn.AllowEdit = False
        Me.G2FTCustCode.OptionsColumn.ReadOnly = True
        Me.G2FTCustCode.Width = 100
        '
        'G2FTCustName
        '
        Me.G2FTCustName.Caption = "Customer Name"
        Me.G2FTCustName.FieldName = "FTCustName"
        Me.G2FTCustName.Name = "G2FTCustName"
        Me.G2FTCustName.OptionsColumn.AllowEdit = False
        Me.G2FTCustName.OptionsColumn.ReadOnly = True
        Me.G2FTCustName.Width = 150
        '
        'G2FTStyleCode
        '
        Me.G2FTStyleCode.Caption = "FTStyleCode"
        Me.G2FTStyleCode.FieldName = "FTStyleCode"
        Me.G2FTStyleCode.Name = "G2FTStyleCode"
        Me.G2FTStyleCode.OptionsColumn.AllowEdit = False
        Me.G2FTStyleCode.OptionsColumn.ReadOnly = True
        Me.G2FTStyleCode.Visible = True
        Me.G2FTStyleCode.VisibleIndex = 1
        Me.G2FTStyleCode.Width = 100
        '
        'G2FTStyleName
        '
        Me.G2FTStyleName.Caption = "FTStyleName"
        Me.G2FTStyleName.FieldName = "FTStyleName"
        Me.G2FTStyleName.Name = "G2FTStyleName"
        Me.G2FTStyleName.OptionsColumn.AllowEdit = False
        Me.G2FTStyleName.OptionsColumn.ReadOnly = True
        Me.G2FTStyleName.Width = 150
        '
        'G2FTOrderNo
        '
        Me.G2FTOrderNo.Caption = "FTOrderNo"
        Me.G2FTOrderNo.FieldName = "FTOrderNo"
        Me.G2FTOrderNo.Name = "G2FTOrderNo"
        Me.G2FTOrderNo.OptionsColumn.AllowEdit = False
        Me.G2FTOrderNo.OptionsColumn.ReadOnly = True
        Me.G2FTOrderNo.Visible = True
        Me.G2FTOrderNo.VisibleIndex = 2
        Me.G2FTOrderNo.Width = 120
        '
        'G2FTSubOrderNo
        '
        Me.G2FTSubOrderNo.Caption = "FTSubOrderNo"
        Me.G2FTSubOrderNo.FieldName = "FTSubOrderNo"
        Me.G2FTSubOrderNo.Name = "G2FTSubOrderNo"
        Me.G2FTSubOrderNo.OptionsColumn.AllowEdit = False
        Me.G2FTSubOrderNo.OptionsColumn.ReadOnly = True
        Me.G2FTSubOrderNo.Visible = True
        Me.G2FTSubOrderNo.VisibleIndex = 3
        Me.G2FTSubOrderNo.Width = 114
        '
        'G2FNQuantity
        '
        Me.G2FNQuantity.Caption = "FNQuantity"
        Me.G2FNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.G2FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G2FNQuantity.FieldName = "FNQuantity"
        Me.G2FNQuantity.Name = "G2FNQuantity"
        Me.G2FNQuantity.OptionsColumn.AllowEdit = False
        Me.G2FNQuantity.OptionsColumn.ReadOnly = True
        '
        'G2FNQuantityExtra
        '
        Me.G2FNQuantityExtra.Caption = "FNQuantityExtra"
        Me.G2FNQuantityExtra.DisplayFormat.FormatString = "{0:n0}"
        Me.G2FNQuantityExtra.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G2FNQuantityExtra.FieldName = "FNQuantityExtra"
        Me.G2FNQuantityExtra.Name = "G2FNQuantityExtra"
        Me.G2FNQuantityExtra.OptionsColumn.AllowEdit = False
        Me.G2FNQuantityExtra.OptionsColumn.ReadOnly = True
        '
        'G2FNGarmentQtyTest
        '
        Me.G2FNGarmentQtyTest.Caption = "FNGarmentQtyTest"
        Me.G2FNGarmentQtyTest.DisplayFormat.FormatString = "{0:n0}"
        Me.G2FNGarmentQtyTest.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G2FNGarmentQtyTest.FieldName = "FNGarmentQtyTest"
        Me.G2FNGarmentQtyTest.Name = "G2FNGarmentQtyTest"
        Me.G2FNGarmentQtyTest.OptionsColumn.AllowEdit = False
        Me.G2FNGarmentQtyTest.OptionsColumn.ReadOnly = True
        '
        'G2FNGrandQuantity
        '
        Me.G2FNGrandQuantity.Caption = "FNGrandQuantity"
        Me.G2FNGrandQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.G2FNGrandQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.G2FNGrandQuantity.FieldName = "FNGrandQuantity"
        Me.G2FNGrandQuantity.Name = "G2FNGrandQuantity"
        Me.G2FNGrandQuantity.OptionsColumn.AllowEdit = False
        Me.G2FNGrandQuantity.OptionsColumn.ReadOnly = True
        '
        'G2FDOriginalShipDate
        '
        Me.G2FDOriginalShipDate.Caption = "Shipment Date ORG"
        Me.G2FDOriginalShipDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.G2FDOriginalShipDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.G2FDOriginalShipDate.FieldName = "FDOriginalShipDate"
        Me.G2FDOriginalShipDate.Name = "G2FDOriginalShipDate"
        Me.G2FDOriginalShipDate.OptionsColumn.AllowEdit = False
        Me.G2FDOriginalShipDate.OptionsColumn.ReadOnly = True
        Me.G2FDOriginalShipDate.Visible = True
        Me.G2FDOriginalShipDate.VisibleIndex = 4
        Me.G2FDOriginalShipDate.Width = 100
        '
        'G2FDShipDateOrginalO
        '
        Me.G2FDShipDateOrginalO.Caption = "O GAC Date Orginal"
        Me.G2FDShipDateOrginalO.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.G2FDShipDateOrginalO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.G2FDShipDateOrginalO.FieldName = "FDShipDateOrginalO"
        Me.G2FDShipDateOrginalO.Name = "G2FDShipDateOrginalO"
        Me.G2FDShipDateOrginalO.OptionsColumn.AllowEdit = False
        Me.G2FDShipDateOrginalO.OptionsColumn.ReadOnly = True
        Me.G2FDShipDateOrginalO.Visible = True
        Me.G2FDShipDateOrginalO.VisibleIndex = 5
        '
        'FNSeq
        '
        Me.FNSeq.Caption = "Seq"
        Me.FNSeq.FieldName = "FNSeq"
        Me.FNSeq.Name = "FNSeq"
        Me.FNSeq.OptionsColumn.AllowEdit = False
        Me.FNSeq.OptionsColumn.ReadOnly = True
        Me.FNSeq.Visible = True
        Me.FNSeq.VisibleIndex = 6
        '
        'G2FDShipDate
        '
        Me.G2FDShipDate.Caption = "FDShipDate"
        Me.G2FDShipDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.G2FDShipDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.G2FDShipDate.FieldName = "FDShipDate"
        Me.G2FDShipDate.Name = "G2FDShipDate"
        Me.G2FDShipDate.OptionsColumn.AllowEdit = False
        Me.G2FDShipDate.OptionsColumn.ReadOnly = True
        Me.G2FDShipDate.Visible = True
        Me.G2FDShipDate.VisibleIndex = 7
        '
        'G2FDShipDateTo
        '
        Me.G2FDShipDateTo.Caption = "FDShipDateTo"
        Me.G2FDShipDateTo.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.G2FDShipDateTo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.G2FDShipDateTo.FieldName = "FDShipDateTo"
        Me.G2FDShipDateTo.Name = "G2FDShipDateTo"
        Me.G2FDShipDateTo.OptionsColumn.AllowEdit = False
        Me.G2FDShipDateTo.OptionsColumn.ReadOnly = True
        Me.G2FDShipDateTo.Visible = True
        Me.G2FDShipDateTo.VisibleIndex = 8
        '
        'G2FDShipDateOrginal
        '
        Me.G2FDShipDateOrginal.Caption = "O GAC Date"
        Me.G2FDShipDateOrginal.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.G2FDShipDateOrginal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.G2FDShipDateOrginal.FieldName = "FDShipDateOrginal"
        Me.G2FDShipDateOrginal.Name = "G2FDShipDateOrginal"
        Me.G2FDShipDateOrginal.OptionsColumn.AllowEdit = False
        Me.G2FDShipDateOrginal.OptionsColumn.ReadOnly = True
        Me.G2FDShipDateOrginal.Visible = True
        Me.G2FDShipDateOrginal.VisibleIndex = 9
        '
        'GGFDShipDateOrginal
        '
        Me.GGFDShipDateOrginal.Caption = "O GAC Date To"
        Me.GGFDShipDateOrginal.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GGFDShipDateOrginal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GGFDShipDateOrginal.FieldName = "FDShipDateOrginalTo"
        Me.GGFDShipDateOrginal.Name = "GGFDShipDateOrginal"
        Me.GGFDShipDateOrginal.OptionsColumn.AllowEdit = False
        Me.GGFDShipDateOrginal.OptionsColumn.ReadOnly = True
        Me.GGFDShipDateOrginal.Visible = True
        Me.GGFDShipDateOrginal.VisibleIndex = 10
        '
        'RepositoryItemDateEdit1
        '
        Me.RepositoryItemDateEdit1.AutoHeight = False
        Me.RepositoryItemDateEdit1.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.EditFormat.FormatString = "dd/MM/yyyy"
        Me.RepositoryItemDateEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.RepositoryItemDateEdit1.Name = "RepositoryItemDateEdit1"
        '
        'otpdetail
        '
        Me.otpdetail.Controls.Add(Me.ogc)
        Me.otpdetail.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otpdetail.Name = "otpdetail"
        Me.otpdetail.Size = New System.Drawing.Size(1273, 666)
        Me.otpdetail.Text = "Detail"
        '
        'ogc
        '
        Me.ogc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Location = New System.Drawing.Point(0, 0)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.ReposFDShipDateTo, Me.RepositoryItemFTSelect})
        Me.ogc.Size = New System.Drawing.Size(1273, 666)
        Me.ogc.TabIndex = 1
        Me.ogc.TabStop = False
        Me.ogc.Tag = "2|"
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTCmpCode, Me.FTCmpName, Me.FTCustCode, Me.FTCustName, Me.FTStyleCode, Me.FTStyleName, Me.FTOrderNo, Me.FTSubOrderNo, Me.CFTPORef, Me.CFTNikePOLineItem, Me.FNQuantity, Me.FNQuantityExtra, Me.FNGarmentQtyTest, Me.FNGrandQuantity, Me.FDOriginalShipDate, Me.GFDShipDateOrginalO, Me.FNTotalRevised, Me.FDShipDate, Me.FDShipDateTo, Me.FDShipDateOrginal, Me.GFDShipDateOrginalTo, Me.FDOriginalShipDateT, Me.FDShipDateT, Me.FDShipDateToT, Me.FTStateChange, Me.GFDShipDateOrginalToT, Me.GFDShipDateOrginalT, Me.GFTStateChangeO, Me.GFDShipDateOrginalOT, Me.GFNHSysCmpId, Me.FDCfmShipDate, Me.FDORShipDate, Me.FTReasonDesc, Me.cFTContinentCode, Me.cFTContinentName, Me.cFTCountryCode, Me.cFTCountryName, Me.cFTShipModeCode, Me.cFTShipModeName, Me.cFTProvinceCode, Me.cFTProvinceName, Me.GridColumn1})
        Me.ogv.CustomizationFormBounds = New System.Drawing.Rectangle(1326, 384, 210, 194)
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsSelection.MultiSelect = True
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'FTCmpCode
        '
        Me.FTCmpCode.Caption = "FTCmpCode"
        Me.FTCmpCode.FieldName = "FTCmpCode"
        Me.FTCmpCode.Name = "FTCmpCode"
        Me.FTCmpCode.OptionsColumn.AllowEdit = False
        Me.FTCmpCode.OptionsColumn.ReadOnly = True
        Me.FTCmpCode.Visible = True
        Me.FTCmpCode.VisibleIndex = 0
        Me.FTCmpCode.Width = 100
        '
        'FTCmpName
        '
        Me.FTCmpName.Caption = "FTCmpName"
        Me.FTCmpName.FieldName = "FTCmpName"
        Me.FTCmpName.Name = "FTCmpName"
        Me.FTCmpName.OptionsColumn.AllowEdit = False
        Me.FTCmpName.OptionsColumn.ReadOnly = True
        Me.FTCmpName.Width = 150
        '
        'FTCustCode
        '
        Me.FTCustCode.Caption = "Customer Code"
        Me.FTCustCode.FieldName = "FTCustCode"
        Me.FTCustCode.Name = "FTCustCode"
        Me.FTCustCode.OptionsColumn.AllowEdit = False
        Me.FTCustCode.OptionsColumn.ReadOnly = True
        Me.FTCustCode.Width = 100
        '
        'FTCustName
        '
        Me.FTCustName.Caption = "Customer Name"
        Me.FTCustName.FieldName = "FTCustName"
        Me.FTCustName.Name = "FTCustName"
        Me.FTCustName.OptionsColumn.AllowEdit = False
        Me.FTCustName.OptionsColumn.ReadOnly = True
        Me.FTCustName.Width = 150
        '
        'FTStyleCode
        '
        Me.FTStyleCode.Caption = "FTStyleCode"
        Me.FTStyleCode.FieldName = "FTStyleCode"
        Me.FTStyleCode.Name = "FTStyleCode"
        Me.FTStyleCode.OptionsColumn.AllowEdit = False
        Me.FTStyleCode.OptionsColumn.ReadOnly = True
        Me.FTStyleCode.Visible = True
        Me.FTStyleCode.VisibleIndex = 1
        Me.FTStyleCode.Width = 100
        '
        'FTStyleName
        '
        Me.FTStyleName.Caption = "FTStyleName"
        Me.FTStyleName.FieldName = "FTStyleName"
        Me.FTStyleName.Name = "FTStyleName"
        Me.FTStyleName.OptionsColumn.AllowEdit = False
        Me.FTStyleName.OptionsColumn.ReadOnly = True
        Me.FTStyleName.Width = 150
        '
        'FTOrderNo
        '
        Me.FTOrderNo.Caption = "FTOrderNo"
        Me.FTOrderNo.FieldName = "FTOrderNo"
        Me.FTOrderNo.Name = "FTOrderNo"
        Me.FTOrderNo.OptionsColumn.AllowEdit = False
        Me.FTOrderNo.OptionsColumn.ReadOnly = True
        Me.FTOrderNo.Visible = True
        Me.FTOrderNo.VisibleIndex = 2
        Me.FTOrderNo.Width = 120
        '
        'FTSubOrderNo
        '
        Me.FTSubOrderNo.Caption = "FTSubOrderNo"
        Me.FTSubOrderNo.FieldName = "FTSubOrderNo"
        Me.FTSubOrderNo.Name = "FTSubOrderNo"
        Me.FTSubOrderNo.OptionsColumn.AllowEdit = False
        Me.FTSubOrderNo.OptionsColumn.ReadOnly = True
        Me.FTSubOrderNo.Visible = True
        Me.FTSubOrderNo.VisibleIndex = 3
        Me.FTSubOrderNo.Width = 114
        '
        'CFTPORef
        '
        Me.CFTPORef.Caption = "Customer PO"
        Me.CFTPORef.FieldName = "FTPORef"
        Me.CFTPORef.Name = "CFTPORef"
        Me.CFTPORef.OptionsColumn.AllowEdit = False
        Me.CFTPORef.OptionsColumn.ReadOnly = True
        Me.CFTPORef.Visible = True
        Me.CFTPORef.VisibleIndex = 5
        Me.CFTPORef.Width = 100
        '
        'CFTNikePOLineItem
        '
        Me.CFTNikePOLineItem.Caption = "PO Line."
        Me.CFTNikePOLineItem.FieldName = "FTNikePOLineItem"
        Me.CFTNikePOLineItem.Name = "CFTNikePOLineItem"
        Me.CFTNikePOLineItem.OptionsColumn.AllowEdit = False
        Me.CFTNikePOLineItem.OptionsColumn.ReadOnly = True
        Me.CFTNikePOLineItem.Visible = True
        Me.CFTNikePOLineItem.VisibleIndex = 4
        '
        'FNQuantity
        '
        Me.FNQuantity.Caption = "FNQuantity"
        Me.FNQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantity.FieldName = "FNQuantity"
        Me.FNQuantity.Name = "FNQuantity"
        Me.FNQuantity.OptionsColumn.AllowEdit = False
        Me.FNQuantity.OptionsColumn.ReadOnly = True
        Me.FNQuantity.Visible = True
        Me.FNQuantity.VisibleIndex = 13
        '
        'FNQuantityExtra
        '
        Me.FNQuantityExtra.Caption = "FNQuantityExtra"
        Me.FNQuantityExtra.DisplayFormat.FormatString = "{0:n0}"
        Me.FNQuantityExtra.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNQuantityExtra.FieldName = "FNQuantityExtra"
        Me.FNQuantityExtra.Name = "FNQuantityExtra"
        Me.FNQuantityExtra.OptionsColumn.AllowEdit = False
        Me.FNQuantityExtra.OptionsColumn.ReadOnly = True
        Me.FNQuantityExtra.Visible = True
        Me.FNQuantityExtra.VisibleIndex = 14
        '
        'FNGarmentQtyTest
        '
        Me.FNGarmentQtyTest.Caption = "FNGarmentQtyTest"
        Me.FNGarmentQtyTest.DisplayFormat.FormatString = "{0:n0}"
        Me.FNGarmentQtyTest.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNGarmentQtyTest.FieldName = "FNGarmentQtyTest"
        Me.FNGarmentQtyTest.Name = "FNGarmentQtyTest"
        Me.FNGarmentQtyTest.OptionsColumn.AllowEdit = False
        Me.FNGarmentQtyTest.OptionsColumn.ReadOnly = True
        Me.FNGarmentQtyTest.Visible = True
        Me.FNGarmentQtyTest.VisibleIndex = 15
        '
        'FNGrandQuantity
        '
        Me.FNGrandQuantity.Caption = "FNGrandQuantity"
        Me.FNGrandQuantity.DisplayFormat.FormatString = "{0:n0}"
        Me.FNGrandQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNGrandQuantity.FieldName = "FNGrandQuantity"
        Me.FNGrandQuantity.Name = "FNGrandQuantity"
        Me.FNGrandQuantity.OptionsColumn.AllowEdit = False
        Me.FNGrandQuantity.OptionsColumn.ReadOnly = True
        Me.FNGrandQuantity.Visible = True
        Me.FNGrandQuantity.VisibleIndex = 16
        '
        'FDOriginalShipDate
        '
        Me.FDOriginalShipDate.Caption = "Shipment Date ORG"
        Me.FDOriginalShipDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDOriginalShipDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDOriginalShipDate.FieldName = "FDOriginalShipDate"
        Me.FDOriginalShipDate.Name = "FDOriginalShipDate"
        Me.FDOriginalShipDate.OptionsColumn.AllowEdit = False
        Me.FDOriginalShipDate.OptionsColumn.ReadOnly = True
        Me.FDOriginalShipDate.Visible = True
        Me.FDOriginalShipDate.VisibleIndex = 17
        Me.FDOriginalShipDate.Width = 100
        '
        'GFDShipDateOrginalO
        '
        Me.GFDShipDateOrginalO.Caption = "O GAC Date Orginal"
        Me.GFDShipDateOrginalO.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GFDShipDateOrginalO.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GFDShipDateOrginalO.FieldName = "FDShipDateOrginalO"
        Me.GFDShipDateOrginalO.Name = "GFDShipDateOrginalO"
        Me.GFDShipDateOrginalO.OptionsColumn.AllowEdit = False
        Me.GFDShipDateOrginalO.OptionsColumn.ReadOnly = True
        '
        'FNTotalRevised
        '
        Me.FNTotalRevised.Caption = "FNTotalRevised"
        Me.FNTotalRevised.FieldName = "FNTotalRevised"
        Me.FNTotalRevised.Name = "FNTotalRevised"
        Me.FNTotalRevised.OptionsColumn.AllowEdit = False
        Me.FNTotalRevised.OptionsColumn.ReadOnly = True
        Me.FNTotalRevised.Visible = True
        Me.FNTotalRevised.VisibleIndex = 19
        '
        'FDShipDate
        '
        Me.FDShipDate.Caption = "FDShipDate"
        Me.FDShipDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDShipDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDShipDate.FieldName = "FDShipDate"
        Me.FDShipDate.Name = "FDShipDate"
        Me.FDShipDate.OptionsColumn.AllowEdit = False
        Me.FDShipDate.OptionsColumn.ReadOnly = True
        Me.FDShipDate.Visible = True
        Me.FDShipDate.VisibleIndex = 18
        '
        'FDShipDateTo
        '
        Me.FDShipDateTo.Caption = "FDShipDateTo"
        Me.FDShipDateTo.ColumnEdit = Me.ReposFDShipDateTo
        Me.FDShipDateTo.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDShipDateTo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDShipDateTo.FieldName = "FDShipDateTo"
        Me.FDShipDateTo.Name = "FDShipDateTo"
        Me.FDShipDateTo.OptionsColumn.AllowEdit = False
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
        'FDShipDateOrginal
        '
        Me.FDShipDateOrginal.Caption = "O GAC Date"
        Me.FDShipDateOrginal.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDShipDateOrginal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDShipDateOrginal.FieldName = "FDShipDateOrginal"
        Me.FDShipDateOrginal.Name = "FDShipDateOrginal"
        Me.FDShipDateOrginal.OptionsColumn.AllowEdit = False
        Me.FDShipDateOrginal.OptionsColumn.ReadOnly = True
        '
        'GFDShipDateOrginalTo
        '
        Me.GFDShipDateOrginalTo.Caption = "O GAC Date To"
        Me.GFDShipDateOrginalTo.ColumnEdit = Me.ReposFDShipDateTo
        Me.GFDShipDateOrginalTo.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.GFDShipDateOrginalTo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.GFDShipDateOrginalTo.FieldName = "FDShipDateOrginalTo"
        Me.GFDShipDateOrginalTo.Name = "GFDShipDateOrginalTo"
        Me.GFDShipDateOrginalTo.OptionsColumn.AllowEdit = False
        '
        'FDOriginalShipDateT
        '
        Me.FDOriginalShipDateT.Caption = "FDOriginalShipDateT"
        Me.FDOriginalShipDateT.FieldName = "FDOriginalShipDateT"
        Me.FDOriginalShipDateT.Name = "FDOriginalShipDateT"
        '
        'FDShipDateT
        '
        Me.FDShipDateT.Caption = "FDShipDateT"
        Me.FDShipDateT.FieldName = "FDShipDateT"
        Me.FDShipDateT.Name = "FDShipDateT"
        '
        'FDShipDateToT
        '
        Me.FDShipDateToT.Caption = "FDShipDateToT"
        Me.FDShipDateToT.FieldName = "FDShipDateToT"
        Me.FDShipDateToT.Name = "FDShipDateToT"
        '
        'FTStateChange
        '
        Me.FTStateChange.Caption = "FTStateChange"
        Me.FTStateChange.FieldName = "FTStateChange"
        Me.FTStateChange.Name = "FTStateChange"
        '
        'GFDShipDateOrginalToT
        '
        Me.GFDShipDateOrginalToT.Caption = "FDShipDateOrginalToT"
        Me.GFDShipDateOrginalToT.FieldName = "FDShipDateOrginalToT"
        Me.GFDShipDateOrginalToT.Name = "GFDShipDateOrginalToT"
        '
        'GFDShipDateOrginalT
        '
        Me.GFDShipDateOrginalT.Caption = "FDShipDateOrginalT"
        Me.GFDShipDateOrginalT.FieldName = "FDShipDateOrginalT"
        Me.GFDShipDateOrginalT.Name = "GFDShipDateOrginalT"
        '
        'GFTStateChangeO
        '
        Me.GFTStateChangeO.Caption = "FTStateChangeO"
        Me.GFTStateChangeO.FieldName = "FTStateChangeO"
        Me.GFTStateChangeO.Name = "GFTStateChangeO"
        '
        'GFDShipDateOrginalOT
        '
        Me.GFDShipDateOrginalOT.Caption = "FDShipDateOrginalOT"
        Me.GFDShipDateOrginalOT.FieldName = "FDShipDateOrginalOT"
        Me.GFDShipDateOrginalOT.Name = "GFDShipDateOrginalOT"
        '
        'GFNHSysCmpId
        '
        Me.GFNHSysCmpId.Caption = "FNHSysCmpId"
        Me.GFNHSysCmpId.FieldName = "FNHSysCmpId"
        Me.GFNHSysCmpId.Name = "GFNHSysCmpId"
        '
        'FDCfmShipDate
        '
        Me.FDCfmShipDate.Caption = "FDCfmShipDate"
        Me.FDCfmShipDate.ColumnEdit = Me.ReposFDShipDateTo
        Me.FDCfmShipDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDCfmShipDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDCfmShipDate.FieldName = "FDCfmShipDate"
        Me.FDCfmShipDate.Name = "FDCfmShipDate"
        Me.FDCfmShipDate.OptionsColumn.AllowEdit = False
        Me.FDCfmShipDate.Visible = True
        Me.FDCfmShipDate.VisibleIndex = 21
        Me.FDCfmShipDate.Width = 108
        '
        'FDORShipDate
        '
        Me.FDORShipDate.Caption = "FDORShipDate"
        Me.FDORShipDate.ColumnEdit = Me.ReposFDShipDateTo
        Me.FDORShipDate.DisplayFormat.FormatString = "dd/MM/yyyy"
        Me.FDORShipDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.FDORShipDate.FieldName = "FDORShipDate"
        Me.FDORShipDate.Name = "FDORShipDate"
        Me.FDORShipDate.OptionsColumn.AllowEdit = False
        Me.FDORShipDate.Visible = True
        Me.FDORShipDate.VisibleIndex = 20
        Me.FDORShipDate.Width = 129
        '
        'FTReasonDesc
        '
        Me.FTReasonDesc.Caption = "FTReasonDesc"
        Me.FTReasonDesc.FieldName = "FTReasonDesc"
        Me.FTReasonDesc.Name = "FTReasonDesc"
        Me.FTReasonDesc.Width = 350
        '
        'cFTContinentCode
        '
        Me.cFTContinentCode.Caption = "FTContinentCode"
        Me.cFTContinentCode.FieldName = "FTContinentCode"
        Me.cFTContinentCode.Name = "cFTContinentCode"
        Me.cFTContinentCode.OptionsColumn.AllowEdit = False
        Me.cFTContinentCode.Visible = True
        Me.cFTContinentCode.VisibleIndex = 6
        Me.cFTContinentCode.Width = 109
        '
        'cFTContinentName
        '
        Me.cFTContinentName.Caption = "FTContinentName"
        Me.cFTContinentName.FieldName = "FTContinentName"
        Me.cFTContinentName.Name = "cFTContinentName"
        Me.cFTContinentName.OptionsColumn.AllowEdit = False
        Me.cFTContinentName.Visible = True
        Me.cFTContinentName.VisibleIndex = 7
        Me.cFTContinentName.Width = 144
        '
        'cFTCountryCode
        '
        Me.cFTCountryCode.Caption = "FTCountryCode"
        Me.cFTCountryCode.FieldName = "FTCountryCode"
        Me.cFTCountryCode.Name = "cFTCountryCode"
        Me.cFTCountryCode.OptionsColumn.AllowEdit = False
        Me.cFTCountryCode.Visible = True
        Me.cFTCountryCode.VisibleIndex = 8
        Me.cFTCountryCode.Width = 96
        '
        'cFTCountryName
        '
        Me.cFTCountryName.Caption = "FTCountryName"
        Me.cFTCountryName.FieldName = "FTCountryName"
        Me.cFTCountryName.Name = "cFTCountryName"
        Me.cFTCountryName.OptionsColumn.AllowEdit = False
        Me.cFTCountryName.Visible = True
        Me.cFTCountryName.VisibleIndex = 9
        Me.cFTCountryName.Width = 157
        '
        'cFTShipModeCode
        '
        Me.cFTShipModeCode.Caption = "FTShipModeCode"
        Me.cFTShipModeCode.FieldName = "FTShipModeCode"
        Me.cFTShipModeCode.Name = "cFTShipModeCode"
        Me.cFTShipModeCode.OptionsColumn.AllowEdit = False
        Me.cFTShipModeCode.Visible = True
        Me.cFTShipModeCode.VisibleIndex = 12
        '
        'cFTShipModeName
        '
        Me.cFTShipModeName.Caption = "FTShipModeName"
        Me.cFTShipModeName.FieldName = "FTShipModeName"
        Me.cFTShipModeName.Name = "cFTShipModeName"
        Me.cFTShipModeName.OptionsColumn.AllowEdit = False
        '
        'cFTProvinceCode
        '
        Me.cFTProvinceCode.Caption = "FTProvinceCode"
        Me.cFTProvinceCode.FieldName = "FTProvinceCode"
        Me.cFTProvinceCode.Name = "cFTProvinceCode"
        Me.cFTProvinceCode.OptionsColumn.AllowEdit = False
        Me.cFTProvinceCode.Visible = True
        Me.cFTProvinceCode.VisibleIndex = 10
        Me.cFTProvinceCode.Width = 86
        '
        'cFTProvinceName
        '
        Me.cFTProvinceName.Caption = "FTProvinceName"
        Me.cFTProvinceName.FieldName = "FTProvinceName"
        Me.cFTProvinceName.Name = "cFTProvinceName"
        Me.cFTProvinceName.OptionsColumn.AllowEdit = False
        Me.cFTProvinceName.Visible = True
        Me.cFTProvinceName.VisibleIndex = 11
        Me.cFTProvinceName.Width = 123
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "GridColumn1"
        Me.GridColumn1.ColumnEdit = Me.RepositoryItemFTSelect
        Me.GridColumn1.FieldName = "FTSelect"
        Me.GridColumn1.Name = "GridColumn1"
        '
        'RepositoryItemFTSelect
        '
        Me.RepositoryItemFTSelect.AutoHeight = False
        Me.RepositoryItemFTSelect.Name = "RepositoryItemFTSelect"
        Me.RepositoryItemFTSelect.ValueChecked = "1"
        Me.RepositoryItemFTSelect.ValueUnchecked = "0"
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmReadExcel)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsavedocument)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmsave)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmexit)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmload)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(139, 330)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(997, 111)
        Me.ogbmainprocbutton.TabIndex = 397
        '
        'ocmsavedocument
        '
        Me.ocmsavedocument.Location = New System.Drawing.Point(581, 39)
        Me.ocmsavedocument.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsavedocument.Name = "ocmsavedocument"
        Me.ocmsavedocument.Size = New System.Drawing.Size(111, 31)
        Me.ocmsavedocument.TabIndex = 97
        Me.ocmsavedocument.TabStop = False
        Me.ocmsavedocument.Tag = "2|"
        Me.ocmsavedocument.Text = "SAVE"
        '
        'ocmsave
        '
        Me.ocmsave.Location = New System.Drawing.Point(443, 39)
        Me.ocmsave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsave.Name = "ocmsave"
        Me.ocmsave.Size = New System.Drawing.Size(111, 31)
        Me.ocmsave.TabIndex = 97
        Me.ocmsave.TabStop = False
        Me.ocmsave.Tag = "2|"
        Me.ocmsave.Text = "SAVE"
        Me.ocmsave.Visible = False
        '
        'ocmexit
        '
        Me.ocmexit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmexit.Location = New System.Drawing.Point(812, 9)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmload
        '
        Me.ocmload.Location = New System.Drawing.Point(20, 6)
        Me.ocmload.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmload.Name = "ocmload"
        Me.ocmload.Size = New System.Drawing.Size(111, 31)
        Me.ocmload.TabIndex = 93
        Me.ocmload.TabStop = False
        Me.ocmload.Tag = "2|"
        Me.ocmload.Text = "LoadData"
        '
        'ocmReadExcel
        '
        Me.ocmReadExcel.Location = New System.Drawing.Point(217, 30)
        Me.ocmReadExcel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmReadExcel.Name = "ocmReadExcel"
        Me.ocmReadExcel.Size = New System.Drawing.Size(111, 31)
        Me.ocmReadExcel.TabIndex = 100
        Me.ocmReadExcel.TabStop = False
        Me.ocmReadExcel.Tag = "2|"
        Me.ocmReadExcel.Text = "READ EXCEL FILE"
        '
        'wPlanningImportGACDate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1283, 703)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.otb)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wPlanningImportGACDate"
        Me.Text = "Import GAC Date "
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otphistory.ResumeLayout(False)
        CType(Me.ogcdetailhistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvdetailhistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemDateEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otpdetail.ResumeLayout(False)
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFDShipDateTo.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFDShipDateTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmdoc As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpdetail As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStyleName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDOriginalShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCustCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCustName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSubOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNQuantityExtra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNGarmentQtyTest As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNGrandQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNTotalRevised As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDShipDateTo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFDShipDateTo As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents FDOriginalShipDateT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDShipDateT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDShipDateToT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateChange As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmsave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GFDShipDateOrginalO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDShipDateOrginal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFDShipDateOrginalTo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFDShipDateOrginalToT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFDShipDateOrginalT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFTStateChangeO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GFDShipDateOrginalOT As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents otphistory As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogcdetailhistory As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvdetailhistory As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents G2FTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2FTCmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2FTCustCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2FTCustName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2FTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2FTStyleName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2FTOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2FTSubOrderNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2FNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2FNQuantityExtra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2FNGarmentQtyTest As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2FNGrandQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2FDOriginalShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2FDShipDateOrginalO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNSeq As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2FDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2FDShipDateTo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents G2FDShipDateOrginal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GGFDShipDateOrginal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemDateEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents GFNHSysCmpId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTPORef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTNikePOLineItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDCfmShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDORShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTReasonDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmsavedocument As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents cFTContinentCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTContinentName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCountryCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCountryName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTShipModeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTShipModeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTProvinceCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTProvinceName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ocmReadExcel As DevExpress.XtraEditors.SimpleButton
End Class
