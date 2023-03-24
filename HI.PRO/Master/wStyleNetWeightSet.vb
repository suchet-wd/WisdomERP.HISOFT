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

Public Class wStyleNetWeightSet
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'HI.TL.HandlerControl.AddHandlerGridColumnEdit(ogv)


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



    Private Sub ocmexit_Click_1(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmLoad_Click(sender As Object, e As EventArgs) Handles ocmLoad.Click
        If VerrifyData() Then
            LoadData()
        End If
    End Sub

    Private Sub wCountryForwarderMaster_Load(sender As Object, e As EventArgs) Handles Me.Load

        ''  LoadData()
    End Sub

    Private Function VerrifyData() As Boolean
        Try

            If Me.FNHSysStyleId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysStyleId_lbl.Text)
                FNHSysStyleId.Focus()
                Return False
            End If

            'If Me.FNHSysStyleIdTo.Text = "" Then
            '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FNHSysStyleIdTo_lbl.Text)
            '    FNHSysStyleIdTo.Focus()
            '    Return False
            'End If


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub LoadData()
        Try
            Dim _Cmd As String = ""
            _Cmd = "Exec  dbo.getloadSizeBreakDownSetNetWeight '" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "' , '" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)


            'With Me.ogv
            '    .OptionsView.ShowAutoFilterRow = False
            '    .OptionsView.ColumnAutoWidth = False
            '    For I As Integer = .Columns.Count - 1 To 0 Step -1
            '        Select Case .Columns(I).FieldName.ToString.ToUpper
            '            Case "FNHSysStyleId".ToUpper, "FTStyleCode".ToUpper, "FTColorway".ToUpper
            '                .Columns(I).AppearanceCell.BackColor = Color.White
            '                .Columns(I).AppearanceCell.ForeColor = Color.Black
            '                .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
            '            Case Else
            '                .Columns.Remove(.Columns(I))
            '        End Select
            '    Next
            '    Dim _colcount As Integer = 0
            '    If Not (_oDt Is Nothing) Then
            '        For Each Col As DataColumn In _oDt.Columns
            '            Select Case Col.ColumnName.ToString.ToUpper
            '                Case "FNHSysStyleId".ToUpper, "FTStyleCode".ToUpper, "FTColorway".ToUpper
            '                Case Else
            '                    _colcount = _colcount + 1
            '                    Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn

            '                    With ColG
            '                        .Visible = True
            '                        .OptionsColumn.AllowEdit = True
            '                        .FieldName = Col.ColumnName.ToString
            '                        .Name = "c" & Col.ColumnName.ToString
            '                        .Caption = Col.ColumnName.ToString
            '                        .VisibleIndex = _colcount
            '                        .ColumnEdit = RepositoryItemCalcQty
            '                    End With
            '                    .Columns.Add(ColG)
            '                    .Columns(Col.ColumnName.ToString).Width = 100
            '            End Select
            '        Next
            '        For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
            '            With GridCol
            '                .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            '            End With
            '        Next
            '    End If
            'End With
            Me.ogc.DataSource = _oDt.Copy
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RepositoryItemCalcQty_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCalcQty.EditValueChanging

    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If SaveDataNet() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                LoadData()
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Function SaveDataNet() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _dt As DataTable
            With DirectCast(Me.ogc.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy

            End With

            For Each R As DataRow In _dt.Select("FTSelect ='1'")
                _Cmd = " update  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "..TPRODMNetWeight "
                _Cmd &= vbCrLf & "Set  FNWeight = " & Val(R!FNWeight)
                _Cmd &= vbCrLf & ",FNNetNetWeight = " & Val(R!FNNetNetWeight)
                _Cmd &= vbCrLf & " where   FNHSysStyleId = " & Val(R!FNHSysStyleId.ToString)
                _Cmd &= vbCrLf & " and    FTColorWay ='" & R!FTColorWay.ToString & "'"
                _Cmd &= vbCrLf & " and    FTSizeBreakDown ='" & R!FTSizeBreakDown.ToString & "'"

                If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_MASTER) = False Then
                    _Cmd = "insert  into " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "..TPRODMNetWeight "
                    _Cmd &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FTColorWay, FTSizeBreakDown, FNWeight ,FNNetNetWeight) "
                    _Cmd &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & "," & Val(R!FNHSysStyleId.ToString)
                    _Cmd &= vbCrLf & ",'" & R!FTColorWay.ToString & "'"
                    _Cmd &= vbCrLf & ",'" & R!FTSizeBreakDown.ToString & "'"
                    _Cmd &= vbCrLf & "," & Val(R!FNWeight)
                    _Cmd &= vbCrLf & "," & Val(R!FNNetNetWeight)
                    HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_MASTER)
                End If
            Next



            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function SaveData() As Boolean
        Try
            Dim _Cmd As String = ""
            With ogv
                _Cmd = ""
                Dim _Value As Double = 0
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case .Columns(I).FieldName.ToString.ToUpper
                        Case "FNHSysStyleId".ToUpper, "FTStyleCode".ToUpper, "FTColorway".ToUpper
                        Case Else
                            Dim rowcount As Integer = .DataRowCount - 1
                            For _i As Integer = 0 To rowcount Step 1
                                Try
                                    _Value = Val(.GetRowCellValue(_i, .Columns(I).FieldName.ToString))
                                Catch ex As Exception
                                    _Value = -1
                                End Try


                                If _Value >= 0 Then
                                    _Cmd &= vbCrLf & " delete from  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "..TPRODMNetWeight "
                                    _Cmd &= vbCrLf & " where   FNHSysStyleId = " & Val(.GetRowCellValue(_i, "FNHSysStyleId"))
                                    _Cmd &= vbCrLf & " and    FTColorWay ='" & (.GetRowCellValue(_i, "FTColorWay")) & "'"
                                    _Cmd &= vbCrLf & " and    FTSizeBreakDown ='" & .Columns(I).Caption.ToString & "'"


                                    _Cmd &= vbCrLf & "insert  into " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "..TPRODMNetWeight "
                                    _Cmd &= vbCrLf & " (FTInsUser, FDInsDate, FTInsTime, FNHSysStyleId, FTColorWay, FTSizeBreakDown, FNWeight) "
                                    _Cmd &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                                    _Cmd &= vbCrLf & "," & Val(.GetRowCellValue(_i, "FNHSysStyleId"))
                                    _Cmd &= vbCrLf & ",'" & (.GetRowCellValue(_i, "FTColorWay")) & "'"
                                    _Cmd &= vbCrLf & ",'" & .Columns(I).Caption.ToString & "'"
                                    _Cmd &= vbCrLf & "," & _Value
                                End If



                            Next
                    End Select
                Next
            End With

            If _Cmd <> "" Then
                If HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MASTER) Then
                    Return True
                Else
                    Return False
                End If
            End If


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub RepositoryItemCalcQty_KeyDown(sender As Object, e As KeyEventArgs) Handles RepositoryItemCalcQty.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.F9
                    'all size
                    With ogv
                        Dim _Size As String = .FocusedColumn.Name
                        Dim _Value As Double = Val(.GetRowCellValue(.FocusedRowHandle, .FocusedColumn))

                        For _i As Integer = 0 To .DataRowCount Step 1
                            .SetRowCellValue(_i, .FocusedColumn, _Value)
                        Next
                    End With

                Case Keys.F10
                    'all colorway
                    With ogv
                        Dim _Size As Integer = Val(.FocusedRowHandle)
                        Dim _Value As Double = Val(.GetRowCellValue(.FocusedRowHandle, .FocusedColumn))
                        For I As Integer = .Columns.Count - 1 To 0 Step -1
                            Select Case .Columns(I).FieldName.ToString.ToUpper
                                Case "FNHSysStyleId".ToUpper, "FTStyleCode".ToUpper, "FTColorway".ToUpper, "FTSizeBreakDown".ToUpper
                                Case Else
                                    .SetRowCellValue(_Size, .Columns(I).FieldName.ToString, _Value)
                            End Select
                        Next
                    End With
                Case Keys.F11
                    'all
                    With ogv
                        Dim _Size As String = .FocusedRowHandle
                        Dim _Value As Double = Val(.GetRowCellValue(.FocusedRowHandle, .FocusedColumn))
                        For I As Integer = .Columns.Count - 1 To 0 Step -1
                            Select Case .Columns(I).FieldName.ToString.ToUpper
                                Case "FNHSysStyleId".ToUpper, "FTStyleCode".ToUpper, "FTColorway".ToUpper, "FTSizeBreakDown".ToUpper
                                Case Else

                                    For _i As Integer = 0 To .DataRowCount Step 1
                                        .SetRowCellValue(_i, .Columns(I).FieldName.ToString, _Value)
                                    Next

                            End Select
                        Next
                    End With
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text) = True Then
                Dim _Cmd As String = ""
                With ogv
                    _Cmd = ""
                    Dim _Value As Double = 0
                    For I As Integer = .Columns.Count - 1 To 0 Step -1
                        Select Case .Columns(I).FieldName.ToString.ToUpper
                            Case "FNHSysStyleId".ToUpper, "FTStyleCode".ToUpper, "FTColorway".ToUpper
                            Case Else
                                Dim rowcount As Integer = .DataRowCount - 1
                                For _i As Integer = 0 To rowcount Step 1
                                    _Cmd &= vbCrLf & " delete from  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "..TPRODMNetWeight "
                                    _Cmd &= vbCrLf & " where   FNHSysStyleId = " & Val(.GetRowCellValue(_i, "FNHSysStyleId"))
                                    _Cmd &= vbCrLf & " and    FTColorWay ='" & (.GetRowCellValue(_i, "FTColorWay")) & "'"
                                    _Cmd &= vbCrLf & " and    FTSizeBreakDown ='" & .Columns(I).Caption.ToString & "'"
                                Next
                        End Select
                    Next
                End With
                If _Cmd <> "" Then
                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_MASTER)
                End If
                LoadData()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmimportexcel_Click(sender As Object, e As EventArgs) Handles ocmimportexcel.Click
        Try
            Dim _Cmd As String = ""
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                .Filter = "Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = True
                Dim dr As DialogResult = .ShowDialog()
                If (dr = System.Windows.Forms.DialogResult.OK) Then

                    Dim _Spls As New HI.TL.SplashScreen("Reading....Please Wait.....", "Import Data From File ")
                    For Each file In .FileNames
                        ' _FileName = .FileName

                        Call ReadXlsfile_Multiple(file, _Spls)

                    Next
                    _Spls.Close()
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ReadXlsfile_Multiple(_fileName As String, ByVal _Spls As HI.TL.SplashScreen)
        Try


            Dim _oDt As New System.Data.DataTable
            Dim _oDtIn As New System.Data.DataTable

            Dim _Qry As String = ""
            Dim _RowDes As Integer = 0
            Dim xlsFilename As String = Path.GetFileName(_fileName)
            _oDt = HI.UL.ReadExcel.Read(_fileName, "Sheet", -1)


            Dim _dt As New DataTable
            With _dt
                .Columns.Add("FTSelect", GetType(String))
                .Columns.Add("FNHSysStyleId", GetType(Integer))
                .Columns.Add("FTStyleCode", GetType(String))
                .Columns.Add("FTColorWay", GetType(String))

                For Each R As DataRow In _oDt.Rows

                    For Each c As DataColumn In _oDt.Columns
                        Select Case R.Item(c.ColumnName).ToUpper
                            Case "FTStyleCode".ToUpper, "FTColorWay".ToUpper
                            Case Else
                                .Columns.Add("" & R.Item(c.ColumnName), GetType(Double))
                        End Select

                    Next

                    Exit For
                Next
                '.Columns.Add("FTSizeBreakDown", GetType(Double))

                '.Columns.Add("FNWeight", GetType(Double))

            End With
            Dim dr As DataRow

            Dim _StyleCode As String = ""
            Dim _StyleCodeHold As String = ""
            Dim _Styleid As Integer = 0

            For Each R As DataRow In _oDt.Rows

                If (_StyleCodeHold <> R.Item(0).ToString) And R.Item(0).ToString <> "" Then
                    _Styleid = HI.Conn.SQLConn.GetField("Select Top 1  FNHSysStyleId   From   " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle with(nolock) where FTStyleCode ='" & R.Item(0).ToString & "'", Conn.DB.DataBaseName.DB_MASTER, "0")
                End If
                If _Styleid > 0 Then
                    dr = _dt.NewRow()
                    dr.Item("FNHSysStyleId") = "0"
                    dr.Item("FNHSysStyleId") = _Styleid
                    dr.Item("FTStyleCode") = R.Item(0).ToString
                    dr.Item("FTColorWay") = R.Item(1).ToString

                    Dim ci As Integer = 2
                    For Each c As DataColumn In _oDt.Columns
                        Select Case c.ColumnName
                            Case "F1".ToUpper, "F2".ToUpper
                            Case Else
                                '.Columns.Add("" & R.Item(c.ColumnName), GetType(Double))
                                dr.Item(_oDt.Rows(0).Item(c.ColumnName)) = Val(R.Item(ci).ToString)
                                ci += +1
                        End Select
                    Next
                    _dt.Rows.Add(dr)
                End If
                _StyleCodeHold = R.Item(0).ToString
            Next
            With Me.ogv
                .OptionsView.ShowAutoFilterRow = False
                .OptionsView.ColumnAutoWidth = False
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case .Columns(I).FieldName.ToString.ToUpper
                        Case "FNHSysStyleId".ToUpper, "FTStyleCode".ToUpper, "FTColorway".ToUpper
                            .Columns(I).AppearanceCell.BackColor = Color.White
                            .Columns(I).AppearanceCell.ForeColor = Color.Black
                            .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                        Case Else
                            .Columns.Remove(.Columns(I))
                    End Select
                Next
                Dim _colcount As Integer = 0
                If Not (_dt Is Nothing) Then
                    For Each Col As DataColumn In _dt.Columns
                        Select Case Col.ColumnName.ToString.ToUpper
                            Case "FNHSysStyleId".ToUpper, "FTStyleCode".ToUpper, "FTColorway".ToUpper
                            Case Else
                                _colcount = _colcount + 1
                                Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn

                                With ColG
                                    .Visible = True
                                    .OptionsColumn.AllowEdit = True
                                    .FieldName = Col.ColumnName.ToString
                                    .Name = "c" & Col.ColumnName.ToString
                                    .Caption = Col.ColumnName.ToString
                                    .VisibleIndex = _colcount
                                    .ColumnEdit = RepositoryItemCalcQty
                                End With
                                .Columns.Add(ColG)
                                .Columns(Col.ColumnName.ToString).Width = 100
                        End Select
                    Next
                    For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                        With GridCol
                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        End With
                    Next
                End If
            End With

            Me.ogc.DataSource = _dt
            Dim _Cmd As String = ""
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub

    Private Sub ocmReadExcelMerCury_Click(sender As Object, e As EventArgs) Handles ocmReadExcelMerCury.Click
        Try
            Dim _Cmd As String = ""
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                .Filter = "Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = True
                Dim dr As DialogResult = .ShowDialog()
                If (dr = System.Windows.Forms.DialogResult.OK) Then

                    Dim _Spls As New HI.TL.SplashScreen("Reading....Please Wait.....", "Import Data From File ")
                    For Each file In .FileNames
                        ' _FileName = .FileName

                        Call ReadXlsfile_MultipleMercury(file, _Spls)

                    Next
                    _Spls.Close()
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub



    Private Function getSizeMap() As DataTable
        Try
            Dim _Cmd As String = " select   FTMapSize, FTMapSizeExtension  from  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & ".dbo.TMERMMapSize  "

            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)
        Catch ex As Exception

        End Try
    End Function

    Private Function getStyle() As DataTable
        Try
            Dim _Cmd As String = " select   FNHSysStyleId, FTStyleCode  from  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMStyle  "

            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Sub ReadXlsfile_MultipleMercury(_fileName As String, ByVal _Spls As HI.TL.SplashScreen)
        Try
            Dim _TabPageSubHead As New DevExpress.XtraTab.XtraTabPage
            Dim _GridDM As New DevExpress.XtraGrid.GridControl
            Dim _GridVDM As New DevExpress.XtraGrid.Views.Grid.GridView
            Dim _GridDD As New DevExpress.XtraGrid.GridControl
            Dim _GridVDD As New DevExpress.XtraGrid.Views.Grid.GridView
            Dim _GrpSum As New DevExpress.XtraEditors.GroupControl
            Dim _GrpDetail As New DevExpress.XtraEditors.GroupControl
            Dim _GrpInfo As New DevExpress.XtraEditors.GroupControl



            Dim _oDt As New System.Data.DataTable
            Dim _oDtIn As New System.Data.DataTable

            Dim _Qry As String = ""
            Dim _RowDes As Integer = 0
            Try
                Dim _FilHold As String = _fileName
                If Microsoft.VisualBasic.Right(_fileName, 5) = ".xlsx" Then

                Else
                    _fileName = Replace(_fileName, ".xls", ".x")
                    _fileName = Replace(_fileName, ".xx", "")
                    _fileName = Replace(_fileName, ".x", "")
                    _fileName = _fileName & ".xlsx"

                    File.Move(_FilHold, _fileName)


                End If

                '   filenew = _fileName

            Catch ex As Exception

            End Try

            Dim xlsFilename As String = Path.GetFileName(_fileName)
            _oDt = HI.UL.ReadExcel.Read(_fileName, "Material Size", -1)

            Dim _dt As New DataTable
            With _dt
                .Columns.Add("FTSelect", GetType(String))
                .Columns.Add("FNHSysStyleId", GetType(Integer))
                .Columns.Add("FTStyleCode", GetType(String))
                .Columns.Add("FTColorWay", GetType(String))
                .Columns.Add("FTSizeBreakDown", GetType(String))
                .Columns.Add("FNWeight", GetType(Double))
                .Columns.Add("FNNetNetWeight", GetType(Double))
            End With
            Dim dr As DataRow
            Dim Size As String = ""
            Dim _dtSizeMat As DataTable = getSizeMap()
            Dim _dtStyleId As DataTable = getStyle()
            Dim i As Integer = 1
            For Each R As DataRow In _oDt.Rows
                If i > 1 Then
                    dr = _dt.NewRow()
                    dr.Item("FTSelect") = "0"
                    For Each Rx As DataRow In _dtStyleId.Select("FTStyleCode ='" & Microsoft.VisualBasic.Left(R.Item(0).ToString(), R.Item(0).ToString().IndexOf("-")) & "'")
                        dr.Item("FNHSysStyleId") = Val(Rx!FNHSysStyleId.ToString)
                    Next

                    dr.Item("FTStyleCode") = Microsoft.VisualBasic.Left(R.Item(0).ToString(), R.Item(0).ToString().IndexOf("-"))
                    dr.Item("FTColorWay") = Replace(R.Item(0).ToString, Microsoft.VisualBasic.Left(R.Item(0).ToString(), R.Item(0).ToString().IndexOf("-") + 1), "")
                    Size = R.Item(1).ToString
                    For Each Rx As DataRow In _dtSizeMat.Select("FTMapSize ='" & Size & "'")
                        Size = Rx!FTMapSizeExtension.ToString
                    Next

                    dr.Item("FTSizeBreakDown") = Size
                    dr.Item("FNWeight") = R.Item(18).ToString
                    dr.Item("FNNetNetWeight") = R.Item(9).ToString
                    _dt.Rows.Add(dr)
                End If
                i += +1

            Next

            Me.ogc.DataSource = _dt

            'TabSub  


        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub


    Private Sub oChkSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles oChkSelectAll.CheckedChanged
        Try

            If _StateSetSelectAll = False Then Exit Sub
            _StateSetSelectAll = False
            '    Me.oChkSelectAll.Checked = False

            Dim _State As String = "0"
            If Me.oChkSelectAll.Checked Then
                _State = "1"
            End If

            With ogc
                If Not (.DataSource Is Nothing) And ogv.RowCount > 0 Then

                    With ogv
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With
                    CType(.DataSource, System.Data.DataTable).AcceptChanges()
                End If
            End With

        Catch ex As Exception
        End Try
        _StateSetSelectAll = True
    End Sub


End Class