Public Class wGenerateStyleNNN


    Private _CopyStyle As wCopyStyle

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _CopyStyle = New wCopyStyle
        HI.TL.HandlerControl.AddHandlerObj(_CopyStyle)
    End Sub


#Region "MAIN PROC"

    Private Sub Proc_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmcopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmcopy.Click

        If Me.FNHSysStyleId.Text <> "" Then
            If "" & Me.FNHSysStyleId.Properties.Tag.ToString <> "" Then
                With _CopyStyle
                    .ProcComplete = False
                    .ShowDialog()

                    If (.ProcComplete) Then

                    End If
                End With

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysStyleId_lbl.Text)
                FNHSysStyleId.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysStyleId_lbl.Text)
            FNHSysStyleId.Focus()
        End If
    End Sub

#End Region


End Class