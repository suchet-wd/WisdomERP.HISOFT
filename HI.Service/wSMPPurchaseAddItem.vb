Public Class wSMPPurchaseAddItem

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' HI.TL.HandlerControl.AddHandlerObj(Me)
    End Sub

    Private _PONO As String = ""
    Public Property PONO As String
        Get
            Return _PONO
        End Get
        Set(value As String)
            _PONO = value
        End Set
    End Property

    Private _FNSeq As Integer = 0
    Public Property FNSeq As Integer
        Get
            Return _FNSeq
        End Get
        Set(value As Integer)
            _FNSeq = value
        End Set
    End Property

    Private _FNSuplID As Integer = 0
    Public Property FNSuplID As Integer
        Get
            Return _FNSuplID
        End Get
        Set(value As Integer)
            _FNSuplID = value
        End Set
    End Property

    Private _AddMat As Boolean = False
    Public Property AddMat As Boolean
        Get
            Return _AddMat
        End Get
        Set(value As Boolean)
            _AddMat = value
        End Set
    End Property

    Private Sub wAddItemPO_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If FTSMPMatDescription.Text = "" Then
            FTSMPMatDescription.Focus()
        Else
            FNQuantity.Focus()
            FNQuantity.SelectAll()
        End If
    End Sub

    Private Sub wAddItemPO_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Call HI.ST.Lang.SP_SETxLanguage(Me)
    End Sub

    Private Sub Calculate(sender As System.Object, e As System.EventArgs) Handles FNPrice.EditValueChanged, FNQuantity.EditValueChanged
        Static _Proc As Boolean

        If Not (_Proc) Then
            _Proc = True

            Dim _Qty As Double = FNQuantity.Value
            Dim _Price As Double = FNPrice.Value
            Dim _DisPer As Double = 0
            Dim _DisAmt As Double = 0

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

            FNAmount.Value = CDbl(Format(_Qty * (_Price - _DisAmt), HI.ST.Config.AmtFormat))

            _Proc = False
        End If
    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.AddMat = False
        Me.Close()
    End Sub

    Private Function ValidateData() As Boolean
        Dim _Pass As Boolean = False

        If FTSMPOrderNo.Text <> "" Then
            If FTSMPMatCode.Text <> "" Then
                If Me.FTSMPMatDescription.Text <> "" Then
                    If FNHSysUnitIdPO.Text <> "" Then
                        If FNQuantity.Value > 0 Then

                            _Pass = True

                        Else

                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysUnitId_lbl.Text)
                            FNHSysUnitIdPO.Focus()

                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDescription_lbl.Text)
                        FTSMPMatDescription.Focus()
                    End If

                Else

                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDescription_lbl.Text)
                    FTSMPMatDescription.Focus()

                End If

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTMatCode_lbl.Text)
                FTSMPMatCode.Focus()
            End If

        Else

            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTSMPOrderNo_lbl.Text)
            FTSMPOrderNo.Focus()

        End If

        Return _Pass
    End Function

    Private Sub ocmadd_Click(sender As System.Object, e As System.EventArgs) Handles ocmadd.Click

        If ValidateData() Then

            Me.AddMat = True
            Me.Close()

        End If

    End Sub

End Class