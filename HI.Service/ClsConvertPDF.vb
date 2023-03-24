Option Explicit On

Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns

Imports System.Data
Imports System.Data.SqlClient
Imports System.IO



Public Class ClsConvertPDF

    Friend Shared _Count_PDF As Integer = 0

#Region " Property "

    Friend Shared DTPurchaseNo As DataTable
    Friend Property Data_DTPurchaseNo As DataTable
        Get
            Return DTPurchaseNo
        End Get
        Set(ByVal value As DataTable)
            DTPurchaseNo = value
        End Set
    End Property


#End Region

#Region "Function "

    Public Shared Function Find_PurchaseNO() As DataTable
        Try


            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            _str = ""
            _str = " SELECT A.FTPurchaseNo,A.FTPurchaseBy,A.FNPoState,A.FTPOTypeState  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.V_TPURTPurchase AS A "
            _str &= Environment.NewLine & " WHERE (FTStateSendApp = '1') AND (FTStateSuperVisorApp = '1') "
            _str &= Environment.NewLine & " AND (FTStateManagerApp ='1')  AND (ISNULL(FTStatePDF,'0') <> '1') "
            _str &= Environment.NewLine & " AND (FTPurchaseBy = '" & HI.ST.UserInfo.UserName & "')"

            ' ใช้ทดสอบ
            '  _str &= Environment.NewLine & " AND (FDInsDate = '2014/06/02')"

            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_PUR)

            If _dt.Rows.Count > 0 Then
                _Count_PDF = _dt.Rows.Count
                Return _dt
            Else
                _Count_PDF = 0
                Return Nothing
            End If

            _dt.Dispose()


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return Nothing
    End Function

    Public Shared Sub Validate_PDF()

        ' Dim a As String = Environment.UserName  ' user login เข้าเครื่อง
        '  HI.ST.SysInfo.StateDirector = True     ' true ทดสอบ Super     false ทดสอบ Manager

        DTPurchaseNo = Nothing
        DTPurchaseNo = Find_PurchaseNO()

        '  MessageBox.Show(_Count_PDF)

        If _Count_PDF > 0 Then

            'HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR)
            'HI.Conn.SQLConn.SqlConnectionOpen()
            'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            'HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For i = 0 To DTPurchaseNo.Rows.Count - 1

                Select Case DTPurchaseNo.Rows(i)("FTPOTypeState").ToString()
                    Case "2"
                        Call Update_Pusrchase_Service_PDF(DTPurchaseNo.Rows(i)("FTPurchaseNo").ToString(), DTPurchaseNo.Rows(i)("FTPurchaseBy").ToString(), Integer.Parse(Val(DTPurchaseNo.Rows(i)("FNPoState").ToString())))
                    Case "3"
                        Call Update_Pusrchase_Number2(DTPurchaseNo.Rows(i)("FTPurchaseNo").ToString(), DTPurchaseNo.Rows(i)("FTPurchaseBy").ToString(), Integer.Parse(Val(DTPurchaseNo.Rows(i)("FNPoState").ToString())))
                    Case "4"
                        Call Update_Pusrchase_Sample(DTPurchaseNo.Rows(i)("FTPurchaseNo").ToString(), DTPurchaseNo.Rows(i)("FTPurchaseBy").ToString(), Integer.Parse(Val(DTPurchaseNo.Rows(i)("FNPoState").ToString())))
                    Case Else
                        Call Update_Pusrchase_PDF(DTPurchaseNo.Rows(i)("FTPurchaseNo").ToString(), DTPurchaseNo.Rows(i)("FTPurchaseBy").ToString(), Integer.Parse(Val(DTPurchaseNo.Rows(i)("FNPoState").ToString())))
                End Select

                'If DTPurchaseNo.Rows(i)("FTPOTypeState").ToString() = "2" Then
                '    Call Update_Pusrchase_Service_PDF(DTPurchaseNo.Rows(i)("FTPurchaseNo").ToString(), DTPurchaseNo.Rows(i)("FTPurchaseBy").ToString(), Integer.Parse(Val(DTPurchaseNo.Rows(i)("FNPoState").ToString())))

                'Else
                '    Call Update_Pusrchase_PDF(DTPurchaseNo.Rows(i)("FTPurchaseNo").ToString(), DTPurchaseNo.Rows(i)("FTPurchaseBy").ToString(), Integer.Parse(Val(DTPurchaseNo.Rows(i)("FNPoState").ToString())))
                'End If

            Next

        End If

        DTPurchaseNo = Nothing
    End Sub

    Public Shared Function Convert_PDF(ByVal Temp_PurchaseNO As String, ByVal Temp_FTPurchaseBy As String, ByVal POState As Integer) As Boolean
        Dim _Return As Boolean = False
        Try
            With New HI.RP.Report
                .FormTitle = "Convert To " & Temp_PurchaseNO & ".pdf"
                .ReportFolderName = "PurchaseOrder\"  '"Purchase Report\" '
                .ReportName = "PurchaseOrder.rpt"
                .AddParameter("Draft", "")
                .Formular = "{TPURTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(Temp_PurchaseNO) & "'"

                ' ตรวจสอบ โฟร์เดอร์ก่อน

                Dim _CheckPath As String = Application.StartupPath & "\PO PDF\" & Temp_FTPurchaseBy

                '  MessageBox.Show(_CheckPath)
                'Dim _CheckPath As String = "\\hisoft_svr\HI SOFT SYSTEM\PO PDF\" & Temp_FTPurchaseBy

                If Not Directory.Exists(_CheckPath) Then
                    Directory.CreateDirectory(_CheckPath)
                    ' MessageBox.Show("CreateDirectory")
                Else
                    ' MessageBox.Show("Not")
                End If


                .PathExport = Application.StartupPath & "\PO PDF\" & Temp_FTPurchaseBy & "\"
                '.PathExport = "\\hisoft_svr\HI SOFT SYSTEM\PO PDF\" & Temp_FTPurchaseBy & "\"
                .ExportName = Temp_PurchaseNO
                .ExportFile = RP.Report.ExFile.PDF

                ' กรณีหาไฟล์ไม่เจอ  ????
                .PrevieNoSplash(POState)


                Dim _FIleExportPDFName As String = .ExportFileSuccessName

                If System.IO.File.Exists(Application.StartupPath & "\PO PDF\" & Temp_FTPurchaseBy & "\" & _FIleExportPDFName) Then
                    Return True
                Else
                    Return False
                End If


            End With
        Catch ex As Exception
            Return False
        End Try

        Return False

    End Function


    'Public Shared Function Convert_PDF(ByVal Temp_PurchaseNO As String) As Boolean
    '    Dim _Return As Boolean = False
    '    Try
    '        With New HI.RP.Report
    '            .FormTitle = "Convert To " & Temp_PurchaseNO & ".pdf"
    '            .ReportFolderName = "PurchaseOrder\"
    '            .ReportName = "PurchaseOrder.rpt"
    '            .AddParameter("Draft", "")
    '            .Formular = "{TPURTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(Temp_PurchaseNO) & "'"
    '            .PathExport = Application.StartupPath & "\PO PDF\"
    '            .ExportName = Temp_PurchaseNO
    '            .ExportFile = RP.Report.ExFile.PDF
    '            .Preview()
    '            _Return = True
    '        End With
    '    Catch ex As Exception
    '        _Return = False
    '    End Try

    '    Return _Return

    'End Function


    Public Shared Sub Update_Pusrchase_PDF(ByVal Temp_FTPurchaseNo As String, ByVal Temp_FTPurchaseBy As String, ByVal POState As Integer) 'As Boolean

        Dim _Str As String = String.Empty

        Try

            If Convert_PDF(Temp_FTPurchaseNo, Temp_FTPurchaseBy, POState) = True Then
                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try

                    _Str = ""
                    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                    _Str &= Environment.NewLine & "SET  [FTStatePDF] = '1'"
                    _Str &= Environment.NewLine & ", [FTPDFDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTPDFTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTPurchase] "
                    _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & Temp_FTPurchaseNo & "'"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        ' Return False

                    End If

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Catch ex As Exception
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                End Try

            End If

          





            ' MessageBox.Show("Tran.Commit")

            ' Return True



        Catch ex As Exception
            MessageBox.Show(ex.Message)
            ' Return False
        End Try

    End Sub


    Public Shared Sub Update_Pusrchase_Service_PDF(ByVal Temp_FTPurchaseNo As String, ByVal Temp_FTPurchaseBy As String, ByVal POState As Integer) 'As Boolean

        Dim _Str As String = String.Empty

        Try

            If POServiseConvert_PDF(Temp_FTPurchaseNo, Temp_FTPurchaseBy, POState) Then

                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try

                    _Str = ""
                    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchaseService "
                    _Str &= Environment.NewLine & "SET  [FTStatePDF] = '1'"
                    _Str &= Environment.NewLine & ", [FTPDFDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTPDFTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTPurchaseService] "
                    _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & Temp_FTPurchaseNo & "'"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        ' Return False
                    End If

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Catch ex As Exception

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                End Try

            End If
           
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            ' Return False
        End Try

    End Sub


    Public Shared Function POServiseConvert_PDF(ByVal Temp_PurchaseNO As String, ByVal Temp_FTPurchaseBy As String, ByVal POState As Integer) As Boolean
        Dim _Return As Boolean = False
        Try
            With New HI.RP.Report
                .FormTitle = "Convert To " & Temp_PurchaseNO & ".pdf"
                .ReportFolderName = "PurchaseOrder\"  '"Purchase Report\" '
                .ReportName = "PurchaseService.rpt"
                .AddParameter("Draft", "")
                .Formular = "{TPURTPurchaseService.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(Temp_PurchaseNO) & "'"

                ' ตรวจสอบ โฟร์เดอร์ก่อน

                Dim _CheckPath As String = Application.StartupPath & "\PO PDF\" & Temp_FTPurchaseBy

                '  MessageBox.Show(_CheckPath)
                'Dim _CheckPath As String = "\\hisoft_svr\HI SOFT SYSTEM\PO PDF\" & Temp_FTPurchaseBy

                If Not Directory.Exists(_CheckPath) Then
                    Directory.CreateDirectory(_CheckPath)
                    ' MessageBox.Show("CreateDirectory")
                Else
                    ' MessageBox.Show("Not")
                End If

                .PathExport = Application.StartupPath & "\PO PDF\" & Temp_FTPurchaseBy & "\"
                '.PathExport = "\\hisoft_svr\HI SOFT SYSTEM\PO PDF\" & Temp_FTPurchaseBy & "\"
                .ExportName = Temp_PurchaseNO
                .ExportFile = RP.Report.ExFile.PDF

                ' กรณีหาไฟล์ไม่เจอ  ????
                .PrevieNoSplash(POState)

                Dim _FIleExportPDFName As String = .ExportFileSuccessName

                If System.IO.File.Exists(Application.StartupPath & "\PO PDF\" & Temp_FTPurchaseBy & "\" & _FIleExportPDFName) Then
                    Return True
                Else
                    Return False
                End If

            End With
        Catch ex As Exception
            Return False
        End Try

        Return False

    End Function

    Public Shared Sub Update_Pusrchase_Number2(ByVal Temp_FTPurchaseNo As String, ByVal Temp_FTPurchaseBy As String, ByVal POState As Integer) 'As Boolean

        Dim _Str As String = String.Empty

        Try

            If PO2Convert_PDF(Temp_FTPurchaseNo, Temp_FTPurchaseBy, POState) Then

                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try

                    _Str = ""
                    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase "
                    _Str &= Environment.NewLine & "SET  [FTStatePDF] = '1'"
                    _Str &= Environment.NewLine & ", [FTPDFDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTPDFTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTFacPurchase] "
                    _Str &= Environment.NewLine & " WHERE FTFacPurchaseNo = '" & Temp_FTPurchaseNo & "'"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        ' Return False
                    End If

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Catch ex As Exception

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                End Try

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            ' Return False
        End Try

    End Sub

    Public Shared Sub Update_Pusrchase_Sample(ByVal Temp_FTPurchaseNo As String, ByVal Temp_FTPurchaseBy As String, ByVal POState As Integer) 'As Boolean

        Dim _Str As String = String.Empty

        Try

            If POSampleConvert_PDF(Temp_FTPurchaseNo, Temp_FTPurchaseBy, POState) = True Then
                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_SAMPLE)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Try

                    _Str = ""
                    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase "
                    _Str &= Environment.NewLine & "SET  [FTStatePDF] = '1'"
                    _Str &= Environment.NewLine & ", [FTPDFDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTPDFTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].[dbo].[TSMPPurchase] "
                    _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & Temp_FTPurchaseNo & "'"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        ' Return False

                    End If

                    HI.Conn.SQLConn.Tran.Commit()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Catch ex As Exception
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                End Try

            End If







            ' MessageBox.Show("Tran.Commit")

            ' Return True



        Catch ex As Exception
            MessageBox.Show(ex.Message)
            ' Return False
        End Try

    End Sub

    Public Shared Function POSampleConvert_PDF(ByVal Temp_PurchaseNO As String, ByVal Temp_FTPurchaseBy As String, ByVal POState As Integer) As Boolean
        Dim _Return As Boolean = False
        Try
            With New HI.RP.Report
                .FormTitle = "Convert To " & Temp_PurchaseNO & ".pdf"
                .ReportFolderName = "PurchaseOrder\"
                .ReportName = "PurchaseSample.rpt"
                .Formular = "{TSMPPurchase.FTPurchaseNo} ='" & HI.UL.ULF.rpQuoted(Temp_PurchaseNO) & "' "

                ' ตรวจสอบ โฟร์เดอร์ก่อน

                Dim _CheckPath As String = Application.StartupPath & "\PO PDF\" & Temp_FTPurchaseBy

                '  MessageBox.Show(_CheckPath)
                'Dim _CheckPath As String = "\\hisoft_svr\HI SOFT SYSTEM\PO PDF\" & Temp_FTPurchaseBy

                If Not Directory.Exists(_CheckPath) Then
                    Directory.CreateDirectory(_CheckPath)
                    ' MessageBox.Show("CreateDirectory")
                Else
                    ' MessageBox.Show("Not")
                End If

                .PathExport = Application.StartupPath & "\PO PDF\" & Temp_FTPurchaseBy & "\"
                '.PathExport = "\\hisoft_svr\HI SOFT SYSTEM\PO PDF\" & Temp_FTPurchaseBy & "\"
                .ExportName = Temp_PurchaseNO
                .ExportFile = RP.Report.ExFile.PDF

                ' กรณีหาไฟล์ไม่เจอ  ????
                .PrevieNoSplash(POState)

                Dim _FIleExportPDFName As String = .ExportFileSuccessName

                If System.IO.File.Exists(Application.StartupPath & "\PO PDF\" & Temp_FTPurchaseBy & "\" & _FIleExportPDFName) Then
                    Return True
                Else
                    Return False
                End If

            End With
        Catch ex As Exception
            Return False
        End Try

        Return False

    End Function


    Public Shared Function PO2Convert_PDF(ByVal Temp_PurchaseNO As String, ByVal Temp_FTPurchaseBy As String, ByVal POState As Integer) As Boolean
        'Dim _Return As Boolean = False
        'Try
        '    With New HI.RP.Report
        '        .FormTitle = "Convert To " & Temp_PurchaseNO & ".pdf"
        '        .ReportFolderName = "PurchaseOrder\"  '"Purchase Report\" '
        '        .ReportName = "PurchaseService.rpt"
        '        .AddParameter("Draft", "")
        '        .Formular = "{TPURTFacPurchase.FTFacPurchaseNo}='" & HI.UL.ULF.rpQuoted(Temp_PurchaseNO) & "'"

        '        ' ตรวจสอบ โฟร์เดอร์ก่อน

        '        Dim _CheckPath As String = Application.StartupPath & "\PO PDF\" & Temp_FTPurchaseBy

        '        '  MessageBox.Show(_CheckPath)
        '        'Dim _CheckPath As String = "\\hisoft_svr\HI SOFT SYSTEM\PO PDF\" & Temp_FTPurchaseBy

        '        If Not Directory.Exists(_CheckPath) Then
        '            Directory.CreateDirectory(_CheckPath)
        '            ' MessageBox.Show("CreateDirectory")
        '        Else
        '            ' MessageBox.Show("Not")
        '        End If

        '        .PathExport = Application.StartupPath & "\PO PDF\" & Temp_FTPurchaseBy & "\"
        '        '.PathExport = "\\hisoft_svr\HI SOFT SYSTEM\PO PDF\" & Temp_FTPurchaseBy & "\"
        '        .ExportName = Temp_PurchaseNO
        '        .ExportFile = RP.Report.ExFile.PDF

        '        ' กรณีหาไฟล์ไม่เจอ  ????
        '        .PrevieNoSplash(POState)

        '        Dim _FIleExportPDFName As String = .ExportFileSuccessName

        '        If System.IO.File.Exists(Application.StartupPath & "\PO PDF\" & Temp_FTPurchaseBy & "\" & _FIleExportPDFName) Then
        '            Return True
        '        Else
        '            Return False
        '        End If

        '    End With
        'Catch ex As Exception
        '    Return False
        'End Try

        'Return False
        Return True
    End Function

#End Region

End Class
