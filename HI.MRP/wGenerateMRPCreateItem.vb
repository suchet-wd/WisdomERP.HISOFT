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
Imports DevExpress.XtraEditors.Controls

Public Class wGenerateMRPCreateItem


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
            ogvmatcode.OptionsView.ShowFilterPanelMode = True
            ogvmatcode.BestFitColumns()

            FTUpdUser.Text = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)
            FDUpdDate.Text = "??/??/????"
            FTUpdTime.Text = "??:??:??"

            sFNHSysStyleId = ""

            Try

                Call W_PRCxSwitchTab()

            Catch ex As Exception
            End Try

        Catch ex As Exception
        End Try
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

                'Try

                Call LoadOrderInfo(Val(FNHSysBuyId.Properties.Tag.ToString), Val(FNHSysStyleId.Properties.Tag.ToString))
                'Call LoadBOMInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")

                'Catch ex As Exception
                'End Try

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

                    ' Dim _Spls As New HI.TL.SplashScreen("Loading.. Please Wait....")
                    Call LoadOrderInfo(Val(FNHSysBuyId.Properties.Tag.ToString), Val(FNHSysStyleId.Properties.Tag.ToString))
                    'If FTOrderSeq.Properties.Tag.ToString <> "" Then
                    ' Call LoadBOMInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
                    Me.otb.SelectedTabPageIndex = 0
                    ' _Spls.Close()
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

        FilterStringAll = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(ogvmatcode.ActiveFilterCriteria)
        ' FilterStringAll = FilterString & FilterStringIn

        If FilterStringAll <> "" Then

            FilterStringAll = " AND " & FilterStringAll
        End If

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




            Dim dtins As New DataTable
            dtins = dt.Select("FNSelect='1'").CopyToDataTable()
            If dtins Is Nothing Then
                dtins.Columns.Add("FTOrderNo", GetType(String))

            Else

                dtins.BeginInit()
                For Each Col As DataColumn In dt.Columns
                    If Col.ColumnName = "FTOrderNo" Then
                    Else
                        dtins.Columns.Remove(Col.ColumnName)
                    End If
                Next
                dtins.BeginInit()

            End If

            HI.Conn.SQLConn.ExecuteStoredProcedure(HI.ST.UserInfo.UserName, "USP_IMPORTTEMPORDERNO", "@tblOrder", dtins, Conn.DB.DataBaseName.DB_MERCHAN)


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


                Dim dtins As New DataTable
                dtins = dt.Select("FNSelect='1'").CopyToDataTable()
                If dtins Is Nothing Then
                    dtins.Columns.Add("FTOrderNo", GetType(String))

                Else

                    dtins.BeginInit()
                    For Each Col As DataColumn In dt.Columns
                        If Col.ColumnName = "FTOrderNo" Then
                        Else
                            dtins.Columns.Remove(Col.ColumnName)
                        End If
                    Next
                    dtins.BeginInit()

                End If

                HI.Conn.SQLConn.ExecuteStoredProcedure(HI.ST.UserInfo.UserName, "USP_IMPORTTEMPORDERNO", "@tblOrder", dtins, Conn.DB.DataBaseName.DB_MERCHAN)

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
        sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_ORDERINFO]"
        sqlCmd.Parameters.AddWithValue("@FNHSysBuyId", _FNHSysBuyId)
        sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", _FNHSysStyleId)
        sqlCmd.Parameters.AddWithValue("@FNDataType", FNSelectOrderType.SelectedIndex)

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

        _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].SP_GETMPR_REPORT " & _FNHSysBuyId & "," & _FNHSysStyleId & ",'" & _OrderNo & "'," & IIf(HI.ST.Lang.Language = ST.Lang.eLang.TH, 1, 0) & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        dtMPRInfo = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

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
        Dim dtitem As New DataTable
        Dim dtsetBomInfo As New DataSet
        Dim _allOrder As String = GetAllOrderArr()
        Dim cmdstring As String = ""

        If _allOrder = "" Then
            _allOrder = "---X---"
        End If
        Dim _Qry As String = ""

        If Not (_Spl Is Nothing) Then

            Application.DoEvents()
            _Spl.UpdateInformation("Calculating MPR Please wait...")

        End If

        Dim Filter As String = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(ogvmatcode.ActiveFilterCriteria)


        If Filter <> "" Then

            Filter = "  AND  " & Filter
        End If



        '_Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].SP_GETBREAKDOWN_CALCMPR " & _FNHSysBuyId & "," & _FNHSysStyleId & ",'" & _allOrder & "'," & IIf(_Save2Table, 1, 0) & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_FilterString) & "'"
        ' _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].SP_GETBREAKDOWN_CALCMPR_TOTEMP " & _FNHSysBuyId & "," & _FNHSysStyleId & ",'" & _allOrder & "'," & IIf(_Save2Table, 1, 0) & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_FilterString) & "'"

        If Calculated Then

            ' _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].SP_CALCMPR_CREATEDATA " & _FNHSysBuyId & "," & _FNHSysStyleId & ",'" & _allOrder & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_FilterString) & "'"



            'Dim dtmatmrp As New DataTable
            'With CType(ogcmatcode.DataSource, DataTable)
            '    .AcceptChanges()
            '    dtmatmrp = .Copy
            'End With

            'Try
            '    For Each R As DataRow In dtmatmrp.Select(Filter)
            '        Dim KK As String = "55"
            '    Next

            'Catch ex As Exception

            'End Try

            cmdstring = " "

            Filter = Filter.Replace("[", "[A.")
            Filter = Filter.Replace("[", "").Replace("]", "")
            If Filter <> "" Then

                cmdstring &= vbCrLf & " DELETE A "
                cmdstring &= vbCrLf & " 	From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTMPR] AS A ,[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTMPR_TMP AS B "
                cmdstring &= vbCrLf & " 	WHERE	B.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                cmdstring &= vbCrLf & " 	  And A.FNHSysStyleId=B.FNHSysStyleId "
                cmdstring &= vbCrLf & "       And A.FNHSysStyleId=B.FNHSysStyleId"
                cmdstring &= vbCrLf & "       And A.FNHSysMerMatId=B.FNHSysMerMatId"
                'cmdstring &= vbCrLf & "       And A.FNHSysMatColorId=B.FNHSysMatColorId"
                'cmdstring &= vbCrLf & "       And A.FNHSysMatSizeId=B.FNHSysMatSizeId "
                cmdstring &= vbCrLf & "       And A.FTOrderNo = B.FTOrderNo "
                cmdstring &= vbCrLf & "    " & Filter

            Else

                cmdstring &= vbCrLf & " DELETE A "
                cmdstring &= vbCrLf & " 	From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTMPR] AS A  "
                cmdstring &= vbCrLf & "      INNER JOIN   (select Distinct FNHSysStyleId,FTOrderNo  from   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTMPR_TMP AS B WITH(NOLOCK) "
                cmdstring &= vbCrLf & " 	WHERE	B.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                cmdstring &= vbCrLf & "  ) AS B"
                cmdstring &= vbCrLf & " 	  ON A.FNHSysStyleId=B.FNHSysStyleId "
                cmdstring &= vbCrLf & "       And A.FTOrderNo=B.FTOrderNo "

            End If

            cmdstring &= vbCrLf & "   INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTMPR]"
            cmdstring &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleId, FNSeq, FNMerMatSeq, FNHSysMerMatId, FNHSysRawMatId, FNColorWaySeq, FNHSysRawMatColorId,"
            cmdstring &= vbCrLf & "    FNRawMatColorSeq, FNHSysMatColorId, FNMatColorSeq, FTRawMatColorCode, FTMatColorCode, FTRawMatColorNameEN, FTRawMatColorNameTH,"
            cmdstring &= vbCrLf & "    FNSieBreakDownSeq, FNHSysRawMatSizeId, FNRawMatSizeSeq, FNHSysMatSizeId, FNMatSizeSeq, FTRawMatSizeCode, FTMatSizeCode,"
            cmdstring &= vbCrLf & "    FTOrderNo1, FTSubOrderNo1, FNConSmp, FNConSmpPlus, FTMainMatCode, FTFabricFrontSize, FTStyleCode, FNHSysUnitId, FTUnitCode,"
            cmdstring &= vbCrLf & "   FNHSysBuyId, FTOrderNo, FTSubOrderNo, FNQuantity, FNQuantityExtra, FNQuantityTest, FNUsedQuantity, FNUsedPlusQuantity, FNPRQuantity, FNPRTotalPrice, FNHSysSuplId, FTStateNominate, FNPrice,"
            cmdstring &= vbCrLf & "   FNHSysCurId, FTSuplCode, FTCurCode, FNHSysCustId, FDOrderDate, FDShipDate, FTPositionPartId, FTPart, FTComponent, FNRawMatSparePer, FNStateChange, FTStateFree, FNHSysSeasonId, FNSeqRef"
            cmdstring &= vbCrLf & "  , FTStateCombination"
            cmdstring &= vbCrLf & "  ,FTStateMainMaterial,FTPositionPartName,FTStateHemNotOptiplan,FNHemNotOptiplan,FNRepeatLengthCM,FNRepeatConvert,FNTotalRepeat "
            cmdstring &= vbCrLf & " 	) "
            cmdstring &= vbCrLf & "    Select  DISTINCT"
            cmdstring &= vbCrLf & "    A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FTUpdUser, A.FDUpdDate, A.FTUpdTime, A.FNHSysStyleId, A.FNSeq, A.FNMerMatSeq, A.FNHSysMerMatId, A.FNHSysRawMatId, A.FNColorWaySeq, A.FNHSysRawMatColorId,"
            cmdstring &= vbCrLf & "    A.FNRawMatColorSeq, A.FNHSysMatColorId, A.FNMatColorSeq, A.FTRawMatColorCode, A.FTMatColorCode, A.FTRawMatColorNameEN, A.FTRawMatColorNameTH,"
            cmdstring &= vbCrLf & "    A.FNSieBreakDownSeq, A.FNHSysRawMatSizeId, A.FNRawMatSizeSeq, A.FNHSysMatSizeId, A.FNMatSizeSeq, A.FTRawMatSizeCode, A.FTMatSizeCode,"
            cmdstring &= vbCrLf & "    A.FTOrderNo1, A.FTSubOrderNo1, A.FNConSmp, A.FNConSmpPlus, A.FTMainMatCode, A.FTFabricFrontSize, A.FTStyleCode, A.FNHSysUnitId, A.FTUnitCode,"
            cmdstring &= vbCrLf & "    A.FNHSysBuyId, A.FTOrderNo, A.FTSubOrderNo, A.FNQuantity, A.FNQuantityExtra, A.FNGarmentQtyTest, A.FNUsedQuantity, A.FNUsedPlusQuantity, A.FNPRQuantity, A.FNPRTotalPrice, A.FNHSysSuplId, A.FTStateNominate, A.FNPrice,"
            cmdstring &= vbCrLf & "    A.FNHSysCurId, A.FTSuplCode, A.FTCurCode, A.FNHSysCustId, A.FDOrderDate, A.FDShipDate, A.FTPositionPartId, A.FTPart, A.FTComponent"
            cmdstring &= vbCrLf & "   ,A.FNRawMatSparePer, '0' AS FNStateChange,A.FTStateFree  ,A.FNHSysSeasonId,A.FNSeqRef"
            cmdstring &= vbCrLf & "   ,A.FTStateCombination"
            cmdstring &= vbCrLf & "   ,A.FTStateMainMaterial"
            cmdstring &= vbCrLf & "   ,A.FTPositionPartName,A.FTStateHemNotOptiplan,A.FNHemNotOptiplan,A.FNRepeatLengthCM ,A.FNRepeatConvert,CASE WHEN ISNULL(A.FNRepeatLengthCM ,0) > 0 THEN CEILING((A.[FNPRQuantity] * A.FNRepeatConvert)/A.FNRepeatLengthCM ) ELSE 0 END AS FNTotalRepeat   "
            cmdstring &= vbCrLf & " 	 From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTMPR_TMP As A With(NOLOCK) "
            cmdstring &= vbCrLf & " 	  WHERE A.FTUserLogIn  ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

            If Filter <> "" Then

                cmdstring &= vbCrLf & "    " & Filter

            End If

            cmdstring &= vbCrLf & "   Declare @TMERTOrder_MatColor AS TABLE( "
            cmdstring &= vbCrLf & "   [FNHSysStyleId] [Int] Not NULL, "
            cmdstring &= vbCrLf & "    [FTOrderNo] [nvarchar](30) Not NULL, "
            cmdstring &= vbCrLf & "    [FNHSysMainMatId] [Int] Not NULL, "
            cmdstring &= vbCrLf & "     [FNHSysRawMatColorId] [Int] Not NULL, "
            cmdstring &= vbCrLf & "   [FTRawMatColorNameTH] [nvarchar](200) NULL, "
            cmdstring &= vbCrLf & "    [FTRawMatColorNameEN] [nvarchar](200) NULL,[FNRowSeq] [int] NULL, "

            cmdstring &= vbCrLf & "   UNIQUE  NONCLUSTERED(FNHSysStyleId, FTOrderNo, FNHSysMainMatId, FNHSysRawMatColorId, [FNRowSeq]) "

            cmdstring &= vbCrLf & "  	) "


            cmdstring &= vbCrLf & "    INSERT INTO  @TMERTOrder_MatColor([FNHSysStyleId] , [FTOrderNo], [FNHSysMainMatId], [FNHSysRawMatColorId], [FTRawMatColorNameTH], [FTRawMatColorNameEN],FNRowSeq )"
            cmdstring &= vbCrLf & "  	Select  FNHSysStyleId "
            cmdstring &= vbCrLf & "  	,FTOrderNo"
            cmdstring &= vbCrLf & "  	,FNHSysMerMatId"
            cmdstring &= vbCrLf & "  ,FNHSysRawMatColorId"
            cmdstring &= vbCrLf & "  	,MAX(FTRawMatColorNameTH) AS FTRawMatColorNameTH"
            cmdstring &= vbCrLf & "  ,MAX(FTRawMatColorNameEN) As FTRawMatColorNameEN"

            cmdstring &= vbCrLf & " ,ROW_NUMBER() Over (Order By [FNHSysStyleId] , [FTOrderNo]) AS [FNRowSeq] "

            cmdstring &= vbCrLf & " 	 From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTMPR_TMP AS A  With(NOLOCK) "
            cmdstring &= vbCrLf & " 	  WHERE FTUserLogIn  ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            cmdstring &= vbCrLf & "  	And FNHSysRawMatColorId <>0"

            If Filter <> "" Then

                cmdstring &= vbCrLf & "   " & Filter

            End If

            cmdstring &= vbCrLf & "   GROUP BY FNHSysStyleId"
            cmdstring &= vbCrLf & "  ,FTOrderNo"
            cmdstring &= vbCrLf & "  	,FNHSysMerMatId"
            cmdstring &= vbCrLf & "  	,FNHSysRawMatColorId "

            cmdstring &= vbCrLf & "    DELETE From BBD "
            cmdstring &= vbCrLf & "    From     @TMERTOrder_MatColor As MMD  INNER Join"
            cmdstring &= vbCrLf & "             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder_Mat_Color AS BBD "
            cmdstring &= vbCrLf & "    On MMD.FNHSysStyleId = BBD.FNHSysStyleId "
            cmdstring &= vbCrLf & "    And MMD.FTOrderNo = BBD.FTOrderNo "

            If Filter <> "" Then

                cmdstring &= vbCrLf & "    And MMD.FNHSysMainMatId = BBD.FNHSysMainMatId"

            End If

            cmdstring &= vbCrLf & " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LOG) & "].[dbo].TMERTMPR_History (  FTUserLogin, FTDateCreate, FNCaltype,FTStyleCode, FTSeasonCode, FTOrderNo, FTMainMatCode, FTRawMatColorCode, FTRawMatColorNameEN, FTRawMatColorNameTH"
            cmdstring &= vbCrLf & "  , FTRawMatSizeCode,  FNQuantity, FNHSysRawMatId, FNHSysStyleId, FNHSysSeasonId,FTFilterItem ,FTCalTypeName,FTCalDate,FTCalTime,FNRowSeq"
            cmdstring &= vbCrLf & "  , FTSubOrderNo,FNUsedQuantity,FNUsedPlusQuantity,FNPRQuantity,FDShipDate,FDOGacDate)"
            cmdstring &= vbCrLf & "   Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',Getdate(),1, ST.FTStyleCode "
            cmdstring &= vbCrLf & "  , SS.FTSeasonCode  "
            cmdstring &= vbCrLf & "  , A.FTOrderNo "
            cmdstring &= vbCrLf & "  , A.FTMainMatCode "
            cmdstring &= vbCrLf & "  , A.FTRawMatColorCode "
            cmdstring &= vbCrLf & "  , A.FTRawMatColorNameEN"
            cmdstring &= vbCrLf & "  , A.FTRawMatColorNameTH"
            cmdstring &= vbCrLf & " , A.FTRawMatSizeCode"
            cmdstring &= vbCrLf & " , SUM(A.FNPRQuantity) AS FNQuantity	"
            cmdstring &= vbCrLf & " , A.FNHSysRawMatId "
            cmdstring &= vbCrLf & " , A.FNHSysStyleId "
            cmdstring &= vbCrLf & " , A.FNHSysSeasonId	"
            cmdstring &= vbCrLf & " , '" & HI.UL.ULF.rpQuoted(Filter) & "'	"
            cmdstring &= vbCrLf & " , 'Calculate'	"
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & "	"
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & "	"

            cmdstring &= vbCrLf & " ,Row_Number() Over (Partition by A.FTOrderNo,A.FTMainMatCode,A.FTRawMatColorCode,A.FTRawMatSizeCode Order by A.FTSubOrderNo,A.FTMainMatCode,A.FTRawMatColorCode ) AS FNRowSeq	"
            cmdstring &= vbCrLf & "  ,A.FTSubOrderNo,SUM(A.FNUsedQuantity) AS FNUsedQuantity,SUM(A.FNUsedPlusQuantity) AS FNUsedPlusQuantity,SUM(A.FNPRQuantity) AS FNPRQuantity "
            cmdstring &= vbCrLf & "  , MAX(ISNULL(Sub.FDShipDate,'') ) AS FDShipDate "
            cmdstring &= vbCrLf & "  , MAX(ISNULL(Sub.FDShipDateOrginal,'') ) AS FDOGacDate "

            cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTMPR_TMP AS A WITH(NOLOCK) "
            cmdstring &= vbCrLf & "       Outer Apply(SELECT TOP 1 ST.FTStyleCode  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TMERMStyle AS ST WITH(NOLOCK) WHERE ST.FNHSysStyleId =A.FNHSysStyleId ) AS ST  "
            cmdstring &= vbCrLf & "       Outer Apply(SELECT TOP 1 SS.FTSeasonCode  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TMERMSeason  AS SS WITH(NOLOCK) WHERE SS.FNHSysSeasonId =A.FNHSysSeasonId ) AS SS "
            cmdstring &= vbCrLf & "       Outer Apply(SELECT TOP 1 Sub.FDShipDate,Sub.FDShipDateOrginal  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrderSub  AS Sub WITH(NOLOCK) WHERE Sub.FTSubOrderNo =A.FTSubOrderNo ) AS Sub "

            cmdstring &= vbCrLf & " 	  WHERE A.FTUserLogIn  ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

            If Filter <> "" Then

                cmdstring &= vbCrLf & "    " & Filter

            End If

            cmdstring &= vbCrLf & "   GROUP BY ST.FTStyleCode  "
            cmdstring &= vbCrLf & "	 ,SS.FTSeasonCode "
            cmdstring &= vbCrLf & " ,A.FTOrderNo ,A.FTSubOrderNo"
            cmdstring &= vbCrLf & " ,A.FTRawMatColorCode, A.FTRawMatSizeCode, A.FTRawMatColorNameEN, A.FTRawMatColorNameTH, A.FTMainMatCode, A.FNHSysRawMatId, A.FNHSysStyleId, A.FNHSysSeasonId "

            cmdstring &= vbCrLf & "    INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder_Mat_Color( FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FTOrderNo"
            cmdstring &= vbCrLf & "  , FNHSysMainMatId, FNHSysRawMatColorId, FTRawMatColorNameTH, FTRawMatColorNameEN)	"
            cmdstring &= vbCrLf & "  	Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            cmdstring &= vbCrLf & "     ,Convert(varchar(10),GetDate(),111)"
            cmdstring &= vbCrLf & "     ,Convert(varchar(8),GetDate(),114)"
            cmdstring &= vbCrLf & "    , MM.FNHSysStyleId"
            cmdstring &= vbCrLf & "    , MM.FTOrderNo"
            cmdstring &= vbCrLf & "    , MM.FNHSysMainMatId"
            cmdstring &= vbCrLf & "    , MM.FNHSysRawMatColorId"
            cmdstring &= vbCrLf & "   , MM.FTRawMatColorNameTH"
            cmdstring &= vbCrLf & "    , MM.FTRawMatColorNameEN "
            cmdstring &= vbCrLf & "   FROM     @TMERTOrder_MatColor As MM  "

            cmdstring &= vbCrLf & " Declare @TabOrder AS Table (OrderNo nvarchar(30)  UNIQUE  NONCLUSTERED  (OrderNo) )"

            cmdstring &= vbCrLf & "    INSERT INTO @TabOrder(OrderNo) "
            cmdstring &= vbCrLf & "   Select Distinct FTOrderNo "
            cmdstring &= vbCrLf & " 	From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTMPR_TMP] AS A WITH(NOLOCK)"
            cmdstring &= vbCrLf & " 	WHERE A.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

            cmdstring &= vbCrLf & " Declare @TabRC AS TABLE ("
            cmdstring &= vbCrLf & " [FTUserLogIn] [varchar](50) NOT NULL,"
            cmdstring &= vbCrLf & " [FTOrderNo] [nvarchar](30) NOT NULL,"
            cmdstring &= vbCrLf & " [FTSubOrderNo] [nvarchar](30) NOT NULL,"
            cmdstring &= vbCrLf & " [FNHSysRawMatId] [int] NOT NULL,"
            cmdstring &= vbCrLf & " [FNUsedQuantity] [numeric](18, 5) NULL,"
            cmdstring &= vbCrLf & " [FNUsedPlusQuantity] [numeric](18, 5) NULL,"
            cmdstring &= vbCrLf & " [FNUsedQuantityDIFF] [numeric](18, 5) NULL,"
            cmdstring &= vbCrLf & " [FNUsedQuantityPlusDIFF] [numeric](18, 5) NULL,"
            cmdstring &= vbCrLf & " [FNHSysUnitId] [int] NULL,"
            cmdstring &= vbCrLf & " [FNPrice] [numeric](18, 5) NULL,"
            cmdstring &= vbCrLf & " [FNHSysCurId] [int] NULL,"
            cmdstring &= vbCrLf & " [FNHSysSuplId] [int] NULL,"
            cmdstring &= vbCrLf & " [FTStateNominate] [varchar](1) NULL,"
            cmdstring &= vbCrLf & " [FNStateChange] [int]  NULL,"
            cmdstring &= vbCrLf & " [FTFabricFrontSize] [nvarchar](50) NULL,"
            cmdstring &= vbCrLf & " [FTStateFree] [varchar](1) NULL,"
            cmdstring &= vbCrLf & " [FNStateNew] [int] NULL,"
            cmdstring &= vbCrLf & " [FNRowSeq] [int] NULL,"
            cmdstring &= vbCrLf & " [FTStateHemNotOptiplan] [varchar](1) NULL,"
            cmdstring &= vbCrLf & " [FNHemNotOptiplan] [numeric](18, 5) NULL,"
            cmdstring &= vbCrLf & " [FNRepeatLengthCM] numeric(18,5) NULL,"
            cmdstring &= vbCrLf & " [FNRepeatConvert] numeric(18,10) NULL,"
            cmdstring &= vbCrLf & " [FNTotalRepeat] numeric(18,5) NULL,"
            cmdstring &= vbCrLf & " [FTMainMatCoide] [nvarchar](50) NULL,"
            cmdstring &= vbCrLf & "  UNIQUE  NONCLUSTERED  ([FTUserLogIn],[FTOrderNo],[FTSubOrderNo],[FNHSysRawMatId],[FNStateChange],[FNRowSeq],[FTMainMatCoide])"
            cmdstring &= vbCrLf & "  ) "

            cmdstring &= vbCrLf & "    INSERT INTO @TabRC(FTUserLogIn, FTOrderNo, FTSubOrderNo, FNHSysRawMatId, FNUsedQuantity, FNUsedPlusQuantity "
            cmdstring &= vbCrLf & "  , FNUsedQuantityDIFF, FNUsedQuantityPlusDIFF, FNHSysUnitId, FNPrice, FNHSysCurId"
            cmdstring &= vbCrLf & "  , FNHSysSuplId, FTStateNominate, FNStateChange,FTFabricFrontSize,FTStateFree,FNStateNew,[FNRowSeq],FTStateHemNotOptiplan,FNHemNotOptiplan,FNRepeatLengthCM,FNRepeatConvert,FNTotalRepeat)"

            cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',FTOrderNo, FTSubOrderNo, FNHSysRawMatId"
            cmdstring &= vbCrLf & " 	, MAX(FNUsedQuantity) AS FNUsedQuantity"
            cmdstring &= vbCrLf & " 	, MAX(FNUsedPlusQuantity) AS FNUsedPlusQuantity"
            cmdstring &= vbCrLf & " 	, MAX(FNUsedQuantityDIFF) AS FNUsedQuantityDIFF"
            cmdstring &= vbCrLf & " 	, MAX(FNUsedQuantityPlusDIFF) AS FNUsedQuantityPlusDIFF"
            cmdstring &= vbCrLf & " 	, FNHSysUnitId, FNPrice, FNHSysCurId, FNHSysSuplId, FTStateNominate"
            cmdstring &= vbCrLf & " 	, FNStateChange"
            cmdstring &= vbCrLf & " 	, FTFabricFrontSize"
            cmdstring &= vbCrLf & " ,Max(FTStateFree) AS FTStateFree"
            cmdstring &= vbCrLf & " 	,FNStateNew"
            cmdstring &= vbCrLf & " 	,ROW_NUMBER() Over (Order By FTOrderNo) AS [FNRowSeq]"
            cmdstring &= vbCrLf & " ,MAX(FTStateHemNotOptiplan) AS FTStateHemNotOptiplan"
            cmdstring &= vbCrLf & " 	,MAX(FNHemNotOptiplan) As FNHemNotOptiplan"
            cmdstring &= vbCrLf & " 	,MAX(FNRepeatLengthCM) AS FNRepeatLengthCM"
            cmdstring &= vbCrLf & " 	,MAX([FNRepeatConvert]) AS FNRepeatConvert"
            cmdstring &= vbCrLf & " 	,0 AS FNTotalRepeat"
            cmdstring &= vbCrLf & " 	FROM("
            cmdstring &= vbCrLf & "  SELECT A.FTOrderNo, '' AS FTSubOrderNo, A.FNHSysRawMatId"
            cmdstring &= vbCrLf & "    , CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedQuantity))) ELSE SUM(FNUsedQuantity)  END AS FNUsedQuantity"
            cmdstring &= vbCrLf & " 	,CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedPlusQuantity))) ELSE  SUM(FNUsedPlusQuantity) END AS FNUsedPlusQuantity"
            cmdstring &= vbCrLf & " 	, MAX(FNHSysUnitId) AS FNHSysUnitId"
            cmdstring &= vbCrLf & " 	, MAX(FNPrice) AS FNPrice"
            cmdstring &= vbCrLf & " 	, MAX(FNHSysCurId) AS FNHSysCurId"
            cmdstring &= vbCrLf & " 	, MAX(FNHSysSuplId) AS FNHSysSuplId"
            cmdstring &= vbCrLf & " 	, MAX(FTStateNominate) AS FTStateNominate"
            cmdstring &= vbCrLf & " 	, (SUM(FNUsedQuantity)) - (MAX(ISNULL(RES.FNUsedQuantity2, 0))) AS FNUsedQuantityDIFF"
            cmdstring &= vbCrLf & " 	, ( CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedPlusQuantity))) ELSE SUM(FNUsedPlusQuantity) END) - (MAX(ISNULL(RES.FNUsedPlusQuantity2, 0))) AS FNUsedQuantityPlusDIFF"
            cmdstring &= vbCrLf & " 	, ( CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedQuantity))) ELSE SUM(FNUsedQuantity)  END  + CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN CEILING(SUM(FNUsedPlusQuantity)) ELSE SUM(FNUsedPlusQuantity) END) - (MAX(ISNULL(RES.FNUsedQuantity2, 0)) + MAX(ISNULL(RES.FNUsedPlusQuantity2, 0))) AS DIFFQTY"
            cmdstring &= vbCrLf & " 	, CASE WHEN ( CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedQuantity))) ELSE SUM(FNUsedQuantity)  END  + CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedPlusQuantity))) ELSE SUM(FNUsedPlusQuantity) END) > "
            cmdstring &= vbCrLf & " 	(MAX(ISNULL(RES.FNUsedQuantity2, 0)) + MAX(ISNULL(RES.FNUsedPlusQuantity2, 0)))"
            cmdstring &= vbCrLf & " 	THEN CASE WHEN (MAX(ISNULL(RES.FNUsedQuantity2, 0)) + MAX(ISNULL(RES.FNUsedPlusQuantity2, 0))) = 0 "
            cmdstring &= vbCrLf & " 	THEN 0 ELSE MAX(RES.FNStateChange) + 1 END"
            cmdstring &= vbCrLf & " 	Else MAX(ISNULL(Res.FNStateChange, 0))"
            cmdstring &= vbCrLf & "    End As FNStateChange	"
            cmdstring &= vbCrLf & "   ,MAX(FTFabricFrontSize) AS FTFabricFrontSize	"
            cmdstring &= vbCrLf & "   ,MAX(ISNULL(FTStateFree,'0')) AS FTStateFree"

            cmdstring &= vbCrLf & "   , CASE WHEN (SUM(FNUsedQuantity) + CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedPlusQuantity))) ELSE SUM(FNUsedPlusQuantity) END) > "
            cmdstring &= vbCrLf & " 		(MAX(ISNULL(RES.FNUsedQuantity2, 0)) + MAX(ISNULL(RES.FNUsedPlusQuantity2, 0))) And (MAX(ISNULL(RES.FNUsedQuantity2, 0)) + MAX(ISNULL(RES.FNUsedPlusQuantity2, 0))) > 0"
            cmdstring &= vbCrLf & " 	THEN 1"
            cmdstring &= vbCrLf & " 	Else 0"
            cmdstring &= vbCrLf & "   End As FNStateNew	"
            cmdstring &= vbCrLf & "   ,MAX(FTStateHemNotOptiplan) AS FTStateHemNotOptiplan"
            cmdstring &= vbCrLf & "   ,SUM(ISNULL(A.FNHemNotOptiplan,0)) AS FNHemNotOptiplan"
            cmdstring &= vbCrLf & "   ,MAX(ISNULL(FNRepeatLengthCM,0)) AS FNRepeatLengthCM"
            cmdstring &= vbCrLf & "   ,MAX(ISNULL([FNRepeatConvert],0)) AS FNRepeatConvert"
            cmdstring &= vbCrLf & "   ,MAX(ISNULL([FTMainMatCode],'')) AS FTMainMatCode"
            cmdstring &= vbCrLf & " 	From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTMPR_TMP] AS A WITH(NOLOCK)"

            cmdstring &= vbCrLf & " 	OUTER APPLY(SELECT FTOrderNo, FNHSysRawMatId, MAX(FNStateChange) AS FNStateChange"
            cmdstring &= vbCrLf & " 	, SUM(FNUsedQuantity) AS FNUsedQuantity2, SUM(FNUsedPlusQuantity) AS FNUsedPlusQuantity2"
            cmdstring &= vbCrLf & " 	From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTOrder_Sourcing]  AS X  WITH(NOLOCK)"
            cmdstring &= vbCrLf & " 	Where X.FTOrderNo =A.FTOrderNo "
            cmdstring &= vbCrLf & " 	AND X.FNHSysRawMatId =A.FNHSysRawMatId "
            cmdstring &= vbCrLf & " 	GROUP BY FTOrderNo, FNHSysRawMatId) AS RES"

            cmdstring &= vbCrLf & " 	WHERE A.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            cmdstring &= vbCrLf & "    And A.FNHSysRawMatId > 0    "

            If Filter <> "" Then

                cmdstring &= vbCrLf & "   " & Filter

            End If

            cmdstring &= vbCrLf & " 	GROUP BY A.FTOrderNo,  A.FNHSysRawMatId"
            cmdstring &= vbCrLf & "  ) AS A "
            cmdstring &= vbCrLf & "  GROUP BY FTOrderNo, FTSubOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice"
            cmdstring &= vbCrLf & " 	, FNHSysCurId, FNHSysSuplId, FTStateNominate, DIFFQTY, FNStateChange,FTFabricFrontSize,FNStateNew "
            cmdstring &= vbCrLf & "   delete from @TabRC where FNStateNew <> 0 And [FNUsedQuantityDIFF] =0 And FNUsedQuantityPlusDIFF =0 AND ISNULL(FNStateChange,0) >0 "
            cmdstring &= vbCrLf & " update @TabRC set FNStateChange = isnull(FNStateChange,0)"

            If Filter <> "" Then

                cmdstring &= vbCrLf & "    DELETE A "
                cmdstring &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrder_Resource] AS A INNER Join @TabOrder As B On A.FTOrderNo = B.OrderNo "
                cmdstring &= vbCrLf & "    CROSS APPLY (SELECT TOP 1 MMM.FTRawMatCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.[TINVENMMaterial] AS MMM WITH(NOLOCK) WHERE MMM.FNHSysRawMatId =A.FNHSysRawMatId ) AS MMM "
                cmdstring &= vbCrLf & " 	Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTOrder_Sourcing] AS C WITH(NOLOCK)"
                cmdstring &= vbCrLf & " 	On A.FTOrderNo = C.FTOrderNo"
                cmdstring &= vbCrLf & " 	And A.FNHSysRawMatId=C.FNHSysRawMatId"
                cmdstring &= vbCrLf & " And A.FNStateChange = C.FNStateChange"
                cmdstring &= vbCrLf & "           INNER Join @TabRC AS RC ON A.FTOrderNo = RC.FTOrderNo  And MMM.FTRawMatCode=RC.FTMainMatCoide"
                cmdstring &= vbCrLf & "  WHERE  RC.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' And C.FTOrderNo Is NULL"

            Else

                cmdstring &= vbCrLf & "   DELETE A"
                cmdstring &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrder_Resource] AS A INNER Join @TabOrder As B On A.FTOrderNo = B.OrderNo "
                cmdstring &= vbCrLf & " 	Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTOrder_Sourcing] AS C WITH(NOLOCK)"
                cmdstring &= vbCrLf & " 	On A.FTOrderNo = C.FTOrderNo"
                cmdstring &= vbCrLf & " 	And A.FNHSysRawMatId=C.FNHSysRawMatId"

                cmdstring &= vbCrLf & " 	WHERE   C.FTOrderNo Is NULL "


            End If


            cmdstring &= vbCrLf & "     Update A"
            cmdstring &= vbCrLf & "  SET [FNUsedQuantity] = CASE WHEN TMP.FNStateNew = 0 THEN TMP.[FNUsedQuantity] ELSE TMP.[FNUsedQuantityDIFF] END"
            cmdstring &= vbCrLf & "    ,[FNUsedPlusQuantity] =CASE WHEN TMP.FNStateNew = 0 THEN TMP.FNUsedPlusQuantity ELSE TMP.FNUsedQuantityPlusDIFF END "
            cmdstring &= vbCrLf & "    ,[FNPrice] = TMP.[FNPrice] "
            cmdstring &= vbCrLf & "    ,FTStateHemNotOptiplan =TMP.FTStateHemNotOptiplan"
            cmdstring &= vbCrLf & "    ,FNHemNotOptiplan=TMP.FNHemNotOptiplan"
            cmdstring &= vbCrLf & "    ,FNRepeatLengthCM = TMP.FNRepeatLengthCM"
            cmdstring &= vbCrLf & "    ,FNRepeatConvert  = TMP.FNRepeatConvert"
            cmdstring &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrder_Resource] As A INNER Join  @TabRC As TMP On"
            cmdstring &= vbCrLf & "    TMP.FNHSysRawMatId = A.FNHSysRawMatId"
            cmdstring &= vbCrLf & "   And TMP.FTOrderNo =A.FTOrderNo"
            cmdstring &= vbCrLf & "   And TMP.FTSubOrderNo =A.FTSubOrderNo"
            cmdstring &= vbCrLf & "   And TMP.FNStateChange =A.FNStateChange"
            cmdstring &= vbCrLf & "   INNER Join @TabOrder AS XA ON A.FTOrderNo = XA.OrderNo"
            cmdstring &= vbCrLf & "    WHERE  TMP.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

            '/* INSERT New RESOURCE */
            cmdstring &= vbCrLf & "  INSERT INTO [TMERTOrder_Resource] ([FTInsUser], [FDInsDate], [FTInsTime], [FTUpdUser], [FDUpdDate], [FTUpdTime] "
            cmdstring &= vbCrLf & " , FTOrderNo, FTSubOrderNo, FNHSysRawMatId"
            cmdstring &= vbCrLf & " 	, FNUsedQuantity"
            cmdstring &= vbCrLf & " 	, FNUsedPlusQuantity"
            cmdstring &= vbCrLf & " 	, FNHSysUnitId, FNPrice, FNHSysCurId, FNHSysSuplId, FTStateNominate"
            cmdstring &= vbCrLf & " , FNStateChange,FTFabricFrontSize,FTStateFree,FTStateHemNotOptiplan,FNHemNotOptiplan,FNRepeatLengthCM,FNRepeatConvert)"
            cmdstring &= vbCrLf & " 	Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  AS [FTInsUser], CONVERT(VARCHAR(10), GETDATE(), 111) AS [FDInsDate], CONVERT(VARCHAR(8), GETDATE(), 108) AS [FTInsTime]"
            cmdstring &= vbCrLf & " 	, '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS [FTUpdUser], CONVERT(VARCHAR(10), GETDATE(), 111) AS [FDUpdDate], CONVERT(VARCHAR(8), GETDATE(), 108) AS [FTUpdTime]"
            cmdstring &= vbCrLf & ", TMP.FTOrderNo, TMP.FTSubOrderNo, TMP.FNHSysRawMatId"
            cmdstring &= vbCrLf & "	, CASE WHEN TMP.FNStateNew = 0 THEN TMP.[FNUsedQuantity] ELSE TMP.[FNUsedQuantityDIFF] END"
            cmdstring &= vbCrLf & "	, CASE WHEN TMP.FNStateNew = 0 THEN TMP.FNUsedPlusQuantity ELSE TMP.FNUsedQuantityPlusDIFF END "
            cmdstring &= vbCrLf & "	, TMP.FNHSysUnitId, TMP.FNPrice, TMP.FNHSysCurId, TMP.FNHSysSuplId, TMP.FTStateNominate"
            cmdstring &= vbCrLf & "	, TMP.FNStateChange"
            cmdstring &= vbCrLf & "	, TMP.FTFabricFrontSize"
            cmdstring &= vbCrLf & "	, TMP.FTStateFree"
            cmdstring &= vbCrLf & "	,TMP.FTStateHemNotOptiplan,TMP.FNHemNotOptiplan,TMP.FNRepeatLengthCM,TMP.FNRepeatConvert"
            cmdstring &= vbCrLf & "FROM @TabRC AS TMP LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrder_Resource] AS B WITH (NOLOCK)"
            cmdstring &= vbCrLf & " On TMP.FNHSysRawMatId = B.FNHSysRawMatId"
            cmdstring &= vbCrLf & " And TMP.FTOrderNo =B.FTOrderNo"
            cmdstring &= vbCrLf & " And TMP.FTSubOrderNo =B.FTSubOrderNo"
            cmdstring &= vbCrLf & " And TMP.FNStateChange =B.FNStateChange"
            cmdstring &= vbCrLf & "	WHERE  TMP.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  And B.FTOrderNo Is NULL"

            cmdstring &= vbCrLf & "	 Update A Set FNOptiplanQuantity = CEILING(ISNULL(Case When a.FNHSysUnitId = MZ.FNHSysUnitId Then MZ.FNTotal  Else Convert(numeric(18, 4),(MZ.FNTotal  * ISNULL(UCV.FNRateTo,1) ) / ISNULL(UCV.FNRateFrom,1))  END ,0) + CASE WHEN ISNULL(MZ.FNTotal,0) > 0 THEN ISNULL(A.FNHemNotOptiplan,0) ELSE 0.0 END)"
            cmdstring &= vbCrLf & " , FNOptiplanQuantityOrg = CEILING(ISNULL(CASE WHEN a.FNHSysUnitId = MZ.FNHSysUnitId THEN MZ.FNTotal  ELSE Convert(numeric(18,4),(MZ.FNTotal  * ISNULL(UCV.FNRateTo,1) ) / ISNULL(UCV.FNRateFrom,1))  END ,0) )"
            cmdstring &= vbCrLf & "  ,FTStateFree = CC.FTStateFree"
            cmdstring &= vbCrLf & "   ,FNTotalRepeat = CASE WHEN ISNULL(A.FNRepeatLengthCM ,0) > 0 THEN CEILING(((A.FNUsedQuantity + A.FNUsedPlusQuantity) * A.FNRepeatConvert)/A.FNRepeatLengthCM ) ELSE 0 END"
            cmdstring &= vbCrLf & "   ,FNTotalRepeatOptiplan = CASE WHEN ISNULL(A.FNRepeatLengthCM ,0) > 0 THEN CEILING((( ISNULL(CASE WHEN a.FNHSysUnitId = MZ.FNHSysUnitId THEN MZ.FNTotal  ELSE Convert(numeric(18,4),(MZ.FNTotal  * ISNULL(UCV.FNRateTo,1) ) / ISNULL(UCV.FNRateFrom,1))  END ,0) ) * A.FNRepeatConvert)/A.FNRepeatLengthCM ) ELSE 0 END"
            cmdstring &= vbCrLf & "	From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Resource AS A INNER Join"
            cmdstring &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B  WITH (NOLOCK) ON A.FNHSysRawMatId = B.FNHSysRawMatId LEFT OUTER Join"
            cmdstring &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK)  ON B.FNHSysRawMatColorId = C.FNHSysRawMatColorId"

            cmdstring &= vbCrLf & "  INNER Join @TabRC  AS CC ON A.FTOrderNo=CC.FTOrderNo And A.FNHSysRawMatId = CC.FNHSysRawMatId"

            cmdstring &= vbCrLf & "   OUTER APPLY("

            cmdstring &= vbCrLf & "   Select  FTOrderNo, FTRawMatCode, FTRawMatColorCode, SUM(FNTotal) As FNTotal, MAX(FNHSysUnitId) As FNHSysUnitId"
            cmdstring &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan AS O WITH (NOLOCK)"
            cmdstring &= vbCrLf & "   Where O.FTOrderNo = A.FTOrderNo And O.FTRawMatCode = ISNULL(B.FTRawMatCode,'')  AND O.FTRawMatColorCode  = ISNULL(C.FTRawMatColorCode,'')"
            cmdstring &= vbCrLf & "   GROUP BY FTOrderNo, FTRawMatCode, FTRawMatColorCode"

            cmdstring &= vbCrLf & "  ) AS MZ"

            cmdstring &= vbCrLf & "  OUTER APPLY("
            cmdstring &= vbCrLf & "	SELECT TOP 1 UCV.FNRateTo , UCV.FNRateFrom"
            cmdstring &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert AS UCV "
            cmdstring &= vbCrLf & "   Where UCV.FNHSysUnitId = MZ.FNHSysUnitId And UCV.FNHSysUnitIdTo = A.FNHSysUnitId"
            cmdstring &= vbCrLf & "  ) As UCV "

            cmdstring &= vbCrLf & "  WHERE CC.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"


            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

        Else

            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].SP_CALCMPR_GETDATA " & _FNHSysBuyId & ", " & _FNHSysStyleId & ",'" & _allOrder & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Filter) & "'"

            Application.DoEvents()
            HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, dtsetBomInfo)

            dtBomInfo = dtsetBomInfo.Tables(0).Copy

            If Calculated = False Then
                dtitem = dtsetBomInfo.Tables(1).Copy
            End If

            Me.ogcmatcode.DataSource = dtBomInfo

        End If

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

                '''''''''  Generate_Resource(0, _allOrder, Filter)
                '''
                'End If

                HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Calculate MPR (Style " & FNHSysStyleId.Text & " FO " & _allOrder & "  Item " & _FilterString & ") ")

            Else

                If Not dtitem Is Nothing Then

                    If dtitem.Rows.Count > 0 Then
                        For Each R As DataRow In dtitem.Rows

                            Dim DataRawMatId As Long = HI.SE.RunID.GetRunNoID("TINVENMMaterial", "FNHSysRawMatId", Conn.DB.DataBaseName.DB_MASTER)

                            If DataRawMatId > 0 Then

                                _Qry = "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial "
                                _Qry &= vbCrLf & "	(FTInsUser, FDInsDate, FTInsTime, FNHSysRawMatId, FTRawMatCode, FTRawMatNameTH, FTRawMatNameEN, FNHSysRawMatColorId, FTRawMatColorNameTH, "
                                _Qry &= vbCrLf & "FTRawMatColorNameEN, FNHSysRawMatSizeId, FNHSysUnitId, FTFabricFrontSize, FTRemark, FPImageRawMat, FTStateActive)"
                                _Qry &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                _Qry &= vbCrLf & "  ," & HI.UL.ULDate.FormatDateDB & ""
                                _Qry &= vbCrLf & "  ," & HI.UL.ULDate.FormatTimeDB & ""
                                _Qry &= vbCrLf & "  ," & DataRawMatId & ""
                                _Qry &= vbCrLf & "   ,'" & HI.UL.ULF.rpQuoted(R!FTRawMatCode.ToString) & "' "
                                _Qry &= vbCrLf & "   ,'" & HI.UL.ULF.rpQuoted(R!FTRawMatNameTH.ToString) & "' "
                                _Qry &= vbCrLf & "  ,'" & HI.UL.ULF.rpQuoted(R!FTRawMatNameEN.ToString) & "' "
                                _Qry &= vbCrLf & "  ," & Val(R!FNHSysRawMatColorId.ToString) & " "
                                _Qry &= vbCrLf & "  ,'" & HI.UL.ULF.rpQuoted(R!FTRawMatColorNameTH.ToString) & "' "
                                _Qry &= vbCrLf & "  ,'" & HI.UL.ULF.rpQuoted(R!FTRawMatColorNameEN.ToString) & "' "
                                _Qry &= vbCrLf & "   ," & Val(R!FNHSysRawMatSizeId.ToString) & " "
                                _Qry &= vbCrLf & "    ,isnull((SELECT  TOP 1  ISNULL(IB.FNHSysUnitId,0) as FNHSysUnitId "
                                _Qry &= vbCrLf & " 	 FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IB  With(NOLOCK)  INNER JOIN "
                                _Qry &= vbCrLf & " 	      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS RA  WITH(NOLOCK)  ON IB.FNHSysRawMatId = RA.FNHSysRawMatId "
                                _Qry &= vbCrLf & "     WHERE ISNULL(IB.FTRawMatCode,'') ='" & HI.UL.ULF.rpQuoted(R!FTRawMatCode.ToString) & "' "
                                _Qry &= vbCrLf & "  ),ISNULL(( "
                                _Qry &= vbCrLf & "   Select  TOP 1  ISNULL(IB.FNHSysUnitId,0) As FNHSysUnitId "
                                _Qry &= vbCrLf & "    FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IB  WITH(NOLOCK)  INNER JOIN "
                                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENAdjustStock_AddIn_Detail As RA  With(NOLOCK)  On IB.FNHSysRawMatId = RA.FNHSysRawMatId "
                                _Qry &= vbCrLf & "    WHERE ISNULL(IB.FTRawMatCode,'') ='" & HI.UL.ULF.rpQuoted(R!FTRawMatCode.ToString) & "' "
                                _Qry &= vbCrLf & " 	 )," & Val(R!FNHSysUnitId.ToString) & ")) As FNHSysUnitId "
                                _Qry &= vbCrLf & "  ,'" & HI.UL.ULF.rpQuoted(R!FTFabricFrontSize.ToString) & "' "
                                _Qry &= vbCrLf & "  ,'" & HI.UL.ULF.rpQuoted(R!FTRemark.ToString) & "' "
                                _Qry &= vbCrLf & "  ,'' AS FPImageRawMat"
                                _Qry &= vbCrLf & "  ,'1'"

                                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                            End If

                        Next

                        Call LoadBOMInfo(_FNHSysBuyId, _FNHSysStyleId, _OrderNo, Calculated, _Save2Table, _FilterString, _Spl)

                    End If

                End If


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

        ' HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

    End Sub


    Private Sub LoadMPRInfo(ByVal _FNHSysBuyId As String, ByVal _FNHSysStyleId As String, ByVal _OrderNo As String, Optional ByVal Calculated As Boolean = False, Optional ByVal _Save2Table As Boolean = False)
        If _FNHSysBuyId = "" Then _FNHSysBuyId = "0"
        If _FNHSysStyleId = "" Then _FNHSysStyleId = "0"

        Dim _Qry As String = ""

        Dim dtMPRInfo As New DataTable

        Try

            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GETBREAKDOWN_GETMPR_BY_BD] " & Val(_FNHSysBuyId) & "," & Val(_FNHSysStyleId) & ",'" & _OrderNo & "'," & IIf(_Save2Table, 1, 0) & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            dtMPRInfo = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Me.GridCalculated.DataSource = dtMPRInfo

            '  GridView3 = GridCalculated.Views(0)

            GridView3.OptionsView.ShowAutoFilterRow = True
            GridView3.BestFitColumns()
            ogvmatcode.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200
            GridView3.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200

            'Me.GridCalculated = GridView3.GridControl
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


    End Sub
    Private Sub LoadMPRInfoNotIn(ByVal _FNHSysBuyId As String, ByVal _FNHSysStyleId As String, ByVal _OrderNo As String, Optional ByVal Calculated As Boolean = False, Optional ByVal _Save2Table As Boolean = False)
        If _FNHSysBuyId = "" Then _FNHSysBuyId = "0"
        If _FNHSysStyleId = "" Then _FNHSysStyleId = "0"

        Dim _Qry As String = ""

        Dim dtMPRInfo As New DataTable
        Try


            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GETBREAKDOWN_GETMPR_NOTIN] " & Val(_FNHSysBuyId) & "," & Val(_FNHSysStyleId) & ",'" & _OrderNo & "'," & IIf(_Save2Table, 1, 0) & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            dtMPRInfo = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
            Me.ogcmrpnot.DataSource = dtMPRInfo
            '  Dim view2 As GridView

            'view2 = ogcmrpnot.Views(0)
            ogvmrpnot.OptionsView.ShowAutoFilterRow = True
            ogvmrpnot.BestFitColumns()
            ogvmatcode.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200
            ogvmrpnot.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200
            '  Me.GridCalculated = view2.GridControl
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


    End Sub

    Private Function CreateNewRawMat(ByVal _OrderNo As String, _FilterString As String) As Boolean
        Dim ret As Boolean = True

        'HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MASTER)
        'HI.Conn.SQLConn.SqlConnectionOpen()
        'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Try
            Dim _Str As String = ""
            Dim dt As DataTable = New DataTable()
            _Str = "Select DISTINCT [FNHSysRawMatId]" &
                     ",[FTRawMatCode] " &
                     ",[FTRawMatNameTH] " &
                     ",[FTRawMatNameEN] " &
                     ",[FNHSysRawMatColorId] " &
                     ",[FTRawMatColorNameTH]" &
                     ",[FTRawMatColorNameEN]" &
                     ",[FNHSysRawMatSizeId] " &
                     ",[FNHSysUnitId] " &
                     ",[FTFabricFrontSize] " &
                     ",[FTRemark] " &
                     ",[FPImageRawMat] " &
                     ",[FTStateActive]  " & vbCrLf &
                     " FROM tempdb..TINVENMMaterial ORDER BY FTRawMatCode, FNHSysRawMatColorId, FNHSysRawMatSizeId"

            'oleDbDataAdapter2 = New SqlClient.SqlDataAdapter(_Str, HI.Conn.SQLConn._ConnString)
            'oleDbDataAdapter2.Fill(dt)

            dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

            Dim _StateUsedUnit As Integer = 0
            For Each r As DataRow In dt.Rows
                Try
                    _Str = "Select TOP 1 FNHSysRawMatId  FROM " & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TINVENMMaterial] With (NOLOCK)"
                    _Str &= vbCrLf & "WHERE [FTRawMatCode] = '" & HI.UL.ULF.rpQuoted(r!FTRawMatCode.ToString) & "' "
                    _Str &= vbCrLf & "AND [FNHSysRawMatColorId] = " & r!FNHSysRawMatColorId.ToString & " "
                    _Str &= vbCrLf & "AND [FNHSysRawMatSizeId] = " & r!FNHSysRawMatSizeId.ToString & " "
                    'Dim SelectCMD As SqlCommand = New SqlCommand(_Str, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                    Dim cnt As Integer
                    ' cnt = SelectCMD.ExecuteScalar
                    cnt = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "0")))
                    _StateUsedUnit = 0
                    Dim GetSysRawmatID As Integer = 0

                    If cnt = 0 Then
                        '_Str = "EXEC HITECH_SYSTEM.dbo.SP_GET_SYSID 'TINVENMMaterial', 'FNHSysRawMatId', 'HITECH_MASTER'"
                        'GetSysRawmatID = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_SYSTEM)
                        'HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        '  _Str = "EXEC HITECH_SYSTEM.dbo.SP_GET_SYSID 'TINVENMMaterial', 'FNHSysRawMatId', 'HITECH_MASTER'"
                        GetSysRawmatID = Integer.Parse(HI.SE.RunID.GetRunNoID("TINVENMMaterial", "FNHSysRawMatId", Conn.DB.DataBaseName.DB_MASTER))

                        _Str = "UPDATE TMERTMPR SET FNHSysRawMatId = " & GetSysRawmatID
                        _Str &= vbCrLf & "WHERE [FTMainMatCode] = '" & HI.UL.ULF.rpQuoted(r!FTRawMatCode.ToString) & "' "
                        _Str &= vbCrLf & "AND [FNHSysRawMatColorId] = " & r!FNHSysRawMatColorId.ToString & " "
                        _Str &= vbCrLf & "AND [FNHSysRawMatSizeId] = " & r!FNHSysRawMatSizeId.ToString & " "
                        _Str &= vbCrLf & "AND ISNULL([FNHSysRawMatId], 0) = 0"
                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)
                        ' HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                        If r!FTRawMatCode.ToString <> "" Then

                            _StateUsedUnit = 0

                            _Str = "    SELECT      TOP 1   ISNULL(B.FNHSysUnitId,0) as FNHSysUnitId"
                            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TINVENMMaterial AS B  WITH(NOLOCK)  INNER JOIN"
                            _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].[dbo].TINVENReceive_Detail AS A  WITH(NOLOCK)  ON B.FNHSysRawMatId = A.FNHSysRawMatId"
                            _Str &= vbCrLf & " WHERE ISNULL(B.FTRawMatCode,'') ='" & HI.UL.ULF.rpQuoted(r!FTRawMatCode.ToString) & "' "

                            _StateUsedUnit = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "0")))

                            If _StateUsedUnit = 0 Then

                                _Str = "	SELECT      TOP 1  ISNULL(B.FNHSysUnitId,0) as FNHSysUnitId"
                                _Str &= vbCrLf & " 	FROM        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TINVENMMaterial AS B  WITH(NOLOCK)  INNER JOIN"
                                _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].[dbo].TINVENAdjustStock_AddIn_Detail AS A  WITH(NOLOCK)  ON B.FNHSysRawMatId = A.FNHSysRawMatId"
                                _Str &= vbCrLf & " WHERE ISNULL(B.FTRawMatCode,'') ='" & HI.UL.ULF.rpQuoted(r!FTRawMatCode.ToString) & "' "

                                _StateUsedUnit = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "0")))

                            End If

                            If _StateUsedUnit = 0 Then
                                _Str = "    SELECT TOP 1  FNHSysUnitId"
                                _Str &= vbCrLf & " 	FROM        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TCNMUnit WITH(NOLOCK)  "
                                _Str &= vbCrLf & "  WHERE  (FNHSysUnitId = " & Integer.Parse(Val(r!FNHSysUnitId.ToString)) & ")  AND  (FTStateUnitStock = '1') "

                                _StateUsedUnit = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "0")))
                            End If

                            _Str = "INSERT INTO " & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TINVENMMaterial]"

                            _Str &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysRawMatId, FTRawMatCode, FTRawMatNameTH, FTRawMatNameEN, FNHSysRawMatColorId, FTRawMatColorNameTH,"
                            _Str &= vbCrLf & "FTRawMatColorNameEN, FNHSysRawMatSizeId, FNHSysUnitId, FTFabricFrontSize, FTRemark, FPImageRawMat, FTStateActive)"

                            _Str &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Str &= vbCrLf & "," & GetSysRawmatID
                            _Str &= vbCrLf & "," & "'" & HI.UL.ULF.rpQuoted(r!FTRawMatCode.ToString) & "'"
                            _Str &= vbCrLf & "," & "'" & HI.UL.ULF.rpQuoted(r!FTRawMatNameTH.ToString) & "'"
                            _Str &= vbCrLf & "," & "'" & HI.UL.ULF.rpQuoted(r!FTRawMatNameEN.ToString) & "'"
                            _Str &= vbCrLf & "," & r!FNHSysRawMatColorId.ToString
                            _Str &= vbCrLf & "," & "'" & HI.UL.ULF.rpQuoted(r!FTRawMatColorNameTH.ToString) & "'"
                            _Str &= vbCrLf & "," & "'" & HI.UL.ULF.rpQuoted(r!FTRawMatColorNameEN.ToString) & "'"
                            _Str &= vbCrLf & "," & r!FNHSysRawMatSizeId.ToString
                            _Str &= vbCrLf & "," & _StateUsedUnit
                            _Str &= vbCrLf & "," & "'" & HI.UL.ULF.rpQuoted(r!FTFabricFrontSize.ToString) & "'"
                            _Str &= vbCrLf & "," & "'' "
                            _Str &= vbCrLf & "," & "'' "
                            _Str &= vbCrLf & "," & "'1'"
                            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

                            'HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                            'HI.Conn.SQLConn.Tran.Commit()

                        End If
                    Else
                        Try
                            GetSysRawmatID = cnt
                            '  HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
                            _Str = "UPDATE " & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTMPR] SET FNHSysRawMatId =" & Integer.Parse(GetSysRawmatID) & ""
                            _Str &= vbCrLf & "WHERE [FTMainMatCode] = '" & HI.UL.ULF.rpQuoted(r!FTRawMatCode.ToString) & "' "
                            _Str &= vbCrLf & "AND [FNHSysRawMatColorId] = " & r!FNHSysRawMatColorId.ToString & " "
                            _Str &= vbCrLf & "AND [FNHSysRawMatSizeId] = " & r!FNHSysRawMatSizeId.ToString & " "
                            _Str &= vbCrLf & "AND ISNULL([FNHSysRawMatId], 0) = 0"

                            'HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                            'HI.Conn.SQLConn.Tran.Commit()
                            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

                            _Str = "    SELECT      TOP 1  ISNULL(B.FNHSysUnitId,0) as FNHSysUnitId "
                            _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TINVENMMaterial AS B  WITH(NOLOCK)  INNER JOIN"
                            _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].[dbo].TINVENReceive_Detail AS A  WITH(NOLOCK)  ON B.FNHSysRawMatId = A.FNHSysRawMatId"
                            _Str &= vbCrLf & " WHERE ISNULL(B.FTRawMatCode,'') = ISNULL((SELECt TOP 1  ISNULL(Z.FTRawMatCode,'') "
                            _Str &= vbCrLf & "	 FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TINVENMMaterial AS Z  WITH(NOLOCK) "
                            _Str &= vbCrLf & "	 WHERE Z.FNHSysRawMatId =" & Integer.Parse(GetSysRawmatID) & ""
                            _Str &= vbCrLf & "	 ),'')"

                            _StateUsedUnit = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "0")))

                            If _StateUsedUnit = 0 Then

                                _Str = "	SELECT      TOP 1  ISNULL(B.FNHSysUnitId,0) as FNHSysUnitId "
                                _Str &= vbCrLf & " 	FROM        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TINVENMMaterial AS B  WITH(NOLOCK)  INNER JOIN"
                                _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].[dbo].TINVENAdjustStock_AddIn_Detail AS A  WITH(NOLOCK)  ON B.FNHSysRawMatId = A.FNHSysRawMatId"
                                _Str &= vbCrLf & " WHERE ISNULL(B.FTRawMatCode,'') = ISNULL((SELECt TOP 1  ISNULL(Z.FTRawMatCode,'') "
                                _Str &= vbCrLf & "	 FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TINVENMMaterial AS Z  WITH(NOLOCK) "
                                _Str &= vbCrLf & "	 WHERE Z.FNHSysRawMatId=" & Integer.Parse(GetSysRawmatID) & ""
                                _Str &= vbCrLf & "	 ),'')"

                                _StateUsedUnit = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "0")))

                            End If

                            If _StateUsedUnit = 0 Then

                                _Str = "    SELECT TOP 1  FNHSysUnitId"
                                _Str &= vbCrLf & " 	FROM        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TCNMUnit WITH(NOLOCK)  "
                                _Str &= vbCrLf & "  WHERE  (FNHSysUnitId = " & Integer.Parse(Val(r!FNHSysUnitId.ToString)) & ")  AND  (FTStateUnitStock = '1') "

                                _StateUsedUnit = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "0")))

                            End If

                            _Str = "Update " & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TINVENMMaterial]"
                            _Str &= vbCrLf & " SET FTRawMatColorNameTH=N'" & HI.UL.ULF.rpQuoted(r!FTRawMatColorNameTH.ToString) & "'"
                            _Str &= vbCrLf & ",FTRawMatColorNameEN=N'" & HI.UL.ULF.rpQuoted(r!FTRawMatColorNameEN.ToString) & "'"
                            _Str &= vbCrLf & ",FNHSysUnitId = " & Integer.Parse(_StateUsedUnit) & " "
                            _Str &= vbCrLf & ",FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                            _Str &= vbCrLf & ",FTUpdTime" & HI.UL.ULDate.FormatTimeDB
                            _Str &= vbCrLf & " WHERE FNHSysRawMatId = " & Integer.Parse(GetSysRawmatID) & " AND FNHSysUnitId <>" & Integer.Parse(_StateUsedUnit) & ""

                            '_Str &= vbCrLf & " Update " & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TINVENMMaterial]"
                            '_Str &= vbCrLf & " SET FTRawMatColorNameTH=N'" & HI.UL.ULF.rpQuoted(r!FTRawMatColorNameTH.ToString) & "'"
                            '_Str &= vbCrLf & ",FTRawMatColorNameEN=N'" & HI.UL.ULF.rpQuoted(r!FTRawMatColorNameEN.ToString) & "'"
                            '_Str &= vbCrLf & ",FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            '_Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                            '_Str &= vbCrLf & ",FTUpdTime" & HI.UL.ULDate.FormatTimeDB
                            '_Str &= vbCrLf & " WHERE FTRawMatCode IN (SELECT TOP 1 FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial WHERE FNHSysRawMatId = " & Integer.Parse(GetSysRawmatID) & " )  "
                            '_Str &= vbCrLf & " AND [FNHSysRawMatColorId] = " & Integer.Parse(Val(r!FNHSysRawMatColorId.ToString)) & " "
                            '_Str &= vbCrLf & " AND ( FTRawMatColorNameTH<>N'" & HI.UL.ULF.rpQuoted(r!FTRawMatColorNameTH.ToString) & "' OR FTRawMatColorNameEN<>N'" & HI.UL.ULF.rpQuoted(r!FTRawMatColorNameEN.ToString) & "' )"

                            HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

                            '_Str = "Update " & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TINVENMMaterial]"
                            '_Str &= vbCrLf & " SET FTRawMatColorNameTH=N'" & HI.UL.ULF.rpQuoted(r!FTRawMatColorNameTH.ToString) & "'"
                            '_Str &= vbCrLf & ",FTRawMatColorNameEN=N'" & HI.UL.ULF.rpQuoted(r!FTRawMatColorNameEN.ToString) & "'"
                            '_Str &= vbCrLf & ",FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            '_Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                            '_Str &= vbCrLf & ",FTUpdTime" & HI.UL.ULDate.FormatTimeDB
                            '_Str &= vbCrLf & " WHERE FTRawMatCode IN (SELECT TOP 1 FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial WHERE FNHSysRawMatId = " & Integer.Parse(GetSysRawmatID) & " )  "
                            '_Str &= vbCrLf & " AND [FNHSysRawMatColorId] = " & Integer.Parse(Val(r!FNHSysRawMatColorId.ToString)) & " "
                            ' HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

                        Catch ex As Exception
                            'HI.Conn.SQLConn.Tran.Rollback()
                            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        End Try

                    End If

                    If _FilterString <> "" Then
                        Generate_Resource(GetSysRawmatID, _OrderNo, _FilterString)
                    End If

                Catch ex As Exception
                    'MsgBox(r!FTRawMatCode.ToString)
                End Try
            Next

            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception
            'HI.Conn.SQLConn.Tran.Rollback()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            ret = False
        End Try

        Return ret
    End Function

    Private Function CreateNewRawMatColor() As Boolean
        Dim ret As Boolean = True

        'HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MASTER)
        'HI.Conn.SQLConn.SqlConnectionOpen()
        'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Try
            Dim _Str As String = ""
            Dim dt As DataTable = New DataTable()
            _Str = "SELECT DISTINCT [FNHSysRawMatColorId]" &
                     ",[FTRawMatColorCode] " &
                     ",[FNRawMatColorSeq] " &
                     ",[FTRawMatColorNameTH] " &
                     ",[FTRawMatColorNameEN] " &
                     ",[FTRemark] " &
                     ",[FTStateActive] " & vbCrLf &
                     "FROM tempdb..TINVENMMatColor ORDER BY [FNRawMatColorSeq], [FTRawMatColorCode]"
            'oleDbDataAdapter2 = New SqlClient.SqlDataAdapter(_Str, HI.Conn.SQLConn._ConnString)
            'oleDbDataAdapter2.Fill(dt)
            dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)
            For Each r As DataRow In dt.Rows
                _Str = "SELECT TOP 1  FNHSysRawMatColorId FROM " & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TINVENMMatColor]"
                _Str &= vbCrLf & "WHERE [FTRawMatColorCode] = '" & HI.UL.ULF.rpQuoted(r!FTRawMatColorCode.ToString) & "' "
                ' Dim SelectCMD As SqlCommand = New SqlCommand(_Str, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                Dim cnt As Integer
                ' cnt = SelectCMD.ExecuteScalar
                cnt = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "0")))
                If cnt = 0 Then
                    ' HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
                    Dim GetSysRawmatID As Integer = 0 ' HI.Conn.SQLConn.GetField("EXEC HITECH_SYSTEM.dbo.SP_GET_SYSID 'TINVENMMatColor', 'FNHSysRawMatColorId', 'HITECH_MASTER'", Conn.DB.DataBaseName.DB_SYSTEM)

                    GetSysRawmatID = Integer.Parse(HI.SE.RunID.GetRunNoID("TINVENMMatColor", "FNHSysRawMatColorId", Conn.DB.DataBaseName.DB_MASTER))


                    If Val(r!FNHSysRawMatColorId) > 0 Then
                        _Str = "INSERT INTO " & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TINVENMMatColor]"
                        _Str &= vbCrLf & "( FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysRawMatColorId, FTRawMatColorCode, FNRawMatColorSeq, FTRawMatColorNameTH, FTRawMatColorNameEN,"
                        _Str &= vbCrLf & "FTRemark, FTStateActive, FTGCWNo)"
                        _Str &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & "," & GetSysRawmatID
                        _Str &= vbCrLf & "," & "'" & HI.UL.ULF.rpQuoted(r!FTRawMatColorCode.ToString) & "'"
                        _Str &= vbCrLf & "," & "'" & r!FNRawMatColorSeq.ToString & "'"
                        _Str &= vbCrLf & "," & "'" & HI.UL.ULF.rpQuoted(r!FTRawMatColorNameTH.ToString) & "'"
                        _Str &= vbCrLf & "," & "'" & HI.UL.ULF.rpQuoted(r!FTRawMatColorNameEN.ToString) & "'"
                        _Str &= vbCrLf & "," & "'' "
                        _Str &= vbCrLf & "," & "'1',''"

                        'HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                        'HI.Conn.SQLConn.Tran.Commit()

                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)
                    End If
                End If

            Next

            'HI.Conn.SQLConn.Tran.Commit()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception
            'HI.Conn.SQLConn.Tran.Rollback()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            ret = False
        End Try

        Return ret
    End Function

    Private Function CreateNewRawMatSize() As Boolean
        Dim ret As Boolean = True

        'HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MASTER)
        'HI.Conn.SQLConn.SqlConnectionOpen()
        'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Try
            Dim _Str As String = ""
            Dim dt As DataTable = New DataTable()
            _Str = "SELECT DISTINCT [FNHSysRawMatSizeId]" &
                     ",[FTRawMatSizeCode] " &
                     ",ISNULL([FNRawMatSizeSeq], 0) AS [FNRawMatSizeSeq] " &
                     ",[FTRawMatSizeNameTH] " &
                     ",[FTRawMatSizeNameEN] " &
                     ",[FTRemark] " &
                     ",[FTStateActive] " & vbCrLf &
                     "FROM tempdb..TINVENMMatSize ORDER BY [FNRawMatSizeSeq], [FTRawMatSizeCode]"
            'oleDbDataAdapter2 = New SqlClient.SqlDataAdapter(_Str, HI.Conn.SQLConn._ConnString)
            'oleDbDataAdapter2.Fill(dt)

            dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)
            For Each r As DataRow In dt.Rows
                _Str = "SELECT TOP 1  FNHSysRawMatSizeId FROM " & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TINVENMMatSize]"
                _Str &= vbCrLf & "WHERE [FTRawMatSizeCode] = '" & HI.UL.ULF.rpQuoted(r!FTRawMatSizeCode.ToString) & "' "
                '  Dim SelectCMD As SqlCommand = New SqlCommand(_Str, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                Dim cnt As Integer
                ' cnt = SelectCMD.ExecuteScalar
                cnt = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "0")))
                If cnt = 0 Then
                    ' HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
                    Dim GetSysRawmatID As Integer = 0 ' HI.Conn.SQLConn.GetField("EXEC HITECH_SYSTEM.dbo.SP_GET_SYSID 'TINVENMMatSize', 'FNHSysRawMatSizeId', 'HITECH_MASTER'", Conn.DB.DataBaseName.DB_SYSTEM)

                    GetSysRawmatID = Integer.Parse(HI.SE.RunID.GetRunNoID("TINVENMMatSize", "FNHSysRawMatSizeId", Conn.DB.DataBaseName.DB_MASTER))
                    If Val(r!FNHSysRawMatSizeId) > 0 Then
                        _Str = "INSERT INTO " & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TINVENMMatSize]"
                        _Str &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysRawMatSizeId, FTRawMatSizeCode, FNRawMatSizeSeq, FTRawMatSizeNameTH, FTRawMatSizeNameEN, FTRemark, "
                        _Str &= vbCrLf & "FTStateActive )"

                        _Str &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Str &= vbCrLf & "," & GetSysRawmatID
                        _Str &= vbCrLf & "," & "'" & HI.UL.ULF.rpQuoted(r!FTRawMatSizeCode.ToString) & "'"
                        _Str &= vbCrLf & "," & "'" & r!FNRawMatSizeSeq.ToString & "'"
                        _Str &= vbCrLf & "," & "'" & HI.UL.ULF.rpQuoted(r!FTRawMatSizeNameTH.ToString) & "'"
                        _Str &= vbCrLf & "," & "'" & HI.UL.ULF.rpQuoted(r!FTRawMatSizeNameEN.ToString) & "'"
                        _Str &= vbCrLf & "," & "'' "
                        _Str &= vbCrLf & "," & "'1'"

                        'HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                        'HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

                    End If

                End If

            Next

            'HI.Conn.SQLConn.Tran.Commit()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception
            'HI.Conn.SQLConn.Tran.Rollback()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            ret = False
        End Try

        Return ret
    End Function

    Private Function Generate_Resource(ByVal _FNHSysRawmatID As Integer, ByVal _OrderNo As String, _FilterString As String) As Boolean
        Dim ret As Boolean = True

        Try

            'HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            'HI.Conn.SQLConn.SqlConnectionOpen()
            'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

            Dim _Qry As String = ""
            '_Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GENERATE_RESOURCE] " & _FNHSysRawmatID & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].SP_GENERATE_RESOURCE_SOURCING " & _FNHSysRawmatID & ",'" & HI.UL.ULF.rpQuoted(_OrderNo) & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_FilterString) & "'"

            'Application.DoEvents()
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Catch ex As Exception
            ret = False
        End Try

        Return ret
    End Function

    Private Sub ocmcalcmrp_Click(sender As System.Object, e As System.EventArgs) Handles ocmcalc.Click

        If Me.FNHSysBuyId.Text <> "" Or FNHSysStyleId.Text <> "" Then

            With CType(ogcmatcode.DataSource, DataTable)
                .AcceptChanges()

                If .Rows.Count <= 0 Then
                    Exit Sub
                End If
            End With

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
                'Dim dtBefore As DataTable
                'Dim dtAfter As DataTable
                'Dim sqlLog As String =
                '         "SELECT   TOP 1   ST.FTStyleCode" & vbCrLf &
                '         "FROM         HITECH_MERCHAN.dbo.TMERTStyle AS ST " & vbCrLf &
                '         "WHERE ST.FNHSysStyleId = " & Val(Me.FNHSysStyleId.Properties.Tag.ToString)

                'dtBefore = HI.Conn.SQLConn.GetDataTable(sqlLog, Conn.DB.DataBaseName.DB_MERCHAN)

                If Me.DeleteAllData(_Spls, FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, OrderNo, True) Then

                    'dtAfter = HI.Conn.SQLConn.GetDataTable(sqlLog & "  ST.FNHSysStyleId=0 ", Conn.DB.DataBaseName.DB_MERCHAN)

                    '  Call PostSave(dtBefore, dtAfter, "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle]", Me.FNHSysStyleId.Text)

                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    ' HI.TL.HandlerControl.ClearControl(Me)

                    'Call FNHSysStyleId_EditValueChanged(FNHSysStyleId, New System.EventArgs)

                    Me.otb.SelectedTabPageIndex = 1

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

                '  Dim tSeqNo As Integer

                _Str = "SELECT TOP 1 FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FNHSysCustId, FNHSysSeasonId, FTStateActive FROM " &
                    "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle] WHERE FNHSysStyleId = " & Val(Me.FNHSysBuyId.Properties.Tag.ToString) & " "
                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    ''Insert Header
                    Dim SelectCMD As SqlCommand = New SqlCommand(_Str, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                    'SelectCMD.Parameters.AddWithValue("@FNHSysStyleId", (Val(Me.FNHSysStyleId.Properties.Tag.ToString))

                    Dim cnt As Integer
                    cnt = SelectCMD.ExecuteScalar

                    If cnt = 0 Then
                        _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle] " &
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
                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle] SET " &
                        "FNHSysStyleId = '" & Val(Me.FNHSysStyleId.Properties.Tag.ToString) & "', " &
                        "FTStyleCode = '" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "', " &
                        "FTStateActive = '" & 1 & "', " &
                        "FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', " &
                        "FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ", " &
                        "FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & "" &
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
        Catch 'ex As Exception
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


        Next

        SplScreen.Dispose()

        Me.GridCalculated.Refresh()
        Call LoadMPRInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
        Call LoadMRPReport(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")

    End Sub

    Private Sub ocmpreview_Click(sender As System.Object, e As System.EventArgs) Handles ocmpreview.Click

        With CType(GridOrderList.DataSource, DataTable)
            .AcceptChanges()

            If .Select("FNSelect='1'").Length <= 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Job Order No !!!", 1508255471, Me.Text, , MessageBoxIcon.Warning)
                Exit Sub
            End If


            Dim dtins As New DataTable
            dtins = .Select("FNSelect='1'").CopyToDataTable()


            HI.Conn.SQLConn.ExecuteStoredProcedure(HI.ST.UserInfo.UserName, "USP_IMPORTTEMPORDERNO", "@tblOrder", dtins, Conn.DB.DataBaseName.DB_MERCHAN)

        End With

        Dim currentCursor As Cursor = Cursor.Current
        Cursor.Current = Cursors.WaitCursor

        ''** DevExpress Report
        ' GridCalculated.ShowPrintPreview()
        'GridCalculated.ShowRibbonPrintPreview()

        ''** Crystal Report
        ' Dim strReportPath As String = Application.StartupPath & "\Reports\Merchandise Report\Mrpreport.rpt"
        'GenCrystalReport("Merchandise Report\", "Mrpreport.rpt")
        GenCrystalReport("Merchandise Report\", "Mrpreport_CreateTemp.rpt")
        Cursor.Current = currentCursor
    End Sub


    Private Sub GridView1_PrintInitialize(sender As System.Object,
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
        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

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

    Private Function DoUpdateTable(ByVal dataAdapter As SqlDataAdapter,
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


        Dim FilterString As String = ""
        Dim FilterStringAll As String = ""
        Dim FilterStringIn As String = ""

        Dim K As String = ""
        Dim KValue As String = ""
        Try
            'For Each colX As GridColumn In ogvmatcode.Columns

            '    K = ""
            '    KValue = ""

            '    Try
            '        K = colX.FilterInfo.FilterString
            '    Catch ex As Exception
            '    End Try

            '    Try
            '        KValue = colX.FilterInfo.Value.ToString
            '    Catch ex As Exception
            '    End Try

            '    Select Case colX.FieldName
            '        Case "FNHSysMerMatId_None"
            '    End Select
            '    Try

            '        Select Case colX.FieldName
            '            Case "FNHSysMerMatId_None"
            '            Case Else

            '                'Dim xValue As String = view.ActiveFilter.Item(colX).Filter.Value
            '                ''If xValue.Contains("*") Then
            '                'FilterString += " AND A." & colX.FieldName & " LIKE ('%" & Replace(xValue, "*", "%%%") & "%')" & vbCrLf
            '                ''Else
            '                ''    FilterString += " AND " & colX.FieldName & " = '" & xValue & "'" & vbCrLf
            '                ''End If

            '                If K <> "" Then

            '                    If FilterStringIn = "" Then
            '                        ' FilterStringIn = " AND A." & colX.FieldName & " IN ('" & HI.UL.ULF.rpQuoted(KValue) & "'"
            '                        FilterStringIn = "  AND  A." & K.Replace("Or [", "Or A.[")
            '                    Else
            '                        'FilterStringIn = FilterStringIn & ",'" & HI.UL.ULF.rpQuoted(KValue) & "'"
            '                        FilterStringIn = FilterStringIn & "  AND  A." & K
            '                    End If

            '                End If

            '                If KValue <> "" Then
            '                    FilterString += " AND A." & colX.FieldName & " LIKE ('%" & Replace(KValue, "*", "%%%") & "%')" & vbCrLf
            '                End If

            '        End Select

            '    Catch ex As Exception
            '    End Try

            'Next

            'If FilterStringIn <> "" Then
            '    'FilterStringIn = FilterStringIn & ""
            'End If


            Dim Filter As String = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(ogvmatcode.ActiveFilterCriteria)


            If Filter <> "" Then

                Filter = "  AND  " & Filter
            End If


            Filter = Filter.Replace("[", "[A.")
            Filter = Filter.Replace("[", "").Replace("]", "")

            Dim cmdstring As String = ""

            If Filter <> "" Then

                cmdstring &= vbCrLf & " DELETE A "
                cmdstring &= vbCrLf & " 	From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTMPR] AS A ,[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTMPR_TMP AS B "
                cmdstring &= vbCrLf & " 	WHERE	B.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                cmdstring &= vbCrLf & " 	  And A.FNHSysStyleId=B.FNHSysStyleId "
                cmdstring &= vbCrLf & "       And A.FNHSysStyleId=B.FNHSysStyleId"
                cmdstring &= vbCrLf & "       And A.FNHSysMerMatId=B.FNHSysMerMatId"
                'cmdstring &= vbCrLf & "       And A.FNHSysMatColorId=B.FNHSysMatColorId"
                'cmdstring &= vbCrLf & "       And A.FNHSysMatSizeId=B.FNHSysMatSizeId "
                cmdstring &= vbCrLf & "       And A.FTOrderNo = B.FTOrderNo "
                cmdstring &= vbCrLf & "    " & Filter

            Else

                cmdstring &= vbCrLf & " DELETE A "
                cmdstring &= vbCrLf & " 	From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTMPR] AS A ,[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTMPR_TMP AS B "
                cmdstring &= vbCrLf & " 	WHERE	B.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                cmdstring &= vbCrLf & " 	  And A.FNHSysStyleId=B.FNHSysStyleId "
                cmdstring &= vbCrLf & "       And A.FTOrderNo=B.FTOrderNo "

            End If



            cmdstring &= vbCrLf & "   Declare @TMERTOrder_MatColor AS TABLE( "
            cmdstring &= vbCrLf & "   [FNHSysStyleId] [Int] Not NULL, "
            cmdstring &= vbCrLf & "    [FTOrderNo] [nvarchar](30) Not NULL, "
            cmdstring &= vbCrLf & "    [FNHSysMainMatId] [Int] Not NULL, "
            cmdstring &= vbCrLf & "     [FNHSysRawMatColorId] [Int] Not NULL, "
            cmdstring &= vbCrLf & "   [FTRawMatColorNameTH] [nvarchar](200) NULL, "
            cmdstring &= vbCrLf & "    [FTRawMatColorNameEN] [nvarchar](200) NULL,[FNRowSeq] [int] NULL, "

            cmdstring &= vbCrLf & "   UNIQUE  NONCLUSTERED(FNHSysStyleId, FTOrderNo, FNHSysMainMatId, FNHSysRawMatColorId, [FNRowSeq]) "

            cmdstring &= vbCrLf & "  	) "


            cmdstring &= vbCrLf & "    INSERT INTO  @TMERTOrder_MatColor([FNHSysStyleId] , [FTOrderNo], [FNHSysMainMatId], [FNHSysRawMatColorId], [FTRawMatColorNameTH], [FTRawMatColorNameEN],FNRowSeq )"
            cmdstring &= vbCrLf & "  	Select  FNHSysStyleId "
            cmdstring &= vbCrLf & "  	,FTOrderNo"
            cmdstring &= vbCrLf & "  	,FNHSysMerMatId"
            cmdstring &= vbCrLf & "  ,FNHSysRawMatColorId"
            cmdstring &= vbCrLf & "  	,MAX(FTRawMatColorNameTH) AS FTRawMatColorNameTH"
            cmdstring &= vbCrLf & "  ,MAX(FTRawMatColorNameEN) As FTRawMatColorNameEN"

            cmdstring &= vbCrLf & " ,ROW_NUMBER() Over (Order By [FNHSysStyleId] , [FTOrderNo]) AS [FNRowSeq] "

            cmdstring &= vbCrLf & " 	 From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTMPR_TMP AS A  With(NOLOCK) "
            cmdstring &= vbCrLf & " 	  WHERE FTUserLogIn  ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            cmdstring &= vbCrLf & "  	And FNHSysRawMatColorId <>0"

            If Filter <> "" Then

                cmdstring &= vbCrLf & "   " & Filter

            End If

            cmdstring &= vbCrLf & "   GROUP BY FNHSysStyleId"
            cmdstring &= vbCrLf & "  ,FTOrderNo"
            cmdstring &= vbCrLf & "  	,FNHSysMerMatId"
            cmdstring &= vbCrLf & "  	,FNHSysRawMatColorId "

            cmdstring &= vbCrLf & "    DELETE From BBD "
            cmdstring &= vbCrLf & "    From     @TMERTOrder_MatColor As MMD  INNER Join"
            cmdstring &= vbCrLf & "             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder_Mat_Color AS BBD "
            cmdstring &= vbCrLf & "    On MMD.FNHSysStyleId = BBD.FNHSysStyleId "
            cmdstring &= vbCrLf & "    And MMD.FTOrderNo = BBD.FTOrderNo "

            If Filter <> "" Then

                cmdstring &= vbCrLf & "    And MMD.FNHSysMainMatId = BBD.FNHSysMainMatId"

            End If

            cmdstring &= vbCrLf & " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LOG) & "].[dbo].TMERTMPR_History (  FTUserLogin, FTDateCreate, FNCaltype,FTStyleCode, FTSeasonCode, FTOrderNo, FTMainMatCode, FTRawMatColorCode, FTRawMatColorNameEN, FTRawMatColorNameTH"
            cmdstring &= vbCrLf & "  , FTRawMatSizeCode,  FNQuantity, FNHSysRawMatId, FNHSysStyleId, FNHSysSeasonId,FTFilterItem ,FTCalTypeName,FTCalDate,FTCalTime,FNRowSeq"
            cmdstring &= vbCrLf & "  , FTSubOrderNo,FNUsedQuantity,FNUsedPlusQuantity,FNPRQuantity,FDShipDate,FDOGacDate)"
            cmdstring &= vbCrLf & "   Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',Getdate(),2, ST.FTStyleCode "
            cmdstring &= vbCrLf & "  , SS.FTSeasonCode  "
            cmdstring &= vbCrLf & "  , A.FTOrderNo "
            cmdstring &= vbCrLf & "  , A.FTMainMatCode "
            cmdstring &= vbCrLf & "  , A.FTRawMatColorCode "
            cmdstring &= vbCrLf & "  , A.FTRawMatColorNameEN"
            cmdstring &= vbCrLf & "  , A.FTRawMatColorNameTH"
            cmdstring &= vbCrLf & " , A.FTRawMatSizeCode"
            cmdstring &= vbCrLf & " , SUM(A.FNPRQuantity) AS FNQuantity	"
            cmdstring &= vbCrLf & " , A.FNHSysRawMatId "
            cmdstring &= vbCrLf & " , A.FNHSysStyleId "
            cmdstring &= vbCrLf & " , A.FNHSysSeasonId	"
            cmdstring &= vbCrLf & " , '" & HI.UL.ULF.rpQuoted(Filter) & "'	"
            cmdstring &= vbCrLf & " , 'Delete'	"
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & "	"
            cmdstring &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & "	"

            cmdstring &= vbCrLf & " ,Row_Number() Over (Partition by A.FTOrderNo,A.FTMainMatCode,A.FTRawMatColorCode,A.FTRawMatSizeCode Order by A.FTRawMatColorCode ) AS FNRowSeq	"
            cmdstring &= vbCrLf & "  ,A.FTSubOrderNo,SUM(A.FNUsedQuantity) AS FNUsedQuantity,SUM(A.FNUsedPlusQuantity) AS FNUsedPlusQuantity,SUM(A.FNPRQuantity) AS FNPRQuantity "
            cmdstring &= vbCrLf & "  , MAX(ISNULL(Sub.FDShipDate,'') ) AS FDShipDate "
            cmdstring &= vbCrLf & "  , MAX(ISNULL(Sub.FDShipDateOrginal,'') ) AS FDOGacDate "

            cmdstring &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTMPR_TMP AS A WITH(NOLOCK) "
            cmdstring &= vbCrLf & "       Outer Apply(SELECT TOP 1 ST.FTStyleCode  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TMERMStyle AS ST WITH(NOLOCK) WHERE ST.FNHSysStyleId =A.FNHSysStyleId ) AS ST  "
            cmdstring &= vbCrLf & "       Outer Apply(SELECT TOP 1 SS.FTSeasonCode  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TMERMSeason  AS SS WITH(NOLOCK) WHERE SS.FNHSysSeasonId =A.FNHSysSeasonId ) AS SS "
            cmdstring &= vbCrLf & "       Outer Apply(SELECT TOP 1 Sub.FDShipDate,Sub.FDShipDateOrginal  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrderSub  AS Sub WITH(NOLOCK) WHERE Sub.FTSubOrderNo =A.FTSubOrderNo ) AS Sub "

            cmdstring &= vbCrLf & " 	  WHERE A.FTUserLogIn  ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AND A.FNHSysRawMatId <>0"

            If Filter <> "" Then

                cmdstring &= vbCrLf & "    " & Filter

            End If

            cmdstring &= vbCrLf & "   GROUP BY ST.FTStyleCode  "
            cmdstring &= vbCrLf & "	 ,SS.FTSeasonCode "
            cmdstring &= vbCrLf & " ,A.FTOrderNo,A.FTSubOrderNo "
            cmdstring &= vbCrLf & " ,A.FTRawMatColorCode, A.FTRawMatSizeCode, A.FTRawMatColorNameEN, A.FTRawMatColorNameTH, A.FTMainMatCode, A.FNHSysRawMatId, A.FNHSysStyleId, A.FNHSysSeasonId "

            cmdstring &= vbCrLf & " Declare @TabOrder AS Table (OrderNo nvarchar(30)  UNIQUE  NONCLUSTERED  (OrderNo) )"

            cmdstring &= vbCrLf & "    INSERT INTO @TabOrder(OrderNo) "
            cmdstring &= vbCrLf & "   Select Distinct FTOrderNo "
            cmdstring &= vbCrLf & " 	From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTMPR_TMP] AS A WITH(NOLOCK)"
            cmdstring &= vbCrLf & " 	WHERE A.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' 	And A.FNHSysRawMatColorId <>0 "

            cmdstring &= vbCrLf & " Declare @TabRC AS TABLE ("
            cmdstring &= vbCrLf & " [FTUserLogIn] [varchar](50) NOT NULL,"
            cmdstring &= vbCrLf & " [FTOrderNo] [nvarchar](30) NOT NULL,"
            cmdstring &= vbCrLf & " [FTSubOrderNo] [nvarchar](30) NOT NULL,"
            cmdstring &= vbCrLf & " [FNHSysRawMatId] [int] NOT NULL,"
            cmdstring &= vbCrLf & " [FNUsedQuantity] [numeric](18, 5) NULL,"
            cmdstring &= vbCrLf & " [FNUsedPlusQuantity] [numeric](18, 5) NULL,"
            cmdstring &= vbCrLf & " [FNUsedQuantityDIFF] [numeric](18, 5) NULL,"
            cmdstring &= vbCrLf & " [FNUsedQuantityPlusDIFF] [numeric](18, 5) NULL,"
            cmdstring &= vbCrLf & " [FNHSysUnitId] [int] NULL,"
            cmdstring &= vbCrLf & " [FNPrice] [numeric](18, 5) NULL,"
            cmdstring &= vbCrLf & " [FNHSysCurId] [int] NULL,"
            cmdstring &= vbCrLf & " [FNHSysSuplId] [int] NULL,"
            cmdstring &= vbCrLf & " [FTStateNominate] [varchar](1) NULL,"
            cmdstring &= vbCrLf & " [FNStateChange] [int]  NULL,"
            cmdstring &= vbCrLf & " [FTFabricFrontSize] [nvarchar](50) NULL,"
            cmdstring &= vbCrLf & " [FTStateFree] [varchar](1) NULL,"
            cmdstring &= vbCrLf & " [FNStateNew] [int] NULL,"
            cmdstring &= vbCrLf & " [FNRowSeq] [int] NULL,"
            cmdstring &= vbCrLf & " [FTStateHemNotOptiplan] [varchar](1) NULL,"
            cmdstring &= vbCrLf & " [FNHemNotOptiplan] [numeric](18, 5) NULL,"
            cmdstring &= vbCrLf & " [FNRepeatLengthCM] numeric(18,5) NULL,"
            cmdstring &= vbCrLf & " [FNRepeatConvert] numeric(18,10) NULL,"
            cmdstring &= vbCrLf & " [FNTotalRepeat] numeric(18,5) NULL,"
            cmdstring &= vbCrLf & " [FTMainMatCoide] [nvarchar](50) NULL,"
            cmdstring &= vbCrLf & "  UNIQUE  NONCLUSTERED  ([FTUserLogIn],[FTOrderNo],[FTSubOrderNo],[FNHSysRawMatId],[FNStateChange],[FNRowSeq],[FTMainMatCoide])"
            cmdstring &= vbCrLf & "  ) "

            cmdstring &= vbCrLf & "    INSERT INTO @TabRC(FTUserLogIn, FTOrderNo, FTSubOrderNo, FNHSysRawMatId, FNUsedQuantity, FNUsedPlusQuantity "
            cmdstring &= vbCrLf & "  , FNUsedQuantityDIFF, FNUsedQuantityPlusDIFF, FNHSysUnitId, FNPrice, FNHSysCurId"
            cmdstring &= vbCrLf & "  , FNHSysSuplId, FTStateNominate, FNStateChange,FTFabricFrontSize,FTStateFree,FNStateNew,[FNRowSeq],FTStateHemNotOptiplan,FNHemNotOptiplan,FNRepeatLengthCM,FNRepeatConvert,FNTotalRepeat)"

            cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',FTOrderNo, FTSubOrderNo, FNHSysRawMatId"
            cmdstring &= vbCrLf & " 	, MAX(FNUsedQuantity) AS FNUsedQuantity"
            cmdstring &= vbCrLf & " 	, MAX(FNUsedPlusQuantity) AS FNUsedPlusQuantity"
            cmdstring &= vbCrLf & " 	, MAX(FNUsedQuantityDIFF) AS FNUsedQuantityDIFF"
            cmdstring &= vbCrLf & " 	, MAX(FNUsedQuantityPlusDIFF) AS FNUsedQuantityPlusDIFF"
            cmdstring &= vbCrLf & " 	, FNHSysUnitId, FNPrice, FNHSysCurId, FNHSysSuplId, FTStateNominate"
            cmdstring &= vbCrLf & " 	, FNStateChange"
            cmdstring &= vbCrLf & " 	, FTFabricFrontSize"
            cmdstring &= vbCrLf & " ,Max(FTStateFree) AS FTStateFree"
            cmdstring &= vbCrLf & " 	,FNStateNew"
            cmdstring &= vbCrLf & " 	,ROW_NUMBER() Over (Order By FTOrderNo) AS [FNRowSeq]"
            cmdstring &= vbCrLf & " ,MAX(FTStateHemNotOptiplan) AS FTStateHemNotOptiplan"
            cmdstring &= vbCrLf & " 	,MAX(FNHemNotOptiplan) As FNHemNotOptiplan"
            cmdstring &= vbCrLf & " 	,MAX(FNRepeatLengthCM) AS FNRepeatLengthCM"
            cmdstring &= vbCrLf & " 	,MAX([FNRepeatConvert]) AS FNRepeatConvert"
            cmdstring &= vbCrLf & " 	,0 AS FNTotalRepeat"
            cmdstring &= vbCrLf & " 	FROM("
            cmdstring &= vbCrLf & "  SELECT A.FTOrderNo, '' AS FTSubOrderNo, A.FNHSysRawMatId"
            cmdstring &= vbCrLf & "    , CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedQuantity))) ELSE SUM(FNUsedQuantity)  END AS FNUsedQuantity"
            cmdstring &= vbCrLf & " 	,CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedPlusQuantity))) ELSE  SUM(FNUsedPlusQuantity) END AS FNUsedPlusQuantity"
            cmdstring &= vbCrLf & " 	, MAX(FNHSysUnitId) AS FNHSysUnitId"
            cmdstring &= vbCrLf & " 	, MAX(FNPrice) AS FNPrice"
            cmdstring &= vbCrLf & " 	, MAX(FNHSysCurId) AS FNHSysCurId"
            cmdstring &= vbCrLf & " 	, MAX(FNHSysSuplId) AS FNHSysSuplId"
            cmdstring &= vbCrLf & " 	, MAX(FTStateNominate) AS FTStateNominate"
            cmdstring &= vbCrLf & " 	, (SUM(FNUsedQuantity)) - (MAX(ISNULL(RES.FNUsedQuantity2, 0))) AS FNUsedQuantityDIFF"
            cmdstring &= vbCrLf & " 	, ( CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedPlusQuantity))) ELSE SUM(FNUsedPlusQuantity) END) - (MAX(ISNULL(RES.FNUsedPlusQuantity2, 0))) AS FNUsedQuantityPlusDIFF"
            cmdstring &= vbCrLf & " 	, ( CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedQuantity))) ELSE SUM(FNUsedQuantity)  END  + CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN CEILING(SUM(FNUsedPlusQuantity)) ELSE SUM(FNUsedPlusQuantity) END) - (MAX(ISNULL(RES.FNUsedQuantity2, 0)) + MAX(ISNULL(RES.FNUsedPlusQuantity2, 0))) AS DIFFQTY"
            cmdstring &= vbCrLf & " 	, CASE WHEN ( CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedQuantity))) ELSE SUM(FNUsedQuantity)  END  + CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedPlusQuantity))) ELSE SUM(FNUsedPlusQuantity) END) > "
            cmdstring &= vbCrLf & " 	(MAX(ISNULL(RES.FNUsedQuantity2, 0)) + MAX(ISNULL(RES.FNUsedPlusQuantity2, 0)))"
            cmdstring &= vbCrLf & " 	THEN CASE WHEN (MAX(ISNULL(RES.FNUsedQuantity2, 0)) + MAX(ISNULL(RES.FNUsedPlusQuantity2, 0))) = 0 "
            cmdstring &= vbCrLf & " 	THEN 0 ELSE MAX(RES.FNStateChange) + 1 END"
            cmdstring &= vbCrLf & " 	Else MAX(ISNULL(Res.FNStateChange, 0))"
            cmdstring &= vbCrLf & "    End As FNStateChange	"
            cmdstring &= vbCrLf & "   ,MAX(FTFabricFrontSize) AS FTFabricFrontSize	"
            cmdstring &= vbCrLf & "   ,MAX(ISNULL(FTStateFree,'0')) AS FTStateFree"

            cmdstring &= vbCrLf & "   , CASE WHEN (SUM(FNUsedQuantity) + CASE WHEN ISNULL(MAX(A.FTStateInteger),'') ='1' THEN Convert(numeric(18,4),CEILING(SUM(FNUsedPlusQuantity))) ELSE SUM(FNUsedPlusQuantity) END) > "
            cmdstring &= vbCrLf & " 		(MAX(ISNULL(RES.FNUsedQuantity2, 0)) + MAX(ISNULL(RES.FNUsedPlusQuantity2, 0))) And (MAX(ISNULL(RES.FNUsedQuantity2, 0)) + MAX(ISNULL(RES.FNUsedPlusQuantity2, 0))) > 0"
            cmdstring &= vbCrLf & " 	THEN 1"
            cmdstring &= vbCrLf & " 	Else 0"
            cmdstring &= vbCrLf & "   End As FNStateNew	"
            cmdstring &= vbCrLf & "   ,MAX(FTStateHemNotOptiplan) AS FTStateHemNotOptiplan"
            cmdstring &= vbCrLf & "   ,SUM(ISNULL(A.FNHemNotOptiplan,0)) AS FNHemNotOptiplan"
            cmdstring &= vbCrLf & "   ,MAX(ISNULL(FNRepeatLengthCM,0)) AS FNRepeatLengthCM"
            cmdstring &= vbCrLf & "   ,MAX(ISNULL([FNRepeatConvert],0)) AS FNRepeatConvert"
            cmdstring &= vbCrLf & "   ,MAX(ISNULL([FTMainMatCode],'')) AS FTMainMatCode"
            cmdstring &= vbCrLf & " 	From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTMPR_TMP] AS A WITH(NOLOCK)"

            cmdstring &= vbCrLf & " 	OUTER APPLY(SELECT FTOrderNo, FNHSysRawMatId, MAX(FNStateChange) AS FNStateChange"
            cmdstring &= vbCrLf & " 	, SUM(FNUsedQuantity) AS FNUsedQuantity2, SUM(FNUsedPlusQuantity) AS FNUsedPlusQuantity2"
            cmdstring &= vbCrLf & " 	From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTOrder_Sourcing]  AS X  WITH(NOLOCK)"
            cmdstring &= vbCrLf & " 	Where X.FTOrderNo =A.FTOrderNo "
            cmdstring &= vbCrLf & " 	AND X.FNHSysRawMatId =A.FNHSysRawMatId "
            cmdstring &= vbCrLf & " 	GROUP BY FTOrderNo, FNHSysRawMatId) AS RES"

            cmdstring &= vbCrLf & " 	WHERE A.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            cmdstring &= vbCrLf & "    And A.FNHSysRawMatId > 0    "

            If Filter <> "" Then

                cmdstring &= vbCrLf & "   " & Filter

            End If

            cmdstring &= vbCrLf & " 	GROUP BY A.FTOrderNo,  A.FNHSysRawMatId"
            cmdstring &= vbCrLf & "  ) AS A "
            cmdstring &= vbCrLf & "  GROUP BY FTOrderNo, FTSubOrderNo, FNHSysRawMatId, FNHSysUnitId, FNPrice"
            cmdstring &= vbCrLf & " 	, FNHSysCurId, FNHSysSuplId, FTStateNominate, DIFFQTY, FNStateChange,FTFabricFrontSize,FNStateNew "
            cmdstring &= vbCrLf & "   delete from @TabRC where FNStateNew <> 0 And [FNUsedQuantityDIFF] =0 And FNUsedQuantityPlusDIFF =0 AND ISNULL(FNStateChange,0) >0 "
            cmdstring &= vbCrLf & " update @TabRC set FNStateChange = isnull(FNStateChange,0)"

            If Filter <> "" Then

                cmdstring &= vbCrLf & "    DELETE A "
                cmdstring &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrder_Resource] AS A INNER Join @TabOrder As B On A.FTOrderNo = B.OrderNo "
                cmdstring &= vbCrLf & "    CROSS APPLY (SELECT TOP 1 MMM.FTRawMatCode [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.[TINVENMMaterial] AS MMM WITH(NOLOCK) WHERE MMM.FNHSysRawMatId =A.FNHSysRawMatId ) AS MMM "
                cmdstring &= vbCrLf & " 	Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTOrder_Sourcing] AS C WITH(NOLOCK)"
                cmdstring &= vbCrLf & " 	On A.FTOrderNo = C.FTOrderNo"
                cmdstring &= vbCrLf & " 	And A.FNHSysRawMatId=C.FNHSysRawMatId"
                cmdstring &= vbCrLf & "  And A.FNStateChange = C.FNStateChange"
                cmdstring &= vbCrLf & "           INNER Join @TabRC AS RC ON A.FTOrderNo = RC.FTOrderNo  And MMM.FTRawMatCode=RC.FTMainMatCoide"
                cmdstring &= vbCrLf & "   WHERE  RC.FTUserLogIn ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' And C.FTOrderNo Is NULL"

            Else

                cmdstring &= vbCrLf & "   DELETE A"
                cmdstring &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.[TMERTOrder_Resource] AS A INNER Join @TabOrder As B On A.FTOrderNo = B.OrderNo "
                cmdstring &= vbCrLf & " 	Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTOrder_Sourcing] AS C WITH(NOLOCK)"
                cmdstring &= vbCrLf & " 	On A.FTOrderNo = C.FTOrderNo"
                cmdstring &= vbCrLf & " 	And A.FNHSysRawMatId=C.FNHSysRawMatId"
                cmdstring &= vbCrLf & " 	WHERE   C.FTOrderNo Is NULL "


            End If


            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

            'FilterStringAll = FilterString & Filter

            'Try
            '    If _FNHSysBuyId = "" Then _FNHSysBuyId = "0"
            '    If _FNHSysStyleId = "" Then _FNHSysStyleId = "0"

            '    HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            '    HI.Conn.SQLConn.SqlConnectionOpen()
            '    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

            '    Dim sqlCmd As New SqlCommand
            '    sqlCmd.Connection = HI.Conn.SQLConn.Cnn
            '    sqlCmd.CommandType = CommandType.StoredProcedure
            '    sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_DELETE_MPR]"
            '    sqlCmd.Parameters.AddWithValue("@FNHSysBuyId", _FNHSysBuyId)
            '    sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", _FNHSysStyleId)
            '    sqlCmd.Parameters.AddWithValue("@FTOrderNo", _OrderNo)
            '    sqlCmd.Parameters.AddWithValue("@DeleteAll", Int(_DeleteAll))
            '    sqlCmd.Parameters.AddWithValue("@FNHSysRawMatId", _ItemNo)
            '    sqlCmd.Parameters.AddWithValue("@FNSeq", _FNSeq)
            '    sqlCmd.Parameters.AddWithValue("@FNMerMatSeq", _FNMerMatSeq)
            '    sqlCmd.Parameters.AddWithValue("@FNHSysMatColorId", _FNHSysMatColorId)
            '    sqlCmd.Parameters.AddWithValue("@FNMatColorSeq", _FNMatColorSeq)
            '    sqlCmd.Parameters.AddWithValue("@FNHSysMatSizeId", _FNHSysMatSizeId)
            '    sqlCmd.Parameters.AddWithValue("@FNMatSizeSeq", _FNMatSizeSeq)
            '    sqlCmd.Parameters.AddWithValue("@FilterString", FilterStringAll)
            '    Dim sqlDA As New SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
            '    sqlDA.DeleteCommand = sqlCmd
            '    sqlDA.DeleteCommand.ExecuteNonQuery()
            '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '    HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete Calculate MPR (Style " & FNHSysStyleId.Text & " FO " & _OrderNo & "  Item " & FilterStringAll & ") ")
            'Catch ex As Exception
            '    HI.Conn.SQLConn.Tran.Rollback()
            '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '    ret = False
            'End Try

            Return ret
        Catch ex As Exception
            Return False
        End Try


    End Function

#End Region

#Region "Create Adapter"
    Public Function CreateAdapter(
    ByVal connection As SqlConnection) As SqlDataAdapter

        Dim adapter As SqlDataAdapter = New SqlDataAdapter()

        ' Create the SelectCommand. 
        Dim command As SqlCommand = New SqlCommand(
            "SELECT FNHSysStyleId, FNSeq, FNMerMatSeq, FNHSysMerMatId, FNPart FROM " &
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle_Mat] " &
            "WHERE FNHSysStyleId = @FNHSysStyleId AND FNSeq = @FNSeq", connection)

        ' Add the parameters for the SelectCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int)
        command.Parameters.Add("@FNSeq", SqlDbType.Int)

        adapter.SelectCommand = command

        ' Create the InsertCommand.
        command = New SqlCommand(
            "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle_Mat] " &
            "([FNHSysStyleId], [FNSeq], [FNMerMatSeq], [FNHSysMerMatId], [FNPart], " &
            "[FTPositionPartName],[FNHSysSuplId],[FTStateNominate],[FNHSysUnitId],[FNPrice],[FNHSysCurId], " &
            "[FNConSmp],[FNConSmpPlus],[FTOrderNo],[FTSubOrderNo],[FTStateActive],[FTStateCombination], FTStateMainMaterial, " &
            "[FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser],[FDUpdDate],[FTUpdTime]) " &
            "VALUES (@FNHSysStyleId, @FNSeq, @FNMerMatSeq, @FNHSysMerMatId, @FNPart, " &
            "@FTPositionPartName, @FNHSysSuplId, @FTStateNominate, @FNHSysUnitId, @FNPrice, @FNHSysCurId, " &
            "@FNConSmp, @FNConSmpPlus, @FTOrderNo, @FTSubOrderNo, @FTStateActive, @FTStateCombination, @FTStateMainMaterial, " &
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
        command = New SqlCommand(
            "UPDATE TMERTStyle_Mat SET " &
            "FNHSysStyleId = @FNHSysStyleId, " &
            "FNSeq = @FNSeq, " &
            "FNMerMatSeq = @FNMerMatSeq, " &
            "FNHSysMerMatId = @FNHSysMerMatId, " &
            "FNPart = @FNPart, " &
            "FTPositionPartName = @FTPositionPartName, " &
            "FNHSysSuplId = @FNHSysSuplId, " &
            "FTStateNominate = @FTStateNominate, " &
            "FNHSysUnitId = @FNHSysUnitId, " &
            "FNPrice = @FNPrice, " &
            "FNHSysCurId =@FNHSysCurId, " &
            "FNConSmp = @FNConSmp, " &
            "FNConSmpPlus = @FNConSmpPlus, " &
            "FTOrderNo = @FTOrderNo, " &
            "FTSubOrderNo = @FTSubOrderNo, " &
            "FTStateActive = @FTStateActive, " &
            "FTStateCombination = @FTStateCombination, " &
            "FTStateMainMaterial = @FTStateMainMaterial, " &
            "FTUpdUser = @FTUpdUser, " &
            "FDUpdDate = @FDUpdDate, " &
            "FTUpdTime = @FTUpdTime " &
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
        command = New SqlCommand(
            "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle_Mat] " &
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
        Dim command As SqlCommand = New SqlCommand(
            "SELECT     O.FNHSysStyleId, B.FTOrderNo, C.FNMatColorSeq, C.FTMatColorNameEN AS FTColorway, B.FTSizeBreakDown, B.FNHSysMatColorId, " &
            "B.FNPrice, B.FNQuantity, B.FNAmt, B.FNExtraQuantity FROM " &
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder AS O INNER JOIN" &
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder_BreakDown AS B ON O.FTOrderNo = B.FTOrderNo INNER JOIN  " &
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TMERMMatColor] AS C ON B.FNHSysMatColorId = C.FNHSysMatColorId " &
            "WHERE (O.FNHSysStyleId = @FNHSysStyleId)" &
            "ORDER BY O.FNHSysStyleId, B.FTOrderNo, C.FNMatColorSeq, B.FTSizeBreakDown", connection)

        ' Add the parameters for the SelectCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int)

        adapter.SelectCommand = command

        Return adapter
    End Function

    Public Function CreateAdapterImportSize(ByVal connection As SqlConnection) As SqlDataAdapter
        Dim adapter As SqlDataAdapter = New SqlDataAdapter()

        ' Create the SelectCommand. 
        Dim command As SqlCommand = New SqlCommand(
            "SELECT     O.FNHSysStyleId, B.FTOrderNo, C.FNMatSizeSeq, C.FTMatSizeNameEN AS FTSizeBreakDown, B.FNHSysMatSizeId, " &
            "B.FNPrice, B.FNQuantity, B.FNAmt, B.FNExtraQuantity FROM " &
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder AS O INNER JOIN" &
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder_BreakDown AS B ON O.FTOrderNo = B.FTOrderNo INNER JOIN  " &
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TMERMMatSize] AS C ON B.FNHSysMatSizeId = C.FNHSysMatSizeId " &
            "WHERE (O.FNHSysStyleId = @FNHSysStyleId)" &
            "ORDER BY O.FNHSysStyleId, B.FTOrderNo, C.FNMatSizeSeq, B.FTSizeBreakDown", connection)

        ' Add the parameters for the SelectCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int)

        adapter.SelectCommand = command

        Return adapter
    End Function

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
        ocmautosc.Visible = (otb.SelectedTabPage.Name = CalcMPR.Name)

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
                    Case CalcMPRNotIn.Name
                        Call LoadMPRInfoNotIn(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, GetOrderArr())
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

    Private Sub ocmautosc_Click(sender As Object, e As EventArgs) Handles ocmautosc.Click
        If GridCalculated.DataSource Is Nothing Then
            Exit Sub
        End If

        Try
            If GridView3.RowCount <= 0 Then Exit Sub

            Dim StateAcc As Boolean = False

            Dim cmdstring As String = ""
            cmdstring = "delete from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempAutoSourcing where UserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

            With GridView3
                For i As Integer = 0 To .RowCount - 1

                    If .GetRowCellValue(i, .Columns.ColumnByFieldName("FTStateAcc")).ToString = "1" Then
                        StateAcc = True

                        cmdstring = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempAutoSourcing "
                        cmdstring &= vbCrLf & " SET FTSubOrderNo ='' "
                        cmdstring &= vbCrLf & " WHERE  UserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        cmdstring &= vbCrLf & "        AND FNHSysRawMatId=" & Val(.GetRowCellValue(i, .Columns.ColumnByFieldName("FNHSysRawMatId")).ToString) & ""
                        cmdstring &= vbCrLf & "        AND FTOrderNo='" & HI.UL.ULF.rpQuoted(.GetRowCellValue(i, .Columns.ColumnByFieldName("FTOrderNo")).ToString) & "'"

                        If HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN) = False Then

                            cmdstring = " insert into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempAutoSourcing(UserLogin, FNHSysRawMatId, FTOrderNo, FTSubOrderNo,FTCusItemCodeRef) "
                            cmdstring &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & "," & Val(.GetRowCellValue(i, .Columns.ColumnByFieldName("FNHSysRawMatId")).ToString) & ""
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.GetRowCellValue(i, .Columns.ColumnByFieldName("FTOrderNo")).ToString) & "'"
                            cmdstring &= vbCrLf & ",''"
                            cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(.GetRowCellValue(i, .Columns.ColumnByFieldName("FTCusItemCodeRef")).ToString) & "'"

                            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

                        End If

                    End If

                Next
            End With


            If StateAcc = True Then
                Dim dtData As New DataSet
                Dim dtData1 As New DataTable
                Dim dtData2 As New DataTable

                cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_AUTOGEN_SOURCING '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                HI.Conn.SQLConn.GetDataSet(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN, dtData)


                Try
                    dtData1 = dtData.Tables(0)
                    dtData2 = dtData.Tables(1)

                    Dim CompleteData As Integer = Val(dtData1.Rows(0)!FNComplete.ToString())


                    If CompleteData > 0 Then
                        HI.MG.ShowMsg.mInfo("Auto Sourcing Data Complete ...", 190055471, Me.Text, " จำนวน " & CompleteData.ToString() & " รายการ", MessageBoxIcon.Warning)
                    End If

                    If dtData2.Select("FTStateMapSupl<>'1'").Length > 0 Then

                    End If

                Catch ex As Exception

                End Try
            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลรายการ Acc กรุณาทำการตรวจสอบ !!!", 121345748, Me.Text,, MessageBoxIcon.Warning)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvmatcode_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles ogvmatcode.CustomColumnDisplayText

        Try
            If e.Column.FieldName = "FNRepeatLengthCM" Then

                If Val(e.Value) = 0 Then
                    e.DisplayText = ""
                End If

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView3_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles GridView3.CustomColumnDisplayText

        Try
            If e.Column.FieldName = "FNRepeatLengthCM" Then

                If Val(e.Value) = 0 Then
                    e.DisplayText = ""
                End If

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub RepositoryItemCheckFTStateAcc_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCheckFTStateAcc.EditValueChanging
        Try
            With Me.GridView3
                If .GetFocusedRowCellValue("FNStateSourcing").ToString = "0" Then
                    e.Cancel = False
                Else
                    e.Cancel = True
                End If

            End With
        Catch ex As Exception
            e.Cancel = True
        End Try
    End Sub

    Private Sub FNSelectOrderType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNSelectOrderType.SelectedIndexChanged
        Try
            If FNHSysBuyId.Text <> "" Or FNHSysStyleId.Text <> "" Then
                Call LoadOrderInfo(Val(FNHSysBuyId.Properties.Tag.ToString), Val(FNHSysStyleId.Properties.Tag.ToString))
                ogcmatcode.DataSource = Nothing
            End If

        Catch ex As Exception

        End Try
    End Sub

End Class