<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UBompage
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Me.ogdbom = New DevExpress.XtraGrid.GridControl()
        Me.ogvbomitem = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CbomRowNbr = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Cis = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Cit = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CitemNbr = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.description = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CvendNm = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CvendLo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CvendCd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CvendId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Cuse = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CcomponentOrd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CitemType1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cpcxSuppliedMatlId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CbomComponentId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CbomItmId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CbomItmSetupTimestamp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CbomItmUpdateTimestamp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.ogvbom = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CstyleNbr = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CseasonCd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CseasonYr = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CstyleNm = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CcycleYear = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CbomStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.bomUpdateTimestamp = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Cdeveloper = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.developerUserId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CbomUpdateUserid = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CprimDevRegAbrv = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cprimDevRegCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cdesignRegAbrv = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cdesignRegCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cfactoryCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.parentFcty = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CsilhouetteCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.csilhouetteDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CmscIdentifier = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmscCode = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CmscLevel1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmscLevel2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cmscLevel3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cprmryAbrv = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.prmryColorCd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cproductId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cplugColorwayCd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CprmryColorId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CprmryDesc = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CcolorwayId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CcolorwayCd = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.bomId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cid = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.cresourceType = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.CobjectId = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.RepositoryItemCheckEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        CType(Me.ogdbom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvbomitem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ogvbom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ogdbom
        '
        Me.ogdbom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ogdbom.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        GridLevelNode1.LevelTemplate = Me.ogvbomitem
        GridLevelNode1.RelationName = "Level1"
        Me.ogdbom.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.ogdbom.Location = New System.Drawing.Point(0, 0)
        Me.ogdbom.MainView = Me.ogvbom
        Me.ogdbom.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.ogdbom.Name = "ogdbom"
        Me.ogdbom.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit1, Me.RepositoryItemCheckEdit1})
        Me.ogdbom.Size = New System.Drawing.Size(1347, 723)
        Me.ogdbom.TabIndex = 18
        Me.ogdbom.TabStop = False
        Me.ogdbom.Tag = "2|"
        Me.ogdbom.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.ogvbomitem, Me.ogvbom})
        '
        'ogvbomitem
        '
        Me.ogvbomitem.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CbomRowNbr, Me.Cis, Me.Cit, Me.CitemNbr, Me.description, Me.CvendNm, Me.CvendLo, Me.CvendCd, Me.CvendId, Me.Cuse, Me.CcomponentOrd, Me.CitemType1, Me.cpcxSuppliedMatlId, Me.CbomComponentId, Me.CbomItmId, Me.CbomItmSetupTimestamp, Me.CbomItmUpdateTimestamp})
        Me.ogvbomitem.GridControl = Me.ogdbom
        Me.ogvbomitem.Name = "ogvbomitem"
        '
        'CbomRowNbr
        '
        Me.CbomRowNbr.Caption = "NO"
        Me.CbomRowNbr.FieldName = "bomRowNbr"
        Me.CbomRowNbr.Name = "CbomRowNbr"
        Me.CbomRowNbr.OptionsColumn.AllowEdit = False
        Me.CbomRowNbr.OptionsColumn.ReadOnly = True
        Me.CbomRowNbr.Visible = True
        Me.CbomRowNbr.VisibleIndex = 0
        '
        'Cis
        '
        Me.Cis.Caption = "is"
        Me.Cis.FieldName = "is"
        Me.Cis.Name = "Cis"
        Me.Cis.OptionsColumn.AllowEdit = False
        Me.Cis.OptionsColumn.ReadOnly = True
        Me.Cis.Visible = True
        Me.Cis.VisibleIndex = 1
        '
        'Cit
        '
        Me.Cit.Caption = "it"
        Me.Cit.FieldName = "it"
        Me.Cit.Name = "Cit"
        Me.Cit.OptionsColumn.AllowEdit = False
        Me.Cit.OptionsColumn.ReadOnly = True
        Me.Cit.Visible = True
        Me.Cit.VisibleIndex = 2
        '
        'CitemNbr
        '
        Me.CitemNbr.Caption = "Item"
        Me.CitemNbr.FieldName = "itemNbr"
        Me.CitemNbr.Name = "CitemNbr"
        Me.CitemNbr.OptionsColumn.AllowEdit = False
        Me.CitemNbr.OptionsColumn.ReadOnly = True
        Me.CitemNbr.Visible = True
        Me.CitemNbr.VisibleIndex = 3
        '
        'description
        '
        Me.description.Caption = "Description"
        Me.description.FieldName = "description"
        Me.description.Name = "description"
        Me.description.OptionsColumn.AllowEdit = False
        Me.description.OptionsColumn.ReadOnly = True
        Me.description.Visible = True
        Me.description.VisibleIndex = 4
        '
        'CvendNm
        '
        Me.CvendNm.Caption = "Vender"
        Me.CvendNm.FieldName = "vendNm"
        Me.CvendNm.Name = "CvendNm"
        Me.CvendNm.OptionsColumn.AllowEdit = False
        Me.CvendNm.OptionsColumn.ReadOnly = True
        Me.CvendNm.Visible = True
        Me.CvendNm.VisibleIndex = 5
        '
        'CvendLo
        '
        Me.CvendLo.Caption = "vendLo"
        Me.CvendLo.FieldName = "vendLo"
        Me.CvendLo.Name = "CvendLo"
        Me.CvendLo.OptionsColumn.AllowEdit = False
        Me.CvendLo.OptionsColumn.ReadOnly = True
        Me.CvendLo.Visible = True
        Me.CvendLo.VisibleIndex = 6
        '
        'CvendCd
        '
        Me.CvendCd.Caption = "vendCd"
        Me.CvendCd.FieldName = "vendCd"
        Me.CvendCd.Name = "CvendCd"
        Me.CvendCd.OptionsColumn.AllowEdit = False
        Me.CvendCd.OptionsColumn.ReadOnly = True
        Me.CvendCd.Visible = True
        Me.CvendCd.VisibleIndex = 7
        '
        'CvendId
        '
        Me.CvendId.Caption = "vendId"
        Me.CvendId.FieldName = "vendId"
        Me.CvendId.Name = "CvendId"
        Me.CvendId.OptionsColumn.AllowEdit = False
        Me.CvendId.OptionsColumn.ReadOnly = True
        Me.CvendId.Visible = True
        Me.CvendId.VisibleIndex = 8
        '
        'Cuse
        '
        Me.Cuse.Caption = "use"
        Me.Cuse.FieldName = "use"
        Me.Cuse.Name = "Cuse"
        Me.Cuse.OptionsColumn.AllowEdit = False
        Me.Cuse.OptionsColumn.ReadOnly = True
        Me.Cuse.Visible = True
        Me.Cuse.VisibleIndex = 9
        '
        'CcomponentOrd
        '
        Me.CcomponentOrd.Caption = "componentOrd"
        Me.CcomponentOrd.FieldName = "componentOrd"
        Me.CcomponentOrd.Name = "CcomponentOrd"
        Me.CcomponentOrd.OptionsColumn.AllowEdit = False
        Me.CcomponentOrd.OptionsColumn.ReadOnly = True
        Me.CcomponentOrd.Visible = True
        Me.CcomponentOrd.VisibleIndex = 10
        '
        'CitemType1
        '
        Me.CitemType1.Caption = "itemType1"
        Me.CitemType1.FieldName = "itemType1"
        Me.CitemType1.Name = "CitemType1"
        Me.CitemType1.OptionsColumn.AllowEdit = False
        Me.CitemType1.OptionsColumn.ReadOnly = True
        Me.CitemType1.Visible = True
        Me.CitemType1.VisibleIndex = 11
        '
        'cpcxSuppliedMatlId
        '
        Me.cpcxSuppliedMatlId.Caption = "pcxSuppliedMatlId"
        Me.cpcxSuppliedMatlId.FieldName = "pcxSuppliedMatlId"
        Me.cpcxSuppliedMatlId.Name = "cpcxSuppliedMatlId"
        Me.cpcxSuppliedMatlId.OptionsColumn.AllowEdit = False
        Me.cpcxSuppliedMatlId.OptionsColumn.ReadOnly = True
        Me.cpcxSuppliedMatlId.Visible = True
        Me.cpcxSuppliedMatlId.VisibleIndex = 12
        '
        'CbomComponentId
        '
        Me.CbomComponentId.Caption = "bomComponentId"
        Me.CbomComponentId.FieldName = "bomComponentId"
        Me.CbomComponentId.Name = "CbomComponentId"
        Me.CbomComponentId.OptionsColumn.AllowEdit = False
        Me.CbomComponentId.OptionsColumn.ReadOnly = True
        Me.CbomComponentId.Visible = True
        Me.CbomComponentId.VisibleIndex = 13
        '
        'CbomItmId
        '
        Me.CbomItmId.Caption = "bomItmId"
        Me.CbomItmId.FieldName = "bomItmId"
        Me.CbomItmId.Name = "CbomItmId"
        Me.CbomItmId.OptionsColumn.AllowEdit = False
        Me.CbomItmId.OptionsColumn.ReadOnly = True
        Me.CbomItmId.Visible = True
        Me.CbomItmId.VisibleIndex = 14
        '
        'CbomItmSetupTimestamp
        '
        Me.CbomItmSetupTimestamp.Caption = "Setup Times"
        Me.CbomItmSetupTimestamp.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"
        Me.CbomItmSetupTimestamp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.CbomItmSetupTimestamp.FieldName = "bomItmSetupTimestamp"
        Me.CbomItmSetupTimestamp.Name = "CbomItmSetupTimestamp"
        Me.CbomItmSetupTimestamp.OptionsColumn.AllowEdit = False
        Me.CbomItmSetupTimestamp.OptionsColumn.ReadOnly = True
        Me.CbomItmSetupTimestamp.Visible = True
        Me.CbomItmSetupTimestamp.VisibleIndex = 15
        '
        'CbomItmUpdateTimestamp
        '
        Me.CbomItmUpdateTimestamp.Caption = "Update Times"
        Me.CbomItmUpdateTimestamp.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"
        Me.CbomItmUpdateTimestamp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.CbomItmUpdateTimestamp.FieldName = "bomItmUpdateTimestamp"
        Me.CbomItmUpdateTimestamp.Name = "CbomItmUpdateTimestamp"
        Me.CbomItmUpdateTimestamp.OptionsColumn.AllowEdit = False
        Me.CbomItmUpdateTimestamp.OptionsColumn.ReadOnly = True
        Me.CbomItmUpdateTimestamp.Visible = True
        Me.CbomItmUpdateTimestamp.VisibleIndex = 16
        '
        'ogvbom
        '
        Me.ogvbom.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.CstyleNbr, Me.CseasonCd, Me.CseasonYr, Me.CstyleNm, Me.CcycleYear, Me.CbomStatus, Me.bomUpdateTimestamp, Me.Cdeveloper, Me.developerUserId, Me.CbomUpdateUserid, Me.CprimDevRegAbrv, Me.cprimDevRegCode, Me.cdesignRegAbrv, Me.cdesignRegCode, Me.cfactoryCode, Me.parentFcty, Me.CsilhouetteCode, Me.csilhouetteDesc, Me.CmscIdentifier, Me.cmscCode, Me.CmscLevel1, Me.cmscLevel2, Me.cmscLevel3, Me.cprmryAbrv, Me.prmryColorCd, Me.cproductId, Me.cplugColorwayCd, Me.CprmryColorId, Me.CprmryDesc, Me.CcolorwayId, Me.CcolorwayCd, Me.bomId, Me.cid, Me.cresourceType, Me.CobjectId})
        Me.ogvbom.CustomizationFormBounds = New System.Drawing.Rectangle(758, 512, 216, 178)
        Me.ogvbom.GridControl = Me.ogdbom
        Me.ogvbom.Name = "ogvbom"
        Me.ogvbom.OptionsBehavior.AutoExpandAllGroups = True
        Me.ogvbom.OptionsBehavior.AutoPopulateColumns = False
        Me.ogvbom.OptionsBehavior.ReadOnly = True
        Me.ogvbom.OptionsCustomization.AllowQuickHideColumns = False
        Me.ogvbom.OptionsNavigation.EnterMoveNextColumn = True
        Me.ogvbom.OptionsView.ColumnAutoWidth = False
        Me.ogvbom.OptionsView.EnableAppearanceEvenRow = True
        Me.ogvbom.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.ogvbom.OptionsView.ShowAutoFilterRow = True
        Me.ogvbom.OptionsView.ShowGroupPanel = False
        Me.ogvbom.Tag = "2|"
        '
        'CstyleNbr
        '
        Me.CstyleNbr.Caption = "Style No"
        Me.CstyleNbr.FieldName = "styleNbr"
        Me.CstyleNbr.Name = "CstyleNbr"
        Me.CstyleNbr.OptionsColumn.AllowEdit = False
        Me.CstyleNbr.OptionsColumn.ReadOnly = True
        Me.CstyleNbr.Visible = True
        Me.CstyleNbr.VisibleIndex = 0
        '
        'CseasonCd
        '
        Me.CseasonCd.Caption = "Season"
        Me.CseasonCd.FieldName = "seasonCd"
        Me.CseasonCd.Name = "CseasonCd"
        Me.CseasonCd.OptionsColumn.AllowEdit = False
        Me.CseasonCd.OptionsColumn.ReadOnly = True
        Me.CseasonCd.Visible = True
        Me.CseasonCd.VisibleIndex = 1
        '
        'CseasonYr
        '
        Me.CseasonYr.Caption = "Year"
        Me.CseasonYr.FieldName = "seasonYr"
        Me.CseasonYr.Name = "CseasonYr"
        Me.CseasonYr.OptionsColumn.AllowEdit = False
        Me.CseasonYr.OptionsColumn.ReadOnly = True
        Me.CseasonYr.Visible = True
        Me.CseasonYr.VisibleIndex = 2
        '
        'CstyleNm
        '
        Me.CstyleNm.Caption = "Style Name"
        Me.CstyleNm.FieldName = "styleNm"
        Me.CstyleNm.Name = "CstyleNm"
        Me.CstyleNm.OptionsColumn.AllowEdit = False
        Me.CstyleNm.OptionsColumn.ReadOnly = True
        Me.CstyleNm.Visible = True
        Me.CstyleNm.VisibleIndex = 3
        '
        'CcycleYear
        '
        Me.CcycleYear.Caption = "Cycle Year"
        Me.CcycleYear.FieldName = "cycleYear"
        Me.CcycleYear.Name = "CcycleYear"
        Me.CcycleYear.OptionsColumn.AllowEdit = False
        Me.CcycleYear.OptionsColumn.ReadOnly = True
        Me.CcycleYear.Visible = True
        Me.CcycleYear.VisibleIndex = 4
        '
        'CbomStatus
        '
        Me.CbomStatus.Caption = "Bom Status"
        Me.CbomStatus.FieldName = "bomStatus"
        Me.CbomStatus.Name = "CbomStatus"
        Me.CbomStatus.OptionsColumn.AllowEdit = False
        Me.CbomStatus.OptionsColumn.ReadOnly = True
        Me.CbomStatus.Visible = True
        Me.CbomStatus.VisibleIndex = 5
        '
        'bomUpdateTimestamp
        '
        Me.bomUpdateTimestamp.Caption = "Bom Update"
        Me.bomUpdateTimestamp.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss"
        Me.bomUpdateTimestamp.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
        Me.bomUpdateTimestamp.FieldName = "bomUpdateTimestamp"
        Me.bomUpdateTimestamp.Name = "bomUpdateTimestamp"
        Me.bomUpdateTimestamp.OptionsColumn.AllowEdit = False
        Me.bomUpdateTimestamp.OptionsColumn.ReadOnly = True
        Me.bomUpdateTimestamp.Visible = True
        Me.bomUpdateTimestamp.VisibleIndex = 6
        '
        'Cdeveloper
        '
        Me.Cdeveloper.Caption = "Developer"
        Me.Cdeveloper.FieldName = "developer"
        Me.Cdeveloper.Name = "Cdeveloper"
        Me.Cdeveloper.OptionsColumn.AllowEdit = False
        Me.Cdeveloper.OptionsColumn.ReadOnly = True
        Me.Cdeveloper.Visible = True
        Me.Cdeveloper.VisibleIndex = 7
        '
        'developerUserId
        '
        Me.developerUserId.Caption = "developerUserId"
        Me.developerUserId.FieldName = "developerUserId"
        Me.developerUserId.Name = "developerUserId"
        Me.developerUserId.OptionsColumn.AllowEdit = False
        Me.developerUserId.OptionsColumn.ReadOnly = True
        Me.developerUserId.Visible = True
        Me.developerUserId.VisibleIndex = 8
        '
        'CbomUpdateUserid
        '
        Me.CbomUpdateUserid.Caption = "Update User"
        Me.CbomUpdateUserid.FieldName = "bomUpdateUserid"
        Me.CbomUpdateUserid.Name = "CbomUpdateUserid"
        Me.CbomUpdateUserid.OptionsColumn.AllowEdit = False
        Me.CbomUpdateUserid.OptionsColumn.ReadOnly = True
        Me.CbomUpdateUserid.Visible = True
        Me.CbomUpdateUserid.VisibleIndex = 9
        '
        'CprimDevRegAbrv
        '
        Me.CprimDevRegAbrv.Caption = "P. Dev Reg"
        Me.CprimDevRegAbrv.FieldName = "primDevRegAbrv"
        Me.CprimDevRegAbrv.Name = "CprimDevRegAbrv"
        Me.CprimDevRegAbrv.OptionsColumn.AllowEdit = False
        Me.CprimDevRegAbrv.OptionsColumn.ReadOnly = True
        Me.CprimDevRegAbrv.Visible = True
        Me.CprimDevRegAbrv.VisibleIndex = 10
        '
        'cprimDevRegCode
        '
        Me.cprimDevRegCode.Caption = "primDevRegCode"
        Me.cprimDevRegCode.FieldName = "primDevRegCode"
        Me.cprimDevRegCode.Name = "cprimDevRegCode"
        Me.cprimDevRegCode.OptionsColumn.AllowEdit = False
        Me.cprimDevRegCode.OptionsColumn.ReadOnly = True
        Me.cprimDevRegCode.Visible = True
        Me.cprimDevRegCode.VisibleIndex = 11
        '
        'cdesignRegAbrv
        '
        Me.cdesignRegAbrv.Caption = "Design Reg."
        Me.cdesignRegAbrv.FieldName = "designRegAbrv"
        Me.cdesignRegAbrv.Name = "cdesignRegAbrv"
        Me.cdesignRegAbrv.OptionsColumn.AllowEdit = False
        Me.cdesignRegAbrv.OptionsColumn.ReadOnly = True
        Me.cdesignRegAbrv.Visible = True
        Me.cdesignRegAbrv.VisibleIndex = 12
        '
        'cdesignRegCode
        '
        Me.cdesignRegCode.Caption = "designRegCode"
        Me.cdesignRegCode.FieldName = "designRegCode"
        Me.cdesignRegCode.Name = "cdesignRegCode"
        Me.cdesignRegCode.OptionsColumn.AllowEdit = False
        Me.cdesignRegCode.OptionsColumn.ReadOnly = True
        Me.cdesignRegCode.Visible = True
        Me.cdesignRegCode.VisibleIndex = 13
        '
        'cfactoryCode
        '
        Me.cfactoryCode.Caption = "Factory Code"
        Me.cfactoryCode.FieldName = "factoryCode"
        Me.cfactoryCode.Name = "cfactoryCode"
        Me.cfactoryCode.OptionsColumn.AllowEdit = False
        Me.cfactoryCode.OptionsColumn.ReadOnly = True
        Me.cfactoryCode.Visible = True
        Me.cfactoryCode.VisibleIndex = 14
        '
        'parentFcty
        '
        Me.parentFcty.Caption = "parentFcty"
        Me.parentFcty.FieldName = "parentFcty"
        Me.parentFcty.Name = "parentFcty"
        Me.parentFcty.OptionsColumn.AllowEdit = False
        Me.parentFcty.OptionsColumn.ReadOnly = True
        Me.parentFcty.Visible = True
        Me.parentFcty.VisibleIndex = 15
        '
        'CsilhouetteCode
        '
        Me.CsilhouetteCode.Caption = "Silhouette Code"
        Me.CsilhouetteCode.FieldName = "silhouetteCode"
        Me.CsilhouetteCode.Name = "CsilhouetteCode"
        Me.CsilhouetteCode.OptionsColumn.AllowEdit = False
        Me.CsilhouetteCode.OptionsColumn.ReadOnly = True
        Me.CsilhouetteCode.Visible = True
        Me.CsilhouetteCode.VisibleIndex = 16
        '
        'csilhouetteDesc
        '
        Me.csilhouetteDesc.Caption = "Silhouette Name"
        Me.csilhouetteDesc.FieldName = "silhouetteDesc"
        Me.csilhouetteDesc.Name = "csilhouetteDesc"
        Me.csilhouetteDesc.OptionsColumn.AllowEdit = False
        Me.csilhouetteDesc.OptionsColumn.ReadOnly = True
        Me.csilhouetteDesc.Visible = True
        Me.csilhouetteDesc.VisibleIndex = 17
        '
        'CmscIdentifier
        '
        Me.CmscIdentifier.Caption = "MSC Id"
        Me.CmscIdentifier.FieldName = "mscIdentifier"
        Me.CmscIdentifier.Name = "CmscIdentifier"
        Me.CmscIdentifier.OptionsColumn.AllowEdit = False
        Me.CmscIdentifier.OptionsColumn.ReadOnly = True
        Me.CmscIdentifier.Visible = True
        Me.CmscIdentifier.VisibleIndex = 18
        '
        'cmscCode
        '
        Me.cmscCode.Caption = "MSC Code"
        Me.cmscCode.FieldName = "mscCode"
        Me.cmscCode.Name = "cmscCode"
        Me.cmscCode.OptionsColumn.AllowEdit = False
        Me.cmscCode.OptionsColumn.ReadOnly = True
        Me.cmscCode.Visible = True
        Me.cmscCode.VisibleIndex = 19
        '
        'CmscLevel1
        '
        Me.CmscLevel1.Caption = "MSC Level 1"
        Me.CmscLevel1.FieldName = "mscLevel1"
        Me.CmscLevel1.Name = "CmscLevel1"
        Me.CmscLevel1.OptionsColumn.AllowEdit = False
        Me.CmscLevel1.OptionsColumn.ReadOnly = True
        Me.CmscLevel1.Visible = True
        Me.CmscLevel1.VisibleIndex = 20
        '
        'cmscLevel2
        '
        Me.cmscLevel2.Caption = "MSC Level 2"
        Me.cmscLevel2.FieldName = "mscLevel2"
        Me.cmscLevel2.Name = "cmscLevel2"
        Me.cmscLevel2.OptionsColumn.AllowEdit = False
        Me.cmscLevel2.OptionsColumn.ReadOnly = True
        Me.cmscLevel2.Visible = True
        Me.cmscLevel2.VisibleIndex = 21
        '
        'cmscLevel3
        '
        Me.cmscLevel3.Caption = "MSC Level 3"
        Me.cmscLevel3.FieldName = "mscLevel3"
        Me.cmscLevel3.Name = "cmscLevel3"
        Me.cmscLevel3.OptionsColumn.AllowEdit = False
        Me.cmscLevel3.OptionsColumn.ReadOnly = True
        Me.cmscLevel3.Visible = True
        Me.cmscLevel3.VisibleIndex = 22
        '
        'cprmryAbrv
        '
        Me.cprmryAbrv.Caption = "prmryAbrv"
        Me.cprmryAbrv.FieldName = "prmryAbrv"
        Me.cprmryAbrv.Name = "cprmryAbrv"
        Me.cprmryAbrv.OptionsColumn.AllowEdit = False
        Me.cprmryAbrv.OptionsColumn.ReadOnly = True
        Me.cprmryAbrv.Visible = True
        Me.cprmryAbrv.VisibleIndex = 23
        '
        'prmryColorCd
        '
        Me.prmryColorCd.Caption = "prmryColorCd"
        Me.prmryColorCd.FieldName = "prmryColorCd"
        Me.prmryColorCd.Name = "prmryColorCd"
        Me.prmryColorCd.OptionsColumn.AllowEdit = False
        Me.prmryColorCd.OptionsColumn.ReadOnly = True
        Me.prmryColorCd.Visible = True
        Me.prmryColorCd.VisibleIndex = 24
        '
        'cproductId
        '
        Me.cproductId.Caption = "product Id"
        Me.cproductId.FieldName = "productId"
        Me.cproductId.Name = "cproductId"
        Me.cproductId.OptionsColumn.AllowEdit = False
        Me.cproductId.OptionsColumn.ReadOnly = True
        Me.cproductId.Visible = True
        Me.cproductId.VisibleIndex = 25
        '
        'cplugColorwayCd
        '
        Me.cplugColorwayCd.Caption = "Plug CW cd"
        Me.cplugColorwayCd.FieldName = "plugColorwayCd"
        Me.cplugColorwayCd.Name = "cplugColorwayCd"
        Me.cplugColorwayCd.OptionsColumn.AllowEdit = False
        Me.cplugColorwayCd.OptionsColumn.ReadOnly = True
        Me.cplugColorwayCd.Visible = True
        Me.cplugColorwayCd.VisibleIndex = 26
        '
        'CprmryColorId
        '
        Me.CprmryColorId.Caption = "Prmry Color Id"
        Me.CprmryColorId.FieldName = "prmryColorId"
        Me.CprmryColorId.Name = "CprmryColorId"
        Me.CprmryColorId.OptionsColumn.AllowEdit = False
        Me.CprmryColorId.OptionsColumn.ReadOnly = True
        Me.CprmryColorId.Visible = True
        Me.CprmryColorId.VisibleIndex = 27
        '
        'CprmryDesc
        '
        Me.CprmryDesc.Caption = "Prmry Desc"
        Me.CprmryDesc.FieldName = "prmryDesc"
        Me.CprmryDesc.Name = "CprmryDesc"
        Me.CprmryDesc.OptionsColumn.AllowEdit = False
        Me.CprmryDesc.OptionsColumn.ReadOnly = True
        Me.CprmryDesc.Visible = True
        Me.CprmryDesc.VisibleIndex = 28
        '
        'CcolorwayId
        '
        Me.CcolorwayId.Caption = "colorway Id"
        Me.CcolorwayId.FieldName = "colorwayId"
        Me.CcolorwayId.Name = "CcolorwayId"
        Me.CcolorwayId.OptionsColumn.AllowEdit = False
        Me.CcolorwayId.OptionsColumn.ReadOnly = True
        Me.CcolorwayId.Visible = True
        Me.CcolorwayId.VisibleIndex = 29
        '
        'CcolorwayCd
        '
        Me.CcolorwayCd.Caption = "colorway Cd"
        Me.CcolorwayCd.FieldName = "colorwayCd"
        Me.CcolorwayCd.Name = "CcolorwayCd"
        Me.CcolorwayCd.OptionsColumn.AllowEdit = False
        Me.CcolorwayCd.OptionsColumn.ReadOnly = True
        Me.CcolorwayCd.Visible = True
        Me.CcolorwayCd.VisibleIndex = 30
        '
        'bomId
        '
        Me.bomId.Caption = "bomId"
        Me.bomId.FieldName = "bomId"
        Me.bomId.Name = "bomId"
        Me.bomId.OptionsColumn.AllowEdit = False
        Me.bomId.OptionsColumn.ReadOnly = True
        Me.bomId.Visible = True
        Me.bomId.VisibleIndex = 31
        '
        'cid
        '
        Me.cid.Caption = "id"
        Me.cid.FieldName = "id"
        Me.cid.Name = "cid"
        Me.cid.OptionsColumn.AllowEdit = False
        Me.cid.OptionsColumn.ReadOnly = True
        Me.cid.Visible = True
        Me.cid.VisibleIndex = 32
        '
        'cresourceType
        '
        Me.cresourceType.Caption = "resourceType"
        Me.cresourceType.FieldName = "resourceType"
        Me.cresourceType.Name = "cresourceType"
        Me.cresourceType.OptionsColumn.AllowEdit = False
        Me.cresourceType.OptionsColumn.ReadOnly = True
        Me.cresourceType.Visible = True
        Me.cresourceType.VisibleIndex = 33
        '
        'CobjectId
        '
        Me.CobjectId.Caption = "objectId"
        Me.CobjectId.FieldName = "objectId"
        Me.CobjectId.Name = "CobjectId"
        Me.CobjectId.OptionsColumn.AllowEdit = False
        Me.CobjectId.OptionsColumn.ReadOnly = True
        Me.CobjectId.Visible = True
        Me.CobjectId.VisibleIndex = 34
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.AutoHeight = False
        Me.RepositoryItemTextEdit1.DisplayFormat.FormatString = "N0"
        Me.RepositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemTextEdit1.EditFormat.FormatString = "N0"
        Me.RepositoryItemTextEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        '
        'RepositoryItemCheckEdit1
        '
        Me.RepositoryItemCheckEdit1.AutoHeight = False
        Me.RepositoryItemCheckEdit1.Caption = "Check"
        Me.RepositoryItemCheckEdit1.Name = "RepositoryItemCheckEdit1"
        Me.RepositoryItemCheckEdit1.ValueChecked = "1"
        Me.RepositoryItemCheckEdit1.ValueUnchecked = "0"
        '
        'UBompage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ogdbom)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "UBompage"
        Me.Size = New System.Drawing.Size(1347, 723)
        CType(Me.ogdbom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvbomitem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ogvbom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemCheckEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ogdbom As DevExpress.XtraGrid.GridControl
    Friend WithEvents ogvbomitem As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CbomRowNbr As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Cis As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Cit As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CitemNbr As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents description As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CvendNm As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CvendLo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CvendCd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CvendId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Cuse As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CcomponentOrd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CitemType1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cpcxSuppliedMatlId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CbomComponentId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CbomItmId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CbomItmSetupTimestamp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CbomItmUpdateTimestamp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ogvbom As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CstyleNbr As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CseasonCd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CseasonYr As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CstyleNm As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CcycleYear As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CbomStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents bomUpdateTimestamp As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Cdeveloper As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents developerUserId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CbomUpdateUserid As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CprimDevRegAbrv As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cprimDevRegCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cdesignRegAbrv As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cdesignRegCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cfactoryCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents parentFcty As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CsilhouetteCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents csilhouetteDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CmscIdentifier As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmscCode As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CmscLevel1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmscLevel2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cmscLevel3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cprmryAbrv As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents prmryColorCd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cproductId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cplugColorwayCd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CprmryColorId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CprmryDesc As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CcolorwayId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CcolorwayCd As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents bomId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cid As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cresourceType As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CobjectId As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents RepositoryItemCheckEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit

End Class
