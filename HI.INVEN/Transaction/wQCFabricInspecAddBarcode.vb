Public Class wQCFabricInspecAddBarcode 

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private _StateProc As Boolean = False
    Public Property StateProc As Boolean
        Get
            Return _StateProc
        End Get
        Set(value As Boolean)
            _StateProc = value
        End Set
    End Property

    Private Sub wQCFabricInspecAddBarcode_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.StateProc = False
        Me.Close()
    End Sub

    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        Dim _dtd As DataTable
        With CType(ogcrawmat.DataSource, DataTable)
            .AcceptChanges()
            _dtd = .Copy
        End With

        If _dtd.Select("FTSelect='1'").Length > 0 Then
            Me.StateProc = True
            Me.Close()
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก ม้วนผ้า !!!", 1502240197, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub FTStateSelectall_CheckedChanged(sender As Object, e As EventArgs) Handles FTStateSelectall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.FTStateSelectall.Checked Then
                _State = "1"
            End If

            With ogcrawmat
                If Not (.DataSource Is Nothing) And ogvrawmat.RowCount > 0 Then
                    With ogvrawmat
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception
        End Try
    End Sub
End Class