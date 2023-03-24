Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid

Public Class wQAPreFinalCustomerTracking

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


    End Sub


#Region "Procedure"
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
            .Columns.Add("FTSubOrderNo", GetType(String))
            .Columns.Add("FTStatePreFinal", GetType(String))
        End With

        Try

            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_QAPreFinal_Tracking_Cust  " & HI.ST.SysInfo.CmpID & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "' "
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Dim _StrFilter As String = ""
            Dim _StrFilter2 As String = ""
            For Each R As DataRow In dt.Rows
                _StrFilter = "FTQADate='" & HI.UL.ULF.rpQuoted(R!FTQADate.ToString) & "' AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "' "

                If dtdata.Select(_StrFilter).Length <= 0 Then
                    dtdata.Rows.Add(R!FTVenderPramCode.ToString, R!FTQADate.ToString,
                                    R!FTUserQA.ToString, R!FTFactory.ToString, R!FTJobState.ToString,
                                    R!FTPORef.ToString, R!FNQAInQty.ToString,
                                    R!FNQAActualQty.ToString, R!FTStyleCode.ToString,
                                    R!FTColorway.ToString, R!FTSeasonCode.ToString,
                                    R!FTSubOrderNo.ToString, R!FTStatePreFinal.ToString)
                End If

                Dim i As Integer = 0
                dtdata.BeginInit()
                Dim Found As Boolean = False
                For Each Rx As DataRow In dtdata.Select(_StrFilter)
                    If dtdata.Columns.Count >= 12 Then
                        '      Dim Found As Boolean = False
                        For ind As Integer = 12 To dtdata.Columns.Count - 1

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

    Private Sub InitGridCustomer(Optional _dt As DataTable = Nothing)

        Try
            Dim _colcount As Integer = 0
            With Me.ogvDetail

                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case .Columns(I).FieldName.ToString.ToUpper

                        Case "FTVenderPramCode".ToUpper, "FTQADate".ToUpper _
                            , "FTUserQA".ToUpper, "FTFactory".ToUpper, "FTJobState".ToUpper _
                             , "FTPORef".ToUpper, "FNQAInQty".ToUpper _
                             , "FNQAActualQty".ToUpper, "FTStyleCode".ToUpper _
                             , "FTColorway".ToUpper, "FTSeasonCode".ToUpper _
                             , "FTSubOrderNo".ToUpper, "FTStatePreFinal".ToUpper

                            .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                        Case Else
                            .Columns.Remove(.Columns(I))
                    End Select
                Next


                If _dt.Columns.Count > 13 Then
                    For ind As Integer = 13 To _dt.Columns.Count - 1

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
                     , "FTColorway".ToUpper, "FTSeasonCode".ToUpper _
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
End Class