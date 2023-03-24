Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns

Imports System.Data.SqlClient
Imports System.Data

Public Class wDirectorApproved

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

        'Me.Enabled = True
        'sBtnSave.Enabled = True
        'sBtnReject.Enabled = True
        'sBtnExit.Enabled = True

    End Sub

    Private Sub wDirectorApproved_Activated(sender As Object, e As EventArgs) Handles Me.Activated

        Me.Enabled = True
        sBtnSave.Enabled = True
        sBtnReject.Enabled = True
        sBtnExit.Enabled = True


        Call Set_HeadGrid()
        Call SetSizeGrid()


    End Sub

#Region "Function"
    Private Function ShowData() As Boolean


        If DT Is Nothing Then
            Me.ogDirectorApproved.DataSource = Nothing

            Return False
        End If

        SetDataTable(DT)
       

        Return True

    End Function


    Private Sub SetDataTable(ByVal TB As DataTable)
        If ogDirectorApproved.InvokeRequired Then
            ogDirectorApproved.Invoke(New Action(Of DataTable)(AddressOf SetDataTable), DT)

        Else
            ogDirectorApproved.DataSource = DT

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
           
            Return False

        End Try

    End Function

    Private Sub Set_HeadGrid()
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


    'Private Function LoadogcTPURTPurchase() As DataTable
    '    Try

    '        'SQLConn._ConnString = DB.ConnectionString(Conn.DB.DataBaseName.DB_PUR)
    '        'SQLConn.SqlConnectionOpen()
    '        'SQLConn.Cmd = SQLConn.Cnn.CreateCommand
    '        'SQLConn.Tran = SQLConn.Cnn.BeginTransaction


    '        Me.Enabled = True
    '        sBtnSave.Enabled = True
    '        sBtnReject.Enabled = True
    '        sBtnExit.Enabled = True

    '        Dim _str As String = String.Empty
    '        Dim _dt As New DataTable



    '                _str = ""
    '                _str = "SELECT  isnull(A.FTStateManagerApp,0) as FTStateApproved, A.FTPurchaseNo,"
    '                _str &= Environment.NewLine & "  SUBSTRING(A.FDPurchaseDate,9,2) + '/'+ SUBSTRING(A.FDPurchaseDate,6,2) + '/' + SUBSTRING(A.FDPurchaseDate,1,4) as FDPurchaseDate,"
    '                _str &= Environment.NewLine & " ISNULL( A.FTPurchaseBy,'') as FTPurchaseBy, "
    '                _str &= Environment.NewLine & " ISNULL( A.FTSuperVisorName,'') as FTSuperVisorName, "
    '                _str &= Environment.NewLine & " isnull(A.FTPurchaseState,'') as FTPurchaseState,"
    '                _str &= Environment.NewLine & " ISNULL( l2.FTCmpRunCode,'') as FTCmpRunCode,"
    '                _str &= Environment.NewLine & " L3.FTSuplCode,"
    '                _str &= Environment.NewLine & " SUBSTRING(A.FDDeliveryDate,9,2) + '/'+ SUBSTRING(A.FDDeliveryDate,6,2) + '/' + SUBSTRING(A.FDDeliveryDate,1,4) as FDDeliveryDate,"
    '                _str &= Environment.NewLine & " L4.FTCrTermCode,"
    '                _str &= Environment.NewLine & " ISNULL( A.FNCreditDay,0) as FNCreditDay,"
    '                _str &= Environment.NewLine & " l5.FTTermOfPMCode,"
    '                _str &= Environment.NewLine & " A.FNHSysCurId,ISNULL(A.FNExchangeRate,0) as FNExchangeRate,"
    '                _str &= Environment.NewLine & " L6.FTDeliveryCode,l7.FTCurCode,L1.FTDeliveryCode,"
    '                _str &= Environment.NewLine & " ISNULL( A.FTContactPerson,'') as FTContactPerson ,ISNULL(A.FTRemark,'') as FTRemark,"
    '                _str &= Environment.NewLine & " ISNULL( A.FNDisCountPer,0) as FNDisCountPer,ISNULL( A.FNDisCountAmt,0) as FNDisCountAmt,"
    '                _str &= Environment.NewLine & " ISNULL(A.FNPONetAmt,0) as FNPONetAmt, ISNULL(A.FNVatPer,0) as FNVatPer,ISNULL(A.FNVatAmt,0) as FNVatAmt,"
    '                _str &= Environment.NewLine & " ISNULL (A.FNSurcharge,0) as FNSurcharge,  ISNULL  (A.FNPOGrandAmt,0) as FNPOGrandAmt,"
    '                _str &= Environment.NewLine & " l8.FTTeamGrpCode,"
    '                _str &= Environment.NewLine & " ISNULL(C.FTUserName,'') as FTUserName,"
    '                _str &= Environment.NewLine & " L9.FTPurGrpCode,"

    '                Select Case HI.ST.Lang.Language
    '                    Case HI.ST.Lang.Lang.EN
    '                        _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameEN,'') as FTCmpRunName,"
    '                        _str &= Environment.NewLine & "isnull(l3.FTSuplNameEN,'') as FTSuplName,"
    '                        _str &= Environment.NewLine & "isnull(l4.FTCrTermDescEN,'') as FTCrTermDesc,"
    '                        _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameEN,'') as FTTermOfPMName,"
    '                        _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescEN,'') as FTDeliveryDesc,"
    '                        _str &= Environment.NewLine & "isnull(l8.FTTeamGrpNameEN,'') as FTTeamGrpName,"
    '                        _str &= Environment.NewLine & "isnull(l9.FTPurGrpNameEN,'') as FTPurGrpName"
    '                    Case HI.ST.Lang.Lang.TH
    '                        _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameTH,'') as FTCmpRunName,"
    '                        _str &= Environment.NewLine & "isnull(l3.FTSuplNameTH,'') as FTSuplName,"
    '                        _str &= Environment.NewLine & "isnull(l4.FTCrTermDescTH,'') as FTCrTermDesc,"
    '                        _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameTH,'') as FTTermOfPMName,"
    '                        _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescTH,'') as FTDeliveryDesc,"
    '                        _str &= Environment.NewLine & "isnull(l8.FTTeamGrpNameTH,'') as FTTeamGrpName,"
    '                        _str &= Environment.NewLine & "isnull(l9.FTPurGrpNameTH,'') as FTPurGrpName"
    '                    Case Else
    '                        _str &= Environment.NewLine & "isnull(l2.FTCmpRunNameEN,'') as FTCmpRunName,"
    '                        _str &= Environment.NewLine & "isnull(l3.FTSuplNameEN,'') as FTSuplName,"
    '                        _str &= Environment.NewLine & "isnull(l4.FTCrTermDescEN,'') as FTCrTermDesc,"
    '                        _str &= Environment.NewLine & "isnull(l5.FTTermOfPMNameEN,'') as FTTermOfPMName,"
    '                        _str &= Environment.NewLine & "isnull(l1.FTDeliveryDescEN,'') as FTDeliveryDesc,"
    '                        _str &= Environment.NewLine & "isnull(l8.FTTeamGrpNameEN,'') as FTTeamGrpName,"
    '                        _str &= Environment.NewLine & "isnull(l9.FTPurGrpNameEN,'') as FTPurGrpName"
    '                End Select

    '                ' _str &= Environment.NewLine & " FROM TPURTPurchase as A with(nolock) INNER JOIN "
    '                _str &= Environment.NewLine & " FROM [" & DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK)  INNER JOIN "
    '                _str &= Environment.NewLine & " [" & DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin as B	ON a.FTSuperVisorName = b.FTUserName INNER JOIN"
    '                _str &= Environment.NewLine & " [" & DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMTeamGrp as C ON B.FNHSysTeamGrpId = C.FNHSysTeamGrpId LEFT JOIN"
    '                _str &= Environment.NewLine & " [" & DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDelivery as L1 with (nolock) ON a.FNHSysDeliveryId=L1.FNHSysDeliveryId  left join"
    '                _str &= Environment.NewLine & " [" & DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmpRun as L2 with (nolock)  on a.FNHSysCmpRunId=L2.FNHSysCmpRunId Left join"
    '                _str &= Environment.NewLine & " [" & DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMSupplier as L3 with (nolock) on a.FNHSysSuplId = L3.FNHSysSuplId Left join"
    '                _str &= Environment.NewLine & " [" & DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCreditTerm as L4 with (nolock) on A.FNHSysCrTermId = L4.FNHSysCrTermId  left join"
    '                _str &= Environment.NewLine & " [" & DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMPaymentTerm as L5 with (nolock) on a.FNHSysTermOfPMId = L5.FNHSysTermOfPMId left join"
    '                _str &= Environment.NewLine & " [" & DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMDelivery as L6 with (nolock) on a.FNHSysDeliveryId = L6.FNHSysDeliveryId left join"
    '                _str &= Environment.NewLine & " [" & DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TFINMCurrency as L7 with (nolock)  on a.FNHSysCurId = L7.FNHSysCurId left join"
    '                _str &= Environment.NewLine & " [" & DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMTeamGrp as L8 with (nolock) on b.FNHSysTeamGrpId = L8.FNHSysTeamGrpId left join"
    '                _str &= Environment.NewLine & " [" & DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TPURMPURGrp as L9 with (nolock) on a.FNHSysPurGrpId = L9.FNHSysPurGrpId "
    '                _str &= Environment.NewLine & " WHERE (a.FTStateSendApp = '1') AND (a.FTStateSuperVisorApp = '1') AND (a.FTStateManagerApp ='0')"
    '                _str &= Environment.NewLine & " Order by a.FDPurchaseDate"

    '                ' _str &= Environment.NewLine & " WHERE (C.FTUserName = '" & HI.ST.UserInfo.UserName & "')"



    '        _dt = SQLConn.GetDataTable(_str, Conn.DB.DataBaseName.DB_PUR)

    '        If _dt.Rows.Count > 0 Then
    '            ' _CountApp = _dt.Rows.Count
    '            Return _dt
    '        Else
    '            ' _CountApp = 0
    '            Return Nothing
    '        End If

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Function


#End Region

    Private Sub wDirectorApproved_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

End Class