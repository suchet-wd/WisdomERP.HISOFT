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
Imports System.IO
Imports System.Text
Imports System.Net
Imports Microsoft.Win32
Imports System.Web
Imports System.Runtime.Serialization


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

    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Function PostdataToWebService() As Boolean
        Try
            Dim urlEndPoint As String = "https://nike-qa.oktapreview.com/oauth2/ausa0mcornpZLi0C40h7/v1/token"
            Dim clientid As String = "testfactory.gsm.bom"
            Dim clientsecret As String = "Ttpx9N1Qpr-thAdjgWVpyMsOKdce0ECElm8XWoAdL7sHH6u0AIOQ0RgfjH59rQb_"
            Dim granttype As String = "client_credentials"
            ' ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
            Dim responsestring As String = ""
            Dim myReq As Net.HttpWebRequest = WebRequest.Create(urlEndPoint)
            '  ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)

            Dim token_access As String = ""

            'Dim json_data As String = "{" + Chr(34) + "client_id" + Chr(34) + ": " + Chr(34) + clientid + Chr(34) + "," + Chr(34) + "client_secret" + Chr(34) + ": " + Chr(34) + clientsecret + Chr(34) + "grant_type" + Chr(34) + ": " + Chr(34) + granttype + Chr(34) + "," + Chr(34) + "}"
            ' Dim json_data As String = "client_id=" + clientid + Chr(34) + "," + Chr(34) + "client_secret" + Chr(34) + ": " + Chr(34) + clientsecret + Chr(34) + "grant_type" + Chr(34) + ": " + Chr(34) + granttype + Chr(34) + "," + Chr(34) + "}"


            Dim json_data As String = "client_id=" & clientid & "client_secret=" & clientsecret & "grant_type=client_credentials"


            Dim json_bytes() As Byte = System.Text.Encoding.UTF8.GetBytes(json_data)

            '  myReq.AllowWriteStreamBuffering = False
            myReq.Method = "POST"
            myReq.ContentType = "application/x-www-form-urlencoded"
            myReq.ContentLength = json_bytes.Length

            ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
            Dim post As IO.Stream = myReq.GetRequestStream

            post.Write(json_bytes, 0, json_bytes.Length)
            ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
            Dim response As HttpWebResponse = DirectCast(myReq.GetResponse(), HttpWebResponse)

            Debug.Print(response.StatusDescription)

            Dim dataStream As IO.Stream = response.GetResponseStream()
            Dim reader As New IO.StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()

            token_access = ""
            Try
                token_access = responseFromServer.Split(":")(1)
            Catch ex As Exception
            End Try

            MsgBox(responseFromServer)  ' Display the content.

            ' Cleanup the streams and the response.
            post.Close()
            reader.Close()
            dataStream.Close()
            response.Close()

            If token_access <> "" Then

            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Sub GetTokenData()

        Dim urlEndPoint As String = "https://nike-qa.oktapreview.com/oauth2/ausa0mcornpZLi0C40h7/v1/token"


        ' Refer to the documentation for more information on how to get the client id/secret
        Dim clientid As String = "testfactory.gsm.bom"
        Dim clientsecret As String = "Ttpx9N1Qpr-thAdjgWVpyMsOKdce0ECElm8XWoAdL7sHH6u0AIOQ0RgfjH59rQb_"
        Dim granttype As String = "client_credentials"
        ' Refer to the documentation for more information on how to get the tokens
        Dim accessToken As String = "OaOXXXXTaSucp8XXcgXXH"


        ' -- Refresh the access token
        Dim request As System.Net.WebRequest = System.Net.HttpWebRequest.Create(urlEndPoint)
        request.Method = "POST"
        request.ContentType = "application/x-www-form-urlencoded"

        'Dim outgoingQueryString As NameValueCollection = System.Web.HttpUtility.ParseQueryString(String.Empty)
        'outgoingQueryString.Add("grant_type", "refresh_token")
        'outgoingQueryString.Add("client_id", clientId)
        'outgoingQueryString.Add("client_secret", clientSecret)
        Dim json_data As String = "client_id=testfactory.gsm.bomclient_secret=Ttpx9N1Qpr-thAdjgWVpyMsOKdce0ECElm8XWoAdL7sHH6u0AIOQ0RgfjH59rQb_grant_type=client_credentials"
        Dim postBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(json_data) 'New ASCIIEncoding().GetBytes(json_data)

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
                    'Dim jsonResult As RefreshTokenResultJSON = New System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(streamReader.ReadToEnd(), GetType(RefreshTokenResultJSON))
                    'accessToken = jsonResult.access_token
                    ' For more information, refer to the documentation
                    'accessToken = streamReader.ReadToEnd()
                End Using
            End Using
        Catch ex As Exception

        End Try


        '' -- Get current user profile
        'request = System.Net.HttpWebRequest.Create("https://api.clicdata.com/profile/user")
        'request.Method = "GET"
        'request.Headers.Add("Authorization", "Bearer " & accessToken)
        'Using response As System.Net.WebResponse = request.GetResponse()
        '    Using streamReader As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
        '        Dim jsonResponseText = streamReader.ReadToEnd()
        '        ' Parse the JSON the way you prefer
        '        ' In this example the JSON will be something Like:
        '        ' {"email_address" "john.smith@clicdata.com", "first_name": "John", "last_login_date": "2016−04−26T14:24:58+00:00", "last_name": "Smith", ...
        '        ' For more information, refer to the documentation
        '    End Using
        'End Using

        '' -- List the current data
        'request = System.Net.HttpWebRequest.Create("https://api.clicdata.com/data/")
        'request.Method = "GET"
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
    End Sub
    Private Sub ocmexporttoxml_Click(sender As Object, e As EventArgs) Handles ocmexporttoxml.Click
        Call PostdataToWebService()
        Call GetTokenData()
    End Sub


#End Region

#Region "Initial Grid"



#End Region


End Class

Public Class RefreshTokenResultJSON
    Public Property access_token As String
End Class
