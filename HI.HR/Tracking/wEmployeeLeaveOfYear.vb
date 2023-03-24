Public Class wEmployeeLeaveOfYear



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
    Private Function LoadDataInfo(Spls As HI.TL.SplashScreen) As Boolean
        Dim _Qry As String = ""

        _Qry = "SELECT AA.FNHSysEmpID, AA.FTEmpCode"
        _Qry &= vbCrLf & ",AA.FTEmpName"
        _Qry &= vbCrLf & ",AA.FTEmpTypeName"
        _Qry &= vbCrLf & ",AA.FTDeptName"
        _Qry &= vbCrLf & ",AA.FTDivisonName"
        _Qry &= vbCrLf & ",AA.FTSectName "
        _Qry &= vbCrLf & ",AA.FTUnitSectName "
        _Qry &= vbCrLf & ",AA.FTPositName "
        _Qry &= vbCrLf & ",AA.FTPositCode"
        _Qry &= vbCrLf & ",AA.FTEmpTypeCode"
        _Qry &= vbCrLf & ",AA.FTDeptCode"
        _Qry &= vbCrLf & ",AA.FTDivisonCode"
        _Qry &= vbCrLf & ",AA.FTSectCode"
        _Qry &= vbCrLf & ",AA.FTUnitSectCode"

        _Qry &= vbCrLf & ",  (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(AA.FTLeaveType0 / 480.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((AA.FTLeaveType0 % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((AA.FTLeaveType0 % 480.00) % 60.00))),2))  AS FTLeaveType0"

        _Qry &= vbCrLf & ",  (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(AA.FTLeaveType1 / 480.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((AA.FTLeaveType1 % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((AA.FTLeaveType1 % 480.00) % 60.00))),2))  AS FTLeaveType1"

        _Qry &= vbCrLf & ",  (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(AA.FTLeaveType4 / 480.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((AA.FTLeaveType4 % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((AA.FTLeaveType4 % 480.00) % 60.00))),2))  AS FTLeaveType4"

        _Qry &= vbCrLf & ",  (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(AA.FTLeaveType5 / 480.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((AA.FTLeaveType5 % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((AA.FTLeaveType5 % 480.00) % 60.00))),2))  AS FTLeaveType5"

        _Qry &= vbCrLf & ",  (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(AA.FTLeaveType6 / 480.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((AA.FTLeaveType6 % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((AA.FTLeaveType6 % 480.00) % 60.00))),2))  AS FTLeaveType6"

        _Qry &= vbCrLf & ",  (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(AA.FTLeaveType7 / 480.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((AA.FTLeaveType7 % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((AA.FTLeaveType7 % 480.00) % 60.00))),2))  AS FTLeaveType7"

        _Qry &= vbCrLf & ",  (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(AA.FTLeaveType8 / 480.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((AA.FTLeaveType8 % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((AA.FTLeaveType8 % 480.00) % 60.00))),2))  AS FTLeaveType8"

        _Qry &= vbCrLf & ",  (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(AA.FTLeaveType9 / 480.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((AA.FTLeaveType9 % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((AA.FTLeaveType9 % 480.00) % 60.00))),2))  AS FTLeaveType9"

        _Qry &= vbCrLf & ",  (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(AA.FTLeaveType97 / 480.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((AA.FTLeaveType97 % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((AA.FTLeaveType97 % 480.00) % 60.00))),2))  AS FTLeaveType97"

        _Qry &= vbCrLf & ",  (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(AA.FTLeaveType98 / 480.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((AA.FTLeaveType98 % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((AA.FTLeaveType98 % 480.00) % 60.00))),2))  AS FTLeaveType98"

        _Qry &= vbCrLf & ",  (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(AA.FTLeaveType99 / 480.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((AA.FTLeaveType99 % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((AA.FTLeaveType99 % 480.00) % 60.00))),2))  AS FTLeaveType99"

        _Qry &= vbCrLf & ",  (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(AA.FTLeaveType2 / 480.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((AA.FTLeaveType2 % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((AA.FTLeaveType2 % 480.00) % 60.00))),2))  AS FTLeaveType2"

        _Qry &= vbCrLf & ",  (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(AA.FTLeaveType17 / 480.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((AA.FTLeaveType17 % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((AA.FTLeaveType17 % 480.00) % 60.00))),2))  AS FTLeaveType17"


        _Qry &= vbCrLf & ",  (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(AA.FNAbsent / 480.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((AA.FNAbsent % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((AA.FNAbsent % 480.00) % 60.00))),2))  AS FNAbsent"

        _Qry &= vbCrLf & ",  (Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor(AA.Late / 480.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),Floor((AA.Late % 480.00) / 60.00))),2)"
        _Qry &= vbCrLf & "+':'+  Right('00' + Convert(varchar(30),Convert(numeric(18,0),((AA.Late % 480.00) % 60.00))),2))  AS Late"

        _Qry &= vbCrLf & " FROM"

        _Qry &= vbCrLf & "(SELECT       M.FNHSysEmpID, M.FTEmpCode"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"
            _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameTH  AS FTEmpTypeName "
            _Qry &= vbCrLf & "  ,Dept.FTDeptDescTH  AS FTDeptName "
            _Qry &= vbCrLf & "  ,DI.FTDivisonNameTH  AS FTDivisonName "
            _Qry &= vbCrLf & "  ,ST.FTSectNameTH  AS FTSectName "
            _Qry &= vbCrLf & "  ,US.FTUnitSectNameTH  AS FTUnitSectName "
            _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameTH  AS FTPositName "
        Else
            _Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpName"
            _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameEN  AS FTEmpTypeName "
            _Qry &= vbCrLf & "  ,Dept.FTDeptDescEN  AS FTDeptName "
            _Qry &= vbCrLf & "  ,DI.FTDivisonNameEN  AS FTDivisonName "
            _Qry &= vbCrLf & "  ,ST.FTSectNameEN  AS FTSectName "
            _Qry &= vbCrLf & "  ,US.FTUnitSectNameEN  AS FTUnitSectName "
            _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameEN  AS FTPositName "
        End If
        _Qry &= vbCrLf & ", ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
        _Qry &= vbCrLf & ", ISNULL(Dept.FTDeptCode,'') AS FTDeptCode, ISNULL(DI.FTDivisonCode,'') AS FTDivisonCode"
        _Qry &= vbCrLf & ", ISNULL(ST.FTSectCode,'') AS FTSectCode,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode"
        _Qry &= vbCrLf & ", OrgPosit.FTPositCode"
        _Qry &= vbCrLf & ",Isnull(SUM(CASE WHEN TL.FTLeaveType ='0' THEN FNTotalMinute  END),0) AS [FTLeaveType0]"
        _Qry &= vbCrLf & ",Isnull(SUM(CASE WHEN TL.FTLeaveType ='1' THEN FNTotalMinute  END),0) AS [FTLeaveType1]"
        _Qry &= vbCrLf & ",Isnull(SUM(CASE WHEN TL.FTLeaveType ='4' THEN FNTotalMinute  END),0) AS [FTLeaveType4]"
        _Qry &= vbCrLf & ",Isnull(SUM(CASE WHEN TL.FTLeaveType ='5' THEN FNTotalMinute  END),0) AS [FTLeaveType5]"
        _Qry &= vbCrLf & ",Isnull(SUM(CASE WHEN TL.FTLeaveType ='6' THEN FNTotalMinute  END),0) AS [FTLeaveType6]"
        _Qry &= vbCrLf & ",Isnull(SUM(CASE WHEN TL.FTLeaveType ='7' THEN FNTotalMinute  END),0) AS [FTLeaveType7]"
        _Qry &= vbCrLf & ",Isnull(SUM(CASE WHEN TL.FTLeaveType ='8' THEN FNTotalMinute  END),0) AS [FTLeaveType8]"
        _Qry &= vbCrLf & ",Isnull(SUM(CASE WHEN TL.FTLeaveType ='9' THEN FNTotalMinute  END),0) AS [FTLeaveType9]"
        _Qry &= vbCrLf & ",Isnull(SUM(CASE WHEN TL.FTLeaveType ='97' THEN FNTotalMinute  END),0) AS [FTLeaveType97]"
        _Qry &= vbCrLf & ",Isnull(SUM(CASE WHEN TL.FTLeaveType ='98' THEN FNTotalMinute  END),0) AS [FTLeaveType98]"
        _Qry &= vbCrLf & ",Isnull(SUM(CASE WHEN TL.FTLeaveType ='99' THEN FNTotalMinute  END),0) AS [FTLeaveType99]"
        _Qry &= vbCrLf & ",Isnull(SUM(CASE WHEN TL.FTLeaveType ='2' THEN FNTotalMinute  END),0) AS [FTLeaveType2]"
        _Qry &= vbCrLf & ",Isnull(SUM(CASE WHEN TL.FTLeaveType ='17' THEN FNTotalMinute  END),0) AS [FTLeaveType17]"

        _Qry &= vbCrLf & ",Isnull(SUM(CASE WHEN TL.FTLeaveType ='888' THEN FNTotalMinute  END),0) AS [FNAbsent]"
        _Qry &= vbCrLf & ",Isnull(SUM(CASE WHEN TL.FTLeaveType ='999' THEN FNTotalMinute  END),0) AS [Late]"
        _Qry &= vbCrLf & "FROM (SELECT        FNHSysEmpID, FTDateTrans, FTLeaveType, FNTotalMinute"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRTTransLeave"
        _Qry &= vbCrLf & "union"
        _Qry &= vbCrLf & "SELECT        FNHSysEmpID, FTDateTrans,'888'  , FNAbsent "
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "]..THRTTrans"
        _Qry &= vbCrLf & "union"
        _Qry &= vbCrLf & "SELECT        FNHSysEmpID, FTDateTrans, '999'  , FNLateNormalMin"
        _Qry &= vbCrLf & "FROM THRTTrans"
        _Qry &= vbCrLf & ") AS TL LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M ON TL.FNHSysEmpID=M.FNHSysEmpID INNER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS OrgPosit WITH (NOLOCK) ON M.FNHSysPositId = OrgPosit.FNHSysPositId "



        _Qry &= vbCrLf & "where (M.FNEmpStatus<>2)"

        '------Criteria By Between DAte
        If Me.Start_Date.Text <> "" Then
            _Qry &= vbCrLf & "AND TL.FTDateTrans>='" & HI.UL.ULDate.ConvertEnDB(Start_Date.Text) & "'"
        End If
        If Me.End_Date.Text <> "" Then
            _Qry &= vbCrLf & "AND TL.FTDateTrans<='" & HI.UL.ULDate.ConvertEnDB(End_Date.Text) & "'"
        End If

        If Me.FNHSysEmpTypeId.Text <> "" Then
            _Qry &= vbCrLf & "AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "'"
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

        _Qry = vbCrLf & "" & HI.ST.Security.PermissionFilterEmployee(_Qry) & ""

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "GROUP BY M.FNHSysEmpID, M.FTEmpCode,P.FTPreNameNameTH,M.FTEmpNameTH,M.FTEmpSurnameTH,ET.FTEmpTypeNameTH"
            _Qry &= vbCrLf & ",Dept.FTDeptDescTH,DI.FTDivisonNameTH,ST.FTSectNameTH,US.FTUnitSectNameTH,OrgPosit.FTPositNameTH"
            _Qry &= vbCrLf & ",ET.FTEmpTypeCode ,Dept.FTDeptCode,ST.FTSectCode, OrgPosit.FTPositCode,M.FTEmpCodeRefer,DI.FTDivisonCode,US.FTUnitSectCode) AS AA"
        Else
            _Qry &= vbCrLf & "GROUP BY M.FNHSysEmpID, M.FTEmpCode,P.FTPreNameNameEN,M.FTEmpNameEN,M.FTEmpSurnameEN,ET.FTEmpTypeNameEN"
            _Qry &= vbCrLf & ",Dept.FTDeptDescEN,DI.FTDivisonNameEN,ST.FTSectNameEN,US.FTUnitSectNameEN,OrgPosit.FTPositNameEN"
            _Qry &= vbCrLf & ",ET.FTEmpTypeCode ,Dept.FTDeptCode,ST.FTSectCode, OrgPosit.FTPositCode,M.FTEmpCodeRefer,DI.FTDivisonCode,US.FTUnitSectCode) AS AA"
        End If
        With Me.ogc
            .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            ogv.ExpandAllGroups()
            ogv.RefreshData()
        End With
        Return True
    End Function
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

            '.Columns("FTEmpTypeCode").Group()
            '.Columns("FTSectCode").Group()
            '.Columns("FTEmpStatusName").Group()

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
    Private Sub wEmployeeLeaveOfYear_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Call InitGrid()

        HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)


        'Try
        '    FNEmpStatusReport.SelectedIndex = 1
        'Catch ex As Exception
        'End Try



    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        'HI.TL.HandlerControl.ClearControl(ogc)
        ogc.DataSource = Nothing
    End Sub

    Private Sub ocmload_Click(sender As System.Object, e As System.EventArgs) Handles ocmload.Click
        Dim _Spls As New HI.TL.SplashScreen("Loading data...   Please Wait  ")
        Me.LoadDataInfo(_Spls)
        _Spls.Close()
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