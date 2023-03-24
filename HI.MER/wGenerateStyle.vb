Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ButtonEdit
Imports System.Data
Imports System.Data.Common
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Windows.Forms
Imports HI.Auditor

Public Class wGenerateStyle


    Private _CopyStyle As wCopyStyle
    Dim View As GridView
    Dim RowsIndex As Double
    Dim TopVisibleIndex As Int32
    Private sFNHSysStyleId As String
    Private ViewColor As GridView
    Private ViewSize As GridView

    ''' <summary>
    ''' Used Data Adapter to control database
    ''' </summary>
    ''' <remarks></remarks>

    Dim oleDbDataAdapter1 As DbDataAdapter
    Dim oleDbDataAdapter2 As DbDataAdapter
    Dim dtStyleDetail As DataTable

    Dim dtBefore As DataTable
    Dim dtAfter As DataTable

    Private Enum TabIndexs As Integer
        StyleDetail = 0
        Colorway = 1
        SizeBreakdown = 2
    End Enum

#Region "Property"

    Private _CallMenuName As String = ""
    
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

#End Region

#Region "Handler Control"

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()        
        ' Add any initialization after the InitializeComponent() call.

        _CopyStyle = New wCopyStyle
        HI.TL.HandlerControl.AddHandlerObj(_CopyStyle)
        Dim oSysLang As New HI.ST.SysLanguage
        Call HI.ST.Lang.InsertLanguage(_CopyStyle)

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _CopyStyle.Name.ToString.Trim, _CopyStyle)
        Catch ex As Exception
        Finally
        End Try

        Call HI.ST.Lang.SP_SETxLanguage(_CopyStyle)


        With RepositoryFTMainMatCode
            AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
            AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
            AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave

        End With

        With RepositoryFTUnitCode
            AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
            AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
            AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
        End With

        With RepositoryFNHSysSuplId
            AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
            AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
            AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
        End With

        With RepositoryFNHSysCurId
            AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
            AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
            AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
        End With

        With RepositoryFTOrderNo
            AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
            AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
            AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
        End With

        With RepositoryFTSubOrderNo
            AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
            AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
            AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave
        End With

    End Sub
#End Region

#Region "Form Load"

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(value As Boolean)
            _ProcComplete = value
        End Set
    End Property

    Private Sub wGenerateStyle_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
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
            View.OptionsView.ShowAutoFilterRow = True
            View.BestFitColumns()

            FTUpdUser.Text = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)
            FDUpdDate.Text = "??/??/????"
            FTUpdTime.Text = "??:??:??"

            sFNHSysStyleId = ""

            ViewColor = Me.ogcstylecolor.Views(0)
            ViewSize = Me.ogcstylesize.Views(0)

        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "MAIN PROC"

    Private Sub Proc_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysStyleId.EditValueChanged
        If FNHSysStyleId.Text <> "" Then
            Dim _Str As String = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
            FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
            If FNHSysStyleId.Properties.Tag.ToString <> "" Then
                Call LoadStyleInfo(FNHSysStyleId.Properties.Tag.ToString)
                Call LoadStyleDetail(FNHSysStyleId.Properties.Tag.ToString)
                Call LoadOrderInfo(FNHSysStyleId.Properties.Tag.ToString)
            Else
                FTStyleNameTH.Text = Nothing
                FTStyleNameEN.Text = Nothing
                FNHSysCustId.Text = Nothing
                FNHSysCustId_None.Text = Nothing
                FNHSysSeasonId.Text = Nothing
                FNHSysSeasonId_None.Text = Nothing
                FTUpdUser.Text = Nothing
                FDUpdDate.Text = Nothing
                FTUpdTime.Text = Nothing

                ogcmatcode.DataSource = Nothing
                ogcmatcode.Refresh()
                ogcstylecolor.DataSource = Nothing
                ogcstylecolor.Refresh()
                ogcstylesize.DataSource = Nothing
                ogcstylesize.Refresh()
            End If

            sFNHSysStyleId = FNHSysStyleId.Text
        Else
            ogcmatcode.DataSource = Nothing
            ogcmatcode.Refresh()
            ogcstylecolor.DataSource = Nothing
            ogcstylecolor.Refresh()
            ogcstylesize.DataSource = Nothing
            ogcstylesize.Refresh()
        End If
    End Sub

    Private Sub FNHSysSeasonId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysSeasonId.EditValueChanged
        If FNHSysSeasonId.Text <> "" And FNHSysStyleId.Text <> "" Then
            Dim _Str As String = "SELECT TOP 1 FNHSysSeasonId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason WITH(NOLOCK) WHERE FTSeasonCode ='" & HI.UL.ULF.rpQuoted(FNHSysSeasonId.Text) & "' "
            FNHSysSeasonId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")

            If Me.SaveData() = True Then
                Call PostSave("[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle]", Me.FNHSysStyleId.Text)
                InitNewRow(CType(ogcmatcode.DataSource, DataTable), TabIndexs.StyleDetail)
            End If
        End If
    End Sub

    Private Sub LoadStyleInfo(ByVal FNHSysStyleId As String)

        Dim _dt As DataTable
        Dim _Str As String = ""
        _Str = "SELECT MS.FNHSysStyleId, MS.FTStyleCode, MS.FTStyleNameTH, MS.FTStyleNameEN, T1.FNHSysCustId, T1.FNHSysSeasonId, T1.FTUpdUser, CONVERT(VARCHAR(10), CONVERT(DATETIME, T1.FDUpdDate, 120), 103) AS FDUpdDate, T1.FTUpdTime"
        _Str &= vbCrLf & "  , T7.FTSeasonCode, T7.FTSeasonNameEN, T8.FTCustCode, T8.FTCustNameEN"
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS MS WITH(NOLOCK)  "
        _Str &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle AS T1 WITH(NOLOCK) ON MS.FNHSysStyleId = T1.FNHSysStyleId"
        _Str &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason T7 WITH(NOLOCK) ON T1.FNHSysSeasonId = T7.FNHSysSeasonId"
        _Str &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer T8 WITH(NOLOCK) ON T1.FNHSysCustId = T8.FNHSysCustId"
        _Str &= vbCrLf & " WHERE(MS.FNHSysStyleId  =" & Val(FNHSysStyleId) & ")"
        _Str &= vbCrLf & " ORDER BY MS.FNHSysStyleId"
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

        If _dt.Rows.Count > 0 Then
            For Each R As DataRow In _dt.Rows
                'FNHSysStyleId_None.Text = R!FTStyleNameTH.ToString
                FTStyleNameTH.Text = R!FTStyleNameTH.ToString
                FTStyleNameEN.Text = R!FTStyleNameEN.ToString
                FNHSysCustId.Text = R!FTCustCode.ToString
                FNHSysCustId_None.Text = R!FTCustNameEN.ToString
                FNHSysSeasonId.Text = R!FTSeasonCode.ToString
                FNHSysSeasonId_None.Text = R!FTSeasonNameEN.ToString

                FTUpdUser.Text = R!FTUpdUser.ToString
                FDUpdDate.Text = R!FDUpdDate.ToString
                FTUpdTime.Text = R!FTUpdTime.ToString
            Next
        Else
            'FNHSysStyleId_None.Text = ""
            FTStyleNameTH.Text = ""
            FTStyleNameEN.Text = ""
            FNHSysCustId.Text = ""
            FNHSysCustId_None.Text = ""
            FNHSysSeasonId.Text = ""
            FNHSysSeasonId_None.Text = ""

            FTUpdUser.Text = ""
            FDUpdDate.Text = ""
            FTUpdTime.Text = ""
        End If

        'Me.otb.SelectedTabPageIndex = 0
        If (FNHSysStyleId <> "") Then
            Call LoadStyleDetail(FNHSysStyleId)
            Call LoadColorwaySize(FNHSysStyleId)
        End If

    End Sub

    Private Sub LoadStyleDetail(ByVal FNHSysStyleId As String)
        'If FNHSysStyleId = "" Then Return
        Dim _Str As String = ""

        If HI.ST.Lang.Language = ST.Lang.Lang.TH Then
            _Str = "SELECT T2.FNHSysStyleId, T2.FNSeq, T2.FNMerMatSeq, T2.FNHSysMerMatId, T3.FTMainMatCode, T3.FTMainMatNameTH AS FNHSysMerMatId_None, T2.FNHSysUnitId, T4.FTUnitCode , T2.FTStateNominate, T2.FNHSysSuplId, T5.FTSuplCode, T2.FNHSysCurId, "
            _Str &= vbCrLf & " T6.FTCurCode, T2.FNPrice, T2.FNPart, T2.FTPositionPartName, T2.FTOrderNo AS FTOrderID, T2.FTSubOrderNo AS FTSubOrderID, "
            _Str &= vbCrLf & "CASE WHEN T2.FTOrderNo = '-1' THEN 'ทั้งหมด' ELSE T2.FTOrderNo END AS FTOrderNo, "
            _Str &= vbCrLf & "CASE WHEN T2.FTSubOrderNo = '-1' THEN 'ทั้งหมด' ELSE T2.FTSubOrderNo END AS FTSubOrderNo, "
        Else
            _Str = "SELECT T2.FNHSysStyleId, T2.FNSeq, T2.FNMerMatSeq, T2.FNHSysMerMatId, T3.FTMainMatCode, T3.FTMainMatNameEN AS FNHSysMerMatId_None, T2.FNHSysUnitId, T4.FTUnitCode , T2.FTStateNominate, T2.FNHSysSuplId, T5.FTSuplCode, T2.FNHSysCurId, "
            _Str &= vbCrLf & " T6.FTCurCode, T2.FNPrice, T2.FNPart, T2.FTPositionPartName, T2.FTOrderNo AS FTOrderID, T2.FTSubOrderNo AS FTSubOrderID, "
            _Str &= vbCrLf & "CASE WHEN T2.FTOrderNo = '-1' THEN 'ALL' ELSE T2.FTOrderNo END AS FTOrderNo, "
            _Str &= vbCrLf & "CASE WHEN T2.FTSubOrderNo = '-1' THEN 'ALL' ELSE T2.FTSubOrderNo END AS FTSubOrderNo, "
        End If

        _Str &= vbCrLf & "T2.FNConSmp, T2.FNConSmpPlus, "
        _Str &= vbCrLf & "CASE WHEN T2.FTStateCombination IS NOT NULL THEN T2.FTStateCombination ELSE "
        _Str &= " CASE WHEN MG.FTMatGrpCode = 'F' THEN '1' ELSE '0' END END AS FTStateCombination, "
        _Str &= vbCrLf & " T2.FTStateActive, T2.FTStateMainMaterial"
        _Str &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle] AS T1 WITH(NOLOCK) INNER JOIN"
        _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle_Mat] AS T2 WITH(NOLOCK) ON T1.FNHSysStyleId = T2.FNHSysStyleId LEFT OUTER JOIN"
        _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS T3 WITH(NOLOCK) ON T2.FNHSysMerMatId = T3.FNHSysMainMatId LEFT OUTER JOIN"
        _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS T4 WITH(NOLOCK) ON T2.FNHSysUnitId = T4.FNHSysUnitId LEFT OUTER JOIN"
        _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS T5 WITH(NOLOCK) ON T2.FNHSysSuplId = T5.FNHSysSuplId LEFT OUTER JOIN"
        _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS T6 WITH(NOLOCK) ON T2.FNHSysCurId = T6.FNHSysCurId LEFT OUTER JOIN"
        _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMMatGrp MG WITH (NOLOCK) ON T3.FNHSysMatGrpId = MG.FNHSysMatGrpId"
        _Str &= vbCrLf & " WHERE (T1.FNHSysStyleId =" & Val(FNHSysStyleId) & ")"
        _Str &= vbCrLf & " ORDER BY T1.FNHSysStyleId, T2.FNMerMatSeq, T2.FNPart"

        'If HI.ST.Lang.Language = ST.Lang.Lang.EN Then
        '    _Str &= vbCrLf & "  ,"
        'Else
        '    _Str &= vbCrLf & "  ,"
        'End If

        oleDbDataAdapter2 = New SqlClient.SqlDataAdapter(_Str, HI.Conn.SQLConn._ConnString)
        dtStyleDetail = New DataTable()
        oleDbDataAdapter2.Fill(dtStyleDetail)

        Me.ogcmatcode.DataSource = dtStyleDetail 'HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)
        Me.ogcmatcode.Refresh()


        Dim View As GridView = Me.ogcmatcode.Views(0)
        InitialGridMergCell()
        View.BestFitColumns()

    End Sub

    Private Sub LoadOrderInfo(ByVal _FNHSysStyleId As String)
        Dim _Str As String = ""
        If _FNHSysStyleId = "" Then Return

        _Str = "SELECT '0' AS FNSelect, A.FTOrderNo, CASE WHEN ISDATE(A.FDOrderDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, A.FDOrderDate), 103) "
        _Str &= vbCrLf & "         ELSE '' END AS FDOrderDate, ISNULL"
        _Str &= vbCrLf & "                  ((SELECT     CASE WHEN ISDATE(L1.FDShipDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, L1.FDShipDate), 103) ELSE '' END AS FDShipDate"
        _Str &= vbCrLf & "                      FROM         (SELECT     X.FTOrderNo, MIN(Y.FDShipDate) AS FDShipDate"
        _Str &= vbCrLf & "                                             FROM          HITECH_MERCHAN.dbo.TMERTOrder AS X INNER JOIN"
        _Str &= vbCrLf & "                                                                    HITECH_MERCHAN.dbo.TMERTOrderSub AS Y ON X.FTOrderNo = Y.FTOrderNo"
        _Str &= vbCrLf & "                                             GROUP BY X.FTOrderNo) AS L1"
        _Str &= vbCrLf & "                      WHERE     (FTOrderNo = A.FTOrderNo)), '') AS FDShipDate, A.FNHSysCustId, C.FTCustCode, C.FTCustNameEN AS FTCustName"
        _Str &= vbCrLf & "FROM         HITECH_MERCHAN.dbo.TMERTOrder AS A LEFT OUTER JOIN"
        _Str &= vbCrLf & "            HITECH_MASTER.dbo.TCNMCustomer AS C ON A.FNHSysCustId = C.FNHSysCustId"
        _Str &= vbCrLf & "WHERE     (A.FNHSysStyleId = " & _FNHSysStyleId & ")"

        oleDbDataAdapter2 = New SqlClient.SqlDataAdapter(_Str, HI.Conn.SQLConn._ConnString)
        dtStyleDetail = New DataTable()
        oleDbDataAdapter2.Fill(dtStyleDetail)

        Me.GridOrderList.DataSource = dtStyleDetail

        Dim view As GridView
        view = GridOrderList.Views(0)
        view.OptionsView.ShowAutoFilterRow = False

        Me.GridOrderList = view.GridControl
        Me.GridOrderList.Refresh()

    End Sub

    Private Sub LoadColorwaySize(ByVal FNHSysStyleId As String)
        Try

            Dim StrSql As String = ""

            Call ImportColor()

            Call ImportSize()

        Catch ex As Exception

        End Try

    End Sub

    Private Function InitialGrid(ByVal InitGrid As GridView) As GridView
        Dim xCol As Integer = 0
        Dim Idx As Integer = 0
        Try
            xCol = InitGrid.Columns.Count - 1
            For Idx = xCol To 0 Step -1
                If Idx >= 10 Then
                    Dim Col As GridColumn = InitGrid.Columns(Idx)
                    View.Columns(Col.Name).Dispose()
                End If
            Next

        Catch ex As Exception
        End Try

        Return InitGrid
    End Function

    Private Sub ocmcopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmcopy.Click

        If Me.FNHSysStyleId.Text <> "" Then
            If "" & Me.FNHSysStyleId.Properties.Tag.ToString <> "" Then
                Call HI.ST.Lang.SP_SETxLanguage(_CopyStyle)

                With _CopyStyle
                    .FNHSysStyleId.Text = Me.FNHSysStyleId.Text
                    .ProcComplete = False
                    .ShowDialog()

                    If (.ProcComplete) Then

                    End If
                End With

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysStyleId_lbl.Text)
                FNHSysStyleId.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysStyleId_lbl.Text)
            FNHSysStyleId.Focus()
        End If
    End Sub

    Private Sub ocmbomaddnew_Click(sender As System.Object, e As System.EventArgs) Handles ocmbomaddnew.Click
        If sFNHSysStyleId = "" Then Return
        InitNewRow(CType(ogcmatcode.DataSource, DataTable), TabIndexs.StyleDetail)
    End Sub

    Private Sub ocmbominsertrow_Click(sender As System.Object, e As System.EventArgs) Handles ocmbominsertrow.Click
        Dim crRow As Double, nxRow As String, nwRow As String
        Dim RowCount As Integer = 0
        View = Me.ogcmatcode.Views(0)
        If sFNHSysStyleId = "" Then Return

        If (Not IsDBNull(View)) Then
            RowsIndex = View.FocusedRowHandle
            TopVisibleIndex = View.TopRowIndex
            RowCount = View.RowCount
        End If

        crRow = Val(View.GetRowCellValue(RowsIndex, "FNMerMatSeq").ToString)
        nxRow = Val(View.GetRowCellValue(RowsIndex + 1, "FNMerMatSeq").ToString)

        nwRow = Str(crRow + 0.01).Trim

        If nxRow = nwRow Then
            Return
        End If

        Dim MaxSeq As Double = 1.0
        Dim LastSeq As Double = 0

        Dim dt As DataTable = CType(ogcmatcode.DataSource, DataTable)

        For Each r As DataRow In dt.Rows
            LastSeq = r.Item("FNSeq")
            If LastSeq >= MaxSeq Then
                MaxSeq = LastSeq + 1
            End If
        Next
        If LastSeq = MaxSeq Then MaxSeq = LastSeq + 1

        Dim dr As DataRow = dt.NewRow()
        dr.Item("FNHSysStyleId") = Val(FNHSysStyleId.Properties.Tag.ToString)
        dr.Item("FNSeq") = MaxSeq
        dr.Item("FNMerMatSeq") = nwRow
        dr.Item("FNPart") = "1"
        dr.Item("FTOrderID") = "-1"
        dr.Item("FTSubOrderID") = "-1"
        If HI.ST.Lang.Language = ST.Lang.Lang.TH Then
            dr.Item("FTOrderNo") = "ทั้งหมด"
            dr.Item("FTSubOrderNo") = "ทั้งหมด"
        Else
            dr.Item("FTOrderNo") = "ALL"
            dr.Item("FTSubOrderNo") = "ALL"
        End If
        dr.Item("FTStateNominate") = "1"
        dr.Item("FTStateCombination") = "0"
        dr.Item("FTStateActive") = "1"
        dr.Item("FTStateMainMaterial") = "0"
        dt.Rows.InsertAt(dr, RowsIndex + 1)

        'Call DoGrouping()

    End Sub

    Private Sub ocmbomnewcolorway_Click(sender As System.Object, e As System.EventArgs) Handles ocmbomnewcolorway.Click
        If sFNHSysStyleId = "" Then Return
        InitNewRow(CType(ogcstylecolor.DataSource, DataTable), TabIndexs.Colorway)
    End Sub

    Private Sub GridView1_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanging
        Dim vw2 As GridView = New GridView()
        Dim vw3 As GridView = New GridView()

        vw2 = ogcstylecolor.Views(0)
        vw2.SetRowCellValue(e.RowHandle, e.Column.FieldName, e.Value)
        ogcstylecolor = vw2.GridControl
        ogcstylecolor.Refresh()

        vw3 = ogcstylesize.Views(0)
        vw3.SetRowCellValue(e.RowHandle, e.Column.FieldName, e.Value)
        ogcstylesize = vw3.GridControl
        ogcstylesize.Refresh()

    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown
        Dim vw1 As GridView = New GridView()
        Dim SelectedRow As Integer, FocusedColumn As Integer

        vw1 = ogcmatcode.Views(0)
        If (Not IsDBNull(vw1)) Then
            SelectedRow = vw1.FocusedRowHandle
            FocusedColumn = vw1.FocusedColumn.AbsoluteIndex
        End If

        If e.KeyCode = Keys.Return Or e.KeyCode = Keys.Tab Then
            If FocusedColumn = GridView1.Columns.Count - 1 Then
                If SelectedRow = vw1.RowCount - 1 Then
                    InitNewRow(CType(ogcmatcode.DataSource, DataTable), TabIndexs.StyleDetail)
                End If
            End If

        End If

        If e.KeyCode = Keys.Down Then
            If SelectedRow = vw1.RowCount - 1 Then
                InitNewRow(CType(ogcmatcode.DataSource, DataTable), TabIndexs.StyleDetail)
            End If
        End If

    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclearclsr.Click
        Me.ogcmatcode.DataSource = Nothing
        Me.ogcstylecolor.DataSource = Nothing
        Me.ogcstylesize.DataSource = Nothing

        Dim xCol As Integer = 0
        Dim Idx As Integer = 0
        Try
            Dim View1 As GridView
            Dim View2 As GridView

            View1 = Me.ogcstylecolor.Views(0)
            View1 = InitialGrid(View1)
            View1.BestFitColumns()

            View2 = Me.ogcstylesize.Views(0)
            View2 = InitialGrid(View2)
            View2.BestFitColumns()

            HI.TL.HandlerControl.ClearControl(Me)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub ocmrefresh_Click(sender As System.Object, e As System.EventArgs) Handles ocmrefresh.Click
        Me.ogcmatcode.DataSource = Nothing
        Me.ogcstylecolor.DataSource = Nothing
        Me.ogcstylesize.DataSource = Nothing

        If FNHSysStyleId.Text <> "" Then
            Dim _Str As String = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
            FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")

            If (FNHSysStyleId.Properties.Tag.ToString <> "") Then
                Call LoadStyleDetail(FNHSysStyleId.Properties.Tag.ToString)
                Call LoadColorwaySize(FNHSysStyleId.Properties.Tag.ToString)
            End If
        End If

    End Sub

    Private Sub InitNewRow(ByVal dataTable As System.Data.DataTable, ByVal TableIndexx As Integer)
        Try
            Dim dr As DataRow
            dtStyleDetail = dataTable

            dr = dtStyleDetail.NewRow()

            Dim MaxSeq As Double = 1.0
            Dim LastSeq As Double = 0

            Dim MaxMatSeq As Double = 1
            Dim LastMatSeq As Integer = 0
            Dim MaxRowIndex As Integer = 0

            For Each r As DataRow In dtStyleDetail.Rows
                LastSeq = r.Item("FNSeq")
                If LastSeq > MaxSeq Then
                    MaxSeq = LastSeq + 1
                    MaxRowIndex = dtStyleDetail.Rows.IndexOf(r)
                End If

                LastMatSeq = Int(r.Item("FNMerMatSeq"))
                If LastMatSeq > MaxMatSeq Then
                    MaxMatSeq = LastMatSeq + 1
                End If

            Next
            If LastSeq = MaxSeq Then MaxSeq = LastSeq + 1
            If LastMatSeq = MaxMatSeq Then MaxMatSeq = LastMatSeq + 1

            'LastMatSeq = Int(dtStyleDetail.Rows(MaxRowIndex).Item("FNMerMatSeq").ToString) + 1

            If TableIndexx = 0 Then
                dr.Item("FNHSysStyleId") = Val(FNHSysStyleId.Properties.Tag.ToString)
                dr.Item("FNSeq") = MaxSeq
                dr.Item("FNMerMatSeq") = MaxMatSeq
                dr.Item("FNHSysMerMatId") = Val(FNHSysStyleId.Properties.Tag.ToString)
                dr.Item("FNPart") = "1"
                dr.Item("FTOrderID") = "-1"
                dr.Item("FTSubOrderID") = "-1"

                If HI.ST.Lang.Language = ST.Lang.Lang.TH Then
                    dr.Item("FTOrderNo") = "ทั้งหมด"
                    dr.Item("FTSubOrderNo") = "ทั้งหมด"
                Else
                    dr.Item("FTOrderNo") = "ALL"
                    dr.Item("FTSubOrderNo") = "ALL"
                End If

                dr.Item("FTStateNominate") = "1"
                dr.Item("FTStateCombination") = "0"
                dr.Item("FTStateActive") = "1"
                dr.Item("FTStateMainMaterial") = "0"
                dtStyleDetail.Rows.Add(dr)

                ogcmatcode.DataSource = dtStyleDetail
                ogcmatcode.Refresh()

            ElseIf TableIndexx = 1 Then
                dr.Item("FNHSysStyleId") = Val(FNHSysStyleId.Properties.Tag.ToString)
                dr.Item("FNSeq") = MaxSeq
                dr.Item("FNColorWaySeq") = MaxSeq
                dtStyleDetail.Rows.Add(dr)

                ogcstylecolor.DataSource = dtStyleDetail
                ogcstylecolor.Refresh()

            Else
                dr.Item("FNHSysStyleId") = Val(FNHSysStyleId.Properties.Tag.ToString)
                dr.Item("FNSeq") = MaxSeq
                dr.Item("FNSieBreakDownSeq") = MaxSeq
                dtStyleDetail.Rows.Add(dr)

                ogcstylesize.DataSource = dtStyleDetail
                ogcstylesize.Refresh()
            End If

            'Call DoGrouping()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ocmbomdeleterow_Click(sender As System.Object, e As System.EventArgs) Handles ocmbomdeleterow.Click
        View = Me.ogcmatcode.Views(0)

        If (Not IsDBNull(View)) Then
            RowsIndex = View.FocusedRowHandle
            TopVisibleIndex = View.TopRowIndex
        End If

        If RowsIndex < 0 Then Return

        If Not (View.PostEditor() And View.UpdateCurrentRow()) Then Return

        'Delete row of Style Detail.
        If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, ) = True Then
            If DoDeleteSource(oleDbDataAdapter2, CType(ogcmatcode.DataSource, DataTable), RowsIndex, TabIndexs.StyleDetail) = False Then Return
            DoDeleteSource(oleDbDataAdapter2, CType(ogcstylecolor.DataSource, DataTable), RowsIndex, TabIndexs.Colorway)
            DoDeleteSource(oleDbDataAdapter2, CType(ogcstylesize.DataSource, DataTable), RowsIndex, TabIndexs.SizeBreakdown)

            UpdateDatasource()

            Dim View1 As GridView = Me.ogcmatcode.Views(0)
            'Dim View2 As GridView = Me.ogcstylecolor.Views(0)
            'Dim View3 As GridView = Me.ogcstylesize.Views(0)

            'View1.SelectRow(RowsIndex - 1)
            If RowsIndex >= View1.RowCount Then RowsIndex = View1.RowCount - 1
            View1.FocusedRowHandle = RowsIndex ' 1
            'ogcmatcode = View1.GridControl
            'ogcmatcode.Refresh()
        End If
    End Sub

    Private Sub ocmdeletecolorway_Click(sender As System.Object, e As System.EventArgs) Handles ocmdeletecolorway.Click
        View = Me.ogcstylecolor.Views(0)

        If (Not IsNothing(View)) Then
            RowsIndex = View.FocusedRowHandle
            TopVisibleIndex = View.TopRowIndex
        End If

        If RowsIndex < 0 Then Return

        Dim dtStyleDetail As DataTable = CType(ogcstylecolor.DataSource, DataTable)

        If Not (View.PostEditor() And View.UpdateCurrentRow()) Then Return

        'Delete row of Color way
        If DoDeleteSource(oleDbDataAdapter2, dtStyleDetail, RowsIndex, TabIndexs.Colorway) = False Then Return

    End Sub

    Private Sub ocmbomimportfromorder_Click(sender As System.Object, e As System.EventArgs) Handles ocmbomimportfromorder.Click
        Call ImportColor()
        Call ImportSize()
    End Sub

    Private Sub ImportColor()

        Dim dataAdapter As SqlDataAdapter
        Dim dt As DataTable = New DataTable()
        Dim dc As DataColumn
        Dim dc1 As DataColumn

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Try
            dataAdapter = CreateAdapterImportColor(HI.Conn.SQLConn.Cnn)

            Dim SelectCmd As SqlCommand = New SqlCommand(dataAdapter.SelectCommand.CommandText, HI.Conn.SQLConn.Cnn)
            SelectCmd.Parameters.AddWithValue("@FNHSysStyleId", Val(FNHSysStyleId.Properties.Tag.ToString))
            SelectCmd.ExecuteNonQuery()

            dataAdapter.SelectCommand = SelectCmd
            dataAdapter.Fill(dt)

            Dim StrSql As String = ""

            Dim sqlCmd As New SqlCommand
            sqlCmd.Connection = HI.Conn.SQLConn.Cnn
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_STYLE_COLORWAY]"
            sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", Val(FNHSysStyleId.Properties.Tag.ToString))
            sqlCmd.Parameters.AddWithValue("@LANGID", HI.ST.Lang.Language.ToString())

            oleDbDataAdapter2 = New SqlClient.SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
            oleDbDataAdapter2.SelectCommand = sqlCmd

            dtStyleDetail = New DataTable()
            oleDbDataAdapter2.Fill(dtStyleDetail)

            '' Initial data to dynamic column
            Dim InitDt As DataTable = New DataTable()
            For Each c As DataColumn In dtStyleDetail.Columns
                InitDt.Columns.Add(c.ColumnName, c.DataType)
            Next

            '' Insert Dynamic Grid Column Header
            View = Me.ogcstylecolor.Views(0)
            View = InitialGrid(View)
            View.BestFitColumns()

            Dim FNSeqCurr As Integer = 0
            Dim FNSeqLast As Integer = 0

            For Each r As DataRow In dt.Rows
                'FNSeqCurr = Val(r!FNMatColorSeq.ToString)
                'If FNSeqLast = 0 Then FNSeqLast = FNSeqCurr
                'If FNSeqCurr > 0 And FNSeqLast < FNSeqCurr Then

                dc = New DataColumn("FTRawMatColorCode" & r!FTColorWay.ToString, System.Type.GetType("System.String"))
                dc1 = New DataColumn("FNHSysRawMatColorId" & "FTRawMatColorCode" & r!FTColorWay.ToString, System.Type.GetType("System.String"))
                dc.Caption = r!FTColorWay.ToString
                dc1.Caption = "FNHSysRawMatColorId"

                If View.Columns(dc.ColumnName) Is Nothing Then

                    Dim ExistCol As Boolean = False
                    For Each x As GridColumn In View.Columns
                        If x.Tag = r!FNHSysMatColorId.ToString Then
                            ExistCol = True
                        End If
                    Next

                    If ExistCol = False Then

                        Try
                            View.Columns.Item(dc.ColumnName).FieldName = dc.ColumnName
                            View.Columns.Item(dc1.ColumnName).FieldName = dc1.ColumnName
                            InitDt.Columns.Add(dc.ColumnName)
                            InitDt.Columns.Add(dc1.ColumnName)
                        Catch ex As Exception
                            View.Columns.AddField(dc.ColumnName)
                            View.Columns(dc.ColumnName).FieldName = dc.ColumnName
                            View.Columns(dc.ColumnName).Name = dc.ColumnName
                            View.Columns(dc.ColumnName).Caption = dc.Caption
                            View.Columns(dc.ColumnName).Visible = True
                            View.Columns(dc.ColumnName).Width = 90

                            View.Columns.AddField(dc1.ColumnName)
                            View.Columns(dc1.ColumnName).FieldName = dc1.ColumnName
                            View.Columns(dc1.ColumnName).Name = dc1.ColumnName
                            View.Columns(dc1.ColumnName).Caption = dc1.Caption
                            View.Columns(dc1.ColumnName).Tag = r!FNHSysMatColorId.ToString
                            View.Columns(dc1.ColumnName).Visible = False

                            Dim repos As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
                            repos.Buttons.Item(0).Tag = "114"
                            View.Columns(dc.ColumnName).ColumnEdit = repos

                            With repos
                                AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
                                AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
                                AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
                                AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave

                            End With

                            InitDt.Columns.Add(dc.ColumnName)
                            InitDt.Columns.Add(dc1.ColumnName)
                            'dtStyleDetail.Columns.Add(dc)
                            'dtStyleDetail.Columns(dc.ColumnName) = dtStyleDetail.Columns(FTColorWay)
                        End Try
                    End If

                End If

                '' Initialize
                'FNSeqLast = FNSeqCurr

            Next


            '' Get new datasource colorway
            sqlCmd = New SqlCommand
            sqlCmd.Connection = HI.Conn.SQLConn.Cnn
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_STYLE_COLORWAYINFO]"
            sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", Val(FNHSysStyleId.Properties.Tag.ToString))
            sqlCmd.Parameters.AddWithValue("@LANGID", HI.ST.Lang.Language.ToString())

            oleDbDataAdapter2 = New SqlClient.SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
            oleDbDataAdapter2.SelectCommand = sqlCmd

            Dim dtColor As DataTable = New DataTable()
            oleDbDataAdapter2.Fill(dtColor)

            '' Fill data to new datatable
            View = Me.ogcstylecolor.Views(0)
            Dim FNSeqSame As Boolean = False
            For Each r As DataRow In dtColor.Rows
                Dim n As DataRow

                FNSeqSame = False
                For Each x As DataRow In InitDt.Rows
                    If x.Item("FNSeq") = r.Item("FNSeq") Then
                        FNSeqSame = True

                        For Each c As GridColumn In View.Columns
                            If c.Tag = r.Item("FNHSysMatColorId").ToString() Then

                                x.Item("FTRawMatColorCode" & r!FTMatColorName.ToString) = r.Item("FTColorWay")
                                x.Item(c.Name) = r.Item("FNHSysRawMatColorId")

                                Exit For
                            End If
                        Next

                    End If
                Next

                If FNSeqSame = False Then
                    n = InitDt.NewRow()
                    n.Item("FNHSysStyleId") = r.Item("FNHSysStyleId")
                    n.Item("FNSeq") = r.Item("FNSeq")
                    n.Item("FNMerMatSeq") = r.Item("FNMerMatSeq")
                    n.Item("FTMainMatCode") = r.Item("FTMainMatCode")
                    n.Item("FTMainMatName") = r.Item("FTMainMatName")
                    n.Item("FNPart") = r.Item("FNPart")
                    n.Item("FTPositionPartName") = r.Item("FTPositionPartName")
                    n.Item("FNConSmp") = r.Item("FNConSmp")
                    n.Item("FNConSmpPlus") = r.Item("FNConSmpPlus")

                    For Each c As GridColumn In View.Columns
                        If c.Tag = r.Item("FNHSysMatColorId").ToString() And r.Item("FNHSysMatColorId").ToString() <> "" Then
                            n.Item("FNColorWaySeq") = r.Item("FNColorWaySeq")
                            n.Item("FTRunColor") = r.Item("FTRunColor")
                            n.Item("FTRawMatColorCode" & r!FTMatColorName.ToString) = r.Item("FTColorWay")
                            n.Item(c.Name) = r.Item("FNHSysRawMatColorId")

                            Exit For
                        End If
                    Next

                    InitDt.Rows.Add(n)
                End If

            Next

            InitDt.AcceptChanges()

            'View.Columns("FTColorWay").Visible = False
            View.BestFitColumns()
            View.OptionsView.ShowAutoFilterRow = True

            ogcstylecolor.DataSource = InitDt
            ogcstylecolor = View.GridControl
            ogcstylecolor.Refresh()

            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            'MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub ImportSize()

        Dim dataAdapter As SqlDataAdapter
        Dim dt As DataTable = New DataTable()
        Dim dc As DataColumn
        Dim dc1 As DataColumn

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Try
            dataAdapter = CreateAdapterImportSize(HI.Conn.SQLConn.Cnn)

            Dim SelectCmd As SqlCommand = New SqlCommand(dataAdapter.SelectCommand.CommandText, HI.Conn.SQLConn.Cnn)
            SelectCmd.Parameters.AddWithValue("@FNHSysStyleId", Val(FNHSysStyleId.Properties.Tag.ToString))
            SelectCmd.ExecuteNonQuery()

            dataAdapter.SelectCommand = SelectCmd
            dataAdapter.Fill(dt)

            Dim StrSql As String = ""

            'Pivoted Size Break down by Style Material
            Dim sqlCmd As New SqlCommand
            sqlCmd.Connection = HI.Conn.SQLConn.Cnn
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_STYLE_SIZEBREAKDOWN]"
            sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", Val(FNHSysStyleId.Properties.Tag.ToString))
            sqlCmd.Parameters.AddWithValue("@LANGID", HI.ST.Lang.Language.ToString())

            oleDbDataAdapter2 = New SqlClient.SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
            oleDbDataAdapter2.SelectCommand = sqlCmd

            dtStyleDetail = New DataTable()
            oleDbDataAdapter2.Fill(dtStyleDetail)


            '' Initial data to dynamic column
            Dim InitDt As DataTable = New DataTable()
            For Each c As DataColumn In dtStyleDetail.Columns
                InitDt.Columns.Add(c.ColumnName, c.DataType)
            Next

            '' Insert Dynamic Grid Column Header
            View = Me.ogcstylesize.Views(0)
            View = InitialGrid(View)
            View.BestFitColumns()

            'Dim FNSeqCurr As Integer = 0
            'Dim FNSeqLast As Integer = 0

            For Each r As DataRow In dt.Rows
                'FNSeqCurr = Val(r!FNMatSizeSeq.ToString)
                'If FNSeqLast = 0 Then FNSeqLast = FNSeqCurr
                'If FNSeqCurr > 0 And FNSeqLast < FNSeqCurr Then

                dc = New DataColumn("FTRawMatSizeCode" & r!FTSizeBreakDown.ToString, System.Type.GetType("System.String"))
                dc1 = New DataColumn("FNHSysRawMatSizeId" & "FTRawMatSizeCode" & r!FTSizeBreakDown.ToString, System.Type.GetType("System.String"))
                dc.Caption = r!FTSizeBreakDown.ToString
                dc1.Caption = "FNHSysRawMatSizeId"

                If View.Columns(dc.ColumnName) Is Nothing Then

                    Dim ExistCol As Boolean = False
                    For Each x As GridColumn In View.Columns
                        If x.Tag = r!FNHSysMatSizeId.ToString Then
                            ExistCol = True
                        End If
                    Next

                    If ExistCol = False Then

                        Try
                            View.Columns.Item(dc.ColumnName).FieldName = dc.ColumnName
                            View.Columns.Item(dc1.ColumnName).FieldName = dc1.ColumnName
                            InitDt.Columns.Add(dc.ColumnName)
                            InitDt.Columns.Add(dc1.ColumnName)
                        Catch ex As Exception
                            View.Columns.AddField(dc.ColumnName)
                            View.Columns(dc.ColumnName).FieldName = dc.ColumnName
                            View.Columns(dc.ColumnName).Name = dc.ColumnName
                            View.Columns(dc.ColumnName).Caption = dc.Caption
                            View.Columns(dc.ColumnName).Visible = True
                            View.Columns(dc.ColumnName).Width = 80

                            View.Columns.AddField(dc1.ColumnName)
                            View.Columns(dc1.ColumnName).FieldName = dc1.ColumnName
                            View.Columns(dc1.ColumnName).Name = dc1.ColumnName
                            View.Columns(dc1.ColumnName).Caption = dc1.Caption
                            View.Columns(dc1.ColumnName).Tag = r!FNHSysMatSizeId.ToString
                            View.Columns(dc1.ColumnName).Visible = False

                            Dim repos As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
                            repos.Buttons.Item(0).Tag = "117"
                            View.Columns(dc.ColumnName).ColumnEdit = repos

                            With repos
                                AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
                                AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
                                AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
                                AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave

                            End With

                            InitDt.Columns.Add(dc.ColumnName)
                            InitDt.Columns.Add(dc1.ColumnName)
                            'dtStyleDetail.Columns.Add(dc)
                            'dtStyleDetail.Columns(dc.ColumnName) = dtStyleDetail.Columns(FTColorWay)
                        End Try

                    End If

                    '' Initialize
                    'FNSeqLast = FNSeqCurr

                End If
            Next


            'Get new size breakdown to datasource
            sqlCmd = New SqlCommand
            sqlCmd.Connection = HI.Conn.SQLConn.Cnn
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_STYLE_SIZEBREAKDOWNINFO]"
            sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", Val(FNHSysStyleId.Properties.Tag.ToString))
            sqlCmd.Parameters.AddWithValue("@LANGID", HI.ST.Lang.Language.ToString())

            oleDbDataAdapter2 = New SqlClient.SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
            oleDbDataAdapter2.SelectCommand = sqlCmd

            Dim dtSize As DataTable = New DataTable()
            oleDbDataAdapter2.Fill(dtSize)


            '' Fill data to new datatable
            Dim FNSeqSame As Boolean = False
            For Each r As DataRow In dtSize.Rows
                Dim n As DataRow

                FNSeqSame = False
                For Each x As DataRow In InitDt.Rows
                    If x.Item("FNSeq") = r.Item("FNSeq") Then
                        FNSeqSame = True

                        For Each c As GridColumn In View.Columns
                            If c.Tag = r.Item("FNHSysMatSizeId").ToString() Then
                                x.Item("FTRawMatSizeCode" & r!FTMatSizeName.ToString) = r.Item("FTSizeBreakDown")
                                x.Item(c.Name) = r.Item("FNHSysRawMatSizeId")

                                Exit For
                            End If
                        Next

                    End If
                Next

                If FNSeqSame = False Then
                    n = InitDt.NewRow()
                    n.Item("FNHSysStyleId") = r.Item("FNHSysStyleId")
                    n.Item("FNSeq") = r.Item("FNSeq")
                    n.Item("FNMerMatSeq") = r.Item("FNMerMatSeq")
                    n.Item("FTMainMatCode") = r.Item("FTMainMatCode")
                    n.Item("FTMainMatName") = r.Item("FTMainMatName")
                    n.Item("FNPart") = r.Item("FNPart")
                    n.Item("FTPositionPartName") = r.Item("FTPositionPartName")
                    n.Item("FNConSmp") = r.Item("FNConSmp")
                    n.Item("FNConSmpPlus") = r.Item("FNConSmpPlus")

                    For Each c As GridColumn In View.Columns
                        If c.Tag = r.Item("FNHSysMatSizeId").ToString() And r.Item("FNHSysMatSizeId").ToString() <> "" Then
                            n.Item("FNSieBreakDownSeq") = r.Item("FNSieBreakDownSeq")
                            n.Item("FTRunSize") = r.Item("FTRunSize")
                            n.Item("FTRawMatSizeCode" & r!FTMatSizeName.ToString) = r.Item("FTSizeBreakDown")
                            n.Item(c.Name) = r.Item("FNHSysRawMatSizeId")

                            Exit For
                        End If
                    Next

                    InitDt.Rows.Add(n)
                End If

            Next

            InitDt.AcceptChanges()

            'View.Columns("FTColorWay").Visible = False
            View.BestFitColumns()
            View.OptionsView.ShowAutoFilterRow = True

            ogcstylesize.DataSource = InitDt
            ogcstylesize = View.GridControl
            ogcstylesize.Refresh()

            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            'MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Function Update_OrderCombinationInfo(ByVal _FNHSysStyleId As Integer) As Boolean
        Dim ret As Boolean = True

        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

            Dim sqlCmd As New SqlCommand
            sqlCmd.Connection = HI.Conn.SQLConn.Cnn
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_UPDATE_COMBINATION_FACTORY_ORDER]"
            sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", _FNHSysStyleId)

            Dim sqlDA As New SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
            sqlDA.SelectCommand = sqlCmd
            sqlDA.SelectCommand.ExecuteNonQuery()

        Catch ex As Exception

            ret = False
        End Try

        Return ret
    End Function

    Private Function Update_OrderMainmaterialInfo(ByVal _FNHSysStyleId As Integer) As Boolean
        Dim ret As Boolean = True

        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

            Dim sqlCmd As New SqlCommand
            sqlCmd.Connection = HI.Conn.SQLConn.Cnn
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_UPDATE_MAINMATERIAL_FACTORY_ORDER]"
            sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", _FNHSysStyleId)

            Dim sqlDA As New SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
            sqlDA.SelectCommand = sqlCmd
            sqlDA.SelectCommand.ExecuteNonQuery()

        Catch ex As Exception

            ret = False
        End Try

        Return ret
    End Function

    Private Sub PostSave(ByVal TableName As String, Optional ByVal refDocKey As String = Nothing)
        Try
            '' Create Audit log.        
            HI.Auditor.CreateLog.CreateLogdata(dtBefore, dtAfter, Me.Name, TableName, refDocKey)
            dtBefore = Nothing
            dtAfter = Nothing
        Catch ex As Exception
            '' To do something
        End Try        
    End Sub

    Private Sub ocmsave_Click(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        'If Me.VerifyData() Then
        If Me.SaveData Then            
            '' Update order information.
            Update_OrderCombinationInfo(Val(Me.FNHSysStyleId.Properties.Tag.ToString))
            Update_OrderMainmaterialInfo(Val(Me.FNHSysStyleId.Properties.Tag.ToString))

            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Me.ProcComplete = True
            LoadStyleInfo(FNHSysStyleId.Properties.Tag.ToString)
            InitNewRow(CType(ogcmatcode.DataSource, DataTable), TabIndexs.StyleDetail)
        Else
            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        End If
        'End If
    End Sub

    Private Sub ocmdelete_Click(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click
        If CType(ogcmatcode.DataSource, DataTable).Rows.Count > 0 Then
            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text) = True Then
                Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
                If Me.DeleteAllData(_Spls) Then
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    HI.TL.HandlerControl.ClearControl(Me)
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If
        End If
    End Sub

    Private Sub ocmbomdiffpart_Click(sender As System.Object, e As System.EventArgs) Handles ocmbomdiffpart.Click

        Dim crRow As Double, nxRow As String, nwRow As String
        Dim RowCount As Integer = 0
        View = Me.ogcmatcode.Views(0)
        If sFNHSysStyleId = "" Then Return

        If (Not IsDBNull(View)) Then
            RowsIndex = View.FocusedRowHandle
            TopVisibleIndex = View.TopRowIndex
            RowCount = View.RowCount
        End If

        crRow = Val(View.GetRowCellValue(RowsIndex, "FNMerMatSeq").ToString)
        nxRow = Val(View.GetRowCellValue(RowsIndex + 1, "FNMerMatSeq").ToString)

        nwRow = Str(crRow + 0.01).Trim

        If nxRow = nwRow Then
            Return
        End If

        Dim MaxSeq As Double = 1.0
        Dim LastSeq As Double = 0

        Dim dt As DataTable = CType(ogcmatcode.DataSource, DataTable)

        For Each r As DataRow In dt.Rows
            LastSeq = r.Item("FNSeq")
            If LastSeq >= MaxSeq Then
                MaxSeq = LastSeq + 1
            End If
        Next
        If LastSeq = MaxSeq Then MaxSeq = LastSeq + 1


        Dim ItemID As Long = Val(View.GetRowCellValue(RowsIndex, "FNHSysMerMatId").ToString)
        Dim ItemCode As String = View.GetRowCellValue(RowsIndex, "FTMainMatCode").ToString
        Dim MaxPart As Integer = 0
        Dim LastPart As Integer = 0
        Dim dtP As DataTable = CType(ogcmatcode.DataSource, DataTable)

        For Each r As DataRow In dt.Rows
            If ItemID = Val(r.Item("FNHSysMerMatId")) Then
                LastPart = CInt(r.Item("FNPart"))
                If LastPart >= MaxPart Then
                    MaxPart = LastPart + 1
                End If
            End If
        Next
        If LastPart = MaxPart Then MaxPart = LastPart + 1

        Dim dr As DataRow = dt.NewRow()
        For Each c As DataColumn In dt.Columns
            Select Case c.ColumnName
                Case "FNSeq"
                    dr.Item(c) = MaxSeq
                Case "FNPart"
                    dr.Item(c) = MaxPart
                Case Else
                    dr.Item(c) = View.GetRowCellValue(RowsIndex, c.ColumnName).ToString
            End Select

        Next

        dt.Rows.InsertAt(dr, RowsIndex + 1)
        Call InitialGridMergCell()

    End Sub

    Private Function SaveData() As Boolean
        Dim ret As Boolean
        Dim _Str As String = ""

        Try

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try

                _Str = "SELECT TOP 1 FNHSysStyleId, FTStyleCode, FTStyleNameTH, FTStyleNameEN, FNHSysCustId, FNHSysSeasonId, FTStateActive FROM " & _
                    "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle] WHERE FNHSysStyleId = " & Val(Me.FNHSysStyleId.Properties.Tag.ToString) & " "
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

                        _Str &= vbCrLf & " SELECT " & Val(Me.FNHSysStyleId.Properties.Tag.ToString) & ", '" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "', '" & HI.UL.ULF.rpQuoted(FTStyleNameTH.Text) & "', '" & HI.UL.ULF.rpQuoted(FTStyleNameEN.Text) & "' "
                        _Str &= vbCrLf & ", " & Val(FNHSysCustId.Properties.Tag.ToString) & ", " & Val(FNHSysSeasonId.Properties.Tag.ToString) & ", 1 "
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

                        '' Create Data Table before update new value
                        Dim sqlBefore As String =
                            "SELECT     ST.FTStyleCode, ST.FTStyleNameTH, ST.FTStyleNameEN, ST.FTStateActive, CT.FTCustCode AS FNHSysCustId, SS.FTSeasonCode AS FNHSysSeasonId, ST.guid" & vbCrLf & _
                            "FROM         HITECH_MERCHAN.dbo.TMERTStyle AS ST LEFT OUTER JOIN" & vbCrLf & _
                            "HITECH_MASTER.dbo.TCNMCustomer AS CT ON ST.FNHSysCustId = CT.FNHSysCustId LEFT OUTER JOIN" & vbCrLf & _
                            "HITECH_MASTER.dbo.TMERMSeason AS SS ON ST.FNHSysSeasonId = SS.FNHSysSeasonId" & vbCrLf & _
                            "WHERE ST.FNHSysStyleId = " & Val(Me.FNHSysStyleId.Properties.Tag.ToString)
                        dtBefore = HI.Conn.SQLConn.GetDataTable(sqlBefore, Conn.DB.DataBaseName.DB_MERCHAN)

                        _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle] SET " & vbCrLf & _
                        "FNHSysStyleId = '" & Val(Me.FNHSysStyleId.Properties.Tag.ToString) & "', " & vbCrLf & _
                        "FTStyleCode = '" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "', " & vbCrLf & _
                        "FTStyleNameTH = '" & HI.UL.ULF.rpQuoted(FTStyleNameTH.Text) & "', " & vbCrLf & _
                        "FTStyleNameEN = '" & HI.UL.ULF.rpQuoted(FTStyleNameEN.Text) & "', " & vbCrLf & _
                        "FNHSysCustId = '" & Val(FNHSysCustId.Properties.Tag.ToString) & "', " & vbCrLf & _
                        "FNHSysSeasonId = '" & Val(FNHSysSeasonId.Properties.Tag.ToString) & "', " & vbCrLf & _
                        "FTStateActive = '" & 1 & "', " & vbCrLf & _
                        "FTUpdUser = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "', " & vbCrLf & _
                        "FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ", " & vbCrLf & _
                        "FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & "" & vbCrLf & _
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


                    '' Create Data Table after update new value
                    Dim sqlAfter As String =
                            "SELECT     ST.FTStyleCode, ST.FTStyleNameTH, ST.FTStyleNameEN, ST.FTStateActive, CT.FTCustCode AS FNHSysCustId, SS.FTSeasonCode AS FNHSysSeasonId, ST.guid" & vbCrLf & _
                            "FROM         HITECH_MERCHAN.dbo.TMERTStyle AS ST LEFT OUTER JOIN" & vbCrLf & _
                            "HITECH_MASTER.dbo.TCNMCustomer AS CT ON ST.FNHSysCustId = CT.FNHSysCustId LEFT OUTER JOIN" & vbCrLf & _
                            "HITECH_MASTER.dbo.TMERMSeason AS SS ON ST.FNHSysSeasonId = SS.FNHSysSeasonId" & vbCrLf & _
                            "WHERE ST.FNHSysStyleId = " & Val(Me.FNHSysStyleId.Properties.Tag.ToString)
                    dtAfter = HI.Conn.SQLConn.GetDataTable(sqlAfter, Conn.DB.DataBaseName.DB_MERCHAN)

                    Call PostSave("[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle]", Me.FNHSysStyleId.Text)

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

#End Region

#Region "Insert & Update Process"

    Private Function UpdateDatasource() As Boolean
        Dim ret As Boolean
        'Save the latest changes to the bound DataTable 
        View = Me.ogcmatcode.Views(0)
        View.ClearSorting()
        View.Columns("FNMerMatSeq").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        View.Columns("FNPart").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending


        Dim dt1 As DataTable = CType(ogcmatcode.DataSource, DataTable)
        Dim dt2 As DataTable = CType(ogcstylecolor.DataSource, DataTable)
        Dim dt3 As DataTable = CType(ogcstylesize.DataSource, DataTable)
        'If Not (View.PostEditor() And View.UpdateCurrentRow()) Then Return False

        ' Update Style detail.
        If IsNothing(dt1) Then
            ret = True
        Else
            ret = DoUpdateTable(oleDbDataAdapter2, dt1)
            If ret = False Then Return ret
        End If

        ' Update Color way
        If IsNothing(dt2) Then
            ret = True
        Else
            ret = DoUpdateColor(oleDbDataAdapter2, dt2)
            If ret = False Then Return ret
        End If

        ' Update Size Breakdown
        If IsNothing(dt3) Then
            ret = True
        Else
            ret = DoUpdateSize(oleDbDataAdapter2, dt3)
            If ret = False Then Return ret
        End If

        If (FNHSysStyleId.Properties.Tag <> "") Then
            Call LoadStyleDetail(FNHSysStyleId.Properties.Tag)
            Call LoadColorwaySize(FNHSysStyleId.Properties.Tag)
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

            '' Create Data Table after update new value
            Dim sqlBefore As String =
                "SELECT     SM.FNHSysStyleId, SM.FNMerMatSeq, MM.FTMainMatCode + ' ' + MM.FTMainMatNameEN AS FNHSysMerMatId, SM.FNPart, SM.FTPositionPartName, " & vbCrLf & _
                "      SP.FTSuplCode + ' ' + SP.FTSuplNameEN AS FNHSysSuplId, SM.FTStateNominate, UN.FTUnitCode + ' ' + UN.FTUnitNameEN AS FNHSysUnitId, SM.FNPrice, " & vbCrLf & _
                "      CC.FTCurCode + ' ' + CC.FTCurDescEN AS FNHSysCurId, SM.FNConSmp, SM.FNConSmpPlus, SM.FTOrderNo, SM.FTSubOrderNo, SM.FTStateActive, SM.FTStateCombination, " & vbCrLf & _
                "      SM.FTStateMainMaterial" & vbCrLf & _
                "FROM         HITECH_MERCHAN.dbo.TMERTStyle_Mat AS SM LEFT OUTER JOIN" & vbCrLf & _
                "                      HITECH_MASTER.dbo.TMERMMainMat AS MM ON SM.FNHSysMerMatId = MM.FNHSysMainMatId LEFT OUTER JOIN" & vbCrLf & _
                "                      HITECH_MASTER.dbo.TCNMSupplier AS SP ON SM.FNHSysSuplId = SP.FNHSysSuplId LEFT OUTER JOIN" & vbCrLf & _
                "                      HITECH_MASTER.dbo.TCNMUnit AS UN ON SM.FNHSysUnitId = UN.FNHSysUnitId LEFT OUTER JOIN" & vbCrLf & _
                "                      HITECH_MASTER.dbo.TFINMCurrency AS CC ON SM.FNHSysCurId = CC.FNHSysCurId" & vbCrLf & _
                "WHERE SM.FNHSysStyleId = " & Val(Me.FNHSysStyleId.Properties.Tag.ToString)
            dtBefore = HI.Conn.SQLConn.GetDataTable(sqlBefore, Conn.DB.DataBaseName.DB_MERCHAN)

            ' Add parameters and set values.
            If dt.Rows.Count = 0 Then Return True
            For Each r As DataRow In dt.Rows
                Dim SelectCMD As SqlCommand = New SqlCommand(dataAdapter.SelectCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                SelectCMD.Parameters.AddWithValue("@FNHSysStyleId", (r.Item("FNHSysStyleId").ToString))
                SelectCMD.Parameters.AddWithValue("@FNSeq", (r.Item("FNSeq").ToString))
                Dim cnt As Integer
                cnt = SelectCMD.ExecuteScalar
                If cnt = 0 Then
                    If r.Item("FTMainMatCode").ToString <> "" Then
                        InsertCMD.Parameters.AddWithValue("@FNHSysStyleId", Val(r.Item("FNHSysStyleId").ToString))
                        InsertCMD.Parameters.AddWithValue("@FNSeq", r.Item("FNSeq").ToString)
                        InsertCMD.Parameters.AddWithValue("@FNMerMatSeq", r.Item("FNMerMatSeq").ToString) ''dt.Rows.IndexOf(r) + 1) 'r.Item("FNSeq").ToString)
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
                        InsertCMD.Parameters.AddWithValue("@FTOrderNo", r.Item("FTOrderID").ToString)
                        InsertCMD.Parameters.AddWithValue("@FTSubOrderNo", r.Item("FTSubOrderID").ToString)
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
                    End If
                Else
                    UpdateCmd.Parameters.AddWithValue("@FNHSysStyleId", r.Item("FNHSysStyleId").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FNSeq", r.Item("FNSeq").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FNMerMatSeq", r.Item("FNMerMatSeq").ToString) 'r.Item("FNSeq").ToString)
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
                    UpdateCmd.Parameters.AddWithValue("@FTOrderNo", r.Item("FTOrderID").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FTSubOrderNo", r.Item("FTSubOrderID").ToString)
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

            '' Create Data Table after update new value
            Dim sqlAfter As String = sqlBefore
            dtAfter = HI.Conn.SQLConn.GetDataTable(sqlAfter, Conn.DB.DataBaseName.DB_MERCHAN)

            Call PostSave("[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle_Mat]", Me.FNHSysStyleId.Text)

            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            MessageBox.Show(ex.Message)
            Return False
        End Try
    End Function

    Private Function DoUpdateColor(ByVal dataAdapter As SqlDataAdapter, _
        ByVal dataTable As System.Data.DataTable) As Boolean
        Dim InsertSql As String = ""

        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim dt As DataTable
            dt = dataTable 'CType(ogcstylecolor.DataSource, DataTable)

            dataAdapter = CreateAdapterColor(HI.Conn.SQLConn.Cnn)
            InsertSql = dataAdapter.InsertCommand.CommandText
            Dim InsertCMD As SqlCommand = New SqlCommand(dataAdapter.InsertCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
            Dim UpdateCmd As SqlCommand = New SqlCommand(dataAdapter.UpdateCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)

            Dim UpdateDate As String = "", UpdateTime As String = ""
            Dim StrSql As String = "SELECT " & HI.UL.ULDate.FormatDateDB & " AS UpdateDate, " & HI.UL.ULDate.FormatTimeDB & " AS UpdateTime"
            Dim dtx As DataTable
            dtx = HI.Conn.SQLConn.GetDataTableOnbeginTrans(StrSql, Conn.DB.DataBaseName.DB_MERCHAN)

            If dtx.Rows.Count > 0 Then
                For Each Rx As DataRow In dtx.Rows
                    UpdateDate = Rx!UpdateDate.ToString()
                    UpdateTime = Rx!UpdateTime.ToString()
                Next
            End If

            Dim Views As GridView = New GridView()
            Views = ogcstylecolor.Views(0)

            '' Create Data Table after update new value
            Dim sqlBefore As String =
                "SELECT     SC.FNHSysStyleId, SC.FNSeq, SC.FNMerMatSeq, SC.FNColorWaySeq, SC.FTRunColor, 'Rawmat color ' + IC.FTRawMatColorCode + ': ' + IC.FTRawMatColorNameEN AS FNHSysRawMatColorId, " & vbCrLf & _
                "                      'Color code ' + MC.FTMatColorCode + ': ' + MC.FTMatColorNameEN AS FNHSysMatColorId" & vbCrLf & _
                "FROM         HITECH_MERCHAN.dbo.TMERTStyle_ColorWay AS SC LEFT OUTER JOIN" & vbCrLf & _
                "                      HITECH_MASTER.dbo.TMERMMatColor AS MC ON SC.FNHSysMatColorId = MC.FNHSysMatColorId LEFT OUTER JOIN" & vbCrLf & _
                "                      HITECH_MASTER.dbo.TINVENMMatColor AS IC ON SC.FNHSysRawMatColorId = IC.FNHSysRawMatColorId" & vbCrLf & _
                "WHERE SC.FNHSysStyleId = " & Val(Me.FNHSysStyleId.Properties.Tag.ToString) & vbCrLf & _
                "ORDER BY SC.FNSeq, SC.FNMerMatSeq, SC.FNColorWaySeq"
            dtBefore = HI.Conn.SQLConn.GetDataTable(sqlBefore, Conn.DB.DataBaseName.DB_MERCHAN)

            ' Add parameters and set values.
            If dt.Rows.Count = 0 Then Return True
            For Each r As DataRow In dt.Rows
                Dim SelectCMD As SqlCommand = New SqlCommand(dataAdapter.SelectCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                Dim CIndex As Integer = 0
                Dim Cols As Integer = dt.Columns.Count
                Dim Colx As Integer = 0
                For Each c As DataColumn In dt.Columns
                    Colx += 1
                    If dt.Columns.IndexOf(c) > 13 Then
                        If dt.Columns.IndexOf(c) Mod 2 = 1 Then
                            CIndex += 1
                            SelectCMD.Parameters.AddWithValue("@FNHSysStyleId", Val(r.Item("FNHSysStyleId").ToString))
                            SelectCMD.Parameters.AddWithValue("@FNSeq", r.Item("FNSeq").ToString)
                            SelectCMD.Parameters.AddWithValue("@FNColorWaySeq", CIndex)

                            Dim cnt As Integer
                            cnt = SelectCMD.ExecuteScalar
                            If cnt = 0 Then

                                InsertCMD.Parameters.AddWithValue("@FNHSysStyleId", Val(r.Item("FNHSysStyleId").ToString))
                                InsertCMD.Parameters.AddWithValue("@FNSeq", r.Item("FNSeq").ToString)
                                InsertCMD.Parameters.AddWithValue("@FNMerMatSeq", CDbl(r.Item("FNMerMatSeq").ToString))
                                InsertCMD.Parameters.AddWithValue("@FTRunColor", r.Item("FTRunColor").ToString)

                                InsertCMD.Parameters.AddWithValue("@FNColorWaySeq", CIndex)
                                InsertCMD.Parameters.AddWithValue("@FNHSysRawMatColorId", r.Item(c.ColumnName.ToString).ToString)
                                InsertCMD.Parameters.AddWithValue("@FNHSysMatColorId", Views.Columns(c.ColumnName.ToString).Tag.ToString)

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

                                UpdateCmd.Parameters.AddWithValue("@FNHSysStyleId", Val(r.Item("FNHSysStyleId").ToString))
                                UpdateCmd.Parameters.AddWithValue("@FNSeq", r.Item("FNSeq").ToString)
                                UpdateCmd.Parameters.AddWithValue("@FNMerMatSeq", CDbl(r.Item("FNMerMatSeq").ToString))
                                UpdateCmd.Parameters.AddWithValue("@FTRunColor", r.Item("FTRunColor").ToString)

                                UpdateCmd.Parameters.AddWithValue("@FNColorWaySeq", CIndex)
                                UpdateCmd.Parameters.AddWithValue("@FNHSysRawMatColorId", r.Item(c.ColumnName.ToString).ToString)
                                UpdateCmd.Parameters.AddWithValue("@FNHSysMatColorId", Views.Columns(c.ColumnName.ToString).Tag.ToString)

                                UpdateCmd.Parameters.AddWithValue("@FTUpdUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
                                UpdateCmd.Parameters.AddWithValue("@FDUpdDate", UpdateDate)
                                UpdateCmd.Parameters.AddWithValue("@FTUpdTime", UpdateTime)

                                UpdateCmd.CommandType = CommandType.Text
                                UpdateCmd.ExecuteNonQuery()
                                UpdateCmd.Parameters.Clear()
                            End If

                            SelectCMD.Parameters.Clear()

                        End If
                    End If
                Next

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            '' Create Data Table after update new value
            Dim sqlAfter As String = sqlBefore
            dtAfter = HI.Conn.SQLConn.GetDataTable(sqlAfter, Conn.DB.DataBaseName.DB_MERCHAN)

            Call PostSave("[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle_ColorWay]", Me.FNHSysStyleId.Text)

            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            MessageBox.Show(ex.Message)
            Return False
        End Try
    End Function

    Private Function DoUpdateSize(ByVal dataAdapter As SqlDataAdapter, _
        ByVal dataTable As System.Data.DataTable) As Boolean
        Dim InsertSql As String = ""
        Dim Views As GridView = New GridView()
        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim dt As DataTable
            dt = dataTable 'CType(ogcstylecolor.DataSource, DataTable)

            dataAdapter = CreateAdapterSize(HI.Conn.SQLConn.Cnn)
            InsertSql = dataAdapter.InsertCommand.CommandText
            Dim InsertCMD As SqlCommand = New SqlCommand(dataAdapter.InsertCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
            Dim UpdateCmd As SqlCommand = New SqlCommand(dataAdapter.UpdateCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)

            Dim UpdateDate As String = "", UpdateTime As String = ""
            Dim StrSql As String = "SELECT " & HI.UL.ULDate.FormatDateDB & " AS UpdateDate, " & HI.UL.ULDate.FormatTimeDB & " AS UpdateTime"
            Dim dtx As DataTable
            dtx = HI.Conn.SQLConn.GetDataTableOnbeginTrans(StrSql, Conn.DB.DataBaseName.DB_MERCHAN)

            If dtx.Rows.Count > 0 Then
                For Each Rx As DataRow In dtx.Rows
                    UpdateDate = Rx!UpdateDate.ToString()
                    UpdateTime = Rx!UpdateTime.ToString()
                Next
            End If

            Views = ogcstylesize.Views(0)

            '' Create Data Table after update new value
            Dim sqlBefore As String =
                "SELECT     SS.FNHSysStyleId, SS.FNSeq, SS.FNMerMatSeq, SS.FNSieBreakDownSeq, SS.FTSizeBreakDown, SS.FTRunSize, 'Rawmat size code ' + VS.FTRawMatSizeCode + ': ' + VS.FTRawMatSizeNameEN AS FNHSysRawMatSizeId," & vbCrLf & _
                "                       'Size code ' + MS.FTMatSizeCode + ': ' + MS.FTMatSizeNameEN AS FNHSysMatSizeId" & vbCrLf & _
                "FROM         HITECH_MERCHAN.dbo.TMERTStyle_SizeBreakDown AS SS LEFT OUTER JOIN" & vbCrLf & _
                "                      HITECH_MASTER.dbo.TINVENMMatSize AS VS ON SS.FNHSysRawMatSizeId = VS.FNHSysRawMatSizeId LEFT OUTER JOIN" & vbCrLf & _
                "                      HITECH_MASTER.dbo.TMERMMatSize AS MS ON SS.FNHSysMatSizeId = MS.FNHSysMatSizeId" & vbCrLf & _
                "WHERE SS.FNHSysStyleId = " & Val(Me.FNHSysStyleId.Properties.Tag.ToString) & vbCrLf & _
                "ORDER BY SS.FNSeq, SS.FNMerMatSeq, SS.FNSieBreakDownSeq"
            dtBefore = HI.Conn.SQLConn.GetDataTable(sqlBefore, Conn.DB.DataBaseName.DB_MERCHAN)

            ' Add parameters and set values.
            If dt.Rows.Count = 0 Then Return True
            For Each r As DataRow In dt.Rows
                Dim SelectCMD As SqlCommand = New SqlCommand(dataAdapter.SelectCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                Dim CIndex As Integer = 0
                Dim Cols As Integer = dt.Columns.Count
                Dim Colx As Integer = 0
                For Each c As DataColumn In dt.Columns
                    Colx += 1
                    If dt.Columns.IndexOf(c) > 13 Then
                        If dt.Columns.IndexOf(c) Mod 2 = 1 Then
                            CIndex += 1
                            SelectCMD.Parameters.AddWithValue("@FNHSysStyleId", Val(r.Item("FNHSysStyleId").ToString))
                            SelectCMD.Parameters.AddWithValue("@FNSeq", r.Item("FNSeq").ToString)
                            SelectCMD.Parameters.AddWithValue("@FNSieBreakDownSeq", CIndex)

                            Dim cnt As Integer
                            cnt = SelectCMD.ExecuteScalar
                            If cnt = 0 Then

                                InsertCMD.Parameters.AddWithValue("@FNHSysStyleId", Val(r.Item("FNHSysStyleId").ToString))
                                InsertCMD.Parameters.AddWithValue("@FNSeq", r.Item("FNSeq").ToString)
                                InsertCMD.Parameters.AddWithValue("@FNMerMatSeq", CDbl(r.Item("FNMerMatSeq").ToString))

                                InsertCMD.Parameters.AddWithValue("@FTRunSize", r.Item("FTRunSize").ToString)
                                InsertCMD.Parameters.AddWithValue("@FNSieBreakDownSeq", CIndex)
                                InsertCMD.Parameters.AddWithValue("@FNHSysRawMatSizeId", r.Item(c.ColumnName.ToString).ToString)
                                InsertCMD.Parameters.AddWithValue("@FNHSysMatSizeId", Views.Columns(c.ColumnName.ToString).Tag.ToString)

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

                                UpdateCmd.Parameters.AddWithValue("@FNHSysStyleId", Val(r.Item("FNHSysStyleId").ToString))
                                UpdateCmd.Parameters.AddWithValue("@FNSeq", r.Item("FNSeq").ToString)
                                UpdateCmd.Parameters.AddWithValue("@FNMerMatSeq", CDbl(r.Item("FNMerMatSeq").ToString))

                                UpdateCmd.Parameters.AddWithValue("@FTRunSize", r.Item("FTRunSize").ToString)
                                UpdateCmd.Parameters.AddWithValue("@FNSieBreakDownSeq", CIndex)
                                UpdateCmd.Parameters.AddWithValue("@FNHSysRawMatSizeId", r.Item(c.ColumnName.ToString).ToString)
                                UpdateCmd.Parameters.AddWithValue("@FNHSysMatSizeId", Views.Columns(c.ColumnName.ToString).Tag.ToString)

                                UpdateCmd.Parameters.AddWithValue("@FTUpdUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
                                UpdateCmd.Parameters.AddWithValue("@FDUpdDate", UpdateDate)
                                UpdateCmd.Parameters.AddWithValue("@FTUpdTime", UpdateTime)

                                UpdateCmd.CommandType = CommandType.Text
                                UpdateCmd.ExecuteNonQuery()
                                UpdateCmd.Parameters.Clear()
                            End If

                            SelectCMD.Parameters.Clear()

                        End If
                    End If
                Next
            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            '' Create Data Table after update new value
            Dim sqlAfter As String = sqlBefore
            dtAfter = HI.Conn.SQLConn.GetDataTable(sqlAfter, Conn.DB.DataBaseName.DB_MERCHAN)

            Call PostSave("[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle_SizeBreakDown]", Me.FNHSysStyleId.Text)
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

    Private Function DeleteAllData(ByVal objspl As HI.TL.SplashScreen) As Boolean
        Try

            Dim _Dt As DataTable = CType(ogcmatcode.DataSource, DataTable)
            Dim _Str As String = ""
            Dim tEDateExec As String = ""
            Dim tDateNext As String = ""

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            objspl.UpdateInformation("Deleting Style No " & FNHSysStyleId.Properties.Tag.ToString())

            '' Delete Color
            _Str = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_ColorWay "
            _Str &= vbCrLf & " WHERE FNHSysStyleId =" & Val(FNHSysStyleId.Properties.Tag.ToString())
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            '' Delete Size
            _Str = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_SizeBreakDown "
            _Str &= vbCrLf & " WHERE FNHSysStyleId =" & Val(FNHSysStyleId.Properties.Tag.ToString())
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            '' Delete detail  
            _Str = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle_Mat "
            _Str &= vbCrLf & " WHERE FNHSysStyleId =" & Val(FNHSysStyleId.Properties.Tag.ToString())
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            '' Delete Header
            _Str = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle "
            _Str &= vbCrLf & " WHERE FNHSysStyleId =" & Val(FNHSysStyleId.Properties.Tag.ToString()) & " "

            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Function DoDeleteSource(ByVal dataAdapter As SqlDataAdapter, _
    ByVal dataTable As System.Data.DataTable, r As Integer, ByVal TableIndexx As Integer) As Boolean

        If dataTable.Rows.Count = 0 Then Return True

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
        Try
            If TableIndexx = 0 Then
                dataAdapter = CreateAdapter(HI.Conn.SQLConn.Cnn)
            ElseIf TableIndexx = 1 Then
                dataAdapter = CreateAdapterColor(HI.Conn.SQLConn.Cnn)
            Else
                dataAdapter = CreateAdapterSize(HI.Conn.SQLConn.Cnn)
            End If

            Dim SelectCMD As SqlCommand = New SqlCommand(dataAdapter.SelectCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
            SelectCMD.Parameters.AddWithValue("@FNHSysStyleId", Val(dataTable.Rows(r).Item("FNHSysStyleId").ToString))
            SelectCMD.Parameters.AddWithValue("@FNSeq", dataTable.Rows(r).Item("FNSeq").ToString)

            Dim cnt As Integer
            cnt = SelectCMD.ExecuteScalar
            If cnt = 0 Then
                If dataTable.Rows(r).RowState <> DataRowState.Deleted Then
                    dataTable.Rows(r).Delete()
                End If

                dataTable.AcceptChanges()

                If TableIndexx = 0 Then
                    ogcmatcode.DataSource = dataTable
                    ogcmatcode.Refresh()
                ElseIf TableIndexx = 1 Then
                    ogcstylecolor.DataSource = dataTable
                    ogcstylecolor.Refresh()
                Else
                    ogcstylesize.DataSource = dataTable
                    ogcstylesize.Refresh()
                End If

                Return True
            End If

            Dim DeleteCmd As SqlCommand = New SqlCommand(dataAdapter.DeleteCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)

            Try
                DeleteCmd.Parameters.AddWithValue("@FNHSysStyleId", dataTable.Rows(r).Item("FNHSysStyleId").ToString)
                DeleteCmd.Parameters.AddWithValue("@FNSeq", dataTable.Rows(r).Item("FNSeq").ToString)

                DeleteCmd.CommandType = CommandType.Text
                DeleteCmd.ExecuteNonQuery()
                DeleteCmd.Parameters.Clear()

                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                If dataTable.Rows(r).RowState <> DataRowState.Deleted Then
                    dataTable.Rows(r).Delete()
                End If

                dataTable.AcceptChanges()

                If TableIndexx = 0 Then
                    ogcmatcode.DataSource = dataTable
                    ogcmatcode.Refresh()
                ElseIf TableIndexx = 1 Then
                    ogcstylecolor.DataSource = dataTable
                    ogcstylecolor.Refresh()
                Else
                    ogcstylesize.DataSource = dataTable
                    ogcstylesize.Refresh()
                End If

                Return True

            Catch ex As Exception
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                MessageBox.Show(ex.Message)
                Return False
            End Try
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
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

    Public Function CreateAdapterColor(ByVal connection As SqlConnection) As SqlDataAdapter

        Dim adapter As SqlDataAdapter = New SqlDataAdapter()

        ' Create the SelectCommand. 
        Dim command As SqlCommand = New SqlCommand( _
            "SELECT [FNHSysStyleId],[FNSeq],[FNMerMatSeq],[FNColorWaySeq],[FTRunColor],[FNHSysRawMatColorId], [FNHSysMatColorId], " & _
            "[FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser],[FDUpdDate],[FTUpdTime] FROM " & _
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle_ColorWay] " & _
            "WHERE (FNHSysStyleId = @FNHSysStyleId AND FNSeq = @FNSeq " & _
            "AND FNColorWaySeq = @FNColorWaySeq " & _
            ") " & _
            "OR (FNHSysStyleId = @FNHSysStyleId AND FNSeq = -1)", connection)

        ' Add the parameters for the SelectCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int)
        command.Parameters.Add("@FNSeq", SqlDbType.Int)
        command.Parameters.Add("@FNColorWaySeq", SqlDbType.Int)

        adapter.SelectCommand = command

        ' Create the InsertCommand.
        command = New SqlCommand( _
            "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle_ColorWay] " & _
            "([FNHSysStyleId],[FNSeq],[FNMerMatSeq],[FNColorWaySeq],[FTRunColor],[FNHSysRawMatColorId], [FNHSysMatColorId], " & _
            "[FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser],[FDUpdDate],[FTUpdTime]) " & _
            "VALUES (@FNHSysStyleId, @FNSeq, @FNMerMatSeq, @FNColorWaySeq, @FTRunColor, @FNHSysRawMatColorId, @FNHSysMatColorId, " & _
            "@FTInsUser, @FDInsDate, @FTInsTime, @FTUpdUser, @FDUpdDate, @FTUpdTime)", connection)

        ' Add the parameters for the InsertCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int, 8, "FNHSysStyleId")
        command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        command.Parameters.Add("@FNMerMatSeq", SqlDbType.Decimal, 5, "FNMerMatSeq")
        command.Parameters.Add("@FNColorWaySeq", SqlDbType.Int, 8, "FNColorWaySeq")
        command.Parameters.Add("@FTRunColor", SqlDbType.NChar, 30, "FTRunColor")
        command.Parameters.Add("@FNHSysRawMatColorId", SqlDbType.Int, 8, "FNHSysRawMatColorId")
        command.Parameters.Add("@FNHSysMatColorId", SqlDbType.Int, 8, "FNHSysMatColorId")

        command.Parameters.Add("@FTInsUser", SqlDbType.NChar, 50, "FTInsUser")
        command.Parameters.Add("@FDInsDate", SqlDbType.VarChar, 10, "FDInsDate")
        command.Parameters.Add("@FTInsTime", SqlDbType.VarChar, 8, "FTInsTime")
        command.Parameters.Add("@FTUpdUser", SqlDbType.NChar, 50, "FTUpdUser")
        command.Parameters.Add("@FDUpdDate", SqlDbType.VarChar, 10, "FDUpdDate")
        command.Parameters.Add("@FTUpdTime", SqlDbType.VarChar, 8, "FTUpdTime")

        adapter.InsertCommand = command

        ' Create the UpdateCommand.
        command = New SqlCommand( _
            "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle_ColorWay] SET " & _
            "FNHSysStyleId          = @FNHSysStyleId, " & _
            "FNSeq                  = @FNSeq, " & _
            "FNMerMatSeq            = @FNMerMatSeq, " & _
            "FNColorWaySeq          = @FNColorWaySeq, " & _
            "FTRunColor             = @FTRunColor, " & _
            "FNHSysRawMatColorId    = @FNHSysRawMatColorId, " & _
            "FNHSysMatColorId       = @FNHSysMatColorId, " & _
            "FTUpdUser              = @FTUpdUser, " & _
            "FDUpdDate              = @FDUpdDate, " & _
            "FTUpdTime              = @FTUpdTime " & _
            "WHERE FNHSysStyleId    = @FNHSysStyleId AND FNSeq = @FNSeq AND FNColorWaySeq = @FNColorWaySeq", connection)

        '"FTColorWay             = @FTColorWay, " & _

        ' Add the parameters for the UpdateCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int, 8, "FNHSysStyleId")
        command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        command.Parameters.Add("@FNMerMatSeq", SqlDbType.Decimal, 5, "FNMerMatSeq")
        command.Parameters.Add("@FNColorWaySeq", SqlDbType.Int, 8, "FNColorWaySeq")
        command.Parameters.Add("@FTRunColor", SqlDbType.NChar, 30, "FTRunColor")
        command.Parameters.Add("@FNHSysRawMatColorId", SqlDbType.Int, 8, "FNHSysRawMatColorId")
        command.Parameters.Add("@FNHSysMatColorId", SqlDbType.Int, 8, "FNHSysMatColorId")

        command.Parameters.Add("@FTUpdUser", SqlDbType.NChar, 50, "FTUpdUser")
        command.Parameters.Add("@FDUpdDate", SqlDbType.VarChar, 10, "FDUpdDate")
        command.Parameters.Add("@FTUpdTime", SqlDbType.VarChar, 8, "FTUpdTime")

        Dim parameter As SqlParameter
        parameter = command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int, 8, "FNHSysStyleId")
        parameter = command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        parameter = command.Parameters.Add("@FNColorWaySeq", SqlDbType.Int, 8, "FNColorWaySeq")
        parameter.SourceVersion = DataRowVersion.Original

        adapter.UpdateCommand = command

        ' Create the DeleteCommand.
        command = New SqlCommand( _
            "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle_ColorWay] " & _
            "WHERE FNHSysStyleId = @FNHSysStyleId AND FNSeq = @FNSeq", connection)

        ' Add the parameters for the DeleteCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int, 8, "FNHSysStyleId")
        command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        parameter.SourceVersion = DataRowVersion.Original

        adapter.DeleteCommand = command

        Return adapter
    End Function

    Public Function CreateAdapterSize(ByVal connection As SqlConnection) As SqlDataAdapter

        Dim adapter As SqlDataAdapter = New SqlDataAdapter()

        ' Create the SelectCommand. 
        Dim command As SqlCommand = New SqlCommand( _
            "SELECT [FNHSysStyleId], [FNSeq], [FNMerMatSeq], [FNSieBreakDownSeq], [FTRunSize], [FNHSysRawMatSizeId], [FNHSysMatSizeId], " & _
            "[FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser],[FDUpdDate],[FTUpdTime] FROM " & _
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle_SizeBreakDown] " & _
            "WHERE (FNHSysStyleId = @FNHSysStyleId AND FNSeq = @FNSeq " & _
            "AND FNSieBreakDownSeq = @FNSieBreakDownSeq " & _
            ") " & _
            "OR (FNHSysStyleId = @FNHSysStyleId AND FNSeq = -1)", connection)

        ' Add the parameters for the SelectCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int)
        command.Parameters.Add("@FNSeq", SqlDbType.Int)
        command.Parameters.Add("@FNColorWaySeq", SqlDbType.Int)

        adapter.SelectCommand = command

        ' Create the InsertCommand.
        command = New SqlCommand( _
            "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle_SizeBreakDown] " & _
            "([FNHSysStyleId], [FNSeq], [FNMerMatSeq], [FNSieBreakDownSeq], [FTRunSize], [FNHSysRawMatSizeId], FNHSysMatSizeId, " & _
            "[FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser],[FDUpdDate],[FTUpdTime]) " & _
            "VALUES (@FNHSysStyleId, @FNSeq, @FNMerMatSeq, @FNSieBreakDownSeq, @FTRunSize, @FNHSysRawMatSizeId, @FNHSysMatSizeId, " & _
            "@FTInsUser, @FDInsDate, @FTInsTime, @FTUpdUser, @FDUpdDate, @FTUpdTime)", connection)

        ' Add the parameters for the InsertCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int, 8, "FNHSysStyleId")
        command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        command.Parameters.Add("@FNMerMatSeq", SqlDbType.Decimal, 5, "FNMerMatSeq")
        command.Parameters.Add("@FNSieBreakDownSeq", SqlDbType.Int, 8, "FNSieBreakDownSeq")
        command.Parameters.Add("@FTRunSize", SqlDbType.NChar, 30, "FTRunSize")
        command.Parameters.Add("@FNHSysRawMatSizeId", SqlDbType.Int, 8, "FNHSysRawMatSizeId")
        command.Parameters.Add("@FNHSysMatSizeId", SqlDbType.Int, 8, "FNHSysMatSizeId")

        command.Parameters.Add("@FTInsUser", SqlDbType.NChar, 50, "FTInsUser")
        command.Parameters.Add("@FDInsDate", SqlDbType.VarChar, 10, "FDInsDate")
        command.Parameters.Add("@FTInsTime", SqlDbType.VarChar, 8, "FTInsTime")
        command.Parameters.Add("@FTUpdUser", SqlDbType.NChar, 50, "FTUpdUser")
        command.Parameters.Add("@FDUpdDate", SqlDbType.VarChar, 10, "FDUpdDate")
        command.Parameters.Add("@FTUpdTime", SqlDbType.VarChar, 8, "FTUpdTime")

        adapter.InsertCommand = command

        ' Create the UpdateCommand.
        command = New SqlCommand( _
            "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle_SizeBreakDown] SET " & _
            "FNHSysStyleId          = @FNHSysStyleId, " & _
            "FNSeq                  = @FNSeq, " & _
            "FNMerMatSeq            = @FNMerMatSeq, " & _
            "FNSieBreakDownSeq      = @FNSieBreakDownSeq, " & _
            "FTRunSize              = @FTRunSize, " & _
            "FNHSysRawMatSizeId     = @FNHSysRawMatSizeId, " & _
            "FNHSysMatSizeId        = @FNHSysMatSizeId, " & _
            "FTUpdUser              = @FTUpdUser, " & _
            "FDUpdDate              = @FDUpdDate, " & _
            "FTUpdTime              = @FTUpdTime " & _
            "WHERE FNHSysStyleId    = @FNHSysStyleId AND FNSeq = @FNSeq AND FNSieBreakDownSeq = @FNSieBreakDownSeq", connection)

        ' Add the parameters for the UpdateCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int, 8, "FNHSysStyleId")
        command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        command.Parameters.Add("@FNMerMatSeq", SqlDbType.Decimal, 5, "FNMerMatSeq")
        command.Parameters.Add("@FNSieBreakDownSeq", SqlDbType.Int, 5, "FNSieBreakDownSeq")
        command.Parameters.Add("@FTRunSize", SqlDbType.NChar, 30, "FTRunSize")
        command.Parameters.Add("@FNHSysRawMatSizeId", SqlDbType.Int, 50, "FNHSysRawMatSizeId")
        command.Parameters.Add("@FNHSysMatSizeId", SqlDbType.Int, 8, "FNHSysMatSizeId")

        command.Parameters.Add("@FTUpdUser", SqlDbType.NChar, 50, "FTUpdUser")
        command.Parameters.Add("@FDUpdDate", SqlDbType.VarChar, 10, "FDUpdDate")
        command.Parameters.Add("@FTUpdTime", SqlDbType.VarChar, 8, "FTUpdTime")

        Dim parameter As SqlParameter
        parameter = command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int, 8, "FNHSysStyleId") 'old id
        parameter = command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        parameter = command.Parameters.Add("@FNSieBreakDownSeq", SqlDbType.Int, 8, "FNSieBreakDownSeq")
        parameter.SourceVersion = DataRowVersion.Original

        adapter.UpdateCommand = command

        ' Create the DeleteCommand.
        command = New SqlCommand( _
            "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle_SizeBreakDown] " & _
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
            "ORDER BY O.FNHSysStyleId, C.FNMatColorSeq, B.FTOrderNo, B.FTSizeBreakDown", connection)

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

#Region "Merge Grid cell"

    Private Sub InitialGridMergCell()
        Dim MergGrid1 As GridView = ogcmatcode.Views(0)
        Dim MergGrid2 As GridView = ogcstylecolor.Views(0)
        Dim MergGrid3 As GridView = ogcstylesize.Views(0)

        'Style Detail
        For Each c As GridColumn In MergGrid1.Columns
            If c.AbsoluteIndex < 13 Then
                c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
            Else
                c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End If
        Next

        'Color Way
        For Each c As GridColumn In MergGrid2.Columns
            If c.AbsoluteIndex < 5 Then
                c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
            Else
                c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End If
        Next

        'Size Breakdown
        For Each c As GridColumn In MergGrid3.Columns
            If c.AbsoluteIndex < 5 Then
                c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
            Else
                c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End If
        Next

    End Sub

    Private Sub GridView1_CellMerge(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles GridView1.CellMerge
        Dim view As GridView = TryCast(sender, GridView)
        Try
            Dim ItemCode1 As String = view.GetRowCellValue(e.RowHandle1, "FTMainMatCode").ToString
            Dim ItemCode2 As String = view.GetRowCellValue(e.RowHandle2, "FTMainMatCode").ToString

            If (ItemCode1 = ItemCode2 And e.Column.AbsoluteIndex < 13) Then
                Dim value1 = view.GetRowCellValue(e.RowHandle1, e.Column)
                Dim value2 = view.GetRowCellValue(e.RowHandle2, e.Column)

                e.Merge = (value1 = value2)
                e.Handled = True
                Return
            Else
                e.Merge = False
                e.Handled = True
                Return
            End If

        Catch ex As Exception
            Return
        End Try
    End Sub

    Private Sub GridView2_CellMerge(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles GridView2.CellMerge
        Dim view As GridView = TryCast(sender, GridView)
        Try
            Dim ItemCode1 As String = view.GetRowCellValue(e.RowHandle1, "FTMainMatCode").ToString
            Dim ItemCode2 As String = view.GetRowCellValue(e.RowHandle2, "FTMainMatCode").ToString

            If (ItemCode1 = ItemCode2 And e.Column.AbsoluteIndex < 5) Then
                Dim value1 = view.GetRowCellValue(e.RowHandle1, e.Column)
                Dim value2 = view.GetRowCellValue(e.RowHandle2, e.Column)

                e.Merge = (value1 = value2)
                e.Handled = True
                Return
            Else
                e.Merge = False
                e.Handled = True
                Return
            End If

        Catch ex As Exception
            Return
        End Try
    End Sub

    Private Sub GridView3_CellMerge(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles GridView3.CellMerge
        Dim view As GridView = TryCast(sender, GridView)
        Try
            Dim ItemCode1 As String = view.GetRowCellValue(e.RowHandle1, "FTMainMatCode").ToString
            Dim ItemCode2 As String = view.GetRowCellValue(e.RowHandle2, "FTMainMatCode").ToString

            If (ItemCode1 = ItemCode2 And e.Column.AbsoluteIndex < 5) Then
                Dim value1 = view.GetRowCellValue(e.RowHandle1, e.Column)
                Dim value2 = view.GetRowCellValue(e.RowHandle2, e.Column)

                e.Merge = (value1 = value2)
                e.Handled = True
                Return
            Else
                e.Merge = False
                e.Handled = True
                Return
            End If

        Catch ex As Exception
            Return
        End Try
    End Sub

#End Region

End Class