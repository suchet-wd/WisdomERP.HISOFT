Public Class wSelectBreakDown 
#Region "Property"

    Private _ProcessSave As Boolean = False
    Public Property ProcessSave As Boolean
        Get
            Return _ProcessSave
        End Get
        Set(value As Boolean)
            _ProcessSave = value
        End Set
    End Property

    Private _OrderProdNo As String = ""
    Public Property OrderProdNo As String
        Get
            Return _OrderProdNo
        End Get
        Set(value As String)
            _OrderProdNo = value
        End Set
    End Property

    Private _Mark As String = ""
    Public Property MarkID As String
        Get
            Return _Mark
        End Get
        Set(value As String)
            _Mark = value
        End Set
    End Property

    Private _TableNo As String = ""
    Public Property TableNo As String
        Get
            Return _TableNo
        End Get
        Set(value As String)
            _TableNo = value
        End Set
    End Property

    Private _ColorWay As String = ""
    Public Property ColorWay As String
        Get
            Return _ColorWay
        End Get
        Set(value As String)
            _ColorWay = value
        End Set
    End Property

    Private _PurchaseNo As String = ""
    Public Property PurchaseNo As String
        Get
            Return _PurchaseNo
        End Get
        Set(value As String)
            _PurchaseNo = value
        End Set
    End Property

    Private _DataPurchase As DataTable = Nothing
    Public Property DataPurchase() As DataTable
        Get
            Return _DataPurchase
        End Get
        Set(value As DataTable)
            _DataPurchase = value
        End Set
    End Property
#End Region

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.ProcessSave = False
        Me.Close()
    End Sub

    Private Sub ocmcopy_Click(sender As Object, e As EventArgs) Handles ocmcopy.Click
        Me.ProcessSave = True
        Me.Close()
    End Sub
End Class