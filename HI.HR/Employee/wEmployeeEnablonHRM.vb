Public Class wEmployeeEnablonHRM

    Private Const _DBEnum As Integer = HI.Conn.DB.DataBaseName.DB_HR
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Private _FormGridDetail As New List(Of HI.TL.DynamicGrid)()
    Private RunEmpCodeByTypeAndSect As Boolean = True
    Private Const _tHyphenSign As String = "/"

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
    Private _SysTableName As String = ""
    Public Property SysTableName As String
        Get
            Return _SysTableName
        End Get
        Set(ByVal value As String)
            _SysTableName = value
        End Set
    End Property

    Private _SysDocType As String = ""
    Public Property SysDocType As String
        Get
            Return _SysDocType
        End Get
        Set(ByVal value As String)
            _SysDocType = value
        End Set
    End Property

    Private _TableName As String = ""
    Public Property TableName As String
        Get
            Return _TableName
        End Get
        Set(ByVal value As String)
            _TableName = value
        End Set
    End Property
    Public ReadOnly Property MainKey As String
        Get
            Return _FormHeader(0).MainKey
        End Get
    End Property

#End Region
#Region "Procedure"

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



    Private Sub wEmployeeExpiry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        ' Call InitGrid()0
        '  Me.LoadDataInfo()
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        HI.TL.HandlerControl.ClearControl(ogcend)
        ogcend.DataSource = Nothing
        HI.TL.HandlerControl.ClearControl(ogcmanday)
        ogcmanday.DataSource = Nothing
        HI.TL.HandlerControl.ClearControl(ogcnew)
        ogcnew.DataSource = Nothing
        HI.TL.HandlerControl.ClearControl(ogcsumleave)
        ogcsumleave.DataSource = Nothing
        HI.TL.HandlerControl.ClearControl(ogcsummary)
        ogcsummary.DataSource = Nothing
        HI.TL.HandlerControl.ClearControl(ogcsumnew)
        ogcsumnew.DataSource = Nothing
        HI.TL.HandlerControl.ClearControl(ogcsumrea)
        ogcsumrea.DataSource = Nothing
        HI.TL.HandlerControl.ClearControl(ogcsumTime)
        ogcsumTime.DataSource = Nothing
        HI.TL.HandlerControl.ClearControl(ogcTime)
        ogcTime.DataSource = Nothing
    End Sub
#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        '   HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogv)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub



    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As System.Object, e As System.EventArgs) Handles ocmload.Click
        Dim _Spls As New HI.TL.SplashScreen("Loading data...   Please Wait  ")
        Me.LoadDatawork(_Spls)
        Me.LoadDataNew(_Spls)
        Me.LoadDataEnd(_Spls)
        Me.LoadDataManday(_Spls)
        Me.LoadSumTime(_Spls)
        Me.LoadSumNew(_Spls)
        Me.LoadSumRea(_Spls)
        Me.LoadSumleave(_Spls)
        Me.LoadSummary(_Spls)
        Me.LoadSummaryFemale(_Spls)
        Me.LoadSummarymale(_Spls)
        Me.LoadSummarry(_Spls)

        _Spls.Close()
    End Sub
    Private Function LoadDatawork(Spls As HI.TL.SplashScreen) As Boolean

        Dim _Qry As String = ""
        Dim tDate As String = ""

        _Qry = "    Select  Top 1 DD.FTDateTrans"
        _Qry &= vbCrLf & "   FROM (SELECT       day_off.FTDateTrans"
        _Qry &= vbCrLf & "From [HITECH_HR].dbo.THRTTrans   AS day_off"
        _Qry &= vbCrLf & "Left Join[HITECH_HR].dbo.THRMEmployee AS M WITH (NOLOCK) ON day_off.FNHSysEmpID =  M.FNHSysEmpID"
        _Qry &= vbCrLf & "  WHERE(day_off.FTDateTrans BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "') AND (DATENAME(dw, day_off.FTDateTrans) <> 'Saturday') AND (DATENAME(dw, day_off.FTDateTrans) <> 'Sunday') AND M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " )AS DD"

        tDate = HI.Conn.SQLConn.GetField(_Qry, HI.Conn.DB.DataBaseName.DB_MERCHAN)






        _Qry = "Select  M.FNHSysEmpID,M.FTEmpCode,M.FNHSysCmpId ,US.FNHSysUnitSectId,L.FNListIndex,'' AS FTNote"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  , PR.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"
            _Qry &= vbCrLf & " ,US.FTUnitSectNameTH AS FTUnitSectName,L.FTNameTH AS FNEmpSex"
        Else
            _Qry &= vbCrLf & "  , PR.FTPreNameNameEN + ' ' +  M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
            _Qry &= vbCrLf & " ,US.FTUnitSectNameEN AS FTUnitSectName,L.FTNameEN AS FNEmpSex"
        End If

        _Qry &= vbCrLf & ",ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode "
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNTime),'.',':') AS FNTime"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(T.FTDateTrans) =1 THEN CONVERT(varchar(10),Convert(datetime,T.FTDateTrans),103) ELSE '' END AS FTDateTrans"
        _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) ON T.FNHSysEmpID =  M.FNHSysEmpID"
        _Qry &= vbCrLf & "INNER Join    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS PR WITH (NOLOCK) ON M.FNHSysPreNameId = PR.FNHSysPreNameId LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId  LEFT OUTER JOIN"
        _Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNEmpSex') AS L ON M.FNEmpSex=L.FNListIndex"
        _Qry &= vbCrLf & " WHERE   (M.FTEmpCode <> '') AND M.FTStateEnablon='1'"
        _Qry &= vbCrLf & "   AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "
        _Qry &= vbCrLf & "And T.FTDateTrans >='" & tDate & "' AND T.FTDateTrans <='" & tDate & "' "

        With Me.ogcTime
            .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            ogvtime.ExpandAllGroups()
            ogvtime.RefreshData()
        End With
    End Function

    Private Function LoadDataNew(Spls As HI.TL.SplashScreen) As Boolean

        Dim _Qry As String = ""
        Dim tDate As String = ""


        _Qry = "Select  M.FNHSysEmpID,M.FTEmpCode,M.FNHSysCmpId ,US.FNHSysUnitSectId,L.FNListIndex,'' AS FTNote"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  , PR.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"
            _Qry &= vbCrLf & " ,US.FTUnitSectNameTH AS FTUnitSectName,L.FTNameTH AS FNEmpSex"
        Else
            _Qry &= vbCrLf & "  , PR.FTPreNameNameEN + ' ' +  M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
            _Qry &= vbCrLf & " ,US.FTUnitSectNameEN AS FTUnitSectName,L.FTNameEN AS FNEmpSex"
        End If

        _Qry &= vbCrLf & ",ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode "
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(M.FDDateStart) =1 THEN CONVERT(varchar(10),Convert(datetime,M.FDDateStart),103) ELSE '' END AS FDDateStart"
        _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) "
        _Qry &= vbCrLf & "INNER Join    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS PR WITH (NOLOCK) ON M.FNHSysPreNameId = PR.FNHSysPreNameId LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId  LEFT OUTER JOIN"
        _Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNEmpSex') AS L ON M.FNEmpSex=L.FNListIndex"
        _Qry &= vbCrLf & " WHERE   (M.FTEmpCode <> '') AND M.FTStateEnablon='1'"
        _Qry &= vbCrLf & "   AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "
        _Qry &= vbCrLf & "And M.FDDateStart  BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "'"

        With Me.ogcnew
            .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            ogvnew.ExpandAllGroups()
            ogvnew.RefreshData()
        End With
    End Function
    Private Function LoadDataEnd(Spls As HI.TL.SplashScreen) As Boolean

        Dim _Qry As String = ""
        Dim tDate As String = ""


        _Qry = "Select  M.FNHSysEmpID,M.FTEmpCode,M.FNHSysCmpId ,US.FNHSysUnitSectId,L.FNListIndex,'' AS FTNote"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  , PR.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"
            _Qry &= vbCrLf & " ,US.FTUnitSectNameTH AS FTUnitSectName,L.FTNameTH AS FNEmpSex,R.FTResignNameTH AS FTResignName"
        Else
            _Qry &= vbCrLf & "  , PR.FTPreNameNameEN + ' ' +  M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
            _Qry &= vbCrLf & " ,US.FTUnitSectNameEN AS FTUnitSectName,L.FTNameEN AS FNEmpSex,R.FTResignNameEN AS FTResignName"
        End If

        _Qry &= vbCrLf & ",ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode "
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(M.FDDateEnd) =1 THEN CONVERT(varchar(10),Convert(datetime,M.FDDateEnd),103) ELSE '' END AS FDDateEnd"
        _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) "
        _Qry &= vbCrLf & "INNER Join    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS PR WITH (NOLOCK) ON M.FNHSysPreNameId = PR.FNHSysPreNameId LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId  LEFT OUTER JOIN"
        _Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNEmpSex') AS L ON M.FNEmpSex=L.FNListIndex LEFT OUTER JOIN"
        _Qry &= vbCrLf & "(select R.FTResignNameTH , R.FTResignNameEN, R.FNHSysResignId,ER.FNHSysEmpID"
        _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee As ER  LEFT Join"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMResign As R On ER.FNHSysResignId=R.FNHSysResignId)As R On M.FNHSysResignId=R.FNHSysResignId AND M.FNHSysEmpID=R.FNHSysEmpID"
        _Qry &= vbCrLf & " WHERE   (M.FTEmpCode <> '') AND M.FTStateEnablon='1'"
        _Qry &= vbCrLf & "   AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "
        _Qry &= vbCrLf & "And M.FDDateEnd  BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "'"

        With Me.ogcend
            .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            ogvend.ExpandAllGroups()
            ogvend.RefreshData()
        End With
    End Function
    Private Function LoadDataManday(Spls As HI.TL.SplashScreen) As Boolean

        Dim _Qry As String = ""
        Dim tDate As String = ""


        _Qry = "Select  M.FNHSysEmpID,M.FTEmpCode,M.FNHSysCmpId ,US.FNHSysUnitSectId,L.FNListIndex,'' AS FTNote"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  , PR.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"
            _Qry &= vbCrLf & " ,US.FTUnitSectNameTH AS FTUnitSectName,L.FTNameTH AS FNEmpSex ,LD.FTNameTH AS Reason"
        Else
            _Qry &= vbCrLf & "  , PR.FTPreNameNameEN + ' ' +  M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
            _Qry &= vbCrLf & " ,US.FTUnitSectNameEN AS FTUnitSectName,L.FTNameEN AS FNEmpSex, ,LD.FTNameEN AS Reason"
        End If

        _Qry &= vbCrLf & ",ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode,LD.FNLeaveTotalTime,LD.FNLeaveTotalTimeMin "
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(LD.FTStartDate) =1 THEN CONVERT(varchar(10),Convert(datetime,LD.FTStartDate),103) ELSE '' END AS FTStartDate"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(LD.FTInsDate) =1 THEN CONVERT(varchar(10),Convert(datetime,LD.FTInsDate),103) ELSE '' END AS FTInsDate"
        _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) "
        _Qry &= vbCrLf & "INNER Join    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS PR WITH (NOLOCK) ON M.FNHSysPreNameId = PR.FNHSysPreNameId LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId  LEFT OUTER JOIN"
        _Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNEmpSex') AS L ON M.FNEmpSex=L.FNListIndex LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  ( select D.FNHSysEmpID,D.FTInsDate,D.FTStartDate,L.FTNameTH,L.FTNameEN,D.FNLeaveTotalTimeMin,D.FNLeaveTotalTime"
        _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily AS D LEFT Join"
        _Qry &= vbCrLf & "(Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [HITECH_SYSTEM].dbo.HSysListData AS L Where L.FTListName ='FNLeaveType')AS L ON D.FTLeaveType=L.FNListIndex  where D.FTStartDate < D.FTInsDate)AS LD ON M.FNHSysEmpID=LD.FNHSysEmpID "
        _Qry &= vbCrLf & " WHERE   (M.FTEmpCode <> '')  AND M.FTStateEnablon='1'"
        _Qry &= vbCrLf & "   AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "
        _Qry &= vbCrLf & "And LD.FTInsDate  BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "' AND LD.FNLeaveTotalTimeMin >='240' AND LD.FTStartDate like '%" & Me.FNYear.Text & "%'"

        With Me.ogcmanday
            .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            ogvmanday.ExpandAllGroups()
            ogvmanday.RefreshData()
        End With
    End Function

    Private Sub ocmclear_Click_1(sender As Object, e As EventArgs)
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        'If VerifyData() Then
        '    If Not Me.ogc.DataSource Is Nothing Then
        '        If Me.ogv.RowCount > 0 Then

        If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave) = True Then
                        Dim _Spls As New HI.TL.SplashScreen("Saving data...   Please Wait  ")
            If Me.Saveworke() Then

                Call Savenewemp()
                Call SaveempRea()
                Call Saveleave()
                _Spls.Close()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                _Spls.Close()
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If






        End If

        '        Else
        '            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการบันทึก กรุณาทำการตรวจสอบ !!!", 1512210547, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        '        End If
        '    Else
        '        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการบันทึก กรุณาทำการตรวจสอบ !!!", 1512210547, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        '    End If




        'End If
    End Sub
    Private Function Saveworke() As Boolean
        Dim _Qry As String = ""


        Dim dt As DataTable
        With CType(Me.ogcTime.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy
        End With


        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String



            For Each R As DataRow In dt.Select("FNHSysEmpID>0 ", "FNHSysEmpID")

                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMStartWork"
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Str &= vbCrLf & " , FNHSysUnitSectId=" & Val(R!FNHSysUnitSectId.ToString) & ""
                _Str &= vbCrLf & " , FNEmpSex=" & Val(R!FNListIndex.ToString) & ""
                _Str &= vbCrLf & " , FNTime=" & Val(R!FNTime.ToString) & ""
                _Str &= vbCrLf & " , FTNote=" & Val(R!FTNote.ToString) & ""
                _Str &= vbCrLf & " , FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " , FDDateAdd=" & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & "   WHERE FNHSysEmpID=" & Val(R!FNHSysEmpID.ToString) & ""
                _Str &= vbCrLf & "       AND FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ""
                _Str &= vbCrLf & "       AND FTDateTrans='" & HI.UL.ULDate.ConvertEnDB(R!FTDateTrans.ToString) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMStartWork"
                    _Str &= vbCrLf & " ("
                    _Str &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime"
                    _Str &= vbCrLf & ", FNHSysEmpID, FTDateTrans, FNHSysUnitSectId, FNEmpSex, FNTime, FTNote, FTUserName, FDDateAdd, FNHSysCmpId"
                    _Str &= vbCrLf & " )"
                    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & " ," & Val(R!FNHSysEmpID.ToString) & ""
                    _Str &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(R!FTDateTrans.ToString) & "'"
                    _Str &= vbCrLf & " ," & Val(R!FNHSysUnitSectId.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNListIndex.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNTime.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FTNote.ToString) & ""
                    _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " ," & HI.ST.SysInfo.CmpID & ""

                    ' _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTDateStart.Text) & "'"


                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function Savenewemp() As Boolean
        Dim _Qry As String = ""


        Dim dt As DataTable
        With CType(Me.ogcnew.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy
        End With


        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String



            For Each R As DataRow In dt.Select("FNHSysEmpID>0 ", "FNHSysEmpID")

                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMNewEmp"
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Str &= vbCrLf & " , FNHSysUnitSectId=" & Val(R!FNHSysUnitSectId.ToString) & ""
                _Str &= vbCrLf & " , FNEmpSex=" & Val(R!FNListIndex.ToString) & ""
                _Str &= vbCrLf & " , FTNote=" & Val(R!FTNote.ToString) & ""
                _Str &= vbCrLf & " , FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " , FDDateAdd=" & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & "   WHERE FNHSysEmpID=" & Val(R!FNHSysEmpID.ToString) & ""
                _Str &= vbCrLf & "       AND FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ""
                _Str &= vbCrLf & "       AND FDDateStart='" & HI.UL.ULDate.ConvertEnDB(R!FDDateStart.ToString) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMNewEmp"
                    _Str &= vbCrLf & " ("
                    _Str &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime"
                    _Str &= vbCrLf & ", FNHSysEmpID, FDDateStart, FNHSysUnitSectId, FNEmpSex, FTNote, FTUserName, FDDateAdd, FNHSysCmpId"
                    _Str &= vbCrLf & " )"
                    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & " ," & Val(R!FNHSysEmpID.ToString) & ""
                    _Str &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(R!FDDateStart.ToString) & "'"
                    _Str &= vbCrLf & " ," & Val(R!FNHSysUnitSectId.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNListIndex.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FTNote.ToString) & ""
                    _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " ," & HI.ST.SysInfo.CmpID & ""

                    ' _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTDateStart.Text) & "'"


                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function SaveempRea() As Boolean
        Dim _Qry As String = ""


        Dim dt As DataTable
        With CType(Me.ogcend.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy
        End With


        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String



            For Each R As DataRow In dt.Select("FNHSysEmpID>0 ", "FNHSysEmpID")

                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMResign"
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Str &= vbCrLf & " , FNHSysUnitSectId=" & Val(R!FNHSysUnitSectId.ToString) & ""
                _Str &= vbCrLf & " , FNEmpSex=" & Val(R!FNListIndex.ToString) & ""
                _Str &= vbCrLf & " , FTNote=" & Val(R!FTNote.ToString) & ""
                _Str &= vbCrLf & " , FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " , FDDateAdd=" & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & "   WHERE FNHSysEmpID=" & Val(R!FNHSysEmpID.ToString) & ""
                _Str &= vbCrLf & "       AND FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ""
                _Str &= vbCrLf & "       AND FDDateEnd='" & HI.UL.ULDate.ConvertEnDB(R!FDDateEnd.ToString) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMResign"
                    _Str &= vbCrLf & " ("
                    _Str &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime"
                    _Str &= vbCrLf & ",  FNHSysEmpID, FDDateEnd, FNHSysUnitSectId, FNEmpSex, FTNote, FTUserName, FDDateAdd, FNHSysCmpId"
                    _Str &= vbCrLf & " )"
                    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & " ," & Val(R!FNHSysEmpID.ToString) & ""
                    _Str &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(R!FDDateEnd.ToString) & "'"
                    _Str &= vbCrLf & " ," & Val(R!FNHSysUnitSectId.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNListIndex.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FTNote.ToString) & ""
                    _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " ," & HI.ST.SysInfo.CmpID & ""

                    ' _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTDateStart.Text) & "'"


                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function
    Private Function Saveleave() As Boolean
        Dim _Qry As String = ""


        Dim dt As DataTable
        With CType(Me.ogcmanday.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy
        End With


        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String



            For Each R As DataRow In dt.Select("FNHSysEmpID>0 ", "FNHSysEmpID")

                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMLeave"
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Str &= vbCrLf & " , FNHSysUnitSectId=" & Val(R!FNHSysUnitSectId.ToString) & ""
                _Str &= vbCrLf & " , FNEmpSex=" & Val(R!FNListIndex.ToString) & ""
                _Str &= vbCrLf & " , FTNote=" & Val(R!FTNote.ToString) & ""
                _Str &= vbCrLf & " , FNLeaveTotalTime=" & Val(R!FNLeaveTotalTime.ToString) & ""
                _Str &= vbCrLf & " , FNLeaveTotalTimeMin=" & Val(R!FNLeaveTotalTimeMin.ToString) & ""
                _Str &= vbCrLf & " , FTUserName='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " , FDDateAdd=" & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & "   WHERE FNHSysEmpID=" & Val(R!FNHSysEmpID.ToString) & ""
                _Str &= vbCrLf & "       AND FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ""
                _Str &= vbCrLf & "       AND FTStartDate='" & HI.UL.ULDate.ConvertEnDB(R!FTStartDate.ToString) & "'"

                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMLeave"
                    _Str &= vbCrLf & " ("
                    _Str &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime"
                    _Str &= vbCrLf & ",   FNHSysEmpID, FTStartDate, FNHSysUnitSectId, FNEmpSex, FTNote, FNLeaveTotalTime, FNLeaveTotalTimeMin, FTUserName, FDDateAdd, FNHSysCmpId"
                    _Str &= vbCrLf & " )"
                    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & " ," & Val(R!FNHSysEmpID.ToString) & ""
                    _Str &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(R!FTStartDate.ToString) & "'"
                    _Str &= vbCrLf & " ," & Val(R!FNHSysUnitSectId.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNListIndex.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FTNote.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNLeaveTotalTime.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FNLeaveTotalTimeMin.ToString) & ""
                    _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " ," & HI.ST.SysInfo.CmpID & ""

                    ' _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTDateStart.Text) & "'"


                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function
    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete) = True Then
            Dim _Spls As New HI.TL.SplashScreen("Deleting data...   Please Wait  ")
            If Me.DeleteDataworke() Then
                Me.DeleteDatanew()
                Me.DeleteDataRea()
                Me.DeleteDataleave()

                _Spls.Close()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            Else
                _Spls.Close()
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
            End If
            Me.otbmain.SelectedTabPageIndex = 0

        End If
    End Sub
    Private Function DeleteDataworke() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim dt As DataTable
            With CType(Me.ogcTime.DataSource, DataTable)
                .AcceptChanges()
                dt = .Copy
            End With

            For Each R As DataRow In dt.Select("FNHSysEmpID>0 ", "FNHSysEmpID")

                Dim _Str As String
                _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMStartWork WHERE  FNHSysCmpId = " & HI.ST.SysInfo.CmpID & " And FTDateTrans ='" & HI.UL.ULDate.ConvertEnDB(R!FTDateTrans.ToString) & "'"
                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
            Next
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function
    Private Function DeleteDatanew() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim dt As DataTable
            With CType(Me.ogcnew.DataSource, DataTable)
                .AcceptChanges()
                dt = .Copy
            End With

            For Each R As DataRow In dt.Select("FNHSysEmpID>0 ", "FNHSysEmpID")

                Dim _Str As String
                _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumNew WHERE  FNHSysCmpId = " & HI.ST.SysInfo.CmpID & " And FDDateStart ='" & HI.UL.ULDate.ConvertEnDB(R!FDDateStart.ToString) & "'"
                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
            Next
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function DeleteDataRea() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim dt As DataTable
            With CType(Me.ogcend.DataSource, DataTable)
                .AcceptChanges()
                dt = .Copy
            End With

            For Each R As DataRow In dt.Select("FNHSysEmpID>0 ", "FNHSysEmpID")

                Dim _Str As String
                _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMResign WHERE  FNHSysCmpId = " & HI.ST.SysInfo.CmpID & " And FDDateEnd ='" & HI.UL.ULDate.ConvertEnDB(R!FDDateEnd.ToString) & "'"
                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
            Next
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function
    Private Function DeleteDataleave() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            Dim dt As DataTable
            With CType(Me.ogcmanday.DataSource, DataTable)
                .AcceptChanges()
                dt = .Copy
            End With

            For Each R As DataRow In dt.Select("FNHSysEmpID>0 ", "FNHSysEmpID")

                Dim _Str As String
                _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMLeave WHERE  FNHSysCmpId = " & HI.ST.SysInfo.CmpID & " And FTStartDate ='" & HI.UL.ULDate.ConvertEnDB(R!FTStartDate.ToString) & "'"
                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If
            Next
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)


            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function
    Private Function LoadSumTime(Spls As HI.TL.SplashScreen) As Boolean

        Dim _Qry As String = ""
        Dim tDate As String = ""


        _Qry = "    Select  Top 1 DD.FTDateTrans"
        _Qry &= vbCrLf & "   FROM (SELECT       day_off.FTDateTrans"
        _Qry &= vbCrLf & "From [HITECH_HR].dbo.THRTTrans   AS day_off"
        _Qry &= vbCrLf & "Left Join[HITECH_HR].dbo.THRMEmployee AS M WITH (NOLOCK) ON day_off.FNHSysEmpID =  M.FNHSysEmpID"
        _Qry &= vbCrLf & "  WHERE(day_off.FTDateTrans BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "') AND (DATENAME(dw, day_off.FTDateTrans) <> 'Saturday') AND (DATENAME(dw, day_off.FTDateTrans) <> 'Sunday') AND M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " )AS DD"

        tDate = HI.Conn.SQLConn.GetField(_Qry, HI.Conn.DB.DataBaseName.DB_MERCHAN)


        _Qry = " Select  M.FNHSysCmpId,Month( '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "') AS FTMonthsum,"
        _Qry &= vbCrLf & "(Select count(T.FTDateTrans)As counttime"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) ON T.FNHSysEmpID =  M.FNHSysEmpID LEFT OUTER Join"
        _Qry &= vbCrLf & " (Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNEmpSex') AS L ON M.FNEmpSex=L.FNListIndex"
        _Qry &= vbCrLf & "  WHERE  M.FNEmpSex='0' AND M.FTStateEnablon='1' And T.FTDateTrans ='" & tDate & "'  AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "   "
        _Qry &= vbCrLf & ")AS FTMalesum,"
        _Qry &= vbCrLf & "(Select count(T.FTDateTrans)as counttime"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) ON T.FNHSysEmpID =  M.FNHSysEmpID LEFT OUTER Join"
        _Qry &= vbCrLf & "(Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNEmpSex') AS L ON M.FNEmpSex=L.FNListIndex"
        _Qry &= vbCrLf & "  WHERE  M.FNEmpSex='1' AND M.FTStateEnablon='1' And T.FTDateTrans ='" & tDate & "'    AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "   "
        _Qry &= vbCrLf & " )AS FTFeMalesum,"
        _Qry &= vbCrLf & " (Select count(T.FTDateTrans)as counttime"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) ON T.FNHSysEmpID =  M.FNHSysEmpID "
        _Qry &= vbCrLf & " Where T.FTDateTrans = '" & tDate & "'  AND M.FTStateEnablon='1'   AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "   "
        _Qry &= vbCrLf & ")AS FTToTalWork,"
        _Qry &= vbCrLf & "  (SELECT  ((DATEDiff(d, '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "', '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "')+1) -  (DATEDiff(ww, '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "', '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "')) -  COUNT( H.FDHolidayDate)) AS Male"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday AS H"
        _Qry &= vbCrLf & "Where H.FDHolidayDate between '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "'   AND  H.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  )AS FTDaysum"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS M WITH (NOLOCK)   "
        _Qry &= vbCrLf & " Where  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "   "


        With Me.ogcsumTime
            .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            ogvSumTime.ExpandAllGroups()
            ogvSumTime.RefreshData()
        End With

        Call Saveworkesum()

    End Function
    Private Function Saveworkesum() As Boolean
        Dim _Qry As String = ""


        Dim dt As DataTable
        With CType(Me.ogcsumTime.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy
        End With


        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            Dim ttype As String = ""
            Dim _SystemKey As String = ""
            Dim _Key As String = ""
            Dim _Val As String = ""
            Dim _StateNew As Boolean = False
            Dim _ManualCode As Boolean = False
            Dim _CmpH As Integer = 0
            Dim _kep As String = ""
            _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FNSeq FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & Me.FNHSysCmpId.Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")

            Dim tyear As String = ""
            Dim tday As String = ""
            Dim tmonth As String = ""
            Dim _month As String = ""
            Dim _Seq As Integer = 0




            For Each R As DataRow In dt.Select("FNHSysCmpId>0 ", "FNHSysCmpId")

                _month = Month(FDDate.Text)
                tyear = Year(Date.Today)
                tmonth = Month(Date.Today)
                tday = Microsoft.VisualBasic.DateAndTime.Day(Date.Today)
                _Seq += +1
                _SystemKey = (_Seq & "" & _CmpH & "" & tyear & "" & _month)

                _kep = HI.Conn.SQLConn.GetField("SELECT TOP 1 S.FNHSysSumWorkId  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & ".dbo.THRTEmployeeHRMSumWork AS S WHERE S.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND S.FTMonthsum='" & _month & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")


                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork"
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Str &= vbCrLf & " , FTToTalWork=" & Val(R!FTToTalWork.ToString) & ""
                _Str &= vbCrLf & " , FTMalesum=" & Val(R!FTMalesum.ToString) & ""
                _Str &= vbCrLf & " , FTFeMalesum=" & Val(R!FTFeMalesum.ToString) & ""
                _Str &= vbCrLf & " , FTDaysum=" & Val(R!FTDaysum.ToString) & ""
                _Str &= vbCrLf & "   WHERE  FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ""
                _Str &= vbCrLf & " AND FTMonthsum=" & Val(R!FTMonthsum.ToString) & ""
                _Str &= vbCrLf & " AND  FNHSysSumWorkId='" & _kep & "'"


                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork"
                    _Str &= vbCrLf & " ("
                    _Str &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime"
                    _Str &= vbCrLf & ", FNHSysSumWorkId, FNHSysCmpId, FTMonthsum, FTToTalWork, FTMalesum, FTFeMalesum, FTDaysum"
                    _Str &= vbCrLf & " )"
                    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & " ," & Val(_SystemKey) & ""
                    _Str &= vbCrLf & " ," & HI.ST.SysInfo.CmpID & ""
                    _Str &= vbCrLf & " ," & Val(R!FTMonthsum.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FTToTalWork.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FTMalesum.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FTFeMalesum.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FTDaysum.ToString) & ""



                    ' _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTDateStart.Text) & "'"


                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function
    Private Function LoadSumNew(Spls As HI.TL.SplashScreen) As Boolean

        Dim _Qry As String = ""
        Dim tDate As String = ""




        _Qry = " Select  M.FNHSysCmpId,Month( '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "') AS FTMonthsum,"
        _Qry &= vbCrLf & "  (Select count(M.FNHSysEmpID)AS CountDate"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK)  LEFT OUTER Join"
        _Qry &= vbCrLf & " (Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNEmpSex') AS L ON M.FNEmpSex=L.FNListIndex"
        _Qry &= vbCrLf & "        WHERE   M.FNEmpSex ='0' And M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " AND M.FTStateEnablon='1'  And M.FDDateStart  BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "')AS FTMalesum,"
        _Qry &= vbCrLf & "(Select count(M.FNHSysEmpID)AS CountDate"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK)  LEFT OUTER Join"
        _Qry &= vbCrLf & "(Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNEmpSex') AS L ON M.FNEmpSex=L.FNListIndex"
        _Qry &= vbCrLf & " WHERE   M.FNEmpSex='1' And M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  AND M.FTStateEnablon='1'  And M.FDDateStart  BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "')AS FTFeMalesum,"
        _Qry &= vbCrLf & "(Select count(M.FNHSysEmpID)AS CountDate"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK)  "
        _Qry &= vbCrLf & " Where M.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & " AND M.FTStateEnablon='1'   And M.FDDateStart  BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "')AS FTToTalWork"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS M WITH (NOLOCK)   "
        _Qry &= vbCrLf & " Where  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "   "


        With Me.ogcsumnew
            .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            ogvsumnew.ExpandAllGroups()
            ogvsumnew.RefreshData()
        End With

        Call SaveNewsum()

    End Function
    Private Function SaveNewsum() As Boolean
        Dim _Qry As String = ""


        Dim dt As DataTable
        With CType(Me.ogcsumnew.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy
        End With


        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String

            Dim ttype As String = ""
            Dim _SystemKey As String = ""
            Dim _Key As String = ""
            Dim _Val As String = ""
            Dim _StateNew As Boolean = False
            Dim _ManualCode As Boolean = False
            Dim _CmpH As Integer = 0
            Dim _kep As String = ""

            _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FNSeq FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & Me.FNHSysCmpId.Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")


            Dim tyear As String = ""
            Dim tday As String = ""
            Dim tmonth As String = ""
            Dim _month As String = ""
            Dim _Seq As Integer = 0




            For Each R As DataRow In dt.Select("FNHSysCmpId>0 ", "FNHSysCmpId")

                _month = Month(FDDate.Text)
                tyear = Year(Date.Today)
                tmonth = Month(Date.Today)
                tday = Microsoft.VisualBasic.DateAndTime.Day(Date.Today)
                _Seq += +1
                _SystemKey = (_Seq & "" & _CmpH & "" & tyear & "" & _month)





                _kep = HI.Conn.SQLConn.GetField("SELECT TOP 1 S.FNHSysSumNewId  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumNew AS S WHERE S.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND S.FTMonthsum='" & _month & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")



                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumNew"
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Str &= vbCrLf & " , FTToTalWork=" & Val(R!FTToTalWork.ToString) & ""
                _Str &= vbCrLf & " , FTMalesum=" & Val(R!FTMalesum.ToString) & ""
                _Str &= vbCrLf & " , FTFeMalesum=" & Val(R!FTFeMalesum.ToString) & ""
                _Str &= vbCrLf & "   WHERE  FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ""
                _Str &= vbCrLf & " AND FTMonthsum=" & Val(R!FTMonthsum.ToString) & ""
                _Str &= vbCrLf & " AND  FNHSysSumNewId='" & _kep & "'"


                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumNew"
                    _Str &= vbCrLf & " ("
                    _Str &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime"
                    _Str &= vbCrLf & ",FNHSysSumNewId, FNHSysCmpId, FTMonthsum, FTToTalWork, FTMalesum, FTFeMalesum"
                    _Str &= vbCrLf & " )"
                    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & " ," & Val(_SystemKey) & ""
                    _Str &= vbCrLf & " ," & HI.ST.SysInfo.CmpID & ""
                    _Str &= vbCrLf & " ," & Val(R!FTMonthsum.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FTToTalWork.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FTMalesum.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FTFeMalesum.ToString) & ""




                    ' _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTDateStart.Text) & "'"


                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function LoadSumRea(Spls As HI.TL.SplashScreen) As Boolean

        Dim _Qry As String = ""
        Dim tDate As String = ""




        _Qry = " Select  M.FNHSysCmpId,Month( '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "') AS FTMonthsum,"
        _Qry &= vbCrLf & "  (Select count(M.FNHSysEmpID)AS CountDate"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK)  LEFT OUTER Join"
        _Qry &= vbCrLf & " (Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNEmpSex') AS L ON M.FNEmpSex=L.FNListIndex"
        _Qry &= vbCrLf & "        WHERE   M.FNEmpSex ='0' AND M.FTStateEnablon='1' And M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "   And M.FDDateEnd  BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "')AS FTMalesum,"
        _Qry &= vbCrLf & "(Select count(M.FNHSysEmpID)AS CountDate"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK)  LEFT OUTER Join"
        _Qry &= vbCrLf & "(Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNEmpSex') AS L ON M.FNEmpSex=L.FNListIndex"
        _Qry &= vbCrLf & " WHERE   M.FNEmpSex='1' AND M.FTStateEnablon='1' And M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "   And M.FDDateEnd  BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "')AS FTFeMalesum,"
        _Qry &= vbCrLf & "(Select count(M.FNHSysEmpID)AS CountDate"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK)  "
        _Qry &= vbCrLf & " Where M.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & "  AND M.FTStateEnablon='1'  And M.FDDateEnd  BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "')AS FTToTalWork"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS M WITH (NOLOCK)   "
        _Qry &= vbCrLf & " Where  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "   "


        With Me.ogcsumrea
            .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            ogvsumrea.ExpandAllGroups()
            ogvsumrea.RefreshData()
        End With

        Call SaveReasum()

    End Function
    Private Function SaveReasum() As Boolean
        Dim _Qry As String = ""


        Dim dt As DataTable
        With CType(Me.ogcsumrea.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy
        End With


        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String



            Dim ttype As String = ""
            Dim _SystemKey As String = ""
            Dim _Key As String = ""
            Dim _Val As String = ""
            Dim _StateNew As Boolean = False
            Dim _ManualCode As Boolean = False
            Dim _CmpH As Integer = 0
            Dim _kep As String = ""

            _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FNSeq FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & Me.FNHSysCmpId.Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")

            Dim tyear As String = ""
            Dim tday As String = ""
            Dim tmonth As String = ""
            Dim _month As String = ""
            Dim _Seq As Integer = 0




            For Each R As DataRow In dt.Select("FNHSysCmpId>0 ", "FNHSysCmpId")

                _month = Month(FDDate.Text)
                tyear = Year(Date.Today)
                tmonth = Month(Date.Today)
                tday = Microsoft.VisualBasic.DateAndTime.Day(Date.Today)
                _Seq += +1
                _SystemKey = (_Seq & "" & _CmpH & "" & tyear & "" & _month)



                _kep = HI.Conn.SQLConn.GetField("SELECT TOP 1 S.FNHSysSumReasonId  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumReason AS S WHERE S.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND S.FTMonthsum='" & _month & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")



                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumReason"
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Str &= vbCrLf & " , FTToTalWork=" & Val(R!FTToTalWork.ToString) & ""
                _Str &= vbCrLf & " , FTMalesum=" & Val(R!FTMalesum.ToString) & ""
                _Str &= vbCrLf & " , FTFeMalesum=" & Val(R!FTFeMalesum.ToString) & ""
                _Str &= vbCrLf & "   WHERE  FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ""
                _Str &= vbCrLf & " AND FTMonthsum=" & Val(R!FTMonthsum.ToString) & ""
                _Str &= vbCrLf & " AND  FNHSysSumReasonId='" & _kep & "'"


                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumReason"
                    _Str &= vbCrLf & " ("
                    _Str &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime"
                    _Str &= vbCrLf & ", FNHSysSumReasonId, FNHSysCmpId, FTMonthsum, FTToTalWork, FTMalesum, FTFeMalesum"
                    _Str &= vbCrLf & " )"
                    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & " ," & Val(_SystemKey) & ""
                    _Str &= vbCrLf & " ," & HI.ST.SysInfo.CmpID & ""
                    _Str &= vbCrLf & " ," & Val(R!FTMonthsum.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FTToTalWork.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FTMalesum.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FTFeMalesum.ToString) & ""




                    ' _Str &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTDateStart.Text) & "'"


                    If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function
    Private Function LoadSumleave(Spls As HI.TL.SplashScreen) As Boolean

        Dim _Qry As String = ""
        Dim tDate As String = "" & Month(Date.Today) & ""


        _Qry = " Select  M.FNHSysCmpId,Month( '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "') AS FTMonthsum"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  , (SELECT L.FTNameTH AS FNTypeLeave"
        Else
            _Qry &= vbCrLf & "  ,(SELECT L.FTNameEN AS FNTypeLeave"
        End If
        _Qry &= vbCrLf & "from(Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNTypeLeave')AS L"
        _Qry &= vbCrLf & " Where L.FNListIndex ='5')AS FNTypeLeave,"
        _Qry &= vbCrLf & "  (SELECT L.FNListIndex"
        _Qry &= vbCrLf & "from(Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNTypeLeave')AS L"
        _Qry &= vbCrLf & " Where L.FNListIndex ='5')AS FNListIndex,"
        _Qry &= vbCrLf & "(Select sum(LD.FNLeaveTotalTimeMin )/480 AS manday"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & "(Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNEmpSex') AS L ON M.FNEmpSex=L.FNListIndex LEFT OUTER JOIN"
        _Qry &= vbCrLf & "( Select D.FNHSysEmpID, D.FTInsDate, D.FTStartDate, L.FTNameTH, L.FTNameEN, D.FNLeaveTotalTimeMin, D.FNLeaveTotalTime"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily AS D LEFT Join"
        _Qry &= vbCrLf & "(Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNLeaveType')AS L ON D.FTLeaveType=L.FNListIndex  where D.FTStartDate < D.FTInsDate)AS LD ON M.FNHSysEmpID=LD.FNHSysEmpID "
        _Qry &= vbCrLf & "WHERE    M.FNEmpSex='0'    AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " AND M.FTStateEnablon='1'  And LD.FTInsDate  BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "' AND LD.FNLeaveTotalTimeMin >='240' AND LD.FTStartDate like '%" & Me.FNYear.Text & "%')AS FTMalesum,"
        _Qry &= vbCrLf & "(Select sum(LD.FNLeaveTotalTimeMin )/480 AS manday"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & "(Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNEmpSex') AS L ON M.FNEmpSex=L.FNListIndex LEFT OUTER JOIN"
        _Qry &= vbCrLf & "( Select D.FNHSysEmpID, D.FTInsDate, D.FTStartDate, L.FTNameTH, L.FTNameEN, D.FNLeaveTotalTimeMin, D.FNLeaveTotalTime"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily AS D LEFT Join"
        _Qry &= vbCrLf & "(Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNLeaveType')AS L ON D.FTLeaveType=L.FNListIndex  where D.FTStartDate < D.FTInsDate)AS LD ON M.FNHSysEmpID=LD.FNHSysEmpID "
        _Qry &= vbCrLf & "WHERE    M.FNEmpSex='1'    AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " AND M.FTStateEnablon='1'  And LD.FTInsDate  BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "'AND LD.FNLeaveTotalTimeMin >='240' AND LD.FTStartDate like '%" & Me.FNYear.Text & "%')AS FTFeMalesum,"
        _Qry &= vbCrLf & "(Select sum(LD.FNLeaveTotalTimeMin )/480 AS manday"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & " ( Select D.FNHSysEmpID, D.FTInsDate, D.FTStartDate, L.FTNameTH, L.FTNameEN, D.FNLeaveTotalTimeMin, D.FNLeaveTotalTime"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily AS D LEFT Join"
        _Qry &= vbCrLf & "(Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNLeaveType')AS L ON D.FTLeaveType=L.FNListIndex  where D.FTStartDate < D.FTInsDate)AS LD ON M.FNHSysEmpID=LD.FNHSysEmpID "
        _Qry &= vbCrLf & "WHERE     M.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & " AND M.FTStateEnablon='1' And LD.FTInsDate  BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "'AND LD.FNLeaveTotalTimeMin >='240' AND LD.FTStartDate like '%" & Me.FNYear.Text & "%')AS FTToTalWork"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS M WITH (NOLOCK)  "
        _Qry &= vbCrLf & " Where  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " "

        _Qry &= vbCrLf & "  UNION"

        _Qry &= vbCrLf & " Select  M.FNHSysCmpId,Month( '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "') AS FTMonthsum"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  , (SELECT L.FTNameTH AS FNTypeLeave"
        Else
            _Qry &= vbCrLf & "  ,(SELECT L.FTNameEN AS FNTypeLeave"
        End If
        _Qry &= vbCrLf & "from(Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNTypeLeave')AS L"
        _Qry &= vbCrLf & "   Where L.FNListIndex ='6')AS FNTypeLeave,"
        _Qry &= vbCrLf & "  (SELECT L.FNListIndex"
        _Qry &= vbCrLf & "from(Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNTypeLeave')AS L"
        _Qry &= vbCrLf & " Where L.FNListIndex ='6')AS FNListIndex,"
        _Qry &= vbCrLf & "(Select COUNT(LD.FNLeaveTotalTimeMin ) As unplan"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & " (Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNEmpSex') AS L ON M.FNEmpSex=L.FNListIndex LEFT OUTER JOIN"
        _Qry &= vbCrLf & " ( Select D.FNHSysEmpID, D.FTInsDate, D.FTStartDate, L.FTNameTH, L.FTNameEN, D.FNLeaveTotalTimeMin, D.FNLeaveTotalTime"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily AS D LEFT Join"
        _Qry &= vbCrLf & "(Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNLeaveType')AS L ON D.FTLeaveType=L.FNListIndex  where D.FTStartDate < D.FTInsDate)AS LD ON M.FNHSysEmpID=LD.FNHSysEmpID"
        _Qry &= vbCrLf & "WHERE    M.FNEmpSex='0'    AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " AND M.FTStateEnablon='1'  And LD.FTInsDate  BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "'AND LD.FNLeaveTotalTimeMin >='240' AND LD.FTStartDate like '%" & Me.FNYear.Text & "%')AS FTMalesum,"
        _Qry &= vbCrLf & " (Select  COUNT(LD.FNLeaveTotalTimeMin ) AS unplan"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & "(Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNEmpSex') AS L ON M.FNEmpSex=L.FNListIndex LEFT OUTER JOIN"
        _Qry &= vbCrLf & " ( Select D.FNHSysEmpID, D.FTInsDate, D.FTStartDate, L.FTNameTH, L.FTNameEN, D.FNLeaveTotalTimeMin, D.FNLeaveTotalTime"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily AS D LEFT Join"
        _Qry &= vbCrLf & "(Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNLeaveType')AS L ON D.FTLeaveType=L.FNListIndex  where D.FTStartDate < D.FTInsDate)AS LD ON M.FNHSysEmpID=LD.FNHSysEmpID"
        _Qry &= vbCrLf & "WHERE    M.FNEmpSex='1'    AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " AND M.FTStateEnablon='1'  And LD.FTInsDate  BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "'AND LD.FNLeaveTotalTimeMin >='240' AND LD.FTStartDate like '%" & Me.FNYear.Text & "%')AS FTFeMalesum,"
        _Qry &= vbCrLf & "(Select  COUNT(LD.FNLeaveTotalTimeMin ) As unplan"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & "   ( Select D.FNHSysEmpID, D.FTInsDate, D.FTStartDate, L.FTNameTH, L.FTNameEN, D.FNLeaveTotalTimeMin, D.FNLeaveTotalTime"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily AS D LEFT Join"
        _Qry &= vbCrLf & "(Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNLeaveType')AS L ON D.FTLeaveType=L.FNListIndex  where D.FTStartDate < D.FTInsDate)AS LD ON M.FNHSysEmpID=LD.FNHSysEmpID "
        _Qry &= vbCrLf & "WHERE     M.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & " AND M.FTStateEnablon='1' And LD.FTInsDate  BETWEEN '" & HI.UL.ULDate.ConvertEnDB(FDDate.Text) & "' AND '" & HI.UL.ULDate.ConvertEnDB(FDDateEnd.Text) & "'AND LD.FNLeaveTotalTimeMin >='240' AND LD.FTStartDate like '%" & Me.FNYear.Text & "%')AS FTToTalWork"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS M WITH (NOLOCK)   "
        _Qry &= vbCrLf & " Where  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "    "


        With Me.ogcsumleave
            .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            ogvsumleave.ExpandAllGroups()
            ogvsumleave.RefreshData()
        End With

        Call Savesumleave()

    End Function
    Private Function Savesumleave() As Boolean
        Dim _Qry As String = ""


        Dim dt As DataTable
        With CType(Me.ogcsumleave.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy
        End With

        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            Dim ttype As String = ""

            Dim _SystemKey As String = ""
            Dim _Key As String = ""
            Dim _Val As String = ""
            Dim _StateNew As Boolean = False
            Dim _ManualCode As Boolean = False
            Dim _CmpH As Integer = 0
            Dim _kep As String = ""

            _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FNSeq FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & Me.FNHSysCmpId.Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")



            Dim type As String = ""
            Dim _month As String = ""

            _month = Month(FDDate.Text)
            Dim tyear As String = ""
            Dim tday As String = ""
            Dim tmonth As String = ""

            Dim _Seq As Integer = 0




            Dim _S As String
            _S = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave WHERE  FNHSysCmpId = " & HI.ST.SysInfo.CmpID & " And FTMonthsum =" & _month & ""

            If HI.Conn.SQLConn.ExecuteNonQuery(_S, HI.Conn.DB.DataBaseName.DB_HR) = True Then
            End If


            For Each R As DataRow In dt.Select("FNHSysCmpId>0 ", "FNHSysCmpId")

                type = Val(R!FNListIndex.ToString)
                '_month = Month(FDDate.Text)
                tyear = Year(Date.Today)
                tmonth = Month(Date.Today)
                tday = Microsoft.VisualBasic.DateAndTime.Day(Date.Today)
                _Seq += +1
                _SystemKey = (_Seq & "" & _CmpH & "" & tyear & "" & _month)
                Dim tID As String = ""



                Dim tID6 As String = ""



                '_kep = HI.Conn.SQLConn.GetField("SELECT TOP 1 S.FNHSysSumLeaveId  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS S WHERE S.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND S.FTMonthsum='" & _month & "' AND S.FNTypeLeave='" & type & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")

                '  If type = "5" Then

                '_Qry = "    Select  Top 1 S.FNHSysSumLeaveId "
                '    _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS S"
                '_Qry &= vbCrLf & "  WHERE S.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND S.FTMonthsum='" & _month & "' AND S.FNTypeLeave='" & type & "'"

                'tID = HI.Conn.SQLConn.GetField(_Qry, HI.Conn.DB.DataBaseName.DB_HR)

                '   _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave"
                '_Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                '_Str &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                '_Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                '_Str &= vbCrLf & " , FTToTalWork=" & Val(R!FTToTalWork.ToString) & ""
                '_Str &= vbCrLf & " , FTMalesum=" & Val(R!FTMalesum.ToString) & ""
                '_Str &= vbCrLf & " , FTFeMalesum=" & Val(R!FTFeMalesum.ToString) & ""
                '_Str &= vbCrLf & "   WHERE  FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ""
                '_Str &= vbCrLf & " AND FTMonthsum=" & Val(R!FTMonthsum.ToString) & ""
                '_Str &= vbCrLf & " AND FNTypeLeave=" & Val(R!FNListIndex.ToString) & ""
                '_Str &= vbCrLf & " AND FNHSysSumLeaveId='" & tID & "'"
                'Else


                '    _Qry = "    Select  Top 1 S.FNHSysSumLeaveId "
                '    _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS S"
                '    _Qry &= vbCrLf & "  WHERE S.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND S.FTMonthsum='" & _month & "' AND S.FNTypeLeave='6'"

                '    tID6 = HI.Conn.SQLConn.GetField(_Qry, HI.Conn.DB.DataBaseName.DB_HR)

                '    _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave"
                '    _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                '    _Str &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                '    _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                '    _Str &= vbCrLf & " , FTToTalWork=" & Val(R!FTToTalWork.ToString) & ""
                '    _Str &= vbCrLf & " , FTMalesum=" & Val(R!FTMalesum.ToString) & ""
                '    _Str &= vbCrLf & " , FTFeMalesum=" & Val(R!FTFeMalesum.ToString) & ""
                '    _Str &= vbCrLf & "   WHERE  FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ""
                '    _Str &= vbCrLf & " AND FTMonthsum=" & Val(R!FTMonthsum.ToString) & ""
                '    _Str &= vbCrLf & " AND FNTypeLeave=" & Val(R!FNListIndex.ToString) & ""
                '    _Str &= vbCrLf & " AND FNHSysSumLeaveId='" & tID6 & "'"
                'End If




                '   If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave"
                    _Str &= vbCrLf & " ("
                    _Str &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime"
                    _Str &= vbCrLf & ", FNHSysSumLeaveId, FNHSysCmpId, FTMonthsum, FTToTalWork, FTMalesum, FTFeMalesum, FNTypeLeave"
                    _Str &= vbCrLf & " )"
                    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & " ," & Val(_SystemKey) & ""
                    _Str &= vbCrLf & " ," & HI.ST.SysInfo.CmpID & ""
                    _Str &= vbCrLf & " ," & Val(R!FTMonthsum.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FTToTalWork.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FTMalesum.ToString) & ""
                    _Str &= vbCrLf & " ," & Val(R!FTFeMalesum.ToString) & ""
                _Str &= vbCrLf & " ," & Val(R!FNListIndex.ToString) & ""
                HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_HR)

                'If HI.Conn.SQLConn.ExecuteNonQuery(_Str, HI.Conn.DB.DataBaseName.DB_HR) = True Then
                '    'If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                '    HI.Conn.SQLConn.Tran.Rollback()
                '    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                '    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                '    Return False
                'End If

                ' End If

            Next

            'HI.Conn.SQLConn.Tran.Commit()
            'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function
    Private Function LoadSummary(Spls As HI.TL.SplashScreen) As Boolean

        Dim _Qry As String = ""

        Dim _DateMounth As String = Month(FDDate.Text)
        'Dim _Malework As Integer = HI.Conn.SQLConn.GetField("SELECT A.FTMalesum AS Malework FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS A WITH(NOLOCK) WHERE A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND A.FTMonthsum='" & _DateMounth & "' ", Conn.DB.DataBaseName.DB_MASTER, "")
        'Dim _FeMalework As Integer = HI.Conn.SQLConn.GetField("SELECT A.FTFeMalesum AS FeMalework FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS A WITH(NOLOCK) WHERE A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND A.FTMonthsum='" & _DateMounth & "' ", Conn.DB.DataBaseName.DB_MASTER, "")
        'Dim _ToTalwork As Integer = HI.Conn.SQLConn.GetField("SELECT FTToTalWork AS ToTalwork FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS A WITH(NOLOCK) WHERE A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND A.FTMonthsum='" & _DateMounth & "' ", Conn.DB.DataBaseName.DB_MASTER, "")

        'Dim _Malenew As Integer = HI.Conn.SQLConn.GetField("SELECT A.FTMalesum AS Malenew FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumNew AS A WITH(NOLOCK) WHERE A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND A.FTMonthsum='" & _DateMounth & "' ", Conn.DB.DataBaseName.DB_MASTER, "")
        'Dim _FeMalenew As Integer = HI.Conn.SQLConn.GetField("SELECT A.FTFeMalesum AS FeMalenew FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumNew AS A WITH(NOLOCK) WHERE A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND A.FTMonthsum='" & _DateMounth & "' ", Conn.DB.DataBaseName.DB_MASTER, "")
        'Dim _ToTalnew As Integer = HI.Conn.SQLConn.GetField("SELECT FTToTalWork AS ToTalnew FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumNew AS A WITH(NOLOCK) WHERE A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND A.FTMonthsum='" & _DateMounth & "' ", Conn.DB.DataBaseName.DB_MASTER, "")

        'Dim _Malerea As Integer = HI.Conn.SQLConn.GetField("SELECT A.FTMalesum AS Malerea FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumReason AS A WITH(NOLOCK) WHERE A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND A.FTMonthsum='" & _DateMounth & "' ", Conn.DB.DataBaseName.DB_MASTER, "")
        'Dim _FeMalerea As Integer = HI.Conn.SQLConn.GetField("SELECT A.FTFeMalesum AS FeMalerea FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumReason AS A WITH(NOLOCK) WHERE A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND A.FTMonthsum='" & _DateMounth & "' ", Conn.DB.DataBaseName.DB_MASTER, "")
        'Dim _ToTalrea As Integer = HI.Conn.SQLConn.GetField("SELECT FTToTalWork AS ToTalrea FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumReason AS A WITH(NOLOCK) WHERE A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND A.FTMonthsum='" & _DateMounth & "' ", Conn.DB.DataBaseName.DB_MASTER, "")

        'Dim _Malelevmd As Integer = HI.Conn.SQLConn.GetField("SELECT A.FTMalesum AS Malelev FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A WITH(NOLOCK) WHERE A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND A.FTMonthsum='" & _DateMounth & "' AND A.FNTypeLeave='5' ", Conn.DB.DataBaseName.DB_MASTER, "")
        'Dim _FeMalelevmd As Integer = HI.Conn.SQLConn.GetField("SELECT A.FTFeMalesum AS FeMalelev FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A WITH(NOLOCK) WHERE A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND A.FTMonthsum='" & _DateMounth & "' AND A.FNTypeLeave='5' ", Conn.DB.DataBaseName.DB_MASTER, "")
        'Dim _ToTallevmd As Integer = HI.Conn.SQLConn.GetField("SELECT FTToTalWork AS ToTallev FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A WITH(NOLOCK) WHERE A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND A.FTMonthsum='" & _DateMounth & "' AND A.FNTypeLeave='5' ", Conn.DB.DataBaseName.DB_MASTER, "")


        'Dim _Malelev As Integer = HI.Conn.SQLConn.GetField("SELECT A.FTMalesum AS Malelev FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A WITH(NOLOCK) WHERE A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND A.FTMonthsum='" & _DateMounth & "' AND A.FNTypeLeave='6' ", Conn.DB.DataBaseName.DB_MASTER, "")
        'Dim _FeMalelev As Integer = HI.Conn.SQLConn.GetField("SELECT A.FTFeMalesum AS FeMalelev FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A WITH(NOLOCK) WHERE A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND A.FTMonthsum='" & _DateMounth & "' AND A.FNTypeLeave='6'", Conn.DB.DataBaseName.DB_MASTER, "")
        'Dim _ToTallev As Integer = HI.Conn.SQLConn.GetField("SELECT FTToTalWork AS ToTallev FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A WITH(NOLOCK) WHERE A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND A.FTMonthsum='" & _DateMounth & "' AND A.FNTypeLeave='6'", Conn.DB.DataBaseName.DB_MASTER, "")




        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry = "  Select 'จำนวนคนที่ทำงานวันแรกของเดือน' AS Detail"
        Else
            _Qry = "Select ' Number workers on the first day of the calendar month' AS Detail "
        End If
        _Qry &= vbCrLf & ",SW.FTMonthsum,SW.FTMalesum,SW.FTFeMalesum,SW.FTToTalWork"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS SW"
        _Qry &= vbCrLf & "  Where SW.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & " AND SW.FTMonthsum='" & _DateMounth & "'  "

        _Qry &= vbCrLf & " UNION ALL"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'จำนวนวันทำงาน ตัดวันนักขัตฤกษื วันอาทิตย์' AS Detail"
        Else
            _Qry &= vbCrLf & "Select 'Number days that were in the month' AS Detail"
        End If
        _Qry &= vbCrLf & ",SW.FTMonthsum,SW.FTDaysum AS FTMalesum,SW.FTDaysum AS FTFeMalesum,SW.FTDaysum AS FTToTalWork"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS SW"
        _Qry &= vbCrLf & " Where SW.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & "  AND SW.FTMonthsum='" & _DateMounth & "'  "

        _Qry &= vbCrLf & "UNION ALL"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'จำนวนพนักงานที่เริ่มงานใหม่ของเดือน' AS Detail"
        Else
            _Qry &= vbCrLf & "Select ' Number  workers hired in the month' AS Detail"
        End If
        _Qry &= vbCrLf & ",SN.FTMonthsum,SN.FTMalesum,SN.FTFeMalesum,SN.FTToTalWork"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumNew AS SN"
        _Qry &= vbCrLf & "Where SN.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & "  AND SN.FTMonthsum='" & _DateMounth & "'   "

        _Qry &= vbCrLf & "UNION ALL"


        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'จำนวนพนักงานที่ลาออกของเดือน' AS Detail"
        Else
            _Qry &= vbCrLf & "Select 'Number  workers that departed in the month' AS Detail"
        End If
        _Qry &= vbCrLf & ",SR.FTMonthsum,SR.FTMalesum,SR.FTFeMalesum,SR.FTToTalWork"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumReason AS SR"
        _Qry &= vbCrLf & "Where SR.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & "   AND SR.FTMonthsum='" & _DateMounth & "'  "


        _Qry &= vbCrLf & "UNION ALL"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select  L.FTNameTH As Detail"
        Else
            _Qry &= vbCrLf & "Select  L.FTNameEN As Detail"
        End If
        _Qry &= vbCrLf & ",SL.FTMonthsum,SL.FTMalesum,SL.FTFeMalesum,SL.FTToTalWork"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS SL  LEFT OUTER Join"
        _Qry &= vbCrLf & "(Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNTypeLeave')AS L ON SL.FNTypeLeave=L.FNListIndex"
        _Qry &= vbCrLf & "where SL.FNHSysCmpId= " & HI.ST.SysInfo.CmpID & "   And SL.FNTypeLeave ='5'   AND SL.FTMonthsum='" & _DateMounth & "'"

        _Qry &= vbCrLf & "UNION ALL"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select  L.FTNameTH As Detail"
        Else
            _Qry &= vbCrLf & "Select  L.FTNameEN As Detail"
        End If
        _Qry &= vbCrLf & ",SL.FTMonthsum,SL.FTMalesum,SL.FTFeMalesum,SL.FTToTalWork"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS SL  LEFT OUTER Join"
        _Qry &= vbCrLf & "(Select L.FTNameTH, L.FTNameEN, L.FNListIndex From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L Where L.FTListName ='FNTypeLeave')AS L ON SL.FNTypeLeave=L.FNListIndex"
        _Qry &= vbCrLf & "where SL.FNHSysCmpId= " & HI.ST.SysInfo.CmpID & "   And SL.FNTypeLeave ='6'   AND SL.FTMonthsum='" & _DateMounth & "'"

        _Qry &= vbCrLf & "UNION ALL"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'Absenteeism -Monthly  ' AS Detail"
        Else
            _Qry &= vbCrLf & "Select 'Absenteeism -Monthly ' AS Detail"
        End If
        _Qry &= vbCrLf & ",SW.FTMonthsum, (MA.FTMalesum * 100)/(SW.FTMalesum*SW.FTDaysum )AS FTMalesum"
        _Qry &= vbCrLf & ", (MA.FTFeMalesum * 100)/(SW.FTFeMalesum*SW.FTDaysum )AS FTFeMalesum"
        _Qry &= vbCrLf & ", (MA.FTToTalWork * 100)/(SW.FTToTalWork*SW.FTDaysum )AS FTToTalWork"
        _Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS SW LEFT OUTER JOIN"
        _Qry &= vbCrLf & "(select A.FTMalesum ,A.FTFeMalesum,A.FTToTalWork,A.FNHSysCmpId,A.FTMonthsum from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A where  A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " And A.FTMonthsum='" & _DateMounth & "' AND A.FNTypeLeave='5')AS MA ON SW.FNHSysCmpId=MA.FNHSysCmpId AND SW.FTMonthsum=MA.FTMonthsum"
        _Qry &= vbCrLf & "where SW.FNHSysCmpId= " & HI.ST.SysInfo.CmpID & "   AND SW.FTMonthsum='" & _DateMounth & "'  "

        _Qry &= vbCrLf & "UNION ALL"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'Turnover - Monthly  ' AS Detail"
        Else
            _Qry &= vbCrLf & "Select 'Turnover - Monthly  ' AS Detail"
        End If

        _Qry &= vbCrLf & ",SW.FTMonthsum, (A.FTMalesum * 100)/SW.FTMalesum AS FTMalesum"
        _Qry &= vbCrLf & ", (A.FTFeMalesum * 100)/SW.FTFeMalesum AS FTFeMalesum"
        _Qry &= vbCrLf & ",(A.FTToTalWork * 100)/SW.FTToTalWork AS FTToTalWork"
        _Qry &= vbCrLf & "from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS SW LEFT OUTER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumReason AS A ON  SW.FNHSysCmpId=A.FNHSysCmpId AND SW.FTMonthsum=A.FTMonthsum"
        _Qry &= vbCrLf & "where SW.FNHSysCmpId= " & HI.ST.SysInfo.CmpID & "    AND SW.FTMonthsum='" & _DateMounth & "' "

        _Qry &= vbCrLf & "UNION ALL"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'AVF Absence -Monthly' AS Detail"
        Else
            _Qry &= vbCrLf & "Select 'AVF Absence -Monthly' AS Detail"
        End If
        _Qry &= vbCrLf & ",SW.FTMonthsum, SW.FTMalesum/MA.FTMalesum AS FTMalesum"
        _Qry &= vbCrLf & ", SW.FTFeMalesum/MA.FTFeMalesum AS FTFeMalesum"
        _Qry &= vbCrLf & ", SW.FTToTalWork/MA.FTToTalWork AS FTToTalWork"
        _Qry &= vbCrLf & "from (select A.FTMalesum ,A.FTFeMalesum,A.FTToTalWork,A.FNHSysCmpId,A.FTMonthsum from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A where A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " And A.FTMonthsum='" & _DateMounth & "'  AND A.FNTypeLeave='5')AS SW LEFT OUTER JOIN"
        _Qry &= vbCrLf & "(select A.FTMalesum ,A.FTFeMalesum,A.FTToTalWork,A.FNHSysCmpId,A.FTMonthsum from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A where  A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " And A.FTMonthsum='" & _DateMounth & "'  AND A.FNTypeLeave='6')AS MA ON SW.FNHSysCmpId=MA.FNHSysCmpId AND SW.FTMonthsum=MA.FTMonthsum"
        _Qry &= vbCrLf & "where SW.FNHSysCmpId= " & HI.ST.SysInfo.CmpID & "     AND SW.FTMonthsum='" & _DateMounth & "'"


        With Me.ogcsummary
            .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            ogvsummary.ExpandAllGroups()
            ogvsummary.RefreshData()
        End With


    End Function

    Private Function LoadSummaryFemale(Spls As HI.TL.SplashScreen) As Boolean

        Dim _Qry As String = ""

        Dim _DateMounth As String = Month(FDDate.Text)




        ''--1
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry = "  Select 'จำนวนคนที่ทำงานวันแรกของเดือน(หญิง)' AS Detail ,"
        'Else
        '    _Qry = "Select ' Number workers on the first day of the calendar month(FEMALE)' AS Detail ,"
        'End If
        '_Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        '_Qry &= vbCrLf & "  FROM("
        '_Qry &= vbCrLf & "      Select  SW.FTMonthsum,SW.FTFeMalesum"
        '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS SW "
        '_Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        '_Qry &= vbCrLf & ") AS SourceTable "
        '_Qry &= vbCrLf & " PIVOT(  "
        '_Qry &= vbCrLf & "AVG(FTFeMalesum)"
        '_Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        '_Qry &= vbCrLf & ") AS PivotTable"

        ''--2
        '_Qry &= vbCrLf & "UNION ALL"
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & "Select 'จำนวนวันทำงาน ตัดวันนักขัตฤกษื วันอาทิตย์' AS Detail,"
        'Else
        '    _Qry &= vbCrLf & "Select 'Number days that were in the month(FEMALE)' AS Detail,"
        'End If
        '_Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        '_Qry &= vbCrLf & "  FROM("
        '_Qry &= vbCrLf & " Select  SW.FTMonthsum,CAST (SW.FTDaysum As INT)As FTDaysum"
        '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork As SW "
        '_Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        '_Qry &= vbCrLf & ") AS SourceTable "
        '_Qry &= vbCrLf & " PIVOT(  "
        '_Qry &= vbCrLf & "   AVG(FTDaysum)"
        '_Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        '_Qry &= vbCrLf & ") AS PivotTable"


        ''--3
        '_Qry &= vbCrLf & "UNION ALL"
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & "Select 'จำนวนพนักงานที่เริ่มงานใหม่ของเดือน(หญิง)' AS Detail,"
        'Else
        '    _Qry &= vbCrLf & "Select ' Number  workers hired in the month(FEMALE)' AS Detail,"
        'End If
        '_Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        '_Qry &= vbCrLf & "  FROM("
        '_Qry &= vbCrLf & " Select  SW.FTMonthsum,SW.FTFeMalesum"
        '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumNew AS SW"
        '_Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        '_Qry &= vbCrLf & ") AS SourceTable "
        '_Qry &= vbCrLf & " PIVOT(  "
        '_Qry &= vbCrLf & "  AVG(FTFeMalesum)"
        '_Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        '_Qry &= vbCrLf & ") AS PivotTable"


        ''--4
        '_Qry &= vbCrLf & " UNION ALL"
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & "Select 'จำนวนพนักงานที่ลาออกของเดือน(หญิง)' AS Detail,"
        'Else
        '    _Qry &= vbCrLf & "Select 'Number  workers that departed in the month(FEMALE)' AS Detail,"
        'End If
        '_Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        '_Qry &= vbCrLf & "  FROM("
        '_Qry &= vbCrLf & "   Select  SW.FTMonthsum,SW.FTFeMalesum"
        '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumReason AS SW"
        '_Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        '_Qry &= vbCrLf & ") AS SourceTable "
        '_Qry &= vbCrLf & " PIVOT(  "
        '_Qry &= vbCrLf & "  AVG(FTFeMalesum)"
        '_Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        '_Qry &= vbCrLf & ") AS PivotTable"


        ''--5
        '_Qry &= vbCrLf & " UNION ALL"
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & "Select 'บันทึกลาย้อนหลัง(Manday)(หญิง)' AS Detail,"
        'Else
        '    _Qry &= vbCrLf & "Select 'Number  production worker unplanned person-day (man-day) absences in the month(FEMALE)' AS Detail,"
        'End If
        '_Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        '_Qry &= vbCrLf & "  FROM("
        '_Qry &= vbCrLf & " Select  SW.FTMonthsum,SW.FTFeMalesum"
        '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave  AS SW"
        '_Qry &= vbCrLf & "Where SW.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & "  And SW.FNTypeLeave ='5' And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        '_Qry &= vbCrLf & ") AS SourceTable "
        '_Qry &= vbCrLf & " PIVOT(  "
        '_Qry &= vbCrLf & "  AVG(FTFeMalesum)"
        '_Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        '_Qry &= vbCrLf & ") AS PivotTable"


        ''--6
        '_Qry &= vbCrLf & "  UNION ALL"
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & "Select 'พนักงานที่บันทึกลาย้อนหลัง(หญิง)' AS Detail,"
        'Else
        '    _Qry &= vbCrLf & "Select ' Number  production workers with unplanned absence in the month(FEMALE)' AS Detail,"
        'End If
        '_Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        '_Qry &= vbCrLf & "  FROM("
        '_Qry &= vbCrLf & "   Select  SW.FTMonthsum,SW.FTFeMalesum"
        '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave  AS SW"
        '_Qry &= vbCrLf & "Where SW.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & "  And SW.FNTypeLeave ='6' And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        '_Qry &= vbCrLf & ") AS SourceTable "
        '_Qry &= vbCrLf & " PIVOT(  "
        '_Qry &= vbCrLf & "  AVG(FTFeMalesum)"
        '_Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        '_Qry &= vbCrLf & ") AS PivotTable"


        ''--7
        '_Qry &= vbCrLf & "  UNION ALL"
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & "Select 'Absenteeism -Monthly (หญิง) ' AS Detail,"
        'Else
        '    _Qry &= vbCrLf & "Select 'Absenteeism -Monthly(FEMALE) ' AS Detail,"
        'End If
        '_Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        '_Qry &= vbCrLf & "  FROM("
        '_Qry &= vbCrLf & "  Select  SW.FTMonthsum"
        '_Qry &= vbCrLf & ", (MA.FTFeMalesum * 100)/(SW.FTFeMalesum*SW.FTDaysum )AS FTFeMalesum"
        '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS SW LEFT OUTER Join"
        '_Qry &= vbCrLf & "(Select A.FTMalesum, A.FTFeMalesum, A.FTToTalWork, A.FNHSysCmpId, A.FTMonthsum From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A Where A.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & " And A.FNTypeLeave ='5')AS MA ON SW.FNHSysCmpId=MA.FNHSysCmpId AND SW.FTMonthsum=MA.FTMonthsum"
        '_Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        '_Qry &= vbCrLf & ") AS SourceTable "
        '_Qry &= vbCrLf & " PIVOT(  "
        '_Qry &= vbCrLf & "      AVG(FTFeMalesum)"
        '_Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        '_Qry &= vbCrLf & ") AS PivotTable"

        ''--8
        '_Qry &= vbCrLf & " UNION ALL"
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & "Select 'Turnover - Monthly (หญิง) ' AS Detail,"
        'Else
        '    _Qry &= vbCrLf & "Select 'Turnover - Monthly  (FEMALE)' AS Detail,"
        'End If
        '_Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        '_Qry &= vbCrLf & "  From("
        '_Qry &= vbCrLf & " Select  SW.FTMonthsum"
        '_Qry &= vbCrLf & ", (A.FTFeMalesum * 100)/SW.FTFeMalesum AS FTFeMalesum"
        '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS SW LEFT OUTER Join"
        '_Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumReason AS A ON  SW.FNHSysCmpId=A.FNHSysCmpId And SW.FTMonthsum=A.FTMonthsum"
        '_Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        '_Qry &= vbCrLf & ") As SourceTable "
        '_Qry &= vbCrLf & " PIVOT(  "
        '_Qry &= vbCrLf & " AVG(FTFeMalesum)"
        '_Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        '_Qry &= vbCrLf & ") As PivotTable"


        ''--9
        '_Qry &= vbCrLf & "UNION ALL"
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & "Select 'AVF Absence -Monthly(หญิง)' AS Detail,"
        'Else
        '    _Qry &= vbCrLf & "Select 'AVF Absence -Monthly(FEMALE)' AS Detail,"
        'End If
        '_Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        '_Qry &= vbCrLf & "  From("
        '_Qry &= vbCrLf & "  SELECT SW.FTMonthsum"
        '_Qry &= vbCrLf & ",  SW.FTFeMalesum/MA.FTFeMalesum AS FTFeMalesum"
        '_Qry &= vbCrLf & "from(select A.FTMalesum , A.FTFeMalesum, A.FTToTalWork, A.FNHSysCmpId, A.FTMonthsum, A.FDInsDate from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A where A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & "  And A.FNTypeLeave='5')AS SW LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "(select A.FTMalesum ,A.FTFeMalesum,A.FTToTalWork,A.FNHSysCmpId,A.FTMonthsum from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A where  A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & "  And A.FNTypeLeave='6')AS MA ON SW.FNHSysCmpId=MA.FNHSysCmpId AND SW.FTMonthsum=MA.FTMonthsum"
        '_Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        '_Qry &= vbCrLf & ") As SourceTable "
        '_Qry &= vbCrLf & " PIVOT(  "
        '_Qry &= vbCrLf & " AVG(FTFeMalesum)"
        '_Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        '_Qry &= vbCrLf & ") As PivotTable"


        'With Me.ogcsumfemale
        '    .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        '    ogvsumfemale.ExpandAllGroups()
        '    ogvsumfemale.RefreshData()
        'End With


    End Function

    Private Function LoadSummarymale(Spls As HI.TL.SplashScreen) As Boolean

        Dim _Qry As String = ""

        Dim _DateMounth As String = Month(FDDate.Text)




        ''--1
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry = "  Select 'จำนวนคนที่ทำงานวันแรกของเดือน (ชาย)' AS Detail ,"
        'Else
        '    _Qry = "Select ' Number workers on the first day of the calendar month (MALE)' AS Detail ,"
        'End If
        '_Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        '_Qry &= vbCrLf & "  FROM("
        '_Qry &= vbCrLf & "      Select  SW.FTMonthsum,SW.FTMalesum"
        '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS SW "
        '_Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        '_Qry &= vbCrLf & ") AS SourceTable "
        '_Qry &= vbCrLf & " PIVOT(  "
        '_Qry &= vbCrLf & "AVG(FTMalesum)"
        '_Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        '_Qry &= vbCrLf & ") AS PivotTable"

        ''--2
        '_Qry &= vbCrLf & "UNION ALL"
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & "Select 'จำนวนวันทำงาน ตัดวันนักขัตฤกษื วันอาทิตย์' AS Detail,"
        'Else
        '    _Qry &= vbCrLf & "Select 'Number days that were in the month (MALE)' AS Detail,"
        'End If
        '_Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        '_Qry &= vbCrLf & "  FROM("
        '_Qry &= vbCrLf & " Select  SW.FTMonthsum,CAST (SW.FTDaysum As INT)As FTDaysum"
        '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork As SW "
        '_Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        '_Qry &= vbCrLf & ") AS SourceTable "
        '_Qry &= vbCrLf & " PIVOT(  "
        '_Qry &= vbCrLf & "   AVG(FTDaysum)"
        '_Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        '_Qry &= vbCrLf & ") AS PivotTable"


        ''--3
        '_Qry &= vbCrLf & "UNION ALL"
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & "Select 'จำนวนพนักงานที่เริ่มงานใหม่ของเดือน(ชาย)' AS Detail,"
        'Else
        '    _Qry &= vbCrLf & "Select ' Number  workers hired in the month (MALE)' AS Detail,"
        'End If
        '_Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        '_Qry &= vbCrLf & "  FROM("
        '_Qry &= vbCrLf & " Select  SW.FTMonthsum,SW.FTMalesum"
        '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumNew AS SW"
        '_Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        '_Qry &= vbCrLf & ") AS SourceTable "
        '_Qry &= vbCrLf & " PIVOT(  "
        '_Qry &= vbCrLf & "  AVG(FTMalesum)"
        '_Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        '_Qry &= vbCrLf & ") AS PivotTable"


        ''--4
        '_Qry &= vbCrLf & " UNION ALL"
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & "Select 'จำนวนพนักงานที่ลาออกของเดือน(ชาย)' AS Detail,"
        'Else
        '    _Qry &= vbCrLf & "Select 'Number  workers that departed in the month (MALE)' AS Detail,"
        'End If
        '_Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        '_Qry &= vbCrLf & "  FROM("
        '_Qry &= vbCrLf & "   Select  SW.FTMonthsum,SW.FTMalesum"
        '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumReason AS SW"
        '_Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        '_Qry &= vbCrLf & ") AS SourceTable "
        '_Qry &= vbCrLf & " PIVOT(  "
        '_Qry &= vbCrLf & "  AVG(FTMalesum)"
        '_Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        '_Qry &= vbCrLf & ") AS PivotTable"


        ''--5
        '_Qry &= vbCrLf & " UNION ALL"
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & "Select 'บันทึกลาย้อนหลัง(Manday)(ชาย)' AS Detail,"
        'Else
        '    _Qry &= vbCrLf & "Select 'Number  production worker unplanned person-day (man-day) absences in the month (MALE)' AS Detail,"
        'End If
        '_Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        '_Qry &= vbCrLf & "  FROM("
        '_Qry &= vbCrLf & " Select  SW.FTMonthsum,SW.FTMalesum"
        '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave  AS SW"
        '_Qry &= vbCrLf & "Where SW.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & "  And SW.FNTypeLeave ='5' And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        '_Qry &= vbCrLf & ") AS SourceTable "
        '_Qry &= vbCrLf & " PIVOT(  "
        '_Qry &= vbCrLf & "  AVG(FTMalesum)"
        '_Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        '_Qry &= vbCrLf & ") AS PivotTable"


        ''--6
        '_Qry &= vbCrLf & "  UNION ALL"
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & "Select 'พนักงานที่บันทึกลาย้อนหลัง(ชาย)' AS Detail,"
        'Else
        '    _Qry &= vbCrLf & "Select ' Number  production workers with unplanned absence in the month (MALE)' AS Detail,"
        'End If
        '_Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        '_Qry &= vbCrLf & "  FROM("
        '_Qry &= vbCrLf & "   Select  SW.FTMonthsum,SW.FTMalesum"
        '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave  AS SW"
        '_Qry &= vbCrLf & "Where SW.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & "  And SW.FNTypeLeave ='6' And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        '_Qry &= vbCrLf & ") AS SourceTable "
        '_Qry &= vbCrLf & " PIVOT(  "
        '_Qry &= vbCrLf & "  AVG(FTMalesum)"
        '_Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        '_Qry &= vbCrLf & ") AS PivotTable"


        ''--7
        '_Qry &= vbCrLf & "  UNION ALL"
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & "Select 'Absenteeism -Monthly (ชาย) ' AS Detail,"
        'Else
        '    _Qry &= vbCrLf & "Select 'Absenteeism -Monthly (MALE)' AS Detail,"
        'End If
        '_Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        '_Qry &= vbCrLf & "  FROM("
        '_Qry &= vbCrLf & "  Select  SW.FTMonthsum"
        '_Qry &= vbCrLf & ", (MA.FTMalesum * 100)/(SW.FTMalesum*SW.FTDaysum )AS FTMalesum"
        '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS SW LEFT OUTER Join"
        '_Qry &= vbCrLf & "(Select A.FTMalesum, A.FTFeMalesum, A.FTToTalWork, A.FNHSysCmpId, A.FTMonthsum From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A Where A.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & " And A.FNTypeLeave ='5')AS MA ON SW.FNHSysCmpId=MA.FNHSysCmpId AND SW.FTMonthsum=MA.FTMonthsum"
        '_Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        '_Qry &= vbCrLf & ") AS SourceTable "
        '_Qry &= vbCrLf & " PIVOT(  "
        '_Qry &= vbCrLf & "      AVG(FTMalesum)"
        '_Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        '_Qry &= vbCrLf & ") AS PivotTable"

        ''--8
        '_Qry &= vbCrLf & " UNION ALL"
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & "Select 'Turnover - Monthly (ชาย) ' AS Detail,"
        'Else
        '    _Qry &= vbCrLf & "Select 'Turnover - Monthly (MALE) ' AS Detail,"
        'End If
        '_Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        '_Qry &= vbCrLf & "  From("
        '_Qry &= vbCrLf & " Select  SW.FTMonthsum"
        '_Qry &= vbCrLf & ", (A.FTMalesum * 100)/SW.FTMalesum AS FTMalesum"
        '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS SW LEFT OUTER Join"
        '_Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumReason AS A ON  SW.FNHSysCmpId=A.FNHSysCmpId And SW.FTMonthsum=A.FTMonthsum"
        '_Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        '_Qry &= vbCrLf & ") As SourceTable "
        '_Qry &= vbCrLf & " PIVOT(  "
        '_Qry &= vbCrLf & " AVG(FTMalesum)"
        '_Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        '_Qry &= vbCrLf & ") As PivotTable"


        ''--9
        '_Qry &= vbCrLf & "UNION ALL"
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & "Select 'AVF Absence -Monthly (ชาย)' AS Detail,"
        'Else
        '    _Qry &= vbCrLf & "Select 'AVF Absence -Monthly(MALE)' AS Detail,"
        'End If
        '_Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        '_Qry &= vbCrLf & "  From("
        '_Qry &= vbCrLf & "  SELECT SW.FTMonthsum"
        '_Qry &= vbCrLf & ",   SW.FTMalesum/MA.FTMalesum AS FTMalesum"
        '_Qry &= vbCrLf & "from(select A.FTMalesum , A.FTFeMalesum, A.FTToTalWork, A.FNHSysCmpId, A.FTMonthsum, A.FDInsDate from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A where A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & "  And A.FNTypeLeave='5')AS SW LEFT OUTER JOIN"
        '_Qry &= vbCrLf & "(select A.FTMalesum ,A.FTFeMalesum,A.FTToTalWork,A.FNHSysCmpId,A.FTMonthsum from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A where  A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & "  And A.FNTypeLeave='6')AS MA ON SW.FNHSysCmpId=MA.FNHSysCmpId AND SW.FTMonthsum=MA.FTMonthsum"
        '_Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        '_Qry &= vbCrLf & ") As SourceTable "
        '_Qry &= vbCrLf & " PIVOT(  "
        '_Qry &= vbCrLf & " AVG(FTMalesum)"
        '_Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        '_Qry &= vbCrLf & ") As PivotTable"


        'With Me.ogcsummale
        '    .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        '    ogvsummale.ExpandAllGroups()
        '    ogvsummale.RefreshData()
        'End With


    End Function

    Private Function LoadSummarry(Spls As HI.TL.SplashScreen) As Boolean

        Dim _Qry As String = ""

        Dim _DateMounth As String = Month(FDDate.Text)

        '--2

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry = "Select 'จำนวนวันทำงาน ตัดวันนักขัตฤกษื วันอาทิตย์' AS Detail,"
        Else
            _Qry = "Select 'Number days that were in the month' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  FROM("
        _Qry &= vbCrLf & " Select  SW.FTMonthsum,CAST (SW.FTDaysum As INT)As FTDaysum"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork As SW "
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") AS SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & "   AVG(FTDaysum)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") AS PivotTable"

        '------------ชาย

        _Qry &= vbCrLf & "UNION ALL"
        '--1
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  Select 'จำนวนคนที่ทำงานวันแรกของเดือน (ชาย)' AS Detail ,"
        Else
            _Qry &= vbCrLf & "Select ' Number workers on the first day of the calendar month (MALE)' AS Detail ,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  FROM("
        _Qry &= vbCrLf & "      Select  SW.FTMonthsum,SW.FTMalesum"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS SW "
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") AS SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & "AVG(FTMalesum)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") AS PivotTable"



        '--3
        _Qry &= vbCrLf & "UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'จำนวนพนักงานที่เริ่มงานใหม่ของเดือน(ชาย)' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select ' Number  workers hired in the month (MALE)' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  FROM("
        _Qry &= vbCrLf & " Select  SW.FTMonthsum,SW.FTMalesum"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumNew AS SW"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") AS SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & "  AVG(FTMalesum)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") AS PivotTable"


        '--4
        _Qry &= vbCrLf & " UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'จำนวนพนักงานที่ลาออกของเดือน(ชาย)' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select 'Number  workers that departed in the month (MALE)' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  FROM("
        _Qry &= vbCrLf & "   Select  SW.FTMonthsum,SW.FTMalesum"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumReason AS SW"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") AS SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & "  AVG(FTMalesum)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") AS PivotTable"


        '--5
        _Qry &= vbCrLf & " UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'บันทึกลาย้อนหลัง(Manday)(ชาย)' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select 'Number  production worker unplanned person-day (man-day) absences in the month (MALE)' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  FROM("
        _Qry &= vbCrLf & " Select  SW.FTMonthsum,SW.FTMalesum"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave  AS SW"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & "  And SW.FNTypeLeave ='5' And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") AS SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & "  AVG(FTMalesum)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") AS PivotTable"


        '--6
        _Qry &= vbCrLf & "  UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'พนักงานที่บันทึกลาย้อนหลัง(ชาย)' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select ' Number  production workers with unplanned absence in the month (MALE)' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  FROM("
        _Qry &= vbCrLf & "   Select  SW.FTMonthsum,SW.FTMalesum"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave  AS SW"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & "  And SW.FNTypeLeave ='6' And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") AS SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & "  AVG(FTMalesum)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") AS PivotTable"


        '--7
        _Qry &= vbCrLf & "  UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'Absenteeism -Monthly (ชาย) ' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select 'Absenteeism -Monthly (MALE)' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  FROM("
        _Qry &= vbCrLf & "  Select  SW.FTMonthsum"
        _Qry &= vbCrLf & ", (MA.FTMalesum * 100)/(SW.FTMalesum*SW.FTDaysum )AS FTMalesum"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS SW LEFT OUTER Join"
        _Qry &= vbCrLf & "(Select A.FTMalesum, A.FTFeMalesum, A.FTToTalWork, A.FNHSysCmpId, A.FTMonthsum From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A Where A.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & " And A.FNTypeLeave ='5')AS MA ON SW.FNHSysCmpId=MA.FNHSysCmpId AND SW.FTMonthsum=MA.FTMonthsum"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") AS SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & "      AVG(FTMalesum)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") AS PivotTable"

        '--8
        _Qry &= vbCrLf & " UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'Turnover - Monthly (ชาย) ' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select 'Turnover - Monthly (MALE) ' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  From("
        _Qry &= vbCrLf & " Select  SW.FTMonthsum"
        _Qry &= vbCrLf & ", (A.FTMalesum * 100)/SW.FTMalesum AS FTMalesum"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS SW LEFT OUTER Join"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumReason AS A ON  SW.FNHSysCmpId=A.FNHSysCmpId And SW.FTMonthsum=A.FTMonthsum"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") As SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & " AVG(FTMalesum)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") As PivotTable"


        '--9
        _Qry &= vbCrLf & "UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'AVF Absence -Monthly (ชาย)' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select 'AVF Absence -Monthly(MALE)' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  From("
        _Qry &= vbCrLf & "  SELECT SW.FTMonthsum"
        _Qry &= vbCrLf & ",   SW.FTMalesum/MA.FTMalesum AS FTMalesum"
        _Qry &= vbCrLf & "from(select A.FTMalesum , A.FTFeMalesum, A.FTToTalWork, A.FNHSysCmpId, A.FTMonthsum, A.FDInsDate from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A where A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & "  And A.FNTypeLeave='5')AS SW LEFT OUTER JOIN"
        _Qry &= vbCrLf & "(select A.FTMalesum ,A.FTFeMalesum,A.FTToTalWork,A.FNHSysCmpId,A.FTMonthsum from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A where  A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & "  And A.FNTypeLeave='6')AS MA ON SW.FNHSysCmpId=MA.FNHSysCmpId AND SW.FTMonthsum=MA.FTMonthsum"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") As SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & " AVG(FTMalesum)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") As PivotTable"



        '----------(หญิง)
        _Qry &= vbCrLf & "UNION ALL"
        '--1
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  Select 'จำนวนคนที่ทำงานวันแรกของเดือน(หญิง)' AS Detail ,"
        Else
            _Qry &= vbCrLf & "Select ' Number workers on the first day of the calendar month(FEMALE)' AS Detail ,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  FROM("
        _Qry &= vbCrLf & "      Select  SW.FTMonthsum,SW.FTFeMalesum"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS SW "
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") AS SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & "AVG(FTFeMalesum)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") AS PivotTable"




        '--3
        _Qry &= vbCrLf & "UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'จำนวนพนักงานที่เริ่มงานใหม่ของเดือน(หญิง)' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select ' Number  workers hired in the month(FEMALE)' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  FROM("
        _Qry &= vbCrLf & " Select  SW.FTMonthsum,SW.FTFeMalesum"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumNew AS SW"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") AS SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & "  AVG(FTFeMalesum)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") AS PivotTable"


        '--4
        _Qry &= vbCrLf & " UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'จำนวนพนักงานที่ลาออกของเดือน(หญิง)' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select 'Number  workers that departed in the month(FEMALE)' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  FROM("
        _Qry &= vbCrLf & "   Select  SW.FTMonthsum,SW.FTFeMalesum"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumReason AS SW"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") AS SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & "  AVG(FTFeMalesum)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") AS PivotTable"


        '--5
        _Qry &= vbCrLf & " UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'บันทึกลาย้อนหลัง(Manday)(หญิง)' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select 'Number  production worker unplanned person-day (man-day) absences in the month(FEMALE)' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  FROM("
        _Qry &= vbCrLf & " Select  SW.FTMonthsum,SW.FTFeMalesum"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave  AS SW"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & "  And SW.FNTypeLeave ='5' And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") AS SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & "  AVG(FTFeMalesum)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") AS PivotTable"


        '--6
        _Qry &= vbCrLf & "  UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'พนักงานที่บันทึกลาย้อนหลัง(หญิง)' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select ' Number  production workers with unplanned absence in the month(FEMALE)' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  FROM("
        _Qry &= vbCrLf & "   Select  SW.FTMonthsum,SW.FTFeMalesum"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave  AS SW"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & "  And SW.FNTypeLeave ='6' And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") AS SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & "  AVG(FTFeMalesum)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") AS PivotTable"


        '--7
        _Qry &= vbCrLf & "  UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'Absenteeism -Monthly (หญิง) ' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select 'Absenteeism -Monthly(FEMALE) ' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  FROM("
        _Qry &= vbCrLf & "  Select  SW.FTMonthsum"
        _Qry &= vbCrLf & ", (MA.FTFeMalesum * 100)/(SW.FTFeMalesum*SW.FTDaysum )AS FTFeMalesum"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS SW LEFT OUTER Join"
        _Qry &= vbCrLf & "(Select A.FTMalesum, A.FTFeMalesum, A.FTToTalWork, A.FNHSysCmpId, A.FTMonthsum From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A Where A.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & " And A.FNTypeLeave ='5')AS MA ON SW.FNHSysCmpId=MA.FNHSysCmpId AND SW.FTMonthsum=MA.FTMonthsum"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") AS SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & "      AVG(FTFeMalesum)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") AS PivotTable"

        '--8
        _Qry &= vbCrLf & " UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'Turnover - Monthly (หญิง) ' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select 'Turnover - Monthly  (FEMALE)' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  From("
        _Qry &= vbCrLf & " Select  SW.FTMonthsum"
        _Qry &= vbCrLf & ", (A.FTFeMalesum * 100)/SW.FTFeMalesum AS FTFeMalesum"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS SW LEFT OUTER Join"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumReason AS A ON  SW.FNHSysCmpId=A.FNHSysCmpId And SW.FTMonthsum=A.FTMonthsum"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") As SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & " AVG(FTFeMalesum)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") As PivotTable"


        '--9
        _Qry &= vbCrLf & "UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'AVF Absence -Monthly(หญิง)' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select 'AVF Absence -Monthly(FEMALE)' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  From("
        _Qry &= vbCrLf & "  SELECT SW.FTMonthsum"
        _Qry &= vbCrLf & ",  SW.FTFeMalesum/MA.FTFeMalesum AS FTFeMalesum"
        _Qry &= vbCrLf & "from(select A.FTMalesum , A.FTFeMalesum, A.FTToTalWork, A.FNHSysCmpId, A.FTMonthsum, A.FDInsDate from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A where A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & "  And A.FNTypeLeave='5')AS SW LEFT OUTER JOIN"
        _Qry &= vbCrLf & "(select A.FTMalesum ,A.FTFeMalesum,A.FTToTalWork,A.FNHSysCmpId,A.FTMonthsum from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A where  A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & "  And A.FNTypeLeave='6')AS MA ON SW.FNHSysCmpId=MA.FNHSysCmpId AND SW.FTMonthsum=MA.FTMonthsum"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") As SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & " AVG(FTFeMalesum)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") As PivotTable"




        '------------รวม
        '--1
        _Qry &= vbCrLf & "UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  Select 'จำนวนคนที่ทำงานวันแรกของเดือน' AS Detail ,"
        Else
            _Qry &= vbCrLf & "Select ' Number workers on the first day of the calendar month ' AS Detail ,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  FROM("
        _Qry &= vbCrLf & "      Select  SW.FTMonthsum,SW.FTToTalWork"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS SW "
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") AS SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & "AVG(FTToTalWork)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") AS PivotTable"

        ''--2
        '_Qry &= vbCrLf & "UNION ALL"
        'If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
        '    _Qry &= vbCrLf & "Select 'จำนวนวันทำงาน ตัดวันนักขัตฤกษื วันอาทิตย์' AS Detail,"
        'Else
        '    _Qry &= vbCrLf & "Select 'Number days that were in the month' AS Detail,"
        'End If
        '_Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        '_Qry &= vbCrLf & "  FROM("
        '_Qry &= vbCrLf & " Select  SW.FTMonthsum,CAST (SW.FTDaysum As INT)As FTDaysum"
        '_Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork As SW "
        '_Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        '_Qry &= vbCrLf & ") AS SourceTable "
        '_Qry &= vbCrLf & " PIVOT(  "
        '_Qry &= vbCrLf & "   AVG(FTDaysum)"
        '_Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        '_Qry &= vbCrLf & ") AS PivotTable"


        '--3
        _Qry &= vbCrLf & "UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'จำนวนพนักงานที่เริ่มงานใหม่ของเดือน' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select ' Number  workers hired in the month' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  FROM("
        _Qry &= vbCrLf & " Select  SW.FTMonthsum,SW.FTToTalWork"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumNew AS SW"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") AS SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & "  AVG(FTToTalWork)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") AS PivotTable"


        '--4
        _Qry &= vbCrLf & " UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'จำนวนพนักงานที่ลาออกของเดือน' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select 'Number  workers that departed in the month' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  FROM("
        _Qry &= vbCrLf & "   Select  SW.FTMonthsum,SW.FTToTalWork"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumReason AS SW"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") AS SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & "  AVG(FTToTalWork)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") AS PivotTable"


        '--5
        _Qry &= vbCrLf & " UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'บันทึกลาย้อนหลัง(Manday)' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select 'Number  production worker unplanned person-day (man-day) absences in the month' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  FROM("
        _Qry &= vbCrLf & " Select  SW.FTMonthsum,SW.FTToTalWork"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave  AS SW"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & "  And SW.FNTypeLeave ='5' And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") AS SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & "  AVG(FTToTalWork)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") AS PivotTable"


        '--6
        _Qry &= vbCrLf & "  UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'พนักงานที่บันทึกลาย้อนหลัง' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select ' Number  production workers with unplanned absence in the month' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  FROM("
        _Qry &= vbCrLf & "   Select  SW.FTMonthsum,SW.FTToTalWork"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave  AS SW"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & "  And SW.FNTypeLeave ='6' And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") AS SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & "  AVG(FTToTalWork)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") AS PivotTable"


        '--7
        _Qry &= vbCrLf & "  UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'Absenteeism -Monthly  ' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select 'Absenteeism -Monthly ' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  FROM("
        _Qry &= vbCrLf & "  Select  SW.FTMonthsum"
        _Qry &= vbCrLf & ", (MA.FTToTalWork * 100)/(SW.FTToTalWork*SW.FTDaysum )AS FTToTalWork"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS SW LEFT OUTER Join"
        _Qry &= vbCrLf & "(Select A.FTMalesum, A.FTFeMalesum, A.FTToTalWork, A.FNHSysCmpId, A.FTMonthsum From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A Where A.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & " And A.FNTypeLeave ='5')AS MA ON SW.FNHSysCmpId=MA.FNHSysCmpId AND SW.FTMonthsum=MA.FTMonthsum"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") AS SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & "      AVG(FTToTalWork)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") AS PivotTable"

        '--8
        _Qry &= vbCrLf & " UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'Turnover - Monthly  ' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select 'Turnover - Monthly  ' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  From("
        _Qry &= vbCrLf & " Select  SW.FTMonthsum"
        _Qry &= vbCrLf & ", (A.FTToTalWork * 100)/SW.FTToTalWork AS FTToTalWork"
        _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumWork AS SW LEFT OUTER Join"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumReason AS A ON  SW.FNHSysCmpId=A.FNHSysCmpId And SW.FTMonthsum=A.FTMonthsum"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") As SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & " AVG(FTToTalWork)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") As PivotTable"


        '--9
        _Qry &= vbCrLf & "UNION ALL"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "Select 'AVF Absence -Monthly' AS Detail,"
        Else
            _Qry &= vbCrLf & "Select 'AVF Absence -Monthly' AS Detail,"
        End If
        _Qry &= vbCrLf & "[1], [2], [3], [4]  , [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12]  "
        _Qry &= vbCrLf & "  From("
        _Qry &= vbCrLf & "  SELECT SW.FTMonthsum"
        _Qry &= vbCrLf & ",   SW.FTToTalWork/MA.FTToTalWork AS FTToTalWork"
        _Qry &= vbCrLf & "from(select A.FTMalesum , A.FTFeMalesum, A.FTToTalWork, A.FNHSysCmpId, A.FTMonthsum, A.FDInsDate from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A where A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & "  And A.FNTypeLeave='5')AS SW LEFT OUTER JOIN"
        _Qry &= vbCrLf & "(select A.FTMalesum ,A.FTFeMalesum,A.FTToTalWork,A.FNHSysCmpId,A.FTMonthsum from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRMSumLeave AS A where  A.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & "  And A.FNTypeLeave='6')AS MA ON SW.FNHSysCmpId=MA.FNHSysCmpId AND SW.FTMonthsum=MA.FTMonthsum"
        _Qry &= vbCrLf & "Where SW.FNHSysCmpId =  " & HI.ST.SysInfo.CmpID & " And SW.FDInsDate Like '%" & Me.FNYear.Text & "%'"
        _Qry &= vbCrLf & ") As SourceTable "
        _Qry &= vbCrLf & " PIVOT(  "
        _Qry &= vbCrLf & " AVG(FTToTalWork)"
        _Qry &= vbCrLf & "For FTMonthsum IN ( [1], [2], [3], [4], [5]  , [6]  , [7]  , [8]  , [9]  , [10]  , [11]  , [12] )  "
        _Qry &= vbCrLf & ") As PivotTable"


        With Me.ogcsum
            .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            ogvsum.ExpandAllGroups()
            ogvsum.RefreshData()
        End With


    End Function

    Private Sub GroupControl12_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles GroupControl12.Paint

    End Sub

    Private Sub ogbmainprocbutton_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs) Handles ogbmainprocbutton.Paint

    End Sub

    Private Sub ogcsummary_Click(sender As Object, e As EventArgs) Handles ogcsummary.Click

    End Sub
End Class