Public Class wSMPQCSendSupllist
#Region "Property"

    Private _Process As Boolean = False
    Public Property Process As Boolean
        Get
            Return _Process
        End Get
        Set(value As Boolean)
            _Process = value
        End Set
    End Property

    Private _Data As DataTable = Nothing
    Public Property Data() As DataTable
        Get
            Return _Data
        End Get
        Set(value As DataTable)
            _Data = value
        End Set
    End Property

#End Region

    'Private Sub wQCSendSupllist_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    '    Try
    '        _Process = False
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub wQCSendSupllist_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    '    Try
    '        _Process = False
    '    Catch ex As Exception
    '    End Try
    'End Sub
    Private Sub wQCSendSupllist_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.ogcNotSendSupl.DataSource = _Data
            Me.ogcNotSendSupl.Refresh()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ochkselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ochkselectall.CheckedChanged
        Try
            Dim _State As String = IIf(ochkselectall.Checked = True, "1", "0")
            With ogcNotSendSupl
                If Not (.DataSource Is Nothing) And ogvNotSendSupl.RowCount > 0 Then
                    With ogvNotSendSupl
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

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Try
            _Process = False
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmcopy_Click(sender As Object, e As EventArgs) Handles ocmcopy.Click
        Try
            _Process = True
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub
End Class