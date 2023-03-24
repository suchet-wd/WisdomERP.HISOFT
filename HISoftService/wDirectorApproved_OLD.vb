Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports HI.TL.RunID

Imports System.Data.SqlClient
Imports System.Data
Imports System.Windows.Forms

Imports System.IO

Public Class wDirectorApproved_OLD

    Private DT As DataTable
    Friend Shared _CountApp As Integer = 0

    '  Private Shared _frmApp As New wDirectorApproved ' = Nothing

    Friend Shared DTPurchaseNo As DataTable
    Friend Property Data_DTPurchaseNo As DataTable
        Get
            Return DTPurchaseNo
        End Get
        Set(ByVal value As DataTable)
            DTPurchaseNo = value
        End Set
    End Property


    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'Me.Enabled = True
        'sBtnSave.Enabled = True
        'sBtnReject.Enabled = True
        'sBtnExit.Enabled = True

        '----------- Read Connecttion String From File XML

        ' Log("GetXmlConnectionString")

        ' HI.Conn.DB.GetXmlConnectionString()

        ' Log(Application.StartupPath & "\Database.xml")


        '  Call ValidateApp()

        '  HI.ST.SysInfo.Admin = True

        HI.TL.HandlerControl.AddHandlerObj(Me)

        '  Log("HandlerControl.AddHandlerObj")


        Dim oSysLang As New HI.ST.SysLanguage
        Try
            ' Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmApp.Name.ToString.Trim, _frmApp)
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name, Me)

            '  Log("LoadObjectLanguage")
            ' MsgBox(" ผ่าน oSysLang.LoadObjectLanguage")

        Catch ex As Exception

            '  Log("Error Sub New")

            MessageBox.Show(ex.Message)

        Finally
        End Try



    End Sub

    Private Sub wDirectorApproved_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.Enabled = True
        sBtnSave.Enabled = True
        sBtnReject.Enabled = True
        sBtnExit.Enabled = True


        Call Set_HeadGrid()
        Call SetSizeGrid()
    End Sub

    Private Sub wDirectorApproved_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Application.Exit()
    End Sub

    Private Sub wDirectorApproved_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Me.Enabled = True
        sBtnSave.Enabled = True
        sBtnReject.Enabled = True
        sBtnExit.Enabled = True

        ' System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  ผ่าน wDirectorApproved_Load")
        '  Call WriteLog_Director(Date.Now.ToLongTimeString & "   ผ่าน wDirectorApproved_Load")

        ' MsgBox(" ผ่าน wDirectorApproved_Load")

        '  Log("wDirectorApproved_Load")
        '  Call BindGrid()

        Call ValidateApp()
        otmchkpo.Enabled = True

    End Sub

    Private Shared _PathAppName As String = ""
    Private Shared _AppName As String = ""
    Private Shared _AppServiceName As String = ""

    Public Shared Sub ReadPathAppName()

        While _PathAppName = ""
            Try
                For I As Integer = 1 To 900000000

                Next
                Dim _Qry As String = "Select Top 3  FTCfgName,FTCfgData  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS A WITH(NOLOCK) WHERE FTCfgName in ('AppPath','AppName','AppSevice') "
                Dim dt As DataTable

                dt = HI.Conn.SQLConn.GetDataTable(_Qry, HI.Conn.DB.DataBaseName.DB_SECURITY)



                For Each R As DataRow In dt.Rows


                    Select Case R!FTCfgName.ToString.Trim.ToUpper
                        Case "AppPath".ToUpper
                            _PathAppName = R!FTCfgData.ToString
                        Case "AppName".ToUpper
                            _AppName = R!FTCfgData.ToString
                        Case "AppSevice".ToUpper
                            _AppServiceName = R!FTCfgData.ToString
                    End Select
                Next

            Catch ex As Exception
            End Try
        End While


    End Sub

#Region "Function"

    Private Shared Function FindComputerName(ByVal TempComName As String) As Boolean
        Dim _str As String = String.Empty
        Dim _dt As New DataTable

        Try
            _str = "SELECT  isnull(FTComputerName,'') as FTComputerName,isnull(FTUserName,'') as FTUserName  "
            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSECinfigDirector  WITH(NOLOCK) "
            _str &= Environment.NewLine & " WHERE FTComputerName = '" & TempComName & "'"

            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_SECURITY)

            ' MsgBox(" Director = " & _dt.Rows.Count)

            '  System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  เข้า FindComputerName มี =  " & _dt.Rows.Count)

            If _dt.Rows.Count > 0 Then
                HI.ST.UserInfo.UserName = _dt.Rows(0)!FTUserName.ToString
                Return True
            Else
                Return False
            End If

            _dt.Dispose()

        Catch ex As Exception

        End Try


    End Function

    Private Sub ValidateApp()


        '----------- Read Connecttion String From File XML
        '  HI.Conn.DB.GetXmlConnectionString()

        ' System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  ผ่าน HI.Conn.DB.GetXmlConnectionString")

        ' Log("ValidateApp")

        If FindComputerName(Environment.MachineName.ToString()) Then

            'Log(Environment.MachineName.ToString())

            DTPurchaseNo = Nothing
            DTPurchaseNo = LoadogcTPURTPurchase()
            If _CountApp > 0 Then

                '    Log("_CountApp = " & _CountApp)

                If Not (DTPurchaseNo Is Nothing) Then


                    ogDirectorApproved.DataSource = DTPurchaseNo
                    Dim view As GridView
                    view = ogDirectorApproved.Views(0)
                    view.OptionsView.ShowAutoFilterRow = True
                    Me.ogDirectorApproved = view.GridControl
                    Me.ogDirectorApproved.Refresh()
                    Call SetSizeGrid()
                    'Me.Show()
                    ' Call WriteLog_Director(Date.Now.ToLongTimeString & " DT มีข้อมูล =" & DT.Rows.Count)

                Else
                    ogDirectorApproved.DataSource = Nothing
                    ' Call WriteLog_Director(Date.Now.ToLongTimeString & " DT Nothing")

                End If

                'Shell("", AppWinStyle.Hide)


                'If _frmApp Is Nothing Then
                '    _frmApp = New wDirectorApproved
                'ElseIf _frmApp.IsDisposed Then
                '    _frmApp = New wDirectorApproved
                'End If

                ' HI.TL.HandlerControl.AddHandlerObj(_frmApp)
                'HI.TL.HandlerControl.AddHandlerObj(Me.Name)


                'Dim oSysLang As New HI.ST.SysLanguage
                'Try
                '    ' Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _frmApp.Name.ToString.Trim, _frmApp)
                '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name, Me)


                '    ' MsgBox(" ผ่าน oSysLang.LoadObjectLanguage")

                'Catch ex As Exception
                'Finally
                'End Try

                'Try
                '    '_frmApp.Show()
                '    '_frmApp.BringToFront()
                '    Me.Show()
                '    Me.BringToFront()

                'Catch ex As Exception
                'End Try
            Else
                '    Log("_CountApp = " & _CountApp)
                ' Log("Application.Exit()")
                '     Log(" Me.Hide()")
                ' Application.Exit()


                '  Me.Hide()

            End If

        End If

    End Sub

    Public Shared Sub Log(str As String)
        Try
            Dim fileWritter As StreamWriter = File.AppendText("C:\Test_Service.txt")
            fileWritter.WriteLine(DateTime.Now.ToString() + " " + str)
            fileWritter.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Set_HeadGrid()
        ' System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  ผ่าน Set_HeadGrid")

        '  Call WriteLog_Director(Date.Now.ToLongTimeString & "   ผ่าน Set_HeadGrid")


        With ogvDirectorApproved
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With
    End Sub


    Private Sub SetSizeGrid()

        ogvDirectorApproved.Columns.ColumnByName("ColFTStateApproved").Width = 50
        ogvDirectorApproved.Columns.ColumnByName("ColFTPurchaseBy").Width = 75
        ogvDirectorApproved.Columns.ColumnByName("ColFTSuperVisorName").Width = 75
        ogvDirectorApproved.Columns.ColumnByName("ColFTPurchaseNo").Width = 100
        ogvDirectorApproved.Columns.ColumnByName("ColFDPurchaseDate").Width = 70
        ogvDirectorApproved.Columns.ColumnByName("ColFTDeliveryCode").Width = 75
        ogvDirectorApproved.Columns.ColumnByName("ColFTDeliveryDesc").Width = 100
        ogvDirectorApproved.Columns.ColumnByName("ColFNExchangeRate").Width = 90
        ogvDirectorApproved.Columns.ColumnByName("ColFNDisCountAmt").Width = 75
        ogvDirectorApproved.Columns.ColumnByName("ColFNSurcharge").Width = 75
        ogvDirectorApproved.Columns.ColumnByName("ColFNVatAmt").Width = 110
        ogvDirectorApproved.Columns.ColumnByName("ColFNPOGrandAmt").Width = 110
        ogvDirectorApproved.Columns.ColumnByName("ColFTRemark").Width = 100
        ogvDirectorApproved.Columns.ColumnByName("ColFTTeamGrpCode").Width = 100
        ogvDirectorApproved.Columns.ColumnByName("ColFTTeamGrpName").Width = 130

    End Sub

    Public Shared Function LoadogcTPURTPurchase() As DataTable
        Try

            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            '   Call WriteLog_Director(Date.Now.ToLongTimeString & " ก่อน  LoadogcTPURTPurchased")

            'ระดับ Manager

            _str = ""
            _str = "SELECT  isnull(A.FTStateManagerApp,0) as FTStateApproved, A.FTPurchaseNo,"
            _str &= Environment.NewLine & "  SUBSTRING(A.FDPurchaseDate,9,2) + '/'+ SUBSTRING(A.FDPurchaseDate,6,2) + '/' + SUBSTRING(A.FDPurchaseDate,1,4) as FDPurchaseDate,"
            _str &= Environment.NewLine & " ISNULL( A.FTPurchaseBy,'') as FTPurchaseBy, "
            _str &= Environment.NewLine & " ISNULL( A.FTSuperVisorName,'') as FTSuperVisorName, "
            _str &= Environment.NewLine & " isnull(A.FTPurchaseState,'') as FTPurchaseState,"
            _str &= Environment.NewLine & " ISNULL( l2.FTCmpRunCode,'') as FTCmpRunCode,"
            _str &= Environment.NewLine & " L3.FTSuplCode,"
            _str &= Environment.NewLine & " SUBSTRING(A.FDDeliveryDate,9,2) + '/'+ SUBSTRING(A.FDDeliveryDate,6,2) + '/' + SUBSTRING(A.FDDeliveryDate,1,4) as FDDeliveryDate,"
            _str &= Environment.NewLine & " L4.FTCrTermCode,"
            _str &= Environment.NewLine & " ISNULL( A.FNCreditDay,0) as FNCreditDay,"
            _str &= Environment.NewLine & " l5.FTTermOfPMCode,"
            _str &= Environment.NewLine & " A.FNHSysCurId,ISNULL(A.FNExchangeRate,0) as FNExchangeRate,"
            _str &= Environment.NewLine & " L6.FTDeliveryCode,l7.FTCurCode,L1.FTDeliveryCode,"
            _str &= Environment.NewLine & " ISNULL( A.FTContactPerson,'') as FTContactPerson ,ISNULL(A.FTRemark,'') as FTRemark,"
            _str &= Environment.NewLine & " ISNULL( A.FNDisCountPer,0) as FNDisCountPer,ISNULL( A.FNDisCountAmt,0) as FNDisCountAmt,"
            _str &= Environment.NewLine & " ISNULL(A.FNPONetAmt,0) as FNPONetAmt, ISNULL(A.FNVatPer,0) as FNVatPer,ISNULL(A.FNVatAmt,0) as FNVatAmt,"
            _str &= Environment.NewLine & " ISNULL (A.FNSurcharge,0) as FNSurcharge,  ISNULL  (A.FNPOGrandAmt,0) as FNPOGrandAmt,"
            _str &= Environment.NewLine & " l8.FTTeamGrpCode,"
            _str &= Environment.NewLine & " ISNULL(C.FTUserName,'') as FTUserName,"
            _str &= Environment.NewLine & " L9.FTPurGrpCode,"
            Select Case HI.ST.Lang.Language
                Case HI.ST.Lang.eLang.TH
                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameTH,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & "isnull(l3.FTSuplNameTH,'') as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescTH,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameTH,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescTH,'') as FTDeliveryDesc,"
                    _str &= Environment.NewLine & "isnull(l8.FTTeamGrpNameTH,'') as FTTeamGrpName,"
                    _str &= Environment.NewLine & "isnull(l9.FTPurGrpNameTH,'') as FTPurGrpName"
                Case Else
                    _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameEN,'') as FTCmpRunName,"
                    _str &= Environment.NewLine & "isnull(l3.FTSuplNameEN,'') as FTSuplName,"
                    _str &= Environment.NewLine & "isnull(l4.FTCrTermDescEN,'') as FTCrTermDesc,"
                    _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameEN,'') as FTTermOfPMName,"
                    _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescEN,'') as FTDeliveryDesc,"
                    _str &= Environment.NewLine & "isnull(l8.FTTeamGrpNameEN,'') as FTTeamGrpName,"
                    _str &= Environment.NewLine & "isnull(l9.FTPurGrpNameEN,'') as FTPurGrpName"
            End Select

            _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)  INNER JOIN "
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin as B	ON a.FTSuperVisorName = b.FTUserName INNER JOIN"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp as C ON B.FNHSysTeamGrpId = C.FNHSysTeamGrpId LEFT JOIN"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDelivery as L1 with (nolock) ON a.FNHSysDeliveryId=L1.FNHSysDeliveryId  left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmpRun as L2 with (nolock)  on a.FNHSysCmpRunId=L2.FNHSysCmpRunId Left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMSupplier as L3 with (nolock) on a.FNHSysSuplId = L3.FNHSysSuplId Left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCreditTerm as L4 with (nolock) on A.FNHSysCrTermId = L4.FNHSysCrTermId  left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMPaymentTerm as L5 with (nolock) on a.FNHSysTermOfPMId = L5.FNHSysTermOfPMId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDelivery as L6 with (nolock) on a.FNHSysDeliveryId = L6.FNHSysDeliveryId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency as L7 with (nolock)  on a.FNHSysCurId = L7.FNHSysCurId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMTeamGrp as L8 with (nolock) on b.FNHSysTeamGrpId = L8.FNHSysTeamGrpId left join"
            _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TPURMPURGrp as L9 with (nolock) on a.FNHSysPurGrpId = L9.FNHSysPurGrpId "
            _str &= Environment.NewLine & " WHERE (a.FTStateSendApp = '1') AND (a.FTStateSuperVisorApp = '1') AND (a.FTStateManagerApp ='0')"
            _str &= Environment.NewLine & " Order by A.FTPurchaseBy,a.FDPurchaseDate"

            '  _dt.Columns.Add("FTImageStatus", GetType(Object))
            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_PUR)

            ' Call WriteLog_Director(Date.Now.ToLongTimeString & " หลัง  LoadogcTPURTPurchased  Count = " & _dt.Rows.Count)

            If _dt.Rows.Count > 0 Then
                _CountApp = _dt.Rows.Count
                Return _dt
            Else
                _CountApp = 0
                Return Nothing
            End If

            _dt.Dispose()


        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Function


    Private Sub BindGrid()
        Try

            'Me.Enabled = True
            'sBtnSave.Enabled = True
            'sBtnReject.Enabled = True
            'sBtnExit.Enabled = True

            ' System.IO.File.WriteAllText("E:\text.txt", " ผ่าน BindGrid")

            '   Call WriteLog_Director(Date.Now.ToLongTimeString & " ผ่าน BindGrid")

            Call Set_HeadGrid()

            '  HI.ST.SysInfo.StateDirector = True     ' true ทดสอบ Super     false ทดสอบ Manager
            ' กลับมาเรียก Function ของตัวเอง
            DT = Nothing
            DT = LoadogcTPURTPurchase()

            '  Call WriteLog_Director(Date.Now.ToLongTimeString & " ผ่าน BindGrid >> DT = LoadogcTPURTPurchase  Count = " & DT.Rows.Count)

            If Not (DT Is Nothing) Then
                ogDirectorApproved.DataSource = DT
                Dim view As GridView
                view = ogDirectorApproved.Views(0)
                view.OptionsView.ShowAutoFilterRow = True
                Me.ogDirectorApproved = view.GridControl
                Me.ogDirectorApproved.Refresh()
                Call SetSizeGrid()
                ochkselectall.Checked = False

                ' Call WriteLog_Director(Date.Now.ToLongTimeString & " DT มีข้อมูล =" & DT.Rows.Count)

            Else
                ogDirectorApproved.DataSource = Nothing
                Me.Hide()
                ' Call WriteLog_Director(Date.Now.ToLongTimeString & " DT Nothing")

            End If


        Catch ex As Exception

        End Try

    End Sub



    Friend Function Update_ManagerApproved(ByVal TempGrid As GridView, ByVal TempStatus As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long

        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0


        Try

            ReDim _aPurchaseBy(TempGrid.RowCount - 1)
            ReDim _atPurchaseNo(TempGrid.RowCount - 1)

            For k = 0 To TempGrid.RowCount - 1
                _aPurchaseBy(k) = ""
                _atPurchaseNo(k) = ""
            Next

            _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(_IntCount, "FTPurchaseBy").ToString()

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction



            For i = 0 To TempGrid.RowCount - 1

                If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then
                    _Str = ""
                    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                    _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
                    _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                    _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
                    _Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTPurchase] "
                    _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    If _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString() Then

                        If _atPurchaseNo(_IntCount) = String.Empty Then
                            _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
                        Else
                            _atPurchaseNo(_IntCount) &= " ;" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
                        End If

                    Else
                        _IntCount = _IntCount + 1
                        _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString()
                        _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()

                    End If

                End If
            Next



            For j = 0 To _IntCount

                ' ส่งเมล Approved ไปหา SuperVisor   FNMailStateType = 0

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Approved PurchaseNo   << Converter File to PDF >> ','" & _atPurchaseNo(j) & "' ,0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If



                ' ส่งเมล Approved ไปหา SuperVisor   FNMailStateType = 1

                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailText],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Approved PurchaseNo  << Converter File to PDF >> ','" & _atPurchaseNo(j) & "' ,0,1,0,0,0,0,"
                _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

                ' กรณีส่งหาตัวเอง
                'If TempGrid.GetRowCellValue(i, "FTSuperVisorName").ToString().Trim = HI.ST.UserInfo.UserName Then
                '    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                '    _Str = ""
                '    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                '    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                '    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                '    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                '    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
                '    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & TempGrid.GetRowCellValue(i, "FTSuperVisorName").ToString() & "'"
                '    _Str &= ",'Approved  " & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "  << Converter File to PDF >>' ,0,0,1,0,0,0,"
                '    _Str &= "'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

                '    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                '        HI.Conn.SQLConn.Tran.Rollback()
                '        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                '        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                '        Return False
                '    End If

                'End If


            Next


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try


    End Function

    Friend Function Update_ManagerApproved(ByVal TempGrid As GridView, ByVal TempStatus As String, ByVal TempRemark As String) As Boolean

        Dim _Str As String = String.Empty
        Dim _FTMailId As Long
        Dim _aPurchaseBy() As String
        Dim _atPurchaseNo() As String
        Dim _IntCount As Integer = 0


        Try

            ReDim _aPurchaseBy(TempGrid.RowCount - 1)
            ReDim _atPurchaseNo(TempGrid.RowCount - 1)

            For k = 0 To TempGrid.RowCount - 1
                _aPurchaseBy(k) = ""
                _atPurchaseNo(k) = ""
            Next

            _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(_IntCount, "FTPurchaseBy").ToString()

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_PUR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For i = 0 To TempGrid.RowCount - 1





                If TempGrid.GetRowCellValue(i, "FTStateApproved").ToString() = 1 Then
                    _Str = ""
                    _Str = "UPDATE [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase "
                    _Str &= Environment.NewLine & "SET  [FTStateManagerApp] = '" & TempStatus & "'"
                    _Str &= Environment.NewLine & ", [FTSuperManagerName] = '" & HI.ST.UserInfo.UserName & "'"
                    _Str &= Environment.NewLine & ", [FTSuperManagerAppDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTSuperManagerAppTime] = " & HI.UL.ULDate.FormatTimeDB
                    ' _Str &= Environment.NewLine & ", [FTRemark] = '" & TempRemark & "'"
                    _Str &= Environment.NewLine & ", [FTUpdUser] = '" & HI.ST.UserInfo.UserName & "'"
                    _Str &= Environment.NewLine & ", [FDUpdDate] = " & HI.UL.ULDate.FormatDateDB
                    _Str &= Environment.NewLine & ", [FTUpdTime] = " & HI.UL.ULDate.FormatTimeDB
                    _Str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].[dbo].[TPURTPurchase] "
                    _Str &= Environment.NewLine & " WHERE FTPurchaseNo = '" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "'"

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                    If _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString() Then

                        If _atPurchaseNo(_IntCount) = String.Empty Then
                            _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
                        Else
                            _atPurchaseNo(_IntCount) &= " ;" & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()
                        End If

                    Else
                        _IntCount = _IntCount + 1
                        _aPurchaseBy(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString()
                        _atPurchaseNo(_IntCount) = TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString()

                    End If


                End If

            Next



            For j = 0 To _IntCount
                ' ส่งเมลกลับกรณี Reject

                '_FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                '_Str = ""
                '_Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages]"
                '_Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                '_Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                '_Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                '_Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
                '_Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & TempGrid.GetRowCellValue(i, "FTPurchaseBy").ToString() & "'"
                '_Str &= ",'Reject  " & TempGrid.GetRowCellValue(i, "FTPurchaseNo").ToString() & "' ,0,1,0,0,0,0,"
                '_Str &= "@FTMailText,'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

                'HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark



                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Reject PurchaseNo',0,1,0,0,0,0,"
                _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',0)"

                'HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark


                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If


                _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                _Str = ""
                _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp],[FNMailStateType])"
                _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                _Str &= ",'Reject PurchaseNo' ,0,1,0,0,0,0,"
                _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "',1)"

                'HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark

                If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

                ' กรณีส่ง Mail ให้ตัวเอง
                If _atPurchaseNo(j) = HI.ST.UserInfo.UserName Then
                    _FTMailId = GetRunNoIDCmp("TMAILMessages", "FTMailId", HI.Conn.DB.DataBaseName.DB_MAIL)
                    _Str = ""
                    _Str = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MAIL) & "].[dbo].[TMAILMessages] "
                    _Str &= ControlChars.CrLf & "([FTMailId],[FTMailDate],[FTMailFrom],[FTMailTo]"
                    _Str &= ControlChars.CrLf & ",[FTMailSubject],[FTMailStateOpen],[FNMailStateSend]"  ',[FTMailText],,[FDMailOpenDate]
                    _Str &= ControlChars.CrLf & ",[FNMailStateAttach],[FNMailStatePriority]"
                    _Str &= ControlChars.CrLf & " ,[FNMailStateJobStatus],[FNMailFileAttach],[FTMailText],[FTInsUser],[FDInsDate],[FTInsTime],[FTMailFromTemp])"
                    _Str &= " VALUES (" & _FTMailId & "," & HI.UL.ULDate.FormatDateDB & ",'" & HI.ST.UserInfo.UserName & "','" & _aPurchaseBy(j) & "'"
                    _Str &= ",'Reject PurchaseNo',1,0,0,0,0,0,"
                    _Str &= "'" & TempRemark & vbCrLf & _atPurchaseNo(j) & "','" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"
                    ' _Str &= "@FTMailText,'" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ",'" & HI.ST.UserInfo.UserName & "')"

                    ' HI.Conn.SQLConn.Cmd.Parameters.Add("@FTMailText", SqlDbType.Text).Value = TempRemark

                    If HI.Conn.SQLConn.ExecuteTran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
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
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function



#End Region

    Private Sub ochkselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ochkselectall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With ogDirectorApproved
                If Not (.DataSource Is Nothing) And ogvDirectorApproved.RowCount > 0 Then

                    With ogvDirectorApproved
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, "FTStateApproved", _State)
                            ' .SetRowCellValue(I, .Columns.ColumnByFieldName("FTStateSuperVisorApp"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub sBtnSave_Click(sender As Object, e As EventArgs) Handles sBtnSave.Click
        Try

            If ogvDirectorApproved.RowCount = 0 Then Exit Sub



            Call Update_ManagerApproved(ogvDirectorApproved, 1)
            Call BindGrid()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sBtnExit_Click(sender As Object, e As EventArgs) Handles sBtnExit.Click
        Me.Hide()
    End Sub

    Private Sub sBtnReject_Click(sender As Object, e As EventArgs) Handles sBtnReject.Click
        Dim BolReject As Boolean = False
        Try

            ' ตรวจสอบก่อนมีการ Check หรือไม่

            With ogDirectorApproved
                If Not (.DataSource Is Nothing) And ogvDirectorApproved.RowCount > 0 Then

                    With ogvDirectorApproved
                        For I As Integer = 0 To .RowCount - 1

                            If .GetRowCellValue(I, "FTStateApproved").ToString() = 1 Then
                                BolReject = True
                                Exit For
                            Else
                                BolReject = False

                            End If

                        Next
                    End With

                End If

            End With

            If BolReject = False Then Exit Sub


            Dim _frmshowReject As New wShowReject
            otmchkpo.Enabled = False

            ' _frmshowReject = New wShowReject

            _frmshowReject.ShowDialog()

            Call Update_ManagerApproved(ogvDirectorApproved, 2, wShowReject.Data_Reason)

            otmchkpo.Enabled = True

            Call BindGrid()

        Catch ex As Exception
            otmchkpo.Enabled = True
        End Try



    End Sub

    Private Sub otmchkpo_Tick(sender As Object, e As EventArgs) Handles otmchkpo.Tick

        '  Log("otmchkpo_Tick  Work")


        'Application.DoEvents()
        Call BindGrid()
        'Application.DoEvents()
        Me.Enabled = True
        sBtnSave.Enabled = True
        sBtnReject.Enabled = True
        sBtnExit.Enabled = True

        If ogvDirectorApproved.RowCount > 0 Then

            Me.Show()

            If Me.WindowState = FormWindowState.Minimized Then
                Me.WindowState = FormWindowState.Maximized

            End If
        End If

        Call SetSizeGrid()

        '  Call WriteLog_Director(Date.Now.ToLongTimeString & "  ผ่าน otmchkpo_Tick")

        ' System.IO.File.WriteAllText("E:\text.txt", Date.Now.ToLongTimeString & "  ผ่าน otmchkpo_Tick")
    End Sub

    Private Sub ogvDirectorApproved_DoubleClick(sender As Object, e As EventArgs) Handles ogvDirectorApproved.DoubleClick
        With Me.ogvDirectorApproved

            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub

            ' _OldRow = .FocusedRowHandle

            Dim _PurchaseNo As String = "" & .GetRowCellValue(.FocusedRowHandle, "FTPurchaseNo").ToString()
            Dim _WformPo As New wPurchaseOrder

            With _WformPo
                .ocmexit.Visible = False
                .ocmclear.Visible = False
                .FTPurchaseNo.Properties.ReadOnly = True
                .FTPurchaseNo.Properties.Buttons(0).Enabled = False
                .FTPurchaseNo.Properties.Buttons(1).Enabled = False
            End With

            ' Dim _WShow As New wShowData(_WformPo, _PurchaseNo)

            Dim _TmpMenu As String = HI.ST.SysInfo.MenuName
            HI.ST.SysInfo.MenuName = "MnuManualPurchase"
            Dim _WShow As New wShowData(_WformPo, _PurchaseNo)
            HI.ST.SysInfo.MenuName = _TmpMenu

            With _WShow
                .WindowState = System.Windows.Forms.FormWindowState.Maximized
                .StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                .ShowDialog()
            End With

        End With
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        With Me.ogvDirectorApproved
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _PoNo As String = "" & .GetFocusedRowCellValue("FTPurchaseNo").ToString
            Dim _FNPoState As Integer = 0

            Dim _Qry As String = ""

            _Qry = "Select TOP 1 FNPoState   "
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(_PoNo) & "' "

            _FNPoState = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, HI.Conn.DB.DataBaseName.DB_PUR, "0")))

            With New HI.RP.Report

                Dim _tmplang As HI.ST.Lang.eLang = HI.ST.Lang.Language

                If _FNPoState = 0 Then
                    HI.ST.Lang.Language = HI.ST.Lang.eLang.TH
                Else
                    HI.ST.Lang.Language = HI.ST.Lang.eLang.EN
                End If

                .FormTitle = Me.Text
                .ReportFolderName = "PurchaseOrder\"
                .ReportName = "PurchaseOrder.rpt"
                .AddParameter("Draft", "DRAFT")
                .Formular = "{TPURTPurchase.FTPurchaseNo}='" & HI.UL.ULF.rpQuoted(_PoNo) & "'"
                .Preview()

                HI.ST.Lang.Language = _tmplang
            End With

        End With
    End Sub
End Class