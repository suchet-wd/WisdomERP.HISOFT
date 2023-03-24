Imports Microsoft.Office.Interop.Excel
Imports System.Windows.Forms
Imports System.Text
Imports System.Drawing
Imports System


Public Class wHRReportExportBank_KM
    Private _LstReport As HI.RP.ListReport
    Private _CmpName As String = ""

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Me.Name = _SysFormName

        Condition.PrePareData()
        _LstReport = New HI.RP.ListReport(Me.Name.ToString)
        FNReportname.Properties.Items.AddRange(_LstReport.GetList)



        Dim _Str As String = ""
        _Str = " SELECT TOP 1 "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " FTCmpNameTH  "
        Else
            _Str &= vbCrLf & " FTCmpNameEN  "
        End If

        _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS H WITH(NOLOCK)   "
        _Str &= vbCrLf & " WHERE FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "

        _CmpName = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "")


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
        Dim _StateBank As Integer = 0
        If Me.FTPayTerm.Text = "" Or Me.FTPayYear.Text = "" Then
            HI.MG.ShowMsg.mProcessError(1005210001, "", Me.Text, MessageBoxIcon.Information)
            Exit Sub
        End If

        'If Me.FNHSysEmpTypeId.Text = "" Then
        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysEmpTypeId_lbl.Text)
        '    Exit Sub
        'End If

        If Me.FNHSysBankId.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysBankId_lbl.Text)
            FNHSysBankId.Focus()
            Exit Sub
        End If

        If Me.FNHSysBankBranchId.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysBankBranchId_lbl.Text)
            FNHSysBankBranchId.Focus()
            Exit Sub
        End If

        Select Case FNReportname.SelectedIndex
            Case 1
                'Export Excel Tobank

                Dim Op As New System.Windows.Forms.SaveFileDialog
                Op.Filter = "Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"

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
                _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                _Formular &= " {THRTPayRoll.FTPayYear}='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "' "

                _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                _Formular &= " {THRTPayRoll.FTPayTerm}='" & HI.UL.ULF.rpQuoted(Me.FTPayTerm.Text) & "' "

                _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                _Formular &= " {THRTPayRoll.FNHSysBankId}=" & Val(FNHSysBankId.Properties.Tag.ToString) & " "

                Dim tText As String = ""
                tText = Condition.GetCriteria

                If tText <> "" Then
                    _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                    _Formular &= "" & tText
                End If

                Dim _AllReportName As String = _LstReport.GetValue(FNReportname.SelectedIndex)

                If _AllReportName <> "" Then

                    Call HI.ST.Security.CreateTempEmpMaster(Condition)
                    Call HI.ST.Security.CreateTempPayroll(Condition, FTPayYear.Text, FTPayTerm.Text)

                    If _LstReport.GetValueGenPic(FNReportname.SelectedIndex) = "1" Then
                        Call HI.HRCAL.GenTempData.GenerateEmpPicture(Condition)
                    End If

                    For Each _ReportName As String In _AllReportName.Split(",")

                        With New HI.RP.Report
                            .FormTitle = Me.Text
                            .ReportFolderName = _LstReport.GetFolderReportValue(FNReportname.SelectedIndex)
                            .Formular = _Formular
                            .ReportName = _ReportName
                            .Preview()
                        End With

                    Next

                Else
                    HI.MG.ShowMsg.mProcessError(1005170001, "", Me.Text, MessageBoxIcon.Warning)
                End If

        End Select
    End Sub

    Private Sub wReportHRExportBank_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If FNReportname.Properties.Items.Count < 0 Then
                HI.MG.ShowMsg.mProcessError(1503240091, "ไม่พบการกำหนด File Report !!!", Me.Text, MessageBoxIcon.Information)
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub ExportExCel(_Spls As HI.TL.SplashScreen)
        Try
            Dim tText As String = ""
            Dim _Cmd As String = ""
            _Cmd = "SELECT    ROW_NUMBER() over(order by E.FNHSysEmpID asc) AS FNSeq,   E.FTEmpSurnameEN + ' ' + E.FTEmpNameEN AS Name, E.FTAccNo, E.FTEmpIdNo, R.FNNetpay, R.FTPayYear, R.FTPayTerm, R.FNHSysBankId, " 'P.FTPreNameNameEN + ' ' +
            _Cmd &= vbCrLf & "   E.FTEmpCode,   R.FNHSysBankBranchId, R.FNHSysEmpTypeId, R.FNHSysDeptId, R.FNHSysDivisonId, R.FNHSysSectId, R.FNHSysUnitSectId ,DT.FNMonth AS FTMonth --Right(LEFT(DT.FDCalDateBegin,7),2) AS FTMonth"
            _Cmd &= vbCrLf & ",DT.FDPayDate ,W.FNServicefee ,W.FNFinTransFee"
            _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS R LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS DT ON R.FTPayTerm = DT.FTPayTerm AND R.FTPayYear = DT.FTPayYear LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E ON R.FNHSysEmpID = E.FNHSysEmpID LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MEmpType AS T ON R.FNHSysEmpTypeId = T.FNHSysEmpTypeId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MDepartment AS D ON R.FNHSysDeptId = D.FNHSysDeptId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MDivision AS S ON R.FNHSysDivisonId = S.FNHSysDivisonId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MSect AS C ON R.FNHSysSectId = C.FNHSysSectId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MUnitSect AS F ON R.FNHSysUnitSectId = F.FNHSysUnitSectId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MPrename AS P ON E.FNHSysPreNameId = P.FNHSysPreNameId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MCmp AS N ON E.FNHSysCmpId = N.FNHSysCmpId"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgWelfareKM AS W WITH(NOLOCK) ON R.FNHSysEmpTypeId = W.FNHSysEmpTypeId "

            _Cmd &= vbCrLf & "WHERE R.FTPayYear='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "'"
            _Cmd &= vbCrLf & "  and R.FTPayTerm='" & HI.UL.ULF.rpQuoted(Me.FTPayTerm.Text) & "'"
            _Cmd &= vbCrLf & "  and R.FNHSysBankId=" & Integer.Parse(Val(Me.FNHSysBankId.Properties.Tag)) & ""
            '_Cmd &= vbCrLf & " and R.FNHSysBankBranchId=" & Integer.Parse(Val(Me.FNHSysBankBranchId.Properties.Tag)) & ""

            With Me.Condition
                If (.otpemptype.PageVisible) Then
                    Select Case .FNEmpTypeCon.SelectedIndex
                        Case 1
                            If .FNHSysEmpTypeId.Text <> "" Then
                                _Cmd &= vbCrLf & " and T.FTEmpTypeCode >='" & HI.UL.ULF.rpQuoted(.FNHSysEmpTypeId.Text) & "' "
                            End If

                            If .FNHSysEmpTypeIdTo.Text <> "" Then
                                _Cmd &= vbCrLf & " and T.FTEmpTypeCode <='" & HI.UL.ULF.rpQuoted(.FNHSysEmpTypeIdTo.Text) & "' "
                            End If
                        Case 2
                            tText = ""
                            For Each oRow As DataRow In .DbDtEmployeeType.Rows
                                tText &= oRow("FTCode") & "|"
                            Next

                            If tText.Trim <> "" Then
                                tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                                _Cmd &= vbCrLf & " and  T.FTEmpTypeCode IN('" & tText.Replace("|", "','") & "') "
                            End If
                    End Select
                End If

                '***Department***
                If (.otpdepartment.PageVisible) Then
                    Select Case .FNDeptCon.SelectedIndex
                        Case 1
                            If .FNHSysDeptId.Text <> "" Then
                                _Cmd &= vbCrLf & " and D.FTDeptCode  >='" & HI.UL.ULF.rpQuoted(.FNHSysDeptId.Text) & "' "
                            End If
                            If .FNHSysDeptIdTo.Text <> "" Then
                                _Cmd &= vbCrLf & " and D.FTDeptCode  <='" & HI.UL.ULF.rpQuoted(.FNHSysDeptIdTo.Text) & "' "
                            End If
                        Case 2
                            tText = ""
                            For Each oRow As DataRow In .DbDtDepartment.Rows
                                tText &= oRow("FTCode") & "|"
                            Next
                            If tText.Trim <> "" Then
                                tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                                _Cmd &= vbCrLf & " and D.FTDeptCode  IN('" & tText.Replace("|", "','") & "') "
                            End If

                        Case Else
                    End Select
                End If


                '***Division***
                If (.otpdivision.PageVisible) Then
                    Select Case .FNDivisionCon.SelectedIndex
                        Case 1
                            If .FNHSysDivisonId.Text <> "" Then
                                _Cmd &= vbCrLf & " and  S.FTDivisonCode >='" & HI.UL.ULF.rpQuoted(.FNHSysDivisonId.Text) & "' "
                            End If
                            If .FNHSysDivisonIdTo.Text <> "" Then
                                _Cmd &= vbCrLf & " and  S.FTDivisonCode <='" & HI.UL.ULF.rpQuoted(.FNHSysDivisonIdTo.Text) & "' "
                            End If
                        Case 2
                            tText = ""
                            For Each oRow As DataRow In .DbDtDivision.Rows
                                tText &= oRow("FTCode") & "|"
                            Next
                            If tText.Trim <> "" Then
                                tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                                _Cmd &= vbCrLf & " and  S.FTDivisonCode IN('" & tText.Replace("|", "','") & "') "
                            End If
                        Case Else
                    End Select
                End If



                '***Sect***
                If (.otpsect.PageVisible) Then
                    Select Case .FNSectCon.SelectedIndex
                        Case 1
                            If .FNHSysSectId.Text <> "" Then
                                _Cmd &= vbCrLf & " and  C.FTSectCode  >='" & HI.UL.ULF.rpQuoted(.FNHSysSectId.Text) & "' "
                            End If
                            If .FNHSysSectIdTo.Text <> "" Then
                                _Cmd &= vbCrLf & " and  C.FTSectCode <='" & HI.UL.ULF.rpQuoted(.FNHSysSectIdTo.Text) & "' "
                            End If
                        Case 2
                            tText = ""

                            For Each oRow As DataRow In .DbDtSect.Rows
                                tText &= oRow("FTCode") & "|"
                            Next

                            If tText.Trim <> "" Then
                                tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                                _Cmd &= vbCrLf & " and C.FTSectCode  IN('" & tText.Replace("|", "','") & "') "
                            End If
                        Case Else
                    End Select
                End If

                '***Unit Sect***
                If (.otpunitsect.PageVisible) Then
                    Select Case .FNUnitSectCon.SelectedIndex
                        Case 1
                            If .FNHSysUnitSectId.Text <> "" Then
                                _Cmd &= vbCrLf & " and  F.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(.FNHSysUnitSectId.Text) & "' "
                            End If
                            If .FNHSysUnitSectIdTo.Text <> "" Then
                                _Cmd &= vbCrLf & " and  F.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(.FNHSysUnitSectIdTo.Text) & "' "
                            End If
                        Case 2
                            tText = ""

                            For Each oRow As DataRow In .DbDtUnitSect.Rows
                                tText &= oRow("FTCode") & "|"
                            Next

                            If tText.Trim <> "" Then
                                tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                                _Cmd &= vbCrLf & " and  F.FTUnitSectCode  IN('" & tText.Replace("|", "','") & "') "
                            End If

                        Case Else
                    End Select
                End If


                '***Employee***
                If (.otpemployee.PageVisible) Then
                    Select Case .FNEmpCon.SelectedIndex
                        Case 1
                            If .FNHSysEmpID.Text <> "" Then
                                _Cmd &= vbCrLf & " and E.FTEmpCode  >='" & HI.UL.ULF.rpQuoted(.FNHSysEmpID.Text) & "' "
                            End If
                            If .FNHSysEmpIDTo.Text <> "" Then
                                _Cmd &= vbCrLf & " and E.FTEmpCode <='" & HI.UL.ULF.rpQuoted(.FNHSysEmpIDTo.Text) & "' "
                            End If
                        Case 2
                            tText = ""

                            For Each oRow As DataRow In .DbDtEmp.Rows
                                tText &= oRow("FTCode") & "|"
                            Next

                            If tText.Trim <> "" Then
                                tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                                _Cmd &= vbCrLf & " and  E.FTEmpCode IN('" & tText.Replace("|", "','") & "') "
                            End If

                        Case Else
                    End Select
                End If
            End With

            _Cmd &= vbCrLf & "Group by  E.FTEmpCode, E.FNHSysEmpID ,P.FTPreNameNameEN , E.FTEmpNameEN ,E.FTEmpSurnameEN, E.FTAccNo, E.FTEmpIdNo, R.FNNetpay, R.FTPayYear, R.FTPayTerm, R.FNHSysBankId, "
            _Cmd &= vbCrLf & "      R.FNHSysBankBranchId, R.FNHSysEmpTypeId, R.FNHSysDeptId, R.FNHSysDivisonId, R.FNHSysSectId, R.FNHSysUnitSectId ,DT.FNMonth"
            _Cmd &= vbCrLf & ",DT.FDPayDate ,W.FNServicefee ,W.FNFinTransFee"
            Dim _oDt As System.Data.DataTable
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR)


            'Call NewExcel(_oDt, _Spls)
            Call NewExcelNew_BankForm(_oDt, _Spls)
        Catch ex As Exception
            'MsgBox("Error Step1" & ex.Message.ToString)
        End Try
    End Sub

    Private _FileName As String = ""

    Private Sub NewExcel(ByVal _oDt As System.Data.DataTable, _Spls As HI.TL.SplashScreen)
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


            Dim TmpFile As String = _Path & "\Reports\TmpPayRollToBank.xlsx"
            Dim BakFile As String = _Path & "\Reports\Blank.xlsx"

            Dim oExcel As New Microsoft.Office.Interop.Excel.Application
            Dim xlBookTmp As Workbook
            Dim xlBookBak As Workbook
            Dim Excel As New Microsoft.Office.Interop.Excel.XlDirection


            xlBookTmp = oExcel.Workbooks.Open(TmpFile)
            xlBookBak = oExcel.Workbooks.Open(BakFile)



            xlBookTmp.Worksheets(1).copy(After:=xlBookTmp.Worksheets(1))
            Dim i As Integer = 13
            With xlBookTmp.Worksheets(1)
                If _oDt.Rows.Count > 0 Then
                    .Cells(3, 1) = "Đợt  " & Me.FTPayTerm.Text & "/tháng  " & _oDt.Rows(0)!FTMonth.ToString & "  /năm " & Me.FTPayYear.Text
                    .Cells(4, 1) = "Term  " & Me.FTPayTerm.Text & "/month  " & _oDt.Rows(0)!FTMonth.ToString & "  /year " & Me.FTPayYear.Text
                End If

                'oExcel.Application.DisplayAlerts = False
                'oExcel.Selection.Merge()

                'oExcel.Application.DisplayAlerts = True
                'oExcel.Application.DisplayAlerts = True,
                Try

                    '.Cells(i + 1, 1).Resize(_oDt.Rows.Count).Insert(Shift:=XlDirection.xlDown)
                    '.Cells(i + 1, 2).Resize(_oDt.Rows.Count).Insert(Shift:=XlDirection.xlDown)
                    '.Cells(i + 1, 3).Resize(_oDt.Rows.Count).Insert(Shift:=XlDirection.xlDown)
                    '.Cells(i + 1, 4).Resize(_oDt.Rows.Count).Insert(Shift:=XlDirection.xlDown)
                    '.Cells(i + 1, 5).Resize(_oDt.Rows.Count).Insert(Shift:=XlDirection.xlDown)
                Catch ex As Exception
                End Try


            End With




            For Each R As DataRow In _oDt.Rows
                i += +1

                With xlBookTmp.Worksheets(1)
                    'Date
                    .Rows(CStr(i) & ":" & CStr(i)).Insert(Shift:=XlDirection.xlDown)
                    .Cells(i, 1).Font.Color = 0
                    .Cells(i, 1) = "'" & R!FNSeq.ToString
                    .Cells(i, 2).Font.Color = 0
                    .Cells(i, 2) = "'" & R!Name.ToString
                    .Cells(i, 3).Font.Color = 0
                    .Cells(i, 3) = "'" & R!FTAccNo.ToString
                    .Cells(i, 4).Font.Color = 0
                    .Cells(i, 4) = "'" & R!FTEmpIdNo.ToString
                    .Cells(i, 5).Font.Color = 0
                    .Cells(i, 5) = R!FNNetpay.ToString
                    .Cells(i, 5).NumberFormat = "#,###,###"
                    'NumberFormat = "#,###,###"
                End With

            Next



            i += +2
            With xlBookTmp.Worksheets(1)
                .Cells(i, 5) = "=SUM(E14:E" & 14 + _oDt.Rows.Count & ")"

            End With


            'xlBookTmp.Worksheets(1).Select()

            Try
                If oExcel.Application.Sheets.Count > 1 Then
                    For xi As Integer = oExcel.Application.Sheets.Count To 2 Step -1
                        Try
                            CType(oExcel.Application.ActiveWorkbook.Sheets(xi), Worksheet).Delete()
                            oExcel.Application.DisplayAlerts = False
                        Catch ex As Exception
                        End Try
                        Try
                            oExcel.Sheets(xi).delete()
                            oExcel.Application.DisplayAlerts = True
                        Catch ex As Exception
                        End Try
                    Next
                End If
            Catch ex As Exception
            End Try

            Try
                CType(oExcel.Application.ActiveWorkbook.Sheets(1), Worksheet).Select()
            Catch ex As Exception
            End Try

            oExcel.DisplayAlerts = False
            '_FileName = "C:\Users\NOH-NB\Desktop\TestFile.xlsx"

            xlBookTmp.SaveAs(_FileName)
            xlBookBak.Close()
            xlBookTmp.Close()
            _Spls.Close()

            Process.Start(_FileName)





        Catch ex As Exception
            _Spls.Close()
            HI.MG.ShowMsg.mProcessError(1505029917, "Writing Excel File Error....." & vbCrLf & ex.Message.ToString, Me.Text, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub NewExcelNew_BankForm(ByVal _oDt As System.Data.DataTable, _Spls As HI.TL.SplashScreen)
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
            Dim TmpFile As String = _FileName
            Dim BakFile As String = _FileName

            Dim oExcel As New Microsoft.Office.Interop.Excel.Application
            Dim xlBookTmp As Workbook
            Dim Excel As New Microsoft.Office.Interop.Excel.XlDirection
            xlBookTmp = oExcel.Workbooks.Open(TmpFile)

            Dim i As Integer = 6
            Dim x As Integer = 1000000
            Dim _AccNo As String = ""

            With xlBookTmp.Sheets(1)
                For z As Integer = i To x
                    .Cells(z, 2).Font.Color = 0
                    _AccNo = .Cells(z, 2).Value
                    If _AccNo = "" Then Exit For
                    _AccNo = Replace(_AccNo, "-", "")
                    For Each R As DataRow In _oDt.Select("FTAccNo ='" & _AccNo & "'")
                        .Cells(z, 6) = Double.Parse("0" & R!FNNetpay.ToString) + Double.Parse("0" & R!FNFinTransFee.ToString)
                        .Cells(z, 7) = Double.Parse("0" & R!FNFinTransFee.ToString)
                    Next
                    .Cells(z, 6).NumberFormat = "#,###,###.00"
                    ' .Cells(z, 6).interior.color = Color.LightPink
                    .Cells(z, 7).NumberFormat = "#,###,###.00"
                Next

            End With

            Try
                If oExcel.Application.Sheets.Count > 4 Then
                    For xi As Integer = oExcel.Application.Sheets.Count To 2 Step -1
                        Try
                            If Microsoft.VisualBasic.Right(CType(oExcel.Application.ActiveWorkbook.Sheets(xi), Worksheet).Name.ToString(), 1) = ")" Then
                                CType(oExcel.Application.ActiveWorkbook.Sheets(xi), Worksheet).Delete()
                                oExcel.Application.DisplayAlerts = False
                            End If
                        Catch ex As Exception
                        End Try
                        Try
                            If Microsoft.VisualBasic.Right(oExcel.Sheets(xi).Name.ToString(), 1) = ")" Then
                                oExcel.Sheets(xi).delete()
                                oExcel.Application.DisplayAlerts = True
                            End If
                        Catch ex As Exception
                        End Try
                    Next
                End If
            Catch ex As Exception
            End Try

            Try
                CType(oExcel.Application.ActiveWorkbook.Sheets(1), Worksheet).Select()
            Catch ex As Exception
            End Try

            oExcel.DisplayAlerts = False
            xlBookTmp.Save()
            xlBookTmp.Close()
            _Spls.Close()
            Process.Start(_FileName)
        Catch ex As Exception
            _Spls.Close()
            HI.MG.ShowMsg.mProcessError(1505029917, "Writing Excel File Error....." & vbCrLf & ex.Message.ToString, Me.Text, MessageBoxIcon.Error)
        End Try
    End Sub
End Class