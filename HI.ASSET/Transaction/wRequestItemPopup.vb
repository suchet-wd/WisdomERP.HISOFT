Public Class wRequestItemPopup
    Private _ChkDisPer As Boolean = True
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        With ReposFTSelect
            AddHandler .CheckedChanged, AddressOf CheckEdit_CheckedChanged
        End With
        With ReposReposFNReserveQty
            .Precision = HI.ST.Config.QtyDigit
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave
            AddHandler .EditValueChanged, AddressOf CalcEdit_EditValueChanged
        End With
        With ReposFNNetAmt
            .Precision = HI.ST.Config.QtyDigit
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave
            AddHandler .EditValueChanged, AddressOf CalcEdit_EditValueChanged
        End With
     
        With ogvbarcode
            .Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            .Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.AmtDigit & "}"
            .Columns("FNNetAmt").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNNetAmt")
            .Columns("FNNetAmt").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.AmtDigit & "}"

            .OptionsView.ShowFooter = True
        End With
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


    Private Sub CheckEdit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

            If CType(sender, DevExpress.XtraEditors.CheckEdit).Checked Then
                Dim _balQty As Double = .GetFocusedRowCellValue("FNQuantity")
                .SetFocusedRowCellValue("FNQuantity", _balQty)
            Else
                .SetFocusedRowCellValue("FNQuantity", 0)
            End If

        End With
    End Sub

    Private Sub CalcEdit_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

            Dim _balQty As Double = .GetFocusedRowCellValue("FNNetAmt")

            If sender.value > _balQty Then
                sender.value = _balQty
            End If

        End With
    End Sub

    Private _ProcessProc As Boolean = False
    Public Property ProcessProc As Boolean
        Get
            Return _ProcessProc
        End Get
        Set(value As Boolean)
            _ProcessProc = value
        End Set
    End Property

    'Private Sub wReserveItemPopup_Load(sender As Object, e As EventArgs) Handles Me.Load
    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
        AddHandler RepoFNHSysUnitAssetId.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick
    End Sub

    Private Sub ocmreceive_Click(sender As System.Object, e As System.EventArgs) Handles ocmok.Click
        Me.ProcessProc = True
        Me.Close()
    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.ProcessProc = False
        Me.Close()
    End Sub

    Private Sub ogcbarcode_Click(sender As Object, e As EventArgs) Handles ogcbarcode.Click

    End Sub
    'Private Sub _Calc(sender As Object, e As EventArgs) Handles FNDisAmt.EditValueChanged, FNDisPer.EditValueChanged, FNQuantity.EditValueChanged, FNPrice.EditValueChanged


    '    If Not (ProcLoad) Then
    '        'Proc = True
    '        Dim _POamt As Double = FNQuantity.ToString * FNPrice.ToString
    '        'If _POamt = 0 Then
    '        '    FNDisAmt.ToString = 0
    '        '    FNDisPer.ToString = 0
    '        'End If

    '        Dim _Disper As Double = FNDisPer.ToString
    '        Dim _Disamt As Double = FNDisAmt.ToString

    '        'Select Case sender.name.ToString
    '        '    Case "FNDisPer"
    '        '        If _Disper < 100 Then
    '        '            _Disamt = Format((_POamt * _Disper) / 100, ST.Config.AmtFormat)
    '        '            FNDisAmt.Value = _Disamt
    '        '        Else
    '        '            MG.ShowMsg.mInfo("ถ้าจะใส่แบบนี้ แจกฟรีไปเหอะ ไม่ต้องมาเปิด PO. กลับไปใส่ใหม่ by Programer JOKER", 1611171458, Me.Text, "!@#$@%&$&*", Windows.Forms.MessageBoxIcon.Warning)
    '        '            FNDisPer.Value = 0
    '        '            FNDisPer.Focus()
    '        '        End If
    '        '    Case "FNDisAmt"
    '        '        If _POamt > 0 Then
    '        '            If _Disamt < _POamt Then
    '        '                _Disper = Format((FNDisAmt.Value * 100) / _POamt, ST.Config.PercentFormat)
    '        '            Else
    '        '                MG.ShowMsg.mInfo("ถ้าจะใส่แบบนี้ แจกฟรีไปเหอะ ไม่ต้องมาเปิด PO. กลับไปใส่ใหม่ by Programer JOKER", 1611171458, Me.Text, "!@#$@%&$&*", Windows.Forms.MessageBoxIcon.Warning)
    '        '                FNDisAmt.Value = 0
    '        '                FNDisAmt.Focus()
    '        '            End If
    '        '        Else
    '        '            _Disper = 0
    '        '        End If
    '        '        FNDisPer.Value = _Disper
    '        '    Case Else
    '        '        'FNNetAmt.Value = _POamt - _Disamt
    '        'End Select
    '        If sender.name.ToString = "FNDisPer" Then
    '            If _ChkDisPer Then
    '                If _Disper < 100 Then
    '                    _Disamt = Format((_POamt * _Disper) / 100, ST.Config.AmtFormat)
    '                    FNDisAmt.ToString = _Disamt
    '                Else
    '                    MG.ShowMsg.mInfo("ถ้าจะใส่แบบนี้ แจกฟรีไปเหอะ ไม่ต้องมาเปิด PO. กลับไปใส่ใหม่ by Programer JOKER", 1611171458, Me.Text, "!@#$@%&$&*", Windows.Forms.MessageBoxIcon.Warning)
    '                    FNDisPer.ToString = 0
    '                    'FNDisPer.Focus()
    '                End If
    '            End If
    '        ElseIf sender.name.ToString = "FNDisAmt" Then
    '            Call CheckDiscount(sender.name.ToString)
    '            If Not (_ChkDisPer) Then
    '                If _POamt > 0 Then
    '                    If _Disamt < _POamt Then
    '                        _Disper = Format((FNDisAmt.ToString * 100) / _POamt, ST.Config.PercentFormat)
    '                    Else
    '                        MG.ShowMsg.mInfo("ถ้าจะใส่แบบนี้ แจกฟรีไปเหอะ ไม่ต้องมาเปิด PO. กลับไปใส่ใหม่ by Programer JOKER", 1611171458, Me.Text, "!@#$@%&$&*", Windows.Forms.MessageBoxIcon.Warning)
    '                        FNDisAmt.ToString = 0
    '                        FNDisAmt.Focus()
    '                    End If
    '                Else
    '                    _Disper = 0
    '                End If
    '                FNDisPer.ToString = _Disper
    '            End If
    '        End If
    '        ReposFNNetAmt.value = _POamt - _Disamt
    '        _ChkDisPer = True
    '    End If
    'End Sub
    Private Sub CheckDiscount(ByVal _NameDiscountType As String)
        If _NameDiscountType = "FNDisPer" Then
            _ChkDisPer = True
        Else
            _ChkDisPer = False
        End If
    End Sub
End Class