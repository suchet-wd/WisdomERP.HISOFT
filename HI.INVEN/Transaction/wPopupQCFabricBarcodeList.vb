Public Class wPopupQCFabricBarcodeList

    Private _StateEnter As Boolean = False
    Public Property StateEnter As Boolean
        Get
            Return _StateEnter
        End Get
        Set(value As Boolean)
            _StateEnter = value
        End Set
    End Property

    Private _RowDataSeq As Integer = 0
    Public Property RowDataSeq As Integer
        Get
            Return _RowDataSeq
        End Get
        Set(value As Integer)
            _RowDataSeq = value
        End Set
    End Property


    Private Sub oBtnEnter_Click(sender As Object, e As EventArgs) Handles oBtnEnter.Click
        Try
            _StateEnter = True
            RowDataSeq = Val(ogv.GetFocusedRowCellValue("FNSeq").ToString)
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.



    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try


            _StateEnter = False
            RowDataSeq = -1
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub wPopupQCFabricBarcode_Load(sender As Object, e As EventArgs) Handles Me.Load
        _StateEnter = False
        ogv.OptionsView.ShowAutoFilterRow = False

    End Sub
End Class