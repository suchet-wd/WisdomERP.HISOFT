
Imports DevExpress.XtraBars
Imports System.IO
Imports System.Reflection
Imports System.Globalization
Imports System.Threading
Imports HI.ST
Imports DevExpress.XtraNavBar
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Drawing.Text
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils

Public Class WisdomService

    Private StateUserLogOff As Boolean
    Private _ProcLoad As Boolean = False
    Private _SysPathImageSystem As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images\System"

    Private _AllFuncName As String
    Private _AppSystemPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private _PathFileDll As String
    Private _Splash As HI.TL.SplashScreen
    Private _TimcCheckUserLogin As Integer = 60
    Private _TimcCountCheckUserLogin As Integer = 0
    Private _StateCheckPOApp As Boolean = False
    Private _StateCheckPOAppDirector As Boolean = False
    Private _StateCheckManagerFactory As Boolean = False
    Private _StateCheckWHAppCm As Boolean = False
    Private _StateCheckPOPDF As Boolean = False
    Private _StateCheckMailUser As Boolean = False
    Private _StateCheckUser As Boolean = False
    Private _StateCheckMerAppTVW As Boolean = False
    Private _StateCheckinvoicecharge As Boolean = False
    Private _StateCheckSewingLineLeader As Boolean = False
    Private _StateCheckQAFinalLeader As Boolean = False
    Private _StateUserDC As Boolean = False
    Private _StateUserAppPR As Boolean = False
    Private _ContextStripUserPicture As System.Windows.Forms.ContextMenuStrip
    Private _StateChekOrderCost As Boolean = False
    Private _StateFormLoadSucess As Boolean = False
    Private _StateEmpleaveApp As Boolean = False
    Private _StateCheckPOAppAsset As Boolean = False
    Private _StateCheckPRAppAsset As Boolean = False
    Private _StateCheckPOAppAssetDirector As Boolean = False
    Private _StateCheckPRAppAssetDirector As Boolean = False
    Private _StateCheckSafetyPRAppAsset As Boolean = False
    Private _StateCheckAPPSMP As Boolean = False
    Private _StateCheckAPPSMPMGR As Boolean = False
    Private _StateCheckAPPRDSam As Boolean = False

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Try
            _StateCheckPOAppDirector = True
            _StateCheckManagerFactory = True
            _StateCheckWHAppCm = True
            _StateCheckMerAppTVW = True
            _StateCheckinvoicecharge = True
            _StateCheckPOApp = True
            _StateCheckPOPDF = True
            _StateCheckMailUser = True
            _StateCheckSewingLineLeader = True
            _StateCheckQAFinalLeader = True
            _StateUserDC = True
            _StateUserAppPR = True
            _StateChekOrderCost = True
            _StateEmpleaveApp = True
            _StateCheckPOApp = True
            _StateCheckPOPDF = True
            _StateCheckPOAppDirector = True
            _StateCheckManagerFactory = True
            _StateCheckMerAppTVW = True
            _StateCheckinvoicecharge = True
            _StateFormLoadSucess = True


            ocmcheckapp.Enabled = HI.ST.SysInfo.StateManager
            otmcheckmerapptvw.Enabled = HI.ST.SysInfo.StateSuperVisorMer
            ocmcheckappdirector.Enabled = HI.ST.SysInfo.StateDirector
            ocmcheckmanagerfactoryapp.Enabled = HI.ST.SysInfo.StateFactoryManager
            ocmcheckwhappcm.Enabled = HI.ST.SysInfo.StateWHAppCM
            otmqafinalleader.Enabled = HI.ST.SysInfo.StateUserQAFinalLeader
            otmsewinglineleader.Enabled = HI.ST.SysInfo.StateUserSewingLineLeader
            Me.otmdctimer.Enabled = HI.ST.SysInfo.StateUserDCControl
            Me.otmcheckappr.Enabled = HI.ST.SysInfo.StateUserAppPR
            Me.otmChkOrderCostApp.Enabled = HI.ST.SysInfo.StateOrderCostApp
            Me.otmChkEmpLeaveApp.Enabled = HI.ST.SysInfo.StateEmpLeaveApp
            Me.otmqastyleriskcritical.Enabled = HI.ST.SysInfo.StyleRiskCritical



            _StateCheckPOAppAsset = False ' HI.ST.SysInfo.StateSuperVisorAssetPO
            _StateCheckPRAppAsset = HI.ST.SysInfo.StateSuperVisorPRAsset
            _StateCheckPOAppAssetDirector = HI.ST.SysInfo.StateDirectorAssetPO
            _StateCheckPRAppAssetDirector = HI.ST.SysInfo.StateDirectorAssetPR
            _StateCheckSafetyPRAppAsset = HI.ST.SysInfo.StateSafetyPRAsset

            Me.otmcheckAssetPR.Enabled = HI.ST.SysInfo.StateSuperVisorPRAsset
            Me.otmDirectorAssetPO.Enabled = HI.ST.SysInfo.StateDirectorAssetPO
            Me.otmDirectorAssetPR.Enabled = HI.ST.SysInfo.StateDirectorAssetPR
            Me.otmAssetPo.Enabled = False ' HI.ST.SysInfo.StateSuperVisorAssetPO
            Me.otmcheckSafetyPR.Enabled = HI.ST.SysInfo.StateSafetyPRAsset


            _StateCheckAPPSMP = HI.ST.SysInfo.StateAppSMP
            Me.ocmcheckappsmp.Enabled = HI.ST.SysInfo.StateAppSMP


            _StateCheckAPPSMPMGR = HI.ST.SysInfo.StateAppSMPMGR
            Me.ocmcheckappsmpmgr.Enabled = HI.ST.SysInfo.StateAppSMPMGR



            _StateCheckAPPRDSam = HI.ST.SysInfo.StateAppRDSam
            otmcheckapprdsam.Enabled = HI.ST.SysInfo.StateAppRDSam


            otmcheckinvoicecharge.Interval = (60000 * HI.ST.SysInfo.StateUserInvoiceChargeTimeMin)
            otmcheckinvoicecharge.Enabled = ((HI.ST.SysInfo.StateUserInvoiceCharge) And (HI.ST.SysInfo.StateUserInvoiceChargeDay > 0))

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        If ocmcheckapp.Enabled = False And otmcheckmerapptvw.Enabled = False And ocmcheckappdirector.Enabled = False And ocmcheckmanagerfactoryapp.Enabled = False And
             ocmcheckwhappcm.Enabled = False And otmqafinalleader.Enabled = False And otmsewinglineleader.Enabled = False And otmdctimer.Enabled = False And otmcheckappr.Enabled = False And
              otmDirectorAssetPO.Enabled = False And otmDirectorAssetPR.Enabled = False And otmAssetPo.Enabled = False And Me.ocmcheckappsmp.Enabled = False And Me.ocmcheckappsmpmgr.Enabled = False And Me.otmChkEmpLeaveApp.Enabled = False And Me.otmcheckSafetyPR.Enabled = False Then
            Application.Exit()

        End If


    End Sub

    Private Sub WisdomService_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label1.Text = HI.ST.UserInfo.UserName
        Me.Hide()
    End Sub


    Private Sub ocmcheckapp_Tick(sender As Object, e As EventArgs) Handles ocmcheckapp.Tick

        If (_StateCheckPOApp) And (HI.ST.SysInfo.StateManager = True) Then
            _StateCheckPOApp = False
            Dim _Theard As New Thread(AddressOf CheckPOApp)
            _Theard.Start()
        End If

    End Sub


    Private Sub ocmcheckappdirector_Tick(sender As Object, e As EventArgs) Handles ocmcheckappdirector.Tick
        If (_StateCheckPOAppDirector) And (HI.ST.SysInfo.StateDirector = True) Then
            _StateCheckPOAppDirector = False
            Dim _Theard As New Thread(AddressOf CheckPOAppDirector)
            _Theard.Start()
        End If
    End Sub


#Region "User"

    Private Delegate Sub DelegateCheckUserLogin(ByVal _Username As String)
    Private Sub CheckUserLogin(ByVal _Username As String)
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckUserLogin(AddressOf CheckUserLogin), New Object() {_Username})
        Else
            If _Username <> "" Then
                If Not (HI.ST.SysInfo.Admin) Then
                    Dim _Str As String = ""
                    Dim _Dt As DataTable

                    _Str = "SELECT   TOP 1  FTUserName, FTLogInIP, FTLogInDate, FTLogInTime, FTLogInCom"
                    _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginState WITH(NOLOCK) "
                    _Str &= vbCrLf & " WHERE  FTUserName='" & HI.UL.ULF.rpQuoted(_Username) & "'  "


                    _Dt = HI.Conn.SQLConn.GetDataTable(_Str, HI.Conn.DB.DataBaseName.DB_SECURITY)

                    If _Dt.Rows.Count > 0 Then
                        Dim _msg As String = "This User are Connected"
                        _msg &= vbCrLf & "By IP : " & _Dt.Rows(0)!FTLogInIP.ToString
                        _msg &= vbCrLf & "By Computername : " & _Dt.Rows(0)!FTLogInCom.ToString
                        _msg &= vbCrLf & " "
                        _msg &= vbCrLf & " Users will be removed to Connect. "

                        If _Dt.Rows(0)!FTLogInIP.ToString <> HI.ST.UserInfo.UserLogInComputerIP Then

                            MsgBox(_msg)
                            StateUserLogOff = True
                            Application.Exit()

                        End If

                    Else

                        MsgBox("Not Foud Stasus Login To System !!!")
                        StateUserLogOff = True
                        Application.Exit()

                    End If

                    _Dt.Dispose()

                End If
            End If

            _StateCheckUser = True
        End If
        _TimcCountCheckUserLogin = 0
    End Sub

#End Region

#Region "PO"

    Private Delegate Sub DelegateCheckPOApp()
    Private Sub CheckPOApp()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckPOApp(AddressOf CheckPOApp), New Object() {})
        Else

            HI.Service.ClsService.ValidateApp(HI.ST.SysInfo.AppActScreen)
            _StateCheckPOApp = True
        End If
    End Sub

    Private Delegate Sub DelegateCheckPOAppDirector()
    Private Sub CheckPOAppDirector()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckPOAppDirector(AddressOf CheckPOAppDirector), New Object() {})
        Else

            HI.Service.ClsService.ValidateAppDirector(HI.ST.SysInfo.AppActScreen)
            _StateCheckPOAppDirector = True
        End If
    End Sub

    Private Delegate Sub DelegateCheckManagerFactoryApprove()
    Private Sub CheckManagerFactoryApp()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckManagerFactoryApprove(AddressOf CheckManagerFactoryApp), New Object() {})
        Else

            HI.Service.ClsService.ValidateAppFactoryManager(HI.ST.SysInfo.AppActScreen)
            _StateCheckManagerFactory = True
        End If
    End Sub

    Private Delegate Sub DelegateCheckWHAppCM()
    Private Sub CheckWHAppCM()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckWHAppCM(AddressOf CheckWHAppCM), New Object() {})
        Else
            HI.Service.ClsService.ValidateAppWHCM(HI.ST.SysInfo.AppActScreen)
            _StateCheckWHAppCm = True
        End If
    End Sub


    Private Delegate Sub DelegateCheckMerAppTVW()
    Private Sub CheckMerAppTVW()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckMerAppTVW(AddressOf CheckMerAppTVW), New Object() {})
        Else
            HI.Service.ClsService.ValidateMerManagerApp(HI.ST.SysInfo.AppActScreen)
            _StateCheckMerAppTVW = True
        End If
    End Sub

    Private Delegate Sub DelegateCheckSewingLineLeaderApp()
    Private Sub CheckSewingLineLeaderApp()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckSewingLineLeaderApp(AddressOf CheckSewingLineLeaderApp), New Object() {})
        Else
            HI.Service.ClsService.ValidateLineLeaderApp(HI.ST.SysInfo.AppActScreen)
            _StateCheckSewingLineLeader = True
        End If
    End Sub

    Private Delegate Sub DelegateCheckQAFinalLeaderApp()
    Private Sub CheckQAFinalLeaderApp()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckQAFinalLeaderApp(AddressOf CheckQAFinalLeaderApp), New Object() {})
        Else
            HI.Service.ClsService.ValidateQAFinalLeaderApp(HI.ST.SysInfo.AppActScreen)
            _StateCheckQAFinalLeader = True
        End If
    End Sub

    Private Delegate Sub DelegateCheckUserDocumentControl()
    Private Sub CheckUserDocumentControl()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckUserDocumentControl(AddressOf CheckUserDocumentControl), New Object() {})
        Else
            HI.Service.ClsService.ValidateAppDocumentation(HI.ST.SysInfo.AppActScreen)
            _StateUserDC = True
        End If
    End Sub


    Private Delegate Sub DelegateCheckUserAppPRDataInfoo()
    Private Sub CheckUserAppPRDataInfo()

        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckUserAppPRDataInfoo(AddressOf CheckUserAppPRDataInfo), New Object() {})
        Else
            HI.Service.ClsService.ValidateAppPR(HI.ST.SysInfo.AppActScreen)
            _StateUserAppPR = True
        End If

    End Sub

    Private Delegate Sub DelegateCheckInvoiceCharge()
    Private Sub CheckCheckInvoiceCharge()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckInvoiceCharge(AddressOf CheckCheckInvoiceCharge), New Object() {})
        Else
            Dim _Lang As String = "TH"

            If HI.ST.Lang.Language = Lang.eLang.TH Then
                _Lang = "TH"
            Else
                _Lang = "EN"
            End If

            Dim _dtCharge As DataTable
            Dim _Str As String = "Exec [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.SP_GETINVOICECHARGE_ALERT " & HI.ST.SysInfo.StateUserInvoiceChargeDay & ",'" & HI.UL.ULF.rpQuoted(_Lang) & "' "

            _dtCharge = HI.Conn.SQLConn.GetDataTable(_Str, HI.Conn.DB.DataBaseName.DB_ACCOUNT)

            If _dtCharge.Rows.Count > 0 Then

                Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                Dim _WInvoiceChargeAlert As New wListInvoiceChargeAlert

                HI.ST.SysInfo.MenuName = "mnuSecurity"

                Dim oSysLang As New HI.ST.SysLanguage
                Try
                    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _WInvoiceChargeAlert.Name.ToString.Trim, _WInvoiceChargeAlert)
                Catch ex As Exception
                Finally
                End Try

                With _WInvoiceChargeAlert
                    .DataPO = _dtCharge.Copy
                    .RefreshDataPO()
                    .ShowDialog()
                End With

                HI.ST.SysInfo.MenuName = _TmpMenu

            End If

            _dtCharge.Dispose()

            _StateCheckinvoicecharge = True
        End If
    End Sub


    Private Delegate Sub DelegeteCheckOrderCost()
    Private Sub CheckOrderCost()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegeteCheckOrderCost(AddressOf CheckOrderCost), New Object() {})
        Else
            HI.Service.ClsService.ValidateAppOrderCost(HI.ST.SysInfo.AppActScreen)
            _StateChekOrderCost = True
        End If
    End Sub

#End Region

    Private Sub ocmcheckmanagerfactoryapp_Tick(sender As Object, e As EventArgs) Handles ocmcheckmanagerfactoryapp.Tick
        If (_StateCheckManagerFactory) And (HI.ST.SysInfo.StateFactoryManager = True) Then
            _StateCheckManagerFactory = False

            Dim _Theard As New Thread(AddressOf CheckManagerFactoryApp)
            _Theard.Start()

        End If
    End Sub

    Private Sub ocmcheckwhappcm_Tick(sender As Object, e As EventArgs) Handles ocmcheckwhappcm.Tick
        If (_StateCheckWHAppCm) And (HI.ST.SysInfo.StateWHAppCM = True) Then
            _StateCheckWHAppCm = False

            Dim _Theard As New Thread(AddressOf CheckWHAppCM)
            _Theard.Start()

        End If
    End Sub

    Private Sub otmcheckmerapptvw_Tick(sender As Object, e As EventArgs) Handles otmcheckmerapptvw.Tick
        If (_StateCheckMerAppTVW) And (HI.ST.SysInfo.StateSuperVisorMer = True) Then
            _StateCheckMerAppTVW = False

            Dim _Theard As New Thread(AddressOf CheckMerAppTVW)
            _Theard.Start()

        End If
    End Sub

    Private Sub otmcheckinvoicecharge_Tick(sender As Object, e As EventArgs) Handles otmcheckinvoicecharge.Tick
        If (_StateCheckinvoicecharge) And (HI.ST.SysInfo.StateUserInvoiceCharge = True) And HI.ST.SysInfo.StateUserInvoiceChargeDay > 0 Then
            _StateCheckinvoicecharge = False
            Dim _Theard As New Thread(AddressOf CheckCheckInvoiceCharge)
            _Theard.Start()
        End If
    End Sub

    Private Sub otmsewinglineleader_Tick(sender As Object, e As EventArgs) Handles otmsewinglineleader.Tick
        If (_StateCheckSewingLineLeader) And (HI.ST.SysInfo.StateUserSewingLineLeader = True) Then
            _StateCheckSewingLineLeader = False
            Dim _Theard As New Thread(AddressOf CheckSewingLineLeaderApp)
            _Theard.Start()
        End If
    End Sub

    Private Sub otmqafinalleader_Tick(sender As Object, e As EventArgs) Handles otmqafinalleader.Tick
        If (_StateCheckQAFinalLeader) And (HI.ST.SysInfo.StateUserQAFinalLeader = True) Then
            _StateCheckQAFinalLeader = False
            Dim _Theard As New Thread(AddressOf CheckQAFinalLeaderApp)
            _Theard.Start()
        End If
    End Sub

    Private Sub otmdctimer_Tick(sender As Object, e As EventArgs) Handles otmdctimer.Tick
        If (_StateUserDC) And (HI.ST.SysInfo.StateUserDCControl = True) Then
            _StateUserDC = False
            Dim _Theard As New Thread(AddressOf CheckUserDocumentControl)
            _Theard.Start()
        End If
    End Sub

    Private Sub otmcheckappr_Tick(sender As Object, e As EventArgs) Handles otmcheckappr.Tick
        If (_StateUserAppPR) And (HI.ST.SysInfo.StateUserAppPR = True) Then
            _StateUserAppPR = False
            Dim _Theard As New Thread(AddressOf CheckUserAppPRDataInfo)
            _Theard.Start()
        End If
    End Sub


    Private Sub otmChkOrderCostApp_Tick(sender As Object, e As EventArgs) Handles otmChkOrderCostApp.Tick
        If (_StateChekOrderCost) And (HI.ST.SysInfo.StateOrderCostApp = True) Then
            _StateChekOrderCost = False

            Dim _Theard As New Thread(AddressOf CheckOrderCost)
            _Theard.Start()

        End If
    End Sub


    Private Sub otmChkEmpLeaveApp_Tick(sender As Object, e As EventArgs) Handles otmChkEmpLeaveApp.Tick
        If (_StateEmpleaveApp) And (HI.ST.SysInfo.StateEmpLeaveApp = True) Then
            _StateEmpleaveApp = False

            Dim _Theard As New Thread(AddressOf CheckEmpLeaveApp)
            _Theard.Start()

        End If
    End Sub

    Private Delegate Sub DelegateCheckEmpLeaveApp()
    Private Sub CheckEmpLeaveApp()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckEmpLeaveApp(AddressOf CheckEmpLeaveApp), New Object() {})
        Else

            Try
                HI.Service.ClsService.ValidateAppEmpleave(HI.ST.SysInfo.AppActScreen)
            Catch ex As Exception

            End Try

            _StateEmpleaveApp = True
        End If
    End Sub

    Private Sub otmqastyleriskcritical_Tick(sender As Object, e As EventArgs) Handles otmqastyleriskcritical.Tick
        otmqastyleriskcritical.Enabled = False

        Dim _Theard As New Thread(AddressOf CheckQAStyleRisk)
        _Theard.Start()

    End Sub


    Private Delegate Sub DelegateCheckQAStyleRisk()
    Private Sub CheckQAStyleRisk()
        If Me.InvokeRequired Then

            Me.Invoke(New DelegateCheckQAStyleRisk(AddressOf CheckQAStyleRisk), New Object() {})

        Else

            Try
                Dim cmdstring As String = ""
                cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_QACRITICAL "
                Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, HI.Conn.DB.DataBaseName.DB_PROD)

                If dt.Rows.Count > 0 Then
                    dt.Dispose()

                    Dim QARiskStyleCritical As New HI.Service.StyleRiskCritical
                    QARiskStyleCritical.ShowDialog()

                Else
                    dt.Dispose()
                End If


            Catch ex As Exception

            End Try
            otmqastyleriskcritical.Enabled = True
        End If
    End Sub

    Private Sub otmDirectorAssetPR_Tick(sender As Object, e As EventArgs) Handles otmDirectorAssetPR.Tick
        If (_StateCheckPRAppAssetDirector) And (HI.ST.SysInfo.StateDirectorAssetPR = True) Then
            _StateCheckPRAppAssetDirector = False
            Dim _Theard As New Thread(AddressOf CheckPRAppAssetDirector)
            _Theard.Start()
        End If

    End Sub

    Private Sub otmDirectorAssetPO_Tick(sender As Object, e As EventArgs) Handles otmDirectorAssetPO.Tick
        If (_StateCheckPOAppAssetDirector) And (HI.ST.SysInfo.StateDirectorAssetPO = True) Then
            _StateCheckPOAppAssetDirector = False
            Dim _Theard As New Thread(AddressOf CheckPOAppAssetDirector)
            _Theard.Start()
        End If

    End Sub

    Private Sub otmAssetPo_Tick(sender As Object, e As EventArgs) Handles otmAssetPo.Tick
        If (_StateCheckPOAppAsset) And (HI.ST.SysInfo.StateSuperVisorAssetPO = True) Then
            _StateCheckPOAppAsset = False

            Dim _Theard As New Thread(AddressOf CheckPOAppAsset)
            _Theard.Start()

        End If
    End Sub

    Private Sub otmAssetPR_Tick(sender As Object, e As EventArgs) Handles otmcheckAssetPR.Tick
        If (_StateCheckPRAppAsset) And (HI.ST.SysInfo.StateSuperVisorPRAsset = True) Then
            _StateCheckPRAppAsset = False

            Dim _Theard As New Thread(AddressOf CheckPRAppAsset)
            _Theard.Start()

        End If
    End Sub


    Private Delegate Sub DelegateCheckPRAppAsset()
    Private Sub CheckPRAppAsset()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckPRAppAsset(AddressOf CheckPRAppAsset), New Object() {})
        Else

            Try
                HI.Service.ClsService.ValidateAppAssetPR(HI.ST.SysInfo.AppActScreen)

            Catch ex As Exception
            End Try
            _StateCheckPRAppAsset = True

        End If
    End Sub

    Private Delegate Sub DelegateCheckPRAppAssetDirector()
    Private Sub CheckPRAppAssetDirector()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckPRAppAssetDirector(AddressOf CheckPRAppAssetDirector), New Object() {})
        Else

            Try
                HI.Service.ClsService.ValidateAppAssetPRDirector(HI.ST.SysInfo.AppActScreen)

            Catch ex As Exception

            End Try
            _StateCheckPOAppDirector = True
        End If
    End Sub


    Private Delegate Sub DelegateCheckPOAppAsset()
    Private Sub CheckPOAppAsset()
        'If Me.InvokeRequired Then
        '    Me.Invoke(New DelegateCheckPOAppAsset(AddressOf CheckPOAppAsset), New Object() {})
        'Else

        '    HI.Service.ClsService.ValidateAppAssetPO(HI.ST.SysInfo.AppActScreen)
        '    _StateCheckPOAppAsset = True
        'End If
    End Sub

    Private Delegate Sub DelegateCheckPOAppAssetDirector()
    Private Sub CheckPOAppAssetDirector()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckPOAppAssetDirector(AddressOf CheckPOAppAssetDirector), New Object() {})
        Else

            Try
                HI.Service.ClsService.ValidateAppAssetPODirector(HI.ST.SysInfo.AppActScreen)

            Catch ex As Exception

            End Try
            _StateCheckPOAppDirector = True
        End If
    End Sub

    Private Sub ocmcheckappsmp_Tick(sender As Object, e As EventArgs) Handles ocmcheckappsmp.Tick
        If (_StateCheckAPPSMP) And (HI.ST.SysInfo.StateAppSMP = True) Then
            _StateCheckAPPSMP = False

            Dim _Theard As New Thread(AddressOf CheckPOAppSMP)
            _Theard.Start()

        End If
    End Sub


    Private Delegate Sub DelegateCheckPOAppSMP()
    Private Sub CheckPOAppSMP()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckPOAppSMP(AddressOf CheckPOAppSMP), New Object() {})
        Else
            Try
                HI.Service.ClsService.ValidateAppSMP(HI.ST.SysInfo.AppActScreen)

            Catch ex As Exception

            End Try
            _StateCheckAPPSMP = True
        End If
    End Sub

    Private Sub ocmcheckappsmpmgr_Tick(sender As Object, e As EventArgs) Handles ocmcheckappsmpmgr.Tick
        If (_StateCheckAPPSMPMGR) And (HI.ST.SysInfo.StateAppSMPMGR = True) Then
            _StateCheckAPPSMPMGR = False
            ocmcheckappsmpmgr.Enabled = False
            Dim _Theard As New Thread(AddressOf CheckPOAppSMPMGR)
            _Theard.Start()

        End If
    End Sub



    Private Delegate Sub DelegateCheckPOAppSMPMGR()
    Private Sub CheckPOAppSMPMGR()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckPOAppSMPMGR(AddressOf CheckPOAppSMPMGR), New Object() {})
        Else

            Try
                HI.Service.ClsService.ValidateAppSMPMGR(HI.ST.SysInfo.AppActScreen)

            Catch ex As Exception

            End Try
            _StateCheckAPPSMPMGR = True
            ocmcheckappsmpmgr.Enabled = True
        End If
    End Sub

    Private Sub otmcheckapprdsam_Tick(sender As Object, e As EventArgs) Handles otmcheckapprdsam.Tick
        If (_StateCheckAPPRDSam) And (HI.ST.SysInfo.StateAppRDSam = True) Then
            _StateCheckAPPRDSam = False
            otmcheckapprdsam.Enabled = False
            Dim _Theard As New Thread(AddressOf CheckAppRDSam)
            _Theard.Start()

        End If
    End Sub


    Private Delegate Sub DelegateCheckAppRDSam()
    Private Sub CheckAppRDSam()

        If Me.InvokeRequired Then

            Me.Invoke(New DelegateCheckAppRDSam(AddressOf CheckAppRDSam), New Object() {})

        Else

            Try

                HI.Service.ClsService.ValidateAppRDSam(HI.ST.SysInfo.AppActScreen)

            Catch ex As Exception
            End Try

            _StateCheckAPPRDSam = True
            otmcheckapprdsam.Enabled = True

        End If

    End Sub

    Private Sub otmcheckSafetyPR_Tick(sender As Object, e As EventArgs) Handles otmcheckSafetyPR.Tick
        If (_StateCheckSafetyPRAppAsset) And (HI.ST.SysInfo.StateSafetyPRAsset = True) Then
            _StateCheckSafetyPRAppAsset = False

            Dim _Theard As New Thread(AddressOf CheckSafetyPRAppAsset)
            _Theard.Start()

        End If
    End Sub
    Private Delegate Sub DelegateCheckPRSafetyAppAsset()
    Private Sub CheckSafetyPRAppAsset()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckPRSafetyAppAsset(AddressOf CheckSafetyPRAppAsset), New Object() {})
        Else


            HI.Service.ClsService.ValidateAppAssetPRSafety(HI.ST.SysInfo.AppActScreen)
            _StateCheckSafetyPRAppAsset = True
        End If
    End Sub
End Class
