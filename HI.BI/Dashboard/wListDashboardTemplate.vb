Public Class wListDashboardTemplate 


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private _processok As Boolean = False
    Public Property processok As Boolean
        Get
            Return _processok
        End Get
        Set(value As Boolean)
            _processok = value
        End Set
    End Property

    Private Sub ocmdownloaddashboard_Click(sender As Object, e As EventArgs) Handles ocmdownloaddashboard.Click

        With (CType(Me.ogc.DataSource, DataTable))
            .AcceptChanges()
            If .Select("FTSelect='1'").Length <= 0 Then
                Exit Sub
            End If


            processok = True
            Me.Close()
        End With

    End Sub

    Private Sub ocmuploaddashboard_Click(sender As Object, e As EventArgs) Handles ocmuploaddashboard.Click
        With (CType(Me.ogc.DataSource, DataTable))
            .AcceptChanges()
            If .Select("FTSelect='1'").Length <= 0 Then
                Exit Sub
            End If


            processok = True
            Me.Close()
        End With
    End Sub

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        processok = False
        Me.Close()
    End Sub
End Class