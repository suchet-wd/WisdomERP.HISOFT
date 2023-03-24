Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.XtraReports.Parameters
Imports DevExpress.XtraReports.Expressions
Imports DevExpress.XtraReports.UI

Public Class wReportEmployeeMasterCondition

    Private _LstReport As HI.RP.ListReport
    Sub New(_SysFormName As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.Name = _SysFormName

        Condition.PrePareData()

        _LstReport = New HI.RP.ListReport(_SysFormName)
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

        '*****วันที่เริ่มงาน*********
        If Me.FDWorkStart.Text <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRMEmployee.FDDateStart}>='" & HI.UL.ULDate.ConvertEnDB(Me.FDWorkStart.Text) & "' "
        End If

        If Me.FDWorkEnd.Text <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRMEmployee.FDDateStart}<='" & HI.UL.ULDate.ConvertEnDB(Me.FDWorkEnd.Text) & "' "
        End If

        '*****วันที่ผ่าน Pro *********
        If Me.FDSDateProbation.Text <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRMEmployee.FDDateProbation}>='" & HI.UL.ULDate.ConvertEnDB(Me.FDSDateProbation.Text) & "' "
        End If

        If Me.FDEDateProbation.Text <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRMEmployee.FDDateProbation}<='" & HI.UL.ULDate.ConvertEnDB(Me.FDEDateProbation.Text) & "' "
        End If

        '*****วันที่ลาออก*********
        If Me.FDResignStart.Text <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRMEmployee.FDDateEnd}>='" & HI.UL.ULDate.ConvertEnDB(Me.FDResignStart.Text) & "' "
        End If

        If Me.FDResignEnd.Text <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRMEmployee.FDDateEnd}<='" & HI.UL.ULDate.ConvertEnDB(Me.FDResignEnd.Text) & "' "
        End If

        '*****วันเกิด*********
        If Me.FDBirthStart.Text <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRMEmployee.FDBirthDate}>='" & HI.UL.ULDate.ConvertEnDB(Me.FDBirthStart.Text) & "' "
        End If

        If Me.FDBirthEnd.Text <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " {THRMEmployee.FDBirthDate}<='" & HI.UL.ULDate.ConvertEnDB(Me.FDBirthEnd.Text) & "' "
        End If

        Dim tText As String = ""
        tText = Condition.GetCriteria
        If tText <> "" Then
            _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            _Formular &= " " & tText
        End If

        Dim _AllReportName As String = _LstReport.GetValue(FNReportname.SelectedIndex)

        If _AllReportName <> "" Then

            Call HI.ST.Security.CreateTempEmpMaster(Condition)

            If _LstReport.GetValueGenPic(FNReportname.SelectedIndex) = "1" Then
                Call HI.HRCAL.GenTempData.GenerateEmpPicture(Condition)
            End If

            If Me.Name = "wEmployeeCardReport" Then

                For Each _ReportName As String In _AllReportName.Split(",")

                    If Microsoft.VisualBasic.Right(_ReportName, 4) = ".rpt" Then
                        With New HI.RP.Report

                            '*****วันที่เริ่มงาน*********
                            If Me.FDWorkStart.Text <> "" Then
                                .AddParameter("SFDDateStart", Me.FDWorkStart.Text)
                            End If

                            If Me.FDWorkEnd.Text <> "" Then
                                .AddParameter("EFDDateStart", Me.FDWorkEnd.Text)
                            End If

                            '*****วันที่ลาออก*********
                            If Me.FDResignStart.Text <> "" Then
                                .AddParameter("SFDDateEnd", Me.FDResignStart.Text)
                            End If

                            If Me.FDResignEnd.Text <> "" Then
                                .AddParameter("EFDDateEnd", Me.FDResignEnd.Text)
                            End If

                            '*****วันเกิด*********
                            If Me.FDBirthStart.Text <> "" Then
                                .AddParameter("SFDBirthDate", Me.FDBirthStart.Text)
                            End If

                            If Me.FDBirthEnd.Text <> "" Then
                                .AddParameter("EFDBirthDate", Me.FDBirthEnd.Text)
                            End If

                            If ((HI.ST.SysInfo.Admin)) Then
                                .AddParameter("FNStateSalary", "1")
                            Else

                                Dim _FNStateSalary As String = "0"

                                _Str = "Select U.FNHSysPermissionID ,T.FNHSysEmpTypeId ,T.FTStateSalary"
                                _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS U With(NOLOCK) INNER JOIN"
                                _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS T With(NOLOCK) ON U.FNHSysPermissionID = T.FNHSysPermissionID"
                                _Str &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS TU With(NOLOCK) ON U.FNHSysPermissionID = TU.FNHSysPermissionID"
                                _Str &= vbCrLf & " WHERE U.FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Str &= vbCrLf & "       AND  TU.FTMnuName ='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' "
                                _Str &= vbCrLf & "       AND T.FTStateSalary='1' "
                                _Str &= vbCrLf & " ORDER BY T.FTStateSalary DESC"

                                _pdt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

                                If _pdt.Rows.Count > 0 Then
                                    _FNStateSalary = "1"
                                End If

                                _pdt.Dispose()

                                .AddParameter("FNStateSalary", _FNStateSalary)

                            End If

                            .FormTitle = Me.Text
                            .ReportFolderName = _LstReport.GetFolderReportValue(FNReportname.SelectedIndex)
                            .Formular = _Formular
                            .ReportName = _ReportName
                            .Preview()
                        End With
                    Else



                        Dim odt As New DataTable
                        Dim _Cmd As String = ""
                        _Cmd = "  delete from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo. TmpCardEmployee " '  where UserLogIn='" & HI.ST.UserInfo.UserName & "'"
                        _Cmd &= vbCrLf & "insert into [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo. TmpCardEmployee"
                        _Cmd &= vbCrLf & "( FNHSysEmpID ,FTEmpCode, FDDateStart, FTSeqNo, FTEmpNameTH, FTEmpSurnameTH, UserLogIn, FTEmpSurnameEN, FTEmpNameEN, FTDeptDescTH, FTEmpStatus, FTEmpBarcode, FNHSysCmpId, FTEmpPicture, FTEmpPicName)"
                        _Cmd &= vbCrLf & " select   FNHSysEmpID ,  FTEmpCode, FDDateStart, FTSeqNo, FTEmpNameTH, FTEmpSurnameTH, UserLogIn, FTEmpSurnameEN, FTEmpNameEN, FTDeptDescTH, FNEmpStatus, FTEmpBarcode, FNHSysCmpId, FTEmpPicture, FTEmpPicName "
                        _Cmd &= vbCrLf & "  from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.v_Emp_Card  "
                        _Cmd &= vbCrLf & "  where UserLogIn='" & HI.ST.UserInfo.UserName & "'"
                        _Cmd &= vbCrLf & " and  FTSeqNo <> 99  "
                        _Cmd &= vbCrLf & "and FNHSysCmpId =" & Val(HI.ST.SysInfo.CmpID)

                        HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_HR)

                        '_Cmd = "  select  *  from [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo. TmpCardEmployee   where FTSeqNo = 1  and  UserLogIn='" & HI.ST.UserInfo.UserName & "'"
                        'odt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR, "TmpCardEmployee")



                        'For Each _ReportName As String In _AllReportName.Split(",")



                        Dim filePath As String = Application.StartupPath & "\Reports\" & _LstReport.GetFolderReportValue(FNReportname.SelectedIndex) & "" & _ReportName
                        ' Save a report's layout to the file.  
                        Dim report As New DevExpress.XtraReports.UI.XtraReport
                        'report.DataSource
                        '     report.SaveLayout(filePath)
                        ' Restore a report's layout from the specified file.  
                        'report.Da


                        'Dim objectDataSource As New ObjectDataSource()
                        'objectDataSource.Name = "sqlDataSource1"
                        'objectDataSource.DataSource = odt
                        'objectDataSource.DataMember = "TmpCardEmployee"


                        report.LoadLayout(filePath)
                        DirectCast(report.DataSource, DevExpress.DataAccess.Sql.SqlDataSource).Connection.ConnectionString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
                        'Dim parameter As New Parameter() With {.Name = "UserLogIn", .Type = GetType(String), .Value = HI.ST.UserInfo.UserName}
                        'report.Parameters.Add(Parameter)
                        ' report.Parameters("UserLogIn").Value = HI.ST.UserInfo.UserName



                        '  report.DataSource = objectDataSource


                        'Create a report designer.  
                        'Dim dt As New ReportDesignTool(report)
                        Dim dtx As New ReportPrintTool(report)
                        ' Invoke the standard End-User Designer form. 
                        dtx.ShowPreview()

                        'dt.ShowDesigner()
                        'Next
                    End If

                Next



            Else
                For Each _ReportName As String In _AllReportName.Split(",")
                    With New HI.RP.Report

                        '*****วันที่เริ่มงาน*********
                        If Me.FDWorkStart.Text <> "" Then
                            .AddParameter("SFDDateStart", Me.FDWorkStart.Text)
                        End If

                        If Me.FDWorkEnd.Text <> "" Then
                            .AddParameter("EFDDateStart", Me.FDWorkEnd.Text)
                        End If

                        '*****วันที่ลาออก*********
                        If Me.FDResignStart.Text <> "" Then
                            .AddParameter("SFDDateEnd", Me.FDResignStart.Text)
                        End If

                        If Me.FDResignEnd.Text <> "" Then
                            .AddParameter("EFDDateEnd", Me.FDResignEnd.Text)
                        End If

                        '*****วันเกิด*********
                        If Me.FDBirthStart.Text <> "" Then
                            .AddParameter("SFDBirthDate", Me.FDBirthStart.Text)
                        End If

                        If Me.FDBirthEnd.Text <> "" Then
                            .AddParameter("EFDBirthDate", Me.FDBirthEnd.Text)
                        End If

                        If ((HI.ST.SysInfo.Admin)) Then
                            .AddParameter("FNStateSalary", "1")
                        Else

                            Dim _FNStateSalary As String = "0"

                            _Str = "Select U.FNHSysPermissionID ,T.FNHSysEmpTypeId ,T.FTStateSalary"
                            _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS U With(NOLOCK) INNER JOIN"
                            _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS T With(NOLOCK) ON U.FNHSysPermissionID = T.FNHSysPermissionID"
                            _Str &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS TU With(NOLOCK) ON U.FNHSysPermissionID = TU.FNHSysPermissionID"
                            _Str &= vbCrLf & " WHERE U.FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Str &= vbCrLf & "       AND  TU.FTMnuName ='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' "
                            _Str &= vbCrLf & "       AND T.FTStateSalary='1' "
                            _Str &= vbCrLf & " ORDER BY T.FTStateSalary DESC"

                            _pdt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

                            If _pdt.Rows.Count > 0 Then
                                _FNStateSalary = "1"
                            End If

                            _pdt.Dispose()

                            .AddParameter("FNStateSalary", _FNStateSalary)

                        End If

                        .FormTitle = Me.Text
                        .ReportFolderName = _LstReport.GetFolderReportValue(FNReportname.SelectedIndex)
                        .Formular = _Formular
                        .ReportName = _ReportName
                        .Preview()
                    End With
                Next
            End If

            'For Each _ReportName As String In _AllReportName.Split(",")
            '    With New HI.RP.Report

            '        '*****วันที่เริ่มงาน*********
            '        If Me.FDWorkStart.Text <> "" Then
            '            .AddParameter("SFDDateStart", Me.FDWorkStart.Text)
            '        End If

            '        If Me.FDWorkEnd.Text <> "" Then
            '            .AddParameter("EFDDateStart", Me.FDWorkEnd.Text)
            '        End If

            '        '*****วันที่ลาออก*********
            '        If Me.FDResignStart.Text <> "" Then
            '            .AddParameter("SFDDateEnd", Me.FDResignStart.Text)
            '        End If

            '        If Me.FDResignEnd.Text <> "" Then
            '            .AddParameter("EFDDateEnd", Me.FDResignEnd.Text)
            '        End If

            '        '*****วันเกิด*********
            '        If Me.FDBirthStart.Text <> "" Then
            '            .AddParameter("SFDBirthDate", Me.FDBirthStart.Text)
            '        End If

            '        If Me.FDBirthEnd.Text <> "" Then
            '            .AddParameter("EFDBirthDate", Me.FDBirthEnd.Text)
            '        End If

            '        If ((HI.ST.SysInfo.Admin)) Then
            '            .AddParameter("FNStateSalary", "1")
            '        Else

            '            Dim _FNStateSalary As String = "0"

            '            _Str = "Select U.FNHSysPermissionID ,T.FNHSysEmpTypeId ,T.FTStateSalary"
            '            _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS U With(NOLOCK) INNER JOIN"
            '            _Str &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS T With(NOLOCK) ON U.FNHSysPermissionID = T.FNHSysPermissionID"
            '            _Str &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS TU With(NOLOCK) ON U.FNHSysPermissionID = TU.FNHSysPermissionID"
            '            _Str &= vbCrLf & " WHERE U.FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '            _Str &= vbCrLf & "       AND  TU.FTMnuName ='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) + "' "
            '            _Str &= vbCrLf & "       AND T.FTStateSalary='1' "
            '            _Str &= vbCrLf & " ORDER BY T.FTStateSalary DESC"

            '            _pdt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

            '            'For Each pR As DataRow In _pdt.Rows
            '            '    If (((pR!FNHSysEmpTypeId.ToString) = "1306010002") Or ((pR!FNHSysEmpTypeId.ToString) = "1758590001")) Then
            '            '        .AddParameter("FNStateSalary", pR!FTStateSalary.ToString)
            '            '        _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
            '            '        _Formular &= " {THRMEmpType.FTEmpTypeCode} <> 'M' AND {THRMEmpType.FTEmpTypeCode} <> 'N'"
            '            '        Exit For
            '            '    Else
            '            '        .AddParameter("FNStateSalary", "1")
            '            '    End If
            '            'Next

            '            If _pdt.Rows.Count > 0 Then
            '                _FNStateSalary = "1"
            '            End If

            '            _pdt.Dispose()

            '            .AddParameter("FNStateSalary", _FNStateSalary)

            '        End If

            '        .FormTitle = Me.Text
            '        .ReportFolderName = _LstReport.GetFolderReportValue(FNReportname.SelectedIndex)
            '        .Formular = _Formular
            '        .ReportName = _ReportName
            '        .Preview()
            '    End With
            'Next
        Else
            HI.MG.ShowMsg.mProcessError(1005170001, "", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub wReportMasterCondition_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If FNReportname.Properties.Items.Count < 0 Then
                MsgBox("ไม่พบการกำหนด File Report !!!")
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class