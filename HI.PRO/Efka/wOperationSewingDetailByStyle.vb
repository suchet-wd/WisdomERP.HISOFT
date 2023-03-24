Public Class wOperationSewingDetailByStyle


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
        _Qry = " SELECT     SOP.FNSeq"
        _Qry &= vbCrLf & "  , SOP.FTOperationName  "
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNSam,0) AS FNSam"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNStartSewingfoot,0) AS FNStartSewingfoot"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNEndSewingfoot,0) AS FNEndSewingfoot"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNStartStitches,0) AS FNStartStitches"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNEndStitches,0) AS FNEndStitches"

        _Qry &= vbCrLf & "  , ISNULL(SOP.FNStartBetweenTime,0) AS FNStartBetweenTime"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNEndBetweenTime,0) AS FNEndBetweenTime"

        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationSeawingDetailByStyle AS SOP WITH (NOLOCK)  "
        _Qry &= vbCrLf & "   WHERE SOP.FNHSysStyleId=" & Val(Key.ToString) & " "
        _Qry &= vbCrLf & "  ORDER BY SOP.FNSeq ASC"
        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Me.ogcoperation.DataSource = _dt

        Call InitNewRow()
    End Sub


    Private Sub InitNewRow()

        With CType(Me.ogcoperation.DataSource, DataTable)
            .Rows.Add()
            .Rows(.Rows.Count - 1)!FNSeq = .Rows.Count
            .Rows(.Rows.Count - 1)!FTOperationName = ""
            .Rows(.Rows.Count - 1)!FNSam = 0
            .Rows(.Rows.Count - 1)!FNStartSewingfoot = 0
            .Rows(.Rows.Count - 1)!FNEndSewingfoot = 0
            .Rows(.Rows.Count - 1)!FNStartStitches = 0
            .Rows(.Rows.Count - 1)!FNEndStitches = 0
            .Rows(.Rows.Count - 1)!FNStartBetweenTime = 0
            .Rows(.Rows.Count - 1)!FNEndBetweenTime = 0
            .AcceptChanges()

        End With

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
                Me.ogcoperation.DataSource = Nothing

                Call LoadDataInfo(FNHSysStyleId.Properties.Tag.ToString)

            Else
                Me.ogcoperation.DataSource = Nothing
            End If
        End If
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ogvoperation_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvoperation.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Down
                With CType(Me.ogcoperation.DataSource, DataTable)

                    .AcceptChanges()

                    If .Select("FTOperationName=''").Length <= 0 Then

                        Call InitNewRow()

                        .AcceptChanges()

                        Me.ogvoperation.ClearSelection()
                        Me.ogvoperation.SelectRow(.Rows.Count - 1)
                        Me.ogvoperation.FocusedRowHandle = .Rows.Count - 1
                        Me.ogvoperation.FocusedColumn = Me.ogvoperation.Columns.ColumnByFieldName("FTOperationName")

                    End If

                End With
            Case System.Windows.Forms.Keys.Delete

                Me.ogvoperation.DeleteRow(Me.ogvoperation.FocusedRowHandle)
                    With CType(Me.ogcoperation.DataSource, DataTable)
                        .AcceptChanges()
                        Dim _RIndx As Integer = 0
                        For Each R In .Rows
                            _RIndx = _RIndx + 1
                            R!FNSeq = _RIndx
                        Next
                        .AcceptChanges()
                    End With


            Case System.Windows.Forms.Keys.Enter

        End Select
    End Sub


    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        'If Not (CheckUseOperation(Integer.Parse(Me.FNHSysStyleId.Properties.Tag))) Then
        '    HI.MG.ShowMsg.mInfo("มีการใช้ขั้นตอน แบบผลิตภัณฑ์ นี้แล้ว กรุณาตรวจสอบ !!! ", 1611091553, Me.Text)
        '    Exit Sub
        'End If
        CType(Me.ogcoperation.DataSource, DataTable).AcceptChanges()
        If FNHSysStyleId.Properties.Tag.ToString <> "" And FNHSysStyleId.Text <> "" Then
            With CType(Me.ogcoperation.DataSource, DataTable)
                .AcceptChanges()

                Dim _FNSeq As Integer = 0
                Dim _Spls As New HI.TL.SplashScreen("Saving...Data Please Wait.", Me.Text)
                Dim _Qry As String = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationSeawingDetailByStyle WHERE FNHSysStyleId =" & Val(FNHSysStyleId.Properties.Tag.ToString) & " "
                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    End If

                    For Each R As DataRow In .Select("FTOperationName<> '' ", "FNSeq")

                        _FNSeq = _FNSeq + 1

                        _Qry = "Insert INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationSeawingDetailByStyle"
                        _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime,  FNHSysStyleId, FNSeq, FTOperationName, FNSam, FNStartSewingfoot, FNEndSewingfoot,FNStartStitches,FNEndStitches,FNHSysCmpId,FNStartBetweenTime,FNEndBetweenTime)"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & " ," & Val(FNHSysStyleId.Properties.Tag.ToString) & " "
                        _Qry &= vbCrLf & " ," & _FNSeq & " "
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTOperationName.ToString) & "'"
                        _Qry &= vbCrLf & " ," & Val(R!FNSam.ToString) & " "
                        _Qry &= vbCrLf & " ," & Val(R!FNStartSewingfoot.ToString) & " "
                        _Qry &= vbCrLf & " ," & Val(R!FNEndSewingfoot.ToString) & " "
                        _Qry &= vbCrLf & " ," & Val(R!FNStartStitches.ToString) & " "
                        _Qry &= vbCrLf & " ," & Val(R!FNEndStitches.ToString) & " "
                        _Qry &= vbCrLf & " ," & HI.ST.SysInfo.CmpID & " "
                        _Qry &= vbCrLf & " ," & Val(R!FNStartBetweenTime.ToString) & " "
                        _Qry &= vbCrLf & " ," & Val(R!FNEndBetweenTime.ToString) & " "

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            _Spls.Close()

                            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                            Exit Sub

                        End If

                    Next

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
        ogvoperation.OptionsView.ShowAutoFilterRow = False
        ogvoperation.OptionsSelection.MultiSelect = False
        ogvoperation.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
    End Sub

#End Region


    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Dim _Qry As String = "SELECT TOP 1 FNHSysStyleId  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS MS WITH (NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
        FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")
        Me.ogcoperation.DataSource = Nothing

        Call LoadDataInfo(Val(FNHSysStyleId.Properties.Tag.ToString))
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub ogcoperation_Click(sender As Object, e As EventArgs) Handles ogcoperation.Click

    End Sub
End Class