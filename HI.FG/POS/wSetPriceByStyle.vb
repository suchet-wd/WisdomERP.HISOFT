Imports System.Windows.Forms

Public Class wSetPriceByStyle

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

    Private Sub ReposPrice_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        Try
            With Me.ogvsub
                Dim _Field As String = .GetSelectedCells(.FocusedRowHandle)(0).FieldName
                If DBNull.Value.Equals(.GetRowCellValue(.FocusedRowHandle, _Field)) Then
                    e.Cancel = True
                Else
                    e.Cancel = False
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub


    Private Sub LoadOrderBreakDown(StyleId As String)
        Dim _dt As DataTable
        Dim _Qry As String = ""
        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.SP_GETSTYLE_SIZEBREAKDOWN '" & HI.UL.ULF.rpQuoted(StyleId) & "'"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        With Me.ogvsub
            .BeginInit()
            .Columns("FTColorway").OptionsColumn.AllowFocus = DevExpress.Utils.DefaultBoolean.False

            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper
                    Case "FNHSysStyleId".ToUpper, "FTStyleCode".ToUpper, "FTColorway".ToUpper
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
                        Case "FNHSysStyleId".ToUpper, "FTStyleCode".ToUpper, "FTColorway".ToUpper
                        Case Else
                            _colcount = _colcount + 1
                            Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                            With ColG
                                .Visible = True
                                .FieldName = Col.ColumnName.ToString
                                .Name = "FTStyleCode" & Col.ColumnName.ToString
                                .Caption = Col.ColumnName.ToString
                            End With
                            .Columns.Add(ColG)
                            With .Columns(Col.ColumnName.ToString)
                                Dim _Rep As New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
                                _Rep.Precision = 2
                                _Rep.Buttons.Item(0).Visible = False
                                AddHandler _Rep.EditValueChanging, AddressOf ReposPrice_EditValueChanging
                                AddHandler _Rep.KeyDown, AddressOf Rep_KeyDown
                                .ColumnEdit = _Rep

                                .OptionsFilter.AllowAutoFilter = False
                                .OptionsFilter.AllowFilter = False
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n2}"
                                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                With .OptionsColumn
                                    .AllowMove = False
                                    .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                    .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .AllowShowHide = DevExpress.Utils.DefaultBoolean.False
                                    .AllowEdit = True
                                    .ReadOnly = False
                                    'If Col.ColumnName.ToString.ToUpper = "Total".ToUpper Then
                                    '    .AllowFocus = False
                                    'End If
                                End With
                            End With
                            '.Columns(Col.ColumnName.ToString).Width = 45
                            '.Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            '.Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"
                    End Select
                Next
            End If
            .EndInit()
        End With
        Me.ogcsub.DataSource = _dt.Copy
        _dt.Dispose()
    End Sub

#End Region
#Region "Function"
    Private Function SaveData() As Boolean
        Dim _Qry As String = ""
        Dim _oDt As DataTable = Nothing
        Try
            With CType(ogcsub.DataSource, DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With
        Catch ex As Exception
        End Try

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_ACCOUNT)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTStylePrice"
            _Qry &= vbCrLf & "  WHERE  FNHSysStyleId=" & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
            _Qry &= vbCrLf & "  AND  FNHSysCmpId=" & CInt("0" & Me.FNHSysCmpId.Properties.Tag)
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            For Each R As DataRow In _oDt.Rows
                For Each Col As DataColumn In _oDt.Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FNHSysStyleId".ToUpper, "FTStyleCode".ToUpper, "FTColorway".ToUpper
                        Case Else
                            If Val((R.Item(Col.ColumnName.ToString)).ToString()) > 0 Then

                                _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTStylePrice"
                                _Qry &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime,  FNHSysStyleId,FNHSysCmpId,FNPrice, FTColorway, FTSizeBreakDown)"
                                _Qry &= vbCrLf & " SELECT "
                                _Qry &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Qry &= vbCrLf & " , " & HI.UL.ULDate.FormatDateDB & ""
                                _Qry &= vbCrLf & " , " & HI.UL.ULDate.FormatTimeDB & ""
                                _Qry &= vbCrLf & " ," & CInt("0" & Me.FNHSysStyleId.Properties.Tag)
                                _Qry &= vbCrLf & " ," & CInt("0" & Me.FNHSysCmpId.Properties.Tag)
                                _Qry &= vbCrLf & " ," & Val(R.Item(Col.ColumnName.ToString))
                                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString()) & "'"
                                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "'"

                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    Return False
                                End If
                            End If
                    End Select
                Next
            Next

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
#End Region
#Region "General"

    Private Sub wCustomerBarcodeMapping_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Call InitGridBreakdown()
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
#End Region

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        FNHSysStyleId.Focus()
        FNHSysStyleId.SelectAll()
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Call LoadOrderBreakDown(Me.FNHSysStyleId.Text)
    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysStyleId.EditValueChanged
        Try
            Call LoadOrderBreakDown(Me.FNHSysStyleId.Text)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmSave_Click(sender As Object, e As EventArgs) Handles ocmSave.Click
        Try
            If Me.FNHSysStyleId.Text <> "" Then
                If SaveData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Call LoadOrderBreakDown(Me.FNHSysStyleId.Text)
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysStyleId_lbl.Text)
                Me.FNHSysStyleId.Focus()
                Exit Sub
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Rep_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        Try
            Dim _BPrice As Double = 0
            Select Case e.KeyCode
                Case Keys.A
                    With CType(ogcsub.DataSource, DataTable)
                        .AcceptChanges()
                        With ogvsub
                            'Dim _Field As String = .GetSelectedCells(.FocusedRowHandle)(0).FieldName
                            _BPrice = CType(sender, DevExpress.XtraEditors.CalcEdit).Value 'CDbl("0" & .GetRowCellValue(.FocusedRowHandle, _Field).ToString)

                        End With
                        For Each R As DataRow In .Rows
                            For Each Col As DataColumn In .Columns
                                Select Case Col.ColumnName.ToString.ToUpper
                                    Case "FNHSysStyleId".ToUpper, "FTStyleCode".ToUpper, "FTColorway".ToUpper
                                    Case Else
                                        If (R.Item(Col.ColumnName.ToString)).ToString() <> "" Then
                                            R.Item(Col.ColumnName) = _BPrice
                                        End If
                                End Select
                            Next
                        Next
                        .AcceptChanges()
                    End With
                Case Keys.C
                    With Me.ogvsub
                        Dim _Value As Double = CType(sender, DevExpress.XtraEditors.CalcEdit).Value
                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            Select Case GridCol.FieldName.ToString.ToUpper
                                Case "FNHSysStyleId".ToUpper, "FTStyleCode".ToUpper, "FTColorway".ToUpper
                                Case Else
                                    If .GetRowCellValue(.FocusedRowHandle, GridCol.FieldName.ToString).ToString <> "" Then
                                        .SetFocusedRowCellValue(GridCol.FieldName.ToString, _Value)
                                    End If
                            End Select
                        Next
                        CType(ogcsub.DataSource, DataTable).AcceptChanges()
                    End With
                Case Keys.S
                    With Me.ogvsub
                        Dim _Value As Double = CType(sender, DevExpress.XtraEditors.CalcEdit).Value
                        For I As Integer = 0 To .RowCount - 1
                            If .GetRowCellValue(I, .FocusedColumn.FieldName.ToString).ToString <> "" Then
                                .SetRowCellValue(I, .FocusedColumn.FieldName.ToString, _Value)
                            End If
                        Next
                        CType(ogcsub.DataSource, DataTable).AcceptChanges()
                    End With
            End Select
            Me.ogcsub.RefreshDataSource()
        Catch ex As Exception
        End Try
    End Sub
End Class