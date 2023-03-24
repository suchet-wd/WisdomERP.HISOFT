Imports System.Windows.Forms

Public Class wAccountCloseJobOrderTracking
    Private _StateFormLoad As Boolean = False
    Sub New()
        InitializeComponent()
        _StateFormLoad = True
    End Sub

#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

#End Region

    Private Function LoadDataTracking(Spls As HI.TL.SplashScreen) As Boolean
        Dim _Qry As String = ""
        Dim _dt As DataTable


        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.USP_GETTRACK_CLOSEJOBMANAGESTOCK '" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "','" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTStartShipDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndShipDate.Text) & "','" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "','" & HI.UL.ULF.rpQuoted(FTOrderNo_To.Text) & "' "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)
        ogcdetail.DataSource = _dt.Copy

        _dt.Dispose()

        Return True
    End Function


    Private Sub ClearCreteria()

        FTStartShipDate.Text = ""
        FTEndShipDate.Text = ""

        FTOrderNo.Properties.Tag = ""
        FTOrderNo.Text = ""

        FTOrderNo_To.Properties.Tag = ""
        FTOrderNo_To.Text = ""

        FTCustomerPO.Properties.Tag = ""
        FTCustomerPO.Text = ""

        FTCustomerPO_To.Properties.Tag = ""
        FTCustomerPO_To.Text = ""

        'FNHSysSuplId.Properties.Tag = ""
        'FNHSysSuplId.Text = ""

        'FNHSysSuplIdTo.Properties.Tag = ""
        'FNHSysSuplIdTo.Text = ""

    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If ValidateData() Then
            Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

            If LoadDataTracking(_Spls) Then
            End If

            _Spls.Close()
        End If
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        'HI.TL.HandlerControl.ClearControl(Me)
        Call ClearCreteria()
        ogcdetail.DataSource = Nothing

    End Sub

    Private Function ValidateData() As Boolean

        If FTStartShipDate.Text = "" And FTEndShipDate.Text = "" And FTOrderNo.Text = "" And FTOrderNo_To.Text = "" And FTCustomerPO.Text = "" And FTCustomerPO_To.Text = "" Then
            HI.MG.ShowMsg.mInfo("กรุณาเลือกข้อมูลอย่างน้อยหนึ่งอย่าง !!!", 1511121406, Me.Text)
            Return False
        End If

        'If StateFTPOref.Checked = False And Me.StateFTStyleCode.Checked = False And StateFTSeasonCode.Checked = False And StateFTColorway.Checked = False _
        '    And StateFTSizeBreakDown.Checked = False And StateFTRawMatCode.Checked = False _
        '    And StateFTMatColorCode.Checked = False And StateFTMatSizeCode.Checked = False And StateFTSuplName.Checked = False _
        '    And StateFTCountryName.Checked = False And StateFDPurchaseDate.Checked = False And StateFTPurchaseNo.Checked = False Then

        '    HI.MG.ShowMsg.mInfo("กรูณาทำการเลือกข้อมูลที่ต้องการทำการแสดง !!!", 1511177546, Me.Text)
        '    Return False
        'End If

        Return True
    End Function

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Sub wPoDetailTrackingForExport_Load(sender As Object, e As EventArgs) Handles Me.Load
        _StateFormLoad = False
        'Me.StateFTPurchaseNo.Checked = False
    End Sub

    Private Sub RepFTSelect_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepFTSelect.EditValueChanging

        Try

        Catch ex As Exception
        End Try

    End Sub


End Class