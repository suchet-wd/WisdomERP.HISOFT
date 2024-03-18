Public Class wEmployeeWorkforce

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


#Region "General"



    Private Sub wEmployeeExpiry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        ' Call InitGrid()0
        '  Me.LoadDataInfo()
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
        Me.LoadData(_Spls)
        Me.SaveData()
        Me.LoadDataYear(_Spls)
        _Spls.Close()
    End Sub
    Private Function LoadData(Spls As HI.TL.SplashScreen) As Boolean


        Dim tmonth As String = ""
        Dim Emonth As String = ""
        Dim MM As String = ""
        Dim MMM As String = ""
        tmonth = HI.TL.CboList.GetListValue(FTDate.Properties.Tag.ToString, FTDate.SelectedIndex)
        Emonth = HI.TL.CboList.GetListValue(FTDateTo.Properties.Tag.ToString, FTDateTo.SelectedIndex)


        If tmonth < 10 Then
            MM = "0" & tmonth
        Else
            MM = tmonth

        End If
        If Emonth < 10 Then
            MMM = "0" & Emonth
        Else
            MMM = Emonth

        End If

        Dim _Qry As String = ""

        _Qry = "SELECT 'Number of employees in TOTAL WORKFORCE' AS Detail1,'พนักงานรวมทั้งหมด(ทุกลูกค้า) (ระดับพนักงาน - ระดับบริหารประจำ location)' AS Detail2, month(E.FDInsDate)AS FTMonth,RIGHT(year(E.FDInsDate),2) AS FTYear,COUNT(L.FNListIndex)AS Number,E.FNHSysCmpId,W.FTWorkforceRefCode,'รวมทั้งหมด' AS FNEmpSex"
        _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON E.FNHSysPositId=P.FNHSysPositId  LEFT OUTER JOIN"
        _Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNWorkForceType') AS L ON  P.FNWorkForceType=L.FNListIndex LEFT OUTER JOIN "
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMWorkforceRef AS W WITH (NOLOCK) ON '3'=W.FNEmpSex AND '0'=W.FNWorkForceType"
        _Qry &= vbCrLf & " WHERE E.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " AND L.FTNameTH<>'NULL' AND month(E.FDInsDate) >='" & MM & "'  AND month(E.FDInsDate)<='" & MMM & "' AND year(E.FDInsDate)='" & Me.FNYear.Text & "' AND W.FNPromoteType='0'"
        _Qry &= vbCrLf & "GROUP BY month(E.FDInsDate),year(E.FDInsDate),E.FNHSysCmpId,W.FTWorkforceRefCode "

        _Qry &= vbCrLf & "UNION ALL"

        _Qry &= vbCrLf & "SELECT 'Number of employees in TOTAL WORKFORCE'AS Detail1,'พนักงานรวมทั้งหมด(ทุกลูกค้า) (ระดับพนักงาน - ระดับบริหารประจำ location)' AS Detail2, month(E.FDInsDate)AS FTMonth,RIGHT(year(E.FDInsDate),2) AS FTYear,COUNT(L.FNListIndex)AS Number,E.FNHSysCmpId,W.FTWorkforceRefCode"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & ",EX.FTNameTH AS FNEmpSex"
        Else
            _Qry &= vbCrLf & ",EX.FTNameEN AS FNEmpSex"
        End If
        _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON E.FNHSysPositId=P.FNHSysPositId  LEFT OUTER JOIN"
        _Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNWorkForceType') AS L ON  P.FNWorkForceType=L.FNListIndex LEFT OUTER JOIN"
        _Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNEmpSex') AS EX ON E.FNEmpSex=EX.FNListIndex  LEFT OUTER JOIN "
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMWorkforceRef AS W WITH (NOLOCK) ON  EX.FNListIndex=W.FNEmpSex AND '0'=W.FNWorkForceType"
        _Qry &= vbCrLf & " WHERE E.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " AND L.FTNameTH<>'NULL' AND month(E.FDInsDate) >='" & MM & "'  AND month(E.FDInsDate)<='" & MMM & "' AND year(E.FDInsDate)='" & Me.FNYear.Text & "' AND W.FNPromoteType='0'"
        _Qry &= vbCrLf & "GROUP BY EX.FTNameTH, month(E.FDInsDate),year(E.FDInsDate) ,E.FNHSysCmpId,W.FTWorkforceRefCode"

        _Qry &= vbCrLf & "UNION ALL"

        _Qry &= vbCrLf & "SELECT 'Number of  employees in '+  +L.FTNameEN+ +'positions' AS Detail1,L.FTNameTH+ +'ทั้งหมด' AS Detail2, month(E.FDInsDate)AS FTMonth,RIGHT(year(E.FDInsDate),2) AS FTYear,COUNT(L.FNListIndex)AS Number,E.FNHSysCmpId,W.FTWorkforceRefCode"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & ",EX.FTNameTH AS FNEmpSex"
        Else
            _Qry &= vbCrLf & ",EX.FTNameEN AS FNEmpSex"
        End If
        _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON E.FNHSysPositId=P.FNHSysPositId  LEFT OUTER JOIN"
        _Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNWorkForceType') AS L ON  P.FNWorkForceType=L.FNListIndex LEFT OUTER JOIN"
        _Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNEmpSex') AS EX ON E.FNEmpSex=EX.FNListIndex LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMWorkforceRef AS W WITH (NOLOCK) ON  EX.FNListIndex=W.FNEmpSex AND L.FNListIndex=W.FNWorkForceType"
        _Qry &= vbCrLf & " WHERE E.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " AND L.FTNameTH<>'NULL' AND month(E.FDInsDate) >='" & MM & "'  AND month(E.FDInsDate)<='" & MMM & "' AND year(E.FDInsDate)='" & Me.FNYear.Text & "' AND W.FNPromoteType='0' AND  L.FNListIndex<>'0'"
        _Qry &= vbCrLf & "GROUP BY L.FTNameEN,EX.FTNameTH,L.FTNameTH, month(E.FDInsDate),year(E.FDInsDate) ,E.FNHSysCmpId,W.FTWorkforceRefCode"



        _Qry &= vbCrLf & "UNION ALL"

        _Qry &= vbCrLf & "SELECT 'Number of  employees  promotions internal' AS Detail1,'จำนวนรวมการปรับตำแหน่ง เลื่อนขั้น ภายใน ทั้งหมดรวมชายหญิง' AS Detail2 ,month(M.FTInsDate)AS FTMonth,RIGHT(year(M.FTInsDate),2) AS FTYear,count(M.FNHSysEmpID)AS Number,M.FNHSysCmpId,W.FTWorkforceRefCode AS FTWorkforceRefCode,'ย้ายตำแหน่งรวมทั้งหมด' AS FNEmpSex"
        _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeMasterChange AS M  WITH (NOLOCK) ON E.FNHSysEmpID=M.FNHSysEmpID  AND E.FNHSysCmpId=M.FNHSysCmpId LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPromotionReason AS PM WITH (NOLOCK) ON M.FNHSysPmtReasoneId = PM.FNHSysPmtReasoneId LEFT OUTER JOIN"
        '_Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId=P.FNHSysPositId LEFT OUTER JOIN"
        '_Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS PT WITH (NOLOCK) ON M.FNHSysPositIdTo=PT.FNHSysPositId LEFT OUTER JOIN"
        '_Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNWorkForceType') AS L ON  P.FNWorkForceType=L.FNListIndex LEFT OUTER JOIN"
        '_Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNWorkForceType') AS LT ON  PT.FNWorkForceType=LT.FNListIndex LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMWorkforceRef AS W WITH (NOLOCK) ON '3'=W.FNEmpSex AND   '0'=W.FNWorkForceType    AND '1'=W.FNPromoteType"
        _Qry &= vbCrLf & " WHERE E.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  AND month(M.FTInsDate) >='" & MM & "'  AND month(M.FTInsDate)<='" & MMM & "' AND year(M.FTInsDate)='" & Me.FNYear.Text & "' AND PM.FTPmtReasonCode='P08' "
        _Qry &= vbCrLf & "GROUP BY  month(M.FTInsDate),year(M.FTInsDate),M.FNHSysCmpId,W.FTWorkforceRefCode "

        _Qry &= vbCrLf & "UNION ALL"

        _Qry &= vbCrLf & "SELECT 'Number of  employees  Promote  From  ' +P1.FTPositNameEN+' Positions To ' + P2.FTPositNameEN +' Positions' AS Detail1,'ปรับตำแหน่ง' + P1.FTPositNameTH+ +'ทั้งหมด เป็น' + P2.FTPositNameTH AS Detail2,month(M.FTInsDate)AS FTMonth,RIGHT(year(M.FTInsDate),2) AS FTYear,count(M.FNHSysEmpID)AS Number,M.FNHSysCmpId,W.FTWorkforceRefCode AS FTWorkforceRefCode"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & ",EX.FTNameTH AS FNEmpSex"
        Else
            _Qry &= vbCrLf & ",EX.FTNameEN AS FNEmpSex"
        End If
        _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeMasterChange AS M  WITH (NOLOCK) ON E.FNHSysEmpID=M.FNHSysEmpID  AND E.FNHSysCmpId=M.FNHSysCmpId LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P1 WITH (NOLOCK) ON M.FNHSysPositId=P1.FNHSysPositId LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P2 WITH (NOLOCK) ON M.FNHSysPositIdTo=P2.FNHSysPositId LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPromotionReason AS PM WITH (NOLOCK) ON M.FNHSysPmtReasoneId = PM.FNHSysPmtReasoneId LEFT OUTER JOIN"
        _Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNWorkForceType') AS L ON  P1.FNWorkForceType=L.FNListIndex LEFT OUTER JOIN"
        _Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNWorkForceType') AS LT ON  P2.FNWorkForceType=LT.FNListIndex LEFT OUTER JOIN"
        _Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNEmpSex') AS EX ON E.FNEmpSex=EX.FNListIndex  LEFT OUTER JOIN "
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMWorkforceRef AS W WITH (NOLOCK) ON EX.FNListIndex=W.FNEmpSex AND   L.FNListIndex=W.FNWorkForceType    AND '1'=W.FNPromoteType"
        _Qry &= vbCrLf & " WHERE M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  AND month(M.FTInsDate) >='" & MM & "'  AND month(M.FTInsDate)<='" & MMM & "' AND year(M.FTInsDate)='" & Me.FNYear.Text & "' AND PM.FTPmtReasonCode='P08'"
        _Qry &= vbCrLf & "GROUP BY  P1.FTPositNameTH ,P2.FTPositNameTH ,P1.FTPositNameEN,P2.FTPositNameEN ,PM.FTPmtReasonNameTH,M.FNHSysCmpId, PM.FTPmtReasonCode , month(M.FTInsDate),year(M.FTInsDate)   ,EX.FTNameTH,W.FTWorkforceRefCode"



        _Qry &= vbCrLf & "UNION ALL"

        _Qry &= vbCrLf & "SELECT 'Median  PRODUCTION WORKER tenure Total' AS Detail1,'อายุงานเฉลี่ยของพนักงานทั้งหมด' AS Detail2, month(E.FDInsDate)AS FTMonth,RIGHT(year(E.FDInsDate),2) AS FTYear,isnull(Convert(numeric(18,2),SE.avg_dd),0)AS Number,E.FNHSysCmpId,W.FTWorkforceRefCode AS FTWorkforceRefCode,'รวมทั้งหมด' AS FNEmpSex"
        _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON E.FNHSysPositId=P.FNHSysPositId  LEFT OUTER JOIN"
        _Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNWorkForceType') AS L ON  P.FNWorkForceType=L.FNListIndex LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMWorkforceRef AS W WITH (NOLOCK) ON '3'=W.FNEmpSex And   14=W.FNWorkForceType  LEFT OUTER JOIN"
        _Qry &= vbCrLf & " ( SELECT  M.FNHSysCmpId,sum(datediff( DAY,M.FDDateStart,getdate()))AS DD, count(FNHSysEmpID) as n"
        _Qry &= vbCrLf & ", sum(datediff( DAY,M.FDDateStart,getdate()))  /  count(FNHSysEmpID)  AS  avg_dd, month(M.FDInsDate)AS FTMonth"
        _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) "
        _Qry &= vbCrLf & " WHERE        (M.FTEmpCode <> '') and FNEmpStatus <> 2    AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " "
        _Qry &= vbCrLf & "   AND month(M.FDInsDate) >='" & MM & "'  AND month(M.FDInsDate)<='" & MMM & "' AND year(M.FDInsDate)='" & Me.FNYear.Text & "'"
        _Qry &= vbCrLf & " GROUP BY M.FNHSysCmpId, month(M.FDInsDate))AS SE ON E.FNHSysCmpId=SE.FNHSysCmpId  AND  month(E.FDInsDate)=SE.FTMonth"
        _Qry &= vbCrLf & " WHERE E.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " AND L.FTNameTH<>'NULL' AND month(E.FDInsDate) >='" & MM & "'  AND month(E.FDInsDate)<='" & MMM & "' AND year(E.FDInsDate)='" & Me.FNYear.Text & "' "
        _Qry &= vbCrLf & "GROUP BY  month(E.FDInsDate),year(E.FDInsDate) ,E.FNHSysCmpId,SE.avg_dd,W.FTWorkforceRefCode"


        _Qry &= vbCrLf & "UNION ALL"

        _Qry &= vbCrLf & "SELECT 'Median  PRODUCTION WORKER tenure'+ EX.FTNameEN AS Detail1,'อายุงานเฉลี่ยของพนักงาน'+ EX.FTNameTH AS Detail2, month(E.FDInsDate)AS FTMonth,RIGHT(year(E.FDInsDate),2) AS FTYear,isnull(Convert(numeric(18,2),SE.avg_dd),0)AS Number,E.FNHSysCmpId,W.FTWorkforceRefCode AS FTWorkforceRefCode"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & ",EX.FTNameTH AS FNEmpSex"
        Else
            _Qry &= vbCrLf & ",EX.FTNameEN AS FNEmpSex"
        End If
        _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON E.FNHSysPositId=P.FNHSysPositId  LEFT OUTER JOIN"
        _Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNWorkForceType') AS L ON  P.FNWorkForceType=L.FNListIndex LEFT OUTER JOIN"
        _Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNEmpSex') AS EX ON E.FNEmpSex=EX.FNListIndex  LEFT OUTER JOIN "
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMWorkforceRef AS W WITH (NOLOCK) ON EX.FNListIndex=W.FNEmpSex And   14=W.FNWorkForceType  LEFT OUTER JOIN"
        _Qry &= vbCrLf & " ( SELECT  M.FNHSysCmpId,sum(datediff( DAY,M.FDDateStart,getdate()))AS DD, count(FNHSysEmpID) as n"
        _Qry &= vbCrLf & ", sum(datediff( DAY,M.FDDateStart,getdate()))  /  count(FNHSysEmpID)  AS  avg_dd,M.FNEmpSex, month(M.FDInsDate)AS FTMonth"
        _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) "
        _Qry &= vbCrLf & " WHERE        (M.FTEmpCode <> '') and FNEmpStatus <> 2    AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " "
        _Qry &= vbCrLf & "  AND month(M.FDInsDate) >='" & MM & "'  AND month(M.FDInsDate)<='" & MMM & "' AND year(M.FDInsDate)='" & Me.FNYear.Text & "'"
        _Qry &= vbCrLf & " GROUP BY M.FNHSysCmpId,M.FNEmpSex, month(M.FDInsDate))AS SE ON E.FNHSysCmpId=SE.FNHSysCmpId AND E.FNEmpSex=SE.FNEmpSex AND  month(E.FDInsDate)=SE.FTMonth"
        _Qry &= vbCrLf & " WHERE E.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " AND L.FTNameTH<>'NULL' AND month(E.FDInsDate) >='" & MM & "'  AND month(E.FDInsDate)<='" & MMM & "' AND year(E.FDInsDate)='" & Me.FNYear.Text & "' "
        _Qry &= vbCrLf & "GROUP BY EX.FTNameTH, month(E.FDInsDate),year(E.FDInsDate) ,E.FNHSysCmpId,SE.avg_dd,W.FTWorkforceRefCode,EX.FTNameEN "


        _Qry &= vbCrLf & "UNION ALL"

        _Qry &= vbCrLf & "SELECT 'Median  PRODUCTION WORKER Age Total' AS Detail1,'อายุเฉลี่ยของพนักงานทั้งหมด' AS Detail2, month(E.FDInsDate)AS FTMonth,RIGHT(year(E.FDInsDate),2),isnull(Convert(numeric(18,2),SE.avg_dd),0)AS Number,E.FNHSysCmpId,W.FTWorkforceRefCode AS FTWorkforceRefCode,'รวมทั้งหมด' AS FNEmpSex"
        _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON E.FNHSysPositId=P.FNHSysPositId  LEFT OUTER JOIN"
        _Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNWorkForceType') AS L ON  P.FNWorkForceType=L.FNListIndex LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMWorkforceRef AS W WITH (NOLOCK) ON '3'=W.FNEmpSex And   15=W.FNWorkForceType  LEFT OUTER JOIN"
        _Qry &= vbCrLf & " ( SELECT  M.FNHSysCmpId,sum(datediff( DAY,M.FDBirthDate,getdate()))AS DD, count(FNHSysEmpID) as n"
        _Qry &= vbCrLf & ", sum(datediff( DAY,M.FDBirthDate,getdate()))  /  count(FNHSysEmpID)  AS  avg_dd, month(M.FDInsDate)AS FTMonth"
        _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) "
        _Qry &= vbCrLf & " WHERE        (M.FTEmpCode <> '') and FNEmpStatus <> 2    AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " "
        _Qry &= vbCrLf & "   AND month(M.FDInsDate) >='" & MM & "'  AND month(M.FDInsDate)<='" & MMM & "' AND year(M.FDInsDate)='" & Me.FNYear.Text & "'"
        _Qry &= vbCrLf & " GROUP BY M.FNHSysCmpId, month(M.FDInsDate))AS SE ON E.FNHSysCmpId=SE.FNHSysCmpId  AND  month(E.FDInsDate)=SE.FTMonth"
        _Qry &= vbCrLf & " WHERE E.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " AND L.FTNameTH<>'NULL' AND month(E.FDInsDate) >='" & MM & "'  AND month(E.FDInsDate)<='" & MMM & "' AND year(E.FDInsDate)='" & Me.FNYear.Text & "' "
        _Qry &= vbCrLf & "GROUP BY  month(E.FDInsDate),year(E.FDInsDate) ,E.FNHSysCmpId,SE.avg_dd,W.FTWorkforceRefCode"


        _Qry &= vbCrLf & "UNION ALL"

        _Qry &= vbCrLf & "SELECT 'Median  PRODUCTION WORKER Age'+ EX.FTNameEN AS Detail1,'อายุเฉลี่ยของพนักงาน' + EX.FTNameTH  AS Detail2, month(E.FDInsDate)AS FTMonth,RIGHT(year(E.FDInsDate),2) AS FTYear,isnull(Convert(numeric(18,2),SE.avg_dd),0)AS Number,E.FNHSysCmpId,W.FTWorkforceRefCode AS FTWorkforceRefCode"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & ",EX.FTNameTH AS FNEmpSex"
        Else
            _Qry &= vbCrLf & ",EX.FTNameEN AS FNEmpSex"
        End If
        _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON E.FNHSysPositId=P.FNHSysPositId  LEFT OUTER JOIN"
        _Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNWorkForceType') AS L ON  P.FNWorkForceType=L.FNListIndex LEFT OUTER JOIN"
        _Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNEmpSex') AS EX ON E.FNEmpSex=EX.FNListIndex  LEFT OUTER JOIN "
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMWorkforceRef AS W WITH (NOLOCK) ON EX.FNListIndex=W.FNEmpSex And   15=W.FNWorkForceType  LEFT OUTER JOIN"
        _Qry &= vbCrLf & " ( SELECT  M.FNHSysCmpId,sum(datediff( DAY,M.FDBirthDate,getdate()))AS DD, count(FNHSysEmpID) as n"
        _Qry &= vbCrLf & ", sum(datediff( DAY,M.FDBirthDate,getdate()))  /  count(FNHSysEmpID)  AS  avg_dd,M.FNEmpSex, month(M.FDInsDate)AS FTMonth"
        _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) "
        _Qry &= vbCrLf & " WHERE        (M.FTEmpCode <> '') and FNEmpStatus <> 2    AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " "
        _Qry &= vbCrLf & "  AND month(M.FDInsDate) >='" & MM & "'  AND month(M.FDInsDate)<='" & MMM & "' AND year(M.FDInsDate)='" & Me.FNYear.Text & "'"
        _Qry &= vbCrLf & " GROUP BY M.FNHSysCmpId,M.FNEmpSex, month(M.FDInsDate))AS SE ON E.FNHSysCmpId=SE.FNHSysCmpId AND E.FNEmpSex=SE.FNEmpSex AND  month(E.FDInsDate)=SE.FTMonth"
        _Qry &= vbCrLf & " WHERE E.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & " AND L.FTNameTH<>'NULL' AND month(E.FDInsDate) >='" & MM & "'  AND month(E.FDInsDate)<='" & MMM & "' AND year(E.FDInsDate)='" & Me.FNYear.Text & "' "
        _Qry &= vbCrLf & "GROUP BY EX.FTNameTH, month(E.FDInsDate),year(E.FDInsDate) ,E.FNHSysCmpId,SE.avg_dd,W.FTWorkforceRefCode ,W.FNWorkForceType,EX.FTNameEN"







        With Me.ogcsummary
            .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            ogvsummary.ExpandAllGroups()
            ogvsummary.RefreshData()
        End With



    End Function

    Private Function LoadDataYear(Spls As HI.TL.SplashScreen) As Boolean


        Dim MM As String = ""
        Dim MMM As String = ""


        MM = HI.TL.CboList.GetListValue(FTDate.Properties.Tag.ToString, FTDate.SelectedIndex)
        MMM = HI.TL.CboList.GetListValue(FTDateTo.Properties.Tag.ToString, FTDateTo.SelectedIndex)
        Dim tmonth As String = ""
        Dim Emonth As String = ""

        If MM < 10 Then
            tmonth = "0" & MM
        Else
            tmonth = MM

        End If
        If MMM < 10 Then
            Emonth = "0" & MMM
        Else
            Emonth = MMM

        End If


        Dim _Qry As String = ""

        _Qry = "SELECT W.FTCodeRef AS FTRefCode,W.FTDetailEN,W.FTDetailTH,SUM(W.FTNumber)AS FTNumber ,W.FNHSysCmpId"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & ",isnull(EX.FTNameTH,'รวมทั้งหมด') AS FNSex"
        Else
            _Qry &= vbCrLf & ",isnull(EX.FTNameEN,'ToTal') AS FNSex"
        End If
        _Qry &= vbCrLf & "From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRWorkforce AS W WITH (NOLOCK) LEFT OUTER Join"
        _Qry &= vbCrLf & " (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS L WHERE L.FTListName='FNEmpSex') AS EX ON W.FNEmpSex=EX.FNListIndex "
        _Qry &= vbCrLf & " WHERE W.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  AND W.FTMonth>='" & tmonth & "'  AND W.FTMonth<='" & Emonth & "' AND  W.FTYear='" & Me.FNYear.Text & "'"
        _Qry &= vbCrLf & "GROUP BY  W.FTCodeRef,W.FTDetailEN,W.FTDetailTH,EX.FTNameTH,W.FNHSysCmpId "
        _Qry &= vbCrLf & "ORDER BY  W.FTDetailEN  DESC "



        With Me.ogcsum
            .DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
            ogvsum.ExpandAllGroups()
            ogvsum.RefreshData()
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
            'If Me.Saveworke() Then

            '    Call Savenewemp()
            '    Call SaveempRea()
            '    Call Saveleave()
            '    _Spls.Close()
            '    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            'Else
            '    _Spls.Close()
            '    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            'End If






        End If

        '        Else
        '            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการบันทึก กรุณาทำการตรวจสอบ !!!", 1512210547, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        '        End If
        '    Else
        '        HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการบันทึก กรุณาทำการตรวจสอบ !!!", 1512210547, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        '    End If




        'End If
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        If HI.MG.ShowMsg.mConfirmProcessDefaultNo(MG.ShowMsg.ProcessType.mDelete) = True Then
            Dim _Spls As New HI.TL.SplashScreen("Deleting data...   Please Wait  ")
            If Me.DeleteDataworke() Then


                _Spls.Close()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                HI.TL.HandlerControl.ClearControl(Me)

                HI.TL.HandlerControl.ClearControl(ogcsummary)
                ogcsummary.DataSource = Nothing
                HI.TL.HandlerControl.ClearControl(ogcsum)
                ogcsum.DataSource = Nothing
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
            With CType(Me.ogcsummary.DataSource, DataTable)
                .AcceptChanges()
                dt = .Copy
            End With
            Dim MM As String = ""
            Dim MMM As String = ""
            Dim _kep As String = ""
            Dim _EmpSex As String = ""
            Dim _Sex As String = ""


            MM = HI.TL.CboList.GetListValue(FTDate.Properties.Tag.ToString, FTDate.SelectedIndex)
            MMM = HI.TL.CboList.GetListValue(FTDateTo.Properties.Tag.ToString, FTDateTo.SelectedIndex)
            '_EmpSex = HI.Conn.SQLConn.GetField("SELECT FNListIndex FROM (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.HSysListData AS L WHERE L.FTListName='FNEmpSex')AS A WHERE FTNameTH='" & R!FNEmpSex.ToString & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")



            For Each R As DataRow In dt.Select("FNHSysCmpId>0 ", "FNHSysCmpId")
                _EmpSex = HI.Conn.SQLConn.GetField("SELECT FNListIndex FROM (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.HSysListData AS L WHERE L.FTListName='FNEmpSex')AS A WHERE FTNameTH='" & R!FNEmpSex.ToString & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                If _EmpSex = "" Then
                    _Sex = "3"
                Else
                    _Sex = _EmpSex
                End If
                _kep = HI.Conn.SQLConn.GetField("SELECT TOP 1 S.FNHSysWorkforceId  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRWorkforce AS S WITH (NOLOCK) WHERE S.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND S.FTMonth='" & R!FTMonth.ToString & "'  AND S.FTYear='" & R!FTYear.ToString & "' AND S.FNEmpSex='" & _Sex & "' AND S.FTDetailTH='" & R!Detail2.ToString & "'", Conn.DB.DataBaseName.DB_SYSTEM, "")

                Dim _Str As String
                _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRWorkforce WHERE  FNHSysCmpId = " & HI.ST.SysInfo.CmpID & " AND FTMonth='" & R!FTMonth.ToString & "'  AND  FTYear='" & Me.FNYear.Text & "' AND FNHSysWorkforceId='" & _kep & "'"
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

    Private Function SaveData() As Boolean
        Dim _Qry As String = ""


        Dim dt As DataTable
        With CType(Me.ogcsummary.DataSource, DataTable)
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
            Dim _Sex As String = ""
            Dim _EmpSex As String = ""
            Dim _Run As String = ""
            Dim _RunC As String = ""

            _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCmp WHERE FNHSysCmpId=" & Val("" & Me.FNHSysCmpId.Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")

            Dim tyear As String = ""
            Dim tday As String = ""
            Dim tmonth As String = ""
            Dim Emonth As String = ""
            Dim _month As String = ""
            Dim _Seq As Integer = 0
            Dim _ii As String = ""



            For Each R As DataRow In dt.Select("FNHSysCmpId>0 ", "FNHSysCmpId")
                _EmpSex = HI.Conn.SQLConn.GetField("SELECT FNListIndex FROM (SELECT L.FTNameTH,L.FTNameEN,L.FNListIndex  FROM " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & ".dbo.HSysListData AS L WHERE L.FTListName='FNEmpSex')AS A WHERE FTNameTH='" & R!FNEmpSex.ToString & "' ", Conn.DB.DataBaseName.DB_SYSTEM, "")
                tyear = R!FTYear.ToString

                Emonth = R!FTMonth.ToString
                tday = Microsoft.VisualBasic.DateAndTime.Day(Date.Today)
                _Seq += +1
                _ii = _Seq.ToString("000")
                _Run = HI.Conn.SQLConn.GetField("SELECT TOP 1 RIGHT(S.FTWorkforceRefCode,2)as Code  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMWorkforceRef AS S WITH (NOLOCK) WHERE  S.FTWorkforceRefCode='" & R!FTWorkforceRefCode.ToString & "'", Conn.DB.DataBaseName.DB_SYSTEM, "")

                If _EmpSex = "" Then
                    _Sex = "3"
                Else
                    _Sex = _EmpSex
                End If

                If _Run = "" Then
                    _RunC = _Seq
                Else
                    _RunC = _Run
                End If
                If Emonth < 10 Then
                    tmonth = "0" & R!FTMonth.ToString
                Else
                    tmonth = R!FTMonth.ToString

                End If


                _SystemKey = (tyear & "" & tmonth & "" & _CmpH & "" & _ii)

                _kep = HI.Conn.SQLConn.GetField("SELECT TOP 1 S.FNHSysWorkforceId  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRWorkforce AS S WITH (NOLOCK) WHERE S.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " AND S.FTMonth='" & tmonth & "'  AND S.FTYear='" & tyear & "' AND S.FNEmpSex='" & _Sex & "' AND S.FTDetailTH='" & R!Detail2.ToString & "'", Conn.DB.DataBaseName.DB_SYSTEM, "")


                _Str = " UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRWorkforce"
                _Str &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Str &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Str &= vbCrLf & " , FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Str &= vbCrLf & " , FNEmpSex=" & _Sex & ""
                _Str &= vbCrLf & " , FTDetailEN='" & (R!Detail1.ToString) & "'"
                _Str &= vbCrLf & " , FTDetailTH='" & (R!Detail2.ToString) & "'"
                _Str &= vbCrLf & " , FTCodeRef='" & (R!FTWorkforceRefCode.ToString) & "'"
                _Str &= vbCrLf & "   WHERE  FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ""
                ' _Str &= vbCrLf & " AND FTMonth=" & Val(R!FTMonth.ToString) & ""
                _Str &= vbCrLf & " AND  FTMonth='" & tmonth & "'"
                _Str &= vbCrLf & " AND FTYear=" & Me.FNYear.Text & ""
                _Str &= vbCrLf & " AND  FNHSysWorkforceId='" & _kep & "'"


                If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTEmployeeHRWorkforce"
                    _Str &= vbCrLf & " ("
                    _Str &= vbCrLf & " FTInsUser, FDInsDate, FTInsTime"
                    _Str &= vbCrLf & ", FNHSysWorkforceId, FNHSysCmpId, FTMonth, FTYear, FTNumber, FNEmpSex, FTDetailEN,FTDetailTH,FTCodeRef"
                    _Str &= vbCrLf & " )"
                    _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Str &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""
                    _Str &= vbCrLf & " ," & Val(_SystemKey) & ""
                    _Str &= vbCrLf & " ," & HI.ST.SysInfo.CmpID & ""
                    '_Str &= vbCrLf & " ," & Val(R!FTMonth.ToString) & ""
                    _Str &= vbCrLf & " ,'" & tmonth & "'"
                    _Str &= vbCrLf & " ," & Me.FNYear.Text & ""
                    _Str &= vbCrLf & " ," & Val(R!Number.ToString) & ""
                    _Str &= vbCrLf & " ," & _Sex & ""
                    _Str &= vbCrLf & " ,'" & (R!Detail1.ToString) & "'"
                    _Str &= vbCrLf & " ,'" & (R!Detail2.ToString) & "'"
                    _Str &= vbCrLf & " ,'" & (R!FTWorkforceRefCode.ToString) & "'"


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

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)

        HI.TL.HandlerControl.ClearControl(ogcsummary)
        ogcsummary.DataSource = Nothing
        HI.TL.HandlerControl.ClearControl(ogcsum)
        ogcsum.DataSource = Nothing
    End Sub
End Class