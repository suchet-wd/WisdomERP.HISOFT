Public Class wReceiveMultipleAsset


    Private _PurchaseNo As String = ""
    Public Property PurchaseNo() As String
        Get
            Return _PurchaseNo
        End Get
        Set(value As String)
            _PurchaseNo = value
        End Set
    End Property

    Private _ReceiveNo As String = ""
    Public Property ReceiveNo() As String
        Get
            Return _ReceiveNo
        End Get
        Set(value As String)
            _ReceiveNo = value
        End Set
    End Property


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        With ResFNPOBalQty
            .Precision = HI.ST.Config.QtyDigit
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave
        End With

        With ResFNQuantity
            .Precision = HI.ST.Config.QtyDigit
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave
        End With

        With ResFNRcvHisQty
            .Precision = HI.ST.Config.QtyDigit
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave
        End With

        With ResFNRcvQty
            .Precision = HI.ST.Config.QtyDigit
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave
        End With

        With ResFTStateSelect
            AddHandler .CheckedChanged, AddressOf CheckEdit_CheckedChanged
        End With

    End Sub

    Private Sub CheckEdit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

            If CType(sender, DevExpress.XtraEditors.CheckEdit).Checked Then
                Dim _balQty As Double = .GetFocusedRowCellValue("FNPOBalQty")
                .SetFocusedRowCellValue("FNRcvQty", _balQty)
            Else
                .SetFocusedRowCellValue("FNRcvQty", 0)
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

    Private Function ValidateData() As Boolean
        Dim _Pass As Boolean = False
        Dim _TotalQty As Double = 0
        With CType(Me.ogcrcv.DataSource, DataTable)
            .AcceptChanges()

            For Each R As DataRow In .Rows
                _TotalQty = _TotalQty + Double.Parse(Val(R!FNOrderRcvQuantity.ToString))
            Next

        End With

        If _TotalQty = CFNQuantity.Value Then
            _Pass = True
        Else
            HI.MG.ShowMsg.mInfo("จำนวนเกลี่ยยอดไม่เท่ากับจำนวนรับ กรุณาทำการตรวจสอบยอดเกลี่ย !!!", 1408300001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function

    Private Sub ocmreceive_Click(sender As System.Object, e As System.EventArgs) Handles ocmreceive.Click
        If ValidateData() Then
            Me.ProcessProc = True
            Me.Close()
        End If

    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.ProcessProc = False
        Me.Close()
    End Sub

End Class