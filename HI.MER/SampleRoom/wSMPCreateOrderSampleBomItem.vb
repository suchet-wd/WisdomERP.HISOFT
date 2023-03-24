Imports DevExpress.XtraEditors.Controls

Public Class wSMPCreateOrderSampleBomItem

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




        With ResFTStateSelect
            AddHandler .CheckedChanged, AddressOf CheckEdit_CheckedChanged
        End With

    End Sub

    Private Sub CheckEdit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

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

        With CType(ogcrcv.DataSource, DataTable)
            .AcceptChanges()

            If .Select("FTSelect='1'").Length > 0 Then
                Me.ProcessProc = True
                Me.Close()

            Else
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกข้อมูลรายการ !!!", 2010224517, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End With

    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.ProcessProc = False
        Me.Close()
    End Sub

    Private Sub FTStaReceiveAll_CheckedChanged(sender As Object, e As EventArgs) Handles FTStaReceiveAll.CheckedChanged
        Try
            Dim State As String = "0"

            If FTStaReceiveAll.Checked Then
                State = "1"
            End If


            With ogcrcv
                If Not (.DataSource Is Nothing) And ogvrcv.RowCount > 0 Then

                    With ogvrcv
                        For I As Integer = 0 To .RowCount - 1


                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), State)

                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With


        Catch ex As Exception

        End Try
    End Sub

    Private Sub ResFNRcvQty_EditValueChanging(sender As Object, e As ChangingEventArgs)

    End Sub

End Class