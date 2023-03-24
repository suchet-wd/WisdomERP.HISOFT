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

Public Class wFactoryHubOrder

    Private Shared _MContextMenuStripGrid As System.Windows.Forms.ContextMenuStrip
    Private Shared _MContextMenuStripGridSetFlags As System.Windows.Forms.ContextMenuStrip
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

    Private PathFileXML As String = System.Windows.Forms.Application.StartupPath & "\FHSXML\"

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

        Call CreateManuStripGrid()


        Me.ogcdt1.ContextMenuStrip = _MContextMenuStripGrid
        Me.ogcdt2.ContextMenuStrip = _MContextMenuStripGridSetFlags
        Me.ogcdt3.ContextMenuStrip = Nothing
        Me.ogcdt4.ContextMenuStrip = Nothing
        Me.ogcdt5.ContextMenuStrip = Nothing
        Me.ogcdt6.ContextMenuStrip = Nothing

    End Sub

    Private Sub CreateManuStripGrid()

        _MContextMenuStripGrid = New System.Windows.Forms.ContextMenuStrip
        _MContextMenuStripGridSetFlags = New System.Windows.Forms.ContextMenuStrip

        Dim _ShowToPDF As New System.Windows.Forms.ToolStripMenuItem
        Dim _ShowSetFlags As New System.Windows.Forms.ToolStripMenuItem

        With _ShowToPDF
            .Name = "ocmShowPDF"
            .Text = "Show PDF File"


            AddHandler .Click, AddressOf ShowToPDFl_Click
        End With

        With _ShowSetFlags
            .Name = "ocmShowPDF"
            .Text = "Set New Flags"


            AddHandler .Click, AddressOf ShowSetFlags_Click
        End With

        With _MContextMenuStripGrid
            .Name = "ContextExportDataGridControl"
            .Items.AddRange(New System.Windows.Forms.ToolStripItem() {_ShowToPDF})
        End With


        With _MContextMenuStripGridSetFlags
            .Name = "ContextExportDataGridControlSetFlags"
            .Items.AddRange(New System.Windows.Forms.ToolStripItem() {_ShowSetFlags})
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


    Private Sub ShowSetFlags_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            Dim FTFacCode As String = FNHSysVenderPramId.Text
            Dim FTPONo As String = ""
            Dim FTPOLineNo As String = ""

            With CType(CType(sender.Owner, ContextMenuStrip).SourceControl, DevExpress.XtraGrid.GridControl)

                Dim _ObjMainView As Object = .MainView

                Dim StateSetFlag As Boolean = False

                Dim Spls As New HI.TL.SplashScreen("Sending Request Update Flag For UnAcknowlage Data... ")
                Try
                    For Each i As Integer In _ObjMainView.GetSelectedRows()
                        Try

                            FTPONo = (_ObjMainView.GetRowCellValue(i, "PO_Number").ToString())
                            FTPOLineNo = (_ObjMainView.GetRowCellValue(i, "PO_Item").ToString())

                        Catch ex As Exception

                            FTPONo = ""
                            FTPOLineNo = ""
                        End Try

                        If FTFacCode <> "" And FTPONo <> "" And FTPOLineNo <> "" Then
                            StateSetFlag = GetWebServiceOrdersSetFlags(FTFacCode, FTPONo, FTPOLineNo)
                        End If


                    Next
                Catch ex As Exception

                End Try
                Spls.Close()

                If StateSetFlag Then
                    MsgBox("Update Flag For UnAcknowlage Data Complete...")
                End If

            End With



        Catch ex As Exception
        End Try
    End Sub


    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmrefresh.Click

        Dim Spls As New HI.TL.SplashScreen("Loading.... please wait")


        Call GetWebServiceOrdersDataGet()
        ' GetTokenData()

        Call SetFilerColumn()

        Spls.Close()

    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Function GetWebServiceOrdersDataGet() As Boolean
        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)

        Dim PageCount As Integer = 0
        Dim urlEndPoint As String = "http://192.168.99.156/OLLIe/OLLIe.svc/Orders" '"http://192.168.99.156/OLLIe/OLLIe.svc?wsdl"
        Dim soapAction As String = "http://tempuri.org/IOrders/OrdersDataGet"
        ' Refer to the documentation for more information on how to get the client id/secret
        ' urlEndPoint = soapAction
        ' Refer to the documentation for more information on how to get the tokens
        Dim accessToken As String = ""
        Dim jsonResponseText As String = ""
        Dim BuyGroup As String = Microsoft.VisualBasic.Right("000" & FNHSysBuyGrpId.Text, 2)
        Dim FactoryCode As String = FNHSysVenderPramId.Text
        Dim GACDateBegin As String = HI.UL.ULDate.ConvertEnDB(FTStartGacDate.Text).Replace("/", "-")
        Dim GACDateEnd As String = HI.UL.ULDate.ConvertEnDB(FTEndGacDate.Text).Replace("/", "-")
        Dim OrderDocDateBegin As String = HI.UL.ULDate.ConvertEnDB(FTStartDocDate.Text).Replace("/", "-")
        Dim OrderDocDateEnd As String = HI.UL.ULDate.ConvertEnDB(FTEndDocDate.Text).Replace("/", "-")
        Dim FHubXML As New FactoryHub.FactoryHubXML()
        Dim json_data As String = FHubXML.OrderDataGetXMLRequest(BuyGroup, FactoryCode, GACDateBegin, GACDateEnd, OrderDocDateBegin, OrderDocDateEnd)

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

                    SaveXML(response)

                    For Each tablename As String In {"DataGetOrderHeaders", "DataGetOrderItems", "DataGetOrderSizes", "DataGetOrderItemsVas", "DataGetOrderSizesVas", "DataGetOrderItemsText"}
                        Select Case tablename
                            Case "DataGetOrderHeaders"
                                Try
                                    ogcdt1.DataSource = theDataSet.Tables(tablename).Copy
                                    ogvdt1.BestFitColumns()
                                Catch ex As Exception
                                    ogcdt1.DataSource = Nothing
                                End Try

                            Case "DataGetOrderItems"

                                Try
                                    ogcdt2.DataSource = theDataSet.Tables(tablename).Copy
                                    ogvdt2.BestFitColumns()
                                Catch ex As Exception
                                    ogcdt2.DataSource = Nothing
                                End Try
                            Case "DataGetOrderSizes"
                                Try
                                    ogcdt3.DataSource = theDataSet.Tables(tablename).Copy
                                    ogvdt3.BestFitColumns()
                                Catch ex As Exception
                                    ogcdt3.DataSource = Nothing
                                End Try

                            Case "DataGetOrderItemsVas"
                                Try
                                    ogcdt4.DataSource = theDataSet.Tables(tablename).Copy
                                    ogvdt4.BestFitColumns()
                                Catch ex As Exception
                                    ogcdt4.DataSource = Nothing
                                End Try

                            Case "DataGetOrderSizesVas"
                                Try
                                    ogcdt5.DataSource = theDataSet.Tables(tablename).Copy
                                    ogvdt5.BestFitColumns()
                                Catch ex As Exception
                                    ogcdt5.DataSource = Nothing
                                End Try

                            Case "DataGetOrderItemsText"
                                Try
                                    ogcdt6.DataSource = theDataSet.Tables(tablename).Copy
                                    ogvdt6.BestFitColumns()
                                Catch ex As Exception
                                    ogcdt6.DataSource = Nothing
                                End Try

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
            MsgBox(ex.Message())
            Return False
        End Try



        Return True

    End Function

    Private Function SaveXML(response As String) As Boolean
        'Dim _Qry As String

        'Try

        '    Dim theReader As New StringReader(response)

        '    Dim theDataSet As New DataSet
        '    theDataSet.ReadXml(theReader)

        '    Dim doc As New XmlDocument
        '    doc.Load(response)
        '    Dim XX As New SqlTypes.SqlXml(New MemoryStream(System.IO.File.ReadAllBytes(response)))

        '    HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_FHS)
        '    HI.Conn.SQLConn.SqlConnectionOpen()


        '    Dim cmd As New SqlCommand("USP_INSERT_XML", HI.Conn.SQLConn.Cnn)
        '    cmd.CommandType = CommandType.StoredProcedure
        '    cmd.Parameters.AddWithValue("@User", HI.ST.UserInfo.UserName)
        '    cmd.Parameters.AddWithValue("@FileName", "TESTTTTT")
        '    cmd.Parameters.AddWithValue("@xml", New SqlTypes.SqlXml(New XmlNodeReader(doc)))
        '    cmd.ExecuteNonQuery()

        '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)

        '    doc = Nothing
        '    Return True
        'Catch ex As Exception
        '    Return False
        'End Try

    End Function

    Private Sub otb_Click(sender As Object, e As EventArgs) Handles otb.Click

    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
    Private Function GetWebServiceOrdersDataGetUnAcknowledged() As Boolean
        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)

        Dim PageCount As Integer = 0
        Dim urlEndPoint As String = "http://192.168.99.156/OLLIe/OLLIe.svc/Orders" '"http://192.168.99.156/OLLIe/OLLIe.svc?wsdl"
        Dim soapAction As String = "http://tempuri.org/IOrders/OrdersDataGetUnAcknowledged"
        ' Refer to the documentation for more information on how to get the client id/secret
        ' urlEndPoint = soapAction
        ' Refer to the documentation for more information on how to get the tokens
        Dim accessToken As String = ""
        Dim jsonResponseText As String = ""

        Dim FactoryCode As String = FNHSysVenderPramId.Text
        Dim _XMLFIleName As String = DateTime.Now().ToString().Replace(" ", "_").Replace("/", "_").Replace(":", "_")


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

                        theDataSet.WriteXml(PathFileXML & "OrdersDataGetUnAcknowledged_" & _XMLFIleName & ".XML")

                        SaveXML(PathFileXML & "OrdersDataGetUnAcknowledged_" & _XMLFIleName & ".XML", _XMLFIleName)

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

                        SaveOrder(OrderHeaders, OrderItems, OrderSizes, OrderItemsVas, OrderSizesVas, OrderItemsText)

                    End If

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


    Private Function SaveXML(XMLString As String, FileName As String) As Boolean
        Dim _Qry As String

        Try

            Dim doc As New XmlDocument
            doc.Load(XMLString)

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_FHS)
            HI.Conn.SQLConn.SqlConnectionOpen()


            Dim cmd As New SqlCommand("USP_INSERT_XML", HI.Conn.SQLConn.Cnn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@User", HI.ST.UserInfo.UserName)
            cmd.Parameters.AddWithValue("@FileName", FileName)
            cmd.Parameters.AddWithValue("@xml", New SqlTypes.SqlXml(New XmlNodeReader(doc)))
            cmd.ExecuteNonQuery()

            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub SaveOrder(OrderHeaders As DataTable, OrderItems As DataTable, OrderSizes As DataTable, OrderItemsVas As DataTable, OrderSizesVas As DataTable, OrderItemsText As DataTable)

        Dim PORef As String = ""
        Dim LineItemNo As String = ""
        Dim State As Boolean = False

        Dim CmdInsertOrderItems As String = ""

        Dim cmdstring As String = ""
        Dim ColIdx As Integer = 0


        For Each R As DataRow In OrderHeaders.Rows

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

        Next


    End Sub




    Private Sub SetFilerColumn()



        Try
            For Each c As GridColumn In ogvdt1.Columns
                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
            Next

            For Each c As GridColumn In ogvdt2.Columns
                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
            Next

            For Each c As GridColumn In ogvdt3.Columns
                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
            Next

            For Each c As GridColumn In ogvdt4.Columns
                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
            Next

            For Each c As GridColumn In ogvdt5.Columns
                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
            Next

            For Each c As GridColumn In ogvdt6.Columns
                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
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

    Private Function GetWebServiceOrdersSetFlags(FactoryCode As String, OrderNumber As String, OrderItemNumber As String) As Boolean
        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
        Dim ReturnDescription As String = ""
        Dim PageCount As Integer = 0
        Dim urlEndPoint As String = "http://192.168.99.156/OLLIe/OLLIe.svc/Orders" '"http://192.168.99.156/OLLIe/OLLIe.svc?wsdl"
        Dim soapAction As String = "http://tempuri.org/IOrders/OrdersDataGetSetFlags"
        ' Refer to the documentation for more information on how to get the client id/secret
        ' urlEndPoint = soapAction
        ' Refer to the documentation for more information on how to get the tokens
        Dim accessToken As String = ""
        Dim jsonResponseText As String = ""


        Dim FHubXML As New FactoryHub.FactoryHubXML()
        Dim json_data As String = FHubXML.OrdersDataGetSetFlagsXMLRequest(FactoryCode, OrderNumber, OrderItemNumber, FactoryHub.FactoryHubXML.FSHSetFlags.Order_Flag_Y)

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

                    If theDataSet.Tables.IndexOf("OutputMessage") >= 0 Then

                        ReturnDescription = theDataSet.Tables("OutputMessage").Rows(0)!ReturnDescription.ToString()

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

        'If ReturnDescription <> "" Then
        '    MsgBox(ReturnDescription)
        'End If


        Return True
    End Function


#End Region

End Class
