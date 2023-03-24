Public Class wEditScanQuantityAddOrder

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private _AddNewData As Boolean = False
    Public Property AddNewData As Boolean
        Get
            Return _AddNewData
        End Get
        Set(value As Boolean)
            _AddNewData = value
        End Set
    End Property

    Private Sub ocmCancel_Click(sender As Object, e As EventArgs) Handles ocmCancel.Click

        Me.AddNewData = True
        Me.Close()
    End Sub

    Private Function VerifyData() As Boolean
        Dim _State As Boolean = False
        If Me.FNHSysUnitSectId.Text <> "" Then
            If Me.FTOrderNo.Text <> "" Then
                If Me.FTSubOrderNo.Text <> "" Then

                    _State = True

                Else

                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTSubOrderNo_lbl.Text)

                End If
            Else

                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTOrderNo_lbl.Text)

            End If

        Else

            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysUnitSectId_lbl.Text)

        End If

        Return _State

    End Function

    Private Function AddOrder() As Boolean


        Dim _Cmd As String = ""
        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            _Cmd = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderAddEditScanQuantity "
            _Cmd &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ",FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "
            'If FNStateSewPack.SelectedIndex = 3 Then
            '    _Cmd &= vbCrLf & ",FNHSysPartId,FTSemiPart=" & Val(Me.FNHSysPartId.Properties.Tag)
            '    _Cmd &= vbCrLf & ",FTSemiPart='" & Me.FTSemiPart.Text & "'"
            'End If

            _Cmd &= vbCrLf & " WHERE  FNHSysUnitSectId=" & Val(FNHSysUnitSectId.Properties.Tag.ToString) & " "
            _Cmd &= vbCrLf & "        AND FTDate='" & HI.UL.ULDate.ConvertEnDB(FTDate.Text) & "'"
            _Cmd &= vbCrLf & "        AND FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
            _Cmd &= vbCrLf & "        AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "'"
            _Cmd &= vbCrLf & "        AND FNStateSewPack='" & FNStateSewPack.SelectedIndex & "'"
            _Cmd &= vbCrLf & " AND FNHSysPartId=" & Val(Me.FNHSysPartId.Properties.Tag)
            _Cmd &= vbCrLf & " AND FTSemiPart='" & Me.FTSemiPart.Text & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                _Cmd = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderAddEditScanQuantity "
                _Cmd &= vbCrLf & "  ("
                _Cmd &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FNHSysUnitSectId, FTDate, FTOrderNo, FTSubOrderNo, FNHSysCmpId,FNStateSewPack,FNHSysPartId,FTSemiPart "
                _Cmd &= vbCrLf & " ) "
                _Cmd &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB & ""
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Cmd &= vbCrLf & "," & Val(FNHSysUnitSectId.Properties.Tag.ToString) & " "
                _Cmd &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(FTDate.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text) & "'"
                _Cmd &= vbCrLf & "," & Val(HI.ST.SysInfo.CmpID) & " "
                _Cmd &= vbCrLf & "," & Val(FNStateSewPack.SelectedIndex) & " "
                Try
                    _Cmd &= vbCrLf & "," & Val(Me.FNHSysPartId.Properties.Tag)
                Catch ex As Exception
                    _Cmd &= vbCrLf & ",0"
                End Try

                _Cmd &= vbCrLf & ",'" & Me.FTSemiPart.Text & "'"



                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Return False
                End If
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return False
        End Try


    End Function

    Private Sub ocmOK_Click(sender As Object, e As EventArgs) Handles ocmOK.Click

        If VerifyData() Then

            If AddOrder() Then

                Me.AddNewData = True
                Me.Close()

            Else

                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

            End If

        End If

    End Sub

    Private Sub FNStateSewPack_EditValueChanged(sender As Object, e As EventArgs) Handles FNStateSewPack.EditValueChanged
        Try

            Me.FNHSysPartId.Enabled = Me.FNStateSewPack.SelectedIndex = 3
            Me.FTSemiPart.Enabled = Me.FNStateSewPack.SelectedIndex = 3
        Catch ex As Exception

        End Try
    End Sub
End Class