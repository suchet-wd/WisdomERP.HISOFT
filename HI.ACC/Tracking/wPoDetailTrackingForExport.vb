Imports System.Windows.Forms

Public Class wPoDetailTrackingForExport
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
        Dim _lang As String = "TH"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _lang = "TH"
        Else
            _lang = "EN"
        End If

        _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.SP_GET_POTRACK_FOR_EXPORT_DEPT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_lang) & "'"
        _Qry = _Qry & ",'" & StateFTPOref.EditValue.ToString & "'"
        _Qry = _Qry & ",'" & StateFTStyleCode.EditValue.ToString & "'"
        _Qry = _Qry & ",'" & StateFTSeasonCode.EditValue.ToString & "'"
        _Qry = _Qry & ",'" & StateFTColorway.EditValue.ToString & "'"
        _Qry = _Qry & ",'" & StateFTSizeBreakDown.EditValue.ToString & "'"
        _Qry = _Qry & ",'" & StateFTRawMatCode.EditValue.ToString & "'"
        _Qry = _Qry & ",'" & StateFTMatColorCode.EditValue.ToString & "'"
        _Qry = _Qry & ",'" & StateFTMatSizeCode.EditValue.ToString & "'"
        _Qry = _Qry & ",'" & StateFTSuplName.EditValue.ToString & "'"
        _Qry = _Qry & ",'" & StateFTCountryName.EditValue.ToString & "'"
        _Qry = _Qry & ",'" & StateFDPurchaseDate.EditValue.ToString & "'"
        _Qry = _Qry & ",'" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text.Trim) & "'"
        _Qry = _Qry & ",'" & HI.UL.ULF.rpQuoted(FTCustomerPO_To.Text.Trim) & "'"
        _Qry = _Qry & ",''"
        _Qry = _Qry & ",''"
        _Qry = _Qry & ",'" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text.Trim) & "'"
        _Qry = _Qry & ",'" & HI.UL.ULF.rpQuoted(FNHSysStyleIdTo.Text.Trim) & "'"
        _Qry = _Qry & ",'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text.Trim) & "'"
        _Qry = _Qry & ",'" & HI.UL.ULF.rpQuoted(FTOrderNo_To.Text.Trim) & "'"
        _Qry = _Qry & ",'" & HI.UL.ULDate.ConvertEnDB(SFDPurchaseDate.Text) & "'"
        _Qry = _Qry & ",'" & HI.UL.ULDate.ConvertEnDB(EFDPurchaseDate.Text) & "'"
        _Qry = _Qry & ",'" & HI.UL.ULF.rpQuoted(FNHSysSuplId.Text.Trim) & "'"
        _Qry = _Qry & ",'" & HI.UL.ULF.rpQuoted(FNHSysSuplIdTo.Text.Trim) & "'"
        _Qry = _Qry & ",'" & StateFTPurchaseNo.EditValue.ToString & "'"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)
        ogcPoDetailTracking.DataSource = _dt.Copy

        _dt.Dispose()

        Return True
    End Function

    Private Sub ClearCreteria()

        SFDPurchaseDate.Text = ""
        EFDPurchaseDate.Text = ""

        FNHSysStyleId.Properties.Tag = ""
        FNHSysStyleId.Text = ""

        FNHSysStyleIdTo.Properties.Tag = ""
        FNHSysStyleIdTo.Text = ""

        FTOrderNo.Properties.Tag = ""
        FTOrderNo.Text = ""

        FTOrderNo_To.Properties.Tag = ""
        FTOrderNo_To.Text = ""

        FTCustomerPO.Properties.Tag = ""
        FTCustomerPO.Text = ""

        FTCustomerPO_To.Properties.Tag = ""
        FTCustomerPO_To.Text = ""

        FNHSysSuplId.Properties.Tag = ""
        FNHSysSuplId.Text = ""

        FNHSysSuplIdTo.Properties.Tag = ""
        FNHSysSuplIdTo.Text = ""

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
        ogcPoDetailTracking.DataSource = Nothing

    End Sub

    Private Function ValidateData() As Boolean
        If SFDPurchaseDate.Text = "" And EFDPurchaseDate.Text = "" And FNHSysStyleId.Text = "" And FNHSysStyleIdTo.Text = "" And FTOrderNo.Text = "" And FTOrderNo_To.Text = "" And FTCustomerPO.Text = "" And FTCustomerPO_To.Text = "" And FNHSysSuplId.Text = "" And FNHSysSuplIdTo.Text = "" Then
            HI.MG.ShowMsg.mInfo("กรุณาเลือกข้อมูลอย่างน้อยหนึ่งอย่าง !!!", 1511121406, Me.Text)
            Return False
        End If

        If StateFTPOref.Checked = False And Me.StateFTStyleCode.Checked = False And StateFTSeasonCode.Checked = False And StateFTColorway.Checked = False _
            And StateFTSizeBreakDown.Checked = False And StateFTRawMatCode.Checked = False _
            And StateFTMatColorCode.Checked = False And StateFTMatSizeCode.Checked = False And StateFTSuplName.Checked = False _
            And StateFTCountryName.Checked = False And StateFDPurchaseDate.Checked = False And StateFTPurchaseNo.Checked = False Then

            HI.MG.ShowMsg.mInfo("กรูณาทำการเลือกข้อมูลที่ต้องการทำการแสดง !!!", 1511177546, Me.Text)
            Return False
        End If

        Return True
    End Function

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub wPoDetailTrackingForExport_Load(sender As Object, e As EventArgs) Handles Me.Load
        _StateFormLoad = False
        Me.StateFTPurchaseNo.Checked = False
    End Sub

    Private Sub StateFTPOref_CheckedChanged(sender As Object, e As EventArgs) Handles StateFTPOref.CheckedChanged, StateFTStyleCode.CheckedChanged, StateFTSeasonCode.CheckedChanged, StateFTColorway.CheckedChanged, _
             StateFTSizeBreakDown.CheckedChanged, StateFTRawMatCode.CheckedChanged, _
             StateFTMatColorCode.CheckedChanged, StateFTMatSizeCode.CheckedChanged, StateFTSuplName.CheckedChanged, _
             StateFTCountryName.CheckedChanged, StateFDPurchaseDate.CheckedChanged, StateFTPurchaseNo.CheckedChanged

        Try
            If _StateFormLoad Then Exit Sub
            Me.ogcPoDetailTracking.DataSource = Nothing

            With Me.ogvdetailPoTracking
                .BeginInit()
                .Columns.ColumnByFieldName("FTPOref").Visible = (StateFTPOref.Checked)
                .Columns.ColumnByFieldName("FTStyleCode").Visible = (StateFTStyleCode.Checked)
                .Columns.ColumnByFieldName("FTSeasonCode").Visible = (StateFTSeasonCode.Checked)
                .Columns.ColumnByFieldName("FTColorway").Visible = (StateFTColorway.Checked)
                .Columns.ColumnByFieldName("FTSizeBreakDown").Visible = (StateFTSizeBreakDown.Checked)
                .Columns.ColumnByFieldName("FTRawMatCode").Visible = (StateFTRawMatCode.Checked)
                .Columns.ColumnByFieldName("FTRawMatColorCode").Visible = (StateFTMatColorCode.Checked)
                .Columns.ColumnByFieldName("FTRawMatSizeCode").Visible = (StateFTMatSizeCode.Checked)
                .Columns.ColumnByFieldName("FTSuplName").Visible = (StateFTSuplName.Checked)
                .Columns.ColumnByFieldName("FTCountryName").Visible = (StateFTCountryName.Checked)
                .Columns.ColumnByFieldName("FTPurchaseNo").Visible = (StateFTPurchaseNo.Checked)
                .Columns.ColumnByFieldName("FDPurchaseDate").Visible = (StateFDPurchaseDate.Checked)
                .EndInit()
            End With
        Catch ex As Exception

        End Try

    End Sub
End Class