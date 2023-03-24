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

Public Class wExportDataToIntelecut


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

    Private Sub Proc_Close(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub


    Private Sub LoadOrderListingInfo()
        Dim Qry As String = ""
        Dim dt As New DataTable

        Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..[SP_Get_Data_For_Intelecut] " & Val(FNHSysBuyId.Properties.Tag.ToString) & " "
        Qry &= vbCrLf & "," & Val(FNHSysStyleId.Properties.Tag.ToString) & "," & Val(FNHSysSeasonId.Properties.Tag.ToString) & ",'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "',''"

        dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MERCHAN)



        With Me.ogv

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTModule".ToUpper, "FTStyleCode".ToUpper, "FTStyleName".ToUpper, "FTSeasonCode".ToUpper, "FTFit".ToUpper _
                              , "FTProdTypeCode".ToUpper, "FTCustomer".ToUpper, "FTSellingPrice".ToUpper, "FTSloper".ToUpper, "FTEstimateed".ToUpper, "FDShipDate".ToUpper, "FTOrderReceive".ToUpper, "FTCountryCode".ToUpper _
                            , "FTPart".ToUpper, "FTMainPart".ToUpper, "FNConSmp".ToUpper, "FTMatColorCode".ToUpper, "FTColorHex".ToUpper, "FTFabric".ToUpper, "FTRawMatColorName".ToUpper, "FabricCategory".ToUpper, "FNExtra".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select

            Next

            If Not (dt Is Nothing) Then
                For Each Col As DataColumn In dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTModule".ToUpper, "FTStyleCode".ToUpper, "FTStyleName".ToUpper, "FTSeasonCode".ToUpper, "FTFit".ToUpper _
                              , "FTProdTypeCode".ToUpper, "FTCustomer".ToUpper, "FTSellingPrice".ToUpper, "FTSloper".ToUpper, "FTEstimateed".ToUpper, "FDShipDate".ToUpper, "FTOrderReceive".ToUpper, "FTCountryCode".ToUpper _
                            , "FTPart".ToUpper, "FTMainPart".ToUpper, "FNConSmp".ToUpper, "FTMatColorCode".ToUpper, "FTColorHex".ToUpper, "FTFabric".ToUpper, "FTRawMatColorName".ToUpper, "FabricCategory".ToUpper, "FNExtra".ToUpper
                        Case Else

                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                .Visible = True
                                .FieldName = Col.ColumnName.ToString
                                .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                .Caption = Col.ColumnName.ToString

                            End With

                            .Columns.Add(ColG)

                            With .Columns(Col.ColumnName.ToString)

                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n0}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False
                                    .ReadOnly = True
                                End With

                            End With

                            .Columns(Col.ColumnName.ToString).Width = 50
                            '.Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            '.Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

                    End Select

                Next

            End If

            'If _colcount > 4 Then
            '    .BestFitColumns()
            'End If

        End With


        Me.ogcmatcode.DataSource = dt

    End Sub

    Private Sub ocmclear1_Click(sender As System.Object, e As System.EventArgs) Handles ocmclearclsr.Click
        Me.ogcmatcode.DataSource = Nothing
        Dim xCol As Integer = 0
        Dim Idx As Integer = 0

        Try
            _Clear = True
            HI.TL.HandlerControl.ClearControl(Me)

        Catch ex As Exception

        End Try
        _Clear = False
    End Sub

    Private Function PROC_VALIDATEbSHOWBROWSEDATA() As Boolean

        If Me.FTOrderNo.Text = "" And FNHSysStyleId.Text = "" And FNHSysBuyId.Text = "" And FNHSysSeasonId.Text = "" Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmrefresh.Click

        If Not PROC_VALIDATEbSHOWBROWSEDATA() = True Then Exit Sub

        Dim spl As New HI.TL.SplashScreen("Loading.....")
        Call LoadOrderListingInfo()
        spl.Close()
    End Sub

    Private Sub FuncExcel_Click(sender As System.Object, e As System.EventArgs) Handles FuncExcel.Click
        If Me.ogv.RowCount <= 0 Then
            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการ Export กรุณาทำการตรวจสอบ !!!", 1505140001, Me.Text)
        Else
            Try

                Dim Op As New System.Windows.Forms.SaveFileDialog
                Op.Filter = "Excel Files(.xlsx)|*.xlsx"
                Op.ShowDialog()

                Try

                    If Op.FileName <> "" Then

                        With ogv
                            .ExportToXlsx(Op.FileName)

                            Try
                                Process.Start(Op.FileName)
                            Catch ex As Exception
                            End Try

                        End With

                    End If

                Catch ex As Exception
                End Try

            Catch ex As Exception
            End Try

        End If
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Function PostdataToWebService() As Boolean
        Try
            Dim url As String = "http://128.199.73.202/oauth/token"


            Dim responsestring As String = ""
            Dim myReq As Net.HttpWebRequest = WebRequest.Create(url)

            Dim proxy As IWebProxy = CType(myReq.Proxy, IWebProxy)
            Dim myProxy As New WebProxy()
            Dim encoding As New ASCIIEncoding
            Dim token_access As String = ""

            Dim json_data As String = "{" + Chr(34) + "grant_type" + Chr(34) + ": " + Chr(34) + "client_credentials" + Chr(34) + "," + Chr(34) + "client_id" + Chr(34) + ": " + Chr(34) + "426d3abdbc19d31f3320f511a4bf5a01bad7cdca0f4b52defd212ba4ff207f52" + Chr(34) + "," + Chr(34) + "client_secret" + Chr(34) + ": " + Chr(34) + "2db1e4fb4c414eedcca34a96eb40871d766ac420a3cdfc5ba4b8749a8264a081" + Chr(34) + "}"
            Dim json_bytes() As Byte = System.Text.Encoding.ASCII.GetBytes(json_data)

            myReq.AllowWriteStreamBuffering = False
            myReq.Method = "POST"
            myReq.ContentType = "application/json"
            myReq.ContentLength = json_bytes.Length

            Dim post As IO.Stream = myReq.GetRequestStream

            post.Write(json_bytes, 0, json_bytes.Length)

            Dim response As Net.HttpWebResponse = myReq.GetResponse

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

            'Cleanup the streams and the response.
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

    Private Sub ocmexporttoxml_Click(sender As Object, e As EventArgs) Handles ocmexporttoxml.Click
        Call PostdataToWebService()
    End Sub


#End Region

#Region "Initial Grid"



#End Region


End Class