Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.Office.Interop.Excel

Public Class wHRReportSocial_LA

    Private _LstReport As HI.RP.ListReport
    Private _FileName As String = ""
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Me.Name = _SysFormName
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
        Dim _Formular As String = ""
        Dim _pdt As DataTable
        Dim _Str As String = ""

        If Me.FTPayYear.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPayYear_lbl.Text)
            FTPayYear.Focus()
            Exit Sub
        End If

        If Me.FNMonth.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNMonth_lbl.Text)
            FNMonth.Focus()
            Exit Sub
        End If



        Select Case FNReportname.SelectedIndex
            'Case 1


            '    _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            '    _Formular &= " P.FTPayYear='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "' "

            '    _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            '    _Formular &= " D.FNMonth=" & HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex) & " "

            '    Dim tText As String = ""
            '    tText = Condition.GetCriteria

            '    tText = tText.Replace("{THRMEmployee.FTEmpCode}", "EM.FTEmpCode").Replace("[", "(").Replace("]", ")")


            '    If tText <> "" Then
            '        _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            '        _Formular &= "" & tText

            '        tText = " AND " & tText
            '    End If


            '    Dim _Dt As DataTable = HI.HRCAL.Social.CreateTextFileSSO("", "", Me.FTPayYear.Text, Microsoft.VisualBasic.Right("0000" & HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex), 2), Me.FTSendSSODate.Text, tText)






            '    If Not (_Dt Is Nothing) Then
            '        If _Dt.Rows.Count > 0 Then
            '            Dim _strBuilder As New StringBuilder()

            '            For Each R As DataRow In _Dt.Rows
            '                For Each C As DataColumn In _Dt.Columns
            '                    If Not (IsDBNull(R.Item(C))) Then
            '                        _strBuilder.Append(R.Item(C).ToString())
            '                    End If
            '                Next
            '                _strBuilder.AppendLine()
            '            Next

            '            Dim Op As New System.Windows.Forms.SaveFileDialog
            '            Op.Filter = "Text Files(*.DAT)|*.DAT"
            '            Op.FileName = "SSOSENT.DAT"
            '            Op.ShowDialog()

            '            Try
            '                If Op.FileName <> "" Then
            '                    ' File.WriteAllText(Op.FileName, "", Encoding.ASCII)
            '                    Dim myWriter As New IO.StreamWriter(Op.FileName, True, System.Text.Encoding.Default)
            '                    myWriter.WriteLine(_strBuilder.ToString())
            '                    myWriter.Close()
            '                    HI.MG.ShowMsg.mInfo("", 1005280003, Me.Text)

            '                End If
            '            Catch ex As Exception ': MsgBox(ex.Message)
            '            End Try
            '        Else
            '            HI.MG.ShowMsg.mInvalidData("", 1005280002, Me.Text)
            '        End If
            '    Else
            '        HI.MG.ShowMsg.mInvalidData("", 1005280002, Me.Text)
            '    End If

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










                'If Not (_Dt Is Nothing) Then
                '    If _Dt.Rows.Count > 0 Then
                '        Dim _strBuilder As New StringBuilder()

                '        For Each R As DataRow In _Dt.Rows
                '            For Each C As DataColumn In _Dt.Columns
                '                If Not (IsDBNull(R.Item(C))) Then
                '                    _strBuilder.Append(R.Item(C).ToString())
                '                End If
                '            Next
                '            _strBuilder.AppendLine()
                '        Next

                '        Dim Op As New System.Windows.Forms.SaveFileDialog
                '        Op.Filter = "Text Files(*.DAT)|*.DAT"
                '        Op.FileName = "SSOSENT.DAT"
                '        Op.ShowDialog()

                '        Try
                '            If Op.FileName <> "" Then
                '                ' File.WriteAllText(Op.FileName, "", Encoding.ASCII)
                '                Dim myWriter As New IO.StreamWriter(Op.FileName, True, System.Text.Encoding.Default)
                '                myWriter.WriteLine(_strBuilder.ToString())
                '                myWriter.Close()
                '                HI.MG.ShowMsg.mInfo("", 1005280003, Me.Text)

                '            End If
                '        Catch ex As Exception ': MsgBox(ex.Message)
                '        End Try
                '    Else
                '        HI.MG.ShowMsg.mInvalidData("", 1005280002, Me.Text)
                '    End If
                'Else
                '    HI.MG.ShowMsg.mInvalidData("", 1005280002, Me.Text)
                'End If

            Case Else


                Dim _AllReportName As String = _LstReport.GetValue(FNReportname.SelectedIndex)

                If _AllReportName <> "" Then
                    '' Call HI.ST.Security.CreateTempEmpMaster(Condition)
                    ''  Call HI.ST.Security.CreateTempPayroll(Condition, FTPayYear.Text)
                    If _LstReport.GetValueGenPic(FNReportname.SelectedIndex) = "1" Then
                        Call HI.HRCAL.GenTempData.GenerateEmpPicture(Condition)
                    End If



                    '_Formular = IIf(_Formular.Trim <> "", " AND ", "")
                    '_Formular &= " {THRTPayRoll.FTPayYear}='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "' "

                    '_Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                    '_Formular &= " {THRMCfgPayDT.FNMonth}=" & HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex) & " "

                    '_Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                    '_Formular &= " {THRMEmployee.FNHSysCmpId}=" & Val(HI.ST.SysInfo.CmpID) & " "

                    _Formular = IIf(_Formular.Trim <> "", " AND ", "")
                    _Formular &= " {THRTPayRoll.FTPayYear}='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "' "

                    '_Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                    '_Formular &= " {THRMCfgPayDT.FNMonth}=" & HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex) & " "

                    _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                    _Formular &= " {THRMEmployee.FNHSysCmpId}=" & Val(HI.ST.SysInfo.CmpID) & " "

                    _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                    _Formular &= " {THRTPayRoll.FNMonth}=" & HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex) & " "

                    _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                    _Formular &= " {THRTPayRoll.FNHSysCmpId}=" & Val(HI.ST.SysInfo.CmpID) & " "

                    Dim tText As String = ""
                    tText = Condition.GetCriteria

                    If tText <> "" Then
                        _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                        _Formular &= "" & tText
                    End If

                    Dim _date As String = ""
                    Dim _month As String = ""
                    Dim _year As String = ""

                    If Me.FTSendSSODate.Text <> "" Then

                        _date = Me.FTSendSSODate.Text.Substring(0, 2)
                        _month = Me.FTSendSSODate.Text.Substring(3, 2)
                        _year = Me.FTSendSSODate.Text.Substring(6, 4)

                        Select Case _month
                            Case 1
                                _month = "มกราคม"
                            Case 2
                                _month = "กุมภาพันธ์"
                            Case 3
                                _month = "มีนาคม"
                            Case 4
                                _month = "เมษายน"
                            Case 5
                                _month = "พฤษภาคม"
                            Case 6
                                _month = "มิถุนายน"
                            Case 7
                                _month = "กรกฎาคม"
                            Case 8
                                _month = "สิงหาคม"
                            Case 9
                                _month = "กันยายน"
                            Case 10
                                _month = "ตุลาคม"
                            Case 11
                                _month = "พฤษจิกายน"
                            Case 12
                                _month = "ธันวาคม"
                        End Select
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
                            '            _Formular &= " {THRMEmployee.FNHSysEmpTypeId} <> 1306010002 AND {THRMEmployee.FNHSysEmpTypeId} <> 1758590001"
                            '            Exit For
                            '        Else
                            '            .AddParameter("FNStateSalary", "1")
                            '        End If
                            '    Next
                            'End If

                            .FormTitle = Me.Text
                            .ReportFolderName = _LstReport.GetFolderReportValue(FNReportname.SelectedIndex)
                            .Formular = _Formular
                            .AddParameter("@paramDate", _date)
                            .AddParameter("@paramMonth", _month)
                            .AddParameter("@paramYear", _year)
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
            Dim _Month As Integer
            _Month = Microsoft.VisualBasic.Right("0000" & HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex), 2)

            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " P.FTPayYear='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "' "

            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " D.FNMonth=" & HI.TL.CboList.GetListValue(FNMonth.Properties.Tag.ToString, FNMonth.SelectedIndex) & " "

            ''Dim'' tText As String = ""
            tText = Condition.GetCriteria

            tText = tText.Replace("{THRMEmployee.FTEmpCode}", "EM.FTEmpCode").Replace("[", "(").Replace("]", ")")


            If tText <> "" Then
                _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                _Formular &= "" & tText

                tText = " AND " & tText
            End If

            _Qry = "   SELECT  ROW_NUMBER() over(order by  FTSocialNo asc) AS FNSeq , FTPayYear , FNMonth, FTEmpIdNo"
            _Qry &= vbCrLf & " ,FNTotalRecalSSO, FNTotalRecalSSO_Base, FNSocial, FNSocialCmp "
            _Qry &= vbCrLf & " , FTPreNameCode, FTEmpNameEN, FTEmpSurnameEN,FTEmpNicknameEN "
            _Qry &= vbCrLf & " , FTEmpNameTH, FTEmpSurnameTH, FTSocialNo, FTSectCode , FTEmpCode "
            _Qry &= vbCrLf & "  FROM V_PayrollSSO_LA P"

            _Qry &= vbCrLf & "  WHERE        (P.FTPayYear ='" & Format(IIf(Integer.Parse(_Year) > 2500, Integer.Parse(_Year) - 543, Integer.Parse(_Year)), "0000") & "')"
            _Qry &= vbCrLf & "   AND (P.FNMonth =" & Val(_Month) & " ) "
            _Qry &= vbCrLf & tText



            'If _EmpType <> "" Then
            '    _Qry &= vbCrLf & "   AND P.FNHSysEmpTypeID=" & Integer.Parse(Val(_EmpType)) & "  "
            'End If

            _Qry &= vbCrLf & " AND P.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "





            Dim _oDt As System.Data.DataTable
            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


            'Call NewExcel(_oDt, _Spls)
            Call NewExcelNew_SSOForm(_oDt, _Spls)
        Catch ex As Exception
            'MsgBox("Error Step1" & ex.Message.ToString)
        End Try
    End Sub

    Private Sub NewExcelNew_SSOForm(ByVal _oDt As System.Data.DataTable, _Spls As HI.TL.SplashScreen)
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
            Dim TmpFile As String = _Path & "\Reports\SSOLA_Detail.xlsx"
            Dim oExcel As New Microsoft.Office.Interop.Excel.Application
            Dim xlBookTmp As Workbook
            Dim Excel As New Microsoft.Office.Interop.Excel.XlDirection

            xlBookTmp = oExcel.Workbooks.Open(TmpFile)

            Dim _date As String = HI.Conn.SQLConn.GetField("Select convert(varchar(10) ,  getdate() ,103) as Date", Conn.DB.DataBaseName.DB_HR)
            Dim i As Integer = 10
            Dim s As Integer = 13
            Dim _FNTotalRecalSSO As String = Format(_oDt.Compute("SUM(FNTotalRecalSSO)", "FNTotalRecalSSO>0"), "##,###").ToString
            Dim _FNTotalRecalSSO_Base As String = Format(_oDt.Compute("SUM(FNTotalRecalSSO_Base)", "FNTotalRecalSSO_Base>0"), "##,###").ToString
            Dim _FNSocial As String = Format(_oDt.Compute("SUM(FNSocial)", "FNSocial>0"), "##,###").ToString
            Dim _FNSocialCmp As String = Format(_oDt.Compute("SUM(FNSocialCmp)", "FNSocialCmp>0"), "##,###").ToString

            'Dim _nettotalKip As String = HI.UL.ULF.Convert_Bath_LA(_oDt.Compute("SUM(FNNetpay)", "FNNetpay>0"))
            'With xlBookTmp.Worksheets(2)
            '    .Cells(11, 1) = "ຊື່ບັນຊີ ໄຮ-ເທັກລາວ ແອບພາເຣວ ຈຳກັດ.  ເລກບັນຊີ  =>   0302000010004449  ຈຳນວນເງິນ    " & _nettotal & " ກີບ."
            '    .Cells(12, 1) = "(" & _nettotalKip & " ) ເພື່ອໂອນເຂົ້າບັນຊີຂອງພະນັກງານຕາມລາຍລະອຽດດັ່ງລຸ່ມນີ້:"
            '    .Cells(5, 4) = "ວັນທີ : " & _date
            'End With

            For Each R As DataRow In _oDt.Rows
                i += +1
                s += +1

                With xlBookTmp.Worksheets(1)
                    'Date
                    .Rows(CStr(i) & ":" & CStr(i)).Insert(Shift:=XlDirection.xlDown)
                    .Cells(i, 1).Font.Color = 0
                    .Cells(i, 1) = "" & R!FNSeq.ToString
                    .Cells(i, 2).Font.Color = 0
                    .Cells(i, 2).HorizontalAlignment = XlHAlign.xlHAlignLeft
                    .Cells(i, 2) = "" & R!FTSocialNo.ToString
                    .Cells(i, 3).Font.Color = 0
                    .Cells(i, 3) = "" & R!FTPreNameCode.ToString & " " & R!FTEmpNameTH.ToString & " " & R!FTEmpSurnameTH.ToString
                    '.Cells(i, 4).Font.Color = 0
                    '.Cells(i, 4) = "'" & R!FTEmpIdNo.ToString
                    .Cells(i, 4).Font.Color = 0
                    .Cells(i, 4) = R!FTEmpCode.ToString

                    .Cells(i, 5).Font.Color = 0
                    .Cells(i, 5) = R!FTSectCode.ToString

                    .Cells(i, 6).Font.Color = 0
                    .Cells(i, 6) = R!FNTotalRecalSSO.ToString

                    .Cells(i, 7).Font.Color = 0
                    .Cells(i, 7) = R!FNTotalRecalSSO_Base.ToString

                    .Cells(i, 8).Font.Color = 0
                    .Cells(i, 8) = R!FNSocialCmp.ToString

                    .Cells(i, 9).Font.Color = 0
                    .Cells(i, 9) = R!FNSocial.ToString


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
            i += +2
            s += +2
            With xlBookTmp.Worksheets(1)
                .Cells(i, 4).Font.Color = 0
                .Cells(i, 4) = "Total"


                .Cells(i, 6).Font.Color = 0
                .Cells(i, 6) = _FNTotalRecalSSO.ToString

                .Cells(i, 7).Font.Color = 0
                .Cells(i, 7) = _FNTotalRecalSSO_Base.ToString

                .Cells(i, 8).Font.Color = 0
                .Cells(i, 8) = _FNSocialCmp.ToString

                .Cells(i, 9).Font.Color = 0
                .Cells(i, 9) = _FNSocial.ToString
            End With



            i += +4
            With xlBookTmp.Worksheets(1)
                .Cells(i, 2) = "certify that tha above information is true and correct. I am responsible for any wrong information provided"

            End With


            i += +4
            With xlBookTmp.Worksheets(1)
                .Cells(i, 2) = "Factory Manager"
                .Cells(i, 4) = "Head of Account"
                .Cells(i, 7) = "Prepared by"
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




    Private Sub wReportSSO_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If FNReportname.Properties.Items.Count < 0 Then
                MsgBox("ไม่พบการกำหนด File Report !!!")
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class