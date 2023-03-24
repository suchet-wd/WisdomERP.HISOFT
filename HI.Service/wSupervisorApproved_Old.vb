Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns

Imports System.Data.SqlClient
Imports System.Data

Public Class wSupervisorApproved_Old
    ' Private Mythread As Threading.Thread
    Private DT As DataTable
    Private ClsService As New ClsService

    Private MSSQL_Conn As SqlConnection = Nothing
    Private MSSQL_Da As SqlDataAdapter = Nothing
    Private MSSQL_Dr As SqlDataReader = Nothing
    Private MSSQL_Cmd As SqlCommand = Nothing
    Private MSSQL_Tran As SqlTransaction = Nothing




    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Call ConnectMSSQL("", "", "", "")


        ' Call ConnectionString(Conn.DB.DataBaseName.DB_PUR)

        ' Timer1.Interval = 3000
        ' Timer1.Enabled = True
        Me.Enabled = True
        sBtnSave.Enabled = True
        sBtnReject.Enabled = True
        sBtnExit.Enabled = True

    End Sub

    Private Sub wSupervisorApproved_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Me.Enabled = True
        sBtnSave.Enabled = True
        sBtnReject.Enabled = True
        sBtnExit.Enabled = True


        Call Set_HeadGrid()
        Call SetSizeGrid()

    End Sub



    'Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
    '    MyBase.OnFormClosing(e)
    '    Me.Hide()
    'End Sub


    Private Sub wSupervisorApproved_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Call ClsService.Update_SupervisorApproved(ogvSupervisorApproved)
        ' Me = Nothing
    End Sub



#Region "Function"
    Public Sub LoadDataInfo(Key As String)

    End Sub
    Private Sub FindData()

        '  Call ConnectMSSQL()

        If ShowData() = True Then

        Else

            'Me.Close()
            Application.Exit()
        End If

    End Sub

    Private Function ShowData() As Boolean

        DT = ClsService.LoadogcTPURTPurchase

        ' Threading.Thread.Sleep(6000)

        If DT Is Nothing Then
            Me.ogSupervisorApproved.DataSource = Nothing

            Return False
        End If



        ' ogSupervisorApproved.DataSource = DT

        SetDataTable(DT)
        ' ogSupervisorApproved.Visible = True

        'Dim view As GridView
        'view = ogSupervisorApproved.Views(0)
        'view.OptionsView.ShowAutoFilterRow = True
        'view.BestFitColumns()

        'Me.ogSupervisorApproved = view.GridControl
        'Me.ogSupervisorApproved.Refresh()


        Return True

    End Function

    Private Sub SetDataTable(ByVal TB As DataTable)
        If ogSupervisorApproved.InvokeRequired Then
            ogSupervisorApproved.Invoke(New Action(Of DataTable)(AddressOf SetDataTable), DT)

        Else
            ogSupervisorApproved.DataSource = DT

            'Dim view As GridView
            'view = ogSupervisorApproved.Views(0)
            'view.OptionsView.ShowAutoFilterRow = True
            ''view.BestFitColumns()

            'Me.ogSupervisorApproved = view.GridControl
            'Me.ogSupervisorApproved.Refresh()


        End If
    End Sub


    Private Function ConnectMSSQL(ByVal strServer As String, ByVal strUser As String, ByVal strPass As String, ByVal strDatabase As String) As Boolean

        Try

            Dim strConn As New SqlConnectionStringBuilder
            ' "SERVER=HT-DEVELOPX;UID=sa;PWD=1234;Initial Catalog=HITECH_SYSTEM"
            strConn.DataSource = "HT-DEVELOPX"
            strConn.UserID = "sa"
            strConn.Password = "1234"
            strConn.InitialCatalog = "HITECH_MAIL"

            MSSQL_Conn = New SqlConnection(strConn.ConnectionString)
            MSSQL_Conn.Open()
            MSSQL_Conn.Close()

            Return True

        Catch ex As Exception

            MessageBox.Show("ไม่สามารถเชื่อมต่อกับฐานข้อมูลได้", "Message 38", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False

        End Try

    End Function

    Private Sub Set_HeadGrid()
        With ogvSupervisorApproved
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect
        End With
    End Sub


    Private Sub SetSizeGrid()

        ogvSupervisorApproved.Columns.ColumnByName("ColFTStateApproved").Width = 50
        ogvSupervisorApproved.Columns.ColumnByName("ColFTPurchaseBy").Width = 75
        ogvSupervisorApproved.Columns.ColumnByName("ColFTSuperVisorName").Width = 75
        ogvSupervisorApproved.Columns.ColumnByName("ColFTPurchaseNo").Width = 100
        ogvSupervisorApproved.Columns.ColumnByName("ColFDPurchaseDate").Width = 70
        ogvSupervisorApproved.Columns.ColumnByName("ColFTDeliveryCode").Width = 75
        ogvSupervisorApproved.Columns.ColumnByName("ColFTDeliveryDesc").Width = 100
        ogvSupervisorApproved.Columns.ColumnByName("ColFNExchangeRate").Width = 90
        ogvSupervisorApproved.Columns.ColumnByName("ColFNDisCountAmt").Width = 75
        ogvSupervisorApproved.Columns.ColumnByName("ColFNSurcharge").Width = 75
        ogvSupervisorApproved.Columns.ColumnByName("ColFNVatAmt").Width = 110
        ogvSupervisorApproved.Columns.ColumnByName("ColFNPOGrandAmt").Width = 110
        ogvSupervisorApproved.Columns.ColumnByName("ColFTRemark").Width = 100
        ogvSupervisorApproved.Columns.ColumnByName("ColFTTeamGrpCode").Width = 100
        ogvSupervisorApproved.Columns.ColumnByName("ColFTTeamGrpName").Width = 130

        ' ogvSupervisorApproved.Columns.ColumnByName("GridColumn2").Width = 200
        ' ogvSupervisorApproved.Columns.ColumnByName("GridColumn3").Width = 200
        ' ogvSupervisorApproved.Columns.ColumnByName("GridColumn4").Width = 100
        ' ogvSupervisorApproved.Columns.ColumnByName("GridColumn5").Width = 100

    End Sub

    Private Function LoadogcTPURTPurchase() As DataTable
        Try

            'HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR)
            'HI.Conn.SQLConn.SqlConnectionOpen()
            'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            'HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            Me.Enabled = True
            sBtnSave.Enabled = True
            sBtnReject.Enabled = True
            sBtnExit.Enabled = True

            Dim _str As String = String.Empty
            Dim _dt As New DataTable

            Select Case HI.ST.SysInfo.StateDirector

                Case False ' ระดับ SuperVisorApp

                    _str = ""
                    _str = "SELECT  isnull(A.FTStateSuperVisorApp,0) as FTStateApproved, A.FTPurchaseNo,"
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

                    ' _str &= Environment.NewLine & " FROM TPURTPurchase as A with(nolock) INNER JOIN "
                    _str &= Environment.NewLine & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)  INNER JOIN "
                    _str &= Environment.NewLine & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin as B	ON a.FTPurchaseBy = b.FTUserName INNER JOIN"
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
                    _str &= Environment.NewLine & " WHERE (C.FTUserName = '" & HI.ST.UserInfo.UserName & "')"
                    ' _str &= Environment.NewLine & " AND (a.FTStateSendApp = '1') AND  (isnull(a.FTStateSuperVisorApp,'0') = '0')"
                    _str &= Environment.NewLine & " AND (a.FTStateSendApp = '1') AND (a.FTStateSuperVisorApp = '0')"
                    _str &= Environment.NewLine & " Order by  A.FTPurchaseBy, a.FDPurchaseDate"




                Case True   'ระดับ Manager

                    '_str = "SELECT  isnull(A.FTStateSuperVisorApp,0) as FTStateSuperVisorApp, A.FTPurchaseNo,"


                    'เปลี่ยนค่าแทน ใช้ DataGrid ตัวเดียวกัน   Supervisor + Manager
                    'FTStateManagerApp  ==> FTStateSuperVisorApp

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

                    ' _str &= Environment.NewLine & " FROM TPURTPurchase as A with(nolock) INNER JOIN "
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
                    '  _str &= Environment.NewLine & " WHERE (a.FTStateSendApp = '1') AND (isnull(a.FTStateSuperVisorApp,'0') = '1') AND (isnull(a.FTStateManagerApp,'0') ='0')"
                    _str &= Environment.NewLine & " WHERE (a.FTStateSendApp = '1') AND (a.FTStateSuperVisorApp = '1') AND (a.FTStateManagerApp ='0')"
                    _str &= Environment.NewLine & " Order by  a.FTPurchaseBy, a.FDPurchaseDate"



                    '_str &= Environment.NewLine & " WHERE (a.FTStateSendApp = '1') AND (a.FTStateSuperVisorApp = '1') AND (a.FTStateManagerApp ='0')"
                    '_str &= Environment.NewLine & " Order by  a.FTPurchaseBy, a.FDPurchaseDate"

                    ' _str &= Environment.NewLine & " WHERE (C.FTUserName = '" & HI.ST.UserInfo.UserName & "')"



            End Select


            _dt = HI.Conn.SQLConn.GetDataTable(_str, HI.Conn.DB.DataBaseName.DB_PUR)

            If _dt.Rows.Count > 0 Then
                ' _CountApp = _dt.Rows.Count
                Return _dt
            Else
                ' _CountApp = 0
                Return Nothing
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function



#End Region


    Private Sub wSupervisorApproved_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.Enabled = True
        sBtnSave.Enabled = True
        sBtnReject.Enabled = True
        sBtnExit.Enabled = True

        Call BindGrid()
        otmchkpo.Enabled = True
    End Sub

    Private Sub BindGrid()
        Try

            Me.Enabled = True
            sBtnSave.Enabled = True
            sBtnReject.Enabled = True
            sBtnExit.Enabled = True


            Call Set_HeadGrid()

            '  HI.ST.SysInfo.StateDirector = True     ' true ทดสอบ Super     false ทดสอบ Manager

            ' กลับมาเรียก Function ของตัวเอง
            DT = Nothing
            DT = LoadogcTPURTPurchase()

            If Not (DT Is Nothing) Then
                ogSupervisorApproved.DataSource = DT
                Dim view As GridView
                view = ogSupervisorApproved.Views(0)
                view.OptionsView.ShowAutoFilterRow = True
                Me.ogSupervisorApproved = view.GridControl
                Me.ogSupervisorApproved.Refresh()
                Call SetSizeGrid()
                ochkselectall.Checked = False
            Else
                ogSupervisorApproved.DataSource = Nothing
                Me.Hide()
                ' Me.Close()
            End If

            ' Call SetSizeGrid()


            'ogSupervisorApproved.DataSource = ClsService.Data_DTPurchaseNo
            'Dim view As GridView
            'view = ogSupervisorApproved.Views(0)
            'view.OptionsView.ShowAutoFilterRow = True
            ''view.BestFitColumns()
            'Me.ogSupervisorApproved = view.GridControl
            'Me.ogSupervisorApproved.Refresh()
        Catch ex As Exception
            ' MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub ochkselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ochkselectall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With ogSupervisorApproved
                If Not (.DataSource Is Nothing) And ogvSupervisorApproved.RowCount > 0 Then

                    With ogvSupervisorApproved
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

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles sBtnSave.Click

        '  Dim BolReject As Boolean = False
        Try

            If ogvSupervisorApproved.RowCount = 0 Then Exit Sub

            ' ตรวจสอบก่อนมีการ Check หรือไม่

            'With ogSupervisorApproved
            '    If Not (.DataSource Is Nothing) And ogvSupervisorApproved.RowCount > 0 Then

            '        With ogvSupervisorApproved
            '            For I As Integer = 0 To .RowCount - 1

            '                If .GetRowCellValue(I, "FTStateApproved").ToString() = 1 Then
            '                    BolReject = True
            '                    Exit For
            '                Else
            '                    BolReject = False

            '                End If

            '            Next
            '        End With

            '    End If

            'End With

            'If BolReject = False Then Exit Sub


            Select Case HI.ST.SysInfo.StateDirector

                Case False ' ระดับ SuperVisorApp
                    Call ClsService.Update_SupervisorApproved(ogvSupervisorApproved, 1)

                Case True ' ระดับ Manager
                    Call ClsService.Update_ManagerApproved(ogvSupervisorApproved, 1)

                    ' สร้าง File PDF  เอา คริสตันรายงาน มาจากทัย

            End Select

            Call BindGrid()

        Catch ex As Exception

        End Try
        ' Me.Close()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles sBtnExit.Click
        'Me.Close()
        Me.Hide()
    End Sub

    Private Sub BtnReject_Click(sender As Object, e As EventArgs) Handles sBtnReject.Click

        ' Dim _frmshowReject As New wShowReject
        Dim BolReject As Boolean = False
        Try

            ' ตรวจสอบก่อนมีการ Check หรือไม่

            With ogSupervisorApproved
                If Not (.DataSource Is Nothing) And ogvSupervisorApproved.RowCount > 0 Then

                    With ogvSupervisorApproved
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

            Select Case HI.ST.SysInfo.StateDirector

                Case False ' ระดับ SuperVisorApp
                    Call ClsService.Update_SupervisorApproved(ogvSupervisorApproved, 2, wShowReject.Data_Reason)

                Case True ' ระดับ Manager
                    Call ClsService.Update_ManagerApproved(ogvSupervisorApproved, 2, wShowReject.Data_Reason)

            End Select


            otmchkpo.Enabled = True


            Call BindGrid()

        Catch ex As Exception
            otmchkpo.Enabled = True
        End Try




        'Dim sRemark As String
        'sRemark = InputBox("Reasons for rejection", "Reject")

        'Select Case HI.ST.SysInfo.StateDirector

        '    Case False ' ระดับ SuperVisorApp
        '        Call ClsService.Update_SupervisorApproved(ogvSupervisorApproved, 2, sRemark)

        '    Case True ' ระดับ Manager
        '        Call ClsService.Update_ManagerApproved(ogvSupervisorApproved, 2, sRemark)

        'End Select


        '  Me.Close()
    End Sub

    Private Sub otmchkpo_Tick(sender As Object, e As EventArgs) Handles otmchkpo.Tick

        ' Me.Hide()
        'ogApprovedMail.Visible = False

        Application.DoEvents()
        Call BindGrid()
        'Application.DoEvents()
        Me.Enabled = True
        sBtnSave.Enabled = True
        sBtnReject.Enabled = True
        sBtnExit.Enabled = True

        If ogvSupervisorApproved.RowCount > 0 Then

            If Me.WindowState = FormWindowState.Minimized Then    'FormWindowState.Minimized = 1

                Me.WindowState = FormWindowState.Maximized

            End If
        End If

        Call SetSizeGrid()


        ' ogApprovedMail.Visible = True
        '  Me.Show()

    End Sub


    Private Sub ogvSupervisorApproved_DoubleClick(sender As Object, e As EventArgs) Handles ogvSupervisorApproved.DoubleClick

        With Me.ogvSupervisorApproved

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


    Private Sub ogSupervisorApproved_Click(sender As Object, e As EventArgs) Handles ogSupervisorApproved.Click

    End Sub
End Class