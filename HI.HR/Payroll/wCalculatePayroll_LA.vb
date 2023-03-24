Imports DevExpress.CodeParser
Imports DevExpress.XtraGrid.Views.Grid

Public Class wCalculatePayroll_LA
    Private _ListNotCal As wListEmplyeeNotCalPayroll
#Region "General"

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _ListNotCal = New wListEmplyeeNotCalPayroll
        HI.TL.HandlerControl.AddHandlerObj(_ListNotCal)


        Dim _SystemLang As New ST.SysLanguage
        Try
            Call _SystemLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ListNotCal.Name.ToString.Trim, _ListNotCal)
        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmclose.Click
        Me.Close()
    End Sub

    Private Function GenQuery(SDate As String, EDate As String, _FNHSysEmpTypeId As Integer, Optional StateDel As Boolean = False) As String
        Dim _Qry As String = ""

        _Qry = " SELECT      '0' AS FTSelect,  M.FNHSysEmpID, M.FTEmpCode,M.FNHSysEmpTypeId, ET.FNCalType"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & " , ISNULL(P.FTPreNameNameTH,N'') + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

        Else
            _Qry &= vbCrLf & " , ISNULL(P.FTPreNameNameEN,N'') + ' ' + M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
        End If

        _Qry &= vbCrLf & " , M.FTDeligentCode "
        _Qry &= vbCrLf & " FROM        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        _Qry &= vbCrLf & "   THRMTimeShift AS SH WITH (NOLOCK) ON M.FNHSysShiftID = SH.FNHSysShiftID "
        _Qry &= vbCrLf & "   INNER Join "
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS SE WITH (NOLOCK) ON M.FNHSysSectId = SE.FNHSysSectId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId "
        _Qry &= vbCrLf & "   WHERE  M.FTEmpCode <> ''  "
        _Qry &= vbCrLf & "   AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "
        '  _Qry &= vbCrLf & "   AND M.FDDateStart <=N'" & EDate & "' "

        If FTPayTerm.Text = "01" Then
            _Qry &= vbCrLf & "   AND  ( CASE WHEN ISNULL(M.FDDateTransfer,'') <> '' AND ISNULL(M.FDDateTransfer,'') > M.FDDateStart THEN ISNULL(M.FDDateTransfer,'') ELSE M.FDDateStart  END <=N'" & EDate & "' "

            _Qry &= vbCrLf & " OR (M.FNHSysEmpID IN ("
            _Qry &= vbCrLf & "SELECT     P.FNHSysEmpID "
            _Qry &= vbCrLf & "  FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS P  WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M  WITH (NOLOCK)  ON P.FNHSysEmpID = M.FNHSysEmpID"
            _Qry &= vbCrLf & "  WHERE     (P.FTPayYear = '" & (Integer.Parse(Val(Me.FTPayYear.Text)) - 1).ToString("0000") & "') "
            _Qry &= vbCrLf & "  AND (P.FTPayTerm = '25') "
            _Qry &= vbCrLf & "  AND (M.FDDateEnd <> '') "
            _Qry &= vbCrLf & "  AND M.FDDateEnd<='" & SDate & "'"
            _Qry &= vbCrLf & "))"
            _Qry &= vbCrLf & "     )"
        Else
            _Qry &= vbCrLf & "   AND CASE WHEN ISNULL(M.FDDateTransfer,'') <> '' AND ISNULL(M.FDDateTransfer,'') > M.FDDateStart THEN ISNULL(M.FDDateTransfer,'') ELSE M.FDDateStart  END <=N'" & EDate & "' "
        End If

        If StateDel = False Then

            If FTPayTerm.Text = "01" Then
                _Qry &= vbCrLf & "   AND ((M.FDDateEnd =N'' OR M.FDDateEnd >'" & SDate & "' )   "

                _Qry &= vbCrLf & " OR (M.FDDateEnd<>'' AND M.FDDateEnd <='" & SDate & "' AND  M.FNHSysEmpID IN ("
                _Qry &= vbCrLf & "SELECT     P.FNHSysEmpID "
                _Qry &= vbCrLf & "  FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS P  WITH (NOLOCK) INNER JOIN"
                _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M  WITH (NOLOCK)  ON P.FNHSysEmpID = M.FNHSysEmpID"
                _Qry &= vbCrLf & "  WHERE     (P.FTPayYear = '" & (Integer.Parse(Val(Me.FTPayYear.Text)) - 1).ToString("0000") & "') "
                _Qry &= vbCrLf & "  AND (P.FTPayTerm = '25') "
                _Qry &= vbCrLf & "  AND (M.FDDateEnd <> '') "
                _Qry &= vbCrLf & "  AND M.FDDateEnd<='" & SDate & "'"
                _Qry &= vbCrLf & "))"
                _Qry &= vbCrLf & "     )"

            Else
                _Qry &= vbCrLf & "   AND ((M.FDDateEnd =N'' OR M.FDDateEnd >'" & SDate & "' )   "

                _Qry &= vbCrLf & " OR (M.FDDateEnd<>'' AND M.FDDateEnd <='" & SDate & "' AND  M.FNHSysEmpID IN ("
                _Qry &= vbCrLf & " SELECT DISTINCT(MN.FNHSysEmpID) AS FNHSysEmpID "
                _Qry &= vbCrLf & "  FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTManage MN "

                _Qry &= vbCrLf & "   INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) ON MN.FNHSysEmpID=M.FNHSysEmpID "
                _Qry &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
                _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
                _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId "
                _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS SE WITH (NOLOCK) ON M.FNHSysSectId = SE.FNHSysSectId "
                _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId "

                _Qry &= vbCrLf & "  WHERE     (FTPayYear = '" & (Integer.Parse(Val(Me.FTPayYear.Text))).ToString("0000") & "') "
                _Qry &= vbCrLf & "  AND (FTPayTerm = '" & FTPayTerm.Text & "') "



                If _FNHSysEmpTypeId > 0 Then
                    _Qry &= vbCrLf & " AND ET.FNHSysEmpTypeId='" & Val(_FNHSysEmpTypeId) & "' "
                End If
                'If Me.FNHSysEmpTypeId.Text <> "" Then
                '    _Qry &= vbCrLf & " AND ET.FTEmpTypeCode=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
                'End If

                '------Criteria By Employeee Code
                If Me.FNHSysEmpId.Text <> "" Then
                    _Qry &= vbCrLf & " AND M.FTEmpCode >=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
                End If

                If Me.FNHSysEmpIdTo.Text <> "" Then
                    _Qry &= vbCrLf & " AND M.FTEmpCode <=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
                End If

                '------Criteria By Department
                If Me.FNHSysDeptId.Text <> "" Then
                    _Qry &= vbCrLf & " AND  Dept.FTDeptCode>=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
                End If

                If Me.FNHSysDeptIdTo.Text <> "" Then
                    _Qry &= vbCrLf & " AND  Dept.FTDeptCode<=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
                End If

                '------Criteria By Division
                If Me.FNHSysDivisonId.Text <> "" Then
                    _Qry &= vbCrLf & " AND  DI.FTDivisonCode>=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
                End If

                If Me.FNHSysDivisonIdTo.Text <> "" Then
                    _Qry &= vbCrLf & " AND  DI.FTDivisonCode<=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
                End If

                '------Criteria By Sect
                If Me.FNHSysSectId.Text <> "" Then
                    _Qry &= vbCrLf & " AND  SE.FTSectCode>=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
                End If

                If Me.FNHSysSectIdTo.Text <> "" Then
                    _Qry &= vbCrLf & " AND  SE.FTSectCode<=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
                End If

                '------Criteria Unit Sect
                If Me.FNHSysUnitSectId.Text <> "" Then
                    _Qry &= vbCrLf & " AND   US.FTUnitSectCode>=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
                End If

                If Me.FNHSysUnitSectIdTo.Text <> "" Then
                    _Qry &= vbCrLf & " AND   US.FTUnitSectCode<=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
                End If



                _Qry &= vbCrLf & ")"
                _Qry &= vbCrLf & "     ))"

            End If

        End If

        If Not (HI.ST.SysInfo.Admin) Then

            _Qry &= vbCrLf & "  AND M.FNHSysEmpTypeId IN ("
            _Qry &= vbCrLf & " Select DISTINCT UPT.FNHSysEmpTypeId"
            _Qry &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID"
            _Qry &= vbCrLf & "  WHERE UP.FTUserName=N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & "  AND UPM.FTMnuName=N'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) & "' "

            _Qry &= vbCrLf & "  )      "
            _Qry &= vbCrLf & " AND M.FNHSysSectId IN ( "
            _Qry &= vbCrLf & " Select DISTINCT S.FNHSysSectId"
            _Qry &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeType AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID"
            _Qry &= vbCrLf & "   CROSS JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S  WITH(NOLOCK)"
            _Qry &= vbCrLf & "  WHERE UP.FTUserName=N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & "  AND UPM.FTMnuName=N'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) & "' "
            _Qry &= vbCrLf & "  AND UPT.FTStateAll=N'1' "
            _Qry &= vbCrLf & " UNION"
            _Qry &= vbCrLf & " Select DISTINCT S.FNHSysSectId"
            _Qry &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLoginPermission AS UP WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionMenu AS UPM WITH (NOLOCK) ON UP.FNHSysPermissionID = UPM.FNHSysPermissionID INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEPermissionEmployeeTypeSect AS UPT WITH (NOLOCK) ON UP.FNHSysPermissionID = UPT.FNHSysPermissionID AND UPM.FNHSysPermissionID = UPT.FNHSysPermissionID"
            _Qry &= vbCrLf & "   INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH(NOLOCK) ON UPT.FNHSysSectId = S.FNHSysSectId  "
            _Qry &= vbCrLf & "  WHERE UP.FTUserName=N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Qry &= vbCrLf & "  AND UPM.FTMnuName=N'" & HI.UL.ULF.rpQuoted(HI.ST.SysInfo.MenuName) & "' "
            _Qry &= vbCrLf & "  )      "

        End If



        If _FNHSysEmpTypeId > 0 Then
            _Qry &= vbCrLf & " AND ET.FNHSysEmpTypeId='" & Val(_FNHSysEmpTypeId) & "' "
        End If
        'If Me.FNHSysEmpTypeId.Text <> "" Then
        '    _Qry &= vbCrLf & " And ET.FTEmpTypeCode = N '" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
        'End If

        '------Criteria By Employeee Code
        If Me.FNHSysEmpId.Text <> "" Then
            _Qry &= vbCrLf & " AND M.FTEmpCode >=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
        End If

        If Me.FNHSysEmpIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND M.FTEmpCode <=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
        End If

        '------Criteria By Department
        If Me.FNHSysDeptId.Text <> "" Then
            _Qry &= vbCrLf & " AND  Dept.FTDeptCode>=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
        End If

        If Me.FNHSysDeptIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  Dept.FTDeptCode<=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
        End If

        '------Criteria By Division
        If Me.FNHSysDivisonId.Text <> "" Then
            _Qry &= vbCrLf & " AND  DI.FTDivisonCode>=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
        End If

        If Me.FNHSysDivisonIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  DI.FTDivisonCode<=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
        End If

        '------Criteria By Sect
        If Me.FNHSysSectId.Text <> "" Then
            _Qry &= vbCrLf & " AND  SE.FTSectCode>=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
        End If

        If Me.FNHSysSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  SE.FTSectCode<=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
        End If

        '------Criteria Unit Sect
        If Me.FNHSysUnitSectId.Text <> "" Then
            _Qry &= vbCrLf & " AND   US.FTUnitSectCode>=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
        End If

        If Me.FNHSysUnitSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND   US.FTUnitSectCode<=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
        End If


        Return _Qry
    End Function

    Private Sub Calculate()

        Dim dtGroup_EmpType As DataTable

        Dim dtemptype As DataTable
        Dim _Dt As DataTable
        Dim _Qry As String = ""
        Dim _QryDel As String = ""
        Dim _StateVacationRet As Integer
        Dim _FTStaDeductAbsent As Integer = 0
        Dim _FTStaCalPayRoll As Integer = 0
        Dim _FNStateSalaryType As Integer = 0

        Dim _FNHSysEmpTypeId As Integer = 0
        Dim _dttmpemp As New DataTable

        _dttmpemp.Columns.Add("FTEmpCode", GetType(String))
        _dttmpemp.Columns.Add("FTEmpName", GetType(String))


        Dim _FNEmpTypeGroupID As Integer = 0
        Dim _FTPayYear As String = ""
        Dim _FTPayTerm As String = ""
        Dim _FTStartDate As String = ""
        Dim _FTEndDate As String = ""

        Dim _FDPayDate As String = ""


        Dim _Rec As Integer = 0
        Dim _RecError As Integer = 0
        Dim _TotalRec As Integer = 0

        Dim _FNHSysEmpTypeId_M_thai As Integer = 0


        If Not (Me.ogcemptype.DataSource Is Nothing) Then
            With CType(Me.ogcemptype.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTSelect='1'").Length > 0 Then
                    For Each RT As DataRow In .Select("FTSelect='1'")


                        _FNHSysEmpTypeId = Val(RT!FNHSysEmpTypeId.ToString)
                        _FNEmpTypeGroupID = Val(RT!FNEmpTypeGroupID.ToString)
                        _FTPayYear = RT!FTPayYear.ToString
                        _FTPayTerm = RT!FTPayTerm.ToString

                        _FTStartDate = RT!FDCalDateBegin.ToString
                        _FTEndDate = RT!FDCalDateEnd.ToString
                        _FDPayDate = RT!FDPayDate.ToString

                        _Qry = " SELECT FTEmpTypeCode,FTEmpTypeNameEN AS FTDescription,FNHSysEmpTypeId  "
                        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType ET WITH ( NOLOCK ) "
                        _Qry &= vbCrLf & " WHERE  FTStateActive ='1'  AND FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "

                        If Val(_FNEmpTypeGroupID) <> 0 Then
                            _Qry &= vbCrLf & " AND  FNEmpTypeGroup =" & Val(_FNEmpTypeGroupID) & " "
                        End If


                        If Val(_FNHSysEmpTypeId) <> 0 Then
                            _Qry &= vbCrLf & " AND ET.FNHSysEmpTypeId=" & Val(_FNHSysEmpTypeId)
                        End If

                        dtGroup_EmpType = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
                        Dim _Spls As New HI.TL.SplashScreen("Prepre Data For Calculate.. Please Wait ")



                        For Each dr In dtGroup_EmpType.Rows
                            _FNHSysEmpTypeId = Val(dr!FNHSysEmpTypeId)



                            If Integer.Parse(_FTPayYear) >= 2014 Then
                                _Qry = " SELECT   TOP 1 FCCfgRetValue"
                                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigReturnVacationSet WITH(NOLOCK) "
                                _Qry &= vbCrLf & "  WHERE      (FNCalType =" & Val(_FNHSysEmpTypeId) & ")"
                                _Qry &= vbCrLf & "  AND (FTCfgRetTerm = '" & HI.UL.ULF.rpQuoted(_FTPayTerm) & "')"
                                _StateVacationRet = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))
                            Else
                                _StateVacationRet = 0
                            End If

                            Dim _FNWorkDayInWeekBF As Integer = 0
                            Dim _FNWorkDayInWeek As Integer = 15
                            Dim _FNWorkDayInMonth As Integer = 30
                            Dim _dtWKDay As DataTable

                            _Qry = " SELECT SUM(ISNULL(A.FNWorkDay,0)) AS FNMonthWorkDay"
                            _Qry &= vbCrLf & " ,B.FNWorkDay AS FNWeekWorkDay"
                            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS A WITH(NOLOCK)"
                            _Qry &= vbCrLf & "  INNER Join"
                            _Qry &= vbCrLf & " (SELECT FTPayTerm, FTPayYear,FNMonth, FNHSysEmpTypeId, ISNULL(FNWorkDay,0) AS FNWorkDay"
                            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS X WITH(NOLOCK)"
                            _Qry &= vbCrLf & " WHERE  (FTPayTerm = '" & HI.UL.ULF.rpQuoted(_FTPayTerm) & "') "
                            _Qry &= vbCrLf & " AND (FTPayYear = '" & HI.UL.ULF.rpQuoted(_FTPayYear) & "') "
                            _Qry &= vbCrLf & " AND (FNHSysEmpTypeId =" & Val(_FNHSysEmpTypeId) & ")) AS B"
                            _Qry &= vbCrLf & " ON A.FTPayYear = B.FTPayYear"
                            _Qry &= vbCrLf & "  AND A.FNHSysEmpTypeId = B.FNHSysEmpTypeId "
                            _Qry &= vbCrLf & " AND A.FNMonth = B.FNMonth"
                            _Qry &= vbCrLf & " GROUP BY B.FNWorkDay"
                            _dtWKDay = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                            For Each R As DataRow In _dtWKDay.Rows

                                If Val(R!FNMonthWorkDay.ToString) > 0 Then
                                    _FNWorkDayInMonth = Val(R!FNMonthWorkDay.ToString)

                                    If _FNWorkDayInMonth > 30 Then _FNWorkDayInMonth = 30

                                End If

                                If Val(R!FNWeekWorkDay.ToString) > 0 Then
                                    _FNWorkDayInWeek = Val(R!FNWeekWorkDay.ToString)
                                End If

                                Exit For
                            Next

                            _Qry = " SELECT SUM(ISNULL(A.FNWorkDay,0)) AS FNWeekWorkDay"
                            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS A WITH(NOLOCK)"
                            _Qry &= vbCrLf & "  INNER Join"
                            _Qry &= vbCrLf & " (SELECT FTPayTerm, FTPayYear,FNMonth, FNHSysEmpTypeId, ISNULL(FNWorkDay,0) AS FNWorkDay"
                            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS X WITH(NOLOCK)"
                            _Qry &= vbCrLf & " WHERE  (FTPayTerm = '" & HI.UL.ULF.rpQuoted(_FTPayTerm) & "') "
                            _Qry &= vbCrLf & " AND (FTPayYear = '" & HI.UL.ULF.rpQuoted(_FTPayYear) & "') "
                            _Qry &= vbCrLf & " AND (FNHSysEmpTypeId =" & Val(_FNHSysEmpTypeId) & ")) AS B"
                            _Qry &= vbCrLf & " ON A.FTPayYear = B.FTPayYear"
                            _Qry &= vbCrLf & "  AND A.FNHSysEmpTypeId = B.FNHSysEmpTypeId "
                            _Qry &= vbCrLf & " AND A.FNMonth = B.FNMonth"
                            _Qry &= vbCrLf & " WHERE  (A.FTPayTerm < '" & HI.UL.ULF.rpQuoted(_FTPayTerm) & "') "
                            _Qry &= vbCrLf & " GROUP BY B.FNWorkDay"
                            _dtWKDay = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                            For Each R As DataRow In _dtWKDay.Rows

                                If Val(R!FNWeekWorkDay.ToString) > 0 Then
                                    _FNWorkDayInWeekBF = Val(R!FNWeekWorkDay.ToString)
                                End If

                                Exit For
                            Next
                            _dtWKDay.Dispose()

                            _Qry = "SELECT TOP 1 FNCalType,FTStaDeductAbsent,FTStaCalPayRoll,FNStateSalaryType FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType  WITH(NOLOCK) WHERE FNHSysEmpTypeId=" & Integer.Parse(Val(_FNHSysEmpTypeId)) & "  "
                            dtemptype = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

                            Dim _TmpCalType As Integer = 0

                            Dim SDate As String = HI.UL.ULDate.ConvertEnDB(_FTStartDate)
                            Dim EDate As String = HI.UL.ULDate.ConvertEnDB(_FTEndDate)

                            For Each R As DataRow In dtemptype.Rows
                                _TmpCalType = Integer.Parse(Val(R!FNCalType.ToString))
                                _FTStaDeductAbsent = Integer.Parse(Val(R!FTStaDeductAbsent.ToString))
                                _FTStaCalPayRoll = Integer.Parse(Val(R!FTStaCalPayRoll.ToString))
                                _FNStateSalaryType = Integer.Parse(Val(R!FNStateSalaryType.ToString))
                                Exit For
                            Next
                            dtemptype.Dispose()
                            If _TmpCalType = 2 Or _TmpCalType = 3 Then

                                If _FTStaCalPayRoll = 1 Then
                                    SDate = HI.UL.ULDate.ConvertEnDB(Microsoft.VisualBasic.Left(EDate, 8) & "01")  'วันแรกของเดือน
                                    EDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(HI.UL.ULDate.AddMonth(Microsoft.VisualBasic.Left(EDate, 8) & "01", 1), -1)) 'วันแของเดือน
                                End If

                            End If

                            _Qry = Me.GenQuery(SDate, EDate, _FNHSysEmpTypeId, True)

                            _QryDel = "DELETE  P FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS P , (" & _Qry & ") As M"
                            _QryDel &= vbCrLf & " WHERE P.FNHSysEmpID = M.FNHSysEmpID"
                            _QryDel &= vbCrLf & " AND P.FTPayYear = '" & _FTPayYear & "'"
                            _QryDel &= vbCrLf & " AND P.FTPayTerm = '" & _FTPayTerm & "'"
                            HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

                            _QryDel = "DELETE  PF FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollCalculate AS PF , (" & _Qry & ") As M"
                            _QryDel &= vbCrLf & " WHERE PF.FNHSysEmpID = M.FNHSysEmpID"
                            _QryDel &= vbCrLf & " AND PF.FTPayYear = '" & _FTPayYear & "'"
                            _QryDel &= vbCrLf & " AND PF.FTPayTerm = '" & _FTPayTerm & "'"
                            HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

                            _QryDel = "  DELETE PF FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin As PF, (" & _Qry & ")  As M"
                            _QryDel &= vbCrLf & " WHERE PF.FNHSysEmpID = M.FNHSysEmpID"
                            _QryDel &= vbCrLf & " AND PF.FTPayYear = '" & _FTPayYear & "'"
                            _QryDel &= vbCrLf & " AND PF.FTPayTerm = '" & _FTPayTerm & "'"
                            HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

                            _QryDel = "  DELETE PFM FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFinCalculate As PFM, (" & _Qry & ")  As M"
                            _QryDel &= vbCrLf & " WHERE PFM.FNHSysEmpID = M.FNHSysEmpID"
                            _QryDel &= vbCrLf & " AND PFM.FTPayYear = '" & _FTPayYear & "'"
                            _QryDel &= vbCrLf & " AND PFM.FTPayTerm = '" & _FTPayTerm & "'"
                            HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

                            _QryDel = "  DELETE PML  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollLeave AS PML, (" & _Qry & ") AS M"
                            _QryDel &= vbCrLf & " WHERE PML.FNHSysEmpID = M.FNHSysEmpID"
                            _QryDel &= vbCrLf & " AND PML.FTPayYear = '" & _FTPayYear & "'"
                            _QryDel &= vbCrLf & " AND PML.FTPayTerm = '" & _FTPayTerm & "'"
                            HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

                            _QryDel = "  DELETE PMC  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTManageCalculate AS PMC, (" & _Qry & ") AS M"
                            _QryDel &= vbCrLf & " WHERE PMC.FNHSysEmpID = M.FNHSysEmpID"
                            _QryDel &= vbCrLf & " AND PMC.FTPayYear = '" & _FTPayYear & "'"
                            _QryDel &= vbCrLf & " AND PMC.FTPayTerm = '" & _FTPayTerm & "'"
                            HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)


                            _Qry = Me.GenQuery(SDate, EDate, _FNHSysEmpTypeId)
                            _Qry &= vbCrLf & " ORDER BY  M.FTEmpCode  "

                            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


                            _TotalRec += _Dt.Rows.Count
                            HI.HRCAL.Calculate.LoadSocialRate()
                            HI.HRCAL.Calculate.LoadTaxRate()
                            HI.HRCAL.Calculate.LoadDiscountTax()


                            For Each R As DataRow In _Dt.Rows


                                If R!FNHSysEmpTypeId.ToString = "2012610375" Then
                                    _FNHSysEmpTypeId_M_thai = _FNHSysEmpTypeId_M_thai + 1
                                End If


                                _Rec = _Rec + 1
                                _Spls.UpdateInformation("Calculating... Employee Code " & R!FTEmpCode.ToString & "    " & R!FTEmpName.ToString & "  Record  " & _Rec.ToString & " Of " & _TotalRec.ToString & "  (" & Format((_Rec * 100.0) / _TotalRec, "0.00") & " % ) ")



                                'If HI.HRCAL.Calculate.CalculateWeekEnd(HI.ST.UserInfo.UserName, R!FNHSysEmpID.ToString _
                                '                            , R!FNHSysEmpTypeId.ToString, HI.UL.ULDate.ConvertEnDB(_FTStartDate), HI.UL.ULDate.ConvertEnDB(_FTEndDate) _
                                '                            , _FTPayYear, _FTPayTerm, _FDPayDate, R!FTDeligentCode.ToString, _TmpCalType.ToString _
                                '                            , False, _StateVacationRet, _FTStaDeductAbsent, _FTStaCalPayRoll, _FNStateSalaryType, _FNWorkDayInWeek, _FNWorkDayInMonth) = False Then

                                If HI.HRCAL.Calculate.CalculateWeekEnd_LA(HI.ST.UserInfo.UserName, R!FNHSysEmpID.ToString _
                                        , R!FNHSysEmpTypeId.ToString, HI.UL.ULDate.ConvertEnDB(_FTStartDate), HI.UL.ULDate.ConvertEnDB(_FTEndDate) _
                                        , _FTPayYear, _FTPayTerm, _FDPayDate, R!FTDeligentCode.ToString, _TmpCalType.ToString _
                                        , False, _StateVacationRet, _FTStaDeductAbsent, _FTStaCalPayRoll, _FNStateSalaryType, 0,
                                        0, _FNWorkDayInWeek, _FNWorkDayInMonth, _FNWorkDayInWeekBF, 0, 0, R!FNCalType.ToString) = False Then


                                    _dttmpemp.Rows.Add(R!FTEmpCode.ToString, R!FTEmpName.ToString)

                                    _RecError = _RecError + 1

                                End If

                            Next

                        Next

                        _Spls.Close()

                    Next
                End If
            End With
            HI.MG.ShowMsg.mInvalidData("", 1105030002, Me.Text, (_Rec - _RecError).ToString & " Records  ")
        End If

        If _dttmpemp.Rows.Count > 0 Then

            If _FNHSysEmpTypeId_M_thai > 0 Then
            Else

                HI.MG.ShowMsg.mInvalidData("พบข้อมูล วันทำงานงวด ยังไม่มีการ Accept ไม่สามารถทำการคำนวณได้ !!!", 1175030002, Me.Text)
            End If


            With _ListNotCal
                .ogclist.DataSource = _dttmpemp.Copy
                .ShowDialog()
            End With

        End If

        _dttmpemp.Dispose()




    End Sub

    Private Sub ocmcalculate_Click(sender As System.Object, e As System.EventArgs) Handles ocmcalculate.Click

        Call Calculate()

        'If HI.UL.ULDate.CheckDate(Me.FTStartDate.Text) <> "" And HI.UL.ULDate.CheckDate(Me.FTEndDate.Text) <> "" Then

        '    Dim dtGroup_EmpType As DataTable

        '    Dim dtemptype As DataTable
        '    Dim _Dt As DataTable
        '    Dim _Qry As String = ""
        '    Dim _QryDel As String = ""
        '    Dim _StateVacationRet As Integer
        '    Dim _FTStaDeductAbsent As Integer = 0
        '    Dim _FTStaCalPayRoll As Integer = 0
        '    Dim _FNStateSalaryType As Integer = 0

        '    Dim _FNHSysEmpTypeId As Integer = 0

        '    _Qry = " SELECT FTEmpTypeCode,FTEmpTypeNameEN AS FTDescription,FNHSysEmpTypeId  "
        '    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType ET WITH ( NOLOCK ) "
        '    _Qry &= vbCrLf & " WHERE  FTStateActive ='1'  AND FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "
        '    If FNEmpTypeGroup.SelectedIndex >= 0 Then
        '        _Qry &= vbCrLf & " AND  FNEmpTypeGroup =" & Val(FNEmpTypeGroup.SelectedIndex.ToString) & " "
        '    End If


        '    If Me.FNHSysEmpTypeId.Text <> "" Then
        '        _Qry &= vbCrLf & " AND ET.FTEmpTypeCode=N'" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
        '    End If

        '    dtGroup_EmpType = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        '    Dim _Spls As New HI.TL.SplashScreen("Prepre Data For Calculate.. Please Wait ")
        '    Dim _Rec As Integer = 0
        '    Dim _RecError As Integer = 0
        '    Dim _TotalRec As Integer = 0
        '    Dim _dttmpemp As New DataTable

        '    _dttmpemp.Columns.Add("FTEmpCode", GetType(String))
        '    _dttmpemp.Columns.Add("FTEmpName", GetType(String))

        '    For Each dr In dtGroup_EmpType.Rows
        '        _FNHSysEmpTypeId = Val(dr!FNHSysEmpTypeId)



        '        If Integer.Parse(FTPayYear.Text) >= 2014 Then
        '            _Qry = " SELECT   TOP 1 FCCfgRetValue"
        '            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigReturnVacationSet WITH(NOLOCK) "
        '            _Qry &= vbCrLf & "  WHERE      (FNCalType =" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & ")"
        '            _Qry &= vbCrLf & "  AND (FTCfgRetTerm = '" & HI.UL.ULF.rpQuoted(FTPayTerm.Text) & "')"
        '            _StateVacationRet = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0"))
        '        Else
        '            _StateVacationRet = 0
        '        End If

        '        Dim _FNWorkDayInWeekBF As Integer = 0
        '        Dim _FNWorkDayInWeek As Integer = 15
        '        Dim _FNWorkDayInMonth As Integer = 30
        '        Dim _dtWKDay As DataTable

        '        _Qry = " SELECT SUM(ISNULL(A.FNWorkDay,0)) AS FNMonthWorkDay"
        '        _Qry &= vbCrLf & " ,B.FNWorkDay AS FNWeekWorkDay"
        '        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS A WITH(NOLOCK)"
        '        _Qry &= vbCrLf & "  INNER Join"
        '        _Qry &= vbCrLf & " (SELECT FTPayTerm, FTPayYear,FNMonth, FNHSysEmpTypeId, ISNULL(FNWorkDay,0) AS FNWorkDay"
        '        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS X WITH(NOLOCK)"
        '        _Qry &= vbCrLf & " WHERE  (FTPayTerm = '" & HI.UL.ULF.rpQuoted(FTPayTerm.Text) & "') "
        '        _Qry &= vbCrLf & " AND (FTPayYear = '" & HI.UL.ULF.rpQuoted(FTPayYear.Text) & "') "
        '        _Qry &= vbCrLf & " AND (FNHSysEmpTypeId =" & Val(_FNHSysEmpTypeId) & ")) AS B"
        '        _Qry &= vbCrLf & " ON A.FTPayYear = B.FTPayYear"
        '        _Qry &= vbCrLf & "  AND A.FNHSysEmpTypeId = B.FNHSysEmpTypeId "
        '        _Qry &= vbCrLf & " AND A.FNMonth = B.FNMonth"
        '        _Qry &= vbCrLf & " GROUP BY B.FNWorkDay"
        '        _dtWKDay = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        '        For Each R As DataRow In _dtWKDay.Rows

        '            If Val(R!FNMonthWorkDay.ToString) > 0 Then
        '                _FNWorkDayInMonth = Val(R!FNMonthWorkDay.ToString)

        '                If _FNWorkDayInMonth > 30 Then _FNWorkDayInMonth = 30

        '            End If

        '            If Val(R!FNWeekWorkDay.ToString) > 0 Then
        '                _FNWorkDayInWeek = Val(R!FNWeekWorkDay.ToString)
        '            End If

        '            Exit For
        '        Next

        '        _Qry = " SELECT SUM(ISNULL(A.FNWorkDay,0)) AS FNWeekWorkDay"
        '        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS A WITH(NOLOCK)"
        '        _Qry &= vbCrLf & "  INNER Join"
        '        _Qry &= vbCrLf & " (SELECT FTPayTerm, FTPayYear,FNMonth, FNHSysEmpTypeId, ISNULL(FNWorkDay,0) AS FNWorkDay"
        '        _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS X WITH(NOLOCK)"
        '        _Qry &= vbCrLf & " WHERE  (FTPayTerm = '" & HI.UL.ULF.rpQuoted(FTPayTerm.Text) & "') "
        '        _Qry &= vbCrLf & " AND (FTPayYear = '" & HI.UL.ULF.rpQuoted(FTPayYear.Text) & "') "
        '        _Qry &= vbCrLf & " AND (FNHSysEmpTypeId =" & Val(_FNHSysEmpTypeId) & ")) AS B"
        '        _Qry &= vbCrLf & " ON A.FTPayYear = B.FTPayYear"
        '        _Qry &= vbCrLf & "  AND A.FNHSysEmpTypeId = B.FNHSysEmpTypeId "
        '        _Qry &= vbCrLf & " AND A.FNMonth = B.FNMonth"
        '        _Qry &= vbCrLf & " WHERE  (A.FTPayTerm < '" & HI.UL.ULF.rpQuoted(FTPayTerm.Text) & "') "
        '        _Qry &= vbCrLf & " GROUP BY B.FNWorkDay"
        '        _dtWKDay = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        '        For Each R As DataRow In _dtWKDay.Rows

        '            If Val(R!FNWeekWorkDay.ToString) > 0 Then
        '                _FNWorkDayInWeekBF = Val(R!FNWeekWorkDay.ToString)
        '            End If

        '            Exit For
        '        Next
        '        _dtWKDay.Dispose()

        '        _Qry = "SELECT TOP 1 FNCalType,FTStaDeductAbsent,FTStaCalPayRoll,FNStateSalaryType FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType  WITH(NOLOCK) WHERE FNHSysEmpTypeId=" & Integer.Parse(Val(_FNHSysEmpTypeId)) & "  "
        '        dtemptype = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        '        Dim _TmpCalType As Integer = 0

        '        Dim SDate As String = HI.UL.ULDate.ConvertEnDB(FTStartDate.Text)
        '        Dim EDate As String = HI.UL.ULDate.ConvertEnDB(FTEndDate.Text)

        '        For Each R As DataRow In dtemptype.Rows
        '            _TmpCalType = Integer.Parse(Val(R!FNCalType.ToString))
        '            _FTStaDeductAbsent = Integer.Parse(Val(R!FTStaDeductAbsent.ToString))
        '            _FTStaCalPayRoll = Integer.Parse(Val(R!FTStaCalPayRoll.ToString))
        '            _FNStateSalaryType = Integer.Parse(Val(R!FNStateSalaryType.ToString))
        '            Exit For
        '        Next
        '        dtemptype.Dispose()
        '        If _TmpCalType = 2 Or _TmpCalType = 3 Then

        '            If _FTStaCalPayRoll = 1 Then
        '                SDate = HI.UL.ULDate.ConvertEnDB(Microsoft.VisualBasic.Left(EDate, 8) & "01")  'วันแรกของเดือน
        '                EDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(HI.UL.ULDate.AddMonth(Microsoft.VisualBasic.Left(EDate, 8) & "01", 1), -1)) 'วันแของเดือน
        '            End If

        '        End If

        '        _Qry = Me.GenQuery(SDate, EDate, _FNHSysEmpTypeId, True)

        '        _QryDel = "DELETE  P FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll AS P , (" & _Qry & ") As M"
        '        _QryDel &= vbCrLf & " WHERE P.FNHSysEmpID = M.FNHSysEmpID"
        '        _QryDel &= vbCrLf & " AND P.FTPayYear = '" & FTPayYear.Text & "'"
        '        _QryDel &= vbCrLf & " AND P.FTPayTerm = '" & FTPayTerm.Text & "'"
        '        HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

        '        _QryDel = "DELETE  PF FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollCalculate AS PF , (" & _Qry & ") As M"
        '        _QryDel &= vbCrLf & " WHERE PF.FNHSysEmpID = M.FNHSysEmpID"
        '        _QryDel &= vbCrLf & " AND PF.FTPayYear = '" & FTPayYear.Text & "'"
        '        _QryDel &= vbCrLf & " AND PF.FTPayTerm = '" & FTPayTerm.Text & "'"
        '        HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

        '        _QryDel = "  DELETE PF FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFin As PF, (" & _Qry & ")  As M"
        '        _QryDel &= vbCrLf & " WHERE PF.FNHSysEmpID = M.FNHSysEmpID"
        '        _QryDel &= vbCrLf & " AND PF.FTPayYear = '" & FTPayYear.Text & "'"
        '        _QryDel &= vbCrLf & " AND PF.FTPayTerm = '" & FTPayTerm.Text & "'"
        '        HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

        '        _QryDel = "  DELETE PFM FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollFinCalculate As PFM, (" & _Qry & ")  As M"
        '        _QryDel &= vbCrLf & " WHERE PFM.FNHSysEmpID = M.FNHSysEmpID"
        '        _QryDel &= vbCrLf & " AND PFM.FTPayYear = '" & FTPayYear.Text & "'"
        '        _QryDel &= vbCrLf & " AND PFM.FTPayTerm = '" & FTPayTerm.Text & "'"
        '        HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

        '        _QryDel = "  DELETE PML  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollLeave AS PML, (" & _Qry & ") AS M"
        '        _QryDel &= vbCrLf & " WHERE PML.FNHSysEmpID = M.FNHSysEmpID"
        '        _QryDel &= vbCrLf & " AND PML.FTPayYear = '" & FTPayYear.Text & "'"
        '        _QryDel &= vbCrLf & " AND PML.FTPayTerm = '" & FTPayTerm.Text & "'"
        '        HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)

        '        _QryDel = "  DELETE PMC  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTManageCalculate AS PMC, (" & _Qry & ") AS M"
        '        _QryDel &= vbCrLf & " WHERE PMC.FNHSysEmpID = M.FNHSysEmpID"
        '        _QryDel &= vbCrLf & " AND PMC.FTPayYear = '" & FTPayYear.Text & "'"
        '        _QryDel &= vbCrLf & " AND PMC.FTPayTerm = '" & FTPayTerm.Text & "'"
        '        HI.Conn.SQLConn.ExecuteNonQuery(_QryDel, Conn.DB.DataBaseName.DB_HR)


        '        _Qry = Me.GenQuery(SDate, EDate, _FNHSysEmpTypeId)
        '        _Qry &= vbCrLf & " ORDER BY  M.FTEmpCode  "

        '        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


        '        _TotalRec += _Dt.Rows.Count
        '        HI.HRCAL.Calculate.LoadSocialRate()
        '        HI.HRCAL.Calculate.LoadTaxRate()
        '        HI.HRCAL.Calculate.LoadDiscountTax()


        '        For Each R As DataRow In _Dt.Rows

        '            _Rec = _Rec + 1
        '            _Spls.UpdateInformation("Calculating... Employee Code " & R!FTEmpCode.ToString & "    " & R!FTEmpName.ToString & "  Record  " & _Rec.ToString & " Of " & _TotalRec.ToString & "  (" & Format((_Rec * 100.0) / _TotalRec, "0.00") & " % ) ")

        '            If HI.HRCAL.Calculate.CalculateWeekEnd(HI.ST.UserInfo.UserName, R!FNHSysEmpID.ToString _
        '                                        , R!FNHSysEmpTypeId.ToString, HI.UL.ULDate.ConvertEnDB(FTStartDate.Text), HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) _
        '                                        , FTPayYear.Text, FTPayTerm.Text, FDPayDate.Text, R!FTDeligentCode.ToString, _TmpCalType.ToString _
        '                                        , False, _StateVacationRet, _FTStaDeductAbsent, _FTStaCalPayRoll, _FNStateSalaryType, _FNWorkDayInWeek, _FNWorkDayInMonth) = False Then

        '                _dttmpemp.Rows.Add(R!FTEmpCode.ToString, R!FTEmpName.ToString)

        '                _RecError = _RecError + 1

        '            End If

        '        Next

        '    Next

        '    _Spls.Close()

        '    HI.MG.ShowMsg.mInvalidData("", 1105030002, Me.Text, (_Rec - _RecError).ToString & " Records  ")

        '    If _dttmpemp.Rows.Count > 0 Then
        '        HI.MG.ShowMsg.mInvalidData("พบข้อมูล วันทำงานงวด ยังไม่มีการ Accept ไม่สามารถทำการคำนวณได้ !!!", 1175030002, Me.Text)

        '        With _ListNotCal
        '            .ogclist.DataSource = _dttmpemp.Copy
        '            .ShowDialog()
        '        End With

        '    End If

        '    _dttmpemp.Dispose()
        'Else

        '    HI.MG.ShowMsg.mInvalidData("", 1104040001, Me.Text)

        '    If HI.UL.ULDate.CheckDate(Me.FTStartDate.Text) = "" Then
        '        FTStartDate.Focus()
        '    ElseIf HI.UL.ULDate.CheckDate(Me.FTEndDate.Text) = "" Then
        '        FTEndDate.Focus()
        '    End If

        'End If



    End Sub


    Private Sub FNHSysEmpTypeId_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FNHSysEmpTypeId.EditValueChanged
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.FNHSysEmpTypeId_ValueChanged(AddressOf FNHSysEmpTypeId_EditValueChanged), New Object() {sender, e})
            Else
                If FNHSysEmpTypeId.Text <> "" Then
                    Dim _Qry As String = ""
                    Dim _Dt As DataTable

                    _Qry = "SELECT TOP 1  FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WITH(NOLOCK) WHERE FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "' AND FNHSysCmpId=" & HI.ST.SysInfo.CmpID
                    FNHSysEmpTypeId.Properties.Tag = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0"))

                    _Qry = " SELECT        D.FTPayTerm, D.FTPayYear, D.FNHSysEmpTypeId, D.FNMonth, D.FTTermOfMonth"
                    _Qry &= vbCrLf & " , D.FDPayDate, D.FDCalDateBegin, D.FDCalDateEnd, D.FDDateClose, "
                    _Qry &= vbCrLf & "  D.FTStateTermEndOfYear,D.FDPayDate "

                    If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                        _Qry &= vbCrLf & " , V_ListMonth.FTNameTH  AS FTMonth "
                    Else
                        _Qry &= vbCrLf & " , V_ListMonth.FTNameEN AS FTMonth "
                    End If

                    _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayHD AS H WITH (NOLOCK) INNER JOIN"
                    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS D WITH (NOLOCK) ON H.FTPayTerm = D.FTPayTerm "
                    _Qry &= vbCrLf & "  AND H.FTPayYear = D.FTPayYear AND "
                    _Qry &= vbCrLf & "    H.FNHSysEmpTypeId = D.FNHSysEmpTypeId INNER JOIN"
                    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_ListMonth ON D.FNMonth = V_ListMonth.FNListIndex"
                    _Qry &= vbCrLf & " WHERE H.FNHSysEmpTypeId =" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "

                    _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                    For Each R As DataRow In _Dt.Rows

                        Me.FTStartDate.Text = HI.UL.ULDate.ConvertEN(R!FDCalDateBegin.ToString)
                        Me.FTEndDate.Text = HI.UL.ULDate.ConvertEN(R!FDCalDateEnd.ToString)
                        Me.FTMonth.Text = R!FTMonth.ToString
                        Me.FTPayTerm.Text = R!FTPayTerm.ToString
                        Me.FTPayYear.Text = R!FTPayYear.ToString
                        Me.FDPayDate.Text = HI.UL.ULDate.ConvertEN(R!FDPayDate.ToString)

                        Exit For

                    Next

                End If
            End If

        Catch ex As Exception
        End Try

    End Sub
    Private ds As DataSet
    Private Sub wCalculateWeekend_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Me.FNEmpTypeGroup.SelectedIndex = -1
        ''   ds = New DataSet()

        bindEmptypeGroup()
        bindEmptype()


    End Sub

    Private Sub bindEmptypeGroup()
        Dim _Qry As String
        Dim dt As DataTable
        Dim _Dt As DataTable
        _Qry = " SELECT     '0' AS FTSelect , E.FNEmpTypeGroup AS FNEmpTypeGroupID, L.FTNameEN AS 'FTEmpTypeGroup' "
        '_Qry &= vbCrLf & ", MIN(D.FDCalDateBegin) AS 'FDCalDateBegin', MAX(D.FDCalDateEnd) As 'FDCalDateEnd', MAX(D.FDPayDate) AS 'FDPayDate' "
        _Qry &= vbCrLf & " ,convert(varchar, CAST(MIN(D.FDCalDateBegin) AS date), 103)  AS 'FDCalDateBegin'  "
        _Qry &= vbCrLf & "  ,convert(varchar, CAST(MAX(D.FDCalDateEnd) AS date), 103) As 'FDCalDateEnd'  "
        _Qry &= vbCrLf & "  ,convert(varchar,  CAST(MAX(D.FDPayDate) AS date), 103) AS 'FDPayDate' "
        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayHD AS H WITH (NOLOCK) INNER JOIN "
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType E ON H.FNHSysEmpTypeId= E.FNHSysEmpTypeId INNER JOIN "
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS D WITH (NOLOCK) ON H.FTPayTerm = D.FTPayTerm AND H.FTPayYear = D.FTPayYear AND "
        _Qry &= vbCrLf & " H.FNHSysEmpTypeId = D.FNHSysEmpTypeId INNER JOIN "
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_ListMonth ON D.FNMonth = V_ListMonth.FNListIndex "
        _Qry &= vbCrLf & " OUTER APPLY (SELECT TOP 20 * FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.[HSysListData] WHERE FTListName='FNEmpTypeGroup' AND E.FNEmpTypeGroup=FNListIndex ) L "
        _Qry &= vbCrLf & " WHERE E.FTStateActive = '1' AND E.FNHSysCmpId= " & HI.ST.SysInfo.CmpID & " "
        _Qry &= vbCrLf & " GROUP BY FNEmpTypeGroup,L.FTNameEN "
        _Qry &= vbCrLf & " ORDER BY L.FTNameEN "
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        Me.ogcemptypeGroup.DataSource = dt


        If _Dt Is Nothing Then
            _Dt = dt.Copy
        Else
            _Dt.Merge(dt.Copy)
        End If
        ''  Return dt
        dt.Dispose()

    End Sub


    Private Sub bindEmptype()
        Try

            Dim dtT As DataTable
            Dim _Qry As String

            'Dim _NewValue As String

            'Dim _FNEmpTypeGroupID As Integer
            'Dim _DtT As DataTable

            'Dim _dt_c As DataTable

            'Dim StrWhere As String = ""
            'Dim FNEmpTypeGroupID As String = ""

            'If StrWhere = "" And _FNEmpTypeGroupID.ToString <> "" Then
            '    StrWhere = "'" & _FNEmpTypeGroupID & "'"
            'End If

            'If StrWhere <> "" Then


            _Qry = " SELECT     '0' AS FTSelect  ,D.FTPayTerm, D.FTPayYear, D.FNHSysEmpTypeId, D.FNMonth, D.FTTermOfMonth , FTEmpTypeCode, FTEmpTypeNameEN AS FTDescription , L.FTNameEN AS 'FTEmpTypeGroup',  E.FNEmpTypeGroup AS FNEmpTypeGroupID "
            _Qry &= vbCrLf & " , convert(varchar, CAST(D.FDPayDate AS date), 103) AS 'FDPayDate',convert(varchar, CAST(D.FDCalDateBegin AS date), 103) AS 'FDCalDateBegin' "
            _Qry &= vbCrLf & " , convert(varchar, CAST(D.FDCalDateEnd AS date), 103) AS 'FDCalDateEnd',convert(varchar,   CAST(D.FDDateClose AS date), 103) AS 'FDDateClose' "
            _Qry &= vbCrLf & "  ,D.FTStateTermEndOfYear "

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & " , V_ListMonth.FTNameTH  AS FTMonth "
            Else
                _Qry &= vbCrLf & " , V_ListMonth.FTNameEN AS FTMonth "
            End If

            _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayHD AS H WITH (NOLOCK) INNER JOIN "
            _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType E ON H.FNHSysEmpTypeId= E.FNHSysEmpTypeId INNER JOIN "
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS D WITH (NOLOCK) ON H.FTPayTerm = D.FTPayTerm "
            _Qry &= vbCrLf & "  AND H.FTPayYear = D.FTPayYear AND "
            _Qry &= vbCrLf & "    H.FNHSysEmpTypeId = D.FNHSysEmpTypeId INNER JOIN "
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_ListMonth ON D.FNMonth = V_ListMonth.FNListIndex "
            _Qry &= vbCrLf & "   OUTER APPLY (SELECT TOP 20 * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].[dbo].[HSysListData] WHERE FTListName='FNEmpTypeGroup' AND E.FNEmpTypeGroup=FNListIndex ) L "
            _Qry &= vbCrLf & " WHERE E.FTStateActive = '1' AND E.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "
            _Qry &= vbCrLf & " ORDER BY L.FTNameEN ,D.FTPayTerm DESC , D.FTPayYear DESC  "

            dtT = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Me.ogcemptype.DataSource = dtT

            'If _DtT Is Nothing Then
            '    _DtT = dtT.Copy
            'Else
            '    _DtT.Merge(dtT.Copy)
            'End If
            'dtT.Dispose()

            'End If
            '' Return dtT
        Catch ex As Exception
            ' Return Nothing
        End Try

    End Sub

    Private Sub FNEmpTypeGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNEmpTypeGroup.SelectedIndexChanged
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New HI.Delegate.Dele.FNHSysEmpTypeId_ValueChanged(AddressOf FNHSysEmpTypeId_EditValueChanged), New Object() {sender, e})
            Else
                If FNEmpTypeGroup.SelectedIndex >= 0 Then
                    Dim _Qry As String = ""
                    Dim _Dt As DataTable

                    '_Qry = "SELECT TOP 1  FNHSysEmpTypeId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType WITH(NOLOCK) WHERE FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(FNHSysEmpTypeId.Text) & "' AND FNHSysCmpId=" & HI.ST.SysInfo.CmpID
                    'FNHSysEmpTypeId.Properties.Tag = Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0"))

                    _Qry = " SELECT   TOP 1     D.FTPayTerm, D.FTPayYear, D.FNHSysEmpTypeId, D.FNMonth, D.FTTermOfMonth"
                    _Qry &= vbCrLf & " , D.FDPayDate, D.FDCalDateBegin, D.FDCalDateEnd, D.FDDateClose, "
                    _Qry &= vbCrLf & "  D.FTStateTermEndOfYear,D.FDPayDate "

                    If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                        _Qry &= vbCrLf & " , V_ListMonth.FTNameTH  AS FTMonth "
                    Else
                        _Qry &= vbCrLf & " , V_ListMonth.FTNameEN AS FTMonth "
                    End If

                    _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayHD AS H WITH (NOLOCK) INNER JOIN "
                    _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType E ON H.FNHSysEmpTypeId= E.FNHSysEmpTypeId INNER JOIN "
                    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS D WITH (NOLOCK) ON H.FTPayTerm = D.FTPayTerm "
                    _Qry &= vbCrLf & "  AND H.FTPayYear = D.FTPayYear AND "
                    _Qry &= vbCrLf & "    H.FNHSysEmpTypeId = D.FNHSysEmpTypeId INNER JOIN"
                    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.V_ListMonth ON D.FNMonth = V_ListMonth.FNListIndex "
                    _Qry &= vbCrLf & " WHERE E.FNEmpTypeGroup =" & Val(FNEmpTypeGroup.SelectedIndex.ToString) & " AND E.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "
                    _Qry &= vbCrLf & " ORDER BY D.FTPayTerm DESC , D.FTPayYear DESC  "

                    _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                    For Each R As DataRow In _Dt.Rows

                        Me.FTStartDate.Text = HI.UL.ULDate.ConvertEN(R!FDCalDateBegin.ToString)
                        Me.FTEndDate.Text = HI.UL.ULDate.ConvertEN(R!FDCalDateEnd.ToString)
                        Me.FTMonth.Text = R!FTMonth.ToString
                        Me.FTPayTerm.Text = R!FTPayTerm.ToString
                        Me.FTPayYear.Text = R!FTPayYear.ToString
                        Me.FDPayDate.Text = HI.UL.ULDate.ConvertEN(R!FDPayDate.ToString)

                        Exit For

                    Next
                    Me.FNHSysEmpTypeId.Text = ""
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ReposCheckEmpTypeGroup_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ReposCheckEmpTypeGroup.EditValueChanging
        Try
            Dim dtT As DataTable
            Dim _Qry As String
            Dim _NewValue As String
            Dim _FNEmpTypeGroupID As Integer
            Dim _DtT As DataTable

            Dim _dt_c As DataTable
            ' Dim _Dt As DataTable

            _NewValue = e.NewValue.ToString()

            Dim _FTEmpTypeGroupName As String


            'With CType(ogvemptypeGroup.DataSource, DataTable)
            '    .AcceptChanges()
            '    _dt_c = .Copy()
            'End With

            With Me.ogvemptypeGroup

                _FTEmpTypeGroupName = .GetFocusedRowCellValue("FTEmpTypeGroup").ToString

            End With

            _FNEmpTypeGroupID = HI.TL.CboList.GetIndexByText("FNEmpTypeGroup", _FTEmpTypeGroupName)

            ''  Dim FNEmpTypeGroupID As String = ""
            'For Each R As DataRow In _dt_c.Select("FNEmpTypeGroupID=" & _FNEmpTypeGroupID)

            '    FNEmpTypeGroupID = R!FNEmpTypeGroupID.ToString


            'Next
            Dim _State As String = ""

            If _NewValue = "1" Then
                _State = "1"
            Else
                _State = "0"
            End If
            Dim a As String = ""
            Dim b As String = ""
            With ogvemptype
                If Not (.DataSource Is Nothing) And ogvemptype.RowCount > 0 Then

                    With ogvemptype
                        For I As Integer = 0 To .RowCount - 1


                            a = .GetRowCellValue(I, .Columns.ColumnByFieldName("FTEmpTypeGroup"))
                            If a <> "" Then
                                b = a
                            End If
                            If (.GetRowCellValue(I, .Columns.ColumnByFieldName("FTEmpTypeGroup")) = _FTEmpTypeGroupName) Then
                                .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)

                                'ogcemptype_FTEmpTypeGroup'
                                'ogcemptype_FTEmpTypeGroup'
                            End If

                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With

        Catch ex As Exception

        End Try


    End Sub


    Private Sub FNHSysEmpTypeGroup_lbl_Click(sender As Object, e As EventArgs) Handles FNHSysEmpTypeGroup_lbl.Click

    End Sub

    Private Sub ochkselectall_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ochkselectall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With ogcemptypeGroup
                If Not (.DataSource Is Nothing) And ogvemptypeGroup.RowCount > 0 Then

                    With ogvemptypeGroup
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With

            With ogcemptype
                If Not (.DataSource Is Nothing) And ogvemptype.RowCount > 0 Then

                    With ogvemptype
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

    'Private Sub ogv_MasterRowGetChildList(sender As Object, e As MasterRowGetChildListEventArgs) Handles ogvemptypeGroup.MasterRowGetChildList
    '    Dim grid As GridView
    '    grid = DirectCast(sender, GridView)
    '    e.ChildList = getlistchilt(grid.GetRowCellValue(grid.FocusedRowHandle, "FTEmpTypeGroup"))
    'End Sub

    'Private Function getlistchilt(EmpGrpId As String) As DataView
    '    Try
    '        Dim _dt As DataTable
    '        _dt = ds.Tables(1)
    '        Dim x As DataView
    '        x = New DataView(_dt)


    '        'x = _dt '.fill("FTEmpTypeGroup=" & EmpGrpId)
    '        x.RowFilter = "FTEmpTypeGroup='" & EmpGrpId & "'"
    '        x.AllowEdit = False

    '        Return x
    '    Catch ex As Exception

    '    End Try
    'End Function

    'Private Sub ogv_MasterRowGetRelationCount(sender As Object, e As MasterRowGetRelationCountEventArgs) Handles ogvemptypeGroup.MasterRowGetRelationCount
    '    e.RelationCount = 1
    'End Sub

    'Private Sub ogv_MasterRowGetRelationName(sender As Object, e As MasterRowGetRelationNameEventArgs) Handles ogvemptypeGroup.MasterRowGetRelationName
    '    e.RelationName = "EmpType"
    'End Sub

    'Private Sub ogv_MasterRowEmpty(sender As Object, e As MasterRowEmptyEventArgs) Handles ogvemptypeGroup.MasterRowEmpty
    '    e.IsEmpty = False
    'End Sub
#End Region


End Class