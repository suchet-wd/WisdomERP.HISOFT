Imports System.Windows.Forms
Public Class wAddEmpHealth

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.


        With RepositoryFTDate
            RemoveHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_Leave
            ' AddHandler .Leave, AddressOf HI.TL.HandlerControl.RepositoryItemDate_GotFocus
            AddHandler .Leave, AddressOf ItemDate_Leave
        End With


    End Sub

#Region "Property"

    Private _EmpSysID As Integer = 0
    Public Property EmpSysID As Integer
        Get
            Return _EmpSysID
        End Get
        Set(value As Integer)
            _EmpSysID = value
        End Set
    End Property

    Private _DateCheck As String = ""
    Public Property DateCheck As String
        Get
            Return _DateCheck
        End Get
        Set(value As String)
            _DateCheck = value
        End Set
    End Property

    Private _HealthSeq As Integer = 0
    Public Property HealthSeq As Integer
        Get
            Return _HealthSeq
        End Get
        Set(value As Integer)
            _HealthSeq = value
        End Set
    End Property

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(value As Boolean)
            _ProcComplete = value
        End Set
    End Property

#End Region

#Region "General"
    Private Sub wAddEmpHealth_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Me.oTab.SelectedTabPageIndex = 0

            If Me.HealthSeq > 0 Then
                Call LoadDataEdit()

            Else
                Call LoadVaccHistory()
            End If
            HI.ST.Lang.SP_SETxLanguage(Me)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmsave_Click(sender As System.Object, e As System.EventArgs) Handles ocmsave.Click
        If Me.VerifyData() Then
            If Me.SaveData Then
                Call SaveHealth()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                Me.ProcComplete = True
                Me.Close()
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FDHealth.Text <> "" Then
            If Me.FNHSysHospitalId.Text <> "" And Me.FNHSysHospitalId.Properties.Tag.ToString <> "" Then
                _Pass = True
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysHospitalId_lbl.Text)
                FNHSysHospitalId.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FDHealth_lbl.Text)
            FDHealth.Focus()
        End If

        Return _Pass
    End Function

    Private Function SaveData() As Boolean
        Dim _Qry As String = ""
        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try

                _Qry = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHealthHistory SET "
                _Qry &= vbCrLf & " FDHealth=N'" & HI.UL.ULDate.ConvertEnDB(FDHealth.Text) & "',FTGenResults=N'" & HI.UL.ULF.rpQuoted(FTGenResults.Text) & "'"
                _Qry &= vbCrLf & " ,FCGenWeight=" & FCGenWeight.Value & ",FCGenHigh=" & FCGenHigh.Value & ",FTPregnant=N'" & HI.TL.CboList.GetListValue("FTPregnant", Me.FTPregnant.SelectedIndex) & "'"
                _Qry &= vbCrLf & ",FCBloodPressure='" & HI.UL.ULF.rpQuoted(FCBloodPressure.Text) & "',FTCarouse=N'" & HI.TL.CboList.GetListValue("FTCarouse", FTCarouse.SelectedIndex) & "'"
                _Qry &= vbCrLf & " ,FTSmoke=N'" & HI.TL.CboList.GetListValue("FTSmoke", FTSmoke.SelectedIndex) & "',FTCongenitalDisease=N'" & HI.UL.ULF.rpQuoted(FTCongenitalDisease.Text) & "', "
                _Qry &= vbCrLf & "FTResultsChest=N'" & HI.TL.CboList.GetListValue("FTResultsChest", FTResultsChest.SelectedIndex) & "',FTNumberXRay=N'" & HI.UL.ULF.rpQuoted(FTNumberXRay.Text) & "' "
                _Qry &= vbCrLf & ",FTRemainSugar=N'" & HI.UL.ULF.rpQuoted(FTRemainSugar.Text) & "',FTFBS=N'" & HI.UL.ULF.rpQuoted(FTFBS.Text) & "'"
                _Qry &= vbCrLf & ",FTUricAcid=N'" & HI.UL.ULF.rpQuoted(FTUricAcid.Text) & "',FTLiverSGOT=N'" & HI.UL.ULF.rpQuoted(FTLiverSGOT.Text) & "',FTLiverSGOTUL=N'" & FTLiverSGOTUL.Value.ToString & "'"
                _Qry &= vbCrLf & ",FTLiverSGPTUL=N'" & HI.UL.ULF.rpQuoted(FTLiverSGPTUL.Value.ToString) & "',FTLiverSGPT=N'" & HI.UL.ULF.rpQuoted(FTLiverSGPT.Text) & "'"
                _Qry &= vbCrLf & ",FTKidneyBUN=N'" & HI.UL.ULF.rpQuoted(FTKidneyBUN.Text) & "', "
                _Qry &= vbCrLf & "FTKidneyBUNMGDL=N'" & HI.UL.ULF.rpQuoted(FTKidneyBUNMGDL.Value.ToString) & "',FTKidneyCreatinine=N'" & HI.UL.ULF.rpQuoted(FTKidneyCreatinine.Text) & "',FTKidneyCreatinineMGDL=N'" & HI.UL.ULF.rpQuoted(FTKidneyCreatinineMGDL.Value.ToString) & "'"
                _Qry &= vbCrLf & ",FTFatHDL=N'" & HI.UL.ULF.rpQuoted(FTFatHDL.Text) & "',FTFatHDLMGDL=N'" & HI.UL.ULF.rpQuoted(FTFatHDLMGDL.Value.ToString) & "',FTFatCholesteral=N'" & HI.UL.ULF.rpQuoted(FTFatCholesteral.Text) & "'"
                _Qry &= vbCrLf & " ,FTFatCholesteralMGDL=N'" & HI.UL.ULF.rpQuoted(FTFatCholesteralMGDL.Value.ToString) & "',FTFatTriglyseride=N'" & HI.UL.ULF.rpQuoted(FTFatTriglyseride.Text) & "' , "
                _Qry &= vbCrLf & "FTFatTriglyserideMGDL=N'" & HI.UL.ULF.rpQuoted(FTFatTriglyserideMGDL.Value.ToString) & "',FTCellResults=N'" & HI.TL.CboList.GetListValue("FTCellResults", FTCellResults.SelectedIndex) & "',FCCellN=N'" & HI.UL.ULF.rpQuoted(FCCellN.Text) & "',FCCellL=" & FCCellL.Value & " ,FCCellE=" & FCCellE.Value & ",FCCellM=" & FCCellM.Value & ",FCCellHB=" & FCCellHB.Value & ",FCCellHCT=" & FCCellHCT.Value & ""
                _Qry &= vbCrLf & ",FCCellWBC=" & FCCellWBC.Value & ",FTRBCMorpholory=N'" & HI.UL.ULF.rpQuoted(FTRBCMorpholory.Text) & "',FTUrineResults=N'" & HI.TL.CboList.GetListValue("FTUrineResults", FTUrineResults.SelectedIndex) & "',FTUrineEPT=N'" & HI.UL.ULF.rpQuoted(FTUrineEPT.Text) & "', "
                _Qry &= vbCrLf & "FTUrineSugar=N'" & HI.UL.ULF.rpQuoted(FTUrineSugar.Text) & "',FTUrineRBC=N'" & HI.UL.ULF.rpQuoted(FTUrineRBC.Text) & "',FTUrineProtien=N'" & HI.UL.ULF.rpQuoted(FTUrineProtien.Text) & "',FTUrineWBC=N'" & HI.UL.ULF.rpQuoted(FTUrineWBC.Text) & "'"
                _Qry &= vbCrLf & " ,FTUrineOth=N'" & HI.UL.ULF.rpQuoted(FTUrineOth.Text) & "',FTOthCEA=N'" & HI.UL.ULF.rpQuoted(FTOthCEA.Text) & "',FTOthAFP=N'" & HI.UL.ULF.rpQuoted(FTOthAFP.Text) & "'"
                _Qry &= vbCrLf & ",FTOthCA125=N'" & HI.UL.ULF.rpQuoted(FTOthCA125.Text) & "',FTOthPSA=N'" & HI.UL.ULF.rpQuoted(FTOthPSA.Text) & "',FTOthAntiHIV=N'" & HI.UL.ULF.rpQuoted(FTOthAntiHIV.Text) & "',FTOthVDRL=N'" & HI.UL.ULF.rpQuoted(FTOthVDRL.Text) & "',FTHBVHGs=N'" & HI.UL.ULF.rpQuoted(FTHBVHGs.Text) & "' , "
                _Qry &= vbCrLf & " FTHBVAntiHGs=N'" & HI.UL.ULF.rpQuoted(FTHBVAntiHGs.Text) & "',FNHSysHospitalId=" & Val(FNHSysHospitalId.Properties.Tag.ToString) & ",FNHSysBldId=" & Val(FNHSysBldId.Properties.Tag.ToString) & " "
                _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & ",FCUrineSpGr='" & HI.UL.ULF.rpQuoted(Me.FCUrineSpGr.Text) & "'"
                _Qry &= vbCrLf & ",FCPlatelets=" & Me.FCPlatelets.Value & ", FTUrineColor='" & HI.UL.ULF.rpQuoted(Me.FTUrineColor.Text) & "' ,FCUrinepH=" & Me.FCUrinepH.Value & ",FTUrineAppearance='" & HI.UL.ULF.rpQuoted(Me.FTUrineAppearance.Text) & "'"
                _Qry &= vbCrLf & ",FTUrineGlucose='" & HI.UL.ULF.rpQuoted(Me.FTUrineGlucose.Text) & "' ,FTUrineBlood='" & HI.UL.ULF.rpQuoted(Me.FTUrineBlood.Text) & "'"
                _Qry &= vbCrLf & ",FTUrineEpi='" & HI.UL.ULF.rpQuoted(Me.FTUrineEpi.Text) & "' , FCChemisDTX=" & Me.FCChemisDTX.Value & " ,FCChemisUricAcid =" & Me.FCChemisUricAcid.Value & ""
                _Qry &= vbCrLf & ",FCChemisLDL=" & Me.FCChemisLDL.Value & " , FCChemisAlkaline=" & Me.FCChemisAlkaline.Value & ",FTSpirpmrtry='" & HI.TL.CboList.GetListValue("FTSpirpmrtry", Me.FTSpirpmrtry.SelectedIndex) & "'"
                _Qry &= vbCrLf & ",FCAudioRightEar=" & Me.FCAudioRightEar.Value & ",FTAudioRightResults='" & HI.TL.CboList.GetListValue("FTAudioRightResults", Me.FTAudioRightResults.SelectedIndex) & "',FCAudioLeftEar=" & Me.FCAudioLeftEar.Value
                _Qry &= vbCrLf & ",FTAudioLeftResults='" & HI.TL.CboList.GetListValue("FTAudioLeftResults", Me.FTAudioLeftResults.SelectedIndex) & "' , FTAudioResults='" & HI.TL.CboList.GetListValue("FTAudioResults", Me.FTAudioResults.SelectedIndex) & "'"
                _Qry &= vbCrLf & ",FTDoctorOpinion='" & HI.UL.ULF.rpQuoted(Me.FTDoctorOpinion.Text) & "'"
                _Qry &= vbCrLf & ",FTChemisResults='" & HI.TL.CboList.GetListValue("FTChemisResults", Me.FTChemisResults.SelectedIndex) & "'"

                _Qry &= vbCrLf & ",FTXRayNote='" & HI.UL.ULF.rpQuoted(Me.FTXRayNote.Text) & "'"
                _Qry &= vbCrLf & ",FTHDLResult='" & HI.TL.CboList.GetListValue("FTHDLResult", Me.FTHDLResult.SelectedIndex) & "'"
                _Qry &= vbCrLf & ",FTTrygleResult='" & HI.TL.CboList.GetListValue("FTTrygleResult", Me.FTTrygleResult.SelectedIndex) & "'"
                _Qry &= vbCrLf & ",FTChemisUricAcid='" & HI.UL.ULF.rpQuoted(Me.FTChemisUricAcid.Text) & "'"
                _Qry &= vbCrLf & ",FTChemisUricAcidResult='" & HI.TL.CboList.GetListValue("FTChemisUricAcidResult", Me.FTChemisUricAcidResult.SelectedIndex) & "'"
                _Qry &= vbCrLf & ",FTMethy='" & HI.UL.ULF.rpQuoted(Me.FTMethy.Text) & "'"
                _Qry &= vbCrLf & ",FTMethyResult='" & HI.TL.CboList.GetListValue("FTMethyResult", Me.FTMethyResult.SelectedIndex) & "'"
                _Qry &= vbCrLf & ",FTChemisLDL='" & HI.UL.ULF.rpQuoted(Me.FTChemisLDL.Text) & "'"
                _Qry &= vbCrLf & ",FTLDLResult='" & HI.TL.CboList.GetListValue("FTLDLResult", Me.FTLDLResult.SelectedIndex) & "'"
                _Qry &= vbCrLf & ",FTTubResult='" & HI.TL.CboList.GetListValue("FTTubResult", Me.FTTubResult.SelectedIndex) & "'"

                _Qry &= vbCrLf & ",FTTubNote='" & HI.UL.ULF.rpQuoted(Me.FTTubNote.Text) & "'"
                _Qry &= vbCrLf & ",FTTaiResult='" & HI.TL.CboList.GetListValue("FTTaiResult", Me.FTTaiResult.SelectedIndex) & "'"
                _Qry &= vbCrLf & ",FTTaiNote='" & HI.UL.ULF.rpQuoted(Me.FTTaiNote.Text) & "'"
                _Qry &= vbCrLf & ",FTFBSResult='" & HI.TL.CboList.GetListValue("FTFBSResult", Me.FTFBSResult.SelectedIndex) & "'"
                _Qry &= vbCrLf & ",FTFBSNote='" & HI.UL.ULF.rpQuoted(Me.FTFBSNote.Text) & "'"
                _Qry &= vbCrLf & ",FTEarNote='" & HI.UL.ULF.rpQuoted(Me.FTEarNote.Text) & "'"
                _Qry &= vbCrLf & ",FTUrineNote='" & HI.UL.ULF.rpQuoted(Me.FTUrineNote.Text) & "'"
                _Qry &= vbCrLf & ",FTHBsAgResult='" & HI.TL.CboList.GetListValue("FTHBsAgResult", Me.FTHBsAgResult.SelectedIndex) & "'"
                _Qry &= vbCrLf & ",FTHBsAgNote='" & HI.UL.ULF.rpQuoted(Me.FTHBsAgNote.Text) & "'"
                _Qry &= vbCrLf & ",FTLungNote='" & HI.UL.ULF.rpQuoted(Me.FTLungNote.Text) & "'"
                _Qry &= vbCrLf & ",FTOtherNote='" & HI.UL.ULF.rpQuoted(Me.FTOtherNote.Text) & "'"
                _Qry &= vbCrLf & ",FTBact='" & HI.UL.ULF.rpQuoted(Me.FTBact.Text) & "'"
                _Qry &= vbCrLf & ",FTMedGeneralResult='" & HI.TL.CboList.GetListValue("FTMedGeneralResult", Me.FTMedGeneralResult.SelectedIndex) & "'"
                '_Qry &= vbCrLf & ",FNMVC=" & Me.FNMCV.Value
                _Qry &= vbCrLf & ",FNMCH=" & Me.FNMCH.Value
                _Qry &= vbCrLf & ",FNMCHC=" & Me.FNMCHC.Value

                _Qry &= vbCrLf & ",FNFVC=" & Me.FNFVC.Value
                _Qry &= vbCrLf & ",FNFEV1=" & Me.FNFEV1.Value
                _Qry &= vbCrLf & ",FNFEVFVCPer=" & Me.FNFEVFVCPer.Value
                _Qry &= vbCrLf & ",FNPulse=" & Me.FNPulse.Value
                _Qry &= vbCrLf & ",FNMethyMg=" & Me.FNMethyMg.Value
                _Qry &= vbCrLf & ",FTEKGResult ='" & HI.UL.ULF.rpQuoted(Me.FTEKGResult.Text) & "'"

                _Qry &= vbCrLf & ",FTCellNote='" & HI.UL.ULF.rpQuoted(Me.FTCellNote.Text) & "'"
                _Qry &= vbCrLf & ",FTChemisNote='" & HI.UL.ULF.rpQuoted(Me.FTChemisNote.Text) & "'"
                _Qry &= vbCrLf & ",FTCholestNote='" & HI.UL.ULF.rpQuoted(Me.FTCholestNote.Text) & "'"

                _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(Me.EmpSysID) & " "
                _Qry &= vbCrLf & "  AND FNSeqNo=" & Val(Me.HealthSeq) & "  "

                Dim tSeqNo As Integer

                If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    _Qry = "SELECT MAX(FNSeqNo) AS FNSeqNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHealthHistory WHERE  FNHSysEmpID=" & Val(Me.EmpSysID) & " "

                    tSeqNo = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                    tSeqNo = Val(tSeqNo) + 1

                    _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHealthHistory(FNHSysEmpID, FNSeqNo, FDHealth, FTGenResults, FCGenWeight, FCGenHigh, FTPregnant, FCBloodPressure, FTCarouse, FTSmoke, FTCongenitalDisease, "
                    _Qry &= vbCrLf & "   FTResultsChest, FTNumberXRay, FTRemainSugar, FTFBS, FTUricAcid, FTLiverSGOT, FTLiverSGOTUL, FTLiverSGPT, FTLiverSGPTUL, FTKidneyBUN, "
                    _Qry &= vbCrLf & "  FTKidneyBUNMGDL, FTKidneyCreatinine, FTKidneyCreatinineMGDL, FTFatHDL, FTFatHDLMGDL, FTFatCholesteral, FTFatCholesteralMGDL, FTFatTriglyseride, "
                    _Qry &= vbCrLf & "  FTFatTriglyserideMGDL, FTCellResults, FCCellN, FCCellL, FCCellE, FCCellM, FCCellHB, FCCellHCT, FCCellWBC, FTRBCMorpholory, FTUrineResults, FTUrineEPT, "
                    _Qry &= vbCrLf & "  FTUrineSugar, FTUrineRBC, FTUrineProtien, FTUrineWBC, FTUrineOth, FTOthCEA, FTOthAFP, FTOthCA125, FTOthPSA, FTOthAntiHIV, FTOthVDRL, FTHBVHGs, "
                    _Qry &= vbCrLf & " FTHBVAntiHGs,FNHSysHospitalId,FNHSysBldId"
                    _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime"
                    _Qry &= vbCrLf & ",FCUrineSpGr,FCPlatelets,FTUrineColor,FCUrinepH,FTUrineAppearance,FTUrineGlucose,FTUrineBlood,FTUrineEpi,FCChemisDTX,FCChemisUricAcid"
                    _Qry &= vbCrLf & ",FCChemisLDL,FCChemisAlkaline,FTSpirpmrtry,FCAudioRightEar,FTAudioRightResults,FCAudioLeftEar,FTAudioLeftResults,FTAudioResults"
                    _Qry &= vbCrLf & ",FTDoctorOpinion,FTChemisResults  "

                    _Qry &= vbCrLf & " , FTXRayNote,FTHDLResult,FTTrygleResult ,FTChemisUricAcid,FTChemisUricAcidResult,FTMethy,FTMethyResult,FTChemisLDL , FTLDLResult,FTTubResult "
                    _Qry &= vbCrLf & ",FTTubNote,FTTaiResult,FTTaiNote,FTFBSResult,FTFBSNote,FTEarNote,FTUrineNote,FTHBsAgResult,FTHBsAgNote , FTLungNote ,FTOtherNote ,FTBact,FTMedGeneralResult"
                    _Qry &= vbCrLf & ",FNMCV,FNMCH,FNMCHC , FNFVC ,FNFEV1,FNFEVFVCPer,FNPulse,FNMethyMg,FTEKGResult ,FTCellNote,FTChemisNote,FTCholestNote"

                    _Qry &= vbCrLf & ")  "
                    _Qry &= vbCrLf & " SELECT " & Val(Me.EmpSysID) & "," & tSeqNo & ""
                    _Qry &= vbCrLf & " ,N'" & HI.UL.ULDate.ConvertEnDB(FDHealth.Text) & "',N'" & HI.UL.ULF.rpQuoted(FTGenResults.Text) & "'"
                    _Qry &= vbCrLf & " ," & FCGenWeight.Value & "," & FCGenHigh.Value & ",N'" & HI.TL.CboList.GetListValue("FTPregnant", FTPregnant.SelectedIndex) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FCBloodPressure.Text) & "',N'" & HI.TL.CboList.GetListValue("FTCarouse", FTCarouse.SelectedIndex) & "'"
                    _Qry &= vbCrLf & " ,N'" & Integer.Parse(HI.TL.CboList.GetIndexByText("FTSmoke", FTSmoke.Text)) & "',N'" & HI.UL.ULF.rpQuoted(FTCongenitalDisease.Text) & "', "
                    _Qry &= vbCrLf & "N'" & Integer.Parse(HI.TL.CboList.GetIndexByText("FTResultsChest", FTResultsChest.Text)) & "',N'" & HI.UL.ULF.rpQuoted(FTNumberXRay.Text) & "' "
                    _Qry &= vbCrLf & "  ,N'" & HI.UL.ULF.rpQuoted(FTRemainSugar.Text) & "',N'" & HI.UL.ULF.rpQuoted(FTFBS.Text) & "'"
                    _Qry &= vbCrLf & " , N'" & HI.UL.ULF.rpQuoted(FTUricAcid.Text) & "', '" & HI.UL.ULF.rpQuoted(FTLiverSGOT.Text) & "'"
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTLiverSGOTUL.Text) & "',N'" & HI.UL.ULF.rpQuoted(FTLiverSGPT.Text) & "'"
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTLiverSGPTUL.Value.ToString) & "',N'" & HI.UL.ULF.rpQuoted(FTKidneyBUN.Text) & "', "
                    _Qry &= vbCrLf & "N'" & HI.UL.ULF.rpQuoted(FTKidneyBUNMGDL.Value.ToString) & "',N'" & HI.UL.ULF.rpQuoted(FTKidneyCreatinine.Text) & "',N'" & HI.UL.ULF.rpQuoted(FTKidneyCreatinineMGDL.Value.ToString) & "'"
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTFatHDL.Text) & "',N'" & HI.UL.ULF.rpQuoted(FTFatHDLMGDL.Value.ToString) & "',N'" & HI.UL.ULF.rpQuoted(FTFatCholesteral.Text) & "'"
                    _Qry &= vbCrLf & " ,N'" & HI.UL.ULF.rpQuoted(FTFatCholesteralMGDL.Value.ToString) & "',N'" & HI.UL.ULF.rpQuoted(FTFatTriglyseride.Text) & "' , "
                    _Qry &= vbCrLf & "N'" & HI.UL.ULF.rpQuoted(FTFatTriglyserideMGDL.Value.ToString) & "',N'" & HI.TL.CboList.GetListValue("FTCellResults", FTCellResults.SelectedIndex) & "',N'" & HI.UL.ULF.rpQuoted(FCCellN.Text) & "'," & FCCellL.Value & " ," & FCCellE.Value & "," & FCCellM.Value & "," & FCCellHB.Value & "," & FCCellHCT.Value & ""
                    _Qry &= vbCrLf & "," & FCCellWBC.Value & ",N'" & HI.UL.ULF.rpQuoted(FTRBCMorpholory.Text) & "',N'" & Integer.Parse(HI.TL.CboList.GetIndexByText("FTUrineResults", FTUrineResults.Text)) & "',N'" & HI.UL.ULF.rpQuoted(FTUrineEPT.Text) & "', "
                    _Qry &= vbCrLf & "N'" & HI.UL.ULF.rpQuoted(FTUrineSugar.Text) & "',N'" & HI.UL.ULF.rpQuoted(FTUrineRBC.Text) & "',N'" & HI.UL.ULF.rpQuoted(FTUrineProtien.Text) & "',N'" & HI.UL.ULF.rpQuoted(FTUrineWBC.Text) & "'"
                    _Qry &= vbCrLf & " ,N'" & HI.UL.ULF.rpQuoted(FTUrineOth.Text) & "',N'" & HI.UL.ULF.rpQuoted(FTOthCEA.Text) & "',N'" & HI.UL.ULF.rpQuoted(FTOthAFP.Text) & "'"
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTOthCA125.Text) & "',N'" & HI.UL.ULF.rpQuoted(FTOthPSA.Text) & "',N'" & HI.UL.ULF.rpQuoted(FTOthAntiHIV.Text) & "',N'" & HI.UL.ULF.rpQuoted(FTOthVDRL.Text) & "',N'" & HI.UL.ULF.rpQuoted(FTHBVHGs.Text) & "' , "
                    _Qry &= vbCrLf & " N'" & HI.UL.ULF.rpQuoted(FTHBVAntiHGs.Text) & "'," & Val(FNHSysHospitalId.Properties.Tag.ToString) & "," & Val(FNHSysBldId.Properties.Tag.ToString) & " "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & "  ,'" & HI.UL.ULF.rpQuoted(Me.FCUrineSpGr.Text) & "'"
                    _Qry &= vbCrLf & "," & Me.FCPlatelets.Value & ",'" & HI.UL.ULF.rpQuoted(Me.FTUrineColor.Text) & "' ," & Me.FCUrinepH.Value & ",'" & HI.UL.ULF.rpQuoted(Me.FTUrineAppearance.Text) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTUrineGlucose.Text) & "' ,'" & HI.UL.ULF.rpQuoted(Me.FTUrineBlood.Text) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTUrineEpi.Text) & "' ," & Me.FCChemisDTX.Value & " ," & Me.FCChemisUricAcid.Value & ""
                    _Qry &= vbCrLf & "," & Me.FCChemisLDL.Value & " , " & Me.FCChemisAlkaline.Value & ",'" & Integer.Parse(HI.TL.CboList.GetIndexByText("FTSpirpmrtry", Me.FTSpirpmrtry.Text)) & "'"
                    _Qry &= vbCrLf & "," & Me.FCAudioRightEar.Value & ",'" & HI.TL.CboList.GetListValue("FTAudioRightResults", Me.FTAudioRightResults.SelectedIndex) & "'," & Me.FCAudioLeftEar.Value
                    _Qry &= vbCrLf & ",'" & HI.TL.CboList.GetListValue("FTAudioLeftResults", Me.FTAudioLeftResults.SelectedIndex) & "' , '" & Integer.Parse(HI.TL.CboList.GetIndexByText("FTAudioResults", Me.FTAudioResults.Text)) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTDoctorOpinion.Text) & "'"
                    _Qry &= vbCrLf & ",'" & HI.TL.CboList.GetListValue("FTChemisResults", Me.FTChemisResults.SelectedIndex) & "'"

                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTXRayNote.Text) & "'"
                    _Qry &= vbCrLf & ",'" & HI.TL.CboList.GetListValue("FTHDLResult", Me.FTHDLResult.SelectedIndex) & "'"
                    _Qry &= vbCrLf & ",'" & Integer.Parse(HI.TL.CboList.GetIndexByText("FTTrygleResult", Me.FTTrygleResult.Text)) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTChemisUricAcid.Text) & "'"
                    _Qry &= vbCrLf & ",'" & HI.TL.CboList.GetListValue("FTChemisUricAcidResult", Me.FTChemisUricAcidResult.SelectedIndex) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTMethy.Text) & "'"
                    _Qry &= vbCrLf & ",'" & HI.TL.CboList.GetListValue("FTMethyResult", Me.FTMethyResult.SelectedIndex) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTChemisLDL.Text) & "'"
                    _Qry &= vbCrLf & ",'" & HI.TL.CboList.GetListValue("FTLDLResult", Me.FTLDLResult.SelectedIndex) & "'"
                    _Qry &= vbCrLf & ",'" & HI.TL.CboList.GetListValue("FTTubResult", Me.FTTubResult.SelectedIndex) & "'"


                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTTubNote.Text) & "'"
                    _Qry &= vbCrLf & ",'" & HI.TL.CboList.GetListValue("FTTaiResult", Me.FTTaiResult.SelectedIndex) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTTaiNote.Text) & "'"
                    _Qry &= vbCrLf & ",'" & HI.TL.CboList.GetListValue("FTFBSResult", Me.FTFBSResult.SelectedIndex) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTFBSNote.Text) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTEarNote.Text) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTUrineNote.Text) & "'"
                    _Qry &= vbCrLf & ",'" & HI.TL.CboList.GetListValue("FTHBsAgResult", Me.FTHBsAgResult.SelectedIndex) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTHBsAgNote.Text) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTLungNote.Text) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTOtherNote.Text) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTBact.Text) & "'"
                    _Qry &= vbCrLf & ",'" & Integer.Parse(HI.TL.CboList.GetIndexByText("FTMedGeneralResult", Me.FTMedGeneralResult.Text)) & "'"
                    _Qry &= vbCrLf & "," & Me.FNMCV.Value
                    _Qry &= vbCrLf & "," & Me.FNMCH.Value
                    _Qry &= vbCrLf & "," & Me.FNMCH.Value

                    _Qry &= vbCrLf & "," & Me.FNFVC.Value
                    _Qry &= vbCrLf & "," & Me.FNFEV1.Value
                    _Qry &= vbCrLf & "," & Me.FNFEVFVCPer.Value
                    _Qry &= vbCrLf & "," & Me.FNPulse.Value
                    _Qry &= vbCrLf & "," & Me.FNMethyMg.Value
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTEKGResult.Text) & "'"

                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTCellNote.Text) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTChemisNote.Text) & "'"
                    _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTCholestNote.Text) & "'"


                    If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If


                _Qry = "UPDATE THRTEmployeeHealthHistory SET FNSeqNo=FNNo"
                _Qry &= vbCrLf & " FROM THRTEmployeeHealthHistory INNER JOIN "
                _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FDInsDate, FTInsTime) AS FNNo, FNSeqNo,FNHSysEmpID"
                _Qry &= vbCrLf & " FROM THRTEmployeeHealthHistory"
                _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(Me.EmpSysID) & ""
                _Qry &= vbCrLf & ") T1 ON THRTEmployeeHealthHistory.FNSeqNo=T1.FNSeqNo "
                _Qry &= vbCrLf & " AND THRTEmployeeHealthHistory.FNHSysEmpID=T1.FNHSysEmpID "
                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                _Qry = "UPDATE E SET FCWeight=THRTEmployeeHealthHistory.FCGenWeight,FCHeight=THRTEmployeeHealthHistory.FCGenHigh "
                _Qry &= vbCrLf & " FROM THRMEmployee AS E INNER JOIN "
                _Qry &= vbCrLf & "  THRTEmployeeHealthHistory ON  E.FNHSysEmpID=THRTEmployeeHealthHistory.FNHSysEmpID  INNER JOIN "
                _Qry &= vbCrLf & "(SELECT TOP 1 FNSeqNo,FNHSysEmpID"
                _Qry &= vbCrLf & " FROM THRTEmployeeHealthHistory"
                _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(Me.EmpSysID) & " ORDER BY FNSeqNo DESC"
                _Qry &= vbCrLf & ") T1 ON THRTEmployeeHealthHistory.FNSeqNo=T1.FNSeqNo "
                _Qry &= vbCrLf & " AND THRTEmployeeHealthHistory.FNHSysEmpID=T1.FNHSysEmpID "
                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                Dim _oDt As DataTable = CType(Me.ogchisory.DataSource, DataTable)
                Dim Seq As Integer = 0
                For Each R As DataRow In _oDt.Select("FTVaccDate<>'' and FTVaccLocation <> ''", "FNVaccSeq")
                    Seq += +1
                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeVaccinationHistory"
                    _Qry &= vbCrLf & "Set FTUpdUser='" & HI.ST.UserInfo.UserName & "'"
                    _Qry &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & ",FTVaccDate='" & HI.UL.ULDate.ConvertEnDB(R!FTVaccDate.ToString) & "'"
                    _Qry &= vbCrLf & ",FTVaccLocation='" & HI.UL.ULF.rpQuoted(R!FTVaccLocation.ToString) & "'"
                    _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Val(Me.EmpSysID)
                    _Qry &= vbCrLf & "and FNVaccSeq=" & Integer.Parse(Seq)
                    If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeVaccinationHistory"
                        _Qry &= "( FTInsUser, FDInsDate, FTInsTime, FNHSysEmpID, FNVaccSeq, FTVaccDate, FTVaccLocation)"
                        _Qry &= vbCrLf & "Select '" & HI.ST.UserInfo.UserName & "'"
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & "," & Val(Me.EmpSysID)
                        _Qry &= vbCrLf & "," & Integer.Parse(Seq)
                        _Qry &= vbCrLf & ",'" & HI.UL.ULDate.ConvertEnDB(R!FTVaccDate.ToString) & "'"
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTVaccLocation.ToString) & "'"
                        If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        End If
                    End If
                Next
                _Qry = "Delete From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeVaccinationHistory"
                _Qry &= vbCrLf & "WHERE FNHSysEmpID=" & Val(Me.EmpSysID)
                _Qry &= vbCrLf & "and FNVaccSeq >" & Integer.Parse(Seq)
                If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                End If
                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return True
            Catch ex As Exception
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End Try
            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub ocmexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.ProcComplete = False
        FTRiskFactorOne.Text = ""
        FTRiskFactorTwo.Text = ""
        FTRiskFactorThree.Text = ""
        FTRiskFactorFour.Text = ""
        FTRiskFactorFive.Text = ""
        FTRiskFactorSix.Text = ""
        FTRiskFactorSeven.Text = ""
        Me.Close()
    End Sub

    Private Sub LoadDataEdit()

        Dim _StrWhere As String = ""
        Dim _value As String = ""
        Dim _Qry As String = ""

        _Qry = " SELECT   TOP 1 H.FDHealth, H.FTGenResults, H.FCGenWeight, H.FCGenHigh, H.FTPregnant, H.FCBloodPressure, H.FTCarouse, H.FTSmoke, H.FTCongenitalDisease, "
        _Qry &= vbCrLf & " H.FTResultsChest, H.FTNumberXRay, H.FTRemainSugar, H.FTFBS, H.FTUricAcid, H.FTLiverSGOT, H.FTLiverSGOTUL, H.FTLiverSGPT, H.FTLiverSGPTUL, "
        _Qry &= vbCrLf & "    H.FTKidneyBUN, H.FTKidneyBUNMGDL, H.FTKidneyCreatinine, H.FTKidneyCreatinineMGDL, H.FTFatHDL, H.FTFatHDLMGDL, H.FTFatCholesteral, "
        _Qry &= vbCrLf & "    H.FTFatCholesteralMGDL, H.FTFatTriglyseride, H.FTFatTriglyserideMGDL, H.FTCellResults, H.FCCellN, H.FCCellL, H.FCCellE, H.FCCellM, H.FCCellHB, H.FCCellHCT, "
        _Qry &= vbCrLf & "    H.FCCellWBC, H.FTRBCMorpholory, H.FTUrineResults, H.FTUrineEPT, H.FTUrineSugar, H.FTUrineRBC, H.FTUrineProtien, H.FTUrineWBC, H.FTUrineOth, "
        _Qry &= vbCrLf & "    H.FTOthCEA, H.FTOthAFP, H.FTOthCA125, H.FTOthPSA, H.FTOthAntiHIV, H.FTOthVDRL, H.FTHBVHGs, H.FTHBVAntiHGs, P.FTHospitalCode AS FNHSysHospitalId, "
        _Qry &= vbCrLf & "    B.FTBldCode AS FNHSysBldId"

        _Qry &= vbCrLf & " ,H.FCPlatelets, H.FTUrineColor, H.FCUrinepH, H.FTUrineAppearance, H.FCUrineSpGr, H.FTUrineGlucose, H.FTUrineBlood "
        _Qry &= vbCrLf & " ,H.FTUrineEpi, H.FCChemisDTX, H.FCChemisUricAcid, H.FCChemisLDL, H.FCChemisAlkaline, H.FTSpirpmrtry, H.FCAudioRightEar"
        _Qry &= vbCrLf & ", H.FTAudioLeftResults, H.FTAudioResults , H.FTDoctorOpinion, H.FTAudioRightResults, H.FCAudioLeftEar,H.FTChemisResults"

        _Qry &= vbCrLf & ", H.FTXRayNote, H.FTHDLResult, H.FTChoResult, H.FTTrygleResult, H.FTChemisUricAcid, H.FTChemisUricAcidResult"
        _Qry &= vbCrLf & ", H.FTMethy, H.FTMethyResult, H.FTChemisLDL, H.FTLDLResult, H.FTTubResult, H.FTTubNote"
        _Qry &= vbCrLf & ", H.FTTaiResult, H.FTTaiNote, H.FTFBSResult, H.FTFBSNote, H.FTEarNote, H.FTUrineNote, H.FTHBsAgResult, H.FTHBsAgNote"
        _Qry &= vbCrLf & ", H.FTLungNote, H.FTOtherNote ,H.FTBact,H.FTMedGeneralResult ,H.FNPulse , H.FNFVC , H.FNFEV1 , H.FNFEVFVCPer  , H.FNPulse , H.FNMethyMg , H.FTEKGResult ,H.FTCellNote,H.FTChemisNote,H.FTCholestNote"

        _Qry &= vbCrLf & ",C.FTStateFirstCheck, C.FTStateCheckJobs, C.FTStateAnnualCheck, C.FTStateSurveillance, C.FTDoctorName,C. FTProfessionalNo,"
        _Qry &= vbCrLf & "C.FTDoctorAddr, C.FTDoctorTel,C.FTStateNormal, C.FTStateNotNormal,C. FTNotNarmalNote,C. FTLaboratoryNote, C.FTRiskFactorOne,C. FTResultOne, C.FTRiskFactorTwo, C.FTResultTwo, C.FTRiskFactorThree, C.FTResultThree, C.FTRiskFactorFour,"
        _Qry &= vbCrLf & " C. FTResultFour, C.FTRiskFactorFive, C.FTResultFive, C.FTRiskFactorSix,C. FTResultSix, C.FTRiskFactorSeven, C.FTResultSeven"

        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHealthHistory AS H WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHospital AS P WITH (NOLOCK) ON H.FNHSysHospitalId = P.FNHSysHospitalId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMBlood AS B WITH (NOLOCK) ON H.FNHSysBldId = B.FNHSysBldId  LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_HealthCheck AS C WITH (NOLOCK) ON  H.FNHSysEmpID=C.FNHSysEmpID AND H.FNSeqNo=C.FNSeqNo"
        _Qry &= vbCrLf & "  WHERE  (H.FNHSysEmpID =" & Val(Me.EmpSysID) & ")"
        _Qry &= vbCrLf & "  AND (H.FNSeqNo =" & Val(Me.HealthSeq) & ")"

        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        Dim _FieldName As String = ""
        For Each R As DataRow In _dt.Rows
            For Each Col As DataColumn In _dt.Columns
                _FieldName = Col.ColumnName.ToString

                For Each Obj As Object In Me.Controls.Find(_FieldName, True)
                    Select Case Obj.GetType.FullName.ToString.ToUpper
                        Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                                .Text = R.Item(Col).ToString
                            End With
                        Case "DevExpress.XtraEditors.CalcEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.CalcEdit)
                                .Value = Val(R.Item(Col).ToString)
                            End With
                        Case "DevExpress.XtraEditors.ComboBoxEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.ComboBoxEdit)
                                Try
                                    If "" & .Properties.Tag.ToString <> "" Then
                                        .SelectedIndex = HI.TL.CboList.GetIndexByValue("" & .Properties.Tag.ToString, R.Item(Col).ToString)
                                    Else
                                        .SelectedIndex = Val(R.Item(Col).ToString)
                                    End If
                                Catch ex As Exception
                                    .SelectedIndex = -1
                                End Try
                            End With
                        Case "DevExpress.XtraEditors.CheckEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.CheckEdit)
                                .EditValue = (Integer.Parse(Val(R.Item(Col).ToString))).ToString
                            End With
                        Case "DevExpress.XtraEditors.MemoEdit".ToUpper, "DevExpress.XtraEditors.TextEdit".ToUpper
                            Obj.Text = R.Item(Col).ToString
                        Case "DevExpress.XtraEditors.PictureEdit".ToUpper
                            With CType(Obj, DevExpress.XtraEditors.PictureEdit)
                                Try
                                    .Image = HI.UL.ULImage.LoadImage("" & .Properties.Tag.ToString & R.Item(Col).ToString)
                                Catch ex As Exception
                                    .Image = Nothing
                                End Try
                            End With
                        Case "DevExpress.XtraEditors.DateEdit".ToUpper
                            Try
                                With CType(Obj, DevExpress.XtraEditors.DateEdit)
                                    If .Properties.DisplayFormat.FormatString = "dd/MM/yyyy" Or .Properties.DisplayFormat.FormatString = "d" Then
                                        .DateTime = CDate(HI.UL.ULDate.ConvertEnDB(R.Item(Col).ToString))
                                    Else
                                        .Text = R.Item(Col).ToString
                                    End If
                                End With
                            Catch ex As Exception
                            End Try
                        Case Else
                            Obj.Text = R.Item(Col).ToString
                    End Select
                Next
            Next

            Exit For
        Next
        Call LoadVaccHistory()
        'FTRiskFactorOne.Text = ""
        'FTRiskFactorTwo.Text = ""
        'FTRiskFactorThree.Text = ""
        'FTRiskFactorFour.Text = ""
        'FTRiskFactorFive.Text = ""
        'FTRiskFactorSix.Text = ""
        'FTRiskFactorSeven.Text = ""
        Me.ocmexit.Focus()
    End Sub

    Private Sub LoadVaccHistory()
        Try
            Dim _Qry As String = ""
            Dim _oDt As DataTable
            _Qry = "Select FNVaccSeq , CASE WHEN Isdate(FTVaccDate) = 1 Then convert(varchar(10),convert(datetime,FTVaccDate),103) Else '' End AS FTVaccDate , FTVaccLocation"
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeVaccinationHistory WITH(NOLOCK) "
            _Qry &= vbCrLf & "  WHERE  (FNHSysEmpID =" & Val(Me.EmpSysID) & ")"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            If _oDt.Rows.Count = 0 Then
                With _oDt
                    .Rows.Add()
                    .AcceptChanges()
                    For Each R As DataRow In .Rows
                        R!FNVaccSeq = 1
                    Next
                End With
            End If
            Me.ogchisory.DataSource = _oDt
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Handler"
    Private Shared Sub ItemDate_Leave(sender As Object, e As System.EventArgs)
        Try
            With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

                Dim _TDate As String
                Try
                    _TDate = HI.UL.ULDate.ConvertEnDB(CType(sender, DevExpress.XtraEditors.DateEdit).DateTime)
                    If _TDate = "0001/01/01" Then
                        _TDate = ""
                    End If
                Catch ex As Exception
                    _TDate = ""
                End Try
                CType(sender, DevExpress.XtraEditors.DateEdit).Text = _TDate
                Try
                    CType(sender, DevExpress.XtraEditors.DateEdit).DateTime = _TDate
                Catch ex As Exception
                End Try
                .SetRowCellValue(.FocusedRowHandle, .FocusedColumn.FieldName.ToString, HI.UL.ULDate.ConvertEN(_TDate))

            End With
        Catch ex As Exception
        End Try
    End Sub
#End Region


    Private Sub FNGenBMI_EditValueChanged(sender As Object, e As EventArgs) Handles FCGenBMI.EditValueChanged
        Try
            Select Case True
                Case (Me.FCGenBMI.Value > 0.01 And Me.FCGenBMI.Value < 18.49)
                    Me.FCBMIType.SelectedIndex = 1
                Case (Me.FCGenBMI.Value >= 18.5 And Me.FCGenBMI.Value < 22.99)
                    Me.FCBMIType.SelectedIndex = 2
                Case (Me.FCGenBMI.Value >= 23 And Me.FCGenBMI.Value < 24.99)
                    Me.FCBMIType.SelectedIndex = 3
                Case (Me.FCGenBMI.Value >= 25 And Me.FCGenBMI.Value < 29.99)
                    Me.FCBMIType.SelectedIndex = 4
                Case (Me.FCGenBMI.Value >= 30)
                    Me.FCBMIType.SelectedIndex = 5
                Case Else
                    Me.FCBMIType.SelectedIndex = 0
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FCGenHigh_EditValueChanged(sender As Object, e As EventArgs) Handles FCGenHigh.EditValueChanged
        Try
            If Me.FCGenHigh.Value > 0 And Me.FCGenWeight.Value > 0 Then
                Me.FCGenBMI.Value = Me.FCGenWeight.Value / ((Me.FCGenHigh.Value / 100) * (Me.FCGenHigh.Value / 100))
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvhistory_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles ogvhistory.KeyDown
        Try
            Select Case e.KeyCode
                Case Keys.Enter, Keys.Down
                    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                        If .FocusedColumn.FieldName.ToString <> "FTVaccLocation" Then
                            Exit Sub
                        End If
                        Dim x As Integer = 0
                        If .GetRowCellValue(.FocusedRowHandle, "FTVaccDate").ToString <> "" Or .GetRowCellValue(.FocusedRowHandle, "FTVaccLocation").ToString <> "" Then
                            With CType((CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).GridControl).DataSource, DataTable)
                                .AcceptChanges()
                                If .Select("FTVaccDate='' or FTVaccDate Is null").Length <= 0 Then
                                    x = .Rows.Count + 1
                                    .Rows.Add(x)
                                    .Rows(x - 1).Item("FNVaccSeq") = x
                                End If
                                .AcceptChanges()
                            End With
                            .FocusedRowHandle = x
                            .FocusedColumn = .Columns.ColumnByFieldName("FTVaccDate")
                        End If
                    End With
                Case Keys.Delete
                    With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
                        .DeleteRow(.FocusedRowHandle)
                        With CType(ogchisory.DataSource, DataTable)
                            .AcceptChanges()
                            Dim x As Integer = 0
                            For Each r As DataRow In .Select("FNVaccSeq<>0", "FNVaccSeq")
                                x += +1
                                r!FNGrpSeq = x
                            Next
                            .AcceptChanges()
                        End With
                    End With
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Function SaveHealth() As Boolean
        Dim _Qry As String = ""
        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try

                _Qry = "UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_HealthCheck SET "
                _Qry &= vbCrLf & "FTStateFirstCheck=N'" & FTStateFirstCheck.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",FTStateCheckJobs=N'" & FTStateCheckJobs.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",FTStateAnnualCheck=N'" & FTStateAnnualCheck.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",FTStateSurveillance=N'" & FTStateSurveillance.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",FTDoctorName=N'" & HI.UL.ULF.rpQuoted(FTDoctorName.Text) & "' "
                _Qry &= vbCrLf & ",FTProfessionalNo=N'" & HI.UL.ULF.rpQuoted(FTProfessionalNo.Text) & "' "
                _Qry &= vbCrLf & ",FTDoctorAddr=N'" & HI.UL.ULF.rpQuoted(FTDoctorAddr.Text) & "' "
                _Qry &= vbCrLf & ",FTDoctorTel=N'" & HI.UL.ULF.rpQuoted(FTDoctorTel.Text) & "' "
                _Qry &= vbCrLf & ",FTStateNormal=N'" & FTStateNormal.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",FTStateNotNormal=N'" & FTStateNotNormal.EditValue.ToString & "' "
                _Qry &= vbCrLf & ",FTNotNarmalNote=N'" & HI.UL.ULF.rpQuoted(FTNotNarmalNote.Text) & "' "
                _Qry &= vbCrLf & ",FTLaboratoryNote=N'" & HI.UL.ULF.rpQuoted(FTLaboratoryNote.Text) & "' "
                _Qry &= vbCrLf & ",FTRiskFactorOne=N'" & HI.UL.ULF.rpQuoted(FTRiskFactorOne.Text) & "' "
                _Qry &= vbCrLf & ",FTResultOne=N'" & HI.UL.ULF.rpQuoted(FTResultOne.Text) & "' "
                _Qry &= vbCrLf & ",FTRiskFactorTwo=N'" & HI.UL.ULF.rpQuoted(FTRiskFactorTwo.Text) & "' "
                _Qry &= vbCrLf & ",FTResultTwo=N'" & HI.UL.ULF.rpQuoted(FTResultTwo.Text) & "' "
                _Qry &= vbCrLf & ",FTRiskFactorThree=N'" & HI.UL.ULF.rpQuoted(FTRiskFactorThree.Text) & "' "
                _Qry &= vbCrLf & ",FTResultThree=N'" & HI.UL.ULF.rpQuoted(FTResultThree.Text) & "' "
                _Qry &= vbCrLf & ",FTRiskFactorFour=N'" & HI.UL.ULF.rpQuoted(FTRiskFactorFour.Text) & "' "
                _Qry &= vbCrLf & ",FTResultFour=N'" & HI.UL.ULF.rpQuoted(FTResultFour.Text) & "' "
                _Qry &= vbCrLf & ",FTRiskFactorFive=N'" & HI.UL.ULF.rpQuoted(FTRiskFactorFive.Text) & "' "
                _Qry &= vbCrLf & ",FTResultFive=N'" & HI.UL.ULF.rpQuoted(FTResultFive.Text) & "' "
                _Qry &= vbCrLf & ",FTRiskFactorSix=N'" & HI.UL.ULF.rpQuoted(FTRiskFactorSix.Text) & "' "
                _Qry &= vbCrLf & ",FTResultSix=N'" & HI.UL.ULF.rpQuoted(FTResultSix.Text) & "' "
                _Qry &= vbCrLf & ",FTRiskFactorSeven=N'" & HI.UL.ULF.rpQuoted(FTRiskFactorSeven.Text) & "' "
                _Qry &= vbCrLf & ",FTResultSeven=N'" & HI.UL.ULF.rpQuoted(FTResultSeven.Text) & "' "
                _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(Me.EmpSysID) & " "
                _Qry &= vbCrLf & "  AND FNSeqNo=" & Val(Me.HealthSeq) & "  "



                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then
                    _Qry = "SELECT MAX(FNSeqNo) AS FNSeqNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_HealthCheck WHERE  FNHSysEmpID=" & Val(Me.EmpSysID) & " "

                    Dim tSeqNo As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
                    tSeqNo = Val(tSeqNo) + 1

                    _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_HealthCheck( "
                    _Qry &= vbCrLf & " FNHSysEmpID, FNSeqNo, FTStateFirstCheck, FTStateCheckJobs, FTStateAnnualCheck, FTStateSurveillance, FTDoctorName, "
                    _Qry &= vbCrLf & "  FTProfessionalNo, FTDoctorAddr, FTDoctorTel, FTStateNormal, FTStateNotNormal, FTNotNarmalNote, FTLaboratoryNote, FTRiskFactorOne, FTResultOne, FTRiskFactorTwo, FTResultTwo, FTRiskFactorThree, "
                    _Qry &= vbCrLf & "FTResultThree, FTRiskFactorFour, FTResultFour, FTRiskFactorFive, FTResultFive, FTRiskFactorSix, FTResultSix, FTRiskFactorSeven, FTResultSeven, FTInsUser, FDInsDate, FTInsTime)"
                    _Qry &= vbCrLf & " SELECT " & Val(Me.EmpSysID) & "," & tSeqNo & ""
                    _Qry &= vbCrLf & ",N'" & FTStateFirstCheck.EditValue.ToString & "' "
                    _Qry &= vbCrLf & ",N'" & FTStateCheckJobs.EditValue.ToString & "' "
                    _Qry &= vbCrLf & ",N'" & FTStateAnnualCheck.EditValue.ToString & "' "
                    _Qry &= vbCrLf & ",N'" & FTStateSurveillance.EditValue.ToString & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTDoctorName.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTProfessionalNo.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTDoctorAddr.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTDoctorTel.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & FTStateNormal.EditValue.ToString & "' "
                    _Qry &= vbCrLf & ",N'" & FTStateNotNormal.EditValue.ToString & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTNotNarmalNote.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTLaboratoryNote.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTRiskFactorOne.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTResultOne.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTRiskFactorTwo.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTResultTwo.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTRiskFactorThree.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTResultThree.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTRiskFactorFour.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTResultFour.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTRiskFactorFive.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTResultFive.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTRiskFactorSix.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTResultSix.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTRiskFactorSeven.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(FTResultSeven.Text) & "' "
                    _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB



                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                End If


                _Qry = "UPDATE THRMEmployee_HealthCheck SET FNSeqNo=FNNo"
                _Qry &= vbCrLf & " FROM THRMEmployee_HealthCheck INNER JOIN "
                _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FDInsDate, FTInsTime) AS FNNo, FNSeqNo,FNHSysEmpID"
                _Qry &= vbCrLf & " FROM THRMEmployee_HealthCheck"
                _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(Me.EmpSysID) & ""
                _Qry &= vbCrLf & ") T1 ON THRMEmployee_HealthCheck.FNSeqNo=T1.FNSeqNo "
                _Qry &= vbCrLf & " AND THRMEmployee_HealthCheck.FNHSysEmpID=T1.FNHSysEmpID "
                HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


            Catch ex As Exception
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End Try
            Return True
            FTRiskFactorOne.Text = ""
            FTRiskFactorTwo.Text = ""
            FTRiskFactorThree.Text = ""
            FTRiskFactorFour.Text = ""
            FTRiskFactorFive.Text = ""
            FTRiskFactorSix.Text = ""
            FTRiskFactorSeven.Text = ""

        Catch ex As Exception
            Return False
        End Try



    End Function

End Class