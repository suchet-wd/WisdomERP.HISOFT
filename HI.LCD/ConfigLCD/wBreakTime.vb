Imports System.Windows.Forms
Imports System.Drawing
Public Class wBreakTime


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


    End Sub

    Private Sub LoadData()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "DECLARE @DynamicPivotQuery AS NVARCHAR(MAX)"
            _Cmd &= vbCrLf & " DECLARE @ColumnName varchar(max)"
            _Cmd &= vbCrLf & "SELECT @ColumnName= ISNULL(@ColumnName + ',','') "
            _Cmd &= vbCrLf & "   + QUOTENAME(FNHSysPeriodOfTimeId)"
            _Cmd &= vbCrLf & "FROM ("
            _Cmd &= vbCrLf & "SELECT  distinct   FNHSysPeriodOfTimeId "
            _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMPeiodOfTime"
            _Cmd &= vbCrLf & ") AS Courses"
            _Cmd &= vbCrLf & "SET @DynamicPivotQuery = "
            _Cmd &= vbCrLf & "     N'SELECT FNHSysUnitSectId, ' + @ColumnName + '"
            _Cmd &= vbCrLf & " INTO #Tmp "
            _Cmd &= vbCrLf & "        FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODLCDConfigTime"
            _Cmd &= vbCrLf & " PIVOT(max(FNConfigTime) FOR   FNHSysPeriodOfTimeId IN (' + @ColumnName + ')) AS PVTTable"
            '_Cmd &= vbCrLf & "Select S.FTUnitSectCode AS FTUnitSectCode , T.* From #Tmp AS T INNER JOIN [HITECH_MASTER]..TCNMUnitSect  AS S ON T.FNHSysUnitSectId = S.FNHSysUnitSectId'"
            _Cmd &= vbCrLf & "Select S.FTUnitSectCode AS FTUnitSectCode ,S.FNHSysUnitSectId AS FNHSysUnitSectIdM, T.* From  "
            _Cmd &= vbCrLf & "[HITECH_MASTER]..TCNMUnitSect  AS S LEFT OUTER JOIN #Tmp AS T"
            _Cmd &= vbCrLf & "ON S.FNHSysUnitSectId = T.FNHSysUnitSectId"
            _Cmd &= vbCrLf & "WHERE S.FTStateSew =''1'''"

            _Cmd &= vbCrLf & "EXEC sp_executesql @DynamicPivotQuery"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            Call CreateColumn(_oDt)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CreateColumn(ByVal _oDt As DataTable)
        Try
            Dim _dt As DataTable = _oDt
            Dim _Qry As String = ""
            Dim _colcount As Integer = 0

            With Me.ogvTime
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case .Columns(I).FieldName.ToString.ToUpper
                        Case "FNHSysUnitSectIdM".ToUpper, "FTUnitSectCode".ToUpper, "FNHSysUnitSectId".ToUpper
                        Case Else
                            .Columns.Remove(.Columns(I))
                    End Select

                Next
                Dim _Cmd As String = ""

                If Not (_dt Is Nothing) Then
                    For Each Col As DataColumn In _dt.Columns

                        Select Case Col.ColumnName.ToString.ToUpper
                            Case "FNHSysUnitSectIdM".ToUpper, "FTUnitSectCode".ToUpper, "FNHSysUnitSectId".ToUpper
                            Case Else
                                _colcount = _colcount + 1
                                Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                                With ColG
                                    .Visible = True
                                    .FieldName = Col.ColumnName.ToString
                                    .Name = Col.ColumnName.ToString

                                    _Cmd = "Select Top 1   FTStartTime +'-'+FTEndTime AS FTTime  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMPeiodOfTime WITH(NOLOCK) Where FNHSysPeriodOfTimeId =" & Integer.Parse(Val(Col.ColumnName.ToString))
                                    .Caption = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                                   
                                End With

                                .Columns.Add(ColG)
                                With .Columns(Col.ColumnName.ToString)

                                    .OptionsFilter.AllowAutoFilter = False
                                    .OptionsFilter.AllowFilter = False
                                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                    .DisplayFormat.FormatString = "{0:n0}"
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                                    _Cmd = "Select Top 1 Isnull(FTStateBreackTime,'0') AS FTStateBreackTime  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMPeiodOfTime WITH(NOLOCK) Where FNHSysPeriodOfTimeId =" & Integer.Parse(Val(Col.ColumnName.ToString))
                                    If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0") = "1" Then
                                        .AppearanceCell.BackColor = Color.DarkGray
                                    End If

                                    With .OptionsColumn
                                        .AllowMove = False
                                        .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                        .AllowEdit = False
                                        .ReadOnly = True
                                    End With

                                End With

                                .Columns(Col.ColumnName.ToString).Width = 80
                                .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                                .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"
                                .OptionsFind.AllowFindPanel = False
                                .OptionsFilter.AllowFilterEditor = False

                              
                        End Select

                    Next

                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                        With GridCol
                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        End With
                    Next

                End If



            End With
        Catch ex As Exception

        End Try


        Me.ogcTime.DataSource = _oDt

       
        _oDt.Dispose()


    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Call LoadData()
    End Sub

    Private Sub ogvTime_DoubleClick(sender As Object, e As EventArgs) Handles ogvTime.DoubleClick, ogvTime.Click
        Try
            With Me.ogvTime
                Dim _Cmd As String = ""
                _Cmd = "Select Top 1 Isnull(FTStateBreackTime,'0') AS FTStateBreackTime  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TPRODMPeiodOfTime WITH(NOLOCK) Where FNHSysPeriodOfTimeId =" & Integer.Parse(Val(.FocusedColumn.Name))
                If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "0") = "0" Then
                    Me.FNHSysPeriodOfTimeId.Text = HI.Conn.SQLConn.GetField("Select Top 1 FTPeriadOfTimeCode From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TPRODMPeiodOfTime WITH(NOLOCK) WHERE FNHSysPeriodOfTimeId = " & Integer.Parse(Val(.FocusedColumn.Name)), Conn.DB.DataBaseName.DB_MASTER)
                    Me.FNHSysUnitSectId.Text = HI.Conn.SQLConn.GetField("Select Top 1 FTUnitSectCode From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect WITH(NOLOCK)  WHERE FNHSysUnitSectId =" & Integer.Parse(Val(.GetRowCellValue(.FocusedRowHandle, "FNHSysUnitSectIdM"))), Conn.DB.DataBaseName.DB_MASTER)
                    Me.FNConfigTime.Value = Integer.Parse(Val("0" & .FocusedValue))
                    Me.FNConfigTime.Focus()
                Else
                    HI.TL.HandlerControl.ClearControl(Me)
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        If Me.VerrifyData() Then
            If Me.SaveData() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Call LoadData()
                HI.TL.HandlerControl.ClearControl(Me)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If

    End Sub

    Private Function SaveData() As Boolean
        Try
            Dim _Cmd As String = ""

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODLCDConfigTime "
            _Cmd &= vbCrLf & "Set FNConfigTime =" & Integer.Parse(Val(Me.FNConfigTime.Value))
            _Cmd &= vbCrLf & ",FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ",FTUpdTime =" & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & "Where FNHSysPeriodOfTimeId =" & Integer.Parse(Val(Me.FNHSysPeriodOfTimeId.Properties.Tag))
            _Cmd &= vbCrLf & " AND FNHSysUnitSectId=" & Integer.Parse(Val(Me.FNHSysUnitSectId.Properties.Tag))
            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODLCDConfigTime (FNHSysPeriodOfTimeId,FNHSysUnitSectId,FNConfigTime)"
                _Cmd &= vbCrLf & "Select " & Me.FNHSysPeriodOfTimeId.Properties.Tag
                _Cmd &= vbCrLf & "," & Me.FNHSysUnitSectId.Properties.Tag
                _Cmd &= vbCrLf & "," & Me.FNConfigTime.Value

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

    Private Function VerrifyData() As Boolean
        Try
            If Me.FNHSysUnitSectId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, FNHSysUnitSectId_lbl.Text)
                Me.FNHSysUnitSectId.Focus()
                Return False
            End If

            If Me.FNHSysPeriodOfTimeId.Properties.Tag.ToString = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, FNHSysPeriodOfTimeId_lbl.Text)
                Me.FNHSysPeriodOfTimeId.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub FNConfigTime_KeyDown(sender As Object, e As KeyEventArgs) Handles FNConfigTime.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Call ocmsave_Click(Me.ocmsave, Nothing)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function DeleteData() As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODLCDConfigTime"
            _Cmd &= vbCrLf & "Where FNHSysPeriodOfTimeId =" & Integer.Parse(Val(Me.FNHSysPeriodOfTimeId.Properties.Tag))
            _Cmd &= vbCrLf & " AND FNHSysUnitSectId=" & Integer.Parse(Val(Me.FNHSysUnitSectId.Properties.Tag))
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
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
    End Function

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If Me.DeleteData Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                Me.LoadData()
                HI.TL.HandlerControl.ClearControl(Me)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub wBreakTime_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.LoadData()
        Catch ex As Exception
        End Try
    End Sub

End Class