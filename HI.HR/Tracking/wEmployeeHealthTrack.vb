Public Class wEmployeeHealthTrack
    Private oGridViewSizeSpec As DevExpress.XtraGrid.Views.Grid.GridView
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


        _Qry = "  SELECT      '0' AS FTSelect, E.FNHSysEmpID, E.FTEmpCode,E.FNEmpStatus"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + E.FTEmpNameTH + '  ' + E.FTEmpSurnameTH AS FTEmpName"
            _Qry &= vbCrLf & "  ,M.FTNameTH AS FTSexName "
            _Qry &= vbCrLf & "  ,C.FTCmpNameTH AS FTCmpName,C.FTAddr1TH AS FTAddr1C,C.FTDistrictTH AS FTDistrictC,C.FTSubDistrictTH AS FTSubDistrictC,C.FTProvinceTH AS FTProvinceC "
        Else
            _Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + E.FTEmpNameEN + '  ' + E.FTEmpSurnameEN AS FTEmpName"
            _Qry &= vbCrLf & "  ,M.FTNameEN AS FTSexName "
            _Qry &= vbCrLf & "  ,C.FTCmpNameEN AS FTCmpName,C.FTAddr1EN AS FTAddr1C,C.FTDistrictEN AS FTDistrictC,C.FTSubDistrictEN AS FTSubDistrictC,C.FTProvinceEN AS FTProvinceC "
        End If
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(E.FDBirthDate) =1 THEN  CONVERT(varchar(10),Convert(datetime,E.FDBirthDate),103) ELSE '' END AS FDBirthDate"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(E.FDDateStart) =1 THEN  CONVERT(varchar(10),Convert(datetime,E.FDDateStart),103) ELSE '' END AS FDDateStart"
        _Qry &= vbCrLf & ",E.FTEmpIdNo,E.FTAddrNo,E.FTAddrMoo,E.FTAddrSoi,E.FTAddrRoad,E.FTAddrTumbol,E.FTAddrAmphur,E.FTAddrProvince,E.FTAddrPostCode,E.FTAddrTel"
        _Qry &= vbCrLf & ",E.FTAddrNo1,E.FTAddrMoo1,E.FTAddrSoi1,E.FTAddrRoad1,E.FTAddrTumbol1,E.FTAddrAmphur1,E.FTAddrProvince1,E.FTAddrPostCode1,E.FTAddrTel1"
        _Qry &= vbCrLf & ",C.FTPostCode AS FTPostCodeC,C.FTPhone AS FTPhoneC"
        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E  WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON E.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON E.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON E.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON E.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON E.FNHSysPreNameId = P.FNHSysPreNameId"
        _Qry &= vbCrLf & "LEFT OUTER JOIN (   SELECT FNListIndex, FTNameTH, FTNameEN"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS X WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE  (FTListName = N'FNEmpSex')"
        _Qry &= vbCrLf & ") AS M ON  E.FNEmpSex=M.FNListIndex LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON E.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C  WITH (NOLOCK) ON E.FNHSysCmpId=C.FNHSysCmpId"
        _Qry &= vbCrLf & " WHERE  E.FNHSysEmpID<>''  "
        _Qry &= vbCrLf & "   AND  E.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "
        '--E.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & "  
        '------Criteria By Employeee Code
        If Me.FNHSysEmpTypeId.Text <> "" Then
            _Qry &= vbCrLf & " And ET.FTEmpTypeCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "'"
        End If

        If Me.FNHSysEmpId.Text <> "" Then
            _Qry &= vbCrLf & " AND E.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
        End If

        If Me.FNHSysEmpIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND E.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
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
            Case Else
                _Qry &= vbCrLf & " AND   E.FNEmpStatus=" & Val(FNEmpStatusReport.SelectedIndex - 1) & " "
        End Select

        _Qry &= vbCrLf & "ORDER BY E.FTEmpCode ASC"

        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        With Me.ogc
            .DataSource = _dt.Copy
            ogv.ExpandAllGroups()
            ogv.RefreshData()
        End With

        Call LoadExperience()
        ' Call Loadstricken()
        Call LoadStrick()
        Call Loadhealthcheck()
        Call Loadsickness()
        _dt.Dispose()

        Return True
    End Function
    Private Sub LoadExperience()
        Try
            Dim _Qry As String = ""


            _Qry = "  SELECT       E.FNHSysEmpID, E.FTEmpCode,E.FNEmpStatus"
            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + E.FTEmpNameTH + '  ' + E.FTEmpSurnameTH AS FTEmpName"
                _Qry &= vbCrLf & "  ,H.FTNameTH AS FTHealthRiskFactors,PE.FTNameTH AS FTProtectiveEquipment"
            Else
                _Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + E.FTEmpNameEN + '  ' + E.FTEmpSurnameEN AS FTEmpName"
                _Qry &= vbCrLf & "  ,H.FTNameEN AS FTHealthRiskFactors,PE.FTNameEN AS FTProtectiveEquipment"
            End If
            _Qry &= vbCrLf & ",EE.FNSeqNo,EE.FTCmpName AS tFTCmpName,EE.FTBusinessType,EE.FTResponsibility"
            _Qry &= vbCrLf & ",CASE WHEN ISDATE(EE.FDStartDate) =1 THEN  CONVERT(varchar(10),Convert(datetime,EE.FDStartDate),103) ELSE '' END AS FDStartDate"
            _Qry &= vbCrLf & ",CASE WHEN ISDATE(EE.FDEndDate) =1 THEN  CONVERT(varchar(10),Convert(datetime,EE.FDEndDate),103) ELSE '' END AS FDEndDate"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E  WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON E.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON E.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON E.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON E.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON E.FNHSysPreNameId = P.FNHSysPreNameId  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON E.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeExperience AS EE  WITH (NOLOCK) ON E.FNHSysEmpID=EE.FNHSysEmpID"
            _Qry &= vbCrLf & "LEFT OUTER JOIN (   SELECT FNListIndex, FTNameTH, FTNameEN"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE  (FTListName = N'FTHealthRiskFactors')"
            _Qry &= vbCrLf & ") AS H ON EE.FTHealthRiskFactors=H.FNListIndex"
            _Qry &= vbCrLf & "LEFT OUTER JOIN (   SELECT FNListIndex, FTNameTH, FTNameEN"
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE  (FTListName = N'FTProtectiveEquipment')"
            _Qry &= vbCrLf & ") AS PE ON EE.FTProtectiveEquipment=PE.FNListIndex"
            _Qry &= vbCrLf & " WHERE  E.FNHSysEmpID<>'' "
            _Qry &= vbCrLf & "   AND  E.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "

            '------Criteria By Employeee Code
            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & " And ET.FTEmpTypeCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "'"
            End If

            If Me.FNHSysEmpId.Text <> "" Then
                _Qry &= vbCrLf & " AND E.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
            End If

            If Me.FNHSysEmpIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND E.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
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
                Case Else
                    _Qry &= vbCrLf & " AND   E.FNEmpStatus=" & Val(FNEmpStatusReport.SelectedIndex - 1) & " "
            End Select
            _Qry &= vbCrLf & "ORDER BY E.FTEmpCode ASC,EE.FNSeqNo ASC"

            Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            With Me.ogctime
                .DataSource = _dt.Copy
                ogvtime.ExpandAllGroups()
                ogvtime.RefreshData()
            End With

        Catch ex As Exception
        End Try
    End Sub
    'Private Sub Loadstricken()
    '    Try
    '        Dim _Qry As String = ""


    '        _Qry = "  SELECT       E.FNHSysEmpID, E.FTEmpCode AS sFTEmpCode"
    '        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
    '            _Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + E.FTEmpNameTH + '  ' + E.FTEmpSurnameTH AS sFTEmpName"
    '        Else
    '            _Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + E.FTEmpNameEN + '  ' + E.FTEmpSurnameEN AS sFTEmpName"
    '        End If
    '        _Qry &= vbCrLf & ", S.FTSeqStricken,S.FTStricken,S.FTYear"
    '        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E  WITH (NOLOCK) LEFT OUTER JOIN"
    '        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON E.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
    '        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON E.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
    '        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON E.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
    '        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON E.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
    '        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON E.FNHSysPreNameId = P.FNHSysPreNameId  LEFT OUTER JOIN"
    '        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON E.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
    '        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_Stricken AS S  WITH (NOLOCK) ON E.FNHSysEmpID=S.FNHSysEmpID"
    '        _Qry &= vbCrLf & " WHERE  E.FNHSysEmpID<>'' AND S.FTStricken<>'' "

    '        '------Criteria By Employeee Code
    '        If Me.FNHSysEmpTypeId.Text <> "" Then
    '            _Qry &= vbCrLf & " And ET.FTEmpTypeCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "'"
    '        End If

    '        If Me.FNHSysEmpId.Text <> "" Then
    '            _Qry &= vbCrLf & " AND E.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
    '        End If

    '        If Me.FNHSysEmpIdTo.Text <> "" Then
    '            _Qry &= vbCrLf & " AND E.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
    '        End If

    '        '------Criteria By Department
    '        If Me.FNHSysDeptId.Text <> "" Then
    '            _Qry &= vbCrLf & " AND  Dept.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
    '        End If

    '        If Me.FNHSysDeptIdTo.Text <> "" Then
    '            _Qry &= vbCrLf & " AND  Dept.FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
    '        End If

    '        '------Criteria By Division
    '        If Me.FNHSysDivisonId.Text <> "" Then
    '            _Qry &= vbCrLf & " AND  DI.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
    '        End If

    '        If Me.FNHSysDivisonIdTo.Text <> "" Then
    '            _Qry &= vbCrLf & " AND  DI.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
    '        End If

    '        '------Criteria By Sect
    '        If Me.FNHSysSectId.Text <> "" Then
    '            _Qry &= vbCrLf & " AND  ST.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
    '        End If

    '        If Me.FNHSysSectIdTo.Text <> "" Then
    '            _Qry &= vbCrLf & " AND  ST.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
    '        End If

    '        '------Criteria Unit Sect
    '        If Me.FNHSysUnitSectId.Text <> "" Then
    '            _Qry &= vbCrLf & " AND   US.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
    '        End If

    '        If Me.FNHSysUnitSectIdTo.Text <> "" Then
    '            _Qry &= vbCrLf & " AND   US.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
    '        End If

    '        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

    '        With Me.ogcstricken
    '            .DataSource = _dt.Copy
    '            ogvstricken.ExpandAllGroups()
    '            ogvstricken.RefreshData()
    '        End With

    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Private Sub Loadstric()
    '    Try
    '        Dim _Qry As String = ""


    '        _Qry = "  SELECT       E.FNHSysEmpID, E.FTEmpCode AS ssFTEmpCode"
    '        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
    '            _Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + E.FTEmpNameTH + '  ' + E.FTEmpSurnameTH AS ssFTEmpName"
    '        Else
    '            _Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + E.FTEmpNameEN + '  ' + E.FTEmpSurnameEN AS ssFTEmpName"
    '        End If
    '        _Qry &= vbCrLf & ", H.FTStateNoDisease, H.FTStateDisease, H.FTDiseaseNote, H.FTStateNoSurgery, H.FTStateSurgery, H.FTSurgeryNote, H.FTStateNoImmunity, "
    '        _Qry &= vbCrLf & "H.FTStateImmunity, H.FTImmunityNote, H.FTStateNoSirFamily, H.FTStateSirFamily, H.FTRelationD, H.FTDiseaseD, H.FTRelationM, H.FTDiseaseM, H.FTRelationS, H.FTDiseaseS, H.FTStateNoDrugDisease, H.FTStateDrugDisease, H.FTDrugDiseaseNote,"
    '        _Qry &= vbCrLf & "H.FTStateNoHobby, H.FTStateHobby, H.FTHobbyNote, H.FTStateNoSmoking, H.FTSmoking, H.FTStateQuitSmoking, H.FTYearSmoking, H.FTMonthSmoking, H.FTSmokingQ, H.FTStateNoAlcohol, H.FTStateLessAlcohol, H.FTStateOneAlcohol,"
    '        _Qry &= vbCrLf & "H.FTStateThreeAlcohol, H.FTStateOverThreeAlcohol, H.FTStateQuitAlcohol, H.FTYearAlcohol, H.FTMonthAlcohol, H.FTStateNoDope, H.FTStateDope, H.FTDopeNote, H.FTOther, H.FTStateSmoking"

    '        _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E  WITH (NOLOCK) LEFT OUTER JOIN"
    '        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON E.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
    '        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON E.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
    '        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON E.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
    '        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON E.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
    '        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON E.FNHSysPreNameId = P.FNHSysPreNameId  LEFT OUTER JOIN"
    '        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON E.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
    '        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_HistoryOfIllness AS H WITH (NOLOCK) ON E.FNHSysEmpID=H.FNHSysEmpID"
    '        _Qry &= vbCrLf & " WHERE  E.FNHSysEmpID<>''  "

    '        '------Criteria By Employeee Code
    '        If Me.FNHSysEmpTypeId.Text <> "" Then
    '            _Qry &= vbCrLf & " And ET.FTEmpTypeCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "'"
    '        End If

    '        If Me.FNHSysEmpId.Text <> "" Then
    '            _Qry &= vbCrLf & " AND E.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
    '        End If

    '        If Me.FNHSysEmpIdTo.Text <> "" Then
    '            _Qry &= vbCrLf & " AND E.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
    '        End If

    '        '------Criteria By Department
    '        If Me.FNHSysDeptId.Text <> "" Then
    '            _Qry &= vbCrLf & " AND  Dept.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
    '        End If

    '        If Me.FNHSysDeptIdTo.Text <> "" Then
    '            _Qry &= vbCrLf & " AND  Dept.FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
    '        End If

    '        '------Criteria By Division
    '        If Me.FNHSysDivisonId.Text <> "" Then
    '            _Qry &= vbCrLf & " AND  DI.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
    '        End If

    '        If Me.FNHSysDivisonIdTo.Text <> "" Then
    '            _Qry &= vbCrLf & " AND  DI.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
    '        End If

    '        '------Criteria By Sect
    '        If Me.FNHSysSectId.Text <> "" Then
    '            _Qry &= vbCrLf & " AND  ST.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
    '        End If

    '        If Me.FNHSysSectIdTo.Text <> "" Then
    '            _Qry &= vbCrLf & " AND  ST.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
    '        End If

    '        '------Criteria Unit Sect
    '        If Me.FNHSysUnitSectId.Text <> "" Then
    '            _Qry &= vbCrLf & " AND   US.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
    '        End If

    '        If Me.FNHSysUnitSectIdTo.Text <> "" Then
    '            _Qry &= vbCrLf & " AND   US.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
    '        End If

    '        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

    '        With Me.ogcstric
    '            .DataSource = _dt.Copy
    '            ogvstrick.ExpandAllGroups()
    '            ogvstrick.RefreshData()
    '        End With

    '    Catch ex As Exception
    '    End Try
    'End Sub


    '--------------
    Private Function W_PRCbRemoveGridViewColumn(ByVal pGridView As DevExpress.XtraGrid.Views.Grid.GridView) As DevExpress.XtraGrid.Views.Grid.GridView
        Try

            With pGridView
                For nLoopColGridView As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case .Columns.Item(nLoopColGridView).Name.ToString.ToUpper
                        Case "FTOther".ToString.ToUpper
                            '...Do nothing
                            Exit For
                        Case Else
                            .Columns.Remove(.Columns.Item(nLoopColGridView))
                    End Select

                Next

            End With

            'pGridView.Columns.Clear()

        Catch ex As Exception
            ' MsgBox(ex.Message().ToString() & ControlChars.CrLf & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
        End Try

        Return pGridView
    End Function
    Private Sub LoadStrick()
        Me.ogcstric.DataSource = Nothing
        Me.ogcstric.Refresh()

        Call W_PRCbRemoveGridViewColumn(Me.ogvstrick)
        'Me.ogvtime.OptionsView.ColumnAutoWidth = False
        Me.ogvstrick.RefreshData()

        Try
            Dim _StartDate As String = ""
            Dim _EndDate As String = ""
            Dim _Qry As String = ""
            Dim _dt As DataTable
            Dim _TimeDT As DataTable

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"


            Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

            _Qry = "  SELECT       E.FNHSysEmpID, E.FTEmpCode AS ssFTEmpCode,E.FNEmpStatus"
            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + E.FTEmpNameTH + '  ' + E.FTEmpSurnameTH AS ssFTEmpName"
            Else
                _Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + E.FTEmpNameEN + '  ' + E.FTEmpSurnameEN AS ssFTEmpName"
            End If
            _Qry &= vbCrLf & ", H.FTStateNoDisease, H.FTStateDisease, H.FTDiseaseNote, H.FTStateNoSurgery, H.FTStateSurgery, H.FTSurgeryNote, H.FTStateNoImmunity, "
            _Qry &= vbCrLf & "H.FTStateImmunity, H.FTImmunityNote, H.FTStateNoSirFamily, H.FTStateSirFamily, H.FTRelationD, H.FTDiseaseD, H.FTRelationM, H.FTDiseaseM, H.FTRelationS, H.FTDiseaseS, H.FTStateNoDrugDisease, H.FTStateDrugDisease, H.FTDrugDiseaseNote,"
            _Qry &= vbCrLf & "H.FTStateNoHobby, H.FTStateHobby, H.FTHobbyNote, H.FTStateNoSmoking, H.FTSmoking, H.FTStateQuitSmoking, H.FTYearSmoking, H.FTMonthSmoking, H.FTSmokingQ, H.FTStateNoAlcohol, H.FTStateLessAlcohol, H.FTStateOneAlcohol,"
            _Qry &= vbCrLf & "H.FTStateThreeAlcohol, H.FTStateOverThreeAlcohol, H.FTStateQuitAlcohol, H.FTYearAlcohol, H.FTMonthAlcohol, H.FTStateNoDope, H.FTStateDope, H.FTDopeNote, H.FTOther, H.FTStateSmoking"

            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E  WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON E.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON E.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON E.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON E.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON E.FNHSysPreNameId = P.FNHSysPreNameId  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON E.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_HistoryOfIllness AS H WITH (NOLOCK) ON E.FNHSysEmpID=H.FNHSysEmpID"
            _Qry &= vbCrLf & " WHERE  E.FNHSysEmpID<>''  "
            _Qry &= vbCrLf & "   AND  E.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "

            '------Criteria By Employeee Code
            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & " And ET.FTEmpTypeCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "'"
            End If

            If Me.FNHSysEmpId.Text <> "" Then
                _Qry &= vbCrLf & " AND E.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
            End If

            If Me.FNHSysEmpIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND E.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
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
                Case Else
                    _Qry &= vbCrLf & " AND   E.FNEmpStatus=" & Val(FNEmpStatusReport.SelectedIndex - 1) & " "
            End Select
            _Qry &= vbCrLf & "ORDER BY E.FTEmpCode ASC"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)



            'Time DataTable
            _Qry = "  SELECT       E.FNHSysEmpID, E.FTEmpCode AS sFTEmpCode,E.FNEmpStatus"
            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + E.FTEmpNameTH + '  ' + E.FTEmpSurnameTH AS sFTEmpName"
            Else
                _Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + E.FTEmpNameEN + '  ' + E.FTEmpSurnameEN AS sFTEmpName"
            End If
            _Qry &= vbCrLf & ", S.FTSeqStricken,S.FTStricken,S.FTYear"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E  WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON E.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON E.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON E.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON E.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON E.FNHSysPreNameId = P.FNHSysPreNameId  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON E.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_Stricken AS S  WITH (NOLOCK) ON E.FNHSysEmpID=S.FNHSysEmpID"
            _Qry &= vbCrLf & " WHERE  E.FNHSysEmpID<>'' AND S.FTStricken<>'' "
            _Qry &= vbCrLf & "   AND  E.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "

            '------Criteria By Employeee Code
            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & " And ET.FTEmpTypeCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "'"
            End If

            If Me.FNHSysEmpId.Text <> "" Then
                _Qry &= vbCrLf & " AND E.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
            End If

            If Me.FNHSysEmpIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND E.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
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
                Case Else
                    _Qry &= vbCrLf & " AND   E.FNEmpStatus=" & Val(FNEmpStatusReport.SelectedIndex - 1) & " "
            End Select

            _Qry &= vbCrLf & "ORDER BY E.FTEmpCode ASC"

            _TimeDT = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Dim _ColCount As Integer = 0
            Dim _CountTable As DataRow()

            oGridViewSizeSpec = Me.ogcstric.Views(0)

            For Each _R As DataRow In _dt.Rows
                _CountTable = _TimeDT.Select("FNHSysEmpId = " & _R!FNHSysEmpId)
                If (_ColCount < _CountTable.Length) Then
                    _ColCount = _CountTable.Length
                End If
            Next

            For i As Integer = 1 To _ColCount
                Dim oColScanTime As DataColumn = New DataColumn("Stricken" & i, System.Type.GetType("System.String"))
                oColScanTime.Caption = "เคยป่วยเป็นโรค/พ.ศ. ครั้งที่" & i

                oGridViewSizeSpec.Columns.AddField(oColScanTime.ColumnName)
                oGridViewSizeSpec.Columns(oColScanTime.ColumnName).FieldName = oColScanTime.ColumnName
                oGridViewSizeSpec.Columns(oColScanTime.ColumnName).Name = oColScanTime.ColumnName
                oGridViewSizeSpec.Columns(oColScanTime.ColumnName).Caption = oColScanTime.Caption
                'oGridViewSizeSpec.Columns(oColScanTime.ColumnName).Tag = oRow.Item("FNHSysMatSizeId")
                oGridViewSizeSpec.Columns(oColScanTime.ColumnName).Visible = True
                oGridViewSizeSpec.Columns(oColScanTime.ColumnName).Width = 100
                oGridViewSizeSpec.Columns(oColScanTime.ColumnName).OptionsColumn.TabStop = False
                oGridViewSizeSpec.Columns(oColScanTime.ColumnName).OptionsColumn.AllowEdit = False
                oGridViewSizeSpec.Columns(oColScanTime.ColumnName).OptionsColumn.AllowMove = False
                oGridViewSizeSpec.Columns(oColScanTime.ColumnName).OptionsColumn.AllowSort = False

                _dt.Columns.Add(oColScanTime.ColumnName, oColScanTime.DataType)
            Next

            For Each _R As DataRow In _TimeDT.Rows
                For Each _Row As DataRow In _dt.Rows
                    Dim ColName As String
                    If (_Row!FNHSysEmpId = _R!FNHSysEmpId) Then
                        For i As Integer = 1 To _ColCount
                            ColName = "Stricken" & i
                            Dim _Text As String = _R!FTStricken.ToString & " เมื่อปี พ.ศ. " & _R!FTYear.ToString
                            ' _Text = _Text.Substring(0, 2) & ":" & _Text.Substring(2, 2)
                            If (IsDBNull(_Row(ColName))) Then
                                _Row(ColName) = _Text
                                Exit For
                            End If
                        Next
                    End If
                Next
            Next

            Me.ogcstric.DataSource = _dt
            Me.ogvstrick.BestFitColumns()
            ogvstrick.ExpandAllGroups()
            _Spls.Close()

            '_RowDataChange = False
        Catch
        End Try

    End Sub

    Private Sub Loadsickness()
        Try
            Dim _Qry As String = ""


            _Qry = "  SELECT       E.FNHSysEmpID, E.FTEmpCode AS kFTEmpCode,E.FNEmpStatus"
            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + E.FTEmpNameTH + '  ' + E.FTEmpSurnameTH AS kFTEmpName"
            Else
                _Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + E.FTEmpNameEN + '  ' + E.FTEmpSurnameEN AS kFTEmpName"
            End If
            _Qry &= vbCrLf & " ,W.FTCauseOfInjury,W.FTDisability,W.FTInjuredBody,W.FTLeaveWorkMore,W.FTLeaveWorkNiMore,W.FTLossOfSomeOrgans"
            _Qry &= vbCrLf & ", CASE WHEN ISDATE(W.FDWorkDate) = 1 THEN CONVERT(varchar(10), CONVERT(Datetime, W.FDWorkDate), 103) ELSE '' END AS FDWorkDate"
            _Qry &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E  WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON E.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON E.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON E.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON E.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON E.FNHSysPreNameId = P.FNHSysPreNameId  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON E.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_WorkSickness AS W WITH (NOLOCK) ON E.FNHSysEmpID=W.FNHSysEmpID"
            _Qry &= vbCrLf & " WHERE  E.FNHSysEmpID<>'' "
            _Qry &= vbCrLf & "   AND  E.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "

            '------Criteria By Employeee Code
            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & " And ET.FTEmpTypeCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "'"
            End If

            If Me.FNHSysEmpId.Text <> "" Then
                _Qry &= vbCrLf & " AND E.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
            End If

            If Me.FNHSysEmpIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND E.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
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
                Case Else
                    _Qry &= vbCrLf & " AND   E.FNEmpStatus=" & Val(FNEmpStatusReport.SelectedIndex - 1) & " "
            End Select
            _Qry &= vbCrLf & "ORDER BY E.FTEmpCode ASC"

            Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            With Me.ogcsickness
                .DataSource = _dt.Copy
                ogvsickness.ExpandAllGroups()
                ogvsickness.RefreshData()
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Loadhealthcheck()
        Try
            Dim _Qry As String = ""



            _Qry = "  SELECT       E.FNHSysEmpID, E.FTEmpCode AS hFTEmpCode,E.FNEmpStatus"
            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + E.FTEmpNameTH + '  ' + E.FTEmpSurnameTH AS hFTEmpName"
            Else
                _Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + E.FTEmpNameEN + '  ' + E.FTEmpSurnameEN AS hFTEmpName"
            End If
            _Qry &= vbCrLf & ",H.FNSeqNo AS hFNSeqNo,C.StateCheck,H.FTDoctorName, H.FTProfessionalNo,H.FTDoctorAddr, H.FTDoctorTel, H.FTStateNormal, H.FTStateNotNormal, H.FTNotNarmalNote, H.FTLaboratoryNote, H.FTRiskFactorOne, H.FTResultOne"
            _Qry &= vbCrLf & ", H.FTRiskFactorTwo, H.FTResultTwo, H.FTRiskFactorThree, H.FTResultThree, H.FTRiskFactorFour,H.FTResultFour, H.FTRiskFactorFive, H.FTResultFive, H.FTRiskFactorSix, H.FTResultSix"
            _Qry &= vbCrLf & ", H.FTRiskFactorSeven, H.FTResultSeven,HH.FCGenWeight,HH.FCGenHigh,HH.FCGenWeight/((HH.FCGenHigh/100)*(HH.FCGenHigh/100))AS FCGenBMI,HH.FCBloodPressure,HH.FNPulse"

            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E  WITH (NOLOCK) LEFT OUTER Join"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON E.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER Join"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON E.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER Join"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON E.FNHSysSectId = ST.FNHSysSectId LEFT OUTER Join"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON E.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER Join"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON E.FNHSysPreNameId = P.FNHSysPreNameId  LEFT OUTER Join"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON E.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER Join"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_HealthCheck AS H WITH (NOLOCK) ON E.FNHSysEmpID=H.FNHSysEmpID LEFT OUTER Join"
            _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHealthHistory AS HH WITH (NOLOCK) ON E.FNHSysEmpID=HH.FNHSysEmpID AND H.FNSeqNo=HH.FNSeqNo LEFT OUTER JOIN"
            _Qry &= vbCrLf & "( Select SS.FNHSysEmpID, SS.FNSeqNo, SS.StateCheck"
            _Qry &= vbCrLf & "From(Select H.FNHSysEmpID,'ตรวจครั้งแรก'AS StateCheck,H.FNSeqNo"
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_HealthCheck AS H  "
            _Qry &= vbCrLf & " Where H.FTStateFirstCheck ='1'"
            _Qry &= vbCrLf & "UNION"
            _Qry &= vbCrLf & "Select H.FNHSysEmpID,'ตรวจประจำปี'AS StateCheck,H.FNSeqNo"
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_HealthCheck AS H  "
            _Qry &= vbCrLf & "Where H.FTStateAnnualCheck ='1'"
            _Qry &= vbCrLf & " UNION"
            _Qry &= vbCrLf & "Select H.FNHSysEmpID,'ตรวจเมื่อเปลี่ยนงาน'AS StateCheck,H.FNSeqNo"
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_HealthCheck AS H  "
            _Qry &= vbCrLf & "Where H.FTStateCheckJobs ='1'"
            _Qry &= vbCrLf & " UNION"
            _Qry &= vbCrLf & "Select H.FNHSysEmpID,'ตรวจเฝ้าระวังตามความจำเป็น'AS StateCheck,H.FNSeqNo"
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_HealthCheck AS H  "
            _Qry &= vbCrLf & " Where H.FTStateSurveillance ='1'"
            _Qry &= vbCrLf & ")AS SS )AS C ON H.FNHSysEmpID=C.FNHSysEmpID And H.FNSeqNo=C.FNSeqNo"
            _Qry &= vbCrLf & " WHERE  E.FNHSysEmpID<>'' " ' AND H.FNSeqNo='" & HI.UL.ULF.rpQuoted(FNSeqNo.ToString) & "'  "
            _Qry &= vbCrLf & "   AND  E.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "
            '------Criteria By Employeee Code
            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & " And ET.FTEmpTypeCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "'"
            End If

            If Me.FNHSysEmpId.Text <> "" Then
                _Qry &= vbCrLf & " AND E.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
            End If

            If Me.FNHSysEmpIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND E.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
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
                Case Else
                    _Qry &= vbCrLf & " AND   E.FNEmpStatus=" & Val(FNEmpStatusReport.SelectedIndex - 1) & " "
            End Select

            _Qry &= vbCrLf & "ORDER BY  E.FTEmpCode ASC, H.FNSeqNo ASC"

            Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


            With Me.ogchealth
                .DataSource = _dt.Copy
                ogvhealth.ExpandAllGroups()
                ogvhealth.RefreshData()
            End With
        Catch ex As Exception
        End Try
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

        Try
            FNEmpStatusReport.SelectedIndex = 1
        Catch ex As Exception
        End Try

    End Sub

    Private Function VerifyData() As Boolean
        'If Me.FTDateStart.Text <> "" And Me.FTDateEnd.Text <> "" Then
        '    If Microsoft.VisualBasic.Right(Me.FTDateStart.Text, 4) = Microsoft.VisualBasic.Right(Me.FTDateEnd.Text, 4) Then
        '        Return True
        '    Else
        '        HI.MG.ShowMsg.mInfo("วันที่เริ่มต้นและวันที่สิ้นสุด ไม่ได้อยู่ในปีเดียวกันกรุณาทำการตรวจสอบ !!!", 15062655487, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        '        Return False
        '    End If
        'Else
        '    HI.MG.ShowMsg.mInfo("กรุณาทำการระบุวันที่เริ่มต้นแล้ะวันที่สิ้นสุด !!!", 15062655488, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        '    Return False
        'End If
    End Function

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click


        HI.TL.HandlerControl.ClearControl(Me)
        'HI.TL.HandlerControl.ClearControl(ogc)
        ogc.DataSource = Nothing
        ogctime.DataSource = Nothing
        ogcstric.DataSource = Nothing
        ogchealth.DataSource = Nothing
        ogcsickness.DataSource = Nothing
    End Sub

    Private Sub ocmload_Click(sender As System.Object, e As System.EventArgs) Handles ocmload.Click
        'If VerifyData() Then
        Dim _Spls As New HI.TL.SplashScreen("Loading data...   Please Wait  ")
        Me.LoadDataInfo(_Spls)
        Me.otbmain.SelectedTabPageIndex = 0
        _Spls.Close()
        'End If

    End Sub




#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogv)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    'Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
    '    With Me.ogvtime

    '        Dim _Spls As New HI.TL.SplashScreen("Generating data...   Please Wait  ")
    '        Me.LoadDataInfo(_Spls)
    '        Me.otbmain.SelectedTabPageIndex = 0
    '        _Spls.Close()

    '        Dim _Qry As String = ""

    '' _Qry = " {THRMEmployee.FNHSysEmpID} <> ''"
    ''_Qry &= vbCrLf & " AND  {THRMEmployee.FNHSysCmpId}=" & HI.ST.SysInfo.CmpID & ""
    '_Qry = "   {THRMEmployee.FNHSysCmpId}=" & HI.ST.SysInfo.CmpID & ""

    'If Me.FNHSysEmpTypeId.Text <> "" Then
    '    _Qry &= vbCrLf & "AND  {THRMEmpType.FTEmpTypeCode}='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "'"
    'End If

    ''------Criteria By Employeee Code
    'If Me.FNHSysEmpId.Text <> "" Then
    '    _Qry &= vbCrLf & " AND {THRMEmployee.FTEmpCode} >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
    'End If

    'If Me.FNHSysEmpIdTo.Text <> "" Then
    '    _Qry &= vbCrLf & " AND {THRMEmployee.FTEmpCode} <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
    'End If

    ''------Criteria By Department
    'If Me.FNHSysDeptId.Text <> "" Then
    '    _Qry &= vbCrLf & " AND  {TCNMDepartment.FTDeptCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
    'End If

    'If Me.FNHSysDeptIdTo.Text <> "" Then
    '    _Qry &= vbCrLf & " AND  {TCNMDepartment.FTDeptCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
    'End If

    ''------Criteria By Division
    'If Me.FNHSysDivisonId.Text <> "" Then
    '    _Qry &= vbCrLf & " AND  {TCNMDivision.FTDivisonCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
    'End If

    'If Me.FNHSysDivisonIdTo.Text <> "" Then
    '    _Qry &= vbCrLf & " AND  {TCNMDivision.FTDivisonCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
    'End If

    ''------Criteria By Sect
    'If Me.FNHSysSectId.Text <> "" Then
    '    _Qry &= vbCrLf & " AND  {TCNMSect.FTSectCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
    'End If

    'If Me.FNHSysSectIdTo.Text <> "" Then
    '    _Qry &= vbCrLf & " AND  {TCNMSect.FTSectCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
    'End If

    ''------Criteria Unit Sect
    'If Me.FNHSysUnitSectId.Text <> "" Then
    '    _Qry &= vbCrLf & " AND   {TCNMUnitSect.FTUnitSectCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
    'End If

    'If Me.FNHSysUnitSectIdTo.Text <> "" Then
    '    _Qry &= vbCrLf & " AND   {TCNMUnitSect.FTUnitSectCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
    'End If

    '        With Me.ogv

    '            If .RowCount > 0 Then

    '                Dim _AllEmpCode As String = ""

    '                For I As Integer = 0 To .RowCount - 1

    '                    If _AllEmpCode = "" Then
    '                        _AllEmpCode = "" & .GetRowCellValue(I, "FTEmpCode").ToString
    '                    Else
    '                        _AllEmpCode = _AllEmpCode & "','" & .GetRowCellValue(I, "FTEmpCode").ToString
    '                    End If

    '                Next

    '                _Qry = "  {THRMEmployee.FTEmpCode}  IN ['" & _AllEmpCode & "'] "

    '            End If

    '        End With

    '        With New HI.RP.Report
    '            .FormTitle = Me.Text
    '            .ReportFolderName = "Human Report\"
    '            .ReportName = "Health.rpt"
    '            .Formular = _Qry
    '            .Preview()
    '        End With

    '    End With
    'End Sub

    Private Sub ogv_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogv.RowCellStyle
        With ogv
            If .GetRowCellValue(e.RowHandle, "FNEmpStatus") = "2" Then
                e.Appearance.ForeColor = Drawing.Color.Red
            End If
        End With
    End Sub
    Private Sub ogvtime_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvtime.RowCellStyle
        With ogvtime
            If .GetRowCellValue(e.RowHandle, "FNEmpStatus") = "2" Then
                e.Appearance.ForeColor = Drawing.Color.Red
            End If
        End With
    End Sub
    Private Sub ogvsickness_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvsickness.RowCellStyle
        With ogvsickness
            If .GetRowCellValue(e.RowHandle, "FNEmpStatus") = "2" Then
                e.Appearance.ForeColor = Drawing.Color.Red
            End If
        End With
    End Sub
    Private Sub ogvstrick_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvstrick.RowCellStyle
        With ogvstrick
            If .GetRowCellValue(e.RowHandle, "FNEmpStatus") = "2" Then
                e.Appearance.ForeColor = Drawing.Color.Red
            End If
        End With
    End Sub
    Private Sub ogvhealth_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvhealth.RowCellStyle
        With ogvhealth
            If .GetRowCellValue(e.RowHandle, "FNEmpStatus") = "2" Then
                e.Appearance.ForeColor = Drawing.Color.Red
            End If
        End With
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        'If Me.FNHSysEmpId.Text <> "" And Me.FNHSysEmpId.Properties.Tag.ToString <> "" Then
        '    With New HI.RP.Report

        '        Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language


        '        Dim _Fm As String = "  {V_HREmpH.FNHSysCmpId}=" & HI.ST.SysInfo.CmpID & " "

        '        If Me.FNHSysEmpTypeId.Text <> "" Then
        '            _Fm &= vbCrLf & " AND {V_HREmpH.FTEmpTypeCode}='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
        '        End If

        '        '------Criteria By Employeee Code
        '        If Me.FNHSysEmpId.Text <> "" Then
        '            _Fm &= vbCrLf & " AND {V_HREmpH.FTEmpCode} >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
        '        End If

        '        If Me.FNHSysEmpIdTo.Text <> "" Then
        '            _Fm &= vbCrLf & " AND {V_HREmpH.FTEmpCode} <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
        '        End If

        '        '------Criteria By Department
        '        If Me.FNHSysDeptId.Text <> "" Then
        '            _Fm &= vbCrLf & " AND  {V_HREmpH.FTDeptCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
        '        End If

        '        If Me.FNHSysDeptIdTo.Text <> "" Then
        '            _Fm &= vbCrLf & " AND  {V_HREmpH.FTDeptCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
        '        End If

        '        '------Criteria By Division
        '        If Me.FNHSysDivisonId.Text <> "" Then
        '            _Fm &= vbCrLf & " AND  {V_HREmpH.FTDivisonCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
        '        End If

        '        If Me.FNHSysDivisonIdTo.Text <> "" Then
        '            _Fm &= vbCrLf & " AND  {V_HREmpH.FTDivisonCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
        '        End If

        '        '------Criteria By Sect
        '        If Me.FNHSysSectId.Text <> "" Then
        '            _Fm &= vbCrLf & " AND  {V_HREmpH.FTSectCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
        '        End If

        '        If Me.FNHSysSectIdTo.Text <> "" Then
        '            _Fm &= vbCrLf & " AND  {V_HREmpH.FTSectCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
        '        End If

        '        '------Criteria Unit Sect
        '        If Me.FNHSysUnitSectId.Text <> "" Then
        '            _Fm &= vbCrLf & " AND   {V_HREmpH.FTUnitSectCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
        '        End If

        '        If Me.FNHSysUnitSectIdTo.Text <> "" Then
        '            _Fm &= vbCrLf & " AND   {V_HREmpH.FTUnitSectCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
        '        End If







        '        .FormTitle = Me.Text
        '        .ReportFolderName = "Human Report\"
        '        .ReportName = "HealthNew.rpt"
        '        '.AddParameter("Draft", "DRAFT")
        '        .Formular = _Fm ' "{V_HREmpH.FTEmpCode}='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "'"
        '        .Preview()


        '        '.FormTitle = Me.Text
        '        '.ReportFolderName = "Human Report\"
        '        '.ReportName = "Health3.rpt"
        '        ''.AddParameter("Draft", "DRAFT")
        '        '.Formular = _Fm '"{V_HREmpH.FTEmpCode}='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "'"
        '        '.Preview()


        '        '.FormTitle = Me.Text
        '        '.ReportFolderName = "Human Report\"
        '        '.ReportName = "Health2.rpt"
        '        ''.AddParameter("Draft", "DRAFT")
        '        '.Formular = _Fm ' "{V_HREmpH.FTEmpCode}='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "'"
        '        '.Preview()


        '        '.FormTitle = Me.Text
        '        '.ReportFolderName = "Human Report\"
        '        '.ReportName = "Health1.rpt"
        '        ''.AddParameter("Draft", "DRAFT")
        '        '.Formular = _Fm '"{V_HREmpH.FTEmpCode}='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "'"
        '        '.Preview()


        '        '.FormTitle = Me.Text
        '        '.ReportFolderName = "Human Report\"
        '        '.ReportName = "Health.rpt"
        '        ''.AddParameter("Draft", "DRAFT")
        '        '.Formular = _Fm ' "{V_HREmpH.FTEmpCode}='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "'"
        '        '.Preview()


        '        HI.ST.Lang.Language = _tmplang
        '    End With
        'Else
        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FNHSysEmpId_lbl.Text)
        '    FNHSysEmpId.Focus()
        'End If

        Dim dt As DataTable
        dt = CType(ogc.DataSource, DataTable)
        If dt.Select("FTSelect='1' ").Length = 1 Then

            Dim QRY As String = ""
            QRY = "SELECT FTEmpCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee"
            Dim Num As Boolean = True
            If dt.Select("FTSelect='1' ").Length > 0 Then
                For Each R As DataRow In dt.Select("FTSelect='1' ")
                    If Num = True Then
                        QRY &= vbCrLf & " WHERE "
                    Else
                        QRY &= vbCrLf & " OR "
                    End If

                    QRY &= vbCrLf & "FTEmpCode = '" & HI.UL.ULF.rpQuoted(R!FTEmpCode.ToString) & "'"
                    Num = False
                Next
            End If
            dt = HI.Conn.SQLConn.GetDataTable(QRY, Conn.DB.DataBaseName.DB_FIXED)

            For Each R As DataRow In dt.Rows
                If R!FTEmpCode.ToString <> "" Then
                    With New HI.RP.Report
                        .FormTitle = Me.Text
                        .ReportFolderName = "Human Report\"
                        .ReportName = "HealthNew.rpt"
                        '.AddParameter("Draft", "DRAFT")
                        .Formular = "{V_HREmpH.FTEmpCode}='" & HI.UL.ULF.rpQuoted(R!FTEmpCode.ToString) & "'"
                        .Preview()
                    End With
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, R!FTEmpCode.ToString)

                End If
            Next

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text)
        End If
    End Sub

    Private Sub otbmain_Click(sender As Object, e As EventArgs) Handles otbmain.Click

    End Sub


    'Private Sub LoadHealth(Key As Object)
    '    Dim _Qry As String = ""
    '    Dim _dtprod As DataTable
    '    otbhealth.TabPages.Clear()

    '    _Qry = "SELECT  DISTINCT ISNULL(H.FNSeqNo,'0')AS  FNSeqNo "
    '    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E  WITH (NOLOCK) LEFT OUTER JOIN"
    '    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee_HealthCheck AS H  WITH (NOLOCK) ON E.FNHSysEmpID=H.FNHSysEmpID LEFT OUTER JOIN"
    '    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON E.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
    '    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON E.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
    '    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON E.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
    '    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON E.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
    '    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON E.FNHSysPreNameId = P.FNHSysPreNameId  LEFT OUTER JOIN"
    '    _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON E.FNHSysEmpTypeId = ET.FNHSysEmpTypeId"
    '    _Qry &= vbCrLf & " WHERE  E.FNHSysEmpID<>'' AND H.FNSeqNo   <>'0'  "
    '    If Me.FNHSysEmpTypeId.Text <> "" Then
    '        _Qry &= vbCrLf & " And ET.FTEmpTypeCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "'"
    '    End If

    '    If Me.FNHSysEmpId.Text <> "" Then
    '        _Qry &= vbCrLf & " AND E.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
    '    End If

    '    If Me.FNHSysEmpIdTo.Text <> "" Then
    '        _Qry &= vbCrLf & " AND E.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
    '    End If

    '    '------Criteria By Department
    '    If Me.FNHSysDeptId.Text <> "" Then
    '        _Qry &= vbCrLf & " AND  Dept.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
    '    End If

    '    If Me.FNHSysDeptIdTo.Text <> "" Then
    '        _Qry &= vbCrLf & " AND  Dept.FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
    '    End If

    '    '------Criteria By Division
    '    If Me.FNHSysDivisonId.Text <> "" Then
    '        _Qry &= vbCrLf & " AND  DI.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
    '    End If

    '    If Me.FNHSysDivisonIdTo.Text <> "" Then
    '        _Qry &= vbCrLf & " AND  DI.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
    '    End If

    '    '------Criteria By Sect
    '    If Me.FNHSysSectId.Text <> "" Then
    '        _Qry &= vbCrLf & " AND  ST.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
    '    End If

    '    If Me.FNHSysSectIdTo.Text <> "" Then
    '        _Qry &= vbCrLf & " AND  ST.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
    '    End If

    '    '------Criteria Unit Sect
    '    If Me.FNHSysUnitSectId.Text <> "" Then
    '        _Qry &= vbCrLf & " AND   US.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
    '    End If

    '    If Me.FNHSysUnitSectIdTo.Text <> "" Then
    '        _Qry &= vbCrLf & " AND   US.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
    '    End If

    '    _dtprod = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)

    '    For Each R As DataRow In _dtprod.Rows

    '        Dim Otp As New DevExpress.XtraTab.XtraTabPage()
    '        With Otp
    '            .Name = R!FNSeqNo.ToString
    '            .Text = R!FNSeqNo.ToString
    '        End With

    '        otbhealth.TabPages.Add(Otp)

    '    Next

    '    If _dtprod.Rows.Count > 0 Then
    '        otbhealth.SelectedTabPageIndex = 0
    '    End If

    '    _dtprod.Dispose()
    'End Sub
    'Private Sub otbhealth_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs)
    '    Try
    '        If (Me.InvokeRequired) Then
    '            Me.Invoke(New HI.Delegate.Dele.XtraTab_SelectedPageChanged(AddressOf otbhealth_SelectedPageChanged), New Object() {sender, e})
    '        Else
    '            If Not (otbhealth.SelectedTabPage Is Nothing) Then
    '                Call Loadhealthcheck(otbhealth.SelectedTabPage.Name.ToString)
    '            Else
    '                Me.ogchealth.DataSource = Nothing
    '            End If
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

End Class