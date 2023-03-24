Imports System.Windows.Forms

Public Class wAddItemPRServiceAsset
    Private _Load As Boolean = False
    Private _ChkDisPer As Boolean = True
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _Load = False
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
    Private _PONO1 As String = ""
    Public Property PONO1 As String
        Get
            Return _PONO1
        End Get
        Set(value As String)
            _PONO1 = value
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

    Private _ProcLoad As Boolean
    Public Property ProcLoad As Boolean
        Get
            Return _ProcLoad
        End Get
        Set(value As Boolean)
            _ProcLoad = value
        End Set
    End Property

#End Region

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        ' Me.AddComplete = False
        Me.Close()
    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        If Verify() Then
            Me.AddMat = True
            Me.Close()
        End If
    End Sub

    Private Function Verify() As Boolean
        Dim _Pass As Boolean = False

        If Me.FTDescription.Text <> "" Then
            If Me.FNQuantity.Value > 0 Then
                If Me.FNPrice.Value > 0 Then
                    _Pass = True
                Else
                    MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNPrice_lbl.Text)
                    Me.FNPrice.Focus()
                End If
            Else
                MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNQuantity_lbl.Text)
                Me.FNQuantity.Focus()
            End If
        Else
            MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTDescription_lbl.Text)
            Me.FTDescription.Focus()
        End If
        Return _Pass
    End Function

    Private Sub Calculate(sender As System.Object, e As System.EventArgs) Handles FNDisAmt.EditValueChanged, FNDisPer.EditValueChanged, FNQuantity.EditValueChanged, FNPrice.EditValueChanged
        If _Load Then
            '    Dim _Qty As Double = FNQuantity.Value
            '    Dim _Price As Double = FNPrice.Value
            '    Try
            '        Select Case sender.Name.ToString.ToUpper
            '            Case "FNPrice".ToUpper
            '                If _Price > 0 Then
            '                    FNAmount.Value = CDbl(Format(_Qty * _Price, HI.ST.Config.AmtFormat))
            '                Else
            '                    FNAmount.Value = 0
            '                End If
            '            Case "FNQuantity".ToString.ToUpper
            '                If _Qty > 0 Then
            '                    FNAmount.Value = CDbl(Format(_Qty * _Price, HI.ST.Config.AmtFormat))
            '                Else
            '                    FNAmount.Value = 0
            '                    FNPrice.Value = 0
            '                End If
            '        End Select
            '    Catch ex As Exception

            '    End Try
            'End If
            ' If Not (ProcLoad) Then
            'Proc = True
            Dim _POamt As Double = FNQuantity.Value * FNPrice.Value
            If _POamt = 0 Then
                FNDisAmt.Value = 0
                FNDisPer.Value = 0
            End If

            Dim _Disper As Double = FNDisPer.Value
            Dim _Disamt As Double = FNDisAmt.Value


            If sender.name.ToString = "FNDisPer" Then
                If _ChkDisPer Then
                    If _Disper < 100 Then
                        _Disamt = Format((_POamt * _Disper) / 100, ST.Config.AmtFormat)
                        FNDisAmt.Value = _Disamt
                    Else
                        MG.ShowMsg.mInfo("...", 1611171458, Me.Text, "!@#$@%&$&*", System.Windows.Forms.MessageBoxIcon.Warning)
                        FNDisPer.Value = 0
                        FNDisPer.Focus()
                    End If
                End If
            ElseIf sender.name.ToString = "FNDisAmt" Then
                Call CheckDiscount(sender.name.ToString)
                If Not (_ChkDisPer) Then
                    If _POamt > 0 Then
                        If _Disamt < _POamt Then
                            _Disper = Format((FNDisAmt.Value * 100) / _POamt, ST.Config.PercentFormat)
                        Else
                            MG.ShowMsg.mInfo("...", 1611171458, Me.Text, "!@#$@%&$&*", System.Windows.Forms.MessageBoxIcon.Warning)
                            FNDisAmt.Value = 0
                            FNDisAmt.Focus()
                        End If
                    Else
                        _Disper = 0
                    End If
                    FNDisPer.Value = _Disper
                End If
            End If
            FNAmount.Value = _POamt - _Disamt
            _ChkDisPer = True
        End If
    End Sub

    Private Sub wAddItemPOServiceAsset_Load(sender As Object, e As EventArgs) Handles Me.Load
        If FTDescription.Text = "" Then
            FTDescription.Focus()
            FNQuantity.Value = 0
            FNPrice.Value = 0
        Else
            FNQuantity.Focus()
            FNQuantity.SelectAll()
        End If

        _Load = True
    End Sub

    Private Sub FTAssetCode_EditValueChanged(sender As Object, e As EventArgs) Handles FTAssetCode.EditValueChanged
        FNPrice.Value = 0
    End Sub
    Private Sub CheckDiscount(ByVal _NameDiscountType As String)
        If _NameDiscountType = "FNDisPer" Then
            _ChkDisPer = True
        Else
            _ChkDisPer = False
        End If
    End Sub
End Class