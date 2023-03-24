<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wAsset
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
        Me.ogc = New DevExpress.XtraGrid.GridControl()
        Me.ogv = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.FTCmpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.AssetTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTAssetCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTAssetName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTAssetNameTH = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTAssetNameEN = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTAssetModelName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTAssetBrandName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSerialNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRefer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTAssetGrpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTAssetTypeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTProductCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDDateAdd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDDateUsed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDDateStartWarranty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDDateEndWarranty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMaxPower = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitAssetCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTUnitSectCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTEmpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSuplName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCurCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDPurchaseDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTPurchaseBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTInvoiceNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDInvoiceDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTReceiveNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FDReceiveDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTReceiveBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMaximumStock = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNMinimumStock = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTStateCritical = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepStateCritical = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTStateActive = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepStateActive = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FNHSysFixedAssetId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTLocationAsset = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmedit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmpreview = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmexit = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmrefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmdelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmaddnew = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepStateCritical, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepStateActive, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        Me.SuspendLayout()
        '
        'ogc
        '
        Me.ogc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogc.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Location = New System.Drawing.Point(0, 0)
        Me.ogc.MainView = Me.ogv
        Me.ogc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogc.Name = "ogc"
        Me.ogc.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepStateCritical, Me.RepStateActive})
        Me.ogc.Size = New System.Drawing.Size(3347, 646)
        Me.ogc.TabIndex = 0
        Me.ogc.Tag = "2|"
        Me.ogc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogv})
        '
        'ogv
        '
        Me.ogv.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.FTCmpCode, Me.AssetTypeName, Me.FTAssetCode, Me.FTAssetName, Me.FTAssetNameTH, Me.FTAssetNameEN, Me.FTAssetModelName, Me.FTAssetBrandName, Me.FTSerialNo, Me.FTRefer, Me.FTAssetGrpName, Me.FTAssetTypeName, Me.FTProductCode, Me.FDDateAdd, Me.FDDateUsed, Me.FDDateStartWarranty, Me.FDDateEndWarranty, Me.FNMaxPower, Me.FTUnitAssetCode, Me.FTUnitSectCode, Me.FTEmpName, Me.FTSuplName, Me.FNPrice, Me.FTCurCode, Me.FTPurchaseNo, Me.FDPurchaseDate, Me.FTPurchaseBy, Me.FTInvoiceNo, Me.FDInvoiceDate, Me.FTReceiveNo, Me.FDReceiveDate, Me.FTReceiveBy, Me.FNMaximumStock, Me.FNMinimumStock, Me.FTRemark, Me.FTStateCritical, Me.FTStateActive, Me.FNHSysFixedAssetId, Me.FTLocationAsset})
        Me.ogv.GridControl = Me.ogc
        Me.ogv.Name = "ogv"
        Me.ogv.OptionsView.ColumnAutoWidth = False
        Me.ogv.OptionsView.ShowGroupPanel = False
        '
        'FTCmpCode
        '
        Me.FTCmpCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTCmpCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTCmpCode.Caption = "FTCmpCode"
        Me.FTCmpCode.FieldName = "FTCmpCode"
        Me.FTCmpCode.Name = "FTCmpCode"
        Me.FTCmpCode.OptionsColumn.AllowEdit = False
        Me.FTCmpCode.OptionsColumn.ReadOnly = True
        Me.FTCmpCode.Visible = True
        Me.FTCmpCode.VisibleIndex = 0
        Me.FTCmpCode.Width = 71
        '
        'AssetTypeName
        '
        Me.AssetTypeName.AppearanceHeader.Options.UseTextOptions = True
        Me.AssetTypeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.AssetTypeName.Caption = "AssetTypeName"
        Me.AssetTypeName.FieldName = "AssetTypeName"
        Me.AssetTypeName.Name = "AssetTypeName"
        Me.AssetTypeName.OptionsColumn.AllowEdit = False
        Me.AssetTypeName.OptionsColumn.ReadOnly = True
        Me.AssetTypeName.Visible = True
        Me.AssetTypeName.VisibleIndex = 1
        Me.AssetTypeName.Width = 120
        '
        'FTAssetCode
        '
        Me.FTAssetCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTAssetCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAssetCode.Caption = "FTAssetCode"
        Me.FTAssetCode.FieldName = "FTAssetCode"
        Me.FTAssetCode.Name = "FTAssetCode"
        Me.FTAssetCode.OptionsColumn.AllowEdit = False
        Me.FTAssetCode.OptionsColumn.ReadOnly = True
        Me.FTAssetCode.Visible = True
        Me.FTAssetCode.VisibleIndex = 2
        '
        'FTAssetName
        '
        Me.FTAssetName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTAssetName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAssetName.Caption = "FTAssetName"
        Me.FTAssetName.FieldName = "FTAssetName"
        Me.FTAssetName.Name = "FTAssetName"
        Me.FTAssetName.OptionsColumn.AllowEdit = False
        Me.FTAssetName.OptionsColumn.ReadOnly = True
        Me.FTAssetName.Width = 120
        '
        'FTAssetNameTH
        '
        Me.FTAssetNameTH.AppearanceHeader.Options.UseTextOptions = True
        Me.FTAssetNameTH.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAssetNameTH.Caption = "FTAssetNameTH"
        Me.FTAssetNameTH.FieldName = "FTAssetNameTH"
        Me.FTAssetNameTH.Name = "FTAssetNameTH"
        Me.FTAssetNameTH.OptionsColumn.AllowEdit = False
        Me.FTAssetNameTH.OptionsColumn.ReadOnly = True
        Me.FTAssetNameTH.Visible = True
        Me.FTAssetNameTH.VisibleIndex = 3
        Me.FTAssetNameTH.Width = 120
        '
        'FTAssetNameEN
        '
        Me.FTAssetNameEN.AppearanceHeader.Options.UseTextOptions = True
        Me.FTAssetNameEN.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAssetNameEN.Caption = "FTAssetNameEN"
        Me.FTAssetNameEN.FieldName = "FTAssetNameEN"
        Me.FTAssetNameEN.Name = "FTAssetNameEN"
        Me.FTAssetNameEN.OptionsColumn.AllowEdit = False
        Me.FTAssetNameEN.OptionsColumn.ReadOnly = True
        Me.FTAssetNameEN.Visible = True
        Me.FTAssetNameEN.VisibleIndex = 4
        Me.FTAssetNameEN.Width = 120
        '
        'FTAssetModelName
        '
        Me.FTAssetModelName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTAssetModelName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAssetModelName.Caption = "FTAssetModelName"
        Me.FTAssetModelName.FieldName = "FTAssetModelName"
        Me.FTAssetModelName.Name = "FTAssetModelName"
        Me.FTAssetModelName.OptionsColumn.AllowEdit = False
        Me.FTAssetModelName.OptionsColumn.ReadOnly = True
        Me.FTAssetModelName.Visible = True
        Me.FTAssetModelName.VisibleIndex = 6
        Me.FTAssetModelName.Width = 120
        '
        'FTAssetBrandName
        '
        Me.FTAssetBrandName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTAssetBrandName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAssetBrandName.Caption = "FTAssetBrandName"
        Me.FTAssetBrandName.FieldName = "FTAssetBrandName"
        Me.FTAssetBrandName.Name = "FTAssetBrandName"
        Me.FTAssetBrandName.OptionsColumn.AllowEdit = False
        Me.FTAssetBrandName.OptionsColumn.ReadOnly = True
        Me.FTAssetBrandName.Visible = True
        Me.FTAssetBrandName.VisibleIndex = 7
        Me.FTAssetBrandName.Width = 120
        '
        'FTSerialNo
        '
        Me.FTSerialNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSerialNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSerialNo.Caption = "FTSerialNo"
        Me.FTSerialNo.FieldName = "FTSerialNo"
        Me.FTSerialNo.Name = "FTSerialNo"
        Me.FTSerialNo.OptionsColumn.AllowEdit = False
        Me.FTSerialNo.OptionsColumn.ReadOnly = True
        Me.FTSerialNo.Visible = True
        Me.FTSerialNo.VisibleIndex = 8
        Me.FTSerialNo.Width = 130
        '
        'FTRefer
        '
        Me.FTRefer.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRefer.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRefer.Caption = "FTRefer"
        Me.FTRefer.FieldName = "FTRefer"
        Me.FTRefer.Name = "FTRefer"
        Me.FTRefer.OptionsColumn.AllowEdit = False
        Me.FTRefer.OptionsColumn.ReadOnly = True
        Me.FTRefer.Visible = True
        Me.FTRefer.VisibleIndex = 9
        '
        'FTAssetGrpName
        '
        Me.FTAssetGrpName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTAssetGrpName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAssetGrpName.Caption = "FTAssetGrpName"
        Me.FTAssetGrpName.FieldName = "FTAssetGrpName"
        Me.FTAssetGrpName.Name = "FTAssetGrpName"
        Me.FTAssetGrpName.OptionsColumn.AllowEdit = False
        Me.FTAssetGrpName.OptionsColumn.ReadOnly = True
        Me.FTAssetGrpName.Visible = True
        Me.FTAssetGrpName.VisibleIndex = 10
        Me.FTAssetGrpName.Width = 120
        '
        'FTAssetTypeName
        '
        Me.FTAssetTypeName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTAssetTypeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTAssetTypeName.Caption = "FTAssetTypeName"
        Me.FTAssetTypeName.FieldName = "FTAssetTypeName"
        Me.FTAssetTypeName.Name = "FTAssetTypeName"
        Me.FTAssetTypeName.OptionsColumn.AllowEdit = False
        Me.FTAssetTypeName.OptionsColumn.ReadOnly = True
        Me.FTAssetTypeName.Visible = True
        Me.FTAssetTypeName.VisibleIndex = 11
        Me.FTAssetTypeName.Width = 120
        '
        'FTProductCode
        '
        Me.FTProductCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTProductCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTProductCode.Caption = "FTProductCode"
        Me.FTProductCode.FieldName = "FTProductCode"
        Me.FTProductCode.Name = "FTProductCode"
        Me.FTProductCode.OptionsColumn.AllowEdit = False
        Me.FTProductCode.OptionsColumn.ReadOnly = True
        Me.FTProductCode.Visible = True
        Me.FTProductCode.VisibleIndex = 12
        Me.FTProductCode.Width = 80
        '
        'FDDateAdd
        '
        Me.FDDateAdd.AppearanceHeader.Options.UseTextOptions = True
        Me.FDDateAdd.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDDateAdd.Caption = "FDDateAdd"
        Me.FDDateAdd.FieldName = "FDDateAdd"
        Me.FDDateAdd.Name = "FDDateAdd"
        Me.FDDateAdd.OptionsColumn.AllowEdit = False
        Me.FDDateAdd.OptionsColumn.ReadOnly = True
        Me.FDDateAdd.Visible = True
        Me.FDDateAdd.VisibleIndex = 13
        '
        'FDDateUsed
        '
        Me.FDDateUsed.AppearanceHeader.Options.UseTextOptions = True
        Me.FDDateUsed.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDDateUsed.Caption = "FDDateUsed"
        Me.FDDateUsed.FieldName = "FDDateUsed"
        Me.FDDateUsed.Name = "FDDateUsed"
        Me.FDDateUsed.OptionsColumn.AllowEdit = False
        Me.FDDateUsed.OptionsColumn.ReadOnly = True
        Me.FDDateUsed.Visible = True
        Me.FDDateUsed.VisibleIndex = 14
        '
        'FDDateStartWarranty
        '
        Me.FDDateStartWarranty.AppearanceHeader.Options.UseTextOptions = True
        Me.FDDateStartWarranty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDDateStartWarranty.Caption = "FDDateStartWarranty"
        Me.FDDateStartWarranty.FieldName = "FDDateStartWarranty"
        Me.FDDateStartWarranty.Name = "FDDateStartWarranty"
        Me.FDDateStartWarranty.OptionsColumn.AllowEdit = False
        Me.FDDateStartWarranty.OptionsColumn.ReadOnly = True
        Me.FDDateStartWarranty.Visible = True
        Me.FDDateStartWarranty.VisibleIndex = 15
        '
        'FDDateEndWarranty
        '
        Me.FDDateEndWarranty.AppearanceHeader.Options.UseTextOptions = True
        Me.FDDateEndWarranty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDDateEndWarranty.Caption = "FDDateEndWarranty"
        Me.FDDateEndWarranty.FieldName = "FDDateEndWarranty"
        Me.FDDateEndWarranty.Name = "FDDateEndWarranty"
        Me.FDDateEndWarranty.OptionsColumn.AllowEdit = False
        Me.FDDateEndWarranty.OptionsColumn.ReadOnly = True
        Me.FDDateEndWarranty.Visible = True
        Me.FDDateEndWarranty.VisibleIndex = 16
        '
        'FNMaxPower
        '
        Me.FNMaxPower.AppearanceHeader.Options.UseTextOptions = True
        Me.FNMaxPower.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNMaxPower.Caption = "FNMaxPower"
        Me.FNMaxPower.FieldName = "FNMaxPower"
        Me.FNMaxPower.Name = "FNMaxPower"
        Me.FNMaxPower.OptionsColumn.AllowEdit = False
        Me.FNMaxPower.OptionsColumn.ReadOnly = True
        Me.FNMaxPower.Visible = True
        Me.FNMaxPower.VisibleIndex = 17
        '
        'FTUnitAssetCode
        '
        Me.FTUnitAssetCode.Caption = "FTUnitAssetCode"
        Me.FTUnitAssetCode.FieldName = "FTUnitAssetCode"
        Me.FTUnitAssetCode.Name = "FTUnitAssetCode"
        Me.FTUnitAssetCode.OptionsColumn.AllowEdit = False
        Me.FTUnitAssetCode.OptionsColumn.ReadOnly = True
        Me.FTUnitAssetCode.Visible = True
        Me.FTUnitAssetCode.VisibleIndex = 19
        '
        'FTUnitSectCode
        '
        Me.FTUnitSectCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTUnitSectCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTUnitSectCode.Caption = "FTUnitSectCode"
        Me.FTUnitSectCode.FieldName = "FTUnitSectCode"
        Me.FTUnitSectCode.Name = "FTUnitSectCode"
        Me.FTUnitSectCode.OptionsColumn.AllowEdit = False
        Me.FTUnitSectCode.OptionsColumn.ReadOnly = True
        Me.FTUnitSectCode.Visible = True
        Me.FTUnitSectCode.VisibleIndex = 18
        '
        'FTEmpName
        '
        Me.FTEmpName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTEmpName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTEmpName.Caption = "FTEmpName"
        Me.FTEmpName.FieldName = "FTEmpName"
        Me.FTEmpName.Name = "FTEmpName"
        Me.FTEmpName.OptionsColumn.AllowEdit = False
        Me.FTEmpName.OptionsColumn.ReadOnly = True
        Me.FTEmpName.Visible = True
        Me.FTEmpName.VisibleIndex = 20
        Me.FTEmpName.Width = 100
        '
        'FTSuplName
        '
        Me.FTSuplName.AppearanceHeader.Options.UseTextOptions = True
        Me.FTSuplName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTSuplName.Caption = "FTSuplName"
        Me.FTSuplName.FieldName = "FTSuplName"
        Me.FTSuplName.Name = "FTSuplName"
        Me.FTSuplName.OptionsColumn.AllowEdit = False
        Me.FTSuplName.OptionsColumn.ReadOnly = True
        Me.FTSuplName.Visible = True
        Me.FTSuplName.VisibleIndex = 21
        Me.FTSuplName.Width = 120
        '
        'FNPrice
        '
        Me.FNPrice.AppearanceHeader.Options.UseTextOptions = True
        Me.FNPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNPrice.Caption = "FNPrice"
        Me.FNPrice.FieldName = "FNPrice"
        Me.FNPrice.Name = "FNPrice"
        Me.FNPrice.OptionsColumn.AllowEdit = False
        Me.FNPrice.OptionsColumn.ReadOnly = True
        Me.FNPrice.Visible = True
        Me.FNPrice.VisibleIndex = 22
        '
        'FTCurCode
        '
        Me.FTCurCode.AppearanceHeader.Options.UseTextOptions = True
        Me.FTCurCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTCurCode.Caption = "FTCurCode"
        Me.FTCurCode.FieldName = "FTCurCode"
        Me.FTCurCode.Name = "FTCurCode"
        Me.FTCurCode.OptionsColumn.AllowEdit = False
        Me.FTCurCode.OptionsColumn.ReadOnly = True
        Me.FTCurCode.Visible = True
        Me.FTCurCode.VisibleIndex = 23
        '
        'FTPurchaseNo
        '
        Me.FTPurchaseNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTPurchaseNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPurchaseNo.Caption = "FTPurchaseNo"
        Me.FTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.FTPurchaseNo.Name = "FTPurchaseNo"
        Me.FTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.FTPurchaseNo.OptionsColumn.ReadOnly = True
        Me.FTPurchaseNo.Visible = True
        Me.FTPurchaseNo.VisibleIndex = 24
        Me.FTPurchaseNo.Width = 100
        '
        'FDPurchaseDate
        '
        Me.FDPurchaseDate.AppearanceHeader.Options.UseTextOptions = True
        Me.FDPurchaseDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDPurchaseDate.Caption = "FDPurchaseDate"
        Me.FDPurchaseDate.FieldName = "FDPurchaseDate"
        Me.FDPurchaseDate.Name = "FDPurchaseDate"
        Me.FDPurchaseDate.OptionsColumn.AllowEdit = False
        Me.FDPurchaseDate.OptionsColumn.ReadOnly = True
        Me.FDPurchaseDate.Visible = True
        Me.FDPurchaseDate.VisibleIndex = 25
        '
        'FTPurchaseBy
        '
        Me.FTPurchaseBy.AppearanceHeader.Options.UseTextOptions = True
        Me.FTPurchaseBy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTPurchaseBy.Caption = "FTPurchaseBy"
        Me.FTPurchaseBy.FieldName = "FTPurchaseBy"
        Me.FTPurchaseBy.Name = "FTPurchaseBy"
        Me.FTPurchaseBy.OptionsColumn.AllowEdit = False
        Me.FTPurchaseBy.OptionsColumn.ReadOnly = True
        Me.FTPurchaseBy.Visible = True
        Me.FTPurchaseBy.VisibleIndex = 26
        '
        'FTInvoiceNo
        '
        Me.FTInvoiceNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTInvoiceNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTInvoiceNo.Caption = "FTInvoiceNo"
        Me.FTInvoiceNo.FieldName = "FTInvoiceNo"
        Me.FTInvoiceNo.Name = "FTInvoiceNo"
        Me.FTInvoiceNo.OptionsColumn.AllowEdit = False
        Me.FTInvoiceNo.OptionsColumn.ReadOnly = True
        Me.FTInvoiceNo.Visible = True
        Me.FTInvoiceNo.VisibleIndex = 27
        Me.FTInvoiceNo.Width = 100
        '
        'FDInvoiceDate
        '
        Me.FDInvoiceDate.AppearanceHeader.Options.UseTextOptions = True
        Me.FDInvoiceDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDInvoiceDate.Caption = "FDInvoiceDate"
        Me.FDInvoiceDate.FieldName = "FDInvoiceDate"
        Me.FDInvoiceDate.Name = "FDInvoiceDate"
        Me.FDInvoiceDate.OptionsColumn.AllowEdit = False
        Me.FDInvoiceDate.OptionsColumn.ReadOnly = True
        Me.FDInvoiceDate.Visible = True
        Me.FDInvoiceDate.VisibleIndex = 28
        '
        'FTReceiveNo
        '
        Me.FTReceiveNo.AppearanceHeader.Options.UseTextOptions = True
        Me.FTReceiveNo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTReceiveNo.Caption = "FTReceiveNo"
        Me.FTReceiveNo.FieldName = "FTReceiveNo"
        Me.FTReceiveNo.Name = "FTReceiveNo"
        Me.FTReceiveNo.OptionsColumn.AllowEdit = False
        Me.FTReceiveNo.OptionsColumn.ReadOnly = True
        Me.FTReceiveNo.Visible = True
        Me.FTReceiveNo.VisibleIndex = 29
        '
        'FDReceiveDate
        '
        Me.FDReceiveDate.AppearanceHeader.Options.UseTextOptions = True
        Me.FDReceiveDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FDReceiveDate.Caption = "FDReceiveDate"
        Me.FDReceiveDate.FieldName = "FDReceiveDate"
        Me.FDReceiveDate.Name = "FDReceiveDate"
        Me.FDReceiveDate.OptionsColumn.AllowEdit = False
        Me.FDReceiveDate.OptionsColumn.ReadOnly = True
        Me.FDReceiveDate.Visible = True
        Me.FDReceiveDate.VisibleIndex = 30
        '
        'FTReceiveBy
        '
        Me.FTReceiveBy.AppearanceHeader.Options.UseTextOptions = True
        Me.FTReceiveBy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTReceiveBy.Caption = "FTReceiveBy"
        Me.FTReceiveBy.FieldName = "FTReceiveBy"
        Me.FTReceiveBy.Name = "FTReceiveBy"
        Me.FTReceiveBy.OptionsColumn.AllowEdit = False
        Me.FTReceiveBy.OptionsColumn.ReadOnly = True
        Me.FTReceiveBy.Visible = True
        Me.FTReceiveBy.VisibleIndex = 31
        '
        'FNMaximumStock
        '
        Me.FNMaximumStock.AppearanceHeader.Options.UseTextOptions = True
        Me.FNMaximumStock.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNMaximumStock.Caption = "FNMaximumStock"
        Me.FNMaximumStock.FieldName = "FNMaximumStock"
        Me.FNMaximumStock.Name = "FNMaximumStock"
        Me.FNMaximumStock.OptionsColumn.AllowEdit = False
        Me.FNMaximumStock.OptionsColumn.ReadOnly = True
        Me.FNMaximumStock.Visible = True
        Me.FNMaximumStock.VisibleIndex = 32
        '
        'FNMinimumStock
        '
        Me.FNMinimumStock.AppearanceHeader.Options.UseTextOptions = True
        Me.FNMinimumStock.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FNMinimumStock.Caption = "FNMinimumStock"
        Me.FNMinimumStock.FieldName = "FNMinimumStock"
        Me.FNMinimumStock.Name = "FNMinimumStock"
        Me.FNMinimumStock.OptionsColumn.AllowEdit = False
        Me.FNMinimumStock.OptionsColumn.ReadOnly = True
        Me.FNMinimumStock.Visible = True
        Me.FNMinimumStock.VisibleIndex = 33
        '
        'FTRemark
        '
        Me.FTRemark.AppearanceHeader.Options.UseTextOptions = True
        Me.FTRemark.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTRemark.Caption = "FTRemark"
        Me.FTRemark.FieldName = "FTRemark"
        Me.FTRemark.Name = "FTRemark"
        Me.FTRemark.OptionsColumn.AllowEdit = False
        Me.FTRemark.OptionsColumn.ReadOnly = True
        Me.FTRemark.Visible = True
        Me.FTRemark.VisibleIndex = 34
        Me.FTRemark.Width = 160
        '
        'FTStateCritical
        '
        Me.FTStateCritical.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStateCritical.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStateCritical.Caption = "FTStateCritical"
        Me.FTStateCritical.ColumnEdit = Me.RepStateCritical
        Me.FTStateCritical.FieldName = "FTStateCritical"
        Me.FTStateCritical.Name = "FTStateCritical"
        Me.FTStateCritical.OptionsColumn.AllowEdit = False
        Me.FTStateCritical.OptionsColumn.ReadOnly = True
        Me.FTStateCritical.Visible = True
        Me.FTStateCritical.VisibleIndex = 35
        Me.FTStateCritical.Width = 55
        '
        'RepStateCritical
        '
        Me.RepStateCritical.AutoHeight = False
        Me.RepStateCritical.Caption = "Check"
        Me.RepStateCritical.Name = "RepStateCritical"
        Me.RepStateCritical.ValueChecked = "1"
        Me.RepStateCritical.ValueUnchecked = "0"
        '
        'FTStateActive
        '
        Me.FTStateActive.AppearanceHeader.Options.UseTextOptions = True
        Me.FTStateActive.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.FTStateActive.Caption = "FTStateActive"
        Me.FTStateActive.ColumnEdit = Me.RepStateActive
        Me.FTStateActive.FieldName = "FTStateActive"
        Me.FTStateActive.Name = "FTStateActive"
        Me.FTStateActive.OptionsColumn.AllowEdit = False
        Me.FTStateActive.OptionsColumn.ReadOnly = True
        Me.FTStateActive.Visible = True
        Me.FTStateActive.VisibleIndex = 36
        Me.FTStateActive.Width = 55
        '
        'RepStateActive
        '
        Me.RepStateActive.AutoHeight = False
        Me.RepStateActive.Caption = "Check"
        Me.RepStateActive.Name = "RepStateActive"
        Me.RepStateActive.ValueChecked = "1"
        Me.RepStateActive.ValueUnchecked = "0"
        '
        'FNHSysFixedAssetId
        '
        Me.FNHSysFixedAssetId.Caption = "FNHSysFixedAssetId"
        Me.FNHSysFixedAssetId.FieldName = "FNHSysFixedAssetId"
        Me.FNHSysFixedAssetId.Name = "FNHSysFixedAssetId"
        '
        'FTLocationAsset
        '
        Me.FTLocationAsset.Caption = "FTLocationAsset"
        Me.FTLocationAsset.FieldName = "FTLocationAsset"
        Me.FTLocationAsset.Name = "FTLocationAsset"
        Me.FTLocationAsset.OptionsColumn.AllowEdit = False
        Me.FTLocationAsset.OptionsColumn.ReadOnly = True
        Me.FTLocationAsset.Visible = True
        Me.FTLocationAsset.VisibleIndex = 5
        Me.FTLocationAsset.Width = 110
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
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(825, 229)
        Me.ogbmainprocbutton.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(849, 58)
        Me.ogbmainprocbutton.TabIndex = 3
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmedit
        '
        Me.ocmedit.Location = New System.Drawing.Point(483, 14)
        Me.ocmedit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmedit.Name = "ocmedit"
        Me.ocmedit.Size = New System.Drawing.Size(111, 31)
        Me.ocmedit.TabIndex = 98
        Me.ocmedit.TabStop = False
        Me.ocmedit.Tag = "2|"
        Me.ocmedit.Text = "EDIT"
        '
        'ocmpreview
        '
        Me.ocmpreview.Location = New System.Drawing.Point(365, 14)
        Me.ocmpreview.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmpreview.Name = "ocmpreview"
        Me.ocmpreview.Size = New System.Drawing.Size(111, 31)
        Me.ocmpreview.TabIndex = 97
        Me.ocmpreview.TabStop = False
        Me.ocmpreview.Tag = "2|"
        Me.ocmpreview.Text = "PREVIEW"
        Me.ocmpreview.Visible = False
        '
        'ocmexit
        '
        Me.ocmexit.Location = New System.Drawing.Point(723, 14)
        Me.ocmexit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmexit.Name = "ocmexit"
        Me.ocmexit.Size = New System.Drawing.Size(111, 31)
        Me.ocmexit.TabIndex = 96
        Me.ocmexit.TabStop = False
        Me.ocmexit.Tag = "2|"
        Me.ocmexit.Text = "EXIT"
        '
        'ocmrefresh
        '
        Me.ocmrefresh.Location = New System.Drawing.Point(246, 14)
        Me.ocmrefresh.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmrefresh.Name = "ocmrefresh"
        Me.ocmrefresh.Size = New System.Drawing.Size(111, 31)
        Me.ocmrefresh.TabIndex = 95
        Me.ocmrefresh.TabStop = False
        Me.ocmrefresh.Tag = "2|"
        Me.ocmrefresh.Text = "CLEAR"
        '
        'ocmdelete
        '
        Me.ocmdelete.Location = New System.Drawing.Point(128, 14)
        Me.ocmdelete.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmdelete.Name = "ocmdelete"
        Me.ocmdelete.Size = New System.Drawing.Size(111, 31)
        Me.ocmdelete.TabIndex = 94
        Me.ocmdelete.TabStop = False
        Me.ocmdelete.Tag = "2|"
        Me.ocmdelete.Text = "DELETE"
        '
        'ocmaddnew
        '
        Me.ocmaddnew.Location = New System.Drawing.Point(10, 14)
        Me.ocmaddnew.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ocmaddnew.Name = "ocmaddnew"
        Me.ocmaddnew.Size = New System.Drawing.Size(111, 31)
        Me.ocmaddnew.TabIndex = 93
        Me.ocmaddnew.TabStop = False
        Me.ocmaddnew.Tag = "2|"
        Me.ocmaddnew.Text = "NEW"
        '
        'wAsset
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(3347, 646)
        Me.Controls.Add(Me.ogbmainprocbutton)
        Me.Controls.Add(Me.ogc)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "wAsset"
        Me.Text = "wAsset"
        CType(Me.ogc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepStateCritical, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepStateActive, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogc As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogv As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmedit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmpreview As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmexit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmrefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmdelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmaddnew As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FTCmpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents AssetTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAssetCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAssetName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAssetModelName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAssetBrandName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSerialNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAssetGrpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAssetTypeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTProductCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDDateAdd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDDateUsed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDDateStartWarranty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDDateEndWarranty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMaxPower As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitSectCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTEmpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTSuplName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTCurCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDPurchaseDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTPurchaseBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTInvoiceNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDInvoiceDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTReceiveNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FDReceiveDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTReceiveBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMaximumStock As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNMinimumStock As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateCritical As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTStateActive As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepStateCritical As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents RepStateActive As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FNHSysFixedAssetId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAssetNameTH As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTAssetNameEN As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTRefer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTUnitAssetCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTLocationAsset As DevExpress.XtraGrid.Columns.GridColumn
End Class
