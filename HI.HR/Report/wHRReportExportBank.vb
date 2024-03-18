Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.Office.Interop.Excel

Public Class wHRReportExportBank
    Private _LstReport As HI.RP.ListReport
    Private _FileName As String = ""

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Me.Name = _SysFormName

        Condition.PrePareData()
        _LstReport = New HI.RP.ListReport(Me.Name.ToString)
        FNReportname.Properties.Items.AddRange(_LstReport.GetList)

        If FNReportname.Properties.Items.Count = 1 Then
            ogbreportname.Visible = False
            Me.Height = Me.Height - ogbreportname.Height
        End If
        FNHSysEmpTypeId_None.Visible = False
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Sub ocmpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmpreview.Click
        Dim _Formular As String = ""
        Dim _StateBank As Integer = 0
        Dim _pdt As System.Data.DataTable
        Dim _Str As String = ""
        If Me.FTPayTerm.Text = "" Or Me.FTPayYear.Text = "" Then
            HI.MG.ShowMsg.mProcessError(1005210001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
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
                Dim _Qry As String = ""


                _Qry = "SELECT TOP 1 FNPackBank "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMBankBranch WITH (NOLOCK) "
                _Qry &= vbCrLf & "  WHERE FTBankBranchCode='" & HI.UL.ULF.rpQuoted(FNHSysBankBranchId.Text) & "' "
                _Qry &= vbCrLf & " AND FNHSysBankId= " & Integer.Parse(Val(FNHSysBankId.Properties.Tag.ToString())) & "  "

                _StateBank = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))

                If _StateBank < 1 Then
                    _Qry = "SELECT TOP 1 FNPackBank "
                    _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMBank WITH (NOLOCK) "
                    _Qry &= vbCrLf & "  WHERE FTBankCode='" & HI.UL.ULF.rpQuoted(FNHSysBankId.Text) & "' "

                    _StateBank = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))
                End If

                If _StateBank < 1 Then
                    HI.MG.ShowMsg.mProcessError(1403260001, "ไม่พบรูปแบบ File การ Export ของ ธนาคาร กรุณาทำการตรวจสอบข้อมูลธนาคาร" < Me.Text)
                    Exit Sub
                End If

        End Select

        '_Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        '_Formular &= "  {THRMEmpType.FTEmpTypeCode}='" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "' "

        _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        _Formular &= " {THRTPayRoll.FTPayYear}='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "' "

        _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        _Formular &= " {THRTPayRoll.FTPayTerm}='" & HI.UL.ULF.rpQuoted(Me.FTPayTerm.Text) & "' "

        '_Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        '_Formular &= " {THRTPayRoll.FNHSysBankId}=" & Val(FNHSysBankId.Properties.Tag.ToString) & " "

        _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        _Formular &= " {THRMEmployee.FNHSysCmpId}=" & Val(HI.ST.SysInfo.CmpID) & " "

        If Me.Type.Text = "M" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= "  {THRMEmpType.FTEmpTypeCode} IN ['M','N','O','M1','N1','O1','M2','N2','O2'] "
        Else
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= "  NOT({THRMEmpType.FTEmpTypeCode}  IN ['M','N','O','M1','N1','O1','M2','N2','O2'] )"
        End If

        Dim tText As String = ""
        tText = Condition.GetCriteria

        If tText <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= "" & tText
        End If

        Dim _FNHSysEmpTypeId As String = ""

        Select Case FNReportname.SelectedIndex
            Case 1

                Dim _SqlWhere As String = " WHERE FNHSysEmpTypeId <> 0  AND FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "
                If Me.Type.Text = "M" Then
                    _SqlWhere &= IIf(_SqlWhere.Trim <> "", " AND ", "")
                    _SqlWhere &= "  FTEmpTypeCode IN('M','N','O','M1','N1','O1','M2','N2','O2') "
                Else
                    _SqlWhere &= IIf(_SqlWhere.Trim <> "", " AND ", "")
                    _SqlWhere &= "  FTEmpTypeCode NOT IN('M','N','O','M1','N1','O1','M2','N2','O2') "
                End If


                If _SqlWhere <> "" Then
                    Dim _Qry As String = ""
                    Dim _dtemptype As New System.Data.DataTable

                    _Qry = " Select FNHSysEmpTypeId"
                    _Qry &= "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS T WITH(NOLOCK) "
                    _Qry &= "   " & _SqlWhere

                    _dtemptype = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                    For Each R As DataRow In _dtemptype.Rows
                        _FNHSysEmpTypeId &= "|" & R!FNHSysEmpTypeId.ToString
                    Next

                End If


                Dim _MsgShow As String = ""
                Dim _Dt As DataTable = HI.HRCAL.Bank.Export(Me.FNHSysBankId.Text, "", _FNHSysEmpTypeId.Split("|"), Me.FTPayYear.Text, Me.FTPayTerm.Text, "", Me.FDCalDateBegin.Text, Me.FDCalDateEnd.Text, Me.FDPayDate.Text, _StateBank, _MsgShow)

                If Not (_Dt Is Nothing) Then
                    If _Dt.Rows.Count > 0 Then
                        Dim Ridx As Integer = 0
                        Dim TotalRidx As Integer = _Dt.Rows.Count
                        Dim _strBuilder As New StringBuilder()

                        For Each R As DataRow In _Dt.Rows
                            Ridx = Ridx + 1
                            For Each C As DataColumn In _Dt.Columns
                                If Not (IsDBNull(R.Item(C))) Then
                                    _strBuilder.Append(R.Item(C).ToString())
                                End If
                            Next

                            If Ridx = TotalRidx Then

                            Else
                                _strBuilder.AppendLine()
                            End If

                        Next
                        Dim strDate = ""
                        strDate = Format(Now, "yyyyMMdd")
                        Dim Op As New System.Windows.Forms.SaveFileDialog
                        Op.Filter = "Text Files(*.txt)|*.txt"
                        Op.FileName = "ExportToBank" + strDate + ".txt"
                        Op.ShowDialog()

                        Try

                            If Op.FileName <> "" Then

                                Dim myWriter As New IO.StreamWriter(Op.FileName, False, System.Text.Encoding.Default)
                                myWriter.WriteLine(_strBuilder.ToString())
                                myWriter.Close()
                                HI.MG.ShowMsg.mInfo("Export Complete !!!", 1005280005, Me.Text, _MsgShow, System.Windows.Forms.MessageBoxIcon.Information)

                            End If

                        Catch ex As Exception
                        End Try
                    Else
                        HI.MG.ShowMsg.mInvalidData("", 1005280004, Me.Text)
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData("", 1005280004, Me.Text)
                End If

            Case 2  'Data file For TMB

                'Dim _SqlWhere As String = " WHERE FNHSysEmpTypeId <> 0   AND FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "
                'Select Case Condition.FNEmpTypeCon.SelectedIndex
                '    Case 1

                '        If Condition.FNHSysEmpTypeId.Text <> "" Then
                '            _SqlWhere &= IIf(_SqlWhere.Trim <> "", " AND ", "")
                '            _SqlWhere &= " FTEmpTypeCode>='" & HI.UL.ULF.rpQuoted(Condition.FNHSysEmpTypeId.Text) & "' "
                '        End If

                '        If Condition.FNHSysEmpTypeIdTo.Text <> "" Then
                '            _SqlWhere &= IIf(_SqlWhere.Trim <> "", " AND ", "")
                '            _SqlWhere &= " FTEmpTypeCode <='" & HI.UL.ULF.rpQuoted(Condition.FNHSysEmpTypeIdTo.Text) & "' "
                '        End If

                '    Case 2
                '        tText = ""

                '        For Each oRow As DataRow In Condition.DbDtEmployeeType.Rows
                '            tText &= oRow("FTCode") & "|"
                '        Next

                '        If tText.Trim <> "" Then
                '            tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                '            _SqlWhere &= IIf(_SqlWhere.Trim <> "", " AND ", "")
                '            _SqlWhere &= "  FTEmpTypeCode IN('" & tText.Replace("|", "','") & "') "
                '        End If

                '    Case Else

                'End Select

                Dim _SqlWhere As String = " WHERE FNHSysEmpTypeId <> 0   AND FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "
                If Me.Type.Text = "M" Then
                    _SqlWhere &= IIf(_SqlWhere.Trim <> "", " AND ", "")
                    _SqlWhere &= "  FTEmpTypeCode IN('M','N','O','M1','N1','O1','M2','N2','O2') "
                Else
                    _SqlWhere &= IIf(_SqlWhere.Trim <> "", " AND ", "")
                    _SqlWhere &= "  FTEmpTypeCode NOT IN('M','N','O','M1','N1','O1','M2','N2','O2') "
                End If



                If _SqlWhere <> "" Then
                    Dim _Qry As String = ""
                    Dim _dtemptype As New System.Data.DataTable

                    _Qry = " Select FNHSysEmpTypeId"
                    _Qry &= "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS T WITH(NOLOCK) "
                    _Qry &= "   " & _SqlWhere

                    _dtemptype = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                    For Each R As DataRow In _dtemptype.Rows
                        _FNHSysEmpTypeId &= "|" & R!FNHSysEmpTypeId.ToString
                    Next

                End If

                Dim Preriod As String = "" '6
                Dim Preriod_DDMMYY As String = "" '6
                Dim _TotalRec As String = "" '5
                Dim _TotalAmount As String = "" '11
                Dim _I As String = "I" '1
                Dim _Filler As String = "        " '8


                Dim line1 As String = ""
                Dim line2 As String = ""
                Dim line5 As String = ""
                Dim _MsgShow As String = ""

                Preriod = Me.FDPayDate.Text.Substring(6, 4) + Me.FDPayDate.Text.Substring(3, 2) + Me.FDPayDate.Text.Substring(0, 2)
                Preriod_DDMMYY = Me.FDPayDate.Text.Substring(0, 2) + Me.FDPayDate.Text.Substring(3, 2) + Me.FDPayDate.Text.Substring(8, 2)

                Dim _Dt As System.Data.DataTable = HI.EXPORTBANK.ExporForTMB41.ExportDataFile_TMB128(Me.FNHSysBankId.Text, "", _FNHSysEmpTypeId.Split("|"), Me.FTPayYear.Text, Me.FTPayTerm.Text, "", Me.FDCalDateBegin.Text, Me.FDCalDateEnd.Text, Preriod_DDMMYY, _MsgShow, _TotalRec, _TotalAmount, line1, line2, line5, Type.Text)

                If Not (_Dt Is Nothing) Then
                    If _Dt.Rows.Count > 0 Then
                        Dim Ridx As Integer = 0
                        Dim TotalRidx As Integer = _Dt.Rows.Count
                        Dim _strBuilder As New StringBuilder()


                        _strBuilder.Append(line1)
                        _strBuilder.AppendLine()

                        For Each R As DataRow In _Dt.Rows
                            Ridx = Ridx + 1

                            _strBuilder.Append(R!RECORD_TYPE.ToString() & R!SEQUENCE_NUMBER.ToString() _
                                                & R!BANK_CODE.ToString() & R!ACCOUNT_NUMBER.ToString() _
                                                & R!TRANSACTION_CODE.ToString() & R!AMOUNT.ToString() _
                                                & R!SERVICE_TYPE.ToString() & R!STATUS.ToString() _
                                                & R!REFERENCE_AREA1.ToString() & R!INSERVICE_DATE.ToString() _
                                                & R!COMPANY_CODE.ToString() & R!HOME_BRANCH.ToString() _
                                                & R!REFERENCE_AREA2.ToString() & R!TMB_FLAG.ToString() _
                                                & R!SPARE.ToString() & R!ACCOUNT_NAME.ToString())
                            _strBuilder.AppendLine()


                        Next

                        _strBuilder.Append(line5)

                        Dim Op As New System.Windows.Forms.SaveFileDialog
                        Op.Filter = "Text Files(*.txt)|*.txt"
                        Op.FileName = "ExportToBankTMB_" + HI.ST.SysInfo.CmpCode + "_" + Me.Type.Text + "_" + Preriod + ".txt"
                        Op.ShowDialog()

                        Try

                            If Op.FileName <> "" Then

                                Dim myWriter As New IO.StreamWriter(Op.FileName, False, System.Text.Encoding.Default)
                                myWriter.WriteLine(_strBuilder.ToString())
                                myWriter.Close()
                                HI.MG.ShowMsg.mInfo("Export Complete !!! TMB", 1005280005, Me.Text, _MsgShow, System.Windows.Forms.MessageBoxIcon.Information)

                            End If

                        Catch ex As Exception
                        End Try
                    Else
                        HI.MG.ShowMsg.mInvalidData("", 1005280004, Me.Text)
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData("", 1005280004, Me.Text)
                End If

                '     HI.MG.ShowMsg.mInfo("Export Complete !!! TMB ", 1005280005, Me.Text, _MsgShow, System.Windows.Forms.MessageBoxIcon.Information)


                '''''''''OLD''''''''''''''''''''
                'Dim _SqlWhere As String = " WHERE FNHSysEmpTypeId <> 0   AND FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "
                'Select Case Condition.FNEmpTypeCon.SelectedIndex
                '    Case 1

                '        If Condition.FNHSysEmpTypeId.Text <> "" Then
                '            _SqlWhere &= IIf(_SqlWhere.Trim <> "", " AND ", "")
                '            _SqlWhere &= " FTEmpTypeCode>='" & HI.UL.ULF.rpQuoted(Condition.FNHSysEmpTypeId.Text) & "' "
                '        End If

                '        If Condition.FNHSysEmpTypeIdTo.Text <> "" Then
                '            _SqlWhere &= IIf(_SqlWhere.Trim <> "", " AND ", "")
                '            _SqlWhere &= " FTEmpTypeCode <='" & HI.UL.ULF.rpQuoted(Condition.FNHSysEmpTypeIdTo.Text) & "' "
                '        End If

                '    Case 2
                '        tText = ""

                '        For Each oRow As DataRow In Condition.DbDtEmployeeType.Rows
                '            tText &= oRow("FTCode") & "|"
                '        Next

                '        If tText.Trim <> "" Then
                '            tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                '            _SqlWhere &= IIf(_SqlWhere.Trim <> "", " AND ", "")
                '            _SqlWhere &= "  FTEmpTypeCode IN('" & tText.Replace("|", "','") & "') "
                '        End If

                '    Case Else

                'End Select

                'If _SqlWhere <> "" Then
                '    Dim _Qry As String = ""
                '    Dim _dtemptype As New DataTable

                '    _Qry = " Select FNHSysEmpTypeId"
                '    _Qry &= "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS T WITH(NOLOCK) "
                '    _Qry &= "   " & _SqlWhere

                '    _dtemptype = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                '    For Each R As DataRow In _dtemptype.Rows
                '        _FNHSysEmpTypeId &= "|" & R!FNHSysEmpTypeId.ToString
                '    Next

                'End If


                'Dim _MsgShow As String = ""



                'Dim _Dt As DataTable = HI.EXPORTBANK.ExporForTMB41.ExportDataFile(Me.FNHSysBankId.Text, "", _FNHSysEmpTypeId.Split("|"), Me.FTPayYear.Text, Me.FTPayTerm.Text, "", Me.FDCalDateBegin.Text, Me.FDCalDateEnd.Text, Me.FDPayDate.Text, _MsgShow)
                Dim x As System.Data.DataTable
                x = New System.Data.DataTable


                'If Not (_Dt Is Nothing) Then
                '    If _Dt.Rows.Count > 0 Then
                '        Dim Ridx As Integer = 0
                '        Dim TotalRidx As Integer = _Dt.Rows.Count
                '        Dim _strBuilder As New StringBuilder()

                '        For Each R As DataRow In _Dt.Rows
                '            Ridx = Ridx + 1
                '            For Each C As DataColumn In _Dt.Columns
                '                If Not (IsDBNull(R.Item(C))) Then
                '                    _strBuilder.Append(R.Item(C).ToString())
                '                End If
                '            Next

                '            If Ridx = TotalRidx Then

                '            Else
                '                _strBuilder.AppendLine()
                '            End If

                '        Next

                '        Dim Op As New System.Windows.Forms.SaveFileDialog
                '        Op.Filter = "Text Files(*.txt)|*.txt"
                '        Op.FileName = "ExportToBankTMB41.txt"
                '        Op.ShowDialog()

                '        Try

                '            If Op.FileName <> "" Then

                '                Dim myWriter As New IO.StreamWriter(Op.FileName, False, System.Text.Encoding.Default)
                '                myWriter.WriteLine(_strBuilder.ToString())
                '                myWriter.Close()
                '                HI.MG.ShowMsg.mInfo("Export Complete !!! TMB", 1005280005, Me.Text, _MsgShow, System.Windows.Forms.MessageBoxIcon.Information)

                '            End If

                '        Catch ex As Exception
                '        End Try
                '    Else
                '        HI.MG.ShowMsg.mInvalidData("", 1005280004, Me.Text)
                '    End If
                'Else
                '    HI.MG.ShowMsg.mInvalidData("", 1005280004, Me.Text)
                'End If

                ''     HI.MG.ShowMsg.mInfo("Export Complete !!! TMB ", 1005280005, Me.Text, _MsgShow, System.Windows.Forms.MessageBoxIcon.Information)


            Case 3  'Data file For SCB

                Dim _Qry As String = ""
                Dim _SqlWhere As String = " WHERE FNHSysEmpTypeId <> 0   AND FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "
                If Me.Type.Text = "M" Then
                    _SqlWhere &= IIf(_SqlWhere.Trim <> "", " AND ", "")
                    _SqlWhere &= "  FTEmpTypeCode IN('M','N','O') "
                Else
                    _SqlWhere &= IIf(_SqlWhere.Trim <> "", " AND ", "")
                    _SqlWhere &= "  FTEmpTypeCode NOT IN('M','N','O') "
                End If
                'Select Case Condition.FNEmpTypeCon.SelectedIndex
                '    Case 1

                '        If Condition.FNHSysEmpTypeId.Text <> "" Then
                '            _SqlWhere &= IIf(_SqlWhere.Trim <> "", " AND ", "")
                '            _SqlWhere &= " FTEmpTypeCode>='" & HI.UL.ULF.rpQuoted(Condition.FNHSysEmpTypeId.Text) & "' "
                '        End If

                '        If Condition.FNHSysEmpTypeIdTo.Text <> "" Then
                '            _SqlWhere &= IIf(_SqlWhere.Trim <> "", " AND ", "")
                '            _SqlWhere &= " FTEmpTypeCode <='" & HI.UL.ULF.rpQuoted(Condition.FNHSysEmpTypeIdTo.Text) & "' "
                '        End If

                '    Case 2
                '        tText = ""

                '        For Each oRow As DataRow In Condition.DbDtEmployeeType.Rows
                '            tText &= oRow("FTCode") & "|"
                '        Next

                '        If tText.Trim <> "" Then
                '            tText = Microsoft.VisualBasic.Left(Trim(tText), Len(Trim(tText)) - 1)
                '            _SqlWhere &= IIf(_SqlWhere.Trim <> "", " AND ", "")
                '            _SqlWhere &= "  FTEmpTypeCode IN('" & tText.Replace("|", "','") & "') "
                '        End If

                '    Case 3




                '    Case Else

                'End Select

                If _SqlWhere <> "" Then

                    Dim _dtemptype As New System.Data.DataTable

                    _Qry = " Select FNHSysEmpTypeId"
                    _Qry &= "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS T WITH(NOLOCK) "
                    _Qry &= "   " & _SqlWhere

                    _dtemptype = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                    For Each R As DataRow In _dtemptype.Rows
                        _FNHSysEmpTypeId &= "|" & R!FNHSysEmpTypeId.ToString
                    Next

                End If


                Dim _MsgShow As String = ""

                Dim _Space As String = "        " '8
                Dim _TempPayment As String = "" '1
                Dim _ATSCode As String = "000000" '6
                Dim _CompCode As String = "CVN                           " '30
                Dim _DA As String = "DA" '2
                Dim Preriod As String = "" '6
                Dim _TotalRec As String = "" '5
                Dim _TotalAmount As String = "" '11
                Dim _I As String = "I" '1
                Dim _Filler As String = "        " '8


                Dim line1 As String = ""
                Dim line2 As String = ""
                Dim line5 As String = ""





                If Me.Type.Text = "M" Then
                    _TempPayment = "M"
                Else
                    _TempPayment = "H"
                End If

                If HI.ST.SysInfo.CmpCode = "CVN" Then

                    Preriod = Me.FDPayDate.Text.Substring(0, 2) + Me.FDPayDate.Text.Substring(3, 2) + Me.FDPayDate.Text.Substring(8, 2)
                    Dim _Dt As System.Data.DataTable = HI.EXPORTBANK.ExporForSCB.ExportDataFile(Me.FNHSysBankId.Text, "", _FNHSysEmpTypeId.Split("|"), Me.FTPayYear.Text, Me.FTPayTerm.Text, "", Me.FDCalDateBegin.Text, Me.FDCalDateEnd.Text, Preriod, _MsgShow, _TotalRec, _TotalAmount)

                    Dim _TextHeader As String = ""
                    _TotalAmount = ("00000000000" + _TotalAmount.ToString)
                    _TotalAmount = _TotalAmount.Substring(_TotalAmount.Length - 11, 11)

                    _TotalRec = ("00000" + _TotalRec.ToString)
                    _TotalRec = _TotalRec.Substring(_TotalRec.Length - 5, 5)
                    _TextHeader = _Space + _TempPayment + _ATSCode + _CompCode + _DA + Preriod + _TotalRec + _TotalAmount + _I + _Filler

                    If Not (_Dt Is Nothing) Then
                        If _Dt.Rows.Count > 0 Then
                            Dim Ridx As Integer = 0
                            Dim TotalRidx As Integer = _Dt.Rows.Count
                            Dim _strBuilder As New StringBuilder()
                            _strBuilder.Append(_TextHeader)
                            _strBuilder.AppendLine()
                            For Each R As DataRow In _Dt.Rows
                                Ridx = Ridx + 1
                                For Each C As DataColumn In _Dt.Columns
                                    If Not (IsDBNull(R.Item(C))) Then
                                        _strBuilder.Append(R.Item(C).ToString())
                                    End If
                                Next

                                If Ridx = TotalRidx Then

                                Else
                                    _strBuilder.AppendLine()
                                End If

                            Next

                            Dim Op As New System.Windows.Forms.SaveFileDialog
                            Op.Filter = "Text Files(*.txt)|*.txt"
                            Op.FileName = "ExportToBankSCB_" + _TempPayment + "_" + Preriod + ".txt"
                            Op.ShowDialog()

                            Try

                                If Op.FileName <> "" Then

                                    Dim myWriter As New IO.StreamWriter(Op.FileName, False, System.Text.Encoding.Default)
                                    myWriter.WriteLine(_strBuilder.ToString())
                                    myWriter.Close()
                                    HI.MG.ShowMsg.mInfo("Export Complete !!! SCB", 1005280005, Me.Text, _MsgShow, System.Windows.Forms.MessageBoxIcon.Information)

                                End If

                            Catch ex As Exception
                            End Try
                        Else
                            HI.MG.ShowMsg.mInvalidData("", 1005280004, Me.Text)
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData("", 1005280004, Me.Text)
                    End If




                Else
                    Dim CMD As String

                    'ProcessStartInfo info = New ProcessStartInfo("cmd.exe")
                    'Process.Start(info);



                    ''''''''''''''''''''''''
                    '''HITECH

                    'If n = 0 Then



                    Preriod = Me.FDPayDate.Text.Substring(6, 4) + Me.FDPayDate.Text.Substring(3, 2) + Me.FDPayDate.Text.Substring(0, 2)

                    Dim _Dt As System.Data.DataTable = HI.EXPORTBANK.ExporForSCB.ExportDataFile_HT(Me.FNHSysBankId.Text, "", _FNHSysEmpTypeId.Split("|"), Me.FTPayYear.Text, Me.FTPayTerm.Text, "", Me.FDCalDateBegin.Text, Me.FDCalDateEnd.Text, Preriod, _MsgShow, _TotalRec, _TotalAmount, line1, line2, line5, Type.Text)


                    If Not (_Dt Is Nothing) Then
                        If _Dt.Rows.Count > 0 Then
                            Dim Ridx As Integer = 0
                            Dim TotalRidx As Integer = _Dt.Rows.Count
                            Dim _strBuilder As New StringBuilder()
                            _strBuilder.Append(line1)
                            _strBuilder.AppendLine()
                            _strBuilder.Append(line2)
                            _strBuilder.AppendLine()
                            For Each R As DataRow In _Dt.Rows
                                Ridx = Ridx + 1

                                _strBuilder.Append(R!C3_1RecordType.ToString() & R!C3_2CreditsNumberSequence.ToString() & R!C3_4CreditAccount.ToString() & R!C3_4CreditAmount.ToString() _
                                                           & R!C3_5CreditCurrency.ToString() & R!C3_6InRef.ToString() & R!C3_7.ToString() & R!C3_8.ToString() & R!C3_9.ToString() & R!C3_10.ToString() _
                                                           & R!C3_11PickLocation.ToString() & R!C3_12WHTFromType.ToString() & R!C3_13.ToString() & R!C3_14.ToString() & R!C3_15.ToString() & R!C3_16.ToString() & R!C3_17.ToString() & R!C3_18.ToString() _
                                                           & R!C3_19.ToString() & R!C3_20WHTRemark.ToString() & R!C3_21.ToString() & R!C3_22RecBankCode.ToString() & R!C3_23.ToString() & R!C3_24.ToString() & R!C3_25.ToString() & R!C3_26.ToString() _
                                                           & R!C3_27.ToString() & R!C3_28.ToString() & R!C3_29.ToString() & R!C3_30.ToString() & R!C3_31.ToString() & R!C3_32.ToString() & R!C3_33.ToString() & R!C3_34.ToString())



                                _strBuilder.AppendLine()
                                _strBuilder.Append(R!C4_1RecordType.ToString() & R!C4_2InRef.ToString() & R!C4_3CreditsNumberSequence.ToString() & R!C4_4Payee1ID.ToString() _
                                            & R!C4_5.ToString() & R!C4_6PayeeAddr1.ToString() & R!C4_7PayeeAddr2.ToString() & R!C4_8PayeeAddr3.ToString() & R!C4_9PayeeTaxID.ToString() & R!C4_10PayeeNameEng.ToString() & R!C4_11.ToString() & R!C4_12.ToString() & R!C4_13Payee1Email.ToString() _
                                            & R!C4_14Payee2NameThai.ToString() & R!C4_15Payee2Address1.ToString() & R!C4_16Payee2Address2.ToString() & R!C4_17Payee2Address3.ToString())


                                _strBuilder.AppendLine()


                            Next
                            _strBuilder.Append(line5)

                            Dim Op As New System.Windows.Forms.SaveFileDialog
                            Op.Filter = "Text Files(*.txt)|*.txt"
                            Op.FileName = "ExportToBankSCB_" + HI.ST.SysInfo.CmpCode + "_" + _TempPayment + "_" + Preriod + ".txt"

                            Op.ShowDialog()

                            Dim pathSave As String = IO.Path.GetDirectoryName(Op.FileName)

                            Dim filePayrollTxt As String
                            Dim filePayrollHash As String
                            Dim path As String
                            Dim path_report As String

                            Dim path_hash_bat As String
                            Dim path_payrollTxt As String
                            Dim path_payrollHash As String
                            Dim path_payrollPDF As String

                            path = System.Windows.Forms.Application.StartupPath
                            filePayrollTxt = "ExportToBankSCB_" + HI.ST.SysInfo.CmpCode + "_" + _TempPayment + "_" + Preriod + ".txt"
                            filePayrollHash = "ExportToBankSCB_" + HI.ST.SysInfo.CmpCode + "_" + _TempPayment + "_" + Preriod + "_Hash.txt"
                            '   path += "\Reports\HumanExport\" + "ExportToBankSCB_" + _TempPayment + "_" + Preriod + ".txt"
                            path_report = path + "\Reports\HumanExport"

                            path_payrollTxt = path_report + "\" + filePayrollTxt


                            path_hash_bat = "C:\HASHMODULE\FORWINDOW\SCBHashProgram.bat"
                            pathSave = "C:\SCB\" + filePayrollTxt
                            path_payrollHash = "C:\SCB\OUT\" + filePayrollHash
                            path_payrollPDF = "C:\SCB\PDF"

                            CMD = "/C " + path_hash_bat + " " + pathSave + " " + path_payrollHash + " " + path_payrollPDF


                            ''Dim path As String = IO.Path.GetDirectoryName(SaveFileDialog.FileName)   sss
                            ''  CMD = "C:\>HASHMODULE\FORWINDOW\SCBHashProgram.bat C:\SCB\ExportToBankSCB_H_20210305.txt C:\SCB\OUT\aa_hash.txt C:\SCB\PDF"

                            Try

                                If Op.FileName <> "" Then

                                    Dim myWriter As New IO.StreamWriter(Op.FileName, False, Encoding.Default)

                                    myWriter.WriteLine(_strBuilder.ToString())
                                    myWriter.Close()


                                End If

                                'If System.IO.File.Exists(path_payrollTxt) = False Then
                                '    Dim objWriter As New System.IO.StreamWriter(path_payrollTxt, True)
                                '    objWriter.WriteLine(_strBuilder.ToString())
                                '    objWriter.Close()
                                'End If




                                'Shell("cmd.exe", CMD)
                                'Shell(CMD)
                                'Shell("cmd.exe /c cd C:\ & adb shell monkey -p com.android.system -v 1")
                                'Shell("cmd.exe /c cd C:\HASHMODULE\FORWINDOW\SCBHashProgram.bat C:\SCB\ExportToBankSCB_H_20210305.txt C:\SCB\OUT\miaa_hash.txt C:\SCB\PDF")
                                'Shell("cmd.exe cd /c  c: \d /k  cd C:\HASHMODULE\FORWINDOW\SCBHashProgram.bat C:\SCB\ExportToBankSCB_H_20210305.txt C:\SCB\OUT\aa_hash.txt C:\SCB\PDF")
                                '' Shell("cmd.exe   cd /k  c: /k ")
                                ' Shell("cmd.exe /c", AppWinStyle.Hide)
                                'Shell(CMD)

                                Dim startInfo As New ProcessStartInfo("cmd.exe")
                                startInfo.WindowStyle = ProcessWindowStyle.Normal
                                'startInfo.Arguments = "/C C:\HASHMODULE\FORWINDOW\SCBHashProgram.bat C:\SCB\ExportToBankSCB_H_20210305.txt C:\SCB\OUT\pay_hash.txt C:\SCB\PDF"

                                startInfo.Arguments = CMD
                                startInfo.RedirectStandardOutput = True
                                startInfo.UseShellExecute = False
                                ' startInfo.CreateNoWindow = True
                                Dim p = Process.Start(startInfo)
                                p.WaitForExit(999999)

                                Dim result = p.StandardOutput.ReadToEnd()

                                p.Close()

                            Catch ex As Exception
                                MsgBox(ex.Message.ToString())
                            End Try

                        Else
                            HI.MG.ShowMsg.mInvalidData("", 1005280004, Me.Text)
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData("", 1005280004, Me.Text)
                    End If
                    'Else
                    '    HI.MG.ShowMsg.mInfo("Can not Export SCB. Employee Account No or Bank Branch has not found.", 1005280005, Me.Text, _strName, System.Windows.Forms.MessageBoxIcon.Information)

                    'End If

                End If
            Case 4  '' LDB bank hitech_loas


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
                    Call HI.ST.Security.CreateTempPayroll(Condition, FTPayYear.Text, FTPayTerm.Text)


                    Try

                        If _LstReport.GetValueGenPic(FNReportname.SelectedIndex) = "1" Then
                            Call HI.HRCAL.GenTempData.GenerateEmpPicture(Condition)
                        End If

                        For Each _ReportName As String In _AllReportName.Split(",")

                            With New HI.RP.Report
                                'If ((HI.ST.SysInfo.Admin)) Then
                                '    .AddParameter("FNStateSalary", "1")
                                'Else
                                '    _Str = "Select U.FNHSysPermissionID ,T.FNHSysEmpTypeId ,T.FTStateSalary"
                                '    _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission As U With(NOLOCK) LEFT OUTER JOIN"
                                '    _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType As T With(NOLOCK) On U.FNHSysPermissionID = T.FNHSysPermissionID"
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
                    Catch ex As Exception
                        MsgBox(ex.Message.ToString)
                    End Try


                Else
                    HI.MG.ShowMsg.mProcessError(1005170001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                End If

        End Select
    End Sub



    Private Sub ExportExCel(_Spls As HI.TL.SplashScreen)
        Try
            Dim tText As String = ""
            Dim _Cmd As String = ""
            _Cmd = "SELECT   ROW_NUMBER() over(order by  E.FTAccNo asc) AS FNSeq " 'P.FTPreNameNameEN + ' ' +
            '' _Cmd &= vbCrLf & " , P.FTPreNameNameEN+' '+  E.FTEmpNameEN+ ' ' + E.FTEmpSurnameEN  AS Name"
            _Cmd &= vbCrLf & " ,UPPER(CAST( REPLACE(P.FTPreNameNameEN,'.','') as  nvarchar(10))+' '+  E.FTEmpNameEN+ ' ' +  E.FTEmpSurnameEN )AS 'Name' "
            _Cmd &= vbCrLf & " , E.FTAccNo, E.FTEmpIdNo, convert(numeric(18,0) ,  R.FNNetpay ) as FNNetpay, R.FTPayYear, R.FTPayTerm, R.FNHSysBankId,"
            _Cmd &= vbCrLf & "      R.FNHSysBankBranchId, R.FNHSysEmpTypeId, R.FNHSysDeptId, R.FNHSysDivisonId, R.FNHSysSectId, R.FNHSysUnitSectId ,DT.FNMonth AS FTMonth --Right(LEFT(DT.FDCalDateBegin,7),2) AS FTMonth"
            _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS R LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS DT ON R.FTPayTerm = DT.FTPayTerm AND R.FTPayYear = DT.FTPayYear and R.FNHSysEmpTypeId=DT.FNHSysEmpTypeId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E ON R.FNHSysEmpID = E.FNHSysEmpID LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MEmpType AS T ON R.FNHSysEmpTypeId = T.FNHSysEmpTypeId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MDepartment AS D ON R.FNHSysDeptId = D.FNHSysDeptId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MDivision AS S ON R.FNHSysDivisonId = S.FNHSysDivisonId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MSect AS C ON R.FNHSysSectId = C.FNHSysSectId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MUnitSect AS F ON R.FNHSysUnitSectId = F.FNHSysUnitSectId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MPrename AS P ON E.FNHSysPreNameId = P.FNHSysPreNameId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MCmp AS N ON E.FNHSysCmpId = N.FNHSysCmpId"


            _Cmd &= vbCrLf & "WHERE R.FTPayYear='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "'"
            _Cmd &= vbCrLf & " and R.FTPayTerm='" & HI.UL.ULF.rpQuoted(Me.FTPayTerm.Text) & "'"
            _Cmd &= vbCrLf & " and R.FNHSysBankId=" & Integer.Parse(Val(Me.FNHSysBankId.Properties.Tag)) & ""
            '_Cmd &= vbCrLf & " and R.FNHSysBankBranchId=" & Integer.Parse(Val(Me.FNHSysBankBranchId.Properties.Tag)) & ""
            ' _Cmd &= vbCrLf & " and isnull( R.FNNetpay ,0) > 0 "

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

            _Cmd &= vbCrLf & "Group by  E.FNHSysEmpID ,P.FTPreNameNameEN , E.FTEmpNameEN ,E.FTEmpSurnameEN, E.FTAccNo, E.FTEmpIdNo, R.FNNetpay, R.FTPayYear, R.FTPayTerm, R.FNHSysBankId, "
            _Cmd &= vbCrLf & "      R.FNHSysBankBranchId, R.FNHSysEmpTypeId, R.FNHSysDeptId, R.FNHSysDivisonId, R.FNHSysSectId, R.FNHSysUnitSectId ,DT.FNMonth"
            _Cmd &= vbCrLf & " ORDER BY E.FTAccNo ASC  "
            Dim _oDt As System.Data.DataTable
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR)


            'Call NewExcel(_oDt, _Spls)
            Call NewExcelNew_BankForm(_oDt, _Spls)
        Catch ex As Exception
            'MsgBox("Error Step1" & ex.Message.ToString)
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
            Dim _Path As String = System.Windows.Forms.Application.StartupPath.ToString
            Dim TmpFile As String = _Path & "\Reports\TmpPayRollToBank_Loas.xlsx"
            Dim oExcel As New Microsoft.Office.Interop.Excel.Application
            Dim xlBookTmp As Workbook
            Dim Excel As New Microsoft.Office.Interop.Excel.XlDirection

            xlBookTmp = oExcel.Workbooks.Open(TmpFile)

            'xlBookTmp.Worksheets(1).copy(After:=xlBookTmp.Worksheets(1))
            'xlBookTmp.Worksheets(2).copy(After:=xlBookTmp.Worksheets(2))
            Dim _date As String = HI.Conn.SQLConn.GetField("Select convert(varchar(10) ,  getdate() ,103) as Date", Conn.DB.DataBaseName.DB_HR)
            Dim i As Integer = 1
            Dim s As Integer = 13
            Dim _nettotal As String = Format(_oDt.Compute("SUM(FNNetpay)", "FNNetpay>0"), "##,###").ToString
            Dim _nettotalKip As String = HI.UL.ULF.Convert_Bath_LA(_oDt.Compute("SUM(FNNetpay)", "FNNetpay>0"))
            With xlBookTmp.Worksheets(2)
                .Cells(11, 1) = "ຊື່ບັນຊີ ໄຮ-ເທັກລາວ ແອບພາເຣວ ຈຳກັດ.  ເລກບັນຊີ  =>   0302000010004449  ຈຳນວນເງິນ    " & _nettotal & " ກີບ."
                .Cells(12, 1) = "(" & _nettotalKip & " ) ເພື່ອໂອນເຂົ້າບັນຊີຂອງພະນັກງານຕາມລາຍລະອຽດດັ່ງລຸ່ມນີ້:"
                .Cells(5, 4) = "ວັນທີ : " & _date
            End With

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
                    .Cells(i, 2) = "" & R!Name.ToString
                    .Cells(i, 3).Font.Color = 0
                    .Cells(i, 3) = "" & R!FTAccNo.ToString
                    '.Cells(i, 4).Font.Color = 0
                    '.Cells(i, 4) = "'" & R!FTEmpIdNo.ToString
                    .Cells(i, 4).Font.Color = 0
                    .Cells(i, 4) = R!FNNetpay.ToString
                    '.Cells(i, 4).NumberFormat = "#,###,###"
                End With


                With xlBookTmp.Worksheets(2)
                    'Date
                    .Rows(CStr(s) & ":" & CStr(s)).Insert(Shift:=XlDirection.xlDown)
                    .Cells(s, 1).Font.Color = 0
                    .Cells(s, 1) = "" & R!FNSeq.ToString
                    .Cells(s, 2).Font.Color = 0
                    .Cells(s, 2) = "" & R!Name.ToString
                    .Cells(s, 2).HorizontalAlignment = XlHAlign.xlHAlignLeft
                    .Cells(s, 3).Font.Color = 0
                    .Cells(s, 3) = "" & R!FTAccNo.ToString
                    '.Cells(i, 4).Font.Color = 0
                    '.Cells(i, 4) = "'" & R!FTEmpIdNo.ToString
                    .Cells(s, 4).NumberFormat = "#,###,###"
                    .Cells(s, 4).Font.Color = 0
                    .Cells(s, 4) = R!FNNetpay.ToString
                    .Cells(s, 4).HorizontalAlignment = XlHAlign.xlHAlignRight
                    .Cells(s, 4).NumberFormat = "#,###,###"
                End With



            Next
            i += +2
            s += +2
            With xlBookTmp.Worksheets(2)
                .Cells(s, 3) = "TOTAL"
                .Cells(s, 3).Font.Bold = True
                .Cells(s, 3).HorizontalAlignment = XlHAlign.xlHAlignCenter
                .Cells(s, 4) = _nettotal
                .Cells(s, 4).HorizontalAlignment = XlHAlign.xlHAlignRight
                .Cells(s, 4).Font.Bold = True
            End With
            s += +2
            With xlBookTmp.Worksheets(2)

                .Cells(s, 2) = "ຜູ້ອຳນວຍການ"
                .Cells(s, 2).HorizontalAlignment = XlHAlign.xlHAlignCenter

                .Cells(s, 4) = "ຜູ້ສະຫຼຸບ"
                .Cells(s, 4).HorizontalAlignment = XlHAlign.xlHAlignCenter
            End With



            'With xlBookTmp.Worksheets(1)
            '    If _oDt.Rows.Count > 0 Then
            '        .Cells(i, 2) = "Duy Xuyên, ngày/date  …... tháng/month " & _oDt.Rows(0)!FTMonth.ToString & " năm/year " & Me.FTPayYear.Text
            '    End If
            'End With
            'xlBookTmp.Worksheets(1).Select()


            'Try
            '    If oExcel.Application.Sheets.Count > 1 Then
            '        For xi As Integer = oExcel.Application.Sheets.Count To 2 Step -1
            '            Try
            '                CType(oExcel.Application.ActiveWorkbook.Sheets(xi), Worksheet).Delete()
            '                oExcel.Application.DisplayAlerts = False
            '            Catch ex As Exception
            '            End Try
            '            Try
            '                oExcel.Sheets(xi).delete()
            '                oExcel.Application.DisplayAlerts = True
            '            Catch ex As Exception
            '            End Try
            '        Next
            '    End If
            'Catch ex As Exception
            'End Try

            i += +1
            Dim n As Integer = 0
            n = i + i

            For index As Integer = i To n
                With xlBookTmp.Worksheets(1)
                    'Date
                    .Rows(CStr(index) & ":" & CStr(index)).EntireRow.Delete

                End With
            Next



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



    Private Sub wReportHRExportBank_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Try

            If FNReportname.Properties.Items.Count < 0 Then
                MsgBox("ไม่พบการกำหนด File Report !!!")
                Me.Close()
            End If

        Catch ex As Exception
        End Try

    End Sub


End Class