Imports System.Windows.Forms
Imports DevExpress.Data
Imports DevExpress.Data.Filtering
Imports DevExpress.Utils
Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Public Class wPopupSelectItemTrucksheet

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        Call InitGrid()
        Call LoadLayout(Me, Me)
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
    Private Sub oChkSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles oChkSelectAll.CheckedChanged
        Try

            If _StateSetSelectAll = False Then Exit Sub
            _StateSetSelectAll = False
            '    Me.oChkSelectAll.Checked = False

            Dim _State As String = "0"
            If Me.oChkSelectAll.Checked Then
                _State = "1"
            End If
            Dim _oDt As New DataTable
            Select Case Me.otb.SelectedTabPage.Name.ToString.ToUpper
                Case "otabporef".ToUpper
                    With ogcsum
                        If Not (.DataSource Is Nothing) And ogvsum.RowCount > 0 Then
                            With ogvsum
                                For I As Integer = 0 To .RowCount - 1
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                                Next
                            End With
                            Dim _Total As Integer = 0
                            Dim _TotalCBM As Double = 0
                            With DirectCast(Me.ogcsum.DataSource, DataTable)
                                .AcceptChanges()
                                For Each R As DataRow In .Select("FTSelect='1'")
                                    _Total += +1
                                    _TotalCBM += +Val(R!TotalCbm.ToString)
                                Next
                                .AcceptChanges()
                                _oDt = .Select("FTSelect='1'").CopyToDataTable



                            End With
                        End If
                    End With
                    With ogc
                        If Not (.DataSource Is Nothing) And ogv.RowCount > 0 Then


                            With DirectCast(Me.ogc.DataSource, DataTable)
                                .AcceptChanges()

                                For Each Rx As DataRow In _oDt.Rows
                                    For Each R As DataRow In .Select("FTCustomerPO='" & Rx!FTCustomerPO.ToString & "'  and FTPOLine='" & Rx!FTPOLine.ToString & "'")
                                        R!FTSelect = "1"
                                    Next
                                Next

                                .AcceptChanges()

                            End With

                        End If
                    End With

                Case "otpdetail".ToUpper


                    With ogc
                        If Not (.DataSource Is Nothing) And ogv.RowCount > 0 Then

                            With ogv
                                For I As Integer = 0 To .RowCount - 1
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                                Next
                            End With

                            Dim _Total As Integer = 0
                            Dim _TotalCBM As Double = 0
                            With DirectCast(Me.ogc.DataSource, DataTable)
                                .AcceptChanges()
                                For Each R As DataRow In .Select("FTSelect='1'")
                                    _Total += +1
                                    _TotalCBM += +Val(R!TotalCbm.ToString)
                                Next
                                _oDt = .Select("FTSelect='1'").CopyToDataTable



                            End With

                        End If
                    End With

                    With ogcsum
                        If Not (.DataSource Is Nothing) And ogvsum.RowCount > 0 Then
                            With DirectCast(Me.ogcsum.DataSource, DataTable)
                                .AcceptChanges()
                                For Each Rx As DataRow In _oDt.Rows
                                    For Each R As DataRow In .Select("FTCustomerPO='" & Rx!FTCustomerPO.ToString & "'  and FTPOLine='" & Rx!FTPOLine.ToString & "'")
                                        R!FTSelect = "1"
                                    Next
                                Next
                                .AcceptChanges()
                            End With
                        End If
                    End With
            End Select


        Catch ex As Exception
        End Try
        _StateSetSelectAll = True
    End Sub


#Region "Procedure"
    Private _ds As New DataSet
    Public Sub LoadData()
        Dim _Qry As String = ""
        Dim _Cmd As String = ""
        Dim dt As New DataTable

        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")

        Try

            _Cmd = "Select  *  from GET_TrucksheetToMerge()"

            dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_FG)

            '_Qry = "    Select  FTPORef as FTCustomerPO , FTNikePOLineItem as FTPOLine FROM     " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FG) & ".dbo. TFGTTruckSheet_SUM with(nolock) where FTTruckSheetNo='" & DocNo & "'   "
            '_oWDb = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_FG)
            'If _oWDb.Rows.Count > 0 Then
            '    _oDt = _oWDb.Copy
            'End If
            Dim _Total As Integer = 0
            Dim _TotalCBM As Double = 0
            Me.ogcsum.DataSource = dt.Copy



            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try

        dt.Dispose()

    End Sub



#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        _State = False
        Me.Close()
    End Sub



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
    Private Sub ocmsavedocument_Click(sender As Object, e As EventArgs) Handles ocmselect.Click
        If Not (Me.ogcsum.DataSource Is Nothing) Then
            With DirectCast(Me.ogcsum.DataSource, DataTable)
                .AcceptChanges()
                oDtselect = .Select("FTSelect ='1'").CopyToDataTable
            End With


            _State = True
            Me.Close()
        End If
    End Sub





    Private Sub RepositoryItemsFTSelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemsFTSelect.EditValueChanging
        Try

            With ogv
                If .FocusedRowHandle < -1 Then Exit Sub
                .SetRowCellValue(.FocusedRowHandle, "FTSelect", e.NewValue)
                Dim _Total As Integer = 0
                Dim _TotalCBM As Double = 0
                With DirectCast(Me.ogc.DataSource, DataTable)
                    .AcceptChanges()
                    For Each R As DataRow In .Select("FTSelect='1'")
                        _Total += +1
                        _TotalCBM += +Val(R!TotalCbm.ToString)
                    Next


                End With
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private pDt As New DataTable
    Private Sub RepositoryItemCheckEdit1_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCheckEdit1.EditValueChanging
        Try

            With ogvsum
                If .FocusedRowHandle < -1 Then Exit Sub
                .SetRowCellValue(.FocusedRowHandle, "FTSelect", e.NewValue)
                If e.NewValue = "1" Then
                    Dim _PORef As String = .GetRowCellValue(.FocusedRowHandle, "FTCustomerPO")
                    Dim _POLine As String = .GetRowCellValue(.FocusedRowHandle, "FTPOLine")

                    Dim dt As DataTable
                    Dim _Total As Integer = 0
                    Dim _TotalCBM As Double = 0
                    With DirectCast(Me.ogcsum.DataSource, DataTable)
                        .AcceptChanges()
                        For Each R As DataRow In .Select("FTSelect='1'")
                            _Total += +Val(R!FNTotalCarton.ToString)
                            _TotalCBM += +Val(R!TotalCbm.ToString)
                        Next

                        Me.ogc.DataSource = pDt
                        With DirectCast(Me.ogc.DataSource, DataTable)
                            .AcceptChanges()
                            For Each R As DataRow In .Select("FTCustomerPO='" & _PORef & "' and FTPOLine='" & _POLine & "'")
                                R!FTSelect = "1"
                                _Total += +1
                                _TotalCBM += +Val(R!TotalCbm.ToString)
                            Next
                            .AcceptChanges()
                            dt = .Copy
                        End With
                        Me.ogc.DataSource = dt.Select("FTSelect='1'").CopyToDataTable

                    End With
                Else
                    Dim _PORef As String = .GetRowCellValue(.FocusedRowHandle, "FTCustomerPO")
                    Dim _POLine As String = .GetRowCellValue(.FocusedRowHandle, "FTPOLine")

                    Dim dt As DataTable
                    Dim _Total As Integer = 0
                    Dim _TotalCBM As Double = 0
                    With DirectCast(Me.ogcsum.DataSource, DataTable)
                        .AcceptChanges()
                        For Each R As DataRow In .Select("FTCustomerPO='" & _PORef & "' and FTPOLine='" & _POLine & "' and FTSelect='0'")
                            _Total += +Val(R!FNTotalCarton.ToString)
                            _TotalCBM += +Val(R!TotalCbm.ToString)
                        Next

                        Me.ogc.DataSource = pDt
                        With DirectCast(Me.ogc.DataSource, DataTable)
                            .AcceptChanges()
                            For Each R As DataRow In .Select("FTCustomerPO='" & _PORef & "' and FTPOLine='" & _POLine & "' and FTSelect='1'")
                                R!FTSelect = "0"
                                '_Total += +1
                                '_TotalCBM += +Val(R!TotalCbm.ToString)
                            Next
                            .AcceptChanges()
                            dt = .Copy
                        End With
                        Me.ogc.DataSource = dt.Select("FTSelect='1'").CopyToDataTable

                    End With
                End If

            End With

        Catch ex As Exception

        End Try
    End Sub



    Private Sub SaveLayout(ByVal ObjParent As Object, ByVal MainParent As Object)

        On Error Resume Next
        For Each Obj As Object In ObjParent.Controls
            Select Case True
                Case (TypeOf Obj Is DevExpress.XtraGrid.GridControl)
                    HI.UL.AppRegistry.SaveLayoutGridToRegistry(MainParent, CType(Obj, DevExpress.XtraGrid.GridControl).MainView)
                Case False
            End Select

            If Obj.Controls.count > 0 Then
                SaveLayout(Obj, MainParent)
            End If
        Next


    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        Try
            SaveLayout(Me, Me)
            HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadLayout(ByVal ObjParent As Object, ByVal MainParent As Object)

        On Error Resume Next
        For Each Obj As Object In ObjParent.Controls
            Select Case True
                Case (TypeOf Obj Is DevExpress.XtraGrid.GridControl)

                    Call HI.UL.AppRegistry.LoadLayoutGridFromRegistry(MainParent, CType(Obj, DevExpress.XtraGrid.GridControl).MainView)

                Case False
            End Select

            If Obj.Controls.count > 0 Then
                LoadLayout(Obj, MainParent)
            End If
        Next


    End Sub


End Class