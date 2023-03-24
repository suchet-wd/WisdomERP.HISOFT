Imports DevExpress.XtraGrid

Public Class wRecievePopup

    Public _Proc As Boolean
    Private Property Proc As Boolean
        Get
            Return _Proc
        End Get
        Set(value As Boolean)
            _Proc = value
        End Set
    End Property
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Proc = False
        Me.Close()
    End Sub

    Private Sub ocmselect_Click(sender As Object, e As EventArgs) Handles ocmselect.Click
        Proc = True
        Me.Close()
    End Sub

    Private Sub wRecievePopup_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

        Catch ex As Exception

        End Try
    End Sub



 
  
    Private Sub ReposFNHSysDrugUnitIdTo_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ReposFNHSysDrugUnitIdTo.ButtonClick
        Try
            'Call HI.TL.HandlerControl.DynamicResponButtone_ButtonClick(sender, e)
            Call HI.TL.HandlerControl.DynamicResponButtoneSysHide_ButtonClick(sender, e)
        Catch ex As Exception

        End Try
    End Sub
End Class