﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wCustomerOrderValueTracking
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
        Dim SerializableAppearanceObject1 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject2 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject3 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject4 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject5 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Dim SerializableAppearanceObject6 As DevExpress.Utils.SerializableAppearanceObject = New DevExpress.Utils.SerializableAppearanceObject()
        Me.ogbCmp = New DevExpress.XtraEditors.GroupControl()
        Me.ogbmainprocbutton = New DevExpress.XtraEditors.PanelControl()
        Me.ocmRefresh = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmclearclsr = New DevExpress.XtraEditors.SimpleButton()
        Me.ocmExit = New DevExpress.XtraEditors.SimpleButton()
        Me.FNHSysBuyId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysBuyId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysBuyId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysSeasonId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysSeasonId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysSeasonId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysCustId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCustId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysCustId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysStyleId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysStyleId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysStyleId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FTPORef_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.FNHSysPOID = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysCmpId_None = New DevExpress.XtraEditors.TextEdit()
        Me.FNHSysCmpId = New DevExpress.XtraEditors.ButtonEdit()
        Me.FNHSysCmpId_lbl = New DevExpress.XtraEditors.LabelControl()
        Me.otcFactoryOrderNo = New DevExpress.XtraTab.XtraTabControl()
        Me.otpSubOrderNoBreakdown = New DevExpress.XtraTab.XtraTabPage()
        Me.ogdAdjustSubFONoBreakdown = New DevExpress.XtraGrid.GridControl()
        Me.ogvAdjustSubFONoBreakdown = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.oColFTOrderNo_Breakdown = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFTSubOrderNo_Breakdown = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSeasonCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTOrderSetType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTCurCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysMatColorId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTCustomerPO = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTNikePOLineItem = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFTStateHold = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ReposFTStateHold = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.FTMatColorCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMatColorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNHSysMatSizeId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMatSizeCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTMatSizeName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNQunatity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCalcEditReal0 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.oColFNPrice = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCalcEditReal2 = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.FNPriceOrg = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FNPriceDiff = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNOperateFeeAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNNetFOB = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNExtraQty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNQuantityExtra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNGrandQuantity = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNAmntExtra = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNGrandAmnt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNGarmentQtyTest = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryFNGarmentQtyTest = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
        Me.oColBreakdownFTStyleCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColBreakdownFTBuyCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNHSysBuyIdBreakdown = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNHSysBuyIdCodeBreakdown = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.oColFNHSysBuyIdNameBreakdown = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cFNHSysStyleId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.FTSubOrderNoRef = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CFNHSysSeasonId = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.ogbCmp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbCmp.SuspendLayout()
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ogbmainprocbutton.SuspendLayout()
        CType(Me.FNHSysBuyId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysBuyId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysSeasonId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysSeasonId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCustId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCustId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysPOID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.otcFactoryOrderNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.otcFactoryOrderNo.SuspendLayout()
        Me.otpSubOrderNoBreakdown.SuspendLayout()
        CType(Me.ogdAdjustSubFONoBreakdown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvAdjustSubFONoBreakdown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReposFTStateHold, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEditReal0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCalcEditReal2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryFNGarmentQtyTest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogbCmp
        '
        Me.ogbCmp.Controls.Add(Me.ogbmainprocbutton)
        Me.ogbCmp.Controls.Add(Me.FNHSysBuyId_None)
        Me.ogbCmp.Controls.Add(Me.FNHSysBuyId_lbl)
        Me.ogbCmp.Controls.Add(Me.FNHSysBuyId)
        Me.ogbCmp.Controls.Add(Me.FNHSysSeasonId_None)
        Me.ogbCmp.Controls.Add(Me.FNHSysSeasonId_lbl)
        Me.ogbCmp.Controls.Add(Me.FNHSysSeasonId)
        Me.ogbCmp.Controls.Add(Me.FNHSysCustId_None)
        Me.ogbCmp.Controls.Add(Me.FNHSysCustId_lbl)
        Me.ogbCmp.Controls.Add(Me.FNHSysCustId)
        Me.ogbCmp.Controls.Add(Me.FNHSysStyleId_None)
        Me.ogbCmp.Controls.Add(Me.FNHSysStyleId_lbl)
        Me.ogbCmp.Controls.Add(Me.FNHSysStyleId)
        Me.ogbCmp.Controls.Add(Me.FTPORef_lbl)
        Me.ogbCmp.Controls.Add(Me.FNHSysPOID)
        Me.ogbCmp.Controls.Add(Me.FNHSysCmpId_None)
        Me.ogbCmp.Controls.Add(Me.FNHSysCmpId)
        Me.ogbCmp.Controls.Add(Me.FNHSysCmpId_lbl)
        Me.ogbCmp.Dock = System.Windows.Forms.DockStyle.Top
        Me.ogbCmp.Location = New System.Drawing.Point(0, 0)
        Me.ogbCmp.Name = "ogbCmp"
        Me.ogbCmp.ShowCaption = False
        Me.ogbCmp.Size = New System.Drawing.Size(1023, 82)
        Me.ogbCmp.TabIndex = 0
        '
        'ogbmainprocbutton
        '
        Me.ogbmainprocbutton.Controls.Add(Me.ocmRefresh)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmclearclsr)
        Me.ogbmainprocbutton.Controls.Add(Me.ocmExit)
        Me.ogbmainprocbutton.Location = New System.Drawing.Point(232, 20)
        Me.ogbmainprocbutton.Name = "ogbmainprocbutton"
        Me.ogbmainprocbutton.Size = New System.Drawing.Size(522, 72)
        Me.ogbmainprocbutton.TabIndex = 462
        Me.ogbmainprocbutton.Tag = "2|"
        '
        'ocmRefresh
        '
        Me.ocmRefresh.Location = New System.Drawing.Point(21, 11)
        Me.ocmRefresh.Name = "ocmRefresh"
        Me.ocmRefresh.Size = New System.Drawing.Size(96, 25)
        Me.ocmRefresh.TabIndex = 99
        Me.ocmRefresh.TabStop = False
        Me.ocmRefresh.Tag = "2|"
        Me.ocmRefresh.Text = "REFRESH"
        '
        'ocmclearclsr
        '
        Me.ocmclearclsr.Location = New System.Drawing.Point(155, 11)
        Me.ocmclearclsr.Name = "ocmclearclsr"
        Me.ocmclearclsr.Size = New System.Drawing.Size(95, 25)
        Me.ocmclearclsr.TabIndex = 97
        Me.ocmclearclsr.TabStop = False
        Me.ocmclearclsr.Tag = "2|"
        Me.ocmclearclsr.Text = "CLEAR"
        '
        'ocmExit
        '
        Me.ocmExit.Location = New System.Drawing.Point(296, 11)
        Me.ocmExit.Name = "ocmExit"
        Me.ocmExit.Size = New System.Drawing.Size(95, 25)
        Me.ocmExit.TabIndex = 96
        Me.ocmExit.TabStop = False
        Me.ocmExit.Tag = "2|"
        Me.ocmExit.Text = "EXIT"
        '
        'FNHSysBuyId_None
        '
        Me.FNHSysBuyId_None.Location = New System.Drawing.Point(792, 28)
        Me.FNHSysBuyId_None.Name = "FNHSysBuyId_None"
        Me.FNHSysBuyId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysBuyId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysBuyId_None.Properties.ReadOnly = True
        Me.FNHSysBuyId_None.Size = New System.Drawing.Size(219, 20)
        Me.FNHSysBuyId_None.TabIndex = 473
        Me.FNHSysBuyId_None.Tag = "2|"
        '
        'FNHSysBuyId_lbl
        '
        Me.FNHSysBuyId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysBuyId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysBuyId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysBuyId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysBuyId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysBuyId_lbl.Location = New System.Drawing.Point(494, 28)
        Me.FNHSysBuyId_lbl.Name = "FNHSysBuyId_lbl"
        Me.FNHSysBuyId_lbl.Size = New System.Drawing.Size(156, 20)
        Me.FNHSysBuyId_lbl.TabIndex = 471
        Me.FNHSysBuyId_lbl.Tag = "2|"
        Me.FNHSysBuyId_lbl.Text = "Buy :"
        '
        'FNHSysBuyId
        '
        Me.FNHSysBuyId.Location = New System.Drawing.Point(656, 28)
        Me.FNHSysBuyId.Name = "FNHSysBuyId"
        Me.FNHSysBuyId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "113", Nothing, True)})
        Me.FNHSysBuyId.Properties.Tag = ""
        Me.FNHSysBuyId.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysBuyId.TabIndex = 4
        Me.FNHSysBuyId.Tag = "2|"
        '
        'FNHSysSeasonId_None
        '
        Me.FNHSysSeasonId_None.Location = New System.Drawing.Point(792, 52)
        Me.FNHSysSeasonId_None.Name = "FNHSysSeasonId_None"
        Me.FNHSysSeasonId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysSeasonId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysSeasonId_None.Properties.ReadOnly = True
        Me.FNHSysSeasonId_None.Size = New System.Drawing.Size(219, 20)
        Me.FNHSysSeasonId_None.TabIndex = 476
        Me.FNHSysSeasonId_None.Tag = "2|"
        '
        'FNHSysSeasonId_lbl
        '
        Me.FNHSysSeasonId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysSeasonId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysSeasonId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysSeasonId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysSeasonId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysSeasonId_lbl.Location = New System.Drawing.Point(494, 52)
        Me.FNHSysSeasonId_lbl.Name = "FNHSysSeasonId_lbl"
        Me.FNHSysSeasonId_lbl.Size = New System.Drawing.Size(156, 20)
        Me.FNHSysSeasonId_lbl.TabIndex = 474
        Me.FNHSysSeasonId_lbl.Tag = "2|"
        Me.FNHSysSeasonId_lbl.Text = "Season :"
        '
        'FNHSysSeasonId
        '
        Me.FNHSysSeasonId.Location = New System.Drawing.Point(656, 52)
        Me.FNHSysSeasonId.Name = "FNHSysSeasonId"
        Me.FNHSysSeasonId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "94", Nothing, True)})
        Me.FNHSysSeasonId.Properties.Tag = ""
        Me.FNHSysSeasonId.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysSeasonId.TabIndex = 5
        Me.FNHSysSeasonId.Tag = "2|"
        '
        'FNHSysCustId_None
        '
        Me.FNHSysCustId_None.Location = New System.Drawing.Point(792, 6)
        Me.FNHSysCustId_None.Name = "FNHSysCustId_None"
        Me.FNHSysCustId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCustId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCustId_None.Properties.ReadOnly = True
        Me.FNHSysCustId_None.Size = New System.Drawing.Size(219, 20)
        Me.FNHSysCustId_None.TabIndex = 470
        Me.FNHSysCustId_None.Tag = "2|"
        '
        'FNHSysCustId_lbl
        '
        Me.FNHSysCustId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCustId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCustId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCustId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCustId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCustId_lbl.Location = New System.Drawing.Point(494, 6)
        Me.FNHSysCustId_lbl.Name = "FNHSysCustId_lbl"
        Me.FNHSysCustId_lbl.Size = New System.Drawing.Size(156, 20)
        Me.FNHSysCustId_lbl.TabIndex = 468
        Me.FNHSysCustId_lbl.Tag = "2|"
        Me.FNHSysCustId_lbl.Text = "Customer :"
        '
        'FNHSysCustId
        '
        Me.FNHSysCustId.Location = New System.Drawing.Point(656, 6)
        Me.FNHSysCustId.Name = "FNHSysCustId"
        Me.FNHSysCustId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject3, "", "83", Nothing, True)})
        Me.FNHSysCustId.Properties.Tag = ""
        Me.FNHSysCustId.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysCustId.TabIndex = 3
        Me.FNHSysCustId.Tag = "2|"
        '
        'FNHSysStyleId_None
        '
        Me.FNHSysStyleId_None.Location = New System.Drawing.Point(287, 52)
        Me.FNHSysStyleId_None.Name = "FNHSysStyleId_None"
        Me.FNHSysStyleId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysStyleId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysStyleId_None.Properties.ReadOnly = True
        Me.FNHSysStyleId_None.Size = New System.Drawing.Size(199, 20)
        Me.FNHSysStyleId_None.TabIndex = 467
        Me.FNHSysStyleId_None.Tag = "2|"
        '
        'FNHSysStyleId_lbl
        '
        Me.FNHSysStyleId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysStyleId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysStyleId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysStyleId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysStyleId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysStyleId_lbl.Location = New System.Drawing.Point(0, 54)
        Me.FNHSysStyleId_lbl.Name = "FNHSysStyleId_lbl"
        Me.FNHSysStyleId_lbl.Size = New System.Drawing.Size(148, 20)
        Me.FNHSysStyleId_lbl.TabIndex = 465
        Me.FNHSysStyleId_lbl.Tag = "2|"
        Me.FNHSysStyleId_lbl.Text = "Style No :"
        '
        'FNHSysStyleId
        '
        Me.FNHSysStyleId.Location = New System.Drawing.Point(154, 52)
        Me.FNHSysStyleId.Name = "FNHSysStyleId"
        Me.FNHSysStyleId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject4, "", "89", Nothing, True)})
        Me.FNHSysStyleId.Properties.Tag = ""
        Me.FNHSysStyleId.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysStyleId.TabIndex = 2
        Me.FNHSysStyleId.Tag = "2|"
        '
        'FTPORef_lbl
        '
        Me.FTPORef_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FTPORef_lbl.Appearance.Options.UseForeColor = True
        Me.FTPORef_lbl.Appearance.Options.UseTextOptions = True
        Me.FTPORef_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FTPORef_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FTPORef_lbl.Location = New System.Drawing.Point(0, 28)
        Me.FTPORef_lbl.Name = "FTPORef_lbl"
        Me.FTPORef_lbl.Size = New System.Drawing.Size(148, 20)
        Me.FTPORef_lbl.TabIndex = 463
        Me.FTPORef_lbl.Tag = "2|"
        Me.FTPORef_lbl.Text = "Cust. PO.:"
        '
        'FNHSysPOID
        '
        Me.FNHSysPOID.Location = New System.Drawing.Point(154, 28)
        Me.FNHSysPOID.Name = "FNHSysPOID"
        Me.FNHSysPOID.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject5, "", "145", Nothing, True)})
        Me.FNHSysPOID.Properties.Tag = ""
        Me.FNHSysPOID.Size = New System.Drawing.Size(130, 20)
        Me.FNHSysPOID.TabIndex = 1
        Me.FNHSysPOID.Tag = "2|"
        '
        'FNHSysCmpId_None
        '
        Me.FNHSysCmpId_None.Location = New System.Drawing.Point(286, 6)
        Me.FNHSysCmpId_None.Name = "FNHSysCmpId_None"
        Me.FNHSysCmpId_None.Properties.Appearance.BackColor = System.Drawing.Color.LightCyan
        Me.FNHSysCmpId_None.Properties.Appearance.Options.UseBackColor = True
        Me.FNHSysCmpId_None.Properties.ReadOnly = True
        Me.FNHSysCmpId_None.Size = New System.Drawing.Size(196, 20)
        Me.FNHSysCmpId_None.TabIndex = 450
        Me.FNHSysCmpId_None.Tag = ""
        '
        'FNHSysCmpId
        '
        Me.FNHSysCmpId.Location = New System.Drawing.Point(155, 6)
        Me.FNHSysCmpId.Name = "FNHSysCmpId"
        Me.FNHSysCmpId.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Nothing, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject6, "", "11", Nothing, True)})
        Me.FNHSysCmpId.Properties.Tag = ""
        Me.FNHSysCmpId.Size = New System.Drawing.Size(129, 20)
        Me.FNHSysCmpId.TabIndex = 0
        Me.FNHSysCmpId.Tag = ""
        '
        'FNHSysCmpId_lbl
        '
        Me.FNHSysCmpId_lbl.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.FNHSysCmpId_lbl.Appearance.Options.UseForeColor = True
        Me.FNHSysCmpId_lbl.Appearance.Options.UseTextOptions = True
        Me.FNHSysCmpId_lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        Me.FNHSysCmpId_lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
        Me.FNHSysCmpId_lbl.Location = New System.Drawing.Point(5, 7)
        Me.FNHSysCmpId_lbl.Name = "FNHSysCmpId_lbl"
        Me.FNHSysCmpId_lbl.Size = New System.Drawing.Size(147, 17)
        Me.FNHSysCmpId_lbl.TabIndex = 448
        Me.FNHSysCmpId_lbl.Tag = "2|"
        Me.FNHSysCmpId_lbl.Text = "Company :"
        '
        'otcFactoryOrderNo
        '
        Me.otcFactoryOrderNo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.otcFactoryOrderNo.Location = New System.Drawing.Point(0, 89)
        Me.otcFactoryOrderNo.Name = "otcFactoryOrderNo"
        Me.otcFactoryOrderNo.SelectedTabPage = Me.otpSubOrderNoBreakdown
        Me.otcFactoryOrderNo.Size = New System.Drawing.Size(1023, 548)
        Me.otcFactoryOrderNo.TabIndex = 2
        Me.otcFactoryOrderNo.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.otpSubOrderNoBreakdown})
        '
        'otpSubOrderNoBreakdown
        '
        Me.otpSubOrderNoBreakdown.Controls.Add(Me.ogdAdjustSubFONoBreakdown)
        Me.otpSubOrderNoBreakdown.Name = "otpSubOrderNoBreakdown"
        Me.otpSubOrderNoBreakdown.Size = New System.Drawing.Size(1015, 518)
        Me.otpSubOrderNoBreakdown.Text = "Sub Order No Breakdown"
        '
        'ogdAdjustSubFONoBreakdown
        '
        Me.ogdAdjustSubFONoBreakdown.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ogdAdjustSubFONoBreakdown.Location = New System.Drawing.Point(0, 0)
        Me.ogdAdjustSubFONoBreakdown.MainView = Me.ogvAdjustSubFONoBreakdown
        Me.ogdAdjustSubFONoBreakdown.Name = "ogdAdjustSubFONoBreakdown"
        Me.ogdAdjustSubFONoBreakdown.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCalcEditReal0, Me.RepositoryItemCalcEditReal2, Me.RepositoryFNGarmentQtyTest, Me.ReposFTStateHold})
        Me.ogdAdjustSubFONoBreakdown.Size = New System.Drawing.Size(1009, 508)
        Me.ogdAdjustSubFONoBreakdown.TabIndex = 0
        Me.ogdAdjustSubFONoBreakdown.Tag = "3"
        Me.ogdAdjustSubFONoBreakdown.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvAdjustSubFONoBreakdown})
        '
        'ogvAdjustSubFONoBreakdown
        '
        Me.ogvAdjustSubFONoBreakdown.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.oColFTOrderNo_Breakdown, Me.oColFTSubOrderNo_Breakdown, Me.FTSeasonCode, Me.CFTOrderSetType, Me.FTCurCode, Me.FNHSysMatColorId, Me.CFTCustomerPO, Me.CFTNikePOLineItem, Me.CFTStateHold, Me.FTMatColorCode, Me.FTMatColorName, Me.FNHSysMatSizeId, Me.FTMatSizeCode, Me.FTMatSizeName, Me.oColFNQunatity, Me.oColFNPrice, Me.FNPriceOrg, Me.FNPriceDiff, Me.CFNOperateFeeAmt, Me.CFNNetFOB, Me.oColFNAmt, Me.oColFNExtraQty, Me.oColFNQuantityExtra, Me.oColFNGrandQuantity, Me.oColFNAmntExtra, Me.oColFNGrandAmnt, Me.oColFNGarmentQtyTest, Me.oColBreakdownFTStyleCode, Me.oColBreakdownFTBuyCode, Me.oColFNHSysBuyIdBreakdown, Me.oColFNHSysBuyIdCodeBreakdown, Me.oColFNHSysBuyIdNameBreakdown, Me.cFNHSysStyleId, Me.FTSubOrderNoRef, Me.CFNHSysSeasonId})
        Me.ogvAdjustSubFONoBreakdown.GridControl = Me.ogdAdjustSubFONoBreakdown
        Me.ogvAdjustSubFONoBreakdown.Name = "ogvAdjustSubFONoBreakdown"
        Me.ogvAdjustSubFONoBreakdown.OptionsView.ShowFooter = True
        Me.ogvAdjustSubFONoBreakdown.OptionsView.ShowGroupPanel = False
        '
        'oColFTOrderNo_Breakdown
        '
        Me.oColFTOrderNo_Breakdown.Caption = "FTOrderNo"
        Me.oColFTOrderNo_Breakdown.FieldName = "FTOrderNo"
        Me.oColFTOrderNo_Breakdown.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.oColFTOrderNo_Breakdown.Name = "oColFTOrderNo_Breakdown"
        Me.oColFTOrderNo_Breakdown.OptionsColumn.AllowEdit = False
        Me.oColFTOrderNo_Breakdown.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFTOrderNo_Breakdown.OptionsColumn.ReadOnly = True
        Me.oColFTOrderNo_Breakdown.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "FTOrderNo", "{0}")})
        Me.oColFTOrderNo_Breakdown.Visible = True
        Me.oColFTOrderNo_Breakdown.VisibleIndex = 0
        Me.oColFTOrderNo_Breakdown.Width = 120
        '
        'oColFTSubOrderNo_Breakdown
        '
        Me.oColFTSubOrderNo_Breakdown.Caption = "FTSubOrderNo"
        Me.oColFTSubOrderNo_Breakdown.FieldName = "FTSubOrderNo"
        Me.oColFTSubOrderNo_Breakdown.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.oColFTSubOrderNo_Breakdown.Name = "oColFTSubOrderNo_Breakdown"
        Me.oColFTSubOrderNo_Breakdown.OptionsColumn.AllowEdit = False
        Me.oColFTSubOrderNo_Breakdown.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFTSubOrderNo_Breakdown.OptionsColumn.ReadOnly = True
        Me.oColFTSubOrderNo_Breakdown.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "FTSubOrderNo", "{0}")})
        Me.oColFTSubOrderNo_Breakdown.Visible = True
        Me.oColFTSubOrderNo_Breakdown.VisibleIndex = 1
        Me.oColFTSubOrderNo_Breakdown.Width = 120
        '
        'FTSeasonCode
        '
        Me.FTSeasonCode.Caption = "Season Code"
        Me.FTSeasonCode.FieldName = "FTSeasonCode"
        Me.FTSeasonCode.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.FTSeasonCode.Name = "FTSeasonCode"
        Me.FTSeasonCode.OptionsColumn.AllowEdit = False
        Me.FTSeasonCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSeasonCode.OptionsColumn.ReadOnly = True
        Me.FTSeasonCode.Visible = True
        Me.FTSeasonCode.VisibleIndex = 3
        Me.FTSeasonCode.Width = 87
        '
        'CFTOrderSetType
        '
        Me.CFTOrderSetType.Caption = "Set Type"
        Me.CFTOrderSetType.FieldName = "FTOrderSetType"
        Me.CFTOrderSetType.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.CFTOrderSetType.Name = "CFTOrderSetType"
        Me.CFTOrderSetType.OptionsColumn.AllowEdit = False
        Me.CFTOrderSetType.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTOrderSetType.OptionsColumn.ReadOnly = True
        Me.CFTOrderSetType.Visible = True
        Me.CFTOrderSetType.VisibleIndex = 2
        '
        'FTCurCode
        '
        Me.FTCurCode.Caption = "สกุลเงิน"
        Me.FTCurCode.FieldName = "FTCurCode"
        Me.FTCurCode.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.FTCurCode.Name = "FTCurCode"
        Me.FTCurCode.OptionsColumn.AllowEdit = False
        Me.FTCurCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTCurCode.OptionsColumn.ReadOnly = True
        Me.FTCurCode.Visible = True
        Me.FTCurCode.VisibleIndex = 4
        Me.FTCurCode.Width = 62
        '
        'FNHSysMatColorId
        '
        Me.FNHSysMatColorId.Caption = "FNHSysMatColorId"
        Me.FNHSysMatColorId.FieldName = "FNHSysMatColorId"
        Me.FNHSysMatColorId.Name = "FNHSysMatColorId"
        Me.FNHSysMatColorId.OptionsColumn.AllowEdit = False
        Me.FNHSysMatColorId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysMatColorId.OptionsColumn.ReadOnly = True
        '
        'CFTCustomerPO
        '
        Me.CFTCustomerPO.Caption = "Customer PO"
        Me.CFTCustomerPO.FieldName = "FTCustomerPO"
        Me.CFTCustomerPO.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.CFTCustomerPO.Name = "CFTCustomerPO"
        Me.CFTCustomerPO.OptionsColumn.AllowEdit = False
        Me.CFTCustomerPO.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTCustomerPO.OptionsColumn.ReadOnly = True
        Me.CFTCustomerPO.Visible = True
        Me.CFTCustomerPO.VisibleIndex = 5
        Me.CFTCustomerPO.Width = 94
        '
        'CFTNikePOLineItem
        '
        Me.CFTNikePOLineItem.Caption = "PO Line"
        Me.CFTNikePOLineItem.FieldName = "FTNikePOLineItem"
        Me.CFTNikePOLineItem.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.CFTNikePOLineItem.Name = "CFTNikePOLineItem"
        Me.CFTNikePOLineItem.OptionsColumn.AllowEdit = False
        Me.CFTNikePOLineItem.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTNikePOLineItem.OptionsColumn.ReadOnly = True
        Me.CFTNikePOLineItem.Visible = True
        Me.CFTNikePOLineItem.VisibleIndex = 6
        '
        'CFTStateHold
        '
        Me.CFTStateHold.Caption = "Hold"
        Me.CFTStateHold.ColumnEdit = Me.ReposFTStateHold
        Me.CFTStateHold.FieldName = "FTStateHold"
        Me.CFTStateHold.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
        Me.CFTStateHold.Name = "CFTStateHold"
        Me.CFTStateHold.OptionsColumn.AllowEdit = False
        Me.CFTStateHold.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFTStateHold.OptionsColumn.ReadOnly = True
        Me.CFTStateHold.Visible = True
        Me.CFTStateHold.VisibleIndex = 7
        Me.CFTStateHold.Width = 61
        '
        'ReposFTStateHold
        '
        Me.ReposFTStateHold.AutoHeight = False
        Me.ReposFTStateHold.Caption = "Check"
        Me.ReposFTStateHold.Name = "ReposFTStateHold"
        Me.ReposFTStateHold.ValueChecked = "1"
        Me.ReposFTStateHold.ValueUnchecked = "0"
        '
        'FTMatColorCode
        '
        Me.FTMatColorCode.Caption = "FTMatColorCode"
        Me.FTMatColorCode.FieldName = "FTMatColorCode"
        Me.FTMatColorCode.Name = "FTMatColorCode"
        Me.FTMatColorCode.OptionsColumn.AllowEdit = False
        Me.FTMatColorCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTMatColorCode.OptionsColumn.ReadOnly = True
        Me.FTMatColorCode.Visible = True
        Me.FTMatColorCode.VisibleIndex = 8
        Me.FTMatColorCode.Width = 22
        '
        'FTMatColorName
        '
        Me.FTMatColorName.Caption = "FTMatColorName"
        Me.FTMatColorName.FieldName = "FTMatColorName"
        Me.FTMatColorName.Name = "FTMatColorName"
        Me.FTMatColorName.OptionsColumn.AllowEdit = False
        Me.FTMatColorName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTMatColorName.OptionsColumn.ReadOnly = True
        Me.FTMatColorName.Visible = True
        Me.FTMatColorName.VisibleIndex = 9
        Me.FTMatColorName.Width = 64
        '
        'FNHSysMatSizeId
        '
        Me.FNHSysMatSizeId.Caption = "FNHSysMatSizeId"
        Me.FNHSysMatSizeId.FieldName = "FNHSysMatSizeId"
        Me.FNHSysMatSizeId.Name = "FNHSysMatSizeId"
        Me.FNHSysMatSizeId.OptionsColumn.AllowEdit = False
        Me.FNHSysMatSizeId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNHSysMatSizeId.OptionsColumn.ReadOnly = True
        '
        'FTMatSizeCode
        '
        Me.FTMatSizeCode.Caption = "FTMatSizeCode"
        Me.FTMatSizeCode.FieldName = "FTMatSizeCode"
        Me.FTMatSizeCode.Name = "FTMatSizeCode"
        Me.FTMatSizeCode.OptionsColumn.AllowEdit = False
        Me.FTMatSizeCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTMatSizeCode.OptionsColumn.ReadOnly = True
        Me.FTMatSizeCode.Visible = True
        Me.FTMatSizeCode.VisibleIndex = 10
        Me.FTMatSizeCode.Width = 27
        '
        'FTMatSizeName
        '
        Me.FTMatSizeName.Caption = "FTMatSizeName"
        Me.FTMatSizeName.FieldName = "FTMatSizeName"
        Me.FTMatSizeName.Name = "FTMatSizeName"
        Me.FTMatSizeName.OptionsColumn.AllowEdit = False
        Me.FTMatSizeName.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTMatSizeName.OptionsColumn.ReadOnly = True
        Me.FTMatSizeName.Visible = True
        Me.FTMatSizeName.VisibleIndex = 11
        Me.FTMatSizeName.Width = 37
        '
        'oColFNQunatity
        '
        Me.oColFNQunatity.Caption = "FNQuantity"
        Me.oColFNQunatity.ColumnEdit = Me.RepositoryItemCalcEditReal0
        Me.oColFNQunatity.FieldName = "FNQuantity"
        Me.oColFNQunatity.Name = "oColFNQunatity"
        Me.oColFNQunatity.OptionsColumn.AllowEdit = False
        Me.oColFNQunatity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFNQunatity.OptionsColumn.ReadOnly = True
        Me.oColFNQunatity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantity", "{0:n0}")})
        Me.oColFNQunatity.Visible = True
        Me.oColFNQunatity.VisibleIndex = 12
        Me.oColFNQunatity.Width = 27
        '
        'RepositoryItemCalcEditReal0
        '
        Me.RepositoryItemCalcEditReal0.AutoHeight = False
        Me.RepositoryItemCalcEditReal0.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEditReal0.DisplayFormat.FormatString = "N0"
        Me.RepositoryItemCalcEditReal0.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemCalcEditReal0.EditFormat.FormatString = "N0"
        Me.RepositoryItemCalcEditReal0.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemCalcEditReal0.Name = "RepositoryItemCalcEditReal0"
        '
        'oColFNPrice
        '
        Me.oColFNPrice.Caption = "FNPrice"
        Me.oColFNPrice.ColumnEdit = Me.RepositoryItemCalcEditReal2
        Me.oColFNPrice.DisplayFormat.FormatString = "{0:n4}"
        Me.oColFNPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.oColFNPrice.FieldName = "FNPrice"
        Me.oColFNPrice.Name = "oColFNPrice"
        Me.oColFNPrice.OptionsColumn.AllowEdit = False
        Me.oColFNPrice.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFNPrice.OptionsColumn.ReadOnly = True
        Me.oColFNPrice.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNPrice", "{0:n2}")})
        Me.oColFNPrice.Visible = True
        Me.oColFNPrice.VisibleIndex = 13
        Me.oColFNPrice.Width = 27
        '
        'RepositoryItemCalcEditReal2
        '
        Me.RepositoryItemCalcEditReal2.AutoHeight = False
        Me.RepositoryItemCalcEditReal2.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryItemCalcEditReal2.DisplayFormat.FormatString = "N2"
        Me.RepositoryItemCalcEditReal2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemCalcEditReal2.EditFormat.FormatString = "N2"
        Me.RepositoryItemCalcEditReal2.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemCalcEditReal2.Name = "RepositoryItemCalcEditReal2"
        '
        'FNPriceOrg
        '
        Me.FNPriceOrg.Caption = "Price Org"
        Me.FNPriceOrg.DisplayFormat.FormatString = "{0:n4}"
        Me.FNPriceOrg.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPriceOrg.FieldName = "FNPriceOrg"
        Me.FNPriceOrg.Name = "FNPriceOrg"
        Me.FNPriceOrg.OptionsColumn.AllowEdit = False
        Me.FNPriceOrg.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNPriceOrg.OptionsColumn.ReadOnly = True
        Me.FNPriceOrg.Visible = True
        Me.FNPriceOrg.VisibleIndex = 14
        Me.FNPriceOrg.Width = 34
        '
        'FNPriceDiff
        '
        Me.FNPriceDiff.Caption = "Price Diff"
        Me.FNPriceDiff.DisplayFormat.FormatString = "{0:n2}"
        Me.FNPriceDiff.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.FNPriceDiff.FieldName = "FNPriceDiff"
        Me.FNPriceDiff.Name = "FNPriceDiff"
        Me.FNPriceDiff.OptionsColumn.AllowEdit = False
        Me.FNPriceDiff.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FNPriceDiff.OptionsColumn.ReadOnly = True
        Me.FNPriceDiff.Visible = True
        Me.FNPriceDiff.VisibleIndex = 15
        Me.FNPriceDiff.Width = 34
        '
        'CFNOperateFeeAmt
        '
        Me.CFNOperateFeeAmt.AppearanceCell.ForeColor = System.Drawing.Color.Black
        Me.CFNOperateFeeAmt.AppearanceCell.Options.UseForeColor = True
        Me.CFNOperateFeeAmt.Caption = "OF. Amt"
        Me.CFNOperateFeeAmt.DisplayFormat.FormatString = "{0:n4}"
        Me.CFNOperateFeeAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNOperateFeeAmt.FieldName = "FNOperateFeeAmt"
        Me.CFNOperateFeeAmt.Name = "CFNOperateFeeAmt"
        Me.CFNOperateFeeAmt.OptionsColumn.AllowEdit = False
        Me.CFNOperateFeeAmt.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNOperateFeeAmt.OptionsColumn.ReadOnly = True
        Me.CFNOperateFeeAmt.Visible = True
        Me.CFNOperateFeeAmt.VisibleIndex = 16
        '
        'CFNNetFOB
        '
        Me.CFNNetFOB.AppearanceCell.ForeColor = System.Drawing.Color.Black
        Me.CFNNetFOB.AppearanceCell.Options.UseForeColor = True
        Me.CFNNetFOB.Caption = "Net FOB"
        Me.CFNNetFOB.DisplayFormat.FormatString = "{0:n4}"
        Me.CFNNetFOB.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.CFNNetFOB.FieldName = "FNNetFOB"
        Me.CFNNetFOB.Name = "CFNNetFOB"
        Me.CFNNetFOB.OptionsColumn.AllowEdit = False
        Me.CFNNetFOB.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNNetFOB.OptionsColumn.ReadOnly = True
        Me.CFNNetFOB.Visible = True
        Me.CFNNetFOB.VisibleIndex = 17
        '
        'oColFNAmt
        '
        Me.oColFNAmt.Caption = "FNAmt"
        Me.oColFNAmt.DisplayFormat.FormatString = "N2"
        Me.oColFNAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.oColFNAmt.FieldName = "FNAmt"
        Me.oColFNAmt.Name = "oColFNAmt"
        Me.oColFNAmt.OptionsColumn.AllowEdit = False
        Me.oColFNAmt.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFNAmt.OptionsColumn.ReadOnly = True
        Me.oColFNAmt.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmt", "{0:n2}")})
        Me.oColFNAmt.Visible = True
        Me.oColFNAmt.VisibleIndex = 18
        Me.oColFNAmt.Width = 27
        '
        'oColFNExtraQty
        '
        Me.oColFNExtraQty.Caption = "FNExtraQty"
        Me.oColFNExtraQty.ColumnEdit = Me.RepositoryItemCalcEditReal2
        Me.oColFNExtraQty.FieldName = "FNExtraQty"
        Me.oColFNExtraQty.Name = "oColFNExtraQty"
        Me.oColFNExtraQty.OptionsColumn.AllowEdit = False
        Me.oColFNExtraQty.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFNExtraQty.OptionsColumn.ReadOnly = True
        Me.oColFNExtraQty.Visible = True
        Me.oColFNExtraQty.VisibleIndex = 19
        Me.oColFNExtraQty.Width = 27
        '
        'oColFNQuantityExtra
        '
        Me.oColFNQuantityExtra.Caption = "FNQuantityExtra"
        Me.oColFNQuantityExtra.DisplayFormat.FormatString = "N0"
        Me.oColFNQuantityExtra.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.oColFNQuantityExtra.FieldName = "FNQuantityExtra"
        Me.oColFNQuantityExtra.Name = "oColFNQuantityExtra"
        Me.oColFNQuantityExtra.OptionsColumn.AllowEdit = False
        Me.oColFNQuantityExtra.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFNQuantityExtra.OptionsColumn.ReadOnly = True
        Me.oColFNQuantityExtra.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNQuantityExtra", "{0:n0}")})
        Me.oColFNQuantityExtra.Visible = True
        Me.oColFNQuantityExtra.VisibleIndex = 20
        Me.oColFNQuantityExtra.Width = 27
        '
        'oColFNGrandQuantity
        '
        Me.oColFNGrandQuantity.Caption = "FNGrandQuantity"
        Me.oColFNGrandQuantity.DisplayFormat.FormatString = "N0"
        Me.oColFNGrandQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.oColFNGrandQuantity.FieldName = "FNGrandQuantity"
        Me.oColFNGrandQuantity.Name = "oColFNGrandQuantity"
        Me.oColFNGrandQuantity.OptionsColumn.AllowEdit = False
        Me.oColFNGrandQuantity.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFNGrandQuantity.OptionsColumn.ReadOnly = True
        Me.oColFNGrandQuantity.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNGrandQuantity", "{0:n0}")})
        Me.oColFNGrandQuantity.Visible = True
        Me.oColFNGrandQuantity.VisibleIndex = 21
        Me.oColFNGrandQuantity.Width = 27
        '
        'oColFNAmntExtra
        '
        Me.oColFNAmntExtra.Caption = "FNAmntExtra"
        Me.oColFNAmntExtra.DisplayFormat.FormatString = "N2"
        Me.oColFNAmntExtra.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.oColFNAmntExtra.FieldName = "FNAmntExtra"
        Me.oColFNAmntExtra.Name = "oColFNAmntExtra"
        Me.oColFNAmntExtra.OptionsColumn.AllowEdit = False
        Me.oColFNAmntExtra.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFNAmntExtra.OptionsColumn.ReadOnly = True
        Me.oColFNAmntExtra.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNAmntExtra", "{0:n2}")})
        Me.oColFNAmntExtra.Visible = True
        Me.oColFNAmntExtra.VisibleIndex = 22
        Me.oColFNAmntExtra.Width = 27
        '
        'oColFNGrandAmnt
        '
        Me.oColFNGrandAmnt.Caption = "FNGrandAmnt"
        Me.oColFNGrandAmnt.DisplayFormat.FormatString = "N2"
        Me.oColFNGrandAmnt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.oColFNGrandAmnt.FieldName = "FNGrandAmnt"
        Me.oColFNGrandAmnt.Name = "oColFNGrandAmnt"
        Me.oColFNGrandAmnt.OptionsColumn.AllowEdit = False
        Me.oColFNGrandAmnt.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFNGrandAmnt.OptionsColumn.ReadOnly = True
        Me.oColFNGrandAmnt.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNGrandAmnt", "{0:n2}")})
        Me.oColFNGrandAmnt.Visible = True
        Me.oColFNGrandAmnt.VisibleIndex = 23
        Me.oColFNGrandAmnt.Width = 27
        '
        'oColFNGarmentQtyTest
        '
        Me.oColFNGarmentQtyTest.Caption = "FNGarmentQtyTest"
        Me.oColFNGarmentQtyTest.ColumnEdit = Me.RepositoryFNGarmentQtyTest
        Me.oColFNGarmentQtyTest.DisplayFormat.FormatString = "N0"
        Me.oColFNGarmentQtyTest.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.oColFNGarmentQtyTest.FieldName = "FNGarmentQtyTest"
        Me.oColFNGarmentQtyTest.Name = "oColFNGarmentQtyTest"
        Me.oColFNGarmentQtyTest.OptionsColumn.AllowEdit = False
        Me.oColFNGarmentQtyTest.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFNGarmentQtyTest.OptionsColumn.ReadOnly = True
        Me.oColFNGarmentQtyTest.Summary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FNGarmentQtyTest", "{0:n0}")})
        Me.oColFNGarmentQtyTest.Visible = True
        Me.oColFNGarmentQtyTest.VisibleIndex = 24
        Me.oColFNGarmentQtyTest.Width = 108
        '
        'RepositoryFNGarmentQtyTest
        '
        Me.RepositoryFNGarmentQtyTest.AutoHeight = False
        Me.RepositoryFNGarmentQtyTest.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.RepositoryFNGarmentQtyTest.Name = "RepositoryFNGarmentQtyTest"
        Me.RepositoryFNGarmentQtyTest.Precision = 2
        '
        'oColBreakdownFTStyleCode
        '
        Me.oColBreakdownFTStyleCode.Caption = "FTStyleCode"
        Me.oColBreakdownFTStyleCode.FieldName = "FTStyleCode"
        Me.oColBreakdownFTStyleCode.Name = "oColBreakdownFTStyleCode"
        Me.oColBreakdownFTStyleCode.OptionsColumn.AllowEdit = False
        Me.oColBreakdownFTStyleCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColBreakdownFTStyleCode.OptionsColumn.ReadOnly = True
        '
        'oColBreakdownFTBuyCode
        '
        Me.oColBreakdownFTBuyCode.Caption = "FTBuyCode"
        Me.oColBreakdownFTBuyCode.FieldName = "FTBuyCode"
        Me.oColBreakdownFTBuyCode.Name = "oColBreakdownFTBuyCode"
        Me.oColBreakdownFTBuyCode.OptionsColumn.AllowEdit = False
        Me.oColBreakdownFTBuyCode.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColBreakdownFTBuyCode.OptionsColumn.ReadOnly = True
        '
        'oColFNHSysBuyIdBreakdown
        '
        Me.oColFNHSysBuyIdBreakdown.Caption = "FNHSysBuyId_Hide"
        Me.oColFNHSysBuyIdBreakdown.FieldName = "FNHSysBuyId_Hide"
        Me.oColFNHSysBuyIdBreakdown.Name = "oColFNHSysBuyIdBreakdown"
        Me.oColFNHSysBuyIdBreakdown.OptionsColumn.AllowEdit = False
        Me.oColFNHSysBuyIdBreakdown.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFNHSysBuyIdBreakdown.OptionsColumn.ReadOnly = True
        '
        'oColFNHSysBuyIdCodeBreakdown
        '
        Me.oColFNHSysBuyIdCodeBreakdown.Caption = "FNHSysBuyId"
        Me.oColFNHSysBuyIdCodeBreakdown.FieldName = "FNHSysBuyId"
        Me.oColFNHSysBuyIdCodeBreakdown.Name = "oColFNHSysBuyIdCodeBreakdown"
        Me.oColFNHSysBuyIdCodeBreakdown.OptionsColumn.AllowEdit = False
        Me.oColFNHSysBuyIdCodeBreakdown.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFNHSysBuyIdCodeBreakdown.OptionsColumn.ReadOnly = True
        '
        'oColFNHSysBuyIdNameBreakdown
        '
        Me.oColFNHSysBuyIdNameBreakdown.Caption = "FNHSysBuyId_None"
        Me.oColFNHSysBuyIdNameBreakdown.FieldName = "FNHSysBuyId_None"
        Me.oColFNHSysBuyIdNameBreakdown.Name = "oColFNHSysBuyIdNameBreakdown"
        Me.oColFNHSysBuyIdNameBreakdown.OptionsColumn.AllowEdit = False
        Me.oColFNHSysBuyIdNameBreakdown.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.oColFNHSysBuyIdNameBreakdown.OptionsColumn.ReadOnly = True
        '
        'cFNHSysStyleId
        '
        Me.cFNHSysStyleId.Caption = "FNHSysStyleId"
        Me.cFNHSysStyleId.FieldName = "FNHSysStyleId"
        Me.cFNHSysStyleId.Name = "cFNHSysStyleId"
        Me.cFNHSysStyleId.OptionsColumn.AllowEdit = False
        Me.cFNHSysStyleId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.cFNHSysStyleId.OptionsColumn.ReadOnly = True
        '
        'FTSubOrderNoRef
        '
        Me.FTSubOrderNoRef.Caption = "FTSubOrderNoRef"
        Me.FTSubOrderNoRef.FieldName = "FTSubOrderNoRef"
        Me.FTSubOrderNoRef.Name = "FTSubOrderNoRef"
        Me.FTSubOrderNoRef.OptionsColumn.AllowEdit = False
        Me.FTSubOrderNoRef.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.FTSubOrderNoRef.OptionsColumn.ReadOnly = True
        '
        'CFNHSysSeasonId
        '
        Me.CFNHSysSeasonId.Caption = "FNHSysSeasonId"
        Me.CFNHSysSeasonId.FieldName = "FNHSysSeasonId"
        Me.CFNHSysSeasonId.Name = "CFNHSysSeasonId"
        Me.CFNHSysSeasonId.OptionsColumn.AllowEdit = False
        Me.CFNHSysSeasonId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[False]
        Me.CFNHSysSeasonId.OptionsColumn.ReadOnly = True
        '
        'wCustomerOrderValueTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1023, 634)
        Me.Controls.Add(Me.otcFactoryOrderNo)
        Me.Controls.Add(Me.ogbCmp)
        Me.Name = "wCustomerOrderValueTracking"
        Me.Text = "Customer Order Price Tracking"
        CType(Me.ogbCmp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbCmp.ResumeLayout(False)
        CType(Me.ogbmainprocbutton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ogbmainprocbutton.ResumeLayout(False)
        CType(Me.FNHSysBuyId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysBuyId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysSeasonId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysSeasonId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCustId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCustId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysStyleId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysPOID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId_None.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FNHSysCmpId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.otcFactoryOrderNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.otcFactoryOrderNo.ResumeLayout(False)
        Me.otpSubOrderNoBreakdown.ResumeLayout(False)
        CType(Me.ogdAdjustSubFONoBreakdown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvAdjustSubFONoBreakdown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReposFTStateHold, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEditReal0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCalcEditReal2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryFNGarmentQtyTest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ogbCmp As DevExpress.XtraEditors.GroupControl
    Friend WithEvents FNHSysCmpId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCmpId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysCmpId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents otcFactoryOrderNo As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents otpSubOrderNoBreakdown As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents ogdAdjustSubFONoBreakdown As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvAdjustSubFONoBreakdown As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents oColFTOrderNo_Breakdown As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFTSubOrderNo_Breakdown As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysMatColorId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMatColorCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMatColorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysMatSizeId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMatSizeCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FTMatSizeName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNQunatity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNPrice As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNExtraQty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNQuantityExtra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNGrandQuantity As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNAmntExtra As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNGrandAmnt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNGarmentQtyTest As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCalcEditReal0 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents RepositoryItemCalcEditReal2 As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents ogbmainprocbutton As DevExpress.XtraEditors.PanelControl
    Friend WithEvents ocmRefresh As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmclearclsr As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ocmExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents oColBreakdownFTStyleCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColBreakdownFTBuyCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNHSysBuyIdBreakdown As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNHSysBuyIdCodeBreakdown As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents oColFNHSysBuyIdNameBreakdown As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryFNGarmentQtyTest As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
    Friend WithEvents FTCurCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cFNHSysStyleId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPriceOrg As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNPriceDiff As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents FNHSysBuyId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysBuyId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysBuyId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysSeasonId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysSeasonId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysSeasonId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysCustId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysCustId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysCustId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FNHSysStyleId_None As DevExpress.XtraEditors.TextEdit
    Friend WithEvents FNHSysStyleId_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysStyleId As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTPORef_lbl As DevExpress.XtraEditors.LabelControl
    Friend WithEvents FNHSysPOID As DevExpress.XtraEditors.ButtonEdit
    Friend WithEvents FTSubOrderNoRef As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTCustomerPO As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTNikePOLineItem As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTStateHold As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ReposFTStateHold As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents FTSeasonCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNHSysSeasonId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNOperateFeeAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFNNetFOB As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CFTOrderSetType As DevExpress.XtraGrid.Columns.GridColumn
End Class
