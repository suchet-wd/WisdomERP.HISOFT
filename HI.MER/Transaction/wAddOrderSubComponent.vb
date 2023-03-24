Public Class wAddOrderSubComponent 
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private _AddComponent As Boolean = False
    Public Property AddComponent As Boolean
        Get
            Return _AddComponent
        End Get
        Set(value As Boolean)
            _AddComponent = value
        End Set
    End Property

    Private Sub wAddOrderSubComponent_Load(sender As Object, e As EventArgs) Handles Me.Load

        Call HI.ST.Lang.SP_SETxLanguage(Me)
        Me.AddComponent = False

    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.Close()
    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        If FNSeq.Value > 0 Then
            If Me.FNHSysMerMatId.Text <> "" Then
                If Me.FNHSysMerMatId.Properties.Tag.ToString <> "" Then

                    If Me.FTComponent.Text.Trim <> "" Then

                        Me.AddComponent = True
                        Me.Close()

                    Else

                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTComponent_lbl.Text)
                        FTComponent.Focus()

                    End If

                Else

                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysMerMatId_lbl.Text)
                    FNHSysMerMatId.Focus()

                End If

            Else

                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysMerMatId_lbl.Text)
                FNHSysMerMatId.Focus()

            End If

        Else

            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysMerMatId_lbl.Text)
            FNSeq.Focus()

        End If
    End Sub

End Class