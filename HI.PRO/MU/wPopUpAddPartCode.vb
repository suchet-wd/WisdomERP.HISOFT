

Public Class wPopUpAddPartCode
    Private Shared _Reason As String
    ' Private Shared _frmReject As wShowReject = Nothing

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        HI.TL.HandlerControl.AddHandlerObj(Me)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name, Me)
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private _Poss As Boolean = False
    Public Property Poss As Boolean
        Get
            Return _Poss
        End Get
        Set(value As Boolean)
            _Poss = value
        End Set
    End Property

    Private _QtyCarton As Integer = 0
    Public Property QtyCarton As Integer
        Get
            Return _QtyCarton
        End Get
        Set(value As Integer)
            _QtyCarton = value
        End Set
    End Property

    Private Sub SBtnExit_Click(sender As Object, e As EventArgs) Handles SBtnExit.Click
        Try
            _Poss = False
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SBtnOK_Click(sender As Object, e As EventArgs) Handles SBtnOK.Click
        Try
            With DirectCast(Me.ogcpart.DataSource, DataTable)
                .AcceptChanges()
                If .Select("FTSelect='1'").Length <= 0 Then
                    HI.MG.ShowMsg.mInfo("Pls Select Part No. !!!!", 2202281311, Me.Text)
                    Exit Sub
                End If
                .AcceptChanges()
            End With
            _Poss = True
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub


End Class