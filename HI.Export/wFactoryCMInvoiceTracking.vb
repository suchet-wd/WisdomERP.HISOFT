Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ButtonEdit
Imports DevExpress.XtraGrid.Filter
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraPrintingLinks
Imports DevExpress.Utils

Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Configuration
Imports System.Data.Common
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Diagnostics
Imports System.Globalization
Imports System.Windows.Forms
Imports System.Threading


Public Class wFactoryCMInvoiceTracking


    Dim View As GridView
    Dim RowsIndex As Double
    Dim TopVisibleIndex As Int32
    Private sFNHSysStyleId As String

    ''' Used Data Adapter to control database

    Dim oleDbDataAdapter1 As DbDataAdapter
    Dim oleDbDataAdapter2 As DbDataAdapter
    Dim dtStyleDetail As System.Data.DataTable
    Private _StateCheckPOWaitting As Boolean

    Dim FirstLoad As Boolean = True
    Private Inited As Boolean
    Dim RetMessage As String = ""

#Region "Handler Control"

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        'With RepositoryFTMainMatCode
        '    AddHandler .Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
        '    AddHandler .ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
        '    AddHandler .EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
        '    AddHandler .Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave

        'End With

        Call InitGrid()

    End Sub

    Private _ProcComplete As Boolean = False
    Public Property ProcComplete As Boolean
        Get
            Return _ProcComplete
        End Get
        Set(value As Boolean)
            _ProcComplete = value
        End Set
    End Property

#End Region

#Region "MAIN PROC"

    Private Sub wOrderListingInfo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            FirstLoad = False
            FNHSysCmpId = GetPropertyTagValue(FNHSysCmpId)
            FNHSysBuyId = GetPropertyTagValue(FNHSysBuyId)
            FNHSysStyleId = GetPropertyTagValue(FNHSysStyleId)
            FNHSysCustId = GetPropertyTagValue(FNHSysCustId)
            FNHSysSeasonId = GetPropertyTagValue(FNHSysSeasonId)
            FNHSysPOID = GetPropertyTagValue(FNHSysPOID)


            sFNHSysStyleId = ""

            '_StateCheckPOWaitting = False
            'Dim _Theard As New Thread(AddressOf CheckWaiting)
            '_Theard.Start()

            ' Me.ocmcheckpowaiting.Enabled = True

        Catch ex As Exception

        End Try


    End Sub

    Private Sub Proc_Close(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub FNHSysCmpId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysCmpId.EditValueChanged
        'FNHSysCmpId = GetPropertyTagValue(FNHSysCmpId)
        'If FirstLoad = False Then
        '    Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        '    ogcmatcode.Refresh()
        'End If
    End Sub

    Private Sub FNHSysBuyId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysBuyId.EditValueChanged
        'FNHSysBuyId = GetPropertyTagValue(FNHSysBuyId)
        'If FirstLoad = False Then
        '    Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        '    ogcmatcode.Refresh()
        'End If
    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysStyleId.EditValueChanged
        'FNHSysStyleId = GetPropertyTagValue(FNHSysStyleId)
        'If FirstLoad = False Then
        '    Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        '    ogcmatcode.Refresh()
        'End If
    End Sub

    Private Sub FNHSysCustId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysCustId.EditValueChanged
        'FNHSysCustId = GetPropertyTagValue(FNHSysCustId)
        'If FirstLoad = False Then
        '    Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        '    ogcmatcode.Refresh()
        'End If
    End Sub

    Private Sub FNHSysSeasonId_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysSeasonId.EditValueChanged
        'FNHSysSeasonId = GetPropertyTagValue(FNHSysSeasonId)
        'If FirstLoad = False Then
        '    Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        '    ogcmatcode.Refresh()
        'End If
    End Sub

    Private Sub FNHSysPOID_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FNHSysPOID.EditValueChanged
        'FNHSysPOID = GetPropertyTagValue(FNHSysPOID)
        'If FirstLoad = False Then
        '    Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        '    ogcmatcode.Refresh()
        'End If
    End Sub


    Private Function GetPropertyTagValue(ByVal obj As Object) As Control
        Dim _Str As String = ""
        Dim sTable As String = ""
        Dim sField As String = ""
        Dim sValue As String = ""
        Dim sCrite As String = ""

        Select Case obj.GetType.FullName.ToString.ToUpper
            Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                If CType(obj, DevExpress.XtraEditors.ButtonEdit).Properties.Tag = "" Then
                    Select Case CType(obj, System.Windows.Forms.Control).Name.ToUpper
                        Case "FNHSysCmpId".ToUpper
                            sTable = "HITECH_MASTER.dbo.TCNMCmp"
                            sField = obj.GetType.FullName.ToString()
                            sCrite = "FTCmpCode"
                            sValue = CType(obj, DevExpress.XtraEditors.ButtonEdit).Text
                        Case "FNHSysStyleId".ToUpper
                            sTable = "HITECH_MASTER.dbo.TMERMStyle"
                            sField = obj.GetType.FullName.ToString()
                            sCrite = "FTStyleCode"
                            sValue = CType(obj, DevExpress.XtraEditors.ButtonEdit).Text
                        Case "FNHSysCustId".ToUpper
                            sTable = "HITECH_MASTER.dbo.TCNMCustomer"
                            sField = obj.GetType.FullName.ToString()
                            sCrite = "FTCustCode"
                            sValue = CType(obj, DevExpress.XtraEditors.ButtonEdit).Text
                        Case "FNHSysBuyId".ToUpper
                            sTable = "HITECH_MASTER.dbo.TMERMBuy"
                            sField = obj.GetType.FullName.ToString()
                            sCrite = "FTBuyCode"
                            sValue = CType(obj, DevExpress.XtraEditors.ButtonEdit).Text
                        Case "FNHSysSeasonId".ToUpper
                            sTable = "HITECH_MASTER.dbo.TMERMSeason"
                            sField = obj.GetType.FullName.ToString()
                            sCrite = "FTSeasonCode"
                            sValue = CType(obj, DevExpress.XtraEditors.ButtonEdit).Text
                        Case "FNHSysPOID".ToUpper
                            sTable = "HITECH_MERCHAN.DBO.TMERTORDER"
                            sField = "FTPOREF"
                            sCrite = "FTPOREF"
                            sValue = CType(obj, DevExpress.XtraEditors.ButtonEdit).Text
                    End Select
                    If sTable <> "" Then
                        _Str = "SELECT TOP 1 " & sField & " FROM " & sTable & " WITH(NOLOCK) WHERE " & sCrite & " = '" & sValue & "'"
                        CType(obj, DevExpress.XtraEditors.ButtonEdit).Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
                    Else
                        CType(obj, DevExpress.XtraEditors.ButtonEdit).Properties.Tag = ""
                    End If

                End If

        End Select

        Return obj

    End Function

    Private Sub LoadOrderListingInfoToDirector(ByVal _FNHSysCmpId As String, ByVal _FTPORef As String, ByVal _FNHSysStyleId As String, ByVal _FNHSysCustId As String, ByVal _FNHSysBuyId As String, ByVal _FNHSysSeasonId As String)
        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Try

            If _FNHSysCmpId.Trim() = "" Then _FNHSysCmpId = "0"
            If _FNHSysStyleId.Trim() = "" Then _FNHSysStyleId = "0"
            If _FNHSysCustId.Trim() = "" Then _FNHSysCustId = "0"
            If _FNHSysBuyId.Trim() = "" Then _FNHSysBuyId = "0"
            If _FNHSysSeasonId.Trim() = "" Then _FNHSysSeasonId = "0"

            Dim dt As New System.Data.DataTable
            Dim _Qry As String = ""

            _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].[SP_ORDER_LISTING_FACCMINV_TRACKING] " & Val(_FNHSysCmpId) & ",'" & HI.UL.ULF.rpQuoted(_FTPORef) & "'," & Val(_FNHSysStyleId) & "," & Val(_FNHSysCustId) & "," & Val(_FNHSysBuyId) & "," & Val(_FNHSysSeasonId) & ",'" & HI.UL.ULF.rpQuoted(HI.ST.Lang.Language.ToString()) & "','" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "'"
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)
            Me.ogcdirector.DataSource = dt

            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmclear1_Click(sender As System.Object, e As System.EventArgs) Handles ocmclearclsr.Click

        ogcdirector.DataSource = Nothing
        Dim xCol As Integer = 0
        Dim Idx As Integer = 0
        Try

            HI.TL.HandlerControl.ClearControl(Me)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function PROC_VALIDATEbSHOWBROWSEDATA() As Boolean
        Dim bFlagValidate As Boolean = False

        If Not bFlagValidate AndAlso Me.FNHSysCmpId.Text.Trim <> "" Then bFlagValidate = True
        If Not bFlagValidate AndAlso Me.FNHSysPOID.Text.Trim <> "" Then bFlagValidate = True
        If Not bFlagValidate AndAlso Me.FNHSysStyleId.Text.Trim <> "" Then bFlagValidate = True
        If Not bFlagValidate AndAlso Me.FNHSysCustId.Text.Trim <> "" Then bFlagValidate = True
        If Not bFlagValidate AndAlso Me.FNHSysBuyId.Text.Trim <> "" Then bFlagValidate = True
        If Not bFlagValidate AndAlso Me.FNHSysSeasonId.Text.Trim <> "" Then bFlagValidate = True

        If Not (bFlagValidate) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return bFlagValidate

    End Function

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmrefresh.Click



        _StateCheckPOWaitting = False
        'Dim _Theard As New Thread(AddressOf CheckWaiting)
        '_Theard.Start()

        'Me.otbmain.SelectedTabPageIndex = 0

        Select Case Me.otbmain.SelectedTabPageIndex
            Case 0
                If Not PROC_VALIDATEbSHOWBROWSEDATA() = True Then Exit Sub
                Call RefreshGrid()
            Case 1

                Dim _Spls As New HI.TL.SplashScreen("Loading....data,Please wait.")

                Call CheckWaiting()

                _Spls.Close()
        End Select
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub



    Private Sub RefreshGrid()
        Dim _Str As String = ""
        Dim _Spls As New HI.TL.SplashScreen("Loading....data,Please wait.")

        Try

            ogcdirector.DataSource = Nothing

            If FNHSysBuyId.Text <> "" Then
                _Str = "SELECT TOP 1 FNHSysBuyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy WITH(NOLOCK) WHERE FTBuyCode ='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' "
                FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
            End If

            Try
                Call LoadOrderListingInfoToDirector(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text.Trim, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
            Catch ex As Exception
            End Try

        Catch ex As Exception
        End Try

        _Spls.Close()

    End Sub

#End Region

#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(ByVal value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(ByVal value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(ByVal value As String)
            _CallMethodParm = value
        End Set
    End Property

    Private Shared _DTApprovedProduction As System.Data.DataTable = Nothing '...list user in team group production by order company
    Private Shared ReadOnly Property LoadOrderApprovedProduction(ByVal paramFTOrderNo As String) As System.Data.DataTable
        Get
            _DTApprovedProduction = Nothing

            Dim sSQL As String
            sSQL = ""
            REM 20104/12/03
            'sSQL = "DECLARE @FTOrderNo AS NVARCHAR(30);"
            'sSQL &= Environment.NewLine & "DECLARE @FDAppDate AS NVARCHAR(10);"
            'sSQL &= Environment.NewLine & "DECLARE @FTAppTime AS NVARCHAR(10);"
            'sSQL &= Environment.NewLine & "SET @FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "';"
            'sSQL &= Environment.NewLine & "SELECT @FDAppDate = CONVERT(VARCHAR(10), CAST(A.FDAppDate AS DATE), 103), @FTAppTime = A.FTAppTime FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS A (NOLOCK) WHERE A.FTOrderNo = @FTOrderNo;"
            'sSQL &= Environment.NewLine & "SELECT A.FTUserName, A.FTUserDescriptionEN, A.FTUserDescriptionTH, A.FNHSysTeamGrpId, @FDAppDate AS FDAppDate, @FTAppTime AS FTAppTime"
            'sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEUserLogin AS A (NOLOCK)"
            'sSQL &= Environment.NewLine & "WHERE EXISTS (SELECT 'T'"
            'sSQL &= Environment.NewLine & "              FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMTeamGrp AS L1  (NOLOCK)"
            'sSQL &= Environment.NewLine & "              WHERE L1.FTStateProd = N'1'"
            'sSQL &= Environment.NewLine & "                    AND L1.FTStateActive = N'1')"
            'sSQL &= Environment.NewLine & "      AND EXISTS (SELECT 'T'"
            'sSQL &= Environment.NewLine & "                  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEPermissionCmp AS aa (NOLOCK) INNER JOIN [HITECH_SECURITY]..TSEUserLoginPermission AS bb (NOLOCK) ON aa.FNHSysPermissionID = bb.FNHSysPermissionID"
            'sSQL &= Environment.NewLine & "                  WHERE aa.FNHSysCmpId = ISNULL((SELECT TOP 1 cc.FNHSysCmpId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS cc (NOLOCK) WHERE cc.FTOrderNo = @FTOrderNo), -1)"
            'sSQL &= Environment.NewLine & "                        AND A.FTUserName = bb.FTUserName)"
            'sSQL &= Environment.NewLine & "                        AND A.FNHSysTeamGrpId > 0"
            'sSQL &= Environment.NewLine & "ORDER BY A.FTUserName ASC;"

            REM 2014/12/12
            'sSQL = "DECLARE @FTOrderNo AS NVARCHAR(30);"
            'sSQL &= Environment.NewLine & "DECLARE @FNHSysCmpId     AS INT;"
            'sSQL &= Environment.NewLine & "DECLARE @FNHSysTeamGrpId AS INT;"
            'sSQL &= Environment.NewLine & "DECLARE @FDAppDate AS NVARCHAR(10);"
            'sSQL &= Environment.NewLine & "DECLARE @FTAppTime AS NVARCHAR(10);"
            'sSQL &= Environment.NewLine & "SET @FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "';"
            'sSQL &= Environment.NewLine & "SELECT @FNHSysCmpId = A.FNHSysCmpId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS A (NOLOCK) WHERE A.FTOrderNo = @FTOrderNo;"
            'sSQL &= Environment.NewLine & "SELECT @FDAppDate = CONVERT(VARCHAR(10), CAST(A.FDAppDate AS DATE), 103), @FTAppTime = A.FTAppTime FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS A (NOLOCK) WHERE A.FTOrderNo = @FTOrderNo;"
            'sSQL &= Environment.NewLine & "IF (@FNHSysCmpId = 1309150001)"
            'sSQL &= Environment.NewLine & "  BEGIN"
            'sSQL &= Environment.NewLine & "    /*TEAM PRODUCTION COMP 70*/"
            'sSQL &= Environment.NewLine & "    SET @FNHSysTeamGrpId = 1411190001;"
            'sSQL &= Environment.NewLine & "    SELECT A.FTUserName, A.FTUserDescriptionEN, A.FTUserDescriptionTH, B.FTTeamGrpCode, B.FTTeamGrpNameEN, B.FTTeamGrpNameTH, @FDAppDate AS FDAppDate, @FTAppTime AS FTAppTime"
            'sSQL &= Environment.NewLine & "    FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEUserLogin AS A (NOLOCK) LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMTeamGrp AS B (NOLOCK) ON A.FNHSysTeamGrpId  = B.FNHSysTeamGrpId"
            'sSQL &= Environment.NewLine & "    WHERE A.FNHSysTeamGrpId = @FNHSysTeamGrpId"
            'sSQL &= Environment.NewLine & "          AND A.FTStateActive = N'1'"
            'sSQL &= Environment.NewLine & "          AND B.FTStateActive = N'1'"
            'sSQL &= Environment.NewLine & "          AND B.FTStateProd = N'1'"
            'sSQL &= Environment.NewLine & "    ORDER BY A.FTUserName ASC"
            'sSQL &= Environment.NewLine & "  END"
            'sSQL &= Environment.NewLine & "ELSE "
            'sSQL &= Environment.NewLine & "  BEGIN"
            'sSQL &= Environment.NewLine & "     IF (@FNHSysCmpId = 1306010001)"
            'sSQL &= Environment.NewLine & "        BEGIN"
            'sSQL &= Environment.NewLine & "           /*TEAM PRODUCTION COMP 91*/"
            'sSQL &= Environment.NewLine & "           SET @FNHSysTeamGrpId = 1411220001;"
            'sSQL &= Environment.NewLine & "           SELECT A.FTUserName, A.FTUserDescriptionEN, A.FTUserDescriptionTH, B.FTTeamGrpCode, B.FTTeamGrpNameEN, B.FTTeamGrpNameTH, @FDAppDate AS FDAppDate, @FTAppTime AS FTAppTime"
            'sSQL &= Environment.NewLine & "           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEUserLogin AS A (NOLOCK) LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMTeamGrp AS B (NOLOCK) ON A.FNHSysTeamGrpId  = B.FNHSysTeamGrpId"
            'sSQL &= Environment.NewLine & "           WHERE A.FNHSysTeamGrpId = @FNHSysTeamGrpId"
            'sSQL &= Environment.NewLine & "	                AND A.FTStateActive = N'1'"
            'sSQL &= Environment.NewLine & " 	            AND B.FTStateActive = N'1'"
            'sSQL &= Environment.NewLine & " 	            AND B.FTStateProd = N'1'"
            'sSQL &= Environment.NewLine & "           ORDER BY A.FTUserName ASC"
            'sSQL &= Environment.NewLine & "        END"
            'sSQL &= Environment.NewLine & "  END;"

            sSQL = "DECLARE @FTOrderNo AS NVARCHAR(30)"
            sSQL &= Environment.NewLine & "DECLARE @FNHSysCmpId AS INT;"
            sSQL &= Environment.NewLine & "DECLARE @FDAppDate AS NVARCHAR(10);"
            sSQL &= Environment.NewLine & "DECLARE @FTAppTime AS NVARCHAR(10);"
            sSQL &= Environment.NewLine & "SET @FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "'"
            sSQL &= Environment.NewLine & "SELECT @FNHSysCmpId = A.FNHSysCmpId, @FDAppDate = CONVERT(VARCHAR(10), CAST(A.FDAppDate AS DATE), 103), @FTAppTime = A.FTAppTime"
            sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS A (NOLOCK) "
            sSQL &= Environment.NewLine & "WHERE A.FTOrderNo = @FTOrderNo;"
            sSQL &= Environment.NewLine & "SELECT DISTINCT MM.FTUserName, KK.FTUserDescriptionEN, KK.FTUserDescriptionTH, @FDAppDate AS FDAppDate, @FTAppTime AS FTAppTime"
            sSQL &= Environment.NewLine & "FROM (SELECT A.FTUserName, A.FNHSysPermissionID"
            sSQL &= Environment.NewLine & "      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEUserLoginPermission AS A (NOLOCK)"
            sSQL &= Environment.NewLine & "      WHERE A.FNHSysPermissionID > 0"
            sSQL &= Environment.NewLine & "            AND EXISTS (SELECT 'T'"
            sSQL &= Environment.NewLine & "                        FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEPermissionCmp AS LL (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCmp AS NN (NOLOCK) ON LL.FNHSysCmpId = NN.FNHSysCmpId"
            sSQL &= Environment.NewLine & "                        WHERE LL.FNHSysCmpId = @FNHSysCmpId"
            sSQL &= Environment.NewLine & "  			                 AND A.FNHSysPermissionID = LL.FNHSysPermissionID"
            sSQL &= Environment.NewLine & " 		               )"
            sSQL &= Environment.NewLine & "      ) AS MM INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..TSEUserLogin AS KK (NOLOCK) ON MM.FTUserName = KK.FTUserName"
            sSQL &= Environment.NewLine & "              LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMTeamGrp AS PP (NOLOCK) ON KK.FNHSysTeamGrpId = PP.FNHSysTeamGrpId"
            sSQL &= Environment.NewLine & "WHERE KK.FTStateActive = N'1'"

            'sSQL &= Environment.NewLine & "      AND EXISTS (SELECT 'T'"
            'sSQL &= Environment.NewLine & "                  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMTeamGrp AS L1 (NOLOCK)"
            'sSQL &= Environment.NewLine & " 	             WHERE KK.FTUserName = L1.FTUserName"
            'sSQL &= Environment.NewLine & " 	                   AND L1.FTStateActive = N'1'"
            'sSQL &= Environment.NewLine & "			               AND L1.FTStateProd = N'1'"
            'sSQL &= Environment.NewLine & " 	             )"
            'sSQL &= Environment.NewLine & "      AND KK.FNHSysTeamGrpId > 0"

            'sSQL &= Environment.NewLine & "      AND EXISTS (SELECT 'T'"
            'sSQL &= Environment.NewLine & "                  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMTeamGrp AS L1 (NOLOCK)"
            'sSQL &= Environment.NewLine & " 	             WHERE KK.FNHSysTeamGrpId = L1.FNHSysTeamGrpId"
            'sSQL &= Environment.NewLine & "                        KK.FTUserName = L1.FTUserName"
            'sSQL &= Environment.NewLine & "                        AND L1.FTStateActive = N '1'"
            'sSQL &= Environment.NewLine & "			               AND L1.FTStateProd = N'1'"
            'sSQL &= Environment.NewLine & " 	             )"

            sSQL &= Environment.NewLine & "      AND ISNULL(KK.FNHSysTeamGrpId, 0) > 0"
            sSQL &= Environment.NewLine & "      AND PP.FTStateActive = N'1'"
            sSQL &= Environment.NewLine & "      AND PP.FTStateProd = N'1'"

            REM 2015/02/26 sSQL &= Environment.NewLine & "ORDER BY KK.FTUserName ASC;"

            _DTApprovedProduction = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_SECURITY)

            Return _DTApprovedProduction

        End Get

    End Property

#End Region

#Region "Initial Grid"

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = "FTPOref"
        Dim sFieldSum As String = "FNQuantity"
        Dim sFieldSumAmt As String = ""


        Dim sFieldGrpCount As String = "FTPOref"
        Dim sFieldGrpSum As String = "FNQuantity"
        Dim sFieldGrpSumAmt As String = ""

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogvpowaiting

            .ClearGrouping()
            .ClearDocument()

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
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSumAmt.Split("|")
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
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSumAmt.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            .OptionsView.ShowAutoFilterRow = False
            .ExpandAllGroups()
            .RefreshData()

        End With


        '------End Add Summary Grid-------------
    End Sub

#End Region

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click

        Try

            Dim _Fm As String = ""
            _Fm = "{TACCTFactoryCMInvoice.FTCustomerPO}='" & HI.UL.ULF.rpQuoted("" & Me.ogvdirector.GetFocusedRowCellValue("FTCustomerPO").ToString) & "' "
            _Fm &= " And {TACCTFactoryCMInvoice.FTInvoiceNo}='" & HI.UL.ULF.rpQuoted("" & Me.ogvdirector.GetFocusedRowCellValue("FTInvoiceNo").ToString) & "' "

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Account\"
                .ReportName = "ReportInvoiceCm.rpt"
                .Formular = _Fm
                .Preview()
            End With

        Catch ex As Exception
        End Try

    End Sub


    Private Sub ogvdirector_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvdirector.RowCellStyle

        Try
            With Me.ogvdirector
                If "" & .GetRowCellValue(e.RowHandle, "FTStateReject").ToString = "1" Then
                    e.Appearance.ForeColor = Color.Orange
                ElseIf "" & .GetRowCellValue(e.RowHandle, "FTStateApp").ToString = "1" Then
                    e.Appearance.ForeColor = Color.Green
                ElseIf "" & .GetRowCellValue(e.RowHandle, "FTStateSendApp").ToString = "1" Then
                    e.Appearance.ForeColor = Color.Blue
                End If
            End With
        Catch ex As Exception
        End Try

        Try
            With Me.ogvdirector
                If "" & .GetRowCellValue(e.RowHandle, "FTStateWHApp").ToString = "1" Then
                    e.Appearance.ForeColor = Color.Green
                ElseIf "" & .GetRowCellValue(e.RowHandle, "FTStateWHReject").ToString = "1" Then
                    e.Appearance.ForeColor = Color.Orange
                End If
            End With
        Catch ex As Exception
        End Try


        Try
            With Me.ogvdirector
                Select Case e.Column.FieldName
                    Case "FTCustomerPO"
                        If Val("" & .GetRowCellValue(e.RowHandle, "FNStateImportNetPrice").ToString) > 0 Then

                            e.Appearance.ForeColor = Color.Red
                            e.Appearance.Font = New Font("tahoma", 8, FontStyle.Bold)

                            'Dim cmdstring As String = "SELECT TOP 1 isnull(FNNetPrice,-1) as  FNNetPrice "
                            'cmdstring &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination "
                            'cmdstring &= vbCrLf & " WHERE FTPOref='" & HI.UL.ULF.rpQuoted(.GetRowCellValue(e.RowHandle, e.Column.FieldName.ToString)) & "'"
                            'cmdstring &= vbCrLf & " and  FNNetPrice is null "
                            ''cmdstring &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(.GetRowCellValue(e.RowHandle, "FTColorway").ToString) & "'"
                            ''cmdstring &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(e.Column.Caption) & "'"
                            ''cmdstring &= vbCrLf & " AND FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(.GetRowCellValue(e.RowHandle, "FTNikePOLineItem").ToString) & "'"
                            'Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)
                            'If _oDt.Rows.Count > 0 Then
                            '    Dim netprice As Decimal = _oDt.Rows(0).Item("FNNetPrice")  '(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN, "0"))
                            '    If netprice <= 0 Then
                            '        e.Appearance.ForeColor = Color.Red
                            '        e.Appearance.Font = New Font("tahoma", 8, FontStyle.Bold)
                            '    End If
                            'End If


                        Else

                        End If

                End Select

            End With
        Catch ex As Exception

        End Try

    End Sub


    Private Delegate Sub DelegateCheckWaiting()
    Private Sub CheckWaiting()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckWaiting(AddressOf CheckWaiting), New Object() {})
        Else

            Dim _Qry As String
            Dim _LangDisplay As String = "TH"
            Dim dt As System.Data.DataTable

            If HI.ST.Lang.Language <> ST.Lang.eLang.TH Then
                _LangDisplay = "EN"
            End If

            _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].[dbo].[SP_GET_CUSTOMER_PO_WAITING] '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(_LangDisplay) & "'"
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)
            Me.ogcpowaiting.DataSource = dt.Copy


            _StateCheckPOWaitting = True
        End If
    End Sub

    Private Sub ocmcheckapp_Tick(sender As Object, e As EventArgs) Handles ocmcheckpowaiting.Tick
        'If (_StateCheckPOWaitting) Then
        '    _StateCheckPOWaitting = False
        '    Dim _Theard As New Thread(AddressOf CheckWaiting)
        '    _Theard.Start()
        'End If
    End Sub

    Private Sub ocmpreviewtvwthb_Click(sender As Object, e As EventArgs) Handles ocmpreviewtvwthb.Click
        Try

            Dim _Fm As String = ""
            _Fm = "{TACCTFactoryCMInvoice.FTCustomerPO}='" & HI.UL.ULF.rpQuoted("" & Me.ogvdirector.GetFocusedRowCellValue("FTCustomerPO").ToString) & "' "
            _Fm &= " And {TACCTFactoryCMInvoice.FTInvoiceNo}='" & HI.UL.ULF.rpQuoted("" & Me.ogvdirector.GetFocusedRowCellValue("FTInvoiceNo").ToString) & "' "

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Account\"
                .ReportName = "ReportInvoiceCm_THB.rpt"
                .Formular = _Fm
                .Preview()
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvpowaiting_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvpowaiting.RowCellStyle
        Try
            With Me.ogvpowaiting
                Select Case e.Column.FieldName
                    Case "FTPOref"
                        If Val("" & .GetRowCellValue(e.RowHandle, "FNStateImportNetPrice").ToString) > 0 Then
                            e.Appearance.ForeColor = Color.Red
                            e.Appearance.Font = New Font("tahoma", 8, FontStyle.Bold)
                            'Dim cmdstring As String = "SELECT TOP 1 isnull(FNNetPrice,-1) as  FNNetPrice "
                            'cmdstring &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination "
                            'cmdstring &= vbCrLf & " WHERE FTPOref='" & HI.UL.ULF.rpQuoted(.GetRowCellValue(e.RowHandle, e.Column.FieldName.ToString)) & "'"
                            'cmdstring &= vbCrLf & " and  FNNetPrice is null "
                            ''cmdstring &= vbCrLf & " AND FTColorway='" & HI.UL.ULF.rpQuoted(.GetRowCellValue(e.RowHandle, "FTColorway").ToString) & "'"
                            ''cmdstring &= vbCrLf & " AND FTSizeBreakDown='" & HI.UL.ULF.rpQuoted(e.Column.Caption) & "'"
                            ''cmdstring &= vbCrLf & " AND FTNikePOLineItem='" & HI.UL.ULF.rpQuoted(.GetRowCellValue(e.RowHandle, "FTNikePOLineItem").ToString) & "'"
                            'Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)
                            'If _oDt.Rows.Count > 0 Then
                            '    Dim netprice As Decimal = _oDt.Rows(0).Item("FNNetPrice")  '(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN, "0"))
                            '    If netprice <= 0 Then
                            '        e.Appearance.ForeColor = Color.Red
                            '        e.Appearance.Font = New Font("tahoma", 8, FontStyle.Bold)
                            '    End If
                            'End If


                        Else

                        End If

                End Select

            End With
        Catch ex As Exception

        End Try
    End Sub
End Class