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
Imports System.Reflection
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base

Public Class wGenerateStyleThread

    Private _StateSelectAll As Boolean = False
    Private _dtpart As New List(Of DataTable)
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
        'Call HI.ST.Lang.InsertLanguage(_CopyStyle)

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

        With RepFTSeasonCode
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
            ' View = New GridView()

            _StateSelectAll = True

            View = ogcmatcode.Views(0)
            View.FocusedRowHandle = 0

            'ogcmatcode.ViewCollection.Add(View)
            'ogcmatcode.MainView = View
            'View.GridControl = ogcmatcode

            View.OptionsView.ShowAutoFilterRow = False
            View.OptionsView.NewItemRowPosition = NewItemRowPosition.None
            View.OptionsNavigation.AutoFocusNewRow = True
            View.OptionsBehavior.AllowAddRows = True
            View.OptionsBehavior.AllowDeleteRows = True
            View.OptionsBehavior.Editable = True
            View.OptionsView.ShowAutoFilterRow = True
            View.BestFitColumns()

            GridView1.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200
            GridView2.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            GridView3.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            GridView1.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            GridView2.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            GridView3.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            GridView1.Columns.ColumnByFieldName("FTComponent").Width = 100

            FTUpdUser.Text = HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)
            FDUpdDate.Text = "??/??/????"
            FTUpdTime.Text = "??:??:??"

            sFNHSysStyleId = ""

            ViewColor = Me.ogcstylecolor.Views(0)
            ViewSize = Me.ogcstylesize.Views(0)

            'With Me.ogvpart
            '    .OptionsView.ShowAutoFilterRow = False
            'End With

            Call TabChange()
            Call CreateMergeEditControl()

            _StateSelectAll = False

        Catch ex As Exception
        End Try
        _StateSelectAll = False
    End Sub
#End Region

#Region "MAIN PROC"

    Private Function CheckOwner() As Boolean

        If (HI.ST.UserInfo.UserName.ToUpper = FTUpdUser.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Or FTUpdUser.Text.ToUpper = "" Then
            Return True
        Else
            HI.MG.ShowMsg.mProcessError(1411200101, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข Style นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If

    End Function

    Private Sub Proc_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysStyleId.EditValueChanged

        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysStyleId_EditValueChanged), New Object() {sender, e})
        Else
            If FNHSysStyleId.Text <> "" Then
                Dim _Str As String = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
                FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
                If FNHSysStyleId.Properties.Tag.ToString <> "" And FNHSysSeasonId.Properties.Tag.ToString <> "" Then
                    Dim _Spls As New HI.TL.SplashScreen("Loading.... ,Please Wait.")
                    Try

                        Call LoadStyleInfo(FNHSysStyleId.Properties.Tag.ToString)
                        ' Call LoadStyleDetail(FNHSysStyleId.Properties.Tag.ToString)
                        Call LoadOrderInfo(FNHSysStyleId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)

                    Catch ex As Exception

                    End Try
                    _Spls.Close()
                Else

                    FNHSysCustId.Text = Nothing
                    FNHSysCustId_None.Text = Nothing
                    'FNHSysSeasonId.Text = Nothing
                    'FNHSysSeasonId_None.Text = Nothing
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
        End If

    End Sub

    Private Sub FNHSysSeasonId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysSeasonId.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysStyleId_EditValueChanged), New Object() {sender, e})
        Else
            If FNHSysSeasonId.Text <> "" Then

                Dim _Str As String = ""

                _Str = "SELECT TOP 1 FNHSysSeasonId "
                _Str &= vbCrLf & " FROM (SELECT FNHSysSeasonId,FTSeasonCode"
                _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason WITH(NOLOCK) WHERE FTStateActive ='1'   "
                _Str &= vbCrLf & "  UNION "
                _Str &= vbCrLf & "  SELECT -1 AS FNHSysSeasonId,'All' AS FTSeasonCode ) AS A"
                _Str &= vbCrLf & "  WHERE FTSeasonCode ='" & HI.UL.ULF.rpQuoted(FNHSysSeasonId.Text) & "' "
                FNHSysSeasonId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
                If FNHSysStyleId.Properties.Tag.ToString <> "" And FNHSysSeasonId.Properties.Tag.ToString <> "" Then

                    Dim _Spls As New HI.TL.SplashScreen("Loading.... ,Please Wait.")
                    Try

                        Call LoadStyleInfo(FNHSysStyleId.Properties.Tag.ToString)
                        Call LoadOrderInfo(FNHSysStyleId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)

                    Catch ex As Exception
                    End Try

                    _Spls.Close()

                Else

                    'FNHSysCustId.Text = Nothing
                    'FNHSysCustId_None.Text = Nothing
                    'FNHSysSeasonId.Text = Nothing
                    'FNHSysSeasonId_None.Text = Nothing
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
        End If

    End Sub

    Private Sub LoadStyleInfo(ByVal _FNHSysStyleId As String, Optional LoadColorAndSize As Boolean = True)

        Call ClearMergeCtrlData()

        Dim _dt As DataTable
        Dim _dtupd As DataTable
        Dim _Str As String = ""
        _Str = "SELECT MS.FNHSysStyleId, MS.FTStyleCode, MS.FTStyleNameTH, MS.FTStyleNameEN, T1.FNHSysCustId, T1.FNHSysSeasonId, T1.FTUpdUser, CONVERT(VARCHAR(10), CONVERT(DATETIME, T1.FDUpdDate, 120), 103) AS FDUpdDate, T1.FTUpdTime"
        _Str &= vbCrLf & "  , T7.FTSeasonCode, T7.FTSeasonNameEN, T8.FTCustCode, T8.FTCustNameEN"
        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS MS WITH(NOLOCK)  "
        _Str &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle AS T1 WITH(NOLOCK) ON MS.FNHSysStyleId = T1.FNHSysStyleId"
        _Str &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason T7 WITH(NOLOCK) ON T1.FNHSysSeasonId = T7.FNHSysSeasonId"
        _Str &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer T8 WITH(NOLOCK) ON T1.FNHSysCustId = T8.FNHSysCustId"
        _Str &= vbCrLf & " WHERE(MS.FNHSysStyleId  =" & Val(_FNHSysStyleId) & ")"
        _Str &= vbCrLf & " ORDER BY MS.FNHSysStyleId"
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

        If _dt.Rows.Count > 0 Then

            For Each R As DataRow In _dt.Rows
                'FNHSysStyleId_None.Text = R!FTStyleNameTH.ToString
                FTStyleNameTH.Text = R!FTStyleNameTH.ToString
                FTStyleNameEN.Text = R!FTStyleNameEN.ToString
                FNHSysCustId.Text = R!FTCustCode.ToString
                FNHSysCustId_None.Text = R!FTCustNameEN.ToString
                'FNHSysSeasonId.Text = R!FTSeasonCode.ToString
                'FNHSysSeasonId_None.Text = R!FTSeasonNameEN.ToString


            Next

            _Str = "SELECT TOP 1"
            _Str &= vbCrLf & "  	ISNULL(FTUpdUser,FTInsUser) AS FTUpdUser"
            _Str &= vbCrLf & "  ,ISNULL(FDUpdDate,FDInsDate) AS FDUpdDate"
            _Str &= vbCrLf & "  ,ISNULL(FTUpdTime,FTInsTime) AS FTUpdTime"
            _Str &= vbCrLf & "  ,FNHSysStyleId, FNHSysSeasonId"
            _Str &= vbCrLf & "     FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_Mat AS X WITH(NOLOCK)"
            _Str &= vbCrLf & " WHERE (X.FNHSysStyleId  =" & Val(_FNHSysStyleId) & ")"
            _Str &= vbCrLf & " AND (X.FNHSysSeasonId  =" & Val(FNHSysSeasonId.Properties.Tag.ToString) & ")"
            _Str &= vbCrLf & "   ORDER BY ISNULL(FDUpdDate,FDInsDate) DESC,ISNULL(FTUpdTime,FTInsTime) DESC"
            _dtupd = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_PROD)

            If _dtupd.Rows.Count > 0 Then

                For Each R As DataRow In _dtupd.Rows
                    FTUpdUser.Text = R!FTUpdUser.ToString
                    FDUpdDate.Text = R!FDUpdDate.ToString
                    FTUpdTime.Text = R!FTUpdTime.ToString
                    Exit For
                Next

            Else
                FTUpdUser.Text = ""
                FDUpdDate.Text = ""
                FTUpdTime.Text = ""
            End If

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
        If (_FNHSysStyleId <> "") Then

            Call LoadStyleDetail(_FNHSysStyleId, FNHSysSeasonId.Properties.Tag.ToString)
            If LoadColorAndSize Then
                Call LoadColorwaySize(_FNHSysStyleId, FNHSysSeasonId.Properties.Tag.ToString)
            End If

        End If

    End Sub

    Private Sub LoadStyleDetail(ByVal FNHSysStyleId As String, _FNHSysSeasonId As Integer)
        'If FNHSysStyleId = "" Then Return
        Dim _Str As String = ""
        _StateSelectAll = True

        _Str = "SELECT '0' AS FTSelect,T2.FNHSysStyleId, T2.FNSeq, T2.FNMerMatSeq, T2.FNHSysMerMatId, T3.FTMainMatCode, T2.FNHSysUnitId, T4.FTUnitCode , T2.FTStateNominate, T2.FNHSysSuplId, T5.FTSuplCode, T2.FNHSysCurId," & Integer.Parse(Val(FNHSysSeasonId.Properties.Tag.ToString)) & " AS FNHSysSeasonId,  "
        _Str &= vbCrLf & " T6.FTCurCode, T2.FNPrice, T2.FNPart"

        _Str &= vbCrLf & ", T2.FTOrderNo AS FTOrderID, T2.FTSubOrderNo AS FTSubOrderID "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            _Str &= vbCrLf & ",  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].FN_GetPosipartName(1, T2.FTPart, T2.FTPositionPartId) AS FTPositionPartName"
            _Str &= vbCrLf & ", T3.FTMainMatNameTH AS FNHSysMerMatId_None, "
            _Str &= vbCrLf & "CASE WHEN T2.FTOrderNo = '-1' THEN 'ทั้งหมด' ELSE T2.FTOrderNo END AS FTOrderNo, "
            _Str &= vbCrLf & "CASE WHEN T2.FTSubOrderNo = '-1' THEN 'ทั้งหมด' ELSE T2.FTSubOrderNo END AS FTSubOrderNo, "

        Else

            _Str &= vbCrLf & ",  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].FN_GetPosipartName(0, T2.FTPart, T2.FTPositionPartId) AS FTPositionPartName"
            _Str &= vbCrLf & ", T3.FTMainMatNameEN AS FNHSysMerMatId_None, "
            _Str &= vbCrLf & "CASE WHEN T2.FTOrderNo = '-1' THEN 'ALL' ELSE T2.FTOrderNo END AS FTOrderNo, "
            _Str &= vbCrLf & "CASE WHEN T2.FTSubOrderNo = '-1' THEN 'ALL' ELSE T2.FTSubOrderNo END AS FTSubOrderNo, "

        End If

        _Str &= vbCrLf & "T2.FNConSmp, T2.FNConSmpPlus, "
        _Str &= vbCrLf & "CASE WHEN T2.FTStateCombination IS NOT NULL THEN T2.FTStateCombination ELSE "
        _Str &= " CASE WHEN MG.FTMatGrpCode = 'F' THEN '1' ELSE '0' END END AS FTStateCombination, "
        _Str &= vbCrLf & " T2.FTStateActive, T2.FTStateMainMaterial"
        _Str &= vbCrLf & " ,T2.FTPositionPartId, T2.FTPart"
        _Str &= vbCrLf & " ,T2.FTComponent"
        _Str &= vbCrLf & " ,ISNULL(T2.FTStateFree,'0') AS FTStateFree,'1' AS FTStateDataDetail"
        _Str &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTStyle] AS T1 WITH(NOLOCK) INNER JOIN"
        _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TMERTThreadStyle_Mat] AS T2 WITH(NOLOCK) ON T1.FNHSysStyleId = T2.FNHSysStyleId LEFT OUTER JOIN"
        _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS T3 WITH(NOLOCK) ON T2.FNHSysMerMatId = T3.FNHSysMainMatId LEFT OUTER JOIN"
        _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS T4 WITH(NOLOCK) ON T2.FNHSysUnitId = T4.FNHSysUnitId LEFT OUTER JOIN"
        _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS T5 WITH(NOLOCK) ON T2.FNHSysSuplId = T5.FNHSysSuplId LEFT OUTER JOIN"
        _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS T6 WITH(NOLOCK) ON T2.FNHSysCurId = T6.FNHSysCurId LEFT OUTER JOIN"
        _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMMatGrp MG WITH (NOLOCK) ON T3.FNHSysMatGrpId = MG.FNHSysMatGrpId"
        _Str &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason SCS WITH (NOLOCK) ON T2.FNHSysSeasonId=SCS.FNHSysSeasonId "
        _Str &= vbCrLf & " WHERE (T1.FNHSysStyleId =" & Val(FNHSysStyleId) & ")"
        _Str &= vbCrLf & " AND (T2.FNHSysSeasonId =" & Val(_FNHSysSeasonId) & ")"
        _Str &= vbCrLf & " ORDER BY T1.FNHSysStyleId, T2.FNMerMatSeq, T2.FNPart"

        'If HI.ST.Lang.Language = ST.Lang.Lang.EN Then
        '    _Str &= vbCrLf & "  ,"
        'Else
        '    _Str &= vbCrLf & "  ,"
        'End If

        ' oleDbDataAdapter2 = New SqlClient.SqlDataAdapter(_Str, HI.Conn.SQLConn._ConnString)
        dtStyleDetail = New DataTable()
        ' oleDbDataAdapter2.Fill(dtStyleDetail)
        dtStyleDetail = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)
        Me.ogcmatcode.DataSource = dtStyleDetail  'HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)
        Me.ogcmatcode.Refresh()


        Dim View As GridView = Me.ogcmatcode.Views(0)
        InitialGridMergCell()
        View.BestFitColumns()
        GridView1.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200
        GridView2.Columns.ColumnByFieldName("FTMainMatName").Width = 200
        GridView3.Columns.ColumnByFieldName("FTMainMatName").Width = 200
        GridView1.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
        GridView2.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
        GridView3.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
        GridView1.Columns.ColumnByFieldName("FTComponent").Width = 100


        _StateSelectAll = False


    End Sub

    Private Sub LoadOrderInfo(ByVal _FNHSysStyleId As String, _FNHSysSeasonId As String)
        Dim _Str As String = ""
        If _FNHSysStyleId = "" Then Return

        _Str = "SELECT '0' AS FNSelect"
        _Str &= vbCrLf & "   , A.FTOrderNo"
        _Str &= vbCrLf & "  ,SEAS.FTSeasonCode "
        _Str &= vbCrLf & "   , CASE WHEN ISDATE(A.FDOrderDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, A.FDOrderDate), 103) "
        _Str &= vbCrLf & "         ELSE '' END AS FDOrderDate, ISNULL"
        _Str &= vbCrLf & "                  ((SELECT     CASE WHEN ISDATE(L1.FDShipDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, L1.FDShipDate), 103) ELSE '' END AS FDShipDate"
        _Str &= vbCrLf & "                      FROM         (SELECT     X.FTOrderNo, MIN(Y.FDShipDate) AS FDShipDate"
        _Str &= vbCrLf & "                                             FROM          HITECH_MERCHAN.dbo.TMERTOrder AS X INNER JOIN"
        _Str &= vbCrLf & "                                                                    HITECH_MERCHAN.dbo.TMERTOrderSub AS Y ON X.FTOrderNo = Y.FTOrderNo"
        _Str &= vbCrLf & "                                             GROUP BY X.FTOrderNo) AS L1"
        _Str &= vbCrLf & "                      WHERE     (FTOrderNo = A.FTOrderNo)), '') AS FDShipDate, A.FNHSysCustId, C.FTCustCode, C.FTCustNameEN AS FTCustName"
        _Str &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) LEFT OUTER JOIN"
        _Str &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer AS C WITH(NOLOCK)  ON A.FNHSysCustId = C.FNHSysCustId"
        _Str &= vbCrLf & "    INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTStyle  AS MST WITH(NOLOCK) ON A.FNHSysStyleId  = MST.FNHSysStyleId  "
        _Str &= vbCrLf & "  	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason  AS SEAS WITH(NOLOCK)  ON "
        _Str &= vbCrLf & "  	CASE WHEN ISNULL(A.FNHSysSeasonId,0) <=0 THEN ISNULL(MST.FNHSysSeasonId,0) ELSE ISNULL(A.FNHSysSeasonId,0)  END  = SEAS.FNHSysSeasonId"
        _Str &= vbCrLf & "WHERE     (A.FNHSysStyleId = " & Val(_FNHSysStyleId) & ") "

        If _FNHSysSeasonId > 0 Then
            _Str &= vbCrLf & "   AND CASE WHEN ISNULL(A.FNHSysSeasonId,0) <=0 THEN ISNULL(MST.FNHSysSeasonId,0) ELSE ISNULL(A.FNHSysSeasonId,0) END = " & Val(_FNHSysSeasonId) & " "
        End If

        _Str &= vbCrLf & "   ORDER BY  A.FTOrderNo"

        dtStyleDetail = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

        Me.GridOrderList.DataSource = dtStyleDetail

        Dim view As GridView
        view = GridOrderList.Views(0)
        view.OptionsView.ShowAutoFilterRow = False

        Me.GridOrderList = view.GridControl
        Me.GridOrderList.Refresh()

    End Sub

    Private Sub LoadColorwaySize(ByVal FNHSysStyleId As String, _FNHSysSeasonId As String)
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
                If Idx >= 13 Then
                    Dim Col As GridColumn = InitGrid.Columns(Idx)
                    View.Columns(Col.Name).Dispose()
                End If
            Next

        Catch ex As Exception
        End Try

        Return InitGrid
    End Function

    Private Sub ocmcopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmcopy.Click

        'If Me.FNHSysStyleId.Text <> "" Then
        '    If "" & Me.FNHSysStyleId.Properties.Tag.ToString <> "" Then
        '        If FNHSysSeasonId.Text <> "" Then

        '            If FNHSysSeasonId.Properties.Tag.ToString <> "" Then
        '                Call HI.ST.Lang.SP_SETxLanguage(_CopyStyle)

        '                With _CopyStyle
        '                    .FNHSysStyleId.Text = Me.FNHSysStyleId.Text
        '                    .FNHSysSeasonIdS.Text = FNHSysSeasonId.Text.Trim
        '                    .FNHSysSeasonIdS_None.Text = FNHSysSeasonId_None.Text
        '                    .FNHSysSeasonIdS.Properties.Tag = FNHSysSeasonId.Properties.Tag.ToString
        '                    .FNHSysStyleId2.Text = ""
        '                    .FNHSysSeasonId.Text = ""
        '                    .ProcComplete = False
        '                    .ShowDialog()

        '                    If (.ProcComplete) Then
        '                        Me.otb.SelectedTabPage = otpmatcode
        '                        ''Call ocmsave_Click(ocmsave, e)
        '                    End If

        '                End With

        '            End If

        '        End If

        '    Else
        '        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysStyleId_lbl.Text)
        '        FNHSysStyleId.Focus()
        '    End If
        'Else
        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysStyleId_lbl.Text)
        '    FNHSysStyleId.Focus()
        'End If
    End Sub

    Private Sub ocmbomaddnew_Click(sender As System.Object, e As System.EventArgs) Handles ocmbomaddnew.Click

        If CheckOwner() = False Then
            Exit Sub
        End If

        If FNHSysStyleId.Properties.Tag.ToString = "" Or FNHSysSeasonId.Properties.Tag.ToString = "" Then Return
        CType(ogcmatcode.DataSource, DataTable).AcceptChanges()
        InitNewRow(CType(ogcmatcode.DataSource, DataTable), TabIndexs.StyleDetail)
    End Sub

    Private Sub ocmbominsertrow_Click(sender As System.Object, e As System.EventArgs) Handles ocmbominsertrow.Click
        If CheckOwner() = False Then
            Exit Sub
        End If
        Dim crRow As Double, nxRow As String, nwRow As String
        Dim RowCount As Integer = 0
        View = Me.ogcmatcode.Views(0)
        If FNHSysStyleId.Properties.Tag.ToString = "" Or FNHSysSeasonId.Properties.Tag.ToString = "" Then Return

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

        ' Dim MaxSeq As Double = Val(FNHSysSeasonId.Properties.Tag.ToString()) + 1.0

        Dim MaxSeq As Double = GetDataRunMaxSeq(FNHSysStyleId.Properties.Tag.ToString()) + 1
        If MaxSeq <= 0 Then MaxSeq = 1



        Dim LastSeq As Double = 0
        CType(ogcmatcode.DataSource, DataTable).AcceptChanges()
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
        dr.Item("FNHSysMerMatId") = 0 ' Val(FNHSysStyleId.Properties.Tag.ToString)
        dr.Item("FNPart") = "1"
        ' dr.Item("FNHSysSeasonId") = "-1"
        dr.Item("FTOrderID") = "-1"
        dr.Item("FTSubOrderID") = "-1"
        dr.Item("FTMainMatCode") = ""
        dr.Item("FNHSysMerMatId_None") = ""

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            ' dr.Item("FTSeasonCode") = "ALL"
            dr.Item("FTOrderNo") = "ทั้งหมด"
            dr.Item("FTSubOrderNo") = "ทั้งหมด"
        Else
            ' dr.Item("FTSeasonCode") = "ALL"
            dr.Item("FTOrderNo") = "ALL"
            dr.Item("FTSubOrderNo") = "ALL"
        End If

        dr.Item("FNHSysSeasonId") = Integer.Parse(Val(FNHSysSeasonId.Properties.Tag.ToString))

        dr.Item("FTStateNominate") = "1"
        dr.Item("FTStateCombination") = "0"
        dr.Item("FTStateFree") = "0"
        dr.Item("FTStateActive") = "1"
        dr.Item("FTStateMainMaterial") = "0"
        dr.Item("FTStateDataDetail") = "0"

        dt.Rows.InsertAt(dr, RowsIndex + 1)

        'Call DoGrouping()

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
                    CType(ogcmatcode.DataSource, DataTable).AcceptChanges()
                    InitNewRow(CType(ogcmatcode.DataSource, DataTable), TabIndexs.StyleDetail)
                End If
            End If

        End If

        If e.KeyCode = Keys.Down Then
            If SelectedRow = vw1.RowCount - 1 Then
                CType(ogcmatcode.DataSource, DataTable).AcceptChanges()
                InitNewRow(CType(ogcmatcode.DataSource, DataTable), TabIndexs.StyleDetail)
            End If
        End If

    End Sub


    Private Sub ClearMergeCtrlData()

        Try
            ogcmatcode.Controls.Remove(m_mergedCellEditorSupl)
        Catch ex As Exception
        End Try

        Try
            ogcmatcode.Controls.Remove(m_mergedCellEditorMainMat)
        Catch ex As Exception
        End Try

        Try
            ogcmatcode.Controls.Remove(m_mergedCellEditorUnit)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclearclsr.Click

        Call ClearMergeCtrlData()

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

            GridView1.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200
            GridView2.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            GridView3.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            GridView1.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            GridView2.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            GridView3.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            GridView1.Columns.ColumnByFieldName("FTComponent").Width = 100

            HI.TL.HandlerControl.ClearControl(Me)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub ocmrefresh_Click(sender As System.Object, e As System.EventArgs) Handles ocmrefresh.Click
        Dim _Spls As New HI.TL.SplashScreen("Loading.... ,Please Wait.")
        Try
            Call ClearMergeCtrlData()

            Me.ogcmatcode.DataSource = Nothing
            Me.ogcstylecolor.DataSource = Nothing
            Me.ogcstylesize.DataSource = Nothing

            If FNHSysStyleId.Text <> "" Then
                Dim _Str As String = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
                FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")

                If (FNHSysStyleId.Properties.Tag.ToString <> "") Then

                    Call LoadStyleDetail(FNHSysStyleId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
                    Call LoadColorwaySize(FNHSysStyleId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)

                End If

                Call LoadOrderInfo(FNHSysStyleId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)

            End If

        Catch ex As Exception
        End Try

        _Spls.Close()

    End Sub

    Private Function GetDataRunMaxSeq(_FNHSysStyleId As Integer) As Integer
        Dim _MaxSeq As Integer = 0
        Dim _Qry As String = ""

        _Qry = " SELECT TOP 1 FNSeq"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_Mat AS X WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE (FNHSysStyleId = " & Val(_FNHSysStyleId) & ")"
        _Qry &= vbCrLf & " ORDER BY FNSeq DESC"

        _MaxSeq = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "0"))

        Return _MaxSeq
    End Function

    Private Sub InitNewRow(ByVal dataTable As System.Data.DataTable, ByVal TableIndexx As Integer)
        Try


            If dataTable.Select("FTMainMatCode=''").Length > 0 Then
                Exit Sub
            End If

            Dim dr As DataRow
            dtStyleDetail = dataTable

            dr = dtStyleDetail.NewRow()
            Dim _RunSeq As Integer = 0

            _RunSeq = GetDataRunMaxSeq(FNHSysStyleId.Properties.Tag.ToString()) 'Val(FNHSysSeasonId.Properties.Tag.ToString())

            'If _RunSeq <= 0 Then
            '    _RunSeq = 104440000
            'End If

            Dim MaxSeq As Double = _RunSeq + 1.0
            If MaxSeq <= 0 Then MaxSeq = 1

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
                dr.Item("FNHSysMerMatId") = 0 ' Val(FNHSysStyleId.Properties.Tag.ToString)
                dr.Item("FNPart") = "1"
                ' dr.Item("FNHSysSeasonId") = "-1"
                dr.Item("FTOrderID") = "-1"
                dr.Item("FTSubOrderID") = "-1"
                dr.Item("FTMainMatCode") = ""
                dr.Item("FNHSysMerMatId_None") = ""

                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    ' dr.Item("FTSeasonCode") = "ALL"
                    dr.Item("FTOrderNo") = "ทั้งหมด"
                    dr.Item("FTSubOrderNo") = "ทั้งหมด"
                Else
                    ' dr.Item("FTSeasonCode") = "ALL"
                    dr.Item("FTOrderNo") = "ALL"
                    dr.Item("FTSubOrderNo") = "ALL"
                End If

                dr.Item("FNHSysSeasonId") = Integer.Parse(Val(FNHSysSeasonId.Properties.Tag.ToString))

                dr.Item("FNConSmp") = 0
                dr.Item("FNConSmpPlus") = 0
                dr.Item("FTStateNominate") = "1"
                dr.Item("FTStateCombination") = "0"
                dr.Item("FTStateFree") = "0"
                dr.Item("FTStateActive") = "1"
                dr.Item("FTStateMainMaterial") = "0"
                dr.Item("FTStateDataDetail") = "0"

                dtStyleDetail.Rows.Add(dr)

                ogcmatcode.DataSource = dtStyleDetail
                ogcmatcode.Refresh()

            ElseIf TableIndexx = 1 Then

                dr.Item("FNHSysStyleId") = Val(FNHSysStyleId.Properties.Tag.ToString)
                dr.Item("FNSeq") = MaxSeq
                dr.Item("FNColorWaySeq") = MaxSeq
                dr.Item("FTRunColor") = "1"
                dtStyleDetail.Rows.Add(dr)

                ogcstylecolor.DataSource = dtStyleDetail
                ogcstylecolor.Refresh()

            Else

                dr.Item("FNHSysStyleId") = Val(FNHSysStyleId.Properties.Tag.ToString)
                dr.Item("FNSeq") = MaxSeq
                dr.Item("FNSieBreakDownSeq") = MaxSeq
                dr.Item("FTRunSize") = "1"
                dtStyleDetail.Rows.Add(dr)

                ogcstylesize.DataSource = dtStyleDetail
                ogcstylesize.Refresh()

            End If

            'Call DoGrouping()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ocmbomdeleterow_Click(sender As System.Object, e As System.EventArgs) Handles ocmbomdeleterow.Click

        If CheckOwner() = False Then
            Exit Sub
        End If
        Dim _dtmat As DataTable
        With CType(Me.ogcmatcode.DataSource, DataTable)
            .AcceptChanges()
            _dtmat = .Copy
        End With

        With GridView1

            Dim _Seq As String = "" '"" & .GetRowCellValue(RowsIndex, "FNMerMatSeq").ToString
            Dim _ItemCode As String = "" '"" & .GetRowCellValue(RowsIndex, "FTMainMatCode").ToString
            Dim _Part As String = "" '"" & .GetRowCellValue(RowsIndex, "FNPart").ToString
            Dim _FTOrderNo As String = "" '"" & .GetRowCellValue(RowsIndex, "FTOrderNo").ToString
            Dim _DataFNSeq As String = "" '"" & .GetRowCellValue(RowsIndex, "FNSeq").ToString
            Dim _Msg As String = "" '" Item Code : " & _ItemCode & "     Sequence No : " & _Seq & "     Part No : " & _Part & " "
            Dim _Qry As String = "" ' ""
            Dim _FoundMRp As Boolean = False
            _Msg = ""

            _Seq = ""
            _ItemCode = ""
            _Part = ""
            _FTOrderNo = ""
            _DataFNSeq = ""

            If _dtmat.Select("FTSelect='1'").Length > 0 Then
                _dtmat.BeginInit()

                For Each R As DataRow In _dtmat.Select("FTSelect='1'")
                    _Seq = R!FNMerMatSeq.ToString
                    _ItemCode = R!FTMainMatCode.ToString
                    _Part = R!FNPart.ToString
                    _FTOrderNo = R!FTOrderNo.ToString
                    _DataFNSeq = R!FNSeq.ToString


                    If _Msg = "" Then
                        _Msg = " Item Code : " & _ItemCode & "     Sequence No : " & _Seq & "     Part No : " & _Part & " "
                    Else
                        _Msg &= vbCrLf & " Item Code : " & _ItemCode & "     Sequence No : " & _Seq & "     Part No : " & _Part & " "
                    End If
                    'End If


                Next
                _dtmat.EndInit()


                If _Msg <> "" Then
                    If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, _Msg) = True Then

                        Dim Spls As New HI.TL.SplashScreen("Deleting...,Please Wait.")
                        Try
                            CType(ogcmatcode.DataSource, DataTable).AcceptChanges()
                            Dim dt As DataTable = CType(ogcmatcode.DataSource, DataTable)
                            Dim ItemID As Long = 0 'Val(.GetRowCellValue(RowsIndex, "FNHSysMerMatId").ToString)

                            Dim CurrentPart As Integer = 1 'Integer.Parse(Val(.GetRowCellValue(RowsIndex, "FNPart").ToString))

                            'For Each r As DataRow In dt.Select("FNHSysMerMatId=" & ItemID & "", "FNPart")
                            '    If Integer.Parse(Val(r.Item("FNPart"))) = CurrentPart Then

                            '    Else
                            '        If Integer.Parse(Val(r.Item("FNPart"))) > CurrentPart Then
                            '            r.Item("FNPart") = Integer.Parse(Val(r.Item("FNPart"))) - 1
                            '        End If
                            '    End If

                            'Next

                            'Dim NewPart As Integer = CurrentPart
                            'For Each r As DataRow In dt.Select("FNHSysMerMatId=" & ItemID & " AND FNPart>" & CurrentPart & "", "FNPart")
                            '    r.Item("FNPart") = NewPart
                            '    NewPart = NewPart + 1
                            'Next

                            For Each R As DataRow In _dtmat.Select("FTSelect='1'", "FNSeq")
                                _Seq = R!FNMerMatSeq.ToString
                                _ItemCode = R!FTMainMatCode.ToString
                                _Part = R!FNPart.ToString
                                _FTOrderNo = R!FTOrderNo.ToString
                                _DataFNSeq = R!FNSeq.ToString
                                ItemID = Val(R!FNHSysMerMatId.ToString())

                                _Qry = " DELETE FROM  A "
                                _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_Mat AS A"
                                _Qry &= vbCrLf & "  WHERE   A.FNHSysStyleId=" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & " "
                                _Qry &= vbCrLf & " AND A.FNMerMatSeq=" & Val(_Seq) & ""
                                _Qry &= vbCrLf & " AND A.FNHSysMerMatId=" & ItemID & ""
                                _Qry &= vbCrLf & " AND A.FNSeq=" & Val(_DataFNSeq) & ""
                                _Qry &= vbCrLf & " AND A.FNHSysSeasonId=" & Val(FNHSysSeasonId.Properties.Tag.ToString) & ""
                                _Qry &= vbCrLf & " UPDATE AA SET FNPart = FNPart -1 "
                                _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_Mat AS AA"
                                _Qry &= vbCrLf & "  WHERE   AA.FNHSysStyleId=" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & " "
                                _Qry &= vbCrLf & " AND AA.FNMerMatSeq=" & Val(_Seq) & ""
                                _Qry &= vbCrLf & " AND AA.FNHSysMerMatId=" & ItemID & ""
                                _Qry &= vbCrLf & " AND AA.FNSeq>" & Val(_DataFNSeq) & ""
                                _Qry &= vbCrLf & " AND AA.FNHSysSeasonId=" & Val(FNHSysSeasonId.Properties.Tag.ToString) & " AND FNPart>1"
                                _Qry &= vbCrLf & " AND (FNPart -1) NOT IN (SELECT  FNPart  "
                                _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_Mat AS AA"
                                _Qry &= vbCrLf & "  WHERE   AA.FNHSysStyleId=" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & " "
                                _Qry &= vbCrLf & " AND AA.FNMerMatSeq=" & Val(_Seq) & ""
                                _Qry &= vbCrLf & " AND AA.FNHSysMerMatId=" & ItemID & ""
                                _Qry &= vbCrLf & " AND AA.FNHSysSeasonId=" & Val(FNHSysSeasonId.Properties.Tag.ToString) & "  )"

                                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, _Qry & " (Style " & FNHSysStyleId.Text & " Season " & FNHSysSeasonId.Text & "  Item " & _ItemCode & ") ")

                            Next
                            'If DoDeleteSource(oleDbDataAdapter2, CType(ogcmatcode.DataSource, DataTable), RowsIndex, TabIndexs.StyleDetail) = False Then Return
                            'DoDeleteSource(oleDbDataAdapter2, CType(ogcstylecolor.DataSource, DataTable), RowsIndex, TabIndexs.Colorway)
                            'DoDeleteSource(oleDbDataAdapter2, CType(ogcstylesize.DataSource, DataTable), RowsIndex, TabIndexs.SizeBreakdown)

                            'If DoDeleteSource(oleDbDataAdapter2, CType(ogcmatcode.DataSource, DataTable), Double.Parse(Val(_DataFNSeq)), TabIndexs.StyleDetail) = False Then
                            '    Spls.Close()
                            '    Return
                            'End If

                            'DoDeleteSource(oleDbDataAdapter2, CType(ogcstylecolor.DataSource, DataTable), Double.Parse(Val(_DataFNSeq)), TabIndexs.Colorway)
                            'DoDeleteSource(oleDbDataAdapter2, CType(ogcstylesize.DataSource, DataTable), Double.Parse(Val(_DataFNSeq)), TabIndexs.SizeBreakdown)

                            UpdateDatasourceDelete()

                            Call LoadStyleDetail(FNHSysStyleId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
                            Call LoadColorwaySize(FNHSysStyleId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)

                            CType(ogcmatcode.DataSource, DataTable).AcceptChanges()
                            ' Dim View1 As GridView = Me.ogcmatcode.Views(0)
                            'Dim View2 As GridView = Me.ogcstylecolor.Views(0)
                            'Dim View3 As GridView = Me.ogcstylesize.Views(0)

                            'View1.SelectRow(RowsIndex - 1)
                            If RowsIndex >= .RowCount Then RowsIndex = .RowCount - 1
                            .FocusedRowHandle = RowsIndex ' 1
                            'ogcmatcode = View1.GridControl
                            'ogcmatcode.Refresh()

                        Catch ex As Exception

                        End Try

                        Spls.Close()
                    End If
                Else

                    If _FoundMRp = True Then
                        HI.MG.ShowMsg.mInfo("พบข้อมูลการ Generate MRP แล้วไม่สามารถทำการลบได้ !!!", 1408260001, Me.Text, , MessageBoxIcon.Warning)
                    End If
                End If


            Else
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกรายการที่ต้องการทำการลบ !!!", 1603130542, Me.Text, , MessageBoxIcon.Warning)
                Exit Sub
            End If


            'Delete row of Style Detail.

        End With

    End Sub

    Private Sub ocmdeletecolorway_Click(sender As System.Object, e As System.EventArgs)
        If FNHSysStyleId.Properties.Tag.ToString = "" Or FNHSysSeasonId.Properties.Tag.ToString = "" Then Return
        If CheckOwner() = False Then
            Exit Sub
        End If

        With Me.GridView2
            If .FocusedRowHandle < 0 Then Exit Sub
            If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then

                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบ Colorway ใช่หรือไม่ ?", 1406030001, .FocusedColumn.Caption) Then

                    Dim Col1 As String = .FocusedColumn.FieldName.ToString
                    Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                    Dim Col3 As String = "FTRawMatColorNameEN" & .FocusedColumn.FieldName.ToString.Replace("FTRawMatColorCode", "")
                    Dim Col4 As String = "FTRawMatColorNameTH" & .FocusedColumn.FieldName.ToString.Replace("FTRawMatColorCode", "")

                    Try
                        GridView2.Columns.Remove(GridView2.Columns.ColumnByFieldName(Col1))
                        CType(Me.ogcstylecolor.DataSource, DataTable).Columns.Remove(Col1)
                    Catch ex As Exception
                    End Try

                    Try
                        GridView2.Columns.Remove(GridView2.Columns.ColumnByFieldName(Col2))
                        CType(Me.ogcstylecolor.DataSource, DataTable).Columns.Remove(Col2)
                    Catch ex As Exception
                    End Try

                    Try
                        GridView2.Columns.Remove(GridView2.Columns.ColumnByFieldName(Col3))
                        CType(Me.ogcstylecolor.DataSource, DataTable).Columns.Remove(Col3)
                    Catch ex As Exception
                    End Try

                    Try
                        GridView2.Columns.Remove(GridView2.Columns.ColumnByFieldName(Col4))
                        CType(Me.ogcstylecolor.DataSource, DataTable).Columns.Remove(Col4)
                    Catch ex As Exception
                    End Try

                    CType(Me.ogcstylecolor.DataSource, DataTable).AcceptChanges()

                    HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, " DELETE Colorway " & .FocusedColumn.Caption & " (Style " & FNHSysStyleId.Text & " Season " & FNHSysSeasonId.Text & ") ")

                End If
            End If

        End With
    End Sub

    Private Sub ocmbomimportfromorder_Click(sender As System.Object, e As System.EventArgs)
        If FNHSysStyleId.Properties.Tag.ToString = "" Or FNHSysSeasonId.Properties.Tag.ToString = "" Then Return
        If CheckOwner() = False Then
            Exit Sub
        End If
        Call ImportColor()
        Call ImportSize()
    End Sub

    Private Sub ImportColor()

        Dim dataAdapter As SqlDataAdapter
        Dim dt As DataTable = New DataTable()

        Dim dc As DataColumn
        Dim dc1 As DataColumn
        Dim dc3 As DataColumn
        Dim dc4 As DataColumn

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Try
            dataAdapter = CreateAdapterImportColor(HI.Conn.SQLConn.Cnn)

            Dim SelectCmd As SqlCommand = New SqlCommand(dataAdapter.SelectCommand.CommandText, HI.Conn.SQLConn.Cnn)
            SelectCmd.Parameters.AddWithValue("@FNHSysStyleId", Val(FNHSysStyleId.Properties.Tag.ToString))
            SelectCmd.Parameters.AddWithValue("@FNHSysSeasonId", Val(FNHSysSeasonId.Properties.Tag.ToString))
            SelectCmd.ExecuteNonQuery()

            dataAdapter.SelectCommand = SelectCmd
            dataAdapter.Fill(dt)

            Dim StrSql As String = ""

            'Dim sqlCmd As New SqlCommand
            'sqlCmd.Connection = HI.Conn.SQLConn.Cnn
            'sqlCmd.CommandType = CommandType.StoredProcedure
            'sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_STYLE_COLORWAY]"
            'sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", Val(FNHSysStyleId.Properties.Tag.ToString))
            'sqlCmd.Parameters.AddWithValue("@LANGID", HI.ST.Lang.Language.ToString())

            'oleDbDataAdapter2 = New SqlClient.SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
            'oleDbDataAdapter2.SelectCommand = sqlCmd

            'dtStyleDetail = New DataTable()
            'oleDbDataAdapter2.Fill(dtStyleDetail)

            StrSql = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_STYLE_COLORWAY_NEW_BOMSHEET] " & Val(FNHSysStyleId.Properties.Tag.ToString) & "," & Val(FNHSysSeasonId.Properties.Tag.ToString) & ",'" & HI.ST.Lang.Language.ToString() & "' "
            dtStyleDetail = HI.Conn.SQLConn.GetDataTable(StrSql, Conn.DB.DataBaseName.DB_MERCHAN)

            '' Initial data to dynamic column
            Dim InitDt As DataTable = New DataTable()

            InitDt = dtStyleDetail.Clone()
            'For Each c As DataColumn In dtStyleDetail.Columns
            '    InitDt.Columns.Add(c.ColumnName, c.DataType)
            'Next

            '' Insert Dynamic Grid Column Header
            View = Me.ogcstylecolor.Views(0)
            View = InitialGrid(View)
            View.BestFitColumns()

            GridView1.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200
            GridView2.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            GridView3.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            GridView1.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            GridView2.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            GridView3.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            GridView1.Columns.ColumnByFieldName("FTComponent").Width = 100

            Dim FNSeqCurr As Integer = 0
            Dim FNSeqLast As Integer = 0

            For Each r As DataRow In dt.Rows
                'FNSeqCurr = Val(r!FNMatColorSeq.ToString)
                'If FNSeqLast = 0 Then FNSeqLast = FNSeqCurr
                'If FNSeqCurr > 0 And FNSeqLast < FNSeqCurr Then

                dc = New DataColumn("FTRawMatColorCode" & r!FTColorWay.ToString, System.Type.GetType("System.String"))
                dc1 = New DataColumn("FNHSysRawMatColorId" & "FTRawMatColorCode" & r!FTColorWay.ToString, System.Type.GetType("System.String"))
                dc3 = New DataColumn("FTRawMatColorNameTH" & r!FTColorWay.ToString, System.Type.GetType("System.String"))
                dc4 = New DataColumn("FTRawMatColorNameEN" & r!FTColorWay.ToString, System.Type.GetType("System.String"))

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
                            View.Columns.Item(dc3.ColumnName).FieldName = dc3.ColumnName
                            View.Columns.Item(dc4.ColumnName).FieldName = dc4.ColumnName
                            InitDt.Columns.Add(dc.ColumnName)
                            InitDt.Columns.Add(dc1.ColumnName)
                            InitDt.Columns.Add(dc3.ColumnName)
                            InitDt.Columns.Add(dc4.ColumnName)

                        Catch ex As Exception

                            View.Columns.AddField(dc.ColumnName)
                            View.Columns(dc.ColumnName).FieldName = dc.ColumnName
                            View.Columns(dc.ColumnName).Name = dc.ColumnName
                            View.Columns(dc.ColumnName).Caption = dc.Caption
                            View.Columns(dc.ColumnName).Visible = True
                            View.Columns(dc.ColumnName).Width = 70
                            View.Columns(dc.ColumnName).OptionsColumn.AllowShowHide = False
                            View.Columns(dc.ColumnName).OptionsColumn.ShowInCustomizationForm = False

                            View.Columns.AddField(dc1.ColumnName)
                            View.Columns(dc1.ColumnName).FieldName = dc1.ColumnName
                            View.Columns(dc1.ColumnName).Name = dc1.ColumnName
                            View.Columns(dc1.ColumnName).Caption = dc1.Caption
                            View.Columns(dc1.ColumnName).Tag = r!FNHSysMatColorId.ToString
                            View.Columns(dc1.ColumnName).Visible = False
                            View.Columns(dc1.ColumnName).OptionsColumn.AllowShowHide = False
                            View.Columns(dc1.ColumnName).OptionsColumn.ShowInCustomizationForm = False

                            View.Columns.AddField(dc3.ColumnName)
                            View.Columns(dc3.ColumnName).FieldName = dc3.ColumnName
                            View.Columns(dc3.ColumnName).Name = dc3.ColumnName
                            View.Columns(dc3.ColumnName).Caption = dc3.Caption
                            View.Columns(dc3.ColumnName).Tag = r!FNHSysMatColorId.ToString
                            View.Columns(dc3.ColumnName).Visible = False
                            View.Columns(dc3.ColumnName).OptionsColumn.AllowShowHide = False
                            View.Columns(dc3.ColumnName).OptionsColumn.ShowInCustomizationForm = False

                            View.Columns.AddField(dc4.ColumnName)
                            View.Columns(dc4.ColumnName).FieldName = dc4.ColumnName
                            View.Columns(dc4.ColumnName).Name = dc4.ColumnName
                            View.Columns(dc4.ColumnName).Caption = dc4.Caption
                            View.Columns(dc4.ColumnName).Tag = r!FNHSysMatColorId.ToString
                            View.Columns(dc4.ColumnName).Visible = False
                            View.Columns(dc4.ColumnName).OptionsColumn.AllowShowHide = False
                            View.Columns(dc4.ColumnName).OptionsColumn.ShowInCustomizationForm = False

                            Dim repos As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
                            repos.Buttons.Item(0).Tag = "169"
                            View.Columns(dc.ColumnName).ColumnEdit = repos
                            View.Columns(dc1.ColumnName).ColumnEdit = RepRawMatIDCaledit

                            With repos
                                .CharacterCasing = CharacterCasing.Upper

                                AddHandler .Click, AddressOf DynamicResponButtoneColor_Gotocus
                                AddHandler .ButtonClick, AddressOf DynamicResponButtoneSysHideColor_ButtonClick
                                AddHandler .EditValueChanged, AddressOf DynamicResponButtoneditSysHideColor_EditValueChanged
                                AddHandler .Leave, AddressOf DynamicResponButtoneditSysHideColor_Leave
                                AddHandler .KeyDown, AddressOf DynamicButtoneditColor_KeyDown

                            End With

                            InitDt.Columns.Add(dc.ColumnName)
                            InitDt.Columns.Add(dc1.ColumnName)
                            InitDt.Columns.Add(dc3.ColumnName)
                            InitDt.Columns.Add(dc4.ColumnName)
                            'dtStyleDetail.Columns.Add(dc)
                            'dtStyleDetail.Columns(dc.ColumnName) = dtStyleDetail.Columns(FTColorWay)
                        End Try
                    End If

                End If

                '' Initialize
                'FNSeqLast = FNSeqCurr

            Next

            Dim dtColor As DataTable = New DataTable()
            ' oleDbDataAdapter2.Fill(dtColor)

            StrSql = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[SP_GET_STYLE_COLORWAYINFO_NEW_BOMSHEET_THREAD] " & Val(FNHSysStyleId.Properties.Tag.ToString) & "," & Val(FNHSysSeasonId.Properties.Tag.ToString) & ",'" & HI.ST.Lang.Language.ToString() & "' "
            dtColor = HI.Conn.SQLConn.GetDataTable(StrSql, Conn.DB.DataBaseName.DB_PROD)

            '' Fill data to new datatable
            View = Me.ogcstylecolor.Views(0)
            Dim FNSeqSame As Boolean = False
            For Each r As DataRow In dtColor.Rows
                Dim n As DataRow
                If r.Item("FNHSysMatColorId").ToString() = "1406060024" Then
                    Beep()
                End If
                FNSeqSame = False
                For Each x As DataRow In InitDt.Select("FNSeq=" & Val(r.Item("FNSeq")) & "")
                    If x.Item("FNSeq") = r.Item("FNSeq") Then
                        FNSeqSame = True

                        For Each c As GridColumn In View.Columns
                            If c.Tag = "1406060024" Then
                                Beep()
                            End If

                            If c.Tag = "1406060024" Then
                                Beep()
                            End If

                            If c.Tag = r.Item("FNHSysMatColorId").ToString() Then
                                'If c.Caption = r.Item("FTMatColorName").ToString() And r.Item("FNHSysMatColorId").ToString() <> "" And c.FieldName.ToString.Replace("FNHSysRawMatColorId" & "FTRawMatColorCode", "") = r.Item("FTMatColorName").ToString() Then
                                x.Item("FTRawMatColorCode" & r!FTMatColorName.ToString) = r.Item("FTColorWay")
                                x.Item(c.Name) = r.Item("FNHSysRawMatColorId")
                                x.Item("FTRawMatColorNameTH" & r!FTMatColorName.ToString) = r.Item("FTRawMatColorNameTH")
                                x.Item("FTRawMatColorNameEN" & r!FTMatColorName.ToString) = r.Item("FTRawMatColorNameEN")

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
                    n.Item("FTOrderNo") = r.Item("FTOrderNo")
                    n.Item("FTSubOrderNo") = r.Item("FTSubOrderNo")
                    n.Item("FTRunColor") = r.Item("FTRunColor")

                    For Each c As GridColumn In View.Columns

                        If c.Tag = "1406060024" Then
                            Beep()
                        End If

                        If c.Tag = r.Item("FNHSysMatColorId").ToString() And r.Item("FNHSysMatColorId").ToString() <> "" Then
                            'If c.Caption = r.Item("FTMatColorName").ToString() And r.Item("FNHSysMatColorId").ToString() <> "" And c.FieldName.ToString.Replace("FNHSysRawMatColorId" & "FTRawMatColorCode", "") = r.Item("FTMatColorName").ToString() Then


                            n.Item("FNColorWaySeq") = r.Item("FNColorWaySeq")
                            '    n.Item("FTRunColor") = IIf(r.Item("FTRunColor").ToString = "", "0", r.Item("FTRunColor").ToString)
                            n.Item("FTRawMatColorCode" & r!FTMatColorName.ToString) = r.Item("FTColorWay")
                            n.Item("FTRawMatColorNameEN" & r!FTMatColorName.ToString) = r.Item("FTRawMatColorNameEN")
                            n.Item("FTRawMatColorNameTH" & r!FTMatColorName.ToString) = r.Item("FTRawMatColorNameTH")
                            n.Item(c.Name) = r.Item("FNHSysRawMatColorId")
                            ' ,SC.FTRawMatColorNameEN,SC.FTRawMatColorNameTH 
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
            GridView1.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200
            GridView2.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            GridView3.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            GridView1.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            GridView2.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            GridView3.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            GridView1.Columns.ColumnByFieldName("FTComponent").Width = 100


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
            SelectCmd.Parameters.AddWithValue("@FNHSysSeasonId", Val(FNHSysSeasonId.Properties.Tag.ToString))
            SelectCmd.ExecuteNonQuery()

            dataAdapter.SelectCommand = SelectCmd
            dataAdapter.Fill(dt)

            Dim StrSql As String = ""


            StrSql = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_STYLE_SIZEBREAKDOWN_NEW_BOMSHEET] " & Val(FNHSysStyleId.Properties.Tag.ToString) & "," & Val(FNHSysSeasonId.Properties.Tag.ToString) & ",'" & HI.ST.Lang.Language.ToString() & "' "
            dtStyleDetail = HI.Conn.SQLConn.GetDataTable(StrSql, Conn.DB.DataBaseName.DB_MERCHAN)

            '' Initial data to dynamic column
            Dim InitDt As DataTable = New DataTable()
            InitDt = dtStyleDetail.Clone()

            'For Each c As DataColumn In dtStyleDetail.Columns
            '    InitDt.Columns.Add(c.ColumnName, c.DataType)
            'Next

            '' Insert Dynamic Grid Column Header
            View = Me.ogcstylesize.Views(0)
            View = InitialGrid(View)
            View.BestFitColumns()
            GridView1.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200
            GridView2.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            GridView3.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            GridView1.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            GridView2.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            GridView3.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            GridView1.Columns.ColumnByFieldName("FTComponent").Width = 100

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
                            View.Columns(dc.ColumnName).Width = 70
                            View.Columns(dc.ColumnName).OptionsColumn.AllowShowHide = False
                            View.Columns(dc.ColumnName).OptionsColumn.ShowInCustomizationForm = False

                            View.Columns.AddField(dc1.ColumnName)
                            View.Columns(dc1.ColumnName).FieldName = dc1.ColumnName
                            View.Columns(dc1.ColumnName).Name = dc1.ColumnName
                            View.Columns(dc1.ColumnName).Caption = dc1.Caption
                            View.Columns(dc1.ColumnName).Tag = r!FNHSysMatSizeId.ToString
                            View.Columns(dc1.ColumnName).Visible = False
                            View.Columns(dc1.ColumnName).OptionsColumn.AllowShowHide = False
                            View.Columns(dc1.ColumnName).OptionsColumn.ShowInCustomizationForm = False

                            Dim repos As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
                            repos.Buttons.Item(0).Tag = "170"
                            View.Columns(dc.ColumnName).ColumnEdit = repos

                            With repos
                                .CharacterCasing = CharacterCasing.Upper
                                AddHandler .Click, AddressOf DynamicResponButtone_Gotocus
                                AddHandler .ButtonClick, AddressOf DynamicResponButtoneSysHide_ButtonClick
                                AddHandler .EditValueChanged, AddressOf DynamicResponButtoneditSysHide_EditValueChanged
                                AddHandler .Leave, AddressOf DynamicResponButtoneditSysHide_Leave
                                AddHandler .KeyDown, AddressOf DynamicButtoneditSize_KeyDown
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

            Dim dtSize As DataTable = New DataTable()
            'oleDbDataAdapter2.Fill(dtSize)
            StrSql = " EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[SP_GET_STYLE_SIZEBREAKDOWNINFO_NEW_BOMSHEET_THREAD] " & Val(FNHSysStyleId.Properties.Tag.ToString) & "," & Val(FNHSysSeasonId.Properties.Tag.ToString) & ",'" & HI.ST.Lang.Language.ToString() & "' "
            dtSize = HI.Conn.SQLConn.GetDataTable(StrSql, Conn.DB.DataBaseName.DB_PROD)

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
                    n.Item("FTOrderNo") = r.Item("FTOrderNo")
                    n.Item("FTSubOrderNo") = r.Item("FTSubOrderNo")
                    n.Item("FTRunSize") = r.Item("FTRunSize")

                    For Each c As GridColumn In View.Columns
                        If c.Tag = r.Item("FNHSysMatSizeId").ToString() And r.Item("FNHSysMatSizeId").ToString() <> "" Then
                            n.Item("FNSieBreakDownSeq") = r.Item("FNSieBreakDownSeq")
                            ' n.Item("FTRunSize") = IIf(r.Item("FTRunSize").ToString = "", "0", r.Item("FTRunSize").ToString)
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
            GridView1.Columns.ColumnByFieldName("FNHSysMerMatId_None").Width = 200
            GridView2.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            GridView3.Columns.ColumnByFieldName("FTMainMatName").Width = 200
            GridView1.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            GridView2.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            GridView3.Columns.ColumnByFieldName("FTPositionPartName").Width = 150
            GridView1.Columns.ColumnByFieldName("FTComponent").Width = 100


            ogcstylesize.DataSource = InitDt
            ogcstylesize = View.GridControl
            ogcstylesize.Refresh()

            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            'MessageBox.Show(ex.Message)
        End Try

    End Sub


    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        Try
            If Me.FNHSysStyleId.Text <> "" And FNHSysStyleId.Properties.Tag.ToString <> "" Then
                If Me.FNHSysCustId.Text <> "" And Me.FNHSysCustId.Properties.Tag.ToString <> "" Then
                    If Me.FNHSysSeasonId.Text <> "" And Me.FNHSysSeasonId.Properties.Tag.ToString <> "" Then
                        _Pass = True
                    Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysSeasonId_lbl.Text)
                        FNHSysSeasonId.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysCustId_lbl.Text)
                    FNHSysCustId.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysStyleId_lbl.Text)
                FNHSysStyleId.Focus()
            End If
        Catch ex As Exception

        End Try
        Return _Pass

    End Function

    Private Sub ocmsave_Click(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        If CheckOwner() = False Then
            Exit Sub
        End If

        If Me.VerifyData() Then
            If ogcmatcode.DataSource Is Nothing Then
                Exit Sub
            End If

            With CType(ogcmatcode.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTUnitCode=''").Length > 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาทำการระบุ  หน่วยวัตถุดิบให้ครบ !!!", 1505210874, Me.Text, , MessageBoxIcon.Warning)
                    Exit Sub
                End If

                If .Select("FTStateFree='1' AND FTSuplCode=''").Length > 0 Then
                    HI.MG.ShowMsg.mInfo("พบข้อมูลวัตถุดิบ Free กรุณาทำการระบุ Supplier !!!", 1505210875, Me.Text, , MessageBoxIcon.Warning)
                    Exit Sub
                End If

            End With

            Dim _Spls As New HI.TL.SplashScreen("Saving.... ,Please Wait.")
            If Me.SaveData Then
                '' Update order information.

                Me.ProcComplete = True
                LoadStyleInfo(FNHSysStyleId.Properties.Tag.ToString, False)

                Try
                    Select Case Me.otb.SelectedTabPage.Name
                        Case otpmatcode.Name

                            ' Call LoadStyleDetail(FNHSysStyleId.Properties.Tag.ToString)
                            Call LoadColorwaySize(FNHSysStyleId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)

                        Case otpmatcolor.Name
                            'Try
                            '    Call ImportColor()
                            '    Call ImportSize()
                            'Catch ex As Exception
                            'End Try
                        Case otpmatsize.Name
                            'Try
                            '    Call ImportSize()
                            'Catch ex As Exception
                            'End Try
                    End Select

                Catch ex As Exception
                End Try

                _Spls.Close()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                ' InitNewRow(CType(ogcmatcode.DataSource, DataTable), TabIndexs.StyleDetail)
            Else
                _Spls.Close()
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub ocmdelete_Click(sender As System.Object, e As System.EventArgs) Handles ocmdelete.Click

        If CheckOwner() = False Then
            Exit Sub
        End If
        If CType(ogcmatcode.DataSource, DataTable).Rows.Count > 0 Then

            Dim _Qry As String = ""
            _Qry = "  Select TOP 1 FNHSysStyleId"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTMPRThread AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & ""
            _Qry &= vbCrLf & " AND FNHSysSeasonId=" & Integer.Parse(Val(FNHSysSeasonId.Properties.Tag.ToString)) & ""

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "") = "" Then

                If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text) = True Then
                    Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
                    If Me.DeleteAllData(_Spls) Then


                        HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, "Delete Data (Style " & FNHSysStyleId.Text & " Season " & FNHSysSeasonId.Text & ") ")


                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                        HI.TL.HandlerControl.ClearControl(Me)
                    Else
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    End If
                End If

            Else
                HI.MG.ShowMsg.mInfo("Style นี้ได้ทำการ Generate MRP แล้วไม่สามารถทำการลบได้ !!!", 1408210001, Me.Text, FNHSysStyleId.Text, MessageBoxIcon.Warning)
            End If

        End If
    End Sub

    Private Sub ocmbomdiffpart_Click(sender As System.Object, e As System.EventArgs) Handles ocmbomdiffpart.Click
        If CheckOwner() = False Then
            Exit Sub
        End If
        Try
            CType(ogcmatcode.DataSource, DataTable).AcceptChanges()
        Catch ex As Exception
        End Try

        Dim crRow As Double, nxRow As String, nwRow As String
        Dim RowCount As Integer = 0
        View = Me.ogcmatcode.Views(0)
        If FNHSysStyleId.Properties.Tag.ToString = "" Or FNHSysSeasonId.Properties.Tag.ToString = "" Then Return

        If (Not IsDBNull(View)) Then
            RowsIndex = View.FocusedRowHandle
            TopVisibleIndex = View.TopRowIndex
            RowCount = View.RowCount
        End If
        nxRow = 0

        crRow = Val(View.GetRowCellValue(RowsIndex, "FNMerMatSeq").ToString)

        Dim dt As DataTable = CType(ogcmatcode.DataSource, DataTable)

        Try
            ' nxRow = Val(View.GetRowCellValue(RowsIndex + 1, "FNMerMatSeq").ToString)
            For Each R As DataRow In dt.Select("FNMerMatSeq>" & crRow & "", "FNMerMatSeq")
                nxRow = Double.Parse(Val(R!FNMerMatSeq.ToString))
                Exit For
            Next

        Catch ex As Exception
            nxRow = 0
        End Try

        nwRow = Str(crRow + 0.001).Trim

        'If nxRow = nwRow Then
        '    Return
        'End If
        ' Dim MaxSeq As Double = 1.0

        Dim _RunSeq As Integer = 0

        _RunSeq = GetDataRunMaxSeq(FNHSysStyleId.Properties.Tag.ToString()) 'Val(FNHSysSeasonId.Properties.Tag.ToString())

        Dim MaxSeq As Double = _RunSeq + 1.0

        Dim LastSeq As Double = 0

        For Each r As DataRow In dt.Rows

            LastSeq = r.Item("FNSeq")

            If LastSeq >= MaxSeq Then
                MaxSeq = LastSeq + 1
            End If

        Next

        If LastSeq = MaxSeq Then MaxSeq = LastSeq + 1

        Dim ItemID As Long = Val(View.GetRowCellValue(RowsIndex, "FNHSysMerMatId").ToString)
        Dim ItemCode As String = View.GetRowCellValue(RowsIndex, "FTMainMatCode").ToString
        Dim CurrentPart As Integer = Integer.Parse(Val(View.GetRowCellValue(RowsIndex, "FNPart").ToString))
        Dim MaxPart As Integer = 0
        Dim LastPart As Integer = 0
        Dim dtP As DataTable = CType(ogcmatcode.DataSource, DataTable)


        dt = CType(ogcmatcode.DataSource, DataTable)
        Dim StateAdd As Boolean = False
        For Each r As DataRow In dt.Select("FNHSysMerMatId=" & ItemID & "", "FNPart")

            If Integer.Parse(Val(r.Item("FNPart"))) = CurrentPart Then
                If StateAdd = False Then
                    Dim dr As DataRow = dt.NewRow()

                    For Each c As DataColumn In dt.Columns

                        Select Case c.ColumnName
                            Case "FNSeq"
                                dr.Item(c) = MaxSeq
                            Case "FNPart"
                                dr.Item(c) = CurrentPart + 1
                            Case "FTStateDataDetail"
                                dr.Item(c) = "0"
                            Case Else
                                Try
                                    dr.Item(c) = View.GetRowCellValue(RowsIndex, c.ColumnName) '.ToString
                                Catch ex As Exception
                                End Try
                        End Select

                    Next

                    dt.Rows.InsertAt(dr, RowsIndex + 1)

                    StateAdd = True
                End If

            Else
                If Integer.Parse(Val(r.Item("FNPart"))) > CurrentPart Then
                    r.Item("FNPart") = Integer.Parse(Val(r.Item("FNPart"))) + 1
                End If
            End If
        Next

        Try
            CType(ogcmatcode.DataSource, DataTable).AcceptChanges()
        Catch ex As Exception
        End Try

        ' If LastPart = MaxPart Then MaxPart = LastPart + 1

        Call InitialGridMergCell()

    End Sub

    Private Function SaveData() As Boolean
        Dim ret As Boolean
        Dim _Str As String = ""

        Try

            ret = UpdateDatasource()

            Return ret

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

        CType(ogcmatcode.DataSource, DataTable).AcceptChanges()
        CType(ogcstylecolor.DataSource, DataTable).AcceptChanges()
        CType(ogcstylesize.DataSource, DataTable).AcceptChanges()

        Dim dt1 As DataTable = CType(ogcmatcode.DataSource, DataTable)
        Dim dt2 As DataTable = CType(ogcstylecolor.DataSource, DataTable)
        Dim dt3 As DataTable = CType(ogcstylesize.DataSource, DataTable)
        'If Not (View.PostEditor() And View.UpdateCurrentRow()) Then Return False
        Select Case Me.otb.SelectedTabPage.Name
            Case otpmatcode.Name
                ' Update Style detail.
                If IsNothing(dt1) Then
                    ret = True
                Else

                    'If FTStateSaveTabPage.Checked = False Then
                    '    Call LoadColorwaySize(FNHSysStyleId.Properties.Tag, FNHSysSeasonId.Properties.Tag.ToString)
                    'End If

                    HI.Auditor.CreateLog.CreateLogBomSheetStyleMat(Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)), Integer.Parse(Val(FNHSysSeasonId.Properties.Tag.ToString)))
                    ret = DoUpdateTable(oleDbDataAdapter2, dt1)
                    HI.Auditor.CreateLog.CreateLogBomSheetStyleMat(Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)), Integer.Parse(Val(FNHSysSeasonId.Properties.Tag.ToString)))

                    If ret = False Then Return ret
                End If

                Dim _Qry As String = ""

                _Qry = " DELETE FROM  C"
                _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_Mat AS A RIGHT OUTER JOIN"
                _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_ColorWay AS C ON A.FNHSysStyleId = C.FNHSysStyleId AND A.FNSeq = C.FNSeq"
                _Qry &= vbCrLf & "  WHERE   C.FNHSysStyleId=" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & " AND   (A.FNHSysStyleId Is NULL)"

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                _Qry = " DELETE FROM  C"
                _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_Mat AS A RIGHT OUTER JOIN"
                _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_SizeBreakDown AS C ON A.FNHSysStyleId = C.FNHSysStyleId AND A.FNSeq = C.FNSeq"
                _Qry &= vbCrLf & "  WHERE   C.FNHSysStyleId=" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & " AND   (A.FNHSysStyleId Is NULL)"

                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)


                Call LoadColorwaySize(FNHSysStyleId.Properties.Tag, FNHSysSeasonId.Properties.Tag.ToString)

                    dt2 = CType(ogcstylecolor.DataSource, DataTable)
                    dt3 = CType(ogcstylesize.DataSource, DataTable)

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

            Case otpmatcolor.Name
                ' Update Color way
                If IsNothing(dt2) Then
                    ret = True
                Else
                    ret = DoUpdateColor(oleDbDataAdapter2, dt2)
                    If ret = False Then Return ret
                End If


                Call ImportSize()

                    ' Call ImportSizeForSave()
                    dt3 = CType(ogcstylesize.DataSource, DataTable)

                ' Update Size Breakdown
                If IsNothing(dt3) Then
                    ret = True
                Else
                    ret = DoUpdateSize(oleDbDataAdapter2, dt3)
                    If ret = False Then Return ret
                End If

            Case otpmatsize.Name
                ' Update Size Breakdown
                If IsNothing(dt3) Then
                    ret = True
                Else
                    ret = DoUpdateSize(oleDbDataAdapter2, dt3)
                    If ret = False Then Return ret
                End If
        End Select

        Return ret

    End Function

    Private Function UpdateDatasourceDelete() As Boolean
        Dim ret As Boolean
        'Save the latest changes to the bound DataTable 
        View = Me.ogcmatcode.Views(0)
        View.ClearSorting()
        View.Columns("FNMerMatSeq").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        View.Columns("FNPart").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

        CType(ogcmatcode.DataSource, DataTable).AcceptChanges()
        CType(ogcstylecolor.DataSource, DataTable).AcceptChanges()
        CType(ogcstylesize.DataSource, DataTable).AcceptChanges()

        Dim dt1 As DataTable = CType(ogcmatcode.DataSource, DataTable)
        Dim dt2 As DataTable = CType(ogcstylecolor.DataSource, DataTable)
        Dim dt3 As DataTable = CType(ogcstylesize.DataSource, DataTable)
        'If Not (View.PostEditor() And View.UpdateCurrentRow()) Then Return False

        Dim _Qry As String = ""

        _Qry = " DELETE FROM  C"
        _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_Mat AS A RIGHT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_ColorWay AS C ON A.FNHSysStyleId = C.FNHSysStyleId AND A.FNSeq = C.FNSeq"
        _Qry &= vbCrLf & "  WHERE   C.FNHSysStyleId=" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & " AND   (A.FNHSysStyleId Is NULL)"

        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

        _Qry = " DELETE FROM  C"
        _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_Mat AS A RIGHT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_SizeBreakDown AS C ON A.FNHSysStyleId = C.FNHSysStyleId AND A.FNSeq = C.FNSeq"
        _Qry &= vbCrLf & "  WHERE   C.FNHSysStyleId=" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & " AND   (A.FNHSysStyleId Is NULL)"

        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Return ret

    End Function

    Private Function DoUpdateTable(ByVal dataAdapter As SqlDataAdapter,
        ByVal dataTable As System.Data.DataTable) As Boolean
        Dim InsertSql As String = ""

        Dim dt As DataTable
        dt = dataTable 'CType(ogcmatcode.DataSource, DataTable)

        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
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
            dtx = HI.Conn.SQLConn.GetDataTable(StrSql, Conn.DB.DataBaseName.DB_PROD)

            If dtx.Rows.Count > 0 Then
                For Each Rx As DataRow In dtx.Rows
                    UpdateDate = Rx!UpdateDate.ToString()
                    UpdateTime = Rx!UpdateTime.ToString()
                Next
            End If

            Dim _Cmd As String = ""
            If dt.Rows.Count = 0 Then Return True
            For Each r As DataRow In dt.Select("FTMainMatCode<>''")
                Dim SelectCMD As SqlCommand = New SqlCommand(dataAdapter.SelectCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                SelectCMD.Parameters.AddWithValue("@FNHSysStyleId", (r.Item("FNHSysStyleId").ToString))
                SelectCMD.Parameters.AddWithValue("@FNSeq", (r.Item("FNSeq").ToString))
                Dim cnt As Integer
                cnt = SelectCMD.ExecuteScalar
                If cnt = 0 Then

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
                    InsertCMD.Parameters.AddWithValue("@FTStateFree", CInt(Val(r.Item("FTStateFree").ToString)).ToString)
                    InsertCMD.Parameters.AddWithValue("@FTPositionPartId", r.Item("FTPositionPartId").ToString)
                    InsertCMD.Parameters.AddWithValue("@FTPart", r.Item("FTPart").ToString)
                    InsertCMD.Parameters.AddWithValue("@FTComponent", r.Item("FTComponent").ToString)
                    InsertCMD.Parameters.AddWithValue("@FNHSysSeasonId", CInt(Val(FNHSysSeasonId.Properties.Tag.ToString)))
                    'InsertCMD.Parameters.AddWithValue("@FNHSysSeasonId", CInt(Val(r.Item("FNHSysSeasonId").ToString)))
                    InsertCMD.CommandType = CommandType.Text
                    InsertCMD.ExecuteNonQuery()
                    InsertCMD.Parameters.Clear()

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
                    UpdateCmd.Parameters.AddWithValue("@FTPositionPartId", r.Item("FTPositionPartId").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FTPart", r.Item("FTPart").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FTComponent", r.Item("FTComponent").ToString)
                    UpdateCmd.Parameters.AddWithValue("@FTStateFree", CInt(Val(r.Item("FTStateFree").ToString)).ToString)
                    UpdateCmd.Parameters.AddWithValue("@FNHSysSeasonId", CInt(Val(FNHSysSeasonId.Properties.Tag.ToString)))
                    'UpdateCmd.Parameters.AddWithValue("@FNHSysSeasonId", CInt(Val(r.Item("FNHSysSeasonId").ToString)))
                    UpdateCmd.CommandType = CommandType.Text
                    UpdateCmd.ExecuteNonQuery()
                    UpdateCmd.Parameters.Clear()


                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            '' Create Data Table after update new value
            'Dim sqlAfter As String = sqlBefore
            'dtAfter = HI.Conn.SQLConn.GetDataTable(sqlAfter, Conn.DB.DataBaseName.DB_MERCHAN)

            ' Call PostSave("[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTThreadStyle_Mat]", Me.FNHSysStyleId.Text)

            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            MessageBox.Show(ex.Message)
            Return False
        End Try
    End Function

    Private Function DoUpdateColor(ByVal dataAdapter As SqlDataAdapter,
        ByVal dataTable As System.Data.DataTable) As Boolean
        Dim InsertSql As String = ""

        Try

            HI.Auditor.CreateLog.CreateLogBomSheetStyleColorWay(Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)), Integer.Parse(Val(FNHSysSeasonId.Properties.Tag.ToString)))

            Dim _Qry As String = ""
            Dim UpdateDate As String = "", UpdateTime As String = ""
            Dim StrSql As String = "SELECT " & HI.UL.ULDate.FormatDateDB & " AS UpdateDate, " & HI.UL.ULDate.FormatTimeDB & " AS UpdateTime"
            Dim dtx As DataTable
            dtx = HI.Conn.SQLConn.GetDataTable(StrSql, Conn.DB.DataBaseName.DB_PROD)

            If dtx.Rows.Count > 0 Then

                For Each Rx As DataRow In dtx.Rows

                    UpdateDate = Rx!UpdateDate.ToString()
                    UpdateTime = Rx!UpdateTime.ToString()

                Next

            End If

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim dt As DataTable
            dt = dataTable 'CType(ogcstylecolor.DataSource, DataTable)

            'dataAdapter = CreateAdapterColor(HI.Conn.SQLConn.Cnn)
            'InsertSql = dataAdapter.InsertCommand.CommandText
            'Dim InsertCMD As SqlCommand = New SqlCommand(dataAdapter.InsertCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
            'Dim UpdateCmd As SqlCommand = New SqlCommand(dataAdapter.UpdateCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)

            Dim Views As GridView = New GridView()
            Views = ogcstylecolor.Views(0)

            '' Create Data Table after update new value
            'Dim sqlBefore As String =
            '    "SELECT     SC.FNHSysStyleId, SC.FNSeq, SC.FNMerMatSeq, SC.FNColorWaySeq, SC.FTRunColor, 'Rawmat color ' + IC.FTRawMatColorCode + ': ' + IC.FTRawMatColorNameEN AS FNHSysRawMatColorId, " & vbCrLf & _
            '    "                      'Color code ' + MC.FTMatColorCode + ': ' + MC.FTMatColorNameEN AS FNHSysMatColorId" & vbCrLf & _
            '    "FROM         HITECH_MERCHAN.dbo.TMERTThreadStyle_ColorWay AS SC LEFT OUTER JOIN" & vbCrLf & _
            '    "                      HITECH_MASTER.dbo.TMERMMatColor AS MC ON SC.FNHSysMatColorId = MC.FNHSysMatColorId LEFT OUTER JOIN" & vbCrLf & _
            '    "                      HITECH_MASTER.dbo.TINVENMMatColor AS IC ON SC.FNHSysRawMatColorId = IC.FNHSysRawMatColorId" & vbCrLf & _
            '    "WHERE SC.FNHSysStyleId = " & Val(Me.FNHSysStyleId.Properties.Tag.ToString) & vbCrLf & _
            '    "ORDER BY  SC.FNHSysStyleId, SC.FNSeq, SC.FNMerMatSeq,'Color code ' + MC.FTMatColorCode + ': ' + MC.FTMatColorNameEN "
            'dtBefore = HI.Conn.SQLConn.GetDataTable(sqlBefore, Conn.DB.DataBaseName.DB_MERCHAN)
            'Add parameters and set values.

            '' Delete Color
            _Qry = " DELETE FROM  A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_ColorWay AS A "
            _Qry &= vbCrLf & "  RIGHT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_Mat AS B ON A.FNHSysStyleId=B.FNHSysStyleId AND A.FNSeq=B.FNSeq"
            _Qry &= vbCrLf & " WHERE B.FNHSysStyleId =" & Val(FNHSysStyleId.Properties.Tag.ToString())
            _Qry &= vbCrLf & " AND B.FNHSysSeasonId =" & Val(FNHSysSeasonId.Properties.Tag.ToString())
            _Qry &= vbCrLf & " AND A.FNHSysStyleId IS NULL "

            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Qry = " DELETE FROM  A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_ColorWay AS A "
            _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_Mat AS B ON A.FNHSysStyleId=B.FNHSysStyleId AND A.FNSeq=B.FNSeq"
            _Qry &= vbCrLf & " WHERE B.FNHSysStyleId =" & Val(FNHSysStyleId.Properties.Tag.ToString())
            _Qry &= vbCrLf & " AND B.FNHSysSeasonId =" & Val(FNHSysSeasonId.Properties.Tag.ToString())
            _Qry &= vbCrLf & " AND B.FTStateActive ='1' "

            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            Dim dttmpColorway As New DataTable
            dttmpColorway.Columns.Add("FNHSysMatColorId", GetType(Integer))
            dttmpColorway.Columns.Add("FNColorWaySeq", GetType(Integer))

            If dt.Rows.Count = 0 Then Return True
            For Each r As DataRow In dt.Rows
                ' Dim SelectCMD As SqlCommand = New SqlCommand(dataAdapter.SelectCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                Dim CIndex As Integer = 0
                Dim Cols As Integer = dt.Columns.Count
                Dim Colx As Integer = 0
                Dim ColorWay As String = ""
                For Each c As DataColumn In dt.Columns
                    Colx += 1

                    If dt.Columns.IndexOf(c) > 15 And (Microsoft.VisualBasic.Left(c.ColumnName.ToString, "FTRawMatColorName".Length)).ToUpper <> "FTRawMatColorName".ToUpper Then

                        If (Microsoft.VisualBasic.Left(c.ColumnName.ToString, ("FNHSysRawMatColorId" & "FTRawMatColorCode").Length)).ToUpper = ("FNHSysRawMatColorId" & "FTRawMatColorCode").ToUpper Then

                            CIndex += 1
                            'SelectCMD.Parameters.AddWithValue("@FNHSysStyleId", Val(r.Item("FNHSysStyleId").ToString))
                            'SelectCMD.Parameters.AddWithValue("@FNSeq", r.Item("FNSeq").ToString)
                            'SelectCMD.Parameters.AddWithValue("@FNColorWaySeq", CIndex)

                            ColorWay = c.ColumnName.ToString.Replace("FNHSysRawMatColorId" & "FTRawMatColorCode", "")
                            'Dim cnt As Integer
                            'cnt = SelectCMD.ExecuteScalar
                            'If cnt = 0 Then

                            'If dttmpColorway.Select("FNHSysMatColorId=" & Integer.Parse(Val(Views.Columns(c.ColumnName.ToString).Tag.ToString)) & "").Length <= 0 Then
                            '    dttmpColorway.Rows.Add(Integer.Parse(Val(Views.Columns(c.ColumnName.ToString).Tag.ToString)), CIndex)
                            'End If

                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TMERTThreadStyle_ColorWay] "
                            _Qry &= vbCrLf & "   ([FNHSysStyleId],[FNSeq],[FNMerMatSeq],[FNColorWaySeq],[FTRunColor],[FNHSysRawMatColorId], [FNHSysMatColorId], "
                            _Qry &= vbCrLf & "  [FTInsUser],[FDInsDate],[FTInsTime],[FTRawMatColorNameTH],[FTRawMatColorNameEN]) "
                            _Qry &= vbCrLf & "  SELECT " & Val(r.Item("FNHSysStyleId").ToString) & ""
                            _Qry &= vbCrLf & " , " & Val(r.Item("FNSeq").ToString)
                            _Qry &= vbCrLf & " , " & Val(r.Item("FNMerMatSeq").ToString)
                            _Qry &= vbCrLf & "  ," & CIndex
                            _Qry &= vbCrLf & " , '" & HI.UL.ULF.rpQuoted(r.Item("FTRunColor").ToString) & "'"
                            _Qry &= vbCrLf & "  ," & Val(r.Item(c.ColumnName.ToString).ToString)
                            _Qry &= vbCrLf & "  ," & Val(Views.Columns(c.ColumnName.ToString).Tag.ToString)
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Qry &= vbCrLf & " , " & HI.UL.ULDate.FormatTimeDB
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(r.Item("FTRawMatColorNameTH" & ColorWay).ToString()) & "'"
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(r.Item("FTRawMatColorNameEN" & ColorWay).ToString()) & "'"

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False

                            End If

                            'InsertCMD.Parameters.AddWithValue("@FNHSysStyleId", Val(r.Item("FNHSysStyleId").ToString))
                            'InsertCMD.Parameters.AddWithValue("@FNSeq", r.Item("FNSeq").ToString)
                            'InsertCMD.Parameters.AddWithValue("@FNMerMatSeq", CDbl(r.Item("FNMerMatSeq").ToString))
                            'InsertCMD.Parameters.AddWithValue("@FTRunColor", r.Item("FTRunColor").ToString)

                            'InsertCMD.Parameters.AddWithValue("@FNColorWaySeq", CIndex)
                            'InsertCMD.Parameters.AddWithValue("@FNHSysRawMatColorId", r.Item(c.ColumnName.ToString).ToString)
                            'InsertCMD.Parameters.AddWithValue("@FNHSysMatColorId", Views.Columns(c.ColumnName.ToString).Tag.ToString)

                            'InsertCMD.Parameters.AddWithValue("@FTRawMatColorNameTH", r.Item("FTRawMatColorNameTH" & ColorWay).ToString)
                            'InsertCMD.Parameters.AddWithValue("@FTRawMatColorNameEN", r.Item("FTRawMatColorNameEN" & ColorWay).ToString)

                            'InsertCMD.Parameters.AddWithValue("@FTInsUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
                            'InsertCMD.Parameters.AddWithValue("@FDInsDate", UpdateDate)
                            'InsertCMD.Parameters.AddWithValue("@FTInsTime", UpdateTime)
                            'InsertCMD.Parameters.AddWithValue("@FTUpdUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
                            'InsertCMD.Parameters.AddWithValue("@FDUpdDate", UpdateDate)
                            'InsertCMD.Parameters.AddWithValue("@FTUpdTime", UpdateTime)

                            'InsertCMD.CommandType = CommandType.Text
                            'InsertCMD.ExecuteNonQuery()
                            'InsertCMD.Parameters.Clear()
                            'Else

                            '    If dttmpColorway.Select("FNHSysMatColorId=" & Integer.Parse(Val(Views.Columns(c.ColumnName.ToString).Tag.ToString)) & "").Length <= 0 Then
                            '        dttmpColorway.Rows.Add(Integer.Parse(Val(Views.Columns(c.ColumnName.ToString).Tag.ToString)), CIndex)
                            '    End If

                            '    UpdateCmd.Parameters.AddWithValue("@FNHSysStyleId", Val(r.Item("FNHSysStyleId").ToString))
                            '    UpdateCmd.Parameters.AddWithValue("@FNSeq", r.Item("FNSeq").ToString)
                            '    UpdateCmd.Parameters.AddWithValue("@FNMerMatSeq", CDbl(r.Item("FNMerMatSeq").ToString))
                            '    UpdateCmd.Parameters.AddWithValue("@FTRunColor", r.Item("FTRunColor").ToString)
                            '    UpdateCmd.Parameters.AddWithValue("@FTRawMatColorNameTH", r.Item("FTRawMatColorNameTH" & ColorWay).ToString)
                            '    UpdateCmd.Parameters.AddWithValue("@FTRawMatColorNameEN", r.Item("FTRawMatColorNameEN" & ColorWay).ToString)

                            '    UpdateCmd.Parameters.AddWithValue("@FNColorWaySeq", CIndex)
                            '    UpdateCmd.Parameters.AddWithValue("@FNHSysRawMatColorId", r.Item(c.ColumnName.ToString).ToString)
                            '    UpdateCmd.Parameters.AddWithValue("@FNHSysMatColorId", Views.Columns(c.ColumnName.ToString).Tag.ToString)

                            '    UpdateCmd.Parameters.AddWithValue("@FTUpdUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
                            '    UpdateCmd.Parameters.AddWithValue("@FDUpdDate", UpdateDate)
                            '    UpdateCmd.Parameters.AddWithValue("@FTUpdTime", UpdateTime)

                            '    UpdateCmd.CommandType = CommandType.Text
                            '    UpdateCmd.ExecuteNonQuery()
                            '    UpdateCmd.Parameters.Clear()
                            '    UpdateCmd.Parameters.Clear()
                            '
                            'End If

                            'SelectCMD.Parameters.Clear()

                        End If
                    End If
                Next

            Next

            'If dttmpColorway.Rows.Count > 0 Then

            '    _Qry = " DELETE  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTThreadStyle_ColorWay "
            '    _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & Integer.Parse(Val(Me.FNHSysStyleId.Properties.Tag.ToString)) & " "

            '    _Qry &= vbCrLf & " AND NOT( Convert(varchar(30),FNHSysMatColorId)+'|'+ Convert(varchar(30),FNColorWaySeq) IN (''"

            '    For Each R As DataRow In dttmpColorway.Rows
            '        _Qry &= vbCrLf & ",'" & R!FNHSysMatColorId.ToString & "|" & R!FNColorWaySeq.ToString & "'"
            '    Next

            '    _Qry &= vbCrLf & " ))"
            '    HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            'Else
            '    _Qry = " DELETE  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTThreadStyle_ColorWay "
            '    _Qry &= vbCrLf & " WHERE FNHSysStyleId=" & Integer.Parse(Val(Me.FNHSysStyleId.Properties.Tag.ToString)) & " "

            '    HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
            'End If

            dttmpColorway.Dispose()
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            '' Create Data Table after update new value
            'Dim sqlAfter As String = sqlBefore
            'dtAfter = HI.Conn.SQLConn.GetDataTable(sqlAfter, Conn.DB.DataBaseName.DB_MERCHAN)

            ' Call PostSave("[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTThreadStyle_ColorWay]", Me.FNHSysStyleId.Text)
            HI.Auditor.CreateLog.CreateLogBomSheetStyleColorWay(Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)), Integer.Parse(Val(FNHSysSeasonId.Properties.Tag.ToString)))

            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            MessageBox.Show(ex.Message)
            Return False
        End Try
    End Function

    Private Function DoUpdateSize(ByVal dataAdapter As SqlDataAdapter,
        ByVal dataTable As System.Data.DataTable) As Boolean
        Dim InsertSql As String = ""
        Dim Views As GridView = New GridView()
        Try

            HI.Auditor.CreateLog.CreateLogBomSheetStyleBreakDown(Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)), Integer.Parse(Val(FNHSysSeasonId.Properties.Tag.ToString)))

            Dim _Qry As String = ""
            Dim UpdateDate As String = "", UpdateTime As String = ""
            Dim StrSql As String = "SELECT " & HI.UL.ULDate.FormatDateDB & " AS UpdateDate, " & HI.UL.ULDate.FormatTimeDB & " AS UpdateTime"
            Dim dtx As DataTable
            dtx = HI.Conn.SQLConn.GetDataTable(StrSql, Conn.DB.DataBaseName.DB_PROD)

            If dtx.Rows.Count > 0 Then
                For Each Rx As DataRow In dtx.Rows
                    UpdateDate = Rx!UpdateDate.ToString()
                    UpdateTime = Rx!UpdateTime.ToString()
                Next
            End If

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim dt As DataTable
            dt = dataTable 'CType(ogcstylecolor.DataSource, DataTable)

            'dataAdapter = CreateAdapterSize(HI.Conn.SQLConn.Cnn)
            'InsertSql = dataAdapter.InsertCommand.CommandText
            'Dim InsertCMD As SqlCommand = New SqlCommand(dataAdapter.InsertCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
            'Dim UpdateCmd As SqlCommand = New SqlCommand(dataAdapter.UpdateCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)


            Views = ogcstylesize.Views(0)

            '' Create Data Table after update new value
            'Dim sqlBefore As String =
            '    "SELECT     SS.FNHSysStyleId, SS.FNSeq, SS.FNMerMatSeq, SS.FNSieBreakDownSeq, SS.FTSizeBreakDown, SS.FTRunSize, 'Rawmat size code ' + VS.FTRawMatSizeCode + ': ' + VS.FTRawMatSizeNameEN AS FNHSysRawMatSizeId," & vbCrLf & _
            '    "                       'Size code ' + MS.FTMatSizeCode + ': ' + MS.FTMatSizeNameEN AS FNHSysMatSizeId" & vbCrLf & _
            '    "FROM         HITECH_MERCHAN.dbo.TMERTThreadStyle_SizeBreakDown AS SS LEFT OUTER JOIN" & vbCrLf & _
            '    "                      HITECH_MASTER.dbo.TINVENMMatSize AS VS ON SS.FNHSysRawMatSizeId = VS.FNHSysRawMatSizeId LEFT OUTER JOIN" & vbCrLf & _
            '    "                      HITECH_MASTER.dbo.TMERMMatSize AS MS ON SS.FNHSysMatSizeId = MS.FNHSysMatSizeId" & vbCrLf & _
            '    "WHERE SS.FNHSysStyleId = " & Val(Me.FNHSysStyleId.Properties.Tag.ToString) & vbCrLf & _
            '    "ORDER BY  SS.FNHSysStyleId, SS.FNSeq, SS.FNMerMatSeq,SS.FTSizeBreakDown"
            'dtBefore = HI.Conn.SQLConn.GetDataTable(sqlBefore, Conn.DB.DataBaseName.DB_MERCHAN)

            Dim dttmpColorway As New DataTable
            dttmpColorway.Columns.Add("FNHSysMatSizeId", GetType(Integer))
            dttmpColorway.Columns.Add("FNSieBreakDownSeq", GetType(Integer))

            '' Delete Size
            _Qry = " DELETE FROM A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_SizeBreakDown  AS  A "
            _Qry &= vbCrLf & "  RIGHT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_Mat AS B ON A.FNHSysStyleId=B.FNHSysStyleId AND A.FNSeq=B.FNSeq"
            _Qry &= vbCrLf & " WHERE B.FNHSysStyleId =" & Val(FNHSysStyleId.Properties.Tag.ToString())
            _Qry &= vbCrLf & " AND B.FNHSysSeasonId =" & Val(FNHSysSeasonId.Properties.Tag.ToString())
            _Qry &= vbCrLf & " AND A.FNHSysStyleId IS NULL "

            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Qry = " DELETE FROM A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_SizeBreakDown  AS  A "
            _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_Mat AS B ON A.FNHSysStyleId=B.FNHSysStyleId AND A.FNSeq=B.FNSeq"
            _Qry &= vbCrLf & " WHERE B.FNHSysStyleId =" & Val(FNHSysStyleId.Properties.Tag.ToString())
            _Qry &= vbCrLf & " AND B.FNHSysSeasonId =" & Val(FNHSysSeasonId.Properties.Tag.ToString())
            _Qry &= vbCrLf & " AND B.FTStateActive ='1' "

            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            ' Add parameters and set values.
            If dt.Rows.Count = 0 Then Return True
            For Each r As DataRow In dt.Rows
                ' Dim SelectCMD As SqlCommand = New SqlCommand(dataAdapter.SelectCommand.CommandText, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
                Dim CIndex As Integer = 0
                Dim Cols As Integer = dt.Columns.Count
                Dim Colx As Integer = 0
                For Each c As DataColumn In dt.Columns
                    Colx += 1
                    If dt.Columns.IndexOf(c) > 15 Then
                        If dt.Columns.IndexOf(c) Mod 2 = 1 Then
                            CIndex += 1

                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TMERTThreadStyle_SizeBreakDown] "
                            _Qry &= vbCrLf & "    ([FNHSysStyleId], [FNSeq], [FNMerMatSeq], [FNSieBreakDownSeq], [FTRunSize], [FNHSysRawMatSizeId], FNHSysMatSizeId, "
                            _Qry &= vbCrLf & "   [FTInsUser],[FDInsDate],[FTInsTime]) "
                            _Qry &= vbCrLf & "  SELECT " & Val(r.Item("FNHSysStyleId").ToString) & ""
                            _Qry &= vbCrLf & " , " & Val(r.Item("FNSeq").ToString)
                            _Qry &= vbCrLf & " , " & Val(r.Item("FNMerMatSeq").ToString)
                            _Qry &= vbCrLf & "  ," & CIndex
                            _Qry &= vbCrLf & " , '" & HI.UL.ULF.rpQuoted(r.Item("FTRunSize").ToString) & "'"

                            _Qry &= vbCrLf & "  ," & Val(r.Item(c.ColumnName.ToString).ToString)
                            _Qry &= vbCrLf & "  ," & Val(Views.Columns(c.ColumnName.ToString).Tag.ToString)
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Qry &= vbCrLf & " , " & HI.UL.ULDate.FormatTimeDB

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If



                        End If
                    End If
                Next
            Next


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)



            HI.Auditor.CreateLog.CreateLogBomSheetStyleBreakDown(Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)), Integer.Parse(Val(FNHSysSeasonId.Properties.Tag.ToString)))

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


            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            objspl.UpdateInformation("Deleting Style No " & FNHSysStyleId.Properties.Tag.ToString() & " Season " & FNHSysSeasonId.Text.Trim)

            '' Delete Color
            _Str = " DELETE  FROM  A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_ColorWay AS A "
            _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_Mat AS B ON A.FNHSysStyleId=B.FNHSysStyleId AND A.FNSeq=B.FNSeq"
            _Str &= vbCrLf & " WHERE B.FNHSysStyleId =" & Val(FNHSysStyleId.Properties.Tag.ToString())
            _Str &= vbCrLf & " AND B.FNHSysSeasonId =" & Val(FNHSysSeasonId.Properties.Tag.ToString())
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            '' Delete Size
            _Str = " DELETE  FROM A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_SizeBreakDown  AS  A "
            _Str &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_Mat AS B ON A.FNHSysStyleId=B.FNHSysStyleId AND A.FNSeq=B.FNSeq"
            _Str &= vbCrLf & " WHERE B.FNHSysStyleId =" & Val(FNHSysStyleId.Properties.Tag.ToString())
            _Str &= vbCrLf & " AND B.FNHSysSeasonId =" & Val(FNHSysSeasonId.Properties.Tag.ToString())
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            '' Delete detail  
            _Str = " DELETE FROM B FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TMERTThreadStyle_Mat AS B "
            _Str &= vbCrLf & " WHERE B.FNHSysStyleId =" & Val(FNHSysStyleId.Properties.Tag.ToString())
            _Str &= vbCrLf & " AND B.FNHSysSeasonId =" & Val(FNHSysSeasonId.Properties.Tag.ToString())
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

    Private Function DoDeleteSource(ByVal dataAdapter As SqlDataAdapter,
    ByVal dataTable As System.Data.DataTable, r As Double, ByVal TableIndexx As Integer) As Boolean

        If dataTable.Rows.Count = 0 Then Return True
        Dim _DtRowIndex As Integer
        For Each Rx As DataRow In dataTable.Select("FNSeq=" & r & "")
            _DtRowIndex = dataTable.Rows.IndexOf(Rx)
            Exit For
        Next


        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
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
            SelectCMD.Parameters.AddWithValue("@FNHSysStyleId", Val(FNHSysStyleId.Properties.Tag.ToString.ToString))
            SelectCMD.Parameters.AddWithValue("@FNSeq", r)

            Dim cnt As Integer
            cnt = SelectCMD.ExecuteScalar
            If cnt = 0 Then
                If dataTable.Rows(_DtRowIndex).RowState <> DataRowState.Deleted Then
                    dataTable.Rows(_DtRowIndex).Delete()
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
                DeleteCmd.Parameters.AddWithValue("@FNHSysStyleId", Val(FNHSysStyleId.Properties.Tag.ToString.ToString))
                DeleteCmd.Parameters.AddWithValue("@FNSeq", r)

                DeleteCmd.CommandType = CommandType.Text
                DeleteCmd.ExecuteNonQuery()
                DeleteCmd.Parameters.Clear()

                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                If dataTable.Rows(_DtRowIndex).RowState <> DataRowState.Deleted Then
                    dataTable.Rows(_DtRowIndex).Delete()
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
    Public Function CreateAdapter(
    ByVal connection As SqlConnection) As SqlDataAdapter

        Dim adapter As SqlDataAdapter = New SqlDataAdapter()

        ' Create the SelectCommand. 
        Dim command As SqlCommand = New SqlCommand(
            "SELECT FNHSysStyleId, FNSeq, FNMerMatSeq, FNHSysMerMatId, FNPart FROM " &
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TMERTThreadStyle_Mat] " &
            "WHERE FNHSysStyleId = @FNHSysStyleId AND FNSeq = @FNSeq", connection)

        ' Add the parameters for the SelectCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int)
        command.Parameters.Add("@FNSeq", SqlDbType.Int)

        adapter.SelectCommand = command

        ' Create the InsertCommand.
        command = New SqlCommand(
            "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TMERTThreadStyle_Mat] " &
            "([FNHSysStyleId], [FNSeq], [FNMerMatSeq], [FNHSysMerMatId], [FNPart], " &
            "[FTPositionPartName],[FNHSysSuplId],[FTStateNominate],[FNHSysUnitId],[FNPrice],[FNHSysCurId], " &
            "[FNConSmp],[FNConSmpPlus],[FTOrderNo],[FTSubOrderNo],[FTStateActive],[FTStateCombination], FTStateMainMaterial, " &
            "[FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser],[FDUpdDate],[FTUpdTime],[FTPositionPartId],[FTPart],[FTComponent],[FTStateFree],[FNHSysSeasonId]) " &
            "VALUES (@FNHSysStyleId, @FNSeq, @FNMerMatSeq, @FNHSysMerMatId, @FNPart, " &
            "@FTPositionPartName, @FNHSysSuplId, @FTStateNominate, @FNHSysUnitId, @FNPrice, @FNHSysCurId, " &
            "@FNConSmp, @FNConSmpPlus, @FTOrderNo, @FTSubOrderNo, @FTStateActive, @FTStateCombination, @FTStateMainMaterial, " &
            "@FTInsUser, @FDInsDate, @FTInsTime, @FTUpdUser, @FDUpdDate, @FTUpdTime,@FTPositionPartId,@FTPart,@FTComponent,@FTStateFree,@FNHSysSeasonId)", connection)

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
        command.Parameters.Add("@FTPositionPartId", SqlDbType.NChar, 500, "FTPositionPartId")
        command.Parameters.Add("@FTPart", SqlDbType.NChar, 30, "FTPart")
        command.Parameters.Add("@FTComponent", SqlDbType.NChar, 500, "FTComponent")
        command.Parameters.Add("@FTStateFree", SqlDbType.VarChar, 1, "FTStateFree")
        command.Parameters.Add("@FNHSysSeasonId", SqlDbType.Int, 8, "FNHSysSeasonId")
        adapter.InsertCommand = command

        ' Create the UpdateCommand.
        command = New SqlCommand(
            "UPDATE TMERTThreadStyle_Mat SET " &
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
            "FTUpdTime = @FTUpdTime, " &
            "FTPositionPartId = @FTPositionPartId, " &
            "FTPart = @FTPart, " &
            "FTComponent = @FTComponent, " &
            "FTStateFree = @FTStateFree, " &
            "FNHSysSeasonId = @FNHSysSeasonId " &
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
        command.Parameters.Add("@FTPositionPartId", SqlDbType.NChar, 500, "FTPositionPartId")
        command.Parameters.Add("@FTPart", SqlDbType.NChar, 30, "FTPart")
        command.Parameters.Add("@FTComponent", SqlDbType.NChar, 500, "FTComponent")
        command.Parameters.Add("@FTStateFree", SqlDbType.VarChar, 1, "FTStateFree")
        command.Parameters.Add("@FNHSysSeasonId", SqlDbType.Int, 8, "FNHSysSeasonId")

        Dim parameter As SqlParameter = command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int, 64, "FNHSysStyleId") 'old id
        parameter.SourceVersion = DataRowVersion.Original

        adapter.UpdateCommand = command

        ' Create the DeleteCommand.
        command = New SqlCommand(
            "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TMERTThreadStyle_Mat] " &
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
        Dim command As SqlCommand = New SqlCommand(
            "SELECT [FNHSysStyleId],[FNSeq],[FNMerMatSeq],[FNColorWaySeq],[FTRunColor],[FNHSysRawMatColorId], [FNHSysMatColorId], " &
            "[FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser],[FDUpdDate],[FTUpdTime] FROM " &
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TMERTThreadStyle_ColorWay] " &
            "WHERE (FNHSysStyleId = @FNHSysStyleId AND FNSeq = @FNSeq " &
            "AND FNColorWaySeq = @FNColorWaySeq " &
            ") " &
            "OR (FNHSysStyleId = @FNHSysStyleId AND FNSeq = -1)", connection)

        ' Add the parameters for the SelectCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int)
        command.Parameters.Add("@FNSeq", SqlDbType.Int)
        command.Parameters.Add("@FNColorWaySeq", SqlDbType.Int)

        adapter.SelectCommand = command

        ' Create the InsertCommand.
        command = New SqlCommand(
            "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TMERTThreadStyle_ColorWay] " &
            "([FNHSysStyleId],[FNSeq],[FNMerMatSeq],[FNColorWaySeq],[FTRunColor],[FNHSysRawMatColorId], [FNHSysMatColorId], " &
            "[FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser],[FDUpdDate],[FTUpdTime],[FTRawMatColorNameTH],[FTRawMatColorNameEN]) " &
            "VALUES (@FNHSysStyleId, @FNSeq, @FNMerMatSeq, @FNColorWaySeq, @FTRunColor, @FNHSysRawMatColorId, @FNHSysMatColorId, " &
            "@FTInsUser, @FDInsDate, @FTInsTime, @FTUpdUser, @FDUpdDate, @FTUpdTime,@FTRawMatColorNameTH,@FTRawMatColorNameEN)", connection)

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
        command.Parameters.Add("@FTRawMatColorNameTH", SqlDbType.NChar, 200, "FTRawMatColorNameTH")
        command.Parameters.Add("@FTRawMatColorNameEN", SqlDbType.NChar, 200, "FTRawMatColorNameEN")

        adapter.InsertCommand = command

        ' Create the UpdateCommand.
        command = New SqlCommand(
            "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TMERTThreadStyle_ColorWay] SET " &
            "FNHSysStyleId          = @FNHSysStyleId, " &
            "FNSeq                  = @FNSeq, " &
            "FNMerMatSeq            = @FNMerMatSeq, " &
            "FNColorWaySeq          = @FNColorWaySeq, " &
            "FTRunColor             = @FTRunColor, " &
            "FNHSysRawMatColorId    = @FNHSysRawMatColorId, " &
            "FNHSysMatColorId       = @FNHSysMatColorId, " &
            "FTUpdUser              = @FTUpdUser, " &
            "FDUpdDate              = @FDUpdDate, " &
            "FTUpdTime              = @FTUpdTime, " &
            "FTRawMatColorNameTH    = @FTRawMatColorNameTH, " &
            "FTRawMatColorNameEN    = @FTRawMatColorNameEN " &
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
        command.Parameters.Add("@FTRawMatColorNameTH", SqlDbType.NChar, 200, "FTRawMatColorNameTH")
        command.Parameters.Add("@FTRawMatColorNameEN", SqlDbType.NChar, 200, "FTRawMatColorNameEN")

        Dim parameter As SqlParameter
        parameter = command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int, 8, "FNHSysStyleId")
        parameter = command.Parameters.Add("@FNSeq", SqlDbType.Int, 8, "FNSeq")
        parameter = command.Parameters.Add("@FNColorWaySeq", SqlDbType.Int, 8, "FNColorWaySeq")
        parameter.SourceVersion = DataRowVersion.Original

        adapter.UpdateCommand = command

        ' Create the DeleteCommand.
        command = New SqlCommand(
            "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TMERTThreadStyle_ColorWay] " &
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
        Dim command As SqlCommand = New SqlCommand(
            "SELECT [FNHSysStyleId], [FNSeq], [FNMerMatSeq], [FNSieBreakDownSeq], [FTRunSize], [FNHSysRawMatSizeId], [FNHSysMatSizeId], " &
            "[FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser],[FDUpdDate],[FTUpdTime] FROM " &
            "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TMERTThreadStyle_SizeBreakDown] " &
            "WHERE (FNHSysStyleId = @FNHSysStyleId AND FNSeq = @FNSeq " &
            "AND FNSieBreakDownSeq = @FNSieBreakDownSeq " &
            ") " &
            "OR (FNHSysStyleId = @FNHSysStyleId AND FNSeq = -1)", connection)

        ' Add the parameters for the SelectCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int)
        command.Parameters.Add("@FNSeq", SqlDbType.Int)
        command.Parameters.Add("@FNColorWaySeq", SqlDbType.Int)

        adapter.SelectCommand = command

        ' Create the InsertCommand.
        command = New SqlCommand(
            "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TMERTThreadStyle_SizeBreakDown] " &
            "([FNHSysStyleId], [FNSeq], [FNMerMatSeq], [FNSieBreakDownSeq], [FTRunSize], [FNHSysRawMatSizeId], FNHSysMatSizeId, " &
            "[FTInsUser],[FDInsDate],[FTInsTime],[FTUpdUser],[FDUpdDate],[FTUpdTime]) " &
            "VALUES (@FNHSysStyleId, @FNSeq, @FNMerMatSeq, @FNSieBreakDownSeq, @FTRunSize, @FNHSysRawMatSizeId, @FNHSysMatSizeId, " &
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
        command = New SqlCommand(
            "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TMERTThreadStyle_SizeBreakDown] SET " &
            "FNHSysStyleId          = @FNHSysStyleId, " &
            "FNSeq                  = @FNSeq, " &
            "FNMerMatSeq            = @FNMerMatSeq, " &
            "FNSieBreakDownSeq      = @FNSieBreakDownSeq, " &
            "FTRunSize              = @FTRunSize, " &
            "FNHSysRawMatSizeId     = @FNHSysRawMatSizeId, " &
            "FNHSysMatSizeId        = @FNHSysMatSizeId, " &
            "FTUpdUser              = @FTUpdUser, " &
            "FDUpdDate              = @FDUpdDate, " &
            "FTUpdTime              = @FTUpdTime " &
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
        command = New SqlCommand(
            "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].[dbo].[TMERTThreadStyle_SizeBreakDown] " &
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

        Dim _Qry As String = ""

        _Qry = " SELECT  FNHSysStyleId,FNMatColorSeq,FTColorway,FNHSysMatColorId "
        _Qry &= vbCrLf & "  FROM ( SELECT     O.FNHSysStyleId, C.FNMatColorSeq, C.FTMatColorCode AS FTColorway, B.FNHSysMatColorId "
        _Qry &= vbCrLf & "  FROM "
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder AS O WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder_BreakDown AS B WITH(NOLOCK) ON O.FTOrderNo = B.FTOrderNo INNER JOIN  "
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TMERMMatColor] AS C WITH(NOLOCK)  ON B.FNHSysMatColorId = C.FNHSysMatColorId "
        _Qry &= vbCrLf & "  WHERE (O.FNHSysStyleId = @FNHSysStyleId AND O.FNHSysSeasonId = CASE WHEN @FNHSysSeasonId <=0 THEN  O.FNHSysSeasonId ELSE @FNHSysSeasonId END) "
        _Qry &= vbCrLf & "  GROUP BY  O.FNHSysStyleId, C.FNMatColorSeq, C.FTMatColorCode , B.FNHSysMatColorId "
        _Qry &= vbCrLf & "  UNION "
        _Qry &= vbCrLf & "  SELECT        A.FNHSysStyleId, B.FNMatColorSeq, B.FTMatColorCode AS FTColorway, B.FNHSysMatColorId"
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTStyle_ColorWay AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TMERMMatColor AS B WITH(NOLOCK) ON A.FNHSysMatColorId = B.FNHSysMatColorId"
        _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTStyle_Mat AS TA WITH(NOLOCK) ON A.FNHSysStyleId = TA.FNHSysStyleId AND A.FNSeq=TA.FNSeq "
        _Qry &= vbCrLf & "  WHERE (A.FNHSysStyleId = @FNHSysStyleId AND TA.FNHSysSeasonId = @FNHSysSeasonId   AND (TA.FTStateActive = '1') ) "
        _Qry &= vbCrLf & "  GROUP BY  A.FNHSysStyleId, B.FNMatColorSeq, B.FTMatColorCode, B.FNHSysMatColorId "
        _Qry &= vbCrLf & "   ) AS A"
        _Qry &= vbCrLf & "  GROUP BY  FNHSysStyleId, FNMatColorSeq, FTColorway, FNHSysMatColorId "
        _Qry &= vbCrLf & "  ORDER BY FNHSysStyleId, FNMatColorSeq "

        ' Create the SelectCommand. 
        Dim command As SqlCommand = New SqlCommand(_Qry, connection)

        ' Add the parameters for the SelectCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int)
        command.Parameters.Add("@FNHSysSeasonId", SqlDbType.Int)
        adapter.SelectCommand = command

        Return adapter
    End Function

    Public Function CreateAdapterImportSize(ByVal connection As SqlConnection) As SqlDataAdapter
        Dim adapter As SqlDataAdapter = New SqlDataAdapter()
        Dim _Qry As String = ""

        _Qry = " SELECT  FNHSysStyleId,FNMatSizeSeq,FTSizeBreakDown,FNHSysMatSizeId "
        _Qry &= vbCrLf & " FROM "
        _Qry &= vbCrLf & " ( SELECT  O.FNHSysStyleId, C.FNMatSizeSeq, C.FTMatSizeCode AS FTSizeBreakDown, B.FNHSysMatSizeId "
        _Qry &= vbCrLf & " FROM "
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder AS O  WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTOrder_BreakDown AS B  WITH(NOLOCK)  ON O.FTOrderNo = B.FTOrderNo INNER JOIN  "
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TMERMMatSize] AS C  WITH(NOLOCK)  ON B.FNHSysMatSizeId = C.FNHSysMatSizeId "
        _Qry &= vbCrLf & " WHERE  (O.FNHSysStyleId = @FNHSysStyleId AND O.FNHSysSeasonId = CASE WHEN @FNHSysSeasonId <=0 THEN  O.FNHSysSeasonId ELSE @FNHSysSeasonId END) "
        _Qry &= vbCrLf & " GROUP BY  O.FNHSysStyleId, C.FNMatSizeSeq, C.FTMatSizeCode, B.FNHSysMatSizeId "
        _Qry &= vbCrLf & "  UNION "
        _Qry &= vbCrLf & "  SELECT        A.FNHSysStyleId, B.FNMatSizeSeq, B.FTMatSizeCode AS  FTSizeBreakDown, B.FNHSysMatSizeId"
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTStyle_SizeBreakDown AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TMERMMatSize AS B WITH(NOLOCK) ON A.FNHSysMatSizeId = B.FNHSysMatSizeId"
        _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].TMERTStyle_Mat AS TA WITH(NOLOCK) ON A.FNHSysStyleId = TA.FNHSysStyleId AND A.FNSeq=TA.FNSeq "
        _Qry &= vbCrLf & "  WHERE (A.FNHSysStyleId = @FNHSysStyleId  AND TA.FNHSysSeasonId = @FNHSysSeasonId  AND (TA.FTStateActive = '1')  ) "
        _Qry &= vbCrLf & "  GROUP BY   A.FNHSysStyleId, B.FNMatSizeSeq, B.FTMatSizeCode   , B.FNHSysMatSizeId "
        _Qry &= vbCrLf & "   ) AS A"
        _Qry &= vbCrLf & "  GROUP BY  FNHSysStyleId,FNMatSizeSeq,FTSizeBreakDown,FNHSysMatSizeId "
        _Qry &= vbCrLf & " ORDER BY FNHSysStyleId,FNMatSizeSeq,FTSizeBreakDown,FNHSysMatSizeId"

        ' Create the SelectCommand. 

        Dim command As SqlCommand = New SqlCommand(_Qry, connection)

        ' Add the parameters for the SelectCommand.
        command.Parameters.Add("@FNHSysStyleId", SqlDbType.Int)
        command.Parameters.Add("@FNHSysSeasonId", SqlDbType.Int)

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
            If c.FieldName.ToString.ToUpper = "FTSelect".ToUpper Then
                c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            Else
                If c.AbsoluteIndex < 13 Then
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
                End If
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

    Private Sub RepFTPositionPartName_QueryCloseUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RepFTPositionPartName.QueryCloseUp
        'Try
        '    With Me.GridView1
        '        If .FocusedRowHandle < 0 Then
        '            Exit Sub
        '        End If

        '        Me.FTPart.Focus()
        '        Me.FTPart.SelectAll()

        '        .SetFocusedRowCellValue("FTPart", Me.FTPart.Text)

        '        Dim _PartName As String = ""
        '        Dim _PartIDKey As String = ""

        '        With CType(Me.ogcpart.DataSource, DataTable)
        '            .AcceptChanges()
        '            For Each R As DataRow In .Select("FTSelect='1'")

        '                If _PartName = "" Then
        '                    _PartName = R!FTPartName.ToString
        '                    _PartIDKey = R!FNHSysPartId.ToString
        '                Else
        '                    _PartName = _PartName & "," & R!FTPartName.ToString
        '                    _PartIDKey = _PartIDKey & "|" & R!FNHSysPartId.ToString
        '                End If

        '            Next

        '        End With

        '        If Me.FTPart.Text <> "" Then
        '            _PartName = Me.FTPart.Text & ":" & _PartName
        '        End If

        '        .SetFocusedRowCellValue("FTPositionPartName", _PartName)
        '        .SetFocusedRowCellValue("FTPositionPartId", _PartIDKey)

        '        Try
        '            ' If "" & .GetFocusedRowCellValue("FTComponent").ToString = "" Or _PartName <> "" Then
        '            If "" & .GetFocusedRowCellValue("FTComponent").ToString.Trim() = "" Then
        '                .SetFocusedRowCellValue("FTComponent", _PartName)
        '            End If
        '        Catch ex As Exception
        '        End Try

        '        'CType(Me.ogcpart.DataSource, DataTable).AcceptChanges()

        '    End With
        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub RepFTPositionPartName_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles RepFTPositionPartName.QueryPopUp
        'Try

        '    Dim _PartKey As String = ""
        '    Dim _PartIDKey As String = ""
        '    With Me.GridView1
        '        If .FocusedRowHandle < 0 Then
        '            Exit Sub
        '        End If
        '        _PartKey = "" & .GetFocusedRowCellValue("FTPart").ToString
        '        _PartIDKey = "" & .GetFocusedRowCellValue("FTPositionPartId").ToString
        '    End With

        '    ogvpart.ClearColumnsFilter()
        '    ogvpart.ActiveFilter.Clear()

        '    Me.ogcpart.DataSource = _dtpart(0).Copy
        '    ogvpart.Columns.ColumnByFieldName("FTSelect").Width = 40
        '    ogvpart.Columns.ColumnByFieldName("FTPartName").Width = 150

        '    Me.FTPart.Text = _PartKey
        '    With CType(Me.ogcpart.DataSource, DataTable)
        '        For Each Str As String In _PartIDKey.Split("|")
        '            For Each R As DataRow In .Select("FNHSysPartId=" & Integer.Parse(Val(Str)) & "")
        '                R!FTSelect = "1"
        '                Exit For
        '            Next
        '        Next
        '        .AcceptChanges()
        '    End With
        'Catch ex As Exception
        'End Try

    End Sub

    Private Sub TabChange()

        _StateSelectAll = True

        ocmbomaddnew.Visible = (otb.SelectedTabPage.Name = otpmatcode.Name)
        ocmbomdeleterow.Visible = (otb.SelectedTabPage.Name = otpmatcode.Name)
        ocmbomdiffpart.Visible = (otb.SelectedTabPage.Name = otpmatcode.Name)
        ocmbominsertrow.Visible = (otb.SelectedTabPage.Name = otpmatcode.Name)


        _StateSelectAll = False


        HI.TL.METHOD.CallActiveToolBarFunction(Me)

    End Sub

    Private Sub otb_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otb.SelectedPageChanged
        Call TabChange()
    End Sub

    Private Sub ocmbomdeletesize_Click(sender As Object, e As EventArgs)
        If CheckOwner() = False Then
            Exit Sub
        End If
        'View = Me.ogcstylecolor.Views(0)

        'If (Not IsNothing(View)) Then
        '    RowsIndex = View.FocusedRowHandle
        '    TopVisibleIndex = View.TopRowIndex
        'End If

        'If RowsIndex < 0 Then Return

        'Dim dtStyleDetail As DataTable = CType(ogcstylecolor.DataSource, DataTable)

        'If Not (View.PostEditor() And View.UpdateCurrentRow()) Then Return

        ''Delete row of Color way
        'If DoDeleteSource(oleDbDataAdapter2, dtStyleDetail, RowsIndex, TabIndexs.Colorway) = False Then Return
        If FNHSysStyleId.Properties.Tag.ToString = "" Or FNHSysSeasonId.Properties.Tag.ToString = "" Then Return
        With Me.GridView3
            If .FocusedRowHandle < 0 Then Exit Sub
            If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatSizeCode".Length) = "FTRawMatSizeCode".ToUpper Then
                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบ Size ใช่หรือไม่ ?", 1406030077, .FocusedColumn.Caption) Then
                    Dim Col1 As String = .FocusedColumn.FieldName.ToString
                    Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)
                    Dim dt As DataTable = CType(Me.ogcstylesize.DataSource, DataTable)
                    Try
                        GridView3.Columns.Remove(GridView3.Columns.ColumnByFieldName(Col1))
                        dt.Columns.Remove(Col1)
                    Catch ex As Exception
                    End Try

                    Try
                        GridView3.Columns.Remove(GridView3.Columns.ColumnByFieldName(Col2))
                        dt.Columns.Remove(Col2)
                    Catch ex As Exception
                    End Try
                    CType(Me.ogcstylesize.DataSource, DataTable).AcceptChanges()

                    HI.Auditor.CreateLog.CreateLogDelete(HI.ST.SysInfo.MenuName, Me.Name, " DELETE Size Breakdown " & .FocusedColumn.Caption & " (Style " & FNHSysStyleId.Text & " Season " & FNHSysSeasonId.Text & ") ")


                End If
            End If

        End With
    End Sub

#Region "Handle Control"

    Private Sub DynamicButtoneditColor_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.F7

                With CType(sender, DevExpress.XtraEditors.ButtonEdit)

                    If .Properties.Buttons.Count >= 1 Then
                        If (.Properties.Buttons.Item(0).Visible) Then
                            .PerformClick(.Properties.Buttons.Item(0))
                        End If
                    End If
                End With

            Case Keys.F1



            Case Keys.F10, Keys.F11, Keys.F12

                Try
                    With Me.GridView2
                        If .FocusedRowHandle < 0 Then Exit Sub
                        Select Case e.KeyCode
                            Case Keys.F10

                                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                                    If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatColorCode")) = "FTRawMatColorCode".ToUpper Then

                                        Dim Col1 As String = GridCol.FieldName.ToString
                                        Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                                        .SetFocusedRowCellValue(GridCol.FieldName, "N/R")
                                        .SetFocusedRowCellValue(Col2, "-1")

                                    End If

                                Next

                                CType(ogcstylecolor.DataSource, DataTable).AcceptChanges()
                                ogcstylecolor.RefreshDataSource()

                            Case Keys.F11

                                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                                    If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatColorCode")) = "FTRawMatColorCode".ToUpper Then

                                        Dim Col1 As String = GridCol.FieldName.ToString
                                        Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                                        .SetFocusedRowCellValue(GridCol.FieldName, "")
                                        .SetFocusedRowCellValue(Col2, "0")

                                    End If

                                Next

                                CType(ogcstylecolor.DataSource, DataTable).AcceptChanges()
                                ogcstylecolor.RefreshDataSource()

                            Case Keys.F12

                                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns

                                    If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatColorCode")) = "FTRawMatColorCode".ToUpper Then

                                        Dim Code As String = GridCol.Caption
                                        Dim _ColorInt As Integer = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysRawMatColorId FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor WITH(NOLOCK) WHERE FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(Code) & "'  ", Conn.DB.DataBaseName.DB_MASTER, "0")))

                                        If _ColorInt > 0 Then

                                            Dim Col1 As String = GridCol.FieldName.ToString
                                            Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                                            .SetFocusedRowCellValue(GridCol.FieldName, Code)
                                            .SetFocusedRowCellValue(Col2, _ColorInt.ToString)

                                        End If

                                    End If

                                Next

                                CType(ogcstylecolor.DataSource, DataTable).AcceptChanges()
                                ogcstylecolor.RefreshDataSource()

                        End Select
                    End With
                Catch ex As Exception
                End Try

            Case Keys.F9

                Try
                    With Me.GridView2
                        If .FocusedRowHandle < 0 Then Exit Sub
                        Dim _VisibleIndex As Integer = .FocusedColumn.VisibleIndex

                        Dim Code As String = ""
                        With CType(sender, DevExpress.XtraEditors.ButtonEdit)
                            Code = .Text
                        End With

                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatColorCode")) = "FTRawMatColorCode".ToUpper Then
                                If GridCol.VisibleIndex > _VisibleIndex Then


                                    Dim _ColorInt As Integer = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysRawMatColorId FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor WITH(NOLOCK) WHERE FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(Code) & "'  ", Conn.DB.DataBaseName.DB_MASTER, "0")))

                                    If _ColorInt > 0 Then
                                        Dim Col1 As String = GridCol.FieldName.ToString
                                        Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                                        .SetFocusedRowCellValue(GridCol.FieldName, Code)
                                        .SetFocusedRowCellValue(Col2, _ColorInt.ToString)

                                    End If
                                End If

                            End If
                        Next

                        CType(ogcstylecolor.DataSource, DataTable).AcceptChanges()
                        ogcstylecolor.RefreshDataSource()

                    End With
                Catch ex As Exception
                End Try

            Case Keys.F6 ' (Keys.Alt + Keys.F12)
                With Me.GridView2
                    If .FocusedRowHandle < 0 Then Exit Sub

                    For Ridx As Integer = .FocusedRowHandle To .RowCount - 1
                        Dim Col1 As String = .FocusedColumn.FieldName.ToString
                        Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                        .SetRowCellValue(Ridx, Col1, "N/R")
                        .SetRowCellValue(Ridx, Col2, "-1")

                    Next

                    CType(ogcstylecolor.DataSource, DataTable).AcceptChanges()
                    ogcstylecolor.RefreshDataSource()

                End With
            Case Else

        End Select
    End Sub

    Private Sub DynamicButtoneditSize_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.F7

                With CType(sender, DevExpress.XtraEditors.ButtonEdit)

                    If .Properties.Buttons.Count >= 1 Then
                        If (.Properties.Buttons.Item(0).Visible) Then
                            .PerformClick(.Properties.Buttons.Item(0))
                        End If
                    End If
                End With
            Case Keys.F10, Keys.F11, Keys.F12
                Try
                    With Me.GridView3
                        If .FocusedRowHandle < 0 Then Exit Sub
                        Select Case e.KeyCode
                            Case Keys.F10

                                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                                    If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatSizeCode")) = "FTRawMatSizeCode".ToUpper Then

                                        Dim Col1 As String = GridCol.FieldName.ToString
                                        Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                        .SetFocusedRowCellValue(GridCol.FieldName, "N/R")
                                        .SetFocusedRowCellValue(Col2, "-1")

                                    End If
                                Next

                                CType(ogcstylesize.DataSource, DataTable).AcceptChanges()
                                ogcstylesize.RefreshDataSource()

                            Case Keys.F11
                                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                                    If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatSizeCode")) = "FTRawMatSizeCode".ToUpper Then
                                        Dim Col1 As String = GridCol.FieldName.ToString
                                        Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                        .SetFocusedRowCellValue(GridCol.FieldName, "")
                                        .SetFocusedRowCellValue(Col2, "0")

                                    End If
                                Next

                                CType(ogcstylesize.DataSource, DataTable).AcceptChanges()
                                ogcstylesize.RefreshDataSource()
                            Case Keys.F12
                                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                                    If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatSizeCode")) = "FTRawMatSizeCode".ToUpper Then

                                        Dim Code As String = GridCol.Caption
                                        Dim _SizeInt As Integer = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysRawMatSizeId FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize WITH(NOLOCK) WHERE FTRawMatSizeCode='" & HI.UL.ULF.rpQuoted(Code) & "'  ", Conn.DB.DataBaseName.DB_MASTER, "0")))

                                        If _SizeInt > 0 Then
                                            Dim Col1 As String = GridCol.FieldName.ToString
                                            Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                            .SetFocusedRowCellValue(GridCol.FieldName, Code)
                                            .SetFocusedRowCellValue(Col2, _SizeInt.ToString)

                                        End If


                                    End If
                                Next

                                CType(ogcstylesize.DataSource, DataTable).AcceptChanges()
                                ogcstylesize.RefreshDataSource()
                        End Select
                    End With
                Catch ex As Exception

                End Try
            Case Keys.F9
                Try
                    With Me.GridView3
                        If .FocusedRowHandle < 0 Then Exit Sub
                        Dim _VisibleIndex As Integer = .FocusedColumn.VisibleIndex

                        Dim Code As String = ""
                        With CType(sender, DevExpress.XtraEditors.ButtonEdit)
                            Code = .Text
                        End With

                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatSizeCode")) = "FTRawMatSizeCode".ToUpper Then

                                If GridCol.VisibleIndex > _VisibleIndex Then

                                    Dim _SizeInt As Integer = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysRawMatSizeId FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize WITH(NOLOCK) WHERE FTRawMatSizeCode='" & HI.UL.ULF.rpQuoted(Code) & "'  ", Conn.DB.DataBaseName.DB_MASTER, "0")))

                                    If _SizeInt > 0 Then
                                        Dim Col1 As String = GridCol.FieldName.ToString
                                        Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                        .SetFocusedRowCellValue(GridCol.FieldName, Code)
                                        .SetFocusedRowCellValue(Col2, _SizeInt.ToString)

                                    End If
                                End If

                            End If
                        Next

                        CType(ogcstylesize.DataSource, DataTable).AcceptChanges()
                        ogcstylesize.RefreshDataSource()

                    End With
                Catch ex As Exception
                End Try

            Case Keys.F6 '(Keys.Alt + Keys.F12)
                With Me.GridView3
                    If .FocusedRowHandle < 0 Then Exit Sub

                    For Ridx As Integer = .FocusedRowHandle To .RowCount - 1
                        Dim Col1 As String = .FocusedColumn.FieldName.ToString
                        Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                        .SetRowCellValue(Ridx, Col1, "N/R")
                        .SetRowCellValue(Ridx, Col2, "-1")

                    Next

                    CType(ogcstylesize.DataSource, DataTable).AcceptChanges()
                    ogcstylesize.RefreshDataSource()
                End With

            Case Else

        End Select
    End Sub

    Private Sub DynamicResponButtoneSysHide_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Select Case e.Button.Index
            Case 0

                If Val(e.Button.Tag.ToString) <= 0 Then Exit Sub

                If Not (_StateProc) Then
                    _StateProc = True
                    Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
                    With New HI.TL.wDynamicBrowseInfo(Val(e.Button.Tag.ToString), _form)
                        .BrowseID = Val(e.Button.Tag.ToString)

                        .X = MousePosition.X
                        .Y = MousePosition.Y

                        Dim _Qrysql As String = ""
                        Dim _dtbrw As New DataTable
                        Dim _dtret As New DataTable
                        Dim _BrowseCmd As String = ""
                        Dim _BrowseSortCmd As String = ""
                        Dim _BrowseWhereCmd As String = ""
                        Dim _FTBrwCmdField As String = ""
                        Dim _FTBrwCmdFieldOptional As String = ""
                        Dim FTBrwCmdGroupBy As String = ""
                        Dim _Where As String = ""
                        Dim _ConFiledName As String = ""
                        _Where = HI.TL.HandlerControl.GridViewGetBrowseRespon(sender, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView), .BrowseID, _dtret, _BrowseCmd, _BrowseSortCmd, _BrowseWhereCmd, _FTBrwCmdField, _FTBrwCmdFieldOptional, FTBrwCmdGroupBy, _Qrysql, "", "")

                        If _Qrysql = "" Then
                            _StateProc = False
                            Exit Sub
                        End If

                        _dtbrw = New DataTable
                        _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql & "  " & _BrowseWhereCmd & "  " & _Where & "  " & FTBrwCmdGroupBy & "  " & _BrowseSortCmd, Conn.DB.DataBaseName.DB_SYSTEM)

                        .Data = _dtbrw.Copy
                        .DataRetField = _dtret.Copy

                        _dtbrw.Dispose()
                        _dtret.Dispose()

                        .ShowDialog()

                        If Not (.ValuesReturn Is Nothing) Then

                            For Each Row As DataRow In .DataRetField.Select("NOT(FTRetField IS NULL)")
                                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                                    Dim _ColName As String = Row!FTRetField.ToString

                                    If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                    Else
                                        _ColName = _ColName & "" & .FocusedColumn.Name.ToString

                                        If (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                            If Microsoft.VisualBasic.Left(Row!FTRetField, Row!FTRetField.ToString.Length) = Microsoft.VisualBasic.Left(.FocusedColumn.Name.ToString, Row!FTRetField.ToString.Length) Then
                                                _ColName = .FocusedColumn.Name.ToString
                                            End If
                                        End If
                                    End If

                                    If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                        Dim ctrl As Object = .Columns.ColumnByFieldName(_ColName).ColumnEdit

                                        If ctrl Is Nothing Then

                                            Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                Case "System.Int32".ToUpper
                                                    .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                Case "System.String".ToUpper
                                                    .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                Case Else
                                                    .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                            End Select
                                        Else
                                            If Row!FTStatePropertyTag.ToString = "Y" Then
                                                Try
                                                    If ctrl Is Nothing Then
                                                        Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                            Case "System.Int32".ToUpper
                                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                            Case "System.String".ToUpper
                                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                            Case Else
                                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                        End Select
                                                    Else

                                                        Try

                                                            If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatSizeCode".Length) = "FTRawMatSizeCode".ToUpper Then
                                                                Dim Col1 As String = .FocusedColumn.FieldName.ToString
                                                                Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                                                Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                                                                    Case "System.Int32".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Val(Row!ValuesRet.ToString))

                                                                    Case "System.String".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                    Case Else
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                End Select
                                                            ElseIf Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then
                                                                Dim Col1 As String = .FocusedColumn.FieldName.ToString
                                                                Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                                                                Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                                                                    Case "System.Int32".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Val(Row!ValuesRet.ToString))

                                                                    Case "System.String".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                    Case Else
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                End Select
                                                            Else
                                                                Select Case .Columns.ColumnByFieldName(_ColName & "_Hide").ColumnType.FullName.ToString.ToUpper
                                                                    Case "System.Int32".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Val(Row!ValuesRet.ToString))
                                                                    Case "System.String".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                                    Case Else
                                                                        .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                                End Select
                                                            End If
                                                        Catch ex As Exception
                                                        End Try

                                                        'Try
                                                        '    With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                                                        '        .Text = Row!ValuesRet.ToString
                                                        '    End With
                                                        'Catch ex As Exception
                                                        'End Try

                                                        Try
                                                            With CType(ctrl, DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit)
                                                                .Tag = Row!ValuesRet.ToString
                                                            End With
                                                        Catch ex As Exception
                                                        End Try

                                                    End If
                                                Catch ex As Exception
                                                End Try
                                            Else


                                                Try
                                                    With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                                                        .Text = Row!ValuesRet.ToString
                                                    End With
                                                Catch ex As Exception
                                                End Try

                                                Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                    Case "System.Int32".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                    Case "System.String".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    Case Else
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                End Select

                                            End If

                                        End If

                                    End If

                                End With
                            Next
                        End If

                        .Data.Dispose()
                        .DataRetField.Dispose()

                    End With

                    _StateProc = False

                    With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                        Try
                            Dim IC As Integer = 1
                            Do While .Columns(.FocusedColumn.AbsoluteIndex + IC).Visible = False
                                IC = IC + 1
                            Loop
                            .FocusedColumn = .Columns(.FocusedColumn.AbsoluteIndex + IC)
                        Catch ex As Exception
                        End Try

                    End With

                End If

        End Select
    End Sub

    Private _StateProc As Boolean
    Private Sub DynamicResponButtoneditSysHide_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender, DevExpress.XtraEditors.ButtonEdit)

            If Not (_StateProc) Then
                _StateProc = True

                Dim _BrowseID As Integer
                Dim _Name As String
                Dim _Data As String
                Dim _BrowseCmd As String = ""
                Dim _BrowseSortCmd As String = ""
                Dim _BrowseWhereCmd As String = ""
                Dim _FTBrwCmdField As String = ""
                Dim _FTBrwCmdFieldOptional As String = ""
                Dim FTBrwCmdGroupBy As String = ""
                Dim _ConFiledName As String = ""

                Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
                If IsNothing(_form) Then _form = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent

                With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)

                    _Name = .Name.ToString.Replace("Res_", "")

                    _Data = .Text
                    If .Properties.Buttons.Count > 1 Then
                        If UCase("" & .Properties.Buttons.Item(1).Tag) = UCase("d") Then
                            Dim T As System.Type = _form.GetType()

                            Dim _pdbnameinfo As PropertyInfo
                            Dim _ptablenameinfo As PropertyInfo
                            Dim _pdoctypeinfo As PropertyInfo

                            Dim _minfo As MethodInfo

                            _pdbnameinfo = T.GetProperty("SysDBName")
                            _ptablenameinfo = T.GetProperty("SysTableName")
                            _pdoctypeinfo = T.GetProperty("SysDocType")
                            _minfo = T.GetMethod("InitData")

                            If Not (_pdbnameinfo Is Nothing) AndAlso Not (_ptablenameinfo Is Nothing) AndAlso Not (_pdoctypeinfo Is Nothing) Then

                                If .Text = HI.TL.Document.GetDocumentNo(_pdbnameinfo.GetValue(_form, Nothing).ToString, _ptablenameinfo.GetValue(_form, Nothing).ToString, _pdoctypeinfo.GetValue(_form, Nothing).ToString, True) Then
                                    .Properties.Tag = ""
                                    _StateProc = False
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                    .Properties.Tag = ""

                    _BrowseID = Val("" & .Properties.Buttons.Item(0).Tag)

                    Dim _Qrysql As String = ""
                    Dim _dtbrw As New DataTable
                    Dim _dtret As New DataTable
                    Dim _Where As String = ""

                    _Where = HI.TL.HandlerControl.GridViewGetBrowseRespon(sender, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView), _BrowseID, _dtret, _BrowseCmd, _BrowseSortCmd, _BrowseWhereCmd, _FTBrwCmdField, _FTBrwCmdFieldOptional, FTBrwCmdGroupBy, _Qrysql, _ConFiledName, _Data, True)

                    If _Qrysql = "" Then
                        _StateProc = False
                        Exit Sub
                    End If

                    _Qrysql = _BrowseCmd.ToUpper.Replace("SELECT", " SELECT  ") & " " & _BrowseWhereCmd & _Where & "  " & FTBrwCmdGroupBy & " " & _BrowseSortCmd

                    If _Where <> "" AndAlso _ConFiledName <> "" Then
                        _dtbrw = New DataTable
                        _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)

                        If _dtbrw.Rows.Count > 0 Then
                            .Properties.Tag = _Data
                        Else
                            .Properties.Tag = ""
                        End If

                        With _dtbrw
                            If .Rows.Count > 0 Then
                                For Each Row As DataRow In _dtret.Rows
                                    If .Columns.IndexOf(Row!FTBrwField.ToString) >= 0 Then

                                        Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                            Case "FN", "FC", "FS"
                                                If IsNumeric(.Rows(0).Item(Row!FTBrwField.ToString).ToString) Then
                                                    Row!ValuesRet = CDbl(.Rows(0).Item(Row!FTBrwField.ToString).ToString)
                                                Else
                                                    Row!ValuesRet = "0"
                                                End If
                                            Case Else
                                                Row!ValuesRet = .Rows(0).Item(Row!FTBrwField.ToString).ToString
                                        End Select
                                    Else

                                        Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                            Case "FN", "FC", "FS"
                                                Row!ValuesRet = "0"
                                            Case Else
                                                Row!ValuesRet = ""
                                        End Select
                                    End If
                                Next
                            Else
                                For Each Row As DataRow In _dtret.Rows

                                    Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                        Case "FN", "FC", "FS"
                                            Row!ValuesRet = "0"
                                        Case Else
                                            Row!ValuesRet = ""
                                    End Select

                                Next
                            End If
                        End With

                        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                            For Each Row As DataRow In _dtret.Select("NOT(FTRetField IS NULL)")
                                If (Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatSizeCode".Length) = Row!FTRetField.ToString.ToUpper Or Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = Row!FTRetField.ToString.ToUpper) And Row!FTStatePropertyTag.ToString <> "Y" Then
                                    Continue For
                                End If

                                Dim _ColName As String = Row!FTRetField.ToString
                                If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                Else
                                    _ColName = _ColName & "" & .FocusedColumn.Name.ToString

                                    If (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                        If Microsoft.VisualBasic.Left(Row!FTRetField, Row!FTRetField.ToString.Length) = Microsoft.VisualBasic.Left(.FocusedColumn.Name.ToString, Row!FTRetField.ToString.Length) Then
                                            _ColName = .FocusedColumn.Name.ToString
                                        End If
                                    End If
                                End If


                                If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then

                                    Dim ctrl As Object = .Columns.ColumnByFieldName(_ColName).ColumnEdit

                                    If ctrl Is Nothing Then

                                        Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                            Case "System.Int32".ToUpper
                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                            Case "System.String".ToUpper
                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                            Case Else
                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                        End Select
                                    Else
                                        If Row!FTStatePropertyTag.ToString = "Y" Then
                                            Try
                                                If ctrl Is Nothing Then
                                                    Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                        Case "System.Int32".ToUpper
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                        Case "System.String".ToUpper
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                        Case Else
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    End Select
                                                Else
                                                    Try



                                                        If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatSizeCode".Length) = "FTRawMatSizeCode".ToUpper Then
                                                            Dim Col1 As String = .FocusedColumn.FieldName.ToString
                                                            Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                                            Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                                                                Case "System.Int32".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Val(Row!ValuesRet.ToString))

                                                                Case "System.String".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                Case Else
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                            End Select
                                                        ElseIf Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then
                                                            Dim Col1 As String = .FocusedColumn.FieldName.ToString
                                                            Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                                                            Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                                                                Case "System.Int32".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Val(Row!ValuesRet.ToString))

                                                                Case "System.String".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                Case Else
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                            End Select
                                                        Else
                                                            Select Case .Columns.ColumnByFieldName(_ColName & "_Hide").ColumnType.FullName.ToString.ToUpper
                                                                Case "System.Int32".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Val(Row!ValuesRet.ToString))
                                                                Case "System.String".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                                Case Else
                                                                    .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                            End Select
                                                        End If

                                                    Catch ex As Exception
                                                    End Try

                                                    'Try
                                                    '    With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                                                    '        .Text = Row!ValuesRet.ToString
                                                    '    End With
                                                    'Catch ex As Exception
                                                    'End Try

                                                    Try
                                                        With CType(ctrl, DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit)
                                                            .Tag = Row!ValuesRet.ToString
                                                        End With
                                                    Catch ex As Exception

                                                    End Try

                                                End If
                                            Catch ex As Exception
                                            End Try

                                        Else


                                            If Row!FTRetField.ToString.ToUpper = _Name.ToUpper Then
                                            Else
                                                Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                    Case "System.Int32".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                    Case "System.String".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    Case Else
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                End Select
                                            End If

                                        End If
                                    End If



                                End If
                            Next
                        End With


                    End If

                    _dtbrw.Dispose()
                    _dtret.Dispose()

                End With

                _StateProc = False
            End If

        End With
    End Sub

    Private Sub DynamicResponButtoneditSysHide_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

        With CType(sender, DevExpress.XtraEditors.ButtonEdit)
            If .Text <> "" Then

                Dim _value As String = ""
                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                    If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatSizeCode".Length) = "FTRawMatSizeCode".ToUpper Then
                        Dim Col1 As String = .FocusedColumn.FieldName.ToString
                        Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                        Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                            Case "System.Int32".ToUpper
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString
                            Case "System.String".ToUpper
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString()
                            Case Else
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString()
                        End Select
                    ElseIf Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then
                        Dim Col1 As String = .FocusedColumn.FieldName.ToString
                        Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                        Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                            Case "System.Int32".ToUpper
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString
                            Case "System.String".ToUpper
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString()
                            Case Else
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString()
                        End Select
                    End If


                    If _value = "0" Or _value = "" Then
                        .SetFocusedRowCellValue(.FocusedColumn, "")
                    End If

                End With
            End If
        End With

    End Sub

    Private Sub DynamicResponButtone_Gotocus(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim _Data As String

        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                _Data = "" & .GetRowCellValue(.FocusedRowHandle, .FocusedColumn).ToString
            End With
        Catch ex As Exception
            _Data = ""
        End Try

        Try
            With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                .Text = _Data
                .Properties.Tag = .Text
            End With
        Catch ex As Exception
        End Try

    End Sub

    Private Sub DynamicResponButtoneSysHideColor_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        Select Case e.Button.Index
            Case 0

                If Val(e.Button.Tag.ToString) <= 0 Then Exit Sub

                If Not (_StateProc) Then
                    _StateProcColor = True
                    Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
                    With New HI.TL.wDynamicBrowseInfo(Val(e.Button.Tag.ToString), _form)
                        .BrowseID = Val(e.Button.Tag.ToString)

                        .X = MousePosition.X
                        .Y = MousePosition.Y

                        Dim _Qrysql As String = ""
                        Dim _dtbrw As New DataTable
                        Dim _dtret As New DataTable
                        Dim _BrowseCmd As String = ""
                        Dim _BrowseSortCmd As String = ""
                        Dim _BrowseWhereCmd As String = ""
                        Dim _FTBrwCmdField As String = ""
                        Dim _FTBrwCmdFieldOptional As String = ""
                        Dim FTBrwCmdGroupBy As String = ""
                        Dim _Where As String = ""
                        Dim _ConFiledName As String = ""
                        _Where = HI.TL.HandlerControl.GridViewGetBrowseRespon(sender, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView), .BrowseID, _dtret, _BrowseCmd, _BrowseSortCmd, _BrowseWhereCmd, _FTBrwCmdField, _FTBrwCmdFieldOptional, FTBrwCmdGroupBy, _Qrysql, "", "")

                        If _Qrysql = "" Then
                            _StateProcColor = False
                            Exit Sub
                        End If

                        _dtbrw = New DataTable
                        _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql & "  " & _BrowseWhereCmd & "  " & _Where & "  " & FTBrwCmdGroupBy & "  " & _BrowseSortCmd, Conn.DB.DataBaseName.DB_SYSTEM)

                        .Data = _dtbrw.Copy
                        .DataRetField = _dtret.Copy

                        _dtbrw.Dispose()
                        _dtret.Dispose()

                        .ShowDialog()

                        If Not (.ValuesReturn Is Nothing) Then

                            For Each Row As DataRow In .DataRetField.Select("NOT(FTRetField IS NULL)")
                                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                                    Dim _ColName As String = Row!FTRetField.ToString

                                    If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                    Else
                                        _ColName = _ColName & "" & .FocusedColumn.Name.ToString

                                        If (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                            If Microsoft.VisualBasic.Left(Row!FTRetField, Row!FTRetField.ToString.Length) = Microsoft.VisualBasic.Left(.FocusedColumn.Name.ToString, Row!FTRetField.ToString.Length) Then
                                                _ColName = .FocusedColumn.Name.ToString
                                            End If
                                        End If
                                    End If

                                    If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                        Dim ctrl As Object = .Columns.ColumnByFieldName(_ColName).ColumnEdit

                                        If ctrl Is Nothing Then

                                            Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                Case "System.Int32".ToUpper
                                                    .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                Case "System.String".ToUpper
                                                    .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                Case Else
                                                    .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                            End Select
                                        Else
                                            If Row!FTStatePropertyTag.ToString = "Y" Then
                                                Try
                                                    If ctrl Is Nothing Then
                                                        Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                            Case "System.Int32".ToUpper
                                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                            Case "System.String".ToUpper
                                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                            Case Else
                                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                        End Select
                                                    Else

                                                        Try

                                                            If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatSizeCode".Length) = "FTRawMatSizeCode".ToUpper Then
                                                                Dim Col1 As String = .FocusedColumn.FieldName.ToString
                                                                Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                                                Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                                                                    Case "System.Int32".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Val(Row!ValuesRet.ToString))

                                                                    Case "System.String".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                    Case Else
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                End Select
                                                            ElseIf Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then
                                                                Dim Col1 As String = .FocusedColumn.FieldName.ToString
                                                                Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                                                                Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                                                                    Case "System.Int32".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Val(Row!ValuesRet.ToString))

                                                                    Case "System.String".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                    Case Else
                                                                        .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                End Select
                                                            Else
                                                                Select Case .Columns.ColumnByFieldName(_ColName & "_Hide").ColumnType.FullName.ToString.ToUpper
                                                                    Case "System.Int32".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Val(Row!ValuesRet.ToString))
                                                                    Case "System.String".ToUpper
                                                                        .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                                    Case Else
                                                                        .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                                End Select
                                                            End If
                                                        Catch ex As Exception
                                                        End Try

                                                        'Try
                                                        '    With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                                                        '        .Text = Row!ValuesRet.ToString
                                                        '    End With
                                                        'Catch ex As Exception
                                                        'End Try

                                                        Try
                                                            With CType(ctrl, DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit)
                                                                .Tag = Row!ValuesRet.ToString
                                                            End With
                                                        Catch ex As Exception
                                                        End Try

                                                    End If
                                                Catch ex As Exception
                                                End Try
                                            Else


                                                Try
                                                    With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                                                        .Text = Row!ValuesRet.ToString
                                                    End With
                                                Catch ex As Exception
                                                End Try

                                                Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                    Case "System.Int32".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                    Case "System.String".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    Case Else
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                End Select
                                            End If
                                        End If
                                    End If
                                End With
                            Next
                        End If

                        .Data.Dispose()
                        .DataRetField.Dispose()

                        Call SetColorDescription(CType(sender, DevExpress.XtraEditors.ButtonEdit).Text)

                    End With

                    _StateProcColor = False

                    With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                        Try
                            Dim IC As Integer = 1
                            Do While .Columns(.FocusedColumn.AbsoluteIndex + IC).Visible = False
                                IC = IC + 1
                            Loop
                            .FocusedColumn = .Columns(.FocusedColumn.AbsoluteIndex + IC)
                        Catch ex As Exception
                        End Try
                    End With

                End If

        End Select
    End Sub

    Private _StateProcColor As Boolean
    Private Sub DynamicResponButtoneditSysHideColor_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender, DevExpress.XtraEditors.ButtonEdit)

            If Not (_StateProcColor) Then
                _StateProcColor = True

                Dim _BrowseID As Integer
                Dim _Name As String
                Dim _Data As String
                Dim _BrowseCmd As String = ""
                Dim _BrowseSortCmd As String = ""
                Dim _BrowseWhereCmd As String = ""
                Dim _FTBrwCmdField As String = ""
                Dim _FTBrwCmdFieldOptional As String = ""
                Dim FTBrwCmdGroupBy As String = ""
                Dim _ConFiledName As String = ""

                Dim _form As Object = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent.FindForm
                If IsNothing(_form) Then _form = CType(sender, DevExpress.XtraEditors.ButtonEdit).Parent

                With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)

                    _Name = .Name.ToString.Replace("Res_", "")

                    _Data = .Text
                    If .Properties.Buttons.Count > 1 Then
                        If UCase("" & .Properties.Buttons.Item(1).Tag) = UCase("d") Then
                            Dim T As System.Type = _form.GetType()

                            Dim _pdbnameinfo As PropertyInfo
                            Dim _ptablenameinfo As PropertyInfo
                            Dim _pdoctypeinfo As PropertyInfo

                            Dim _minfo As MethodInfo

                            _pdbnameinfo = T.GetProperty("SysDBName")
                            _ptablenameinfo = T.GetProperty("SysTableName")
                            _pdoctypeinfo = T.GetProperty("SysDocType")
                            _minfo = T.GetMethod("InitData")

                            If Not (_pdbnameinfo Is Nothing) AndAlso Not (_ptablenameinfo Is Nothing) AndAlso Not (_pdoctypeinfo Is Nothing) Then

                                If .Text = HI.TL.Document.GetDocumentNo(_pdbnameinfo.GetValue(_form, Nothing).ToString, _ptablenameinfo.GetValue(_form, Nothing).ToString, _pdoctypeinfo.GetValue(_form, Nothing).ToString, True) Then
                                    .Properties.Tag = ""
                                    _StateProcColor = False
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                    .Properties.Tag = ""

                    _BrowseID = Val("" & .Properties.Buttons.Item(0).Tag)

                    Dim _Qrysql As String = ""
                    Dim _dtbrw As New DataTable
                    Dim _dtret As New DataTable
                    Dim _Where As String = ""

                    _Where = HI.TL.HandlerControl.GridViewGetBrowseRespon(sender, CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView), _BrowseID, _dtret, _BrowseCmd, _BrowseSortCmd, _BrowseWhereCmd, _FTBrwCmdField, _FTBrwCmdFieldOptional, FTBrwCmdGroupBy, _Qrysql, _ConFiledName, _Data, True)

                    If _Qrysql = "" Then
                        _StateProcColor = False
                        Exit Sub
                    End If

                    _Qrysql = _BrowseCmd.ToUpper.Replace("SELECT", " SELECT  ") & " " & _BrowseWhereCmd & _Where & "  " & FTBrwCmdGroupBy & " " & _BrowseSortCmd

                    If _Where <> "" AndAlso _ConFiledName <> "" Then
                        _dtbrw = New DataTable
                        _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)

                        If _dtbrw.Rows.Count > 0 Then
                            .Properties.Tag = _Data
                        Else
                            .Properties.Tag = ""
                        End If

                        With _dtbrw
                            If .Rows.Count > 0 Then
                                For Each Row As DataRow In _dtret.Rows
                                    If .Columns.IndexOf(Row!FTBrwField.ToString) >= 0 Then

                                        Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                            Case "FN", "FC", "FS"
                                                If IsNumeric(.Rows(0).Item(Row!FTBrwField.ToString).ToString) Then
                                                    Row!ValuesRet = CDbl(.Rows(0).Item(Row!FTBrwField.ToString).ToString)
                                                Else
                                                    Row!ValuesRet = "0"
                                                End If
                                            Case Else
                                                Row!ValuesRet = .Rows(0).Item(Row!FTBrwField.ToString).ToString
                                        End Select
                                    Else

                                        Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                            Case "FN", "FC", "FS"
                                                Row!ValuesRet = "0"
                                            Case Else
                                                Row!ValuesRet = ""
                                        End Select
                                    End If
                                Next
                            Else
                                For Each Row As DataRow In _dtret.Rows

                                    Select Case Microsoft.VisualBasic.Left(Row!FTBrwField.ToString.ToUpper, 2)
                                        Case "FN", "FC", "FS"
                                            Row!ValuesRet = "0"
                                        Case Else
                                            Row!ValuesRet = ""
                                    End Select

                                Next
                            End If
                        End With

                        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                            For Each Row As DataRow In _dtret.Select("NOT(FTRetField IS NULL)")
                                If (Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatSizeCode".Length) = Row!FTRetField.ToString.ToUpper Or Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = Row!FTRetField.ToString.ToUpper) And Row!FTStatePropertyTag.ToString <> "Y" Then
                                    Continue For
                                End If

                                Dim _ColName As String = Row!FTRetField.ToString
                                If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                Else
                                    _ColName = _ColName & "" & .FocusedColumn.Name.ToString

                                    If (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then
                                        If Microsoft.VisualBasic.Left(Row!FTRetField, Row!FTRetField.ToString.Length) = Microsoft.VisualBasic.Left(.FocusedColumn.Name.ToString, Row!FTRetField.ToString.Length) Then
                                            _ColName = .FocusedColumn.Name.ToString
                                        End If
                                    End If
                                End If

                                If Not (.Columns.ColumnByFieldName(_ColName) Is Nothing) Then

                                    Dim ctrl As Object = .Columns.ColumnByFieldName(_ColName).ColumnEdit

                                    If ctrl Is Nothing Then

                                        Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                            Case "System.Int32".ToUpper
                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                            Case "System.String".ToUpper
                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                            Case Else
                                                .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                        End Select
                                    Else
                                        If Row!FTStatePropertyTag.ToString = "Y" Then
                                            Try
                                                If ctrl Is Nothing Then

                                                    Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                        Case "System.Int32".ToUpper
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                        Case "System.String".ToUpper
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                        Case Else
                                                            .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    End Select

                                                Else

                                                    Try
                                                        If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatSizeCode".Length) = "FTRawMatSizeCode".ToUpper Then
                                                            Dim Col1 As String = .FocusedColumn.FieldName.ToString
                                                            Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                                            Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                                                                Case "System.Int32".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Val(Row!ValuesRet.ToString))

                                                                Case "System.String".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                Case Else
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                            End Select
                                                        ElseIf Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then
                                                            Dim Col1 As String = .FocusedColumn.FieldName.ToString
                                                            Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                                                            Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                                                                Case "System.Int32".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Val(Row!ValuesRet.ToString))

                                                                Case "System.String".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                                Case Else
                                                                    .SetRowCellValue(.FocusedRowHandle, Col2, Row!ValuesRet.ToString)
                                                            End Select
                                                        Else
                                                            Select Case .Columns.ColumnByFieldName(_ColName & "_Hide").ColumnType.FullName.ToString.ToUpper
                                                                Case "System.Int32".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Val(Row!ValuesRet.ToString))
                                                                Case "System.String".ToUpper
                                                                    .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                                Case Else
                                                                    .SetRowCellValue(.FocusedRowHandle, _ColName & "_Hide", Row!ValuesRet.ToString)
                                                            End Select
                                                        End If

                                                    Catch ex As Exception
                                                    End Try

                                                    Try
                                                        With CType(ctrl, DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit)
                                                            .Tag = Row!ValuesRet.ToString
                                                        End With
                                                    Catch ex As Exception
                                                    End Try
                                                End If

                                            Catch ex As Exception
                                            End Try

                                        Else

                                            If Row!FTRetField.ToString.ToUpper = _Name.ToUpper Then
                                            Else
                                                Select Case .Columns.ColumnByFieldName(_ColName).ColumnType.FullName.ToString.ToUpper
                                                    Case "System.Int32".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Val(Row!ValuesRet.ToString))
                                                    Case "System.String".ToUpper
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                    Case Else
                                                        .SetRowCellValue(.FocusedRowHandle, _ColName, Row!ValuesRet.ToString)
                                                End Select
                                            End If

                                        End If
                                    End If
                                End If
                            Next


                        End With
                    End If

                    _dtbrw.Dispose()
                    _dtret.Dispose()

                    Call SetColorDescription(.Text)

                End With

                _StateProcColor = False
            End If

        End With
    End Sub

    Private Sub SetColorDescription(_ColorCode As String)
        If _ColorCode <> "" Then
            Try
                Dim _Found As Boolean = False
                With GridView2
                    If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then

                        Dim Col1 As String = .FocusedColumn.FieldName.ToString
                        Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                        Dim Col3 As String = "FTRawMatColorNameTH" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                        Dim Col4 As String = "FTRawMatColorNameEN" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                        Dim SysMatCode As String = .GetRowCellValue(.FocusedRowHandle, "FTMainMatCode")
                        Dim SysStyleID As String = .GetRowCellValue(.FocusedRowHandle, "FNHSysStyleId")
                        Dim SysColorID As String = .GetRowCellValue(.FocusedRowHandle, Col2)

                        Dim _DescTH As String = ""
                        Dim _DescEN As String = ""

                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            If GridCol.FieldName <> .FocusedColumn.FieldName Then
                                If Microsoft.VisualBasic.Left(GridCol.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then
                                    If .GetFocusedRowCellValue(GridCol.FieldName).ToString = _ColorCode Then
                                        Dim Col5 As String = "FTRawMatColorNameTH" & Microsoft.VisualBasic.Right(GridCol.FieldName.ToString, GridCol.FieldName.ToString.Length - "FTRawMatColorCode".Length)
                                        Dim Col6 As String = "FTRawMatColorNameEN" & Microsoft.VisualBasic.Right(GridCol.FieldName.ToString, GridCol.FieldName.ToString.Length - "FTRawMatColorCode".Length)

                                        Try
                                            _DescTH = .GetRowCellValue(.FocusedRowHandle, Col5)
                                            _DescEN = .GetRowCellValue(.FocusedRowHandle, Col6)
                                        Catch ex As Exception
                                        End Try

                                        _Found = True
                                        Exit For

                                    End If
                                End If
                            End If
                        Next

                        If Not (_Found) Then
                            Dim _Qry As String = ""
                            Dim dt As DataTable

                            _Qry = " SELECT  TOP 1 C.FTRawMatColorNameTH, C.FTRawMatColorNameEN  "
                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTThreadStyle_ColorWay AS C WITH(NOLOCK) INNER JOIN"
                            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTThreadStyle_Mat AS B WITH(NOLOCK)  ON C.FNHSysStyleId = B.FNHSysStyleId AND C.FNSeq = B.FNSeq INNER JOIN"
                            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS A WITH(NOLOCK)  ON B.FNHSysMerMatId = A.FNHSysMainMatId"
                            _Qry &= vbCrLf & "  WHERE  A.FTMainMatCode='" & HI.UL.ULF.rpQuoted(SysMatCode) & "' "
                            _Qry &= vbCrLf & "  AND   C.FNHSysStyleId = " & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & " "
                            _Qry &= vbCrLf & "  AND   C.FNHSysRawMatColorId = " & Integer.Parse(Val(SysColorID)) & " "

                            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            If dt.Rows.Count <= 0 Then

                                _Qry = " SELECT  TOP 1   FNHSysRawMatColorId, FTRawMatColorNameTH, FTRawMatColorNameEN"
                                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C  WITH(NOLOCK) "
                                _Qry &= vbCrLf & "  WHERE FNHSysRawMatColorId = " & Integer.Parse(Val(SysColorID)) & " "
                                dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            End If

                            For Each R As DataRow In dt.Rows
                                _DescTH = R!FTRawMatColorNameTH.ToString
                                _DescEN = R!FTRawMatColorNameEN.ToString
                                Exit For
                            Next
                            dt.Dispose()
                        End If

                        .SetRowCellValue(.FocusedRowHandle, Col3, _DescTH)
                        .SetRowCellValue(.FocusedRowHandle, Col4, _DescEN)

                    End If
                End With
            Catch ex As Exception

            End Try


        Else
            Try
                With GridView2
                    If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then

                        Dim Col1 As String = .FocusedColumn.FieldName.ToString
                        Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                        Dim Col3 As String = "FTRawMatColorNameTH" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                        Dim Col4 As String = "FTRawMatColorNameEN" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                        .SetRowCellValue(.FocusedRowHandle, Col3, "")
                        .SetRowCellValue(.FocusedRowHandle, Col4, "")

                    End If

                End With
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub DynamicResponButtoneditSysHideColor_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)

        With CType(sender, DevExpress.XtraEditors.ButtonEdit)
            If .Text <> "" Then

                Dim _value As String = ""
                With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                    If Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatSizeCode".Length) = "FTRawMatSizeCode".ToUpper Then
                        Dim Col1 As String = .FocusedColumn.FieldName.ToString
                        Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                        Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                            Case "System.Int32".ToUpper
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString
                            Case "System.String".ToUpper
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString()
                            Case Else
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString()
                        End Select
                    ElseIf Microsoft.VisualBasic.Left(.FocusedColumn.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper Then
                        Dim Col1 As String = .FocusedColumn.FieldName.ToString
                        Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                        Select Case .Columns.ColumnByFieldName(Col2).ColumnType.FullName.ToString.ToUpper
                            Case "System.Int32".ToUpper
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString
                            Case "System.String".ToUpper
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString()
                            Case Else
                                _value = "" & .GetRowCellValue(.FocusedRowHandle, Col2).ToString()
                        End Select

                        If _value = "0" Or _value = "" Then

                            Dim Col3 As String = "FTRawMatColorNameTH" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                            Dim Col4 As String = "FTRawMatColorNameEN" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                            .SetRowCellValue(.FocusedRowHandle, Col3, "")
                            .SetRowCellValue(.FocusedRowHandle, Col4, "")

                        End If
                    End If


                    If _value = "0" Or _value = "" Then
                        .SetFocusedRowCellValue(.FocusedColumn, "")

                    End If

                End With
            End If
        End With

    End Sub

    Private Sub DynamicResponButtoneColor_Gotocus(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim _Data As String

        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
                _Data = "" & .GetRowCellValue(.FocusedRowHandle, .FocusedColumn).ToString
            End With
        Catch ex As Exception
            _Data = ""
        End Try

        Try
            With DirectCast(sender, DevExpress.XtraEditors.ButtonEdit)
                .Text = _Data
                .Properties.Tag = .Text
            End With
        Catch ex As Exception
        End Try

    End Sub
#End Region

    Private Sub RepRawMatIDCaledit_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepRawMatIDCaledit.EditValueChanging
        With Me.GridView2
            If .FocusedRowHandle < 0 Then Exit Sub
            Dim _ColName As String = .FocusedColumn.FieldName.ToString

            If Not (e.NewValue = e.OldValue) Then
                If e.NewValue = 0 Or e.NewValue = -1 Then
                    .SetFocusedRowCellValue("" & _ColName, "")

                Else

                End If
            End If

        End With
    End Sub

    Private Sub GridView2_CellValueChanged(sender As Object, e As Views.Base.CellValueChangedEventArgs) Handles GridView2.CellValueChanged
        With Me.GridView2
            If .FocusedRowHandle < 0 Then Exit Sub
            Static Proc As Boolean
            If Not (Proc) Then
                Proc = True
                Try
                    Dim _ColName As String = Microsoft.VisualBasic.Left(e.Column.FieldName.ToString.ToUpper, ("FNHSysRawMatColorIdFTRawMatColorCode").Length)

                    If _ColName.ToUpper = ("FNHSysRawMatColorIdFTRawMatColorCode").ToUpper Then
                        Dim ColorWay As String = Microsoft.VisualBasic.Right(e.Column.FieldName.ToString, e.Column.FieldName.ToString.Length - ("FNHSysRawMatColorIdFTRawMatColorCode").Length)

                        If Integer.Parse(Val(e.Value)) = 0 Then
                            .SetRowCellValue(e.RowHandle, "FTRawMatColorNameTH" & ColorWay, "")
                            .SetRowCellValue(e.RowHandle, "FTRawMatColorNameEN" & ColorWay, "")
                        Else
                            Dim _ItemCode As String = "" & .GetRowCellValue(e.RowHandle, "FTMainMatCode").ToString
                            Dim _Qry As String = ""
                            Dim dt As DataTable

                            _Qry = " SELECT  TOP 1  A.FNHSysRawMatColorId "
                            _Qry &= vbCrLf & "  ,ISNULL(B.FTRawMatColorNameTH,A.FTRawMatColorNameTH) AS FTRawMatColorNameTH"
                            _Qry &= vbCrLf & "  ,ISNULL(B.FTRawMatColorNameEN,A.FTRawMatColorNameEN) AS FTRawMatColorNameEN"
                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS A WITH(NOLOCK)  LEFT JOIN"
                            _Qry &= vbCrLf & "  (SELECT        B.FNHSysRawMatColorId, B.FTRawMatColorNameTH, B.FTRawMatColorNameEN"
                            _Qry &= vbCrLf & "   FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTThreadStyle_Mat AS A WITH(NOLOCK)  INNER JOIN"
                            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTThreadStyle_ColorWay AS B WITH(NOLOCK)  ON A.FNHSysStyleId = B.FNHSysStyleId AND A.FNSeq = B.FNSeq INNER JOIN"
                            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS M WITH(NOLOCK) ON A.FNHSysMerMatId = M.FNHSysMainMatId"
                            _Qry &= vbCrLf & "   WHERE        (M.FTMainMatCode = N'" & HI.UL.ULF.rpQuoted(_ItemCode) & "')"
                            _Qry &= vbCrLf & "  AND  B.FNHSysRawMatColorId=" & Integer.Parse(Val(e.Value)) & " "
                            _Qry &= vbCrLf & "  ) AS B "
                            _Qry &= vbCrLf & "  ON A.FNHSysRawMatColorId = B.FNHSysRawMatColorId"
                            _Qry &= vbCrLf & "  WHERE  A.FNHSysRawMatColorId=" & Integer.Parse(Val(e.Value)) & " "

                            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            For Each R As DataRow In dt.Rows
                                .SetRowCellValue(e.RowHandle, "FTRawMatColorNameTH" & ColorWay, R!FTRawMatColorNameTH.ToString)
                                .SetRowCellValue(e.RowHandle, "FTRawMatColorNameEN" & ColorWay, R!FTRawMatColorNameEN.ToString)
                                Exit For
                            Next

                            dt.Dispose()

                        End If
                    End If
                Catch ex As Exception

                End Try
                Proc = False
            End If

        End With
    End Sub
    Private Sub ReposFNMerMatSeq_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposFNMerMatSeq.EditValueChanging
        Try
            CType(ogcmatcode.DataSource, DataTable).AcceptChanges()
            With GridView1
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim MaxSeq As Integer = CType(ogcmatcode.DataSource, DataTable).Select("FNMerMatSeq=" & e.NewValue & "").Length + 1
                .SetFocusedRowCellValue("FNPart", MaxSeq)

                CType(ogcmatcode.DataSource, DataTable).AcceptChanges()

            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView2.KeyDown
        Try
            With Me.GridView2
                If .FocusedRowHandle < 0 Then Exit Sub
                Select Case e.KeyCode
                    Case Keys.F10
                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatColorCode")) = "FTRawMatColorCode".ToUpper Then

                                Dim Col1 As String = GridCol.FieldName.ToString
                                Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                                .SetFocusedRowCellValue(GridCol.FieldName, "N/R")
                                .SetFocusedRowCellValue(Col2, "-1")

                            End If
                        Next

                        CType(ogcstylecolor.DataSource, DataTable).AcceptChanges()
                        ogcstylecolor.RefreshDataSource()

                    Case Keys.F11
                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatColorCode")) = "FTRawMatColorCode".ToUpper Then

                                Dim Col1 As String = GridCol.FieldName.ToString
                                Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)
                                .SetFocusedRowCellValue(GridCol.FieldName, "")
                                .SetFocusedRowCellValue(Col2, "0")
                            End If
                        Next

                        CType(ogcstylecolor.DataSource, DataTable).AcceptChanges()
                        ogcstylecolor.RefreshDataSource()
                    Case Keys.F12
                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatColorCode")) = "FTRawMatColorCode".ToUpper Then

                                Dim Code As String = GridCol.Caption
                                Dim _ColorInt As Integer = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysRawMatColorId FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor WITH(NOLOCK) WHERE FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(Code) & "'  ", Conn.DB.DataBaseName.DB_MASTER, "0")))

                                If _ColorInt > 0 Then
                                    Dim Col1 As String = GridCol.FieldName.ToString
                                    Dim Col2 As String = "FNHSysRawMatColorId" & "FTRawMatColorCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatColorCode".Length)

                                    .SetFocusedRowCellValue(GridCol.FieldName, Code)
                                    .SetFocusedRowCellValue(Col2, _ColorInt.ToString)
                                End If


                            End If
                        Next

                        CType(ogcstylecolor.DataSource, DataTable).AcceptChanges()
                        ogcstylecolor.RefreshDataSource()
                End Select
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GridView3_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView3.KeyDown
        Try
            With Me.GridView3
                If .FocusedRowHandle < 0 Then Exit Sub
                Select Case e.KeyCode
                    Case Keys.F10

                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatSizeCode")) = "FTRawMatSizeCode".ToUpper Then

                                Dim Col1 As String = GridCol.FieldName.ToString
                                Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                .SetFocusedRowCellValue(GridCol.FieldName, "N/R")
                                .SetFocusedRowCellValue(Col2, "-1")
                            End If
                        Next

                        CType(ogcstylesize.DataSource, DataTable).AcceptChanges()
                        ogcstylesize.RefreshDataSource()

                    Case Keys.F11
                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatSizeCode")) = "FTRawMatSizeCode".ToUpper Then
                                Dim Col1 As String = GridCol.FieldName.ToString
                                Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                .SetFocusedRowCellValue(GridCol.FieldName, "")
                                .SetFocusedRowCellValue(Col2, "0")
                            End If
                        Next

                        CType(ogcstylesize.DataSource, DataTable).AcceptChanges()
                        ogcstylesize.RefreshDataSource()
                    Case Keys.F12
                        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                            If Microsoft.VisualBasic.Left(GridCol.Name.ToUpper, Len("FTRawMatSizeCode")) = "FTRawMatSizeCode".ToUpper Then

                                Dim Code As String = GridCol.Caption
                                Dim _SizeInt As Integer = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysRawMatSizeId FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize WITH(NOLOCK) WHERE FTRawMatSizeCode='" & HI.UL.ULF.rpQuoted(Code) & "'  ", Conn.DB.DataBaseName.DB_MASTER, "0")))

                                If _SizeInt > 0 Then
                                    Dim Col1 As String = GridCol.FieldName.ToString
                                    Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)

                                    .SetFocusedRowCellValue(GridCol.FieldName, Code)
                                    .SetFocusedRowCellValue(Col2, _SizeInt.ToString)
                                End If


                            End If
                        Next

                        CType(ogcstylesize.DataSource, DataTable).AcceptChanges()
                        ogcstylesize.RefreshDataSource()
                End Select
            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub GridView2_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView2.RowCellStyle
        Try
            With GridView2
                If Microsoft.VisualBasic.Left(e.Column.FieldName.ToUpper, Len("FTRawMatColorCode")) = "FTRawMatColorCode".ToUpper Then
                    If "" & .GetRowCellValue(e.RowHandle, e.Column).ToString() = "N/R" Then

                        e.Appearance.BackColor = System.Drawing.Color.LightCyan
                        e.Appearance.ForeColor = System.Drawing.Color.Blue

                    End If
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private lastRowHandle As Integer = GridControl.InvalidRowHandle
    Private lastColumn As GridColumn = Nothing
    Private ttInfo As ToolTipControlInfo = Nothing

    Private Sub objToolTipController_GetActiveObjectInfo(sender As Object, e As ToolTipControllerGetActiveObjectInfoEventArgs) Handles objToolTipController.GetActiveObjectInfo
        Try
            If e.Info Is Nothing AndAlso e.SelectedControl Is ogcstylecolor Then
                Dim view As GridView = TryCast(ogcstylecolor.FocusedView, GridView)
                Dim info As GridHitInfo = view.CalcHitInfo(e.ControlMousePosition)

                If info.InRowCell AndAlso (info.RowHandle <> lastRowHandle OrElse info.Column IsNot lastColumn) Then
                    lastRowHandle = info.RowHandle
                    lastColumn = info.Column
                    Dim text As String = view.GetRowCellDisplayText(info.RowHandle, info.Column)
                    Dim cellKey As String = info.RowHandle.ToString() & " - " & info.Column.ToString()

                    If Microsoft.VisualBasic.Left(info.Column.FieldName.ToString.ToUpper, "FTRawMatColorCode".Length) = "FTRawMatColorCode".ToUpper And text <> "" Then

                        Dim Col5 As String = "FTRawMatColorNameTH" & Microsoft.VisualBasic.Right(info.Column.FieldName.ToString, info.Column.FieldName.ToString.Length - "FTRawMatColorCode".Length)
                        Dim Col6 As String = "FTRawMatColorNameEN" & Microsoft.VisualBasic.Right(info.Column.FieldName.ToString, info.Column.FieldName.ToString.Length - "FTRawMatColorCode".Length)

                        Try
                            text &= vbCrLf & "" & view.GetRowCellValue(info.RowHandle, Col5)
                            text &= vbCrLf & "" & view.GetRowCellValue(info.RowHandle, Col6)
                        Catch ex As Exception
                        End Try

                    End If

                    ttInfo = New DevExpress.Utils.ToolTipControlInfo(cellKey, text)

                End If

                If ttInfo IsNot Nothing Then
                    e.Info = ttInfo
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Enum EditMergeCellData As Integer
        ItemSeq = 0
        ItemCode = 1
        SuplCode = 2
        UNitCode = 3
    End Enum

    Private _StateEditMergeCell As EditMergeCellData = EditMergeCellData.ItemSeq
    Property StateEditMergeCell As EditMergeCellData
        Get
            Return _StateEditMergeCell
        End Get
        Set(value As EditMergeCellData)
            _StateEditMergeCell = value
        End Set
    End Property

    Private m_mergedCellEditorSupl As DevExpress.XtraEditors.ButtonEdit
    Private m_mergedCellEditorMainMat As DevExpress.XtraEditors.ButtonEdit
    Private m_mergedCellEditorUnit As DevExpress.XtraEditors.ButtonEdit
    Private m_mergedCellsEdited As GridCellInfoCollection

    Private Sub CreateMergeEditControl()
        m_mergedCellEditorSupl = New DevExpress.XtraEditors.ButtonEdit
        m_mergedCellEditorMainMat = New DevExpress.XtraEditors.ButtonEdit
        m_mergedCellEditorUnit = New DevExpress.XtraEditors.ButtonEdit

        With m_mergedCellEditorSupl
            .Name = "FNHSysSuplIdTo"
            .Properties.Buttons(0).Tag = 175

            AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicButtone_ButtonClick
            AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly
        End With

        With m_mergedCellEditorMainMat
            .Name = "FNHSysMainMatIdTo"
            .Properties.Buttons(0).Tag = 452

            AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicButtone_ButtonClick
            AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly
        End With

        With m_mergedCellEditorUnit
            .Name = "FNHSysUnitIdTo"
            .Properties.Buttons(0).Tag = 246

            AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicButtone_ButtonClick
            AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicButtonedit_EditValueChanged
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicButtonedit_LeaveOnly
        End With

    End Sub


    Private Sub GridView1_MouseDown(sender As Object, e As MouseEventArgs) Handles GridView1.MouseDown
        Dim tmpview As DevExpress.XtraGrid.Views.Grid.GridView = sender
        Dim hInfo As GridHitInfo = tmpview.CalcHitInfo(e.X, e.Y)

        If (hInfo.InRowCell) Then

            If Not (m_mergedCellsEdited Is Nothing) Then
                Select Case StateEditMergeCell
                    Case EditMergeCellData.SuplCode
                        If (ogcmatcode.Contains(m_mergedCellEditorSupl)) Then
                            ogcmatcode.Controls.Remove(m_mergedCellEditorSupl)
                            For Each cellInfo As GridCellInfo In m_mergedCellsEdited
                                With GridView1
                                    If m_mergedCellEditorSupl.Text <> "" Then
                                        .SetRowCellValue(cellInfo.RowHandle, "FTSuplCode", m_mergedCellEditorSupl.Text)

                                        Try
                                            .SetRowCellValue(cellInfo.RowHandle, "FNHSysSuplId", Integer.Parse(Val(m_mergedCellEditorSupl.Properties.Tag.ToString())))
                                        Catch ex As Exception
                                            .SetRowCellValue(cellInfo.RowHandle, "FNHSysSuplId", 0)
                                        End Try
                                    End If
                                End With
                            Next

                        End If
                    Case EditMergeCellData.ItemCode
                        If (ogcmatcode.Contains(m_mergedCellEditorMainMat)) Then
                            ogcmatcode.Controls.Remove(m_mergedCellEditorMainMat)
                            Dim _FNHSysMerMatId_None As String = ""
                            Dim _Qry As String = ""

                            _Qry = "SELECT TOP 1 "

                            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                                _Qry &= vbCrLf & " FTMainMatNameEN AS FTMainMatName "
                            Else
                                _Qry &= vbCrLf & " FTMainMatNameTH AS FTMainMatName "
                            End If

                            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS A WITH(NOLOCK) "
                            _Qry &= vbCrLf & " WHERE FNHSysMainMatId=" & Integer.Parse(Val(m_mergedCellEditorMainMat.Properties.Tag.ToString())) & ""
                            _FNHSysMerMatId_None = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")

                            For Each cellInfo As GridCellInfo In m_mergedCellsEdited
                                With GridView1
                                    If m_mergedCellEditorMainMat.Text <> "" Then
                                        .SetRowCellValue(cellInfo.RowHandle, "FTMainMatCode", m_mergedCellEditorMainMat.Text)

                                        Try
                                            .SetRowCellValue(cellInfo.RowHandle, "FNHSysMerMatId", Integer.Parse(Val(m_mergedCellEditorMainMat.Properties.Tag.ToString())))
                                        Catch ex As Exception
                                            .SetRowCellValue(cellInfo.RowHandle, "FNHSysMerMatId", 0)
                                        End Try

                                        .SetRowCellValue(cellInfo.RowHandle, "FNHSysMerMatId_None", _FNHSysMerMatId_None)
                                    End If

                                End With
                            Next

                        End If
                    Case EditMergeCellData.UNitCode
                        If (ogcmatcode.Contains(m_mergedCellEditorUnit)) Then
                            ogcmatcode.Controls.Remove(m_mergedCellEditorUnit)
                            For Each cellInfo As GridCellInfo In m_mergedCellsEdited
                                With GridView1
                                    If m_mergedCellEditorUnit.Text <> "" Then
                                        .SetRowCellValue(cellInfo.RowHandle, "FTUnitCode", m_mergedCellEditorUnit.Text)

                                        Try
                                            .SetRowCellValue(cellInfo.RowHandle, "FNHSysUnitId", Integer.Parse(Val(m_mergedCellEditorUnit.Properties.Tag.ToString())))
                                        Catch ex As Exception
                                            .SetRowCellValue(cellInfo.RowHandle, "FNHSysUnitId", 0)
                                        End Try
                                    End If


                                End With
                            Next

                        End If
                End Select

                m_mergedCellsEdited = Nothing
            End If

            Dim vInfo As GridViewInfo = tmpview.GetViewInfo()
            Dim cInfo As GridCellInfo = vInfo.GetGridCellInfo(hInfo)
            Select Case cInfo.Column.FieldName.ToString
                Case "FTSuplCode"

                    If (TypeOf (cInfo) Is DevExpress.XtraGrid.Views.Grid.ViewInfo.GridMergedCellInfo) Then
                        If (m_mergedCellsEdited IsNot Nothing) Then
                            ogcmatcode.Controls.Remove(m_mergedCellEditorSupl)
                        End If

                        ogcmatcode.Controls.Add(m_mergedCellEditorSupl)
                        m_mergedCellEditorSupl.Bounds = cInfo.Bounds
                        ' ''m_mergedCellEditorSupl.Text = cInfo.CellValue.ToString()
                        m_mergedCellEditorSupl.Text = cInfo.CellValue.ToString()
                        m_mergedCellsEdited = (CType(cInfo, GridMergedCellInfo)).MergedCells
                        StateEditMergeCell = EditMergeCellData.SuplCode
                    End If
                Case "FTMainMatCode"
                    If (TypeOf (cInfo) Is DevExpress.XtraGrid.Views.Grid.ViewInfo.GridMergedCellInfo) Then
                        If (m_mergedCellsEdited IsNot Nothing) Then
                            ogcmatcode.Controls.Remove(m_mergedCellEditorMainMat)
                        End If

                        ogcmatcode.Controls.Add(m_mergedCellEditorMainMat)
                        m_mergedCellEditorMainMat.Bounds = cInfo.Bounds
                        m_mergedCellEditorMainMat.Text = cInfo.CellValue.ToString()
                        ''m_mergedCellsEdited = (CType(cInfo, GridMergedCellInfo)).MergedCells
                        m_mergedCellsEdited = (CType(cInfo, GridMergedCellInfo)).MergedCells
                        StateEditMergeCell = EditMergeCellData.ItemCode
                        '' ''End If
                    End If
                Case "FTUnitCode"
                    If (TypeOf (cInfo) Is DevExpress.XtraGrid.Views.Grid.ViewInfo.GridMergedCellInfo) Then
                        If (m_mergedCellsEdited IsNot Nothing) Then
                            ogcmatcode.Controls.Remove(m_mergedCellEditorUnit)
                        End If

                        ogcmatcode.Controls.Add(m_mergedCellEditorUnit)
                        m_mergedCellEditorUnit.Bounds = cInfo.Bounds
                        m_mergedCellEditorUnit.Text = cInfo.CellValue.ToString()
                        m_mergedCellsEdited = (CType(cInfo, GridMergedCellInfo)).MergedCells
                        StateEditMergeCell = EditMergeCellData.UNitCode
                    End If
            End Select
        End If
    End Sub


    Private Sub GridView3_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView3.RowCellStyle
        Try
            With GridView3
                If Microsoft.VisualBasic.Left(e.Column.FieldName.ToUpper, Len("FTRawMatSizeCode")) = "FTRawMatSizeCode".ToUpper Then

                    If "" & .GetRowCellValue(e.RowHandle, e.Column).ToString() = "N/R" Then

                        e.Appearance.BackColor = System.Drawing.Color.LightCyan
                        e.Appearance.ForeColor = System.Drawing.Color.Blue

                    End If

                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub otb_SelectedPageChanging(sender As Object, e As DevExpress.XtraTab.TabPageChangingEventArgs) Handles otb.SelectedPageChanging
        Try
            Select Case e.PrevPage.Name
                Case otpmatcode.Name
                    With CType(ogcmatcode.DataSource, DataTable)
                        .AcceptChanges()

                        If .Select("FTStateDataDetail='0'").Length > 0 Then
                            HI.MG.ShowMsg.mInfo("พบข้อมูลรายการใหม่ กรุณาทำการบันทึกข้อมูลก่อน !!!", 1607210089, Me.Text, , MessageBoxIcon.Warning)
                            e.Cancel = True
                        End If

                    End With

                Case otpmatcolor.Name
                Case otpmatsize.Name

            End Select
        Catch ex As Exception

        End Try

    End Sub
End Class