Imports System.Windows.Forms

Public Class wAddItemPOAsset

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Property"
    Private _AddComplete As Boolean = False
    Public Property AddComplete As Boolean
        Get
            Return _AddComplete
        End Get
        Set(value As Boolean)
            _AddComplete = value
        End Set
    End Property
    Private _PONO As String = ""
    Public Property PONO As String
        Get
            Return _PONO
        End Get
        Set(value As String)
            _PONO = value
        End Set
    End Property
#End Region
  
    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.AddComplete = False
        Me.Close()
    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        If Verify() Then
            Me.AddComplete = True
            Me.Close()
        End If
    End Sub

    Private Function Verify() As Boolean
        Dim _Pass As Boolean = False
        If Me.FTAssetCode.Text <> "" Then
            If Me.FNHSysUnitAssetId.Text <> "" Then
                If Me.FNPrice.Value > 0 Then
                    If Me.FNQuantity.Value > 0 Then
                        _Pass = True
                    Else
                        MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNQuantity_lbl.Text)
                        Me.FNQuantity.Focus()
                    End If
                Else
                    MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNPrice_lbl.Text)
                    Me.FNPrice.Focus()
                End If
            Else
                MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysUnitId_lbl.Text)
                Me.FNHSysUnitAssetId.Focus()
            End If
        Else
            MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysFixedAssetId_lbl.Text)
            Me.FTAssetCode.Focus()
        End If
        Return _Pass
    End Function

    Private Sub Calculate(sender As System.Object, e As System.EventArgs) Handles FNPrice.EditValueChanged, FNDisPer.EditValueChanged, FNQuantity.EditValueChanged
        Static _Proc As Boolean

        If Not (_Proc) Then
            _Proc = True

            Dim _Qty As Double = FNQuantity.Value
            Dim _Price As Double = FNPrice.Value
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

    Private Sub FNHSysUnitAssetId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysUnitAssetId.EditValueChanged

    End Sub
End Class