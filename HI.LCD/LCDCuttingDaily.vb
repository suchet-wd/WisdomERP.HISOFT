Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid

Public Class LCDCuttingDaily
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

    Private Sub LCDWarehouseDash_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized

        GridView1.OptionsSelection.EnableAppearanceFocusedRow = True
        GridView1.OptionsSelection.EnableAppearanceHideSelection = True
        GridView1.OptionsSelection.EnableAppearanceFocusedCell = False

        GridView1.OptionsView.ShowAutoFilterRow = False
        GridView1.OptionsSelection.MultiSelect = False
        GridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect

        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In GridView1.Columns
            With GridCol
                .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                .OptionsColumn.AllowMove = False

            End With
        Next


        Dim mTheard As New Threading.Thread(AddressOf LoadData)
        mTheard.Start()


    End Sub

    Private Sub LCDWarehouseDash_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Delegate Sub DelegateLoadTime()
    Private Sub LoadData()
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New DelegateLoadTime(AddressOf LoadData), New Object() {})
            Else



                Dim RowIndx As Integer = 0
                Try
                    Dim _Cmd As String = ""

                    _Cmd = " exec USP_GETDATACUTTING_DASH '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserLogInComputer) & "'," & Val(HI.ST.SysInfo.CmpID)
                    Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                    Me.ogc.DataSource = dt.Copy

                    RowIndx = dt.Rows.Count

                    loadInfo(dt)
                Catch ex As Exception
                End Try

                If RowIndx > 0 Then
                    Me.ottime.Enabled = True
                Else

                    If RowIndx > 12 Then
                        Me.ottime.Interval = 10000
                    Else
                        Me.ottime.Interval = (120000 / RowIndx)
                    End If

                    Timer1.Enabled = True

                End If

            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Rowshow As Integer = 0
    Private Sub ottime_Tick(sender As Object, e As EventArgs) Handles ottime.Tick
        Try
            ottime.Enabled = False
            Timer1.Enabled = False

            Dim mTheard As New Threading.Thread(AddressOf LoadData)
            mTheard.Start()

        Catch ex As Exception
            ottime.Enabled = True
            ' Timer1.Enabled = True
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim _Cmd As String = ""
            Dim _Step As Integer = 1

            'If Rowshow <= GridView1.RowCount Then

            '    Rowshow += +1

            '    'If GridView1.RowCount < 14 Then
            '    '    Rowshow += +1
            '    'ElseIf GridView1.RowCount >= 14 And GridView1.RowCount < 20 Then
            '    '    Rowshow += +2
            '    'ElseIf GridView1.RowCount >= 20 And GridView1.RowCount < 28 Then
            '    '    Rowshow += +3
            '    'Else
            '    '    Rowshow += +14
            '    'End If

            'Else
            '    Rowshow = 0
            'End If

            'GridView1.OptionsNavigation.AutoMoveRowFocus = True

            'Me.GridView1.FocusedRowHandle = Rowshow
            'Me.GridView1.RefreshRow(Rowshow)

            If Rowshow < GridView1.RowCount Then
                Rowshow = Rowshow + 1

                GridView1.OptionsNavigation.AutoMoveRowFocus = True

                Me.GridView1.FocusedRowHandle = Rowshow
                Me.GridView1.RefreshRow(Rowshow)

            Else
                Timer1.Enabled = False
                Rowshow = 0
                Dim mTheard As New Threading.Thread(AddressOf LoadData)
                mTheard.Start()

            End If

        Catch ex As Exception
        End Try
    End Sub


    Sub loadInfo(ByRef pdt As DataTable)
        Try

            Try

                lblTotalQty.Text = Val(pdt.Compute("sum(FNTarget)", "FTStateToday = '1'")).ToString("#,0")
                lblFinishQty.Text = Val(pdt.Compute("sum(FNQuantity)", "FTStateToday = '1'")).ToString("#,0")
                lblBalQty.Text = Val(pdt.Compute("sum(FNBalQty)", "FTStateToday = '1'")).ToString("#,0")

            Catch ex As Exception

                lblFinishQty.Text = 0
                lblBalQty.Text = pdt.Rows.Count

            End Try

        Catch ex As Exception

        End Try
        pdt.Dispose()
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

    Private Sub LCDCuttingDaily_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Try

            Dim stdw As Integer = 1460
            Dim stdh As Integer = 900
            Dim HStdF As Integer = 27
            Dim HStdFLine As Integer = 23

            Dim wWith As Integer = Me.Width

            HStdF = (wWith * HStdF) / stdw
            HStdFLine = (wWith * HStdFLine) / stdw


            GridView1.Appearance.HeaderPanel.Font = New Font("Tahoma", HStdF)
            GridView1.Appearance.Row.Font = New Font("Tahoma", HStdFLine)
            GridView1.Appearance.FocusedRow.Font = New Font("Tahoma", HStdFLine)


            With Me.GridView1
                .Columns.ColumnByFieldName("FDPlanDate").Width = ((wWith * 12) / 100)
                .Columns.ColumnByFieldName("FTJobNo").Width = ((wWith * 15) / 100)
                .Columns.ColumnByFieldName("FTStyleCode").Width = ((wWith * 12) / 100)
                .Columns.ColumnByFieldName("FTColorway").Width = ((wWith * 18) / 100)
                .Columns.ColumnByFieldName("FNTarget").Width = ((wWith * 9) / 100)
                .Columns.ColumnByFieldName("FNQuantity").Width = ((wWith * 9) / 100)
                '.Columns.ColumnByFieldName("FNPer").Width = ((wWith * 12) / 100)


            End With

            lbltotalpo.Width = ((wWith * 15) / 100)
            lblTotalQty.Width = ((wWith * 20) / 100)
            lblFinishQty.Width = ((wWith * 20) / 100)
            lblFinishQty.Width = ((wWith * 20) / 100)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView1.RowStyle
        Try
            If Val(GridView1.GetRowCellValue(e.RowHandle, "FNBalQty").ToString) <= 0 Then
                e.Appearance.ForeColor = Color.Lime
                e.HighPriority = True
            End If
        Catch ex As Exception

        End Try
    End Sub



#Region "Property"

#End Region

End Class