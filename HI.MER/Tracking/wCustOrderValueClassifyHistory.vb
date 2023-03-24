Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ButtonEdit
Imports DevExpress.XtraGrid.Filter
Imports DevExpress.XtraPrinting
Imports System.Data.Common
Imports System.Windows.Forms
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.Configuration
Imports System.Diagnostics
Imports DevExpress.XtraPrintingLinks
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized

Public Class wCustOrderValueClassifyHistory


    Dim View As GridView
    Dim RowsIndex As Double
    Dim TopVisibleIndex As Int32
    Private sFNHSysStyleId As String

    ''' Used Data Adapter to control database

    Dim oleDbDataAdapter1 As DbDataAdapter
    Dim oleDbDataAdapter2 As DbDataAdapter
    Dim dtStyleDetail As DataTable

    Dim AppConfig As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
    Dim saveFileDialog As SaveFileDialog = New SaveFileDialog()
    Dim selectedFile As String = ""
    Dim lastSelectedPath As String = ""
    Dim sectionName As String = "appSettings"
    Dim newKey As String = "KeepLastBrowseFolder"

    Private Inited As Boolean
    Dim FirstLoad As Boolean = True

#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(ByVal value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(ByVal value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(ByVal value As String)
            _CallMethodParm = value
        End Set
    End Property

#End Region

#Region "Handler Control"

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'With RepositoryFTMainMatCode
        '    AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
        '    AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
        '    AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
        '    AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave

        'End With

    End Sub

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(value As Boolean)
            _ProcComplete = value
        End Set
    End Property

#End Region

#Region "MAIN PROC"

    Private Sub wForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            FirstLoad = False
            FNHSysCmpId = GetPropertyTagValue(FNHSysCmpId)
            FNHSysBuyId = GetPropertyTagValue(FNHSysBuyId)
            FNHSysStyleId = GetPropertyTagValue(FNHSysStyleId)
            FNHSysCustId = GetPropertyTagValue(FNHSysCustId)
            FNHSysPOID = GetPropertyTagValue(FNHSysPOID)

            View = New GridView()
            View = ogcmatcode.Views(0)
            View.FocusedRowHandle = 0

            ogcmatcode.ViewCollection.Add(View)
            ogcmatcode.MainView = View

            View.GridControl = ogcmatcode
            View.OptionsView.ShowAutoFilterRow = False
            View.OptionsView.NewItemRowPosition = NewItemRowPosition.None
            View.OptionsNavigation.AutoFocusNewRow = True
            View.OptionsBehavior.AllowAddRows = True
            View.OptionsBehavior.AllowDeleteRows = True
            View.OptionsBehavior.Editable = True
            View.BestFitColumns()

            sFNHSysStyleId = ""

            Dim section As System.Configuration.AppSettingsSection = _
                DirectCast(AppConfig.GetSection(sectionName),  _
                    System.Configuration.AppSettingsSection)
            For Each setting As System.Configuration.SettingElement In section.Settings
                Dim value As String = setting.Value.ValueXml.InnerText
                Dim name As String = setting.Name
                If name.ToLower = sectionName.ToLower Then
                    lastSelectedPath = value
                    Return
                End If
            Next

            'lastSelectedPath =""

        Catch ex As Exception
            lastSelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        End Try

        If (lastSelectedPath <> "") Then
            saveFileDialog.InitialDirectory = lastSelectedPath
        Else
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        End If
        saveFileDialog.ValidateNames = True
        saveFileDialog.DereferenceLinks = False     ' Will return .lnk in shortcuts.
        saveFileDialog.Filter = "Excel |*.xlsx"

    End Sub

    Private Sub Proc_Close(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub FNHSysCmpId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysCmpId.EditValueChanged
        FNHSysCmpId = GetPropertyTagValue(FNHSysCmpId)
        If FirstLoad = False Then
            Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, "") 'FNHSysSeasonId.Properties.Tag.ToString)
            ogcmatcode.Refresh()
        End If
    End Sub

    Private Sub FNHSysBuyId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysBuyId.EditValueChanged
        FNHSysBuyId = GetPropertyTagValue(FNHSysBuyId)
        If FirstLoad = False Then
            Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, "") 'FNHSysSeasonId.Properties.Tag.ToString)
            ogcmatcode.Refresh()
        End If
    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysStyleId.EditValueChanged
        FNHSysStyleId = GetPropertyTagValue(FNHSysStyleId)
        If FirstLoad = False Then
            Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, "") 'FNHSysSeasonId.Properties.Tag.ToString)
            ogcmatcode.Refresh()
        End If
    End Sub

    Private Sub FNHSysCustId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysCustId.EditValueChanged
        FNHSysCustId = GetPropertyTagValue(FNHSysCustId)
        If FirstLoad = False Then
            Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, "") 'FNHSysSeasonId.Properties.Tag.ToString)
            ogcmatcode.Refresh()
        End If
    End Sub

    'Private Sub FNHSysSeasonId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysSeasonId.EditValueChanged
    '    FNHSysSeasonId = GetPropertyTagValue(FNHSysSeasonId)
    '    Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
    '    ogcmatcode.Refresh()
    'End Sub

    Private Sub FNHSysPOID_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysPOID.EditValueChanged
        FNHSysPOID = GetPropertyTagValue(FNHSysPOID)
        If FirstLoad = False Then
            Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, "") 'FNHSysSeasonId.Properties.Tag.ToString)
            ogcmatcode.Refresh()
        End If
    End Sub

    Private Sub FXShipDate1_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FTStartOrderDate.EditValueChanged, FTEndOrderDate.EditValueChanged, FTStartShipment.EditValueChanged, FTEndShipment.EditValueChanged
        Dim ShipDate As String = ""
        If FirstLoad = False Then
            Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, "")
            ogcmatcode.Refresh()
        End If
    End Sub

    Private Function GetPropertyTagValue(ByVal obj As Object) As Control
        Dim _Str As String = ""
        Dim sTable As String = ""
        Dim sField As String = ""
        Dim sValue As String = ""
        Dim sCrite As String = ""

        Select Case obj.GetType.FullName.ToString.ToUpper
            Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                If CType(obj, DevExpress.XtraEditors.ButtonEdit).Properties.Tag = "" Then
                    Select Case obj.GetType.FullName.ToUpper
                        Case "FNHSysCmpId".ToUpper
                            sTable = "HITECH_MASTER.dbo.TCNMCmp"
                            sField = obj.GetType.FullName.ToString()
                            sCrite = "FTCmpCode"
                            sValue = CType(obj, DevExpress.XtraEditors.ButtonEdit).Text
                        Case "FNHSysStyleId".ToUpper
                            sTable = "HITECH_MASTER.dbo.TMERMStyle"
                            sField = obj.GetType.FullName.ToString()
                            sCrite = "FTStyleCode"
                            sValue = CType(obj, DevExpress.XtraEditors.ButtonEdit).Text
                        Case "FNHSysCustId".ToUpper
                            sTable = "HITECH_MASTER.dbo.TCNMCustomer"
                            sField = obj.GetType.FullName.ToString()
                            sCrite = "FTCustCode"
                            sValue = CType(obj, DevExpress.XtraEditors.ButtonEdit).Text
                        Case "FNHSysBuyId".ToUpper
                            sTable = "HITECH_MASTER.dbo.TMERMBuy"
                            sField = obj.GetType.FullName.ToString()
                            sCrite = "FTBuyCode"
                            sValue = CType(obj, DevExpress.XtraEditors.ButtonEdit).Text
                        Case "FNHSysSeasonId".ToUpper
                            sTable = "HITECH_MASTER.dbo.TMERMSeason"
                            sField = obj.GetType.FullName.ToString()
                            sCrite = "FTSeasonCode"
                            sValue = CType(obj, DevExpress.XtraEditors.ButtonEdit).Text
                        Case "FNHSysPOID".ToUpper
                            sTable = "HITECH_MERCHAN.DBO.TMERTORDER"
                            sField = "FTPOREF"
                            sCrite = "FTPOREF"
                            sValue = CType(obj, DevExpress.XtraEditors.ButtonEdit).Text
                    End Select
                    If sTable <> "" Then
                        _Str = "SELECT TOP 1 " & sField & " FROM " & sTable & " WITH(NOLOCK) WHERE " & sCrite & " = '" & sValue & "'"
                        CType(obj, DevExpress.XtraEditors.ButtonEdit).Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
                    Else
                        CType(obj, DevExpress.XtraEditors.ButtonEdit).Properties.Tag = ""
                    End If

                End If
        End Select

        Return obj
    End Function

    Private Sub LoadOrderListingInfo(ByVal _FNHSysCmpId As String, ByVal _FTPORef As String, ByVal _FNHSysStyleId As String, ByVal _FNHSysCustId As String, ByVal _FNHSysBuyId As String, ByVal _FDShipDate As String)
        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        If _FNHSysCmpId.Trim() = "" Then _FNHSysCmpId = "0"
        If _FNHSysStyleId.Trim() = "" Then _FNHSysStyleId = "0"
        If _FNHSysCustId.Trim() = "" Then _FNHSysCustId = "0"
        If _FNHSysBuyId.Trim() = "" Then _FNHSysBuyId = "0"
        If _FDShipDate.Trim() = "" Then _FDShipDate = ""

        'Dim sqlCmd As New SqlCommand
        'sqlCmd.Connection = HI.Conn.SQLConn.Cnn
        'sqlCmd.CommandType = CommandType.StoredProcedure
        'sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_ORDER_VALUE_CLASSIFY_INFO]"
        ''sqlCmd.Parameters.AddWithValue("@FNHSysCmpId", _FNHSysCmpId)
        'sqlCmd.Parameters.AddWithValue("@FTPORef", _FTPORef)

        'REM 2014/05/24
        ''---------------------------------------------------------------------------
        ''sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", _FNHSysStyleId)
        ''sqlCmd.Parameters.AddWithValue("@FNHSysCustId", _FNHSysCustId)
        ''sqlCmd.Parameters.AddWithValue("@FNHSysBuyId", _FNHSysBuyId)
        ''---------------------------------------------------------------------------
        'sqlCmd.Parameters.AddWithValue("@FNHSysCmpId", Val(_FNHSysCmpId))
        'sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", Val(_FNHSysStyleId))
        'sqlCmd.Parameters.AddWithValue("@FNHSysCustId", Val(_FNHSysCustId))
        'sqlCmd.Parameters.AddWithValue("@FNHSysBuyId", Val(_FNHSysBuyId))

        'sqlCmd.Parameters.AddWithValue("@FDShipDate", _FDShipDate)
        'sqlCmd.Parameters.AddWithValue("@LANGID", HI.ST.Lang.Language.ToString())

        'Dim sqlDA As New SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
        'sqlDA.SelectCommand = sqlCmd
        Dim dt As New DataTable
        'sqlDA.Fill(dt)

        Dim _Qry As String = ""

        _Qry = " 	SELECT FNHSysCmpId, FTCmpCode, FTCompName, FTPORef, FNHSysStyleId, FTStyleCode, FTStyleName, FTOrderNo, "
        _Qry &= vbCrLf & " 		  FTSubOrderNo, FNHSysCustId, FTCustCode, FTCustName, FNHSysBuyId, FTBuyCode, FNHSysSeasonId, FTSeasonCode, "
        _Qry &= vbCrLf & " 		  FNHSysProdTypeId, FTProdTypeCode, FTProdTypeName, FDOrderDate, FDShipDate, FNHSysCountryId, FTCountryCode, "
        _Qry &= vbCrLf & " 		  FTCountryName, FTCurCode,			  "
        _Qry &= vbCrLf & " 		  SUM(ISNULL(FNQuantity, 0)) AS FNQuantity, SUM(ISNULL(FNQuantityExtra, 0)) AS FNQuantityExtra, SUM(FNTotalQuantity) AS FNTotalQuantity, 						  "
        _Qry &= vbCrLf & " 		  SUM(ISNULL(FNAmt, 0)) AS FNAmt, SUM(ISNULL(FNAmntExtra, 0)) AS FNAmntExtra, SUM(ISNULL(FNTotalAmount, 0)) AS FNTotalAmount,"
        _Qry &= vbCrLf & " 		  SUM(FNAmountMST) AS FNAmountMST, SUM(FNAmountExtraMST) AS FNAmountExtraMST, SUM(FNTotalAmountMST) AS FNTotalAmountMST,FTPlantName"
        _Qry &= vbCrLf & " 	 FROM ("
        _Qry &= vbCrLf & " 		SELECT     ORD.FNHSysCmpId, COM.FTCmpCode"
        _Qry &= vbCrLf & "  , ORD.FTPORef, ORD.FNHSysStyleId, STYLE.FTStyleCode	, ORD.FTOrderNo"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & " 	,COM.FTCmpNameTH AS FTCompName "
            _Qry &= vbCrLf & " 	,STYLE.FTStyleNameTH AS FTStyleName "
            _Qry &= vbCrLf & " 	,CUS.FTCustNameTH  AS FTCustName"
            _Qry &= vbCrLf & "  ,CON.FTCountryNameTH  AS FTCountryName "
            _Qry &= vbCrLf & "  ,PROD.FTProdTypeNameTH  AS FTProdTypeName"
            _Qry &= vbCrLf & "  ,MPPL.FTPlantNameTH  AS FTPlantName"
        Else
            _Qry &= vbCrLf & " 	,COM.FTCmpNameEN AS FTCompName "
            _Qry &= vbCrLf & " 	,STYLE.FTStyleNameEN AS FTStyleName "
            _Qry &= vbCrLf & " 	,CUS.FTCustNameEN  AS FTCustName"
            _Qry &= vbCrLf & "  ,CON.FTCountryNameEN  AS FTCountryName "
            _Qry &= vbCrLf & "  ,PROD.FTProdTypeNameEN  AS FTProdTypeName"
            _Qry &= vbCrLf & "  ,MPPL.FTPlantNameEN  AS FTPlantName"
        End If
     
        _Qry &= vbCrLf & " 	,ODS.FTSubOrderNo, ORD.FNHSysCustId, CUS.FTCustCode,  ORD.FNHSysBuyId, BUY.FTBuyCode, SEA.FNHSysSeasonId, SEA.FTSeasonCode, "
        _Qry &= vbCrLf & " 		  ORD.FNHSysProdTypeId, PROD.FTProdTypeCode,  "
        _Qry &= vbCrLf & " 	  CONVERT(VARCHAR(10), CAST(ORD.FDOrderDate AS DATE), 103) AS FDOrderDate, "
        _Qry &= vbCrLf & " 	  CONVERT(VARCHAR(10), CAST(ODS.FDShipDate AS DATE), 103) AS FDShipDate, ODS.FNHSysCountryId, CON.FTCountryCode, "

        _Qry &= vbCrLf & " 	  OSBD.FTColorway, OSBD.FTSizeBreakDown, CUR.FTCurCode, "
        _Qry &= vbCrLf & " 	  ISNULL(OSBD.[FNQuantity], 0) AS FNQuantity, ISNULL(OSBD.[FNQuantityExtra], 0) AS FNQuantityExtra, OSBD.FNGrandQuantity AS FNTotalQuantity, 						  "
        _Qry &= vbCrLf & " 	  ISNULL(OSBD.[FNAmt], 0) AS FNAmt, ISNULL(OSBD.[FNAmntExtra], 0) AS FNAmntExtra, ISNULL(OSBD.FNGrandAmnt, 0) AS FNTotalAmount,"
        _Qry &= vbCrLf & " 	  0 AS FNAmountMST, 0 AS FNAmountExtraMST, 0 AS FNTotalAmountMST"
        _Qry &= vbCrLf & " 	  FROM    "
        _Qry &= vbCrLf & " 		  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle AS MST  WITH(NOLOCK)   OUTER JOIN"
        _Qry &= vbCrLf & " 		  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS ORD   WITH(NOLOCK) ON MST.FNHSysStyleId = ORD.FNHSysStyleId INNER JOIN"
        _Qry &= vbCrLf & " 		  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS ODS  WITH(NOLOCK)   ON ORD.FTOrderNo = ODS.FTOrderNo INNER JOIN"
        _Qry &= vbCrLf & " 		  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS OSBD  WITH(NOLOCK)   ON ODS.FTOrderNo = OSBD.FTOrderNo AND ODS.FTSubOrderNo = OSBD.FTSubOrderNo INNER JOIN "
        _Qry &= vbCrLf & " 		  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS COM  WITH(NOLOCK)   ON ORD.FNHSysCmpId = COM.FNHSysCmpId INNER JOIN"
        _Qry &= vbCrLf & " 		  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS STYLE  WITH(NOLOCK)   ON ORD.FNHSysStyleId = STYLE.FNHSysStyleId INNER JOIN"
        _Qry &= vbCrLf & " 		  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy AS BUY   WITH(NOLOCK)  ON ORD.FNHSysBuyId = BUY.FNHSysBuyId ON MST.FNHSysStyleId = STYLE.FNHSysStyleId LEFT OUTER JOIN"
        _Qry &= vbCrLf & " 		  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProductType AS PROD  WITH(NOLOCK)   ON ORD.FNHSysProdTypeId = PROD.FNHSysProdTypeId INNER JOIN"
        _Qry &= vbCrLf & " 		  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCountry AS CON  WITH(NOLOCK)   ON ODS.FNHSysCountryId = CON.FNHSysCountryId INNER JOIN"
        _Qry &= vbCrLf & " 		  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS CUS  WITH(NOLOCK)   ON ORD.FNHSysCustId = CUS.FNHSysCustId LEFT JOIN"
        _Qry &= vbCrLf & " 		  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS CUR  WITH(NOLOCK)  ON ODS.FNHSysCurId = CUR.FNHSysCurId LEFT JOIN "
        _Qry &= vbCrLf & " 		  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPlant AS MPPL  WITH(NOLOCK)  ON ORD.FNHSysPlantId = MPPL.FNHSysPlantId"
        _Qry &= vbCrLf & " 	 LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SEA ON "
        _Qry &= vbCrLf & " 		CASE WHEN ISNULL(ORD.FNHSysSeasonId,0) <=0 THEN ISNULL(MST.FNHSysSeasonId,0) ELSE ISNULL(ORD.FNHSysSeasonId,0)  END  = SEA.FNHSysSeasonId"


        _Qry &= vbCrLf & " 		WHERE (ORD.FNHSysCmpId = CASE WHEN " & Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString())) & " =0 THEN ORD.FNHSysCmpId ELSE " & Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString())) & " END)"
        _Qry &= vbCrLf & " 		AND ORD.FNHSysStyleId = CASE WHEN " & Integer.Parse(Val(Me.FNHSysStyleId.Properties.Tag.ToString())) & " =0 THEN ORD.FNHSysStyleId ELSE " & Integer.Parse(Val(Me.FNHSysStyleId.Properties.Tag.ToString())) & " END"
        _Qry &= vbCrLf & " 		AND (ORD.FNHSysCustId = CASE WHEN " & Integer.Parse(Val(Me.FNHSysCustId.Properties.Tag.ToString())) & " =0 THEN ORD.FNHSysCustId ELSE " & Integer.Parse(Val(Me.FNHSysCustId.Properties.Tag.ToString())) & " END)"
        _Qry &= vbCrLf & " 		AND (ORD.FNHSysBuyId = CASE WHEN " & Integer.Parse(Val(Me.FNHSysBuyId.Properties.Tag.ToString())) & " =0 THEN ORD.FNHSysBuyId ELSE " & Integer.Parse(Val(Me.FNHSysBuyId.Properties.Tag.ToString())) & " END)"
        _Qry &= vbCrLf & " 		AND (ORD.FTPORef = CASE WHEN '" & HI.UL.ULF.rpQuoted(FNHSysPOID.Text) & "' = '' THEN ORD.FTPORef ELSE '" & HI.UL.ULF.rpQuoted(FNHSysPOID.Text) & "' END)"

        If FTStartShipment.Text <> "" Then
            _Qry &= vbCrLf & "  AND ODS.FDShipDate>='" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "' "
        End If

        If FTEndShipment.Text <> "" Then
            _Qry &= vbCrLf & "  AND ODS.FDShipDate<='" & HI.UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "' "
        End If

        If FTStartOrderDate.Text <> "" Then
            _Qry &= vbCrLf & "  AND ORD.FDOrderDate>='" & HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text) & "' "
        End If

        If FTEndOrderDate.Text <> "" Then
            _Qry &= vbCrLf & "  AND ORD.FDOrderDate<='" & HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text) & "' "
        End If

        _Qry &= vbCrLf & " 	) A "
        _Qry &= vbCrLf & " 	 GROUP BY FNHSysCmpId, FTCmpCode, FTCompName, FTPORef, FNHSysStyleId, FTStyleCode, FTStyleName, FTOrderNo, "
        _Qry &= vbCrLf & " 	 FTSubOrderNo, FNHSysCustId, FTCustCode, FTCustName, FNHSysBuyId, FTBuyCode, FNHSysSeasonId, FTSeasonCode,"
        _Qry &= vbCrLf & " 	  FNHSysProdTypeId, FTProdTypeCode, FTProdTypeName, FDOrderDate, FDShipDate, FNHSysCountryId, FTCountryCode,"
        _Qry &= vbCrLf & " 	    FTCountryName, FTCurCode,FTPlantName"
        _Qry &= vbCrLf & " 	 ORDER BY FTCustCode, FTCustName, FTOrderNo, FTSubOrderNo"

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
        Me.ogcmatcode.DataSource = dt

        Dim view As GridView
        view = ogcmatcode.Views(0)
        view.OptionsView.ShowAutoFilterRow = True
        view.BestFitColumns()

        Me.ogcmatcode = view.GridControl
        Me.ogcmatcode.Refresh()

        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        If Inited = False Then
            '''InitGrid()
        End If

    End Sub

    Private Sub ocmclear1_Click(sender As System.Object, e As System.EventArgs) Handles ocmclearclsr.Click
        Me.ogcmatcode.DataSource = Nothing
        Dim xCol As Integer = 0
        Dim Idx As Integer = 0
        Try

            HI.TL.HandlerControl.ClearControl(Me)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function PROC_VALIDATEbSHOWBROWSEDATA() As Boolean
        Dim bFlagValidate As Boolean = False

        If Not bFlagValidate AndAlso Me.FNHSysCmpId.Text.Trim <> "" Then bFlagValidate = True
        If Not bFlagValidate AndAlso Me.FNHSysPOID.Text.Trim <> "" Then bFlagValidate = True
        If Not bFlagValidate AndAlso Me.FNHSysStyleId.Text.Trim <> "" Then bFlagValidate = True
        If Not bFlagValidate AndAlso Me.FNHSysCustId.Text.Trim <> "" Then bFlagValidate = True
        If Not bFlagValidate AndAlso Me.FNHSysBuyId.Text.Trim <> "" Then bFlagValidate = True

        If Not (bFlagValidate) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return bFlagValidate

    End Function

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmrefresh.Click

        If Not PROC_VALIDATEbSHOWBROWSEDATA() = True Then Exit Sub

        Dim _Str As String = ""

        ogcmatcode.DataSource = Nothing

        If FNHSysBuyId.Text <> "" Then
            _Str = "SELECT TOP 1 FNHSysBuyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy WITH(NOLOCK) WHERE FTBuyCode ='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' "
            FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
        End If
        Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, "")

    End Sub

    Private Sub FuncExcel_Click(sender As System.Object, e As System.EventArgs) Handles FuncExcel.Click
        AddHandler saveFileDialog.FileOk, AddressOf Me.saveFileDialog_FileOk
        Dim dialogResult As Object = saveFileDialog.ShowDialog
        Try
            If (dialogResult = dialogResult.OK) Then

                CType(ogcmatcode.Views(0), GridView).BestFitColumns()
                CType(ogcmatcode.Views(0), GridView).OptionsPrint.AutoWidth = False

                'Me.ogcmatcode.ExportToXlsx(selectedFile)
                Dim _options As DevExpress.XtraPrinting.XlsxExportOptions = New DevExpress.XtraPrinting.XlsxExportOptions
                _options.TextExportMode = TextExportMode.Value
                _options.ExportMode = XlsExportMode.SingleFile
                _options.ShowGridLines = True
                _options.SheetName = "OrderListingInfo"
                Me.ogcmatcode.ExportToXlsx(selectedFile, _options)


                'Dim link As DevExpress.XtraPrintingLinks.PrintableComponentLinkBase = New PrintableComponentLinkBase(New PrintingSystemBase)
                'link.PaperKind = System.Drawing.Printing.PaperKind.A4                
                'link.Component = ogcmatcode
                'link.Landscape = True                

                'Dim _Options1 As XlsxExportOptions = New XlsxExportOptions
                '_Options1.SheetName = "OrderListingInfo"
                '_Options1.ExportMode = XlsExportMode.SingleFile
                '_Options1.TextExportMode = TextExportMode.Value
                'link.ExportToXlsx(selectedFile, _Options1)

                Dim strPaths As String = System.IO.Path.GetDirectoryName(saveFileDialog.FileName)

                Dim newValue As String = strPaths

                Dim section As System.Configuration.AppSettingsSection = _
                DirectCast(AppConfig.GetSection(sectionName),  _
                    System.Configuration.AppSettingsSection)

                'AppConfig.AppSettings.CurrentConfiguration.Sections.Add(sectionName, section)

                AppConfig.AppSettings.Settings.Add(newKey, newValue)
                AppConfig.Save(ConfigurationSaveMode.Full)

                ConfigurationManager.RefreshSection(sectionName)

                If (selectedFile <> "") Then
                    Process.Start(selectedFile)
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub saveFileDialog_FileOk(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim fileDialog As SaveFileDialog = CType(sender, SaveFileDialog)
        selectedFile = fileDialog.FileName
        If (String.IsNullOrEmpty(selectedFile) OrElse selectedFile.Contains(".lnk")) Then
            e.Cancel = True
        End If
        Return
    End Sub

#End Region

#Region "Initial Grid"

    Private Sub InitGrid()
        Dim ret As Boolean = Inited
        If (Inited = True) Then
            Return
        End If
        Try
            Dim bandedView As GridView = GridView1
            bandedView.ClearGrouping()
            bandedView.ClearDocument()

            bandedView.Columns("FTCustCode").SortIndex = 0
            bandedView.Columns("FTCustCode").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            bandedView.Columns("FTPORef").SortIndex = 1
            bandedView.Columns("FTPORef").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

            ' Make the group footers always visible.
            bandedView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways
            bandedView.Columns("FTCustCode").Group()
            bandedView.Columns("FTPORef").Group()

            ' Create and setup the 1 summary item.
            Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem
            item1.FieldName = "FTCustCode"
            item1.SummaryType = DevExpress.Data.SummaryItemType.None
            item1.DisplayFormat = "{0}"
            item1.ShowInGroupColumnFooter = bandedView.Columns("FTCustCode")
            bandedView.GroupSummary.Add(item1)

            ' Create and setup the 2 summary item.
            Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem
            item2.FieldName = "FTPORef"
            item2.SummaryType = DevExpress.Data.SummaryItemType.None
            item2.DisplayFormat = "{0}"
            item2.ShowInGroupColumnFooter = bandedView.Columns("FTPORef")
            bandedView.GroupSummary.Add(item2)

            ' Create and setup the 3 summary item.
            Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem
            item3.FieldName = "FNQuantity"
            item3.SummaryType = DevExpress.Data.SummaryItemType.Sum
            item3.DisplayFormat = "{0:n0}"
            item3.ShowInGroupColumnFooter = bandedView.Columns("FNQuantity")
            bandedView.GroupSummary.Add(item3)

            ' Create and setup the 4 summary item.
            Dim item4 As GridGroupSummaryItem = New GridGroupSummaryItem
            item4.FieldName = "FNExtraQuantity"
            item4.SummaryType = DevExpress.Data.SummaryItemType.Sum
            item4.DisplayFormat = "{0:n0}"
            item4.ShowInGroupColumnFooter = bandedView.Columns("FNExtraQuantity")
            bandedView.GroupSummary.Add(item4)

            ' Create and setup the 4 summary item.
            Dim item5 As GridGroupSummaryItem = New GridGroupSummaryItem
            item5.FieldName = "FNTotalQuantity"
            item5.SummaryType = DevExpress.Data.SummaryItemType.Sum
            item5.DisplayFormat = "{0:n0}"
            item5.ShowInGroupColumnFooter = bandedView.Columns("FNTotalQuantity")
            bandedView.GroupSummary.Add(item5)


            bandedView.Columns("FTCustCode").Summary.Add(DevExpress.Data.SummaryItemType.Count, "FTCustCode")
            bandedView.Columns("FTPORef").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTPORef")
            bandedView.Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            bandedView.Columns("FNExtraQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNExtraQuantity")
            bandedView.Columns("FNTotalQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNTotalQuantity")

            bandedView.Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n0}"
            bandedView.Columns("FNExtraQuantity").SummaryItem.DisplayFormat = "{0:n0}"
            bandedView.Columns("FNTotalQuantity").SummaryItem.DisplayFormat = "{0:n0}"

            bandedView.GroupFooterShowMode = GroupFooterShowMode.VisibleIfExpanded
            bandedView.ExpandAllGroups()
            bandedView.ClearGrouping()

            bandedView.BestFitColumns()
            bandedView.RefreshData()
            Inited = True

        Catch ex As System.Exception
            Inited = False
        End Try
    End Sub

#End Region

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        With Me.GridView1
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _FTOrderNo As String = "" & .GetFocusedRowCellValue("FTOrderNo").ToString

            Dim _from As New wReportMERFactoryOrderNo(_FTOrderNo)
            HI.TL.HandlerControl.AddHandlerObj(_from)
            With _from
                Dim oSysLang As New HI.ST.SysLanguage

                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _from.Name.ToString.Trim, _from)
                Catch ex As Exception
                End Try

                Call HI.ST.Lang.SP_SETxLanguage(_from)

                .ShowDialog()
            End With

        End With
    End Sub
End Class