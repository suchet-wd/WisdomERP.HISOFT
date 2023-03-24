Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid

Public Class LCDEmployeeLeaveDash
    Private _StateFTStateDaily As Boolean = False
    Private _TimeSwitchtoSpeed As Integer = 0
    Private _TimeSwitchToHeader As Integer = 1


    Private _TotalEmpFromMasterLine1 As Integer = 0
    Private _TotalEmpHRmorningLine1 As Integer = 0


    Private _SystemFilePath As Boolean = False
    Property StateWindowsUser As Boolean
        Get
            Return _SystemFilePath
        End Get
        Set(value As Boolean)
            _SystemFilePath = value
        End Set
    End Property

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.



        Dim _Qry As String = ""
        Dim _dt As DataTable

        'olbqc
    End Sub
    Private _oDt As DataTable
    Private Sub LCDWarehouseDash_Load(sender As Object, e As EventArgs) Handles Me.Load

        HI.TL.HandlerControl.AddHandlerObj(Me)

        LabelControl2.Text = "Employee Leave \ Resign"
        LoadData()

        ogvDocket.OptionsView.ShowAutoFilterRow = False
        '  ogvDocket.OptionsView.ColumnAutoWidth = True

        bindData()




        'Dim _Cmd As String
        'With BandedGridView2
        '    If .RowCount < 0 Or .FocusedRowHandle < -1 Then Exit Sub
        '    Dim shipdate As String = HI.UL.ULDate.ConvertEnDB(.GetRowCellValue(0, .Columns("FDShipDate")))

        '    _Cmd = "Select  *     FROM     dbo.getTotalCustomerPO91('" & shipdate & "')  "
        '    _Cmd = "Select  *     FROM     dbo.getTotalCustomerPO91('" & shipdate & "')  "
        '    _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
        '    loadInfo()
        '    'LoadData(shipdate)
        'End With

    End Sub

    Private Sub LCDWarehouseDash_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
            Case Keys.D + Keys.Control
                Me.WindowState = FormWindowState.Minimized
            Case Keys.F + Keys.Control
                Me.WindowState = FormWindowState.Minimized

        End Select
    End Sub

    Private Delegate Sub DelegateLoadTime()
    Private Sub LoadData(Optional ByVal ShipDate As String = "")
        Try
            'If Me.InvokeRequired Then
            '    Me.Invoke(New DelegateLoadTime(AddressOf LoadData), New Object() {})
            'Else
            '    Try
            '        Dim _Cmd As String = ""
            '        _Cmd = "Select  *     FROM     dbo.getTotalCustomerPO91('" & ShipDate & "')  "
            '        _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            '        _Cmd = " exec SP_GETLCDWHLean91 " & Val(HI.ST.SysInfo.CmpID)
            '        Me.ogc.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            '    Catch ex As Exception
            '    End Try
            'End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs)
        Try
            Dim View As GridView = sender
            'If (e.RowHandle >= 0) Then
            '    Dim category As Integer = View.GetRowCellDisplayText(e.RowHandle, View.Columns("FNQuantityBal"))
            '    Dim shipdate As String = HI.UL.ULDate.ConvertEnDB(View.GetRowCellDisplayText(e.RowHandle, View.Columns("FDShipDate")))
            '    'Dim getDateNow As String = Date.Now().ToShortDateString("yyyy/MM/dd")
            '    Dim _Qty As Integer = View.GetRowCellValue(e.RowHandle, View.Columns("FNQuantityScanFG"))
            '    Dim _Seq As Integer = View.GetRowCellValue(e.RowHandle, View.Columns("FNSeq"))


            '    'If _Seq > 0 Then
            '    '    e.Appearance.ForeColor = Color.Red
            '    '    '  e.HighPriority = True
            '    'End If
            '    If _Qty > 0 Then
            '        e.Appearance.ForeColor = Color.Yellow
            '        ' e.HighPriority = True
            '    End If


            '    If category = 0 Then
            '        e.Appearance.ForeColor = Color.Green
            '        ' e.HighPriority = True
            '    End If
            '    'If category > 20 And (shipdate <= getDateNow) Then
            '    '    e.Appearance.ForeColor = Color.Red
            '    '    e.HighPriority = True
            '    'End If
            'End If
        Catch ex As Exception
        End Try
    End Sub
    Private Rowshow As Integer = 0
    Private Sub ottime_Tick(sender As Object, e As EventArgs) Handles ottime.Tick
        Try
            ' LoadData()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        bindData()
    End Sub


    Private Sub bindData()
        Try
            Dim _Qry As String = ""
            Dim _FNHSysCmpId As Integer = 0
            Dim _dt As DataTable

            _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GET_DOCKET_TIME_LEAVE "
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Dim _colcount As Integer = 0

            With Me.ogvDocket

                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case .Columns(I).FieldName.ToString.ToUpper

                        Case "FNListIndex".ToUpper, "FTNameTH".ToUpper, "FTNameEN".ToUpper
                            .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                        Case Else
                            .Columns.Remove(.Columns(I))
                    End Select
                Next

                If Not (_dt Is Nothing) Then
                    For Each Col As DataColumn In _dt.Columns

                        Select Case Col.ColumnName.ToString.ToUpper
                            Case "FNListIndex".ToUpper, "FTNameTH".ToUpper, "FTNameEN".ToUpper

                            Case Else
                                _colcount = _colcount + 1
                                Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                                With ColG
                                    .Visible = True
                                    .FieldName = Col.ColumnName.ToString

                                    .Name = "FTDate" & Col.ColumnName.ToString


                                    If IsDate(Col.ColumnName.ToString) Then

                                        .Caption = Format(CType(Col.ColumnName.ToString, Date), "dd/MM/yyyy")
                                    Else
                                        .Caption = Col.ColumnName.ToString
                                    End If


                                    .AppearanceHeader.BackColor = Color.Green
                                End With

                                .Columns.Add(ColG)

                                With .Columns(Col.ColumnName.ToString)

                                    .OptionsFilter.AllowAutoFilter = False
                                    .OptionsFilter.AllowFilter = False
                                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                    '.DisplayFormat.FormatString = "{0:n2}"
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                    .Width = 165
                                    .AppearanceCell.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
                                    .AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                                    .AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(0, Byte), Integer))
                                    .AppearanceHeader.Font = New System.Drawing.Font("Tahoma", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

                                    .AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
                                    With .OptionsColumn
                                        .AllowMove = False
                                        .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                        .AllowEdit = False
                                        .ReadOnly = True

                                    End With

                                End With

                                .Columns(Col.ColumnName.ToString).Width = 165
                                .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                                .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n2}"

                        End Select

                    Next

                End If


            End With

            Me.ogcDocket.DataSource = _dt.Copy

            _colcount = 0



        Catch ex As Exception
        End Try
    End Sub


    Sub loadInfo()
        Try
            'lblTotalQty.Text = _oDt.Rows.Count
            'Try
            '    lblFinishQty.Text = _oDt.Compute("count(FTPOref)", "FNTotalCartonBal = 0")
            '    lblBalQty.Text = _oDt.Rows.Count - _oDt.Compute("count(FTPOref)", "FNTotalCartonBal = 0")
            '    lblgrandqty.Text = String.Format(CultureInfo.InvariantCulture, "{0:#,#}", _oDt.Compute("sum(FNGrandQuantity)", "FNGrandQuantity>0"))
            '    lblbal.Text = String.Format(CultureInfo.InvariantCulture, "{0:#,#}", _oDt.Compute("sum(FNQuantityBal)", "FNQuantityBal>0"))
            'Catch ex As Exception
            '    lblFinishQty.Text = 0
            '    lblBalQty.Text = _oDt.Rows.Count
            '    lblgrandqty.Text = 0
            'End Try
        Catch ex As Exception
        End Try
    End Sub

    Private Sub lbltotalpo_KeyDown(sender As Object, e As KeyEventArgs)
        'Me.Close()

    End Sub

    Private Sub ogc_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            '  Me.Close()



        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvDocket_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvDocket.RowCellStyle
        Try

            Dim view As GridView = TryCast(sender, GridView)
            'Dim _mark As Integer = Integer.Parse(Val((view.GetRowCellValue(e.RowHandle, "FNSeq"))))
            'Dim _ShipDate As String = "" : Dim _ShipCals As String = ""
            '_ShipDate = HI.UL.ULDate.ConvertEnDB(view.GetRowCellValue(e.RowHandle, "FDShipDate"))
            '_ShipCals = HI.UL.ULDate.ConvertEnDB(view.GetRowCellValue(e.RowHandle, "FDExpectedFinishDate"))



            If e.Column.FieldName <> "FTNameTH" Then
                e.Appearance.ForeColor = Color.Black
                e.Appearance.BackColor = Color.White
                e.Appearance.BackColor2 = Color.White

                ' e.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical


            End If

            'If _ShipDate < _ShipCals Then
            '    If e.Column.FieldName = "FDExpectedFinishDate" Then
            '        e.Appearance.ForeColor = Color.Red
            '    End If

            'End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LabelControl2_Click(sender As Object, e As EventArgs) Handles LabelControl2.Click

    End Sub

    Private Sub ogvDocket_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles ogvDocket.CustomColumnDisplayText

    End Sub

    Private Sub ogvDocket_CustomDrawCell(sender As Object, e As RowCellCustomDrawEventArgs) Handles ogvDocket.CustomDrawCell
        Select Case e.Column.FieldName.ToString.ToUpper

            Case "FNListIndex".ToUpper, "FTNameTH".ToUpper, "FTNameEN".ToUpper
            Case Else
                If ogvDocket.GetRowCellValue(e.RowHandle, "FNListIndex") = "905" Then
                    e.DisplayText = Format(e.CellValue, "0.00") & " %"
                Else



                    If e.CellValue.ToString = "0.00" Then
                        e.DisplayText = ""
                    Else
                        e.DisplayText = Format(e.CellValue, "#0")
                    End If


                End If


        End Select

        'If e.RowHandle = GridControl.AutoFilterRowHandle Then
        '    e.DisplayText = "AutoFilterRow"
        'End If
        'If e.RowHandle = GridControl.NewItemRowHandle Then
        '    e.DisplayText = "NewItemRow"
        'End If
    End Sub

    'Private Sub GridView1_CustomDrawColumnHeader(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventArgs)
    '    If e.Column Is Nothing Then
    '        Return
    '    End If
    '    If e.Column.AppearanceHeader.BackColor <> Color.Empty Then
    '        e.Info.AllowColoring = True
    '        e.Column.AppearanceHeader.BackColor = Color.Green

    '    End If
    'End Sub

#Region "Property"

#End Region

End Class