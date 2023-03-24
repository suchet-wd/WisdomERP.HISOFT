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

Public Class wOrderChangeFactoryCM


    Dim View As GridView
    Dim RowsIndex As Double
    Dim TopVisibleIndex As Int32
    Private sFNHSysStyleId As String

    ''' Used Data Adapter to control database

    Dim oleDbDataAdapter1 As DbDataAdapter
    Dim oleDbDataAdapter2 As DbDataAdapter
    Dim dtStyleDetail As DataTable

    Dim saveFileDialog As SaveFileDialog = New SaveFileDialog()
    Dim selectedFile As String = ""
    Dim lastSelectedPath As String = ""
    Dim sectionName As String = "appSettings"
    Dim newKey As String = "KeepLastBrowseFolder"

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


        Catch ex As Exception
        End Try

        FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

    End Sub

    Private Sub Proc_Close(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
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


    Private Sub LoadOrderListingInfoChangeFactory(ByVal _FNHSysCmpId As String, ByVal _FTPORef As String, ByVal _FNHSysStyleId As String, ByVal _FNHSysCustId As String, ByVal _FNHSysBuyId As String, ByVal _FNHSysSeasonId As String)
        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Try

            If _FNHSysCmpId.Trim() = "" Then _FNHSysCmpId = "0"
            If _FNHSysStyleId.Trim() = "" Then _FNHSysStyleId = "0"
            If _FNHSysCustId.Trim() = "" Then _FNHSysCustId = "0"
            If _FNHSysBuyId.Trim() = "" Then _FNHSysBuyId = "0"
            If _FNHSysSeasonId.Trim() = "" Then _FNHSysSeasonId = "0"

            Dim sqlCmd As New SqlCommand
            sqlCmd.Connection = HI.Conn.SQLConn.Cnn
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_ORDER_LISTING_CHANGE_FACTORY_CM]"
            sqlCmd.Parameters.AddWithValue("@FNHSysCmpId", Val(HI.ST.SysInfo.CmpID))
            sqlCmd.Parameters.AddWithValue("@FTPORef", _FTPORef)
            sqlCmd.Parameters.AddWithValue("@FNHSysStyleId", _FNHSysStyleId)
            sqlCmd.Parameters.AddWithValue("@FNHSysCustId", _FNHSysCustId)
            sqlCmd.Parameters.AddWithValue("@FNHSysBuyId", _FNHSysBuyId)
            sqlCmd.Parameters.AddWithValue("@FNHSysSeasonId", _FNHSysSeasonId)
            sqlCmd.Parameters.AddWithValue("@LANGID", HI.ST.Lang.Language.ToString())
            sqlCmd.Parameters.AddWithValue("@FTUserLogin", HI.ST.UserInfo.UserName)

            Dim sqlDA As New SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
            sqlDA.SelectCommand = sqlCmd
            Dim dt As New DataTable
            sqlDA.Fill(dt)

            Me.ogcdirector.DataSource = dt

            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            'If Inited = False Then
            '    InitGrid()
            'End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ocmclear1_Click(sender As System.Object, e As System.EventArgs) Handles ocmclearclsr.Click

        ogcdirector.DataSource = Nothing
        Dim xCol As Integer = 0
        Dim Idx As Integer = 0
        Try
            HI.TL.HandlerControl.ClearControl(Me)
            FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
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

        If Not (bFlagValidate) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return bFlagValidate

    End Function

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs)
        If Not PROC_VALIDATEbSHOWBROWSEDATA() = True Then Exit Sub
        Call RefreshGrid()
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Sub RefreshGrid()
        Dim _Str As String = ""
        Dim _Spls As New HI.TL.SplashScreen("Loading....data,Please wait.")

        ogcdirector.DataSource = Nothing
        ockselectallsenddirect.Checked = False


        If FNHSysBuyId.Text <> "" Then
            _Str = "SELECT TOP 1 FNHSysBuyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy WITH(NOLOCK) WHERE FTBuyCode ='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' "
            FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
        End If

        Call LoadOrderListingInfoChangeFactory(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text.Trim, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        _Spls.Close()

    End Sub

    Private Function Savedata() As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Changing.... Factory  , Please wait. ")
        Try
            Dim _dt As DataTable
            With CType(Me.ogcdirector.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy
            End With

            Dim _Qry As String = ""
            _Qry = "SELECT TOP 1 FNHSysCmpId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS A WITH(NOLOCK) WHERE FTCmpCode='" & HI.UL.ULF.rpQuoted(FNHSysCmpIdTo.Text) & "'"
            FNHSysCmpIdTo.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "0")

            For Each R As DataRow In _dt.Select("FTSelect ='1'")

                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Change_Factory_CM"
                _Qry &= vbCrLf & "("
                _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime,  FTOrderNo, FNSeq, FNHSysCmpId, FNHSysCmpIdTo,FTRemark"
                _Qry &= vbCrLf & ")"
                _Qry &= vbCrLf & " SELECT "
                _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'"
                _Qry &= vbCrLf & " ,ISNULL(("
                _Qry &= vbCrLf & " SELECT TOP 1 FNSeq "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Change_Factory_CM"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'"
                _Qry &= vbCrLf & " ORDER BY FNSeq DESC"
                _Qry &= vbCrLf & " ),0) + 1 "
                _Qry &= vbCrLf & " ," & Val(R!FNHSysCmpId.ToString) & ""
                _Qry &= vbCrLf & " ," & Val(FNHSysCmpIdTo.Properties.Tag.ToString()) & ""
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "'"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                _Qry = " UPDATE A SET FNHSysCmpId=" & Val(FNHSysCmpIdTo.Properties.Tag.ToString()) & ""
                _Qry &= vbCrLf & " ,FTChangeCmpBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ,FDChangeCmpDate=" & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & " ,FTChangeCmpTime=" & HI.UL.ULDate.FormatTimeDB

                '_Qry &= vbCrLf & " ,FTStateSendDirectorApp='1'"
                '_Qry &= vbCrLf & " ,FTStateSendDirectorBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                '_Qry &= vbCrLf & " ,FDStateSendDirectorDate=" & HI.UL.ULDate.FormatDateDB
                '_Qry &= vbCrLf & " ,FTStateSendDirectorTime=" & HI.UL.ULDate.FormatTimeDB
                '_Qry &= vbCrLf & " ,FTStateFactoryApp='0'"
                '_Qry &= vbCrLf & " ,FTStateFactoryAppBy=''"
                '_Qry &= vbCrLf & " ,FTStateFactoryReject='0'"
                '_Qry &= vbCrLf & " ,FTStateFactoryRejectBy=''"


                _Qry &= vbCrLf & " ,FNHSysCmpOwnerId=" & Val(FNHSysCmpId.Properties.Tag.ToString()) & ""
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'"
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Next

        Catch ex As Exception

            _Spls.Close()
            Return False

        End Try

        _Spls.Close()
        Return True
    End Function
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

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        With CType(Me.ogcdirector.DataSource, DataTable)
            .AcceptChanges()

            If .Select("FTSelect='1'").Length <= 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกรายการที่ต้องการทำการเปลี่ยน โรงงาน !!!", 1507080945, Me.Text, , MessageBoxIcon.Warning)
                Exit Sub
            End If
        End With

        If Me.FNHSysCmpIdTo.Text <> "" And Val(Me.FNHSysCmpIdTo.Properties.Tag.ToString()) > 0 Then
            If Me.FTRemark.Text <> "" Then
                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ เปลี่ยน โรงงานผลิต ใช่หรือไม่ !!!", 1507080941) Then
                    If Savedata() Then
                        HI.MG.ShowMsg.mInfo("Change Factory Complete !!!", 1507080946, Me.Text, , MessageBoxIcon.Information)
                        Call RefreshGrid()
                        Me.FNHSysCmpIdTo.Text = ""
                        Me.FTRemark.Text = ""
                    End If
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FTRemark_lbl.Text)
                FTRemark.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysCmpIdTo_lbl.Text)
            FNHSysCmpIdTo.Focus()
        End If
    End Sub

    Private Sub ockselectallsenddirect_CheckedChanged(sender As Object, e As EventArgs) Handles ockselectallsenddirect.CheckedChanged
        Try
            Dim _State As String = "0"
            If Me.ockselectallsenddirect.Checked Then
                _State = "1"
            End If

            With ogcdirector
                If Not (.DataSource Is Nothing) And ogvdirector.RowCount > 0 Then

                    With ogvdirector
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        If Not PROC_VALIDATEbSHOWBROWSEDATA() = True Then Exit Sub
        Call RefreshGrid()
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        With Me.ogvdirector
            If .RowCount <= 0 Then Exit Sub
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

            Dim _PoNo As String = "" & .GetFocusedRowCellValue("FTOrderNo").ToString
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Merchandise Report\"
                .ReportName = "FactoryPO.rpt"
                .Formular = "{TMERTOrder.FTOrderNo}='" & HI.UL.ULF.rpQuoted(_PoNo) & "' "
                .Preview()
            End With
        End With
    End Sub
End Class