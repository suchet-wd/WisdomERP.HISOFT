Public Class wFundRatePopUp

    'Public wEmployee _Main {Get Set}

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub wFundRatePopUp_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            Me.ogcdetail.DataSource = _oDt

        Catch ex As Exception
        End Try
    End Sub

    Public _State As Boolean = False
    Private Property State As Boolean
        Get
            Return _State
        End Get
        Set(value As Boolean)
            _State = value
        End Set
    End Property

    Public _oDt As DataTable

    Private Property oDt As DataTable
        Get
            Return _oDt
        End Get
        Set(value As DataTable)
            _oDt = value
        End Set
    End Property

    Private _fundRate As String

    Public Property FundRate As String
        Get
            Return _fundRate
        End Get
        Set(value As String)
            _fundRate = value
        End Set
    End Property

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        _State = False
        Me.Close()
    End Sub

    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        _State = True
        Me.Close()
    End Sub

    Private Sub Ogvdetail_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles ogvdetail.RowCellClick
        Dim cellValue As String = ogvdetail.GetFocusedRowCellValue("FNEmpPay").ToString()

        FundRate_lbl.Text = cellValue
        _fundRate = cellValue

    End Sub
End Class