Public Class wAddRequestAsset
    Private _ChkDisPer As Boolean = True
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' HI.TL.HandlerControl.AddHandlerObj(Me)
    End Sub
#Region "Property"
    Private _PRNO As String = ""
    Public Property PRNO As String
        Get
            Return _PRNO
        End Get
        Set(value As String)
            _PRNO = value
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

#Region "Function"
    Private Function ValidateData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FTAssetCode.Text <> "" Then
            If Me.FNQuantity.Value > 0 Then
                If Me.FNPrice.Value > 0 Then
                    If Me.FNHSysUnitAssetId.Text <> "" Then
                        _Pass = True
                    Else
                        MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysUnitId_lbl.Text)
                        Me.FNHSysUnitAssetId.Focus()
                    End If
                Else
                    MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNPrice_lbl.Text)
                    Me.FNPrice.Focus()
                End If
            Else
                MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNQuantity_lbl.Text)
                Me.FNQuantity.Focus()
            End If
        Else
            MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysFixedAssetId_lbl.Text)
            Me.FTAssetCode.Focus()
        End If

        Return _Pass
    End Function

#End Region


    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.AddMat = False
        Me.Close()
    End Sub

    Private Sub wAddRequest_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call HI.ST.Lang.SP_SETxLanguage(Me)
        Me.ProcLoad = False
    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        If ValidateData() Then
            Me.AddMat = True
            Me.Close()
        End If
    End Sub

    Private Sub _Calc(sender As Object, e As EventArgs) Handles FNDisAmt.EditValueChanged, FNDisPer.EditValueChanged, FNQuantity.EditValueChanged, FNPrice.EditValueChanged


        If Not (ProcLoad) Then
            'Proc = True
            Dim _POamt As Double = FNQuantity.Value * FNPrice.Value
            If _POamt = 0 Then
                FNDisAmt.Value = 0
                FNDisPer.Value = 0
            End If

            Dim _Disper As Double = FNDisPer.Value
            Dim _Disamt As Double = FNDisAmt.Value

            'Select Case sender.name.ToString
            '    Case "FNDisPer"
            '        If _Disper < 100 Then
            '            _Disamt = Format((_POamt * _Disper) / 100, ST.Config.AmtFormat)
            '            FNDisAmt.Value = _Disamt
            '        Else
            '            MG.ShowMsg.mInfo("ถ้าจะใส่แบบนี้ แจกฟรีไปเหอะ ไม่ต้องมาเปิด PO. กลับไปใส่ใหม่ by Programer JOKER", 1611171458, Me.Text, "!@#$@%&$&*", System.Windows.Forms.MessageBoxIcon.Warning)
            '            FNDisPer.Value = 0
            '            FNDisPer.Focus()
            '        End If
            '    Case "FNDisAmt"
            '        If _POamt > 0 Then
            '            If _Disamt < _POamt Then
            '                _Disper = Format((FNDisAmt.Value * 100) / _POamt, ST.Config.PercentFormat)
            '            Else
            '                MG.ShowMsg.mInfo("ถ้าจะใส่แบบนี้ แจกฟรีไปเหอะ ไม่ต้องมาเปิด PO. กลับไปใส่ใหม่ by Programer JOKER", 1611171458, Me.Text, "!@#$@%&$&*", System.Windows.Forms.MessageBoxIcon.Warning)
            '                FNDisAmt.Value = 0
            '                FNDisAmt.Focus()
            '            End If
            '        Else
            '            _Disper = 0
            '        End If
            '        FNDisPer.Value = _Disper
            '    Case Else
            '        'FNNetAmt.Value = _POamt - _Disamt
            'End Select
            If sender.name.ToString = "FNDisPer" Then
                If _ChkDisPer Then
                    If _Disper < 100 Then
                        _Disamt = Format((_POamt * _Disper) / 100, ST.Config.AmtFormat)
                        FNDisAmt.Value = _Disamt
                    Else
                        MG.ShowMsg.mInfo("ถ้าจะใส่แบบนี้ แจกฟรีไปเหอะ ไม่ต้องมาเปิด PO. กลับไปใส่ใหม่ by Programer JOKER", 1611171458, Me.Text, "!@#$@%&$&*", System.Windows.Forms.MessageBoxIcon.Warning)
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
                            MG.ShowMsg.mInfo("ถ้าจะใส่แบบนี้ แจกฟรีไปเหอะ ไม่ต้องมาเปิด PO. กลับไปใส่ใหม่ by Programer JOKER", 1611171458, Me.Text, "!@#$@%&$&*", System.Windows.Forms.MessageBoxIcon.Warning)
                            FNDisAmt.Value = 0
                            FNDisAmt.Focus()
                        End If
                    Else
                        _Disper = 0
                    End If
                    FNDisPer.Value = _Disper
                End If
            End If
            FNNetAmt.Value = _POamt - _Disamt
            _ChkDisPer = True
        End If
    End Sub

    Private Sub CheckDiscount(ByVal _NameDiscountType As String)
        If _NameDiscountType = "FNDisPer" Then
            _ChkDisPer = True
        Else
            _ChkDisPer = False
        End If
    End Sub

    Private Sub FNNetAmt_EditValueChanged(sender As Object, e As EventArgs) Handles FNNetAmt.EditValueChanged

    End Sub
End Class