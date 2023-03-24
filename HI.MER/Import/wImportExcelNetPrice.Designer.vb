<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class wImportExcelNetPrice

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
        Me.ogbBrowseFile = New DevExpress.XtraEditors.GroupControl()
        Me.FTFilePath_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FTFilePath = New DevExpress.XtraEditors.ButtonEdit()
        Me.ogbViewDetail = New DevExpress.XtraEditors.GroupControl()
        Me.otcImportOrderNo = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.ogdConfirmImport = New DevExpress.XtraGrid.GridControl()
        Me.ogvConfirmImport = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.oColFNRowImport = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTPONo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTPOTrading = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTPOItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNHSysMerTeamId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTMerTeamCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTMerTeamDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTPOCreateDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTOrderDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNHSysPlantId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTPlantDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNHSysBuyGrpId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTBuyGrpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTBuyGrpDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTStyle = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTStyleDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNHSysMainCategoryId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTMainCategoryCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTMainCategoryDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTProdTypeDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTMaterial = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTMaterialDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTPlanningSeason = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTYear = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNHSysBuyerId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTBuyerCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTBuyerDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNHSysCountryId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNHSysCountryCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTCountryDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNHSysGenderId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTGenderCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTGenderDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFDShipmentDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFDShipmentDateOriginal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNHSysMatColorId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTColorwayCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTColorwayDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNHSysShipModeId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTShipModeDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNHSysUnitId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTUnitDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.ogdImportOrder = New DevExpress.XtraGrid.GridControl()
        Me.ogvImportOrder = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmImportnetprice = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmReadExcel = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclearclsr = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmExit = New DevExpress.XtraEditors.SimpleButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.ogbBrowseFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbBrowseFile.SuspendLayout()
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbViewDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbViewDetail.SuspendLayout()
        CType(Me.otcImportOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otcImportOrderNo.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.ogdConfirmImport, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvConfirmImport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.ogdImportOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvImportOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogbBrowseFile
        '
        Me.ogbBrowseFile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbBrowseFile.Controls.Add(Me.FTFilePath_lbl)
        Me.ogbBrowseFile.Controls.Add(Me.FTFilePath)
        Me.ogbBrowseFile.Location = New System.Drawing.Point(0, 3)
        Me.ogbBrowseFile.Name = "ogbBrowseFile"
        Me.ogbBrowseFile.Size = New System.Drawing.Size(1023, 60)
        Me.ogbBrowseFile.TabIndex = 0
        Me.ogbBrowseFile.Text = "Browse Source File"
        '
        'FTFilePath_lbl
        '
        Me.FTFilePath_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath_lbl.Appearance.Options.UseForeColor = True
        Me.FTFilePath_lbl.Appearance.Options.UseTextOptions = True
        Me.FTFilePath_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTFilePath_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTFilePath_lbl.Location = New System.Drawing.Point(14, 30)
        Me.FTFilePath_lbl.Name = "FTFilePath_lbl"
        Me.FTFilePath_lbl.Size = New System.Drawing.Size(128, 19)
        Me.FTFilePath_lbl.TabIndex = 432
        Me.FTFilePath_lbl.Tag = "2|"
        Me.FTFilePath_lbl.Text = "File Path :"
        '
        'FTFilePath
        '
        Me.FTFilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FTFilePath.Location = New System.Drawing.Point(147, 30)
        Me.FTFilePath.Name = "FTFilePath"
        Me.FTFilePath.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.FTFilePath.Properties.Appearance.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.LightCyan
        Me.FTFilePath.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceDisabled.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceDisabled.Options.UseForeColor = True
        Me.FTFilePath.Properties.AppearanceFocused.BackColor = System.Drawing.Color.GreenYellow
        Me.FTFilePath.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceFocused.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceFocused.Options.UseForeColor = True
        Me.FTFilePath.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.LightCyan
        Me.FTFilePath.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Blue
        Me.FTFilePath.Properties.AppearanceReadOnly.Options.UseBackColor = True
        Me.FTFilePath.Properties.AppearanceReadOnly.Options.UseForeColor = True
        Me.FTFilePath.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
        Me.FTFilePath.Properties.ReadOnly = True
        Me.FTFilePath.Size = New System.Drawing.Size(869, 20)
        Me.FTFilePath.TabIndex = 0
        Me.FTFilePath.Tag = "2|"
        '
        'ogbViewDetail
        '
        Me.ogbViewDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogbViewDetail.Controls.Add(Me.otcImportOrderNo)
        Me.ogbViewDetail.Location = New System.Drawing.Point(0, 70)
        Me.ogbViewDetail.Name = "ogbViewDetail"
        Me.ogbViewDetail.Size = New System.Drawing.Size(1023, 556)
        Me.ogbViewDetail.TabIndex = 1
        Me.ogbViewDetail.Text = "Factory Order Detail From Customer And Merchandiser"
        '
        'otcImportOrderNo
        '
        Me.otcImportOrderNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.otcImportOrderNo.Location = New System.Drawing.Point(2, 20)
        Me.otcImportOrderNo.Name = "otcImportOrderNo"
        Me.otcImportOrderNo.SelectedTabPage = Me.XtraTabPage1
        Me.otcImportOrderNo.Size = New System.Drawing.Size(1019, 534)
        Me.otcImportOrderNo.TabIndex = 0
        Me.otcImportOrderNo.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage2})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.ogdConfirmImport)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1011, 504)
        Me.XtraTabPage1.Text = "Re-check and confirm import factory order no."
        '
        'ogdConfirmImport
        '
        Me.ogdConfirmImport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdConfirmImport.Location = New System.Drawing.Point(0, 0)
        Me.ogdConfirmImport.MainView = Me.ogvConfirmImport
        Me.ogdConfirmImport.Name = "ogdConfirmImport"
        Me.ogdConfirmImport.Size = New System.Drawing.Size(1011, 504)
        Me.ogdConfirmImport.TabIndex = 0
        Me.ogdConfirmImport.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvConfirmImport})
        '
        'ogvConfirmImport
        '
        Me.ogvConfirmImport.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.oColFNRowImport, Me.oColFTPONo, Me.oColFTPOTrading, Me.oColFTPOItem, Me.oColFNHSysMerTeamId, Me.oColFTMerTeamCode, Me.oColFTMerTeamDesc, Me.oColFTPOCreateDate, Me.oColFTOrderDate, Me.oColFNHSysPlantId, Me.oColFTPlantDesc, Me.oColFNHSysBuyGrpId, Me.oColFTBuyGrpCode, Me.oColFTBuyGrpDesc, Me.oColFTStyle, Me.oColFTStyleDesc, Me.oColFNHSysMainCategoryId, Me.oColFTMainCategoryCode, Me.oColFTMainCategoryDesc, Me.oColFTProdTypeDesc, Me.oColFTMaterial, Me.oColFTMaterialDesc, Me.oColFTPlanningSeason, Me.oColFTYear, Me.oColFNHSysBuyerId, Me.oColFTBuyerCode, Me.oColFTBuyerDesc, Me.oColFNHSysCountryId, Me.oColFNHSysCountryCode, Me.oColFTCountryDesc, Me.oColFNHSysGenderId, Me.oColFTGenderCode, Me.oColFTGenderDesc, Me.oColFDShipmentDate, Me.oColFDShipmentDateOriginal, Me.oColFNHSysMatColorId, Me.oColFTColorwayCode, Me.oColFTColorwayDesc, Me.oColFNHSysShipModeId, Me.oColFTShipModeDesc, Me.oColFNHSysUnitId, Me.oColFTUnitDesc})
        Me.ogvConfirmImport.GridControl = Me.ogdConfirmImport
        Me.ogvConfirmImport.Name = "ogvConfirmImport"
        Me.ogvConfirmImport.OptionsView.ShowGroupPanel = False
        '
        'oColFNRowImport
        '
        Me.oColFNRowImport.Caption = "FNRowImport"
        Me.oColFNRowImport.FieldName = "FNRowImport"
        Me.oColFNRowImport.Name = "oColFNRowImport"
        '
        'oColFTPONo
        '
        Me.oColFTPONo.Caption = "PO No."
        Me.oColFTPONo.FieldName = "FTPONo"
        Me.oColFTPONo.Name = "oColFTPONo"
        Me.oColFTPONo.OptionsColumn.AllowEdit = False
        Me.oColFTPONo.OptionsColumn.AllowMove = False
        Me.oColFTPONo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFTPONo.Visible = True
        Me.oColFTPONo.VisibleIndex = 0
        '
        'oColFTPOTrading
        '
        Me.oColFTPOTrading.Caption = "PO Trading"
        Me.oColFTPOTrading.FieldName = "FTPOTrading"
        Me.oColFTPOTrading.Name = "oColFTPOTrading"
        Me.oColFTPOTrading.OptionsColumn.AllowEdit = False
        Me.oColFTPOTrading.OptionsColumn.AllowMove = False
        Me.oColFTPOTrading.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFTPOItem
        '
        Me.oColFTPOItem.Caption = "PO Item"
        Me.oColFTPOItem.FieldName = "FTPOItem"
        Me.oColFTPOItem.Name = "oColFTPOItem"
        Me.oColFTPOItem.OptionsColumn.AllowEdit = False
        Me.oColFTPOItem.OptionsColumn.AllowMove = False
        Me.oColFTPOItem.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFNHSysMerTeamId
        '
        Me.oColFNHSysMerTeamId.Caption = "FNHSysMerTeamId"
        Me.oColFNHSysMerTeamId.FieldName = "FNHSysMerTeamId"
        Me.oColFNHSysMerTeamId.Name = "oColFNHSysMerTeamId"
        '
        'oColFTMerTeamCode
        '
        Me.oColFTMerTeamCode.Caption = "Merchandiser Team"
        Me.oColFTMerTeamCode.FieldName = "FTMerTeamCode"
        Me.oColFTMerTeamCode.Name = "oColFTMerTeamCode"
        Me.oColFTMerTeamCode.OptionsColumn.AllowEdit = False
        Me.oColFTMerTeamCode.OptionsColumn.AllowMove = False
        Me.oColFTMerTeamCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFTMerTeamDesc
        '
        Me.oColFTMerTeamDesc.Caption = "Team Description"
        Me.oColFTMerTeamDesc.FieldName = "FTMerTeamDesc"
        Me.oColFTMerTeamDesc.Name = "oColFTMerTeamDesc"
        Me.oColFTMerTeamDesc.OptionsColumn.AllowEdit = False
        Me.oColFTMerTeamDesc.OptionsColumn.AllowMove = False
        Me.oColFTMerTeamDesc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFTPOCreateDate
        '
        Me.oColFTPOCreateDate.Caption = "PO Create Date"
        Me.oColFTPOCreateDate.FieldName = "FTPOCreateDate"
        Me.oColFTPOCreateDate.Name = "oColFTPOCreateDate"
        Me.oColFTPOCreateDate.OptionsColumn.AllowEdit = False
        Me.oColFTPOCreateDate.OptionsColumn.AllowMove = False
        Me.oColFTPOCreateDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFTOrderDate
        '
        Me.oColFTOrderDate.Caption = "Order Date"
        Me.oColFTOrderDate.FieldName = "FTOrderDate"
        Me.oColFTOrderDate.Name = "oColFTOrderDate"
        Me.oColFTOrderDate.OptionsColumn.AllowEdit = False
        Me.oColFTOrderDate.OptionsColumn.AllowMove = False
        Me.oColFTOrderDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFNHSysPlantId
        '
        Me.oColFNHSysPlantId.Caption = "FNHSysPlantId"
        Me.oColFNHSysPlantId.FieldName = "FNHSysPlantId"
        Me.oColFNHSysPlantId.Name = "oColFNHSysPlantId"
        '
        'oColFTPlantDesc
        '
        Me.oColFTPlantDesc.Caption = "Plant"
        Me.oColFTPlantDesc.FieldName = "FTPlantDesc"
        Me.oColFTPlantDesc.Name = "oColFTPlantDesc"
        Me.oColFTPlantDesc.OptionsColumn.AllowEdit = False
        Me.oColFTPlantDesc.OptionsColumn.AllowMove = False
        Me.oColFTPlantDesc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFNHSysBuyGrpId
        '
        Me.oColFNHSysBuyGrpId.Caption = "FNHSysBuyGrpId"
        Me.oColFNHSysBuyGrpId.FieldName = "FNHSysBuyGrpId"
        Me.oColFNHSysBuyGrpId.Name = "oColFNHSysBuyGrpId"
        '
        'oColFTBuyGrpCode
        '
        Me.oColFTBuyGrpCode.Caption = "Buy Group Code"
        Me.oColFTBuyGrpCode.FieldName = "FTBuyGrpCode"
        Me.oColFTBuyGrpCode.Name = "oColFTBuyGrpCode"
        Me.oColFTBuyGrpCode.OptionsColumn.AllowEdit = False
        Me.oColFTBuyGrpCode.OptionsColumn.AllowMove = False
        Me.oColFTBuyGrpCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFTBuyGrpDesc
        '
        Me.oColFTBuyGrpDesc.Caption = "Buy Group Description"
        Me.oColFTBuyGrpDesc.FieldName = "FTBuyGrpDesc"
        Me.oColFTBuyGrpDesc.Name = "oColFTBuyGrpDesc"
        Me.oColFTBuyGrpDesc.OptionsColumn.AllowEdit = False
        Me.oColFTBuyGrpDesc.OptionsColumn.AllowMove = False
        Me.oColFTBuyGrpDesc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFTStyle
        '
        Me.oColFTStyle.Caption = "Style"
        Me.oColFTStyle.FieldName = "FTStyle"
        Me.oColFTStyle.Name = "oColFTStyle"
        Me.oColFTStyle.OptionsColumn.AllowEdit = False
        Me.oColFTStyle.OptionsColumn.AllowMove = False
        Me.oColFTStyle.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFTStyle.Visible = True
        Me.oColFTStyle.VisibleIndex = 1
        '
        'oColFTStyleDesc
        '
        Me.oColFTStyleDesc.Caption = "Style Description"
        Me.oColFTStyleDesc.FieldName = "FTStyleDesc"
        Me.oColFTStyleDesc.Name = "oColFTStyleDesc"
        Me.oColFTStyleDesc.OptionsColumn.AllowEdit = False
        Me.oColFTStyleDesc.OptionsColumn.AllowMove = False
        Me.oColFTStyleDesc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFNHSysMainCategoryId
        '
        Me.oColFNHSysMainCategoryId.Caption = "FNHSysMainCategoryId"
        Me.oColFNHSysMainCategoryId.FieldName = "FNHSysMainCategoryId"
        Me.oColFNHSysMainCategoryId.Name = "oColFNHSysMainCategoryId"
        '
        'oColFTMainCategoryCode
        '
        Me.oColFTMainCategoryCode.Caption = "Category"
        Me.oColFTMainCategoryCode.FieldName = "FTMainCategoryCode"
        Me.oColFTMainCategoryCode.Name = "oColFTMainCategoryCode"
        Me.oColFTMainCategoryCode.OptionsColumn.AllowEdit = False
        Me.oColFTMainCategoryCode.OptionsColumn.AllowMove = False
        Me.oColFTMainCategoryCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFTMainCategoryDesc
        '
        Me.oColFTMainCategoryDesc.Caption = "Category Description"
        Me.oColFTMainCategoryDesc.FieldName = "FTMainCategoryDesc"
        Me.oColFTMainCategoryDesc.Name = "oColFTMainCategoryDesc"
        Me.oColFTMainCategoryDesc.OptionsColumn.AllowEdit = False
        Me.oColFTMainCategoryDesc.OptionsColumn.AllowMove = False
        Me.oColFTMainCategoryDesc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFTProdTypeDesc
        '
        Me.oColFTProdTypeDesc.Caption = "Productype Description"
        Me.oColFTProdTypeDesc.FieldName = "FTProdTypeDesc"
        Me.oColFTProdTypeDesc.Name = "oColFTProdTypeDesc"
        Me.oColFTProdTypeDesc.OptionsColumn.AllowEdit = False
        Me.oColFTProdTypeDesc.OptionsColumn.AllowMove = False
        Me.oColFTProdTypeDesc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFTMaterial
        '
        Me.oColFTMaterial.Caption = "Material"
        Me.oColFTMaterial.FieldName = "FTMaterial"
        Me.oColFTMaterial.Name = "oColFTMaterial"
        Me.oColFTMaterial.OptionsColumn.AllowEdit = False
        Me.oColFTMaterial.OptionsColumn.AllowMove = False
        Me.oColFTMaterial.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFTMaterial.Visible = True
        Me.oColFTMaterial.VisibleIndex = 2
        '
        'oColFTMaterialDesc
        '
        Me.oColFTMaterialDesc.Caption = "Material Description"
        Me.oColFTMaterialDesc.FieldName = "FTMaterialDesc"
        Me.oColFTMaterialDesc.Name = "oColFTMaterialDesc"
        Me.oColFTMaterialDesc.OptionsColumn.AllowEdit = False
        Me.oColFTMaterialDesc.OptionsColumn.AllowMove = False
        Me.oColFTMaterialDesc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFTPlanningSeason
        '
        Me.oColFTPlanningSeason.Caption = "Planning Season"
        Me.oColFTPlanningSeason.FieldName = "FTPlanningSeason"
        Me.oColFTPlanningSeason.Name = "oColFTPlanningSeason"
        Me.oColFTPlanningSeason.OptionsColumn.AllowEdit = False
        Me.oColFTPlanningSeason.OptionsColumn.AllowMove = False
        Me.oColFTPlanningSeason.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFTPlanningSeason.Visible = True
        Me.oColFTPlanningSeason.VisibleIndex = 3
        '
        'oColFTYear
        '
        Me.oColFTYear.Caption = "Year"
        Me.oColFTYear.FieldName = "FTYear"
        Me.oColFTYear.Name = "oColFTYear"
        Me.oColFTYear.OptionsColumn.AllowEdit = False
        Me.oColFTYear.OptionsColumn.AllowMove = False
        Me.oColFTYear.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFTYear.Visible = True
        Me.oColFTYear.VisibleIndex = 4
        '
        'oColFNHSysBuyerId
        '
        Me.oColFNHSysBuyerId.Caption = "FNHSysBuyerId"
        Me.oColFNHSysBuyerId.FieldName = "FNHSysBuyerId"
        Me.oColFNHSysBuyerId.Name = "oColFNHSysBuyerId"
        '
        'oColFTBuyerCode
        '
        Me.oColFTBuyerCode.Caption = "Buyer"
        Me.oColFTBuyerCode.FieldName = "FTBuyerCode"
        Me.oColFTBuyerCode.Name = "oColFTBuyerCode"
        Me.oColFTBuyerCode.OptionsColumn.AllowEdit = False
        Me.oColFTBuyerCode.OptionsColumn.AllowMove = False
        Me.oColFTBuyerCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFTBuyerDesc
        '
        Me.oColFTBuyerDesc.Caption = "Buyer Description"
        Me.oColFTBuyerDesc.FieldName = "FTBuyerDesc"
        Me.oColFTBuyerDesc.Name = "oColFTBuyerDesc"
        Me.oColFTBuyerDesc.OptionsColumn.AllowEdit = False
        Me.oColFTBuyerDesc.OptionsColumn.AllowMove = False
        Me.oColFTBuyerDesc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFNHSysCountryId
        '
        Me.oColFNHSysCountryId.Caption = "FNHSysCountryId"
        Me.oColFNHSysCountryId.FieldName = "FNHSysCountryId"
        Me.oColFNHSysCountryId.Name = "oColFNHSysCountryId"
        '
        'oColFNHSysCountryCode
        '
        Me.oColFNHSysCountryCode.Caption = "Country"
        Me.oColFNHSysCountryCode.FieldName = "FNHSysCountryCode"
        Me.oColFNHSysCountryCode.Name = "oColFNHSysCountryCode"
        Me.oColFNHSysCountryCode.OptionsColumn.AllowEdit = False
        Me.oColFNHSysCountryCode.OptionsColumn.AllowMove = False
        Me.oColFNHSysCountryCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFTCountryDesc
        '
        Me.oColFTCountryDesc.Caption = "Country Description"
        Me.oColFTCountryDesc.FieldName = "FTCountryDesc"
        Me.oColFTCountryDesc.Name = "oColFTCountryDesc"
        Me.oColFTCountryDesc.OptionsColumn.AllowEdit = False
        Me.oColFTCountryDesc.OptionsColumn.AllowMove = False
        Me.oColFTCountryDesc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFNHSysGenderId
        '
        Me.oColFNHSysGenderId.Caption = "FNHSysGenderId"
        Me.oColFNHSysGenderId.FieldName = "FNHSysGenderId"
        Me.oColFNHSysGenderId.Name = "oColFNHSysGenderId"
        '
        'oColFTGenderCode
        '
        Me.oColFTGenderCode.Caption = "Gender"
        Me.oColFTGenderCode.FieldName = "FTGenderCode"
        Me.oColFTGenderCode.Name = "oColFTGenderCode"
        Me.oColFTGenderCode.OptionsColumn.AllowEdit = False
        Me.oColFTGenderCode.OptionsColumn.AllowMove = False
        Me.oColFTGenderCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFTGenderDesc
        '
        Me.oColFTGenderDesc.Caption = "Gender Description"
        Me.oColFTGenderDesc.FieldName = "FTGenderDesc"
        Me.oColFTGenderDesc.Name = "oColFTGenderDesc"
        Me.oColFTGenderDesc.OptionsColumn.AllowEdit = False
        Me.oColFTGenderDesc.OptionsColumn.AllowMove = False
        Me.oColFTGenderDesc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        '
        'oColFDShipmentDate
        '
        Me.oColFDShipmentDate.Caption = "GAC Date"
        Me.oColFDShipmentDate.FieldName = "FDShipmentDate"
        Me.oColFDShipmentDate.Name = "oColFDShipmentDate"
        Me.oColFDShipmentDate.OptionsColumn.AllowEdit = False
        Me.oColFDShipmentDate.OptionsColumn.AllowMove = False
        Me.oColFDShipmentDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFDShipmentDate.Visible = True
        Me.oColFDShipmentDate.VisibleIndex = 5
        '
        'oColFDShipmentDateOriginal
        '
        Me.oColFDShipmentDateOriginal.Caption = "OGAC Date"
        Me.oColFDShipmentDateOriginal.FieldName = "FDShipmentDateOriginal"
        Me.oColFDShipmentDateOriginal.Name = "oColFDShipmentDateOriginal"
        Me.oColFDShipmentDateOriginal.OptionsColumn.AllowEdit = False
        Me.oColFDShipmentDateOriginal.OptionsColumn.AllowMove = False
        Me.oColFDShipmentDateOriginal.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFDShipmentDateOriginal.Visible = True
        Me.oColFDShipmentDateOriginal.VisibleIndex = 6
        '
        'oColFNHSysMatColorId
        '
        Me.oColFNHSysMatColorId.Caption = "FNHSysMatColorId"
        Me.oColFNHSysMatColorId.FieldName = "FNHSysMatColorId"
        Me.oColFNHSysMatColorId.Name = "oColFNHSysMatColorId"
        '
        'oColFTColorwayCode
        '
        Me.oColFTColorwayCode.Caption = "Color"
        Me.oColFTColorwayCode.FieldName = "FTColorwayCode"
        Me.oColFTColorwayCode.Name = "oColFTColorwayCode"
        Me.oColFTColorwayCode.OptionsColumn.AllowEdit = False
        Me.oColFTColorwayCode.OptionsColumn.AllowMove = False
        Me.oColFTColorwayCode.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFTColorwayCode.Visible = True
        Me.oColFTColorwayCode.VisibleIndex = 7
        '
        'oColFTColorwayDesc
        '
        Me.oColFTColorwayDesc.Caption = "Color Description"
        Me.oColFTColorwayDesc.FieldName = "FTColorwayDesc"
        Me.oColFTColorwayDesc.Name = "oColFTColorwayDesc"
        Me.oColFTColorwayDesc.OptionsColumn.AllowEdit = False
        Me.oColFTColorwayDesc.OptionsColumn.AllowMove = False
        Me.oColFTColorwayDesc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFTColorwayDesc.Visible = True
        Me.oColFTColorwayDesc.VisibleIndex = 8
        '
        'oColFNHSysShipModeId
        '
        Me.oColFNHSysShipModeId.Caption = "FNHSysShipModeId"
        Me.oColFNHSysShipModeId.FieldName = "FNHSysShipModeId"
        Me.oColFNHSysShipModeId.Name = "oColFNHSysShipModeId"
        '
        'oColFTShipModeDesc
        '
        Me.oColFTShipModeDesc.Caption = "Ship Mode"
        Me.oColFTShipModeDesc.FieldName = "FTShipModeDesc"
        Me.oColFTShipModeDesc.Name = "oColFTShipModeDesc"
        Me.oColFTShipModeDesc.OptionsColumn.AllowEdit = False
        Me.oColFTShipModeDesc.OptionsColumn.AllowMove = False
        Me.oColFTShipModeDesc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFTShipModeDesc.Visible = True
        Me.oColFTShipModeDesc.VisibleIndex = 9
        '
        'oColFNHSysUnitId
        '
        Me.oColFNHSysUnitId.Caption = "FNHSysUnitId"
        Me.oColFNHSysUnitId.FieldName = "FNHSysUnitId"
        Me.oColFNHSysUnitId.Name = "oColFNHSysUnitId"
        '
        'oColFTUnitDesc
        '
        Me.oColFTUnitDesc.Caption = "Unit Description"
        Me.oColFTUnitDesc.FieldName = "FTUnitDesc"
        Me.oColFTUnitDesc.Name = "oColFTUnitDesc"
        Me.oColFTUnitDesc.OptionsColumn.AllowEdit = False
        Me.oColFTUnitDesc.OptionsColumn.AllowMove = False
        Me.oColFTUnitDesc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFTUnitDesc.Visible = True
        Me.oColFTUnitDesc.VisibleIndex = 10
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.ogdImportOrder)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(1013, 506)
        Me.XtraTabPage2.Text = "Raw Data Import Order"
        '
        'ogdImportOrder
        '
        Me.ogdImportOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdImportOrder.Location = New System.Drawing.Point(0, 0)
        Me.ogdImportOrder.MainView = Me.ogvImportOrder
        Me.ogdImportOrder.Name = "ogdImportOrder"
        Me.ogdImportOrder.Size = New System.Drawing.Size(1013, 506)
        Me.ogdImportOrder.TabIndex = 0
        Me.ogdImportOrder.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvImportOrder})
        '
        'ogvImportOrder
        '
        Me.ogvImportOrder.GridControl = Me.ogdImportOrder
        Me.ogvImportOrder.Name = "ogvImportOrder"
        Me.ogvImportOrder.OptionsView.ShowGroupPanel = False
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmImportnetprice)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmReadExcel)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclearclsr)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmExit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(170, 11)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(714, 72)
        Me.ogbmainprocbutton.TabIndex = 462
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmImportnetprice
        '
        Me.ocmImportnetprice.Location = New System.Drawing.Point(10, 40)
        Me.ocmImportnetprice.Name = "ocmImportnetprice"
        Me.ocmImportnetprice.Size = New System.Drawing.Size(221, 25)
        Me.ocmImportnetprice.TabIndex = 99
        Me.ocmImportnetprice.TabStop = False
        Me.ocmImportnetprice.Tag = "2|"
        Me.ocmImportnetprice.Text = "IMPORT ORDER NET PRICE."
        '
        'ocmReadExcel
        '
        Me.ocmReadExcel.Location = New System.Drawing.Point(10, 8)
        Me.ocmReadExcel.Name = "ocmReadExcel"
        Me.ocmReadExcel.Size = New System.Drawing.Size(221, 25)
        Me.ocmReadExcel.TabIndex = 98
        Me.ocmReadExcel.TabStop = False
        Me.ocmReadExcel.Tag = "2|"
        Me.ocmReadExcel.Text = "READ EXCEL FILE"
        '
        'ocmclearclsr
        '
        Me.ocmclearclsr.Location = New System.Drawing.Point(344, 8)
        Me.ocmclearclsr.Name = "ocmclearclsr"
        Me.ocmclearclsr.Size = New System.Drawing.Size(95, 25)
        Me.ocmclearclsr.TabIndex = 97
        Me.ocmclearclsr.TabStop = False
        Me.ocmclearclsr.Tag = "2|"
        Me.ocmclearclsr.Text = "CLEAR"
        '
        'ocmExit
        '
        Me.ocmExit.Location = New System.Drawing.Point(445, 8)
        Me.ocmExit.Name = "ocmExit"
        Me.ocmExit.Size = New System.Drawing.Size(95, 25)
        Me.ocmExit.TabIndex = 96
        Me.ocmExit.TabStop = False
        Me.ocmExit.Tag = "2|"
        Me.ocmExit.Text = "EXIT"
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'wImportExcelNetPrice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1023, 634)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogbViewDetail)
        Me.Controls.Add(Me.ogbBrowseFile)
        Me.Name = "wImportExcelNetPrice"
        Me.Text = "Import Net Price Order No."
        CType(Me.ogbBrowseFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbBrowseFile.ResumeLayout(False)
        CType(Me.FTFilePath.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbViewDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbViewDetail.ResumeLayout(False)
        CType(Me.otcImportOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otcImportOrderNo.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.ogdConfirmImport, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvConfirmImport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage2.ResumeLayout(False)
        CType(Me.ogdImportOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvImportOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbBrowseFile As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FTFilePath As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents ogbViewDetail As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ogdImportOrder As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvImportOrder As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents FTFilePath_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmImportnetprice As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmReadExcel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclearclsr As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents otcImportOrderNo As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogdConfirmImport As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvConfirmImport As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents oColFNRowImport As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTPONo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTPOTrading As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTPOItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNHSysMerTeamId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTMerTeamCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTMerTeamDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTPOCreateDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTOrderDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNHSysPlantId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTPlantDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNHSysBuyGrpId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTBuyGrpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTBuyGrpDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTStyle As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTStyleDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNHSysMainCategoryId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTMainCategoryCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTMainCategoryDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTProdTypeDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTMaterial As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTMaterialDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTPlanningSeason As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTYear As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNHSysBuyerId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTBuyerCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTBuyerDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNHSysCountryId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNHSysCountryCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTCountryDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNHSysGenderId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTGenderCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTGenderDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFDShipmentDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFDShipmentDateOriginal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNHSysMatColorId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTColorwayCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTColorwayDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNHSysShipModeId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTShipModeDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNHSysUnitId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTUnitDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
