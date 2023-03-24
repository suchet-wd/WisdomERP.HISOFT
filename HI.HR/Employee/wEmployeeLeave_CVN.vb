Imports System
Imports System.Windows.Forms
Imports System.IO
Imports System.Data.SqlClient
Imports DevExpress.XtraPdfViewer
Imports DevExpress.XtraRichEdit
Imports DevExpress.Pdf
Imports System.Globalization
Imports System.Threading
Public Class wEmployeeLeave_CVN
    Private _LstReport As HI.RP.ListReport
    Private _leavePro As Boolean = False
    Private _leavePerDay As Boolean = False
    Private oDbdtLeave As DataTable
    Sub New()

        _ProcPrepare = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _Actualdate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

        Dim _Qry As String = ""
        _Qry = "SELECT TOP 1 FTStateApproveLeave "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEConfig AS A WITH(NOLOCK)"
        ocmapprove.Visible = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "") = "1")

        _LstReport = New HI.RP.ListReport(Me.Name.ToString)
        FNReportname.Properties.Items.AddRange(_LstReport.GetList)

        _Qry = " SELECT TOP 1 FTCfgData"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS Z WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE  (FTCfgName = N'LeavePragNentPayMergeSundayHoloday')"

        LeavePragNentPayMergeSundayHoloday = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")

        _Qry = " SELECT TOP 1 FTCfgData"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS Z WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE  (FTCfgName = N'CfgLeaveProcess')"
        ocmapprove.Visible = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "") = "1")
        _leavePro = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "") = "1")

        _Qry = " SELECT TOP 1 FTCfgData"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS Z WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE  (FTCfgName = N'CfgleaveProcessPerDay')"
        _leavePerDay = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "") = "1")

        _Qry = " SELECT TOP 1 FTCfgData"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS Z WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE  (FTCfgName = N'CfgleaveSendApp')"
        ocmSendApprove.Visible = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "") = "1")

        _ProcPrepare = False
    End Sub

    Private _ProcPrepare As Boolean = False
    Private _Actualdate As String = ""
    ReadOnly Property Actualdate As String
        Get
            Return _Actualdate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property

    Private _LeavePragNentPayMergeSundayHoloday As String = ""
    Public Property LeavePragNentPayMergeSundayHoloday As String
        Get
            Return _LeavePragNentPayMergeSundayHoloday
        End Get
        Set(value As String)
            _LeavePragNentPayMergeSundayHoloday = value
        End Set
    End Property

#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

#End Region

#Region "MAIN PROC"

    Private Function GetValCheck(_Idx As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT top 1    isnull(FTCallMethodName,'') as  FTCallMethodName "
            _Cmd &= vbCrLf & "  From   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK) "
            _Cmd &= vbCrLf & "    WHERE  (FTListName = N'FNLeaveType')"
            _Cmd &= vbCrLf & "    and   (FNListIndex = N'" & _Idx & "')"
            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_SYSTEM, "") = "ValcationLeave_Check"
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ProcessSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click
        If Me.VerrifyData Then
            If Not ChkActiveLeaveHRDelete() Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขข้อมูลการลาของพนักงานคนนี้ได้ กรุณาตรวจสอบ !!!", 1702281015, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If

            If GetValCheck(HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex)) Then
                Call ValcationLeave_Check()
            End If
            'If HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) = "98" Or HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) = "97" Or HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) = "0" Then
            'Else
            '    Call ValcationLeave_Check()
            'End If


            Call Checkpay(HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex), False)

            If TotalLeave < (FTNetDay.Value * ocetotaltime.Value) Then
                HI.MG.ShowMsg.mProcessError(1004090001, "พบข้อมูลการลาเกินกำหนด !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Information, "  " & (TotalLeave / 480).ToString & "  Day ")

                If HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) = "98" Then
                    Exit Sub
                End If
                If HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) = "999" Then
                    Exit Sub
                End If
            End If

            Dim _Msg As String = ""
            Dim _Qry As String = ""
            _Qry = "Select      TOP  1  'In M: ' + ISNULL(FTScanMIn,'') + char(13) + 'Out M: ' + ISNULL(FTScanMOut,'') + char(13) + 'In A: ' + ISNULL(FTScanAIn,'') + char(13) + 'Out A: ' + ISNULL(FTScanAOut,'')  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Integer.Parse(Val(FNHSysEmpId.Properties.Tag.ToString)) & ""
            _Qry &= vbCrLf & " AND FTDateTrans>='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'  "
            _Qry &= vbCrLf & " AND FTDateTrans<='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'  "
            _Qry &= vbCrLf & " AND (ISNULL(FTScanMIn,'')<>'' OR ISNULL(FTScanMOut,'')<>'' OR  ISNULL(FTScanAIn,'')<>'' OR  ISNULL(FTScanAOut,'') <>'' ) "
            _Msg = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

            'If _Msg <> "" Then
            '    HI.MG.ShowMsg.mProcessError(1144090001, "พบข้อมูลการรูดบัตรของพนักงาน กรุณาทำการตรวจสอบ !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning, vbCrLf & vbCrLf & _Msg & vbCrLf & vbCrLf & "")
            'End If

            Dim _State As Boolean = False
            If _Msg <> "" Then
                _State = HI.MG.ShowMsg.mConfirmProcessDefaultNo("พบข้อมูลการรูดบัตรของพนักงาน กรุณาทำการตรวจสอบ !!!", 1144090001, FNHSysEmpId_None.Text & vbCrLf & vbCrLf & _Msg & vbCrLf & vbCrLf & "")
            Else
                _State = HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการบันทึกข้อมูลการลาใช่หรือไม่ ?", 1404300001, FNHSysEmpId_None.Text)
            End If

            'If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการบันทึกข้อมูลการลาใช่หรือไม่ ?", 1404300001, FNHSysEmpId_None.Text) Then
            If (_State) Then
                Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")

                If Me.SaveData() Then
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    FTStartDate_EditValueChanged(FTStartDate, New System.EventArgs)
                    Call LoadDataEmployeeLeaveHistory()
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If

            End If
        End If
    End Sub

    Private Sub ProcessDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If Me.VerrifyData Then
            If Not ChkActiveLeaveHRDelete() Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขข้อมูลการลาของพนักงานคนนี้ได้ กรุณาตรวจสอบ !!!", 1702281720, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If

            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบข้อมูลการลาใช่หรือไม่ ?", 1404300002, FNHSysEmpId_None.Text) Then
                Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
                If Me.DeleteData() Then
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    FTStartDate_EditValueChanged(FTStartDate, New System.EventArgs)
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If
        End If
    End Sub

    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Me.ogc.DataSource = Nothing
        Me.FTEmpPicName.Image = Nothing
        FTStartDate.Text = ""
        FTEndDate.Text = ""


    End Sub


    Private Sub ProcessPreview(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmpreview.Click

        Dim _AllReportName As String = _LstReport.GetValue(FNReportname.SelectedIndex)


        If Me.FNHSysEmpId.Text <> "" And Me.FNHSysEmpId.Properties.Tag.ToString <> "" Then
            For Each _ReportName As String In _AllReportName.Split(",")
                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .ReportFolderName = _LstReport.GetFolderReportValue(FNReportname.SelectedIndex)
                    .ReportName = _ReportName

                    Dim sumBusinessLeave As String = "0"
                    Dim sumSickLeave As String = "0"
                    Dim sumVacationLeave As String = "0"
                    Dim sumAbsent As String = "0"
                    Dim sumLate As String = "0"
                    Dim LeaveVacation As String = "0"
                    Dim sumSpecialLeave As String = "0"
                    Dim sumLeaveNotpaid As String = "0"
                    Dim sumAccident As String = "0"
                    Dim strCon As String = ""

                    Call SumLeaveReport(sumBusinessLeave, sumSickLeave, sumVacationLeave, sumAbsent, sumLate, LeaveVacation, sumSpecialLeave, sumLeaveNotpaid, sumAccident)

                    .AddParameter("sumBusinessLeave", sumBusinessLeave)
                    .AddParameter("sumSickLeave", sumSickLeave)
                    .AddParameter("sumVacationLeave", sumVacationLeave)
                    .AddParameter("sumAbsent", sumAbsent)
                    .AddParameter("sumLate", sumLate)
                    .AddParameter("sumSpecialLeave", sumSpecialLeave)
                    .AddParameter("sumLeaveNotpaid", sumLeaveNotpaid)
                    .AddParameter("TotVacation", LeaveVacation)
                    .AddParameter("sumAccident", sumAccident)

                    If FTStartDate.Text <> "" Then strCon += "{THRTLeaveAdvanceDaily.FTStartDate}=  '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "' "
                    If FTEndDate.Text <> "" Then strCon += "AND {THRTLeaveAdvanceDaily.FTEndDate}='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "' "
                    strCon += "AND {THRMEmployee.FNHSysEmpID}=" & Integer.Parse(Val((Me.FNHSysEmpId.Properties.Tag.ToString))) & ""

                    .Formular = strCon
                    .Preview()
                End With
            Next

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysEmpId_lbl.Text)
            FNHSysEmpId.Focus()
        End If



    End Sub

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Procedure "

    Private Function SaveData() As Boolean
        Try
            Dim _StateMngApp As Boolean = False
            Dim _Qry As String
            Dim _QryFile As String
            _Qry = "SELECT Top 1  Isnull(FTStateMngApp,'0')  as FTStateMngApp "
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType On THRMEmployee.FNHSysEmpTypeId = HITECH_MASTER.dbo.THRMEmpType.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "WHERE FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
            _StateMngApp = (HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0") = "1")


            Dim _PicName As String = ""
            _Qry = "Select TOP 1 FNHSysEmpID FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily With(NOLOCK)"
            _Qry &= vbCrLf & "WHERE FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
            _Qry &= vbCrLf & "And FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            _Qry &= vbCrLf & "AND FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            _Qry &= vbCrLf & " AND FTLeaveType = '" & HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) & "'"

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") = "" Then

                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily (FTInsUser, FTInsDate, FTInsTime"
                _Qry &= vbCrLf & " , FNHSysEmpID, FTStartDate, FTEndDate,FTHoliday, FNLeaveTotalDay"
                _Qry &= vbCrLf & " , FTLeaveType,  FTLeavePay"
                _Qry &= vbCrLf & " , FTLeaveStartTime, FTLeaveEndTime, FNLeaveTotalTime,FNLeaveTotalTimeMin, FTLeaveNote"
                _Qry &= vbCrLf & ",FTStaCalSSO,FTStaLeaveDay,FTStateMedicalCertificate,FTMedicalCertificateName,FTStateDeductVacation ,FNSpecialLeaveType"

                Select Case True
                    Case Not (ocmSendApprove.Visible) And Not (ocmapprove.Visible) And _leavePro = False And Not (_StateMngApp)
                        '***** state appoved originale
                        _Qry &= vbCrLf & " ,FTApproveState"
                        _Qry &= vbCrLf & " , FTApproveDate"
                        _Qry &= vbCrLf & " , FTApproveTime"
                        _Qry &= vbCrLf & " , FTApproveBy"

                        '***** send approved
                        _Qry &= vbCrLf & " , FTSendApproveState"
                        _Qry &= vbCrLf & " , FDSendApproveDate"
                        _Qry &= vbCrLf & " , FTSendApproveTime"
                        _Qry &= vbCrLf & " , FTSendApproveBy"

                        '***** mng approved
                        _Qry &= vbCrLf & " , FTMngApproveState"
                        _Qry &= vbCrLf & " , FDMngApproveDate"
                        _Qry &= vbCrLf & " , FTMngApproveTime"
                        _Qry &= vbCrLf & " , FTMngApproveBy"

                    Case Not (ocmSendApprove.Visible) And (ocmapprove.Visible) And _StateMngApp
                        '***** send approved
                        _Qry &= vbCrLf & " , FTSendApproveState"
                        _Qry &= vbCrLf & " , FDSendApproveDate"
                        _Qry &= vbCrLf & " , FTSendApproveTime"
                        _Qry &= vbCrLf & " , FTSendApproveBy "

                    Case Not (ocmSendApprove.Visible) And (ocmapprove.Visible) And Not (_StateMngApp)

                        '***** send approved
                        _Qry &= vbCrLf & " , FTSendApproveState"
                        _Qry &= vbCrLf & " , FDSendApproveDate"
                        _Qry &= vbCrLf & " , FTSendApproveTime"
                        _Qry &= vbCrLf & " , FTSendApproveBy"

                        '***** mng approved
                        _Qry &= vbCrLf & " , FTMngApproveState"
                        _Qry &= vbCrLf & " , FDMngApproveDate"
                        _Qry &= vbCrLf & " , FTMngApproveTime"
                        _Qry &= vbCrLf & " , FTMngApproveBy"


                    Case Else
                        _Qry &= vbCrLf & " ,FTApproveState"
                End Select
                _Qry &= vbCrLf & " )"

                _Qry &= vbCrLf & " VALUES ('" & HI.ST.UserInfo.UserName & "'"
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & " ,'" & Val(FNHSysEmpId.Properties.Tag.ToString) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
                _Qry &= vbCrLf & " ,'" & FTStateNotMergeHoliday.EditValue.ToString & "'"
                _Qry &= vbCrLf & " ," & FTNetDay.Value
                _Qry &= vbCrLf & " ,'" & HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) & "'"
                _Qry &= vbCrLf & " ,'" & FTStateLeavepay.EditValue.ToString & "'"
                _Qry &= vbCrLf & " ,'" & Me.FTSTime.Text & "'"
                _Qry &= vbCrLf & " ,'" & Me.FTETime.Text & "'"
                _Qry &= vbCrLf & " ," & FNNetTime.Value
                _Qry &= vbCrLf & " ," & ocetotaltime.Value
                _Qry &= vbCrLf & " ,N'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"

                'If Not (ocmapprove.Visible) And _leavePro = False Then
                '    _Qry &= vbCrLf & " ,'1'"
                'Else
                '    _Qry &= vbCrLf & " ,'0'"
                'End If

                _Qry &= vbCrLf & " ,'" & FTStateCalSSo.EditValue.ToString & "'"
                _Qry &= vbCrLf & " ,'" & HI.TL.CboList.GetListValue("" & FNLeaveDay.Properties.Tag.ToString, FNLeaveDay.SelectedIndex) & "'"
                _Qry &= vbCrLf & " ,'" & FTStateMedicalCertificate.EditValue.ToString & "','" & HI.UL.ULF.rpQuoted(_PicName) & "'"
                _Qry &= vbCrLf & " ,'" & FTStateDeductVacation.EditValue.ToString & "'"
                _Qry &= vbCrLf & " ,'" & HI.TL.CboList.GetListValue("" & FNSpecialLeaveType.Properties.Tag.ToString, FNSpecialLeaveType.SelectedIndex) & "'"

                Select Case True
                    Case Not (ocmSendApprove.Visible) And Not (ocmapprove.Visible) And _leavePro = False And Not (_StateMngApp)
                        '***** state appoved originale
                        _Qry &= vbCrLf & ",'1' "
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ",'" & HI.ST.UserInfo.UserName & "'"

                        '***** send approved
                        _Qry &= vbCrLf & ",'1'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ",'" & HI.ST.UserInfo.UserName & "'"

                        '***** mng approved
                        _Qry &= vbCrLf & " ,'1'"
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & " ,'" & HI.ST.UserInfo.UserName & "'"

                    Case Not (ocmSendApprove.Visible) And (ocmapprove.Visible) And _StateMngApp
                        '***** send approved
                        _Qry &= vbCrLf & " ,'1'"
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & " ,'" & HI.ST.UserInfo.UserName & "'"
                    Case Not (ocmSendApprove.Visible) And (ocmapprove.Visible) And Not (_StateMngApp)
                        '***** send approved
                        _Qry &= vbCrLf & ",'1'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & ",'" & HI.ST.UserInfo.UserName & "'"

                        '***** mng approved
                        _Qry &= vbCrLf & " ,'1'"
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & " ,'" & HI.ST.UserInfo.UserName & "'"

                    Case Else
                        _Qry &= vbCrLf & " ,'0' "
                End Select
                _Qry &= vbCrLf & " )"




            Else

                _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily SET"
                _Qry &= vbCrLf & " FTUpdUser = '" & HI.ST.UserInfo.UserName & "'"
                _Qry &= vbCrLf & " ,FTUpdDate = " & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & " ,FTUpdTime = " & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & " ,FTHoliday = '" & FTStateNotMergeHoliday.EditValue.ToString & "'"
                _Qry &= vbCrLf & " ,FNLeaveTotalDay = " & FTNetDay.Value
                _Qry &= vbCrLf & " ,FTLeaveType = '" & HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) & "'"
                _Qry &= vbCrLf & " ,FTLeavePay = '" & FTStateLeavepay.EditValue.ToString & "'"
                _Qry &= vbCrLf & " ,FTLeaveStartTime = '" & Me.FTSTime.Text & "'"
                _Qry &= vbCrLf & " ,FTLeaveEndTime = '" & Me.FTETime.Text & "'"
                _Qry &= vbCrLf & " ,FTLeaveNote = N'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
                _Qry &= vbCrLf & " ,FNLeaveTotalTime=" & FNNetTime.Value
                _Qry &= vbCrLf & " ,FNLeaveTotalTimeMin=" & ocetotaltime.Value

                Select Case True
                    Case Not (ocmSendApprove.Visible) And Not (ocmapprove.Visible) And _leavePro = False And Not (_StateMngApp)
                        '***** state appoved originale
                        _Qry &= vbCrLf & " ,FTApproveState='1' "
                        _Qry &= vbCrLf & " , FTApproveDate=" & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & " , FTApproveTime=" & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & " , FTApproveBy = '" & HI.ST.UserInfo.UserName & "'"

                        '***** send approved
                        _Qry &= vbCrLf & " , FTSendApproveState = '1'"
                        _Qry &= vbCrLf & " , FDSendApproveDate=" & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & " , FTSendApproveTime=" & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & " , FTSendApproveBy = '" & HI.ST.UserInfo.UserName & "'"

                        '***** mng approved
                        _Qry &= vbCrLf & " , FTMngApproveState = '1'"
                        _Qry &= vbCrLf & " , FDMngApproveDate=" & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & " , FTMngApproveTime=" & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & " , FTMngApproveBy = '" & HI.ST.UserInfo.UserName & "'"

                        _Qry &= vbCrLf & " ,FTApproveState='0' "
                        _Qry &= vbCrLf & " , FTDirApproveState='0'"


                    Case Not (ocmSendApprove.Visible) And (ocmapprove.Visible) And _StateMngApp
                        '***** send approved
                        _Qry &= vbCrLf & " , FTSendApproveState = '1'"
                        _Qry &= vbCrLf & " , FDSendApproveDate=" & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & " , FTSendApproveTime=" & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & " , FTSendApproveBy = '" & HI.ST.UserInfo.UserName & "'"
                        _Qry &= vbCrLf & " ,FTApproveState='0' "

                        _Qry &= vbCrLf & " ,FTMngApproveState = '0'"
                        _Qry &= vbCrLf & ", FDMngApproveDate =NULL"
                        _Qry &= vbCrLf & ", FTMngApproveTime =NULL"
                        _Qry &= vbCrLf & ", FTMngApproveBy =NULL"

                        _Qry &= vbCrLf & " , FTDirApproveState='0'"
                        _Qry &= vbCrLf & ", FDDirApproveDate =NULL"
                        _Qry &= vbCrLf & ", FTDirApproveBy =NULL"
                        _Qry &= vbCrLf & ", FTDirApproveTime =NULL"

                    Case Not (ocmSendApprove.Visible) And (ocmapprove.Visible) And Not (_StateMngApp)
                        '***** send approved
                        _Qry &= vbCrLf & " , FTSendApproveState = '1'"
                        _Qry &= vbCrLf & " , FDSendApproveDate=" & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & " , FTSendApproveTime=" & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & " , FTSendApproveBy = '" & HI.ST.UserInfo.UserName & "'"

                        '***** mng approved
                        _Qry &= vbCrLf & " , FTMngApproveState = '1'"
                        _Qry &= vbCrLf & " , FDMngApproveDate=" & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & " , FTMngApproveTime=" & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & " , FTMngApproveBy = '" & HI.ST.UserInfo.UserName & "'"


                        '_Qry &= vbCrLf & " ,FTMngApproveState = '0'"
                        '_Qry &= vbCrLf & ", FDMngApproveDate =NULL"
                        '_Qry &= vbCrLf & ", FTMngApproveTime =NULL"
                        '_Qry &= vbCrLf & ", FTMngApproveBy =NULL"

                        _Qry &= vbCrLf & " , FTDirApproveState='0'"
                        _Qry &= vbCrLf & ", FDDirApproveDate =NULL"
                        _Qry &= vbCrLf & ", FTDirApproveBy =NULL"
                        _Qry &= vbCrLf & ", FTDirApproveTime =NULL"

                    Case Else
                        _Qry &= vbCrLf & " ,FTMngApproveState = '0'"
                        _Qry &= vbCrLf & ", FDMngApproveDate =NULL"
                        _Qry &= vbCrLf & ", FTMngApproveTime =NULL"
                        _Qry &= vbCrLf & ", FTMngApproveBy =NULL"
                End Select

                _Qry &= vbCrLf & " ,FNSpecialLeaveType='" & HI.TL.CboList.GetListValue("" & FNSpecialLeaveType.Properties.Tag.ToString, FNSpecialLeaveType.SelectedIndex) & "'"
                _Qry &= vbCrLf & " , FTStaCalSSO='" & FTStateCalSSo.EditValue.ToString & "'"
                _Qry &= vbCrLf & " , FTStaLeaveDay='" & HI.TL.CboList.GetListValue("" & FNLeaveDay.Properties.Tag.ToString, FNLeaveDay.SelectedIndex) & "'"
                _Qry &= vbCrLf & " ,FTStateMedicalCertificate= '" & FTStateMedicalCertificate.EditValue.ToString & "'"
                _Qry &= vbCrLf & " ,FTMedicalCertificateName='" & HI.UL.ULF.rpQuoted(_PicName) & "' "
                _Qry &= vbCrLf & " , FTStateDeductVacation='" & FTStateDeductVacation.EditValue.ToString & "'"

                _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
                _Qry &= vbCrLf & " AND FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
                _Qry &= vbCrLf & " AND FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
                _Qry &= vbCrLf & " AND FTLeaveType = '" & HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) & "'"

            End If
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            Call savePDF()

            _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & " AND FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            _Qry &= vbCrLf & " AND FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            _Qry &= vbCrLf & " AND FTLeaveType = '" & HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) & "'"
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            If Not (ocmapprove.Visible) And _leavePro = False Then
                Call ApproveData()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ApproveData() As Boolean
        Try
            Dim _EndProcDate As String = HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text)
            Dim _NextProcDate As String = ""
            Dim nNextDay As Double = 0
            _NextProcDate = HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text)

            Dim _TotalHour As Double = 0
            Dim _FNTotalMonute As Double = 0
            Dim _FNTotalPayHour As Double = 0
            Dim _FNTotalPayMonute As Double = 0
            Dim _FNTotalNotPayHour As Double = 0
            Dim _FNTotalNotPayMonute As Double = 0
            Dim _TmpTotalHour As Double = 0
            Dim _TmpFNTotalMonute As Double = 0
            Dim _TmpFNTotalPayHour As Double = 0
            Dim _TmpFNTotalPayMonute As Double = 0
            Dim _TmpFNTotalNotPayHour As Double = 0
            Dim _TmpFNTotalNotPayMonute As Double = 0
            Dim _dtWeekend As DataTable
            Dim _dtHoliday As DataTable
            Dim _SkipProcess As Boolean
            Dim _Qry As String
            Dim _LeaveCode As String = HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex)
            Dim _WeekEnd As Integer
            Dim _LeavePragNentPay As Integer = 0
            Dim _LeavePragNentNotPay As Boolean = False
            Dim _EmpTypeWeekly As DataTable

            _TmpTotalHour = CDbl(Format(Val(FNNetTime.Value), "0.00"))
            _TmpFNTotalMonute = ocetotaltime.Value

            If (Me.FTStateLeavepay.Checked) Then
                _TmpFNTotalPayHour = _TmpTotalHour
                _TmpFNTotalPayMonute = _TmpFNTotalMonute
            Else
                _TmpFNTotalNotPayHour = _TmpTotalHour
                _TmpFNTotalNotPayMonute = _TmpFNTotalMonute
            End If

            _Qry = "SELECt   FDHolidayDate  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmpTypeWeeklySpecial WITH(NOLOCK) "
            _Qry &= vbCrLf & "  WHERE FDHolidayDate>='" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "' "
            _Qry &= vbCrLf & "  AND FDHolidayDate<='" & HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) & "' "
            _Qry &= vbCrLf & "  AND FNHSysEmpTypeId=" & Integer.Parse(Val(FNHSysEmpTypeId.Properties.Tag.ToString)) & " "

            _EmpTypeWeekly = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
            _Qry = "   SELECT    Top 1   FTSunday,FTMonday, FTTuesday, FTWednesday, "
            _Qry &= vbCrLf & "   FTThursday, FTFriday, FTSaturday"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly  As W WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(FNHSysEmpId.Properties.Tag.ToString) & " "
            _dtWeekend = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            If _dtWeekend.Rows.Count <= 0 Then

            Else
                _EmpTypeWeekly.Rows.Clear()
            End If


            _Qry = "SELECt   FDHolidayDate   "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday WITH(NOLOCK) WHERE FTStateActive='1' "
            _dtHoliday = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

            Dim _EmpOrgShift As String = FNHSysShiftID.Text
            Dim _EmpShift As String = _EmpOrgShift
            Dim _EmpPgmCode As String
            Dim _TotalWorkHour As Double
            Dim _DateReset As String = ""

            _Qry = " SELECT CASE WHEN RiGHT(FTCurrenDate,5) >=FTLeaveReset THEN LEFT(FTCurrenDate,4) ELSE  LEFT(FTBefore,4)  END +'/' + FTLeaveReset"
            _Qry &= vbCrLf & "  FROM"
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT  TOP 1 Convert(varchar(10),GetDate(),111)  AS FTCurrenDate ,Convert(varchar(10),DateAdd(YEAR,-1,GetDate()),111) AS FTBefore,L.FTLeaveReset"
            _Qry &= vbCrLf & " FROM            THRMConfigLeave  AS L WITH (NOLOCK)  INNER JOIN THRMEmployee AS M WITH(NOLOCK )"
            _Qry &= vbCrLf & "  ON  L.FNHSysEmpTypeId=M.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "  WHERE   M.FNHSysEmpID=" & Val(Me.FNHSysEmpId.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & " ) As T"

            _DateReset = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")


            If _LeaveCode = 97 Then

                _Qry = "Select Top 1 FNLeavePay "
                _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLeave WITH(NOLOCK) "
                _Qry &= vbCrLf & " WHERE FNHSysEmpTypeId =" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & " AND FTLeaveCode ='" & _LeaveCode & "' "

                _LeavePragNentPay = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")))

                _Qry = "   SELECT        COUNT(FTDateTrans) AS FNPayDay"
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave WITH(NOLOCK) "
                _Qry &= vbCrLf & " WHERE        (FTLeaveType ='" & _LeaveCode & "')"
                _Qry &= vbCrLf & " AND (FNHSysEmpID =" & Val(FNHSysEmpId.Properties.Tag.ToString) & " ) "
                _Qry &= vbCrLf & " AND (FTDateTrans < N'" & _NextProcDate & "')"
                _Qry &= vbCrLf & " and ( FTDateTrans >='" & _DateReset & "' )"
                _Qry &= vbCrLf & " AND (FNTotalPayMinute > 0) "

                _LeavePragNentPay = _LeavePragNentPay - Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")))

            End If


            Do While _NextProcDate <= _EndProcDate

                _EmpPgmCode = ""
                _EmpPgmCode = ""
                _TotalWorkHour = 8

                _WeekEnd = Weekday(CDate(_NextProcDate), Microsoft.VisualBasic.FirstDayOfWeek.Sunday)

                'Select Case FNLeaveDay.SelectedIndex
                '    Case 0
                '        _TotalHour = CDbl(Format(_TotalWorkHour, "0.00"))
                '        _FNTotalMonute = ((_TotalHour - (_TotalHour Mod 1)) * 60) + ((_TotalHour Mod 1) * 100)
                '    Case 1
                '        _TotalHour = CDbl(Format(4, "0.00"))
                '        _FNTotalMonute = ((_TotalHour - (_TotalHour Mod 1)) * 60) + ((_TotalHour Mod 1) * 100)
                '    Case 2
                '        If _TotalWorkHour > 8 Then
                '            _TotalHour = CDbl(Format(5, "0.00"))
                '            _FNTotalMonute = ((_TotalHour - (_TotalHour Mod 1)) * 60) + ((_TotalHour Mod 1) * 100)
                '        Else
                '            _TotalHour = CDbl(Format(4, "0.00"))
                '            _FNTotalMonute = ((_TotalHour - (_TotalHour Mod 1)) * 60) + ((_TotalHour Mod 1) * 100)
                '        End If
                '    Case Else
                _TotalHour = _TmpTotalHour
                _FNTotalMonute = _TmpFNTotalMonute
                ' End Select

                If (Me.FTStateLeavepay.Checked) Then
                    _FNTotalPayHour = _TotalHour
                    _FNTotalPayMonute = _FNTotalMonute
                Else
                    _FNTotalNotPayHour = _TotalHour
                    _FNTotalNotPayMonute = _FNTotalMonute
                End If

                _SkipProcess = False
                _LeavePragNentNotPay = False

                If Not (Me.FTStateNotMergeHoliday.Checked) Then
                Else

                    ' If LeaveNotMergeSunday <> "1" Then
                    For Each Rday As DataRow In _dtWeekend.Rows
                        If Rday.Item(_WeekEnd - 1).ToString = "1" Then
                            _SkipProcess = True
                        End If
                        Exit For
                    Next
                    ' End If

                    If _SkipProcess = False Then
                        For Each Dr As DataRow In _dtHoliday.Select("   FDHolidayDate  = '" & HI.UL.ULDate.ConvertEnDB(_NextProcDate) & "' ")
                            _SkipProcess = True
                        Next
                    End If

                    If _SkipProcess = False Then
                        For Each Dr As DataRow In _EmpTypeWeekly.Select("   FDHolidayDate  = '" & HI.UL.ULDate.ConvertEnDB(_NextProcDate) & "' ")
                            _SkipProcess = True
                        Next
                    End If

                End If

                If _LeaveCode = "97" And FTStateLeavepay.Checked = True Then

                    If LeavePragNentPayMergeSundayHoloday <> "1" Then
                        For Each Rday As DataRow In _dtWeekend.Rows

                            If Rday.Item(_WeekEnd - 1).ToString = "1" Then
                                _LeavePragNentNotPay = True
                            End If

                            Exit For
                        Next

                        For Each Rday As DataRow In _EmpTypeWeekly.Select("   FDHolidayDate  = '" & HI.UL.ULDate.ConvertEnDB(_NextProcDate) & "' ")
                            _LeavePragNentNotPay = True
                        Next

                    End If

                End If

                If Not (_SkipProcess) Then

                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave(FTInsUser, FTInsDate, FTInsTime"
                    _Qry &= vbCrLf & " ,FNHSysEmpID,FTDateTrans,FTLeaveType"
                    _Qry &= vbCrLf & ",FNTotalHour,FNTotalMinute,FNTotalPayHour,FNTotalPayMinute"
                    _Qry &= vbCrLf & ",FNTotalNotPayHour,FNTotalNotPayMinute,FTLeaveStartTime,FTLeaveEndTime,FTStaCalSSO,FTStaLeaveDay"
                    _Qry &= vbCrLf & ",FNLeaveTotalDay,FTStateMedicalCertificate,FTStateDeductVacation)"
                    _Qry &= vbCrLf & "  SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ," & Val(FNHSysEmpId.Properties.Tag.ToString) & ",'" & _NextProcDate & "' "
                    _Qry &= vbCrLf & " ,'" & _LeaveCode & "'"
                    _Qry &= vbCrLf & " ," & _TotalHour & ""
                    _Qry &= vbCrLf & " ," & _FNTotalMonute & ""

                    If (_LeaveCode = "97" And (_LeavePragNentPay <= 0 Or _LeavePragNentNotPay)) And FTStateLeavepay.Checked = True Then

                        _Qry &= vbCrLf & " ,0"
                        _Qry &= vbCrLf & " ,0"
                        _Qry &= vbCrLf & " ," & _TotalHour & ""
                        _Qry &= vbCrLf & " ," & _FNTotalMonute & ""

                    Else

                        _Qry &= vbCrLf & " ," & _FNTotalPayHour & ""
                        _Qry &= vbCrLf & " ," & _FNTotalPayMonute & ""
                        _Qry &= vbCrLf & " ," & _FNTotalNotPayHour & ""
                        _Qry &= vbCrLf & " ," & _FNTotalNotPayMonute & ""

                    End If

                    _Qry &= vbCrLf & ",'" & Me.FTSTime.Text & "'"
                    _Qry &= vbCrLf & ",'" & Me.FTETime.Text & "'"
                    _Qry &= vbCrLf & ",'" & Me.FTStateCalSSo.EditValue.ToString & "'"
                    _Qry &= vbCrLf & ",'" & HI.TL.CboList.GetListValue("" & FNLeaveDay.Properties.Tag.ToString, FNLeaveDay.SelectedIndex) & "'"
                    _Qry &= vbCrLf & "," & Me.FTNetDay.Value & " "
                    _Qry &= vbCrLf & ",'" & FTStateMedicalCertificate.EditValue.ToString & "'"
                    _Qry &= vbCrLf & ",'" & Me.FTStateDeductVacation.EditValue.ToString & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    If _LeaveCode = "97" And FTStateLeavepay.Checked = True Then
                        If Not (_LeavePragNentNotPay) Then
                            _LeavePragNentPay = _LeavePragNentPay - 1
                        End If
                    End If

                End If

                HI.HRCAL.Calculate.CalculateWorkTime(HI.ST.UserInfo.UserName, Me.FNHSysEmpId.Properties.Tag.ToString, _NextProcDate, _NextProcDate)

                _NextProcDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_NextProcDate, 1))

            Loop

            HI.HRCAL.Calculate.DisposeObject()

            _dtWeekend.Dispose()
            _dtHoliday.Dispose()

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            Dim _Qry As String


            _Qry = "DELETE FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily"
            _Qry &= vbCrLf & " WHERE FNHSysEmpId = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
            _Qry &= vbCrLf & " AND FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            _Qry &= vbCrLf & " AND FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            _Qry &= vbCrLf & " AND FTLeaveType = '" & HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) & "'"
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave "
            _Qry &= vbCrLf & " WHERE FNHSysEmpId = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
            _Qry &= vbCrLf & " AND FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            _Qry &= vbCrLf & " AND FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            _Qry &= vbCrLf & " AND FTLeaveType = '" & HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) & "'"
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            Dim _EndProcDate As String = HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text)
            Dim _NextProcDate As String = ""
            Dim nNextDay As Double = 0
            _NextProcDate = HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text)
            Do While _NextProcDate <= _EndProcDate

                HI.HRCAL.Calculate.CalculateWorkTime(HI.ST.UserInfo.UserName, Me.FNHSysEmpId.Properties.Tag.ToString, _NextProcDate, _NextProcDate)

                _NextProcDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_NextProcDate, 1))
            Loop
            HI.HRCAL.Calculate.DisposeObject()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function VerrifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNHSysEmpId.Text <> "" And FNHSysEmpId.Properties.Tag.ToString <> "" Then
            If HI.UL.ULDate.CheckDate(FTStartDate.Text) <> "" And HI.UL.ULDate.CheckDate(FTEndDate.Text) <> "" Then
                If Me.FTSTime.Text <> "" And Me.FTETime.Text <> "" Then
                    If Me.FTSTime.Text <> Me.FTETime.Text Then
                        If Me.FTNetDay.Value > 0 Then
                            _Pass = True
                        Else
                            HI.MG.ShowMsg.mInvalidData("ไม่พบจำนวนวันลา  กรุณาทำการระบุข้อมูลวันให้ถูกต้อง !!!", 1304050001, Me.Text)
                            FTEndDate.Focus()
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData("กรุณาทำการระบุข้อมูลเวลา !!!", 1304050002, Me.Text)
                        Me.FTETime.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData("กรุณาทำการระบุข้อมูลเวลา !!!", 1304050002, Me.Text)

                    If Me.FTSTime.Text = "" Then
                        Me.FTSTime.Focus()
                    ElseIf Me.FTETime.Text = "" Then
                        Me.FTETime.Focus()
                    End If
                End If
            Else
                HI.MG.ShowMsg.mInvalidData("ไม่พบจำนวนวันลา  กรุณาทำการระบุข้อมูลวันให้ถูกต้อง !!!", 1304050001, Me.Text)
                If HI.UL.ULDate.CheckDate(FTStartDate.Text) = "" Then
                    FTStartDate.Focus()
                ElseIf HI.UL.ULDate.CheckDate(FTEndDate.Text) = "" Then
                    FTEndDate.Focus()
                End If
            End If
        Else

            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysEmpId_lbl.Text)
            FNHSysEmpId.Focus()

        End If

        Return _Pass
    End Function

    Private Sub LoadDataEmployeeShift()
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "  SELECT   TOP 1     M.FNHSysEmpID, S.FTIn1, S.FTOut1, S.FTIn2, S.FTOut2, S.FCHour,M.FNHSysShiftID"
        _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS S WITH (NOLOCK) ON M.FNHSysShiftID = S.FNHSysShiftID"
        _Qry &= vbCrLf & "   WHERE M.FNHSysEmpID=" & Val(FNHSysEmpId.Properties.Tag.ToString) & " "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.FTIn1.Text = ""
        Me.FTOut1.Text = ""
        Me.FTIn2.Text = ""
        Me.FTOut2.Text = ""
        Me.FTIn1M.Text = ""
        Me.FTOut1M.Text = ""
        Me.FTIn2M.Text = ""
        Me.FTOut2M.Text = ""
        Me.FNHSysShiftID.Text = ""

        If _dt.Rows.Count > 0 Then

            For Each R As DataRow In _dt.Rows

                Me.FNHSysShiftID.Text = R!FNHSysShiftID.ToString

                Me.FTIn1.Text = R!FTIn1.ToString
                Me.FTOut1.Text = R!FTOut1.ToString
                Me.FTIn2.Text = R!FTIn2.ToString
                Me.FTOut2.Text = R!FTOut2.ToString

                Me.FTIn1M.Text = R!FTIn1.ToString
                Me.FTOut1M.Text = R!FTOut1.ToString
                Me.FTIn2M.Text = R!FTIn2.ToString
                Me.FTOut2M.Text = R!FTOut2.ToString

                Exit For
            Next

        End If

        Call LoadDataEmployeeMoveShift()

    End Sub

    Private Sub LoadDataEmployeeMoveShift()
        Dim _Qry As String = ""
        Dim _dt As DataTable


        _Qry = "   SELECT        M.FNHSysEmpID, S.FTIn1, S.FTOut1, S.FTIn2, S.FTOut2, S.FCHour"
        _Qry &= vbCrLf & " FROM     THRMEmployeeMoveShift AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "   THRMTimeShift AS S WITH (NOLOCK) ON M.FNHSysShiftID = S.FNHSysShiftID"
        _Qry &= vbCrLf & " WHERE  M.FNHSysEmpID =" & Val(FNHSysEmpId.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & " AND M.FDShiftDate ='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "' "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.FTIn1M.Text = Me.FTIn1.Text
        Me.FTOut1M.Text = Me.FTOut1.Text
        Me.FTIn2M.Text = Me.FTIn2.Text
        Me.FTOut2M.Text = Me.FTOut2.Text

        For Each R As DataRow In _dt.Rows
            Me.FTIn1M.Text = R!FTIn1.ToString
            Me.FTOut1M.Text = R!FTOut1.ToString
            Me.FTIn2M.Text = R!FTIn2.ToString
            Me.FTOut2M.Text = R!FTOut2.ToString
            Exit For
        Next

        Call FNLeaveDay_SelectedIndexChanged(FNLeaveDay, New System.EventArgs)

    End Sub

    Private Sub SetEnableLeavePay()
        If ocmapprove.Visible Then
            'FTStateLeavepay.Enabled = (ocmapprove.Visible)
            FTStateCalSSo.Enabled = (ocmapprove.Visible)
            FTStateMedicalCertificate.Enabled = (ocmapprove.Visible)
        Else
            ' FTStateLeavepay.Enabled = True
            FTStateCalSSo.Enabled = True
            FTStateMedicalCertificate.Enabled = True
        End If
    End Sub
#End Region

#Region "General"

#End Region

    Private Sub PrepareDatatable()


        oDbdtLeave = New DataTable
        oDbdtLeave.Columns.Add("FTLeaveCode", GetType(String))
        oDbdtLeave.Columns.Add("FTLeaveName", GetType(String))
        oDbdtLeave.Columns.Add("FNLeaveRight", GetType(Integer))
        oDbdtLeave.Columns.Add("FNLeaveUsed", GetType(Integer))
        oDbdtLeave.Columns.Add("FNLeaveBal", GetType(Integer))
        ogdLeave.DataSource = oDbdtLeave

    End Sub

    Protected Sub ShowLeaveInfo(ByVal EmpCode As String)


        Dim _Qry As String
        Dim tResetLeave As String

        Try
            Dim tEmpType As String = FNHSysEmpTypeId.Properties.Tag.ToString

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US", True)
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd"
            Dim sDate As String = Year(Date.Today) & "/" & Month(Date.Today) & "/" & Microsoft.VisualBasic.DateAndTime.Day(Date.Today)
            Dim eDate As String = Year(Date.Today) & "/" & Month(Date.Today) & "/" & Microsoft.VisualBasic.DateAndTime.Day(Date.Today)

            If Year(Date.Today) > 2400 Then
                sDate = Year(Date.Today) - 543 & "/" & Month(Date.Today).ToString.PadLeft(2, "0") & "/" & Microsoft.VisualBasic.DateAndTime.Day(Date.Today).ToString.PadLeft(2, "0")
            Else
                sDate = Year(Date.Today) & "/" & Month(Date.Today).ToString.PadLeft(2, "0") & "/" & Microsoft.VisualBasic.DateAndTime.Day(Date.Today).ToString.PadLeft(2, "0")
            End If

            Dim FDDateStart As String = ""
            Dim FDDateEnd As String = ""
            Dim FDDateProbation As String = ""
            FDDateProbation_lbl.Text = ""
            _Qry = "SELECT FDDateStart, FDDateEnd , FDDateProbation"
            _Qry &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(EmpCode) & " "
            Dim _dt As DataTable
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            If _dt.Rows.Count > 0 Then

                For Each R As DataRow In _dt.Rows

                    FDDateStart = R!FDDateStart.ToString
                    FDDateEnd = R!FDDateEnd.ToString
                    FDDateProbation = R!FDDateProbation.ToString
                    FDDateProbation_lbl.Text = R!FDDateProbation.ToString
                    Exit For
                Next

            End If


            sDate = HI.UL.ULDate.ConvertEnDB(FDDateStart)
            eDate = HI.UL.ULDate.ConvertEnDB(FDDateEnd)

            'Dim oDbdt As DataTable

            Dim nYear As Integer = 0
            Dim nMonth As Integer = 0
            _Qry = "SP_Datediff '" & sDate & "',N'" & eDate & "'"
            Dim oRow As DataRow = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR).Rows(0)
            nYear = oRow("FNYear")
            nMonth = (nYear * 12) + oRow("FNMonth")

            Dim LeaveVacation As Double = 0

            _Qry = "   SELECT  TOP 1  dbo.FN_Get_Emp_Vacation(FNHSysEmpID,FNHSysEmpTypeId,ISNULL(FDDateStart,N''),ISNULL(FDDateEnd,N''),ISNULL(FDDateProbation,N'')) AS FNEmpVacation"
            _Qry &= vbCrLf & "   FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMEmployee  AS M WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID=" & Val(EmpCode) & " "

            LeaveVacation = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))

            _Qry = " SELECT CASE WHEN RiGHT(FTCurrenDate,5) >=FTLeaveReset THEN LEFT(FTCurrenDate,4) ELSE  LEFT(FTBefore,4)  END +'/' + FTLeaveReset AS FTLeaveReset"
            _Qry &= vbCrLf & "  FROM"
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT  TOP 1 Convert(varchar(10),GetDate(),111)  AS FTCurrenDate ,Convert(varchar(10),DateAdd(YEAR,-1,GetDate()),111) AS FTBefore,L.FTLeaveReset"
            _Qry &= vbCrLf & " FROM  THRMConfigLeave  AS L WITH (NOLOCK)  INNER JOIN THRMEmployee AS M WITH(NOLOCK )"
            _Qry &= vbCrLf & "  ON  L.FNHSysEmpTypeId=M.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "  WHERE   M.FNHSysEmpID=" & Val(EmpCode) & " "
            _Qry &= vbCrLf & " ) As T"

            tResetLeave = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")


            '_Qry = " SELECT  (( Convert(varchar(30),Convert(numeric(18,0),Floor(SUM(ISNULL(FNAbsent,0))/ 480.00))))"
            '_Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((SUM(ISNULL(FNAbsent,0)) % 480.00) / 60.00))),2)"
            '_Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((SUM(ISNULL(FNAbsent,0)) % 480.00) % 60.00))),2))  AS FNAbsent"
            '_Qry &= vbCrLf & " FROM THRTTrans WITH(Nolock)"
            '_Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(EmpCode) & "  "
            '_Qry &= vbCrLf & " AND FTDateTrans>=N'" & tResetLeave & "' AND FTDateTrans<=Convert(varchar(10),Getdate(),111)"
            'FTAbsentH.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")

            '_Qry = " SELECT  (( Convert(varchar(30),Convert(numeric(18,0),Floor(SUM(ISNULL(FNLateNormalMin,0))/ 480.00))))"
            '_Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((SUM(ISNULL(FNLateNormalMin,0)) % 480.00) / 60.00))),2)"
            '_Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((SUM(ISNULL(FNLateNormalMin,0)) % 480.00) % 60.00))),2))  AS FNLateNormalMin"
            '_Qry &= vbCrLf & " FROM THRTTrans WITH(Nolock)"
            '_Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(EmpCode) & "  "
            '_Qry &= vbCrLf & " AND FTDateTrans>=N'" & tResetLeave & "'  AND FTDateTrans<=Convert(varchar(10),Getdate(),111) "
            'FTLateMin.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")


            Dim _baseWorkMin As String = "480.00"
            _Qry = " SELECT Convert(numeric(18,2),FCHour*60) AS n FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMTimeShift WITH(NOLOCK) WHERE FTShiftCode=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysShiftID.Text) & "' "
            _baseWorkMin = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "480.00").ToString


            _Qry = "SELECT FTLeaveCode,FTLeaveName"

            _Qry &= vbCrLf & ", (( Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveRight,0))/ " & _baseWorkMin & "))))"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((ISNULL(FNLeaveRight,0)) % " & _baseWorkMin & ") / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((ISNULL(FNLeaveRight,0)) % " & _baseWorkMin & ") % 60.00))),2))  AS FNLeaveRight"

            _Qry &= vbCrLf & " , (( Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveUsed,0))/ " & _baseWorkMin & "))))"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((ISNULL(FNLeaveUsed,0)) % " & _baseWorkMin & ") / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((ISNULL(FNLeaveUsed,0)) % " & _baseWorkMin & ") % 60.00))),2))  AS FNLeaveUsed"

            _Qry &= vbCrLf & " , ((Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveBal,0))/ " & _baseWorkMin & "))))"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((ISNULL(FNLeaveBal,0)) % " & _baseWorkMin & ") / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((ISNULL(FNLeaveBal,0)) % " & _baseWorkMin & ") % 60.00))),2))  AS FNLeaveBal"

            _Qry &= vbCrLf & " FROM  (SELECT V_LeaveType.FTLeaveCode,FTLeaveName"

            _Qry &= vbCrLf & ",Cast((ISNULL(FNLeaveRight,0) * " & _baseWorkMin & ") AS numeric(18,0)) AS FNLeaveRight"
            _Qry &= vbCrLf & ",ISNULL(FNTotalMinute,0) AS FNLeaveUsed"
            _Qry &= vbCrLf & ",(Cast((ISNULL(FNLeaveRight,0) * " & _baseWorkMin & ") AS numeric(18,0))) - ISNULL(FNTotalMinute,0)   AS FNLeaveBal"

            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT CAST(FNListIndex AS varchar(3)) AS FTLeaveCode," & IIf(HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal, "FTNameTH", "FTNameEN") & " AS FTLeaveName "
            _Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.V_LeaveType WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNListIndex<>98"
            _Qry &= vbCrLf & ") AS V_LeaveType"
            _Qry &= vbCrLf & " Left Join"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " Select THRMConfigLeave.FTLeaveCode"
            _Qry &= vbCrLf & ",CASE WHEN ISNULL(THRMEmployeeLeave.FNLeaveRight,-1)=-1 THEN Cast(ISNULL(THRMConfigLeave.FNLeaveRight,0) AS numeric(18,0)) ELSE Cast(ISNULL(THRMEmployeeLeave.FNLeaveRight,0) AS numeric(18,0)) END AS FNLeaveRight"
            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FTLeaveCode,FNLeaveRight "
            _Qry &= vbCrLf & " FROM THRMConfigLeave WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysEmpTypeId=" & Val(tEmpType) & " "
            _Qry &= vbCrLf & ") THRMConfigLeave"
            _Qry &= vbCrLf & " Left Join"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FTLeaveCode,Cast(ISNULL(FNLeaveRight,0) AS numeric(18,2)) AS FNLeaveRight "
            _Qry &= vbCrLf & " FROM THRMEmployeeLeave WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(EmpCode) & " "
            _Qry &= vbCrLf & ") THRMEmployeeLeave"
            _Qry &= vbCrLf & " ON THRMConfigLeave.FTLeaveCode=THRMEmployeeLeave.FTLeaveCode"
            _Qry &= vbCrLf & ") T ON V_LeaveType.FTLeaveCode=T.FTLeaveCode"
            _Qry &= vbCrLf & " LEFT JOIN "
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FTLeaveType,SUM(FNTotalMinute) AS FNTotalMinute "
            _Qry &= vbCrLf & " FROM THRTTransLeave  WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  FTLeaveType<>N'98'"
            _Qry &= vbCrLf & " AND FNHSysEmpID=" & Val(EmpCode) & " "
            _Qry &= vbCrLf & " AND FTDateTrans>=N'" & tResetLeave & "'"
            _Qry &= vbCrLf & " GROUP BY FTLeaveType"
            _Qry &= vbCrLf & ") AS THRTTransLeave"
            _Qry &= vbCrLf & " ON V_LeaveType.FTLeaveCode=THRTTransLeave.FTLeaveType) AS MM1"

            _Qry &= vbCrLf & " UNION "
            _Qry &= vbCrLf & "SELECT FTLeaveCode,FTLeaveName"

            _Qry &= vbCrLf & " , (Right( Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveRight,0))/ " & _baseWorkMin & "))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((ISNULL(FNLeaveRight,0)) % " & _baseWorkMin & ") / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((ISNULL(FNLeaveRight,0)) % " & _baseWorkMin & ") % 60.00))),2))  AS FNLeaveRight"

            _Qry &= vbCrLf & ", (Right( Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveUsed,0))/ " & _baseWorkMin & "))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((ISNULL(FNLeaveUsed,0)) % " & _baseWorkMin & ") / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((ISNULL(FNLeaveUsed,0)) % " & _baseWorkMin & ") % 60.00))),2))  AS FNLeaveUsed"

            _Qry &= vbCrLf & " , (Right( Convert(varchar(30),Convert(numeric(18,0),Floor((ISNULL(FNLeaveBal,0) - (ISNULL(FNLeaveBal,0) % 480) )/ " & _baseWorkMin & "))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(((ISNULL(FNLeaveBal,0)) % " & _baseWorkMin & ") / 60.00))),2)"
            _Qry &= vbCrLf & "  +':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),(((ISNULL(FNLeaveBal,0)) % " & _baseWorkMin & ") % 60.00))),2))  AS FNLeaveBal"


            _Qry &= vbCrLf & " FROM (SELECT  V_LeaveType.FTLeaveCode,FTLeaveName"

            _Qry &= vbCrLf & ",Cast((ISNULL(FNLeaveRight," & LeaveVacation & ") * " & _baseWorkMin & ")  AS numeric(18,0)) AS FNLeaveRight"
            _Qry &= vbCrLf & ",ISNULL(FNTotalMinute,0) AS FNLeaveUsed"
            _Qry &= vbCrLf & ",(Cast((ISNULL(FNLeaveRight," & LeaveVacation & ") * " & _baseWorkMin & ")  AS numeric(18,0))) -ISNULL(FNTotalMinute,0)   AS FNLeaveBal"


            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT CAST(FNListIndex AS varchar(3)) AS FTLeaveCode," & IIf(HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal, "FTNameTH", "FTNameEN") & " AS FTLeaveName "
            _Qry &= vbCrLf & " FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.V_LeaveType WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNListIndex=98"
            _Qry &= vbCrLf & ") AS V_LeaveType"
            _Qry &= vbCrLf & " Left Join"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " Select THRMConfigLeave.FTLeaveCode"
            _Qry &= vbCrLf & "," & LeaveVacation & " AS FNLeaveRight"
            _Qry &= vbCrLf & " FROM"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FTLeaveCode,FNLeaveRight "
            _Qry &= vbCrLf & " FROM THRMConfigLeave WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  FNHSysEmpTypeId=" & Val(tEmpType) & " "
            _Qry &= vbCrLf & ") THRMConfigLeave"
            _Qry &= vbCrLf & " Left Join"
            _Qry &= vbCrLf & "("
            _Qry &= vbCrLf & " SELECT FTLeaveCode," & LeaveVacation & " AS FNLeaveRight "
            _Qry &= vbCrLf & " FROM THRMEmployeeLeave WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(EmpCode) & " "
            _Qry &= vbCrLf & ") THRMEmployeeLeave"
            _Qry &= vbCrLf & " ON THRMConfigLeave.FTLeaveCode=THRMEmployeeLeave.FTLeaveCode"
            _Qry &= vbCrLf & ") T ON V_LeaveType.FTLeaveCode=T.FTLeaveCode"
            _Qry &= vbCrLf & " LEFT JOIN "
            _Qry &= vbCrLf & "("

            _Qry &= vbCrLf & " SELECT FTLeaveType,SUM(FNTotalMinute) AS FNTotalMinute "
            _Qry &= vbCrLf & " FROM THRTTransLeave  WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  FTLeaveType=N'98'"
            _Qry &= vbCrLf & " AND FNHSysEmpID=" & Val(EmpCode) & ""
            _Qry &= vbCrLf & " AND FTDateTrans>=N'" & tResetLeave & "'"
            _Qry &= vbCrLf & " GROUP BY FTLeaveType"
            _Qry &= vbCrLf & "union all "
            _Qry &= vbCrLf & " SELECT '98' as  FTLeaveType,SUM(FNTotalMinute) AS FNTotalMinute "
            _Qry &= vbCrLf & " FROM THRTTransLeave  WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE  FTLeaveType=N'999' AND isnull(FTStateDeductVacation,'0') = '1' "
            _Qry &= vbCrLf & " AND FNHSysEmpID=" & Val(EmpCode) & ""
            _Qry &= vbCrLf & " AND FTDateTrans>=N'" & tResetLeave & "'"
            _Qry &= vbCrLf & " GROUP BY FTLeaveType"

            _Qry &= vbCrLf & ") AS THRTTransLeave"
            _Qry &= vbCrLf & " ON V_LeaveType.FTLeaveCode=THRTTransLeave.FTLeaveType) AS MM2"

            ogdLeave.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Catch ex As Exception
        End Try

    End Sub

    Private TotalLeave As Integer
    Private Sub FNLeaveDay_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNLeaveDay.SelectedIndexChanged


        If (_ProcPrepare) Then Exit Sub
        Select Case FNLeaveDay.SelectedIndex
            Case 3

                FTSTime.Properties.ReadOnly = False
                FTETime.Properties.ReadOnly = False

            Case Else

                FTSTime.Properties.ReadOnly = True
                FTETime.Properties.ReadOnly = True

                Select Case FNLeaveDay.SelectedIndex
                    Case 0
                        Me.FTSTime.Text = Me.FTIn1M.Text
                        Me.FTETime.Text = Me.FTOut2M.Text
                    Case 1
                        Me.FTSTime.Text = Me.FTIn1M.Text
                        Me.FTETime.Text = Me.FTOut1M.Text
                    Case 2
                        Me.FTSTime.Text = Me.FTIn2M.Text
                        Me.FTETime.Text = Me.FTOut2M.Text
                End Select

        End Select
        Call Checkpay(HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex))
        Call SetEnableLeavePay()
    End Sub

    Private Sub wLeave_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _ProcClick = False
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub

    Private Sub LoadDataEmployeeLeaveHistory()
        Dim _Qry As String = ""
        Dim _dt As DataTable

        '_Qry = "SELECT Convert(varchar(10),Convert(datetime,FTStartDate),103) As FTStartDate"
        '_Qry &= vbCrLf & ", Convert(varchar(10),Convert(datetime,FTEndDate),103) As FTEndDate"
        _Qry = "SELECT Convert(datetime,A.FTStartDate) As FTStartDate"
        _Qry &= vbCrLf & ", Convert(datetime,A.FTEndDate) As FTEndDate"
        _Qry &= vbCrLf & ",CASE WHEN ISNULL(A.FTHoliday,'0') = '1' THEN  '1'  ELSE '0'  END AS  FTHoliday"
        _Qry &= vbCrLf & ",A.FTLeaveType"
        _Qry &= vbCrLf & ", A.FNLeaveTotalDay, B.FTNameTH AS FTLeaveTypeName"
        _Qry &= vbCrLf & ", CASE WHEN ISNULL(FTLeavePay,'0') = '1' THEN '1' ELSE '0' END AS FTLeavePay"
        _Qry &= vbCrLf & ", FTLeaveStartTime , FTLeaveEndTime, FNLeaveTotalTime, FTLeaveNote,A.FTLeaveType AS FTLeaveTypeCode"
        _Qry &= vbCrLf & ",CASE WHEN ISNULL(FTApproveState,'0') = '1' THEN  '1'  ELSE '0'  END AS  FTApproveState"
        _Qry &= vbCrLf & ",CASE WHEN ISNULL(FTStaCalSSO,'0') = '1' THEN  '1'  ELSE '0'  END AS  FTStaCalSSO"
        _Qry &= vbCrLf & "  ,CASE WHEN ISNULL(FTStaLeaveDay,'-1') <='0' THEN '0' ELSE FTStaLeaveDay END As FTStaLeaveDay,ISNULL(A.FTStateDeductVacation,'0') AS FTStateDeductVacation"

        _Qry &= vbCrLf & " , ISNULL(A.FTMngApproveState,'0') AS FTMngApproveState "
        _Qry &= vbCrLf & " , ISNULL(A.FTMngApproveBy,'') AS FTMngApproveBy "
        _Qry &= vbCrLf & " , Convert(nvarchar(10) ,convert(datetime,A.FDMngApproveDate) , 103) AS FDMngApproveDate "
        _Qry &= vbCrLf & " , ISNULL(A.FTMngApproveTime,'') AS FTMngApproveTime "

        _Qry &= vbCrLf & " , ISNULL(A.FTSendApproveState,'0') AS FTSendApproveState "
        _Qry &= vbCrLf & " , ISNULL(A.FTSendApproveBy,'') AS FTSendApproveBy "
        _Qry &= vbCrLf & " , Convert(nvarchar(10) ,convert(datetime,A.FDSendApproveDate) , 103) AS FDSendApproveDate "
        _Qry &= vbCrLf & " , ISNULL(A.FTSendApproveTime,'') AS FTSendApproveTime "

        '_Qry &= vbCrLf & ",CASE WHEN ISNULL(A.FTSendApproveState,'0') = '1' THEN '1' ELSE '0' END AS FTSendApproveState,Convert(datetime,A.FDSendApproveDate) As FDSendApproveDate,FTSendApproveTime,FTSendApproveBy"

        '_Qry &= vbCrLf & ",CASE WHEN ISNULL(FTMngApproveState,'0') = '1' THEN  '1'  ELSE '0'  END AS  FTMngApproveState,Convert(datetime,A.FDMngApproveDate) AS FDMngApproveDate,FTMngApproveTime,FTMngApproveBy"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & " , ISNULL(C.FTNameTH,'') AS FTStaLeaveDayName "
        Else
            _Qry &= vbCrLf & " , ISNULL(C.FTNameEN,'') AS FTStaLeaveDayName "
        End If


        _Qry &= vbCrLf & " , ISNULL(A.FTDirApproveState,'0') AS FTDirApproveState "
        _Qry &= vbCrLf & " , ISNULL(A.FTDirApproveBy,'') AS FTDirApproveBy "
        _Qry &= vbCrLf & " , Convert(nvarchar(10) ,convert(datetime,A.FDDirApproveDate) , 103) AS FDDirApproveDate "
        _Qry &= vbCrLf & " , ISNULL(A.FTDirApproveTime,'') AS FTDirApproveTime "

        _Qry &= vbCrLf & " , ISNULL(A.FTStateMedicalCertificate,'0') AS FTStateMedicalCertificate "
        _Qry &= vbCrLf & " , ISNULL(A.FTUpdUser,A.FTInsUser) AS FTInsUser "
        _Qry &= vbCrLf & " ,CASE WHEN ISNULL(A.FTUpdUser,'') ='' THEN A.FTInsDate ELSE A.FTUpdDate  END AS FTInsDate "
        _Qry &= vbCrLf & " ,CASE WHEN ISNULL(A.FTUpdUser,'') ='' THEN A.FTInsTime ELSE A.FTUpdTime END  AS FTInsTime "

        _Qry &= vbCrLf & "  FROM dbo.THRTLeaveAdvanceDaily As A WITH(NOLOCK) Left Outer Join (SELECt * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS LT WITH(NOLOCK) WHERE FTListName='FNLeaveType' ) As B ON A.FTLeaveType = Convert(varchar(50),B.FNListIndex) "
        _Qry &= vbCrLf & "  Left Outer Join (SELECt * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS LD WITH(NOLOCK)  WHERE FTListName='FNLeaveDay' ) As C ON A.FTStaLeaveDay = Convert(varchar(50),C.FNListIndex) "
        _Qry &= vbCrLf & "  WHERE FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""

        _Qry &= vbCrLf & "ORDER BY A.FTStartDate DESC"
        'FNLeaveSickType'
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        Me.ogc.DataSource = _dt

    End Sub

    Private Sub Checkpay(ByVal leavekey As String, Optional ByVal SetCheckPay As Boolean = True)
        Dim _Qry As String
        Dim _dt As DataTable
        Dim _DateReset As String
        Dim _MsgRet As String = ""
        Dim _Msg As String = ""
        Dim _Leave As Double = 0
        Dim _LeavePay As Double = 0
        Dim _GLeave As Double = 0
        Dim _GLeavePay As Double = 0
        Dim _Month As Integer = 0
        Dim _FTStaHoliday As String = "0"

        Me.FNSpecialLeaveType.Visible = (leavekey = "999")

        Dim _CalType As String = ""
        _Qry = " SELECT  ET.FNCalType "
        _Qry &= vbCrLf & " FROM            THRMEmployee AS M INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId"
        _Qry &= vbCrLf & " WHERE (M.FNHSysEmpID =" & Val(Me.FNHSysEmpId.Properties.Tag.ToString) & ") "

        _CalType = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

        _Qry = " SELECT CASE WHEN RiGHT(FTCurrenDate,5) >=FTLeaveReset THEN LEFT(FTCurrenDate,4) ELSE  LEFT(FTBefore,4)  END +'/' + FTLeaveReset"
        _Qry &= vbCrLf & "  FROM"
        _Qry &= vbCrLf & " ("
        _Qry &= vbCrLf & " SELECT  TOP 1 Convert(varchar(10),GetDate(),111)  AS FTCurrenDate ,Convert(varchar(10),DateAdd(YEAR,-1,GetDate()),111) AS FTBefore,L.FTLeaveReset"
        _Qry &= vbCrLf & " FROM            THRMConfigLeave  AS L WITH (NOLOCK)  INNER JOIN THRMEmployee AS M WITH(NOLOCK )"
        _Qry &= vbCrLf & "  ON  L.FNHSysEmpTypeId=M.FNHSysEmpTypeId"
        _Qry &= vbCrLf & "  WHERE   M.FNHSysEmpID=" & Val(Me.FNHSysEmpId.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & " ) As T"

        _DateReset = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

        Dim _FTStateDeductVacation As String = ""

        _Qry = " SELECT TOP 1   L.FTLeaveReset, "
        _Qry &= vbCrLf & "   L.FNLeaveRight, L.FNLeavePay,ISNULL(L.FTStaHoliday,'') AS FTStaHoliday,ISNULL(L.FTStateDeductVacation,'0') AS FTStateDeductVacation "
        _Qry &= vbCrLf & " FROM            THRMConfigLeave  AS L WITH (NOLOCK)  INNER JOIN THRMEmployee AS M WITH(NOLOCK )"
        _Qry &= vbCrLf & "  ON  L.FNHSysEmpTypeId=M.FNHSysEmpTypeId"
        _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID=" & Val(Me.FNHSysEmpId.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & " AND L.FTLeaveCode='" & HI.UL.ULF.rpQuoted(leavekey) & "'"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        For Each R As DataRow In _dt.Rows
            _Leave = Val(R!FNLeaveRight.ToString)
            _LeavePay = Val(R!FNLeavePay.ToString)
            _FTStaHoliday = R!FTStaHoliday.ToString
            _FTStateDeductVacation = R!FTStateDeductVacation.ToString
        Next

        Me.FTStateDeductVacation.Checked = (_FTStateDeductVacation = "1")

        If leavekey = "98" Then

            _Qry = "   SELECT  TOP 1  dbo.FN_Get_Emp_Vacation(FNHSysEmpID,FNHSysEmpTypeId,ISNULL(FDDateStart,''),ISNULL(FDDateEnd,''),ISNULL(FDDateProbation,'')) AS FNEmpVacation"
            _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee  AS M WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID=" & Val(Me.FNHSysEmpId.Properties.Tag.ToString) & " "

            _Leave = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))


            _LeavePay = _Leave

        End If

        Dim _baseWorkMin As String = "480.00"
        _Qry = " SELECT Convert(numeric(18,2),FCHour*60) AS n FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRMTimeShift WITH(NOLOCK) WHERE FNHSysShiftID='" & HI.UL.ULF.rpQuoted(Me.FNHSysShiftID.Text) & "' "
        _baseWorkMin = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "480").ToString
        LabelControl8.Text = _baseWorkMin
        '_Leave = _Leave * 480
        '_LeavePay = _LeavePay * 480

        _Leave = _Leave * _baseWorkMin
        _LeavePay = _LeavePay * _baseWorkMin
        LabelControl1.Text = _Leave
        _GLeave = 0
        _GLeavePay = 0

        _Qry = "  SELECT        SUM(FNTotalMinute) AS FNTotalMinute,Sum(FNTotalPayMinute) As FNTotalPayMinute "
        _Qry &= vbCrLf & "   FROM THRTTransLeave WITH (NOLOCK)"
        _Qry &= vbCrLf & " WHERE (FNHSysEmpID = " & Val(Me.FNHSysEmpId.Properties.Tag.ToString) & ") "
        _Qry &= vbCrLf & " AND (FTDateTrans >= N'" & _DateReset & "')"
        _Qry &= vbCrLf & " AND (FTLeaveType = '" & HI.UL.ULF.rpQuoted(leavekey) & "')"
        _Qry &= vbCrLf & " AND FTDateTrans <'" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "' "

        If leavekey = "0" Then
            _Qry &= vbCrLf & " AND (ISNULL(FNLeaveSickType,0) = 1  OR  (ISNULL(FNLeaveSickType,0)=0 AND FNTotalPayMinute >0 ))  "
        End If

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        For Each R As DataRow In _dt.Rows
            _GLeave = Val(R!FNTotalMinute.ToString)
            _GLeavePay = Val(R!FNTotalPayMinute.ToString)
        Next

        If leavekey = "98" Then

            '_Qry = "  SELECT  SUM(FNTotalMinute) AS FNTotalMinute,Sum(FNTotalPayMinute) As FNTotalPayMinute "
            '_Qry &= vbCrLf & "   FROM THRTTransLeave WITH (NOLOCK)"
            '_Qry &= vbCrLf & " WHERE (FNHSysEmpID = " & Val(Me.FNHSysEmpId.Properties.Tag.ToString) & ") "
            '_Qry &= vbCrLf & " AND (FTDateTrans >= N'" & _DateReset & "')"
            '_Qry &= vbCrLf & " AND FTDateTrans <'" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "' "
            '_Qry &= vbCrLf & " AND isnull(FTStateDeductVacation,'0') = '1'"
            '* ปรับเรื่องลาพักร้อนเกิน ในกรณีที่ลาแล้วยังไม่ได้ อนุมัติ.... 20190523

            _Qry = "Select   SUM(FNLeaveTotalDay * FNLeaveTotalTimeMin) AS FNTotalMinute "
            _Qry &= vbCrLf & "   FROM THRTLeaveAdvanceDaily WITH (NOLOCK)"
            _Qry &= vbCrLf & " WHERE (FNHSysEmpID = " & Val(Me.FNHSysEmpId.Properties.Tag.ToString) & ") "
            _Qry &= vbCrLf & " AND (FTStartDate >= N'" & _DateReset & "')"
            _Qry &= vbCrLf & " AND FTLeaveType='98'"
            '_Qry &= vbCrLf & " AND FTDateTrans <'" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "' "
            _Qry &= vbCrLf & " OR (FTLeaveType <> '98' and  isnull(FTStateDeductVacation,'0' ) = '1'  and   FTStartDate >= N'" & _DateReset & "' and FNHSysEmpID = " & Val(Me.FNHSysEmpId.Properties.Tag.ToString) & ")"


            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            For Each R As DataRow In _dt.Rows
                Dim totalmin As Double = Double.Parse("0" & R!FNTotalMinute.ToString)
                _Leave = _Leave - totalmin
            Next

        End If
        If (SetCheckPay) Then
            If _Leave < _GLeave Then

                HI.MG.ShowMsg.mProcessError(1004090001, "พบข้อมูลการลาเกินกำหนด !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Information)
            End If
        End If
        LabelControl7.Text = _GLeave

        If leavekey = "98" Then
            TotalLeave = _Leave
        Else
            TotalLeave = (_Leave - _GLeave)
        End If

        If TotalLeave <= 0 Then
            TotalLeave = 0
        Else
            TotalLeave = TotalLeave '/ 480
        End If

        LabelControl6.Text = TotalLeave

        Try
            If leavekey = "97" Then
            Else
                _GLeave = _GLeave + (FTNetDay.Value * ocetotaltime.Value)
            End If
        Catch ex As Exception
        End Try

        Dim _leavePayAllow As String = ""
        ''FTStateLeavepay.Enabled = True
        If (SetCheckPay) Then
            If leavekey = "0" Then 'ลาป่วย

                If FNHSysEmpTypeId.Text = "M" Or FNHSysEmpTypeId.Text = "N" Then
                    If Val(FTNetDay.Text) < 3 Then
                        _leavePayAllow = "y"
                    Else
                        If FTStateMedicalCertificate.Checked = True Then
                            _leavePayAllow = "y"
                        Else
                            FTStateLeavepay.Checked = False
                            '' FTStateLeavepay.Enabled = False
                        End If
                    End If
                Else ' D S Z
                    If FTStateMedicalCertificate.Checked = True Then
                        _leavePayAllow = "y"
                    Else
                        FTStateLeavepay.Checked = False
                        ''  FTStateLeavepay.Enabled = False
                    End If
                End If

                If ChkPayLeave(leavekey) And _leavePayAllow = "y" Then
                    FTStateLeavepay.Checked = (_LeavePay >= _GLeave)
                Else
                    FTStateLeavepay.Checked = False
                    '' FTStateLeavepay.Enabled = False

                End If

            ElseIf leavekey = "1" Then 'ลากิจ


                Dim prodate = FDDateProbation_lbl.Text
                Dim leaveStart = FTStartDate.Text

                prodate = HI.UL.ULDate.ConvertEnDB(prodate)
                leaveStart = HI.UL.ULDate.ConvertEnDB(leaveStart)

                prodate = prodate.Replace("/", "")
                leaveStart = leaveStart.Replace("/", "")
                If Val(leaveStart) > Val(prodate) Then
                    If (FNLeaveDay.SelectedIndex = 0) Then
                        _leavePayAllow = "y"
                    Else
                        FTStateLeavepay.Checked = False
                        ''  FTStateLeavepay.Enabled = False
                    End If

                End If


                If ChkPayLeave(leavekey) And _leavePayAllow = "y" Then
                    FTStateLeavepay.Checked = (_LeavePay >= _GLeave)
                Else
                    FTStateLeavepay.Checked = False
                    ''  FTStateLeavepay.Enabled = False
                End If

            Else
                If ChkPayLeave(leavekey) Then
                    FTStateLeavepay.Checked = (_LeavePay >= _GLeave) '(_LeavePay >= (_GLeave + (FTNetDay.Value * ocetotaltime.Value)))
                Else
                    FTStateLeavepay.Checked = False
                End If


            End If

            FTStateNotMergeHoliday.Checked = ((_FTStaHoliday = "1") Or (leavekey = "98"))
            FTStateCalSSo.Checked = FTStateLeavepay.Checked
        End If

    End Sub

    Private Function ChkPayLeave(ByVal leavekey As String) As Boolean
        Try
            Dim _Cmd As String = ""
            If leavekey = "98" Then
                Return True
            Else
                _Cmd = "SELECT Top 1  L.FNLeavePay FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLeave AS L WITH(NOLOCK) INNER JOIN   "
                _Cmd &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH(NOLOCK) ON L.FNHSysEmpTypeId = E.FNHSysEmpTypeId"
                _Cmd &= vbCrLf & "WHERE E.FNHSysEmpID=" & Integer.Parse(Me.FNHSysEmpId.Properties.Tag.ToString) & " "
                _Cmd &= vbCrLf & " and FTLeaveCode = '" & HI.UL.ULF.rpQuoted(leavekey) & "'"
                Return Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "0")) > 0
            End If


        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub SumLeaveReport(ByRef sumBusinessLeave As String, ByRef sumSickLeave As String, ByRef sumVacationLeave As String, ByRef sumAbsent As String, ByRef sumLate As String, ByRef LeaveVacation As String, ByRef sumSpecailLeave As String, ByRef sumLeaveNotPaid As String, ByRef sumAccident As String)
        Try
            Dim _Qry As String
            Dim _dt As DataTable
            Dim _DateReset As String
            Dim _MsgRet As String = ""
            Dim _Msg As String = ""
            Dim _Month As Integer

            sumBusinessLeave = "0"
            sumSickLeave = "0"
            sumVacationLeave = "0"
            sumAbsent = "0"
            sumLate = "0"
            LeaveVacation = "0"

            If HI.UL.ULDate.CheckDate(FTStartDate.Text) <> "" Then
                _Qry = "  Select  DATEDIFF(MONTH,CONVERT(Datetime,FDDateStart),CONVERT(Datetime,'" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "'))"
            Else
                _Qry = "  Select  DATEDIFF(MONTH,CONVERT(Datetime,FDDateStart),GetDate())"
            End If

            _Qry &= vbCrLf & " FROM THRMEmployee AS M WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID=" & Val(Me.FNHSysEmpId.Properties.Tag.ToString) & " "

            _Month = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "-1"))

            If _Month >= 0 Then

                '_Qry = "    SELECT TOP 1  VC.FNLeaveRight, VC.FNAgeBegin, VC.FNAgeEnd,  VC.FNHSysEmpTypeId"
                '_Qry &= vbCrLf & " FROM     THRMConfigLeaveVacation AS VC WITH(NOLOCK) INNER JOIN  THRMEmployee AS M WITH(NOLOCK) ON  VC.FNHSysEmpTypeId=M.FNHSysEmpTypeId"
                '_Qry &= vbCrLf & "  WHERE   M.FNHSysEmpID='" & Val(Me.FNHSysEmpId.Properties.Tag.ToString) & "' "
                '_Qry &= vbCrLf & "  AND   VC.FNAgeBegin<=" & _Month & " "
                '_Qry &= vbCrLf & "  AND   VC.FNAgeEnd>=" & _Month & " "

                '_dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                'For Each R As DataRow In _dt.Rows
                '    LeaveVacation = Val(R!FNLeaveRight.ToString)
                'Next


                _Qry = "   SELECT  TOP 1  dbo.FN_Get_Emp_Vacation(FNHSysEmpID,FNHSysEmpTypeId,ISNULL(FDDateStart,''),ISNULL(FDDateEnd,''),ISNULL(FDDateProbation,'')) AS FNEmpVacation"
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee  AS M WITH(NOLOCK)"
                _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID=" & Val(Me.FNHSysEmpId.Properties.Tag.ToString) & " "

                LeaveVacation = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))

            End If

            _Qry = " SELECT CASE WHEN RiGHT(FTCurrenDate,5) >=FTLeaveReset THEN LEFT(FTCurrenDate,4) ELSE  LEFT(FTBefore,4)  END +'/' + FTLeaveReset"
            _Qry &= vbCrLf & "  FROM"
            _Qry &= vbCrLf & " ("
            _Qry &= vbCrLf & " SELECT  TOP 1 Convert(varchar(10),GetDate(),111)  AS FTCurrenDate ,Convert(varchar(10),DateAdd(YEAR,-1,GetDate()),111) AS FTBefore,L.FTLeaveReset"
            _Qry &= vbCrLf & " FROM            THRMConfigLeave  AS L WITH (NOLOCK)  INNER JOIN THRMEmployee AS M WITH(NOLOCK )"
            _Qry &= vbCrLf & "  ON  L.FNHSysEmpTypeId=M.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "  WHERE   M.FNHSysEmpID=" & Val(Me.FNHSysEmpId.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & " ) As T"

            _DateReset = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

            _Qry = "  SELECT      FTLeaveType,  SUM(CASE WHEN FTDateTrans < Convert(varchar(10),Getdate(),111) THEN  FNTotalMinute ELSE 0 END ) As FNTotalMinute "
            _Qry &= vbCrLf & "   FROM THRTTransLeave With (NOLOCK)"
            _Qry &= vbCrLf & " WHERE (FNHSysEmpID = " & Val(Me.FNHSysEmpId.Properties.Tag.ToString) & ") "
            _Qry &= vbCrLf & " And (FTDateTrans >= N'" & _DateReset & "')"
            _Qry &= vbCrLf & " AND (FTDateTrans < N'" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "')"
            _Qry &= vbCrLf & " Group By FTLeaveType "

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            sumBusinessLeave = "0"
            sumSickLeave = "0"
            sumVacationLeave = "0"
            sumLeaveNotPaid = "0"
            sumAccident = "0"
            Dim _Sum As Integer
            For Each R As DataRow In _dt.Rows

                _Sum = Integer.Parse(Val(R!FNTotalMinute.ToString))

                Select Case R!FTLeaveType.ToString

                    Case "0"
                        sumSickLeave = Format((_Sum \ 480), "00") & ":" & Format(((_Sum Mod 480) \ 60), "00") & ":" & Format(((_Sum Mod 480) Mod 60), "00") & ""

                    Case "1"
                        sumBusinessLeave = Format((_Sum \ 480), "00") & ":" & Format(((_Sum Mod 480) \ 60), "00") & ":" & Format(((_Sum Mod 480) Mod 60), "00") & ""

                    Case "98"
                        sumVacationLeave = Format((_Sum \ 480), "00") & ":" & Format(((_Sum Mod 480) \ 60), "00") & ":" & Format(((_Sum Mod 480) Mod 60), "00") & ""

                    Case "999"
                        sumSpecailLeave = Format((_Sum \ 480), "00") & ":" & Format(((_Sum Mod 480) \ 60), "00") & ":" & Format(((_Sum Mod 480) Mod 60), "00") & ""

                    Case "6"
                        sumLeaveNotPaid = Format((_Sum \ 480), "00") & ":" & Format(((_Sum Mod 480) \ 60), "00") & ":" & Format(((_Sum Mod 480) Mod 60), "00") & ""

                    Case "16"
                        sumAccident = Format((_Sum \ 480), "00") & ":" & Format(((_Sum Mod 480) \ 60), "00") & ":" & Format(((_Sum Mod 480) Mod 60), "00") & ""

                End Select

            Next

            '_Qry = "  SELECT    SUM(ISNULL(FNLateNormalMin,0)) AS FNLateNormalMin "
            '_Qry &= vbCrLf & " ,Sum(ISNULL(FNAbsent,0)) AS FNAbsent "

            _Qry = " SELECT    SUM( CASE WHEN FTDateTrans < Convert(varchar(10),Getdate(),111) THEN  ISNULL(FNLateNormalMin,0) ELSE 0 END  ) AS FNLateNormalMin "
            _Qry &= vbCrLf & " ,Sum(   CASE WHEN FTDateTrans < Convert(varchar(10),Getdate(),111) THEN  ISNULL(FNAbsent,0) ELSE 0 END   ) AS FNAbsent "
            _Qry &= vbCrLf & "   FROM THRTTrans WITH (NOLOCK)"
            _Qry &= vbCrLf & " WHERE (FNHSysEmpID = " & Val(Me.FNHSysEmpId.Properties.Tag.ToString) & ") "
            _Qry &= vbCrLf & " AND (FTDateTrans >= N'" & _DateReset & "')"
            _Qry &= vbCrLf & " AND (FTDateTrans < N'" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "')"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            For Each R As DataRow In _dt.Rows

                _Sum = Integer.Parse(Val(R!FNAbsent.ToString))

                If _Sum > 0 Then
                    sumAbsent = Format((_Sum \ 480), "00") & ":" & Format(((_Sum Mod 480) \ 60), "00") & ":" & Format(((_Sum Mod 480) Mod 60), "00") & ""
                End If

                _Sum = Integer.Parse(Val(R!FNLateNormalMin.ToString))
                If _Sum > 0 Then
                    sumLate = Format((_Sum \ 480), "00") & ":" & Format(((_Sum Mod 480) \ 60), "00") & ":" & Format(((_Sum Mod 480) Mod 60), "00") & ""
                End If

                Exit For
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTStartDate_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FTStartDate.EditValueChanged, FTEndDate.EditValueChanged
        If (_ProcPrepare) Then Exit Sub

        Static _Proc As Boolean
        If Not (_Proc) And Not (_ProcClick) Then
            _Proc = True

            Select Case sender.name.ToString.ToUpper
                Case "FTStartDate".ToUpper
                    Try
                        Try
                            If (HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) <= "1900/00/00") Then
                                FTStartDate.DateTime = Nothing
                                FTStartDate.Text = ""

                            Else
                                FTStartDate.DateTime = HI.UL.ULDate.ConvertEnDB(FTStartDate.Text)
                            End If

                        Catch ex As Exception
                            FTStartDate.DateTime = Nothing
                            FTStartDate.Text = ""

                        End Try
                    Catch ex As Exception
                    End Try

                    If HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) < HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) And FTEndDate.Text <> "" Then
                        Try
                            If (HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) <= "1900/00/00") Then

                                FTEndDate.DateTime = Nothing
                                FTEndDate.Text = ""
                            Else
                                FTEndDate.DateTime = HI.UL.ULDate.ConvertEnDB(FTStartDate.Text)
                            End If

                        Catch ex As Exception

                            FTEndDate.DateTime = Nothing
                            FTEndDate.Text = ""
                        End Try

                    End If
                Case "FTEndDate".ToUpper
                    Try
                        If (HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) <= "1900/00/00") Then
                            FTEndDate.DateTime = Nothing
                            FTEndDate.Text = ""
                        Else
                            FTEndDate.DateTime = HI.UL.ULDate.ConvertEnDB(FTEndDate.Text)
                        End If

                    Catch ex As Exception
                        FTEndDate.DateTime = Nothing
                        FTEndDate.Text = ""
                    End Try

                    If FTStartDate.Text = "" Then
                        Try

                            If (HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) <= "1900/00/00") Then
                                FTStartDate.DateTime = Nothing
                                FTStartDate.Text = ""
                            Else
                                FTStartDate.DateTime = HI.UL.ULDate.ConvertEnDB(FTEndDate.Text)
                            End If

                        Catch ex As Exception
                            FTStartDate.DateTime = Nothing
                            FTStartDate.Text = ""
                        End Try
                    End If

            End Select

            Call LoadDataEmployeeMoveShift()
            Call LoadDataEmployeeLeaveHistory()

            Call FTETime_EditValueChanged(FTETime, New System.EventArgs)

            Call Checkpay(HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex))

            Call SetEnableLeavePay()
            'Call CheckNewCondition()


            _Proc = False
        End If

    End Sub



    Private Sub CalculateTime()
        Dim _T1 As Integer = 0
        Dim _T2 As Integer = 0
        Select Case FNLeaveDay.SelectedIndex
            Case 0


                If FTIn1M.Text <> "" And FTOut1M.Text <> "" Then
                    If FTIn1M.Text > FTOut1M.Text Then
                        _T1 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & FTIn1M.Text), CDate(Me.ActualNextDate & "  " & FTOut1M.Text))
                    Else
                        _T1 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & FTIn1M.Text), CDate(Me.Actualdate & "  " & FTOut1M.Text))
                    End If
                End If

                If FTIn2M.Text <> "" And FTOut2M.Text <> "" Then
                    If FTIn2M.Text > FTOut2M.Text Then
                        _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & FTIn2M.Text), CDate(Me.ActualNextDate & "  " & FTOut2M.Text))
                    Else
                        _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & FTIn2M.Text), CDate(Me.Actualdate & "  " & FTOut2M.Text))
                    End If
                End If

            Case 1

                If FTIn1M.Text <> "" And FTOut1M.Text <> "" Then
                    If FTIn1M.Text > FTOut1M.Text Then
                        _T1 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & FTIn1M.Text), CDate(Me.ActualNextDate & "  " & FTOut1M.Text))
                    Else
                        _T1 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & FTIn1M.Text), CDate(Me.Actualdate & "  " & FTOut1M.Text))
                    End If
                End If

            Case 2

                If FTIn2M.Text <> "" And FTOut2M.Text <> "" Then
                    If FTIn2M.Text > FTOut2M.Text Then
                        _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & FTIn2M.Text), CDate(Me.ActualNextDate & "  " & FTOut2M.Text))
                    Else
                        _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & FTIn2M.Text), CDate(Me.Actualdate & "  " & FTOut2M.Text))
                    End If
                End If

        End Select

        ocetotaltime.Value = (_T1 + _T2)
        FNNetTime.Value = CDbl(Format((ocetotaltime.Value \ 60.0) + ((ocetotaltime.Value - ((ocetotaltime.Value \ 60.0) * 60.0)) / 100.0), "0.00"))

    End Sub

    Private Sub FNHSysEmpId_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNHSysEmpId.EditValueChanged
        If (_ProcPrepare) Then Exit Sub

        If Me.InvokeRequired Then
            Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FNHSysEmpId_EditValueChanged), New Object() {sender, e})
        Else

            If FNHSysEmpId.Text <> "" Then
                Dim _Qry As String = "SELECT TOP 1 FNHSysEmpID  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WITH(NOLOCK) WHERE FTEmpCode ='" & HI.UL.ULF.rpQuoted(FNHSysEmpId.Text) & "' "
                FNHSysEmpId.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")
            End If

            Call LoadEmpInfo(FNHSysEmpId.Properties.Tag.ToString)
            Call LoadDataEmployeeShift()
            Call LoadDataEmployeeLeaveHistory()
            Call FNLeaveType_SelectedIndexChanged(FNLeaveType, New System.EventArgs)
            Call FNLeaveDay_SelectedIndexChanged(FNLeaveDay, New System.EventArgs)
            Call pdf_clear()
            Call ShowLeaveInfo(FNHSysEmpId.Properties.Tag.ToString)
        End If

    End Sub

    Public Sub LoadEmpCodeByEmpIDInfo(ByVal Key As Object)
        Dim _Qry As String = "SELECT TOP 1 FTEmpCode   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WITH(NOLOCK) WHERE FNHSysEmpID =" & Val(Key) & " "
        FNHSysEmpId.Text = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")
    End Sub

    Private Sub CheckNewCondition()

        If FNLeaveType.SelectedIndex = 0 Then ' ลาป่วย
            '' FTStateLeavepay.Enabled = False
            FTStateLeavepay.Checked = False

            If FNHSysEmpTypeId.Text = "M" Or FNHSysEmpTypeId.Text = "N" Then
                If Val(FTNetDay.Text) < 3 Then
                    FTStateLeavepay.Checked = True
                Else
                    If FTStateMedicalCertificate.Checked = True Then
                        FTStateLeavepay.Checked = True
                    Else
                        FTStateLeavepay.Checked = False
                    End If
                End If
            Else ' D S Z
                If FTStateMedicalCertificate.Checked = True Then
                    FTStateLeavepay.Checked = True
                Else
                    FTStateLeavepay.Checked = False
                End If
            End If
        ElseIf FNLeaveType.SelectedIndex = 1 Then  'ลากิจ
            Dim prodate = FDDateProbation_lbl.Text
            Dim leaveStart = FTStartDate.Text

            prodate = HI.UL.ULDate.ConvertEnDB(prodate)
            leaveStart = HI.UL.ULDate.ConvertEnDB(leaveStart)

            prodate = prodate.Replace("/", "")
            leaveStart = leaveStart.Replace("/", "")
            If Val(leaveStart) > Val(prodate) Then
                '' FTStateLeavepay.Enabled = False
                FTStateLeavepay.Checked = False
            Else
                '' FTStateLeavepay.Enabled = True

            End If


        End If
    End Sub

    Private Sub LoadEmpInfo(ByVal FNHSysEmpID As String)
        Dim _dt As DataTable
        Dim _Qry As String = ""
        _Qry = " SELECT    TOP 1     M.FTEmpCode, M.FTEmpCodeRefer, M.FTEmpNameTH, M.FTEmpSurnameTH, M.FTEmpNicknameTH, M.FTEmpNameEN, M.FNHSysEmpTypeId, M.FNHSysDeptId, "
        _Qry &= vbCrLf & "   D.FTDeptCode, Di.FTDivisonCode, M.FNHSysDivisonId, M.FNHSysSectId, S.FTSectCode, ET.FTEmpTypeCode, M.FNHSysUnitSectId, US.FTUnitSectCode,"
        _Qry &= vbCrLf & "  M.FNHSysEmpID, M.FTEmpPicName, M.FNHSysPositId, P.FTPositCode"
        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId = P.FNHSysPositId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
        _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID  =" & Val(FNHSysEmpID) & ""
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        FTEmpPicName.Image = Nothing
        If _dt.Rows.Count > 0 Then
            For Each R As DataRow In _dt.Rows

                FTEmpPicName.Image = HI.UL.ULImage.LoadImage(HI.ST.SysInfo.SysPath & "EmpPicture\" & R!FTEmpPicName.ToString)
                FNHSysEmpTypeId.Text = R!FTEmpTypeCode.ToString
                FNHSysDeptId.Text = R!FTDeptCode.ToString
                FNHSysDivisonId.Text = R!FTDivisonCode.ToString
                FNHSysSectId.Text = R!FTSectCode.ToString
                FNHSysUnitSectId.Text = R!FTUnitSectCode.ToString
                FNHSysPositId.Text = R!FTPositCode.ToString
                FNHSysEmpTypeId.Properties.Tag = R!FNHSysEmpTypeId.ToString
            Next
        Else
            FNHSysEmpTypeId.Text = ""
            FNHSysDeptId.Text = ""
            FNHSysDivisonId.Text = ""
            FNHSysSectId.Text = ""
            FNHSysUnitSectId.Text = ""
            FNHSysPositId.Text = ""
            FNHSysEmpTypeId.Properties.Tag = "0"
        End If


    End Sub

    Private Sub FTETime_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FTETime.EditValueChanged
        If (_ProcPrepare) Then Exit Sub
        If Me.FTStartDate.Text <> "" And Me.FTETime.Text <> "" And Me.FTSTime.Text <> "" And Me.FTETime.Text <> "" Then

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday

            Dim _T1 As Integer = 0
            Dim _T2 As Integer = 0
            Dim _Res As Integer = 0
            Select Case FNLeaveDay.SelectedIndex
                Case 0, 1, 2

                    If FTIn1M.Text <> "" And FTOut1M.Text <> "" Then
                        If FTIn1M.Text > FTOut1M.Text Then
                            _T1 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & FTIn1M.Text), CDate(Me.ActualNextDate & "  " & FTOut1M.Text))
                        Else
                            _T1 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & FTIn1M.Text), CDate(Me.Actualdate & "  " & FTOut1M.Text))
                        End If
                    End If

                    If FTIn2M.Text <> "" And FTOut2M.Text <> "" Then
                        If FTIn2M.Text > FTOut2M.Text Then
                            _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & FTIn2M.Text), CDate(Me.ActualNextDate & "  " & FTOut2M.Text))
                        Else
                            _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & FTIn2M.Text), CDate(Me.Actualdate & "  " & FTOut2M.Text))
                        End If
                    End If

                Case Else

                    Dim STime As String = ""
                    Dim ETime As String = ""

                    If FTOut1M.Text >= "00:00" And FTOut2M.Text <= "06:00" Then
                        If CDate(Me.Actualdate & "  " & FTSTime.Text) < CDate(Me.ActualNextDate & "  " & FTOut1M.Text) And FTSTime.Text >= FTIn1M.Text Then
                            If FTSTime.Text <= FTIn1M.Text And FTSTime.Text > "06:00" Then
                                STime = FTIn1M.Text
                            Else
                                STime = FTSTime.Text
                            End If
                        Else
                            If FTIn1M.Text > FTOut1M.Text Then
                                If Not (FTSTime.Text > FTOut1M.Text And FTSTime.Text < FTIn1M.Text) Then
                                    STime = FTSTime.Text
                                End If
                            End If
                        End If

                        If FTETime.Text >= "00:00" And FTETime.Text <= "06:00" Then
                            If FTETime.Text < FTOut1M.Text And FTSTime.Text < FTETime.Text Then
                                ETime = FTETime.Text
                            Else
                                ETime = FTOut1M.Text
                            End If
                        Else
                            If CDate(Me.Actualdate & "  " & FTETime.Text) < CDate(Me.ActualNextDate & "  " & FTOut1M.Text) Then
                                ETime = FTETime.Text
                            Else
                                ETime = FTOut1M.Text
                            End If
                        End If
                        If STime <> "" And ETime <> "" Then
                            If STime > ETime Then
                                _T1 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & STime), CDate(Me.ActualNextDate & "  " & ETime))
                            Else
                                _T1 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & STime), CDate(Me.Actualdate & "  " & ETime))
                            End If
                        End If


                        STime = ""
                        ETime = ""

                        If FTSTime.Text >= "00:00" And FTSTime.Text >= FTIn2M.Text Then
                            If FTIn2M.Text >= "00:00" And FTIn2M.Text <= "06:00" And FTSTime.Text > FTIn2M.Text And FTSTime.Text > FTOut2M.Text Then
                                If CDate(Me.Actualdate & "  " & FTSTime.Text) <= CDate(Me.ActualNextDate & "  " & FTIn2M.Text) Then
                                    If FTETime.Text > FTIn2M.Text Then
                                        STime = FTIn2M.Text
                                    End If
                                Else
                                    STime = FTSTime.Text
                                End If
                            Else
                                If FTSTime.Text <= FTIn2M.Text Then
                                    STime = FTIn2M.Text
                                Else
                                    STime = FTSTime.Text
                                End If
                            End If
                        Else
                            If FTETime.Text > FTOut1M.Text And FTETime.Text >= FTIn2M.Text Then
                                STime = FTIn2M.Text
                            End If
                        End If

                        If FTETime.Text >= "00:00" And FTETime.Text <= "06:00" And FTIn2M.Text >= "00:00" And FTIn2M.Text <= "06:00" Then
                            If FTETime.Text < FTOut2M.Text Then
                                ETime = FTETime.Text
                            Else
                                ETime = FTOut2M.Text
                            End If
                        End If

                        If STime <> "" And ETime <> "" Then
                            If STime > ETime Then
                                _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & STime), CDate(Me.ActualNextDate & "  " & ETime))
                            Else
                                _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & STime), CDate(Me.Actualdate & "  " & ETime))
                            End If
                        End If


                    Else

                        If FTSTime.Text < FTOut1M.Text Then
                            If FTSTime.Text <= FTIn1M.Text Then
                                STime = FTIn1M.Text
                            Else
                                STime = FTSTime.Text
                            End If
                        ElseIf FTSTime.Text > FTIn1M.Text And FTIn1M.Text > FTOut1M.Text And FTOut1M.Text >= "00:00" And FTOut1M.Text <= "06:00" Then
                            STime = FTSTime.Text
                        End If

                        If FTETime.Text < FTOut1M.Text Then
                            ETime = FTETime.Text
                        Else
                            ETime = FTOut1M.Text
                        End If

                        If STime <> "" And ETime <> "" Then
                            If STime > ETime Then
                                _T1 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & STime), CDate(Me.ActualNextDate & "  " & ETime))
                            Else
                                _T1 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & STime), CDate(Me.Actualdate & "  " & ETime))
                            End If
                        End If

                        STime = ""
                        ETime = ""

                        If FTSTime.Text < FTOut2M.Text And FTETime.Text > FTIn2M.Text Then
                            If FTSTime.Text <= FTIn2M.Text Then
                                STime = FTIn2M.Text
                            Else
                                STime = FTSTime.Text
                            End If
                        ElseIf FTSTime.Text > FTIn1M.Text And FTIn1M.Text > FTOut1M.Text And FTOut1M.Text >= "00:00" And FTOut1M.Text <= "06:00" Then
                            STime = FTIn2M.Text
                        End If

                        If FTETime.Text > FTIn2M.Text Then
                            If FTETime.Text < FTOut2M.Text Then
                                ETime = FTETime.Text
                            Else
                                ETime = FTOut2M.Text
                            End If
                        End If

                        If STime <> "" And ETime <> "" Then
                            If STime > ETime Then
                                _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & STime), CDate(Me.ActualNextDate & "  " & ETime))
                            Else
                                _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & STime), CDate(Me.Actualdate & "  " & ETime))
                            End If
                        End If
                    End If




                    'If FTSTime.Text <> "" And FTETime.Text <> "" Then
                    '    If FTSTime.Text > FTETime.Text Then
                    '        _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & FTSTime.Text), CDate(Me.ActualNextDate & "  " & FTETime.Text))
                    '    Else
                    '        _T2 = DateDiff(DateInterval.Minute, CDate(Me.Actualdate & "  " & FTSTime.Text), CDate(Me.Actualdate & "  " & FTETime.Text))
                    '    End If
                    'Else
                    'End If
            End Select

            Dim _Total As Integer = 0
            Select Case FNLeaveDay.SelectedIndex
                Case 0
                    _Total = _T1 + _T2
                Case 1
                    _Total = _T1
                Case 2
                    _Total = _T2
                Case Else
                    _Total = _T1 + _T2
                    If _Total > 480 Then _Total = 480
            End Select

            ocetotaltime.Value = _Total
            Me.FNNetTime.Value = CDbl(Format((_Total \ 60.0) + ((_Total - ((_Total \ 60.0) * 60.0)) / 100.0), "0.00"))

            Dim _Qry As String
            Dim _dtHoliday As DataTable
            Dim _dtWeekend As DataTable

            Dim _EmpTypeWeekly As DataTable

            FNHSysEmpTypeId.Properties.Tag = HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK) WHERE FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "'", Conn.DB.DataBaseName.DB_MASTER, "")

            _Qry = "SELECt   FDHolidayDate  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmpTypeWeeklySpecial WITH(NOLOCK) "
            _Qry &= vbCrLf & "  WHERE FDHolidayDate>='" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "' "
            _Qry &= vbCrLf & "  AND FDHolidayDate<='" & HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) & "' "
            _Qry &= vbCrLf & "  AND FNHSysEmpTypeId=" & Integer.Parse(Val(FNHSysEmpTypeId.Properties.Tag.ToString)) & " "

            _EmpTypeWeekly = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
            _Qry = "   SELECT    Top 1   FTSunday,FTMonday, FTTuesday, FTWednesday, "
            _Qry &= vbCrLf & "   FTThursday, FTFriday, FTSaturday"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly  As W WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(FNHSysEmpId.Properties.Tag.ToString) & " "
            _dtWeekend = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            If _dtWeekend.Rows.Count <= 0 Then
                '_Qry = "   SELECT    FTSunday, FTMonday, FTTuesday, FTWednesday, FTThursday, FTFriday,"
                '_Qry &= vbCrLf & "    FTSaturday "
                '_Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift  As W WITH(NOLOCK) "
                '_Qry &= vbCrLf & " WHERE FNHSysShiftID =" & Val(FNHSysShiftID.Text) & " "
                '_dtWeekend = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            Else
                _EmpTypeWeekly.Rows.Clear()
            End If

            _Qry = "SELECt   FDHolidayDate  "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday WITH(NOLOCK) "
            _Qry &= vbCrLf & "  WHERE FDHolidayDate>='" & HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) & "' "
            _Qry &= vbCrLf & "  AND FDHolidayDate<='" & HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) & "'  AND FTStateActive='1' "
            _dtHoliday = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

            Dim _EndProcDate As String = HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text)
            Dim _NextProcDate As String = HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text)
            Dim nNextDay As Double = 0
            Dim _TotalDay As Integer = 0
            Dim _SkipProcess As Boolean = False
            Dim _WeekEnd As Integer

            If _NextProcDate <> "" And _EndProcDate <> "" And _NextProcDate >= "1800/01/01" And _EndProcDate >= "1800/01/01" Then
                Do While _NextProcDate <= _EndProcDate
                    _WeekEnd = Weekday(CDate(_NextProcDate), Microsoft.VisualBasic.FirstDayOfWeek.Sunday)
                    _SkipProcess = False
                    If Me.FTStateNotMergeHoliday.Checked Then

                        For Each Rday As DataRow In _dtWeekend.Rows

                            If Rday.Item(_WeekEnd - 1).ToString = "1" Then
                                _SkipProcess = True
                            End If

                            Exit For
                        Next

                        If _SkipProcess = False Then
                            For Each Dr As DataRow In _dtHoliday.Select("   FDHolidayDate  = '" & HI.UL.ULDate.ConvertEnDB(_NextProcDate) & "' ")
                                _SkipProcess = True
                            Next
                        End If

                        If _SkipProcess = False Then
                            For Each Dr As DataRow In _EmpTypeWeekly.Select("   FDHolidayDate  = '" & HI.UL.ULDate.ConvertEnDB(_NextProcDate) & "' ")
                                _SkipProcess = True
                            Next
                        End If


                    End If

                    If Not (_SkipProcess) Then
                        _TotalDay = _TotalDay + 1
                    End If

                    _NextProcDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_NextProcDate, 1))

                Loop
            End If

            Me.FTNetDay.Value = _TotalDay

        Else
            Me.ocetotaltime.Value = 0
            Me.FNNetTime.Value = 0
            Me.FTNetDay.Value = 0
        End If

    End Sub

    Private Sub FNLeaveType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNLeaveType.SelectedIndexChanged, FNSpecialLeaveType.SelectedIndexChanged

        If (_ProcPrepare) Then Exit Sub

        If FNLeaveType.SelectedIndex >= 0 Then
            Call Checkpay(HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex))
        End If

        If FTStartDate.Text = "" Then
            FTStartDate.DateTime = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))

            FTEndDate.DateTime = FTStartDate.DateTime
        End If

        Call FTStartDate_EditValueChanged(FTStartDate, New System.EventArgs)



    End Sub

    Private Sub ogc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private _ProcClick As Boolean
    Private Sub ogv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogv.DoubleClick
        Try
            With ogv
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                _ProcClick = True
                Me.FNLeaveType.SelectedIndex = HI.TL.CboList.GetIndexByValue(Me.FNLeaveType.Properties.Tag.ToString, "" & .GetRowCellValue(.FocusedRowHandle, "FTLeaveType").ToString)
                Me.FNLeaveDay.SelectedIndex = HI.TL.CboList.GetIndexByValue(Me.FNLeaveDay.Properties.Tag.ToString, "" & .GetRowCellValue(.FocusedRowHandle, "FTStaLeaveDay").ToString)

                Try
                    Me.FTStartDate.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTStartDate").ToString)
                Catch ex As Exception
                End Try

                Try
                    Me.FTEndDate.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTEndDate").ToString)
                Catch ex As Exception
                End Try

                Me.FTSTime.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTLeaveStartTime").ToString
                Me.FTETime.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTLeaveEndTime").ToString
                Me.FTStateCalSSo.Checked = ("" & .GetRowCellValue(.FocusedRowHandle, "FTStaCalSSO").ToString = "1")
                Me.FTStateLeavepay.Checked = ("" & .GetRowCellValue(.FocusedRowHandle, "FTLeavePay").ToString = "1")
                Me.FTStateMedicalCertificate.Checked = ("" & .GetRowCellValue(.FocusedRowHandle, "FTStateMedicalCertificate").ToString = "1")
                Me.FTStateDeductVacation.Checked = ("" & .GetRowCellValue(.FocusedRowHandle, "FTStateDeductVacation").ToString = "1")

                Me.FTRemark.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTLeaveNote").ToString
                Me.FTRemark.Focus()
                _ProcClick = False
                Call FTStartDate_EditValueChanged(FTEndDate, New System.EventArgs)
                Call Load_PDF()



            End With
        Catch ex As Exception
            _ProcClick = False
        End Try
        _ProcClick = False
    End Sub

    Private Sub FTSTime_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FTSTime.EditValueChanged

        Call FTETime_EditValueChanged(FTSTime, New System.EventArgs)

    End Sub

    Private Sub FNLeaveSickType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) = "0" Then
            Call Checkpay(HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex))
        End If

    End Sub

    Private Sub FTNetDay_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FTNetDay.EditValueChanged
        If HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) = "0" Then

            'If FTNetDay.Value >= 3 Then
            '    FTStateLeavepay.Checked = False
            'End If

        Else
            Try
                If TotalLeave < FTNetDay.Value Then
                    HI.MG.ShowMsg.mProcessError(1004090001, "พบข้อมูลการลาเกินกำหนด !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Information, " ( " & TotalLeave.ToString & " Day )")
                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub FTStartDate_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles FTStartDate.Leave

        Try
            Try
                FTStartDate.DateTime = HI.UL.ULDate.ConvertEnDB(FTStartDate.Text)

            Catch ex As Exception
                FTStartDate.DateTime = Nothing
                FTStartDate.Text = ""
            End Try
        Catch ex As Exception
        End Try

        If FTEndDate.Text = "" Then
            Try
                FTEndDate.DateTime = HI.UL.ULDate.ConvertEnDB(FTStartDate.Text)
            Catch ex As Exception
                FTEndDate.DateTime = Nothing
                FTEndDate.Text = ""
            End Try
        End If
    End Sub

    Private Sub FTEndDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles FTEndDate.LostFocus

        If FTEndDate.Text <> "" And FTStartDate.Text <> "" Then
            If HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) < HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) Then
                Try
                    FTStartDate.DateTime = HI.UL.ULDate.ConvertEnDB(FTEndDate.Text)
                Catch ex As Exception
                    FTStartDate.DateTime = Nothing
                    FTStartDate.Text = ""
                End Try

            End If
        End If

    End Sub

    Private Function ChkActiveLeaveHRApp() As Boolean
        Try
            Dim _Qry As String = ""
            Dim _permiss As Boolean = False
            Dim _poss As Boolean = False
            _Qry = "SELECT TOP 1  Isnull( D.FTSendApproveState,'0') as FTSendApproveState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily AS D  WITH(NOLOCK)"
            '_Qry &= vbCrLf & "INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS L WITH(NOLOCK) ON (D.FTUpdUser = L.FTUserName OR D.FTUpdUser = L.FTUserName ) and D.FNHSysEmpID = L.FNHSysEmpID "
            _Qry &= vbCrLf & "WHERE D.FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
            _Qry &= vbCrLf & "AND D.FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            _Qry &= vbCrLf & "AND D.FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            _Qry &= vbCrLf & "AND D.FTLeaveType = '" & HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) & "'"
            _Qry &= vbCrLf & "AND Isnull( D.FTMngApproveState,'0') = '1' and  Isnull( D.FTSendApproveState,'0') = '1' "
            _poss = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0") = "1"


            _Qry = " Select  Top 1  C.FNHSysCmpId"
            _Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEUserLoginPermission AS P WITH (NOLOCK) LEFT OUTER Join"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEPermissionCmp AS C WITH (NOLOCK) ON P.FNHSysPermissionID = C.FNHSysPermissionID"
            _Qry &= vbCrLf & "Where (P.FNHSysPermissionID IN"
            _Qry &= vbCrLf & "           (SELECT FNHSysPermissionID"
            _Qry &= vbCrLf & "From [HITECH_SECURITY]..TSEPermissionObjectControl WITH (NOLOCK)"
            _Qry &= vbCrLf & "Where (FTFormName = N'wEmployeeLeave') AND (FTObjName = N'ocmapprove'))) AND (P.FTUserName = '" & HI.ST.UserInfo.UserName & "')"
            _Qry &= vbCrLf & "and C.FNHSysCmpId=" & HI.ST.SysInfo.CmpID
            _permiss = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0") <> ""

            Return (_poss = True And _permiss = True)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ChkActiveLeaveHR() As Boolean
        Try
            Dim _Qry As String = ""
            _Qry = "SELECT TOP 1  Isnull( D.FTSendApproveState,'0') as FTSendApproveState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily AS D  WITH(NOLOCK)"
            _Qry &= vbCrLf & "INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS L WITH(NOLOCK) ON (D.FTUpdUser = L.FTUserName OR D.FTUpdUser = L.FTUserName ) and D.FNHSysEmpID = L.FNHSysEmpID "
            _Qry &= vbCrLf & "WHERE D.FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
            _Qry &= vbCrLf & "AND D.FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            _Qry &= vbCrLf & "AND D.FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            _Qry &= vbCrLf & "AND D.FTLeaveType = '" & HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) & "'"
            '_Qry &= vbCrLf & "AND (D.FTUpdUser = '" & HI.ST.UserInfo.UserName & "' OR D.FTInsUser = '" & HI.ST.UserInfo.UserName & "') "
            _Qry &= vbCrLf & "AND Isnull( D.FTSendApproveState,'0') = '1'  "
            Return HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR).Rows.Count > 0
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ChkActiveLeaveHRDelete() As Boolean
        Try

            If HI.ST.SysInfo.Admin Then
                Return True
            Else

                Dim _Qry As String = ""
                Dim _Premis As Boolean = False
                Dim _Approve As Boolean = False
                Dim _SendApp As Boolean = False
                Dim _MngApp As Boolean = False
                Dim _User As String = ""
                Dim _oDt As DataTable

                _Qry = " Select  Top 1  C.FNHSysCmpId"
                _Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEUserLoginPermission AS P WITH (NOLOCK) LEFT OUTER Join"
                _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEPermissionCmp AS C WITH (NOLOCK) ON P.FNHSysPermissionID = C.FNHSysPermissionID"
                _Qry &= vbCrLf & "Where (P.FNHSysPermissionID IN"
                _Qry &= vbCrLf & "           (SELECT FNHSysPermissionID"
                _Qry &= vbCrLf & "From [HITECH_SECURITY]..TSEPermissionObjectControl WITH (NOLOCK)"
                _Qry &= vbCrLf & "Where (FTFormName = N'wEmployeeLeave') AND (FTObjName = N'ocmdelete'))) AND (P.FTUserName = '" & HI.ST.UserInfo.UserName & "')"
                _Qry &= vbCrLf & "and C.FNHSysCmpId=" & HI.ST.SysInfo.CmpID
                _Premis = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR).Rows.Count > 0

                _Qry = "SELECT TOP 1  Isnull( D.FTSendApproveState,'0') as FTSendApproveState ,isnull(D.FTMngApproveState,'0') AS FTMngApproveState , Isnull(D.FTApproveState,'0') AS FTApproveState  , Isnull(D.FTUpdUser,D.FTInsUser)  as FTInsUser"
                _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily As D  With(NOLOCK)"
                _Qry &= vbCrLf & "WHERE D.FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
                _Qry &= vbCrLf & "And D.FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
                _Qry &= vbCrLf & "AND D.FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
                _Qry &= vbCrLf & "AND D.FTLeaveType = '" & HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) & "'"
                _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
                For Each R As DataRow In _oDt.Rows
                    _SendApp = R!FTSendApproveState.ToString = "1"
                    _MngApp = R!FTMngApproveState.ToString = "1"
                    _Approve = R!FTApproveState.ToString = "1"
                    _User = R!FTInsUser.ToString
                Next

                Select Case True
                    Case _SendApp And Not (_MngApp) And Not (_Approve) And _User.ToUpper = HI.ST.UserInfo.UserName.ToUpper
                        Return True
                    Case _SendApp And _MngApp And Not (_Approve) And _User.ToUpper = HI.ST.UserInfo.UserName.ToUpper
                        Return True
                    Case _SendApp And _MngApp And _Approve And _Premis
                        Return True
                    Case _SendApp And _MngApp And _User.ToUpper = HI.ST.UserInfo.UserName.ToUpper And Not (_Approve)
                        Return True
                    Case _oDt.Rows.Count <= 0
                        Return True
                    Case Else
                        Return False
                End Select
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click
        If Me.VerrifyData Then
            If Not ChkActiveLeaveHRApp() Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถแก้ไขข้อมูลการลาของพนักงานคนนี้ได้ กรุณาตรวจสอบ !!!", 1702281015, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If
            Dim _Msg As String = ""
            Dim _Qry As String = ""
            _Qry = "Select TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily WITH(NOLOCK)"
            _Qry &= vbCrLf & "WHERE FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
            _Qry &= vbCrLf & "AND FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            _Qry &= vbCrLf & "AND FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            _Qry &= vbCrLf & " AND FTLeaveType = '" & HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) & "'"

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") = "" Then
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลการลา ไม่สามารถทำการอนุมัติได้ กรุณาทำการตรวจสอบ !!!", 14121807312, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim _State As Boolean = False
            _State = HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ อนุมัติ การลาใช่หรือไม่ ?", 14121807101, FNHSysEmpId_None.Text)

            If (_State) Then
                Dim _Spls As New HI.TL.SplashScreen("Approving...   Please Wait   ")
                If Me.SaveData() Then

                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily "
                    _Qry &= vbCrLf & " SET FTApproveState='1'"
                    _Qry &= vbCrLf & " ,FTApproveDate=" & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ,FTApproveTime=" & HI.UL.ULDate.FormatTimeDB & ""
                    _Qry &= vbCrLf & " ,FTApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
                    _Qry &= vbCrLf & " AND FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
                    _Qry &= vbCrLf & " AND FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
                    _Qry &= vbCrLf & " AND FTLeaveType = '" & HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave "
                    _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & " AND FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
                    _Qry &= vbCrLf & " AND FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
                    _Qry &= vbCrLf & " AND FTLeaveType = '" & HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    If Me.ApproveData Then
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        FTStartDate_EditValueChanged(FTStartDate, New System.EventArgs)
                        Call LoadDataEmployeeLeaveHistory()
                    Else
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            End If
        End If
    End Sub

    Private Sub ocmSendApprove_Click(sender As Object, e As EventArgs) Handles ocmSendApprove.Click
        If Me.VerrifyData Then
            If ChkActiveLeaveHR() Then
                HI.MG.ShowMsg.mInfo("มีการส่งอนุมัติ กรุณาตรวจสอบ !!!", 1703111015, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If
            If SendApprove() Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Function SendApprove() As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRTLeaveAdvanceDaily"
            _Cmd &= vbCrLf & "Set FTSendApproveState='1'"
            _Cmd &= vbCrLf & ", FDSendApproveDate=" & HI.UL.ULDate.FormatDateDB & ""
            _Cmd &= vbCrLf & ", FTSendApproveTime=" & HI.UL.ULDate.FormatTimeDB & ""
            _Cmd &= vbCrLf & ", FTSendApproveBy='" & HI.ST.UserInfo.UserName & "'"
            _Cmd &= vbCrLf & " Where  FTStartDate='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            _Cmd &= vbCrLf & "and  FTEndDate='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            _Cmd &= vbCrLf & "and FNHSysEmpID=" & Integer.Parse("0" & Me.FNHSysEmpId.Properties.Tag)
            HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_HR)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    'leave


    Sub ValcationLeave_Check()
        ' If FNLeaveType.SelectedIndex = (HI.TL.CboList.GetIndexByValue("" & FNLeaveType.Properties.Tag.ToString, "1")) Then
        Call Checkpay(HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, (HI.TL.CboList.GetIndexByValue("" & FNLeaveType.Properties.Tag.ToString, "98"))), False)
        Dim leave As Integer = TotalLeave
        If (leave / 480) > 0 Then
            If HI.MG.ShowMsg.mConfirmProcess("คุณมีวันพักร้อนเหลือ ต้องการใช้วันพักร้อนแทนวันลากิจหรือไม่", 1404300007, " " & (leave / 480).ToString & " day ") Then
                FNLeaveType.SelectedIndex = (HI.TL.CboList.GetIndexByValue("" & FNLeaveType.Properties.Tag.ToString, "98"))
                Exit Sub
            End If
        End If
        ' End If
    End Sub
    'pdf


    Private _FilePath As String
    Private data As Byte()
    Private _Pdfdata As Byte()


    Private Sub PDF_Import()
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                .Filter = "Image Files(*.PNG;*.BMP;*.JPG;*.GIF) |*.PNG;*.BMP;*.JPG;*.GIF|PDF files(*.PDF)|*.PDF"
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    _FilePath = .FileName
                    Call Readfile()
                    Me.otab.SelectedTabPage = Me.otabpage2
                Else
                    _FilePath = ""
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Readfile()
        Try
            If _FilePath <> "" Then
                Dim _FileType As String = "" : Dim _Data() As Byte : Dim _FileName As String : Dim _StreamFile As Stream
                _FileType = System.IO.Path.GetExtension(_FilePath)
                _FileName = System.IO.Path.GetFileName(_FilePath)
                Select Case _FileType.ToUpper
                    Case ".PDF".ToUpper
                        Call _PDFViewer(_FilePath)
                        Call PDFBinary()
                    Case ".BMP".ToUpper
                        Call _PicViewer(_FilePath)
                        Call PicBinary()
                    Case ".JPG".ToUpper
                        Call _PicViewer(_FilePath)
                        Call PicBinary()
                    Case ".GIF".ToUpper
                        Call _PicViewer(_FilePath)
                        Call PicBinary()
                    Case ".PNG".ToUpper
                        Call _PicViewer(_FilePath)
                        Call PicBinary()
                    Case Else
                        HI.MG.ShowMsg.mInfo("Can't Set File Type Pls Check...", 1510301710, Me.Text)
                        Exit Sub
                End Select
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PDFBinary()
        Try
            If _FilePath <> "" Then
                Dim _FileType As String = "" : Dim _FileName As String
                _FileType = System.IO.Path.GetExtension(_FilePath)
                _FileName = System.IO.Path.GetFileName(_FilePath)
                Select Case _FileType.ToUpper
                    Case ".PDF".ToUpper
                        Dim br As New BinaryReader(New FileStream(_FilePath, FileMode.Open, FileAccess.Read))
                        data = br.ReadBytes(CInt(New FileInfo(_FilePath).Length))
                        _Pdfdata = data
                        _FBFile = "PDF"
                End Select
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub _PDFViewer(_FileName As String)
        Try
            Me.oGrpdetail.Controls.Clear()
            Dim _Pdfv As New PdfViewer
            _Pdfv.Dock = DockStyle.Fill
            _Pdfv.NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
            _Pdfv.LoadDocument(_FileName)
            'Me.oGrpdetail.Controls.Add(SimpleButton1)
            Me.oGrpdetail.Controls.Add(_Pdfv)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Load_PDF()
        Call pdf_clear()

        Dim _cmd As String = ""
        _cmd = "SELECT FBFileRef,FBFile FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily WITH(NOLOCK)"
        _cmd &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & " "
        _cmd &= vbCrLf & "And FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
        _cmd &= vbCrLf & "AND FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
        _cmd &= vbCrLf & " AND FTLeaveType = '" & HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex) & "'"
        Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_cmd, Conn.DB.DataBaseName.DB_HR)
        For Each R As DataRow In _oDt.Rows
            Dim FBFile As String = R!FBFile
            Me.oGrpdetail.Controls.Clear()
            Select Case FBFile
                Case "PDF"
                    Dim _Pdfv As New PdfViewer
                    With _Pdfv
                        .Dock = DockStyle.Fill
                        _Pdfdata = CType(R!FBFileRef, Byte())
                        .NavigationPaneInitialVisibility = PdfNavigationPaneVisibility.Hidden
                        .LoadDocument(New MemoryStream(CType(R!FBFileRef, Byte())))
                    End With
                    'Me.oGrpdetail.Controls.Add(SimpleButton1)
                    Me.oGrpdetail.Controls.Add(_Pdfv)

                Case "Picture"
                    HI.ST.UserInfo.UserImage = HI.UL.ULImage.ConvertByteArrayToImmage(R!FBFileRef)
                    FTLeavePic.Dock = DockStyle.Fill
                    FTLeavePic.Image = HI.ST.UserInfo.UserImage
                    Me.oGrpdetail.Controls.Add(FTLeavePic)
            End Select




        Next
    End Sub
    Private _FBFile As String
    Private Sub savePDF()
        If data Is Nothing Then
        Else
            Dim _Qry As String
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily SET"
            _Qry &= vbCrLf & " FTInsUser=@FTInsUser, FBFileRef=@FBFileRef ,FBFile=@FBFile"
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=@FNHSysEmpID"
            _Qry &= vbCrLf & " AND FTStartDate=@FTStartDate"
            _Qry &= vbCrLf & " AND FTEndDate=@FTEndDate"
            _Qry &= vbCrLf & " AND FTLeaveType=@FTLeaveType"
            Dim cmd As New SqlCommand(_Qry, HI.Conn.SQLConn.Cnn)
            cmd.Parameters.AddWithValue("@FTInsUser", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName))
            Dim p1 As New SqlParameter("@FBFileRef", SqlDbType.VarBinary)
            p1.Value = data
            Dim p2 As New SqlParameter("@FTStartDate", SqlDbType.NVarChar)
            p2.Value = HI.UL.ULF.rpQuoted(HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text))
            Dim p3 As New SqlParameter("@FNHSysEmpID", SqlDbType.Int)
            p3.Value = Integer.Parse("0" & Val(FNHSysEmpId.Properties.Tag.ToString))
            Dim p4 As New SqlParameter("@FTEndDate", SqlDbType.NVarChar)
            p4.Value = HI.UL.ULF.rpQuoted(HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text))
            Dim p5 As New SqlParameter("@FTLeaveType", SqlDbType.NVarChar)
            p5.Value = HI.UL.ULF.rpQuoted(HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex))
            Dim p6 As New SqlParameter("@FBFile", SqlDbType.NVarChar)
            p6.Value = _FBFile
            cmd.Parameters.Add(p1)
            cmd.Parameters.Add(p3)
            cmd.Parameters.Add(p2)
            cmd.Parameters.Add(p4)
            cmd.Parameters.Add(p5)
            cmd.Parameters.Add(p6)
            cmd.ExecuteNonQuery()
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cnn)
        End If
    End Sub

    'Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
    '    Try
    '        For Each obj As Object In Me.oGrpdetail.Controls
    '            'Dim _Filter As String = "Excel Workbook(*.xlsx)|*.xlsx|Excel 97-2003 Workbook(*.xls)|*.xls|PDF(*.pdf)|*.pdf|Word Documents(97-2003)|*.doc|Word Documents(2010-2013)|*.docx"
    '            Select Case obj.GetType.Name.ToString
    '                Case "PdfViewer"
    '                    Dim dialog As New SaveFileDialog()
    '                    dialog.Filter = "PDF files |*.pdf"
    '                    Dim result As DialogResult = dialog.ShowDialog()
    '                    Dim fileName As String = dialog.FileName
    '                    If result = DialogResult.OK Then
    '                        With CType(obj, DevExpress.XtraPdfViewer.PdfViewer)
    '                            My.Computer.FileSystem.WriteAllBytes(fileName, _Pdfdata, True)
    '                        End With
    '                        Process.Start(fileName)
    '                    End If
    '            End Select
    '        Next

    '    Catch ex As Exception
    '    End Try
    'End Sub
    Sub pdf_clear()
        Me.oGrpdetail.Controls.Clear()
        data = Nothing
        _Pdfdata = Nothing
        _FilePath = Nothing
    End Sub


    Private Sub ocmReadDocumentfile_Click(sender As Object, e As EventArgs) Handles ocmReadDocumentfile.Click
        PDF_Import()
    End Sub

    Private Sub _PicViewer(_FileName As String)
        Try
            Me.oGrpdetail.Controls.Clear()

            FTLeavePic.Dock = DockStyle.Fill
            FTLeavePic.Image = HI.UL.ULImage.LoadImage(_FilePath)
            Me.oGrpdetail.Controls.Add(FTLeavePic)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PicBinary()
        Try
            If _FilePath <> "" Then
                Dim _FileType As String = "" : Dim _FileName As String
                _FileType = System.IO.Path.GetExtension(_FilePath)
                _FileName = System.IO.Path.GetFileName(_FilePath)
                Dim br As New BinaryReader(New FileStream(_FilePath, FileMode.Open, FileAccess.Read))
                data = br.ReadBytes(CInt(New FileInfo(_FilePath).Length))
                _Pdfdata = data
                _FBFile = "Picture"
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private _picdata As Byte()

    Private Sub FTStateMedicalCertificate_CheckedChanged(sender As Object, e As EventArgs) Handles FTStateMedicalCertificate.CheckedChanged
        Call Checkpay(HI.TL.CboList.GetListValue("" & FNLeaveType.Properties.Tag.ToString, FNLeaveType.SelectedIndex))
    End Sub
End Class