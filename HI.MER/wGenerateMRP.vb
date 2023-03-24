Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ButtonEdit
Imports DevExpress.XtraGrid.Filter
Imports DevExpress.XtraPrinting

Imports System.Data.Common
Imports System.Windows.Forms
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Drawing
Imports DevExpress.Data.Filtering


Public Class wGenerateMRP


    Dim View As GridView
    Dim RowsIndex As Double
    Dim TopVisibleIndex As Int32
    Private sFNHSysStyleId As String

    ''' Used Data Adapter to control database

    Dim oleDbDataAdapter1 As DbDataAdapter
    Dim oleDbDataAdapter2 As DbDataAdapter
    Dim dtStyleDetail As DataTable


    Private Enum TabIndexs As Integer
        StyleDetail = 0
        Colorway = 1
        SizeBreakdown = 2
    End Enum

#Region "Handler Control"

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        With RepositoryFTMainMatCode
            AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
            AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
            AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave

        End With

    End Sub
#End Region

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private Sub wGenerateMRP_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            View = New GridView()
            View = ogcmatcode.Views(0)
            View.FocusedRowHandle = 0

            ogcmatcode.ViewCollection.Add(View)
            ogcmatcode.MainView = View

            View.GridControl = ogcmatcode
            View.OptionsView.ShowAutoFilterRow = False
            View.OptionsView.NewItemRowPosition = NewItemRowPosition.None
            View.OptionsNavigation.AutoFocusNewRow = True
            View.OptionsBehavior.AllowAddRows = True
            View.OptionsBehavior.AllowDeleteRows = True
            View.OptionsBehavior.Editable = True
            View.BestFitColumns()

            FTUpdUser.Text = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)
            FDUpdDate.Text = "??/??/????"
            FTUpdTime.Text = "??:??:??"

            sFNHSysStyleId = ""

        Catch ex As Exception
        End Try
    End Sub

#Region "MAIN PROC"

    Private Sub Proc_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub FNHSysBuyId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysBuyId.EditValueChanged
        Dim _Str As String = ""
        FNHSysStyleId.Text = ""
        FNHSysStyleId.Properties.Tag = ""

        ogcmatcode.DataSource = Nothing
        GridOrderList.DataSource = Nothing
        GridCalculated.DataSource = Nothing

        _Str = "SELECT TOP 1 FNHSysBuyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy WITH(NOLOCK) WHERE FTBuyCode ='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' "
        FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")

        Call LoadOrderInfo(Val(FNHSysBuyId.Properties.Tag.ToString), Val(FNHSysStyleId.Properties.Tag.ToString))
        Call LoadBOMInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
        Call LoadMPRInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysStyleId.EditValueChanged
        Dim _Str As String = ""
        If FNHSysBuyId.Text <> "" Then
            _Str = "SELECT TOP 1 FNHSysBuyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy WITH(NOLOCK) WHERE FTBuyCode ='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' "
            FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
        End If

        FNHSysStyleId.Properties.Tag = ""

        If FNHSysStyleId.Text <> "" Then
            _Str = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
            FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
            If FNHSysStyleId.Properties.Tag.ToString <> "" Then
                Call LoadOrderInfo(Val(FNHSysBuyId.Properties.Tag.ToString), Val(FNHSysStyleId.Properties.Tag.ToString))
                'If FTOrderSeq.Properties.Tag.ToString <> "" Then
                Call LoadBOMInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
                Call LoadMPRInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
                'Else
                '    Call LoadInformation(FNHSysStyleId.Properties.Tag.ToString, -1)
                'End If
            Else
                FTUpdUser.Text = Nothing
                FDUpdDate.Text = Nothing
                FTUpdTime.Text = Nothing

                ogcmatcode.DataSource = Nothing
                ogcmatcode.Refresh()
            End If

            sFNHSysStyleId = FNHSysStyleId.Text
        Else

            Call LoadOrderInfo(Val(FNHSysBuyId.Properties.Tag.ToString), Val(FNHSysStyleId.Properties.Tag.ToString))
            Call LoadBOMInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
            'Call LoadMPRInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")

            ogcmatcode.Refresh()
        End If
    End Sub

    Private Sub GridView2_CellValueChanging(sender As System.Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView2.CellValueChanging
        Dim OrderNoArr As String = ""
        Dim dt As DataTable = CType(GridOrderList.DataSource, DataTable)

        If Not dt Is Nothing Then
            For Each r As DataRow In dt.Rows
                RowsIndex = dt.Rows.IndexOf(r)
                If e.Column.FieldName = "FNSelect" Then
                    If e.RowHandle = RowsIndex And e.Value = "1" Then
                        If OrderNoArr <> "" Then OrderNoArr += ","
                        OrderNoArr += r!FTOrderNo.ToString
                    ElseIf e.RowHandle <> RowsIndex And r!FNSelect.ToString = "1" Then
                        If OrderNoArr <> "" Then OrderNoArr += ","
                        OrderNoArr += r!FTOrderNo.ToString
                    End If
                ElseIf e.Column.FieldName = "FNStateSourcing" Then

                End If

            Next
        End If

        Call LoadBOMInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, OrderNoArr)
        Call LoadMPRInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, OrderNoArr)

        'GetBOMInfo()

    End Sub

    Private Sub GetBOMInfo(Optional ByVal Calculated = False)
        Dim OrderNo As String = GetOrderArr()

        Dim view As ColumnView = GridView1
        Dim FilterString As String = ""

        For Each colX As GridColumn In GridView1.Columns
            Try
                Dim xValue As String = view.ActiveFilter.Item(colX).Filter.Value
                'If xValue.Contains("*") Then
                FilterString += " AND " & colX.FieldName & " LIKE ('%" & Replace(xValue, "*", "%%%") & "%')" & vbCrLf
                'Else
                '    FilterString += " AND " & colX.FieldName & " = '" & xValue & "'" & vbCrLf
                'End If

            Catch ex As Exception
            End Try

        Next

        'MsgBox(FilterString)

        Call LoadBOMInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, OrderNo, Calculated, Calculated, FilterString)
        Call LoadMPRInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, OrderNo, Calculated, Calculated)

    End Sub

    Private Function GetOrderArr() As String
        Dim OrderNoArr As String = ""
        Dim dt As DataTable = CType(GridOrderList.DataSource, DataTable)

        Try
            If Not dt Is Nothing Then
                For Each r As DataRow In dt.Rows
                    If r!FNSelect.ToString = "1" Then
                        If OrderNoArr <> "" Then OrderNoArr += ","
                        OrderNoArr += r!FTOrderNo.ToString
                    End If
                Next
            End If

        Catch ex As Exception
            OrderNoArr = ""
        End Try

        Return OrderNoArr

    End Function

    Private Function GetOrderArrForReport() As String
        Dim OrderNoArr As String = ""
        Dim dt As DataTable = CType(GridOrderList.DataSource, DataTable)

        Try
            If Not dt Is Nothing Then
                For Each r As DataRow In dt.Rows
                    If r!FNSelect.ToString = "1" Then
                        If OrderNoArr <> "" Then OrderNoArr += " AND "
                        OrderNoArr += "{TMERTMPR.FTOrderNo} = '" & r!FTOrderNo.ToString & "'"
                    End If
                Next
            End If

        Catch ex As Exception
            OrderNoArr = ""
        End Try

        Return OrderNoArr

    End Function

    Private Sub LoadOrderInfo(ByVal _FNHSysBuyId As Integer, ByVal _FNHSysStyleId As Integer)
        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Dim sqlCmd As New SqlCommand
        sqlCmd.Connection = HI.Conn.SQLConn.Cnn
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_ORDERINFO]"
        sqlCmd.Parameters.AddWithValue("@FNHSysBuyId", _FNHSysBuyId)
        sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", _FNHSysStyleId)

        oleDbDataAdapter2 = New SqlClient.SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
        oleDbDataAdapter2.SelectCommand = sqlCmd
        dtStyleDetail = New DataTable()
        oleDbDataAdapter2.Fill(dtStyleDetail)

        Me.GridOrderList.DataSource = dtStyleDetail

        Dim view As GridView
        view = GridOrderList.Views(0)
        view.OptionsView.ShowAutoFilterRow = True

        Me.GridOrderList = view.GridControl
        Me.GridOrderList.Refresh()

    End Sub

    Private Sub LoadBOMInfo(ByVal _FNHSysBuyId As String, ByVal _FNHSysStyleId As String, ByVal _OrderNo As String, Optional ByVal Calculated As Boolean = False, Optional ByVal _Save2Table As Boolean = False, Optional _FilterString As String = "")
        If _FNHSysBuyId = "" Then _FNHSysBuyId = "0"
        If _FNHSysStyleId = "" Then _FNHSysStyleId = "0"

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Dim sqlCmd As New SqlCommand
        sqlCmd.Connection = HI.Conn.SQLConn.Cnn
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GETBREAKDOWN_CALCMPR]"
        sqlCmd.Parameters.AddWithValue("@FNHSysBuyId", _FNHSysBuyId)
        sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", _FNHSysStyleId)
        sqlCmd.Parameters.AddWithValue("@FTOrderNo", _OrderNo)
        sqlCmd.Parameters.AddWithValue("@StateSave", _Save2Table)
        sqlCmd.Parameters.AddWithValue("@UserName", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
        sqlCmd.Parameters.AddWithValue("@FilterString", _FilterString)

        Dim dtBomInfo As New DataTable
        Try
            Dim sqlDA As New SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
            sqlDA.SelectCommand = sqlCmd

            sqlDA.Fill(dtBomInfo)
        Catch ex As Exception
            'MsgBox(ex.Message)
            'Exit Sub
        End Try

        Me.ogcmatcode.DataSource = dtBomInfo

        Try
            If Calculated Then
                'Me.GridCalculated.DataSource = dtStyleDetail
                'Dim view2 As GridView

                'view2 = GridCalculated.Views(0)
                'view2.OptionsView.ShowAutoFilterRow = True
                'Me.GridCalculated = view2.GridControl
                'Me.GridCalculated.Refresh()

                If HI.ST.Lang.Language = ST.Lang.Lang.TH Then
                    If CreateNewRawMat(_OrderNo) = False Then MsgBox("ไม่สามารถสร้างรายการ วัตถุดิบใหม่ได้")
                    If CreateNewRawMatColor() = False Then MsgBox("ไม่สามารถสร้างรายการ สีวัตถุดิบใหม่ได้")
                    If CreateNewRawMatSize() = False Then MsgBox("ไม่สามารถสร้างรายการ ขนาดวัตถุดิบใหม่ได้")
                Else
                    If CreateNewRawMat(_OrderNo) = False Then MsgBox("Cannot create new material")
                    If CreateNewRawMatColor() = False Then MsgBox("Cannot create new color of material")
                    If CreateNewRawMatSize() = False Then MsgBox("Cannot create new size of material")
                End If

                FTUpdUser.Text = dtBomInfo.Rows(1)("FTUpdUser").ToString
                FDUpdDate.Text = dtBomInfo.Rows(1)("FDUpdDate").ToString
                FTUpdTime.Text = dtBomInfo.Rows(1)("FTUpdTime").ToString

            End If

        Catch ex As Exception

        End Try

        Dim view As GridView
        view = ogcmatcode.Views(0)
        view.OptionsView.ShowAutoFilterRow = True
        view.BestFitColumns()

        Me.ogcmatcode = view.GridControl
        Me.ogcmatcode.Refresh()

        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

    End Sub

    Private Sub LoadMPRInfo(ByVal _FNHSysBuyId As String, ByVal _FNHSysStyleId As String, ByVal _OrderNo As String, Optional ByVal Calculated As Boolean = False, Optional ByVal _Save2Table As Boolean = False)
        If _FNHSysBuyId = "" Then _FNHSysBuyId = "0"
        If _FNHSysStyleId = "" Then _FNHSysStyleId = "0"

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Dim sqlCmd As New SqlCommand
        sqlCmd.Connection = HI.Conn.SQLConn.Cnn
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GETBREAKDOWN_GETMPR]"
        sqlCmd.Parameters.AddWithValue("@FNHSysBuyId", _FNHSysBuyId)
        sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", _FNHSysStyleId)
        sqlCmd.Parameters.AddWithValue("@FTOrderNo", _OrderNo)
        sqlCmd.Parameters.AddWithValue("@StateSave", _Save2Table)
        sqlCmd.Parameters.AddWithValue("@UserName", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))

        Dim sqlDA As New SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
        sqlDA.SelectCommand = sqlCmd
        Dim dtMPRInfo As New DataTable
        Try
            sqlDA.Fill(dtMPRInfo)

            Me.GridCalculated.DataSource = dtMPRInfo
            Dim view2 As GridView

            view2 = GridCalculated.Views(0)
            view2.OptionsView.ShowAutoFilterRow = True
            view2.BestFitColumns()

            Me.GridCalculated = view2.GridControl
            'Me.GridCalculated.Refresh()

        Catch ex As Exception
        End Try

        If dtMPRInfo.Rows.Count >= 1 Then
            FTUpdUser.Text = dtMPRInfo.Rows(0)("FTUpdUser").ToString
            FDUpdDate.Text = dtMPRInfo.Rows(0)("FDUpdDate").ToString
            FTUpdTime.Text = dtMPRInfo.Rows(0)("FTUpdTime").ToString
        Else
            FTUpdUser.Text = ""
            FDUpdDate.Text = ""
            FTUpdTime.Text = ""
        End If

        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
    End Sub

    Private Function CreateNewRawMat(ByVal _OrderNo As String) As Boolean
        Dim ret As Boolean = True

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MASTER)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Try
            Dim _Str As String = ""
            Dim dt As DataTable = New DataTable()
            _Str = "SELECT DISTINCT [FNHSysRawMatId]" & _
                     ",[FTRawMatCode] " & _
                     ",[FTRawMatNameTH] " & _
                     ",[FTRawMatNameEN] " & _
                     ",[FNHSysRawMatColorId] " & _
                     ",[FNHSysRawMatSizeId] " & _
                     ",[FNHSysUnitId] " & _
                     ",[FTFabricFrontSize] " & _
                     ",[FTRemark] " & _
                     ",[FPImageRawMat] " & _
                     ",[FTStateActive]  " & vbCrLf & _
                     "FROM tempdb..TINVENMMaterial ORDER BY FTRawMatCode, FNHSysRawMatColorId, FNHSysRawMatSizeId"
            oleDbDataAdapter2 = New SqlClient.SqlDataAdapter(_Str, HI.Conn.SQLConn._ConnString)
            oleDbDataAdapter2.Fill(dt)

            For Each r As DataRow In dt.Rows
                Try
                    _Str = "SELECT DISTINCT FNHSysRawMatId, FTRawMatCode, FNHSysRawMatColorId, FNHSysRawMatSizeId FROM " & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TINVENMMaterial]"
                    _Str &= vbCrLf & "WHERE [FTRawMatCode] = '" & r!FTRawMatCode.ToString & "' "
                    _Str &= vbCrLf & "AND [FNHSysRawMatColorId] = " & r!FNHSysRawMatColorId.ToString & " "
                    _Str &= vbCrLf & "AND [FNHSysRawMatSizeId] = " & r!FNHSysRawMatSizeId.ToString & " "
                    Dim SelectCMD As SqlCommand = New SqlCommand(_Str, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                    Dim cnt As Integer
                    cnt = SelectCMD.ExecuteScalar

                    Dim GetSysRawmatID As Integer = 0

                    If cnt = 0 Then
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
                        GetSysRawmatID = HI.Conn.SQLConn.GetField("EXEC HITECH_SYSTEM.dbo.SP_GET_SYSID 'TINVENMMaterial', 'FNHSysRawMatId', 'HITECH_MASTER'", Conn.DB.DataBaseName.DB_SYSTEM)

                        _Str = "UPDATE TMERTMPR SET FNHSysRawMatId = " & GetSysRawmatID
                        _Str &= vbCrLf & "WHERE [FTMainMatCode] = '" & r!FTRawMatCode.ToString & "' "
                        _Str &= vbCrLf & "AND [FNHSysRawMatColorId] = " & r!FNHSysRawMatColorId.ToString & " "
                        _Str &= vbCrLf & "AND [FNHSysRawMatSizeId] = " & r!FNHSysRawMatSizeId.ToString & " "
                        _Str &= vbCrLf & "AND ISNULL([FNHSysRawMatId], 0) = 0"

                        HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                        If r!FTRawMatCode.ToString <> "" Then
                            _Str = "INSERT INTO " & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TINVENMMaterial]"
                            _Str &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Str &= vbCrLf & "," & GetSysRawmatID
                            _Str &= vbCrLf & "," & "'" & r!FTRawMatCode.ToString & "'"
                            _Str &= vbCrLf & "," & "'" & r!FTRawMatNameTH.ToString & "'"
                            _Str &= vbCrLf & "," & "'" & r!FTRawMatNameEN.ToString & "'"
                            _Str &= vbCrLf & "," & r!FNHSysRawMatColorId.ToString
                            _Str &= vbCrLf & "," & r!FNHSysRawMatSizeId.ToString
                            _Str &= vbCrLf & "," & r!FNHSysUnitId.ToString
                            _Str &= vbCrLf & "," & "'" & r!FTFabricFrontSize.ToString & "'"
                            _Str &= vbCrLf & "," & "'' "
                            _Str &= vbCrLf & "," & "'' "
                            _Str &= vbCrLf & "," & "'1'"

                            HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.Tran.Commit()
                        End If
                    Else
                        Try
                            GetSysRawmatID = cnt
                            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
                            _Str = "UPDATE " & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTMPR] SET FNHSysRawMatId = "
                            _Str &= vbCrLf & "(SELECT TOP 1 FNHSysRawMatId FROM HITECH_MASTER.dbo.TINVENMMaterial"
                            _Str &= vbCrLf & "WHERE [FTRawMatCode] = '" & r!FTRawMatCode.ToString & "' "
                            _Str &= vbCrLf & "AND [FNHSysRawMatColorId] = " & r!FNHSysRawMatColorId.ToString & " "
                            _Str &= vbCrLf & "AND [FNHSysRawMatSizeId] = " & r!FNHSysRawMatSizeId.ToString & " "
                            _Str &= ")"
                            _Str &= vbCrLf & "WHERE [FTMainMatCode] = '" & r!FTRawMatCode.ToString & "' "
                            _Str &= vbCrLf & "AND [FNHSysRawMatColorId] = " & r!FNHSysRawMatColorId.ToString & " "
                            _Str &= vbCrLf & "AND [FNHSysRawMatSizeId] = " & r!FNHSysRawMatSizeId.ToString & " "
                            _Str &= vbCrLf & "AND ISNULL([FNHSysRawMatId], 0) = 0"

                            HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.Tran.Commit()

                        Catch ex As Exception
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        End Try

                    End If

                    Generate_Resource(GetSysRawmatID, _OrderNo)

                Catch ex As Exception
                    'MsgBox(r!FTRawMatCode.ToString)
                End Try
            Next

            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            ret = False
        End Try

        Return ret
    End Function

    Private Function CreateNewRawMatColor() As Boolean
        Dim ret As Boolean = True

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MASTER)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Try
            Dim _Str As String = ""
            Dim dt As DataTable = New DataTable()
            _Str = "SELECT DISTINCT [FNHSysRawMatColorId]" & _
                     ",[FTRawMatColorCode] " & _
                     ",[FNRawMatColorSeq] " & _
                     ",[FTRawMatColorNameTH] " & _
                     ",[FTRawMatColorNameEN] " & _
                     ",[FTRemark] " & _
                     ",[FTStateActive] " & vbCrLf & _
                     "FROM tempdb..TINVENMMatColor ORDER BY [FNRawMatColorSeq], [FTRawMatColorCode]"
            oleDbDataAdapter2 = New SqlClient.SqlDataAdapter(_Str, HI.Conn.SQLConn._ConnString)
            oleDbDataAdapter2.Fill(dt)

            For Each r As DataRow In dt.Rows
                _Str = "SELECT DISTINCT FNHSysRawMatColorId, FTRawMatColorCode FROM " & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TINVENMMatColor]"
                _Str &= vbCrLf & "WHERE [FTRawMatColorCode] = '" & r!FTRawMatColorCode.ToString & "' "
                Dim SelectCMD As SqlCommand = New SqlCommand(_Str, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                Dim cnt As Integer
                cnt = SelectCMD.ExecuteScalar

                If cnt = 0 Then
                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
                    Dim GetSysRawmatID As Integer = HI.Conn.SQLConn.GetField("EXEC HITECH_SYSTEM.dbo.SP_GET_SYSID 'TINVENMMatColor', 'FNHSysRawMatColorId', 'HITECH_MASTER'", Conn.DB.DataBaseName.DB_SYSTEM)
                    If Val(r!FNHSysRawMatColorId) > 0 Then
                        _Str = "INSERT INTO " & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TINVENMMatColor]"
                        _Str &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & "," & GetSysRawmatID
                        _Str &= vbCrLf & "," & "'" & r!FTRawMatColorCode.ToString & "'"
                        _Str &= vbCrLf & "," & "'" & r!FNRawMatColorSeq.ToString & "'"
                        _Str &= vbCrLf & "," & "'" & r!FTRawMatColorNameTH.ToString & "'"
                        _Str &= vbCrLf & "," & "'" & r!FTRawMatColorNameEN.ToString & "'"
                        _Str &= vbCrLf & "," & "'' "
                        _Str &= vbCrLf & "," & "'1'"

                        HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.Tran.Commit()
                    End If
                End If

            Next

            'HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            ret = False
        End Try

        Return ret
    End Function

    Private Function CreateNewRawMatSize() As Boolean
        Dim ret As Boolean = True

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MASTER)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Try
            Dim _Str As String = ""
            Dim dt As DataTable = New DataTable()
            _Str = "SELECT DISTINCT [FNHSysRawMatSizeId]" & _
                     ",[FTRawMatSizeCode] " & _
                     ",ISNULL([FNRawMatSizeSeq], 0) AS [FNRawMatSizeSeq] " & _
                     ",[FTRawMatSizeNameTH] " & _
                     ",[FTRawMatSizeNameEN] " & _
                     ",[FTRemark] " & _
                     ",[FTStateActive] " & vbCrLf & _
                     "FROM tempdb..TINVENMMatSize ORDER BY [FNRawMatSizeSeq], [FTRawMatSizeCode]"
            oleDbDataAdapter2 = New SqlClient.SqlDataAdapter(_Str, HI.Conn.SQLConn._ConnString)
            oleDbDataAdapter2.Fill(dt)

            For Each r As DataRow In dt.Rows
                _Str = "SELECT DISTINCT FNHSysRawMatSizeId, FTRawMatSizeCode FROM " & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TINVENMMatSize]"
                _Str &= vbCrLf & "WHERE [FTRawMatSizeCode] = '" & r!FTRawMatSizeCode.ToString & "' "
                Dim SelectCMD As SqlCommand = New SqlCommand(_Str, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                Dim cnt As Integer
                cnt = SelectCMD.ExecuteScalar

                If cnt = 0 Then
                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
                    Dim GetSysRawmatID As Integer = HI.Conn.SQLConn.GetField("EXEC HITECH_SYSTEM.dbo.SP_GET_SYSID 'TINVENMMatSize', 'FNHSysRawMatSizeId', 'HITECH_MASTER'", Conn.DB.DataBaseName.DB_SYSTEM)
                    If Val(r!FNHSysRawMatSizeId) > 0 Then
                        _Str = "INSERT INTO " & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TINVENMMatSize]"
                        _Str &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & "," & GetSysRawmatID
                        _Str &= vbCrLf & "," & "'" & r!FTRawMatSizeCode.ToString & "'"
                        _Str &= vbCrLf & "," & "'" & r!FNRawMatSizeSeq.ToString & "'"
                        _Str &= vbCrLf & "," & "'" & r!FTRawMatSizeNameTH.ToString & "'"
                        _Str &= vbCrLf & "," & "'" & r!FTRawMatSizeNameEN.ToString & "'"
                        _Str &= vbCrLf & "," & "'' "
                        _Str &= vbCrLf & "," & "'1'"

                        HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.Tran.Commit()
                    End If
                End If

            Next

            'HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            ret = False
        End Try

        Return ret
    End Function

    Private Function Generate_Resource(ByVal _FNHSysRawmatID As Integer, ByVal _OrderNo As String) As Boolean
        Dim ret As Boolean = True

        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

            Dim sqlCmd As New SqlCommand
            sqlCmd.Connection = HI.Conn.SQLConn.Cnn
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GENERATE_RESOURCE]"
            sqlCmd.Parameters.AddWithValue("@FNHSysRawMatId", _FNHSysRawmatID)
            sqlCmd.Parameters.AddWithValue("@FTOrderNo", _OrderNo)
            sqlCmd.Parameters.AddWithValue("@UserName", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))

            Dim sqlDA As New SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
            sqlDA.SelectCommand = sqlCmd
            sqlDA.SelectCommand.ExecuteNonQuery()

        Catch ex As Exception

            ret = False
        End Try

        Return ret
    End Function

    Private Sub ocmcalcmrp_Click(sender As System.Object, e As System.EventArgs) Handles ocmcalc.Click
        'If Me.VerifyData() Then

        If True Then
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Me.ProcComplete = True
            Call GetBOMInfo(True)
            Me.otb.SelectedTabPageIndex = 1
        Else
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        End If
        'End If
    End Sub

    Private Sub ocmclear1_Click(sender As System.Object, e As System.EventArgs) Handles ocmclearclsr.Click
        Me.ogcmatcode.DataSource = Nothing
        Me.GridCalculated.DataSource = Nothing
        Me.GridOrderList.DataSource = Nothing

        Dim xCol As Integer = 0
        Dim Idx As Integer = 0
        Try
            Dim View1 As GridView
            Dim View2 As GridView

            View1 = Me.GridCalculated.Views(0)
            View1.BestFitColumns()

            View2 = Me.GridOrderList.Views(0)
            View2.BestFitColumns()

            HI.TL.HandlerControl.ClearControl(Me)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmrefresh.Click
        Dim _Str As String = ""

        ogcmatcode.DataSource = Nothing
        GridOrderList.DataSource = Nothing
        GridCalculated.DataSource = Nothing

        If FNHSysBuyId.Text <> "" Then
            _Str = "SELECT TOP 1 FNHSysBuyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy WITH(NOLOCK) WHERE FTBuyCode ='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' "
            FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
        End If
        Call LoadOrderInfo(Val(FNHSysBuyId.Properties.Tag.ToString), Val(FNHSysStyleId.Properties.Tag.ToString))
        Call LoadBOMInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
        Call LoadMPRInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
    End Sub

    Private Sub ocmdelete_Click(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click
        If CType(ogcmatcode.DataSource, DataTable).Rows.Count > 0 Then
            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text) = True Then
                Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
                Dim OrderNo As String = GetOrderArr()
                If Me.DeleteAllData(_Spls, FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, OrderNo, True) Then
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    HI.TL.HandlerControl.ClearControl(Me)

                    Me.GridCalculated.DataSource = Nothing
                    Me.GridCalculated.Refresh()
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If
        End If
    End Sub

    Private Function SaveData() As Boolean
        Dim ret As Boolean = True
        Dim _Str As String = ""
        Return ret
        Try

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try

                Dim tSeqNo As Integer

                _Str = "SELECT TOP 1 FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FNHSysCustId, FNHSysSeasonId, FTStateActive FROM " & _
                    "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle] WHERE FNHSysStyleId = " & Val(Me.FNHSysBuyId.Properties.Tag.ToString) & " "
                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    ''Insert Header
                    Dim SelectCMD As SqlCommand = New SqlCommand(_Str, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                    'SelectCMD.Parameters.AddWithValue("@FNHSysStyleId", (Val(Me.FNHSysStyleId.Properties.Tag.ToString))

                    Dim cnt As Integer
                    cnt = SelectCMD.ExecuteScalar

                    If cnt = 0 Then
                        _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle] " & _
                            "(FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FNHSysCustId, FNHSysSeasonId, FTStateActive, "
                        _Str &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime) "

                        _Str &= vbCrLf & " SELECT " & Val(Me.FNHSysStyleId.Properties.Tag.ToString) & ", '" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text)
                        _Str &= vbCrLf & ", N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Str &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB & ""
                        _Str &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB & ""
                        _Str &= vbCrLf & ", N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Str &= vbCrLf & ", " & HI.UL.ULDate.FormatDateDB & ""
                        _Str &= vbCrLf & ", " & HI.UL.ULDate.FormatTimeDB

                        If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                    Else
                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle] SET " & _
                        "FNHSysStyleId = '" & Val(Me.FNHSysStyleId.Properties.Tag.ToString) & "', " & _
                        "FTStyleCode = '" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "', " & _
                        "FTStateActive = '" & 1 & "', " & _
                        "FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', " & _
                        "FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ", " & _
                        "FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & "" & _
                        "WHERE FNHSysStyleId = " & Val(Me.FNHSysStyleId.Properties.Tag.ToString)

                        If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                    End If

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    ''Insert & Update Detail
                    ret = UpdateDatasource()

                End If

                Return ret

            Catch ex As Exception

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False

            End Try

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub GridView3_PopupMenuShowing(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView3.PopupMenuShowing
        'viewContextMenuStrip.Show(MousePosition)
    End Sub

    Private Sub viewContextMenuStrip_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles viewContextMenuStrip.ItemClicked
        If MsgBox("Do you want to delete selected rows?", MsgBoxStyle.YesNo, "Comfirm Delete") = MsgBoxResult.No Then Return
        Dim OrderNo As String = GetOrderArr()
        Dim SplScreen As New HI.TL.SplashScreen()

        For Each i As Integer In GridView3.GetSelectedRows
            Dim row As DataRow = GridView3.GetDataRow(i)
            SplScreen.Text = "Delete (" & i + 1 & ") Item " & row("FTMainMatCode").ToString & " Color " & row("FTMatColorCode").ToString & " Size " & row("FTMatSizeCode").ToString
            DeleteAllData(SplScreen, FNHSysBuyId.Properties.Tag.ToString,
                          row("FNHSysStyleId").ToString,
                          row("FTOrderNo").ToString,
                          False,
                          row("FNHSysRawMatId").ToString,
                          row("FNSeq").ToString,
                          row("FNMerMatSeq").ToString,
                          row("FNHSysMatColorId").ToString,
                          row("FNMatColorSeq").ToString,
                          row("FNHSysMatSizeId").ToString,
                          row("FNMatSizeSeq").ToString)

            'MessageBox.Show(row("FNSeq").ToString & " " & row("FNMerMatSeq").ToString & ", " & row("FTMainMatCode").ToString & ", " &
            '                row("FNHSysMatColorId").ToString & ", " & row("FNMatColorSeq").ToString & ", " &
            '                row("FNHSysMatSizeId").ToString & ", " & row("FNMatSizeSeq").ToString)

        Next

        SplScreen.Dispose()

        Me.GridCalculated.Refresh()
        Call LoadMPRInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
    End Sub

    Private Sub ocmpreview_Click(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click
        Dim currentCursor As Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        ''** DevExpress Report
        ' GridCalculated.ShowPrintPreview()
        'GridCalculated.ShowRibbonPrintPreview()

        ''** Crystal Report
        ' Dim strReportPath As String = Application.StartupPath & "\Reports\Merchandise Report\Mrpreport.rpt"
        GenCrystalReport("Merchandise Report\", "Mrpreport.rpt")

        Cursor.Current = currentCursor
    End Sub

    Private Sub GridView1_PrintInitialize(sender As System.Object, _
            e As DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs) _
            Handles GridView1.PrintInitialize
        Dim pb As PrintingSystemBase = CType(e.PrintingSystem, PrintingSystemBase)
        pb.PageSettings.Landscape = True
    End Sub

    Private Sub GenCrystalReport(ByVal strReportPath As String, ByVal strReportName As String)

        Dim dataView1 As DataView
        Dim op As CriteriaOperator = GridView3.ActiveFilterCriteria
        Dim filterString As String = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(op)

        dataView1 = GridView3.DataSource
        dataView1.RowFilter = filterString
        'Dim dt As DataTable = CType(GridCalculated.DataSource, DataTable)

        'Dim ds As DataSet = New DataSet()
        'Dim dt As DataTable = New DataTable()
        'dt = dataView1.ToTable("Table")
        'ds.Tables.Add(dt)

        '' Check file exists
        If Not IO.File.Exists(Application.StartupPath & "\Reports\" & strReportPath & strReportName) Then
            'Throw (New Exception("Unable to locate report file:" & strReportName))
            MsgBox("Unable to locate report file: " & strReportName, vbOKOnly + MsgBoxStyle.Exclamation, "Warning")
            Exit Sub
        End If

        ''Assign the datasource and set the properties for Report viewer
        'Dim rptDocument As New CrystalDecisions.CrystalReports.Engine.ReportDocument

        'rptDocument.Load(strReportPath)
        'rptDocument.SetDataSource(ds)

        'Dim objForm As New wPreviewReport
        ''objForm.CrystalReportViewer1.ReportSource = strReportPath
        ''objForm.Show()

        'objForm.CrystalReportViewer1.ShowRefreshButton = False
        'objForm.CrystalReportViewer1.ShowCloseButton = False
        'objForm.CrystalReportViewer1.ShowGroupTreeButton = False
        'objForm.CrystalReportViewer1.ReportSource = rptDocument
        'objForm.Show()

        Dim strFoumalar As String = ""
        If Val(FNHSysBuyId.Properties.Tag.ToString()) > 0 Then
            If strFoumalar <> "" Then strFoumalar += " AND "
            strFoumalar += "{TMERTMPR.FNHSysBuyId} = " & FNHSysBuyId.Properties.Tag.ToString() & ""
        End If
        If Val(FNHSysStyleId.Properties.Tag.ToString()) > 0 Then
            If strFoumalar <> "" Then strFoumalar += " AND "
            REM 2014/05/15 strFoumalar += "{TMERTMPR.FNHSysBuyId} = " & Val(FNHSysBuyId.Properties.Tag.ToString()) & ""
            strFoumalar += "{TMERTMPR.FNHSysStyleId} = " & Val(FNHSysStyleId.Properties.Tag.ToString()) & ""
        End If

        If GetOrderArrForReport() <> "" Then
            If strFoumalar <> "" Then strFoumalar += " AND "
            strFoumalar += GetOrderArrForReport()
        End If

        If filterString.Trim() <> "" Then
            If strFoumalar <> "" Then
                strFoumalar += " AND " & Replace(Replace(Replace(filterString, "[", "{TMERTMPR."), "]", "}"), "%", "*")
            Else
                strFoumalar = Replace(Replace(Replace(filterString, "[", "{TMERTMPR."), "]", "}"), "%", "*")
            End If
        End If

        With New HI.RP.Report
            .ReportFolderName = strReportPath
            .ReportName = strReportName
            .Formular = strFoumalar
            .Preview()
        End With

    End Sub

#End Region

#Region "Insert & Update Process"

    Private Function UpdateDatasource() As Boolean
        Dim ret As Boolean
        'Save the latest changes to the bound DataTable 
        View = Me.ogcmatcode.Views(0)
        View.ClearSorting()
        View.Columns("FNMerMatSeq").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

        Dim dt1 As DataTable = CType(ogcmatcode.DataSource, DataTable)
        'If Not (View.PostEditor() And View.UpdateCurrentRow()) Then Return False

        ' Update Style detail.
        If IsNothing(dt1) Then
            ret = True
        Else
            If (FNHSysBuyId.Properties.Tag <> "") Then
                Call LoadBOMInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
            End If

        End If

        Return ret

    End Function

    Private Function DoUpdateTable(ByVal dataAdapter As SqlDataAdapter, _
        ByVal dataTable As System.Data.DataTable) As Boolean
        Dim InsertSql As String = ""

        Dim dt As DataTable
        dt = dataTable 'CType(ogcmatcode.DataSource, DataTable)

        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            dataAdapter = CreateAdapter(HI.Conn.SQLConn.Cnn)
            InsertSql = dataAdapter.InsertCommand.CommandText
            Dim InsertCMD As SqlCommand = New SqlCommand(dataAdapter.InsertCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
            Dim UpdateCmd As SqlCommand = New SqlCommand(dataAdapter.UpdateCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)

            Dim UpdateDate As String = "", UpdateTime As String = ""
            Dim StrSql As String = "SELECT " & HI.UL.ULDate.FormatDateDB & " AS UpdateDate, " & HI.UL.ULDate.FormatTimeDB & " AS UpdateTime"
            Dim dtx As DataTable
            dtx = HI.Conn.SQLConn.GetDataTable(StrSql, Conn.DB.DataBaseName.DB_MERCHAN)

            If dtx.Rows.Count > 0 Then
                For Each Rx As DataRow In dtx.Rows
                    UpdateDate = Rx!UpdateDate.ToString()
                    UpdateTime = Rx!UpdateTime.ToString()
                Next
            End If

            ' Add parameters and set values.
            If dt.Rows.Count = 0 Then Return True
            For Each r As DataRow In dt.Rows
                Dim SelectCMD As SqlCommand = New SqlCommand(dataAdapter.SelectCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                SelectCMD.Parameters.AddWithValue("@FNHSysStyleId", (r.Item("FNHSysStyleId").ToString))
                SelectCMD.Parameters.AddWithValue("@FNSeq", (r.Item("FNSeq").ToString))
                Dim cnt As Integer
                cnt = SelectCMD.ExecuteScalar
                If cnt = 0 Then
                    InsertCMD.Parameters.AddWithValue("@FNHSysStyleId", Val(r.Item("FNHSysStyleId").ToString))
                    InsertCMD.Parameters.AddWithValue("@FNSeq", r.Item("FNSeq").ToString)
                    InsertCMD.Parameters.AddWithValue("@FNMerMatSeq", dt.Rows.IndexOf(r) + 1) 'r.Item("FNSeq").ToString)
                    InsertCMD.Parameters.AddWithValue("@FNHSysMerMatId", r.Item("FNHSysMerMatId").ToString)
                    InsertCMD.Parameters.AddWithValue("@FNPart", r.Item("FNPart").ToString)

                    InsertCMD.Parameters.AddWithValue("@FTPositionPartName", r.Item("FTPositionPartName").ToString)
                    InsertCMD.Parameters.AddWithValue("@FNHSysSuplId", CInt(Val(r.Item("FNHSysSuplId").ToString)))
                    InsertCMD.Parameters.AddWithValue("@FTStateNominate", CInt(Val(r.Item("FTStateNominate").ToString)).ToString)
                    InsertCMD.Parameters.AddWithValue("@FNHSysUnitId", CInt(Val(r.Item("FNHSysUnitId").ToString)))
                    InsertCMD.Parameters.AddWithValue("@FNPrice", Val(r.Item("FNPrice").ToString))
                    InsertCMD.Parameters.AddWithValue("@FNHSysCurId", CInt(Val(r.Item("FNHSysCurId").ToString)))
                    InsertCMD.Parameters.AddWithValue("@FNConSmp", Val(r.Item("FNConSmp").ToString))
                    InsertCMD.Parameters.AddWithValue("@FNConSmpPlus", Val(r.Item("FNConSmpPlus").ToString))
                    InsertCMD.Parameters.AddWithValue("@FTOrderNo", r.Item("FTOrderNo").ToString)
                    InsertCMD.Parameters.AddWithValue("@FTSubOrderNo", r.Item("FTSubOrderNo").ToString)
                    InsertCMD.Parameters.AddWithValue("@FTStateActive", CInt(Val(r.Item("FTStateActive").ToString)).ToString)
                    InsertCMD.Parameters.AddWithValue("@FTStateCombination", CInt(Val(r.Item("FTStateCombination").ToString)).ToString)
                    InsertCMD.Parameters.AddWithValue("@FTStateMainMaterial", CInt(Val(r.Item("FTStateMainMaterial").ToString)).ToString)
                    InsertCMD.Parameters.AddWithValue("@FTInsUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
                    InsertCMD.Parameters.AddWithValue("@FDInsDate", UpdateDate)
                    InsertCMD.Parameters.AddWithValue("@FTInsTime", UpdateTime)
                    InsertCMD.Parameters.AddWithValue("@FTUpdUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
                    InsertCMD.Parameters.AddWithValue("@FDUpdDate", UpdateDate)
                    InsertCMD.Parameters.AddWithValue("@FTUpdTime", UpdateTime)

                    InsertCMD.CommandType = CommandType.Text
                    InsertCMD.ExecuteNonQuery()
                    InsertCMD.Parameters.Clear()
                Else
                    UpdateCmd.Parameters.AddWithValue("@FNHSysStyleId", r.Item("FNHSysStyleId").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FNSeq", r.Item("FNSeq").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FNMerMatSeq", dt.Rows.IndexOf(r) + 1) 'r.Item("FNSeq").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FNHSysMerMatId", r.Item("FNHSysMerMatId").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FNPart", r.Item("FNPart").ToString)

                    UpdateCmd.Parameters.AddWithValue("@FTPositionPartName", r.Item("FTPositionPartName").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FNHSysSuplId", CInt(Val(r.Item("FNHSysSuplId").ToString)))
                    UpdateCmd.Parameters.AddWithValue("@FTStateNominate", CInt(Val(r.Item("FTStateNominate").ToString)).ToString)
                    UpdateCmd.Parameters.AddWithValue("@FNHSysUnitId", CInt(Val(r.Item("FNHSysUnitId").ToString)))
                    UpdateCmd.Parameters.AddWithValue("@FNPrice", Val(r.Item("FNPrice").ToString))
                    UpdateCmd.Parameters.AddWithValue("@FNHSysCurId", CInt(Val(r.Item("FNHSysCurId").ToString)))
                    UpdateCmd.Parameters.AddWithValue("@FNConSmp", Val(r.Item("FNConSmp").ToString))
                    UpdateCmd.Parameters.AddWithValue("@FNConSmpPlus", Val(r.Item("FNConSmpPlus").ToString))
                    UpdateCmd.Parameters.AddWithValue("@FTOrderNo", r.Item("FTOrderNo").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FTSubOrderNo", r.Item("FTSubOrderNo").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FTStateActive", CInt(Val(r.Item("FTStateActive").ToString)).ToString)
                    UpdateCmd.Parameters.AddWithValue("@FTStateCombination", CInt(Val(r.Item("FTStateCombination").ToString)).ToString)
                    UpdateCmd.Parameters.AddWithValue("@FTStateMainMaterial", CInt(Val(r.Item("FTStateMainMaterial").ToString)).ToString)
                    UpdateCmd.Parameters.AddWithValue("@FTUpdUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
                    UpdateCmd.Parameters.AddWithValue("@FDUpdDate", UpdateDate)
                    UpdateCmd.Parameters.AddWithValue("@FTUpdTime", UpdateTime)

                    UpdateCmd.CommandType = CommandType.Text
                    UpdateCmd.ExecuteNonQuery()
                    UpdateCmd.Parameters.Clear()
                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            MessageBox.Show(ex.Message)
            Return False
        End Try
    End Function

#End Region

#Region "Delete Process"

    Private Function DeleteAllData(ByVal objspl As HI.TL.SplashScreen, ByVal _FNHSysBuyId As String, ByVal _FNHSysStyleId As String, ByVal _OrderNo As String,
                                    Optional ByVal _DeleteAll As Boolean = True,
                                    Optional ByVal _ItemNo As Integer = 0, Optional ByVal _FNSeq As Integer = 0,
                                    Optional ByVal _FNMerMatSeq As Integer = 0,
                                    Optional ByVal _FNHSysMatColorId As Integer = 0,
                                    Optional ByVal _FNMatColorSeq As Double = 0,
                                    Optional ByVal _FNHSysMatSizeId As Integer = 0,
                                    Optional ByVal _FNMatSizeSeq As Double = 0) As Boolean
        Dim ret As Boolean = True
        Try
            If _FNHSysBuyId = "" Then _FNHSysBuyId = "0"
            If _FNHSysStyleId = "" Then _FNHSysStyleId = "0"

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

            Dim sqlCmd As New SqlCommand
            sqlCmd.Connection = HI.Conn.SQLConn.Cnn
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_DELETE_MPR]"
            sqlCmd.Parameters.AddWithValue("@FNHSysBuyId", _FNHSysBuyId)
            sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", _FNHSysStyleId)
            sqlCmd.Parameters.AddWithValue("@FTOrderNo", _OrderNo)
            sqlCmd.Parameters.AddWithValue("@DeleteAll", Int(_DeleteAll))

            sqlCmd.Parameters.AddWithValue("@FNHSysRawMatId", _ItemNo)
            sqlCmd.Parameters.AddWithValue("@FNSeq", _FNSeq)
            sqlCmd.Parameters.AddWithValue("@FNMerMatSeq", _FNMerMatSeq)
            sqlCmd.Parameters.AddWithValue("@FNHSysMatColorId", _FNHSysMatColorId)
            sqlCmd.Parameters.AddWithValue("@FNMatColorSeq", _FNMatColorSeq)
            sqlCmd.Parameters.AddWithValue("@FNHSysMatSizeId", _FNHSysMatSizeId)
            sqlCmd.Parameters.AddWithValue("@FNMatSizeSeq", _FNMatSizeSeq)

            Dim sqlDA As New SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
            sqlDA.DeleteCommand = sqlCmd
            sqlDA.DeleteCommand.ExecuteNonQuery()

            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            ret = False
        End Try

        Return ret
    End Function

#End Region

#Region "Create Adapter"
    Public Function CreateAdapter( _
    ByVal connection As SqlConnection) As SqlDataAdapter

        Dim adapter As SqlDataAdapter = New SqlDataAdapter()

        ' Create the SelectCommand. 
        Dim command As SqlCommand = New SqlCommand( _
            "SELECT FNHSysStyleId, FNSeq, FNMerMatSeq, FNHSysMerMatId, FNPart FROM " & _
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle_Mat] " & _
            "WHERE FNHSysStyleId = @FNHSysStyleId AND FNSeq = @FNSeq", connection)

        ' Add the parameters for the SelectCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int)
        command.Parameters.Add("@FNSeq", SqlDbType.Int)

        adapter.SelectCommand = command

        ' Create the InsertCommand.
        command = New SqlCommand( _
            "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle_Mat] " & _
            "([FNHSysStyleId], [FNSeq], [FNMerMatSeq], [FNHSysMerMatId], [FNPart], " & _
            "[FTPositionPartName],[FNHSysSuplId],[FTStateNominate],[FNHSysUnitId],[FNPrice],[FNHSysCurId], " & _
            "[FNConSmp],[FNConSmpPlus],[FTOrderNo],[FTSubOrderNo],[FTStateActive],[FTStateCombination], FTStateMainMaterial, " & _
            "[FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser],[FDUpdDate],[FTUpdTime]) " & _
            "VALUES (@FNHSysStyleId, @FNSeq, @FNMerMatSeq, @FNHSysMerMatId, @FNPart, " & _
            "@FTPositionPartName, @FNHSysSuplId, @FTStateNominate, @FNHSysUnitId, @FNPrice, @FNHSysCurId, " & _
            "@FNConSmp, @FNConSmpPlus, @FTOrderNo, @FTSubOrderNo, @FTStateActive, @FTStateCombination, @FTStateMainMaterial, " & _
            "@FTInsUser, @FDInsDate, @FTInsTime, @FTUpdUser, @FDUpdDate, @FTUpdTime)", connection)

        ' Add the parameters for the InsertCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int, 8, "FNHSysStyleId")
        command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        command.Parameters.Add("@FNMerMatSeq", SqlDbType.Decimal, 5, "FNMerMatSeq")
        command.Parameters.Add("@FNHSysMerMatId", SqlDbType.Decimal, 5, "FNHSysMerMatId")
        command.Parameters.Add("@FNPart", SqlDbType.Int, 8, "FNPart")

        command.Parameters.Add("@FTPositionPartName", SqlDbType.NChar, 50, "FTPositionPartName")
        command.Parameters.Add("@FNHSysSuplId", SqlDbType.Int, 8, "FNHSysSuplId")
        command.Parameters.Add("@FTStateNominate", SqlDbType.VarChar, 1, "FTStateNominate")
        command.Parameters.Add("@FNHSysUnitId", SqlDbType.Int, 8, "FNHSysUnitId")
        command.Parameters.Add("@FNPrice", SqlDbType.Decimal, 5, "FNPrice")
        command.Parameters.Add("@FNHSysCurId", SqlDbType.Int, 8, "FNHSysCurId")
        command.Parameters.Add("@FNConSmp", SqlDbType.Decimal, 5, "FNConSmp")
        command.Parameters.Add("@FNConSmpPlus", SqlDbType.Decimal, 5, "FNConSmpPlus")
        command.Parameters.Add("@FTOrderNo", SqlDbType.NChar, 30, "FTOrderNo")
        command.Parameters.Add("@FTSubOrderNo", SqlDbType.NChar, 30, "FTSubOrderNo")
        command.Parameters.Add("@FTStateActive", SqlDbType.VarChar, 1, "FTStateActive")
        command.Parameters.Add("@FTStateCombination", SqlDbType.VarChar, 1, "FTStateCombination")
        command.Parameters.Add("@FTStateMainMaterial", SqlDbType.VarChar, 1, "FTStateMainMaterial")
        command.Parameters.Add("@FTInsUser", SqlDbType.NChar, 50, "FTInsUser")
        command.Parameters.Add("@FDInsDate", SqlDbType.VarChar, 10, "FDInsDate")
        command.Parameters.Add("@FTInsTime", SqlDbType.VarChar, 8, "FTInsTime")
        command.Parameters.Add("@FTUpdUser", SqlDbType.NChar, 50, "FTUpdUser")
        command.Parameters.Add("@FDUpdDate", SqlDbType.VarChar, 10, "FDUpdDate")
        command.Parameters.Add("@FTUpdTime", SqlDbType.VarChar, 8, "FTUpdTime")

        adapter.InsertCommand = command

        ' Create the UpdateCommand.
        command = New SqlCommand( _
            "UPDATE TMERTStyle_Mat SET " & _
            "FNHSysStyleId = @FNHSysStyleId, " & _
            "FNSeq = @FNSeq, " & _
            "FNMerMatSeq = @FNMerMatSeq, " & _
            "FNHSysMerMatId = @FNHSysMerMatId, " & _
            "FNPart = @FNPart, " & _
            "FTPositionPartName = @FTPositionPartName, " & _
            "FNHSysSuplId = @FNHSysSuplId, " & _
            "FTStateNominate = @FTStateNominate, " & _
            "FNHSysUnitId = @FNHSysUnitId, " & _
            "FNPrice = @FNPrice, " & _
            "FNHSysCurId =@FNHSysCurId, " & _
            "FNConSmp = @FNConSmp, " & _
            "FNConSmpPlus = @FNConSmpPlus, " & _
            "FTOrderNo = @FTOrderNo, " & _
            "FTSubOrderNo = @FTSubOrderNo, " & _
            "FTStateActive = @FTStateActive, " & _
            "FTStateCombination = @FTStateCombination, " & _
            "FTStateMainMaterial = @FTStateMainMaterial, " & _
            "FTUpdUser = @FTUpdUser, " & _
            "FDUpdDate = @FDUpdDate, " & _
            "FTUpdTime = @FTUpdTime " & _
            "WHERE FNHSysStyleId = @FNHSysStyleId AND FNSeq = @FNSeq", connection)

        ' Add the parameters for the UpdateCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int, 8, "FNHSysStyleId")
        command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        command.Parameters.Add("@FNMerMatSeq", SqlDbType.Decimal, 5, "FNMerMatSeq")
        command.Parameters.Add("@FNHSysMerMatId", SqlDbType.Decimal, 5, "FNHSysMerMatId")
        command.Parameters.Add("@FNPart", SqlDbType.Int, 8, "FNPart")

        command.Parameters.Add("@FTPositionPartName", SqlDbType.NChar, 50, "FTPositionPartName")
        command.Parameters.Add("@FNHSysSuplId", SqlDbType.Int, 8, "FNHSysSuplId")
        command.Parameters.Add("@FTStateNominate", SqlDbType.VarChar, 1, "FTStateNominate")
        command.Parameters.Add("@FNHSysUnitId", SqlDbType.Int, 8, "FNHSysUnitId")
        command.Parameters.Add("@FNPrice", SqlDbType.Decimal, 5, "FNPrice")
        command.Parameters.Add("@FNHSysCurId", SqlDbType.Int, 8, "FNHSysCurId")
        command.Parameters.Add("@FNConSmp", SqlDbType.Decimal, 5, "FNConSmp")
        command.Parameters.Add("@FNConSmpPlus", SqlDbType.Decimal, 5, "FNConSmpPlus")
        command.Parameters.Add("@FTOrderNo", SqlDbType.NChar, 30, "FTOrderNo")
        command.Parameters.Add("@FTSubOrderNo", SqlDbType.NChar, 30, "FTSubOrderNo")
        command.Parameters.Add("@FTStateActive", SqlDbType.VarChar, 1, "FTStateActive")
        command.Parameters.Add("@FTStateCombination", SqlDbType.VarChar, 1, "FTStateCombination")
        command.Parameters.Add("@FTStateMainMaterial", SqlDbType.VarChar, 1, "FTStateMainMaterial")
        command.Parameters.Add("@FTUpdUser", SqlDbType.NChar, 50, "FTUpdUser")
        command.Parameters.Add("@FDUpdDate", SqlDbType.VarChar, 10, "FDUpdDate")
        command.Parameters.Add("@FTUpdTime", SqlDbType.VarChar, 8, "FTUpdTime")

        Dim parameter As SqlParameter = command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int, 64, "FNHSysStyleId") 'old id
        parameter.SourceVersion = DataRowVersion.Original

        adapter.UpdateCommand = command

        ' Create the DeleteCommand.
        command = New SqlCommand( _
            "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle_Mat] " & _
            "WHERE FNHSysStyleId = @FNHSysStyleId AND FNSeq = @FNSeq", connection)

        ' Add the parameters for the DeleteCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int, 8, "FNHSysStyleId")
        command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        parameter.SourceVersion = DataRowVersion.Original

        adapter.DeleteCommand = command

        Return adapter
    End Function

    Public Function CreateAdapterImportColor(ByVal connection As SqlConnection) As SqlDataAdapter
        Dim adapter As SqlDataAdapter = New SqlDataAdapter()

        ' Create the SelectCommand. 
        Dim command As SqlCommand = New SqlCommand( _
            "SELECT     O.FNHSysStyleId, B.FTOrderNo, C.FNMatColorSeq, C.FTMatColorNameEN AS FTColorway, B.FTSizeBreakDown, B.FNHSysMatColorId, " & _
            "B.FNPrice, B.FNQuantity, B.FNAmt, B.FNExtraQuantity FROM " & _
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder AS O INNER JOIN" & _
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder_BreakDown AS B ON O.FTOrderNo = B.FTOrderNo INNER JOIN  " & _
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TMERMMatColor] AS C ON B.FNHSysMatColorId = C.FNHSysMatColorId " & _
            "WHERE (O.FNHSysStyleId = @FNHSysStyleId)" & _
            "ORDER BY O.FNHSysStyleId, B.FTOrderNo, C.FNMatColorSeq, B.FTSizeBreakDown", connection)

        ' Add the parameters for the SelectCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int)

        adapter.SelectCommand = command

        Return adapter
    End Function

    Public Function CreateAdapterImportSize(ByVal connection As SqlConnection) As SqlDataAdapter
        Dim adapter As SqlDataAdapter = New SqlDataAdapter()

        ' Create the SelectCommand. 
        Dim command As SqlCommand = New SqlCommand( _
            "SELECT     O.FNHSysStyleId, B.FTOrderNo, C.FNMatSizeSeq, C.FTMatSizeNameEN AS FTSizeBreakDown, B.FNHSysMatSizeId, " & _
            "B.FNPrice, B.FNQuantity, B.FNAmt, B.FNExtraQuantity FROM " & _
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder AS O INNER JOIN" & _
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder_BreakDown AS B ON O.FTOrderNo = B.FTOrderNo INNER JOIN  " & _
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TMERMMatSize] AS C ON B.FNHSysMatSizeId = C.FNHSysMatSizeId " & _
            "WHERE (O.FNHSysStyleId = @FNHSysStyleId)" & _
            "ORDER BY O.FNHSysStyleId, B.FTOrderNo, C.FNMatSizeSeq, B.FTSizeBreakDown", connection)

        ' Add the parameters for the SelectCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int)

        adapter.SelectCommand = command

        Return adapter
    End Function

#End Region

End Class