Public Class wEmployeeHealthHistory

    Private _AddHealth As wAddEmpHealth
    Private _AddHealthCost As wAddEmpHealthCost
    Private _AddHealthSS As wAddEmpHealthSS

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _AddHealth = New wAddEmpHealth
        _AddHealthCost = New wAddEmpHealthCost
        _AddHealthSS = New wAddEmpHealthSS

        Dim _SystemLang As New ST.SysLanguage

        Try
            Call _SystemLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddHealth.Name.ToString.Trim, _AddHealth)
        Catch ex As Exception
        Finally
        End Try

        Try

            Call _SystemLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddHealthCost.Name.ToString.Trim, _AddHealthCost)
        Catch ex As Exception
        Finally
        End Try

        Try

            Call _SystemLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _AddHealthSS.Name.ToString.Trim, _AddHealthSS)
        Catch ex As Exception
        Finally
        End Try

        HI.TL.HandlerControl.AddHandlerObj(_AddHealth)
        HI.TL.HandlerControl.AddHandlerObj(_AddHealthCost)
        HI.TL.HandlerControl.AddHandlerObj(_AddHealthSS)

        Call TabChange()

    End Sub

#Region "Procedure"
    Private Sub LoadEmpInfo(ByVal FNHSysEmpID As String)

        Dim _PathEmpPic As String
        _PathEmpPic = ""
        Dim cmdstring As String = "Select Top 1 FTCfgData FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE FTCfgName='PathEmpPic'"

        _PathEmpPic = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "")

        Dim _dt As DataTable
        Dim _Qry As String = ""
        _Qry = " SELECT    TOP 1     M.FTEmpCode, M.FTEmpCodeRefer, M.FTEmpNameTH, M.FTEmpSurnameTH, M.FTEmpNicknameTH, M.FTEmpNameEN, M.FNHSysEmpTypeId, M.FNHSysDeptId, "
        _Qry &= vbCrLf & "   D.FTDeptCode, Di.FTDivisonCode, M.FNHSysDivisonId, M.FNHSysSectId, S.FTSectCode, ET.FTEmpTypeCode, M.FNHSysUnitSectId, US.FTUnitSectCode,"
        _Qry &= vbCrLf & "  M.FNHSysEmpID, M.FTEmpPicName, M.FNHSysPositId, P.FTPositCode"
        _Qry &= vbCrLf & "  FROM            THRMEmployee AS M WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId = P.FNHSysPositId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
        _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID  =" & Val(FNHSysEmpID) & ""
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        FTEmpPicName.Image = Nothing
        If _dt.Rows.Count > 0 Then
            For Each R As DataRow In _dt.Rows
                If _PathEmpPic = "" Then
                    FTEmpPicName.Image = HI.UL.ULImage.LoadImage(HI.ST.SysInfo.SysPath & "EmpPicture\" & R!FTEmpPicName.ToString)
                Else
                    FTEmpPicName.Image = HI.UL.ULImage.LoadImage(_PathEmpPic & R!FTEmpPicName.ToString)
                End If
                FNHSysEmpTypeId.Text = R!FTEmpTypeCode.ToString
                FNHSysDeptId.Text = R!FTDeptCode.ToString
                FNHSysDivisonId.Text = R!FTDivisonCode.ToString
                FNHSysSectId.Text = R!FTSectCode.ToString
                FNHSysUnitSectId.Text = R!FTUnitSectCode.ToString
                FNHSysPositId.Text = R!FTPositCode.ToString

            Next
        Else
            FNHSysEmpTypeId.Text = ""
            FNHSysDeptId.Text = ""
            FNHSysDivisonId.Text = ""
            FNHSysSectId.Text = ""
            FNHSysUnitSectId.Text = ""
            FNHSysPositId.Text = ""
        End If

        Call LoadHealthHistory()
        Call LoadHealthHistoryExpense()
        Call LoadWorkSickness()

        Me.otb.SelectedTabPageIndex = 0
    End Sub

    Private Sub TabChange()
        Try
            ocmaddemphealth.Visible = (otb.SelectedTabPage.Name = otphealth.Name)
            ocmeditemphealth.Visible = (otb.SelectedTabPage.Name = otphealth.Name)
            ocmremoveemphealth.Visible = (otb.SelectedTabPage.Name = otphealth.Name)

            ocmaddemphealtcost.Visible = (otb.SelectedTabPage.Name = otpcost.Name)
            ocmeditemphealtcost.Visible = (otb.SelectedTabPage.Name = otpcost.Name)
            ocmremoveemphealtcost.Visible = (otb.SelectedTabPage.Name = otpcost.Name)

            ocmaddemphealthSS.Visible = (otb.SelectedTabPage.Name = otpss.Name)
            ocmeditemphealthSS.Visible = (otb.SelectedTabPage.Name = otpss.Name)
            ocmremoveemphealthSS.Visible = (otb.SelectedTabPage.Name = otpss.Name)

            HI.TL.METHOD.CallActiveToolBarFunction(Me)
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "General"

    Private Sub wEmployeeHealthHistory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmpreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmpreview.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Me.FNHSysEmpID.Focus()
    End Sub

    Private Sub FNHSysEmpID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNHSysEmpID.EditValueChanged

        If Me.InvokeRequired Then
            Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FNHSysEmpID_EditValueChanged), New Object() {sender, e})
        Else
            If FNHSysEmpID.Text <> "" Then
                FNHSysEmpID.Properties.Tag = HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysEmpID  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WITH(NOLOCK) WHERE FTEmpCode ='" & HI.UL.ULF.rpQuoted(FNHSysEmpID.Text) & "' ", Conn.DB.DataBaseName.DB_HR, "")
                Call LoadEmpInfo(FNHSysEmpID.Properties.Tag.ToString)
            Else
                ogchealth.DataSource = Nothing
                ogccost.DataSource = Nothing

            End If
        End If

    End Sub

    Private Sub LoadHealthHistory()
        Dim _Qry As String = ""


        _Qry = " SELECT  H.FNSeqNo AS FNSeqNo,C.StateCheck  , CASE WHEN ISDATE(H.FDHealth) = 1 THEN CONVERT(varchar(10), CONVERT(Datetime, H.FDHealth), 103) ELSE '' END AS FDHealth, P.FTHospitalCode"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  ,P.FTHospitalNameTH AS FTHospitalName "
        Else
            _Qry &= vbCrLf & "  ,P.FTHospitalNameEN AS FTHospitalName "
        End If


        _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHealthHistory AS H WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHospital AS P WITH (NOLOCK) ON H.FNHSysHospitalId = P.FNHSysHospitalId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMBlood AS B WITH (NOLOCK) ON H.FNHSysBldId = B.FNHSysBldId  LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  (  SELECT H.FNHSysEmpID,H.FNSeqNo,H.StateCheck"
        _Qry &= vbCrLf & " FROM(SELECT  H.FNHSysEmpID,'ตรวจครั้งแรก'AS StateCheck,H.FNSeqNo"
        _Qry &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_HealthCheck AS H  "
        _Qry &= vbCrLf & " WHERE  H.FTStateFirstCheck='1'"
        _Qry &= vbCrLf & " UNION"
        _Qry &= vbCrLf & " Select  H.FNHSysEmpID,'ตรวจประจำปี'AS StateCheck,H.FNSeqNo"
        _Qry &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_HealthCheck AS H  "
        _Qry &= vbCrLf & " Where H.FTStateAnnualCheck ='1'"
        _Qry &= vbCrLf & "  UNION"
        _Qry &= vbCrLf & "   Select  H.FNHSysEmpID,'ตรวจเมื่อเปลี่ยนงาน'AS StateCheck,H.FNSeqNo"
        _Qry &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_HealthCheck AS H  "
        _Qry &= vbCrLf & " Where H.FTStateCheckJobs ='1'"
        _Qry &= vbCrLf & " UNION"
        _Qry &= vbCrLf & " Select  H.FNHSysEmpID,'ตรวจเฝ้าระวังตามความจำเป็น'AS StateCheck,H.FNSeqNo"
        _Qry &= vbCrLf & " From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_HealthCheck AS H  "
        _Qry &= vbCrLf & " Where H.FTStateSurveillance ='1'"
        _Qry &= vbCrLf & " )AS H)AS C ON H.FNHSysEmpID=C.FNHSysEmpID And H.FNSeqNo=C.FNSeqNo"
        _Qry &= vbCrLf & "  WHERE        (H.FNHSysEmpID =" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & ")  "
        _Qry &= vbCrLf & "  ORDER BY H.FDHealth DESC "

        Me.ogchealth.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

    End Sub

    Private Sub LoadHealthHistoryExpense()
        Dim _Qry As String = ""

        _Qry = " SELECT        H.FNSeqNo, CASE WHEN ISDATE(H.FDTreatment) = 1 THEN CONVERT(varchar(10), CONVERT(Datetime, H.FDTreatment), 103) ELSE '' END AS FDTreatment, "
        _Qry &= vbCrLf & "  H.FTBillNo, H.FNHSysHospitalId, P.FTHospitalCode"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  ,P.FTHospitalNameTH AS FTHospitalName "
        Else
            _Qry &= vbCrLf & "  ,P.FTHospitalNameEN AS FTHospitalName "
        End If

        _Qry &= vbCrLf & " , H.FCMedical, H.FCSocial, H.FCDisburse, H.FTNote"
        _Qry &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeMedicalExpenses AS H WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHospital AS P WITH (NOLOCK) ON H.FNHSysHospitalId = P.FNHSysHospitalId"
        _Qry &= vbCrLf & "  WHERE        (H.FNHSysEmpID =" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & ")  "
        _Qry &= vbCrLf & "  ORDER BY H.FDTreatment DESC "

        Me.ogccost.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

    End Sub

    Private Function verifydata() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNHSysEmpID.Text <> "" And Me.FNHSysEmpID.Properties.Tag.ToString <> "" Then
            _Pass = True
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysEmpID_lbl.Text)
            FNHSysEmpID.Focus()
        End If

        Return _Pass

    End Function

    Private Sub ocmaddhealth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmaddemphealth.Click

        If verifydata() Then
            HI.TL.HandlerControl.ClearControl(_AddHealth)
            With _AddHealth
                .EmpSysID = Val(Me.FNHSysEmpID.Properties.Tag.ToString)
                .HealthSeq = 0
                .ProcComplete = False
                .ShowDialog()

                If .ProcComplete Then

                    Call LoadHealthHistory()

                End If

            End With
        End If

    End Sub

    Private Sub ocmaddcost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmaddemphealtcost.Click

        If verifydata() Then

            HI.TL.HandlerControl.ClearControl(_AddHealthCost)

            With _AddHealthCost
                .EmpSysID = Val(Me.FNHSysEmpID.Properties.Tag.ToString)
                .HealthSeq = 0
                .ProcComplete = False
                .ShowDialog()

                If .ProcComplete Then
                    Call LoadHealthHistoryExpense()
                End If

            End With
        End If

    End Sub

    Private Sub ogvhealth_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogvhealth.DoubleClick

        With ogvhealth
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            Dim _HealtSeq As Integer = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSeqNo").ToString)
            HI.TL.HandlerControl.ClearControl(_AddHealth)

            With _AddHealth
                .EmpSysID = Val(Me.FNHSysEmpID.Properties.Tag.ToString)
                .HealthSeq = _HealtSeq
                .ProcComplete = False
                .ShowDialog()

                If .ProcComplete Then
                    Call LoadHealthHistory()
                End If

            End With

        End With

    End Sub

    Private Sub ogvcost_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogvcost.DoubleClick

        With ogvcost

            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            Dim _HealtSeq As Integer = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSeqNo").ToString)
            HI.TL.HandlerControl.ClearControl(_AddHealthCost)
            With _AddHealthCost
                .EmpSysID = Val(Me.FNHSysEmpID.Properties.Tag.ToString)
                .HealthSeq = _HealtSeq
                .ProcComplete = False
                .ShowDialog()

                If .ProcComplete Then
                    Call LoadHealthHistoryExpense()
                End If

            End With

        End With

    End Sub

    Private Sub ocmedithealth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmeditemphealth.Click

        With ogvhealth
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            Dim _HealtSeq As Integer = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSeqNo").ToString)
            HI.TL.HandlerControl.ClearControl(_AddHealth)
            With _AddHealth
                .EmpSysID = Val(Me.FNHSysEmpID.Properties.Tag.ToString)
                .HealthSeq = _HealtSeq
                .ProcComplete = False
                .ShowDialog()

                If .ProcComplete Then
                    Call LoadHealthHistory()
                End If

            End With

        End With

    End Sub

    Private Sub ocmremovehealth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmremoveemphealth.Click

        With ogvhealth
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            Dim _HealtSeq As Integer = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSeqNo").ToString)

            Dim _Qry As String
            _Qry = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHealthHistory WHERE FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & " AND  FNSeqNo=" & _HealtSeq & " "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)
            _Qry = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_HealthCheck WHERE FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & " AND  FNSeqNo=" & _HealtSeq & " "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)


            _Qry = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHealthHistory SET FNSeqNo=FNNo"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHealthHistory INNER JOIN "
            _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FDInsDate, FTInsTime) AS FNNo, FNSeqNo,FNHSysEmpID"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHealthHistory"
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & ""
            _Qry &= vbCrLf & ") T1 ON  THRTEmployeeHealthHistory.FNSeqNo=T1.FNSeqNo "
            _Qry &= vbCrLf & " AND  THRTEmployeeHealthHistory.FNHSysEmpID=T1.FNHSysEmpID "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

            Call LoadHealthHistory()

        End With

    End Sub

    Private Sub ocmeditcost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmeditemphealtcost.Click
        With ogvcost
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            Dim _HealtSeq As Integer = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSeqNo").ToString)
            HI.TL.HandlerControl.ClearControl(_AddHealthCost)

            With _AddHealthCost
                .EmpSysID = Val(Me.FNHSysEmpID.Properties.Tag.ToString)
                .HealthSeq = _HealtSeq
                .ShowDialog()

                If .ProcComplete Then

                    Call LoadHealthHistoryExpense()

                End If

            End With

        End With
    End Sub

    Private Sub ocmremovecost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmremoveemphealtcost.Click
        With ogvcost
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            Dim _HealtSeq As Integer = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSeqNo").ToString)

            Dim _Qry As String
            _Qry = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeMedicalExpenses WHERE FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & " AND  FNSeqNo=" & _HealtSeq & " "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

            _Qry = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeMedicalExpenses SET FNSeqNo=FNNo"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeMedicalExpenses INNER JOIN "
            _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FDInsDate, FTInsTime) AS FNNo, FNSeqNo,FNHSysEmpID"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeMedicalExpenses"
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & ""
            _Qry &= vbCrLf & ") T1 ON  THRTEmployeeMedicalExpenses.FNSeqNo=T1.FNSeqNo "
            _Qry &= vbCrLf & " AND  THRTEmployeeMedicalExpenses.FNHSysEmpID=T1.FNHSysEmpID "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

            Call LoadHealthHistoryExpense()

        End With
    End Sub

    Private Sub ocmclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Me.FNHSysEmpID.Focus()
        Me.otb.SelectedTabPageIndex = 0
    End Sub

#End Region

    Private Sub otb_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otb.SelectedPageChanged
        Call TabChange()
    End Sub

    Private Sub ocmaddemphealthSS_Click(sender As Object, e As EventArgs) Handles ocmaddemphealthSS.Click
        If verifydata() Then
            HI.TL.HandlerControl.ClearControl(_AddHealthSS)
            With _AddHealthSS
                .EmpSysID = Val(Me.FNHSysEmpID.Properties.Tag.ToString)
                .HealthSeq = 0
                .ProcComplete = False
                .ShowDialog()

                If .ProcComplete Then

                    Call LoadWorkSickness()

                End If

            End With
        End If
    End Sub

    Private Sub ocmeditemphealthSS_Click(sender As Object, e As EventArgs) Handles ocmeditemphealthSS.Click
        With ogvsickness
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            Dim _HealtSeq As Integer = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSeqNo").ToString)
            HI.TL.HandlerControl.ClearControl(_AddHealthSS)
            With _AddHealthSS
                .EmpSysID = Val(Me.FNHSysEmpID.Properties.Tag.ToString)
                .HealthSeq = _HealtSeq
                .ProcComplete = False
                .ShowDialog()

                If .ProcComplete Then
                    Call LoadWorkSickness()
                End If

            End With

        End With
    End Sub

    Private Sub ocmremoveemphealthSS_Click(sender As Object, e As EventArgs) Handles ocmremoveemphealthSS.Click
        With ogvsickness
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            Dim _HealtSeq As Integer = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNSeqNo").ToString)

            Dim _Qry As String
            _Qry = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_WorkSickness WHERE FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & " AND  FNSeqNo=" & _HealtSeq & " "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)



            _Qry = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_WorkSickness SET FNSeqNo=FNNo"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_WorkSickness INNER JOIN "
            _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FDInsDate, FTInsTime) AS FNNo, FNSeqNo,FNHSysEmpID"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_WorkSickness"
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & ""
            _Qry &= vbCrLf & ") T1 ON  THRMEmployee_WorkSickness.FNSeqNo=T1.FNSeqNo "
            _Qry &= vbCrLf & " AND  THRMEmployee_WorkSickness.FNHSysEmpID=T1.FNHSysEmpID "
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

            Call LoadWorkSickness()

        End With

    End Sub

    Private Sub LoadWorkSickness()
        Dim _Qry As String = ""

        _Qry = " SELECT  W.FNSeqNo,W.FTCauseOfInjury,W.FTDisability,W.FTInjuredBody,W.FTLeaveWorkMore,W.FTLeaveWorkNiMore,W.FTLossOfSomeOrgans"
        _Qry &= vbCrLf & ", CASE WHEN ISDATE(W.FDWorkDate) = 1 THEN CONVERT(varchar(10), CONVERT(Datetime, W.FDWorkDate), 103) ELSE '' END AS FDWorkDate"
        _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_WorkSickness AS W WITH (NOLOCK)"
        _Qry &= vbCrLf & "  WHERE        (W.FNHSysEmpID =" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & ")  "


        Me.ogcsickness.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

    End Sub
End Class