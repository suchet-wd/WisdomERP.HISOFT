

Public Class wShowRejectDC
    Private Shared _Reason As String
    ' Private Shared _frmReject As wShowReject = Nothing

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        '' Add any initialization after the InitializeComponent() call.
        'HI.TL.HandlerControl.AddHandlerObj(_frmReject)

        'Dim oSysLang As New ST.SysLanguage
        'Try
        '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmReject.Name.ToString.Trim, _frmReject)
        'Catch ex As Exception
        'Finally
        'End Try

        ' Add any initialization after the InitializeComponent() call.
        HI.TL.HandlerControl.AddHandlerObj(Me)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name, Me)
        Catch ex As Exception
        Finally
        End Try

    End Sub
    Public Shared Property Data_Reason As String
        Get
            Return _Reason
        End Get
        Set(value As String)
            _Reason = value
        End Set
    End Property

    Private Sub wShowReject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Enabled = True
        SBtnOK.Enabled = True
        SBtnExit.Enabled = True
        MedReason.Enabled = True
        MedReason.Text = String.Empty

    End Sub

    Private Sub SBtnOK_Click(sender As Object, e As EventArgs) Handles SBtnOK.Click
        _Reason = MedReason.Text
        Me.Close()
    End Sub

    Private Sub sBtnExit_Click(sender As Object, e As EventArgs) Handles SBtnExit.Click
        _Reason = String.Empty
        Me.Close()
    End Sub

    Private Sub MedReason_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MedReason.KeyPress
        If (e.KeyChar = "'") Or (e.KeyChar = "\") Then
            e.Handled = True
        End If
    End Sub
End Class