Imports System.IO
Imports DevExpress.Pdf
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports System.Text
Imports System.Windows.Forms
Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing
Imports Bytescout.PDFExtractor
Imports SautinSoft.PdfFocus

Public Class wReadPDFBOMSheet

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


    End Sub

    Private Function ExtractTextFromPDF(ByVal filePath As String) As String
        Dim documentText As String = ""
        Try

            Using documentStream As New FileStream(filePath, FileMode.Open, FileAccess.Read)
                Using documentProcessor As New PdfDocumentProcessor()
                    documentProcessor.LoadDocument(documentStream)
                    documentText = documentProcessor.Text
                End Using
            End Using

        Catch
        End Try
        Return documentText
    End Function

    Private Function CreateDatattable() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("FTSeason", GetType(String))
        dt.Columns.Add("FTStyleNo", GetType(String))
        dt.Columns.Add("FTStyleName", GetType(String))
        dt.Columns.Add("FTSupplier", GetType(String))
        dt.Columns.Add("FTRawMatCode", GetType(String))
        dt.Columns.Add("FTRawMatDesc", GetType(String))
        dt.Columns.Add("FTPosittion", GetType(String))
        dt.Columns.Add("FNUsedQuantity", GetType(Double))
        dt.Columns.Add("FTUnit", GetType(String))
     
        Return dt
    End Function

    
    Private Sub LoadDataShowGrid()

        Try
            Dim PageNo As Integer = 1
            Dim RowNo As Integer = 1
            Dim dtMain As DataTable = Me.CreateDatattable
            Dim _dt As DataTable
            Dim _Qry As String = ""

            _Qry = " SELECT  FNRowSeq, FTSeason, FTStyleNo, FTStyleName"
            _Qry &= vbCrLf & " , FTMSC, FTMSC1, FTMSC2, FTMSC3, FTSupplier, FTRawMatCode"
            _Qry &= vbCrLf & " , FTRawMatDesc, FTPosittion, FNUsedQuantity, FTUnit, FTColorWay, "
            _Qry &= vbCrLf & "      FTRawMatColor, FTRawMatColorDesc"
            _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportStylePDF AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & " ORDER BY FTSeason,FTStyleNo,FNRowSeq,FTRawMatCode,FTPosittion,FTColorWay "

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Dim _StrFilter As String = ""
            Dim ColIndx As Integer = 0
            For Each R As DataRow In _dt.Rows
                _StrFilter = "FTSeason='" & HI.UL.ULF.rpQuoted(R!FTSeason.ToString) & "' AND FTStyleNo='" & HI.UL.ULF.rpQuoted(R!FTStyleNo.ToString) & "'   AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(R!FTRawMatCode.ToString) & "'  AND FTPosittion='" & HI.UL.ULF.rpQuoted(R!FTPosittion.ToString) & "'"

                If dtMain.Select(_StrFilter).Length <= 0 Then
                    dtMain.Rows.Add(R!FTSeason.ToString, R!FTStyleNo.ToString, R!FTStyleName.ToString, R!FTSupplier.ToString, R!FTRawMatCode.ToString, R!FTRawMatDesc.ToString, R!FTPosittion.ToString, Val(R!FNUsedQuantity.ToString), R!FTUnit.ToString)
                End If

                If dtMain.Columns.IndexOf("C" & R!FTColorWay.ToString) < 0 Then
                    dtMain.BeginInit()
                    dtMain.Columns.Add("C" & R!FTColorWay.ToString, GetType(String))
                    dtMain.EndInit()

                    With Me.ogvdetail
                        Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                        With ColG
                            .Visible = True
                            .FieldName = "C" & R!FTColorWay.ToString
                            .Name = "C" & R!FTColorWay.ToString
                            .Caption = R!FTColorWay.ToString
                            .ColumnEdit = ReposFTRawMatDesc
                        End With

                        .Columns.Add(ColG)

                        With .Columns("C" & R!FTColorWay.ToString)

                            .OptionsFilter.AllowAutoFilter = False
                            .OptionsFilter.AllowFilter = False
                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.None
                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            .AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                            With .OptionsColumn
                                .AllowMove = False
                                .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                .AllowEdit = False
                                .AllowMove = False
                            End With

                        End With
                    End With

                End If


                For Each Rx As DataRow In dtMain.Select(_StrFilter)
                    Rx.Item("C" & R!FTColorWay.ToString) = R!FTRawMatColor.ToString
                    Exit For
                Next

            Next

            Me.ogcdetail.DataSource = dtMain.Copy
        Catch ex As Exception

        End Try
    End Sub


    Private Sub ClearGrid()
        With Me.ogvdetail
            For I As Integer = .Columns.Count - 1 To 0 Step -1
                Select Case .Columns(I).FieldName.ToString.ToUpper

                    Case "FTUnit".ToUpper, "FNUsedQuantity".ToUpper, "FTPosittion".ToUpper, "FTRawMatDesc".ToUpper, "FTRawMatCode".ToUpper, "FTSupplier".ToUpper, "FTStyleName".ToUpper, "FTStyleNo".ToUpper, "FTSeason".ToUpper

                        .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                    Case Else
                        .Columns.Remove(.Columns(I))
                End Select
            Next
        End With
        Me.ogcdetail.DataSource = Nothing
    End Sub


    Private Sub Loaddata(FilePath As String)
        Dim _Spl As New HI.TL.SplashScreen("Loading.... ,Please wait.")
        Call ClearGrid()
        Try
          
            Dim _RowImport As Integer = 0
            Dim _StartRowImport As Integer = 10
            Dim _Qry As String = ""
            Dim Str As String = ""

            Dim _FTStyleNo As String = ""
            Dim _FTSeason As String = ""
            Dim _FTStyleName As String = ""
            Dim _FTMSC As String = ""
            Dim _FTMSC1 As String = ""
            Dim _FTMSC2 As String = ""
            Dim _FTMSC3 As String = ""

            _Qry = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportStylePDF WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Str = ExtractTextFromPDF(FilePath)
            Try
                PdfViewer1.CloseDocument()
              
            Catch ex As Exception

            End Try

            Try
                PdfViewer1.LoadDocument(FilePath)
                Dim _PathSaveFileName As String = Path.GetDirectoryName(FilePath)
                
            Catch ex As Exception
            End Try

            Dim ListDataFromPDF As New List(Of DataTable)
            ListDataFromPDF = HI.UL.ReadPDFToDataTable.BytescoutReadPdfFile(FilePath, HI.ST.UserInfo.UserName)

            Dim _dt As New DataTable
            With _dt
                .Columns.Add("F1", GetType(String))
                .Columns.Add("F2", GetType(String))
                .Columns.Add("F3", GetType(String))
                .Columns.Add("F4", GetType(String))
                .Columns.Add("F5", GetType(String))
                .Columns.Add("F6", GetType(String))
                .Columns.Add("F7", GetType(String))
                .Columns.Add("F8", GetType(String))
                .Columns.Add("F9", GetType(String))
                .Columns.Add("F10", GetType(String))
                .Columns.Add("F11", GetType(String))
                .Columns.Add("F12", GetType(String))
            End With

            For Each _dttemp As DataTable In ListDataFromPDF
                _StartRowImport = 10

                If _dttemp.Columns.Count < 12 Then
                    Dim ColCount As Integer = _dttemp.Columns.Count + 1
                    For xi As Integer = ColCount To 12
                        _dttemp.Columns.Add("F" & xi.ToString, GetType(String))
                    Next
                End If

                Dim _FoundDataItem As Boolean = False
                Dim _RowIndx As Integer = 0
                Dim _StateMove As Integer = 0
                Dim _StateNotMove As Integer = 0

                Try
                    If _dttemp.Rows(21)!F3.ToString.Trim.Length = 1 And _dttemp.Rows(21)!F3.ToString.Trim = "" Then
                        _StateNotMove = 1
                    End If
                Catch ex As Exception

                End Try
                _dttemp.BeginInit()
                For Each R As DataRow In _dttemp.Rows
                    _RowIndx = _RowIndx + 1
                    Select Case _RowIndx
                        Case 3, 5, 7, 9

                            If (R!F7.ToString = "" Or R!F7.ToString = ":") And R!F8.ToString <> "" Then
                                R!F7 = R!F8.ToString
                            ElseIf R!F7.ToString = "" And R!F6.ToString <> "" Then
                                R!F7 = R!F6.ToString
                            End If

                            If (R!F11.ToString = "" Or R!F11.ToString = ":") And R!F12.ToString <> "" Then
                                R!F11 = R!F12.ToString
                                R!F12 = ""
                                'ElseIf (R!F11.ToString = "") And R!F12.ToString = "" And R!F10.ToString <> "" Then
                                '    Try
                                '        R!F11 = R!F10.ToString.Split(":")(1).Trim
                                '    Catch ex As Exception
                                '        Try
                                '            R!F11 = R!F10.ToString.Split(":")(0).Trim
                                '        Catch ex2 As Exception
                                '        End Try

                                '    End Try

                                '    R!F12 = ""
                            End If

                            If (R!F11.ToString = "" Or R!F11.ToString = ":") And R!F10.ToString <> "" Then
                                ' R!F11 = R!F10.ToString
                                Try
                                    R!F11 = R!F10.ToString.Split(":")(1).Trim
                                Catch ex As Exception
                                    Try
                                        R!F11 = R!F10.ToString.Split(":")(0).Trim
                                    Catch ex2 As Exception
                                    End Try

                                End Try

                                R!F10 = ""
                            End If
                        Case 4, 6, 8, 10
                            If R!F7.ToString = "" Or R!F7.ToString = ":" Then
                                If _StateNotMove = 0 Then
                                    If R!F6.ToString <> ":" Then
                                        R!F7 = R!F6.ToString
                                    End If
                                Else
                                    R!F7 = R!F8.ToString
                                End If

                            End If
                        Case 1, 2, 11
                        Case 12, 13, 14, 15, 16, 17, 18, 19, 20
                            If _StateNotMove = 0 Then
                                R!F12 = R!F11.ToString
                                R!F11 = R!F10.ToString
                                R!F10 = R!F9.ToString
                                R!F9 = R!F8.ToString
                                R!F8 = R!F7.ToString
                                R!F7 = R!F6.ToString
                                R!F6 = ""
                            End If

                        Case Else

                            If _StateNotMove = 0 Then
                                If R!F1.ToString <> "" Then
                                    _StateMove = 0
                                    If IsNumeric(R!F1.ToString) Then

                                        If R!F7.ToString = "" Then
                                            R!F7 = R!F6.ToString
                                            R!F6 = R!F5.ToString
                                            R!F5 = ""
                                            _StateMove = 1
                                        Else
                                            If R!F7.ToString <> "" Then
                                                _StateMove = 2
                                                R!F12 = R!F11.ToString
                                                R!F11 = R!F10.ToString
                                                R!F10 = R!F9.ToString
                                                R!F9 = R!F8.ToString
                                                R!F8 = R!F7.ToString
                                                R!F7 = R!F6.ToString
                                                R!F6 = R!F5.ToString
                                                R!F5 = ""
                                            End If
                                        End If
                                    End If
                                Else
                                    Select Case _StateMove
                                        Case 1
                                            R!F7 = R!F6.ToString
                                            R!F6 = R!F5.ToString
                                            R!F5 = ""
                                        Case 2
                                            R!F12 = R!F11.ToString
                                            R!F11 = R!F10.ToString
                                            R!F10 = R!F9.ToString
                                            R!F9 = R!F8.ToString
                                            R!F8 = R!F7.ToString
                                            R!F7 = R!F6.ToString
                                            R!F6 = R!F5.ToString
                                            R!F5 = ""
                                    End Select
                                End If
                            Else
                                R!F3 = R!F4.ToString
                                R!F4 = ""
                            End If

                    End Select
                Next
                _dttemp.EndInit()
                If _dttemp.Columns.Count = 12 Then
                    '_dt.Merge(_dttemp.Copy)

                    Dim RIdx As Integer = 1
                   
                    _dttemp.Columns.Add("FTStateDelete", GetType(String))

                    For Each R As DataRow In _dttemp.Rows

                        Select Case RIdx
                            Case 3
                                If _FTStyleName = "" Then
                                    _FTStyleName = R!F7.ToString
                                End If

                                If _FTSeason = "" Then
                                    _FTSeason = R!F11.ToString
                                End If

                            Case 4
                                If _FTMSC = "" Then
                                    _FTMSC = R!F7.ToString
                                End If

                            Case 5
                                If _FTStyleNo = "" Then
                                    _FTStyleNo = R!F11.ToString
                                End If

                            Case 6
                                If _FTMSC1 = "" Then
                                    _FTMSC1 = R!F7.ToString
                                End If

                            Case 8
                                If _FTMSC1 = "" Then
                                    _FTMSC2 = R!F7.ToString
                                End If

                            Case 10
                                If _FTMSC1 = "" Then
                                    _FTMSC3 = R!F7.ToString
                                End If

                        End Select

                        Select Case RIdx
                            Case Is <= 11
                                R!FTStateDelete = "1"
                            Case Else

                        End Select

                        RIdx = RIdx + 1
                    Next

                    _dttemp.BeginInit()



                    For Each R As DataRow In _dttemp.Select("FTStateDelete='1'")
                        R.Delete()
                    Next

                    _dttemp.EndInit()
                    _dttemp.Columns.Add("RowIndx", GetType(Integer))
                    _dttemp.Columns.Add("RowIndxTo", GetType(Integer))
                    RIdx = 1

                    For Each R As DataRow In _dttemp.Rows
                        R!RowIndx = RIdx
                        RIdx = RIdx + 1
                    Next

                    If _dttemp.Select("RowIndx =" & _StartRowImport & " AND F1 ='#' ").Length <= 0 Then
                        If _dttemp.Select("RowIndx =" & _StartRowImport + 1 & " AND F1 ='#' ").Length > 0 Then
                            _StartRowImport = _StartRowImport + 1
                        End If
                    End If

                    For Each R As DataRow In _dttemp.Select("RowIndx >" & _StartRowImport & " AND F1 <>''")
                        For Each Rx As DataRow In _dttemp.Select("(RowIndx >" & Val(R!RowIndx) & " AND F1 <>'') OR (RowIndx >" & Val(R!RowIndx) & " AND F1='' AND F3='N')", "RowIndx")
                            R!RowIndxTo = Val(Rx!RowIndx) - 1
                            Exit For
                        Next
                    Next

                    Dim _dttempstyle As DataTable = _dttemp.Copy

                    Dim ColorWay1 As String = ""
                    Dim ColorWay2 As String = ""
                    Dim ColorWay3 As String = ""
                    Dim ColorWay4 As String = ""

                    For Each R As DataRow In _dttemp.Select("RowIndx =" & _StartRowImport & " ")
                        ColorWay1 = R!F9.ToString.Replace("@", "").Replace("*", "").Replace("#", "").Replace("$", "")
                        ColorWay2 = R!F10.ToString.Replace("@", "").Replace("*", "").Replace("#", "").Replace("$", "")
                        ColorWay3 = R!F11.ToString.Replace("@", "").Replace("*", "").Replace("#", "").Replace("$", "")
                        ColorWay4 = R!F12.ToString.Replace("@", "").Replace("*", "").Replace("#", "").Replace("$", "")
                        Exit For
                    Next

                    RIdx = 1
                    For Each R As DataRow In _dttemp.Select("RowIndx >" & _StartRowImport & " AND F1 <>''")
                        Dim SuplNo As String = ""
                        Dim ItemNo As String = ""
                        Dim ItemDesc As String = ""
                        Dim PositPart As String = ""
                        Dim Unit As String = ""
                        Dim UsedQty As Double = 0
                        Dim Color1 As String = ""
                        Dim ColorDesc1 As String = ""
                        Dim Color2 As String = ""
                        Dim ColorDesc2 As String = ""
                        Dim Color3 As String = ""
                        Dim ColorDesc3 As String = ""
                        Dim Color4 As String = ""
                        Dim ColorDesc4 As String = ""
                        Dim _FondItem As Boolean = False
                        RIdx = 1
                        For Each Rx As DataRow In _dttempstyle.Select("RowIndx >=" & Val(R!RowIndx.ToString) & " AND RowIndx <=" & Val(R!RowIndxTo.ToString) & "", "RowIndx")
                            _FondItem = False
                            If ItemNo = "" And Microsoft.VisualBasic.Left(Rx!F3.ToString, 1) <> "*" Then
                                SuplNo = SuplNo & Rx!F3.ToString
                            End If

                            If ItemNo = "" And Microsoft.VisualBasic.Left(Rx!F3.ToString, 1) = "*" Then
                                ItemNo = Replace(Replace(Replace(Replace(Replace(Rx!F3.ToString, "*", ""), " ", ""), "N", ""), "A", ""), "F", "")
                                _FondItem = True
                            End If

                            If ItemNo <> "" And _FondItem = False Then
                                If Rx!F3.ToString & Rx!F4.ToString & Rx!F5.ToString <> "" Then
                                    ItemDesc = ItemDesc & (Rx!F3.ToString & Rx!F4.ToString & Rx!F5.ToString)
                                End If
                            End If
                            If Rx!F6.ToString <> "" Then
                                PositPart = PositPart & Rx!F6.ToString
                            End If
                            If Rx!F7.ToString <> "" Then
                                If IsNumeric(Rx!F7.ToString) Then
                                    UsedQty = Double.Parse(Rx!F7.ToString)
                                End If
                            End If

                            If Rx!F8.ToString <> "" Then
                                Unit = Unit & Rx!F8.ToString
                            End If
                            If Rx!F9.ToString <> "" And ColorWay1 <> "" Then
                                'Color1 = Color1 & IIf(Color1 <> "", vbCrLf, "") & Rx!F9.ToString
                                Color1 = Color1 & " " & Rx!F9.ToString
                            End If

                            If Rx!F10.ToString <> "" And ColorWay2 <> "" Then
                                'Color2 = Color2 & IIf(Color2 <> "", vbCrLf, "") & Rx!F10.ToString
                                Color2 = Color2 & " " & Rx!F10.ToString
                            End If

                            If Rx!F11.ToString <> "" And ColorWay3 <> "" Then
                                'Color3 = Color3 & IIf(Color3 <> "", vbCrLf, "") & Rx!F11.ToString
                                Color3 = Color3 & " " & Rx!F11.ToString
                            End If

                            If Rx!F12.ToString <> "" And ColorWay4 <> "" Then
                                'Color4 = Color4 & IIf(Color4 <> "", vbCrLf, "") & Rx!F12.ToString
                                Color4 = Color4 & " " & Rx!F12.ToString
                            End If

                            RIdx = RIdx + 1
                        Next

                        If ItemNo <> "" Then

                            Dim StateSkip As Boolean = False
                            For I As Integer = 1 To 4
                                StateSkip = False
                                Select Case I
                                    Case 1
                                        If ColorWay1 = "" Then
                                            StateSkip = True
                                        End If
                                    Case 2
                                        If ColorWay2 = "" Then
                                            StateSkip = True
                                        End If
                                    Case 3
                                        If ColorWay3 = "" Then
                                            StateSkip = True
                                        End If
                                    Case 4
                                        If ColorWay4 = "" Then
                                            StateSkip = True
                                        End If
                                End Select

                                If StateSkip = False Then
                                    _RowImport = _RowImport + 1

                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportStylePDF ("
                                    _Qry &= vbCrLf & "   FTUserLogin, FNRowSeq, FTSeason, FTStyleNo, FTStyleName"
                                    _Qry &= vbCrLf & "   , FTMSC, FTMSC1, FTMSC2, FTMSC3, FTSupplier, FTRawMatCode"
                                    _Qry &= vbCrLf & "   , FTRawMatDesc, FTPosittion, FNUsedQuantity, FTUnit, FTColorWay"
                                    _Qry &= vbCrLf & "  , FTRawMatColor, FTRawMatColorDesc )"
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    _Qry &= vbCrLf & "," & _RowImport & ""
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTSeason) & "'"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTStyleNo) & "'"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTStyleName) & "'"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTMSC) & "'"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTMSC1) & "'"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTMSC2) & "'"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTMSC3) & "'"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(SuplNo) & "'"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(ItemNo) & "'"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(ItemDesc) & "'"
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(PositPart) & "'"
                                    _Qry &= vbCrLf & "," & UsedQty & ""
                                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Unit) & "'"

                                    Select Case I
                                        Case 1

                                            ColorDesc1 = Color1

                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(ColorWay1) & "'"

                                            'If ColorDesc1 <> "" Then
                                            '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted((Color1.Split(" ")(0)).Trim()) & "'"
                                            '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted((ColorDesc1.Replace((Color1.Split(" ")(0)).Trim(), "")).Trim) & "'"
                                            'Else
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Color1) & "'"
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(ColorDesc1) & "'"
                                            ' End If


                                        Case 2
                                            ColorDesc2 = Color2

                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(ColorWay2) & "'"

                                            'If ColorDesc2 <> "" Then

                                            '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted((Color2.Split(" ")(0)).Trim()) & "'"
                                            '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted((ColorDesc2.Replace((Color2.Split(" ")(0)).Trim(), "")).Trim) & "'"

                                            'Else

                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Color2) & "'"
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(ColorDesc2) & "'"

                                            ' End If

                                        Case 3
                                            ColorDesc3 = Color3

                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(ColorWay3) & "'"

                                            'If ColorDesc3 <> "" Then

                                            '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted((Color3.Split(" ")(0)).Trim()) & "'"
                                            '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted((ColorDesc3.Replace((Color3.Split(" ")(0)).Trim(), "")).Trim) & "'"

                                            'Else

                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Color3) & "'"
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(ColorDesc3) & "'"

                                            ' End If

                                        Case 4
                                            ColorDesc4 = Color4

                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(ColorWay4) & "'"

                                            'If ColorDesc4 <> "" Then

                                            '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted((Color4.Split(" ")(0)).Trim()) & "'"
                                            '    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted((ColorDesc4.Replace((Color4.Split(" ")(0)).Trim(), "")).Trim) & "'"

                                            'Else

                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Color4) & "'"
                                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(ColorDesc4) & "'"

                                            ' End If

                                    End Select
                                End If

                                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                            Next

                        End If

                    Next


                End If

            Next

            Call LoadDataShowGrid()
            _Spl.Close()

        Catch ex As Exception
            _Spl.Close()
        End Try


    End Sub


    Private Sub FTFilePath_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTFilePath.ButtonClick
        Select Case e.Button.Index
            Case 0
                Try
                    Dim opFileDialog As New System.Windows.Forms.OpenFileDialog
                    opFileDialog.Filter = "PDF Files(*.PDF)|*.PDF"
                    opFileDialog.ShowDialog()

                    Try

                        If opFileDialog.FileName <> "" Then
                            Me.FTFilePath.Text = opFileDialog.FileName
                            Call Loaddata(opFileDialog.FileName)
                        End If
                    Catch ex As Exception
                    End Try

                Catch ex As Exception
                    Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString())
                End Try

            Case Else
                '...do nothing
        End Select
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Call ClearGrid()
        Try
            PdfViewer1.CloseDocument()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ocmloaddata_Click(sender As Object, e As EventArgs) Handles ocmloaddata.Click
        If Me.FTFilePath.Text.Trim <> "" Then
            Loaddata(Me.FTFilePath.Text)
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, ogbselectfile.Text)
        End If
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        If Me.FTFilePath.Text.Trim <> "" Then

            Loaddata(Me.FTFilePath.Text)
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, ogbselectfile.Text)
        End If
    End Sub

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvdetail)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvdetail)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTFilePath_EditValueChanged(sender As Object, e As EventArgs) Handles FTFilePath.EditValueChanged
        Call ClearGrid()
    End Sub

    Private Sub ocmimportbimpdf_Click(sender As Object, e As EventArgs) Handles ocmimportbimpdf.Click

        If Not (Me.ogcdetail.DataSource Is Nothing) Then
            Dim _dt As DataTable
            With CType(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy
            End With

            If _dt.Rows.Count > 0 Then

                Dim _StateImportData As Boolean = True
                Dim _StyleCode As String = ""
                Dim _SeasonCode As String = ""
                Dim _StyleName As String = ""

                For Each R As DataRow In _dt.Rows
                    _StyleCode = R!FTStyleNo.ToString
                    _StyleName = R!FTStyleName.ToString
                    _SeasonCode = R!FTSeason.ToString
                    Exit For
                Next

                Dim _Qry As String
                Dim _SysStyleDevId As Integer
                _Qry = "SELECT TOP 1   FNHSysStyleDevId"
                _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle AS A With(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE  (FTStyleDevCode = N'" & HI.UL.ULF.rpQuoted(_StyleCode) & "') "
                _Qry &= vbCrLf & "  AND (FTSeason = N'" & HI.UL.ULF.rpQuoted(_SeasonCode) & "')"
                _SysStyleDevId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "0")))

                If _SysStyleDevId > 0 Then
                    _StateImportData = HI.MG.ShowMsg.mConfirmProcess("พบ Style นี้ ในระบบแล้ว คุณต้องการทำการ Import อีกครั้งใช่หรือไม่ !!!", 1587889897)
                End If

                If _StateImportData Then
                    If ImportData() Then

                        HI.MG.ShowMsg.mInfo("Import Data Complete !!!", 1506889897, Me.Text, , MessageBoxIcon.Warning)

                        _Qry = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempImportStylePDF WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                        Call LoadDataShowGrid()

                    End If
                End If


            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการ ทำการ Import !!!", 1506889766, Me.Text, , MessageBoxIcon.Warning)
            End If

        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก File ที่ต้องการทำการ Import !!!", 1506889765, Me.Text, , MessageBoxIcon.Warning)
        End If
    End Sub

    Private Function ImportData() As Boolean
        Dim _StateImport As Boolean = True
        Dim _SysStyleDevId As Integer = 0
        Dim _Qry As String = ""
        Dim _Spls As New HI.TL.SplashScreen("Importing.... Please wait.")

        Try

            Dim _dtcheckmat As DataTable
            Dim _dt As DataTable

            With CType(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy
            End With

            Dim _StyleCode As String = ""
            Dim _SeasonCode As String = ""
            Dim _StyleName As String = ""
            Dim _MatCode As String = ""
            Dim _Unit As String = ""
            Dim _Suplier As String = ""
            Dim _UnitM As String = ""
            Dim _SuplierM As String = ""

            For Each R As DataRow In _dt.Rows
                _StyleCode = R!FTStyleNo.ToString
                _StyleName = R!FTStyleName.ToString
                _SeasonCode = R!FTSeason.ToString
                Exit For
            Next

            _Qry = "SELECT TOP 1   FNHSysStyleDevId"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle AS A With(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE  (FTStyleDevCode = N'" & HI.UL.ULF.rpQuoted(_StyleCode) & "') "
            _Qry &= vbCrLf & "  AND (FTSeason = N'" & HI.UL.ULF.rpQuoted(_SeasonCode) & "')"
            _SysStyleDevId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, "0")))

            If _SysStyleDevId <= 0 Then
                _SysStyleDevId = HI.TL.RunID.GetRunNoID("TMERTDevelopStyle", "FNHSysStyleDevId", Conn.DB.DataBaseName.DB_MERCHAN)

                _Qry = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle "
                _Qry &= vbCrLf & " ("
                _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FNHSysStyleDevId, FTStyleDevCode, FTStyleDevNameTH, FTStyleDevNameEN, FTSeason "
                _Qry &= vbCrLf & " )"
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & "," & _SysStyleDevId & ""
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_StyleCode) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_StyleName) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_StyleName) & "'"
                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_SeasonCode) & "'"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            End If

            If _SysStyleDevId > 0 Then

                _Qry = " DELETE FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_Mat  WHERE  FNHSysStyleDevId=" & _SysStyleDevId & " "
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                _Qry = " DELETE FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_ColorWay  WHERE  FNHSysStyleDevId=" & _SysStyleDevId & " "
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                _Qry = " DELETE FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_SizeBreakDown  WHERE  FNHSysStyleDevId=" & _SysStyleDevId & " "
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                Dim _matseq As Integer = 0

                For Each R As DataRow In _dt.Rows
                    _matseq = _matseq + 1

                    _MatCode = R!FTRawMatCode.ToString()
                    _Unit = R!FTUnit.ToString.Replace("EACH", "PCS").Replace("LINEAR YARD", "YDS").Trim
                    _Suplier = R!FTSupplier.ToString.Replace("NIKE-APPROVED VENDOR", "").Trim


                    _Qry = "SELECT  TOP 1 A.FTSuplCode "
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS A WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE A.FTSuplCode='" & HI.UL.ULF.rpQuoted(_Suplier) & "'"

                    _SuplierM = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")

                    _Qry = "SELECT  TOP 1 A.FTUnitCode "
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS A WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE A.FTUnitCode='" & HI.UL.ULF.rpQuoted(_Unit) & "'"

                    _UnitM = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")

                    _Qry = "SELECT  TOP 1 A.FTMainMatCode, A.FTCusItemCodeRef, B.FTSuplCode, C.FTUnitCode"
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS A WITH(NOLOCK) LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS C WITH(NOLOCK) ON A.FNHSysUnitId = C.FNHSysUnitId LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "       [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS B WITH(NOLOCK) ON A.FNHSysSuplId = B.FNHSysSuplId"
                    _Qry &= vbCrLf & " WHERE A.FTCusItemCodeRef='" & HI.UL.ULF.rpQuoted(_MatCode) & "'"

                    _dtcheckmat = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                    If _dtcheckmat.Rows.Count > 0 Then
                        For Each Rxm As DataRow In _dtcheckmat.Rows

                            If _UnitM = "" Then
                                If Rxm!FTUnitCode.ToString <> _Unit And _Unit <> "" Then
                                    _Unit = ""
                                ElseIf _Unit = "" Then
                                    _Unit = Rxm!FTUnitCode.ToString
                                End If
                            End If

                            If _SuplierM = "" Then
                                _Suplier = Rxm!FTSuplCode.ToString
                                'If Rxm!FTSuplCode.ToString <> _Suplier And _Suplier <> "" Then
                                '    _Suplier = Rxm!FTSuplCode.ToString
                                'ElseIf _Suplier = "" Then
                                '    _Suplier = Rxm!FTSuplCode.ToString
                                'End If
                            End If


                            Exit For
                        Next

                    Else

                        _Unit = _UnitM
                        _Suplier = _SuplierM

                    End If

                    _Qry = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_Mat "
                    _Qry &= vbCrLf & " ("
                    _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime,  FNHSysStyleDevId, FNSeq"
                    _Qry &= vbCrLf & " , FNMerMatSeq, FTItemNo, FTItemDesc, FTPartNameEN, FTPartNameTH, FTSuplCode, FTStateNominate "
                    _Qry &= vbCrLf & "  ,FTUnitCode, FNPrice, FNHSysCurId, FNConSmp, FTComponent, FTStateActive"
                    _Qry &= vbCrLf & ", FTStateCombination, FTStateMainMaterial, FTStateFree,FNPart"

                    _Qry &= vbCrLf & " )"
                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & "," & _SysStyleDevId & ""
                    _Qry &= vbCrLf & "," & _matseq & ""
                    _Qry &= vbCrLf & "," & _matseq & ""
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_MatCode) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRawMatDesc.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPosittion.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTPosittion.ToString) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Suplier) & "'"

                    If R!FTSupplier.ToString.Contains("NIKE-APPROVED VENDOR") = True Then
                        _Qry &= vbCrLf & ",'1'"
                    Else
                        _Qry &= vbCrLf & ",'0'"
                    End If

                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Unit) & "'"
                    _Qry &= vbCrLf & ",0"
                    _Qry &= vbCrLf & ",0"
                    _Qry &= vbCrLf & ", " & Val(R!FNUsedQuantity.ToString) & ""
                    _Qry &= vbCrLf & ",'','1','0','0','0',1"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                    Dim FNColorWaySeq As Integer = 1

                    Dim _Color As String = ""
                    Dim _ColorCode As String = ""
                    Dim _ColorCode1 As String = ""
                    Dim _ColorDesc As String = ""
                    Dim _RunCount As Integer = 0

                    For Each Col As DataColumn In _dt.Columns
                        Select Case Col.ColumnName.ToString.ToUpper
                            Case "FTUnit".ToUpper, "FNUsedQuantity".ToUpper, "FTPosittion".ToUpper, "FTRawMatDesc".ToUpper, "FTRawMatCode".ToUpper, "FTSupplier".ToUpper, "FTStyleName".ToUpper, "FTStyleNo".ToUpper, "FTSeason".ToUpper
                            Case Else

                                _ColorDesc = ""
                                _ColorCode = ""
                                _RunCount = 0
                                _ColorCode1 = ""

                                If R.Item(Col.ColumnName.ToString).ToString.Trim() <> "" Then
                                    _Color = R.Item(Col.ColumnName.ToString).ToString.Trim()
                                    _RunCount = 0

                                    If Microsoft.VisualBasic.Left(_Color.ToUpper, 3) = "GCW" Then

                                        _ColorCode = _Color.Split(" ")(0)

                                        _ColorDesc = _Color.Replace(_ColorCode, "").Trim

                                        If _ColorDesc = "" Then
                                            _ColorDesc = _ColorCode
                                        End If

                                        _ColorDesc = ""

                                    Else

                                        For Each Str As String In _Color.Split(" ")
                                            _RunCount = _RunCount + 1

                                            If _RunCount Mod 2 = 1 Then

                                                _ColorCode1 = Str.Trim()

                                                If _ColorCode = "" Then
                                                    _ColorCode = Str.Trim()
                                                Else
                                                    _ColorCode = _ColorCode & "/" & Str.Trim()
                                                End If

                                            Else
                                                If _ColorDesc = "" Then
                                                    _ColorDesc = _ColorCode1 & " " & Str.Trim()
                                                Else
                                                    _ColorDesc = _ColorDesc & "/" & _ColorCode1 & " " & Str.Trim()
                                                End If

                                            End If

                                        Next

                                        If _ColorDesc = "" Then
                                            _ColorDesc = _ColorCode
                                        End If

                                    End If

                                End If

                                _Qry = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_ColorWay "
                                _Qry &= vbCrLf & " ("
                                _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FNHSysStyleDevId, FNSeq, FNMerMatSeq, FTColorWay, FTColorCode, FTColorNameTH, FTColorNameEN,FNColorWaySeq,FTRunColor"
                                _Qry &= vbCrLf & " )"
                                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                _Qry &= vbCrLf & "," & _SysStyleDevId & ""
                                _Qry &= vbCrLf & "," & _matseq & ""
                                _Qry &= vbCrLf & "," & _matseq & ""
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Microsoft.VisualBasic.Right(Col.ColumnName.ToString, Col.ColumnName.ToString.Length - 1)) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_ColorCode) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_ColorDesc) & "'"
                                _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_ColorDesc) & "'"
                                _Qry &= vbCrLf & "," & FNColorWaySeq & ""
                                _Qry &= vbCrLf & ",'1'"

                                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
                                FNColorWaySeq = FNColorWaySeq + 1

                        End Select

                    Next

                Next

                _Qry = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_SizeBreakDown "
                _Qry &= vbCrLf & " ("
                _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FNHSysStyleDevId, FNSeq, FNMerMatSeq, FTSizeBreakDown, FTSizeCode,FNSieBreakDownSeq,FTRunSize"
                _Qry &= vbCrLf & " )"
                _Qry &= vbCrLf & " SELECT A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FNHSysStyleDevId, A.FNSeq, A.FNMerMatSeq ,B.FTSizeCode,'' AS FTSizeCode,B.FNSieBreakDownSeq,'1'"
                _Qry &= vbCrLf & "  FROM"
                _Qry &= vbCrLf & " (SELECT      FTInsUser, FDInsDate, FTInsTime, FNHSysStyleDevId, FNSeq, FNMerMatSeq "
                _Qry &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_Mat  WHERE  FNHSysStyleDevId=" & _SysStyleDevId & "  ) AS A CROSS JOIN"
                _Qry &= vbCrLf & " (SELECT 'S' AS FTSizeCode,1 AS FNSieBreakDownSeq"
                _Qry &= vbCrLf & "    UNION"
                _Qry &= vbCrLf & " SELECT 'M' AS FTSizeCode,2 AS FNSieBreakDownSeq"
                _Qry &= vbCrLf & "  UNION"
                _Qry &= vbCrLf & " SELECT 'L' AS FTSizeCode,3 AS FNSieBreakDownSeq"
                _Qry &= vbCrLf & " UNION"
                _Qry &= vbCrLf & " SELECT 'XL' AS FTSizeCode,4 AS FNSieBreakDownSeq) AS B"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                _Qry = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat"
                _Qry &= vbCrLf & " ("
                _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleDevId, FNSeq, FNMerMatSeq, FTItemNo, FTItemDesc, FNPart, FTPartNameEN, FTPartNameTH, FTSuplCode,"
                _Qry &= vbCrLf & " FTStateNominate, FTUnitCode, FNPrice, FNHSysCurId, FNConSmp, FNConSmpPlus, FTComponent, FTStateActive, FTStateCombination, FTStateMainMaterial, FTStateFree, FTPositionPartId, FTPart"
                _Qry &= vbCrLf & " )"
                _Qry &= vbCrLf & "   SELECT  A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FTUpdUser, A.FDUpdDate, A.FTUpdTime, A.FNHSysStyleDevId, A.FNSeq, A.FNMerMatSeq, A.FTItemNo, A.FTItemDesc, A.FNPart, A.FTPartNameEN, A.FTPartNameTH, "
                _Qry &= vbCrLf & " A.FTSuplCode, A.FTStateNominate, A.FTUnitCode, A.FNPrice, A.FNHSysCurId, A.FNConSmp, A.FNConSmpPlus, A.FTComponent, A.FTStateActive, A.FTStateCombination, A.FTStateMainMaterial, A.FTStateFree,"
                _Qry &= vbCrLf & "   A.FTPositionPartId, A.FTPart"
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_Mat AS A LEFT OUTER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_Mat AS B ON A.FNHSysStyleDevId = B.FNHSysStyleDevId AND A.FNSeq = B.FNSeq AND A.FNMerMatSeq = B.FNMerMatSeq AND A.FTItemNo = B.FTItemNo AND A.FNPart = B.FNPart"
                _Qry &= vbCrLf & "  WHERE  A.FNHSysStyleDevId=" & _SysStyleDevId & " "
                _Qry &= vbCrLf & "  AND  (B.FNHSysStyleDevId Is NULL) "

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                _Qry = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_ColorWay"
                _Qry &= vbCrLf & " ("
                _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleDevId, FNSeq, FNMerMatSeq, FTColorWay, FNColorWaySeq, FTRunColor, FTColorCode, FTColorNameTH,  FTColorNameEN"
                _Qry &= vbCrLf & " )"
                _Qry &= vbCrLf & "   SELECT  A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FTUpdUser, A.FDUpdDate, A.FTUpdTime, A.FNHSysStyleDevId, A.FNSeq, A.FNMerMatSeq, A.FTColorWay, A.FNColorWaySeq, A.FTRunColor, A.FTColorCode, A.FTColorNameTH, A.FTColorNameEN"
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_ColorWay AS A LEFT OUTER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_ColorWay AS B ON A.FNHSysStyleDevId = B.FNHSysStyleDevId AND A.FNSeq = B.FNSeq AND A.FNMerMatSeq = B.FNMerMatSeq AND A.FTColorWay = B.FTColorWay "
                _Qry &= vbCrLf & "  WHERE  A.FNHSysStyleDevId=" & _SysStyleDevId & " "
                _Qry &= vbCrLf & "  AND  (B.FNHSysStyleDevId Is NULL) "

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                _Qry = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_SizeBreakDown"
                _Qry &= vbCrLf & " ("
                _Qry &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysStyleDevId, FNSeq, FNMerMatSeq, FTSizeBreakDown, FNSieBreakDownSeq, FTRunSize, FTSizeCode"
                _Qry &= vbCrLf & " )"
                _Qry &= vbCrLf & "   SELECT  A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FTUpdUser, A.FDUpdDate, A.FTUpdTime, A.FNHSysStyleDevId, A.FNSeq, A.FNMerMatSeq, A.FTSizeBreakDown, A.FNSieBreakDownSeq, A.FTRunSize, A.FTSizeCode "
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_SizeBreakDown AS A LEFT OUTER JOIN"
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTDevelopStyle_SizeBreakDown AS B ON A.FNHSysStyleDevId = B.FNHSysStyleDevId AND A.FNSeq = B.FNSeq AND A.FNMerMatSeq = B.FNMerMatSeq AND A.FTSizeBreakDown = B.FTSizeBreakDown "
                _Qry &= vbCrLf & "  WHERE  A.FNHSysStyleDevId=" & _SysStyleDevId & " "
                _Qry &= vbCrLf & "  AND  (B.FNHSysStyleDevId Is NULL) "

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            End If

        Catch ex As Exception
        End Try

        _Qry = " DELETE FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_Mat  WHERE  FNHSysStyleDevId=" & _SysStyleDevId & " "
        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        _Qry = " DELETE FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_ColorWay  WHERE  FNHSysStyleDevId=" & _SysStyleDevId & " "
        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        _Qry = " DELETE FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempDevelopStyle_SizeBreakDown  WHERE  FNHSysStyleDevId=" & _SysStyleDevId & " "
        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        _Spls.Close()
        Return _StateImport
    End Function
End Class