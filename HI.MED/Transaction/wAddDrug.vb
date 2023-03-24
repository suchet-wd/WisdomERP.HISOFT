

Public Class wAddDrug


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Property"
    Public _StateSave As Boolean = False
    Private Property StateSave As Boolean
        Get
            Return _StateSave
        End Get
        Set(value As Boolean)
            _StateSave = value
        End Set
    End Property


#End Region



    Private Sub Calculate(sender As System.Object, e As System.EventArgs) Handles FNPOPrice.EditValueChanged, FNDisPer.EditValueChanged, FNPOQuantity.EditValueChanged
        Static _Proc As Boolean

        If Not (_Proc) Then
            _Proc = True

            Dim _Qty As Double = FNPOQuantity.Value
            Dim _Price As Double = FNPOPrice.Value
            Dim _DisPer As Double = FNDisPer.Value
            Dim _DisAmt As Double = FNDisAmt.Value

            Select Case sender.Name.ToString.ToUpper
                Case "FNPrice".ToUpper
                    If _DisPer > 0 Then
                        _DisAmt = CDbl(Format((_Price * _DisPer) / 100, HI.ST.Config.PriceFormat))
                    Else
                        _DisAmt = 0
                    End If
                Case "FNDisPer".ToString.ToUpper

                    If _Price > 0 Then
                        _DisAmt = CDbl(Format((_Price * _DisPer) / 100.0, HI.ST.Config.PriceFormat))
                    Else
                        _DisAmt = 0
                    End If

            End Select

            FNDisAmt.Value = _DisAmt
            FNNetAmt.Value = CDbl(Format(_Qty * (_Price - _DisAmt), HI.ST.Config.AmtFormat))

            _Proc = False
        End If
    End Sub


    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        Try
            If Verrify() Then
                StateSave = True
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function Verrify() As Boolean
        Try
            If Me.FNHSysDrugId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysDrugId_lbl.Text)
                Me.FNHSysDrugId.Focus()
                Return False
            End If

            If Me.FNHSysDrugUnitId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysDrugUnitId_lbl.Text)
                Me.FNHSysDrugUnitId.Focus()
                Return False
            End If

            If Me.FNPOPrice.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNPOPrice_lbl.text)
                Me.FNPOPrice.Focus()
                Return False
            End If

            If Me.FNPOQuantity.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNPOQuantity_lbl.text)
                Me.FNPOQuantity.Focus()
                Return False
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Try
            StateSave = False
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub
End Class