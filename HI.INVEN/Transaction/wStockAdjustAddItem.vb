Imports System.Windows.Forms

Public Class wStockAdjustAddItem

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' HI.TL.HandlerControl.AddHandlerObj(Me)
    End Sub

    Private _DocNo As String = ""
    Public Property DocNo As String
        Get
            Return _DocNo
        End Get
        Set(value As String)
            _DocNo = value
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
        If FNHSysRawMatId.Text = "" Then
            FNHSysRawMatId.Focus()
        Else
            FNPOQuantity.Focus()
            FNPOQuantity.SelectAll()
        End If
    End Sub

    Private Sub wAddItemPO_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Call HI.ST.Lang.SP_SETxLanguage(Me)
    End Sub

    Private Sub Calculate(sender As System.Object, e As System.EventArgs) Handles FNPOPrice.EditValueChanged, FNPOQuantity.EditValueChanged
        'Static _Proc As Boolean

        'If Not (_Proc) Then
        '    _Proc = True

        '    Dim _Qty As Double = FNPOQuantity.Value
        '    Dim _Price As Double = FNPOPrice.Value
        '    Dim _DisPer As Double = FNDisPer.Value
        '    Dim _DisAmt As Double = FNDisAmt.Value


        '    Select Case sender.Name.ToString.ToUpper
        '        Case "FNPrice".ToUpper
        '            If _DisPer > 0 Then
        '                _DisAmt = CDbl(Format((_Price * _DisPer) / 100, HI.ST.Config.PriceFormat))
        '            Else
        '                _DisAmt = 0
        '            End If
        '        Case "FNDisPer".ToString.ToUpper
        '            If _Price > 0 Then
        '                _DisAmt = CDbl(Format((_Price * _DisPer) / 100, HI.ST.Config.PriceFormat))
        '            Else
        '                _DisAmt = 0
        '            End If
        '    End Select

        '    FNDisAmt.Value = _DisAmt
        '    FNNetAmt.Value = CDbl(Format(_Qty * (_Price - _DisAmt), HI.ST.Config.AmtFormat))

        '    _Proc = False
        'End If
    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.AddMat = False
        Me.Close()
    End Sub

    Private Function ValidateData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FNHSysRawMatId.Text <> "" And Me.FNHSysRawMatId.Properties.Tag.ToString <> "" Then
            If Me.FNHSysUnitIdPO.Text <> "" And Me.FNHSysUnitIdPO.Properties.Tag.ToString <> "" Then
                If (Me.FTOrderNo.Text <> "" And Me.FTOrderNo.Properties.Tag.ToString <> "") Or FTOrderNo.Visible = False Then
                    If Me.FTPurchaseNo.Text.Trim <> "" Then
                        If FNPOQuantity.Value > 0 Then
                            _Pass = True
                        Else
                            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNQuantity_lbl.Text)
                            FNPOQuantity.Focus()
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPurchaseNo_lbl.Text)
                        FTPurchaseNo.Focus()
                    End If
                Else
                    If FTOrderNo.Visible Then
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTOrderNo_lbl.Text)
                        FTOrderNo.Focus()
                    End If
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysUnitId_lbl.Text)
                FNHSysUnitIdPO.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysRawMatId_lbl.Text)
            FNHSysRawMatId.Focus()
        End If

        Return _Pass
    End Function

    Private Sub ocmadd_Click(sender As System.Object, e As System.EventArgs) Handles ocmadd.Click
        If HI.ST.ValidateData.CloseJob(FTOrderNo.Text) Then
            HI.MG.ShowMsg.mInfo("บัญชีได้ทำการปิดจ๊อบแล้วไม่สามารถทำรายการใดๆได้อีก !!!", 1502260678, Me.Text, , MessageBoxIcon.Warning)
            Exit Sub
        End If
        If ValidateData() Then
            Me.AddMat = True
            Me.Close()
        End If

    End Sub



    Private Sub FNOrderType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles FNOrderType.SelectedIndexChanged
        'Try
        '    Select Case FNOrderType.SelectedIndex
        '        Case 0
        '            FNHSysRawMatId.Properties.Buttons(0).Tag = "136"

        '            If FNHSysRawMatId.Text <> "" Then
        '                Call HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(FNHSysRawMatId, New System.EventArgs)
        '            End If

        '        Case Else
        '            FNHSysRawMatId.Properties.Buttons(0).Tag = "106"

        '            If FNHSysRawMatId.Text <> "" Then
        '                Call HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(FNHSysRawMatId, New System.EventArgs)
        '            End If
        '    End Select
        'Catch ex As Exception

        'End Try

    End Sub

    Private Sub FTOrderNo_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FTOrderNo.EditValueChanged

    End Sub

    Private Sub FNHSysRawMatId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysRawMatId.EditValueChanged

    End Sub
End Class