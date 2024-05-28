Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.Spreadsheet
Imports Microsoft.Win32

Public Class wImportOptiplan
    Private _DefailtPath As String

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _DefailtPath = ""

        Try
            _DefailtPath = ReadRegistry()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub FTFilePath_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTFilePath.ButtonClick
        Select Case e.Button.Index
            Case 0
                Try
                    Dim opFileDialog As New System.Windows.Forms.OpenFileDialog
                    opFileDialog.Filter = "Excel Files(*.xls;*.xlsx;*.csv)|*.xls;*.xlsx;*.csv"
                    opFileDialog.ShowDialog()

                    Try
                        If opFileDialog.FileName <> "" Then
                            Dim _Pls As New HI.TL.SplashScreen("Reading...File Please Wait...")
                            FTFilePath.Text = opFileDialog.FileName


                            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
                            System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
                            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
                            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

                            Dim _extension As String = ""
                            _extension = Path.GetExtension(opFileDialog.FileName)
                            Dim _dt As New DataTable
                            Dim _ConnString As String = ""

                            Dim regKey As RegistryKey
                            Dim _ExcelVer As String = ""

                            regKey = My.Computer.Registry.ClassesRoot.OpenSubKey("Excel.Application", False).OpenSubKey("CurVer", False)

                            _ExcelVer = regKey.GetValue("").ToString()

                            Select Case _extension.ToUpper
                                Case ".xls".ToUpper
                                    _ConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & opFileDialog.FileName & ";Extended Properties='Excel 8.0;HDR=YES';"
                                Case ".xlsx".ToUpper, ".csv".ToUpper
                                    _ConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & opFileDialog.FileName & ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';"
                            End Select

                            Dim cn As System.Data.OleDb.OleDbConnection

                            Dim cmd As System.Data.OleDb.OleDbDataAdapter

                            cn = New System.Data.OleDb.OleDbConnection(_ConnString)

                            Try
                                cmd = New System.Data.OleDb.OleDbDataAdapter("select * from [Optiplan II - 1$]", cn)
                                cn.Open()
                                cmd.Fill(_dt)
                                cn.Close()
                            Catch ex As Exception

                                Try
                                    _ConnString = ""
                                    _ConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & opFileDialog.FileName & ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';"

                                    cn = New System.Data.OleDb.OleDbConnection(_ConnString)
                                    cmd = New System.Data.OleDb.OleDbDataAdapter("select * from [Optiplan II - 1$]", cn)
                                    cn.Open()
                                    cmd.Fill(_dt)
                                    cn.Close()

                                Catch ex2 As Exception

                                    _Pls.Close()
                                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล ", 1407070002, Me.Text, "Optiplan II - 1", System.Windows.Forms.MessageBoxIcon.Warning)
                                    Exit Sub

                                End Try

                            End Try

                            Dim I As Integer = 0
                            Try
                                opshet.Document.Worksheets.Insert(0)
                                For Each wk As DevExpress.Spreadsheet.Worksheet In opshet.Document.Worksheets
                                    If I = 0 Then
                                    Else
                                        opshet.Document.Worksheets.Remove(wk)
                                    End If
                                    I = I + 1
                                Next
                            Catch ex As Exception

                            End Try

                            Try
                                opshet.Document.Worksheets(0).Name = "Optiplan II - 1"
                            Catch ex As Exception
                            End Try

                            I = 0
                            If Not (_dt Is Nothing) Then
                                If _dt.Rows.Count > 0 Then
                                    If _dt.Columns(0).ColumnName.ToUpper = "Fabric Report".ToUpper Then
                                        '' If _dt.Rows(0).Item(0).ToString.ToUpper = "Fabric Report".ToUpper Then
                                        Dim Row As DataRow = _dt.NewRow

                                        For Each Col As DataColumn In _dt.Columns

                                            If I = 0 Then
                                                Row.Item(I) = Col.ColumnName.ToString
                                            Else
                                                '  Row.Item(I) = ""
                                            End If

                                            I = I + 1

                                        Next

                                        _dt.Rows.InsertAt(Row, 0)

                                        'End If
                                    End If
                                End If
                            End If

                            Try
                                For Each wk As DevExpress.Spreadsheet.Worksheet In opshet.Document.Worksheets
                                    ' wk.Import(_dt,  True, 0, 0)
                                    wk.Import(_dt, False, 0, 0)
                                    I = I + 1
                                Next
                            Catch ex As Exception

                            End Try

                            Try

                                With opshet.Document.Worksheets(0)

                                    .Cells(0).ColumnWidth = 600
                                    .Cells(1).ColumnWidth = 300
                                    .Cells(2).ColumnWidth = 400
                                    .Cells(3).ColumnWidth = 300
                                    .Cells(4).ColumnWidth = 300
                                    .Cells(5).ColumnWidth = 300
                                    .Cells(6).ColumnWidth = 300
                                    .Cells(7).ColumnWidth = 300
                                    .Cells(8).ColumnWidth = 300
                                    .Cells(6, 3).Value = "'Cloth usage"
                                    .Cells(6, 4).Value = "'per Garment"
                                    .Cells(6, 5).Value = "2%"
                                    .Cells(6, 6).Value = "'per Garment"
                                    .Cells(6, 7).Value = "'Sum"
                                    .Cells(6, 8).Value = "'per Garment"

                                End With

                            Catch ex As Exception
                            End Try

                            _Pls.Close()

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

    Private Sub FTFilePath_EditValueChanged(sender As Object, e As EventArgs) Handles FTFilePath.EditValueChanged
        'If (Me.InvokeRequired) Then
        '    Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FTFilePath_EditValueChanged), New Object() {sender, e})
        'Else

        '    System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
        '    System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
        '    System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        '    System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

        '    Try
        '        opt2.Visible = False
        '        Me.opshet.Visible = True
        '        'New MemoryStream(System.IO.File.ReadAllBytes(tPathImg))'
        '        '
        '        'Me.opshet.LoadDocument(Me.FTFilePath.Text)

        '        Dim _extension As String = ""
        '        _extension = Path.GetExtension(Me.FTFilePath.Text)
        '        Me.opshet.LoadDocument(Me.FTFilePath.Text)

        '    Catch ex As Exception
        '        Me.opshet.Visible = False
        '        opt2.Visible = True
        '    End Try
        'End If
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Me.FTFilePath.Text = ""

        Dim I As Integer = 0

        Try

            opshet.Document.Worksheets.Insert(0)

            For Each wk As DevExpress.Spreadsheet.Worksheet In opshet.Document.Worksheets

                If I = 0 Then
                Else
                    opshet.Document.Worksheets.Remove(wk)
                End If

                I = I + 1

            Next

        Catch ex As Exception
        End Try

        Try
            opshet.Document.Worksheets(0).Name = "Sheet1"
        Catch ex As Exception
        End Try

        Me.FNHSysUnitId.Text = "M"
    End Sub

    Private Function ImportOptiplan(Optional StateDelete As Boolean = False, Optional StateByFolder As Boolean = False) As Boolean
        If Me.FTFilePath.Text <> "" Or StateByFolder Then

            If FNHSysUnitId.Text = "" Or Integer.Parse(Val(FNHSysUnitId.Properties.Tag.ToString)) <= 0 Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysUnitId_lbl.Text)
                FNHSysUnitId.Focus()
                Return False
            End If

            Dim FoundSheet As Boolean = False
            Dim tmpwk As DevExpress.Spreadsheet.Worksheet
            For Each wk As DevExpress.Spreadsheet.Worksheet In opshet.Document.Worksheets
                If wk.Name = "Optiplan II - 1" Then
                    FoundSheet = True
                    tmpwk = wk
                    Exit For
                End If
            Next

            If (FoundSheet) Then
                Dim DocType As String = ""
                Dim mark As String = ""
                With tmpwk
                    mark = .Cells(4, 0).DisplayText
                    DocType = .Cells(4, 1).DisplayText

                End With

                If mark = "" Then

                    If StateByFolder = False Then
                        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Mark กรุณาทำการตรวจสอบ !!!", 1406140100, Me.Text, AccessibleDescription, System.Windows.Forms.MessageBoxIcon.Warning)
                    End If


                    Return False
                End If

                If StateDelete Then
                    DocType = "D"
                End If

                Dim _Proc As Boolean = False

                If StateByFolder = False Then
                    Select Case DocType.ToUpper
                        Case "A".ToUpper
                            _Proc = HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการเพิ่มยอดจำนวน ใช่หรือไม่", 1406140101, "Mark " & mark)
                        Case "D".ToUpper
                            _Proc = HI.MG.ShowMsg.mConfirmProcess("คุณต้องการลบ ใช่หรือไม่", 1406140102, "Mark " & mark)
                            'Case "U".ToUpper
                            '    _Proc = HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการปรับยอดจำนวน ใช่หรือไม่", 1406140103, Me.Text, "Mark " & mark)
                        Case Else
                            _Proc = HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Import Optiplan ใช่หรือไม่ ?", 1406140104, "Mark " & mark)
                    End Select
                Else
                    _Proc = True
                End If

                Dim _Spls As HI.TL.SplashScreen

                If (_Proc) Then
                    If StateByFolder = False Then
                        _Spls = New HI.TL.SplashScreen("Importing... Please Wait ")
                    End If

                    Try
                        Dim dt As DataTable
                        Dim ColName As String

                        dt = New DataTable
                        With tmpwk
                            For c = 0 To .GetUsedRange.ColumnCount - 1

                                Try
                                    ColName = "F" & (c + 1).ToString
                                Catch ex2 As Exception
                                    ColName = c.ToString
                                End Try

                                dt.Columns.Add(ColName, GetType(String))

                            Next c

                            For r = 0 To .GetUsedRange.RowCount - 1
                                Dim Rxins As DataRow = dt.NewRow()
                                For c = 0 To .GetUsedRange.ColumnCount - 1
                                    Rxins.Item(c) = .Cells(r, c).DisplayText
                                Next c
                                dt.Rows.Add(Rxins)
                            Next r

                        End With

                        Dim IBlank As Integer = 0
                        Dim Rx As Integer = 0
                        Dim OrderNo As String = ""
                        Dim RawMatCode As String = ""
                        Dim ColorWay As String = ""
                        Dim RawMatColor As String = ""
                        Dim UsedQty As Double = 0
                        Dim PlusQty As Double = 0
                        Dim NetQty As Double = 0
                        Dim _Qry As String = ""

                        If dt.Rows.Count > 0 Then

                            Select Case DocType.ToUpper
                                Case "D".ToUpper

                                Case Else

                                    If FTStateDeletebefore.Checked Then
                                        IBlank = 0
                                        Rx = 0

                                        Dim dtdelete As New DataTable
                                        dtdelete.Columns.Add("FTOrderNo", GetType(String))

                                        For Each R As DataRow In dt.Rows
                                            OrderNo = ""

                                            If Rx > 6 And IBlank <= 0 Then
                                                If R!F1.ToString <> "" Then
                                                    OrderNo = R!F1.ToString.Split("-")(0)

                                                    If OrderNo.Contains("|") Then

                                                        For Each RxO As String In OrderNo.Split("|")
                                                            If dtdelete.Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(RxO) & "'").Length <= 0 Then

                                                                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan"
                                                                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(RxO) & "' "
                                                                _Qry &= vbCrLf & "       AND FTMark='" & HI.UL.ULF.rpQuoted(mark) & "' "

                                                                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PUR)

                                                                dtdelete.Rows.Add(RxO)

                                                            End If
                                                        Next

                                                    Else
                                                        If dtdelete.Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'").Length <= 0 Then

                                                            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan"
                                                            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                                                            _Qry &= vbCrLf & "       AND FTMark='" & HI.UL.ULF.rpQuoted(mark) & "' "

                                                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PUR)

                                                            dtdelete.Rows.Add(OrderNo)

                                                        End If
                                                    End If


                                                Else

                                                    IBlank = IBlank + 1

                                                End If

                                            End If

                                            Rx = Rx + 1

                                        Next

                                        For Each R As DataRow In dtdelete.Rows

                                            _Qry = " UPDATE A SET FNOptiplanQuantity =  CEILING(CASE WHEN MZ.FTRawMatCode IS NULL THEN 0.00 ELSE  CASE WHEN a.FNHSysUnitId = MZ.FNHSysUnitId THEN MZ.FNTotal  ELSE Convert(numeric(18,4),(MZ.FNTotal  * UCV.FNRateTo ) /UCV.FNRateFrom)  END  END   + CASE WHEN ISNULL(MZ.FNTotal,0) > 0 THEN ISNULL(A.FNHemNotOptiplan,0) ELSE 0.0 END) "
                                            _Qry &= vbCrLf & " ,FNOptiplanQuantityOrg =  CEILING(CASE WHEN MZ.FTRawMatCode IS NULL THEN 0.00 ELSE  CASE WHEN a.FNHSysUnitId = MZ.FNHSysUnitId THEN MZ.FNTotal  ELSE Convert(numeric(18,4),(MZ.FNTotal  * UCV.FNRateTo ) /UCV.FNRateFrom)  END  END)  "
                                            _Qry &= vbCrLf & " ,FNTotalRepeatOptiplan =   CASE WHEN ISNULL(A.FNRepeatLengthCM ,0) > 0 THEN CEILING((( CASE WHEN MZ.FTRawMatCode IS NULL THEN 0.00 ELSE  CASE WHEN a.FNHSysUnitId = MZ.FNHSysUnitId THEN MZ.FNTotal  ELSE Convert(numeric(18,4),(MZ.FNTotal  * UCV.FNRateTo ) /UCV.FNRateFrom)  END  END) * A.FNRepeatConvert)/A.FNRepeatLengthCM ) ELSE 0 END   "
                                            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Resource AS A INNER JOIN"
                                            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B ON A.FNHSysRawMatId = B.FNHSysRawMatId INNER JOIN"
                                            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C ON B.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
                                            _Qry &= vbCrLf & "    (SELECT FTOrderNo, FTRawMatCode, FTRawMatColorCode, SUM(FNTotal) AS FNTotal, MAX(FNHSysUnitId) AS FNHSysUnitId"
                                            _Qry &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan AS O WITH (NOLOCK)"
                                            _Qry &= vbCrLf & "   WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                                            _Qry &= vbCrLf & "   GROUP BY FTOrderNo, FTRawMatCode, FTRawMatColorCode) AS MZ ON A.FTOrderNo = MZ.FTOrderNo AND B.FTRawMatCode = MZ.FTRawMatCode AND "
                                            _Qry &= vbCrLf & "  C.FTRawMatColorCode = MZ.FTRawMatColorCode LEFT OUTER JOIN"
                                            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert AS UCV WITH(NOLOCK) ON A.FNHSysUnitId = UCV.FNHSysUnitIdTo AND MZ.FNHSysUnitId = UCV.FNHSysUnitId"
                                            _Qry &= vbCrLf & " WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "

                                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PUR)

                                        Next

                                        dtdelete.Dispose()

                                    End If

                            End Select

                            IBlank = 0
                            Rx = 0

                            Dim Consump As Decimal = 0.0

                            For Each R As DataRow In dt.Rows
                                OrderNo = ""
                                If Rx > 6 And IBlank <= 0 Then
                                    If R!F1.ToString <> "" Then

                                        OrderNo = R!F1.ToString.Split("-")(0)

                                        Try
                                            RawMatCode = R!F1.ToString.Split("-")(1)
                                        Catch ex As Exception
                                        End Try

                                        If R!F1.ToString.Split("-").Length > 2 Then

                                            For i As Integer = 2 To R!F1.ToString.Split("-").Length - 1
                                                Try
                                                    RawMatCode = RawMatCode & "-" & R!F1.ToString.Split("-")(i)
                                                Catch ex As Exception
                                                End Try
                                            Next

                                        End If

                                        ColorWay = R!F3.ToString.Split("-")(0)

                                        Try
                                            RawMatColor = R!F3.ToString.Split("-")(1)
                                        Catch ex As Exception
                                        End Try

                                        UsedQty = 0
                                        PlusQty = 0
                                        NetQty = 0
                                        Consump = 0

                                        If IsNumeric(R!F4.ToString) Then
                                            UsedQty = CDbl(R!F4.ToString)
                                        End If

                                        If IsNumeric(R!F5.ToString) Then
                                            Consump = CDbl(R!F5.ToString)
                                        End If

                                        If IsNumeric(R!F6.ToString) Then
                                            PlusQty = CDbl(R!F6.ToString)
                                        End If

                                        If IsNumeric(R!F8.ToString) Then
                                            NetQty = CDbl(R!F8.ToString)
                                        End If

                                        If OrderNo.Contains("|") Then

                                            OrderSplit(OrderNo, DocType, mark, RawMatCode, ColorWay, RawMatColor, Consump, UsedQty, PlusQty, NetQty)

                                        Else

                                            Select Case DocType.ToUpper
                                                Case "A".ToUpper

                                                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan"
                                                    _Qry &= vbCrLf & " SET FNUsed=FNUsed+" & UsedQty & ""
                                                    _Qry &= vbCrLf & " ,FNPlus=FNPlus+" & PlusQty & ""
                                                    _Qry &= vbCrLf & ", FNTotal=FNTotal+" & NetQty & ""
                                                    _Qry &= vbCrLf & ", FNConsump=" & Consump & ""
                                                    _Qry &= vbCrLf & ", FNHSysUnitId=" & Integer.Parse(Val(FNHSysUnitId.Properties.Tag.ToString)) & ""
                                                    _Qry &= vbCrLf & " ,FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                                    _Qry &= vbCrLf & " ,FDUpdDate = Convert(varchar(10),Getdate(),111)"
                                                    _Qry &= vbCrLf & " ,FTUpdTime =Convert(varchar(8),Getdate(),114)"
                                                    _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                                                    _Qry &= vbCrLf & " AND FTMark='" & HI.UL.ULF.rpQuoted(mark) & "' "
                                                    _Qry &= vbCrLf & " AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(RawMatCode) & "' "
                                                    _Qry &= vbCrLf & " AND FTMatColorCode='" & HI.UL.ULF.rpQuoted(ColorWay) & "' "
                                                    _Qry &= vbCrLf & " AND FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(RawMatColor) & "' "

                                                Case "D".ToUpper

                                                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan"
                                                    _Qry &= vbCrLf & " SET FNUsed=FNUsed-" & UsedQty & ""
                                                    _Qry &= vbCrLf & " ,FNPlus=FNPlus-" & PlusQty & ""
                                                    _Qry &= vbCrLf & ", FNTotal=FNTotal-" & NetQty & ""
                                                    _Qry &= vbCrLf & ", FNConsump=" & Consump & ""
                                                    _Qry &= vbCrLf & " ,FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                                    _Qry &= vbCrLf & " ,FDUpdDate = Convert(varchar(10),Getdate(),111)"
                                                    _Qry &= vbCrLf & " ,FTUpdTime =Convert(varchar(8),Getdate(),114)"
                                                    _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                                                    _Qry &= vbCrLf & " AND FTMark='" & HI.UL.ULF.rpQuoted(mark) & "' "
                                                    _Qry &= vbCrLf & " AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(RawMatCode) & "' "
                                                    _Qry &= vbCrLf & " AND FTMatColorCode='" & HI.UL.ULF.rpQuoted(ColorWay) & "' "
                                                    _Qry &= vbCrLf & " AND FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(RawMatColor) & "' "
                                                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PUR)

                                                    _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan"
                                                    _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                                                    _Qry &= vbCrLf & " AND FTMark='" & HI.UL.ULF.rpQuoted(mark) & "' "
                                                    _Qry &= vbCrLf & " AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(RawMatCode) & "' "
                                                    _Qry &= vbCrLf & " AND FTMatColorCode='" & HI.UL.ULF.rpQuoted(ColorWay) & "' "
                                                    _Qry &= vbCrLf & " AND FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(RawMatColor) & "' "
                                                    _Qry &= vbCrLf & " AND FNTotal<=0 "

                                                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PUR)

                                                Case Else

                                                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan"
                                                    _Qry &= vbCrLf & " SET FNUsed=" & UsedQty & ""
                                                    _Qry &= vbCrLf & " ,FNPlus=" & PlusQty & ""
                                                    _Qry &= vbCrLf & ", FNTotal=" & NetQty & ""
                                                    _Qry &= vbCrLf & ", FNConsump=" & Consump & ""
                                                    _Qry &= vbCrLf & ", FNHSysUnitId=" & Integer.Parse(Val(FNHSysUnitId.Properties.Tag.ToString)) & ""
                                                    _Qry &= vbCrLf & " ,FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                                    _Qry &= vbCrLf & " ,FDUpdDate = Convert(varchar(10),Getdate(),111)"
                                                    _Qry &= vbCrLf & " ,FTUpdTime =Convert(varchar(8),Getdate(),114)"
                                                    _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                                                    _Qry &= vbCrLf & " AND FTMark='" & HI.UL.ULF.rpQuoted(mark) & "' "
                                                    _Qry &= vbCrLf & " AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(RawMatCode) & "' "
                                                    _Qry &= vbCrLf & " AND FTMatColorCode='" & HI.UL.ULF.rpQuoted(ColorWay) & "' "
                                                    _Qry &= vbCrLf & " AND FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(RawMatColor) & "' "

                                                    If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PUR) = False Then

                                                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan"
                                                        _Qry &= vbCrLf & "  ("
                                                        _Qry &= vbCrLf & "   FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTMark, FTRawMatCode, FTMatColorCode, FTRawMatColorCode, FNUsed, FNPlus, FNTotal,FNHSysUnitId,FNConsump"
                                                        _Qry &= vbCrLf & " )"
                                                        _Qry &= vbCrLf & "  SELECT "
                                                        _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                                        _Qry &= vbCrLf & " ,Convert(varchar(10),Getdate(),111)"
                                                        _Qry &= vbCrLf & " ,Convert(varchar(8),Getdate(),114)"
                                                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(mark) & "' "
                                                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(RawMatCode) & "' "
                                                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(ColorWay) & "' "
                                                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(RawMatColor) & "' "
                                                        _Qry &= vbCrLf & " ," & UsedQty & ""
                                                        _Qry &= vbCrLf & " ," & PlusQty & ""
                                                        _Qry &= vbCrLf & " ," & NetQty & ""
                                                        _Qry &= vbCrLf & " ," & Integer.Parse(Val(FNHSysUnitId.Properties.Tag.ToString)) & ""
                                                        _Qry &= vbCrLf & " ," & Consump & ""

                                                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PUR)

                                                    End If

                                            End Select

                                            '_Qry = " UPDATE A SET FNOptiplanQuantity = ISNULL(("
                                            '_Qry &= vbCrLf & "  SELECT Convert(numeric(18,4),SUM(O.FNTotal	)"
                                            '_Qry &= vbCrLf & " 	/ CASE WHEN MAX(O.FNHSysUnitId) = A.FNHSysUnitId  OR MAX(O.FNHSysUnitId) IS NULL THEN 1 ELSE"
                                            '_Qry &= vbCrLf & "  ISNULL(("
                                            '_Qry &= vbCrLf & " 		Select TOP 1 (UV.FNRateFrom * UV.FNRateTo)"
                                            '_Qry &= vbCrLf & " 		FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert AS UV  WITH(NOLOCK) "
                                            '_Qry &= vbCrLf & " 			WHERE  (UV.FNHSysUnitId = MAX(O.FNHSysUnitId)) "
                                            '_Qry &= vbCrLf & " 			 AND (UV.FNHSysUnitIdTo = A.FNHSysUnitId)"
                                            '_Qry &= vbCrLf & "  ),0) END"
                                            '_Qry &= vbCrLf & " 	) AS FNTotal"
                                            '_Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan AS O WITH(NOLOCK)"
                                            '_Qry &= vbCrLf & "  WHERE O.FTOrderNo = A.FTOrderNo"
                                            '_Qry &= vbCrLf & "  AND O.FTRawMatCode =B.FTRawMatCode "
                                            '_Qry &= vbCrLf & "  AND O.FTRawMatColorCode=C.FTRawMatColorCode "
                                            '_Qry &= vbCrLf & " 	),0)"
                                            '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Resource AS A INNER JOIN"
                                            '_Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B ON A.FNHSysRawMatId = B.FNHSysRawMatId INNER JOIN"
                                            '_Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C ON B.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
                                            '_Qry &= vbCrLf & " WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                                            '_Qry &= vbCrLf & " AND B.FTRawMatCode='" & HI.UL.ULF.rpQuoted(RawMatCode) & "' "
                                            '_Qry &= vbCrLf & " AND C.FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(RawMatColor) & "' "


                                            _Qry = " UPDATE A SET FNOptiplanQuantity =  CEILING((CASE WHEN MZ.FTRawMatCode IS NULL THEN 0.00 ELSE  CASE WHEN a.FNHSysUnitId = MZ.FNHSysUnitId THEN MZ.FNTotal  ELSE Convert(numeric(18,4),(MZ.FNTotal  * UCV.FNRateTo ) /UCV.FNRateFrom)  END  END)  + CASE WHEN ISNULL(MZ.FNTotal,0) > 0 THEN ISNULL(A.FNHemNotOptiplan,0) ELSE 0.0 END) "
                                            _Qry &= vbCrLf & "  ,FNOptiplanQuantityOrg =  CEILING((CASE WHEN MZ.FTRawMatCode IS NULL THEN 0.00 ELSE  CASE WHEN a.FNHSysUnitId = MZ.FNHSysUnitId THEN MZ.FNTotal  ELSE Convert(numeric(18,4),(MZ.FNTotal  * UCV.FNRateTo ) /UCV.FNRateFrom)  END  END))   "
                                            _Qry &= vbCrLf & " ,FNTotalRepeatOptiplan =   CASE WHEN ISNULL(A.FNRepeatLengthCM ,0) > 0 THEN CEILING((( CASE WHEN MZ.FTRawMatCode IS NULL THEN 0.00 ELSE  CASE WHEN a.FNHSysUnitId = MZ.FNHSysUnitId THEN MZ.FNTotal  ELSE Convert(numeric(18,4),(MZ.FNTotal  * UCV.FNRateTo ) /UCV.FNRateFrom)  END  END) * A.FNRepeatConvert)/A.FNRepeatLengthCM ) ELSE 0 END   "
                                            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Resource AS A INNER JOIN"
                                            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B ON A.FNHSysRawMatId = B.FNHSysRawMatId INNER JOIN"
                                            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C ON B.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
                                            _Qry &= vbCrLf & "    (SELECT FTOrderNo, FTRawMatCode, FTRawMatColorCode, SUM(FNTotal) AS FNTotal, MAX(FNHSysUnitId) AS FNHSysUnitId"
                                            _Qry &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan AS O WITH (NOLOCK)"
                                            _Qry &= vbCrLf & "   WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                                            _Qry &= vbCrLf & "   GROUP BY FTOrderNo, FTRawMatCode, FTRawMatColorCode) AS MZ ON A.FTOrderNo = MZ.FTOrderNo AND B.FTRawMatCode = MZ.FTRawMatCode AND "
                                            _Qry &= vbCrLf & "  C.FTRawMatColorCode = MZ.FTRawMatColorCode LEFT OUTER JOIN"
                                            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert AS UCV WITH(NOLOCK) ON A.FNHSysUnitId = UCV.FNHSysUnitIdTo AND MZ.FNHSysUnitId = UCV.FNHSysUnitId"
                                            _Qry &= vbCrLf & " WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                                            _Qry &= vbCrLf & " AND B.FTRawMatCode='" & HI.UL.ULF.rpQuoted(RawMatCode) & "' "
                                            _Qry &= vbCrLf & " AND C.FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(RawMatColor) & "' "
                                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PUR)

                                        End If


                                    Else
                                        IBlank = IBlank + 1
                                    End If

                                End If

                                Rx = Rx + 1
                            Next

                        End If

                        If StateByFolder = False Then
                            _Spls.Close()
                            HI.MG.ShowMsg.mInfo("Process Complete !!!", 1606140778, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)


                        End If


                        Return True

                    Catch ex As Exception
                        If StateByFolder = False Then
                            _Spls.Close()
                            HI.MG.ShowMsg.mInfo("Process Not Complete !!!", 1606141778, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
                        End If
                        Return False
                    End Try

                End If
            Else
                If StateByFolder = False Then
                    HI.MG.ShowMsg.mInfo("ไม่พบ Sheet Name +++", 1606140077, Me.Text, "Optiplan II - 1", System.Windows.Forms.MessageBoxIcon.Warning)
                End If
                Return False
            End If
        Else
            If StateByFolder = False Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก File !!!", 1606140078, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If
            Return False
        End If
    End Function

    Private Sub OrderSplit(AllOrder As String, DocType As String, mark As String, RawMatCode As String, ColorWay As String, RawMatColor As String, Consump As Decimal, AllUsedQty As Double, AllPlusQty As Double, AllNetQty As Double)

        Dim _Qry As String = ""
        Dim UsedQty As Double = 0
        Dim PlusQty As Double = 0
        Dim NetQty As Double = 0
        Dim TotalPlanQty As Double = 0
        Dim OrderQty As Double = 0
        Dim cmdstring As String = ""
        Dim dtOrder As New DataTable

        cmdstring = "EXEC   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_IMPORT_OPTOPLAN_GETDATA_MULTI @FTMOrderNo='" & HI.UL.ULF.rpQuoted(AllOrder) & "',@MatCode='" & HI.UL.ULF.rpQuoted(RawMatCode) & "',@MatColor='" & HI.UL.ULF.rpQuoted(RawMatColor) & "',@Colorway='" & HI.UL.ULF.rpQuoted(ColorWay) & "'"

        dtOrder = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)

        For Each R As DataRow In dtOrder.Rows
            TotalPlanQty = TotalPlanQty + Val(R!FNQuantity.ToString)
        Next

        If dtOrder.Rows.Count > 0 Then

            For Each OrderNo As String In AllOrder.Split("|")

                UsedQty = 0
                PlusQty = 0
                NetQty = 0
                OrderQty = 0

                For Each R As DataRow In dtOrder.Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'")
                    OrderQty = Val(R!FNQuantity.ToString)
                Next


                If OrderQty >= 0 And TotalPlanQty > 0 Then

                    UsedQty = CDbl(Format((OrderQty * AllUsedQty) / TotalPlanQty, "0.000"))
                    PlusQty = CDbl(Format((OrderQty * AllPlusQty) / TotalPlanQty, "0.000"))
                    NetQty = CDbl(Format((OrderQty * AllNetQty) / TotalPlanQty, "0.000"))

                End If

                Select Case DocType.ToUpper
                    Case "A".ToUpper

                        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan"
                        _Qry &= vbCrLf & " SET FNUsed=FNUsed+" & UsedQty & ""
                        _Qry &= vbCrLf & " ,FNPlus=FNPlus+" & PlusQty & ""
                        _Qry &= vbCrLf & ", FNTotal=FNTotal+" & NetQty & ""
                        _Qry &= vbCrLf & ", FNConsump=" & Consump & ""
                        _Qry &= vbCrLf & ", FNHSysUnitId=" & Integer.Parse(Val(FNHSysUnitId.Properties.Tag.ToString)) & ""
                        _Qry &= vbCrLf & " ,FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & " ,FDUpdDate = Convert(varchar(10),Getdate(),111)"
                        _Qry &= vbCrLf & " ,FTUpdTime =Convert(varchar(8),Getdate(),114)"
                        _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                        _Qry &= vbCrLf & " AND FTMark='" & HI.UL.ULF.rpQuoted(mark) & "' "
                        _Qry &= vbCrLf & " AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(RawMatCode) & "' "
                        _Qry &= vbCrLf & " AND FTMatColorCode='" & HI.UL.ULF.rpQuoted(ColorWay) & "' "
                        _Qry &= vbCrLf & " AND FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(RawMatColor) & "' "

                    Case "D".ToUpper

                        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan"
                        _Qry &= vbCrLf & " SET FNUsed=FNUsed-" & UsedQty & ""
                        _Qry &= vbCrLf & " ,FNPlus=FNPlus-" & PlusQty & ""
                        _Qry &= vbCrLf & ", FNTotal=FNTotal-" & NetQty & ""
                        _Qry &= vbCrLf & ", FNConsump=" & Consump & ""
                        _Qry &= vbCrLf & " ,FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & " ,FDUpdDate = Convert(varchar(10),Getdate(),111)"
                        _Qry &= vbCrLf & " ,FTUpdTime =Convert(varchar(8),Getdate(),114)"
                        _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                        _Qry &= vbCrLf & " AND FTMark='" & HI.UL.ULF.rpQuoted(mark) & "' "
                        _Qry &= vbCrLf & " AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(RawMatCode) & "' "
                        _Qry &= vbCrLf & " AND FTMatColorCode='" & HI.UL.ULF.rpQuoted(ColorWay) & "' "
                        _Qry &= vbCrLf & " AND FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(RawMatColor) & "' "
                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PUR)

                        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan"
                        _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                        _Qry &= vbCrLf & " AND FTMark='" & HI.UL.ULF.rpQuoted(mark) & "' "
                        _Qry &= vbCrLf & " AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(RawMatCode) & "' "
                        _Qry &= vbCrLf & " AND FTMatColorCode='" & HI.UL.ULF.rpQuoted(ColorWay) & "' "
                        _Qry &= vbCrLf & " AND FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(RawMatColor) & "' "
                        _Qry &= vbCrLf & " AND FNTotal<=0 "

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PUR)

                    Case Else

                        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan"
                        _Qry &= vbCrLf & " SET FNUsed=" & UsedQty & ""
                        _Qry &= vbCrLf & " ,FNPlus=" & PlusQty & ""
                        _Qry &= vbCrLf & ", FNTotal=" & NetQty & ""
                        _Qry &= vbCrLf & ", FNConsump=" & Consump & ""
                        _Qry &= vbCrLf & ", FNHSysUnitId=" & Integer.Parse(Val(FNHSysUnitId.Properties.Tag.ToString)) & ""
                        _Qry &= vbCrLf & " ,FTUpdUser ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & " ,FDUpdDate = Convert(varchar(10),Getdate(),111)"
                        _Qry &= vbCrLf & " ,FTUpdTime =Convert(varchar(8),Getdate(),114)"
                        _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                        _Qry &= vbCrLf & " AND FTMark='" & HI.UL.ULF.rpQuoted(mark) & "' "
                        _Qry &= vbCrLf & " AND FTRawMatCode='" & HI.UL.ULF.rpQuoted(RawMatCode) & "' "
                        _Qry &= vbCrLf & " AND FTMatColorCode='" & HI.UL.ULF.rpQuoted(ColorWay) & "' "
                        _Qry &= vbCrLf & " AND FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(RawMatColor) & "' "

                        If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PUR) = False Then

                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan"
                            _Qry &= vbCrLf & "  ("
                            _Qry &= vbCrLf & "   FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTMark, FTRawMatCode, FTMatColorCode, FTRawMatColorCode, FNUsed, FNPlus, FNTotal,FNHSysUnitId,FNConsump"
                            _Qry &= vbCrLf & " )"
                            _Qry &= vbCrLf & "  SELECT "
                            _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Qry &= vbCrLf & " ,Convert(varchar(10),Getdate(),111)"
                            _Qry &= vbCrLf & " ,Convert(varchar(8),Getdate(),114)"
                            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(mark) & "' "
                            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(RawMatCode) & "' "
                            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(ColorWay) & "' "
                            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(RawMatColor) & "' "
                            _Qry &= vbCrLf & " ," & UsedQty & ""
                            _Qry &= vbCrLf & " ," & PlusQty & ""
                            _Qry &= vbCrLf & " ," & NetQty & ""
                            _Qry &= vbCrLf & " ," & Integer.Parse(Val(FNHSysUnitId.Properties.Tag.ToString)) & ""
                            _Qry &= vbCrLf & " ," & Consump & ""

                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PUR)

                        End If

                End Select



                _Qry = " UPDATE A SET FNOptiplanQuantity =  CASE WHEN MZ.FTRawMatCode IS NULL THEN 0.00 ELSE  CASE WHEN a.FNHSysUnitId = MZ.FNHSysUnitId THEN MZ.FNTotal  ELSE Convert(numeric(18,4),(MZ.FNTotal  * UCV.FNRateTo ) /UCV.FNRateFrom)  END  END "
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Resource AS A INNER JOIN"
                _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B ON A.FNHSysRawMatId = B.FNHSysRawMatId INNER JOIN"
                _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C ON B.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
                _Qry &= vbCrLf & "    (SELECT FTOrderNo, FTRawMatCode, FTRawMatColorCode, SUM(FNTotal) AS FNTotal, MAX(FNHSysUnitId) AS FNHSysUnitId"
                _Qry &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTImportOptiplan AS O WITH (NOLOCK)"
                _Qry &= vbCrLf & "   WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                _Qry &= vbCrLf & "   GROUP BY FTOrderNo, FTRawMatCode, FTRawMatColorCode) AS MZ ON A.FTOrderNo = MZ.FTOrderNo AND B.FTRawMatCode = MZ.FTRawMatCode AND "
                _Qry &= vbCrLf & "  C.FTRawMatColorCode = MZ.FTRawMatColorCode LEFT OUTER JOIN"
                _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert AS UCV WITH(NOLOCK) ON A.FNHSysUnitId = UCV.FNHSysUnitIdTo AND MZ.FNHSysUnitId = UCV.FNHSysUnitId"
                _Qry &= vbCrLf & " WHERE A.FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "' "
                _Qry &= vbCrLf & " AND B.FTRawMatCode='" & HI.UL.ULF.rpQuoted(RawMatCode) & "' "
                _Qry &= vbCrLf & " AND C.FTRawMatColorCode='" & HI.UL.ULF.rpQuoted(RawMatColor) & "' "
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PUR)

            Next

        End If





        dtOrder.Dispose()
    End Sub

    Private Sub ocmimportoptiplan_Click(sender As Object, e As EventArgs) Handles ocmimportoptiplan.Click
        Call ImportOptiplan()

    End Sub

    Private Sub wImportOptiplan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        FTFilePath.Focus()
    End Sub

    Private Sub wImportOptiplan_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysUnitId.Text = "M"
    End Sub

    Private Sub ocmdeleteoptiplan_Click(sender As Object, e As EventArgs) Handles ocmdeleteoptiplan.Click
        Call ImportOptiplan(True)
    End Sub

    Private Sub ocmimportoptiplanallinfolder_Click(sender As Object, e As EventArgs) Handles ocmimportoptiplanallinfolder.Click

        If FNHSysUnitId.Text = "" Or Integer.Parse(Val(FNHSysUnitId.Properties.Tag.ToString)) <= 0 Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysUnitId_lbl.Text)
            FNHSysUnitId.Focus()
            Exit Sub
        End If

        Dim TotalComplete As Integer = 0
        Dim TotalNotComplete As Integer = 0

        Try



            Dim Op As New System.Windows.Forms.FolderBrowserDialog

                If _DefailtPath <> "" Then
                    Op.SelectedPath = _DefailtPath
                End If

                Try
                    If Op.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                        If _DefailtPath <> Op.SelectedPath Then

                            WriteRegistry(Op.SelectedPath)
                            _DefailtPath = Op.SelectedPath

                        End If


                    Dim fileEntries As String() = Directory.GetFiles(Op.SelectedPath, "*.xls")
                    '"xls,*.xlsx,*.csv"
                    ' Directory.EnumerateFiles(Op.SelectedPath, "*.*", SearchOption.AllDirectories).Where(s >= s.EndsWith(".mp3") || s.EndsWith(".jpg"))

                    ' Process the list of .txt files found in the directory. '

                    If fileEntries.Length > 0 Then

                        Dim fileName As String
                        Dim _Pls As New HI.TL.SplashScreen("Reading...File Please Wait...")


                        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
                        System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
                        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
                        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"


                        For Each fileName In fileEntries
                            If (System.IO.File.Exists(fileName)) Then
                                'Read File and Print Result if its true
                                FTFilePath.Text = fileName

                                Dim _extension As String = ""
                                _extension = Path.GetExtension(fileName)
                                Dim _dt As New DataTable
                                Dim _ConnString As String = ""
                                Dim StaetReadFile As Boolean = False

                                Dim regKey As RegistryKey
                                Dim _ExcelVer As String = ""

                                regKey = My.Computer.Registry.ClassesRoot.OpenSubKey("Excel.Application", False).OpenSubKey("CurVer", False)

                                _ExcelVer = regKey.GetValue("").ToString()

                                Select Case _extension.ToUpper
                                    Case ".xls".ToUpper
                                        _ConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fileName & ";Extended Properties='Excel 8.0;HDR=YES';"
                                    Case ".xlsx".ToUpper, ".csv".ToUpper
                                        _ConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fileName & ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';"
                                End Select

                                Dim cn As System.Data.OleDb.OleDbConnection

                                Dim cmd As System.Data.OleDb.OleDbDataAdapter

                                cn = New System.Data.OleDb.OleDbConnection(_ConnString)

                                Try
                                    cmd = New System.Data.OleDb.OleDbDataAdapter("select * from [Optiplan II - 1$]", cn)
                                    cn.Open()
                                    cmd.Fill(_dt)
                                    cn.Close()

                                    StaetReadFile = True
                                Catch ex As Exception

                                    Try
                                        _ConnString = ""
                                        _ConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fileName & ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';"

                                        cn = New System.Data.OleDb.OleDbConnection(_ConnString)
                                        cmd = New System.Data.OleDb.OleDbDataAdapter("select * from [Optiplan II - 1$]", cn)
                                        cn.Open()
                                        cmd.Fill(_dt)
                                        cn.Close()
                                        StaetReadFile = True
                                    Catch ex2 As Exception



                                    End Try

                                End Try


                                If StaetReadFile Then
                                    Dim I As Integer = 0
                                    Try
                                        opshet.Document.Worksheets.Insert(0)
                                        For Each wk As DevExpress.Spreadsheet.Worksheet In opshet.Document.Worksheets
                                            If I = 0 Then
                                            Else
                                                opshet.Document.Worksheets.Remove(wk)
                                            End If
                                            I = I + 1
                                        Next
                                    Catch ex As Exception

                                    End Try

                                    Try
                                        opshet.Document.Worksheets(0).Name = "Optiplan II - 1"
                                    Catch ex As Exception
                                    End Try

                                    I = 0
                                    If Not (_dt Is Nothing) Then
                                        If _dt.Rows.Count > 0 Then
                                            If _dt.Columns(0).ColumnName.ToUpper = "Fabric Report".ToUpper Then
                                                '' If _dt.Rows(0).Item(0).ToString.ToUpper = "Fabric Report".ToUpper Then
                                                Dim Row As DataRow = _dt.NewRow

                                                For Each Col As DataColumn In _dt.Columns

                                                    If I = 0 Then
                                                        Row.Item(I) = Col.ColumnName.ToString
                                                    Else
                                                        '  Row.Item(I) = ""
                                                    End If

                                                    I = I + 1

                                                Next

                                                _dt.Rows.InsertAt(Row, 0)

                                                'End If
                                            End If
                                        End If
                                    End If

                                    Try
                                        For Each wk As DevExpress.Spreadsheet.Worksheet In opshet.Document.Worksheets
                                            ' wk.Import(_dt,  True, 0, 0)
                                            wk.Import(_dt, False, 0, 0)
                                            I = I + 1
                                        Next
                                    Catch ex As Exception

                                    End Try

                                    Try

                                        With opshet.Document.Worksheets(0)

                                            .Cells(0).ColumnWidth = 600
                                            .Cells(1).ColumnWidth = 300
                                            .Cells(2).ColumnWidth = 400
                                            .Cells(3).ColumnWidth = 300
                                            .Cells(4).ColumnWidth = 300
                                            .Cells(5).ColumnWidth = 300
                                            .Cells(6).ColumnWidth = 300
                                            .Cells(7).ColumnWidth = 300
                                            .Cells(8).ColumnWidth = 300
                                            .Cells(6, 3).Value = "'Cloth usage"
                                            .Cells(6, 4).Value = "'per Garment"
                                            .Cells(6, 5).Value = "2%"
                                            .Cells(6, 6).Value = "'per Garment"
                                            .Cells(6, 7).Value = "'Sum"
                                            .Cells(6, 8).Value = "'per Garment"

                                        End With

                                    Catch ex As Exception
                                    End Try

                                    Dim State As Boolean = ImportOptiplan(False, True)

                                    If State Then
                                        TotalComplete = TotalComplete + 1
                                    Else
                                        TotalNotComplete = TotalNotComplete + 1
                                    End If
                                End If

                            End If

                        Next


                        _Pls.Close()

                    Else

                    End If

                End If
            Catch ex As Exception
            End Try

            If TotalComplete > 0 Or TotalNotComplete > 0 Then
                HI.MG.ShowMsg.mInfo("Import  Data Complete..", 122330400, Me.Text, "Complete " & TotalComplete.ToString() & " File Not Complete " & TotalNotComplete.ToString() & " File ", MessageBoxIcon.Information)
            End If

        Catch ex As Exception
        End Try

    End Sub



    Public Shared Function ReadRegistry() As String
        Dim regKey As RegistryKey
        Dim valreturn As String = ""

        regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        If regKey Is Nothing Then

            Registry.CurrentUser.CreateSubKey("Software\HI SOFT", RegistryKeyPermissionCheck.ReadWriteSubTree)
            regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        End If

        valreturn = regKey.GetValue("PathImportOptiplan", "")
        regKey.Close()

        Return valreturn
    End Function

    Public Shared Sub WriteRegistry(ByVal value As Object)

        Dim regKey As RegistryKey
        regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        If regKey Is Nothing Then

            Registry.CurrentUser.CreateSubKey("Software\HI SOFT", True)
            regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        End If

        regKey.SetValue("PathImportOptiplan", value.ToString)
        regKey.Close()

    End Sub

End Class