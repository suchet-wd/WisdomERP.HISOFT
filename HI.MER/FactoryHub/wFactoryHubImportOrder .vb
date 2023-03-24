Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
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
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.IO
Imports System.Text
Imports System.Net
Imports Microsoft.Win32
Imports System.Web
Imports System.Runtime.Serialization
Imports System.ComponentModel
Imports System.Xml

Public Class wFactoryHubImportOrder

    Private Shared _MContextMenuStripGrid As System.Windows.Forms.ContextMenuStrip

    Private MappIngSize As wFactoryHubMappingSize
    Private stylepopup As wFactoryHubStylePopup

    ''' Used Data Adapter to control database


    Private _FactoryHubListOrderNo As wFactoryHubListOrderNo
    Private Inited As Boolean
    Private _Clear As Boolean = False
    Dim FirstLoad As Boolean = True
    Private tSql As String = ""
    Private StrAllPgm As String = "HIT,HIC,HIG,HTV,HIP,HSC"
    Private PathFileXML As String = System.Windows.Forms.Application.StartupPath & "\FHSXML"
    Private PathFileXMLCompleted As String = System.Windows.Forms.Application.StartupPath & "\FHSXML\Completed"

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
        _FactoryHubListOrderNo = New wFactoryHubListOrderNo

        HI.TL.HandlerControl.AddHandlerObj(_FactoryHubListOrderNo)

        Dim oSysLang As New HI.ST.SysLanguage

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _FactoryHubListOrderNo.Name.ToString.Trim, _FactoryHubListOrderNo)
        Catch ex As Exception
        End Try

        MappIngSize = New wFactoryHubMappingSize
        HI.TL.HandlerControl.AddHandlerObj(MappIngSize)


        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, MappIngSize.Name.ToString.Trim, MappIngSize)
        Catch ex As Exception
        End Try


        stylepopup = New wFactoryHubStylePopup
        HI.TL.HandlerControl.AddHandlerObj(stylepopup)


        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, stylepopup.Name.ToString.Trim, stylepopup)
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
        FNHSysMerTeamId.Text = "MP"
        If CheckDataBaseFHS() Then

            Call CreateManuStripGrid()

            'Me.ogdmain.ContextMenuStrip = _MContextMenuStripGrid
            Me.mogcdt1.ContextMenuStrip = _MContextMenuStripGrid
            Me.mogcdt3.ContextMenuStrip = Nothing
            Me.mogcdt4.ContextMenuStrip = Nothing
            Me.mogcdt5.ContextMenuStrip = Nothing
            Me.mogcdt6.ContextMenuStrip = Nothing

            Call ShowData()

        End If

    End Sub

    Private Sub CreateManuStripGrid()

        _MContextMenuStripGrid = New System.Windows.Forms.ContextMenuStrip

        Dim _ShowToPDF As New System.Windows.Forms.ToolStripMenuItem

        With _ShowToPDF

            .Name = "ocmShowPDF"
            .Text = "Show PDF File"
            AddHandler .Click, AddressOf ShowToPDFl_Click

        End With

        With _MContextMenuStripGrid
            .Name = "ContextExportDataGridControl"
            .Items.AddRange(New System.Windows.Forms.ToolStripItem() {_ShowToPDF})
        End With

    End Sub

    Private Sub ShowToPDFl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            Dim FTFacCode As String = ""
            Dim FTPONo As String = ""

            With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraGrid.GridControl)

                Dim _ObjMainView As Object = .MainView
                Select Case HI.ENM.Control.GeTypeControl(_ObjMainView)
                    Case ENM.Control.ControlType.BandedGridView

                        Dim _BandedGridView As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView = .MainView
                        With _BandedGridView

                            Try

                                FTFacCode = (.GetFocusedRowCellValue("Factory_Vendor_Code").ToString())
                                FTPONo = (.GetFocusedRowCellValue("PO_Number").ToString())

                            Catch ex As Exception
                                FTFacCode = ""
                                FTPONo = ""
                            End Try

                        End With

                    Case ENM.Control.ControlType.AdvBandedGridView

                        Dim _AdvBandedGridView As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView = .MainView
                        With _AdvBandedGridView


                            Try
                                FTFacCode = (.GetFocusedRowCellValue("Factory_Vendor_Code").ToString())
                                FTPONo = (.GetFocusedRowCellValue("PO_Number").ToString())
                            Catch ex As Exception

                                FTFacCode = ""
                                FTPONo = ""
                            End Try

                        End With

                    Case ENM.Control.ControlType.GridView

                        Dim _GridView As DevExpress.XtraGrid.Views.Grid.GridView = .MainView
                        With _GridView

                            Try
                                FTFacCode = (.GetFocusedRowCellValue("Factory_Vendor_Code").ToString())
                                FTPONo = (.GetFocusedRowCellValue("PO_Number").ToString())
                            Catch ex As Exception
                                FTFacCode = ""
                                FTPONo = ""
                            End Try

                        End With

                End Select

            End With

            If FTFacCode <> "" And FTPONo <> "" Then
                Call GetWebServiceOrdersGetPDF(FTFacCode, FTPONo)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Function GetWebServiceOrdersDataGetUnAcknowledged(VenderPrgm As String) As Boolean
        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)

        Dim PageCount As Integer = 0
        Dim urlEndPoint As String = "http://192.168.99.156/OLLIe/OLLIe.svc/Orders" '"http://192.168.99.156/OLLIe/OLLIe.svc?wsdl"
        Dim soapAction As String = "http://tempuri.org/IOrders/OrdersDataGetUnAcknowledged"
        ' Refer to the documentation for more information on how to get the client id/secret
        ' urlEndPoint = soapAction
        ' Refer to the documentation for more information on how to get the tokens
        Dim accessToken As String = ""
        Dim jsonResponseText As String = ""

        Dim FactoryCode As String = VenderPrgm
        Dim _XMLFIleName As String = VenderPrgm & "_" & DateTime.Now().ToString().Replace(" ", "_").Replace("/", "_").Replace(":", "_")

        Dim FHubXML As New FactoryHub.FactoryHubXML()
        Dim json_data As String = FHubXML.OrdersDataGetUnAcknowledgedXMLRequest(FactoryCode, "0")

        Dim data As String = json_data
        ' Dim url As String = "http://192.168.99.156/OLLIe/OLLIe.svc?wsdl"
        Dim responsestring As String = ""

        Try

            Dim myReq As HttpWebRequest = WebRequest.Create(urlEndPoint)
            Dim encoding As New ASCIIEncoding
            Dim buffer() As Byte = encoding.GetBytes(json_data)
            Dim response As String = ""

            myReq.AllowWriteStreamBuffering = False
            myReq.Method = "POST"
            myReq.ContentType = "text/xml;charset=UTF-8"
            myReq.Headers.Add("SOAPAction", soapAction)
            myReq.ContentLength = buffer.Length

            Dim post As Stream = myReq.GetRequestStream

            post.Write(buffer, 0, buffer.Length)
            post.Close()

            Dim myResponse As HttpWebResponse = myReq.GetResponse
            Dim responsedata As Stream = myResponse.GetResponseStream
            Dim responsereader As New StreamReader(responsedata)

            response = responsereader.ReadToEnd

            Dim theDataSet As New DataSet()
            Try

                If response <> "" Then

                    Dim theReader As New StringReader(response)

                    theDataSet.ReadXml(theReader)

                    If theDataSet.Tables.IndexOf("DataGetOrderHeaders") >= 0 Then

                        Dim XmlFilePath As String = PathFileXML & "\" & _XMLFIleName & ".XML"


                        'XmlFilePath = "\\wsm-app-91\WISDOM_SYSTEM\FHSXML\HIT_18_06_2020_11_44_06_AM-admin.XML"
                        'theDataSet.ReadXml(XmlFilePath)

                        theDataSet.WriteXml(XmlFilePath)

                        SaveXML(XmlFilePath, _XMLFIleName, theReader)

                        Dim OrderHeaders As New DataTable
                        Dim OrderItems As New DataTable
                        Dim OrderItemsText As New DataTable
                        Dim OrderItemsVas As New DataTable
                        Dim OrderSizes As New DataTable
                        Dim OrderSizesVas As New DataTable

                        For Each tablename As String In {"DataGetOrderHeaders", "DataGetOrderItems", "DataGetOrderSizes", "DataGetOrderItemsVas", "DataGetOrderSizesVas", "DataGetOrderItemsText"}

                            Select Case tablename
                                Case "DataGetOrderHeaders"
                                    Try
                                        OrderHeaders = theDataSet.Tables(tablename).Copy
                                    Catch ex As Exception
                                        OrderHeaders = Nothing
                                    End Try
                                Case "DataGetOrderItems"
                                    Try
                                        OrderItems = theDataSet.Tables(tablename).Copy
                                    Catch ex As Exception
                                        OrderItems = Nothing
                                    End Try
                                Case "DataGetOrderSizes"
                                    Try
                                        OrderSizes = theDataSet.Tables(tablename).Copy
                                    Catch ex As Exception
                                        OrderSizes = Nothing
                                    End Try
                                Case "DataGetOrderItemsVas"
                                    Try
                                        OrderItemsVas = theDataSet.Tables(tablename).Copy
                                    Catch ex As Exception
                                        OrderItemsVas = Nothing
                                    End Try

                                Case "DataGetOrderSizesVas"
                                    Try
                                        OrderSizesVas = theDataSet.Tables(tablename).Copy
                                    Catch ex As Exception
                                        OrderSizesVas = Nothing
                                    End Try

                                Case "DataGetOrderItemsText"
                                    Try
                                        OrderItemsText = theDataSet.Tables(tablename).Copy
                                    Catch ex As Exception
                                        OrderItemsText = Nothing
                                    End Try

                            End Select
                        Next

                        theDataSet.Dispose()

                        If SaveOrder(OrderHeaders, OrderItems, OrderSizes, OrderItemsVas, OrderSizesVas, OrderItemsText) Then

                            Try
                                File.Copy(XmlFilePath, PathFileXMLCompleted & "\" & _XMLFIleName & "-" & HI.ST.UserInfo.UserName & ".XML", True)
                                File.Delete(XmlFilePath)

                            Catch ex As Exception
                            End Try

                        End If

                    End If

                Else

                    theDataSet.Dispose()
                    Return False

                End If

            Catch ex As Exception

                theDataSet.Dispose()

                MsgBox(ex.Message() & " Program " & VenderPrgm)
                Return False

            End Try

        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function SaveXML(XMLPathString As String, FileName As String, theReader As StringReader) As Boolean
        Dim _Qry As String

        Try

            Dim data As Byte()
            data = System.IO.File.ReadAllBytes(XMLPathString)
            Dim doc As New XmlDocument
            doc.Load(XMLPathString)

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_FHS)
            HI.Conn.SQLConn.SqlConnectionOpen()

            Dim cmd As New SqlCommand("USP_INSERT_XML", HI.Conn.SQLConn.Cnn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@User", HI.ST.UserInfo.UserName)
            cmd.Parameters.AddWithValue("@FileName", FileName)
            cmd.Parameters.AddWithValue("@xml", New SqlTypes.SqlXml(New XmlNodeReader(doc)))
            cmd.Parameters.AddWithValue("@ObjBi", New SqlTypes.SqlBinary(data))

            cmd.ExecuteNonQuery()

            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

            doc = Nothing
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function SaveOrder(OrderHeaders As DataTable, OrderItems As DataTable, OrderSizes As DataTable, OrderItemsVas As DataTable, OrderSizesVas As DataTable, OrderItemsText As DataTable) As Boolean

        Dim PORef As String = ""
        Dim LineItemNo As String = ""
        Dim State As Boolean = False

        Dim CmdInsertOrderItems As String = ""

        Dim cmdstring As String = ""
        Dim ColIdx As Integer = 0


        For Each R As DataRow In OrderHeaders.Rows

            Try

                PORef = R!PO_Number
                State = True


                ColIdx = 0
                cmdstring = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderHeaders (PO_Number,PO_Doc_Date,Company_Code,PO_Org,PO_Group,PO_Type,Currency_Type,Ship_Via_Instructions,BUY_SEASON,BUY_YEAR,BUY_GROUP,Factory_Vendor_Code,Vendor_Location_Code_MCO,Sold_To_Ref,PO_Ref,TTMI,FxRelevant,NewOrderFlag,Source_System,FTStateNew,FTUserName,FTDate,FTTime) "
                cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(R!PO_Number.ToString()) & "' "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!PO_Doc_Date.ToString()) & "' "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!Company_Code.ToString()) & "' "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!PO_Org.ToString()) & "' "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!PO_Group.ToString()) & "' "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!PO_Type.ToString()) & "' "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!Currency_Type.ToString()) & "' "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!Ship_Via_Instructions.ToString()) & "' "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!BUY_SEASON.ToString()) & "' "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!BUY_YEAR.ToString()) & "' "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!BUY_GROUP.ToString()) & "' "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!Factory_Vendor_Code.ToString()) & "' "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!Vendor_Location_Code_MCO.ToString()) & "' "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!Sold_To_Ref.ToString()) & "' "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!PO_Ref.ToString()) & "' "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!TTMI.ToString()) & "' "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FxRelevant.ToString()) & "' "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!NewOrderFlag.ToString()) & "' "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!Source_System.ToString()) & "' "
                cmdstring &= vbCrLf & ",'1' AS FTStateNew  "
                cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "

                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try

                    Dim cmd As String = ""
                    cmd = " delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderHeaders  where PO_Number='" & HI.UL.ULF.rpQuoted(PORef) & "'  "
                    cmd &= vbCrLf & " delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItems   where PO_Number='" & HI.UL.ULF.rpQuoted(PORef) & "'  "
                    cmd &= vbCrLf & " delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderSizes  where PO_Number='" & HI.UL.ULF.rpQuoted(PORef) & "'  "
                    cmd &= vbCrLf & " delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItemsVas   where PO_Number='" & HI.UL.ULF.rpQuoted(PORef) & "'  "
                    cmd &= vbCrLf & " delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderSizesVas  where PO_Number='" & HI.UL.ULF.rpQuoted(PORef) & "'  "
                    cmd &= vbCrLf & " delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItemsText where PO_Number='" & HI.UL.ULF.rpQuoted(PORef) & "'  "

                    HI.Conn.SQLConn.Execute_Tran(cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) > 0 Then

                        For Each R2 As DataRow In OrderItems.Select("PO_Number='" & HI.UL.ULF.rpQuoted(PORef) & "'")

                            ColIdx = 0
                            cmdstring = ""
                            cmdstring = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItems (PO_Number,PO_Item,Material_Number,Material_Description,Company_Code"
                            cmdstring &= vbCrLf & " ,Plant,Nike_Division_Code,Quantity,UOM,Mode_Code,Mode_Code_Description,OGAC_Date,GAC_Date,GAC_Reason_Code,Customer_PO"
                            cmdstring &= vbCrLf & ",Customer_Request_Date,SO_Billing_Date,Sub_Contract_Vendor,Delivery_Date,Statistical_Delivery_Date,Launch_Indicator"
                            cmdstring &= vbCrLf & ",Material_Dev_Code,Silhhouette_Code,Gender_Age_Code,SO_NUMBER,SO_ITEM,AFS_STOCK_CATEGORY,CHANGED_BY,CUST_PO_ITEM"
                            cmdstring &= vbCrLf & ",Address_Code_Id,Ship_To_Account,Color_Combo_Name,Color_Combo_ShortName,RGAC_Date"
                            cmdstring &= vbCrLf & ",Plan_Month_Date,MSR_Indicator,Plant_Ref,MSRP_US,FXAdjAmount,FXAdjPercent,Customer_Cancellation_Date"
                            cmdstring &= vbCrLf & ",CABCode,UCC_NAME1,UCC_ADDRESS1,UCC_ADDRESS2,UCC_ADDRESS3,UCC_CITY,UCC_REGION,UCC_ZIP,UCC_COUNTRY,CI_NAME1,CI_NAME2"
                            cmdstring &= vbCrLf & ",CI_NAME3,CI_NAME4,CI_ADDRESS1,CI_ADDRESS2,CI_ADDRESS3,CI_ADDRESS4,CI_CITY,CI_REGION,CI_ZIP,CI_COUNTRYTXT"
                            cmdstring &= vbCrLf & ",NewOrderFlag,Acceptance_Date,Tracking_Number,Telephone,Telephone2,NIKEiD_SAP_PO,NIKEiD_SAP_PO_ITEM,PO_Summary_Qtr,FTDate,FTTime,FTUserName) "
                            cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(R2!PO_Number.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!PO_Item.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Material_Number.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Material_Description.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Company_Code.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Plant.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Nike_Division_Code.ToString()) & "' "
                            cmdstring &= vbCrLf & "," & Val(R2!Quantity.ToString()) & " "

                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!UOM.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Mode_Code.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Mode_Code_Description.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!OGAC_Date.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!GAC_Date.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!GAC_Reason_Code.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Customer_PO.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Customer_Request_Date.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!SO_Billing_Date.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Sub_Contract_Vendor.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Delivery_Date.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Statistical_Delivery_Date.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Launch_Indicator.ToString()) & "' "

                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Material_Dev_Code.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Silhhouette_Code.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Gender_Age_Code.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!SO_NUMBER.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!SO_ITEM.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!AFS_STOCK_CATEGORY.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!CHANGED_BY.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!CUST_PO_ITEM.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Address_Code_Id.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Ship_To_Account.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Color_Combo_Name.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Color_Combo_ShortName.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!RGAC_Date.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Plan_Month_Date.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!MSR_Indicator.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Plant_Ref.ToString()) & "' "

                            cmdstring &= vbCrLf & "," & Val(R2!MSRP_US.ToString()) & " "
                            cmdstring &= vbCrLf & "," & Val(R2!FXAdjAmount.ToString()) & " "
                            cmdstring &= vbCrLf & "," & Val(R2!FXAdjPercent.ToString()) & " "

                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Customer_Cancellation_Date.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!CABCode.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!UCC_NAME1.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!UCC_ADDRESS1.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!UCC_ADDRESS2.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!UCC_ADDRESS3.ToString()) & "' "

                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!UCC_CITY.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!UCC_REGION.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!UCC_ZIP.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!UCC_COUNTRY.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!CI_NAME1.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!CI_NAME2.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!CI_NAME3.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!CI_NAME4.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!CI_ADDRESS1.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!CI_ADDRESS2.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!CI_ADDRESS3.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!CI_ADDRESS4.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!CI_CITY.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!CI_REGION.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!CI_ZIP.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!CI_COUNTRYTXT.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!NewOrderFlag.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Acceptance_Date.ToString()) & "' "

                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Tracking_Number.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Telephone.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Telephone2.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!NIKEiD_SAP_PO.ToString()) & "' "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!NIKEiD_SAP_PO_ITEM.ToString()) & "' "
                            cmdstring &= vbCrLf & "," & Val(R2!PO_Summary_Qtr.ToString()) & " "

                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                            cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

                            If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                State = False
                                Exit For
                            End If
                        Next

                        If State Then

                            Try
                                For Each R2 As DataRow In OrderSizes.Select("PO_Number='" & HI.UL.ULF.rpQuoted(PORef) & "'")



                                    ColIdx = 0


                                    cmdstring = ""

                                    cmdstring = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderSizes (PO_Number,PO_Item,PO_Size_Index,SIZE_GRID_VALUE,Quantity,UPC_Number,Net_Price,Gross_Price,NIKEiD_SAP_FOB,FTDate,FTTime,FTUserName) "
                                    cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(R2!PO_Number.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!PO_Item.ToString()) & "' "
                                    cmdstring &= vbCrLf & "," & Val(R2!PO_Size_Index.ToString()) & " "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!SIZE_GRID_VALUE.ToString()) & "' "
                                    cmdstring &= vbCrLf & "," & Val(R2!Quantity.ToString()) & " "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!UPC_Number.ToString()) & "' "
                                    cmdstring &= vbCrLf & "," & Val(R2!Net_Price.ToString()) & " "
                                    cmdstring &= vbCrLf & "," & Val(R2!Gross_Price.ToString()) & " "
                                    cmdstring &= vbCrLf & "," & Val(R2!NIKEiD_SAP_FOB.ToString()) & " "

                                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

                                    If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                        State = False
                                        Exit For
                                    End If

                                Next
                            Catch ex As Exception
                                State = False
                            End Try


                        End If


                        If State And Not (OrderItemsVas Is Nothing) Then

                            Try
                                For Each R2 As DataRow In OrderItemsVas.Select("PO_Number='" & HI.UL.ULF.rpQuoted(PORef) & "'")

                                    ColIdx = 0
                                    cmdstring = ""
                                    cmdstring = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItemsVas (PO_Number,PO_Item,SO_Item_Sequence_Number,SO_Number,SO_Item,VAS_CODE,VAS_FIELD,VAS_TYPE,VAS_SIZE,VAS_MATNR,MSR_BUOM,VAS_DESCRIPTION) "
                                    cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(R2!PO_Number.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!PO_Item.ToString()) & "' "
                                    cmdstring &= vbCrLf & "," & Val(R2!SO_Item_Sequence_Number.ToString()) & " "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!SO_Number.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!SO_Item.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!VAS_CODE.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!VAS_FIELD.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!VAS_TYPE.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!VAS_SIZE.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!VAS_MATNR.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!MSR_BUOM.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!VAS_DESCRIPTION.ToString()) & "' "

                                    If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                        State = False
                                        Exit For
                                    End If

                                Next

                            Catch ex As Exception
                                State = False
                            End Try

                        End If


                        If State And Not (OrderSizesVas Is Nothing) Then

                            Try
                                For Each R2 As DataRow In OrderSizesVas.Select("PO_Number='" & HI.UL.ULF.rpQuoted(PORef) & "'")

                                    ColIdx = 0
                                    cmdstring = ""
                                    cmdstring = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderSizesVas (PO_Number,PO_Item,PO_Size_Index,SO_Item_Sequence_Number,SO_Number,SO_Item,VAS_CODE,VAS_FIELD,VAS_TYPE,VAS_SIZE,VAS_MATNR,MSR_BUOM,VAS_DESCRIPTION) "
                                    cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(R2!PO_Number.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!PO_Item.ToString()) & "' "
                                    cmdstring &= vbCrLf & "," & Val(R2!PO_Size_Index.ToString()) & " "
                                    cmdstring &= vbCrLf & "," & Val(R2!SO_Item_Sequence_Number.ToString()) & " "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!SO_Number.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!SO_Item.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!VAS_CODE.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!VAS_FIELD.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!VAS_TYPE.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!VAS_SIZE.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!VAS_MATNR.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!MSR_BUOM.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!VAS_DESCRIPTION.ToString()) & "' "

                                    If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                        State = False
                                        Exit For

                                    End If

                                Next

                            Catch ex As Exception
                                State = False
                            End Try



                        End If

                        If State And Not (OrderItemsText Is Nothing) Then

                            Try
                                For Each R2 As DataRow In OrderItemsText.Select("PO_Number='" & HI.UL.ULF.rpQuoted(PORef) & "'")


                                    ColIdx = 0
                                    cmdstring = ""
                                    cmdstring = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItemsText (PO_Number,PO_Item,Text_Seq,Text_Type,Text_Value,LANGUAGE_ISO,Text_ID) "
                                    cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(R2!PO_Number.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!PO_Item.ToString()) & "' "
                                    cmdstring &= vbCrLf & "," & Val(R2!Text_Seq.ToString()) & " "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Text_Type.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Text_Value.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!LANGUAGE_ISO.ToString()) & "' "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R2!Text_ID.ToString()) & "' "


                                    If HI.Conn.SQLConn.Execute_Tran(cmdstring, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                        State = False
                                        Exit For
                                    End If
                                Next
                            Catch ex As Exception
                                State = False
                            End Try

                        End If


                        If State Then

                            HI.Conn.SQLConn.Tran.Commit()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        Else
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        End If


                    Else
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    End If

                Catch ex As Exception

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                End Try
            Catch ex99 As Exception
                Dim msgx As String = ex99.Message

            End Try


        Next

        Return True

    End Function


    Private Sub ShowData()

        Me.ogdmain.DataSource = Nothing
        Me.ockAll.Checked = False

        Dim cmd As String = ""
        Call ClearColumnGrid()
        Dim OrderHeaders As New DataTable
        Dim OrderItems As New DataTable
        Dim OrderSizes As New DataTable
        Dim OrderItemsVas As New DataTable
        Dim OrderSizesVas As New DataTable
        Dim OrderItemsText As New DataTable

        cmd = " Select * from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderHeaders AS X WITH(NOLOCK)"
        OrderHeaders = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_FHS)
        mogcdt1.DataSource = OrderHeaders
        mogvdt1.BestFitColumns()


        cmd = " Select * from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItems AS X WITH(NOLOCK)"
        OrderItems = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_FHS)
        mogcdt2.DataSource = OrderItems
        mogvdt2.BestFitColumns()

        cmd = " Select * from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderSizes AS X WITH(NOLOCK)"
        OrderSizes = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_FHS)
        mogcdt3.DataSource = OrderSizes
        mogvdt3.BestFitColumns()

        cmd = " Select * from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItemsVas AS X WITH(NOLOCK)"
        OrderItemsVas = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_FHS)
        mogcdt4.DataSource = OrderItemsVas
        mogvdt4.BestFitColumns()

        cmd = " Select * from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderSizesVas AS X WITH(NOLOCK)"
        OrderSizesVas = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_FHS)
        mogcdt5.DataSource = OrderSizesVas
        mogvdt5.BestFitColumns()

        cmd = " Select * from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItemsText AS X WITH(NOLOCK)"
        OrderItemsText = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_FHS)
        mogcdt6.DataSource = OrderItemsText
        mogvdt6.BestFitColumns()


        Dim ds As New DataSet
        Dim dtmain As New DataTable

        cmd = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.USP_GETFHS_DATA "
        HI.Conn.SQLConn.GetDataSet(cmd, Conn.DB.DataBaseName.DB_FHS, ds)

        Try

            dtmain = ds.Tables(0).Copy
            Call SetNewColumn(ds.Tables(1))

        Catch ex As Exception
        End Try

        Me.ogdmain.DataSource = dtmain

        Call SetFilerColumn()

    End Sub


    Private Sub SetNewColumn(dt As DataTable)
        Try
            Dim StrCol As String = dt.Rows(0)!FTColumn.ToString

            With Me.ogvmain
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
                            .Name = "Size" & R.Replace(" ", "_").Replace("[", "").Replace("]", "")
                            .Caption = R.Replace("[", "").Replace("]", "")
                            .Visible = True

                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                            If R.Contains("Price") Then
                                .DisplayFormat.FormatString = "{0:n2}"
                            Else

                                .DisplayFormat.FormatString = "{0:n0}"
                            End If



                            With .OptionsColumn
                                .AllowMove = False
                                .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                .AllowSort = DevExpress.Utils.DefaultBoolean.False

                                .AllowEdit = False
                                .ReadOnly = True
                            End With


                            If R.Contains("Price") Then
                                .Width = 70
                            Else
                                .Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                                .SummaryItem.DisplayFormat = "{0:n0}"
                                .Width = 50
                            End If


                        End With

                        .Columns.Add(ColG)
                    Next

                End If


                Dim ColG2 As New DevExpress.XtraGrid.Columns.GridColumn
                With ColG2
                    .FieldName = "FTItemNote"
                    .Name = "FTItemNote"
                    .Caption = "Item Note"
                    .Visible = True
                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                    .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                    With .OptionsColumn
                        .AllowMove = False
                        .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .AllowEdit = False
                        .ReadOnly = True
                    End With
                    .Width = 300
                End With

                .Columns.Add(ColG2)
                .EndInit()
            End With


            Try
                With _FactoryHubListOrderNo.ogvmain
                    .BeginInit()



                    For I As Integer = .Columns.Count - 1 To 0 Step -1
                        Select Case Microsoft.VisualBasic.Left(.Columns(I).Name.ToString, 4).ToUpper
                            Case "CFIX".ToUpper
                            Case Else

                                .Columns.Remove(.Columns(I))
                        End Select
                    Next


                    If StrCol <> "" Then

                        For Each R As String In StrCol.Split(",")


                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG

                                .FieldName = R.Replace("[", "").Replace("]", "")
                                .Name = "Size" & R.Replace(" ", "_").Replace("[", "").Replace("]", "")
                                .Caption = R.Replace("[", "").Replace("]", "")
                                .Visible = True

                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                                If R.Contains("Price") Then
                                    .DisplayFormat.FormatString = "{0:n2}"
                                Else

                                    .DisplayFormat.FormatString = "{0:n0}"
                                End If


                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False
                                    .ReadOnly = True
                                End With


                                If R.Contains("Price") Then
                                    .Width = 70
                                Else
                                    .Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                                    .SummaryItem.DisplayFormat = "{0:n0}"
                                    .Width = 50
                                End If


                            End With

                            .Columns.Add(ColG)
                        Next

                    End If

                    Dim ColG2 As New DevExpress.XtraGrid.Columns.GridColumn
                    With ColG2
                        .FieldName = "FTItemNote"
                        .Name = "FTItemNote"
                        .Caption = "Item Note"
                        .Visible = True
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                        .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                        With .OptionsColumn
                            .AllowMove = False
                            .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                            .AllowSort = DevExpress.Utils.DefaultBoolean.False
                            .AllowEdit = False
                            .ReadOnly = True
                        End With
                        .Width = 300
                    End With

                    .Columns.Add(ColG2)
                    .EndInit()
                End With
            Catch ex As Exception

            End Try


        Catch ex As Exception

        End Try



    End Sub
    Private Sub SetFilerColumn()
        Try

            For Each c As GridColumn In ogvmain.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next


            For Each c As GridColumn In mogvdt1.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next

            For Each c As GridColumn In mogvdt2.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next

            For Each c As GridColumn In mogvdt3.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next

            For Each c As GridColumn In mogvdt4.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next

            For Each c As GridColumn In mogvdt5.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next

            For Each c As GridColumn In mogvdt6.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next

        Catch ex As Exception
        End Try

    End Sub

    Private Function GetWebServiceOrdersGetPDF(FactoryCode As String, OrderNumber As String) As Boolean
        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)

        Dim PageCount As Integer = 0
        Dim urlEndPoint As String = "http://192.168.99.156/OLLIe/OLLIe.svc/Orders" '"http://192.168.99.156/OLLIe/OLLIe.svc?wsdl"
        Dim soapAction As String = "http://tempuri.org/IOrders/OrdersGetPDF"
        ' Refer to the documentation for more information on how to get the client id/secret
        ' urlEndPoint = soapAction
        ' Refer to the documentation for more information on how to get the tokens
        Dim accessToken As String = ""
        Dim jsonResponseText As String = ""


        Dim FHubXML As New FactoryHub.FactoryHubXML()
        Dim json_data As String = FHubXML.OrdersGetPDFXMLRequest(FactoryCode, OrderNumber)

        Dim data As String = json_data
        ' Dim url As String = "http://192.168.99.156/OLLIe/OLLIe.svc?wsdl"
        Dim responsestring As String = ""

        Try
            Dim myReq As HttpWebRequest = WebRequest.Create(urlEndPoint)
            Dim encoding As New ASCIIEncoding
            Dim buffer() As Byte = encoding.GetBytes(json_data)
            Dim response As String = ""

            myReq.AllowWriteStreamBuffering = False
            myReq.Method = "POST"
            myReq.ContentType = "text/xml;charset=UTF-8"
            myReq.Headers.Add("SOAPAction", soapAction)
            myReq.ContentLength = buffer.Length

            Dim post As Stream = myReq.GetRequestStream

            post.Write(buffer, 0, buffer.Length)
            post.Close()

            Dim myResponse As HttpWebResponse = myReq.GetResponse
            Dim responsedata As Stream = myResponse.GetResponseStream
            Dim responsereader As New StreamReader(responsedata)

            response = responsereader.ReadToEnd

            Dim theDataSet As New DataSet()
            Try

                Dim theReader As New StringReader(response)

                theDataSet.ReadXml(theReader)


                If response <> "" Then

                    If theDataSet.Tables.IndexOf("") >= 0 Then

                    End If

                    theDataSet.Dispose()
                Else
                    theDataSet.Dispose()
                    Return False
                End If
            Catch ex As Exception
                theDataSet.Dispose()
                Return False
            End Try

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function


    Private Sub ocmclearclsr_Click(sender As Object, e As EventArgs) Handles ocmclearclsr.Click

    End Sub

    Private Sub ClearColumnGrid()

        Try
            With ogvmain
                .BeginInit()


                For Each c As GridColumn In .Columns
                    Select Case c.FieldName
                        Case "FTSelect", "FTNikePO", "PO_Doc_Date", "PO_Number", "PO_Ref", "PO_Group", "Currency_Type", "Ship_Via_Instructions", "BUY_SEASON", "BUY_YEAR", "BUY_GROUP", "Factory_Vendor_Code", "Vendor_Location_Code_MCO", "PO_Item", "Material_Number" _
                            , "Material_Description", "Plant", "Nike_Division_Code", "UOM", "Mode_Code", "Mode_Code_Description", "OGAC_Date", "GAC_Date", "GAC_Reason_Code", "Material_Dev_Code", "Silhhouette_Code", "Gender_Age_Code", "SO_NUMBER", "SO_ITEM", "Color_Combo_Name", "Color_Combo_ShortName", "MSRP_US" _
                            , "FTStatenew", "FTStateAdd", "FTStateDeduct", "FTStateinfo", "Quantity", "UCC_NAME1", "Ship_To_Account"

                        Case Else
                            .Columns.Remove(c)
                    End Select

                Next


                .EndInit()
            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Function CheckWriteFile() As Boolean

        Try

            If (Not System.IO.Directory.Exists(PathFileXML)) Then
                System.IO.Directory.CreateDirectory(PathFileXML)
            End If

            If (Not System.IO.Directory.Exists(PathFileXML & "\TestXML")) Then
                System.IO.Directory.CreateDirectory(PathFileXML & "\TestXML")
            End If
            System.IO.Directory.Delete(PathFileXML & "\TestXML")


            If (Not System.IO.Directory.Exists(PathFileXMLCompleted)) Then
                System.IO.Directory.CreateDirectory(PathFileXMLCompleted)
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click

        FNHSysMerTeamId.Text = "MP"
        If CheckDataBaseFHS() = False Then Exit Sub
        If CheckWriteFile() Then

            Dim Spls As New HI.TL.SplashScreen("Loading.... please wait")

            Me.ogdmain.DataSource = Nothing
            mogcdt1.DataSource = Nothing
            mogcdt2.DataSource = Nothing
            mogcdt3.DataSource = Nothing
            mogcdt4.DataSource = Nothing
            mogcdt5.DataSource = Nothing
            mogcdt6.DataSource = Nothing


            For Each Pgm As String In StrAllPgm.Split(",")
                Spls.UpdateInformation("Loading  " & Pgm & ".... please wait")

                Call GetWebServiceOrdersDataGetUnAcknowledged(Pgm)
            Next

            MappingSuplierData()
            ' GetTokenData()
            Call ShowData()

            Me.otb.SelectedTabPageIndex = 0
            Spls.Close()
        Else
            HI.MG.ShowMsg.mInfo("ไม่สามารถเข้าถึงที่เก็บ File ได้ !!!!", 1909270015, Me.Text)
        End If

    End Sub

    Private Sub ocmexit_Click_1(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Function Verifydata() As Boolean
        If FNHSysBuyId.Text <> "" Then
            If FNHSysBuyGrpId.Text <> "" Then
                If FNHSysMerTeamId.Text <> "" Then

                    Return True

                Else

                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysMerTeamId_lbl.Text)
                    FNHSysMerTeamId.Focus()

                End If

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysBuyGrpId_lbl.Text)
                FNHSysBuyGrpId.Focus()
            End If

        Else

            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysBuyId_lbl.Text)
            FNHSysBuyId.Focus()

        End If


        Return False
    End Function
    Private Sub ocmImportOrder_Click(sender As Object, e As EventArgs) Handles ocmImportOrder.Click
        If CheckDataBaseFHS() = False Then Exit Sub
        If Verifydata() Then
            If MappingSuplierData() Then
                If Not (Me.ogdmain.DataSource Is Nothing) Then

                    Dim dt As New DataTable
                    With CType(Me.ogdmain.DataSource, DataTable)
                        .AcceptChanges()
                        dt = .Copy
                    End With

                    Dim tSql As String = ""
                    Dim cmdstring As String = ""
                    If dt.Select("FTSelect='1'").Length > 0 Then

                        Dim Spls As New HI.TL.SplashScreen("Importing....")
                        cmdstring = "delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPOrderHeadersImport where FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                        HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_FHS)

                        For Each R As DataRow In dt.Select("FTSelect='1'")

                            cmdstring = "insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPOrderHeadersImport (FTUserName,PO_Number,PO_Item,FTNikePO) "
                            cmdstring &= vbCrLf & " select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!PO_Number.ToString) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!PO_Item.ToString) & "'"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTNikePO.ToString) & "'"

                            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_FHS)

                        Next


                        cmdstring = " EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.USP_INSERT_TMP_FORIMPORT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Val(FNHSysMerTeamId.Properties.Tag.ToString) & ""
                        HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_FHS)


                        Dim dtnostyle As DataTable
                        cmdstring = " SELECT X.FTStyle "
                        cmdstring &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTImportOrderTemp AS X "
                        cmdstring &= vbCrLf & "  OUTER APPLY (SELECT TOP 1  ST.FTStyleCode  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH(NOLOCK) WHERE ST.FTStyleCode =X.FTStyle   ) AS ST "
                        cmdstring &= vbCrLf & " WHERE X.FTUserLogin ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AND ST.FTStyleCode IS NULL "

                        dtnostyle = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)


                        If dtnostyle.Rows.Count > 0 Then

                            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Style Master ไม่สามารถทำการ Import ได้", 1455877494, Me.Text,, MessageBoxIcon.Warning)

                            With stylepopup
                                .ogcstyle.DataSource = dtnostyle
                                .ShowDialog()
                            End With

                            Exit Sub
                        End If


                        cmdstring = " UPDATE X SET X.FNHSysBuyGrpId=" & Val(FNHSysBuyGrpId.Properties.Tag.ToString()) & ",X.FTBuyGrpNameDesc='" & HI.UL.ULF.rpQuoted(FNHSysBuyGrpId.Text.Trim()) & "' "
                        cmdstring &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTImportOrderTemp AS X "
                        cmdstring &= vbCrLf & " WHERE X.FTUserLogin ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
                        HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)


                        tSql = " EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_IMPORTORDER_UPDATEGRP '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

                        HI.Conn.SQLConn.ExecuteNonQuery(tSql, Conn.DB.DataBaseName.DB_FHS)

                        Dim StateImport As Boolean = False
                        Dim StateImportNewData As Boolean = False

                        If FNFHSImportOrderType.SelectedIndex = 2 Then
                            StateImport = True
                            StateImportNewData = False
                        Else
                            StateImportNewData = True

                            StateImport = W_PRCbImportFactoryOrder(Spls, (FNFHSImportOrderType.SelectedIndex = 0))
                        End If


                        If StateImport Then

                            '...clear temp after process import order complete
                            '---------------------------------------------------------------------------------------------------------------------------------------------------------
                            '  If Not System.Diagnostics.Debugger.IsAttached = True Then
                            Application.DoEvents()

                            Spls.UpdateInformation("Finishing Generate Order .....Please Wait")

                            Dim dtOrderNo As New DataTable

                            If StateImportNewData Then

                                tSql = "SELECT   FTPONo, FTGenerateOrderNo "
                                tSql &= Environment.NewLine & " FROM           [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].. TMERTImportOrderTemp "
                                tSql &= Environment.NewLine & " WHERE        (FTUserLogin =  N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') "
                                tSql &= Environment.NewLine & " GROUP BY FTPONo, FTGenerateOrderNo "

                                dtOrderNo = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_MERCHAN)

                            End If


                            tSql = " EXEC  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FHS) & "].. USP_INSERT_MOVE_CFMIMPORT  N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            HI.Conn.SQLConn.ExecuteOnly(tSql, HI.Conn.DB.DataBaseName.DB_FHS)

                            Try

                                If StateImportNewData Then
                                    tSql = ""

                                    tSql = "UPDATE A"
                                    tSql &= Environment.NewLine & "Set A.FPOrderImage1 = C.FPStyleImage1,"
                                    tSql &= Environment.NewLine & "    A.FPOrderImage2 = C.FPStyleImage2,"
                                    tSql &= Environment.NewLine & "    A.FPOrderImage3 = C.FPStyleImage3,"
                                    tSql &= Environment.NewLine & "    A.FPOrderImage4 = C.FPStyleImage4"
                                    tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] As A LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] As B On A.FTOrderNo = B.FTGenerateOrderNo"
                                    tSql &= Environment.NewLine & "                                        LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] As C On A.FNHSysStyleId = C.FNHSysStyleId"
                                    tSql &= Environment.NewLine & "WHERE B.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                                    If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                                    Else
                                    End If

                                    Dim dtstss As DataTable
                                    cmdstring = "  SELECT  DISTINCT C.FTOrderNo  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A "
                                    cmdstring &= Environment.NewLine & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS C WITH(NOLOCK) ON A.FTGenerateOrderNo = C.FTOrderNo"
                                    cmdstring &= Environment.NewLine & "     WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                                    dtstss = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                                    For Each R As DataRow In dtstss.Rows

                                        cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_GEN_BREAKDOWN_SHIPDESINATION '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'"
                                        HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                                    Next

                                End If

                            Catch ex As Exception
                            End Try

                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTImportOrderUpdExtraQtyTemp] AS A"
                            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            End If

                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A"
                            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            End If

                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A"
                            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            End If


                            Call ShowData()

                            Me.otb.SelectedTabPageIndex = 0

                            Spls.Close()
                            Select Case HI.ST.Lang.Language

                                Case HI.ST.Lang.eLang.TH
                                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "นำเข้าข้อมูลรายการใบสั่งผลิต เรียบร้อยแล้ว...")

                                Case Else
                                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, "Import Factory Hub Order No. Complete...")
                            End Select

                            If StateImportNewData Then


                                Dim dtmain As New DataTable
                                dtmain = dt.Select("FTSelect='1'").CopyToDataTable



                                For Each R As DataRow In dtmain.Rows


                                    Try
                                        If dtOrderNo.Select("FTPONo='" & HI.UL.ULF.rpQuoted(R!FTNikePO.ToString()) & "'").Length > 0 Then

                                            For Each rx As DataRow In dtOrderNo.Select("FTPONo='" & HI.UL.ULF.rpQuoted(R!FTNikePO.ToString()) & "'")

                                                R!FTOrderNo = rx!FTGenerateOrderNo.ToString

                                            Next

                                        End If

                                    Catch ex As Exception

                                    End Try

                                Next

                                With _FactoryHubListOrderNo
                                    .ogdmain.DataSource = dtmain
                                    .ShowDialog()
                                End With

                            End If

                        Else

                            ' If Not System.Diagnostics.Debugger.IsAttached = True Then
                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTImportOrderUpdExtraQtyTemp] AS A"
                            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            End If

                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] AS A"
                            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            End If

                            tSql = ""
                            tSql = "DELETE A"
                            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A"
                            tSql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';"

                            If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                            End If

                            ' Else
                            '...developer test temp data
                            ' End If
                            Spls.Close()

                            MsgBox("พบปัญหาในการบันทึกรายการนำเข้าข้อมูลใบสั่งผลิต !!!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, My.Application.Info.Title)

                        End If


                        cmdstring = "delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.TTMPOrderHeadersImport where FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                        HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_FHS)

                        Spls.Close()

                    Else

                        HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกรายการที่ต้องทำการ Import Order !!!", 1910214578, Me.Text,, MessageBoxIcon.Warning)

                    End If

                    dt.Dispose()

                End If
            End If

        End If

    End Sub


    Private Function W_PRCbImportFactoryOrder(_Spls As HI.TL.SplashScreen, Optional statecaltest As Boolean = True) As Boolean
        '...last modify 2014/12/19 when drop field Amount, Qty TMERTOrder_BreakDown, TMERTOrder, TMERTOrderSub
        Dim tsql As String = ""
        Dim _bImportComplete As Boolean = False

        Dim tMsgSplash As String = ""

        Try
            Dim nFNHSysCustId As Integer
            Dim nFNHSysCmpRunId As Integer
            Dim nFNHSysBuyId As Integer
            Dim nFNHSysVenderPramId As Integer
            Dim _FNImportOrderType As Integer = 0


            _FNImportOrderType = 0

            nFNHSysCustId = 1310210002
            nFNHSysCmpRunId = 1406160009

            nFNHSysBuyId = Val(Me.FNHSysBuyId.Properties.Tag)


            Dim oDBdtImport As System.Data.DataTable

            tsql = ""
            tsql = "SELECT  A.FNHSysMerTeamId, A.FTPONo, A.FTPOTrading, A.FTPOCreateDate, A.FTOrderDate"
            tsql &= Environment.NewLine & "         , " & nFNHSysBuyId & " AS FNHSysBuyId, " & nFNHSysCmpRunId & " AS FNHSysCmpRunId, " & nFNHSysCustId & " AS FNHSysCustId"
            tsql &= Environment.NewLine & "         , N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTOrderBy, 0 AS FNOrderType, 0 AS FNJobState"
            tsql &= Environment.NewLine & "         , A.FTStyle, A.FDShipDate, A.FDShipDateOrginal, A.FNHSysBuyGrpId, A.FNHSysGenderId, A.FNHSysProdTypeId"
            tsql &= Environment.NewLine & "         , A.FNHSysPlantId, A.FTYear, A.FNHSysMainCategoryId, A.FTPlanningSeason, A.FNHSysCountryId, A.FNHSysBuyerId"
            tsql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
            tsql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            tsql &= Environment.NewLine & "      AND A.FNRowImport = (SELECT MAX(L1.FNRowImport)"
            tsql &= Environment.NewLine & "                           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS L1 WITH(NOLOCK)"
            tsql &= Environment.NewLine & "                           WHERE L1.FTUserLogin = A.FTUserLogin"
            tsql &= Environment.NewLine & "                                 AND L1.FTPONo = A.FTPONO"
            tsql &= Environment.NewLine & "                           GROUP BY L1.FTPONo)"
            tsql &= Environment.NewLine & "ORDER BY B.FNHSysMerTeamId ASC, A.FNRowImport ASC;"

            oDBdtImport = HI.Conn.SQLConn.GetDataTable(tsql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If Not oDBdtImport Is Nothing AndAlso oDBdtImport.Rows.Count > 0 Then
                '...generate facotry order no.
                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                tsql = ""
                tsql = " DECLARE @DBName NVARCHAR(30);"
                tsql &= Environment.NewLine & " DECLARE @TblName NVARCHAR(30);"
                tsql &= Environment.NewLine & " DECLARE @DocType NVARCHAR(30);"
                tsql &= Environment.NewLine & " DECLARE @GetFotmat NVARCHAR(30);"
                tsql &= Environment.NewLine & " DECLARE @AddPrefix NVARCHAR(30);"
                tsql &= Environment.NewLine & " SET @DBName = N'" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "';"
                tsql &= Environment.NewLine & " SET @TblName = N'TMERTOrder';"
                tsql &= Environment.NewLine & " SET @DocType = '" & _FNImportOrderType.ToString & "';"
                tsql &= Environment.NewLine & " SET @GetFotmat = '';"
                tsql &= Environment.NewLine & " SET @AddPrefix = N'" & HI.UL.ULF.rpQuoted("NI") & "';"
                tsql &= Environment.NewLine & " DECLARE @tblSrcConfigDoc AS TABLE(FTRunNo NVARCHAR(30), FTRunStr NVARCHAR(30), FNRunning INT, FNRunningNoMax INT);"
                tsql &= Environment.NewLine & " INSERT INTO @tblSrcConfigDoc(FTRunNo, FTRunStr, FNRunning, FNRunningNoMax)"
                tsql &= Environment.NewLine & " EXEC SP_GET_FACTORY_ORDERNO_MAX @DBName, @TblName, @DocType, @GetFotmat,@AddPrefix;"
                tsql &= Environment.NewLine & " DECLARE @FTRunNo        AS NVARCHAR(30);"
                tsql &= Environment.NewLine & " DECLARE @FTRunStr       AS NVARCHAR(30);"
                tsql &= Environment.NewLine & " DECLARE @FNRunning      AS INT;"
                tsql &= Environment.NewLine & " DECLARE @FNRunningNoMax AS INT;"
                tsql &= Environment.NewLine & " SELECT @FTRunNo = A.FTRunNo, @FTRunStr = A.FTRunStr, @FNRunning = A.FNRunning, @FNRunningNoMax = A.FNRunningNoMax"
                tsql &= Environment.NewLine & " FROM @tblSrcConfigDoc AS A;"
                tsql &= Environment.NewLine & " --SET @FNRunningNoMax = @FNRunningNoMax + 1;"
                tsql &= Environment.NewLine & " create table #Tab("
                tsql &= Environment.NewLine & " [FTInsUser] [nvarchar](50) NULL,"
                tsql &= Environment.NewLine & " [FDInsDate] [varchar](10) NULL,"
                tsql &= Environment.NewLine & " [FTInsTime] [varchar](8) NULL,"
                tsql &= Environment.NewLine & " [FTUpdUser] [nvarchar](50) NULL,"
                tsql &= Environment.NewLine & " [FDUpdDate] [varchar](10) NULL,"
                tsql &= Environment.NewLine & " [FTUpdTime] [varchar](8) NULL,"
                tsql &= Environment.NewLine & " [FTOrderNo] [nvarchar](30) NOT NULL,"
                tsql &= Environment.NewLine & " [FDOrderDate] [nvarchar](10) NULL,"
                tsql &= Environment.NewLine & " [FTOrderBy] [nvarchar](50) NULL,"
                tsql &= Environment.NewLine & " [FNOrderType] [int] NULL,"
                tsql &= Environment.NewLine & " [FNHSysCmpId] [int] NULL,"
                tsql &= Environment.NewLine & " [FNHSysCmpRunId] [int] NULL,"
                tsql &= Environment.NewLine & " [FNHSysStyleId] [int] NULL,"
                tsql &= Environment.NewLine & " [FTPORef] [nvarchar](50) NULL,"
                tsql &= Environment.NewLine & " [FNHSysCustId] [int] NULL,"
                tsql &= Environment.NewLine & " [FNHSysAgencyId] [int] NULL,"
                tsql &= Environment.NewLine & " [FNHSysProdTypeId] [int] NULL,"
                tsql &= Environment.NewLine & " [FNHSysBuyerId] [int] NULL,"
                tsql &= Environment.NewLine & " [FTMainMaterial] [nvarchar](500) NULL,"
                tsql &= Environment.NewLine & " [FTCombination] [nvarchar](500) NULL,"
                tsql &= Environment.NewLine & " [FTRemark] [nvarchar](1000) NULL,"
                tsql &= Environment.NewLine & " [FTStateOrderApp] [nvarchar](1) NULL,"
                tsql &= Environment.NewLine & " [FTAppBy] [nvarchar](50) NULL,"
                tsql &= Environment.NewLine & " [FDAppDate] [varchar](10) NULL,"
                tsql &= Environment.NewLine & " [FTAppTime] [varchar](8) NULL,"
                tsql &= Environment.NewLine & " [FNJobState] [int] NULL,"
                tsql &= Environment.NewLine & " [FTStateBy] [nvarchar](50) NULL,"
                tsql &= Environment.NewLine & " [FDStateDate] [varchar](10) NULL,"
                tsql &= Environment.NewLine & " [FTStateTime] [varchar](8) NULL,"
                tsql &= Environment.NewLine & " [FTImage1] [nvarchar](100) NULL,"
                tsql &= Environment.NewLine & " [FTImage2] [nvarchar](100) NULL,"
                tsql &= Environment.NewLine & " [FTImage3] [nvarchar](100) NULL,"
                tsql &= Environment.NewLine & " [FTImage4] [nvarchar](100) NULL,"
                tsql &= Environment.NewLine & " [FNHSysBrandId] [int] NULL,"
                tsql &= Environment.NewLine & " [FNHSysBuyId] [int] NULL,"
                tsql &= Environment.NewLine & " [FTCancelAppBy] [nvarchar](50) NULL,"
                tsql &= Environment.NewLine & " [FDCancelAppDate] [varchar](10) NULL,"
                tsql &= Environment.NewLine & " [FDCancelAppTime] [varchar](8) NULL,"
                tsql &= Environment.NewLine & " [FTCancelAppRemark] [nvarchar](500) NULL,"
                tsql &= Environment.NewLine & " [FTPOTradingCo] [nvarchar](30) NULL,"
                tsql &= Environment.NewLine & " [FTPOItem] [nvarchar](30) NULL,"
                tsql &= Environment.NewLine & " [FTPOCreateDate] [varchar](10) NULL,"
                tsql &= Environment.NewLine & " [FNHSysMerTeamId] [int] NULL,"
                tsql &= Environment.NewLine & " [FNHSysPlantId] [int] NULL,"
                tsql &= Environment.NewLine & " [FNHSysBuyGrpId] [int] NULL,"
                tsql &= Environment.NewLine & " [FNHSysMainCategoryId] [int] NULL,"
                tsql &= Environment.NewLine & " [FNHSysVenderPramId] [int] NULL,"
                tsql &= Environment.NewLine & " [FTOrderCreateStatus] [nvarchar](1) NULL,"
                tsql &= Environment.NewLine & " [FTImportUser] [nvarchar](50) NULL,"
                tsql &= Environment.NewLine & " [FDImportDate] [nvarchar](10) NULL,"
                tsql &= Environment.NewLine & " [FTImportTime] [nvarchar](8) NULL,"
                tsql &= Environment.NewLine & " [FNRowImport] [Int],"
                tsql &= Environment.NewLine & " [FTStyle] [nvarchar](30) NULL,"
                tsql &= Environment.NewLine & " [FNHSysSeasonId] [int] NULL,"
                tsql &= Environment.NewLine & " [FNHSysStyleIdRef] [int] NULL,"
                tsql &= Environment.NewLine & " [FNHSysProvinceId] [int] NULL,"
                tsql &= Environment.NewLine & " [FTSubPgm] [nvarchar](200) NULL,"
                tsql &= Environment.NewLine & " [FTStateSet] [varchar](1) NULL,"
                tsql &= Environment.NewLine & " [DFCmpID] [int] NULL"
                tsql &= Environment.NewLine & "  )"
                tsql &= Environment.NewLine & " INSERT INTO  #Tab"
                tsql &= Environment.NewLine & "                              ([FTInsUser],[FDInsDate],[FTInsTime]"
                tsql &= Environment.NewLine & "                              ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tsql &= Environment.NewLine & "                              ,[FTOrderNo],[FDOrderDate],[FTOrderBy],[FNOrderType]"
                tsql &= Environment.NewLine & "                              ,[FNHSysCmpId],[FNHSysCmpRunId],[FNHSysStyleId],[FTPORef]"
                tsql &= Environment.NewLine & "                              ,[FNHSysCustId],[FNHSysAgencyId],[FNHSysProdTypeId],[FNHSysBuyerId]"
                tsql &= Environment.NewLine & "                              ,[FTMainMaterial],[FTCombination],[FTRemark]"
                tsql &= Environment.NewLine & "                              ,[FTStateOrderApp],[FTAppBy],[FDAppDate],[FTAppTime]"
                tsql &= Environment.NewLine & "                              ,[FNJobState],[FTStateBy],[FDStateDate],[FTStateTime]"
                tsql &= Environment.NewLine & "                              ,[FTImage1],[FTImage2],[FTImage3],[FTImage4]"
                tsql &= Environment.NewLine & "                              ,[FNHSysBrandId],[FNHSysBuyId],[FTCancelAppBy],[FDCancelAppDate]"
                tsql &= Environment.NewLine & "                              ,[FDCancelAppTime],[FTCancelAppRemark],[FTPOTradingCo],[FTPOItem]"
                tsql &= Environment.NewLine & "                              ,[FTPOCreateDate],[FNHSysMerTeamId],[FNHSysPlantId],[FNHSysBuyGrpId]"
                tsql &= Environment.NewLine & "                              ,[FNHSysMainCategoryId],[FNHSysVenderPramId],[FTOrderCreateStatus],[FTImportUser],[FDImportDate],[FTImportTime],[FNRowImport],[FTStyle],[FNHSysSeasonId],[FNHSysStyleIdRef],[FNHSysProvinceId],[FTSubPgm],[FTStateSet],[DFCmpID])"
                tsql &= Environment.NewLine & "SELECT NULL AS FTInsUser, CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                tsql &= Environment.NewLine & "     , NULL AS FTUpdUser, NULL AS FDUpdDate, NULL AS FTUpdTime"
                tsql &= Environment.NewLine & "     , @FTRunNo + (RIGHT(@FTRunStr, @FNRunning - LEN(CONVERT(VARCHAR(30) , (ROW_NUMBER() OVER(ORDER BY B.FTMerTeamCode ASC,A.FTStyle ASC,A.FTPONO ASC) + @FNRunningNoMax)))) + CONVERT(VARCHAR(30) , (ROW_NUMBER() OVER(ORDER BY B.FTMerTeamCode ASC,A.FTStyle ASC,A.FTPONO ASC) + @FNRunningNoMax))) AS FTOrderNo"
                tsql &= Environment.NewLine & "     , A.FTOrderDate, NULL AS FTOrderBy, " & _FNImportOrderType.ToString & " AS FNOrderType"
                tsql &= Environment.NewLine & "     , NULL AS FNHSysCmpId"
                tsql &= Environment.NewLine & ", " & nFNHSysCmpRunId & " AS FNHSysCmpRunId"
                tsql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS L1 WITH(NOLOCK) WHERE L1.FTStyleCode = A.FTStyle), NULL) AS FNHSysStyleId"
                tsql &= Environment.NewLine & "     , A.FTPONo, " & nFNHSysCustId & " AS FNHSysCustId"
                tsql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysAgencyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMAgency] AS L1 WITH(NOLOCK) WHERE L1.FTAgencyCode = N'-'),NULL) AS FNHSysAgencyId"
                tsql &= Environment.NewLine & "     , A.FNHSysProdTypeId, A.FNHSysBuyerId, NULL AS FTMainMaterial, NULL AS FTCombination, NULL AS FTRemark"
                tsql &= Environment.NewLine & "     , NULL AS FTStateOrderApp, NULL AS FTAppBy, NULL AS FDAppDate, NULL AS FTAppTime"
                tsql &= Environment.NewLine & "     , 0 AS FNJobState, NULL AS FTStateBy, NULL AS FDStateDate, NULL AS FTStateTime"
                tsql &= Environment.NewLine & "     , NULL AS FTImage1, NULL AS FTImage2, NULL AS FTImage3, NULL AS FTImage4"
                tsql &= Environment.NewLine & "     , ISNULL((SELECT L1.FNHSysBrandId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMBrand] AS L1 WITH(NOLOCK) WHERE L1.FTBrandCode = N'-'),NULL) AS FNHSysBrandId"
                tsql &= Environment.NewLine & "     , " & nFNHSysBuyId & " AS FNHSysBuyId"
                tsql &= Environment.NewLine & "     , NULL AS FTCancelAppBy, NULL AS FDCancelAppDate, NULL AS FDCancelAppTime, NULL AS FTCancelAppRemark"
                tsql &= Environment.NewLine & "     , A.FTPOTrading, NULL AS FTPOItem, A.FTPOCreateDate"
                tsql &= Environment.NewLine & "     , A.FNHSysMerTeamId, A.FNHSysPlantId, A.FNHSysBuyGrpId"
                tsql &= Environment.NewLine & "     , A.FNHSysMainCategoryId, A.FNHSysVenderPramId"
                tsql &= Environment.NewLine & "     , N'Y' AS FTOrderCreateStatus, N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTImportUser , CONVERT(VARCHAR(10),GETDATE(),111) AS FDImportDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTImportTime,A.FNRowImport,A.FTStyle "
                tsql &= Environment.NewLine & "     , ISNULL((SELECT  TOP 1  L1.FNHSysSeasonId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMSeason] AS L1 WITH(NOLOCK) WHERE L1.FTSeasonCode = A.FTPlanningSeason + RIGHT(A.FTYear,2)), 0) AS FNHSysSeasonId"
                tsql &= Environment.NewLine & "     , ISNULL((SELECT  TOP 1 L1.FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS L1 WITH(NOLOCK) WHERE LEFT(L1.FTStyleCode,LEN(A.FTStyle)) = A.FTStyle), 0) AS FNHSysStyleIdRef,A.FNHSysProvinceId" ',A.FTSubPgm


                tsql &= Environment.NewLine & "  ,ISNULL((SELECT TOP 1 FTSubPgm  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS XA2 WHERE  XA2.FTUserLogin = A.FTUserLogin AND  XA2.FTPONo = A.FTPONO  AND XA2.FTStyle = A.FTStyle AND ISNULL(XA2.FTSubPgm,'') <>'' ORDER BY XA2.FTSubPgmSeq),'') AS FTSubPgm "
                tsql &= Environment.NewLine & "     , ISNULL((SELECT  TOP 1 L1.FTStateStyleSet FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS L1 WITH(NOLOCK) WHERE L1.FTStyleCode = A.FTStyle), '0') AS FTStateSet"

                tsql &= Environment.NewLine & " ,ISNULL(DFCX.DFCmpID,0) AS DFCmpID "
                tsql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] As A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] As B With(NOLOCK) On A.FNHSysMerTeamId = B.FNHSysMerTeamId"

                tsql &= Environment.NewLine & "  OUTER APPLY (  Select TOP 1  CASE WHEN ISNUMERIC(XXXX.FTCfgData) = 1 THEN Convert(int,XXXX.FTCfgData)   ELSE  0  END AS DFCmpID   "
                tsql &= Environment.NewLine & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig  AS XXXX WITH(NOLOCK) "
                tsql &= Environment.NewLine & "   WHERE  (XXXX.FTCfgName =A.FTVenderPramCode)"

                tsql &= Environment.NewLine & "   ) AS DFCX "

                tsql &= Environment.NewLine & "WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tsql &= Environment.NewLine & "      AND A.FNRowImport = (SELECT MAX(L1.FNRowImport)"
                tsql &= Environment.NewLine & "                           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS L1 WITH(NOLOCK)"
                tsql &= Environment.NewLine & "                           WHERE L1.FTUserLogin = A.FTUserLogin"
                tsql &= Environment.NewLine & "                                 AND L1.FTPONo = A.FTPONO"
                tsql &= Environment.NewLine & "                                 AND L1.FTStyle = A.FTStyle"
                tsql &= Environment.NewLine & "                           GROUP BY L1.FTPONo)"
                tsql &= Environment.NewLine & " ORDER BY B.FTMerTeamCode ASC,A.FTStyle ASC,A.FTPONO ASC;"
                tsql &= Environment.NewLine & " UPDATE A"
                tsql &= Environment.NewLine & " SET FTGenerateOrderNo = ISNULL((SELECT TOP 1 ISNULL(FTOrderNo,'')"
                tsql &= Environment.NewLine & "                                FROM #Tab"
                tsql &= Environment.NewLine & "                                WHERE FTPORef = A.FTPONO AND FTStyle = A.FTStyle), '')"
                tsql &= Environment.NewLine & ",FTStateSet = ISNULL((SELECT TOP 1 ISNULL(FTStateSet,'')"
                tsql &= Environment.NewLine & "                                FROM #Tab"
                tsql &= Environment.NewLine & "                                WHERE FTPORef = A.FTPONO AND FTStyle = A.FTStyle), '')"
                tsql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"


                tsql &= Environment.NewLine & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

                tsql &= Environment.NewLine & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrder]"
                tsql &= Environment.NewLine & "                              ([FTInsUser],[FDInsDate],[FTInsTime]"
                tsql &= Environment.NewLine & "                              ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tsql &= Environment.NewLine & "                              ,[FTOrderNo],[FDOrderDate],[FTOrderBy],[FNOrderType]"
                tsql &= Environment.NewLine & "                              ,[FNHSysCmpId],[FNHSysCmpRunId],[FNHSysStyleId],[FTPORef]"
                tsql &= Environment.NewLine & "                              ,[FNHSysCustId],[FNHSysAgencyId],[FNHSysProdTypeId],[FNHSysBuyerId]"
                tsql &= Environment.NewLine & "                              ,[FTMainMaterial],[FTCombination],[FTRemark]"
                tsql &= Environment.NewLine & "                              ,[FTStateOrderApp],[FTAppBy],[FDAppDate],[FTAppTime]"
                tsql &= Environment.NewLine & "                              ,[FNJobState],[FTStateBy],[FDStateDate],[FTStateTime]"
                tsql &= Environment.NewLine & "                              ,[FTImage1],[FTImage2],[FTImage3],[FTImage4]"
                tsql &= Environment.NewLine & "                              ,[FNHSysBrandId],[FNHSysBuyId],[FTCancelAppBy],[FDCancelAppDate]"
                tsql &= Environment.NewLine & "                              ,[FDCancelAppTime],[FTCancelAppRemark],[FTPOTradingCo],[FTPOItem]"
                tsql &= Environment.NewLine & "                              ,[FTPOCreateDate],[FNHSysMerTeamId],[FNHSysPlantId],[FNHSysBuyGrpId]"
                tsql &= Environment.NewLine & "                              ,[FNHSysMainCategoryId],[FNHSysVenderPramId],[FTOrderCreateStatus],[FTImportUser],[FDImportDate],[FTImportTime],[FNHSysSeasonId],[FNHSysCmpIdCreate],[FTSubPgm])"
                tsql &= Environment.NewLine & " SELECT [FTInsUser],[FDInsDate],[FTInsTime]"
                tsql &= Environment.NewLine & "       ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tsql &= Environment.NewLine & "       ,[FTOrderNo],[FDOrderDate]"
                tsql &= Environment.NewLine & "       ,ISNULL(("
                tsql &= Environment.NewLine & "   SELECT TOP 1 FTUserName"
                tsql &= Environment.NewLine & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TMERMMerTeam AS AX WITH(NOLOCK)"
                tsql &= Environment.NewLine & "   WHERE   (AX.FNHSysMerTeamId = M.FNHSysMerTeamId)"
                tsql &= Environment.NewLine & "        ),'')  AS FTOrderBy"
                tsql &= Environment.NewLine & ",[FNOrderType]"
                ' tSql &= Environment.NewLine & "       ,[FNHSysCmpId]"
                tsql &= Environment.NewLine & "       ,CASE WHEN  ISNULL(DFCmpID,0) > 0 THEN  DFCmpID ELSE ISNULL(("
                tsql &= Environment.NewLine & "   SELECT TOP 1 FNHSysCmpId"
                tsql &= Environment.NewLine & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder AS AX WITH(NOLOCK)"
                tsql &= Environment.NewLine & "   WHERE  (AX.FNOrderType = 13) "
                tsql &= Environment.NewLine & "           AND (AX.FNHSysStyleId = M.FNHSysStyleId)"
                tsql &= Environment.NewLine & "   ORDER BY FTOrderNo DESC"
                tsql &= Environment.NewLine & "        ),"
                tsql &= Environment.NewLine & " ISNULL(("
                tsql &= Environment.NewLine & "   SELECT TOP 1 FNHSysCmpId"
                tsql &= Environment.NewLine & "   FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder AS AX WITH(NOLOCK)"
                tsql &= Environment.NewLine & "   WHERE  (AX.FNHSysStyleId = M.FNHSysStyleId) "
                tsql &= Environment.NewLine & "   ORDER BY FTOrderNo DESC"
                tsql &= Environment.NewLine & "        ),NULL)"
                tsql &= Environment.NewLine & " ) END AS FNHSysCmpId"
                tsql &= Environment.NewLine & "       ,[FNHSysCmpRunId],[FNHSysStyleId],[FTPORef]"
                tsql &= Environment.NewLine & "       ,[FNHSysCustId],[FNHSysAgencyId],[FNHSysProdTypeId],[FNHSysBuyerId]"
                tsql &= Environment.NewLine & "       ,[FTMainMaterial],[FTCombination],[FTRemark]"
                tsql &= Environment.NewLine & "       ,[FTStateOrderApp],[FTAppBy],[FDAppDate],[FTAppTime]"
                tsql &= Environment.NewLine & "       ,[FNJobState],[FTStateBy],[FDStateDate],[FTStateTime]"
                tsql &= Environment.NewLine & "       ,[FTImage1],[FTImage2],[FTImage3],[FTImage4]"
                tsql &= Environment.NewLine & "       ,[FNHSysBrandId],[FNHSysBuyId],[FTCancelAppBy],[FDCancelAppDate]"
                tsql &= Environment.NewLine & "       ,[FDCancelAppTime],[FTCancelAppRemark],[FTPOTradingCo],[FTPOItem]"
                tsql &= Environment.NewLine & "       ,[FTPOCreateDate],[FNHSysMerTeamId],[FNHSysPlantId],[FNHSysBuyGrpId]"
                tsql &= Environment.NewLine & "       ,[FNHSysMainCategoryId],[FNHSysVenderPramId],[FTOrderCreateStatus],[FTImportUser],[FDImportDate],[FTImportTime],[FNHSysSeasonId]," & Val(HI.ST.SysInfo.CmpID) & ",FTSubPgm"



                tsql &= Environment.NewLine & " FROM #Tab AS M "
                tsql &= Environment.NewLine & " DROP TABLE #Tab;"

                '...Merge Transaction TMERTOrderSub And TMERTOrderSub_BreakDown
                '========================================================================================================================================================================

                tsql &= Environment.NewLine & " create table #Tabsuborder("
                tsql &= Environment.NewLine & "  [FTInsUser] [nvarchar](50) NULL,"
                tsql &= Environment.NewLine & "[FDInsDate] [varchar](10) NULL,"
                tsql &= Environment.NewLine & "[FTInsTime] [varchar](8) NULL,"
                tsql &= Environment.NewLine & "[FTUpdUser] [nvarchar](50) NULL,"
                tsql &= Environment.NewLine & "[FDUpdDate] [varchar](10) NULL,"
                tsql &= Environment.NewLine & "[FTUpdTime] [varchar](8) NULL,"
                tsql &= Environment.NewLine & "[FTOrderNo] [nvarchar](30) NOT NULL,"
                tsql &= Environment.NewLine & "[FTSubOrderNo] [nvarchar](30) NOT NULL,"
                tsql &= Environment.NewLine & "[FDSubOrderDate] [nvarchar](10) NULL,"
                tsql &= Environment.NewLine & "[FTSubOrderBy] [nvarchar](50) NULL,"
                tsql &= Environment.NewLine & "[FDProDate] [nvarchar](10) NULL,"
                tsql &= Environment.NewLine & "[FDShipDate] [nvarchar](10) NULL,"
                tsql &= Environment.NewLine & "	[FNHSysBuyId] [int] NULL,"
                tsql &= Environment.NewLine & "[FNHSysContinentId] [int] NULL,"
                tsql &= Environment.NewLine & "[FNHSysCountryId] [int] NULL,"
                tsql &= Environment.NewLine & "[FNHSysProvinceId] [int] NULL,"
                tsql &= Environment.NewLine & "	[FNHSysShipModeId] [int] NULL,"
                tsql &= Environment.NewLine & "[FNHSysCurId] [int] NULL,"
                tsql &= Environment.NewLine & "[FNHSysGenderId] [int] NULL,"
                tsql &= Environment.NewLine & "[FNHSysUnitId] [int] NULL,"
                tsql &= Environment.NewLine & "[FNSubOrderState] [int] NULL,"
                tsql &= Environment.NewLine & "[FTStateEmb] [nvarchar](1) NULL,"
                tsql &= Environment.NewLine & "[FTStatePrint] [nvarchar](1) NULL,"
                tsql &= Environment.NewLine & "[FTStateHeat] [nvarchar](1) NULL,"
                tsql &= Environment.NewLine & "[FTStateLaser] [nvarchar](1) NULL,"
                tsql &= Environment.NewLine & "[FTStateWindows] [nvarchar](1) NULL,"
                tsql &= Environment.NewLine & "[FTStateSewOnly] [nvarchar](1) NULL,"
                tsql &= Environment.NewLine & "[FTStateOther1] [nvarchar](1) NULL,"
                tsql &= Environment.NewLine & "[FTOther1Note] [nvarchar](50) NULL,"
                tsql &= Environment.NewLine & "[FTStateOther2] [nvarchar](1) NULL,"
                tsql &= Environment.NewLine & "[FTOther2Note] [nvarchar](50) NULL,"
                tsql &= Environment.NewLine & "[FTStateOther3] [nvarchar](1) NULL,"
                tsql &= Environment.NewLine & "[FTOther3Note1] [nvarchar](50) NULL,"
                tsql &= Environment.NewLine & "[FTRemark] [nvarchar](1000) NULL,"
                tsql &= Environment.NewLine & "[FNHSysShipPortId] [int] NULL,"
                tsql &= Environment.NewLine & "[FDShipDateOrginal] [nvarchar](10) NULL,"
                tsql &= Environment.NewLine & "[FTCustRef] [nvarchar](200) NULL,"
                tsql &= Environment.NewLine & "[FTPOTrading] [nvarchar](50) NULL,"
                tsql &= Environment.NewLine & "[FNGrpSeq] [int] NULL,"
                tsql &= Environment.NewLine & "[FNHSysPlantId] [int] NULL,"
                tsql &= Environment.NewLine & "[FNHSysBuyGrpId] [int] NULL,"
                tsql &= Environment.NewLine & " [FTStateSet]  [int] NULL"




                tsql &= Environment.NewLine & "  )"
                tsql &= Environment.NewLine & "INSERT INTO #Tabsuborder"
                tsql &= Environment.NewLine & "         ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                tsql &= Environment.NewLine & "         ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                tsql &= Environment.NewLine & "         ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                tsql &= Environment.NewLine & "         ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                tsql &= Environment.NewLine & "         ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                tsql &= Environment.NewLine & "         ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                tsql &= Environment.NewLine & "         ,[FTStateLaser],[FTStateWindows],[FTStateSewOnly],[FTStateOther1],[FTOther1Note]"
                tsql &= Environment.NewLine & "         ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                tsql &= Environment.NewLine & "         ,[FTRemark]"
                tsql &= Environment.NewLine & "         ,[FNHSysShipPortId]"
                tsql &= Environment.NewLine & "         ,[FDShipDateOrginal],[FTCustRef],[FNGrpSeq],[FNHSysPlantId],[FNHSysBuyGrpId],[FTStateSet],FTPOTrading )"


                'tSql &= Environment.NewLine & " FROM ( "
                tsql &= Environment.NewLine & " SELECT DISTINCT  NULL AS FTInsUser , CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                tsql &= Environment.NewLine & "     , NULL AS FTUpdUser , NULL AS FDUpdDate, NULL AS FTUpdTime"
                tsql &= Environment.NewLine & "     , A.FTOrderNo"


                tsql &= Environment.NewLine & "     , (A.FTOrderNo + '-' +   CASE WHEN ROW_NUMBER() OVER (PARTITION BY A.FTOrderNo ORDER By A.FTOrderNo,A.FDShipDate) <=26 THEN  CHAR(64 + ROW_NUMBER() OVER (PARTITION BY A.FTOrderNo ORDER By A.FTOrderNo,A.FDShipDate)) ELSE 'A' + CHAR(64 + ((ROW_NUMBER() OVER (PARTITION BY A.FTOrderNo ORDER By A.FTOrderNo,A.FDShipDate)) - 26))   END  ) AS FTSubOrderNo"     '...แยก sub order no. ตาม destination (country)

                tsql &= Environment.NewLine & "     , A.FDOrderDate AS FDSubOrderDate"
                tsql &= Environment.NewLine & "     , N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTSubOrderBy"
                tsql &= Environment.NewLine & "     , ISNULL((SELECT [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[FN_GET_PRODDATE_IMPORT] (A.FDShipDate, " & nFNHSysCustId & ", NULL)), NULL) AS FDProdDate"
                tsql &= Environment.NewLine & "     , A.FDShipDate AS FDShipDate"
                tsql &= Environment.NewLine & "     , NULL AS FNHSysBuyId"
                tsql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysContinentId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCountry] AS L1 WITH(NOLOCK) WHERE L1.FNHSysCountryId =  A.FNHSysCountryId),0) AS FNHSysContinentId"
                tsql &= Environment.NewLine & "     , A.FNHSysCountryId"
                'tSql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysProvinceId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMPlant] AS L1 WITH(NOLOCK) WHERE L1.FNHSysPlantId =  A.FNHSysPlantId ), 0) AS FNHSysProvinceId"
                tsql &= Environment.NewLine & "     , A.FNHSysProvinceId AS FNHSysProvinceId"
                tsql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysShipModeId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipMode] AS L1 WITH(NOLOCK) WHERE L1.FTShipModeCode = B.FTMode), 0) AS FNHSysShipModeId"
                tsql &= Environment.NewLine & "     , A.FNHSysCurId AS FNHSysCurId"
                tsql &= Environment.NewLine & "     , A.FNHSysGenderId"
                tsql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysUnitId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMUnit] AS L1 WITH(NOLOCK) WHERE L1.FTUnitCode = B.FTUom), 0) AS FNHSysUnitId"
                tsql &= Environment.NewLine & "     , NULL AS FNSubOrderState"
                tsql &= Environment.NewLine & "     , C.FTStateEmb AS FTStateEmb"
                tsql &= Environment.NewLine & "     , C.FTStatePrint AS FTStatePrint"
                tsql &= Environment.NewLine & "     , C.FTStateHeat AS FTStateHeat"
                tsql &= Environment.NewLine & "     , C.FTStateLaser AS FTStateLaser"
                tsql &= Environment.NewLine & "     , C.FTStateWindows AS FTStateWindows,C.FTStateSewOnly"
                tsql &= Environment.NewLine & "     , NULL AS FTOther1Note, NULL AS FTOther1Note, NULL AS FTStateOther2, NULL AS FTOther2Note, NULL AS FTStateOther3, NULL AS FTOther3Note1, NULL AS FTRemark"
                tsql &= Environment.NewLine & "     , ISNULL((SELECT TOP 1 L1.FNHSysShipPortId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipPort] AS L1 WITH(NOLOCK) WHERE L1.FTShipPortCode = N'-'),NULL) AS FNHSysShipPortId"
                tsql &= Environment.NewLine & "     , A.FDShipDateOrginal AS FDShipDateOrginal"
                tsql &= Environment.NewLine & "     , dbo.FN_GetCustomer_Refer(A.FNHSysPlantId, A.FNHSysBuyerId) AS FTCustRef,A.FNGrpSeq,A.FNHSysPlantId,A.FNHSysBuyGrpId,A.FTStateSet,A.FTPOTrading"
                tsql &= Environment.NewLine & " FROM( "

                '-----Order Normal Data ------
                tsql &= Environment.NewLine & " SELECT  CASE WHEN ISNULL(A.FTStateSet,'0') ='1' THEN 1 ELSE 0 END AS FTStateSet,A.FTPONo, C.FDOrderDate, A.FTPOTrading, A.FTPOItem, A.FNRowImport, A.FTStyle, C.FTOrderNo,A.FNGrpSeq"
                tsql &= Environment.NewLine & "          , A.FDShipDate, A.FDShipDateOrginal, A.FNHSysGenderId, A.FNHSysCountryId,A.FNHSysPlantId,A.FNHSysBuyGrpId,A.FNHSysBuyerId,A.FNHSysProvinceId"
                'tSql &= Environment.NewLine & "          , ISNULL((SELECT TOP 1 L1.FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS L1 WITH(NOLOCK) WHERE L1.FNHSysCustId = C.FNHSysCustId), NULL) AS FNHSysCurId"
                tsql &= Environment.NewLine & "          , CASE WHEN ISNULL(FTCurrency,'')='' THEN ISNULL((SELECT TOP 1 L1.FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS L1 WITH(NOLOCK) WHERE L1.FNHSysCustId = C.FNHSysCustId), NULL) "
                tsql &= Environment.NewLine & "    ELSE  ISNULL((SELECT TOP 1 L1.FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TFINMCurrency] AS L1 WITH(NOLOCK) WHERE L1.FTCurCode = A.FTCurrency), NULL)  END AS FNHSysCurId"

                tsql &= Environment.NewLine & "   ,C.FTPOTradingCo As FTPOTrading2"
                tsql &= Environment.NewLine & "     FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                tsql &= Environment.NewLine & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS C WITH(NOLOCK) ON A.FTGenerateOrderNo = C.FTOrderNo"
                tsql &= Environment.NewLine & "     WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tsql &= Environment.NewLine & "           AND NOT EXISTS (SELECT 'T'"
                tsql &= Environment.NewLine & "                           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS L2 WITH(NOLOCK)"
                tsql &= Environment.NewLine & "                           WHERE C.FTOrderNo = L2.FTOrderNo)"
                tsql &= Environment.NewLine & "           AND A.FNRowImport IN (SELECT MAX(L1.FNRowImport)"
                tsql &= Environment.NewLine & "                                FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS L1 WITH(NOLOCK)"
                tsql &= Environment.NewLine & "                                WHERE L1.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tsql &= Environment.NewLine & "                                      AND L1.FTPONo = A.FTPONO"
                tsql &= Environment.NewLine & "                                GROUP BY L1.FTPONo,FDShipDate,FTStyle,FNHSysBuyGrpId,FNHSysPlantId,FNGrpSeq) "
                tsql &= Environment.NewLine & "  "

                '-----Order Normal Data -------

                '-----Order Normal Data Set---- 
                tsql &= Environment.NewLine & " UNION  "
                tsql &= Environment.NewLine & " SELECT 2 AS FTStateSet, A.FTPONo, C.FDOrderDate, A.FTPOTrading, A.FTPOItem, A.FNRowImport, A.FTStyle, C.FTOrderNo,A.FNGrpSeq"
                tsql &= Environment.NewLine & "          , A.FDShipDate, A.FDShipDateOrginal, A.FNHSysGenderId, A.FNHSysCountryId,A.FNHSysPlantId,A.FNHSysBuyGrpId,A.FNHSysBuyerId,A.FNHSysProvinceId"
                'tSql &= Environment.NewLine & "          , ISNULL((SELECT TOP 1 L1.FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS L1 WITH(NOLOCK) WHERE L1.FNHSysCustId = C.FNHSysCustId), NULL) AS FNHSysCurId"
                tsql &= Environment.NewLine & "          , CASE WHEN ISNULL(FTCurrency,'')='' THEN ISNULL((SELECT TOP 1 L1.FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] AS L1 WITH(NOLOCK) WHERE L1.FNHSysCustId = C.FNHSysCustId), NULL) "
                tsql &= Environment.NewLine & "    ELSE  ISNULL((SELECT TOP 1 L1.FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TFINMCurrency] AS L1 WITH(NOLOCK) WHERE L1.FTCurCode = A.FTCurrency), NULL)  END AS FNHSysCurId"

                tsql &= Environment.NewLine & "  ,C.FTPOTradingCo As FTPOTrading2"
                tsql &= Environment.NewLine & "     FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                tsql &= Environment.NewLine & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS C WITH(NOLOCK) ON A.FTGenerateOrderNo = C.FTOrderNo"
                tsql &= Environment.NewLine & "     WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tsql &= Environment.NewLine & "           AND NOT EXISTS (SELECT 'T'"
                tsql &= Environment.NewLine & "                           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS L2 WITH(NOLOCK)"
                tsql &= Environment.NewLine & "                           WHERE C.FTOrderNo = L2.FTOrderNo)"
                tsql &= Environment.NewLine & "           AND A.FNRowImport IN (SELECT MAX(L1.FNRowImport)"
                tsql &= Environment.NewLine & "                                FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS L1 WITH(NOLOCK)"
                tsql &= Environment.NewLine & "                                WHERE L1.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tsql &= Environment.NewLine & "                                      AND L1.FTPONo = A.FTPONO"
                tsql &= Environment.NewLine & "                                GROUP BY L1.FTPONo,FDShipDate,FTStyle,FNHSysBuyGrpId,FNHSysPlantId,FNGrpSeq) "
                tsql &= Environment.NewLine & "           AND ISNULL(A.FTStateSet,'0') ='1'"
                '-----Order Normal Data Set 


                tsql &= Environment.NewLine & " ) AS A INNER JOIN "
                tsql &= Environment.NewLine & "     (SELECT FNRowImport, FTPONo, FTPOTrading, FTStyle, FTShipDate, MAX(FTMode) AS FTMode, MAX(FTUom) AS FTUom "
                tsql &= Environment.NewLine & "      FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTImportOrderSizeBreakdownTemp WITH(NOLOCK) GROUP BY FNRowImport, FTPONo, FTPOTrading, FTStyle, FTShipDate"
                tsql &= Environment.NewLine & "     ) AS B ON A.FTPONo = B.FTPONo"
                tsql &= Environment.NewLine & "               And A.FTPOTrading = B.FTPOTrading"
                tsql &= Environment.NewLine & "               And A.FTStyle = B.FTStyle"
                tsql &= Environment.NewLine & "               And A.FNRowImport = B.FNRowImport"
                tsql &= Environment.NewLine & "       LEFT JOIN (SELECT L4.FNHSysStyleId, L4.FTStyleCode, ISNULL(MAX(L3.FTStateEmb),0) AS FTStateEmb, ISNULL(MAX(L3.FTStatePrint),0) AS FTStatePrint, ISNULL(MAX(L3.FTStateHeat),0) AS FTStateHeat, ISNULL(MAX(L3.FTStateLaser),0) AS FTStateLaser, ISNULL(MAX(L3.FTStateWindows),0) AS FTStateWindows,ISNULL(MAX(L3.FTStateSewOnly),'0') AS FTStateSewOnly"
                tsql &= Environment.NewLine & "                  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTStyle_Part] AS L3 WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS L4 WITH(NOLOCK) ON L3.FNHSysStyleId = L4.FNHSysStyleId"
                tsql &= Environment.NewLine & "                  GROUP BY L4.FNHSysStyleId, L4.FTStyleCode ) AS C ON A.FTStyle = C.FTStyleCode"

                'tSql &= Environment.NewLine & " ) AS A "

                tsql &= Environment.NewLine & "ORDER BY A.FTOrderNo ASC;"


                tsql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub]"
                tsql &= Environment.NewLine & "         ([FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                tsql &= Environment.NewLine & "         ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                tsql &= Environment.NewLine & "         ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                tsql &= Environment.NewLine & "         ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                tsql &= Environment.NewLine & "         ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                tsql &= Environment.NewLine & "         ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                tsql &= Environment.NewLine & "         ,[FTStateLaser],[FTStateWindows],[FTStateSewOnly],[FTStateOther1],[FTOther1Note]"
                tsql &= Environment.NewLine & "         ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                tsql &= Environment.NewLine & "         ,[FTRemark]"
                tsql &= Environment.NewLine & "         ,[FNHSysShipPortId]"
                tsql &= Environment.NewLine & "         ,[FDShipDateOrginal],[FTCustRef],[FNHSysPlantId],[FNHSysBuyGrpId],[FNOrderSetType],[FTPOTrading])"
                tsql &= Environment.NewLine & "         SELECT [FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser]"
                tsql &= Environment.NewLine & "         ,[FDUpdDate],[FTUpdTime],[FTOrderNo],[FTSubOrderNo]"
                tsql &= Environment.NewLine & "         ,[FDSubOrderDate],[FTSubOrderBy],[FDProDate],[FDShipDate]"
                tsql &= Environment.NewLine & "         ,[FNHSysBuyId],[FNHSysContinentId],[FNHSysCountryId],[FNHSysProvinceId]"
                tsql &= Environment.NewLine & "         ,[FNHSysShipModeId],[FNHSysCurId],[FNHSysGenderId],[FNHSysUnitId]"
                tsql &= Environment.NewLine & "         ,[FNSubOrderState],[FTStateEmb],[FTStatePrint],[FTStateHeat]"
                tsql &= Environment.NewLine & "         ,[FTStateLaser],[FTStateWindows],[FTStateSewOnly],[FTStateOther1],[FTOther1Note]"
                tsql &= Environment.NewLine & "         ,[FTStateOther2],[FTOther2Note],[FTStateOther3],[FTOther3Note1]"
                tsql &= Environment.NewLine & "         ,[FTRemark]"
                tsql &= Environment.NewLine & "         ,[FNHSysShipPortId]"
                tsql &= Environment.NewLine & "         ,[FDShipDateOrginal],[FTCustRef],[FNHSysPlantId],[FNHSysBuyGrpId],FTStateSet,FTPOTrading"
                tsql &= Environment.NewLine & " FROM #Tabsuborder"

                tsql &= Environment.NewLine & " UPDATE A"

                tsql &= Environment.NewLine & " SET FTGenerateOrderSubNo = ISNULL((SELECT TOP 1 ISNULL(FTSubOrderNo,'')"
                tsql &= Environment.NewLine & "                                FROM #Tabsuborder"
                tsql &= Environment.NewLine & "                                WHERE FTOrderNo = A.FTGenerateOrderNo AND FDShipDate = A.FDShipDate AND FNGrpSeq=A.FNGrpSeq  AND FNHSysPlantId=A.FNHSysPlantId AND FNHSysBuyGrpId=A.FNHSysBuyGrpId  AND FTStateSet<>2 ), '')"
                tsql &= Environment.NewLine & " ,FTGenerateOrderSubNo2 = ISNULL((SELECT TOP 1 ISNULL(FTSubOrderNo,'')"
                tsql &= Environment.NewLine & "                                FROM #Tabsuborder"
                tsql &= Environment.NewLine & "                                WHERE FTOrderNo = A.FTGenerateOrderNo AND FDShipDate = A.FDShipDate AND FNGrpSeq=A.FNGrpSeq  AND FNHSysPlantId=A.FNHSysPlantId AND FNHSysBuyGrpId=A.FNHSysBuyGrpId AND FTStateSet=2), '')"

                tsql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMerTeam] AS B WITH(NOLOCK) ON A.FNHSysMerTeamId = B.FNHSysMerTeamId"
                tsql &= Environment.NewLine & " WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                tsql &= Environment.NewLine & " DROP TABLE #Tabsuborder;"

                tsql &= Environment.NewLine & " DECLARE @TMERTOrderSub_BreakDown_Import AS TABLE ([FTInsUser] [nvarchar](50), [FDInsDate] [varchar](10) NULL, [FTInsTime] [varchar](8) NULL,"
                tsql &= Environment.NewLine & "                              [FTUpdUser] [nvarchar](50) NULL, [FDUpdDate] [varchar](10) NULL, [FTUpdTime] [varchar](8) NULL,"
                tsql &= Environment.NewLine & "                              [FTOrderNo] [nvarchar](30) NOT NULL, [FTSubOrderNo] [nvarchar](30) NOT NULL,"
                tsql &= Environment.NewLine & "                              [FTColorway] [nvarchar](30) NOT NULL, [FTSizeBreakDown] [nvarchar](30) NOT NULL,"
                tsql &= Environment.NewLine & "                              [FNPrice] [numeric](18, 5) NULL, [FNQuantity] [int] NULL, [FNAmt] [numeric](18, 5) NULL,"
                tsql &= Environment.NewLine & "                              [FNHSysMatColorId] [int] NULL, [FNHSysMatSizeId] [int] NULL,"
                tsql &= Environment.NewLine & "                              [FNExtraQty] [numeric](18, 5) NULL, [FNQuantityExtra] [numeric](18, 5) NULL,"
                tsql &= Environment.NewLine & "                              [FNGrandQuantity] [numeric](18, 5) NULL, [FNAmntExtra] [numeric](18, 5) NULL,"
                tsql &= Environment.NewLine & "                              [FNGrandAmnt] [numeric](18, 5) NULL, [FNGarmentQtyTest] [int] NULL,[FNAmntQtyTest] [numeric](18, 5) NULL,[FTPOItem] [varchar](30) NULL,[FNCMDisPer]  [numeric](18, 5) NULL,[FNOperateFee]  [numeric](18, 5) NULL )"

                '-----Order Normal Data 
                tsql &= Environment.NewLine & " INSERT INTO @TMERTOrderSub_BreakDown_Import ([FTInsUser], [FDInsDate], [FTInsTime],"
                tsql &= Environment.NewLine & "				    [FTUpdUser], [FDUpdDate], [FTUpdTime],"
                tsql &= Environment.NewLine & "				    [FTOrderNo], [FTSubOrderNo], [FTColorway],"
                tsql &= Environment.NewLine & "				    [FTSizeBreakDown], [FNPrice],"
                tsql &= Environment.NewLine & "				    [FNQuantity], [FNAmt], [FNHSysMatColorId], [FNHSysMatSizeId],"
                tsql &= Environment.NewLine & "				    [FNExtraQty], [FNQuantityExtra], [FNGrandQuantity],"
                tsql &= Environment.NewLine & "				    [FNAmntExtra], [FNGrandAmnt],[FTPOItem],[FNCMDisPer],[FNOperateFee])"
                tsql &= Environment.NewLine & " SELECT ordImport.FTInsUser, ordImport.FDInsDate, ordImport.FTInsTime,"
                tsql &= Environment.NewLine & "       ordImport.FTUpdUser, ordImport.FDUpdDate, ordImport.FTUpdTime,"
                tsql &= Environment.NewLine & "       ordImport.FTOrderNo, ordImport.FTSubOrderNo, ordImport.FTColorway,"
                tsql &= Environment.NewLine & "       ordImport.FTSizeBreakDown, ordImport.FNPrice, ordImport.FNQuantity,"
                tsql &= Environment.NewLine & "       ordImport.FNAmt, ordImport.FNHSysMatColorId, ordImport.FNHSysMatSizeId,"
                tsql &= Environment.NewLine & "       ordImport.FNExtraQty, ordImport.FNQuantityExtra, ordImport.FNGrandQuantity,"
                tsql &= Environment.NewLine & "       ordImport.FNAmntExtra, ordImport.FNGrandAmnt,ordImport.FTPOItem,ordImport.FNCMDisPer,ordImport.FNOperateFee"
                tsql &= Environment.NewLine & " FROM (SELECT NULL AS FTInsUser, CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                tsql &= Environment.NewLine & "           , NULL AS FTUpdUser, NULL AS FDUpdDate, NULL AS FTUpdTime"
                tsql &= Environment.NewLine & "           , AA.FTGenerateOrderNo AS FTOrderNo"
                tsql &= Environment.NewLine & "           , (MM3.FTSubOrderNo) AS FTSubOrderNo"
                tsql &= Environment.NewLine & "           , A.FTColorwayCode AS FTColorway"
                tsql &= Environment.NewLine & "           , A.FTSizeBreakdownCode AS FTSizeBreakDown"
                tsql &= Environment.NewLine & "           , ISNULL(MAX(A.FNPrice),0) AS FNPrice"
                tsql &= Environment.NewLine & "           , ISNULL(SUM(A.FNQuantity), 0) AS FNQuantity"
                tsql &= Environment.NewLine & "           , ISNULL((SUM(A.FNQuantity) * MAX(A.FNPrice)), NULL) AS FNAmt"
                tsql &= Environment.NewLine & "           , C.FNHSysMatColorId AS FNHSysMatColorId"
                tsql &= Environment.NewLine & "           , D.FNHSysMatSizeId AS FNHSysMatSizeId"
                tsql &= Environment.NewLine & "           , NULL AS FNExtraQty"
                tsql &= Environment.NewLine & "           , NULL AS FNQuantityExtra"
                tsql &= Environment.NewLine & "           , ISNULL(SUM(A.FNQuantity), 0) AS FNGrandQuantity"
                tsql &= Environment.NewLine & "           , NULL AS FNAmntExtra"
                tsql &= Environment.NewLine & "           , ISNULL((SUM(A.FNQuantity) * MAX(A.FNPrice)), NULL) AS FNGrandAmnt,MAX(ISNULL(A.FTPOItem,'')) AS FTPOItem,MAX(ISNULL(STM.FNCMDisPer,0)) AS FNCMDisPer"
                tsql &= Environment.NewLine & "           ,MAX(CASE WHEN ISNULL(Cmpc.FNCmpBranchState,0) <=0 THEN ISNULL(Cus.FNOperateFee,0) ELSE ISNULL(Cus.FNOperateFeeForeign,0) END   ) AS FNOperateFee"
                tsql &= Environment.NewLine & "      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS AA WITH(NOLOCK)"
                tsql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] As MM3 With(NOLOCK) On AA.FTGenerateOrderNo = MM3.FTOrderNo And  AA.FDShipDate = MM3.FDShipDate And AA.FTGenerateOrderSubNo = MM3.FTSubOrderNo"
                tsql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] As A With(NOLOCK)  On  AA.FTUserLogin = A.FTUserLogin And AA.FTPONo = A.FTPONo And AA.FDShipDate = A.FTShipDate And AA.FTStyle = A.FTStyle And MM3.FDShipDate = A.FTShipDate And AA.FTPOItem=A.FTPOItem"
                tsql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] As B With(NOLOCK) On AA.FTGenerateOrderNo = B.FTOrderNo"
                tsql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] As C (NOLOCK) On A.FTColorwayCode = C.FTMatColorCode"
                tsql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] As D (NOLOCK) On A.FTSizeBreakdownCode = D.FTMatSizeCode"
                tsql &= Environment.NewLine & "           LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] As STM With(NOLOCK) On B.FNHSysStyleId = STM.FNHSysStyleId"
                tsql &= Environment.NewLine & "           LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] As Cus With(NOLOCK) On B.FNHSysCustId = Cus.FNHSysCustId"
                tsql &= Environment.NewLine & "           LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCmp] As Cmpc With(NOLOCK) On B.FNHSysCmpId = Cmpc.FNHSysCmpId"
                tsql &= Environment.NewLine & "      WHERE A.FNQuantity > 0 "
                tsql &= Environment.NewLine & "      GROUP BY AA.FTGenerateOrderNo, A.FTPONo, A.FTPOTrading, A.FTStyle, MM3.FTSubOrderNo, A.FTColorwayCode, A.FTSizeBreakdownCode, C.FNHSysMatColorId, D.FNHSysMatSizeId"
                tsql &= Environment.NewLine & "      ) As ordImport"
                '-----Order Normal Data


                '-----Order Normal Data  Set
                tsql &= Environment.NewLine & " INSERT INTO @TMERTOrderSub_BreakDown_Import ([FTInsUser], [FDInsDate], [FTInsTime],"
                tsql &= Environment.NewLine & "				    [FTUpdUser], [FDUpdDate], [FTUpdTime],"
                tsql &= Environment.NewLine & "				    [FTOrderNo], [FTSubOrderNo], [FTColorway],"
                tsql &= Environment.NewLine & "				    [FTSizeBreakDown], [FNPrice],"
                tsql &= Environment.NewLine & "				    [FNQuantity], [FNAmt], [FNHSysMatColorId], [FNHSysMatSizeId],"
                tsql &= Environment.NewLine & "				    [FNExtraQty], [FNQuantityExtra], [FNGrandQuantity],"
                tsql &= Environment.NewLine & "				    [FNAmntExtra], [FNGrandAmnt],[FTPOItem],[FNCMDisPer],[FNOperateFee])"
                tsql &= Environment.NewLine & " SELECT ordImport.FTInsUser, ordImport.FDInsDate, ordImport.FTInsTime,"
                tsql &= Environment.NewLine & "       ordImport.FTUpdUser, ordImport.FDUpdDate, ordImport.FTUpdTime,"
                tsql &= Environment.NewLine & "       ordImport.FTOrderNo, ordImport.FTSubOrderNo, ordImport.FTColorway,"
                tsql &= Environment.NewLine & "       ordImport.FTSizeBreakDown, ordImport.FNPrice, ordImport.FNQuantity,"
                tsql &= Environment.NewLine & "       ordImport.FNAmt, ordImport.FNHSysMatColorId, ordImport.FNHSysMatSizeId,"
                tsql &= Environment.NewLine & "       ordImport.FNExtraQty, ordImport.FNQuantityExtra, ordImport.FNGrandQuantity,"
                tsql &= Environment.NewLine & "       ordImport.FNAmntExtra, ordImport.FNGrandAmnt,ordImport.FTPOItem,ordImport.FNCMDisPer,ordImport.FNOperateFee"
                tsql &= Environment.NewLine & " FROM (SELECT NULL AS FTInsUser, CONVERT(VARCHAR(10),GETDATE(),111) AS FDInsDate, CONVERT(VARCHAR(8),GETDATE(),114) AS FTInsTime"
                tsql &= Environment.NewLine & "           , NULL AS FTUpdUser, NULL AS FDUpdDate, NULL AS FTUpdTime"
                tsql &= Environment.NewLine & "           , AA.FTGenerateOrderNo AS FTOrderNo"
                tsql &= Environment.NewLine & "           , (MM3.FTSubOrderNo) AS FTSubOrderNo"
                tsql &= Environment.NewLine & "           , A.FTColorwayCode AS FTColorway"
                tsql &= Environment.NewLine & "           , A.FTSizeBreakdownCode AS FTSizeBreakDown"
                tsql &= Environment.NewLine & "           , 0 AS FNPrice"
                tsql &= Environment.NewLine & "           , ISNULL(SUM(A.FNQuantity), 0) AS FNQuantity"
                tsql &= Environment.NewLine & "           , 0 AS FNAmt"
                tsql &= Environment.NewLine & "           , C.FNHSysMatColorId AS FNHSysMatColorId"
                tsql &= Environment.NewLine & "           , D.FNHSysMatSizeId AS FNHSysMatSizeId"
                tsql &= Environment.NewLine & "           , NULL AS FNExtraQty"
                tsql &= Environment.NewLine & "           , NULL AS FNQuantityExtra"
                tsql &= Environment.NewLine & "           , ISNULL(SUM(A.FNQuantity), 0) AS FNGrandQuantity"
                tsql &= Environment.NewLine & "           , NULL AS FNAmntExtra"
                tsql &= Environment.NewLine & "           , 0 AS FNGrandAmnt,MAX(ISNULL(A.FTPOItem,'')) AS FTPOItem,MAX(ISNULL(STM.FNCMDisPer,0)) AS FNCMDisPer"
                tsql &= Environment.NewLine & "           ,MAX(CASE WHEN ISNULL(Cmpc.FNCmpBranchState,0) <=0 THEN ISNULL(Cus.FNOperateFee,0) ELSE ISNULL(Cus.FNOperateFeeForeign,0) END   ) AS FNOperateFee"
                tsql &= Environment.NewLine & "      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS AA WITH(NOLOCK)"

                tsql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] As MM3 With(NOLOCK) On AA.FTGenerateOrderNo = MM3.FTOrderNo And  AA.FDShipDate = MM3.FDShipDate And AA.FTGenerateOrderSubNo2 = MM3.FTSubOrderNo"
                tsql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderSizeBreakdownTemp] As A With(NOLOCK)  On  AA.FTUserLogin = A.FTUserLogin And AA.FTPONo = A.FTPONo And AA.FDShipDate = A.FTShipDate And AA.FTStyle = A.FTStyle And MM3.FDShipDate = A.FTShipDate And AA.FTPOItem=A.FTPOItem"
                tsql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] As B With(NOLOCK) On AA.FTGenerateOrderNo = B.FTOrderNo"
                tsql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] As C (NOLOCK) On A.FTColorwayCode = C.FTMatColorCode"
                tsql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] As D (NOLOCK) On A.FTSizeBreakdownCode = D.FTMatSizeCode"
                tsql &= Environment.NewLine & "           LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] As STM With(NOLOCK) On B.FNHSysStyleId = STM.FNHSysStyleId"
                tsql &= Environment.NewLine & "           LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCustomer] As Cus With(NOLOCK) On B.FNHSysCustId = Cus.FNHSysCustId"
                tsql &= Environment.NewLine & "           LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCmp] As Cmpc With(NOLOCK) On B.FNHSysCmpId = Cmpc.FNHSysCmpId"

                tsql &= Environment.NewLine & "      WHERE A.FNQuantity > 0  AND ISNULL(AA.FTGenerateOrderSubNo2,'') <> ''"
                tsql &= Environment.NewLine & "      GROUP BY AA.FTGenerateOrderNo, A.FTPONo, A.FTPOTrading, A.FTStyle, MM3.FTSubOrderNo, A.FTColorwayCode, A.FTSizeBreakdownCode, C.FNHSysMatColorId, D.FNHSysMatSizeId"
                tsql &= Environment.NewLine & "      ) As ordImport"
                '-----Order Normal Set

                tsql &= Environment.NewLine & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_BreakDown] ([FTInsUser],[FDInsDate],[FTInsTime]"
                tsql &= Environment.NewLine & "                 ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tsql &= Environment.NewLine & "                 ,[FTOrderNo],[FTSubOrderNo]"
                tsql &= Environment.NewLine & "                 ,[FTColorway],[FTSizeBreakDown]"
                tsql &= Environment.NewLine & "                 ,[FNPrice],[FNQuantity],[FNAmt]"
                tsql &= Environment.NewLine & "                 ,[FNHSysMatColorId],[FNHSysMatSizeId]"
                tsql &= Environment.NewLine & "                 ,[FNExtraQty],[FNQuantityExtra]"
                tsql &= Environment.NewLine & "                 ,[FNGrandQuantity]"
                tsql &= Environment.NewLine & "                 ,[FNAmntExtra]"
                tsql &= Environment.NewLine & "                 ,[FNGrandAmnt],[FNPriceOrg],[FTNikePOLineItem],[FNCMDisPer],[FNCMDisAmt],[FNOperateFee],[FNOperateFeeAmt],[FNNetFOB],FNNetPriceOperateFee,FNNetPriceOperateFeeAmt,FNNetNetPrice)"
                tsql &= Environment.NewLine & " Select aa.[FTInsUser], aa.[FDInsDate], aa.[FTInsTime],"
                tsql &= Environment.NewLine & "       aa.[FTUpdUser], aa.[FDUpdDate], aa.[FTUpdTime],"
                tsql &= Environment.NewLine & "       aa.[FTOrderNo], aa.[FTSubOrderNo], aa.[FTColorway],"
                tsql &= Environment.NewLine & "       aa.[FTSizeBreakDown], aa.[FNPrice],"
                tsql &= Environment.NewLine & "       aa.[FNQuantity], aa.[FNAmt], aa.[FNHSysMatColorId], aa.[FNHSysMatSizeId],"
                tsql &= Environment.NewLine & "       aa.[FNExtraQty], aa.[FNQuantityExtra], aa.[FNGrandQuantity],"
                tsql &= Environment.NewLine & "       aa.[FNAmntExtra], aa.[FNGrandAmnt],aa.[FNPrice],aa.[FTPOItem],aa.FNCMDisPer,Convert(numeric(18,4),((aa.[FNPrice]* ISNULL(aa.FNCMDisPer,0))/100.00))"
                tsql &= Environment.NewLine & "     ,aa.FNOperateFee,Convert(numeric(18,4),((aa.[FNPrice]* ISNULL(aa.FNOperateFee,0))/100.00)) AS FNOperateFeeAmt,(aa.[FNPrice] - Convert(numeric(18,4),((aa.[FNPrice]* ISNULL(aa.FNOperateFee,0))/100.00)) ) AS FNNetFOB "
                tsql &= Environment.NewLine & "     ,aa.FNOperateFee,Convert(numeric(18,4),((aa.[FNPrice]* ISNULL(aa.FNOperateFee,0))/100.00)) AS FNOperateFeeAmt,(aa.[FNPrice] - Convert(numeric(18,4),((aa.[FNPrice]* ISNULL(aa.FNOperateFee,0))/100.00)) ) AS FNNetFOB "
                tsql &= Environment.NewLine & " FROM @TMERTOrderSub_BreakDown_Import As aa"
                tsql &= Environment.NewLine & " WHERE Not EXISTS (Select 'T'"
                tsql &= Environment.NewLine & "                  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS bb (NOLOCK)"
                tsql &= Environment.NewLine & "                  WHERE bb.FTOrderNo = aa.FTOrderNo"
                tsql &= Environment.NewLine & "                        AND bb.FTSubOrderNo = aa.FTSubOrderNo"
                tsql &= Environment.NewLine & "                        AND bb.FTColorway = aa.FTColorway"
                tsql &= Environment.NewLine & "                        AND bb.FTSizeBreakDown = aa.FTSizeBreakDown"
                tsql &= Environment.NewLine & "                  );"

                tsql &= Environment.NewLine & " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Pack( "
                tsql &= Environment.NewLine & "   FTOrderNo, FTSubOrderNo, FNPackSeq, FTPackDescription, FTPackNote, FTImage, FBImage "
                tsql &= Environment.NewLine & " )"
                tsql &= Environment.NewLine & "  Select  A.FTOrderNo,A.FTSubOrderNo,P.FNPackSeq, P.FTPackDescription, P.FTPackNote,'' AS FTImage, P.FBImage "
                tsql &= Environment.NewLine & "  FROM( Select DISTINCT B.FTOrderNo , B4.FTSubOrderNo, B.FNHSysStyleId, B.FNHSysSeasonId "
                tsql &= Environment.NewLine & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] As AA With(NOLOCK) "
                tsql &= Environment.NewLine & "     INNER Join [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] As B With(NOLOCK) On AA.FTGenerateOrderNo = B.FTOrderNo     "
                tsql &= Environment.NewLine & "     INNER Join [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] As B4 With(NOLOCK) On B.FTOrderNo = B4.FTOrderNo "
                tsql &= Environment.NewLine & "     WHERE AA.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                tsql &= Environment.NewLine & " ) As A "
                tsql &= Environment.NewLine & "   INNER Join [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTStyle_Packing As P With(NOLOCK) On A.FNHSysStyleId = P.FNHSysStyleId And A.FNHSysSeasonId = P.FNHSysSeasonId "

                '========================================================================================================================================================================

                If HI.Conn.SQLConn.Execute_Tran(tsql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    'MsgBox("Step Append New Order No !!!")
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _bImportComplete = False
                Else
                    'HI.Conn.SQLConn.Tran.Commit()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _bImportComplete = True
                End If

                'If HI.Conn.SQLConn.ExecuteOnly(tSql, Conn.DB.DataBaseName.DB_MERCHAN) <= 0 Then
                '    'MsgBox("Step Append New Order No !!!")
                '    HI.Conn.SQLConn.Tran.Rollback()
                '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                '    _bImportComplete = False
                'Else
                '    'HI.Conn.SQLConn.Tran.Commit()
                '    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                '    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                '    _bImportComplete = True
                'End If

            Else
                'MsgBox("พบปัญหาในการบันทึกรายการนำเข้าข้อมูลใบสั่งผลิต !!!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If

            If _bImportComplete = True Then
                Application.DoEvents()

                If statecaltest Then
                    _Spls.UpdateInformation("Generate Factory Sub Order Extra And Test Quantity .....Please Wait")

                    'tsql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTImportOrderUpdExtraQtyTemp] ([FTUserLogin]"
                    'tsql &= Environment.NewLine & "             ,[FTOrderNo]"
                    'tsql &= Environment.NewLine & "             ,[FTPORef]"
                    'tsql &= Environment.NewLine & "             ,[FTSubOrderNo]"
                    'tsql &= Environment.NewLine & "             ,[FNHSysMatColorId]"
                    'tsql &= Environment.NewLine & "             ,[FNHSysMatSizeId]"
                    'tsql &= Environment.NewLine & "             ,[FNQuantity]"
                    'tsql &= Environment.NewLine & "             ,[FNPrice]"
                    'tsql &= Environment.NewLine & "             ,[FNAmt]"
                    'tsql &= Environment.NewLine & "             ,[FNExtraQty]"
                    'tsql &= Environment.NewLine & "             ,[FNQuantityExtra]"
                    'tsql &= Environment.NewLine & "             ,[FNGrandQuantity]"
                    'tsql &= Environment.NewLine & "             ,[FNAmntExtra]"
                    'tsql &= Environment.NewLine & "             ,[FNGrandAmnt]"
                    'tsql &= Environment.NewLine & "             ,[FNGarmentQtyTest],[FTOrderSubNo2],[FTShipDate],[FNExtraType],[FNHSysStyleId],[FNHSysSeasonId])"
                    'tsql &= Environment.NewLine & " SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTUserLogin,"
                    'tsql &= Environment.NewLine & "       M2.FTOrderNo, M2.FTPORef, M3.FTSubOrderNo, "
                    'tsql &= Environment.NewLine & "       M4.FNHSysMatColorId, M4.FNHSysMatSizeId,"
                    'tsql &= Environment.NewLine & "       M4.FNQuantity, M4.FNPrice, M4.FNAmt, M4.FNExtraQty, M4.FNQuantityExtra, M4.FNGrandQuantity,"
                    'tsql &= Environment.NewLine & "       M4.FNAmntExtra, M4.FNGrandAmnt, M4.FNGarmentQtyTest,ISNULL(M1.FTGenerateOrderSubNo2,'') AS FTGenerateOrderSubNo2,M3.FDShipDate,M1.FNExtraType,M2.FNHSysStyleId,M2.FNHSysSeasonId"
                    'tsql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS M1 WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS M2 WITH(NOLOCK) ON M1.FTGenerateOrderNo = M2.FTOrderNo"
                    'tsql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS M3 WITH(NOLOCK) ON M2.FTOrderNo = M3.FTOrderNo AND M1.FTGenerateOrderSubNo = M3.FTSubOrderNo"
                    'tsql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS M4 WITH(NOLOCK) ON M3.FTOrderNo = M4.FTOrderNo"
                    'tsql &= Environment.NewLine & "     AND M3.FTSubOrderNo = M4.FTSubOrderNo"
                    'tsql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS M5 WITH(NOLOCK) ON M4.FNHSysMatColorId = M5.FNHSysMatColorId"
                    'tsql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS M6 WITH(NOLOCK) ON M4.FNHSysMatSizeId = M6.FNHSysMatSizeId"
                    'tsql &= Environment.NewLine & " WHERE M1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AND ISNULL(M3.FNOrderSetType,0) <=1"
                    'tsql &= Environment.NewLine & "           AND M1.FNRowImport IN (SELECT MAX(L1.FNRowImport)"
                    'tsql &= Environment.NewLine & "                                FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS L1 WITH(NOLOCK)"
                    'tsql &= Environment.NewLine & "                                WHERE L1.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    'tsql &= Environment.NewLine & "                                      AND L1.FTPONo = M1.FTPONO"
                    'tsql &= Environment.NewLine & "                                GROUP BY L1.FTPONo,FDShipDate,FTStyle,FNHSysBuyGrpId,FNHSysPlantId,FNGrpSeq) "


                    'tsql &= Environment.NewLine & "       AND M2.FTOrderNo NOT IN ('AA11BB22CC33DD44EE55ZZ99')"
                    'tsql &= Environment.NewLine & "ORDER BY M2.FTOrderNo ASC, M3.FTSubOrderNo ASC, M5.FNMatColorSeq ASC, M6.FNMatSizeSeq ASC;"


                    '...represent record sub order no / size breakdown use for compute extra quantity garment by vendor programme/main catetory BB:Basketball, FB:Football/plant/buy group
                    tsql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTImportOrderUpdExtraQtyTemp] ([FTUserLogin]"
                    tsql &= Environment.NewLine & "             ,[FTOrderNo]"
                    tsql &= Environment.NewLine & "             ,[FTPORef]"
                    tsql &= Environment.NewLine & "             ,[FTSubOrderNo]"
                    tsql &= Environment.NewLine & "             ,[FNHSysMatColorId]"
                    tsql &= Environment.NewLine & "             ,[FNHSysMatSizeId]"
                    tsql &= Environment.NewLine & "             ,[FNQuantity]"
                    tsql &= Environment.NewLine & "             ,[FNPrice]"
                    tsql &= Environment.NewLine & "             ,[FNAmt]"
                    tsql &= Environment.NewLine & "             ,[FNExtraQty]"
                    tsql &= Environment.NewLine & "             ,[FNQuantityExtra]"
                    tsql &= Environment.NewLine & "             ,[FNGrandQuantity]"
                    tsql &= Environment.NewLine & "             ,[FNAmntExtra]"
                    tsql &= Environment.NewLine & "             ,[FNGrandAmnt]"
                    tsql &= Environment.NewLine & "             ,[FNGarmentQtyTest],[FTOrderSubNo2],[FTShipDate],[FNExtraType],[FNHSysStyleId],[FNHSysSeasonId])"
                    tsql &= Environment.NewLine & " SELECT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTUserLogin,"
                    tsql &= Environment.NewLine & "       M2.FTOrderNo, M2.FTPORef, M3.FTSubOrderNo, "
                    tsql &= Environment.NewLine & "       M4.FNHSysMatColorId, M4.FNHSysMatSizeId,"
                    tsql &= Environment.NewLine & "       M4.FNQuantity, M4.FNPrice, M4.FNAmt, M4.FNExtraQty, M4.FNQuantityExtra, M4.FNGrandQuantity,"
                    tsql &= Environment.NewLine & "       M4.FNAmntExtra, M4.FNGrandAmnt, M4.FNGarmentQtyTest,ISNULL(M1.FTGenerateOrderSubNo2,'') AS FTGenerateOrderSubNo2,M3.FDShipDate,M1.FNExtraType,M2.FNHSysStyleId,M2.FNHSysSeasonId"
                    tsql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS M1 WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS M2 WITH(NOLOCK) ON M1.FTGenerateOrderNo = M2.FTOrderNo"
                    tsql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS M3 WITH(NOLOCK) ON M2.FTOrderNo = M3.FTOrderNo AND M1.FTGenerateOrderSubNo = M3.FTSubOrderNo"
                    tsql &= Environment.NewLine & "     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS M4 WITH(NOLOCK) ON M3.FTOrderNo = M4.FTOrderNo"
                    tsql &= Environment.NewLine & "     AND M3.FTSubOrderNo = M4.FTSubOrderNo"
                    tsql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS M5 WITH(NOLOCK) ON M4.FNHSysMatColorId = M5.FNHSysMatColorId"
                    tsql &= Environment.NewLine & "     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS M6 WITH(NOLOCK) ON M4.FNHSysMatSizeId = M6.FNHSysMatSizeId"
                    tsql &= Environment.NewLine & " WHERE M1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AND ISNULL(M3.FNOrderSetType,0) <=1"
                    tsql &= Environment.NewLine & "           AND M1.FNRowImport IN (SELECT MAX(L1.FNRowImport)"
                    tsql &= Environment.NewLine & "                                FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS L1 WITH(NOLOCK)"
                    tsql &= Environment.NewLine & "                                WHERE L1.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    tsql &= Environment.NewLine & "                                      AND L1.FTPONo = M1.FTPONO"
                    tsql &= Environment.NewLine & "                                GROUP BY L1.FTPONo,FDShipDate,FTStyle,FNHSysBuyGrpId,FNHSysPlantId,FNGrpSeq) "
                    tsql &= Environment.NewLine & "       AND M2.FTOrderNo NOT IN ('AA11BB22CC33DD44EE55ZZ99')"
                    tsql &= Environment.NewLine & " ORDER BY M2.FTOrderNo ASC, M3.FTSubOrderNo ASC, M5.FNMatColorSeq ASC, M6.FNMatSizeSeq ASC;"

                    If HI.Conn.SQLConn.Execute_Tran(tsql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        'MsgBox("Step Append New ImportOrderUpdExtraQtyTemp !!!")
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        _bImportComplete = False
                    Else
                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        _bImportComplete = True
                    End If
                End If


            End If

            If _bImportComplete = True Then

                If _bImportComplete = True Then

                    Dim Cmdstring As String = ""
                    Dim dtstss As New DataTable

                    Cmdstring = "  SELECT  DISTINCT C.FNHSysStyleId,C.FNHSysSeasonId   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS A "
                    Cmdstring &= Environment.NewLine & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS C WITH(NOLOCK) ON A.FTGenerateOrderNo = C.FTOrderNo"
                    Cmdstring &= Environment.NewLine & "     WHERE A.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                    dtstss = HI.Conn.SQLConn.GetDataTable(Cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                    For Each R As DataRow In dtstss.Rows
                        csOrder.ConfirmSizeSpecToOrder(Val(R!FNHSysStyleId.ToString), Val(R!FNHSysSeasonId.ToString))
                    Next

                    If statecaltest Then

                        tsql = ""
                        'tSql = "EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_UPDATE_IMPORT_ORDER_QTY_EXTRA_GARMENT_NEW N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        tsql = "EXEC  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_UPDATEIMPORTORDER_QTY_EXTRAGARMENT N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                        If HI.Conn.SQLConn.ExecuteOnly(tsql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                        End If

                    End If

                    tsql = ""
                    tsql = " UPDATE X"
                    tsql &= Environment.NewLine & " SET X.FNGrandQuantity = ISNULL(X.FNQuantity,0) + ISNULL(Y.FNQuantityExtra, 0)+ ISNULL(X.FNGarmentQtyTest, 0)+ ISNULL(X.FNExternalQtyTest, 0),"
                    tsql &= Environment.NewLine & "    X.FNAmntExtra = ISNULL(Y.FNQuantityExtra,0) * X.FNPrice,"
                    tsql &= Environment.NewLine & "    X.FNGrandAmnt = ISNULL(X.FNAmt, 0) + (ISNULL(Y.FNQuantityExtra, 0) * X.FNPrice) + (ISNULL(X.FNGarmentQtyTest,0) * X.FNPrice),"
                    tsql &= Environment.NewLine & "    X.FNAmntQtyTest = ISNULL(X.FNGarmentQtyTest,0) * X.FNPrice"
                    tsql &= Environment.NewLine & "   , X.FNExternalAmntQtyTest = Convert(numeric(18,2),(ISNULL(X.FNExternalQtyTest,0) * X.FNPrice))"
                    tsql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS X INNER JOIN (SELECT M2.FTOrderNo, M3.FTSubOrderNo,"
                    tsql &= Environment.NewLine & "                                                                        M4.FNHSysMatColorId, M4.FNHSysMatSizeId,"
                    tsql &= Environment.NewLine & "                                                                        M4.FNQuantity, M4.FNPrice, M4.FNAmt, M4.FNExtraQty, M4.FNQuantityExtra, M4.FNGrandQuantity,"
                    tsql &= Environment.NewLine & "                                                                        M4.FNAmntExtra, M4.FNGrandAmnt, M4.FNGarmentQtyTest,M4.FNExternalQtyTest"
                    tsql &= Environment.NewLine & "                                                                 FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS M1 WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS M2 WITH(NOLOCK) ON M1.FTGenerateOrderNo = M2.FTOrderNo"
                    tsql &= Environment.NewLine & " 																     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS M3 WITH(NOLOCK) ON M1.FTGenerateOrderNo = M3.FTOrderNo  AND M1.FTGenerateOrderSubNo =M3.FTSubOrderNo"
                    tsql &= Environment.NewLine & "  																     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS M4 WITH(NOLOCK) ON M3.FTOrderNo = M4.FTOrderNo"
                    tsql &= Environment.NewLine & " 																               AND M3.FTSubOrderNo = M4.FTSubOrderNo"
                    tsql &= Environment.NewLine & "   																     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS M5 WITH(NOLOCK) ON M4.FNHSysMatColorId = M5.FNHSysMatColorId"
                    tsql &= Environment.NewLine & " 																     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS M6 WITH(NOLOCK) ON M4.FNHSysMatSizeId = M6.FNHSysMatSizeId"
                    tsql &= Environment.NewLine & "                                                                 WHERE M1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    tsql &= Environment.NewLine & "           AND M1.FNRowImport IN (SELECT MAX(L1.FNRowImport)"
                    tsql &= Environment.NewLine & "                                FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS L1 WITH(NOLOCK)"
                    tsql &= Environment.NewLine & "                                WHERE L1.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    tsql &= Environment.NewLine & "                                      AND L1.FTPONo = M1.FTPONO"
                    tsql &= Environment.NewLine & "                                GROUP BY L1.FTPONo,FDShipDate,FTStyle,FNHSysBuyGrpId,FNHSysPlantId,FNGrpSeq) "

                    tsql &= Environment.NewLine & "                                                                       AND  M2.FTOrderNo NOT IN ('AA11BB22CC33DD44EE55ZZ99')"
                    tsql &= Environment.NewLine & " 															    ) AS Y ON X.FTOrderNo = Y.FTOrderNo"
                    tsql &= Environment.NewLine & " AND  X.FTSubOrderNo = Y.FTSubOrderNo"
                    tsql &= Environment.NewLine & "        AND X.FNHSysMatColorId = Y.FNHSysMatColorId"
                    tsql &= Environment.NewLine & "        AND X.FNHSysMatSizeId = Y.FNHSysMatSizeId"
                    tsql &= Environment.NewLine & "  UPDATE X"
                    tsql &= Environment.NewLine & " SET X.FNGrandQuantity = ISNULL(X.FNQuantity,0) + ISNULL(Y.FNQuantityExtra, 0)+ ISNULL(X.FNGarmentQtyTest, 0)+ ISNULL(X.FNExternalQtyTest, 0),"
                    tsql &= Environment.NewLine & "    X.FNAmntExtra = ISNULL(Y.FNQuantityExtra,0) * X.FNPrice,"
                    tsql &= Environment.NewLine & "    X.FNGrandAmnt = ISNULL(X.FNAmt, 0) + Convert(numeric(18,2),(ISNULL(Y.FNQuantityExtra, 0) * X.FNPrice)) + Convert(numeric(18,2),(ISNULL(X.FNGarmentQtyTest,0) * X.FNPrice)),"
                    tsql &= Environment.NewLine & "    X.FNAmntQtyTest = Convert(numeric(18,2),(ISNULL(X.FNGarmentQtyTest,0) * X.FNPrice))"
                    tsql &= Environment.NewLine & "   , X.FNExternalAmntQtyTest = Convert(numeric(18,2),(ISNULL(X.FNExternalQtyTest,0) * X.FNPrice))"
                    tsql &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS X INNER JOIN (SELECT M2.FTOrderNo, M3.FTSubOrderNo,"
                    tsql &= Environment.NewLine & "                                                                        M4.FNHSysMatColorId, M4.FNHSysMatSizeId,"
                    tsql &= Environment.NewLine & "                                                                        M4.FNQuantity, M4.FNPrice, M4.FNAmt, M4.FNExtraQty, M4.FNQuantityExtra, M4.FNGrandQuantity,"
                    tsql &= Environment.NewLine & "                                                                        M4.FNAmntExtra, M4.FNGrandAmnt, M4.FNGarmentQtyTest,M4.FNExternalQtyTest"
                    tsql &= Environment.NewLine & "                                                                 FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS M1 WITH(NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS M2 WITH(NOLOCK) ON M1.FTGenerateOrderNo = M2.FTOrderNo"
                    tsql &= Environment.NewLine & " 																     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS M3 WITH(NOLOCK) ON M1.FTGenerateOrderNo = M3.FTOrderNo  AND M1.FTGenerateOrderSubNo2 =M3.FTSubOrderNo "
                    tsql &= Environment.NewLine & "  																     LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS M4 WITH(NOLOCK) ON M3.FTOrderNo = M4.FTOrderNo"
                    tsql &= Environment.NewLine & " 																               AND M3.FTSubOrderNo = M4.FTSubOrderNo"
                    tsql &= Environment.NewLine & "   																     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS M5 WITH(NOLOCK) ON M4.FNHSysMatColorId = M5.FNHSysMatColorId"
                    tsql &= Environment.NewLine & " 																     INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS M6 WITH(NOLOCK) ON M4.FNHSysMatSizeId = M6.FNHSysMatSizeId"
                    tsql &= Environment.NewLine & "                                                                 WHERE M1.FTUserLogin = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    tsql &= Environment.NewLine & "                                                                       AND M1.FNRowImport IN (SELECT MAX(L1.FNRowImport)"
                    tsql &= Environment.NewLine & "                                FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTImportOrderTemp] AS L1 WITH(NOLOCK)"
                    tsql &= Environment.NewLine & "                                WHERE L1.FTUserLogin =N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    tsql &= Environment.NewLine & "                                      AND L1.FTPONo = M1.FTPONO"
                    tsql &= Environment.NewLine & "                                GROUP BY L1.FTPONo,FDShipDate,FTStyle,FNHSysBuyGrpId,FNHSysPlantId,FNGrpSeq) "

                    tsql &= Environment.NewLine & "                                                                       AND  M2.FTOrderNo NOT IN ('AA11BB22CC33DD44EE55ZZ99')"
                    tsql &= Environment.NewLine & " 															    ) AS Y ON X.FTOrderNo = Y.FTOrderNo"
                    tsql &= Environment.NewLine & " AND  X.FTSubOrderNo = Y.FTSubOrderNo"
                    tsql &= Environment.NewLine & "        AND X.FNHSysMatColorId = Y.FNHSysMatColorId"
                    tsql &= Environment.NewLine & "        AND X.FNHSysMatSizeId = Y.FNHSysMatSizeId"

                    If HI.Conn.SQLConn.ExecuteOnly(tsql, HI.Conn.DB.DataBaseName.DB_MERCHAN) = True Then


                        _bImportComplete = True

                    Else

                        'MsgBox("Step Append New TMERTOrderSub_BreakDown !!!")
                        _bImportComplete = False

                    End If


                    '==============================================================================================================================================================================
                Else
                    'MsgBox("พบปัญหาในการบันทึกรายการนำเข้าข้อมูลใบสั่งผลิต !!!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                End If

            End If

        Catch ex As Exception

        End Try

        Return _bImportComplete

    End Function

    Private Sub ogdmain_Click(sender As Object, e As EventArgs) Handles ogdmain.Click

    End Sub

    Private Sub ockAll_CheckedChanged(sender As Object, e As EventArgs) Handles ockAll.CheckedChanged
        Try
            Dim _State As String = "0"
            If Me.ockAll.Checked Then
                _State = "1"
            End If

            With ogdmain
                If Not (.DataSource Is Nothing) And ogvmain.RowCount > 0 Then

                    With ogvmain
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try
    End Sub


    Private Function MappingSuplierData() As Boolean

        Dim cmdstring As String = ""
        Dim dt As New DataTable
        Dim dtunit As New DataTable

        cmdstring = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.USP_FHS_SIZEMAPPING"
        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_FHS)

        If dt.Rows.Count > 0 Then
            With MappIngSize

                .LoadWisdomSize()
                .ogclist.DataSource = dt.Copy()
                .ShowDialog()

            End With

            Return False
        Else
            Return True
        End If
    End Function


    Private Function CheckDataBaseFHS() As Boolean

        Dim cmdstring As String = ""
        Dim dt As New DataTable


        cmdstring = " SELECT " & HI.UL.ULDate.FormatDateDB & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItems"

        If HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_FHS, "") = "" Then

            HI.MG.ShowMsg.mInfo("ไม่พบ Database FHS กรุณาทำการตรวจสอบ !!!", 120847554, Me.Text,, MessageBoxIcon.Warning)
            Return False
        Else
            Return True
        End If
    End Function


    Private Sub CheckMasterFileData()

        Call W_PRCbValidateExistsMasterSeason()
        Call W_PRCbValidateExistsMasterStyle()
        Call W_PRCbValidateExistsMatchColor()
        Call W_PRCbValidateExistsMasterGender()
        Call W_PRCbValidateExistsMasterShipMode()

    End Sub

    Private Function W_PRCbValidateExistsMasterSeason() As Boolean
        Dim bRet As Boolean = False
        Try

            Dim oDBdt As New DataTable

            tSql = " SELECT A.FTSeasonCode "
            tSql &= vbCrLf & " FROM (Select        BUY_SEASON + RIGHT(BUY_YEAR,2) As FTSeasonCode "
            tSql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderHeaders As A With(NOLOCK) "
            tSql &= vbCrLf & " GROUP BY BUY_SEASON + RIGHT(BUY_YEAR,2) ) AS A  "
            tSql &= vbCrLf & " OUTER APPLY (Select TOP 1  FTSeasonCode FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason As SS With (NOLOCK) WHERE SS.FTSeasonCode = A.FTSeasonCode) As SS"
            tSql &= vbCrLf & " WHERE ISNULL(SS.FTSeasonCode ,'') ='' "

            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_FHS)

            Dim tSeason As String
            For Each R As DataRow In oDBdt.Rows
                tSeason = R!FTSeasonCode.ToString

                Dim nFNHSysSeasonId As Integer

                nFNHSysSeasonId = Val(HI.TL.RunID.GetRunNoID("TMERMSeason", "FNHSysSeasonId", HI.Conn.DB.DataBaseName.DB_MASTER).ToString())
                Dim oStrBuilder As New System.Text.StringBuilder()

                oStrBuilder.Remove(0, oStrBuilder.Length)

                oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMSeason]([FTInsUser]")
                oStrBuilder.AppendLine("			,[FDInsDate]")
                oStrBuilder.AppendLine("			,[FTInsTime]")
                oStrBuilder.AppendLine("			,[FTUpdUser]")
                oStrBuilder.AppendLine("			,[FDUpdDate]")
                oStrBuilder.AppendLine("			,[FTUpdTime]")
                oStrBuilder.AppendLine("			,[FNHSysSeasonId]")
                oStrBuilder.AppendLine("			,[FTSeasonCode]")
                oStrBuilder.AppendLine("			,[FTSeasonNameTH]")
                oStrBuilder.AppendLine("			,[FTSeasonNameEN]")
                oStrBuilder.AppendLine("			,[FTRemark]")
                oStrBuilder.AppendLine("			,[FTStateActive])")
                oStrBuilder.AppendLine("VALUES(NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine(String.Format("      ,{0}", nFNHSysSeasonId))
                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tSeason) & "'")
                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tSeason) & "'")
                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tSeason) & "'")
                oStrBuilder.AppendLine("      ,''")
                oStrBuilder.AppendLine("      ,'1')")
                tSql = ""
                tSql = oStrBuilder.ToString()

                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) Then
                End If

            Next




        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            ' End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterStyle() As Boolean
        Dim bRet As Boolean = False
        Try


            Dim oDBdt As New DataTable


            tSql = "  Select  A.FTStyleCode,A.Material_Description,A.FTSeasonCode "
            tSql &= vbCrLf & " FROM(Select   LEFT(B.Material_Number, CHARINDEX('-',B.Material_Number)-1 ) AS FTStyleCode "
            tSql &= vbCrLf & "  , MAX(A.BUY_SEASON + Right(A.BUY_YEAR, 2)) AS FTSeasonCode	"
            tSql &= vbCrLf & ", MAX(B.Material_Description) As Material_Description	"
            tSql &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderHeaders AS A WITH(NOLOCK) INNER Join "
            tSql &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItems As B With(NOLOCK) On A.PO_Number = B.PO_Number "
            tSql &= vbCrLf & " Group By Left(B.Material_Number, CHARINDEX('-',B.Material_Number)-1 )  ) AS A "
            tSql &= vbCrLf & "OUTER APPLY (Select TOP 1  FTStyleCode FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As SS With (NOLOCK) WHERE SS.FTStyleCode = A.FTStyleCode) As SS "
            tSql &= vbCrLf & " Where ISNULL(SS.FTStyleCode,'') ='' "

            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_FHS)


            Dim tStyleCode As String
            Dim tStyleDesc As String
            For Each R As DataRow In oDBdt.Rows

                tStyleCode = R!FTStyleCode.ToString
                tStyleDesc = R!Material_Description.ToString

                Dim nFNHSysId As Integer

                nFNHSysId = Val(HI.TL.RunID.GetRunNoID("TMERMStyle", "FNHSysStyleId", HI.Conn.DB.DataBaseName.DB_MASTER).ToString())
                Dim oStrBuilder As New System.Text.StringBuilder()

                oStrBuilder.Remove(0, oStrBuilder.Length)

                oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle]([FTInsUser]")
                oStrBuilder.AppendLine("			,[FDInsDate]")
                oStrBuilder.AppendLine("			,[FTInsTime]")
                oStrBuilder.AppendLine("			,[FTUpdUser]")
                oStrBuilder.AppendLine("			,[FDUpdDate]")
                oStrBuilder.AppendLine("			,[FTUpdTime]")
                oStrBuilder.AppendLine("			,[FNHSysStyleId]")
                oStrBuilder.AppendLine("			,[FTStyleCode]")
                oStrBuilder.AppendLine("			,[FTStyleNameTH]")
                oStrBuilder.AppendLine("			,[FTStyleNameEN]")
                oStrBuilder.AppendLine("			,[FTRemark]")
                oStrBuilder.AppendLine("			,[FTStateActive])")
                oStrBuilder.AppendLine("VALUES(NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine("      ,NULL")
                oStrBuilder.AppendLine(String.Format("      ,{0}", nFNHSysId))
                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tStyleCode) & "'")
                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tStyleDesc) & "'")
                oStrBuilder.AppendLine("      ,N'" & HI.UL.ULF.rpQuoted(tStyleDesc) & "'")
                oStrBuilder.AppendLine("      ,''")
                oStrBuilder.AppendLine("      ,'1')")
                tSql = ""
                tSql = oStrBuilder.ToString()

                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) Then
                End If

            Next

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMatchColor() As Boolean
        Dim bRet As Boolean = False

        Try

            Dim oDBdt As New DataTable


            tSql = "  SELECT A.FTColor "
            tSql &= vbCrLf & " FROM (SELECT   RIGHT(B.Material_Number,LEN(B.Material_Number) - CHARINDEX('-',B.Material_Number) ) AS FTColor"
            tSql &= vbCrLf & " From   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItems As B With(NOLOCK) "
            tSql &= vbCrLf & " GROUP BY RIGHT(B.Material_Number,LEN(B.Material_Number) - CHARINDEX('-',B.Material_Number) ) ) AS A "
            tSql &= vbCrLf & " OUTER APPLY (SELECT TOP 1  FTMatColorCode FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatColor AS SS WITH (NOLOCK) WHERE SS.FTMatColorCode = A.FTColor) AS SS  "
            tSql &= vbCrLf & " WHERE ISNULL(SS.FTMatColorCode ,'') ='' "

            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_FHS)


            Dim tColorCode As String

            For Each R As DataRow In oDBdt.Rows

                tColorCode = R!FTColor.ToString


                Dim nFNHSysMatColorId As Integer

                nFNHSysMatColorId = Val(HI.TL.RunID.GetRunNoID("TMERMMatColor", "FNHSysMatColorId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                tSql = ""
                tSql = "DECLARE @nFNMatColorId AS INT;"
                tSql &= Environment.NewLine & "DECLARE @tFTMatColorCode AS NVARCHAR(30);"
                tSql &= Environment.NewLine & "DECLARE @tFTMatColorDesc AS NVARCHAR(100);"
                tSql &= Environment.NewLine & "SET @nFNMatColorId = " & nFNHSysMatColorId & ";"
                tSql &= Environment.NewLine & "SET @tFTMatColorCode = N'" & HI.UL.ULF.rpQuoted(tColorCode) & "';"
                tSql &= Environment.NewLine & "SET @tFTMatColorDesc = N'" & HI.UL.ULF.rpQuoted(tColorCode) & "';"
                tSql &= Environment.NewLine & "DECLARE @FNMatColorSeq AS INT;"
                tSql &= Environment.NewLine & "SELECT @FNMatColorSeq = ISNULL(MAX(A.FNMatColorSeq),0)"
                tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatColor] AS A"
                tSql &= Environment.NewLine & "GROUP BY A.FNMatColorSeq;"
                tSql &= Environment.NewLine & "--PRINT 'FNMatColorSeq Current : ' + CONVERT(VARCHAR(10), @FNMatColorSeq);"
                tSql &= Environment.NewLine & "--PRINT 'FNMatColorSeq Next : ' + CONVERT(VARCHAR(10), (@FNMatColorSeq + 1));"
                tSql &= Environment.NewLine & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TMERMMatColor]([FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "                                                 ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tSql &= Environment.NewLine & "                                                 ,[FNHSysMatColorId],[FTMatColorCode],[FNMatColorSeq]"
                tSql &= Environment.NewLine & "                                                 ,[FTMatColorNameTH],[FTMatColorNameEN],[FTRemark],[FTStateActive])"
                tSql &= Environment.NewLine & "VALUES(NULL, NULL, NULL"
                tSql &= Environment.NewLine & ",NULL, NULL, NULL"
                tSql &= Environment.NewLine & ",@nFNMatColorId, @tFTMatColorCode, (ISNULL(@FNMatColorSeq, 0) + 1)"
                tSql &= Environment.NewLine & ",@tFTMatColorDesc, @tFTMatColorDesc, '', '1');"

                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                    'MsgBox("Execuete data complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End If

            Next

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterGender() As Boolean
        Dim bRet As Boolean = False
        Try


            Dim oDBdt As New DataTable


            tSql = " SELECT A.FTGender "
            tSql &= vbCrLf & " FROM (Select   GD.FTGender "
            tSql &= vbCrLf & " FROM	[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItems AS B WITH(NOLOCK)  "
            tSql &= vbCrLf & "	OUTER APPLY (Select TOP 1  [DESC] As FTGender  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FHS) & "].dbo.TMGENDER_AGE_CODE As SS With (NOLOCK) WHERE SS.GNDR_AGE = B.Gender_Age_Code) As GD "
            tSql &= vbCrLf & " WHERE ISNULL(GD.FTGender,'') <> '' "
            tSql &= vbCrLf & " GROUP BY GD.FTGender ) As A "
            tSql &= vbCrLf & " OUTER APPLY (SELECT TOP 1  FTGenderNameEN FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMGender AS SS WITH (NOLOCK) WHERE SS.FTGenderNameEN = A.FTGender) AS SS "
            tSql &= vbCrLf & " WHERE ISNULL(SS.FTGenderNameEN ,'') ='' "


            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_FHS)


            Dim tMatchGender As String

            For Each R As DataRow In oDBdt.Rows

                tMatchGender = R!FTGender.ToString


                Dim nFNHSysGenderId As Integer

                nFNHSysGenderId = Val(HI.TL.RunID.GetRunNoID("TMERMGender", "FNHSysGenderId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                tSql = ""
                tSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMGender]([FTInsUser],[FDInsDate],[FTInsTime]"
                tSql &= Environment.NewLine & "                        ,[FTUpdUser],[FDUpdDate],[FTUpdTime]"
                tSql &= Environment.NewLine & "                        ,[FNHSysGenderId],[FTGenderCode],[FTGenderNameTH]"
                tSql &= Environment.NewLine & "                        ,[FTGenderNameEN],[FTRemark],[FTStateActive])"
                tSql &= Environment.NewLine & "VALUES(NULL, NULL, NULL"
                tSql &= "                            ,NULL, NULL , NULL"
                tSql &= "                            ," & nFNHSysGenderId & ", N'" & HI.UL.ULF.rpQuoted(tMatchGender) & "', N'" & HI.UL.ULF.rpQuoted(tMatchGender) & "'"
                tSql &= "                            ,N'" & HI.UL.ULF.rpQuoted(tMatchGender) & "', '', '1')"

                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                    'MsgBox("Execuete data complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End If

            Next


        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            ' End If
        End Try

        Return bRet

    End Function

    Private Function W_PRCbValidateExistsMasterShipMode() As Boolean
        Dim bRet As Boolean = False
        Try

            Dim oDBdt As New DataTable


            tSql = " SELECT A.Mode_Code "
            tSql &= vbCrLf & " FROM (SELECT   B.Mode_Code "
            tSql &= vbCrLf & " FROM	[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_FHS) & "].dbo.OrderItems AS B WITH(NOLOCK)  "

            tSql &= vbCrLf & " WHERE ISNULL(B.Mode_Code,'') <> '' "
            tSql &= vbCrLf & " GROUP BY B.Mode_Code ) AS A "
            tSql &= vbCrLf & " OUTER APPLY (SELECT TOP 1  FTShipModeCode FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipMode AS SS WITH (NOLOCK) WHERE SS.FTShipModeCode = A.Mode_Code) AS SS "
            tSql &= vbCrLf & " WHERE ISNULL(SS.FTShipModeCode ,'') ='' "


            oDBdt = HI.Conn.SQLConn.GetDataTable(tSql, Conn.DB.DataBaseName.DB_FHS)


            Dim tShipMode As String
            Dim nFNHSysShipModeId As Integer


            For Each R As DataRow In oDBdt.Rows


                tShipMode = R!Mode_Code.ToString()



                nFNHSysShipModeId = Val(HI.TL.RunID.GetRunNoID("TCNMShipMode", "FNHSysShipModeId", Conn.DB.DataBaseName.DB_MASTER).ToString())

                Dim oStrBuilder As New System.Text.StringBuilder()

                oStrBuilder.Remove(0, oStrBuilder.Length)

                oStrBuilder.AppendLine("INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMShipMode]")
                oStrBuilder.AppendLine("           ([FTInsUser]")
                oStrBuilder.AppendLine("           ,[FDInsDate]")
                oStrBuilder.AppendLine("           ,[FTInsTime]")
                oStrBuilder.AppendLine("           ,[FTUpdUser]")
                oStrBuilder.AppendLine("           ,[FDUpdDate]")
                oStrBuilder.AppendLine("           ,[FTUpdTime]")
                oStrBuilder.AppendLine("           ,[FNHSysShipModeId]")
                oStrBuilder.AppendLine("           ,[FTShipModeCode]")
                oStrBuilder.AppendLine("           ,[FTShipModenNameTH]")
                oStrBuilder.AppendLine("           ,[FTShipModeNameEN]")
                oStrBuilder.AppendLine("           ,[FTRemark]")
                oStrBuilder.AppendLine("           ,[FTStateActive])")
                oStrBuilder.AppendLine("VALUES (NULL")
                oStrBuilder.AppendLine("       ,NULL")
                oStrBuilder.AppendLine("       ,NULL")
                oStrBuilder.AppendLine("       ,NULL")
                oStrBuilder.AppendLine("       ,NULL")
                oStrBuilder.AppendLine("       ,NULL")
                oStrBuilder.AppendLine(String.Format("       ,{0}", nFNHSysShipModeId))
                oStrBuilder.AppendLine("       ,N'" & HI.UL.ULF.rpQuoted(tShipMode) & "'")
                oStrBuilder.AppendLine("       ,N'" & HI.UL.ULF.rpQuoted(tShipMode) & "'")
                oStrBuilder.AppendLine("       ,N'" & HI.UL.ULF.rpQuoted(tShipMode) & "'")
                oStrBuilder.AppendLine("       ,''")
                oStrBuilder.AppendLine("       ,'1')")

                tSql = ""
                tSql = oStrBuilder.ToString()

                If HI.Conn.SQLConn.ExecuteNonQuery(tSql, HI.Conn.DB.DataBaseName.DB_MASTER) = True Then
                    'MsgBox("Execute data complete ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End If

            Next


        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            'End If
        End Try

        Return bRet

    End Function




#End Region

End Class
