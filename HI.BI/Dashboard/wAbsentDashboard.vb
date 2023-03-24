Imports DevExpress.DashboardCommon
Imports System.IO
Imports DevExpress.DashboardWin
Imports DevExpress.Utils
Imports System.Windows.Forms
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports Microsoft.Win32
Imports System.Data.SqlClient
Imports DevExpress.DataAccess

Public Class wAbsentDashboard

    Private _listDashboardName As wListDashboardTemplate
    Private _StateFormLoadSuccess As Boolean = False
    Enum StateDashBoard As Integer
        MainDashboard = 0
        SubDashboard = 1
    End Enum


    Private _SysPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Dashboard"
    Private _DashBoardFilter As wAbsentDashboardFilter
    Private _MainQueryCollection As DevExpress.DataAccess.Sql.SqlQueryCollection
    Private _SubQueryCollection As DevExpress.DataAccess.Sql.SqlQueryCollection


    Private Shared Function CreateCopy(ByVal dashboard As Dashboard) As Dashboard

        If Not (dashboard Is Nothing) Then
            Using stream As Stream = New MemoryStream()
                dashboard.SaveToXml(stream)
                stream.Seek(0L, SeekOrigin.Begin)
                Dim copy As New Dashboard()
                copy.LoadFromXml(stream)
                For i As Integer = 0 To dashboard.DataSources.Count - 1
                    Dim dataSource As IDashboardDataSource = dashboard.DataSources(i)

                    If TypeOf dataSource Is DashboardObjectDataSource Then
                        copy.DataSources(i).Data = dataSource.Data
                        ' copy.DataSources(i).Data. = "FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    End If

                Next i
                Return copy
            End Using
        End If

    End Function

    Private dashboardModified_Renamed As Boolean = False
    Private dashboard_Renamed As Dashboard

    Protected Property Dashboard() As Dashboard
        Get
            Return dashboard_Renamed
        End Get
        Set(ByVal value As Dashboard)
            If value IsNot dashboard_Renamed Then
                DisposeDashboard()
                dashboard_Renamed = value
                OnDashboardChanged()
            End If
        End Set
    End Property

    Protected Overridable ReadOnly Property ShowFooterPanel() As Boolean
        Get
            Return False
        End Get
    End Property

    Protected Overridable ReadOnly Property CalculateHiddenTotals() As Boolean
        Get
            Return False
        End Get
    End Property

    Protected ReadOnly Property DashboardViewer() As DashboardViewer
        Get
            Return dashboardViewer_Renamed
        End Get
    End Property

    Protected ReadOnly Property DashboardModified() As Boolean
        Get
            Return dashboardModified_Renamed
        End Get
    End Property

    Private _MainDashboard As Dashboard
    Protected Property MainDashboard() As Dashboard
        Get
            Return _MainDashboard
        End Get
        Set(ByVal value As Dashboard)
            _MainDashboard = value
        End Set
    End Property

    Private _SubDashboard As Dashboard
    Protected Property SubDashboard() As Dashboard
        Get
            Return _SubDashboard
        End Get
        Set(ByVal value As Dashboard)
            _SubDashboard = value
        End Set
    End Property


    'Private _MainDashboardUser As Dashboard
    'Protected Property MainDashboardUser() As Dashboard
    '    Get
    '        Return _MainDashboardUser
    '    End Get
    '    Set(ByVal value As Dashboard)
    '        _MainDashboardUser = value
    '    End Set
    'End Property

    'Private _SubDashboardUser As Dashboard
    'Protected Property SubDashboardUser() As Dashboard
    '    Get
    '        Return _SubDashboardUser
    '    End Get
    '    Set(ByVal value As Dashboard)
    '        _SubDashboardUser = value
    '    End Set
    'End Property

    Private _StateShowDashboard As StateDashBoard = StateDashBoard.MainDashboard
    Protected Property StateShowDashboard() As StateDashBoard
        Get
            Return _StateShowDashboard
        End Get
        Set(ByVal value As StateDashBoard)
            _StateShowDashboard = value
        End Set
    End Property

    Public Sub New()
        InitializeComponent()
        Initialize()

        _StateFormLoadSuccess = False
        _DashBoardFilter = New wAbsentDashboardFilter

        HI.TL.HandlerControl.AddHandlerObj(_DashBoardFilter)

        Dim oSysLang As New ST.SysLanguage
        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _DashBoardFilter.Name.ToString.Trim, _DashBoardFilter)
        Catch ex As Exception
        Finally
        End Try

        Call ReadAllRegistrySubKey()

        _listDashboardName = New wListDashboardTemplate
        HI.TL.HandlerControl.AddHandlerObj(_listDashboardName)

        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _listDashboardName.Name.ToString.Trim, _listDashboardName)
        Catch ex As Exception
        Finally
        End Try

        'Dim AppDash As Dashboard = New Dashboard
        'AppDash.LoadFromXml(_SysPath & "\DashboardProductionPerfoemance.xml")


        'HI.Conn.DB.UsedDB(HI.Conn.DB.DataBaseName.DB_PROD)

        'For Each _SqlData As DashboardSqlDataSource In AppDash.DataSources
        '    Dim _StrConn As String = _SqlData.Connection.ConnectionString
        '    _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"
        'Next

        'Me.MainDashboard = AppDash


        'Dim AppDashSub As Dashboard = New Dashboard
        'AppDashSub.LoadFromXml(_SysPath & "\DashboardProductionPerfoemance_Sub.xml")


        'HI.Conn.DB.UsedDB(HI.Conn.DB.DataBaseName.DB_PROD)

        'For Each _SqlData As DashboardSqlDataSource In AppDashSub.DataSources
        '    Dim _StrConn As String = _SqlData.Connection.ConnectionString
        '    _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"
        'Next

        'Me.SubDashboard = AppDashSub

        'Dim MainDashboardUserByte() As Byte = ReadRegistry(KeyName.MainDashboard)

        'If Not (MainDashboardUserByte Is Nothing) Then

        '    Try
        '        Using stream As Stream = New MemoryStream(MainDashboardUserByte)
        '            'stream.Read(MainDashboardUserByte, 0, MainDashboardUserByte.Length)
        '            stream.Seek(0L, SeekOrigin.Begin)
        '            Dim copy As New Dashboard()
        '            copy.LoadFromXml(stream)

        '             For Each _SqlData As DashboardSqlDataSource In copy.DataSources
        '                Dim _StrConn As String = _SqlData.Connection.ConnectionString
        '                _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"
        '            Next

        '            MainDashboardUser = copy
        '        End Using
        '    Catch ex As Exception
        '        Using stream As Stream = New MemoryStream()
        '            AppDash.SaveToXml(stream)
        '            stream.Seek(0L, SeekOrigin.Begin)
        '            Dim copy As New Dashboard()
        '            copy.LoadFromXml(stream)


        '            HI.Conn.DB.UsedDB(HI.Conn.DB.DataBaseName.DB_PROD)

        '            For Each _SqlData As DashboardSqlDataSource In copy.DataSources
        '                Dim _StrConn As String = _SqlData.Connection.ConnectionString
        '                _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"
        '            Next

        '            MainDashboardUser = copy
        '        End Using
        '    End Try



        'Else
        '    Using stream As Stream = New MemoryStream()
        '        AppDash.SaveToXml(stream)
        '        stream.Seek(0L, SeekOrigin.Begin)
        '        Dim copy As New Dashboard()
        '        copy.LoadFromXml(stream)

        '        For Each _SqlData As DashboardSqlDataSource In copy.DataSources
        '            Dim _StrConn As String = _SqlData.Connection.ConnectionString
        '            _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"
        '        Next

        '        MainDashboardUser = copy
        '    End Using
        'End If


        'Dim SubDashboardUserByte() As Byte = ReadRegistry(KeyName.SubDashboard)

        'If Not (SubDashboardUserByte Is Nothing) Then
        '    Try
        '        Using stream As Stream = New MemoryStream(SubDashboardUserByte)
        '            'stream.Read(SubDashboardUserByte, 0, SubDashboardUserByte.Length)
        '            stream.Seek(0L, SeekOrigin.Begin)
        '            Dim copy As New Dashboard()
        '            copy.LoadFromXml(stream)

        '            For Each _SqlData As DashboardSqlDataSource In copy.DataSources
        '                Dim _StrConn As String = _SqlData.Connection.ConnectionString
        '                _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"
        '            Next

        '            SubDashboardUser = copy

        '        End Using

        '    Catch ex As Exception
        '        Using stream As Stream = New MemoryStream()

        '            AppDashSub.SaveToXml(stream)
        '            stream.Seek(0L, SeekOrigin.Begin)
        '            Dim copy As New Dashboard()
        '            copy.LoadFromXml(stream)

        '            For Each _SqlData As DashboardSqlDataSource In copy.DataSources
        '                Dim _StrConn As String = _SqlData.Connection.ConnectionString
        '                _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"
        '            Next

        '            SubDashboardUser = copy

        '        End Using
        '    End Try

        'Else

        '    Using stream As Stream = New MemoryStream()

        '        AppDashSub.SaveToXml(stream)
        '        stream.Seek(0L, SeekOrigin.Begin)
        '        Dim copy As New Dashboard()
        '        copy.LoadFromXml(stream)

        '        For Each _SqlData As DashboardSqlDataSource In copy.DataSources

        '            Dim _StrConn As String = _SqlData.Connection.ConnectionString
        '            _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"

        '        Next

        '        SubDashboardUser = copy

        '    End Using

        'End If

        ''Dim sqlDataSource As New DashboardSqlDataSource("Data Source 1", customstringParams)
        ''Dim query As SelectQuery = SelectQueryFluentBuilder _
        ''    .AddTable("SalesPerson") _
        ''    .SelectColumns("CategoryName", "Extended Price") _
        ''    .Build("Query 1")
        ''sqlDataSource.Queries.Add(query)
        ''sqlDataSource.Fill()
        ''Dashboard.DataSources.Add(sqlDataSource)

        Call LoadDashboardQuryData()

        ocmfilterdata.Visible = True
        ocmnew.Visible = True
        ' ocmreset.Visible = True
        ocmmaindashboard.Visible = False
        FNDashboard_lbl.Visible = True
        FNDashboard.Visible = True
        ' ocmdeletedashboardname.Visible = True
        'dashboardViewer_Renamed.Dashboard = MainDashboardUser

        StateShowDashboard = StateDashBoard.MainDashboard

    End Sub

    Protected Overridable Sub EditDashboard(Optional AddNew As Boolean = False)

        Using dashboard As Dashboard = CreateCopy(dashboardViewer_Renamed.Dashboard)

            HI.Conn.DB.UsedDB(HI.Conn.DB.DataBaseName.DB_PROD)

            For Each _SqlData As DashboardSqlDataSource In dashboard.DataSources
                Dim _StrConn As String = _SqlData.Connection.ConnectionString
                _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"
            Next

            If AddNew Then
                dashboard.Items.Clear()
            End If

            Using designerForm As New DesignerFormDashboard(dashboard, AddNew)
                'Using designerForm As New DesignerDashboard(dashboard)
                designerForm.AllowFormGlass = If(DevExpress.Skins.SkinManager.AllowFormSkins, DefaultBoolean.False, DefaultBoolean.True)

                designerForm.SaveDashboardIdx = FNDashboard.SelectedIndex
                designerForm.SaveDashboardName = FNDashboard.Text
                designerForm.DashBoadhListName = FNDashboard


                designerForm.ShowDialog()
                If designerForm.SaveDashboard Then

                    Dim _DashnoardName As String = designerForm.SaveDashboardName
                    Dim Newdashboard As Dashboard = CreateCopy(designerForm.Dashboard)

                    HI.Conn.DB.UsedDB(HI.Conn.DB.DataBaseName.DB_PROD)

                    For Each _SqlData As DashboardSqlDataSource In Newdashboard.DataSources
                        Dim _StrConn As String = _SqlData.Connection.ConnectionString
                        _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"
                    Next

                    Dim _br() As Byte = Nothing
                    Using stream As Stream = New MemoryStream()
                        Newdashboard.SaveToXml(stream)
                        'stream.Seek(0L, SeekOrigin.Begin)
                        _br = (CType(stream, MemoryStream)).ToArray()
                    End Using

                    Select Case StateShowDashboard
                        Case StateDashBoard.MainDashboard

                            WriteRegistry(_DashnoardName, _br)
                            MainDashboard = Newdashboard

                            If FNDashboard.SelectedIndex = 0 AndAlso AddNew = False Then

                                Dim _br2() As Byte = Nothing
                                Using stream As Stream = New MemoryStream()
                                    SubDashboard.SaveToXml(stream)
                                    'stream.Seek(0L, SeekOrigin.Begin)
                                    _br2 = (CType(stream, MemoryStream)).ToArray()
                                End Using

                                WriteRegistrySubDash(_DashnoardName, _br2)

                            End If

                        Case StateDashBoard.SubDashboard
                            WriteRegistrySubDash(_DashnoardName, _br)
                            SubDashboard = Newdashboard
                    End Select

                    dashboardViewer_Renamed.Dashboard = Newdashboard
                    dashboardModified_Renamed = True

                    If StateShowDashboard = StateDashBoard.MainDashboard Then
                        Dim _founditem As Boolean = False
                        For Each _item2 As DevExpress.XtraEditors.Controls.ImageComboBoxItem In FNDashboard.Properties.Items

                            If _item2.Value.ToString = _DashnoardName Then
                                _founditem = True
                                Exit For
                            End If
                        Next

                        If Not (_founditem) Then
                            Me.FNDashboard.Properties.Items.Add(_DashnoardName, _DashnoardName, 0)
                        End If

                        FNDashboard.Text = _DashnoardName
                    End If

                End If
            End Using

        End Using

    End Sub

    'Enum KeyName As Integer
    '    MainDashboard = 0
    '    SubDashboard = 1
    'End Enum

    Private Const _SubKeySelectDashboard As String = "Software\HI SOFT\DashBoardDefualt"
    Private Const _SubKeyName As String = "Software\HI SOFT\DashBoard"
    Private Const _SubKeyNameSub As String = "Software\HI SOFT\DashBoardSub"

    Private Sub ReadAllRegistrySubKey()

        Dim _Default As String = ReadRegistryDefault("Default")


        If _Default = "" Then _Default = "Default"
        FNDashboard.Properties.Items.Clear()
        FNDashboard.Properties.Items.Add("Default", "Default", 0)

        Dim regKey As RegistryKey

        regKey = Registry.CurrentUser.OpenSubKey(_SubKeyName, True)

        If Not (regKey Is Nothing) Then
            For Each subKeyName As String In regKey.GetSubKeyNames()
                FNDashboard.Properties.Items.Add(subKeyName, subKeyName, 0)
            Next

            regKey.Close()
            regKey.Dispose()
        End If


        Try
            FNDashboard.Text = _Default
        Catch ex As Exception
            FNDashboard.SelectedIndex = 0
        End Try



    End Sub

    Private Sub WriteRegistryDefault(Key As String, value As String)
        Dim regKey As RegistryKey

        regKey = Registry.CurrentUser.OpenSubKey(_SubKeySelectDashboard, True)
        If regKey Is Nothing Then
            Registry.CurrentUser.CreateSubKey(_SubKeySelectDashboard, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree)
            regKey = Registry.CurrentUser.OpenSubKey(_SubKeySelectDashboard, True)
        End If

        If value Is Nothing Then

            Try
                regKey.DeleteValue(Key.ToString(), False)
                Registry.CurrentUser.DeleteSubKey(_SubKeySelectDashboard)
            Catch ex As Exception
            End Try

        Else
            regKey.SetValue(Key.ToString(), value, RegistryValueKind.String)
        End If


        regKey.Close()

    End Sub

    Private Function ReadRegistryDefault(Key As String) As String
        Dim regKey As RegistryKey
        Dim valreturn As String = Nothing

        regKey = Registry.CurrentUser.OpenSubKey(_SubKeySelectDashboard, True)

        If regKey Is Nothing Then
            Registry.CurrentUser.CreateSubKey(_SubKeySelectDashboard, RegistryKeyPermissionCheck.ReadWriteSubTree)
            regKey = Registry.CurrentUser.OpenSubKey(_SubKeySelectDashboard, True)
        End If

        valreturn = regKey.GetValue(Key.ToString(), Nothing)
        regKey.Close()


        If valreturn Is Nothing Then valreturn = ""
        Return valreturn

    End Function

    Private Sub WriteRegistry(Key As String, value As Byte())
        Dim regKey As RegistryKey

        regKey = Registry.CurrentUser.OpenSubKey(_SubKeyName & "\" & Key, True)
        If regKey Is Nothing Then
            Registry.CurrentUser.CreateSubKey(_SubKeyName & "\" & Key, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree)
            regKey = Registry.CurrentUser.OpenSubKey(_SubKeyName & "\" & Key, True)
        End If

        If value Is Nothing Then
            Try
                regKey.DeleteValue(Key.ToString(), False)
                Registry.CurrentUser.DeleteSubKey(_SubKeyName & "\" & Key)
            Catch ex As Exception
            End Try

        Else
            regKey.SetValue(Key.ToString(), value, RegistryValueKind.Binary)
        End If


        regKey.Close()

    End Sub

    Private Function ReadRegistry(Key As String) As Byte()
        Dim regKey As RegistryKey
        Dim valreturn() As Byte = Nothing

        regKey = Registry.CurrentUser.OpenSubKey(_SubKeyName & "\" & Key, True)

        If regKey Is Nothing Then
            Registry.CurrentUser.CreateSubKey(_SubKeyName & "\" & Key, RegistryKeyPermissionCheck.ReadWriteSubTree)
            regKey = Registry.CurrentUser.OpenSubKey(_SubKeyName & "\" & Key, True)
        End If

        valreturn = regKey.GetValue(Key.ToString(), Nothing)
        regKey.Close()

        Return valreturn

    End Function


    Private Sub WriteRegistrySubDash(Key As String, value As Byte())
        Dim regKey As RegistryKey

        regKey = Registry.CurrentUser.OpenSubKey(_SubKeyNameSub & "\" & Key, True)
        If regKey Is Nothing Then
            Registry.CurrentUser.CreateSubKey(_SubKeyNameSub & "\" & Key, Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree)
            regKey = Registry.CurrentUser.OpenSubKey(_SubKeyNameSub & "\" & Key, True)
        End If

        If value Is Nothing Then
            Try
                regKey.DeleteValue(Key.ToString(), False)
                Registry.CurrentUser.DeleteSubKey(_SubKeyNameSub & "\" & Key)
            Catch ex As Exception

            End Try

        Else
            regKey.SetValue(Key.ToString(), value, RegistryValueKind.Binary)
        End If


        regKey.Close()

    End Sub

    Private Function ReadRegistrySubDash(Key As String) As Byte()
        Dim regKey As RegistryKey
        Dim valreturn() As Byte = Nothing

        regKey = Registry.CurrentUser.OpenSubKey(_SubKeyNameSub & "\" & Key, True)

        If regKey Is Nothing Then
            Registry.CurrentUser.CreateSubKey(_SubKeyNameSub & "\" & Key, RegistryKeyPermissionCheck.ReadWriteSubTree)
            regKey = Registry.CurrentUser.OpenSubKey(_SubKeyNameSub & "\" & Key, True)
        End If

        valreturn = regKey.GetValue(Key.ToString(), Nothing)
        regKey.Close()

        Return valreturn

    End Function


    Protected Overridable Sub SubscribeDashboardEvents()
    End Sub

    Private Sub OnDashboardChanged()
        dashboardViewer_Renamed.Dashboard = dashboard_Renamed
    End Sub

    Private Sub Initialize()
        panelFooter.Visible = ShowFooterPanel
        dashboardViewer_Renamed.CalculateHiddenTotals = CalculateHiddenTotals
        SubscribeDashboardEvents()
        LoadDashboard()
    End Sub

    Private Sub LoadDashboard()
        'Dim asm As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
        'Dim dashboardType As Type = asm.GetType(String.Format("DashboardMainDemo.Dashboards.{0}", Me.GetType().Name))
        'If dashboardType IsNot Nothing Then
        '    Dashboard = CType(Activator.CreateInstance(dashboardType), Dashboard)
        'Else
        '    Dashboard = Nothing
        'End If
    End Sub

    Private Sub DisposeDashboard()
        If dashboard_Renamed IsNot Nothing Then
            dashboard_Renamed.Dispose()
        End If
    End Sub


    Private Sub ocmnew_Click(sender As Object, e As EventArgs) Handles ocmnew.Click
        EditDashboard(True)
    End Sub

    Private Sub btnEditDashboard_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEditDashboard.Click
        EditDashboard(False)
    End Sub

    Private Sub wAbsentDashboard_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Sub wAbsentDashboard_Load(sender As Object, e As EventArgs) Handles Me.Load

        '  dashboardViewer_Renamed.Dashboard = HumanResources
        _StateFormLoadSuccess = True

        If ocmdeletedashboardname.Enabled Then ocmdeletedashboardname.Visible = True
        If ocmloaddashboard.Enabled Then ocmloaddashboard.Visible = True
        If ocmuploadloaddashboard.Enabled Then ocmuploadloaddashboard.Visible = True

        Call FNDashboard_SelectedIndexChanged(FNDashboard, New System.EventArgs)

        If ocmnew.Enabled = False And btnEditDashboard.Enabled = False Then
            Call Loaddatadashboard(1, 1, 0, False)
        End If

        otmcheck.Enabled = True
        Me.Refresh()

    End Sub

    Private Sub Loaddatadashboard(PerformanceCharttype As Integer, DefectAndDowntimetype As Integer, DefectType As Integer, Optional StateFormLoad As Boolean = False, Optional dataselect As DataTable = Nothing)
        Dim _Cmd As String = ""
        Dim _spls As New HI.TL.SplashScreen("Loading...data Please wait.")
        Dim _PerformanceCharttype As Integer
        Dim _DefectAndDowntimetype As Integer

        Select Case PerformanceCharttype
            Case 0
                _PerformanceCharttype = 1
            Case 1
                _PerformanceCharttype = 0
            Case 2
                _PerformanceCharttype = 2
            Case 3
                _PerformanceCharttype = 3
            Case Else
                _PerformanceCharttype = 1
        End Select

        Select Case DefectAndDowntimetype
            Case 0
                _DefectAndDowntimetype = 1
            Case 1
                _DefectAndDowntimetype = 0
            Case 2
                _DefectAndDowntimetype = 2
            Case 3
                _DefectAndDowntimetype = 3
            Case Else
                _DefectAndDowntimetype = 1
        End Select

        '_Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DASHBOARD_PROD_PERFORMANCE "
        '' 
        'HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)

        '_Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DASHBOARD_PROD_BUSINESS_UNIT "
        ''  
        'HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)

        '_Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DASHBOARD_PROD_PERFORMANCE_BY_LINE " & _PerformanceCharttype & ""
        '' 
        'HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)

        '_Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DASHBOARD_PROD_DEFECT_DOWNTIME " & _DefectAndDowntimetype & ""
        '' 
        'HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)

        '_Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DASHBOARD_PROD_TOP3_DEFECT " & DefectType & ""
        ''
        'HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)

        '_Cmd = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DASHBOARD_PROD_TOP3_DEFECT_SUB " & DefectType & ""
        '' 
        'HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)

        Dim _dtcmp As DataTable
        Dim _Str, _Qry As String
        Dim _ServerName, _UID, _PWS, _DBName As String
        Dim _ConnectString As String = ""
        Dim _FNHSysCmpId As Integer = 0
        Dim dtdashboardName As New DataTable
        Dim _StateFixLoad As Boolean = False
        dtdashboardName.Columns.Add("Name", GetType(String))

        If ocmnew.Enabled = False And btnEditDashboard.Enabled = False Then
            _StateFixLoad = True

            For i As Integer = 0 To FNDashboard.Properties.Items.Count - 1
                Dim AppDash As Dashboard = New Dashboard
                Dim AppDashSub As Dashboard = New Dashboard

                If i = 0 Then
                    AppDash.LoadFromXml(_SysPath & "\DashboardProductionPerfoemance.xml")
                    AppDashSub.LoadFromXml(_SysPath & "\DashboardProductionPerfoemance_Sub.xml")

                Else

                    Dim DashboardName As String = FNDashboard.Properties.Items(i).Value.ToString()

                    Dim MainDashboardUserByte() As Byte = ReadRegistry(DashboardName)

                    If Not (MainDashboardUserByte Is Nothing) Then
                        Using stream As Stream = New MemoryStream(MainDashboardUserByte)
                            'stream.Read(MainDashboardUserByte, 0, MainDashboardUserByte.Length)
                            stream.Seek(0L, SeekOrigin.Begin)
                            Dim copy As New Dashboard()
                            copy.LoadFromXml(stream)


                            AppDash = copy
                        End Using
                    Else
                        AppDash = Nothing
                    End If



                    Dim SubDashboardUserByte() As Byte = ReadRegistrySubDash(DashboardName)

                    If Not (SubDashboardUserByte Is Nothing) Then
                        Using stream As Stream = New MemoryStream(SubDashboardUserByte)
                            'stream.Read(SubDashboardUserByte, 0, SubDashboardUserByte.Length)
                            stream.Seek(0L, SeekOrigin.Begin)
                            Dim copy As New Dashboard()
                            copy.LoadFromXml(stream)


                            AppDashSub = copy

                        End Using
                    Else
                        AppDashSub = Nothing
                    End If
                End If



                If Not (AppDash Is Nothing) Then
                    For Each Coll As Object In AppDash.Items
                        Dim MemberName As String = ""

                        Try
                            MemberName = Coll.DataMember
                            If MemberName <> "" Then
                                If dtdashboardName.Select("Name='" & HI.UL.ULF.rpQuoted(MemberName) & "'").Length <= 0 Then
                                    dtdashboardName.Rows.Add(MemberName)
                                End If
                            End If

                        Catch ex As Exception
                        End Try

                    Next
                End If

                If Not (AppDashSub Is Nothing) Then
                    For Each Coll As Object In AppDashSub.Items
                        Dim MemberName As String = ""

                        Try
                            MemberName = Coll.DataMember
                            If MemberName <> "" Then
                                If dtdashboardName.Select("Name='" & HI.UL.ULF.rpQuoted(MemberName) & "'").Length <= 0 Then
                                    dtdashboardName.Rows.Add(MemberName)
                                End If
                            End If

                        Catch ex As Exception
                        End Try

                    Next
                End If

                AppDash.Dispose()
                AppDashSub.Dispose()
            Next

        End If

        Dim dtquery As DataTable
        Dim cmd As String = "SELECT *,'0' AS FTLoadData FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardQuery AS X WITH(NOLOCK) WHERE FTStateActive='1' "
        dtquery = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_PROD)

        For Each R As DataRow In dtquery.Rows

            _Qry = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo." & R!FTTableName.ToString & " WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

        Next

        If _StateFixLoad Then

            For Each R As DataRow In dtquery.Rows

                If dtdashboardName.Select("Name='" & HI.UL.ULF.rpQuoted(R!FTTableName.ToString) & "'").Length > 0 Then
                    R!FTLoadData = "1"
                End If

            Next

        Else
            For Each R As DataRow In dtquery.Rows
                R!FTLoadData = "1"
            Next
        End If

        If dataselect Is Nothing Then
            _Str = " SELECT   '1' AS FTSelect "
            _Str &= vbCrLf & ",M.FNHSysCmpId,CASE WHEN M.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " THEN 1 ELSE 2 END AS FNSeq"
            _Str &= vbCrLf & ",M.FTCmpCode,ISNULL(IPP.FTIPServer,'') AS FTIPServer,0 AS FNCompensationFoundByYearOption_Hide"
            _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS M WITH(NOLOCK) "
            _Str &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSECompanyIPServer AS IPP WITH(NOLOCK) ON M.FNHSysCmpId = IPP.FNHSysCmpId "
            _Str &= vbCrLf & " WHERE ISNULL(M.FTStateActive,'') ='1' AND ISNULL(IPP.FTIPServer,'') <>''  AND ISNULL(FTStateDashboard,'')='1' "

            If StateFormLoad Then
                _Str &= vbCrLf & " AND M.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ""
            End If
            _Str &= vbCrLf & " ORDER BY M.FTCmpCode"

            _dtcmp = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)
        Else
            _dtcmp = dataselect
        End If

        For Each R As DataRow In _dtcmp.Select("FTSelect='1' ", "FNSeq")
            _FNHSysCmpId = Val(R!FNHSysCmpId.ToString)
            If HI.Conn.DB.UsedDB(Conn.DB.DataBaseName.DB_PROD) Then
                _ServerName = R!FTIPServer.ToString
                _UID = HI.Conn.DB.UIDName
                _PWS = HI.Conn.DB.PWDName
                _DBName = HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD)
                _ConnectString = "SERVER=" & _ServerName & ";UID=" & _UID & ";PWD=" & _PWS & ";Initial Catalog=" & _DBName
                _spls.UpdateInformation("Loading.... Data Company " & R!FTCmpCode.ToString & "   Please wait....")
                Try
                    Dim _dt As New DataTable

                    Try
                        If dtquery.Select("FTTableName='TPROBIDashboardAbsentDetail_ALL_Branch' AND FTLoadData='1'").Length > 0 Or dtquery.Select("FTTableName='TPROBIDashboardAbsentDetail' AND FTLoadData='1'").Length > 0 Then
                            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DASHBOARD_PROD_ABSENTEEISM_DETAIL_PULL "
                            _dt = HI.Conn.SQLConn.GetDataTableConectstring(_Qry, _ConnectString)

                            If dtquery.Select("FTTableName='TPROBIDashboardAbsentDetail_ALL_Branch' AND FTLoadData='1'").Length > 0 Then
                                For Each R2 As DataRow In _dt.Rows
                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardAbsentDetail_ALL_Branch("
                                    _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FTDate, FNAllPlan, FNAllActual, FNAllPer, FNCutPlan"
                                    _Qry &= vbCrLf & ", FNCutActual, FNAbsentCutPer, FNHeatPlan, FNHeatActual, FNAbsentHeatPer, FNLaserPlan"
                                    _Qry &= vbCrLf & ", FNLaserActual, FNAbsentLaserPer, FNEmbroidPlan, "
                                    _Qry &= vbCrLf & " FNEmbroidActual, FNAbsentEmbroidPer, FNSewPlan, FNSewActual"
                                    _Qry &= vbCrLf & " , FNAbsentESewPer, FNQCPlan, FNQCActual, FNAbsentQCPer, FNMechanicPlan, FNMechanicActual, FNAbsentMechanicPer)"
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"

                                    _Qry &= vbCrLf & ",'" & R2!FTDateTrans.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNAllPlan.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNAllActual.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNAbsentAllPer.ToString & "'"

                                    _Qry &= vbCrLf & ",'" & R2!FNCutPlan.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNCutActual.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNAbsentCutPer.ToString & "'"

                                    _Qry &= vbCrLf & ",'" & R2!FNHeatPlan.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNHeatActual.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNAbsentHeatPer.ToString & "'"

                                    _Qry &= vbCrLf & ",'" & R2!FNLaserPlan.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNLaserActual.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNAbsentLaserPer.ToString & "'"

                                    _Qry &= vbCrLf & ",'" & R2!FNEmbroidPlan.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNEmbroidActual.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNAbsentEmbroidPer.ToString & "'"

                                    _Qry &= vbCrLf & ",'" & R2!FNSewPlan.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNSewActual.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNAbsentESewPer.ToString & "'"

                                    _Qry &= vbCrLf & ",'" & R2!FNQCPlan.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNQCActual.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNAbsentQCPer.ToString & "'"

                                    _Qry &= vbCrLf & ",'" & R2!FNMechanicPlan.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNMechanicActual.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNAbsentMechanicPer.ToString & "'"

                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                Next

                            End If

                            If Val(R!FNSeq) = 1 Then

                                If dtquery.Select("FTTableName='TPROBIDashboardAbsentDetail' AND FTLoadData='1'").Length > 0 Then

                                    For Each R2 As DataRow In _dt.Rows
                                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardAbsentDetail("
                                        _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FTDate, FNAllPlan, FNAllActual, FNAllPer, FNCutPlan"
                                        _Qry &= vbCrLf & ", FNCutActual, FNAbsentCutPer, FNHeatPlan, FNHeatActual, FNAbsentHeatPer, FNLaserPlan"
                                        _Qry &= vbCrLf & ", FNLaserActual, FNAbsentLaserPer, FNEmbroidPlan, "
                                        _Qry &= vbCrLf & " FNEmbroidActual, FNAbsentEmbroidPer, FNSewPlan, FNSewActual"
                                        _Qry &= vbCrLf & " , FNAbsentESewPer, FNQCPlan, FNQCActual, FNAbsentQCPer, FNMechanicPlan, FNMechanicActual, FNAbsentMechanicPer)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"

                                        _Qry &= vbCrLf & ",'" & R2!FTDateTrans.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNAllPlan.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNAllActual.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNAbsentAllPer.ToString & "'"

                                        _Qry &= vbCrLf & ",'" & R2!FNCutPlan.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNCutActual.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNAbsentCutPer.ToString & "'"

                                        _Qry &= vbCrLf & ",'" & R2!FNHeatPlan.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNHeatActual.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNAbsentHeatPer.ToString & "'"

                                        _Qry &= vbCrLf & ",'" & R2!FNLaserPlan.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNLaserActual.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNAbsentLaserPer.ToString & "'"

                                        _Qry &= vbCrLf & ",'" & R2!FNEmbroidPlan.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNEmbroidActual.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNAbsentEmbroidPer.ToString & "'"

                                        _Qry &= vbCrLf & ",'" & R2!FNSewPlan.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNSewActual.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNAbsentESewPer.ToString & "'"

                                        _Qry &= vbCrLf & ",'" & R2!FNQCPlan.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNQCActual.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNAbsentQCPer.ToString & "'"

                                        _Qry &= vbCrLf & ",'" & R2!FNMechanicPlan.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNMechanicActual.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNAbsentMechanicPer.ToString & "'"

                                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                    Next

                                End If

                            End If

                        End If

                    Catch ex As Exception
                    End Try

                    Try
                        If dtquery.Select("FTTableName='TPROBIDashboardBusinessUnit_ALL_Branch' AND FTLoadData='1'").Length > 0 Or dtquery.Select("FTTableName='TPROBIDashboardBusinessUnit' AND FTLoadData='1'").Length > 0 Then
                            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DASHBOARD_PROD_BUSINESS_UNIT_PULL "
                            _dt = HI.Conn.SQLConn.GetDataTableConectstring(_Qry, _ConnectString)

                            If dtquery.Select("FTTableName='TPROBIDashboardBusinessUnit_ALL_Branch' AND FTLoadData='1'").Length > 0 Then

                                For Each R2 As DataRow In _dt.Rows

                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardBusinessUnit_ALL_Branch("
                                    _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FNSeq, FTUnitSect, FTState, FNQuantity, FTDataType)"
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNSeq.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTData.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTState.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNQuantity.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTData2.ToString & "'"

                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                Next

                            End If

                            If Val(R!FNSeq) = 1 Then

                                If dtquery.Select("FTTableName='TPROBIDashboardBusinessUnit' AND FTLoadData='1'").Length > 0 Then

                                    For Each R2 As DataRow In _dt.Rows

                                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardBusinessUnit("
                                        _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FNSeq, FTUnitSect, FTState, FNQuantity, FTDataType)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNSeq.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTData.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTState.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNQuantity.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTData2.ToString & "'"

                                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                    Next

                                End If

                            End If

                        End If

                    Catch ex As Exception
                    End Try


                    Try

                        If dtquery.Select("FTTableName='TPROBIDashboardPerformanceDefectDeatil_ALL_Branch' AND FTLoadData='1'").Length > 0 Or dtquery.Select("FTTableName='TPROBIDashboardPerformanceDefectDeatil' AND FTLoadData='1'").Length > 0 Then

                            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DASHBOARD_PROD_DEFECT_DETAIL_PULL "
                            _dt = HI.Conn.SQLConn.GetDataTableConectstring(_Qry, _ConnectString)

                            If dtquery.Select("FTTableName='TPROBIDashboardPerformanceDefectDeatil_ALL_Branch' AND FTLoadData='1'").Length > 0 Then
                                For Each R2 As DataRow In _dt.Rows

                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardPerformanceDefectDeatil_ALL_Branch("
                                    _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FDQADate,FNSeq, FTQADetailName, FNQtyDefect, FNTotalQtyDefect, FNDefectPer)"
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FDQADate.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNSeq.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTQADetailName.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!QtyDefect.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!TotalQtyDefect.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!DefectPer.ToString & "'"

                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                Next
                            End If

                            If Val(R!FNSeq) = 1 Then
                                If dtquery.Select("FTTableName='TPROBIDashboardPerformanceDefectDeatil' AND FTLoadData='1'").Length > 0 Then

                                    For Each R2 As DataRow In _dt.Rows

                                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardPerformanceDefectDeatil("
                                        _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FDQADate,FNSeq, FTQADetailName, FNQtyDefect, FNTotalQtyDefect, FNDefectPer)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FDQADate.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNSeq.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTQADetailName.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!QtyDefect.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!TotalQtyDefect.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!DefectPer.ToString & "'"

                                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                    Next

                                End If

                            End If

                        End If



                    Catch ex As Exception
                    End Try


                    Try
                        If dtquery.Select("FTTableName='TPROBIDashboardDefectAndDowntime_ALL_Branch' AND FTLoadData='1'").Length > 0 Or dtquery.Select("FTTableName='TPROBIDashboardDefectAndDowntime' AND FTLoadData='1'").Length > 0 Then
                            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DASHBOARD_PROD_DEFECT_DOWNTIME_PULL "
                            _dt = HI.Conn.SQLConn.GetDataTableConectstring(_Qry, _ConnectString)

                            If dtquery.Select("FTTableName='TPROBIDashboardDefectAndDowntime_ALL_Branch' AND FTLoadData='1'").Length > 0 Then
                                For Each R2 As DataRow In _dt.Rows

                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardDefectAndDowntime_ALL_Branch("
                                    _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FNType, FTUnitSect, FTTypeCode, FNQty)"
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNType.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTUnitSectCode.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNTypeCode.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNQty.ToString & "'"


                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                Next
                            End If

                            If Val(R!FNSeq) = 1 Then

                                If dtquery.Select("FTTableName='TPROBIDashboardDefectAndDowntime' AND FTLoadData='1'").Length > 0 Then

                                    For Each R2 As DataRow In _dt.Rows

                                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardDefectAndDowntime("
                                        _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FNType, FTUnitSect, FTTypeCode, FNQty)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNType.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTUnitSectCode.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNTypeCode.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNQty.ToString & "'"


                                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                    Next

                                End If

                            End If
                        End If

                    Catch ex As Exception
                    End Try

                    Try
                        If dtquery.Select("FTTableName='TPROBIDashboardDetailByDateAndLine_ALL_Branch' AND FTLoadData='1'").Length > 0 Or dtquery.Select("FTTableName='TPROBIDashboardDetailByDateAndLine' AND FTLoadData='1'").Length > 0 Then
                            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DASHBOARD_PROD_PERFORMANCE_BY_LINE_DETAIL_PULL "
                            _dt = HI.Conn.SQLConn.GetDataTableConectstring(_Qry, _ConnectString)

                            If dtquery.Select("FTTableName='TPROBIDashboardDetailByDateAndLine_ALL_Branch' AND FTLoadData='1'").Length > 0 Then
                                For Each R2 As DataRow In _dt.Rows

                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardDetailByDateAndLine_ALL_Branch("
                                    _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FTDate, FTLineNo, FTBU, FNSam, FNOutput"
                                    _Qry &= vbCrLf & ", FNTotalActualQC, FNTotalQCDefect, FNPlanEmp, FNPlanTime, FNActualEmp"
                                    _Qry &= vbCrLf & ", FNActualTime, FNDownTimeEmp, FNDownTimeMachine, FNTotalDownTime,FNSumOutPutMultiSamActual)"
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTFromDate.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTLineNo.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTBU.ToString & "'"

                                    _Qry &= vbCrLf & ",'" & R2!FNSam.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNScanQuantity.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNTotalActualQC.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNTotalQCDefect.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNPlanEmp.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNPlanTime.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNTotalActualEmp.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNTime.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNDownTimeEmp.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNDownTimeMachine.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNTotalDownTime.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNSumOutPutMultiSamActual.ToString & "'"


                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                Next
                            End If

                            If Val(R!FNSeq) = 1 Then
                                If dtquery.Select("FTTableName='TPROBIDashboardDetailByDateAndLine' AND FTLoadData='1'").Length > 0 Then

                                    For Each R2 As DataRow In _dt.Rows

                                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardDetailByDateAndLine("
                                        _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FTDate, FTLineNo, FTBU, FNSam, FNOutput"
                                        _Qry &= vbCrLf & ", FNTotalActualQC, FNTotalQCDefect, FNPlanEmp, FNPlanTime, FNActualEmp"
                                        _Qry &= vbCrLf & ", FNActualTime, FNDownTimeEmp, FNDownTimeMachine, FNTotalDownTime,FNSumOutPutMultiSamActual)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTFromDate.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTLineNo.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTBU.ToString & "'"

                                        _Qry &= vbCrLf & ",'" & R2!FNSam.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNScanQuantity.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNTotalActualQC.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNTotalQCDefect.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNPlanEmp.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNPlanTime.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNTotalActualEmp.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNTime.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNDownTimeEmp.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNDownTimeMachine.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNTotalDownTime.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNSumOutPutMultiSamActual.ToString & "'"


                                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                    Next

                                End If
                            End If
                        End If

                    Catch ex As Exception
                    End Try

                    Try
                        If dtquery.Select("FTTableName='TPROBIDashboardDetailByDateAndLineByStyle_ALL_Branch' AND FTLoadData='1'").Length > 0 Or dtquery.Select("FTTableName='TPROBIDashboardDetailByDateAndLineByStyle' AND FTLoadData='1'").Length > 0 Then
                            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DASHBOARD_PROD_PERFORMANCE_BY_LINE_DETAIL_STYLE_PULL "
                            _dt = HI.Conn.SQLConn.GetDataTableConectstring(_Qry, _ConnectString)


                            If dtquery.Select("FTTableName='TPROBIDashboardDetailByDateAndLineByStyle_ALL_Branch' AND FTLoadData='1'").Length > 0 Then
                                For Each R2 As DataRow In _dt.Rows

                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardDetailByDateAndLineByStyle_ALL_Branch("
                                    _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FTDate, FTLineNo, FTBU, FNSam, FNOutput"
                                    _Qry &= vbCrLf & ", FNTotalActualQC, FNTotalQCDefect, FNPlanEmp, FNPlanTime, FNActualEmp"
                                    _Qry &= vbCrLf & ", FNActualTime, FNDownTimeEmp, FNDownTimeMachine, FNTotalDownTime,FTStyleNo)"
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTFromDate.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTLineNo.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTBU.ToString & "'"

                                    _Qry &= vbCrLf & ",'" & R2!FNSam.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNScanQuantity.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNTotalActualQC.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNTotalQCDefect.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNPlanEmp.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNPlanTime.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNTotalActualEmp.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNTime.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNDownTimeEmp.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNDownTimeMachine.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNTotalDownTime.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTStyleNo.ToString & "'"


                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                Next
                            End If

                            If Val(R!FNSeq) = 1 Then

                                If dtquery.Select("FTTableName='TPROBIDashboardDetailByDateAndLineByStyle' AND FTLoadData='1'").Length > 0 Then

                                    For Each R2 As DataRow In _dt.Rows

                                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardDetailByDateAndLineByStyle("
                                        _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FTDate, FTLineNo, FTBU, FNSam, FNOutput"
                                        _Qry &= vbCrLf & ", FNTotalActualQC, FNTotalQCDefect, FNPlanEmp, FNPlanTime, FNActualEmp"
                                        _Qry &= vbCrLf & ", FNActualTime, FNDownTimeEmp, FNDownTimeMachine, FNTotalDownTime,FTStyleNo)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTFromDate.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTLineNo.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTBU.ToString & "'"

                                        _Qry &= vbCrLf & ",'" & R2!FNSam.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNScanQuantity.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNTotalActualQC.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNTotalQCDefect.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNPlanEmp.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNPlanTime.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNTotalActualEmp.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNTime.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNDownTimeEmp.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNDownTimeMachine.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNTotalDownTime.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTStyleNo.ToString & "'"


                                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                    Next

                                End If
                            End If
                        End If

                    Catch ex As Exception
                    End Try

                    Try

                        If dtquery.Select("FTTableName='TPROBIDashboardPerformanceByLine_ALL_Branch' AND FTLoadData='1'").Length > 0 Or dtquery.Select("FTTableName='TPROBIDashboardPerformanceByLine' AND FTLoadData='1'").Length > 0 Then
                            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DASHBOARD_PROD_PERFORMANCE_BY_LINE_PULL "
                            _dt = HI.Conn.SQLConn.GetDataTableConectstring(_Qry, _ConnectString)

                            If dtquery.Select("FTTableName='TPROBIDashboardPerformanceByLine_ALL_Branch' AND FTLoadData='1'").Length > 0 Then
                                For Each R2 As DataRow In _dt.Rows

                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardPerformanceByLine_ALL_Branch("
                                    _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FNType, FTUnitSectCode, FNSeq, FNOutput, FNProdPer, FNEffPer,FNTime)"
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNType.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTUnitSectCode.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNSeq.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNOutput.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNProdPer.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNEffPer.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNTime.ToString & "'"

                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                Next

                            End If

                            If Val(R!FNSeq) = 1 Then
                                If dtquery.Select("FTTableName='TPROBIDashboardPerformanceByLine' AND FTLoadData='1'").Length > 0 Then

                                    For Each R2 As DataRow In _dt.Rows

                                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardPerformanceByLine("
                                        _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FNType, FTUnitSectCode, FNSeq, FNOutput, FNProdPer, FNEffPer,FNTime)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNType.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTUnitSectCode.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNSeq.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNOutput.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNProdPer.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNEffPer.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNTime.ToString & "'"

                                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                    Next

                                End If

                            End If

                        End If




                    Catch ex As Exception
                    End Try

                    Try
                        If dtquery.Select("FTTableName='TPROBIDashboardPerformance_ALL_Branch' AND FTLoadData='1'").Length > 0 Or dtquery.Select("FTTableName='TPROBIDashboardPerformance' AND FTLoadData='1'").Length > 0 Then
                            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DASHBOARD_PROD_PERFORMANCE_PULL "
                            _dt = HI.Conn.SQLConn.GetDataTableConectstring(_Qry, _ConnectString)

                            If dtquery.Select("FTTableName='TPROBIDashboardPerformance_ALL_Branch' AND FTLoadData='1'").Length > 0 Then
                                For Each R2 As DataRow In _dt.Rows

                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardPerformance_ALL_Branch("
                                    _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FTDataShow, FNSeq, FTYesterday, FTToDay, FTAVG7Day, FTMonth,FTYesterday01,FTYesterday02,FTYesterday03,FTYesterday04,FTYesterday05)"
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTDataShow.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNSeq.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTYesterday.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTToDay.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTAVG7Day.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTMonth.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTYesterday01.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTYesterday02.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTYesterday03.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTYesterday04.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTYesterday05.ToString & "'"

                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                Next
                            End If

                            If Val(R!FNSeq) = 1 Then

                                If dtquery.Select("FTTableName='TPROBIDashboardPerformance' AND FTLoadData='1'").Length > 0 Then
                                    For Each R2 As DataRow In _dt.Rows

                                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardPerformance("
                                        _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FTDataShow, FNSeq, FTYesterday, FTToDay, FTAVG7Day, FTMonth,FTYesterday01,FTYesterday02,FTYesterday03,FTYesterday04,FTYesterday05)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTDataShow.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNSeq.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTYesterday.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTToDay.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTAVG7Day.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTMonth.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTYesterday01.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTYesterday02.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTYesterday03.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTYesterday04.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTYesterday05.ToString & "'"

                                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                    Next


                                End If

                            End If

                        End If

                    Catch ex As Exception
                    End Try

                    Try
                        If dtquery.Select("FTTableName='TPROBIDashboardPerformanceTopDefect_ALL_Branch' AND FTLoadData='1'").Length > 0 Or dtquery.Select("FTTableName='TPROBIDashboardPerformanceTopDefect' AND FTLoadData='1'").Length > 0 Then
                            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DASHBOARD_PROD_TOP3_DEFECT_PULL "
                            _dt = HI.Conn.SQLConn.GetDataTableConectstring(_Qry, _ConnectString)

                            If dtquery.Select("FTTableName='TPROBIDashboardPerformanceTopDefect_ALL_Branch' AND FTLoadData='1'").Length > 0 Then
                                For Each R2 As DataRow In _dt.Rows

                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardPerformanceTopDefect_ALL_Branch("
                                    _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FNSeq, FTQADetailName, FNQtyDefect, FNTotalQtyDefect, FNDefectPer)"
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNSeq.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTQADetailName.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!QtyDefect.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!TotalQtyDefect.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!DefectPer.ToString & "'"


                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                Next
                            End If

                            If Val(R!FNSeq) = 1 Then
                                If dtquery.Select("FTTableName='TPROBIDashboardPerformanceTopDefect' AND FTLoadData='1'").Length > 0 Then

                                    For Each R2 As DataRow In _dt.Rows

                                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardPerformanceTopDefect("
                                        _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FNSeq, FTQADetailName, FNQtyDefect, FNTotalQtyDefect, FNDefectPer)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNSeq.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTQADetailName.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!QtyDefect.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!TotalQtyDefect.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!DefectPer.ToString & "'"


                                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                    Next

                                End If


                            End If
                        End If



                    Catch ex As Exception
                    End Try

                    Try
                        If dtquery.Select("FTTableName='TPROBIDashboardPerformanceTopDefectSub_ALL_Branch' AND FTLoadData='1'").Length > 0 Or dtquery.Select("FTTableName='TPROBIDashboardPerformanceTopDefectSub' AND FTLoadData='1'").Length > 0 Then
                            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DASHBOARD_PROD_TOP3_DEFECT_SUB_PULL "
                            _dt = HI.Conn.SQLConn.GetDataTableConectstring(_Qry, _ConnectString)

                            If dtquery.Select("FTTableName='TPROBIDashboardPerformanceTopDefectSub_ALL_Branch' AND FTLoadData='1'").Length > 0 Then
                                For Each R2 As DataRow In _dt.Rows

                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardPerformanceTopDefectSub_ALL_Branch("
                                    _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FNHSysUnitSectId, FTUnitSectCode, FNSeq, FTQADetailName, FNQtyDefect, FNTotalQtyDefect, FNDefectPer)"
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNHSysUnitSectId.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTUnitSectCode.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNSeq.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FTQADetailName.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!Quantity.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!TotalQuantity.ToString & "'"
                                    _Qry &= vbCrLf & ",'" & R2!DefectPer.ToString & "'"

                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                Next
                            End If



                            If Val(R!FNSeq) = 1 Then
                                If dtquery.Select("FTTableName='TPROBIDashboardPerformanceTopDefectSub' AND FTLoadData='1'").Length > 0 Then
                                    For Each R2 As DataRow In _dt.Rows

                                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardPerformanceTopDefectSub("
                                        _Qry &= vbCrLf & " FTUserLogIn,FTCmp,FNHSysUnitSectId, FTUnitSectCode, FNSeq, FTQADetailName, FNQtyDefect, FNTotalQtyDefect, FNDefectPer)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNHSysUnitSectId.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTUnitSectCode.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNSeq.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FTQADetailName.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!Quantity.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!TotalQuantity.ToString & "'"
                                        _Qry &= vbCrLf & ",'" & R2!DefectPer.ToString & "'"

                                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                    Next
                                End If


                            End If
                        End If


                    Catch ex As Exception
                    End Try


                    Try
                        If dtquery.Select("FTTableName='TPROBIDashboardWHStock_ALL_Branch' AND FTLoadData='1'").Length > 0 Or dtquery.Select("FTTableName='TPROBIDashboardWHStock' AND FTLoadData='1'").Length > 0 Then
                            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DASHBOARD_WH_FABRIC_DETAIL_PULL " & Val(R!FNHSysCmpId.ToString) & ""
                            _dt = HI.Conn.SQLConn.GetDataTableConectstring(_Qry, _ConnectString)

                            If dtquery.Select("FTTableName='TPROBIDashboardWHStock_ALL_Branch' AND FTLoadData='1'").Length > 0 Then
                                For Each R2 As DataRow In _dt.Rows

                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardWHStock_ALL_Branch("
                                    _Qry &= vbCrLf & " FTUserLogIn, FTCmp, FNSeq, FNAge, FNQuantity, FNPer, FNFabricMaximum)"
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                    _Qry &= vbCrLf & ",'" & Val(R2!FNType.ToString) & "'"
                                    _Qry &= vbCrLf & ",'" & R2!FNAge.ToString & "'"
                                    _Qry &= vbCrLf & "," & Val(R2!FNQuantity.ToString) & ""
                                    _Qry &= vbCrLf & "," & Val(R2!FNPer.ToString) & ""
                                    _Qry &= vbCrLf & "," & Val(R2!FNFabricMaximum.ToString) & ""


                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                Next
                            End If



                            If Val(R!FNSeq) = 1 Then
                                If dtquery.Select("FTTableName='TPROBIDashboardWHStock' AND FTLoadData='1'").Length > 0 Then
                                    For Each R2 As DataRow In _dt.Rows

                                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardWHStock("
                                        _Qry &= vbCrLf & " FTUserLogIn, FTCmp, FNSeq, FNAge, FNQuantity, FNPer, FNFabricMaximum)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                        _Qry &= vbCrLf & ",'" & Val(R2!FNType.ToString) & "'"
                                        _Qry &= vbCrLf & ",'" & R2!FNAge.ToString & "'"
                                        _Qry &= vbCrLf & "," & Val(R2!FNQuantity.ToString) & ""
                                        _Qry &= vbCrLf & "," & Val(R2!FNPer.ToString) & ""
                                        _Qry &= vbCrLf & "," & Val(R2!FNFabricMaximum.ToString) & ""

                                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                    Next
                                End If

                            End If
                        End If

                    Catch ex As Exception
                    End Try

                    Try
                        If dtquery.Select("FTTableName='TPROBIDashboardWHFG_ALL_Branch' AND FTLoadData='1'").Length > 0 Or dtquery.Select("FTTableName='TPROBIDashboardWHFG' AND FTLoadData='1'").Length > 0 Then
                            _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.SP_GET_DASHBOARD_WH_FINISHGOOD_PULL " & Val(R!FNHSysCmpId.ToString) & ""
                            _dt = HI.Conn.SQLConn.GetDataTableConectstring(_Qry, _ConnectString)

                            If dtquery.Select("FTTableName='TPROBIDashboardWHFG_ALL_Branch' AND FTLoadData='1'").Length > 0 Then
                                For Each R2 As DataRow In _dt.Rows

                                    _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardWHFG_ALL_Branch("
                                    _Qry &= vbCrLf & "  FTUserLogIn, FTCmp, FNQuantity, FNPer, FNCartonMaximum)"
                                    _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                    _Qry &= vbCrLf & ",'" & Val(R2!FNQuantity.ToString) & "'"
                                    _Qry &= vbCrLf & ",'" & Val(R2!FNPer.ToString) & "'"
                                    _Qry &= vbCrLf & "," & Val(R2!FNCartonMaximum.ToString) & ""


                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                Next
                            End If



                            If Val(R!FNSeq) = 1 Then
                                If dtquery.Select("FTTableName='TPROBIDashboardWHFG' AND FTLoadData='1'").Length > 0 Then
                                    For Each R2 As DataRow In _dt.Rows

                                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardWHFG("
                                        _Qry &= vbCrLf & "  FTUserLogIn, FTCmp, FNQuantity, FNPer, FNCartonMaximum)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                                        _Qry &= vbCrLf & ",'" & Val(R2!FNQuantity.ToString) & "'"
                                        _Qry &= vbCrLf & ",'" & Val(R2!FNPer.ToString) & "'"
                                        _Qry &= vbCrLf & "," & Val(R2!FNCartonMaximum.ToString) & ""

                                        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                    Next
                                End If


                            End If
                        End If

                    Catch ex As Exception
                    End Try
                    _dt.Dispose()
                Catch ex22 As Exception
                    ' System.Windows.Forms.MessageBox.Show(ex22.Message())
                End Try
            End If
        Next

        Me.dashboardViewer_Renamed.ReloadData()
        Me.dashboardViewer_Renamed.Refresh()

        _spls.Close()

    End Sub

    'Private Sub dashboardViewer_Renamed_ConfigureDataConnection(sender As Object, e As DashboardConfigureDataConnectionEventArgs) Handles dashboardViewer_Renamed.ConfigureDataConnection
    '    Dim pcp As MsSqlConnectionParameters = TryCast(e.ConnectionParameters, MsSqlConnectionParameters)

    '    If Not (pcp Is Nothing) Then
    '        HI.Conn.DB.UsedDB(HI.Conn.DB.DataBaseName.DB_PROD)
    '        pcp.ServerName = HI.Conn.DB.SerVerName
    '        pcp.Password = HI.Conn.DB.PWDName
    '        pcp.UserName = HI.Conn.DB.UIDName
    '        pcp.AuthorizationType = MsSqlAuthorizationType.SqlServer
    '    End If

    'End Sub

    Private Sub dashboardViewer_Renamed_DashboardItemClick(sender As Object, e As DashboardItemMouseActionEventArgs) Handles dashboardViewer_Renamed.DashboardItemClick
        Select Case e.DashboardItemName.ToUpper
            Case "GDefectIssue".ToUpper
                With CType(sender, DashboardViewer)

                    Dim _name As String = .Dashboard.Items(e.DashboardItemName).GetType.FullName()
                    Select Case _name
                        Case "DevExpress.DashboardCommon.GridDashboardItem"

                            If Not (Me.SubDashboard Is Nothing) Then
                                dashboardViewer_Renamed.Dashboard = Me.SubDashboard
                                StateShowDashboard = StateDashBoard.SubDashboard
                                ocmfilterdata.Visible = False
                                ocmnew.Visible = False
                                FNDashboard_lbl.Visible = False
                                FNDashboard.Visible = False
                                ocmmaindashboard.Visible = True
                                ocmdeletedashboardname.Visible = False
                                ocmloaddashboard.Visible = False
                                ocmuploadloaddashboard.Visible = False

                            End If
                        Case Else

                            Try
                                Dim _grid As DevExpress.DashboardCommon.GridDashboardItem = .Dashboard.Items(e.DashboardItemName)

                                If _grid.DataMember.ToUpper = "TPROBIDashboardPerformanceTopDefect".ToUpper Then
                                    If Not (Me.SubDashboard Is Nothing) Then
                                        dashboardViewer_Renamed.Dashboard = Me.SubDashboard
                                        StateShowDashboard = StateDashBoard.SubDashboard
                                        ocmfilterdata.Visible = False
                                        ocmnew.Visible = False
                                        FNDashboard_lbl.Visible = False
                                        FNDashboard.Visible = False
                                        ocmmaindashboard.Visible = True
                                        ocmdeletedashboardname.Visible = False
                                        ocmloaddashboard.Visible = False
                                        ocmuploadloaddashboard.Visible = False

                                    End If
                                End If

                            Catch ex As Exception

                            End Try



                    End Select
                End With
            Case Else
                Try
                    Dim _grid As DevExpress.DashboardCommon.GridDashboardItem = CType(sender, DashboardViewer).Dashboard.Items(e.DashboardItemName)

                    If _grid.DataMember.ToUpper = "TPROBIDashboardPerformanceTopDefect".ToUpper Then
                        If Not (Me.SubDashboard Is Nothing) Then
                            dashboardViewer_Renamed.Dashboard = Me.SubDashboard
                            StateShowDashboard = StateDashBoard.SubDashboard
                            ocmfilterdata.Visible = False
                            ocmnew.Visible = False
                            FNDashboard_lbl.Visible = False
                            FNDashboard.Visible = False
                            ocmmaindashboard.Visible = True
                            ocmdeletedashboardname.Visible = False
                            ocmloaddashboard.Visible = False
                            ocmuploadloaddashboard.Visible = False

                        End If
                    End If

                Catch ex As Exception

                End Try
        End Select
    End Sub

    Private Sub ocmmain_Click(sender As Object, e As EventArgs) Handles ocmfilterdata.Click

        'With _DashBoardFilter
        '    .StateFilter = False
        '    .ShowDialog()

        '    If (.StateFilter) Then

        ' Call Loaddatadashboard(.FNProductionPerformanceChart.SelectedIndex, .FNDefectAndDowntimeChart.SelectedIndex, .FNDefectIssue.SelectedIndex)
        Call Loaddatadashboard(1, 1, 0, False)
        '    End If
        'End With

    End Sub

    Private Sub ocmmaindashboard_Click(sender As Object, e As EventArgs) Handles ocmmaindashboard.Click

        dashboardViewer_Renamed.Dashboard = Me.MainDashboard
        StateShowDashboard = StateDashBoard.MainDashboard

        ocmfilterdata.Visible = True
        ocmnew.Visible = True
        ocmmaindashboard.Visible = False
        FNDashboard_lbl.Visible = True
        FNDashboard.Visible = True
        ocmdeletedashboardname.Visible = True
        ocmloaddashboard.Visible = True
        ocmuploadloaddashboard.Visible = True

    End Sub

    Private Sub ocmreset_Click(sender As Object, e As EventArgs)

        'Using stream As Stream = New MemoryStream()

        '    Me.MainDashboard.SaveToXml(stream)
        '    stream.Seek(0L, SeekOrigin.Begin)
        '    Dim copy As New Dashboard()
        '    copy.LoadFromXml(stream)

        '    For Each _SqlData As DashboardSqlDataSource In copy.DataSources
        '        Dim _StrConn As String = _SqlData.Connection.ConnectionString
        '        _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"
        '    Next

        '    Me.MainDashboardUser = copy

        'End Using

        'Using stream As Stream = New MemoryStream()

        '    Me.SubDashboard.SaveToXml(stream)
        '    stream.Seek(0L, SeekOrigin.Begin)
        '    Dim copy As New Dashboard()
        '    copy.LoadFromXml(stream)

        '    For Each _SqlData As DashboardSqlDataSource In copy.DataSources
        '        Dim _StrConn As String = _SqlData.Connection.ConnectionString
        '        _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"
        '    Next

        '    Me.SubDashboardUser = copy

        'End Using

        'WriteRegistry(KeyName.MainDashboard, Nothing)
        'WriteRegistry(KeyName.SubDashboard, Nothing)

        'Me.dashboardViewer_Renamed.Dashboard = Me.MainDashboardUser

    End Sub

    Private Sub LoadDashboardQuryData()

        Dim dtquery As DataTable
        Dim cmd As String = "SELECT * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardQuery AS X WITH(NOLOCK) WHERE FTStateActive='1'"
        dtquery = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_PROD)


        Dim AppDash As Dashboard = New Dashboard
        AppDash.LoadFromXml(_SysPath & "\DashboardProductionPerfoemance.xml")

        HI.Conn.DB.UsedDB(HI.Conn.DB.DataBaseName.DB_PROD)

        If dtquery.Select("FNTypeOfSeq=0").Length > 0 Then

            For Each _SqlData As DashboardSqlDataSource In AppDash.DataSources

                _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"
                _SqlData.Queries.Clear()

                For Each R As DataRow In dtquery.Select("FNTypeOfSeq=0", "FNSeq")

                    Dim Query As New DevExpress.DataAccess.Sql.CustomSqlQuery
                    Query.Name = R!FTName.ToString
                    Query.Parameters.Add(New QueryParameter("FTUserLogIn", GetType(Expression), New Expression("'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'")))
                    Query.Sql = "SELECT * FROM " & R!FTTableName.ToString & " WHERE FTUserLogIn=@FTUserLogIn "

                    _SqlData.Queries.Add(Query)
                  
                Next

                _MainQueryCollection = _SqlData.Queries

            Next

        Else
            For Each _SqlData As DashboardSqlDataSource In AppDash.DataSources

                _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"
               
                _MainQueryCollection = _SqlData.Queries

            Next
        End If

        AppDash.Dispose()

        Dim AppDashSub As Dashboard = New Dashboard
        AppDashSub.LoadFromXml(_SysPath & "\DashboardProductionPerfoemance_Sub.xml")


        HI.Conn.DB.UsedDB(HI.Conn.DB.DataBaseName.DB_PROD)

        If dtquery.Select("FNTypeOfSeq=1").Length > 0 Then
            For Each _SqlData As DashboardSqlDataSource In AppDashSub.DataSources

                _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"
                _SqlData.Queries.Clear()

                For Each R As DataRow In dtquery.Select("FNTypeOfSeq=1", "FNSeq")

                    Dim Query As New DevExpress.DataAccess.Sql.CustomSqlQuery
                    Query.Name = R!FTName.ToString
                    Query.Parameters.Add(New QueryParameter("FTUserLogIn", GetType(Expression), New Expression("'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'")))
                    Query.Sql = "SELECT * FROM " & R!FTTableName.ToString & " WHERE FTUserLogIn=@FTUserLogIn "

                    _SqlData.Queries.Add(Query)

                Next

                _SubQueryCollection = _SqlData.Queries
            Next
        Else
            For Each _SqlData As DashboardSqlDataSource In AppDashSub.DataSources

                _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"

                _SubQueryCollection = _SqlData.Queries
            Next
        End If


        AppDashSub.Dispose()

    End Sub


    Private Sub LoadDashboardDefult()
        Dim AppDash As Dashboard = New Dashboard
        AppDash.LoadFromXml(_SysPath & "\DashboardProductionPerfoemance.xml")

        HI.Conn.DB.UsedDB(HI.Conn.DB.DataBaseName.DB_PROD)

        For Each _SqlData As DashboardSqlDataSource In AppDash.DataSources

            _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"
            _SqlData.Queries.Clear()

            For Each _Query As DevExpress.DataAccess.Sql.SqlQuery In _MainQueryCollection
                _SqlData.Queries.Add(_Query)
            Next

        Next

        Me.MainDashboard = AppDash


        Dim AppDashSub As Dashboard = New Dashboard
        AppDashSub.LoadFromXml(_SysPath & "\DashboardProductionPerfoemance_Sub.xml")


        HI.Conn.DB.UsedDB(HI.Conn.DB.DataBaseName.DB_PROD)

        For Each _SqlData As DashboardSqlDataSource In AppDashSub.DataSources

            _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"
            _SqlData.Queries.Clear()

            For Each _Query As DevExpress.DataAccess.Sql.SqlQuery In _SubQueryCollection
                _SqlData.Queries.Add(_Query)
            Next

        Next

        Me.SubDashboard = AppDashSub

        Me.dashboardViewer_Renamed.Dashboard = Me.MainDashboard

    End Sub


    Private Sub LoadDashboardFromRegisrty(DashboardName As String)

        Try
            Dim AppDash As Dashboard = New Dashboard
            Dim AppDashSub As Dashboard = New Dashboard
            HI.Conn.DB.UsedDB(HI.Conn.DB.DataBaseName.DB_PROD)

            Dim MainDashboardUserByte() As Byte = ReadRegistry(DashboardName)

            If Not (MainDashboardUserByte Is Nothing) Then
                Using stream As Stream = New MemoryStream(MainDashboardUserByte)
                    'stream.Read(MainDashboardUserByte, 0, MainDashboardUserByte.Length)
                    stream.Seek(0L, SeekOrigin.Begin)
                    Dim copy As New Dashboard()
                    copy.LoadFromXml(stream)

                    For Each _SqlData As DashboardSqlDataSource In copy.DataSources

                        _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"

                        _SqlData.Queries.Clear()

                        For Each _Query As DevExpress.DataAccess.Sql.SqlQuery In _MainQueryCollection
                            _SqlData.Queries.Add(_Query)

                        Next

                    Next

                    MainDashboard = copy
                End Using
            Else
                MainDashboard = Nothing
            End If



            Dim SubDashboardUserByte() As Byte = ReadRegistrySubDash(DashboardName)

            If Not (SubDashboardUserByte Is Nothing) Then
                Using stream As Stream = New MemoryStream(SubDashboardUserByte)
                    'stream.Read(SubDashboardUserByte, 0, SubDashboardUserByte.Length)
                    stream.Seek(0L, SeekOrigin.Begin)
                    Dim copy As New Dashboard()
                    copy.LoadFromXml(stream)

                    For Each _SqlData As DashboardSqlDataSource In copy.DataSources

                        _SqlData.Connection.ConnectionString = "XpoProvider=MSSqlServer; Data Source=" & HI.Conn.DB.SerVerName & "; Initial Catalog=" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "; User Id=" & HI.Conn.DB.UIDName & "; Password=" & HI.Conn.DB.PWDName & ";"

                        _SqlData.Queries.Clear()

                        For Each _Query As DevExpress.DataAccess.Sql.SqlQuery In _SubQueryCollection
                            _SqlData.Queries.Add(_Query)
                        Next

                    Next

                    SubDashboard = copy

                End Using
            Else
                SubDashboard = Nothing
            End If


            Me.dashboardViewer_Renamed.Dashboard = Me.MainDashboard
        Catch ex As Exception
        End Try

    End Sub

    Private Sub FNDashboard_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNDashboard.SelectedIndexChanged
        If (_StateFormLoadSuccess) Then

            Dim _spls As New HI.TL.SplashScreen("Loading... Dashboard  Please wait.")

            Select Case FNDashboard.SelectedIndex
                Case -1, 0
                    Call LoadDashboardDefult()
                Case Else
                    Call LoadDashboardFromRegisrty(FNDashboard.Text)
            End Select


            WriteRegistryDefault("Default", FNDashboard.Text)

            _spls.Close()

        End If
    End Sub

    Private Sub ocmdeletedashboardname_Click(sender As Object, e As EventArgs) Handles ocmdeletedashboardname.Click
        If Me.FNDashboard.Text.Trim <> "" And Me.FNDashboard.Text.Trim.ToUpper <> "Default".ToUpper And Me.FNDashboard.SelectedIndex <> 0 Then

            If HI.MG.ShowMsg.mConfirmProcessDefaultNo("คุณต้องการทำารลบรูปแแบ Dashboard ใช่หรือไม่ ?", 1611054784, Me.FNDashboard.Text.Trim) = True Then

                WriteRegistry(Me.FNDashboard.Text.Trim, Nothing)
                WriteRegistrySubDash(Me.FNDashboard.Text.Trim, Nothing)

                Me.FNDashboard.Properties.Items.Remove(Me.FNDashboard.SelectedItem)
                Me.FNDashboard.SelectedIndex = 0

            End If

        End If

    End Sub

    Private Sub ocmuploadloaddashboard_Click(sender As Object, e As EventArgs) Handles ocmuploadloaddashboard.Click
        If Me.FNDashboard.Properties.Items.Count > 1 Then
            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Upload Dashboard Templete ไปยัง Server ใช่หรือไม่ (ระวัง กรณีที่ชื่อซ้ำระบบจะทำการเขียนทับ) ?", 1612120596) = False Then Exit Sub
            Dim dt As New DataTable
            dt.Columns.Add("FTSelect", GetType(String))
            dt.Columns.Add("FTDashBoardName", GetType(String))
            Dim dashboardname As String = ""

            For I As Integer = 1 To FNDashboard.Properties.Items.Count - 1
                dashboardname = FNDashboard.Properties.Items(I).Value.ToString

                dt.Rows.Add("0", dashboardname)
            Next


            With _listDashboardName
                .processok = False
                .ocmcancel.Enabled = True
                .ocmdownloaddashboard.Enabled = False
                .ocmdownloaddashboard.Visible = False
                .ocmuploaddashboard.Enabled = True
                .ocmuploaddashboard.Visible = True
                .ogc.DataSource = dt
                .ogc.RefreshDataSource()
                .ShowDialog()

                If .processok Then

                    Dim _spls As New HI.TL.SplashScreen("Uploading... Pleas wait.")
                    Try
                        CType(.ogc.DataSource, DataTable).AcceptChanges()
                        dt = CType(.ogc.DataSource, DataTable)
                        Dim _Cmd As String = ""

                        For Each R As DataRow In dt.Select("FTSelect='1'")

                            Dim MainDashboardUserByte() As Byte = ReadRegistry(R!FTDashBoardName.ToString)

                            If Not (MainDashboardUserByte Is Nothing) Then


                                _Cmd = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardTemplate"
                                _Cmd &= vbCrLf & " SET FTDashBoardName='" & HI.UL.ULF.rpQuoted(R!FTDashBoardName.ToString) & "'"
                                _Cmd &= vbCrLf & " WHERE  FTDashBoardName='" & HI.UL.ULF.rpQuoted(R!FTDashBoardName.ToString) & "'"

                                If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD) = False Then

                                    _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardTemplate(FTDashBoardName)"
                                    _Cmd &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(R!FTDashBoardName.ToString) & "'"
                                    HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                                End If

                                _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardTemplate"
                                _Cmd &= " Set  FBDashBoard=@FBDashBoard"
                                _Cmd &= "  Where FTDashBoardName=@FTDashBoardName"


                                Dim _Cnn = New System.Data.SqlClient.SqlConnection()
                                Try
                                    Dim _ConnString As String = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
                                    If _Cnn.State = ConnectionState.Open Then
                                        _Cnn.Close()
                                    End If
                                    _Cnn.ConnectionString = _ConnString
                                    _Cnn.Open()

                                    Dim cmd As New SqlCommand(_Cmd, _Cnn)
                                    Dim p1 As New SqlParameter("@FBDashBoard", SqlDbType.VarBinary)
                                    p1.Value = MainDashboardUserByte

                                    Dim p2 As New SqlParameter("@FTDashBoardName", SqlDbType.NVarChar)
                                    p2.Value = R!FTDashBoardName.ToString
                                    cmd.Parameters.Add(p1)
                                    cmd.Parameters.Add(p2)

                                    cmd.ExecuteNonQuery()

                                    If _Cnn.State = ConnectionState.Open Then
                                        _Cnn.Close()
                                    End If

                                Catch ex As Exception
                                    If _Cnn.State = ConnectionState.Open Then
                                        _Cnn.Close()
                                    End If
                                End Try
                                _Cnn.Dispose()

                                Dim SubDashboardUserByte() As Byte = ReadRegistrySubDash(R!FTDashBoardName.ToString)
                                If Not (SubDashboardUserByte Is Nothing) Then
                                    _Cmd = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardTemplateSub"
                                    _Cmd &= vbCrLf & " SET FTDashBoardName='" & HI.UL.ULF.rpQuoted(R!FTDashBoardName.ToString) & "'"
                                    _Cmd &= vbCrLf & " WHERE  FTDashBoardName='" & HI.UL.ULF.rpQuoted(R!FTDashBoardName.ToString) & "'"

                                    If HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD) = False Then

                                        _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardTemplateSub(FTDashBoardName)"
                                        _Cmd &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(R!FTDashBoardName.ToString) & "'"
                                        HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)

                                    End If


                                    _Cmd = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardTemplateSub"
                                    _Cmd &= " Set  FBDashBoard=@FBDashBoard"
                                    _Cmd &= "  Where FTDashBoardName=@FTDashBoardName"


                                    Dim _Cnn2 = New System.Data.SqlClient.SqlConnection()
                                    Try
                                        Dim _ConnString As String = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
                                        If _Cnn2.State = ConnectionState.Open Then
                                            _Cnn2.Close()
                                        End If
                                        _Cnn2.ConnectionString = _ConnString
                                        _Cnn2.Open()

                                        Dim cmd2 As New SqlCommand(_Cmd, _Cnn2)
                                        Dim p1 As New SqlParameter("@FBDashBoard", SqlDbType.VarBinary)
                                        p1.Value = SubDashboardUserByte

                                        Dim p2 As New SqlParameter("@FTDashBoardName", SqlDbType.NVarChar)
                                        p2.Value = R!FTDashBoardName.ToString
                                        cmd2.Parameters.Add(p1)
                                        cmd2.Parameters.Add(p2)

                                        cmd2.ExecuteNonQuery()

                                        If _Cnn.State = ConnectionState.Open Then
                                            _Cnn.Close()
                                        End If

                                    Catch ex As Exception
                                        If _Cnn2.State = ConnectionState.Open Then
                                            _Cnn2.Close()
                                        End If
                                    End Try
                                    _Cnn2.Dispose()

                                End If

                            End If

                        Next
                        _spls.Close()
                        HI.MG.ShowMsg.mInfo("Upload Complete !!!", 1612120551, Me.Text, , MessageBoxIcon.Information)
                    Catch ex As Exception
                        _spls.Close()
                    End Try



                End If
            End With

        Else
            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Template บนเครื่อง Computer ที่สามารถทำการ Upload ได้ !!!", 1612120547, Me.Text, , MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub ocmloaddashboard_Click(sender As Object, e As EventArgs) Handles ocmloaddashboard.Click

        Dim cmd As String = ""
        Dim dt As DataTable

        cmd = "SELECT '0' AS FTSelect,FTDashBoardName FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardTemplate AS X WITH(NOLOCK) ORDER BY FTDashBoardName"
        dt = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_PROD)


        If dt.Rows.Count > 0 Then
            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Download Dashboard Templete จาก Server ใช่หรือไม่ (ระวัง กรณีที่ชื่อซ้ำระบบจะทำการเขียนทับ) ?", 1612120597) = False Then Exit Sub
            With _listDashboardName
                .processok = False
                .ocmcancel.Enabled = True
                .ocmdownloaddashboard.Enabled = True
                .ocmdownloaddashboard.Visible = True
                .ocmuploaddashboard.Enabled = False
                .ocmuploaddashboard.Visible = False
                .ogc.DataSource = dt
                .ogc.RefreshDataSource()
                .ShowDialog()

                If .processok Then
                    Dim _spls As New HI.TL.SplashScreen("Uploading... Pleas wait.")

                    Try
                        Dim dtdata As DataTable
                        Dim dtdatasub As DataTable
                        cmd = "SELECT '0' AS FTSelect,FTDashBoardName,FBDashBoard FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardTemplate AS X WITH(NOLOCK) ORDER BY FTDashBoardName"
                        dtdata = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_PROD)

                        cmd = "SELECT '0' AS FTSelect,FTDashBoardName,FBDashBoard FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROBIDashboardTemplateSub AS X WITH(NOLOCK) ORDER BY FTDashBoardName"
                        dtdatasub = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_PROD)

                        CType(.ogc.DataSource, DataTable).AcceptChanges()
                        dt = CType(.ogc.DataSource, DataTable)
                        Dim _Cmd As String = ""
                        Dim _DashnoardName As String = ""

                        For Each R As DataRow In dt.Select("FTSelect='1'")
                            _DashnoardName = R!FTDashBoardName.ToString
                            For Each R2 As DataRow In dtdata.Select("FTDashBoardName='" & HI.UL.ULF.rpQuoted(_DashnoardName) & "'")
                                Try
                                    WriteRegistry(_DashnoardName, R2!FBDashBoard)
                                Catch ex As Exception
                                End Try
                            Next

                            For Each R2 As DataRow In dtdatasub.Select("FTDashBoardName='" & HI.UL.ULF.rpQuoted(_DashnoardName) & "'")
                                Try
                                    WriteRegistrySubDash(_DashnoardName, R2!FBDashBoard)
                                Catch ex As Exception
                                End Try
                            Next


                            Dim _founditem As Boolean = False
                            For Each _item2 As DevExpress.XtraEditors.Controls.ImageComboBoxItem In FNDashboard.Properties.Items

                                If _item2.Value.ToString = _DashnoardName Then
                                    _founditem = True
                                    Exit For
                                End If
                            Next

                            If Not (_founditem) Then
                                Me.FNDashboard.Properties.Items.Add(_DashnoardName, _DashnoardName, 0)
                            End If

                        Next
                        dtdatasub.Dispose()
                        dtdata.Dispose()
                        _spls.Close()
                        HI.MG.ShowMsg.mInfo("Download Complete !!!", 1612120552, Me.Text, , MessageBoxIcon.Information)
                    Catch ex As Exception
                        _spls.Close()
                    End Try
                End If
            End With
        Else
            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Template บนเครื่อง Server ที่สามารถทำการ Download ได้ !!!", 1612120548, Me.Text, , MessageBoxIcon.Warning)
        End If

    End Sub
End Class