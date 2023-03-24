Public Class wOperationSewingDetailLineMachine


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ReposAssetCode.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains
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


    Private Sub LoadMachine(ByVal unitsectid As Integer)

        Dim dt As DataTable
        Dim _Qry As String

        _Qry = "  Select   A.FNHSysFixedAssetId, B.FTAssetCode, B.FTAssetNameEN As FTAssetName "
        _Qry &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMConfigEfkaMachineSerial As A WITH(NOLOCK) INNER Join "
        _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset As B WITH(NOLOCK) On A.FNHSysFixedAssetId = B.FNHSysFixedAssetId "
        _Qry &= vbCrLf & " WHERE  A.FNHSysUnitSectId=" & unitsectid & " "
        _Qry &= vbCrLf & " GROUP BY A.FNHSysFixedAssetId, B.FTAssetCode, B.FTAssetNameEN "
        _Qry &= vbCrLf & " ORDER BY B.FTAssetCode "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Me.ReposAssetCode.DataSource = dt.Copy
        dt.Dispose()

    End Sub

#Region "Procedure"

    Public Sub LoadDataInfo(ByVal unitsectid As Integer, ByVal styletid As Integer)

        If unitsectid = 0 Or styletid = 0 Then
            Me.ogcoperation.DataSource = Nothing
            Exit Sub
        End If


        Dim _Qry As String = ""
        _Qry = " Select     SOP.FNSeq"
        _Qry &= vbCrLf & "  , SOP.FTOperationName  "
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNSam,0) As FNSam"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNStartSewingfoot,0) As FNStartSewingfoot"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNEndSewingfoot,0) As FNEndSewingfoot"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNStartStitches,0) As FNStartStitches"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNEndStitches,0) As FNEndStitches"
        _Qry &= vbCrLf & "  , ISNULL(X.FNHSysFixedAssetId,0) As FNHSysFixedAssetId"
        _Qry &= vbCrLf & "  , ISNULL(X.FTAssetCode,'') AS FTAssetCode"
        _Qry &= vbCrLf & "  , ISNULL(X.FTAssetName,'') AS FTAssetName"

        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationSeawingDetailByStyle AS SOP WITH (NOLOCK)  "


        _Qry &= vbCrLf & " OUTER APPLY  (SELECT  X.FNHSysFixedAssetId ,AST.FTAssetCode,AST.FTAssetNameEN AS FTAssetName "
        _Qry &= vbCrLf & "    FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationSeawingDetailLineMachine As X With (NOLOCK)  "
        _Qry &= vbCrLf & "    INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset As AST With (NOLOCK)  ON X.FNHSysFixedAssetId =AST.FNHSysFixedAssetId "

        _Qry &= vbCrLf & "  WHERE X.FNHSysUnitSectId=" & unitsectid & " "
        _Qry &= vbCrLf & "  AND X.FNHSysStyleId=" & styletid & " "
        _Qry &= vbCrLf & "  AND X.FNSeq = SOP.FNSeq "
        _Qry &= vbCrLf & "  ) As X "

        _Qry &= vbCrLf & "   WHERE SOP.FNHSysStyleId=" & styletid & " "
        _Qry &= vbCrLf & "  ORDER BY SOP.FNSeq ASC "

        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Me.ogcoperation.DataSource = _dt

    End Sub

#End Region

#Region "General"
    Private Sub FNHSysStyleId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysStyleId.EditValueChanged
        If Me.InvokeRequired Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysStyleId_EditValueChanged), New Object() {sender, e})
        Else
            If FNHSysStyleId.Text <> "" Then

                Dim _Qry As String = "Select TOP 1 FNHSysStyleId  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As MS With (NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
                FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")
                Me.ogcoperation.DataSource = Nothing

                Call LoadDataInfo(Val(FNHSysUnitSectId.Properties.Tag.ToString), Val(FNHSysStyleId.Properties.Tag.ToString))

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

            Case System.Windows.Forms.Keys.Delete


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
                Dim _Qry As String = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationSeawingDetailLineMachine WHERE FNHSysStyleId =" & Val(FNHSysStyleId.Properties.Tag.ToString) & " AND  FNHSysUnitSectId =" & Val(FNHSysUnitSectId.Properties.Tag.ToString) & " "
                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    End If

                    For Each R As DataRow In .Select("FTOperationName<> '' ", "FNSeq")

                        _FNSeq = _FNSeq + 1

                        _Qry = "Insert INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODMOperationSeawingDetailLineMachine"
                        _Qry &= vbCrLf & "(FTInsUser, FDInsDate, FTInsTime,  FNHSysStyleId, FNSeq, FTOperationName, FNSam, FNStartSewingfoot, FNEndSewingfoot,FNStartStitches,FNEndStitches,FNHSysUnitSectId,FDDate,FNHSysFixedAssetId)"
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
                        _Qry &= vbCrLf & " ," & Val(FNHSysUnitSectId.Properties.Tag.ToString) & " "
                        _Qry &= vbCrLf & " ,''"
                        _Qry &= vbCrLf & " ," & Val(R!FNHSysFixedAssetId.ToString) & " "

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

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In ogvoperation.Columns
            col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        Next
    End Sub

#End Region

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click

        Me.ogcoperation.DataSource = Nothing

        Call LoadDataInfo(Val(FNHSysUnitSectId.Properties.Tag.ToString), Val(FNHSysStyleId.Properties.Tag.ToString))
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub ogcoperation_Click(sender As Object, e As EventArgs) Handles ogcoperation.Click
    End Sub

    Private Sub FNHSysUnitSectId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysUnitSectId.EditValueChanged
        If Me.InvokeRequired Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysStyleId_EditValueChanged), New Object() {sender, e})
        Else
            If FNHSysUnitSectId.Text <> "" Then

                Dim _Qry As String = "SELECT TOP 1 FNHSysUnitSectId  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS MS WITH (NOLOCK) WHERE FTUnitSectCode ='" & HI.UL.ULF.rpQuoted(FNHSysUnitSectId.Text) & "' "
                FNHSysUnitSectId.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")
                Me.ogcoperation.DataSource = Nothing

                Call LoadMachine(Val(FNHSysUnitSectId.Properties.Tag.ToString))
                Call LoadDataInfo(Val(FNHSysUnitSectId.Properties.Tag.ToString), Val(FNHSysStyleId.Properties.Tag.ToString))

            Else
                Me.ogcoperation.DataSource = Nothing
            End If
        End If
    End Sub

    Private Sub ReposAssetCode_EditValueChanged(sender As Object, e As EventArgs) Handles ReposAssetCode.EditValueChanged
        Try

            With Me.ogvoperation
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.LookUpEdit = DirectCast(sender, DevExpress.XtraEditors.LookUpEdit)
                .SetFocusedRowCellValue("FTAssetName", obj.GetColumnValue("FTAssetName").ToString)
                .SetFocusedRowCellValue("FNHSysFixedAssetId", obj.GetColumnValue("FNHSysFixedAssetId").ToString)

            End With

            CType(Me.ogcoperation.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception

        End Try
    End Sub
End Class