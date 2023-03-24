Public Class wReservedAutoPopUp 
    Private _oDt As DataTable
    Public Property oDt As DataTable
        Get
            Return _oDt
        End Get
        Set(value As DataTable)
            _oDt = value
        End Set
    End Property

    Private _Proc As Boolean
    Public Property Proc As Boolean
        Get
            Return _Proc
        End Get
        Set(value As Boolean)
            _Proc = value
        End Set
    End Property

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Private Sub oClose_Click(sender As Object, e As EventArgs) Handles oClose.Click
        Proc = False
        Me.Close()
    End Sub

    Private Sub oSend_Click(sender As Object, e As EventArgs) Handles oSend.Click
        Try
            Proc = True
            With CType(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                oDt = .Copy
            End With
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub wReservedAutoPopUp_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.ogcdetail.DataSource = _oDt.Copy
        Catch ex As Exception
        End Try
    End Sub

    

    Private Sub oFTSelect_CheckedChanged(sender As Object, e As EventArgs) Handles oFTSelect.CheckedChanged
        Try
            With CType(ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows

                    If oFTSelect.Checked = True Then
                        R!FTSelect = "1"
                    Else
                        R!FTSelect = "0"
                    End If

                Next
                .AcceptChanges()
            End With
        Catch ex As Exception

        End Try
    End Sub
End Class