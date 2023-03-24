
Imports System.IO
Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Text

Public Class wBarcodeAPI
    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click


        Dim myReq As Net.HttpWebRequest = WebRequest.Create("http://localhost:61500/api/BarcodeGrp/" + FTBarcodeNo.Text + "")
        Dim proxy As IWebProxy = CType(myReq.Proxy, IWebProxy)
        Dim token_access As String = ""

        myReq.AllowWriteStreamBuffering = False
        myReq.ContentType = "application/json; charset=utf-8"

        Dim response As Net.HttpWebResponse = myReq.GetResponse


        Dim dataStream As IO.Stream = response.GetResponseStream()
        Dim reader As New IO.StreamReader(dataStream)
        Dim responseFromServer As String = reader.ReadToEnd()

        reader.Close()
        dataStream.Close()
        response.Close()

        Dim dt As System.Data.DataSet = JsonConvert.DeserializeObject(Of System.Data.DataSet)(responseFromServer)
        PostdataToWebService(dt)
    End Sub




    Private Function PostdataToWebService(dt As System.Data.DataSet) As Boolean
        Try


            Dim responsestring As String = ""
            Dim myReq As Net.HttpWebRequest = WebRequest.Create("http://localhost:61500/api/BarcodeGrp/")

            Dim proxy As IWebProxy = CType(myReq.Proxy, IWebProxy)
            Dim myProxy As New WebProxy()
            Dim encoding As New ASCIIEncoding
            Dim token_access As String = ""

            Dim json_data As String = JsonConvert.SerializeObject(dt)
            Dim json_bytes() As Byte = System.Text.Encoding.UTF8.GetBytes(json_data)

            myReq.AllowWriteStreamBuffering = False
            myReq.Method = "POST"
            ' myReq.Method = "PUT"
            myReq.ContentType = "application/json"
            myReq.ContentLength = json_bytes.Length

            Dim post As IO.Stream = myReq.GetRequestStream

            post.Write(json_bytes, 0, json_bytes.Length)

            Dim response As Net.HttpWebResponse = myReq.GetResponse

            Dim dataStream As IO.Stream = response.GetResponseStream()
            Dim reader As New IO.StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            If response.StatusCode = HttpStatusCode.OK Then

            Else

            End If
            token_access = ""
            Try
                token_access = responseFromServer.Split(":")(1)
            Catch ex As Exception
            End Try


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

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim DT As New DataTable
        DT.Columns.Add("FTBarcodeNo")
        DT.Rows.Add("8772190209000121")
        Dim ds As New DataSet
        ds.Tables.Add(DT)


        If PostdataToWebService(ds) Then

        End If
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Try

            Dim responsestring As String = ""
            Dim myReq As Net.HttpWebRequest = WebRequest.Create("http://localhost:61500/api/Movelocation/")

            Dim proxy As IWebProxy = CType(myReq.Proxy, IWebProxy)
            Dim myProxy As New WebProxy()
            Dim encoding As New ASCIIEncoding
            Dim token_access As String = ""


            Dim Uinfo As New JSonHeaderDocument

            Uinfo.username = "Admin"
            Uinfo.HSysCmpId = "HT91"
            Uinfo.WHCode = "91ACC"
            Uinfo.Note = "88512121446788512"
            Uinfo.DocumentNo = "88512121446788512"
            Uinfo.WHLocCode = "88512121446788512"

            Dim json_data As String = JsonConvert.SerializeObject(Uinfo)
            Dim json_bytes() As Byte = System.Text.Encoding.UTF8.GetBytes(json_data)

            myReq.AllowWriteStreamBuffering = False
            myReq.Method = "POST"
            ' myReq.Method = "PUT"
            myReq.ContentType = "application/json"
            myReq.ContentLength = json_bytes.Length

            Dim post As IO.Stream = myReq.GetRequestStream

            post.Write(json_bytes, 0, json_bytes.Length)

            Dim response As Net.HttpWebResponse = myReq.GetResponse

            Dim dataStream As IO.Stream = response.GetResponseStream()
            Dim reader As New IO.StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            If response.StatusCode = HttpStatusCode.OK Then

            Else

            End If
            token_access = ""
            Try
                token_access = responseFromServer.Split(":")(1)
            Catch ex As Exception
            End Try


            post.Close()
            reader.Close()
            dataStream.Close()
            response.Close()

            If token_access <> "" Then

            Else

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PostDataSet()
        Try

            Dim responsestring As String = ""
            Dim myReq As Net.HttpWebRequest = WebRequest.Create("http://localhost:61500/api/UserInfo/")

            Dim proxy As IWebProxy = CType(myReq.Proxy, IWebProxy)
            Dim myProxy As New WebProxy()
            Dim encoding As New ASCIIEncoding
            Dim token_access As String = ""


            Dim Uinfo As New RefreshUserInfoJSON

            Uinfo.username = "w70raviwan"
            Uinfo.password = "9999977777"
            Uinfo.cmpcode = "HT91"



            Dim dts As New System.Data.DataSet("JsonDs")
            Dim dt As New System.Data.DataTable()
            dt.Columns.Add("reportname", GetType(String))
            dt.Columns.Add("formula", GetType(String))

            dt.Rows.Add("BarcodeGrpSlip", "{TINVENBarcode.FTBarcodeGrpNo}='BGR-481903280004' ")
            dts.Tables.Add(dt)


            Dim json_data As String = JsonConvert.SerializeObject(Uinfo)
            Dim json_bytes() As Byte = System.Text.Encoding.UTF8.GetBytes(json_data)

            myReq.AllowWriteStreamBuffering = False
            myReq.Method = "POST"
            ' myReq.Method = "PUT"
            myReq.ContentType = "application/json"
            myReq.ContentLength = json_bytes.Length

            Dim post As IO.Stream = myReq.GetRequestStream

            post.Write(json_bytes, 0, json_bytes.Length)

            Dim response As Net.HttpWebResponse = myReq.GetResponse

            Dim dataStream As IO.Stream = response.GetResponseStream()
            Dim reader As New IO.StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()

            Dim JSoRet As JSonDataRet = JsonConvert.DeserializeObject(Of JSonDataRet)(responseFromServer)

            If response.StatusCode = HttpStatusCode.OK Then

            Else

            End If
            token_access = ""
            Try
                token_access = responseFromServer.Split(":")(1)
            Catch ex As Exception
            End Try

            post.Close()
            reader.Close()
            dataStream.Close()
            response.Close()

            If token_access <> "" Then
            Else
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Call PostDataSet()
    End Sub

    Private Sub wBarcodeAPI_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim rbyte() As Byte = Encoding.ASCII.GetBytes("r")
        Dim rbyte2() As Byte = Encoding.ASCII.GetBytes("a")
        Dim rbyte3() As Byte = Encoding.ASCII.GetBytes("a")

        Dim msg As String = "Hello"

        Dim hexstring As String



        For i = 0 To msg.Length - 2 Step 2

            hexstring = Hex(Asc(msg(i))) + Hex(Asc(msg(i + 1)))

        Next i
        'Dim HexValue1 As String = Convert.ToString(Convert.ToInt32("r"), 16)
        'Dim HexValue2 As String = Convert.ToString(Convert.ToInt32("a"), 16)
        'Dim HexValue3 As String = Convert.ToString(-128, 16)


        Dim rbyte4 As Byte = CType(128, Byte)

    End Sub
End Class


Public Class RefreshUserInfoJSON
    Public username As String
    Public password As String
    Public cmpcode As String
End Class

Public Class JSonDataRet
    Public status As String
    Public Refer As String
    Public Refer2 As String
    Public Refer3 As String
    Public Refer4 As String
    Public Refer5 As String

End Class

Public Class JSonHeaderDocument
    Public HSysCmpId As String = "-"
    Public DocumentNo As String = "-"
    Public WHCode As String = "-"
    Public WHLocCode As String = "-"
    Public Note As String = "-"
    Public UserName As String = "-"
End Class

