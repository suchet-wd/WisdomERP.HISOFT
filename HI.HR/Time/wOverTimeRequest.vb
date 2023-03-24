Public Class wOverTimeRequest

    Private _ListOTOver As wListEmplyeeOTOver
    Private _Report As wHROverTimeRequestSlip

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ActualDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

        Call InitGrid()

        _ListOTOver = New wListEmplyeeOTOver
        HI.TL.HandlerControl.AddHandlerObj(_ListOTOver)


        Dim _SystemLang As New ST.SysLanguage
        Try
            Call _SystemLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ListOTOver.Name.ToString.Trim, _ListOTOver)
        Catch ex As Exception
        Finally
        End Try

        _Report = New wHROverTimeRequestSlip
        HI.TL.HandlerControl.AddHandlerObj(_Report)

        Try
            Call _SystemLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _Report.Name.ToString.Trim, _Report)
        Catch ex As Exception
        Finally
        End Try

    End Sub

#Region "Initial Grid"

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = "FTEmpCode"
        Dim sFieldSum As String = ""
        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        With ogv
            .ClearGrouping()
            .ClearDocument()

            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True

        End With
        '------End Add Summary Grid-------------
    End Sub
#End Region


#Region "Property"

    Private _ActualDate As String = ""
    ReadOnly Property ActualDate As String
        Get
            Return _ActualDate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property

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

    Private Sub ProcessSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click

        If Me.VerrifyData Then

            If Me.FNHSysEmpTypeId.Text <> "" Then
                If HI.HRCAL.Time.CheckClosePeriod(FTDateRequest.Text, 0, Integer.Parse(Val(Me.FNHSysEmpTypeId.Properties.Tag.ToString))) = True Then
                    HI.MG.ShowMsg.mInfo("ไม่สามารถทำการแก้ไขข้อมูลหลังการปิดงวดได้ กรุณาทำการตรวจสอบ", 14040001, Me.Text)
                    Exit Sub
                End If
            End If

            If checkdateover(HI.UL.ULDate.ConvertEnDB(FTDateRequest.Text)) = False Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถทำการขอล่วงหน้าได้เกิน 15 วัน !!!!", 1404170456, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim _Spls As New HI.TL.SplashScreen("Saving And Calculating Work Time...   Please Wait   ")
            Dim _dtOTOver As New DataTable
            _dtOTOver.Columns.Add("FTEmpCode", GetType(String))
            _dtOTOver.Columns.Add("FTEmpName", GetType(String))
            _dtOTOver.Columns.Add("FTTime", GetType(String))
            _dtOTOver.Columns.Add("FTStateLockNotOver", GetType(String))

            If Me.SaveData(_Spls, _dtOTOver) Then
                _Spls.Close()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                If _dtOTOver.Rows.Count > 0 Then
                    HI.MG.ShowMsg.mInfo("พบข้อมูลพนักงานทำโอทีเกิน", 1404010002, Me.Text)

                    With _ListOTOver
                        .ogclist.DataSource = _dtOTOver
                        .ShowDialog()
                    End With

                End If

                Me.ocmload_Click(ocmload, New System.EventArgs)
            Else
                _Spls.Close()
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If

            _dtOTOver.Dispose()


        End If

    End Sub

    Private Sub ProcessDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If CType(ogc.DataSource, DataTable).Rows.Count > 0 Then

            If Me.FNHSysEmpTypeId.Text <> "" Then
                If HI.HRCAL.Time.CheckClosePeriod(FTDateRequest.Text, 0, Integer.Parse(Val(Me.FNHSysEmpTypeId.Properties.Tag.ToString))) = True Then
                    HI.MG.ShowMsg.mInfo("ไม่สามารถทำการแก้ไขข้อมูลหลังการปิดงวดได้ กรุณาทำการตรวจสอบ", 14040001, Me.Text)
                    Exit Sub
                End If
            End If

            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบข้อมูลการขอโอทีใช่หรือไม่ ?", 1404290031, FTDateRequest.Text) = True Then
                CType(ogc.DataSource, DataTable).AcceptChanges()
                If CType(ogc.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then
                    If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text) = True Then
                        Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")

                        If Me.DeleteData(_Spls) Then

                            _Spls.Close()
                            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                            Me.ocmload_Click(ocmload, New System.EventArgs)

                        Else
                            _Spls.Close()
                            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                        End If

                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกพนักงาน", 1304030001, Me.Text)
                End If
            End If
           
        Else
            HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกพนักงาน", 1304030001, Me.Text)
        End If

    End Sub

    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click

        Me.ogc.DataSource = Nothing
        ochkselectall.Checked = False

        HI.TL.HandlerControl.ClearControl(Me)
        Me.FTDateRequest.Focus()

    End Sub

    Private Sub ProcessPreview(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmpreview.Click

        'With _Report
        '    HI.ST.Lang.SP_SETxLanguage(_Report)
        '    HI.TL.HandlerControl.ClearControl(_Report)
        '    .SFTDateTrans.Text = Me.FTDateRequest.Text
        '    .ocmpreview.Enabled = True
        '    .ocmexit.Enabled = True
        '    .ShowDialog()
        'End With

        If Me.FTDateRequest.Text <> "" Then

            If CType(Me.ogc.DataSource, DataTable).Rows.Count <= 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกข้อมูลพนักงาน !!!", 1606110099, Me.Text)
                Exit Sub
            End If

            Dim _Fm As String = " {THRTDailyOTRequest.FTDateRequest}='" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"
            _Fm &= "  AND  {THRMEmployee.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

            Dim _FmEmpMonth As String = ""
            Dim _FmEmpOther As String = ""
            Dim dtMonth As DataTable

            Dim _spls As New HI.TL.SplashScreen("Creating Data... Please Wait.", Me.Text)
            Try

                Dim _Qry As String
                _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_TmpReport WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)
                Dim Rx() As DataRow
                With CType(Me.ogc.DataSource, DataTable)
                    .AcceptChanges()
                    Rx = .Select("FTSelect='1'")

                    If Rx.Length <= 0 Then
                        Rx = .Select("FTSelect<>'1'")
                    End If

                    For Each R As DataRow In Rx
                        Call InsertTmpData(Integer.Parse(Val(R!FNHSysEmpID.ToString)))
                    Next

                End With


                _Qry = "    Select DISTINCT  B.FNHSysEmpTypeId"
                _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_TmpReport AS A INNER JOIN"
                _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS B ON A.FNHSysEmpTypeId = B.FNHSysEmpTypeId"
                _Qry &= vbCrLf & "   WHERE   (A.FTUserLogin ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') "
                _Qry &= vbCrLf & "   AND (B.FNCalType = 2)"
                dtMonth = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                For Each R As DataRow In dtMonth.Rows
                    If _FmEmpMonth = "" Then
                        _FmEmpMonth = " ( {THRMEmployee.FNHSysEmpTypeId} IN [" & Integer.Parse(Val(R!FNHSysEmpTypeId.ToString)).ToString & ""
                    Else
                        _FmEmpMonth &= "," & Integer.Parse(Val(R!FNHSysEmpTypeId.ToString)).ToString & ""
                    End If
                Next

                If _FmEmpMonth <> "" Then
                    _FmEmpMonth &= "])"
                End If

                _Qry = "    Select TOP 1 A.FTUserLogin "
                _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_TmpReport AS A INNER JOIN"
                _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS B ON A.FNHSysEmpTypeId = B.FNHSysEmpTypeId"
                _Qry &= vbCrLf & "   WHERE   (A.FTUserLogin ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') "
                _Qry &= vbCrLf & "   AND (B.FNCalType <> 2)"
                _FmEmpOther = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

                _spls.Close()

            Catch ex As Exception
                _spls.Close()
            End Try

            If _FmEmpOther <> "" Then

                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .ReportFolderName = "Human Report\"
                    .ReportName = "OTRequestSlip.rpt"

                    If _FmEmpMonth = "" Then
                        .Formular = _Fm
                    Else
                        .Formular = _Fm & " AND ( NOT  " & _FmEmpMonth & " ) "
                    End If

                    .Preview()
                End With

            End If

            If _FmEmpMonth <> "" Then
                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .ReportFolderName = "Human Report\"
                    .ReportName = "OTRequestSlip_M.rpt"
                    .Formular = _Fm & " AND  " & _FmEmpMonth
                    .Preview()
                End With
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTDateRequest_lbl.Text)
            FTDateRequest.Focus()
        End If
    End Sub

    Private Sub InsertTmpData(FNHSysEmpID As Integer)

        Dim _Qry As String = ""
        _Qry = "  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_TmpReport(FTUserLogIn, FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FNHSysEmpID, FTEmpCode, FNHSysCmpId, FTEmpCodeRefer, FNHSysPreNameId, FTEmpNameTH, FTEmpSurnameTH, "
        _Qry &= vbCrLf & "   FTEmpNicknameTH, FTEmpNameEN, FTEmpSurnameEN, FTEmpNicknameEN, FNEmpSex, FNUseBarcode, FTEmpBarcode, FTEmpPicName, FNHSysShiftID, FNScanCtrl, FDDateStart, FDDateEnd,"
        _Qry &= vbCrLf & "   FNHSysResignId, FTResign, FDDateProbation, FTProbationSta, FNEmpStatus, FNHSysEmpTypeId, FNHSysDeptId, FNHSysDivisonId, FNHSysSectId, FNHSysUnitSectId, FNHSysPositId, FNHSysEmpIDLeader,"
        _Qry &= vbCrLf & "   FNLateCutSta, FNPaidOTSta, FDBirthDate, FNHSysBldId, FCWeight, FCHeight, FNHSysRaceId, FNHSysNationalityId, FNHSysReligionId, FNMilitary, FTMilitaryNote, FTEmpIdNo, FDDateIdNoAssign,"
        _Qry &= vbCrLf & "   FDDateIdNoEnd, FTEmpIdNoBy, FTSocialNo, FNHSysHospitalId, FTTaxNo, FNEverRegisSta, FNCalSocialSta, FNCalTaxSta, FNHSysPayRollPayId, FTAccNo, FNHSysBankId, FNHSysBankBranchId, FNCarStatus,"
        _Qry &= vbCrLf & "   FTCarId, FTCarLicense, FNMotorCycleStatus, FTMotorCycleId, FTMotorCycleLicense, FTDrug, FTDiesea, FTHobby, FTCriminalCauseSta, FTCriminalCause, FTAddrNo, FTAddrHome, FTAddrMoo, FTAddrSoi,"
        _Qry &= vbCrLf & "   FTAddrRoad, FTAddrTumbol, FTAddrAmphur, FTAddrProvince, FTAddrPostCode, FTAddrTel, FTAddrNo1, FTAddrHome1, FTAddrMoo1, FTAddrSoi1, FTAddrRoad1, FTAddrTumbol1, FTAddrAmphur1, FTAddrProvince1,"
        _Qry &= vbCrLf & "   FTAddrPostCode1, FTAddrTel1, FTMobile, FTEmail, FNSalary, FNMaritalStatus, FTRefName, FTRefAddr, FTRefCareer, FTRefPosit, FTRefAddrWork, FTRefTel, FTRefRelation, FTRefNote, FTRefName1, FTRefAddr1,"
        _Qry &= vbCrLf & "   FTRefCareer1, FTRefPosit1, FTRefAddrWork1, FTRefTel1, FTRefRelation1, FTRefNote1, FTFatherName, FNFatherLife, FTFatherIDNo, FTFatherAddr, FTFatherCareer, FTFatherPosit, FTFatherAddrWork, FTFatherTel,"
        _Qry &= vbCrLf & "   FTMotherName, FNMotherLife, FTMotherIDNo, FTMotherAddr, FTMotherCareer, FTMotherPosit, FTMotherAddrWork, FTMotherTel, FTMateName, FTMateIncome, FNMateLife, FTMateIDNo, FTMateAddr,"
        _Qry &= vbCrLf & "   FTMateCareer, FTMatePosit, FTMateAddrWork, FTMateTel, FTMateFatherName, FTMateFatherIDNo, FTMateMotherName, FTMateMotherIDNo, FCModFather, FCModMother, FCModMateFather, FCModMateMother,"
        _Qry &= vbCrLf & "   FCPremium, FCInterest, FCUnitRMF, FCUnitLTF, FCDeductDonate, FCDeductDonateStudy, FCDeductDividend, FCDisabledDependents, FCHealthInsurFatherMotherMate, FTHealthInsurIDFather,"
        _Qry &= vbCrLf & "   FTHealthInsurIDMother, FTHealthInsurIDFatherMate, FTHealthInsurIDMotherMate, FCIncomeBefore, FCTaxBefore, FCSocialBefore, FTFundIDNo, FDFundBegin, FDFundEnd, FCExceptAgeOver,"
        _Qry &= vbCrLf & "   FCExceptAgeOverMate, FDRetire, FTStaCalMonthEnd, FDDateTransfer, FTDeligentCode )"
        _Qry &= vbCrLf & " SELECT   '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS  FTUserLogIn,"
        _Qry &= vbCrLf & " M.FTInsUser, M.FDInsDate, M.FTInsTime, M.FTUpdUser, M.FDUpdDate, M.FTUpdTime, M.FNHSysEmpID, M.FTEmpCode, M.FNHSysCmpId, M.FTEmpCodeRefer, M.FNHSysPreNameId, M.FTEmpNameTH, "
        _Qry &= vbCrLf & "  M.FTEmpSurnameTH, M.FTEmpNicknameTH, M.FTEmpNameEN, M.FTEmpSurnameEN, M.FTEmpNicknameEN, M.FNEmpSex, M.FNUseBarcode, M.FTEmpBarcode, M.FTEmpPicName, M.FNHSysShiftID,"
        _Qry &= vbCrLf & "  M.FNScanCtrl, M.FDDateStart, M.FDDateEnd, M.FNHSysResignId, M.FTResign, M.FDDateProbation, M.FTProbationSta, M.FNEmpStatus, M.FNHSysEmpTypeId, M.FNHSysDeptId, M.FNHSysDivisonId,"
        _Qry &= vbCrLf & "  M.FNHSysSectId, M.FNHSysUnitSectId, M.FNHSysPositId, M.FNHSysEmpIDLeader, M.FNLateCutSta, M.FNPaidOTSta, M.FDBirthDate, M.FNHSysBldId, M.FCWeight, M.FCHeight, M.FNHSysRaceId,"
        _Qry &= vbCrLf & "  M.FNHSysNationalityId, M.FNHSysReligionId, M.FNMilitary, M.FTMilitaryNote, M.FTEmpIdNo, M.FDDateIdNoAssign, M.FDDateIdNoEnd, M.FTEmpIdNoBy, M.FTSocialNo, M.FNHSysHospitalId, M.FTTaxNo,"
        _Qry &= vbCrLf & "  M.FNEverRegisSta, M.FNCalSocialSta, M.FNCalTaxSta, M.FNHSysPayRollPayId, M.FTAccNo, M.FNHSysBankId, M.FNHSysBankBranchId, M.FNCarStatus, M.FTCarId, M.FTCarLicense, M.FNMotorCycleStatus,"
        _Qry &= vbCrLf & "  M.FTMotorCycleId, M.FTMotorCycleLicense, M.FTDrug, M.FTDiesea, M.FTHobby, M.FTCriminalCauseSta, M.FTCriminalCause, M.FTAddrNo, M.FTAddrHome, M.FTAddrMoo, M.FTAddrSoi, M.FTAddrRoad,"
        _Qry &= vbCrLf & "  M.FTAddrTumbol, M.FTAddrAmphur, M.FTAddrProvince, M.FTAddrPostCode, M.FTAddrTel, M.FTAddrNo1, M.FTAddrHome1, M.FTAddrMoo1, M.FTAddrSoi1, M.FTAddrRoad1, M.FTAddrTumbol1, M.FTAddrAmphur1,"
        _Qry &= vbCrLf & "  M.FTAddrProvince1, M.FTAddrPostCode1, M.FTAddrTel1, M.FTMobile, M.FTEmail"
        _Qry &= vbCrLf & " , CASE WHEN ES.FNHSysEmpTypeId IS NULL AND LS.FNHSysSectId IS NULL THEN 0 ELSE M.FNSalary END AS FNSalary "
        _Qry &= vbCrLf & " , M.FNMaritalStatus, M.FTRefName, M.FTRefAddr, M.FTRefCareer, M.FTRefPosit, M.FTRefAddrWork, M.FTRefTel,"
        _Qry &= vbCrLf & "  M.FTRefRelation, M.FTRefNote, M.FTRefName1, M.FTRefAddr1, M.FTRefCareer1, M.FTRefPosit1, M.FTRefAddrWork1, M.FTRefTel1, M.FTRefRelation1, M.FTRefNote1, M.FTFatherName, M.FNFatherLife,"
        _Qry &= vbCrLf & "  M.FTFatherIDNo, M.FTFatherAddr, M.FTFatherCareer, M.FTFatherPosit, M.FTFatherAddrWork, M.FTFatherTel, M.FTMotherName, M.FNMotherLife, M.FTMotherIDNo, M.FTMotherAddr, M.FTMotherCareer,"
        _Qry &= vbCrLf & "  M.FTMotherPosit, M.FTMotherAddrWork, M.FTMotherTel, M.FTMateName, M.FTMateIncome, M.FNMateLife, M.FTMateIDNo, M.FTMateAddr, M.FTMateCareer, M.FTMatePosit, M.FTMateAddrWork, M.FTMateTel,"
        _Qry &= vbCrLf & "  M.FTMateFatherName, M.FTMateFatherIDNo, M.FTMateMotherName, M.FTMateMotherIDNo, M.FCModFather, M.FCModMother, M.FCModMateFather, M.FCModMateMother, M.FCPremium, M.FCInterest,"
        _Qry &= vbCrLf & "  M.FCUnitRMF, M.FCUnitLTF, M.FCDeductDonate, M.FCDeductDonateStudy, M.FCDeductDividend, M.FCDisabledDependents, M.FCHealthInsurFatherMotherMate, M.FTHealthInsurIDFather,"
        _Qry &= vbCrLf & "  M.FTHealthInsurIDMother, M.FTHealthInsurIDFatherMate, M.FTHealthInsurIDMotherMate, M.FCIncomeBefore, M.FCTaxBefore, M.FCSocialBefore, M.FTFundIDNo, M.FDFundBegin, M.FDFundEnd,"
        _Qry &= vbCrLf & "  M.FCExceptAgeOver, M.FCExceptAgeOverMate, M.FDRetire, M.FTStaCalMonthEnd, M.FDDateTransfer, M.FTDeligentCode"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH(NOLOCK) "

        _Qry &= vbCrLf & " LEFT OUTER JOIN "
        _Qry &= vbCrLf & " ("

        If (HI.ST.SysInfo.Admin) Then
            _Qry &= vbCrLf & " SELECT DISTINCT   FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WITH(NOLOCK)  "
        Else
            _Qry &= vbCrLf & " SELECT    DISTINCT     RT.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS RT WITH(NOLOCK)   INNER JOIN"
            _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS U WITH(NOLOCK)   ON RT.FNHSysPermissionID = U.FNHSysPermissionID "
            _Qry &= vbCrLf & "  WHERE U.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AND FTStateSalary='1' "
            _Qry &= vbCrLf & " "
        End If

        _Qry &= vbCrLf & " )  As ES ON M.FNHSysEmpTypeId = ES.FNHSysEmpTypeId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN "
        _Qry &= vbCrLf & "   ("

        If (HI.ST.SysInfo.Admin) Then
            _Qry &= vbCrLf & " SELECT DISTINCT  FNHSysSectId  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect WITH(NOLOCK)   "
        Else

            _Qry &= vbCrLf & " Select DISTINCT S.FNHSysSectId"
            _Qry &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID"
            _Qry &= vbCrLf & "   CROSS JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S  WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE UP.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & "  AND UPM.FTMnuName='" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) & "' "
            _Qry &= vbCrLf & "  AND UPT.FTStateAll='1' AND UPT.FTStateSalary='1'    "
            _Qry &= vbCrLf & " UNION"
            _Qry &= vbCrLf & " Select DISTINCT S.FNHSysSectId"
            _Qry &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID INNER JOIN "
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeTypeSect AS UPT2 WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT2.FNHSysPermissionID AND UPT.FNHSysPermissionID = UPT2.FNHSysPermissionID "
            _Qry &= vbCrLf & "   INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH(NOLOCK) ON UPT2.FNHSysSectId = S.FNHSysSectId  "
            _Qry &= vbCrLf & "  WHERE UP.FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & "  AND UPM.FTMnuName='" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) & "'   "
            _Qry &= vbCrLf & "  AND ISNULL(UPT.FTStateAll,'') <> '1' AND UPT.FTStateSalary='1'   "

        End If

        _Qry &= vbCrLf & ")  As LS ON M.FNHSysSectId = LS.FNHSysSectId "

        _Qry &= vbCrLf & " WHERE M.FNHSysEmpID =" & FNHSysEmpID & ""
        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

    End Sub
    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Procedure "

    Private Function SaveData(Spls As HI.TL.SplashScreen, ByRef _dtref As DataTable) As Boolean

        Dim _Qry As String
        CType(ogc.DataSource, DataTable).AcceptChanges()
        Dim _Dt As DataTable = CType(ogc.DataSource, DataTable)
        Dim _tmpdt As DataTable

        Dim _ToatlRecord As Integer = _Dt.Select("FTSelect='1'").Length
        Dim _Rec As Integer = 0
        Dim totalSum As Integer = 0

        Dim _TotalHour As Double = 0

        Dim _Rest As Double = 0
        Dim _FNTotalMonute As Double = 0

        Dim _TotalHour2 As Double = 0
        Dim _Rest2 As Double = 0
        Dim _FNTotalMonute2 As Double = 0

        Dim _TotalNetHour As Double = 0
        Dim _FNTotalNetMonute As Double = 0
        Dim _FoundOver As Boolean = False
        '---------OT รวมทั้งหมด----------------
        _FNTotalNetMonute = Me.ocetotaltimea.Value + Me.ocetotaltimem.Value
        _TotalNetHour = CDbl(Format((_FNTotalNetMonute \ 60.0) + ((_FNTotalNetMonute - ((_FNTotalNetMonute \ 60.0) * 60.0)) / 100.0), "0.00"))
        '---------OT รวมทั้งหมด----------------

        '---------OT ช่วงเช้า------------------
        _FNTotalMonute = Me.ocetotaltimea.Value
        _TotalHour = CDbl(Format((_FNTotalMonute \ 60.0) + ((_FNTotalMonute - ((_FNTotalMonute \ 60.0) * 60.0)) / 100.0), "0.00"))
        _Rest = Integer.Parse(Me.rest2.Value)
        _FNTotalMonute = ((_TotalHour - (_TotalHour Mod 1)) * 60) + ((_TotalHour Mod 1) * 100)
        '---------OT ช่วงเช้า-----------------

        '---------OT ช่วงบ่าย------------------
        _FNTotalMonute2 = Me.ocetotaltimem.Value
        _TotalHour2 = CDbl(Format((_FNTotalMonute2 \ 60.0) + ((_FNTotalMonute2 - ((_FNTotalMonute2 \ 60.0) * 60.0)) / 100.0), "0.00"))
        _Rest2 = Integer.Parse(Me.rest1.Value)
        '---------OT ช่วงบ่าย------------------

        Try

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _Dt.Rows
                _FoundOver = False
                If R!FTSelect.ToString = "1" Then
                    _Rec = _Rec + 1

                    If HI.HRCAL.Time.CheckClosePeriod(FTDateRequest.Text, 0, Integer.Parse(Val(R!FNHSysEmpTypeId.ToString))) = False Then

                        If Not (Spls Is Nothing) Then
                            Spls.UpdateInformation("Save Data Employee " & R!FTEmpCode.ToString & "   Record   " & _Rec.ToString & " Of " & _ToatlRecord.ToString & "  (" & Format((_Rec * 100.0) / _ToatlRecord, "0.00") & " % ) ")
                        End If

                        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GETTimeOTWeek " & Val(R!FNHSysEmpID) & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "' "
                        _tmpdt = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Qry)

                        For Each Rx As DataRow In _tmpdt.Rows

                            Select Case Rx!FTStateFixOTPerWeek.ToString
                                Case "1"
                                    totalSum = (Val(Rx!FNActualMin.ToString) + _FNTotalMonute)

                                    If totalSum > Val(Rx!FNOTPerWeekMin.ToString) Then
                                        _dtref.Rows.Add(R!FTEmpCode, R!FTEmpName, Format(((totalSum) \ 60), "00") & " : " & Format(((totalSum) Mod 60), "00"), R!FTStateLockNotOver.ToString)

                                        If R!FTStateLockNotOver.ToString = "1" Then
                                            _FoundOver = True
                                        End If

                                    End If

                            End Select

                            Exit For
                        Next

                        If Not (_FoundOver) Then

                            _Qry = " SELECT TOP 1 FTDateRequest  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest WITH (NOLOCK)"
                            _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(R!FNHSysEmpID) & " "
                            _Qry &= vbCrLf & " AND FTDateRequest = '" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"

                            If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR, "") <> "" Then

                                _Qry = " Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest "
                                _Qry &= vbCrLf & "  SET  FTUpdUser='" & HI.ST.UserInfo.UserName & "' "
                                _Qry &= vbCrLf & " ,FTUpdDate=" & HI.UL.ULDate.FormatDateDB & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & "  "
                                _Qry &= vbCrLf & " ,FTOtIn='" & Me.otba1starttime.Text & "' "
                                _Qry &= vbCrLf & " ,FTOtOut='" & Me.otba1endtime.Text & "' "
                                _Qry &= vbCrLf & " ,FNRest=" & _Rest & " "
                                _Qry &= vbCrLf & " ,FNOtTotalTime=" & _TotalHour & " "
                                _Qry &= vbCrLf & " ,FNOtTotalTimeMinute=" & _FNTotalMonute & " "
                                _Qry &= vbCrLf & " ,FTOtIn3='" & Me.otba2starttime.Text & "' "
                                _Qry &= vbCrLf & " ,FTOtOut3='" & Me.otba2endtime.Text & "' "
                                _Qry &= vbCrLf & " ,FNRest2=" & _Rest2 & " "
                                _Qry &= vbCrLf & " ,FNOtTotalTime2=" & _TotalHour2 & " "
                                _Qry &= vbCrLf & " ,FNOtTotalTimeMinute2=" & _FNTotalMonute2 & " "
                                _Qry &= vbCrLf & " ,FNOtNetTime=" & _TotalNetHour & " "
                                _Qry &= vbCrLf & " ,FNOtNetTimeMinute=" & _FNTotalNetMonute & " "
                                _Qry &= vbCrLf & " ,FTOtNote='', FTApproveState='1' ,FTStateDaily='" & HI.UL.ULF.rpQuoted(FTStateDaily.EditValue.ToString) & "' "
                                _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(R!FNHSysEmpID) & ""
                                _Qry &= vbCrLf & " AND FTDateRequest = '" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"

                            Else

                                _Qry = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest (  FTInsUser, FTInsDate, FTInsTime "
                                _Qry &= vbCrLf & "  , FNHSysEmpID, FTDateRequest, FTOtIn, FTOtOut"
                                _Qry &= vbCrLf & "  , FNOtTotalTime, FNOtTotalTimeMinute, FTOtNote, FTApproveState,FNRest,FNRest2,FNOtTotalTime2, FNOtTotalTimeMinute2"
                                _Qry &= vbCrLf & ",FTOtIn3,FTOtOut3,FNOtNetTime,FNOtNetTimeMinute,FTStateDaily )  "
                                _Qry &= vbCrLf & " SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & "  "
                                _Qry &= vbCrLf & " ,'" & Val(R!FNHSysEmpID) & "'"
                                _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "','" & Me.otba1starttime.Text & "','" & Me.otba1endtime.Text & "' "
                                _Qry &= vbCrLf & " ," & _TotalHour & "," & _FNTotalMonute & ",'','1'," & _Rest & " "
                                _Qry &= vbCrLf & " ," & _Rest2 & "," & _TotalHour2 & "," & _FNTotalMonute2 & "  "
                                _Qry &= vbCrLf & ",'" & Me.otba2starttime.Text & "','" & Me.otba2endtime.Text & "'," & _TotalNetHour & "," & _FNTotalNetMonute & ",'" & HI.UL.ULF.rpQuoted(FTStateDaily.EditValue.ToString) & "' "

                            End If

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                                Return False

                            End If

                        End If
                    End If
     
                End If
            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Rec = 0
            For Each R As DataRow In _Dt.Rows
                If R!FTSelect.ToString = "1" Then

                    _Rec = _Rec + 1
                    If HI.HRCAL.Time.CheckClosePeriod(FTDateRequest.Text, 0, Integer.Parse(Val(R!FNHSysEmpTypeId.ToString))) = False Then
                        If Not (Spls Is Nothing) Then
                            Spls.UpdateInformation("Calculate Work Time Employee " & R!FTEmpCode.ToString & "   Record   " & _Rec.ToString & " Of " & _ToatlRecord.ToString & "  (" & Format((_Rec * 100.0) / _ToatlRecord, "0.00") & " % ) ")
                        End If

                        HI.HRCAL.Calculate.TransTimeCard(HI.ST.UserInfo.UserName, HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text), Val(R!FNHSysEmpID))
                    End If

                End If
            Next

            Return True

        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return False
        End Try

    End Function

    Private Function DeleteData(Spls As HI.TL.SplashScreen) As Boolean
        Try

            CType(ogc.DataSource, DataTable).AcceptChanges()
            Dim _Dt As DataTable = CType(ogc.DataSource, DataTable)

            Dim _ToatlRecord As Integer = _Dt.Select("FTSelect='1'").Length
            Dim _Rec As Integer = 0

            Dim _Qry As String = ""

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _Dt.Rows

                If R!FTSelect.ToString = "1" Then

                    _Rec = _Rec + 1
                    If HI.HRCAL.Time.CheckClosePeriod(FTDateRequest.Text, 0, Integer.Parse(Val(R!FNHSysEmpTypeId.ToString))) = False Then
                        If Not (Spls Is Nothing) Then
                            Spls.UpdateInformation("Delete Data Employee " & R!FTEmpCode.ToString & "   Record   " & _Rec.ToString & " Of " & _ToatlRecord.ToString & "  (" & Format((_Rec * 100.0) / _ToatlRecord, "0.00") & " % ) ")
                        End If

                        _Qry = " Delete FROM dbo.THRTDailyOTRequest "
                        _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(R!FNHSysEmpID) & " "
                        _Qry &= vbCrLf & " AND FTDateRequest = '" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        End If
                    End If


                End If
            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Rec = 0
            For Each R As DataRow In _Dt.Rows
                If R!FTSelect.ToString = "1" Then
                    _Rec = _Rec + 1
                    If HI.HRCAL.Time.CheckClosePeriod(FTDateRequest.Text, 0, Integer.Parse(Val(R!FNHSysEmpTypeId.ToString))) = False Then
                        If Not (Spls Is Nothing) Then
                            Spls.UpdateInformation("Calculate Work Time Employee " & R!FTEmpCode.ToString & "   Record   " & _Rec.ToString & " Of " & _ToatlRecord.ToString & "  (" & Format((_Rec * 100.0) / _ToatlRecord, "0.00") & " % ) ")
                        End If

                        HI.HRCAL.Calculate.TransTimeCard(HI.ST.UserInfo.UserName, HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text), Val(R!FNHSysEmpID))
                    End If
                End If
            Next

            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Function VerrifyData() As Boolean
        Dim _Pass As Boolean = False
        If HI.UL.ULDate.CheckDate(FTDateRequest.Text) <> "" Then
            If Not (ogc.DataSource Is Nothing) Then
                CType(ogc.DataSource, DataTable).AcceptChanges()
                If CType(ogc.DataSource, DataTable).Rows.Count > 0 Then
                    If CType(ogc.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then



                        If Me.otba1starttime.Text = Me.otba1endtime.Text Then
                            Me.otba1starttime.Text = ""
                            Me.otba1endtime.Text = ""
                        End If

                        If Me.otba2starttime.Text = Me.otba2endtime.Text Then
                            Me.otba2starttime.Text = ""
                            Me.otba2endtime.Text = ""
                        End If

                        If (Me.otba1starttime.Text <> "" And Me.otba1endtime.Text <> "") Or
                                (Me.otba2starttime.Text <> "" And Me.otba2endtime.Text <> "") Or (Me.FTStateDaily.Checked) Then

                            _Pass = True

                        Else

                            HI.MG.ShowMsg.mInvalidData("กรุณาทำการเวลาขอโอที", 1304030002, Me.Text)
                        End If

                    Else
                        HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกพนักงาน", 1304030001, Me.Text)
                        FTDateRequest.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกพนักงาน", 1304030001, Me.Text)
                    FTDateRequest.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกพนักงาน", 1304030001, Me.Text)
                FTDateRequest.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateRequest_lbl.Text)
            FTDateRequest.Focus()
        End If

        Return _Pass
    End Function

    Private Sub LoadDataInfo()
        Me.ogc.DataSource = Nothing
        ochkselectall.Checked = False

        Dim _Dt As DataTable
        Dim _Qry As String = ""

        _Qry = "SELECT  '0' AS FTSelect,  M.FNHSysEmpID, M.FTEmpCode,M.FNHSysEmpTypeId"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & ",P.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

        Else
            _Qry &= vbCrLf & ",P.FTPreNameNameEN + ' ' +  M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
        End If

        _Qry &= vbCrLf & ",ST.FTSectCode "
        _Qry &= vbCrLf & ",US.FTUnitSectCode "
        _Qry &= vbCrLf & ",SH.FTShiftNameTH AS FTShiftName"
        _Qry &= vbCrLf & ",OTR.FTOtIn, OTR.FTOtOut, OTR.FTOtIn3, OTR.FTOtOut3"
        _Qry &= vbCrLf & ",OTR.FNOtTotalTime, OTR.FTApproveState "
        _Qry &= vbCrLf & ",ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
        _Qry &= vbCrLf & ",ISNULL(Dept.FTDeptCode,'') AS FTDeptCode "
        _Qry &= vbCrLf & ",ISNULL(DI.FTDivisonCode,'') AS FTDivisonCode "
        _Qry &= vbCrLf & ",ISNULL(MSD.FTShiftCode,SH.FTShiftCode) AS FTShiftCode "
        _Qry &= vbCrLf & ",ISNULL(OTR.FTScanOtIn,'') AS FTScanOtIn "
        _Qry &= vbCrLf & ",ISNULL(OTR.FTScanOtOut,'') AS FTScanOtOut "
        _Qry &= vbCrLf & ",ISNULL(OTR.FTWorkTime,'') AS FTWorkTime "
        _Qry &= vbCrLf & ",ISNULL(OTR.FTStateDaily,'0') AS FTStateDaily "
        _Qry &= vbCrLf & ",ISNULL(OTR.FTSaveBy,'') AS FTSaveBy "
        _Qry &= vbCrLf & ",[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_LoadTimeOTWeek( M.FNHSysEmpID,'" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "') AS FTOTWork "
        _Qry &= vbCrLf & ",ISNULL(EmpOT.FTStateLockNotOver,ISNULL(ET.FTStateLockNotOver,'0') ) AS FTStateLockNotOver "
        _Qry &= vbCrLf & ",(ISNULL(EmpOT.FNOTPerWeek,ISNULL(ET.FNOTPerWeek,0) ) *60 ) AS FNMaxOTPerWeek "
        _Qry &= vbCrLf & " FROM        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        _Qry &= vbCrLf & "   THRMTimeShift AS SH WITH (NOLOCK) ON M.FNHSysShiftID = SH.FNHSysShiftID "
        _Qry &= vbCrLf & "   INNER Join "
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId "
        _Qry &= vbCrLf & " 	 LEFT OUTER JOIN("
        _Qry &= vbCrLf & "		SELECT        M.FNHSysEmpID, M.FDShiftDate,S.FNHSysShiftID, S.FTShiftCode, S.FTShiftNameTH, S.FTShiftNameEN"
        _Qry &= vbCrLf & "		FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "	 [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS S WITH (NOLOCK) ON M.FNHSysShiftID = S.FNHSysShiftID"
        _Qry &= vbCrLf & "	WHERE        (M.FDShiftDate ='" & HI.UL.ULDate.ConvertEnDB(FTDateRequest.Text) & "')"
        _Qry &= vbCrLf & " ) AS MS ON M.FNHSysEmpID =MS.FNHSysEmpID"
        _Qry &= vbCrLf & " LEFT OUTER JOIN("
        _Qry &= vbCrLf & "	SELECT OT.FTOtIn, OT.FTOtOut, OT.FTOtIn3, OT.FTOtOut3"
        _Qry &= vbCrLf & "		,OT.FNOtNetTime  AS  FNOtTotalTime, OT.FTApproveState, OT.FNHSysEmpID, OT.FTDateRequest"
        _Qry &= vbCrLf & ",TT.FTScanAOTIn AS FTScanOtIn "
        _Qry &= vbCrLf & ",CASE WHEN ISNULL(TT.FTScanAOTOut2,'')='' THEN TT.FTScanAOTOut ELSE TT.FTScanAOTOut2 END AS FTScanOtOut "
        _Qry &= vbCrLf & ",Replace(Convert(varchar(30),ISNULL(TT.FNOT1,0.00)),'.',':') AS FTWorkTime"
        _Qry &= vbCrLf & ",CASE WHEN ISNULL(OT.FTStateDaily,'0') ='1' THEN '1' ELSE '0' END  AS FTStateDaily "
        _Qry &= vbCrLf & ",CASE WHEN ISNULL(OT.FTUpdUser,'') ='' THEN  OT.FTInsUser ELSE ISNULL(OT.FTUpdUser,'') END  AS FTSaveBy "
        _Qry &= vbCrLf & "	FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest AS OT WITH (NOLOCK)"
        _Qry &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS TT WITH (NOLOCK) ON  OT.FNHSysEmpID = TT.FNHSysEmpID AND OT.FTDateRequest = TT.FTDateTrans   "
        _Qry &= vbCrLf & "	WHERE   (OT.FTDateRequest ='" & HI.UL.ULDate.ConvertEnDB(FTDateRequest.Text) & "')"
        _Qry &= vbCrLf & " ) AS OTR ON M.FNHSysEmpID = OTR.FNHSysEmpID"
        _Qry &= vbCrLf & "  LEFT OUTER JOIN  ( SELECT S.FNHSysEmpID,SH.FTShiftCode "
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift S  WITH (NOLOCK)"
        _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS SH WITH (NOLOCK) ON S.FNHSysShiftID = SH.FNHSysShiftID "
        _Qry &= vbCrLf & "  WHERE FDShiftDate ='" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "' "
        _Qry &= vbCrLf & "  "
        _Qry &= vbCrLf & ")  AS MSD  ON M.FNHSysEmpID = MSD.FNHSysEmpID  "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgEmployeeOTPerWeek AS EmpOT WITH (Nolock) ON M.FNHSysEmpID = EmpOT.FNHSysEmpID "

        _Qry &= vbCrLf & " WHERE  M.FTEmpCode <> '' AND ISNULL(M.FNPaidOTSta,0) = 0  "
        _Qry &= vbCrLf & " AND M.FDDateStart <='" & HI.UL.ULDate.ConvertEnDB(FTDateRequest.Text) & "' "
        _Qry &= vbCrLf & " AND (M.FDDateEnd ='' OR M.FDDateEnd >'" & HI.UL.ULDate.ConvertEnDB(FTDateRequest.Text) & "' )   "
        _Qry &= vbCrLf & " AND M.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "

        _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)


        If Me.FNHSysEmpTypeId.Text <> "" Then
            _Qry &= vbCrLf & " AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
        End If



        '------Criteria By Employeee Code
        If Me.FNHSysEmpId.Text <> "" Then
            _Qry &= vbCrLf & " AND M.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
        End If

        If Me.FNHSysEmpIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND M.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
        End If

        '------Criteria By Department
        If Me.FNHSysDeptId.Text <> "" Then
            _Qry &= vbCrLf & " AND  Dept.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
        End If

        If Me.FNHSysDeptIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  Dept.FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
        End If

        '------Criteria By Division
        If Me.FNHSysDivisonId.Text <> "" Then
            _Qry &= vbCrLf & " AND  DI.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
        End If

        If Me.FNHSysDivisonIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  DI.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
        End If

        '------Criteria By Sect
        If Me.FNHSysSectId.Text <> "" Then
            _Qry &= vbCrLf & " AND  ST.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
        End If

        If Me.FNHSysSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  ST.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
        End If

        '------Criteria Unit Sect
        If Me.FNHSysUnitSectId.Text <> "" Then
            _Qry &= vbCrLf & " AND   US.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
        End If

        If Me.FNHSysUnitSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND   US.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
        End If

        _Qry &= vbCrLf & " ORDER BY  M.FTEmpCode  "

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.ogc.DataSource = _Dt
    End Sub
#End Region

#Region "General"

    Private Function checkdateover(datadate As String) As Boolean
        Dim cmd As String = ""
        cmd = "SELECT DATEDIFF (day,Getdate(),Convert(Datetime,'" & datadate & "'))"

        If Val(HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_SYSTEM, "0")) > 15 Then
            Return False
        Else
            Return True
        End If

    End Function
    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If HI.UL.ULDate.CheckDate(FTDateRequest.Text) <> "" Then
            If checkdateover(HI.UL.ULDate.ConvertEnDB(FTDateRequest.Text)) = False Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถทำการขอล่วงหน้าได้เกิน 15 วัน !!!!", 1404170456, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
                Exit Sub
            End If

            Call LoadDataInfo()

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateRequest_lbl.Text)
            FTDateRequest.Focus()
        End If
    End Sub

    Private Sub FTDateRequest_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FTDateRequest.EditValueChanged
        Me.ogc.DataSource = Nothing
        ochkselectall.Checked = False
    End Sub

    Private Sub ochkselectall_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ochkselectall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With ogc
                If Not (.DataSource Is Nothing) And ogv.RowCount > 0 Then

                    With ogv
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub time_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles otba1starttime.EditValueChanged,
        otba1endtime.EditValueChanged,
        otba2starttime.EditValueChanged,
        otba2endtime.EditValueChanged

        Select Case sender.name.ToString.ToUpper

            Case "otba1starttime".ToUpper, "otba1endtime".ToUpper,
                 "otba2starttime".ToUpper, "otba2endtime".ToUpper

                Dim _T1 As Integer = 0
                Dim _T2 As Integer = 0
                Dim _Res As Integer = 0
                '------------------------OT เย็น ช่วงแรก -------------------------------
                If otba1starttime.Text <> "" And otba1endtime.Text <> "" Then
                    If otba1starttime.Text > otba1endtime.Text Then
                        _T1 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & otba1starttime.Text), CDate(Me.ActualNextDate & "  " & otba1endtime.Text))
                    Else
                        _T1 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & otba1starttime.Text), CDate(Me.ActualDate & "  " & otba1endtime.Text))
                    End If
                End If
                '------------------------OT เย็น ช่วงแรก -------------------------------

                '------------------------OT เย็น ช่วงที่สอง -------------------------------
                If otba2starttime.Text <> "" And otba2endtime.Text <> "" Then
                    If otba2starttime.Text > otba2endtime.Text Then
                        _T2 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & otba2starttime.Text), CDate(Me.ActualNextDate & "  " & otba2endtime.Text))
                    Else
                        _T2 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & otba2starttime.Text), CDate(Me.ActualDate & "  " & otba2endtime.Text))
                    End If
                End If
                '------------------------OT เย็น ช่วงที่สอง -------------------------------

                '-------- พักระหว่างช่วง ------------------------------------------------
                If _T1 > 0 And _T2 > 0 Then

                    If otba1endtime.Text > otba2starttime.Text Then
                        _Res = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & otba1endtime.Text), CDate(Me.ActualNextDate & "  " & otba2starttime.Text))
                    Else
                        _Res = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & otba1endtime.Text), CDate(Me.ActualDate & "  " & otba2starttime.Text))
                    End If
                End If
                '-------- พักระหว่างช่วง ------------------------------------------------

                ocetotaltimea.Value = _T1 + _T2
                rest2.Value = _Res
        End Select
        Dim _Total As Integer = ocetotaltimem.Value + ocetotaltimea.Value
        Me.ocetotaltime.Value = CDbl(Format((_Total \ 60.0) + ((_Total - ((_Total \ 60.0) * 60.0)) / 100.0), "0.00"))

    End Sub

    Private Sub wOTRequest_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)
    End Sub

    Private Sub ocetotaltime_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles ocetotaltime.EditValueChanged
        Try
            Me.otbttotaltime.Text = (Format(Me.ocetotaltime.Value, "0.00")).Replace(".", ":")
        Catch ex As Exception
            Me.otbttotaltime.Text = "0:00"
        End Try

    End Sub

    Private Sub ogv_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogv.RowCellStyle

        With ogv

            Try
                Dim _Time As Integer = Integer.Parse(Val(("" & .GetRowCellValue(e.RowHandle, "FTOTWork")).ToString.Split(":")(0))) + Integer.Parse(Val(("" & .GetRowCellValue(e.RowHandle, "FTOTWork")).ToString.Split(":")(1)))

                Select Case True
                    Case (_Time >= Integer.Parse(Val("" & .GetRowCellValue(e.RowHandle, "FNMaxOTPerWeek"))))
                        e.Appearance.BackColor = Drawing.Color.FromArgb(255, 192, 128)
                    Case (_Time > 0) And (_Time < Integer.Parse(Val("" & .GetRowCellValue(e.RowHandle, "FNMaxOTPerWeek"))))
                        e.Appearance.BackColor = Drawing.Color.FromArgb(192, 255, 192)
                End Select

            Catch ex As Exception
            End Try

        End With

    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogv)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub
End Class