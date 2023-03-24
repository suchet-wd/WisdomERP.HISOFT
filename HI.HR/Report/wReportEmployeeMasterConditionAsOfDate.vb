Imports System.Data.SqlClient
Imports System.IO

Public Class wReportEmployeeMasterConditionAsOfDate

    Private _LstReport As HI.RP.ListReport
    Sub New(_SysFormName As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Me.Name = _SysFormName

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

        If Me.FTAsofDate.Text = "" Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTAsofDate_lbl.Text)
            Exit Sub
        End If

        _Formular &= IIf(_Formular.Trim <> "", " AND ", "")
        _Formular &= " {THRMEmployee.FDDateStart}<='" & HI.UL.ULDate.ConvertEnDB(Me.FTAsofDate.Text) & "' "

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

            ' Call HI.ST.Security.CreateTempEmpMaster(Condition)

            Call CreateTempEmpMasterAsof(Condition, Me.FTAsofDate.Text)

            If _LstReport.GetValueGenPic(FNReportname.SelectedIndex) = "1" Then
                Call HI.HRCAL.GenTempData.GenerateEmpPicture(Condition)
            End If

            For Each _ReportName As String In _AllReportName.Split(",")
                With New HI.RP.Report

                    '*****วันที่*********
                    If Me.FTAsofDate.Text <> "" Then
                        .AddParameter("FTAsofDate", Me.FTAsofDate.Text)
                    End If

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
    End Sub

    Private Sub wReportMasterConditionAsOfDate_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If FNReportname.Properties.Items.Count < 0 Then
                MsgBox("ไม่พบการกำหนด File Report !!!")
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CreateTempEmpMasterAsof(Condition As HI.HR.HRCondition, _ASOfDate As String)


        Dim _Qry As String = ""
        Dim _Formular As String = ""
        Dim tText As String = ""

        Try
            tText = Condition.GetCriteria().ToString().Trim()
        Catch ex As Exception
        End Try

        If tText <> "" Then
            If _Formular <> "" Then
                _Formular &= " AND "
            End If

            _Formular &= "" & tText
        End If


        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_TmpReport WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
        _Qry &= Constants.vbCrLf & "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_TmpReport(FTUserLogIn, FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysEmpID, FTEmpCode, FNHSysCmpId, FTEmpCodeRefer, FNHSysPreNameId, FTEmpNameTH, FTEmpSurnameTH, "
        _Qry &= Constants.vbCrLf & "   FTEmpNicknameTH, FTEmpNameEN, FTEmpSurnameEN, FTEmpNicknameEN, FNEmpSex, FNUseBarcode, FTEmpBarcode, FTEmpPicName, FNHSysShiftID, FNScanCtrl, FDDateStart, FDDateEnd,"
        _Qry &= Constants.vbCrLf & "   FNHSysResignId, FTResign, FDDateProbation, FTProbationSta, FNEmpStatus, FNHSysEmpTypeId, FNHSysDeptId, FNHSysDivisonId, FNHSysSectId, FNHSysUnitSectId, FNHSysPositId, FNHSysEmpIDLeader,"
        _Qry &= Constants.vbCrLf & "   FNLateCutSta, FNPaidOTSta, FDBirthDate, FNHSysBldId, FCWeight, FCHeight, FNHSysRaceId, FNHSysNationalityId, FNHSysReligionId, FNMilitary, FTMilitaryNote, FTEmpIdNo, FDDateIdNoAssign,"
        _Qry &= Constants.vbCrLf & "   FDDateIdNoEnd, FTEmpIdNoBy, FTSocialNo, FNHSysHospitalId, FTTaxNo, FNEverRegisSta, FNCalSocialSta, FNCalTaxSta, FNHSysPayRollPayId, FTAccNo, FNHSysBankId, FNHSysBankBranchId, FNCarStatus,"
        _Qry &= Constants.vbCrLf & "   FTCarId, FTCarLicense, FNMotorCycleStatus, FTMotorCycleId, FTMotorCycleLicense, FTDrug, FTDiesea, FTHobby, FTCriminalCauseSta, FTCriminalCause, FTAddrNo, FTAddrHome, FTAddrMoo, FTAddrSoi,"
        _Qry &= Constants.vbCrLf & "   FTAddrRoad, FTAddrTumbol, FTAddrAmphur, FTAddrProvince, FTAddrPostCode, FTAddrTel, FTAddrNo1, FTAddrHome1, FTAddrMoo1, FTAddrSoi1, FTAddrRoad1, FTAddrTumbol1, FTAddrAmphur1, FTAddrProvince1,"
        _Qry &= Constants.vbCrLf & "   FTAddrPostCode1, FTAddrTel1, FTMobile, FTEmail, FNSalary, FNMaritalStatus, FTRefName, FTRefAddr, FTRefCareer, FTRefPosit, FTRefAddrWork, FTRefTel, FTRefRelation, FTRefNote, FTRefName1, FTRefAddr1,"
        _Qry &= Constants.vbCrLf & "   FTRefCareer1, FTRefPosit1, FTRefAddrWork1, FTRefTel1, FTRefRelation1, FTRefNote1, FTFatherName, FNFatherLife, FTFatherIDNo, FTFatherAddr, FTFatherCareer, FTFatherPosit, FTFatherAddrWork, FTFatherTel,"
        _Qry &= Constants.vbCrLf & "   FTMotherName, FNMotherLife, FTMotherIDNo, FTMotherAddr, FTMotherCareer, FTMotherPosit, FTMotherAddrWork, FTMotherTel, FTMateName, FTMateIncome, FNMateLife, FTMateIDNo, FTMateAddr,"
        _Qry &= Constants.vbCrLf & "   FTMateCareer, FTMatePosit, FTMateAddrWork, FTMateTel, FTMateFatherName, FTMateFatherIDNo, FTMateMotherName, FTMateMotherIDNo, FCModFather, FCModMother, FCModMateFather, FCModMateMother,"
        _Qry &= Constants.vbCrLf & "   FCPremium, FCInterest, FCUnitRMF, FCUnitLTF, FCDeductDonate, FCDeductDonateStudy, FCDeductDividend, FCDisabledDependents, FCHealthInsurFatherMotherMate, FTHealthInsurIDFather,"
        _Qry &= Constants.vbCrLf & "   FTHealthInsurIDMother, FTHealthInsurIDFatherMate, FTHealthInsurIDMotherMate, FCIncomeBefore, FCTaxBefore, FCSocialBefore, FTFundIDNo, FDFundBegin, FDFundEnd, FCExceptAgeOver,"
        _Qry &= Constants.vbCrLf & "   FCExceptAgeOverMate, FDRetire, FTStaCalMonthEnd, FDDateTransfer, FTDeligentCode )"
        _Qry &= Constants.vbCrLf & " SELECT   '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS  FTUserLogIn,"
        _Qry &= Constants.vbCrLf & " M.FTInsUser, M.FDInsDate, M.FTInsTime, M.FTUpdUser, M.FDUpdDate, M.FTUpdTime, M.FNHSysEmpID, M.FTEmpCode, M.FNHSysCmpId, M.FTEmpCodeRefer, M.FNHSysPreNameId, M.FTEmpNameTH, "
        _Qry &= Constants.vbCrLf & "  M.FTEmpSurnameTH, M.FTEmpNicknameTH, M.FTEmpNameEN, M.FTEmpSurnameEN, M.FTEmpNicknameEN, M.FNEmpSex, M.FNUseBarcode, M.FTEmpBarcode, M.FTEmpPicName, M.FNHSysShiftID,"
        _Qry &= Constants.vbCrLf & "  M.FNScanCtrl, M.FDDateStart, M.FDDateEnd, M.FNHSysResignId, M.FTResign, M.FDDateProbation, M.FTProbationSta, M.FNEmpStatus, M.FNHSysEmpTypeId, M.FNHSysDeptId, M.FNHSysDivisonId,"
        _Qry &= Constants.vbCrLf & "  M.FNHSysSectId, M.FNHSysUnitSectId, M.FNHSysPositId, M.FNHSysEmpIDLeader, M.FNLateCutSta, M.FNPaidOTSta, M.FDBirthDate, M.FNHSysBldId, M.FCWeight, M.FCHeight, M.FNHSysRaceId,"
        _Qry &= Constants.vbCrLf & "  M.FNHSysNationalityId, M.FNHSysReligionId, M.FNMilitary, M.FTMilitaryNote, M.FTEmpIdNo, M.FDDateIdNoAssign, M.FDDateIdNoEnd, M.FTEmpIdNoBy, M.FTSocialNo, M.FNHSysHospitalId, M.FTTaxNo,"
        _Qry &= Constants.vbCrLf & "  M.FNEverRegisSta, M.FNCalSocialSta, M.FNCalTaxSta, M.FNHSysPayRollPayId, M.FTAccNo, M.FNHSysBankId, M.FNHSysBankBranchId, M.FNCarStatus, M.FTCarId, M.FTCarLicense, M.FNMotorCycleStatus,"
        _Qry &= Constants.vbCrLf & "  M.FTMotorCycleId, M.FTMotorCycleLicense, M.FTDrug, M.FTDiesea, M.FTHobby, M.FTCriminalCauseSta, M.FTCriminalCause, M.FTAddrNo, M.FTAddrHome, M.FTAddrMoo, M.FTAddrSoi, M.FTAddrRoad,"
        _Qry &= Constants.vbCrLf & "  M.FTAddrTumbol, M.FTAddrAmphur, M.FTAddrProvince, M.FTAddrPostCode, M.FTAddrTel, M.FTAddrNo1, M.FTAddrHome1, M.FTAddrMoo1, M.FTAddrSoi1, M.FTAddrRoad1, M.FTAddrTumbol1, M.FTAddrAmphur1,"
        _Qry &= Constants.vbCrLf & "  M.FTAddrProvince1, M.FTAddrPostCode1, M.FTAddrTel1, M.FTMobile, M.FTEmail"
        _Qry &= Constants.vbCrLf & " , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE M.FNSalary END AS FNSalary "
        _Qry &= Constants.vbCrLf & " , M.FNMaritalStatus, M.FTRefName, M.FTRefAddr, M.FTRefCareer, M.FTRefPosit, M.FTRefAddrWork, M.FTRefTel,"
        _Qry &= Constants.vbCrLf & "  M.FTRefRelation, M.FTRefNote, M.FTRefName1, M.FTRefAddr1, M.FTRefCareer1, M.FTRefPosit1, M.FTRefAddrWork1, M.FTRefTel1, M.FTRefRelation1, M.FTRefNote1, M.FTFatherName, M.FNFatherLife,"
        _Qry &= Constants.vbCrLf & "  M.FTFatherIDNo, M.FTFatherAddr, M.FTFatherCareer, M.FTFatherPosit, M.FTFatherAddrWork, M.FTFatherTel, M.FTMotherName, M.FNMotherLife, M.FTMotherIDNo, M.FTMotherAddr, M.FTMotherCareer,"
        _Qry &= Constants.vbCrLf & "  M.FTMotherPosit, M.FTMotherAddrWork, M.FTMotherTel, M.FTMateName, M.FTMateIncome, M.FNMateLife, M.FTMateIDNo, M.FTMateAddr, M.FTMateCareer, M.FTMatePosit, M.FTMateAddrWork, M.FTMateTel,"
        _Qry &= Constants.vbCrLf & "  M.FTMateFatherName, M.FTMateFatherIDNo, M.FTMateMotherName, M.FTMateMotherIDNo, M.FCModFather, M.FCModMother, M.FCModMateFather, M.FCModMateMother, M.FCPremium, M.FCInterest,"
        _Qry &= Constants.vbCrLf & "  M.FCUnitRMF, M.FCUnitLTF, M.FCDeductDonate, M.FCDeductDonateStudy, M.FCDeductDividend, M.FCDisabledDependents, M.FCHealthInsurFatherMotherMate, M.FTHealthInsurIDFather,"
        _Qry &= Constants.vbCrLf & "  M.FTHealthInsurIDMother, M.FTHealthInsurIDFatherMate, M.FTHealthInsurIDMotherMate, M.FCIncomeBefore, M.FCTaxBefore, M.FCSocialBefore, M.FTFundIDNo, M.FDFundBegin, M.FDFundEnd,"
        _Qry &= Constants.vbCrLf & "  M.FCExceptAgeOver, M.FCExceptAgeOverMate, M.FDRetire, M.FTStaCalMonthEnd, M.FDDateTransfer, M.FTDeligentCode"

        _Qry &= Constants.vbCrLf & " FROM "

        _Qry &= Constants.vbCrLf & "  ( "

        _Qry &= Constants.vbCrLf & "  SELECT M.FTInsUser, M.FDInsDate, M.FTInsTime, M.FTUpdUser, M.FDUpdDate, M.FTUpdTime"
        _Qry &= Constants.vbCrLf & " , M.FNHSysEmpID, M.FTEmpCode, M.FNHSysCmpId, M.FTEmpCodeRefer, M.FNHSysPreNameId, M.FTEmpNameTH, "
        _Qry &= Constants.vbCrLf & "   M.FTEmpSurnameTH, M.FTEmpNicknameTH, M.FTEmpNameEN, M.FTEmpSurnameEN, M.FTEmpNicknameEN, M.FNEmpSex, M.FNUseBarcode, M.FTEmpBarcode, M.FTEmpPicName, M.FNHSysShiftID, M.FNScanCtrl,"
        _Qry &= Constants.vbCrLf & "   M.FDDateStart, M.FDDateEnd, M.FNHSysResignId, M.FTResign, M.FDDateProbation, M.FTProbationSta, M.FNEmpStatus"
        _Qry &= Constants.vbCrLf & " , ISNULL(MMM2.FNHSysEmpTypeId,M.FNHSysEmpTypeId) AS FNHSysEmpTypeId"
        _Qry &= Constants.vbCrLf & " ,  ISNULL(MMM2.FNHSysDeptId,M.FNHSysDeptId) AS  FNHSysDeptId"
        _Qry &= Constants.vbCrLf & " ,  ISNULL(MMM2.FNHSysDivisonId,M.FNHSysDivisonId) AS FNHSysDivisonId"
        _Qry &= Constants.vbCrLf & " , ISNULL(MMM2.FNHSysSectId,M.FNHSysSectId) AS FNHSysSectId"
        _Qry &= Constants.vbCrLf & " ,  ISNULL(MMM2.FNHSysUnitSectId,M.FNHSysUnitSectId) AS FNHSysUnitSectId"
        _Qry &= Constants.vbCrLf & " ,  ISNULL(MMM2.FNHSysPositId,M.FNHSysPositId) AS FNHSysPositId"
        _Qry &= Constants.vbCrLf & " , M.FNHSysEmpIDLeader, M.FNLateCutSta, M.FNPaidOTSta, M.FDBirthDate, M.FNHSysBldId, M.FCWeight, M.FCHeight, M.FNHSysRaceId, M.FNHSysNationalityId,"
        _Qry &= Constants.vbCrLf & "   M.FNHSysReligionId, M.FNMilitary, M.FTMilitaryNote, M.FTEmpIdNo, M.FDDateIdNoAssign, M.FDDateIdNoEnd, M.FTEmpIdNoBy, M.FTSocialNo, M.FNHSysHospitalId, M.FTTaxNo, M.FNEverRegisSta, M.FNCalSocialSta,"
        _Qry &= Constants.vbCrLf & "   M.FNCalTaxSta, M.FNHSysPayRollPayId, M.FTAccNo, M.FNHSysBankId, M.FNHSysBankBranchId, M.FNCarStatus, M.FTCarId, M.FTCarLicense, M.FNMotorCycleStatus, M.FTMotorCycleId, M.FTMotorCycleLicense,"
        _Qry &= Constants.vbCrLf & "   M.FTDrug, M.FTDiesea, M.FTHobby, M.FTCriminalCauseSta, M.FTCriminalCause, M.FTAddrNo, M.FTAddrHome, M.FTAddrMoo, M.FTAddrSoi, M.FTAddrRoad, M.FTAddrTumbol, M.FTAddrAmphur, M.FTAddrProvince,"
        _Qry &= Constants.vbCrLf & "   M.FTAddrPostCode, M.FTAddrTel, M.FTAddrNo1, M.FTAddrHome1, M.FTAddrMoo1, M.FTAddrSoi1, M.FTAddrRoad1, M.FTAddrTumbol1, M.FTAddrAmphur1, M.FTAddrProvince1, M.FTAddrPostCode1, M.FTAddrTel1,"
        _Qry &= Constants.vbCrLf & "   M.FTMobile, M.FTEmail, M.FNSalary, M.FNMaritalStatus, M.FTRefName, M.FTRefAddr, M.FTRefCareer, M.FTRefPosit, M.FTRefAddrWork, M.FTRefTel, M.FTRefRelation, M.FTRefNote, M.FTRefName1, M.FTRefAddr1,"
        _Qry &= Constants.vbCrLf & "   M.FTRefCareer1, M.FTRefPosit1, M.FTRefAddrWork1, M.FTRefTel1, M.FTRefRelation1, M.FTRefNote1, M.FTFatherName, M.FNFatherLife, M.FTFatherIDNo, M.FTFatherAddr, M.FTFatherCareer, M.FTFatherPosit,"
        _Qry &= Constants.vbCrLf & "   M.FTFatherAddrWork, M.FTFatherTel, M.FTMotherName, M.FNMotherLife, M.FTMotherIDNo, M.FTMotherAddr, M.FTMotherCareer, M.FTMotherPosit, M.FTMotherAddrWork, M.FTMotherTel, M.FTMateName,"
        _Qry &= Constants.vbCrLf & "   M.FTMateIncome, M.FNMateLife, M.FTMateIDNo, M.FTMateAddr, M.FTMateCareer, M.FTMatePosit, M.FTMateAddrWork, M.FTMateTel, M.FTMateFatherName, M.FTMateFatherIDNo, M.FTMateMotherName,"
        _Qry &= Constants.vbCrLf & "   M.FTMateMotherIDNo, M.FCModFather, M.FCModMother, M.FCModMateFather, M.FCModMateMother, M.FCPremium, M.FCInterest, M.FCUnitRMF, M.FCUnitLTF, M.FCDeductDonate, M.FCDeductDonateStudy,"
        _Qry &= Constants.vbCrLf & "   M.FCDeductDividend, M.FCDisabledDependents, M.FCHealthInsurFatherMotherMate, M.FTHealthInsurIDFather, M.FTHealthInsurIDMother, M.FTHealthInsurIDFatherMate, M.FTHealthInsurIDMotherMate,"
        _Qry &= Constants.vbCrLf & "   M.FCIncomeBefore, M.FCTaxBefore, M.FCSocialBefore, M.FTFundIDNo, M.FDFundBegin, M.FDFundEnd, M.FCExceptAgeOver, M.FCExceptAgeOverMate, M.FDRetire, M.FTStaCalMonthEnd, M.FDDateTransfer,"
        _Qry &= Constants.vbCrLf & "   M.FTDeligentCode, M.FTStateWorkpermit"

        _Qry &= Constants.vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH(NOLOCK)"


        _Qry &= Constants.vbCrLf & "  LEFT OUTER JOIN (Select A1.FNHSysEmpID"
        _Qry &= Constants.vbCrLf & "  , A1.FTEffectiveDate"
        _Qry &= Constants.vbCrLf & "  , A2.FNHSysEmpTypeId,A2.FNHSysDeptId"
        _Qry &= Constants.vbCrLf & "   , A2.FNHSysDivisonId"
        _Qry &= Constants.vbCrLf & "   , A2.FNHSysSectId"
        _Qry &= Constants.vbCrLf & "   , A2.FNHSysUnitSectId"
        _Qry &= Constants.vbCrLf & "   , A2.FNHSysPositId"
        _Qry &= Constants.vbCrLf & "   , A2.FNHSysEmpTypeIdTo"
        _Qry &= Constants.vbCrLf & "   , A2.FNHSysDeptIdTo"
        _Qry &= Constants.vbCrLf & "   , A2.FNHSysDivisonIdTo"
        _Qry &= Constants.vbCrLf & "   , A2.FNHSysSectIdTo"
        _Qry &= Constants.vbCrLf & "   , A2.FNHSysUnitSectIdTo"
        _Qry &= Constants.vbCrLf & "   , A2.FNHSysPositIdTo"
        _Qry &= Constants.vbCrLf & "  FROM "
        _Qry &= Constants.vbCrLf & "  ("
        _Qry &= Constants.vbCrLf & "  SELECT O.FNHSysEmpID,O.FTEffectiveDate,MAX(O2.FNSeq ) AS FNSeq"
        _Qry &= Constants.vbCrLf & "  FROM ("
        _Qry &= Constants.vbCrLf & "  SELECT FNHSysEmpID, MIN(FTEffectiveDate) AS FTEffectiveDate"
        _Qry &= Constants.vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeMasterChange  WITH(NOLOCK)"
        _Qry &= Constants.vbCrLf & "  WHERE  FTEffectiveDate >= N'" & HI.UL.ULDate.ConvertEnDB(_ASOfDate) & "'"
        _Qry &= Constants.vbCrLf & "  GROUP BY FNHSysEmpID"
        _Qry &= Constants.vbCrLf & " ) O INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeMasterChange AS O2 WITH(NOLOCK) ON O.FNHSysEmpID =O2.FNHSysEmpID AND O.FTEffectiveDate =O2.FTEffectiveDate"
        _Qry &= Constants.vbCrLf & " GROUP BY O.FNHSysEmpID,O.FTEffectiveDate"
        _Qry &= Constants.vbCrLf & " ) AS A1 INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeMasterChange AS A2 WITH(NOLOCK) ON A1.FNHSysEmpID =A2.FNHSysEmpID AND A1.FTEffectiveDate =A2.FTEffectiveDate AND A1.FNSeq =A2.FNSeq"
        _Qry &= Constants.vbCrLf & " ) MMM2  ON M.FNHSysEmpID = MMM2.FNHSysEmpID  ) AS M"

        _Qry &= Constants.vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= Constants.vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= Constants.vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Div WITH (NOLOCK) ON M.FNHSysDivisonId = Div.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= Constants.vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (NOLOCK) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
        _Qry &= Constants.vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId"

        _Qry &= Constants.vbCrLf & " LEFT OUTER JOIN "
        _Qry &= Constants.vbCrLf & " ("

        If ((HI.ST.SysInfo.Admin)) Then

            _Qry &= Constants.vbCrLf & " SELECT DISTINCT FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WITH(NOLOCK)  "

        Else

            _Qry &= Constants.vbCrLf & " SELECT   DISTINCT     RT.FNHSysEmpTypeId"
            _Qry &= Constants.vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS RT WITH(NOLOCK)   INNER JOIN"
            _Qry &= Constants.vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS U WITH(NOLOCK)   ON RT.FNHSysPermissionID = U.FNHSysPermissionID "
            _Qry &= Constants.vbCrLf & "  WHERE U.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AND FTStateSalary='1' "
            _Qry &= Constants.vbCrLf & " "
        End If

        _Qry &= Constants.vbCrLf & " )  As ES ON M.FNHSysEmpTypeId = ES.FNHSysEmpTypeId "
        _Qry &= Constants.vbCrLf & " LEFT OUTER JOIN "
        _Qry &= Constants.vbCrLf & "   ("

        If ((HI.ST.SysInfo.Admin)) Then
            _Qry &= Constants.vbCrLf & " SELECT DISTINCT FNHSysSectId  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect WITH(NOLOCK)   "
        Else
            _Qry &= Constants.vbCrLf & " Select DISTINCT S.FNHSysSectId"
            _Qry &= Constants.vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN"
            _Qry &= Constants.vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN"
            _Qry &= Constants.vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID"
            _Qry &= Constants.vbCrLf & "   CROSS JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S  WITH(NOLOCK)"
            _Qry &= Constants.vbCrLf & "  WHERE UP.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= Constants.vbCrLf & "  AND UPM.FTMnuName='" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) & "' "
            _Qry &= Constants.vbCrLf & "  AND UPT.FTStateAll='1' AND UPT.FTStateSalary='1'    "
            _Qry &= Constants.vbCrLf & " UNION"
            _Qry &= Constants.vbCrLf & " Select DISTINCT S.FNHSysSectId"
            _Qry &= Constants.vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN"
            _Qry &= Constants.vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN"
            _Qry &= Constants.vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID INNER JOIN "
            _Qry &= Constants.vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeTypeSect AS UPT2 WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPT.FNHSysPermissionID = UPT2.FNHSysPermissionID "
            _Qry &= Constants.vbCrLf & "   INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH(NOLOCK) ON UPT2.FNHSysSectId = S.FNHSysSectId  "
            _Qry &= Constants.vbCrLf & "  WHERE UP.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= Constants.vbCrLf & "  AND UPM.FTMnuName='" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) & "'   "
            _Qry &= Constants.vbCrLf & "  AND ISNULL(UPT.FTStateAll,'') <> '1' AND UPT.FTStateSalary='1'   "


        End If

        _Qry &= Constants.vbCrLf & ")  As LS ON M.FNHSysSectId = LS.FNHSysSectId "


        _Qry &= Constants.vbCrLf & " WHERE M.FTEmpCode<>'' "
        _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)


        _Qry &= Constants.vbCrLf & "  AND  M.FNHSysEmpID<> 0 "

        If _Formular <> "" Then
            _Qry &= Constants.vbCrLf & "  AND ( " & _Formular.Replace("THRMEmployee", "M").Replace("TCNMUnitSect", "US").Replace("TCNMSect", "S").Replace("TCNMDivision", "Div").Replace("TCNMDepartment", "Dept").Replace("THRMEmpType", "ET").Replace("{", "").Replace("}", "").Replace("[", "(").Replace("]", ")") & " ) "
        End If

        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)
    End Sub
    
End Class