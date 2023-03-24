Imports System.Management
Imports System.Windows.Forms
Imports System.Timers

Friend Class Start
    Public Declare Function GetVolumeSerialNumber Lib "kernel32" Alias "GetVolumeInformationA" (ByVal lpRootPathName As String, ByVal lpVolumeNameBuffer As Long, ByVal nVolumeNameSize As Long, ByVal lpVolumeSerialNumber As Long, ByVal lpMaximumComponentLength As Long, ByVal lpFileSystemFlags As Long, ByVal lpFileSystemNameBuffer As Long, ByVal nFileSystemNameSize As Long) As Long
    Private Shared t As System.Timers.Timer

    Private Sub New()
    End Sub

    <STAThread()> _
    Shared Sub Main()

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

        DevExpress.UserSkins.BonusSkins.Register()
        DevExpress.Skins.SkinManager.EnableFormSkins()
        Application.EnableVisualStyles()
        DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("McSkin")
        Application.SetCompatibleTextRenderingDefault(False)

        '----------- Read Connecttion String From File XML
        HI.Conn.DB.GetXmlConnectionString()

        'Call ReadPathAppName()
        HI.ST.Lang.Language = HI.ST.Lang.eLang.TH
        Try
            Dim ProcessAppName As Process() = Process.GetProcessesByName(_AppName)
            Dim ProcessAppName2 As Process() = Process.GetProcessesByName(_AppName & ".exe")

            If ProcessAppName.Length >= 1 Or ProcessAppName2.Length >= 1 Then
                End
            End If

        Catch ex As Exception
        End Try

        If FindComputerName(Environment.MachineName.ToString()) Then
            If CheckPurchaseApp() OrElse CheckFactoryApp() OrElse CheckApproveDocument() OrElse LoadOrderCostApprove() OrElse LoadLeaveWaitApp() Then

                Try
                    Dim _Str As String = "SELECT * FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEConfig WITH(NOLOCK)"
                    Dim _dt As DataTable
                    _dt = HI.Conn.SQLConn.GetDataTable(_Str, HI.Conn.DB.DataBaseName.DB_SECURITY)

                    For Each R As DataRow In _dt.Rows
                        HI.ST.SysInfo.CmpDefualtCode = R!FTCmpCode.ToString
                        HI.ST.SysInfo.CmpCode = R!FTCmpCode.ToString
                        HI.ST.Config.StateNotShowEmpLeave = (R!FTStateNotShowEmpLeave.ToString = "1")
                        Exit For
                    Next
                Catch ex As Exception
                End Try

                Try
                    'Application.Run(New wDirectorApproved)
                    Application.Run(New wDirectorApproved)
                Catch ex As Exception
                End Try

            Else
                End
            End If
        Else
            End
        End If

    End Sub

    Private Shared _PathAppName As String = ""
    Private Shared _AppName As String = ""
    Private Shared _AppServiceName As String = ""

    Public Shared Sub ReadPathAppName()

        ' While _PathAppName = ""
        Try
            For I As Integer = 1 To 900000000

            Next
            Dim _Qry As String = "Select Top 3  FTCfgName,FTCfgData  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS A WITH(NOLOCK) WHERE FTCfgName in ('AppPath','AppName','AppSevice') "
            Dim dt As DataTable

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, HI.Conn.DB.DataBaseName.DB_SECURITY)

            For Each R As DataRow In dt.Rows

                Select Case R!FTCfgName.ToString.Trim.ToUpper
                    Case "AppPath".ToUpper
                        _PathAppName = R!FTCfgData.ToString
                    Case "AppName".ToUpper
                        _AppName = R!FTCfgData.ToString
                    Case "AppSevice".ToUpper
                        _AppServiceName = R!FTCfgData.ToString
                End Select

            Next

        Catch ex As Exception
        End Try
        ' End While


    End Sub

    Private Shared _StateShow As Boolean = False
    Private Shared Sub TimerFired(ByVal sender As Object, ByVal e As ElapsedEventArgs)

        Try

            Dim ProcessName As Process() = Process.GetProcessesByName(_AppServiceName)
            Dim ProcessAppName As Process() = Process.GetProcessesByName(_AppName)

            If _StateShow Or ProcessAppName.Length >= 1 Then
            Else

                If FindComputerName(Environment.MachineName.ToString()) Then
                    If CheckPurchaseApp() OrElse CheckFactoryApp() OrElse CheckApproveDocument() OrElse LoadOrderCostApprove() Then
                        _StateShow = True
                        Try
                            'With New wDirectorApproved
                            '    .ShowDialog()
                            '    _StateShow = False
                            'End With

                            With New wDirectorApproved
                                .ShowDialog()
                                _StateShow = False
                            End With
                        Catch ex As Exception
                            _StateShow = False
                            MsgBox(ex.Message())
                        End Try
                    Else
                    End If
                Else
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Public Shared Function CheckPurchaseApp() As Boolean
        Try

            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            _str = ""
            _str = "SELECT * FROM ("
            _str &= Environment.NewLine & " SELECT  TOP 1  A.FTPurchaseNo  "
            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.V_TPURTPurchase AS A INNER JOIN "
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin as B	ON a.FTSuperVisorName = b.FTUserName LEFT OUTER JOIN"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp as C ON B.FNHSysTeamGrpId = C.FNHSysTeamGrpId LEFT JOIN"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDelivery as L1 with (nolock) ON a.FNHSysDeliveryId=L1.FNHSysDeliveryId  left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmpRun as L2 with (nolock)  on a.FNHSysCmpRunId=L2.FNHSysCmpRunId Left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMSupplier as L3 with (nolock) on a.FNHSysSuplId = L3.FNHSysSuplId Left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCreditTerm as L4 with (nolock) on A.FNHSysCrTermId = L4.FNHSysCrTermId  left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMPaymentTerm as L5 with (nolock) on a.FNHSysTermOfPMId = L5.FNHSysTermOfPMId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDelivery as L6 with (nolock) on a.FNHSysDeliveryId = L6.FNHSysDeliveryId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency as L7 with (nolock)  on a.FNHSysCurId = L7.FNHSysCurId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMTeamGrp as L8 with (nolock) on b.FNHSysTeamGrpId = L8.FNHSysTeamGrpId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TPURMPURGrp as L9 with (nolock) on a.FNHSysPurGrpId = L9.FNHSysPurGrpId "


            _str &= Environment.NewLine & "   INNER JOIN (SELECT U.FTUserName, T.FNHSysDirectorGrpId"
            _str &= Environment.NewLine & " FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U WITH(NOLOCK) INNER JOIN"
            _str &= Environment.NewLine & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp AS T WITH(NOLOCK) ON U.FNHSysTeamGrpId = T.FNHSysTeamGrpId"
            _str &= Environment.NewLine & " WHERE  T.FNHSysDirectorGrpId IN (SELECT DISTINCT DG.FNHSysDirectorGrpId"
            _str &= Environment.NewLine & "	FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDirectorGrpUser AS DGU WITH(NOLOCK)  INNER JOIN"
            _str &= Environment.NewLine & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDirectorGrp AS DG  WITH(NOLOCK) ON DGU.FNHSysDirectorGrpId = DG.FNHSysDirectorGrpId"
            _str &= Environment.NewLine & "	WHERE  (DGU.FTUserName = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'))"
            _str &= Environment.NewLine & " ) AS MU ON a.FTPurchaseBy = MU.FTUserName"

            _str &= Environment.NewLine & " WHERE (a.FTStateSendApp = '1') AND (a.FTStateSuperVisorApp = '1') AND (a.FTStateManagerApp ='0') AND A.FTPoTypeState <>'3' "


            _str &= Environment.NewLine & " UNION "

            _str &= Environment.NewLine & " SELECT  TOP 1  A.FTPurchaseNo  "
            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.V_TPURTPurchase AS A INNER JOIN "
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin as B	ON a.FTSuperVisorName = b.FTUserName LEFT OUTER JOIN"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp as C ON B.FNHSysTeamGrpId = C.FNHSysTeamGrpId LEFT JOIN"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDelivery as L1 with (nolock) ON a.FNHSysDeliveryId=L1.FNHSysDeliveryId  left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmpRun as L2 with (nolock)  on a.FNHSysCmpRunId=L2.FNHSysCmpRunId Left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMSupplier as L3 with (nolock) on a.FNHSysSuplId = L3.FNHSysSuplId Left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCreditTerm as L4 with (nolock) on A.FNHSysCrTermId = L4.FNHSysCrTermId  left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMPaymentTerm as L5 with (nolock) on a.FNHSysTermOfPMId = L5.FNHSysTermOfPMId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDelivery as L6 with (nolock) on a.FNHSysDeliveryId = L6.FNHSysDeliveryId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency as L7 with (nolock)  on a.FNHSysCurId = L7.FNHSysCurId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMTeamGrp as L8 with (nolock) on b.FNHSysTeamGrpId = L8.FNHSysTeamGrpId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TPURMPURGrp as L9 with (nolock) on a.FNHSysPurGrpId = L9.FNHSysPurGrpId "


            _str &= Environment.NewLine & "   INNER JOIN (SELECT U.FTUserName, T.FNHSysDirectorGrpId"
            _str &= Environment.NewLine & " FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U WITH(NOLOCK) INNER JOIN"
            _str &= Environment.NewLine & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp AS T WITH(NOLOCK) ON U.FNHSysTeamGrpId = T.FNHSysTeamGrpId"
            _str &= Environment.NewLine & " WHERE  T.FNHSysDirectorGrpIdTo IN (SELECT DISTINCT DG.FNHSysDirectorGrpId"
            _str &= Environment.NewLine & "	FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDirectorGrpUser AS DGU WITH(NOLOCK)  INNER JOIN"
            _str &= Environment.NewLine & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDirectorGrp AS DG  WITH(NOLOCK) ON DGU.FNHSysDirectorGrpId = DG.FNHSysDirectorGrpId"
            _str &= Environment.NewLine & "	WHERE  (DGU.FTUserName = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'))"
            _str &= Environment.NewLine & " ) AS MU ON a.FTPurchaseBy = MU.FTUserName"

            _str &= Environment.NewLine & " WHERE (a.FTStateSendApp = '1') AND (a.FTStateSuperVisorApp = '1') AND (a.FTStateManagerApp ='0') AND A.FTPoTypeState ='3' "

            _str &= Environment.NewLine & " ) AS A "

            Return (HI.Conn.SQLConn.GetField(_str, HI.Conn.DB.DataBaseName.DB_PUR, "") <> "")

        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Shared Function CheckFactoryApp() As Boolean
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            Dim _LangDisPlay As String = "TH"

            If HI.ST.Lang.Language <> HI.ST.Lang.eLang.TH Then
                _LangDisPlay = "EN"
            End If

            _str = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_ORDER_LIST_DIRECTOR_APPROVE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_LangDisPlay) & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_PUR)

            If _dt.Rows.Count > 0 Then
                _dt.Dispose()
                Return True
            Else
                _dt.Dispose()
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function CheckApproveDocument() As Boolean
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            Dim _LangDisPlay As String = "TH"

            If HI.ST.Lang.Language <> HI.ST.Lang.eLang.TH Then
                _LangDisPlay = "EN"
            End If

            _str = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_DOC) & "].dbo.SP_GET_DIRECTOR_APP_DOCUMENT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_LangDisPlay) & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_DOC)

            If _dt.Rows.Count > 0 Then
                _dt.Dispose()
                Return True
            Else
                _dt.Dispose()
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function LoadOrderCostApprove() As Boolean
        Try
            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            Dim _LangDisPlay As String = "TH"

            If HI.ST.Lang.Language <> HI.ST.Lang.eLang.TH Then
                _LangDisPlay = "EN"
            End If

            _str = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.SP_GET_ORDERCOST_APP '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_LangDisPlay) & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_ACCOUNT)

            If _dt.Rows.Count > 0 Then
                _dt.Dispose()
                Return True
            Else
                _dt.Dispose()
                Return False
            End If

          
        Catch ex As Exception
            Return False
        End Try


    End Function

    Public Shared Function LoadLeaveWaitApp() As Boolean
        Try
            Dim _Cmd As String = String.Empty
            Dim _dt As New DataTable

            Dim _LangDisPlay As String = "TH"

            If HI.ST.Lang.Language <> HI.ST.Lang.eLang.TH Then
                _LangDisPlay = "EN"
            End If

            _Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_LeaveApproved '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_LangDisPlay) & "'"
            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, HI.Conn.DB.DataBaseName.DB_HR)

            If _dt.Rows.Count > 0 Then
                _dt.Dispose()
                Return True
            Else
                _dt.Dispose()
                Return False
            End If


        Catch ex As Exception
            Return False
        End Try


    End Function


    Private Shared Function FindComputerName(ByVal TempComName As String) As Boolean
        Dim _str As String = String.Empty
        Dim _dt As New DataTable

        Try
            _str = "SELECT  isnull(FTComputerName,'') as FTComputerName,isnull(FTUserName,'') as FTUserName  "
            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSECinfigDirector  WITH(NOLOCK) "
            _str &= Environment.NewLine & " WHERE FTComputerName = '" & TempComName & "'"

            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_SECURITY)

   
            If _dt.Rows.Count > 0 Then
                HI.ST.UserInfo.UserName = _dt.Rows(0)!FTUserName.ToString
                Return True
            Else
                Return False
            End If

            _dt.Dispose()

        Catch ex As Exception

        End Try

        Return False
    End Function

End Class
