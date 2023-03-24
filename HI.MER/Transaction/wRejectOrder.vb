Imports System.Windows.Forms
Imports System.Windows.Forms.Control
Imports System.Data.Sql

Public Class wRejectOrder

#Region "Variable Declaration"

    Private Enum eJobState
        JobActive = 0
        JobSendApprove = 1
        JobCancel = 2
        JobClose = 3
    End Enum

    Private tSql As String
    Private _tFTOrderNo As String
    Private _FONoState As Integer
    Private _FNJobState As Integer

#End Region

#Region "Property"

    Private Shared _DTStateOrderNo As System.Data.DataTable = Nothing
    Private Shared ReadOnly Property LoadStateOrderNo(ByVal paramFTOrderNo As String) As System.Data.DataTable
        Get
            _DTStateOrderNo = Nothing

            Dim sSQL As String
            sSQL = ""

            '...State Main Factory Order No
            sSQL = "SELECT  ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPR AS A WITH(NOLOCK) WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "')),'0') AS FTStateMRP"
            sSQL &= Environment.NewLine & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReserve AS A WITH(NOLOCK) WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "')),'0') AS FTStateReserve"
            sSQL &= Environment.NewLine & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTOrder_Sourcing AS A WITH(NOLOCK) WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "')),'0') AS FTStateSourcing"
            sSQL &= Environment.NewLine & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS A WITH(NOLOCK) WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "')),'0') AS FTStatePurchase"
            sSQL &= Environment.NewLine & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS A WITH(NOLOCK) WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "')),'0') AS FTStateReceive"
            sSQL &= Environment.NewLine & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENAdjustStock_AddIn_Detail AS A WITH(NOLOCK) WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "')),'0') AS FTStateAdjust"
            sSQL &= Environment.NewLine & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferOrder AS A WITH(NOLOCK) WHERE FTOrderNoTo ='" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "')),'0') AS FTStateTransferIn"
            sSQL &= Environment.NewLine & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTransferOrder AS A WITH(NOLOCK) WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "')),'0') AS FTStateTransferOut"
            sSQL &= Environment.NewLine & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS A WITH(NOLOCK) WHERE FTOrderNo ='" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "')),'0') AS FTStateProduction"
            sSQL &= Environment.NewLine & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
            sSQL &= Environment.NewLine & "           FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS B (NOLOCK) ON A.FTOrderProdNo = B.FTOrderProdNo"
            sSQL &= Environment.NewLine & "           WHERE B.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "'), '0') AS FTStateCutting"
            sSQL &= Environment.NewLine & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
            sSQL &= Environment.NewLine & "           FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B (NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo"
            sSQL &= Environment.NewLine & "                                                                                                                                 INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS C (NOLOCK) ON B.FTOrderProdNo = C.FTOrderProdNo"
            sSQL &= Environment.NewLine & "															                                                                       INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS D (NOLOCK) ON A.FNHSysUnitSectId = D.FNHSysUnitSectId"
            sSQL &= Environment.NewLine & "           WHERE D.FTStateActive = N'1'"
            sSQL &= Environment.NewLine & "                 AND D.FTStateSew = N'1'"
            sSQL &= Environment.NewLine & "                 AND C.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "'), '0') AS FTStateSewing"
            sSQL &= Environment.NewLine & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
            sSQL &= Environment.NewLine & "           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan AS B (NOLOCK) ON A.FTPackNo = B.FTPackNo"
            sSQL &= Environment.NewLine & "           WHERE B.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "'), '0') AS FTStatePacking"

            _DTStateOrderNo = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            Return _DTStateOrderNo

        End Get

    End Property

    Private Shared _DTStateOrderSub As System.Data.DataTable = Nothing
    Private Shared ReadOnly Property LoadStateOrderNoSub(ByVal paramFTOrderNo As String, ByVal paramFTOrderNoSub As String) As System.Data.DataTable
        Get
            _DTStateOrderSub = Nothing

            Dim sSQL As String
            sSQL = ""

            '...State Sub Factory Order No
            sSQL = "SELECT  ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTMPR AS A WITH(NOLOCK) WHERE FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "')),'0') AS FTStateSubMRP"
            sSQL &= Environment.NewLine & "  ,ISNULL(((SELECT TOP 1 '1' AS FTState FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS A WITH(NOLOCK) WHERE FTSubOrderNo = '" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "')),'0') AS FTStateSubProduction"
            sSQL &= Environment.NewLine & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
            sSQL &= Environment.NewLine & "           FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS B (NOLOCK) ON A.FTOrderProdNo = B.FTOrderProdNo"
            sSQL &= Environment.NewLine & "                                                                                                                      INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS C (NOLOCK) ON B.FTOrderNo = C.FTOrderNo"
            sSQL &= Environment.NewLine & "												                                                                                                                                                                                                     AND B.FTOrderProdNo = C.FTOrderProdNo"
            sSQL &= Environment.NewLine & "           WHERE B.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "'"
            sSQL &= Environment.NewLine & "	               AND C.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "'), '0') AS FTStateSubCutting"
            sSQL &= Environment.NewLine & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
            sSQL &= Environment.NewLine & "           FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcodeScan_Detail AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS B (NOLOCK) ON A.FTBarcodeNo = B.FTBarcodeBundleNo"
            sSQL &= Environment.NewLine & "                                                                                                                                 INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS C (NOLOCK) ON B.FTOrderProdNo = C.FTOrderProdNo"
            sSQL &= Environment.NewLine & "																		                                                           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd_Detail AS D (NOLOCK) ON C.FTOrderNo = D.FTOrderNo"
            sSQL &= Environment.NewLine & "																		                                                                                                                                                                                        AND C.FTOrderProdNo = D.FTOrderProdNo"
            sSQL &= Environment.NewLine & "																		                                                           INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS E (NOLOCK) ON A.FNHSysUnitSectId = E.FNHSysUnitSectId"
            sSQL &= Environment.NewLine & "           WHERE E.FTStateActive = N'1'"
            sSQL &= Environment.NewLine & "                 AND E.FTStateSew = N'1'"
            sSQL &= Environment.NewLine & "                 AND C.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo) & "'"
            sSQL &= Environment.NewLine & "			       AND D.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "'), '0') AS FTStateSubSewing"
            sSQL &= Environment.NewLine & "  ,ISNULL((SELECT TOP 1 '1' AS FTState"
            sSQL &= Environment.NewLine & "           FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack AS A (NOLOCK) INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan AS B (NOLOCK) ON A.FTPackNo = B.FTPackNo"
            sSQL &= Environment.NewLine & "           WHERE B.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNo.Trim) & "'"
            sSQL &= Environment.NewLine & "	               AND B.FTSubOrderNo = N'" & HI.UL.ULF.rpQuoted(paramFTOrderNoSub) & "'), '0') AS FTStateSubPacking"

            _DTStateOrderSub = HI.Conn.SQLConn.GetDataTable(sSQL, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            Return _DTStateOrderSub

        End Get

    End Property

#End Region

#Region "Procedure And Function"

    Sub New(ByVal ptOrderNo As String, paramFNJobState As Integer)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        '============================================================================================
        HI.TL.HandlerControl.AddHandlerObj(Me)

        Dim oSysLang As New HI.ST.SysLanguage

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, Me.Name.ToString.Trim, Me)
        Catch ex As Exception
        End Try

        Call HI.ST.Lang.SP_SETxLanguage(Me)
        '============================================================================================

        _tFTOrderNo = ptOrderNo

        If _tFTOrderNo.Trim() <> "" Then
            Call W_PRCbLoadRejectOrderNo()
        End If

        _FONoState = paramFNJobState

    End Sub

    Private Function PROC_GETbValidateBeforeRejectOrderNo(ByVal paramFTOrderNo As String) As Boolean
        Dim bValidate As Boolean = True

        Dim tmpDTStateOrderNo As System.Data.DataTable

        tmpDTStateOrderNo = LoadStateOrderNo(paramFTOrderNo)

        If Not DBNull.Value.Equals(tmpDTStateOrderNo) And tmpDTStateOrderNo.Rows.Count > 0 Then

            For Each oDataRow As System.Data.DataRow In tmpDTStateOrderNo.Rows
                If oDataRow!FTStateReceive.ToString = "1" Then
                    bValidate = False
                    MessageBox.Show(IIf(HI.ST.Lang.Language = HI.ST.Lang.eLang.TH, "ไม่สามารถยกเลิกรายการใบสั่งผลิต : " & paramFTOrderNo & " นี้ได้ เนื่องจากมีการรับวัตถุดิบแล้ว !!!", "Can not re-ject factory order no : " & paramFTOrderNo & " this due to receive the raw material.!!!!"), "Reject Factory Order No.", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                Exit For

            Next

            If Not tmpDTStateOrderNo Is Nothing Then tmpDTStateOrderNo.Dispose()

        End If

        Return bValidate

    End Function

    Private Function W_PRCbRejectOrderNo_BACKUP_20150105() As Boolean
        Dim _bFlagReject As Boolean = False

        If Me.FTCancelAppRemark.Text.Trim() <> "" Then

            'FTStateMRP
            'FTStateReserve
            'FTStateSourcing
            'FTStatePurchase
            'FTStateReceive
            'FTStateAdjust
            'FTStateTransferIn
            'FTStateTransferOut
            'FTStateProduction
            'FTStateCutting
            'FTStateSewing

            Dim tmpDTStateOrderNo As System.Data.DataTable

            tmpDTStateOrderNo = LoadStateOrderNo(_tFTOrderNo)

            Dim tTextFTStateMRP As String = ""
            'Dim tTextFTStateReserve As String = ""
            Dim tTextFTStateSourcing As String = ""
            Dim tTextFTStatePurchase As String = ""
            Dim tTextFTStateReceive As String = ""

            tTextFTStateMRP = "0" : tTextFTStateSourcing = "0" : tTextFTStatePurchase = "0" : tTextFTStateReceive = "0"

            If Not DBNull.Value.Equals(tmpDTStateOrderNo) AndAlso tmpDTStateOrderNo.Rows.Count > 0 Then
                For Each oDataRow As System.Data.DataRow In tmpDTStateOrderNo.Rows
                    tTextFTStateMRP = oDataRow!FTStateMRP.ToString
                    tTextFTStateSourcing = oDataRow!FTStateSourcing.ToString
                    tTextFTStatePurchase = oDataRow!FTStatePurchase.ToString
                    tTextFTStateReceive = oDataRow!FTStateReceive.ToString

                    Exit For

                Next

            End If

            If Not tmpDTStateOrderNo Is Nothing Then tmpDTStateOrderNo.Dispose()

            If tTextFTStateReceive = "0" Then

                If HI.MG.ShowMsg.mConfirmProcess("", 1403110007, _tFTOrderNo) = True Then

                    Try
                        '...ยกเลิกรายการเลขที่ใบสั่งผลิต
                        tSql = ""
                        tSql = "UPDATE A"
                        tSql &= Environment.NewLine & "SET A.FNJobState = " & eJobState.JobCancel & ","
                        tSql &= Environment.NewLine & "    A.FTStateBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',"
                        tSql &= Environment.NewLine & "    A.FDStateDate = " & HI.UL.ULDate.FormatDateDB & ","
                        tSql &= Environment.NewLine & "    A.FTStateTime = " & HI.UL.ULDate.FormatTimeDB & ","
                        tSql &= Environment.NewLine & "    A.FTCancelAppBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',"
                        tSql &= Environment.NewLine & "    A.FDCancelAppDate = " & HI.UL.ULDate.FormatDateDB & ","
                        tSql &= Environment.NewLine & "    A.FDCancelAppTime = " & HI.UL.ULDate.FormatTimeDB & ","
                        tSql &= Environment.NewLine & "    A.FTCancelAppRemark = N'" & HI.UL.ULF.rpQuoted(Me.FTCancelAppRemark.Text.Trim()) & "'"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A"
                        tSql &= Environment.NewLine & "WHERE  A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "';"

                        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Else
                            _bFlagReject = True
                            HI.Conn.SQLConn.Tran.Commit()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        End If

                    Catch ex As Exception
                        'MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        DialogResult = System.Windows.Forms.DialogResult.Cancel
                    End Try

                End If

            Else
                MessageBox.Show(IIf(HI.ST.Lang.Language = HI.ST.Lang.eLang.TH, "ไม่สามารถยกเลิกรายการใบสั่งผลิต : " & _tFTOrderNo & " นี้ได้ เนื่องจากมีการรับวัตถุดิบแล้ว !!!", "Can not re-ject factory order no : " & _tFTOrderNo & " this due to receive the raw material.!!!!"), "Reject Factory Order No.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTCancelAppRemark_lbl.Text)
            Me.FTCancelAppRemark.Focus()
        End If

        Return _bFlagReject

    End Function

    Private Function W_PRCbRejectOrderNo_BACKUP_20150109() As Boolean

        Dim _bFlagReject As Boolean = False

        If Me.FTCancelAppRemark.Text.Trim() <> "" Then
            '...FTStateMPR
            '...FTStateSourcing
            '...FTStatePurchase
            '...FTStateReceived

            Dim tmpDTStateOrderNo As System.Data.DataTable

            tmpDTStateOrderNo = LoadStateOrderNo(_tFTOrderNo)

            Dim tTextFTStateMRP As String = ""
            Dim tTextFTStateSourcing As String = ""
            Dim tTextFTStatePurchase As String = ""
            Dim tTextFTStateReceive As String = ""

            tTextFTStateMRP = "0" : tTextFTStateSourcing = "0" : tTextFTStatePurchase = "0" : tTextFTStateReceive = "0"

            If Not DBNull.Value.Equals(tmpDTStateOrderNo) AndAlso tmpDTStateOrderNo.Rows.Count > 0 Then
                For Each oDataRow As System.Data.DataRow In tmpDTStateOrderNo.Rows
                    tTextFTStateMRP = oDataRow!FTStateMRP.ToString
                    tTextFTStateSourcing = oDataRow!FTStateSourcing.ToString
                    tTextFTStatePurchase = oDataRow!FTStatePurchase.ToString
                    tTextFTStateReceive = oDataRow!FTStateReceive.ToString

                    Exit For

                Next

            End If

            If Not tmpDTStateOrderNo Is Nothing Then tmpDTStateOrderNo.Dispose()

            If tTextFTStateReceive = "0" Then

                If HI.MG.ShowMsg.mConfirmProcess("", 1403110007, _tFTOrderNo) = True Then

                    Try
                        '...TPURTPurchase/TPURTPurchase_OrderNo

                        '...ยกเลิกรายการเลขที่ใบสั่งผลิต
                        tSql = ""
                        tSql = "UPDATE A"
                        tSql &= Environment.NewLine & "SET A.FNJobState = " & eJobState.JobCancel & ","
                        tSql &= Environment.NewLine & "    A.FTStateBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',"
                        tSql &= Environment.NewLine & "    A.FDStateDate = " & HI.UL.ULDate.FormatDateDB & ","
                        tSql &= Environment.NewLine & "    A.FTStateTime = " & HI.UL.ULDate.FormatTimeDB & ","
                        tSql &= Environment.NewLine & "    A.FTCancelAppBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',"
                        tSql &= Environment.NewLine & "    A.FDCancelAppDate = " & HI.UL.ULDate.FormatDateDB & ","
                        tSql &= Environment.NewLine & "    A.FDCancelAppTime = " & HI.UL.ULDate.FormatTimeDB & ","
                        tSql &= Environment.NewLine & "    A.FTCancelAppRemark = N'" & HI.UL.ULF.rpQuoted(Me.FTCancelAppRemark.Text.Trim()) & "'"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A"
                        tSql &= Environment.NewLine & "WHERE  A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "';"

                        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Else
                            _bFlagReject = True
                            HI.Conn.SQLConn.Tran.Commit()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        End If

                    Catch ex As Exception
                        'MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        DialogResult = System.Windows.Forms.DialogResult.Cancel
                    End Try

                End If

            Else
                MessageBox.Show(IIf(HI.ST.Lang.Language = HI.ST.Lang.eLang.TH, "ไม่สามารถยกเลิกรายการใบสั่งผลิต : " & _tFTOrderNo & " นี้ได้ เนื่องจากมีการรับวัตถุดิบแล้ว !!!", "Can not re-ject factory order no : " & _tFTOrderNo & " this due to receive the raw material.!!!!"), "Reject Factory Order No.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If



        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTCancelAppRemark_lbl.Text)
            Me.FTCancelAppRemark.Focus()
        End If

        Return _bFlagReject

    End Function

    Private Function W_PRCbRejectOrderNo() As Boolean

        Dim _bFlagReject As Boolean = False

        If Me.FTCancelAppRemark.Text.Trim() <> "" Then
            '...FTStateMPR
            '...FTStateSourcing
            '...FTStatePurchase
            '...FTStateReceived

            Dim tmpDTStateOrderNo As System.Data.DataTable

            tmpDTStateOrderNo = LoadStateOrderNo(_tFTOrderNo)

            Dim tTextFTStateMRP As String = ""
            Dim tTextFTStateSourcing As String = ""
            Dim tTextFTStatePurchase As String = ""
            Dim tTextFTStateReceive As String = ""

            tTextFTStateMRP = "0" : tTextFTStateSourcing = "0" : tTextFTStatePurchase = "0" : tTextFTStateReceive = "0"

            If Not DBNull.Value.Equals(tmpDTStateOrderNo) AndAlso tmpDTStateOrderNo.Rows.Count > 0 Then
                For Each oDataRow As System.Data.DataRow In tmpDTStateOrderNo.Rows
                    tTextFTStateMRP = oDataRow!FTStateMRP.ToString
                    tTextFTStateSourcing = oDataRow!FTStateSourcing.ToString
                    tTextFTStatePurchase = oDataRow!FTStatePurchase.ToString
                    tTextFTStateReceive = oDataRow!FTStateReceive.ToString

                    Exit For

                Next

            End If

            If Not tmpDTStateOrderNo Is Nothing Then tmpDTStateOrderNo.Dispose()

            'If tTextFTStateReceive = "0" Then

            '    If HI.MG.ShowMsg.mConfirmProcess("", 1403110007, _tFTOrderNo) = True Then

            '        Try
            '            '...TPURTPurchase/TPURTPurchase_OrderNo

            '            '...ยกเลิกรายการเลขที่ใบสั่งผลิต
            '            tSql = ""
            '            tSql = "UPDATE A"
            '            tSql &= Environment.NewLine & "SET A.FNJobState = " & eJobState.JobCancel & ","
            '            tSql &= Environment.NewLine & "    A.FTStateBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',"
            '            tSql &= Environment.NewLine & "    A.FDStateDate = " & HI.UL.ULDate.FormatDateDB & ","
            '            tSql &= Environment.NewLine & "    A.FTStateTime = " & HI.UL.ULDate.FormatTimeDB & ","
            '            tSql &= Environment.NewLine & "    A.FTCancelAppBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',"
            '            tSql &= Environment.NewLine & "    A.FDCancelAppDate = " & HI.UL.ULDate.FormatDateDB & ","
            '            tSql &= Environment.NewLine & "    A.FDCancelAppTime = " & HI.UL.ULDate.FormatTimeDB & ","
            '            tSql &= Environment.NewLine & "    A.FTCancelAppRemark = N'" & HI.UL.ULF.rpQuoted(Me.FTCancelAppRemark.Text.Trim()) & "'"
            '            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A"
            '            tSql &= Environment.NewLine & "WHERE  A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "';"

            '            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
            '            HI.Conn.SQLConn.SqlConnectionOpen()
            '            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            '            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            '            If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            '                HI.Conn.SQLConn.Tran.Rollback()
            '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '            Else
            '                _bFlagReject = True
            '                HI.Conn.SQLConn.Tran.Commit()
            '                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            '            End If

            '        Catch ex As Exception
            '            'MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
            '            HI.Conn.SQLConn.Tran.Rollback()
            '            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            '            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            '            DialogResult = System.Windows.Forms.DialogResult.Cancel
            '        End Try

            '    End If

            'Else
            '    MessageBox.Show(IIf(HI.ST.Lang.Language = HI.ST.Lang.eLang.TH, "ไม่สามารถยกเลิกรายการใบสั่งผลิต : " & _tFTOrderNo & " นี้ได้ เนื่องจากมีการรับวัตถุดิบแล้ว !!!", "Can not re-ject factory order no : " & _tFTOrderNo & " this due to receive the raw material.!!!!"), "Reject Factory Order No.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End If

            '...modify 2015/01/12
            If HI.MG.ShowMsg.mConfirmProcess("ท่านต้องการยกเลิกรายการใบสั่งผลิตนี้ใช่หรือไม่", 1403110007, _tFTOrderNo) = True Then

                If tTextFTStateReceive = "1" Then
                    '...clear TMERTOrder_Resource
                    '...clear TMERTMPR
                    '...clear TPURTOrder_Sourcing
                    Try
                        tSql = ""
                        tSql = "DECLARE @FTOrderNo AS NVARCHAR(30);"
                        tSql &= Environment.NewLine & "SET @FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "';"
                        tSql &= Environment.NewLine & "DECLARE @tmpTOrder_Sourcing_NOT_PONo AS TABLE(FTOrderNo NVARCHAR(30), FTSubOrderNo NVARCHAR(30), FNHSysRawMatId INT);"
                        tSql &= Environment.NewLine & "INSERT INTO @tmpTOrder_Sourcing_NOT_PONo(FTOrderNo, FTSubOrderNo, FNHSysRawMatId)"
                        tSql &= Environment.NewLine & "SELECT A.FTOrderNo, A.FTSubOrderNo, A.FNHSysRawMatId"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTOrder_Sourcing AS A (NOLOCK) LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS B (NOLOCK) ON A.FTPurchaseNo = B.FTPurchaseNo"
                        tSql &= Environment.NewLine & "WHERE A.FTOrderNo = @FTOrderNo"
                        tSql &= Environment.NewLine & "      AND B.FTPurchaseNo IS NULL"
                        tSql &= Environment.NewLine & "ORDER BY A.FTOrderNo, A.FTSubOrderNo, A.FNHSysRawMatId;"

                        tSql &= Environment.NewLine & "/*CLEAR Order Resource : Sourcing but not have PO No.*/"
                        tSql &= Environment.NewLine & "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder_Resource AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTOrder_Sourcing AS B (NOLOCK) ON A.FTOrderNo = B.FTOrderNo"
                        tSql &= Environment.NewLine & "WHERE A.FTOrderNo = @FTOrderNo"
                        tSql &= Environment.NewLine & "      AND A.FNHSysRawMatId = B.FNHSysRawMatId"
                        tSql &= Environment.NewLine & "      AND B.FTPurchaseNo = '';"

                        tSql &= Environment.NewLine & "/*CLEAR Order Resource : Non Sourcing*/"
                        tSql &= Environment.NewLine & "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder_Resource AS A"
                        tSql &= Environment.NewLine & "WHERE A.FTOrderNo = @FTOrderNo"
                        tSql &= Environment.NewLine & "      AND NOT EXISTS (SELECT 'T'"
                        tSql &= Environment.NewLine & "                      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTOrder_Sourcing AS L1 (NOLOCK)"
                        tSql &= Environment.NewLine & "                      WHERE A.FTOrderNo = L1.FTOrderNo"
                        tSql &= Environment.NewLine & " AND A.FNHSysRawMatId = L1.FNHSysRawMatId"
                        tSql &= Environment.NewLine & "                      );"

                        tSql &= Environment.NewLine & "/*CLEAR MPR State Sourcing but not have PO No.*/"
                        tSql &= Environment.NewLine & "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTMPR AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTOrder_Sourcing AS B (NOLOCK) ON A.FTOrderNo = B.FTOrderNo"
                        tSql &= Environment.NewLine & "WHERE A.FTOrderNo = @FTOrderNo"
                        tSql &= Environment.NewLine & "      AND A.FNHSysRawMatId = B.FNHSysRawMatId"
                        tSql &= Environment.NewLine & "      AND B.FTPurchaseNo = '';"

                        tSql &= Environment.NewLine & "/*CLEAR MPR State not have sourcing*/"
                        tSql &= Environment.NewLine & "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTMPR AS A"
                        tSql &= Environment.NewLine & "WHERE A.FTOrderNo = @FTOrderNo"
                        tSql &= Environment.NewLine & "      AND NOT EXISTS (SELECT 'T'"
                        tSql &= Environment.NewLine & "                      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTOrder_Sourcing AS L1 (NOLOCK)"
                        tSql &= Environment.NewLine & "                      WHERE A.FTOrderNo = L1.FTOrderNo"
                        tSql &= Environment.NewLine & "                            AND A.FNHSysRawMatId =L1.FNHSysRawMatId"
                        tSql &= Environment.NewLine & "                      );"

                        tSql &= Environment.NewLine & "/*CLEAR SOURCING Not have PO No.*/"
                        tSql &= Environment.NewLine & "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTOrder_Sourcing AS A INNER JOIN (SELECT L1.*"
                        tSql &= Environment.NewLine & "                                                 FROM @tmpTOrder_Sourcing_NOT_PONo AS L1"
                        tSql &= Environment.NewLine & "										        ) AS B ON A.FTOrderNo = B.FTOrderNo"
                        tSql &= Environment.NewLine & " 											          AND A.FTSubOrderNo = B.FTSubOrderNo"
                        tSql &= Environment.NewLine & "													      AND A.FNHSysRawMatId = B.FNHSysRawMatId"
                        tSql &= Environment.NewLine & "WHERE A.FTOrderNo = @FTOrderNo;"

                        tSql &= Environment.NewLine & "/*Rejected Factory Order No.*/"
                        tSql &= Environment.NewLine & "UPDATE A"
                        tSql &= Environment.NewLine & "SET A.FNJobState = " & eJobState.JobCancel & ","
                        tSql &= Environment.NewLine & "    A.FTStateBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',"
                        tSql &= Environment.NewLine & "    A.FDStateDate = " & HI.UL.ULDate.FormatDateDB & ","
                        tSql &= Environment.NewLine & "    A.FTStateTime = " & HI.UL.ULDate.FormatTimeDB & ","
                        tSql &= Environment.NewLine & "    A.FTCancelAppBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',"
                        tSql &= Environment.NewLine & "    A.FDCancelAppDate = " & HI.UL.ULDate.FormatDateDB & ","
                        tSql &= Environment.NewLine & "    A.FDCancelAppTime = " & HI.UL.ULDate.FormatTimeDB & ","
                        tSql &= Environment.NewLine & "    A.FTCancelAppRemark = N'" & HI.UL.ULF.rpQuoted(Me.FTCancelAppRemark.Text.Trim()) & "',"
                        tSql &= Environment.NewLine & "    A.FTStateOrderApp = N'0'"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A"
                        tSql &= Environment.NewLine & "WHERE  A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "';"


                        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Else
                            _bFlagReject = True
                            HI.Conn.SQLConn.Tran.Commit()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        End If

                    Catch ex As Exception
                        If System.Diagnostics.Debugger.IsAttached = True Then
                            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
                        End If

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        DialogResult = System.Windows.Forms.DialogResult.Cancel
                    End Try

                ElseIf tTextFTStatePurchase = "1" Then
                    '...clear TMERTOrder_Resource
                    '...clear TMERTMPR
                    '...clear TPURTOrder_Sourcing
                    Try
                        tSql = ""
                        tSql = "DECLARE @FTOrderNo AS NVARCHAR(30);"
                        tSql &= Environment.NewLine & "SET @FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "';"
                        tSql &= Environment.NewLine & "DECLARE @tmpTOrder_Sourcing_NOT_PONo AS TABLE(FTOrderNo NVARCHAR(30), FTSubOrderNo NVARCHAR(30), FNHSysRawMatId INT);"
                        tSql &= Environment.NewLine & "INSERT INTO @tmpTOrder_Sourcing_NOT_PONo(FTOrderNo, FTSubOrderNo, FNHSysRawMatId)"
                        tSql &= Environment.NewLine & "SELECT A.FTOrderNo, A.FTSubOrderNo, A.FNHSysRawMatId"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTOrder_Sourcing AS A (NOLOCK) LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTPurchase AS B (NOLOCK) ON A.FTPurchaseNo = B.FTPurchaseNo"
                        tSql &= Environment.NewLine & "WHERE A.FTOrderNo = @FTOrderNo"
                        tSql &= Environment.NewLine & "      AND B.FTPurchaseNo IS NULL"
                        tSql &= Environment.NewLine & "ORDER BY A.FTOrderNo, A.FTSubOrderNo, A.FNHSysRawMatId;"

                        tSql &= Environment.NewLine & "/*CLEAR Order Resource : Sourcing but not have PO No.*/"
                        tSql &= Environment.NewLine & "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder_Resource AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTOrder_Sourcing AS B (NOLOCK) ON A.FTOrderNo = B.FTOrderNo"
                        tSql &= Environment.NewLine & "WHERE A.FTOrderNo = @FTOrderNo"
                        tSql &= Environment.NewLine & "      AND A.FNHSysRawMatId = B.FNHSysRawMatId"
                        tSql &= Environment.NewLine & "      AND B.FTPurchaseNo = '';"

                        tSql &= Environment.NewLine & "/*CLEAR Order Resource : Non Sourcing*/"
                        tSql &= Environment.NewLine & "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder_Resource AS A"
                        tSql &= Environment.NewLine & "WHERE A.FTOrderNo = @FTOrderNo"
                        tSql &= Environment.NewLine & "      AND NOT EXISTS (SELECT 'T'"
                        tSql &= Environment.NewLine & "                      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTOrder_Sourcing AS L1 (NOLOCK)"
                        tSql &= Environment.NewLine & "                      WHERE A.FTOrderNo = L1.FTOrderNo"
                        tSql &= Environment.NewLine & " AND A.FNHSysRawMatId = L1.FNHSysRawMatId"
                        tSql &= Environment.NewLine & "                      );"

                        tSql &= Environment.NewLine & "/*CLEAR MPR State Sourcing but not have PO No.*/"
                        tSql &= Environment.NewLine & "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTMPR AS A INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTOrder_Sourcing AS B (NOLOCK) ON A.FTOrderNo = B.FTOrderNo"
                        tSql &= Environment.NewLine & "WHERE A.FTOrderNo = @FTOrderNo"
                        tSql &= Environment.NewLine & "      AND A.FNHSysRawMatId = B.FNHSysRawMatId"
                        tSql &= Environment.NewLine & "      AND B.FTPurchaseNo = '';"

                        tSql &= Environment.NewLine & "/*CLEAR MPR State not have sourcing*/"
                        tSql &= Environment.NewLine & "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTMPR AS A"
                        tSql &= Environment.NewLine & "WHERE A.FTOrderNo = @FTOrderNo"
                        tSql &= Environment.NewLine & "      AND NOT EXISTS (SELECT 'T'"
                        tSql &= Environment.NewLine & "                      FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTOrder_Sourcing AS L1 (NOLOCK)"
                        tSql &= Environment.NewLine & "                      WHERE A.FTOrderNo = L1.FTOrderNo"
                        tSql &= Environment.NewLine & "                            AND A.FNHSysRawMatId =L1.FNHSysRawMatId"
                        tSql &= Environment.NewLine & "                      );"

                        tSql &= Environment.NewLine & "/*CLEAR SOURCING Not have PO No.*/"
                        tSql &= Environment.NewLine & "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTOrder_Sourcing AS A INNER JOIN (SELECT L1.*"
                        tSql &= Environment.NewLine & "                                                 FROM @tmpTOrder_Sourcing_NOT_PONo AS L1"
                        tSql &= Environment.NewLine & "										        ) AS B ON A.FTOrderNo = B.FTOrderNo"
                        tSql &= Environment.NewLine & " 											          AND A.FTSubOrderNo = B.FTSubOrderNo"
                        tSql &= Environment.NewLine & "													      AND A.FNHSysRawMatId = B.FNHSysRawMatId"
                        tSql &= Environment.NewLine & "WHERE A.FTOrderNo = @FTOrderNo;"

                        tSql &= Environment.NewLine & "/*Rejected Factory Order No.*/"
                        tSql &= Environment.NewLine & "UPDATE A"
                        tSql &= Environment.NewLine & "SET A.FNJobState = " & eJobState.JobCancel & ","
                        tSql &= Environment.NewLine & "    A.FTStateBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',"
                        tSql &= Environment.NewLine & "    A.FDStateDate = " & HI.UL.ULDate.FormatDateDB & ","
                        tSql &= Environment.NewLine & "    A.FTStateTime = " & HI.UL.ULDate.FormatTimeDB & ","
                        tSql &= Environment.NewLine & "    A.FTCancelAppBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',"
                        tSql &= Environment.NewLine & "    A.FDCancelAppDate = " & HI.UL.ULDate.FormatDateDB & ","
                        tSql &= Environment.NewLine & "    A.FDCancelAppTime = " & HI.UL.ULDate.FormatTimeDB & ","
                        tSql &= Environment.NewLine & "    A.FTCancelAppRemark = N'" & HI.UL.ULF.rpQuoted(Me.FTCancelAppRemark.Text.Trim()) & "',"
                        tSql &= Environment.NewLine & "    A.FTStateOrderApp = N'0'"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A"
                        tSql &= Environment.NewLine & "WHERE  A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "';"


                        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Else
                            _bFlagReject = True
                            HI.Conn.SQLConn.Tran.Commit()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        End If

                    Catch ex As Exception
                        If System.Diagnostics.Debugger.IsAttached = True Then
                            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
                        End If

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        DialogResult = System.Windows.Forms.DialogResult.Cancel
                    End Try

                ElseIf tTextFTStateSourcing = "1" Then
                    '...clear TPURTOrder_Sourcing
                    '...clear TMERTMPR
                    '...clear TMERTOrder_Resource
                    Try
                        tSql = ""
                        tSql = "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "]..TPURTOrder_Sourcing AS A"
                        tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "';"
                        tSql = "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTMPR AS A"
                        tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "';"
                        tSql &= Environment.NewLine & "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder_Resource AS A"
                        tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "';"

                        tSql &= Environment.NewLine & "/*Rejected Factory Order No.*/"
                        tSql &= Environment.NewLine & "UPDATE A"
                        tSql &= Environment.NewLine & "SET A.FNJobState = " & eJobState.JobCancel & ","
                        tSql &= Environment.NewLine & "    A.FTStateBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',"
                        tSql &= Environment.NewLine & "    A.FDStateDate = " & HI.UL.ULDate.FormatDateDB & ","
                        tSql &= Environment.NewLine & "    A.FTStateTime = " & HI.UL.ULDate.FormatTimeDB & ","
                        tSql &= Environment.NewLine & "    A.FTCancelAppBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',"
                        tSql &= Environment.NewLine & "    A.FDCancelAppDate = " & HI.UL.ULDate.FormatDateDB & ","
                        tSql &= Environment.NewLine & "    A.FDCancelAppTime = " & HI.UL.ULDate.FormatTimeDB & ","
                        tSql &= Environment.NewLine & "    A.FTCancelAppRemark = N'" & HI.UL.ULF.rpQuoted(Me.FTCancelAppRemark.Text.Trim()) & "',"
                        tSql &= Environment.NewLine & "    A.FTStateOrderApp = N'0'"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A"
                        tSql &= Environment.NewLine & "WHERE  A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "';"

                        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Else
                            _bFlagReject = True
                            HI.Conn.SQLConn.Tran.Commit()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        End If

                    Catch ex As Exception
                        If System.Diagnostics.Debugger.IsAttached = True Then
                            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
                        End If

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        DialogResult = System.Windows.Forms.DialogResult.Cancel
                    End Try

                ElseIf tTextFTStateMRP = "1" Then
                    '...clear TMERTMPR
                    '...clear TMERTOrder_Resource
                    Try
                        tSql = ""
                        tSql = "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTMPR AS A"
                        tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "';"
                        tSql &= Environment.NewLine & "DELETE A"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder_Resource AS A"
                        tSql &= Environment.NewLine & "WHERE A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "';"

                        tSql &= Environment.NewLine & "/*Rejected Factory Order No.*/"
                        tSql &= Environment.NewLine & "UPDATE A"
                        tSql &= Environment.NewLine & "SET A.FNJobState = " & eJobState.JobCancel & ","
                        tSql &= Environment.NewLine & "    A.FTStateBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',"
                        tSql &= Environment.NewLine & "    A.FDStateDate = " & HI.UL.ULDate.FormatDateDB & ","
                        tSql &= Environment.NewLine & "    A.FTStateTime = " & HI.UL.ULDate.FormatTimeDB & ","
                        tSql &= Environment.NewLine & "    A.FTCancelAppBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',"
                        tSql &= Environment.NewLine & "    A.FDCancelAppDate = " & HI.UL.ULDate.FormatDateDB & ","
                        tSql &= Environment.NewLine & "    A.FDCancelAppTime = " & HI.UL.ULDate.FormatTimeDB & ","
                        tSql &= Environment.NewLine & "    A.FTCancelAppRemark = N'" & HI.UL.ULF.rpQuoted(Me.FTCancelAppRemark.Text.Trim()) & "',"
                        tSql &= Environment.NewLine & "    A.FTStateOrderApp = N'0'"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A"
                        tSql &= Environment.NewLine & "WHERE  A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "';"

                        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Else
                            _bFlagReject = True
                            HI.Conn.SQLConn.Tran.Commit()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        End If

                    Catch ex As Exception
                        If System.Diagnostics.Debugger.IsAttached = True Then
                            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
                        End If

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        DialogResult = System.Windows.Forms.DialogResult.Cancel
                    End Try

                Else

                    Try
                        '...ยกเลิกรายการเลขที่ใบสั่งผลิต '...begin state {normal}
                        '...change FNJobState ==> Cancel
                        tSql = ""
                        tSql = "/*Rejected Factory Order No.*/"
                        tSql &= "UPDATE A"
                        tSql &= Environment.NewLine & "SET A.FNJobState = " & eJobState.JobCancel & ","
                        tSql &= Environment.NewLine & "    A.FTStateBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',"
                        tSql &= Environment.NewLine & "    A.FDStateDate = " & HI.UL.ULDate.FormatDateDB & ","
                        tSql &= Environment.NewLine & "    A.FTStateTime = " & HI.UL.ULDate.FormatTimeDB & ","
                        tSql &= Environment.NewLine & "    A.FTCancelAppBy = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',"
                        tSql &= Environment.NewLine & "    A.FDCancelAppDate = " & HI.UL.ULDate.FormatDateDB & ","
                        tSql &= Environment.NewLine & "    A.FDCancelAppTime = " & HI.UL.ULDate.FormatTimeDB & ","
                        tSql &= Environment.NewLine & "    A.FTCancelAppRemark = N'" & HI.UL.ULF.rpQuoted(Me.FTCancelAppRemark.Text.Trim()) & "',"
                        tSql &= Environment.NewLine & "    A.FTStateOrderApp = N'0'"
                        tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A"
                        tSql &= Environment.NewLine & "WHERE  A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "';"

                        HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_MERCHAN)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        If HI.Conn.SQLConn.Execute_Tran(tSql, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Else
                            _bFlagReject = True
                            HI.Conn.SQLConn.Tran.Commit()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        End If

                    Catch ex As Exception
                        If System.Diagnostics.Debugger.IsAttached = True Then
                            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
                        End If

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        DialogResult = System.Windows.Forms.DialogResult.Cancel
                    End Try

                End If

            End If

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FTCancelAppRemark_lbl.Text)
            Me.FTCancelAppRemark.Focus()
        End If

        Return _bFlagReject

    End Function

    Private Function W_PRCbLoadRejectOrderNo() As Boolean
        Dim _bFlagRejectInfo As Boolean = False
        Dim oDBdtRejectOrderNo As DataTable
        Try
            HI.TL.HandlerControl.ClearControl(Me.ogbOrderRejectRemark)

            _FNJobState = 0

            tSql = ""
            tSql = "SELECT A.FNJobState, A.FTCancelAppRemark, A.FTCancelAppBy, A.FDCancelAppDate, A.FDCancelAppTime"
            tSql &= Environment.NewLine & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..[TMERTOrder] AS A WITH(NOLOCK)"
            tSql &= Environment.NewLine & "WHERE  A.FTOrderNo = N'" & HI.UL.ULF.rpQuoted(_tFTOrderNo) & "'"

            oDBdtRejectOrderNo = HI.Conn.SQLConn.GetDataTable(tSql, HI.Conn.DB.DataBaseName.DB_MERCHAN)

            If oDBdtRejectOrderNo.Rows.Count > 0 Then

                If Not DBNull.Value.Equals(oDBdtRejectOrderNo.Rows(0).Item("FNJobState")) Then

                    _FNJobState = oDBdtRejectOrderNo.Rows(0).Item("FNJobState")

                    If _FNJobState = eJobState.JobCancel Then
                        If Not DBNull.Value.Equals(oDBdtRejectOrderNo.Rows(0).Item("FTCancelAppRemark")) Then
                            Me.FTCancelAppRemark.Text = oDBdtRejectOrderNo.Rows(0).Item("FTCancelAppRemark").ToString()
                        Else
                            Me.FTCancelAppRemark.Text = "????"
                        End If

                        If Not DBNull.Value.Equals(oDBdtRejectOrderNo.Rows(0).Item("FTCancelAppBy")) Then
                            Me.FTCancelAppBy.Text = oDBdtRejectOrderNo.Rows(0).Item("FTCancelAppBy").ToString()
                        Else
                            Me.FTCancelAppBy.Text = "????"
                        End If

                        If Not DBNull.Value.Equals(oDBdtRejectOrderNo.Rows(0).Item("FDCancelAppDate")) Then
                            Me.FDCancelAppDate.Text = HI.UL.ULDate.ConvertEN(oDBdtRejectOrderNo.Rows(0).Item("FDCancelAppDate"))
                        Else
                            Me.FDCancelAppDate.Text = "????"
                        End If

                        If Not DBNull.Value.Equals(oDBdtRejectOrderNo.Rows(0).Item("FDCancelAppTime")) Then
                            Me.FDCancelAppTime.Text = oDBdtRejectOrderNo.Rows(0).Item("FDCancelAppTime").ToString()
                        Else
                            Me.FDCancelAppTime.Text = "????"
                        End If

                    End If

                    _bFlagRejectInfo = True

                End If

            End If

            If _FNJobState = eJobState.JobCancel Then
                Me.FTCancelAppRemark.Properties.ReadOnly = True
            End If

        Catch ex As Exception
            MsgBox(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString(), MsgBoxStyle.OkOnly, My.Application.Info.Title)
        End Try

        Return _bFlagRejectInfo

    End Function

#End Region

    Private Sub wRejectOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If _FONoState = eJobState.JobCancel Or _FONoState = eJobState.JobClose Then
            Me.ocmok.Enabled = False
        Else
            Me.ocmok.Enabled = True
        End If

        Me.FTCancelAppRemark.Focus()

        'Dim dt As DataTable = SqlDataSourceEnumerator.Instance.GetDataSources
        'For Each dr As DataRow In dt.Rows
        '    ddlInstances.Items.Add(String.Concat(dr("ServerName"), "\\", dr("InstanceName")))
        'Next

    End Sub

    Private Sub ocmcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmcancel.Click
        DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ocmok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmok.Click
        If _FNJobState = eJobState.JobCancel Then
            '...Nothing ปฎิเสธการยกเลิกรายการเลขที่ใบสั่งผลิต
        Else
            If W_PRCbRejectOrderNo() = True Then
                DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        End If
    End Sub

    Private Sub FTCancelAppRemark_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles FTCancelAppRemark.KeyPress
        e.Handled = False
    End Sub

    Private Sub FTCancelAppRemark_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles FTCancelAppRemark.PreviewKeyDown
        If e.KeyData = Keys.Tab Then
            'e.IsInputKey = True
            'Me.ocmok.Focus()
            'SendKeys.Send("{TAB}")
        End If
    End Sub

End Class