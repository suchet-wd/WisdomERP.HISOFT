Imports System.Windows.Forms

Public Class wStockAdjustAddItemAsset

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

    Private _AssetComplete As Boolean = False
    Public Property AssetComplete As Boolean
        Get
            Return _AssetComplete
        End Get
        Set(value As Boolean)
            _AssetComplete = value
        End Set
    End Property

    Private Sub wAddItemPO_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
      
    End Sub

    Private Sub wAddItemPO_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Call HI.ST.Lang.SP_SETxLanguage(Me)
        RemoveHandler Me.FTPurchaseNo.Leave, AddressOf TL.HandlerControl.DynamicButtonedit_LeaveOnly
        RemoveHandler Me.FTPurchaseNo.EditValueChanged, AddressOf TL.HandlerControl.DynamicButtonedit_EditValueChanged
    End Sub

    Private Function ValidateData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FTAssetCode.Text <> "" Then
            If Me.FNHSysUnitAssetId.Text <> "" Then
                ' If Me.FTPurchaseNo.Text <> "" Then
                If Me.FNPrice.Value > 0 Then
                    If Me.FNQuantity.Value > 0 Then
                        _Pass = True
                    Else
                        MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNQuantity_lbl.Text)
                        Me.FNQuantity.Focus()
                    End If
                Else
                    MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNPrice_lbl.Text)
                    Me.FNPrice.Focus()
                End If
                'Else
                '    MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FTPurchaseNo_lbl.Text)
                '    Me.FTPurchaseNo.Focus()
                'End If
            Else
                MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysUnitId_lbl.Text)
                Me.FNHSysUnitAssetId.Focus()
            End If
        Else
            MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysFixedAssetId_lbl.Text)
            Me.FTAssetCode.Focus()
        End If

        Return _Pass
    End Function

    Private Sub ocmadd_Click(sender As System.Object, e As System.EventArgs) Handles ocmadd.Click
        
        If ValidateData() Then
            Me.AssetComplete = True
            Me.Close()
        End If

    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.AssetComplete = False
        Me.Close()
    End Sub

   
End Class