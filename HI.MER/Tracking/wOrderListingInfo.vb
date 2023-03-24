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

Public Class wOrderListingInfo


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
    Private _Clear As Boolean = False
    Dim FirstLoad As Boolean = True

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

#Region "MAIN PROC"

    Private Sub wOrderListingInfo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            FirstLoad = False

            FNHSysCmpId = GetPropertyTagValue(FNHSysCmpId)
            FNHSysBuyId = GetPropertyTagValue(FNHSysBuyId)
            FNHSysStyleId = GetPropertyTagValue(FNHSysStyleId)
            FNHSysCustId = GetPropertyTagValue(FNHSysCustId)
            FNHSysSeasonId = GetPropertyTagValue(FNHSysSeasonId)
            FNHSysPOID = GetPropertyTagValue(FNHSysPOID)

            View = New GridView()
            View = ogcmatcode.Views(0)
            View.FocusedRowHandle = 0

            ogcmatcode.ViewCollection.Add(View)
            ogcmatcode.MainView = View

            View.GridControl = ogcmatcode
            View.OptionsView.ShowAutoFilterRow = True
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
        'FNHSysCmpId = GetPropertyTagValue(FNHSysCmpId)
        'If FirstLoad = False Then
        '    If Me.FNHSysCmpId.Text <> "" Then
        '        Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        '        ogcmatcode.Refresh()
        '    Else
        '        ogcmatcode.DataSource = Nothing
        '    End If
        'End If

    End Sub

    Private Sub FNHSysBuyId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysBuyId.EditValueChanged
        'FNHSysBuyId = GetPropertyTagValue(FNHSysBuyId)
        'If FirstLoad = False Then
        '    If Me.FNHSysBuyId.Text <> "" Then
        '        Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        '        ogcmatcode.Refresh()
        '    Else
        '        ogcmatcode.DataSource = Nothing
        '    End If

        'End If

    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysStyleId.EditValueChanged
        'FNHSysStyleId = GetPropertyTagValue(FNHSysStyleId)
        'If FirstLoad = False Then
        '    If Me.FNHSysStyleId.Text <> "" Then
        '        Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        '        ogcmatcode.Refresh()
        '    Else
        '        ogcmatcode.DataSource = Nothing
        '    End If
        'End If

    End Sub

    Private Sub FNHSysCustId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysCustId.EditValueChanged
        'FNHSysCustId = GetPropertyTagValue(FNHSysCustId)
        'If FirstLoad = False Then
        '    If Me.FNHSysCustId.Text <> "" Then
        '        Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        '        ogcmatcode.Refresh()
        '    Else
        '        ogcmatcode.DataSource = Nothing
        '    End If
        'End If

    End Sub

    Private Sub FNHSysSeasonId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysSeasonId.EditValueChanged
        'FNHSysSeasonId = GetPropertyTagValue(FNHSysSeasonId)
        'If FirstLoad = False Then
        '    If Me.FNHSysSeasonId.Text <> "" Then
        '        Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        '        ogcmatcode.Refresh()
        '    Else
        '        ogcmatcode.DataSource = Nothing
        '    End If

        'End If

    End Sub

    Private Sub FNHSysPOID_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysPOID.EditValueChanged
        'FNHSysPOID = GetPropertyTagValue(FNHSysPOID)
        'If FirstLoad = False Then
        '    If Me.FNHSysPOID.Text <> "" Then
        '        Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        '        ogcmatcode.Refresh()
        '    Else
        '        ogcmatcode.DataSource = Nothing
        '    End If
        'End If

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

    Private Sub LoadOrderListingInfo(ByVal _FNHSysCmpId As String, ByVal _FTPORef As String, ByVal _FNHSysStyleId As String, ByVal _FNHSysCustId As String, ByVal _FNHSysBuyId As String, ByVal _FNHSysSeasonId As String)
        Dim Qry As String = ""
        Dim dt As New DataTable
        'HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        'HI.Conn.SQLConn.SqlConnectionOpen()
        'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        'If _FNHSysCmpId.Trim() = "" Then _FNHSysCmpId = "0"
        'If _FNHSysStyleId.Trim() = "" Then _FNHSysStyleId = "0"
        'If _FNHSysCustId.Trim() = "" Then _FNHSysCustId = "0"
        'If _FNHSysBuyId.Trim() = "" Then _FNHSysBuyId = "0"
        'If _FNHSysSeasonId.Trim() = "" Then _FNHSysSeasonId = "0"

        'Dim sqlCmd As New SqlCommand
        'sqlCmd.Connection = HI.Conn.SQLConn.Cnn
        'sqlCmd.CommandType = CommandType.StoredProcedure
        'sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_ORDER_LISTING_INFO]"

        'REM by Tid 2014/03/19
        ''sqlCmd.Parameters.AddWithValue("@FNHSysCmpId", _FNHSysCmpId)
        ''sqlCmd.Parameters.AddWithValue("@FTPORef", _FTPORef)
        ''sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", _FNHSysStyleId)
        ''sqlCmd.Parameters.AddWithValue("@FNHSysCustId", _FNHSysCustId)
        ''sqlCmd.Parameters.AddWithValue("@FNHSysBuyId", _FNHSysBuyId)
        ''sqlCmd.Parameters.AddWithValue("@FNHSysSeasonId", _FNHSysSeasonId)

        'sqlCmd.Parameters.AddWithValue("@FNHSysCmpId", Val(_FNHSysCmpId))
        'sqlCmd.Parameters.AddWithValue("@FTPORef", _FTPORef)
        'sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", Val(_FNHSysStyleId))
        'sqlCmd.Parameters.AddWithValue("@FNHSysCustId", Val(_FNHSysCustId))
        'sqlCmd.Parameters.AddWithValue("@FNHSysBuyId", Val(_FNHSysBuyId))
        'sqlCmd.Parameters.AddWithValue("@FNHSysSeasonId", Val(_FNHSysSeasonId))

        'sqlCmd.Parameters.AddWithValue("'" & HI.ST.Lang.Language.ToString & "'", HI.ST.Lang.Language.ToString())

        'Dim sqlDA As New SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
        'sqlDA.SelectCommand = sqlCmd

        'sqlDA.Fill(dt)
        If Not (_Clear) Then
            Dim _Spls As New HI.TL.SplashScreen("Loading...data please wait")
            Try
                'Qry = "exec[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..[SP_ORDER_LISTING_INFO] " & Val(_FNHSysCmpId.ToString) & " "
                'Qry &= vbCrLf & ",'" & _FTPORef.ToString & "'," & Val(_FNHSysStyleId.ToString) & "," & Val(_FNHSysCustId.ToString) & "," & Val(_FNHSysBuyId.ToString) & "," & Val(_FNHSysSeasonId.ToString) & ""
                'Qry &= vbCrLf & ",'" & HI.ST.Lang.Language.ToString & "'"



                Qry = "   Select  distinct    ORD.FNHSysCmpId, COM.FTCmpCode, Case When '" & HI.ST.Lang.Language.ToString & "' = 'TH' THEN COM.FTCmpNameTH ELSE COM.FTCmpNameEN END AS FTCompName,ORD.FTRemark "
                Qry &= vbCrLf & ",V.FTPORef AS FTPORef"
                Qry &= vbCrLf & ", ORD.FNHSysStyleId, "
                Qry &= vbCrLf & "  STYLE.FTStyleCode, CASE WHEN '" & HI.ST.Lang.Language.ToString & "' = 'TH' THEN STYLE.FTStyleNameTH ELSE STYLE.FTStyleNameEN END AS FTStyleName, ORD.FTOrderNo, ODS.FTSubOrderNo, ORD.FNHSysCustId, "
                Qry &= vbCrLf & "  CUS.FTCustCode, Case When '" & HI.ST.Lang.Language.ToString & "' = 'TH' THEN CUS.FTCustNameTH ELSE CUS.FTCustNameEN END AS FTCustName, ORD.FNHSysBuyId, BUY.FTBuyCode, SEA.FNHSysSeasonId, "
                Qry &= vbCrLf & "  SEA.FTSeasonCode, ORD.FNHSysProdTypeId, PROD.FTProdTypeCode, CASE WHEN '" & HI.ST.Lang.Language.ToString & "' = 'TH' THEN PROD.FTProdTypeNameTH ELSE PROD.FTProdTypeNameEN END AS FTProdTypeName, "
                Qry &= vbCrLf & "  Convert(VARCHAR(10), CAST(ORD.FDOrderDate As Date), 103) As FDOrderDate, CONVERT(VARCHAR(10), CAST(V.FDShipDate As Date), 103) As FDShipDate, ODS.FNHSysCountryId, "
                Qry &= vbCrLf & "  CON.FTCountryCode, Case When '" & HI.ST.Lang.Language.ToString & "' = 'TH' THEN CON.FTCountryNameTH ELSE CON.FTCountryNameEN END AS FTCountryName, OSB.FTColorway, OSB.FTSizeBreakDown, "
                Qry &= vbCrLf & "  ISNULL(OSB.FNQuantity, 0) As FNQuantity, ISNULL(OSB.FNQuantityExtra, 0) As FNExtraQuantity, ISNULL(OSB.FNGarmentQtyTest, 0) As FNGarmentQtyTest,ISNULL(OSB.FNExternalQtyTest, 0) As FNExternalQtyTest, (ISNULL(OSB.FNQuantity, 0) + ISNULL(OSB.FNQuantityExtra, 0) + ISNULL(OSB.FNGarmentQtyTest, 0)+ISNULL(OSB.FNExternalQtyTest, 0)) As FNTotalQuantity, "
                Qry &= vbCrLf & "  ODS.FTStateEmb, ODS.FTStatePrint, ODS.FTStateHeat, ODS.FTStateLaser, ODS.FTStateWindows,ISNULL(ODS.FTStateSewOnly,'0') As FTStateSewOnly"

                Qry &= vbCrLf & " ,V.FTNikePOLineItem  "
                Qry &= vbCrLf & " ,MT.FTMerTeamCode  "
                Qry &= vbCrLf & " ,ISNULL(ORD.FTUpdUser ,ISNULL(ORD.FTInsUser,'')) AS UserName "

                Qry &= vbCrLf & " ,ISNULL(JTT.FNJobStateName ,'') AS FNJobStateName "

                Qry &= vbCrLf & " ,ISNULL(ORD.FDOrderDate,'') AS FTDocumentDate"
                Qry &= vbCrLf & "   ,CASE WHEN LEN((CASE WHEN ODS.FTStatePrint ='1' THEN 'P-' ELSE ''END + "
                Qry &= vbCrLf & " Case WHEN ODS.FTStateEmb ='1' THEN 'E-' ELSE ''END   +"
                Qry &= vbCrLf & " Case When ODS.FTStateHeat ='1' THEN 'H-' ELSE ''END +"
                Qry &= vbCrLf & " Case WHEN ODS.FTStateLaser ='1' THEN 'L-' ELSE ''END  +"
                Qry &= vbCrLf & " Case When ODS.FTStateWindows ='1' THEN 'PP-' ELSE ''END )) > 1 "
                Qry &= vbCrLf & " THEN "
                Qry &= vbCrLf & " Left(((Case When ODS.FTStatePrint ='1' THEN 'P-' ELSE ''END +"
                Qry &= vbCrLf & " Case WHEN ODS.FTStateEmb ='1' THEN 'E-' ELSE ''END   +"
                Qry &= vbCrLf & " Case When ODS.FTStateHeat ='1' THEN 'H-' ELSE ''END +"
                Qry &= vbCrLf & " Case WHEN ODS.FTStateLaser ='1' THEN 'L-' ELSE ''END  +"
                Qry &= vbCrLf & " Case When ODS.FTStateWindows ='1' THEN 'PP-' ELSE ''END )),LEN((CASE WHEN ODS.FTStatePrint ='1' THEN 'P-' ELSE ''END +"
                Qry &= vbCrLf & " Case WHEN ODS.FTStateEmb ='1' THEN 'E-' ELSE ''END   +"
                Qry &= vbCrLf & " Case When ODS.FTStateHeat ='1' THEN 'H-' ELSE ''END +"
                Qry &= vbCrLf & " Case WHEN ODS.FTStateLaser ='1' THEN 'L-' ELSE ''END  +"
                Qry &= vbCrLf & " Case When ODS.FTStateWindows ='1' THEN 'PP-' ELSE ''END ))-1)"
                Qry &= vbCrLf & " Else"
                Qry &= vbCrLf & "  ''"
                Qry &= vbCrLf & "   End"
                Qry &= vbCrLf & " AS PROCESSROUTENAME"

                Qry &= vbCrLf & " ,MST.FTStyleNameEN AS FTStyleName,ISNULL(ORD.FTStateOrderApp,'0') AS FTStateOrderApp,ORD.FTAppBy,ORD.FDAppDate,ORD.FTStateBy,ORD.FDStateDate"


                Qry &= vbCrLf & " ,X21.FTOrderTypeName"

                Qry &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder As ORD With(NOLOCK)  INNER Join"
                Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub As ODS With(NOLOCK)  On ODS.FTOrderNo = ORD.FTOrderNo INNER Join"
                Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown As OSB With(NOLOCK)  On OSB.FTOrderNo = ODS.FTOrderNo And OSB.FTSubOrderNo = ODS.FTSubOrderNo INNER Join"
                Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination As V On OSB.FTOrderNo=V.FTOrderNo And OSB.FTSubOrderNo=V.FTSubOrderNo And OSB.FTColorway=V.FTColorway And OSB.FTSizeBreakDown=V.FTSizeBreakDown INNER JOIN"
                Qry &= vbCrLf & "	  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As STYLE With(NOLOCK) On ORD.FNHSysStyleId = STYLE.FNHSysStyleId INNER JOIN"
                Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As MST With(NOLOCK)  On MST.FNHSysStyleId = STYLE.FNHSysStyleId LEFT OUTER JOIN"
                Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason As SEA With(NOLOCK) On ORD.FNHSysSeasonId = SEA.FNHSysSeasonId LEFT OUTER JOIN"
                Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp As COM With(NOLOCK)  On ORD.FNHSysCmpId = COM.FNHSysCmpId INNER JOIN"
                Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy As BUY With(NOLOCK) On ORD.FNHSysBuyId = BUY.FNHSysBuyId INNER JOIN"
                Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMProductType As PROD With(NOLOCK) On ORD.FNHSysProdTypeId = PROD.FNHSysProdTypeId INNER JOIN"
                Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCountry As CON With(NOLOCK) On ODS.FNHSysCountryId = CON.FNHSysCountryId INNER JOIN"
                Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer As CUS With(NOLOCK) On ORD.FNHSysCustId = CUS.FNHSysCustId"
                Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMerTeam As MT With (NOLOCK)  On ORD.FNHSysMerTeamId = MT.FNHSysMerTeamId "


                Qry &= vbCrLf & "  LEFT OUTER JOIN  ( Select  FNListIndex, FTNameEN AS FNJobStateName From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)  Where (FTListName = N'FNJobState') ) AS JTT ON ORD.FNJobState = JTT.FNListIndex "

                Qry &= vbCrLf & "  OUTER APPLY (SELECT TOP 1 FTNameEN AS FTOrderTypeName  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS X21 WITH(NOLOCK) WHERE        (X21.FTListName = N'FNOrderType') AND X21.FNListIndex = ORD.FNOrderType  ) AS X21 "


                Qry &= vbCrLf & "WHERE   ORD.FTOrderNo <> '' "



                If FTStartOrderDate.Text <> "" Then
                    Qry &= vbCrLf & "  AND   ORD.FDOrderDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text) & "'"
                End If

                If FTEndOrderDate.Text <> "" Then
                    Qry &= vbCrLf & "  AND   ORD.FDOrderDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text) & "'"
                End If


                If FTStartShipment.Text <> "" Then
                    Qry &= vbCrLf & "  AND   V.FDShipDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "'"
                End If


                If FTEndShipment.Text <> "" Then
                    Qry &= vbCrLf & "  AND   V.FDShipDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "'"
                End If

                If Val(_FNHSysCmpId.ToString) > 0 Then
                    Qry &= vbCrLf & "  AND   ORD.FNHSysCmpId=" & Val(_FNHSysCmpId.ToString) & " "
                End If

                If Val(_FNHSysStyleId.ToString) > 0 Then
                    Qry &= vbCrLf & "  AND   ORD.FNHSysStyleId=" & Val(_FNHSysStyleId.ToString) & " "
                End If

                If Val(_FNHSysCustId.ToString) > 0 Then
                    Qry &= vbCrLf & "  AND   ORD.FNHSysCustId=" & Val(_FNHSysCustId.ToString) & " "
                End If

                If Val(_FNHSysBuyId.ToString) > 0 Then
                    Qry &= vbCrLf & "  AND   ORD.FNHSysBuyId=" & Val(_FNHSysBuyId.ToString) & " "
                End If

                If Val(_FNHSysSeasonId.ToString) > 0 Then
                    Qry &= vbCrLf & "  AND   ORD.FNHSysSeasonId =" & Val(_FNHSysSeasonId.ToString) & " "
                End If

                If _FTPORef <> "" Then
                    Qry &= vbCrLf & "  AND   V.FTPORef ='" & HI.UL.ULF.rpQuoted(_FTPORef) & "' "
                End If

                Qry &= vbCrLf & " ORDER BY ORD.FTOrderNo, ODS.FTSubOrderNo "

                dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Catch ex As Exception

            End Try


            _Spls.Close()
            Me.ogcmatcode.DataSource = dt
        End If



        'Dim view As GridView
        'view = ogcmatcode.Views(0)
        'view.OptionsView.ShowAutoFilterRow = True
        'view.BestFitColumns()

        'Me.ogcmatcode = view.GridControl
        'Me.ogcmatcode.Refresh()

        'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        If Inited = False Then
            InitGrid()
        End If

    End Sub

    Private Sub ocmclear1_Click(sender As System.Object, e As System.EventArgs) Handles ocmclearclsr.Click
        Me.ogcmatcode.DataSource = Nothing
        Dim xCol As Integer = 0
        Dim Idx As Integer = 0
        Try
            _Clear = True
            HI.TL.HandlerControl.ClearControl(Me)


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        _Clear = False
    End Sub

    Private Function PROC_VALIDATEbSHOWBROWSEDATA() As Boolean
        'Dim bFlagValidate As Boolean = False

        'If Not bFlagValidate AndAlso Me.FNHSysCmpId.Text.Trim <> "" Then bFlagValidate = True
        'If Not bFlagValidate AndAlso Me.FNHSysPOID.Text.Trim <> "" Then bFlagValidate = True
        'If Not bFlagValidate AndAlso Me.FNHSysStyleId.Text.Trim <> "" Then bFlagValidate = True
        'If Not bFlagValidate AndAlso Me.FNHSysCustId.Text.Trim <> "" Then bFlagValidate = True
        'If Not bFlagValidate AndAlso Me.FNHSysBuyId.Text.Trim <> "" Then bFlagValidate = True
        'If Not bFlagValidate AndAlso Me.FNHSysSeasonId.Text.Trim <> "" Then bFlagValidate = True


        'If Not (bFlagValidate) Then
        '    HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        'End If

        'Return bFlagValidate



        If Me.FNHSysCmpId.Text = "" And Me.FNHSysPOID.Text = "" And FNHSysStyleId.Text = "" And FNHSysCustId.Text = "" And FNHSysBuyId.Text = "" And FNHSysSeasonId.Text = "" And FTStartOrderDate.Text = "" And FTEndOrderDate.Text = "" And FTStartShipment.Text = "" And FTEndShipment.Text = "" Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmrefresh.Click

        If Not PROC_VALIDATEbSHOWBROWSEDATA() = True Then Exit Sub

        Dim _Str As String = ""

        ogcmatcode.DataSource = Nothing

        If FNHSysBuyId.Text <> "" Then
            _Str = "Select TOP 1 FNHSysBuyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy With(NOLOCK) WHERE FTBuyCode ='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' "
            FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
        End If

        Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)

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

            '...Quaytity Garment Test
            Dim item6 As GridGroupSummaryItem = New GridGroupSummaryItem
            item6.FieldName = "FNGarmentQtyTest"
            item6.SummaryType = DevExpress.Data.SummaryItemType.Sum
            item6.DisplayFormat = "{0:n0}"
            item6.ShowInGroupColumnFooter = bandedView.Columns("FNGarmentQtyTest")
            bandedView.GroupSummary.Add(item6)

            ' Create and setup the 4 summary item.
            Dim item5 As GridGroupSummaryItem = New GridGroupSummaryItem
            item5.FieldName = "FNTotalQuantity"
            item5.SummaryType = DevExpress.Data.SummaryItemType.Sum
            item5.DisplayFormat = "{0:n0}"
            item5.ShowInGroupColumnFooter = bandedView.Columns("FNTotalQuantity")
            bandedView.GroupSummary.Add(item5)


            bandedView.Columns("FTCustCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTCustCode")
            bandedView.Columns("FTPORef").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTPORef")
            bandedView.Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            bandedView.Columns("FNExtraQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNExtraQuantity")
            bandedView.Columns("FNGarmentQtyTest").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNGarmentQtyTest")
            bandedView.Columns("FNTotalQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNTotalQuantity")

            bandedView.Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n0}"
            bandedView.Columns("FNExtraQuantity").SummaryItem.DisplayFormat = "{0:n0}"
            bandedView.Columns("FNGarmentQtyTest").SummaryItem.DisplayFormat = "{0:n0}"
            bandedView.Columns("FNTotalQuantity").SummaryItem.DisplayFormat = "{0:n0}"

            bandedView.GroupFooterShowMode = GroupFooterShowMode.VisibleIfExpanded
            bandedView.ExpandAllGroups()
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

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click

    End Sub
End Class