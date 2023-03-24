Imports System.Threading

Public Class wIssueApproveReceive

    Private _StateCheckWaitting As Boolean
    Sub New()

        _ProcPrepare = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Dim _Qry As String = ""

        _Qry = " SELECT TOP 1 FTCfgData"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS Z WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE  (FTCfgName = N'LeavePragNentPayMergeSundayHoloday')"

        LeavePragNentPayMergeSundayHoloday = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")


        _ProcPrepare = False

    End Sub

#Region "Initial Grid"

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        'Dim sFieldCount As String = "FTEmpCode"

        'With ogv
        '    .ClearGrouping()
        '    .ClearDocument()

        '    For Each Str As String In sFieldCount.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
        '        End If
        '    Next

        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        '    .OptionsView.ShowFooter = True
        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
        '    .OptionsView.ShowGroupPanel = True
        '    .ExpandAllGroups()
        '    .RefreshData()

        'End With
        '------End Add Summary Grid-------------
    End Sub

#End Region

    Private _ProcPrepare As Boolean = False

    Private _Actualdate As String = ""
    ReadOnly Property Actualdate As String
        Get
            Return _Actualdate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property

#Region "Property"

    Private _LeavePragNentPayMergeSundayHoloday As String = ""
    Public Property LeavePragNentPayMergeSundayHoloday As String
        Get
            Return _LeavePragNentPayMergeSundayHoloday
        End Get
        Set(value As String)
            _LeavePragNentPayMergeSundayHoloday = value
        End Set
    End Property

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

#End Region

#Region "MAIN PROC"

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Procedure "

#End Region

#Region "General"

#End Region

    Private Delegate Sub DelegateCheckWaiting()
    Private Sub CheckWaiting()
        If Me.InvokeRequired Then
            Me.Invoke(New DelegateCheckWaiting(AddressOf CheckWaiting), New Object() {})
        Else

            Dim _Qry As String = ""
            Dim _dt As DataTable

            _Qry = "   Select  '0' as FTSelect "
            _Qry &= vbCrLf & "  ,A.FTIssueNo"
            _Qry &= vbCrLf & "  , A.FDIssueDate"
            _Qry &= vbCrLf & "  , A.FTIssueBy"
            _Qry &= vbCrLf & "  , A.FTOrderNo"
            _Qry &= vbCrLf & "  , A.FNHSysCmpId"
            _Qry &= vbCrLf & "  , A.FNHSysWHCmpId"
            _Qry &= vbCrLf & "  , A.FNHSysRawMatId"
            _Qry &= vbCrLf & "  ,IM.FTRawMatCode "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "  ,IM.FTRawMatNameTH AS FTRawMatName "
            Else
                _Qry &= vbCrLf & "  ,IM.FTRawMatNameEN AS FTRawMatName"
            End If

            _Qry &= vbCrLf & "  ,IMC.FTRawMatColorCode  "
            _Qry &= vbCrLf & "  ,IMS.FTRawMatSizeCode "
            _Qry &= vbCrLf & "  ,IMU.FTUnitCode  "
            _Qry &= vbCrLf & "  , A.FNQuantity "
            _Qry &= vbCrLf & "  FROM( Select H.FTIssueNo "
            _Qry &= vbCrLf & "  , H.FDIssueDate "
            _Qry &= vbCrLf & "  , H.FTIssueBy "
            _Qry &= vbCrLf & "  , H.FTOrderNo "
            _Qry &= vbCrLf & "  , H.FNHSysCmpId "
            _Qry &= vbCrLf & "  , WH.FNHSysCmpId As FNHSysWHCmpId "
            _Qry &= vbCrLf & "  , BB.FNHSysRawMatId "
            _Qry &= vbCrLf & "  , SUM(B.FNQuantity) As FNQuantity "
            _Qry &= vbCrLf & "  From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As BB With(NOLOCK) INNER Join "
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT As B With(NOLOCK) On BB.FTBarcodeNo = B.FTBarcodeNo INNER Join "
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue As H With(NOLOCK) On B.FTDocumentNo = H.FTIssueNo INNER Join "
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse As WH With(NOLOCK) On H.FNHSysWHId = WH.FNHSysWHId"

            _Qry &= vbCrLf & " WHERE H.FNHSysCmpId <> WH.FNHSysCmpId "
            _Qry &= vbCrLf & "  And WH.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID) & ""
            _Qry &= vbCrLf & "  And  H.FNHSysCmpId <> " & Val(HI.ST.SysInfo.CmpID) & ""
            _Qry &= vbCrLf & "        And ISNULL(H.FTStateSendApp,'')='1'"
            _Qry &= vbCrLf & "   And ISNULL(B.FTSateApp,'0') <>'1' "

            _Qry &= vbCrLf & "  Group By H.FTIssueNo "
            _Qry &= vbCrLf & "  , H.FDIssueDate "
            _Qry &= vbCrLf & "  , H.FTIssueBy "
            _Qry &= vbCrLf & "  , H.FTOrderNo "
            _Qry &= vbCrLf & "  , H.FNHSysCmpId "
            _Qry &= vbCrLf & "  , WH.FNHSysCmpId "
            _Qry &= vbCrLf & "  , BB.FNHSysRawMatId ) As A INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM With(NOLOCK) On A.FNHSysRawMatId = IM.FNHSysRawMatId "
            _Qry &= vbCrLf & "  Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor As IMC With(NOLOCK) On IM.FNHSysRawMatColorId = IMC.FNHSysRawMatColorId "
            _Qry &= vbCrLf & "  Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize As IMS With(NOLOCK) On IM.FNHSysRawMatSizeId = IMS.FNHSysRawMatSizeId "
            _Qry &= vbCrLf & "  Left OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit As IMU With(NOLOCK) On IM.FNHSysUnitId = IMU.FNHSysUnitId "
            _Qry &= vbCrLf & "  ORDER BY A.FTIssueNo		"
            _Qry &= vbCrLf & "  ,IM.FTRawMatCode "
            _Qry &= vbCrLf & " ,IMC.FTRawMatColorCode "
            _Qry &= vbCrLf & "   ,IMS.FTRawMatSizeCode "

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)


            If Me.ogc.DataSource Is Nothing Then
                Me.ogc.DataSource = _dt.Copy
            Else

                Dim dtmain As DataTable

                With CType(Me.ogc.DataSource, DataTable)
                    .AcceptChanges()
                    dtmain = .Copy()
                End With

                Try
                    Dim dt4 As DataTable = _dt.[Select]().Where(Function(x) Not dtmain.[Select](String.Format("FTIssueNo = '{0}' AND FNHSysRawMatId = {1} ", {x("FTIssueNo"), x("FNHSysRawMatId")})).Any()).CopyToDataTable()
                    dtmain.Merge(dt4.Copy)

                Catch ex As Exception
                End Try

                Me.ogc.DataSource = dtmain.Copy

            End If

            _StateCheckWaitting = True

        End If
    End Sub

    Private Sub wEmployeeLeaveNotAppTracking_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ocmcheckwaiting.Enabled = False
    End Sub

    Private Sub wLeave_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _StateCheckWaitting = False
        ocmcheckwaiting.Enabled = False
        Dim _Theard As New Thread(AddressOf CheckWaiting)
        _Theard.Start()
        InitGrid()
        ocmcheckwaiting.Enabled = True
        ' Me.ocmcheckwaiting.Enabled = True
    End Sub

    Private Sub ocmcheckwaiting_Tick(sender As Object, e As EventArgs) Handles ocmcheckwaiting.Tick
        If (_StateCheckWaitting) Then
            _StateCheckWaitting = False
            Dim _Theard As New Thread(AddressOf CheckWaiting)
            _Theard.Start()
        End If
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            ocmcheckwaiting.Enabled = False
            Me.ogc.DataSource = Nothing
            _StateCheckWaitting = False
            CheckWaiting()
            ocmcheckwaiting.Enabled = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmapprove_Click(sender As Object, e As EventArgs) Handles ocmapprove.Click
        ocmcheckwaiting.Enabled = False
        Try
            Dim _oDt As DataTable
            Dim _Cmd As String = ""
            With DirectCast(Me.ogc.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTSelect ='1'").Length <= 0 Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text)
                    Exit Sub
                End If

                If Not HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, " อนุมัติรับกานจ่ายวัตถุดิบ.....") Then
                    Exit Sub
                End If


                For Each R As DataRow In .Select("FTSelect ='1'")

                    _Cmd = " Update A SET "
                    _Cmd &= vbCrLf & "  FTSateApp='1'"
                    _Cmd &= vbCrLf & ", FTSateAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Cmd &= vbCrLf & ", FTSateAppDate=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ", FTSateAppTime=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS A"
                    _Cmd &= vbCrLf & "  INNER JOIN ("
                    _Cmd &= vbCrLf & "         SELECT X.FTBarcodeNo "
                    _Cmd &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT As X With(NOLOCK)  "
                    _Cmd &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS XB With(NOLOCK)  ON X.FTBarcodeNo=XB.FTBarcodeNo "
                    _Cmd &= vbCrLf & "  WHERE X.FTDocumentNo='" & HI.UL.ULF.rpQuoted(R!FTIssueNo.ToString) & "'"
                    _Cmd &= vbCrLf & "  AND XB.FNHSysRawMatId=" & Val(R!FNHSysRawMatId.ToString) & ""
                    _Cmd &= vbCrLf & ""
                    _Cmd &= vbCrLf & " ) As B ON A.FTBarcodeNo = B.FTBarcodeNo "
                    _Cmd &= vbCrLf & " WHERE A.FTDocumentNo='" & HI.UL.ULF.rpQuoted(R!FTIssueNo.ToString) & "'"

                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_INVEN)

                Next

            End With


            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Me.ogc.DataSource = Nothing
            CheckWaiting()

        Catch ex As Exception
        End Try

        ocmcheckwaiting.Enabled = True

    End Sub

    Private Sub oSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles oSelectAll.CheckedChanged
        Try
            Dim _State As String = ""
            _State = IIf(Me.oSelectAll.Checked, "1", "0")

            With ogc
                If Not (.DataSource Is Nothing) And ogv.RowCount > 0 Then
                    With ogv
                        For i As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(i, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With
                End If
            End With
        Catch ex As Exception

        End Try
    End Sub

End Class