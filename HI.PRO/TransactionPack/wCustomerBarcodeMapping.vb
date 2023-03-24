Public Class wCustomerBarcodeMapping 

    Private _MapppingBarcode As New List(Of DataTable)

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


#Region "Procedure"
    Private Sub InitGridBreakdown()
        With ogvsub
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsMenu.EnableColumnMenu = False
            .OptionsMenu.ShowAutoFilterRowItem = False
            .OptionsFilter.AllowFilterEditor = False
            .OptionsFilter.AllowColumnMRUFilterList = False
            .OptionsFilter.AllowMRUFilterList = False
            .OptionsSelection.MultiSelect = True
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        End With
    End Sub

    Private Sub LoadMappingBarcode(OrderNo As Object)
        _MapppingBarcode.Clear()
        Dim _Qry As String = ""
        Dim dt As DataTable

        _Qry = " SELECT  FTOrderNo,FTSubOrderNo, FTColorway, FTSizeBreakDown, FTCustBarcodeNo"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS A WITH(NOLOCK)  "
        _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'  AND ISNULL(FTCustBarcodeNo,'') <>''  "
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        _MapppingBarcode.Add(dt.Copy)
        dt.Dispose()
    End Sub

    Private Sub LoadOrderBreakDown(OrderNo As Object)
        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0
        FTCustBarcodeNo.Text = ""
        FTColorway.Text = ""
        FTSize.Text = ""

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_Get_OrderBreakDownAllSub '" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        With Me.ogvsub
            .BeginInit()
            .Columns("FTColorway").OptionsColumn.AllowFocus = DevExpress.Utils.DefaultBoolean.False

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                        .Columns(I).OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                        .Columns(I).OptionsColumn.AllowShowHide = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next

            If Not (_dt Is Nothing) Then
                For Each Col As DataColumn In _dt.Columns

                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper
                        Case Else
                            _colcount = _colcount + 1
                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                .Visible = True
                                .FieldName = Col.ColumnName.ToString
                                .Name = "FTSubOrderNo" & Col.ColumnName.ToString
                                .Caption = Col.ColumnName.ToString

                            End With

                            .Columns.Add(ColG)

                            With .Columns(Col.ColumnName.ToString)

                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n0}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowShowHide = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = False
                                    .ReadOnly = True

                                    'If Col.ColumnName.ToString.ToUpper = "Total".ToUpper Then
                                    '    .AllowFocus = False
                                    'End If

                                End With

                            End With

                            .Columns(Col.ColumnName.ToString).Width = 45
                            .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"

                    End Select
                Next
            End If
            .EndInit()
        End With

        Me.ogcsub.DataSource = _dt.Copy
        _dt.Dispose()
        Call LoadMappingBarcode(OrderNo)

    End Sub

#End Region
    Private Function ValidateDataScan() As Boolean
        Dim _Qry As String = ""

        _Qry = " SELECT TOP 1 A.FTOrderNo "
        _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScanOutline As A With(NOLOCK) "
        _Qry &= vbCrLf & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle As B With(NOLOCK) On A.FTBarcodeNo = B.FTBarcodeBundleNo "
        _Qry &= vbCrLf & "  WHERE  A.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "' "
        _Qry &= vbCrLf & "  AND  A.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text) & "' "
        _Qry &= vbCrLf & "  AND  B.FTColorway='" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text) & "' "
        _Qry &= vbCrLf & "  AND  B.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Me.FTSize.Text) & "' "

        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
            HI.MG.ShowMsg.mInfo("พบข้อมูลการ Scan ท้ายไลน์แล้ว ไม่สามารถทำการลบหรือแก้ไขได้ !!!!", 1702243649, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
            Return True
        Else
            _Qry = " SELECT TOP 1 A.FTOrderNo "
            _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan As A With(NOLOCK) "
            _Qry &= vbCrLf & "  WHERE  A.FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "' "
            _Qry &= vbCrLf & "  AND  A.FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text) & "' "
            _Qry &= vbCrLf & "  AND  A.FTColorway='" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text) & "' "
            _Qry &= vbCrLf & "  AND  A.FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Me.FTSize.Text) & "' "
            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then
                HI.MG.ShowMsg.mInfo("พบข้อมูลการ Scan ท้ายไลน์แล้ว ไม่สามารถทำการลบหรือแก้ไขได้ !!!!", 1702243649, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
                Return True
            End If
        End If


        Return False
    End Function


#Region "Function"
    Private Function ValidateData(Optional StateSave As Boolean = True) As Boolean
        Dim _State As Boolean = False

        If Me.FTOrderNo.Text <> "" Then
            If Me.FTColorway.Text <> "" And Me.FTSize.Text <> "" Then

                If StateSave Then
                    If Me.FTCustBarcodeNo.Text.Trim <> "" Then


                        Dim _Qry As String = ""

                        _Qry = "Select TOP 1 FTOrderNo "
                        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode With(NOLOCK) "
                        _Qry &= vbCrLf & " WHERE FTCustBarcodeNo='" & HI.UL.ULF.rpQuoted(Me.FTCustBarcodeNo.Text.Trim) & "'"
                        _Qry &= vbCrLf & " AND  FTOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim) & "' "
                        _Qry &= vbCrLf & " AND  FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text.Trim) & "' "
                        _Qry &= vbCrLf & " AND ( FTColorway<>'" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text.Trim) & "'"
                        _Qry &= vbCrLf & " OR FTSizeBreakDown <>'" & HI.UL.ULF.rpQuoted(Me.FTSize.Text.Trim) & "'"
                        _Qry &= vbCrLf & " ) "

                        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then
                            _State = True
                        Else
                            HI.MG.ShowMsg.mInfo("Barcode หมายเชขนี้ มีการผูก กับ สี ไซส์ อื่นแล้ว  !!!", 1405260003, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        End If

                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTCustBarcodeNo_lbl.Text)
                        Me.FTCustBarcodeNo.Focus()
                        Me.FTCustBarcodeNo.SelectAll()
                    End If
                Else
                    _State = True
                End If
            Else
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Color Way And Size Breakdown !!!", 1405260001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTOrderNo_lbl.Text)
            Me.FTOrderNo.Focus()
            Me.FTOrderNo.SelectAll()
        End If

        Return _State
    End Function

    Private Function SaveData() As Boolean
         Dim _Qry As String = ""

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode "
            _Qry &= vbCrLf & " SET FTCustBarcodeNo='" & HI.UL.ULF.rpQuoted(FTCustBarcodeNo.Text.Trim) & "'  "
            _Qry &= vbCrLf & " ,FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
            _Qry &= vbCrLf & "  WHERE  FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim()) & "' "
            _Qry &= vbCrLf & "  AND  FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text.Trim()) & "' "
            _Qry &= vbCrLf & "  AND  FTColorway='" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text) & "' "
            _Qry &= vbCrLf & "  AND  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Me.FTSize.Text) & "' "
       
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode"
                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,  FTOrderNo, FTColorway, FTSizeBreakDown, FTCustBarcodeNo,FTSubOrderNo)"
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text.Trim()) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTSize.Text) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTCustBarcodeNo.Text.Trim) & "'  "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTSubOrderNo.Text.Trim) & "'  "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If


            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, _Qry)

            Try
                Dim _Str As String = "(FTColorway='" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text) & "' AND  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Me.FTSize.Text) & "' AND FTSubOrderNo='') OR (FTColorway='" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text) & "' AND  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Me.FTSize.Text) & "' AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text) & "')"


                If _MapppingBarcode(0).Select(_Str).Length <= 0 Then
                    _MapppingBarcode(0).Rows.Add(Me.FTOrderNo.Text, Me.FTSubOrderNo.Text, Me.FTColorway.Text, Me.FTSize.Text, Me.FTCustBarcodeNo.Text)
                Else
                    For Each R As DataRow In _MapppingBarcode(0).Select(_Str)
                        R!FTCustBarcodeNo = FTCustBarcodeNo.Text

                        Exit For
                    Next
                End If

                CType(Me.ogcsub.DataSource, DataTable).AcceptChanges()

            Catch ex As Exception
            End Try

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Function DeleteData() As Boolean
        Dim _Qry As String = ""

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode "
            _Qry &= vbCrLf & "  WHERE  FTOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "' "
            _Qry &= vbCrLf & "  AND  FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text) & "' "
            _Qry &= vbCrLf & "  AND  FTColorway='" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text) & "' "
            _Qry &= vbCrLf & "  AND  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Me.FTSize.Text) & "' "

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, _Qry)

            Try
                Dim _Str As String = "(FTColorway='" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text) & "' AND  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Me.FTSize.Text) & "' AND FTSubOrderNo='') OR (FTColorway='" & HI.UL.ULF.rpQuoted(Me.FTColorway.Text) & "' AND  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(Me.FTSize.Text) & "' AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.FTSubOrderNo.Text) & "')"

                For Each R As DataRow In _MapppingBarcode(0).Select(_Str)
                    R.Delete()
                    Exit For
                Next

                CType(Me.ogcsub.DataSource, DataTable).AcceptChanges()

            Catch ex As Exception
            End Try

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

#End Region
#Region "General"
    Private Sub FTOrderNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTOrderNo.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTOrderNo_EditValueChanged), New Object() {sender, e})
        Else
            Call LoadOrderBreakDown(FTOrderNo.Text)
        End If
    End Sub

    Private Sub wCustomerBarcodeMapping_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Call InitGridBreakdown()
        Me.ogvsub.Columns("FTColorway").OptionsColumn.AllowFocus = DevExpress.Utils.DefaultBoolean.False

        FTCustBarcodeNo.EnterMoveNextControl = False
    End Sub

    'Private Sub ogvsub_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvsub.KeyDown
    '    With Me.ogvsub
    '        If .FocusedRowHandle < 0 Then Exit Sub
    '        If .FocusedColumn.FieldName.ToUpper = "FTColorway".ToUpper Then Exit Sub
    '        If .FocusedColumn.FieldName.ToUpper = "Total".ToUpper Then Exit Sub

    '        Select Case e.KeyCode
    '            Case System.Windows.Forms.Keys.Delete
    '                If Me.ocmdeletecustbarcode.Enabled Then
    '                    Call ocmdeletecustbarcode_Click(ocmsavecustbarcode, New System.EventArgs)
    '                End If
    '        End Select
    '    End With
    'End Sub

    Private Sub ogvsub_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvsub.RowCellStyle

        Try
            Select Case e.Column.FieldName.ToUpper
                Case "Total".ToUpper, "FTColorway".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper
                Case Else
                    Try
                        If _MapppingBarcode.Count > 0 Then

                            Dim _Str As String = "(FTColorway='" & HI.UL.ULF.rpQuoted(Me.ogvsub.GetRowCellValue(e.RowHandle, "FTColorway")) & "' AND  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(e.Column.Caption.ToString.Trim()) & "' AND FTSubOrderNo='') OR (FTColorway='" & HI.UL.ULF.rpQuoted(Me.ogvsub.GetRowCellValue(e.RowHandle, "FTColorway")) & "' AND  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(e.Column.Caption.ToString.Trim()) & "' AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.ogvsub.GetRowCellValue(e.RowHandle, "FTSubOrderNo")) & "')"
                            If _MapppingBarcode(0).Select(_Str).Length > 0 Then
                                e.Appearance.BackColor = Drawing.Color.LightCyan
                                e.Appearance.ForeColor = Drawing.Color.Blue
                            End If
                        End If
                    Catch ex As Exception

                    End Try

            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvsub_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles ogvsub.SelectionChanged

        FTSubOrderNo.Text = ""
        FTCustBarcodeNo.Text = ""
        FTColorway.Text = ""
        FTSize.Text = ""

        With Me.ogvsub
            If .FocusedRowHandle < 0 Then Exit Sub
            Try
                Select Case .FocusedColumn.FieldName.ToUpper
                    Case "Total".ToUpper, "FTColorway".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper
                    Case Else
                        Try

                            FTSubOrderNo.Text = Me.ogvsub.GetRowCellValue(.FocusedRowHandle, "FTSubOrderNo")
                            FTColorway.Text = Me.ogvsub.GetRowCellValue(.FocusedRowHandle, "FTColorway")
                            FTSize.Text = .FocusedColumn.Caption.ToString.Trim()

                            If _MapppingBarcode.Count > 0 Then

                                Dim _Str As String = "(FTColorway='" & HI.UL.ULF.rpQuoted(Me.ogvsub.GetRowCellValue(.FocusedRowHandle, "FTColorway")) & "' AND  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(.FocusedColumn.Caption.ToString.Trim()) & "' AND FTSubOrderNo='') OR (FTColorway='" & HI.UL.ULF.rpQuoted(Me.ogvsub.GetRowCellValue(.FocusedRowHandle, "FTColorway")) & "' AND  FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(.FocusedColumn.Caption.ToString.Trim()) & "' AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(Me.ogvsub.GetRowCellValue(.FocusedRowHandle, "FTSubOrderNo")) & "')"

                                For Each R As DataRow In _MapppingBarcode(0).Select(_Str)
                                    FTCustBarcodeNo.Text = R!FTCustBarcodeNo.ToString
                                    Exit For
                                Next

                            End If

                            FTCustBarcodeNo.Focus()
                            FTCustBarcodeNo.SelectAll()

                        Catch ex As Exception
                        End Try
                End Select
            Catch ex As Exception
            End Try
        End With

    End Sub

    Private Sub FTCustBarcodeNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FTCustBarcodeNo.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Enter
                If Me.ocmsavecustbarcode.Enabled Then
                    Call ocmsavecustbarcode_Click(ocmsavecustbarcode, New System.EventArgs)
                End If
            Case System.Windows.Forms.Keys.Delete
                If Me.ocmdeletecustbarcode.Enabled Then
                    Call ocmdeletecustbarcode_Click(ocmsavecustbarcode, New System.EventArgs)

                End If
        End Select
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmsavecustbarcode_Click(sender As Object, e As EventArgs) Handles ocmsavecustbarcode.Click
        If Me.ocmsavecustbarcode.Enabled Then
            If Me.ValidateData Then
                'If ValidateDataScan() = True Then
                '    Exit Sub
                'End If

                If Me.SaveData Then
                    With Me.ogvsub
                        Dim _RowInd As Integer = .FocusedRowHandle
                        Dim _ColInd As Integer = .FocusedColumn.VisibleIndex

                        If .FocusedRowHandle < .RowCount Then

                            Try

                                If .Columns(_ColInd + 2).FieldName.ToUpper = "Total".ToUpper Then
                                    _RowInd = _RowInd + 1
                                    _ColInd = 3
                                Else
                                    _ColInd = _ColInd + 2
                                End If

                            Catch ex As Exception
                            End Try

                        Else

                            Try

                                If .Columns(_ColInd + 2).FieldName.ToUpper = "Total".ToUpper Then
                                Else
                                    _ColInd = _ColInd + 2
                                End If

                            Catch ex As Exception
                            End Try

                        End If

                        ' ogcsub.ForceInitialize()
                        ogcsub.Select()

                        .FocusedRowHandle = _RowInd
                        .FocusedColumn = .Columns(_ColInd)
                        .SelectCell(.FocusedRowHandle, .FocusedColumn)

                        Me.FTCustBarcodeNo.Focus()
                        Me.FTCustBarcodeNo.SelectAll()

                    End With
                End If
            End If
        End If
    End Sub
#End Region

    Private Sub ocmdeletecustbarcode_Click(sender As Object, e As EventArgs) Handles ocmdeletecustbarcode.Click
        If Me.ocmdeletecustbarcode.Enabled Then
            If Me.ValidateData(False) Then

                If ValidateDataScan() = True Then
                    Exit Sub
                End If
                If Me.DeleteData Then
                    With Me.ogvsub
                        Dim _RowInd As Integer = .FocusedRowHandle
                        Dim _ColInd As Integer = .FocusedColumn.VisibleIndex

                        If .FocusedRowHandle < .RowCount Then
                            Try
                                If .Columns(_ColInd + 2).FieldName.ToUpper = "Total".ToUpper Then
                                    _RowInd = _RowInd + 1
                                    _ColInd = 2
                                Else
                                    _ColInd = _ColInd + 2
                                End If
                            Catch ex As Exception
                            End Try

                        Else

                            Try
                                If .Columns(_ColInd + 2).FieldName.ToUpper = "Total".ToUpper Then
                                Else
                                    _ColInd = _ColInd + 2
                                End If
                            Catch ex As Exception
                            End Try

                        End If

                        ogcsub.Select()

                        .FocusedRowHandle = _RowInd
                        .FocusedColumn = .Columns(_ColInd)
                        .SelectCell(.FocusedRowHandle, .FocusedColumn)

                        Me.FTCustBarcodeNo.Focus()
                        Me.FTCustBarcodeNo.SelectAll()

                    End With
                End If
            End If
            End If

    End Sub

    Private Sub FTCustBarcodeNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTCustBarcodeNo.EditValueChanged
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        FTOrderNo.Focus()
        FTOrderNo.SelectAll()
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Call LoadOrderBreakDown(FTOrderNo.Text)
    End Sub

    Private Sub ocmgeneratebarcodewip_Click(sender As Object, e As EventArgs) Handles ocmgeneratebarcodewip.Click
        If Me.FTOrderNo.Text <> "" Then
            Dim _Qry As String = ""

            _Qry = " SELECT  TOP 1 FTOrderNo"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrder_CustBarcode AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & "    WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then
                Dim _wAutogen As New wAutoGenerateBarcodeCust
                HI.TL.HandlerControl.AddHandlerObj(_wAutogen)
                Dim oSysLang As New ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wAutogen.Name.ToString.Trim, _wAutogen)
                Catch ex As Exception
                Finally
                End Try

                With _wAutogen
                    .OrderNo = FTOrderNo.Text
                    .StateProc = False
                    .ShowDialog()

                    If (.StateProc) Then
                        Call LoadOrderBreakDown(FTOrderNo.Text)
                    End If

                End With
            Else
                HI.MG.ShowMsg.mInfo("พบหารสร้าง Barcode แล้ว ไม่สามารถทำการ Auto ได้", 1506778945, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)

            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderNo_lbl.Text)
            Me.FTOrderNo.Focus()
        End If
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click

        If Me.FTOrderNo.Text <> "" Then

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Production\"
                .ReportName = "BarCodeCustomerSlip.rpt"
                .Formular = "{TPRODTOrder_CustBarcode.FTOrderNo}='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "' "
                .Preview()
            End With

        Else

            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTOrderNo_lbl.Text)
            Me.FTOrderNo.Focus()

        End If

    End Sub
End Class