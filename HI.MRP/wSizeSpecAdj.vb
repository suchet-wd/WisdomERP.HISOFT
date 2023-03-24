Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraPdfViewer

Public Class wSizeSpecAdj

    Private _CopyPopUp As wCopySizeSpec
    Private _QCPopUp As wSizeSpecQCPopUp
    Private _wNewSize As wNewSize
    Private pStateNew As Boolean = False
    Private _FilePath As String

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        _CopyPopUp = New wCopySizeSpec
        HI.TL.HandlerControl.AddHandlerObj(_CopyPopUp)
        _QCPopUp = New wSizeSpecQCPopUp
        HI.TL.HandlerControl.AddHandlerObj(_QCPopUp)

        _wNewSize = New wNewSize
        HI.TL.HandlerControl.AddHandlerObj(_wNewSize)
        Call HI.ST.Lang.SP_SETxLanguage(_wNewSize)

        Dim oSysLang As New ST.SysLanguage
        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wNewSize.Name.ToString.Trim, _wNewSize)
            Call oSysLang.InsertObjectLanguage(HI.ST.SysInfo.ModuleName, _CopyPopUp.Name.ToString.Trim, _CopyPopUp)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _CopyPopUp.Name.ToString.Trim, _CopyPopUp)
            Call oSysLang.InsertObjectLanguage(HI.ST.SysInfo.ModuleName, _QCPopUp.Name.ToString.Trim, _QCPopUp)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _QCPopUp.Name.ToString.Trim, _QCPopUp)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub wSizeSpecAdj_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.ogvdetail.OptionsView.ShowAutoFilterRow = False
            Call InitGridBreakdown()
            HI.TL.HandlerControl.AddHandlerGridColumnEdit(ogvdetail)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub InitGridBreakdown()
        With ogvdetail
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsMenu.EnableColumnMenu = False
            .OptionsMenu.ShowAutoFilterRowItem = False
            .OptionsFilter.AllowFilterEditor = False
            .OptionsFilter.AllowColumnMRUFilterList = False
            .OptionsFilter.AllowMRUFilterList = False
            .OptionsSelection.MultiSelect = True
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        End With
    End Sub
    Private Sub LoadData()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _colcount As Integer = 0
            Dim SeasonCodeData As String = "" 'Me.FTSeasonCode.Text

            _Cmd = "Exec SP_GETIMPORT_SIZESPEC '" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "','" & HI.UL.ULF.rpQuoted(SeasonCodeData) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTDate.Text) & "','" & HI.UL.ULF.rpQuoted(Me.FTExpCode.Text) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)

            With Me.ogvdetail
                .BeginInit()
                ' .Columns("FTColorway").OptionsColumn.AllowFocus = DevExpress.Utils.DefaultBoolean.False

                For I As Integer = .Columns.Count - 1 To 0 Step -1

                    Select Case .Columns(I).FieldName.ToString.ToUpper
                        Case "FTStyleCode".ToUpper, "FTSeasonCode".ToUpper, "FTDate".ToUpper, "FTEXP".ToUpper, "FTMeasCode".ToUpper, "FTGarmentSpec".ToUpper,
                            "FTPomDesc".ToUpper, "FTMedPattern".ToUpper, "FTTOLPlus".ToUpper, "FTGrandRule1".ToUpper, "FTGrandRule2".ToUpper, "FNMeasSeq".ToUpper, "FTMeasCode_Hide".ToUpper
                            .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                            .Columns(I).OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                            .Columns(I).OptionsColumn.AllowShowHide = DevExpress.Utils.DefaultBoolean.False
                        Case Else
                            .Columns.Remove(.Columns(I))
                    End Select

                Next

                If Not (_oDt Is Nothing) Then
                    For Each Col As DataColumn In _oDt.Columns
                        Select Case Col.ColumnName.ToString.ToUpper
                            Case "FTStyleCode".ToUpper, "FTSeasonCode".ToUpper, "FTDate".ToUpper, "FTEXP".ToUpper, "FTMeasCode".ToUpper, "FTGarmentSpec".ToUpper,
                            "FTPomDesc".ToUpper, "FTMedPattern".ToUpper, "FTTOLPlus".ToUpper, "FTGrandRule1".ToUpper, "FTGrandRule2".ToUpper, "FNMeasSeq".ToUpper, "FTMeasCode_Hide".ToUpper

                            Case Else
                                _colcount = _colcount + 1
                                Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                                With ColG
                                    .Visible = True
                                    .FieldName = Col.ColumnName.ToString
                                    .Name = "FTStyleCode" & Col.ColumnName.ToString
                                    .Caption = Col.ColumnName.ToString
                                End With
                                .Columns.Add(ColG)
                                With .Columns(Col.ColumnName.ToString)
                                    'Dim _Rep As New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit
                                    '_Rep.Precision = 2
                                    '_Rep.Buttons.Item(0).Visible = False
                                    'AddHandler _Rep.EditValueChanging, AddressOf ReposPrice_EditValueChanging
                                    'AddHandler _Rep.KeyDown, AddressOf Rep_KeyDown
                                    '.ColumnEdit = _Rep

                                    .OptionsFilter.AllowAutoFilter = False
                                    .OptionsFilter.AllowFilter = False
                                    '.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                    '.DisplayFormat.FormatString = "{0:n2}"
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                    With .OptionsColumn
                                        .AllowMove = False
                                        '.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                        .AllowShowHide = DevExpress.Utils.DefaultBoolean.False
                                        .AllowEdit = True
                                        .ReadOnly = False
                                        'If Col.ColumnName.ToString.ToUpper = "Total".ToUpper Then
                                        '    .AllowFocus = False
                                        'End If
                                    End With
                                End With
                                '.Columns(Col.ColumnName.ToString).Width = 45
                                '.Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                                '.Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n0}"
                        End Select
                    Next
                End If
                .EndInit()
            End With
            Me.ogcdetail.DataSource = _oDt.Select("", "FNMeasSeq ASC").CopyToDataTable
            ' HI.TL.HandlerControl.AddHandlerGridColumnEdit(ogvdetail)
            Call LoadFile()


        Catch ex As Exception
        End Try
    End Sub


    Private Sub LoadFile()
        Try
            Dim _Cmd As String = "" : Dim _oDT As DataTable
            _Cmd = "Select   FTStyleCode, FTSeasonCode, FNSeq, FBFile , FNFileType ,FTFileName "
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_File WITH(NOLOCK)"
            _Cmd &= vbCrLf & " WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"
            _Cmd &= vbCrLf & " Order by FNSeq asc"
            _oDT = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN,, False)
            Me.oTabcAttachedDetail.TabPages.Clear()
            For Each R As DataRow In _oDT.Rows
                Select Case CInt("0" & R!FNFileType.ToString)
                    Case 0 ' pdf
                        Try

                            Dim _Pdfv As New PdfViewer
                            With _Pdfv
                                .Dock = DockStyle.Fill
                                .LoadDocument(New MemoryStream(CType(R!FBFile, Byte())))
                            End With
                            Dim _Tabpage As New DevExpress.XtraTab.XtraTabPage
                            _Tabpage.Name = "XtraTabPage" & R!FTFileName.ToString
                            _Tabpage.Text = "" & R!FTFileName.ToString
                            _Tabpage.Tag = R!FBFile
                            _Tabpage.Controls.Add(_Pdfv)
                            Me.oTabcAttachedDetail.TabPages.Add(_Tabpage)


                        Catch ex As Exception
                        End Try
                    Case 1 'excel
                        Dim _XlsV As New DevExpress.XtraSpreadsheet.SpreadsheetControl
                        With _XlsV
                            .ReadOnly = True
                            .Dock = DockStyle.Fill
                            .Unit = DevExpress.Office.DocumentUnit.Inch
                            .CreateGraphics.PageUnit = System.Drawing.GraphicsUnit.Document
                            If R!FTFileName.ToString Like "*.xlsx" Then
                                .LoadDocument(New MemoryStream(CType(R!FBFile, Byte())), DevExpress.Spreadsheet.DocumentFormat.Xlsx)
                            Else
                                .LoadDocument(New MemoryStream(CType(R!FBFile, Byte())), DevExpress.Spreadsheet.DocumentFormat.Xls)
                            End If


                        End With
                        Dim _Tabpage As New DevExpress.XtraTab.XtraTabPage
                        _Tabpage.Name = "XtraTabPage" & R!FTFileName.ToString
                        _Tabpage.Text = "" & R!FTFileName.ToString
                        _Tabpage.Tag = R!FBFile
                        _Tabpage.Controls.Add(_XlsV)
                        Me.oTabcAttachedDetail.TabPages.Add(_Tabpage)

                End Select
            Next
            Me.oTabcAttachedDetail.SelectedTabPageIndex = Me.oTabcAttachedDetail.TabPages.Count
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Rep_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)

    End Sub
    Private Sub ReposPrice_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs)
        Try
            With Me.ogvdetail
                Dim _Field As String = .GetSelectedCells(.FocusedRowHandle)(0).FieldName
                If DBNull.Value.Equals(.GetRowCellValue(.FocusedRowHandle, _Field)) Then
                    e.Cancel = True
                Else
                    e.Cancel = False
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            Call LoadData()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If Me.FNHSysStyleSSPId.Text <> "" Then
                If SaveData() Then

                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                    Call LoadData()

                    Me.FTPostState.Checked = False
                    Me.FTPostTime.Text = ""
                    Me.FTUserPost.Text = ""
                    Me.FDPostDate.Text = ""

                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysStyleSSPId_lbl.Text)
                Me.FNHSysStyleSSPId.Focus()
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function SaveData() As Boolean
        Dim _Qry As String = ""
        Dim _oDt As DataTable = Nothing
        Try
            With CType(ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                _oDt = .Copy
            End With
        Catch ex As Exception
        End Try

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try


            Dim _Seq As Integer = 0
            For Each R As DataRow In _oDt.Rows
                _Seq += +1
                For Each Col As DataColumn In _oDt.Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTStyleCode".ToUpper, "FTSeasonCode".ToUpper, "FTDate".ToUpper, "FTEXP".ToUpper, "FTMeasCode".ToUpper, "FTGarmentSpec".ToUpper,
                             "FTPomDesc".ToUpper, "FTMedPattern".ToUpper, "FTTOLPlus".ToUpper, "FTGrandRule1".ToUpper, "FTGrandRule2".ToUpper, "FNMeasSeq".ToUpper, "FTMeasCode_Hide".ToUpper
                        Case Else

                            If (R.Item(Col.ColumnName.ToString)).ToString() <> "" Then

                                _Qry = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_Size"
                                _Qry &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                                _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                                _Qry &= vbCrLf & ",FNQuantity='" & R.Item(Col.ColumnName.ToString) & "'"
                                _Qry &= vbCrLf & "WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString()) & "'"
                                _Qry &= vbCrLf & "and FTSeasonCode='" & HI.UL.ULF.rpQuoted(R!FTSeasonCode.ToString()) & "'"
                                _Qry &= vbCrLf & "and FTMeasCode='" & HI.UL.ULF.rpQuoted(R!FTMeasCode.ToString()) & "'"
                                _Qry &= vbCrLf & "and FTSizeCode='" & HI.UL.ULF.rpQuoted(Col.ColumnName.ToString) & "'"
                                _Qry &= vbCrLf & "And FNMeasSeq=" & Integer.Parse("0" & R!FNMeasSeq.ToString)

                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                    _Qry = "Insert Into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_Size"
                                    _Qry &= "(FTInsUser, FDInsDate, FTInsTime, FTStyleCode, FTSeasonCode, FTMeasCode, FTSizeCode, FNQuantity ,FNMeasSeq)"
                                    _Qry &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString) & "'"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted("") & "'"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMeasCode.ToString) & "'"
                                    _Qry &= vbCrLf & ",'" & Replace(HI.UL.ULF.rpQuoted(Col.ColumnName.ToString), "FTRawMatSizeCode", "") & "'"
                                    _Qry &= vbCrLf & ",'" & R.Item(Col.ColumnName.ToString) & "'"
                                    _Qry &= vbCrLf & "," & _Seq

                                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                        HI.Conn.SQLConn.Tran.Rollback()
                                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                        Return False
                                    End If

                                End If

                            End If

                    End Select

                Next

                _Qry = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_Meas"
                _Qry &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & ",FTGarmentSpec='" & HI.UL.ULF.rpQuoted(R!FTGarmentSpec.ToString) & "'"
                _Qry &= vbCrLf & ",FTPomDesc='" & HI.UL.ULF.rpQuoted(R!FTPomDesc.ToString) & "'"
                _Qry &= vbCrLf & ",FTMedPattern='" & HI.UL.ULF.rpQuoted(R!FTMedPattern.ToString) & "'"
                _Qry &= vbCrLf & ",FTTOLPlus='" & HI.UL.ULF.rpQuoted(R!FTTOLPlus.ToString) & "'"
                _Qry &= vbCrLf & ",FTGrandRule1='" & HI.UL.ULF.rpQuoted(R!FTGrandRule1.ToString) & "'"
                _Qry &= vbCrLf & ",FTGrandRule2='" & HI.UL.ULF.rpQuoted(R!FTGrandRule2.ToString) & "'"
                _Qry &= vbCrLf & "WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString()) & "'"
                _Qry &= vbCrLf & "and FTSeasonCode='" & HI.UL.ULF.rpQuoted(R!FTSeasonCode.ToString()) & "'"
                _Qry &= vbCrLf & "and FTMeasCode='" & HI.UL.ULF.rpQuoted(R!FTMeasCode.ToString()) & "'"
                _Qry &= vbCrLf & "and FNMeasSeq=" & Integer.Parse("0" & R!FNMeasSeq.ToString())

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Qry = "Insert Into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_MEAS"
                    _Qry &= "( FTInsUser, FDInsDate, FTInsTime, FTStyleCode, FTSeasonCode, FTMeasCode, FTGarmentSpec, FTPomDesc, FTMedPattern, FTTOLPlus, FTGrandRule1, FTGrandRule2,FNMeasSeq)"
                    _Qry &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString()) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTSeasonCode.ToString()) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMeasCode.ToString()) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTGarmentSpec.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPomDesc.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTMedPattern.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTTOLPlus.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTGrandRule1.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTGrandRule2.ToString) & "'"
                    _Qry &= vbCrLf & "," & _Seq
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

            Next

            _Qry = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec"
            _Qry &= vbCrLf & "Set FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",FTNote='" & HI.UL.ULF.rpQuoted(Me.FTNote.Text) & "'"
            _Qry &= vbCrLf & ",FTPostState='0' ,FTPostTime='' ,FDPostDate='' ,FTUserPost=''"
            _Qry &= vbCrLf & "WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"
            _Qry &= vbCrLf & "and FTSeasonCode='" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                _Qry = "Insert Into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec"
                _Qry &= "(FTInsUser, FDInsDate, FTInsTime,FTStyleCode, FTSeasonCode, FTDate, FTEXP)"
                _Qry &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FTDate.Text) & "'"
                _Qry &= vbCrLf & ",'" & Me.FTExpCode.Text & "'"
                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            End If

            'Dim _Cmd As String = ""
            'Dim _StyleId As Integer = 0 : Dim _SeaSonId As Integer = 0
            '_Cmd = "Select Top 1 FNHSysStyleId From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) "
            '_Cmd &= vbCrLf & "WHERE  FTStyleCode = '" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"
            '_StyleId = Integer.Parse("0" & HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "0"))
            '_Cmd = "Select Top 1 FNHSysSeasonId  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason WITH(NOLOCK) "
            '_Cmd &= vbCrLf & "WHERE FTSeasonCode='" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text) & "'"
            '_SeaSonId = Integer.Parse("0" & HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "0"))
            '_Cmd = "Select O.FTOrderNo, S.FTSubOrderNo From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S WITH(NOLOCK) ON O.FTOrderNo = S.FTOrderNo"
            '_Cmd &= vbCrLf & " Where O.FNHSysStyleId=" & _StyleId
            '_Cmd &= vbCrLf & " and O.FNHSysSeasonId=" & _SeaSonId

            'For Each R As DataRow In HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Cmd).Rows
            '    _Cmd = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_ApprovedInfo"
            '    _Cmd &= vbCrLf & "Set FTStateApprovedSizeSpec='0'"
            '    _Cmd &= vbCrLf & ",FDDateApprovedSizeSpec=" & HI.UL.ULDate.FormatDateDB
            '    _Cmd &= vbCrLf & ",FTTimeApprovedSizeSpec=" & HI.UL.ULDate.FormatTimeDB
            '    _Cmd &= vbCrLf & ",FTUserApprovedSizeSpec='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '    _Cmd &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
            '    _Cmd &= vbCrLf & "AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
            '    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '    End If
            'Next


            _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_MEAS"
            _Qry &= vbCrLf & "WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"
            _Qry &= vbCrLf & " and FTMeasCode=''"
            HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            Dim _Cmd As String = ""
            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_File"
            _Cmd &= vbCrLf & " where FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"
            _Cmd &= vbCrLf & "  and FTSeasonCode='" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            Dim _Date As String = "" : Dim _Time As String = "" : Dim _FileType As String = "" : Dim _FileName As String = ""
            _Date = HI.Conn.SQLConn.GetFieldOnBeginTrans("Select " & HI.UL.ULDate.FormatDateDB, Conn.DB.DataBaseName.DB_MERCHAN, "")
            _Time = HI.Conn.SQLConn.GetFieldOnBeginTrans("Select " & HI.UL.ULDate.FormatTimeDB, Conn.DB.DataBaseName.DB_MERCHAN, "")
            Dim Data As Byte() : _Seq = 0
            For Each t As DevExpress.XtraTab.XtraTabPage In Me.oTabcAttachedDetail.TabPages
                _Seq += +1

                Data = t.Tag
                'Dim br As New BinaryReader(New FileStream(t.Tag, FileMode.Open, FileAccess.Read))
                'Data = br.ReadBytes(CInt(New FileInfo(t.Tag).Length))
                Dim mrb As MemoryStream = New MemoryStream(CType(t.Tag, Byte()))

                _FileType = Microsoft.VisualBasic.Right(t.Name.ToString, (t.Name.ToString.Length - t.Name.ToString.IndexOf(".")))
                If _FileType Like "*..*" Then
                    _FileType = Replace(_FileType, "..", ".")
                End If
                _FileName = System.IO.Path.GetFileName(t.Text)

                _Cmd = "insert into  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_File (FTInsUser , FDInsDate ,FTInsTime ,FTStyleCode , FTSeasonCode ,FNSeq ,FBFile , FNFileType ,FTFileName)"
                _Cmd &= " Values( @FTInsUser , @FDInsDate ,@FTInsTime ,@FTStyleCode , @FTSeasonCode ,@FNSeq ,@FBFile ,@FNFileType , @FTFileName)"

                Dim cmd As New SqlCommand(_Cmd, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)

                cmd.Parameters.AddWithValue("@FTStyleCode", Me.FNHSysStyleSSPId.Text)

                Dim p As New SqlParameter("@FBFile", SqlDbType.VarBinary)
                p.Value = Data

                Dim p1 As New SqlParameter("@FNSeq", SqlDbType.Int)
                p1.Value = _Seq
                Dim p2 As New SqlParameter("@FTInsUser", SqlDbType.NVarChar)
                p2.Value = HI.ST.UserInfo.UserName
                Dim p3 As New SqlParameter("@FDInsDate", SqlDbType.NVarChar)
                p3.Value = _Date
                Dim p4 As New SqlParameter("@FTInsTime", SqlDbType.NVarChar)
                p4.Value = _Time
                Dim p5 As New SqlParameter("@FTSeasonCode", SqlDbType.NVarChar)
                p5.Value = Me.FTSeasonCode.Text
                Dim p6 As New SqlParameter("@FNFileType", SqlDbType.NVarChar)
                p6.Value = IIf(_FileType = ".pdf", 0, 1)
                Dim p7 As New SqlParameter("@FTFileName", SqlDbType.NVarChar)
                p7.Value = _FileName

                cmd.Parameters.Add(p)
                cmd.Parameters.Add(p1)
                cmd.Parameters.Add(p2)
                cmd.Parameters.Add(p3)
                cmd.Parameters.Add(p4)
                cmd.Parameters.Add(p5)
                cmd.Parameters.Add(p6)
                cmd.Parameters.Add(p7)
                cmd.ExecuteNonQuery()
            Next


            'If Not (Data Is Nothing) Then

            '    _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_File"
            '    _Cmd &= " Set  FBFile=@FBFile"
            '    _Cmd &= " ,FTUpdUser=@FTUpdUser"
            '    _Cmd &= "  ,FDUpdDate=@FDUpdDate"
            '    _Cmd &= "  ,FTUpdTime=@FTUpdTime"
            '    _Cmd &= "  Where FTStyleCode=@FTStyleCode"
            '    _Cmd &= "  and FTSeasonCode=@FTSeasonCode"
            '    _Cmd &= " and FNSeq=@FNSeq"
            '    Dim cmd As New SqlCommand(_Cmd, HI.Conn.SQLConn.Cnn, HI.Conn.SQLConn.Tran)
            '    cmd.Parameters.AddWithValue("@FTStyleCode", Me.FNHSysStyleSSPId.Text)
            '    cmd.Parameters.AddWithValue("@FTSeasonCode", Me.FTSeasonCode.Text)
            '    Dim p6 As New SqlParameter("@FBFile", SqlDbType.VarBinary)
            '    p6.Value = Data

            '    Dim p8 As New SqlParameter("@FTUpdUser", SqlDbType.NVarChar)
            '    p8.Value = HI.ST.UserInfo.UserName
            '    Dim p9 As New SqlParameter("@FDUpdDate", SqlDbType.NVarChar)
            '    p9.Value = HI.UL.ULDate.FormatDateDB
            '    Dim p10 As New SqlParameter("@FTUpdTime", SqlDbType.NVarChar)
            '    p10.Value = HI.UL.ULDate.FormatTimeDB


            '    cmd.Parameters.Add(p6)
            '    cmd.Parameters.Add(p8)
            '    cmd.Parameters.Add(p9)
            '    cmd.Parameters.Add(p10)

            '    cmd.ExecuteNonQuery()
            'End If


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

    Private Sub ocmpostsizespec_Click(sender As Object, e As EventArgs) Handles ocmpostsizespec.Click
        Try
            Dim _State As Boolean = False
            If (Me.VerifyData) Then
                If Not (Me.CheckOwner) Then Exit Sub

                Dim StateCFMToSeason As Boolean = False

                'If Me.FTPostState.Checked Then Exit Sub
                If Me.ogdseason.DataSource Is Nothing Then
                    'HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Season ที่ต้องการทำการ Post Data Size Spec....", 1389261157, Me.Text)
                    'Exit Sub
                Else
                    With CType(Me.ogdseason.DataSource, DataTable)
                        .AcceptChanges()

                        If .Select("FTSelect='1'").Length <= 0 Then
                            'HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Season ที่ต้องการทำการ Post Data Size Spec....", 1389261157, Me.Text)
                            'Exit Sub
                        Else
                            StateCFMToSeason = True
                        End If

                    End With
                End If


                If StateCFMToSeason = False Then
                    Dim _Cmd As String = ""

                    _Cmd = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec"
                    _Cmd &= vbCrLf & "Set FTPostState='1'"
                    _Cmd &= vbCrLf & ",FTPostTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",FDPostDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",FTUserPost='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "Where FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"

                    If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN) = True Then
                        HI.MG.ShowMsg.mInfo("Confirm Data SizeSpec Successfully....", 1549261217, Me.Text)
                        Call LoadDt()
                        Call LoadData()

                    End If
                Else
                    If PostSizeSpec(_State) Then
                        HI.MG.ShowMsg.mInfo("Post Data SizeSpec Successfully....", 1509261217, Me.Text)
                        Call LoadDt(True)
                        Call LoadData()
                    Else
                        If Not (_State) Then
                            HI.MG.ShowMsg.mInfo("Post Data SizeSpec failed....", 1509261157, Me.Text)
                        End If
                    End If
                End If





            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function PostSizeSpec(Optional ByRef _State As Boolean = False) As Boolean
        Try
            Dim _Cmd As String = "" : Dim _oDt As DataTable : Dim _dt As DataTable
            Dim _StyleId As Integer = 0 : Dim tmpsubject As String = "" : Dim tmpmessage As String = "" : Dim _OrderNo As String = "" : Dim _SeaSonId As Integer = 0

            Dim dtss As DataTable
            Dim _dtqc As DataTable = Nothing
            With CType(Me.ogdseason.DataSource, DataTable)
                .AcceptChanges()

                dtss = .Copy

            End With

            _Cmd = "Select Top 1 FNHSysStyleId From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE  FTStyleCode = '" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"
            _StyleId = Integer.Parse("0" & HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "0"))

            '_Cmd = "Select Top 1 FNHSysSeasonId  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason WITH(NOLOCK) "
            '_Cmd &= vbCrLf & " WHERE FTSeasonCode='" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text) & "'"
            '_SeaSonId = Integer.Parse("0" & HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "0"))

            If _StyleId <> 0 Then

                For Each Rxt As DataRow In dtss.Select("FTSelect='1'")

                    _Cmd = "Select  O.FTOrderNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O  WITH (NOLOCK)"
                    _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQC_Meas AS M WITH(NOLOCK) ON O.FTOrderNo = M.FTOrderNo"
                    _Cmd &= vbCrLf & "WHERE O.FNHSysStyleId=" & _StyleId
                    _Cmd &= vbCrLf & "And O.FNHSysSeasonId=" & Val(Rxt!FNHSysSeasonId)

                    _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)

                    If _dtqc Is Nothing Then
                        _dtqc = _dt.Copy
                    Else
                        _dtqc.Merge(_dt)
                    End If


                Next

                If Not (_dtqc Is Nothing) Then
                    If _dtqc.Rows.Count > 0 Then

                        With _QCPopUp
                            ._oDt = _dtqc
                            ._State = False
                            .ShowDialog()

                            If Not (._State) Then
                                Return False
                            End If

                        End With

                    End If
                End If


                For Each Rxt As DataRow In dtss.Select("FTSelect='1'")

                    _Cmd = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_ORDER_SIZESPEC_POST '" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "','" & HI.UL.ULF.rpQuoted(Rxt!FTSeasonCode.ToString) & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)

                    _Cmd = "Select O.FTOrderNo, S.FTSubOrderNo From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) LEFT OUTER JOIN"
                    _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S WITH(NOLOCK) ON O.FTOrderNo = S.FTOrderNo"
                    _Cmd &= vbCrLf & " Where O.FNHSysStyleId=" & _StyleId
                    _Cmd &= vbCrLf & " and O.FNHSysSeasonId=" & Val(Rxt!FNHSysSeasonId.ToString)

                    For Each R As DataRow In HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN).Rows

                        _Cmd = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_ApprovedInfo"
                        _Cmd &= vbCrLf & "Set FTStateApprovedSizeSpec='1'"
                        _Cmd &= vbCrLf & ",FNCntApprovedSizeSpec = Isnull(FNCntApprovedSizeSpec,0)+1"
                        _Cmd &= vbCrLf & ",FDDateApprovedSizeSpec=" & HI.UL.ULDate.FormatDateDB
                        _Cmd &= vbCrLf & ",FTTimeApprovedSizeSpec=" & HI.UL.ULDate.FormatTimeDB
                        _Cmd &= vbCrLf & ",FTUserApprovedSizeSpec='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Cmd &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                        _Cmd &= vbCrLf & "AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"

                        If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN) = False Then

                            _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_ApprovedInfo "
                            _Cmd &= "(FTOrderNo ,FTSubOrderNo ,FTStateApprovedSizeSpec ,FNCntApprovedSizeSpec,FDDateApprovedSizeSpec,FTTimeApprovedSizeSpec,FTUserApprovedSizeSpec)"
                            _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "','1' , NULL"
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                            HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)

                        End If

                    Next

                Next

                _Cmd = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec"
                _Cmd &= vbCrLf & "Set FTPostState='1'"
                _Cmd &= vbCrLf & ",FTPostTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",FDPostDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ",FTUserPost='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "Where FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"

                HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)

                Return True

                '_Cmd = "Select Top 1 FTOrderNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder WITH (NOLOCK)"
                '_Cmd &= vbCrLf & "WHERE FNHSysStyleId=" & _StyleId
                '_Cmd &= vbCrLf & "And FNHSysSeasonId=" & _SeaSonId

                'If (HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "").ToString) <> "" Then

                '    _Cmd = "Select  O.FTOrderNo From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O  WITH (NOLOCK)"
                '    _Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTQC_Meas AS M WITH(NOLOCK) ON O.FTOrderNo = M.FTOrderNo"
                '    _Cmd &= vbCrLf & "WHERE O.FNHSysStyleId=" & _StyleId
                '    _Cmd &= vbCrLf & "And O.FNHSysSeasonId=" & _SeaSonId

                '    _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)

                '    If _dt.Rows.Count > 0 Then

                '        With _QCPopUp
                '            ._oDt = _dt
                '            ._State = False
                '            .ShowDialog()

                '            If Not (._State) Then
                '                Return False
                '            End If

                '        End With

                '    End If

                '    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                '    HI.Conn.SQLConn.SqlConnectionOpen()
                '    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                '    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
                '    Try

                '        _Cmd = "Exec dbo.SP_ORDER_SIZESPEC_POST '" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "','" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text) & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                '        HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                '        '_Cmd = "UPDATE S "
                '        '_Cmd &= vbCrLf & "Set S.FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                '        '_Cmd &= vbCrLf & " ,S.FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                '        '_Cmd &= vbCrLf & " ,S.FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                '        '_Cmd &= vbCrLf & " ,S.FTSizeSpecDesc=  T.FTSizeSpecDesc "
                '        '_Cmd &= vbCrLf & " ,S.FTSizeSpecExtension=  T.FTSizeSpecExtension "
                '        '_Cmd &= vbCrLf & ",S.FTTolerant= T.FTTolerant"
                '        '_Cmd &= vbCrLf & " FROM (Select   A.FTOrderNo ,A.FTSubOrderNo "
                '        '_Cmd &= vbCrLf & ", T.Seq , T.FNHSysMatSizeId , T.FTSizeSpecDesc , T.FTSizeSpecExtension , T.FTTolerant"
                '        '_Cmd &= vbCrLf & "From (Select   O.FTOrderNo, S.FTSubOrderNo,O.FNHSysStyleId ,O.FNHSysSeasonId From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) LEFT OUTER JOIN"
                '        '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S WITH(NOLOCK) ON O.FTOrderNo = S.FTOrderNo"
                '        '_Cmd &= vbCrLf & ")AS A CROSS JOIN "
                '        '_Cmd &= vbCrLf & "(SELECT        H.FTStyleCode, H.FTSeasonCode, M.FTMeasCode ,Q.Seq ,Z.FNHSysMatSizeId ,M.FTMeasCode+' '+M.FTGarmentSpec+' '+M.FTPomDesc AS FTSizeSpecDesc,  S.FNQuantity as FTSizeSpecExtension , M.FTTOLPlus as FTTolerant"
                '        '_Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec AS H WITH (NOLOCK) LEFT OUTER JOIN"
                '        '_Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_MEAS AS M WITH (NOLOCK) ON H.FTStyleCode = M.FTStyleCode AND H.FTSeasonCode = M.FTSeasonCode LEFT OUTER JOIN"
                '        '_Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_Size AS S WITH (NOLOCK) ON H.FTStyleCode = S.FTStyleCode AND H.FTSeasonCode = S.FTSeasonCode AND M.FTMeasCode = S.FTMeasCode"
                '        '_Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS Z WITH(NOLOCK) ON S.FTSizeCode = Z.FTMatSizeCode"
                '        '_Cmd &= vbCrLf & "	INNER JOIN (SELECT        FTStyleCode, FTSeasonCode, FTMeasCode ,ROW_NUMBER() OVER(order by convert(int, FTMeasCode) asc) AS Seq"
                '        '_Cmd &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_MEAS WITH(NOLOCK)"
                '        '_Cmd &= vbCrLf & "where FTStyleCode = '" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "' and FTSeasonCode ='" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text) & "'"
                '        '_Cmd &= vbCrLf & ") AS Q ON M.FTStyleCode = Q.FTStyleCode and M.FTSeasonCode = Q.FTSeasonCode and M.FTMeasCode = Q.FTMeasCode"
                '        '_Cmd &= vbCrLf & ") AS T "
                '        '_Cmd &= vbCrLf & " where A.FNHSysStyleId =" & Integer.Parse("0" & _StyleId) & " And A.FNHSysSeasonId=" & Integer.Parse("0" & _SeaSonId) & ") AS T LEFT OUTER JOIN "
                '        '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_SizeSpec AS S ON T.FTOrderNo = S.FTOrderNo and T.FTSubOrderNo = S.FTSubOrderNo"
                '        '_Cmd &= vbCrLf & "and T.Seq =  S.FNSeq and T.FNHSysMatSizeId = S.FNHSysMatSizeId"
                '        '_Cmd &= vbCrLf & "Where S.FTOrderNo is not null "
                '        'If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                '        'End If


                '        '_Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_SizeSpec"
                '        '_Cmd &= "( FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension, FTTolerant)"
                '        '_Cmd &= vbCrLf & "Select  distinct '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',Convert(varchar(10),Getdate(),111),Convert(varchar(8),Getdate(),114), "
                '        '_Cmd &= vbCrLf & "T.FTOrderNo ,T.FTSubOrderNo , T.Seq ,T.FNHSysMatSizeId , T.FTSizeSpecDesc , T.FTSizeSpecExtension , T.FTTolerant "
                '        '_Cmd &= vbCrLf & " FROM (Select   A.FTOrderNo ,A.FTSubOrderNo "
                '        '_Cmd &= vbCrLf & ", T.Seq , T.FNHSysMatSizeId , T.FTSizeSpecDesc , T.FTSizeSpecExtension , T.FTTolerant"
                '        '_Cmd &= vbCrLf & "From (Select   O.FTOrderNo, S.FTSubOrderNo,O.FNHSysStyleId , O.FNHSysSeasonId  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) LEFT OUTER JOIN"
                '        '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S WITH(NOLOCK) ON O.FTOrderNo = S.FTOrderNo"
                '        '_Cmd &= vbCrLf & ")AS A CROSS JOIN "
                '        '_Cmd &= vbCrLf & "(SELECT        H.FTStyleCode, H.FTSeasonCode, M.FTMeasCode ,Q.Seq ,Z.FNHSysMatSizeId ,M.FTMeasCode+' '+ M.FTGarmentSpec+' '+M.FTPomDesc AS FTSizeSpecDesc,  S.FNQuantity as FTSizeSpecExtension , M.FTTOLPlus as FTTolerant"
                '        '_Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec AS H WITH (NOLOCK) LEFT OUTER JOIN"
                '        '_Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_MEAS AS M WITH (NOLOCK) ON H.FTStyleCode = M.FTStyleCode AND H.FTSeasonCode = M.FTSeasonCode LEFT OUTER JOIN"
                '        ''_Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_Size AS S WITH (NOLOCK) ON H.FTStyleCode = S.FTStyleCode AND H.FTSeasonCode = S.FTSeasonCode AND M.FTMeasCode = S.FTMeasCode"

                '        '_Cmd &= vbCrLf & "   (SELECT        S.FTMeasCode, S.FTSizeCode, S.FTSeasonCode, S.FTStyleCode, Z.FNQuantity"
                '        '_Cmd &= vbCrLf & "FROM            (SELECT        t.FTMeasCode, s.FTSizeCode, t.FTSeasonCode, t.FTStyleCode"
                '        '_Cmd &= vbCrLf & "    FROM            (SELECT        FTMeasCode, FTSeasonCode, FTStyleCode"
                '        '_Cmd &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_MEAS WITH(NOLOCK)) AS t CROSS JOIN"
                '        '_Cmd &= vbCrLf & "  (SELECT DISTINCT FTSizeCode"
                '        '_Cmd &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_Size WITH(NOLOCK)) AS s) AS S LEFT OUTER JOIN"
                '        '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_Size AS Z WITH(NOLOCK) ON S.FTSizeCode = Z.FTSizeCode AND S.FTStyleCode = Z.FTStyleCode AND S.FTSeasonCode = Z.FTSeasonCode AND "
                '        '_Cmd &= vbCrLf & " S.FTMeasCode = Z.FTMeasCode ) AS S"
                '        '_Cmd &= vbCrLf & "  ON H.FTStyleCode = S.FTStyleCode AND H.FTSeasonCode = S.FTSeasonCode AND M.FTMeasCode = S.FTMeasCode "


                '        '_Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize AS Z WITH(NOLOCK) ON S.FTSizeCode = Z.FTMatSizeCode"
                '        '_Cmd &= vbCrLf & "	INNER JOIN (SELECT        FTStyleCode, FTSeasonCode, FTMeasCode ,ROW_NUMBER() OVER(order by convert(int, FTMeasCode) asc) AS Seq"
                '        '_Cmd &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_MEAS WITH(NOLOCK)"
                '        '_Cmd &= vbCrLf & "where FTStyleCode = '" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "' and FTSeasonCode ='" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text) & "'"
                '        '_Cmd &= vbCrLf & ") AS Q ON S.FTStyleCode = Q.FTStyleCode and S.FTSeasonCode = Q.FTSeasonCode and S.FTMeasCode = Q.FTMeasCode"
                '        '_Cmd &= vbCrLf & ") AS T "
                '        '_Cmd &= vbCrLf & " where A.FNHSysStyleId =" & Integer.Parse("0" & _StyleId) & " And A.FNHSysSeasonId=" & Integer.Parse("0" & _SeaSonId) & ") AS T LEFT OUTER JOIN "
                '        '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_SizeSpec AS S ON T.FTOrderNo = S.FTOrderNo and T.FTSubOrderNo = S.FTSubOrderNo"
                '        '_Cmd &= vbCrLf & "and T.Seq =  S.FNSeq and T.FNHSysMatSizeId = S.FNHSysMatSizeId"
                '        '_Cmd &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS B WITH(NOLOCK) ON T.FTOrderNo = B.FTOrderNo and T.FTSubOrderNo = B.FTSubOrderNo"
                '        '_Cmd &= vbCrLf & "and T.FNHSysMatSizeId = B.FNHSysMatSizeId"
                '        '_Cmd &= vbCrLf & "Where S.FTOrderNo is null  AND  T.FNHSysMatSizeId is not null "
                '        'If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                '        '    'HI.Conn.SQLConn.Tran.Rollback()
                '        '    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                '        '    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                '        '    'Return False
                '        'End If

                '        _Cmd = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec"
                '        _Cmd &= vbCrLf & "Set FTPostState='1'"
                '        _Cmd &= vbCrLf & ",FTPostTime=" & HI.UL.ULDate.FormatTimeDB
                '        _Cmd &= vbCrLf & ",FDPostDate=" & HI.UL.ULDate.FormatDateDB
                '        _Cmd &= vbCrLf & ",FTUserPost='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                '        _Cmd &= vbCrLf & "Where FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"
                '        ' _Cmd &= vbCrLf & " and FTSeasonCode ='" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text) & "'"

                '        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                '            'HI.Conn.SQLConn.Tran.Rollback()
                '            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                '            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                '            'Return False
                '        End If

                '        _Cmd = "Select O.FTOrderNo, S.FTSubOrderNo From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) LEFT OUTER JOIN"
                '        _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S WITH(NOLOCK) ON O.FTOrderNo = S.FTOrderNo"
                '        _Cmd &= vbCrLf & " Where O.FNHSysStyleId=" & _StyleId
                '        _Cmd &= vbCrLf & " and O.FNHSysSeasonId=" & _SeaSonId

                '        For Each R As DataRow In HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Cmd).Rows

                '            _Cmd = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_ApprovedInfo"
                '            _Cmd &= vbCrLf & "Set FTStateApprovedSizeSpec='1'"
                '            _Cmd &= vbCrLf & ",FNCntApprovedSizeSpec = Isnull(FNCntApprovedSizeSpec,0)+1"
                '            _Cmd &= vbCrLf & ",FDDateApprovedSizeSpec=" & HI.UL.ULDate.FormatDateDB
                '            _Cmd &= vbCrLf & ",FTTimeApprovedSizeSpec=" & HI.UL.ULDate.FormatTimeDB
                '            _Cmd &= vbCrLf & ",FTUserApprovedSizeSpec='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                '            _Cmd &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                '            _Cmd &= vbCrLf & "AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"

                '            '   _Cmd &= vbCrLf & " AND Isnull(FTStateApprovedSizeSpec,'0') = '1'"
                '            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                '                _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_ApprovedInfo "
                '                _Cmd &= "(FTOrderNo ,FTSubOrderNo ,FTStateApprovedSizeSpec ,FNCntApprovedSizeSpec,FDDateApprovedSizeSpec,FTTimeApprovedSizeSpec,FTUserApprovedSizeSpec)"
                '                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "','" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "','1' , NULL"
                '                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                '                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                '                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                '                HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                '            End If

                '        Next

                '        _Cmd = " Select   O.FTOrderNo  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) LEFT OUTER JOIN"
                '        '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S WITH(NOLOCK) ON O.FTOrderNo = S.FTOrderNo"
                '        '_Cmd &= vbCrLf & " WHERE  O.FNHSysStyleId =" & _StyleId
                '        '_Cmd &= vbCrLf & "And O.FNHSysSeasonId=" & _SeaSonId

                '        'For Each row As DataRow In HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Cmd).Rows

                '        '    If _OrderNo <> "" Then _OrderNo &= ","
                '        '    _OrderNo &= "" & row!FTOrderNo.ToString & ""

                '        'Next

                '        tmpsubject = "New Post SizeSpec From Style No." & Me.FNHSysStyleSSPId.Text & " " ' Season No." & Me.FTSeasonCode.Text & "   "
                '        tmpmessage = "New Post SizeSpec From Style No." & Me.FNHSysStyleSSPId.Text & " " ' Season No." & Me.FTSeasonCode.Text & "   "

                '        'If _OrderNo <> "" Then
                '        '    tmpmessage &= vbCrLf & " Pls Approve Sub OrderNo SizeSpec. in OrderNo (" & _OrderNo & ")"
                '        'End If

                '        tmpmessage &= vbCrLf & "Date :" & HI.Conn.SQLConn.GetFieldOnBeginTrans("Select convert(varchar(10),convert(datetime," & HI.UL.ULDate.FormatDateDB & "),103) ", Conn.DB.DataBaseName.DB_MERCHAN, "")
                '        tmpmessage &= vbCrLf & "By :" & HI.ST.UserInfo.UserName
                '        tmpmessage &= vbCrLf & "Note :" & FTNote.Text
                '        ' HI.MG.ShowMsg.mInfo("Send Mail To Merchandiser Team Complete !!!", 1509261034, Me.Text, Me.FNHSysStyleSSPId.Text & "  " & Me.FTSeasonCode.Text, System.Windows.Forms.MessageBoxIcon.Information)
                '        HI.Conn.SQLConn.Tran.Commit()
                '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


                '        '_Cmd = "    SELECT DISTINCT U.FTUserName"
                '        '_Cmd &= vbCrLf & "FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U WITH (NOLOCK) INNER JOIN"
                '        '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp AS T WITH (NOLOCK) ON U.FNHSysTeamGrpId = T.FNHSysTeamGrpId INNER JOIN"
                '        '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS P WITH (NOLOCK) ON U.FTUserName = P.FTUserName INNER JOIN"
                '        '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionCmp AS C WITH (NOLOCK) ON P.FNHSysPermissionID = C.FNHSysPermissionID"
                '        '_Cmd &= vbCrLf & "WHERE  (C.FNHSysCmpId = " & Integer.Parse(HI.ST.SysInfo.CmpID.ToString) & ")"
                '        '_Cmd &= vbCrLf & " AND U.FNHSysMerTeamId in ("
                '        '_Cmd &= vbCrLf & " SELECT distinct   FNHSysMerTeamId "
                '        '_Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin"
                '        '_Cmd &= vbCrLf & "where FTUserName in ("
                '        '_Cmd &= vbCrLf & "Select FTOrderBy From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder "
                '        '_Cmd &= vbCrLf & "Where FNHSysStyleId =" & _StyleId
                '        '_Cmd &= vbCrLf & "And FNHSysSeasonId=" & _SeaSonId
                '        '_Cmd &= vbCrLf & "))"

                '        '_oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SECURITY)

                '        'For Each R As DataRow In _oDt.Rows
                '        '    If HI.Mail.ClsSendMail.SendMail(HI.ST.UserInfo.UserName, HI.UL.ULF.rpQuoted(R!FTUserName.ToString), tmpsubject, tmpmessage, -1, "") Then
                '        '    End If
                '        'Next

                '        Return True
                '    Catch ex As Exception
                '        Return False
                '    End Try
                'Else
                '    HI.MG.ShowMsg.mInfo("Also not Order Information Style No.....", 1509261313, Me.Text)
                '    _State = True
                '    Return False
                'End If

            Else
                HI.MG.ShowMsg.mInfo("Invalid Style Code. Pls Check.!!!!", 15100904, Me.Text)
            End If

            Return False
        Catch ex As Exception

            Return False
        End Try
    End Function

    Private Function CheckOwner() As Boolean

        If (HI.ST.UserInfo.UserName.ToUpper = FTUpdUser.Text.ToUpper) Or (HI.ST.SysInfo.Admin) Or FTUpdUser.Text.ToUpper = "" Then
            Return True
        Else
            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            _Qry = "SELECT TOP 1  FNHSysMerTeamId  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(FTUpdUser.Text) & "' "

            _Qry2 = "SELECT TOP 1  FNHSysMerTeamId  "
            _Qry2 &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry2 &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "") = HI.Conn.SQLConn.GetField(_Qry2, Conn.DB.DataBaseName.DB_SECURITY, "") Then
                Return True
            Else
                HI.MG.ShowMsg.mProcessError(1411200101, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข Style นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                Return False
            End If
        End If

    End Function

    Private Function VerifyData() As Boolean
        Try
            If Me.FNHSysStyleSSPId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysStyleSSPId_lbl.Text)
                Me.FNHSysStyleSSPId.Focus()
                Return False
            End If
            With CType(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                If .Rows.Count <= 0 Then
                    Return False
                End If
            End With
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Delegate Sub DFNHSysStyleSSPId_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Private Sub FNHSysStyleSSPId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysStyleSSPId.EditValueChanged
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New DFNHSysStyleSSPId_EditValueChanged(AddressOf FNHSysStyleSSPId_EditValueChanged), New Object() {sender, e})
            Else
                'HI.TL.HandlerControl.ClearControl(Me)
                Me.ogcdetail.DataSource = Nothing
                Me.ogcdetail.Refresh()
                Me.oTabcAttachedDetail.TabPages.Clear()
                If FNHSysStyleSSPId.Text <> "" Then
                    Me.FTExpCode.ReadOnly = Not (pStateNew)
                    Call LoadDt()

                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadDt(Optional ByVal state As Boolean = False)
        Try
            If Not (pStateNew) Then
                If Me.FNHSysStyleSSPId.Text <> "" Then

                    Dim _Cmd As String = ""

                    _Cmd = "SELECT  TOP 1  Isnull(FTUpdUser,FTInsUser) AS FTUpdUser , convert(nvarchar(10),convert(datetime,Isnull(FDUpdDate, FDInsDate)),103) AS FDUpdDate ,  ISNULL(FTUpdTime, FTInsTime) AS FTUpdTime "
                    _Cmd &= vbCrLf & ",  FTNote, FTPostState, FTPostTime, FDPostDate, FTUserPost"
                    _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTMPRImportSizeSpec WITH (NOLOCK) "
                    _Cmd &= vbCrLf & " WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"
                    '_Cmd &= vbCrLf & " And FTSeasonCode='" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text) & "'"

                    Call LoadDtInfo(HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN))

                End If
            Else
                If Me.FNHSysStyleSSPId.Text <> "" Then

                    Dim _Cmd As String = ""
                    If Not (state) Then
                        _Cmd = "SELECT  TOP 0  Isnull(FTUpdUser,FTInsUser) AS FTUpdUser , convert(nvarchar(10),convert(datetime,Isnull(FDUpdDate, FDInsDate)),103) AS FDUpdDate ,  ISNULL(FTUpdTime, FTInsTime) AS FTUpdTime "
                        _Cmd &= vbCrLf & ",  FTNote, FTPostState, FTPostTime, FDPostDate, FTUserPost"
                        _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTMPRImportSizeSpec WITH (NOLOCK) "
                        _Cmd &= vbCrLf & " WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"
                        _Cmd &= vbCrLf & " UNION  "
                        _Cmd &= vbCrLf & " select  '" & HI.ST.UserInfo.UserName & "' AS FTUpdUser ," & HI.UL.ULDate.FormatDateDB & " AS FDUpdDate ,  " & HI.UL.ULDate.FormatTimeDB & " AS FTUpdTime "
                        _Cmd &= vbCrLf & ", '' FTNote,'0' FTPostState,'' FTPostTime,'' FDPostDate,'' FTUserPost"
                    Else
                        _Cmd = "SELECT  TOP 1  Isnull(FTUpdUser,FTInsUser) AS FTUpdUser , convert(nvarchar(10),convert(datetime,Isnull(FDUpdDate, FDInsDate)),103) AS FDUpdDate ,  ISNULL(FTUpdTime, FTInsTime) AS FTUpdTime "
                        _Cmd &= vbCrLf & ",  FTNote, FTPostState, FTPostTime, FDPostDate, FTUserPost"
                        _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTMPRImportSizeSpec WITH (NOLOCK) "
                        _Cmd &= vbCrLf & " WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"
                        '_Cmd &= vbCrLf & " And FTSeasonCode='" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text) & "'"
                    End If
                    Call LoadDtInfo(HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN))

                End If

            End If
            Call LoadData()
            Call LoadSeason(Me.FNHSysStyleSSPId.Text.Trim)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadSeason(stylekey As String)

        Dim cmd As String = ""

        cmd = "SELECT '0' AS FTSelect "
        cmd &= vbCrLf & ", SS.FTSeasonCode, SS.FNHSysSeasonId "
        cmd &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder As A WITH(NOLOCK) INNER Join "
        cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As ST  WITH(NOLOCK)  On A.FNHSysStyleId = ST.FNHSysStyleId INNER Join "
        cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SS  WITH(NOLOCK)  ON A.FNHSysSeasonId = SS.FNHSysSeasonId "
        cmd &= vbCrLf & " WHERE ST.FTStyleCode='" & HI.UL.ULF.rpQuoted(stylekey) & "'"
        cmd &= vbCrLf & " Group By SS.FTSeasonCode, SS.FNHSysSeasonId "
        cmd &= vbCrLf & " Order By SS.FTSeasonCode "

        Dim dt As DataTable
        dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_MERCHAN)

        Me.ogdseason.DataSource = dt.Copy
        dt.Dispose()

    End Sub
    Private Sub LoadDtInfo(_Dt As DataTable)
        Try
            Dim _FieldName As String = ""
            For Each R As DataRow In _Dt.Rows
                For Each Col As DataColumn In _Dt.Columns
                    _FieldName = Col.ColumnName.ToString

                    For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                        Select Case HI.ENM.Control.GeTypeControl(Obj)
                            Case ENM.Control.ControlType.ButtonEdit
                                With CType(Obj, DevExpress.XtraEditors.ButtonEdit)

                                    .Text = R.Item(Col).ToString

                                End With

                            Case ENM.Control.ControlType.CalcEdit
                                With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                    .Value = Val(R.Item(Col).ToString)
                                End With
                            Case ENM.Control.ControlType.ComboBoxEdit
                                With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                    Try
                                        .SelectedIndex = Val(R.Item(Col).ToString)
                                    Catch ex As Exception
                                        .SelectedIndex = -1
                                    End Try
                                End With
                            Case ENM.Control.ControlType.CheckEdit
                                With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                    .EditValue = (Integer.Parse(Val(R.Item(Col).ToString))).ToString
                                End With
                            Case ENM.Control.ControlType.MemoEdit, ENM.Control.ControlType.TextEdit
                                Obj.Text = R.Item(Col).ToString
                            Case ENM.Control.ControlType.PictureEdit
                                With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                    Try
                                        .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString)
                                    Catch ex As Exception
                                        .Image = Nothing
                                    End Try
                                End With
                            Case ENM.Control.ControlType.DateEdit
                                Try

                                    With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                        .Text = HI.UL.ULDate.ConvertEN(R.Item(Col).ToString)
                                    End With
                                Catch ex As Exception
                                End Try
                            Case Else
                                Obj.Text = R.Item(Col).ToString
                        End Select
                    Next
                Next

                Exit For
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTSeasonCode_EditValueChanged(sender As Object, e As EventArgs) Handles FTSeasonCode.EditValueChanged
        Try
            Call LoadDt()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmcopy_Click(sender As Object, e As EventArgs) Handles ocmcopy.Click
        Try
            Dim _Cmd As String = ""
            Dim _StateDouble As Boolean = False
            HI.TL.HandlerControl.ClearControl(_CopyPopUp)
            With _CopyPopUp
                .FNHSysStyleSSPId.Text = Me.FNHSysStyleSSPId.Text
                .FTExpCode.Text = Me.FTExpCode.Text
                .FTSeasonCode.Text = Me.FTSeasonCode.Text
                .FTDate.Text = Me.FTDate.Text
                .State = False
                .ShowDialog()
                If .State Then

                    _Cmd = "Select * FRom  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTMPRImportSizeSpec "
                    _Cmd &= vbCrLf & "WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(.FNHSysStyleSSPId.Text) & "'"
                    _Cmd &= vbCrLf & " AND FTSeasonCode=''"
                    Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MEDC)
                    If _oDt.Rows.Count > 0 Then
                        _StateDouble = True
                        If Not HI.MG.ShowMsg.mConfirmProcess("ไซต์สเปคสไตล์นี้มีอยู่แล้ว ", 1705061623, Me.Text) Then
                            Exit Sub
                        End If
                    End If

                    HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                    HI.Conn.SQLConn.SqlConnectionOpen()
                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                    Try
                        Dim _Qry As String = ""
                        If _StateDouble Then

                            _Qry = "delete From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_Size"
                            _Qry &= vbCrLf & "WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(.FNHSysStyleSSPId.Text) & "'"
                            _Qry &= vbCrLf & " AND FTSeasonCode=''"
                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            End If

                            _Qry = "delete from   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_Meas"
                            _Qry &= vbCrLf & "WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(.FNHSysStyleSSPId.Text) & "'"
                            _Qry &= vbCrLf & " AND FTSeasonCode=''"
                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            End If

                            _Qry = "delete from    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec"
                            _Qry &= vbCrLf & "WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(.FNHSysStyleSSPId.Text) & "'"
                            _Qry &= vbCrLf & " AND FTSeasonCode=''"
                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            End If

                        End If

                        _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTMPRImportSizeSpec "
                        _Cmd &= "(FTInsUser, FDInsDate, FTInsTime,  FTStyleCode, FTSeasonCode, FTDate, FTEXP, FTNote, FTPostState, FTPostTime, FDPostDate, FTUserPost)"
                        _Cmd &= vbCrLf & "  SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & ", " & HI.UL.ULDate.FormatTimeDB & ""
                        _Cmd &= vbCrLf & ", '" & .FNHSysStyleSSPId.Text & "', '', '" & HI.UL.ULDate.ConvertEnDB(.FTDate.Text) & "','" & .FTExpCode.Text & "', FTNote, FTPostState, FTPostTime, FDPostDate, FTUserPost"
                        _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTMPRImportSizeSpec"
                        _Cmd &= vbCrLf & "WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"
                        _Cmd &= vbCrLf & " AND FTSeasonCode='" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text) & "'"

                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            HI.MG.ShowMsg.mInfo("Copy SizeSpec failed...", 1509301133, Me.Text)
                            Exit Sub
                        End If

                        _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTMPRImportSizeSpec_MEAS "
                        _Cmd &= "(FTInsUser, FDInsDate, FTInsTime,   FTStyleCode, FTSeasonCode, FTMeasCode, FTGarmentSpec, FTPomDesc, FTMedPattern, FTTOLPlus, FTGrandRule1, "
                        _Cmd &= "FTGrandRule2, FTDescription,FNMeasSeq)"
                        _Cmd &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & ", " & HI.UL.ULDate.FormatTimeDB & ""
                        _Cmd &= vbCrLf & ", '" & .FNHSysStyleSSPId.Text & "', ''"
                        _Cmd &= vbCrLf & " , FTMeasCode, FTGarmentSpec, FTPomDesc, FTMedPattern, FTTOLPlus, FTGrandRule1, "
                        _Cmd &= vbCrLf & "  FTGrandRule2, FTDescription,FNMeasSeq"
                        _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTMPRImportSizeSpec_MEAS"
                        _Cmd &= vbCrLf & "WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"
                        _Cmd &= vbCrLf & " AND FTSeasonCode='" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text) & "'"

                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            HI.MG.ShowMsg.mInfo("Copy SizeSpec failed...", 1509301133, Me.Text)
                            Exit Sub
                        End If

                        _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTMPRImportSizeSpec_Size "
                        _Cmd &= "(FTInsUser, FDInsDate, FTInsTime,   FTStyleCode, FTSeasonCode, FTMeasCode, FTSizeCode, FNQuantity,FNMeasSeq)"
                        _Cmd &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & HI.UL.ULDate.FormatDateDB & ", " & HI.UL.ULDate.FormatTimeDB & ""
                        _Cmd &= vbCrLf & ", '" & .FNHSysStyleSSPId.Text & "', ''"
                        _Cmd &= vbCrLf & ",FTMeasCode, FTSizeCode, FNQuantity,FNMeasSeq"
                        _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTMPRImportSizeSpec_Size "
                        _Cmd &= vbCrLf & "WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"
                        _Cmd &= vbCrLf & " AND FTSeasonCode='" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text) & "'"

                        If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            HI.MG.ShowMsg.mInfo("Copy SizeSpec failed...", 1509301133, Me.Text)
                            Exit Sub
                        End If

                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        HI.MG.ShowMsg.mInfo("Copy SizeSpec Complete...", 1509301132, Me.Text)

                    Catch ex As Exception
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        HI.MG.ShowMsg.mInfo("Copy SizeSpec failed...", 1509301133, Me.Text)
                        Exit Sub
                    End Try

                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
            Me.ogcdetail.DataSource = Nothing
            Me.ogcdetail.Refresh()
            Me.oTabcAttachedDetail.TabPages.Clear()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        Try
            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text) Then
                If FTPostState.Checked Then
                    If Not HI.MG.ShowMsg.mConfirmProcess("ไซต์สเปคมีการอนุมัติใช้งานไปแล้ว คุณแน่ใจที่จะลบข้อมูล", 1705061625, Me.Text) Then
                        Exit Sub
                    End If
                End If

                If DeleteData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    HI.TL.HandlerControl.ClearControl(Me)
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)

                End If
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Function DeleteData() As Boolean

        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try
            Dim _Qry As String = ""
            _Qry = "delete From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_Size"
            _Qry &= vbCrLf & "WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"
            _Qry &= vbCrLf & " AND FTSeasonCode='" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If


            _Qry = "delete from   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_Meas"
            _Qry &= vbCrLf & "WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"
            _Qry &= vbCrLf & " AND FTSeasonCode='" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Qry = "delete from    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec"
            _Qry &= vbCrLf & "WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"
            _Qry &= vbCrLf & " AND FTSeasonCode='" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If

            'Dim _Cmd As String = ""
            'Dim _StyleId As Integer = 0 : Dim _SeaSonId As Integer = 0
            '_Cmd = "Select Top 1 FNHSysStyleId From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle WITH(NOLOCK) "
            '_Cmd &= vbCrLf & "WHERE  FTStyleCode = '" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"

            '_StyleId = Integer.Parse("0" & HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "0"))
            '_Cmd = "Select Top 1 FNHSysSeasonId  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason WITH(NOLOCK) "
            '_Cmd &= vbCrLf & "WHERE FTSeasonCode='" & HI.UL.ULF.rpQuoted(Me.FTSeasonCode.Text) & "'"
            '_SeaSonId = Integer.Parse("0" & HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "0"))

            '_Cmd = "Select O.FTOrderNo, S.FTSubOrderNo From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS S WITH(NOLOCK) ON O.FTOrderNo = S.FTOrderNo"
            '_Cmd &= vbCrLf & " Where O.FNHSysStyleId=" & _StyleId
            '_Cmd &= vbCrLf & " and O.FNHSysSeasonId=" & _SeaSonId
            'For Each R As DataRow In HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Cmd).Rows
            '    _Cmd = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_ApprovedInfo"
            '    _Cmd &= vbCrLf & "Set FTStateApprovedSizeSpec='0'"
            '    _Cmd &= vbCrLf & ",FDDateApprovedSizeSpec=" & HI.UL.ULDate.FormatDateDB
            '    _Cmd &= vbCrLf & ",FTTimeApprovedSizeSpec=" & HI.UL.ULDate.FormatTimeDB
            '    _Cmd &= vbCrLf & ",FTUserApprovedSizeSpec='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '    _Cmd &= vbCrLf & "WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
            '    _Cmd &= vbCrLf & "AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString) & "'"
            '    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '    End If
            'Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub FNHSysStyleSSPId_ButtonClick(sender As Object, e As ButtonPressedEventArgs) Handles FNHSysStyleSSPId.ButtonClick
        Try
            pStateNew = (e.Button.Index = 2)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmbomnewsize_Click(sender As Object, e As EventArgs) Handles ocmbomnewsize.Click
        Try
            HI.ST.Lang.SP_SETxLanguage(_wNewSize)
            With _wNewSize
                .FTSizeBreakDown.Text = ""
                .ProcNew = False
                .ShowDialog()

                If .ProcNew Then
                    Dim _Qry As String = ""
                    Dim _NewSize As String = .FTSizeBreakDown.Text
                    Dim _NewSizeID As String = .FTSizeBreakDown.Properties.Tag.ToString

                    Dim InitDt As DataTable = DirectCast(Me.ogcdetail.DataSource, DataTable)
                    Dim dc As DataColumn
                    Dim dc1 As DataColumn

                    If Me.ogvdetail.Columns.ColumnByFieldName("FTRawMatColorCode" & _NewSize) Is Nothing Then

                        dc = New DataColumn("FTRawMatSizeCode" & _NewSize, System.Type.GetType("System.String"))
                        dc1 = New DataColumn("FNHSysRawMatSizeId" & "FTRawMatSizeCode" & _NewSize, System.Type.GetType("System.String"))

                        dc.Caption = _NewSize
                        dc1.Caption = "FNHSysRawMatColorId"

                        If ogvdetail.Columns(dc.ColumnName) Is Nothing Then

                            Try
                                ogvdetail.Columns.Item(dc.ColumnName).FieldName = dc.ColumnName
                                ogvdetail.Columns.Item(dc1.ColumnName).FieldName = dc1.ColumnName
                                InitDt.Columns.Add(dc.ColumnName)
                                InitDt.Columns.Add(dc1.ColumnName)
                            Catch ex As Exception
                                ogvdetail.Columns.AddField(dc.ColumnName)
                                ogvdetail.Columns(dc.ColumnName).FieldName = dc.ColumnName
                                ogvdetail.Columns(dc.ColumnName).Name = dc.ColumnName
                                ogvdetail.Columns(dc.ColumnName).Caption = dc.Caption
                                ogvdetail.Columns(dc.ColumnName).Visible = True
                                ogvdetail.Columns(dc.ColumnName).Width = 70
                                ogvdetail.Columns(dc.ColumnName).OptionsColumn.AllowShowHide = False
                                ogvdetail.Columns(dc.ColumnName).OptionsColumn.ShowInCustomizationForm = False

                                ogvdetail.Columns.AddField(dc1.ColumnName)
                                ogvdetail.Columns(dc1.ColumnName).FieldName = dc1.ColumnName
                                ogvdetail.Columns(dc1.ColumnName).Name = dc1.ColumnName
                                ogvdetail.Columns(dc1.ColumnName).Caption = dc1.Caption
                                ogvdetail.Columns(dc1.ColumnName).Tag = _NewSizeID
                                ogvdetail.Columns(dc1.ColumnName).Visible = False
                                ogvdetail.Columns(dc1.ColumnName).OptionsColumn.AllowShowHide = False
                                ogvdetail.Columns(dc1.ColumnName).OptionsColumn.ShowInCustomizationForm = False

                                Dim repos As DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit = New DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit()
                                repos.Precision = 2
                                ogvdetail.Columns(dc.ColumnName).ColumnEdit = repos

                                'With repos
                                '    .CharacterCasing = CharacterCasing.Upper
                                '    AddHandler .Click, AddressOf DynamicResponButtone_Gotocus
                                '    AddHandler .ButtonClick, AddressOf DynamicResponButtoneSysHide_ButtonClick
                                '    AddHandler .Leave, AddressOf DynamicResponButtoneditSysHide_Leave
                                'End With

                                InitDt.Columns.Add(dc.ColumnName)
                                InitDt.Columns.Add(dc1.ColumnName)

                            End Try

                        End If

                        CType(Me.ogvdetail.DataSource, DataTable).AcceptChanges()

                    End If
                End If

            End With
        Catch ex As Exception

        End Try
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


    Private _StateProc As Boolean
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

    Private Sub ocmbomdeletesize_Click(sender As Object, e As EventArgs) Handles ocmbomdeletesize.Click
        Try

            With Me.ogvdetail
                If .FocusedRowHandle < 0 Then Exit Sub
                If CheckSize(.FocusedColumn.FieldName.ToString.ToUpper) Then
                    If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบ Size ใช่หรือไม่ ?", 1406030077, .FocusedColumn.Caption) Then
                        Dim Col1 As String = .FocusedColumn.FieldName.ToString
                        'Dim Col2 As String = "FNHSysRawMatSizeId" & "FTRawMatSizeCode" & Microsoft.VisualBasic.Right(Col1, Col1.Length - "FTRawMatSizeCode".Length)
                        Dim dt As DataTable = CType(Me.ogcdetail.DataSource, DataTable)
                        Try
                            ogvdetail.Columns.Remove(ogvdetail.Columns.ColumnByFieldName(Col1))
                            dt.Columns.Remove(Col1)
                        Catch ex As Exception
                        End Try
                        Dim _Qry As String = ""

                        _Qry = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_Size"

                        _Qry &= vbCrLf & "WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"
                        _Qry &= vbCrLf & "and FTSizeCode='" & HI.UL.ULF.rpQuoted(Col1) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                        'Try
                        '    ogvdetail.Columns.Remove(ogvdetail.Columns.ColumnByFieldName(Col2))
                        '    dt.Columns.Remove(Col2)
                        'Catch ex As Exception
                        'End Try
                        CType(Me.ogcdetail.DataSource, DataTable).AcceptChanges()
                    End If
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Function CheckSize(sizecode As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT top 1 FTMatSizeCode, FTMatSizeNameEN AS FTDescription, FNMatSizeSeq, FNHSysMatSizeId"
            _Cmd &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMatSize  with(Nolock)"
            _Cmd &= vbCrLf & " where FTMatSizeCode ='" & HI.UL.ULF.rpQuoted(sizecode) & "'"
            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MASTER).Rows.Count = 1
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub FuncNewEditItem_Click(sender As Object, e As EventArgs) Handles ocmadd.Click
        Try
            If VerifyDataAddrow() Then
                With Me.ogvdetail
                    .AddNewRow()
                    Dim rowHandle As Integer = .GetRowHandle(.DataRowCount)
                    If .IsNewItemRow(rowHandle) Then
                        .SetRowCellValue(rowHandle, "FTStyleCode", Me.FNHSysStyleSSPId.Text)
                        .SetRowCellValue(rowHandle, "FTDate", Me.FTDate.Text)
                        .SetRowCellValue(rowHandle, "FTEXP", Me.FTExpCode.Text)
                    End If
                End With

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FuncRemoveDetail_Click(sender As Object, e As EventArgs) Handles ocmremove.Click
        Try
            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete) = False Then Exit Sub

            With Me.ogvdetail
                Dim _Qry As String = ""
                _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_MEAS"
                _Qry &= vbCrLf & "WHERE FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleSSPId.Text) & "'"
                _Qry &= vbCrLf & " and FTMeasCode='" & .GetRowCellValue(.FocusedRowHandle, "FTMeasCode").ToString & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                .DeleteRow(Me.ogvdetail.FocusedRowHandle)
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Function VerifyDataAddrow() As Boolean
        Try
            If Me.FNHSysStyleSSPId.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysStyleSSPId_lbl.Text)
                Me.FNHSysStyleSSPId.Focus()
                Return False
            End If
            If Me.FTExpCode.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTExpCode_lbl.Text)
                Me.FTExpCode.Focus()
                Return False
            End If

            If Me.FTDate.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTDate_lbl.Text)
                Me.FTDate.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception

        End Try
    End Function

    Private Sub FTExpCode_EditValueChanged(sender As Object, e As EventArgs) Handles FTExpCode.EditValueChanged
        Try
            With Me.ogvdetail
                If .RowCount < 0 And .FocusedRowHandle < 0 Then Exit Sub
                For i As Integer = 0 To (.RowCount - 1)
                    .SetRowCellValue(i, "FTEXP", Me.FTExpCode.Text)
                Next
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTDate_EditValueChanged(sender As Object, e As EventArgs) Handles FTDate.EditValueChanged
        Try
            With Me.ogvdetail
                If .RowCount < 0 And .FocusedRowHandle < 0 Then Exit Sub
                For i As Integer = 0 To (.RowCount - 1)
                    .SetRowCellValue(i, "FTDate", Me.FTDate.Text)
                Next
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmReadDocumentfile_Click(sender As Object, e As EventArgs) Handles ocmReadDocumentfile.Click
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                .Filter = "PDF files |*.pdf|Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"


                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    _FilePath = .FileName
                    Call Readfile()
                Else
                    _FilePath = ""
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Readfile()
        Try
            If _FilePath <> "" Then
                Dim _FileType As String = "" : Dim _Data() As Byte : Dim _FileName As String : Dim _StreamFile As Stream
                _FileType = System.IO.Path.GetExtension(_FilePath)
                _FileName = System.IO.Path.GetFileName(_FilePath)
                Select Case _FileType.ToUpper
                    Case ".XLSX".ToUpper, ".XLS".ToUpper
                        Call _ExcelViewer(_FilePath)
                    Case ".PDF".ToUpper
                        Call _PDFViewer(_FilePath)
                    Case Else
                        HI.MG.ShowMsg.mInfo("Can't Set File Type Pls Check...", 1510301710, Me.Text)
                        Exit Sub
                End Select
            End If
            Me.oTabcAttachedDetail.SelectedTabPageIndex = Me.oTabcAttachedDetail.TabPages.Count
        Catch ex As Exception
        End Try
    End Sub

    Private Sub _PDFViewer(_FileName As String)
        Try
            '  Me.oGrpdetail.Controls.Clear()
            Dim _Pdfv As New PdfViewer
            _Pdfv.Dock = DockStyle.Fill
            _Pdfv.LoadDocument(_FileName)
            Dim _Tabpage As New DevExpress.XtraTab.XtraTabPage
            _Tabpage.Controls.Add(_Pdfv)
            _Tabpage.Name = "XtraTabPage" & _FileName.ToString
            _Tabpage.Text = "" & System.IO.Path.GetFileName(_FileName)
            Dim br As New BinaryReader(New FileStream(_FilePath, FileMode.Open, FileAccess.Read))
            _Tabpage.Tag = br.ReadBytes(CInt(New FileInfo(_FilePath).Length))
            Me.oTabcAttachedDetail.TabPages.Add(_Tabpage)

            '   Me.oGrpdetail.Controls.Add(_Pdfv)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub _ExcelViewer(_FileName As String)
        Try
            '   Me.oGrpdetail.Controls.Clear()
            _FileName = System.IO.Path.GetFileName(_FilePath)
            Dim _Excel As New DevExpress.XtraSpreadsheet.SpreadsheetControl
            _Excel.Dock = DockStyle.Fill
            _Excel.LoadDocument(_FilePath)
            _Excel.ReadOnly = True
            Dim _Tabpage As New DevExpress.XtraTab.XtraTabPage
            _Tabpage.Name = "XtraTabPage" & _FileName.ToString
            _Tabpage.Text = "" & System.IO.Path.GetFileName(_FileName)
            Dim br As New BinaryReader(New FileStream(_FilePath, FileMode.Open, FileAccess.Read))
            _Tabpage.Tag = br.ReadBytes(CInt(New FileInfo(_FilePath).Length))
            _Tabpage.Controls.Add(_Excel)
            Me.oTabcAttachedDetail.TabPages.Add(_Tabpage)
            '    Me.oGrpdetail.Controls.Add(_Excel)
        Catch ex As Exception
            HI.MG.ShowMsg.mInfo(ex.ToString, 1902141317, Me.Text, "")
        End Try
    End Sub

    Private Sub ocmRevovefile_Click(sender As Object, e As EventArgs) Handles ocmRevovefile.Click
        Try
            If HI.MG.ShowMsg.mConfirmProcessDefaultNo("คุณต้องการลบไฟล์ใช่หรือไม่...", 1902141203, "",) = False Then Exit Sub
            oTabcAttachedDetail.TabPages.Remove(oTabcAttachedDetail.SelectedTabPage)

        Catch ex As Exception
            HI.MG.ShowMsg.mInfo(ex.ToString, 1902141317, Me.Text, "")
        End Try
    End Sub
End Class