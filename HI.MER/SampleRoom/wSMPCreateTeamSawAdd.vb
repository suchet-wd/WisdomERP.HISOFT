Imports System.Drawing
Imports DevExpress.XtraEditors.Controls

Public Class wSMPCreateTeamSawAdd


    Private _StateSumGrid As Boolean = False
    Private _StateGridSelect As Boolean = False

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.



    End Sub

#Region "Property"

    Private _JobProdNo As String = ""
    Property JobProdNo As String
        Get
            Return _JobProdNo
        End Get
        Set(value As String)
            _JobProdNo = value
        End Set
    End Property

    Private _OrderNo As String = ""
    Property OrderNo As String
        Get
            Return _OrderNo
        End Get
        Set(value As String)
            _OrderNo = value
        End Set
    End Property

    Private _Process As Boolean = False
    Property Process As Boolean
        Get
            Return _Process
        End Get
        Set(value As Boolean)
            _Process = value
        End Set
    End Property
#End Region

#Region "Procedure"

    Private Sub InitGrid()

        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvBreakdown.Columns

            GridCol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False

        Next


        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvBreakdownbal.Columns

            GridCol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False

        Next

        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvBreakdownprod.Columns

            GridCol.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False

        Next

        With ogvBreakdown
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
            '.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
            .OptionsMenu.EnableColumnMenu = False
            .OptionsMenu.ShowAutoFilterRowItem = False
            .OptionsFilter.AllowFilterEditor = False
            .OptionsFilter.AllowColumnMRUFilterList = False
            .OptionsFilter.AllowMRUFilterList = False
        End With

        With ogvBreakdownbal
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsMenu.EnableColumnMenu = False
            .OptionsMenu.ShowAutoFilterRowItem = False
            .OptionsFilter.AllowFilterEditor = False
            .OptionsFilter.AllowColumnMRUFilterList = False
            .OptionsFilter.AllowMRUFilterList = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        End With

        With ogvBreakdownprod
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsMenu.EnableColumnMenu = False
            .OptionsMenu.ShowAutoFilterRowItem = False
            .OptionsFilter.AllowFilterEditor = False
            .OptionsFilter.AllowColumnMRUFilterList = False
            .OptionsFilter.AllowMRUFilterList = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        End With



    End Sub

    Private Sub InitData()
        ogdBreakdown.DataSource = Nothing
        ogdBreakdownbal.DataSource = Nothing
        ogdBreakdownprod.DataSource = Nothing


        Dim cmd As String = ""
        Dim _dt As DataTable
        Dim _dtSub As DataTable
        Dim _dtbal As DataTable
        Dim _dtProd As DataTable
        Dim _Total As Integer = 0

        cmd = "  Select  B.FTSizeBreakDown"
        cmd &= vbCrLf & "  ,ISNULL(B.FNQuantity,0) As FNQuantity "
        cmd &= vbCrLf & " ,ISNULL(B.FTColorway,'') AS FTColorway"
        cmd &= vbCrLf & " ,Case When ISDATE(ISNULL(B.FTDeliveryDate,'')) = 1 THEN  Convert(nvarchar(10),Convert(Datetime,B.FTDeliveryDate),103) ELSE '' END AS FTDeliveryDate"
        cmd &= vbCrLf & " ,ISNULL(B.FTRemark,'') AS FTRemark,A.FNSeq"
        cmd &= vbCrLf & " FROM (SELECT '1' AS FTSelect ,FTSizeBreakDown,FTColorway,FNQuantity,FTDeliveryDate,FTRemark"
        cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "]..TSMPOrder_Breakdown AS X2 WITH(NOLOCK)"
        cmd &= vbCrLf & " Where X2.FTSMPOrderNo ='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "'"
        cmd &= vbCrLf & ") AS B"
        cmd &= vbCrLf & "OUTER APPLY (SELECT TOP 1 X32.FNMatSizeSeq AS FNSeq From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMMatSize AS X32 WITH(NOLOCK) WHERE X32.FTMatSizeCode=B.FTSizeBreakDown ) AS A "

        cmd &= vbCrLf & " ORDER BY A.FNSeq "

        _dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SAMPLE)
        _dtbal = _dt.Copy


        cmd = "  Select  FTSMPOrderNo, FTColorway, FTSizeBreakDown, SUM(FNQuantity) As FNQuantity  "
        cmd &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "]..TSMPSampleTeamBreakdown AS X WITH(NOLOCK) "
        cmd &= vbCrLf & " WHERE (FTSMPOrderNo ='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "') "
        cmd &= vbCrLf & "  Group By FTSMPOrderNo, FTColorway, FTSizeBreakDown "

        _dtSub = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SAMPLE)

        For Each R As DataRow In _dtSub.Rows

            _dtbal.BeginInit()
            For Each Rx As DataRow In _dtbal.Select("FTColorway='" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "'", "FNSeq")

                If Val(R!FNQuantity) >= Val(Rx!FNQuantity) Then
                    Rx!FNQuantity = 0
                Else
                    Rx!FNQuantity = Val(Rx!FNQuantity) - Val(R!FNQuantity)
                End If


                Exit For
            Next
            _dtbal.EndInit()


        Next

        _dtSub.Dispose()

        _dtProd = _dtbal.Copy

        ogdBreakdown.DataSource = _dt.Copy
        ogdBreakdownbal.DataSource = _dtbal.Copy
        ogdBreakdownprod.DataSource = _dtProd.Copy

    End Sub



#End Region

#Region "Function"

    Private Function CreateNewJobProducttion() As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Generating Job Production...Please Wait")

        Dim _Qry As String = ""
        Dim _OrderNo As String = ""
        Dim _SubOrderNo As String = ""
        Dim _ColorWay As String = ""
        Dim _dtjobprod As DataTable
        Dim _tmpOrderProd As String = ""

        Dim I As Integer = 0
        Try



            _Qry = "   SELECT  TOP 1 FTTeam"
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam"
            _Qry &= vbCrLf & "  WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "'  "
            _Qry &= vbCrLf & "  ORDER BY FTTeam DESC "
            _tmpOrderProd = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SAMPLE, "")

            If _tmpOrderProd = "" Then
                _tmpOrderProd = Me.OrderNo & "-T01"
            Else
                _tmpOrderProd = Me.OrderNo & "-T" & Microsoft.VisualBasic.Right("000" & Format(Val(Microsoft.VisualBasic.Right(_tmpOrderProd, 2)) + 1, "0"), 2)
            End If


            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SAMPLE)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            _Qry = "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam "
            _Qry &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo, FTTeam, FTRemartk)"
            _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "' "
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderProd) & "' "
            _Qry &= vbCrLf & ",''"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                _Spls.Close()
                Return False
            End If

            Dim dtprod As DataTable

            With CType(ogdBreakdownprod.DataSource, DataTable)
                .AcceptChanges()
                dtprod = .Copy
            End With

            _Qry = "  DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeamBreakdown WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "' AND FTTeam='" & HI.UL.ULF.rpQuoted(_tmpOrderProd) & "' "
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            For Each R As DataRow In dtprod.Select("FNQuantity > 0", "FNSeq")

                _Qry = "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeamBreakdown "
                _Qry &= vbCrLf & "   (FTInsUser, FDInsDate, FTInsTime,FTSMPOrderNo, FTTeam, FTColorway, FTSizeBreakDown, FNSeq, FNQuantity)"
                _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.OrderNo) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpOrderProd) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTColorway.ToString) & "' "
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString) & "' "
                _Qry &= vbCrLf & "," & Val(R!FNSeq) & ""
                _Qry &= vbCrLf & "," & Val(R!FNQuantity) & ""

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    Return False
                End If


            Next

            dtprod.Dispose()

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            _Spls.Close()

            Return False
        End Try

        _Spls.Close()
        Return True
    End Function


#End Region

    Private Sub wGenerateJobProd_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim _Spls As New HI.TL.SplashScreen("Loading...Please Wait")

        Call InitGrid()
        Call InitData()
        _Spls.Close()
    End Sub


    Private Sub ocmclose_Click(sender As Object, e As EventArgs) Handles ocmclose.Click
        Me.Process = False
        Me.Close()
    End Sub


    Private Sub ocmcreate_Click(sender As Object, e As EventArgs) Handles ocmcreate.Click

        CType(Me.ogdBreakdownbal.DataSource, DataTable).AcceptChanges()
        CType(Me.ogdBreakdownprod.DataSource, DataTable).AcceptChanges()


        Dim dtprod As DataTable

        With CType(ogdBreakdownprod.DataSource, DataTable)
            .AcceptChanges()
            dtprod = .Copy
        End With

        Try
            If dtprod.Select("FNQuantity > 0").Length > 0 Then
                If Me.JobProdNo = "" Then

                    If Me.CreateNewJobProducttion Then
                        HI.MG.ShowMsg.mInfo("Generate Job Producttion Complete !!!", 1404110001, Me.Text)
                        Me.Process = True
                        Me.Close()
                    Else
                        HI.MG.ShowMsg.mInfo("Can not Generate Job Producttion, Fail !!!", 1404110002, Me.Text)
                    End If

                End If
            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลจำนวน กรุณาทำการตรวจสอบ !!!", 1809190045, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)

            End If


        Catch ex As Exception

        End Try




    End Sub

    Private Sub RepoC3FNQuantity_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepoC3FNQuantity.EditValueChanging
        Try
            If Val(e.NewValue) >= 0 Then

                Dim OrderSize As String = ""
                Dim OrderColor As String = ""

                With ogvBreakdownprod
                    OrderSize = .GetFocusedRowCellValue("FTSizeBreakDown").ToString
                    OrderColor = .GetFocusedRowCellValue("FTColorway").ToString
                End With

                Dim dtbal As DataTable

                With CType(ogdBreakdownbal.DataSource, DataTable)
                    .AcceptChanges()
                    dtbal = .Copy
                End With

                Dim BalQty As Integer = 0

                For Each R As DataRow In dtbal.Select("FTColorway='" & HI.UL.ULF.rpQuoted(OrderColor) & "' AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(OrderSize) & "'", "FNSeq")
                    BalQty = Val(R!FNQuantity)

                    Exit For
                Next
                dtbal.Dispose()

                If Val(e.NewValue) <= BalQty Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If

            Else

                    e.Cancel = True
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class