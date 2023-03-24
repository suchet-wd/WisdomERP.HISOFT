Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns

Public Class wSMPSetMaterailFree

    Private GridDataBefore As String = ""

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.



    End Sub

#Region "Initial Grid"

#End Region


#Region "Custom summaries"

    Private totalSum As Double = 0
    Private GrpSum As Double = 0
    Private _RowHandleHold As Integer = 0


    Private Sub InitSummaryStartValue()
        totalSum = 0
        GrpSum = 0
        _RowHandleHold = 0
    End Sub

    Private Sub ogvsummary_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvoperation.CustomSummaryCalculate
        Try
            If e.SummaryProcess = CustomSummaryProcess.Start Then
                InitSummaryStartValue()
            End If

            With ogvoperation
                Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
                    Case "FNSMPSam"
                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                            If e.IsTotalSummary Then
                                If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                    If (.GetRowCellValue(e.RowHandle, "FTSMPOrderNo").ToString <> .GetRowCellValue(_RowHandleHold, "FTSMPOrderNo").ToString()) Then
                                        totalSum = totalSum + (Val(e.FieldValue.ToString))

                                    End If
                                End If
                                _RowHandleHold = e.RowHandle
                            End If
                            e.TotalValue = totalSum
                        End If


                End Select
            End With

        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Property"

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

#Region "Procedure"



    Public Sub LoadDataInfo(ByVal Key As String)

        Dim _Qry As String = ""
        _Qry = " SELECT     SOP.FNSeq"

        _Qry &= vbCrLf & "  , Case When ISDATE(ISNULL( SOP.FTDate,'')) = 1 THEN  Convert(nvarchar(10),Convert(Datetime, SOP.FTDate),103) ELSE '' END AS  FTDate "
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNQuantity,0) As FNQuantity"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNPass,0) As FNPass"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNNotPass,0) As FNNotPass"
        _Qry &= vbCrLf & " ,  ISNULL(SOP.FTRemark,'') AS FTRemark"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FNPass,0) +  ISNULL(SOP.FNNotPass,0) As FNTotalQC"

        _Qry &= vbCrLf & "  , ISNULL(SOP.FTSizeBreakDown,'') As FTSizeBreakDown"
        _Qry &= vbCrLf & "  , ISNULL(SOP.FTColorway,'') As FTColorway"

        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleQC AS SOP WITH (NOLOCK)"
        _Qry &= vbCrLf & "   WHERE SOP.FTTeam='" & HI.UL.ULF.rpQuoted(Key.ToString) & "' "
        _Qry &= vbCrLf & "  ORDER BY SOP.FNSeq ASC"
        Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
        Me.ogcoperation.DataSource = _dt

    End Sub


#End Region

#Region "General"


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub



    Private Function CheckProcess(key As String, Optional showmsg As Boolean = True) As Boolean
        Dim stateprocess As Boolean = False

        Dim cmd As String = ""

        cmd = "select top 1 FTStateFinish from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPSampleTeam AS x with(nolock) where FTTeam='" & HI.UL.ULF.rpQuoted(key) & "'"

        stateprocess = (HI.Conn.SQLConn.GetField(cmd, Conn.DB.DataBaseName.DB_SAMPLE, "") = "1")

        If stateprocess Then

            If showmsg Then
                HI.MG.ShowMsg.mInfo("พบข้อมูลบันทึก จบ กระบวนการทำงานแล้ว ไม่สามารถลบหรือแก้ไขได้ !!!", 1809210046, Me.Text, key, System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        End If

        Return stateprocess
    End Function



    Private Sub wOperationByStyle_Load(sender As Object, e As EventArgs) Handles Me.Load


        With Me.ogvoperation
            .Columns.ColumnByFieldName("FTStateFree").OptionsColumn.AllowEdit = (Me.ocmsettingmatfree.Enabled)

        End With


    End Sub

#End Region

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Call LoadOrderProdDetail()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs)
        HI.TL.HandlerControl.ClearControl(Me)

    End Sub

    Private Sub ogcoperation_Click(sender As Object, e As EventArgs) Handles ogcoperation.Click

    End Sub


    Private Sub LoadOrderProdDetail()
        Dim cmd As String = ""
        Dim _dtprod As DataTable

        ogcoperation.DataSource = Nothing

        cmd = "   Select  A.FTSMPOrderNo "


        cmd &= vbCrLf & "  , Case When ISDATE(A.FDSMPOrderDate) = 1 Then  convert(Datetime,A.FDSMPOrderDate) Else NULL END AS  FDSMPOrderDate"

        cmd &= vbCrLf & "  , Case When ISDATE(A.FDSendToSMPDate) = 1 Then  convert(Datetime,A.FDSendToSMPDate) Else NULL END AS  FDSendToSMPDate"
        cmd &= vbCrLf & "  , Case When ISDATE(A.FTStateReceiptDate) = 1 Then  convert(Datetime,A.FTStateReceiptDate) Else NULL END AS  FTStateReceiptDate"

        cmd &= vbCrLf & " ,A.FTStyleName"
        cmd &= vbCrLf & " ,A.FTGenderCode "
        cmd &= vbCrLf & " ,A.FTGenderName"
        cmd &= vbCrLf & " ,A.FTSMPOrderBy "
        cmd &= vbCrLf & " ,A.FTOrderRemark"


        cmd &= vbCrLf & " ,A.FTCustomerTeam  "
        cmd &= vbCrLf & " , A.FNSMPPrototypeNo"
        cmd &= vbCrLf & " , A.FTStyleCode"
        cmd &= vbCrLf & " , A.FTSeasonCode"
        cmd &= vbCrLf & " , A.FTCustCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            cmd &= vbCrLf & "  ,ISNULL(XT.FTNameTH,'') AS FNSMPOrderType"
            cmd &= vbCrLf & "   , A.FTCustNameTH AS FTCustName"
            cmd &= vbCrLf & "  ,ISNULL(XTOT.FTNameTH,'') AS FNOrderSampleType"

        Else

            cmd &= vbCrLf & "  ,ISNULL(XT.FTNameEN,'') AS FNSMPOrderType"
            cmd &= vbCrLf & "   , A.FTCustNameEN  AS FTCustName"
            cmd &= vbCrLf & "  ,ISNULL(XTOT.FTNameTH,'') AS FNOrderSampleType"

        End If

        cmd &= vbCrLf & "   , A.FTMerTeamCode"


        cmd &= vbCrLf & "   , XSAM.*"
        cmd &= vbCrLf & "  FROM(Select A.FTSMPOrderNo, A.FDSMPOrderDate, A.FNSMPOrderType, A.FNSMPPrototypeNo, MST.FTStyleCode, MSS.FTSeasonCode, MCT.FTCustCode, MCT.FTCustNameTH, MCT.FTCustNameEN, MMT.FTMerTeamCode"

        cmd &= vbCrLf & " 	,A.FTStateAppDate AS FDSendToSMPDate "
        cmd &= vbCrLf & " 	,A.FTStateReceiptDate"
        cmd &= vbCrLf & " 	,MST.FTStyleNameEN  AS FTStyleName"
        cmd &= vbCrLf & " 	,GD.FTGenderCode "
        cmd &= vbCrLf & " 	,GD.FTGenderNameEN  AS FTGenderName"
        cmd &= vbCrLf & " ,A.FTSMPOrderBy "
        cmd &= vbCrLf & " ,CASE WHEN ISNULL(A.FTRemark,'') <> ''  THEN ISNULL(A.FTRemark,'') + char(30) ELSE  '' END "
        cmd &= vbCrLf & " +   CASE WHEN ISNULL(FTStateEmb,'')='1' THEN 'Emb,' ELSE '' END"
        cmd &= vbCrLf & " 	+   CASE WHEN ISNULL(FTStatePrint,'')='1' THEN 'Print,' ELSE '' END"
        cmd &= vbCrLf & " +   CASE WHEN ISNULL(FTStateHeat,'')='1' THEN 'Heat,' ELSE '' END"
        cmd &= vbCrLf & " 	+   CASE WHEN ISNULL(FTStateLaser,'')='1' THEN 'Laser,' ELSE '' END"
        cmd &= vbCrLf & " +   CASE WHEN ISNULL(FTStateWindows,'')='1' THEN 'window' ELSE '' END AS FTOrderRemark"

        cmd &= vbCrLf & " 	,A.FNOrderSampleType "
        cmd &= vbCrLf & " 	,A.FTCustomerTeam  "
        cmd &= vbCrLf & "   From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder As A With(NOLOCK)  LEFT OUTER Join"
        cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As MST With(NOLOCK) On A.FNHSysStyleId = MST.FNHSysStyleId LEFT OUTER Join"
        cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason As MSS With(NOLOCK)  On A.FNHSysSeasonId = MSS.FNHSysSeasonId LEFT OUTER Join"
        cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCustomer As MCT With(NOLOCK)  On A.FNHSysCustId = MCT.FNHSysCustId LEFT OUTER Join"
        cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMerTeam As MMT With(NOLOCK)  On A.FNHSysMerTeamId = MMT.FNHSysMerTeamId LEFT OUTER Join"
        cmd &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMGender As GD With(NOLOCK)  On A.FNHSysGenderId = GD.FNHSysGenderId "


        cmd &= vbCrLf & "    WHERE A.FTSMPOrderNo<>''"

        If FNHSysCustId.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FNHSysCustId=" & Val(FNHSysCustId.Properties.Tag.ToString) & ""
        End If

        If FNHSysStyleId.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FNHSysStyleId=" & Val(FNHSysStyleId.Properties.Tag.ToString) & ""
        End If

        If FNHSysSeasonId.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FNHSysSeasonId=" & Val(FNHSysSeasonId.Properties.Tag.ToString) & ""
        End If

        If FNHSysMerTeamId.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FNHSysMerTeamId=" & Val(FNHSysMerTeamId.Properties.Tag.ToString) & ""
        End If

        If FTStartOrderDate.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FDSMPOrderDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text) & "'"
        End If

        If FTEndOrderDate.Text <> "" Then
            cmd &= vbCrLf & "  AND A.FDSMPOrderDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text) & "'"
        End If

        cmd &= vbCrLf & "  ) As A"

        cmd &= vbCrLf & "  OUTER APPLY (   "
        'cmd &= vbCrLf & "  Select  SSAM.FNMatSeq, SSAM.FTMat, SSAM.FTMatName, SSAM.FTMatColor, SSAM.FTMatColorName, SSAM.FTMatSize, SSAM.FNMatQuantity,  SSAM.FTRemark,  SSAM.FTStateFree,  S.FTSuplCode AS FNHSysSuplId, U.FTUnitCode AS FNHSysUnitId ,0 AS FNStateMatType "
        'cmd &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_FabricMatList As SSAM With (NOLOCK)  Left OUTER JOIN "
        'cmd &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S With (NOLOCK)  ON SSAM.FNHSysSuplId = S.FNHSysSuplId LEFT OUTER JOIN "
        'cmd &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U With (NOLOCK)  ON SSAM.FNHSysUnitId = U.FNHSysUnitId "
        'cmd &= vbCrLf & "   WHERE  (SSAM.FTSMPOrderNo =A.FTSMPOrderNo)  "
        'cmd &= vbCrLf & "  UNION ALL "
        cmd &= vbCrLf & "  Select  SSAM.FNMatSeq, SSAM.FTMat, SSAM.FTMatName, SSAM.FTMatColor, SSAM.FTMatColorName, SSAM.FTMatSize, SSAM.FNMatQuantity,  SSAM.FTRemark,  SSAM.FTStateFree,  S.FTSuplCode AS FNHSysSuplId, U.FTUnitCode AS FNHSysUnitId ,1 AS FNStateMatType,SSAM.FNHSysRawmatId "
        cmd &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_MatList As SSAM With (NOLOCK)  Left OUTER JOIN "
        cmd &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S With (NOLOCK)  ON SSAM.FNHSysSuplId = S.FNHSysSuplId LEFT OUTER JOIN "
        cmd &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U With (NOLOCK)  ON SSAM.FNHSysUnitId = U.FNHSysUnitId "
        cmd &= vbCrLf & "   WHERE  (SSAM.FTSMPOrderNo =A.FTSMPOrderNo)  "

        cmd &= vbCrLf & "   ) As XSAM"

        cmd &= vbCrLf & "    Left OUTER JOIN (  "
        cmd &= vbCrLf & "  Select FNListIndex, FTNameTH, FTNameEN "
        cmd &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L With (NOLOCK) "
        cmd &= vbCrLf & "   WHERE  (FTListName = N'FNSMPOrderType')  "
        cmd &= vbCrLf & "   ) AS XT ON  A.FNSMPOrderType =XT.FNListIndex "

        cmd &= vbCrLf & "    Left OUTER JOIN (  "
        cmd &= vbCrLf & "  Select  FNListIndex,FTNameTH,FTNameEN "
        cmd &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData As L With (NOLOCK) "
        cmd &= vbCrLf & "   Where (FTListName = N'FNOrderSampleType')  "
        cmd &= vbCrLf & "   ) As XTOT On  A.FNOrderSampleType = XTOT.FNListIndex"
        cmd &= vbCrLf & "  WHERE ISNULL(XSAM.FTMat,'') <> ''"
        cmd &= vbCrLf & " ORDER BY  A.FTSMPOrderNo "

        _dtprod = HI.Conn.SQLConn.GetDataTable(cmd, Conn.DB.DataBaseName.DB_SAMPLE)

        ogcoperation.DataSource = _dtprod.Copy

        _dtprod.Dispose()



    End Sub


    Private Sub RepFTStateFree_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFTStateFree.EditValueChanging
        Try

            With Me.ogvoperation
                Dim SmpOrder As String = .GetFocusedRowCellValue("FTSMPOrderNo").ToString()
                Dim SmpOrderMatSeq As Integer = Val(.GetFocusedRowCellValue("FNMatSeq").ToString())
                Dim SmpOrderMatType As Integer = Val(.GetFocusedRowCellValue("FNStateMatType").ToString())
                Dim cmdstring As String = ""
                Dim StateFree As String = "0"

                Dim mRawmatId As Integer = Val(.GetFocusedRowCellValue("FNHSysRawmatId").ToString())

                If e.NewValue.ToString() = "1" Then
                    StateFree = "1"
                End If


                'If SmpOrderMatType = 0 Then

                '    cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_FabricMatList  Set FTStateFree='" & StateFree & "'"
                '    cmdstring &= vbCrLf & " ,FTStateFreeUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                '    cmdstring &= vbCrLf & ",FDStateFreeUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                '    cmdstring &= vbCrLf & ",FTStateFreeUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                '    cmdstring &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(SmpOrder) & "' AND  FNMatSeq=" & SmpOrderMatSeq & ""

                '    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)

                'Else

                '    cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_MatList  Set FTStateFree='" & StateFree & "'"
                '    cmdstring &= vbCrLf & " ,FTStateFreeUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                '    cmdstring &= vbCrLf & ",FDStateFreeUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                '    cmdstring &= vbCrLf & ",FTStateFreeUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                '    cmdstring &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(SmpOrder) & "' AND  FNMatSeq=" & SmpOrderMatSeq & ""

                '    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)

                'End If

                cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_MatList  Set FTStateFree='" & StateFree & "'"
                cmdstring &= vbCrLf & " ,FTStateFreeUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                cmdstring &= vbCrLf & ",FDStateFreeUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ",FTStateFreeUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                cmdstring &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(SmpOrder) & "' AND  FNHSysRawmatId=" & mRawmatId & ""
                cmdstring &= vbCrLf & " update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Resource  Set FTStateFree='" & StateFree & "'"
                cmdstring &= vbCrLf & " ,FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                cmdstring &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                cmdstring &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(SmpOrder) & "' AND  FNHSysRawMatId=" & mRawmatId & ""

                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)


                'cmdstring = "update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder_Resource  Set FTStateFree='" & StateFree & "'"
                'cmdstring &= vbCrLf & " ,FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                'cmdstring &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                'cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                'cmdstring &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(SmpOrder) & "' AND  FNHSysRawMatId=" & mRawmatId & ""

                'HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)


            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmsettingmatfree_Click(sender As Object, e As EventArgs) Handles ocmsettingmatfree.Click

    End Sub
End Class