Imports System.Threading

Public Class wEmployeeLeaveNotAppTracking

    Private _wEmployeeLeave As wEmployeeLeave
    Private _StateCheckWaitting As Boolean
    Private _WformEmpLeave As wEmployeeLeave
    Sub New()

        _ProcPrepare = True
        ' This call is required by the designer.
        InitializeComponent()
        _wEmployeeLeave = New wEmployeeLeave
        HI.TL.HandlerControl.AddHandlerObj(_wEmployeeLeave)
        ' Add any initialization after the InitializeComponent() call.

        _WformEmpLeave = New wEmployeeLeave
        HI.TL.HandlerControl.AddHandlerObj(_WformEmpLeave)

        Dim _Qry As String = ""

        _Qry = " SELECT TOP 1 FTCfgData"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS Z WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE  (FTCfgName = N'LeavePragNentPayMergeSundayHoloday')"

        LeavePragNentPayMergeSundayHoloday = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")


        _ProcPrepare = False

    End Sub

#Region "Initial Grid"

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = "FTEmpCode"

        With ogv
            .ClearGrouping()
            .ClearDocument()

            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            .OptionsView.ShowGroupPanel = True
            .ExpandAllGroups()
            .RefreshData()

        End With
        '------End Add Summary Grid-------------
    End Sub

#End Region

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

#Region "Property"

    Private _LeavePragNentPayMergeSundayHoloday As String = ""
    Public Property LeavePragNentPayMergeSundayHoloday As String
        Get
            Return _LeavePragNentPayMergeSundayHoloday
        End Get
        Set(value As String)
            _LeavePragNentPayMergeSundayHoloday = value
        End Set
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

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Try
            _wEmployeeLeave.Dispose()
        Catch ex As Exception

        End Try
        Me.Close()

    End Sub

#End Region

#Region " Procedure "

#End Region

#Region "General"

#End Region

    Private Delegate Sub DelegateCheckWaiting()
    Private Sub CheckWaiting()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckWaiting(AddressOf CheckWaiting), New Object() {})
        Else

            Dim _Qry As String = ""
            Dim _dt As DataTable

            _Qry = "  SELECT   '0' as FTSelect ,    M.FNHSysEmpID, M.FTEmpCode , A.FNLeaveTotalTimeMin , A.FTStateDeductVacation "

            _Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"
            _Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpNameEN"
            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then

                '' _Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"
                _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameTH  AS FTEmpTypeName "
                _Qry &= vbCrLf & "  ,Dept.FTDeptDescTH  AS FTDeptName "
                _Qry &= vbCrLf & "  ,DI.FTDivisonNameTH  AS FTDivisonName "
                _Qry &= vbCrLf & "  ,ST.FTSectNameTH  AS FTSectName "
                _Qry &= vbCrLf & "  ,US.FTUnitSectNameTH  AS FTUnitSectName "
                _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameTH  AS FTPositName "
                _Qry &= vbCrLf & "  , B.FTNameTH  AS FTLeaveTypeName "
                _Qry &= vbCrLf & " , ISNULL(C.FTNameTH,'') AS FTStaLeaveDayName "

            Else

                '' _Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpName"
                _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameEN  AS FTEmpTypeName "
                _Qry &= vbCrLf & "  ,Dept.FTDeptDescEN  AS FTDeptName "
                _Qry &= vbCrLf & "  ,DI.FTDivisonNameEN  AS FTDivisonName "
                _Qry &= vbCrLf & "  ,ST.FTSectNameEN  AS FTSectName "
                _Qry &= vbCrLf & "  ,US.FTUnitSectNameEN  AS FTUnitSectName "
                _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameEN  AS FTPositName "
                _Qry &= vbCrLf & "  , B.FTNameEN  AS FTLeaveTypeName "
                _Qry &= vbCrLf & " , ISNULL(C.FTNameEN,'') AS FTStaLeaveDayName "

            End If

            _Qry &= vbCrLf & ", ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
            _Qry &= vbCrLf & ", ISNULL(Dept.FTDeptCode,'') AS FTDeptCode, ISNULL(DI.FTDivisonCode,'') AS FTDivisonCode"
            _Qry &= vbCrLf & ", ISNULL(ST.FTSectCode,'') AS FTSectCode,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode"
            _Qry &= vbCrLf & ", OrgPosit.FTPositCode"
            _Qry &= vbCrLf & ", Convert(datetime,A.FTStartDate) As FTStartDate"
            _Qry &= vbCrLf & ", Convert(datetime,A.FTEndDate) As FTEndDate"
            _Qry &= vbCrLf & ",CASE WHEN ISNULL(A.FTHoliday,'0') = '1' THEN  '1'  ELSE '0'  END AS  FTHoliday"
            _Qry &= vbCrLf & ",A.FTLeaveType"
            _Qry &= vbCrLf & ", A.FNLeaveTotalDay "
            _Qry &= vbCrLf & ", CASE WHEN ISNULL(FTLeavePay,'0') = '1' THEN '1' ELSE '0' END AS FTLeavePay"
            _Qry &= vbCrLf & ", FTLeaveStartTime , FTLeaveEndTime, FNLeaveTotalTime, FTLeaveNote,A.FTLeaveType AS FTLeaveTypeCode"
            _Qry &= vbCrLf & ",CASE WHEN ISNULL(FTStaCalSSO,'0') = '1' THEN  '1'  ELSE '0'  END AS  FTStaCalSSO"
            _Qry &= vbCrLf & ",CASE WHEN ISNULL(FTStaLeaveDay,'-1') <='0' THEN '0' ELSE FTStaLeaveDay END As FTStaLeaveDay"

            _Qry &= vbCrLf & " , ISNULL(A.FTMngApproveState,'0') AS FTMngApproveState "
            _Qry &= vbCrLf & " , ISNULL(A.FTMngApproveBy,'') AS FTMngApproveBy "
            _Qry &= vbCrLf & " , Convert(nvarchar(10) ,convert(datetime,A.FDMngApproveDate) , 103) AS FDMngApproveDate "
            _Qry &= vbCrLf & " , ISNULL(A.FTMngApproveTime,'') AS FTMngApproveTime "


            _Qry &= vbCrLf & " , ISNULL(A.FTDirApproveState,'0') AS FTDirApproveState "
            _Qry &= vbCrLf & " , ISNULL(A.FTDirApproveBy,'') AS FTDirApproveBy "
            _Qry &= vbCrLf & " , Convert(nvarchar(10) ,convert(datetime,A.FDDirApproveDate) , 103) AS FDDirApproveDate "
            _Qry &= vbCrLf & " , ISNULL(A.FTDirApproveTime,'') AS FTDirApproveTime "


            _Qry &= vbCrLf & " , ISNULL(A.FTStateMedicalCertificate,'0') AS FTStateMedicalCertificate "
            _Qry &= vbCrLf & ",A.FTInsUser, A.FTInsTime"
            _Qry &= vbCrLf & " , Convert(nvarchar(10) ,convert(datetime,A.FTInsDate) , 103) AS FTInsDate "

            _Qry &= vbCrLf & " , CASE WHEN ISNULL(A.FBFile,'') = '' THEN '0' else '1' END AS FTFBFile"
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily As A WITH(NOLOCK) Left Outer Join (SELECt * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS LT WITH(NOLOCK) WHERE FTListName='FNLeaveType' ) As B ON A.FTLeaveType = Convert(varchar(50),B.FNListIndex) "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN (SELECT * "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS LD WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTListName='FNLeaveDay' ) As C ON A.FTStaLeaveDay = Convert(varchar(50),C.FNListIndex) "
            _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH(NOLOCK) ON A.FNHSysEmpID = M.FNHSysEmpID  LEFT OUTER JOIN "
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS OrgPosit WITH (NOLOCK) ON M.FNHSysPositId = OrgPosit.FNHSysPositId "
            _Qry &= vbCrLf & "  WHERE M.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID) & " AND ISNULL(FTApproveState,'0') <>'1'  and ( ISNULL(A.FTDirApproveState, N'0') = N'1' OR  isnull(M.FTUserNameMngFac,'') = '')  and ISNULL(A.FTMngApproveState, N'0') = N'1' "


            _Qry &= vbCrLf & " ORDER BY A.FTStartDate ASC,M.FTEmpCode  ASC "

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            Me.ogc.DataSource = _dt.Copy

            _StateCheckWaitting = True

            Call CheckWaitingAllTracking()

            Call CheckWaitingReject()

        End If
    End Sub


    Private Sub CheckWaitingAllTracking()


        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "  SELECT   '0' as FTSelect ,    M.FNHSysEmpID, M.FTEmpCode , A.FNLeaveTotalTimeMin , A.FTStateDeductVacation "

        _Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"
        _Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpNameEN"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then

            ''_Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"
            _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameTH  AS FTEmpTypeName "
            _Qry &= vbCrLf & "  ,Dept.FTDeptDescTH  AS FTDeptName "
            _Qry &= vbCrLf & "  ,DI.FTDivisonNameTH  AS FTDivisonName "
            _Qry &= vbCrLf & "  ,ST.FTSectNameTH  AS FTSectName "
            _Qry &= vbCrLf & "  ,US.FTUnitSectNameTH  AS FTUnitSectName "
            _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameTH  AS FTPositName "
            _Qry &= vbCrLf & "  , B.FTNameTH  AS FTLeaveTypeName "
            _Qry &= vbCrLf & " , ISNULL(C.FTNameTH,'') AS FTStaLeaveDayName "
            _Qry &= vbCrLf & "  ,u1.FTUserDescriptionTH AS FTUserNameChk "
            _Qry &= vbCrLf & "  ,u2.FTUserDescriptionTH  AS FTUserNameMngFac "
            _Qry &= vbCrLf & "  "
        Else

            '' _Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpName"
            _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameEN  AS FTEmpTypeName "
            _Qry &= vbCrLf & "  ,Dept.FTDeptDescEN  AS FTDeptName "
            _Qry &= vbCrLf & "  ,DI.FTDivisonNameEN  AS FTDivisonName "
            _Qry &= vbCrLf & "  ,ST.FTSectNameEN  AS FTSectName "
            _Qry &= vbCrLf & "  ,US.FTUnitSectNameEN  AS FTUnitSectName "
            _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameEN  AS FTPositName "
            _Qry &= vbCrLf & "  , B.FTNameEN  AS FTLeaveTypeName "
            _Qry &= vbCrLf & " , ISNULL(C.FTNameEN,'') AS FTStaLeaveDayName "
            _Qry &= vbCrLf & "  ,u1.FTUserDescriptionEN AS FTUserNameChk "
            _Qry &= vbCrLf & "  ,u2.FTUserDescriptionEN  AS FTUserNameMngFac "
        End If

        _Qry &= vbCrLf & ", ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
        _Qry &= vbCrLf & ", ISNULL(Dept.FTDeptCode,'') AS FTDeptCode, ISNULL(DI.FTDivisonCode,'') AS FTDivisonCode"
        _Qry &= vbCrLf & ", ISNULL(ST.FTSectCode,'') AS FTSectCode,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode"
        _Qry &= vbCrLf & ", OrgPosit.FTPositCode"
        _Qry &= vbCrLf & ", Convert(datetime,A.FTStartDate) As FTStartDate"
        _Qry &= vbCrLf & ", Convert(datetime,A.FTEndDate) As FTEndDate"
        _Qry &= vbCrLf & ",CASE WHEN ISNULL(A.FTHoliday,'0') = '1' THEN  '1'  ELSE '0'  END AS  FTHoliday"
        _Qry &= vbCrLf & ",A.FTLeaveType"
        _Qry &= vbCrLf & ", A.FNLeaveTotalDay "
        _Qry &= vbCrLf & ", CASE WHEN ISNULL(FTLeavePay,'0') = '1' THEN '1' ELSE '0' END AS FTLeavePay"
        _Qry &= vbCrLf & ", FTLeaveStartTime , FTLeaveEndTime, FNLeaveTotalTime, FTLeaveNote,A.FTLeaveType AS FTLeaveTypeCode"
        _Qry &= vbCrLf & ",CASE WHEN ISNULL(FTStaCalSSO,'0') = '1' THEN  '1'  ELSE '0'  END AS  FTStaCalSSO"
        _Qry &= vbCrLf & ",CASE WHEN ISNULL(FTStaLeaveDay,'-1') <='0' THEN '0' ELSE FTStaLeaveDay END As FTStaLeaveDay"

        _Qry &= vbCrLf & " , ISNULL(A.FTMngApproveState,'0') AS FTMngApproveState "
        _Qry &= vbCrLf & " , ISNULL(A.FTMngApproveBy,'') AS FTMngApproveBy "
        _Qry &= vbCrLf & " , Convert(nvarchar(10) ,convert(datetime,A.FDMngApproveDate) , 103) AS FDMngApproveDate "
        _Qry &= vbCrLf & " , ISNULL(A.FTMngApproveTime,'') AS FTMngApproveTime "


        _Qry &= vbCrLf & " , ISNULL(A.FTDirApproveState,'0') AS FTDirApproveState "
        _Qry &= vbCrLf & " , ISNULL(A.FTDirApproveBy,'') AS FTDirApproveBy "
        _Qry &= vbCrLf & " , Convert(nvarchar(10) ,convert(datetime,A.FDDirApproveDate) , 103) AS FDDirApproveDate "
        _Qry &= vbCrLf & " , ISNULL(A.FTDirApproveTime,'') AS FTDirApproveTime "


        _Qry &= vbCrLf & " , ISNULL(A.FTStateMedicalCertificate,'0') AS FTStateMedicalCertificate "
        _Qry &= vbCrLf & ",A.FTInsUser, A.FTInsTime, M.FTUserNameChk, M.FTUserNameMngFac "

        _Qry &= vbCrLf & " "

        _Qry &= vbCrLf & " , Convert(nvarchar(10) ,convert(datetime,A.FTInsDate) , 103) AS FTInsDate "
        _Qry &= vbCrLf & " , CASE WHEN ISNULL(A.FBFile,'') = '' THEN '0' else '1' END AS FTFBFile"
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily As A WITH(NOLOCK) Left Outer Join (SELECt * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS LT WITH(NOLOCK) WHERE FTListName='FNLeaveType' ) As B ON A.FTLeaveType = Convert(varchar(50),B.FNListIndex) "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN (SELECT * "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS LD WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTListName='FNLeaveDay' ) As C ON A.FTStaLeaveDay = Convert(varchar(50),C.FNListIndex) "
        _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH(NOLOCK) ON A.FNHSysEmpID = M.FNHSysEmpID  LEFT OUTER JOIN "
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS OrgPosit WITH (NOLOCK) ON M.FNHSysPositId = OrgPosit.FNHSysPositId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].[dbo].[TSEUserLogin] as u1 on u1.FTUserName=M.FTUserNameChk "
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].[dbo].[TSEUserLogin] as u2 on u2.FTUserName=M.FTUserNameMngFac "


        _Qry &= vbCrLf & "  WHERE M.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID) & " AND ISNULL(FTApproveState,'0') <>'1'  "

        _Qry &= vbCrLf & "   and (ISNULL(A.FTMngApproveState, N'0') = N'0'  OR "
        _Qry &= vbCrLf & "  ( ISNULL(A.FTDirApproveState, N'0') = N'0' AND  isnull(M.FTUserNameMngFac,'') <> '')) "

        _Qry &= vbCrLf & " "
        _Qry &= vbCrLf & " "
        _Qry &= vbCrLf & " "
        _Qry &= vbCrLf & "    AND NOT (( A.FTMngApproveState= N'0' AND  isnull(A.FTMngApproveBy,'') <> '')  "
        _Qry &= vbCrLf & "     OR "
        _Qry &= vbCrLf & "   ( A.FTDirApproveState= N'0' AND  isnull(A.FTDirApproveBy,'') <> '')) "
        _Qry &= vbCrLf & " ORDER BY A.FTStartDate ASC,M.FTEmpCode  ASC "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        Me.ogcAll.DataSource = _dt.Copy


    End Sub

    Private Sub CheckWaitingReject()


        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "  SELECT   '0' as FTSelect ,    M.FNHSysEmpID, M.FTEmpCode , A.FNLeaveTotalTimeMin , A.FTStateDeductVacation "

        _Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"
        _Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpNameEN"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then

            ''_Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"
            _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameTH  AS FTEmpTypeName "
            _Qry &= vbCrLf & "  ,Dept.FTDeptDescTH  AS FTDeptName "
            _Qry &= vbCrLf & "  ,DI.FTDivisonNameTH  AS FTDivisonName "
            _Qry &= vbCrLf & "  ,ST.FTSectNameTH  AS FTSectName "
            _Qry &= vbCrLf & "  ,US.FTUnitSectNameTH  AS FTUnitSectName "
            _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameTH  AS FTPositName "
            _Qry &= vbCrLf & "  , B.FTNameTH  AS FTLeaveTypeName "
            _Qry &= vbCrLf & " , ISNULL(C.FTNameTH,'') AS FTStaLeaveDayName "
            _Qry &= vbCrLf & "  ,u1.FTUserDescriptionTH AS FTUserNameChk "
            _Qry &= vbCrLf & "  ,u2.FTUserDescriptionTH  AS FTUserNameMngFac "
            _Qry &= vbCrLf & "  "
        Else

            '' _Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpName"
            _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameEN  AS FTEmpTypeName "
            _Qry &= vbCrLf & "  ,Dept.FTDeptDescEN  AS FTDeptName "
            _Qry &= vbCrLf & "  ,DI.FTDivisonNameEN  AS FTDivisonName "
            _Qry &= vbCrLf & "  ,ST.FTSectNameEN  AS FTSectName "
            _Qry &= vbCrLf & "  ,US.FTUnitSectNameEN  AS FTUnitSectName "
            _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameEN  AS FTPositName "
            _Qry &= vbCrLf & "  , B.FTNameEN  AS FTLeaveTypeName "
            _Qry &= vbCrLf & " , ISNULL(C.FTNameEN,'') AS FTStaLeaveDayName "
            _Qry &= vbCrLf & "  ,u1.FTUserDescriptionEN AS FTUserNameChk "
            _Qry &= vbCrLf & "  ,u2.FTUserDescriptionEN  AS FTUserNameMngFac "
        End If

        _Qry &= vbCrLf & ", ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
        _Qry &= vbCrLf & ", ISNULL(Dept.FTDeptCode,'') AS FTDeptCode, ISNULL(DI.FTDivisonCode,'') AS FTDivisonCode"
        _Qry &= vbCrLf & ", ISNULL(ST.FTSectCode,'') AS FTSectCode,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode"
        _Qry &= vbCrLf & ", OrgPosit.FTPositCode"
        _Qry &= vbCrLf & ", Convert(datetime,A.FTStartDate) As FTStartDate"
        _Qry &= vbCrLf & ", Convert(datetime,A.FTEndDate) As FTEndDate"
        _Qry &= vbCrLf & ",CASE WHEN ISNULL(A.FTHoliday,'0') = '1' THEN  '1'  ELSE '0'  END AS  FTHoliday"
        _Qry &= vbCrLf & ",A.FTLeaveType"
        _Qry &= vbCrLf & ", A.FNLeaveTotalDay "
        _Qry &= vbCrLf & ", CASE WHEN ISNULL(FTLeavePay,'0') = '1' THEN '1' ELSE '0' END AS FTLeavePay"
        _Qry &= vbCrLf & ", FTLeaveStartTime , FTLeaveEndTime, FNLeaveTotalTime, FTLeaveNote,A.FTLeaveType AS FTLeaveTypeCode"
        _Qry &= vbCrLf & ",CASE WHEN ISNULL(FTStaCalSSO,'0') = '1' THEN  '1'  ELSE '0'  END AS  FTStaCalSSO"
        _Qry &= vbCrLf & ",CASE WHEN ISNULL(FTStaLeaveDay,'-1') <='0' THEN '0' ELSE FTStaLeaveDay END As FTStaLeaveDay"

        _Qry &= vbCrLf & " , ISNULL(A.FTMngApproveState,'0') AS FTMngApproveState "
        _Qry &= vbCrLf & " , ISNULL(A.FTMngApproveBy,'') AS FTMngApproveBy "
        _Qry &= vbCrLf & " , Convert(nvarchar(10) ,convert(datetime,A.FDMngApproveDate) , 103) AS FDMngApproveDate "
        _Qry &= vbCrLf & " , ISNULL(A.FTMngApproveTime,'') AS FTMngApproveTime "


        _Qry &= vbCrLf & " , ISNULL(A.FTDirApproveState,'0') AS FTDirApproveState "
        _Qry &= vbCrLf & " , ISNULL(A.FTDirApproveBy,'') AS FTDirApproveBy "
        _Qry &= vbCrLf & " , Convert(nvarchar(10) ,convert(datetime,A.FDDirApproveDate) , 103) AS FDDirApproveDate "
        _Qry &= vbCrLf & " , ISNULL(A.FTDirApproveTime,'') AS FTDirApproveTime "


        _Qry &= vbCrLf & " , ISNULL(A.FTStateMedicalCertificate,'0') AS FTStateMedicalCertificate "
        _Qry &= vbCrLf & ",A.FTInsUser, A.FTInsTime, M.FTUserNameChk, M.FTUserNameMngFac "

        _Qry &= vbCrLf & " "

        _Qry &= vbCrLf & " , Convert(nvarchar(10) ,convert(datetime,A.FTInsDate) , 103) AS FTInsDate "
        _Qry &= vbCrLf & " , CASE WHEN ISNULL(A.FBFile,'') = '' THEN '0' else '1' END AS FTFBFile"
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily As A WITH(NOLOCK) Left Outer Join (SELECt * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS LT WITH(NOLOCK) WHERE FTListName='FNLeaveType' ) As B ON A.FTLeaveType = Convert(varchar(50),B.FNListIndex) "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN (SELECT * "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS LD WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTListName='FNLeaveDay' ) As C ON A.FTStaLeaveDay = Convert(varchar(50),C.FNListIndex) "
        _Qry &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH(NOLOCK) ON A.FNHSysEmpID = M.FNHSysEmpID  LEFT OUTER JOIN "
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS OrgPosit WITH (NOLOCK) ON M.FNHSysPositId = OrgPosit.FNHSysPositId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].[dbo].[TSEUserLogin] as u1 on u1.FTUserName=M.FTUserNameChk "
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].[dbo].[TSEUserLogin] as u2 on u2.FTUserName=M.FTUserNameMngFac "


        _Qry &= vbCrLf & "  WHERE M.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID) & "  "
        _Qry &= vbCrLf & "  AND (( A.FTMngApproveState= N'0' AND  isnull(A.FTMngApproveBy,'') <> '') "
        _Qry &= vbCrLf & "     OR "
        _Qry &= vbCrLf & "  ( A.FTDirApproveState= N'0' AND  isnull(A.FTDirApproveBy,'') <> ''))"
        _Qry &= vbCrLf & "  and  A.FTStartDate >  dateadd(month,-3, getdate()) "
        _Qry &= vbCrLf & " ORDER BY A.FTStartDate ASC,M.FTEmpCode  ASC "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        Me.ogcReject.DataSource = _dt.Copy


    End Sub


    Private Sub wEmployeeLeaveNotAppTracking_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ocmcheckwaiting.Enabled = False
    End Sub

    Private Sub wLeave_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _StateCheckWaitting = False
        Dim _Theard As New Thread(AddressOf CheckWaiting)
        _Theard.Start()
        InitGrid()
        AddHandler ogv.DoubleClick, AddressOf GridView_DoubleClick
        ' Me.ocmcheckwaiting.Enabled = True
    End Sub

    Public Shared Sub GridView_DoubleClick(sender As Object, e As System.EventArgs)

        With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
            If .FocusedRowHandle < 0 Then Exit Sub
            Dim _Form As Object = .GridControl.FindForm




            If Not (_Form Is Nothing) Then
                Dim _Value As String = Nothing
                Dim myList As New ArrayList

                Dim T As System.Type = _Form.GetType()

                Dim _MemuName As String = "MnuLeave"
                Dim _MethodName As String = "LoadEmpCodeByEmpIDInfoCustom" ''_pcallmathodinfo.GetValue(_Form, Nothing).ToString()
                Dim _MethodParmName As String = "FNHSysEmpID,FTLeaveType,FTStaLeaveDayName,FTStartDate,FTEndDate,FTLeaveStartTime,FTLeaveEndTime,FTStaCalSSO,FTLeavePay,FTStateDeductVacation,FTHoliday,FTStateMedicalCertificate,FTLeaveNote" ''_pcallmethodpraminfo.GetValue(_Form, Nothing).ToString()
                'Data_FTStartDate = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTStartDate").ToString)
                'Catch ex As Exception
                'End Try

                'Try
                '    Data_FTEndDate = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTEndDate").ToString)
                If _MemuName <> "" And _MethodName <> "" And _MethodParmName <> "" Then


                    If _MethodParmName <> "" Then
                        For Each Str As String In _MethodParmName.Split(",")
                            If Str <> "" Then


                                If _Value <> "" Then
                                    _Value &= "|"
                                End If

                                If Not (.Columns.ColumnByFieldName(Str) Is Nothing) Then
                                    _Value &= .GetFocusedRowCellValue(.Columns.ColumnByFieldName(Str)).ToString()
                                End If



                            End If


                        Next

                        myList.Add(_Value)
                    End If

                    Try

                        Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
                        HI.ST.SysInfo.MenuName = _MemuName
                        Call CallByName(_Form.Parent.Parent, "CallWindowForm", CallType.Method, {_MemuName, _MethodName, myList.ToArray(GetType(String))})
                        HI.ST.SysInfo.MenuName = _TmpMenu

                    Catch ex As Exception
                    End Try
                End If
            End If



        End With
    End Sub


    Private Sub ocmcheckwaiting_Tick(sender As Object, e As EventArgs) Handles ocmcheckwaiting.Tick
        If (_StateCheckWaitting) Then
            _StateCheckWaitting = False
            Dim _Theard As New Thread(AddressOf CheckWaiting)
            _Theard.Start()
        End If
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            CheckWaiting()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click
        Try
            Dim _oDt As DataTable
            Dim _Cmd As String = ""
            With DirectCast(Me.ogc.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTSelect ='1'").Length <= 0 Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text)
                    Exit Sub
                End If

                If Not HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, " อนุมัติการลา..") Then
                    Exit Sub
                End If


                For Each R As DataRow In .Select("FTSelect ='1'")
                    _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily"
                    _Cmd &= vbCrLf & " Set FTApproveState='1'"
                    _Cmd &= vbCrLf & ", FTApproveBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ", FTApproveDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ", FTApproveTime=" & HI.UL.ULDate.FormatTimeDB

                    _Cmd &= vbCrLf & " WHERE FNHSysEmpId='" & HI.UL.ULF.rpQuoted(R!FNHSysEmpId.ToString) & "'"
                    _Cmd &= vbCrLf & "and FTStartDate='" & HI.UL.ULDate.ConvertEnDB(R!FTStartDate.ToString) & "'"
                    _Cmd &= vbCrLf & "and  FTEndDate='" & HI.UL.ULDate.ConvertEnDB(R!FTEndDate.ToString) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_HR)


                    _Cmd = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave "
                    _Cmd &= vbCrLf & " WHERE FNHSysEmpID = " & Val(R!FNHSysEmpId.ToString) & " "
                    _Cmd &= vbCrLf & " AND FTDateTrans >= '" & HI.UL.ULDate.ConvertEnDB(R!FTStartDate.ToString) & "'"
                    _Cmd &= vbCrLf & " AND FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(R!FTEndDate.ToString) & "'"
                    _Cmd &= vbCrLf & " AND FTLeaveType = '" & HI.TL.CboList.GetListValue("FNLeaveDay", R!FTLeaveType.ToString) & "'"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_HR)



                    ApproveDataLeave(R!FTEndDate.ToString, R!FTStartDate.ToString, R!FTLeaveType.ToString, R!FNLeaveTotalTime.ToString, R!FNLeaveTotalTimeMin.ToString, R!FTLeavePay.ToString _
                                 , Val(R!FNHSysEmpId.ToString), R!FTStateMedicalCertificate.ToString, R!FTStaLeaveDay.ToString, R!FTLeaveStartTime.ToString, R!FTLeaveEndTime.ToString, R!FTStaCalSSO.ToString _
                                 , R!FNLeaveTotalDay.ToString, R!FTStateDeductVacation.ToString, R!FTHoliday.ToString)

                Next
            End With


            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            CheckWaiting()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub oSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles oSelectAll.CheckedChanged
        Try
            Dim _State As String = ""
            _State = IIf(Me.oSelectAll.Checked, "1", "0")

            With ogc
                If Not (.DataSource Is Nothing) And ogv.RowCount > 0 Then
                    With ogv
                        For i As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(i, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub




    Private Function ApproveDataLeave(ByVal _EndDate As String, _StartDate As String, _LeaveType As String, _NetTime As Double, _Totaltime As Double,
                                          _LeavePay As String, _EmpId As Integer, _StateMedicalCertificate As String, _LeaveDay As String,
                                          _StartTime As String, _EndTime As String, _StateSocial As String, _LeaveTotalDay As Integer, _StateDeductVacation As String, _FTHoliday As String) As Boolean
        Try
            Dim _EndProcDate As String = HI.UL.ULDate.ConvertEnDB(_EndDate)
            Dim _NextProcDate As String = ""
            Dim nNextDay As Double = 0
            _NextProcDate = HI.UL.ULDate.ConvertEnDB(_StartDate)

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
            Dim _LeaveCode As String = HI.TL.CboList.GetListValue("FNLeaveDay", _LeaveType)
            Dim _WeekEnd As Integer
            Dim _LeavePragNentPay As Integer = 0
            Dim _LeavePragNentNotPay As Boolean = False
            Dim _EmpTypeWeekly As DataTable
            Dim _EmpTypeId As Integer = 0

            _TmpTotalHour = CDbl(Format(Val(_NetTime), "0.00"))
            _TmpFNTotalMonute = _Totaltime

            If (_LeavePay = "1") Then
                _TmpFNTotalPayHour = _TmpTotalHour
                _TmpFNTotalPayMonute = _TmpFNTotalMonute
            Else
                _TmpFNTotalNotPayHour = _TmpTotalHour
                _TmpFNTotalNotPayMonute = _TmpFNTotalMonute
            End If

            _Qry = "Select Top 1 FNHSysEmpTypeId From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(_EmpId) & " "
            _EmpTypeId = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")

            _Qry = "SELECt   FDHolidayDate  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmpTypeWeeklySpecial WITH(NOLOCK) "
            _Qry &= vbCrLf & "  WHERE FDHolidayDate>='" & HI.UL.ULDate.ConvertEnDB(_StartDate) & "' "
            _Qry &= vbCrLf & "  AND FDHolidayDate<='" & HI.UL.ULDate.ConvertEnDB(_EndDate) & "' "
            _Qry &= vbCrLf & "  AND FNHSysEmpTypeId=" & Integer.Parse(Val(_EmpTypeId)) & " "

            _EmpTypeWeekly = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
            _Qry = "   SELECT    Top 1   FTSunday,FTMonday, FTTuesday, FTWednesday, "
            _Qry &= vbCrLf & "   FTThursday, FTFriday, FTSaturday"
            _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly  As W WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(_EmpId) & " "
            _dtWeekend = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            If _dtWeekend.Rows.Count <= 0 Then
            Else
                _EmpTypeWeekly.Rows.Clear()
            End If

            _Qry = "SELECt   FDHolidayDate   "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday WITH(NOLOCK) WHERE FTStateActive='1' AND FNHSysCmpId= " & Val(HI.ST.SysInfo.CmpID)
            _dtHoliday = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

            _Qry = "  SELECT   TOP 1   M.FNHSysShiftID"
            _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS S WITH (NOLOCK) ON M.FNHSysShiftID = S.FNHSysShiftID"
            _Qry &= vbCrLf & "   WHERE M.FNHSysEmpID=" & Val(_EmpId) & " "

            Dim _EmpOrgShift As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
            Dim _EmpShift As String = _EmpOrgShift
            Dim _EmpPgmCode As String
            Dim _TotalWorkHour As Double

            If _LeaveCode = 97 Then
                _Qry = "Select Top 1 FNLeavePay "
                _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLeave WITH(NOLOCK) "
                _Qry &= vbCrLf & " WHERE FNHSysEmpTypeId =" & Val(_EmpTypeId) & " "
                _Qry &= vbCrLf & " AND FTLeaveCode ='" & _LeaveCode & "' "
                _LeavePragNentPay = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")))
                '_Qry = "   SELECT        COUNT(FTDateTrans) AS FNPayDay"
                '_Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave WITH(NOLOCK) "
                '_Qry &= vbCrLf & " WHERE        (FTLeaveType ='" & _LeaveCode & "')"
                '_Qry &= vbCrLf & " AND (FNHSysEmpID =" & Val(_EmpId) & " ) "
                '_Qry &= vbCrLf & " AND (FTDateTrans < N'" & _NextProcDate & "')"
                '_Qry &= vbCrLf & " AND (FNTotalPayMinute > 0) "

                _Qry = "   SELECT        COUNT(FTDateTrans) AS FNPayDay"
                _Qry &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave WITH(NOLOCK) "
                _Qry &= vbCrLf & " WHERE        (FTLeaveType ='" & _LeaveCode & "')"
                _Qry &= vbCrLf & " AND (FNHSysEmpID =" & Val(_EmpId) & " ) "

                _Qry &= vbCrLf & " AND (FTDateTrans > DATEADD(month, -6, '" & _NextProcDate & "')) "
                _Qry &= vbCrLf & " AND (FNTotalPayMinute > 0) "

                _LeavePragNentPay = _LeavePragNentPay - Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")))
            End If


            Do While _NextProcDate <= _EndProcDate
                _EmpPgmCode = ""
                _EmpPgmCode = ""
                _TotalWorkHour = 8
                _WeekEnd = Weekday(CDate(_NextProcDate), Microsoft.VisualBasic.FirstDayOfWeek.Sunday)
                _TotalHour = _TmpTotalHour
                _FNTotalMonute = _TmpFNTotalMonute

                If (_LeavePay = "1") Then
                    _FNTotalPayHour = _TotalHour
                    _FNTotalPayMonute = _FNTotalMonute
                Else
                    _FNTotalNotPayHour = _TotalHour
                    _FNTotalNotPayMonute = _FNTotalMonute
                End If

                _SkipProcess = False
                _LeavePragNentNotPay = False

                If (_FTHoliday = "0") Then
                Else
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

                If _LeaveCode = "97" And (_LeavePay = "1") Then
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
                    _Qry &= vbCrLf & " ," & Val(_EmpId) & ",'" & _NextProcDate & "' "
                    _Qry &= vbCrLf & " ,'" & _LeaveCode & "'"
                    _Qry &= vbCrLf & " ," & _TotalHour & ""
                    _Qry &= vbCrLf & " ," & _FNTotalMonute & ""

                    If (_LeaveCode = "97" And (_LeavePragNentPay <= 0 Or _LeavePragNentNotPay)) And (_LeavePay = "1") Then

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

                    _Qry &= vbCrLf & " ,'" & _StartTime & "'"
                    _Qry &= vbCrLf & " ,'" & _EndTime & "'"
                    _Qry &= vbCrLf & " ,'" & _StateSocial & "'"
                    _Qry &= vbCrLf & " ,'" & HI.TL.CboList.GetListValue("FNLeaveDay", _LeaveDay) & "'"
                    _Qry &= vbCrLf & "," & _LeaveTotalDay & " "
                    _Qry &= vbCrLf & ",'" & _StateMedicalCertificate & "'"
                    _Qry &= vbCrLf & " ,'" & _StateDeductVacation & "'"

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    If _LeaveCode = "97" And (_LeavePay = "1") Then
                        If Not (_LeavePragNentNotPay) Then
                            _LeavePragNentPay = _LeavePragNentPay - 1
                        End If
                    End If

                End If

                HI.HRCAL.Calculate.CalculateWorkTime(HI.ST.UserInfo.UserName, _EmpId, _NextProcDate, _NextProcDate)

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

    Private Sub ogv_DoubleClick(sender As Object, e As EventArgs) ' Handles ogv.DoubleClick
        Try
            Dim Data_FNHSysEmpID As Integer = 0

            Dim Data_FTLeaveType As String = ""
            Dim Data_FTStaLeaveDay As String = ""
            Dim Data_FTStartDate As String = ""
            Dim Data_FTEndDate As String = ""
            Dim Data_FTLeaveStartTime As String = ""
            Dim Data_FTLeaveEndTime As String = ""
            Dim Data_FTStaCalSSO As String = ""
            Dim Data_FTLeavePay As String = ""
            Dim Data_FTStateDeductVacation As String = ""
            Dim Data_FTLeaveNote As String = ""
            Dim Data_FTHoliday As String = ""
            Dim Data_FTStateMedicalCertificate As String = ""

            With ogv
                Data_FNHSysEmpID = .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID")

                Data_FTLeaveType = .GetRowCellValue(.FocusedRowHandle, "FTLeaveType")
                Data_FTStaLeaveDay = .GetRowCellValue(.FocusedRowHandle, "FTStaLeaveDay")

                Try
                    Data_FTStartDate = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTStartDate").ToString)
                Catch ex As Exception
                End Try

                Try
                    Data_FTEndDate = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTEndDate").ToString)
                Catch ex As Exception
                End Try

                Data_FTLeaveStartTime = "" & .GetRowCellValue(.FocusedRowHandle, "FTLeaveStartTime").ToString
                Data_FTLeaveEndTime = "" & .GetRowCellValue(.FocusedRowHandle, "FTLeaveEndTime").ToString

                Data_FTStaCalSSO = ("" & .GetRowCellValue(.FocusedRowHandle, "FTStaCalSSO").ToString = "1")
                Data_FTLeavePay = ("" & .GetRowCellValue(.FocusedRowHandle, "FTLeavePay").ToString = "1")
                Data_FTStateDeductVacation = ("" & .GetRowCellValue(.FocusedRowHandle, "FTStateDeductVacation").ToString = "1")

                Data_FTLeaveNote = "" & .GetRowCellValue(.FocusedRowHandle, "FTLeaveNote").ToString
                Data_FTHoliday = ("" & .GetRowCellValue(.FocusedRowHandle, "FTHoliday").ToString = "1")
                Data_FTStateMedicalCertificate = ("" & .GetRowCellValue(.FocusedRowHandle, "FTStateMedicalCertificate").ToString = "1")
            End With

            Dim _Cmd As String = "Select top 1  FTEmpCode from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee with(nolock) where FNHSysEmpId=" & Data_FNHSysEmpID
            Dim empcode As String = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_HR, "")
            Dim _oDt As DataTable

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Cmd = "SELECT  top 1    M.FTEmpCode, P.FTPreNameNameTH + ' ' +  M.FTEmpNameTH +' ' +  M.FTEmpSurnameTH AS FTDescription, CASE WHEN ISDAte(M.FDDateStart) = 1 THEN CONVERT(Varchar(10),Convert(Datetime,M.FDDateStart),103) ELSE '' END As  FDDateStart, M.FNHSysEmpID,CASE WHEN ISDAte(M.FDDateEnd) = 1 THEN CONVERT(Varchar(10),Convert(Datetime,M.FDDateEnd),103) ELSE '' END As  FDDateEnd,FTEmpCodeRefer FROM            [HITECH_HR].dbo.THRMEmployee  AS M WITH (NOLOCK) INNER JOIN HITECH_MASTER.dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId"
                _Cmd &= vbCrLf & " where M.FNHSysEmpId=" & Data_FNHSysEmpID
            Else
                _Cmd = "SELECT   Top 1   M.FTEmpCode, P.FTPreNameNameEN + ' ' +  M.FTEmpNameEN +' ' +  M.FTEmpSurnameEN AS FTDescription, CASE WHEN ISDAte(M.FDDateStart) = 1 THEN CONVERT(Varchar(10),Convert(Datetime,M.FDDateStart),103) ELSE '' END As  FDDateStart, M.FNHSysEmpID,CASE WHEN ISDAte(M.FDDateEnd) = 1 THEN CONVERT(Varchar(10),Convert(Datetime,M.FDDateEnd),103) ELSE '' END As  FDDateEnd,FTEmpCodeRefer FROM            [HITECH_HR].dbo.THRMEmployee  AS M WITH (NOLOCK) INNER JOIN HITECH_MASTER.dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId"
                _Cmd &= vbCrLf & " where M.FNHSysEmpId=" & Data_FNHSysEmpID
            End If
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_HR)

            ' Dim _WformLeave As New wEmployeeLeave

            'Call HI.TL.CboList.PrepareList()


            'wEmployeeLeave_H = New wEmployeeLeave
            ''Call HI.TL.HandlerControl.AddHandlerObj(_WformPo)
            'With _WformPo

            '    .ocmexit.Visible = False
            '    .ocmclear.Visible = False
            '    .FNHSysEmpId.Properties.ReadOnly = True
            '    .FNHSysEmpId.Properties.Buttons(0).Enabled = False
            '    .FNHSysEmpId.Text = empcode
            '    .FNHSysEmpId_None.Text = _oDt.Rows(0).Item("FTDescription").ToString


            '    .FNLeaveType.SelectedIndex = HI.TL.CboList.GetIndexByValue(.FNLeaveType.Properties.Tag.ToString, "" & Data_FTLeaveType)
            '    .FNLeaveDay.SelectedIndex = HI.TL.CboList.GetIndexByValue(.FNLeaveDay.Properties.Tag.ToString, "" & Data_FTStaLeaveDay)


            '    .FTStartDate.DateTime = Data_FTStartDate
            '    .FTEndDate.DateTime = Data_FTEndDate

            '    .FTStateCalSSo.Checked = Data_FTStaCalSSO
            '    .FTStateLeavepay.Checked = Data_FTLeavePay
            '    .FTStateDeductVacation.Checked = Data_FTStateDeductVacation



            '    .FTRemark.Text = Data_FTLeaveNote

            '    .FNLeaveType_param.Text = Data_FTLeaveType
            '    .FTStartDate_param.Text = Data_FTStartDate
            '    .FTStateNotMergeHoliday.Checked = Data_FTHoliday
            '    .FTStateNotMergeHoliday_param.Checked = Data_FTHoliday
            '    '.FNHSysEmpId.Properties.Buttons(1).Enabled = False
            '    ' Call wLeave_Load




            'End With

            'Call HI.TL.HandlerControl.AddHandlerObj(_WformPo)


            ' Dim _WShow As New wShowData(_WformPo, _PurchaseNo)


            'Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
            'HI.ST.SysInfo.MenuName = "MnuLeave"
            'Dim _WShow As New wShowData(_WformPo, empcode)
            'HI.ST.SysInfo.MenuName = _TmpMenu

            'Dim _Main As HI.APP.Main()
            'If Not (_WformEmpLeave Is Nothing) Then
            '    Call HI.TL.HandlerControl.AddHandlerObj(_WformEmpLeave)
            'End If

            '_wEmployeeLeave = New wEmployeeLeave
            'Call HI.TL.HandlerControl.AddHandlerObj(_wEmployeeLeave)
            If (_wEmployeeLeave.ActiveControl Is Nothing) Then




                _wEmployeeLeave = New wEmployeeLeave
                Call HI.TL.HandlerControl.AddHandlerObj(_wEmployeeLeave)
            End If

            With _wEmployeeLeave

                .ControlBox = True
                '' .MdiChildren(0)
                .MdiParent = Me.Parent.Parent
                .WindowState = System.Windows.Forms.FormWindowState.Maximized



                '' With DirectCast(.MdiChildren(0), wEmployeeLeave)


                '.ocmexit.Visible = False
                '    .ocmclear.Visible = False
                .FNHSysEmpId.Properties.ReadOnly = True
                .FNHSysEmpId.Properties.Buttons(0).Enabled = False
                .FNHSysEmpId.Text = empcode
                ''   .LoadKey()
                .FNHSysEmpId_None.Text = _oDt.Rows(0).Item("FTDescription").ToString
                ''._HRProcess = "Y"

                .FNLeaveType.SelectedIndex = HI.TL.CboList.GetIndexByValue(.FNLeaveType.Properties.Tag.ToString, "" & Data_FTLeaveType)
                .FNLeaveDay.SelectedIndex = HI.TL.CboList.GetIndexByValue(.FNLeaveDay.Properties.Tag.ToString, "" & Data_FTStaLeaveDay)
                ''  .FNLeaveDay_param.Text = Data_FTStaLeaveDay

                .FTSTime.Text = Data_FTLeaveStartTime
                .FTETime.Text = Data_FTLeaveEndTime
                '' .FTSTime_param.Text = Data_FTLeaveStartTime
                '' .FTETime_param.Text = Data_FTLeaveEndTime

                .FTStartDate.DateTime = Data_FTStartDate
                .FTEndDate.DateTime = Data_FTEndDate


                .FTStateCalSSo.Checked = Data_FTStaCalSSO
                .FTStateLeavepay.Checked = Data_FTLeavePay
                .FTStateDeductVacation.Checked = Data_FTStateDeductVacation



                .FTRemark.Text = Data_FTLeaveNote

                ''.FNLeaveType_param.Text = Data_FTLeaveType
                ''.FTStartDate_param.Text = Data_FTStartDate

                .FTStateMedicalCertificate.Checked = Data_FTStateMedicalCertificate
                .FTStateNotMergeHoliday.Checked = Data_FTHoliday
                '' .FTStateNotMergeHoliday_param.Checked = Data_FTHoliday
                ''  End With
                '' .WindowState = System.Windows.Forms.FormWindowState.Maximized
                '' .StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                .Load_PDF()

                .Show()
                HI.ST.SysInfo.MenuName = "MnuLeave"
                '.Activate()

            End With

        Catch ex As Exception
            Dim mx As String = ex.Message.ToString()
        End Try
    End Sub
End Class