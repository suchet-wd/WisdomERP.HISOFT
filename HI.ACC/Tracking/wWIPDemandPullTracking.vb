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
Imports System.Data.Common
Imports System.Windows.Forms
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Globalization
Imports System.Configuration
Imports System.Diagnostics
Imports DevExpress.XtraPrintingLinks
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.Specialized

Public Class wWIPDemandPullTracking


    Dim View As GridView
    Dim RowsIndex As Double
    Dim TopVisibleIndex As Int32
    Private sFNHSysStyleId As String

    ''' Used Data Adapter to control database

    Dim oleDbDataAdapter1 As DbDataAdapter
    Dim oleDbDataAdapter2 As DbDataAdapter
    Dim dtStyleDetail As DataTable


    Dim saveFileDialog As SaveFileDialog = New SaveFileDialog()

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

#End Region

#Region "MAIN PROC"

    Private Sub wOrderListingInfo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try


            FNHSysCmpId = GetPropertyTagValue(FNHSysCmpId)
            FNHSysBuyId = GetPropertyTagValue(FNHSysBuyId)
            FNHSysStyleId = GetPropertyTagValue(FNHSysStyleId)
            FNHSysCustId = GetPropertyTagValue(FNHSysCustId)
            FNHSysSeasonId = GetPropertyTagValue(FNHSysSeasonId)
            FNHSysPOID = GetPropertyTagValue(FNHSysPOID)

            sFNHSysStyleId = ""



        Catch ex As Exception
        End Try

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

    Private Sub LoadOrderListingInfo(ByVal _FNHSysCmpId As String, ByVal _FTPORef As String, ByVal _FNHSysStyleId As String, ByVal _FNHSysCustId As String, ByVal _FNHSysBuyId As String, ByVal _FNHSysSeasonId As String)
        Dim Qry As String = ""
        Dim dt As New DataTable

        Dim Spls As New HI.TL.SplashScreen("Loading.....")

        Try

            Qry = "exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..[SP_WIP_DATA_DEMANDPULLINFO] " & Val(_FNHSysCmpId.ToString) & " "
            Qry &= vbCrLf & ",'" & _FTPORef.ToString & "'," & Val(_FNHSysStyleId.ToString) & "," & Val(_FNHSysCustId.ToString) & "," & Val(_FNHSysBuyId.ToString) & "," & Val(_FNHSysSeasonId.ToString) & ""

            dt = HI.Conn.SQLConn.GetDataTable(Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Catch ex As Exception
        End Try

        Spls.Close()

        Me.ogcmatcode.DataSource = dt
        ogvdata.BestFitColumns()

    End Sub

    Private Sub ocmclear1_Click(sender As System.Object, e As System.EventArgs) Handles ocmclearclsr.Click
        Me.ogcmatcode.DataSource = Nothing
        Dim xCol As Integer = 0
        Dim Idx As Integer = 0
        Try

            HI.TL.HandlerControl.ClearControl(Me)


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Function PROC_VALIDATEbSHOWBROWSEDATA() As Boolean
        'Dim bFlagValidate As Boolean = False

        'If Not bFlagValidate AndAlso Me.FNHSysCmpId.Text.Trim <> "" Then bFlagValidate = True
        'If Not bFlagValidate AndAlso Me.FNHSysPOID.Text.Trim <> "" Then bFlagValidate = True
        'If Not bFlagValidate AndAlso Me.FNHSysStyleId.Text.Trim <> "" Then bFlagValidate = True
        'If Not bFlagValidate AndAlso Me.FNHSysCustId.Text.Trim <> "" Then bFlagValidate = True
        'If Not bFlagValidate AndAlso Me.FNHSysBuyId.Text.Trim <> "" Then bFlagValidate = True
        'If Not bFlagValidate AndAlso Me.FNHSysSeasonId.Text.Trim <> "" Then bFlagValidate = True


        'If Not (bFlagValidate) Then
        '    HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        'End If

        'Return bFlagValidate
        If Me.FNHSysCmpId.Text = "" And Me.FNHSysPOID.Text = "" And FNHSysStyleId.Text = "" And FNHSysCustId.Text = "" And FNHSysBuyId.Text = "" And FNHSysSeasonId.Text = "" Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmrefresh.Click

        If Not PROC_VALIDATEbSHOWBROWSEDATA() = True Then Exit Sub

        Dim _Str As String = ""

        ogcmatcode.DataSource = Nothing

        'If FNHSysBuyId.Text <> "" Then
        '    _Str = "SELECT TOP 1 FNHSysBuyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy WITH(NOLOCK) WHERE FTBuyCode ='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' "
        '    FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
        'End If

        'Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, FNHSysPOID.Text, FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, FNHSysBuyId.Properties.Tag.ToString, FNHSysSeasonId.Properties.Tag.ToString)
        Call LoadOrderListingInfo(FNHSysCmpId.Properties.Tag.ToString, "", FNHSysStyleId.Properties.Tag.ToString, FNHSysCustId.Properties.Tag.ToString, 0, 0)

    End Sub

    Private Sub ocmexit_Click(sender As System.Object, e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ogvdata_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvdata.CellMerge
        Try

            With Me.ogvdata
                If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                    e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                    e.Handled = True
                    e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Else
                    e.Merge = False
                    e.Handled = True
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub


#End Region

#Region "Initial Grid"



#End Region

End Class