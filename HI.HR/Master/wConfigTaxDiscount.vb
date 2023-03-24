
Public Class wConfigTaxDiscount

    Private Sub FormRefresh()
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Function SaveDataObject(Obj As Object, ByVal sqlcmd As SqlClient.SqlCommand, ByVal Tr As SqlClient.SqlTransaction)
        Dim _Qry As String
        For Each Obj2 As Object In Obj.Controls
            Select Case HI.ENM.Control.GeTypeControl(Obj2)
                Case ENM.Control.ControlType.CalcEdit

                    _Qry = " Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfig  SET  FTKeyValue = '" & CType(Obj2, DevExpress.XtraEditors.CalcEdit).Value.ToString & "' "
                    _Qry &= vbCrLf & " WHERE FTKeyCode=N'" & Obj2.Name.ToString & "'"

                    If HI.Conn.SQLConn.ExecuteTran(_Qry, sqlcmd, Tr) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

            End Select

            If Obj2.Controls.count > 0 Then
                If SaveDataObject(Obj2, sqlcmd, Tr) = False Then
                    Return False
                End If
            End If
        Next

        Return True
    End Function

    Private Function SaveData() As Boolean
        Try
            Dim _Qry As String


            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try
                If SaveDataObject(Me, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) = False Then
                    Return False
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
        Catch ex As Exception

            Return False
        End Try
    End Function

    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim _Dt As DataTable

        _Dt = HI.Conn.SQLConn.GetDataTable(_StrQuery, Conn.DB.DataBaseName.DB_HR)

        For Each R As DataRow In _Dt.Rows
            For Each ctrl As Object In Me.Controls.Find(R!FTKeyCode.ToString.Trim, True)
                Select Case HI.ENM.Control.GeTypeControl(ctrl)
                    Case ENM.Control.ControlType.CalcEdit
                        With CType(ctrl, DevExpress.XtraEditors.CalcEdit)
                            .Value = Val(R!FTKeyValue.ToString)
                        End With
                End Select
            Next
        Next
    End Sub

#Region "MAIN PROC"

    Private Sub ProcessSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click

        If Me.SaveData() Then
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        Else
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        End If

    End Sub

    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.FormRefresh()
    End Sub

    Private Sub ProcessPreview(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

    Private _StrQuery As String = ""
    Private Sub wConfigDiscountTax_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        _StrQuery = " SELECT FTKeyValue,FTKeyCode "
        _StrQuery &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfig "
        _StrQuery &= vbCrLf & " WHERE FTKeyCode <> ''  "
        For Each Obj As Object In Me.Controls
            Select Case HI.ENM.Control.GeTypeControl(Obj)
                Case ENM.Control.ControlType.CalcEdit
                    _StrQuery &= vbCrLf & " AND FTKeyCode=N'" & Obj.Name.ToString & "'"
            End Select
        Next

        Call LoadData()
    End Sub
End Class