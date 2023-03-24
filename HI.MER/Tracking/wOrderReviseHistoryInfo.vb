
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
Imports System.Globalization
Imports System.Configuration
Imports System.Diagnostics
Imports DevExpress.XtraPrintingLinks
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized

Public Class wOrderReviseHistoryInfo

    Dim RowsIndex As Double
    Dim TopVisibleIndex As Int32
    Private sFNHSysStyleId As String
    Private oGridView As DevExpress.XtraGrid.Views.Grid.GridView

    ''' Used Data Adapter to control database

    Dim oleDbDataAdapter1 As DbDataAdapter
    Dim oleDbDataAdapter2 As DbDataAdapter
    Dim dtStyleDetail As DataTable

    Private Inited As Boolean
    Dim FirstLoad As Boolean = True

    'Private nNumCntLoadOrderListing As Integer

#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(ByVal value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(ByVal value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(ByVal value As String)
            _CallMethodParm = value
        End Set
    End Property

#End Region

#Region "Handler Control"

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'With RepositoryFTMainMatCode
        '    AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
        '    AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
        '    AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
        '    AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave

        'End With

        'For Each oGridViewCol As DevExpress.XtraGrid.Columns.GridColumn In Me.GridView1.Columns

        '    Select Case oGridViewCol.FieldName.ToUpper
        '        Case "GUID".ToUpper, "FTTableName".ToUpper, "FTRefGUID".ToUpper
        '            oGridViewCol.OptionsColumn.FixedWidth = True
        '        Case Else
        '            oGridViewCol.OptionsColumn.FixedWidth = False
        '    End Select

        'Next

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

#End Region

#Region "MAIN PROC"

    Private Sub wOrderListingInfo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        '' Do some thing like this.
    End Sub

    Private Sub Proc_Close(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub ocmrefresh_Click(sender As System.Object, e As System.EventArgs) Handles ocmrefresh.Click
        REM 20150123
        'Dim _Str As String = ""
        'Me.ogorder.DataSource = Nothing
        'Me.ogorder.Refresh()
        'Me.GridView1.OptionsView.ColumnAutoWidth = False
        'Call LoadOrderListingInfo()
        Try
            Call LoadOrderListingInfo()
        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, My.Application.Info.Title)
            End If
        End Try

    End Sub

    Private Sub ocmclearclsr_Click(sender As System.Object, e As System.EventArgs) Handles ocmclearclsr.Click
        Me.ogorder.DataSource = Nothing
        Dim xCol As Integer = 0
        Dim Idx As Integer = 0
        Try
            HI.TL.HandlerControl.ClearControl(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub LoadOrderListingInfo_BACKUP_20150122()
        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Dim sqlCmd As New SqlCommand
        sqlCmd.Connection = HI.Conn.SQLConn.Cnn
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_GET_CHANGEDLOG_INFO]"
        sqlCmd.Parameters.AddWithValue("@LANGID", HI.ST.Lang.Language)

        Dim sqlDA As New SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
        sqlDA.SelectCommand = sqlCmd
        Dim dt As New DataTable

        'sqlDA.Fill(dt)
        'Me.ogorder.DataSource = dt

        If System.Diagnostics.Debugger.IsAttached = True Then
            Try
                sqlDA.Fill(dt)
                Me.ogorder.DataSource = dt
            Catch ex As Exception
                'MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, Me.Text)
                Throw New Exception(ex.Message.ToString & Environment.NewLine & ex.StackTrace.ToString)
            End Try
        Else
            sqlDA.Fill(dt)
            Me.ogorder.DataSource = dt
        End If

        Dim view As GridView
        view = ogorder.Views(0)
        view.OptionsView.ShowAutoFilterRow = True
        view.BestFitColumns()

        Me.ogorder = view.GridControl
        Me.ogorder.Refresh()

        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        'If Inited = False Then
        '    InitGrid()
        'End If

    End Sub

    Private Sub LoadOrderListingInfo()

        Try
            Dim dt As System.Data.DataTable

            Dim oStrBuilder As New System.Text.StringBuilder
            Dim sSQL As String

            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("DECLARE @LANGID AS INT;")
            oStrBuilder.AppendLine(String.Format("SET @LANGID = {0};", IIf(HI.ST.Lang.eLang.TH, 2, 1)))
            oStrBuilder.AppendLine("SELECT M.*")
            oStrBuilder.AppendLine("FROM (")
            oStrBuilder.AppendLine("SELECT [Guid]")
            oStrBuilder.AppendLine("      ,[FTFormName] = (SELECT TOP 1 CASE WHEN @LANGID = 2 THEN L.FTLangTH ELSE L.FTLangEN END FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_LANG) & "].dbo.HSysLanguage L (NOLOCK) WHERE L.FTFormName = A.FTFormName AND L.FTObjectName= A.FTFormName)")
            oStrBuilder.AppendLine("      ,[FTTableName]")
            oStrBuilder.AppendLine("      ,ISNULL( (SELECT TOP 1 CASE WHEN @LANGID = 2 THEN L.FTLangTH ELSE L.FTLangEN END FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_LANG) & "].dbo.HSysLanguage L (NOLOCK) WHERE L.FTFormName = A.FTFormName AND LEFT(L.FTObjectName,LEN(A.FTChangeObject)) = A.FTChangeObject),[FTChangeObject]) AS [FTChangeObject]")
            oStrBuilder.AppendLine("      ,CASE WHEN ISDATE(LEFT([FTChangeFrom],10)) = 1  AND LEN([FTChangeFrom]) = 10 THEN  CONVERT(VARCHAR(10), CAST([FTChangeFrom] AS DATE), 103)")
            oStrBuilder.AppendLine("                                                                                   ELSE [FTChangeFrom] END AS [FTChangeFrom]")
            oStrBuilder.AppendLine("      ,CASE WHEN ISDATE(LEFT([FTChangeTo],10)) = 1  AND LEN([FTChangeTo]) = 10 THEN CONVERT(VARCHAR(10), CAST([FTChangeTo] AS DATE), 103)")
            oStrBuilder.AppendLine("                                                                               ELSE [FTChangeTo] END AS [FTChangeTo]")
            oStrBuilder.AppendLine("      ,[FTRefGUID]")
            oStrBuilder.AppendLine("      ,CASE WHEN  CHARINDEX('|',[FTRefDocKey]) > 5 THEN CASE WHEN  LEN(LEFT([FTRefDocKey], CHARINDEX('|',[FTRefDocKey])-1)) = 9 OR LEN(LEFT([FTRefDocKey], CHARINDEX('|',[FTRefDocKey])-1)) = 10 OR LEN(LEFT([FTRefDocKey], CHARINDEX('|',[FTRefDocKey])-1)) = 11  THEN LEFT([FTRefDocKey], CHARINDEX('|',[FTRefDocKey])-1) ELSE '' END")
            oStrBuilder.AppendLine("            WHEN  LEN([FTRefDocKey]) = 9 OR LEN([FTRefDocKey]) = 10 OR LEN([FTRefDocKey]) = 11 THEN [FTRefDocKey]")
            oStrBuilder.AppendLine("																							   ELSE '' END AS FTOrderNo")
            oStrBuilder.AppendLine("      ,CASE WHEN  CHARINDEX('|',[FTRefDocKey]) > 5 THEN REPLACE( RIGHT([FTRefDocKey],LEN([FTRefDocKey])- CHARINDEX('|',[FTRefDocKey])), '|', ', ')")
            oStrBuilder.AppendLine("                                                   ELSE  REPLACE([FTRefDocKey], '|', ', ')  END AS [FTRefDocKey]")
            oStrBuilder.AppendLine("      ,[FTUpdUser]")
            oStrBuilder.AppendLine("      ,CONVERT(VARCHAR(10), CAST([FDUpdDate] AS DATE), 103) AS [FDUpdDate]")
            oStrBuilder.AppendLine("      ,[FTUpdTime]")
            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_LOG) & "].dbo.HSysAuditLog A (NOLOCK)")
            'oStrBuilder.AppendLine("WHERE FTTableName LIKE '%TMERTOrder%' OR FTTableName LIKE '%TMERTStyle%'")
            oStrBuilder.AppendLine("WHERE FTTableName LIKE '%TMERTOrder%'")

            oStrBuilder.AppendLine("UNION ALL")

            oStrBuilder.AppendLine("SELECT [Guid]")
            oStrBuilder.AppendLine("      ,[FTFormName] = (SELECT TOP 1 CASE WHEN @LANGID = 2 THEN L.FTLangTH ELSE L.FTLangEN END FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_LANG) & "].dbo.HSysLanguage L (NOLOCK) WHERE L.FTFormName = A.FTFormName AND L.FTObjectName= A.FTFormName)")
            oStrBuilder.AppendLine("      ,[FTTableName]")
            oStrBuilder.AppendLine("      ,ISNULL( (SELECT TOP 1 CASE WHEN @LANGID = 2 THEN L.FTLangTH ELSE L.FTLangEN END FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_LANG) & "].dbo.HSysLanguage L (NOLOCK) WHERE L.FTFormName = A.FTFormName AND LEFT(L.FTObjectName,LEN(A.FTChangeObject)) = A.FTChangeObject),[FTChangeObject]) AS [FTChangeObject]")
            oStrBuilder.AppendLine("      ,CASE WHEN ISDATE(LEFT([FTChangeFrom],10)) = 1  AND LEN([FTChangeFrom]) = 10 THEN  CONVERT(VARCHAR(10), CAST([FTChangeFrom] AS DATE), 103)")
            oStrBuilder.AppendLine("                                                                                   ELSE [FTChangeFrom] END AS [FTChangeFrom]")
            oStrBuilder.AppendLine("      ,CASE WHEN ISDATE(LEFT([FTChangeTo],10)) = 1  AND LEN([FTChangeTo]) = 10 THEN CONVERT(VARCHAR(10), CAST([FTChangeTo] AS DATE), 103)")
            oStrBuilder.AppendLine("                                                                               ELSE [FTChangeTo] END AS [FTChangeTo]")
            oStrBuilder.AppendLine("      ,[FTRefGUID]")
            oStrBuilder.AppendLine("      ,CASE WHEN  CHARINDEX('|',[FTRefDocKey]) > 5 THEN CASE WHEN  LEN(LEFT([FTRefDocKey], CHARINDEX('|',[FTRefDocKey])-1)) = 9 OR LEN(LEFT([FTRefDocKey], CHARINDEX('|',[FTRefDocKey])-1)) = 10 OR LEN(LEFT([FTRefDocKey], CHARINDEX('|',[FTRefDocKey])-1)) = 11  THEN LEFT([FTRefDocKey], CHARINDEX('|',[FTRefDocKey])-1) ELSE '' END")
            oStrBuilder.AppendLine("            WHEN  LEN([FTRefDocKey]) = 9 OR LEN([FTRefDocKey]) = 10 OR LEN([FTRefDocKey]) = 11 THEN [FTRefDocKey]")
            oStrBuilder.AppendLine("																							   ELSE '' END AS FTOrderNo")
            oStrBuilder.AppendLine("      ,CASE WHEN  CHARINDEX('|',[FTRefDocKey]) > 5 THEN REPLACE( RIGHT([FTRefDocKey],LEN([FTRefDocKey])- CHARINDEX('|',[FTRefDocKey])), '|', ', ')")
            oStrBuilder.AppendLine("                                                   ELSE  REPLACE([FTRefDocKey], '|', ', ')  END AS [FTRefDocKey]")
            oStrBuilder.AppendLine("      ,[FTUpdUser]")
            oStrBuilder.AppendLine("      ,CONVERT(VARCHAR(10), CAST([FDUpdDate] AS DATE), 103) AS [FDUpdDate]")
            oStrBuilder.AppendLine("      ,[FTUpdTime]")
            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_LOG) & "].dbo.HSysAuditLog A (NOLOCK)")
            oStrBuilder.AppendLine("WHERE FTTableName LIKE '%TMERTStyle%'")
            oStrBuilder.AppendLine(") AS M")

            oStrBuilder.AppendLine("ORDER BY M.FDUpdDate DESC, M.FTUpdTime DESC;")

            sSQL = oStrBuilder.ToString()

            Try
                Me.ogorder.DataSource = Nothing
                Me.ogorder.Refresh()
                Me.GridView1.OptionsView.ColumnAutoWidth = False

                oGridView = Me.ogorder.Views(0)

                dt = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_LOG)

                Me.ogorder.DataSource = dt
                Me.ogorder = oGridView.GridControl
                oGridView.OptionsView.ColumnAutoWidth = False
                oGridView.OptionsView.ShowAutoFilterRow = True
                Me.ogorder.Refresh()
                oGridView.RefreshData()

                Me.oGridView.OptionsView.BestFitMaxRowCount = 1000
                Me.oGridView.BestFitColumns()

            Catch ex As Exception
                If System.Diagnostics.Debugger.IsAttached = True Then
                    MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End If
            End Try

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

    End Sub

#End Region

#Region "Initial Grid"

    Private Sub InitGrid()
        'Dim ret As Boolean = Inited
        'If (Inited = True) Then
        '    Return
        'End If
        'Try
        '    Dim bandedView As GridView = GridView1
        '    bandedView.ClearGrouping()
        '    bandedView.ClearDocument()

        '    bandedView.Columns("FTCustCode").SortIndex = 0
        '    bandedView.Columns("FTCustCode").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        '    bandedView.Columns("FTPORef").SortIndex = 1
        '    bandedView.Columns("FTPORef").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending

        '    ' Make the group footers always visible.
        '    bandedView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways
        '    bandedView.Columns("FTCustCode").Group()
        '    bandedView.Columns("FTPORef").Group()

        '    ' Create and setup the 1 summary item.
        '    Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem
        '    item1.FieldName = "FTCustCode"
        '    item1.SummaryType = DevExpress.Data.SummaryItemType.None
        '    item1.DisplayFormat = "{0}"
        '    item1.ShowInGroupColumnFooter = bandedView.Columns("FTCustCode")
        '    bandedView.GroupSummary.Add(item1)

        '    ' Create and setup the 2 summary item.
        '    Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem
        '    item2.FieldName = "FTPORef"
        '    item2.SummaryType = DevExpress.Data.SummaryItemType.None
        '    item2.DisplayFormat = "{0}"
        '    item2.ShowInGroupColumnFooter = bandedView.Columns("FTPORef")
        '    bandedView.GroupSummary.Add(item2)

        '    ' Create and setup the 3 summary item.
        '    Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem
        '    item3.FieldName = "FNQuantity"
        '    item3.SummaryType = DevExpress.Data.SummaryItemType.Sum
        '    item3.DisplayFormat = "{0:n0}"
        '    item3.ShowInGroupColumnFooter = bandedView.Columns("FNQuantity")
        '    bandedView.GroupSummary.Add(item3)

        '    ' Create and setup the 4 summary item.
        '    Dim item4 As GridGroupSummaryItem = New GridGroupSummaryItem
        '    item4.FieldName = "FNExtraQuantity"
        '    item4.SummaryType = DevExpress.Data.SummaryItemType.Sum
        '    item4.DisplayFormat = "{0:n0}"
        '    item4.ShowInGroupColumnFooter = bandedView.Columns("FNExtraQuantity")
        '    bandedView.GroupSummary.Add(item4)

        '    ' Create and setup the 4 summary item.
        '    Dim item5 As GridGroupSummaryItem = New GridGroupSummaryItem
        '    item5.FieldName = "FNTotalQuantity"
        '    item5.SummaryType = DevExpress.Data.SummaryItemType.Sum
        '    item5.DisplayFormat = "{0:n0}"
        '    item5.ShowInGroupColumnFooter = bandedView.Columns("FNTotalQuantity")
        '    bandedView.GroupSummary.Add(item5)


        '    bandedView.Columns("FTCustCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTCustCode")
        '    bandedView.Columns("FTPORef").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTPORef")
        '    bandedView.Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
        '    bandedView.Columns("FNExtraQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNExtraQuantity")
        '    bandedView.Columns("FNTotalQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNTotalQuantity")

        '    bandedView.Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n0}"
        '    bandedView.Columns("FNExtraQuantity").SummaryItem.DisplayFormat = "{0:n0}"
        '    bandedView.Columns("FNTotalQuantity").SummaryItem.DisplayFormat = "{0:n0}"

        '    bandedView.GroupFooterShowMode = GroupFooterShowMode.VisibleIfExpanded
        '    bandedView.ExpandAllGroups()
        '    bandedView.BestFitColumns()
        '    bandedView.RefreshData()
        '    Inited = True

        'Catch ex As System.Exception
        '    Inited = False
        'End Try
    End Sub

#End Region

End Class