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
Imports Newtonsoft.Json
Imports System.ComponentModel

Public Class wOrdersDataGet


    Dim View As GridView
    Dim RowsIndex As Double
    Dim TopVisibleIndex As Int32
    Private sFNHSysStyleId As String

    ''' Used Data Adapter to control database

    Dim oleDbDataAdapter1 As DbDataAdapter
    Dim oleDbDataAdapter2 As DbDataAdapter
    Dim dtStyleDetail As DataTable

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
        HI.TL.HandlerControl.AddHandlerObj(Me)

    End Sub


    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmrefresh.Click
        Dim Spls As New HI.TL.SplashScreen("Loading.... please wait")



        Call GetWebService()
        ' GetTokenData()
        Spls.Close()
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Function GetTokenData() As Boolean
        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)

        Dim PageCount As Integer = 0
        Dim urlEndPoint As String = "http://192.168.99.152/OLLIe/OLLIe.svc?wsdl" '"http://192.168.99.152/OLLIe/OLLIe.svc"
        Dim soapAction As String = "http://192.168.99.152/OLLIe/OLLIe.svc/Orders"
        ' Refer to the documentation for more information on how to get the client id/secret
        ' urlEndPoint = soapAction
        ' Refer to the documentation for more information on how to get the tokens
        Dim accessToken As String = ""
        Dim jsonResponseText As String = ""
        Dim BuyGroup As String = "01" 'FNHSysBuyGrpId.Text
        Dim FactoryCode As String = "HIT" 'FNHSysVenderPramId.Text
        Dim GACDateBegin As String = HI.UL.ULDate.ConvertEnDB(FTStartGacDate.Text).Replace("/", "-")
        Dim GACDateEnd As String = HI.UL.ULDate.ConvertEnDB(FTEndGacDate.Text).Replace("/", "-")
        Dim OrderDocDateBegin As String = HI.UL.ULDate.ConvertEnDB(FTStartDocDate.Text).Replace("/", "-")
        Dim OrderDocDateEnd As String = HI.UL.ULDate.ConvertEnDB(FTEndDocDate.Text).Replace("/", "-")


        Dim FHubXML As New FactoryHub.FactoryHubXML()
        Dim json_data As String = FHubXML.OrderDataGetXMLRequest(BuyGroup, FactoryCode, GACDateBegin, GACDateEnd, OrderDocDateBegin, OrderDocDateEnd)

        Dim postBytes() As Byte = System.Text.Encoding.ASCII.GetBytes(json_data)


        Dim responsestring As String = ""
        ' -- Refresh the access token
        Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(urlEndPoint)
        request.Headers.Add("SOAPAction", urlEndPoint)
        request.UseDefaultCredentials = True
        request.PreAuthenticate = True
        request.Credentials = CredentialCache.DefaultCredentials

        request.Method = "POST"
        request.ContentType = "text/xml; charset=utf-8"



        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
        Dim postStream As Stream = request.GetRequestStream()
        postStream.Write(postBytes, 0, postBytes.Length)
        postStream.Flush()
        postStream.Close()



        Try
            ' ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
            Using response As System.Net.WebResponse = request.GetResponse()
                Using streamReader As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
                    ' Parse the JSON the way you prefer
                    jsonResponseText = streamReader.ReadToEnd()

                End Using
            End Using
        Catch ex As Exception
            jsonResponseText = ""
        End Try

        If jsonResponseText <> "" Then




        Else
            Return False
        End If


        Return True

    End Function

    Private Function GetWebService() As Boolean
        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)

        Dim PageCount As Integer = 0
        Dim urlEndPoint As String = "http://192.168.99.152/OLLIe/OLLIe.svc/Orders" '"http://192.168.99.152/OLLIe/OLLIe.svc?wsdl"
        Dim soapAction As String = "http://tempuri.org/IOrders/OrdersDataGet"
        ' Refer to the documentation for more information on how to get the client id/secret
        ' urlEndPoint = soapAction
        ' Refer to the documentation for more information on how to get the tokens
        Dim accessToken As String = ""
        Dim jsonResponseText As String = ""
        Dim BuyGroup As String = "01" 'FNHSysBuyGrpId.Text
        Dim FactoryCode As String = "HIT" 'FNHSysVenderPramId.Text
        Dim GACDateBegin As String = HI.UL.ULDate.ConvertEnDB(FTStartGacDate.Text).Replace("/", "-")
        Dim GACDateEnd As String = HI.UL.ULDate.ConvertEnDB(FTEndGacDate.Text).Replace("/", "-")
        Dim OrderDocDateBegin As String = HI.UL.ULDate.ConvertEnDB(FTStartDocDate.Text).Replace("/", "-")
        Dim OrderDocDateEnd As String = HI.UL.ULDate.ConvertEnDB(FTEndDocDate.Text).Replace("/", "-")


        Dim FHubXML As New FactoryHub.FactoryHubXML()
        Dim json_data As String = FHubXML.OrderDataGetXMLRequest(BuyGroup, FactoryCode, GACDateBegin, GACDateEnd, OrderDocDateBegin, OrderDocDateEnd)

        Dim data As String = json_data
        ' Dim url As String = "http://192.168.99.152/OLLIe/OLLIe.svc?wsdl"
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

                    For Each tablename As String In {"DataGetOrderHeaders", "DataGetOrderItems", "DataGetOrderSizes", "DataGetOrderItemsVas", "DataGetOrderSizesVas", "DataGetOrderItemsText"}
                        Select Case tablename
                            Case "DataGetOrderHeaders"
                                ogcdt1.DataSource = theDataSet.Tables(tablename).Copy
                            Case "DataGetOrderItems"
                                ogcdt2.DataSource = theDataSet.Tables(tablename).Copy
                            Case "DataGetOrderSizes"
                                ogcdt3.DataSource = theDataSet.Tables(tablename).Copy
                            Case "DataGetOrderItemsVas"
                                ogcdt4.DataSource = theDataSet.Tables(tablename).Copy
                            Case "DataGetOrderSizesVas"
                                ogcdt5.DataSource = theDataSet.Tables(tablename).Copy
                            Case "DataGetOrderItemsText"
                                ogcdt6.DataSource = theDataSet.Tables(tablename).Copy
                        End Select
                    Next

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


#End Region

End Class
