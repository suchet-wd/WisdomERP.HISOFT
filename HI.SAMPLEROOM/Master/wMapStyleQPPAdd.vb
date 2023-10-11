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

Public Class wMapStyleQPPAdd

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        HI.TL.HandlerControl.AddHandlerGridColumnEdit(ogv)
        ' Call InitGrid()
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
            _Cmd = "Exec  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.sp_get_MapStylelist_RefCode '" & DocNo & "'"
            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MASTER)

            Me.ogc.DataSource = _oDt

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmexit_Click_1(sender As Object, e As EventArgs) Handles ocmexit.Click
        State = False
        Me.Close()
    End Sub



    Private Function checkmapstyle(styleid As Integer) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "select  *  from   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TMASTMap  a  "
            _Cmd &= vbCrLf & " where   FNHSysStyleId=" & Val(styleid)

            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE).Rows.Count = 0
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub wCountryForwarderMasterAdd_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadData()
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If Me.FNHSysSeasonId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysSeasonId_lbl.Text)
                Exit Sub
            End If
            Dim _Cmd As String = ""


            If DocNo = "" Then
                DocNo = HI.Conn.SQLConn.GetField("exec " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.[SP_GET_SYSID] 'TMASTMap' , 'FTMapStyleCode' ,'HITECH_SampleRoom'", Conn.DB.DataBaseName.DB_SYSTEM)
            Else
                _Cmd = " Delete From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TMASTMap where  FTMapStyleCode='" & DocNo & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
            End If



            With DirectCast(Me.ogc.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Select("FNHSysStyleId <> '' ")


                    _Cmd = " Insert into " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TMASTMap "
                    _Cmd &= vbCrLf & " ( FTInsUser, FDInsDate, FTInsTime,   FTMapStyleCode, FNHSysStyleId ,FNHSysSeasonId)"
                    _Cmd &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & " ,'" & DocNo & "'"
                    _Cmd &= vbCrLf & " ," & Val(R!FNHSysStyleId_Hide.ToString)
                    _Cmd &= vbCrLf & " ," & Val(Me.FNHSysSeasonId.Properties.Tag)
                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)



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
            _Cmd = " Delete From  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TMASTMap where  FTMapStyleCode='" & DocNo & "' "
            HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)

            HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            State = True
            Me.Close()
        Catch ex As Exception
            State = False
            Me.Close()
        End Try
    End Sub

    Private Sub FNHSysStyleId_KeyDown(sender As Object, e As KeyEventArgs) Handles FNHSysStyleId.KeyDown
        Try

            If e.KeyCode = Keys.Enter Then
                If FNHSysStyleId.Text <> "" Then
                    If checkmapstyle(Me.FNHSysStyleId.Properties.Tag) Then
                        Dim _dt As DataTable
                        With DirectCast(Me.ogc.DataSource, DataTable)
                            .AcceptChanges()
                            _dt = .Copy
                        End With

                        Dim dr As DataRow = _dt.NewRow
                        dr("FNHSysStyleId_Hide") = Me.FNHSysStyleId.Properties.Tag
                        dr("FNHSysStyleId") = Me.FNHSysStyleId.Text
                        dr("FNHSysStyleId_None") = Me.FNHSysStyleId_None.Text
                        _dt.Rows.Add(dr)

                        Me.ogc.DataSource = _dt
                        _dt.Dispose()


                    Else
                        HI.MG.ShowMsg.mInfo("Style นี้ มีในกลุ่มอืนแล้ว กรุณาตรวจสอบ", 2304181513, Me.Text)
                    End If
                    '  LoadData()
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogv_KeyDown(sender As Object, e As KeyEventArgs) Handles ogv.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                Dim _FNHSysStyleId As Integer = 0
                With Me.ogv

                    .DeleteRow(.FocusedRowHandle)

                End With

            End If
        Catch ex As Exception

        End Try
    End Sub
End Class