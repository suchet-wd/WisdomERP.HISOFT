Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid

Public Class LCDWarehouseDash
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
        LoadData()
        Dim _Cmd As String
        With BandedGridView2
            If .RowCount < 0 Or .FocusedRowHandle < -1 Then Exit Sub
            Dim shipdate As String = HI.UL.ULDate.ConvertEnDB(.GetRowCellValue(0, .Columns("FDShipDate")))

            _Cmd = "Select  *     FROM     dbo.getTotalCustomerPO70('" & shipdate & "')  "
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            loadInfo()
            'LoadData(shipdate)
        End With

    End Sub

    Private Sub LCDWarehouseDash_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Delegate Sub DelegateLoadTime()
    Private Sub LoadData(Optional ByVal ShipDate As String = "")
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New DelegateLoadTime(AddressOf LoadData), New Object() {})
            Else
                Try
                    Dim _Cmd As String = ""
                    _Cmd = "Select  *     FROM     dbo.getTotalCustomerPO70('" & ShipDate & "')  "
                    _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
                    _Cmd = " exec SP_GETLCDWHLean70 " & Val(HI.ST.SysInfo.CmpID)
                    Me.ogc.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles BandedGridView2.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim category As Integer = View.GetRowCellDisplayText(e.RowHandle, View.Columns("FNQuantityBal"))
                Dim shipdate As String = HI.UL.ULDate.ConvertEnDB(View.GetRowCellDisplayText(e.RowHandle, View.Columns("FDShipDate")))
                'Dim getDateNow As String = Date.Now().ToShortDateString("yyyy/MM/dd")
                Dim _Qty As Integer = View.GetRowCellValue(e.RowHandle, View.Columns("FNQuantityScanFG"))
                Dim _Seq As Integer = View.GetRowCellValue(e.RowHandle, View.Columns("FNSeq"))


                'If _Seq > 0 Then
                '    e.Appearance.ForeColor = Color.Red
                '    '  e.HighPriority = True
                'End If
                If _Qty > 0 Then
                    e.Appearance.ForeColor = Color.Yellow
                    ' e.HighPriority = True
                End If


                If category = 0 Then
                    e.Appearance.ForeColor = Color.Green
                    ' e.HighPriority = True
                End If
                'If category > 20 And (shipdate <= getDateNow) Then
                '    e.Appearance.ForeColor = Color.Red
                '    e.HighPriority = True
                'End If
            End If
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
        Try
            Dim _Cmd As String = ""
            Dim _Step As Integer = 1

            If Rowshow <= BandedGridView2.RowCount Then
                If BandedGridView2.RowCount < 14 Then
                    Rowshow += +1
                ElseIf BandedGridView2.RowCount >= 14 And BandedGridView2.RowCount < 20 Then
                    Rowshow += +2
                ElseIf BandedGridView2.RowCount >= 20 And BandedGridView2.RowCount < 28 Then
                    Rowshow += +3
                Else
                    Rowshow += +14
                End If

            Else
                Rowshow = 0
                With BandedGridView2
                    If .RowCount < 0 Or .FocusedRowHandle < -1 Then Exit Sub
                    Dim shipdate As String = HI.UL.ULDate.ConvertEnDB(.GetRowCellValue(Rowshow, .Columns("FDShipDate")))

                    _Cmd = "Select  *     FROM     dbo.getTotalCustomerPO70('" & shipdate & "')  "
                    _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
                    '  loadInfo()
                    LoadData(shipdate)
                End With
                ' LoadData()
            End If

            Me.BandedGridView2.FocusedRowHandle = Rowshow
            Me.BandedGridView2.RefreshRow(Rowshow)
            BandedGridView2.OptionsNavigation.AutoMoveRowFocus = True

            With BandedGridView2
                If .RowCount < 0 Or .FocusedRowHandle < -1 Then Exit Sub
                Dim shipdate As String = HI.UL.ULDate.ConvertEnDB(.GetRowCellValue(Rowshow, .Columns("FDShipDate")))
                _Cmd = "Select  *     FROM     dbo.getTotalCustomerPO70('" & shipdate & "')  "
                _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
                loadInfo()


            End With

        Catch ex As Exception
        End Try
    End Sub


    Sub loadInfo()
        Try
            lblTotalQty.Text = _oDt.Rows.Count
            Try
                lblFinishQty.Text = _oDt.Compute("count(FTPOref)", "FNTotalCartonBal = 0")
                lblBalQty.Text = _oDt.Rows.Count - _oDt.Compute("count(FTPOref)", "FNTotalCartonBal = 0")
                lblgrandqty.Text = String.Format(CultureInfo.InvariantCulture, "{0:#,#}", _oDt.Compute("sum(FNGrandQuantity)", "FNGrandQuantity>0"))
                lblbal.Text = String.Format(CultureInfo.InvariantCulture, "{0:#,#}", _oDt.Compute("sum(FNQuantityBal)", "FNQuantityBal>0"))
            Catch ex As Exception
                lblFinishQty.Text = 0
                lblBalQty.Text = _oDt.Rows.Count
                lblgrandqty.Text = 0


            End Try


        Catch ex As Exception

        End Try
    End Sub

    Private Sub lbltotalpo_KeyDown(sender As Object, e As KeyEventArgs) Handles lbltotalpo.KeyDown
        'Me.Close()
    End Sub

    Private Sub ogc_KeyDown(sender As Object, e As KeyEventArgs) Handles ogc.KeyDown
        Try
            'Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BandedGridView2_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles BandedGridView2.RowCellStyle
        Try



            Dim view As GridView = TryCast(sender, GridView)
            Dim _mark As Integer = Integer.Parse(Val((view.GetRowCellValue(e.RowHandle, "FNSeq"))))
            Dim _ShipDate As String = "" : Dim _ShipCals As String = ""
            _ShipDate = HI.UL.ULDate.ConvertEnDB(view.GetRowCellValue(e.RowHandle, "FDShipDate"))
            _ShipCals = HI.UL.ULDate.ConvertEnDB(view.GetRowCellValue(e.RowHandle, "FDExpectedFinishDate"))



            If e.Column.FieldName = "FDShipDate" Then
                If _mark > 0 Then
                    e.Appearance.ForeColor = Color.Red
                End If
            End If
            If _ShipDate < _ShipCals Then
                If e.Column.FieldName = "FDExpectedFinishDate" Then
                    e.Appearance.ForeColor = Color.Red
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub



#Region "Property"

#End Region

End Class