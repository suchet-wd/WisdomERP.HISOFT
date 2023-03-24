Public Class wConfigColor

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Procedure"
    Public Sub LoadDataInfo()

        Dim _Qry As String = ""
        _Qry = " SELECT A.FNHSysCmpRunId"
        _Qry &= vbCrLf & " , A.FTCmpRunCode"
        _Qry &= vbCrLf & "  , 'A' + Convert(nvarchar(30),ISNULL(B.FNGraphImageIndex,0)) AS FNGraphImageIndex"
        _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmpRun AS A WITH(NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTCmpRunImage AS B WITH(NOLOCK) ON A.FNHSysCmpRunId = B.FNHSysCmpRunId"
        _Qry &= vbCrLf & "  WHERE  (A.FTStateActive = '1')"
        _Qry &= vbCrLf & "  ORDER BY  A.FTCmpRunCode"

        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Me.ogcdetail.DataSource = _dt

    End Sub

    Private Function SaveData(_dt As DataTable) As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Saving....  , Please wait. ")


        Try
            Dim _Qry As String = ""
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PLANNING)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            If Not (_dt Is Nothing) Then
                For Each R As DataRow In _dt.Rows

                    _Qry = " UPDATE A SET "
                    _Qry &= vbCrLf & "  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & " ,FNGraphImageIndex=" & Integer.Parse(Val(R!FNGraphImageIndex.ToString.Replace("A", ""))) & ""
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTCmpRunImage AS A"
                    _Qry &= vbCrLf & " WHERE FNHSysCmpRunId=" & Integer.Parse(Val(R!FNHSysCmpRunId.ToString)) & ""


                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTCmpRunImage"
                        _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime, FNHSysCmpRunId, FNGraphImageIndex)"
                        _Qry &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNHSysCmpRunId.ToString)) & ""
                        _Qry &= vbCrLf & " ," & Integer.Parse(Val(R!FNGraphImageIndex.ToString.Replace("A", ""))) & ""
                
                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            _Spls.Close()
                            Return False
                        End If

                    End If

                Next
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            Return False
        End Try

        _Spls.Close()
        Return True

    End Function

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Me.LoadDataInfo()
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If Not (Me.ogcdetail.DataSource Is Nothing) Then
            Dim _dt As DataTable
            With CType(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy
            End With

            If _dt.Rows.Count > 0 Then
                If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave) = True Then
                    If Me.SaveData(_dt) Then
                        Call LoadDataInfo()
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Else
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub wConfigColor_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call LoadDataInfo()
    End Sub

End Class