﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class wDirectorApproved
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
        Me.components = New System.ComponentModel.Container()
        Me.sBtnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.sBtnReject = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ogDirectorApproved = New DevExpress.XtraGrid.GridControl()
        Me.ogvDirectorApproved = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.ColFTStateApproved = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.ColFTPurchaseBy = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTSuperVisorName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTPurchaseNo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFDPurchaseDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTPurchaseState = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTCmpRunCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTCmpRunName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTSuplCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTSuplName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFDDeliveryDate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTCrTermCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTCrTermDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNCreditDay = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTTermOfPMCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTTermOfPMName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTDeliveryCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTDeliveryDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNExchangeRate = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTContactPerson = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNDisCountAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNSurcharge = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNVatAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNPOGrandAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTRemark = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNDisCountPer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNPONetAmt = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFNVatPer = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTTeamGrpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTTeamGrpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTPurGrpCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ColFTPurGrpName = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CheckEdit1 = New DevExpress.XtraEditors.CheckEdit()
        Me.sBtnExit = New DevExpress.XtraEditors.SimpleButton()
        Me.otmchkpo = New System.Windows.Forms.Timer(Me.components)
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ogDirectorApproved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvDirectorApproved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CheckEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sBtnSave
        '
        Me.sBtnSave.Location = New System.Drawing.Point(36, 15)
        Me.sBtnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.sBtnSave.Name = "sBtnSave"
        Me.sBtnSave.Size = New System.Drawing.Size(111, 28)
        Me.sBtnSave.TabIndex = 0
        Me.sBtnSave.Text = "Save"
        '
        'sBtnReject
        '
        Me.sBtnReject.Location = New System.Drawing.Point(188, 15)
        Me.sBtnReject.Margin = New System.Windows.Forms.Padding(4)
        Me.sBtnReject.Name = "sBtnReject"
        Me.sBtnReject.Size = New System.Drawing.Size(111, 28)
        Me.sBtnReject.TabIndex = 1
        Me.sBtnReject.Text = "Reject"
        '
        'GroupControl1
        '
        Me.GroupControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl1.AppearanceCaption.Options.UseTextOptions = True
        Me.GroupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GroupControl1.Controls.Add(Me.ogDirectorApproved)
        Me.GroupControl1.Controls.Add(Me.CheckEdit1)
        Me.GroupControl1.Location = New System.Drawing.Point(8, 59)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(1607, 391)
        Me.GroupControl1.TabIndex = 2
        Me.GroupControl1.Text = "Approved  Purchase"
        '
        'ogDirectorApproved
        '
        Me.ogDirectorApproved.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(4)
        Me.ogDirectorApproved.Location = New System.Drawing.Point(7, 31)
        Me.ogDirectorApproved.MainView = Me.ogvDirectorApproved
        Me.ogDirectorApproved.Margin = New System.Windows.Forms.Padding(4)
        Me.ogDirectorApproved.Name = "ogDirectorApproved"
        Me.ogDirectorApproved.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemCheckEdit1})
        Me.ogDirectorApproved.Size = New System.Drawing.Size(1595, 356)
        Me.ogDirectorApproved.TabIndex = 1
        Me.ogDirectorApproved.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvDirectorApproved})
        '
        'ogvDirectorApproved
        '
        Me.ogvDirectorApproved.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.ColFTStateApproved, Me.ColFTPurchaseBy, Me.ColFTSuperVisorName, Me.ColFTPurchaseNo, Me.ColFDPurchaseDate, Me.ColFTPurchaseState, Me.ColFTCmpRunCode, Me.ColFTCmpRunName, Me.ColFTSuplCode, Me.ColFTSuplName, Me.ColFDDeliveryDate, Me.ColFTCrTermCode, Me.ColFTCrTermDesc, Me.ColFNCreditDay, Me.ColFTTermOfPMCode, Me.ColFTTermOfPMName, Me.ColFTDeliveryCode, Me.ColFTDeliveryDesc, Me.ColFNExchangeRate, Me.ColFTContactPerson, Me.ColFNDisCountAmt, Me.ColFNSurcharge, Me.ColFNVatAmt, Me.ColFNPOGrandAmt, Me.ColFTRemark, Me.ColFNDisCountPer, Me.ColFNPONetAmt, Me.ColFNVatPer, Me.ColFTTeamGrpCode, Me.ColFTTeamGrpName, Me.ColFTPurGrpCode, Me.ColFTPurGrpName})
        Me.ogvDirectorApproved.GridControl = Me.ogDirectorApproved
        Me.ogvDirectorApproved.Name = "ogvDirectorApproved"
        Me.ogvDirectorApproved.OptionsView.ColumnAutoWidth = False
        Me.ogvDirectorApproved.OptionsView.ShowGroupPanel = False
        '
        'ColFTStateApproved
        '
        Me.ColFTStateApproved.Caption = "FTStateApproved"
        Me.ColFTStateApproved.ColumnEdit = Me.RepositoryItemCheckEdit1
        Me.ColFTStateApproved.FieldName = "FTStateApproved"
        Me.ColFTStateApproved.Name = "ColFTStateApproved"
        Me.ColFTStateApproved.Visible = True
        Me.ColFTStateApproved.VisibleIndex = 0
        Me.ColFTStateApproved.Width = 50
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'ColFTPurchaseBy
        '
        Me.ColFTPurchaseBy.Caption = "FTPurchaseBy"
        Me.ColFTPurchaseBy.FieldName = "FTPurchaseBy"
        Me.ColFTPurchaseBy.Name = "ColFTPurchaseBy"
        Me.ColFTPurchaseBy.OptionsColumn.AllowEdit = False
        Me.ColFTPurchaseBy.OptionsColumn.ReadOnly = True
        Me.ColFTPurchaseBy.Visible = True
        Me.ColFTPurchaseBy.VisibleIndex = 1
        '
        'ColFTSuperVisorName
        '
        Me.ColFTSuperVisorName.Caption = "FTSuperVisorName"
        Me.ColFTSuperVisorName.FieldName = "FTSuperVisorName"
        Me.ColFTSuperVisorName.Name = "ColFTSuperVisorName"
        Me.ColFTSuperVisorName.OptionsColumn.AllowEdit = False
        Me.ColFTSuperVisorName.OptionsColumn.ReadOnly = True
        Me.ColFTSuperVisorName.Visible = True
        Me.ColFTSuperVisorName.VisibleIndex = 2
        '
        'ColFTPurchaseNo
        '
        Me.ColFTPurchaseNo.Caption = "FTPurchaseNo"
        Me.ColFTPurchaseNo.FieldName = "FTPurchaseNo"
        Me.ColFTPurchaseNo.Name = "ColFTPurchaseNo"
        Me.ColFTPurchaseNo.OptionsColumn.AllowEdit = False
        Me.ColFTPurchaseNo.OptionsColumn.ReadOnly = True
        Me.ColFTPurchaseNo.Visible = True
        Me.ColFTPurchaseNo.VisibleIndex = 3
        Me.ColFTPurchaseNo.Width = 100
        '
        'ColFDPurchaseDate
        '
        Me.ColFDPurchaseDate.Caption = "FDPurchaseDate"
        Me.ColFDPurchaseDate.FieldName = "FDPurchaseDate"
        Me.ColFDPurchaseDate.Name = "ColFDPurchaseDate"
        Me.ColFDPurchaseDate.OptionsColumn.AllowEdit = False
        Me.ColFDPurchaseDate.OptionsColumn.ReadOnly = True
        Me.ColFDPurchaseDate.Visible = True
        Me.ColFDPurchaseDate.VisibleIndex = 4
        Me.ColFDPurchaseDate.Width = 70
        '
        'ColFTPurchaseState
        '
        Me.ColFTPurchaseState.Caption = "FTPurchaseState"
        Me.ColFTPurchaseState.FieldName = "FTPurchaseState"
        Me.ColFTPurchaseState.Name = "ColFTPurchaseState"
        Me.ColFTPurchaseState.OptionsColumn.AllowEdit = False
        Me.ColFTPurchaseState.OptionsColumn.ReadOnly = True
        '
        'ColFTCmpRunCode
        '
        Me.ColFTCmpRunCode.Caption = "FTCmpRunCode"
        Me.ColFTCmpRunCode.FieldName = "FTCmpRunCode"
        Me.ColFTCmpRunCode.Name = "ColFTCmpRunCode"
        Me.ColFTCmpRunCode.OptionsColumn.AllowEdit = False
        Me.ColFTCmpRunCode.OptionsColumn.ReadOnly = True
        '
        'ColFTCmpRunName
        '
        Me.ColFTCmpRunName.Caption = "FTCmpRunName"
        Me.ColFTCmpRunName.FieldName = "FTCmpRunName"
        Me.ColFTCmpRunName.Name = "ColFTCmpRunName"
        Me.ColFTCmpRunName.OptionsColumn.AllowEdit = False
        Me.ColFTCmpRunName.OptionsColumn.ReadOnly = True
        '
        'ColFTSuplCode
        '
        Me.ColFTSuplCode.Caption = "FTSuplCode"
        Me.ColFTSuplCode.FieldName = "FTSuplCode"
        Me.ColFTSuplCode.Name = "ColFTSuplCode"
        Me.ColFTSuplCode.OptionsColumn.AllowEdit = False
        Me.ColFTSuplCode.OptionsColumn.ReadOnly = True
        Me.ColFTSuplCode.Visible = True
        Me.ColFTSuplCode.VisibleIndex = 5
        '
        'ColFTSuplName
        '
        Me.ColFTSuplName.Caption = "FTSuplName"
        Me.ColFTSuplName.FieldName = "FTSuplName"
        Me.ColFTSuplName.Name = "ColFTSuplName"
        Me.ColFTSuplName.OptionsColumn.AllowEdit = False
        Me.ColFTSuplName.OptionsColumn.ReadOnly = True
        Me.ColFTSuplName.Visible = True
        Me.ColFTSuplName.VisibleIndex = 6
        Me.ColFTSuplName.Width = 150
        '
        'ColFDDeliveryDate
        '
        Me.ColFDDeliveryDate.Caption = "FDDeliveryDate"
        Me.ColFDDeliveryDate.FieldName = "FDDeliveryDate"
        Me.ColFDDeliveryDate.Name = "ColFDDeliveryDate"
        Me.ColFDDeliveryDate.OptionsColumn.AllowEdit = False
        Me.ColFDDeliveryDate.OptionsColumn.ReadOnly = True
        '
        'ColFTCrTermCode
        '
        Me.ColFTCrTermCode.Caption = "FTCrTermCode"
        Me.ColFTCrTermCode.FieldName = "FTCrTermCode"
        Me.ColFTCrTermCode.Name = "ColFTCrTermCode"
        Me.ColFTCrTermCode.OptionsColumn.AllowEdit = False
        Me.ColFTCrTermCode.OptionsColumn.ReadOnly = True
        '
        'ColFTCrTermDesc
        '
        Me.ColFTCrTermDesc.Caption = "FTCrTermDesc"
        Me.ColFTCrTermDesc.FieldName = "FTCrTermDesc"
        Me.ColFTCrTermDesc.Name = "ColFTCrTermDesc"
        Me.ColFTCrTermDesc.OptionsColumn.AllowEdit = False
        Me.ColFTCrTermDesc.OptionsColumn.ReadOnly = True
        '
        'ColFNCreditDay
        '
        Me.ColFNCreditDay.Caption = "FNCreditDay"
        Me.ColFNCreditDay.FieldName = "FNCreditDay"
        Me.ColFNCreditDay.Name = "ColFNCreditDay"
        Me.ColFNCreditDay.OptionsColumn.AllowEdit = False
        Me.ColFNCreditDay.OptionsColumn.ReadOnly = True
        '
        'ColFTTermOfPMCode
        '
        Me.ColFTTermOfPMCode.Caption = "FTTermOfPMCode"
        Me.ColFTTermOfPMCode.FieldName = "FTTermOfPMCode"
        Me.ColFTTermOfPMCode.Name = "ColFTTermOfPMCode"
        Me.ColFTTermOfPMCode.OptionsColumn.AllowEdit = False
        Me.ColFTTermOfPMCode.OptionsColumn.ReadOnly = True
        '
        'ColFTTermOfPMName
        '
        Me.ColFTTermOfPMName.Caption = "FTTermOfPMName"
        Me.ColFTTermOfPMName.FieldName = "FTTermOfPMName"
        Me.ColFTTermOfPMName.Name = "ColFTTermOfPMName"
        Me.ColFTTermOfPMName.OptionsColumn.AllowEdit = False
        Me.ColFTTermOfPMName.OptionsColumn.ReadOnly = True
        '
        'ColFTDeliveryCode
        '
        Me.ColFTDeliveryCode.Caption = "FTDeliveryCode"
        Me.ColFTDeliveryCode.FieldName = "FTDeliveryCode"
        Me.ColFTDeliveryCode.Name = "ColFTDeliveryCode"
        Me.ColFTDeliveryCode.OptionsColumn.AllowEdit = False
        Me.ColFTDeliveryCode.OptionsColumn.ReadOnly = True
        Me.ColFTDeliveryCode.Visible = True
        Me.ColFTDeliveryCode.VisibleIndex = 7
        '
        'ColFTDeliveryDesc
        '
        Me.ColFTDeliveryDesc.Caption = "FTDeliveryDesc"
        Me.ColFTDeliveryDesc.FieldName = "FTDeliveryDesc"
        Me.ColFTDeliveryDesc.Name = "ColFTDeliveryDesc"
        Me.ColFTDeliveryDesc.OptionsColumn.AllowEdit = False
        Me.ColFTDeliveryDesc.OptionsColumn.ReadOnly = True
        Me.ColFTDeliveryDesc.Visible = True
        Me.ColFTDeliveryDesc.VisibleIndex = 8
        '
        'ColFNExchangeRate
        '
        Me.ColFNExchangeRate.Caption = "FNExchangeRate"
        Me.ColFNExchangeRate.DisplayFormat.FormatString = "{0:n4}"
        Me.ColFNExchangeRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColFNExchangeRate.FieldName = "FNExchangeRate"
        Me.ColFNExchangeRate.Name = "ColFNExchangeRate"
        Me.ColFNExchangeRate.OptionsColumn.AllowEdit = False
        Me.ColFNExchangeRate.OptionsColumn.ReadOnly = True
        Me.ColFNExchangeRate.Visible = True
        Me.ColFNExchangeRate.VisibleIndex = 9
        '
        'ColFTContactPerson
        '
        Me.ColFTContactPerson.Caption = "FTContactPerson"
        Me.ColFTContactPerson.FieldName = "FTContactPerson"
        Me.ColFTContactPerson.Name = "ColFTContactPerson"
        Me.ColFTContactPerson.OptionsColumn.AllowEdit = False
        Me.ColFTContactPerson.OptionsColumn.ReadOnly = True
        '
        'ColFNDisCountAmt
        '
        Me.ColFNDisCountAmt.Caption = "FNDisCountAmt"
        Me.ColFNDisCountAmt.DisplayFormat.FormatString = "{0:n4}"
        Me.ColFNDisCountAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColFNDisCountAmt.FieldName = "FNDisCountAmt"
        Me.ColFNDisCountAmt.Name = "ColFNDisCountAmt"
        Me.ColFNDisCountAmt.OptionsColumn.AllowEdit = False
        Me.ColFNDisCountAmt.OptionsColumn.ReadOnly = True
        Me.ColFNDisCountAmt.Visible = True
        Me.ColFNDisCountAmt.VisibleIndex = 10
        '
        'ColFNSurcharge
        '
        Me.ColFNSurcharge.Caption = "FNSurcharge"
        Me.ColFNSurcharge.DisplayFormat.FormatString = "{0:n2}"
        Me.ColFNSurcharge.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColFNSurcharge.FieldName = "FNSurcharge"
        Me.ColFNSurcharge.Name = "ColFNSurcharge"
        Me.ColFNSurcharge.OptionsColumn.AllowEdit = False
        Me.ColFNSurcharge.OptionsColumn.ReadOnly = True
        Me.ColFNSurcharge.Visible = True
        Me.ColFNSurcharge.VisibleIndex = 11
        '
        'ColFNVatAmt
        '
        Me.ColFNVatAmt.Caption = "FNVatAmt"
        Me.ColFNVatAmt.DisplayFormat.FormatString = "{0:n4}"
        Me.ColFNVatAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColFNVatAmt.FieldName = "FNVatAmt"
        Me.ColFNVatAmt.Name = "ColFNVatAmt"
        Me.ColFNVatAmt.OptionsColumn.AllowEdit = False
        Me.ColFNVatAmt.OptionsColumn.ReadOnly = True
        Me.ColFNVatAmt.Visible = True
        Me.ColFNVatAmt.VisibleIndex = 12
        '
        'ColFNPOGrandAmt
        '
        Me.ColFNPOGrandAmt.Caption = "FNPOGrandAmt"
        Me.ColFNPOGrandAmt.DisplayFormat.FormatString = "{0:n4}"
        Me.ColFNPOGrandAmt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColFNPOGrandAmt.FieldName = "FNPOGrandAmt"
        Me.ColFNPOGrandAmt.Name = "ColFNPOGrandAmt"
        Me.ColFNPOGrandAmt.OptionsColumn.AllowEdit = False
        Me.ColFNPOGrandAmt.OptionsColumn.ReadOnly = True
        Me.ColFNPOGrandAmt.Visible = True
        Me.ColFNPOGrandAmt.VisibleIndex = 13
        '
        'ColFTRemark
        '
        Me.ColFTRemark.Caption = "FTRemark"
        Me.ColFTRemark.FieldName = "FTRemark"
        Me.ColFTRemark.Name = "ColFTRemark"
        Me.ColFTRemark.OptionsColumn.AllowEdit = False
        Me.ColFTRemark.OptionsColumn.ReadOnly = True
        Me.ColFTRemark.Visible = True
        Me.ColFTRemark.VisibleIndex = 14
        '
        'ColFNDisCountPer
        '
        Me.ColFNDisCountPer.Caption = "FNDisCountPer"
        Me.ColFNDisCountPer.FieldName = "FNDisCountPer"
        Me.ColFNDisCountPer.Name = "ColFNDisCountPer"
        Me.ColFNDisCountPer.OptionsColumn.AllowEdit = False
        Me.ColFNDisCountPer.OptionsColumn.ReadOnly = True
        '
        'ColFNPONetAmt
        '
        Me.ColFNPONetAmt.Caption = "FNPONetAmt"
        Me.ColFNPONetAmt.FieldName = "FNPONetAmt"
        Me.ColFNPONetAmt.Name = "ColFNPONetAmt"
        Me.ColFNPONetAmt.OptionsColumn.AllowEdit = False
        Me.ColFNPONetAmt.OptionsColumn.ReadOnly = True
        '
        'ColFNVatPer
        '
        Me.ColFNVatPer.Caption = "FNVatPer"
        Me.ColFNVatPer.DisplayFormat.FormatString = "{0:n2}"
        Me.ColFNVatPer.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.ColFNVatPer.FieldName = "FNVatPer"
        Me.ColFNVatPer.Name = "ColFNVatPer"
        Me.ColFNVatPer.OptionsColumn.AllowEdit = False
        Me.ColFNVatPer.OptionsColumn.ReadOnly = True
        '
        'ColFTTeamGrpCode
        '
        Me.ColFTTeamGrpCode.Caption = "FTTeamGrpCode"
        Me.ColFTTeamGrpCode.FieldName = "FTTeamGrpCode"
        Me.ColFTTeamGrpCode.Name = "ColFTTeamGrpCode"
        Me.ColFTTeamGrpCode.OptionsColumn.AllowEdit = False
        Me.ColFTTeamGrpCode.OptionsColumn.ReadOnly = True
        '
        'ColFTTeamGrpName
        '
        Me.ColFTTeamGrpName.Caption = "FTTeamGrpName"
        Me.ColFTTeamGrpName.FieldName = "FTTeamGrpName"
        Me.ColFTTeamGrpName.Name = "ColFTTeamGrpName"
        Me.ColFTTeamGrpName.OptionsColumn.AllowEdit = False
        Me.ColFTTeamGrpName.OptionsColumn.ReadOnly = True
        '
        'ColFTPurGrpCode
        '
        Me.ColFTPurGrpCode.Caption = "FTPurGrpCode"
        Me.ColFTPurGrpCode.FieldName = "FTPurGrpCode"
        Me.ColFTPurGrpCode.Name = "ColFTPurGrpCode"
        Me.ColFTPurGrpCode.OptionsColumn.AllowEdit = False
        Me.ColFTPurGrpCode.OptionsColumn.ReadOnly = True
        '
        'ColFTPurGrpName
        '
        Me.ColFTPurGrpName.Caption = "FTPurGrpName"
        Me.ColFTPurGrpName.FieldName = "FTPurGrpName"
        Me.ColFTPurGrpName.Name = "ColFTPurGrpName"
        Me.ColFTPurGrpName.OptionsColumn.AllowEdit = False
        Me.ColFTPurGrpName.OptionsColumn.ReadOnly = True
        '
        'CheckEdit1
        '
        Me.CheckEdit1.Location = New System.Drawing.Point(28, 0)
        Me.CheckEdit1.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckEdit1.Name = "CheckEdit1"
        Me.CheckEdit1.Properties.Caption = "Select All"
        Me.CheckEdit1.Size = New System.Drawing.Size(160, 20)
        Me.CheckEdit1.TabIndex = 0
        '
        'sBtnExit
        '
        Me.sBtnExit.Location = New System.Drawing.Point(1504, 15)
        Me.sBtnExit.Margin = New System.Windows.Forms.Padding(4)
        Me.sBtnExit.Name = "sBtnExit"
        Me.sBtnExit.Size = New System.Drawing.Size(100, 28)
        Me.sBtnExit.TabIndex = 3
        Me.sBtnExit.Text = "Exit"
        '
        'otmchkpo
        '
        Me.otmchkpo.Interval = 60000
        '
        'wDirectorApproved
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1620, 455)
        Me.Controls.Add(Me.sBtnExit)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.sBtnReject)
        Me.Controls.Add(Me.sBtnSave)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "wDirectorApproved"
        Me.Text = "wDirectorApproved"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ogDirectorApproved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvDirectorApproved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CheckEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sBtnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents sBtnReject As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents CheckEdit1 As DevExpress.XtraEditors.CheckEdit
    Friend WithEvents ogDirectorApproved As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvDirectorApproved As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents sBtnExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ColFTStateApproved As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents ColFTPurchaseBy As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTSuperVisorName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTPurchaseNo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFDPurchaseDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTPurchaseState As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTCmpRunCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTCmpRunName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTSuplCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTSuplName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFDDeliveryDate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTCrTermCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTCrTermDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNCreditDay As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTTermOfPMCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTTermOfPMName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTDeliveryCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTDeliveryDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNExchangeRate As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTContactPerson As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNDisCountAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNSurcharge As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNVatAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNPOGrandAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTRemark As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNDisCountPer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNPONetAmt As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFNVatPer As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTTeamGrpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTTeamGrpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTPurGrpCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ColFTPurGrpName As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents otmchkpo As System.Windows.Forms.Timer
End Class
