Imports System.Data.SqlClient
Imports System.IO
Imports System.Text

Imports System.Windows.Forms
Imports Microsoft.Office.Interop.Excel

Public Class wHRReportExportTax1_LA

    Private _LstReport As HI.RP.ListReport
    Private _FileName As String = ""
    Sub New(Optional _SysFormName As String = "")

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If _SysFormName <> "" Then
            Me.Name = _SysFormName
        End If

        Condition.PrePareData()

        _LstReport = New HI.RP.ListReport(Me.Name.ToString)
        FNReportname.Properties.Items.AddRange(_LstReport.GetList)

        If FNReportname.Properties.Items.Count = 1 Then
            ogbreportname.Visible = False
            Me.Height = Me.Height - ogbreportname.Height
        End If

    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmpreview.Click
        Dim _ExFormular As String = ""
        Dim _Formular As String = ""
        Dim _pdt As DataTable
        Dim _Str As String = ""

        If Me.FTPayYear.Text = "" Then
            HI.MG.ShowMsg.mProcessError(1005210001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        End If

        _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        _Formular &= " {THRTPayRoll.FTPayYear}='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "' "

        _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        _Formular &= " {THRMCfgPayDT.FNMonth}=" & (FNMonth.SelectedIndex + 1) & " "


        Dim tText As String = ""
        tText = Condition.GetCriteria

        If tText <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= "" & tText
        End If

        Select Case FNReportname.SelectedIndex


            Case 1

                ''Excel File Laos 


                Dim Op As New System.Windows.Forms.SaveFileDialog
                Op.Filter = "Excel Worksheets(2010-2013)" & "|*.xlsx|Excel Worksheets(97-2003)|*.xls"

                Op.ShowDialog()
                Try
                    If Op.FileName <> "" Then
                        _FileName = Op.FileName.ToString
                        'ExportExcel()
                        Dim _Spls As New HI.TL.SplashScreen("Prepre Data For Calculate.. Please Wait ")

                        Call ExportExCel(_Spls)


                    End If
                Catch ex As Exception
                End Try






            Case Else
                Dim _AllReportName As String = _LstReport.GetValue(FNReportname.SelectedIndex)

                If _AllReportName <> "" Then

                    Call HI.ST.Security.CreateTempEmpMaster(Condition)
                    Call HI.ST.Security.CreateTempPayroll(Condition, FTPayYear.Text)

                    If _LstReport.GetValueGenPic(FNReportname.SelectedIndex) = "1" Then
                        Call HI.HRCAL.GenTempData.GenerateEmpPicture(Condition)
                    End If

                    For Each _ReportName As String In _AllReportName.Split(",")
                        With New HI.RP.Report
                            'If ((HI.ST.SysInfo.Admin)) Then
                            '    .AddParameter("FNStateSalary", "1")
                            'Else
                            '    _Str = "Select U.FNHSysPermissionID ,T.FNHSysEmpTypeId ,T.FTStateSalary"
                            '    _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS U With(NOLOCK) LEFT OUTER JOIN"
                            '    _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS T With(NOLOCK) ON U.FNHSysPermissionID = T.FNHSysPermissionID"
                            '    _Str &= vbCrLf & " WHERE FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            '    _Str &= vbCrLf & " ORDER BY FTStateSalary DESC"
                            '    _pdt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)
                            '    For Each pR As DataRow In _pdt.Rows
                            '        If (((pR!FNHSysEmpTypeId.ToString) = "1306010002") Or ((pR!FNHSysEmpTypeId.ToString) = "1758590001")) Then
                            '            .AddParameter("FNStateSalary", pR!FTStateSalary.ToString)
                            '            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                            '            _Formular &= " {THRMEmpType.FTEmpTypeCode} <> 'M' AND {THRMEmpType.FTEmpTypeCode} <> 'N'"
                            '            Exit For
                            '        Else
                            '            .AddParameter("FNStateSalary", "1")
                            '        End If
                            '    Next
                            'End If

                            .FormTitle = Me.Text
                            .ReportFolderName = _LstReport.GetFolderReportValue(FNReportname.SelectedIndex)
                            .Formular = _Formular
                            .ReportName = _ReportName
                            .Preview()
                        End With
                    Next
                Else
                    HI.MG.ShowMsg.mProcessError(1005170001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                End If

        End Select

    End Sub


    Private Sub ExportExCel(_Spls As HI.TL.SplashScreen)
        Try
            Dim tText As String = ""
            Dim _Cmd As String = ""




            Dim _Qry As String = ""
            Dim _Formular As String = ""
            Dim _Year As String = ""

            _Year = FTPayYear.Text
            Dim _Month As String
            _Month = Microsoft.VisualBasic.Right("0000" & HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex), 2)






            Dim _ComName As String = ""
            Dim _ComID As String = ""
            Dim _ComTaxID As String = ""
            Dim _ComBnkBranchID As String = ""
            Dim _ComAcc As String = ""
            Dim _GAmount As Double = 0
            Dim _TotalRec As Integer = 0

            Dim FTYearThai As String = Format(Val(_Year) + 543, "0000")

            Dim _PayDateforTransfer As String = ""

            '_Qry = " SELECT TOP 1 FNHSysCmpId, FNHSysCmpTitleId, FTCmpCode, FTCmpNameTH, FTCmpNameEN, "
            '_Qry &= vbCrLf & " FTAddr1TH, FTAddr2TH, FTSubDistrictTH, FTDistrictTH, FTProvinceTH, FTAddr1EN, FTAddr2EN, FTSubDistrictEN, FTDistrictEN, FTProvinceEN, FTPostCode, FTPhone,"
            '_Qry &= vbCrLf & "  FTFax, FTMobile, FTMail, FTWebSite, FTNote, FTTaxNo, FTSocialNo, FNHSysBankId, FTBankBranchCode, FTDepositCode, FTBnkAccNo, FTBnkAccName,"
            '_Qry &= vbCrLf & "  FTBchSocial, FTBchTax, FPImageCmpLogo, FTDocRun, FTStateActive"
            '_Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH (NOLOCK) "
            '_Qry &= vbCrLf & " WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "
            'Dim _Dt As DataTable
            '_Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

            'For Each R As DataRow In _Dt.Rows
            '    _ComName = R!FTCmpNameTH.ToString
            '    _ComID = R!FTDepositCode.ToString
            '    _ComAcc = R!FTBnkAccNo.ToString.Replace("-", "")
            '    _ComTaxID = R!FTTaxNo.ToString
            '    _ComBnkBranchID = ""
            'Next
            _PayDateforTransfer = Format(Val(_Year), "0000") & "/" & Format(Val(_Month), "00") & "/" & "01"

            _Qry = "select CONVERT(varchar,dateadd(d,-(day(dateadd(m,1,'" & _PayDateforTransfer & "'))),dateadd(m,1,'" & _PayDateforTransfer & "')),112) AS 'paydate'"
            _PayDateforTransfer = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR)

            Dim pay30day_flag As String
            _Qry = " SELECT ISNULL(FTCfgData, 0) AS FTCfgData FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig WHERE  (FTCfgName = 'Pay30day_Flag') "
            pay30day_flag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")

            _Qry = "  	SELECT    ROW_NUMBER() OVER(ORDER BY P.FTEmpIdNo) AS FROMTYPE, '0000000000000' AS ComIDNO,'" & _ComTaxID & "' AS COMTaxNo, '" & _ComBnkBranchID & "' AS Tax_BranchNo,"
            _Qry &= vbCrLf & "   P.FTEmpIdNo , '0000000000' AS FTTaxNo, PP.FTPreNameNameTH AS FTEmpPreCode, M.FTEmpNameTH, M.FTEmpSurnameTH,"
            _Qry &= vbCrLf & "   (ISNULL(M.FTAddrNo,'')+ISNULL(M.FTAddrHome,'')+ISNULL(M.FTAddrMoo,'')+ISNULL(M.FTAddrSoi,'')+ISNULL(M.FTAddrRoad,'')+ISNULL(M.FTAddrTumbol,'')+ISNULL(M.FTAddrAmphur,'')+ISNULL(M.FTAddrProvince,'')) AS EmpAddress,"
            _Qry &= vbCrLf & "   M. FTAddrPostCode,'" & Format(Val(_Month), "00") & "'  AS MonthPay,'" & FTYearThai & "'  AS YearPay, '1' AS IncomCode, '" & _PayDateforTransfer & "' AS PayDate, '0' AS TaxRate,"
            _Qry &= vbCrLf & "   CAST(ROUND(SUM(P.FNTotalRecalTAX),0) as decimal(18,0)) AS TOTALPAY"
            _Qry &= vbCrLf & "   ,CAST(ROUND(SUM(P.FNTax),0) as decimal(18,0)) AS SUMTAX, '1' AS TaxCondition"

            _Qry &= vbCrLf & "   FROM   (((THRTPayRoll P "
            _Qry &= vbCrLf & "    LEFT OUTER JOIN THRMCfgPayDT PD ON ((P.FTPayYear=PD.FTPayYear)  "
            _Qry &= vbCrLf & "    AND (P.FTPayTerm=PD.FTPayTerm)) AND (P.FNHSysEmpTypeId=PD.FNHSysEmpTypeId)) "
            _Qry &= vbCrLf & "   INNER JOIN THRMEmployee M ON P.FNHSysEmpID=M.FNHSysEmpID) "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN V_MPrename PP ON M.FNHSysPreNameId=PP.FNHSysPreNameId) "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN V_MCmp V_MCmp ON M.FNHSysCmpId=V_MCmp.FNHSysCmpId "


            If pay30day_flag = "1" Then
                _Qry &= vbCrLf & "  LEFT JOIN HITECH_MASTER.dbo.THRMEmpType ET ON M.FNHSysEmpTypeId=ET.FNHSysEmpTypeId "
                _Qry &= vbCrLf & "  WHERE    P.FTPayYear='" & _Year & "' AND PD.FNMonth ='" & _Month & "' "
                _Qry &= vbCrLf & " AND M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "
                _Qry &= vbCrLf & " AND (ET.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID) & " AND FTEmpTypeCode not in ('M','N','O', 'M1','N1','O1', 'M2','N2', 'M3','N3'))"


                _Qry &= vbCrLf & " AND (ET.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID) & " )"
            Else
                _Qry &= vbCrLf & "  WHERE    P.FTPayYear='" & _Year & "' AND PD.FNMonth ='" & _Month & "' "
                _Qry &= vbCrLf & " AND M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "

            End If
            '_Qry &= vbCrLf & "  WHERE    P.FTPayYear='" & _Year & "' AND PD.FNMonth ='" & _Month & "' "
            '_Qry &= vbCrLf & " AND M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "


            _Qry &= vbCrLf & "  GROUP BY PP.FTPreNameNameTH, V_MCmp.FTTaxNo, PD.FNMonth
                                     , PD.FTPayYear, P.FTEmpIdNo, M.FTEmpNameTH, M.FTEmpSurnameTH
                                     , V_MCmp.FTBchTax
                                     , M.FTTaxNo, P.FTPayYear
                                     ,ISNULL(M.FTAddrNo,''),ISNULL(M.FTAddrHome,''),ISNULL(M.FTAddrMoo,''),ISNULL(M.FTAddrSoi,''),ISNULL(M.FTAddrRoad,''),ISNULL(M.FTAddrTumbol,''),ISNULL(M.FTAddrAmphur,''),ISNULL(M.FTAddrProvince,'')
                                     ,M. FTAddrPostCode "

            _Qry &= vbCrLf & " HAVING  SUM(P.FNTotalRecalTAX) > 1300000  "


            Dim _oDt As System.Data.DataTable
            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


            'Call NewExcel(_oDt, _Spls)
            Call NewExcelNew_TAX_LAForm(_oDt, _Spls)
        Catch ex As Exception
            'MsgBox("Error Step1" & ex.Message.ToString)
        End Try
    End Sub

    Private Sub NewExcelNew_TAX_LAForm(ByVal _oDt As System.Data.DataTable, _Spls As HI.TL.SplashScreen)
        Try

            Dim _Qry As String = ""
            Dim _DateNow As String = HI.Conn.SQLConn.GetField(HI.UL.ULDate.FormatDateDB, Conn.DB.DataBaseName.DB_HR, "")
            Dim _Rec As Integer = 0
            Dim _RecError As Integer = 0
            Dim _TotalRec As Integer = 0
            Dim _l As Integer = 0
            Dim _SumPQtyDay As Double = 0
            Dim _SumUQtyDay As Double = 0
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim _Path As String = System.Windows.Forms.Application.StartupPath.ToString
            Dim TmpFile As String = _Path & "\Reports\TAX_LA.xlsx"
            Dim oExcel As New Microsoft.Office.Interop.Excel.Application
            Dim xlBookTmp As Workbook
            Dim Excel As New Microsoft.Office.Interop.Excel.XlDirection

            xlBookTmp = oExcel.Workbooks.Open(TmpFile)

            Dim _date As String = HI.Conn.SQLConn.GetField("Select convert(varchar(10) ,  getdate() ,103) as Date", Conn.DB.DataBaseName.DB_HR)
            Dim i As Integer = 6
            Dim s As Integer = 13

            Dim _FNTOTALPAY As String = Format(_oDt.Compute("SUM(TOTALPAY)", "TOTALPAY>0"), "##,###").ToString
            Dim _FNSUMTAX As String = Format(_oDt.Compute("SUM(SUMTAX)", "SUMTAX>0"), "##,###").ToString


            Dim _N_Count As Integer = 0

            _N_Count = Val(_oDt.Rows.Count)

            Dim _Year As String = ""

            _Year = FTPayYear.Text
            Dim _Month As String
            _Month = Microsoft.VisualBasic.Right("0000" & HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex), 2)



            'Dim _FNSocial As String = Format(_oDt.Compute("SUM(FNSocial)", "FNSocial>0"), "##,###").ToString
            'Dim _FNSocialCmp As String = Format(_oDt.Compute("SUM(FNSocialCmp)", "FNSocialCmp>0"), "##,###").ToString

            'Dim _nettotalKip As String = HI.UL.ULF.Convert_Bath_LA(_oDt.Compute("SUM(FNNetpay)", "FNNetpay>0"))
            With xlBookTmp.Worksheets(1)
                .Cells(5, 1) = "ຕາຕະລາງເງິນເດືອນພື້ນຖານ ແລະ ຜົນປະໂຫຍດຢ່າງອື່ນ ປະຈຳເດືອນ  " & _Month & " / ປີ " & _Year & ""

            End With

            For Each R As DataRow In _oDt.Rows
                i += +1


                With xlBookTmp.Worksheets(1)
                    'Date
                    .Rows(CStr(i) & ":" & CStr(i)).Insert(Shift:=XlDirection.xlDown)
                    .Cells(i, 1).Font.Color = 0
                    .Cells(i, 1) = "" & R!FROMTYPE.ToString
                    .Cells(i, 2).Font.Color = 0
                    .Cells(i, 2).HorizontalAlignment = XlHAlign.xlHAlignLeft
                    .Cells(i, 2) = "" & R!FTEmpPreCode.ToString & " " & R!FTEmpNameTH.ToString & " " & R!FTEmpSurnameTH.ToString
                    .Cells(i, 3).Font.Color = 0
                    .Cells(i, 3).HorizontalAlignment = XlHAlign.xlHAlignRight
                    .Cells(i, 3) = "" & R!TOTALPAY.ToString
                    .Cells(i, 3).NumberFormat = "#,###,###"
                    '.Cells(i, 4).Font.Color = 0
                    '.Cells(i, 4) = "'" & R!FTEmpIdNo.ToString


                    '.Cells(i, 4).NumberFormat = "#,###,###"
                End With


                'With xlBookTmp.Worksheets(2)
                '    'Date
                '    .Rows(CStr(s) & ":" & CStr(s)).Insert(Shift:=XlDirection.xlDown)
                '    .Cells(s, 1).Font.Color = 0
                '    .Cells(s, 1) = "" & R!FNSeq.ToString
                '    .Cells(s, 2).Font.Color = 0
                '    .Cells(s, 2) = "" & R!Name.ToString
                '    .Cells(s, 2).HorizontalAlignment = XlHAlign.xlHAlignLeft
                '    .Cells(s, 3).Font.Color = 0
                '    .Cells(s, 3) = "" & R!FTAccNo.ToString
                '    '.Cells(i, 4).Font.Color = 0
                '    '.Cells(i, 4) = "'" & R!FTEmpIdNo.ToString
                '    .Cells(s, 4).Font.Color = 0
                '    .Cells(s, 4) = R!FNNetpay.ToString
                '    .Cells(s, 4).HorizontalAlignment = XlHAlign.xlHAlignRight
                '    .Cells(s, 4).NumberFormat = "#,###,###"
                'End With



            Next
            i += +1

            With xlBookTmp.Worksheets(1)
                .Cells(i, 2).Font.Color = 0
                .Cells(i, 2) = "ລວມ"

                .Cells(i, 3).Font.Color = 0
                .Cells(i, 3).HorizontalAlignment = XlHAlign.xlHAlignRight
                .Cells(i, 3) = _FNTOTALPAY.ToString
            End With

            i += +2

            With xlBookTmp.Worksheets(1)
                .Cells(i, 4).Font.Color = 0
                .Cells(i, 4) = "ວຽງຈັນ,ວັນທີ.................................."

            End With

            i += +2

            With xlBookTmp.Worksheets(1)
                .Cells(i, 2).Font.Color = 0
                .Cells(i, 2) = "ລາຍເຊັນ ແລະ ຊື່ແຈ້ງຜູ້ອຳນວຍການ"

                .Cells(i, 4).Font.Color = 0
                .Cells(i, 4) = "ລາຍເຊັນ ແລະ ຊື່ແຈ້ງຜູ້ແຈ້ງເສຍອາກອນ"
            End With


            i += +4

            With xlBookTmp.Worksheets(1)
                .Cells(i, 4).Font.Color = 0
                .Cells(i, 4) = "ທ. ວິໄລທອງ ສີປະເສີດ"
            End With




            '''   ปะหน้า

            With xlBookTmp.Worksheets(2)
                .Cells(12, 2) = "ປະຈໍາເດືອນ  " & _Month & " / ປີ " & _Year & ""

            End With

            With xlBookTmp.Worksheets(2)
                .Cells(i, 4).Font.Color = 0
                .Cells(17, 4) = _N_Count.ToString

            End With

            With xlBookTmp.Worksheets(2)
                .Cells(i, 4).Font.Color = 0
                .Cells(18, 4) = _FNTOTALPAY.ToString

            End With




            If Val(_Month) + 1 = 13 Then
                _Year = _Year + 1
            End If
            Dim _date_line As String = "ກຳນົດມອບອາກອນບໍ່ໃຫເກາຍວັນທີ: 20/" & Val(_Month) + 1 & "/" & _Year

            With xlBookTmp.Worksheets(2)
                .Cells(i, 4).Font.Color = 0
                .Cells(21, 4) = _FNSUMTAX.ToString



                .Cells(30, 2).Font.Color = 0
                .Cells(30, 2) = _date_line

            End With

            Try
                CType(oExcel.Application.ActiveWorkbook.Sheets(1), Worksheet).Select()
            Catch ex As Exception
            End Try

            oExcel.DisplayAlerts = False
            '_FileName = "C:\Users\NOH-NB\Desktop\TestFile.xlsx"

            xlBookTmp.SaveAs(_FileName)
            xlBookTmp.Close()
            _Spls.Close()
            Process.Start(_FileName)
        Catch ex As Exception
            _Spls.Close()
            HI.MG.ShowMsg.mProcessError(1505029917, "Writing Excel File Error....." & vbCrLf & ex.Message.ToString, Me.Text, MessageBoxIcon.Error)
        End Try
    End Sub




    Private Sub wReportHRByPayRollByYear_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If FNReportname.Properties.Items.Count < 0 Then
                MsgBox("ไม่พบการกำหนด File Report !!!")
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class