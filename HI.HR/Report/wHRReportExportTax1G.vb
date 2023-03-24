Imports System.Data.SqlClient
Imports System.IO
Imports System.Text

Public Class wHRReportExportTax1G

    Private _LstReport As HI.RP.ListReport
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

        '_Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        '_Formular &= " {THRTPayRoll.FTPayYear}='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "' "

        '_Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        '_Formular &= " {THRMEmployee.FNHSysCmpId}=" & Val(HI.ST.SysInfo.CmpID) & " "

        '_Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        '_Formular &= " {THRTPayRoll.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

        'Dim tText As String = ""
        'tText = Condition.GetCriteria

        'If tText <> "" Then
        '    _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        '    _Formular &= "" & tText
        'End If

        Select Case FNReportname.SelectedIndex
            Case 1
                Dim _Dt As DataTable = HI.HRCAL.Social.Export(HRCAL.Social.TaxType.ภงด1ก, "", Me.FNHSysEmpTypeId.Properties.Tag.ToString, Me.FTPayYear.Text, "01", _ExFormular)

                If Not (_Dt Is Nothing) Then
                    If _Dt.Rows.Count > 0 Then
                        Dim _strBuilder As New StringBuilder()

                        For Each R As DataRow In _Dt.Rows

                            For Each C As DataColumn In _Dt.Columns
                                If Not (IsDBNull(R.Item(C))) Then
                                    _strBuilder.Append(R.Item(C).ToString())
                                End If
                            Next

                            _strBuilder.AppendLine()
                        Next

                        Dim Op As New System.Windows.Forms.SaveFileDialog
                        Op.Filter = "Text Files(*.TXT)|*.TXT"
                        Op.FileName = "Tax1G.txt"
                        Op.ShowDialog()

                        Try
                            If Op.FileName <> "" Then
                                ' File.WriteAllText(Op.FileName, "", Encoding.ASCII)
                                Dim myWriter As New IO.StreamWriter(Op.FileName, True, System.Text.Encoding.Default)
                                myWriter.WriteLine(_strBuilder.ToString())
                                myWriter.Close()
                                HI.MG.ShowMsg.mInfo("", 1005280003, Me.Text)

                            End If
                        Catch ex As Exception ': MsgBox(ex.Message)
                        End Try
                    Else
                        HI.MG.ShowMsg.mInvalidData("", 1005280002, Me.Text)
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData("", 1005280002, Me.Text)
                End If
            Case Else
                Dim _AllReportName As String = _LstReport.GetValue(FNReportname.SelectedIndex)

                If _AllReportName <> "" Then
                    Call HI.ST.Security.CreateTempEmpMaster(Condition)
                    Call HI.ST.Security.CreateTempPayroll(Condition, FTPayYear.Text)
                    If _LstReport.GetValueGenPic(FNReportname.SelectedIndex) = "1" Then
                        Call HI.HRCAL.GenTempData.GenerateEmpPicture(Condition)
                    End If

                    For Each _ReportName As String In _AllReportName.Split(",")

                        _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                        _Formular &= " {THRTPayRoll.FTPayYear}='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "' "

                        _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                        _Formular &= " {THRMEmployee.FNHSysCmpId}=" & Val(HI.ST.SysInfo.CmpID) & " "

                        If _ReportName = "SummaryIncomeTaxBB.rpt" Then
                            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                            _Formular &= " {THRTPayRoll.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        End If


                        Dim tText As String = ""
                        tText = Condition.GetCriteria

                        If tText <> "" Then
                            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                            _Formular &= "" & tText
                        End If



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