Imports System.ComponentModel
Imports System.Windows.Forms

Public Class wPurchaseTrackingPIAddTracking

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' HI.TL.HandlerControl.AddHandlerObj(Me)
    End Sub



    Private _AddMat As Boolean = False
    Public Property AddMat As Boolean
        Get
            Return _AddMat
        End Get
        Set(value As Boolean)
            _AddMat = value
        End Set
    End Property

    Private Sub wAddItemPO_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated

    End Sub

    Private Sub wAddItemPO_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub


    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.AddMat = False
        Me.Close()
    End Sub


    Private Function ValidateData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FTContactName.Text.Trim <> "" Then
            If Me.FTTrackDate.Text <> "" Then
                If Me.FTRemark.Text.Trim <> "" Then
                    _Pass = True
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTRemark_lbl.Text)
                    FTRemark.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTTrackDate_lbl.Text)
                FTTrackDate.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPINo_lbl.Text)
            FTContactName.Focus()
        End If


        Return _Pass
    End Function

    Private Sub ocmadd_Click(sender As System.Object, e As System.EventArgs) Handles ocmadd.Click

        'If Me.FTOrderNo.Properties.ReadOnly = True And FNHSysRawMatId.Properties.Buttons(0).Enabled = False Then
        '    If (CheckReceive(Me.PONO, 0) = False) Then Exit Sub
        'Else
        '    If (CheckReceive(Me.PONO) = False) Then Exit Sub
        'End If

        'If HI.ST.ValidateData.CloseJob(Me.FTOrderNo.Text) Then
        '    HI.MG.ShowMsg.mInfo("บัญชีได้ทำการปิดจ๊อบแล้วไม่สามารถทำรายการใดๆได้อีก !!!", 1502260678, Me.Text, , MessageBoxIcon.Warning)
        '    Exit Sub
        'End If

        If ValidateData() Then
            Me.AddMat = True
            Me.Close()
        End If

    End Sub


    Private Sub FNOrderType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        'Try
        '    Select Case FNOrderType.SelectedIndex
        '        Case 0
        '            'FNHSysRawMatId.Properties.Buttons(0).Tag = "136"
        '            FNHSysRawMatId.Properties.Buttons(0).Tag = "469"

        '            If FNHSysRawMatId.Text <> "" Then
        '                Call HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(FNHSysRawMatId, New System.EventArgs)
        '            End If

        '        Case Else
        '            'FNHSysRawMatId.Properties.Buttons(0).Tag = "106"
        '            FNHSysRawMatId.Properties.Buttons(0).Tag = "469"

        '            If FNHSysRawMatId.Text <> "" Then
        '                Call HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged(FNHSysRawMatId, New System.EventArgs)
        '            End If
        '    End Select
        'Catch ex As Exception

        'End Try

    End Sub



    Private Sub wAddItemPO_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub

    Private Sub FTPurchaseNo_EditValueChanged(sender As Object, e As EventArgs)

    End Sub

End Class