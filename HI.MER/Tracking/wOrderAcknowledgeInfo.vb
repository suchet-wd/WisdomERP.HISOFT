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
Imports DevExpress.XtraEditors.Controls

Public Class wOrderAcknowledgeInfo


    Dim View As GridView
    Dim RowsIndex As Double
    Dim TopVisibleIndex As Int32
    Private sFNHSysStyleId As String

    ''' Used Data Adapter to control database

    Dim oleDbDataAdapter1 As DbDataAdapter
    Dim oleDbDataAdapter2 As DbDataAdapter
    Dim dtStyleDetail As DataTable

    Dim AppConfig As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
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

            View = New GridView()
            View = ogcmatcode.Views(0)
            View.FocusedRowHandle = 0

            ogcmatcode.ViewCollection.Add(View)
            ogcmatcode.MainView = View

            View.GridControl = ogcmatcode
            View.OptionsView.ShowAutoFilterRow = False
            View.OptionsView.NewItemRowPosition = NewItemRowPosition.None
            View.OptionsNavigation.AutoFocusNewRow = True
            View.OptionsBehavior.AllowAddRows = True
            View.OptionsBehavior.AllowDeleteRows = True
            View.OptionsBehavior.Editable = True
            View.BestFitColumns()

            sFNHSysStyleId = ""

            Dim section As System.Configuration.AppSettingsSection =
                DirectCast(AppConfig.GetSection(sectionName),
                    System.Configuration.AppSettingsSection)
            For Each setting As System.Configuration.SettingElement In section.Settings
                Dim value As String = setting.Value.ValueXml.InnerText
                Dim name As String = setting.Name
                If name.ToLower = sectionName.ToLower Then
                    lastSelectedPath = value
                    Return
                End If
            Next

            'lastSelectedPath =""

        Catch ex As Exception
            lastSelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        End Try

        If (lastSelectedPath <> "") Then

            saveFileDialog.InitialDirectory = lastSelectedPath

        Else

            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

        End If

        saveFileDialog.ValidateNames = True
        saveFileDialog.DereferenceLinks = False     ' Will return .lnk in shortcuts.
        saveFileDialog.Filter = "Excel |*.xlsx"
        Call TabChange()
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

    Private Function GetPropertyTagValue_BACKUP_20150106_1743(ByVal obj As Object) As Control
        Dim _Str As String = ""
        Dim sTable As String = ""
        Dim sField As String = ""
        Dim sValue As String = ""
        Dim sCrite As String = ""

        Select Case obj.GetType.FullName.ToString.ToUpper
            Case "DevExpress.XtraEditors.ButtonEdit".ToUpper
                If CType(obj, DevExpress.XtraEditors.ButtonEdit).Properties.Tag = "" Then
                    Select Case obj.GetType.FullName.ToUpper
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

    Private Sub LoadOrderListingInfo(ByVal _FNHSysCmpId As String, ByVal _FTPORef As String, ByVal _FNHSysStyleId As String, ByVal _FNHSysCustId As String, ByVal _FNHSysBuyId As String, ByVal _FNHSysSeasonId As String)
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
            sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_ORDER_LISTING_APPROVE_INFO]"
            sqlCmd.Parameters.AddWithValue("@FNHSysCmpId", _FNHSysCmpId)
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

            Me.ogcmatcode.DataSource = dt

            'Dim view As GridView
            'view = ogcmatcode.Views(0)
            'view.OptionsView.ShowAutoFilterRow = True
            ogvapprove.BestFitColumns()

            'Me.ogcmatcode = view.GridControl
            Me.ogcmatcode.Refresh()

            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            'If Inited = False Then
            '    InitGrid()
            'End If

        Catch ex As Exception

        End Try

    End Sub

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

            Dim sqlCmd As New SqlCommand
            sqlCmd.Connection = HI.Conn.SQLConn.Cnn
            sqlCmd.CommandTimeout = 0
            sqlCmd.CommandType = CommandType.StoredProcedure
            sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_ORDER_LISTING_SENDDIRECTOR_INFO]"
            sqlCmd.Parameters.AddWithValue("@FNHSysCmpId", _FNHSysCmpId)
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


    Private Sub ocmsave_Click_REM_20141126(sender As System.Object, e As System.EventArgs)

        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

        Dim CountApproved As Integer = 0
        Dim CountCancel As Integer = 0
        Dim CountError As Integer = 0
        Dim CountNone As Integer = 0

        RetMessage = ""

        Try

            Dim dt As DataTable = CType(ogcmatcode.DataSource, DataTable)

            For Each r As DataRow In dt.Rows

                Dim sqlCmd As New SqlCommand

                sqlCmd.Connection = HI.Conn.SQLConn.Cnn
                sqlCmd.CommandType = CommandType.StoredProcedure
                sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_UPDATE_ORDER_APPROVE_STATUS]"
                sqlCmd.Parameters.Clear()
                sqlCmd.Parameters.AddWithValue("@FNHSysCmpId", r!FNHSysCmpId)
                sqlCmd.Parameters.AddWithValue("@FTOrderNo", r!FTOrderNo)
                sqlCmd.Parameters.AddWithValue("@FTStateOrderApp", r!FTStateOrderApp)
                sqlCmd.Parameters.AddWithValue("@UserName", HI.ST.UserInfo.UserName)
                sqlCmd.Parameters.AddWithValue("@LANGID", HI.ST.Lang.Language.ToString())
                sqlCmd.Parameters.AddWithValue("@RetMsg", "").Direction = ParameterDirection.Output

                Dim sqlDA As New SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
                sqlDA.SelectCommand = sqlCmd
                sqlDA.SelectCommand.ExecuteNonQuery()

                If (sqlCmd.Parameters("@RetMsg").Value.ToString() = "A") Then
                    'RetMessage += "ยกเลิกการอนุมัติ Order No. " + r!FTOrderNo.ToString + vbCrLf
                    CountApproved += 1



                ElseIf (sqlCmd.Parameters("@RetMsg").Value.ToString() = "D") Then
                    'RetMessage += "อนุมัติ Order No. " + r!FTOrderNo.ToString + vbCrLf
                    CountCancel += 1
                ElseIf (sqlCmd.Parameters("@RetMsg").Value.ToString() = "E") Then
                    CountError += 1
                Else
                    CountNone += 1
                End If

                HI.Conn.SQLConn.DisposeSqlConnection(sqlCmd)

            Next

            RetMessage = String.Format("ยกเลิกการอนุมัติ Order จำนวน {0} รายการ" & vbCrLf _
                        + "อนุมัติ Order จำนวน {1} รายการ" & vbCrLf _
                        + "ไม่สามารถทำรายการได้ จำนวน {2} รายการ", CountCancel, CountApproved, CountError)

            MsgBox(RetMessage, vbOKOnly)

            Call RefreshGrid()

        Catch ex As Exception
            'If System.Diagnostics.Debugger.IsAttached = True Then
            '    MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            'End If
        End Try

    End Sub

    Private Sub ocmclear1_Click(sender As System.Object, e As System.EventArgs) Handles ocmclearclsr.Click
        Me.ogcmatcode.DataSource = Nothing
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

        If Not (bFlagValidate) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return bFlagValidate

    End Function

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmrefresh.Click
        If Not PROC_VALIDATEbSHOWBROWSEDATA() = True Then Exit Sub
        Call RefreshGrid()
    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ChkSelectAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ChkSelectAll.CheckedChanged

        Try

            If Not (ogcmatcode.DataSource Is Nothing) Then
                Dim dtSelectAll As DataTable = CType(ogcmatcode.DataSource, DataTable)

                For Each r As DataRow In dtSelectAll.Rows
                    r!FTStateOrderApp = IIf(ChkSelectAll.EditValue = True, 1, 0)
                Next
            End If

        Catch ex As Exception

        End Try


    End Sub

    Private Sub RefreshGrid()
        Dim _Str As String = ""
        Dim _Spls As New HI.TL.SplashScreen("Loading....data,Please wait.")

        Try
            ogcmatcode.DataSource = Nothing
            ogcdirector.DataSource = Nothing

            Try
                ockselectallsenddirect.Checked = False
                ChkSelectAll.Checked = False
            Catch ex As Exception
            End Try


            If FNHSysBuyId.Text <> "" Then
                _Str = "SELECT TOP 1 FNHSysBuyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy WITH(NOLOCK) WHERE FTBuyCode ='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' "
                FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
            End If

            Try
                Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text.Trim, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
            Catch ex As Exception

            End Try

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
        Dim ret As Boolean = Inited
        If (Inited = True) Then
            Return
        End If
        Try
            Dim bandedView As GridView = ogvapprove
            bandedView.ClearGrouping()
            bandedView.ClearDocument()

            bandedView.Columns("FTCustCode").SortIndex = 0
            bandedView.Columns("FTCustCode").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            bandedView.Columns("FTPORef").SortIndex = 1
            bandedView.Columns("FTPORef").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            ' Make the group footers always visible.
            bandedView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways
            bandedView.Columns("FTCustCode").Group()
            bandedView.Columns("FTPORef").Group()

            ' Create and setup the 1 summary item.
            Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem
            item1.FieldName = "FTCustCode"
            item1.SummaryType = DevExpress.Data.SummaryItemType.None
            item1.DisplayFormat = "{0}"
            item1.ShowInGroupColumnFooter = bandedView.Columns("FTCustCode")
            bandedView.GroupSummary.Add(item1)

            ' Create and setup the 2 summary item.
            Dim item2 As GridGroupSummaryItem = New GridGroupSummaryItem
            item2.FieldName = "FTPORef"
            item2.SummaryType = DevExpress.Data.SummaryItemType.None
            item2.DisplayFormat = "{0}"
            item2.ShowInGroupColumnFooter = bandedView.Columns("FTPORef")
            bandedView.GroupSummary.Add(item2)

            ' Create and setup the 3 summary item.
            Dim item3 As GridGroupSummaryItem = New GridGroupSummaryItem
            item3.FieldName = "FNQuantity"
            item3.SummaryType = DevExpress.Data.SummaryItemType.Sum
            item3.DisplayFormat = "{0:n0}"
            item3.ShowInGroupColumnFooter = bandedView.Columns("FNQuantity")
            bandedView.GroupSummary.Add(item3)

            ' Create and setup the 4 summary item.
            Dim item4 As GridGroupSummaryItem = New GridGroupSummaryItem
            item4.FieldName = "FNExtraQuantity"
            item4.SummaryType = DevExpress.Data.SummaryItemType.Sum
            item4.DisplayFormat = "{0:n0}"
            item4.ShowInGroupColumnFooter = bandedView.Columns("FNExtraQuantity")
            bandedView.GroupSummary.Add(item4)

            ' Create and setup the 4 summary item.
            Dim item5 As GridGroupSummaryItem = New GridGroupSummaryItem
            item5.FieldName = "FNTotalQuantity"
            item5.SummaryType = DevExpress.Data.SummaryItemType.Sum
            item5.DisplayFormat = "{0:n0}"
            item5.ShowInGroupColumnFooter = bandedView.Columns("FNTotalQuantity")
            bandedView.GroupSummary.Add(item5)


            bandedView.Columns("FTCustCode").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTCustCode")
            bandedView.Columns("FTPORef").Summary.Add(DevExpress.Data.SummaryItemType.None, "FTPORef")
            bandedView.Columns("FNQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNQuantity")
            bandedView.Columns("FNExtraQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNExtraQuantity")
            bandedView.Columns("FNTotalQuantity").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNTotalQuantity")

            bandedView.Columns("FNQuantity").SummaryItem.DisplayFormat = "{0:n0}"
            bandedView.Columns("FNExtraQuantity").SummaryItem.DisplayFormat = "{0:n0}"
            bandedView.Columns("FNTotalQuantity").SummaryItem.DisplayFormat = "{0:n0}"

            bandedView.GroupFooterShowMode = GroupFooterShowMode.VisibleIfExpanded
            bandedView.ExpandAllGroups()
            bandedView.BestFitColumns()
            bandedView.RefreshData()
            Inited = True

        Catch ex As System.Exception
            Inited = False
        End Try
    End Sub

#End Region


    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Select Case otbmain.SelectedTabPage.Name
            Case otpapprove.Name

                With Me.ogvapprove
                    If .RowCount <= 0 Then Exit Sub
                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub

                    Dim _FTOrderNo As String = "" & .GetFocusedRowCellValue("FTOrderNo").ToString

                    Dim _from As New wReportMERFactoryOrderNo(_FTOrderNo)
                    HI.TL.HandlerControl.AddHandlerObj(_from)
                    With _from
                        Dim oSysLang As New HI.ST.SysLanguage

                        Try
                            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _from.Name.ToString.Trim, _from)
                        Catch ex As Exception
                        End Try

                        Call HI.ST.Lang.SP_SETxLanguage(_from)

                        .ShowDialog()

                    End With

                End With

            Case otpsenddirector.Name
                With CType(Me.ogcdirector.DataSource, DataTable)
                    .AcceptChanges()
                    If .Select("FTSelect='1'").Length <= 0 Then
                        HI.MG.ShowMsg.mInfo("กรุณาระบุข้องมูล ที่ต้องการทำการ Preview", 1507060584, Me.Text, , MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    'If .Select("FTSelect='1' AND FTStateDirectorApp='1'").Length <= 0 Then
                    '    HI.MG.ShowMsg.mInfo("กรุณาระบุข้องมูล ที่ Director ทำการ Approve แล้ว ที่ต้องการทำการ Preview", 1507060586, Me.Text, , MessageBoxIcon.Warning)
                    '    Exit Sub
                    'End If

                    'For Each R As DataRow In .Select("FTSelect='1' ")

                    '    With New HI.RP.Report
                    '        .FormTitle = Me.Text
                    '        .ReportFolderName = "Merchandise Report\"
                    '        .ReportName = "FactoryPO.rpt"
                    '        .Formular = "{TMERTOrder.FTOrderNo}='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                    '        .Preview()
                    '    End With
                    'Next

                    Dim dt As DataTable = .Copy

                    Dim grp As List(Of String) = (dt.Select("FTSelect='1'", "FTOrderNo").CopyToDataTable).AsEnumerable() _
                                                  .Select(Function(r) r.Field(Of String)("FTOrderNo")) _
                                                  .Distinct() _
                                                  .ToList()


                    For Each orderno As String In grp
                        With New HI.RP.Report
                            .FormTitle = Me.Text
                            .ReportFolderName = "Merchandise Report\"
                            .ReportName = "FactoryPO.rpt"
                            .Formular = "{TMERTOrder.FTOrderNo}='" & HI.UL.ULF.rpQuoted(orderno) & "' "
                            .Preview()
                        End With
                    Next

                End With
        End Select

    End Sub


    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Dim CountApproved As Integer = 0
        Dim CountCancel As Integer = 0
        Dim CountError As Integer = 0
        Dim CountNone As Integer = 0

        Dim tSubjectMail As String
        Dim tStrMailFrom As String

        Dim tmpDTApproveProduction As System.Data.DataTable
        Dim tmpDTOrderApprove As System.Data.DataTable
        Dim numCntWriteMail As Integer = 0

        Dim tmpDTRejectApproveProduction As System.Data.DataTable
        Dim tmpDTOrderRejectApprove As System.Data.DataTable
        Dim numCntRejectMaill As Integer = 0

        RetMessage = ""

        Try
            tStrMailFrom = ""
            tStrMailFrom = HI.ST.UserInfo.UserName

            '...Cache Mail Approved
            '=====================================================================================================================================
            tmpDTOrderApprove = New System.Data.DataTable

            Dim oColFTOrderApprove As System.Data.DataColumn
            oColFTOrderApprove = New System.Data.DataColumn("FTOrderApprove", System.Type.GetType("System.String"))
            oColFTOrderApprove.Caption = "FTOrderApprove"
            tmpDTOrderApprove.Columns.Add(oColFTOrderApprove.ColumnName, oColFTOrderApprove.DataType)

            Dim oColFTOrderApproveSendMailTo As System.Data.DataColumn
            oColFTOrderApproveSendMailTo = New System.Data.DataColumn("FTOrerApproveSendMailTo", System.Type.GetType("System.String"))
            oColFTOrderApproveSendMailTo.Caption = "FTOrderApproveSendMailTo"
            tmpDTOrderApprove.Columns.Add(oColFTOrderApproveSendMailTo.ColumnName, oColFTOrderApproveSendMailTo.DataType)

            Dim oColFTOrderMailMsg As System.Data.DataColumn
            oColFTOrderMailMsg = New System.Data.DataColumn("FTOrderMailMsg", System.Type.GetType("System.String"))
            oColFTOrderMailMsg.Caption = "FTOrderMailMsg"
            tmpDTOrderApprove.Columns.Add(oColFTOrderMailMsg.ColumnName, oColFTOrderApprove.DataType)
            '=====================================================================================================================================

            '...Cache Mail Reject Approved
            '=====================================================================================================================================
            tmpDTOrderRejectApprove = New System.Data.DataTable

            Dim oColFTOrderRejectApprove As System.Data.DataColumn
            oColFTOrderRejectApprove = New System.Data.DataColumn("FTOrderRejectApprove", System.Type.GetType("System.String"))
            oColFTOrderRejectApprove.Caption = "FTOrderRejectApprove"
            tmpDTOrderRejectApprove.Columns.Add(oColFTOrderRejectApprove.ColumnName, oColFTOrderRejectApprove.DataType)

            Dim oColFTOrderRejectApproveSendMailTo As System.Data.DataColumn
            oColFTOrderRejectApproveSendMailTo = New System.Data.DataColumn("FTOrerRejectApproveSendMailTo", System.Type.GetType("System.String"))
            oColFTOrderRejectApproveSendMailTo.Caption = "FTOrerRejectApproveSendMailTo"
            tmpDTOrderRejectApprove.Columns.Add(oColFTOrderRejectApproveSendMailTo.ColumnName, oColFTOrderRejectApproveSendMailTo.DataType)

            Dim oColFTOrderRejectMailMsg As System.Data.DataColumn
            oColFTOrderRejectMailMsg = New System.Data.DataColumn("FTOrderRejectMailMsg", System.Type.GetType("System.String"))
            oColFTOrderRejectMailMsg.Caption = "FTOrderRejectMailMsg"
            tmpDTOrderRejectApprove.Columns.Add(oColFTOrderRejectMailMsg.ColumnName, oColFTOrderRejectMailMsg.DataType)
            '=====================================================================================================================================

            Dim dt As DataTable = CType(ogcmatcode.DataSource, DataTable)

            For Each r As DataRow In dt.Rows

                'If System.Diagnostics.Debugger.IsAttached = True Then
                '    MsgBox("Factory Order No : " & r!FTOrderNo.ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
                'End If

                '...user can be approved factory order no. more than one order no. for any times and then use in team production can see factory order no more than one job xxx

                HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

                Dim sqlCmd As New SqlCommand

                sqlCmd.Connection = HI.Conn.SQLConn.Cnn
                sqlCmd.CommandType = CommandType.StoredProcedure
                sqlCmd.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[SP_UPDATE_ORDER_APPROVE_STATUS]"
                sqlCmd.Parameters.Clear()
                sqlCmd.Parameters.AddWithValue("@FNHSysCmpId", r!FNHSysCmpId)
                sqlCmd.Parameters.AddWithValue("@FTOrderNo", r!FTOrderNo)
                sqlCmd.Parameters.AddWithValue("@FTStateOrderApp", r!FTStateOrderApp)
                sqlCmd.Parameters.AddWithValue("@UserName", HI.ST.UserInfo.UserName)
                sqlCmd.Parameters.AddWithValue("@LANGID", HI.ST.Lang.Language.ToString())
                sqlCmd.Parameters.AddWithValue("@RetMsg", "").Direction = ParameterDirection.Output '...return output after excute command

                Dim sqlDA As New SqlDataAdapter(sqlCmd.CommandText, HI.Conn.SQLConn._ConnString)
                sqlDA.SelectCommand = sqlCmd
                sqlDA.SelectCommand.ExecuteNonQuery()

                If (sqlCmd.Parameters("@RetMsg").Value.ToString() = "A") Then
                    'RetMessage += "ยกเลิกการอนุมัติ Order No. " + r!FTOrderNo.ToString + vbCrLf
                    CountApproved += 1

                    tmpDTApproveProduction = LoadOrderApprovedProduction(r!FTOrderNo.ToString)

                    'If System.Diagnostics.Debugger.IsAttached = True Then
                    '    If Not DBNull.Value.Equals(tmpDTApproveProduction) AndAlso tmpDTApproveProduction.Rows.Count > 0 Then
                    '        MsgBox("tmpDTApprovProduct Rows Count is : " & tmpDTApproveProduction.Rows.Count, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
                    '    End If

                    'End If

                    For Each oDataRow As System.Data.DataRow In tmpDTApproveProduction.Rows

                        Dim oNewRowOrderApprove As System.Data.DataRow

                        oNewRowOrderApprove = tmpDTOrderApprove.NewRow()

                        oNewRowOrderApprove.Item("FTOrderApprove") = r!FTOrderNo.ToString
                        oNewRowOrderApprove.Item("FTOrerApproveSendMailTo") = oDataRow!FTUserName.ToString
                        oNewRowOrderApprove.Item("FTOrderMailMsg") = "Approved Date : " & oDataRow!FDAppDate.ToString & Environment.NewLine & "Approved Time : " & oDataRow!FTAppTime.ToString & Environment.NewLine & "Approved By   : " & tStrMailFrom

                        tmpDTOrderApprove.Rows.Add(oNewRowOrderApprove)

                    Next

                ElseIf (sqlCmd.Parameters("@RetMsg").Value.ToString() = "D") Then
                    'RetMessage += "อนุมัติ Order No. " + r!FTOrderNo.ToString + vbCrLf

                    '...ยกเลิกการอนุมัติผลิต
                    CountCancel += 1

                    tmpDTRejectApproveProduction = LoadOrderApprovedProduction(r!FTOrderNo.ToString)

                    For Each oDataRejectRow As System.Data.DataRow In tmpDTRejectApproveProduction.Rows

                        Dim oNewRowOrderRejectApprove As System.Data.DataRow

                        oNewRowOrderRejectApprove = tmpDTOrderRejectApprove.NewRow()

                        oNewRowOrderRejectApprove.Item("FTOrderRejectApprove") = r!FTOrderNo.ToString
                        oNewRowOrderRejectApprove.Item("FTOrerRejectApproveSendMailTo") = oDataRejectRow!FTUserName.ToString
                        oNewRowOrderRejectApprove.Item("FTOrderRejectMailMsg") = "Cancel Approved Date : " & oDataRejectRow!FDAppDate.ToString & Environment.NewLine & "Cancel Approved Time : " & oDataRejectRow!FTAppTime.ToString & Environment.NewLine & "Cancel Approved By   : " & tStrMailFrom

                        tmpDTOrderRejectApprove.Rows.Add(oNewRowOrderRejectApprove)

                    Next

                ElseIf (sqlCmd.Parameters("@RetMsg").Value.ToString() = "E") Then
                    CountError += 1
                Else
                    CountNone += 1
                End If

                HI.Conn.SQLConn.DisposeSqlConnection(sqlCmd)

            Next

            If Not tmpDTApproveProduction Is Nothing Then tmpDTApproveProduction.Dispose()

            If Not tmpDTRejectApproveProduction Is Nothing Then tmpDTRejectApproveProduction.Dispose()

            '...sort data table ascending by FTUserName, FTOrderNo
            '...iterate loop send mail to user team production
            '...User หนึ่งท่านสามารถรับรายการ Factory Order No. ที่มีการ Approved ได้มากกว่า 1 รายการ Factory Order No.
            '=========================================================================================================================================================================================
            '...re-arrange

            '...Send mail approved
            '=========================================================================================================================================================================================
            If Not tmpDTOrderApprove Is Nothing AndAlso tmpDTOrderApprove.Rows.Count > 0 Then

                Dim oDataViewSendMail As New System.Data.DataView(tmpDTOrderApprove)

                oDataViewSendMail.Sort = "FTOrerApproveSendMailTo ASC, FTOrderApprove ASC"

                Dim tmpDTActionSendMail As System.Data.DataTable = oDataViewSendMail.ToTable()

                If Not tmpDTActionSendMail Is Nothing AndAlso tmpDTActionSendMail.Rows.Count > 0 Then

                    numCntWriteMail = 0

                    Select Case HI.ST.Lang.Language
                        Case HI.ST.Lang.eLang.EN
                            tSubjectMail = "Approved production factory Order No."
                        Case HI.ST.Lang.eLang.TH
                            tSubjectMail = "อนุมัติรายการใบสั่งผลิต"
                        Case Else
                            tSubjectMail = "Approved production factory Order No."
                    End Select

                    Dim tTextSendMailTo As String = ""
                    Dim tTextSendMailToPrv As String = ""
                    Dim tTextMailInfoRef As String = ""
                    Dim tTextMailApproveMsg As String = ""
                    Dim oStrBuilder As New System.Text.StringBuilder()

                    '...first record
                    tTextSendMailToPrv = tmpDTActionSendMail.Rows(0).Item("FTOrerApproveSendMailTo").ToString

                    oStrBuilder.Remove(0, oStrBuilder.Length)

                    For Each oRowSendMail As System.Data.DataRow In tmpDTActionSendMail.Rows

                        tTextSendMailTo = oRowSendMail.Item("FTOrerApproveSendMailTo").ToString

                        If tTextSendMailTo <> tTextSendMailToPrv Then
                            '...action send mail xxxxx call sub or function send mail {HI.Mail.ClsSendMail.SendMail(tStrMailFrom, tStrMailTo, tSubjectMail, tMessageMail, 1, r!FTOrderNo.ToString)}
                            '...Mail Info Reference >>> FTMailInfoRef >>> FTOrderNo1|FTOrderNo2|FTOrder3|...|FTOrderNoXX

                            tTextMailApproveMsg = ""
                            tTextMailApproveMsg = oStrBuilder.ToString()

                            '...perform send mail
                            If HI.Mail.ClsSendMail.SendMail(tStrMailFrom, tTextSendMailToPrv, tSubjectMail, tTextMailApproveMsg, 1, tTextMailInfoRef) = True Then
                                'If System.Diagnostics.Debugger.IsAttached = True Then
                                '    MsgBox("SEND MAIL APPROVED FACTORY ORDER NO. COMPLETE ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
                                'End If

                                numCntWriteMail = numCntWriteMail + 1

                            End If

                            '...clear history send mail 
                            '------------------------------------------------------------------------
                            tTextSendMailToPrv = "" : tTextMailInfoRef = ""

                            oStrBuilder.Remove(0, oStrBuilder.Length)
                            '------------------------------------------------------------------------

                            '...initial send mail to
                            '------------------------------------------------------------------------
                            tTextSendMailToPrv = tTextSendMailTo
                            tTextMailInfoRef = IIf(tTextMailInfoRef = "", oRowSendMail.Item("FTOrderApprove").ToString, tTextMailInfoRef & "|" & oRowSendMail.Item("FTOrderApprove").ToString)
                            oStrBuilder.AppendLine("Approved Factory Order No. : " & oRowSendMail.Item("FTOrderApprove").ToString)
                            oStrBuilder.AppendLine(oRowSendMail.Item("FTOrderMailMsg").ToString)
                            '------------------------------------------------------------------------

                        Else
                            tTextMailInfoRef = IIf(tTextMailInfoRef = "", oRowSendMail.Item("FTOrderApprove").ToString, tTextMailInfoRef & "|" & oRowSendMail.Item("FTOrderApprove").ToString)
                            oStrBuilder.AppendLine("Approved Factory Order No. : " & oRowSendMail.Item("FTOrderApprove").ToString)
                            oStrBuilder.AppendLine(oRowSendMail.Item("FTOrderMailMsg").ToString) '...append new line string mail message detail
                        End If

                    Next

                    If Not tmpDTActionSendMail Is Nothing And tmpDTActionSendMail.Rows.Count > 0 Then
                        '...perform send mail
                        If HI.Mail.ClsSendMail.SendMail(tStrMailFrom, tTextSendMailToPrv, tSubjectMail, tTextMailApproveMsg, 1, tTextMailInfoRef) = True Then
                            'If System.Diagnostics.Debugger.IsAttached = True Then
                            '    MsgBox("SEND MAIL APPROVED FACTORY ORDER NO. COMPLETE ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
                            'End If

                            numCntWriteMail = numCntWriteMail + 1

                        End If

                    End If

                    tmpDTActionSendMail.Dispose()

                    If System.Diagnostics.Debugger.IsAttached = True Then
                        MsgBox("SEND MAIL APPROVED FACTORY ORDER NO. COMPLETE ..." & Environment.NewLine & "Number total write Mail is : " & numCntWriteMail, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
                    End If

                End If

                If Not tmpDTOrderApprove Is Nothing Then tmpDTOrderApprove.Dispose() '...De-Allocate to memory resource

            End If
            '=========================================================================================================================================================================================

            '...send mail reject approved
            '=========================================================================================================================================================================================
            If Not tmpDTOrderRejectApprove Is Nothing And tmpDTOrderRejectApprove.Rows.Count > 0 Then
                Dim oDataViewSendMailReject As New System.Data.DataView(tmpDTOrderRejectApprove)

                oDataViewSendMailReject.Sort = "FTOrerRejectApproveSendMailTo ASC, FTOrderRejectApprove ASC"

                Dim tmpDTActionSendMailReject As System.Data.DataTable = oDataViewSendMailReject.ToTable()

                If Not tmpDTActionSendMailReject Is Nothing AndAlso tmpDTActionSendMailReject.Rows.Count > 0 Then

                    numCntRejectMaill = 0

                    Select Case HI.ST.Lang.Language
                        Case HI.ST.Lang.eLang.EN
                            tSubjectMail = "Cancel Approved production factory Order No."
                        Case HI.ST.Lang.eLang.TH
                            tSubjectMail = "ยกเลิกการอนุมัติรายการใบสั่งผลิต"
                        Case Else
                            tSubjectMail = "Cancel Approved production factory Order No."
                    End Select

                    Dim tTextSendMailTo As String = ""
                    Dim tTextSendMailToPrv As String = ""
                    Dim tTextMailInfoRef As String = ""
                    Dim tTextMailApproveMsg As String = ""
                    Dim oStrBuilder As New System.Text.StringBuilder()

                    '...first record
                    tTextSendMailToPrv = tmpDTActionSendMailReject.Rows(0).Item("FTOrerRejectApproveSendMailTo").ToString

                    oStrBuilder.Remove(0, oStrBuilder.Length)

                    For Each oRowSendMail As System.Data.DataRow In tmpDTActionSendMailReject.Rows

                        tTextSendMailTo = oRowSendMail.Item("FTOrerRejectApproveSendMailTo").ToString

                        If tTextSendMailTo <> tTextSendMailToPrv Then
                            '...action send mail xxxxx call sub or function send mail {HI.Mail.ClsSendMail.SendMail(tStrMailFrom, tStrMailTo, tSubjectMail, tMessageMail, 1, r!FTOrderNo.ToString)}
                            '...Mail Info Reference >>> FTMailInfoRef >>> FTOrderNo1|FTOrderNo2|FTOrder3|...|FTOrderNoXX

                            tTextMailApproveMsg = ""
                            tTextMailApproveMsg = oStrBuilder.ToString()

                            '...perform send mail
                            If HI.Mail.ClsSendMail.SendMail(tStrMailFrom, tTextSendMailToPrv, tSubjectMail, tTextMailApproveMsg, 1, tTextMailInfoRef) = True Then
                                'If System.Diagnostics.Debugger.IsAttached = True Then
                                '    MsgBox("SEND MAIL APPROVED FACTORY ORDER NO. COMPLETE ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
                                'End If

                                numCntRejectMaill = numCntRejectMaill + 1

                            End If

                            '...clear history send mail 
                            '------------------------------------------------------------------------
                            tTextSendMailToPrv = "" : tTextMailInfoRef = ""

                            oStrBuilder.Remove(0, oStrBuilder.Length)
                            '------------------------------------------------------------------------

                            '...initial send mail to
                            '------------------------------------------------------------------------
                            tTextSendMailToPrv = tTextSendMailTo
                            tTextMailInfoRef = IIf(tTextMailInfoRef = "", oRowSendMail.Item("FTOrderRejectApprove").ToString, tTextMailInfoRef & "|" & oRowSendMail.Item("FTOrderRejectApprove").ToString)
                            oStrBuilder.AppendLine("Cancel Approved Factory Order No. : " & oRowSendMail.Item("FTOrderRejectApprove").ToString)
                            oStrBuilder.AppendLine(oRowSendMail.Item("FTOrderRejectMailMsg").ToString)
                            '------------------------------------------------------------------------

                        Else
                            tTextMailInfoRef = IIf(tTextMailInfoRef = "", oRowSendMail.Item("FTOrderRejectApprove").ToString, tTextMailInfoRef & "|" & oRowSendMail.Item("FTOrderRejectApprove").ToString)
                            oStrBuilder.AppendLine("Cancel Approved Factory Order No. : " & oRowSendMail.Item("FTOrderRejectApprove").ToString)
                            oStrBuilder.AppendLine(oRowSendMail.Item("FTOrderRejectMailMsg").ToString) '...append new line string mail message detail
                        End If

                    Next

                    If Not tmpDTActionSendMailReject Is Nothing And tmpDTActionSendMailReject.Rows.Count > 0 Then
                        '...perform send mail
                        If HI.Mail.ClsSendMail.SendMail(tStrMailFrom, tTextSendMailToPrv, tSubjectMail, tTextMailApproveMsg, 1, tTextMailInfoRef) = True Then
                            'If System.Diagnostics.Debugger.IsAttached = True Then
                            '    MsgBox("SEND MAIL APPROVED FACTORY ORDER NO. COMPLETE ...", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
                            'End If

                            numCntRejectMaill = numCntRejectMaill + 1

                        End If

                    End If

                    tmpDTActionSendMailReject.Dispose()

                    If System.Diagnostics.Debugger.IsAttached = True Then
                        MsgBox("SEND MAIL REJECT APPROVED FACTORY ORDER NO. COMPLETE ..." & Environment.NewLine & "Number total write Mail Reject is : " & numCntRejectMaill, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
                    End If

                End If

                If Not tmpDTOrderRejectApprove Is Nothing Then tmpDTOrderRejectApprove.Dispose() '...De-Allocate to memory resource

            End If
            '=========================================================================================================================================================================================

            RetMessage = String.Format("ยกเลิกการอนุมัติ Order จำนวน {0} รายการ" & vbCrLf _
                        + "อนุมัติ Order จำนวน {1} รายการ" & vbCrLf _
                        + "ไม่สามารถทำรายการได้ จำนวน {2} รายการ", CountCancel, CountApproved, CountError)

            MsgBox(RetMessage, vbOKOnly)

            Call RefreshGrid()

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title.ToString)
            End If

        End Try

    End Sub

    Private Sub otbmain_Click(sender As Object, e As EventArgs) Handles otbmain.Click

    End Sub

    Private Sub otbmain_SelectedPageChanged(sender As Object, e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otbmain.SelectedPageChanged
        Call TabChange()
    End Sub

    Private Sub TabChange()

        Me.ocmsendpoapprove.Visible = (Me.otbmain.SelectedTabPage.Name = Me.otpsenddirector.Name)
        Me.ocmsave.Visible = (Me.otbmain.SelectedTabPage.Name = Me.otpapprove.Name)
        Me.ocmpreviewtvwthb.Visible = (Me.otbmain.SelectedTabPage.Name = Me.otpsenddirector.Name)

        HI.TL.METHOD.CallActiveToolBarFunction(Me)
    End Sub

    Private Sub ogvdirector_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvdirector.RowCellStyle
        Try
            With Me.ogvdirector
                If "" & .GetRowCellValue(e.RowHandle, "FTStateDirectorReject").ToString = "1" Then
                    e.Appearance.ForeColor = Color.Orange
                ElseIf "" & .GetRowCellValue(e.RowHandle, "FTStateDirectorApp").ToString = "1" Then
                    e.Appearance.ForeColor = Color.Green
                ElseIf "" & .GetRowCellValue(e.RowHandle, "FTStateSendDirectorApp").ToString = "1" Then
                    e.Appearance.ForeColor = Color.Blue
                End If
            End With
        Catch ex As Exception

        End Try

        Try
            With Me.ogvdirector
                If "" & .GetRowCellValue(e.RowHandle, "FTStateFactoryApp").ToString = "1" Then
                    e.Appearance.ForeColor = Color.Green
                ElseIf "" & .GetRowCellValue(e.RowHandle, "FTStateFactoryReject").ToString = "1" Then
                    e.Appearance.ForeColor = Color.Orange
                End If
            End With
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ocmsendpoapprove_Click(sender As Object, e As EventArgs) Handles ocmsendpoapprove.Click

        With CType(ogcdirector.DataSource, DataTable)
            .AcceptChanges()

            If .Select("FTSelect='1'").Length <= 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาระบุข้อมูลที่ต้องการทำการส่ง Approved !!!", 1507048874, Me.Text, , MessageBoxIcon.Warning)
                Exit Sub
            End If

        End With

        If SendApproveToDirector() Then
            HI.MG.ShowMsg.mInfo("ระบบได้ทำการส่ง Approved !!!", 1507048879, Me.Text, , MessageBoxIcon.Information)
            Call RefreshGrid()
        End If

    End Sub

    Private Function SendApproveToDirector() As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Sending...Approve ,Please wait.")
        Try
            Dim dt As DataTable

            With CType(ogcdirector.DataSource, DataTable)
                .AcceptChanges()
                dt = .Copy
            End With

            Dim _Qry As String = ""

            'For Each R As DataRow In dt.Select("FTSelect='1'")
            '    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder"
            '    _Qry &= vbCrLf & " SET FTStateSendDirectorApp='1'"
            '    _Qry &= vbCrLf & " ,FTStateSendDirectorBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '    _Qry &= vbCrLf & " ,FDStateSendDirectorDate=" & HI.UL.ULDate.FormatDateDB
            '    _Qry &= vbCrLf & " ,FTStateSendDirectorTime=" & HI.UL.ULDate.FormatTimeDB
            '    _Qry &= vbCrLf & " ,FTStateDirectorApp='0'"
            '    _Qry &= vbCrLf & " ,FTStateDirectorAppBy=''"
            '    _Qry &= vbCrLf & " ,FTStateDirectorReject='0'"
            '    _Qry &= vbCrLf & " ,FTStateDirectorRejectBy=''"
            '    _Qry &= vbCrLf & " ,FTStateFactoryApp='0'"
            '    _Qry &= vbCrLf & " ,FTStateFactoryAppBy=''"
            '    _Qry &= vbCrLf & " ,FTStateFactoryReject='0'"
            '    _Qry &= vbCrLf & " ,FTStateFactoryRejectBy=''"
            '    _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'"
            '    _Qry &= vbCrLf & " AND ( "
            '    _Qry &= vbCrLf & "  (ISNULL(FTStateSendDirectorApp,'') <>'1' ) "
            '    _Qry &= vbCrLf & "  OR (ISNULL(FTStateSendDirectorApp,'') ='1' AND ISNULL(FTStateDirectorReject,'')='1' ) "
            '    _Qry &= vbCrLf & "  OR (ISNULL(FTStateSendDirectorApp,'') ='1' AND ISNULL(FTStateDirectorApp,'')='1'  AND ISNULL(FTStateFactoryReject,'')='1' ) "
            '    _Qry &= vbCrLf & " )"

            '    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            '    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub"
            '    _Qry &= vbCrLf & " SET FTStateSendDirectorApp='1'"
            '    _Qry &= vbCrLf & " ,FTStateSendDirectorBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '    _Qry &= vbCrLf & " ,FDStateSendDirectorDate=" & HI.UL.ULDate.FormatDateDB
            '    _Qry &= vbCrLf & " ,FTStateSendDirectorTime=" & HI.UL.ULDate.FormatTimeDB
            '    _Qry &= vbCrLf & " ,FTStateDirectorApp='0'"
            '    _Qry &= vbCrLf & " ,FTStateDirectorAppBy=''"
            '    _Qry &= vbCrLf & " ,FTStateDirectorReject='0'"
            '    _Qry &= vbCrLf & " ,FTStateDirectorRejectBy=''"
            '    _Qry &= vbCrLf & " ,FTStateFactoryApp='0'"
            '    _Qry &= vbCrLf & " ,FTStateFactoryAppBy=''"
            '    _Qry &= vbCrLf & " ,FTStateFactoryReject='0'"
            '    _Qry &= vbCrLf & " ,FTStateFactoryRejectBy=''"
            '    _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'"
            '    _Qry &= vbCrLf & " AND ( "
            '    _Qry &= vbCrLf & "  (ISNULL(FTStateSendDirectorApp,'') <>'1' ) "
            '    _Qry &= vbCrLf & "  OR (ISNULL(FTStateSendDirectorApp,'') ='1' AND ISNULL(FTStateDirectorReject,'')='1' ) "
            '    _Qry &= vbCrLf & "  OR (ISNULL(FTStateSendDirectorApp,'') ='1' AND ISNULL(FTStateDirectorApp,'')='1'  AND ISNULL(FTStateFactoryReject,'')='1' ) "
            '    _Qry &= vbCrLf & " )"

            '    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            '    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert"
            '    _Qry &= vbCrLf & " SET FTStateSendDirectorApp='1'"
            '    _Qry &= vbCrLf & " ,FTStateSendDirectorBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '    _Qry &= vbCrLf & " ,FDStateSendDirectorDate=" & HI.UL.ULDate.FormatDateDB
            '    _Qry &= vbCrLf & " ,FTStateSendDirectorTime=" & HI.UL.ULDate.FormatTimeDB
            '    _Qry &= vbCrLf & " ,FTStateDirectorApp='0'"
            '    _Qry &= vbCrLf & " ,FTStateDirectorAppBy=''"
            '    _Qry &= vbCrLf & " ,FTStateDirectorReject='0'"
            '    _Qry &= vbCrLf & " ,FTStateDirectorRejectBy=''"
            '    _Qry &= vbCrLf & " ,FTStateFactoryApp='0'"
            '    _Qry &= vbCrLf & " ,FTStateFactoryAppBy=''"
            '    _Qry &= vbCrLf & " ,FTStateFactoryReject='0'"
            '    _Qry &= vbCrLf & " ,FTStateFactoryRejectBy=''"
            '    _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString()) & "'"
            '    _Qry &= vbCrLf & " AND ( "
            '    _Qry &= vbCrLf & "  (ISNULL(FTStateSendDirectorApp,'') <>'1' ) "
            '    _Qry &= vbCrLf & "  OR (ISNULL(FTStateSendDirectorApp,'') ='1' AND ISNULL(FTStateDirectorReject,'')='1' ) "
            '    _Qry &= vbCrLf & "  OR (ISNULL(FTStateSendDirectorApp,'') ='1' AND ISNULL(FTStateDirectorApp,'')='1'  AND ISNULL(FTStateFactoryReject,'')='1' ) "
            '    _Qry &= vbCrLf & " )"

            '    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            'Next


            Dim grp As List(Of String) = (dt.Select("FTSelect='1'", "FTOrderNo").CopyToDataTable).AsEnumerable() _
                                                    .Select(Function(r) r.Field(Of String)("FTOrderNo")) _
                                                    .Distinct() _
                                                    .ToList()


            For Each orderno As String In grp

                _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder"
                _Qry &= vbCrLf & " SET FTStateSendDirectorApp='1'"
                _Qry &= vbCrLf & " ,FTStateSendDirectorBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ,FDStateSendDirectorDate=" & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & " ,FTStateSendDirectorTime=" & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & " ,FTStateDirectorApp='0'"
                _Qry &= vbCrLf & " ,FTStateDirectorAppBy=''"
                _Qry &= vbCrLf & " ,FTStateDirectorReject='0'"
                _Qry &= vbCrLf & " ,FTStateDirectorRejectBy=''"
                _Qry &= vbCrLf & " ,FTStateFactoryApp='0'"
                _Qry &= vbCrLf & " ,FTStateFactoryAppBy=''"
                _Qry &= vbCrLf & " ,FTStateFactoryReject='0'"
                _Qry &= vbCrLf & " ,FTStateFactoryRejectBy=''"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(orderno) & "'"
                _Qry &= vbCrLf & " AND ( "
                _Qry &= vbCrLf & "  (ISNULL(FTStateSendDirectorApp,'') <>'1' ) "
                _Qry &= vbCrLf & "  OR (ISNULL(FTStateSendDirectorApp,'') ='1' AND ISNULL(FTStateDirectorReject,'')='1' ) "
                _Qry &= vbCrLf & "  OR (ISNULL(FTStateSendDirectorApp,'') ='1' AND ISNULL(FTStateDirectorApp,'')='1'  AND ISNULL(FTStateFactoryReject,'')='1' ) "
                _Qry &= vbCrLf & " )"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub"
                _Qry &= vbCrLf & " SET FTStateSendDirectorApp='1'"
                _Qry &= vbCrLf & " ,FTStateSendDirectorBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ,FDStateSendDirectorDate=" & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & " ,FTStateSendDirectorTime=" & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & " ,FTStateDirectorApp='0'"
                _Qry &= vbCrLf & " ,FTStateDirectorAppBy=''"
                _Qry &= vbCrLf & " ,FTStateDirectorReject='0'"
                _Qry &= vbCrLf & " ,FTStateDirectorRejectBy=''"
                _Qry &= vbCrLf & " ,FTStateFactoryApp='0'"
                _Qry &= vbCrLf & " ,FTStateFactoryAppBy=''"
                _Qry &= vbCrLf & " ,FTStateFactoryReject='0'"
                _Qry &= vbCrLf & " ,FTStateFactoryRejectBy=''"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(orderno) & "'"
                _Qry &= vbCrLf & " AND ( "
                _Qry &= vbCrLf & "  (ISNULL(FTStateSendDirectorApp,'') <>'1' ) "
                _Qry &= vbCrLf & "  OR (ISNULL(FTStateSendDirectorApp,'') ='1' AND ISNULL(FTStateDirectorReject,'')='1' ) "
                _Qry &= vbCrLf & "  OR (ISNULL(FTStateSendDirectorApp,'') ='1' AND ISNULL(FTStateDirectorApp,'')='1'  AND ISNULL(FTStateFactoryReject,'')='1' ) "
                _Qry &= vbCrLf & " )"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert"
                _Qry &= vbCrLf & " SET FTStateSendDirectorApp='1'"
                _Qry &= vbCrLf & " ,FTStateSendDirectorBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ,FDStateSendDirectorDate=" & HI.UL.ULDate.FormatDateDB
                _Qry &= vbCrLf & " ,FTStateSendDirectorTime=" & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & " ,FTStateDirectorApp='0'"
                _Qry &= vbCrLf & " ,FTStateDirectorAppBy=''"
                _Qry &= vbCrLf & " ,FTStateDirectorReject='0'"
                _Qry &= vbCrLf & " ,FTStateDirectorRejectBy=''"
                _Qry &= vbCrLf & " ,FTStateFactoryApp='0'"
                _Qry &= vbCrLf & " ,FTStateFactoryAppBy=''"
                _Qry &= vbCrLf & " ,FTStateFactoryReject='0'"
                _Qry &= vbCrLf & " ,FTStateFactoryRejectBy=''"
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(orderno) & "'"
                _Qry &= vbCrLf & " AND ( "
                _Qry &= vbCrLf & "  (ISNULL(FTStateSendDirectorApp,'') <>'1' ) "
                _Qry &= vbCrLf & "  OR (ISNULL(FTStateSendDirectorApp,'') ='1' AND ISNULL(FTStateDirectorReject,'')='1' ) "
                _Qry &= vbCrLf & "  OR (ISNULL(FTStateSendDirectorApp,'') ='1' AND ISNULL(FTStateDirectorApp,'')='1'  AND ISNULL(FTStateFactoryReject,'')='1' ) "
                _Qry &= vbCrLf & " )"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Next

        Catch ex As Exception

        End Try
        _Spls.Close()
        Return True



    End Function

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

    Private Sub ocmpreviewtvwthb_Click(sender As Object, e As EventArgs) Handles ocmpreviewtvwthb.Click
        Select Case otbmain.SelectedTabPage.Name
            Case otpapprove.Name
            Case otpsenddirector.Name
                With CType(Me.ogcdirector.DataSource, DataTable)
                    .AcceptChanges()
                    If .Select("FTSelect='1'").Length <= 0 Then
                        HI.MG.ShowMsg.mInfo("กรุณาระบุข้องมูล ที่ต้องการทำการ Preview", 1507060584, Me.Text, , MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    'If .Select("FTSelect='1' AND FTStateDirectorApp='1'").Length <= 0 Then
                    '    HI.MG.ShowMsg.mInfo("กรุณาระบุข้องมูล ที่ Director ทำการ Approve แล้ว ที่ต้องการทำการ Preview", 1507060586, Me.Text, , MessageBoxIcon.Warning)
                    '    Exit Sub
                    'End If

                    'For Each R As DataRow In .Select("FTSelect='1' ")

                    '    With New HI.RP.Report
                    '        .FormTitle = Me.Text
                    '        .ReportFolderName = "Merchandise Report\"
                    '        .ReportName = "FactoryPO_THB.rpt"
                    '        .Formular = "{TMERTOrder.FTOrderNo}='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                    '        .Preview()
                    '    End With
                    'Next

                    Dim dt As DataTable = .Copy

                    Dim grp As List(Of String) = (dt.Select("FTSelect='1'", "FTOrderNo").CopyToDataTable).AsEnumerable() _
                                                  .Select(Function(r) r.Field(Of String)("FTOrderNo")) _
                                                  .Distinct() _
                                                  .ToList()


                    For Each orderno As String In grp
                        With New HI.RP.Report
                            .FormTitle = Me.Text
                            .ReportFolderName = "Merchandise Report\"
                            .ReportName = "FactoryPO_THB.rpt"
                            .Formular = "{TMERTOrder.FTOrderNo}='" & HI.UL.ULF.rpQuoted(orderno) & "' "
                            .Preview()
                        End With
                    Next

                End With
        End Select
    End Sub

    Private Sub ogvdirector_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvdirector.CellMerge
        Try
            With Me.ogvdirector
                If "" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString _
                                And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                    e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                    e.Handled = True

                Else

                    e.Merge = False
                    e.Handled = True

                End If

            End With

        Catch ex As Exception
        End Try

    End Sub

    Private Sub RepositorySelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositorySelect.EditValueChanging
        Try
            Dim State As String = "0"
            If e.NewValue.ToString = "1" Then
                State = "1"
            Else
                State = "0"
            End If

            Dim OrderNo As String = ""

            With ogvdirector
                OrderNo = "" & .GetFocusedRowCellValue("FTOrderNo").ToString()
            End With


            With CType(ogcdirector.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Select("FTOrderNo='" & OrderNo & "'")
                    R!FTSelect = State
                Next

                .AcceptChanges()
            End With

        Catch ex As Exception

        End Try
    End Sub
End Class