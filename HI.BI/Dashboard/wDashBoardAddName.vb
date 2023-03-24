Public Class wDashBoardAddName 

    Private _ProcOK As Boolean = False
    Public Property ProcOK As Boolean
        Get
            Return _ProcOK
        End Get
        Set(value As Boolean)
            _ProcOK = value
        End Set
    End Property

    Private _DashBoadhListName As DevExpress.XtraEditors.ComboBoxEdit
    Public Property DashBoadhListName As DevExpress.XtraEditors.ComboBoxEdit
        Get
            Return _DashBoadhListName
        End Get
        Set(value As DevExpress.XtraEditors.ComboBoxEdit)
            _DashBoadhListName = value
        End Set
    End Property

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        ProcOK = False
        Me.Close()
    End Sub

    Private Sub ocmok_Click(sender As Object, e As EventArgs) Handles ocmok.Click
        If Me.FTDashboardName.Text.Trim <> "" And Me.FTDashboardName.Text.Trim.ToUpper <> "Default".ToUpper Then


            'For Each Item As DashBoadhListName.Properties.Items
            Dim _founditem As Boolean = False

            For Each _item2 As DevExpress.XtraEditors.Controls.ImageComboBoxItem In DashBoadhListName.Properties.Items

                If _item2.Value.ToString = Me.FTDashboardName.Text.Trim Then
                    _founditem = True
                    Exit For
                End If
            Next


            If Not (_founditem) Then
                ProcOK = True
                Me.Close()
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                FTDashboardName.Focus()
                FTDashboardName.SelectAll()
            End If


        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
            FTDashboardName.Focus()
            FTDashboardName.SelectAll()
        End If
    End Sub

End Class