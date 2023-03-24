Public Class wCopyTableCut 
#Region "Property"

    Private _ProcessSave As Boolean = False
    Public Property ProcessSave As Boolean
        Get
            Return _ProcessSave
        End Get
        Set(value As Boolean)
            _ProcessSave = value
        End Set
    End Property

    Private _OrderProdNo As String = ""
    Public Property OrderProdNo As String
        Get
            Return _OrderProdNo
        End Get
        Set(value As String)
            _OrderProdNo = value
        End Set
    End Property

    Private _Mark As String = ""
    Public Property MarkID As String
        Get
            Return _Mark
        End Get
        Set(value As String)
            _Mark = value
        End Set
    End Property

    Private _TableNo As String = ""
    Public Property TableNo As String
        Get
            Return _TableNo
        End Get
        Set(value As String)
            _TableNo = value
        End Set
    End Property

    Private _ColorWay As String = ""
    Public Property ColorWay As String
        Get
            Return _ColorWay
        End Get
        Set(value As String)
            _ColorWay = value
        End Set
    End Property

    Private _PurchaseNo As String = ""
    Public Property PurchaseNo As String
        Get
            Return _PurchaseNo
        End Get
        Set(value As String)
            _PurchaseNo = value
        End Set
    End Property

    Private _DataPurchase As DataTable = Nothing
    Public Property DataPurchase() As DataTable
        Get
            Return _DataPurchase
        End Get
        Set(value As DataTable)
            _DataPurchase = value
        End Set
    End Property
#End Region

    Private Sub ocmcopy_Click(sender As Object, e As EventArgs) Handles ocmcopy.Click
        If Me.FTOrderProdNo.Text <> "" Then
            If Me.FNHSysMarkId.Text <> "" Then
                If Me.FNTableNo.Text <> "" Then
                    If Me.FNTotal.Value > 0 Then
                        Me.ProcessSave = True
                        Me.Close()
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNTotal_lbl.Text)
                        FNTotal.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNTableNo_lbl.Text)
                    FNTableNo.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysMarkId_lbl.Text)
                FNHSysMarkId.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderProdNo_lbl.Text)
            FTOrderProdNo.Focus()
        End If

    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.ProcessSave = False
        Me.Close()
    End Sub

End Class