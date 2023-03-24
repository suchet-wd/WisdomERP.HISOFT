Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraTab

Public Class wBOMListingAdd

    Private StateLoad As Boolean = False
    Private _RowDataChange As Boolean



    Private BOMSysID As Integer = 0
    Private StyleID As Integer = 0
    Private SeasonID As Integer = 0

    Public StateClose As Boolean = False
    Public StateEdit As Boolean = False

    Public StateReLoad As Boolean = False

    Private wListOrder As wBOMListingListOrderNo
    Private _SysImgPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"

    Private _wNewColorway As wNewColorway
    Private _wNewSize As wNewSize
    Private _wChangeDesc As wChangeColorDesc
    Private _wChangeColorway As wChangeColorway

    Private CalculateExportDataMRP As wBOMListingCalculateMRP
    Public Shared _MContextMenuStripGrid As System.Windows.Forms.ContextMenuStrip

    Private pBOMListUpdate As New List(Of BOMData)()

    Private OldValue As String = ""
    Private NewValue As String = ""
    Private FilerData As Boolean = False

    Enum BOMDataType
        DataString = 0
        DataInteger = 1
    End Enum

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        StateLoad = False


        wListOrder = New wBOMListingListOrderNo
        HI.TL.HandlerControl.AddHandlerObj(wListOrder)
        Dim oSysLang As New HI.ST.SysLanguage
        'Call HI.ST.Lang.InsertLanguage(_CopyStyle)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, wListOrder.Name.ToString.Trim, wListOrder)
        Catch ex As Exception
        Finally
        End Try

        Call HI.ST.Lang.SP_SETxLanguage(wListOrder)


        _wNewColorway = New wNewColorway
        HI.TL.HandlerControl.AddHandlerObj(_wNewColorway)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wNewColorway.Name.ToString.Trim, _wNewColorway)
        Catch ex As Exception
        Finally
        End Try
        Call HI.ST.Lang.SP_SETxLanguage(_wNewColorway)

        _wNewSize = New wNewSize
        HI.TL.HandlerControl.AddHandlerObj(_wNewSize)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wNewSize.Name.ToString.Trim, _wNewSize)
        Catch ex As Exception
        Finally
        End Try
        Call HI.ST.Lang.SP_SETxLanguage(_wNewSize)

        _wChangeDesc = New wChangeColorDesc
        HI.TL.HandlerControl.AddHandlerObj(_wChangeDesc)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wChangeDesc.Name.ToString.Trim, _wChangeDesc)
        Catch ex As Exception
        Finally
        End Try
        Call HI.ST.Lang.SP_SETxLanguage(_wChangeDesc)

        _wChangeColorway = New wChangeColorway
        HI.TL.HandlerControl.AddHandlerObj(_wChangeColorway)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wChangeColorway.Name.ToString.Trim, _wChangeColorway)
        Catch ex As Exception
        Finally
        End Try
        Call HI.ST.Lang.SP_SETxLanguage(_wChangeColorway)



        CalculateExportDataMRP = New wBOMListingCalculateMRP
        HI.TL.HandlerControl.AddHandlerObj(CalculateExportDataMRP)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, CalculateExportDataMRP.Name.ToString.Trim, CalculateExportDataMRP)
        Catch ex As Exception
        Finally
        End Try
        Call HI.ST.Lang.SP_SETxLanguage(CalculateExportDataMRP)


        Call CreateMergeEditControl()

        Call LoadMaster()

        Call CreateManuStripGrid()

    End Sub

    Public Sub CreateManuStripGrid()

        _MContextMenuStripGrid = New System.Windows.Forms.ContextMenuStrip
        Dim _ExportToExcel As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToExcelData As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToCsv As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToPDF As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToText As New System.Windows.Forms.ToolStripMenuItem
        Dim _ClearLayout As New System.Windows.Forms.ToolStripMenuItem

        With _ExportToExcel
            .Name = "ocmExportToExcel"
            .Text = "Export To Excel"

            Dim tPathImg As String = _SysImgPath & "\Func\ExportToExcel.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If

            AddHandler .Click, AddressOf HI.TL.HandlerControl.ExportToExcel_Click
        End With


        With _ExportToExcelData
            .Name = "ocmExportToExcelData"
            .Text = "Export To Excel (Data)"

            Dim tPathImg As String = _SysImgPath & "\Func\ExportToExcel.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If

            AddHandler .Click, AddressOf HI.TL.HandlerControl.ExportToExcelData_Click
        End With

        With _ExportToCsv
            .Name = "ocmExportToCsv"
            .Text = "Export To CSV"
            Dim tPathImg As String = _SysImgPath & "\Func\ExportToCSV.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf HI.TL.HandlerControl.ExportToCSV_Click
        End With

        With _ExportToPDF
            .Name = "ocmExportToPDF"
            .Text = "Export To PDF"
            Dim tPathImg As String = _SysImgPath & "\Func\ExportToPDF.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf HI.TL.HandlerControl.ExportToPDF_Click
        End With

        With _ExportToText
            .Name = "ocmExportToText"
            .Text = "Export To Text"
            Dim tPathImg As String = _SysImgPath & "\Func\ExportToText.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf HI.TL.HandlerControl.ExportToText_Click
        End With


        With _ClearLayout
            .Name = "ocmClearGridLayout"
            .Text = "Clear Grid Layout"
            Dim tPathImg As String = _SysImgPath & "\Func\HRClear.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf ClearLayout_Click
        End With

        With _MContextMenuStripGrid
            .Name = "BomAddContextExportDataGridControl"
            .Items.AddRange(New System.Windows.Forms.ToolStripItem() {_ExportToExcel, _ExportToExcelData, _ExportToCsv, _ExportToPDF, _ExportToText, _ClearLayout})
        End With




    End Sub


    Public Sub ClearLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            Try
                Dim _Form As Object

                With CType(CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraGrid.GridControl).MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                    _Form = .GridControl.FindForm

                End With

                HI.UL.AppRegistry.DeleteLayoutGridToRegistry(_Form, CType(sender, DevExpress.XtraGrid.Views.Grid.GridView))
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try

    End Sub
#Region "Class"
    Private Class BOMData

        Private _FieldName As String
        Public Property FieldName() As String
            Get
                Return _FieldName
            End Get
            Set(ByVal value As String)
                _FieldName = value
            End Set
        End Property

        Private _FieldValue As String
        Public Property FieldValue() As String
            Get
                Return _FieldValue
            End Get
            Set(ByVal value As String)
                _FieldValue = value
            End Set
        End Property

        Private _FieldValueTH As String
        Public Property FieldValueTH() As String
            Get
                Return _FieldValueTH
            End Get
            Set(ByVal value As String)
                _FieldValueTH = value
            End Set
        End Property

        Private _FieldValueEN As String
        Public Property FieldValueEN() As String
            Get
                Return _FieldValueEN
            End Get
            Set(ByVal value As String)
                _FieldValueEN = value
            End Set
        End Property


        Private _FieldDataType As BOMDataType
        Public Property FieldDataType() As BOMDataType
            Get
                Return _FieldDataType
            End Get
            Set(ByVal value As BOMDataType)
                _FieldDataType = value
            End Set
        End Property

    End Class
#End Region



#Region "Master"

    Private Sub LoadMaster()
        Call LoadSetPart()
        Call LoadMainMat()
        Call LoadSupplier()
        Call LoadCurrency()
        Call LoadUnit()
        Call LoadCustomer()
        Call LoadDeveloper()
        Call LoadRMColor()
        Call LoadRMSize()
    End Sub
    Private Sub LoadSetPart()

        Dim _Qry As String

        _Qry = "SELECT        FNListIndex AS FNIndex"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " ,FTNameTH  AS FTName "
        Else
            _Qry &= vbCrLf & " , FTNameEN AS FTName "
        End If

        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE  (FTListName = N'FNOrderSetTypeBOM')"
        _Qry &= vbCrLf & " ORDER BY FNListIndex  "

        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        Me.RepositoryFNOrderSetType.DataSource = dt.Copy
        Me.RepositoryFNOrderSetType2.DataSource = dt.Copy
        Me.RepositoryFNOrderSetType3.DataSource = dt.Copy
        Me.RepositoryFNOrderSetType4.DataSource = dt.Copy
        Me.RepositoryFNOrderSetType5.DataSource = dt.Copy

        dt.Dispose()

    End Sub


    Private Sub LoadCustomer()

        Dim _Qry As String

        _Qry = " SELECT        A.FNHSysCustId,A.FTCustCode "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "  , A.FTCustNameTH As FTCustName"
        Else
            _Qry &= vbCrLf & "  , A.FTCustNameEN As FTCustName"
        End If


        _Qry &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer As A WITH(NOLOCK) "

        _Qry &= vbCrLf & "  Where A.FTStateActive ='1' "
        _Qry &= vbCrLf & "  ORDER BY A.FTCustCode  "


        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        Me.FNHSysCustId.Properties.DataSource = dt.Copy

        dt.Dispose()

    End Sub


    Private Sub LoadDeveloper()

        Dim _Qry As String

        _Qry = " SELECT       A.FTUserName AS FTDevelopName "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "  , A.FTUserDescriptionTH As FTDevelopName2"
        Else
            _Qry &= vbCrLf & "  , A.FTUserDescriptionEN As FTDevelopName2"
        End If


        _Qry &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin As A WITH(NOLOCK) "

        _Qry &= vbCrLf & "  Where A.FTStateActive ='1' AND ISNULL(A.FTStateAdmin,'') <> '1' "
        _Qry &= vbCrLf & "  ORDER BY A.FTUserName  "


        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SECURITY)

        Me.FTDevelopName.Properties.DataSource = dt.Copy

        dt.Dispose()

    End Sub


    Private Sub LoadMainMat()

        Dim _Qry As String

        _Qry = " SELECT        A.FNHSysMainMatId,A.FTMainMatCode "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "  , A.FTMainMatNameTH As FTMainMatName"
        Else
            _Qry &= vbCrLf & "  , A.FTMainMatNameEN As FTMainMatName"
        End If


        _Qry &= vbCrLf & "  , A.FTStateNominate, A.FNHSysUnitId, A.FNHSysCurId"
        _Qry &= vbCrLf & "  , A.FNPrice, A.FTStateMainMaterial, A.FTStateActive, A.FNHSysSuplId, S.FTSuplCode, U.FTUnitCode, C.FTCurCode "
        _Qry &= vbCrLf & "  ,A.FTFabricFrontSize,A.FTStateDefualtBOMColorByColorway,A.FTStateDefualtBOMSizeBySizeBreakdown,A.FTStateSilk "
        _Qry &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat As A WITH(NOLOCK) LEFT OUTER Join "
        _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency As C WITH(NOLOCK) On A.FNHSysCurId = C.FNHSysCurId LEFT OUTER Join "
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit As U WITH(NOLOCK) On A.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER Join "
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier As S WITH(NOLOCK) On A.FNHSysSuplId = S.FNHSysSuplId "
        _Qry &= vbCrLf & "  Where A.FTStateActive ='1' "
        _Qry &= vbCrLf & "  ORDER BY A.FTMainMatCode  "


        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        Me.RepositoryItemGridLookUpEditMainMatCode.DataSource = dt.Copy

        m_mergedCellEditorMainMat.Properties.DataSource = dt.Copy

        dt.Dispose()

    End Sub


    Private Sub LoadSupplier()

        Dim _Qry As String

        _Qry = " SELECT         A.FNHSysSuplId,A.FTSuplCode ,A.FNHSysCurId,C.FTCurCode "


        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "  , A.FTSuplNameTH As FTSuplName"
        Else
            _Qry &= vbCrLf & "  , A.FTSuplNameEN As FTSuplName"
        End If

        _Qry &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier As A WITH(NOLOCK) "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency As C WITH(NOLOCK) ON A.FNHSysCurId = C.FNHSysCurId "
        _Qry &= vbCrLf & "  Where A.FTStateActive ='1'  "
        _Qry &= vbCrLf & "  ORDER BY A.FTSuplCode  "


        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        Me.RepositoryItemGridLookUpEditFTSuplCode.DataSource = dt.Copy

        m_mergedCellEditorSupl.Properties.DataSource = dt.Copy

        dt.Dispose()

    End Sub

    Private Sub LoadCurrency()

        Dim _Qry As String

        _Qry = " SELECT        A.FNHSysCurId,A.FTCurCode "
        _Qry &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency As A WITH(NOLOCK) "
        _Qry &= vbCrLf & "  Where A.FTStateActive ='1'  "
        _Qry &= vbCrLf & "  ORDER BY A.FTCurCode  "


        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        Me.RepositoryItemGridLookUpEditFTCurCode.DataSource = dt.Copy
        m_mergedCellEditorCurr.Properties.DataSource = dt.Copy
        dt.Dispose()

    End Sub

    Private Sub LoadUnit()

        Dim _Qry As String

        _Qry = " SELECT        A.FNHSysUnitId,A.FTUnitCode "
        _Qry &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit As A WITH(NOLOCK) "
        _Qry &= vbCrLf & "  Where A.FTStateActive ='1'  "
        _Qry &= vbCrLf & "  ORDER BY A.FTUnitCode  "


        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        Me.RepositoryItemGridLookUpEditFTUnitCode.DataSource = dt.Copy
        m_mergedCellEditorUnit.Properties.DataSource = dt.Copy

        dt.Dispose()

    End Sub


    Private Sub LoadRMColor()

        Dim _Qry As String
        _Qry = " SELECT * FROM ( SELECT  0 FNHSysRawMatColorId,'' AS FTRawMatColorCode ,'' AS FTRawMatColorNameTH, '' AS FTRawMatColorNameEN"
        _Qry &= vbCrLf & " UNION "
        _Qry &= vbCrLf & " SELECT        A.FNHSysRawMatColorId,A.FTRawMatColorCode "
        _Qry &= vbCrLf & "  , A.FTRawMatColorNameTH, A.FTRawMatColorNameEN "
        _Qry &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor As A WITH(NOLOCK)  "

        _Qry &= vbCrLf & "  Where A.FTStateActive ='1' ) AS A "
        _Qry &= vbCrLf & "  ORDER BY A.FTRawMatColorCode  "


        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        Me.RepositoryItemGridLookUpEditRMColor.DataSource = dt.Copy

        dt.Dispose()

    End Sub


    Private Sub LoadRMSize()

        Dim _Qry As String

        _Qry = " SELECT * FROM ( SELECT    0 AS FNHSysRawMatSizeId,'' AS FTRawMatSizeCode"
        _Qry &= vbCrLf & " UNION "
        _Qry &= vbCrLf & "  SELECT        A.FNHSysRawMatSizeId,A.FTRawMatSizeCode "

        _Qry &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize As A WITH(NOLOCK)  "

        _Qry &= vbCrLf & "  Where A.FTStateActive ='1' ) AS A"
        _Qry &= vbCrLf & "  ORDER BY A.FTRawMatSizeCode  "


        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        Me.RepositoryItemGridLookUpEditRMSize.DataSource = dt.Copy

        dt.Dispose()

    End Sub

#End Region


#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

    Private _ActualDate As String = ""
    ReadOnly Property ActualDate As String
        Get
            Return _ActualDate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property


#End Region

#Region "Procedure"

    Private Sub InitGrid()


        For Each GrCol As DevExpress.XtraGrid.Columns.GridColumn In ogvmat.Columns
            GrCol.OptionsColumn.AllowSort = DefaultBoolean.False
        Next

        For Each GrCol As DevExpress.XtraGrid.Columns.GridColumn In ogvmatcolor.Columns
            GrCol.OptionsColumn.AllowSort = DefaultBoolean.False
        Next


        For Each GrCol As DevExpress.XtraGrid.Columns.GridColumn In ogvmatcolornote.Columns
            GrCol.OptionsColumn.AllowSort = DefaultBoolean.False
        Next


        For Each GrCol As DevExpress.XtraGrid.Columns.GridColumn In ogvmatsize.Columns
            GrCol.OptionsColumn.AllowSort = DefaultBoolean.False
        Next


        For Each GrCol As DevExpress.XtraGrid.Columns.GridColumn In ogvmatsilkcolor.Columns
            GrCol.OptionsColumn.AllowSort = DefaultBoolean.False
        Next

    End Sub

    Public Sub LoadBom(BomID As Integer)

        StateLoad = False


        With Me.ogvmat

            ' .Columns.ColumnByFieldName("FTSelect").Visible = StateEdit
            .Columns.ColumnByFieldName("FTSelect").OptionsColumn.AllowEdit = StateEdit


            .Columns.ColumnByFieldName("FNMatSeq").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FTMainMatCode").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FTUnitCode").OptionsColumn.AllowEdit = False ' StateEdit
            .Columns.ColumnByFieldName("FTSuplCode").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FTCurCode").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FNPrice").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FTPositionPartName").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FNPackPerCarton").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FNRepeatLengthCM").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FNConSmp").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FNConSmpPlus").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FTOrderNo").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FTSubOrderNo").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FNOrderSetType").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FTStateNominate").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FTStateCombination").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FTStateHemNotOptiplan").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FTStateFree").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FTStateDTM").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FTDTMNote").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FTStateActive").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FTNote").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FTStateMatConfirm").OptionsColumn.AllowEdit = StateEdit
            .Columns.ColumnByFieldName("FTStateCalMRP").OptionsColumn.AllowEdit = StateEdit


        End With

        ogvmatcolor.Columns.ColumnByFieldName("FTRunColor").OptionsColumn.AllowEdit = StateEdit
        ogvmatsilkcolor.Columns.ColumnByFieldName("FTSilkName").OptionsColumn.AllowEdit = StateEdit


        For Each bt As Object In UIButtonPanel.Buttons
            If StateEdit Then

                Select Case HI.ENM.Control.GeTypeControl(bt)
                    Case ENM.Control.ControlType.WindowsUIButton
                        With CType(bt, DevExpress.XtraBars.Docking2010.WindowsUIButton)
                            .Visible = True
                        End With

                    Case ENM.Control.ControlType.WindowsUISeparator
                        With CType(bt, DevExpress.XtraBars.Docking2010.WindowsUISeparator)
                            .Visible = True
                        End With
                End Select

            Else


                Select Case HI.ENM.Control.GeTypeControl(bt)
                    Case ENM.Control.ControlType.WindowsUIButton
                        With CType(bt, DevExpress.XtraBars.Docking2010.WindowsUIButton)

                            If .Tag.ToString = "exit" Then
                                .Visible = True
                            Else

                                .Visible = False
                            End If


                        End With

                    Case ENM.Control.ControlType.WindowsUISeparator
                        With CType(bt, DevExpress.XtraBars.Docking2010.WindowsUISeparator)
                            .Visible = False
                        End With
                End Select

            End If
        Next

        Dim _Qry As String = ""
        Dim _dt As DataTable
        BOMSysID = 0
        StyleID = 0
        SeasonID = 0

        Me.ogcmat.DataSource = Nothing
        Me.ogcmatcolor.DataSource = Nothing
        Me.ogcmatsize.DataSource = Nothing
        Me.ogcmatcolornote.DataSource = Nothing
        Me.ogcmatsilkcolor.DataSource = Nothing



        Dim _Spls As New HI.TL.SplashScreen("Loading BOM...   Please Wait   ")

        _Qry = "Select           A.FTInsUser, A.FDInsDate, A.FTInsTime "
        _Qry &= vbCrLf & "    , A.FTUpdUser, A.FDUpdDate, A.FTUpdTime "
        _Qry &= vbCrLf & "   , A.FNHSysBomId "
        _Qry &= vbCrLf & "	, A.FNHSysStyleId "
        _Qry &= vbCrLf & "  , A.FNHSysSeasonId "
        _Qry &= vbCrLf & ", A.FNHSysCustId "
        _Qry &= vbCrLf & "  , A.FNBomType "
        _Qry &= vbCrLf & "	, BT.FNBomTypeName "
        _Qry &= vbCrLf & "  , A.FNBomVersion "
        _Qry &= vbCrLf & "	, A.FTDevelopName, A.FTNote, A.FTStateActive, A.FTSatetConfirm "
        _Qry &= vbCrLf & "  , A.FTSatetConfirmBy, A.FDSatetConfirmDate, A.FTSatetConfirmTime "
        _Qry &= vbCrLf & " , A.FTSatetApprove, A.FTSatetApproveBy, A.FDSatetApproveDate,A.FNStateBomOrder "
        _Qry &= vbCrLf & "  , A.FTSatetApproveTime, ST.FTStyleCode, SS.FTSeasonCode "
        _Qry &= vbCrLf & "	, ST.FTStyleNameEN As FTStyleName, ST.FTStateGameDays, ST.FTStateStyleSet "
        _Qry &= vbCrLf & "  ,BM1.FTBomMatUpdUser  "
        _Qry &= vbCrLf & "	,BM1.FTBomMatUpdTime "
        _Qry &= vbCrLf & "	,BM2.FTBomMatColorUpdUser "
        _Qry &= vbCrLf & "	,BM2.FTBomMatColorUpdTime "
        _Qry &= vbCrLf & "	,BM3.FTBomMatSizeUpdUser "
        _Qry &= vbCrLf & "	,BM3.FTBomMatSizeUpdTime "
        _Qry &= vbCrLf & "	,Cust.* ,Dev.*"
        _Qry &= vbCrLf & " From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM As A With(NOLOCK) INNER Join "
        _Qry &= vbCrLf & " " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle As ST With(NOLOCK) On A.FNHSysStyleId = ST.FNHSysStyleId INNER JOIN "
        _Qry &= vbCrLf & "  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMSeason As SS With(NOLOCK) On A.FNHSysSeasonId = SS.FNHSysSeasonId "
        _Qry &= vbCrLf & "  OUTER APPLY (Select TOP 1 L.FTNameTH As  FNBomTypeName FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.HSysListData As L With(NOLOCK) WHERE L.FTListName ='FNBomDevType' AND L.FNListIndex = A.FNBomType )   AS BT    "

        _Qry &= vbCrLf & "  OUTER APPLY(SELECT TOP 1 CASE WHEN ISNULL(BM1.FDUpdDate,'') ='' THEN BM1.FTInsUser ELSE BM1.FTUpdUser END AS  FTBomMatUpdUser "
        _Qry &= vbCrLf & "           ,CASE WHEN ISNULL(BM1.FDUpdDate,'') ='' THEN BM1.FDInsDate  + ' ' + BM1.FTInsTime ELSE ISNULL(BM1.FDUpdDate,'') + ' ' + BM1.FTUpdTime END AS FTBomMatUpdTime "
        _Qry &= vbCrLf & "     From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat AS BM1 WITH(NOLOCK) "
        _Qry &= vbCrLf & "  	Where BM1.FNHSysBomId = A.FNHSysBomId "
        _Qry &= vbCrLf & "     Order By CASE WHEN ISNULL(BM1.FDUpdDate,'') ='' THEN BM1.FDInsDate  + ' ' + BM1.FTInsTime ELSE ISNULL(BM1.FDUpdDate,'') + ' ' + BM1.FTUpdTime END DESC "

        _Qry &= vbCrLf & "   )   AS BM1       "
        _Qry &= vbCrLf & "      OUTER APPLY(SELECT TOP 1 CASE WHEN ISNULL(BM2.FDUpdDate,'') ='' THEN BM2.FTInsUser ELSE BM2.FTUpdUser END AS  FTBomMatColorUpdUser "
        _Qry &= vbCrLf & "     ,CASE WHEN ISNULL(BM2.FDUpdDate,'') ='' THEN BM2.FDInsDate  + ' ' + BM2.FTInsTime ELSE ISNULL(BM2.FDUpdDate,'') + ' ' + BM2.FTUpdTime END AS FTBomMatColorUpdTime "
        _Qry &= vbCrLf & "     From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Colorway AS BM2 WITH(NOLOCK) "
        _Qry &= vbCrLf & "   Where BM2.FNHSysBomId = A.FNHSysBomId "
        _Qry &= vbCrLf & "    Order By CASE WHEN ISNULL(BM2.FDUpdDate,'') ='' THEN BM2.FDInsDate  + ' ' + BM2.FTInsTime ELSE ISNULL(BM2.FDUpdDate,'') + ' ' + BM2.FTUpdTime END DESC "
        _Qry &= vbCrLf & "     )   AS BM2       "
        _Qry &= vbCrLf & "     OUTER APPLY(SELECT TOP 1 CASE WHEN ISNULL(BM3.FDUpdDate,'') ='' THEN BM3.FTInsUser ELSE BM3.FTUpdUser END AS  FTBomMatSizeUpdUser  "
        _Qry &= vbCrLf & "       ,CASE WHEN ISNULL(BM3.FDUpdDate,'') ='' THEN BM3.FDInsDate  + ' ' + BM3.FTInsTime ELSE ISNULL(BM3.FDUpdDate,'') + ' ' + BM3.FTUpdTime END As FTBomMatSizeUpdTime "
        _Qry &= vbCrLf & "     From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Sizebreakdown AS BM3 WITH(NOLOCK)  "
        _Qry &= vbCrLf & "  	Where BM3.FNHSysBomId = A.FNHSysBomId "
        _Qry &= vbCrLf & "     Order By CASE WHEN ISNULL(BM3.FDUpdDate,'') ='' THEN BM3.FDInsDate  + ' ' + BM3.FTInsTime ELSE ISNULL(BM3.FDUpdDate,'') + ' ' + BM3.FTUpdTime END DESC "
        _Qry &= vbCrLf & "     )   AS BM3       "

        _Qry &= vbCrLf & "     OUTER APPLY(  SELECT   TOP 1   Cust.FTCustCode "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "  , Cust.FTCustNameTH As FTCustName"
        Else
            _Qry &= vbCrLf & "  , Cust.FTCustNameEN As FTCustName"
        End If

        _Qry &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer As Cust WITH(NOLOCK) "
        _Qry &= vbCrLf & "  Where Cust.FNHSysCustId =A.FNHSysCustId  "
        _Qry &= vbCrLf & "  ) AS   Cust "

        _Qry &= vbCrLf & "     OUTER APPLY(  SELECT   TOP 1    "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & "   Dev.FTUserDescriptionTH As FTDevelopName2"
        Else
            _Qry &= vbCrLf & "   Dev.FTUserDescriptionEN As FTDevelopName2"
        End If


        _Qry &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin As Dev WITH(NOLOCK) "
        _Qry &= vbCrLf & "  Where Dev.FTUserName =A.FTDevelopName  "
        _Qry &= vbCrLf & "  ) AS   Dev "


        _Qry &= vbCrLf & " WHERE  A.FNHSysBomId=" & BomID & " AND  A.FNHSysBomId <> 0 "
        _Qry &= vbCrLf & " ORDER BY ST.FTStyleCode, SS.FTSeasonCode,BT.FNBomTypeName,A.FNBomVersion "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        For Each R As DataRow In _dt.Rows

            BOMSysID = BomID
            StyleID = Val(R!FNHSysStyleId.ToString)
            SeasonID = Val(R!FNHSysSeasonId.ToString)

            FNHSysStyleId.Text = R!FTStyleCode.ToString
            FNHSysStyleId_None.Text = R!FTStyleName.ToString
            FNHSysSeasonId.Text = R!FTSeasonCode.ToString
            FNVersion.Value = Val(R!FNBomVersion.ToString)

            FNHSysCustId.Text = R!FTCustCode.ToString
            FNHSysCustId.EditValue = R!FNHSysCustId.ToString
            FNHSysCustId_None.Text = R!FTCustName.ToString

            FTDevelopName.Text = R!FTDevelopName.ToString
            FTDevelopName.EditValue = R!FTDevelopName.ToString
            FTDevelopName_None.Text = R!FTDevelopName2.ToString


            FTRemark.Text = R!FTNote.ToString
            FTStateActive.Checked = (R!FTStateActive.ToString = "1")
            xFTSatetConfirm.Checked = (R!FTSatetConfirm.ToString = "1")
            FTStateGameDays.Checked = (R!FTStateGameDays.ToString = "1")
            FTStateStyleSet.Checked = (R!FTStateStyleSet.ToString = "1")

            FNStateBomOrder.SelectedIndex = Val(R!FNStateBomOrder.ToString)
            FNBomDevType.SelectedIndex = Val(R!FNBomType.ToString)

        Next


        If BOMSysID > 0 Then

            Call LoadBOMDetaiil(BOMSysID)

        End If
        _dt.Dispose()


        LoadSPOrder(BOMSysID)



        _Spls.Close()

        _RowDataChange = False
        StateLoad = True

    End Sub

    Private Sub LoadBOMDetaiil(BOMID As Integer, Optional pLoadType As Integer = 0)

        Dim Spls As New HI.TL.SplashScreen("Loading... BOM Detail Please Wait.")

        Dim _Qry As String = ""

        Dim ds As New DataSet
        _Qry = "EXEC " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_GETBOMGARMENT_DATA '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & BOMSysID & "," & pLoadType & ""

        HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, ds)

        Try

            Select Case pLoadType
                Case 0
                    If ds.Tables.Count = 7 Then

                        Call SetNewColumnColoyWay(ds.Tables(0))
                        Call SetNewColumnSize(ds.Tables(1))
                        Me.ogcmat.DataSource = ds.Tables(2)
                        Me.ogcmatcolor.DataSource = ds.Tables(3)
                        Me.ogcmatcolornote.DataSource = ds.Tables(4)
                        Me.ogcmatsilkcolor.DataSource = ds.Tables(5)

                        Me.ogcmatsize.DataSource = ds.Tables(6)



                    ElseIf ds.Tables.Count = 3 Then
                        Me.ogcmat.DataSource = ds.Tables(2)

                    End If
                Case 1
                    If ds.Tables.Count = 4 Then
                        Call SetNewColumnColoyWay(ds.Tables(0))

                        Me.ogcmatcolor.DataSource = ds.Tables(1)
                        Me.ogcmatcolornote.DataSource = ds.Tables(2)
                        Me.ogcmatsilkcolor.DataSource = ds.Tables(3)


                    End If
                Case 2
                    If ds.Tables.Count = 2 Then

                        Call SetNewColumnSize(ds.Tables(0))

                        Me.ogcmatsize.DataSource = ds.Tables(1)

                    End If
            End Select


            ogvmat.LeftCoord = 0
            ogvmatcolor.LeftCoord = 0
            ogvmatcolornote.LeftCoord = 0
            ogvmatsilkcolor.LeftCoord = 0
            ogvmatsize.LeftCoord = 0


        Catch ex As Exception
        End Try

        Spls.Close()
        StateReLoad = False
    End Sub
    Private Sub LoadSPOrder(BOMID As Integer)

        Dim cmdstring As String = ""


        cmdstring = " Select  A.FTOrderNo, C.FTCmpCode ,C1.FTBuyCode "
        cmdstring &= vbCrLf & " From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SpecialOrder As A WITH(NOLOCK) INNER Join "
        cmdstring &= vbCrLf & "    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.V_OrderProdAndSMPAll As B WITH(NOLOCK) On A.FTOrderNo = B.FTOrderNo INNER Join "
        cmdstring &= vbCrLf & "   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCmp AS C WITH(NOLOCK) ON B.FNHSysCmpId = C.FNHSysCmpId  "

        cmdstring &= vbCrLf & "  Outer Apply( select top 1 C1.FTBuyCode  from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMBuy AS C1 WITH(NOLOCK) WHERE C1.FNHSysBuyId = B.FNHSysBuyId ) C1  "

        cmdstring &= vbCrLf & "  WHERE  A.FNHSysBomId = " & BOMID & " "
        cmdstring &= vbCrLf & "  Order BY A.FTOrderNo, C.FTCmpCode "

        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)
        ogdorder.DataSource = dt.Copy

        dt.Dispose()

    End Sub

    Private Sub SetNewColumnColoyWay(dt As DataTable)
        Try
            Dim StrCol As String = dt.Rows(0)!FTColumn.ToString

            Dim strGridViewName As String = "ogcmatcolor|ogcmatcolornote|ogcmatsilkcolor"
            Dim ViIndx As Integer = 1000

            For Each gStrName As String In strGridViewName.Split("|")
                ViIndx = 1000
                For Each Obj As Object In Me.Controls.Find(gStrName, True)

                    ViIndx = ViIndx + 1
                    With CType(CType(Obj, DevExpress.XtraGrid.GridControl).MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                        .BeginInit()


                        For I As Integer = .Columns.Count - 1 To 0 Step -1
                            Select Case Microsoft.VisualBasic.Left(.Columns(I).Name.ToString, 4).ToUpper
                                Case "CFIX".ToUpper

                                Case Else

                                    Dim FName As String = .Columns(I).FieldName

                                    .Columns.Remove(.Columns(I))
                            End Select

                        Next


                        If StrCol <> "" Then

                            For Each R As String In StrCol.Split(",")


                                Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                                With ColG

                                    .FieldName = R.Replace("[", "").Replace("]", "")
                                    .Name = "gViewColor" & gStrName & R.Replace(" ", "_").Replace("[", "").Replace("]", "")
                                    .Caption = R.Replace("[", "").Replace("]", "")
                                    .Visible = True
                                    .VisibleIndex = ViIndx
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


                                    Select Case gStrName
                                        Case "ogcmatcolor"

                                            .ColumnEdit = RepositoryItemGridLookUpEditRMColor

                                        Case "ogcmatcolornote"
                                            .ColumnEdit = RepositoryItemTextEditColorNote
                                        Case "ogcmatsilkcolor"
                                            .ColumnEdit = RepositoryItemPopupContainerEditSilkColor

                                    End Select

                                    With .OptionsColumn

                                        Select Case gStrName
                                            Case "ogcmatcolor"
                                                .AllowMove = True
                                            Case Else
                                                .AllowMove = False
                                        End Select

                                        .AllowShowHide = False
                                        .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                        .AllowMerge = DefaultBoolean.False
                                        .AllowEdit = StateEdit
                                        .ReadOnly = False
                                    End With

                                    With .OptionsFilter
                                        .AutoFilterCondition = AutoFilterCondition.Contains
                                        .FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList
                                    End With


                                    Select Case gStrName
                                        Case "ogvmatcolor"
                                            .Width = 80
                                        Case "ogvmatcolornote"
                                            .Width = 100
                                        Case "ogvmatsilkcolor"
                                            .Width = 100
                                    End Select


                                End With

                                .Columns.Add(ColG)

                                'If StateEdit Then
                                '    .Columns.ColumnByFieldName(ColG.FieldName).AppearanceCell.BackColor = Color.LightCyan
                                '    .Columns.ColumnByFieldName(ColG.FieldName).OptionsColumn.AllowShowHide = False
                                'End If


                                If gStrName = "ogcmatcolor" Then
                                    Dim ColGTH As New DevExpress.XtraGrid.Columns.GridColumn
                                    With ColGTH

                                        .FieldName = R.Replace("[", "").Replace("]", "") & "TH"
                                        .Name = gStrName & R.Replace(" ", "_").Replace("[", "").Replace("]", "") & "TH"
                                        .Caption = R.Replace("[", "").Replace("]", "") & "TH"
                                        .Visible = False

                                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                        .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


                                        With .OptionsColumn
                                            .AllowMove = False
                                            .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                            .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                            .AllowMerge = DefaultBoolean.False
                                            .AllowEdit = False
                                            .ReadOnly = True
                                        End With

                                        .Width = 0

                                        With .OptionsFilter
                                            .AutoFilterCondition = AutoFilterCondition.Contains
                                            .FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList
                                        End With

                                    End With


                                    .Columns.Add(ColGTH)


                                    Dim ColGEN As New DevExpress.XtraGrid.Columns.GridColumn
                                    With ColGEN

                                        .FieldName = R.Replace("[", "").Replace("]", "") & "EN"
                                        .Name = gStrName & R.Replace(" ", "_").Replace("[", "").Replace("]", "") & "EN"
                                        .Caption = R.Replace("[", "").Replace("]", "")
                                        .Visible = False

                                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                        .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


                                        With .OptionsColumn
                                            .AllowMove = False
                                            .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                            .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                            .AllowMerge = DefaultBoolean.False
                                            .AllowEdit = False
                                            .ReadOnly = True

                                        End With

                                        With .OptionsFilter
                                            .AutoFilterCondition = AutoFilterCondition.Contains
                                            .FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList
                                        End With


                                        .Width = 0

                                    End With

                                    .Columns.Add(ColGEN)
                                End If



                            Next

                        End If

                        .EndInit()

                    End With


                Next

            Next



        Catch ex As Exception
        End Try

    End Sub

    Private Sub SetNewColumnSize(dt As DataTable)
        Try
            Dim StrCol As String = dt.Rows(0)!FTColumn.ToString

            With Me.ogvmatsize
                .BeginInit()



                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case Microsoft.VisualBasic.Left(.Columns(I).Name.ToString, 4).ToUpper
                        Case "CFIX".ToUpper

                        Case Else

                            Dim FName As String = .Columns(I).FieldName

                            .Columns.Remove(.Columns(I))
                    End Select

                Next


                If StrCol <> "" Then

                    For Each R As String In StrCol.Split(",")


                        Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                        With ColG

                            .FieldName = R.Replace("[", "").Replace("]", "")
                            .Name = "gViewSize" & R.Replace(" ", "_").Replace("[", "").Replace("]", "")
                            .Caption = R.Replace("[", "").Replace("]", "")
                            .Visible = True

                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                            .ColumnEdit = RepositoryItemGridLookUpEditRMSize

                            With .OptionsColumn
                                .AllowMove = True
                                .AllowShowHide = False
                                .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                .AllowMerge = DefaultBoolean.False



                                .AllowEdit = StateEdit
                                .ReadOnly = False
                            End With

                            With .OptionsFilter
                                .AutoFilterCondition = AutoFilterCondition.Contains
                                .FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList
                            End With

                            .Width = 80


                        End With

                        .Columns.Add(ColG)

                        'If StateEdit Then
                        '    .Columns.ColumnByFieldName(ColG.FieldName).AppearanceCell.BackColor = Color.LightCyan
                        '    .Columns.ColumnByFieldName(ColG.FieldName).OptionsColumn.AllowShowHide = False
                        'End If


                    Next

                End If


                .EndInit()
            End With

        Catch ex As Exception
        End Try

    End Sub


    Private Sub SetFilerColumn()
        Try


            For Each c As GridColumn In ogvmatcolor.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next


            For Each c As GridColumn In ogvmatcolornote.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next



            For Each c As GridColumn In ogvmatsilkcolor.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next


            For Each c As GridColumn In ogvmatsize.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next


        Catch ex As Exception
        End Try

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = True



        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function
#End Region

#Region "General"


    Enum EditMergeCellData As Integer
        ItemSeq = 0
        ItemCode = 1
        SuplCode = 2
        UNitCode = 3
        Currency = 4
    End Enum

    Private _StateEditMergeCell As EditMergeCellData = EditMergeCellData.ItemSeq
    Property StateEditMergeCell As EditMergeCellData
        Get
            Return _StateEditMergeCell
        End Get
        Set(value As EditMergeCellData)
            _StateEditMergeCell = value
        End Set
    End Property

    Private m_mergedCellEditorSupl As DevExpress.XtraEditors.GridLookUpEdit
    Private m_mergedCellEditorMainMat As DevExpress.XtraEditors.GridLookUpEdit
    Private m_mergedCellEditorUnit As DevExpress.XtraEditors.GridLookUpEdit
    Private m_mergedCellEditorCurr As DevExpress.XtraEditors.GridLookUpEdit
    Private m_mergedCellEditorMatSeq As DevExpress.XtraEditors.CalcEdit

    Private m_mergedCellsEdited As GridCellInfoCollection

    Private Sub CreateMergeEditControl()
        m_mergedCellEditorSupl = New DevExpress.XtraEditors.GridLookUpEdit
        m_mergedCellEditorMainMat = New DevExpress.XtraEditors.GridLookUpEdit
        m_mergedCellEditorUnit = New DevExpress.XtraEditors.GridLookUpEdit
        m_mergedCellEditorCurr = New DevExpress.XtraEditors.GridLookUpEdit
        m_mergedCellEditorMatSeq = New DevExpress.XtraEditors.CalcEdit

        With m_mergedCellEditorMatSeq
            .Properties.DisplayFormat.FormatType = ReposFNMerMatSeq.DisplayFormat.FormatType
            .Properties.DisplayFormat.FormatString = ReposFNMerMatSeq.DisplayFormat.FormatString

            .Properties.EditFormat.FormatType = ReposFNMerMatSeq.EditFormat.FormatType
            .Properties.EditFormat.FormatString = ReposFNMerMatSeq.EditFormat.FormatString
        End With

        With m_mergedCellEditorSupl
            .Name = "FNHSysSuplIdTo"

            .Properties.ValueMember = RepositoryItemGridLookUpEditFTSuplCode.ValueMember
            .Properties.DisplayMember = RepositoryItemGridLookUpEditFTSuplCode.DisplayMember
            .Properties.PopupFormSize = RepositoryItemGridLookUpEditFTSuplCode.PopupFormSize

            With .Properties.View.OptionsView
                .ColumnAutoWidth = False
                .ShowAutoFilterRow = True
            End With

            With .Properties.View
                .BeginInit()
                .Columns.Clear()

                For Each GrCol As DevExpress.XtraGrid.Columns.GridColumn In RepositoryItemGridLookUpEditFTSuplCode.View.Columns
                    Dim gvvol As New DevExpress.XtraGrid.Columns.GridColumn
                    gvvol.Caption = GrCol.Caption
                    gvvol.FieldName = GrCol.FieldName
                    gvvol.Width = GrCol.Width
                    gvvol.Visible = GrCol.Visible
                    gvvol.VisibleIndex = GrCol.VisibleIndex
                    gvvol.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains

                    gvvol.OptionsColumn.AllowEdit = False
                    gvvol.OptionsColumn.ReadOnly = True
                    gvvol.OptionsColumn.AllowMerge = DefaultBoolean.False
                    gvvol.OptionsColumn.AllowSort = DefaultBoolean.False

                    .Columns.Add(gvvol)
                Next
                .EndInit()
            End With

            AddHandler .QueryPopUp, AddressOf RepositoryItemGridLookUpEditRMSize_QueryPopUp
        End With

        With m_mergedCellEditorMainMat
            .Name = "FNHSysMainMatIdTo"

            .Properties.ValueMember = RepositoryItemGridLookUpEditMainMatCode.ValueMember
            .Properties.DisplayMember = RepositoryItemGridLookUpEditMainMatCode.DisplayMember
            .Properties.PopupFormSize = RepositoryItemGridLookUpEditMainMatCode.PopupFormSize

            With .Properties.View.OptionsView
                .ColumnAutoWidth = False
                .ShowAutoFilterRow = True
            End With

            With .Properties.View
                .BeginInit()
                .Columns.Clear()

                For Each GrCol As DevExpress.XtraGrid.Columns.GridColumn In RepositoryItemGridLookUpEditMainMatCode.View.Columns
                    Dim gvvol As New DevExpress.XtraGrid.Columns.GridColumn
                    gvvol.Caption = GrCol.Caption
                    gvvol.FieldName = GrCol.FieldName
                    gvvol.Width = GrCol.Width
                    gvvol.Visible = GrCol.Visible
                    gvvol.VisibleIndex = GrCol.VisibleIndex
                    gvvol.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains

                    gvvol.OptionsColumn.AllowEdit = False
                    gvvol.OptionsColumn.ReadOnly = True
                    gvvol.OptionsColumn.AllowMerge = DefaultBoolean.False
                    gvvol.OptionsColumn.AllowSort = DefaultBoolean.False

                    .Columns.Add(gvvol)
                Next
                .EndInit()
            End With
            AddHandler .QueryPopUp, AddressOf RepositoryItemGridLookUpEditRMSize_QueryPopUp
        End With

        With m_mergedCellEditorUnit
            .Name = "FNHSysUnitIdTo"

            .Properties.ValueMember = RepositoryItemGridLookUpEditFTUnitCode.ValueMember
            .Properties.DisplayMember = RepositoryItemGridLookUpEditFTUnitCode.DisplayMember
            .Properties.PopupFormSize = RepositoryItemGridLookUpEditFTUnitCode.PopupFormSize

            With .Properties.View.OptionsView
                .ColumnAutoWidth = False
                .ShowAutoFilterRow = True
            End With

            With .Properties.View
                .BeginInit()
                .Columns.Clear()

                For Each GrCol As DevExpress.XtraGrid.Columns.GridColumn In RepositoryItemGridLookUpEditFTUnitCode.View.Columns
                    Dim gvvol As New DevExpress.XtraGrid.Columns.GridColumn
                    gvvol.Caption = GrCol.Caption
                    gvvol.FieldName = GrCol.FieldName
                    gvvol.Width = GrCol.Width
                    gvvol.Visible = GrCol.Visible
                    gvvol.VisibleIndex = GrCol.VisibleIndex
                    gvvol.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains

                    gvvol.OptionsColumn.AllowEdit = False
                    gvvol.OptionsColumn.ReadOnly = True
                    gvvol.OptionsColumn.AllowMerge = DefaultBoolean.False
                    gvvol.OptionsColumn.AllowSort = DefaultBoolean.False

                    .Columns.Add(gvvol)
                Next
                .EndInit()
            End With
            AddHandler .QueryPopUp, AddressOf RepositoryItemGridLookUpEditRMSize_QueryPopUp
        End With


        With m_mergedCellEditorCurr
            .Name = "FNHSysCurIdTo"
            .Properties.Buttons(0).Tag = 965

            .Properties.ValueMember = RepositoryItemGridLookUpEditFTCurCode.ValueMember
            .Properties.DisplayMember = RepositoryItemGridLookUpEditFTCurCode.DisplayMember
            .Properties.PopupFormSize = RepositoryItemGridLookUpEditFTCurCode.PopupFormSize

            With .Properties.View.OptionsView
                .ColumnAutoWidth = False
                .ShowAutoFilterRow = True
            End With

            With .Properties.View
                .BeginInit()
                .Columns.Clear()

                For Each GrCol As DevExpress.XtraGrid.Columns.GridColumn In RepositoryItemGridLookUpEditFTCurCode.View.Columns
                    Dim gvvol As New DevExpress.XtraGrid.Columns.GridColumn
                    gvvol.Caption = GrCol.Caption
                    gvvol.FieldName = GrCol.FieldName
                    gvvol.Width = GrCol.Width
                    gvvol.Visible = GrCol.Visible
                    gvvol.VisibleIndex = GrCol.VisibleIndex
                    gvvol.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains

                    gvvol.OptionsColumn.AllowEdit = False
                    gvvol.OptionsColumn.ReadOnly = True
                    gvvol.OptionsColumn.AllowMerge = DefaultBoolean.False
                    gvvol.OptionsColumn.AllowSort = DefaultBoolean.False

                    .Columns.Add(gvvol)
                Next
                .EndInit()
            End With
            AddHandler .QueryPopUp, AddressOf RepositoryItemGridLookUpEditRMSize_QueryPopUp
        End With

        'With m_mergedCellEditorUnit
        '    .Name = "mFabricSize"
        '    .Properties.MaxLength = 50
        'End With

    End Sub


    Private Sub AddNewRow(Optional pmMatSeq As Decimal = 0, Optional FocusLastRow As Boolean = True)

        If Me.ogcmat.DataSource Is Nothing Then
            LoadBOMDetaiil(BOMSysID)
        End If

        With CType(Me.ogcmat.DataSource, DataTable)
            .AcceptChanges()

            If .Select("FNMatSeq=0").Length > 0 Then
                Exit Sub
            End If

            If .Select("FTMainMatCode=''").Length > 0 Then
                Exit Sub
            End If


            Dim MaxSeq As Decimal = 0
            If pmMatSeq = 0 Then
                For Each RxMax As DataRow In .Select("FNMatSeq>0", "FNMatSeq DESC")
                    MaxSeq = Val(RxMax!FNMatSeq.ToString) + 1.0
                    Exit For
                Next
            Else

                Do
                    pmMatSeq = pmMatSeq + 0.01
                Loop Until .Select("FNMatSeq=" & pmMatSeq & "").Length <= 0

                MaxSeq = pmMatSeq

            End If

            If MaxSeq = 0 Then
                MaxSeq = 1
            End If

            Dim dr As DataRow = .NewRow()

            For Each c As DataColumn In .Columns

                Select Case c.ColumnName
                    Case "FNMatSeq"
                        dr.Item(c) = MaxSeq
                    Case "FNSeq"
                        dr.Item(c) = 0
                    Case "FNPart"
                        dr.Item(c) = 1
                    Case "FTStateActive", "FTRunColor", "FTRunSize"
                        dr.Item(c) = "1"
                    Case "FTStateCombination", "FTStateMainMaterial", "FTStateHemNotOptiplan", "FTStateFree", "FTStateMatConfirm", "FTStateDTM", "FTStateNominate", "FTStateSilk", "FTSelect"
                        dr.Item(c) = "0"
                    Case Else
                        Try


                            Select Case c.DataType.ToString
                                Case "System.String"
                                    dr.Item(c) = ""
                                Case Else
                                    dr.Item(c) = 0
                            End Select

                            'If c.DataType = GetType(String) Then
                            '    dr.Item(c) = ""
                            'Else
                            '    dr.Item(c) = 0
                            'End If


                        Catch ex As Exception
                        End Try
                End Select

            Next
            .Rows.Add(dr)
            .AcceptChanges()





        End With
        ogvmat.LeftCoord = 0

        If FocusLastRow Then '
            ogvmat.FocusedRowHandle = ogvmat.RowCount - 1
        End If

        ' ogvmat.FocusedColumn = ogvmat.Columns.ColumnByFieldName("FTMainMatCode")

    End Sub

    Private Sub InsertRow()
        Dim pMatId As Integer = 0
        Dim pSeq As Integer = 0
        Dim pMatSeq As Decimal = 0

        With Me.ogvmat
            pMatId = Val(.GetFocusedRowCellValue("FNHSysMainMatId").ToString)
            pSeq = Val(.GetFocusedRowCellValue("FNSeq").ToString)
            pMatSeq = Decimal.Parse(Format(Val(.GetFocusedRowCellValue("FNMatSeq").ToString), "0.00"))

        End With

        AddNewRow(pMatSeq, False)
    End Sub

    Private Sub DiffMatPart()
        Dim pMatId As Integer = 0
        Dim pSeq As Integer = 0
        Dim pMatSeq As Decimal = 0.0
        Dim pMatpart As Integer = 0

        With Me.ogvmat
            pMatId = Val(.GetFocusedRowCellValue("FNHSysMainMatId").ToString)
            pSeq = Val(.GetFocusedRowCellValue("FNSeq").ToString)
            pMatSeq = Decimal.Parse(Format(Val(.GetFocusedRowCellValue("FNMatSeq").ToString), "0.00"))
            pMatpart = Val(.GetFocusedRowCellValue("FNPart").ToString)
        End With

        Call DifferentMaterialPart(BOMSysID, pMatId, pSeq, pMatSeq, pMatpart)

    End Sub

    Private Sub ImportColowayAndSizeBreakdown()

        If HI.MG.ShowMsg.mConfirmProcess("You want to import Colorway And Size Breakdown From Ooder ?", 2204130457) = True Then
            Call ImportColorSizeFromOrder(0, 0, "", 0, "", 0)
        End If
    End Sub

    Private Sub AddNewColorway()
        HI.ST.Lang.SP_SETxLanguage(_wNewColorway)
        With _wNewColorway
            .FTColorway.Text = ""
            .FTColorway.Properties.Tag = 0
            .ProcNew = False
            .ShowDialog()

            If .ProcNew Then
                Dim _NewColorWay As String = .FTColorway.Text
                Dim _NewColorID As Integer = Val(.FTColorway.Properties.Tag.ToString)


                If _NewColorID > 0 And _NewColorWay <> "" Then
                    Call ImportColorSizeFromOrder(1, _NewColorID, _NewColorWay, 0, "", 0)
                End If


            End If

        End With
    End Sub

    Private Sub DeleteColorWay()

        With Me.ogvmatcolor
            If .FocusedRowHandle < 0 Then Exit Sub
            If Microsoft.VisualBasic.Left(.FocusedColumn.Name.ToString.ToUpper, "gViewColor".Length) = "gViewColor".ToUpper Then
                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบ Size ใช่หรือไม่ ?", 1406030077, .FocusedColumn.Caption) Then
                    Dim ColColorField As String = .FocusedColumn.FieldName.ToString
                    Dim ColColorFieldTH As String = .FocusedColumn.FieldName.ToString & "TH"
                    Dim ColColorFieldEN As String = .FocusedColumn.FieldName.ToString & "EN"
                    Dim ColColor As String = .FocusedColumn.Caption

                    If ColColor <> "" Then
                        If ImportColorSizeFromOrder(1, 0, ColColor, 0, "", 1) Then

                            Try
                                ogvmatcolor.Columns.Remove(ogvmatcolor.Columns.ColumnByFieldName(ColColorField))
                                ogvmatcolor.Columns.Remove(ogvmatcolor.Columns.ColumnByFieldName(ColColorFieldTH))
                                ogvmatcolor.Columns.Remove(ogvmatcolor.Columns.ColumnByFieldName(ColColorFieldEN))

                                With CType(Me.ogcmatcolor.DataSource, DataTable)
                                    .BeginInit()
                                    .Columns.Remove(ColColorField)
                                    .Columns.Remove(ColColorFieldTH)
                                    .Columns.Remove(ColColorFieldEN)
                                    .EndInit()
                                    .AcceptChanges()
                                End With

                                ogvmatcolornote.Columns.Remove(ogvmatcolornote.Columns.ColumnByFieldName(ColColorField))

                                With CType(Me.ogcmatcolornote.DataSource, DataTable)
                                    .BeginInit()
                                    .Columns.Remove(ColColorField)

                                    .EndInit()
                                    .AcceptChanges()
                                End With

                                ogvmatsilkcolor.Columns.Remove(ogvmatsilkcolor.Columns.ColumnByFieldName(ColColorField))

                                With CType(Me.ogcmatsilkcolor.DataSource, DataTable)
                                    .BeginInit()
                                    .Columns.Remove(ColColorField)

                                    .EndInit()
                                    .AcceptChanges()
                                End With

                            Catch ex As Exception
                            End Try

                        End If
                    End If


                End If
            End If

        End With
    End Sub

    Private Sub ChangeColorName()
        With Me.ogvmatcolor
            If .FocusedRowHandle < 0 Then Exit Sub
            If Microsoft.VisualBasic.Left(.FocusedColumn.Name.ToString.ToUpper, "gViewColor".Length) = "gViewColor".ToUpper Then


                Dim ColColorField As String = .FocusedColumn.FieldName.ToString
                Dim ColColorFieldTH As String = .FocusedColumn.FieldName.ToString & "TH"
                Dim ColColorFieldEN As String = .FocusedColumn.FieldName.ToString & "EN"
                Dim ColColor As String = .FocusedColumn.Caption


                Dim pCode As String = ""
                Dim pNameTH As String = ""
                Dim pNameEN As String = ""
                Dim mlId As Integer = 0
                Dim pFiledName As String = ""
                Dim pColorWay As String = .FocusedColumn.Caption.Trim()

                pFiledName = "FNHSysRawMatColorId"

                mlId = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, ColColorField).ToString))
                pCode = "" & .GetRowCellDisplayText(.FocusedRowHandle, ColColorField).ToString
                pNameTH = .GetRowCellValue(.FocusedRowHandle, ColColorFieldTH).ToString
                pNameEN = .GetRowCellValue(.FocusedRowHandle, ColColorFieldEN).ToString

                Dim pSeq As Integer = Val(.GetFocusedRowCellValue("FNSeq").ToString)

                Dim _Qry As String = ""
                Dim _ItemCode As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTMainMatCode").ToString
                Dim _State As Boolean = False
                Dim _FTOrderNo As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTOrderNo").ToString

                '_Qry = " SELECT   TOP 1   '1' AS FTState"
                '_Qry &= vbCrLf & "   FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPR AS A WITH(NOLOCK)  INNER JOIN"
                '_Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS M WITH(NOLOCK) ON A.FNHSysMerMatId = M.FNHSysMainMatId"
                '_Qry &= vbCrLf & "   WHERE        (M.FTMainMatCode = N'" & HI.UL.ULF.rpQuoted(_ItemCode) & "')"
                '_Qry &= vbCrLf & "   AND  A.FNHSysRawMatColorId=" & Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, ColColorField).ToString)) & " "
                '_Qry &= vbCrLf & "   AND  A.FNHSysStyleId=" & StyleID & " "
                '_Qry &= vbCrLf & "   AND  A.FNHSysSeasonId=" & SeasonID & " "

                'If _FTOrderNo <> "ALL" And _FTOrderNo <> "ทั้งหมด" Then
                '    _Qry &= vbCrLf & "   AND    (A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_FTOrderNo) & "') "
                'End If

                '_State = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") <> "")

                _State = False

                With _wChangeDesc

                    .FTRawMatColorNameTH.Text = "" & ogvmatcolor.GetRowCellValue(ogvmatcolor.FocusedRowHandle, ColColorFieldTH).ToString
                    .FTRawMatColorNameEN.Text = "" & ogvmatcolor.GetRowCellValue(ogvmatcolor.FocusedRowHandle, ColColorFieldEN).ToString
                    .FTRawMatColorNameTH.Properties.ReadOnly = _State
                    .FTRawMatColorNameEN.Properties.ReadOnly = _State

                    .ProcNew = False
                    .ShowDialog()

                    If .ProcNew Then

                        pNameTH = .FTRawMatColorNameTH.Text.Trim
                        pNameEN = .FTRawMatColorNameEN.Text.Trim


                        pBOMListUpdate.Clear()

                        Dim pBOM As New BOMData
                        pBOM.FieldName = pFiledName
                        pBOM.FieldValue = mlId
                        pBOM.FieldValueTH = pNameTH
                        pBOM.FieldValueEN = pNameEN


                        pBOM.FieldDataType = BOMDataType.DataInteger
                        pBOMListUpdate.Add(pBOM)

                        If UpdateBOMColor(BOMSysID, pSeq, pColorWay, pBOMListUpdate, True) = False Then



                        Else

                            With ogvmatcolor
                                .SetFocusedRowCellValue(ColColorFieldTH, pNameTH)
                                .SetFocusedRowCellValue(ColColorFieldEN, pNameEN)


                            End With

                        End If

                        pBOMListUpdate.Clear()



                        CType(Me.ogcmatcolor.DataSource, DataTable).AcceptChanges()
                    End If
                End With


            End If

        End With
    End Sub

    Private Sub ChangeColorWay()
        With Me.ogvmatcolor
            If .FocusedRowHandle < 0 Then Exit Sub
            If Microsoft.VisualBasic.Left(.FocusedColumn.Name.ToString.ToUpper, "gViewColor".Length) = "gViewColor".ToUpper Then


                Dim ColColorField As String = .FocusedColumn.FieldName.ToString
                Dim ColColorFieldTH As String = .FocusedColumn.FieldName.ToString & "TH"
                Dim ColColorFieldEN As String = .FocusedColumn.FieldName.ToString & "EN"
                Dim ColColor As String = .FocusedColumn.Caption


                Dim pCode As String = ""
                Dim pNameTH As String = ""
                Dim pNameEN As String = ""
                Dim mlId As Integer = 0
                Dim pFiledName As String = ""
                Dim pColorWay As String = .FocusedColumn.Caption.Trim()

                pFiledName = "FNHSysRawMatColorId"

                mlId = Integer.Parse(Val("" & .GetRowCellValue(.FocusedRowHandle, ColColorField).ToString))
                pCode = "" & .GetRowCellDisplayText(.FocusedRowHandle, ColColorField).ToString
                pNameTH = .GetRowCellValue(.FocusedRowHandle, ColColorFieldTH).ToString
                pNameEN = .GetRowCellValue(.FocusedRowHandle, ColColorFieldEN).ToString

                Dim pSeq As Integer = Val(.GetFocusedRowCellValue("FNSeq").ToString)

                HI.ST.Lang.SP_SETxLanguage(_wChangeColorway)

                With _wChangeColorway
                    .FTColorwayFrom.Text = pColorWay
                    .FTColorway.Text = ""

                    .ProcNew = False
                    .ShowDialog()

                    If .ProcNew Then
                        Dim _NewColorWay As String = .FTColorway.Text
                        Dim _NewColorID As Integer = Val(.FTColorway.Properties.Tag.ToString)


                        If _NewColorID > 0 And _NewColorWay <> "" Then
                            If ImportColorSizeFromOrder(1, _NewColorID, _NewColorWay, 0, "", 2, pColorWay) Then



                                'Try
                                '    ogvmatcolor.Columns.Remove(ogvmatcolor.Columns.ColumnByFieldName(ColColorField))
                                '    ogvmatcolor.Columns.Remove(ogvmatcolor.Columns.ColumnByFieldName(ColColorFieldTH))
                                '    ogvmatcolor.Columns.Remove(ogvmatcolor.Columns.ColumnByFieldName(ColColorFieldEN))

                                '    With ogvmatcolor
                                '        .BeginInit()
                                '        .Columns.ColumnByFieldName(ColColorField).Caption = _NewColorWay
                                '        .Columns.ColumnByFieldName(ColColorField).Name = .Columns.ColumnByFieldName(ColColorField).Name.Replace(pColorWay, _NewColorWay)
                                '        .Columns.ColumnByFieldName(ColColorFieldTH).Name = .Columns.ColumnByFieldName(ColColorField).Name.Replace(pColorWay, _NewColorWay)
                                '        .Columns.ColumnByFieldName(ColColorFieldEN).Name = .Columns.ColumnByFieldName(ColColorField).Name.Replace(pColorWay, _NewColorWay)
                                '        .EndInit()

                                '    End With


                                '    With ogvmatcolornote
                                '        .BeginInit()
                                '        .Columns.ColumnByFieldName(ColColorField).Caption = _NewColorWay
                                '        .Columns.ColumnByFieldName(ColColorField).Name = .Columns.ColumnByFieldName(ColColorField).Name.Replace(pColorWay, _NewColorWay)

                                '        .EndInit()

                                '    End With


                                '    With ogvmatsilkcolor
                                '        .BeginInit()
                                '        .Columns.ColumnByFieldName(ColColorField).Caption = _NewColorWay
                                '        .Columns.ColumnByFieldName(ColColorField).Name = .Columns.ColumnByFieldName(ColColorField).Name.Replace(pColorWay, _NewColorWay)

                                '        .EndInit()

                                '    End With




                                'Catch ex As Exception
                                'End Try


                            End If


                        End If


                    End If


                End With

            End If

        End With
    End Sub

    Private Sub AddNewSizeBreakdown()
        HI.ST.Lang.SP_SETxLanguage(_wNewSize)
        With _wNewSize
            .FTSizeBreakDown.Text = ""
            .FTSizeBreakDown.Properties.Tag = 0
            .ProcNew = False
            .ShowDialog()

            If .ProcNew Then

                Dim _NewSize As String = .FTSizeBreakDown.Text
                Dim _NewSizeID As Integer = Val(.FTSizeBreakDown.Properties.Tag.ToString)

                If _NewSizeID > 0 And _NewSize <> "" Then
                    Call ImportColorSizeFromOrder(2, 0, "", _NewSizeID, _NewSize, 0)
                End If

            End If

        End With
    End Sub

    Private Sub DeleteSizeBreakdown()
        With Me.ogvmatsize
            If .FocusedRowHandle < 0 Then Exit Sub
            If Microsoft.VisualBasic.Left(.FocusedColumn.Name.ToString.ToUpper, "gViewSize".Length) = "gViewSize".ToUpper Then
                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบ Size ใช่หรือไม่ ?", 1406030077, .FocusedColumn.Caption) Then
                    Dim ColSizeField As String = .FocusedColumn.FieldName.ToString
                    Dim ColSize As String = .FocusedColumn.Caption

                    If ColSize <> "" Then

                        If ImportColorSizeFromOrder(2, 0, "", 99, ColSize, 1) Then

                            Try
                                ogvmatsize.Columns.Remove(ogvmatsize.Columns.ColumnByFieldName(ColSizeField))

                                With CType(Me.ogcmatsize.DataSource, DataTable)
                                    .BeginInit()
                                    .Columns.Remove(ColSizeField)
                                    .EndInit()
                                    .AcceptChanges()
                                End With

                            Catch ex As Exception
                            End Try

                        End If

                    End If

                End If

            End If

        End With
    End Sub

    Private Sub InterchangeChart()
        Dim cmdstring As String = ""

        cmdstring = "  EXEC " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_GETBOMGARMENT_INTERCHANGE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & BOMSysID & " "

        Call LoadBOMDetaiil(BOMSysID)

    End Sub

    Private Sub RemoveRow()
        Dim pMatId As Integer = 0
        Dim pSeq As Integer = 0
        Dim pMatSeq As Decimal = 0.0
        Dim pMatPart As Integer = 0
        Dim _Msg As String = ""
        Dim MatCode As String = ""



        Dim dt As New DataTable
        dt.Columns.Add("FNHSysBomId", GetType(Integer))
        dt.Columns.Add("NHSysMainMatId", GetType(Integer))
        dt.Columns.Add("FNSeq", GetType(Integer))



        With CType(ogcmat.DataSource, DataTable)
            .AcceptChanges()

            For Each R As DataRow In .Select("FTSelect='1'")


                dt.Rows.Add(BOMSysID, Val(R!FNHSysMainMatId.ToString), Val(R!FNSeq.ToString))


                pMatId = Val(R!FNHSysMainMatId.ToString)
                pSeq = Val(R!FNSeq.ToString)
                pMatSeq = Val(R!FNMatSeq.ToString)

                MatCode = R!FTMainMatCode.ToString
                pMatPart = Val(R!FNPart.ToString)

                If _Msg = "" Then
                    _Msg = " Item Code : " & MatCode & "     Sequence No : " & pMatSeq.ToString & "     Part No : " & pMatPart.ToString & " "

                Else
                    _Msg &= vbCrLf & " Item Code : " & MatCode & "     Sequence No : " & pMatSeq.ToString & "     Part No : " & pMatPart.ToString & " "
                End If

            Next


        End With

        If dt.Rows.Count > 0 Then

            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, _Msg) = True Then

                Call DeletePart(BOMSysID, dt)

            End If
        End If

        dt.Dispose()
    End Sub
    Private Function ImportColorSizeFromOrder(pLoadType As Integer, pColorID As Integer, pColor As String, pSizeID As Integer, pSize As String, pState As Integer, Optional pOldColor As String = "") As Boolean

        Dim CountProc As Integer = 0
        Dim Spls As New HI.TL.SplashScreen("Processing... Color Size Breakdown. Please wait.")


        Dim CmdString As String = "EXEC " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_GETBOMGARMENT_IMPORTCOLORSIZE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & BOMSysID & "," & pColorID & ",'" & HI.UL.ULF.rpQuoted(pColor) & "'," & pSizeID & ",'" & HI.UL.ULF.rpQuoted(pSize) & "'," & pState & ",'" & HI.UL.ULF.rpQuoted(pOldColor) & "'"
        CountProc = Val(HI.Conn.SQLConn.GetField(CmdString, Conn.DB.DataBaseName.DB_MERCHAN, "0"))

        Try
            If CountProc > 0 Then

                If pState = 0 Or pOldColor <> "" Then

                    LoadBOMDetaiil(BOMSysID, pLoadType)

                End If

            End If

        Catch ex As Exception
        End Try

        Spls.Close()

        Return (CountProc > 0)

    End Function

    Private Sub DifferentMaterialPart(BOMSysID As Integer, pMatId As Integer, pSeq As Integer, pMatSeq As Integer, pMatPart As Integer)

        Dim Spls As New HI.TL.SplashScreen("Differentting Material Part ... Please wait.")


        Dim CmdString As String = "EXEC " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_GETBOMGARMENT_DIFFPART '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & BOMSysID & "," & pMatId & "," & pSeq & "," & pMatSeq & ""



        Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(CmdString, Conn.DB.DataBaseName.DB_MERCHAN)
        Dim pProcCount As Integer = 0
        Dim pMaxSeq As Integer = 0
        Dim pPart As Integer = 0

        For Each R As DataRow In dt.Rows

            pProcCount = Val(R!FNProcCount.ToString)
            pMaxSeq = Val(R!FNMaxSeq.ToString)
            pPart = Val(R!FNPart.ToString)


        Next

        dt.Dispose()


        Try
            If pProcCount > 0 Then
                With CType(Me.ogcmat.DataSource, DataTable)
                    .BeginInit()

                    For Each Rm As DataRow In .Select("FNHSysMainMatId =" & pMatId & " AND FNPart > " & pMatPart & " and FNSeq <>" & pSeq & " ")
                        Rm!FNPart = Val(Rm!FNPart) + 1
                    Next
                    .EndInit()

                    If .Select("FNSeq =" & pSeq & " ").Length > 0 Then
                        Dim dr As DataRow = .NewRow()

                        For Each Rm As DataRow In .Select("FNSeq =" & pSeq & " ")
                            For Each c As DataColumn In .Columns

                                Select Case c.ColumnName
                                    Case "FNSeq"
                                        dr.Item(c) = pMaxSeq
                                    Case "FNPart"
                                        dr.Item(c) = pPart

                                    Case "FTStateMatConfirm"
                                        dr.Item(c) = "0"
                                    Case Else
                                        Try
                                            dr.Item(c) = Rm.Item(c)
                                        Catch ex As Exception
                                        End Try
                                End Select

                            Next
                            Exit For
                        Next


                        .Rows.InsertAt(dr, .Rows.Count + 1)

                        .AcceptChanges()
                    End If

                End With

                With CType(Me.ogcmatcolor.DataSource, DataTable)

                    .BeginInit()

                    For Each Rm As DataRow In .Select("FNHSysMainMatId =" & pMatId & " AND FNPart > " & pMatPart & " and FNSeq <>" & pSeq & " ")
                        Rm!FNPart = Val(Rm!FNPart) + 1
                    Next
                    .EndInit()


                    If .Select("FNSeq =" & pSeq & " ").Length > 0 Then
                        Dim dr As DataRow = .NewRow()

                        For Each Rm As DataRow In .Select("FNSeq =" & pSeq & " ")
                            For Each c As DataColumn In .Columns

                                Select Case c.ColumnName
                                    Case "FNSeq"
                                        dr.Item(c) = pMaxSeq
                                    Case "FNPart"
                                        dr.Item(c) = pPart


                                    Case Else
                                        Try
                                            dr.Item(c) = Rm.Item(c)
                                        Catch ex As Exception
                                        End Try
                                End Select

                            Next
                            Exit For
                        Next


                        .Rows.InsertAt(dr, .Rows.Count + 1)

                        .AcceptChanges()
                    End If

                End With

                With CType(Me.ogcmatsize.DataSource, DataTable)

                    .BeginInit()

                    For Each Rm As DataRow In .Select("FNHSysMainMatId =" & pMatId & " AND FNPart > " & pMatPart & " and FNSeq <>" & pSeq & " ")
                        Rm!FNPart = Val(Rm!FNPart) + 1
                    Next
                    .EndInit()

                    If .Select("FNSeq =" & pSeq & " ").Length > 0 Then
                        Dim dr As DataRow = .NewRow()

                        For Each Rm As DataRow In .Select("FNSeq =" & pSeq & " ")
                            For Each c As DataColumn In .Columns

                                Select Case c.ColumnName
                                    Case "FNSeq"
                                        dr.Item(c) = pMaxSeq
                                    Case "FNPart"
                                        dr.Item(c) = pPart


                                    Case Else
                                        Try
                                            dr.Item(c) = Rm.Item(c)
                                        Catch ex As Exception
                                        End Try
                                End Select

                            Next
                            Exit For
                        Next


                        .Rows.InsertAt(dr, .Rows.Count + 1)

                        .AcceptChanges()
                    End If

                End With

                With CType(Me.ogcmatcolornote.DataSource, DataTable)

                    .BeginInit()

                    For Each Rm As DataRow In .Select("FNHSysMainMatId =" & pMatId & " AND FNPart > " & pMatPart & " and FNSeq <>" & pSeq & " ")
                        Rm!FNPart = Val(Rm!FNPart) + 1
                    Next
                    .EndInit()

                    If .Select("FNSeq =" & pSeq & " ").Length > 0 Then
                        Dim dr As DataRow = .NewRow()

                        For Each Rm As DataRow In .Select("FNSeq =" & pSeq & " ")
                            For Each c As DataColumn In .Columns

                                Select Case c.ColumnName
                                    Case "FNSeq"
                                        dr.Item(c) = pMaxSeq
                                    Case "FNPart"
                                        dr.Item(c) = pPart


                                    Case Else
                                        Try
                                            dr.Item(c) = Rm.Item(c)
                                        Catch ex As Exception
                                        End Try
                                End Select

                            Next
                            Exit For
                        Next


                        .Rows.InsertAt(dr, .Rows.Count + 1)

                        .AcceptChanges()
                    End If

                End With

                With CType(Me.ogcmatsilkcolor.DataSource, DataTable)

                    .BeginInit()

                    For Each Rm As DataRow In .Select("FNHSysMainMatId =" & pMatId & " AND FNPart > " & pMatPart & " and FNSeq <>" & pSeq & " ")
                        Rm!FNPart = Val(Rm!FNPart) + 1
                    Next
                    .EndInit()

                    If .Select("FNSeq =" & pSeq & " ").Length > 0 Then
                        Dim dr As DataRow = .NewRow()

                        For Each Rm As DataRow In .Select("FNSeq =" & pSeq & " ")
                            For Each c As DataColumn In .Columns

                                Select Case c.ColumnName
                                    Case "FNSeq"
                                        dr.Item(c) = pMaxSeq
                                    Case "FNPart"
                                        dr.Item(c) = pPart


                                    Case Else
                                        Try
                                            dr.Item(c) = Rm.Item(c)
                                        Catch ex As Exception
                                        End Try
                                End Select

                            Next
                            Exit For
                        Next


                        .Rows.InsertAt(dr, .Rows.Count + 1)

                        .AcceptChanges()
                    End If

                End With

            End If

        Catch ex As Exception

        End Try

        Spls.Close()


    End Sub

    Private Sub DeletePart(BOMSysID As Integer, dt As DataTable)

        Dim Spls As New HI.TL.SplashScreen("Differentting Material Part ... Please wait.")


        Dim pValue As New List(Of HI.Conn.SQLConn.StoreTableParameter)
        Dim pTableValue As New List(Of HI.Conn.SQLConn.StoreTableParameterTable)

        Dim pV1 As New HI.Conn.SQLConn.StoreTableParameter
        pV1.ParameterName = "@User"
        pV1.ParameterValue = HI.ST.UserInfo.UserName

        pValue.Add(pV1)

        Dim pV2 As New HI.Conn.SQLConn.StoreTableParameter
        pV2.ParameterName = "@BomId"
        pV2.ParameterValue = BOMSysID.ToString
        pValue.Add(pV2)


        Dim pVT As New HI.Conn.SQLConn.StoreTableParameterTable
        pVT.ParameterName = "@tblBOMSeq"
        pVT.ParameterValue = dt
        pTableValue.Add(pVT)


        Dim dtproc As DataTable = HI.Conn.SQLConn.GetDataTableExecuteStoredProcedureTable(pValue, "USP_GETBOMGARMENT_REMOVEMAT", pTableValue, Conn.DB.DataBaseName.DB_MERCHAN)

        Dim pProcCount As Boolean = False
        pValue.Clear()
        pTableValue.Clear()

        If dtproc.Rows.Count > 0 Then
            pProcCount = (Val(dtproc.Rows(0)!FNProcCount.ToString) > 0)
        End If
        dtproc.Dispose()
        Try
            If pProcCount = True Then
                LoadBOMDetaiil(BOMSysID)
            End If

        Catch ex As Exception

        End Try

        Spls.Close()


    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            StateLoad = True

            Me.ogvorder.OptionsSelection.MultiSelect = True
            Me.ogvorder.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect

            InitGrid()

            Call SetFilerColumn()

            Call otb_SelectedPageChanged(otb, New TabPageChangedEventArgs(Nothing, otpmaterial))


            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, ogvmat)
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, ogvmatcolor)
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, ogvmatsize)
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, ogvmatcolornote)
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, ogvmatsilkcolor)


            Me.ogcmat.ContextMenuStrip = _MContextMenuStripGrid
            Me.ogcmatcolor.ContextMenuStrip = _MContextMenuStripGrid
            Me.ogcmatcolornote.ContextMenuStrip = _MContextMenuStripGrid
            Me.ogcmatsilkcolor.ContextMenuStrip = _MContextMenuStripGrid
            Me.ogcmatsize.ContextMenuStrip = _MContextMenuStripGrid

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs)

        HI.TL.HandlerControl.ClearControl(Me)

    End Sub

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs)

        Me.Close()

    End Sub

    Private Sub wBOMListingAdd_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Try
            Dim cmdstring As String = ""

            cmdstring = " Delete BOM "
            cmdstring &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_UserEidt  AS BOM  "
            cmdstring &= vbCrLf & " WHERE BOM.FTUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "

            HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub wBOMListingAdd_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Me.Dispose()
    End Sub


    Private lastRowHandle As Integer = GridControl.InvalidRowHandle
    Private lastColumn As GridColumn = Nothing
    Private ttInfo As ToolTipControlInfo = Nothing

    Private Sub objToolTipController_GetActiveObjectInfo(sender As Object, e As ToolTipControllerGetActiveObjectInfoEventArgs) Handles objToolTipController.GetActiveObjectInfo
        Try
            If e.Info Is Nothing AndAlso e.SelectedControl Is ogcmatcolor Then
                Dim view As GridView = TryCast(ogcmatcolor.FocusedView, GridView)
                Dim info As GridHitInfo = view.CalcHitInfo(e.ControlMousePosition)

                If info.InRowCell AndAlso (info.RowHandle <> lastRowHandle OrElse info.Column IsNot lastColumn) Then
                    lastRowHandle = info.RowHandle
                    lastColumn = info.Column
                    Dim text As String = view.GetRowCellDisplayText(info.RowHandle, info.Column)
                    Dim cellKey As String = info.RowHandle.ToString() & " - " & info.Column.ToString()

                    If Microsoft.VisualBasic.Left(info.Column.Name, "gViewColor".Length).ToUpper = "gViewColor".ToUpper And text.Trim <> "" Then

                        Dim Col5 As String = info.Column.FieldName & "TH"
                        Dim Col6 As String = info.Column.FieldName & "EN"

                        Try
                            text &= vbCrLf & "Name (TH) :" & view.GetRowCellValue(info.RowHandle, Col5)
                            text &= vbCrLf & "Name (EN) :" & view.GetRowCellValue(info.RowHandle, Col6)
                        Catch ex As Exception
                        End Try

                    End If

                    ttInfo = New DevExpress.Utils.ToolTipControlInfo(cellKey, text)

                End If

                If ttInfo IsNot Nothing Then
                    e.Info = ttInfo
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryItemPopupPositionPart_QueryPopUp(sender As Object, e As CancelEventArgs) Handles RepositoryItemPopupPositionPart.QueryPopUp
        Try
            Dim cmdstring As String = ""
            Dim pSeq As Integer = Val(Me.ogvmat.GetFocusedRowCellValue("FNSeq").ToString)
            Dim pPart As String = Me.ogvmat.GetFocusedRowCellValue("FTPart").ToString

            cmdstring = "Select ISNULL(B.FTState,'0') AS FTSelect ,FNHSysPartId,FTPartCode"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                cmdstring &= vbCrLf & ",FTPartNameTH AS FTPartName"
            Else
                cmdstring &= vbCrLf & ",FTPartNameEN AS FTPartName"
            End If

            cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS A With(NOLOCK) "

            cmdstring &= vbCrLf & "Outer Apply  (select top 1 '1' AS FTState FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Part AS B WITH(NOLOCK) WHERE  B.FNHSysBomId =" & BOMSysID & " AND B.FNSeq=" & pSeq & " And B.FNHSysPartId= A.FNHSysPartId )  As B "

            cmdstring &= vbCrLf & " WHERE ISNULL(FTStateActive,'')='1'  "
            cmdstring &= vbCrLf & " ORDER BY FTPartCode "

            Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)


            ogvpart.ClearColumnsFilter()
            ogvpart.ActiveFilter.Clear()
            ogvpart.OptionsView.ShowAutoFilterRow = True

            Me.ogcpart.DataSource = dt.Copy
            ogvpart.Columns.ColumnByFieldName("FTSelect").Width = 40
            ogvpart.Columns.ColumnByFieldName("FTPartName").Width = 150

            Me.FTPart.Text = pPart

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryItemPopupPositionPart_QueryCloseUp(sender As Object, e As CancelEventArgs) Handles RepositoryItemPopupPositionPart.QueryCloseUp

        Try
            With Me.ogvmat
                If .FocusedRowHandle < 0 Then
                    Exit Sub
                End If

                ogvpart.FocusedColumn = ogvpart.Columns.ColumnByFieldName("FTPartName")

                Dim pSeq As Integer = Val(Me.ogvmat.GetFocusedRowCellValue("FNSeq").ToString)

                Me.FTPart.Focus()
                Me.FTPart.SelectAll()

                Dim _PartName As String = ""
                Dim _PartIDKey As String = ""
                Dim cmdstring As String = ""

                cmdstring = "declare @Rec int =0  declare @Tab AS Table (FNHSysPartId int )"

                With CType(Me.ogcpart.DataSource, DataTable)
                    .AcceptChanges()
                    For Each R As DataRow In .Select("FTSelect='1'")

                        If _PartName = "" Then
                            _PartName = R!FTPartName.ToString

                            cmdstring &= vbCrLf & "   insert into @Tab(FNHSysPartId) "
                            cmdstring &= vbCrLf & " Values(" & Val(R!FNHSysPartId.ToString()) & ") "

                        Else

                            _PartName = _PartName & "," & R!FTPartName.ToString
                            cmdstring &= vbCrLf & " ,(" & Val(R!FNHSysPartId.ToString()) & ") "

                        End If

                    Next

                End With

                If Me.FTPart.Text <> "" Then

                    If _PartName = "" Then
                        _PartName = Me.FTPart.Text
                    Else
                        _PartName = Me.FTPart.Text & ":" & _PartName
                    End If

                End If


                cmdstring &= vbCrLf & " INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Part (FTInsUser, FDInsDate, FTInsTime, FNHSysBomId, FNSeq, FNHSysPartId) "
                cmdstring &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB & ""
                cmdstring &= vbCrLf & "," & BOMSysID & ""
                cmdstring &= vbCrLf & "," & pSeq & ""
                cmdstring &= vbCrLf & ",A.FNHSysPartId"
                cmdstring &= vbCrLf & "    FROM  @Tab AS A  "
                cmdstring &= vbCrLf & "   OUTER APPLY (SELECT TOP 1 '1' AS FTState FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Part AS B  WHERE    B.FNHSysBomId = " & BOMSysID & " And B.FNSeq = " & pSeq & " AND  B.FNHSysPartId = A.FNHSysPartId ) B  "
                cmdstring &= vbCrLf & "     WHERE   ISNULL(B.FTState,'') ='' "
                cmdstring &= vbCrLf & "  DELETE A "
                cmdstring &= vbCrLf & "    FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Part AS A  "
                cmdstring &= vbCrLf & "   OUTER APPLY (SELECT TOP 1 '1' AS FTState FROM  @Tab AS B  WHERE  B.FNHSysPartId = A.FNHSysPartId ) B  "
                cmdstring &= vbCrLf & "     WHERE  A.FNHSysBomId = " & BOMSysID & " And A.FNSeq = " & pSeq & "  AND ISNULL(B.FTState,'') ='' "
                cmdstring &= vbCrLf & " UPDATE  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat   SET  "
                cmdstring &= vbCrLf & " FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                cmdstring &= vbCrLf & " ,FTPart ='" & HI.UL.ULF.rpQuoted(Me.FTPart.Text.Trim) & "' "
                cmdstring &= vbCrLf & " ,FTComponent = CASE WHEN ISNULL(FTComponent,'') ='' THEN   '" & HI.UL.ULF.rpQuoted(_PartName) & "' ELSE FTComponent END "
                cmdstring &= vbCrLf & " WHERE FNHSysBomId=" & BOMSysID & " "
                cmdstring &= vbCrLf & " AND  FNSeq=" & pSeq & "  "
                cmdstring &= vbCrLf & " SET @Rec = @@ROWCOUNT  "
                cmdstring &= vbCrLf & "  Select  Top 1   @Rec AS FNState, TR.FTPart,TR.FTInsUser, TR.FDInsDate, TR.FTInsTime,TR.FNMatSeq ,TR.FTUpdUser, TR.FDUpdDate, TR.FTUpdTime,TR.FTComponent "
                cmdstring &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat  AS TR  WITH(NOLOCK)  "
                cmdstring &= vbCrLf & "  WHERE TR.FNHSysBomId=" & BOMSysID & " "
                cmdstring &= vbCrLf & "  AND  TR.FNSeq=" & pSeq & "  "


                Dim mdt As DataTable
                mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                If mdt.Rows.Count > 0 Then

                    If Val(mdt.Rows(0)!FNState.ToString) > 0 Then

                        With Me.ogvmat

                            For Each Rxp As DataRow In mdt.Rows

                                .SetFocusedRowCellValue("FTUpdUser", Rxp!FTUpdUser.ToString)
                                .SetFocusedRowCellValue("FDUpdDate", Rxp!FDUpdDate.ToString)
                                .SetFocusedRowCellValue("FTUpdTime", Rxp!FTUpdTime.ToString)
                                .SetFocusedRowCellValue("FTPart", Rxp!FTPart.ToString)
                                .SetFocusedRowCellValue("FTComponent", Rxp!FTComponent.ToString)

                            Next

                        End With

                        StateReLoad = True

                        mdt.Dispose()

                        .SetFocusedRowCellValue("FTPart", Me.FTPart.Text)
                        .SetFocusedRowCellValue("FTPositionPartName", _PartName)
                    Else
                        mdt.Dispose()

                    End If


                Else
                    mdt.Dispose()

                End If

            End With
        Catch ex As Exception
        End Try


    End Sub

    Private Sub RepositoryItemPopupOrder_QueryPopUp(sender As Object, e As CancelEventArgs) Handles RepositoryItemPopupOrder.QueryPopUp
        Try
            Me.ogdlistorder.DataSource = Nothing
            ockselectorderall.Checked = False

            Dim cmdstring As String = ""
            Dim pSeq As Integer = Val(Me.ogvmat.GetFocusedRowCellValue("FNSeq").ToString)

            cmdstring = "Select ISNULL(B.FTState,'0') AS FTSelect ,A.FTOrderNo,C1.FTBuyCode"



            cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.V_OrderProdAndSMPAll AS A With(NOLOCK) "

            If FNStateBomOrder.SelectedIndex = 1 Then

                cmdstring &= vbCrLf & " INNER JOIN ( SELECT DISTINCT FTOrderNo FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo. TMERTBOM_SpecialOrder AS SP WITH(NOLOCK)  WHERE  SP.FNHSysBomId =" & BOMSysID & " )  AS SP  ON A.FTOrderNo =SP.FTOrderNo "

            End If

            cmdstring &= vbCrLf & " Outer Apply  (select top 1 '1' AS FTState FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Order AS B WITH(NOLOCK) WHERE  B.FNHSysBomId =" & BOMSysID & " AND B.FNSeq=" & pSeq & " And B.FTOrderNo= A.FTOrderNo )  As B "

            cmdstring &= vbCrLf & "  Outer Apply( select top 1 C1.FTBuyCode  from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMBuy AS C1 WITH(NOLOCK) WHERE C1.FNHSysBuyId = A.FNHSysBuyId ) C1  "


            cmdstring &= vbCrLf & " WHERE  A.FNHSysStyleId =" & StyleID & " AND A.FNHSysSeasonId =" & SeasonID & "  "
            cmdstring &= vbCrLf & " ORDER BY A.FTOrderNo "

            Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

            ogvlistorder.ClearColumnsFilter()
            ogvlistorder.ActiveFilter.Clear()
            ogvlistorder.OptionsView.ShowAutoFilterRow = True

            Me.ogdlistorder.DataSource = dt.Copy

            dt.Dispose()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryItemPopupOrder_QueryCloseUp(sender As Object, e As CancelEventArgs) Handles RepositoryItemPopupOrder.QueryCloseUp


        Try
            With Me.ogvmat
                If .FocusedRowHandle < 0 Then
                    Exit Sub
                End If

                ogvlistorder.FocusedColumn = ogvlistorder.Columns.ColumnByFieldName("FTOrderNo")

                Dim pSeq As Integer = Val(Me.ogvmat.GetFocusedRowCellValue("FNSeq").ToString)

                Me.FTPart.Focus()
                Me.FTPart.SelectAll()

                Dim RIdx As Integer = 0
                Dim cmdstring As String = ""

                cmdstring = "declare @Rec int =0  declare @Tab AS Table (FTOrderNo nvarchar(30) )"

                With CType(Me.ogdlistorder.DataSource, DataTable)
                    .AcceptChanges()
                    For Each R As DataRow In .Select("FTSelect='1'")

                        If RIdx = 0 Then

                            cmdstring &= vbCrLf & "   insert into @Tab(FTOrderNo) "
                            cmdstring &= vbCrLf & " Values('" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "') "

                        Else


                            cmdstring &= vbCrLf & " ,('" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "') "

                        End If
                        RIdx = RIdx + 1
                    Next

                End With

                cmdstring &= vbCrLf & " INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Order (FTInsUser, FDInsDate, FTInsTime, FNHSysBomId, FNSeq, FTOrderNo) "
                cmdstring &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB & ""
                cmdstring &= vbCrLf & "," & BOMSysID & ""
                cmdstring &= vbCrLf & "," & pSeq & ""
                cmdstring &= vbCrLf & ",A.FTOrderNo"
                cmdstring &= vbCrLf & "    FROM  @Tab AS A  "
                cmdstring &= vbCrLf & "   OUTER APPLY (SELECT TOP 1 '1' AS FTState FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Order AS B  WHERE    B.FNHSysBomId = " & BOMSysID & " And B.FNSeq = " & pSeq & " AND  B.FTOrderNo = A.FTOrderNo ) B  "
                cmdstring &= vbCrLf & "     WHERE   ISNULL(B.FTState,'') ='' "
                cmdstring &= vbCrLf & "  DELETE A "
                cmdstring &= vbCrLf & "    FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Order AS A  "
                cmdstring &= vbCrLf & "   OUTER APPLY (SELECT TOP 1 '1' AS FTState FROM  @Tab AS B  WHERE  B.FTOrderNo = A.FTOrderNo ) B  "
                cmdstring &= vbCrLf & "     WHERE  A.FNHSysBomId = " & BOMSysID & " And A.FNSeq = " & pSeq & "  AND ISNULL(B.FTState,'') ='' "

                cmdstring &= vbCrLf & "  DELETE A "
                cmdstring &= vbCrLf & "    FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SubOrder AS A  "
                cmdstring &= vbCrLf & "   OUTER APPLY (SELECT TOP 1 '1' AS FTState "
                cmdstring &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Order AS B  WHERE   B.FNHSysBomId = " & BOMSysID & " And B.FNSeq = " & pSeq & "  AND  B.FTOrderNo = A.FTOrderNo ) B  "
                cmdstring &= vbCrLf & "     WHERE  A.FNHSysBomId = " & BOMSysID & " And A.FNSeq = " & pSeq & "  And ISNULL(B.FTState,'') ='' "


                cmdstring &= vbCrLf & " UPDATE  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat   SET  "
                cmdstring &= vbCrLf & " FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""

                cmdstring &= vbCrLf & " WHERE FNHSysBomId=" & BOMSysID & " "
                cmdstring &= vbCrLf & " AND  FNSeq=" & pSeq & "  "
                cmdstring &= vbCrLf & " SET @Rec = @@ROWCOUNT  "
                cmdstring &= vbCrLf & "  Select  Top 1   @Rec AS FNState, TR.FTPart,TR.FTInsUser, TR.FDInsDate, TR.FTInsTime,TR.FNMatSeq ,TR.FTUpdUser, TR.FDUpdDate, TR.FTUpdTime,TR.FTComponent ,ISNULL(OD.FTOrderNo, 'ALL') AS FTOrderNo ,ISNULL(ODS.FTSubOrderNo, 'ALL') AS FTSubOrderNo "
                cmdstring &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat  AS TR  WITH(NOLOCK)  "

                cmdstring &= vbCrLf & "  OUTER APPLY(select  STUFF((SELECT  ',' + FTOrderNo  "
                cmdstring &= vbCrLf & " 	From( SELECT DISTINCT OD.FTOrderNo AS   FTOrderNo "
                cmdstring &= vbCrLf & " 	From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Order AS OD WITH(NOLOCK)	"
                cmdstring &= vbCrLf & "  Where OD.FNHSysBomId = TR.FNHSysBomId "
                cmdstring &= vbCrLf & " 	 And OD.FNSeq = TR.FNSeq  "
                cmdstring &= vbCrLf & " 	 ) AS T  "
                cmdstring &= vbCrLf & " 	For Xml PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'')  AS FTOrderNo ) AS  OD "


                cmdstring &= vbCrLf & "   OUTER APPLY(select  STUFF((SELECT  ',' + FTSubOrderNo   "
                cmdstring &= vbCrLf & " From( SELECT DISTINCT ODS.FTSubOrderNo AS   FTSubOrderNo "
                cmdstring &= vbCrLf & " 	From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SubOrder AS ODS WITH(NOLOCK) "

                cmdstring &= vbCrLf & "  WHERE   ODS.FNHSysBomId =  TR.FNHSysBomId "
                cmdstring &= vbCrLf & " 	 And ODS.FNSeq = TR.FNSeq "
                cmdstring &= vbCrLf & " 	 ) AS T  "
                cmdstring &= vbCrLf & "  For Xml PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'')  AS FTSubOrderNo ) AS  ODS "



                cmdstring &= vbCrLf & "  WHERE TR.FNHSysBomId=" & BOMSysID & " "
                cmdstring &= vbCrLf & "  AND  TR.FNSeq=" & pSeq & "  "


                Dim mdt As DataTable
                mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                If mdt.Rows.Count > 0 Then

                    If Val(mdt.Rows(0)!FNState.ToString) > 0 Then

                        With Me.ogvmat

                            For Each Rxp As DataRow In mdt.Rows

                                .SetFocusedRowCellValue("FTUpdUser", Rxp!FTUpdUser.ToString)
                                .SetFocusedRowCellValue("FDUpdDate", Rxp!FDUpdDate.ToString)
                                .SetFocusedRowCellValue("FTUpdTime", Rxp!FTUpdTime.ToString)

                                .SetFocusedRowCellValue("FTOrderNo", Rxp!FTOrderNo.ToString)
                                .SetFocusedRowCellValue("FTSubOrderNo", Rxp!FTSubOrderNo.ToString)


                            Next

                        End With


                        mdt.Dispose()
                        StateReLoad = True

                    Else
                        mdt.Dispose()

                    End If


                Else
                    mdt.Dispose()

                End If

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryItemPopupSubOrderNo_QueryPopUp(sender As Object, e As CancelEventArgs) Handles RepositoryItemPopupSubOrderNo.QueryPopUp
        Try
            ogdlistsuborder.DataSource = Nothing
            ocksubjobselectall.Checked = False

            Dim cmdstring As String = ""
            Dim pSeq As Integer = Val(Me.ogvmat.GetFocusedRowCellValue("FNSeq").ToString)

            cmdstring = "Select ISNULL(B.FTState,'0') AS FTSelect ,A.FTOrderNo,A.FTSubOrderNo"
            cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS A With(NOLOCK) "
            cmdstring &= vbCrLf & " INNER JOIN ( SELECT DISTINCT SP.FTOrderNo FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo. TMERTBOM_Order AS SP WITH(NOLOCK)  WHERE  SP.FNHSysBomId =" & BOMSysID & " AND   SP.FNSeq=" & pSeq & " )  AS SP ON A.FTOrderNo =SP.FTOrderNo"
            cmdstring &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS OD With(NOLOCK) ON A.FTOrderNo =OD.FTOrderNo"

            cmdstring &= vbCrLf & " Outer Apply  (select top 1 '1' AS FTState FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SubOrder AS B WITH(NOLOCK) WHERE  B.FNHSysBomId =" & BOMSysID & " AND B.FNSeq=" & pSeq & " And B.FTSubOrderNo= A.FTSubOrderNo )  As B "

            cmdstring &= vbCrLf & " WHERE  OD.FNHSysStyleId =" & StyleID & " AND OD.FNHSysSeasonId =" & SeasonID & "  "

            cmdstring &= vbCrLf & " UNION "
            cmdstring &= vbCrLf & " Select ISNULL(B.FTState,'0') AS FTSelect ,A.FTOrderNo,(A.FTSubOrderNo + '-D' + Convert(nvarchar(10),A.FNDivertSeq)) AS FTSubOrderNoo"
            cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert AS A With(NOLOCK) "
            cmdstring &= vbCrLf & " INNER JOIN ( SELECT DISTINCT SP.FTOrderNo FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo. TMERTBOM_Order AS SP WITH(NOLOCK)  WHERE  SP.FNHSysBomId =" & BOMSysID & " AND   SP.FNSeq=" & pSeq & " )  AS SP ON A.FTOrderNo =SP.FTOrderNo"
            cmdstring &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS OD With(NOLOCK) ON A.FTOrderNo =OD.FTOrderNo"

            cmdstring &= vbCrLf & " Outer Apply  (select top 1 '1' AS FTState FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SubOrder AS B WITH(NOLOCK) WHERE  B.FNHSysBomId =" & BOMSysID & " AND B.FNSeq=" & pSeq & " And B.FTSubOrderNo= (A.FTSubOrderNo + '-D' + Convert(nvarchar(10),A.FNDivertSeq)) )  As B "

            cmdstring &= vbCrLf & " WHERE  OD.FNHSysStyleId =" & StyleID & " AND OD.FNHSysSeasonId =" & SeasonID & "  "



            Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

            ogvlistsuborder.ClearColumnsFilter()
            ogvlistsuborder.ActiveFilter.Clear()
            ogvlistsuborder.OptionsView.ShowAutoFilterRow = True

            Me.ogdlistsuborder.DataSource = dt.Copy

            dt.Dispose()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryItemPopupSubOrderNo_QueryCloseUp(sender As Object, e As CancelEventArgs) Handles RepositoryItemPopupSubOrderNo.QueryCloseUp


        Try
            With Me.ogvmat
                If .FocusedRowHandle < 0 Then
                    Exit Sub
                End If

                ogvlistsuborder.FocusedColumn = ogvlistsuborder.Columns.ColumnByFieldName("FTOrderNo")

                Dim pSeq As Integer = Val(Me.ogvmat.GetFocusedRowCellValue("FNSeq").ToString)


                Dim cmdstring As String = ""
                Dim RIdx As Integer = 0
                cmdstring = "declare @Rec int =0  declare @Tab AS Table (FTOrderNo nvarchar(30),FTSubOrderNo nvarchar(30) )"

                With CType(Me.ogdlistsuborder.DataSource, DataTable)
                    .AcceptChanges()
                    For Each R As DataRow In .Select("FTSelect='1'")

                        If RIdx = 0 Then

                            cmdstring &= vbCrLf & "   insert into @Tab(FTOrderNo,FTSubOrderNo) "
                            cmdstring &= vbCrLf & " Values('" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "','" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString()) & "') "

                        Else


                            cmdstring &= vbCrLf & " ,('" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "','" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString()) & "') "

                        End If
                        RIdx = RIdx + 1
                    Next

                End With

                cmdstring &= vbCrLf & " INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SubOrder (FTInsUser, FDInsDate, FTInsTime, FNHSysBomId, FNSeq, FTOrderNo,FTSubOrderNo) "
                cmdstring &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB & ""
                cmdstring &= vbCrLf & "," & BOMSysID & ""
                cmdstring &= vbCrLf & "," & pSeq & ""
                cmdstring &= vbCrLf & ",A.FTOrderNo"
                cmdstring &= vbCrLf & ",A.FTSubOrderNo"
                cmdstring &= vbCrLf & "    FROM  @Tab AS A  "
                cmdstring &= vbCrLf & "   OUTER APPLY (SELECT TOP 1 '1' AS FTState FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SubOrder AS B  WHERE    B.FNHSysBomId = " & BOMSysID & " And B.FNSeq = " & pSeq & " AND  B.FTSubOrderNo = A.FTSubOrderNo ) B  "
                cmdstring &= vbCrLf & "     WHERE   ISNULL(B.FTState,'') ='' "
                cmdstring &= vbCrLf & "  DELETE A "
                cmdstring &= vbCrLf & "    FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SubOrder AS A  "
                cmdstring &= vbCrLf & "   OUTER APPLY (SELECT TOP 1 '1' AS FTState FROM  @Tab AS B  WHERE  B.FTSubOrderNo = A.FTSubOrderNo ) B  "
                cmdstring &= vbCrLf & "     WHERE  A.FNHSysBomId = " & BOMSysID & " And A.FNSeq = " & pSeq & "  AND ISNULL(B.FTState,'') ='' "



                cmdstring &= vbCrLf & " UPDATE  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat   SET  "
                cmdstring &= vbCrLf & " FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""

                cmdstring &= vbCrLf & " WHERE FNHSysBomId=" & BOMSysID & " "
                cmdstring &= vbCrLf & " AND  FNSeq=" & pSeq & "  "
                cmdstring &= vbCrLf & " SET @Rec = @@ROWCOUNT  "
                cmdstring &= vbCrLf & "  Select  Top 1   @Rec AS FNState, TR.FTPart,TR.FTInsUser, TR.FDInsDate, TR.FTInsTime,TR.FNMatSeq ,TR.FTUpdUser, TR.FDUpdDate, TR.FTUpdTime,ISNULL(ODS.FTSubOrderNo, 'ALL') AS FTSubOrderNo "
                cmdstring &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat  AS TR  WITH(NOLOCK)  "



                cmdstring &= vbCrLf & "   OUTER APPLY(select  STUFF((SELECT  ',' + FTSubOrderNo   "
                cmdstring &= vbCrLf & " From( SELECT DISTINCT ODS.FTSubOrderNo AS   FTSubOrderNo "
                cmdstring &= vbCrLf & " 	From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SubOrder AS ODS WITH(NOLOCK) "

                cmdstring &= vbCrLf & "  WHERE   ODS.FNHSysBomId =  TR.FNHSysBomId "
                cmdstring &= vbCrLf & " 	 And ODS.FNSeq = TR.FNSeq "
                cmdstring &= vbCrLf & " 	 ) AS T  "
                cmdstring &= vbCrLf & "  For Xml PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'')  AS FTSubOrderNo ) AS  ODS "



                cmdstring &= vbCrLf & "  WHERE TR.FNHSysBomId=" & BOMSysID & " "
                cmdstring &= vbCrLf & "  AND  TR.FNSeq=" & pSeq & "  "


                Dim mdt As DataTable
                mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                If mdt.Rows.Count > 0 Then

                    If Val(mdt.Rows(0)!FNState.ToString) > 0 Then

                        With Me.ogvmat

                            For Each Rxp As DataRow In mdt.Rows

                                .SetFocusedRowCellValue("FTUpdUser", Rxp!FTUpdUser.ToString)
                                .SetFocusedRowCellValue("FDUpdDate", Rxp!FDUpdDate.ToString)
                                .SetFocusedRowCellValue("FTUpdTime", Rxp!FTUpdTime.ToString)

                                .SetFocusedRowCellValue("FTSubOrderNo", Rxp!FTSubOrderNo.ToString)


                            Next

                        End With


                        mdt.Dispose()

                        StateReLoad = True

                    Else
                        mdt.Dispose()

                    End If


                Else
                    mdt.Dispose()

                End If

            End With
        Catch ex As Exception
        End Try
    End Sub



    Private Sub RepositoryItemPopupContainerEditSilkColor_QueryPopUp(sender As Object, e As CancelEventArgs) Handles RepositoryItemPopupContainerEditSilkColor.QueryPopUp
        Try
            Dim cmdstring As String = ""
            Dim pSeq As Integer = Val(Me.ogvmatsilkcolor.GetFocusedRowCellValue("FNSeq").ToString)

            Dim pColorWay As String = ogvmatsilkcolor.FocusedColumn.Caption.Trim()

            cmdstring = " SELECT    ISNULL(B.FTState,'0') AS FTSelect  ,ISNULL(B.FNMatSeq,0) AS FNMatSeq,  A.FNHSysRawMatColorId,A.FTRawMatColorCode,ISNULL(B.FTRawMatColorNameEN,A.FTRawMatColorNameEN) AS FTRawMatColorNameEN "


            cmdstring &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor As A WITH(NOLOCK) "


            cmdstring &= vbCrLf & " Outer Apply  (select top 1 '1' AS FTState,FTRawMatColorNameEN,FNMatSeq FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SilkColorwayMaterial AS B WITH(NOLOCK) WHERE  B.FNHSysBomId =" & BOMSysID & " AND B.FNSeq=" & pSeq & " AND FTColorway='" & HI.UL.ULF.rpQuoted(pColorWay) & "' And B.FNHSysRawMatColorId = A.FNHSysRawMatColorId )  As B "

            cmdstring &= vbCrLf & "  Where A.FTStateActive ='1' "
            cmdstring &= vbCrLf & " ORDER BY A.FTRawMatColorCode "

            Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

            ogvSilkColor.ClearColumnsFilter()
            ogvSilkColor.ActiveFilter.Clear()
            ogvSilkColor.OptionsView.ShowAutoFilterRow = True

            Me.ogcSilkColor.DataSource = dt.Copy

            dt.Dispose()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryItemPopupContainerEditSilkColor_QueryCloseUp(sender As Object, e As CancelEventArgs) Handles RepositoryItemPopupContainerEditSilkColor.QueryCloseUp

        Try
            With Me.ogvmatsilkcolor
                If .FocusedRowHandle < 0 Then
                    Exit Sub
                End If

                'ogvmatsilkcolor.FocusedColumn = ogvmatsilkcolor.Columns.ColumnByFieldName("FTRawMatColorCode")
                Dim pSeq As Integer = Val(Me.ogvmatsilkcolor.GetFocusedRowCellValue("FNSeq").ToString)
                Dim pColorWay As String = ogvmatsilkcolor.FocusedColumn.Caption.Trim()

                Dim cmdstring As String = ""
                Dim RIdx As Integer = 0
                cmdstring = "declare @Rec int =0  declare @Tab AS Table (FNHSysBomId  int,FNSeq int ,FNMatSeq int,FTColorway    nvarchar(60) ,FTRawMatColorCode nvarchar(60),FNHSysRawMatColorId int,FTRawMatColorName nvarchar(200) )"

                With CType(Me.ogcSilkColor.DataSource, DataTable)
                    .AcceptChanges()
                    For Each R As DataRow In .Select("FTSelect='1'")

                        If RIdx = 0 Then

                            cmdstring &= vbCrLf & "   insert into @Tab(FNHSysBomId,FNSeq,FNMatSeq,FTColorway,FTRawMatColorCode,FNHSysRawMatColorId,FTRawMatColorName) "
                            cmdstring &= vbCrLf & " Values(" & BOMSysID & "," & pSeq & "," & Val(R!FNMatSeq.ToString) & ",'" & HI.UL.ULF.rpQuoted(pColorWay) & "', '" & HI.UL.ULF.rpQuoted(R!FTRawMatColorCode.ToString()) & "'," & Val(R!FNHSysRawMatColorId.ToString()) & ", '" & HI.UL.ULF.rpQuoted(R!FTRawMatColorNameEN.ToString()) & "') "

                        Else


                            cmdstring &= vbCrLf & " ,(" & BOMSysID & "," & pSeq & "," & Val(R!FNMatSeq.ToString) & ",'" & HI.UL.ULF.rpQuoted(pColorWay) & "','" & HI.UL.ULF.rpQuoted(R!FTRawMatColorCode.ToString()) & "'," & Val(R!FNHSysRawMatColorId.ToString()) & ", '" & HI.UL.ULF.rpQuoted(R!FTRawMatColorNameEN.ToString()) & "') "

                        End If
                        RIdx = RIdx + 1
                    Next

                End With


                cmdstring &= vbCrLf & "UPDATE M  SET "
                cmdstring &= vbCrLf & " FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                cmdstring &= vbCrLf & " ,FTRawMatColorNameTH =A.FTRawMatColorName "
                cmdstring &= vbCrLf & " ,FTRawMatColorNameEN =A.FTRawMatColorName "
                cmdstring &= vbCrLf & " ,FNMatSeq =A.FNMatSeq "
                cmdstring &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SilkColorwayMaterial M "

                cmdstring &= vbCrLf & " INNER JOIN    @Tab AS A ON M.FNHSysBomId = A.FNHSysBomId AND M.FNSeq=A.FNSeq AND M.FTColorway=A.FTColorway  And M.FNHSysRawMatColorId = A.FNHSysRawMatColorId "
                cmdstring &= vbCrLf & "     WHERE   M.FNHSysBomId = " & BOMSysID & " And M.FNSeq = " & pSeq & "  AND M.FTColorway='" & HI.UL.ULF.rpQuoted(pColorWay) & "' "


                cmdstring &= vbCrLf & " INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SilkColorwayMaterial (FTInsUser, FDInsDate, FTInsTime, FNHSysBomId, FNSeq,"
                cmdstring &= vbCrLf & "  FNColorWaySeq, FNHSysMatColorId, FTColorway, FNHSysRawMatColorId, FNMatSeq, FTRawMatColorNameTH,  FTRawMatColorNameEN, FTSilkNote) "
                cmdstring &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB & ""
                cmdstring &= vbCrLf & "," & BOMSysID & ""
                cmdstring &= vbCrLf & "," & pSeq & ""
                cmdstring &= vbCrLf & ",M.FNColorWaySeq"
                cmdstring &= vbCrLf & ",M.FNHSysMatColorId"
                cmdstring &= vbCrLf & ",M.FTColorway"
                cmdstring &= vbCrLf & ",A.FNHSysRawMatColorId"
                cmdstring &= vbCrLf & ",A.FNMatSeq "
                cmdstring &= vbCrLf & ",A.FTRawMatColorName AS  FTRawMatColorNameTH,A.FTRawMatColorName AS   FTRawMatColorNameEN,'' FTSilkNote "

                cmdstring &= vbCrLf & "    FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SilkColorway AS M INNER JOIN  @Tab AS A ON M.FNHSysBomId = A.FNHSysBomId AND M.FNSeq=A.FNSeq AND M.FTColorway=A.FTColorway  "
                cmdstring &= vbCrLf & "   OUTER APPLY (SELECT TOP 1 '1' AS FTState FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SilkColorwayMaterial AS B WITH(NOLOCK) WHERE  B.FNHSysBomId =" & BOMSysID & " AND B.FNSeq=" & pSeq & " AND B.FTColorway='" & HI.UL.ULF.rpQuoted(pColorWay) & "' And B.FNHSysRawMatColorId = A.FNHSysRawMatColorId )  As B "
                cmdstring &= vbCrLf & "     WHERE   M.FNHSysBomId = " & BOMSysID & " And M.FNSeq = " & pSeq & "  AND M.FTColorway='" & HI.UL.ULF.rpQuoted(pColorWay) & "'  AND ISNULL(B.FTState,'') ='' "
                cmdstring &= vbCrLf & "  DELETE A "
                cmdstring &= vbCrLf & "    FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SilkColorwayMaterial AS A  "
                cmdstring &= vbCrLf & "   OUTER APPLY (SELECT TOP 1 '1' AS FTState FROM  @Tab AS B  WHERE  B.FNHSysRawMatColorId = A.FNHSysRawMatColorId ) B  "
                cmdstring &= vbCrLf & "     WHERE  A.FNHSysBomId = " & BOMSysID & " And A.FNSeq = " & pSeq & "  AND A.FTColorway='" & HI.UL.ULF.rpQuoted(pColorWay) & "'  AND ISNULL(B.FTState,'') ='' "



                cmdstring &= vbCrLf & " UPDATE  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SilkColorway   SET  "
                cmdstring &= vbCrLf & " FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                cmdstring &= vbCrLf & "     WHERE  FNHSysBomId = " & BOMSysID & " And FNSeq = " & pSeq & "  AND FTColorway='" & HI.UL.ULF.rpQuoted(pColorWay) & "' "

                cmdstring &= vbCrLf & " SET @Rec = @@ROWCOUNT  "
                cmdstring &= vbCrLf & "  Select  Top 1   @Rec AS FNState, ISNULL(RMC.FTColorRM, '') AS FTRawMatColorCode "
                cmdstring &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SilkColorway  AS TR  WITH(NOLOCK)  "



                cmdstring &= vbCrLf & "   OUTER APPLY(select  STUFF((SELECT  ',' + FTRawMatColorCode    "
                cmdstring &= vbCrLf & " From( SELECT  MIN(RMC.FNMatSeq) AS FNMatSeq ,C.FTRawMatColorCode   "
                cmdstring &= vbCrLf & " 	From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SilkColorwayMaterial AS  RMC WITH(NOLOCK)"
                cmdstring &= vbCrLf & " 	INNER Join " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TINVENMMatColor AS C ON RMC.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
                cmdstring &= vbCrLf & "  WHERE    RMC.FNHSysBomId = TR.FNHSysBomId "
                cmdstring &= vbCrLf & " 	       And RMC.FNSeq=TR.FNSeq "
                cmdstring &= vbCrLf & " 	       And RMC.FTColorway = TR.FTColorway GROUP BY C.FTRawMatColorCode  "
                cmdstring &= vbCrLf & " 	 ) AS T  "
                cmdstring &= vbCrLf & "  For Xml PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'')  AS FTColorRM ) AS  RMC "

                cmdstring &= vbCrLf & "     WHERE  TR.FNHSysBomId = " & BOMSysID & " And TR.FNSeq = " & pSeq & "  AND TR.FTColorway='" & HI.UL.ULF.rpQuoted(pColorWay) & "' "

                Dim mdt As DataTable
                mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                If mdt.Rows.Count > 0 Then

                    If Val(mdt.Rows(0)!FNState.ToString) > 0 Then

                        With Me.ogvmatsilkcolor

                            For Each Rxp As DataRow In mdt.Rows

                                .SetFocusedRowCellValue(.FocusedColumn.FieldName, Rxp!FTRawMatColorCode.ToString)


                            Next

                        End With

                        mdt.Dispose()

                    Else
                        mdt.Dispose()

                    End If


                Else
                    mdt.Dispose()

                End If

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryItemGridLookUpEditMainMatCode_EditValueChanged(sender As Object, e As EventArgs) 'Handles RepositoryItemGridLookUpEditMainMatCode.EditValueChanged

        Try
            Dim MatCode As String = ""
            With Me.ogvmat
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)

                Dim mNominate As String = "0"
                Dim mMainMaterial As String = "0"
                Dim pText As String = obj.Text
                Dim mMatId As Integer = 0
                Dim pMatName As String = ""

                'mMatId = Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysMainMatId").ToString())
                'MatCode = (obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTMainMatCode").ToString())

                'pMatName = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTMainMatName").ToString


                With CType(obj.Properties.DataSource, DataTable)

                    For Each Rx As DataRow In .Select("FTMainMatCode='" & HI.UL.ULF.rpQuoted(pText) & "'")
                        MatCode = Rx!FTMainMatCode.ToString
                        mMatId = Val((Rx!FNHSysMainMatId.ToString))
                        pMatName = Rx!FTMainMatName.ToString
                    Next

                End With


                Dim pSuplCode As String = "" 'obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTSuplCode").ToString

                Dim mCurCode As String = "" 'obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTCurCode").ToString
                Dim mUnitCode As String = "" 'obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTUnitCode").ToString

                Dim mPrice As Decimal = 0.0 'Decimal.Parse(Format(Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNPrice").ToString), "0.0000"))

                Dim mFrontSize As String = "" 'obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTFabricFrontSize").ToString


                Dim pStateDefualtBOMColorByColorway As String = "0" ' obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTStateDefualtBOMColorByColorway").ToString
                Dim pStateDefualtBOMSizeBySizeBreakdown As String = "0" 'obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTStateDefualtBOMSizeBySizeBreakdown").ToString
                Dim pStateSilk As String = "0" 'obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTStateSilk").ToString

                'If obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTStateNominate").ToString = "1" Then
                '    mNominate = "1"
                'End If

                'If obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTStateMainMaterial").ToString = "1" Then
                '    mMainMaterial = "1"
                'End If

                Dim mSuplId As Integer = 0 'Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysSuplId").ToString)
                Dim mUnitId As Integer = 0 'Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysUnitId").ToString)
                Dim mCurId As Integer = 0 'Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysCurId").ToString)
                Dim mdefaultcolor As Integer = 0
                Dim mdefaultsize As Integer = 0

                Dim cmdstring As String = ""

                cmdstring = " SELECT    TOP 1      A.FNHSysMainMatId,A.FTMainMatCode "
                cmdstring &= vbCrLf & "  , A.FTStateNominate, A.FNHSysUnitId, A.FNHSysCurId"
                cmdstring &= vbCrLf & "  , A.FNPrice, A.FTStateMainMaterial, A.FTStateActive, A.FNHSysSuplId, S.FTSuplCode, U.FTUnitCode, C.FTCurCode "
                cmdstring &= vbCrLf & " , A.FTFabricFrontSize,A.FTStateDefualtBOMColorByColorway,A.FTStateDefualtBOMSizeBySizeBreakdown,A.FTStateSilk,A.FNHSysRawMatColorId,A.FNHSysRawMatSizeId "
                cmdstring &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat As A WITH(NOLOCK) LEFT OUTER Join "
                cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency As C WITH(NOLOCK) On A.FNHSysCurId = C.FNHSysCurId LEFT OUTER Join "
                cmdstring &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit As U WITH(NOLOCK) On A.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER Join "
                cmdstring &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier As S WITH(NOLOCK) On A.FNHSysSuplId = S.FNHSysSuplId "
                cmdstring &= vbCrLf & "  Where A.FTStateActive ='1'  AND   A.FNHSysMainMatId =" & mMatId & " "
                cmdstring &= vbCrLf & "  ORDER BY A.FTMainMatCode  "


                Dim dtmat As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)
                For Each Rmx As DataRow In dtmat.Rows
                    pSuplCode = Rmx!FTSuplCode.ToString
                    mCurCode = Rmx!FTCurCode.ToString
                    mUnitCode = Rmx!FTUnitCode.ToString
                    mPrice = Decimal.Parse(Format(Val(Rmx!FNPrice.ToString), "0.0000"))
                    mFrontSize = Rmx!FTFabricFrontSize.ToString

                    If Rmx!FTStateDefualtBOMColorByColorway.ToString = "1" Then
                        pStateDefualtBOMColorByColorway = "1"
                    End If

                    If Rmx!FTStateDefualtBOMSizeBySizeBreakdown.ToString = "1" Then
                        pStateDefualtBOMSizeBySizeBreakdown = "1"
                    End If


                    If Rmx!FTStateSilk.ToString = "1" Then
                        pStateSilk = "1"
                    End If

                    If Rmx!FTStateNominate.ToString = "1" Then
                        mNominate = "1"
                    End If

                    If Rmx!FTStateMainMaterial.ToString = "1" Then
                        mMainMaterial = "1"
                    End If

                    mSuplId = Val(Rmx!FNHSysSuplId.ToString)
                    mUnitId = Val(Rmx!FNHSysUnitId.ToString)
                    mCurId = Val(Rmx!FNHSysCurId.ToString)
                    mdefaultcolor = Val(Rmx!FNHSysRawMatColorId.ToString)
                    mdefaultsize = Val(Rmx!FNHSysRawMatSizeId.ToString)
                Next

                dtmat.Dispose()
                Dim mStateActive As String = .GetFocusedRowCellValue("FTStateActive").ToString
                Dim mPart As Integer = Val(.GetFocusedRowCellValue("FNPart").ToString)
                Dim mMatSeq As Decimal = Decimal.Parse(Format(Val(.GetFocusedRowCellValue("FNMatSeq").ToString), "0.00"))



                If Val(.GetFocusedRowCellValue("FNSeq").ToString) = 0 Then


                    cmdstring = " Declare @Seq int = 0 "
                    cmdstring &= vbCrLf & " Declare @Rec int = 0 "
                    cmdstring &= vbCrLf & " SET @Seq = ISNULL((select max(FNSeq) FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat WHERE  FNHSysBomId =" & BOMSysID & " ),0) +1 "
                    cmdstring &= vbCrLf & " insert into  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat  (FTInsUser, FDInsDate, FTInsTime, FNHSysBomId "
                    cmdstring &= vbCrLf & " , FNSeq, FNMatSeq, FNHSysMainMatId, FTFabricFrontSize, FNPart, FNHSysSuplId, FTStateNominate, FNHSysUnitId, FNHSysCurId, FNPrice, FTStateActive, FTStateMainMaterial, FNConSmp, FNConSmpPlus"
                    cmdstring &= vbCrLf & " ,    FNOrderSetType,FTStateCombination, FTPart, FTComponent, FTStateFree, FTStateHemNotOptiplan, FNRepeatLengthCM, FNRepeatConvert"
                    cmdstring &= vbCrLf & " , FNPackPerCarton, FNConSmpSplit, FTStateDTM,  FTDTMNote, FTNote, FTStateMatConfirm, FTRunColor, FTRunSize,FTSilkName,FTStateSilk,FTStateCalMRP,FTStateExportOptiplan)"
                    cmdstring &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    cmdstring &= vbCrLf & "," & BOMSysID & " "
                    cmdstring &= vbCrLf & ",@Seq "
                    cmdstring &= vbCrLf & " ," & mMatSeq & ""
                    cmdstring &= vbCrLf & " ," & mMatId & ""
                    cmdstring &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(mFrontSize) & "' "
                    cmdstring &= vbCrLf & " ," & mPart & ""
                    cmdstring &= vbCrLf & " ," & mSuplId & ""
                    cmdstring &= vbCrLf & ", '" & mNominate & "' "
                    cmdstring &= vbCrLf & " ," & mUnitId & ""
                    cmdstring &= vbCrLf & " ," & mCurId & ""
                    cmdstring &= vbCrLf & " ," & mPrice & ""
                    cmdstring &= vbCrLf & ", '" & mStateActive & "' "
                    cmdstring &= vbCrLf & ", '" & mMainMaterial & "' "


                    cmdstring &= vbCrLf & ",0 FNConSmp,0 FNConSmpPlus"
                    cmdstring &= vbCrLf & " ,  0  FNOrderSetType,'0' FTStateCombination,'' FTPart,'' FTComponent,'0' FTStateFree,'0' FTStateHemNotOptiplan,0 FNRepeatLengthCM,0 FNRepeatConvert"
                    cmdstring &= vbCrLf & " ,0 FNPackPerCarton,0 FNConSmpSplit,'0' FTStateDTM, '' FTDTMNote,'' FTNote,'0' FTStateMatConfirm,'1','1' "



                    If pStateSilk = "1" Then
                        cmdstring &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(Microsoft.VisualBasic.Left(pMatName, 500)) & "','1' "
                    Else
                        cmdstring &= vbCrLf & ", '','0' "
                    End If
                    cmdstring &= vbCrLf & ",'0','0' "
                    cmdstring &= vbCrLf & " SET @Rec = @@ROWCOUNT  "

                    cmdstring &= vbCrLf & " EXEC  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_GETBOMGARMENT_CHECKDEFUALT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & BOMSysID & ",@Seq,'" & pStateDefualtBOMColorByColorway & "','" & pStateDefualtBOMSizeBySizeBreakdown & "','" & pStateSilk & "'," & mdefaultcolor & "," & mdefaultsize & ", '1' "

                    'cmdstring &= vbCrLf & "  Select  Top 1 @Rec  AS FNState ,FNSeq,FTInsUser, FDInsDate, FTInsTime,FNMatSeq ,FTUpdUser, FDUpdDate, FTUpdTime,FTStateMatConfirm,FTStateSilk  "


                    'cmdstring &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat  AS TR  WITH(NOLOCK)  "
                    'cmdstring &= vbCrLf & " WHERE FNHSysBomId=" & BOMSysID & " "
                    'cmdstring &= vbCrLf & " AND  FNSeq=@Seq  "




                Else



                    cmdstring = " Declare @Seq int = " & Val(.GetFocusedRowCellValue("FNSeq").ToString) & " "
                    cmdstring &= vbCrLf & " Declare @Rec int = 0 "

                    cmdstring &= vbCrLf & " UPDATE  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat   SET  "
                    cmdstring &= vbCrLf & " FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    cmdstring &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                    cmdstring &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""

                    cmdstring &= vbCrLf & " ,FNMatSeq=" & mMatSeq & ""
                    cmdstring &= vbCrLf & " ,FNHSysMainMatId=" & mMatId & ""
                    cmdstring &= vbCrLf & ",FTFabricFrontSize= '" & HI.UL.ULF.rpQuoted(mFrontSize) & "' "

                    cmdstring &= vbCrLf & " ,FNHSysSuplId=" & mSuplId & ""
                    cmdstring &= vbCrLf & ", FTStateNominate='" & mNominate & "' "
                    cmdstring &= vbCrLf & " ,FNHSysUnitId=" & mUnitId & ""
                    cmdstring &= vbCrLf & " ,FNHSysCurId=" & mCurId & ""
                    cmdstring &= vbCrLf & " ,FNPrice=" & mPrice & ""
                    cmdstring &= vbCrLf & ", FTStateMainMaterial='" & mMainMaterial & "' "
                    cmdstring &= vbCrLf & ", FTStateCombination='" & mMainMaterial & "' "
                    cmdstring &= vbCrLf & ",FTStateMatConfirm='0'"


                    If pStateSilk = "1" Then
                        cmdstring &= vbCrLf & ",FTSilkName= '" & HI.UL.ULF.rpQuoted(Microsoft.VisualBasic.Left(pMatName, 500)) & "' "
                        cmdstring &= vbCrLf & ", FTStateSilk='1' "
                    Else
                        cmdstring &= vbCrLf & ", FTSilkName='' "
                        cmdstring &= vbCrLf & ", FTStateSilk='0' "
                    End If

                    cmdstring &= vbCrLf & ", FTStateCalMRP='0' "
                    cmdstring &= vbCrLf & ", FTStateExportOptiplan='0' "


                    cmdstring &= vbCrLf & " WHERE FNHSysBomId=" & BOMSysID & " "
                    cmdstring &= vbCrLf & " AND  FNSeq=@Seq  "

                    cmdstring &= vbCrLf & " SET @Rec = @@ROWCOUNT  "

                    cmdstring &= vbCrLf & " EXEC  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_GETBOMGARMENT_CHECKDEFUALT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & BOMSysID & ",@Seq,'" & pStateDefualtBOMColorByColorway & "','" & pStateDefualtBOMSizeBySizeBreakdown & "','" & pStateSilk & "'," & mdefaultcolor & "," & mdefaultsize & ",'2' "



                End If


                cmdstring &= vbCrLf & "  Select  Top 1 @Rec  AS FNState ,FNSeq,FTInsUser, FDInsDate, FTInsTime,FNMatSeq ,FTUpdUser, FDUpdDate, FTUpdTime,FTStateMatConfirm,FTStateSilk ,FTStateCalMRP,FTStateExportOptiplan,FTStateCombination "


                cmdstring &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat  AS TR  WITH(NOLOCK)  "
                cmdstring &= vbCrLf & " WHERE FNHSysBomId=" & BOMSysID & " "
                cmdstring &= vbCrLf & " AND  FNSeq=@Seq  "



                Dim mdt As DataTable
                mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                If mdt.Rows.Count > 0 Then

                    If Val(mdt.Rows(0)!FNState.ToString) > 0 Then
                        For Each Rxp As DataRow In mdt.Rows


                            .SetFocusedRowCellValue("FTMainMatName", pMatName)
                            .SetFocusedRowCellValue("FTSuplCode", pSuplCode)
                            .SetFocusedRowCellValue("FTCurCode", mCurCode)
                            .SetFocusedRowCellValue("FTUnitCode", mUnitCode)
                            .SetFocusedRowCellValue("FNPrice", mPrice)
                            .SetFocusedRowCellValue("FTStateNominate", mNominate)
                            .SetFocusedRowCellValue("FTStateMainMaterial", mMainMaterial)
                            .SetFocusedRowCellValue("FTStateCombination", mMainMaterial)
                            .SetFocusedRowCellValue("FTFabricFrontSize", mFrontSize)

                            .SetFocusedRowCellValue("FNHSysMainMatId", mMatId)
                            .SetFocusedRowCellValue("FNHSysSuplId", mSuplId)
                            .SetFocusedRowCellValue("FNHSysUnitId", mUnitId)
                            .SetFocusedRowCellValue("FNHSysCurId", mCurId)

                            .SetFocusedRowCellValue("FNSeq", Val(Rxp!FNSeq.ToString))
                            .SetFocusedRowCellValue("FTInsUser", Rxp!FTInsUser.ToString)
                            .SetFocusedRowCellValue("FDInsDate", Rxp!FDInsDate.ToString)
                            .SetFocusedRowCellValue("FTInsTime", Rxp!FTInsTime.ToString)
                            .SetFocusedRowCellValue("FNMatSeq", Val(Rxp!FNMatSeq.ToString))
                            .SetFocusedRowCellValue("FTUpdUser", Rxp!FTUpdUser.ToString)
                            .SetFocusedRowCellValue("FDUpdDate", Rxp!FDUpdDate.ToString)
                            .SetFocusedRowCellValue("FTUpdTime", Rxp!FTUpdTime.ToString)
                            .SetFocusedRowCellValue("FTStateMatConfirm", Rxp!FTStateMatConfirm.ToString)
                            .SetFocusedRowCellValue("FTStateSilk", Rxp!FTStateSilk.ToString)
                            .SetFocusedRowCellValue("FTStateCalMRP", Rxp!FTStateCalMRP.ToString)
                            .SetFocusedRowCellValue("FTStateExportOptiplan", Rxp!FTStateExportOptiplan.ToString)

                        Next

                        Me.ogvmat.FocusedColumn = Me.ogvmat.Columns.ColumnByFieldName("FTMainMatName")

                        OldValue = MatCode
                        StateReLoad = True
                    Else
                        Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, Val(OldValue))
                    End If

                Else
                    Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, Val(OldValue))
                End If

            End With

        Catch ex As Exception
        End Try

    End Sub

    Private Sub RepositoryItemGridLookUpEditFTSuplCode_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemGridLookUpEditFTSuplCode.EditValueChanged, RepositoryItemGridLookUpEditFTUnitCode.EditValueChanged, RepositoryItemGridLookUpEditFTCurCode.EditValueChanged
        Try
            Dim MatCode As String = ""
            With Me.ogvmat
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)

                Dim pCode As String = ""
                Dim mlId As Integer = 0
                Dim pFiledName As String = ""

                Dim pText As String = obj.Text


                Dim CurpCode As String = ""
                Dim CurmlId As Integer = 0



                Select Case .FocusedColumn.FieldName
                    Case "FTSuplCode"
                        pFiledName = "FNHSysSuplId"
                        'pCode = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTSuplCode").ToString
                        'mlId = Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysSuplId").ToString)

                        With CType(obj.Properties.DataSource, DataTable)

                            For Each Rx As DataRow In .Select("FTSuplCode='" & HI.UL.ULF.rpQuoted(pText) & "'")
                                pCode = Rx!FTSuplCode.ToString
                                mlId = Val((Rx!FNHSysSuplId.ToString))

                                CurpCode = Rx!FTCurCode.ToString
                                CurmlId = Val((Rx!FNHSysCurId.ToString))
                            Next

                        End With

                    Case "FTUnitCode"
                        pFiledName = "FNHSysUnitId"
                        'pCode = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTUnitCode").ToString
                        'mlId = Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysUnitId").ToString)

                        With CType(obj.Properties.DataSource, DataTable)

                            For Each Rx As DataRow In .Select("FTUnitCode='" & HI.UL.ULF.rpQuoted(pText) & "'")
                                pCode = Rx!FTUnitCode.ToString
                                mlId = Val((Rx!FNHSysUnitId.ToString))

                            Next

                        End With


                    Case "FTCurCode"
                        pFiledName = "FNHSysCurId"
                        'pCode = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTCurCode").ToString
                        'mlId = Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysCurId").ToString)


                        With CType(obj.Properties.DataSource, DataTable)

                            For Each Rx As DataRow In .Select("FTCurCode='" & HI.UL.ULF.rpQuoted(pText) & "'")
                                pCode = Rx!FTCurCode.ToString
                                mlId = Val((Rx!FNHSysCurId.ToString))

                            Next

                        End With

                    Case Else
                        Exit Sub
                End Select


                Dim pSeq As Integer = Val(Me.ogvmat.GetFocusedRowCellValue("FNSeq").ToString)

                If (OldValue) <> pCode And pSeq > 0 And (pText = "" Or (pText <> "" And pCode <> "")) Then

                    pBOMListUpdate.Clear()

                    Dim pBOM As New BOMData
                    pBOM.FieldName = pFiledName
                    pBOM.FieldValue = mlId
                    pBOM.FieldDataType = BOMDataType.DataInteger
                    pBOMListUpdate.Add(pBOM)

                    If UpdateBOMItem(BOMSysID, pSeq, pBOMListUpdate) = False Then

                        Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, OldValue)

                    Else
                        Me.ogvmat.SetFocusedRowCellValue(pFiledName, Val(mlId))


                        If CurpCode <> "" Then
                            Try

                                Me.ogvmat.SetFocusedRowCellValue("FTCurCode", CurpCode)
                                Me.ogvmat.SetFocusedRowCellValue("FNHSysCurId", Val(CurmlId))
                            Catch ex As Exception

                            End Try


                        End If

                        OldValue = pCode

                    End If

                    pBOMListUpdate.Clear()

                Else
                    Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, OldValue)
                End If

            End With

        Catch ex As Exception
            Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, Val(OldValue))
        End Try


    End Sub



    Private Function UpdateBOMHeader(pBomID As Integer, pData As List(Of BOMData)) As Boolean
        Dim cmdstring As String = ""


        Try
            Dim mFoundStateComfirm As Boolean = False
            Dim mFoundStatePackPer As Boolean = False

            cmdstring = " Declare @Rec int = 0 "
            cmdstring &= vbCrLf & " UPDATE  BOM   SET  "
            cmdstring &= vbCrLf & " FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            cmdstring &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
            cmdstring &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""


            For I As Integer = 0 To pData.Count - 1
                If pData(I).FieldDataType = BOMDataType.DataInteger Then
                    cmdstring &= vbCrLf & ", " & pData(I).FieldName & "=" & pData(I).FieldValue & ""
                Else
                    cmdstring &= vbCrLf & " ," & pData(I).FieldName & " ='" & HI.UL.ULF.rpQuoted(pData(I).FieldValue) & "' "
                End If

            Next

            cmdstring &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM  AS BOM  "
            cmdstring &= vbCrLf & " WHERE BOM.FNHSysBomId=" & pBomID & " "

            cmdstring &= vbCrLf & " SET @Rec = @@ROWCOUNT  "
            cmdstring &= vbCrLf & "  Select  Top 1   @Rec AS FNState, FTInsUser, FDInsDate, FTInsTime "

            cmdstring &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM  AS TR  WITH(NOLOCK)  "
            cmdstring &= vbCrLf & " WHERE FNHSysBomId=" & pBomID & " "


            Dim mdt As DataTable
            mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

            If mdt.Rows.Count > 0 Then

                If Val(mdt.Rows(0)!FNState.ToString) > 0 Then

                    mdt.Dispose()
                    Return True
                Else
                    mdt.Dispose()
                    Return False
                End If


            Else
                mdt.Dispose()
                Return False
            End If


        Catch ex As Exception
            Return False
        End Try

    End Function


    Private Function UpdateBOMItem(pBomID As Integer, BomSeq As Integer, pData As List(Of BOMData)) As Boolean
        Dim cmdstring As String = ""


        Try
            Dim mFoundStateComfirm As Boolean = False
            Dim mFoundStatePackPer As Boolean = False
            Dim mStateRunColor As Boolean = False
            cmdstring = " Declare @Rec int = 0 "
            cmdstring &= vbCrLf & " UPDATE  BOM   SET  "
            cmdstring &= vbCrLf & " FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            cmdstring &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
            cmdstring &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""


            For I As Integer = 0 To pData.Count - 1
                If pData(I).FieldDataType = BOMDataType.DataInteger Then
                    cmdstring &= vbCrLf & ", " & pData(I).FieldName & "=" & pData(I).FieldValue & ""
                Else
                    cmdstring &= vbCrLf & " ," & pData(I).FieldName & " ='" & HI.UL.ULF.rpQuoted(pData(I).FieldValue) & "' "
                End If

                Select Case pData(I).FieldName
                    Case "FTStateMatConfirm"

                        If pData(I).FieldValue = "1" Then
                            mFoundStateComfirm = True

                            cmdstring &= vbCrLf & " ,FTStateFirstMatConfirmUpdUser = CASE WHEN ISNULL(FTStateFirstMatConfirmUpdUser,'') =''  THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ELSE  FTStateFirstMatConfirmUpdUser END "
                            cmdstring &= vbCrLf & " , FDStateFirstMatConfirmUpdDate=CASE WHEN ISNULL(FDStateFirstMatConfirmUpdDate,'') =''  THEN " & HI.UL.ULDate.FormatDateDB & " ELSE FDStateFirstMatConfirmUpdDate  END "
                            cmdstring &= vbCrLf & ", FTStateFirstMatConfirmUpdTime=CASE WHEN ISNULL(FTStateFirstMatConfirmUpdTime,'') =''  THEN " & HI.UL.ULDate.FormatTimeDB & " ELSE FTStateFirstMatConfirmUpdTime END "

                            cmdstring &= vbCrLf & " ,FTStateMatConfirmUpdUser = CASE WHEN ISNULL(FTStateFirstMatConfirmUpdUser,'') <>''  THEN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' ELSE  '' END "
                            cmdstring &= vbCrLf & " , FDStateMatConfirmUpdDate=CASE WHEN ISNULL(FDStateFirstMatConfirmUpdDate,'') <>''  THEN " & HI.UL.ULDate.FormatDateDB & " ELSE ''  END "
                            cmdstring &= vbCrLf & ", FTStateMatConfirmUpdTime=CASE WHEN ISNULL(FTStateFirstMatConfirmUpdTime,'') <>''  THEN " & HI.UL.ULDate.FormatTimeDB & " ELSE '' END "

                            cmdstring &= vbCrLf & ",FTStateActive= '1'"
                        End If
                        cmdstring &= vbCrLf & ",FTStateCalMRP='0'"

                    Case "FNConSmpPlus"
                        mFoundStatePackPer = True

                        cmdstring &= vbCrLf & ",FTStateMatConfirm='0',FTStateCalMRP='0'"

                        If Val(pData(I).FieldValue) > 0 Then
                            cmdstring &= vbCrLf & ",FTStateHemNotOptiplan= '1'"
                        Else
                            cmdstring &= vbCrLf & ",FTStateHemNotOptiplan= '0'"
                        End If

                    Case "FNOrderSetType"

                        cmdstring &= vbCrLf & ",FTStateMatConfirm='0',FTStateCalMRP='0'"
                        cmdstring &= vbCrLf & ",FNConSmpSplit= Convert(numeric(18,5), ISNULL(FNConSmp,0) / CASE WHEN " & pData(I).FieldValue & "=3 THEN 2.0 ELse 1.0 END ) "

                    Case "FNConSmp"

                        cmdstring &= vbCrLf & ",FTStateMatConfirm='0',FTStateCalMRP='0'"
                        cmdstring &= vbCrLf & ",FNConSmpSplit= Convert(numeric(18,5), " & Val(pData(I).FieldValue) & " / CASE WHEN FNOrderSetType=3 THEN 2.0 ELse 1.0 END ) "

                    Case "FNPackPerCarton"

                        mFoundStatePackPer = True
                        cmdstring &= vbCrLf & ",FTStateMatConfirm='0',FTStateCalMRP='0'"

                        If Val(pData(I).FieldValue) > 0 Then
                            cmdstring &= vbCrLf & ",FNConSmp= Convert(numeric(18,5), 1.0 / " & Val(pData(I).FieldValue) & " ) "
                            cmdstring &= vbCrLf & ",FNConSmpSplit= Convert(numeric(18,5),  Convert(numeric(18,5), 1.0 / " & Val(pData(I).FieldValue) & " ) / CASE WHEN FNOrderSetType=3 THEN 2.0 ELse 1.0 END ) "
                        End If


                    Case "FNRepeatLengthCM"

                        cmdstring &= vbCrLf & ",FTStateMatConfirm='0',FTStateCalMRP='0'"

                        cmdstring &= vbCrLf & ",FNRepeatConvert=   CASE WHEN " & Val(pData(I).FieldValue) & "=  0.0 THEN 0.0 ELSE   CASE WHEN  ISNULL((select top 1 X.FNHSysUnitId from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS X With(NOLOCK) WHERE X.FTUnitCode='CM'),1311090002) = BOM.FNHSysUnitId THEN  1.0  ELSE   ISNULL(( "

                        cmdstring &= vbCrLf & "  SELECT      TOP 1   Convert(numeric(18,10),CASE WHEN FNRateFrom > 1 THEN FNRateTo/FNRateFrom  ELSE  FNRateFrom * FNRateTo END)  As  FNConvRatio "
                        cmdstring &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert  WITH (NOLOCK) "
                        cmdstring &= vbCrLf & "  WHERE FNHSysUnitId =BOM.FNHSysUnitId "
                        cmdstring &= vbCrLf & "  AND FNHSysUnitIdTo =ISNULL((select top 1 X.FNHSysUnitId from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS X With(NOLOCK) WHERE X.FTUnitCode='CM'),1311090002) "


                        cmdstring &= vbCrLf & " ),0)  End    End "



                    Case "FNHSysUnitId"
                        cmdstring &= vbCrLf & ",FTStateMatConfirm='0',FTStateCalMRP='0'"

                        cmdstring &= vbCrLf & ",FNRepeatConvert=   CASE WHEN ISNULL(FNRepeatLengthCM,0) =  0.0 THEN 0.0 ELSE   CASE WHEN  ISNULL((select top 1 X.FNHSysUnitId from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS X With(NOLOCK) WHERE X.FTUnitCode='CM'),1311090002) = " & Val(pData(I).FieldValue) & " THEN  1.0  ELSE   ISNULL(( "

                        cmdstring &= vbCrLf & "  SELECT      TOP 1   Convert(numeric(18,10),CASE WHEN UV.FNRateFrom > 1 THEN UV.FNRateTo/UV.FNRateFrom  ELSE  UV.FNRateFrom * UV.FNRateTo END)  As  FNConvRatio "
                        cmdstring &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert AS UV  WITH (NOLOCK) "
                        cmdstring &= vbCrLf & "  WHERE UV.FNHSysUnitId =" & Val(pData(I).FieldValue) & " "
                        cmdstring &= vbCrLf & "  AND UV.FNHSysUnitIdTo =ISNULL((select top 1 X.FNHSysUnitId from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS X With(NOLOCK) WHERE X.FTUnitCode='CM'),1311090002) "


                        cmdstring &= vbCrLf & " ),0)  End    End "
                    Case "FTStateCalMRP"

                    Case "FTStateExportOptiplan"
                    Case "FTRunColor"
                        mStateRunColor = True
                    Case Else
                        cmdstring &= vbCrLf & ",FTStateMatConfirm='0',FTStateCalMRP='0'"
                End Select


            Next

            cmdstring &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat  AS BOM  "
            cmdstring &= vbCrLf & " WHERE BOM.FNHSysBomId=" & pBomID & " "
            cmdstring &= vbCrLf & " AND  BOM.FNSeq=" & BomSeq & "  "
            cmdstring &= vbCrLf & " SET @Rec = @@ROWCOUNT  "
            cmdstring &= vbCrLf & "  Select  Top 1   @Rec AS FNState, FNSeq,FTInsUser, FDInsDate, FTInsTime,FNMatSeq ,FTUpdUser, FDUpdDate, FTUpdTime,FTStateMatConfirm ,FTStateCalMRP,FTStateExportOptiplan,FTStateActive "

            If mFoundStateComfirm = True Then
                cmdstring &= vbCrLf & " , FTStateFirstMatConfirmUpdUser,FDStateFirstMatConfirmUpdDate,FTStateFirstMatConfirmUpdTime,FTStateMatConfirmUpdUser,FDStateMatConfirmUpdDate,FTStateMatConfirmUpdTime"
            End If

            If mFoundStatePackPer = True Then
                cmdstring &= vbCrLf & " ,FNConSmp,FNConSmpPlus,FTStateHemNotOptiplan"
            End If

            cmdstring &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat  AS TR  WITH(NOLOCK)  "
            cmdstring &= vbCrLf & " WHERE FNHSysBomId=" & pBomID & " "
            cmdstring &= vbCrLf & " AND  FNSeq=" & BomSeq & "  "

            Dim mdt As DataTable
            mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

            If mdt.Rows.Count > 0 Then

                If Val(mdt.Rows(0)!FNState.ToString) > 0 Then

                    If mStateRunColor = False Then
                        With Me.ogvmat

                            For Each Rxp As DataRow In mdt.Rows

                                .SetFocusedRowCellValue("FNSeq", Val(Rxp!FNSeq.ToString))
                                .SetFocusedRowCellValue("FTUpdUser", Rxp!FTUpdUser.ToString)
                                .SetFocusedRowCellValue("FDUpdDate", Rxp!FDUpdDate.ToString)
                                .SetFocusedRowCellValue("FTUpdTime", Rxp!FTUpdTime.ToString)
                                .SetFocusedRowCellValue("FTStateMatConfirm", Rxp!FTStateMatConfirm.ToString)
                                .SetFocusedRowCellValue("FTStateCalMRP", Rxp!FTStateCalMRP.ToString)
                                .SetFocusedRowCellValue("FTStateExportOptiplan", Rxp!FTStateExportOptiplan.ToString)
                                .SetFocusedRowCellValue("FTStateActive", Rxp!FTStateActive.ToString)


                                If mFoundStateComfirm = True Then

                                    .SetFocusedRowCellValue("FTStateFirstMatConfirmUpdUser", Rxp!FTStateFirstMatConfirmUpdUser.ToString)
                                    .SetFocusedRowCellValue("FDStateFirstMatConfirmUpdDate", Rxp!FDStateFirstMatConfirmUpdDate.ToString)
                                    .SetFocusedRowCellValue("FTStateFirstMatConfirmUpdTime", Rxp!FTStateFirstMatConfirmUpdTime.ToString)

                                    .SetFocusedRowCellValue("FTStateMatConfirmUpdUser", Rxp!FTStateMatConfirmUpdUser.ToString)
                                    .SetFocusedRowCellValue("FDStateMatConfirmUpdDate", Rxp!FDStateMatConfirmUpdDate.ToString)
                                    .SetFocusedRowCellValue("FTStateMatConfirmUpdTime", Rxp!FTStateMatConfirmUpdTime.ToString)

                                End If


                                If mFoundStatePackPer = True Then

                                    .SetFocusedRowCellValue("FNConSmp", Rxp!FNConSmp.ToString)
                                    .SetFocusedRowCellValue("FNConSmpPlus", Rxp!FNConSmpPlus.ToString)

                                    .SetFocusedRowCellValue("FTStateHemNotOptiplan", Rxp!FTStateHemNotOptiplan.ToString)

                                End If

                            Next

                        End With
                        StateReLoad = True
                    End If


                    mdt.Dispose()


                    Return True
                Else
                    mdt.Dispose()
                    Return False
                End If


            Else
                mdt.Dispose()
                Return False
            End If


        Catch ex As Exception
            Return False
        End Try

    End Function



    Private Function UpdateBOMItemMerge(pBomID As Integer, Mat As Integer, pData As List(Of BOMData)) As Boolean
        Dim cmdstring As String = ""


        Try
            Dim StateUpdate1 As Boolean = False
            Dim StateUpdate2 As Boolean = False
            Dim NewMatID As Integer = 0
            Dim StateMat As Boolean = False
            cmdstring = " Declare @Rec int = 0 "
            cmdstring &= vbCrLf & " UPDATE  BOM   SET  "
            cmdstring &= vbCrLf & " FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            cmdstring &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
            cmdstring &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""


            For I As Integer = 0 To pData.Count - 1
                If pData(I).FieldDataType = BOMDataType.DataInteger Then
                    cmdstring &= vbCrLf & ", " & pData(I).FieldName & "=" & pData(I).FieldValue & ""
                Else
                    cmdstring &= vbCrLf & " ," & pData(I).FieldName & " ='" & HI.UL.ULF.rpQuoted(pData(I).FieldValue) & "' "
                End If

                Select Case pData(I).FieldName
                    Case "FNHSysMainMatId"
                        StateMat = True
                        NewMatID = Val(pData(I).FieldValue)
                    Case "FNHSysUnitId"
                        If StateUpdate1 = False Then
                            cmdstring &= vbCrLf & ",FNRepeatConvert=   CASE WHEN ISNULL(FNRepeatLengthCM,0) =  0.0 THEN 0.0 ELSE   CASE WHEN  ISNULL((select top 1 X.FNHSysUnitId from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS X With(NOLOCK) WHERE X.FTUnitCode='CM'),1311090002) = " & Val(pData(I).FieldValue) & " THEN  1.0  ELSE   ISNULL(( "

                            cmdstring &= vbCrLf & "  SELECT      TOP 1   Convert(numeric(18,10),CASE WHEN UV.FNRateFrom > 1 THEN UV.FNRateTo/UV.FNRateFrom  ELSE  UV.FNRateFrom * UV.FNRateTo END)  As  FNConvRatio "
                            cmdstring &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert AS UV  WITH (NOLOCK) "
                            cmdstring &= vbCrLf & "  WHERE UV.FNHSysUnitId =" & Val(pData(I).FieldValue) & " "
                            cmdstring &= vbCrLf & "  AND UV.FNHSysUnitIdTo =ISNULL((select top 1 X.FNHSysUnitId from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS X With(NOLOCK) WHERE X.FTUnitCode='CM'),1311090002) "


                            cmdstring &= vbCrLf & " ),0)  End    End "
                        End If




                        If StateUpdate2 = False Then
                            cmdstring &= vbCrLf & ",FTStateMatConfirm='0',FTStateCalMRP='0'"
                            StateUpdate2 = True
                        End If

                    Case Else
                        If StateUpdate2 = False Then
                            cmdstring &= vbCrLf & ",FTStateMatConfirm='0',FTStateCalMRP='0'"
                            StateUpdate2 = True
                        End If


                End Select


            Next

            cmdstring &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat  AS BOM  "
            cmdstring &= vbCrLf & " WHERE BOM.FNHSysBomId=" & pBomID & " "
            cmdstring &= vbCrLf & " AND  BOM.FNHSysMainMatId=" & Mat & "  "
            cmdstring &= vbCrLf & " SET @Rec = @@ROWCOUNT  "
            cmdstring &= vbCrLf & "  Select    @Rec AS FNState, FNSeq,FTInsUser, FDInsDate, FTInsTime,FNMatSeq ,FTUpdUser, FDUpdDate, FTUpdTime,FTStateMatConfirm,FTStateCalMRP "


            cmdstring &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat  AS TR  WITH(NOLOCK)  "
            cmdstring &= vbCrLf & " WHERE FNHSysBomId=" & pBomID & " "

            If StateMat Then
                cmdstring &= vbCrLf & " AND  FNHSysMainMatId=" & NewMatID & "  "
            Else
                cmdstring &= vbCrLf & " AND  FNHSysMainMatId=" & Mat & "  "
            End If


            Dim mdt As DataTable
            mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

            If mdt.Rows.Count > 0 Then

                If Val(mdt.Rows(0)!FNState.ToString) > 0 Then

                    With Me.ogvmat

                        For Each Rxp As DataRow In mdt.Rows

                            .SetFocusedRowCellValue("FNSeq", Val(Rxp!FNSeq.ToString))
                            .SetFocusedRowCellValue("FTUpdUser", Rxp!FTUpdUser.ToString)
                            .SetFocusedRowCellValue("FDUpdDate", Rxp!FDUpdDate.ToString)
                            .SetFocusedRowCellValue("FTUpdTime", Rxp!FTUpdTime.ToString)
                            .SetFocusedRowCellValue("FTStateMatConfirm", Rxp!FTStateMatConfirm.ToString)
                            .SetFocusedRowCellValue("FTStateCalMRP", Rxp!FTStateCalMRP.ToString)



                        Next

                    End With


                    mdt.Dispose()

                    StateReLoad = True
                    Return True
                Else
                    mdt.Dispose()
                    Return False
                End If


            Else
                mdt.Dispose()
                Return False
            End If




        Catch ex As Exception
            Return False
        End Try

    End Function


    Private Function UpdateBOMColor(pBomID As Integer, BomSeq As Integer, pColorWay As String, pData As List(Of BOMData), Optional StateUpdateColorName As Boolean = False) As Boolean
        Dim cmdstring As String = ""


        Try
            Dim mFoundStateComfirm As Boolean = False
            Dim mFoundStatePackPer As Boolean = False

            cmdstring = " Declare @Rec int = 0 "
            cmdstring &= vbCrLf & " UPDATE  BOM   SET  "
            cmdstring &= vbCrLf & " FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            cmdstring &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
            cmdstring &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""


            For I As Integer = 0 To pData.Count - 1

                If StateUpdateColorName = False Then

                    If pData(I).FieldDataType = BOMDataType.DataInteger Then
                        cmdstring &= vbCrLf & ", " & pData(I).FieldName & "=" & pData(I).FieldValue & ""
                    Else
                        cmdstring &= vbCrLf & " ," & pData(I).FieldName & " ='" & HI.UL.ULF.rpQuoted(pData(I).FieldValue) & "' "
                    End If

                End If

                Select Case pData(I).FieldName


                    Case "FNHSysRawMatColorId"
                        cmdstring &= vbCrLf & ",FTRawMatColorNameTH= '" & HI.UL.ULF.rpQuoted(pData(I).FieldValueTH) & "' "
                        cmdstring &= vbCrLf & ",FTRawMatColorNameEN= '" & HI.UL.ULF.rpQuoted(pData(I).FieldValueEN) & "' "

                    Case "FTColorNote"
                        cmdstring &= vbCrLf & ", FTFTColorNoteUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        cmdstring &= vbCrLf & " , FDFTColorNoteUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                        cmdstring &= vbCrLf & ", FTFTColorNoteUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""

                End Select


            Next

            cmdstring &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Colorway  AS BOM  "
            cmdstring &= vbCrLf & " WHERE BOM.FNHSysBomId=" & pBomID & " "
            cmdstring &= vbCrLf & " AND  BOM.FNSeq=" & BomSeq & "  "

            If pColorWay <> "" Then
                cmdstring &= vbCrLf & " AND  BOM.FTColorway='" & HI.UL.ULF.rpQuoted(pColorWay) & "'  "
            End If


            cmdstring &= vbCrLf & " SET @Rec = @@ROWCOUNT  "
            cmdstring &= vbCrLf & "  Select  Top 1   @Rec AS FNState "

            cmdstring &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Colorway  AS TR  WITH(NOLOCK)  "
            cmdstring &= vbCrLf & " WHERE FNHSysBomId=" & pBomID & " "
            cmdstring &= vbCrLf & " AND  FNSeq=" & BomSeq & "  "

            If pColorWay <> "" Then
                cmdstring &= vbCrLf & " AND  FTColorway='" & HI.UL.ULF.rpQuoted(pColorWay) & "'  "
            End If


            Dim mdt As DataTable
            mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

            If mdt.Rows.Count > 0 Then

                If Val(mdt.Rows(0)!FNState.ToString) > 0 Then


                    Return True
                Else
                    mdt.Dispose()
                    Return False
                End If

            Else
                mdt.Dispose()
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function UpdateBOMSize(pBomID As Integer, BomSeq As Integer, pSizeBD As String, pData As List(Of BOMData)) As Boolean
        Dim cmdstring As String = ""


        Try
            Dim mFoundStateComfirm As Boolean = False
            Dim mFoundStatePackPer As Boolean = False

            cmdstring = " Declare @Rec int = 0 "
            cmdstring &= vbCrLf & " UPDATE  BOM   SET  "
            cmdstring &= vbCrLf & " FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            cmdstring &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
            cmdstring &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""


            For I As Integer = 0 To pData.Count - 1
                If pData(I).FieldDataType = BOMDataType.DataInteger Then
                    cmdstring &= vbCrLf & ", " & pData(I).FieldName & "=" & pData(I).FieldValue & ""
                Else
                    cmdstring &= vbCrLf & " ," & pData(I).FieldName & " ='" & HI.UL.ULF.rpQuoted(pData(I).FieldValue) & "' "
                End If

                Select Case pData(I).FieldName


                    Case "FNHSysRawMatSizeId"


                    Case "FTSizeNote"
                        cmdstring &= vbCrLf & ", FTSizeNoteUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        cmdstring &= vbCrLf & " , FDSizeNoteUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                        cmdstring &= vbCrLf & ", FTSizeNoteUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""

                End Select


            Next

            cmdstring &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Sizebreakdown  AS BOM  "
            cmdstring &= vbCrLf & " WHERE BOM.FNHSysBomId=" & pBomID & " "
            cmdstring &= vbCrLf & " AND  BOM.FNSeq=" & BomSeq & "  "
            cmdstring &= vbCrLf & " AND  BOM.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(pSizeBD) & "'  "

            cmdstring &= vbCrLf & " SET @Rec = @@ROWCOUNT  "
            cmdstring &= vbCrLf & "  Select  Top 1   @Rec AS FNState "

            cmdstring &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Sizebreakdown  AS TR  WITH(NOLOCK)  "
            cmdstring &= vbCrLf & " WHERE FNHSysBomId=" & pBomID & " "
            cmdstring &= vbCrLf & " AND  FNSeq=" & BomSeq & "  "
            cmdstring &= vbCrLf & " AND  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(pSizeBD) & "'  "

            Dim mdt As DataTable
            mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

            If mdt.Rows.Count > 0 Then

                If Val(mdt.Rows(0)!FNState.ToString) > 0 Then


                    Return True
                Else
                    mdt.Dispose()
                    Return False
                End If


            Else
                mdt.Dispose()
                Return False
            End If




        Catch ex As Exception
            Return False
        End Try

    End Function



    Private Sub ogvmat_ShowingEditor(sender As Object, e As CancelEventArgs) Handles ogvmat.ShowingEditor
        Try

            OldValue = ""
            NewValue = ""

            If StateEdit Then

                With Me.ogvmat
                    Select Case .FocusedColumn.FieldName
                        Case "FTSelect"
                            e.Cancel = False
                        Case "FTMainMatCode", "FNMatSeq"

                            e.Cancel = False
                            OldValue = .GetFocusedRowCellValue(.FocusedColumn.FieldName).ToString
                        Case "FTDTMNote"
                            If .GetFocusedRowCellValue("FTStateDTM").ToString = "1" Then
                                e.Cancel = False
                                OldValue = .GetFocusedRowCellValue(.FocusedColumn.FieldName).ToString
                            Else
                                e.Cancel = True
                            End If

                        Case "FTSubOrderNo"

                            If .GetFocusedRowCellValue("FTOrderNo").ToString = "ALL" Then
                                e.Cancel = True

                            Else
                                e.Cancel = False
                                OldValue = .GetFocusedRowCellValue(.FocusedColumn.FieldName).ToString
                            End If

                        Case Else
                            If .GetFocusedRowCellValue("FTMainMatCode").ToString = "" Then

                                e.Cancel = True

                            Else
                                e.Cancel = False
                                OldValue = .GetFocusedRowCellValue(.FocusedColumn.FieldName).ToString
                            End If
                    End Select

                End With

            Else
                e.Cancel = True
            End If

        Catch ex As Exception
            OldValue = ""
            e.Cancel = True
        End Try
    End Sub

    Private Sub ogvmatcolor_ShowingEditor(sender As Object, e As CancelEventArgs) Handles ogvmatcolor.ShowingEditor
        Try

            OldValue = ""
            NewValue = ""

            If StateEdit Then

                With Me.ogvmatcolor
                    Select Case .FocusedColumn.FieldName
                        Case "FTMainMatCode", "FNMatSeq"
                            e.Cancel = False

                            OldValue = .GetFocusedRowCellValue(.FocusedColumn.FieldName).ToString
                        Case Else
                            If .GetFocusedRowCellValue("FTMainMatCode").ToString = "" Then

                                e.Cancel = True

                            Else
                                e.Cancel = False
                                OldValue = .GetFocusedRowCellValue(.FocusedColumn.FieldName).ToString
                            End If
                    End Select

                End With

            Else
                e.Cancel = True
            End If

        Catch ex As Exception
            OldValue = ""
            e.Cancel = True
        End Try
    End Sub

    Private Sub ogvmatcolornote_ShowingEditor(sender As Object, e As CancelEventArgs)
        Try

            OldValue = ""
            NewValue = ""

            If StateEdit Then

                With Me.ogvmatcolornote
                    Select Case .FocusedColumn.FieldName
                        Case "FTMainMatCode", "FNMatSeq"
                            e.Cancel = False

                            OldValue = .GetFocusedRowCellValue(.FocusedColumn.FieldName).ToString
                        Case Else
                            If .GetFocusedRowCellValue("FTMainMatCode").ToString = "" Then

                                e.Cancel = True

                            Else
                                e.Cancel = False
                                OldValue = .GetFocusedRowCellValue(.FocusedColumn.FieldName).ToString
                            End If
                    End Select

                End With

            Else
                e.Cancel = True
            End If

        Catch ex As Exception
            OldValue = ""
            e.Cancel = True
        End Try
    End Sub

    Private Sub ogvmatsize_ShowingEditor(sender As Object, e As CancelEventArgs) Handles ogvmatsize.ShowingEditor
        Try

            OldValue = ""
            NewValue = ""

            If StateEdit Then

                With Me.ogvmatsize
                    Select Case .FocusedColumn.FieldName
                        Case "FTMainMatCode", "FNMatSeq"
                            e.Cancel = False

                            OldValue = .GetFocusedRowCellValue(.FocusedColumn.FieldName).ToString
                        Case Else
                            If .GetFocusedRowCellValue("FTMainMatCode").ToString = "" Then

                                e.Cancel = True

                            Else
                                e.Cancel = False
                                OldValue = .GetFocusedRowCellValue(.FocusedColumn.FieldName).ToString
                            End If
                    End Select

                End With

            Else
                e.Cancel = True
            End If

        Catch ex As Exception
            OldValue = ""
            e.Cancel = True
        End Try
    End Sub

    Private Sub ogvmatsilkcolor_ShowingEditor(sender As Object, e As CancelEventArgs) Handles ogvmatsilkcolor.ShowingEditor
        Try

            OldValue = ""
            NewValue = ""

            If StateEdit Then

                With Me.ogvmatsilkcolor
                    Select Case .FocusedColumn.FieldName
                        Case "FTMainMatCode", "FNMatSeq"
                            e.Cancel = False

                            OldValue = .GetFocusedRowCellValue(.FocusedColumn.FieldName).ToString
                        Case Else
                            If .GetFocusedRowCellValue("FTMainMatCode").ToString = "" Then

                                e.Cancel = True

                            Else
                                e.Cancel = False
                                OldValue = .GetFocusedRowCellValue(.FocusedColumn.FieldName).ToString
                            End If
                    End Select

                End With

            Else
                e.Cancel = True
            End If

        Catch ex As Exception
            OldValue = ""
            e.Cancel = True
        End Try
    End Sub


    Private Sub ReposFNMerMatSeq_Leave(sender As Object, e As EventArgs) Handles ReposFNMerMatSeq.Leave
        Try
            With CType(sender, DevExpress.XtraEditors.CalcEdit)
                Dim pValue As Decimal = Decimal.Parse(Format(Val(.Value.ToString), "0.00"))
                Dim pSeq As Integer = Val(Me.ogvmat.GetFocusedRowCellValue("FNSeq").ToString)
                Dim pItem As String = Me.ogvmat.GetFocusedRowCellValue("FTMainMatCode").ToString

                With CType(Me.ogcmat.DataSource, DataTable)
                    .AcceptChanges()

                    If .Select("FTMainMatCode<>'" & pItem & "' AND FNMatSeq=" & pValue & "").Length > 0 Then
                        Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, Val(OldValue))
                        Exit Sub
                    End If

                End With


                If Val(OldValue) <> pValue And pSeq > 0 And pItem <> "" Then


                    pBOMListUpdate.Clear()

                    Dim pBOM As New BOMData
                    pBOM.FieldName = Me.ogvmat.FocusedColumn.FieldName
                    pBOM.FieldValue = pValue
                    pBOM.FieldDataType = BOMDataType.DataInteger
                    pBOMListUpdate.Add(pBOM)


                    If UpdateBOMItem(BOMSysID, pSeq, pBOMListUpdate) = False Then
                        Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, Val(OldValue))
                    End If

                    pBOMListUpdate.Clear()

                    StateReLoad = True

                End If

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReposFNMerMatSeq_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ReposFNMerMatSeq.EditValueChanging
        Try

            If Val(e.NewValue.ToString) < 0 Then
                e.Cancel = True
            Else
                e.Cancel = False
            End If

        Catch ex As Exception
            e.Cancel = True
        End Try
    End Sub

    Private Sub RepositoryItemGridLookUpEditMainMatCode_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemGridLookUpEditMainMatCode.EditValueChanging

        Try

            With Me.ogvmat
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim pMatSeq As Decimal = Val(.GetFocusedRowCellValue("FNMatSeq").ToString)

                With CType(Me.ogcmat.DataSource, DataTable)
                    If .Select("FTMainMatCode='" & HI.UL.ULF.rpQuoted(e.NewValue.ToString) & "' AND FNMatSeq <> " & pMatSeq & "").Length > 0 Then
                        HI.MG.ShowMsg.mInfo("พบรายกา Material นี้แล้ว ไม่สามารถเพิ่มซ้ำได้ !!!", 2201445578, Me.Text,, MessageBoxIcon.Warning)

                        e.Cancel = True

                    Else

                        'Try

                        '    ' RepositoryItemGridLookUpEditMainMatCode_EditValueChanged(sender, New EventArgs)

                        'Catch ex As Exception
                        'End Try



                        Try
                            Dim MatCode As String = ""
                            With Me.ogvmat
                                If .FocusedRowHandle < 0 Then Exit Sub

                                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)

                                Dim mNominate As String = "0"
                                Dim mMainMaterial As String = "0"
                                Dim pText As String = e.NewValue.ToString
                                Dim mMatId As Integer = 0
                                Dim pMatName As String = ""

                                'mMatId = Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysMainMatId").ToString())
                                'MatCode = (obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTMainMatCode").ToString())

                                'pMatName = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTMainMatName").ToString


                                With CType(obj.Properties.DataSource, DataTable)

                                    For Each Rx As DataRow In .Select("FTMainMatCode='" & HI.UL.ULF.rpQuoted(pText) & "'")
                                        MatCode = Rx!FTMainMatCode.ToString
                                        mMatId = Val((Rx!FNHSysMainMatId.ToString))
                                        pMatName = Rx!FTMainMatName.ToString
                                    Next

                                End With


                                Dim pSuplCode As String = "" 'obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTSuplCode").ToString

                                Dim mCurCode As String = "" 'obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTCurCode").ToString
                                Dim mUnitCode As String = "" 'obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTUnitCode").ToString

                                Dim mPrice As Decimal = 0.0 'Decimal.Parse(Format(Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNPrice").ToString), "0.0000"))

                                Dim mFrontSize As String = "" 'obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTFabricFrontSize").ToString


                                Dim pStateDefualtBOMColorByColorway As String = "0" ' obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTStateDefualtBOMColorByColorway").ToString
                                Dim pStateDefualtBOMSizeBySizeBreakdown As String = "0" 'obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTStateDefualtBOMSizeBySizeBreakdown").ToString
                                Dim pStateSilk As String = "0" 'obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTStateSilk").ToString

                                'If obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTStateNominate").ToString = "1" Then
                                '    mNominate = "1"
                                'End If

                                'If obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTStateMainMaterial").ToString = "1" Then
                                '    mMainMaterial = "1"
                                'End If

                                Dim mSuplId As Integer = 0 'Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysSuplId").ToString)
                                Dim mUnitId As Integer = 0 'Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysUnitId").ToString)
                                Dim mCurId As Integer = 0 'Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysCurId").ToString)
                                Dim mdefaultcolor As Integer = 0
                                Dim mdefaultsize As Integer = 0

                                Dim cmdstring As String = ""

                                cmdstring = " SELECT    TOP 1      A.FNHSysMainMatId,A.FTMainMatCode "
                                cmdstring &= vbCrLf & "  , A.FTStateNominate, A.FNHSysUnitId, A.FNHSysCurId"
                                cmdstring &= vbCrLf & "  , A.FNPrice, A.FTStateMainMaterial, A.FTStateActive, A.FNHSysSuplId, S.FTSuplCode, U.FTUnitCode, C.FTCurCode "
                                cmdstring &= vbCrLf & " , A.FTFabricFrontSize,A.FTStateDefualtBOMColorByColorway,A.FTStateDefualtBOMSizeBySizeBreakdown,A.FTStateSilk,A.FNHSysRawMatColorId,A.FNHSysRawMatSizeId "
                                cmdstring &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat As A WITH(NOLOCK) LEFT OUTER Join "
                                cmdstring &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency As C WITH(NOLOCK) On A.FNHSysCurId = C.FNHSysCurId LEFT OUTER Join "
                                cmdstring &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit As U WITH(NOLOCK) On A.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER Join "
                                cmdstring &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier As S WITH(NOLOCK) On A.FNHSysSuplId = S.FNHSysSuplId "
                                cmdstring &= vbCrLf & "  Where A.FTStateActive ='1'  AND   A.FNHSysMainMatId =" & mMatId & " "
                                cmdstring &= vbCrLf & "  ORDER BY A.FTMainMatCode  "


                                Dim dtmat As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)
                                For Each Rmx As DataRow In dtmat.Rows
                                    pSuplCode = Rmx!FTSuplCode.ToString
                                    mCurCode = Rmx!FTCurCode.ToString
                                    mUnitCode = Rmx!FTUnitCode.ToString
                                    mPrice = Decimal.Parse(Format(Val(Rmx!FNPrice.ToString), "0.0000"))
                                    mFrontSize = Rmx!FTFabricFrontSize.ToString

                                    If Rmx!FTStateDefualtBOMColorByColorway.ToString = "1" Then
                                        pStateDefualtBOMColorByColorway = "1"
                                    End If

                                    If Rmx!FTStateDefualtBOMSizeBySizeBreakdown.ToString = "1" Then
                                        pStateDefualtBOMSizeBySizeBreakdown = "1"
                                    End If


                                    If Rmx!FTStateSilk.ToString = "1" Then
                                        pStateSilk = "1"
                                    End If

                                    If Rmx!FTStateNominate.ToString = "1" Then
                                        mNominate = "1"
                                    End If

                                    If Rmx!FTStateMainMaterial.ToString = "1" Then
                                        mMainMaterial = "1"
                                    End If

                                    mSuplId = Val(Rmx!FNHSysSuplId.ToString)
                                    mUnitId = Val(Rmx!FNHSysUnitId.ToString)
                                    mCurId = Val(Rmx!FNHSysCurId.ToString)
                                    mdefaultcolor = Val(Rmx!FNHSysRawMatColorId.ToString)
                                    mdefaultsize = Val(Rmx!FNHSysRawMatSizeId.ToString)
                                Next

                                dtmat.Dispose()
                                Dim mStateActive As String = .GetFocusedRowCellValue("FTStateActive").ToString
                                Dim mPart As Integer = Val(.GetFocusedRowCellValue("FNPart").ToString)
                                Dim mMatSeq As Decimal = Decimal.Parse(Format(Val(.GetFocusedRowCellValue("FNMatSeq").ToString), "0.00"))



                                If Val(.GetFocusedRowCellValue("FNSeq").ToString) = 0 Then


                                    cmdstring = " Declare @Seq int = 0 "
                                    cmdstring &= vbCrLf & " Declare @Rec int = 0 "
                                    cmdstring &= vbCrLf & " SET @Seq = ISNULL((select max(FNSeq) FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat WHERE  FNHSysBomId =" & BOMSysID & " ),0) +1 "
                                    cmdstring &= vbCrLf & " insert into  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat  (FTInsUser, FDInsDate, FTInsTime, FNHSysBomId "
                                    cmdstring &= vbCrLf & " , FNSeq, FNMatSeq, FNHSysMainMatId, FTFabricFrontSize, FNPart, FNHSysSuplId, FTStateNominate, FNHSysUnitId, FNHSysCurId, FNPrice, FTStateActive, FTStateMainMaterial, FNConSmp, FNConSmpPlus"
                                    cmdstring &= vbCrLf & " ,    FNOrderSetType,FTStateCombination, FTPart, FTComponent, FTStateFree, FTStateHemNotOptiplan, FNRepeatLengthCM, FNRepeatConvert"
                                    cmdstring &= vbCrLf & " , FNPackPerCarton, FNConSmpSplit, FTStateDTM,  FTDTMNote, FTNote, FTStateMatConfirm, FTRunColor, FTRunSize,FTSilkName,FTStateSilk,FTStateCalMRP,FTStateExportOptiplan)"
                                    cmdstring &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                    cmdstring &= vbCrLf & "," & BOMSysID & " "
                                    cmdstring &= vbCrLf & ",@Seq "
                                    cmdstring &= vbCrLf & " ," & mMatSeq & ""
                                    cmdstring &= vbCrLf & " ," & mMatId & ""
                                    cmdstring &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(mFrontSize) & "' "
                                    cmdstring &= vbCrLf & " ," & mPart & ""
                                    cmdstring &= vbCrLf & " ," & mSuplId & ""
                                    cmdstring &= vbCrLf & ", '" & mNominate & "' "
                                    cmdstring &= vbCrLf & " ," & mUnitId & ""
                                    cmdstring &= vbCrLf & " ," & mCurId & ""
                                    cmdstring &= vbCrLf & " ," & mPrice & ""
                                    cmdstring &= vbCrLf & ", '" & mStateActive & "' "
                                    cmdstring &= vbCrLf & ", '" & mMainMaterial & "' "


                                    cmdstring &= vbCrLf & ",0 FNConSmp,0 FNConSmpPlus"
                                    cmdstring &= vbCrLf & " ,  0  FNOrderSetType,'" & mMainMaterial & "' FTStateCombination,'' FTPart,'' FTComponent,'0' FTStateFree,'0' FTStateHemNotOptiplan,0 FNRepeatLengthCM,0 FNRepeatConvert"
                                    cmdstring &= vbCrLf & " ,0 FNPackPerCarton,0 FNConSmpSplit,'0' FTStateDTM, '' FTDTMNote,'' FTNote,'0' FTStateMatConfirm,'1','1' "



                                    If pStateSilk = "1" Then
                                        cmdstring &= vbCrLf & ", '" & HI.UL.ULF.rpQuoted(Microsoft.VisualBasic.Left(pMatName, 500)) & "','1' "
                                    Else
                                        cmdstring &= vbCrLf & ", '','0' "
                                    End If
                                    cmdstring &= vbCrLf & ",'0','0' "
                                    cmdstring &= vbCrLf & " SET @Rec = @@ROWCOUNT  "

                                    cmdstring &= vbCrLf & " EXEC  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_GETBOMGARMENT_CHECKDEFUALT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & BOMSysID & ",@Seq,'" & pStateDefualtBOMColorByColorway & "','" & pStateDefualtBOMSizeBySizeBreakdown & "','" & pStateSilk & "'," & mdefaultcolor & "," & mdefaultsize & ", '1' "

                                    'cmdstring &= vbCrLf & "  Select  Top 1 @Rec  AS FNState ,FNSeq,FTInsUser, FDInsDate, FTInsTime,FNMatSeq ,FTUpdUser, FDUpdDate, FTUpdTime,FTStateMatConfirm,FTStateSilk  "


                                    'cmdstring &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat  AS TR  WITH(NOLOCK)  "
                                    'cmdstring &= vbCrLf & " WHERE FNHSysBomId=" & BOMSysID & " "
                                    'cmdstring &= vbCrLf & " AND  FNSeq=@Seq  "




                                Else



                                    cmdstring = " Declare @Seq int = " & Val(.GetFocusedRowCellValue("FNSeq").ToString) & " "
                                    cmdstring &= vbCrLf & " Declare @Rec int = 0 "

                                    cmdstring &= vbCrLf & " UPDATE  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat   SET  "
                                    cmdstring &= vbCrLf & " FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                    cmdstring &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                                    cmdstring &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""

                                    cmdstring &= vbCrLf & " ,FNMatSeq=" & mMatSeq & ""
                                    cmdstring &= vbCrLf & " ,FNHSysMainMatId=" & mMatId & ""
                                    cmdstring &= vbCrLf & ",FTFabricFrontSize= '" & HI.UL.ULF.rpQuoted(mFrontSize) & "' "

                                    cmdstring &= vbCrLf & " ,FNHSysSuplId=" & mSuplId & ""
                                    cmdstring &= vbCrLf & ", FTStateNominate='" & mNominate & "' "
                                    cmdstring &= vbCrLf & " ,FNHSysUnitId=" & mUnitId & ""
                                    cmdstring &= vbCrLf & " ,FNHSysCurId=" & mCurId & ""
                                    cmdstring &= vbCrLf & " ,FNPrice=" & mPrice & ""
                                    cmdstring &= vbCrLf & ", FTStateMainMaterial='" & mMainMaterial & "' "
                                    cmdstring &= vbCrLf & ",FTStateMatConfirm='0'"


                                    If pStateSilk = "1" Then
                                        cmdstring &= vbCrLf & ",FTSilkName= '" & HI.UL.ULF.rpQuoted(Microsoft.VisualBasic.Left(pMatName, 500)) & "' "
                                        cmdstring &= vbCrLf & ", FTStateSilk='1' "
                                    Else
                                        cmdstring &= vbCrLf & ", FTSilkName='' "
                                        cmdstring &= vbCrLf & ", FTStateSilk='0' "
                                    End If

                                    cmdstring &= vbCrLf & ", FTStateCalMRP='0' "
                                    cmdstring &= vbCrLf & ", FTStateExportOptiplan='0' "


                                    cmdstring &= vbCrLf & " WHERE FNHSysBomId=" & BOMSysID & " "
                                    cmdstring &= vbCrLf & " AND  FNSeq=@Seq  "

                                    cmdstring &= vbCrLf & " SET @Rec = @@ROWCOUNT  "

                                    cmdstring &= vbCrLf & " EXEC  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_GETBOMGARMENT_CHECKDEFUALT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & BOMSysID & ",@Seq,'" & pStateDefualtBOMColorByColorway & "','" & pStateDefualtBOMSizeBySizeBreakdown & "','" & pStateSilk & "'," & mdefaultcolor & "," & mdefaultsize & ",'2' "



                                End If


                                cmdstring &= vbCrLf & "  Select  Top 1 @Rec  AS FNState ,FNSeq,FTInsUser, FDInsDate, FTInsTime,FNMatSeq ,FTUpdUser, FDUpdDate, FTUpdTime,FTStateMatConfirm,FTStateSilk ,FTStateCalMRP,FTStateExportOptiplan "


                                cmdstring &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat  AS TR  WITH(NOLOCK)  "
                                cmdstring &= vbCrLf & " WHERE FNHSysBomId=" & BOMSysID & " "
                                cmdstring &= vbCrLf & " AND  FNSeq=@Seq  "



                                Dim mdt As DataTable
                                mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                                If mdt.Rows.Count > 0 Then

                                    If Val(mdt.Rows(0)!FNState.ToString) > 0 Then
                                        For Each Rxp As DataRow In mdt.Rows

                                            .SetFocusedRowCellValue("FTMainMatCode", MatCode)


                                            .SetFocusedRowCellValue("FTMainMatName", pMatName)
                                            .SetFocusedRowCellValue("FTSuplCode", pSuplCode)
                                            .SetFocusedRowCellValue("FTCurCode", mCurCode)
                                            .SetFocusedRowCellValue("FTUnitCode", mUnitCode)
                                            .SetFocusedRowCellValue("FNPrice", mPrice)
                                            .SetFocusedRowCellValue("FTStateNominate", mNominate)
                                            .SetFocusedRowCellValue("FTStateMainMaterial", mMainMaterial)
                                            .SetFocusedRowCellValue("FTFabricFrontSize", mFrontSize)

                                            .SetFocusedRowCellValue("FNHSysMainMatId", mMatId)
                                            .SetFocusedRowCellValue("FNHSysSuplId", mSuplId)
                                            .SetFocusedRowCellValue("FNHSysUnitId", mUnitId)
                                            .SetFocusedRowCellValue("FNHSysCurId", mCurId)


                                            .SetFocusedRowCellValue("FNSeq", Val(Rxp!FNSeq.ToString))
                                            .SetFocusedRowCellValue("FTInsUser", Rxp!FTInsUser.ToString)
                                            .SetFocusedRowCellValue("FDInsDate", Rxp!FDInsDate.ToString)
                                            .SetFocusedRowCellValue("FTInsTime", Rxp!FTInsTime.ToString)
                                            .SetFocusedRowCellValue("FNMatSeq", Val(Rxp!FNMatSeq.ToString))
                                            .SetFocusedRowCellValue("FTUpdUser", Rxp!FTUpdUser.ToString)
                                            .SetFocusedRowCellValue("FDUpdDate", Rxp!FDUpdDate.ToString)
                                            .SetFocusedRowCellValue("FTUpdTime", Rxp!FTUpdTime.ToString)
                                            .SetFocusedRowCellValue("FTStateMatConfirm", Rxp!FTStateMatConfirm.ToString)
                                            .SetFocusedRowCellValue("FTStateSilk", Rxp!FTStateSilk.ToString)
                                            .SetFocusedRowCellValue("FTStateCalMRP", Rxp!FTStateCalMRP.ToString)
                                            .SetFocusedRowCellValue("FTStateExportOptiplan", Rxp!FTStateExportOptiplan.ToString)


                                        Next
                                        Me.ogvmat.FocusedColumn = Me.ogvmat.Columns.ColumnByFieldName("FTMainMatName")

                                        OldValue = MatCode
                                        StateReLoad = True
                                    Else
                                        Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, Val(OldValue))
                                    End If

                                Else
                                    Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, Val(OldValue))
                                End If

                            End With

                        Catch ex As Exception
                        End Try

                        e.Cancel = False

                    End If
                End With

            End With

        Catch ex As Exception
        End Try

    End Sub

    Private Sub RepositoryItemCalcEditFNConSmp_Leave(sender As Object, e As EventArgs) Handles RepositoryItemCalcEditFNConSmp.Leave, RepositoryItemCalcEditFNConSmpPlus.Leave, RepositoryItemCalcEditFNRepeatLengthCM.Leave, RepositoryPriceCalcEdit.Leave, RepositoryFNPackPerCarton.Leave
        Try
            With CType(sender, DevExpress.XtraEditors.CalcEdit)
                Dim pValue As Decimal = .Properties.DisplayFormat.GetDisplayText(.Value)
                Dim pSeq As Integer = Val(Me.ogvmat.GetFocusedRowCellValue("FNSeq").ToString)

                If Val(OldValue) <> pValue And pSeq > 0 Then

                    pBOMListUpdate.Clear()

                    Dim pBOM As New BOMData
                    pBOM.FieldName = Me.ogvmat.FocusedColumn.FieldName
                    pBOM.FieldValue = pValue
                    pBOM.FieldDataType = BOMDataType.DataInteger
                    pBOMListUpdate.Add(pBOM)

                    If UpdateBOMItem(BOMSysID, pSeq, pBOMListUpdate) = False Then

                        Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, Val(OldValue))

                    End If

                    pBOMListUpdate.Clear()

                End If

            End With

        Catch ex As Exception

            Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, Val(OldValue))

        End Try

    End Sub

    Private Sub RepositoryFTStateActive_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryFTStateActive.EditValueChanging

        Try

            Dim pValue As String = "0"
            If e.NewValue.ToString = "1" Then
                pValue = "1"
            End If


            Dim pSeq As Integer = Val(Me.ogvmat.GetFocusedRowCellValue("FNSeq").ToString)

            If (OldValue) <> pValue And pSeq > 0 Then

                Select Case Me.ogvmat.FocusedColumn.FieldName
                    Case "FTStateCalMRP", "FTStateExportOptiplan"
                        If pValue = "1" Then
                            e.Cancel = True
                            Exit Sub
                        End If
                    Case Else
                        e.Cancel = False
                End Select


                pBOMListUpdate.Clear()

                Dim pBOM As New BOMData
                pBOM.FieldName = Me.ogvmat.FocusedColumn.FieldName
                pBOM.FieldValue = pValue
                pBOM.FieldDataType = BOMDataType.DataString
                pBOMListUpdate.Add(pBOM)


                If UpdateBOMItem(BOMSysID, pSeq, pBOMListUpdate) = False Then
                    e.Cancel = True
                    Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, OldValue)
                Else
                    Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, pValue)
                    OldValue = pValue
                    e.Cancel = False
                End If

                pBOMListUpdate.Clear()

            End If



        Catch ex As Exception
            e.Cancel = True
            Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, OldValue)
        End Try

    End Sub

    Private Sub RepositoryItemSelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemSelect.EditValueChanging
        Try

            Dim pValue As String = "0"
            If e.NewValue.ToString = "1" Then
                pValue = "1"
            End If
            e.Cancel = False
            Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, pValue)



        Catch ex As Exception
            e.Cancel = True

        End Try
    End Sub

    Private Sub RepositoryItemMemoExFTDTMNote_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemMemoExFTDTMNote.EditValueChanged, RepositoryItemMemoExEditFTComponent.EditValueChanged
        Try


            Dim pValue As String = ""
            Dim pOldValue As String = ""


            With CType(sender, DevExpress.XtraEditors.MemoExEdit)
                pValue = .EditValue.ToString
                pOldValue = .OldEditValue.ToString
            End With
            Dim pSeq As Integer = Val(Me.ogvmat.GetFocusedRowCellValue("FNSeq").ToString)

            If (OldValue) <> pValue And pSeq > 0 Then

                pBOMListUpdate.Clear()

                Dim pBOM As New BOMData
                pBOM.FieldName = Me.ogvmat.FocusedColumn.FieldName
                pBOM.FieldValue = pValue
                pBOM.FieldDataType = BOMDataType.DataString
                pBOMListUpdate.Add(pBOM)


                If UpdateBOMItem(BOMSysID, pSeq, pBOMListUpdate) = False Then

                    Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, (OldValue))
                Else

                    Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, (pValue))
                End If

                pBOMListUpdate.Clear()

            Else

                Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, (OldValue))
            End If



        Catch ex As Exception

            Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, (OldValue))
        End Try
    End Sub

    Private Sub RepositoryFNOrderSetType_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryFNOrderSetType.EditValueChanged
        Try


            Dim pValue As String = ""
            Dim pOldValue As String = ""


            With CType(sender, DevExpress.XtraEditors.LookUpEdit)
                pValue = .EditValue.ToString
                pOldValue = .OldEditValue.ToString
            End With
            Dim pSeq As Integer = Val(Me.ogvmat.GetFocusedRowCellValue("FNSeq").ToString)

            If (OldValue) <> pValue And pSeq > 0 Then


                pBOMListUpdate.Clear()

                Dim pBOM As New BOMData
                pBOM.FieldName = Me.ogvmat.FocusedColumn.FieldName
                pBOM.FieldValue = pValue
                pBOM.FieldDataType = BOMDataType.DataInteger
                pBOMListUpdate.Add(pBOM)


                If UpdateBOMItem(BOMSysID, pSeq, pBOMListUpdate) = False Then

                    Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, (OldValue))
                Else

                    Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, (pValue))


                End If

                pBOMListUpdate.Clear()

            Else

                Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, (OldValue))
            End If



        Catch ex As Exception

            Me.ogvmat.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, (OldValue))
        End Try
    End Sub

    Private Sub RepFTSelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFTSelect.EditValueChanging
        Try

            Dim xState As String = "0"

            If e.NewValue.ToString = "1" Then
                xState = "1"
            End If
            With Me.ogvpart
                .SetFocusedRowCellValue(.FocusedColumn.FieldName, xState)
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepFTSelectOrder_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFTSelectOrder.EditValueChanging
        Try
            Dim xState As String = "0"

            If e.NewValue.ToString = "1" Then
                xState = "1"
            End If
            With Me.ogvlistorder
                .SetFocusedRowCellValue(.FocusedColumn.FieldName, xState)
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepFTSelectSubOrder_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFTSelectSubOrder.EditValueChanging
        Try
            Dim xState As String = "0"

            If e.NewValue.ToString = "1" Then
                xState = "1"
            End If
            With Me.ogvlistsuborder
                .SetFocusedRowCellValue(.FocusedColumn.FieldName, xState)
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvmat_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles ogvmat.CustomColumnDisplayText
        Try

            Select Case e.Column.FieldName
                Case "FNRepeatLengthCM", "FNPackPerCarton"
                    If Val(e.Value) = 0 Then
                        e.DisplayText = ""
                    End If


            End Select

        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNStateBomOrder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNStateBomOrder.SelectedIndexChanged
        Try

            If StateLoad = False Then
                Exit Sub
            End If

            Dim pValue As String = ""
            Dim pOldValue As String = ""

            pValue = FNStateBomOrder.SelectedIndex

            If (OldValue) <> pValue Then

                pBOMListUpdate.Clear()

                Dim pBOM As New BOMData
                pBOM.FieldName = "FNStateBomOrder"
                pBOM.FieldValue = pValue
                pBOM.FieldDataType = BOMDataType.DataInteger
                pBOMListUpdate.Add(pBOM)

                If UpdateBOMHeader(BOMSysID, pBOMListUpdate) = False Then

                    FNStateBomOrder.SelectedIndex = OldValue

                End If

                pBOMListUpdate.Clear()

            Else

                FNStateBomOrder.SelectedIndex = OldValue
            End If

        Catch ex As Exception

            FNStateBomOrder.SelectedIndex = OldValue
        End Try
    End Sub

    Private Sub FNStateBomOrder_GotFocus(sender As Object, e As EventArgs) Handles FNStateBomOrder.GotFocus
        OldValue = FNStateBomOrder.SelectedIndex
    End Sub



    Private Sub FTRemark_Leave(sender As Object, e As EventArgs) Handles FTRemark.Leave
        Try

            If StateLoad = False Then
                Exit Sub
            End If

            Dim pValue As String = ""
            Dim pOldValue As String = ""



            pValue = FTRemark.Text



            If (OldValue) <> pValue Then


                pBOMListUpdate.Clear()

                Dim pBOM As New BOMData
                pBOM.FieldName = "FTNote"
                pBOM.FieldValue = pValue
                pBOM.FieldDataType = BOMDataType.DataString
                pBOMListUpdate.Add(pBOM)


                If UpdateBOMHeader(BOMSysID, pBOMListUpdate) = False Then

                    FTRemark.Text = OldValue

                End If

                pBOMListUpdate.Clear()

            Else

                FTRemark.Text = OldValue
            End If

        Catch ex As Exception

            FTRemark.Text = OldValue
        End Try
    End Sub

    Private Sub FTRemark_GotFocus(sender As Object, e As EventArgs) Handles FTRemark.GotFocus
        OldValue = FTRemark.Text
    End Sub

    Private Sub FNHSysCustId_GotFocus(sender As Object, e As EventArgs) Handles FNHSysCustId.GotFocus, FTDevelopName.GotFocus
        OldValue = sender.Text
    End Sub

    Private Sub FTDevelopName_EditValueChanged(sender As Object, e As EventArgs) Handles FTDevelopName.EditValueChanged, FNHSysCustId.EditValueChanged

        If StateLoad = False Then
            Exit Sub
        End If

        Try
            Dim MatCode As String = ""


            Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)

            Dim pText As String = obj.Text
            Dim pCode As String = ""
            Dim pName As String = ""
            Dim mlId As String = "0"
            Dim pFiledName As String = ""

            Select Case sender.Name
                Case "FTDevelopName"
                    pFiledName = "FTDevelopName"

                    'pCode = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTDevelopName").ToString
                    'mlId = (obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTDevelopName").ToString)
                    'pName = (obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTDevelopName2").ToString)

                    With CType(FTDevelopName.Properties.DataSource, DataTable)

                        For Each Rx As DataRow In .Select("FTDevelopName='" & HI.UL.ULF.rpQuoted(pText) & "'")
                            pCode = Rx!FTDevelopName.ToString
                            mlId = (Rx!FTDevelopName.ToString)
                            pName = Rx!FTDevelopName2.ToString
                        Next

                    End With

                Case "FNHSysCustId"
                    pFiledName = "FNHSysCustId"

                    'pCode = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTCustCode").ToString
                    'mlId = (obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysCustId").ToString)
                    'pName = (obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTCustName").ToString)

                    With CType(FNHSysCustId.Properties.DataSource, DataTable)

                        For Each Rx As DataRow In .Select("FTCustCode='" & HI.UL.ULF.rpQuoted(pText) & "'")
                            pCode = Rx!FTCustCode.ToString
                            mlId = (Rx!FNHSysCustId.ToString)
                            pName = Rx!FTCustName.ToString
                        Next

                    End With

                Case Else
                    Exit Sub
            End Select

            If (OldValue) <> pCode Then

                pBOMListUpdate.Clear()

                Dim pBOM As New BOMData
                pBOM.FieldName = pFiledName
                pBOM.FieldValue = mlId

                Select Case sender.Name
                    Case "FTDevelopName"
                        pBOM.FieldDataType = BOMDataType.DataString
                    Case "FNHSysCustId"
                        pBOM.FieldDataType = BOMDataType.DataInteger

                    Case Else
                        Exit Sub
                End Select


                pBOMListUpdate.Add(pBOM)

                If UpdateBOMHeader(BOMSysID, pBOMListUpdate) = False Then

                    sender.te = OldValue
                Else

                    Select Case sender.Name
                        Case "FTDevelopName"
                            FTDevelopName_None.Text = pName
                        Case "FNHSysCustId"
                            FNHSysCustId_None.Text = pName


                            Dim cmdstring As String = ""

                            cmdstring = "update ST set FNHSysCustId =" & mlId & " "
                            cmdstring &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTStyle  AS ST  "
                            cmdstring &= vbCrLf & " WHERE ST.FNHSysStyleId=" & StyleID & " "

                            HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                        Case Else
                            Exit Sub
                    End Select
                End If

                pBOMListUpdate.Clear()

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ogdorder_EmbeddedNavigator_ButtonClick(sender As Object, e As DevExpress.XtraEditors.NavigatorButtonClickEventArgs) Handles ogdorder.EmbeddedNavigator.ButtonClick
        Select Case e.Button.ButtonType
            'Case DevExpress.XtraEditors.NavigatorButtonType.Remove

            '    Dim cmdstring As String = ""
            '    With Me.ogvorder

            '        For Each i As Integer In .GetSelectedRows()
            '            Dim pOrderNo As String = "" & .GetRowCellValue(i, "FTOrderNo").ToString

            '            If pOrderNo <> "" Then

            '            End If

            '        Next


            '    End With


            '    If cmdstring <> "" Then
            '        If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then

            '            Call LoadSPOrder(BOMSysID)


            '        End If

            '    End If


            Case DevExpress.XtraEditors.NavigatorButtonType.Append

                If FNStateBomOrder.SelectedIndex = 1 And StateEdit Then

                    Dim cmdstring As String = ""
                    cmdstring = "Select ISNULL(B.FTState,'0') AS FTSelect ,A.FTOrderNo, C.FTCmpCode,C1.FTBuyCode"
                    cmdstring &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.V_OrderProdAndSMPAll AS A With(NOLOCK) "
                    cmdstring &= vbCrLf & "    INNER Join  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCmp AS C WITH(NOLOCK) ON A.FNHSysCmpId = C.FNHSysCmpId  "
                    cmdstring &= vbCrLf & "  Outer Apply( select top 1 C1.FTBuyCode  from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMBuy AS C1 WITH(NOLOCK) WHERE C1.FNHSysBuyId = A.FNHSysBuyId ) C1  "

                    cmdstring &= vbCrLf & " Outer Apply  (select top 1 '1' AS FTState FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SpecialOrder AS B WITH(NOLOCK) WHERE  B.FNHSysBomId =" & BOMSysID & "  And B.FTOrderNo= A.FTOrderNo )  As B "

                    cmdstring &= vbCrLf & " WHERE  A.FNHSysStyleId =" & StyleID & " AND A.FNHSysSeasonId =" & SeasonID & "  "
                    cmdstring &= vbCrLf & " ORDER BY A.FTOrderNo "

                    Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)


                    With wListOrder

                        .StateChange = False
                        .BOMSysID = BOMSysID
                        .StyleID = StyleID
                        .SeasonID = SeasonID
                        .ogvorder.ClearColumnsFilter()
                        .ogvorder.ClearSorting()
                        .ogdorder.DataSource = dt.Copy
                        .ShowDialog()

                        If .StateChange Then

                            LoadSPOrder(BOMSysID)

                        End If

                    End With

                    dt.Dispose()

                End If

            Case Else

        End Select

        e.Handled = True
    End Sub


    Private Sub otb_SelectedPageChanging(sender As Object, e As TabPageChangingEventArgs) Handles otb.SelectedPageChanging
        If e.PrevPage Is otpmaterial Then

            If StateEdit Then

                If StateReLoad Then

                    Call LoadBOMDetaiil(BOMSysID)

                End If

            End If

        End If
    End Sub

    Private Sub RepositoryItemGridLookUpEditRMColor_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemGridLookUpEditRMColor.EditValueChanged
        Try
            Dim MatCode As String = ""
            With Me.ogvmatcolor
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)

                Dim pCode As String = ""
                Dim pNameTH As String = ""
                Dim pNameEN As String = ""
                Dim mlId As Integer = 0
                Dim pFiledName As String = ""
                Dim pColorWay As String = .FocusedColumn.Caption.Trim()
                Dim pText As String = obj.Text

                pFiledName = "FNHSysRawMatColorId"
                'pCode = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorCode").ToString
                'mlId = Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysRawMatColorId").ToString)
                'pNameTH = (obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorNameTH").ToString)
                'pNameEN = (obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatColorNameEN").ToString)



                With CType(RepositoryItemGridLookUpEditRMColor.DataSource, DataTable)

                    For Each Rx As DataRow In .Select("FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(pText) & "'")
                        pCode = Rx!FTRawMatColorCode.ToString
                        mlId = Val((Rx!FNHSysRawMatColorId.ToString))
                        pNameTH = Rx!FTRawMatColorNameTH.ToString
                        pNameEN = Rx!FTRawMatColorNameEN.ToString


                    Next

                End With



                Dim pSeq As Integer = Val(.GetFocusedRowCellValue("FNSeq").ToString)

                Dim pMainMatId As Integer = Val(.GetFocusedRowCellValue("FNHSysMainMatId").ToString)

                If (OldValue) <> mlId.ToString And pSeq > 0 And (pText = "" Or (pText <> "" And pCode <> "")) Then


                    Dim cmdstring As String = "s"
                    cmdstring = "select top 1 BOM.FTRawMatColorNameTH,BOM.FTRawMatColorNameEN "
                    cmdstring &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Colorway  AS BOM WITH(NOLOCK) "
                    cmdstring &= vbCrLf & " INNER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat  AS BOM1 WITH(NOLOCK)  ON BOM.FNHSysBomId= BOM1.FNHSysBomId AND BOM.FNSeq =BOM1.FNSeq "
                    cmdstring &= vbCrLf & " WHERE BOM.FNHSysBomId=" & BOMSysID & " "
                    cmdstring &= vbCrLf & " AND  BOM1.FNHSysMainMatId=" & pMainMatId & "  "
                    cmdstring &= vbCrLf & " AND  BOM.FNHSysRawMatColorId=" & mlId & "  "
                    cmdstring &= vbCrLf & " ORDER BY ISNULL(BOM.FDUpdDate + ' ' + BOM.FTUpdTime,BOM.FDInsDate + ' ' + BOM.FTInsTime) DESC  "
                    Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                    For Each R As DataRow In dt.Rows
                        pNameTH = R!FTRawMatColorNameTH.ToString
                        pNameEN = R!FTRawMatColorNameEN.ToString
                    Next

                    dt.Dispose()

                    pBOMListUpdate.Clear()

                    Dim pBOM As New BOMData
                    pBOM.FieldName = pFiledName
                    pBOM.FieldValue = mlId
                    pBOM.FieldValueTH = pNameTH
                    pBOM.FieldValueEN = pNameEN


                    pBOM.FieldDataType = BOMDataType.DataInteger
                    pBOMListUpdate.Add(pBOM)

                    If UpdateBOMColor(BOMSysID, pSeq, pColorWay, pBOMListUpdate) = False Then

                        .SetFocusedRowCellValue(.FocusedColumn.FieldName, OldValue)

                    Else
                        .SetFocusedRowCellValue(.FocusedColumn.FieldName, Val(mlId))
                        .SetFocusedRowCellValue(.FocusedColumn.FieldName & "TH", pNameTH)
                        .SetFocusedRowCellValue(.FocusedColumn.FieldName & "EN", pNameEN)


                        OldValue = mlId.ToString

                    End If

                    pBOMListUpdate.Clear()

                Else
                    .SetFocusedRowCellValue(.FocusedColumn.FieldName, OldValue)
                End If

            End With

        Catch ex As Exception
            Me.ogvmatcolor.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, Val(OldValue))
        End Try
    End Sub


    Private Sub RepCheckEdit_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepCheckEdit.EditValueChanging
        Try

            With Me.ogvmatcolor
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim pValue As String = "0"
                If e.NewValue.ToString = "1" Then
                    pValue = "1"
                End If


                Dim pSeq As Integer = Val(Me.ogvmatcolor.GetFocusedRowCellValue("FNSeq").ToString)

                If (OldValue) <> pValue And pSeq > 0 Then

                    pBOMListUpdate.Clear()

                    Dim pBOM As New BOMData
                    pBOM.FieldName = .FocusedColumn.FieldName
                    pBOM.FieldValue = pValue


                    pBOM.FieldDataType = BOMDataType.DataString
                    pBOMListUpdate.Add(pBOM)

                    If UpdateBOMItem(BOMSysID, pSeq, pBOMListUpdate) = False Then

                        .SetFocusedRowCellValue(.FocusedColumn.FieldName, OldValue)

                    Else
                        .SetFocusedRowCellValue(.FocusedColumn.FieldName, pValue)
                        OldValue = pValue
                    End If

                    pBOMListUpdate.Clear()

                End If

            End With

        Catch ex As Exception
            e.Cancel = True
            Me.ogvmatcolor.SetFocusedRowCellValue(Me.ogvmat.FocusedColumn.FieldName, OldValue)
        End Try

    End Sub


    Private Sub RepositoryItemTextEditColorNote_Leave(sender As Object, e As EventArgs) Handles RepositoryItemTextEditColorNote.Leave



        Try
            With CType(sender, DevExpress.XtraEditors.TextEdit)
                Dim pValue As String = .Text
                Dim pSeq As Integer = Val(Me.ogvmatcolornote.GetFocusedRowCellValue("FNSeq").ToString)

                Dim pColorWay As String = Me.ogvmatcolornote.FocusedColumn.Caption.Trim()

                If (OldValue) <> pValue And pSeq > 0 Then

                    pBOMListUpdate.Clear()

                    Dim pBOM As New BOMData
                    pBOM.FieldName = "FTColorNote"
                    pBOM.FieldValue = pValue
                    pBOM.FieldDataType = BOMDataType.DataString
                    pBOMListUpdate.Add(pBOM)

                    If UpdateBOMColor(BOMSysID, pSeq, pColorWay, pBOMListUpdate) = False Then

                        Me.ogvmatcolornote.SetFocusedRowCellValue(Me.ogvmatcolornote.FocusedColumn.FieldName, (OldValue))

                    End If

                    pBOMListUpdate.Clear()

                End If

            End With

        Catch ex As Exception

            Me.ogvmatcolornote.SetFocusedRowCellValue(Me.ogvmatcolornote.FocusedColumn.FieldName, (OldValue))

        End Try


    End Sub

    Private Sub RepositoryItemGridLookUpEditRMSize_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemGridLookUpEditRMSize.EditValueChanged

        Try
            Dim MatCode As String = ""
            With Me.ogvmatsize
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)

                Dim pCode As String = ""
                Dim mlId As Integer = 0
                Dim pFiledName As String = ""
                Dim pSizeBD As String = .FocusedColumn.Caption.Trim()
                Dim pText As String = obj.Text

                pFiledName = "FNHSysRawMatSizeId"
                'pCode = obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTRawMatSizeCode").ToString
                'mlId = Val(obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysRawMatSizeId").ToString)


                With CType(RepositoryItemGridLookUpEditRMSize.DataSource, DataTable)

                    For Each Rx As DataRow In .Select("FTRawMatSizeCode='" & HI.UL.ULF.rpQuoted(pText) & "'")
                        pCode = Rx!FTRawMatSizeCode.ToString
                        mlId = Val((Rx!FNHSysRawMatSizeId.ToString))

                    Next

                End With

                Dim pSeq As Integer = Val(.GetFocusedRowCellValue("FNSeq").ToString)

                If (OldValue) <> mlId.ToString And pSeq > 0 And (pText = "" Or (pText <> "" And pCode <> "")) Then

                    pBOMListUpdate.Clear()

                    Dim pBOM As New BOMData
                    pBOM.FieldName = pFiledName
                    pBOM.FieldValue = mlId
                    pBOM.FieldDataType = BOMDataType.DataInteger
                    pBOMListUpdate.Add(pBOM)

                    If UpdateBOMSize(BOMSysID, pSeq, pSizeBD, pBOMListUpdate) = False Then

                        .SetFocusedRowCellValue(.FocusedColumn.FieldName, OldValue)

                    Else
                        .SetFocusedRowCellValue(.FocusedColumn.FieldName, Val(mlId))

                        OldValue = mlId.ToString
                    End If

                    pBOMListUpdate.Clear()

                Else
                    .SetFocusedRowCellValue(.FocusedColumn.FieldName, OldValue)
                End If

            End With

        Catch ex As Exception
            Me.ogvmatsize.SetFocusedRowCellValue(Me.ogvmatsize.FocusedColumn.FieldName, Val(OldValue))
        End Try

    End Sub

    Private Sub RepositoryItemTextEditFTSilkName_Leave(sender As Object, e As EventArgs) Handles RepositoryItemTextEditFTSilkName.Leave

        Try
            With CType(sender, DevExpress.XtraEditors.TextEdit)
                Dim pValue As String = .Text
                Dim pSeq As Integer = Val(Me.ogvmatsilkcolor.GetFocusedRowCellValue("FNSeq").ToString)

                If (OldValue) <> pValue And pSeq > 0 Then

                    pBOMListUpdate.Clear()

                    Dim pBOM As New BOMData
                    pBOM.FieldName = Me.ogvmatsilkcolor.FocusedColumn.FieldName
                    pBOM.FieldValue = pValue
                    pBOM.FieldDataType = BOMDataType.DataString
                    pBOMListUpdate.Add(pBOM)

                    If UpdateBOMItem(BOMSysID, pSeq, pBOMListUpdate) = False Then

                        Me.ogvmatsilkcolor.SetFocusedRowCellValue(Me.ogvmatsilkcolor.FocusedColumn.FieldName, (OldValue))

                    End If

                    pBOMListUpdate.Clear()

                End If

            End With

        Catch ex As Exception

            Me.ogvmatsilkcolor.SetFocusedRowCellValue(Me.ogvmatsilkcolor.FocusedColumn.FieldName, (OldValue))

        End Try


    End Sub

    Delegate Sub MyGridLookUpEditDelegate(ByVal gridLookUpEdit As DevExpress.XtraEditors.GridLookUpEdit)
    Private Sub RepositoryItemGridLookUpEditRMSize_QueryPopUp(sender As Object, e As CancelEventArgs) Handles RepositoryItemGridLookUpEditFTCurCode.QueryPopUp, RepositoryItemGridLookUpEditFTSuplCode.QueryPopUp,
                                                        RepositoryItemGridLookUpEditFTUnitCode.QueryPopUp, RepositoryItemGridLookUpEditRMColor.QueryPopUp, RepositoryItemGridLookUpEditRMSize.QueryPopUp, FTDevelopName.QueryPopUp, FNHSysCustId.QueryPopUp

        Dim gridLookUpEdit As DevExpress.XtraEditors.GridLookUpEdit = TryCast(sender, DevExpress.XtraEditors.GridLookUpEdit)

        Dim myArray(0) As Object

        myArray(0) = gridLookUpEdit
        'If InvokeRequired Then  
        'Error Here  
        gridLookUpEdit.BeginInvoke(New MyGridLookUpEditDelegate(Sub() ClearFilter(gridLookUpEdit)), myArray)
        'End If  

    End Sub


    Private Sub RepositoryItemGridLookUpEditMainMatCode_QueryPopUp(sender As Object, e As CancelEventArgs) Handles RepositoryItemGridLookUpEditMainMatCode.QueryPopUp

        'Dim gridLookUpEdit As DevExpress.XtraEditors.GridLookUpEdit = TryCast(sender, DevExpress.XtraEditors.GridLookUpEdit)

        'Dim myArray(0) As Object

        'myArray(0) = gridLookUpEdit
        ''If InvokeRequired Then  
        ''Error Here  
        'gridLookUpEdit.BeginInvoke(New MyGridLookUpEditDelegate(Sub() ClearFilter(gridLookUpEdit)), myArray)
        ''End If  

    End Sub

    Public Sub ClearFilter(ByVal gridLookUpEdit As DevExpress.XtraEditors.GridLookUpEdit)
        gridLookUpEdit.Properties.View.ActiveFilterString = String.Empty
    End Sub



    Private Sub ogcmatsize_Click(sender As Object, e As EventArgs) Handles ogcmatsize.Click

    End Sub

    Private Sub UIButtonPanel_Click(sender As Object, e As EventArgs) Handles UIButtonPanel.Click

    End Sub


    Private Sub otb_SelectedPageChanged(sender As Object, e As TabPageChangedEventArgs) Handles otb.SelectedPageChanged






        For Each bt As Object In UIButtonPanel.Buttons
            If StateEdit Then

                Select Case HI.ENM.Control.GeTypeControl(bt)
                    Case ENM.Control.ControlType.WindowsUIButton
                        With CType(bt, DevExpress.XtraBars.Docking2010.WindowsUIButton)

                            If .Tag.ToString = "exit" Then
                                .Visible = True

                            Else

                                Select Case .Tag.ToString
                                    Case "add", "remove", "diff", "calculate", "insert", "interchange"
                                        .Visible = (e.Page Is otpmaterial)
                                    Case "newcolor", "deletecolor", "changecolordesc", "changecolor"
                                        .Visible = (e.Page Is otpMaterialColor)
                                    Case "newsize", "deletesize"
                                        .Visible = (e.Page Is otpMaterialSize)
                                    Case "import"
                                        .Visible = (e.Page Is otpMaterialColor Or e.Page Is otpMaterialSize)
                                End Select

                            End If


                        End With

                    Case ENM.Control.ControlType.WindowsUISeparator
                        With CType(bt, DevExpress.XtraBars.Docking2010.WindowsUISeparator)

                            .Visible = (e.Page Is otpMaterialColor Or e.Page Is otpMaterialSize Or e.Page Is otpmaterial)

                        End With
                End Select

            Else


                Select Case HI.ENM.Control.GeTypeControl(bt)
                    Case ENM.Control.ControlType.WindowsUIButton
                        With CType(bt, DevExpress.XtraBars.Docking2010.WindowsUIButton)

                            If .Tag.ToString = "exit" Then
                                .Visible = True
                            Else

                                .Visible = False
                            End If


                        End With

                    Case ENM.Control.ControlType.WindowsUISeparator
                        With CType(bt, DevExpress.XtraBars.Docking2010.WindowsUISeparator)
                            .Visible = False
                        End With
                End Select

            End If
        Next


        '    Select Case e.Page
        '        Case otpmaterial
        '        Case 
        'End Select


    End Sub

    Private Sub ogvmat_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvmat.CellMerge

        Try

            With ogvmat
                Dim ItemCode1 As String = .GetRowCellValue(e.RowHandle1, "FTMainMatCode").ToString
                Dim ItemCode2 As String = .GetRowCellValue(e.RowHandle2, "FTMainMatCode").ToString

                If (ItemCode1 = ItemCode2) Then
                    Dim value1 = .GetRowCellValue(e.RowHandle1, e.Column)
                    Dim value2 = .GetRowCellValue(e.RowHandle2, e.Column)

                    e.Merge = (value1 = value2)
                    e.Handled = True
                    Return
                Else
                    e.Merge = False
                    e.Handled = True
                    Return
                End If
            End With



        Catch ex As Exception
            Return
        End Try
    End Sub

    Private Sub ogvmat_MouseDown(sender As Object, e As MouseEventArgs) Handles ogvmat.MouseDown

        Try
            Dim tmpview As DevExpress.XtraGrid.Views.Grid.GridView = sender
            Dim hInfo As GridHitInfo = tmpview.CalcHitInfo(e.X, e.Y)

            If (hInfo.InRowCell) Then

                If Not (m_mergedCellsEdited Is Nothing) Then

                    Dim StateProc As Boolean = False

                    Select Case StateEditMergeCell
                        Case EditMergeCellData.ItemSeq
                            If (ogcmat.Contains(m_mergedCellEditorMatSeq)) Then
                                ogcmat.Controls.Remove(m_mergedCellEditorMatSeq)


                                Dim StateUpdate As Boolean = False
                                Dim pNValue As String = (Format(m_mergedCellEditorMatSeq.Value, "0.00"))
                                If Val(OldValue) <> Val(pNValue) Then

                                    For Each cellInfo As GridCellInfo In m_mergedCellsEdited


                                        Dim pMatID As Integer


                                        With ogvmat
                                            pMatID = Val(.GetRowCellValue(cellInfo.RowHandle, "FNHSysMainMatId").ToString)
                                        End With

                                        With CType(ogcmat.DataSource, DataTable)
                                            If .Select("FNHSysMainMatId<>" & pMatID & " AND FNMatSeq=" & pNValue & "").Length <= 0 Then

                                                pBOMListUpdate.Clear()

                                                Dim pBOM As New BOMData
                                                pBOM.FieldName = "FNMatSeq"
                                                pBOM.FieldValue = pNValue
                                                pBOM.FieldDataType = BOMDataType.DataInteger
                                                pBOMListUpdate.Add(pBOM)

                                                If UpdateBOMItemMerge(BOMSysID, pMatID, pBOMListUpdate) Then
                                                    StateUpdate = True
                                                End If

                                                pBOMListUpdate.Clear()

                                            Else
                                                StateUpdate = False
                                            End If


                                        End With


                                        Exit For


                                    Next

                                    If StateUpdate Then
                                        For Each cellInfo As GridCellInfo In m_mergedCellsEdited
                                            ogvmat.SetRowCellValue(cellInfo.RowHandle, "FNMatSeq", Decimal.Parse(pNValue))
                                        Next
                                    End If

                                End If


                            End If
                        Case EditMergeCellData.SuplCode
                            If (ogcmat.Contains(m_mergedCellEditorSupl)) Then
                                ogcmat.Controls.Remove(m_mergedCellEditorSupl)

                                Dim StateUpdate As Boolean = False
                                Dim pNValue As String = (m_mergedCellEditorSupl.EditValue.ToString)
                                Dim CurId As Integer = 0
                                Dim SuplId As Integer = 0
                                Dim CurCode As String = ""

                                If (OldValue) <> (pNValue) Then

                                    For Each cellInfo As GridCellInfo In m_mergedCellsEdited


                                        Dim pMatID As Integer

                                        If (OldValue) <> (pNValue) Then
                                            With ogvmat
                                                pMatID = Val(.GetRowCellValue(cellInfo.RowHandle, "FNHSysMainMatId").ToString)
                                            End With


                                            Dim dtmat As DataTable

                                            Dim _Qry As String = ""

                                            _Qry = "SELECT TOP 1  A.FNHSysSuplId,A.FNHSysCurId ,CC.FTCurCode"
                                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS A WITH(NOLOCK) "
                                            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS CC WITH(NOLOCK)  ON A.FNHSysCurId = CC.FNHSysCurId "
                                            _Qry &= vbCrLf & " WHERE A.FTSuplCode ='" & HI.UL.ULF.rpQuoted(pNValue) & "'"
                                            dtmat = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                                            For Each R As DataRow In dtmat.Rows
                                                SuplId = Val(R!FNHSysSuplId.ToString)
                                                CurId = Val(R!FNHSysCurId.ToString)
                                                CurCode = R!FTCurCode.ToString
                                            Next

                                            dtmat.Dispose()


                                            pBOMListUpdate.Clear()

                                            Dim pBOM As New BOMData
                                            pBOM.FieldName = "FNHSysSuplId"
                                            pBOM.FieldValue = SuplId
                                            pBOM.FieldDataType = BOMDataType.DataInteger
                                            pBOMListUpdate.Add(pBOM)


                                            Dim pBOM2 As New BOMData
                                            pBOM2.FieldName = "FNHSysCurId"
                                            pBOM2.FieldValue = CurId
                                            pBOM2.FieldDataType = BOMDataType.DataInteger
                                            pBOMListUpdate.Add(pBOM2)

                                            If UpdateBOMItemMerge(BOMSysID, pMatID, pBOMListUpdate) Then
                                                StateUpdate = True
                                            End If

                                            pBOMListUpdate.Clear()


                                            Exit For
                                        End If

                                    Next

                                    If StateUpdate Then

                                        With ogvmat

                                            For Each cellInfo As GridCellInfo In m_mergedCellsEdited
                                                .SetRowCellValue(cellInfo.RowHandle, "FTSuplCode", (pNValue))
                                                .SetRowCellValue(cellInfo.RowHandle, "FNHSysSuplId", (SuplId))
                                                .SetRowCellValue(cellInfo.RowHandle, "FTCurCode", CurCode)
                                                .SetRowCellValue(cellInfo.RowHandle, "FNHSysCurId", CurId)
                                            Next

                                        End With

                                    End If

                                End If


                            End If
                        Case EditMergeCellData.ItemCode
                            If (ogcmat.Contains(m_mergedCellEditorMainMat)) Then
                                ogcmat.Controls.Remove(m_mergedCellEditorMainMat)




                                Dim StateUpdate As Boolean = False
                                Dim pNValue As String = (m_mergedCellEditorMainMat.EditValue.ToString)
                                Dim CurId As Integer = 0
                                Dim CurCode As String = ""
                                Dim _FNHSysMerMatId_None As String = ""
                                Dim _FTStateMainMaterial As String = ""
                                Dim _FTFabricFrontSize As String = ""

                                Dim MMatId As Integer = 0
                                Dim SuplId As Integer = 0
                                Dim SuplCode As String = ""

                                If (OldValue) <> (pNValue) Then

                                    For Each cellInfo As GridCellInfo In m_mergedCellsEdited


                                        Dim pMatID As Integer
                                        Dim pMatSeq As Integer = 0

                                        If (OldValue) <> (pNValue) Then

                                            With ogvmat
                                                Dim pID As String = .GetRowCellValue(cellInfo.RowHandle, "FNHSysMainMatId").ToString
                                                pMatID = Val(.GetRowCellValue(cellInfo.RowHandle, "FNHSysMainMatId").ToString)

                                                pMatSeq = Val(.GetRowCellValue(cellInfo.RowHandle, "FNMatSeq").ToString)
                                            End With

                                            Dim dtmat As DataTable
                                            Dim _Qry As String = ""

                                            _Qry = "SELECT TOP 1 A.FNHSysMainMatId "

                                            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                                                _Qry &= vbCrLf & ", A.FTMainMatNameEN AS FTMainMatName "
                                            Else
                                                _Qry &= vbCrLf & " ,A.FTMainMatNameTH AS FTMainMatName "
                                            End If
                                            _Qry &= vbCrLf & "  ,A.FTStateMainMaterial ,FTFabricFrontSize"

                                            _Qry &= vbCrLf & "  ,A.FNHSysSuplId, A.FNHSysCurId,S.FTSuplCode ,CC.FTCurCode,A.FNHSysMainMatId"

                                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS A WITH(NOLOCK) "
                                            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK) ON A.FNHSysSuplId= A.FNHSysSuplId "
                                            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS CC WITH(NOLOCK)  ON A.FNHSysCurId = CC.FNHSysCurId "
                                            _Qry &= vbCrLf & " WHERE A.FTMainMatCode='" & HI.UL.ULF.rpQuoted(pNValue) & "'"

                                            dtmat = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                                            For Each R As DataRow In dtmat.Rows
                                                _FNHSysMerMatId_None = R!FTMainMatName.ToString

                                                If Val(R!FTStateMainMaterial.ToString) = 1 Then
                                                    _FTStateMainMaterial = "1"
                                                Else
                                                    _FTStateMainMaterial = "0"
                                                End If

                                                _FTFabricFrontSize = R!FTFabricFrontSize.ToString

                                                CurId = Val(R!FNHSysCurId.ToString)
                                                CurCode = R!FTCurCode.ToString

                                                SuplId = Val(R!FNHSysSuplId.ToString)
                                                SuplCode = R!FTSuplCode.ToString
                                                MMatId = Val(R!FNHSysMainMatId.ToString)

                                            Next

                                            dtmat.Dispose()







                                            With CType(ogcmat.DataSource, DataTable)
                                                If .Select("FNHSysMainMatId=" & MMatId & " AND FNMatSeq <> " & pMatSeq & "").Length <= 0 Then







                                                    pBOMListUpdate.Clear()

                                                    Dim pBOM As New BOMData
                                                    pBOM.FieldName = "FNHSysMainMatId"
                                                    pBOM.FieldValue = MMatId
                                                    pBOM.FieldDataType = BOMDataType.DataInteger
                                                    pBOMListUpdate.Add(pBOM)


                                                    Dim pBOM2 As New BOMData
                                                    pBOM2.FieldName = "FNHSysSuplId"
                                                    pBOM2.FieldValue = SuplId
                                                    pBOM2.FieldDataType = BOMDataType.DataInteger
                                                    pBOMListUpdate.Add(pBOM2)

                                                    Dim pBOM3 As New BOMData
                                                    pBOM3.FieldName = "FNHSysCurId"
                                                    pBOM3.FieldValue = CurId
                                                    pBOM3.FieldDataType = BOMDataType.DataInteger
                                                    pBOMListUpdate.Add(pBOM3)

                                                    Dim pBOM4 As New BOMData
                                                    pBOM4.FieldName = "FTFabricFrontSize"
                                                    pBOM4.FieldValue = _FTFabricFrontSize
                                                    pBOM4.FieldDataType = BOMDataType.DataString
                                                    pBOMListUpdate.Add(pBOM4)

                                                    Dim pBOM5 As New BOMData
                                                    pBOM5.FieldName = "FTStateMainMaterial"
                                                    pBOM5.FieldValue = _FTStateMainMaterial
                                                    pBOM5.FieldDataType = BOMDataType.DataString
                                                    pBOMListUpdate.Add(pBOM5)



                                                    If UpdateBOMItemMerge(BOMSysID, pMatID, pBOMListUpdate) Then
                                                        StateUpdate = True
                                                    End If

                                                    pBOMListUpdate.Clear()

                                                Else
                                                    StateUpdate = False
                                                End If


                                            End With


                                            Exit For
                                        End If

                                    Next

                                    If StateUpdate Then
                                        With ogvmat
                                            For Each cellInfo As GridCellInfo In m_mergedCellsEdited


                                                .SetRowCellValue(cellInfo.RowHandle, "FTMainMatCode", pNValue)
                                                .SetRowCellValue(cellInfo.RowHandle, "FNHSysMainMatId", MMatId)




                                                .SetRowCellValue(cellInfo.RowHandle, "FTMainMatName", _FNHSysMerMatId_None)
                                                .SetRowCellValue(cellInfo.RowHandle, "FTStateMainMaterial", _FTStateMainMaterial)
                                                .SetRowCellValue(cellInfo.RowHandle, "FTFabricFrontSize", _FTFabricFrontSize)


                                                If CurId > 0 And CurCode <> "" Then
                                                    .SetRowCellValue(cellInfo.RowHandle, "FNHSysCurId", Integer.Parse(CurId))
                                                    .SetRowCellValue(cellInfo.RowHandle, "FTCurCode", CurCode)
                                                End If

                                                If SuplId > 0 And SuplCode <> "" Then
                                                    .SetRowCellValue(cellInfo.RowHandle, "FNHSysSuplId", Integer.Parse(SuplId))
                                                    .SetRowCellValue(cellInfo.RowHandle, "FTSuplCode", SuplCode)
                                                End If

                                            Next
                                        End With


                                    End If

                                End If



                            End If
                        Case EditMergeCellData.UNitCode
                            If (ogcmat.Contains(m_mergedCellEditorUnit)) Then
                                ogcmat.Controls.Remove(m_mergedCellEditorUnit)


                                Dim StateUpdate As Boolean = False
                                Dim pNValue As String = (m_mergedCellEditorUnit.EditValue.ToString)
                                Dim UnitId As Integer = 0

                                If (OldValue) <> (pNValue) Then


                                    Dim dtmat As DataTable

                                    Dim _Qry As String = ""

                                    _Qry = "SELECT TOP 1  CC.FNHSysUnitId ,CC.FTUnitCode"
                                    _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS CC WITH(NOLOCK)  "
                                    _Qry &= vbCrLf & " WHERE CC.FTUnitCode ='" & HI.UL.ULF.rpQuoted(pNValue) & "'"
                                    dtmat = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                                    For Each R As DataRow In dtmat.Rows

                                        UnitId = Val(R!FNHSysUnitId.ToString)

                                    Next

                                    dtmat.Dispose()

                                    For Each cellInfo As GridCellInfo In m_mergedCellsEdited

                                        Dim pMatID As Integer

                                        With ogvmat
                                            pMatID = Val(.GetRowCellValue(cellInfo.RowHandle, "FNHSysMainMatId").ToString)
                                        End With

                                        pBOMListUpdate.Clear()

                                        Dim pBOM As New BOMData
                                        pBOM.FieldName = "FNHSysUnitId"
                                        pBOM.FieldValue = pNValue
                                        pBOM.FieldDataType = BOMDataType.DataInteger
                                        pBOMListUpdate.Add(pBOM)

                                        If UpdateBOMItemMerge(BOMSysID, pMatID, pBOMListUpdate) Then
                                            StateUpdate = True
                                        End If

                                        pBOMListUpdate.Clear()


                                        Exit For

                                    Next

                                    If StateUpdate Then
                                        For Each cellInfo As GridCellInfo In m_mergedCellsEdited
                                            ogvmat.SetRowCellValue(cellInfo.RowHandle, "FNHSysUnitId", UnitId)
                                            ogvmat.SetRowCellValue(cellInfo.RowHandle, "FTUnitCode", pNValue)
                                        Next
                                    End If

                                End If



                            End If

                        Case EditMergeCellData.Currency
                            If (ogcmat.Contains(m_mergedCellEditorCurr)) Then
                                ogcmat.Controls.Remove(m_mergedCellEditorCurr)



                                Dim StateUpdate As Boolean = False
                                Dim pNValue As String = (m_mergedCellEditorCurr.EditValue.ToString)
                                Dim CurId As Integer = 0
                                If (OldValue) <> (pNValue) Then


                                    Dim dtmat As DataTable

                                    Dim _Qry As String = ""

                                    _Qry = "SELECT TOP 1  CC.FNHSysCurId ,CC.FTCurCode"
                                    _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS CC WITH(NOLOCK)  "
                                    _Qry &= vbCrLf & " WHERE CC.FTCurCode ='" & HI.UL.ULF.rpQuoted(pNValue) & "'"
                                    dtmat = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                                    For Each R As DataRow In dtmat.Rows

                                        CurId = Val(R!FNHSysCurId.ToString)

                                    Next

                                    dtmat.Dispose()


                                    For Each cellInfo As GridCellInfo In m_mergedCellsEdited


                                        Dim pMatID As Integer


                                        With ogvmat
                                            pMatID = Val(.GetRowCellValue(cellInfo.RowHandle, "FNHSysMainMatId").ToString)
                                        End With

                                        pBOMListUpdate.Clear()

                                        Dim pBOM As New BOMData
                                        pBOM.FieldName = "FNHSysCurId"
                                        pBOM.FieldValue = CurId
                                        pBOM.FieldDataType = BOMDataType.DataInteger
                                        pBOMListUpdate.Add(pBOM)

                                        If UpdateBOMItemMerge(BOMSysID, pMatID, pBOMListUpdate) Then
                                            StateUpdate = True
                                        End If

                                        pBOMListUpdate.Clear()


                                        Exit For


                                    Next

                                    If StateUpdate Then
                                        For Each cellInfo As GridCellInfo In m_mergedCellsEdited
                                            ogvmat.SetRowCellValue(cellInfo.RowHandle, "FNHSysCurId", CurId)
                                            ogvmat.SetRowCellValue(cellInfo.RowHandle, "FTCurCode", pNValue)
                                        Next
                                    End If

                                End If




                            End If
                    End Select

                    m_mergedCellsEdited = Nothing
                End If

                Dim vInfo As GridViewInfo = tmpview.GetViewInfo()
                Dim cInfo As GridCellInfo = vInfo.GetGridCellInfo(hInfo)
                Select Case cInfo.Column.FieldName.ToString
                    Case "FNMatSeq"
                        If (TypeOf (cInfo) Is DevExpress.XtraGrid.Views.Grid.ViewInfo.GridMergedCellInfo) Then

                            If (m_mergedCellsEdited IsNot Nothing) Then
                                ogcmat.Controls.Remove(m_mergedCellEditorMatSeq)
                            End If

                            m_mergedCellsEdited = (CType(cInfo, GridMergedCellInfo)).MergedCells

                            If m_mergedCellsEdited.Count > 0 Then
                                ogcmat.Controls.Add(m_mergedCellEditorMatSeq)
                                m_mergedCellEditorMatSeq.Bounds = cInfo.Bounds
                                ' ''m_mergedCellEditorSupl.Text = cInfo.CellValue.ToString()
                                m_mergedCellEditorMatSeq.Value = cInfo.CellValue.ToString()

                                OldValue = cInfo.CellValue.ToString()

                                StateEditMergeCell = EditMergeCellData.ItemSeq
                            End If


                        End If
                    Case "FTSuplCode"

                        If (TypeOf (cInfo) Is DevExpress.XtraGrid.Views.Grid.ViewInfo.GridMergedCellInfo) Then

                            If (m_mergedCellsEdited IsNot Nothing) Then
                                ogcmat.Controls.Remove(m_mergedCellEditorSupl)
                            End If

                            m_mergedCellsEdited = (CType(cInfo, GridMergedCellInfo)).MergedCells

                            If m_mergedCellsEdited.Count > 0 Then
                                ogcmat.Controls.Add(m_mergedCellEditorSupl)
                                m_mergedCellEditorSupl.Bounds = cInfo.Bounds
                                ' ''m_mergedCellEditorSupl.Text = cInfo.CellValue.ToString()
                                m_mergedCellEditorSupl.EditValue = cInfo.CellValue.ToString()

                                OldValue = cInfo.CellValue.ToString()

                                StateEditMergeCell = EditMergeCellData.SuplCode
                            End If


                        End If
                    Case "FTMainMatCode"
                        If (TypeOf (cInfo) Is DevExpress.XtraGrid.Views.Grid.ViewInfo.GridMergedCellInfo) Then
                            If (m_mergedCellsEdited IsNot Nothing) Then
                                ogcmat.Controls.Remove(m_mergedCellEditorMainMat)
                            End If

                            m_mergedCellsEdited = (CType(cInfo, GridMergedCellInfo)).MergedCells

                            If m_mergedCellsEdited.Count > 0 Then
                                ogcmat.Controls.Add(m_mergedCellEditorMainMat)
                                m_mergedCellEditorMainMat.Bounds = cInfo.Bounds
                                m_mergedCellEditorMainMat.EditValue = cInfo.CellValue.ToString()
                                OldValue = cInfo.CellValue.ToString()

                                StateEditMergeCell = EditMergeCellData.ItemCode
                                '' ''End If
                            End If

                        End If
                    Case "FTUnitCode"
                        If (TypeOf (cInfo) Is DevExpress.XtraGrid.Views.Grid.ViewInfo.GridMergedCellInfo) Then
                            If (m_mergedCellsEdited IsNot Nothing) Then
                                ogcmat.Controls.Remove(m_mergedCellEditorUnit)
                            End If

                            m_mergedCellsEdited = (CType(cInfo, GridMergedCellInfo)).MergedCells

                            If m_mergedCellsEdited.Count > 0 Then
                                ogcmat.Controls.Add(m_mergedCellEditorUnit)
                                m_mergedCellEditorUnit.Bounds = cInfo.Bounds
                                m_mergedCellEditorUnit.EditValue = cInfo.CellValue.ToString()
                                OldValue = cInfo.CellValue.ToString()

                                StateEditMergeCell = EditMergeCellData.UNitCode
                            End If


                        End If

                    Case "FTCurCode"
                        If (TypeOf (cInfo) Is DevExpress.XtraGrid.Views.Grid.ViewInfo.GridMergedCellInfo) Then
                            If (m_mergedCellsEdited IsNot Nothing) Then
                                ogcmat.Controls.Remove(m_mergedCellEditorCurr)
                            End If

                            m_mergedCellsEdited = (CType(cInfo, GridMergedCellInfo)).MergedCells

                            If m_mergedCellsEdited.Count > 0 Then
                                ogcmat.Controls.Add(m_mergedCellEditorCurr)
                                m_mergedCellEditorCurr.Bounds = cInfo.Bounds
                                m_mergedCellEditorCurr.EditValue = cInfo.CellValue.ToString()

                                OldValue = cInfo.CellValue.ToString()

                                StateEditMergeCell = EditMergeCellData.Currency
                            End If


                        End If
                End Select
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvmatcolor_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvmatcolor.RowCellStyle
        Try
            With ogvmatcolor

                If Microsoft.VisualBasic.Left(e.Column.Name, Len("gViewColor")) = "gViewColor" Then

                    If "" & .GetRowCellDisplayText(e.RowHandle, e.Column).ToString() = "N/R" Then

                        e.Appearance.BackColor = System.Drawing.Color.LightCyan
                        e.Appearance.ForeColor = System.Drawing.Color.Blue

                    End If

                End If

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvmatsize_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvmatsize.RowCellStyle
        Try
            With ogvmatsize

                If Microsoft.VisualBasic.Left(e.Column.Name, Len("gViewSize")) = "gViewSize" Then

                    If "" & .GetRowCellDisplayText(e.RowHandle, e.Column).ToString() = "N/R" Then

                        e.Appearance.BackColor = System.Drawing.Color.LightCyan
                        e.Appearance.ForeColor = System.Drawing.Color.Blue

                    End If

                End If

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvmatcolor_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvmatcolor.KeyDown
        Try
            With Me.ogvmatcolor
                If .FocusedRowHandle < 0 Then Exit Sub
                Select Case e.KeyCode
                    Case Keys.F10, Keys.F11, Keys.F12

                        Dim pSeq As Integer = Val(.GetFocusedRowCellValue("FNSeq").ToString)

                        Dim mState As Integer = 1  ' Update by Code
                        Dim mColorCode As String = "N/R"

                        Select Case e.KeyCode
                            Case Keys.F11
                                mColorCode = ""

                            Case Keys.F12
                                mState = 3 ' Update by Size Breakdown
                        End Select

                        Dim Spls As New HI.TL.SplashScreen("Loading... BOM Detail Please Wait.")

                        Try


                            Dim _Qry As String = ""

                            Dim ds As New DataSet
                            _Qry = "EXEC " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_GETBOMGARMENT_UPDATECOLORSIZE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & BOMSysID & "," & pSeq & ",'" & HI.UL.ULF.rpQuoted(mColorCode) & "',''," & mState & ""

                            HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, ds)

                            If ds.Tables.Count = 2 Then
                                If Val(ds.Tables(0).Rows(0).Item("FNCount").ToString) > 0 Then

                                    With CType(ogcmatcolor.DataSource, DataTable)


                                        For Each R As DataRow In .Select("FNSeq =" & pSeq & "")

                                            For Each Col As DataColumn In .Columns

                                                Try
                                                    R.Item(Col.ColumnName) = ds.Tables(1).Rows(0).Item(Col.ColumnName)
                                                Catch ex As Exception

                                                End Try

                                            Next
                                        Next


                                        .AcceptChanges()
                                    End With



                                End If

                            End If
                        Catch ex As Exception

                        End Try

                        Spls.Close()

                End Select
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvmatsize_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvmatsize.KeyDown
        Try
            With Me.ogvmatsize
                If .FocusedRowHandle < 0 Then Exit Sub
                Select Case e.KeyCode
                    Case Keys.F10, Keys.F11, Keys.F12

                        Dim pSeq As Integer = Val(.GetFocusedRowCellValue("FNSeq").ToString)

                        Dim mState As Integer = 2  ' Update by Code
                        Dim mSizeCode As String = "N/R"

                        Select Case e.KeyCode
                            Case Keys.F11
                                mSizeCode = ""

                            Case Keys.F12
                                mState = 4 ' Update by Size Breakdown
                        End Select

                        Dim Spls As New HI.TL.SplashScreen("Loading... BOM Detail Please Wait.")

                        Try


                            Dim _Qry As String = ""

                            Dim ds As New DataSet
                            _Qry = "EXEC " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_GETBOMGARMENT_UPDATECOLORSIZE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & BOMSysID & "," & pSeq & ",'','" & HI.UL.ULF.rpQuoted(mSizeCode) & "'," & mState & ""

                            HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, ds)

                            If ds.Tables.Count = 2 Then
                                If Val(ds.Tables(0).Rows(0).Item("FNCount").ToString) > 0 Then

                                    With CType(ogcmatsize.DataSource, DataTable)


                                        For Each R As DataRow In .Select("FNSeq =" & pSeq & "")

                                            For Each Col As DataColumn In .Columns

                                                Try
                                                    R.Item(Col.ColumnName) = ds.Tables(1).Rows(0).Item(Col.ColumnName)
                                                Catch ex As Exception

                                                End Try

                                            Next

                                        Next

                                        .AcceptChanges()
                                    End With


                                End If

                            End If
                        Catch ex As Exception

                        End Try

                        Spls.Close()

                End Select
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvSilkColor_ShowingEditor(sender As Object, e As CancelEventArgs) Handles ogvSilkColor.ShowingEditor
        Try
            With Me.ogvSilkColor
                Select Case .FocusedColumn.FieldName
                    Case "FTSelect"
                        e.Cancel = False

                    Case Else

                        If .GetFocusedRowCellValue("FTSelect").ToString = "1" Then
                            e.Cancel = False
                        Else
                            e.Cancel = True
                        End If

                End Select

            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTStateActive_CheckedChanged(sender As Object, e As EventArgs) Handles FTStateActive.CheckedChanged
        Try

            If StateLoad = False Then
                Exit Sub
            End If

            Dim pValue As String = ""
            Dim pOldValue As String = ""


            If FTStateActive.Checked Then
                pValue = "1"
            Else
                pValue = "0"
            End If


            If pValue <> "" Then


                pBOMListUpdate.Clear()

                Dim pBOM As New BOMData
                pBOM.FieldName = "FTStateActive"
                pBOM.FieldValue = pValue
                pBOM.FieldDataType = BOMDataType.DataString
                pBOMListUpdate.Add(pBOM)


                If UpdateBOMHeader(BOMSysID, pBOMListUpdate) = False Then

                    FNStateBomOrder.SelectedIndex = OldValue

                End If

                pBOMListUpdate.Clear()

            Else

                FNStateBomOrder.SelectedIndex = OldValue
            End If

        Catch ex As Exception

            FNStateBomOrder.SelectedIndex = OldValue
        End Try
    End Sub


    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
        If StateEdit Then
            Select Case keyData
                Case Keys.Control + Keys.M
                    AddNewRow()
                Case Keys.Control + Keys.I
                    InsertRow()
                Case Keys.Control + Keys.Z
                    RemoveRow()
                Case Keys.Alt + Keys.A
                    DiffMatPart()
                Case Keys.Alt + Keys.I
                    ImportColowayAndSizeBreakdown()

                Case Keys.Alt + Keys.C
                    ChangeColorWay()
                Case Keys.Shift + Keys.C
                    ChangeColorName()
                Case Keys.Shift + Keys.E
                Case Keys.Shift + Keys.A
                    Select Case otb.SelectedTabPage.Name
                        Case otpMaterialColor.Name
                            AddNewColorway()
                        Case otpMaterialSize.Name
                            AddNewSizeBreakdown()
                    End Select



                Case Keys.Shift + Keys.D

                    Select Case otb.SelectedTabPage.Name
                        Case otpMaterialColor.Name
                            DeleteColorWay()
                        Case otpMaterialSize.Name
                            DeleteSizeBreakdown()
                    End Select

            End Select
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function


    Private Sub CalculateAndExportOptiplan()
        With CalculateExportDataMRP
            .WindowState = FormWindowState.Maximized
            .FNHSysStyleId.Text = Me.FNHSysStyleId.Text
            .FNHSysSeasonId.Text = Me.FNHSysSeasonId.Text
            .FNVersion.Value = Me.FNVersion.Value
            .FNBomDevType.SelectedIndex = Me.FNBomDevType.SelectedIndex

            .BOMID = BOMSysID
            .StyleID = StyleID
            .SeasonID = SeasonID

            '.FNCaltype.SelectedIndex = 0
            .FNSelectOrderType.SelectedIndex = 0
            .StateBomOrder = FNStateBomOrder.SelectedIndex
            .ochkselectall.Checked = False
            .ogcmatcode.DataSource = Nothing
            .ogcmrp.DataSource = Nothing
            .ogcmrpnot.DataSource = Nothing
            .GridCalculated.DataSource = Nothing
            .otb.TabPages.Clear()
            .LoadOrderInfo()
            .otbmrp.SelectedTabPageIndex = 0
            .StateCal = False
            .ogvmatcode.ClearColumnsFilter()
            .ogvmrp.ClearColumnsFilter()
            .ogvorderlist.ClearColumnsFilter()
            .ogvmrpnot.ClearColumnsFilter()
            .ShowDialog()


            If .StateCal = True Then

                Dim Spls As New HI.TL.SplashScreen("Loading...")

                Try

                    Dim cmdstring As String = ""
                    cmdstring = "  Select  FNSeq,FTStateCalMRP,FTStateExportOptiplan "
                    cmdstring &= vbCrLf & " , FTStateLastCalMRPUser, FDStateLastCalMRPDate, FTStateLastCalMRPTime, FTStateFirstCalMRPUser, FDStateFirstCalMRPDate, FTStateFirstCalMRPTime"
                    cmdstring &= vbCrLf & " , FTStateFirstExportOptiplanUser, FDStateFirstExportOptiplanDate, FTStateFirstExportOptiplanTime, FTStateLastExportOptiplanUser, FDStateLastExportOptiplanDate, FTStateLastExportOptiplanTime "
                    cmdstring &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_Mat  AS TR  WITH(NOLOCK)  "
                    cmdstring &= vbCrLf & " WHERE FNHSysBomId=" & BOMSysID & " "


                    Dim mdt As DataTable
                    mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)


                    With CType(ogcmat.DataSource, DataTable)
                        .AcceptChanges()
                        For Each R As DataRow In mdt.Rows

                            For Each Rx As DataRow In .Select("FNSeq=" & Val(R!FNSeq) & "")

                                Rx!FTStateCalMRP = R!FTStateCalMRP

                                Rx!FTStateLastCalMRPUser = R!FTStateLastCalMRPUser
                                Rx!FDStateLastCalMRPDate = R!FDStateLastCalMRPDate
                                Rx!FTStateLastCalMRPTime = R!FTStateLastCalMRPTime
                                Rx!FTStateFirstCalMRPUser = R!FTStateFirstCalMRPUser
                                Rx!FDStateFirstCalMRPDate = R!FDStateFirstCalMRPDate
                                Rx!FTStateFirstCalMRPTime = R!FTStateFirstCalMRPTime


                                Rx!FTStateExportOptiplan = R!FTStateExportOptiplan
                                Rx!FTStateFirstExportOptiplanUser = R!FTStateFirstExportOptiplanUser
                                Rx!FDStateFirstExportOptiplanDate = R!FDStateFirstExportOptiplanDate
                                Rx!FTStateFirstExportOptiplanTime = R!FTStateFirstExportOptiplanTime
                                Rx!FTStateLastExportOptiplanUser = R!FTStateLastExportOptiplanUser
                                Rx!FDStateLastExportOptiplanDate = R!FDStateLastExportOptiplanDate
                                Rx!FTStateLastExportOptiplanTime = R!FTStateLastExportOptiplanTime

                            Next

                        Next
                        .AcceptChanges()
                    End With




                Catch ex As Exception

                End Try


                Spls.Close()

            End If
        End With
    End Sub

    Private Sub UIButtonPanel_ButtonClick(sender As Object, e As DevExpress.XtraBars.Docking2010.ButtonEventArgs) Handles UIButtonPanel.ButtonClick
        Select Case e.Button.Properties.Tag
            Case "add"
                If StateEdit Then
                    AddNewRow()
                End If
            Case "insert"
                If StateEdit Then

                    InsertRow()
                End If
            Case "calculate"
                If StateEdit Then

                    If FTStateActive.Checked Then
                        CalculateAndExportOptiplan()
                    End If

                End If
            Case "remove"
                If StateEdit Then

                    RemoveRow()

                End If
            Case "diff"
                If StateEdit Then

                    DiffMatPart()

                End If
            Case "import"

                If StateEdit Then

                    ImportColowayAndSizeBreakdown()

                End If

            Case "newcolor"


                If StateEdit Then
                    AddNewColorway()
                End If

            Case "deletecolor"
                If StateEdit = False Then Exit Sub


                DeleteColorWay()
            Case "changecolordesc"
                If StateEdit = False Then Exit Sub
                ChangeColorName()

            Case "changecolor"


                If StateEdit Then


                    ChangeColorWay()

                End If

            Case "newsize"

                If StateEdit = False Then Exit Sub
                AddNewSizeBreakdown()


            Case "deletesize"

                If StateEdit = False Then Exit Sub

                DeleteSizeBreakdown()

            Case "reloadmaster"
                Dim spls As New HI.TL.SplashScreen("Re Loading Master....")
                LoadMaster()
                spls.Close()

            Case "interchange"
                If StateEdit = False Then Exit Sub

                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Check BOM interchange ใช่หรือไม่ ?", 1406034277) Then
                    Dim spls As New HI.TL.SplashScreen("BOM Checking interchange....")
                    InterchangeChart()
                    spls.Close()
                End If



            Case "exit"
                Me.Close()
        End Select

    End Sub

    Private Sub otb_Click(sender As Object, e As EventArgs) Handles otb.Click

    End Sub

    Private Sub ogvmat_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvmat.KeyDown
        Try


            If (ogcmat.DataSource Is Nothing) Then

                Exit Sub
            End If


            With Me.ogvmat
                If .RowCount <= 0 Then Exit Sub
                Select Case e.KeyCode
                    Case Keys.F1


                        Dim pSeq As Integer = 0

                        For I As Integer = 0 To .RowCount - 1

                            If .GetRowCellValue(I, "FTStateMatConfirm").ToString = "0" Then

                                .FocusedRowHandle = I

                                pSeq = Val(.GetRowCellValue(I, "FNSeq").ToString)

                                pBOMListUpdate.Clear()

                                Dim pBOM As New BOMData
                                pBOM.FieldName = "FTStateMatConfirm"
                                pBOM.FieldValue = "1"
                                pBOM.FieldDataType = BOMDataType.DataString
                                pBOMListUpdate.Add(pBOM)


                                If UpdateBOMItem(BOMSysID, pSeq, pBOMListUpdate) = False Then

                                    .SetRowCellValue(I, "FTStateMatConfirm", "0")
                                Else
                                    .SetRowCellValue(I, "FTStateMatConfirm", "1")

                                End If

                                pBOMListUpdate.Clear()

                            End If

                        Next

                    Case Keys.F2


                        Dim pSeq As Integer = 0

                        For I As Integer = 0 To .RowCount - 1

                            If .GetRowCellValue(I, "FTStateCalMRP").ToString = "1" Then

                                .FocusedRowHandle = I

                                pSeq = Val(.GetRowCellValue(I, "FNSeq").ToString)

                                pBOMListUpdate.Clear()

                                Dim pBOM As New BOMData
                                pBOM.FieldName = "FTStateCalMRP"
                                pBOM.FieldValue = "0"
                                pBOM.FieldDataType = BOMDataType.DataString
                                pBOMListUpdate.Add(pBOM)


                                Dim pBOM2 As New BOMData
                                pBOM2.FieldName = "FTStateExportOptiplan"
                                pBOM2.FieldValue = "0"
                                pBOM2.FieldDataType = BOMDataType.DataString
                                pBOMListUpdate.Add(pBOM2)


                                If UpdateBOMItem(BOMSysID, pSeq, pBOMListUpdate) = False Then

                                    .SetRowCellValue(I, "FTStateCalMRP", "1")

                                Else
                                    .SetRowCellValue(I, "FTStateCalMRP", "0")
                                    .SetRowCellValue(I, "FTStateExportOptiplan", "0")

                                End If

                                pBOMListUpdate.Clear()

                            End If

                        Next
                    Case Keys.F3


                        Dim pSeq As Integer = 0

                        For I As Integer = 0 To .RowCount - 1

                            If .GetRowCellValue(I, "FTStateMatConfirm").ToString = "1" Then

                                .FocusedRowHandle = I

                                pSeq = Val(.GetRowCellValue(I, "FNSeq").ToString)

                                pBOMListUpdate.Clear()

                                Dim pBOM As New BOMData
                                pBOM.FieldName = "FTStateMatConfirm"
                                pBOM.FieldValue = "0"
                                pBOM.FieldDataType = BOMDataType.DataString
                                pBOMListUpdate.Add(pBOM)


                                If UpdateBOMItem(BOMSysID, pSeq, pBOMListUpdate) = False Then

                                    .SetRowCellValue(I, "FTStateMatConfirm", "1")
                                Else
                                    .SetRowCellValue(I, "FTStateMatConfirm", "0")

                                End If

                                pBOMListUpdate.Clear()

                            End If

                        Next

                End Select
            End With
        Catch ex As Exception
        End Try
    End Sub



    Dim textEdit As New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit

    Private Sub GridView1_CustomRowCellEdit(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs) Handles ogvmat.CustomRowCellEdit, ogvmatcolor.CustomRowCellEdit, ogvmatcolornote.CustomRowCellEdit, ogvmatsilkcolor.CustomRowCellEdit, ogvmatsize.CustomRowCellEdit
        If e.Column.DisplayFormat.FormatType = FormatType.None AndAlso sender.IsFilterRow(e.RowHandle) Then
            e.RepositoryItem = textEdit
        End If
    End Sub

    Private Sub RepositoryItemGridLookUpEditRMColor_KeyDown(sender As Object, e As KeyEventArgs) Handles RepositoryItemGridLookUpEditRMColor.KeyDown
        Try
            With Me.ogvmatcolor
                If .FocusedRowHandle < 0 Then Exit Sub
                Select Case e.KeyCode
                    Case Keys.F9

                        Dim pSeq As Integer = Val(.GetFocusedRowCellValue("FNSeq").ToString)

                        Dim mState As Integer = 1  ' Update by Code
                        Dim _ColorInt As Integer = .GetFocusedRowCellValue(.FocusedColumn.FieldName).ToString
                        Dim mColorCode As String = .GetFocusedRowCellDisplayText(.FocusedColumn.FieldName)
                        Dim pColorWay As String = ""
                        Dim _VisibleIndex As Integer = .FocusedColumn.VisibleIndex

                        Dim pNameTH As String = .GetFocusedRowCellValue(.FocusedColumn.FieldName & "TH").ToString
                        Dim pNameEN As String = .GetFocusedRowCellValue(.FocusedColumn.FieldName & "EN").ToString

                        Dim Spls As New HI.TL.SplashScreen("Loading... BOM Detail Please Wait.")

                        Try

                            For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                                If Microsoft.VisualBasic.Strings.Left(GridCol.Name, "gViewColor".Length) = "gViewColor" Then
                                    If GridCol.VisibleIndex > _VisibleIndex Then
                                        If GridCol.Visible = True Then



                                            Dim ColColorField As String = GridCol.FieldName.ToString
                                            Dim ColColorFieldTH As String = GridCol.FieldName.ToString & "TH"
                                            Dim ColColorFieldEN As String = GridCol.FieldName.ToString & "EN"
                                            pColorWay = GridCol.Caption

                                            If pColorWay <> "" Then

                                                pBOMListUpdate.Clear()

                                                Dim pBOM As New BOMData
                                                pBOM.FieldName = "FNHSysRawMatColorId"
                                                pBOM.FieldValue = _ColorInt
                                                pBOM.FieldValueTH = pNameTH
                                                pBOM.FieldValueEN = pNameEN

                                                pBOM.FieldDataType = BOMDataType.DataInteger
                                                pBOMListUpdate.Add(pBOM)

                                                If UpdateBOMColor(BOMSysID, pSeq, pColorWay, pBOMListUpdate) = False Then



                                                Else
                                                    .SetFocusedRowCellValue(ColColorField, Val(_ColorInt))
                                                    .SetFocusedRowCellValue(ColColorFieldTH, pNameTH)
                                                    .SetFocusedRowCellValue(ColColorFieldEN, pNameEN)

                                                End If

                                                pBOMListUpdate.Clear()



                                            End If

                                        End If

                                    End If

                                End If
                            Next



                        Catch ex As Exception

                        End Try

                        Spls.Close()

                    Case Keys.F6


                        Dim Spls As New HI.TL.SplashScreen("Updating... BOM Color Detail Please Wait.")

                        Try



                            If .FocusedRowHandle < 0 Then Exit Sub

                            Dim pColorWay As String = .FocusedColumn.Caption
                            Dim ColColorField As String = .FocusedColumn.FieldName.ToString
                            Dim ColColorFieldTH As String = .FocusedColumn.FieldName.ToString & "TH"
                            Dim ColColorFieldEN As String = .FocusedColumn.FieldName.ToString & "EN"
                            Dim pColorCode As String = "N/R"
                            Dim _ColorInt As Integer = 0
                            Dim pNameTH As String = ""
                            Dim pNameEN As String = ""

                            Dim cmd As String = ""
                            cmd = " Select  Top 1  B2.FNHSysRawMatColorId ,B2.FTRawMatColorNameEN ,B2.FTRawMatColorNameTH 
                                                                 From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TINVENMMatColor AS B2 WITH(NOLOCK)
																 Where B2.FTRawMatColorCode = '" & HI.UL.ULF.rpQuoted(pColorCode) & "'  And B2.FNHSysRawMatColorId <> 0 "


                            Dim dtMatColor As DataTable = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MASTER)


                            For Each R As DataRow In dtMatColor.Rows
                                _ColorInt = Integer.Parse(Val(R!FNHSysRawMatColorId.ToString))
                                pNameTH = R!FTRawMatColorNameTH.ToString
                                pNameEN = R!FTRawMatColorNameEN.ToString
                            Next
                            dtMatColor.Dispose()
                            If _ColorInt <> 0 Then

                                Dim pSeq As Integer

                                For Ridx As Integer = .FocusedRowHandle To .RowCount - 1
                                    pSeq = Integer.Parse(Val(.GetRowCellValue(Ridx, "FNSeq").ToString))

                                    If pSeq > 0 Then
                                        pBOMListUpdate.Clear()

                                        Dim pBOM As New BOMData
                                        pBOM.FieldName = "FNHSysRawMatColorId"
                                        pBOM.FieldValue = _ColorInt
                                        pBOM.FieldValueTH = pNameTH
                                        pBOM.FieldValueEN = pNameEN

                                        pBOM.FieldDataType = BOMDataType.DataInteger
                                        pBOMListUpdate.Add(pBOM)

                                        If UpdateBOMColor(BOMSysID, pSeq, pColorWay, pBOMListUpdate) = False Then



                                        Else
                                            .SetRowCellValue(Ridx, ColColorField, Val(_ColorInt))
                                            .SetRowCellValue(Ridx, ColColorFieldTH, pNameTH)
                                            .SetRowCellValue(Ridx, ColColorFieldEN, pNameEN)

                                        End If

                                        pBOMListUpdate.Clear()

                                    End If


                                Next


                            End If

                        Catch ex As Exception

                        End Try

                        Spls.Close()
                    Case Keys.Delete

                        Dim pSeq As Integer = Val(.GetFocusedRowCellValue("FNSeq").ToString)

                        Dim mState As Integer = 1  ' Update by Code
                        Dim _ColorInt As Integer = 0
                        Dim mColorCode As String = .GetFocusedRowCellDisplayText(.FocusedColumn.FieldName)
                        Dim pColorWay As String = ""

                        Dim ColColorField As String = .FocusedColumn.FieldName.ToString
                        Dim ColColorFieldTH As String = .FocusedColumn.FieldName.ToString & "TH"
                        Dim ColColorFieldEN As String = .FocusedColumn.FieldName.ToString & "EN"
                        pColorWay = .FocusedColumn.Caption


                        pBOMListUpdate.Clear()

                        Dim pBOM As New BOMData
                        pBOM.FieldName = "FNHSysRawMatColorId"
                        pBOM.FieldValue = _ColorInt
                        pBOM.FieldValueTH = ""
                        pBOM.FieldValueEN = ""

                        pBOM.FieldDataType = BOMDataType.DataInteger
                        pBOMListUpdate.Add(pBOM)

                        If UpdateBOMColor(BOMSysID, pSeq, pColorWay, pBOMListUpdate) = False Then



                        Else
                            .SetFocusedRowCellValue(ColColorField, Val(_ColorInt))
                            .SetFocusedRowCellValue(ColColorFieldTH, "")
                            .SetFocusedRowCellValue(ColColorFieldEN, "")

                        End If

                        pBOMListUpdate.Clear()


                End Select
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryItemGridLookUpEditRMSize_KeyDown(sender As Object, e As KeyEventArgs) Handles RepositoryItemGridLookUpEditRMSize.KeyDown
        Try
            With Me.ogvmatsize
                If .FocusedRowHandle < 0 Then Exit Sub
                Select Case e.KeyCode
                    Case Keys.F9

                        Dim pSeq As Integer = Val(.GetFocusedRowCellValue("FNSeq").ToString)

                        Dim mState As Integer = 1  ' Update by Code
                        Dim _SizeInt As Integer = .GetFocusedRowCellValue(.FocusedColumn.FieldName).ToString
                        Dim mSizeCode As String = .GetFocusedRowCellDisplayText(.FocusedColumn.FieldName)
                        Dim pSizeBD As String = ""
                        Dim _VisibleIndex As Integer = .FocusedColumn.VisibleIndex



                        Dim Spls As New HI.TL.SplashScreen("Loading... BOM Detail Please Wait.")

                        Try

                            For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                                If Microsoft.VisualBasic.Strings.Left(GridCol.Name, "gViewSize".Length) = "gViewSize" Then
                                    If GridCol.VisibleIndex > _VisibleIndex Then
                                        If GridCol.Visible = True Then



                                            Dim ColColorField As String = GridCol.FieldName.ToString

                                            pSizeBD = GridCol.Caption

                                            If pSizeBD <> "" Then

                                                pBOMListUpdate.Clear()


                                                Dim pBOM As New BOMData
                                                pBOM.FieldName = "FNHSysRawMatSizeId"
                                                pBOM.FieldValue = _SizeInt
                                                pBOM.FieldDataType = BOMDataType.DataInteger
                                                pBOMListUpdate.Add(pBOM)

                                                If UpdateBOMSize(BOMSysID, pSeq, pSizeBD, pBOMListUpdate) = False Then


                                                Else
                                                    .SetFocusedRowCellValue(ColColorField, Val(_SizeInt))
                                                End If


                                                pBOMListUpdate.Clear()



                                            End If

                                        End If

                                    End If

                                End If
                            Next



                        Catch ex As Exception

                        End Try

                        Spls.Close()
                    Case Keys.F6


                        Dim Spls As New HI.TL.SplashScreen("Updating... BOM Color Detail Please Wait.")

                        Try



                            If .FocusedRowHandle < 0 Then Exit Sub

                            Dim pSize As String = .FocusedColumn.Caption
                            Dim ColSizeField As String = .FocusedColumn.FieldName.ToString

                            Dim pSizeCode As String = "N/R"
                            Dim _SizeInt As Integer = 0

                            Dim cmd As String = ""
                            cmd = " Select  Top 1  B2.FNHSysRawMatSizeId 
                                                                 From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TINVENMMatSize AS B2 WITH(NOLOCK)
																 Where B2.FTRawMatSizeCode = '" & HI.UL.ULF.rpQuoted(pSizeCode) & "'  And B2.FNHSysRawMatSizeId <> 0 "




                            Dim dtMatColor As DataTable = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MASTER)


                            For Each R As DataRow In dtMatColor.Rows
                                _SizeInt = Integer.Parse(Val(R!FNHSysRawMatSizeId.ToString))

                            Next

                            dtMatColor.Dispose()
                            If _SizeInt <> 0 Then
                                Dim pSeq As Integer

                                For Ridx As Integer = .FocusedRowHandle To .RowCount - 1
                                    pSeq = Integer.Parse(Val(.GetRowCellValue(Ridx, "FNSeq").ToString))

                                    If pSeq > 0 Then
                                        pBOMListUpdate.Clear()

                                        Dim pBOM As New BOMData
                                        pBOM.FieldName = "FNHSysRawMatSizeId"
                                        pBOM.FieldValue = _SizeInt


                                        pBOM.FieldDataType = BOMDataType.DataInteger
                                        pBOMListUpdate.Add(pBOM)

                                        If UpdateBOMSize(BOMSysID, pSeq, pSize, pBOMListUpdate) = False Then



                                        Else
                                            .SetRowCellValue(Ridx, ColSizeField, Val(_SizeInt))


                                        End If

                                        pBOMListUpdate.Clear()

                                    End If


                                Next

                            End If


                        Catch ex As Exception

                        End Try

                        Spls.Close()
                    Case Keys.Delete
                        Dim pSeq As Integer = Val(.GetFocusedRowCellValue("FNSeq").ToString)

                        Dim mState As Integer = 1  ' Update by Code
                        Dim _SizeInt As Integer = 0
                        Dim mSizeCode As String = .GetFocusedRowCellDisplayText(.FocusedColumn.FieldName)
                        Dim pSizeBD As String = ""


                        Dim ColColorField As String = .FocusedColumn.FieldName.ToString

                        pSizeBD = .FocusedColumn.Caption

                        If pSizeBD <> "" Then

                            pBOMListUpdate.Clear()


                            Dim pBOM As New BOMData
                            pBOM.FieldName = "FNHSysRawMatSizeId"
                            pBOM.FieldValue = _SizeInt
                            pBOM.FieldDataType = BOMDataType.DataInteger
                            pBOMListUpdate.Add(pBOM)

                            If UpdateBOMSize(BOMSysID, pSeq, pSizeBD, pBOMListUpdate) = False Then


                            Else
                                .SetFocusedRowCellValue(ColColorField, Val(_SizeInt))
                            End If


                            pBOMListUpdate.Clear()



                        End If





                End Select
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryItemGridLookUpEditFTUnitCode_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemGridLookUpEditFTUnitCode.EditValueChanging, RepositoryItemGridLookUpEditFTCurCode.EditValueChanging, RepositoryItemGridLookUpEditFTSuplCode.EditValueChanging
        Try

            RepositoryItemGridLookUpEditFTSuplCode_EditValueChanged(sender, New EventArgs)

        Catch ex As Exception
        End Try

    End Sub

    Private Sub RepositoryItemGridLookUpEditRMColor_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemGridLookUpEditRMColor.EditValueChanging
        Try
            RepositoryItemGridLookUpEditRMColor_EditValueChanged(sender, New EventArgs)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryItemGridLookUpEditRMSize_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemGridLookUpEditRMSize.EditValueChanging
        Try
            RepositoryItemGridLookUpEditRMSize_EditValueChanged(sender, New EventArgs)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvmat_ColumnFilterChanged(sender As Object, e As EventArgs) Handles ogvmat.ColumnFilterChanged
        Try

            Dim gFilterString As String = ogvmat.ActiveFilterString

            ogvmatcolor.ActiveFilterString = gFilterString
            ogvmatsize.ActiveFilterString = gFilterString
            ogvmatcolornote.ActiveFilterString = gFilterString

            'For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvmat.Columns

            '    Try
            '        Dim K As String = GridCol.FilterInfo.Value.ToString

            '        Try
            '            If K <> "" Then
            '                ogvmatcolor.Columns.ColumnByFieldName(GridCol.FieldName).FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[" & GridCol.FieldName & "] Like '%" & K & "%'", K, K, ColumnFilterType.Value)
            '                ogvmatsize.Columns.ColumnByFieldName(GridCol.FieldName).FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[" & GridCol.FieldName & "] Like '%" & K & "%'", K, K, ColumnFilterType.Value)
            '                ogvmatcolornote.Columns.ColumnByFieldName(GridCol.FieldName).FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[" & GridCol.FieldName & "] Like '%" & K & "%'", K, K, ColumnFilterType.Value)

            '            Else
            '                ogvmatcolor.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
            '                ogvmatsize.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
            '                ogvmatcolornote.Columns.ColumnByFieldName(GridCol.FieldName).ClearFilter()
            '            End If

            '        Catch ex As Exception

            '        End Try
            '    Catch ex As Exception

            '    End Try




            ' Next




        Catch ex As Exception


        End Try
    End Sub

    Private Sub ogvmat_ColumnPositionChanged(sender As Object, e As EventArgs) Handles ogvmat.ColumnPositionChanged

        Try
            HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, ogvmat)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ogvmatcolor_ColumnPositionChanged(sender As Object, e As EventArgs) Handles ogvmatcolor.ColumnPositionChanged

        Try
            HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, ogvmatcolor)
        Catch ex As Exception

        End Try


    End Sub


    Private Sub ogvmatsize_ColumnPositionChanged(sender As Object, e As EventArgs) Handles ogvmatsize.ColumnPositionChanged

        Try
            HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, ogvmatsize)
        Catch ex As Exception

        End Try


    End Sub

    Private Sub ogvmatcolornote_ColumnPositionChanged(sender As Object, e As EventArgs) Handles ogvmatcolornote.ColumnPositionChanged

        Try
            HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, ogvmatcolornote)
        Catch ex As Exception

        End Try


    End Sub

    Private Sub ogvmatsilkcolor_ColumnPositionChanged(sender As Object, e As EventArgs) Handles ogvmatsilkcolor.ColumnPositionChanged

        Try

            HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, ogvmatsilkcolor)

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ockselectorderall_CheckedChanged(sender As Object, e As EventArgs) Handles ockselectorderall.CheckedChanged
        Try

            If Not Me.ogdlistorder.DataSource Is Nothing Then
                If ogvlistorder.RowCount > 0 Then

                    Dim cState As String = "0"

                    If ockselectorderall.Checked Then
                        cState = "1"
                    End If

                    With ogvlistorder
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, "FTSelect", cState)

                        Next
                    End With
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvmatcolor_DragObjectDrop(sender As Object, e As DragObjectDropEventArgs) Handles ogvmatcolor.DragObjectDrop
        Try

            Dim oBjGrid As DevExpress.XtraGrid.Columns.GridColumn = CType(e.DragObject, DevExpress.XtraGrid.Columns.GridColumn)

            Dim StateFound As Boolean = False
            Dim ColSeq As Integer = 0
            'For Each GrCol As DevExpress.XtraGrid.Columns.GridColumn In ogvmatcolor.Columns

            '    Select Case Microsoft.VisualBasic.Left(GrCol.Name.ToString, 4).ToUpper
            '        Case "CFIX".ToUpper

            '        Case Else

            '            If GrCol.Visible Then
            '                If GrCol.VisibleIndex > oBjGrid.VisibleIndex Then
            '                    ColSeq = ColSeq + 1
            '                    StateFound = True
            '                    Exit For
            '                Else
            '                    ColSeq = ColSeq + 1
            '                End If
            '            End If

            '    End Select

            'Next
            ColSeq = 1
            With ogvmatcolor
                .BeginInit()
                For I As Integer = 0 To .Columns.Count - 1
                    Select Case Microsoft.VisualBasic.Left(.Columns(I).Name.ToString, 4).ToUpper
                        Case "CFIX".ToUpper

                        Case Else

                            If .Columns(I).Visible Then
                                Dim pCap As String = .Columns(I).Caption
                                Dim pIdx As Integer = .Columns(I).AbsoluteIndex
                                Dim pIdx2 As Integer = .Columns(I).VisibleIndex

                                If .Columns(I).VisibleIndex < oBjGrid.VisibleIndex Then
                                    ColSeq = ColSeq + 1
                                    'Else
                                    '    ColSeq = ColSeq + 1
                                End If
                            End If

                    End Select

                Next

                .EndInit()
            End With


            '  oBjGrid.VisibleIndex
            If ColSeq > 0 Then
                Dim cmdstring As String = ""

                cmdstring = "EXEC " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_GETBOMGARMENT_UPDATEBREAKDOWNSORTSEQ '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & BOMSysID & ",'" & HI.UL.ULF.rpQuoted(oBjGrid.Caption) & "',''," & ColSeq & ""
                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ogvmatsize_DragObjectDrop(sender As Object, e As DragObjectDropEventArgs) Handles ogvmatsize.DragObjectDrop
        Try

            Dim oBjGrid As DevExpress.XtraGrid.Columns.GridColumn = CType(e.DragObject, DevExpress.XtraGrid.Columns.GridColumn)

            Dim StateFound As Boolean = False
            Dim ColSeq As Integer = 0
            For Each GrCol As DevExpress.XtraGrid.Columns.GridColumn In ogvmatsize.Columns

                Select Case Microsoft.VisualBasic.Left(GrCol.Name.ToString, 4).ToUpper
                    Case "CFIX".ToUpper

                    Case Else

                        If GrCol.Visible Then
                            If GrCol.Caption = oBjGrid.Caption Then
                                ColSeq = ColSeq + 1
                                StateFound = True
                                Exit For
                            Else
                                ColSeq = ColSeq + 1
                            End If
                        End If

                End Select

            Next

            If StateFound Then
                Dim cmdstring As String = ""


                cmdstring = "EXEC " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.USP_GETBOMGARMENT_UPDATEBREAKDOWNSORTSEQ '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & BOMSysID & ",'','" & HI.UL.ULF.rpQuoted(oBjGrid.Caption) & "'," & ColSeq & ""
                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepositoryFNOrderSetType_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryFNOrderSetType.EditValueChanging
        Try
            If FTStateStyleSet.Checked = False Then
                If e.NewValue.ToString = 0 Then
                    e.Cancel = False

                Else
                    e.Cancel = True
                End If
            Else
                If e.NewValue.ToString = 0 Then
                    e.Cancel = True

                Else
                    e.Cancel = False
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub RepositoryItemCheckEditSilkColor_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCheckEditSilkColor.EditValueChanging
        Try

            With Me.ogvSilkColor
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim pValue As String = "0"
                If e.NewValue.ToString = "1" Then
                    pValue = "1"
                End If


                .SetFocusedRowCellValue(.FocusedColumn.FieldName, pValue)



            End With

        Catch ex As Exception
            e.Cancel = True

        End Try
    End Sub

    Private Sub RepositoryItemPopupContainerEditSilkColor_KeyDown(sender As Object, e As KeyEventArgs) Handles RepositoryItemPopupContainerEditSilkColor.KeyDown
        Try
            With Me.ogvmatsilkcolor
                If .FocusedRowHandle < 0 Then Exit Sub
                Select Case e.KeyCode
                    Case Keys.F9

                        Dim pSeq As Integer = Val(.GetFocusedRowCellValue("FNSeq").ToString)
                        Dim mState As Integer = 1  ' Update by Code
                        Dim mColorWay As String = .FocusedColumn.Caption
                        Dim cmdstring As String = ""

                        If .GetFocusedRowCellDisplayText(.FocusedColumn.FieldName) <> "" Then

                            Dim pColorWay As String = ""
                            Dim _VisibleIndex As Integer = .FocusedColumn.VisibleIndex

                            Dim Spls As New HI.TL.SplashScreen("Loading... BOM Detail Please Wait.")

                            Try

                                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                                    If Microsoft.VisualBasic.Strings.Left(GridCol.Name, "gViewColor".Length) = "gViewColor" Then
                                        If GridCol.VisibleIndex > _VisibleIndex Then
                                            If GridCol.Visible = True Then



                                                Dim ColColorField As String = GridCol.FieldName.ToString

                                                pColorWay = GridCol.Caption

                                                If pColorWay <> "" Then
                                                    cmdstring = " declare @Rec int =0  "
                                                    cmdstring &= vbCrLf & " delete from " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SilkColorwayMaterial where  FNHSysBomId=" & BOMSysID & " And FNSeq =" & pSeq & " And FTColorway='" & HI.UL.ULF.rpQuoted(pColorWay) & "' "
                                                    cmdstring &= vbCrLf & "   INSERT INTO " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SilkColorwayMaterial (FTInsUser, FDInsDate, FTInsTime, FNHSysBomId, FNSeq,"
                                                    cmdstring &= vbCrLf & "  FNColorWaySeq, FNHSysMatColorId, FTColorway, FNHSysRawMatColorId, FNMatSeq, FTRawMatColorNameTH,  FTRawMatColorNameEN, FTSilkNote) "
                                                    cmdstring &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                                    cmdstring &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB & ""
                                                    cmdstring &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB & ""
                                                    cmdstring &= vbCrLf & "," & BOMSysID & ""
                                                    cmdstring &= vbCrLf & "," & pSeq & ""
                                                    cmdstring &= vbCrLf & ",M.FNColorWaySeq"
                                                    cmdstring &= vbCrLf & ",M.FNHSysMatColorId"
                                                    cmdstring &= vbCrLf & ",M.FTColorway"
                                                    cmdstring &= vbCrLf & ",B.FNHSysRawMatColorId"
                                                    cmdstring &= vbCrLf & ",B.FNMatSeq "
                                                    cmdstring &= vbCrLf & ",B.FTRawMatColorNameTH,B.FTRawMatColorNameEN,B.FTSilkNote "

                                                    cmdstring &= vbCrLf & "    FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SilkColorway AS M  "
                                                    cmdstring &= vbCrLf & "   CROSS APPLY (SELECT MIN(B.FNColorWaySeq) As FNColorWaySeq, B.FNHSysMatColorId, B.FTColorway, B.FNHSysRawMatColorId, MIN(B.FNMatSeq) As FNMatSeq "
                                                    cmdstring &= vbCrLf & " , MAX(B.FTRawMatColorNameTH) As FTRawMatColorNameTH,  MAX(B.FTRawMatColorNameEN) As FTRawMatColorNameEN, MAX(B.FTSilkNote) AS FTSilkNote "
                                                    cmdstring &= vbCrLf & "FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SilkColorwayMaterial AS B WITH(NOLOCK) "
                                                    cmdstring &= vbCrLf & " WHERE  B.FNHSysBomId =" & BOMSysID & " And B.FNSeq=" & pSeq & " And B.FTColorway='" & HI.UL.ULF.rpQuoted(mColorWay) & "' "
                                                    cmdstring &= vbCrLf & " GROUP BY B.FNHSysMatColorId, B.FTColorway, B.FNHSysRawMatColorId "

                                                    cmdstring &= vbCrLf & " )  As B "
                                                    cmdstring &= vbCrLf & "     WHERE   M.FNHSysBomId = " & BOMSysID & " And M.FNSeq = " & pSeq & "  And M.FTColorway='" & HI.UL.ULF.rpQuoted(pColorWay) & "'   "



                                                    cmdstring &= vbCrLf & " UPDATE  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SilkColorway   SET  "
                                                    cmdstring &= vbCrLf & " FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                                    cmdstring &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                                                    cmdstring &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                                                    cmdstring &= vbCrLf & "     WHERE  FNHSysBomId = " & BOMSysID & " And FNSeq = " & pSeq & "  AND FTColorway='" & HI.UL.ULF.rpQuoted(pColorWay) & "' "

                                                    cmdstring &= vbCrLf & " SET @Rec = @@ROWCOUNT  "
                                                    cmdstring &= vbCrLf & "  Select  Top 1   @Rec AS FNState, ISNULL(RMC.FTColorRM, '') AS FTRawMatColorCode "
                                                    cmdstring &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SilkColorway  AS TR  WITH(NOLOCK)  "


                                                    cmdstring &= vbCrLf & "   OUTER APPLY(select  STUFF((SELECT  ',' + FTRawMatColorCode    "
                                                    cmdstring &= vbCrLf & " From( SELECT  MIN(RMC.FNMatSeq) AS FNMatSeq ,C.FTRawMatColorCode   "
                                                    cmdstring &= vbCrLf & " 	From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SilkColorwayMaterial AS  RMC WITH(NOLOCK)"
                                                    cmdstring &= vbCrLf & " 	INNER Join " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TINVENMMatColor AS C ON RMC.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
                                                    cmdstring &= vbCrLf & "  WHERE    RMC.FNHSysBomId = TR.FNHSysBomId "
                                                    cmdstring &= vbCrLf & " 	       And RMC.FNSeq=TR.FNSeq "
                                                    cmdstring &= vbCrLf & " 	       And RMC.FTColorway = TR.FTColorway GROUP BY C.FTRawMatColorCode  "
                                                    cmdstring &= vbCrLf & " 	 ) AS T  "
                                                    cmdstring &= vbCrLf & "  For Xml PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'')  AS FTColorRM ) AS  RMC "

                                                    cmdstring &= vbCrLf & "     WHERE  TR.FNHSysBomId = " & BOMSysID & " And TR.FNSeq = " & pSeq & "  AND TR.FTColorway='" & HI.UL.ULF.rpQuoted(pColorWay) & "' "

                                                    Dim mdt As DataTable
                                                    mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                                                    If mdt.Rows.Count > 0 Then

                                                        If Val(mdt.Rows(0)!FNState.ToString) > 0 Then

                                                            With Me.ogvmatsilkcolor

                                                                For Each Rxp As DataRow In mdt.Rows

                                                                    .SetFocusedRowCellValue(.FocusedColumn.FieldName, Rxp!FTRawMatColorCode.ToString)


                                                                Next

                                                            End With

                                                            mdt.Dispose()

                                                        Else
                                                            mdt.Dispose()

                                                        End If


                                                    Else
                                                        mdt.Dispose()

                                                    End If

                                                End If

                                            End If

                                        End If

                                    End If
                                Next



                            Catch ex As Exception

                            End Try

                            Spls.Close()
                        End If

                    Case Keys.Delete

                        Dim pSeq As Integer = Val(.GetFocusedRowCellValue("FNSeq").ToString)
                        Dim mState As Integer = 1  ' Update by Code
                        Dim mColorWay As String = .FocusedColumn.Caption
                        Dim cmdstring As String = ""

                        cmdstring = " declare @Rec int =0  "
                        cmdstring &= vbCrLf & " delete from    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SilkColorwayMaterial where  FNHSysBomId=" & BOMSysID & " AND FNSeq =" & pSeq & " AND FTColorway='" & HI.UL.ULF.rpQuoted(mColorWay) & "' "
                        cmdstring &= vbCrLf & " SET @Rec = @@ROWCOUNT  "
                        cmdstring &= vbCrLf & "  Select  Top 1   @Rec AS FNState, ISNULL(RMC.FTColorRM, '') AS FTRawMatColorCode "
                        cmdstring &= vbCrLf & "  From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SilkColorway  AS TR  WITH(NOLOCK)  "


                        cmdstring &= vbCrLf & "   OUTER APPLY(select  STUFF((SELECT  ',' + FTRawMatColorCode    "
                        cmdstring &= vbCrLf & " From( SELECT  MIN(RMC.FNMatSeq) AS FNMatSeq ,C.FTRawMatColorCode   "
                        cmdstring &= vbCrLf & " 	From " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTBOM_SilkColorwayMaterial AS  RMC WITH(NOLOCK)"
                        cmdstring &= vbCrLf & " 	INNER Join " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TINVENMMatColor AS C ON RMC.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
                        cmdstring &= vbCrLf & "  WHERE    RMC.FNHSysBomId = TR.FNHSysBomId "
                        cmdstring &= vbCrLf & " 	       And RMC.FNSeq=TR.FNSeq "
                        cmdstring &= vbCrLf & " 	       And RMC.FTColorway = TR.FTColorway GROUP BY C.FTRawMatColorCode  "
                        cmdstring &= vbCrLf & " 	 ) AS T  "
                        cmdstring &= vbCrLf & "  For Xml PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'')  AS FTColorRM ) AS  RMC "

                        cmdstring &= vbCrLf & "     WHERE  TR.FNHSysBomId = " & BOMSysID & " And TR.FNSeq = " & pSeq & "  AND TR.FTColorway='" & HI.UL.ULF.rpQuoted(mColorWay) & "' "

                        Dim mdt As DataTable
                        mdt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                        If mdt.Rows.Count > 0 Then

                            If Val(mdt.Rows(0)!FNState.ToString) > 0 Then

                                With Me.ogvmatsilkcolor

                                    For Each Rxp As DataRow In mdt.Rows

                                        .SetFocusedRowCellValue(.FocusedColumn.FieldName, Rxp!FTRawMatColorCode.ToString)


                                    Next

                                End With

                                mdt.Dispose()

                            Else
                                mdt.Dispose()

                            End If


                        Else
                            mdt.Dispose()

                        End If


                End Select
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocksubjobselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ocksubjobselectall.CheckedChanged
        Try

            If Not Me.ogdlistsuborder.DataSource Is Nothing Then
                If ogvlistsuborder.RowCount > 0 Then

                    Dim cState As String = "0"

                    If ocksubjobselectall.Checked Then
                        cState = "1"
                    End If

                    With ogvlistsuborder
                        For I As Integer = 0 To .RowCount - 1

                            .SetRowCellValue(I, "FTSelect", cState)

                        Next
                    End With
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub
End Class