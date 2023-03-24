Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid

Public Class wReportQADetail_tracking

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


    End Sub


#Region "Procedure"

    Private Sub LoadCompany()
        Dim _Str As String

        _Str = " SELECT   '1' AS FTSelect "
        _Str &= vbCrLf & ",M.FNHSysCmpId"
        _Str &= vbCrLf & ",M.FTCmpCode,ISNULL(IPP.FTIPServer,'') AS FTIPServer"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            _Str &= vbCrLf & " , M.FTCmpNameTH AS FTCmpName "
            _Str &= vbCrLf & " ,ISNULL(("
            _Str &= vbCrLf & "SELECT TOP 1 FTNameTH"
            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)"
            _Str &= vbCrLf & "WHERE  (FTListName = N'FNCompensationFoundByYearOption') "
            _Str &= vbCrLf & "AND (FNListIndex = 0)"
            _Str &= vbCrLf & " ),'') AS FNCompensationFoundByYearOption "

        Else

            _Str &= vbCrLf & " , M.FTCmpNameEN AS FTCmpName "
            _Str &= vbCrLf & " ,ISNULL(("
            _Str &= vbCrLf & "SELECT TOP 1 FTNameEN"
            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)"
            _Str &= vbCrLf & "WHERE  (FTListName = N'FNCompensationFoundByYearOption') "
            _Str &= vbCrLf & "AND (FNListIndex = 0)"
            _Str &= vbCrLf & " ),'') AS FNCompensationFoundByYearOption "

        End If

        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS M WITH(NOLOCK) "
        _Str &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSECompanyIPServer AS IPP WITH(NOLOCK) ON M.FNHSysCmpId = IPP.FNHSysCmpId "
        _Str &= vbCrLf & " WHERE ISNULL(M.FTStateActive,'') ='1' AND ISNULL(IPP.FTIPServer,'') <>'' "
        _Str &= vbCrLf & " ORDER BY M.FTCmpCode"

        Me.ogccmp.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

    End Sub


    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim dt As New DataTable

        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Dim dtdata As New DataTable
        With dtdata
            .Columns.Add("FTVenderPramCode", GetType(String))
            .Columns.Add("FTQADate", GetType(String))
            .Columns.Add("FTUserQA", GetType(String))
            .Columns.Add("FTFactory", GetType(String))
            .Columns.Add("FTJobState", GetType(String))
            .Columns.Add("FTPORef", GetType(String))
            .Columns.Add("FNQAInQty", GetType(Integer))
            .Columns.Add("FNQAActualQty", GetType(Integer))
            .Columns.Add("FTStyleCode", GetType(String))
            .Columns.Add("FTColorway", GetType(String))
            .Columns.Add("FTSeasonCode", GetType(String))
            .Columns.Add("FTSeasonCodeYear", GetType(String))
            .Columns.Add("FTSubOrderNo", GetType(String))
            .Columns.Add("FTStatePreFinal", GetType(String))
        End With

        Try

            Dim _odtcmp As DataTable
            With DirectCast(Me.ogccmp.DataSource, DataTable)
                .AcceptChanges()

                _odtcmp = .Copy

            End With
            dt = New DataTable
            'For Each CMP As DataRow In _odtcmp.Select("FTSelect='1'")

            '    _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_QAPreFinal_Tracking_Cust_report  " & Val(CMP!FNHSysCmpId.ToString) & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "' "
            '    dt.Merge(HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR))


            'Next


            If _odtcmp.Select("FTSelect='1'").Length > 0 Then
                _odtcmp.Dispose()

                Dim _ServerName, _UID, _PWS, _DBName As String
                Dim _ConnectString As String = ""
                Dim _FNHSysCmpId As Integer = 0
                Dim _CmpCode As String

                Dim _TotalRow As Integer = 0
                Dim _Rx As Integer = 0
                Dim Ridx As Integer = 0


                For Each R As DataRow In _odtcmp.Select("FTSelect='1'")

                    _CmpCode = R!FTCmpCode.ToString

                    If HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_PROD) Then
                        Ridx = Ridx + 1

                        _ServerName = R!FTIPServer.ToString
                        _UID = HI.Conn.DB.UIDName
                        _PWS = HI.Conn.DB.PWDName
                        _DBName = HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD)

                        _ConnectString = "SERVER=" & _ServerName & ";UID=" & _UID & ";PWD=" & _PWS & ";Initial Catalog=" & _DBName

                        _Spls.UpdateInformation("Loading.... Data Company " & R!FTCmpCode.ToString & "   Please wait....")
                        Try
                            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_QAPreFinal_Tracking_Cust_report  " & Val(R!FNHSysCmpId.ToString) & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "' "
                            ' dt.Merge(HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR))
                            dt.Merge(GetSewFGData(_Qry, _ConnectString))
                        Catch ex22 As Exception
                            ' System.Windows.Forms.MessageBox.Show(ex22.Message())
                        End Try
                    End If
                Next

            End If
            _odtcmp.Dispose()





            Dim _StrFilter As String = ""
            Dim _StrFilter2 As String = ""
            For Each R As DataRow In dt.Rows
                _StrFilter = "FTQADate='" & HI.UL.ULF.rpQuoted(R!FTQADate.ToString) & "' AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' "
                _StrFilter &= "  AND FTPORef='" & HI.UL.ULF.rpQuoted(R!FTPORef.ToString) & "'  AND FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' "

                If dtdata.Select(_StrFilter).Length <= 0 Then
                    dtdata.Rows.Add(R!FTVenderPramCode.ToString, R!FTQADate.ToString,
                                    R!FTUserQA.ToString, R!FTFactory.ToString, R!FTJobState.ToString,
                                    R!FTPORef.ToString, R!FNQAInQty.ToString,
                                    R!FNQAActualQty.ToString, R!FTStyleCode.ToString,
                                    R!FTColorway.ToString, R!FTSeasonCode.ToString, R!FTSeasonCodeYear.ToString,
                                    R!FTSubOrderNo.ToString, R!FTStatePreFinal.ToString)
                End If

                Dim i As Integer = 0
                dtdata.BeginInit()
                Dim Found As Boolean = False
                For Each Rx As DataRow In dtdata.Select(_StrFilter)
                    If Integer.Parse("0" & R!FNStateQAByDetail.ToString) > 0 Then
                        If dtdata.Columns.Count >= 13 Then
                            '      Dim Found As Boolean = False
                            For ind As Integer = 13 To dtdata.Columns.Count - 1

                                If Microsoft.VisualBasic.Left(dtdata.Columns(ind).ColumnName.ToString, 2) = "TD" Then

                                    If Rx.Item(dtdata.Columns(ind).ColumnName.ToString).ToString = "" Then
                                        Rx.Item(dtdata.Columns(ind).ColumnName.ToString) = R!FTQADetailCode.ToString
                                        Rx.Item(dtdata.Columns(ind).ColumnName.ToString.Replace("TD", "TQ")) = Integer.Parse("0" & R!FNStateQAByDetail.ToString) '1

                                        Found = True
                                        Exit For
                                    ElseIf Rx.Item(dtdata.Columns(ind).ColumnName.ToString).ToString = R!FTQADetailCode.ToString Then

                                        ' Rx.Item(dtdata.Columns(ind).ColumnName.ToString.Replace("TD", "TQ")) = Integer.Parse(Val(Rx.Item(dtdata.Columns(ind).ColumnName.ToString.Replace("TD", "TQ")).ToString)) + 1
                                        Found = True
                                        Exit For

                                    End If

                                    i = i + 1
                                End If

                            Next

                            If Found = False Then
                                dtdata.Columns.Add("TD" & i.ToString, GetType(String))
                                dtdata.Columns.Add("TQ" & i.ToString, GetType(Integer))

                                Rx.Item("TD" & i.ToString) = R!FTQADetailCode.ToString
                                Rx.Item("TQ" & i.ToString) = Integer.Parse("0" & R!FNStateQAByDetail.ToString)

                            End If
                        Else
                            dtdata.Columns.Add("TD" & i.ToString)
                            dtdata.Columns.Add("TQ" & i.ToString)
                        End If
                    End If


                Next


                dtdata.EndInit()

            Next
            Call InitGridCustomer(dtdata)

            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try

        dt.Dispose()

    End Sub


    Private Function GetSewFGData(cmsstring As String, connstring As String) As DataTable
        Dim _Cnn = New System.Data.SqlClient.SqlConnection()
        Dim _Cmd = New System.Data.SqlClient.SqlCommand()
        Dim objDataSet As New DataTable
        Try

            If _Cnn.State = ConnectionState.Open Then
                _Cnn.Close()
            End If
            _Cnn.ConnectionString = connstring
            _Cnn.Open()
            _Cmd = _Cnn.CreateCommand

            Dim _Adepter As New System.Data.SqlClient.SqlDataAdapter(_Cmd)
            _Adepter.SelectCommand.CommandTimeout = 0
            _Adepter.SelectCommand.CommandType = CommandType.Text
            _Adepter.SelectCommand.CommandText = cmsstring
            _Adepter.Fill(objDataSet)
            _Adepter.Dispose()

            HI.Conn.SQLConn.DisposeSqlConnection(_Cmd)
            HI.Conn.SQLConn.DisposeSqlConnection(_Cnn)
        Catch ex As Exception
            HI.Conn.SQLConn.DisposeSqlConnection(_Cmd)
            HI.Conn.SQLConn.DisposeSqlConnection(_Cnn)
        End Try
        Return objDataSet
    End Function

    Private Sub InitGridCustomer(Optional _dt As DataTable = Nothing)

        Try
            Dim _colcount As Integer = 0
            With Me.ogvDetail

                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case .Columns(I).FieldName.ToString.ToUpper

                        Case "FTVenderPramCode".ToUpper, "FTQADate".ToUpper _
                            , "FTUserQA".ToUpper, "FTFactory".ToUpper, "FTJobState".ToUpper _
                             , "FTPORef".ToUpper, "FNQAInQty".ToUpper _
                             , "FNQAActualQty".ToUpper, "FTStyleCode".ToUpper, "FTSeasonCodeYear".ToUpper _
                             , "FTColorway".ToUpper, "FTSeasonCode".ToUpper _
                             , "FTSubOrderNo".ToUpper, "FTStatePreFinal".ToUpper

                            .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                        Case Else
                            .Columns.Remove(.Columns(I))
                    End Select
                Next


                If _dt.Columns.Count > 14 Then
                    For ind As Integer = 14 To _dt.Columns.Count - 1

                        Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                        With ColG
                            .Visible = True
                            .FieldName = _dt.Columns(ind).ColumnName.ToString
                            .Name = "C" & _dt.Columns(ind).ColumnName.ToString

                            If Microsoft.VisualBasic.Left(_dt.Columns(ind).ColumnName.ToString, 2) = "TD" Then
                                .Caption = "Defect Code" & Microsoft.VisualBasic.Right(_dt.Columns(ind).ColumnName.ToString, _dt.Columns(ind).ColumnName.ToString.Length - 2)
                            Else
                                .Caption = "Defect Qty" & Microsoft.VisualBasic.Right(_dt.Columns(ind).ColumnName.ToString, _dt.Columns(ind).ColumnName.ToString.Length - 2)
                            End If

                            .Width = 90
                        End With

                        .Columns.Add(ColG)

                        With ColG

                            .OptionsFilter.AllowAutoFilter = False
                            .OptionsFilter.AllowFilter = False
                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.None
                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center


                            With .OptionsColumn
                                .AllowMove = False
                                .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                .AllowEdit = False
                                .AllowMove = False
                            End With

                        End With
                    Next


                End If

            End With

            Me.ogcDetail.DataSource = _dt
        Catch ex As Exception

        End Try

    End Sub

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If Me.FTStartDate.Text <> "" And Me.FTEndDate.Text <> "" Then
            Call LoadData()
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกช่วงข้อมูลที่ต้องการดูข้อมูล !!!", 1496730001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadCompany()
    End Sub

    Private Sub pivotGridControl_CustomCellDisplayText(sender As Object, e As DevExpress.XtraPivotGrid.PivotCellDisplayTextEventArgs)
        Try
            If (e.Value = 0) Then e.DisplayText = ""

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvDetail_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvDetail.RowCellStyle
        Try
            Select Case e.Column.FieldName.ToString.ToUpper
                Case "FTVenderPramCode".ToUpper, "FTQADate".ToUpper _
                    , "FTUserQA".ToUpper, "FTFactory".ToUpper, "FTJobState".ToUpper _
                     , "FTPORef".ToUpper, "FNQAInQty".ToUpper _
                     , "FNQAActualQty".ToUpper, "FTStyleCode".ToUpper _
                     , "FTColorway".ToUpper, "FTSeasonCode".ToUpper, "FTSeasonCodeYear".ToUpper _
                     , "FTSubOrderNo".ToUpper, "FTStatePreFinal".ToUpper
                Case Else
                    If Microsoft.VisualBasic.Left(e.Column.FieldName.ToString, 2).ToUpper = "TQ" Then
                        If e.CellValue > 0 Then
                            e.Appearance.BackColor = System.Drawing.Color.Yellow
                            e.Appearance.BackColor2 = System.Drawing.Color.LightYellow
                            e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
                        End If
                    End If


            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private _StateSetSelectAll As Boolean = True
    Private Sub oChkSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles oChkSelectAll.CheckedChanged
        Try

            If _StateSetSelectAll = False Then Exit Sub
            _StateSetSelectAll = False
            '    Me.oChkSelectAll.Checked = False

            Dim _State As String = "0"
            If Me.oChkSelectAll.Checked Then
                _State = "1"
            End If

            With ogccmp
                If Not (.DataSource Is Nothing) And ogvcmp.RowCount > 0 Then

                    With ogvcmp
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With
                    CType(.DataSource, System.Data.DataTable).AcceptChanges()
                End If
            End With

        Catch ex As Exception
        End Try
        _StateSetSelectAll = True
    End Sub

End Class