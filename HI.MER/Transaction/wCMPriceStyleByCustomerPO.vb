Public Class wCMPriceStyleByCustomerPO


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

#End Region

#Region "Procedure"

    Public Sub LoadCodeBySysIDInfo(ByVal Key As Object)

        Dim _Qry As String = "SELECT TOP 1 FTStyleCode  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS MS WITH (NOLOCK) WHERE FNHSysStyleId  =" & Val(Key.ToString) & " "
        FNHSysStyleId.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")

    End Sub

    Public Sub LoadDataInfo(ByVal Key As Object)

        Dim _Qry As String = ""

        _Qry = "  Select A.FNHSysStyleId"
        _Qry &= vbCrLf & "  ,A.FTPOref"
        _Qry &= vbCrLf & "   ,ISNULL(STP.FNCM,ISNULL(ST.FNCM,0) ) AS FNCM"
        _Qry &= vbCrLf & "  ,ISNULL(STP.FNCMDisPer,ST.FNCMDisPer ) AS FNCMDisPer"
        _Qry &= vbCrLf & "   ,ISNULL(STP.FNCMDisAmt,ST.FNCMDisAmt ) AS FNCMDisAmt"
        _Qry &= vbCrLf & "   ,ISNULL(STP.FNNetCM,ISNULL(ST.FNNetCM,0) ) AS FNNetCM"
        _Qry &= vbCrLf & "  FROM ( SELECT O.FNHSysStyleId, S.FTPOref"
        _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination AS S ON O.FTOrderNo = S.FTOrderNo"
        _Qry &= vbCrLf & "  GROUP BY O.FNHSysStyleId, S.FTPOref) AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH(NOLOCK) ON A.FNHSysStyleId = ST.FNHSysStyleId "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTCMPrice AS STP WITH(NOLOCK) ON A.FNHSysStyleId = STP.FNHSysStyleId AND A.FTPOref = STP.FTCustomerPO "
        _Qry &= vbCrLf & "  WHERE  A.FNHSysStyleId=" & Val(Key) & ""
        _Qry &= vbCrLf & "  ORDER BY FTPOref"

        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Me.ogcdetail.DataSource = _dt

    End Sub
#End Region

#Region "General"
    Private Sub FNHSysStyleId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysStyleId.EditValueChanged
        If Me.InvokeRequired Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysStyleId_EditValueChanged), New Object() {sender, e})
        Else
            If FNHSysStyleId.Text <> "" Then

                Dim _Qry As String = "SELECT TOP 1 FNHSysStyleId  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS MS WITH (NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
                FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")
                Me.ogcdetail.DataSource = Nothing

                Call LoadDataInfo(FNHSysStyleId.Properties.Tag.ToString)

            Else
                Me.ogcdetail.DataSource = Nothing
            End If
        End If
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        CType(Me.ogcdetail.DataSource, DataTable).AcceptChanges()
        If FNHSysStyleId.Properties.Tag.ToString <> "" And FNHSysStyleId.Text <> "" Then
            With CType(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()

                Dim _FNSeq As Integer = 0
                Dim _Spls As New HI.TL.SplashScreen("Saving...Data Please Wait.", Me.Text)
                Dim _Qry As String = ""
                Dim _FTCustomerPO As String = ""

                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try

                    For Each R As DataRow In .Select("FTPOref<>''")

                        If _FTCustomerPO = "" Then
                            _FTCustomerPO = R!FTPOref.ToString
                        Else
                            _FTCustomerPO = _FTCustomerPO & "','" & R!FTPOref.ToString
                        End If

                        _Qry = "UPDATE A SET  "
                        _Qry &= vbCrLf & "   FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & ",FNCM=" & Val(R!FNCM.ToString) & ""
                        _Qry &= vbCrLf & ",FNCMDisPer=" & Val(R!FNCMDisPer.ToString) & ""
                        _Qry &= vbCrLf & ",FNCMDisAmt=" & Val(R!FNCMDisAmt.ToString) & ""
                        _Qry &= vbCrLf & ",FNNetCM=" & (Val(R!FNCM.ToString) - Val(R!FNCMDisAmt.ToString)) & ""
                        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTCMPrice AS A "
                        _Qry &= vbCrLf & "  WHERE FNHSysStyleId =" & Val(FNHSysStyleId.Properties.Tag.ToString) & " "
                        _Qry &= vbCrLf & " AND  FTCustomerPO='" & HI.UL.ULF.rpQuoted(R!FTPOref.ToString) & "' "

                        If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            _Qry = "Insert INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTCMPrice"
                            _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime,  FNHSysStyleId, FTCustomerPO, FNCM, FNCMDisPer, FNCMDisAmt,FNNetCM)"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                            _Qry &= vbCrLf & " ," & Val(FNHSysStyleId.Properties.Tag.ToString) & " "
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPOref.ToString) & "' "
                            _Qry &= vbCrLf & "," & Val(R!FNCM.ToString) & ""
                            _Qry &= vbCrLf & "," & Val(R!FNCMDisPer.ToString) & ""
                            _Qry &= vbCrLf & "," & Val(R!FNCMDisAmt.ToString) & ""
                            _Qry &= vbCrLf & "," & (Val(R!FNCM.ToString) - Val(R!FNCMDisAmt.ToString)) & ""

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                _Spls.Close()

                                Exit Sub

                            End If
                        End If


                    Next

                    _Qry = "DELETE   A   "
                    _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTCMPrice AS A "
                    _Qry &= vbCrLf & "  WHERE FNHSysStyleId =" & Val(FNHSysStyleId.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & "   AND  NOT(FTCustomerPO IN ('" & HI.UL.ULF.rpQuoted(_FTCustomerPO) & "')) "

                    HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Try
                        Call ocmrefresh_Click(ocmrefresh, New System.EventArgs)
                    Catch ex As Exception
                    End Try

                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Catch ex As Exception
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End Try

            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysStyleId_lbl.Text)
            FNHSysStyleId.Focus()
        End If

    End Sub


    Private Sub wOperationByStyle_Load(sender As Object, e As EventArgs) Handles Me.Load
        ogvdetail.OptionsView.ShowAutoFilterRow = True
        ogvdetail.OptionsSelection.MultiSelect = False
        ogvdetail.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
    End Sub

#End Region

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Dim _Qry As String = "SELECT TOP 1 FNHSysStyleId  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS MS WITH (NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
        FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")
        Me.ogcdetail.DataSource = Nothing

        Call LoadDataInfo(Val(FNHSysStyleId.Properties.Tag.ToString))
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub RepCMP_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepCMP.EditValueChanging
        Try
            If e.NewValue < 0 Then
                e.Cancel = True
            Else
                Dim _NetCMP As Double = e.NewValue - Val("" & ogvdetail.GetFocusedRowCellValue("FNCMDisAmt"))
                Me.ogvdetail.SetFocusedRowCellValue("FNNetCM", _NetCMP)

                CType(Me.ogcdetail.DataSource, DataTable).AcceptChanges()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepCMP_Spin(sender As Object, e As DevExpress.XtraEditors.Controls.SpinEventArgs) Handles RepCMP.Spin
        e.Handled = False
    End Sub
End Class