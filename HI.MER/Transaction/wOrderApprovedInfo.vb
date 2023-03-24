Option Explicit On
Option Strict Off

Imports System
Imports System.Collections.Generic
Imports DevExpress
Imports Microsoft.VisualBasic

Public Class wOrderApprovedInfo

#Region "Variable Declaration"

    Private sSQL As String
    Private bFlagLoad As Boolean = False
    Private bFlagSaveAllInfo As Boolean = False
    Private bFlagTabInfoChange As Boolean = False

    Private Enum eApprovedTabIndex As Integer
        ApprovedOrderNo = 0
        ApprovedSubOrderNo = 1
        ApprovedComponent = 2
        ApprovedSewing = 3
        ApprovedPacking = 4
        ApprovedPackingCarton = 5
        ApprovedSizeSpec = 6
    End Enum
    'Private oGridViewApprovedOrderNoInfo As DevExpress.XtraGrid.Views.Grid.GridView
    'Private oGridViewApprovedOrderNoSubInfo As DevExpress.XtraGrid.Views.Grid.GridView
    'Private oGridViewApprovedComponentInfo As DevExpress.XtraGrid.Views.Grid.GridView
    'Private oGridViewApprovedSewingInfo As DevExpress.XtraGrid.Views.Grid.GridView
    'Private oGridViewApprovedPackingInfo As DevExpress.XtraGrid.Views.Grid.GridView
    'Private oGridViewApprovedPackRatioInfo As DevExpress.XtraGrid.Views.Grid.GridView
    'Private oGridViewApprovedSizeSpecInfo As DevExpress.XtraGrid.Views.Grid.GridView
#End Region

#Region "MAIN PROC"

    Private Sub PROC_SAVEDATA(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmSave.Click
        ''...verify before save data approved info
        'bFlagSaveAllInfo = False
        'If PROC_SAVEbApprovedOrder() = True Then
        '    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
        '    Call PROC_SHOWbBrowseDataListApprovedOrderInfo()
        '    If bFlagSaveAllInfo = True Then Me.oTabApprovedInfo.SelectedTabPageIndex = 0
        'End If
        'bFlagSaveAllInfo = False
    End Sub

    Private Sub PROC_APPROVED(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmapprove.Click
        '...verify before save data approved info
        bFlagSaveAllInfo = False
        If PROC_SAVEbApprovedOrder() = True Then
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Call PROC_SHOWbBrowseDataListApprovedOrderInfo()
            If bFlagSaveAllInfo = True Then Me.oTabApprovedInfo.SelectedTabPageIndex = 0
        End If
        bFlagSaveAllInfo = False
    End Sub

    Private Sub PROC_LOADDATA(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmloaddata.Click

        If Not PROC_VALIDATEbBeforeLoadData() = True Then
            Me.ogdApprovedOrderInfo.DataSource = Nothing : Me.ogdApprovedOrderInfo.Refresh()
            Me.ogdApprovedSubOrderNoInfo.DataSource = Nothing : Me.ogdApprovedSubOrderNoInfo.Refresh()
            Me.ogdApprovedComponentInfo.DataSource = Nothing : Me.ogdApprovedComponentInfo.Refresh()
            Me.ogdApprovedSewingInfo.DataSource = Nothing : Me.ogdApprovedSewingInfo.Refresh()
            Me.ogdApprovedPackingInfo.DataSource = Nothing : Me.ogdApprovedPackingInfo.Refresh()
            Me.ogdApprovedPackRatioInfo.DataSource = Nothing : Me.ogdApprovedPackRatioInfo.Refresh()
            Me.ogdApprovedSizeSpecInfo.DataSource = Nothing : Me.ogdApprovedSizeSpecInfo.Refresh()

            Exit Sub

        End If

        Dim _Spls As New HI.TL.SplashScreen("Loading... Please Wait....")

        Try
            bFlagLoad = True
            Call PROC_SHOWbBrowseDataListApprovedOrderInfo()
            'Me.oTabApprovedInfo.SelectedTabPageIndex = eApprovedTabIndex.ApprovedOrderNo
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
            End If
        End Try

        'If bFlagLoad = True Then Me.oTabApprovedInfo.SelectedTabPageIndex = 0
        Me.oTabApprovedInfo.SelectedTabPageIndex = 0

        bFlagLoad = False

    End Sub

    Private Sub PROC_REFRESHDATA_REM_20150226_1545()

        If Not PROC_VALIDATEbBeforeLoadData() = True Then
            Exit Sub
        End If

        Dim _Spls As New HI.TL.SplashScreen("Loading... Please Wait....")

        Try
            Me.oChkApproveAllInfo.Checked = False

            Call PROC_SHOWbBrowseDataListApprovedOrderInfo()

            Me.ogvApprovedOrderInfo.ClearColumnsFilter()
            Me.ogvApprovedOrderInfo.ActiveFilter.Clear()

            Me.ogvApprovedSubOrderNoInfo.ClearColumnsFilter()
            Me.ogvApprovedSubOrderNoInfo.ActiveFilter.Clear()

            Me.ogvApprovedComponentInfo.ClearColumnsFilter()
            Me.ogvApprovedComponentInfo.ActiveFilter.Clear()

            Me.ogvApprovedSewingInfo.ClearColumnsFilter()
            Me.ogvApprovedSewingInfo.ActiveFilter.Clear()

            Me.ogvApprovedPackingInfo.ClearColumnsFilter()
            Me.ogvApprovedPackingInfo.ActiveFilter.Clear()

            Me.ogvApprovedPackRatioInfo.ClearColumnsFilter()
            Me.ogvApprovedPackRatioInfo.ActiveFilter.Clear()

            Me.ogvApprovedSizeSpecInfo.ClearColumnsFilter()
            Me.ogvApprovedSewingInfo.ActiveFilter.Clear()

            'Select Case Me.oTabApprovedInfo.SelectedTabPageIndex
            '    Case eApprovedTabIndex.ApprovedOrderNo

            '    Case eApprovedTabIndex.ApprovedSubOrderNo

            '    Case eApprovedTabIndex.ApprovedComponent

            '    Case eApprovedTabIndex.ApprovedSewing

            '    Case eApprovedTabIndex.ApprovedPacking

            '    Case eApprovedTabIndex.ApprovedPackingCarton

            '    Case eApprovedTabIndex.ApprovedSizeSpec

            'End Select

            Me.oTabApprovedInfo.SelectedTabPageIndex = 0

            _Spls.Close()

        Catch ex As Exception
            _Spls.Close()
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
            End If
        End Try

    End Sub

    Private Sub PROC_REFRESHDATA(sender As Object, e As EventArgs) Handles ocmRefresh.Click
        If Not PROC_VALIDATEbBeforeLoadData() = True Then
            Me.ogdApprovedOrderInfo.DataSource = Nothing : Me.ogdApprovedOrderInfo.Refresh()
            Me.ogdApprovedSubOrderNoInfo.DataSource = Nothing : Me.ogdApprovedSubOrderNoInfo.Refresh()
            Me.ogdApprovedComponentInfo.DataSource = Nothing : Me.ogdApprovedComponentInfo.Refresh()
            Me.ogdApprovedSewingInfo.DataSource = Nothing : Me.ogdApprovedSewingInfo.Refresh()
            Me.ogdApprovedPackingInfo.DataSource = Nothing : Me.ogdApprovedPackingInfo.Refresh()
            Me.ogdApprovedPackRatioInfo.DataSource = Nothing : Me.ogdApprovedPackRatioInfo.Refresh()
            Me.ogdApprovedSizeSpecInfo.DataSource = Nothing : Me.ogdApprovedSizeSpecInfo.Refresh()

            Exit Sub

        End If

        Dim _Spls As New HI.TL.SplashScreen("Loading... Please Wait....")

        Try
            Call PROC_SHOWbBrowseDataListApprovedOrderInfo()

            If bFlagLoad = True Then Me.oTabApprovedInfo.SelectedTabPageIndex = eApprovedTabIndex.ApprovedOrderNo

            'Me.ogvApprovedOrderInfo.ClearColumnsFilter()
            'Me.ogvApprovedOrderInfo.ActiveFilter.Clear()

            'Me.ogvApprovedSubOrderNoInfo.ClearColumnsFilter()
            'Me.ogvApprovedSubOrderNoInfo.ActiveFilter.Clear()

            'Me.ogvApprovedComponentInfo.ClearColumnsFilter()
            'Me.ogvApprovedComponentInfo.ActiveFilter.Clear()

            'Me.ogvApprovedSewingInfo.ClearColumnsFilter()
            'Me.ogvApprovedSewingInfo.ActiveFilter.Clear()

            'Me.ogvApprovedPackingInfo.ClearColumnsFilter()
            'Me.ogvApprovedPackingInfo.ActiveFilter.Clear()

            'Me.ogvApprovedPackRatioInfo.ClearColumnsFilter()
            'Me.ogvApprovedPackRatioInfo.ActiveFilter.Clear()

            'Me.ogvApprovedSizeSpecInfo.ClearColumnsFilter()
            'Me.ogvApprovedSewingInfo.ActiveFilter.Clear()

            'Select Case Me.oTabApprovedInfo.SelectedTabPageIndex
            '    Case eApprovedTabIndex.ApprovedOrderNo
            '    Case eApprovedTabIndex.ApprovedSubOrderNo
            '    Case eApprovedTabIndex.ApprovedComponent
            '    Case eApprovedTabIndex.ApprovedSewing
            '    Case eApprovedTabIndex.ApprovedPacking
            '    Case eApprovedTabIndex.ApprovedPackingCarton
            '    Case eApprovedTabIndex.ApprovedSizeSpec
            'End Select

            REM 2015/02/26 Me.oTabApprovedInfo.SelectedTabPageIndex = 0

            _Spls.Close()

        Catch ex As Exception
            _Spls.Close()
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
            End If
        End Try

        bFlagLoad = False

    End Sub

    Private Sub PROC_CLEARSCREENCRITERIA(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclearclsr.Click
        Try
            Me.FNHSysBuyId.Text = ""
            Me.FNHSysStyleId.Text = ""

            Me.ogvApprovedOrderInfo.ClearColumnsFilter() : Me.ogvApprovedOrderInfo.ActiveFilter.Clear()
            Me.ogvApprovedSubOrderNoInfo.ClearColumnsFilter() : Me.ogvApprovedSubOrderNoInfo.ActiveFilter.Clear()
            Me.ogvApprovedComponentInfo.ClearColumnsFilter() : Me.ogvApprovedComponentInfo.ActiveFilter.Clear()
            Me.ogvApprovedSewingInfo.ClearColumnsFilter() : Me.ogvApprovedSewingInfo.ActiveFilter.Clear()
            Me.ogvApprovedPackingInfo.ClearColumnsFilter() : Me.ogvApprovedPackingInfo.ActiveFilter.Clear()
            Me.ogvApprovedPackRatioInfo.ClearColumnsFilter() : Me.ogvApprovedPackRatioInfo.ActiveFilter.Clear()
            Me.ogvApprovedSizeSpecInfo.ClearColumnsFilter() : Me.ogvApprovedSizeSpecInfo.ActiveFilter.Clear()

            Me.ogdApprovedOrderInfo.DataSource = Nothing : Me.ogdApprovedOrderInfo.Refresh()
            Me.ogdApprovedSubOrderNoInfo.DataSource = Nothing : Me.ogdApprovedSubOrderNoInfo.Refresh()
            Me.ogdApprovedComponentInfo.DataSource = Nothing : Me.ogdApprovedComponentInfo.Refresh()
            Me.ogdApprovedSewingInfo.DataSource = Nothing : Me.ogdApprovedSewingInfo.Refresh()
            Me.ogdApprovedPackingInfo.DataSource = Nothing : Me.ogdApprovedPackingInfo.Refresh()
            Me.ogdApprovedPackRatioInfo.DataSource = Nothing : Me.ogdApprovedPackRatioInfo.Refresh()
            Me.ogdApprovedSizeSpecInfo.DataSource = Nothing : Me.ogdApprovedSewingInfo.Refresh()

            Me.oChkApproveAllInfo.Checked = False : Me.oChkApproveAllRecord.Checked = False

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If
        End Try

    End Sub

    Private Sub PROC_CLOSESCREEN(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmExit.Click
        Me.Close()
    End Sub

#End Region

#Region "Procedure And Function"
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        'HI.TL.HandlerControl.AddHandlerObj(Me.FNHSysBuyId)
        'HI.TL.HandlerControl.AddHandlerObj(Me.FNHSysStyleId)
    End Sub

    Private Function PROC_GRIDVIEWbInitial() As Boolean
        Dim bRetInitial As Boolean = False
        Try

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

        Return bRetInitial

    End Function

    Private Function PROC_VALIDATEbBeforeLoadData() As Boolean
        Dim bValidate As Boolean = False

        If Val(Me.FNHSysBuyId.Properties.Tag) > 0 Then
            bValidate = True
        End If

        If Val(Me.FNHSysStyleId.Properties.Tag) > 0 Then
            bValidate = True
        End If

        If Not (bValidate) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return bValidate

    End Function

    Private Function PROC_SHOWbBrowseDataListApprovedOrderInfo_REM_20150226_1555() As Boolean

        Dim bRetLoadComplete As Boolean = False
        Dim sSQLWhere As String
        Dim DTApprovedOrderInfo As System.Data.DataTable
        Dim DTApprovedSubOrder As System.Data.DataTable
        '=================================================================
        Dim DTApprovedSubOrderInfo As System.Data.DataTable
        Dim DTApprovedComponentInfo As System.Data.DataTable
        Dim DTApprovedSewingInfo As System.Data.DataTable
        Dim DTApprovedPackingInfo As System.Data.DataTable
        Dim DTApprovedPackRatio As System.Data.DataTable
        Dim DTApprovedSizeSpecInfo As System.Data.DataTable
        '=================================================================
        Try
            Me.ogdApprovedOrderInfo.DataSource = Nothing
            Me.ogdApprovedOrderInfo.Refresh()

            Me.ogdApprovedSubOrderNoInfo.DataSource = Nothing
            Me.ogdApprovedSubOrderNoInfo.Refresh()

            Me.ogdApprovedComponentInfo.DataSource = Nothing
            Me.ogdApprovedComponentInfo.Refresh()

            Me.ogdApprovedSewingInfo.DataSource = Nothing
            Me.ogdApprovedSewingInfo.Refresh()

            Me.ogdApprovedPackingInfo.DataSource = Nothing
            Me.ogdApprovedPackingInfo.Refresh()

            Me.ogdApprovedPackRatioInfo.DataSource = Nothing
            Me.ogdApprovedPackRatioInfo.Refresh()

            Me.ogdApprovedSizeSpecInfo.DataSource = Nothing
            Me.ogdApprovedSizeSpecInfo.Refresh()

            Me.oChkApproveAllInfo.Checked = False
            Me.oChkApproveAllRecord.Checked = False

            sSQL = ""
            sSQL = "SELECT B.FTOrderNo AS FTOrderNo,"
            sSQL &= Environment.NewLine & "      CASE WHEN ISNULL(A.FTApprovedInfoState, '0') = '0' THEN '0' ELSE '1' END   AS FTApprovedInfoState,"
            sSQL &= Environment.NewLine & "      A.FNApprovedInfoCnt,"
            sSQL &= Environment.NewLine & "      CONVERT(VARCHAR(10), CAST(A.FDApprovedInfoDate AS DATE), 103) AS FDApprovedInfoDate,"
            sSQL &= Environment.NewLine & "      A.FTApprovedInfoTime,"
            sSQL &= Environment.NewLine & "      A.FTApprovedInfoBy,"
            sSQL &= Environment.NewLine & "      CASE WHEN ISNULL(A.FTRevisedInfoState, '0') = '0' THEN '0' ELSE '1' END AS FTRevisedInfoState,"
            sSQL &= Environment.NewLine & "      A.FNRevisedInfoCnt,"
            sSQL &= Environment.NewLine & "      CONVERT(VARCHAR(10), CAST(A.FDRevisedInfoDate AS DATE), 103) AS FDRevisedInfoDate,"
            sSQL &= Environment.NewLine & "      A.FTRevisedInfoTime,"
            sSQL &= Environment.NewLine & "      A.FTRevisedInfoBy,"
            sSQL &= Environment.NewLine & "      CASE WHEN ISNULL(A.FTApprovedInfoState, '0') = '0' THEN '0' ELSE '1' END   AS FTApprovedInfoStateHide"
            sSQL &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS B (NOLOCK) LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder_ApprovedInfo AS A (NOLOCK) ON A.FTOrderNo = B.FTOrderNo"

            sSQLWhere = ""

            If Me.FNHSysBuyId.Text.Trim <> "" Then
                If sSQLWhere <> "" Then sSQLWhere = sSQLWhere & "   AND "
                sSQLWhere &= "B.FNHSysBuyId = " & Val(Me.FNHSysBuyId.Properties.Tag)
            End If

            If Me.FNHSysStyleId.Text.Trim <> "" Then
                If sSQLWhere <> "" Then sSQLWhere = sSQLWhere & "   AND "
                sSQLWhere &= "B.FNHSysStyleId = " & Val(Me.FNHSysStyleId.Properties.Tag)
            End If

            If sSQLWhere <> "" Then sSQL &= Environment.NewLine & "WHERE " & sSQLWhere

            sSQL &= Environment.NewLine & "    AND B.FNJobState IN (0, 1)"

            If Not HI.ST.SysInfo.Admin Then
                sSQL &= Environment.NewLine & "      AND B.FNHSysMerTeamId = (SELECT TOP 1 L1.FNHSysMerTeamId AS FNHSysMerTeamId"
                sSQL &= Environment.NewLine & "                               FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..[TSEUserLogin] AS L1 WITH(NOLOCK)"
                sSQL &= Environment.NewLine & "                               WHERE L1.FTUserName = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
            End If

            sSQL &= Environment.NewLine & "ORDER BY A.FTOrderNo ASC;"

            DTApprovedOrderInfo = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            Me.ogdApprovedOrderInfo.DataSource = DTApprovedOrderInfo
            Me.ogdApprovedOrderInfo.Refresh()

            sSQL = ""
            sSQL = "SELECT A.[FTOrderNo] AS FTOrderNo, C.[FTSubOrderNo] AS FTSubOrderNo"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSubOrderNo], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSubOrderNo, B.[FNCntApprovedSubOrderNo], CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedSubOrderNo] AS DATE), 103) AS FDDateApprovedSubOrderNo, B.[FTTimeApprovedSubOrderNo], B.[FTUserApprovedSubOrderNo]"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedComponent], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedComponent, B.[FNCntApprovedComponent], CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedComponent] AS DATE), 103) AS FDDateApprovedComponent, B.[FTTimeApprovedComponent], B.[FTUserApprovedComponent]"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSewing], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSewing, B.[FNCntApprovedSewing], CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedSewing] AS DATE), 103) AS FDDateApprovedSewing, B.[FTTimeApprovedSewing], B.[FTUserApprovedSewing]"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPacking], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPacking, B.[FNCntApprovedPacking], CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedPacking] AS DATE), 103) AS FDDateApprovedPacking, B.[FTTimeApprovedPacking], B.[FTUserApprovedPacking]"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPackRatio], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPackRatio, B.[FNCntApprovedPackRatio], CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedPackRatio] AS DATE), 103) AS FDDateApprovedPackRatio, B.[FTTimeApprovedPackRatio], B.[FTUserApprovedPackRatio]"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSizeSpec], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSizeSpec, B.[FNCntApprovedSizeSpec], CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedSizeSpec] AS DATE), 103) AS FDDateApprovedSizeSpec, B.[FTTimeApprovedSizeSpec], B.[FTUserApprovedSizeSpec]"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSubOrderNo], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSubOrderNoHide"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedComponent], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedComponentHide"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSewing], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSewingHide"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPacking], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPackingHide"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPackRatio], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPackRatioHide"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSizeSpec], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSizeSpecHide"
            sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS A (NOLOCK) LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS C (NOLOCK) ON A.FTOrderNo = C.FTOrderNo"
            sSQL &= Environment.NewLine & "                                  LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS B (NOLOCK) ON B.FTOrderNo = C.FTOrderNo"
            sSQL &= Environment.NewLine & "											                                                                        AND B.FTSubOrderNo = C.FTSubOrderNo"

            sSQLWhere = ""

            If Me.FNHSysBuyId.Text.Trim <> "" Then
                If sSQLWhere <> "" Then sSQLWhere = sSQLWhere & "    AND "
                sSQLWhere &= "A.FNHSysBuyId = " & Val(Me.FNHSysBuyId.Properties.Tag)
            End If

            If Me.FNHSysStyleId.Text.Trim <> "" Then
                If sSQLWhere <> "" Then sSQLWhere = sSQLWhere & "    AND "
                sSQLWhere &= "A.FNHSysStyleId = " & Val(Me.FNHSysStyleId.Properties.Tag)
            End If

            If sSQLWhere <> "" Then sSQL &= Environment.NewLine & "WHERE " & sSQLWhere

            sSQL &= Environment.NewLine & "     AND A.FNJobState IN (0, 1)"

            If Not HI.ST.SysInfo.Admin Then
                sSQL &= Environment.NewLine & "      AND A.FNHSysMerTeamId = (SELECT TOP 1 L1.FNHSysMerTeamId AS FNHSysMerTeamId"
                sSQL &= Environment.NewLine & "                               FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..[TSEUserLogin] AS L1 WITH(NOLOCK)"
                sSQL &= Environment.NewLine & "                               WHERE L1.FTUserName = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
            End If

            sSQL &= Environment.NewLine & "ORDER BY A.FTOrderNo ASC, C.FTSubOrderNo ASC;"

            DTApprovedSubOrder = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            Dim tmpDTSubOrderNoApproved As System.Data.DataTable
            Dim tmpDTComponentApproved As System.Data.DataTable
            Dim tmpDTSewingApproved As System.Data.DataTable
            Dim tmpDTPackingApproved As System.Data.DataTable
            Dim tmpDTPackRatioApproved As System.Data.DataTable
            Dim tmpDTSizeSpecApproved As System.Data.DataTable

            tmpDTSubOrderNoApproved = DTApprovedSubOrder.Copy()
            tmpDTComponentApproved = DTApprovedSubOrder.Copy()
            tmpDTSewingApproved = DTApprovedSubOrder.Copy()
            tmpDTPackingApproved = DTApprovedSubOrder.Copy()
            tmpDTPackRatioApproved = DTApprovedSubOrder.Copy()
            tmpDTSizeSpecApproved = DTApprovedSubOrder.Copy()

            If Not tmpDTSubOrderNoApproved Is Nothing Then
                tmpDTSubOrderNoApproved.BeginInit()
                For numLoopSubOrderNo As Integer = tmpDTSubOrderNoApproved.Columns.Count - 1 To 0 Step -1
                    Select Case tmpDTSubOrderNoApproved.Columns(numLoopSubOrderNo).ColumnName.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTStateApprovedSubOrderNo".ToUpper, "FNCntApprovedSubOrderNo".ToUpper, "FDDateApprovedSubOrderNo".ToUpper, "FTTimeApprovedSubOrderNo".ToUpper, "FTUserApprovedSubOrderNo".ToUpper, "FTStateApprovedSubOrderNoHide".ToUpper
                            '...Nothing
                        Case Else
                            tmpDTSubOrderNoApproved.Columns.Remove(tmpDTSubOrderNoApproved.Columns(numLoopSubOrderNo))
                    End Select

                Next
                tmpDTSubOrderNoApproved.EndInit()
                tmpDTSubOrderNoApproved.AcceptChanges()
            End If

            If Not tmpDTComponentApproved Is Nothing Then
                tmpDTComponentApproved.BeginInit()
                For numLoopComponent As Integer = tmpDTComponentApproved.Columns.Count - 1 To 0 Step -1
                    Select Case tmpDTComponentApproved.Columns(numLoopComponent).ColumnName.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTStateApprovedComponent".ToUpper, "FNCntApprovedComponent".ToUpper, "FDDateApprovedComponent".ToUpper, "FTTimeApprovedComponent".ToUpper, "FTUserApprovedComponent".ToUpper, "FTStateApprovedSubOrderNoHide".ToUpper, "FTStateApprovedComponentHide".ToUpper
                            '...Nothing
                        Case Else
                            tmpDTComponentApproved.Columns.Remove(tmpDTComponentApproved.Columns(numLoopComponent))
                    End Select

                Next
                tmpDTComponentApproved.EndInit()
                tmpDTComponentApproved.AcceptChanges()
            End If

            If Not tmpDTSewingApproved Is Nothing Then
                tmpDTSewingApproved.BeginInit()
                For numLoopSew As Integer = tmpDTSewingApproved.Columns.Count - 1 To 0 Step -1
                    Select Case tmpDTSewingApproved.Columns(numLoopSew).ColumnName.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTStateApprovedSewing".ToUpper, "FNCntApprovedSewing".ToUpper, "FDDateApprovedSewing".ToUpper, "FTTimeApprovedSewing".ToUpper, "FTUserApprovedSewing".ToUpper, "FTStateApprovedSewingHide".ToUpper
                            '...Nothing
                        Case Else
                            tmpDTSewingApproved.Columns.Remove(tmpDTSewingApproved.Columns(numLoopSew))
                    End Select

                Next
                tmpDTSewingApproved.EndInit()
                tmpDTSewingApproved.AcceptChanges()
            End If

            If Not tmpDTPackingApproved Is Nothing Then
                tmpDTPackingApproved.BeginInit()
                For numLoopPack As Integer = tmpDTPackingApproved.Columns.Count - 1 To 0 Step -1
                    Select Case tmpDTPackingApproved.Columns(numLoopPack).ColumnName.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTStateApprovedPacking".ToUpper, "FNCntApprovedPacking".ToUpper, "FDDateApprovedPacking".ToUpper, "FTTimeApprovedPacking".ToUpper, "FTUserApprovedPacking".ToUpper, "FTStateApprovedPackingHide".ToUpper
                            '...Nothing
                        Case Else
                            tmpDTPackingApproved.Columns.Remove(tmpDTPackingApproved.Columns(numLoopPack))
                    End Select

                Next
                tmpDTPackingApproved.EndInit()
                tmpDTPackingApproved.AcceptChanges()
            End If

            If Not tmpDTPackRatioApproved Is Nothing Then
                tmpDTPackRatioApproved.BeginInit()
                For numLoopPackRatio As Integer = tmpDTPackRatioApproved.Columns.Count - 1 To 0 Step -1
                    Select Case tmpDTPackRatioApproved.Columns(numLoopPackRatio).ColumnName.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTStateApprovedPackRatio".ToUpper, "FNCntApprovedPackRatio".ToUpper, "FDDateApprovedPackRatio".ToUpper, "FTTimeApprovedPackRatio".ToUpper, "FTUserApprovedPackRatio".ToUpper, "FTStateApprovedPackRatioHide".ToUpper
                            '...Nothing
                        Case Else
                            tmpDTPackRatioApproved.Columns.Remove(tmpDTPackRatioApproved.Columns(numLoopPackRatio))
                    End Select

                Next
                tmpDTPackRatioApproved.EndInit()
                tmpDTPackRatioApproved.AcceptChanges()
            End If

            If Not tmpDTSizeSpecApproved Is Nothing Then
                tmpDTSizeSpecApproved.BeginInit()
                For numLoopSizeSpec As Integer = tmpDTSizeSpecApproved.Columns.Count - 1 To 0 Step -1
                    Select Case tmpDTSizeSpecApproved.Columns(numLoopSizeSpec).ColumnName.ToUpper
                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTStateApprovedSizeSpec".ToUpper, "FNCntApprovedSizeSpec".ToUpper, "FDDateApprovedSizeSpec".ToUpper, "FTTimeApprovedSizeSpec".ToUpper, "FTUserApprovedSizeSpec".ToUpper, "FTStateApprovedSizeSpecHide".ToUpper
                            '...Nothing
                        Case Else
                            tmpDTSizeSpecApproved.Columns.Remove(tmpDTSizeSpecApproved.Columns(numLoopSizeSpec))
                    End Select

                Next
                tmpDTSizeSpecApproved.EndInit()
                tmpDTSizeSpecApproved.AcceptChanges()
            End If

            '...DTApprovedSubOrderInfo
            DTApprovedSubOrderInfo = New System.Data.DataTable

            Dim oColFTOrderNo As System.Data.DataColumn
            oColFTOrderNo = New System.Data.DataColumn("FTOrderNo", System.Type.GetType("System.String"))
            oColFTOrderNo.Caption = "FTOrderNo"
            DTApprovedSubOrderInfo.Columns.Add(oColFTOrderNo.ColumnName, oColFTOrderNo.DataType)

            Dim oColFTSubOrderNo As System.Data.DataColumn
            oColFTSubOrderNo = New System.Data.DataColumn("FTSubOrderNo", System.Type.GetType("System.String"))
            oColFTSubOrderNo.Caption = "FTSubOrderNo"
            DTApprovedSubOrderInfo.Columns.Add(oColFTSubOrderNo.ColumnName, oColFTSubOrderNo.DataType)

            Dim oColFTStateApprovedSubOrderNo As System.Data.DataColumn
            oColFTStateApprovedSubOrderNo = New System.Data.DataColumn("FTStateApprovedSubOrderNo", System.Type.GetType("System.String"))
            oColFTStateApprovedSubOrderNo.Caption = "FTStateApprovedSubOrderNo"
            DTApprovedSubOrderInfo.Columns.Add(oColFTStateApprovedSubOrderNo.ColumnName, oColFTStateApprovedSubOrderNo.DataType)

            Dim oColFNCntApprovedSubOrderNo As System.Data.DataColumn
            oColFNCntApprovedSubOrderNo = New System.Data.DataColumn("FNCntApprovedSubOrderNo", System.Type.GetType("System.Int32"))
            oColFNCntApprovedSubOrderNo.Caption = "FNCntApprovedSubOrderNo"
            DTApprovedSubOrderInfo.Columns.Add(oColFNCntApprovedSubOrderNo.ColumnName, oColFNCntApprovedSubOrderNo.DataType)

            Dim oColFDDateApprovedSubOrderNo As System.Data.DataColumn
            oColFDDateApprovedSubOrderNo = New System.Data.DataColumn("FDDateApprovedSubOrderNo", System.Type.GetType("System.String"))
            oColFDDateApprovedSubOrderNo.Caption = "FDDateApprovedSubOrderNo"
            DTApprovedSubOrderInfo.Columns.Add(oColFDDateApprovedSubOrderNo.ColumnName, oColFDDateApprovedSubOrderNo.DataType)

            Dim oColFTUserApprovedSubOrderNo As System.Data.DataColumn
            oColFTUserApprovedSubOrderNo = New System.Data.DataColumn("FTUserApprovedSubOrderNo", System.Type.GetType("System.String"))
            oColFTUserApprovedSubOrderNo.Caption = "FTUserApprovedSubOrderNo"
            DTApprovedSubOrderInfo.Columns.Add(oColFTUserApprovedSubOrderNo.ColumnName, oColFTUserApprovedSubOrderNo.DataType)

            Dim oColFTStateApprovedSubOrderNoHide As System.Data.DataColumn
            oColFTStateApprovedSubOrderNoHide = New System.Data.DataColumn("FTStateApprovedSubOrderNoHide", System.Type.GetType("System.String"))
            oColFTStateApprovedSubOrderNoHide.Caption = "FTStateApprovedSubOrderNoHide"
            DTApprovedSubOrderInfo.Columns.Add(oColFTStateApprovedSubOrderNoHide.ColumnName, oColFTStateApprovedSubOrderNoHide.DataType)

            For Each oDataRow1 As System.Data.DataRow In tmpDTSubOrderNoApproved.Rows
                DTApprovedSubOrderInfo.ImportRow(oDataRow1)
            Next

            If Not tmpDTSubOrderNoApproved Is Nothing Then tmpDTSubOrderNoApproved.Dispose()

            Me.ogdApprovedSubOrderNoInfo.DataSource = DTApprovedSubOrderInfo
            Me.ogdApprovedSubOrderNoInfo.Refresh()

            'DTApprovedComponentInfo
            DTApprovedComponentInfo = New System.Data.DataTable

            Dim oColFTOrderNoComponent As System.Data.DataColumn
            oColFTOrderNoComponent = New System.Data.DataColumn("FTOrderNo", System.Type.GetType("System.String"))
            oColFTOrderNoComponent.Caption = "FTOrderNo"
            DTApprovedComponentInfo.Columns.Add(oColFTOrderNoComponent.ColumnName, oColFTOrderNoComponent.DataType)

            Dim oColFTSubOrderNoComponent As System.Data.DataColumn
            oColFTSubOrderNoComponent = New System.Data.DataColumn("FTSubOrderNo", System.Type.GetType("System.String"))
            oColFTSubOrderNo.Caption = "FTSubOrderNo"
            DTApprovedComponentInfo.Columns.Add(oColFTSubOrderNoComponent.ColumnName, oColFTSubOrderNoComponent.DataType)

            Dim oColFTStateApprovedComponent As System.Data.DataColumn
            oColFTStateApprovedComponent = New System.Data.DataColumn("FTStateApprovedComponent", System.Type.GetType("System.String"))
            oColFTStateApprovedComponent.Caption = "FTStateApprovedComponent"
            DTApprovedComponentInfo.Columns.Add(oColFTStateApprovedComponent.ColumnName, oColFTStateApprovedComponent.DataType)

            Dim oColFNCntApprovedComponent As System.Data.DataColumn
            oColFNCntApprovedComponent = New System.Data.DataColumn("FNCntApprovedComponent", System.Type.GetType("System.Int32"))
            oColFNCntApprovedComponent.Caption = "FNCntApprovedComponent"
            DTApprovedComponentInfo.Columns.Add(oColFNCntApprovedComponent.ColumnName, oColFNCntApprovedComponent.DataType)

            Dim oColFDDateApprovedComponent As System.Data.DataColumn
            oColFDDateApprovedComponent = New System.Data.DataColumn("FDDateApprovedComponent", System.Type.GetType("System.String"))
            oColFDDateApprovedComponent.Caption = "FDDateApprovedComponent"
            DTApprovedComponentInfo.Columns.Add(oColFDDateApprovedComponent.ColumnName, oColFDDateApprovedComponent.DataType)

            Dim oColFTTimeApprovedComponent As System.Data.DataColumn
            oColFTTimeApprovedComponent = New System.Data.DataColumn("FTTimeApprovedComponent", System.Type.GetType("System.String"))
            oColFTTimeApprovedComponent.Caption = "FTTimeApprovedComponent"
            DTApprovedComponentInfo.Columns.Add(oColFTTimeApprovedComponent.ColumnName, oColFTTimeApprovedComponent.DataType)

            Dim oColFTUserApprovedComponent As System.Data.DataColumn
            oColFTUserApprovedComponent = New System.Data.DataColumn("FTUserApprovedComponent", System.Type.GetType("System.String"))
            oColFTUserApprovedComponent.Caption = "FTUserApprovedComponent"
            DTApprovedComponentInfo.Columns.Add(oColFTUserApprovedComponent.ColumnName, oColFTUserApprovedComponent.DataType)

            Dim oColFTStateApprovedComponentHide As System.Data.DataColumn
            oColFTStateApprovedComponentHide = New System.Data.DataColumn("FTStateApprovedComponentHide", System.Type.GetType("System.String"))
            oColFTStateApprovedComponentHide.Caption = "FTStateApprovedComponentHide"
            DTApprovedComponentInfo.Columns.Add(oColFTStateApprovedComponentHide.ColumnName, oColFTStateApprovedComponentHide.DataType)

            For Each oDataRow2 As System.Data.DataRow In tmpDTComponentApproved.Rows
                DTApprovedComponentInfo.ImportRow(oDataRow2)
            Next

            If Not tmpDTComponentApproved Is Nothing Then tmpDTComponentApproved.Dispose()

            Me.ogdApprovedComponentInfo.DataSource = DTApprovedComponentInfo
            Me.ogdApprovedComponentInfo.Refresh()

            'DTApprovedSewingInfo
            DTApprovedSewingInfo = New System.Data.DataTable

            Dim oColFTOrderNoSew As System.Data.DataColumn
            oColFTOrderNoSew = New System.Data.DataColumn("FTOrderNo", System.Type.GetType("System.String"))
            oColFTOrderNoSew.Caption = "FTOrderNo"
            DTApprovedSewingInfo.Columns.Add(oColFTOrderNoSew.ColumnName, oColFTOrderNoSew.DataType)

            Dim oColFTSubOrderNoSew As System.Data.DataColumn
            oColFTSubOrderNoSew = New System.Data.DataColumn("FTSubOrderNo", System.Type.GetType("System.String"))
            oColFTSubOrderNoSew.Caption = "FTSubOrderNo"
            DTApprovedSewingInfo.Columns.Add(oColFTSubOrderNoSew.ColumnName, oColFTSubOrderNoSew.DataType)

            Dim oColFTStateApprovedSewing As System.Data.DataColumn
            oColFTStateApprovedSewing = New System.Data.DataColumn("FTStateApprovedSewing", System.Type.GetType("System.String"))
            oColFTStateApprovedSewing.Caption = "FTStateApprovedSewing"
            DTApprovedSewingInfo.Columns.Add(oColFTStateApprovedSewing.ColumnName, oColFTStateApprovedSewing.DataType)

            Dim oColFNCntApprovedSewing As System.Data.DataColumn
            oColFNCntApprovedSewing = New System.Data.DataColumn("FNCntApprovedSewing", System.Type.GetType("System.Int32"))
            oColFNCntApprovedSewing.Caption = "FNCntApprovedSewing"
            DTApprovedSewingInfo.Columns.Add(oColFNCntApprovedSewing.ColumnName, oColFNCntApprovedSewing.DataType)

            Dim oColFDDateApprovedSewing As System.Data.DataColumn
            oColFDDateApprovedSewing = New System.Data.DataColumn("FDDateApprovedSewing", System.Type.GetType("System.String"))
            oColFDDateApprovedSewing.Caption = "FDDateApprovedSewing"
            DTApprovedSewingInfo.Columns.Add(oColFDDateApprovedSewing.ColumnName, oColFDDateApprovedSewing.DataType)

            Dim oColFTTimeApprovedSewing As System.Data.DataColumn
            oColFTTimeApprovedSewing = New System.Data.DataColumn("FTTimeApprovedSewing", System.Type.GetType("System.String"))
            oColFTTimeApprovedSewing.Caption = "FTTimeApprovedSewing"
            DTApprovedSewingInfo.Columns.Add(oColFTTimeApprovedSewing.ColumnName, oColFTTimeApprovedSewing.DataType)

            Dim oColFTUserApprovedSewing As System.Data.DataColumn
            oColFTUserApprovedSewing = New System.Data.DataColumn("FTUserApprovedSewing", System.Type.GetType("System.String"))
            oColFTUserApprovedSewing.Caption = "FTUserApprovedSewing"
            DTApprovedSewingInfo.Columns.Add(oColFTUserApprovedSewing.ColumnName, oColFTUserApprovedSewing.DataType)

            Dim oColFTStateApprovedSewingHide As System.Data.DataColumn
            oColFTStateApprovedSewingHide = New System.Data.DataColumn("FTStateApprovedSewingHide", System.Type.GetType("System.String"))
            oColFTStateApprovedComponentHide.Caption = "FTStateApprovedSewingHide"
            DTApprovedSewingInfo.Columns.Add(oColFTStateApprovedSewingHide.ColumnName, oColFTStateApprovedSewingHide.DataType)

            For Each oDataRow3 As System.Data.DataRow In tmpDTSewingApproved.Rows
                DTApprovedSewingInfo.ImportRow(oDataRow3)
            Next

            If Not tmpDTSewingApproved Is Nothing Then tmpDTSewingApproved.Dispose()

            Me.ogdApprovedSewingInfo.DataSource = DTApprovedSewingInfo
            Me.ogdApprovedSewingInfo.Refresh()

            'DTApprovedPackingInfo
            DTApprovedPackingInfo = New System.Data.DataTable

            Dim oColFTOrderNoPack As System.Data.DataColumn
            oColFTOrderNoPack = New System.Data.DataColumn("FTOrderNo", System.Type.GetType("System.String"))
            oColFTOrderNoPack.Caption = "FTOrderNo"
            DTApprovedPackingInfo.Columns.Add(oColFTOrderNoPack.ColumnName, oColFTOrderNoPack.DataType)

            Dim oColFTSubOrderNoPack As System.Data.DataColumn
            oColFTSubOrderNoPack = New System.Data.DataColumn("FTSubOrderNo", System.Type.GetType("System.String"))
            oColFTSubOrderNoPack.Caption = "FTSubOrderNo"
            DTApprovedPackingInfo.Columns.Add(oColFTSubOrderNoPack.ColumnName, oColFTSubOrderNoPack.DataType)

            Dim oColFTStateApprovedPacking As System.Data.DataColumn
            oColFTStateApprovedPacking = New System.Data.DataColumn("FTStateApprovedPacking", System.Type.GetType("System.String"))
            oColFTStateApprovedPacking.Caption = "FTStateApprovedPacking"
            DTApprovedPackingInfo.Columns.Add(oColFTStateApprovedPacking.ColumnName, oColFTStateApprovedPacking.DataType)

            Dim oColFNCntApprovedPacking As System.Data.DataColumn
            oColFNCntApprovedPacking = New System.Data.DataColumn("FNCntApprovedPacking", System.Type.GetType("System.Int32"))
            oColFNCntApprovedPacking.Caption = "FNCntApprovedPacking"
            DTApprovedPackingInfo.Columns.Add(oColFNCntApprovedPacking.ColumnName, oColFNCntApprovedPacking.DataType)

            Dim oColFDDateApprovedPacking As System.Data.DataColumn
            oColFDDateApprovedPacking = New System.Data.DataColumn("FDDateApprovedPacking", System.Type.GetType("System.String"))
            oColFDDateApprovedPacking.Caption = "FDDateApprovedPacking"
            DTApprovedPackingInfo.Columns.Add(oColFDDateApprovedPacking.ColumnName, oColFDDateApprovedPacking.DataType)

            Dim oColFTTimeApprovedPacking As System.Data.DataColumn
            oColFTTimeApprovedPacking = New System.Data.DataColumn("FTTimeApprovedPacking", System.Type.GetType("System.String"))
            oColFTTimeApprovedPacking.Caption = "FTTimeApprovedPacking"
            DTApprovedPackingInfo.Columns.Add(oColFTTimeApprovedPacking.ColumnName, oColFTTimeApprovedPacking.DataType)

            Dim oColFTUserApprovedPacking As System.Data.DataColumn
            oColFTUserApprovedPacking = New System.Data.DataColumn("FTUserApprovedPacking", System.Type.GetType("System.String"))
            oColFTUserApprovedPacking.Caption = "FTUserApprovedPacking"
            DTApprovedPackingInfo.Columns.Add(oColFTUserApprovedPacking.ColumnName, oColFTUserApprovedPacking.DataType)

            Dim oColFTStateApprovedPackingHide As System.Data.DataColumn
            oColFTStateApprovedPackingHide = New System.Data.DataColumn("FTStateApprovedPackingHide", System.Type.GetType("System.String"))
            oColFTStateApprovedPackingHide.Caption = "FTStateApprovedPackingHide"
            DTApprovedPackingInfo.Columns.Add(oColFTStateApprovedPackingHide.ColumnName, oColFTStateApprovedPackingHide.DataType)

            For Each oDataRow4 As System.Data.DataRow In tmpDTPackingApproved.Rows
                DTApprovedPackingInfo.ImportRow(oDataRow4)
            Next

            If Not tmpDTPackingApproved Is Nothing Then tmpDTPackingApproved.Dispose()

            Me.ogdApprovedPackingInfo.DataSource = DTApprovedPackingInfo
            Me.ogdApprovedPackingInfo.Refresh()

            'DTApprovedPackRatio
            DTApprovedPackRatio = New System.Data.DataTable

            Dim oColFTOrderNoPackRatio As System.Data.DataColumn
            oColFTOrderNoPackRatio = New System.Data.DataColumn("FTOrderNo", System.Type.GetType("System.String"))
            oColFTOrderNoPackRatio.Caption = "FTOrderNo"
            DTApprovedPackRatio.Columns.Add(oColFTOrderNoPackRatio.ColumnName, oColFTOrderNoPackRatio.DataType)

            Dim oColFTSubOrderNoPackRatio As System.Data.DataColumn
            oColFTSubOrderNoPackRatio = New System.Data.DataColumn("FTSubOrderNo", System.Type.GetType("System.String"))
            oColFTSubOrderNoPackRatio.Caption = "FTSubOrderNo"
            DTApprovedPackRatio.Columns.Add(oColFTSubOrderNoPackRatio.ColumnName, oColFTSubOrderNoPackRatio.DataType)

            Dim oColFTStateApprovedPackRatio As System.Data.DataColumn
            oColFTStateApprovedPackRatio = New System.Data.DataColumn("FTStateApprovedPackRatio", System.Type.GetType("System.String"))
            oColFTStateApprovedPackRatio.Caption = "FTStateApprovedPackRatio"
            DTApprovedPackRatio.Columns.Add(oColFTStateApprovedPackRatio.ColumnName, oColFTStateApprovedPackRatio.DataType)

            Dim oColFNCntApprovedPackRatio As System.Data.DataColumn
            oColFNCntApprovedPackRatio = New System.Data.DataColumn("FNCntApprovedPackRatio", System.Type.GetType("System.Int32"))
            oColFNCntApprovedPackRatio.Caption = "FNCntApprovedPackRatio"
            DTApprovedPackRatio.Columns.Add(oColFNCntApprovedPackRatio.ColumnName, oColFNCntApprovedPackRatio.DataType)

            Dim oColFDDateApprovedPackRatio As System.Data.DataColumn
            oColFDDateApprovedPackRatio = New System.Data.DataColumn("FDDateApprovedPackRatio", System.Type.GetType("System.String"))
            oColFDDateApprovedPackRatio.Caption = "FDDateApprovedPackRatio"
            DTApprovedPackRatio.Columns.Add(oColFDDateApprovedPackRatio.ColumnName, oColFDDateApprovedPackRatio.DataType)

            Dim oColFTTimeApprovedPackRatio As System.Data.DataColumn
            oColFTTimeApprovedPackRatio = New System.Data.DataColumn("FTTimeApprovedPackRatio", System.Type.GetType("System.String"))
            oColFTTimeApprovedPackRatio.Caption = "FTTimeApprovedPackRatio"
            DTApprovedPackRatio.Columns.Add(oColFTTimeApprovedPackRatio.ColumnName, oColFTTimeApprovedPackRatio.DataType)

            Dim oColFTUserApprovedPackRatio As System.Data.DataColumn
            oColFTUserApprovedPackRatio = New System.Data.DataColumn("FTUserApprovedPackRatio", System.Type.GetType("System.String"))
            oColFTUserApprovedPackRatio.Caption = "FTUserApprovedPackRatio"
            DTApprovedPackRatio.Columns.Add(oColFTUserApprovedPackRatio.ColumnName, oColFTUserApprovedPackRatio.DataType)

            Dim oColFTStateApprovedPackRatioHide As System.Data.DataColumn
            oColFTStateApprovedPackRatioHide = New System.Data.DataColumn("FTStateApprovedPackRatioHide", System.Type.GetType("System.String"))
            oColFTStateApprovedPackingHide.Caption = "FTStateApprovedPackRatioHide"
            DTApprovedPackRatio.Columns.Add(oColFTStateApprovedPackRatioHide.ColumnName, oColFTStateApprovedPackRatioHide.DataType)

            For Each oDataRow5 As System.Data.DataRow In tmpDTPackRatioApproved.Rows
                DTApprovedPackRatio.ImportRow(oDataRow5)
            Next

            If Not tmpDTPackRatioApproved Is Nothing Then tmpDTPackRatioApproved.Dispose()

            Me.ogdApprovedPackRatioInfo.DataSource = DTApprovedPackRatio
            Me.ogdApprovedPackRatioInfo.Refresh()

            'DTApprovedSizeSpecInfo
            DTApprovedSizeSpecInfo = New System.Data.DataTable

            Dim oColFTOrderNoSizeSpec As System.Data.DataColumn
            oColFTOrderNoSizeSpec = New System.Data.DataColumn("FTOrderNo", System.Type.GetType("System.String"))
            oColFTOrderNoSizeSpec.Caption = "FTOrderNo"
            DTApprovedSizeSpecInfo.Columns.Add(oColFTOrderNoSizeSpec.ColumnName, oColFTOrderNoSizeSpec.DataType)

            Dim oColFTSubOrderNoSizeSpec As System.Data.DataColumn
            oColFTSubOrderNoSizeSpec = New System.Data.DataColumn("FTSubOrderNo", System.Type.GetType("System.String"))
            oColFTSubOrderNoSizeSpec.Caption = "FTSubOrderNo"
            DTApprovedSizeSpecInfo.Columns.Add(oColFTSubOrderNoSizeSpec.ColumnName, oColFTSubOrderNoSizeSpec.DataType)

            Dim oColFTStateApprovedSizeSpec As System.Data.DataColumn
            oColFTStateApprovedSizeSpec = New System.Data.DataColumn("FTStateApprovedSizeSpec", System.Type.GetType("System.String"))
            oColFTStateApprovedSizeSpec.Caption = "FTStateApprovedSizeSpec"
            DTApprovedSizeSpecInfo.Columns.Add(oColFTStateApprovedSizeSpec.ColumnName, oColFTStateApprovedSizeSpec.DataType)

            Dim oColFNCntApprovedSizeSpec As System.Data.DataColumn
            oColFNCntApprovedSizeSpec = New System.Data.DataColumn("FNCntApprovedSizeSpec", System.Type.GetType("System.Int32"))
            oColFNCntApprovedSizeSpec.Caption = "FNCntApprovedSizeSpec"
            DTApprovedSizeSpecInfo.Columns.Add(oColFNCntApprovedSizeSpec.ColumnName, oColFNCntApprovedSubOrderNo.DataType)

            Dim oColFDDateApprovedSizeSpec As System.Data.DataColumn
            oColFDDateApprovedSizeSpec = New System.Data.DataColumn("FDDateApprovedSizeSpec", System.Type.GetType("System.String"))
            oColFDDateApprovedSizeSpec.Caption = "FDDateApprovedSizeSpec"
            DTApprovedSizeSpecInfo.Columns.Add(oColFDDateApprovedSizeSpec.ColumnName, oColFDDateApprovedSizeSpec.DataType)

            Dim oColFTTimeApprovedSizeSpec As System.Data.DataColumn
            oColFTTimeApprovedSizeSpec = New System.Data.DataColumn("FTTimeApprovedSizeSpec", System.Type.GetType("System.String"))
            oColFTTimeApprovedSizeSpec.Caption = "FTTimeApprovedSizeSpec"
            DTApprovedSizeSpecInfo.Columns.Add(oColFTTimeApprovedSizeSpec.ColumnName, oColFTTimeApprovedSizeSpec.DataType)

            Dim oColFTUserApprovedSizeSpec As System.Data.DataColumn
            oColFTUserApprovedSizeSpec = New System.Data.DataColumn("FTUserApprovedSizeSpec", System.Type.GetType("System.String"))
            oColFTUserApprovedSizeSpec.Caption = "FTUserApprovedSizeSpec"
            DTApprovedSizeSpecInfo.Columns.Add(oColFTUserApprovedSizeSpec.ColumnName, oColFTUserApprovedSizeSpec.DataType)

            Dim oColFTStateApprovedSizeSpecHide As System.Data.DataColumn
            oColFTStateApprovedSizeSpecHide = New System.Data.DataColumn("FTStateApprovedSizeSpecHide", System.Type.GetType("System.String"))
            oColFTStateApprovedSizeSpecHide.Caption = "FTStateApprovedSizeSpecHide"
            DTApprovedSizeSpecInfo.Columns.Add(oColFTStateApprovedSizeSpecHide.ColumnName, oColFTStateApprovedSizeSpecHide.DataType)

            For Each oDataRow6 As System.Data.DataRow In tmpDTSizeSpecApproved.Rows
                DTApprovedSizeSpecInfo.ImportRow(oDataRow6)
            Next

            If Not tmpDTSizeSpecApproved Is Nothing Then tmpDTSizeSpecApproved.Dispose()

            Me.ogdApprovedSizeSpecInfo.DataSource = DTApprovedSizeSpecInfo
            Me.ogdApprovedSizeSpecInfo.Refresh()

            bRetLoadComplete = True

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
            End If
        End Try

        Return bRetLoadComplete

    End Function

    Private Function PROC_SHOWbBrowseDataListApprovedOrderInfo() As Boolean

        Dim bRetLoadComplete As Boolean = False
        Dim sSQLWhere As String
        Dim DTApprovedOrderInfo As System.Data.DataTable
        Dim DTApprovedSubOrder As System.Data.DataTable
        '=================================================================
        Dim DTApprovedSubOrderInfo As System.Data.DataTable
        Dim DTApprovedComponentInfo As System.Data.DataTable
        Dim DTApprovedSewingInfo As System.Data.DataTable
        Dim DTApprovedPackingInfo As System.Data.DataTable
        Dim DTApprovedPackRatio As System.Data.DataTable
        Dim DTApprovedSizeSpecInfo As System.Data.DataTable
        '=================================================================
        Try
            Dim bChkApprovedTabOrderInfo As Boolean = False
            Dim bChkApprovedTabSubOrderNo As Boolean = False
            Dim bChkApprovedTabComponent As Boolean = False
            Dim bChkApprovedTabSewing As Boolean = False
            Dim bChkApprovedTabPacking As Boolean = False
            Dim bChkApprovedTabPackCarton As Boolean = False
            Dim bChkApprovedTabSizeSpec As Boolean = False

            If bFlagLoad = True Then '...FNHSysBuyId, FNHSysStyleId ==> EditValue_Changed
                bChkApprovedTabOrderInfo = True : bChkApprovedTabSubOrderNo = True : bChkApprovedTabComponent = True : bChkApprovedTabSewing = True : bChkApprovedTabPacking = True : bChkApprovedTabPackCarton = True : bChkApprovedTabSizeSpec = True
            Else
                Select Case Me.oTabApprovedInfo.SelectedTabPageIndex
                    Case eApprovedTabIndex.ApprovedOrderNo : bChkApprovedTabOrderInfo = True
                    Case eApprovedTabIndex.ApprovedSubOrderNo : bChkApprovedTabSubOrderNo = True
                    Case eApprovedTabIndex.ApprovedComponent : bChkApprovedTabComponent = True
                    Case eApprovedTabIndex.ApprovedSewing : bChkApprovedTabSewing = True
                    Case eApprovedTabIndex.ApprovedPacking : bChkApprovedTabPacking = True
                    Case eApprovedTabIndex.ApprovedPackingCarton : bChkApprovedTabPackCarton = True
                    Case eApprovedTabIndex.ApprovedSizeSpec : bChkApprovedTabSizeSpec = True
                End Select

            End If

            If bFlagSaveAllInfo = True Then
                If Not bChkApprovedTabOrderInfo Then bChkApprovedTabOrderInfo = True
                If Not bChkApprovedTabSubOrderNo Then bChkApprovedTabSubOrderNo = True
                If Not bChkApprovedTabComponent Then bChkApprovedTabComponent = True
                If Not bChkApprovedTabSewing Then bChkApprovedTabSewing = True
                If Not bChkApprovedTabPacking Then bChkApprovedTabPacking = True
                If Not bChkApprovedTabPackCarton Then bChkApprovedTabPackCarton = True
                If Not bChkApprovedTabSizeSpec Then bChkApprovedTabSizeSpec = True
            End If

            Me.oChkApproveAllInfo.Checked = False
            Me.oChkApproveAllRecord.Checked = False

            If bChkApprovedTabOrderInfo = True Then
                Me.ogdApprovedOrderInfo.DataSource = Nothing : Me.ogdApprovedOrderInfo.Refresh()

                sSQL = ""
                sSQL = "SELECT  C.FTStyleCode AS FTStyleCode,"
                sSQL &= Environment.NewLine & "       C.FTStyleNameEN AS FTStyleName,SEA.FTSeasonCode,"
                sSQL &= Environment.NewLine & "       B.FTOrderNo AS FTOrderNo,"
                sSQL &= Environment.NewLine & "       CASE WHEN ISNULL(A.FTApprovedInfoState, '0') = '0' THEN '0' ELSE '1' END   AS FTApprovedInfoState,"
                sSQL &= Environment.NewLine & "       A.FNApprovedInfoCnt,"
                sSQL &= Environment.NewLine & "       CONVERT(VARCHAR(10), CAST(A.FDApprovedInfoDate AS DATE), 103) AS FDApprovedInfoDate,"
                sSQL &= Environment.NewLine & "       A.FTApprovedInfoTime,"
                sSQL &= Environment.NewLine & "       A.FTApprovedInfoBy,"
                sSQL &= Environment.NewLine & "       CASE WHEN ISNULL(A.FTRevisedInfoState, '0') = '0' THEN '0' ELSE '1' END AS FTRevisedInfoState,"
                sSQL &= Environment.NewLine & "       A.FNRevisedInfoCnt,"
                sSQL &= Environment.NewLine & "       CONVERT(VARCHAR(10), CAST(A.FDRevisedInfoDate AS DATE), 103) AS FDRevisedInfoDate,"
                sSQL &= Environment.NewLine & "       A.FTRevisedInfoTime,"
                sSQL &= Environment.NewLine & "       A.FTRevisedInfoBy,"
                sSQL &= Environment.NewLine & "       CASE WHEN ISNULL(A.FTApprovedInfoState, '0') = '0' THEN '0' ELSE '1' END   AS FTApprovedInfoStateHide"
                sSQL &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS B (NOLOCK) LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder_ApprovedInfo AS A (NOLOCK) ON A.FTOrderNo = B.FTOrderNo"
                sSQL &= Environment.NewLine & "                                                                                                         LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS C (NOLOCK) ON B.FNHSysStyleId = C.FNHSysStyleId"
                sSQL &= Environment.NewLine & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SEA WITH(NOLOCK)  ON B.FNHSysSeasonId = SEA.FNHSysSeasonId"
                REM 2015/03/09
                'sSQL = "SELECT B.FTOrderNo AS FTOrderNo,"
                'sSQL &= Environment.NewLine & "       CASE WHEN ISNULL(A.FTApprovedInfoState, '0') = '0' THEN '0' ELSE '1' END   AS FTApprovedInfoState,"
                'sSQL &= Environment.NewLine & "       A.FNApprovedInfoCnt,"
                'sSQL &= Environment.NewLine & "       CONVERT(VARCHAR(10), CAST(A.FDApprovedInfoDate AS DATE), 103) AS FDApprovedInfoDate,"
                'sSQL &= Environment.NewLine & "       A.FTApprovedInfoTime,"
                'sSQL &= Environment.NewLine & "       A.FTApprovedInfoBy,"
                'sSQL &= Environment.NewLine & "       CASE WHEN ISNULL(A.FTRevisedInfoState, '0') = '0' THEN '0' ELSE '1' END AS FTRevisedInfoState,"
                'sSQL &= Environment.NewLine & "       A.FNRevisedInfoCnt,"
                'sSQL &= Environment.NewLine & "       CONVERT(VARCHAR(10), CAST(A.FDRevisedInfoDate AS DATE), 103) AS FDRevisedInfoDate,"
                'sSQL &= Environment.NewLine & "       A.FTRevisedInfoTime,"
                'sSQL &= Environment.NewLine & "       A.FTRevisedInfoBy,"
                'sSQL &= Environment.NewLine & "       CASE WHEN ISNULL(A.FTApprovedInfoState, '0') = '0' THEN '0' ELSE '1' END   AS FTApprovedInfoStateHide"
                'sSQL &= Environment.NewLine & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS B (NOLOCK) LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder_ApprovedInfo AS A (NOLOCK) ON A.FTOrderNo = B.FTOrderNo"
                'sSQL &= Environment.NewLine & "                                                                                                         LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS C (NOLOCK) ON B.FNHSysStyleId = C.FNHSysStyleId"

                sSQLWhere = ""

                If Me.FNHSysBuyId.Text.Trim <> "" Then
                    If sSQLWhere <> "" Then sSQLWhere = sSQLWhere & "   AND "
                    sSQLWhere &= "B.FNHSysBuyId = " & Val(Me.FNHSysBuyId.Properties.Tag)
                End If

                If Me.FNHSysStyleId.Text.Trim <> "" Then
                    If sSQLWhere <> "" Then sSQLWhere = sSQLWhere & "   AND "
                    sSQLWhere &= "B.FNHSysStyleId = " & Val(Me.FNHSysStyleId.Properties.Tag)
                End If

                If sSQLWhere <> "" Then sSQL &= Environment.NewLine & "WHERE " & sSQLWhere

                Select Case FNJobAppinfoState.SelectedIndex
                    Case 0
                        sSQL &= Environment.NewLine & "    AND B.FNJobState IN (1)"
                    Case 1
                        sSQL &= Environment.NewLine & "    AND B.FNJobState IN (0)"
                    Case Else
                        sSQL &= Environment.NewLine & "    AND B.FNJobState IN (0, 1)"
                End Select

                If Not HI.ST.SysInfo.Admin Then
                    sSQL &= Environment.NewLine & "      AND B.FNHSysMerTeamId = (SELECT TOP 1 L1.FNHSysMerTeamId AS FNHSysMerTeamId"
                    sSQL &= Environment.NewLine & "                               FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..[TSEUserLogin] AS L1 WITH(NOLOCK)"
                    sSQL &= Environment.NewLine & "                               WHERE L1.FTUserName = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
                End If

                REM 2015/03/10 sSQL &= Environment.NewLine & "ORDER BY C.FTStyleCode ASC, A.FTOrderNo ASC;"
                'sSQL &= Environment.NewLine & "ORDER BY C.FTStyleCode ASC, A.FTApprovedInfoState ASC, B.FTOrderNo ASC"
                sSQL &= Environment.NewLine & "ORDER BY A.FTApprovedInfoState ASC, C.FTStyleCode ASC, B.FTOrderNo ASC;"

                DTApprovedOrderInfo = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

                Me.ogdApprovedOrderInfo.DataSource = DTApprovedOrderInfo
                Me.ogdApprovedOrderInfo.Refresh()

                If bFlagLoad = True Then
                    Me.ogvApprovedOrderInfo.ClearColumnsFilter()
                    Me.ogvApprovedOrderInfo.ActiveFilter.Clear()
                End If

            End If

            sSQL = ""
            REM 2015/03/09
            'sSQL = "SELECT A.[FTOrderNo] AS FTOrderNo, C.[FTSubOrderNo] AS FTSubOrderNo"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSubOrderNo], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSubOrderNo, B.[FNCntApprovedSubOrderNo], CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedSubOrderNo] AS DATE), 103) AS FDDateApprovedSubOrderNo, B.[FTTimeApprovedSubOrderNo], B.[FTUserApprovedSubOrderNo]"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedComponent], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedComponent, B.[FNCntApprovedComponent], CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedComponent] AS DATE), 103) AS FDDateApprovedComponent, B.[FTTimeApprovedComponent], B.[FTUserApprovedComponent]"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSewing], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSewing, B.[FNCntApprovedSewing], CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedSewing] AS DATE), 103) AS FDDateApprovedSewing, B.[FTTimeApprovedSewing], B.[FTUserApprovedSewing]"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPacking], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPacking, B.[FNCntApprovedPacking], CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedPacking] AS DATE), 103) AS FDDateApprovedPacking, B.[FTTimeApprovedPacking], B.[FTUserApprovedPacking]"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPackRatio], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPackRatio, B.[FNCntApprovedPackRatio], CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedPackRatio] AS DATE), 103) AS FDDateApprovedPackRatio, B.[FTTimeApprovedPackRatio], B.[FTUserApprovedPackRatio]"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSizeSpec], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSizeSpec, B.[FNCntApprovedSizeSpec], CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedSizeSpec] AS DATE), 103) AS FDDateApprovedSizeSpec, B.[FTTimeApprovedSizeSpec], B.[FTUserApprovedSizeSpec]"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSubOrderNo], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSubOrderNoHide"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedComponent], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedComponentHide"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSewing], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSewingHide"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPacking], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPackingHide"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPackRatio], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPackRatioHide"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSizeSpec], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSizeSpecHide"
            'sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS A (NOLOCK) LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS C (NOLOCK) ON A.FTOrderNo = C.FTOrderNo"
            'sSQL &= Environment.NewLine & "                                       LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS B (NOLOCK) ON B.FTOrderNo = C.FTOrderNo"
            'sSQL &= Environment.NewLine & "											                                                                            AND B.FTSubOrderNo = C.FTSubOrderNo"

            REM 2015/03/09
            'sSQL = "SELECT D.FTStyleCode, D.FTStyleNameEN AS FTStyleName"
            'sSQL &= Environment.NewLine & "     ,A.[FTOrderNo] AS FTOrderNo, C.[FTSubOrderNo] AS FTSubOrderNo"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSubOrderNo], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSubOrderNo, B.[FNCntApprovedSubOrderNo], CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedSubOrderNo] AS DATE), 103) AS FDDateApprovedSubOrderNo, B.[FTTimeApprovedSubOrderNo], B.[FTUserApprovedSubOrderNo]"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedComponent], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedComponent, B.[FNCntApprovedComponent], CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedComponent] AS DATE), 103) AS FDDateApprovedComponent, B.[FTTimeApprovedComponent], B.[FTUserApprovedComponent]"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSewing], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSewing, B.[FNCntApprovedSewing], CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedSewing] AS DATE), 103) AS FDDateApprovedSewing, B.[FTTimeApprovedSewing], B.[FTUserApprovedSewing]"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPacking], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPacking, B.[FNCntApprovedPacking], CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedPacking] AS DATE), 103) AS FDDateApprovedPacking, B.[FTTimeApprovedPacking], B.[FTUserApprovedPacking]"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPackRatio], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPackRatio, B.[FNCntApprovedPackRatio], CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedPackRatio] AS DATE), 103) AS FDDateApprovedPackRatio, B.[FTTimeApprovedPackRatio], B.[FTUserApprovedPackRatio]"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSizeSpec], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSizeSpec, B.[FNCntApprovedSizeSpec], CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedSizeSpec] AS DATE), 103) AS FDDateApprovedSizeSpec, B.[FTTimeApprovedSizeSpec], B.[FTUserApprovedSizeSpec]"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSubOrderNo], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSubOrderNoHide"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedComponent], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedComponentHide"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSewing], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSewingHide"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPacking], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPackingHide"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPackRatio], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPackRatioHide"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSizeSpec], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSizeSpecHide"
            'sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS A (NOLOCK) LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS C (NOLOCK) ON A.FTOrderNo = C.FTOrderNo"
            'sSQL &= Environment.NewLine & "                                       LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS B (NOLOCK) ON B.FTOrderNo = C.FTOrderNo"
            'sSQL &= Environment.NewLine & "	                                      LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS D (NOLOCK) ON A.FNHSysStyleId = D.FNHSysStyleId"

            REM 2015/03/10
            'sSQL = "SELECT D.FTStyleCode, D.FTStyleNameEN AS FTStyleName"
            'sSQL &= Environment.NewLine & "     ,A.[FTOrderNo] AS FTOrderNo, C.[FTSubOrderNo] AS FTSubOrderNo"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSubOrderNo], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSubOrderNo, CASE WHEN B.[FNCntApprovedSubOrderNo] = 0 THEN NULL ELSE B.FNCntApprovedSubOrderNo END AS FNCntApprovedSubOrderNo, CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedSubOrderNo] AS DATE), 103) AS FDDateApprovedSubOrderNo, B.[FTTimeApprovedSubOrderNo], B.[FTUserApprovedSubOrderNo]"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedComponent], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedComponent, CASE WHEN B.[FNCntApprovedComponent] = 0 THEN NULL ELSE B.FNCntApprovedComponent END AS FNCntApprovedComponent, CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedComponent] AS DATE), 103) AS FDDateApprovedComponent, B.[FTTimeApprovedComponent], B.[FTUserApprovedComponent]"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSewing], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSewing, CASE WHEN B.[FNCntApprovedSewing] = 0 THEN NULL ELSE B.FNCntApprovedSewing END AS FNCntApprovedSewing, CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedSewing] AS DATE), 103) AS FDDateApprovedSewing, B.[FTTimeApprovedSewing], B.[FTUserApprovedSewing]"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPacking], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPacking, CASE WHEN B.[FNCntApprovedPacking] = 0 THEN NULL ELSE B.FNCntApprovedPacking END AS FNCntApprovedPacking, CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedPacking] AS DATE), 103) AS FDDateApprovedPacking, B.[FTTimeApprovedPacking], B.[FTUserApprovedPacking]"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPackRatio], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPackRatio, CASE WHEN B.[FNCntApprovedPackRatio] = 0 THEN NULL ELSE B.FNCntApprovedPackRatio END AS FNCntApprovedPackRatio, CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedPackRatio] AS DATE), 103) AS FDDateApprovedPackRatio, B.[FTTimeApprovedPackRatio], B.[FTUserApprovedPackRatio]"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSizeSpec], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSizeSpec, CASE WHEN B.[FNCntApprovedSizeSpec] = 0 THEN NULL ELSE B.FNCntApprovedSizeSpec END AS FNCntApprovedSizeSpec, CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedSizeSpec] AS DATE), 103) AS FDDateApprovedSizeSpec, B.[FTTimeApprovedSizeSpec], B.[FTUserApprovedSizeSpec]"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSubOrderNo], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSubOrderNoHide"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedComponent], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedComponentHide"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSewing], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSewingHide"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPacking], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPackingHide"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPackRatio], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPackRatioHide"
            'sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSizeSpec], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSizeSpecHide"
            'sSQL &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS A (NOLOCK) LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS C (NOLOCK) ON A.FTOrderNo = C.FTOrderNo"
            'sSQL &= Environment.NewLine & "                                       LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS B (NOLOCK) ON B.FTOrderNo = C.FTOrderNo"
            'sSQL &= Environment.NewLine & "	                                      LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS D (NOLOCK) ON A.FNHSysStyleId = D.FNHSysStyleId"

            sSQL = "SELECT  D.FTStyleCode, D.FTStyleNameEN AS FTStyleName,SEA.FTSeasonCode"
            sSQL &= Environment.NewLine & "         ,A.[FTOrderNo] AS FTOrderNo, A.[FTSubOrderNo] AS FTSubOrderNo"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSubOrderNo], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSubOrderNo, CASE WHEN B.[FNCntApprovedSubOrderNo] = 0 THEN NULL ELSE B.FNCntApprovedSubOrderNo END AS FNCntApprovedSubOrderNo, CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedSubOrderNo] AS DATE), 103) AS FDDateApprovedSubOrderNo, B.[FTTimeApprovedSubOrderNo], B.[FTUserApprovedSubOrderNo]"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedComponent], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedComponent, CASE WHEN B.[FNCntApprovedComponent] = 0 THEN NULL ELSE B.FNCntApprovedComponent END AS FNCntApprovedComponent, CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedComponent] AS DATE), 103) AS FDDateApprovedComponent, B.[FTTimeApprovedComponent], B.[FTUserApprovedComponent]"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSewing], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSewing, CASE WHEN B.[FNCntApprovedSewing] = 0 THEN NULL ELSE B.FNCntApprovedSewing END AS FNCntApprovedSewing, CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedSewing] AS DATE), 103) AS FDDateApprovedSewing, B.[FTTimeApprovedSewing], B.[FTUserApprovedSewing]"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPacking], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPacking, CASE WHEN B.[FNCntApprovedPacking] = 0 THEN NULL ELSE B.FNCntApprovedPacking END AS FNCntApprovedPacking, CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedPacking] AS DATE), 103) AS FDDateApprovedPacking, B.[FTTimeApprovedPacking], B.[FTUserApprovedPacking]"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPackRatio], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPackRatio, CASE WHEN B.[FNCntApprovedPackRatio] = 0 THEN NULL ELSE B.FNCntApprovedPackRatio END AS FNCntApprovedPackRatio, CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedPackRatio] AS DATE), 103) AS FDDateApprovedPackRatio, B.[FTTimeApprovedPackRatio], B.[FTUserApprovedPackRatio]"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSizeSpec], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSizeSpec, CASE WHEN B.[FNCntApprovedSizeSpec] = 0 THEN NULL ELSE B.FNCntApprovedSizeSpec END AS FNCntApprovedSizeSpec, CONVERT(VARCHAR(10), CAST(B.[FDDateApprovedSizeSpec] AS DATE), 103) AS FDDateApprovedSizeSpec, B.[FTTimeApprovedSizeSpec], B.[FTUserApprovedSizeSpec]"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSubOrderNo], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSubOrderNoHide"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedComponent], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedComponentHide"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSewing], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSewingHide"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPacking], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPackingHide"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedPackRatio], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedPackRatioHide"
            sSQL &= Environment.NewLine & "     ,CASE WHEN ISNULL(B.[FTStateApprovedSizeSpec], '0') = N'0' THEN '0' ELSE '1' END AS FTStateApprovedSizeSpecHide"
            sSQL &= Environment.NewLine & "FROM (SELECT L1.FNHSysBuyId, L1.FNHSysStyleId, L2.FTOrderNo, L1.FNJobState, L2.FTSubOrderNo, L1.FNHSysMerTeamId,L1.FNHSysSeasonId"
            sSQL &= Environment.NewLine & "      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS L1 (NOLOCK) , HITECH_MERCHAN..TMERTOrderSub AS L2 (NOLOCK)"
            sSQL &= Environment.NewLine & "      WHERE L1.FTOrderNo = L2.FTOrderNo"
            sSQL &= Environment.NewLine & "     ) AS A LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS B (NOLOCK) ON A.FTOrderNo = B.FTOrderNo"
            sSQL &= Environment.NewLine & "                                                                                                                                            AND A.FTSubOrderNo = B.FTSubOrderNo"
            sSQL &= Environment.NewLine & "            LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS D (NOLOCK) ON A.FNHSysStyleId = D.FNHSysStyleId"
            sSQL &= Environment.NewLine & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SEA WITH(NOLOCK)  ON A.FNHSysSeasonId = SEA.FNHSysSeasonId"

            sSQLWhere = ""

            If Me.FNHSysBuyId.Text.Trim <> "" Then
                If sSQLWhere <> "" Then sSQLWhere = sSQLWhere & "    AND "
                sSQLWhere &= "A.FNHSysBuyId = " & Val(Me.FNHSysBuyId.Properties.Tag)
            End If

            If Me.FNHSysStyleId.Text.Trim <> "" Then
                If sSQLWhere <> "" Then sSQLWhere = sSQLWhere & "    AND "
                sSQLWhere &= "A.FNHSysStyleId = " & Val(Me.FNHSysStyleId.Properties.Tag)
            End If

            If sSQLWhere <> "" Then sSQL &= Environment.NewLine & "WHERE " & sSQLWhere


            Select Case FNJobAppinfoState.SelectedIndex
                Case 0
                    sSQL &= Environment.NewLine & "    AND A.FNJobState IN (1)"
                Case 1
                    sSQL &= Environment.NewLine & "    AND A.FNJobState IN (0)"
                Case Else
                    sSQL &= Environment.NewLine & "    AND A.FNJobState IN (0, 1)"
            End Select

            '  sSQL &= Environment.NewLine & "     AND A.FNJobState IN (0, 1)"

            If Not HI.ST.SysInfo.Admin Then
                sSQL &= Environment.NewLine & "      AND A.FNHSysMerTeamId = (SELECT TOP 1 L1.FNHSysMerTeamId AS FNHSysMerTeamId"
                sSQL &= Environment.NewLine & "                               FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "]..[TSEUserLogin] AS L1 WITH(NOLOCK)"
                sSQL &= Environment.NewLine & "                               WHERE L1.FTUserName = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "')"
            End If

            'sSQL &= Environment.NewLine & "ORDER BY  D.FTStyleCode ASC, A.FTOrderNo ASC, C.FTSubOrderNo ASC;"
            sSQL &= Environment.NewLine & "ORDER BY D.FTStyleCode ASC, A.FTOrderNo, A.FTSubOrderNo ASC;"

            DTApprovedSubOrder = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If bChkApprovedTabSubOrderNo = True Then
                Me.ogdApprovedSubOrderNoInfo.DataSource = Nothing : Me.ogdApprovedSubOrderNoInfo.Refresh()

                Dim tmpDTSubOrderNoApproved As System.Data.DataTable

                tmpDTSubOrderNoApproved = DTApprovedSubOrder.Copy()

                If Not tmpDTSubOrderNoApproved Is Nothing Then
                    tmpDTSubOrderNoApproved.BeginInit()
                    For numLoopSubOrderNo As Integer = tmpDTSubOrderNoApproved.Columns.Count - 1 To 0 Step -1
                        Select Case tmpDTSubOrderNoApproved.Columns(numLoopSubOrderNo).ColumnName.ToUpper
                            Case "FTStateSelect".ToUpper, "FTStyleCode".ToUpper, "FTSeasonCode".ToUpper, "FTStyleName".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTStateApprovedSubOrderNo".ToUpper, "FNCntApprovedSubOrderNo".ToUpper, "FDDateApprovedSubOrderNo".ToUpper, "FTTimeApprovedSubOrderNo".ToUpper, "FTUserApprovedSubOrderNo".ToUpper, "FTStateApprovedSubOrderNoHide".ToUpper
                                '...Nothing
                            Case Else
                                tmpDTSubOrderNoApproved.Columns.Remove(tmpDTSubOrderNoApproved.Columns(numLoopSubOrderNo))
                        End Select

                    Next
                    tmpDTSubOrderNoApproved.EndInit()
                    tmpDTSubOrderNoApproved.AcceptChanges()
                End If

                '...DTApprovedSubOrderInfo
                DTApprovedSubOrderInfo = New System.Data.DataTable

                'Dim oColFTStateSelectSubOrderNo As System.Data.DataColumn
                'oColFTStateSelectSubOrderNo = New System.Data.DataColumn("FTStateSelect", System.Type.GetType("System.String"))
                ''oColFTStyleCodeSubOrderNo.Caption = "FTStyleCode"
                'DTApprovedSubOrderInfo.Columns.Add(oColFTStateSelectSubOrderNo.ColumnName, oColFTStateSelectSubOrderNo.DataType)

                Dim oColFTStyleCodeSubOrderNo As System.Data.DataColumn
                oColFTStyleCodeSubOrderNo = New System.Data.DataColumn("FTStyleCode", System.Type.GetType("System.String"))
                'oColFTStyleCodeSubOrderNo.Caption = "FTStyleCode"
                DTApprovedSubOrderInfo.Columns.Add(oColFTStyleCodeSubOrderNo.ColumnName, oColFTStyleCodeSubOrderNo.DataType)

                Dim oColFTStyleNameSubOrderNo As System.Data.DataColumn
                oColFTStyleNameSubOrderNo = New System.Data.DataColumn("FTStyleName", System.Type.GetType("System.String"))
                oColFTStyleNameSubOrderNo.Caption = "FTStyleName"
                DTApprovedSubOrderInfo.Columns.Add(oColFTStyleNameSubOrderNo.ColumnName, oColFTStyleCodeSubOrderNo.DataType)

                Dim oColFTStyleCodeSeason As System.Data.DataColumn
                oColFTStyleCodeSeason = New System.Data.DataColumn("FTSeasonCode", System.Type.GetType("System.String"))
                'oColFTStyleCodeSubOrderNo.Caption = "FTStyleCode"
                DTApprovedSubOrderInfo.Columns.Add(oColFTStyleCodeSeason.ColumnName, oColFTStyleCodeSeason.DataType)

                Dim oColFTOrderNo As System.Data.DataColumn
                oColFTOrderNo = New System.Data.DataColumn("FTOrderNo", System.Type.GetType("System.String"))
                'oColFTOrderNo.Caption = "FTOrderNo"
                DTApprovedSubOrderInfo.Columns.Add(oColFTOrderNo.ColumnName, oColFTOrderNo.DataType)

                Dim oColFTSubOrderNo As System.Data.DataColumn
                oColFTSubOrderNo = New System.Data.DataColumn("FTSubOrderNo", System.Type.GetType("System.String"))
                oColFTSubOrderNo.Caption = "FTSubOrderNo"
                DTApprovedSubOrderInfo.Columns.Add(oColFTSubOrderNo.ColumnName, oColFTSubOrderNo.DataType)

                Dim oColFTStateApprovedSubOrderNo As System.Data.DataColumn
                oColFTStateApprovedSubOrderNo = New System.Data.DataColumn("FTStateApprovedSubOrderNo", System.Type.GetType("System.String"))
                'oColFTStateApprovedSubOrderNo.Caption = "FTStateApprovedSubOrderNo"
                DTApprovedSubOrderInfo.Columns.Add(oColFTStateApprovedSubOrderNo.ColumnName, oColFTStateApprovedSubOrderNo.DataType)

                Dim oColFNCntApprovedSubOrderNo As System.Data.DataColumn
                oColFNCntApprovedSubOrderNo = New System.Data.DataColumn("FNCntApprovedSubOrderNo", System.Type.GetType("System.Int32"))
                'oColFNCntApprovedSubOrderNo.Caption = "FNCntApprovedSubOrderNo"
                DTApprovedSubOrderInfo.Columns.Add(oColFNCntApprovedSubOrderNo.ColumnName, oColFNCntApprovedSubOrderNo.DataType)

                Dim oColFDDateApprovedSubOrderNo As System.Data.DataColumn
                oColFDDateApprovedSubOrderNo = New System.Data.DataColumn("FDDateApprovedSubOrderNo", System.Type.GetType("System.String"))
                'oColFDDateApprovedSubOrderNo.Caption = "FDDateApprovedSubOrderNo"
                DTApprovedSubOrderInfo.Columns.Add(oColFDDateApprovedSubOrderNo.ColumnName, oColFDDateApprovedSubOrderNo.DataType)

                Dim oColFTTimeApprovedSubOrderNo As System.Data.DataColumn
                oColFTTimeApprovedSubOrderNo = New System.Data.DataColumn("FTTimeApprovedSubOrderNo", System.Type.GetType("System.String"))
                'oColFTTimeApprovedSubOrderNo.Caption = "FTTimeApprovedSubOrderNo"
                DTApprovedSubOrderInfo.Columns.Add(oColFTTimeApprovedSubOrderNo.ColumnName, oColFTTimeApprovedSubOrderNo.DataType)

                Dim oColFTUserApprovedSubOrderNo As System.Data.DataColumn
                oColFTUserApprovedSubOrderNo = New System.Data.DataColumn("FTUserApprovedSubOrderNo", System.Type.GetType("System.String"))
                'oColFTUserApprovedSubOrderNo.Caption = "FTUserApprovedSubOrderNo"
                DTApprovedSubOrderInfo.Columns.Add(oColFTUserApprovedSubOrderNo.ColumnName, oColFTUserApprovedSubOrderNo.DataType)

                Dim oColFTStateApprovedSubOrderNoHide As System.Data.DataColumn
                oColFTStateApprovedSubOrderNoHide = New System.Data.DataColumn("FTStateApprovedSubOrderNoHide", System.Type.GetType("System.String"))
                ' oColFTStateApprovedSubOrderNoHide.Caption = "FTStateApprovedSubOrderNoHide"
                DTApprovedSubOrderInfo.Columns.Add(oColFTStateApprovedSubOrderNoHide.ColumnName, oColFTStateApprovedSubOrderNoHide.DataType)

                For Each oDataRow1 As System.Data.DataRow In tmpDTSubOrderNoApproved.Rows
                    DTApprovedSubOrderInfo.ImportRow(oDataRow1)
                Next

                If Not DTApprovedSubOrderInfo Is Nothing AndAlso DTApprovedSubOrderInfo.Rows.Count > 0 Then DTApprovedSubOrderInfo.DefaultView.Sort = "FTStateApprovedSubOrderNo ASC, FTStyleCode ASC, FTOrderNo ASC, FTSubOrderNo ASC"

                If Not tmpDTSubOrderNoApproved Is Nothing Then tmpDTSubOrderNoApproved.Dispose()

                Me.ogdApprovedSubOrderNoInfo.DataSource = DTApprovedSubOrderInfo
                Me.ogdApprovedSubOrderNoInfo.Refresh()

                If bFlagLoad = True Then
                    Me.ogvApprovedSubOrderNoInfo.ClearColumnsFilter()
                    Me.ogvApprovedSubOrderNoInfo.ActiveFilter.Clear()
                End If

            End If

            If bChkApprovedTabComponent = True Then
                Me.ogdApprovedComponentInfo.DataSource = Nothing : Me.ogdApprovedComponentInfo.Refresh()

                Dim tmpDTComponentApproved As System.Data.DataTable

                tmpDTComponentApproved = DTApprovedSubOrder.Copy()

                If Not tmpDTComponentApproved Is Nothing Then
                    tmpDTComponentApproved.BeginInit()
                    For numLoopComponent As Integer = tmpDTComponentApproved.Columns.Count - 1 To 0 Step -1
                        Select Case tmpDTComponentApproved.Columns(numLoopComponent).ColumnName.ToUpper
                            Case "FTStateSelect".ToUpper, "FTStyleCode".ToUpper, "FTSeasonCode".ToUpper, "FTStyleName".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTStateApprovedComponent".ToUpper, "FNCntApprovedComponent".ToUpper, "FDDateApprovedComponent".ToUpper, "FTTimeApprovedComponent".ToUpper, "FTUserApprovedComponent".ToUpper, "FTStateApprovedSubOrderNoHide".ToUpper, "FTStateApprovedComponentHide".ToUpper
                                '...Nothing
                            Case Else
                                tmpDTComponentApproved.Columns.Remove(tmpDTComponentApproved.Columns(numLoopComponent))
                        End Select

                    Next
                    tmpDTComponentApproved.EndInit()
                    tmpDTComponentApproved.AcceptChanges()
                End If

                'DTApprovedComponentInfo
                DTApprovedComponentInfo = New System.Data.DataTable

                'Dim oColFTStateSelectComponent As System.Data.DataColumn
                'oColFTStateSelectComponent = New System.Data.DataColumn("FTStateSelect", System.Type.GetType("System.String"))
                ''oColFTStyleCodeComponent.Caption = "FTStyleCode"
                'DTApprovedComponentInfo.Columns.Add(oColFTStateSelectComponent.ColumnName, oColFTStateSelectComponent.DataType)

                Dim oColFTStyleCodeComponent As System.Data.DataColumn
                oColFTStyleCodeComponent = New System.Data.DataColumn("FTStyleCode", System.Type.GetType("System.String"))
                'oColFTStyleCodeComponent.Caption = "FTStyleCode"
                DTApprovedComponentInfo.Columns.Add(oColFTStyleCodeComponent.ColumnName, oColFTStyleCodeComponent.DataType)

                Dim oColFTStyleNameComponent As System.Data.DataColumn
                oColFTStyleNameComponent = New System.Data.DataColumn("FTStyleName", System.Type.GetType("System.String"))
                'oColFTStyleNameComponent.Caption = "FTStyleName"
                DTApprovedComponentInfo.Columns.Add(oColFTStyleNameComponent.ColumnName, oColFTStyleNameComponent.DataType)

                Dim oColFTStyleNameComponentSeason As System.Data.DataColumn
                oColFTStyleNameComponentSeason = New System.Data.DataColumn("FTSeasonCode", System.Type.GetType("System.String"))
                'oColFTStyleNameComponent.Caption = "FTStyleName"
                DTApprovedComponentInfo.Columns.Add(oColFTStyleNameComponentSeason.ColumnName, oColFTStyleNameComponentSeason.DataType)

                Dim oColFTOrderNoComponent As System.Data.DataColumn
                oColFTOrderNoComponent = New System.Data.DataColumn("FTOrderNo", System.Type.GetType("System.String"))
                'oColFTOrderNoComponent.Caption = "FTOrderNo"
                DTApprovedComponentInfo.Columns.Add(oColFTOrderNoComponent.ColumnName, oColFTOrderNoComponent.DataType)

                Dim oColFTSubOrderNoComponent As System.Data.DataColumn
                oColFTSubOrderNoComponent = New System.Data.DataColumn("FTSubOrderNo", System.Type.GetType("System.String"))
                'oColFTSubOrderNoComponent.Caption = "FTSubOrderNo"
                DTApprovedComponentInfo.Columns.Add(oColFTSubOrderNoComponent.ColumnName, oColFTSubOrderNoComponent.DataType)

                Dim oColFTStateApprovedComponent As System.Data.DataColumn
                oColFTStateApprovedComponent = New System.Data.DataColumn("FTStateApprovedComponent", System.Type.GetType("System.String"))
                'oColFTStateApprovedComponent.Caption = "FTStateApprovedComponent"
                DTApprovedComponentInfo.Columns.Add(oColFTStateApprovedComponent.ColumnName, oColFTStateApprovedComponent.DataType)

                Dim oColFNCntApprovedComponent As System.Data.DataColumn
                oColFNCntApprovedComponent = New System.Data.DataColumn("FNCntApprovedComponent", System.Type.GetType("System.Int32"))
                oColFNCntApprovedComponent.Caption = "FNCntApprovedComponent"
                DTApprovedComponentInfo.Columns.Add(oColFNCntApprovedComponent.ColumnName, oColFNCntApprovedComponent.DataType)

                Dim oColFDDateApprovedComponent As System.Data.DataColumn
                oColFDDateApprovedComponent = New System.Data.DataColumn("FDDateApprovedComponent", System.Type.GetType("System.String"))
                'oColFDDateApprovedComponent.Caption = "FDDateApprovedComponent"
                DTApprovedComponentInfo.Columns.Add(oColFDDateApprovedComponent.ColumnName, oColFDDateApprovedComponent.DataType)

                Dim oColFTTimeApprovedComponent As System.Data.DataColumn
                oColFTTimeApprovedComponent = New System.Data.DataColumn("FTTimeApprovedComponent", System.Type.GetType("System.String"))
                'oColFTTimeApprovedComponent.Caption = "FTTimeApprovedComponent"
                DTApprovedComponentInfo.Columns.Add(oColFTTimeApprovedComponent.ColumnName, oColFTTimeApprovedComponent.DataType)

                Dim oColFTUserApprovedComponent As System.Data.DataColumn
                oColFTUserApprovedComponent = New System.Data.DataColumn("FTUserApprovedComponent", System.Type.GetType("System.String"))
                'oColFTUserApprovedComponent.Caption = "FTUserApprovedComponent"
                DTApprovedComponentInfo.Columns.Add(oColFTUserApprovedComponent.ColumnName, oColFTUserApprovedComponent.DataType)

                Dim oColFTStateApprovedComponentHide As System.Data.DataColumn
                oColFTStateApprovedComponentHide = New System.Data.DataColumn("FTStateApprovedComponentHide", System.Type.GetType("System.String"))
                'oColFTStateApprovedComponentHide.Caption = "FTStateApprovedComponentHide"
                DTApprovedComponentInfo.Columns.Add(oColFTStateApprovedComponentHide.ColumnName, oColFTStateApprovedComponentHide.DataType)

                For Each oDataRow2 As System.Data.DataRow In tmpDTComponentApproved.Rows
                    DTApprovedComponentInfo.ImportRow(oDataRow2)
                Next

                If Not DTApprovedComponentInfo Is Nothing AndAlso DTApprovedComponentInfo.Rows.Count > 0 Then DTApprovedComponentInfo.DefaultView.Sort = "FTStateApprovedComponent ASC, FTStyleCode ASC, FTOrderNo ASC, FTSubOrderNo ASC"

                If Not tmpDTComponentApproved Is Nothing Then tmpDTComponentApproved.Dispose()

                Me.ogdApprovedComponentInfo.DataSource = DTApprovedComponentInfo
                Me.ogdApprovedComponentInfo.Refresh()

                If bFlagLoad = True Then
                    Me.ogvApprovedComponentInfo.ClearColumnsFilter()
                    Me.ogvApprovedComponentInfo.ActiveFilter.Clear()
                End If

            End If

            If bChkApprovedTabSewing = True Then
                Me.ogdApprovedSewingInfo.DataSource = Nothing : Me.ogdApprovedSewingInfo.Refresh()

                Dim tmpDTSewingApproved As System.Data.DataTable

                tmpDTSewingApproved = DTApprovedSubOrder.Copy()

                If Not tmpDTSewingApproved Is Nothing Then
                    tmpDTSewingApproved.BeginInit()
                    For numLoopSew As Integer = tmpDTSewingApproved.Columns.Count - 1 To 0 Step -1
                        Select Case tmpDTSewingApproved.Columns(numLoopSew).ColumnName.ToUpper
                            Case "FTStateSelect".ToUpper, "FTStyleCode".ToUpper, "FTSeasonCode".ToUpper, "FTStyleName".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTStateApprovedSewing".ToUpper, "FNCntApprovedSewing".ToUpper, "FDDateApprovedSewing".ToUpper, "FTTimeApprovedSewing".ToUpper, "FTUserApprovedSewing".ToUpper, "FTStateApprovedSewingHide".ToUpper
                                '...Nothing
                            Case Else
                                tmpDTSewingApproved.Columns.Remove(tmpDTSewingApproved.Columns(numLoopSew))
                        End Select

                    Next
                    tmpDTSewingApproved.EndInit()
                    tmpDTSewingApproved.AcceptChanges()
                End If

                'DTApprovedSewingInfo
                DTApprovedSewingInfo = New System.Data.DataTable
                'Dim oColFTStateSelectSew As System.Data.DataColumn
                'oColFTStateSelectSew = New System.Data.DataColumn("FTStateSelect", System.Type.GetType("System.String"))
                ''oColFTStyleCodeComponent.Caption = "FTStyleCode"
                'DTApprovedComponentInfo.Columns.Add(oColFTStateSelectSew.ColumnName, oColFTStateSelectSew.DataType)

                Dim oColFTStyleCodeSew As System.Data.DataColumn
                oColFTStyleCodeSew = New System.Data.DataColumn("FTStyleCode", System.Type.GetType("System.String"))
                'oColFTStyleCodeSew.Caption = "FTStyleCode"
                DTApprovedSewingInfo.Columns.Add(oColFTStyleCodeSew.ColumnName, oColFTStyleCodeSew.DataType)

                Dim oColFTStyleNameSew As System.Data.DataColumn
                oColFTStyleNameSew = New System.Data.DataColumn("FTStyleName", System.Type.GetType("System.String"))
                'oColFTStyleNameSew.Caption = "FTStyleName"
                DTApprovedSewingInfo.Columns.Add(oColFTStyleNameSew.ColumnName, oColFTStyleNameSew.DataType)

                Dim oColFTStyleNameSewSeason As System.Data.DataColumn
                oColFTStyleNameSewSeason = New System.Data.DataColumn("FTSeasonCode", System.Type.GetType("System.String"))
                'oColFTStyleNameSew.Caption = "FTStyleName"
                DTApprovedSewingInfo.Columns.Add(oColFTStyleNameSewSeason.ColumnName, oColFTStyleNameSewSeason.DataType)

                Dim oColFTOrderNoSew As System.Data.DataColumn
                oColFTOrderNoSew = New System.Data.DataColumn("FTOrderNo", System.Type.GetType("System.String"))
                'oColFTOrderNoSew.Caption = "FTOrderNo"
                DTApprovedSewingInfo.Columns.Add(oColFTOrderNoSew.ColumnName, oColFTOrderNoSew.DataType)

                Dim oColFTSubOrderNoSew As System.Data.DataColumn
                oColFTSubOrderNoSew = New System.Data.DataColumn("FTSubOrderNo", System.Type.GetType("System.String"))
                'oColFTSubOrderNoSew.Caption = "FTSubOrderNo"
                DTApprovedSewingInfo.Columns.Add(oColFTSubOrderNoSew.ColumnName, oColFTSubOrderNoSew.DataType)

                Dim oColFTStateApprovedSewing As System.Data.DataColumn
                oColFTStateApprovedSewing = New System.Data.DataColumn("FTStateApprovedSewing", System.Type.GetType("System.String"))
                'oColFTStateApprovedSewing.Caption = "FTStateApprovedSewing"
                DTApprovedSewingInfo.Columns.Add(oColFTStateApprovedSewing.ColumnName, oColFTStateApprovedSewing.DataType)

                Dim oColFNCntApprovedSewing As System.Data.DataColumn
                oColFNCntApprovedSewing = New System.Data.DataColumn("FNCntApprovedSewing", System.Type.GetType("System.Int32"))
                'oColFNCntApprovedSewing.Caption = "FNCntApprovedSewing"
                DTApprovedSewingInfo.Columns.Add(oColFNCntApprovedSewing.ColumnName, oColFNCntApprovedSewing.DataType)

                Dim oColFDDateApprovedSewing As System.Data.DataColumn
                oColFDDateApprovedSewing = New System.Data.DataColumn("FDDateApprovedSewing", System.Type.GetType("System.String"))
                'oColFDDateApprovedSewing.Caption = "FDDateApprovedSewing"
                DTApprovedSewingInfo.Columns.Add(oColFDDateApprovedSewing.ColumnName, oColFDDateApprovedSewing.DataType)

                Dim oColFTTimeApprovedSewing As System.Data.DataColumn
                oColFTTimeApprovedSewing = New System.Data.DataColumn("FTTimeApprovedSewing", System.Type.GetType("System.String"))
                oColFTTimeApprovedSewing.Caption = "FTTimeApprovedSewing"
                DTApprovedSewingInfo.Columns.Add(oColFTTimeApprovedSewing.ColumnName, oColFTTimeApprovedSewing.DataType)

                Dim oColFTUserApprovedSewing As System.Data.DataColumn
                oColFTUserApprovedSewing = New System.Data.DataColumn("FTUserApprovedSewing", System.Type.GetType("System.String"))
                'oColFTUserApprovedSewing.Caption = "FTUserApprovedSewing"
                DTApprovedSewingInfo.Columns.Add(oColFTUserApprovedSewing.ColumnName, oColFTUserApprovedSewing.DataType)

                Dim oColFTStateApprovedSewingHide As System.Data.DataColumn
                oColFTStateApprovedSewingHide = New System.Data.DataColumn("FTStateApprovedSewingHide", System.Type.GetType("System.String"))
                'oColFTStateApprovedComponentHide.Caption = "FTStateApprovedSewingHide"
                DTApprovedSewingInfo.Columns.Add(oColFTStateApprovedSewingHide.ColumnName, oColFTStateApprovedSewingHide.DataType)

                For Each oDataRow3 As System.Data.DataRow In tmpDTSewingApproved.Rows
                    DTApprovedSewingInfo.ImportRow(oDataRow3)
                Next

                If Not DTApprovedSewingInfo Is Nothing AndAlso DTApprovedSewingInfo.Rows.Count > 0 Then DTApprovedSewingInfo.DefaultView.Sort = "FTStateApprovedSewing ASC, FTStyleCode ASC, FTOrderNo ASC, FTSubOrderNo ASC"

                If Not tmpDTSewingApproved Is Nothing Then tmpDTSewingApproved.Dispose()

                Me.ogdApprovedSewingInfo.DataSource = DTApprovedSewingInfo
                Me.ogdApprovedSewingInfo.Refresh()

                If bFlagLoad = True Then
                    Me.ogvApprovedSewingInfo.ClearColumnsFilter()
                    Me.ogvApprovedSewingInfo.ActiveFilter.Clear()
                End If

            End If

            If bChkApprovedTabPacking = True Then
                Me.ogdApprovedPackingInfo.DataSource = Nothing : Me.ogdApprovedPackingInfo.Refresh()

                Dim tmpDTPackingApproved As System.Data.DataTable

                tmpDTPackingApproved = DTApprovedSubOrder.Copy()

                If Not tmpDTPackingApproved Is Nothing Then
                    tmpDTPackingApproved.BeginInit()
                    For numLoopPack As Integer = tmpDTPackingApproved.Columns.Count - 1 To 0 Step -1
                        Select Case tmpDTPackingApproved.Columns(numLoopPack).ColumnName.ToUpper
                            Case "FTStateSelect".ToUpper, "FTStyleCode".ToUpper, "FTSeasonCode".ToUpper, "FTStyleName".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTStateApprovedPacking".ToUpper, "FNCntApprovedPacking".ToUpper, "FDDateApprovedPacking".ToUpper, "FTTimeApprovedPacking".ToUpper, "FTUserApprovedPacking".ToUpper, "FTStateApprovedPackingHide".ToUpper
                                '...Nothing
                            Case Else
                                tmpDTPackingApproved.Columns.Remove(tmpDTPackingApproved.Columns(numLoopPack))
                        End Select

                    Next
                    tmpDTPackingApproved.EndInit()
                    tmpDTPackingApproved.AcceptChanges()
                End If

                'DTApprovedPackingInfo
                DTApprovedPackingInfo = New System.Data.DataTable

                'Dim oColFTStateSelectPack As System.Data.DataColumn
                'oColFTStateSelectPack = New System.Data.DataColumn("FTStateSelect", System.Type.GetType("System.String"))
                ''oColFTStyleCodePack.Caption = "FTStyleCode"
                'DTApprovedPackingInfo.Columns.Add(oColFTStateSelectPack.ColumnName, oColFTStateSelectPack.DataType)

                Dim oColFTStyleCodePack As System.Data.DataColumn
                oColFTStyleCodePack = New System.Data.DataColumn("FTStyleCode", System.Type.GetType("System.String"))
                'oColFTStyleCodePack.Caption = "FTStyleCode"
                DTApprovedPackingInfo.Columns.Add(oColFTStyleCodePack.ColumnName, oColFTStyleCodePack.DataType)

                Dim oColFTStyleNamePack As System.Data.DataColumn
                oColFTStyleNamePack = New System.Data.DataColumn("FTStyleName", System.Type.GetType("System.String"))
                'oColFTStyleNamePack.Caption = "FTStyleName"
                DTApprovedPackingInfo.Columns.Add(oColFTStyleNamePack.ColumnName, oColFTStyleNamePack.DataType)

                Dim oColFTStyleNamePackSeason As System.Data.DataColumn
                oColFTStyleNamePackSeason = New System.Data.DataColumn("FTSeasonCode", System.Type.GetType("System.String"))
                'oColFTStyleNamePack.Caption = "FTStyleName"
                DTApprovedPackingInfo.Columns.Add(oColFTStyleNamePackSeason.ColumnName, oColFTStyleNamePackSeason.DataType)

                Dim oColFTOrderNoPack As System.Data.DataColumn
                oColFTOrderNoPack = New System.Data.DataColumn("FTOrderNo", System.Type.GetType("System.String"))
                'oColFTOrderNoPack.Caption = "FTOrderNo"
                DTApprovedPackingInfo.Columns.Add(oColFTOrderNoPack.ColumnName, oColFTOrderNoPack.DataType)

                Dim oColFTSubOrderNoPack As System.Data.DataColumn
                oColFTSubOrderNoPack = New System.Data.DataColumn("FTSubOrderNo", System.Type.GetType("System.String"))
                'oColFTSubOrderNoPack.Caption = "FTSubOrderNo"
                DTApprovedPackingInfo.Columns.Add(oColFTSubOrderNoPack.ColumnName, oColFTSubOrderNoPack.DataType)

                Dim oColFTStateApprovedPacking As System.Data.DataColumn
                oColFTStateApprovedPacking = New System.Data.DataColumn("FTStateApprovedPacking", System.Type.GetType("System.String"))
                'oColFTStateApprovedPacking.Caption = "FTStateApprovedPacking"
                DTApprovedPackingInfo.Columns.Add(oColFTStateApprovedPacking.ColumnName, oColFTStateApprovedPacking.DataType)

                Dim oColFNCntApprovedPacking As System.Data.DataColumn
                oColFNCntApprovedPacking = New System.Data.DataColumn("FNCntApprovedPacking", System.Type.GetType("System.Int32"))
                'oColFNCntApprovedPacking.Caption = "FNCntApprovedPacking"
                DTApprovedPackingInfo.Columns.Add(oColFNCntApprovedPacking.ColumnName, oColFNCntApprovedPacking.DataType)

                Dim oColFDDateApprovedPacking As System.Data.DataColumn
                oColFDDateApprovedPacking = New System.Data.DataColumn("FDDateApprovedPacking", System.Type.GetType("System.String"))
                'oColFDDateApprovedPacking.Caption = "FDDateApprovedPacking"
                DTApprovedPackingInfo.Columns.Add(oColFDDateApprovedPacking.ColumnName, oColFDDateApprovedPacking.DataType)

                Dim oColFTTimeApprovedPacking As System.Data.DataColumn
                oColFTTimeApprovedPacking = New System.Data.DataColumn("FTTimeApprovedPacking", System.Type.GetType("System.String"))
                'oColFTTimeApprovedPacking.Caption = "FTTimeApprovedPacking"
                DTApprovedPackingInfo.Columns.Add(oColFTTimeApprovedPacking.ColumnName, oColFTTimeApprovedPacking.DataType)

                Dim oColFTUserApprovedPacking As System.Data.DataColumn
                oColFTUserApprovedPacking = New System.Data.DataColumn("FTUserApprovedPacking", System.Type.GetType("System.String"))
                'oColFTUserApprovedPacking.Caption = "FTUserApprovedPacking"
                DTApprovedPackingInfo.Columns.Add(oColFTUserApprovedPacking.ColumnName, oColFTUserApprovedPacking.DataType)

                Dim oColFTStateApprovedPackingHide As System.Data.DataColumn
                oColFTStateApprovedPackingHide = New System.Data.DataColumn("FTStateApprovedPackingHide", System.Type.GetType("System.String"))
                'oColFTStateApprovedPackingHide.Caption = "FTStateApprovedPackingHide"
                DTApprovedPackingInfo.Columns.Add(oColFTStateApprovedPackingHide.ColumnName, oColFTStateApprovedPackingHide.DataType)

                For Each oDataRow4 As System.Data.DataRow In tmpDTPackingApproved.Rows
                    DTApprovedPackingInfo.ImportRow(oDataRow4)
                Next

                If Not DTApprovedPackingInfo Is Nothing AndAlso DTApprovedPackingInfo.Rows.Count > 0 Then DTApprovedPackingInfo.DefaultView.Sort = "FTStateApprovedPacking ASC, FTStyleCode ASC, FTOrderNo ASC, FTSubOrderNo ASC"

                If Not tmpDTPackingApproved Is Nothing Then tmpDTPackingApproved.Dispose()

                Me.ogdApprovedPackingInfo.DataSource = DTApprovedPackingInfo
                Me.ogdApprovedPackingInfo.Refresh()

                If bFlagLoad = True Then
                    Me.ogvApprovedPackingInfo.ClearColumnsFilter()
                    Me.ogvApprovedPackingInfo.ActiveFilter.Clear()
                End If

            End If

            If bChkApprovedTabPackCarton = True Then
                Me.ogdApprovedPackRatioInfo.DataSource = Nothing : Me.ogdApprovedPackRatioInfo.Refresh()

                Dim tmpDTPackRatioApproved As System.Data.DataTable

                tmpDTPackRatioApproved = DTApprovedSubOrder.Copy()

                If Not tmpDTPackRatioApproved Is Nothing Then
                    tmpDTPackRatioApproved.BeginInit()
                    For numLoopPackRatio As Integer = tmpDTPackRatioApproved.Columns.Count - 1 To 0 Step -1
                        Select Case tmpDTPackRatioApproved.Columns(numLoopPackRatio).ColumnName.ToUpper
                            Case "FTStateSelect".ToUpper, "FTStyleCode".ToUpper, "FTSeasonCode".ToUpper, "FTStyleName".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTStateApprovedPackRatio".ToUpper, "FNCntApprovedPackRatio".ToUpper, "FDDateApprovedPackRatio".ToUpper, "FTTimeApprovedPackRatio".ToUpper, "FTUserApprovedPackRatio".ToUpper, "FTStateApprovedPackRatioHide".ToUpper
                                '...Nothing
                            Case Else
                                tmpDTPackRatioApproved.Columns.Remove(tmpDTPackRatioApproved.Columns(numLoopPackRatio))
                        End Select

                    Next
                    tmpDTPackRatioApproved.EndInit()
                    tmpDTPackRatioApproved.AcceptChanges()
                End If

                'DTApprovedPackRatio
                DTApprovedPackRatio = New System.Data.DataTable

                'Dim oColFTStateSelectRatio As System.Data.DataColumn
                'oColFTStateSelectRatio = New System.Data.DataColumn("FTStateSelect", System.Type.GetType("System.String"))
                ''oColFTStyleCodePackRatio.Caption = "FTStyleCode"
                'DTApprovedPackRatio.Columns.Add(oColFTStateSelectRatio.ColumnName, oColFTStateSelectRatio.DataType)

                Dim oColFTStyleCodePackRatio As System.Data.DataColumn
                oColFTStyleCodePackRatio = New System.Data.DataColumn("FTStyleCode", System.Type.GetType("System.String"))
                'oColFTStyleCodePackRatio.Caption = "FTStyleCode"
                DTApprovedPackRatio.Columns.Add(oColFTStyleCodePackRatio.ColumnName, oColFTStyleCodePackRatio.DataType)

                Dim oColFTStyleNamePackRatio As System.Data.DataColumn
                oColFTStyleNamePackRatio = New System.Data.DataColumn("FTStyleName", System.Type.GetType("System.String"))
                oColFTStyleNamePackRatio.Caption = "FTStyleName"
                DTApprovedPackRatio.Columns.Add(oColFTStyleNamePackRatio.ColumnName, oColFTStyleNamePackRatio.DataType)

                Dim oColFTStyleNamePackRatioSeason As System.Data.DataColumn
                oColFTStyleNamePackRatioSeason = New System.Data.DataColumn("FTSeasonCode", System.Type.GetType("System.String"))
                oColFTStyleNamePackRatioSeason.Caption = "Season"
                DTApprovedPackRatio.Columns.Add(oColFTStyleNamePackRatioSeason.ColumnName, oColFTStyleNamePackRatioSeason.DataType)

                Dim oColFTOrderNoPackRatio As System.Data.DataColumn
                oColFTOrderNoPackRatio = New System.Data.DataColumn("FTOrderNo", System.Type.GetType("System.String"))
                'oColFTOrderNoPackRatio.Caption = "FTOrderNo"
                DTApprovedPackRatio.Columns.Add(oColFTOrderNoPackRatio.ColumnName, oColFTOrderNoPackRatio.DataType)

                Dim oColFTSubOrderNoPackRatio As System.Data.DataColumn
                oColFTSubOrderNoPackRatio = New System.Data.DataColumn("FTSubOrderNo", System.Type.GetType("System.String"))
                'oColFTSubOrderNoPackRatio.Caption = "FTSubOrderNo"
                DTApprovedPackRatio.Columns.Add(oColFTSubOrderNoPackRatio.ColumnName, oColFTSubOrderNoPackRatio.DataType)

                Dim oColFTStateApprovedPackRatio As System.Data.DataColumn
                oColFTStateApprovedPackRatio = New System.Data.DataColumn("FTStateApprovedPackRatio", System.Type.GetType("System.String"))
                'oColFTStateApprovedPackRatio.Caption = "FTStateApprovedPackRatio"
                DTApprovedPackRatio.Columns.Add(oColFTStateApprovedPackRatio.ColumnName, oColFTStateApprovedPackRatio.DataType)

                Dim oColFNCntApprovedPackRatio As System.Data.DataColumn
                oColFNCntApprovedPackRatio = New System.Data.DataColumn("FNCntApprovedPackRatio", System.Type.GetType("System.Int32"))
                'oColFNCntApprovedPackRatio.Caption = "FNCntApprovedPackRatio"
                DTApprovedPackRatio.Columns.Add(oColFNCntApprovedPackRatio.ColumnName, oColFNCntApprovedPackRatio.DataType)

                Dim oColFDDateApprovedPackRatio As System.Data.DataColumn
                oColFDDateApprovedPackRatio = New System.Data.DataColumn("FDDateApprovedPackRatio", System.Type.GetType("System.String"))
                'oColFDDateApprovedPackRatio.Caption = "FDDateApprovedPackRatio"
                DTApprovedPackRatio.Columns.Add(oColFDDateApprovedPackRatio.ColumnName, oColFDDateApprovedPackRatio.DataType)

                Dim oColFTTimeApprovedPackRatio As System.Data.DataColumn
                oColFTTimeApprovedPackRatio = New System.Data.DataColumn("FTTimeApprovedPackRatio", System.Type.GetType("System.String"))
                'oColFTTimeApprovedPackRatio.Caption = "FTTimeApprovedPackRatio"
                DTApprovedPackRatio.Columns.Add(oColFTTimeApprovedPackRatio.ColumnName, oColFTTimeApprovedPackRatio.DataType)

                Dim oColFTUserApprovedPackRatio As System.Data.DataColumn
                oColFTUserApprovedPackRatio = New System.Data.DataColumn("FTUserApprovedPackRatio", System.Type.GetType("System.String"))
                'oColFTUserApprovedPackRatio.Caption = "FTUserApprovedPackRatio"
                DTApprovedPackRatio.Columns.Add(oColFTUserApprovedPackRatio.ColumnName, oColFTUserApprovedPackRatio.DataType)

                Dim oColFTStateApprovedPackRatioHide As System.Data.DataColumn
                oColFTStateApprovedPackRatioHide = New System.Data.DataColumn("FTStateApprovedPackRatioHide", System.Type.GetType("System.String"))
                'oColFTStateApprovedPackingHide.Caption = "FTStateApprovedPackRatioHide"
                DTApprovedPackRatio.Columns.Add(oColFTStateApprovedPackRatioHide.ColumnName, oColFTStateApprovedPackRatioHide.DataType)

                For Each oDataRow5 As System.Data.DataRow In tmpDTPackRatioApproved.Rows
                    DTApprovedPackRatio.ImportRow(oDataRow5)
                Next

                If Not DTApprovedPackRatio Is Nothing AndAlso DTApprovedPackRatio.Rows.Count > 0 Then DTApprovedPackRatio.DefaultView.Sort = "FTStateApprovedPackRatio ASC, FTStyleCode ASC, FTOrderNo ASC, FTOrderNo ASC"

                If Not tmpDTPackRatioApproved Is Nothing Then tmpDTPackRatioApproved.Dispose()

                Me.ogdApprovedPackRatioInfo.DataSource = DTApprovedPackRatio
                Me.ogdApprovedPackRatioInfo.Refresh()

                If bFlagLoad = True Then
                    Me.ogvApprovedPackRatioInfo.ClearColumnsFilter()
                    Me.ogvApprovedPackRatioInfo.ActiveFilter.Clear()
                End If

            End If

            If bChkApprovedTabSizeSpec = True Then
                Me.ogdApprovedSizeSpecInfo.DataSource = Nothing : Me.ogdApprovedSizeSpecInfo.Refresh()

                Dim tmpDTSizeSpecApproved As System.Data.DataTable

                tmpDTSizeSpecApproved = DTApprovedSubOrder.Copy()

                If Not tmpDTSizeSpecApproved Is Nothing Then
                    tmpDTSizeSpecApproved.BeginInit()
                    For numLoopSizeSpec As Integer = tmpDTSizeSpecApproved.Columns.Count - 1 To 0 Step -1
                        Select Case tmpDTSizeSpecApproved.Columns(numLoopSizeSpec).ColumnName.ToUpper
                            Case "FTStateSelect".ToUpper, "FTStyleCode".ToUpper, "FTSeasonCode".ToUpper, "FTStyleName".ToUpper, "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTStateApprovedSizeSpec".ToUpper, "FNCntApprovedSizeSpec".ToUpper, "FDDateApprovedSizeSpec".ToUpper, "FTTimeApprovedSizeSpec".ToUpper, "FTUserApprovedSizeSpec".ToUpper, "FTStateApprovedSizeSpecHide".ToUpper
                                '...Nothing
                            Case Else
                                tmpDTSizeSpecApproved.Columns.Remove(tmpDTSizeSpecApproved.Columns(numLoopSizeSpec))
                        End Select

                    Next
                    tmpDTSizeSpecApproved.EndInit()
                    tmpDTSizeSpecApproved.AcceptChanges()
                End If

                'DTApprovedSizeSpecInfo
                DTApprovedSizeSpecInfo = New System.Data.DataTable

                'Dim oColFTStateSelectSizeSpec As System.Data.DataColumn
                'oColFTStateSelectSizeSpec = New System.Data.DataColumn("FTStateSelect", System.Type.GetType("System.String"))
                'oColFTStateSelectSizeSpec.Caption = "FTStateSelect"
                'DTApprovedSizeSpecInfo.Columns.Add(oColFTStateSelectSizeSpec.ColumnName, oColFTStateSelectSizeSpec.DataType)

                Dim oColFTStyleCodeSizeSpec As System.Data.DataColumn
                oColFTStyleCodeSizeSpec = New System.Data.DataColumn("FTStyleCode", System.Type.GetType("System.String"))
                oColFTStyleCodeSizeSpec.Caption = "FTStyleCode"
                DTApprovedSizeSpecInfo.Columns.Add(oColFTStyleCodeSizeSpec.ColumnName, oColFTStyleCodeSizeSpec.DataType)

                Dim oColFTStyleNameSizeSpec As System.Data.DataColumn
                oColFTStyleNameSizeSpec = New System.Data.DataColumn("FTStyleName", System.Type.GetType("System.String"))
                oColFTStyleCodeSizeSpec.Caption = "FTStyleName"
                DTApprovedSizeSpecInfo.Columns.Add(oColFTStyleNameSizeSpec.ColumnName, oColFTStyleNameSizeSpec.DataType)

                Dim oColFTStyleNameSizeSpecSeason As System.Data.DataColumn
                oColFTStyleNameSizeSpecSeason = New System.Data.DataColumn("FTSeasonCode", System.Type.GetType("System.String"))
                oColFTStyleNameSizeSpecSeason.Caption = "Season"
                DTApprovedSizeSpecInfo.Columns.Add(oColFTStyleNameSizeSpecSeason.ColumnName, oColFTStyleNameSizeSpecSeason.DataType)

                Dim oColFTOrderNoSizeSpec As System.Data.DataColumn
                oColFTOrderNoSizeSpec = New System.Data.DataColumn("FTOrderNo", System.Type.GetType("System.String"))
                'oColFTOrderNoSizeSpec.Caption = "FTOrderNo"
                DTApprovedSizeSpecInfo.Columns.Add(oColFTOrderNoSizeSpec.ColumnName, oColFTOrderNoSizeSpec.DataType)

                Dim oColFTSubOrderNoSizeSpec As System.Data.DataColumn
                oColFTSubOrderNoSizeSpec = New System.Data.DataColumn("FTSubOrderNo", System.Type.GetType("System.String"))
                'oColFTSubOrderNoSizeSpec.Caption = "FTSubOrderNo"
                DTApprovedSizeSpecInfo.Columns.Add(oColFTSubOrderNoSizeSpec.ColumnName, oColFTSubOrderNoSizeSpec.DataType)

                Dim oColFTStateApprovedSizeSpec As System.Data.DataColumn
                oColFTStateApprovedSizeSpec = New System.Data.DataColumn("FTStateApprovedSizeSpec", System.Type.GetType("System.String"))
                'oColFTStateApprovedSizeSpec.Caption = "FTStateApprovedSizeSpec"
                DTApprovedSizeSpecInfo.Columns.Add(oColFTStateApprovedSizeSpec.ColumnName, oColFTStateApprovedSizeSpec.DataType)

                Dim oColFNCntApprovedSizeSpec As System.Data.DataColumn
                oColFNCntApprovedSizeSpec = New System.Data.DataColumn("FNCntApprovedSizeSpec", System.Type.GetType("System.Int32"))
                'oColFNCntApprovedSizeSpec.Caption = "FNCntApprovedSizeSpec"
                DTApprovedSizeSpecInfo.Columns.Add(oColFNCntApprovedSizeSpec.ColumnName, oColFNCntApprovedSizeSpec.DataType)

                Dim oColFDDateApprovedSizeSpec As System.Data.DataColumn
                oColFDDateApprovedSizeSpec = New System.Data.DataColumn("FDDateApprovedSizeSpec", System.Type.GetType("System.String"))
                'oColFDDateApprovedSizeSpec.Caption = "FDDateApprovedSizeSpec"
                DTApprovedSizeSpecInfo.Columns.Add(oColFDDateApprovedSizeSpec.ColumnName, oColFDDateApprovedSizeSpec.DataType)

                Dim oColFTTimeApprovedSizeSpec As System.Data.DataColumn
                oColFTTimeApprovedSizeSpec = New System.Data.DataColumn("FTTimeApprovedSizeSpec", System.Type.GetType("System.String"))
                oColFTTimeApprovedSizeSpec.Caption = "FTTimeApprovedSizeSpec"
                DTApprovedSizeSpecInfo.Columns.Add(oColFTTimeApprovedSizeSpec.ColumnName, oColFTTimeApprovedSizeSpec.DataType)

                Dim oColFTUserApprovedSizeSpec As System.Data.DataColumn
                oColFTUserApprovedSizeSpec = New System.Data.DataColumn("FTUserApprovedSizeSpec", System.Type.GetType("System.String"))
                'oColFTUserApprovedSizeSpec.Caption = "FTUserApprovedSizeSpec"
                DTApprovedSizeSpecInfo.Columns.Add(oColFTUserApprovedSizeSpec.ColumnName, oColFTUserApprovedSizeSpec.DataType)

                Dim oColFTStateApprovedSizeSpecHide As System.Data.DataColumn
                oColFTStateApprovedSizeSpecHide = New System.Data.DataColumn("FTStateApprovedSizeSpecHide", System.Type.GetType("System.String"))
                'oColFTStateApprovedSizeSpecHide.Caption = "FTStateApprovedSizeSpecHide"
                DTApprovedSizeSpecInfo.Columns.Add(oColFTStateApprovedSizeSpecHide.ColumnName, oColFTStateApprovedSizeSpecHide.DataType)

                For Each oDataRow6 As System.Data.DataRow In tmpDTSizeSpecApproved.Rows
                    DTApprovedSizeSpecInfo.ImportRow(oDataRow6)
                Next

                If Not DTApprovedSizeSpecInfo Is Nothing AndAlso DTApprovedSizeSpecInfo.Rows.Count > 0 Then DTApprovedSizeSpecInfo.DefaultView.Sort = "FTStateApprovedSizeSpec ASC, FTStyleCode ASC, FTOrderNo ASC, FTSubOrderNo ASC"

                If Not tmpDTSizeSpecApproved Is Nothing Then tmpDTSizeSpecApproved.Dispose()

                Me.ogdApprovedSizeSpecInfo.DataSource = DTApprovedSizeSpecInfo
                Me.ogdApprovedSizeSpecInfo.Refresh()

                If bFlagLoad = True Then
                    Me.ogvApprovedSizeSpecInfo.ClearColumnsFilter()
                    Me.ogvApprovedSewingInfo.ActiveFilter.Clear()
                End If

            End If

            bRetLoadComplete = True

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
            End If
        End Try

        Return bRetLoadComplete

    End Function

    Private Function PROC_SAVEbApprovedOrder() As Boolean

        If Me.ogvApprovedOrderInfo.RowCount < 1 AndAlso
           Me.ogvApprovedSubOrderNoInfo.RowCount < 1 AndAlso
           Me.ogvApprovedComponentInfo.RowCount < 1 AndAlso
           Me.ogvApprovedSewingInfo.RowCount < 1 AndAlso
           Me.ogvApprovedPackingInfo.RowCount < 1 AndAlso
           Me.ogvApprovedPackRatioInfo.RowCount < 1 AndAlso
           Me.ogvApprovedSizeSpecInfo.RowCount < 1 Then

            Return False

        End If

        Dim bSaveApprovedInfo As Boolean = False
        Dim _Spls As HI.TL.SplashScreen

        Try
            If HI.MG.ShowMsg.mConfirmProcess("ท่านต้องการอนุมัติรายการสารสนเทศสำหรับรายการใบสั่งผลิตใช่หรือไม่", 1502140001, "Confirm") = True Then

                Dim numCountApprovedOrderNo As Integer, numCountCancelApprovedOrderNo As Integer
                Dim numCountApprovedSubOrderNo As Integer, numCountCancelApprovedSubOrderNo As Integer
                Dim numCountApprovedComponent As Integer, numCountCancelApprovedComponent As Integer
                Dim numCountApprovedSew As Integer, numCountCancelApprovedSew As Integer
                Dim numCountApprovedPack As Integer, numCountCancelApprovedPack As Integer
                Dim numCountApprovedPackRatio As Integer, numCountCancelApprovedPackRatio As Integer
                Dim numCountApprovedSizeSpec As Integer, numCountCancelApprovedSizeSpec As Integer

                numCountApprovedOrderNo = 0 : numCountCancelApprovedOrderNo = 0
                numCountApprovedSubOrderNo = 0 : numCountCancelApprovedSubOrderNo = 0
                numCountApprovedComponent = 0 : numCountCancelApprovedComponent = 0
                numCountApprovedSew = 0 : numCountCancelApprovedSew = 0
                numCountApprovedPack = 0 : numCountCancelApprovedPack = 0
                numCountApprovedPackRatio = 0 : numCountCancelApprovedPackRatio = 0
                numCountApprovedSizeSpec = 0 : numCountCancelApprovedSizeSpec = 0

                If Me.oChkApproveAllInfo.Checked = True Then
                    bFlagSaveAllInfo = True
                End If

                If Not System.Diagnostics.Debugger.IsAttached = True Then
                    _Spls = New HI.TL.SplashScreen("Save data  Please Wait...")
                End If

                Dim oStrBuilder As New System.Text.StringBuilder

                oStrBuilder.Remove(0, oStrBuilder.Length)

                '...iterate loop generate string for manipulate update data approved information

                Dim bChkApprovedTabOrderInfo As Boolean = False
                Dim bChkApprovedTabSubOrderNo As Boolean = False
                Dim bChkApprovedTabComponent As Boolean = False
                Dim bChkApprovedTabSewing As Boolean = False
                Dim bChkApprovedTabPacking As Boolean = False
                Dim bChkApprovedTabPackCarton As Boolean = False
                Dim bChkApprovedTabSizeSpec As Boolean = False

                Dim tChkApprovedAllInfo As String = "0"
                '...validate check approved information
                If Me.oChkApproveAllInfo.Checked = True Then
                    tChkApprovedAllInfo = "1"
                End If

                REM 2015/03/10
                '====================================================================================================
                'If tChkApprovedAllInfo = "0" Then
                '    Select Case Me.oTabApprovedInfo.SelectedTabPageIndex
                '        Case eApprovedTabIndex.ApprovedOrderNo : bChkApprovedTabOrderInfo = True
                '        Case eApprovedTabIndex.ApprovedSubOrderNo : bChkApprovedTabSubOrderNo = True
                '        Case eApprovedTabIndex.ApprovedComponent : bChkApprovedTabComponent = True
                '        Case eApprovedTabIndex.ApprovedSewing : bChkApprovedTabSewing = True
                '        Case eApprovedTabIndex.ApprovedPacking : bChkApprovedTabPacking = True
                '        Case eApprovedTabIndex.ApprovedPackingCarton : bChkApprovedTabPackCarton = True
                '        Case eApprovedTabIndex.ApprovedSizeSpec : bChkApprovedTabSizeSpec = True
                '    End Select
                'Else
                '    REM 2015/03/10 bChkApprovedTabOrderInfo = True : bChkApprovedTabSubOrderNo = True : bChkApprovedTabComponent = True : bChkApprovedTabSewing = True : bChkApprovedTabPacking = True : bChkApprovedTabPackCarton = True : bChkApprovedTabSizeSpec = True
                '    bChkApprovedTabOrderInfo = True : bChkApprovedTabSubOrderNo = True : bChkApprovedTabComponent = True : bChkApprovedTabSewing = True : bChkApprovedTabPacking = True : bChkApprovedTabPackCarton = True : bChkApprovedTabSizeSpec = True
                'End If
                '====================================================================================================

                Select Case True
                    Case (Me.oTabApprovedInfo.SelectedTabPageIndex = eApprovedTabIndex.ApprovedOrderNo) And (tChkApprovedAllInfo = "1")
                        bChkApprovedTabOrderInfo = True : bChkApprovedTabSubOrderNo = True : bChkApprovedTabComponent = True : bChkApprovedTabSewing = True : bChkApprovedTabPacking = True : bChkApprovedTabPackCarton = True : bChkApprovedTabSizeSpec = True
                    Case (Me.oTabApprovedInfo.SelectedTabPageIndex = eApprovedTabIndex.ApprovedSubOrderNo) And (tChkApprovedAllInfo = "1")
                        bChkApprovedTabSubOrderNo = True : bChkApprovedTabComponent = True : bChkApprovedTabSewing = True : bChkApprovedTabPacking = True : bChkApprovedTabPackCarton = True : bChkApprovedTabSizeSpec = True
                    Case Else
                        Select Case Me.oTabApprovedInfo.SelectedTabPageIndex
                            Case eApprovedTabIndex.ApprovedOrderNo : bChkApprovedTabOrderInfo = True
                            Case eApprovedTabIndex.ApprovedSubOrderNo : bChkApprovedTabSubOrderNo = True
                            Case eApprovedTabIndex.ApprovedComponent : bChkApprovedTabComponent = True
                            Case eApprovedTabIndex.ApprovedSewing : bChkApprovedTabSewing = True
                            Case eApprovedTabIndex.ApprovedPacking : bChkApprovedTabPacking = True
                            Case eApprovedTabIndex.ApprovedPackingCarton : bChkApprovedTabPackCarton = True
                            Case eApprovedTabIndex.ApprovedSizeSpec : bChkApprovedTabSizeSpec = True
                            Case Else
                                '...Nothing 
                        End Select
                End Select

                If bChkApprovedTabOrderInfo = True Then
                    Dim nFNRowEffectApprovedOrderNo As Integer, nFNRowCancelApprovedOrderNo As Integer
                    nFNRowEffectApprovedOrderNo = 0 : nFNRowCancelApprovedOrderNo = 0

                    '...Apporved Order No. Info
                    For nLoopApprovedOrder As Integer = 0 To Me.ogvApprovedOrderInfo.DataRowCount - 1

                        Dim oDataRowOrderNo As DataRow = Me.ogvApprovedOrderInfo.GetDataRow(nLoopApprovedOrder)

                        Dim tTextFTOrderNo As String
                        Dim tTextFTApprovedInfoState As String, tTextFTApprovedInfoStateHide As String
                        Dim nNumFNApprovedInfoCnt As Integer

                        tTextFTOrderNo = oDataRowOrderNo.Item("FTOrderNo").ToString

                        tTextFTApprovedInfoState = oDataRowOrderNo.Item("FTApprovedInfoState").ToString
                        tTextFTApprovedInfoStateHide = oDataRowOrderNo.Item("FTApprovedInfoStateHide").ToString

                        If DBNull.Value.Equals(oDataRowOrderNo.Item("FNApprovedInfoCnt")) = True Then
                            nNumFNApprovedInfoCnt = 0
                        Else
                            nNumFNApprovedInfoCnt = Val(oDataRowOrderNo.Item("FNApprovedInfoCnt")) + 1
                        End If

                        If tTextFTApprovedInfoState = "1" AndAlso tTextFTApprovedInfoStateHide = "0" Then
                            '...approved factory order no. information
                            nFNRowEffectApprovedOrderNo = nFNRowEffectApprovedOrderNo + 1

                            oStrBuilder.AppendLine("IF NOT EXISTS(SELECT TOP 1 A.FTOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder_ApprovedInfo] AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "')")
                            oStrBuilder.AppendLine("   BEGIN")
                            oStrBuilder.AppendLine("      PRINT 'Add new record';")
                            oStrBuilder.AppendLine("	  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrder_ApprovedInfo] ([FTOrderNo], [FTApprovedInfoState], [FNApprovedInfoCnt], [FDApprovedInfoDate], [FTApprovedInfoTime], [FTApprovedInfoBy])")
                            oStrBuilder.AppendLine("       SELECT N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "', N'1', 1, @FTApprovedInfoDate, @FTApprovedInfoTime, N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';")
                            oStrBuilder.AppendLine("   END")
                            oStrBuilder.AppendLine("ELSE")
                            oStrBuilder.AppendLine("   BEGIN")
                            oStrBuilder.AppendLine("	  PRINT 'update exists record';")
                            oStrBuilder.AppendLine("	  UPDATE A")
                            oStrBuilder.AppendLine("	  SET  A.[FTApprovedInfoState] = N'1'")
                            oStrBuilder.AppendLine("		  ,A.[FNApprovedInfoCnt] = ISNULL(A.[FNApprovedInfoCnt], 0) + 1")
                            oStrBuilder.AppendLine("		  ,A.[FDApprovedInfoDate] = @FTApprovedInfoDate")
                            oStrBuilder.AppendLine("		  ,A.[FTApprovedInfoTime] = @FTApprovedInfoTime")
                            oStrBuilder.AppendLine(String.Format("		  ,A.[FTApprovedInfoBy] = N'{0}'", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)))
                            oStrBuilder.AppendLine("		   ,A.[FTRevisedInfoState] = N'0'")
                            oStrBuilder.AppendLine("	  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrder_ApprovedInfo] AS A")
                            oStrBuilder.AppendLine("	  WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "'")
                            oStrBuilder.AppendLine("   END;")

                        ElseIf tTextFTApprovedInfoState = "0" AndAlso tTextFTApprovedInfoStateHide = "1" Then
                            '...cancel approved factory order no. information

                            nFNRowCancelApprovedOrderNo = nFNRowCancelApprovedOrderNo + 1

                            oStrBuilder.AppendLine("SET @FNApprovedInfoCnt = 0;")
                            oStrBuilder.AppendLine("SELECT TOP 1 @FNApprovedInfoCnt = A.FNApprovedInfoCnt FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder_ApprovedInfo AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "';")
                            oStrBuilder.AppendLine("PRINT '@FNApprovedInfoCnt : ' + CONVERT(VARCHAR(30), @FNApprovedInfoCnt);")
                            oStrBuilder.AppendLine("IF (@FNApprovedInfoCnt IS NULL)")
                            oStrBuilder.AppendLine("   BEGIN")
                            oStrBuilder.AppendLine("      SET @FNApprovedInfoCnt = 0")
                            oStrBuilder.AppendLine("   END;")
                            oStrBuilder.AppendLine("IF (@FNApprovedInfoCnt > 0)")
                            oStrBuilder.AppendLine("BEGIN")
                            oStrBuilder.AppendLine("   IF (@FNApprovedInfoCnt = 1)")
                            oStrBuilder.AppendLine("       BEGIN")
                            oStrBuilder.AppendLine("          PRINT 'Clear Transaction : Approved Factory Order Information';")
                            oStrBuilder.AppendLine("          DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder_ApprovedInfo AS A WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "'")
                            oStrBuilder.AppendLine("       END;")
                            oStrBuilder.AppendLine("   IF (@FNApprovedInfoCnt > 1)")
                            oStrBuilder.AppendLine("      BEGIN")
                            oStrBuilder.AppendLine("         PRINT 'Decrement time''s Approved Factory Order Information and clear information other approved info !!!';")
                            oStrBuilder.AppendLine("         UPDATE A")
                            oStrBuilder.AppendLine("         SET A.FTApprovedInfoState = N'0',")
                            oStrBuilder.AppendLine("             A.FNApprovedInfoCnt = A.FNApprovedInfoCnt - 1,")
                            oStrBuilder.AppendLine("             A.FDApprovedInfoDate = NULL,")
                            oStrBuilder.AppendLine("             A.FTApprovedInfoTime = NULL")
                            oStrBuilder.AppendLine("         FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder_ApprovedInfo AS A")
                            oStrBuilder.AppendLine("         WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "'")
                            oStrBuilder.AppendLine("      END")
                            oStrBuilder.AppendLine("END;")

                        End If

                    Next nLoopApprovedOrder

                    If System.Diagnostics.Debugger.IsAttached = True Then
                        If nFNRowEffectApprovedOrderNo > 0 OrElse nFNRowCancelApprovedOrderNo > 0 Then
                            MsgBox(String.Format("Execute committ transaction approved factory order no. : {0} " & Environment.NewLine & "Execute committ transaction cancel approved facatory order no. : {1} ", {nFNRowEffectApprovedOrderNo, nFNRowCancelApprovedOrderNo}), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                        End If
                    End If

                    numCountApprovedOrderNo = nFNRowEffectApprovedOrderNo
                    numCountCancelApprovedOrderNo = nFNRowCancelApprovedOrderNo

                End If

                '...cancel approved any state factory sub order no.
                '--------------------------------------------------------------------------------------------------------------------------------
                Dim DTCancelApprovedSubOrderNo As System.Data.DataTable
                DTCancelApprovedSubOrderNo = New System.Data.DataTable

                Dim oColFTOrderNoCancel As System.Data.DataColumn
                oColFTOrderNoCancel = New System.Data.DataColumn("FTOrderNoCancel", System.Type.GetType("System.String"))
                oColFTOrderNoCancel.Caption = "FTOrderNoCancel"
                DTCancelApprovedSubOrderNo.Columns.Add(oColFTOrderNoCancel.ColumnName, oColFTOrderNoCancel.DataType)

                Dim oColFTSubOrderNoCancel As System.Data.DataColumn
                oColFTSubOrderNoCancel = New System.Data.DataColumn("FTSubOrderNoCancel", System.Type.GetType("System.String"))
                oColFTSubOrderNoCancel.Caption = "FTSubOrderNoCancel"
                DTCancelApprovedSubOrderNo.Columns.Add(oColFTSubOrderNoCancel.ColumnName, oColFTSubOrderNoCancel.DataType)
                '--------------------------------------------------------------------------------------------------------------------------------

                If bChkApprovedTabSubOrderNo = True Then
                    Dim nFNRowEffectApprovedSubOrderNo As Integer, nFNRowCancelApprovedSubOrderNo As Integer
                    nFNRowEffectApprovedSubOrderNo = 0 : nFNRowCancelApprovedSubOrderNo = 0

                    '...Approved Sub Order No. Info
                    For nLoopApprovedSubOrder As Integer = 0 To Me.ogvApprovedSubOrderNoInfo.DataRowCount - 1

                        Dim oDataRowSubOrder As DataRow = Me.ogvApprovedSubOrderNoInfo.GetDataRow(nLoopApprovedSubOrder)

                        Dim tTextFTOrderNo As String
                        Dim tTextFTSubOrderNo As String
                        Dim tTextFTApprovedInfoState As String, tTextFTApprovedInfoStateHide As String
                        Dim nNumFNApprovedInfoCnt As Integer

                        tTextFTOrderNo = oDataRowSubOrder.Item("FTOrderNo").ToString
                        tTextFTSubOrderNo = oDataRowSubOrder.Item("FTSubOrderNo").ToString

                        tTextFTApprovedInfoState = oDataRowSubOrder.Item("FTStateApprovedSubOrderNo").ToString
                        tTextFTApprovedInfoStateHide = oDataRowSubOrder.Item("FTStateApprovedSubOrderNoHide").ToString

                        If DBNull.Value.Equals(oDataRowSubOrder.Item("FNCntApprovedSubOrderNo")) = True Then
                            nNumFNApprovedInfoCnt = 0
                        Else
                            nNumFNApprovedInfoCnt = Val(oDataRowSubOrder.Item("FNCntApprovedSubOrderNo")) + 1
                        End If

                        If tTextFTApprovedInfoState = "1" AndAlso tTextFTApprovedInfoStateHide = "0" Then
                            nFNRowEffectApprovedSubOrderNo = nFNRowEffectApprovedSubOrderNo + 1

                            oStrBuilder.AppendLine("IF NOT EXISTS (SELECT TOP 1 A.FTOrderNo, A.FTSubOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_ApprovedInfo] AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "')")
                            oStrBuilder.AppendLine("   BEGIN")
                            oStrBuilder.AppendLine("      PRINT 'Add new record';")
                            oStrBuilder.AppendLine("	  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_ApprovedInfo] ([FTOrderNo], [FTSubOrderNo], [FTStateApprovedSubOrderNo], [FNCntApprovedSubOrderNo], [FDDateApprovedSubOrderNo], [FTTimeApprovedSubOrderNo], [FTUserApprovedSubOrderNo])")
                            oStrBuilder.AppendLine("      SELECT N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "', N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "', N'1', 1, @FTApprovedInfoDate, @FTApprovedInfoTime, N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';")
                            oStrBuilder.AppendLine("   END")
                            oStrBuilder.AppendLine("ELSE ")
                            oStrBuilder.AppendLine("   BEGIN")
                            oStrBuilder.AppendLine("      PRINT 'update exists record';")
                            oStrBuilder.AppendLine("	  UPDATE A")
                            oStrBuilder.AppendLine("	  SET A.[FTStateApprovedSubOrderNo] = N'1'")
                            oStrBuilder.AppendLine("		 ,A.[FNCntApprovedSubOrderNo] = ISNULL(A.[FNCntApprovedSubOrderNo], 0) + 1")
                            oStrBuilder.AppendLine("		 ,A.[FDDateApprovedSubOrderNo] = @FTApprovedInfoDate")
                            oStrBuilder.AppendLine("		 ,A.[FTTimeApprovedSubOrderNo] = @FTApprovedInfoTime")
                            oStrBuilder.AppendLine(String.Format("		 ,A.[FTUserApprovedSubOrderNo] = N'{0}'", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)))
                            oStrBuilder.AppendLine("	  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_ApprovedInfo] AS A")
                            oStrBuilder.AppendLine("	  WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "'")
                            oStrBuilder.AppendLine("			AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "'")
                            oStrBuilder.AppendLine("   END;")

                        ElseIf tTextFTApprovedInfoState = "0" AndAlso tTextFTApprovedInfoStateHide = "1" Then
                            '...cancel approved factory sub order no.
                            nFNRowCancelApprovedSubOrderNo = nFNRowCancelApprovedSubOrderNo + 1

                            If DTCancelApprovedSubOrderNo.Select("FTOrderNoCancel = '" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "' AND FTSubOrderNoCancel = '" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "'").Length <= 0 Then
                                DTCancelApprovedSubOrderNo.Rows.Add(tTextFTOrderNo, tTextFTSubOrderNo)
                            End If

                            If Not DTCancelApprovedSubOrderNo Is Nothing AndAlso DTCancelApprovedSubOrderNo.Rows.Count > 0 Then DTCancelApprovedSubOrderNo.AcceptChanges()

                            oStrBuilder.AppendLine("UPDATE A")
                            oStrBuilder.AppendLine("SET A.FTStateApprovedSubOrderNo = N'0',")
                            oStrBuilder.AppendLine("    A.FNCntApprovedSubOrderNo = A.FNCntApprovedSubOrderNo - 1,")
                            oStrBuilder.AppendLine("    A.FDDateApprovedSubOrderNo = NULL,")
                            oStrBuilder.AppendLine("    A.FTTimeApprovedSubOrderNo = NULL,")
                            oStrBuilder.AppendLine("    A.FTUserApprovedSubOrderNo = NULL")
                            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A")
                            oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "'")
                            oStrBuilder.AppendLine("      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "';")

                        End If

                    Next nLoopApprovedSubOrder

                    If System.Diagnostics.Debugger.IsAttached = True Then
                        If nFNRowEffectApprovedSubOrderNo > 0 OrElse nFNRowCancelApprovedSubOrderNo > 0 Then
                            MsgBox(String.Format("Execute committ transaction approved factory sub order no. : {0} " & Environment.NewLine & "Execute committ transaction cancel approved factory sub order no. : {1} ", {nFNRowEffectApprovedSubOrderNo, nFNRowCancelApprovedSubOrderNo}), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                        End If
                    End If

                    numCountApprovedSubOrderNo = nFNRowEffectApprovedSubOrderNo
                    numCountCancelApprovedSubOrderNo = nFNRowCancelApprovedSubOrderNo

                End If

                If bChkApprovedTabComponent = True Then
                    Dim nFNRowEffectApprovedComponent As Integer, nFNRowCancelApprovedComponent As Integer
                    nFNRowEffectApprovedComponent = 0 : nFNRowCancelApprovedComponent = 0

                    '...Approved Component Info
                    For nLoopApproveComponent As Integer = 0 To Me.ogvApprovedComponentInfo.DataRowCount - 1

                        Dim oDataRowComponent As DataRow = Me.ogvApprovedComponentInfo.GetDataRow(nLoopApproveComponent)

                        Dim tTextFTOrderNo As String
                        Dim tTextFTSubOrderNo As String
                        Dim tTextFTApprovedInfoState As String, tTextFTApprovedInfoStateHide As String
                        Dim nNumFNApprovedInfoCnt As Integer

                        tTextFTOrderNo = oDataRowComponent.Item("FTOrderNo").ToString
                        tTextFTSubOrderNo = oDataRowComponent.Item("FTSubOrderNo").ToString

                        tTextFTApprovedInfoState = oDataRowComponent.Item("FTStateApprovedComponent").ToString
                        tTextFTApprovedInfoStateHide = oDataRowComponent.Item("FTStateApprovedComponentHide").ToString

                        If DBNull.Value.Equals(oDataRowComponent.Item("FNCntApprovedComponent")) = True Then
                            nNumFNApprovedInfoCnt = 0
                        Else
                            nNumFNApprovedInfoCnt = Val(oDataRowComponent.Item("FNCntApprovedComponent")) + 1
                        End If

                        If tTextFTApprovedInfoState = "1" AndAlso tTextFTApprovedInfoStateHide = "0" Then
                            nFNRowEffectApprovedComponent = nFNRowEffectApprovedComponent + 1

                            oStrBuilder.AppendLine("IF NOT EXISTS(SELECT TOP 1 A.FTOrderNo, A.FTSubOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_ApprovedInfo] AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "')")
                            oStrBuilder.AppendLine("   BEGIN")
                            oStrBuilder.AppendLine("      PRINT 'add new record';")
                            oStrBuilder.AppendLine("	  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_ApprovedInfo] ([FTOrderNo], [FTSubOrderNo], [FTStateApprovedComponent], [FNCntApprovedComponent], [FDDateApprovedComponent], [FTTimeApprovedComponent],[FTUserApprovedComponent])")
                            oStrBuilder.AppendLine("      SELECT N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "', N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "', N'1', 1, @FTApprovedInfoDate, @FTApprovedInfoTime, N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';")
                            oStrBuilder.AppendLine("   END")
                            oStrBuilder.AppendLine("ELSE")
                            oStrBuilder.AppendLine("   BEGIN")
                            oStrBuilder.AppendLine("      PRINT 'update exists record';")
                            oStrBuilder.AppendLine("	  UPDATE A")
                            oStrBuilder.AppendLine("	  SET  A.[FTStateApprovedComponent] = '1'")
                            oStrBuilder.AppendLine("		  ,A.[FNCntApprovedComponent] = ISNULL(A.[FNCntApprovedComponent], 0) + 1")
                            oStrBuilder.AppendLine("		  ,A.[FDDateApprovedComponent] = @FTApprovedInfoDate")
                            oStrBuilder.AppendLine("		  ,A.[FTTimeApprovedComponent] = @FTApprovedInfoTime")
                            oStrBuilder.AppendLine(String.Format("		  ,A.[FTUserApprovedComponent] = N'{0}'", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)))
                            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_ApprovedInfo] AS A")
                            oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "'")
                            oStrBuilder.AppendLine("	  AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "';")
                            oStrBuilder.AppendLine("   END;")

                        ElseIf tTextFTApprovedInfoState = "0" AndAlso tTextFTApprovedInfoStateHide = "1" Then
                            '...cancel approved factory sub order no. component
                            nFNRowCancelApprovedComponent = nFNRowCancelApprovedComponent + 1

                            If DTCancelApprovedSubOrderNo.Select("FTOrderNoCancel = '" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "' AND FTSubOrderNoCancel = '" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "'").Length <= 0 Then
                                DTCancelApprovedSubOrderNo.Rows.Add(tTextFTOrderNo, tTextFTSubOrderNo)
                            End If

                            If Not DTCancelApprovedSubOrderNo Is Nothing AndAlso DTCancelApprovedSubOrderNo.Rows.Count > 0 Then DTCancelApprovedSubOrderNo.AcceptChanges()

                            oStrBuilder.AppendLine("UPDATE A")
                            oStrBuilder.AppendLine("SET A.FTStateApprovedComponent = N'0',")
                            oStrBuilder.AppendLine("    A.FNCntApprovedComponent = A.FNCntApprovedComponent - 1,")
                            oStrBuilder.AppendLine("    A.FDDateApprovedComponent = NULL,")
                            oStrBuilder.AppendLine("    A.FTTimeApprovedComponent = NULL,")
                            oStrBuilder.AppendLine("    A.FTUserApprovedComponent = NULL")
                            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A")
                            oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "'")
                            oStrBuilder.AppendLine("      AND A.FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "';")

                        End If

                    Next nLoopApproveComponent

                    If System.Diagnostics.Debugger.IsAttached = True Then
                        If nFNRowEffectApprovedComponent > 0 OrElse nFNRowCancelApprovedComponent > 0 Then
                            MsgBox(String.Format("Execute committ transaction approved component. : {0} " & Environment.NewLine & "Execute committ transaction cancel approved component : {1}", {nFNRowEffectApprovedComponent, nFNRowCancelApprovedComponent}), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                        End If
                    End If

                    numCountApprovedComponent = nFNRowEffectApprovedComponent
                    numCountCancelApprovedComponent = nFNRowCancelApprovedComponent

                End If

                If bChkApprovedTabSewing = True Then
                    Dim nFNRowEffectApprovedSew As Integer, nFNRowCancelApprovedSew As Integer
                    nFNRowEffectApprovedSew = 0 : nFNRowCancelApprovedSew = 0

                    '...Approved Sewing Info
                    For nLoopApproveSew As Integer = 0 To Me.ogvApprovedSewingInfo.DataRowCount - 1
                        Dim oDataRowSew As DataRow = Me.ogvApprovedSewingInfo.GetDataRow(nLoopApproveSew)

                        Dim tTextFTOrderNo As String
                        Dim tTextFTSubOrderNo As String
                        Dim tTextFTApprovedInfoState As String, tTextFTApprovedInfoStateHide As String
                        Dim nNumFNApprovedInfoCnt As Integer

                        tTextFTOrderNo = oDataRowSew.Item("FTOrderNo").ToString
                        tTextFTSubOrderNo = oDataRowSew.Item("FTSubOrderNo").ToString

                        tTextFTApprovedInfoState = oDataRowSew.Item("FTStateApprovedSewing").ToString
                        tTextFTApprovedInfoStateHide = oDataRowSew.Item("FTStateApprovedSewingHide").ToString

                        If DBNull.Value.Equals(oDataRowSew.Item("FNCntApprovedSewing")) = True Then
                            nNumFNApprovedInfoCnt = 0
                        Else
                            nNumFNApprovedInfoCnt = Val(oDataRowSew.Item("FNCntApprovedSewing")) + 1
                        End If

                        If tTextFTApprovedInfoState = "1" AndAlso tTextFTApprovedInfoStateHide = "0" Then
                            nFNRowEffectApprovedSew = nFNRowEffectApprovedSew + 1

                            oStrBuilder.AppendLine("IF NOT EXISTS(SELECT TOP 1 A.FTOrderNo, A.FTSubOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_ApprovedInfo] AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "')")
                            oStrBuilder.AppendLine("   BEGIN")
                            oStrBuilder.AppendLine("      PRINT 'add new record sewing';")
                            oStrBuilder.AppendLine("	  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_ApprovedInfo] ([FTOrderNo], [FTSubOrderNo], [FTStateApprovedSewing], [FNCntApprovedSewing], [FDDateApprovedSewing], [FTTimeApprovedSewing],[FTUserApprovedSewing])")
                            oStrBuilder.AppendLine("      SELECT N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "', N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "', N'1', 1, @FTApprovedInfoDate, @FTApprovedInfoTime, N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';")
                            oStrBuilder.AppendLine("   END")
                            oStrBuilder.AppendLine("ELSE")
                            oStrBuilder.AppendLine("   BEGIN")
                            oStrBuilder.AppendLine("      PRINT 'update exists record sewing';")
                            oStrBuilder.AppendLine("	  UPDATE A")
                            oStrBuilder.AppendLine("	  SET  A.[FTStateApprovedSewing] = '1'")
                            oStrBuilder.AppendLine("		  ,A.[FNCntApprovedSewing] = ISNULL(A.[FNCntApprovedSewing], 0) + 1")
                            oStrBuilder.AppendLine("		  ,A.[FDDateApprovedSewing] = @FTApprovedInfoDate")
                            oStrBuilder.AppendLine("		  ,A.[FTTimeApprovedSewing] = @FTApprovedInfoTime")
                            oStrBuilder.AppendLine(String.Format("		  ,A.[FTUserApprovedSewing] = N'{0}'", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)))
                            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_ApprovedInfo] AS A")
                            oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "'")
                            oStrBuilder.AppendLine("	  AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "';")
                            oStrBuilder.AppendLine("   END;")

                        ElseIf tTextFTApprovedInfoState = "0" AndAlso tTextFTApprovedInfoStateHide = "1" Then
                            '...cancel approved factory sub order no. sewing
                            nFNRowCancelApprovedSew = nFNRowCancelApprovedSew + 1

                            If DTCancelApprovedSubOrderNo.Select("FTOrderNoCancel = '" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "' AND FTSubOrderNoCancel = '" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "'").Length <= 0 Then
                                DTCancelApprovedSubOrderNo.Rows.Add(tTextFTOrderNo, tTextFTSubOrderNo)
                            End If

                            If Not DTCancelApprovedSubOrderNo Is Nothing AndAlso DTCancelApprovedSubOrderNo.Rows.Count > 0 Then DTCancelApprovedSubOrderNo.AcceptChanges()

                            oStrBuilder.AppendLine("PRINT 'Cancel record approved sewing';")
                            oStrBuilder.AppendLine("UPDATE A")
                            oStrBuilder.AppendLine("SET A.FTStateApprovedSewing = N'1',")
                            oStrBuilder.AppendLine("    A.FNCntApprovedSewing = A.FNCntApprovedSewing - 1,")
                            oStrBuilder.AppendLine("    A.FDDateApprovedSewing = NULL,")
                            oStrBuilder.AppendLine("    A.FTTimeApprovedSewing = NULL,")
                            oStrBuilder.AppendLine("    A.FTUserApprovedSewing = NULL")
                            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A")
                            oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "'")
                            oStrBuilder.AppendLine("      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "';")

                        End If

                    Next

                    If System.Diagnostics.Debugger.IsAttached = True Then
                        If nFNRowEffectApprovedSew > 0 OrElse nFNRowCancelApprovedSew > 0 Then
                            MsgBox(String.Format("Execute committ transaction approved sewing. : {0} " & Environment.NewLine & "Execute committ transaction cancel approved sewing. : {1} ", {nFNRowEffectApprovedSew, nFNRowCancelApprovedSew}), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                        End If
                    End If

                    numCountApprovedSew = nFNRowEffectApprovedSew
                    numCountCancelApprovedSew = nFNRowCancelApprovedSew

                End If

                If bChkApprovedTabPacking = True Then
                    Dim nFNRowEffectApprovedPack As Integer, nFNRowCancelApprovedPack As Integer
                    nFNRowEffectApprovedPack = 0 : nFNRowCancelApprovedPack = 0

                    '...Apprved Packing Info
                    For nLoopApprovePack As Integer = 0 To Me.ogvApprovedPackingInfo.DataRowCount - 1
                        Dim oDataRowSew As DataRow = Me.ogvApprovedPackingInfo.GetDataRow(nLoopApprovePack)

                        Dim tTextFTOrderNo As String
                        Dim tTextFTSubOrderNo As String
                        Dim tTextFTApprovedInfoState As String, tTextFTApprovedInfoStateHide As String
                        Dim nNumFNApprovedInfoCnt As Integer

                        tTextFTOrderNo = oDataRowSew.Item("FTOrderNo").ToString
                        tTextFTSubOrderNo = oDataRowSew.Item("FTSubOrderNo").ToString

                        tTextFTApprovedInfoState = oDataRowSew.Item("FTStateApprovedPacking").ToString
                        tTextFTApprovedInfoStateHide = oDataRowSew.Item("FTStateApprovedPackingHide").ToString

                        If DBNull.Value.Equals(oDataRowSew.Item("FNCntApprovedPacking")) = True Then
                            nNumFNApprovedInfoCnt = 0
                        Else
                            nNumFNApprovedInfoCnt = Val(oDataRowSew.Item("FNCntApprovedPacking")) + 1
                        End If

                        If tTextFTApprovedInfoState = "1" AndAlso tTextFTApprovedInfoStateHide = "0" Then
                            nFNRowEffectApprovedPack = nFNRowEffectApprovedPack + 1

                            oStrBuilder.AppendLine("IF NOT EXISTS(SELECT TOP 1 A.FTOrderNo, A.FTSubOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_ApprovedInfo] AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "')")
                            oStrBuilder.AppendLine("   BEGIN")
                            oStrBuilder.AppendLine("      PRINT 'add new record';")
                            oStrBuilder.AppendLine("	  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_ApprovedInfo] ([FTOrderNo], [FTSubOrderNo], [FTStateApprovedPacking], [FNCntApprovedPacking], [FDDateApprovedPacking], [FTTimeApprovedPacking],[FTUserApprovedPacking])")
                            oStrBuilder.AppendLine("      SELECT N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "', N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "', N'1', 1, @FTApprovedInfoDate, @FTApprovedInfoTime, N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';")
                            oStrBuilder.AppendLine("   END")
                            oStrBuilder.AppendLine("ELSE")
                            oStrBuilder.AppendLine("   BEGIN")
                            oStrBuilder.AppendLine("      PRINT 'update exists record';")
                            oStrBuilder.AppendLine("	  UPDATE A")
                            oStrBuilder.AppendLine("	  SET  A.[FTStateApprovedPacking] = '1'")
                            oStrBuilder.AppendLine("		  ,A.[FNCntApprovedPacking] = ISNULL(A.[FNCntApprovedPacking], 0) + 1")
                            oStrBuilder.AppendLine("		  ,A.[FDDateApprovedPacking] = @FTApprovedInfoDate")
                            oStrBuilder.AppendLine("		  ,A.[FTTimeApprovedPacking] = @FTApprovedInfoTime")
                            oStrBuilder.AppendLine(String.Format("		  ,A.[FTUserApprovedPacking] = N'{0}'", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)))
                            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_ApprovedInfo] AS A")
                            oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "'")
                            oStrBuilder.AppendLine("	  AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "';")
                            oStrBuilder.AppendLine("   END;")

                        ElseIf tTextFTApprovedInfoState = "0" AndAlso tTextFTApprovedInfoStateHide = "1" Then
                            '...cancel approved factory sub order no. packing
                            If DTCancelApprovedSubOrderNo.Select("FTOrderNoCancel = '" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "' AND FTSubOrderNoCancel = '" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "'").Length <= 0 Then
                                DTCancelApprovedSubOrderNo.Rows.Add(tTextFTOrderNo, tTextFTSubOrderNo)
                            End If

                            If Not DTCancelApprovedSubOrderNo Is Nothing AndAlso DTCancelApprovedSubOrderNo.Rows.Count > 0 Then DTCancelApprovedSubOrderNo.AcceptChanges()

                            oStrBuilder.AppendLine("UPDATE A")
                            oStrBuilder.AppendLine("SET A.FTStateApprovedPacking = N'0',")
                            oStrBuilder.AppendLine("    A.FNCntApprovedPacking = A.FNCntApprovedPacking - 1,")
                            oStrBuilder.AppendLine("    A.FDDateApprovedPacking = NULL,")
                            oStrBuilder.AppendLine("    A.FTTimeApprovedPacking = NULL,")
                            oStrBuilder.AppendLine("    A.FTUserApprovedPacking = NULL")
                            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A")
                            oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "'")
                            oStrBuilder.AppendLine("      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "';")

                        End If

                    Next nLoopApprovePack

                    If System.Diagnostics.Debugger.IsAttached = True Then
                        If nFNRowEffectApprovedPack > 0 OrElse nFNRowCancelApprovedPack > 0 Then
                            MsgBox(String.Format("Execute committ transaction approved packing. : {0} " & Environment.NewLine & "Execute commit transactioin cancel approved packing : {1}", {nFNRowEffectApprovedPack, nFNRowCancelApprovedPack}), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                        End If
                    End If

                    numCountApprovedPack = nFNRowEffectApprovedPack
                    numCountCancelApprovedPack = nFNRowCancelApprovedPack

                End If

                If bChkApprovedTabPackCarton = True Then
                    Dim nFNRowEffectApprovedPackRatio As Integer, nFNRowCancelApprovedPackRatio As Integer
                    nFNRowEffectApprovedPackRatio = 0 : nFNRowCancelApprovedPackRatio = 0

                    '...Approved Pack Ratio
                    For nLoopApprovePackRatio As Integer = 0 To Me.ogvApprovedPackRatioInfo.DataRowCount - 1
                        Dim oDataRowSew As DataRow = Me.ogvApprovedPackRatioInfo.GetDataRow(nLoopApprovePackRatio)

                        Dim tTextFTOrderNo As String
                        Dim tTextFTSubOrderNo As String
                        Dim tTextFTApprovedInfoState As String, tTextFTApprovedInfoStateHide As String
                        Dim nNumFNApprovedInfoCnt As Integer

                        tTextFTOrderNo = oDataRowSew.Item("FTOrderNo").ToString
                        tTextFTSubOrderNo = oDataRowSew.Item("FTSubOrderNo").ToString

                        tTextFTApprovedInfoState = oDataRowSew.Item("FTStateApprovedPackRatio").ToString
                        tTextFTApprovedInfoStateHide = oDataRowSew.Item("FTStateApprovedPackRatioHide").ToString

                        If DBNull.Value.Equals(oDataRowSew.Item("FNCntApprovedPackRatio")) = True Then
                            nNumFNApprovedInfoCnt = 0
                        Else
                            nNumFNApprovedInfoCnt = Val(oDataRowSew.Item("FNCntApprovedPackRatio")) + 1
                        End If

                        If tTextFTApprovedInfoState = "1" AndAlso tTextFTApprovedInfoStateHide = "0" Then
                            nFNRowEffectApprovedPackRatio = nFNRowEffectApprovedPackRatio + 1

                            oStrBuilder.AppendLine("IF NOT EXISTS(SELECT TOP 1 A.FTOrderNo, A.FTSubOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_ApprovedInfo] AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "')")
                            oStrBuilder.AppendLine("   BEGIN")
                            oStrBuilder.AppendLine("      PRINT 'add new record';")
                            oStrBuilder.AppendLine("	  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_ApprovedInfo] ([FTOrderNo], [FTSubOrderNo], [FTStateApprovedPackRatio], [FNCntApprovedPackRatio], [FDDateApprovedPackRatio], [FTTimeApprovedPackRatio],[FTUserApprovedPackRatio])")
                            oStrBuilder.AppendLine("      SELECT N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "', N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "', N'1', 1, @FTApprovedInfoDate, @FTApprovedInfoTime, N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';")
                            oStrBuilder.AppendLine("   END")
                            oStrBuilder.AppendLine("ELSE")
                            oStrBuilder.AppendLine("   BEGIN")
                            oStrBuilder.AppendLine("      PRINT 'update exists record';")
                            oStrBuilder.AppendLine("	  UPDATE A")
                            oStrBuilder.AppendLine("	  SET  A.[FTStateApprovedPackRatio] = '1'")
                            oStrBuilder.AppendLine("		  ,A.[FNCntApprovedPackRatio] = ISNULL(A.[FNCntApprovedPackRatio], 0) + 1")
                            oStrBuilder.AppendLine("		  ,A.[FDDateApprovedPackRatio] = @FTApprovedInfoDate")
                            oStrBuilder.AppendLine("		  ,A.[FTTimeApprovedPackRatio] = @FTApprovedInfoTime")
                            oStrBuilder.AppendLine(String.Format("		  ,A.[FTUserApprovedPackRatio] = N'{0}'", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)))
                            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_ApprovedInfo] AS A")
                            oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "'")
                            oStrBuilder.AppendLine("	  AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "';")
                            oStrBuilder.AppendLine("   END;")

                        ElseIf tTextFTApprovedInfoState = "0" AndAlso tTextFTApprovedInfoStateHide = "1" Then
                            '...cancel approved factory sub order no. pack carton ratio

                            nFNRowCancelApprovedPackRatio = nFNRowCancelApprovedPackRatio + 1

                            If DTCancelApprovedSubOrderNo.Select("FTOrderNoCancel = '" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "' AND FTSubOrderNoCancel = '" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "'").Length <= 0 Then
                                DTCancelApprovedSubOrderNo.Rows.Add(tTextFTOrderNo, tTextFTSubOrderNo)
                            End If

                            If Not DTCancelApprovedSubOrderNo Is Nothing AndAlso DTCancelApprovedSubOrderNo.Rows.Count > 0 Then DTCancelApprovedSubOrderNo.AcceptChanges()

                            oStrBuilder.AppendLine("UPDATE A")
                            oStrBuilder.AppendLine("SET A.FTStateApprovedPackRatio = N'0',")
                            oStrBuilder.AppendLine("    A.FNCntApprovedPackRatio = A.FNCntApprovedPackRatio - 1,")
                            oStrBuilder.AppendLine("    A.FDDateApprovedPackRatio = NULL,")
                            oStrBuilder.AppendLine("    A.FTTimeApprovedPackRatio = NULL,")
                            oStrBuilder.AppendLine("    A.FTUserApprovedPackRatio = NULL")
                            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A")
                            oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "'")
                            oStrBuilder.AppendLine("      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "';")

                        End If

                    Next nLoopApprovePackRatio

                    If System.Diagnostics.Debugger.IsAttached = True Then
                        If nFNRowEffectApprovedPackRatio > 0 OrElse nFNRowCancelApprovedPackRatio > 0 Then
                            MsgBox(String.Format("Execute committ transaction approved pack carton ratio. : {0} " & Environment.NewLine & "Execute committ transaction cancel approved pack carton ratio. : {1}", {nFNRowEffectApprovedPackRatio, nFNRowCancelApprovedPackRatio}), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                        End If
                    End If

                    numCountApprovedPackRatio = nFNRowEffectApprovedPackRatio
                    numCountCancelApprovedPackRatio = nFNRowCancelApprovedPackRatio

                End If

                If bChkApprovedTabSizeSpec = True Then
                    Dim nFNRowEffectApprovedSizeSpec As Integer, nFNRowCancelApprovedSizeSpec As Integer
                    nFNRowEffectApprovedSizeSpec = 0 : nFNRowCancelApprovedSizeSpec = 0

                    '...Approved Size Spec Info
                    For nLoopApproveSizeSpec As Integer = 0 To Me.ogvApprovedSizeSpecInfo.DataRowCount - 1
                        Dim oDataRowSizeSpec As DataRow = Me.ogvApprovedSizeSpecInfo.GetDataRow(nLoopApproveSizeSpec)

                        Dim tTextFTOrderNo As String
                        Dim tTextFTSubOrderNo As String
                        Dim tTextFTApprovedInfoState As String, tTextFTApprovedInfoStateHide As String
                        Dim nNumFNApprovedInfoCnt As Integer

                        tTextFTOrderNo = oDataRowSizeSpec.Item("FTOrderNo").ToString
                        tTextFTSubOrderNo = oDataRowSizeSpec.Item("FTSubOrderNo").ToString

                        tTextFTApprovedInfoState = oDataRowSizeSpec.Item("FTStateApprovedSizeSpec").ToString
                        tTextFTApprovedInfoStateHide = oDataRowSizeSpec.Item("FTStateApprovedSizeSpecHide").ToString

                        If DBNull.Value.Equals(oDataRowSizeSpec.Item("FNCntApprovedSizeSpec")) = True Then
                            nNumFNApprovedInfoCnt = 0
                        Else
                            nNumFNApprovedInfoCnt = Val(oDataRowSizeSpec.Item("FNCntApprovedSizeSpec")) + 1
                        End If

                        If tTextFTApprovedInfoState = "1" AndAlso tTextFTApprovedInfoStateHide = "0" Then
                            nFNRowEffectApprovedSizeSpec = nFNRowEffectApprovedSizeSpec + 1

                            oStrBuilder.AppendLine("IF NOT EXISTS(SELECT TOP 1 A.FTOrderNo, A.FTSubOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_ApprovedInfo] AS A (NOLOCK) WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "')")
                            oStrBuilder.AppendLine("   BEGIN")
                            oStrBuilder.AppendLine("      PRINT 'add new record';")
                            oStrBuilder.AppendLine("	  INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_ApprovedInfo] ([FTOrderNo], [FTSubOrderNo], [FTStateApprovedSizeSpec], [FNCntApprovedSizeSpec], [FDDateApprovedSizeSpec], [FTTimeApprovedSizeSpec],[FTUserApprovedSizeSpec])")
                            oStrBuilder.AppendLine("      SELECT N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "', N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "', N'1', 1, @FTApprovedInfoDate, @FTApprovedInfoTime, N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "';")
                            oStrBuilder.AppendLine("   END")
                            oStrBuilder.AppendLine("ELSE")
                            oStrBuilder.AppendLine("   BEGIN")
                            oStrBuilder.AppendLine("      PRINT 'update exists record';")
                            oStrBuilder.AppendLine("	  UPDATE A")
                            oStrBuilder.AppendLine("	  SET  A.[FTStateApprovedSizeSpec] = '1'")
                            oStrBuilder.AppendLine("		  ,A.[FNCntApprovedSizeSpec] = ISNULL(A.[FNCntApprovedSizeSpec], 0) + 1")
                            oStrBuilder.AppendLine("		  ,A.[FDDateApprovedSizeSpec] = @FTApprovedInfoDate")
                            oStrBuilder.AppendLine("		  ,A.[FTTimeApprovedSizeSpec] = @FTApprovedInfoTime")
                            oStrBuilder.AppendLine(String.Format("		  ,A.[FTUserApprovedSizeSpec] = N'{0}'", HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName)))
                            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].[dbo].[TMERTOrderSub_ApprovedInfo] AS A")
                            oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "'")
                            oStrBuilder.AppendLine("	  AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "';")
                            oStrBuilder.AppendLine("   END;")

                        ElseIf tTextFTApprovedInfoState = "0" AndAlso tTextFTApprovedInfoStateHide = "1" Then
                            '...cancel approved factory sub order no. size spec

                            nFNRowCancelApprovedSizeSpec = nFNRowCancelApprovedSizeSpec + 1

                            'oStrBuilder.AppendLine("SET @FNCntApprovedSizeSpec = 0;")

                            If DTCancelApprovedSubOrderNo.Select("FTOrderNoCancel = '" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "' AND FTSubOrderNoCancel = '" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "'").Length <= 0 Then
                                DTCancelApprovedSubOrderNo.Rows.Add(tTextFTOrderNo, tTextFTSubOrderNo)
                            End If

                            If Not DTCancelApprovedSubOrderNo Is Nothing AndAlso DTCancelApprovedSubOrderNo.Rows.Count > 0 Then DTCancelApprovedSubOrderNo.AcceptChanges()

                            oStrBuilder.AppendLine("UPDATE A")
                            oStrBuilder.AppendLine("SET A.FTStateApprovedSizeSpec = N'0',")
                            oStrBuilder.AppendLine("    A.FNCntApprovedSizeSpec = A.FNCntApprovedSizeSpec - 1,")
                            oStrBuilder.AppendLine("    A.FDDateApprovedSizeSpec = NULL,")
                            oStrBuilder.AppendLine("    A.FTTimeApprovedSizeSpec = NULL,")
                            oStrBuilder.AppendLine("    A.FTUserApprovedSizeSpec = NULL")
                            oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A")
                            oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTOrderNo) & "'")
                            oStrBuilder.AppendLine("      AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(tTextFTSubOrderNo) & "';")

                        End If

                    Next nLoopApproveSizeSpec

                    If System.Diagnostics.Debugger.IsAttached = True Then
                        If nFNRowEffectApprovedSizeSpec > 0 OrElse nFNRowCancelApprovedSizeSpec > 0 Then
                            MsgBox(String.Format("Execute Approved committ transaction approved size spec. : {0} " & Environment.NewLine & "Execute committ transaction cancel approved size spec : {1}", {nFNRowEffectApprovedSizeSpec, nFNRowCancelApprovedSizeSpec}), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                        End If
                    End If

                    numCountApprovedSizeSpec = nFNRowEffectApprovedSizeSpec
                    numCountCancelApprovedSizeSpec = nFNRowCancelApprovedSizeSpec

                End If

                If Not DTCancelApprovedSubOrderNo Is Nothing AndAlso DTCancelApprovedSubOrderNo.Rows.Count > 0 Then

                    For Each oDataRowCancel As System.Data.DataRow In DTCancelApprovedSubOrderNo.Rows
                        oStrBuilder.AppendLine("SET @FNCntApprovedSubOrderNo = 0;")
                        oStrBuilder.AppendLine("SET @FNCntApprovedComponent = 0;")
                        oStrBuilder.AppendLine("SET @FNCntApprovedSewing = 0;")
                        oStrBuilder.AppendLine("SET @FNCntApprovedPacking = 0;")
                        oStrBuilder.AppendLine("SET @FNCntApprovedPackRatio = 0;")
                        oStrBuilder.AppendLine("SET @FNCntApprovedSizeSpec = 0;")
                        oStrBuilder.AppendLine("SELECT TOP 1 @FNCntApprovedSubOrderNo = A.FNCntApprovedSubOrderNo, @FNCntApprovedComponent = A.FNCntApprovedComponent, @FNCntApprovedSewing = A.FNCntApprovedSewing, @FNCntApprovedPacking = A.FNCntApprovedPacking, @FNCntApprovedPackRatio = A.FNCntApprovedPackRatio, @FNCntApprovedSizeSpec = A.FNCntApprovedSizeSpec")
                        oStrBuilder.AppendLine("FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A (NOLOCK)")
                        oStrBuilder.AppendLine("WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(oDataRowCancel.Item("FTOrderNoCancel").ToString) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(oDataRowCancel.Item("FTSubOrderNoCancel").ToString) & "';")
                        oStrBuilder.AppendLine("IF (@FNCntApprovedSubOrderNo IS NULL) BEGIN SET @FNCntApprovedSubOrderNo = 0 END;")
                        oStrBuilder.AppendLine("IF (@FNCntApprovedComponent IS NULL) BEGIN SET @FNCntApprovedComponent = 0 END;")
                        oStrBuilder.AppendLine("IF (@FNCntApprovedSewing IS NULL) BEGIN SET @FNCntApprovedSewing = 0 END;")
                        oStrBuilder.AppendLine("IF (@FNCntApprovedPacking IS NULL) BEGIN SET @FNCntApprovedPacking = 0 END;")
                        oStrBuilder.AppendLine("IF (@FNCntApprovedPackRatio IS NULL) BEGIN SET @FNCntApprovedPackRatio = 0 END;")
                        oStrBuilder.AppendLine("IF (@FNCntApprovedSizeSpec IS NULL) BEGIN SET @FNCntApprovedSizeSpec = 0 END;")
                        oStrBuilder.AppendLine(String.Format("PRINT '@FNCntApprovedSubOrderNo : {0} - {1} ' + CONVERT(VARCHAR(30), @FNCntApprovedSubOrderNo);", {oDataRowCancel.Item("FTOrderNoCancel").ToString, oDataRowCancel.Item("FTSubOrderNoCancel").ToString}))
                        oStrBuilder.AppendLine(String.Format("PRINT '@FNCntApprovedComponent : {0} - {1} ' + CONVERT(VARCHAR(30), @FNCntApprovedComponent);", {oDataRowCancel.Item("FTOrderNoCancel").ToString, oDataRowCancel.Item("FTSubOrderNoCancel").ToString}))
                        oStrBuilder.AppendLine(String.Format("PRINT '@FNCntApprovedSewing : {0} - {1} ' + CONVERT(VARCHAR(30), @FNCntApprovedSewing);", {oDataRowCancel.Item("FTOrderNoCancel").ToString, oDataRowCancel.Item("FTSubOrderNoCancel").ToString}))
                        oStrBuilder.AppendLine(String.Format("PRINT '@FNCntApprovedPacking : {0} - {1} ' + CONVERT(VARCHAR(30), @FNCntApprovedPacking);", {oDataRowCancel.Item("FTOrderNoCancel").ToString, oDataRowCancel.Item("FTSubOrderNoCancel").ToString}))
                        oStrBuilder.AppendLine(String.Format("PRINT '@FNCntApprovedPackRatio : {0} - {1} ' + CONVERT(VARCHAR(30), @FNCntApprovedPackRatio);", {oDataRowCancel.Item("FTOrderNoCancel").ToString, oDataRowCancel.Item("FTSubOrderNoCancel").ToString}))
                        oStrBuilder.AppendLine(String.Format("PRINT '@FNCntApprovedSizeSpec : {0} - {1} ' + CONVERT(VARCHAR(30), @FNCntApprovedSizeSpec);", {oDataRowCancel.Item("FTOrderNoCancel").ToString, oDataRowCancel.Item("FTSubOrderNoCancel").ToString}))
                        oStrBuilder.AppendLine("PRINT '(@FNCntApprovedSubOrderNo + @FNCntApprovedComponent + @FNCntApprovedSewing + @FNCntApprovedPacking + @FNCntApprovedPackRatio + @FNCntApprovedSizeSpec)' + CONVERT(VARCHAR(30), (@FNCntApprovedSubOrderNo + @FNCntApprovedComponent + @FNCntApprovedSewing + @FNCntApprovedPacking + @FNCntApprovedPackRatio + @FNCntApprovedSizeSpec));")
                        oStrBuilder.AppendLine("IF ((@FNCntApprovedSubOrderNo + @FNCntApprovedComponent + @FNCntApprovedSewing + @FNCntApprovedPacking + @FNCntApprovedPackRatio + @FNCntApprovedSizeSpec) = 0)")
                        oStrBuilder.AppendLine("   BEGIN")
                        oStrBuilder.AppendLine("      DELETE A FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_ApprovedInfo AS A WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(oDataRowCancel.Item("FTOrderNoCancel").ToString) & "' AND A.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(oDataRowCancel.Item("FTSubOrderNoCancel").ToString) & "'")
                        oStrBuilder.AppendLine("   END;")
                    Next

                End If

                If Not DTCancelApprovedSubOrderNo Is Nothing Then DTCancelApprovedSubOrderNo.Dispose()

                '...step perform manipulate transaction
                If oStrBuilder.Length > 0 Then
                    sSQL = oStrBuilder.ToString()

                    Dim sSQLUpdatePeriod As String

                    sSQLUpdatePeriod = ""
                    sSQLUpdatePeriod = "DECLARE @FTApprovedInfoDate AS NVARCHAR(10);"
                    sSQLUpdatePeriod &= Environment.NewLine & "DECLARE @FTApprovedInfoTime AS NVARCHAR(8);"

                    sSQLUpdatePeriod &= Environment.NewLine & "DECLARE @FNApprovedInfoCnt AS INT;"
                    sSQLUpdatePeriod &= Environment.NewLine & "DECLARE @FNCntApprovedSubOrderNo AS INT;"
                    sSQLUpdatePeriod &= Environment.NewLine & "DECLARE @FNCntApprovedComponent  AS INT;"
                    sSQLUpdatePeriod &= Environment.NewLine & "DECLARE @FNCntApprovedSewing     AS INT;"
                    sSQLUpdatePeriod &= Environment.NewLine & "DECLARE @FNCntApprovedPacking    AS INT;"
                    sSQLUpdatePeriod &= Environment.NewLine & "DECLARE @FNCntApprovedPackRatio  AS INT;"
                    sSQLUpdatePeriod &= Environment.NewLine & "DECLARE @FNCntApprovedSizeSpec   AS INT;"

                    sSQLUpdatePeriod &= Environment.NewLine & "SELECT @FTApprovedInfoDate = CONVERT(VARCHAR(10), GETDATE(),111);"
                    sSQLUpdatePeriod &= Environment.NewLine & "SELECT @FTApprovedInfoTime = CONVERT(varchar(8),  GETDATE(),114);"
                    sSQLUpdatePeriod &= Environment.NewLine & "PRINT '@FTApprovedInfoDate : ' + @FTApprovedInfoDate;"
                    sSQLUpdatePeriod &= Environment.NewLine & "PRINT '@FTApprovedInfoTime : ' + @FTApprovedInfoTime;"

                    sSQL = sSQLUpdatePeriod & Environment.NewLine & sSQL

                    HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(HI.Conn.DB.DataBaseName.DB_MERCHAN)
                    HI.Conn.SQLConn.SqlConnectionOpen()
                    HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                    HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                    If HI.Conn.SQLConn.Execute_Tran(sSQL, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        If System.Diagnostics.Debugger.IsAttached = True Then
                            MsgBox("Save data not complete !!!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
                        End If

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Else
                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        oStrBuilder.Remove(0, oStrBuilder.Length)

                        Select Case HI.ST.Lang.Language
                            Case HI.ST.Lang.eLang.TH
                                If numCountApprovedOrderNo > 0 OrElse numCountCancelApprovedOrderNo > 0 Then
                                    oStrBuilder.AppendLine(String.Format("อนุมัติรายละเอียดข้อมูลการสั่งผลิต จำนวน : {0} รายการ", numCountApprovedOrderNo))
                                    oStrBuilder.AppendLine(String.Format("ยกเลิกอนุมัติรายละเอียดข้อมูลการสั่งผลิต จำนวน : {0} รายการ", numCountCancelApprovedOrderNo))
                                End If

                                If numCountApprovedSubOrderNo > 0 OrElse numCountCancelApprovedSubOrderNo > 0 Then
                                    oStrBuilder.AppendLine(String.Format("อนุมัติรายละเอียด ข้อมูลรายการใบสั่งผลิตย่อย จำนวน : {0} รายการ", numCountApprovedSubOrderNo))
                                    oStrBuilder.AppendLine(String.Format("ยกเลิกอนุมัติรายละเอียด ข้อมูลรายการใบสั่งผลิตย่อย จำนวน : {0} รายการ", numCountCancelApprovedSubOrderNo))
                                End If

                                If numCountApprovedComponent > 0 OrElse numCountCancelApprovedComponent > 0 Then
                                    oStrBuilder.AppendLine(String.Format("อนุมัติรายละเอียด ข้อมูลวัตถุดิบประการผลิต : {0} รายการ", numCountApprovedComponent))
                                    oStrBuilder.AppendLine(String.Format("ยกเลิกอนุมัติรายละเอียด ข้อมูลวัตถุดิบประการผลิต : {0} รายการ", numCountCancelApprovedComponent))
                                End If

                                If numCountApprovedSew > 0 OrElse numCountCancelApprovedSew > 0 Then
                                    oStrBuilder.AppendLine(String.Format("อนุมัติรายละเอียด ข้อมูลขั้นตอนงานเย็บ : {0} รายการ", numCountApprovedSew))
                                    oStrBuilder.AppendLine(String.Format("ยกเลิกอนุมัติรายละเอียด ข้อมูลขั้นตอนงานเย็บ : {0} รายการ", numCountCancelApprovedSew))
                                End If

                                If numCountApprovedPack > 0 OrElse numCountCancelApprovedPack > 0 Then
                                    oStrBuilder.AppendLine(String.Format("อนุมัติรายละเอียด ข้อมูลขั้นตอนงานแพ็ค : {0} รายการ", numCountApprovedPack))
                                    oStrBuilder.AppendLine(String.Format("ยกเลิกอนุมัติรายละเอียด ข้อมูลขั้นตอนงานแพ็ค : {0} รายการ", numCountCancelApprovedPack))
                                End If

                                If numCountApprovedPackRatio > 0 OrElse numCountCancelApprovedPackRatio > 0 Then
                                    oStrBuilder.AppendLine(String.Format("อนุมัติรายละเอียด ข้อมูลอัตราส่วนการแพ็ค {0} รายการ", numCountApprovedPackRatio))
                                    oStrBuilder.AppendLine(String.Format("ยกเลิกอนุมัติรายละเอียด ข้อมูลอัตราส่วนการแพ็ค {0} รายการ", numCountCancelApprovedPackRatio))
                                End If

                                If numCountApprovedSizeSpec > 0 OrElse numCountCancelApprovedSizeSpec > 0 Then
                                    oStrBuilder.AppendLine(String.Format("อนุมัติรายละเอียด ข้อมูลจำเพาะขนาดผลิตภัณฑ์ : {0} รายการ", numCountApprovedSizeSpec))
                                    oStrBuilder.AppendLine(String.Format("ยกเลิกอนุมัติรายละเอียด ข้อมูลจำเพาะขนาดผลิตภัณฑ์ : {0} รายการ", numCountCancelApprovedSizeSpec))
                                End If

                            Case Else

                                If numCountApprovedOrderNo > 0 OrElse numCountCancelApprovedOrderNo > 0 Then
                                    oStrBuilder.AppendLine(String.Format("Approved Order Information : {0} no.", numCountApprovedOrderNo))
                                    oStrBuilder.AppendLine(String.Format("Cancel Approved Order Information : {0} no.", numCountCancelApprovedOrderNo))
                                End If

                                If numCountApprovedSubOrderNo > 0 OrElse numCountCancelApprovedSubOrderNo > 0 Then
                                    oStrBuilder.AppendLine(String.Format("Approved Sub Order Information : {0} no.", numCountApprovedSubOrderNo))
                                    oStrBuilder.AppendLine(String.Format("Cancel Approved Sub Order Information : {0} no.", numCountCancelApprovedSubOrderNo))
                                End If

                                If numCountApprovedComponent > 0 OrElse numCountCancelApprovedComponent > 0 Then
                                    oStrBuilder.AppendLine(String.Format("Approved Component Information : {0} no.", numCountApprovedComponent))
                                    oStrBuilder.AppendLine(String.Format("Cancel Approved Component Information : {0} no.", numCountCancelApprovedComponent))
                                End If

                                If numCountApprovedSew > 0 OrElse numCountCancelApprovedSew > 0 Then
                                    oStrBuilder.AppendLine(String.Format("Approved Sewing Information : {0} no.", numCountApprovedSew))
                                    oStrBuilder.AppendLine(String.Format("Cancel Approved Sewing Information : {0} no.", numCountCancelApprovedSew))
                                End If

                                If numCountApprovedPack > 0 OrElse numCountCancelApprovedPack > 0 Then
                                    oStrBuilder.AppendLine(String.Format("Approved Packing Infomation : {0} no.", numCountApprovedPack))
                                    oStrBuilder.AppendLine(String.Format("Cancel Approved Packing Infomation : {0} no.", numCountCancelApprovedPack))
                                End If

                                If numCountApprovedPackRatio > 0 OrElse numCountCancelApprovedPackRatio > 0 Then
                                    oStrBuilder.AppendLine(String.Format("Approved Ratio Packing : {0} no.", numCountApprovedPackRatio))
                                    oStrBuilder.AppendLine(String.Format("Cancel Approved Ratio Packing : {0} no.", numCountCancelApprovedPackRatio))
                                End If

                                If numCountApprovedSizeSpec > 0 OrElse numCountCancelApprovedSizeSpec > 0 Then
                                    oStrBuilder.AppendLine(String.Format("Approved Size Spec Information : {0} no.", numCountApprovedSizeSpec))
                                    oStrBuilder.AppendLine(String.Format("Cancel Approved Size Spec Information : {0} no.", numCountCancelApprovedSizeSpec))
                                End If

                        End Select

                        If oStrBuilder.Length > 0 Then
                            MsgBox(oStrBuilder.ToString, vbOKOnly + vbInformation, My.Application.Info.Title)
                        End If

                        bSaveApprovedInfo = True

                    End If

                End If

            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString(), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title.ToString)
            End If
        End Try

        If Not System.Diagnostics.Debugger.IsAttached = True Then
            _Spls.Close()
        End If

        Return bSaveApprovedInfo

    End Function

#End Region

#Region "Event Handle"

#End Region

    Private Sub wOrderApprovedInfo_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.oTabApprovedInfo.SelectedTabPageIndex = 0
    End Sub

    Private Sub FNHSysBuyId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysBuyId.EditValueChanged
        Try
            bFlagLoad = True
            'If Me.FNHSysBuyId.Text.Trim <> "" Then
            '    Dim sSQLBuyId As String = "SELECT TOP 1 A.FNHSysBuyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMBuy AS A (NOLOCK) WHERE A.FTBuyCode = N'" & HI.UL.ULF.rpQuoted(Me.FNHSysBuyId.Text.Trim) & "';"
            '    Me.FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField(sSQLBuyId, HI.Conn.DB.DataBaseName.DB_MASTER, "")
            '    If Val(Me.FNHSysBuyId.Properties.Tag) > 0 Then
            '        Call PROC_SHOWbBrowseDataListApprovedOrderInfo()
            '    Else
            '        If Val(Me.FNHSysBuyId.Properties.Tag) = 0 AndAlso Val(Me.FNHSysStyleId.Properties.Tag) = 0 Then
            '            Me.ogdApprovedOrderInfo.DataSource = Nothing
            '            Me.ogdApprovedOrderInfo.Refresh()

            '            Me.ogdApprovedSubOrderNoInfo.DataSource = Nothing
            '            Me.ogdApprovedSubOrderNoInfo.Refresh()

            '            Me.ogdApprovedComponentInfo.DataSource = Nothing
            '            Me.ogdApprovedComponentInfo.Refresh()

            '            Me.ogdApprovedSewingInfo.DataSource = Nothing
            '            Me.ogdApprovedSewingInfo.Refresh()

            '            Me.ogdApprovedPackingInfo.DataSource = Nothing
            '            Me.ogdApprovedPackingInfo.Refresh()

            '            Me.ogdApprovedPackRatioInfo.DataSource = Nothing
            '            Me.ogdApprovedPackRatioInfo.Refresh()

            '            Me.ogdApprovedSizeSpecInfo.DataSource = Nothing
            '            Me.ogdApprovedSizeSpecInfo.Refresh()
            '        Else
            '            Call PROC_SHOWbBrowseDataListApprovedOrderInfo()
            '        End If
            '    End If
            'Else
            '    Me.FNHSysBuyId.Properties.Tag = ""
            '    If Val(Me.FNHSysStyleId.Properties.Tag) > 0 Then
            '        Call PROC_SHOWbBrowseDataListApprovedOrderInfo()
            '    Else
            '        Me.ogdApprovedOrderInfo.DataSource = Nothing
            '        Me.ogdApprovedOrderInfo.Refresh()

            '        Me.ogdApprovedSubOrderNoInfo.DataSource = Nothing
            '        Me.ogdApprovedSubOrderNoInfo.Refresh()

            '        Me.ogdApprovedComponentInfo.DataSource = Nothing
            '        Me.ogdApprovedComponentInfo.Refresh()

            '        Me.ogdApprovedSewingInfo.DataSource = Nothing
            '        Me.ogdApprovedSewingInfo.Refresh()

            '        Me.ogdApprovedPackingInfo.DataSource = Nothing
            '        Me.ogdApprovedPackingInfo.Refresh()

            '        Me.ogdApprovedPackRatioInfo.DataSource = Nothing
            '        Me.ogdApprovedPackRatioInfo.Refresh()

            '        Me.ogdApprovedSizeSpecInfo.DataSource = Nothing
            '        Me.ogdApprovedSizeSpecInfo.Refresh()
            '    End If

            'End If

            If Me.FNHSysBuyId.Text.Trim <> "" Then

                Dim sSQLBuyId As String = "SELECT TOP 1 A.FNHSysBuyId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMBuy AS A (NOLOCK) WHERE A.FTBuyCode = N'" & HI.UL.ULF.rpQuoted(Me.FNHSysBuyId.Text.Trim) & "';"
                Me.FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField(sSQLBuyId, HI.Conn.DB.DataBaseName.DB_MASTER, "")

            End If

            Me.ogdApprovedOrderInfo.DataSource = Nothing : Me.ogdApprovedOrderInfo.Refresh()
            Me.ogdApprovedSubOrderNoInfo.DataSource = Nothing : Me.ogdApprovedSubOrderNoInfo.Refresh()
            Me.ogdApprovedComponentInfo.DataSource = Nothing : Me.ogdApprovedComponentInfo.Refresh()
            Me.ogdApprovedSewingInfo.DataSource = Nothing : Me.ogdApprovedSewingInfo.Refresh()
            Me.ogdApprovedPackingInfo.DataSource = Nothing : Me.ogdApprovedPackingInfo.Refresh()
            Me.ogdApprovedPackRatioInfo.DataSource = Nothing : Me.ogdApprovedPackRatioInfo.Refresh()
            Me.ogdApprovedSizeSpecInfo.DataSource = Nothing : Me.ogdApprovedSizeSpecInfo.Refresh()

        Catch ex As Exception

            If System.Diagnostics.Debugger.IsAttached = True Then

                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)

            End If

        End Try

        REM 2015/03/05 If bFlagLoad = True Then Me.oTabApprovedInfo.SelectedTabPageIndex = 0

        REM 2015/03/05 bFlagLoad = False

    End Sub

    Private Sub FNHSysStyleId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysStyleId.EditValueChanged
        Try
            bFlagLoad = True
            'If Me.FNHSysStyleId.Text.Trim <> "" Then
            '    Dim sSQLStyleId As String = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle WITH(NOLOCK) WHERE FTStyleCode = N'" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text.Trim) & "';"
            '    Me.FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(sSQLStyleId, Conn.DB.DataBaseName.DB_MASTER, "")
            '    If Val(Me.FNHSysStyleId.Properties.Tag) > 0 Then
            '        Call PROC_SHOWbBrowseDataListApprovedOrderInfo()
            '    Else
            '        If Val(Me.FNHSysBuyId.Properties.Tag) = 0 AndAlso Val(Me.FNHSysStyleId.Properties.Tag) = 0 Then
            '            Me.ogdApprovedOrderInfo.DataSource = Nothing
            '            Me.ogdApprovedOrderInfo.Refresh()

            '            Me.ogdApprovedSubOrderNoInfo.DataSource = Nothing
            '            Me.ogdApprovedSubOrderNoInfo.Refresh()

            '            Me.ogdApprovedComponentInfo.DataSource = Nothing
            '            Me.ogdApprovedComponentInfo.Refresh()

            '            Me.ogdApprovedSewingInfo.DataSource = Nothing
            '            Me.ogdApprovedSewingInfo.Refresh()

            '            Me.ogdApprovedPackingInfo.DataSource = Nothing
            '            Me.ogdApprovedPackingInfo.Refresh()

            '            Me.ogdApprovedPackRatioInfo.DataSource = Nothing
            '            Me.ogdApprovedPackRatioInfo.Refresh()

            '            Me.ogdApprovedSizeSpecInfo.DataSource = Nothing
            '            Me.ogdApprovedSizeSpecInfo.Refresh()
            '        Else
            '            Call PROC_SHOWbBrowseDataListApprovedOrderInfo()
            '        End If

            '    End If

            'Else
            '    Me.FNHSysStyleId.Properties.Tag = ""
            '    If Val(Me.FNHSysBuyId.Properties.Tag) > 0 Then
            '        Call PROC_SHOWbBrowseDataListApprovedOrderInfo()
            '    Else
            '        Me.ogdApprovedOrderInfo.DataSource = Nothing
            '        Me.ogdApprovedOrderInfo.Refresh()

            '        Me.ogdApprovedSubOrderNoInfo.DataSource = Nothing
            '        Me.ogdApprovedSubOrderNoInfo.Refresh()

            '        Me.ogdApprovedComponentInfo.DataSource = Nothing
            '        Me.ogdApprovedComponentInfo.Refresh()

            '        Me.ogdApprovedSewingInfo.DataSource = Nothing
            '        Me.ogdApprovedSewingInfo.Refresh()

            '        Me.ogdApprovedPackingInfo.DataSource = Nothing
            '        Me.ogdApprovedPackingInfo.Refresh()

            '        Me.ogdApprovedPackRatioInfo.DataSource = Nothing
            '        Me.ogdApprovedPackRatioInfo.Refresh()

            '        Me.ogdApprovedSizeSpecInfo.DataSource = Nothing
            '        Me.ogdApprovedSizeSpecInfo.Refresh()
            '    End If

            'End If

            If Me.FNHSysStyleId.Text.Trim <> "" Then

                Dim sSQLStyleId As String = "SELECT TOP 1 FNHSysStyleId FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle WITH(NOLOCK) WHERE FTStyleCode = N'" & HI.UL.ULF.rpQuoted(FNHSysStyleId.Text.Trim) & "';"
                Me.FNHSysStyleId.Properties.Tag = HI.Conn.SQLConn.GetField(sSQLStyleId, Conn.DB.DataBaseName.DB_MASTER, "")

            End If

            Me.ogdApprovedOrderInfo.DataSource = Nothing : Me.ogdApprovedOrderInfo.Refresh()
            Me.ogdApprovedSubOrderNoInfo.DataSource = Nothing : Me.ogdApprovedSubOrderNoInfo.Refresh()
            Me.ogdApprovedComponentInfo.DataSource = Nothing : Me.ogdApprovedComponentInfo.Refresh()
            Me.ogdApprovedSewingInfo.DataSource = Nothing : Me.ogdApprovedSewingInfo.Refresh()
            Me.ogdApprovedPackingInfo.DataSource = Nothing : Me.ogdApprovedPackingInfo.Refresh()
            Me.ogdApprovedPackRatioInfo.DataSource = Nothing : Me.ogdApprovedPackRatioInfo.Refresh()
            Me.ogdApprovedSizeSpecInfo.DataSource = Nothing : Me.ogdApprovedSizeSpecInfo.Refresh()

        Catch ex As Exception

            If System.Diagnostics.Debugger.IsAttached = True Then

                MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)

            End If

        End Try

        REM 2015/03/05 If bFlagLoad = True Then Me.oTabApprovedInfo.SelectedTabPageIndex = 0
        REM 2015/03/05 bFlagLoad = False

    End Sub

    Private Sub oChkApproveAllIssue_CheckedChanged(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try
    End Sub

    Private Sub oChkApproveAllRecord_CheckedChanged(sender As Object, e As EventArgs) Handles oChkApproveAllRecord.CheckedChanged

        If bFlagTabInfoChange = True Then Exit Sub

        Try

            Dim bChkApprovedTabOrderInfo As Boolean = False
            Dim bChkApprovedTabSubOrderNo As Boolean = False
            Dim bChkApprovedTabComponent As Boolean = False
            Dim bChkApprovedTabSewing As Boolean = False
            Dim bChkApprovedTabPacking As Boolean = False
            Dim bChkApprovedTabPackCarton As Boolean = False
            Dim bChkApprovedTabSizeSpec As Boolean = False
            Dim tChkApprovedAllInfo As String = "0"

            '...validate check approved information
            If Me.oChkApproveAllInfo.Checked = True Then
                tChkApprovedAllInfo = "1"
            End If

            If tChkApprovedAllInfo = "0" Then

                Select Case Me.oTabApprovedInfo.SelectedTabPageIndex
                    Case eApprovedTabIndex.ApprovedOrderNo : bChkApprovedTabOrderInfo = True
                    Case eApprovedTabIndex.ApprovedSubOrderNo : bChkApprovedTabSubOrderNo = True
                    Case eApprovedTabIndex.ApprovedComponent : bChkApprovedTabComponent = True
                    Case eApprovedTabIndex.ApprovedSewing : bChkApprovedTabSewing = True
                    Case eApprovedTabIndex.ApprovedPacking : bChkApprovedTabPacking = True
                    Case eApprovedTabIndex.ApprovedPackingCarton : bChkApprovedTabPackCarton = True
                    Case eApprovedTabIndex.ApprovedSizeSpec : bChkApprovedTabSizeSpec = True
                End Select

            Else

                REM 2015/03/05 bChkApprovedTabOrderInfo = True : bChkApprovedTabSubOrderNo = True : bChkApprovedTabComponent = True : bChkApprovedTabSewing = True : bChkApprovedTabPacking = True : bChkApprovedTabPackCarton = True : bChkApprovedTabSizeSpec = True
                If Me.oTabApprovedInfo.SelectedTabPageIndex = eApprovedTabIndex.ApprovedOrderNo Then
                    bChkApprovedTabOrderInfo = True : bChkApprovedTabSubOrderNo = True : bChkApprovedTabComponent = True : bChkApprovedTabSewing = True : bChkApprovedTabPacking = True : bChkApprovedTabPackCarton = True : bChkApprovedTabSizeSpec = True
                Else

                    Select Case Me.oTabApprovedInfo.SelectedTabPageIndex
                        Case eApprovedTabIndex.ApprovedSubOrderNo : bChkApprovedTabSubOrderNo = True
                        Case eApprovedTabIndex.ApprovedComponent : bChkApprovedTabComponent = True
                        Case eApprovedTabIndex.ApprovedSewing : bChkApprovedTabSewing = True
                        Case eApprovedTabIndex.ApprovedPacking : bChkApprovedTabPacking = True
                        Case eApprovedTabIndex.ApprovedPackingCarton : bChkApprovedTabPackCarton = True
                        Case eApprovedTabIndex.ApprovedSizeSpec : bChkApprovedTabSizeSpec = True
                        Case Else
                            '...Nothing
                    End Select

                End If

            End If

            'Order No.
            '===================================================================================================
            If bChkApprovedTabOrderInfo = True Then
                Dim strValChkOrderNo As String = "0"
                If Me.oChkApproveAllRecord.Checked Then
                    strValChkOrderNo = "1"
                End If

                With Me.ogdApprovedOrderInfo
                    If Not (.DataSource Is Nothing) And ogvApprovedOrderInfo.RowCount > 0 Then

                        'With ogvApprovedOrderInfo
                        '    For nIdxRowApprovedOrder As Integer = 0 To .RowCount - 1
                        '        REM 2015/03/04
                        '        'If .GetRowCellValue(nIdxRowApprovedOrder, "FTApprovedInfoStateHide").ToString = "0" Then
                        '        '    .SetRowCellValue(nIdxRowApprovedOrder, .Columns.ColumnByFieldName("FTApprovedInfoState"), strValChkOrderNo)
                        '        'End If

                        '        .SetRowCellValue(nIdxRowApprovedOrder, .Columns.ColumnByFieldName("FTApprovedInfoState"), strValChkOrderNo)

                        '    Next nIdxRowApprovedOrder

                        'End With
                        With CType(.DataSource, DataTable)
                            .AcceptChanges()
                            For Each R As DataRow In .Rows
                                R!FTApprovedInfoState = strValChkOrderNo
                            Next
                            .AcceptChanges()
                        End With
                        .Refresh()
                    End If

                End With
            End If
            '===================================================================================================

            'Sub Order No.
            '===================================================================================================
            If bChkApprovedTabSubOrderNo = True Then
                Dim strValChkSubOrderNo As String = "0"
                If Me.oChkApproveAllRecord.Checked Then
                    strValChkSubOrderNo = "1"
                End If

                With Me.ogdApprovedSubOrderNoInfo

                    If Not (.DataSource Is Nothing) And ogvApprovedSubOrderNoInfo.RowCount > 0 Then

                        CType(.DataSource, System.Data.DataTable).DefaultView.Sort = ""

                        'CType(.DataSource, System.Data.DataTable).BeginInit()

                        With ogvApprovedSubOrderNoInfo
                            For nIdxApprovedSubOrder As Integer = 0 To .RowCount - 1

                                REM 2015/03/04
                                'If .GetRowCellValue(nIdxApprovedSubOrder, "FTStateApprovedSubOrderNoHide").ToString = "0" Then
                                '    .SetRowCellValue(nIdxApprovedSubOrder, .Columns.ColumnByFieldName("FTStateApprovedSubOrderNo"), strValChkSubOrderNo)
                                'End If

                                REM 2015/03/05
                                'If Me.oTabApprovedInfo.SelectedTabPageIndex = eApprovedTabIndex.ApprovedSubOrderNo Then
                                '    If .GetRowCellValue(nIdxApprovedSubOrder, "FTStateApprovedSubOrderNoHide").ToString = "0" Then
                                '        .SetRowCellValue(nIdxApprovedSubOrder, .Columns.ColumnByFieldName("FTStateApprovedSubOrderNo"), strValChkSubOrderNo)
                                '    End If

                                'Else
                                '    .SetRowCellValue(nIdxApprovedSubOrder, .Columns.ColumnByFieldName("FTStateApprovedSubOrderNo"), strValChkSubOrderNo)
                                'End If

                                .SetRowCellValue(nIdxApprovedSubOrder, .Columns.ColumnByFieldName("FTStateApprovedSubOrderNo"), strValChkSubOrderNo)

                            Next nIdxApprovedSubOrder

                        End With

                        'CType(.DataSource, System.Data.DataTable).EndInit()

                        CType(.DataSource, DataTable).AcceptChanges()

                        If Not CType(.DataSource, System.Data.DataTable) Is Nothing AndAlso CType(.DataSource, System.Data.DataTable).Rows.Count > 0 Then CType(.DataSource, System.Data.DataTable).DefaultView.Sort = "FTStateApprovedSubOrderNo ASC, FTStyleCode ASC, FTOrderNo ASC, FTSubOrderNo ASC"

                    End If

                End With

            End If
            '===================================================================================================

            'Component
            '===================================================================================================
            If bChkApprovedTabComponent = True Then
                Dim strValChkComponent As String = "0"
                If Me.oChkApproveAllRecord.Checked Then
                    strValChkComponent = "1"
                End If

                With Me.ogdApprovedComponentInfo
                    If Not (.DataSource Is Nothing) And ogvApprovedComponentInfo.RowCount > 0 Then

                        CType(.DataSource, System.Data.DataTable).DefaultView.Sort = ""

                        With ogvApprovedComponentInfo
                            For nIdxApprovedComponent As Integer = 0 To .RowCount - 1
                                REM 2015/03/04
                                'If .GetRowCellValue(nIdxApprovedComponent, "FTStateApprovedComponentHide").ToString = "0" Then
                                '    .SetRowCellValue(nIdxApprovedComponent, .Columns.ColumnByFieldName("FTStateApprovedComponent"), strValChkComponent)
                                'End If

                                REM 2015/03/05
                                'If Me.oTabApprovedInfo.SelectedTabPageIndex = eApprovedTabIndex.ApprovedComponent Then
                                '    If .GetRowCellValue(nIdxApprovedComponent, "FTStateApprovedComponentHide").ToString = "0" Then
                                '        .SetRowCellValue(nIdxApprovedComponent, .Columns.ColumnByFieldName("FTStateApprovedComponent"), strValChkComponent)
                                '    End If
                                'Else
                                '    .SetRowCellValue(nIdxApprovedComponent, .Columns.ColumnByFieldName("FTStateApprovedComponent"), strValChkComponent)
                                'End If

                                .SetRowCellValue(nIdxApprovedComponent, .Columns.ColumnByFieldName("FTStateApprovedComponent"), strValChkComponent)

                            Next nIdxApprovedComponent

                        End With

                        CType(.DataSource, DataTable).AcceptChanges()

                        If Not CType(.DataSource, System.Data.DataTable) Is Nothing AndAlso CType(.DataSource, System.Data.DataTable).Rows.Count > 0 Then CType(.DataSource, System.Data.DataTable).DefaultView.Sort = "FTStateApprovedComponent ASC, FTStyleCode ASC, FTOrderNo ASC, FTSubOrderNo ASC"

                    End If

                End With
            End If
            '===================================================================================================

            'Sewing
            '===================================================================================================
            If bChkApprovedTabSewing = True Then
                Dim strValChkSew As String = "0"
                If Me.oChkApproveAllRecord.Checked Then
                    strValChkSew = "1"
                End If

                With Me.ogdApprovedSewingInfo
                    If Not (.DataSource Is Nothing) And ogvApprovedSewingInfo.RowCount > 0 Then

                        CType(.DataSource, System.Data.DataTable).DefaultView.Sort = ""

                        With ogvApprovedSewingInfo
                            For nIdxApprovedSew As Integer = 0 To .RowCount - 1
                                REM 2015/03/04
                                'If .GetRowCellValue(nIdxApprovedSew, "FTStateApprovedSewingHide").ToString = "0" Then
                                '    .SetRowCellValue(nIdxApprovedSew, .Columns.ColumnByFieldName("FTStateApprovedSewing"), strValChkSew)
                                'End If

                                REM 2015/03/05
                                'If Me.oTabApprovedInfo.SelectedTabPageIndex = eApprovedTabIndex.ApprovedSewing Then
                                '    If .GetRowCellValue(nIdxApprovedSew, "FTStateApprovedSewingHide").ToString = "0" Then
                                '        .SetRowCellValue(nIdxApprovedSew, .Columns.ColumnByFieldName("FTStateApprovedSewing"), strValChkSew)
                                '    End If
                                'Else
                                '    .SetRowCellValue(nIdxApprovedSew, .Columns.ColumnByFieldName("FTStateApprovedSewing"), strValChkSew)
                                'End If

                                .SetRowCellValue(nIdxApprovedSew, .Columns.ColumnByFieldName("FTStateApprovedSewing"), strValChkSew)

                            Next nIdxApprovedSew

                        End With

                        CType(.DataSource, DataTable).AcceptChanges()

                        If Not CType(.DataSource, System.Data.DataTable) Is Nothing AndAlso CType(.DataSource, System.Data.DataTable).Rows.Count > 0 Then CType(.DataSource, System.Data.DataTable).DefaultView.Sort = "FTStateApprovedSewing ASC, FTStyleCode ASC, FTOrderNo ASC, FTSubOrderNo ASC"

                    End If

                End With
            End If
            '===================================================================================================

            'Packing
            '===================================================================================================
            If bChkApprovedTabPacking = True Then
                Dim strValChkPack As String = "0"
                If Me.oChkApproveAllRecord.Checked Then
                    strValChkPack = "1"
                End If

                With Me.ogdApprovedPackingInfo
                    If Not (.DataSource Is Nothing) And ogvApprovedPackingInfo.RowCount > 0 Then

                        CType(.DataSource, System.Data.DataTable).DefaultView.Sort = ""

                        With ogvApprovedPackingInfo
                            For nIdxApprovedPack As Integer = 0 To .RowCount - 1
                                REM 2015/03/04
                                'If .GetRowCellValue(nIdxApprovedPack, "FTStateApprovedPackingHide").ToString = "0" Then
                                '    .SetRowCellValue(nIdxApprovedPack, .Columns.ColumnByFieldName("FTStateApprovedPacking"), strValChkPack)
                                'End If

                                REM 2015/03/05
                                'If Me.oTabApprovedInfo.SelectedTabPageIndex = eApprovedTabIndex.ApprovedPacking Then
                                '    If .GetRowCellValue(nIdxApprovedPack, "FTStateApprovedPackingHide").ToString = "0" Then
                                '        .SetRowCellValue(nIdxApprovedPack, .Columns.ColumnByFieldName("FTStateApprovedPacking"), strValChkPack)
                                '    End If
                                'Else
                                '    .SetRowCellValue(nIdxApprovedPack, .Columns.ColumnByFieldName("FTStateApprovedPacking"), strValChkPack)
                                'End If

                                .SetRowCellValue(nIdxApprovedPack, .Columns.ColumnByFieldName("FTStateApprovedPacking"), strValChkPack)

                            Next nIdxApprovedPack

                        End With

                        CType(.DataSource, DataTable).AcceptChanges()

                        If Not CType(.DataSource, System.Data.DataTable) Is Nothing AndAlso CType(.DataSource, System.Data.DataTable).Rows.Count > 0 Then CType(.DataSource, System.Data.DataTable).DefaultView.Sort = "FTStateApprovedPacking ASC, FTStyleCode ASC, FTOrderNo ASC, FTSubOrderNo ASC"

                    End If

                End With
            End If
            '===================================================================================================

            'Pack Ratio
            '===================================================================================================
            If bChkApprovedTabPackCarton = True Then
                Dim strValChkPackRatio As String = "0"
                If Me.oChkApproveAllRecord.Checked Then
                    strValChkPackRatio = "1"
                End If

                With Me.ogdApprovedPackRatioInfo
                    If Not (.DataSource Is Nothing) And ogvApprovedPackRatioInfo.RowCount > 0 Then

                        CType(.DataSource, System.Data.DataTable).DefaultView.Sort = ""

                        With ogvApprovedPackRatioInfo
                            For nIdxApprovedPackRatio As Integer = 0 To .RowCount - 1
                                REM 2015/03/04
                                'If .GetRowCellValue(nIdxApprovedPackRatio, "FTStateApprovedPackRatioHide").ToString = "0" Then
                                '    .SetRowCellValue(nIdxApprovedPackRatio, .Columns.ColumnByFieldName("FTStateApprovedPackRatio"), strValChkPackRatio)
                                'End If

                                REM 2015/03/05
                                'If Me.oTabApprovedInfo.SelectedTabPageIndex = eApprovedTabIndex.ApprovedPackingCarton Then
                                '    If .GetRowCellValue(nIdxApprovedPackRatio, "FTStateApprovedPackRatioHide").ToString = "0" Then
                                '        .SetRowCellValue(nIdxApprovedPackRatio, .Columns.ColumnByFieldName("FTStateApprovedPackRatio"), strValChkPackRatio)
                                '    End If
                                'Else
                                '    .SetRowCellValue(nIdxApprovedPackRatio, .Columns.ColumnByFieldName("FTStateApprovedPackRatio"), strValChkPackRatio)
                                'End If

                                .SetRowCellValue(nIdxApprovedPackRatio, .Columns.ColumnByFieldName("FTStateApprovedPackRatio"), strValChkPackRatio)

                            Next nIdxApprovedPackRatio

                        End With

                        CType(.DataSource, DataTable).AcceptChanges()

                        If Not CType(.DataSource, System.Data.DataTable) Is Nothing AndAlso CType(.DataSource, System.Data.DataTable).Rows.Count > 0 Then CType(.DataSource, System.Data.DataTable).DefaultView.Sort = "FTStateApprovedPackRatio ASC, FTStyleCode ASC, FTOrderNo ASC, FTOrderNo ASC"

                    End If

                End With
            End If
            '===================================================================================================

            'Size Spec
            '===================================================================================================
            If bChkApprovedTabSizeSpec = True Then
                Dim strValChkSizeSpec As String = "0"
                If Me.oChkApproveAllRecord.Checked Then
                    strValChkSizeSpec = "1"
                End If

                With Me.ogdApprovedSizeSpecInfo
                    If Not (.DataSource Is Nothing) And ogvApprovedSizeSpecInfo.RowCount > 0 Then

                        CType(.DataSource, System.Data.DataTable).DefaultView.Sort = ""

                        With ogvApprovedSizeSpecInfo
                            For nIdxApprovedSizeSpec As Integer = 0 To .RowCount - 1
                                'If .GetRowCellValue(nIdxApprovedSizeSpec, "FTStateApprovedSizeSpecHide").ToString = "0" Then
                                '    .SetRowCellValue(nIdxApprovedSizeSpec, .Columns.ColumnByFieldName("FTStateApprovedSizeSpec"), strValChkSizeSpec)
                                'End If

                                REM 2015/03/05
                                'If Me.oTabApprovedInfo.SelectedTabPageIndex = eApprovedTabIndex.ApprovedSizeSpec Then
                                '    If .GetRowCellValue(nIdxApprovedSizeSpec, "FTStateApprovedSizeSpecHide").ToString = "0" Then
                                '        .SetRowCellValue(nIdxApprovedSizeSpec, .Columns.ColumnByFieldName("FTStateApprovedSizeSpec"), strValChkSizeSpec)
                                '    End If
                                'Else
                                '    .SetRowCellValue(nIdxApprovedSizeSpec, .Columns.ColumnByFieldName("FTStateApprovedSizeSpec"), strValChkSizeSpec)
                                'End If

                                .SetRowCellValue(nIdxApprovedSizeSpec, .Columns.ColumnByFieldName("FTStateApprovedSizeSpec"), strValChkSizeSpec)

                            Next nIdxApprovedSizeSpec

                        End With

                        CType(.DataSource, DataTable).AcceptChanges()

                        If Not CType(.DataSource, System.Data.DataTable) Is Nothing AndAlso CType(.DataSource, System.Data.DataTable).Rows.Count > 0 Then CType(.DataSource, System.Data.DataTable).DefaultView.Sort = "FTStateApprovedSizeSpec ASC, FTStyleCode ASC, FTOrderNo ASC, FTSubOrderNo ASC"

                    End If

                End With
            End If
            '===================================================================================================

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

    End Sub

    'Private Sub ogvApprovedSubOrderNoInfo_CustomRowCellEdit(sender As Object, e As XtraGrid.Views.Grid.CustomRowCellEditEventArgs) Handles ogvApprovedSubOrderNoInfo.CustomRowCellEdit
    '    Try

    '    Catch ex As Exception
    '        If System.Diagnostics.Debugger.IsAttached = True Then
    '            MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
    '        End If
    '    End Try
    'End Sub

    'Private Sub ogvApprovedOrderInfo_CustomRowCellEdit(sender As Object, e As XtraGrid.Views.Grid.CustomRowCellEditEventArgs) Handles ogvApprovedOrderInfo.CustomRowCellEdit
    '    Try
    '        If e.RowHandle < 0 Then Exit Sub

    '        If e.Column.FieldName.ToUpper = "FTApprovedInfoState".ToUpper Then

    '            'Dim repositoryItemCheckEditReadOnly = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
    '            'repositoryItemCheckEditReadOnly.Name = "repositoryItemCheckEditReadOnly"
    '            'repositoryItemCheckEditReadOnly.ReadOnly = True

    '            'If Me.ogvApprovedOrderInfo.GetRowCellValue(e.RowHandle, e.Column.FieldName).ToString = "1" Then
    '            '    'e.RepositoryItem = repositoryItemCheckEditReadOnly
    '            '    'e.RepositoryItem.ReadOnly = True
    '            'Else
    '            '    'oRepositoryItemCheckEdit.CheckStyle = XtraEditors.Controls.CheckStyles.Standard
    '            '    'e.RepositoryItem = oRepositoryItemCheckEdit
    '            'End If

    '            'If e.CellValue.ToString = "1" Then
    '            '    e.RepositoryItem.ReadOnly = True
    '            'Else
    '            '    e.RepositoryItem.ReadOnly = False
    '            'End If



    '        End If

    '    Catch ex As Exception
    '        If System.Diagnostics.Debugger.IsAttached = True Then
    '            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
    '        End If
    '    End Try
    'End Sub

    Private Sub ogvApprovedOrderInfo_RowCellStyle(sender As Object, e As XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvApprovedOrderInfo.RowCellStyle
        Try
            Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

            If view.RowCount < 1 Then Exit Sub

            If e.RowHandle < 0 Then Exit Sub

            Dim tTextFTApprovedInfoStateHide As String = view.GetRowCellValue(e.RowHandle, view.Columns("FTApprovedInfoStateHide"))

            Dim nNumFNApprovedInfoCnt As Integer

            If DBNull.Value.Equals(view.GetRowCellValue(e.RowHandle, view.Columns("FNApprovedInfoCnt"))) = True Then
                nNumFNApprovedInfoCnt = 0
            Else
                nNumFNApprovedInfoCnt = Val(view.GetRowCellValue(e.RowHandle, view.Columns("FNApprovedInfoCnt")))
            End If

            If tTextFTApprovedInfoStateHide = "1" Then
                '...current state is approved
                e.Appearance.ForeColor = System.Drawing.Color.DarkBlue
            ElseIf tTextFTApprovedInfoStateHide = "0" AndAlso nNumFNApprovedInfoCnt > 0 Then
                e.Appearance.ForeColor = System.Drawing.Color.DarkRed
            Else
                e.Appearance.ForeColor = DefaultAppearance.ForeColor
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If
        End Try
    End Sub

    Private Sub ogvApprovedOrderInfo_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles ogvApprovedOrderInfo.ShowingEditor
        REM 2015/03/04
        'Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

        'If view.FocusedRowHandle < 0 Then Exit Sub

        'If view.FocusedColumn.FieldName = "FTApprovedInfoState" And view.GetRowCellValue(view.FocusedRowHandle, "FTApprovedInfoStateHide").ToString = "1" Then
        '    e.Cancel = True
        'End If
    End Sub

    Private Sub ogvApprovedSubOrderNoInfo_RowCellStyle(sender As Object, e As XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvApprovedSubOrderNoInfo.RowCellStyle
        Try
            Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

            If view.RowCount < 1 Then Exit Sub

            If e.RowHandle < 0 Then Exit Sub

            Dim tTextFTStateApprovedSubOrderNoHide As String = view.GetRowCellValue(e.RowHandle, view.Columns("FTStateApprovedSubOrderNoHide"))

            Dim nNumFNCntApprovedSubOrderNo As Integer

            If DBNull.Value.Equals(view.GetRowCellValue(e.RowHandle, view.Columns("FNCntApprovedSubOrderNo"))) = True Then
                nNumFNCntApprovedSubOrderNo = 0
            Else
                nNumFNCntApprovedSubOrderNo = Val(view.GetRowCellValue(e.RowHandle, view.Columns("FNCntApprovedSubOrderNo")))
            End If

            If tTextFTStateApprovedSubOrderNoHide = "1" Then
                '...current state is approved
                e.Appearance.ForeColor = System.Drawing.Color.DarkBlue
            ElseIf tTextFTStateApprovedSubOrderNoHide = "0" AndAlso nNumFNCntApprovedSubOrderNo > 0 Then
                e.Appearance.ForeColor = System.Drawing.Color.DarkRed
            Else
                e.Appearance.ForeColor = DefaultAppearance.ForeColor
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If
        End Try
    End Sub

    Private Sub ogvApprovedSubOrderNoInfo_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles ogvApprovedSubOrderNoInfo.ShowingEditor
        REM 2015/03/04
        'Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

        'If view.FocusedRowHandle < 0 Then Exit Sub

        'If view.FocusedColumn.FieldName = "FTStateApprovedSubOrderNo" And view.GetRowCellValue(view.FocusedRowHandle, "FTStateApprovedSubOrderNoHide").ToString = "1" Then
        '    e.Cancel = True
        'End If
    End Sub

    Private Sub ogvApprovedComponentInfo_RowCellStyle(sender As Object, e As XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvApprovedComponentInfo.RowCellStyle
        Try
            Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

            If view.RowCount < 1 Then Exit Sub

            If e.RowHandle < 0 Then Exit Sub

            Dim tTextFTStateApprovedComponentHide As String = view.GetRowCellValue(e.RowHandle, view.Columns("FTStateApprovedComponentHide"))

            Dim nNumFNCntApprovedComponent As Integer

            If DBNull.Value.Equals(view.GetRowCellValue(e.RowHandle, view.Columns("FNCntApprovedComponent"))) = True Then
                nNumFNCntApprovedComponent = 0
            Else
                nNumFNCntApprovedComponent = Val(view.GetRowCellValue(e.RowHandle, view.Columns("FNCntApprovedComponent")))
            End If

            If tTextFTStateApprovedComponentHide = "1" Then
                '...current state is approved
                e.Appearance.ForeColor = System.Drawing.Color.DarkBlue
            ElseIf tTextFTStateApprovedComponentHide = "0" AndAlso nNumFNCntApprovedComponent > 0 Then
                e.Appearance.ForeColor = System.Drawing.Color.DarkRed
            Else
                e.Appearance.ForeColor = DefaultAppearance.ForeColor
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If
        End Try
    End Sub

    Private Sub ogvApprovedComponentInfo_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles ogvApprovedComponentInfo.ShowingEditor
        REM 2015/03/04
        'Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

        'If view.FocusedRowHandle < 0 Then Exit Sub

        'If view.FocusedColumn.FieldName = "FTStateApprovedComponent" And view.GetRowCellValue(view.FocusedRowHandle, "FTStateApprovedComponentHide").ToString = "1" Then
        '    e.Cancel = True
        'End If
    End Sub

    Private Sub ogvApprovedSewingInfo_RowStyle(sender As Object, e As XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvApprovedSewingInfo.RowStyle
        Try
            Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

            If view.RowCount < 1 Then Exit Sub

            If e.RowHandle < 0 Then Exit Sub

            Dim tTextFTStateApprovedSewingHide As String = view.GetRowCellValue(e.RowHandle, view.Columns("FTStateApprovedSewingHide"))

            Dim nNumFNCntApprovedSewing As Integer

            If DBNull.Value.Equals(view.GetRowCellValue(e.RowHandle, view.Columns("FNCntApprovedSewing"))) = True Then
                nNumFNCntApprovedSewing = 0
            Else
                nNumFNCntApprovedSewing = Val(view.GetRowCellValue(e.RowHandle, view.Columns("FNCntApprovedSewing")))
            End If

            If tTextFTStateApprovedSewingHide = "1" Then
                '...current state is approved
                e.Appearance.ForeColor = System.Drawing.Color.DarkBlue
            ElseIf tTextFTStateApprovedSewingHide = "0" AndAlso nNumFNCntApprovedSewing > 0 Then
                e.Appearance.ForeColor = System.Drawing.Color.DarkRed
            Else
                e.Appearance.ForeColor = DefaultAppearance.ForeColor
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If
        End Try
    End Sub

    Private Sub ogvApprovedSewingInfo_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles ogvApprovedSewingInfo.ShowingEditor
        REM 2015/03/04
        'Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

        'If view.FocusedRowHandle < 0 Then Exit Sub

        'If view.FocusedColumn.FieldName = "FTStateApprovedSewing" And view.GetRowCellValue(view.FocusedRowHandle, "FTStateApprovedSewingHide").ToString = "1" Then
        '    e.Cancel = True
        'End If
    End Sub

    Private Sub ogvApprovedPackingInfo_RowCellStyle(sender As Object, e As XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvApprovedPackingInfo.RowCellStyle
        Try
            Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

            If view.RowCount < 1 Then Exit Sub

            If e.RowHandle < 0 Then Exit Sub

            Dim tTextFTStateApprovedPackingHide As String = view.GetRowCellValue(e.RowHandle, view.Columns("FTStateApprovedPackingHide"))

            Dim nNumFNCntApprovedPacking As Integer

            If DBNull.Value.Equals(view.GetRowCellValue(e.RowHandle, view.Columns("FNCntApprovedPacking"))) = True Then
                nNumFNCntApprovedPacking = 0
            Else
                nNumFNCntApprovedPacking = Val(view.GetRowCellValue(e.RowHandle, view.Columns("FNCntApprovedPacking")))
            End If

            If tTextFTStateApprovedPackingHide = "1" Then
                '...current state is approved
                e.Appearance.ForeColor = System.Drawing.Color.DarkBlue
            ElseIf tTextFTStateApprovedPackingHide = "0" AndAlso nNumFNCntApprovedPacking > 0 Then
                e.Appearance.ForeColor = System.Drawing.Color.DarkRed
            Else
                e.Appearance.ForeColor = DefaultAppearance.ForeColor
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If
        End Try
    End Sub

    Private Sub ogvApprovedPackingInfo_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles ogvApprovedPackingInfo.ShowingEditor
        REM 2015/03/04
        'Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

        'If view.FocusedRowHandle < 0 Then Exit Sub

        'If view.FocusedColumn.FieldName = "FTStateApprovedPacking" And view.GetRowCellValue(view.FocusedRowHandle, "FTStateApprovedPackingHide").ToString = "1" Then
        '    e.Cancel = True
        'End If
    End Sub

    Private Sub ogvApprovedPackRatioInfo_RowCellStyle(sender As Object, e As XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvApprovedPackRatioInfo.RowCellStyle
        Try
            Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

            If view.RowCount < 1 Then Exit Sub

            If e.RowHandle < 0 Then Exit Sub

            Dim tTextFTStateApprovedPackRatioHide As String = view.GetRowCellValue(e.RowHandle, view.Columns("FTStateApprovedPackRatioHide"))

            Dim nNumFNCntApprovedPackRatio As Integer

            If DBNull.Value.Equals(view.GetRowCellValue(e.RowHandle, view.Columns("FNCntApprovedPackRatio"))) = True Then
                nNumFNCntApprovedPackRatio = 0
            Else
                nNumFNCntApprovedPackRatio = Val(view.GetRowCellValue(e.RowHandle, view.Columns("FNCntApprovedPackRatio")))
            End If

            If tTextFTStateApprovedPackRatioHide = "1" Then
                '...current state is approved
                e.Appearance.ForeColor = System.Drawing.Color.DarkBlue
            ElseIf tTextFTStateApprovedPackRatioHide = "0" AndAlso nNumFNCntApprovedPackRatio > 0 Then
                e.Appearance.ForeColor = System.Drawing.Color.DarkRed
            Else
                e.Appearance.ForeColor = DefaultAppearance.ForeColor
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If
        End Try
    End Sub

    Private Sub ogvApprovedPackRatioInfo_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles ogvApprovedPackRatioInfo.ShowingEditor
        REM 2015/03/04
        'Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

        'If view.FocusedRowHandle < 0 Then Exit Sub

        'If view.FocusedColumn.FieldName = "FTStateApprovedPackRatio" And view.GetRowCellValue(view.FocusedRowHandle, "FTStateApprovedPackRatioHide").ToString = "1" Then
        '    e.Cancel = True
        'End If
    End Sub

    Private Sub ogvApprovedSizeSpecInfo_RowCellStyle(sender As Object, e As XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvApprovedSizeSpecInfo.RowCellStyle
        Try
            Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

            If view.RowCount < 1 Then Exit Sub

            If e.RowHandle < 0 Then Exit Sub

            Dim tTextFTStateApprovedSizeSpecHide As String = view.GetRowCellValue(e.RowHandle, view.Columns("FTStateApprovedSizeSpecHide"))

            Dim nNumFNCntApprovedSizeSpec As Integer

            If DBNull.Value.Equals(view.GetRowCellValue(e.RowHandle, view.Columns("FNCntApprovedSizeSpec"))) = True Then
                nNumFNCntApprovedSizeSpec = 0
            Else
                nNumFNCntApprovedSizeSpec = Val(view.GetRowCellValue(e.RowHandle, view.Columns("FNCntApprovedSizeSpec")))
            End If

            If tTextFTStateApprovedSizeSpecHide = "1" Then
                '...current state is approved
                e.Appearance.ForeColor = System.Drawing.Color.DarkBlue
            ElseIf tTextFTStateApprovedSizeSpecHide = "0" AndAlso nNumFNCntApprovedSizeSpec > 0 Then
                e.Appearance.ForeColor = System.Drawing.Color.DarkRed
            Else
                e.Appearance.ForeColor = DefaultAppearance.ForeColor
            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If
        End Try
    End Sub

    Private Sub ogvApprovedSizeSpecInfo_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles ogvApprovedSizeSpecInfo.ShowingEditor
        REM 2015/03/04
        'Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

        'If view.FocusedRowHandle < 0 Then Exit Sub

        'If view.FocusedColumn.FieldName = "FTStateApprovedSizeSpec" And view.GetRowCellValue(view.FocusedRowHandle, "FTStateApprovedSizeSpecHide").ToString = "1" Then
        '    e.Cancel = True
        'End If
    End Sub

    Private Sub ApproveAllInfo()
        Try
            Dim _State As String = IIf(Me.oChkApproveAllInfo.Checked, "1", "0")
            With CType(Me.ogdApprovedOrderInfo.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    R!FTApprovedInfoState = _State
                Next
                .AcceptChanges()
            End With
            Me.ogdApprovedOrderInfo.Refresh()

            With CType(Me.ogdApprovedSubOrderNoInfo.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    R!FTStateApprovedSubOrderNo = _State
                Next
                .AcceptChanges()
            End With
            Me.ogdApprovedSubOrderNoInfo.Refresh()

            With CType(Me.ogdApprovedComponentInfo.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    R!FTStateApprovedComponent = _State
                Next
                .AcceptChanges()
            End With
            Me.ogdApprovedOrderInfo.Refresh()

            With CType(Me.ogdApprovedSewingInfo.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    R!FTStateApprovedSewing = _State
                Next
                .AcceptChanges()
            End With
            Me.ogdApprovedSewingInfo.Refresh()

            With CType(Me.ogdApprovedPackingInfo.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    R!FTStateApprovedPacking = _State
                Next
                .AcceptChanges()
            End With
            Me.ogdApprovedPackingInfo.Refresh()

            With CType(Me.ogdApprovedPackRatioInfo.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    R!FTStateApprovedPackRatio = _State
                Next
                .AcceptChanges()
            End With
            Me.ogdApprovedPackRatioInfo.Refresh()

            With CType(Me.ogdApprovedSizeSpecInfo.DataSource, DataTable)
                .AcceptChanges()
                For Each R As DataRow In .Rows
                    R!FTStateApprovedSizeSpec = _State
                Next
                .AcceptChanges()
            End With
            Me.ogdApprovedSizeSpecInfo.Refresh()
        Catch ex As Exception
        End Try
    End Sub


    Private Sub oChkApproveAllInfo_CheckedChanged(sender As Object, e As EventArgs) Handles oChkApproveAllInfo.CheckedChanged

        Call ApproveAllInfo()
        Exit Sub

        If Not Me.oChkApproveAllInfo.Checked = True Then Exit Sub
        '====================================================================================================================================================
        '...approved all factory order no information
        Dim DTApprovedOrderNoAllInfo As System.Data.DataTable

        DTApprovedOrderNoAllInfo = New System.Data.DataTable

        Dim oColFTApprovedOrderNoAllInfo As System.Data.DataColumn
        oColFTApprovedOrderNoAllInfo = New System.Data.DataColumn("FTApprovedOrderNoAllInfo", System.Type.GetType("System.String"))
        oColFTApprovedOrderNoAllInfo.Caption = "FTApprovedOrderNoAllInfo"

        DTApprovedOrderNoAllInfo.Columns.Add(oColFTApprovedOrderNoAllInfo.ColumnName, oColFTApprovedOrderNoAllInfo.DataType)

        Dim oColFTChkStateOrderNoAllInfo As System.Data.DataColumn
        oColFTChkStateOrderNoAllInfo = New System.Data.DataColumn("FTChkStateOrderNoAllInfo", System.Type.GetType("System.String"))
        oColFTChkStateOrderNoAllInfo.Caption = "FTChkStateOrderNoAllInfo"

        DTApprovedOrderNoAllInfo.Columns.Add(oColFTChkStateOrderNoAllInfo.ColumnName, oColFTChkStateOrderNoAllInfo.DataType)

        '...approved all factory order no, sub order no information
        Dim DTApprovedOrderNoSubAllInfo As System.Data.DataTable

        DTApprovedOrderNoSubAllInfo = New System.Data.DataTable

        Dim oColFTApprovedOrderNoSubAllInfo As System.Data.DataColumn
        oColFTApprovedOrderNoSubAllInfo = New System.Data.DataColumn("FTApprovedOrderNoSubAllInfo", System.Type.GetType("System.String"))
        oColFTApprovedOrderNoSubAllInfo.Caption = "FTApprovedOrderNoSubAllInfo"

        DTApprovedOrderNoSubAllInfo.Columns.Add(oColFTApprovedOrderNoSubAllInfo.ColumnName, oColFTApprovedOrderNoSubAllInfo.DataType)

        Dim oColFTApprovedSubOrderNoAllInfo As System.Data.DataColumn
        oColFTApprovedSubOrderNoAllInfo = New System.Data.DataColumn("FTApprovedSubOrderNoAllInfo", System.Type.GetType("System.String"))
        oColFTApprovedSubOrderNoAllInfo.Caption = "FTApprovedSubOrderNoAllInfo"

        DTApprovedOrderNoSubAllInfo.Columns.Add(oColFTApprovedSubOrderNoAllInfo.ColumnName, oColFTApprovedSubOrderNoAllInfo.DataType)

        Dim oColFTChkStateSubOrderNoAllInfo As System.Data.DataColumn
        oColFTChkStateSubOrderNoAllInfo = New System.Data.DataColumn("FTChkStateSubOrderNoAllInfo", System.Type.GetType("System.String"))
        oColFTChkStateSubOrderNoAllInfo.Caption = "FTChkStateSubOrderNoAllInfo"

        DTApprovedOrderNoSubAllInfo.Columns.Add(oColFTChkStateSubOrderNoAllInfo.ColumnName, oColFTChkStateSubOrderNoAllInfo.DataType)

        Select Case Me.oTabApprovedInfo.SelectedTabPageIndex
            Case eApprovedTabIndex.ApprovedOrderNo
                With Me.ogdApprovedOrderInfo

                    If Not .DataSource Is Nothing AndAlso Me.ogvApprovedOrderInfo.RowCount > 0 Then

                        For nLoopApprovedOrder As Integer = 0 To Me.ogvApprovedOrderInfo.DataRowCount - 1

                            Dim oDataRowApprovedOrder As System.Data.DataRow = Me.ogvApprovedOrderInfo.GetDataRow(nLoopApprovedOrder)

                            If DTApprovedOrderNoAllInfo.Select("FTApprovedOrderNoAllInfo = '" & HI.UL.ULF.rpQuoted(oDataRowApprovedOrder.Item("FTOrderNo").ToString) & "'").Length <= 0 Then
                                DTApprovedOrderNoAllInfo.Rows.Add(oDataRowApprovedOrder.Item("FTOrderNo").ToString, oDataRowApprovedOrder.Item("FTApprovedInfoState").ToString)
                            End If

                        Next nLoopApprovedOrder

                    End If

                End With

                If Not DTApprovedOrderNoAllInfo Is Nothing AndAlso DTApprovedOrderNoAllInfo.Rows.Count > 0 Then DTApprovedOrderNoAllInfo.AcceptChanges()

            Case eApprovedTabIndex.ApprovedSubOrderNo
                With Me.ogdApprovedSubOrderNoInfo

                    If Not .DataSource Is Nothing AndAlso Me.ogvApprovedSubOrderNoInfo.RowCount > 0 Then

                        For nLoopApprovedSubOrder As Integer = 0 To Me.ogvApprovedSubOrderNoInfo.DataRowCount - 1

                            Dim oDataRowApprovedSubOrder As System.Data.DataRow = Me.ogvApprovedSubOrderNoInfo.GetDataRow(nLoopApprovedSubOrder)



                            If DTApprovedOrderNoSubAllInfo.Select("FTApprovedOrderNoSubAllInfo = '" & HI.UL.ULF.rpQuoted(oDataRowApprovedSubOrder.Item("FTOrderNo").ToString) & "' AND FTApprovedSubOrderNoAllInfo = '" & HI.UL.ULF.rpQuoted(oDataRowApprovedSubOrder.Item("FTSubOrderNo").ToString) & "'").Length <= 0 Then
                                DTApprovedOrderNoSubAllInfo.Rows.Add(oDataRowApprovedSubOrder.Item("FTOrderNo").ToString, oDataRowApprovedSubOrder.Item("FTSubOrderNo").ToString, oDataRowApprovedSubOrder.Item("FTStateApprovedSubOrderNo").ToString)
                            End If

                        Next nLoopApprovedSubOrder

                    End If

                End With

                If Not DTApprovedOrderNoSubAllInfo Is Nothing AndAlso DTApprovedOrderNoSubAllInfo.Rows.Count > 0 Then DTApprovedOrderNoSubAllInfo.AcceptChanges()
            Case Else
                '...Nothing
        End Select

        Try


            Dim bTabSelectAllInfo As Boolean = False

            Select Case Me.oTabApprovedInfo.SelectedTabPageIndex
                Case eApprovedTabIndex.ApprovedOrderNo, eApprovedTabIndex.ApprovedSubOrderNo
                    bTabSelectAllInfo = True
                Case eApprovedTabIndex.ApprovedComponent, eApprovedTabIndex.ApprovedSewing, eApprovedTabIndex.ApprovedPacking, eApprovedTabIndex.ApprovedPackingCarton, eApprovedTabIndex.ApprovedSizeSpec
                    bTabSelectAllInfo = False
            End Select

            If bTabSelectAllInfo = True Then

                Dim bChkApprovedAllInfo As String
                bChkApprovedAllInfo = "0"
                If Me.oChkApproveAllInfo.Checked = True Then
                    bChkApprovedAllInfo = "1"
                End If




                If bChkApprovedAllInfo = "1" Then

                    If Not DTApprovedOrderNoAllInfo Is Nothing AndAlso DTApprovedOrderNoAllInfo.Rows.Count > 0 Then
                        '...approved all information by factory order no.

                        For nLoopApprovedAll As Integer = 0 To DTApprovedOrderNoAllInfo.Rows.Count - 1

                            Dim tTextFTOrderNo As String
                            Dim tTextFTChkApprovedOrderNo As String

                            tTextFTOrderNo = DTApprovedOrderNoAllInfo.Rows(nLoopApprovedAll)("FTApprovedOrderNoAllInfo").ToString
                            tTextFTChkApprovedOrderNo = bChkApprovedAllInfo '  DTApprovedOrderNoAllInfo.Rows(nLoopApprovedAll)("FTChkStateOrderNoAllInfo").ToString

                            '...approved factory sub order no.
                            With Me.ogdApprovedSubOrderNoInfo

                                If Not .DataSource Is Nothing And ogvApprovedSubOrderNoInfo.RowCount > 0 Then

                                    For nLoopApprovedSubOrderNo As Integer = 0 To Me.ogvApprovedSubOrderNoInfo.DataRowCount - 1
                                        Dim oDataRowApprovedSubOrderNo As System.Data.DataRow = Me.ogvApprovedSubOrderNoInfo.GetDataRow(nLoopApprovedSubOrderNo)
                                        If (oDataRowApprovedSubOrderNo.Item("FTOrderNo").ToString = tTextFTOrderNo) Then
                                            With Me.ogvApprovedSubOrderNoInfo
                                                .SetRowCellValue(nLoopApprovedSubOrderNo, .Columns.ColumnByFieldName("FTStateApprovedSubOrderNo"), tTextFTChkApprovedOrderNo)
                                            End With
                                        End If
                                    Next nLoopApprovedSubOrderNo

                                    CType(.DataSource, System.Data.DataTable).AcceptChanges()

                                End If

                            End With

                            '...approved factory sub order no component.
                            With Me.ogdApprovedComponentInfo

                                If Not .DataSource Is Nothing And ogvApprovedComponentInfo.RowCount > 0 Then

                                    For nLoopApprovedComponent As Integer = 0 To Me.ogvApprovedComponentInfo.DataRowCount - 1

                                        Dim oDataRowApprovedComponent As System.Data.DataRow = Me.ogvApprovedComponentInfo.GetDataRow(nLoopApprovedComponent)

                                        If oDataRowApprovedComponent.Item("FTOrderNo").ToString = tTextFTOrderNo Then

                                            With Me.ogvApprovedComponentInfo
                                                .SetRowCellValue(nLoopApprovedComponent, .Columns.ColumnByFieldName("FTStateApprovedComponent"), tTextFTChkApprovedOrderNo)
                                            End With

                                        End If

                                    Next nLoopApprovedComponent

                                    CType(.DataSource, System.Data.DataTable).AcceptChanges()

                                End If

                            End With

                            '...approved factory sub order no. sewing
                            With Me.ogdApprovedSewingInfo

                                If Not .DataSource Is Nothing And Me.ogvApprovedSewingInfo.RowCount > 0 Then

                                    For nLoopApprovedSew As Integer = 0 To Me.ogvApprovedSewingInfo.DataRowCount - 1

                                        Dim oDataRowApprovedSew As System.Data.DataRow = Me.ogvApprovedSewingInfo.GetDataRow(nLoopApprovedSew)

                                        If oDataRowApprovedSew.Item("FTOrderNo").ToString = tTextFTOrderNo Then

                                            With Me.ogvApprovedSewingInfo
                                                .SetRowCellValue(nLoopApprovedSew, .Columns.ColumnByFieldName("FTStateApprovedSewing"), tTextFTChkApprovedOrderNo)
                                            End With

                                        End If

                                    Next nLoopApprovedSew

                                    CType(.DataSource, System.Data.DataTable).AcceptChanges()

                                End If

                            End With

                            '...approved factory sub order no. packing
                            With Me.ogdApprovedPackingInfo

                                If Not .DataSource Is Nothing And Me.ogvApprovedPackingInfo.RowCount > 0 Then

                                    For nLoopApprovedPack As Integer = 0 To Me.ogvApprovedPackingInfo.DataRowCount - 1

                                        Dim oDataRowApprovedPack As System.Data.DataRow = Me.ogvApprovedPackingInfo.GetDataRow(nLoopApprovedPack)

                                        If oDataRowApprovedPack.Item("FTOrderNo").ToString = tTextFTOrderNo Then

                                            With Me.ogvApprovedPackingInfo
                                                .SetRowCellValue(nLoopApprovedPack, .Columns.ColumnByFieldName("FTStateApprovedPacking"), tTextFTChkApprovedOrderNo)
                                            End With

                                        End If

                                    Next nLoopApprovedPack

                                End If

                            End With

                            '...approved factory sub order no. pack carton ratio
                            With Me.ogdApprovedPackRatioInfo

                                If Not .DataSource Is Nothing And Me.ogvApprovedPackRatioInfo.RowCount > 0 Then

                                    For nLoopApprovedPackRatio As Integer = 0 To Me.ogvApprovedPackRatioInfo.DataRowCount - 1

                                        Dim oDataRowApprovedPackRatio As System.Data.DataRow = Me.ogvApprovedPackRatioInfo.GetDataRow(nLoopApprovedPackRatio)

                                        If oDataRowApprovedPackRatio.Item("FTOrderNo").ToString = tTextFTOrderNo Then

                                            With Me.ogvApprovedPackRatioInfo
                                                .SetRowCellValue(nLoopApprovedPackRatio, .Columns.ColumnByFieldName("FTStateApprovedPackRatio"), tTextFTChkApprovedOrderNo)
                                            End With

                                        End If

                                    Next nLoopApprovedPackRatio

                                    CType(.DataSource, System.Data.DataTable).AcceptChanges()

                                End If

                            End With

                            '...approved factory sub order no. size spec
                            With Me.ogdApprovedSizeSpecInfo

                                If Not .DataSource Is Nothing And Me.ogvApprovedSizeSpecInfo.RowCount > 0 Then

                                    For nLoopApprovedSizeSpec As Integer = 0 To Me.ogvApprovedSizeSpecInfo.DataRowCount - 1

                                        Dim oDataRowSizeSpec As System.Data.DataRow = Me.ogvApprovedSizeSpecInfo.GetDataRow(nLoopApprovedSizeSpec)

                                        If oDataRowSizeSpec.Item("FTOrderNo").ToString = tTextFTOrderNo Then

                                            With Me.ogvApprovedSizeSpecInfo
                                                .SetRowCellValue(nLoopApprovedSizeSpec, .Columns.ColumnByFieldName("FTStateApprovedSizeSpec"), tTextFTChkApprovedOrderNo)
                                            End With

                                        End If

                                    Next

                                    CType(.DataSource, System.Data.DataTable).AcceptChanges()

                                End If

                            End With

                        Next nLoopApprovedAll

                    End If

                    If Not DTApprovedOrderNoSubAllInfo Is Nothing AndAlso DTApprovedOrderNoSubAllInfo.Rows.Count > 0 Then
                        '...approved all information by factory order no and sub order no.

                        For nLoopApprovedOrdeNoSubAll As Integer = 0 To DTApprovedOrderNoSubAllInfo.Rows.Count - 1

                            Dim tTextFTOrderNo As String
                            Dim tTextFTSubOrderNo As String
                            Dim tTextFTChkApprovedOrderSubAll As String

                            tTextFTOrderNo = DTApprovedOrderNoSubAllInfo.Rows(nLoopApprovedOrdeNoSubAll)("FTApprovedOrderNoSubAllInfo").ToString
                            tTextFTSubOrderNo = DTApprovedOrderNoSubAllInfo.Rows(nLoopApprovedOrdeNoSubAll)("FTApprovedSubOrderNoAllInfo").ToString
                            tTextFTChkApprovedOrderSubAll = DTApprovedOrderNoSubAllInfo.Rows(nLoopApprovedOrdeNoSubAll)("FTChkStateSubOrderNoAllInfo").ToString

                            '...approved factory sub order no component.
                            With Me.ogdApprovedComponentInfo

                                If Not .DataSource Is Nothing And ogvApprovedComponentInfo.RowCount > 0 Then

                                    For nLoopApprovedComponent As Integer = 0 To Me.ogvApprovedComponentInfo.DataRowCount - 1

                                        Dim oDataRowApprovedComponent As System.Data.DataRow = Me.ogvApprovedComponentInfo.GetDataRow(nLoopApprovedComponent)

                                        If oDataRowApprovedComponent.Item("FTOrderNo").ToString = tTextFTOrderNo AndAlso oDataRowApprovedComponent.Item("FTSubOrderNo").ToString = tTextFTSubOrderNo Then

                                            With Me.ogvApprovedComponentInfo
                                                .SetRowCellValue(nLoopApprovedComponent, .Columns.ColumnByFieldName("FTStateApprovedComponent"), tTextFTChkApprovedOrderSubAll)
                                            End With

                                        End If

                                    Next nLoopApprovedComponent

                                    CType(.DataSource, System.Data.DataTable).AcceptChanges()

                                End If

                            End With

                            '...approved factory sub order no. sewing
                            With Me.ogdApprovedSewingInfo

                                If Not .DataSource Is Nothing And Me.ogvApprovedSewingInfo.RowCount > 0 Then

                                    For nLoopApprovedSew As Integer = 0 To Me.ogvApprovedSewingInfo.DataRowCount - 1

                                        Dim oDataRowApprovedSew As System.Data.DataRow = Me.ogvApprovedSewingInfo.GetDataRow(nLoopApprovedSew)

                                        If oDataRowApprovedSew.Item("FTOrderNo").ToString = tTextFTOrderNo AndAlso oDataRowApprovedSew.Item("FTSubOrderNo").ToString = tTextFTSubOrderNo Then

                                            With Me.ogvApprovedSewingInfo
                                                .SetRowCellValue(nLoopApprovedSew, .Columns.ColumnByFieldName("FTStateApprovedSewing"), tTextFTChkApprovedOrderSubAll)
                                            End With

                                        End If

                                    Next nLoopApprovedSew

                                    CType(.DataSource, System.Data.DataTable).AcceptChanges()

                                End If

                            End With

                            '...approved factory sub order no. packing
                            With Me.ogdApprovedPackingInfo

                                If Not .DataSource Is Nothing And Me.ogvApprovedPackingInfo.RowCount > 0 Then

                                    For nLoopApprovedPack As Integer = 0 To Me.ogvApprovedPackingInfo.DataRowCount - 1

                                        Dim oDataRowApprovedPack As System.Data.DataRow = Me.ogvApprovedPackingInfo.GetDataRow(nLoopApprovedPack)

                                        If oDataRowApprovedPack.Item("FTOrderNo").ToString = tTextFTOrderNo AndAlso oDataRowApprovedPack.Item("FTSubOrderNo").ToString = tTextFTSubOrderNo Then

                                            With Me.ogvApprovedPackingInfo
                                                .SetRowCellValue(nLoopApprovedPack, .Columns.ColumnByFieldName("FTStateApprovedPacking"), tTextFTChkApprovedOrderSubAll)
                                            End With

                                        End If

                                    Next nLoopApprovedPack

                                End If

                            End With

                            '...approved factory sub order no. pack carton ratio
                            With Me.ogdApprovedPackRatioInfo

                                If Not .DataSource Is Nothing And Me.ogvApprovedPackRatioInfo.RowCount > 0 Then

                                    For nLoopApprovedPackRatio As Integer = 0 To Me.ogvApprovedPackRatioInfo.DataRowCount - 1

                                        Dim oDataRowApprovedPackRatio As System.Data.DataRow = Me.ogvApprovedPackRatioInfo.GetDataRow(nLoopApprovedPackRatio)

                                        If oDataRowApprovedPackRatio.Item("FTOrderNo").ToString = tTextFTOrderNo AndAlso oDataRowApprovedPackRatio.Item("FTSubOrderNo").ToString = tTextFTSubOrderNo Then

                                            With Me.ogvApprovedPackRatioInfo
                                                .SetRowCellValue(nLoopApprovedPackRatio, .Columns.ColumnByFieldName("FTStateApprovedPackRatio"), tTextFTChkApprovedOrderSubAll)
                                            End With

                                        End If

                                    Next nLoopApprovedPackRatio

                                    CType(.DataSource, System.Data.DataTable).AcceptChanges()

                                End If

                            End With

                            '...approved factory sub order no. size spec
                            With Me.ogdApprovedSizeSpecInfo

                                If Not .DataSource Is Nothing And Me.ogvApprovedSizeSpecInfo.RowCount > 0 Then

                                    For nLoopApprovedSizeSpec As Integer = 0 To Me.ogvApprovedSizeSpecInfo.DataRowCount - 1

                                        Dim oDataRowSizeSpec As System.Data.DataRow = Me.ogvApprovedSizeSpecInfo.GetDataRow(nLoopApprovedSizeSpec)

                                        If oDataRowSizeSpec.Item("FTOrderNo").ToString = tTextFTOrderNo AndAlso oDataRowSizeSpec.Item("FTSubOrderNo").ToString = tTextFTSubOrderNo Then

                                            With Me.ogvApprovedSizeSpecInfo
                                                .SetRowCellValue(nLoopApprovedSizeSpec, .Columns.ColumnByFieldName("FTStateApprovedSizeSpec"), tTextFTChkApprovedOrderSubAll)
                                            End With

                                        End If

                                    Next

                                    CType(.DataSource, System.Data.DataTable).AcceptChanges()

                                End If

                            End With

                        Next nLoopApprovedOrdeNoSubAll

                    End If

                End If

                If Not DTApprovedOrderNoAllInfo Is Nothing Then DTApprovedOrderNoAllInfo.Dispose()
                If Not DTApprovedOrderNoSubAllInfo Is Nothing Then DTApprovedOrderNoSubAllInfo.Dispose()

            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If
        End Try

    End Sub

    Private Sub oRepositoryItemCheckEdit_EditValueChanged(sender As Object, e As EventArgs) Handles oRepositoryItemCheckEdit.EditValueChanged

        Try
            If Me.ogvApprovedOrderInfo.RowCount > 0 Then

                If Me.oTabApprovedInfo.SelectedTabPageIndex = eApprovedTabIndex.ApprovedOrderNo Then

                    Dim bChkApprovedOrderNoAll As String

                    bChkApprovedOrderNoAll = "0"

                    If Me.oChkApproveAllInfo.Checked = True Then
                        bChkApprovedOrderNoAll = "1"
                    End If

                    If bChkApprovedOrderNoAll = "1" Then

                        Me.ogvApprovedOrderInfo.PostEditor()

                        Dim tTextFTOrderNo As String
                        Dim bChkStrApprovedOrderNo As String

                        tTextFTOrderNo = ""
                        bChkStrApprovedOrderNo = ""

                        tTextFTOrderNo = Me.ogvApprovedOrderInfo.GetRowCellValue(Me.ogvApprovedOrderInfo.FocusedRowHandle, Me.ogvApprovedOrderInfo.Columns.ColumnByFieldName("FTOrderNo")).ToString
                        bChkStrApprovedOrderNo = Me.ogvApprovedOrderInfo.GetDataRow(Me.ogvApprovedOrderInfo.FocusedRowHandle)(Me.ogvApprovedOrderInfo.FocusedColumn.FieldName).ToString

                        If bChkStrApprovedOrderNo = "" Then bChkStrApprovedOrderNo = "0"

                        If System.Diagnostics.Debugger.IsAttached = True Then
                            MsgBox(String.Format("Factory Order No. : {0} Value Checked is : {1}", {tTextFTOrderNo, bChkStrApprovedOrderNo}), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                        End If

                        '...approved sub order no. information
                        With Me.ogdApprovedSubOrderNoInfo

                            If Not .DataSource Is Nothing AndAlso Me.ogvApprovedSubOrderNoInfo.RowCount > 0 Then
                                '...approved all by factory order no.

                                For nLoopApprovedSubOrder As Integer = 0 To Me.ogvApprovedSubOrderNoInfo.DataRowCount - 1

                                    Dim oDataRowApprovedSubOrder As System.Data.DataRow = Me.ogvApprovedSubOrderNoInfo.GetDataRow(nLoopApprovedSubOrder)

                                    With Me.ogvApprovedSubOrderNoInfo
                                        If (oDataRowApprovedSubOrder.Item(.Columns.ColumnByFieldName("FTOrderNo").FieldName)).Equals(tTextFTOrderNo) Then
                                            .SetRowCellValue(nLoopApprovedSubOrder, .Columns.ColumnByFieldName("FTStateApprovedSubOrderNo"), bChkStrApprovedOrderNo)
                                        End If

                                    End With

                                Next nLoopApprovedSubOrder

                            End If

                            CType(.DataSource, System.Data.DataTable).AcceptChanges()

                        End With

                        '...approved sub order no. component information
                        With Me.ogdApprovedComponentInfo

                            If Not .DataSource Is Nothing AndAlso Me.ogvApprovedComponentInfo.RowCount > 0 Then
                                '...approved all by factory order no.

                                For nLoopApprovedComponent As Integer = 0 To Me.ogvApprovedComponentInfo.DataRowCount - 1

                                    Dim oDataRowComponent As System.Data.DataRow = Me.ogvApprovedComponentInfo.GetDataRow(nLoopApprovedComponent)

                                    With Me.ogvApprovedComponentInfo
                                        If oDataRowComponent.Item(.Columns.ColumnByFieldName("FTOrderNo").FieldName).Equals(tTextFTOrderNo) Then
                                            .SetRowCellValue(nLoopApprovedComponent, .Columns.ColumnByFieldName("FTStateApprovedComponent"), bChkStrApprovedOrderNo)
                                        End If

                                    End With

                                Next nLoopApprovedComponent

                            End If

                            CType(.DataSource, System.Data.DataTable).AcceptChanges()

                        End With

                        '...approved sub order no. sewing information
                        With Me.ogdApprovedSewingInfo

                            If Not .DataSource Is Nothing AndAlso Me.ogvApprovedSewingInfo.RowCount > 0 Then
                                '...approved all by factory order no.

                                For nLoopApprovedSew As Integer = 0 To Me.ogvApprovedSewingInfo.DataRowCount - 1

                                    Dim oDataRowSew As System.Data.DataRow = Me.ogvApprovedSewingInfo.GetDataRow(nLoopApprovedSew)

                                    With Me.ogvApprovedSewingInfo
                                        If oDataRowSew.Item(.Columns.ColumnByFieldName("FTOrderNo").FieldName).Equals(tTextFTOrderNo) Then
                                            .SetRowCellValue(nLoopApprovedSew, .Columns.ColumnByFieldName("FTStateApprovedSewing"), bChkStrApprovedOrderNo)
                                        End If

                                    End With

                                Next nLoopApprovedSew

                            End If

                            CType(.DataSource, System.Data.DataTable).AcceptChanges()

                        End With

                        '...approved sub order no. packing information
                        With Me.ogdApprovedPackingInfo

                            If Not .DataSource Is Nothing AndAlso Me.ogvApprovedPackingInfo.RowCount > 0 Then
                                '...approved all by factory order no.

                                For nLoopApprovedPack As Integer = 0 To Me.ogvApprovedPackingInfo.DataRowCount - 1

                                    Dim oDataRowPack As System.Data.DataRow = Me.ogvApprovedPackingInfo.GetDataRow(nLoopApprovedPack)

                                    With Me.ogvApprovedPackingInfo
                                        If oDataRowPack.Item(.Columns.ColumnByFieldName("FTOrderNo").FieldName).Equals(tTextFTOrderNo) Then
                                            .SetRowCellValue(nLoopApprovedPack, .Columns.ColumnByFieldName("FTStateApprovedPacking"), bChkStrApprovedOrderNo)
                                        End If

                                    End With

                                Next nLoopApprovedPack

                            End If

                            CType(.DataSource, System.Data.DataTable).AcceptChanges()

                        End With

                        '...approved sub order no pack carton information
                        With Me.ogdApprovedPackRatioInfo

                            If Not .DataSource Is Nothing AndAlso Me.ogvApprovedPackRatioInfo.RowCount > 0 Then
                                '...approved all by factory order no.

                                For nLoopApprovePackCarton As Integer = 0 To Me.ogvApprovedPackRatioInfo.DataRowCount - 1

                                    Dim oDataRowPackRatio As System.Data.DataRow = Me.ogvApprovedPackRatioInfo.GetDataRow(nLoopApprovePackCarton)

                                    With Me.ogvApprovedPackRatioInfo
                                        If oDataRowPackRatio.Item(.Columns.ColumnByFieldName("FTOrderNo").FieldName).Equals(tTextFTOrderNo) Then
                                            .SetRowCellValue(nLoopApprovePackCarton, .Columns.ColumnByFieldName("FTStateApprovedPackRatio"), bChkStrApprovedOrderNo)
                                        End If

                                    End With

                                Next nLoopApprovePackCarton

                            End If

                            CType(.DataSource, System.Data.DataTable).AcceptChanges()

                        End With

                        '...approved sub order no sizespcec information
                        With Me.ogdApprovedSizeSpecInfo

                            If Not .DataSource Is Nothing AndAlso Me.ogvApprovedSizeSpecInfo.RowCount > 0 Then
                                '...approved all by factory order no.

                                For nLoopApprovedSizeSpec As Integer = 0 To Me.ogvApprovedSizeSpecInfo.DataRowCount - 1

                                    Dim oDataRowSizeSpec As System.Data.DataRow = Me.ogvApprovedSizeSpecInfo.GetDataRow(nLoopApprovedSizeSpec)

                                    With Me.ogvApprovedSizeSpecInfo
                                        If oDataRowSizeSpec.Item(.Columns.ColumnByFieldName("FTOrderNo").FieldName).Equals(tTextFTOrderNo) Then
                                            .SetRowCellValue(nLoopApprovedSizeSpec, .Columns.ColumnByFieldName("FTStateApprovedSizeSpec"), bChkStrApprovedOrderNo)
                                        End If
                                    End With

                                Next nLoopApprovedSizeSpec

                            End If

                            CType(.DataSource, System.Data.DataTable).AcceptChanges()

                        End With

                    End If

                End If

            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If
        End Try

    End Sub

    Private Sub oTabApprovedInfo_SelectedPageChanged(sender As Object, e As XtraTab.TabPageChangedEventArgs) Handles oTabApprovedInfo.SelectedPageChanged
        Try
            REM 2015/03/10
            'bFlagTabInfoChange = False

            'With Me.oTabApprovedInfo

            '    If Not .SelectedTabPageIndex.Equals(eApprovedTabIndex.ApprovedOrderNo) Then

            '        bFlagTabInfoChange = True

            '        'If System.Diagnostics.Debugger.IsAttached = True Then
            '        '    MsgBox(.TabPages(.SelectedTabPageIndex).TabControl.TabPages.Item(.SelectedTabPageIndex).Name, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            '        'End If

            '        Me.oChkApproveAllRecord.Checked = False

            '    End If

            '    bFlagTabInfoChange = False

            'End With

            Select Case Me.oTabApprovedInfo.SelectedTabPageIndex
                Case eApprovedTabIndex.ApprovedOrderNo
                    bFlagTabInfoChange = True : Me.oChkApproveAllInfo.Enabled = True ': Me.oChkApproveAllInfo.Checked = False
                Case eApprovedTabIndex.ApprovedSubOrderNo
                    bFlagTabInfoChange = True : Me.oChkApproveAllInfo.Enabled = False ': Me.oChkApproveAllInfo.Checked = False
                Case eApprovedTabIndex.ApprovedComponent
                    Me.oChkApproveAllInfo.Enabled = False
                    bFlagTabInfoChange = True ': Me.oChkApproveAllInfo.Checked = False
                Case eApprovedTabIndex.ApprovedSewing
                    Me.oChkApproveAllInfo.Enabled = False
                    bFlagTabInfoChange = True ': Me.oChkApproveAllInfo.Checked = False
                Case eApprovedTabIndex.ApprovedPacking
                    Me.oChkApproveAllInfo.Enabled = False
                    bFlagTabInfoChange = True ': Me.oChkApproveAllInfo.Checked = False
                Case eApprovedTabIndex.ApprovedPackingCarton
                    Me.oChkApproveAllInfo.Enabled = False
                    bFlagTabInfoChange = True ': Me.oChkApproveAllInfo.Checked = False
                Case eApprovedTabIndex.ApprovedSizeSpec
                    Me.oChkApproveAllInfo.Enabled = False
                    bFlagTabInfoChange = True ': Me.oChkApproveAllInfo.Checked = False
                Case Else
                    '...nothing
            End Select

            Me.oChkApproveAllRecord.Checked = False

            bFlagTabInfoChange = False

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, My.Application.Info.Title)
            End If
        End Try

    End Sub

    Private Sub oRepositoryItemCheckEdit2_EditValueChanged(sender As Object, e As EventArgs) Handles oRepositoryItemCheckEdit2.EditValueChanged
        Try
            If Me.ogvApprovedSubOrderNoInfo.RowCount > 0 Then

                If Me.oTabApprovedInfo.SelectedTabPageIndex = eApprovedTabIndex.ApprovedSubOrderNo Then

                    Dim bChkApprovedOrderNoAll As String

                    bChkApprovedOrderNoAll = "0"

                    If Me.oChkApproveAllInfo.Checked = True Then
                        bChkApprovedOrderNoAll = "1"
                    End If

                    If bChkApprovedOrderNoAll = "1" Then

                        Me.ogvApprovedSubOrderNoInfo.PostEditor()

                        Dim tTextFTOrderNo As String
                        Dim tTextFTSubOrderNo As String
                        Dim bChkStrApprovedOrderNoSub As String

                        tTextFTOrderNo = ""
                        bChkStrApprovedOrderNoSub = ""

                        tTextFTOrderNo = Me.ogvApprovedSubOrderNoInfo.GetRowCellValue(Me.ogvApprovedSubOrderNoInfo.FocusedRowHandle, Me.ogvApprovedSubOrderNoInfo.Columns.ColumnByFieldName("FTOrderNo")).ToString
                        tTextFTSubOrderNo = Me.ogvApprovedSubOrderNoInfo.GetRowCellValue(Me.ogvApprovedSubOrderNoInfo.FocusedRowHandle, Me.ogvApprovedSubOrderNoInfo.Columns.ColumnByFieldName("FTSubOrderNo")).ToString
                        bChkStrApprovedOrderNoSub = Me.ogvApprovedSubOrderNoInfo.GetDataRow(Me.ogvApprovedSubOrderNoInfo.FocusedRowHandle)(Me.ogvApprovedSubOrderNoInfo.FocusedColumn.FieldName).ToString

                        If bChkStrApprovedOrderNoSub = "" Then bChkStrApprovedOrderNoSub = "0"

                        If System.Diagnostics.Debugger.IsAttached = True Then
                            MsgBox(String.Format("Factory Order No. , Factory Sub Order No. : {0} || {1} Value Checked is : {2}", {tTextFTOrderNo, tTextFTSubOrderNo, bChkStrApprovedOrderNoSub}), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
                        End If

                        '...approved sub order no. component information
                        With Me.ogdApprovedComponentInfo

                            If Not .DataSource Is Nothing AndAlso Me.ogvApprovedComponentInfo.RowCount > 0 Then
                                '...approved all by factory order no.

                                For nLoopApprovedComponent As Integer = 0 To Me.ogvApprovedComponentInfo.DataRowCount - 1

                                    Dim oDataRowComponent As System.Data.DataRow = Me.ogvApprovedComponentInfo.GetDataRow(nLoopApprovedComponent)

                                    With Me.ogvApprovedComponentInfo
                                        If oDataRowComponent.Item(.Columns.ColumnByFieldName("FTOrderNo").FieldName).Equals(tTextFTOrderNo) AndAlso oDataRowComponent.Item(.Columns.ColumnByFieldName("FTSubOrderNo").FieldName).Equals(tTextFTSubOrderNo) Then
                                            .SetRowCellValue(nLoopApprovedComponent, .Columns.ColumnByFieldName("FTStateApprovedComponent"), bChkStrApprovedOrderNoSub)
                                        End If

                                    End With

                                Next nLoopApprovedComponent

                            End If

                            CType(.DataSource, System.Data.DataTable).AcceptChanges()

                        End With

                        '...approved sub order no. sewing information
                        With Me.ogdApprovedSewingInfo

                            If Not .DataSource Is Nothing AndAlso Me.ogvApprovedSewingInfo.RowCount > 0 Then
                                '...approved all by factory order no.

                                For nLoopApprovedSew As Integer = 0 To Me.ogvApprovedSewingInfo.DataRowCount - 1

                                    Dim oDataRowSew As System.Data.DataRow = Me.ogvApprovedSewingInfo.GetDataRow(nLoopApprovedSew)

                                    With Me.ogvApprovedSewingInfo
                                        If oDataRowSew.Item(.Columns.ColumnByFieldName("FTOrderNo").FieldName).Equals(tTextFTOrderNo) AndAlso oDataRowSew.Item(.Columns.ColumnByFieldName("FTSubOrderNo").FieldName).Equals(tTextFTSubOrderNo) Then
                                            .SetRowCellValue(nLoopApprovedSew, .Columns.ColumnByFieldName("FTStateApprovedSewing"), bChkStrApprovedOrderNoSub)
                                        End If

                                    End With

                                Next nLoopApprovedSew

                            End If

                            CType(.DataSource, System.Data.DataTable).AcceptChanges()

                        End With

                        '...approved sub order no. packing information
                        With Me.ogdApprovedPackingInfo

                            If Not .DataSource Is Nothing AndAlso Me.ogvApprovedPackingInfo.RowCount > 0 Then
                                '...approved all by factory order no.

                                For nLoopApprovedPack As Integer = 0 To Me.ogvApprovedPackingInfo.DataRowCount - 1

                                    Dim oDataRowPack As System.Data.DataRow = Me.ogvApprovedPackingInfo.GetDataRow(nLoopApprovedPack)

                                    With Me.ogvApprovedPackingInfo
                                        If oDataRowPack.Item(.Columns.ColumnByFieldName("FTOrderNo").FieldName).Equals(tTextFTOrderNo) AndAlso oDataRowPack.Item(.Columns.ColumnByFieldName("FTSubOrderNo").FieldName).Equals(tTextFTSubOrderNo) Then
                                            .SetRowCellValue(nLoopApprovedPack, .Columns.ColumnByFieldName("FTStateApprovedPacking"), bChkStrApprovedOrderNoSub)
                                        End If

                                    End With

                                Next nLoopApprovedPack

                            End If

                            CType(.DataSource, System.Data.DataTable).AcceptChanges()

                        End With

                        '...approved sub order no pack carton information
                        With Me.ogdApprovedPackRatioInfo

                            If Not .DataSource Is Nothing AndAlso Me.ogvApprovedPackRatioInfo.RowCount > 0 Then
                                '...approved all by factory order no.

                                For nLoopApprovePackCarton As Integer = 0 To Me.ogvApprovedPackRatioInfo.DataRowCount - 1

                                    Dim oDataRowPackRatio As System.Data.DataRow = Me.ogvApprovedPackRatioInfo.GetDataRow(nLoopApprovePackCarton)

                                    With Me.ogvApprovedPackRatioInfo
                                        If oDataRowPackRatio.Item(.Columns.ColumnByFieldName("FTOrderNo").FieldName).Equals(tTextFTOrderNo) AndAlso oDataRowPackRatio.Item(.Columns.ColumnByFieldName("FTSubOrderNo").FieldName).Equals(tTextFTSubOrderNo) Then
                                            .SetRowCellValue(nLoopApprovePackCarton, .Columns.ColumnByFieldName("FTStateApprovedPackRatio"), bChkStrApprovedOrderNoSub)
                                        End If

                                    End With

                                Next nLoopApprovePackCarton

                            End If

                            CType(.DataSource, System.Data.DataTable).AcceptChanges()

                        End With

                        '...approved sub order no sizespcec information
                        With Me.ogdApprovedSizeSpecInfo

                            If Not .DataSource Is Nothing AndAlso Me.ogvApprovedSizeSpecInfo.RowCount > 0 Then
                                '...approved all by factory order no.

                                For nLoopApprovedSizeSpec As Integer = 0 To Me.ogvApprovedSizeSpecInfo.DataRowCount - 1

                                    Dim oDataRowSizeSpec As System.Data.DataRow = Me.ogvApprovedSizeSpecInfo.GetDataRow(nLoopApprovedSizeSpec)

                                    With Me.ogvApprovedSizeSpecInfo
                                        If oDataRowSizeSpec.Item(.Columns.ColumnByFieldName("FTOrderNo").FieldName).Equals(tTextFTOrderNo) AndAlso oDataRowSizeSpec.Item(.Columns.ColumnByFieldName("FTSubOrderNo").FieldName).Equals(tTextFTSubOrderNo) Then
                                            .SetRowCellValue(nLoopApprovedSizeSpec, .Columns.ColumnByFieldName("FTStateApprovedSizeSpec"), bChkStrApprovedOrderNoSub)
                                        End If
                                    End With

                                Next nLoopApprovedSizeSpec

                            End If

                            CType(.DataSource, System.Data.DataTable).AcceptChanges()

                        End With

                    End If

                End If

            End If

        Catch ex As Exception
            If System.Diagnostics.Debugger.IsAttached = True Then
                MsgBox(ex.Message().ToString & Environment.NewLine & ex.StackTrace().ToString, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, My.Application.Info.Title)
            End If
        End Try

    End Sub


End Class