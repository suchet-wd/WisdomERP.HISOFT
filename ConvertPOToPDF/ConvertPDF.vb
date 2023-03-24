
Imports System.IO

Public Class ConvertPDF
    Private Sub ConvertPDF_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Application.Exit()
    End Sub

    Private Sub ConvertPDF_Load(sender As Object, e As EventArgs) Handles Me.Load

        olbtotal.Text = ""
        olbcurrent.Text = ""
        olbpono.Text = ""
        olbBy.Text = ""

        Call CheckPOToConvertToPDF()

    End Sub

    Private Sub CheckPOToConvertToPDF()
        otmcheckpo.Enabled = False

        Dim cmdstring As String = ""
        Dim dtpdf As New DataTable
        Dim TotalPo As Integer = 0
        Dim CountPo As Integer = 0
        Dim PoNo As String = ""
        Dim PurchaseBy As String = ""
        Dim PoState As Integer = 0

        cmdstring = ""
        cmdstring = " SELECT A.FTPurchaseNo,A.FTPurchaseBy,A.FNPoState,A.FTPOTypeState  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.V_TPURTPurchase AS A "
        cmdstring &= Environment.NewLine & " WHERE (FTStateSendApp = '1') AND (FTStateSuperVisorApp = '1') "
        cmdstring &= Environment.NewLine & " AND (FTStateManagerApp ='1')  AND (ISNULL(FTStatePDF,'0') <> '1') AND ISNULL(A.FTPurchaseBy,'') <> '' "
        dtpdf = HI.Conn.SQLConn.GetDataTable(cmdstring, HI.Conn.DB.DataBaseName.DB_PUR)

        TotalPo = dtpdf.Rows.Count

        If TotalPo > 0 Then
            olbtotal.Text = TotalPo.ToString

            For Each R As DataRow In dtpdf.Rows

                Application.DoEvents()

                CountPo = CountPo + 1
                PoNo = R!FTPurchaseNo.ToString
                PurchaseBy = R!FTPurchaseBy.ToString
                PoState = Val(R!FNPoState.ToString)

                olbcurrent.Text = CountPo.ToString
                olbpono.Text = PoNo
                olbBy.Text = PurchaseBy

                Select Case R!FTPOTypeState.ToString()
                    Case "2"
                        Call Update_Pusrchase_Service_PDF(PoNo, PurchaseBy, PoState)
                    Case "3"
                        Call Update_Pusrchase_Number2(PoNo, PurchaseBy, PoState)
                    Case "4"
                        Call Update_Pusrchase_Sample(PoNo, PurchaseBy, PoState)
                    Case Else
                        Call Update_Pusrchase_PDF(PoNo, PurchaseBy, PoState)
                End Select

            Next

        End If

        dtpdf.Dispose()


        olbpono.Text = "waiting... load data"
        olbBy.Text = ""

        otmcheckpo.Enabled = True

    End Sub

    Private Sub otmcheckpo_Tick(sender As Object, e As EventArgs) Handles otmcheckpo.Tick

        CheckPOToConvertToPDF()

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
                .ExportFile = HI.RP.Report.ExFile.PDF

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

                Try

                    _Str = ""
                    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                    _Str &= Environment.NewLine & "SET  [FTStatePDF] = '1'"
                    _Str &= Environment.NewLine & ", [FTPDFDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTPDFTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTPurchase] "
                    _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & Temp_FTPurchaseNo & "'"

                    HI.Conn.SQLConn.ExecuteOnly(_Str, HI.Conn.DB.DataBaseName.DB_PUR)


                Catch ex As Exception

                End Try

            End If

            ' MessageBox.Show("Tran.Commit")

            ' Return True

        Catch ex As Exception

            ' Return False
        End Try

    End Sub


    Public Shared Sub Update_Pusrchase_Service_PDF(ByVal Temp_FTPurchaseNo As String, ByVal Temp_FTPurchaseBy As String, ByVal POState As Integer) 'As Boolean

        Dim _Str As String = String.Empty

        Try

            If POServiseConvert_PDF(Temp_FTPurchaseNo, Temp_FTPurchaseBy, POState) Then

                Try

                    _Str = ""
                    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchaseService "
                    _Str &= Environment.NewLine & "SET  [FTStatePDF] = '1'"
                    _Str &= Environment.NewLine & ", [FTPDFDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTPDFTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTPurchaseService] "
                    _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & Temp_FTPurchaseNo & "'"

                    HI.Conn.SQLConn.ExecuteOnly(_Str, HI.Conn.DB.DataBaseName.DB_PUR)

                Catch ex As Exception
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
                .ExportFile = HI.RP.Report.ExFile.PDF

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


                Try

                    _Str = ""
                    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTFacPurchase "
                    _Str &= Environment.NewLine & "SET  [FTStatePDF] = '1'"
                    _Str &= Environment.NewLine & ", [FTPDFDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTPDFTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTFacPurchase] "
                    _Str &= Environment.NewLine & " WHERE FTFacPurchaseNo = '" & Temp_FTPurchaseNo & "'"

                    HI.Conn.SQLConn.ExecuteOnly(_Str, HI.Conn.DB.DataBaseName.DB_PUR)

                Catch ex As Exception


                End Try

            End If

        Catch ex As Exception

        End Try

    End Sub


    Public Shared Sub Update_Pusrchase_Sample(ByVal Temp_FTPurchaseNo As String, ByVal Temp_FTPurchaseBy As String, ByVal POState As Integer) 'As Boolean

        Dim _Str As String = String.Empty

        Try

            If POSampleConvert_PDF(Temp_FTPurchaseNo, Temp_FTPurchaseBy, POState) = True Then


                Try

                    _Str = ""
                    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPPurchase "
                    _Str &= Environment.NewLine & "SET  [FTStatePDF] = '1'"
                    _Str &= Environment.NewLine & ", [FTPDFDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTPDFTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].[dbo].[TSMPPurchase] "
                    _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & Temp_FTPurchaseNo & "'"

                    HI.Conn.SQLConn.ExecuteOnly(_Str, HI.Conn.DB.DataBaseName.DB_SAMPLE)

                Catch ex As Exception

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
                .ExportFile = HI.RP.Report.ExFile.PDF

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
        Return True
    End Function
End Class
