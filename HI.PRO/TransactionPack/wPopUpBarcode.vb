

Public Class wPopUpBarcode
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
            If Me.FNCartonNoBegin.Value > Me.FNQtyCarton.Value Then
                HI.MG.ShowMsg.mInfo("Pls Check Carton No. !!!!", 1601061620, Me.Text)
                Exit Sub
            End If
            _Poss = True
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNQtyCarton_EditValueChanged(sender As Object, e As EventArgs) 'Handles FNQtyCarton.EditValueChanged, FNCartonNoBegin.EditValueChanged
        Try
            If Me.FNQtyCarton.Value > _QtyCarton Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถใส่จำนวนกล่องเกินใบแพ็คได้!!!!!", 1512141632, Me.Text, "", System.Windows.Forms.MessageBoxIcon.Stop)
                Me.FNQtyCarton.Value = _QtyCarton
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class