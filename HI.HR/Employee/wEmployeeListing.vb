Public Class wEmployeeListing 
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

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

#Region "Procedure"
    Private Sub LoadDataInfo()

        Dim _Qry  As  String = ""
        _Qry = " SELECT A.* "
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry = _Qry & ",Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge -(A.FNMonthWorkAge % 12) )  /12)) + ' ปี ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge % 12  )) ) + ' เดือน ' + convert(nvarchar(30), A.FNMonthWorkAgeDay) +' วัน' AS FTWorkAge "
            _Qry = _Qry & ",Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge -(A.FNMonthEmpAge % 12) )  /12)) + ' ปี ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge % 12  )) ) + ' เดือน' AS FTEmpAge "
        Else
            _Qry = _Qry & ",Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge -(A.FNMonthWorkAge % 12) )  /12)) + ' Year ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge % 12  )) ) + ' Month ' + convert(nvarchar(30) , A.FNMonthWorkAgeDay) +' Day'  AS FTWorkAge "
            _Qry = _Qry & ",Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge -(A.FNMonthEmpAge % 12) )  /12)) + ' Year ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge % 12  )) ) + ' Month' AS FTEmpAge "
        End If

        _Qry = _Qry & " FROM ( SELECT       M.FNHSysEmpID, M.FTEmpCode"


        _Qry = _Qry & "    , M.FTEmpNicknameTH , M.FTEmpNicknameEN "

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  ,P.FTPreNameNameTH AS FTPreNameName"
            '_Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"
            _Qry &= vbCrLf & "  ,M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"
            _Qry &= vbCrLf & "  ,M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpNameEN"
            _Qry &= vbCrLf & "  ,ES.FTNameTH  AS FTEmpStatusName "
            _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameTH  AS FTEmpTypeName "
            _Qry &= vbCrLf & "  ,Dept.FTDeptDescTH  AS FTDeptName "
            _Qry &= vbCrLf & "  ,DI.FTDivisonNameTH  AS FTDivisonName "
            _Qry &= vbCrLf & "  ,ST.FTSectNameTH  AS FTSectName "
            _Qry &= vbCrLf & "  ,US.FTUnitSectNameTH  AS FTUnitSectName "
            _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameTH  AS FTPositName "
            _Qry &= vbCrLf & "  ,MNT.FTNationalityNameTH  AS FTNationalityName "
            _Qry &= vbCrLf & "  ,MSX.FTNameTH  AS FTSexName "
            _Qry &= vbCrLf & "  ,MSXTT.FTNameTH  AS FTStateCalSocial "

            _Qry &= vbCrLf & " ,'' AS FTWorkforceType"
            '' _Qry &= vbCrLf & " ,POS.FTNameTH AS FTWorkforceType"

            _Qry &= vbCrLf & "  ,CLev_Org.FTCLevelNameTH AS FTCLevelName_Org "
            _Qry &= vbCrLf & "  ,Dept_Org.FTDeptDescTH  AS FTDeptName_Org "
            _Qry &= vbCrLf & "  ,DI_Org.FTDivisonNameTH  AS FTDivisonName_Org "
            _Qry &= vbCrLf & "  ,ST_Org.FTSectNameTH  AS FTSectName_Org "
            _Qry &= vbCrLf & "  ,US_Org.FTUnitSectNameTH  AS FTUnitSectName_Org "
            _Qry &= vbCrLf & "  ,OrgPosit_Org.FTPositNameTH  AS FTPositName_Org "

            _Qry &= vbCrLf & "  ,FNJobLevelNameTH  AS FNJobLevelName "
            _Qry &= vbCrLf & "  ,FNJobRoleNameTH  AS FNJobRoleName "
            _Qry &= vbCrLf & " , FTMobile "
            _Qry &= vbCrLf & " , FTAddrTel1 "
            _Qry &= vbCrLf & "  , L.FTNameTH AS FTEmpTypeGroup , FTAccNo ,FTTaxNo, FTSocialNo "


            _Qry &= vbCrLf & " ,ACG.FTAccountGroupCode, ACG.FTAccountGroupNameTH AS [FTAccountGroupName] "
            _Qry &= vbCrLf & " ,ACG_Org.FTAccountGroupCode  [FTAccountGroupCode_Org], ACG_Org.FTAccountGroupNameTH AS [FTAccountGroupName_Org] "

            _Qry &= vbCrLf & "   ,FormatType.FTNameTH AS FTFormatType "
        Else
            _Qry &= vbCrLf & "  ,P.FTPreNameNameEN AS FTPreNameName"
            '_Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpName"
            _Qry &= vbCrLf & "  ,M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"
            _Qry &= vbCrLf & "  ,M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpNameEN"
            _Qry &= vbCrLf & "  ,ES.FTNameEN  AS FTEmpStatusName "
            _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameEN  AS FTEmpTypeName "
            _Qry &= vbCrLf & "  ,Dept.FTDeptDescEN  AS FTDeptName "
            _Qry &= vbCrLf & "  ,DI.FTDivisonNameEN  AS FTDivisonName "
            _Qry &= vbCrLf & "  ,ST.FTSectNameEN  AS FTSectName "
            _Qry &= vbCrLf & "  ,US.FTUnitSectNameEN  AS FTUnitSectName "
            _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameEN  AS FTPositName "
            _Qry &= vbCrLf & "  ,MNT.FTNationalityNameEN  AS FTNationalityName "
            _Qry &= vbCrLf & "  ,MSX.FTNameEN  AS FTSexName "
            _Qry &= vbCrLf & "  ,MSXTT.FTNameEN  AS FTStateCalSocial "

            _Qry &= vbCrLf & " ,'' AS FTWorkforceType"
            '' _Qry &= vbCrLf & " ,POS.FTNameEN AS FTWorkforceType"

            _Qry &= vbCrLf & "  ,CLev_Org.FTCLevelNameEN AS FTCLevelName_Org "
            _Qry &= vbCrLf & "  ,Dept_Org.FTDeptDescEN  AS FTDeptName_Org "
            _Qry &= vbCrLf & "  ,DI_Org.FTDivisonNameEN  AS FTDivisonName_Org "
            _Qry &= vbCrLf & "  ,ST_Org.FTSectNameEN  AS FTSectName_Org "
            _Qry &= vbCrLf & "  ,US_Org.FTUnitSectNameEN  AS FTUnitSectName_Org "
            _Qry &= vbCrLf & "  ,OrgPosit_Org.FTPositNameEN  AS FTPositName_Org "

            _Qry &= vbCrLf & "  ,FNJobLevelNameEN  AS FNJobLevelName "
            _Qry &= vbCrLf & "  ,FNJobRoleNameEN  AS FNJobRoleName "
            _Qry &= vbCrLf & " , FTMobile "
            _Qry &= vbCrLf & " , FTAddrTel1 "
            _Qry &= vbCrLf & "  , L.FTNameEN AS FTEmpTypeGroup , FTAccNo "
            _Qry &= vbCrLf & " ,ACG.FTAccountGroupCode, ACG.FTAccountGroupNameEN AS [FTAccountGroupName] "
            _Qry &= vbCrLf & " ,ACG_Org.FTAccountGroupCode  [FTAccountGroupCode_Org], ACG_Org.FTAccountGroupNameEN AS [FTAccountGroupName_Org] "

            _Qry &= vbCrLf & "   ,FormatType.FTNameEN AS FTFormatType "

        End If



        _Qry &= vbCrLf & " , ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
        _Qry &= vbCrLf & " , ISNULL(Dept.FTDeptCode,'') AS FTDeptCode, ISNULL(DI.FTDivisonCode,'') AS FTDivisonCode"
        _Qry &= vbCrLf & " , ISNULL(ST.FTSectCode,'') AS FTSectCode,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode"
        _Qry &= vbCrLf & " , OrgPosit.FTPositCode, M.FNEmpStatus,M.FTEmpCodeRefer,M.FTEmpIdNo"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(M.FDDateStart) =1 THEN CONVERT(varchar(10),Convert(datetime,M.FDDateStart),103) ELSE '' END AS FDDateStart"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(M.FDDateEnd) =1 THEN CONVERT(varchar(10),Convert(datetime,M.FDDateEnd),103) ELSE '' END AS FDDateEnd"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(M.FDBirthDate) =1 THEN CONVERT(varchar(10),Convert(datetime,M.FDBirthDate),103) ELSE '' END AS FDBirthDate"


        _Qry &= vbCrLf & ",CASE WHEN ISDATE(M.FDDateProbation) =1 THEN CONVERT(varchar(10),Convert(datetime,M.FDDateProbation),103) ELSE '' END AS FDDateProbation"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(M.FDDateTransfer) =1 THEN CONVERT(varchar(10),Convert(datetime,M.FDDateTransfer),103) ELSE '' END AS FDDateTransfer"


        _Qry &= vbCrLf & " , [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd) AS FNMonthWorkAge"
        _Qry &= vbCrLf & " , [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_Get_Emp_WorkAge_Day(M.FDDateStart,M.FDDateEnd) AS FNMonthWorkAgeDay"
        _Qry &= vbCrLf & " , [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_Get_Emp_Age(M.FDBirthDate) AS FNMonthEmpAge"

        _Qry &= vbCrLf & ",M.FTAddrProvince AS FTEmpProvince"
        _Qry &= vbCrLf & ",M.FTAddrProvince1 AS FTEmpProvinceCurrent,M.FTEmpBarcode"


        _Qry &= vbCrLf & " ,M.FTAddrNo1 , M.FTAddrMoo1, M.FTAddrHome1 , M.FTAddrSoi1, M.FTAddrRoad1 , M.FTAddrTumbol1, M.FTAddrAmphur1 , M.FTAddrProvince1, M.FTAddrPostCode1,CASE WHEN M.FTStateEnablon ='1' THEN '1' ELSE '0' END AS FTStateEnablon "
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",ISNULL(FNEnablonType.FNEnablonTypeNameTH,'-') AS FNEnablonType"
        Else
            _Qry &= vbCrLf & ",ISNULL(FNEnablonType.FNEnablonTypeNameEN,'-') AS FNEnablonType"
        End If

        _Qry &= vbCrLf & ",ISNULL(FTStateUnionMember,'0') AS FTStateUnionMember"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(M.FDRegisterDateUnion) =1 THEN CONVERT(varchar(10),Convert(datetime,M.FDRegisterDateUnion),103) ELSE '' END AS FDRegisterDateUnion"
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ", UNI.FTNameTH AS FTUnion  "
        Else
            _Qry &= vbCrLf & ", UNI.FTNameEN AS FTUnion  "
        End If

        _Qry &= vbCrLf & ",CASE WHEN ISDATE(M.FDDateIdNoEnd) =1 THEN CONVERT(varchar(10),Convert(datetime,M.FDDateIdNoEnd),103) ELSE '' END AS FDDateIdNoEnd"

        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS OrgPosit WITH (NOLOCK) ON M.FNHSysPositId = OrgPosit.FNHSysPositId LEFT OUTER JOIN"

        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPositionGrp AS G WITH(NOLOCK) ON OrgPosit.FNHSysPositGrpId = G.FNHSysPositGrpId "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMAccountGroup AS ACG WITH(NOLOCK) ON US.FNHSysAccountGroupId = ACG.FNHSysAccountGroupId "

        _Qry &= vbCrLf & "  OUTER APPLY ( SELECT FNListIndex AS 'FNEnablonType', FTNameTH AS 'FNEnablonTypeNameTH', FTNameEN 'FNEnablonTypeNameEN' FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData "
        _Qry &= vbCrLf & "  WHERE FTListName  ='FNEnablonType' AND M.FNEnablonType=FNListIndex)   FNEnablonType"
        _Qry &= vbCrLf & "  OUTER APPLY ( SELECT FNListIndex AS 'FNJobLevelIndex', FTNameTH AS 'FNJobLevelNameTH', FTNameEN 'FNJobLevelNameEN' FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData "
        _Qry &= vbCrLf & "  WHERE FTListName  ='FNJobLevel' AND G.FNJobLevel=FNListIndex)   FNJobLevel"
        _Qry &= vbCrLf & "  OUTER APPLY ( SELECT FNListIndex AS 'FNJobRoleIndex', FTNameTH AS 'FNJobRoleNameTH', FTNameEN 'FNJobRoleNameEN' FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData "
        _Qry &= vbCrLf & " WHERE FTListName  ='FNJobRole' AND G.FNJobRole=FNListIndex)   FNJobRole    "




        _Qry &= vbCrLf & "   LEFT OUTER JOIN  "
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept_Org WITH (Nolock) ON M.FNHSysDeptIdOrg = Dept_Org.FNHSysDeptId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI_Org WITH (NOLOCK) ON M.FNHSysDivisonIdOrg = DI_Org.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST_Org WITH (NOLOCK) ON M.FNHSysSectIdOrg = ST_Org.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US_Org WITH (NOLOCK) ON M.FNHSysUnitSectIdOrg = US_Org.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS OrgPosit_Org WITH (NOLOCK) ON M.FNHSysPositIdOrg = OrgPosit_Org.FNHSysPositId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCLevel AS CLev_Org WITH (NOLOCK) ON M.FNHSysCLevelIdOrg = CLev_Org.FNHSysCLevelId LEFT OUTER JOIN"



        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MEmpStatus AS ES ON M.FNEmpStatus = ES.FNListIndex  LEFT OUTER JOIN "
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMNationality AS MNT WITH (NOLOCK) ON M.FNHSysNationalityId = MNT.FNHSysNationalityId"

        _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMAccountGroup AS ACG_Org WITH(NOLOCK) ON US_Org.FNHSysAccountGroupId = ACG_Org.FNHSysAccountGroupId "

        _Qry &= vbCrLf & " LEFT OUTER JOIN ("
        _Qry &= vbCrLf & "    SELECT FNListIndex, FTNameTH, FTNameEN"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS X WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE  (FTListName = N'FNEmpSex')"
        _Qry &= vbCrLf & " ) AS MSX ON  M.FNEmpSex=MSX.FNListIndex "


        '_Qry &= vbCrLf & " LEFT OUTER JOIN ("
        '_Qry &= vbCrLf & "    SELECT FNListIndex, FTNameTH, FTNameEN"
        '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS X WITH(NOLOCK)"
        '_Qry &= vbCrLf & " WHERE  (FTListName = N'FNWorkForceType')"
        '_Qry &= vbCrLf & " )AS POS ON  OrgPosit.FNWorkForceType=POS.FNListIndex  "

        _Qry &= vbCrLf & " LEFT OUTER JOIN ("
        _Qry &= vbCrLf & "    SELECT FNListIndex, FTNameTH, FTNameEN"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS X WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE  (FTListName = N'FNCalSocialSta')"
        _Qry &= vbCrLf & " ) AS MSXTT ON  M.FNCalSocialSta=MSXTT.FNListIndex "


        _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].[dbo].[HSysListData] WHERE FTListName='FNEmpTypeGroup' AND ET.FNEmpTypeGroup=FNListIndex ) L "

        _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].[dbo].[HSysListData] WHERE FTListName='FNUnion' AND M.FNUnion=FNListIndex ) UNI "


        _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 1 * FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].[dbo].[HSysListData] WHERE FTListName='FNEmployeeFormatType' AND OrgPosit.FNEmployeeFormatType=FNListIndex ) FormatType "

        _Qry &= vbCrLf & "  WHERE        (M.FTEmpCode <> '')"
        _Qry &= vbCrLf & "   AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "

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

        Select Case FNEmpStatusReport.SelectedIndex
            Case 0
            Case 4
                _Qry &= vbCrLf & " AND   M.FNEmpStatus IN (0,1) "
            Case Else
                _Qry &= vbCrLf & " AND   M.FNEmpStatus=" & Val(FNEmpStatusReport.SelectedIndex - 1) & " "
        End Select

        _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)


        _Qry = _Qry & " ) AS A "

        With Me.ogc
            .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            ogv.ExpandAllGroups()
            ogv.RefreshData()
        End With

    End Sub
#End Region



#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        With ogv

            '.Columns("FTEmpTypeCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTEmpTypeCode")
            '.Columns("FTDeptCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTDeptCode")
            '.Columns("FTDivisonCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTDivisonCode")
            '.Columns("FTSectCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTSectCode")
            '.Columns("FTUnitSectCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTUnitSectCode")

            .Columns("FTEmpTypeCode").Group()
            .Columns("FTSectCode").Group()
            .Columns("FTEmpStatusName").Group()

            .Columns("FTEmpCode").Summary.Add(DevExpress.Data.SummaryItemType.Count, "FTEmpCode")
            .Columns("FTEmpCode").SummaryItem.DisplayFormat = "{0:n0}"
            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "FTEmpCode", Nothing, "(Count by " & .Columns.ColumnByFieldName("FTEmpCode").Caption & "={0:n0})")
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

#Region "General"
    Private Sub wEmployeeListing_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Call InitGrid()

        HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)


        Try

            FNEmpStatusReport.SelectedIndex = 4
        Catch ex As Exception
        End Try

        Call Me.LoadDataInfo()

    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub ocmload_Click(sender As System.Object, e As System.EventArgs) Handles ocmload.Click
        Me.LoadDataInfo()
    End Sub

    Private Sub ogv_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogv.RowCellStyle
        With ogv
            If .GetRowCellValue(e.RowHandle, "FNEmpStatus") = "2" Then
                e.Appearance.ForeColor = Drawing.Color.Red
            End If
        End With
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogv)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

End Class