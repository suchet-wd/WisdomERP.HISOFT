Imports DevExpress.XtraEditors.Controls

Public Class wSMPReceiveItem

    Public Enum RcvType As Integer
        RcvNormal = 0
        RcvRepire = 1
        RcvFree = 2
    End Enum

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

    Private _ReceiveType As RcvType = RcvType.RcvNormal
    Public Property ReceiveType() As RcvType
        Get
            Return _ReceiveType
        End Get
        Set(value As RcvType)
            _ReceiveType = value
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
            AddHandler .EditValueChanging, AddressOf FNRcvQty_EditValueChanging
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

    Private Sub RcvEdit_EditChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
            Select Case Me.ReceiveType
                Case RcvType.RcvNormal
                    Dim _RcvOverQty As Double = 0
                    Dim _RcvQty As Double = CType(sender, DevExpress.XtraEditors.CalcEdit).Value
                    Dim _SysMatID As Integer = .GetFocusedRowCellValue("FNHSysRawMatId")

                    _RcvOverQty = 0

                    If _RcvOverQty > 0 Then

                        .SetFocusedRowCellValue("FNRcvQty", (_RcvQty - _RcvOverQty))

                        'If .GetFocusedRowCellValue("FTStateSendAppRcv").ToString = "1" Then

                        '    .SetFocusedRowCellValue("FNRcvQty", _RcvQty)
                        '    .SetFocusedRowCellValue("FNRcvQtyPass", (_RcvQty - _RcvOverQty))
                        '    .SetFocusedRowCellValue("FNRcvQtyOver", _RcvOverQty)

                        'Else


                        '    If _RcvQty >= _RcvOverQty Then
                        '            .SetFocusedRowCellValue("FNRcvQty", (_RcvQty - _RcvOverQty))
                        '        Else
                        '            .SetFocusedRowCellValue("FNRcvQty", 0)
                        '        End If


                        'End If
                    Else
                        .SetFocusedRowCellValue("FNRcvQtyPass", _RcvQty)
                        .SetFocusedRowCellValue("FNRcvQtyOver", 0)

                    End If

                    Try
                        CType(sender.Parent.datasource, DataTable).AcceptChanges()
                    Catch ex As Exception
                    End Try

                Case Else
            End Select
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

    Private Sub ocmreceive_Click(sender As System.Object, e As System.EventArgs) Handles ocmreceive.Click
        Me.ProcessProc = True
        Me.Close()
    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.ProcessProc = False
        Me.Close()
    End Sub



    Private Sub ResFTStateSelect_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ResFTStateSelect.EditValueChanging

        Try
            Select Case e.NewValue.ToString
                Case "1"

                    e.Cancel = False


            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Sub FTStaReceiveAll_CheckedChanged(sender As Object, e As EventArgs) Handles FTStaReceiveAll.CheckedChanged

    End Sub

    Private Sub FNRcvQty_EditValueChanging(sender As Object, e As ChangingEventArgs)

        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                'Dim _Bal As Integer = .GetFocusedRowCellValue("FNPOBalQty")

                'If e.NewValue > _Bal Then
                '    e.Cancel = True
                'Else
                'e.Cancel = False
                'End If

                If Val(e.NewValue) < 0 Then
                    e.Cancel = True
                Else
                    e.Cancel = False
                End If
            End With
        Catch ex As Exception

        End Try

    End Sub

End Class