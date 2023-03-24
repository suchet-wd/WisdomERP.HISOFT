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

Public Class wBomAutomate


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


    End Sub



    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmrefresh.Click
        Dim Spls As New HI.TL.SplashScreen("Loading.... please wait")

        otb.TabPages.Clear()

        GetTokenData()
        Spls.Close()
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Function GetTokenData() As Boolean
        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)

        Dim dtbomheader As DataTable = CreateTableBomHeader()
        Dim dtbomItem As DataTable = CreateTableBomLineItem()
        Dim PageCount As Integer = 0
        Dim urlEndPoint As String = "https://nike-qa.oktapreview.com/oauth2/ausa0mcornpZLi0C40h7/v1/token"
        ' Refer to the documentation for more information on how to get the client id/secret
        Dim clientid As String = "testfactory.gsm.bom"
        Dim clientsecret As String = "Ttpx9N1Qpr-thAdjgWVpyMsOKdce0ECElm8XWoAdL7sHH6u0AIOQ0RgfjH59rQb_"
        Dim granttype As String = "client_credentials"
        ' Refer to the documentation for more information on how to get the tokens
        Dim accessToken As String = ""


        ' -- Refresh the access token
        Dim request As System.Net.WebRequest = System.Net.HttpWebRequest.Create(urlEndPoint)
        request.UseDefaultCredentials = True
        request.PreAuthenticate = True
        request.Credentials = CredentialCache.DefaultCredentials

        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"


        Dim json_data As String = String.Format("client_id={0}&client_secret={1}&grant_type=client_credentials", System.Web.HttpUtility.UrlEncode(clientid), System.Web.HttpUtility.UrlEncode(clientsecret))

        Dim postBytes As Byte() = System.Text.Encoding.ASCII.GetBytes(json_data) 'New ASCIIEncoding().GetBytes(json_data)

        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
        Dim postStream As Stream = request.GetRequestStream()
        postStream.Write(postBytes, 0, postBytes.Length)
        postStream.Flush()
        postStream.Close()


        Try
            ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
            Using response As System.Net.WebResponse = request.GetResponse()
                Using streamReader As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
                    ' Parse the JSON the way you prefer
                    Dim jsonResponseText = streamReader.ReadToEnd()
                    Dim jsonResult As RefreshTokenResultJSON = JsonConvert.DeserializeObject(Of RefreshTokenResultJSON)(jsonResponseText)
                    accessToken = jsonResult.access_token
                    ' For more information, refer to the documentation
                    ' accessToken = streamReader.ReadToEnd()
                End Using
            End Using
        Catch ex As Exception

        End Try

        If accessToken <> "" Then

            Dim jsonBomResult As RefreshBomJSON = Nothing
            Dim DataPerPage As String = ""

            If FTFixrecord.Checked Then
                If FNRecord.Value > 0 Then
                    Dim Rcdata As Integer = FNRecord.Value
                    DataPerPage = "/?count=" & Rcdata.ToString
                End If
            End If


            Dim urlBom As String = "https://2lglxh787c.execute-api.us-east-1.amazonaws.com/dev/product/boms_edge/v1" & DataPerPage
                Dim urlBomPage As String = "https://2lglxh787c.execute-api.us-east-1.amazonaws.com/dev"
                Dim xapikey As String = "4B4I0gZQvU3SAG4s3Qzhw5b1uahxu0sx8ZKch5zv"

                ' -- Get current user profile
                request = System.Net.HttpWebRequest.Create(urlBom)
                request.Method = "GET"
                request.Headers.Add("x-api-key", xapikey)
                request.Headers.Add("Authorization", "Bearer " & accessToken)
                Using response As System.Net.WebResponse = request.GetResponse()
                    Using streamReader As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
                        Dim jsonResponseText = streamReader.ReadToEnd()

                        jsonBomResult = JsonConvert.DeserializeObject(Of RefreshBomJSON)(jsonResponseText)


                    End Using
                End Using

                If jsonBomResult IsNot Nothing Then
                    PageCount = PageCount + 1
                    BomHeaderToDataTable(jsonBomResult.objects, PageCount, dtbomheader, dtbomItem)

                    Do While (jsonBomResult.pages IsNot Nothing AndAlso jsonBomResult.pages.next <> "")

                        Dim urlBomPageData As String = urlBomPage + jsonBomResult.pages.next

                        jsonBomResult = Nothing

                        ' -- Get current user profile
                        request = System.Net.HttpWebRequest.Create(urlBomPageData)
                        request.Method = "GET"
                        request.Headers.Add("x-api-key", xapikey)
                        request.Headers.Add("Authorization", "Bearer " & accessToken)
                        Using response As System.Net.WebResponse = request.GetResponse()
                            Using streamReader As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
                                Dim jsonResponseText = streamReader.ReadToEnd()

                                jsonBomResult = JsonConvert.DeserializeObject(Of RefreshBomJSON)(jsonResponseText)

                            End Using
                        End Using

                        If jsonBomResult IsNot Nothing Then
                            PageCount = PageCount + 1
                            BomHeaderToDataTable(jsonBomResult.objects, PageCount, dtbomheader, dtbomItem)
                        End If
                    Loop
                End If

                If dtbomheader.Rows.Count > 0 Then
                    Dim grp As List(Of Integer) = (dtbomheader.Select("FNPage<>0", "FNPage").CopyToDataTable).AsEnumerable() _
                                          .Select(Function(r) r.Field(Of Integer)("FNPage")) _
                                          .Distinct() _
                                          .ToList()

                    For Each PangNum As Integer In grp

                        Dim dtdata As DataTable = dtbomheader.Select("FNPage = " & PangNum & " ").CopyToDataTable()

                        Dim Otp As New DevExpress.XtraTab.XtraTabPage()
                        With Otp
                            .Name = "xbompagenum_" & PangNum.ToString
                            .Text = "Bom Page No . " & PangNum.ToString
                        End With
                        otb.TabPages.Add(Otp)

                        Dim _UCrl As New UBompage()

                        Otp.Controls.Add(_UCrl)
                    _UCrl.Dock = System.Windows.Forms.DockStyle.Fill
                    _UCrl.ogdbom.DataSource = dtdata.Copy

                    Next
                End If
                '' -- List the current data
                'request = System.Net.HttpWebRequest.Create(urlBom)
                'request.Method = "GET"
                'request.Headers.Add("x-api-key", xapikey)
                'request.Headers.Add("Authorization", "Bearer " & accessToken)
                'Using response As System.Net.WebResponse = request.GetResponse()
                '    Using streamReader As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
                '        Dim jsonResponseText = streamReader.ReadToEnd()
                '        ' Parse the JSON the way you prefer
                '        ' In this example the JSON will be something Like:
                '        ' { "count" 265, "data": [{ "description": "","source": "merge", "creation_date": "2015−06−10T12:47:32+02:00", "last_data_update_date": "2015−06−10T12:47:49+02:00", ...
                '        ' For more information, refer to the documentation
                '    End Using
                'End Using



            Else
                Return False
        End If




    End Function

    Private Function CreateTableBomHeader() As DataTable
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(BomHeader))
        Dim dt As New DataTable()
        For i As Integer = 0 To properties.Count - 1
            Dim [property] As PropertyDescriptor = properties(i)
            dt.Columns.Add([property].Name, [property].PropertyType)
        Next

        dt.Columns.Add("FNPage", GetType(Integer))

        Return dt

    End Function

    Private Function CreateTableBomLineItem() As DataTable
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(BomLineItem))
        Dim dt As New DataTable()
        For i As Integer = 0 To properties.Count - 1
            Dim [property] As PropertyDescriptor = properties(i)
            dt.Columns.Add([property].Name, [property].PropertyType)
        Next
        dt.Columns.Add("bomidref", GetType(String))
        Return dt

    End Function


    Private Sub BomHeaderToDataTable(data As IList(Of BomHeader), PageCount As Integer, ByRef BomHeaderData As DataTable, ByRef BomItemData As DataTable)
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(BomHeader))

        Dim bomidref As String = ""
        'Dim values As Object() = New Object(properties.Count - 1) {}
        Dim values As Object() = New Object(properties.Count) {}
        For Each item As BomHeader In data

            bomidref = ""
            For i As Integer = 0 To values.Length - 2

                If properties(i).Name = "id" Then
                    bomidref = properties(i).GetValue(item)
                End If

                values(i) = properties(i).GetValue(item)

                If properties(i).Name = "bomLineItems" Then

                    BomItemToDataTable(properties(i).GetValue(item), bomidref, BomItemData)

                End If
            Next

            values(values.Length - 1) = PageCount

            BomHeaderData.Rows.Add(values)
        Next

    End Sub

    Private Sub BomItemToDataTable(data As IList(Of BomLineItem), bomidref As String, ByRef BomItemData As DataTable)
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(BomLineItem))

        ' 'Dim values As Object() = New Object(properties.Count - 1) {}
        Dim values As Object() = New Object(properties.Count) {}

        For Each item As BomLineItem In data
            For i As Integer = 0 To values.Length - 2
                values(i) = properties(i).GetValue(item)
            Next

            values(values.Length - 1) = bomidref

            BomItemData.Rows.Add(values)
        Next

    End Sub

    Private Sub FTFixrecord_CheckedChanged(sender As Object, e As EventArgs) Handles FTFixrecord.CheckedChanged
        FNRecord.Visible = (FTFixrecord.Checked)
    End Sub

#End Region

End Class

Public Class RefreshTokenResultJSON
    Public Property access_token As String
    Public Property token_type As String
    Public Property expires_in As Integer
    Public Property scope As String

End Class

Public Class RefreshBomJSON
    Public Property objects As List(Of BomHeader)
    Public Property pages As BomPage
End Class

Public Class BomHeader
    Public Property id As String
    Public Property prmryAbrv As String
    Public Property prmryColorCd As String
    Public Property plugColorwayCd As String
    Public Property parentFcty As String
    Public Property designRegCode As String
    Public Property mscCode As String
    Public Property seasonYr As Integer
    Public Property objectId As Long
    Public Property bomUpdateTimestamp As DateTime
    Public Property developer As String
    Public Property bomUpdateUserid As String
    Public Property styleNbr As String
    Public Property bomId As Integer
    Public Property silhouetteCode As Integer
    Public Property prmryDesc As String
    Public Property prmryColorId As Integer
    Public Property productId As Integer
    Public Property cycleYear As String
    Public Property colorwayCd As String
    Public Property primDevRegAbrv As String
    Public Property colorwayId As Integer
    Public Property mscIdentifier As Integer
    Public Property bomStatus As String
    Public Property seasonCd As String
    Public Property designRegAbrv As String
    Public Property styleNm As String
    Public Property silhouetteDesc As String
    Public Property mscLevel3 As String
    Public Property mscLevel2 As String
    Public Property primDevRegCode As String
    Public Property mscLevel1 As String
    Public Property developerUserId As String
    Public Property factoryCode As String
    Public Property resourceType As String
    Public Property bomLineItems As List(Of BomLineItem)
    Public Property links As BomLink
End Class

Public Class BomLineItem
    Public Property pcxSuppliedMatlId As Integer
    Public Property bomItmUpdateTimestamp As DateTime
    Public Property itemType1 As String
    Public Property bomComponentId As Integer
    Public Property itemNbr As Integer
    Public Property description As String
    Public Property [is] As String
    Public Property [it] As String
    Public Property vendLo As String
    Public Property vevendCdndLo As String
    Public Property bomRowNbr As Integer
    Public Property bomItmSetupTimestamp As DateTime
    Public Property vendNm As String
    Public Property componentOrd As Integer
    Public Property vendId As Integer
    Public Property bomItmId As Integer
End Class

Public Class BomLink
    Public Property self As BomLinkRef
End Class

Public Class BomLinkRef
    Public Property ref As String
End Class

Public Class BomPage
    Public Property [next] As String
End Class

'Public NotInheritable Class ExtensionMethods
'    ''' <summary>
'    ''' Converts a List to a datatable
'    ''' </summary>
'    ''' <typeparam name="T"></typeparam>
'    ''' <param name="data"></param>
'    ''' <returns></returns>
'    <System.Runtime.CompilerServices.Extension>
'    Public Shared Function ToDataTable(Of T)(data As IList(Of T)) As DataTable
'        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(T))
'        Dim dt As New DataTable()
'        For i As Integer = 0 To properties.Count - 1
'            Dim [property] As PropertyDescriptor = properties(i)
'            dt.Columns.Add([property].Name, [property].PropertyType)
'        Next
'        Dim values As Object() = New Object(properties.Count - 1) {}
'        For Each item As T In data
'            For i As Integer = 0 To values.Length - 1
'                values(i) = properties(i).GetValue(item)
'            Next
'            dt.Rows.Add(values)
'        Next
'        Return dt
'    End Function
'End Class
