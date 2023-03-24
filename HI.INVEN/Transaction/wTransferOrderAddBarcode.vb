Public Class wTransferOrderAddBarcode

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        With ReposFTSelect
            AddHandler .CheckedChanged, AddressOf CheckEdit_CheckedChanged
        End With
        With ReposFNReserveQty
            .Precision = HI.ST.Config.QtyDigit
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave
            AddHandler .EditValueChanged, AddressOf CalcEdit_EditValueChanged
        End With

        With ogvbarcode
            .Columns("FNQuantityBal").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantityBal")
            .Columns("FNQuantityBal").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .Columns("FNReserveQty").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNReserveQty")
            .Columns("FNReserveQty").SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit & "}"
            .OptionsView.ShowFooter = True
        End With

    End Sub

    Private Sub CheckEdit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

            If CType(sender, DevExpress.XtraEditors.CheckEdit).Checked Then
                Dim _balQty As Double = .GetFocusedRowCellValue("FNQuantityBal")
                .SetFocusedRowCellValue("FNReserveQty", _balQty)
            Else
                .SetFocusedRowCellValue("FNReserveQty", 0)
            End If

        End With
    End Sub

    Private Sub CalcEdit_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

            Dim _balQty As Double = .GetFocusedRowCellValue("FNQuantityBal")

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

    Private Sub wReserveItemPopup_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
    End Sub

    Private Sub ocmreceive_Click(sender As System.Object, e As System.EventArgs) Handles ocmok.Click
        Me.ProcessProc = True
        Me.Close()
    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.ProcessProc = False
        Me.Close()
    End Sub

End Class