Public Class wCMPriceStyleBySeason


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

        _Qry = "  Select O.FNHSysStyleId,O.FNHSysSeasonId"
        _Qry &= vbCrLf & "  ,SEA.FTSeasonCode"
        _Qry &= vbCrLf & "   ,ISNULL(STP.FNCM,ISNULL(ST.FNCM,0) ) AS FNCM"
        _Qry &= vbCrLf & "  ,ISNULL(ST.FNCMDisPer,0 ) AS FNCMDisPer"
        _Qry &= vbCrLf & "   ,ISNULL(STP.FNCMDisAmt,ST.FNCMDisAmt ) AS FNCMDisAmt"
        _Qry &= vbCrLf & "   ,ISNULL(STP.FNNetCM,ISNULL(ST.FNNetCM,0) ) AS FNNetCM "
        _Qry &= vbCrLf & "  , CASE WHEN ISNULL(STP.FNHSysStyleId,0) =0  THEN '1' ELSE '0' END AS FTState "
        _Qry &= vbCrLf & "  FROM  ( "
        _Qry &= vbCrLf & " SELECT DISTINCT FNHSysStyleId,FNHSysSeasonId"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE  O.FNHSysStyleId=" & Val(Key) & ""
        _Qry &= vbCrLf & "  UNION "
        _Qry &= vbCrLf & "  SELECT FNHSysStyleId,FNHSysSeasonId "
        _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTSeasonCMPrice "
        _Qry &= vbCrLf & "  WHERE FNHSysStyleId=" & Val(Key) & ""
        _Qry &= vbCrLf & " ) AS O INNER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS ST WITH(NOLOCK) ON O.FNHSysStyleId = ST.FNHSysStyleId "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTSeasonCMPrice AS STP WITH(NOLOCK) ON O.FNHSysStyleId = STP.FNHSysStyleId AND O.FNHSysSeasonId = STP.FNHSysSeasonId "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SEA WITH(NOLOCK) ON O.FNHSysSeasonId = SEA.FNHSysSeasonId"
        _Qry &= vbCrLf & "  WHERE  O.FNHSysStyleId=" & Val(Key) & ""
        _Qry &= vbCrLf & "  ORDER BY SEA.FTSeasonCode"

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
                Dim _FNHSysSeasonId As String = ""

                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try

                    For Each R As DataRow In .Rows

                        If _FNHSysSeasonId = "" Then
                            _FNHSysSeasonId = R!FNHSysSeasonId.ToString
                        Else
                            _FNHSysSeasonId = _FNHSysSeasonId & "," & R!FNHSysSeasonId.ToString
                        End If

                        _Qry = "UPDATE A SET  "
                        _Qry &= vbCrLf & "   FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & ",FNCM=" & Val(R!FNCM.ToString) & ""
                        _Qry &= vbCrLf & ",FNCMDisPer=" & Val(R!FNCMDisPer.ToString) & ""
                        _Qry &= vbCrLf & ",FNCMDisAmt=" & Val(R!FNCMDisAmt.ToString) & ""
                        _Qry &= vbCrLf & ",FNNetCM=" & (Val(R!FNCM.ToString) - Val(R!FNCMDisAmt.ToString)) & ""
                        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTSeasonCMPrice AS A "
                        _Qry &= vbCrLf & "  WHERE FNHSysStyleId =" & Val(FNHSysStyleId.Properties.Tag.ToString) & " "
                        _Qry &= vbCrLf & " AND  FNHSysSeasonId=" & Integer.Parse(Val((R!FNHSysSeasonId.ToString))) & " "

                        If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            _Qry = "Insert INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTSeasonCMPrice"
                            _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime,  FNHSysStyleId, FNHSysSeasonId, FNCM, FNCMDisPer, FNCMDisAmt,FNNetCM)"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                            _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                            _Qry &= vbCrLf & " ," & Val(FNHSysStyleId.Properties.Tag.ToString) & " "
                            _Qry &= vbCrLf & "," & Integer.Parse(Val((R!FNHSysSeasonId.ToString))) & " "
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
                    _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTSeasonCMPrice AS A "
                    _Qry &= vbCrLf & "  WHERE FNHSysStyleId =" & Val(FNHSysStyleId.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & "   AND  NOT(FNHSysSeasonId IN (" & HI.UL.ULF.rpQuoted(_FNHSysSeasonId) & ")) "

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

            If HI.MER.ValidateExportXML.CheckExportMIChangeCMP(Val(FNHSysStyleId.Properties.Tag.ToString), Val(ogvdetail.GetFocusedRowCellValue("FNHSysSeasonId").ToString)) = True Then
                e.Cancel = True
            Else
                If e.NewValue < 0 Then
                    e.Cancel = True
                Else
                    Dim _NetCMP As Double = e.NewValue - Val("" & ogvdetail.GetFocusedRowCellValue("FNCMDisAmt"))
                    Me.ogvdetail.SetFocusedRowCellValue("FNNetCM", _NetCMP)

                    CType(Me.ogcdetail.DataSource, DataTable).AcceptChanges()
                End If
            End If
           
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepCMP_Spin(sender As Object, e As DevExpress.XtraEditors.Controls.SpinEventArgs) Handles RepCMP.Spin
        e.Handled = False
    End Sub

    Private Sub ogvdetail_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvdetail.RowCellStyle
        Try
            With ogvdetail

                If "" & .GetRowCellValue(e.RowHandle, "FTState").ToString = "1" Then
                    e.Appearance.ForeColor = Drawing.Color.Blue
                End If

            End With

        Catch ex As Exception

        End Try
    End Sub
End Class