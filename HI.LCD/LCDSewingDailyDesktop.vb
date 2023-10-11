Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid

Public Class LCDSewingDailyDesktop
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
        'olbqc
    End Sub

    Private Sub LCDWarehouseDash_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized


        BandedGridView2.OptionsSelection.EnableAppearanceFocusedRow = True
        BandedGridView2.OptionsSelection.EnableAppearanceHideSelection = True
        BandedGridView2.OptionsSelection.EnableAppearanceFocusedCell = False

        BandedGridView2.OptionsView.ShowAutoFilterRow = False
        BandedGridView2.OptionsSelection.MultiSelect = False
        BandedGridView2.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect

        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In BandedGridView2.Columns
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
        Dim _Cmd As String = ""
        Dim _lbljoballQty As Integer = 0
        Dim _lbljobDoneQty As Integer = 0
        Dim _lbljobBalQty As Integer = 0
        Dim _lblsewAllQty As Integer = 0
        Dim _lblsewDoneQty As Integer = 0
        Dim _per As Double = 0.00

        Try
            If Me.InvokeRequired Then
                Me.Invoke(New DelegateLoadTime(AddressOf LoadData), New Object() {})
            Else

                Dim RCount As Integer = 0
                Try

                    _Cmd = " exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.USP_GETDATASEWING_DASH '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserLogInComputer) & "'," & Val(HI.ST.SysInfo.CmpID)
                    Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                    Me.ogc.DataSource = dt.Copy

                    RCount = dt.Rows.Count
                    For Each row As DataRow In dt.Rows
                        _lbljoballQty += row("FNGrandQuantity")
                        _lbljobDoneQty += row("FNTotalFinishQuantity")
                        _lbljobBalQty += row("FNWIPQuantity")
                        _lblsewAllQty += row("FNTarget")
                        _lblsewDoneQty += row("FNFinishQuantity")
                        _per += row("FNDailyPer")
                    Next row

                    dt.Dispose()
                Catch ex As Exception
                End Try

                Me.ottime.Enabled = (RCount <= 0)
                Timer1.Enabled = True
                lbljoballQty.Text = _lbljoballQty.ToString("N0")
                'BandedGridView1.GetDataRow(Rowshow).Item("FNGrandQuantity") '_tota.ToString("N0")lQty
                lbljobDoneQty.Text = _lbljobDoneQty.ToString("N0")
                'BandedGridView1.GetDataRow(Rowshow).Item("FNTotalFinishQuantity") '_totalFinish
                lbljobBalQty.Text = _lbljobBalQty.ToString("N0")
                ' BandedGridView1.GetDataRow(Rowshow).Item("FNWIPQuantity") '_totalWIP
                lblsewAllQty.Text = _lblsewAllQty.ToString("N0")
                'BandedGridView1.GetDataRow(Rowshow).Item("FNTarget")
                lblsewDoneQty.Text = _lblsewDoneQty.ToString("N0")
                'BandedGridView1.GetDataRow(Rowshow).Item("FNFinishQuantity")
                '_per = If(_lblsewAllQty > 0, (_lblsewDoneQty / _lblsewAllQty) / 100, 0.00) 'BandedGridView1.GetDataRow(Rowshow).Item("FNDailyPer")
                If (RCount > 0) Then
                    lblsewBalQty.Text = (_per / RCount).ToString("N2")
                Else
                    lblsewBalQty.Text = "0.00"
                End If
            End If
        Catch ex As Exception
        End Try


    End Sub


    Private Rowshow As Integer = 0
    Private Sub ottime_Tick(sender As Object, e As EventArgs) Handles ottime.Tick
        Try
            ottime.Enabled = False

            Dim mTheard As New Threading.Thread(AddressOf LoadData)
            mTheard.Start()

        Catch ex As Exception
            ottime.Enabled = True
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            'Dim _Cmd As String = ""
            'Dim _Step As Integer = 1

            If Rowshow < BandedGridView2.RowCount Then
                Rowshow += 1

                BandedGridView2.OptionsNavigation.AutoMoveRowFocus = True
                Me.BandedGridView2.FocusedRowHandle = Rowshow
                Me.BandedGridView2.RefreshRow(Rowshow)

            Else
                Timer1.Enabled = False
                Rowshow = 0
                Dim mTheard As New Threading.Thread(AddressOf LoadData)
                mTheard.Start()

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub lbltotalpo_KeyDown(sender As Object, e As KeyEventArgs)
        'Me.Close()
    End Sub

    Private Sub ogc_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            'Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LCDCuttingDaily_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Try

            Dim stdw As Integer = 1460
            Dim stdh As Integer = 900
            Dim HStdF As Integer = 23
            Dim HStdFLine As Integer = 18

            Dim wWith As Integer = Me.Width

            HStdF = (wWith * HStdF) / stdw
            HStdFLine = (wWith * HStdFLine) / stdw

            BandedGridView2.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True
            BandedGridView2.Appearance.HeaderPanel.Font = New Font("Tahoma", HStdF)
            BandedGridView2.Appearance.Row.Font = New Font("Tahoma", HStdFLine)
            BandedGridView2.Appearance.FocusedRow.Font = New Font("Tahoma", HStdFLine)

            lblsewAll.Width = ((wWith * 20) / 100)
            lblsewDone.Width = ((wWith * 20) / 100)
            lblsewBal.Width = ((wWith * 20) / 100)
            lblsewAllQty.Width = ((wWith * 13) / 100)
            lblsewDoneQty.Width = ((wWith * 13) / 100)
            lblsewBalQty.Width = ((wWith * 13) / 100)
            lbljoball.Width = ((wWith * 20) / 100)
            lbljobDone.Width = ((wWith * 20) / 100)
            lbljobBal.Width = ((wWith * 20) / 100)
            lbljoballQty.Width = ((wWith * 13) / 100)
            lbljobDoneQty.Width = ((wWith * 13) / 100)
            lbljobBalQty.Width = ((wWith * 13) / 100)

            'With Me.BandedGridView2
            '    .Columns.ColumnByFieldName("FDShipDate").Width = 50 ' ((wWith * 10) / 100)
            '    .Columns.ColumnByFieldName("ConfirmBook").Width = 50 '((wWith * 10) / 100)
            '    .Columns.ColumnByFieldName("FTOrderNo").Width = 50 '((wWith * 10) / 100)
            '    .Columns.ColumnByFieldName("FTStyleCode").Width = 50 '((wWith * 10) / 100)
            '    .Columns.ColumnByFieldName("Item").Width = 50 '((wWith * 10) / 100)
            '    .Columns.ColumnByFieldName("FNGrandQuantity").Width = 150 ' ((wWith * 10) / 100)
            '    .Columns.ColumnByFieldName("FNWIPQuantity").Width = 150 '((wWith * 10) / 100)
            '    .Columns.ColumnByFieldName("FNTarget").Width = 150 ' ((wWith * 10) / 100)
            '    .Columns.ColumnByFieldName("FNFinishQuantity").Width = 150 ' ((wWith * 10) / 100)
            '    .Columns.ColumnByFieldName("FNTotalFinishQuantity").Width = 150 '((wWith * 10) / 100)
            '    .Columns.ColumnByFieldName("FNDailyPer").Width = 150 '((wWith * 10) / 100)

            'End With

        Catch ex As Exception
        End Try
    End Sub
    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs)
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub


    Private Sub ogc_KeyDown_1(sender As Object, e As KeyEventArgs) Handles ogc.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Sub BandedGridView2_RowStyle(sender As Object, e As RowStyleEventArgs) Handles BandedGridView2.RowStyle
        Try
            Try
                If Val(BandedGridView2.GetRowCellValue(e.RowHandle, "FNWIPQuantity").ToString) <= 0 Then

                    e.Appearance.ForeColor = Color.Lime
                    e.HighPriority = True

                Else

                    Dim pTotalDay As Integer = Val(BandedGridView2.GetRowCellValue(e.RowHandle, "FNTotalDay").ToString)

                    Select Case True
                        Case (pTotalDay <= 1)
                            e.Appearance.ForeColor = Color.Red
                            e.HighPriority = True

                        Case (pTotalDay > 1 And pTotalDay <= 3)
                            e.Appearance.ForeColor = Color.Gold
                        Case Else

                    End Select

                End If
            Catch ex As Exception

            End Try
        Catch ex As Exception
        End Try
    End Sub

#Region "Property"

#End Region

End Class