Imports System.Data.SqlClient
Imports System.IO
Imports System.Text

Public Class wHRReportExportTax91
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

        If Me.FTPayYear.Text = "" Then
            HI.MG.ShowMsg.mProcessError(1005210001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
            Exit Sub
        End If

        'If Me.FNHSysEmpTypeId.Text <> "" Then
        '    _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        '    _Formular &= "  {THRMEmpType.FTEmpTypeCode}='" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "' "

        'End If

        '_Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        '_Formular &= " {THRTPayRoll.FTYear}='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "' "


        'Dim tText As String = ""
        'tText = Condition.GetCriteria

        'If tText <> "" Then
        '    _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        '    _Formular &= "" & tText
        'End If

        Select Case FNReportname.SelectedIndex
            Case 1
                If Me.FNHSysEmpTypeId.Text <> "" Then
                    _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                    _Formular &= "  ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "' "

                End If

                '_Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                '_Formular &= " {THRTPayRoll.FTYear}='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "' "


                'Dim tText As String = ""
                'tText = Condition.GetCriteria

                'If tText <> "" Then
                '    _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                '    _Formular &= "" & tText
                'End If


                Dim _Dt As DataTable = HI.HRCAL.Social.Export(HRCAL.Social.TaxType.ภงด91, "", "", Me.FTPayYear.Text, "01", _Formular)

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
                        Op.FileName = "Tax91.txt"
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

                If Me.FNHSysEmpTypeId.Text <> "" Then
                    _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                    _Formular &= "  {THRMEmpType.FTEmpTypeCode}='" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "' "

                End If

                _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                _Formular &= " {THRTPayRoll.FTYear}='" & HI.UL.ULF.rpQuoted(Me.FTPayYear.Text) & "' "


                Dim tText As String = ""
                tText = Condition.GetCriteria

                If tText <> "" Then
                    _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
                    _Formular &= "" & tText
                End If


                Dim _AllReportName As String = _LstReport.GetValue(FNReportname.SelectedIndex)

                If _AllReportName <> "" Then
                    Call HI.ST.Security.CreateTempEmpMaster(Condition)
                    Call HI.ST.Security.CreateTempPayroll(Condition, FTPayYear.Text)
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