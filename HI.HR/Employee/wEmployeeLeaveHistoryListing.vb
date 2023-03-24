Imports System.IO
Imports DevExpress.Spreadsheet

Public Class wEmployeeLeaveHistoryListing

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ActualDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

        Call InitGrid()

    End Sub

    Private _ActualDate As String = ""
    ReadOnly Property ActualDate As String
        Get
            Return _ActualDate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property

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

#Region "Initial Grid"

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = "FTEmpCode"
        Dim sFieldSum As String = "" ' "FNLateNormalMin|FNLateNormalCut"
        Dim sFieldGrpCount As String = "FTEmpCode"
        Dim sFieldGrpSum As String = "" '"FNEmpWork|FNLateNormalMin|FNLateNormalCut"

        'T.FNLateNormalMin, T.FNLateNormalCut
        Dim sFieldCustomSum As String = "" '"FNTime|FNOT1|FNOT1_5|FNOT2|FNOT3|FNOT4"
        Dim sFieldCustomGrpSum As String = ""

        With ogv
            .ClearGrouping()
            .ClearDocument()
            .Columns("FTUnitSectCode").Group()

            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldCustomSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldCustomGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
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
#Region "MAIN PROC"




    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click

        Me.ogc.DataSource = Nothing
        HI.TL.HandlerControl.ClearControl(Me)

    End Sub

    Private Sub ProcessPreview(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Proc "

    Private Sub LoadData(Optional StateLeaveOnly As Boolean = False)
        Me.ogc.DataSource = Nothing

        Dim _Dt As DataTable
        Dim _Qry As String = ""
        Dim _TotalEmp As Integer = 0
        Dim _Count As Integer = 0
        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        _Qry = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTempEmpLeaveHistory  WHERE  UserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        _Qry = " SELECT     M.FNHSysEmpID, M.FTEmpCode"
        _Qry &= vbCrLf & " FROM        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS SH WITH (NOLOCK) ON M.FNHSysShiftID = SH.FNHSysShiftID "
        _Qry &= vbCrLf & "   INNER Join "
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS OrgDiv WITH (NOLOCK) ON M.FNHSysDivisonId = OrgDiv.FNHSysDivisonId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS OrgSect WITH (NOLOCK) ON M.FNHSysSectId = OrgSect.FNHSysSectId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS OrgUnitSect WITH (NOLOCK) ON M.FNHSysUnitSectId = OrgUnitSect.FNHSysUnitSectId "


        _Qry &= vbCrLf & "  WHERE  M.FTEmpCode <> ''  "
        _Qry &= vbCrLf & "  AND  M.FNHSysCmpId  =" & HI.ST.SysInfo.CmpID & "  "
        _Qry &= vbCrLf & "  AND (ISNULL(M.FDDateEnd,'') ='' OR  ISNULL(M.FDDateEnd,'')>='" & HI.UL.ULDate.ConvertEnDB(Me.FDStartDate.Text) & "'  )"

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
            _Qry &= vbCrLf & " AND  OrgDiv.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
        End If

        If Me.FNHSysDivisonIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  OrgDiv.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
        End If

        '------Criteria By Sect
        If Me.FNHSysSectId.Text <> "" Then
            _Qry &= vbCrLf & " AND  OrgSect.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
        End If

        If Me.FNHSysSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  OrgSect.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
        End If

        '------Criteria Unit Sect
        If Me.FNHSysUnitSectId.Text <> "" Then
            _Qry &= vbCrLf & " AND   OrgUnitSect.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
        End If

        If Me.FNHSysUnitSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND   OrgUnitSect.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
        End If

        _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)

        _Qry &= vbCrLf & " ORDER BY  M.FTEmpCode  "

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


        _TotalEmp = _Dt.Rows.Count
        _Count = 0

        For Each R As DataRow In _Dt.Rows
            _Count = _Count + 1
            _Spls.UpdateInformation("Calculating... Employee Code " & R!FTEmpCode.ToString & "  Record  " & _Count.ToString & " Of " & _TotalEmp.ToString & "  (" & Format((_Count * 100.0) / _TotalEmp, "0.00") & " % ) ")

            _Qry = "Exec  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_CALCULATE_EMP_LEAVHISTORY '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Integer.Parse(Val(R!FNHSysEmpID.ToString)) & ",'" & HI.UL.ULDate.ConvertEnDB(Me.FDStartDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FDEndDate.Text) & "' "
            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        Next

        _Qry = " SELECT       M.FNHSysEmpID, M.FTEmpCode"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal  Then
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

        _Qry &= vbCrLf & " , ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
        _Qry &= vbCrLf & " , ISNULL(Dept.FTDeptCode,'') AS FTDeptCode, ISNULL(DI.FTDivisonCode,'') AS FTDivisonCode"
        _Qry &= vbCrLf & " , ISNULL(ST.FTSectCode,'') AS FTSectCode,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode"
        _Qry &= vbCrLf & " , OrgPosit.FTPositCode "

        _Qry &= vbCrLf & " ,TT.FNL00Min, TT.FNL00Time, TT.FNL01Min, TT.FNL01Time, TT.FNL04Min, TT.FNL04Time"
        _Qry &= vbCrLf & " , TT.FNL05Min, TT.FNL05Time, TT.FNL06Min, TT.FNL06Time, TT.FNL07Min, TT.FNL07Time, TT.FNL08Min, TT.FNL08Time"
        _Qry &= vbCrLf & " ,TT.FNL09Min, TT.FNL09Time, TT.FNL97Min, TT.FNL97Time, TT.FNL98Min, TT.FNL98Time, TT.FNL99Min, TT.FNL99Time, TT.FNL16Min, TT.FNL16Time, TT.FNL02Min, TT.FNL02Time, TT.FNL17Min, TT.FNL17Time, TT.FNL03Min, TT.FNL03Time, TT.FNL18Min, TT.FNL18Time,TT.FNTotal"
        _Qry &= vbCrLf & " ,TT.FTL00Date, TT.FTL01Date, TT.FTL04Date, TT.FTL05Date, TT.FTL06Date"
        _Qry &= vbCrLf & " , TT.FTL07Date, TT.FTL08Date, TT.FTL09Date, TT.FTL97Date, TT.FTL98Date, TT.FTL99Date, TT.FTL16Date, TT.FTL02Date, TT.FTL17Date , TT.FTL03Date, TT.FTL18Date "

        _Qry &= vbCrLf & " ,TT.FNLateMin, TT.FNLateTime, TT.FNLateDate, TT.FNAbsentMin, TT.FNAbsentTime, TT.FNAbsentDate"
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS OrgPosit WITH (NOLOCK) ON M.FNHSysPositId = OrgPosit.FNHSysPositId "
        _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTempEmpLeaveHistory AS TT WITH(NOLOCK)"
        _Qry &= vbCrLf & "  ON  M.FNHSysEmpID = TT.FNHSysEmpID "
        _Qry &= vbCrLf & "  WHERE  TT.UserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  AND   M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "


        If (StateLeaveOnly) Then
            _Qry &= vbCrLf & "  AND TT.FNTotal > 0  "
        End If

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        _Qry = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTempEmpLeaveHistory  WHERE  UserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.ogc.DataSource = _Dt
        ogv.ExpandAllGroups()
        ogv.RefreshData()
        Me.otb.SelectedTabPageIndex = 0

        Dim tmpdt As New DataTable
        tmpdt = _Dt.Clone

        Try
            tmpdt.Rows.Clear()
            _Dt.Select("FNL00Min>0").CopyToDataTable(tmpdt, LoadOption.Upsert)
            Me.ogc00.DataSource = tmpdt.Copy

            tmpdt.Rows.Clear()
            _Dt.Select("FNL01Min>0").CopyToDataTable(tmpdt, LoadOption.Upsert)
            Me.ogc1.DataSource = tmpdt.Copy

            tmpdt.Rows.Clear()
            _Dt.Select("FNL04Min>0").CopyToDataTable(tmpdt, LoadOption.Upsert)
            Me.ogc4.DataSource = tmpdt.Copy

            tmpdt.Rows.Clear()
            _Dt.Select("FNL05Min>0").CopyToDataTable(tmpdt, LoadOption.Upsert)
            Me.ogc5.DataSource = tmpdt.Copy

            tmpdt.Rows.Clear()
            _Dt.Select("FNL06Min>0").CopyToDataTable(tmpdt, LoadOption.Upsert)
            Me.ogc6.DataSource = tmpdt.Copy

            tmpdt.Rows.Clear()
            _Dt.Select("FNL07Min>0").CopyToDataTable(tmpdt, LoadOption.Upsert)
            Me.ogc7.DataSource = tmpdt.Copy

            tmpdt.Rows.Clear()
            _Dt.Select("FNL08Min>0").CopyToDataTable(tmpdt, LoadOption.Upsert)
            Me.ogc8.DataSource = tmpdt.Copy

            tmpdt.Rows.Clear()
            _Dt.Select("FNL09Min>0").CopyToDataTable(tmpdt, LoadOption.Upsert)
            Me.ogc9.DataSource = tmpdt.Copy

            tmpdt.Rows.Clear()
            _Dt.Select("FNL97Min>0").CopyToDataTable(tmpdt, LoadOption.Upsert)
            Me.ogc97.DataSource = tmpdt.Copy

            tmpdt.Rows.Clear()
            _Dt.Select("FNL98Min>0").CopyToDataTable(tmpdt, LoadOption.Upsert)
            Me.ogc98.DataSource = tmpdt.Copy

            tmpdt.Rows.Clear()
            _Dt.Select("FNL99Min>0").CopyToDataTable(tmpdt, LoadOption.Upsert)
            Me.ogc99.DataSource = tmpdt.Copy

            tmpdt.Rows.Clear()
            _Dt.Select("FNLateMin>0").CopyToDataTable(tmpdt, LoadOption.Upsert)
            Me.ogclate.DataSource = tmpdt.Copy

            tmpdt.Rows.Clear()
            _Dt.Select("FNAbsentMin>0").CopyToDataTable(tmpdt, LoadOption.Upsert)
            Me.ogcabsent.DataSource = tmpdt.Copy

            tmpdt.Rows.Clear()
            _Dt.Select("FNL16Min>0").CopyToDataTable(tmpdt, LoadOption.Upsert)
            Me.ogc16.DataSource = tmpdt.Copy


            tmpdt.Rows.Clear()
            _Dt.Select("FNL17Min>0").CopyToDataTable(tmpdt, LoadOption.Upsert)
            Me.ogc17.DataSource = tmpdt.Copy

            tmpdt.Rows.Clear()
            _Dt.Select("FNL02Min>0").CopyToDataTable(tmpdt, LoadOption.Upsert)
            Me.ogc2.DataSource = tmpdt.Copy

            tmpdt.Rows.Clear()
            _Dt.Select("FNL03Min>0").CopyToDataTable(tmpdt, LoadOption.Upsert)
            Me.ogc3.DataSource = tmpdt.Copy

            tmpdt.Rows.Clear()
            _Dt.Select("FNL18Min>0").CopyToDataTable(tmpdt, LoadOption.Upsert)
            Me.ogc18.DataSource = tmpdt.Copy


        Catch ex As Exception

        End Try


        _Spls.Close()
    End Sub
#End Region

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If IsDate(FDStartDate.Text) And IsDate(FDEndDate.Text) Then
            Me.otb.SelectedTabPageIndex = 0
            Call LoadData()
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการระบุวันที่ !!!", 1406060011, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

    End Sub

    Private Sub wEmployeeWeekly_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If FNHSysEmpTypeId.Enabled Then
            FNHSysEmpTypeId.Focus()
        End If
    End Sub

    Private Sub wEmployeeWeekly_BackgroundImageLayoutChanged(sender As Object, e As System.EventArgs) Handles Me.BackgroundImageLayoutChanged

    End Sub

    Private Sub wEmployeeWeekly_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

        Call HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)
    End Sub

    Private Sub ogv_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogv.RowCellStyle
        With ogv

            Try
                If Val("" & .GetRowCellValue(e.RowHandle, "FNTotal")) > 0 Then
                    e.Appearance.ForeColor = Drawing.Color.Red
                End If
            Catch ex As Exception
            End Try


        End With
    End Sub

    Private Sub ocmloadtimeerror_Click(sender As Object, e As EventArgs) Handles ocmloadempleaveonly.Click
        If IsDate(FDStartDate.Text) And IsDate(FDEndDate.Text) Then
            Me.otb.SelectedTabPageIndex = 0
            Call LoadData(True)
        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการระบุวันที่ !!!", 1406060011, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click

        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogv)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)

    End Sub

    Private Sub ogv99_RowCountChanged(sender As Object, e As EventArgs) Handles ogv99.RowCountChanged
        Me.otp99.PageVisible = (ogv99.RowCount > 0)
    End Sub

    Private Sub ogv98_RowCountChanged(sender As Object, e As EventArgs) Handles ogv98.RowCountChanged
        Me.otp98.PageVisible = (ogv98.RowCount > 0)
    End Sub

    Private Sub ogv97_RowCountChanged(sender As Object, e As EventArgs) Handles ogv97.RowCountChanged
        Me.otp97.PageVisible = (ogv97.RowCount > 0)
    End Sub

    Private Sub ogv9_RowCountChanged(sender As Object, e As EventArgs) Handles ogv9.RowCountChanged
        Me.otp09.PageVisible = (ogv9.RowCount > 0)
    End Sub

    Private Sub ogv8_RowCountChanged(sender As Object, e As EventArgs) Handles ogv8.RowCountChanged
        Me.otp08.PageVisible = (ogv8.RowCount > 0)
    End Sub

    Private Sub ogv7_RowCountChanged(sender As Object, e As EventArgs) Handles ogv7.RowCountChanged
        Me.otp07.PageVisible = (ogv7.RowCount > 0)
    End Sub

    Private Sub ogv6_RowCountChanged(sender As Object, e As EventArgs) Handles ogv6.RowCountChanged
        Me.otp06.PageVisible = (ogv7.RowCount > 0)
    End Sub

    Private Sub ogv5_RowCountChanged(sender As Object, e As EventArgs) Handles ogv5.RowCountChanged
        Me.otp05.PageVisible = (ogv5.RowCount > 0)
    End Sub

    Private Sub ogv4_RowCountChanged(sender As Object, e As EventArgs) Handles ogv4.RowCountChanged
        Me.otp04.PageVisible = (ogv4.RowCount > 0)
    End Sub

    Private Sub ogv1_RowCountChanged(sender As Object, e As EventArgs) Handles ogv1.RowCountChanged
        Me.otp01.PageVisible = (ogv1.RowCount > 0)
    End Sub

    Private Sub ogv00_RowCountChanged(sender As Object, e As EventArgs) Handles ogv00.RowCountChanged
        Me.otp00.PageVisible = (ogv00.RowCount > 0)
    End Sub

    Private Sub ogvlate_RowCountChanged(sender As Object, e As EventArgs) Handles ogvlate.RowCountChanged
        Me.otplate.PageVisible = (ogvlate.RowCount > 0)
    End Sub

    Private Sub ogvabsent_RowCountChanged(sender As Object, e As EventArgs) Handles ogvabsent.RowCountChanged
        Me.otpabsent.PageVisible = (ogvabsent.RowCount > 0)
    End Sub

    Private Sub ogv2_RowCountChanged(sender As Object, e As EventArgs) Handles ogv2.RowCountChanged
        Me.otp02.PageVisible = (ogv2.RowCount > 0)
    End Sub

    Private Sub ogv17_RowCountChanged(sender As Object, e As EventArgs) Handles ogv17.RowCountChanged
        Me.otp17.PageVisible = (ogv17.RowCount > 0)
    End Sub


    Private Sub ogv3_RowCountChanged(sender As Object, e As EventArgs) Handles ogv3.RowCountChanged
        Me.otp3.PageVisible = (ogv3.RowCount > 0)
    End Sub

    Private Sub ogv18_RowCountChanged(sender As Object, e As EventArgs) Handles ogv18.RowCountChanged
        Me.otp18.PageVisible = (ogv18.RowCount > 0)
    End Sub

    Private Sub ocmexporttoexcel_Click(sender As Object, e As EventArgs) Handles ocmexporttoexcel.Click
        Try

            'If Me.ogv.RowCount <= 0 Then
            '    HI.MG.ShowMsg.mInfo("ไม่พบข้อมํลที่ต้องการทำการ Export !!!", 1406110009, Me.Text)
            '    Exit Sub
            'End If

            'Dim Op As New System.Windows.Forms.SaveFileDialog
            'Op.Filter = "Excel Files(*.xlsx)|*.xlsx"
            'Op.ShowDialog()

            'Try
            '    If Op.FileName <> "" Then

            '        Dim dtfilename As New DataTable
            '        dtfilename.Columns.Add("FTFileName", GetType(String))


            '        Dim destWorkBook = New Workbook
            '        destWorkBook.CreateNewDocument()

            '        Dim options As New DevExpress.XtraPrinting.XlsxExportOptions
            '        options.ExportMode = DevExpress.XtraPrinting.XlsxExportMode.SingleFile
            '        'Dim options As New destWorkBook.optionmode
            '        'options.ExportMode = DevExpress.XtraPrinting.XlsxExportMode.SingleFile

            '        If Me.ogv.RowCount > 0 Then
            '            options.SheetName = Me.otpsummary.Text
            '            Me.ogv.ExportToXlsx(Op.FileName & "9999", options)

            '            dtfilename.Rows.Add("9999")
            '        End If


            '        If Me.ogv00.RowCount > 0 Then

            '            options.SheetName = Me.otp00.Text
            '            Me.ogv00.ExportToXlsx(Op.FileName & "00", options)
            '            dtfilename.Rows.Add("00")

            '        End If

            '        If Me.ogv1.RowCount > 0 Then

            '            options.SheetName = Me.otp01.Text
            '            Me.ogv1.ExportToXlsx(Op.FileName & "01", options)

            '            dtfilename.Rows.Add("01")

            '        End If

            '        If Me.ogv4.RowCount > 0 Then

            '            options.SheetName = Me.otp04.Text
            '            Me.ogv4.ExportToXlsx(Op.FileName & "04", options)

            '            dtfilename.Rows.Add("04")
            '        End If

            '        If Me.ogv5.RowCount > 0 Then

            '            options.SheetName = Me.otp05.Text
            '            Me.ogv5.ExportToXlsx(Op.FileName & "05", options)

            '            dtfilename.Rows.Add("05")
            '        End If

            '        If Me.ogv6.RowCount > 0 Then

            '            options.SheetName = Me.otp06.Text
            '            Me.ogv6.ExportToXlsx(Op.FileName & "06", options)

            '            dtfilename.Rows.Add("06")
            '        End If

            '        If Me.ogv7.RowCount > 0 Then

            '            options.SheetName = Me.otp07.Text
            '            Me.ogv7.ExportToXlsx(Op.FileName & "07", options)

            '            dtfilename.Rows.Add("07")
            '        End If

            '        If Me.ogv8.RowCount > 0 Then

            '            options.SheetName = Me.otp08.Text
            '            Me.ogv8.ExportToXlsx(Op.FileName & "08", options)

            '            dtfilename.Rows.Add("08")
            '        End If

            '        If Me.ogv9.RowCount > 0 Then

            '            options.SheetName = Me.otp09.Text
            '            Me.ogv9.ExportToXlsx(Op.FileName & "09", options)

            '            dtfilename.Rows.Add("09")
            '        End If

            '        If Me.ogv97.RowCount > 0 Then

            '            options.SheetName = Me.otp97.Text
            '            Me.ogv97.ExportToXlsx(Op.FileName & "97", options)

            '            dtfilename.Rows.Add("97")
            '        End If

            '        If Me.ogv98.RowCount > 0 Then

            '            options.SheetName = Me.otp98.Text
            '            Me.ogv98.ExportToXlsx(Op.FileName & "98", options)

            '            dtfilename.Rows.Add("98")
            '        End If

            '        If Me.ogv99.RowCount > 0 Then

            '            options.SheetName = Me.otp99.Text
            '            Me.ogv99.ExportToXlsx(Op.FileName & "99", options)

            '            dtfilename.Rows.Add("99")
            '        End If

            '        If Me.ogvlate.RowCount > 0 Then

            '            options.SheetName = Me.otplate.Text
            '            Me.ogvlate.ExportToXlsx(Op.FileName & "Late", options)

            '            dtfilename.Rows.Add("Late")
            '        End If

            '        If Me.ogvabsent.RowCount > 0 Then

            '            options.SheetName = Me.otpabsent.Text
            '            Me.ogvabsent.ExportToXlsx(Op.FileName & "Absent", options)

            '            dtfilename.Rows.Add("Absent")
            '        End If

            '        If Me.ogv2.RowCount > 0 Then

            '            options.SheetName = Me.otp02.Text
            '            Me.ogv2.ExportToXlsx(Op.FileName & "02", options)

            '            dtfilename.Rows.Add("02")
            '        End If
            '        If Me.ogv17.RowCount > 0 Then

            '            options.SheetName = Me.otp17.Text
            '            Me.ogv17.ExportToXlsx(Op.FileName & "17", options)

            '            dtfilename.Rows.Add("17")
            '        End If

            '        For Each R As DataRow In dtfilename.Rows
            '            Dim sourceWorkBook As New DevExpress.Spreadsheet.Workbook()
            '            sourceWorkBook.LoadDocument(Op.FileName & R!FTFileName.ToString)

            '            For Each sheet As DevExpress.Spreadsheet.Worksheet In sourceWorkBook.Worksheets
            '                Dim temp As DevExpress.Spreadsheet.Worksheet = destWorkBook.Worksheets.Add()
            '                temp.CopyFrom(sheet)
            '                temp.Name = sheet.Name
            '            Next

            '            sourceWorkBook.Dispose()
            '            Try
            '                Kill(Op.FileName & R!FTFileName.ToString)
            '            Catch ex As Exception
            '            End Try

            '        Next

            '        dtfilename.Dispose()


            '        destWorkBook.Worksheets.RemoveAt(0)
            '        Try
            '            destWorkBook.SaveDocument(Op.FileName)
            '            destWorkBook.Dispose()
            '        Catch ex As Exception
            '            HI.MG.ShowMsg.mInfo("คุณเปิด File ไม่สามารถทำการเขียนทับได้ อยู่กรุณาทำการปิด ก่อน", 1406120001, Me.Text)
            '            Exit Sub
            '            destWorkBook.Dispose()
            '        End Try


            '        Try
            '            Process.Start(Op.FileName)
            '        Catch ex As Exception
            '        End Try

            '    End If
            'Catch ex As Exception
            'End Try

        Catch ex As Exception
        End Try
    End Sub
    
    Private Sub ocmpreview_Click(sender As Object, e As EventArgs)
        If Me.FDStartDate.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Human Report\"
                .ReportName = "HRRptVacationLeave.rpt"

                'Dim _Fm As String = " {THRMEmployee.FDDateEnd} <='" & HI.UL.ULDate.ConvertEnDB(Me.FDEndDate.Text) & "'"
                Dim _Fm As String = "( {THRMEmployee.FDDateEnd} =''OR {THRMEmployee.FDDateEnd} >='" & HI.UL.ULDate.ConvertEnDB(Me.FDStartDate.Text) & "')"
                If Me.FNHSysEmpTypeId.Text <> "" Then
                    _Fm &= vbCrLf & " AND {THRMEmpType.FTEmpTypeCode}='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
                End If

                '------Criteria By Employeee Code
                If Me.FNHSysEmpId.Text <> "" Then
                    _Fm &= vbCrLf & " AND {THRMEmployee.FTEmpCode} >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
                End If

                If Me.FNHSysEmpIdTo.Text <> "" Then
                    _Fm &= vbCrLf & " AND {THRMEmployee.FTEmpCode} <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
                End If

                '------Criteria By Department
                If Me.FNHSysDeptId.Text <> "" Then
                    _Fm &= vbCrLf & " AND  {TCNMDepartment.FTDeptCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
                End If

                If Me.FNHSysDeptIdTo.Text <> "" Then
                    _Fm &= vbCrLf & " AND  {TCNMDepartment.FTDeptCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
                End If

                '------Criteria By Division
                If Me.FNHSysDivisonId.Text <> "" Then
                    _Fm &= vbCrLf & " AND  {TCNMDivision.FTDivisonCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
                End If

                If Me.FNHSysDivisonIdTo.Text <> "" Then
                    _Fm &= vbCrLf & " AND  {TCNMDivision.FTDivisonCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
                End If

                '------Criteria By Sect
                If Me.FNHSysSectId.Text <> "" Then
                    _Fm &= vbCrLf & " AND  {TCNMSect.FTSectCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
                End If

                If Me.FNHSysSectIdTo.Text <> "" Then
                    _Fm &= vbCrLf & " AND  {TCNMSect.FTSectCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
                End If

                '------Criteria Unit Sect
                If Me.FNHSysUnitSectId.Text <> "" Then
                    _Fm &= vbCrLf & " AND   {TCNMUnitSect.FTUnitSectCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
                End If

                If Me.FNHSysUnitSectIdTo.Text <> "" Then
                    _Fm &= vbCrLf & " AND   {TCNMUnitSect.FTUnitSectCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
                End If

                .Formular = _Fm
                .Preview()
            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTDateRequest_lbl.Text)
            FDStartDate.Focus()
        End If
    End Sub

    Private Sub ocmvacationpreview_Click(sender As Object, e As EventArgs) Handles ocmvacationpreview.Click
        Dim _QrySql As String

        If Me.FDEndDate.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Human Report\"
                .ReportName = "HRRptVacationLeave.rpt"

                _QrySql = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.TRPTTempEmpVacation WHERE UserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _QrySql &= vbCrLf & "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.TRPTTempEmpVacation(UserLogin, FNHSysEmpID, FNEmpVacation, FNEmpUsedVacation )"

                _QrySql &= vbCrLf & "SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS UserLogin, M.FNHSysEmpID"
                _QrySql &= vbCrLf & "   , [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_Get_Emp_Vacation(M.FNHSysEmpID,M.FNHSysEmpTypeId,ISNULL(FDDateStart,''),CASE WHEN ISNULL(FDDateEnd,'') ='' THEN '" & HI.UL.ULDate.ConvertEnDB(Me.FDEndDate.Text) & "' ELSE CASE WHEN ISNULL(FDDateEnd,'') >  '" & HI.UL.ULDate.ConvertEnDB(Me.FDEndDate.Text) & "' THEN  '" & HI.UL.ULDate.ConvertEnDB(Me.FDEndDate.Text) & "' ELSE ISNULL(FDDateEnd,'') END END  ,ISNULL(FDDateProbation,'')) AS FNEmpVacation "
                _QrySql &= vbCrLf & "   ,(ISNULL(B.FNTotalMinute,0) /480.00) AS FNEmpUsedVacation "
                _QrySql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee  AS M WITH(NOLOCK) "
                _QrySql &= vbCrLf & "   LEFT OUTER JOIN  "
                _QrySql &= vbCrLf & "   ( "
                _QrySql &= vbCrLf & "   SELECT      FNHSysEmpID, SUM(FNTotalMinute) AS FNTotalMinute "
                _QrySql &= vbCrLf & "   FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave  WITH(NOLOCK) "
                _QrySql &= vbCrLf & "   WHERE        (FTLeaveType = '98') AND (FTDateTrans >= LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FDEndDate.Text) & "',4)+'/01/01') AND FTDateTrans <= '" & HI.UL.ULDate.ConvertEnDB(Me.FDEndDate.Text) & "' "
                _QrySql &= vbCrLf & "   GROUP BY FNHSysEmpID "
                _QrySql &= vbCrLf & "   ) "
                _QrySql &= vbCrLf & "   AS B ON M.FNHSysEmpID = B.FNHSysEmpID "
                _QrySql &= vbCrLf & "   INNER Join "
                _QrySql &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
                _QrySql &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
                _QrySql &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS OrgDiv WITH (NOLOCK) ON M.FNHSysDivisonId = OrgDiv.FNHSysDivisonId "
                _QrySql &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS OrgSect WITH (NOLOCK) ON M.FNHSysSectId = OrgSect.FNHSysSectId "
                _QrySql &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS OrgUnitSect WITH (NOLOCK) ON M.FNHSysUnitSectId = OrgUnitSect.FNHSysUnitSectId "



                _QrySql &= vbCrLf & "  WHERE  M.FTEmpCode <> ''  "
                _QrySql &= vbCrLf & "   AND  M.FNHSysCmpId  =" & HI.ST.SysInfo.CmpID & "  "
                _QrySql &= vbCrLf & "  AND (ISNULL(FDDateEnd,'') ='' OR ISNULL(FDDateEnd,'')>=LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FDEndDate.Text) & "',4)+'/01/01')  "

                _QrySql = HI.ST.Security.PermissionFilterEmployee(_QrySql)

                If Me.FNHSysEmpTypeId.Text <> "" Then
                    _QrySql &= vbCrLf & " AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
                End If

                '------Criteria By Employeee Code
                If Me.FNHSysEmpId.Text <> "" Then
                    _QrySql &= vbCrLf & " AND M.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
                End If

                If Me.FNHSysEmpIdTo.Text <> "" Then
                    _QrySql &= vbCrLf & " AND M.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
                End If

                '------Criteria By Department
                If Me.FNHSysDeptId.Text <> "" Then
                    _QrySql &= vbCrLf & " AND  Dept.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
                End If

                If Me.FNHSysDeptIdTo.Text <> "" Then
                    _QrySql &= vbCrLf & " AND  Dept.FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
                End If

                '------Criteria By Division
                If Me.FNHSysDivisonId.Text <> "" Then
                    _QrySql &= vbCrLf & " AND  OrgDiv.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
                End If

                If Me.FNHSysDivisonIdTo.Text <> "" Then
                    _QrySql &= vbCrLf & " AND  OrgDiv.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
                End If

                '------Criteria By Sect
                If Me.FNHSysSectId.Text <> "" Then
                    _QrySql &= vbCrLf & " AND  OrgSect.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
                End If

                If Me.FNHSysSectIdTo.Text <> "" Then
                    _QrySql &= vbCrLf & " AND  OrgSect.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
                End If

                '------Criteria Unit Sect
                If Me.FNHSysUnitSectId.Text <> "" Then
                    _QrySql &= vbCrLf & " AND   OrgUnitSect.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
                End If

                If Me.FNHSysUnitSectIdTo.Text <> "" Then
                    _QrySql &= vbCrLf & " AND   OrgUnitSect.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
                End If



                HI.Conn.SQLConn.ExecuteNonQuery(_QrySql, Conn.DB.DataBaseName.DB_HR)

                Dim _Fm As String = " {V_EmpVacation.UserLogin}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "

                If Me.FNHSysEmpTypeId.Text <> "" Then
                    _Fm &= vbCrLf & " AND {THRMEmpType.FTEmpTypeCode}='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
                End If

                '------Criteria By Employeee Code
                If Me.FNHSysEmpId.Text <> "" Then
                    _Fm &= vbCrLf & " AND {THRMEmployee.FTEmpCode} >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
                End If

                If Me.FNHSysEmpIdTo.Text <> "" Then
                    _Fm &= vbCrLf & " AND {THRMEmployee.FTEmpCode} <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
                End If

                '------Criteria By Department
                If Me.FNHSysDeptId.Text <> "" Then
                    _Fm &= vbCrLf & " AND  {TCNMDepartment.FTDeptCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
                End If

                If Me.FNHSysDeptIdTo.Text <> "" Then
                    _Fm &= vbCrLf & " AND  {TCNMDepartment.FTDeptCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
                End If

                '------Criteria By Division
                If Me.FNHSysDivisonId.Text <> "" Then
                    _Fm &= vbCrLf & " AND  {TCNMDivision.FTDivisonCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
                End If

                If Me.FNHSysDivisonIdTo.Text <> "" Then
                    _Fm &= vbCrLf & " AND  {TCNMDivision.FTDivisonCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
                End If

                '------Criteria By Sect
                If Me.FNHSysSectId.Text <> "" Then
                    _Fm &= vbCrLf & " AND  {TCNMSect.FTSectCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
                End If

                If Me.FNHSysSectIdTo.Text <> "" Then
                    _Fm &= vbCrLf & " AND  {TCNMSect.FTSectCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
                End If

                '------Criteria Unit Sect
                If Me.FNHSysUnitSectId.Text <> "" Then
                    _Fm &= vbCrLf & " AND   {TCNMUnitSect.FTUnitSectCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
                End If

                If Me.FNHSysUnitSectIdTo.Text <> "" Then
                    _Fm &= vbCrLf & " AND   {TCNMUnitSect.FTUnitSectCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
                End If

                .Formular = _Fm
                .Preview()

            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FDEndDate_lbl.Text)
            FDEndDate.Focus()
        End If

             
    End Sub
End Class