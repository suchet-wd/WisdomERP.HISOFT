Public Class wCompensationFundOfYear



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

    Private Sub LoadCompany()
        Dim _Str As String

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

        Me.ogccmp.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

    End Sub

    Private Function LoadDataInfo(Spls As HI.TL.SplashScreen) As Boolean
        Try
            Dim _Qry As String = ""

            Spls.UpdateInformation("Loading.... Data Company   Please wait....")

            Dim _FTYear As String = Me.FTDateStart.Text

            Dim _dtcmp As DataTable
            With CType(Me.ogccmp.DataSource, DataTable)
                .AcceptChanges()
                _dtcmp = .Copy
            End With

            Try
                _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.TRPTTempCompensationFund  WHERE FTUserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            Catch ex As Exception
                ' System.Windows.Forms.MessageBox.Show(ex.Message())
            End Try

            Dim _ServerName, _UID, _PWS, _DBName As String
            Dim _ConnectString As String = ""
            Dim _FNHSysCmpId As Integer = 0

            For Each R As DataRow In _dtcmp.Select("FTSelect='1'")

                _FNHSysCmpId = Val(R!FNHSysCmpId.ToString)

                If HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_HR) Then
                    _ServerName = R!FTIPServer.ToString
                    _UID = HI.Conn.DB.UIDName
                    _PWS = HI.Conn.DB.PWDName
                    _DBName = HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR)

                    _ConnectString = "SERVER=" & _ServerName & ";UID=" & _UID & ";PWD=" & _PWS & ";Initial Catalog=" & _DBName

                    Spls.UpdateInformation("Loading.... Data Company " & R!FTCmpCode.ToString & "   Please wait....")
                    Try
                        Dim _dt As New DataTable
                        _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GET_CAL_CompensationFund '" & _FTYear & "','" & Val(R!FNCompensationFoundByYearOption_Hide.ToString) & "'"
                        _dt = HI.Conn.SQLConn.GetDataTableConectstring(_Qry, _ConnectString)

                        If _dt.Rows.Count > 0 Then

                            For I As Integer = 1 To 12

                                If _dt.Select("FNMonth=" & I & "").Length > 0 Then

                                    For Each Rmx As DataRow In _dt.Select("FNMonth=" & I & "")

                                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.TRPTTempCompensationFund"
                                        _Qry &= vbCrLf & "(FTUserLogin, FNHSysCmpId, FNMonth, FNTotalEmp, FNEmpDMinSalary, FNEmpMMinSalary, FNEmpDTotalSalary"
                                        _Qry &= vbCrLf & ", FNEmpMTotalSalary, FNEmpOverTotalSalary, FNEmpOTTotalSalary, FNEmpBonusTotalSalary)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & _FNHSysCmpId & "," & I & ""
                                        _Qry &= vbCrLf & "," & Val(Rmx!FNTotalEmp.ToString) & ""
                                        _Qry &= vbCrLf & "," & Val(Rmx!FNDMinSalary.ToString) & ""
                                        _Qry &= vbCrLf & "," & Val(Rmx!FNMMinSalary.ToString) & ""
                                        _Qry &= vbCrLf & "," & Val(Rmx!FNDSumSalary.ToString) & ""
                                        _Qry &= vbCrLf & "," & Val(Rmx!FNMSumSalary.ToString) & ""
                                        _Qry &= vbCrLf & "," & Val(Rmx!FNSalaryOver.ToString) & ""
                                        _Qry &= vbCrLf & "," & Val(Rmx!FNDOTSalary.ToString) + Val(Rmx!FNMOTSalary.ToString) & ""
                                        _Qry &= vbCrLf & "," & Val(Rmx!FNTotalIncomeBonus.ToString) & ""

                                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)
                                        Exit For

                                    Next

                                Else

                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.TRPTTempCompensationFund"
                                    _Qry &= vbCrLf & "(FTUserLogin, FNHSysCmpId, FNMonth, FNTotalEmp, FNEmpDMinSalary, FNEmpMMinSalary, FNEmpDTotalSalary"
                                    _Qry &= vbCrLf & ", FNEmpMTotalSalary, FNEmpOverTotalSalary, FNEmpOTTotalSalary, FNEmpBonusTotalSalary)"
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & _FNHSysCmpId & "," & I & ""
                                    _Qry &= vbCrLf & ",0,0,0,0,0,0,0,0"

                                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)
                                End If

                            Next

                        End If

                        _dt.Dispose()
                    Catch ex22 As Exception
                        ' System.Windows.Forms.MessageBox.Show(ex22.Message())
                    End Try
                   
                End If

            Next

            _Qry = "   Select X.FNMonthSeq"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "  ,X.FTNameTH AS FTMonthName "
            Else
                _Qry &= vbCrLf & "  ,X.FTNameEN	 AS FTMonthName "
            End If

            _Qry &= vbCrLf & "  , SUM(T.FNTotalEmp) AS FNTotalEmp"
            _Qry &= vbCrLf & "  , SUM(T.FNEmpDMinSalary) AS FNEmpDMinSalary"
            _Qry &= vbCrLf & "  , SUM(T.FNEmpMMinSalary) AS FNEmpMMinSalary"
            _Qry &= vbCrLf & "  , SUM(T.FNEmpDTotalSalary) AS FNEmpDTotalSalary"
            _Qry &= vbCrLf & "  , SUM(T.FNEmpMTotalSalary) AS FNEmpMTotalSalary"
            _Qry &= vbCrLf & "  , SUM(T.FNEmpDTotalSalary +T.FNEmpMTotalSalary) AS FNTotalSalary"

            _Qry &= vbCrLf & "  , SUM(T.FNEmpOverTotalSalary) AS FNEmpOverTotalSalary"
            _Qry &= vbCrLf & "  , SUM((T.FNEmpDTotalSalary +T.FNEmpMTotalSalary)-T.FNEmpOverTotalSalary) AS FNTotalPaySalary"

            _Qry &= vbCrLf & "  , SUM(T.FNEmpOTTotalSalary) AS FNEmpOTTotalSalary"
            _Qry &= vbCrLf & "  , SUM(T.FNEmpBonusTotalSalary)  AS FNEmpBonusTotalSalary"

            _Qry &= vbCrLf & "FROM("
            _Qry &= vbCrLf & " SELECT FNListIndex AS FNMonthSeq, FTNameTH, FTNameEN"
            _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS X WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE  (FTListName = N'FNMonth')"
            _Qry &= vbCrLf & ") AS X INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.TRPTTempCompensationFund AS T WITH(NOLOCK) ON X.FNMonthSeq =T.FNMonth "
            _Qry &= vbCrLf & " WHERE T.FTUserLogin ='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & " GROUP BY X.FNMonthSeq"

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "  ,X.FTNameTH  "
            Else
                _Qry &= vbCrLf & "  ,X.FTNameEN	  "
            End If

            _Qry &= vbCrLf & "  ORDER BY X.FNMonthSeq"

            Dim _dtdata As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            With Me.ogc
                .DataSource = _dtdata.Copy
                ogv.ExpandAllGroups()
                ogv.RefreshData()
            End With

            _dtdata.Dispose()

        Catch ex As Exception
        End Try
       
        Return True
    End Function
#End Region

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        With ogv

            .Columns("FNEmpDTotalSalary").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNEmpDTotalSalary")
            .Columns("FNEmpDTotalSalary").SummaryItem.DisplayFormat = "{0:n2}"
           
            .Columns("FNEmpMTotalSalary").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNEmpMTotalSalary")
            .Columns("FNEmpMTotalSalary").SummaryItem.DisplayFormat = "{0:n2}"

            .Columns("FNTotalSalary").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNTotalSalary")
            .Columns("FNTotalSalary").SummaryItem.DisplayFormat = "{0:n2}"

            .Columns("FNEmpOverTotalSalary").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNEmpOverTotalSalary")
            .Columns("FNEmpOverTotalSalary").SummaryItem.DisplayFormat = "{0:n2}"

            .Columns("FNTotalPaySalary").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNTotalPaySalary")
            .Columns("FNTotalPaySalary").SummaryItem.DisplayFormat = "{0:n2}"

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            .OptionsView.ShowGroupPanel = False
            .ExpandAllGroups()

            .RefreshData()

        End With
        '------End Add Summary Grid-------------
    End Sub
#End Region

#Region "General"
    Private Sub wEmployeeLeaveOfYear_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Call LoadCompany()
        Call InitGrid()

        ogv.OptionsView.ShowAutoFilterRow = False
        HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)

    End Sub

    Private Function VerifyData() As Boolean
        If Me.FTDateStart.Text <> "" Then


            Return True

        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการระบุปีที่ต้องการสรุปข้อมูล !!!", 1512130574, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        End If
    End Function

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click


        HI.TL.HandlerControl.ClearControl(Me)
        'HI.TL.HandlerControl.ClearControl(ogc)
        ogc.DataSource = Nothing
    End Sub

    Private Sub ocmload_Click(sender As System.Object, e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then


            Dim _dtcmp As DataTable
            With CType(Me.ogccmp.DataSource, DataTable)
                .AcceptChanges()
                _dtcmp = .Copy
            End With

            If _dtcmp.Select("FTSelect='1'").Length > 0 Then
                _dtcmp.Dispose()

                Dim _Spls As New HI.TL.SplashScreen("Loading data...   Please Wait  ")

                Try
                    Me.LoadDataInfo(_Spls)
                    Me.otbmain.SelectedTabPageIndex = 0
                Catch ex As Exception
                    ' System.Windows.Forms.MessageBox.Show(ex.Message())
                End Try

                _Spls.Close()

            Else
                _dtcmp.Dispose()

                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Company !!!", 15120508456, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)

            End If

        End If

    End Sub

   
#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogv)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub


    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        If VerifyData() Then

            If Not (Me.ogccmp.DataSource) Is Nothing Then
                If Me.ogv.RowCount > 0 Then

                    Dim _Qry As String = ""

                    _Qry = " {TRPTTempCompensationFund.FTUserLogin} = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
               
                    With New HI.RP.Report
                        .FormTitle = Me.Text
                        .ReportFolderName = "Human Report\"
                        .ReportName = "CompensationFund.rpt"
                        .AddParameter("FTDateStart", FTDateStart.Text)
                        .Formular = _Qry
                        .Preview()
                    End With

                Else
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการแสดงรายงานกรุณาทำการตรวจสอบ !!!", 1512216423, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If
            Else
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการแสดงรายงานกรุณาทำการตรวจสอบ !!!", 1512216423, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End If

    End Sub
End Class