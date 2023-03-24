Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPrinting
Imports System.Configuration
Imports System.Data.Common

Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls
Imports System.Text
Imports System.Net
Imports Microsoft.Win32
Imports Newtonsoft.Json
Imports System.IO


Public Class wPostXMLJSon


    Dim View As GridView
    Dim RowsIndex As Double
    Dim TopVisibleIndex As Int32
    Private sFNHSysStyleId As String

    ''' Used Data Adapter to control database

    Dim oleDbDataAdapter1 As DbDataAdapter
    Dim oleDbDataAdapter2 As DbDataAdapter
    Dim dtStyleDetail As System.Data.DataTable



    Dim FirstLoad As Boolean = True
    Private Inited As Boolean
    Dim RetMessage As String = ""

#Region "Handler Control"
    Private _DefailtPath As String
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
        _DefailtPath = ""

        Try
            _DefailtPath = ReadRegistry()
        Catch ex As Exception
        End Try

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

    Private Sub wOrderListingInfo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


    End Sub

    Private Sub Proc_Close(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub FNHSysCmpId_EditValueChanged(sender As System.Object, e As System.EventArgs)
        'FNHSysCmpId = GetPropertyTagValue(FNHSysCmpId)
        'If FirstLoad = False Then
        '    Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        '    ogcmatcode.Refresh()
        'End If
    End Sub

    Private Sub FNHSysBuyId_EditValueChanged(sender As System.Object, e As System.EventArgs)
        'FNHSysBuyId = GetPropertyTagValue(FNHSysBuyId)
        'If FirstLoad = False Then
        '    Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        '    ogcmatcode.Refresh()
        'End If
    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(sender As System.Object, e As System.EventArgs)
        'FNHSysStyleId = GetPropertyTagValue(FNHSysStyleId)
        'If FirstLoad = False Then
        '    Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        '    ogcmatcode.Refresh()
        'End If
    End Sub

    Private Sub FNHSysCustId_EditValueChanged(sender As System.Object, e As System.EventArgs)
        'FNHSysCustId = GetPropertyTagValue(FNHSysCustId)
        'If FirstLoad = False Then
        '    Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        '    ogcmatcode.Refresh()
        'End If
    End Sub

    Private Sub FNHSysSeasonId_EditValueChanged(sender As System.Object, e As System.EventArgs)
        'FNHSysSeasonId = GetPropertyTagValue(FNHSysSeasonId)
        'If FirstLoad = False Then
        '    Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        '    ogcmatcode.Refresh()
        'End If
    End Sub

    Private Sub FNHSysPOID_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysPOID.EditValueChanged
        'FNHSysPOID = GetPropertyTagValue(FNHSysPOID)
        'If FirstLoad = False Then
        '    Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        '    ogcmatcode.Refresh()
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
                    Select Case CType(obj, System.Windows.Forms.Control).Name.ToUpper
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

    Private Sub LoadListingDataInfo(ByVal _FNHSysCmpId As String, ByVal _FTPORef As String, ByVal _FNHSysStyleId As String, ByVal _FNHSysCustId As String, ByVal _FNHSysBuyId As String, ByVal _FNHSysSeasonId As String)
        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Try

            If _FNHSysCmpId.Trim() = "" Then _FNHSysCmpId = "0"
            If _FNHSysStyleId.Trim() = "" Then _FNHSysStyleId = "0"
            If _FNHSysCustId.Trim() = "" Then _FNHSysCustId = "0"
            If _FNHSysBuyId.Trim() = "" Then _FNHSysBuyId = "0"
            If _FNHSysSeasonId.Trim() = "" Then _FNHSysSeasonId = "0"

            Dim dt As New System.Data.DataTable
            Dim cmdstring As String = ""

            cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].[SP_EXPORT_LISTING_FACCMINV_TRACKING] " & Val(_FNHSysCmpId) & ",'" & HI.UL.ULF.rpQuoted(_FTPORef) & "'," & Val(_FNHSysStyleId) & "," & Val(_FNHSysCustId) & "," & Val(_FNHSysBuyId) & "," & Val(_FNHSysSeasonId) & ",'" & HI.UL.ULF.rpQuoted(HI.ST.Lang.Language.ToString()) & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_ACCOUNT)

            Me.ogcdirector.DataSource = dt

            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmclear1_Click(sender As System.Object, e As System.EventArgs) Handles ocmclearclsr.Click

        ogcdirector.DataSource = Nothing
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

        If Not bFlagValidate AndAlso Me.FTInvoiceExportNo.Text.Trim <> "" Then bFlagValidate = True
        If Not bFlagValidate AndAlso Me.FNHSysPOID.Text.Trim <> "" Then bFlagValidate = True
        If Not bFlagValidate AndAlso Me.FTSDate.Text.Trim <> "" Then bFlagValidate = True
        If Not bFlagValidate AndAlso Me.FTEDate.Text.Trim <> "" Then bFlagValidate = True

        If Not (bFlagValidate) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return bFlagValidate

    End Function

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmrefresh.Click
        If Not PROC_VALIDATEbSHOWBROWSEDATA() = True Then Exit Sub
        Call RefreshGrid()
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub



    Private Sub RefreshGrid()

        Dim _Spls As New HI.TL.SplashScreen("Loading....data,Please wait.")

        Try
            ogcdirector.DataSource = Nothing


            Dim Cmdstring As String = ""



            Cmdstring = " Declare @Tabinvoice as Table( "
            Cmdstring &= vbCrLf & "        FTInvoiceExportNo nvarchar(30) Not null "
            Cmdstring &= vbCrLf & "  	,FDInvoiceExportDate  nvarchar(30) Not null "
            Cmdstring &= vbCrLf & "  ,MCINo  nvarchar(30) Not null"
            Cmdstring &= vbCrLf & "  ,CustomerPoNo  nvarchar(30) Not null"
            Cmdstring &= vbCrLf & "  ,MIQty int"
            Cmdstring &= vbCrLf & "  ,XMLQty int"
            Cmdstring &= vbCrLf & "  ,StateXML  nvarchar(1) "
            Cmdstring &= vbCrLf & "  ,StateXMLBy  nvarchar(50) "
            Cmdstring &= vbCrLf & "  ,StateXMLDate  nvarchar(10) "
            Cmdstring &= vbCrLf & "  ,StateXMLTime  nvarchar(10) "
            Cmdstring &= vbCrLf & "   )"
            Cmdstring &= vbCrLf & "   insert into @Tabinvoice (FTInvoiceExportNo,FDInvoiceExportDate,MCINo,CustomerPoNo,MIQty,XMLQty,StateXML,StateXMLBy,StateXMLDate,StateXMLTime)"
            Cmdstring &= vbCrLf & "   Select  A.FTInvoiceExportNo"
            Cmdstring &= vbCrLf & "  	, A.FDInvoiceExportDate"
            Cmdstring &= vbCrLf & "  	, A.FTInvoiceNo"
            Cmdstring &= vbCrLf & "  , A.FTCustomerPO"
            Cmdstring &= vbCrLf & "  ,ISNULL(X1.MCIQuantity,0) As MCIQuantity"
            Cmdstring &= vbCrLf & "  	,ISNULL(X2.XNLQuantity,0) AS XNLQuantity"

            Cmdstring &= vbCrLf & " , CASE WHEN ISNULL(B.FTPostUser,'') <>'' THEN '1' ELSE '0' END AS StateXML "
            Cmdstring &= vbCrLf & " ,  ISNULL(B.FTPostUser,'')  AS StateXMLBy "
            Cmdstring &= vbCrLf & " ,  ISNULL(B.FDPostDate,'')  AS StateXMLDate "
            Cmdstring &= vbCrLf & " ,  ISNULL(B.FTPostTime,'')  AS StateXMLTime "

            Cmdstring &= vbCrLf & "   FROM            " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TACCTFactoryCMInvoice As A With(NOLOCK) LEFT OUTER JOIN"
            Cmdstring &= vbCrLf & "                   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TACCTXMLCreateInvoice As B With(NOLOCK) On A.FTCustomerPO = B.FTCustomerPO And A.FTInvoiceNo = B.FTInvoiceNo"
            Cmdstring &= vbCrLf & "   outer apply(Select sum(X.FNQuantity) As MCIQuantity,MAX( ISNULL(Z123.FTStateXML,'')) AS FTStateXML"
            Cmdstring &= vbCrLf & "   from   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TACCTFactoryCMInvoice_D As X With(nolock)"


            Cmdstring &= vbCrLf & "   outer apply(Select TOP 1  CASE WHEN Z123.FTCusStateFirstSaleXML='1' AND Z123.FTStateFirstSaleXML = '1' AND Z123.FTBuyStateFirstSaleXML = '1' THEN '1' ELSE '0' END AS FTStateXML"
            Cmdstring &= vbCrLf & "  FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERTOrderSub_BreakDown_ShipDestination AS  Z123 "
            Cmdstring &= vbCrLf & " WHERE Z123.FTPOref = X.FTCustomerPO "
            'Cmdstring &= vbCrLf & " AND Z123.FTColorway = X.FTColorway "
            'Cmdstring &= vbCrLf & " AND Z123.FTSizeBreakDown = X.FTSizeBreakDown "
            'Cmdstring &= vbCrLf & " AND Z123.FTNikePOLineItem = X.FTPOLineItem "

            'Cmdstring &= vbCrLf & " AND Z123.FTCusStateFirstSaleXML ='1' "
            'Cmdstring &= vbCrLf & " AND Z123.FTStateFirstSaleXML = '1' "
            'Cmdstring &= vbCrLf & " AND Z123.FTBuyStateFirstSaleXML = '1' "

            Cmdstring &= vbCrLf & "   ) AS Z123"
            Cmdstring &= vbCrLf & "    where x.FTInvoiceNo = a.FTInvoiceNo"
            Cmdstring &= vbCrLf & "  	And x.FTCustomerPO = a.FTCustomerPO "
            Cmdstring &= vbCrLf & "   ) As X1"
            Cmdstring &= vbCrLf & "   outer apply(Select sum(FNQuantity) As XNLQuantity"
            Cmdstring &= vbCrLf & "   from   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TACCTXMLCreateInvoice_D As X With(nolock)"
            Cmdstring &= vbCrLf & "   where x.FTInvoiceNo = a.FTInvoiceNo"
            Cmdstring &= vbCrLf & " And x.FTCustomerPO = a.FTCustomerPO "
            Cmdstring &= vbCrLf & "   	 ) As X2	"
            Cmdstring &= vbCrLf & "   WHERE ISNULL(A.FTInvoiceExportNo,'') <>''	"
            Cmdstring &= vbCrLf & "   AND ISNULL(X1.FTStateXML,'') ='1'	"

            If FNHSysPOID.Text <> "" Then
                Cmdstring &= vbCrLf & "   AND A.FTCustomerPO='" & HI.UL.ULF.rpQuoted(FNHSysPOID.Text) & "'	"
            End If

            If FTInvoiceExportNo.Text <> "" Then
                Cmdstring &= vbCrLf & "   AND A.FTInvoiceExportNo='" & HI.UL.ULF.rpQuoted(FTInvoiceExportNo.Text) & "'	"
            End If

            If FTSDate.Text <> "" Then
                Cmdstring &= vbCrLf & "   AND A.FDInvoiceExportDate >='" & HI.UL.ULDate.ConvertEnDB(FTSDate.Text) & "'	"
            End If

            If FTEDate.Text <> "" Then
                Cmdstring &= vbCrLf & "   AND A.FDInvoiceExportDate<='" & HI.UL.ULDate.ConvertEnDB(FTEDate.Text) & "'	"
            End If


            Cmdstring &= vbCrLf & "   Select  '0' AS FTSelect ,A.FTInvoiceExportNo "
            Cmdstring &= vbCrLf & "   ,Min(A.FDInvoiceExportDate) AS FDInvoiceExportDate"
            Cmdstring &= vbCrLf & "   ,MAX(ISNULL(MCI.MCINo,'')) AS MCINo"
            Cmdstring &= vbCrLf & "    ,MAX(ISNULL(POX.CustomerPoNo,'')) AS CustomerPoNo"
            Cmdstring &= vbCrLf & "   ,Sum(A.MIQty) As MIQty"
            Cmdstring &= vbCrLf & "   ,SUM(A.XMLQty) As XMLQty"
            Cmdstring &= vbCrLf & "   ,Sum(Case When A.XMLQty >A.MIQty Then 0 Else  A.MIQty- A.XMLQty End) As DiffQty "

            Cmdstring &= vbCrLf & ", MIN(StateXML) AS StateXML"
            Cmdstring &= vbCrLf & ", MAX(StateXMLBy) AS StateXMLBy"
            Cmdstring &= vbCrLf & ", MAX(StateXMLDate) AS StateXMLDate"
            Cmdstring &= vbCrLf & " ,MAX(StateXMLTime) AS StateXMLTime"

            Cmdstring &= vbCrLf & "   from @Tabinvoice As A"
            Cmdstring &= vbCrLf & "  	OUTER APPLY(Select  STUFF((Select  ',' + MCINo  "
            Cmdstring &= vbCrLf & "  From ( SELECT DISTINCT MCINo  "
            Cmdstring &= vbCrLf & "  	FROM  @Tabinvoice As  X "
            Cmdstring &= vbCrLf & "  	 WHERE   X.FTInvoiceExportNo=A.FTInvoiceExportNo "
            Cmdstring &= vbCrLf & "  	 ) As T "
            Cmdstring &= vbCrLf & "  	For XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'')  AS MCINo ) AS MCI"

            Cmdstring &= vbCrLf & "  	OUTER APPLY(Select  STUFF((Select  ',' + CustomerPoNo  "
            Cmdstring &= vbCrLf & "  	From ( SELECT DISTINCT CustomerPoNo  "
            Cmdstring &= vbCrLf & "  	FROM  @Tabinvoice As  X "
            Cmdstring &= vbCrLf & "   WHERE   X.FTInvoiceExportNo=A.FTInvoiceExportNo "
            Cmdstring &= vbCrLf & "  	 ) As T "
            Cmdstring &= vbCrLf & "  For XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'')  AS CustomerPoNo ) AS POX"
            Cmdstring &= vbCrLf & "   group by A.FTInvoiceExportNo"
            Cmdstring &= vbCrLf & "   order by A.FTInvoiceExportNo"


            Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(Cmdstring, Conn.DB.DataBaseName.DB_ACCOUNT)
            ogcdirector.DataSource = dt.Copy

        Catch ex As Exception
        End Try

        _Spls.Close()

    End Sub

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

#Region "Initial Grid"



#End Region

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs)

        'Try

        '    Dim _Fm As String = ""
        '    _Fm = "{TACCTFactoryCMInvoice.FTCustomerPO}='" & HI.UL.ULF.rpQuoted("" & Me.ogvdirector.GetFocusedRowCellValue("FTCustomerPO").ToString) & "' "
        '    _Fm &= " And {TACCTFactoryCMInvoice.FTInvoiceNo}='" & HI.UL.ULF.rpQuoted("" & Me.ogvdirector.GetFocusedRowCellValue("FTInvoiceNo").ToString) & "' "

        '    With New HI.RP.Report
        '        .FormTitle = Me.Text
        '        .ReportFolderName = "Account\"
        '        .ReportName = "ReportInvoiceCm.rpt"
        '        .Formular = _Fm
        '        .Preview()
        '    End With

        'Catch ex As Exception
        'End Try

    End Sub



    Private Sub ReposSelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ReposSelect.EditValueChanging
        Try
            Select Case e.NewValue.ToString
                Case "1"

                    If Val(Me.ogvdirector.GetFocusedRowCellValue("DiffQty").ToString) > 0 Then
                        e.Cancel = True
                    Else
                        e.Cancel = False
                    End If

            End Select
        Catch ex As Exception
        End Try

    End Sub

    Private Function SendJSONXML(EFSData As EFSJSON, invoiceno As String) As Boolean
        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)

        Dim PageCount As Integer = 0

        'Start 2020-08-27 - Set New Secret For Go Live
        'Dim OktaurlEndPoint As String = "https://nike-qa.oktapreview.com/oauth2/ausa0mcornpZLi0C40h7/v1/token" 'test
        ' Dim OktaurlEndPoint As String = "https://nike.okta.com/oauth2/aus27z7p76as9Dz0H1t7/v1/token" 'Production

        ' Dim EFSurlEndPoint As String = "https://api.gflstnc.non.thecommons.nike.com/efs/v1/upload" 'test
        'Dim EFSurlEndPoint As String = "https://api.gflstnc.prd.thecommons.nike.com/efs/v1/upload" 'Production


        Dim OktaurlEndPoint As String = "https://nike.okta.com/oauth2/aus27z7p76as9Dz0H1t7/v1/token" 'Production
        Dim EFSurlEndPoint As String = "https://api.gflstnc.prd.thecommons.nike.com/efs/v1/upload" 'Production
        'Start 2020-08-27 - Set New Secret For Go Live

        ' Refer to the documentation for more information on how to get the client id/secret
        'Dim clientid As String = "niketrade.efs.hit"
        'Dim clientsecret As String = "WI4FxjQvBFXqdJfawye9Y28SlIeTd1JnWvtxzNKbjySMN21SFd5G5mQzmMipfU15"


        'Start 2020-08-27 - Set New Secret For Go Live
        'Dim clientid As String = "niketrade.efs.hit"
        'Dim clientsecret As String = "WI4FxjQvBFXqdJfawye9Y28SlIeTd1JnWvtxzNKbjySMN21SFd5G5mQzmMipfU15"



        Dim clientid As String = "niketrade.efs.hit"
        Dim clientsecret As String = "pa2pD_kv5OjQvc7_ZPgANZqB6SQ-jXuH-6BOCdo0gSuRDawZ0Pm9dWQPKsI7aOJt"
        'END 2020-08-27 - Set New Secret For Go Live

        Dim granttype As String = "client_credentials"
        ' Refer to the documentation for more information on how to get the tokens
        Dim accessToken As String = ""

        Dim EFSjson_data As String = ""
        ' -- Refresh the access token
        Dim request As System.Net.WebRequest = System.Net.HttpWebRequest.Create(OktaurlEndPoint)
        request.UseDefaultCredentials = True
        request.PreAuthenticate = True
        request.Credentials = CredentialCache.DefaultCredentials

        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"


        Dim json_data As String = String.Format("client_id={0}&client_secret={1}&scope=trade.efs.create&grant_type=client_credentials", System.Web.HttpUtility.UrlEncode(clientid), System.Web.HttpUtility.UrlEncode(clientsecret))

        Dim postBytes As Byte() = System.Text.Encoding.ASCII.GetBytes(json_data)

        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
        Dim postStream As Stream = request.GetRequestStream()
        postStream.Write(postBytes, 0, postBytes.Length)
        postStream.Flush()
        postStream.Close()

        Dim StateAppcept As Boolean = False

        Try
            ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
            Using response As System.Net.WebResponse = request.GetResponse()
                Using streamReader As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
                    ' Parse the JSON the way you prefer
                    Dim jsonResponseText As String = streamReader.ReadToEnd()
                    Dim jsonResult As RefreshTokenResultJSON = JsonConvert.DeserializeObject(Of RefreshTokenResultJSON)(jsonResponseText)
                    accessToken = jsonResult.access_token

                    If accessToken <> "" Then

                        EFSjson_data = JsonConvert.SerializeObject(EFSData)
                        Dim EFSpostBytes As Byte() = System.Text.Encoding.ASCII.GetBytes(EFSjson_data)

                        Dim requestpost As System.Net.WebRequest = System.Net.HttpWebRequest.Create(EFSurlEndPoint)
                        requestpost.UseDefaultCredentials = True
                        requestpost.PreAuthenticate = True
                        requestpost.Credentials = CredentialCache.DefaultCredentials
                        requestpost.Method = "POST"
                        requestpost.ContentType = "application/json"
                        ' requestpost.Headers.Add("x-api-key", xapikey)
                        requestpost.Headers.Add("Authorization", "Bearer " & accessToken)

                        Dim postStreamdata As Stream = requestpost.GetRequestStream()

                        postStreamdata.Write(EFSpostBytes, 0, EFSpostBytes.Length)
                        postStreamdata.Flush()
                        postStreamdata.Close()

                        Using responsepost As System.Net.HttpWebResponse = requestpost.GetResponse()
                            Dim postjsonSsatus As String = responsepost.StatusCode

                            Select Case responsepost.StatusCode
                                Case HttpStatusCode.OK, HttpStatusCode.Accepted
                                    StateAppcept = True
                                Case Else
                                    StateAppcept = False
                                    MsgBox(responsepost.StatusDescription)
                            End Select
                            '                            '200':
                            'description:                The request was accepted.
                            '            content:
                            '                            Application/ json
                            '                schema:
                            '                  $ref '#/components/schemas/EFSResponse'
                            '          "401"
                            '            description:    Unauthorized
                            '          "403"
                            '            description:    Forbidden
                            '          "500"
                            '            description:    Internal server error

                        End Using

                    Else
                        Return False
                    End If

                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message())
            Return False
        End Try

        If StateAppcept = False Then
            Return False
        End If

        Dim strFile As String = _DefailtPath & "\" & invoiceno.Replace("/", "-").Replace("\", "-") & ".txt"

        Try
            File.Delete(strFile)
        Catch ex As Exception
        End Try

        Dim fileExists As Boolean = File.Exists(strFile)

        Using sw As New StreamWriter(File.Open(strFile, FileMode.OpenOrCreate))
            sw.WriteLine(
            IIf(fileExists,
             EFSjson_data,
            EFSjson_data))
        End Using

        Return True

    End Function


    Private Function SetdataJSON(invoiceno As String, invoicedate As String, Optional PostData As Boolean = False) As Boolean
        Dim cmdstring As String = ""
        Dim VenderPramCode As String = ""
        Dim Material As String = ""
        Dim FTCurCode As String = ""
        Dim RIndx As Integer = 0
        Dim dt As DataTable
        Dim _dtinvoice As DataTable
        Dim JSonHeadcer As New EFSHeader
        Dim InvAmt As Decimal = 0.0
        Dim GInvAmt As Decimal = 0.0

        Dim Lpo As New List(Of EFS_PurchaseOrder)
        Dim LItem As List(Of EFS_item_Line)
        Dim LSize As List(Of EFS_item_sizes)


        cmdstring = " SELECT        A.FTCustomerPO, A.FTInvoiceExportNo, C.FTColorway, C.FTSizeBreakDown, C.FTPOLineItem, MAX(ISNULL(SSPX.FNPrice,0)) AS FNPrice, MAX(ISNULL(SSPX.FNPriceOrg,0)) AS FNPriceFOB, MAX(ISNULL(SSPX.FNNetPrice,0)) AS FNNetPrice "
        cmdstring &= vbCrLf & "  , MAX(ISNULL(SSPX.FNNetNetPrice,0))  As FNFirstPrice, SUM(C.FNQuantity) As FNQuantity,MAX(ISNULL(SSA.FNMatSizeSeq,0) ) AS FNMatSizeSeq ,MAX(ISNULL(SSPX.FTVenderPramCode,'')) AS FTVenderPramCode, MAX(ISNULL(SSPX.FTMaterial,'')) AS FTMaterial, MAX(ISNULL(SSPX.FTCurCode,'')) AS FTCurCode"
        cmdstring &= vbCrLf & " FROM             " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TACCTFactoryCMInvoice AS A WITH(NOLOCK) INNER JOIN"
        cmdstring &= vbCrLf & "    " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TACCTFactoryCMInvoice_D AS C WITH(NOLOCK) ON A.FTCustomerPO = C.FTCustomerPO AND A.FTInvoiceNo = C.FTInvoiceNo"
        cmdstring &= vbCrLf & " OUTER APPLY ( SELECT TOP 1 FNMatSizeSeq FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMMatSize AS XX WITH(NOLOCK) WHERE XX.FTMatSizeCode = C.FTSizeBreakDown ) AS SSA "
        cmdstring &= vbCrLf & " OUTER APPLY ( SELECT TOP 1 XX.FNPrice,XX.FNPriceOrg,XX.FNNetPrice,XX.FNNetFOB,XX.FNNetNetPrice, PG.FTVenderPramCode,LEFT(ST.FTStyleCode,6) AS FTMaterial,CUR.FTCurCode "
        cmdstring &= vbCrLf & "	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS XX WITH(NOLOCK) INNER JOIN"
        cmdstring &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON XX.FTOrderNo = O.FTOrderNo INNER JOIN"
        cmdstring &= vbCrLf & "	    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMVenderPram  AS PG WITH(NOLOCK) ON O.FNHSysVenderPramId = PG.FNHSysVenderPramId"
        cmdstring &= vbCrLf & "	  INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle AS ST WITH(NOLOCK)   ON O.FNHSysStyleId = ST.FNHSysStyleId "
        cmdstring &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S WITH(NOLOCK)  ON XX.FTOrderNo = S.FTOrderNo AND XX.FTSubOrderNo = S.FTSubOrderNo LEFT OUTER JOIN"
        cmdstring &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS CUR WITH(NOLOCK) ON S.FNHSysCurId = CUR.FNHSysCurId"

        cmdstring &= vbCrLf & " WHERE XX.FTPOref = A.FTCustomerPO "
        cmdstring &= vbCrLf & " And XX.FTColorway= C.FTColorway "
        cmdstring &= vbCrLf & " And XX.FTSizeBreakDown=C.FTSizeBreakDown "
        cmdstring &= vbCrLf & " And XX.FTNikePOLineItem=C.FTPOLineItem  ) AS SSPX "
        cmdstring &= vbCrLf & " WHERE        (A.FTInvoiceExportNo = N'" & HI.UL.ULF.rpQuoted(invoiceno) & "') "
        cmdstring &= vbCrLf & "GROUP BY A.FTCustomerPO, A.FTInvoiceExportNo, C.FTColorway, C.FTSizeBreakDown, C.FTPOLineItem "

        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_ACCOUNT)
        If dt.Rows.Count > 0 Then

            InvAmt = 0
            GInvAmt = 0
            For Each R As DataRow In dt.Rows
                GInvAmt = GInvAmt + CDbl(Format(Val(R!FNQuantity.ToString()) * Val(R!FNFirstPrice.ToString()), "0.00"))
            Next


            InvAmt = Decimal.Parse(Format(GInvAmt, "0.00"))

            Dim grp As List(Of String) = (dt.Select("FTCustomerPO<>''", "FTCustomerPO").CopyToDataTable).AsEnumerable() _
                                               .Select(Function(r) r.Field(Of String)("FTCustomerPO")) _
                                               .Distinct() _
                                               .ToList()



            For Each PONO As String In grp

                Dim xPO As New EFS_PurchaseOrder
                xPO.po_number = PONO

                LItem = New List(Of EFS_item_Line)

                'cmdstring = "   SELECT TOP 1 '" & HI.UL.ULF.rpQuoted(invoiceno) & "' AS FTInvoiceExportNo,'" & HI.UL.ULDate.ConvertEnDB(invoicedate) & "' AS FDInvoiceExportDate,MX.FTVenderPramCode,MX.FTMaterial,MX.FTCurCode"
                'cmdstring &= vbCrLf & " FROM             " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TACCTFactoryCMInvoice AS AX WITH(NOLOCK) "
                'cmdstring &= vbCrLf & " OUTER APPLY ( "
                'cmdstring &= vbCrLf & "		SELECT TOP 1  PG.FTVenderPramCode,LEFT(ST.FTStyleCode,6) AS FTMaterial,C.FTCurCode "
                'cmdstring &= vbCrLf & "	FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS OS WITH(NOLOCK) INNER JOIN"
                'cmdstring &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON OS.FTOrderNo = O.FTOrderNo INNER JOIN"
                'cmdstring &= vbCrLf & "	    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMVenderPram  AS PG WITH(NOLOCK) ON O.FNHSysVenderPramId = PG.FNHSysVenderPramId"
                'cmdstring &= vbCrLf & "	  INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle AS ST WITH(NOLOCK)   ON O.FNHSysStyleId = ST.FNHSysStyleId "
                'cmdstring &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S WITH(NOLOCK)  ON OS.FTOrderNo = S.FTOrderNo AND OS.FTSubOrderNo = S.FTSubOrderNo LEFT OUTER JOIN"
                'cmdstring &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS C WITH(NOLOCK) ON S.FNHSysCurId = C.FNHSysCurId"
                'cmdstring &= vbCrLf & "	  WHERE OS.FTPOref ='" & HI.UL.ULF.rpQuoted(PONO) & "'"
                'cmdstring &= vbCrLf & "  ) MX "
                'cmdstring &= vbCrLf & "	  WHERE AX.FTCustomerPO ='" & HI.UL.ULF.rpQuoted(PONO) & "'"

                '_dtinvoice = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_ACCOUNT)

                _dtinvoice = dt.Select("FTCustomerPO='" & HI.UL.ULF.rpQuoted(PONO) & "'").CopyToDataTable()

                If _dtinvoice.Rows.Count > 0 Then
                    VenderPramCode = ""
                    Material = ""
                    FTCurCode = ""
                    For Each Rx As DataRow In _dtinvoice.Rows

                        VenderPramCode = Rx!FTVenderPramCode.ToString()
                        Material = Rx!FTMaterial.ToString()
                        FTCurCode = Rx!FTCurCode.ToString()

                        Exit For
                    Next


                    If RIndx = 0 Then

                        RIndx = RIndx + 1

                        JSonHeadcer.manufacturing_invoice_number = "MCI-" & invoiceno
                        JSonHeadcer.factory_commercial_invoice_number = invoiceno
                        JSonHeadcer.factory_code = VenderPramCode
                        JSonHeadcer.invoice_currency_cd = FTCurCode
                        JSonHeadcer.total_invoice_amount = InvAmt
                        JSonHeadcer.worksheet_indicator = "N"

                    End If


                    Dim grplineitem As List(Of String) = (dt.Select("FTCustomerPO='" & HI.UL.ULF.rpQuoted(PONO) & "'", "FTPOLineItem").CopyToDataTable).AsEnumerable() _
                                               .Select(Function(r) r.Field(Of String)("FTPOLineItem")) _
                                               .Distinct() _
                                               .ToList()


                    For Each PLineItem As String In grplineitem
                        Dim DLO As New EFS_item_Line
                        DLO.item_seq_number = Val(PLineItem)

                        LSize = New List(Of EFS_item_sizes)

                        For Each Rx As DataRow In dt.Select("FTCustomerPO='" & HI.UL.ULF.rpQuoted(PONO) & "' AND  FTPOLineItem='" & PLineItem & "'", "FNMatSizeSeq")
                            DLO.product_cd = Material & "-" & Rx!FTColorway.ToString()



                            Dim DSize As New EFS_item_sizes
                            DSize.size = Rx!FTSizeBreakDown.ToString()
                            DSize.size_quantity = Val(Rx!FNQuantity.ToString)

                            Dim DPrice As New EFS_sale_price
                            DPrice.value = CDbl(Format(Val(Rx!FNFirstPrice.ToString), "0.00"))
                            DPrice.currency_cd = FTCurCode
                            DSize.extended_first_sale_price = DPrice


                            LSize.Add(DSize)
                        Next


                        DLO.included_sizes = LSize
                        LItem.Add(DLO)

                    Next

                End If

                xPO.line_items = LItem

                Lpo.Add(xPO)
            Next

            JSonHeadcer.purchase_orders = Lpo

            Dim DataJS As New EFSJSON
            DataJS.request = JSonHeadcer


            If PostData Then
                If SendJSONXML(DataJS, invoiceno) Then


                    cmdstring = "Update B SET "
                    cmdstring &= vbCrLf & "  FTPostUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    cmdstring &= vbCrLf & " ,  FDPostDate=" & HI.UL.ULDate.FormatDateDB & " "
                    cmdstring &= vbCrLf & " ,  FTPostTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    cmdstring &= vbCrLf & "   FROM            " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TACCTFactoryCMInvoice As A With(NOLOCK) INNER JOIN"
                    cmdstring &= vbCrLf & "                   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & ".dbo.TACCTXMLCreateInvoice As B  On A.FTCustomerPO = B.FTCustomerPO And A.FTInvoiceNo = B.FTInvoiceNo"
                    cmdstring &= vbCrLf & "   WHERE   A.FTInvoiceExportNo='" & HI.UL.ULF.rpQuoted(invoiceno) & "'"

                        If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_ACCOUNT) = False Then

                        End If


                        Return True

                Else

                    Return False

                End If
            Else
                Dim EFSjson_data As String = JsonConvert.SerializeObject(DataJS)

                Dim strFile As String = _DefailtPath & "\" & invoiceno.Replace("/", "-").Replace("\", "-") & ".txt"

                Try
                    File.Delete(strFile)
                Catch ex As Exception
                End Try

                Dim fileExists As Boolean = File.Exists(strFile)

                Using sw As New StreamWriter(File.Open(strFile, FileMode.OpenOrCreate))
                    sw.WriteLine(
                            IIf(fileExists,
                            EFSjson_data,
                            EFSjson_data))
                End Using

                Return True

            End If



        Else
                Return False
        End If


    End Function

    Public Shared Function ReadRegistry() As String
        Dim regKey As RegistryKey
        Dim valreturn As String = ""

        regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)
        If regKey Is Nothing Then

            Registry.CurrentUser.CreateSubKey("Software\HI SOFT", RegistryKeyPermissionCheck.ReadWriteSubTree)
            regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        End If

        valreturn = regKey.GetValue("PathExporJsonNike", "")
        regKey.Close()

        Return valreturn
    End Function

    Public Shared Sub WriteRegistry(ByVal value As Object)

        Dim regKey As RegistryKey
        regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        If regKey Is Nothing Then

            Registry.CurrentUser.CreateSubKey("Software\HI SOFT", True)
            regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        End If

        regKey.SetValue("PathExporJsonNike", value.ToString)
        regKey.Close()

    End Sub


    Private Sub ocmexporttoxml_Click(sender As Object, e As EventArgs) Handles ocmpostdatajson.Click
        If Not (Me.ogcdirector.DataSource Is Nothing) Then
            Dim dtdata As DataTable
            With CType(ogcdirector.DataSource, DataTable)
                .AcceptChanges()
                dtdata = .Copy
            End With

            Dim State As Boolean = False
            If dtdata.Select("FTSelect='1'").Length > 0 Then

                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการส่งข้อมูลไปยัง NIKE ใช่หรือไม่ ?", 19458471) = True Then

                    Dim Op As New System.Windows.Forms.FolderBrowserDialog 'System.Windows.Forms.SaveFileDialog
                    Dim _IndSeq As Integer = 0
                    With Op

                        If _DefailtPath <> "" Then
                            .SelectedPath = _DefailtPath
                        End If


                        If .ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                            If _DefailtPath <> .SelectedPath Then
                                WriteRegistry(.SelectedPath)
                                _DefailtPath = .SelectedPath
                            End If


                            Dim Spls As New HI.TL.SplashScreen("Sending.....")

                            For Each R As DataRow In dtdata.Select("FTSelect='1'")

                                If SetdataJSON(R!FTInvoiceExportNo.ToString, R!FDInvoiceExportDate.ToString, True) Then

                                    State = True

                                End If
                            Next

                            Call RefreshGrid()
                            Spls.Close()

                        End If
                    End With



                    If State Then
                        HI.MG.ShowMsg.mInfo("Send Data To NIKE Complete !!!", 181955487, Me.Text,, MessageBoxIcon.Information)

                    End If
                End If

            End If

        End If

    End Sub

    Private Sub ogvdirector_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvdirector.RowStyle


        Try
            With Me.ogvdirector
                If "" & .GetRowCellValue(e.RowHandle, "StateXML").ToString = "1" Then

                    e.Appearance.ForeColor = Color.Green

                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmpreview_Click_1(sender As Object, e As EventArgs) Handles ocmpreview.Click
        If Not (Me.ogcdirector.DataSource Is Nothing) Then
            Dim dtdata As DataTable
            With CType(ogcdirector.DataSource, DataTable)
                .AcceptChanges()
                dtdata = .Copy
            End With

            Dim State As Boolean = False
            If dtdata.Select("FTSelect='1'").Length > 0 Then


                For Each R As DataRow In dtdata.Select("FTSelect='1'")


                    Dim _Fm As String = ""
                    _Fm = "{V_FCIInvoice.FTInvoiceExportNo}='" & HI.UL.ULF.rpQuoted(R!FTInvoiceExportNo.ToString) & "' "

                    With New HI.RP.Report
                            .FormTitle = Me.Text
                            .ReportFolderName = "Account\"
                        .ReportName = "ReportInvoiceEFS.rpt"
                        .Formular = _Fm
                            .Preview()
                        End With

                Next

            End If

        End If
    End Sub

    Private Sub ocmexporttext_Click(sender As Object, e As EventArgs) Handles ocmexporttext.Click
        If Not (Me.ogcdirector.DataSource Is Nothing) Then
            Dim dtdata As DataTable
            With CType(ogcdirector.DataSource, DataTable)
                .AcceptChanges()
                dtdata = .Copy
            End With

            Dim State As Boolean = False
            If dtdata.Select("FTSelect='1'").Length > 0 Then

                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Export Data To Text File ใช่หรือไม่ ?", 19458488) = True Then

                    Dim Op As New System.Windows.Forms.FolderBrowserDialog 'System.Windows.Forms.SaveFileDialog
                    Dim _IndSeq As Integer = 0
                    With Op

                        If _DefailtPath <> "" Then
                            .SelectedPath = _DefailtPath
                        End If


                        If .ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                            If _DefailtPath <> .SelectedPath Then
                                WriteRegistry(.SelectedPath)
                                _DefailtPath = .SelectedPath
                            End If


                            Dim Spls As New HI.TL.SplashScreen("Sending.....")

                            For Each R As DataRow In dtdata.Select("FTSelect='1'")

                                If SetdataJSON(R!FTInvoiceExportNo.ToString, R!FDInvoiceExportDate.ToString) Then

                                    State = True

                                End If
                            Next

                            Call RefreshGrid()
                            Spls.Close()

                        End If
                    End With



                    If State Then
                        HI.MG.ShowMsg.mInfo("Export Data To Text File Complete !!!", 181955987, Me.Text,, MessageBoxIcon.Information)


                        Try

                            Process.Start("explorer.exe", _DefailtPath)

                        Catch ex As Exception

                        End Try
                    End If
                End If

            End If

        End If
    End Sub
End Class