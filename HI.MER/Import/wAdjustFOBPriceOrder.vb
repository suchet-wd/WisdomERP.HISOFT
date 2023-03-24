Imports System
Imports System.Data
Imports System.Windows.Forms
Imports System.Windows.Forms.Control
Imports System.Linq
Imports System.Collections.Generic
Imports DevExpress
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid.GridView
Imports DevExpress.XtraGrid.Views.BandedGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Base.BaseView
Imports DevExpress.XtraEditors.Repository
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Card
Imports System.Data.SqlClient
Imports System.Reflection
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid.Columns

Public Class wAdjustFOBPriceOrder
    Private PDataTableTmp As New DataTable
    Private FilerData As Boolean = False
    Private _ListPopup As wListCheckExportXML

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ListPopup = New wListCheckExportXML

        HI.TL.HandlerControl.AddHandlerObj(_ListPopup)

        Dim oSysLang As New HI.ST.SysLanguage

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, _ListPopup.Name.ToString.Trim, _ListPopup)
        Catch ex As Exception
        End Try

    End Sub

#Region "Variable Declaration"
    Private tSql As String
    Private Const _tTableFactoryOrderNo As String = "FactoryOrderNo"
    Private Const _tTableFactorySubOrderNo As String = "FactorySubOrderNo"
    Private Const _tTableFactorySubOrderNoBreakdown As String = "FactorySubOrderNoBreakdown"
    Private Const _tRelationName As String = "FONo"

    Private oDBdtOrderNo As System.Data.DataTable
    Private oDBdtSubOrderNo As System.Data.DataTable
    Private oGridViewAdjustFONo As DevExpress.XtraGrid.Views.Grid.GridView

    Private oDBds As DataSet
    Private oDBdtFactoryOrderNo As System.Data.DataTable
    Private oDBdtFactorySubOrderNo As System.Data.DataTable
    Private oDBdtFactorySubOrderNoBreakdown As System.Data.DataTable

    Private _bLoadFONoBreakdown As Boolean = False

    Private Delegate Sub FooterCellCustomDrawEventHandler(ByVal sender As Object, ByVal e As FooterCellCustomDrawEventArgs)

    Private Enum eTabIndexs As Integer
        oTabFactoryOrderNo = 0
        oTabFactorySubOrderNo = 1
        oTabFactorySubOrderNoBreakdown = 2
    End Enum

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

    Private Function CreateDataTableCheckXML() As DataTable
        Dim _dt As New DataTable
        _dt.Columns.Add("FTOrderNo", GetType(String))
        _dt.Columns.Add("FTSubOrderNo", GetType(String))
        _dt.Columns.Add("FTCustomerPO", GetType(String))

        Return _dt
    End Function
#End Region

#Region "MAIN PROC"

    Private Sub PROC_SAVE(sender As Object, e As EventArgs) Handles ocmSaveFOB.Click

        Try
            Dim tConfirmSaveData As String = ""

            CType(ogdAdjustSubFONoBreakdown.DataSource, DataTable).AcceptChanges()
            Select Case Me.otcFactoryOrderNo.SelectedTabPageIndex
                Case eTabIndexs.oTabFactoryOrderNo

                Case eTabIndexs.oTabFactorySubOrderNo

                Case eTabIndexs.oTabFactorySubOrderNoBreakdown
                    '...save only data factory sub order no breakdown

                    If Me.ogvAdjustSubFONoBreakdown.RowCount <= 0 Then
                        HI.MG.ShowMsg.mInfo("ไม่พบรายการข้อมูลใบสั่งผลิตย่อย !!!", 1406260719, Me.Text, , MessageBoxIcon.Warning)

                        'Select Case HI.ST.Lang.Language
                        '    Case HI.ST.Lang.eLang.EN, HI.ST.Lang.eLang.KM, HI.ST.Lang.eLang.VT
                        '        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, "Adjust Factory Sub Order No Breakdown.", "Data not found !!!")
                        '    Case HI.ST.Lang.eLang.TH
                        '        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, "ปรับปรุงข้อมูลรายการ ปริมาณสั่งซื้อ/ราคา/จำนวนเผื่อ ใบสั่งผลิตย่อย", "ไม่พบรายการข้อมูลใบสั่งผลิตย่อย !!!")
                        '        'Case HI.ST.Lang.eLang.KM
                        '        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, "Adjust Factory Order No.", "Data not found !!!")
                        '        'Case HI.ST.Lang.eLang.VT
                        '        '    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, "Adjust Factory Order No.", "Data not found !!!")
                        'End Select

                        Exit Sub

                    End If

                    'Select Case HI.ST.Lang.Language
                    '    Case HI.ST.Lang.eLang.EN, HI.ST.Lang.eLang.KM, HI.ST.Lang.eLang.VT
                    '        tConfirmSaveData = "Are you sure, do you want to save data factory sub order no breakdown?"
                    '    Case HI.ST.Lang.eLang.TH
                    '        tConfirmSaveData = "ยืนยันบันทึกการแก้ไขรายการ ปริมาณสั่งซื้อ/ราคา/จำนวนเผื่อ ใบสั้งผลิตย่อย ?"
                    'End Select

                    If Not HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, Me.Text) Then Exit Sub

                    PDataTableTmp = New DataTable
                    With PDataTableTmp.Columns
                        .Add("FNHSysStyleId", GetType(Integer))
                        .Add("FNHSysSeasonId", GetType(Integer))
                    End With

                    Dim _dt As DataTable = CreateDataTableCheckXML()
                    If W_PRCBSaveAdjustFONoSubBreakdown(_dt) = True Then

                        'Call UpDateMasterStyle()
                        ' Call UpDateMasterStyle()
                        Call W_PRCbLoadFONoSubBreakdown(False)
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.otcFactoryOrderNo.SelectedTabPage.Name)
                        '...refresh data factory sub order no/ factory sub order breakdown
                        'Dim oDBdv As System.Data.DataView
                        'oDBdv = W_PRCdvGETViewFilterData(Me.ogvAdjustFONo)
                        'If Not oDBdv Is Nothing Then
                        '    '...refresh datasource for gridview sub order no and sub order no breakdown
                        '    Dim tFTFilterFONoCriteria As String = ""
                        '    For Each oRowView As DataRowView In oDBdv
                        '        Dim oDataRow As DataRow = oRowView.Row
                        '        tFTFilterFONoCriteria = tFTFilterFONoCriteria & oDataRow.Item("FTOrderNo").ToString() & "|"
                        '    Next

                        '    If tFTFilterFONoCriteria <> "" Then
                        '        tFTFilterFONoCriteria = Microsoft.VisualBasic.Mid$(tFTFilterFONoCriteria, 1, Len(tFTFilterFONoCriteria) - 1)
                        '        Call W_PRCbLoadFONoSubBreakdown(tFTFilterFONoCriteria, "")
                        '        Me.otcFactoryOrderNo.SelectedTabPageIndex = eTabIndexs.oTabFactorySubOrderNoBreakdown
                        '    End If

                        'Else
                        '    Call W_PRCbLoadFONoSubBreakdown()
                        '    Me.otcFactoryOrderNo.SelectedTabPageIndex = eTabIndexs.oTabFactorySubOrderNoBreakdown
                        'End If
                        ' ocmRefresh.PerformClick()

                        'If _dt.Rows.Count > 0 Then
                        '    HI.MG.ShowMsg.mInfo("พบข้อมูลการส่งออก XML File แล้ว ในบางรายการ ไม่สามารถเปลี่ยนแปลงหรือแก้ไขข้อมูลได้ !!!", 1601010576, Me.Text, , MessageBoxIcon.Warning)

                        '    With _ListPopup
                        '        HI.ST.Lang.SP_SETxLanguage(_ListPopup)
                        '        .ogdlist.DataSource = _dt.Copy
                        '        .ogdlist.Refresh()
                        '        .ShowDialog()
                        '    End With
                        'End If
                        '_dt.Dispose()
                    Else
                        ' _dt.Dispose()
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.otcFactoryOrderNo.SelectedTabPage.Name)
                    End If
                    _dt.Dispose()
            End Select

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
            End If

        End Try

    End Sub


    Private Sub PROC_REFRESH(sender As Object, e As EventArgs) Handles ocmRefresh.Click
        'If System.Diagnostics.Debugger.IsAttached = False Then Exit Sub

        Try

            Dim _Spls As New HI.TL.SplashScreen("Loading... Please Wait....")

            Call W_PRCbLoadFONoSubBreakdown()

            ogvAdjustSubFONoBreakdown.ClearColumnsFilter()
            ogvAdjustSubFONoBreakdown.ActiveFilter.Clear()

            Me.otcFactoryOrderNo.SelectedTabPageIndex = eTabIndexs.oTabFactoryOrderNo

            _Spls.Close()

            'For Each oColGrdView As DevExpress.XtraGrid.Columns.GridColumn In DirectCast(Me.ogdAdjustFONo.Views(0), DevExpress.XtraGrid.Views.Grid.GridView).Columns
            '    With oColGrdView
            '        .OptionsColumn.AllowEdit = True
            '    End With
            'Next

            'If Not HI.ST.SysInfo.Admin Then

            '    For Each oColGrdView As DevExpress.XtraGrid.Columns.GridColumn In DirectCast(Me.ogdAdjustFONo.Views(0), DevExpress.XtraGrid.Views.Grid.GridView).Columns
            '        With oColGrdView
            '            .OptionsColumn.AllowEdit = False
            '        End With
            '    Next
            'End If

            'Call W_PRCxExpandViewDetail(False)

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        Me.FNHSysCmpId.Focus()

    End Sub

    Private Sub PROC_CLEAR(send As Object, e As EventArgs) Handles ocmclearclsr.Click
        REM If System.Diagnostics.Debugger.IsAttached = False Then Exit Sub


        Me.ogdAdjustSubFONoBreakdown.DataSource = Nothing
        Me.ogdAdjustSubFONoBreakdown.Refresh()



        ogvAdjustSubFONoBreakdown.ClearColumnsFilter()
        ogvAdjustSubFONoBreakdown.ActiveFilter.Clear()

        HI.TL.HandlerControl.ClearControl(Me)

        Me.otcFactoryOrderNo.SelectedTabPageIndex = eTabIndexs.oTabFactoryOrderNo


        Me.FNHSysCmpId.Text = ""
        Me.FNHSysCmpId.Focus()

    End Sub

    Private Sub PROC_EXIT(sender As Object, e As EventArgs) Handles ocmExit.Click
        Me.Close()
    End Sub

#End Region

#Region "Sub Procedure And Sub Function"


    Private Function W_PRCdvGETViewFilterData(ByVal pGridView As DevExpress.XtraGrid.Views.Grid.GridView) As System.Data.DataView
        Try
            If Not pGridView Is Nothing Then
                If pGridView.ActiveFilter Is Nothing Or pGridView.ActiveFilterEnabled = False Or pGridView.ActiveFilter.Expression = "" Then
                    Return CType(pGridView.DataSource, System.Data.DataView)
                Else
                    Dim oDBdt As System.Data.DataTable = CType(pGridView.GridControl.DataSource, System.Data.DataTable)
                    Dim oDataViewFilterData As New System.Data.DataView(oDBdt)
                    oDataViewFilterData.RowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(pGridView.ActiveFilterCriteria)
                    Return oDataViewFilterData
                End If

            Else
                Return Nothing
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If

            Return Nothing
        End Try

    End Function


    Private Function GridViewGetBrowseRespon(ControlObject As Object, GridView As DevExpress.XtraGrid.Views.Grid.GridView, BrowseID As Integer, ByRef _dtret As DataTable,
                                                ByRef _BrowseCmd As String,
                                                ByRef _BrowseSortCmd As String,
                                                ByRef _BrowseWhereCmd As String,
                                                ByRef _FTBrwCmdField As String,
                                                ByRef _FTBrwCmdFieldOptional As String,
                                                ByRef FTBrwCmdGroupBy As String,
                                                ByRef _Command As String,
                                                 ByRef _ConFiled As String,
                                                _Data As String,
                                                Optional _Editvalue As Boolean = False) As String




        Dim _Qrysql As String
        Dim _ConFieldName As String = ""
        Dim _dtbrw As DataTable
        _Qrysql = " SELECT  TOP 1    BrwID, "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qrysql &= vbCrLf & " FTBrwCmdTH AS FTBrwCmd "
        Else
            _Qrysql &= vbCrLf & " FTBrwCmdEN AS FTBrwCmd "
        End If

        _Qrysql &= vbCrLf & ", BrwRetID,FTConField,FTBrwCmdSort,FTBrwCmdWhere,FTBrwCmdField,FTBrwCmdFieldOptional,FTBrwCmdENGroupBy,FTBrwCmdTHGroupBy "
        _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowse  With (NOLOCK) "
        _Qrysql &= vbCrLf & " WHERE BrwID=" & BrowseID & " "

        _dtbrw = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)
        _Qrysql = ""
        For Each Row As DataRow In _dtbrw.Rows
            _Qrysql = " SELECT  BrwRetID, FNSeq, FTBrwField, FTRetField,Convert(nvarchar(500),'') AS ValuesRet,FTStatePropertyTag "
            _Qrysql &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysBrowseRet With (NOLOCK) "
            _Qrysql &= vbCrLf & " WHERE BrwRetID=" & Val(Row!BrwRetID.ToString) & " "

            _dtret = New DataTable
            _dtret = HI.Conn.SQLConn.GetDataTable(_Qrysql, Conn.DB.DataBaseName.DB_SYSTEM)

            _BrowseCmd = Row!FTBrwCmd.ToString
            _BrowseSortCmd = Row!FTBrwCmdSort.ToString
            _BrowseWhereCmd = Row!FTBrwCmdWhere.ToString

            _FTBrwCmdField = Row!FTBrwCmdField.ToString
            _FTBrwCmdFieldOptional = Row!FTBrwCmdFieldOptional.ToString

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                FTBrwCmdGroupBy = Row!FTBrwCmdTHGroupBy.ToString
            Else
                FTBrwCmdGroupBy = Row!FTBrwCmdENGroupBy.ToString
            End If

            _Command = Row!FTBrwCmd.ToString

            If (_Editvalue) Then
                _ConFieldName = Row!FTConField.ToString
                _ConFiled = _ConFieldName
            End If

        Next


        Dim _Where As String = ""
        Dim _DataCon As String = ""

        Dim I As Integer = 0
        If _ConFieldName <> "" Then
            For Each _QryCon As String In _ConFieldName.Split(",")

                I = I + 1

                If I = 1 Then

                    If _Where = "" Then
                        _Where &= "     " & _QryCon & " ='" & _Data & "'  "
                    Else
                        _Where &= "   AND  " & _QryCon & " ='" & _Data & "'  "
                    End If

                Else

                    _DataCon = ""

                    With GridView
                        If Not (.Columns.ColumnByFieldName(_QryCon) Is Nothing) Then
                            _DataCon = "" & .GetRowCellValue(.FocusedRowHandle, _QryCon).ToString

                            If _Where = "" Then
                                _Where &= "     " & _QryCon & " ='" & _DataCon & "'  "
                            Else
                                _Where &= "   AND  " & _QryCon & " ='" & _DataCon & "'  "
                            End If

                        End If
                    End With

                End If
            Next
        End If

        '------------Browse Where Require Field---------------
        If _FTBrwCmdField <> "" Then
            For Each _QryCon As String In _FTBrwCmdField.Split(",")
                _DataCon = ""
                With GridView
                    If Not (.Columns.ColumnByFieldName(_QryCon.Trim()) Is Nothing) Then
                        Select Case .Columns.ColumnByFieldName(_QryCon.Trim()).ColumnType.FullName.ToString.ToUpper
                            Case Else
                                _DataCon = "" & .GetRowCellValue(.FocusedRowHandle, _QryCon.Trim())
                        End Select

                        If _Where = "" Then
                            _Where &= "     " & _QryCon & " ='" & _DataCon & "'  "
                        Else
                            _Where &= "   AND  " & _QryCon & " ='" & _DataCon & "'  "
                        End If
                    End If
                End With

            Next

        End If

        '------------Browse Where Require Field---------------

        '------------Browse Where Optional Field---------------
        If _FTBrwCmdFieldOptional <> "" Then
            For Each _QryCon As String In _FTBrwCmdFieldOptional.Split(",")
                _DataCon = ""
                With GridView
                    If Not (.Columns.ColumnByFieldName(_QryCon.Trim()) Is Nothing) Then
                        Select Case .Columns.ColumnByFieldName(_QryCon.Trim()).ColumnType.FullName.ToString.ToUpper
                            Case Else
                                _DataCon = "" & .GetRowCellValue(.FocusedRowHandle, _QryCon.Trim())
                        End Select

                        If _DataCon <> "" Then
                            If _Where = "" Then
                                _Where &= "     " & _QryCon & " ='" & _DataCon & "'  "
                            Else
                                _Where &= "   AND  " & _QryCon & " ='" & _DataCon & "'  "
                            End If
                        End If
                    End If
                End With
            Next
        End If

        '------------Browse Where Optional Field---------------
        If _Where <> "" Then
            If _BrowseWhereCmd = "" Then
                _Where = "   WHERE  " & _Where
            Else
                _Where = "   AND  " & _Where
            End If
        End If


        If Not (HI.ST.SysInfo.Admin) Then
            If Microsoft.VisualBasic.Left(ControlObject.name.ToString.ToUpper, 11) = "FNTSysEmpID".ToUpper Then
                _Where = HI.ST.Security.PermissionEmpData(_Where)
            ElseIf Microsoft.VisualBasic.Left(ControlObject.name.ToString.ToUpper, 15) = "FNTSysEmpTypeId".ToUpper Then
                _Where = HI.ST.Security.PermissionEmpType(_Where)
            End If
        End If

        _dtbrw.Dispose()

        Return _Where

    End Function

    Private Function W_PRCbValidateBFSaveData() As Boolean
        Dim _bRet As Boolean = False
        Try

            If Me.ogvAdjustSubFONoBreakdown.RowCount > 0 Then


                _bRet = True

            Else

                Select Case HI.ST.Lang.Language
                    Case HI.ST.Lang.eLang.TH
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, "ปรับปรุงข้อมูลรายการใบสั่งผลิต", "ไม่พบรายการข้อมูลใบสั่งผลิต !!!")
                    Case Else
                        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, "Adjust Factory Order No.", "Data not found !!!")


                End Select

            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        Return _bRet

    End Function


    Private Function W_PRCbLoadFONoSubBreakdown(Optional StateClearFilter As Boolean = True) As Boolean

        _bLoadFONoBreakdown = True
        Dim _Str As String
        If FNHSysBuyId.Text <> "" Then
            _Str = "SELECT TOP 1 FNHSysBuyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy WITH(NOLOCK) WHERE FTBuyCode ='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' "
            FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MERCHAN, "")
        End If

        Dim _FNHSysCmpId As Integer = Integer.Parse(Val(FNHSysCmpId.Properties.Tag.ToString()))
        Dim _FTPORef As String = FNHSysPOID.Text.Trim
        Dim _FNHSysStyleId As Integer = Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString))
        Dim _FNHSysCustId As Integer = Integer.Parse(Val(FNHSysCustId.Properties.Tag.ToString))
        Dim _FNHSysBuyId As Integer = Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString))
        Dim _FNHSysSeasonId As Integer = Integer.Parse(Val(FNHSysSeasonId.Properties.Tag.ToString))

        Dim bRetLoadFONoSubBreakdown As Boolean = False

        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand

            Dim oCmdSubOrderNoBreakdown As New SqlCommand

            oCmdSubOrderNoBreakdown.Connection = HI.Conn.SQLConn.Cnn
            oCmdSubOrderNoBreakdown.CommandType = CommandType.StoredProcedure
            oCmdSubOrderNoBreakdown.CommandText = "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[SP_GET_LIST_FACTORY_ADJUST_FOB_PRICE]"
            oCmdSubOrderNoBreakdown.Parameters.AddWithValue("@FTUserLogin", HI.ST.UserInfo.UserName)
            oCmdSubOrderNoBreakdown.Parameters.AddWithValue("@FTLang", HI.ST.Lang.Language.ToString())
            oCmdSubOrderNoBreakdown.Parameters.AddWithValue("@FTFilterOrderNo", "")
            oCmdSubOrderNoBreakdown.Parameters.AddWithValue("@FTFilterSubOrderNo", "")
            oCmdSubOrderNoBreakdown.Parameters.AddWithValue("@FNHSysCmpId", _FNHSysCmpId)
            oCmdSubOrderNoBreakdown.Parameters.AddWithValue("@FTPORef", _FTPORef)
            oCmdSubOrderNoBreakdown.Parameters.AddWithValue("@FNHSysStyleId", _FNHSysStyleId)
            oCmdSubOrderNoBreakdown.Parameters.AddWithValue("@FNHSysCustId", _FNHSysCustId)
            oCmdSubOrderNoBreakdown.Parameters.AddWithValue("@FNHSysBuyId", _FNHSysBuyId)
            oCmdSubOrderNoBreakdown.Parameters.AddWithValue("@FNHSysSeasonId", _FNHSysSeasonId)

            Dim sqlDA_SubOrderNoBreakdown As New SqlDataAdapter(oCmdSubOrderNoBreakdown.CommandText, HI.Conn.SQLConn._ConnString)

            sqlDA_SubOrderNoBreakdown.SelectCommand = oCmdSubOrderNoBreakdown

            oDBdtFactorySubOrderNoBreakdown = New System.Data.DataTable
            oDBdtFactorySubOrderNoBreakdown.TableName = _tTableFactorySubOrderNoBreakdown

            sqlDA_SubOrderNoBreakdown.Fill(oDBdtFactorySubOrderNoBreakdown)


            If (StateClearFilter) Then

                Me.ogvAdjustSubFONoBreakdown.ActiveFilter.Clear()

            End If

            Me.ogvAdjustSubFONoBreakdown.OptionsView.ShowGroupPanel = False

            Me.ogdAdjustSubFONoBreakdown.DataSource = oDBdtFactorySubOrderNoBreakdown
            'Me.ogvAdjustSubFONoBreakdown.OptionsView.ColumnAutoWidth = False
            Me.ogdAdjustSubFONoBreakdown.Refresh()
            Me.ogvAdjustSubFONoBreakdown.RefreshData()
            Me.ogvAdjustSubFONoBreakdown.BestFitColumns()

            Me.ogvAdjustSubFONoBreakdown.OptionsView.ColumnAutoWidth = False
            Me.ogvAdjustSubFONoBreakdown.RefreshData()

            'Me.ogdAdjustSubFONoBreakdown.ForceInitialize()

            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception

            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If

        End Try

        _bLoadFONoBreakdown = False

        Return bRetLoadFONoSubBreakdown

    End Function

    Private Function W_PRCnGETRowHandleByColumnValue(ByVal pGridView As DevExpress.XtraGrid.Views.Grid.GridView, ByVal ptColFieldName As String, ByVal pVal As Object) As Integer
        Dim nResult As Integer = DevExpress.XtraGrid.GridControl.InvalidRowHandle

        Dim nLoop As Integer
        For nLoop = 0 To pGridView.DataRowCount - 1
            If pGridView.GetDataRow(nLoop)(ptColFieldName).Equals(pVal) Then
                nResult = nLoop
                Exit For
            End If

        Next nLoop

        Return nResult

    End Function

    Private Function W_PRCBSaveAdjustFONoSubBreakdown(ByRef _dt As DataTable) As Boolean
        Dim bSaveFONoSubBreakdown As Boolean = False

        Dim _Spls As HI.TL.SplashScreen

        If Not System.Diagnostics.Debugger.IsAttached = True Then
            _Spls = New HI.TL.SplashScreen("Save data  Please Wait...")
        End If
        Dim _FTMatColorCode As String = ""
        Dim _FTMatSizeCode As String = ""
        Try
            Dim oStrBuilder As New System.Text.StringBuilder()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            Dim nFNHSysMatColorId As Integer, nFNHSysMatSizeId As Integer
            Dim nFNPrice As Double, nFNAmt As Double, nFNExtraQty As Double, nFNAmntExtra As Double, nFNGrandAmnt As Double, nFNGarmentQtyTest As Double
            Dim nFNQuantity As Integer, nFNQuantityExtra As Integer, nFNGrandQuantity As Integer
            Dim _FTCustomerPO As String = ""
            Dim tFTOrderNoBreakdownPrv As String, tFTSubOrderNoBreakdownPrv As String
            Dim _FTStateHold As String = ""

            tFTOrderNoBreakdownPrv = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(0, "FTOrderNo").ToString()
            tFTSubOrderNoBreakdownPrv = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(0, "FTSubOrderNo").ToString()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("DECLARE @FTUpdUser AS NVARCHAR(30);")
            oStrBuilder.AppendLine("DECLARE @FDUpdDate AS NVARCHAR(50);")
            oStrBuilder.AppendLine("DECLARE @FTUpdTime AS NVARCHAR(50);")
            oStrBuilder.AppendLine(String.Format("SET @FTUpdUser = N'{0}';", HI.ST.UserInfo.UserName))
            oStrBuilder.AppendLine("SELECT @FDUpdDate = CONVERT(VARCHAR(10),GETDATE(),111);")
            oStrBuilder.AppendLine("SELECT @FTUpdTime = CONVERT(VARCHAR(8),GETDATE(),114);")

            For nLoopAdjustBreakdown As Integer = 0 To Me.ogvAdjustSubFONoBreakdown.DataRowCount - 1

                Dim tFTOrderNoBreakdown As String, tFTSubOrderNoBreakdown As String

                Try
                    Dim oDataRowBreakdown As DataRow = Me.ogvAdjustSubFONoBreakdown.GetDataRow(nLoopAdjustBreakdown)

                    If Not oDataRowBreakdown Is Nothing Then

                        tFTOrderNoBreakdown = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTOrderNo").ToString()
                        tFTSubOrderNoBreakdown = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTSubOrderNo").ToString()
                        _FTCustomerPO = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTCustomerPO").ToString()

                        If tFTOrderNoBreakdown <> tFTOrderNoBreakdownPrv And tFTSubOrderNoBreakdown <> tFTSubOrderNoBreakdownPrv Then
                            tFTOrderNoBreakdownPrv = tFTOrderNoBreakdown
                            tFTSubOrderNoBreakdownPrv = tFTSubOrderNoBreakdown
                        End If

                        nFNHSysMatColorId = Val(oDataRowBreakdown.Item("FNHSysMatColorId"))
                        nFNHSysMatSizeId = Val(oDataRowBreakdown.Item("FNHSysMatSizeId"))

                        _FTMatColorCode = oDataRowBreakdown.Item("FTMatColorCode").ToString
                        _FTMatSizeCode = oDataRowBreakdown.Item("FTMatSizeCode").ToString
                        _FTStateHold = oDataRowBreakdown.Item("FTStateHold").ToString

                        nFNPrice = 0 : nFNAmt = 0 : nFNExtraQty = 0 : nFNAmntExtra = 0 : nFNGrandAmnt = 0
                        nFNQuantity = 0 : nFNQuantityExtra = 0 : nFNGrandQuantity = 0 : nFNGarmentQtyTest = 0
                        nFNGarmentQtyTest = 0

                        ' If HI.MER.ValidateExportXML.CheckExportFileXML(tFTOrderNoBreakdown, tFTSubOrderNoBreakdown) = False Then
                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNQuantity")) Then
                            nFNQuantity = Val(oDataRowBreakdown.Item("FNQuantity"))
                        Else
                            nFNQuantity = 0
                        End If

                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNPrice")) Then
                            nFNPrice = Val(oDataRowBreakdown.Item("FNPrice"))
                        Else
                            nFNPrice = 0
                        End If

                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNAmt")) Then
                            nFNAmt = Val(oDataRowBreakdown.Item("FNAmt"))
                        Else
                            nFNAmt = 0
                        End If

                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNExtraQty")) Then
                            nFNExtraQty = Val(oDataRowBreakdown.Item("FNExtraQty"))
                        Else
                            nFNExtraQty = 0
                        End If

                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNQuantityExtra")) Then
                            nFNQuantityExtra = Val(oDataRowBreakdown.Item("FNQuantityExtra"))
                        Else
                            nFNQuantityExtra = 0
                        End If

                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGrandQuantity")) Then
                            nFNGrandQuantity = Val(oDataRowBreakdown.Item("FNGrandQuantity"))
                        Else
                            nFNGrandQuantity = 0
                        End If

                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNAmntExtra")) Then
                            nFNAmntExtra = Val(oDataRowBreakdown.Item("FNAmntExtra"))
                        Else
                            nFNAmntExtra = 0
                        End If

                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGrandAmnt")) Then
                            nFNGrandAmnt = Val(oDataRowBreakdown.Item("FNGrandAmnt"))
                        Else
                            nFNGrandAmnt = 0
                        End If

                        If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGarmentQtyTest")) Then
                            nFNGarmentQtyTest = Val(oDataRowBreakdown.Item("FNGarmentQtyTest"))
                        Else
                            nFNGarmentQtyTest = 0
                        End If

                        Dim _FNAmntQtyTest As Double = 0
                        Try
                            _FNAmntQtyTest = Double.Parse(Format(Val(nFNGarmentQtyTest) * Val(nFNPrice), "0.00"))
                        Catch ex As Exception
                        End Try

                        Dim _CMDisPer As Double = GetCMDisPer(tFTOrderNoBreakdown)
                        Dim _FNOperateFee As Double = GetOperateFee(tFTOrderNoBreakdown)

                        If tFTSubOrderNoBreakdown <> Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTSubOrderNoRef").ToString() Then

                            oStrBuilder.AppendLine("UPDATE A")
                            oStrBuilder.AppendLine("SET  ")
                            oStrBuilder.AppendLine(String.Format("    A.FNPrice = {0}", nFNPrice))
                            oStrBuilder.AppendLine(String.Format("  , A.FTStateHold = N'{0}'", _FTStateHold))
                            'oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisPer = {0}", _CMDisPer))
                            'oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisAmt = {0}", (nFNPrice * _CMDisPer) / 100))
                            oStrBuilder.AppendLine(String.Format("  ,  A.FNOperateFee = {0}", _FNOperateFee))
                            oStrBuilder.AppendLine(String.Format("  ,  A.FNOperateFeeAmt = {0}", (nFNPrice * _FNOperateFee) / 100))
                            oStrBuilder.AppendLine(String.Format("  ,  A.FNNetFOB = {0}", nFNPrice - CDbl(Format(((nFNPrice * _FNOperateFee) / 100), "0.0000"))))
                            ' oStrBuilder.AppendLine("  ,  A.FNExternalAmntQtyTest = Convert(numeric(18,2),(ISNULL(A.FNExternalQtyTest,0) * " & nFNPrice & ") ) ")
                            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Divert_BreakDown] AS A")
                            oStrBuilder.AppendLine(String.Format("WHERE A.FTOrderNo = N'{0}'", tFTOrderNoBreakdown))
                            oStrBuilder.AppendLine(String.Format("      AND (A.FTSubOrderNo+ '-D' +Convert(nvarchar(30),A.FNDivertSeq )) = N'{0}'", tFTSubOrderNoBreakdown))
                            oStrBuilder.AppendLine(String.Format("      AND A.FTColorway = N'{0}'", _FTMatColorCode))
                            oStrBuilder.AppendLine(String.Format("      AND A.FTSizeBreakDown = N'{0}';", _FTMatSizeCode))

                        Else

                            oStrBuilder.AppendLine(" UPDATE A")
                            oStrBuilder.AppendLine(" SET A.FTUpdUser = @FTUpdUser,")
                            oStrBuilder.AppendLine("    A.FDUpdDate = @FDUpdDate,")
                            oStrBuilder.AppendLine("    A.FTUpdTime = @FTUpdTime,")
                            oStrBuilder.AppendLine(String.Format("    A.FNPrice = {0},", nFNPrice))
                            oStrBuilder.AppendLine(String.Format("    A.FNAmt = {0},", nFNAmt))
                            oStrBuilder.AppendLine(String.Format("    A.FNAmntExtra = {0},", nFNAmntExtra))
                            oStrBuilder.AppendLine(String.Format("    A.FNGrandAmnt = {0}", nFNGrandAmnt))
                            oStrBuilder.AppendLine(String.Format("  ,  A.FNAmntQtyTest = {0}", _FNAmntQtyTest))
                            oStrBuilder.AppendLine(String.Format("  , A.FTStateHold = N'{0}'", _FTStateHold))
                            'oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisPer = {0}", _CMDisPer))
                            'oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisAmt = {0}", (nFNPrice * _CMDisPer) / 100))
                            oStrBuilder.AppendLine(String.Format("  ,  A.FNOperateFee = {0}", _FNOperateFee))
                            oStrBuilder.AppendLine(String.Format("  ,  A.FNOperateFeeAmt = {0}", (nFNPrice * _FNOperateFee) / 100))
                            oStrBuilder.AppendLine(String.Format("  ,  A.FNNetFOB = {0}", nFNPrice - CDbl(Format(((nFNPrice * _FNOperateFee) / 100), "0.0000"))))

                            oStrBuilder.AppendLine("  ,  A.FNExternalAmntQtyTest = Convert(numeric(18,2),(ISNULL(A.FNExternalQtyTest,0) * " & nFNPrice & ") ) ")

                            oStrBuilder.AppendLine(" FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS A")
                            oStrBuilder.AppendLine(String.Format("WHERE A.FTOrderNo = N'{0}'", tFTOrderNoBreakdown))
                            oStrBuilder.AppendLine(String.Format("      AND A.FTSubOrderNo = N'{0}'", tFTSubOrderNoBreakdown))
                            oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatColorId = {0}", nFNHSysMatColorId))
                            oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatSizeId = {0};", nFNHSysMatSizeId))

                        End If

                        'Else
                        '    If _dt.Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(tFTOrderNoBreakdown) & "' AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(tFTSubOrderNoBreakdown) & "' AND FTCustomerPO='" & HI.UL.ULF.rpQuoted(_FTCustomerPO) & "'").Length <= 0 Then
                        '        _dt.Rows.Add(tFTOrderNoBreakdown, tFTSubOrderNoBreakdown, _FTCustomerPO)
                        '    End If
                        'End If


                    End If

                Catch ex As Exception
                    If System.Diagnostics.Debugger.IsAttached = True Then
                        Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
                    End If
                End Try

                '  Call SaveCMPrice(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FNHSysStyleId").ToString())

            Next nLoopAdjustBreakdown

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            tSql = ""
            tSql = oStrBuilder.ToString()

            If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                bSaveFONoSubBreakdown = False

            Else

                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                bSaveFONoSubBreakdown = True
            End If

        Catch ex As Exception

        End Try

        If Not System.Diagnostics.Debugger.IsAttached = True Then
            _Spls.Close()
        End If

        Return bSaveFONoSubBreakdown

    End Function
    Private Sub W_PRCxRecalSubOrderBreakdownExtraQty(ByVal pnRowHandle As Integer)
        Try
            Dim nFNQuantity As Integer, nFNQuantityExtra As Integer, nFNGrandQuantity As Integer
            Dim nFNPrice As Double, nFNExtraQty As Double, nFNAmt As Double, nFNAmntExtra As Double, nFNGrandAmnt As Double

            nFNQuantity = 0
            nFNPrice = 0
            nFNAmt = 0
            nFNExtraQty = 0

            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(pnRowHandle, "FNQuantity")) Then
                nFNQuantity = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(pnRowHandle, "FNQuantity"))
            End If

            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(pnRowHandle, "FNPrice")) Then
                nFNPrice = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(pnRowHandle, "FNPrice"))
            End If

            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(pnRowHandle, "FNAmt")) Then
                nFNAmt = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(pnRowHandle, oColFNAmt))
            End If

            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(pnRowHandle, "FNExtraQty")) Then
                nFNExtraQty = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(pnRowHandle, "FNExtraQty"))
            End If


            If nFNQuantity = 0 Then
                nFNQuantityExtra = 0
            Else
                If nFNExtraQty = 0 Then
                    nFNQuantityExtra = 0

                Else
                    Dim nFractionNumber#
                    nFractionNumber = nFNQuantity * (nFNExtraQty / 100)

                    Dim tRetExtraQty$ = CStr(nFractionNumber)

                    Dim nPosDigit% = Microsoft.VisualBasic.InStr(tRetExtraQty, ".")

                    If nPosDigit = 0 Then
                        nFNQuantityExtra = CDbl(nFractionNumber)
                    Else
                        Dim tFractionInt$ = Microsoft.VisualBasic.Mid$(tRetExtraQty, 1, nPosDigit - 1)
                        Dim tFractionDecimal$ = Microsoft.VisualBasic.Mid$(tRetExtraQty, nPosDigit + 1, Len(tRetExtraQty) - nPosDigit)

                        Dim nReal#
                        Double.TryParse(tFractionDecimal, nReal)

                        If nReal > 0 Then
                            Dim nFractionInt#
                            Double.TryParse(tFractionInt, nFractionInt)
                            nFNQuantityExtra = CDbl(IIf(nFractionInt < 0, 0, nFractionInt) + 1)
                        Else
                            Dim nFractionInt#
                            Double.TryParse(tFractionInt, nFractionInt)

                            nFNQuantityExtra = CDbl(IIf(nFractionInt < 0, 0, nFractionInt))
                        End If

                    End If

                End If

            End If

            '...update FNQuantityExtra
            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(pnRowHandle, oColFNQuantityExtra, nFNQuantityExtra)
            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(pnRowHandle, oColFNQuantityExtra)
            '...update FNGrandQuantity
            nFNGrandQuantity = nFNQuantity + nFNQuantityExtra
            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(pnRowHandle, oColFNGrandQuantity, nFNGrandQuantity)
            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(pnRowHandle, oColFNGrandQuantity)
            '...update FNAmntExtra
            nFNAmntExtra = nFNPrice * nFNQuantityExtra
            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(pnRowHandle, oColFNAmntExtra, nFNAmntExtra)
            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(pnRowHandle, oColFNAmntExtra)
            '...update FNGrandAmnt
            nFNGrandAmnt = nFNAmt + nFNAmntExtra
            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(pnRowHandle, oColFNGrandAmnt, nFNGrandAmnt)
            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(pnRowHandle, oColFNGrandAmnt)

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & ControlChars.CrLf & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

    End Sub

#End Region


    Private Sub wAdjustAfterImportOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try


            Me.ogdAdjustSubFONoBreakdown.DataSource = Nothing
            'Me.ogvAdjustSubFONoBreakdown.OptionsView.ColumnAutoWidth = False
            Me.ogdAdjustSubFONoBreakdown.Refresh()
            Me.ogvAdjustSubFONoBreakdown.RefreshData()
            Me.ogvAdjustSubFONoBreakdown.BestFitColumns()

            Me.ogvAdjustSubFONoBreakdown.OptionsView.ColumnAutoWidth = False
            Me.ogvAdjustSubFONoBreakdown.RefreshData()

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
            End If
        End Try

    End Sub

    Private Sub ogvAdjustSubFONoBreakdown_CellValueChanged(sender As Object, e As Views.Base.CellValueChangedEventArgs) Handles ogvAdjustSubFONoBreakdown.CellValueChanged

        If _bLoadFONoBreakdown = True Then Exit Sub

        Try
            Dim nRowHandle As Integer
            nRowHandle = e.RowHandle

            Dim bReCalculateColValChange As Boolean = False

            If Not bReCalculateColValChange Then

                bReCalculateColValChange = True

                Select Case e.Column.FieldName
                    Case "FNQuantity"
                        If nRowHandle >= 0 Then
                            Dim nFNQuantity As Integer = 0
                            Dim nFNQuantityExtra As Integer = 0
                            Dim nFNGrandQuantity As Integer = 0
                            Dim nFNExtraQty As Double = 0
                            Dim nFNPrice As Double = 0
                            Dim nFNAmt As Double = 0
                            Dim nFNAmntExtra As Double = 0
                            Dim nFNGrandAmnt As Double = 0

                            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNQuantity")) Then
                                nFNQuantity = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNQuantity"))
                            End If

                            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNPrice")) Then
                                nFNPrice = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNPrice"))
                            End If

                            nFNAmt = nFNQuantity * nFNPrice

                            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNExtraQty")) Then
                                nFNExtraQty = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNExtraQty"))
                            End If

                            '...re-calculate quantity extra
                            If nFNQuantity = 0 Then
                                nFNQuantityExtra = 0
                            Else
                                If nFNExtraQty = 0 Then
                                    nFNQuantityExtra = 0
                                Else
                                    Dim nFractionNumber#
                                    nFractionNumber = nFNQuantity * (nFNExtraQty / 100)

                                    Dim tRetExtraQty$ = CStr(nFractionNumber)

                                    Dim nPosDigit% = Microsoft.VisualBasic.InStr(tRetExtraQty, ".")

                                    If nPosDigit = 0 Then
                                        nFNQuantityExtra = CDbl(nFractionNumber)
                                    Else
                                        Dim tFractionInt$ = Microsoft.VisualBasic.Mid$(tRetExtraQty, 1, nPosDigit - 1)
                                        Dim tFractionDecimal$ = Microsoft.VisualBasic.Mid$(tRetExtraQty, nPosDigit + 1, Len(tRetExtraQty) - nPosDigit)

                                        Dim nReal#
                                        Double.TryParse(tFractionDecimal, nReal)

                                        If nReal > 0 Then
                                            Dim nFractionInt#
                                            Double.TryParse(tFractionInt, nFractionInt)
                                            nFNQuantityExtra = CDbl(IIf(nFractionInt < 0, 0, nFractionInt) + 1)
                                        Else
                                            Dim nFractionInt#
                                            Double.TryParse(tFractionInt, nFractionInt)

                                            nFNQuantityExtra = CDbl(IIf(nFractionInt < 0, 0, nFractionInt))
                                        End If

                                    End If

                                End If

                            End If

                            '...re-calculate grand quantity
                            nFNGrandQuantity = nFNQuantity + nFNQuantityExtra

                            '...re-calculate amount from extra quantity
                            nFNAmntExtra = nFNPrice * nFNQuantityExtra

                            '...re-calculate grand amount
                            nFNGrandAmnt = nFNAmt + nFNAmntExtra

                            '...update row cell value
                            '------------------------------------------------------------------------
                            '...update FNAmt
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, "FNAmt", nFNAmt)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNAmt)
                            '...update FNQuantityExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNQuantityExtra, nFNQuantityExtra)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNQuantityExtra)
                            '...update FNGrandQuantity
                            nFNGrandQuantity = nFNQuantity + nFNQuantityExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNGrandQuantity, nFNGrandQuantity)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNGrandQuantity)
                            '...update FNAmntExtra
                            nFNAmntExtra = nFNPrice * nFNQuantityExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNAmntExtra, nFNAmntExtra)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNAmntExtra)
                            '...update FNGrandAmnt
                            nFNGrandAmnt = nFNAmt + nFNAmntExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNGrandAmnt, nFNGrandAmnt)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNGrandAmnt)
                            '--------------------------------------------------------------------------

                        End If

                    Case "FNPrice"
                        If nRowHandle >= 0 Then
                            Dim nFNQuantity As Integer = 0
                            Dim nFNQuantityExtra As Integer = 0
                            Dim nFNGrandQuantity As Integer = 0
                            Dim nFNExtraQty As Double = 0
                            Dim nFNPrice As Double = 0
                            Dim nFNAmt As Double = 0
                            Dim nFNAmntExtra As Double = 0
                            Dim nFNGrandAmnt As Double = 0

                            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNQuantity")) Then
                                nFNQuantity = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNQuantity"))
                            End If

                            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNPrice")) Then
                                nFNPrice = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNPrice"))
                            End If

                            nFNAmt = nFNQuantity * nFNPrice

                            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNQuantityExtra")) Then
                                nFNQuantityExtra = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNQuantityExtra"))
                            End If

                            '...re-calculate amount from extra quantity
                            nFNAmntExtra = nFNPrice * nFNQuantityExtra

                            '...re-calculate grand amount
                            nFNGrandAmnt = nFNAmt + nFNAmntExtra

                            '...update row cell value
                            '------------------------------------------------------------------------
                            '...update FNAmt
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, "FNAmt", nFNAmt)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNAmt)

                            '...update FNAmntExtra
                            nFNAmntExtra = nFNPrice * nFNQuantityExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNAmntExtra, nFNAmntExtra)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNAmntExtra)

                            '...update FNGrandAmnt
                            nFNGrandAmnt = nFNAmt + nFNAmntExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNGrandAmnt, nFNGrandAmnt)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNGrandAmnt)
                            '--------------------------------------------------------------------------

                        End If

                    Case "FNExtraQty"

                        If nRowHandle >= 0 Then
                            Dim nFNQuantity As Integer = 0
                            Dim nFNQuantityExtra As Integer = 0
                            Dim nFNGrandQuantity As Integer = 0
                            Dim nFNExtraQty As Double = 0
                            Dim nFNPrice As Double = 0
                            Dim nFNAmt As Double = 0
                            Dim nFNAmntExtra As Double = 0
                            Dim nFNGrandAmnt As Double = 0

                            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNQuantity")) Then
                                nFNQuantity = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNQuantity"))
                            End If

                            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNPrice")) Then
                                nFNPrice = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNPrice"))
                            End If

                            nFNAmt = nFNQuantity * nFNPrice

                            If Not DBNull.Value.Equals(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNExtraQty")) Then
                                nFNExtraQty = Val(Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nRowHandle, "FNExtraQty"))
                            End If

                            '...re-calculate quantity extra
                            If nFNQuantity = 0 Then
                                nFNQuantityExtra = 0
                            Else
                                If nFNExtraQty = 0 Then
                                    nFNQuantityExtra = 0
                                Else
                                    Dim nFractionNumber#
                                    nFractionNumber = nFNQuantity * (nFNExtraQty / 100)

                                    Dim tRetExtraQty$ = CStr(nFractionNumber)

                                    Dim nPosDigit% = Microsoft.VisualBasic.InStr(tRetExtraQty, ".")

                                    If nPosDigit = 0 Then
                                        nFNQuantityExtra = CDbl(nFractionNumber)
                                    Else
                                        Dim tFractionInt$ = Microsoft.VisualBasic.Mid$(tRetExtraQty, 1, nPosDigit - 1)
                                        Dim tFractionDecimal$ = Microsoft.VisualBasic.Mid$(tRetExtraQty, nPosDigit + 1, Len(tRetExtraQty) - nPosDigit)

                                        Dim nReal#
                                        Double.TryParse(tFractionDecimal, nReal)

                                        If nReal > 0 Then
                                            Dim nFractionInt#
                                            Double.TryParse(tFractionInt, nFractionInt)
                                            nFNQuantityExtra = CDbl(IIf(nFractionInt < 0, 0, nFractionInt) + 1)
                                        Else
                                            Dim nFractionInt#
                                            Double.TryParse(tFractionInt, nFractionInt)

                                            nFNQuantityExtra = CDbl(IIf(nFractionInt < 0, 0, nFractionInt))
                                        End If

                                    End If

                                End If

                            End If

                            '...re-calculate grand quantity
                            nFNGrandQuantity = nFNQuantity + nFNQuantityExtra

                            '...re-calculate amount from extra quantity
                            nFNAmntExtra = nFNPrice * nFNQuantityExtra

                            '...re-calculate grand amount
                            nFNGrandAmnt = nFNAmt + nFNAmntExtra

                            '...update row cell value
                            '------------------------------------------------------------------------
                            '...update FNQuantityExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNQuantityExtra, nFNQuantityExtra)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNQuantityExtra)

                            '...update FNGrandQuantity
                            nFNGrandQuantity = nFNQuantity + nFNQuantityExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNGrandQuantity, nFNGrandQuantity)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNGrandQuantity)

                            '...update FNAmntExtra
                            nFNAmntExtra = nFNPrice * nFNQuantityExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNAmntExtra, nFNAmntExtra)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNAmntExtra)

                            '...update FNGrandAmnt
                            nFNGrandAmnt = nFNAmt + nFNAmntExtra
                            Me.ogvAdjustSubFONoBreakdown.SetRowCellValue(nRowHandle, oColFNGrandAmnt, nFNGrandAmnt)
                            Me.ogvAdjustSubFONoBreakdown.RefreshRowCell(nRowHandle, oColFNGrandAmnt)
                            '--------------------------------------------------------------------------

                        End If

                End Select

                bReCalculateColValChange = False

            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

    End Sub


    Private Sub ogvAdjustSubFONoBreakdown_CustomDrawFooterCell(sender As Object, e As FooterCellCustomDrawEventArgs) Handles ogvAdjustSubFONoBreakdown.CustomDrawFooterCell
        Try
            If e.Column.FieldName = "FTOrderNo" Then

                If e.Info.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count Then

                    If Me.ogvAdjustSubFONoBreakdown.DataRowCount > 0 Then

                        If Me.ogvAdjustSubFONoBreakdown.ActiveFilter Is Nothing Or Me.ogvAdjustSubFONoBreakdown.ActiveFilterEnabled = False Or Me.ogvAdjustSubFONoBreakdown.ActiveFilter.Expression = "" Then

                            Dim oDBdvFONoView As New System.Data.DataView
                            oDBdvFONoView = CType(Me.ogvAdjustSubFONoBreakdown.DataSource, System.Data.DataView)

                            If oDBdvFONoView.Count > 0 Then
                                Dim oDBdtFONo As System.Data.DataTable
                                oDBdtFONo = oDBdvFONoView.ToTable()

                                If Not oDBdtFONo Is Nothing And oDBdtFONo.Rows.Count > 0 Then
                                    Dim oListFTOrderNo As List(Of String) = oDBdtFONo.AsEnumerable() _
                                                                             .Select(Function(r) r.Field(Of String)("FTOrderNo")) _
                                                                             .Distinct() _
                                                                             .ToList()

                                    If Not oListFTOrderNo Is Nothing And oListFTOrderNo.Count > 0 Then
                                        e.Info.DisplayText = oListFTOrderNo.Count
                                    Else
                                        e.Info.DisplayText = ""
                                    End If
                                Else
                                    e.Info.DisplayText = ""
                                End If
                            Else
                                e.Info.DisplayText = ""
                            End If

                        Else
                            '...count distinct Factory Order No. by row filter criteria
                            Dim oDBdtFONoCriteria As System.Data.DataTable = CType(Me.ogvAdjustSubFONoBreakdown.GridControl.DataSource, System.Data.DataTable)
                            Dim oDataViewFilterData As New System.Data.DataView(oDBdtFONoCriteria)

                            oDataViewFilterData.RowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(Me.ogvAdjustSubFONoBreakdown.ActiveFilterCriteria)

                            If Not oDataViewFilterData Is Nothing And oDataViewFilterData.Count > 0 Then
                                Dim oDBdtFONoRowFilter As System.Data.DataTable
                                oDBdtFONoRowFilter = oDataViewFilterData.ToTable()

                                If Not oDBdtFONoRowFilter Is Nothing And oDBdtFONoRowFilter.Rows.Count > 0 Then
                                    Dim oListFTOrderNoRowFilter As List(Of String) = oDBdtFONoRowFilter.AsEnumerable() _
                                                                             .Select(Function(r) r.Field(Of String)("FTOrderNo")) _
                                                                             .Distinct() _
                                                                             .ToList()

                                    If Not oListFTOrderNoRowFilter Is Nothing And oListFTOrderNoRowFilter.Count > 0 Then
                                        e.Info.DisplayText = oListFTOrderNoRowFilter.Count
                                    Else
                                        e.Info.DisplayText = ""
                                    End If

                                Else
                                    e.Info.DisplayText = ""
                                End If

                            Else
                                e.Info.DisplayText = ""
                            End If

                        End If

                    Else
                        e.Info.DisplayText = ""
                    End If

                    'e.Handled = True

                End If

            ElseIf e.Column.FieldName.Equals("FTSubOrderNo") Then

                If e.Info.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count Then

                    If Me.ogvAdjustSubFONoBreakdown.DataRowCount > 0 Then

                        If Me.ogvAdjustSubFONoBreakdown.ActiveFilter Is Nothing Or Me.ogvAdjustSubFONoBreakdown.ActiveFilterEnabled = False Or Me.ogvAdjustSubFONoBreakdown.ActiveFilter.Expression = "" Then

                            Dim oDBdvFONoView As New System.Data.DataView
                            oDBdvFONoView = CType(Me.ogvAdjustSubFONoBreakdown.DataSource, System.Data.DataView)

                            If oDBdvFONoView.Count > 0 Then
                                Dim oDBdtFONo As System.Data.DataTable
                                oDBdtFONo = oDBdvFONoView.ToTable()

                                If Not oDBdtFONo Is Nothing And oDBdtFONo.Rows.Count > 0 Then
                                    Dim oListFTSubOrderNo As List(Of String) = oDBdtFONo.AsEnumerable() _
                                                                             .Select(Function(r) r.Field(Of String)("FTSubOrderNo")) _
                                                                             .Distinct() _
                                                                             .ToList()

                                    If Not oListFTSubOrderNo Is Nothing And oListFTSubOrderNo.Count > 0 Then
                                        e.Info.DisplayText = oListFTSubOrderNo.Count
                                    Else
                                        e.Info.DisplayText = ""
                                    End If
                                Else
                                    e.Info.DisplayText = ""
                                End If
                            Else
                                e.Info.DisplayText = ""
                            End If

                        Else
                            '...count distinct Factory Order No. by row filter criteria
                            Dim oDBdtFONoCriteria As System.Data.DataTable = CType(Me.ogvAdjustSubFONoBreakdown.GridControl.DataSource, System.Data.DataTable)
                            Dim oDataViewFilterData As New System.Data.DataView(oDBdtFONoCriteria)

                            oDataViewFilterData.RowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(Me.ogvAdjustSubFONoBreakdown.ActiveFilterCriteria)

                            If Not oDataViewFilterData Is Nothing And oDataViewFilterData.Count > 0 Then
                                Dim oDBdtFONoRowFilter As System.Data.DataTable
                                oDBdtFONoRowFilter = oDataViewFilterData.ToTable()

                                If Not oDBdtFONoRowFilter Is Nothing And oDBdtFONoRowFilter.Rows.Count > 0 Then
                                    Dim oListFTSubOrderNoRowFilter As List(Of String) = oDBdtFONoRowFilter.AsEnumerable() _
                                                                             .Select(Function(r) r.Field(Of String)("FTSubOrderNo")) _
                                                                             .Distinct() _
                                                                             .ToList()

                                    If Not oListFTSubOrderNoRowFilter Is Nothing And oListFTSubOrderNoRowFilter.Count > 0 Then
                                        e.Info.DisplayText = oListFTSubOrderNoRowFilter.Count
                                    Else
                                        e.Info.DisplayText = ""
                                    End If

                                Else
                                    e.Info.DisplayText = ""
                                End If

                            Else
                                e.Info.DisplayText = ""
                            End If

                        End If

                    Else
                        e.Info.DisplayText = ""
                    End If

                    'e.Handled = True

                End If

            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

    End Sub

    Private Sub ogvAdjustSubFONoBreakdown_CustomRowFilter(sender As Object, e As Views.Base.RowFilterEventArgs) Handles ogvAdjustSubFONoBreakdown.CustomRowFilter

    End Sub

    'Private Function FooterCellCustomDrawEventArgs() As Object
    '    Throw New NotImplementedException
    'End Function

    Private Sub ogvAdjustSubFONoBreakdown_MouseDown(sender As Object, e As MouseEventArgs) Handles ogvAdjustSubFONoBreakdown.MouseDown
        Try

            Dim hitInfo = Me.ogvAdjustSubFONoBreakdown.CalcHitInfo(e.Location)

            If hitInfo.InRowCell = True Then
                Dim rowHandle As Integer = hitInfo.RowHandle
                Dim column As DevExpress.XtraGrid.Columns.GridColumn = hitInfo.Column

            Else

            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ogdAdjustFONo_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub FNHSysCmpId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCmpId.EditValueChanged

    End Sub

    Private Sub otcFactoryOrderNo_SelectedPageChanging(sender As Object, e As XtraTab.TabPageChangingEventArgs) Handles otcFactoryOrderNo.SelectedPageChanging
        'Select Case e.Page.Name
        '    Case otbSubOrderNo.Name
        '        Try
        '            Dim oDBdv As System.Data.DataView
        '            oDBdv = W_PRCdvGETViewFilterData(Me.ogvAdjustFONo)
        '            If Not oDBdv Is Nothing Then
        '                '...refresh datasource for gridview sub order no and sub order no breakdown
        '                Dim tFTFilterFONoCriteria As String = ""
        '                For Each oRowView As DataRowView In oDBdv
        '                    Dim oDataRow As DataRow = oRowView.Row
        '                    tFTFilterFONoCriteria = tFTFilterFONoCriteria & oDataRow.Item("FTOrderNo").ToString() & "|"
        '                Next

        '                If tFTFilterFONoCriteria <> "" Then
        '                    tFTFilterFONoCriteria = Microsoft.VisualBasic.Mid$(tFTFilterFONoCriteria, 1, Len(tFTFilterFONoCriteria) - 1)
        '                    Call W_PRCbLoadFONoSub(tFTFilterFONoCriteria)
        '                    'Call W_PRCbLoadFONoSubBreakdown(tFTFilterFONoCriteria)
        '                End If

        '            End If
        '        Catch ex As Exception
        '            If System.Diagnostics.Debugger.IsAttached = True Then
        '                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
        '            End If
        '        End Try
        '    Case otpSubOrderNoBreakdown.Name

        '        Try
        '            Dim oDBdv As System.Data.DataView
        '            oDBdv = W_PRCdvGETViewFilterData(Me.ogvAdjustSubFONo)
        '            If Not oDBdv Is Nothing Then
        '                '...refresh datasource for gridview sub order no and sub order no breakdown
        '                Dim tFTFilterFONoSubCriteria As String = ""
        '                For Each oRowView As DataRowView In oDBdv
        '                    Dim oDataRow As DataRow = oRowView.Row
        '                    REM 2014/06/14 tFTFilterFONoCriteria = tFTFilterFONoCriteria & oDataRow.Item("FTOrderNo").ToString() & "|"
        '                    tFTFilterFONoSubCriteria = tFTFilterFONoSubCriteria & oDataRow.Item("FTSubOrderNo").ToString() & "|"
        '                Next

        '                If tFTFilterFONoSubCriteria <> "" Then
        '                    tFTFilterFONoSubCriteria = Microsoft.VisualBasic.Mid$(tFTFilterFONoSubCriteria, 1, Len(tFTFilterFONoSubCriteria) - 1)
        '                    Call W_PRCbLoadFONoSubBreakdown("", tFTFilterFONoSubCriteria)
        '                End If

        '            End If
        '        Catch ex As Exception
        '            If System.Diagnostics.Debugger.IsAttached = True Then
        '                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
        '            End If
        '        End Try

        'End Select
    End Sub


    Private Sub ocmSaveFOB_Click(sender As Object, e As EventArgs) Handles ocmSave.Click
        Try
            PDataTableTmp = New DataTable
            With PDataTableTmp.Columns
                .Add("FNHSysStyleId", GetType(Integer))
                .Add("FNHSysSeasonId", GetType(Integer))
            End With

            Dim _dt As DataTable = CreateDataTableCheckXML()

            If UpdateSubBreakdown(_dt) Then
                ' Call UpDateMasterStyle()

                Call W_PRCbLoadFONoSubBreakdown(False)

            End If

            If _dt.Rows.Count > 0 Then

                HI.MG.ShowMsg.mInfo("พบข้อมูลการส่งออก XML File แล้ว ในบางรายการ ไม่สามารถเปลี่ยนแปลงหรือแก้ไขข้อมูลได้ !!!", 1601010576, Me.Text, , MessageBoxIcon.Warning)

                With _ListPopup
                    HI.ST.Lang.SP_SETxLanguage(_ListPopup)
                    .ogdlist.DataSource = _dt.Copy
                    .ogdlist.Refresh()
                    .ShowDialog()
                End With

            End If

            _dt.Dispose()
        Catch ex As Exception
        End Try
    End Sub

    Private Function UpDateMasterStyle() As Boolean
        Try
            Dim _Cmd As String = "" : Dim _oDt As DataTable
            For Each x As DataRow In PDataTableTmp.Rows

                _Cmd = "Select Max(B.FNCMDisAmt) AS FNCMDisAmt From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_BreakDown AS B WITH(NOLOCK) "
                _Cmd &= vbCrLf & "          INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS B4 WITH(NOLOCK) ON B.FTOrderNo = B4.FTOrderNo  AND B.FTSubOrderNo = B4.FTSubOrderNo "
                _Cmd &= vbCrLf & " INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON B4.FTOrderNo = O.FTOrderNo"
                _Cmd &= vbCrLf & " WHERE O.FNHSysStyleId=" & CInt("0" & x!FNHSysStyleId.ToString)
                _Cmd &= vbCrLf & " AND O.FNHSysSeasonId=" & CInt("0" & x!FNHSysSeasonId.ToString)
                _Cmd &= vbCrLf & "  AND B4.FNHSysCurId IN (SELECT FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS C WITH(NOLOCK) WHERE ISNULL(FTStateLocal,'0') <>'1')"

                _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN)

                For Each R As DataRow In _oDt.Rows

                    _Cmd = "Update [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle"
                    _Cmd &= vbCrLf & "Set FNCMDisAmt=" & CDbl("0" & R!FNCMDisAmt.ToString)
                    _Cmd &= vbCrLf & ", FNNetCM = Isnull(FNCM,0)-" & CDbl("0" & R!FNCMDisAmt.ToString)
                    _Cmd &= vbCrLf & "  WHERE FNHSysStyleId=" & CInt("0" & x!FNHSysStyleId.ToString)

                    HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_MASTER)

                Next

                tSql = "  INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTSeasonCMPrice]  "
                tSql &= Environment.NewLine & " (FNHSysStyleId,FNHSysSeasonId,FNCM,FNCMDisPer,FNCMDisAmt,FNNetCM,FNCostTransport)"
                tSql &= Environment.NewLine & " SELECT XSX.FNHSysStyleId, XSX.FNHSysSeasonId, XSX.FNCM, XSX.FNCMDisPer, XSX.FNCMDisAmt,XSX.FNNetCM, XSX.FNCostTransport"
                tSql &= Environment.NewLine & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTSeasonCMPrice] AS STM "
                tSql &= Environment.NewLine & "     RIGHT OUTER JOIN (SELECT B2.FNHSysStyleId,B2.FNHSysSeasonId,MAX(BB.FNCMDisAmt) AS FNCMDisAmt"
                tSql &= Environment.NewLine & " ,MAX(ST.FNCM) AS FNCM,MAX(ST.FNCMDisPer) AS FNCMDisPer,MAX(ST.FNNetCM) AS FNNetCM,MAX(ST.FNCostTransport) AS FNCostTransport"
                tSql &= Environment.NewLine & "      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS B2 WITH(NOLOCK) "
                tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS B4 WITH(NOLOCK) ON B2.FTOrderNo = B4.FTOrderNo   "
                tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS BB WITH(NOLOCK) ON B4.FTOrderNo = BB.FTOrderNo AND B4.FTSubOrderNo = BB.FTSubOrderNo "
                tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS ST  WITH(NOLOCK) ON B2.FNHSysStyleId = ST.FNHSysStyleId "
                tSql &= Environment.NewLine & "     WHERE  B2.FNHSysStyleId =" & Integer.Parse(Val(CInt(x!FNHSysStyleId.ToString))) & ""
                tSql &= Environment.NewLine & "     AND B2.FNHSysSeasonId=" & CInt("0" & x!FNHSysSeasonId.ToString)
                tSql &= Environment.NewLine & "  AND B4.FNHSysCurId IN (SELECT FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS C WITH(NOLOCK) WHERE ISNULL(FTStateLocal,'0') <>'1')"

                tSql &= Environment.NewLine & "   GROUP BY B2.FNHSysStyleId,B2.FNHSysSeasonId) AS XSX"
                tSql &= Environment.NewLine & " ON  STM.FNHSysStyleId=XSX.FNHSysStyleId AND STM.FNHSysSeasonId=XSX.FNHSysSeasonId"
                tSql &= Environment.NewLine & "  WHERE  STM.FNHSysStyleId IS NULL"
                HI.Conn.SQLConn.ExecuteNonQuery(tSql, Conn.DB.DataBaseName.DB_MERCHAN)

                tSql = "  UPDATE STM"
                tSql &= Environment.NewLine & "   SET FNCMDisAmt=XSX.FNCMDisAmt"
                tSql &= Environment.NewLine & "      ,FNNetCM = (STM.FNCM - XSX.FNCMDisAmt ) "
                tSql &= Environment.NewLine & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTSeasonCMPrice] AS STM "
                tSql &= Environment.NewLine & "   , (SELECT B2.FNHSysStyleId,B2.FNHSysSeasonId,MAX(BB.FNCMDisAmt) AS FNCMDisAmt"
                tSql &= Environment.NewLine & " ,MAX(ST.FNCM) AS FNCM,MAX(ST.FNCMDisPer) AS FNCMDisPer,MAX(ST.FNNetCM) AS FNNetCM,MAX(ST.FNCostTransport) AS FNCostTransport"
                tSql &= Environment.NewLine & "      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS B2 WITH(NOLOCK) "
                tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub] AS B4 WITH(NOLOCK) ON B2.FTOrderNo = B4.FTOrderNo   "
                tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS BB WITH(NOLOCK) ON B4.FTOrderNo = BB.FTOrderNo AND B4.FTSubOrderNo = BB.FTSubOrderNo "
                tSql &= Environment.NewLine & "           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TMERMStyle] AS ST  WITH(NOLOCK) ON B2.FNHSysStyleId = ST.FNHSysStyleId "
                tSql &= Environment.NewLine & "     WHERE  B2.FNHSysStyleId =" & Integer.Parse(Val(CInt(x!FNHSysStyleId.ToString))) & ""
                tSql &= Environment.NewLine & "     AND B2.FNHSysSeasonId=" & CInt("0" & x!FNHSysSeasonId.ToString)
                tSql &= Environment.NewLine & " AND  B4.FNHSysCurId IN (SELECT FNHSysCurId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TFINMCurrency AS C WITH(NOLOCK) WHERE ISNULL(FTStateLocal,'0') <>'1')"
                tSql &= Environment.NewLine & "   GROUP BY B2.FNHSysStyleId,B2.FNHSysSeasonId) AS XSX"
                tSql &= Environment.NewLine & " WHERE  STM.FNHSysStyleId=XSX.FNHSysStyleId AND STM.FNHSysSeasonId=XSX.FNHSysSeasonId"

                HI.Conn.SQLConn.ExecuteNonQuery(tSql, Conn.DB.DataBaseName.DB_MERCHAN)

            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function UpdateSubBreakdown(ByRef _dt As DataTable) As Boolean
        Dim bSaveFONoSubBreakdown As Boolean = False

        Dim _Spls As HI.TL.SplashScreen

        If Not System.Diagnostics.Debugger.IsAttached = True Then
            _Spls = New HI.TL.SplashScreen("Save data  Please Wait...")
        End If

        Dim _FTMatColorCode As String = ""
        Dim _FTMatSizeCode As String = ""
        Try
            Dim oStrBuilder As New System.Text.StringBuilder()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            Dim nFNHSysMatColorId As Integer, nFNHSysMatSizeId As Integer
            Dim nFNPrice As Double, nFNAmt As Double, nFNExtraQty As Double, nFNAmntExtra As Double, nFNGrandAmnt As Double, nFNGarmentQtyTest As Double
            Dim nFNQuantity As Integer, nFNQuantityExtra As Integer, nFNGrandQuantity As Integer
            Dim _FTCustomerPO As String = ""
            Dim tFTOrderNoBreakdownPrv As String, tFTSubOrderNoBreakdownPrv As String
            Dim _FTStateHold As String = ""
            Dim _FTNikePOLineItem As String = ""
            tFTOrderNoBreakdownPrv = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(0, "FTOrderNo").ToString()
            tFTSubOrderNoBreakdownPrv = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(0, "FTSubOrderNo").ToString()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("DECLARE @FTUpdUser AS NVARCHAR(30);")
            oStrBuilder.AppendLine("DECLARE @FDUpdDate AS NVARCHAR(50);")
            oStrBuilder.AppendLine("DECLARE @FTUpdTime AS NVARCHAR(50);")
            oStrBuilder.AppendLine(String.Format("SET @FTUpdUser = N'{0}';", HI.ST.UserInfo.UserName))
            oStrBuilder.AppendLine("SELECT @FDUpdDate = CONVERT(VARCHAR(10),GETDATE(),111);")
            oStrBuilder.AppendLine("SELECT @FTUpdTime = CONVERT(VARCHAR(8),GETDATE(),114);")

            For nLoopAdjustBreakdown As Integer = 0 To Me.ogvAdjustSubFONoBreakdown.DataRowCount - 1

                Dim tFTOrderNoBreakdown As String, tFTSubOrderNoBreakdown As String

                Try
                    Dim oDataRowBreakdown As DataRow = Me.ogvAdjustSubFONoBreakdown.GetDataRow(nLoopAdjustBreakdown)

                    If Not oDataRowBreakdown Is Nothing Then

                        tFTOrderNoBreakdown = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTOrderNo").ToString()
                        tFTSubOrderNoBreakdown = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTSubOrderNo").ToString()
                        _FTCustomerPO = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTCustomerPO").ToString()

                        If tFTOrderNoBreakdown <> tFTOrderNoBreakdownPrv And tFTSubOrderNoBreakdown <> tFTSubOrderNoBreakdownPrv Then
                            tFTOrderNoBreakdownPrv = tFTOrderNoBreakdown
                            tFTSubOrderNoBreakdownPrv = tFTSubOrderNoBreakdown
                        End If

                        nFNHSysMatColorId = Val(oDataRowBreakdown.Item("FNHSysMatColorId"))
                        nFNHSysMatSizeId = Val(oDataRowBreakdown.Item("FNHSysMatSizeId"))

                        _FTMatColorCode = oDataRowBreakdown.Item("FTMatColorCode").ToString
                        _FTMatSizeCode = oDataRowBreakdown.Item("FTMatSizeCode").ToString
                        _FTStateHold = oDataRowBreakdown.Item("FTStateHold").ToString
                        _FTNikePOLineItem = oDataRowBreakdown.Item("FTNikePOLineItem").ToString
                        'If HI.MER.ValidateExportXML.CheckExportFileXML(tFTOrderNoBreakdown, tFTSubOrderNoBreakdown, False) = False Then
                        If HI.MER.ValidateExportXML.CheckExportMILineItem(tFTOrderNoBreakdown, tFTSubOrderNoBreakdown, _FTNikePOLineItem, False) = False Then

                            nFNPrice = 0 : nFNAmt = 0 : nFNExtraQty = 0 : nFNAmntExtra = 0 : nFNGrandAmnt = 0
                            nFNQuantity = 0 : nFNQuantityExtra = 0 : nFNGrandQuantity = 0 : nFNGarmentQtyTest = 0
                            nFNGarmentQtyTest = 0

                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNQuantity")) Then
                                nFNQuantity = Val(oDataRowBreakdown.Item("FNQuantity"))
                            Else
                                nFNQuantity = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNPrice")) Then
                                nFNPrice = Val(oDataRowBreakdown.Item("FNPrice"))
                            Else
                                nFNPrice = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNAmt")) Then
                                nFNAmt = Val(oDataRowBreakdown.Item("FNAmt"))
                            Else
                                nFNAmt = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNExtraQty")) Then
                                nFNExtraQty = Val(oDataRowBreakdown.Item("FNExtraQty"))
                            Else
                                nFNExtraQty = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNQuantityExtra")) Then
                                nFNQuantityExtra = Val(oDataRowBreakdown.Item("FNQuantityExtra"))
                            Else
                                nFNQuantityExtra = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGrandQuantity")) Then
                                nFNGrandQuantity = Val(oDataRowBreakdown.Item("FNGrandQuantity"))
                            Else
                                nFNGrandQuantity = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNAmntExtra")) Then
                                nFNAmntExtra = Val(oDataRowBreakdown.Item("FNAmntExtra"))
                            Else
                                nFNAmntExtra = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGrandAmnt")) Then
                                nFNGrandAmnt = Val(oDataRowBreakdown.Item("FNGrandAmnt"))
                            Else
                                nFNGrandAmnt = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGarmentQtyTest")) Then
                                nFNGarmentQtyTest = Val(oDataRowBreakdown.Item("FNGarmentQtyTest"))
                            Else
                                nFNGarmentQtyTest = 0
                            End If

                            Dim _FNAmntQtyTest As Double = 0
                            Try
                                _FNAmntQtyTest = Double.Parse(Format(Val(nFNGarmentQtyTest) * Val(nFNPrice), "0.00"))
                            Catch ex As Exception
                            End Try

                            Dim _CMDisPer As Double = GetCMDisPer(tFTOrderNoBreakdown)

                            If tFTSubOrderNoBreakdown <> Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTSubOrderNoRef").ToString() Then

                                oStrBuilder.AppendLine("UPDATE A")
                                oStrBuilder.AppendLine("SET  ")
                                oStrBuilder.AppendLine(String.Format("    A.FNPrice = {0}", nFNPrice))
                                oStrBuilder.AppendLine(String.Format("  ,  A.FNPriceOrg = {0}", nFNPrice))
                                oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisPer = {0}", _CMDisPer))
                                oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisAmt = {0}", (nFNPrice * _CMDisPer) / 100))
                                oStrBuilder.AppendLine(String.Format("  , A.FTStateHold = N'{0}'", _FTStateHold))
                                oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Divert_BreakDown] AS A")
                                oStrBuilder.AppendLine(String.Format("WHERE A.FTOrderNo = N'{0}'", tFTOrderNoBreakdown))
                                oStrBuilder.AppendLine(String.Format("      AND (A.FTSubOrderNo+ '-D' +Convert(nvarchar(30),A.FNDivertSeq )) = N'{0}'", tFTSubOrderNoBreakdown))
                                oStrBuilder.AppendLine(String.Format("      AND A.FTColorway = N'{0}'", _FTMatColorCode))
                                oStrBuilder.AppendLine(String.Format("      AND A.FTSizeBreakDown = N'{0}';", _FTMatSizeCode))

                            Else

                                oStrBuilder.AppendLine("UPDATE A")
                                oStrBuilder.AppendLine("SET A.FTUpdUser = @FTUpdUser,")
                                oStrBuilder.AppendLine("    A.FDUpdDate = @FDUpdDate,")
                                oStrBuilder.AppendLine("    A.FTUpdTime = @FTUpdTime,")
                                oStrBuilder.AppendLine(String.Format("    A.FNPrice = {0},", nFNPrice))
                                oStrBuilder.AppendLine(String.Format("    A.FNAmt = {0},", nFNAmt))
                                oStrBuilder.AppendLine(String.Format("    A.FNAmntExtra = {0},", nFNAmntExtra))
                                oStrBuilder.AppendLine(String.Format("    A.FNGrandAmnt = {0}", nFNGrandAmnt))
                                oStrBuilder.AppendLine(String.Format("  ,  A.FNAmntQtyTest = {0}", _FNAmntQtyTest))
                                oStrBuilder.AppendLine(String.Format("  ,  A.FNPriceOrg = {0}", nFNPrice))
                                oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisPer = {0}", _CMDisPer))
                                oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisAmt = {0}", (nFNPrice * _CMDisPer) / 100))
                                oStrBuilder.AppendLine(String.Format("  , A.FTStateHold = N'{0}'", _FTStateHold))
                                oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS A")
                                oStrBuilder.AppendLine(String.Format("WHERE A.FTOrderNo = N'{0}'", tFTOrderNoBreakdown))
                                oStrBuilder.AppendLine(String.Format("      AND A.FTSubOrderNo = N'{0}'", tFTSubOrderNoBreakdown))
                                oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatColorId = {0}", nFNHSysMatColorId))
                                oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatSizeId = {0};", nFNHSysMatSizeId))

                            End If

                            'If Val(oDataRowBreakdown!FNHSysSeasonId.ToString) <= 0 Then
                            '    Call SaveCMPrice(oDataRowBreakdown!FNHSysStyleId.ToString, Integer.Parse(Val(oDataRowBreakdown!FNHSysSeasonId.ToString)))
                            'End If

                        Else

                            If _dt.Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(tFTOrderNoBreakdown) & "' AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(tFTSubOrderNoBreakdown) & "' AND FTCustomerPO='" & HI.UL.ULF.rpQuoted(_FTCustomerPO) & "'").Length <= 0 Then
                                _dt.Rows.Add(tFTOrderNoBreakdown, tFTSubOrderNoBreakdown, _FTCustomerPO)
                            End If

                        End If

                    End If

                Catch ex As Exception
                    If System.Diagnostics.Debugger.IsAttached = True Then
                        Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
                    End If
                End Try

            Next nLoopAdjustBreakdown

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            tSql = ""
            tSql = oStrBuilder.ToString()

            If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                If Not System.Diagnostics.Debugger.IsAttached = True Then
                    MsgBox("Step failed at update factory sub order no breakdown !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End If

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                bSaveFONoSubBreakdown = False
            Else
                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                bSaveFONoSubBreakdown = True
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        If Not System.Diagnostics.Debugger.IsAttached = True Then
            _Spls.Close()
        End If

        Return bSaveFONoSubBreakdown

    End Function

    Private Function UpdateSubBreakdownUpdateDisprice(ByRef _dt As DataTable) As Boolean
        Dim bSaveFONoSubBreakdown As Boolean = False

        Dim _Spls As HI.TL.SplashScreen

        If Not System.Diagnostics.Debugger.IsAttached = True Then
            _Spls = New HI.TL.SplashScreen("Save data  Please Wait...")
        End If

        Dim _FTMatColorCode As String = ""
        Dim _FTMatSizeCode As String = ""
        Try
            Dim oStrBuilder As New System.Text.StringBuilder()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            Dim nFNHSysMatColorId As Integer, nFNHSysMatSizeId As Integer
            Dim nFNPrice As Double, nFNAmt As Double, nFNExtraQty As Double, nFNAmntExtra As Double, nFNGrandAmnt As Double, nFNGarmentQtyTest As Double
            Dim nFNQuantity As Integer, nFNQuantityExtra As Integer, nFNGrandQuantity As Integer
            Dim _FTCustomerPO As String = ""
            Dim tFTOrderNoBreakdownPrv As String, tFTSubOrderNoBreakdownPrv As String
            Dim _FTStateHold As String = ""
            Dim _lineItem As String = ""

            tFTOrderNoBreakdownPrv = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(0, "FTOrderNo").ToString()
            tFTSubOrderNoBreakdownPrv = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(0, "FTSubOrderNo").ToString()

            oStrBuilder.Remove(0, oStrBuilder.Length)

            oStrBuilder.AppendLine("DECLARE @FTUpdUser AS NVARCHAR(30);")
            oStrBuilder.AppendLine("DECLARE @FDUpdDate AS NVARCHAR(50);")
            oStrBuilder.AppendLine("DECLARE @FTUpdTime AS NVARCHAR(50);")
            oStrBuilder.AppendLine(String.Format("SET @FTUpdUser = N'{0}';", HI.ST.UserInfo.UserName))
            oStrBuilder.AppendLine("SELECT @FDUpdDate = CONVERT(VARCHAR(10),GETDATE(),111);")
            oStrBuilder.AppendLine("SELECT @FTUpdTime = CONVERT(VARCHAR(8),GETDATE(),114);")

            For nLoopAdjustBreakdown As Integer = 0 To Me.ogvAdjustSubFONoBreakdown.DataRowCount - 1

                Dim tFTOrderNoBreakdown As String, tFTSubOrderNoBreakdown As String

                Try
                    Dim oDataRowBreakdown As DataRow = Me.ogvAdjustSubFONoBreakdown.GetDataRow(nLoopAdjustBreakdown)

                    If Not oDataRowBreakdown Is Nothing Then

                        tFTOrderNoBreakdown = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTOrderNo").ToString()
                        tFTSubOrderNoBreakdown = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTSubOrderNo").ToString()
                        _FTCustomerPO = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTCustomerPO").ToString()
                        _lineItem = Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTNikePOLineItem").ToString()


                        If tFTOrderNoBreakdown <> tFTOrderNoBreakdownPrv And tFTSubOrderNoBreakdown <> tFTSubOrderNoBreakdownPrv Then
                            tFTOrderNoBreakdownPrv = tFTOrderNoBreakdown
                            tFTSubOrderNoBreakdownPrv = tFTSubOrderNoBreakdown
                        End If

                        nFNHSysMatColorId = Val(oDataRowBreakdown.Item("FNHSysMatColorId"))
                        nFNHSysMatSizeId = Val(oDataRowBreakdown.Item("FNHSysMatSizeId"))

                        _FTMatColorCode = oDataRowBreakdown.Item("FTMatColorCode").ToString
                        _FTMatSizeCode = oDataRowBreakdown.Item("FTMatSizeCode").ToString
                        _FTStateHold = oDataRowBreakdown.Item("FTStateHold").ToString

                        'If HI.MER.ValidateExportXML.CheckExportFileXML(tFTOrderNoBreakdown, tFTSubOrderNoBreakdown, False) = False Then
                        If HI.MER.ValidateExportXML.CheckExportMILineItem(tFTOrderNoBreakdown, tFTSubOrderNoBreakdown, _lineItem, False) = False Then

                            nFNPrice = 0 : nFNAmt = 0 : nFNExtraQty = 0 : nFNAmntExtra = 0 : nFNGrandAmnt = 0
                            nFNQuantity = 0 : nFNQuantityExtra = 0 : nFNGrandQuantity = 0 : nFNGarmentQtyTest = 0
                            nFNGarmentQtyTest = 0

                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNQuantity")) Then
                                nFNQuantity = Val(oDataRowBreakdown.Item("FNQuantity"))
                            Else
                                nFNQuantity = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNPrice")) Then
                                nFNPrice = Val(oDataRowBreakdown.Item("FNPrice"))
                            Else
                                nFNPrice = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNAmt")) Then
                                nFNAmt = Val(oDataRowBreakdown.Item("FNAmt"))
                            Else
                                nFNAmt = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNExtraQty")) Then
                                nFNExtraQty = Val(oDataRowBreakdown.Item("FNExtraQty"))
                            Else
                                nFNExtraQty = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNQuantityExtra")) Then
                                nFNQuantityExtra = Val(oDataRowBreakdown.Item("FNQuantityExtra"))
                            Else
                                nFNQuantityExtra = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGrandQuantity")) Then
                                nFNGrandQuantity = Val(oDataRowBreakdown.Item("FNGrandQuantity"))
                            Else
                                nFNGrandQuantity = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNAmntExtra")) Then
                                nFNAmntExtra = Val(oDataRowBreakdown.Item("FNAmntExtra"))
                            Else
                                nFNAmntExtra = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGrandAmnt")) Then
                                nFNGrandAmnt = Val(oDataRowBreakdown.Item("FNGrandAmnt"))
                            Else
                                nFNGrandAmnt = 0
                            End If

                            If Not DBNull.Value.Equals(oDataRowBreakdown.Item("FNGarmentQtyTest")) Then
                                nFNGarmentQtyTest = Val(oDataRowBreakdown.Item("FNGarmentQtyTest"))
                            Else
                                nFNGarmentQtyTest = 0
                            End If

                            Dim _FNAmntQtyTest As Double = 0
                            Try
                                _FNAmntQtyTest = Double.Parse(Format(Val(nFNGarmentQtyTest) * Val(nFNPrice), "0.00"))
                            Catch ex As Exception
                            End Try

                            Dim _CMDisPer As Double = GetCMDisPer(tFTOrderNoBreakdown)
                            Dim _FNOperateFee As Double = GetOperateFee(tFTOrderNoBreakdown)

                            If tFTSubOrderNoBreakdown <> Me.ogvAdjustSubFONoBreakdown.GetRowCellValue(nLoopAdjustBreakdown, "FTSubOrderNoRef").ToString() Then

                                'oStrBuilder.AppendLine("UPDATE A")
                                'oStrBuilder.AppendLine("SET  ")
                                'oStrBuilder.AppendLine(String.Format("    A.FNPrice = {0}", nFNPrice))
                                'oStrBuilder.AppendLine(String.Format("  ,  A.FNPriceOrg = {0}", nFNPrice))
                                'oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisPer = {0}", _CMDisPer))
                                'oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisAmt = {0}", (nFNPrice * _CMDisPer) / 100))
                                'oStrBuilder.AppendLine(String.Format("  , A.FTStateHold = N'{0}'", _FTStateHold))
                                'oStrBuilder.AppendLine(String.Format("  ,  A.FNOperateFee = {0}", _FNOperateFee))
                                'oStrBuilder.AppendLine(String.Format("  ,  A.FNOperateFeeAmt = {0}", (nFNPrice * _FNOperateFee) / 100))
                                'oStrBuilder.AppendLine(String.Format("  ,  A.FNNetFOB = {0}", nFNPrice - CDbl(Format(((nFNPrice * _FNOperateFee) / 100), "0.0000"))))
                                'oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Divert_BreakDown] AS A")
                                'oStrBuilder.AppendLine(String.Format("WHERE A.FTOrderNo = N'{0}'", tFTOrderNoBreakdown))
                                'oStrBuilder.AppendLine(String.Format("      AND (A.FTSubOrderNo+ '-D' +Convert(nvarchar(30),A.FNDivertSeq )) = N'{0}'", tFTSubOrderNoBreakdown))
                                'oStrBuilder.AppendLine(String.Format("      AND A.FTColorway = N'{0}'", _FTMatColorCode))
                                'oStrBuilder.AppendLine(String.Format("      AND A.FTSizeBreakDown = N'{0}';", _FTMatSizeCode))


                                oStrBuilder.AppendLine("UPDATE A")
                                oStrBuilder.AppendLine("SET  ")
                                oStrBuilder.AppendLine(String.Format("    A.FNPrice = {0}", nFNPrice))
                                oStrBuilder.AppendLine(String.Format("  ,  A.FNPriceOrg = {0}", nFNPrice))
                                oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisPer = {0}", _CMDisPer))
                                ' oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisAmt = {0}", (nFNPrice * _CMDisPer) / 100))
                                oStrBuilder.AppendLine("  ,  A.FNCMDisAmt = Convert(numeric(18,2),(A.FNPriceOrg * " & _CMDisPer & ") / 100.00) ")
                                oStrBuilder.AppendLine(String.Format("  , A.FTStateHold = N'{0}'", _FTStateHold))
                                oStrBuilder.AppendLine(String.Format("  ,  A.FNOperateFee = {0}", _FNOperateFee))
                                'oStrBuilder.AppendLine(String.Format("  ,  A.FNOperateFeeAmt = {0}", (nFNPrice * _FNOperateFee) / 100))
                                'oStrBuilder.AppendLine(String.Format("  ,  A.FNNetFOB = {0}", nFNPrice - CDbl(Format(((nFNPrice * _FNOperateFee) / 100), "0.0000"))))
                                oStrBuilder.AppendLine("  ,  A.FNOperateFeeAmt =  Convert(numeric(18,2),(A.FNPrice * " & _FNOperateFee & ") / 100.00) ")
                                oStrBuilder.AppendLine("  ,  A.FNNetFOB =A.FNPrice - Convert(numeric(18,2),(A.FNPrice * " & _FNOperateFee & ") / 100.00) ")

                                ' oStrBuilder.AppendLine("  ,  A.FNExternalAmntQtyTest = Convert(numeric(18,2),(ISNULL(A.FNExternalQtyTest,0) * " & nFNPrice & ") ) ")


                                oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_Divert_BreakDown] AS A")
                                oStrBuilder.AppendLine(String.Format("WHERE A.FTOrderNo = N'{0}'", tFTOrderNoBreakdown))
                                oStrBuilder.AppendLine(String.Format("      AND (A.FTSubOrderNo+ '-D' +Convert(nvarchar(30),A.FNDivertSeq )) = N'{0}'", tFTSubOrderNoBreakdown))
                                oStrBuilder.AppendLine(String.Format("      AND A.FTColorway = N'{0}'", _FTMatColorCode))
                                oStrBuilder.AppendLine(String.Format("      AND A.FTSizeBreakDown = N'{0}';", _FTMatSizeCode))


                            Else

                                'oStrBuilder.AppendLine("UPDATE A")
                                'oStrBuilder.AppendLine("SET A.FTUpdUser = @FTUpdUser,")
                                'oStrBuilder.AppendLine("    A.FDUpdDate = @FDUpdDate,")
                                'oStrBuilder.AppendLine("    A.FTUpdTime = @FTUpdTime,")
                                'oStrBuilder.AppendLine(String.Format("    A.FNPrice = {0},", nFNPrice))
                                'oStrBuilder.AppendLine(String.Format("    A.FNAmt = {0},", nFNAmt))
                                'oStrBuilder.AppendLine(String.Format("    A.FNAmntExtra = {0},", nFNAmntExtra))
                                'oStrBuilder.AppendLine(String.Format("    A.FNGrandAmnt = {0}", nFNGrandAmnt))
                                'oStrBuilder.AppendLine(String.Format("  ,  A.FNAmntQtyTest = {0}", _FNAmntQtyTest))
                                'oStrBuilder.AppendLine(String.Format("  ,  A.FNPriceOrg = {0}", nFNPrice))
                                'oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisPer = {0}", _CMDisPer))
                                'oStrBuilder.AppendLine(String.Format("  ,  A.FNCMDisAmt = {0}", (nFNPrice * _CMDisPer) / 100))
                                'oStrBuilder.AppendLine(String.Format("  , A.FTStateHold = N'{0}'", _FTStateHold))
                                'oStrBuilder.AppendLine(String.Format("  ,  A.FNOperateFee = {0}", _FNOperateFee))
                                'oStrBuilder.AppendLine(String.Format("  ,  A.FNOperateFeeAmt = {0}", (nFNPrice * _FNOperateFee) / 100))
                                'oStrBuilder.AppendLine(String.Format("  ,  A.FNNetFOB = {0}", nFNPrice - CDbl(Format(((nFNPrice * _FNOperateFee) / 100), "0.0000"))))
                                'oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS A")
                                'oStrBuilder.AppendLine(String.Format("WHERE A.FTOrderNo = N'{0}'", tFTOrderNoBreakdown))
                                'oStrBuilder.AppendLine(String.Format("      AND A.FTSubOrderNo = N'{0}'", tFTSubOrderNoBreakdown))
                                'oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatColorId = {0}", nFNHSysMatColorId))
                                'oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatSizeId = {0};", nFNHSysMatSizeId))

                                oStrBuilder.AppendLine("UPDATE A")
                                oStrBuilder.AppendLine("SET A.FTUpdUser = @FTUpdUser,")
                                oStrBuilder.AppendLine("    A.FDUpdDate = @FDUpdDate,")
                                oStrBuilder.AppendLine("    A.FTUpdTime = @FTUpdTime,")
                                'oStrBuilder.AppendLine(String.Format("    A.FNPrice = {0},", nFNPrice))
                                'oStrBuilder.AppendLine(String.Format("    A.FNAmt = {0},", nFNAmt))
                                'oStrBuilder.AppendLine(String.Format("    A.FNAmntExtra = {0},", nFNAmntExtra))
                                'oStrBuilder.AppendLine(String.Format("    A.FNGrandAmnt = {0}", nFNGrandAmnt))
                                'oStrBuilder.AppendLine(String.Format("  ,  A.FNAmntQtyTest = {0}", _FNAmntQtyTest))
                                'oStrBuilder.AppendLine(String.Format("  ,  A.FNPriceOrg = {0}", nFNPrice))
                                oStrBuilder.AppendLine(String.Format("    A.FNCMDisPer = {0}", _CMDisPer))
                                oStrBuilder.AppendLine("  ,  A.FNCMDisAmt = Convert(numeric(18,2),(A.FNPriceOrg * " & _CMDisPer & ") / 100.00) ")
                                oStrBuilder.AppendLine(String.Format("  , A.FTStateHold = N'{0}'", _FTStateHold))
                                oStrBuilder.AppendLine(String.Format("  ,  A.FNOperateFee = {0}", _FNOperateFee))
                                oStrBuilder.AppendLine("  ,  A.FNOperateFeeAmt =  Convert(numeric(18,2),(A.FNPrice * " & _FNOperateFee & ") / 100.00) ")
                                oStrBuilder.AppendLine("  ,  A.FNNetFOB =A.FNPrice - Convert(numeric(18,2),(A.FNPrice * " & _FNOperateFee & ") / 100.00) ")
                                oStrBuilder.AppendLine("  ,  A.FNExternalAmntQtyTest = Convert(numeric(18,2),(ISNULL(A.FNExternalQtyTest,0) * " & nFNPrice & ") ) ")
                                'oStrBuilder.AppendLine(String.Format("  ,  A.FNNetFOB = {0}", nFNPrice - CDbl(Format(((nFNPrice * _FNOperateFee) / 100), "0.0000"))))
                                oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrderSub_BreakDown] AS A")
                                oStrBuilder.AppendLine(String.Format("WHERE A.FTOrderNo = N'{0}'", tFTOrderNoBreakdown))
                                oStrBuilder.AppendLine(String.Format("      AND A.FTSubOrderNo = N'{0}'", tFTSubOrderNoBreakdown))
                                oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatColorId = {0}", nFNHSysMatColorId))
                                oStrBuilder.AppendLine(String.Format("      AND A.FNHSysMatSizeId = {0};", nFNHSysMatSizeId))

                            End If

                            If HI.MER.ValidateExportXML.CheckExportFileXML(tFTOrderNoBreakdown, tFTSubOrderNoBreakdown, False) = False Then
                                Call SaveCMPrice(oDataRowBreakdown!FNHSysStyleId.ToString, Integer.Parse(Val(oDataRowBreakdown!FNHSysSeasonId.ToString)))
                            End If

                        Else

                            If _dt.Select("FTOrderNo='" & HI.UL.ULF.rpQuoted(tFTOrderNoBreakdown) & "' AND FTSubOrderNo='" & HI.UL.ULF.rpQuoted(tFTSubOrderNoBreakdown) & "' AND FTCustomerPO='" & HI.UL.ULF.rpQuoted(_FTCustomerPO) & "'").Length <= 0 Then
                                _dt.Rows.Add(tFTOrderNoBreakdown, tFTSubOrderNoBreakdown, _FTCustomerPO)
                            End If

                        End If

                    End If

                Catch ex As Exception
                    If System.Diagnostics.Debugger.IsAttached = True Then
                        Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString())
                    End If
                End Try

            Next nLoopAdjustBreakdown

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MERCHAN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            tSql = ""
            tSql = oStrBuilder.ToString()

            If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                If Not System.Diagnostics.Debugger.IsAttached = True Then
                    MsgBox("Step failed at update factory sub order no breakdown !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                End If

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                bSaveFONoSubBreakdown = False
            Else
                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                bSaveFONoSubBreakdown = True
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        If Not System.Diagnostics.Debugger.IsAttached = True Then
            _Spls.Close()
        End If

        Return bSaveFONoSubBreakdown

    End Function

    Private Function SaveCMPrice(tFTOrderNoBreakdown As String, _FNHSysSeasonId As Integer) As Boolean
        Try
            Dim _StyleId As Integer = CInt(tFTOrderNoBreakdown)
            If PDataTableTmp.Select("FNHSysStyleId=" & _StyleId & " AND FNHSysSeasonId=" & _FNHSysSeasonId & "").Count <= 0 Then
                PDataTableTmp.Rows.Add(_StyleId, _FNHSysSeasonId)
            End If
        Catch ex As Exception
        End Try
    End Function

    Private Function GetOperateFee(_OrderNo As String) As Double
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select Top 1 (CASE WHEN ISNULL(Cmpc.FNCmpBranchState,0) <=0 THEN ISNULL(S.FNOperateFee,0) ELSE ISNULL(S.FNOperateFeeForeign,0) END   ) AS FNOperateFee"
            _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCustomer AS S WITH(NOLOCK) INNER JOIN "
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON S.FNHSysCustId = O.FNHSysCustId "
            _Cmd &= vbCrLf & "         LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..[TCNMCmp] As Cmpc With(NOLOCK) On O.FNHSysCmpId = Cmpc.FNHSysCmpId"
            _Cmd &= vbCrLf & " WHERE O.FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            Return Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "0"))
        Catch ex As Exception
        End Try
    End Function

    Private Function GetCMDisPer(_OrderNo As String) As Double
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select Top 1 S.FNCMDisPer From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS S WITH(NOLOCK) INNER JOIN "
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON S.FNHSysStyleId = O.FNHSysStyleId "
            _Cmd &= vbCrLf & "WHERE O.FTOrderNo='" & HI.UL.ULF.rpQuoted(_OrderNo) & "'"
            Return Val(HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_MERCHAN, "0"))
        Catch ex As Exception
        End Try
    End Function

    Private Sub RepositoryItemCalcEditReal2_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemCalcEditReal2.EditValueChanging
        Try
            Dim _NewVale As Double = 0
            Dim _PriceOrgVale As Double = 0

            Try
                _NewVale = e.NewValue
            Catch ex As Exception
            End Try

            With Me.ogvAdjustSubFONoBreakdown
                _PriceOrgVale = Val("" & .GetFocusedRowCellValue("FNPriceOrg").ToString)

                .SetFocusedRowCellValue("FNPriceDiff", _NewVale - _PriceOrgVale)
            End With


        Catch ex As Exception

        End Try
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles ocmsavefobandfree.Click
        Try
            PDataTableTmp = New DataTable
            With PDataTableTmp.Columns
                .Add("FNHSysStyleId", GetType(Integer))
                .Add("FNHSysSeasonId", GetType(Integer))
            End With

            Dim _dt As DataTable = CreateDataTableCheckXML()

            If UpdateSubBreakdownUpdateDisprice(_dt) Then

                Call UpDateMasterStyle()
                Call W_PRCbLoadFONoSubBreakdown(False)

            End If

            If _dt.Rows.Count > 0 Then

                HI.MG.ShowMsg.mInfo("พบข้อมูลการส่งออก XML File แล้ว ในบางรายการ ไม่สามารถเปลี่ยนแปลงหรือแก้ไขข้อมูลได้ !!!", 1601010576, Me.Text, , MessageBoxIcon.Warning)

                With _ListPopup
                    HI.ST.Lang.SP_SETxLanguage(_ListPopup)
                    .ogdlist.DataSource = _dt.Copy
                    .ogdlist.Refresh()
                    .ShowDialog()
                End With

            End If

            _dt.Dispose()
        Catch ex As Exception
        End Try
    End Sub
End Class