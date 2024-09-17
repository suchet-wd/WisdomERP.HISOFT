Imports System.Data.SqlClient

Public Class wCreateBomDevByBomOriginal

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(ByVal value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private Sub ocmcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmcancel.Click
        ClearForm()
        Me.ProcComplete = False
        Me.Close()
    End Sub


    Private Sub ocmok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmok.Click
        If Verify() Then
            Me.ProcComplete = True
            Me.Close()

        End If
    End Sub

    Private Function Verify() As Boolean
        If (FNHSysStyleDevId.Text = "") Then
            FNHSysStyleDevId.Focus()
            HI.MG.ShowMsg.mProcessError(1411200101, "กรุณาใส่ข้อมูล Style ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub ocmClear_Click(sender As Object, e As EventArgs) Handles ocmClear.Click
        ClearForm()
    End Sub

    Private Function ClearForm()
        FNHSysStyleDevId.Text = ""
        FTBomDevStyleCode.Text = ""
        FNHSysStyleDevId_None.Text = ""
    End Function

End Class