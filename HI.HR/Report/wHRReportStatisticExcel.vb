Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Office.Interop.Excel

Imports System.Windows.Forms



Public Class wHRReportStatisticExcel

    Private _LstReport As HI.RP.ListReport
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.



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
        ' FNHSysEmpID, FTEmpCode, FNHSysCmpId, FNHSysEmpTypeId, FNHSysDeptId, FNHSysDivisonId, FNHSysSectId, FNHSysUnitSectId, FNHSysPositId

        Dim _Qry As String = ""

       
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"

            Op.ShowDialog()
            Try
                If Op.FileName <> "" Then
                    _FileName = Op.FileName.ToString
                    'ExportExcel()
                    Dim _Spls As New HI.TL.SplashScreen("Prepre Data For Calculate.. Please Wait ")

                    _NewExcel(_Spls)

                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try


     
        'ExportExcelNew()
    End Sub

    Private _FileName As String = ""

    Private Sub _NewExcel(_Spls As HI.TL.SplashScreen)
        Try
            Dim _Qry As String = ""
            Dim _oDt As System.Data.DataTable
            Dim _oDtM As System.Data.DataTable


            Dim _Rec As Integer = 0
            Dim _RecError As Integer = 0
            Dim _TotalRec As Integer = 0

            Dim _l As Integer = 0

            Dim _SumPQtyDay As Double = 0
            Dim _SumUQtyDay As Double = 0



            'Dim xlApp As Microsoft.Office.Interop.Excel.Application
            'Dim xlWorkBook As Workbook
            'Dim xlWorkSheet() As Worksheet


            Dim misValue As Object = System.Reflection.Missing.Value

            Dim _Path As String = System.Windows.Forms.Application.StartupPath.ToString


            'Dim oExcel As Object
            'oExcel = CreateObject("Excel.Application")
            'oExcel.Workbooks.Open(_Path & "\Reports\ExcelRptHRStatistic.xlsx")

            Dim TmpFile As String = _Path & "\Reports\ExcelRptHRStatistic.xlsx"
            Dim BakFile As String = _Path & "\Reports\Blank.xlsx"

            Dim oExcel As New Microsoft.Office.Interop.Excel.Application
            Dim xlBookTmp As Workbook
            Dim xlBookBak As Workbook

            xlBookTmp = oExcel.Workbooks.Open(TmpFile)
            xlBookBak = oExcel.Workbooks.Open(BakFile)


            'Dim oBook As Workbook
            'Dim oSheet As Worksheet
            'oBook = oExcel.ActiveWorkbook
            'oSheet = oExcel.Worksheets(1)



            'xlApp = New Microsoft.Office.Interop.Excel.Application
            'xlWorkBook = xlApp.Workbooks.Add(misValue)










            '_Qry = " SELECT     FNHSysUnitSectId, FTUnitSectCode , FTUnitSectNameTH, FTUnitSectNameEN"
            '_Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect WITH (NOLOCK)"
            '_Qry &= vbCrLf & " WHERE FNHSysUnitSectId <> 0"

            _Qry &= vbCrLf & " SELECT  distinct   MU.FTUnitSectCode, MU.FNHSysUnitSectId, MU.FTUnitSectNameTH, MU.FTUnitSectNameEN"
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Em WITH(NOLOCK)"
            _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS MT WITH(NOLOCK) ON Em.FNHSysEmpTypeId = MT.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS MD WITH(NOLOCK) ON Em.FNHSysDeptId = MD.FNHSysDeptId"
            _Qry &= vbCrLf & "	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS MV	WITH(NOLOCK) ON Em.FNHSysDivisonId = MV.FNHSysDivisonId"
            _Qry &= vbCrLf & "	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS MS WITH(NOLOCK) ON Em.FNHSysSectId = MS.FNHSysSectId"
            _Qry &= vbCrLf & "	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS MU WITH(NOLOCK) ON Em.FNHSysUnitSectId = MU.FNHSysUnitSectId"
            _Qry &= vbCrLf & " WHERE Em.FNHSysEmpID <> 0 and MU.FNHSysUnitSectId <> 0"


            '***Unit Sect***
            Dim tText As String = ""
            If (Me.Condition.otpunitsect.PageVisible) Then
                Select Case Condition.FNUnitSectCon.SelectedIndex
                    Case 1
                        If Me.Condition.FNHSysUnitSectId.Text <> "" Then

                            _Qry &= vbCrLf & "AND    Isnull(MU.FTUnitSectCode,'')  >='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysUnitSectId.Text) & "' "
                        End If
                        If Me.Condition.FNHSysUnitSectIdTo.Text <> "" Then

                            _Qry &= vbCrLf & "AND    Isnull(MU.FTUnitSectCode,'')  <='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysUnitSectIdTo.Text) & "' "
                        End If
                    Case 2


                        For Each oRow As DataRow In Me.Condition.DbDtUnitSect.Rows
                            tText &= oRow("FTCode") & "|"
                        Next

                        If tText.Trim <> "" Then
                            tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)

                            _Qry &= vbCrLf & "AND    Isnull(MU.FTUnitSectCode,'')   IN('" & tText.Replace("|", "','") & "') "
                        End If

                    Case Else
                End Select
            End If

            If (Me.Condition.otpemptype.PageVisible) Then
                Select Case Me.Condition.FNEmpTypeCon.SelectedIndex
                    Case 1

                        If Me.Condition.FNHSysEmpTypeId.Text <> "" Then

                            _Qry &= vbCrLf & "AND MT.FTEmpTypeCode >=" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysEmpTypeId.Text) & "' "
                        End If

                        If Me.Condition.FNHSysEmpTypeIdTo.Text <> "" Then
                            _Qry &= vbCrLf & "AND MT.FTEmpTypeCode <=" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysEmpTypeIdTo.Text) & "' "
                        End If

                    Case 2
                        tText = ""

                        For Each oRow As DataRow In Me.Condition.DbDtEmployeeType.Rows
                            tText &= oRow("FTCode") & "|"
                        Next

                        If tText.Trim <> "" Then
                            tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                            _Qry &= vbCrLf & "AND  MT.FTEmpTypeCode in('" & tText.Replace("|", "','") & "') "

                        End If

                    Case Else
                End Select
            End If

            '***Department***
            If (Me.Condition.otpdepartment.PageVisible) Then
                Select Case Me.Condition.FNDeptCon.SelectedIndex
                    Case 1
                        If Me.Condition.FNHSysDeptId.Text <> "" Then
                            _Qry &= vbCrLf & "AND Isnull(MD.FTDeptCode,'') >='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysDeptId.Text) & "' "

                        End If
                        If Me.Condition.FNHSysDeptIdTo.Text <> "" Then
                            _Qry &= vbCrLf & "AND Isnull(MD.FTDeptCode,'') <='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysDeptIdTo.Text) & "' "

                        End If
                    Case 2
                        tText = ""
                        For Each oRow As DataRow In Me.Condition.DbDtDepartment.Rows
                            tText &= oRow("FTCode") & "|"
                        Next
                        If tText.Trim <> "" Then
                            tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                            _Qry &= vbCrLf & "AND Isnull(MD.FTDeptCode,'') in('" & tText.Replace("|", "','") & "') "

                        End If

                    Case Else
                End Select
            End If


            '***Division***
            If (Me.Condition.otpdivision.PageVisible) Then
                Select Case Me.Condition.FNDivisionCon.SelectedIndex
                    Case 1
                        If Me.Condition.FNHSysDivisonId.Text <> "" Then
                            _Qry &= vbCrLf & "AND  Isnull(MV.FTDivisonCode,'') >='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysDivisonId.Text) & "' "

                        End If
                        If Me.Condition.FNHSysDivisonIdTo.Text <> "" Then
                            _Qry &= vbCrLf & "AND  Isnull(MV.FTDivisonCode,'')  <='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysDivisonIdTo.Text) & "' "

                        End If
                    Case 2
                        tText = ""

                        For Each oRow As DataRow In Me.Condition.DbDtDivision.Rows
                            tText &= oRow("FTCode") & "|"
                        Next

                        If tText.Trim <> "" Then
                            tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)

                            _Qry &= vbCrLf & "AND  Isnull(MV.FTDivisonCode,'') IN('" & tText.Replace("|", "','") & "') "
                        End If
                    Case Else
                End Select
            End If


            '***Sect***
            If (Me.Condition.otpsect.PageVisible) Then
                Select Case Condition.FNSectCon.SelectedIndex
                    Case 1
                        If Me.Condition.FNHSysSectId.Text <> "" Then

                            _Qry &= vbCrLf & "AND    Isnull(MS.FTSectCode,'')  >='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysSectId.Text) & "' "
                        End If
                        If Me.Condition.FNHSysSectIdTo.Text <> "" Then

                            _Qry &= vbCrLf & "AND      Isnull(MS.FTSectCode,'')  <='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysSectIdTo.Text) & "' "
                        End If
                    Case 2
                        tText = ""

                        For Each oRow As DataRow In Me.Condition.DbDtSect.Rows
                            tText &= oRow("FTCode") & "|"
                        Next

                        If tText.Trim <> "" Then
                            tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)

                            _Qry &= vbCrLf & "AND      Isnull(MS.FTSectCode,'')  IN('" & tText.Replace("|", "','") & "') "
                        End If
                    Case Else
                End Select
            End If


            '***Employee***
            If (Me.Condition.otpemployee.PageVisible) Then
                Select Case Condition.FNEmpCon.SelectedIndex
                    Case 1
                        If Me.Condition.FNHSysEmpID.Text <> "" Then

                            _Qry &= vbCrLf & "AND    Em.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysEmpID.Text) & "' "
                        End If
                        If Me.Condition.FNHSysEmpIDTo.Text <> "" Then

                            _Qry &= vbCrLf & "AND    Em.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysEmpIDTo.Text) & "' "
                        End If
                    Case 2
                        tText = ""

                        For Each oRow As DataRow In Me.Condition.DbDtEmp.Rows
                            tText &= oRow("FTCode") & "|"
                        Next

                        If tText.Trim <> "" Then
                            tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)

                            _Qry &= vbCrLf & "AND   Em.FTEmpCode IN('" & tText.Replace("|", "','") & "') "
                        End If

                    Case Else
                End Select
            End If

            _Qry &= vbCrLf & "ORDER BY MU.FTUnitSectCode ASC"
            _oDtM = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            '*******************************
            For Each H As DataRow In _oDtM.Rows

                _Qry = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.TmpEmpId  WHERE  FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)


                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.TmpEmpId(FNHSysEmpID, FTUserLogIn)"
                _Qry &= vbCrLf & " SELECT Em.FNHSysEmpID,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS Em WITH(NOLOCK)"
                _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS MT WITH(NOLOCK) ON Em.FNHSysEmpTypeId = MT.FNHSysEmpTypeId"
                _Qry &= vbCrLf & "	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS MD WITH(NOLOCK) ON Em.FNHSysDeptId = MD.FNHSysDeptId"
                _Qry &= vbCrLf & "	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS MV	WITH(NOLOCK) ON Em.FNHSysDivisonId = MV.FNHSysDivisonId"
                _Qry &= vbCrLf & "	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS MS WITH(NOLOCK) ON Em.FNHSysSectId = MS.FNHSysSectId"
                _Qry &= vbCrLf & "	LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS MU WITH(NOLOCK) ON Em.FNHSysUnitSectId = MU.FNHSysUnitSectId"
                _Qry &= vbCrLf & " WHERE Em.FNHSysEmpID <> 0"
                _Qry &= vbCrLf & " AND Isnull(MU.FTUnitSectCode,'')='" & HI.UL.ULF.rpQuoted(H!FTUnitSectCode.ToString) & "'"

                '***Empployee Type***
                'Dim tText As String = ""
                If (Me.Condition.otpemptype.PageVisible) Then
                    Select Case Me.Condition.FNEmpTypeCon.SelectedIndex
                        Case 1

                            If Me.Condition.FNHSysEmpTypeId.Text <> "" Then

                                _Qry &= vbCrLf & "AND MT.FTEmpTypeCode >=" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysEmpTypeId.Text) & "' "
                            End If

                            If Me.Condition.FNHSysEmpTypeIdTo.Text <> "" Then
                                _Qry &= vbCrLf & "AND MT.FTEmpTypeCode <=" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysEmpTypeIdTo.Text) & "' "
                            End If

                        Case 2
                            tText = ""

                            For Each oRow As DataRow In Me.Condition.DbDtEmployeeType.Rows
                                tText &= oRow("FTCode") & "|"
                            Next

                            If tText.Trim <> "" Then
                                tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                                _Qry &= vbCrLf & "AND  MT.FTEmpTypeCode in('" & tText.Replace("|", "','") & "') "

                            End If

                        Case Else
                    End Select
                End If

                '***Department***
                If (Me.Condition.otpdepartment.PageVisible) Then
                    Select Case Me.Condition.FNDeptCon.SelectedIndex
                        Case 1
                            If Me.Condition.FNHSysDeptId.Text <> "" Then
                                _Qry &= vbCrLf & "AND Isnull(MD.FTDeptCode,'') >='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysDeptId.Text) & "' "

                            End If
                            If Me.Condition.FNHSysDeptIdTo.Text <> "" Then
                                _Qry &= vbCrLf & "AND Isnull(MD.FTDeptCode,'') <='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysDeptIdTo.Text) & "' "

                            End If
                        Case 2
                            tText = ""
                            For Each oRow As DataRow In Me.Condition.DbDtDepartment.Rows
                                tText &= oRow("FTCode") & "|"
                            Next
                            If tText.Trim <> "" Then
                                tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                                _Qry &= vbCrLf & "AND Isnull(MD.FTDeptCode,'') in('" & tText.Replace("|", "','") & "') "

                            End If

                        Case Else
                    End Select
                End If


                '***Division***
                If (Me.Condition.otpdivision.PageVisible) Then
                    Select Case Me.Condition.FNDivisionCon.SelectedIndex
                        Case 1
                            If Me.Condition.FNHSysDivisonId.Text <> "" Then
                                _Qry &= vbCrLf & "AND  Isnull(MV.FTDivisonCode,'') >='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysDivisonId.Text) & "' "

                            End If
                            If Me.Condition.FNHSysDivisonIdTo.Text <> "" Then
                                _Qry &= vbCrLf & "AND  Isnull(MV.FTDivisonCode,'')  <='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysDivisonIdTo.Text) & "' "

                            End If
                        Case 2
                            tText = ""

                            For Each oRow As DataRow In Me.Condition.DbDtDivision.Rows
                                tText &= oRow("FTCode") & "|"
                            Next

                            If tText.Trim <> "" Then
                                tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)

                                _Qry &= vbCrLf & "AND  Isnull(MV.FTDivisonCode,'') IN('" & tText.Replace("|", "','") & "') "
                            End If
                        Case Else
                    End Select
                End If


                '***Sect***
                If (Me.Condition.otpsect.PageVisible) Then
                    Select Case Condition.FNSectCon.SelectedIndex
                        Case 1
                            If Me.Condition.FNHSysSectId.Text <> "" Then

                                _Qry &= vbCrLf & "AND    Isnull(MS.FTSectCode,'')  >='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysSectId.Text) & "' "
                            End If
                            If Me.Condition.FNHSysSectIdTo.Text <> "" Then

                                _Qry &= vbCrLf & "AND      Isnull(MS.FTSectCode,'')  <='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysSectIdTo.Text) & "' "
                            End If
                        Case 2
                            tText = ""

                            For Each oRow As DataRow In Me.Condition.DbDtSect.Rows
                                tText &= oRow("FTCode") & "|"
                            Next

                            If tText.Trim <> "" Then
                                tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)

                                _Qry &= vbCrLf & "AND      Isnull(MS.FTSectCode,'')  IN('" & tText.Replace("|", "','") & "') "
                            End If
                        Case Else
                    End Select
                End If


                '***Employee***
                If (Me.Condition.otpemployee.PageVisible) Then
                    Select Case Condition.FNEmpCon.SelectedIndex
                        Case 1
                            If Me.Condition.FNHSysEmpID.Text <> "" Then

                                _Qry &= vbCrLf & "AND    Em.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysEmpID.Text) & "' "
                            End If
                            If Me.Condition.FNHSysEmpIDTo.Text <> "" Then

                                _Qry &= vbCrLf & "AND    Em.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysEmpIDTo.Text) & "' "
                            End If
                        Case 2
                            tText = ""

                            For Each oRow As DataRow In Me.Condition.DbDtEmp.Rows
                                tText &= oRow("FTCode") & "|"
                            Next

                            If tText.Trim <> "" Then
                                tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)

                                _Qry &= vbCrLf & "AND   Em.FTEmpCode IN('" & tText.Replace("|", "','") & "') "
                            End If

                        Case Else
                    End Select
                End If

                '***************insert
                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)


                'subExportExcel()

                '*************************************************************

                'Start loop month 

                Dim _oDtMonth As System.Data.DataTable

                _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.[SP_GET_MONTH] '" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _oDtMonth = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                'End loop month
                _TotalRec = _oDtMonth.Rows.Count * _oDtM.Rows.Count
                For Each M As DataRow In _oDtMonth.Rows
                    _Rec += +1
                    _Spls.UpdateInformation("Excel File Writing...  Sheets " & _Rec.ToString & " Of " & _TotalRec.ToString & "  (" & Format((_Rec * 100.0) / _TotalRec, "0.00") & " % )")


                    Dim _oDtS As System.Data.DataTable

                    _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.[SP_REPORT_STATISTIC_HR] '" & HI.UL.ULDate.ConvertEnDB(M!MinFDDate.ToString) & "','" & HI.UL.ULDate.ConvertEnDB(M!MaxFDDate.ToString) & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _oDtS = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


                    _l += +1
                    'xlWorkSheet = xlWorkBook.Worksheets.Add(After:=xlWorkBook.Worksheets(_l))

                    'xlWorkBook.Sheets(_l).name = "" & H!FTUnitSectCode.ToString





                    xlBookTmp.Worksheets(1).copy(After:=xlBookTmp.Worksheets(_l))






                    Dim i As Integer = 5

                    With xlBookTmp.Worksheets(_l)
                        .Cells(6, 42).Formula = "=(E37*100)/(F6+F" & (_oDtS.Rows.Count + 5).ToString & "/2)"
                        .Cells(6, 42).NumberFormat = "0.00"
                    End With


                    _SumPQtyDay = 0
                    _SumUQtyDay = 0
                    For Each R As DataRow In _oDtS.Rows
                        i += +1

                        With xlBookTmp.Worksheets(_l)
                            'Date

                            .Cells(i, 1) = "'" & Mid(R!FTDateTrans.ToString, 9, 2) + "/" + Mid(R!FTDateTrans.ToString, 6, 2) + "/" + Mid(R!FTDateTrans.ToString, 1, 4)

                            'จำนวนพนักงาน (แผน)
                            .Cells(i, 2) = "" & CInt("0" & R!EmpQty.ToString)
                            'พนักงานเข้าใหม่
                            .Cells(i, 4) = "" & CInt("0" & R!QtyStart.ToString)

                            'ลาออก
                            .Cells(i, 5) = "" & CInt("0" & R!QtyEnd.ToString)


                            'ลากิจ
                            .Cells(i, 8) = "" & CInt("0" & R!LeaveV.ToString)
                            .Cells(i, 9) = "" & CInt("0" & R!D1.ToString)
                            .Cells(i, 10) = "'" & R!_H1.ToString

                            'ลาพักร้อน
                            .Cells(i, 11) = "" & CInt("0" & R!LeaveH.ToString)
                            .Cells(i, 12) = "" & CInt("0" & R!D98.ToString)
                            .Cells(i, 13) = "'" & R!_H98.ToString

                            'ลาคลอด
                            .Cells(i, 14) = "" & CInt("0" & R!LeaveM.ToString)
                            .Cells(i, 15) = "" & CInt("0" & R!D97.ToString)
                            .Cells(i, 16) = "'" & R!_H97.ToString

                            'รวม Plan
                            '.Cells(i, 17) = "" & CInt("0" & R!PQtyPeople.ToString)
                            '.Cells(i, 18) = "" & CInt("0" & R!PQtyDay.ToString)
                            .Cells(i, 19) = "'" & R!PQtyHour.ToString

                            'ลาป่วย
                            .Cells(i, 20) = "" & CInt("0" & R!LeaveA.ToString)
                            .Cells(i, 21) = "" & CInt("0" & R!D0.ToString)
                            .Cells(i, 22) = "'" & R!_H0.ToString

                            'ขาด 
                            .Cells(i, 23) = "" & CInt("0" & R!AbEmpQty.ToString)
                            .Cells(i, 24) = "" & CInt("0" & R!AbsentDay.ToString)
                            .Cells(i, 25) = "'" & R!AbsentHour.ToString


                            'รวม
                            '.Cells(i, 27) = "" & CInt("0" & R!UQtyDay.ToString)
                            .Cells(i, 28) = "'" & R!UQtyHour.ToString

                            'สาย
                            .Cells(i, 32) = "" & CInt("0" & R!EmpLateQty.ToString)
                            .Cells(i, 33) = "" & CInt("0" & R!FNLateNormalMin.ToString)


                            '.Cells(i, 34).Formula = "=(I" & i.ToString & "*100)/F" & i.ToString & ""
                            '.Cells(i, 35).Formula = "=(L" & i.ToString & "*100)/F" & i.ToString & ""
                            '.Cells(i, 36).Formula = "=(O" & i.ToString & "*100)/F" & i.ToString & ""
                            '.Cells(i, 37).Formula = "=(R" & i.ToString & "*100)/F" & i.ToString & ""
                            '.Cells(i, 38).Formula = "=(U" & i.ToString & "*100)/F" & i.ToString & ""
                            '.Cells(i, 39).Formula = "=(X" & i.ToString & "*100)/F" & i.ToString & ""
                            '.Cells(i, 40).Formula = "=(AA" & i.ToString & "*100)/F" & i.ToString & ""
                            '.Cells(i, 41).Formula = "=(R" & i.ToString & "+AA" & i.ToString & ")*100/F" & i.ToString & ""



                            .Cells(i, 34).Formula = "=(" & CDbl("0" & R!FN_D1.ToString) & "*100)/F" & i.ToString & ""
                            .Cells(i, 34).NumberFormat = "0.00"
                            .Cells(i, 35).Formula = "=(" & CDbl("0" & R!FN_D98.ToString) & "*100)/F" & i.ToString & ""
                            .Cells(i, 35).NumberFormat = "0.00"
                            .Cells(i, 36).Formula = "=(" & CDbl("0" & R!FN_D97.ToString) & "*100)/F" & i.ToString & ""
                            .Cells(i, 36).NumberFormat = "0.00"
                            'sum by plan
                            .Cells(i, 37).Formula = "=(" & CDbl("0" & R!sumPQtyDay.ToString) & "*100)/F" & i.ToString & ""
                            .Cells(i, 37).NumberFormat = "0.00"
                            .Cells(i, 38).Formula = "=(" & CDbl("0" & R!FN_D0.ToString) & "*100)/F" & i.ToString & ""
                            .Cells(i, 38).NumberFormat = "0.00"
                            .Cells(i, 39).Formula = "=(" & CDbl("0" & R!FN_Absent.ToString) & "*100)/F" & i.ToString & ""
                            .Cells(i, 39).NumberFormat = "0.00"
                            .Cells(i, 40).Formula = "=(" & CDbl("0" & R!sumUQtyDay.ToString) & "*100)/F" & i.ToString & ""
                            .Cells(i, 40).NumberFormat = "0.00"
                            .Cells(i, 41).Formula = "=(" & CDbl("0" & R!sumPQtyDay.ToString) + CDbl("0" & R!sumUQtyDay.ToString) & ")*100/F" & i.ToString & ""
                            .Cells(i, 41).NumberFormat = "0.00"

                            _SumPQtyDay = _SumPQtyDay + CDbl("0" & R!sumPQtyDay.ToString)
                            _SumUQtyDay = _SumUQtyDay + CDbl("0" & R!sumUQtyDay.ToString)
                        End With

                    Next

                    Dim x As Integer = i
                    Dim j As Integer = i
                    Dim t As Integer = CInt("0" & j.ToString) - 5
                    x = 36
                    i = 37
                    With xlBookTmp.Worksheets(_l)
                        'i += +1

                        '.Cells(i, 1) = "'รวม"
                        '.Cells(i, 4).Formula = "=SUM(D6:D" & x.ToString & ")"

                        '.Cells(i, 5).Formula = "=SUM(E6:E" & x.ToString & ")"
                        '.Cells(i, 6).Formula = "=AVERAGE(F6:F" & x.ToString & ")"
                        '.Cells(i, 7).Formula = "=SUM(G6:G" & x.ToString & ")"
                        '.Cells(i, 8).Formula = "=SUM(H6:H" & x.ToString & ")"
                        '.Cells(i, 9).Formula = "=SUM(I6:I" & x.ToString & ")"

                        '.Cells(i, 11).Formula = "=SUM(K6:K" & x.ToString & ")"
                        '.Cells(i, 12).Formula = "=SUM(L6:L" & x.ToString & ")"
                        '.Cells(i, 14).Formula = "=SUM(N6:N" & x.ToString & ")"
                        '.Cells(i, 15).Formula = "=SUM(O6:O" & x.ToString & ")"
                        '.Cells(i, 17).Formula = "=SUM(Q6:Q" & x.ToString & ")"
                        '.Cells(i, 18).Formula = "=SUM(R6:R" & x.ToString & ")"

                        '.Cells(i, 20).Formula = "=SUM(T6:T" & x.ToString & ")"
                        '.Cells(i, 21).Formula = "=SUM(U6:U" & x.ToString & ")"

                        '.Cells(i, 23).Formula = "=SUM(W6:W" & x.ToString & ")"
                        '.Cells(i, 24).Formula = "=SUM(X6:X" & x.ToString & ")"

                        '.Cells(i, 26).Formula = "=SUM(Z6:Z" & x.ToString & ")"

                        '.Cells(i, 27).Formula = "=SUM(AA6:AA" & x.ToString & ")"
                        '.Cells(i, 29).Formula = "=SUM(AC6:AC" & x.ToString & ")"
                        '.Cells(i, 30).Formula = "=SUM(AD6:AD" & x.ToString & ")"

                        '.Cells(i, 31).Formula = "=AVERAGE(AE6:AE" & x.ToString & ")"
                        '.Cells(i, 32).Formula = "=SUM(AF6:AF" & x.ToString & ")"
                        '.Cells(i, 33).Formula = "=SUM(AG6:AG" & x.ToString & ")"
                        '.Cells(i, 34).Formula = "=SUM(E" & i.ToString & "*100)/F" & i.ToString & ""
                        '.Cells(i, 35).Formula = "=(I" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                        '.Cells(i, 36).Formula = "=SUM(L" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                        '.Cells(i, 37).Formula = "=(O" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                        '.Cells(i, 38).Formula = "=(R" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                        '.Cells(i, 39).Formula = "=(U" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                        '.Cells(i, 40).Formula = "=(U" & i.ToString & "*100)/(I" & i.ToString & "*25)"
                        '.Cells(i, 41).Formula = "=(AA" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                        '.Cells(i, 42).Formula = " =SUM(R" & i.ToString & ",AA" & i.ToString & ")*100/(F" & i.ToString & "*25)"

                        .Cells(37, 6).Formula = "=AVERAGE(F6:F" & j.ToString & ")"
                        .Cells(37, 6).NumberFormat = "0.00"

                        .Cells(37, 31).NumberFormat = "0.00"


                        .Cells(37, 34).Formula = "=(I37*100)/(F37*" & t.ToString & ")"
                        .Cells(37, 34).NumberFormat = "0.00"


                        .Cells(37, 35).Formula = "=(L37*100)/(F37*" & t.ToString & ")"
                        .Cells(37, 35).NumberFormat = "0.00"


                        .Cells(37, 36).Formula = "=(O37*100)/(F37*" & t.ToString & ")"
                        .Cells(37, 36).NumberFormat = "0.00"

                        .Cells(37, 37).Formula = "=(R37*100)/(F37*" & t.ToString & ")"
                        .Cells(37, 37).NumberFormat = "0.00"

                        .Cells(37, 38).Formula = "=(U37*100)/(F37*" & t.ToString & ")"
                        .Cells(37, 38).NumberFormat = "0.00"

                        .Cells(37, 39).Formula = "=(X37*100)/(F37*" & t.ToString & ")"
                        .Cells(37, 39).NumberFormat = "0.00"

                        .Cells(37, 40).Formula = "=(AA37*100)/(F37*" & t.ToString & ")"
                        .Cells(37, 40).NumberFormat = "0.00"

                        .Cells(37, 41).Formula = "=((" & _SumUQtyDay & "+" & _SumPQtyDay & ")*100)/(F37*" & t.ToString & ")"
                        .Cells(37, 41).NumberFormat = "0.00"


                        .Cells(37, 42).NumberFormat = "0.00"

                        .Range("B6:B" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow
                        .Range("C6:C" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow

                        .Range("G6:G" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow

                        .Range("AD6:AD" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow
                        .Range("AC6:AC" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow


                    End With


                    'xlWorkSheet(_l).Name = H!FTUnitSectCode.ToString
                    'xlBookTmp.Worksheets(_l).name = "" & H!FTUnitSectCode.ToString
                    '************************************************************
                    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                        xlBookTmp.Worksheets(_l).name = "" & H!FTUnitSectCode.ToString & "(" & Mid(M!MinFDDate.ToString, 6, 2) & ")"
                    Else
                        xlBookTmp.Worksheets(_l).name = "" & H!FTUnitSectCode.ToString & "(" & Mid(M!MinFDDate.ToString, 6, 2) & ")"
                    End If

                    'xlWorkSheet.SaveAs(_FileName, ReadOnlyRecommended:=True)




                Next

            Next



            xlBookTmp.Worksheets(1).Select()

            If oExcel.Application.Sheets.Count > _l Then

                CType(oExcel.Application.ActiveWorkbook.Sheets(_l + 1), Worksheet).Delete()
                oExcel.Application.DisplayAlerts = False
                oExcel.Sheets(_l + 1).delete()
                oExcel.Application.DisplayAlerts = True


            End If

            oExcel.DisplayAlerts = False

            xlBookTmp.SaveAs(_FileName)

            xlBookBak.Close()
            xlBookTmp.Close()
            _Spls.Close()
            Process.Start(_FileName)


            'xlWorkBook.Close()
            'xlApp.Quit()


            'releaseObject(xlApp)
            'releaseObject(xlWorkBook)
            ''releaseObject(xlWorkSheet)

        Catch ex As Exception
            _Spls.Close()
            HI.MG.ShowMsg.mProcessError(141115, "Writing Excel File Error.....", Me.Text, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub subExportExcel()
        Try
            Dim _Qry As String = ""
            Dim _oDtS As System.Data.DataTable

            _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.[SP_REPORT_STATISTIC_HR] '" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _oDtS = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Dim xlApp As Microsoft.Office.Interop.Excel.Application
            Dim xlWorkBook As Workbook
            Dim xlWorkSheet As Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value

            Dim _Path As String = System.Windows.Forms.Application.StartupPath.ToString

            Dim oExcel As Object
            oExcel = CreateObject("Excel.Application")
            oExcel.Workbooks.Open(_Path & "\Reports\ExcelRptHRStatistic.xlsx")

            Dim oBook As Workbook
            Dim oSheet As Worksheet
            oBook = oExcel.ActiveWorkbook
            oSheet = oExcel.Worksheets(1)



            xlApp = New Microsoft.Office.Interop.Excel.Application
            xlWorkBook = xlApp.Workbooks.Add(misValue)

            'xlWorkBook.LoadFromFile("E:\PROJECT\HI SOFT\HI\bin\Debug\Reports\ReportHRStatistic.xls")




            xlWorkSheet = oSheet


            Dim i As Integer = 5

            For Each R As DataRow In _oDtS.Rows
                i += +1

                With xlWorkSheet
                    'Date
                    .Cells(i, 1) = "'" & Mid(R!FTDateTrans.ToString, 9, 2) + "/" + Mid(R!FTDateTrans.ToString, 6, 2) + "/" + Mid(R!FTDateTrans.ToString, 1, 4)

                    'พนักงานเข้าใหม่
                    .Cells(i, 4) = "" & CInt("0" & R!QtyStart.ToString)

                    'ลาออก
                    .Cells(i, 5) = "" & CInt("0" & R!QtyEnd.ToString)


                    'ลากิจ
                    .Cells(i, 8) = "" & CInt("0" & R!LeaveV.ToString)
                    .Cells(i, 9) = "" & CInt("0" & R!D1.ToString)
                    .Cells(i, 10) = "'" & R!_H1.ToString

                    'ลาพักร้อน
                    .Cells(i, 11) = "" & CInt("0" & R!LeaveH.ToString)
                    .Cells(i, 12) = "" & CInt("0" & R!D98.ToString)
                    .Cells(i, 13) = "'" & R!_H98.ToString

                    'ลาคลอด
                    .Cells(i, 14) = "" & CInt("0" & R!LeaveM.ToString)
                    .Cells(i, 15) = "" & CInt("0" & R!D97.ToString)
                    .Cells(i, 16) = "'" & R!_H97.ToString

                    'รวม Plan
                    .Cells(i, 17) = "" & CInt("0" & R!PQtyPeople.ToString)
                    .Cells(i, 18) = "" & CInt("0" & R!PQtyDay.ToString)
                    .Cells(i, 19) = "'" & R!PQtyHour.ToString

                    'ลาป่วย
                    .Cells(i, 20) = "" & CInt("0" & R!LeaveA.ToString)
                    .Cells(i, 21) = "" & CInt("0" & R!D0.ToString)
                    .Cells(i, 22) = "'" & R!_H0.ToString

                    'ขาด 
                    .Cells(i, 23) = "" & CInt("0" & R!AbEmpQty.ToString)
                    .Cells(i, 24) = "" & CInt("0" & R!AbsentDay.ToString)
                    .Cells(i, 25) = "'" & R!AbsentHour.ToString


                    'รวม
                    .Cells(i, 27) = "" & CInt("0" & R!UQtyDay.ToString)
                    .Cells(i, 28) = "'" & R!UQtyHour.ToString

                    'สาย
                    .Cells(i, 32) = "" & CInt("0" & R!EmpLateQty.ToString)
                    .Cells(i, 33) = "" & CInt("0" & R!FNLateNormalMin.ToString)



                End With

            Next

            Dim x As Integer = i
            With xlWorkSheet
                i += +1

                .Cells(i, 1) = "'รวม"
                .Cells(i, 4).Formula = "=SUM(D6:D" & x.ToString & ")"

                .Cells(i, 5).Formula = "=SUM(E6:E" & x.ToString & ")"
                .Cells(i, 6).Formula = "=AVERAGE(F6:F" & x.ToString & ")"
                .Cells(i, 7).Formula = "=SUM(G6:G" & x.ToString & ")"
                .Cells(i, 8).Formula = "=SUM(H6:H" & x.ToString & ")"
                .Cells(i, 9).Formula = "=SUM(I6:I" & x.ToString & ")"

                .Cells(i, 11).Formula = "=SUM(K6:K" & x.ToString & ")"
                .Cells(i, 12).Formula = "=SUM(L6:L" & x.ToString & ")"
                .Cells(i, 14).Formula = "=SUM(N6:N" & x.ToString & ")"
                .Cells(i, 15).Formula = "=SUM(O6:O" & x.ToString & ")"
                .Cells(i, 17).Formula = "=SUM(Q6:Q" & x.ToString & ")"
                .Cells(i, 18).Formula = "=SUM(R6:R" & x.ToString & ")"

                .Cells(i, 20).Formula = "=SUM(T6:T" & x.ToString & ")"
                .Cells(i, 21).Formula = "=SUM(U6:U" & x.ToString & ")"

                .Cells(i, 23).Formula = "=SUM(W6:W" & x.ToString & ")"
                .Cells(i, 24).Formula = "=SUM(X6:X" & x.ToString & ")"

                .Cells(i, 26).Formula = "=SUM(Z6:Z" & x.ToString & ")"

                .Cells(i, 27).Formula = "=SUM(AA6:AA" & x.ToString & ")"
                .Cells(i, 29).Formula = "=SUM(AC6:AC" & x.ToString & ")"
                .Cells(i, 30).Formula = "=SUM(AD6:AD" & x.ToString & ")"

                .Cells(i, 31).Formula = "=AVERAGE(AE6:AE" & x.ToString & ")"
                .Cells(i, 32).Formula = "=SUM(AF6:AF" & x.ToString & ")"
                .Cells(i, 33).Formula = "=SUM(AG6:AG" & x.ToString & ")"
                .Cells(i, 34).Formula = "=SUM(E" & i.ToString & "*100)/F" & i.ToString & ""
                .Cells(i, 35).Formula = "=(I" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                .Cells(i, 36).Formula = "=SUM(L" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                .Cells(i, 37).Formula = "=(O" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                .Cells(i, 38).Formula = "=(R" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                .Cells(i, 39).Formula = "=(U" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                .Cells(i, 40).Formula = "=(U" & i.ToString & "*100)/(I" & i.ToString & "*25)"
                .Cells(i, 41).Formula = "=(AA" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                .Cells(i, 42).Formula = " =SUM(R" & i.ToString & ",AA" & i.ToString & ")*100/(F" & i.ToString & "*25)"


                .Range("B6:B" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow
                .Range("C6:C" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow

                .Range("G6:G" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow

                .Range("AD6:AD" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow
                .Range("AC6:AC" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow


            End With




            xlWorkSheet.SaveAs(_FileName, ReadOnlyRecommended:=True)
            Process.Start(_FileName)


            xlWorkBook.Close()
            xlApp.Quit()


            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)
        Catch ex As Exception

        End Try
    End Sub


    Private Sub ExportExcel()
        Try
            Dim _Qry As String = ""
            Dim _oDt As System.Data.DataTable
            Dim _oDtM As System.Data.DataTable





            _Qry = " SELECT     FNHSysUnitSectId, FTUnitSectCode "
            _Qry &= vbCrLf & "FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.TCNMUnitSect WITH (NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNHSysUnitSectId <> 0"
            '***Unit Sect***
            If (Me.Condition.otpunitsect.PageVisible) Then
                Select Case Condition.FNUnitSectCon.SelectedIndex
                    Case 1
                        If Me.Condition.FNHSysUnitSectId.Text <> "" Then

                            _Qry &= vbCrLf & "AND    Isnull(FTUnitSectCode,'')  >='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysUnitSectId.Text) & "' "
                        End If
                        If Me.Condition.FNHSysUnitSectIdTo.Text <> "" Then

                            _Qry &= vbCrLf & "AND    Isnull(FTUnitSectCode,'')  <='" & HI.UL.ULF.rpQuoted(Me.Condition.FNHSysUnitSectIdTo.Text) & "' "
                        End If
                    Case 2
                        Dim tText = ""

                        For Each oRow As DataRow In Me.Condition.DbDtUnitSect.Rows
                            tText &= oRow("FTCode") & "|"
                        Next

                        If tText.Trim <> "" Then
                            tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)

                            _Qry &= vbCrLf & "AND    Isnull(FTUnitSectCode,'')   IN('" & tText.Replace("|", "','") & "') "
                        End If

                    Case Else
                End Select
            End If


            _oDtM = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            '*******************************
            For Each H As DataRow In _oDtM.Rows


                _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.[SP_REPORT_STATISTIC_HR] '" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


                'Dim _Mount As String = ""

                'For Each row As DataRow In _oDt.Rows
                '    _Mount = "" & Mid(HI.UL.ULDate.ConvertEnDB(row!FTDateTrans.ToString), 1, 7)
                '    Exit For
                'Next

                Dim xlApp As Microsoft.Office.Interop.Excel.Application
                Dim xlWorkBook As Workbook
                Dim xlWorkSheet As Worksheet
                Dim misValue As Object = System.Reflection.Missing.Value

                Dim _Path As String = System.Windows.Forms.Application.StartupPath.ToString

                Dim oExcel As Object
                oExcel = CreateObject("Excel.Application")
                oExcel.Workbooks.Open(_Path & "\Reports\ExcelRptHRStatistic.xlsx")

                Dim oBook As Workbook
                Dim oSheet As Worksheet
                oBook = oExcel.ActiveWorkbook
                oSheet = oExcel.Worksheets(1)



                xlApp = New Microsoft.Office.Interop.Excel.Application
                xlWorkBook = xlApp.Workbooks.Add(misValue)

                'xlWorkBook.LoadFromFile("E:\PROJECT\HI SOFT\HI\bin\Debug\Reports\ReportHRStatistic.xls")




                xlWorkSheet = oSheet


                Dim i As Integer = 5

                For Each R As DataRow In _oDt.Select("LEFT(FTDateTrans,7) =' '")
                    i += +1

                    With xlWorkSheet
                        'Date
                        .Cells(i, 1) = "'" & Mid(R!FTDateTrans.ToString, 9, 2) + "/" + Mid(R!FTDateTrans.ToString, 6, 2) + "/" + Mid(R!FTDateTrans.ToString, 1, 4)

                        'พนักงานเข้าใหม่
                        .Cells(i, 4) = "" & CInt("0" & R!QtyStart.ToString)

                        'ลาออก
                        .Cells(i, 5) = "" & CInt("0" & R!QtyEnd.ToString)


                        'ลากิจ
                        .Cells(i, 8) = "" & CInt("0" & R!LeaveV.ToString)
                        .Cells(i, 9) = "" & CInt("0" & R!D1.ToString)
                        .Cells(i, 10) = "'" & R!_H1.ToString

                        'ลาพักร้อน
                        .Cells(i, 11) = "" & CInt("0" & R!LeaveH.ToString)
                        .Cells(i, 12) = "" & CInt("0" & R!D98.ToString)
                        .Cells(i, 13) = "'" & R!_H98.ToString

                        'ลาคลอด
                        .Cells(i, 14) = "" & CInt("0" & R!LeaveM.ToString)
                        .Cells(i, 15) = "" & CInt("0" & R!D97.ToString)
                        .Cells(i, 16) = "'" & R!_H97.ToString

                        'รวม Plan
                        .Cells(i, 17) = "" & CInt("0" & R!PQtyPeople.ToString)
                        .Cells(i, 18) = "" & CInt("0" & R!PQtyDay.ToString)
                        .Cells(i, 19) = "'" & R!PQtyHour.ToString

                        'ลาป่วย
                        .Cells(i, 20) = "" & CInt("0" & R!LeaveA.ToString)
                        .Cells(i, 21) = "" & CInt("0" & R!D0.ToString)
                        .Cells(i, 22) = "'" & R!_H0.ToString

                        'ขาด 
                        .Cells(i, 23) = "" & CInt("0" & R!AbEmpQty.ToString)
                        .Cells(i, 24) = "" & CInt("0" & R!AbsentDay.ToString)
                        .Cells(i, 25) = "'" & R!AbsentHour.ToString


                        'รวม
                        .Cells(i, 27) = "" & CInt("0" & R!UQtyDay.ToString)
                        .Cells(i, 28) = "'" & R!UQtyHour.ToString

                        'สาย
                        .Cells(i, 32) = "" & CInt("0" & R!EmpLateQty.ToString)
                        .Cells(i, 33) = "" & CInt("0" & R!FNLateNormalMin.ToString)



                    End With

                Next

                Dim x As Integer = i
                With xlWorkSheet
                    i += +1

                    .Cells(i, 1) = "'รวม"
                    .Cells(i, 4).Formula = "=SUM(D6:D" & x.ToString & ")"

                    .Cells(i, 5).Formula = "=SUM(E6:E" & x.ToString & ")"
                    .Cells(i, 6).Formula = "=AVERAGE(F6:F" & x.ToString & ")"
                    .Cells(i, 7).Formula = "=SUM(G6:G" & x.ToString & ")"
                    .Cells(i, 8).Formula = "=SUM(H6:H" & x.ToString & ")"
                    .Cells(i, 9).Formula = "=SUM(I6:I" & x.ToString & ")"

                    .Cells(i, 11).Formula = "=SUM(K6:K" & x.ToString & ")"
                    .Cells(i, 12).Formula = "=SUM(L6:L" & x.ToString & ")"
                    .Cells(i, 14).Formula = "=SUM(N6:N" & x.ToString & ")"
                    .Cells(i, 15).Formula = "=SUM(O6:O" & x.ToString & ")"
                    .Cells(i, 17).Formula = "=SUM(Q6:Q" & x.ToString & ")"
                    .Cells(i, 18).Formula = "=SUM(R6:R" & x.ToString & ")"

                    .Cells(i, 20).Formula = "=SUM(T6:T" & x.ToString & ")"
                    .Cells(i, 21).Formula = "=SUM(U6:U" & x.ToString & ")"

                    .Cells(i, 23).Formula = "=SUM(W6:W" & x.ToString & ")"
                    .Cells(i, 24).Formula = "=SUM(X6:X" & x.ToString & ")"

                    .Cells(i, 26).Formula = "=SUM(Z6:Z" & x.ToString & ")"

                    .Cells(i, 27).Formula = "=SUM(AA6:AA" & x.ToString & ")"
                    .Cells(i, 29).Formula = "=SUM(AC6:AC" & x.ToString & ")"
                    .Cells(i, 30).Formula = "=SUM(AD6:AD" & x.ToString & ")"

                    .Cells(i, 31).Formula = "=AVERAGE(AE6:AE" & x.ToString & ")"
                    .Cells(i, 32).Formula = "=SUM(AF6:AF" & x.ToString & ")"
                    .Cells(i, 33).Formula = "=SUM(AG6:AG" & x.ToString & ")"
                    .Cells(i, 34).Formula = "=SUM(E" & i.ToString & "*100)/F" & i.ToString & ""
                    .Cells(i, 35).Formula = "=(I" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                    .Cells(i, 36).Formula = "=SUM(L" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                    .Cells(i, 37).Formula = "=(O" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                    .Cells(i, 38).Formula = "=(R" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                    .Cells(i, 39).Formula = "=(U" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                    .Cells(i, 40).Formula = "=(U" & i.ToString & "*100)/(I" & i.ToString & "*25)"
                    .Cells(i, 41).Formula = "=(AA" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                    .Cells(i, 42).Formula = " =SUM(R" & i.ToString & ",AA" & i.ToString & ")*100/(F" & i.ToString & "*25)"


                    .Range("B6:B" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow
                    .Range("C6:C" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow

                    .Range("G6:G" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow

                    .Range("AD:AD" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow
                    .Range("AC6:AC" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow


                End With




                xlWorkSheet.SaveAs(_FileName, ReadOnlyRecommended:=True)
                Process.Start(_FileName)


                xlWorkBook.Close()
                xlApp.Quit()


                releaseObject(xlApp)
                releaseObject(xlWorkBook)
                releaseObject(xlWorkSheet)
            Next
            '*******************************



           





        Catch ex As Exception
        End Try
    End Sub

 


    Private Sub NewExportExcel()
        Try


            Dim _Qry As String = ""
            Dim _oDt As System.Data.DataTable
            _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.[SP_REPORT_STATISTIC_HR] '" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


            Dim _Mount As String = ""

            For Each row As DataRow In _oDt.Rows
                _Mount = "" & Mid(HI.UL.ULDate.ConvertEnDB(row!FTDateTrans.ToString), 1, 7)
                Exit For
            Next

            Dim xlApp As Microsoft.Office.Interop.Excel.Application
            Dim xlWorkBook As Workbook
            Dim xlWorkSheet As Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value

            Dim _Path As String = System.Windows.Forms.Application.StartupPath.ToString

            Dim oExcel As Object
            oExcel = CreateObject("Excel.Application")
            oExcel.Workbooks.Open(_Path & "\Reports\ExcelRptHRStatistic.xlsx")

            Dim oBook As Workbook
            Dim oSheet As Worksheet
            oBook = oExcel.ActiveWorkbook
            oSheet = oExcel.Worksheets(1)



            xlApp = New Microsoft.Office.Interop.Excel.Application
            xlWorkBook = xlApp.Workbooks.Add(misValue)

            'xlWorkBook.LoadFromFile("E:\PROJECT\HI SOFT\HI\bin\Debug\Reports\ReportHRStatistic.xls")




            xlWorkSheet = oSheet


            Dim i As Integer = 5

            For Each R As DataRow In _oDt.Select("LEFT(FTDateTrans,7) ='" & _Mount & "'")
                i += +1

                With xlWorkSheet
                    'Date
                    .Cells(i, 1) = "'" & Mid(R!FTDateTrans.ToString, 9, 2) + "/" + Mid(R!FTDateTrans.ToString, 6, 2) + "/" + Mid(R!FTDateTrans.ToString, 1, 4)

                    'พนักงานเข้าใหม่
                    .Cells(i, 4) = "" & CInt("0" & R!QtyStart.ToString)

                    'ลาออก
                    .Cells(i, 5) = "" & CInt("0" & R!QtyEnd.ToString)


                    'ลากิจ
                    .Cells(i, 8) = "" & CInt("0" & R!LeaveV.ToString)
                    .Cells(i, 9) = "" & CInt("0" & R!D1.ToString)
                    .Cells(i, 10) = "'" & R!_H1.ToString

                    'ลาพักร้อน
                    .Cells(i, 11) = "" & CInt("0" & R!LeaveH.ToString)
                    .Cells(i, 12) = "" & CInt("0" & R!D98.ToString)
                    .Cells(i, 13) = "'" & R!_H98.ToString

                    'ลาคลอด
                    .Cells(i, 14) = "" & CInt("0" & R!LeaveM.ToString)
                    .Cells(i, 15) = "" & CInt("0" & R!D97.ToString)
                    .Cells(i, 16) = "'" & R!_H97.ToString

                    'รวม Plan
                    .Cells(i, 17) = "" & CInt("0" & R!PQtyPeople.ToString)
                    .Cells(i, 18) = "" & CInt("0" & R!PQtyDay.ToString)
                    .Cells(i, 19) = "'" & R!PQtyHour.ToString

                    'ลาป่วย
                    .Cells(i, 20) = "" & CInt("0" & R!LeaveA.ToString)
                    .Cells(i, 21) = "" & CInt("0" & R!D0.ToString)
                    .Cells(i, 22) = "'" & R!_H0.ToString

                    'ขาด 
                    .Cells(i, 23) = "" & CInt("0" & R!AbEmpQty.ToString)
                    .Cells(i, 24) = "" & CInt("0" & R!AbsentDay.ToString)
                    .Cells(i, 25) = "'" & R!AbsentHour.ToString


                    'รวม
                    .Cells(i, 27) = "" & CInt("0" & R!UQtyDay.ToString)
                    .Cells(i, 28) = "'" & R!UQtyHour.ToString

                    'สาย
                    .Cells(i, 32) = "" & CInt("0" & R!EmpLateQty.ToString)
                    .Cells(i, 33) = "" & CInt("0" & R!FNLateNormalMin.ToString)



                End With

            Next

            Dim x As Integer = i
            With xlWorkSheet
                i += +1

                .Cells(i, 1) = "'รวม"
                .Cells(i, 4).Formula = "=SUM(D6:D" & x.ToString & ")"

                .Cells(i, 5).Formula = "=SUM(E6:E" & x.ToString & ")"
                .Cells(i, 6).Formula = "=AVERAGE(F6:F" & x.ToString & ")"
                .Cells(i, 7).Formula = "=SUM(G6:G" & x.ToString & ")"
                .Cells(i, 8).Formula = "=SUM(H6:H" & x.ToString & ")"
                .Cells(i, 9).Formula = "=SUM(I6:I" & x.ToString & ")"

                .Cells(i, 11).Formula = "=SUM(K6:K" & x.ToString & ")"
                .Cells(i, 12).Formula = "=SUM(L6:L" & x.ToString & ")"
                .Cells(i, 14).Formula = "=SUM(N6:N" & x.ToString & ")"
                .Cells(i, 15).Formula = "=SUM(O6:O" & x.ToString & ")"
                .Cells(i, 17).Formula = "=SUM(Q6:Q" & x.ToString & ")"
                .Cells(i, 18).Formula = "=SUM(R6:R" & x.ToString & ")"

                .Cells(i, 20).Formula = "=SUM(T6:T" & x.ToString & ")"
                .Cells(i, 21).Formula = "=SUM(U6:U" & x.ToString & ")"

                .Cells(i, 23).Formula = "=SUM(W6:W" & x.ToString & ")"
                .Cells(i, 24).Formula = "=SUM(X6:X" & x.ToString & ")"

                .Cells(i, 26).Formula = "=SUM(Z6:Z" & x.ToString & ")"

                .Cells(i, 27).Formula = "=SUM(AA6:AA" & x.ToString & ")"
                .Cells(i, 29).Formula = "=SUM(AC6:AC" & x.ToString & ")"
                .Cells(i, 30).Formula = "=SUM(AD6:AD" & x.ToString & ")"

                .Cells(i, 31).Formula = "=AVERAGE(AE6:AE" & x.ToString & ")"
                .Cells(i, 32).Formula = "=SUM(AF6:AF" & x.ToString & ")"
                .Cells(i, 33).Formula = "=SUM(AG6:AG" & x.ToString & ")"
                .Cells(i, 34).Formula = "=SUM(E" & i.ToString & "*100)/F" & i.ToString & ""
                .Cells(i, 35).Formula = "=(I" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                .Cells(i, 36).Formula = "=SUM(L" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                .Cells(i, 37).Formula = "=(O" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                .Cells(i, 38).Formula = "=(R" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                .Cells(i, 39).Formula = "=(U" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                .Cells(i, 40).Formula = "=(U" & i.ToString & "*100)/(I" & i.ToString & "*25)"
                .Cells(i, 41).Formula = "=(AA" & i.ToString & "*100)/(F" & i.ToString & "*25)"
                .Cells(i, 42).Formula = " =SUM(R" & i.ToString & ",AA" & i.ToString & ")*100/(F" & i.ToString & "*25)"


                .Range("B6:B" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow
                .Range("C6:C" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow

                .Range("G6:G" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow

                .Range("AD:AD" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow
                .Range("AC6:AC" & i.ToString & "").Interior.Color = XlRgbColor.rgbYellow


            End With




            xlWorkSheet.SaveAs(_FileName, ReadOnlyRecommended:=True)
            Process.Start(_FileName)


            xlWorkBook.Close()
            xlApp.Quit()


            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)





        Catch ex As Exception
        End Try
    End Sub


    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub


    Private Sub wReportHRTrans_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If FNReportname.Properties.Items.Count < 0 Then
                MsgBox("ไม่พบการกำหนด File Report !!!")
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class