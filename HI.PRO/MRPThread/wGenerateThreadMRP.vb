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


Public Class wGenerateThreadMRP

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

    Private Sub InidGridMRPReport()
        With ogvmrp

        End With
    End Sub

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
            ogvmatcode.FocusedRowHandle = 0

            'ogcmatcode.ViewCollection.Add(View)
            'ogcmatcode.MainView = View

            ogvmatcode.GridControl = ogcmatcode
            ogvmatcode.OptionsView.ShowAutoFilterRow = False
            ogvmatcode.OptionsView.NewItemRowPosition = NewItemRowPosition.None
            ogvmatcode.OptionsNavigation.AutoFocusNewRow = True
            ogvmatcode.OptionsBehavior.AllowAddRows = True
            ogvmatcode.OptionsBehavior.AllowDeleteRows = True
            ogvmatcode.OptionsBehavior.Editable = True
            ogvmatcode.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.ShowAlways
            ogvmatcode.BestFitColumns()

            FTUpdUser.Text = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)
            FDUpdDate.Text = "??/??/????"
            FTUpdTime.Text = "??:??:??"

            sFNHSysStyleId = ""

        Catch ex As Exception
        End Try
        W_PRCxSwitchTab()
    End Sub

#Region "MAIN PROC"

    Private Sub Proc_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub FNHSysBuyId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysBuyId.EditValueChanged
        If Me.InvokeRequired Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysBuyId_EditValueChanged), New Object() {sender, e})
        Else

            If (_StateLoadOrder) Then Exit Sub

            Dim _Str As String = ""
            FNHSysStyleId.Text = ""
            FNHSysStyleId.Properties.Tag = ""

            ogcmatcode.DataSource = Nothing
            GridOrderList.DataSource = Nothing
            GridCalculated.DataSource = Nothing

            _Str = "SELECT TOP 1 FNHSysBuyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy WITH(NOLOCK) WHERE FTBuyCode ='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' "
            FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
            FNHSysStyleId.Text = ""

            If FNHSysStyleId.Text <> "" Then

                _Str = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
                FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "")

                Dim _Spls As New HI.TL.SplashScreen("Loading.. Please Wait....")

                Try

                    Call LoadOrderInfo(Val(FNHSysBuyId.Properties.Tag.ToString), Val(FNHSysStyleId.Properties.Tag.ToString))
                    Call LoadBOMInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")

                Catch ex As Exception
                End Try

                _Spls.Close()

            End If

            Me.otb.SelectedTabPageIndex = 0

            'Call LoadMPRInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
            'Call LoadMRPReport(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")

        End If
    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysStyleId.EditValueChanged

        If Me.InvokeRequired Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysStyleId_EditValueChanged), New Object() {sender, e})
        Else
            Dim _Str As String = ""
            If FNHSysBuyId.Text <> "" Then
                _Str = "SELECT TOP 1 FNHSysBuyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy WITH(NOLOCK) WHERE FTBuyCode ='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' "
                FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
            End If

            If (_StateLoadOrder) Then Exit Sub
            FNHSysStyleId.Properties.Tag = "0"

            If FNHSysStyleId.Text <> "" Then
                _Str = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
                FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")

                If Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) > 0 Then

                    Dim _Spls As New HI.TL.SplashScreen("Loading.. Please Wait....")
                    Call LoadOrderInfo(Val(FNHSysBuyId.Properties.Tag.ToString), Val(FNHSysStyleId.Properties.Tag.ToString))
                    'If FTOrderSeq.Properties.Tag.ToString <> "" Then
                    Call LoadBOMInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
                    Me.otb.SelectedTabPageIndex = 0
                    _Spls.Close()
                    'Call LoadMPRInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
                    'Call LoadMRPReport(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
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
                FTStateSplitFabric.Checked = False

            Else

                'Call LoadOrderInfo(Val(FNHSysBuyId.Properties.Tag.ToString), Val(FNHSysStyleId.Properties.Tag.ToString))
                'Call LoadBOMInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")

                ''Call LoadMRPReport(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
                ''Call LoadMPRInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")

                'Me.otb.SelectedTabPageIndex = 0
                'FTStateSplitFabric.Checked = False
                '' ogcmatcode.DataSource = Nothing

                'ogcmatcode.Refresh()
            End If
        End If
      
    End Sub

    Private Sub GetBOMInfo(Optional Calculated As Boolean = False, Optional _Spl As HI.TL.SplashScreen = Nothing)
        Dim OrderNo As String = GetOrderArr()

        Dim view As ColumnView = ogvmatcode
        Dim FilterString As String = ""
        Dim FilterStringAll As String = ""
        Dim FilterStringIn As String = ""


        Dim K As String = ""
        Dim KValue As String = ""

        For Each colX As GridColumn In ogvmatcode.Columns

            K = ""
            KValue = ""

            Try
                K = colX.FilterInfo.FilterString
            Catch ex As Exception
            End Try

            Try
                KValue = colX.FilterInfo.Value.ToString
            Catch ex As Exception
            End Try

            Select Case colX.FieldName
                Case "FNHSysMerMatId_None"
            End Select
            Try

                Select Case colX.FieldName
                    Case "FNHSysMerMatId_None"
                    Case Else

                        'Dim xValue As String = view.ActiveFilter.Item(colX).Filter.Value
                        ''If xValue.Contains("*") Then
                        'FilterString += " AND A." & colX.FieldName & " LIKE ('%" & Replace(xValue, "*", "%%%") & "%')" & vbCrLf
                        ''Else
                        ''    FilterString += " AND " & colX.FieldName & " = '" & xValue & "'" & vbCrLf
                        ''End If

                        If K <> "" Then

                            If FilterStringIn = "" Then
                                ' FilterStringIn = " AND A." & colX.FieldName & " IN ('" & HI.UL.ULF.rpQuoted(KValue) & "'"
                                FilterStringIn = "  AND  A." & K.Replace("Or [", "Or A.[")
                            Else
                                'FilterStringIn = FilterStringIn & ",'" & HI.UL.ULF.rpQuoted(KValue) & "'"
                                FilterStringIn = FilterStringIn & "  AND  A." & K
                            End If

                        End If

                        If KValue <> "" Then
                            FilterString += " AND A." & colX.FieldName & " LIKE ('%" & Replace(KValue, "*", "%%%") & "%')" & vbCrLf
                        End If

                End Select

            Catch ex As Exception
            End Try

        Next

        If FilterStringIn <> "" Then
            'FilterStringIn = FilterStringIn & ""
        End If

        FilterStringAll = FilterString & FilterStringIn

        'MsgBox(FilterString)
        Dim _Str As String

        _Str = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
        FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")

        Call LoadBOMInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, OrderNo, Calculated, Calculated, FilterStringAll)
        'Call LoadMPRInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, OrderNo, Calculated, Calculated)
        'Call LoadMRPReport(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
        Me.otb.SelectedTabPageIndex = 0
    End Sub

    Private Function GetAllOrderArr() As String
        Dim OrderNoArr As String = ""
        Dim dt As DataTable ' = CType(GridOrderList.DataSource, DataTable)
        With CType(GridOrderList.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy()
        End With


        Try

            If Not dt Is Nothing Then
                If dt.Select("FNSelect='1'").Length > 0 Then
                    For Each r As DataRow In dt.Select("FNSelect='1'")
                        If OrderNoArr <> "" Then OrderNoArr += ","
                        OrderNoArr += r!FTOrderNo.ToString
                    Next
                Else
                    For Each r As DataRow In dt.Rows
                        If OrderNoArr <> "" Then OrderNoArr += ","
                        OrderNoArr += r!FTOrderNo.ToString
                    Next
                End If
            Else
                OrderNoArr = "---"
            End If

        Catch ex As Exception
            OrderNoArr = ""
        End Try

        Return OrderNoArr

    End Function

    Private Function GetOrderArr() As String
        Dim OrderNoArr As String = ""
        Dim dt As DataTable = CType(GridOrderList.DataSource, DataTable)

        Try

            If Not dt Is Nothing Then

                For Each r As DataRow In dt.Select("FNSelect='1'")
                    If OrderNoArr <> "" Then OrderNoArr += ","
                    OrderNoArr += r!FTOrderNo.ToString
                Next
            End If

        Catch ex As Exception
            OrderNoArr = ""
        End Try

        Return OrderNoArr

    End Function

    Private Function GetOrderArrForReport() As String
        Dim OrderNoArr As String = ""
        CType(GridOrderList.DataSource, DataTable).AcceptChanges()
        Dim dt As DataTable = CType(GridOrderList.DataSource, DataTable)

        Try
            If Not dt Is Nothing Then

                For Each r As DataRow In dt.Select("FNSelect='1'")

                    If OrderNoArr = "" Then
                        OrderNoArr = "  ( {TMERTMPR.FTOrderNo}   in ['" & HI.UL.ULF.rpQuoted(r!FTOrderNo.ToString) & "'"
                    Else
                        OrderNoArr &= ",'" & r!FTOrderNo.ToString & "'"
                    End If

                Next

                If OrderNoArr <> "" Then
                    OrderNoArr &= " ] )"
                End If

            End If

        Catch ex As Exception
            OrderNoArr = ""
        End Try

        Return OrderNoArr

    End Function

    Private _StateLoadOrder As Boolean = False
    Private Sub LoadOrderInfo(ByVal _FNHSysBuyId As Integer, ByVal _FNHSysStyleId As Integer)
        _StateLoadOrder = True
        Me.ochkselectall.Checked = False

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Dim sqlCmd As New SqlCommand
        sqlCmd.Connection = HI.Conn.SQLConn.Cnn
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_ORDERINFO_CMP]"
        sqlCmd.Parameters.AddWithValue("@FNHSysBuyId", _FNHSysBuyId)
        sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", _FNHSysStyleId)
        sqlCmd.Parameters.AddWithValue("@FNHSysCmpId", HI.ST.SysInfo.CmpID)

        oleDbDataAdapter2 = New SqlClient.SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
        oleDbDataAdapter2.SelectCommand = sqlCmd
        dtStyleDetail = New DataTable()
        oleDbDataAdapter2.Fill(dtStyleDetail)

        'dtStyleDetail.BeginInit()
        'For Each R As DataRow In dtStyleDetail.Rows
        '    R!FNSelect = "1"
        'Next

        'dtStyleDetail.EndInit()

        Me.GridOrderList.DataSource = dtStyleDetail

        Dim view As GridView
        view = GridOrderList.Views(0)
        view.OptionsView.ShowAutoFilterRow = True

        Me.GridOrderList = view.GridControl
        Me.GridOrderList.Refresh()
        _StateLoadOrder = False
    End Sub

    Private Sub LoadMRPReport(ByVal _FNHSysBuyId As String, ByVal _FNHSysStyleId As String, ByVal _OrderNo As String, Optional ByVal Calculated As Boolean = False, Optional ByVal _Save2Table As Boolean = False)
        If _FNHSysBuyId = "" Then _FNHSysBuyId = "0"
        If _FNHSysStyleId = "" Then _FNHSysStyleId = "0"

        Dim _Qry As String = ""
        Dim dtMPRInfo As New DataTable

        _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].SP_GETMPR_REPORT_THREAD " & _FNHSysBuyId & "," & _FNHSysStyleId & ",'" & _OrderNo & "'," & IIf(HI.ST.Lang.Language = ST.Lang.eLang.TH, 1, 0) & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        dtMPRInfo = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Me.ogcmrp.DataSource = dtMPRInfo.Copy
        Me.ogcmrp.Refresh()

        Call InitialGridMergCell()

        ' Me.ogvmrp.OptionsView.AllowCellMerge = True
        dtMPRInfo.Dispose()


    End Sub

    Private Sub LoadBOMInfo(ByVal _FNHSysBuyId As String, ByVal _FNHSysStyleId As String, ByVal _OrderNo As String, Optional ByVal Calculated As Boolean = False, Optional ByVal _Save2Table As Boolean = False, Optional _FilterString As String = "", Optional _Spl As HI.TL.SplashScreen = Nothing)
        If _FNHSysBuyId = "" Then _FNHSysBuyId = "0"
        If _FNHSysStyleId = "" Then _FNHSysStyleId = "0"
        Dim dtBomInfo As New DataTable
        Dim _allOrder As String = GetAllOrderArr()

        If _allOrder = "" Then
            _allOrder = "---X---"
        End If
        Dim _Qry As String = ""

        If Not (_Spl Is Nothing) Then
            Application.DoEvents()
            _Spl.UpdateInformation("Calculating MPR Thread Please wait...")
        End If


        _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].SP_GETBREAKDOWN_CALCMPR_THREAD " & _FNHSysBuyId & "," & _FNHSysStyleId & ",'" & _allOrder & "'," & IIf(_Save2Table, 1, 0) & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_FilterString) & "'"

        Application.DoEvents()
        dtBomInfo = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Me.ogcmatcode.DataSource = dtBomInfo

        Try
            If Calculated Then

                'If Not (_Spl Is Nothing) Then
                '    Application.DoEvents()
                '    _Spl.UpdateInformation("Checking And Create New Material Data Please wait...")
                'End If

                'Application.DoEvents()
                'If CreateNewRawMat(_allOrder, _FilterString) = False Then
                '    HI.MG.ShowMsg.mInfo("ไม่สามารถสร้างรายการ วัตถุดิบใหม่ได้", 1091990011, Me.Text, , MessageBoxIcon.Warning)
                'End If

                'If CreateNewRawMatColor() = False Then
                '    HI.MG.ShowMsg.mInfo("ไม่สามารถสร้างรายการ สีวัตถุดิบใหม่ได้", 1091990012, Me.Text, , MessageBoxIcon.Warning)
                'End If

                'If CreateNewRawMatSize() = False Then
                '    HI.MG.ShowMsg.mInfo("ไม่สามารถสร้างรายการ ขนาดวัตถุดิบใหม่ได้", 1091990013, Me.Text, , MessageBoxIcon.Warning)
                'End If
                Try
                    FTUpdUser.Text = dtBomInfo.Rows(0)("FTUpdUser").ToString
                    FDUpdDate.Text = dtBomInfo.Rows(0)("FDUpdDate").ToString
                    FTUpdTime.Text = dtBomInfo.Rows(0)("FTUpdTime").ToString
                Catch ex As Exception
                End Try

                ' If _FilterString = "" Then

                If Not (_Spl Is Nothing) Then

                    Application.DoEvents()
                    _Spl.UpdateInformation("Creating Data For Sourcing Please wait...")
                    Application.DoEvents()

                End If

                Application.DoEvents()
                Generate_Resource(0, _allOrder)
                'End If

                HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Calculate MPR Thread (Style " & FNHSysStyleId.Text & " FO " & _allOrder & "  Item " & _FilterString & ") ")

            End If

        Catch ex As Exception
        End Try

        Dim view As GridView
        view = ogcmatcode.Views(0)
        view.OptionsView.ShowAutoFilterRow = True
        view.BestFitColumns()

        ogvmatcode.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200
        GridView3.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200

        Me.ogcmatcode = view.GridControl
        Me.ogcmatcode.Refresh()

        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

    End Sub

    Private Sub LoadMPRInfo(ByVal _FNHSysBuyId As String, ByVal _FNHSysStyleId As String, ByVal _OrderNo As String, Optional ByVal Calculated As Boolean = False, Optional ByVal _Save2Table As Boolean = False)
        If _FNHSysBuyId = "" Then _FNHSysBuyId = "0"
        If _FNHSysStyleId = "" Then _FNHSysStyleId = "0"

        Dim _Qry As String = ""

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Dim sqlCmd As New SqlCommand
        sqlCmd.Connection = HI.Conn.SQLConn.Cnn
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[SP_GETBREAKDOWN_GETMPR_BY_BD_THREAD]"
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

            '  GridView3 = GridCalculated.Views(0)
            GridView3.OptionsView.ShowAutoFilterRow = True
            GridView3.BestFitColumns()
            ogvmatcode.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200
            GridView3.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200
            '  Me.GridCalculated = GridView3.GridControl
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

    Private Function Generate_Resource(ByVal _FNHSysRawmatID As Integer, ByVal _OrderNo As String) As Boolean
        Dim ret As Boolean = True

        Try

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

            Dim _Qry As String = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[SP_GENERATE_RESOURCE_THREAD] " & _FNHSysRawmatID & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

            'Application.DoEvents()
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Catch ex As Exception

            ret = False
        End Try

        Return ret
    End Function

    Private Sub ocmcalcmrp_Click(sender As System.Object, e As System.EventArgs) Handles ocmcalc.Click
        If Me.FNHSysBuyId.Text <> "" Or FNHSysStyleId.Text <> "" Then

            If True Then

                With CType(GridOrderList.DataSource, DataTable)
                    .AcceptChanges()

                    If .Select("FNSelect='1'").Length <= 0 Then
                        HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Job Order No !!!", 1508255471, Me.Text, , MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                End With

                Dim _Spl As New HI.TL.SplashScreen("Generating...MRP , Please Wait.")

                Try

                    Me.ProcComplete = True
                    Call GetBOMInfo(True, _Spl)
                    ' Dim OrderNo As String = GetOrderArr()
                    'Call LoadBOMInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, OrderNo)
                    Me.otb.SelectedTabPageIndex = 1

                    _Spl.Close()

                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                Catch ex As Exception
                    _Spl.Close()
                End Try

            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Buy หรือ Style !!!", 1408020001, Me.Text, , MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub ocmclear1_Click(sender As System.Object, e As System.EventArgs) Handles ocmclearclsr.Click
        _StateLoadOrder = True
        Me.ochkselectall.Checked = False
        FTStateSplitFabric.Checked = False
        Try
            Me.ogcmatcode.DataSource = Nothing
            Me.GridCalculated.DataSource = Nothing
            Me.GridOrderList.DataSource = Nothing

            Dim xCol As Integer = 0
            Dim Idx As Integer = 0

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
        _StateLoadOrder = False
    End Sub

    Private Sub ocmrefresh_Click(sender As System.Object, e As System.EventArgs) Handles ocmrefresh.Click

        With CType(GridOrderList.DataSource, DataTable)
            .AcceptChanges()

            If .Select("FNSelect='1'").Length <= 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Job Order No !!!", 1508255471, Me.Text, , MessageBoxIcon.Warning)
                Exit Sub
            End If

        End With

        Dim _Spls As New HI.TL.SplashScreen("Loading.. Please Wait....")
        Try

            Dim _Str As String = ""
            Dim OrderNo As String = GetOrderArr()
            ogcmatcode.DataSource = Nothing
            ' GridOrderList.DataSource = Nothing
            GridCalculated.DataSource = Nothing

            If FNHSysBuyId.Text <> "" Then

                _Str = "SELECT TOP 1 FNHSysBuyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy WITH(NOLOCK) WHERE FTBuyCode ='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' "
                FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "")

            End If

            If FNHSysStyleId.Text <> "" Then

                _Str = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
                FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "")

            End If

            'Call LoadOrderInfo(Val(FNHSysBuyId.Properties.Tag.ToString), Val(FNHSysStyleId.Properties.Tag.ToString))
            Call LoadBOMInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, OrderNo)
            'Call LoadMPRInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, GetOrderArr())
            'Call LoadMRPReport(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, GetOrderArr())

        Catch ex As Exception
        End Try

        Me.otb.SelectedTabPageIndex = 0
        _Spls.Close()

    End Sub

    'Private Sub PostSave(dtBefore As DataTable, dtAfter As DataTable, ByVal TableName As String, Optional ByVal refDocKey As String = Nothing)
    '    Try
    '        '' Create Audit log.        
    '        HI.Auditor.CreateLog.CreateLogdata(dtBefore, dtAfter, Me.Name, TableName, refDocKey)
    '        dtBefore = Nothing
    '        dtAfter = Nothing
    '    Catch ex As Exception
    '        '' To do something
    '    End Try
    'End Sub

    Private Sub ocmdelete_Click(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click
        If CType(ogcmatcode.DataSource, DataTable).Rows.Count > 0 Then

            With CType(GridOrderList.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FNSelect='1'").Length <= 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Job Order No !!!", 1508255471, Me.Text, , MessageBoxIcon.Warning)
                    Exit Sub
                End If

            End With

            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text) = True Then
                Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
                Dim OrderNo As String = GetAllOrderArr()
                Dim dtBefore As DataTable
                Dim dtAfter As DataTable
                Dim sqlLog As String =
                         "SELECT   TOP 1   ST.FTStyleCode" & vbCrLf & _
                         "FROM         HITECH_MERCHAN.dbo.TMERTStyle AS ST " & vbCrLf & _
                         "WHERE ST.FNHSysStyleId = " & Val(Me.FNHSysStyleId.Properties.Tag.ToString)

                dtBefore = HI.Conn.SQLConn.GetDataTable(sqlLog, Conn.DB.DataBaseName.DB_MERCHAN)

                If Me.DeleteAllData(_Spls, FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, OrderNo, True) Then

                    'dtAfter = HI.Conn.SQLConn.GetDataTable(sqlLog & "  ST.FNHSysStyleId=0 ", Conn.DB.DataBaseName.DB_MERCHAN)

                    '  Call PostSave(dtBefore, dtAfter, "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle]", Me.FNHSysStyleId.Text)

                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    ' HI.TL.HandlerControl.ClearControl(Me)

                    Call FNHSysStyleId_EditValueChanged(FNHSysStyleId, New System.EventArgs)

                    Me.GridCalculated.DataSource = Nothing
                    Me.GridCalculated.Refresh()
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If
        End If
    End Sub



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


        Next

        SplScreen.Dispose()

        Me.GridCalculated.Refresh()
        Call LoadMPRInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
        Call LoadMRPReport(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
    End Sub

    Private Sub ocmpreview_Click(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click

        'With CType(GridOrderList.DataSource, DataTable)
        '    .AcceptChanges()

        '    If .Select("FNSelect='1'").Length <= 0 Then
        '        HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Job Order No !!!", 1508255471, Me.Text, , MessageBoxIcon.Warning)
        '        Exit Sub
        '    End If

        'End With

        'Dim currentCursor As Cursor = Cursor.Current
        'Cursor.Current = Cursors.WaitCursor

        '''** DevExpress Report
        '' GridCalculated.ShowPrintPreview()
        ''GridCalculated.ShowRibbonPrintPreview()

        '''** Crystal Report
        '' Dim strReportPath As String = Application.StartupPath & "\Reports\Merchandise Report\Mrpreport.rpt"
        ''GenCrystalReport("Merchandise Report\", "Mrpreport.rpt")
        'GenCrystalReport("Merchandise Report\", "Mrpreport_CreateTemp.rpt")
        'Cursor.Current = currentCursor
    End Sub


    Private Sub GridView1_PrintInitialize(sender As System.Object, _
            e As DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs) _
            Handles ogvmatcode.PrintInitialize
        Dim pb As PrintingSystemBase = CType(e.PrintingSystem, PrintingSystemBase)
        pb.PageSettings.Landscape = True
    End Sub

    Private Sub GenCrystalReport(ByVal strReportPath As String, ByVal strReportName As String)

        Dim dataView1 As DataView
        Dim op As CriteriaOperator = GridView3.ActiveFilterCriteria
        Dim filterString As String = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(op)
        Dim _Qry As String = ""

        dataView1 = GridView3.DataSource
        Try
            dataView1.RowFilter = filterString
        Catch ex As Exception
        End Try

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


        _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_GETMPR_TEMP_FOR_REPORT " & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Dim strFoumalar As String = ""

        strFoumalar += "{TMERTMPR.UserLogin} = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"


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

        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In ogvmatcode.Columns
            Dim K As String = ""
            Dim KValue As String = ""
            Try
                K = GridCol.FilterInfo.FilterString
            Catch ex As Exception
            End Try

            Try
                KValue = GridCol.FilterInfo.Value.ToString
            Catch ex As Exception
            End Try

            Select Case True 'GridCol.FieldName.ToString
                Case GridCol.FieldName.ToString = "FTOrderNo" And (K <> "") And (KValue = "")

                    If strFoumalar <> "" Then
                        strFoumalar += " AND  ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TMERTMPR."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                    Else
                        strFoumalar = " ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TMERTMPR."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                    End If

                Case GridCol.FieldName.ToString = "FTStyleCode" And (K <> "") And (KValue = "")
                    If strFoumalar <> "" Then
                        strFoumalar += " AND  ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TMERTMPR."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                    Else
                        strFoumalar = " ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TMERTMPR."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                    End If
                Case GridCol.FieldName.ToString = "FTMainMatCode" And (K <> "") And (KValue = "")
                    If strFoumalar <> "" Then
                        strFoumalar += " AND  ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TMERTMPR."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                    Else
                        strFoumalar = " ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TMERTMPR."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                    End If

                Case GridCol.FieldName.ToString = "FTOrderNo" And (KValue <> "")
                    If strFoumalar <> "" Then
                        strFoumalar += " AND  ({TMERTMPR.FTOrderNo} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                    Else
                        strFoumalar = "  ({TMERTMPR.FTOrderNo} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                    End If
                Case GridCol.FieldName.ToString = "FTStyleCode" And (KValue <> "")
                    If strFoumalar <> "" Then
                        strFoumalar += " AND  ({TMERTMPR.FTStyleCode} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                    Else
                        strFoumalar = "  ({TMERTMPR.FTStyleCode} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                    End If
                Case GridCol.FieldName.ToString = "FTMainMatCode" And (KValue <> "")
                    If strFoumalar <> "" Then
                        strFoumalar += " AND  ({TMERTMPR.FTMainMatCode} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                    Else
                        strFoumalar = "  ({TMERTMPR.FTMainMatCode} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                    End If
                Case Else
            End Select
        Next

        'If filterString.Trim() <> "" Then
        '    If strFoumalar <> "" Then
        '        strFoumalar += " AND " & Replace(Replace(Replace(filterString, "[", "{TMERTMPR."), "]", "}"), "%", "*")
        '    Else
        '        strFoumalar = Replace(Replace(Replace(filterString, "[", "{TMERTMPR."), "]", "}"), "%", "*")
        '    End If
        'End If

        With New HI.RP.Report
            .ReportFolderName = strReportPath
            .ReportName = strReportName
            .AddParameter("FTStateSplitFabric", FTStateSplitFabric.EditValue.ToString)
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
                Me.otb.SelectedTabPageIndex = 0
            End If

        End If

        Return ret

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


        Dim FilterString As String = ""
        Dim FilterStringAll As String = ""
        Dim FilterStringIn As String = ""

        Dim K As String = ""
        Dim KValue As String = ""

        For Each colX As GridColumn In ogvmatcode.Columns

            K = ""
            KValue = ""

            Try
                K = colX.FilterInfo.FilterString
            Catch ex As Exception
            End Try

            Try
                KValue = colX.FilterInfo.Value.ToString
            Catch ex As Exception
            End Try

            Select Case colX.FieldName
                Case "FNHSysMerMatId_None"
            End Select
            Try

                Select Case colX.FieldName
                    Case "FNHSysMerMatId_None"
                    Case Else

                        'Dim xValue As String = view.ActiveFilter.Item(colX).Filter.Value
                        ''If xValue.Contains("*") Then
                        'FilterString += " AND A." & colX.FieldName & " LIKE ('%" & Replace(xValue, "*", "%%%") & "%')" & vbCrLf
                        ''Else
                        ''    FilterString += " AND " & colX.FieldName & " = '" & xValue & "'" & vbCrLf
                        ''End If

                        If K <> "" Then

                            If FilterStringIn = "" Then
                                ' FilterStringIn = " AND A." & colX.FieldName & " IN ('" & HI.UL.ULF.rpQuoted(KValue) & "'"
                                FilterStringIn = "  AND  A." & K.Replace("Or [", "Or A.[")
                            Else
                                'FilterStringIn = FilterStringIn & ",'" & HI.UL.ULF.rpQuoted(KValue) & "'"
                                FilterStringIn = FilterStringIn & "  AND  A." & K
                            End If

                        End If

                        If KValue <> "" Then
                            FilterString += " AND A." & colX.FieldName & " LIKE ('%" & Replace(KValue, "*", "%%%") & "%')" & vbCrLf
                        End If

                End Select

            Catch ex As Exception
            End Try

        Next

        If FilterStringIn <> "" Then
            'FilterStringIn = FilterStringIn & ""
        End If

        FilterStringAll = FilterString & FilterStringIn

        Try
            If _FNHSysBuyId = "" Then _FNHSysBuyId = "0"
            If _FNHSysStyleId = "" Then _FNHSysStyleId = "0"

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

            Dim sqlCmd As New SqlCommand
            sqlCmd.Connection = HI.Conn.SQLConn.Cnn
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[SP_DELETE_MPR_THREAD]"
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
            sqlCmd.Parameters.AddWithValue("@FilterString", FilterStringAll)

            Dim sqlDA As New SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
            sqlDA.DeleteCommand = sqlCmd
            sqlDA.DeleteCommand.ExecuteNonQuery()

            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Calculate MPR (Style " & FNHSysStyleId.Text & " FO " & _OrderNo & "  Item " & FilterStringAll & ") ")

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
#End Region

    Private Sub InitialGridMergCell()

        For Each c As GridColumn In ogvmrp.Columns

            Select Case c.FieldName.ToString
                Case "FTStyleCode", "FTMainMatCode", "FTMainMatName", "FTPositionPartName", "FTMatColorCode", "FTRawMatColorName", "FTOrderNo", "FTRawMatSizeName", "FTGACDAte", "FTMatSizeCode"
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select
        Next
    End Sub

    Private Sub ogvmrp_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvmrp.CellMerge
        Try
            With Me.ogvmrp

                Select Case e.Column.FieldName.ToString
                    Case "FTStyleCode".ToString
                        If .GetRowCellValue(e.RowHandle1, "FTStyleCode").ToString = .GetRowCellValue(e.RowHandle2, "FTStyleCode").ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FTMainMatCode".ToString
                        If .GetRowCellValue(e.RowHandle1, "FTStyleCode").ToString = .GetRowCellValue(e.RowHandle2, "FTStyleCode").ToString And .GetRowCellValue(e.RowHandle1, "FTMainMatCode").ToString = .GetRowCellValue(e.RowHandle2, "FTMainMatCode").ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FTMainMatName".ToString, "FTPositionPartName".ToString
                        If .GetRowCellValue(e.RowHandle1, "FTStyleCode").ToString = .GetRowCellValue(e.RowHandle2, "FTStyleCode").ToString And .GetRowCellValue(e.RowHandle1, "FTMainMatCode").ToString = .GetRowCellValue(e.RowHandle2, "FTMainMatCode").ToString And .GetRowCellValue(e.RowHandle1, e.Column.FieldName.ToString).ToString = .GetRowCellValue(e.RowHandle2, e.Column.FieldName.ToString).ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FTMatColorCode".ToString
                        If .GetRowCellValue(e.RowHandle1, "FTStyleCode").ToString = .GetRowCellValue(e.RowHandle2, "FTStyleCode").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTMainMatCode").ToString = .GetRowCellValue(e.RowHandle2, "FTMainMatCode").ToString _
                              And .GetRowCellValue(e.RowHandle1, "FTPositionPartName").ToString = .GetRowCellValue(e.RowHandle2, "FTPositionPartName").ToString _
                            And .GetRowCellValue(e.RowHandle1, e.Column.FieldName.ToString).ToString = .GetRowCellValue(e.RowHandle2, e.Column.FieldName.ToString).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                        Else

                            e.Merge = False
                            e.Handled = True

                        End If
                    Case "FTRawMatColorName".ToString

                        If .GetRowCellValue(e.RowHandle1, "FTStyleCode").ToString = .GetRowCellValue(e.RowHandle2, "FTStyleCode").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTMainMatCode").ToString = .GetRowCellValue(e.RowHandle2, "FTMainMatCode").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTPositionPartName").ToString = .GetRowCellValue(e.RowHandle2, "FTPositionPartName").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTMatColorCode").ToString = .GetRowCellValue(e.RowHandle2, "FTMatColorCode").ToString _
                            And .GetRowCellValue(e.RowHandle1, e.Column.FieldName.ToString).ToString = .GetRowCellValue(e.RowHandle2, e.Column.FieldName.ToString).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                        Else

                            e.Merge = False
                            e.Handled = True

                        End If

                    Case "FTRawMatSizeName".ToString, "FTMatSizeCode".ToString

                        If .GetRowCellValue(e.RowHandle1, "FTStyleCode").ToString = .GetRowCellValue(e.RowHandle2, "FTStyleCode").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTMainMatCode").ToString = .GetRowCellValue(e.RowHandle2, "FTMainMatCode").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTPositionPartName").ToString = .GetRowCellValue(e.RowHandle2, "FTPositionPartName").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTMatColorCode").ToString = .GetRowCellValue(e.RowHandle2, "FTMatColorCode").ToString _
                            And .GetRowCellValue(e.RowHandle1, e.Column.FieldName.ToString).ToString = .GetRowCellValue(e.RowHandle2, e.Column.FieldName.ToString).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                        Else

                            e.Merge = False
                            e.Handled = True

                        End If

                    Case "FTOrderNo".ToString

                        If .GetRowCellValue(e.RowHandle1, e.Column.FieldName.ToString).ToString = .GetRowCellValue(e.RowHandle2, e.Column.FieldName.ToString).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                        Else

                            e.Merge = False
                            e.Handled = True

                        End If

                    Case "FTGACDAte".ToString

                        If .GetRowCellValue(e.RowHandle1, "FTStyleCode").ToString = .GetRowCellValue(e.RowHandle2, "FTStyleCode").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTMainMatCode").ToString = .GetRowCellValue(e.RowHandle2, "FTMainMatCode").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTPositionPartName").ToString = .GetRowCellValue(e.RowHandle2, "FTPositionPartName").ToString _
                            And .GetRowCellValue(e.RowHandle1, "FTMatColorCode").ToString = .GetRowCellValue(e.RowHandle2, "FTMatColorCode").ToString _
                             And .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString _
                            And .GetRowCellValue(e.RowHandle1, e.Column.FieldName.ToString).ToString = .GetRowCellValue(e.RowHandle2, e.Column.FieldName.ToString).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                        Else

                            e.Merge = False
                            e.Handled = True

                        End If

                    Case Else

                        e.Merge = False
                        e.Handled = True

                End Select
              
            End With

        Catch ex As Exception

        End Try
    End Sub
    Private Sub W_PRCxSwitchTab()

        ocmcalc.Visible = (otb.SelectedTabPage.Name = otpmatcode.Name)
        ocmdelete.Visible = (otb.SelectedTabPage.Name = otpmatcode.Name)

        ocmgenpo.Visible = (otb.SelectedTabPage.Name = CalcMPR.Name)

        HI.TL.METHOD.CallActiveToolBarFunction(Me)

    End Sub
    Private Sub otb_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otb.SelectedPageChanged
        Try

            Call W_PRCxSwitchTab()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub otb_SelectedPageChanging(sender As Object, e As DevExpress.XtraTab.TabPageChangingEventArgs) Handles otb.SelectedPageChanging

        Dim _Str As String = ""
        If FNHSysBuyId.Text <> "" Then
            _Str = "SELECT TOP 1 FNHSysBuyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy WITH(NOLOCK) WHERE FTBuyCode ='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' "
            FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "")
        End If

        If FNHSysStyleId.Text <> "" Then
            _Str = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
            FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "")
        End If

        If FNHSysStyleId.Properties.Tag = "" And FNHSysStyleId.Properties.Tag = "" Then

            e.Cancel = True
        Else
            e.Cancel = False

            Try

                Select Case e.Page.Name
                    Case CalcMPR.Name
                        Call LoadMPRInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, GetOrderArr())
                    Case tabmrpreport.Name
                        Call LoadMRPReport(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, GetOrderArr())

                End Select

            Catch ex As Exception
            End Try

        End If

    End Sub

    Private Sub RepositoryItemCheckEditFNSelect_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryItemCheckEditFNSelect.EditValueChanged
        Try
            If Not (GridOrderList.DataSource Is Nothing) Then
                CType(GridOrderList.DataSource, DataTable).AcceptChanges()
                GridOrderList.Refresh()
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ochkselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ochkselectall.CheckedChanged
        Try
            If _StateLoadOrder = True Then Exit Sub

            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With GridOrderList
                If Not (.DataSource Is Nothing) And GridView2.RowCount > 0 Then

                    With GridView2
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub otb_Click(sender As Object, e As EventArgs) Handles otb.Click

    End Sub

    Private Sub ocmgenpo_Click(sender As Object, e As EventArgs) Handles ocmgenpo.Click
        If Me.FNHSysBuyId.Text <> "" Or FNHSysStyleId.Text <> "" Then


            With CType(GridOrderList.DataSource, DataTable)
                    .AcceptChanges()

                    If .Select("FNSelect='1'").Length <= 0 Then
                        HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Job Order No !!!", 1508255471, Me.Text, , MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                End With

            Dim OrderNoArr As String = ""
            Dim dt As DataTable
            With CType(GridOrderList.DataSource, DataTable)
                .AcceptChanges()
                dt = .Copy()
            End With

            Try

                For Each r As DataRow In dt.Select("FNSelect='1'")
                    If OrderNoArr <> "" Then OrderNoArr += "','"
                    OrderNoArr += r!FTOrderNo.ToString
                Next

            Catch ex As Exception
                OrderNoArr = ""
            End Try


            Dim wAutoGenPo As New wThreadAutoGeneratePO(OrderNoArr)
            wAutoGenPo.ShowDialog()

        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Buy หรือ Style !!!", 1408020001, Me.Text, , MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub GridView3_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridView3.RowStyle
        Try
            If ("" & GridView3.GetRowCellValue(e.RowHandle, "FTPurchaseNo").ToString <> "") Then
                e.Appearance.BackColor = System.Drawing.Color.LawnGreen
                e.Appearance.ForeColor = System.Drawing.Color.Blue

            End If

        Catch ex As Exception
        End Try

    End Sub
End Class