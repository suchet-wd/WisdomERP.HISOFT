Public Class wEmployeeBI
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

        Dim _Qry As String = ""
        _Qry = " SELECT A.* "
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry = _Qry & ",Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge -(A.FNMonthWorkAge % 12) )  /12)) + ' ปี ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge % 12  )) ) + ' เดือน' AS FTWorkAge "
            _Qry = _Qry & ",Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge -(A.FNMonthEmpAge % 12) )  /12)) + ' ปี ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge % 12  )) ) + ' เดือน' AS FTEmpAge "
        Else
            _Qry = _Qry & ",Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge -(A.FNMonthWorkAge % 12) )  /12)) + ' Year ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge % 12  )) ) + ' Month' AS FTWorkAge "
            _Qry = _Qry & ",Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge -(A.FNMonthEmpAge % 12) )  /12)) + ' Year ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge % 12  )) ) + ' Month' AS FTEmpAge "
        End If

        _Qry = _Qry & " FROM ( SELECT       M.FNHSysEmpID, M.FTEmpCode"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  ,P.FTPreNameNameTH AS FTPreNameName"
            '_Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"
            _Qry &= vbCrLf & "  ,M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"
            _Qry &= vbCrLf & "  ,ES.FTNameTH  AS FTEmpStatusName "
            _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameTH  AS FTEmpTypeName "
            _Qry &= vbCrLf & "  ,Dept.FTDeptDescTH  AS FTDeptName "
            _Qry &= vbCrLf & "  ,DI.FTDivisonNameTH  AS FTDivisonName "
            _Qry &= vbCrLf & "  ,ST.FTSectNameTH  AS FTSectName "
            _Qry &= vbCrLf & "  ,US.FTUnitSectNameTH  AS FTUnitSectName "
            _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameTH  AS FTPositName "
            _Qry &= vbCrLf & "  ,MNT.FTNationalityNameTH  AS FTNationalityName "
            _Qry &= vbCrLf & "  ,MSX.FTNameTH  AS FTSexName "
            _Qry &= vbCrLf & "  ,EMPRe.FTReligionNameTH AS FTReligionName"
            _Qry &= vbCrLf & "  ,EmpRace.FTRaceNameTH AS FTRaceName"
            _Qry &= vbCrLf & "  ,MSXTT.FTNameTH  AS FTStateCalSocial "

        Else
            _Qry &= vbCrLf & "  ,P.FTPreNameNameEN AS FTPreNameName"
            '_Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpName"
            _Qry &= vbCrLf & "  ,M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpName"
            _Qry &= vbCrLf & "  ,ES.FTNameEN  AS FTEmpStatusName "
            _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameEN  AS FTEmpTypeName "
            _Qry &= vbCrLf & "  ,Dept.FTDeptDescEN  AS FTDeptName "
            _Qry &= vbCrLf & "  ,DI.FTDivisonNameEN  AS FTDivisonName "
            _Qry &= vbCrLf & "  ,ST.FTSectNameEN  AS FTSectName "
            _Qry &= vbCrLf & "  ,US.FTUnitSectNameEN  AS FTUnitSectName "
            _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameEN  AS FTPositName "
            _Qry &= vbCrLf & "  ,MNT.FTNationalityNameEN  AS FTNationalityName "
            _Qry &= vbCrLf & "  ,MSX.FTNameEN  AS FTSexName "
            _Qry &= vbCrLf & "  ,EMPRe.FTReligionNameEN AS FTReligionName"
            _Qry &= vbCrLf & "  ,EmpRace.FTRaceNameEN AS FTRaceName"
            _Qry &= vbCrLf & "  ,MSXTT.FTNameEN  AS FTStateCalSocial "
        End If

        _Qry &= vbCrLf & " , ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
        _Qry &= vbCrLf & " , ISNULL(Dept.FTDeptCode,'') AS FTDeptCode, ISNULL(DI.FTDivisonCode,'') AS FTDivisonCode"
        _Qry &= vbCrLf & " , ISNULL(ST.FTSectCode,'') AS FTSectCode,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode"
        _Qry &= vbCrLf & " , OrgPosit.FTPositCode, M.FNEmpStatus,M.FTEmpCodeRefer,M.FTEmpIdNo"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(M.FDDateStart) =1 THEN CONVERT(varchar(10),Convert(datetime,M.FDDateStart),103) ELSE '' END AS FDDateStart"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(M.FDDateEnd) =1 THEN CONVERT(varchar(10),Convert(datetime,M.FDDateEnd),103) ELSE '' END AS FDDateEnd"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(M.FDBirthDate) =1 THEN CONVERT(varchar(10),Convert(datetime,M.FDBirthDate),103) ELSE '' END AS FDBirthDate"
        _Qry &= vbCrLf & " , [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd) AS FNMonthWorkAge"
        _Qry &= vbCrLf & " , [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_Get_Emp_Age(M.FDBirthDate) AS FNMonthEmpAge"
        _Qry &= vbCrLf & ",M.FTAddrProvince AS FTEmpProvince"
        _Qry &= vbCrLf & ",M.FTAddrProvince1 AS FTEmpProvinceCurrent"
        _Qry &= vbCrLf & ",CASE WHEN ISNULL(M.FNEmpSex,0) = 0 THEN 1 ELSE 0 END AS FNMale"
        _Qry &= vbCrLf & ",CASE WHEN ISNULL(M.FNEmpSex,0) = 1 THEN 1 ELSE 0 END AS FNFemale"
        _Qry &= vbCrLf & ",1 AS FNTotal"
        _Qry &= vbCrLf & ", M.FCWeight, M.FCHeight, BB.FTBldCode, EmpRace.FTRaceCode, EMPRe.FTReligionCode, M.FTSocialNo, M.FTTaxNo"

        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS OrgPosit WITH (NOLOCK) ON M.FNHSysPositId = OrgPosit.FNHSysPositId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MEmpStatus AS ES ON M.FNEmpStatus = ES.FNListIndex  LEFT OUTER JOIN "
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMNationality AS MNT WITH (NOLOCK) ON M.FNHSysNationalityId = MNT.FNHSysNationalityId LEFT OUTER JOIN"

        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMReligion AS EMPRe WITH(NOLOCK) ON M.FNHSysReligionId = EMPRe.FNHSysReligionId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMRace AS EmpRace  WITH(NOLOCK) ON M.FNHSysRaceId = EmpRace.FNHSysRaceId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMBlood AS BB WITH(NOLOCK)  ON M.FNHSysBldId = BB.FNHSysBldId"

        _Qry &= vbCrLf & " LEFT OUTER JOIN ("

        _Qry &= vbCrLf & "    SELECT FNListIndex, FTNameTH, FTNameEN"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS X WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE  (FTListName = N'FNEmpSex')"
        _Qry &= vbCrLf & " ) AS MSX ON  M.FNEmpSex=MSX.FNListIndex "

        _Qry &= vbCrLf & " LEFT OUTER JOIN ("
        _Qry &= vbCrLf & "    SELECT FNListIndex, FTNameTH, FTNameEN"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS X WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE  (FTListName = N'FNCalSocialSta')"
        _Qry &= vbCrLf & " ) AS MSXTT ON  M.FNCalSocialSta=MSXTT.FNListIndex "



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
            Case Else
                _Qry &= vbCrLf & " AND   M.FNEmpStatus=" & Val(FNEmpStatusReport.SelectedIndex - 1) & " "
        End Select

        _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)
        _Qry = _Qry & " ) AS A "

        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        With Me.pivotGridControl
            .DataSource = _dt.Copy
        End With

        _dt.Dispose()

    End Sub
#End Region

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        'With ogv

        '    '.Columns("FTEmpTypeCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTEmpTypeCode")
        '    '.Columns("FTDeptCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTDeptCode")
        '    '.Columns("FTDivisonCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTDivisonCode")
        '    '.Columns("FTSectCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTSectCode")
        '    '.Columns("FTUnitSectCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTUnitSectCode")

        '    .Columns("FTEmpTypeCode").Group()
        '    .Columns("FTSectCode").Group()
        '    .Columns("FTEmpStatusName").Group()

        '    .Columns("FTEmpCode").Summary.Add(DevExpress.Data.SummaryItemType.Count, "FTEmpCode")
        '    .Columns("FTEmpCode").SummaryItem.DisplayFormat = "{0:n0}"
        '    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "FTEmpCode", Nothing, "(Count by " & .Columns.ColumnByFieldName("FTEmpCode").Caption & "={0:n0})")
        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        '    .OptionsView.ShowFooter = True
        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
        '    .OptionsView.ShowGroupPanel = True
        '    .ExpandAllGroups()

        '    .RefreshData()

        'End With
        '------End Add Summary Grid-------------
    End Sub
#End Region

#Region "General"
    Private Sub wEmployeeListing_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Call InitGrid()

      

        Try
            FNEmpStatusReport.SelectedIndex = 1
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

#End Region



End Class