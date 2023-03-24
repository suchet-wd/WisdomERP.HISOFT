Public Class wFindTableCut

#Region "Property"

    Private _MainParentForm As Object = Nothing
    Public Property MainParentForm As Object
        Get
            Return _MainParentForm
        End Get
        Set(value As Object)
            _MainParentForm = value
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

    Private _TableNo As String = ""
    Public Property TableNo As String
        Get
            Return _TableNo
        End Get
        Set(value As String)
            _TableNo = value
        End Set
    End Property

#End Region

    Private Sub FindDataTable()
        If Me.FTOrderProdNo.Text <> "" Then

            If Me.FNTableCutNo.Value > 0 Then
                If Me.FNSTableCutNo.Value <= Me.FNTableCutNo.Value And Me.FNETableCutNo.Value >= Me.FNTableCutNo.Value Then
                    Try

                        Call CallByName(Me.MainParentForm, "SearchDataTableCut", CallType.Method, {Me.FNTableCutNo.Value})
                    Catch ex As Exception
                    End Try
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNTableCutNo_lbl.Text)
                FNTableCutNo.Focus()
            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderProdNo_lbl.Text)
            FTOrderProdNo.Focus()
        End If
    End Sub
    Private Sub ocmcopy_Click(sender As Object, e As EventArgs) Handles ocmfind.Click
        Call FindDataTable()

    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.Close()
    End Sub

    Private Sub FNTableCutNo_EditValueChanged(sender As Object, e As EventArgs) Handles FNTableCutNo.EditValueChanged

    End Sub

    Private Sub FNTableCutNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FNTableCutNo.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Enter
                Call FindDataTable()
        End Select
    End Sub
End Class