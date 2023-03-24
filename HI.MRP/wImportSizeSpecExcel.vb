Imports System.Windows.Forms
Imports DevExpress.Spreadsheet
'Imports Microsoft.Office.Interop

Public Class wImportSizeSpecExcel
    Private _wMapSizeImportOrder As HI.MER.wMapSizeImportOrder
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
 
    End Sub
    Private Sub wImportSizeSpecExcel_Load(sender As Object, e As EventArgs) Handles Me.Load
    End Sub

#Region "Command"
    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmimportexcel.Click
        Try
            Dim book As IWorkbook = SpreadsheetControl1.Document
            If book.Path <> "" Then
                If SaveData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mInfo("Pls,Select File Size Spec....", 1509221432, Me.Text)
            End If

        Catch ex As Exception
        End Try
    End Sub
    Private Sub ocmReadExcel_Click(sender As Object, e As EventArgs) Handles ocmReadExcel.Click
        Try
            If Me.FTFilePath.Text <> "" Then
                Call LoadfileExcel(Me.FTFilePath.Text)
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region

    Private Sub LoadfileExcel(filename As String)
        Try
            Dim book As IWorkbook = SpreadsheetControl1.Document
            SpreadsheetControl1.BeginUpdate()
            Try
                book.LoadDocument(filename)
            Finally
                SpreadsheetControl1.EndUpdate()
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Private Function SaveData() As Boolean
        'SaveDataDt()
        Dim _Spls As New HI.TL.SplashScreen("Save Data... Please Wait... ")
        Try
            Dim Dt As DataTable
            Dim _Cmd As String = ""
            Dim book As IWorkbook = SpreadsheetControl1.Document
            Dim _StyleCode As String = "" : Dim _SeasonCode As String = "" : Dim _Exp As String = "" : Dim _Date As String = ""
            Dt = HI.UL.ReadExcel.Read(book.Path.ToString, "Measurements")
            With book.Worksheets("Measurements")
                _StyleCode = .Cells(2, 18).DisplayText
                _SeasonCode = .Cells(4, 13).DisplayText
                _Exp = .Cells(4, 18).DisplayText
                _Date = HI.UL.ULDate.ConvertEnDB(.Cells(5, 18).Value.ToString)
            End With

            'HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FG)
            'HI.Conn.SQLConn.SqlConnectionOpen()
            'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            'HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            'If _StyleCode <> "" And _SeasonCode <> "" Then
            '    Call SaveImportData(_StyleCode, "", _Exp, _Date)
            'Else
            '    Return False
            'End If

            Call SaveImportData(_StyleCode, "", _Exp, _Date)

            Dim row As Integer = 8 : Dim _Meas As String = "" : Dim _GarmentSpec As String = "" : Dim _PomDesc As String = "" : Dim _MedPattern As String = ""
            Dim _Tol As String = "" : Dim _Grads1 As String = "" : Dim _Grand2 As String = "" : Dim _Description As String = ""
            Dim rownull As Integer = 0 : Dim _Seq As Integer = 0 : Dim _MeasHold As String = ""
            Dim CellData As String = ""
            With book.Worksheets("Measurements")
                For i As Integer = 1 To 9999999

                    If .Rows(row).RowHeight > 0 Then

                        CellData = .Cells(row, 0).Value.ToString().Trim
                        If CellData = "CODE" Then
                            For x As Integer = row To 9999999
                                row = row + 1

                                _Meas = .Cells(row, 0).Value.ToString()
                                _GarmentSpec = .Cells(row, 1).Value.ToString()
                                _PomDesc = .Cells(row, 4).Value.ToString()
                                _MedPattern = .Cells(row, 7).Value.ToString()
                                _Tol = .Cells(row, 8).Value.ToString()
                                _Grads1 = .Cells(row, 9).Value.ToString()
                                _Grand2 = .Cells(row, 10).Value.ToString()

                                'If _Meas <> "" And IsNumeric(_Meas) Then
                                If _Meas <> "" Then

                                    rownull = 0
                                    _Seq += +1
                                    'If _MeasHold = _Meas Then
                                    '    _Seq += +1
                                    'Else
                                    '    _Seq = 0
                                    'End If
                                    'Call SaveImportData_MEAS(_GarmentSpec, _PomDesc, _MedPattern, _Tol, _Grads1, _Grand2, _StyleCode, _SeasonCode, _Meas, _Seq)

                                    Call SaveImportData_MEAS(_GarmentSpec, _PomDesc, _MedPattern, _Tol, _Grads1, _Grand2, _StyleCode, "", _Meas, _Seq)

                                    _MeasHold = _Meas

                                Else

                                    Exit For
                                End If
                            Next

                        Else
                            If _Meas = "" Then
                                rownull += +1
                            End If
                            _MeasHold = ""
                        End If

                        If rownull >= 4 Then
                            Exit For
                        End If

                    End If

                    row += +1

                Next

            End With

            Dim _oDtSize As New DataTable
            With _oDtSize
                .Columns.Add("StyleCode", GetType(String))
                .Columns.Add("SeasonCode", GetType(String))
                .Columns.Add("Meas", GetType(String))
                .Columns.Add("SizeCode", GetType(String))
                .Columns.Add("Quantity", GetType(String))
                .Columns.Add("SizeCodeMap", GetType(String))
                .Columns.Add("Seq", GetType(Integer))
                .Columns.Add("DataType", GetType(Integer))
            End With

            row = 8 : Dim rowCode As Integer = 8 : Dim _SizeCode As String = "" : Dim _Quantity As String = ""
            _Seq = 0
            Dim _DataType As Integer = 0
            Dim StateAdd As Boolean = False

            With book.Worksheets("Measurements")
                For i As Integer = 1 To 9999999
                    If .Cells(row, 0).Value.ToString().Trim = "CODE" Then

                        rownull = 0
                        rowCode = row
                        _DataType = _DataType + 1

                        For x As Integer = 9 To 9999999

                            row += +1

                            If .Rows(row).RowHeight > 0 Then

                                _Meas = .Cells(row, 0).Value.ToString()
                                '  If IsNumeric(_Meas) Then
                                If (_Meas <> "") Then
                                    _Seq += +1
                                    StateAdd = True

                                    'If _MeasHold = _Meas Then
                                    '    _Seq += +1
                                    'Else
                                    '    _Seq = 0
                                    'End If

                                    For y As Integer = 11 To 9999999

                                        _SizeCode = .Cells(rowCode, y).Value.ToString()

                                        If _SizeCode <> "" Then

                                            _Quantity = .Cells(row, y).Value.ToString()
                                            ' _Quantity = Format(_Quantity, "0.0")
                                            If _Quantity <> "" Then

                                                Call validateSize(_StyleCode, _SeasonCode, _Meas, _SizeCode, _Quantity, _oDtSize, _Seq, _DataType)
                                                'Call SaveImportData_Size(_StyleCode, _SeasonCode, _Meas, _SizeCode, _Quantity)
                                                'Else
                                                '    Exit For
                                            End If

                                        Else
                                            Exit For
                                        End If

                                    Next

                                    _MeasHold = _Meas
                                Else
                                    Exit For
                                End If

                            End If

                        Next
                    Else

                        If .Cells(row, 0).Value.ToString() = "" Then
                            rownull += +1

                        End If

                        If rownull >= 4 Then
                            Exit For
                        End If

                    End If
                    row += +1
                Next
            End With

            If _oDtSize.Rows.Count > 0 Then
                Call SaveImportData_Size(_oDtSize, _Spls)
            End If


            'HI.Conn.SQLConn.Tran.Commit()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            Return True
        Catch ex As Exception
            _Spls.Close()
            Return False
        End Try
    End Function

    Private Function SaveDataDt() As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Save Data... Please Wait... ")
        Try
            Dim Dt As DataTable
            Dim _Cmd As String = ""
            Dim book As IWorkbook = SpreadsheetControl1.Document
            Dim _StyleCode As String = "" : Dim _SeasonCode As String = "" : Dim _Exp As String = "" : Dim _Date As String = ""
            Dt = HI.UL.ReadExcel.Read(book.Path.ToString, "Measurements")
            With Dt
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    If Not (IsNumeric(R.Item(0))) Then
                        R.Delete()
                    End If
                Next
                .AcceptChanges()
                .BeginInit()
                For i As Integer = 0 To .Columns.Count
                    If .Rows(0).Item(i).ToString = "" Then
                        .Columns.RemoveAt(i)
                    End If
                Next
                .EndInit()
            End With
  
            With book.Worksheets("Measurements")
                _StyleCode = .Cells(2, 18).DisplayText
                _SeasonCode = .Cells(4, 13).DisplayText
                _Exp = .Cells(4, 18).DisplayText
                _Date = HI.UL.ULDate.ConvertEnDB(.Cells(5, 18).Value.ToString)
            End With

            'HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_FG)
            'HI.Conn.SQLConn.SqlConnectionOpen()
            'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            'HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            If _StyleCode <> "" And _SeasonCode <> "" Then
                Call SaveImportData(_StyleCode, _SeasonCode, _Exp, _Date)
            Else
                Return False
            End If

            Dim row As Integer = 8 : Dim _Meas As String = "" : Dim _GarmentSpec As String = "" : Dim _PomDesc As String = "" : Dim _MedPattern As String = ""
            Dim _Tol As String = "" : Dim _Grads1 As String = "" : Dim _Grand2 As String = "" : Dim _Description As String = ""
            Dim rownull As Integer = 0 : Dim _Seq As Integer = 0 : Dim _MeasHold As String = ""
            With book.Worksheets("Measurements")
                For i As Integer = 1 To 9999999
                    row += +1
                    _Meas = .Cells(row, 0).Value.ToString()
                    _GarmentSpec = .Cells(row, 1).Value.ToString()
                    _PomDesc = .Cells(row, 4).Value.ToString()
                    _MedPattern = .Cells(row, 7).Value.ToString()
                    _Tol = .Cells(row, 8).Value.ToString()
                    _Grads1 = .Cells(row, 9).Value.ToString()
                    _Grand2 = .Cells(row, 10).Value.ToString()
                    If _Meas <> "" And IsNumeric(_Meas) Then
                        rownull = 0
                        If _MeasHold = _Meas Then
                            _Seq += +1
                        Else
                            _Seq = 0
                        End If
                        Call SaveImportData_MEAS(_GarmentSpec, _PomDesc, _MedPattern, _Tol, _Grads1, _Grand2, _StyleCode, _SeasonCode, _Meas, _Seq)
                        _MeasHold = _Meas
                    Else
                        If _Meas = "" Then
                            rownull += +1
                        End If
                    End If
                    If rownull >= 2 Then
                        Exit For
                    End If
                Next
            End With

            Dim _oDtSize As New DataTable
            With _oDtSize
                .Columns.Add("StyleCode", GetType(String))
                .Columns.Add("SeasonCode", GetType(String))
                .Columns.Add("Meas", GetType(String))
                .Columns.Add("SizeCode", GetType(String))
                .Columns.Add("Quantity", GetType(Double))
                .Columns.Add("SizeCodeMap", GetType(String))
                .Columns.Add("Seq", GetType(Integer))
                .Columns.Add("DataType", GetType(Integer))
            End With
            row = 8 : Dim rowCode As Integer = 8 : Dim _SizeCode As String = "" : Dim _Quantity As Double = 0
            _Seq = 0
            Dim _DataType As Integer = 1
            Dim StateAdd As Boolean = False
            With book.Worksheets("Measurements")
                For i As Integer = 1 To 9999999
                    If .Cells(row, 0).Value.ToString().Trim = "CODE" Then
                        rownull = 0
                        rowCode = row
                        For x As Integer = 9 To 9999999
                            row += +1
                            StateAdd = True
                            _Meas = .Cells(row, 0).Value.ToString()
                            If IsNumeric(_Meas) Then
                                If _MeasHold = _Meas Then
                                    _Seq += +1
                                Else
                                    _Seq = 0
                                End If
                                For y As Integer = 11 To 9999999
                                    _SizeCode = .Cells(rowCode, y).Value.ToString()
                                    If _SizeCode <> "" Then
                                        _Quantity = Double.Parse("0" & .Cells(row, y).Value.ToString())
                                        _Quantity = Format(_Quantity, "0.0")
                                        If _Quantity > 0 Then
                                            Call validateSize(_StyleCode, _SeasonCode, _Meas, _SizeCode, _Quantity, _oDtSize, _Seq, _DataType)
                                            'Call SaveImportData_Size(_StyleCode, _SeasonCode, _Meas, _SizeCode, _Quantity)
                                            'Else
                                            '    Exit For
                                        End If
                                    Else
                                        Exit For
                                    End If
                                Next
                                _MeasHold = _Meas
                            Else
                                Exit For
                            End If
                        Next
                    Else
                        If .Cells(row, 0).Value.ToString() = "" Then
                            rownull += +1
                            If StateAdd = True Then
                                _DataType = _DataType + 1
                                StateAdd = False
                            End If

                        End If
                        If rownull > 4 Then
                            Exit For
                        End If
                    End If
                    row += +1
                Next
            End With
            If _oDtSize.Rows.Count > 0 Then
                Call SaveImportData_Size(_oDtSize, _Spls)
            End If


            'HI.Conn.SQLConn.Tran.Commit()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            Return True
        Catch ex As Exception
            _Spls.Close()
            Return False
        End Try
    End Function

    Private Sub validateSize(_StyleCode As String, _SeasonCode As String, _Meas As String, _SizeCode As String, _Quantity As String, _RefDt As DataTable, _Seq As Integer, _DataType As Integer)
        Try
            Dim _oDt As DataTable
            Dim _Cmd As String = "" : Dim _SizeCodeMap As String = ""
            Dim MasSizeCode As String = ""
            _Cmd = "Select TOP 1 FTMapSizeExtension  From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERMMapSize WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTMapSize='" & HI.UL.ULF.rpQuoted(_SizeCode) & "'"
            _SizeCodeMap = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "")
            If _SizeCodeMap = "" Then
                _Cmd = "Select TOP 1 A.FTMatSizeCode FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMMatSize] AS A WITH(NOLOCK) "
                _Cmd &= vbCrLf & "WHERE A.FTMatSizeCode = N'" & HI.UL.ULF.rpQuoted(_SizeCode) & "' "
                MasSizeCode = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MASTER, "")
                If MasSizeCode = "" Then
                    With _RefDt
                        .Rows.Add(_StyleCode, _SeasonCode, _Meas, _SizeCode, _Quantity, "", _Seq, _DataType)
                    End With
                Else
                    With _RefDt
                        .Rows.Add(_StyleCode, _SeasonCode, _Meas, _SizeCode, _Quantity, MasSizeCode, _Seq, _DataType)
                    End With
                End If
            Else
                With _RefDt
                    .Rows.Add(_StyleCode, _SeasonCode, _Meas, _SizeCode, _Quantity, _SizeCodeMap, _Seq, _DataType)
                End With
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function SaveImportData(StyleCode As String, SeaSonCode As String, Exp As String, LDate As String) As Boolean
        Try
            Dim _Cmd As String = ""
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec"
            _Cmd &= vbCrLf & " Set  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",FTDate='" & LDate & "'"
            _Cmd &= vbCrLf & ",FTEXP='" & Exp & "'"
            _Cmd &= vbCrLf & ",FTPostState='0' ,FTPostTime='' ,FDPostDate='' ,FTUserPost=''"
            _Cmd &= vbCrLf & "Where FTStyleCode='" & HI.UL.ULF.rpQuoted(StyleCode) & "'"
            _Cmd &= vbCrLf & "and FTSeasonCode='" & HI.UL.ULF.rpQuoted(SeaSonCode) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                _Cmd = "Insert Into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec"
                _Cmd &= "(FTInsUser, FDInsDate, FTInsTime,FTStyleCode, FTSeasonCode, FTDate, FTEXP)"
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(StyleCode) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(SeaSonCode) & "'"
                _Cmd &= vbCrLf & ",'" & LDate & "'"
                _Cmd &= vbCrLf & ",'" & Exp & "'"
                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
            End If

            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_Meas"
            _Cmd &= vbCrLf & "Where FTStyleCode='" & HI.UL.ULF.rpQuoted(StyleCode) & "'"
            _Cmd &= vbCrLf & "And FTSeasonCode='" & HI.UL.ULF.rpQuoted(SeaSonCode) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            _Cmd = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_Size"
            _Cmd &= vbCrLf & "Where FTStyleCode='" & HI.UL.ULF.rpQuoted(StyleCode) & "'"
            _Cmd &= vbCrLf & "And FTSeasonCode='" & HI.UL.ULF.rpQuoted(SeaSonCode) & "'"
            HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function SaveImportData_MEAS(_GarmentSpec As String, _PomDesc As String, _MedPattern As String, _TOLPlus As String, _GrandRule1 As String, _GrandRule2 As String, _StyleCode As String, _
                                         _SeasonCode As String, _MeasCode As String, _Seq As Integer) As Boolean
        Try
            Dim _Cmd As String = ""
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_MEAS"
            _Cmd &= vbCrLf & " Set  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",FTGarmentSpec='" & HI.UL.ULF.rpQuoted(_GarmentSpec) & "'"
            _Cmd &= vbCrLf & ",FTPomDesc='" & HI.UL.ULF.rpQuoted(_PomDesc) & "'"
            _Cmd &= vbCrLf & ",FTMedPattern='" & HI.UL.ULF.rpQuoted(_MedPattern) & "'"
            _Cmd &= vbCrLf & ",FTTOLPlus='" & HI.UL.ULF.rpQuoted(_TOLPlus) & "'"
            _Cmd &= vbCrLf & ",FTGrandRule1='" & HI.UL.ULF.rpQuoted(_GrandRule1) & "'"
            _Cmd &= vbCrLf & ",FTGrandRule2='" & HI.UL.ULF.rpQuoted(_GrandRule2) & "'"
            _Cmd &= vbCrLf & "Where FTStyleCode='" & HI.UL.ULF.rpQuoted(_StyleCode) & "'"
            _Cmd &= vbCrLf & "And FTSeasonCode='" & HI.UL.ULF.rpQuoted(_SeasonCode) & "'"
            _Cmd &= vbCrLf & "And FTMeasCode='" & HI.UL.ULF.rpQuoted(_MeasCode) & "'"
            _Cmd &= vbCrLf & "And FNMeasSeq=" & _Seq
            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                _Cmd = "Insert Into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_MEAS"
                _Cmd &= "( FTInsUser, FDInsDate, FTInsTime, FTStyleCode, FTSeasonCode, FTMeasCode, FTGarmentSpec, FTPomDesc, FTMedPattern, FTTOLPlus, FTGrandRule1, FTGrandRule2,FNMeasSeq)"
                _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_StyleCode) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_SeasonCode) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_MeasCode) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_GarmentSpec) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_PomDesc) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_MedPattern) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_TOLPlus) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_GrandRule1) & "'"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_GrandRule2) & "'"
                _Cmd &= vbCrLf & "," & _Seq
                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
            End If
             
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function SaveImportData_Size(_oDt As DataTable, _Spls As HI.TL.SplashScreen) As Boolean
        Try
            Dim _Cmd As String = "" : Dim _SizeCode As String = ""
            Dim _Dt As New DataTable
            Dim SeasonCode As String = ""

            If _oDt.Select("SizeCodeMap=''").Length > 0 Then
                _Dt.Columns.Add("FTSizeCodeNotExists", GetType(String))
                _Dt.Columns.Add("FTMapSizeExtend", GetType(String))
                For Each R As DataRow In _oDt.Select("SizeCodeMap=''")
                    If _Dt.Select("FTSizeCodeNotExists='" & HI.UL.ULF.rpQuoted(R!SizeCode.ToString) & "'").Length <= 0 Then
                        _Dt.Rows.Add(R!SizeCode.ToString, "")
                    End If
                Next

                _wMapSizeImportOrder = New HI.MER.wMapSizeImportOrder(_Dt)
                HI.TL.HandlerControl.AddHandlerObj(_wMapSizeImportOrder)
                Dim oSysLang As New HI.ST.SysLanguage
                Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _wMapSizeImportOrder.Name.ToString.Trim, _wMapSizeImportOrder)
                Call HI.ST.Lang.SP_SETxLanguage(_wMapSizeImportOrder)
                _Spls.Close()
                With _wMapSizeImportOrder
                    .ShowDialog()
                End With
                _Spls.UpdateInformation("Save Data SizeSpec... Please Wait... ")
            End If

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each r As DataRow In _oDt.Rows
                _SizeCode = IIf(r!SizeCodeMap.ToString <> "", r!SizeCodeMap.ToString, GetSize(r!SizeCode.ToString))
                SeasonCode = "" 'r!SeasonCode.ToString

                _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_Size"
                _Cmd &= vbCrLf & " Set  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Cmd &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",FNQuantity='" & r!Quantity.ToString & "'"
                _Cmd &= vbCrLf & "Where FTStyleCode='" & HI.UL.ULF.rpQuoted(r!StyleCode.ToString) & "'"
                _Cmd &= vbCrLf & "And FTSeasonCode='" & HI.UL.ULF.rpQuoted(SeasonCode) & "'"
                _Cmd &= vbCrLf & "And FTMeasCode='" & HI.UL.ULF.rpQuoted(r!Meas.ToString) & "'"
                _Cmd &= vbCrLf & "And FTSizeCode='" & HI.UL.ULF.rpQuoted(_SizeCode) & "'"
                _Cmd &= vbCrLf & "And FNMeasSeq=" & Integer.Parse("0" & r!Seq.ToString)

                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Cmd = "Insert Into [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPRImportSizeSpec_Size"
                    _Cmd &= "(FTInsUser, FDInsDate, FTInsTime, FTStyleCode, FTSeasonCode, FTMeasCode, FTSizeCode, FNQuantity ,FNMeasSeq)"
                    _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(r!StyleCode.ToString) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(SeasonCode) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(r!Meas.ToString) & "'"
                    _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_SizeCode) & "'"
                    _Cmd &= vbCrLf & ",'" & r!Quantity.ToString & "'"
                    _Cmd &= vbCrLf & "," & Integer.Parse("0" & r!Seq.ToString)

                    If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If
            Next
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function GetSize(_SizeCode As String) As String
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select TOP 1 FTMapSizeExtension  From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERMMapSize WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE FTMapSize='" & HI.UL.ULF.rpQuoted(_SizeCode) & "'"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, _SizeCode)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub FTFilePath_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTFilePath.ButtonClick
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                '.InitialDirectory = "D:\"
                '.InitialDirectory = ""
                .Filter = "Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Me.FTFilePath.Text = .FileName
                Else
                    Me.FTFilePath.Text = ""
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            Me.FTFilePath.Text = ""
            Me.GroupControl1.Controls.Remove(SpreadsheetControl1)
            SpreadsheetControl1 = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
            With Me.SpreadsheetControl1
                .Dock = System.Windows.Forms.DockStyle.Fill
                .Location = New System.Drawing.Point(2, 21)
                .Name = "SpreadsheetControl1"
                .ReadOnly = True
                .Size = New System.Drawing.Size(1036, 534)
                .TabIndex = 394
                .Text = "SpreadsheetControl1"
            End With

            Me.GroupControl1.Controls.Add(Me.SpreadsheetControl1)
        Catch ex As Exception
        End Try
    End Sub
End Class