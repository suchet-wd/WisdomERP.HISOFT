Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.Utils
Imports DevExpress.XtraPivotGrid

Public Class wDailyInOutYear
    Private dtCompany As DataTable
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        PivotGridControl.OptionsChartDataSource.ProvideDataByColumns = False
        PivotGridControl.OptionsChartDataSource.SelectionOnly = False
        PivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = True
        PivotGridControl.OptionsChartDataSource.ProvideRowGrandTotals = False
        PivotGridControl.OptionsChartDataSource.ProvideColumnGrandTotals = False
        chartControl.CrosshairOptions.ShowArgumentLine = False


        PivotGridControl2.OptionsChartDataSource.ProvideDataByColumns = False
        PivotGridControl2.OptionsChartDataSource.SelectionOnly = False
        PivotGridControl2.OptionsChartDataSource.ProvideColumnGrandTotals = True
        PivotGridControl2.OptionsChartDataSource.ProvideRowGrandTotals = False
        PivotGridControl2.OptionsChartDataSource.ProvideColumnGrandTotals = False
        ChartControl2.CrosshairOptions.ShowArgumentLine = False

        chartControl.DataSource = PivotGridControl
        ChartControl2.DataSource = PivotGridControl2
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

#End Region

#Region "Procedure"

    Private Sub LoadCompany()
        Dim _Str As String
        Try
            _Str = " SELECT   '0' AS FTSelect "
            _Str &= vbCrLf & ",M.FNHSysCmpId"
            _Str &= vbCrLf & ",M.FTCmpCode,ISNULL(IPP.FTIPServer,'') AS FTIPServer,0 AS FNCompensationFoundByYearOption_Hide"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

                _Str &= vbCrLf & " , M.FTCmpNameTH AS FTCmpName "
                _Str &= vbCrLf & " ,ISNULL(("
                _Str &= vbCrLf & "SELECT TOP 1 FTNameTH"
                _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)"
                _Str &= vbCrLf & "WHERE  (FTListName = N'FNCompensationFoundByYearOption') "
                _Str &= vbCrLf & "AND (FNListIndex = 0)"
                _Str &= vbCrLf & " ),'') AS FNCompensationFoundByYearOption "

            Else

                _Str &= vbCrLf & " , M.FTCmpNameEN AS FTCmpName "
                _Str &= vbCrLf & " ,ISNULL(("
                _Str &= vbCrLf & "SELECT TOP 1 FTNameEN"
                _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)"
                _Str &= vbCrLf & "WHERE  (FTListName = N'FNCompensationFoundByYearOption') "
                _Str &= vbCrLf & "AND (FNListIndex = 0)"
                _Str &= vbCrLf & " ),'') AS FNCompensationFoundByYearOption "

            End If

            _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS M WITH(NOLOCK) "
            _Str &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSECompanyIPServer AS IPP WITH(NOLOCK) ON M.FNHSysCmpId = IPP.FNHSysCmpId "
            _Str &= vbCrLf & " WHERE ISNULL(M.FTStateActive,'') ='1' AND ISNULL(IPP.FTIPServer,'') <>'' "
            _Str &= vbCrLf & " ORDER BY M.FTCmpCode"
            dtCompany = Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub LoadData()
        Dim Qry As String = ""
        Dim _Date As Date = Me.FDDate.Text
        Dim dt As DataTable
        Dim dtTemp As New DataTable
        Dim _ServerName, _UID, _PWS, _DBName, _ConnStr, _NewDate As String
        'Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        With dtTemp
            .Columns.Add("FTCmpCode", GetType(String))
            .Columns.Add("TypeMonth", GetType(String))
            .Columns.Add("AmtEmp", GetType(Integer))
        End With
        Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_HR)
        _UID = Conn.DB.UIDName : _PWS = Conn.DB.PWDName : _DBName = Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR)
        _NewDate = UL.ULDate.ConvertEnDB(_Date.AddMonths(-3))

        Try
            For Each R As DataRow In dtCompany.Rows
                _ServerName = R!FTIPServer.ToString

                _ConnStr = "SERVER=" & _ServerName & ";UID=" & _UID & ";PWD=" & _PWS & ";Initial Catalog=" & _DBName
                Qry = "select X.FTCmpCode,X.TypeMonth,sum(X.AmtEmp) AS AmtEmp from"
                Qry &= vbCrLf & "("
                Qry &= vbCrLf & "--อันนี้เกิน 3 เดือน"
                Qry &= vbCrLf & "select C.FTCmpCode,'More_Than3Month' as TypeMonth,1 AS AmtEmp from HITECH_HR..THRMEmployee AS E WITH(NOLOCK) INNER JOIN"
                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS C WITH(NOLOCK) ON E.FNHSysCmpId = C.FNHSysCmpId"
                Qry &= vbCrLf & "where E.FDDateEnd='' and E.FDDateStart<'" & _NewDate & "'"
                Qry &= vbCrLf & "and E.FNHSysEmpTypeId=1403080001"
                Qry &= vbCrLf & "union all"
                Qry &= vbCrLf & "--อันนี้ไม่เกิน 3 เดือน"
                Qry &= vbCrLf & "select C.FTCmpCode,'Less3Month' AS TypeMonth,1 AS AmtEmp  from HITECH_HR..THRMEmployee AS E WITH(NOLOCK) INNER JOIN"
                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS C WITH(NOLOCK) ON E.FNHSysCmpId = C.FNHSysCmpId"
                Qry &= vbCrLf & "where E.FDDateEnd<>'' and E.FDDateStart>='" & _NewDate & "' and E.FDDateEnd<'" & UL.ULDate.ConvertEnDB(_Date) & "'"
                Qry &= vbCrLf & "and E.FNHSysEmpTypeId=1403080001"
                Qry &= vbCrLf & ") AS X"
                Qry &= vbCrLf & "group by X.FTCmpCode,X.TypeMonth"

                dt = HI.Conn.SQLConn.GetDataTableConectstring(Qry, _ConnStr)
                For Each K As DataRow In dt.Rows
                    With dtTemp
                        .Rows.Add(K!FTCmpCode.ToString, K!TypeMonth.ToString, K!AmtEmp)
                        .AcceptChanges()
                    End With
                Next
            Next

            PivotGridControl.DataSource = dtTemp.Copy

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        '_Spls.Close()
    End Sub

    Private Sub Loaddata2()
        Dim Qry As String = ""
        Dim _Date As Date = Me.FDDate.Text
        Dim dt As DataTable
        Dim dtTemp As New DataTable
        Dim _ServerName, _UID, _PWS, _DBName, _ConnStr, _NewDate As String
        'Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        With dtTemp
            .Columns.Add("FTCmpCode", GetType(String))
            .Columns.Add("EmpType", GetType(String))
            .Columns.Add("Amtemp", GetType(Double))
        End With
        Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_HR)
        _UID = Conn.DB.UIDName : _PWS = Conn.DB.PWDName : _DBName = Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR)
        _NewDate = UL.ULDate.ConvertEnDB(_Date.AddMonths(1))

        Try
            For Each R As DataRow In dtCompany.Rows
                _ServerName = R!FTIPServer.ToString

                _ConnStr = "SERVER=" & _ServerName & ";UID=" & _UID & ";PWD=" & _PWS & ";Initial Catalog=" & _DBName
                Qry = "select K.FTCmpCode,K.Amtemp ,K.EmpType from"
                Qry &= vbCrLf & "(select X.FTCmpCode,count(X.FNHSysEmpID) AS Amtemp,X.EmpType from"
                Qry &= vbCrLf & "(select C.FTCmpCode,E.FNHSysEmpID,'พนักงานทั้งหมด' AS EmpType from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee AS E WITH(NOLOCK) inner join"
                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS C WItH(NOLOCK) ON E.FNHSysCmpId=C.FNHSysCmpId"
                Qry &= vbCrLf & "where E.FNHSysEmpTypeId>0 and E.FDDateEnd='' and E.FDDateStart<'" & _NewDate & "'"
                Qry &= vbCrLf & "union all"
                Qry &= vbCrLf & "select C.FTCmpCode,E.FNHSysEmpID,'พนักงานเย็บ' AS EmpType from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee AS E WITH(NOLOCK) inner join"
                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS C WItH(NOLOCK) ON E.FNHSysCmpId=C.FNHSysCmpId"
                Qry &= vbCrLf & "where E.FNHSysEmpTypeId=1403080001 and E.FDDateEnd='' and E.FDDateStart<'" & _NewDate & "'"
                Qry &= vbCrLf & "union all"
                Qry &= vbCrLf & "select C.FTCmpCode,E.FNHSysEmpID,'พนักงานรายเดือน' AS EmpType from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee AS E WITH(NOLOCK) inner join"
                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS C WItH(NOLOCK) ON E.FNHSysCmpId=C.FNHSysCmpId"
                Qry &= vbCrLf & "where E.FNHSysEmpTypeId=1306010002 and E.FDDateEnd='' and E.FDDateStart<'" & _NewDate & "'"
                Qry &= vbCrLf & "union all"
                Qry &= vbCrLf & "select C.FTCmpCode,E.FNHSysEmpID,'พนักงานอื่นๆ' AS EmpType from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee AS E WITH(NOLOCK) inner join"
                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS C WItH(NOLOCK) ON E.FNHSysCmpId=C.FNHSysCmpId"
                Qry &= vbCrLf & "where E.FNHSysEmpTypeId<>1306010002 and E.FNHSysEmpTypeId<>1403080001 and E.FDDateEnd='' and E.FDDateStart<'" & _NewDate & "'"
                Qry &= vbCrLf & ") AS X "
                Qry &= vbCrLf & "group by X.FTCmpCode,X.EmpType "
                Qry &= vbCrLf & "union all"
                Qry &= vbCrLf & "select A.FTCmpCode"
                Qry &= vbCrLf & ",convert(numeric(4,2),convert(numeric(4,2),(100.0*B.AmtOther)/(A.AmtSew+B.AmtOther))/convert(numeric(4,2),(100.0*A.AmtSew)/(A.AmtSew+B.AmtOther))) Amtemp"
                Qry &= vbCrLf & ",'อัตราส่วน' AS EmpType from "
                Qry &= vbCrLf & "(select C.FTCmpCode,count(E.FNHSysEmpID) AS AmtSew,'พนักงานเย็บ' AS EmpType from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee AS E WITH(NOLOCK) inner join"
                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS C WItH(NOLOCK) ON E.FNHSysCmpId=C.FNHSysCmpId"
                Qry &= vbCrLf & "where E.FNHSysEmpTypeId=1403080001 and E.FDDateEnd='' and E.FDDateStart<'" & _NewDate & "'"
                Qry &= vbCrLf & "group by FTCmpCode"
                Qry &= vbCrLf & ") AS A INNER JOIN"
                Qry &= vbCrLf & "(select C.FTCmpCode,count(E.FNHSysEmpID) AS AmtOther,'พนักงานอื่นๆ' AS EmpType from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "]..THRMEmployee AS E WITH(NOLOCK) inner join"
                Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS C WItH(NOLOCK) ON E.FNHSysCmpId=C.FNHSysCmpId"
                Qry &= vbCrLf & "where E.FNHSysEmpTypeId<>1306010002 and E.FNHSysEmpTypeId<>1403080001 and E.FDDateEnd='' and E.FDDateStart<'" & _NewDate & "'"
                Qry &= vbCrLf & "group by FTCmpCode)"
                Qry &= vbCrLf & "AS B ON A.FTCmpCode=B.FTCmpCode"
                Qry &= vbCrLf & ") AS K"

                dt = HI.Conn.SQLConn.GetDataTableConectstring(Qry, _ConnStr)
                For Each K As DataRow In dt.Rows
                    With dtTemp
                        .Rows.Add(K!FTCmpCode.ToString, K!EmpType.ToString, K!Amtemp)
                        .AcceptChanges()
                    End With
                Next
            Next

            PivotGridControl2.DataSource = dtTemp.Copy

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        '_Spls.Close()
    End Sub
#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If Me.FDDate.Text <> "" Then
                Dim _Spls As New TL.SplashScreen("Loading Data .......")

                Call LoadCompany()
                Call LoadData()
                Call Loaddata2()
                _Spls.Close()
            Else
                MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text)
                Me.FDDate.Focus()
            End If
        Catch ex As Exception
            
        End Try


    End Sub

    Private Sub wHRBIWageSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Me.FNYear.Text = HI.Conn.SQLConn.GetField("SELECT  TOP 1 Year(FDDateStart)  FROM  THRMEmployee WITH(NOLOCK) Order BY Year(FDDateStart) Desc", Conn.DB.DataBaseName.DB_HR, "")
    End Sub

    'Protected Overrides Sub Finalize()
    '    MyBase.Finalize()
    'End Sub
End Class
