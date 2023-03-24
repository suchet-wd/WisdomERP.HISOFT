Public Class wPurchaseOrderChangePONO

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' HI.TL.HandlerControl.AddHandlerObj(Me)
    End Sub
#Region "Property"
    Private _PRNO As String = ""
    Public Property PRNO As String
        Get
            Return _PRNO
        End Get
        Set(value As String)
            _PRNO = value
        End Set
    End Property

    Private _FNSuplID As Integer = 0
    Public Property FNSuplID As Integer
        Get
            Return _FNSuplID
        End Get
        Set(value As Integer)
            _FNSuplID = value
        End Set
    End Property

    Private _AddMat As Boolean = False
    Public Property AddMat As Boolean
        Get
            Return _AddMat
        End Get
        Set(value As Boolean)
            _AddMat = value
        End Set
    End Property
#End Region


    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.AddMat = False
        Me.Close()
    End Sub

    Private Sub wAddRequest_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click

        If FTRemark.Text.Trim <> "" Then
            Me.AddMat = True
            Me.Close()
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTRemark_lbl.Text)
            FTRemark.Focus()
        End If

    End Sub

End Class