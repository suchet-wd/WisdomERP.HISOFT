Imports DevExpress.XtraEditors.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wPopupSelectItem
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
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogvBarcode = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.sFTBarCodeEAN13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sFTCartonInfo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sFTCartonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sTotalCbm = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.sFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemsFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.cFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTBarCodeEAN13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCustomerPO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTPOLine = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCartonInfo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTCartonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cTotalCbm = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFTStyleName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysStyleId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFDShipDateTo = New DevExpress.XtraEditors.Repository.RepositoryItemDateEdit()
        Me.RepositoryItemFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.RepositoryItemrFTSelect = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ocmdoc = New DevExpress.XtraBars.Docking.DockManager()
        Me.otb = New DevExpress.XtraTab.XtraTabControl()
        Me.otpdetail = New DevExpress.XtraTab.XtraTabPage()
        Me.otabporef = New DevExpress.XtraTab.XtraTabPage()
        Me.ogcsum = New DevExpress.XtraGrid.GridControl()
        Me.ogvsum = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gFTSelect = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.gFTCustomerPO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gFTPOLine = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gFNQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gFNTotalCarton = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gTotalCbm = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gFTStyleName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gFDShipDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gFNPackCount = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gFTColorway = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmselect = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.oChkSelectAll = New DevExpress.XtraEditors.CheckEdit()
        Me.FNTotalCarton_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNTotalCBM_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNTotalCarton = New DevExpress.XtraEditors.CalcEdit()
        Me.FNTotalCBM = New DevExpress.XtraEditors.CalcEdit()
        Me.ocmsavelayout = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogvBarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemsFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFDShipDateTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFDShipDateTo.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemrFTSelect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otb.SuspendLayout()
        Me.otpdetail.SuspendLayout()
        Me.otabporef.SuspendLayout()
        CType(Me.ogcsum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvsum, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.oChkSelectAll.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNTotalCarton.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNTotalCBM.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogvBarcode
        '
        Me.ogvBarcode.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.sFTBarCodeEAN13, Me.sFTCartonInfo, Me.sFNQuantity, Me.sFTCartonCode, Me.sTotalCbm, Me.sFTSelect})
        Me.ogvBarcode.GridControl = Me.ogc
        Me.ogvBarcode.Name = "ogvBarcode"
        Me.ogvBarcode.OptionsView.ShowGroupPanel = False
        '
        'sFTBarCodeEAN13
        '
        Me.sFTBarCodeEAN13.Caption = "FTBarCodeEAN13"
        Me.sFTBarCodeEAN13.FieldName = "FTBarCodeEAN13"
        Me.sFTBarCodeEAN13.Name = "sFTBarCodeEAN13"
        Me.sFTBarCodeEAN13.OptionsColumn.AllowEdit = False
        Me.sFTBarCodeEAN13.Visible = True
        Me.sFTBarCodeEAN13.VisibleIndex = 1
        Me.sFTBarCodeEAN13.Width = 403
        '
        'sFTCartonInfo
        '
        Me.sFTCartonInfo.Caption = "FTCartonInfo"
        Me.sFTCartonInfo.FieldName = "FTCartonInfo"
        Me.sFTCartonInfo.Name = "sFTCartonInfo"
        Me.sFTCartonInfo.OptionsColumn.AllowEdit = False
        Me.sFTCartonInfo.Visible = True
        Me.sFTCartonInfo.VisibleIndex = 2
        Me.sFTCartonInfo.Width = 403
        '
        'sFNQuantity
        '
        Me.sFNQuantity.Caption = "FNQuantity"
        Me.sFNQuantity.DisplayFormat.FormatString = "N0"
        Me.sFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.sFNQuantity.FieldName = "FNQuantity"
        Me.sFNQuantity.Name = "sFNQuantity"
        Me.sFNQuantity.OptionsColumn.AllowEdit = False
        Me.sFNQuantity.Visible = True
        Me.sFNQuantity.VisibleIndex = 3
        Me.sFNQuantity.Width = 403
        '
        'sFTCartonCode
        '
        Me.sFTCartonCode.Caption = "FTCartonCode"
        Me.sFTCartonCode.FieldName = "FTCartonCode"
        Me.sFTCartonCode.Name = "sFTCartonCode"
        Me.sFTCartonCode.OptionsColumn.AllowEdit = False
        Me.sFTCartonCode.Visible = True
        Me.sFTCartonCode.VisibleIndex = 4
        Me.sFTCartonCode.Width = 403
        '
        'sTotalCbm
        '
        Me.sTotalCbm.Caption = "Cbm"
        Me.sTotalCbm.DisplayFormat.FormatString = "N5"
        Me.sTotalCbm.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.sTotalCbm.FieldName = "TotalCbm"
        Me.sTotalCbm.Name = "sTotalCbm"
        Me.sTotalCbm.OptionsColumn.AllowEdit = False
        Me.sTotalCbm.Visible = True
        Me.sTotalCbm.VisibleIndex = 5
        Me.sTotalCbm.Width = 412
        '
        'sFTSelect
        '
        Me.sFTSelect.Caption = "#"
        Me.sFTSelect.ColumnEdit = Me.RepositoryItemsFTSelect
        Me.sFTSelect.FieldName = "FTSelect"
        Me.sFTSelect.Name = "sFTSelect"
        Me.sFTSelect.Visible = True
        Me.sFTSelect.VisibleIndex = 0
        Me.sFTSelect.Width = 86
        '
        'RepositoryItemsFTSelect
        '
        Me.RepositoryItemsFTSelect.AutoHeight = False
        Me.RepositoryItemsFTSelect.Name = "RepositoryItemsFTSelect"
        Me.RepositoryItemsFTSelect.ValueChecked = "1"
        Me.RepositoryItemsFTSelect.ValueUnchecked = "0"
        '
        'ogc
        '
        Me.ogc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Location = New System.Drawing.Point(0, 0)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemsFTSelect})
        Me.ogc.Size = New System.Drawing.Size(1015, 702)
        Me.ogc.TabIndex = 1
        Me.ogc.TabStop = False
        Me.ogc.Tag = "2|"
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv, Me.ogvBarcode})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.cFTSelect, Me.cFTBarCodeEAN13, Me.cFTCustomerPO, Me.cFTPOLine, Me.cFTCartonInfo, Me.cFNQuantity, Me.cFTCartonCode, Me.cTotalCbm, Me.cFTStyleCode, Me.cFTStyleName, Me.cFNHSysStyleId, Me.cFDShipDate})
        Me.ogv.CustomizationFormBounds = New System.Drawing.Rectangle(1326, 384, 210, 194)
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogv.OptionsSelection.MultiSelect = True
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogv.OptionsView.ShowFooter = True
        Me.ogv.OptionsView.ShowGroupPanel = False
        Me.ogv.Tag = "2|"
        '
        'cFTSelect
        '
        Me.cFTSelect.Caption = "#"
        Me.cFTSelect.ColumnEdit = Me.RepositoryItemsFTSelect
        Me.cFTSelect.FieldName = "FTSelect"
        Me.cFTSelect.Name = "cFTSelect"
        Me.cFTSelect.Visible = True
        Me.cFTSelect.VisibleIndex = 0
        Me.cFTSelect.Width = 59
        '
        'cFTBarCodeEAN13
        '
        Me.cFTBarCodeEAN13.Caption = "FTBarCodeEAN13"
        Me.cFTBarCodeEAN13.FieldName = "FTBarCodeEAN13"
        Me.cFTBarCodeEAN13.Name = "cFTBarCodeEAN13"
        Me.cFTBarCodeEAN13.OptionsColumn.AllowEdit = False
        Me.cFTBarCodeEAN13.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "FTBarCodeEAN13", "{0:N0}")})
        Me.cFTBarCodeEAN13.Visible = True
        Me.cFTBarCodeEAN13.VisibleIndex = 1
        Me.cFTBarCodeEAN13.Width = 148
        '
        'cFTCustomerPO
        '
        Me.cFTCustomerPO.Caption = "FTCustomerPO"
        Me.cFTCustomerPO.FieldName = "FTCustomerPO"
        Me.cFTCustomerPO.Name = "cFTCustomerPO"
        Me.cFTCustomerPO.OptionsColumn.AllowEdit = False
        Me.cFTCustomerPO.Visible = True
        Me.cFTCustomerPO.VisibleIndex = 4
        Me.cFTCustomerPO.Width = 132
        '
        'cFTPOLine
        '
        Me.cFTPOLine.Caption = "FTPOLine"
        Me.cFTPOLine.FieldName = "FTPOLine"
        Me.cFTPOLine.Name = "cFTPOLine"
        Me.cFTPOLine.OptionsColumn.AllowEdit = False
        Me.cFTPOLine.Visible = True
        Me.cFTPOLine.VisibleIndex = 5
        Me.cFTPOLine.Width = 116
        '
        'cFTCartonInfo
        '
        Me.cFTCartonInfo.Caption = "FTCartonInfo"
        Me.cFTCartonInfo.FieldName = "FTCartonInfo"
        Me.cFTCartonInfo.Name = "cFTCartonInfo"
        Me.cFTCartonInfo.OptionsColumn.AllowEdit = False
        Me.cFTCartonInfo.Visible = True
        Me.cFTCartonInfo.VisibleIndex = 7
        Me.cFTCartonInfo.Width = 114
        '
        'cFNQuantity
        '
        Me.cFNQuantity.Caption = "FNQuantity"
        Me.cFNQuantity.DisplayFormat.FormatString = "N0"
        Me.cFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cFNQuantity.FieldName = "FNQuantity"
        Me.cFNQuantity.Name = "cFNQuantity"
        Me.cFNQuantity.OptionsColumn.AllowEdit = False
        Me.cFNQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", "{0:N0}")})
        Me.cFNQuantity.Visible = True
        Me.cFNQuantity.VisibleIndex = 8
        Me.cFNQuantity.Width = 122
        '
        'cFTCartonCode
        '
        Me.cFTCartonCode.Caption = "FTCartonCode"
        Me.cFTCartonCode.FieldName = "FTCartonCode"
        Me.cFTCartonCode.Name = "cFTCartonCode"
        Me.cFTCartonCode.OptionsColumn.AllowEdit = False
        Me.cFTCartonCode.Visible = True
        Me.cFTCartonCode.VisibleIndex = 9
        Me.cFTCartonCode.Width = 131
        '
        'cTotalCbm
        '
        Me.cTotalCbm.Caption = "TotalCbm"
        Me.cTotalCbm.DisplayFormat.FormatString = "N4"
        Me.cTotalCbm.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.cTotalCbm.FieldName = "TotalCbm"
        Me.cTotalCbm.Name = "cTotalCbm"
        Me.cTotalCbm.OptionsColumn.AllowEdit = False
        Me.cTotalCbm.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalCbm", "{0:N4}")})
        Me.cTotalCbm.Visible = True
        Me.cTotalCbm.VisibleIndex = 10
        Me.cTotalCbm.Width = 114
        '
        'cFTStyleCode
        '
        Me.cFTStyleCode.Caption = "FTStyleCode"
        Me.cFTStyleCode.FieldName = "FTStyleCode"
        Me.cFTStyleCode.Name = "cFTStyleCode"
        Me.cFTStyleCode.OptionsColumn.AllowEdit = False
        Me.cFTStyleCode.Visible = True
        Me.cFTStyleCode.VisibleIndex = 2
        Me.cFTStyleCode.Width = 127
        '
        'cFTStyleName
        '
        Me.cFTStyleName.Caption = "FTStyleName"
        Me.cFTStyleName.FieldName = "FTStyleName"
        Me.cFTStyleName.Name = "cFTStyleName"
        Me.cFTStyleName.OptionsColumn.AllowEdit = False
        Me.cFTStyleName.Visible = True
        Me.cFTStyleName.VisibleIndex = 3
        Me.cFTStyleName.Width = 121
        '
        'cFNHSysStyleId
        '
        Me.cFNHSysStyleId.Caption = "FNHSysStyleId"
        Me.cFNHSysStyleId.FieldName = "FNHSysStyleId"
        Me.cFNHSysStyleId.Name = "cFNHSysStyleId"
        '
        'cFDShipDate
        '
        Me.cFDShipDate.Caption = "FDShipDate"
        Me.cFDShipDate.FieldName = "FDShipDate"
        Me.cFDShipDate.Name = "cFDShipDate"
        Me.cFDShipDate.OptionsColumn.AllowEdit = False
        Me.cFDShipDate.Visible = True
        Me.cFDShipDate.VisibleIndex = 6
        Me.cFDShipDate.Width = 136
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
        'otb
        '
        Me.otb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otb.Location = New System.Drawing.Point(0, 0)
        Me.otb.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.otb.Name = "otb"
        Me.otb.SelectedTabPage = Me.otpdetail
        Me.otb.Size = New System.Drawing.Size(1025, 739)
        Me.otb.TabIndex = 394
        Me.otb.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otabporef, Me.otpdetail})
        '
        'otpdetail
        '
        Me.otpdetail.Controls.Add(Me.ogc)
        Me.otpdetail.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otpdetail.Name = "otpdetail"
        Me.otpdetail.PageVisible = False
        Me.otpdetail.Size = New System.Drawing.Size(1015, 702)
        Me.otpdetail.Text = "Detail"
        '
        'otabporef
        '
        Me.otabporef.Controls.Add(Me.ogcsum)
        Me.otabporef.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.otabporef.Name = "otabporef"
        Me.otabporef.Size = New System.Drawing.Size(1018, 705)
        Me.otabporef.Text = "Customer PO Select"
        '
        'ogcsum
        '
        Me.ogcsum.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogcsum.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcsum.Location = New System.Drawing.Point(0, 0)
        Me.ogcsum.MainView = Me.ogvsum
        Me.ogcsum.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogcsum.Name = "ogcsum"
        Me.ogcsum.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.ogcsum.Size = New System.Drawing.Size(1018, 705)
        Me.ogcsum.TabIndex = 2
        Me.ogcsum.TabStop = False
        Me.ogcsum.Tag = "2|"
        Me.ogcsum.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvsum, Me.GridView2})
        '
        'ogvsum
        '
        Me.ogvsum.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gFTSelect, Me.gFTCustomerPO, Me.gFTPOLine, Me.gFNQuantity, Me.gFNTotalCarton, Me.gTotalCbm, Me.gFTStyleCode, Me.gFTStyleName, Me.GridColumn11, Me.gFDShipDate, Me.gFNPackCount, Me.gFTColorway})
        Me.ogvsum.CustomizationFormBounds = New System.Drawing.Rectangle(1326, 384, 210, 194)
        Me.ogvsum.GridControl = Me.ogcsum
        Me.ogvsum.Name = "ogvsum"
        Me.ogvsum.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvsum.OptionsSelection.MultiSelect = True
        Me.ogvsum.OptionsView.ColumnAutoWidth = False
        Me.ogvsum.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvsum.OptionsView.ShowFooter = True
        Me.ogvsum.OptionsView.ShowGroupPanel = False
        Me.ogvsum.Tag = "2|"
        '
        'gFTSelect
        '
        Me.gFTSelect.Caption = "#"
        Me.gFTSelect.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.gFTSelect.FieldName = "FTSelect"
        Me.gFTSelect.Name = "gFTSelect"
        Me.gFTSelect.Visible = True
        Me.gFTSelect.VisibleIndex = 0
        Me.gFTSelect.Width = 59
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'gFTCustomerPO
        '
        Me.gFTCustomerPO.Caption = "FTCustomerPO"
        Me.gFTCustomerPO.FieldName = "FTCustomerPO"
        Me.gFTCustomerPO.Name = "gFTCustomerPO"
        Me.gFTCustomerPO.OptionsColumn.AllowEdit = False
        Me.gFTCustomerPO.Visible = True
        Me.gFTCustomerPO.VisibleIndex = 1
        Me.gFTCustomerPO.Width = 132
        '
        'gFTPOLine
        '
        Me.gFTPOLine.Caption = "FTPOLine"
        Me.gFTPOLine.FieldName = "FTPOLine"
        Me.gFTPOLine.Name = "gFTPOLine"
        Me.gFTPOLine.OptionsColumn.AllowEdit = False
        Me.gFTPOLine.Visible = True
        Me.gFTPOLine.VisibleIndex = 2
        Me.gFTPOLine.Width = 116
        '
        'gFNQuantity
        '
        Me.gFNQuantity.Caption = "FNQuantity"
        Me.gFNQuantity.DisplayFormat.FormatString = "N0"
        Me.gFNQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.gFNQuantity.FieldName = "FNQuantity"
        Me.gFNQuantity.Name = "gFNQuantity"
        Me.gFNQuantity.OptionsColumn.AllowEdit = False
        Me.gFNQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", "{0:N0}")})
        Me.gFNQuantity.Visible = True
        Me.gFNQuantity.VisibleIndex = 3
        Me.gFNQuantity.Width = 122
        '
        'gFNTotalCarton
        '
        Me.gFNTotalCarton.Caption = "FNTotalCarton"
        Me.gFNTotalCarton.DisplayFormat.FormatString = "N0"
        Me.gFNTotalCarton.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.gFNTotalCarton.FieldName = "FNTotalCarton"
        Me.gFNTotalCarton.Name = "gFNTotalCarton"
        Me.gFNTotalCarton.OptionsColumn.AllowEdit = False
        Me.gFNTotalCarton.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNTotalCarton", "{0:N0}")})
        Me.gFNTotalCarton.Visible = True
        Me.gFNTotalCarton.VisibleIndex = 4
        Me.gFNTotalCarton.Width = 131
        '
        'gTotalCbm
        '
        Me.gTotalCbm.Caption = "TotalCbm"
        Me.gTotalCbm.DisplayFormat.FormatString = "N4"
        Me.gTotalCbm.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.gTotalCbm.FieldName = "TotalCbm"
        Me.gTotalCbm.Name = "gTotalCbm"
        Me.gTotalCbm.OptionsColumn.AllowEdit = False
        Me.gTotalCbm.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalCbm", "{0:N4}")})
        Me.gTotalCbm.Visible = True
        Me.gTotalCbm.VisibleIndex = 6
        Me.gTotalCbm.Width = 114
        '
        'gFTStyleCode
        '
        Me.gFTStyleCode.Caption = "FTStyleCode"
        Me.gFTStyleCode.FieldName = "FTStyleCode"
        Me.gFTStyleCode.Name = "gFTStyleCode"
        Me.gFTStyleCode.OptionsColumn.AllowEdit = False
        Me.gFTStyleCode.Visible = True
        Me.gFTStyleCode.VisibleIndex = 8
        Me.gFTStyleCode.Width = 127
        '
        'gFTStyleName
        '
        Me.gFTStyleName.Caption = "FTStyleName"
        Me.gFTStyleName.FieldName = "FTStyleName"
        Me.gFTStyleName.Name = "gFTStyleName"
        Me.gFTStyleName.OptionsColumn.AllowEdit = False
        Me.gFTStyleName.Visible = True
        Me.gFTStyleName.VisibleIndex = 9
        Me.gFTStyleName.Width = 121
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "FNHSysStyleId"
        Me.GridColumn11.FieldName = "FNHSysStyleId"
        Me.GridColumn11.Name = "GridColumn11"
        '
        'gFDShipDate
        '
        Me.gFDShipDate.Caption = "FDShipDate"
        Me.gFDShipDate.FieldName = "FDShipDate"
        Me.gFDShipDate.Name = "gFDShipDate"
        Me.gFDShipDate.OptionsColumn.AllowEdit = False
        Me.gFDShipDate.Visible = True
        Me.gFDShipDate.VisibleIndex = 7
        Me.gFDShipDate.Width = 136
        '
        'gFNPackCount
        '
        Me.gFNPackCount.Caption = "FNPackCount"
        Me.gFNPackCount.DisplayFormat.FormatString = "N0"
        Me.gFNPackCount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.gFNPackCount.FieldName = "FNPackCount"
        Me.gFNPackCount.Name = "gFNPackCount"
        Me.gFNPackCount.OptionsColumn.AllowEdit = False
        Me.gFNPackCount.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNPackCount", "{0:N0}")})
        Me.gFNPackCount.Visible = True
        Me.gFNPackCount.VisibleIndex = 5
        Me.gFNPackCount.Width = 81
        '
        'gFTColorway
        '
        Me.gFTColorway.Caption = "สีผลิตภัณฑ์"
        Me.gFTColorway.FieldName = "FTCartonInfo"
        Me.gFTColorway.Name = "gFTColorway"
        Me.gFTColorway.OptionsColumn.AllowEdit = False
        Me.gFTColorway.OptionsColumn.ReadOnly = True
        Me.gFTColorway.Visible = True
        Me.gFTColorway.VisibleIndex = 10
        Me.gFTColorway.Width = 149
        '
        'GridView2
        '
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn13, Me.GridColumn14, Me.GridColumn15, Me.GridColumn16, Me.GridColumn17, Me.GridColumn18})
        Me.GridView2.GridControl = Me.ogcsum
        Me.GridView2.Name = "GridView2"
        Me.GridView2.OptionsView.ShowGroupPanel = False
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "FTBarCodeEAN13"
        Me.GridColumn13.FieldName = "FTBarCodeEAN13"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.OptionsColumn.AllowEdit = False
        Me.GridColumn13.Visible = True
        Me.GridColumn13.VisibleIndex = 1
        Me.GridColumn13.Width = 403
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "FTCartonInfo"
        Me.GridColumn14.FieldName = "FTCartonInfo"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.OptionsColumn.AllowEdit = False
        Me.GridColumn14.Visible = True
        Me.GridColumn14.VisibleIndex = 2
        Me.GridColumn14.Width = 403
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "FNQuantity"
        Me.GridColumn15.DisplayFormat.FormatString = "N0"
        Me.GridColumn15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn15.FieldName = "FNQuantity"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.OptionsColumn.AllowEdit = False
        Me.GridColumn15.Visible = True
        Me.GridColumn15.VisibleIndex = 3
        Me.GridColumn15.Width = 403
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "FTCartonCode"
        Me.GridColumn16.FieldName = "FTCartonCode"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.OptionsColumn.AllowEdit = False
        Me.GridColumn16.Visible = True
        Me.GridColumn16.VisibleIndex = 4
        Me.GridColumn16.Width = 403
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Cbm"
        Me.GridColumn17.DisplayFormat.FormatString = "N5"
        Me.GridColumn17.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.GridColumn17.FieldName = "TotalCbm"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.OptionsColumn.AllowEdit = False
        Me.GridColumn17.Visible = True
        Me.GridColumn17.VisibleIndex = 5
        Me.GridColumn17.Width = 412
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "#"
        Me.GridColumn18.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.GridColumn18.FieldName = "FTSelect"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.Visible = True
        Me.GridColumn18.VisibleIndex = 0
        Me.GridColumn18.Width = 86
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SimpleButton1.Location = New System.Drawing.Point(902, 59)
        Me.SimpleButton1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(111, 31)
        Me.SimpleButton1.TabIndex = 96
        Me.SimpleButton1.TabStop = False
        Me.SimpleButton1.Tag = "2|"
        Me.SimpleButton1.Text = "EXIT"
        '
        'ocmselect
        '
        Me.ocmselect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ocmselect.Location = New System.Drawing.Point(12, 59)
        Me.ocmselect.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmselect.Name = "ocmselect"
        Me.ocmselect.Size = New System.Drawing.Size(111, 31)
        Me.ocmselect.TabIndex = 97
        Me.ocmselect.TabStop = False
        Me.ocmselect.Tag = "2|"
        Me.ocmselect.Text = "Select"
        '
        'PanelControl1
        '
        Me.PanelControl1.Controls.Add(Me.oChkSelectAll)
        Me.PanelControl1.Controls.Add(Me.FNTotalCarton_lbl)
        Me.PanelControl1.Controls.Add(Me.FNTotalCBM_lbl)
        Me.PanelControl1.Controls.Add(Me.FNTotalCarton)
        Me.PanelControl1.Controls.Add(Me.FNTotalCBM)
        Me.PanelControl1.Controls.Add(Me.ocmsavelayout)
        Me.PanelControl1.Controls.Add(Me.SimpleButton1)
        Me.PanelControl1.Controls.Add(Me.ocmselect)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelControl1.Location = New System.Drawing.Point(0, 739)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(1025, 103)
        Me.PanelControl1.TabIndex = 395
        '
        'oChkSelectAll
        '
        Me.oChkSelectAll.Location = New System.Drawing.Point(5, 7)
        Me.oChkSelectAll.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.oChkSelectAll.Name = "oChkSelectAll"
        Me.oChkSelectAll.Properties.AutoHeight = False
        Me.oChkSelectAll.Properties.Caption = "Select All"
        Me.oChkSelectAll.Size = New System.Drawing.Size(178, 20)
        Me.oChkSelectAll.TabIndex = 542
        '
        'FNTotalCarton_lbl
        '
        Me.FNTotalCarton_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNTotalCarton_lbl.Appearance.Options.UseForeColor = True
        Me.FNTotalCarton_lbl.Appearance.Options.UseTextOptions = True
        Me.FNTotalCarton_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTotalCarton_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNTotalCarton_lbl.Location = New System.Drawing.Point(570, 7)
        Me.FNTotalCarton_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNTotalCarton_lbl.Name = "FNTotalCarton_lbl"
        Me.FNTotalCarton_lbl.Size = New System.Drawing.Size(164, 22)
        Me.FNTotalCarton_lbl.TabIndex = 541
        Me.FNTotalCarton_lbl.Tag = "2|"
        Me.FNTotalCarton_lbl.Text = "Total Carton  :"
        '
        'FNTotalCBM_lbl
        '
        Me.FNTotalCBM_lbl.Appearance.ForeColor = System.Drawing.Color.Black
        Me.FNTotalCBM_lbl.Appearance.Options.UseForeColor = True
        Me.FNTotalCBM_lbl.Appearance.Options.UseTextOptions = True
        Me.FNTotalCBM_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNTotalCBM_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNTotalCBM_lbl.Location = New System.Drawing.Point(190, 6)
        Me.FNTotalCBM_lbl.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.FNTotalCBM_lbl.Name = "FNTotalCBM_lbl"
        Me.FNTotalCBM_lbl.Size = New System.Drawing.Size(164, 22)
        Me.FNTotalCBM_lbl.TabIndex = 541
        Me.FNTotalCBM_lbl.Tag = "2|"
        Me.FNTotalCBM_lbl.Text = "Total CBM :"
        '
        'FNTotalCarton
        '
        Me.FNTotalCarton.Location = New System.Drawing.Point(738, 7)
        Me.FNTotalCarton.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.FNTotalCarton.Name = "FNTotalCarton"
        Me.FNTotalCarton.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", Nothing, Nothing, True)})
        Me.FNTotalCarton.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.FNTotalCarton.Properties.Precision = 0
        Me.FNTotalCarton.Properties.ReadOnly = True
        Me.FNTotalCarton.Size = New System.Drawing.Size(191, 23)
        Me.FNTotalCarton.TabIndex = 540
        Me.FNTotalCarton.Tag = "2|"
        '
        'FNTotalCBM
        '
        Me.FNTotalCBM.Location = New System.Drawing.Point(358, 7)
        Me.FNTotalCBM.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.FNTotalCBM.Name = "FNTotalCBM"
        Me.FNTotalCBM.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, True, False, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", Nothing, Nothing, True)})
        Me.FNTotalCBM.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
        Me.FNTotalCBM.Properties.Precision = 5
        Me.FNTotalCBM.Properties.ReadOnly = True
        Me.FNTotalCBM.Size = New System.Drawing.Size(191, 23)
        Me.FNTotalCBM.TabIndex = 540
        Me.FNTotalCBM.Tag = "2|"
        '
        'ocmsavelayout
        '
        Me.ocmsavelayout.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ocmsavelayout.Location = New System.Drawing.Point(773, 58)
        Me.ocmsavelayout.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmsavelayout.Name = "ocmsavelayout"
        Me.ocmsavelayout.Size = New System.Drawing.Size(111, 31)
        Me.ocmsavelayout.TabIndex = 96
        Me.ocmsavelayout.TabStop = False
        Me.ocmsavelayout.Tag = "2|"
        Me.ocmsavelayout.Text = "Save Layout"
        '
        'wPopupSelectItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1025, 842)
        Me.Controls.Add(Me.otb)
        Me.Controls.Add(Me.PanelControl1)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wPopupSelectItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Item"
        CType(Me.ogvBarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemsFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFDShipDateTo.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFDShipDateTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemrFTSelect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ocmdoc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otb.ResumeLayout(False)
        Me.otpdetail.ResumeLayout(False)
        Me.otabporef.ResumeLayout(False)
        CType(Me.ogcsum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvsum, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        CType(Me.oChkSelectAll.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNTotalCarton.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNTotalCBM.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ocmdoc As DevExpress.XtraBars.Docking.DockManager
    Friend WithEvents otb As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpdetail As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ocmselect As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ReposFDShipDateTo As DevExpress.XtraEditors.Repository.RepositoryItemDateEdit
    Friend WithEvents RepositoryItemFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepositoryItemrFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FNTotalCarton_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNTotalCBM_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNTotalCarton As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents FNTotalCBM As DevExpress.XtraEditors.CalcEdit
    Friend WithEvents ogvBarcode As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents sFTBarCodeEAN13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sFTCartonInfo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sFTCartonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sTotalCbm As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents sFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemsFTSelect As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents cFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTBarCodeEAN13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCustomerPO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTPOLine As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCartonInfo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTCartonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cTotalCbm As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFTStyleName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNHSysStyleId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oChkSelectAll As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents cFDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents otabporef As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogcsum As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvsum As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gFTSelect As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents gFTCustomerPO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gFTPOLine As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gFNQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gFNTotalCarton As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gTotalCbm As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gFTStyleName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gFDShipDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gFNPackCount As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ocmsavelayout As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents gFTColorway As DevExpress.XtraGrid.Columns.GridColumn
End Class
