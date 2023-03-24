Imports System.Windows.Forms

Public Class wListCarton

    Private _PackOrderNo As String = ""
    Public Property PackOrderNo As String
        Get
            Return _PackOrderNo
        End Get
        Set(value As String)
            _PackOrderNo = value
        End Set
    End Property

    Private _OrderNo As String = ""
    Public Property OrderNo As String
        Get
            Return _OrderNo
        End Get
        Set(value As String)
            _OrderNo = value
        End Set
    End Property

    Private _State As Boolean = False

    Public Property State As Boolean
        Get
            Return _State
        End Get
        Set(value As Boolean)
            _State = value
        End Set
    End Property

    Private Sub ocmSave_Click(sender As Object, e As EventArgs) Handles ocmSave.Click
        If Not (Verifydata()) Then Exit Sub
        _State = True
        Me.Close()
    End Sub

    Private Function Verifydata() As Boolean
        Try
            Dim _state As Boolean = False
            Dim _row As Boolean = False


            With DirectCast(Me.ogclist.DataSource, DataTable)
                .AcceptChanges()
                For Each r As DataRow In .Select("FTSelect = '1'")
                    _row = True
                    Exit For
                Next
            End With

            If Not (_row) Then
                HI.MG.ShowMsg.mInfo("Plase Select Data  ..........", 1706231808, ogblist.Text)
            End If


            If Me.FNHSysWHFGId.Text <> "" Then
                _state = True
            End If
            If Not (_state) Then
                HI.MG.ShowMsg.mInfo("Plase Select WareHouse To ..........", 1706231807, Me.FNHSysWHFGId_lbl.Text)
                Me.FNHSysWHFGId.Focus()

            End If


            Return _state = True And _row = True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub ocmCancel_Click(sender As Object, e As EventArgs) Handles ocmCancel.Click
        _State = False
        Me.Close()
    End Sub

    Private Sub oSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles oSelectAll.CheckedChanged
        Try
            Dim _State As String = IIf(oSelectAll.Checked = True, "1", "0")
            With ogclist
                If Not (.DataSource Is Nothing) And ogvlist.RowCount > 0 Then
                    With ogvlist
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, "FTSelect", _State)
                        Next
                    End With
                    CType(.DataSource, DataTable).AcceptChanges()
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    'Private Sub ogvlist_KeyDown(sender As Object, e As KeyEventArgs) Handles ogvlist.KeyDown
    '    Try
    '        With ogvlist
    '            If .RowCount > 1 And .FocusedRowHandle > -1 Then

    '                .GetSelectedRows()





    '            End If

    '        End With
    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class