Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ButtonEdit
Imports DevExpress.XtraGrid.Filter
Imports System.Data.Common
Imports System.Windows.Forms
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Drawing


Public Class wOrderListingInfo


    Dim View As GridView
    Dim RowsIndex As Double
    Dim TopVisibleIndex As Int32
    Private sFNHSysStyleId As String

    ''' Used Data Adapter to control database

    Dim oleDbDataAdapter1 As DbDataAdapter
    Dim oleDbDataAdapter2 As DbDataAdapter
    Dim dtStyleDetail As DataTable

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

#Region "MAIN PROC"

    Private Sub wOrderListingInfo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
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

    Private Sub Proc_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub FNHSysBuyId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysBuyId.EditValueChanged
        Dim _Str As String = ""
        FNHSysStyleId.Text = ""
        FNHSysStyleId.Properties.Tag = ""

        ogcmatcode.DataSource = Nothing

        _Str = "SELECT TOP 1 FNHSysBuyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy WITH(NOLOCK) WHERE FTBuyCode ='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' "
        FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")

        Call LoadOrderListingInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysStyleId.EditValueChanged
        Dim _Str As String = ""
        If FNHSysBuyId.Text = "" Then Return
        _Str = "SELECT TOP 1 FNHSysBuyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy WITH(NOLOCK) WHERE FTBuyCode ='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' "
        FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
        FNHSysStyleId.Properties.Tag = ""

        If FNHSysStyleId.Text <> "" Then
            _Str = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) WHERE FTStyleCode ='" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text) & "' "
            FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
            If FNHSysStyleId.Properties.Tag.ToString <> "" Then
                'If FTOrderSeq.Properties.Tag.ToString <> "" Then
                Call LoadOrderListingInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
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

            Call LoadOrderListingInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
            'Call LoadMPRInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")

            ogcmatcode.Refresh()
        End If
    End Sub

    Private Sub GetBOMInfo(Optional ByVal Calculated = False)
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

        Call LoadOrderListingInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "", Calculated, Calculated, FilterString)

    End Sub

    Private Sub LoadOrderListingInfo(ByVal _FNHSysCmpId As Integer, ByVal _FTPORef As String, ByVal _FNHSysStyleId As Integer, ByVal _FNHSysCustId As Integer, ByVal _FNHSysBuyId As Integer, ByVal _FNHSysSeasonId As Integer)
        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Dim sqlCmd As New SqlCommand
        sqlCmd.Connection = HI.Conn.SQLConn.Cnn
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_ORDER_LISTING_INFO]"
        sqlCmd.Parameters.AddWithValue("@FNHSysCmpId", _FNHSysCmpId)
        sqlCmd.Parameters.AddWithValue("@FTPORef", _FTPORef)
        sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", _FNHSysStyleId)
        sqlCmd.Parameters.AddWithValue("@FNHSysCustId", _FNHSysCustId)
        sqlCmd.Parameters.AddWithValue("@FNHSysBuyId", _FNHSysBuyId)
        sqlCmd.Parameters.AddWithValue("@FNHSysSeasonId", _FNHSysSeasonId)

        Dim sqlDA As New SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
        sqlDA.SelectCommand = sqlCmd
        Dim dt As New DataTable
        sqlDA.Fill(dt)

        Me.ogcmatcode.DataSource = dt

        Try
            FTUpdUser.Text = dtStyleDetail.Rows(1)("FTUpdUser").ToString
            FDUpdDate.Text = dtStyleDetail.Rows(1)("FDUpdDate").ToString
            FTUpdTime.Text = dtStyleDetail.Rows(1)("FTUpdTime").ToString

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

    Private Sub ocmclear1_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear1.Click
        Me.ogcmatcode.DataSource = Nothing
        Dim xCol As Integer = 0
        Dim Idx As Integer = 0
        Try

            HI.TL.HandlerControl.ClearControl(Me)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        Dim _Str As String = ""

        ogcmatcode.DataSource = Nothing

        If FNHSysBuyId.Text <> "" Then
            _Str = "SELECT TOP 1 FNHSysBuyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy WITH(NOLOCK) WHERE FTBuyCode ='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' "
            FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
        End If
        Call LoadOrderListingInfo(FNHSysBuyId.Properties.Tag.ToString, FNHSysStyleId.Properties.Tag.ToString, "")
    End Sub

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