Public Class wEmployeeExpiry



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
        Dim _Str As String = ""
        Dim _Ex As String = ""
        Dim _ST As String = ""
        Dim _D1 As String = ""
        Dim _D2 As String = ""
        Dim _D3 As String = ""
        Dim _D4 As String = ""
        Dim _D5 As String = ""
           Dim _D6 As String = ""
        Dim _FNYear As Integer = 0
        Dim _FNMonth As Integer = 0
        Dim _FNDay As Integer = 0
        Dim _FNYear1 As Integer = 0
        Dim _FNMonth1 As Integer = 0
        Dim _FNDay1 As Integer = 0

        Dim _StrDate As String = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.GetOnServer(Conn.DB.DataBaseName.DB_SYSTEM))
        Dim _Year As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 4), 2)
        Dim _Month As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 7), 2)
        Dim _Day As String = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StrDate, 10), 2)


        _Qry &= vbCrLf & "SELECT AA.FNHSysEmpID, AA.FTEmpCode"
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
        _Qry &= vbCrLf & ",EX.FTPassPortNo AS FTPassPostNo,EX.FDDateofIssue"
        _Qry &= vbCrLf & ",EX.FTWorkPermitNo,EX.FDDateValid"
        _Qry &= vbCrLf & ",EX.FTVisaNo AS FTVisasNo,EX.FDDateVisas"
        _Qry &= vbCrLf & ",EX.FTMOUNo AS FTMOUDoccument,EX.FDDateMOU"
        _Qry &= vbCrLf & ",EX.FTExpiry,EX.FTExpiryWork,EX.FTExpiryVisa,EX.FTExpiryMOU"
        _Qry &= vbCrLf & " ,EX.FDDateofExpiry,EX.FDDateUntil,EX.FDDateVisasExpiry,EX.FDDateMOUex"

        _Qry &= vbCrLf & " FROM"

        _Qry &= vbCrLf & "(SELECT       M.FNHSysEmpID, M.FTEmpCode,M.FNEmpStatus, M.FNHSysCmpId"
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
        _Qry &= vbCrLf & "FROM"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN" 'ON TL.FNHSysEmpID=M.FNHSysEmpID 
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS OrgPosit WITH (NOLOCK) ON M.FNHSysPositId = OrgPosit.FNHSysPositId "


        _Qry = vbCrLf & "" & HI.ST.Security.PermissionFilterEmployee(_Qry) & ""

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "GROUP BY M.FNHSysEmpID, M.FTEmpCode,P.FTPreNameNameTH,M.FTEmpNameTH,M.FTEmpSurnameTH,ET.FTEmpTypeNameTH,M.FNEmpStatus, M.FNHSysCmpId"
            _Qry &= vbCrLf & ",Dept.FTDeptDescTH,DI.FTDivisonNameTH,ST.FTSectNameTH,US.FTUnitSectNameTH,OrgPosit.FTPositNameTH"
            _Qry &= vbCrLf & ",ET.FTEmpTypeCode ,Dept.FTDeptCode,ST.FTSectCode, OrgPosit.FTPositCode,M.FTEmpCodeRefer,DI.FTDivisonCode,US.FTUnitSectCode ) AS AA"
        Else
            _Qry &= vbCrLf & "GROUP BY M.FNHSysEmpID, M.FTEmpCode,P.FTPreNameNameEN,M.FTEmpNameEN,M.FTEmpSurnameEN,ET.FTEmpTypeNameEN,M.FNEmpStatus, M.FNHSysCmpId"
            _Qry &= vbCrLf & ",Dept.FTDeptDescEN,DI.FTDivisonNameEN,ST.FTSectNameEN,US.FTUnitSectNameEN,OrgPosit.FTPositNameEN"
            _Qry &= vbCrLf & ",ET.FTEmpTypeCode ,Dept.FTDeptCode,ST.FTSectCode, OrgPosit.FTPositCode,M.FTEmpCodeRefer,DI.FTDivisonCode,US.FTUnitSectCode) AS AA"
        End If
        _Qry &= vbCrLf & "LEFT OUTER JOIN"
        _Qry &= vbCrLf & "(SELECT  E.FTEmpCode,'" & _StrDate & "' as FDDate,  ISNULL((DAY(P.FDDateofExpiry) - DAY('" & _StrDate & "' )+ (month(P.FDDateofExpiry) - month('" & _StrDate & "' ))*30)  +((year(P.FDDateofExpiry) - year('" & _StrDate & "' ))*365-1),0) AS FTExpiry"
        _Qry &= vbCrLf & ", ISNULL((DAY(W.FDDateofExpiry) - DAY('" & _StrDate & "' )+ (month(W.FDDateofExpiry) - month('" & _StrDate & "' ))*30) +((year(W.FDDateofExpiry) - year('" & _StrDate & "' ))*365-1),0) AS FTExpiryWork, ISNULL((DAY(V.FDDateofExpiry) - DAY('" & _StrDate & "')+ (month(V.FDDateofExpiry) - month('" & _StrDate & "'))*30)+((year(V.FDDateofExpiry) - year('" & _StrDate & "' ))*365-1),0) AS FTExpiryVisa"
        _Qry &= vbCrLf & ", ISNULL((DAY(M.FDDateofExpiry) - DAY('" & _StrDate & "')+ (month(M.FDDateofExpiry) - month('" & _StrDate & "'))*30)+((year(M.FDDateofExpiry) - year('" & _StrDate & "' ))*365-1),0) AS FTExpiryMOU"
        _Qry &= vbCrLf & ", ISNULL((DAY(O.FDDateofExpiry) - DAY('" & _StrDate & "')+ (month(O.FDDateofExpiry) - month('" & _StrDate & "'))*30)+((year(O.FDDateofExpiry) - year('" & _StrDate & "' ))*365-1),0) AS FTExpiryOther"
        _Qry &= vbCrLf & ",M.FTMOUNo,P.FTPassPortNo,W.FTWorkpermitNo,V.FTVisaNo,O.FTFileOtherNo"
        _Qry &= vbCrLf & " ,case when isdate(P.FDDateofIssue) = 1 then CONVERT(varchar(10),convert(datetime,P.FDDateofIssue),103) else '' end AS FDDateofIssue"
        _Qry &= vbCrLf & ",case when isdate(P.FDDateofExpiry) = 1 then CONVERT(varchar(10),convert(datetime,P.FDDateofExpiry),103) else '' end AS FDDateofExpiry"
        _Qry &= vbCrLf & ",case when isdate(W.FDDateofIssue) = 1 then CONVERT(varchar(10),convert(datetime,W.FDDateofIssue),103) else '' end AS FDDateValid"
        _Qry &= vbCrLf & ",case when isdate(W.FDDateofExpiry) = 1 then CONVERT(varchar(10),convert(datetime,W.FDDateofExpiry),103) else '' end AS FDDateUntil"
        _Qry &= vbCrLf & ",case when isdate(V.FDDateofIssue) = 1 then CONVERT(varchar(10),convert(datetime,V.FDDateofIssue),103) else '' end AS FDDateVisas"
        _Qry &= vbCrLf & ",case when isdate(V.FDDateofExpiry) = 1 then CONVERT(varchar(10),convert(datetime,V.FDDateofExpiry),103) else '' end AS FDDateVisasExpiry"
        _Qry &= vbCrLf & ",case when isdate(M.FDDateofIssue) = 1 then CONVERT(varchar(10),convert(datetime,M.FDDateofIssue),103) else '' end AS FDDateMOU"
        _Qry &= vbCrLf & ",case when isdate(M.FDDateofExpiry) = 1 then CONVERT(varchar(10),convert(datetime,M.FDDateofExpiry),103) else '' end AS FDDateMOUex"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK)  "

        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_MOU AS M ON E.FNHSysEmpID=M.FNHSysEmpID LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_Passport AS P ON E.FNHSysEmpID=P.FNHSysEmpID LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_Workpermit AS W ON E.FNHSysEmpID=W.FNHSysEmpID LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_Visa AS V ON E.FNHSysEmpID=V.FNHSysEmpID LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_FileOther AS O ON E.FNHSysEmpID=O.FNHSysEmpID"

        _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 1  FDDateofIssue, FDDateofExpiry , FTMOUNo FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_MOU MOU  WHERE MOU.FNHSysEmpID=E.FNHSysEmpID ORDER BY FNMOUSeq DESC   ) M"
        _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 1  FDDateofIssue,FDDateofExpiry , FTPassPortNo FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_Passport Passport WHERE Passport.FNHSysEmpID=E.FNHSysEmpID ORDER BY FNPassportSeq DESC )P"
        _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 1 FDDateofIssue,FDDateofExpiry , FTWorkpermitNo FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_Workpermit Workpermit WHERE Workpermit.FNHSysEmpID=E.FNHSysEmpID ORDER BY FNWorkpermitSeq DESC ) W"
        _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 1 FDDateofIssue,FDDateofExpiry , FTVisaNo FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_Visa Visa WHERE Visa.FNHSysEmpID=E.FNHSysEmpID ORDER BY FNVisaSeq DESC ) V"
        _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 1 FDDateofIssue,FDDateofExpiry , FTFileOtherNo FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_FileOther FileOther WHERE FileOther.FNHSysEmpID=E.FNHSysEmpID ORDER BY FNFileOtherSeq DESC ) O"


        _Qry &= vbCrLf & "WHERE   E.FTEmpCode<>''"

        If Me.FDDateofIssue.Text <> "" Then
            _Qry &= vbCrLf & "AND P.FTPassPortNo<> ''"
            _Qry &= vbCrLf & "AND P.FDDateofIssue>='" & HI.UL.ULDate.ConvertEnDB(FDDateofIssue.Text) & "'"
        End If
        If Me.FDDateofExpiry.Text <> "" Then
            _Qry &= vbCrLf & "AND P.FDDateofExpiry<='" & HI.UL.ULDate.ConvertEnDB(FDDateofExpiry.Text) & "'"
        End If
        '------Criteria By DateWorkPermitNo
        If Me.FDDateValid.Text <> "" Then
            _Qry &= vbCrLf & "AND W.FTWorkpermitNo<> ''"
            _Qry &= vbCrLf & "AND W.FDDateofIssue>='" & HI.UL.ULDate.ConvertEnDB(FDDateValid.Text) & "'"
        End If
        If Me.FDDateUntil.Text <> "" Then
            _Qry &= vbCrLf & "AND W.FDDateofExpiry<='" & HI.UL.ULDate.ConvertEnDB(FDDateUntil.Text) & "'"
        End If
        '------Criteria By DateMOUNo
        If Me.FDDateMOUe.Text <> "" Then
            _Qry &= vbCrLf & "AND  M.FTMOUNo<> ''"
            _Qry &= vbCrLf & "AND M.FDDateofIssue>='" & HI.UL.ULDate.ConvertEnDB(FDDateMOUe.Text) & "'"
        End If
        If Me.FDDateMOUexTo.Text <> "" Then
            _Qry &= vbCrLf & "AND M.FDDateofExpiry<='" & HI.UL.ULDate.ConvertEnDB(FDDateMOUexTo.Text) & "'"
        End If
        '------Criteria By DateVisaNo
        If Me.FDDateVisa.Text <> "" Then
            _Qry &= vbCrLf & "AND V.FTVisaNo<> ''"
            _Qry &= vbCrLf & "AND V.FDDateofIssue>='" & HI.UL.ULDate.ConvertEnDB(FDDateVisa.Text) & "'"
        End If
        If Me.FDDateVisasExp.Text <> "" Then
            _Qry &= vbCrLf & "AND V.FDDateofExpiry<='" & HI.UL.ULDate.ConvertEnDB(FDDateVisasExp.Text) & "'"
        End If
        _Qry &= vbCrLf & " )AS EX ON AA.FTEmpCode=EX.FTEmpCode"

        _Qry &= vbCrLf & "WHERE (AA.FNEmpStatus<>2)  "

        '------Criteria By Between DAte


        If Me.FNHSysEmpTypeId.Text <> "" Then
            _Qry &= vbCrLf & "AND AA.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "'"
        End If

        '------Criteria By Employeee Code
        If Me.FNHSysEmpId.Text <> "" Then
            _Qry &= vbCrLf & " AND AA.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
        End If

        If Me.FNHSysEmpIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND AA.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
        End If

        '------Criteria By PassPostNo
        If Me.FTPassPostNo.Text <> "" Then
            _Qry &= vbCrLf & "AND EX.FTPassPortNo<>'' "
            _Qry &= vbCrLf & " AND  EX.FTPassPortNo>='" & HI.UL.ULF.rpQuoted(Me.FTPassPostNo.Text) & "' "
        End If

        If Me.FTPassPostNoTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  EX.FTPassPortNo<='" & HI.UL.ULF.rpQuoted(Me.FTPassPostNoTo.Text) & "' "
        End If

        '------Criteria By WorkPermitNo
        If Me.FTWorkPermitNo.Text <> "" Then
            _Qry &= vbCrLf & "AND EX.FTWorkpermitNo<> ''"
            _Qry &= vbCrLf & " AND  EX.FTWorkPermitNo>='" & HI.UL.ULF.rpQuoted(Me.FTWorkPermitNo.Text) & "' "
        End If

        If Me.FTWorkPermitNoTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  EX.FTWorkPermitNo<='" & HI.UL.ULF.rpQuoted(Me.FTWorkPermitNoTo.Text) & "' "
        End If

        '------Criteria By MOUNo
        If Me.FTMOUNo.Text <> "" Then
            _Qry &= vbCrLf & " AND EX.FTMOUNo<> ''"
            _Qry &= vbCrLf & " AND  EX.FTMOUNo>='" & HI.UL.ULF.rpQuoted(Me.FTMOUNo.Text) & "' "
        End If

        If Me.FTMOUNoTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  EX.FTMOUNo<='" & HI.UL.ULF.rpQuoted(Me.FTMOUNoTo.Text) & "' "
        End If

        '------Criteria By VisaNo
        If Me.FTVisaNo.Text <> "" Then
            _Qry &= vbCrLf & " AND  EX.FTVisaNo<> ''"
            _Qry &= vbCrLf & " AND  EX.FTVisaNo>='" & HI.UL.ULF.rpQuoted(Me.FTVisaNo.Text) & "' "
        End If

        If Me.FTVisaNoTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  EX.FTVisaNo<='" & HI.UL.ULF.rpQuoted(Me.FTVisaNoTo.Text) & "' "
        End If

        If Me.FDDateofIssue.Text <> "" Then
            _Qry &= vbCrLf & "AND EX.FTPassPortNo<> ''"
        End If
        
        '------Criteria By DateWorkPermitNo
        If Me.FDDateValid.Text <> "" Then
            _Qry &= vbCrLf & "AND EX.FTWorkpermitNo<> ''"
        End If
        
        '------Criteria By DateMOUNo
        If Me.FDDateMOUe.Text <> "" Then
            _Qry &= vbCrLf & "AND  EX.FTMOUNo<> ''"
        End If
       
        '------Criteria By DateVisaNo
        If Me.FDDateVisa.Text <> "" Then
            _Qry &= vbCrLf & "AND EX.FTVisaNo<> ''"
        End If
        

        With Me.ogc
            .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            ogv.ExpandAllGroups()
            ogv.RefreshData()
        End With
        Return True
    End Function
#End Region

    '#Region "Initial Grid"
    '    Private Sub InitGrid()
    '        '------Start Add Summary Grid-------------
    '        With ogv


    '            '.Columns("FTEmpTypeCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTEmpTypeCode")
    '            '.Columns("FTDeptCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTDeptCode")
    '            '.Columns("FTDivisonCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTDivisonCode")
    '            '.Columns("FTSectCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTSectCode")
    '            '.Columns("FTUnitSectCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTUnitSectCode")

    '            '.Columns("FTEmpTypeCode").Group()
    '            '.Columns("FTSectCode").Group()
    '            '.Columns("FTEmpStatusName").Group()

    '            .Columns("FTEmpCode").Summary.Add(DevExpress.Data.SummaryItemType.Count, "FTEmpCode")
    '            .Columns("FTEmpCode").SummaryItem.DisplayFormat = "{0:n0}"
    '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "FTEmpCode", Nothing, "(Count by " & .Columns.ColumnByFieldName("FTEmpCode").Caption & "={0:n0})")
    '            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
    '            .OptionsView.ShowFooter = True
    '            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
    '            .OptionsView.ShowGroupPanel = True
    '            .ExpandAllGroups()

    '            .RefreshData()

    '        End With
    '        '------End Add Summary Grid-------------
    '    End Sub
    '#End Region
#Region "General"
    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Dim _Spls As New HI.TL.SplashScreen("Loading data...   Please Wait  ")
        Me.LoadDataInfo(_Spls)
        _Spls.Close()
    End Sub


    Private Sub wEmployeeExpiry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        ' Call InitGrid()0
    End Sub
    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        'HI.TL.HandlerControl.ClearControl(ogc)
        ogc.DataSource = Nothing
    End Sub
#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogv)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ogv_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogv.RowCellStyle
        Try
            With Me.ogv
                Select Case e.Column.FieldName
                    Case "FTExpiry,FTExpiryWork,FTExpiryVisa,FTExpiryMOU"
                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FTExpiry,FTExpiryWork,FTExpiryVisa,FTExpiryMOU")) > 60 Then
                            e.Appearance.BackColor = System.Drawing.Color.LemonChiffon
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                        Else
                            e.Appearance.BackColor = System.Drawing.Color.Red
                            e.Appearance.ForeColor = System.Drawing.Color.Blue
                        End If


                End Select

            End With
        Catch ex As Exception

        End Try
    End Sub


End Class