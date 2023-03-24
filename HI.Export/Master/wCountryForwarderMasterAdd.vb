Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.Data
Imports DevExpress.Data.Filtering
Imports DevExpress.Utils
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class wCountryForwarderMasterAdd

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        HI.TL.HandlerControl.AddHandlerGridColumnEdit(ogv)
        Call InitGrid()
    End Sub

#Region "Initial Grid"

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = ""
        Dim sFieldSumAmt As String = ""

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        Dim sFieldGrpSumAmt As String = ""
        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogv
            .ClearGrouping()
            .ClearDocument()

            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldCustomSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSumAmt.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldCustomGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSumAmt.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            '.OptionsSelection.MultiSelect = True
            '.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
            .ExpandAllGroups()
            .RefreshData()


        End With




    End Sub
#End Region
    Private _DocNo As String = ""
    Public Property DocNo As String
        Get
            Return _DocNo
        End Get
        Set(value As String)
            _DocNo = value
        End Set
    End Property

    Private _WhId As Integer = 0
    Public Property WhId As Integer
        Get
            Return _WhId
        End Get
        Set(value As Integer)
            _WhId = value
        End Set
    End Property

    Public _CmpId As Integer = 0
    Public Property CmpId As Integer
        Get
            Return _CmpId
        End Get
        Set(value As Integer)
            _CmpId = value
        End Set
    End Property

    Private _StateSetSelectAll As Boolean = True



    Private _oDtselect As DataTable = Nothing
    Public Property oDtselect As DataTable
        Get
            Return _oDtselect
        End Get
        Set(value As DataTable)
            _oDtselect = value
        End Set
    End Property
    Private _State As Boolean = False
    Public Property State As Boolean
        Get
            Return _State
        End Get
        Set(value As Boolean)
            _State = value
        End Set
    End Property

    Private Sub LoadData()
        Try
            Dim _Cmd As String = ""
            _Cmd = "Exec  get_load_TCMMCountryForwarderById " & Val(Me.FNHSysCountryId.Properties.Tag.ToString)
            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MASTER)

            Me.ogc.DataSource = _oDt

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmexit_Click_1(sender As Object, e As EventArgs) Handles ocmexit.Click
        State = False
        Me.Close()
    End Sub

    Private Sub FNHSysCountryId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCountryId.EditValueChanged
        Try
            If FNHSysCountryId.Text <> "" Then

                LoadData()
                Dim _Cmd As String = ""
                _Cmd = " Select top 1  FTRemark  From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCMMCountryForwarder "
                _Cmd &= vbCrLf & " where  FNHSysCountryId=" & Val(Me.FNHSysCountryId.Properties.Tag)
                Me.FTRemark.Text = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER)


            End If



        Catch ex As Exception

        End Try
    End Sub

    Private Sub wCountryForwarderMasterAdd_Load(sender As Object, e As EventArgs) Handles Me.Load
        RemoveHandler RepositoryItemButtonFNHSysSuplId.Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtoneditSysHide_Leave
        Me.ogv.OptionsView.ShowAutoFilterRow = False
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            Dim _Cmd As String = ""

            With DirectCast(Me.ogc.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Select("FNHSysSuplId <> '' ")
                    Dim _Id As Integer = 0
                    _Id = HI.Conn.SQLConn.GetField("exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.[SP_GET_SYSID] 'TCMMCountryForwarder' , 'FNHSysCountryForwarderId' ,'HITECH_MASTER'", Conn.DB.DataBaseName.DB_SYSTEM)
                    _Cmd = " Delete From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCMMCountryForwarder "
                    _Cmd &= vbCrLf & " where  FNHSysCountryId=" & Val(Me.FNHSysCountryId.Properties.Tag)
                    _Cmd &= vbCrLf & " and FNHSysVenderPramId=" & Val(R!FNHSysVenderPramId_Hide.ToString)
                    _Cmd &= vbCrLf & " "
                    _Cmd &= vbCrLf & " Insert into " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCMMCountryForwarder "
                    _Cmd &= vbCrLf & " ( FTInsUser, FDInsDate, FTInsTime,   FNHSysCountryForwarderId, FNHSysCountryId, FNHSysSuplId, FTRemark, FTStateActive, FNHSysVenderPramId)"
                    _Cmd &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & " ," & _Id
                    _Cmd &= vbCrLf & " ," & Val(Me.FNHSysCountryId.Properties.Tag)
                    _Cmd &= vbCrLf & " ," & Val(R!FNHSysSuplId_Hide.ToString)
                    _Cmd &= vbCrLf & " ,'" & Me.FTRemark.Text & "'"
                    _Cmd &= vbCrLf & " ,'1'"
                    _Cmd &= vbCrLf & " ," & Val(R!FNHSysVenderPramId_Hide.ToString)

                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MASTER)
                Next
            End With

            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            State = True
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try

            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text) = False Then Exit Sub

            Dim _Cmd As String = ""
            _Cmd = " Delete From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCMMCountryForwarder "
            _Cmd &= vbCrLf & " where  FNHSysCountryId=" & Val(Me.FNHSysCountryId.Properties.Tag)
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MASTER)

            HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            State = True
            Me.Close()
        Catch ex As Exception
            State = False
            Me.Close()
        End Try
    End Sub
End Class