Public Class wIssueTracking

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Process"
    Private Sub LoadData()
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT        S.FNHSysWHFGId, S.FTOrderNo, S.FTColorway, S.FTSizeBreakDown, S.FNQuantityIss, S.FNQuantitySale, S.FTStyleCode, S.Bal as FNQuantityBal , S.FTWHFGCode"
            _Cmd &= vbCrLf & " ,O.FTPORef "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", S.FTWHFGNameTH as FTWHFGName "
            Else
                _Cmd &= vbCrLf & ", S.FTWHFGNameTH as FTWHFGName"
            End If
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & "]..V_OnhandIssueForSale AS S LEFT OUTER JOIN "
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON S.FTOrderNo = O.FTOrderNo "
            _Cmd &= vbCrLf & "Where S.FTOrderNo <> '' and S.FNQuantityIss > 0"
            If Me.FNHSysWHIdFG.Text <> "" Then
                _Cmd &= vbCrLf & " And S.FTWHFGCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysWHIdFG.Text) & "'"
            End If
            If Me.FNHSysWHIdFGTo.Text <> "" Then
                _Cmd &= vbCrLf & " And S.FTWHFGCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysWHIdFGTo.Text) & "'"
            End If
            If Me.FTOrderNo.Text <> "" Then
                _Cmd &= vbCrLf & " And S.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " And S.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If
            Me.ogcdetail.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_FG)
        Catch ex As Exception
        End Try
    End Sub

    Private Function VerrifyData() As Boolean
        Try
            Dim _State As Boolean = False
            Dim _FieldValidate As String = "FNHSysWHIdFG|FNHSysWHIdFGTo|FTOrderNo|FTOrderNoTo"
            For Each _FieldName As String In _FieldValidate.Split("|")

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case HI.ENM.Control.GeTypeControl(Obj)
                        Case ENM.Control.ControlType.ButtonEdit
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                If .Text <> "" Then
                                    _State = True
                                End If
                            End With
                        Case ENM.Control.ControlType.DateEdit
                            Try
                                With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                    If .Text <> "" Then
                                        _State = True
                                    End If
                                End With
                            Catch ex As Exception
                            End Try
                        Case Else
                    End Select
                Next
            Next
            If Not (_State) Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                Me.FNHSysWHIdFG.Focus()
            End If
            Return _State
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "behavior"

#End Region

#Region "Command Button"
    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If VerrifyData() Then
                Call LoadData()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub
#End Region

End Class