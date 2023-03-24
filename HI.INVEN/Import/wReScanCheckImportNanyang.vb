Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Data

Public Class wReScanCheckImportNanyang

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call CreateMergeEditControl()

    End Sub

#Region "Custom summaries"

    Private totalSum As Decimal = 0
    Private GrpSum As Decimal = 0
    Private _RowHandleHold As Integer = 0


    Private Sub InitSummaryStartValue()
        totalSum = 0
        GrpSum = 0
        _RowHandleHold = 0
    End Sub

    Private Sub ogvdetail_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvdetail.CustomSummaryCalculate
        Try
            If e.SummaryProcess = CustomSummaryProcess.Start Then
                InitSummaryStartValue()
            End If

            With ogvdetail
                Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
                    Case "FNQuantity"
                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                            If e.IsTotalSummary Then
                                If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                    If (.GetRowCellValue(e.RowHandle, "FTBarcodeSuplRefNo").ToString <> .GetRowCellValue(_RowHandleHold, "FTBarcodeSuplRefNo").ToString) Or e.RowHandle = _RowHandleHold Then
                                        ' .GetRowCellValue(e.RowHandle, "FTPOLineItemNo") <> .GetRowCellValue(_RowHandleHold, "FTPOLineItemNo") Or _
                                        totalSum = totalSum + Decimal.Parse(Val(e.FieldValue.ToString))

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
    Enum EditMergeCellData As Integer
        ItemSelect = 0
        ItemFabricSize = 1
        ItemQty = 2

    End Enum

    Private _StateEditMergeCell As EditMergeCellData = EditMergeCellData.ItemSelect
    Property StateEditMergeCell As EditMergeCellData
        Get
            Return _StateEditMergeCell
        End Get
        Set(value As EditMergeCellData)
            _StateEditMergeCell = value
        End Set
    End Property

    Private m_mergedCellEditorSelect As DevExpress.XtraEditors.CheckEdit
    Private m_mergedCellEditorFabricSize As DevExpress.XtraEditors.CalcEdit
    Private m_mergedCellEditorQty As DevExpress.XtraEditors.CalcEdit
    Private m_mergedCellsEdited As GridCellInfoCollection


    Private Sub CreateMergeEditControl()
        m_mergedCellEditorSelect = New DevExpress.XtraEditors.CheckEdit
        m_mergedCellEditorFabricSize = New DevExpress.XtraEditors.CalcEdit
        m_mergedCellEditorQty = New DevExpress.XtraEditors.CalcEdit

        With m_mergedCellEditorSelect
            .Name = "RepDataItemSelect"
            .Properties.ValueChecked = "1"
            .Properties.ValueUnchecked = "0"
            .Text = ""
        End With

        With m_mergedCellEditorFabricSize
            .Name = "RepDataItemFabrisSize"
            .Properties.DisplayFormat.FormatString = "{0:n2}"
            .Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            .Properties.EditFormat.FormatString = "{0:n2}"
            .Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        End With

        With m_mergedCellEditorQty
            .Name = "RepDataItemQty"
            .Properties.DisplayFormat.FormatString = "{0:n4}"
            .Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            .Properties.EditFormat.FormatString = "{0:n4}"
            .Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        End With

    End Sub

#Region "Procedure"

    Private Sub LoadData(DocKey As String)

        Dim _Qry As String = ""
        Dim dt As DataTable

        _Qry = "   Select '0' AS FTSelect,A.FTBarcodeSuplRefNo"
        _Qry &= vbCrLf & "  ,A.FNHSysRawMatId"
        _Qry &= vbCrLf & "  , IM.FTRawMatCode "
        _Qry &= vbCrLf & "  ,ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
        _Qry &= vbCrLf & "  ,ISNULL( S.FTRawMatSizeCode ,'') AS FTRawMatSizeCode"
        _Qry &= vbCrLf & "   ,A.FNHSysUnitId"
        _Qry &= vbCrLf & "  , U.FTUnitCode"
        _Qry &= vbCrLf & "   ,A.FTDocumentNo"
        _Qry &= vbCrLf & "  ,A.FTPurchaseNo,A.FNQuantity AS FNQuantitySupl"
        _Qry &= vbCrLf & "   ,A.FNQuantity"
        _Qry &= vbCrLf & "   ,B.FTOrderNo"
        _Qry &= vbCrLf & "   ,B.FNQuantity AS FNOrderQuantity"
        _Qry &= vbCrLf & "   ,B.FNQuantity AS FNOrderQuantityOrg"
        _Qry &= vbCrLf & "   ,ISNULL(A.FTStateScanCheck,'0') AS FTStateScanCheck"
        _Qry &= vbCrLf & "   ,ISNULL(B.FTFabricFrontSize,'') AS FTFabricFrontSize"
        _Qry &= vbCrLf & "   ,A.FTBatchNo"
        _Qry &= vbCrLf & "   ,A.FTGrade"
        _Qry &= vbCrLf & "   ,A.FTRollNo"
        _Qry &= vbCrLf & "   FROM"
        _Qry &= vbCrLf & "  (SELECT        FTBarcodeSuplRefNo, FNHSysRawMatId, SUM(FNQuantity) AS FNQuantity, FNHSysUnitId, FTDocumentNo, FTPurchaseNo,MAX(ISNULL(FTStateScanCheck,'0')) AS FTStateScanCheck,MAX(FTBatchNo) AS FTBatchNo,MAX(FTGrade) AS FTGrade,MAX(FTRollNo) AS FTRollNo "
        _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(DocKey) & "' "
        _Qry &= vbCrLf & " AND  ISNULL(FTBarcodeSuplRefNo,'') <> ''"
        _Qry &= vbCrLf & "  GROUP BY FTBarcodeSuplRefNo, FNHSysRawMatId, FNHSysUnitId, FTDocumentNo, FTPurchaseNo) AS A"
        _Qry &= vbCrLf & "  LEFT OUTER JOIN "
        _Qry &= vbCrLf & "  ("
        _Qry &= vbCrLf & "  SELECT        FTInsUser, FDInsDate, FTInsTime, FTUpdUser, FDUpdDate, FTUpdTime, FTBarcodeNo, FNHSysWHId, FNHSysRawMatId, FTOrderNo, FNHSysUnitId, FNPrice, FNQuantity, FTDocumentNo, FTFabricFrontSize, "
        _Qry &= vbCrLf & "  FTBatchNo, FTGrade, FTPurchaseNo, FNHSysCmpId, FTBarcodeSuplRefNo, FTStateScanCheck, FTScanCheckDate, FTScanCheckTime, FTScanCheckBy"
        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(DocKey) & "' "
        _Qry &= vbCrLf & " AND  ISNULL(FTBarcodeSuplRefNo,'') <> ''"
        _Qry &= vbCrLf & "  ) AS B ON A.FTBarcodeSuplRefNo = B.FTBarcodeSuplRefNo"
        _Qry &= vbCrLf & " AND   A.FNHSysRawMatId = B.FNHSysRawMatId"
        _Qry &= vbCrLf & "   INNER Join"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON A.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON A.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
        _Qry &= vbCrLf & " ORDER BY ISNULL(A.FTStateScanCheck,'0') ,A.FTBarcodeSuplRefNo,B.FTOrderNo"

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        Me.ogcdetail.DataSource = dt.Copy

    End Sub


    Private Sub LoadBarcodeInfo(BarKey As String)
        Dim _Qry As String = ""
        Dim _Qty As Double = 0
        Dim _Desc As String = ""
        Dim RSeq As Integer = 0
        Dim dt As DataTable

        _Qry = " SELECT     A.FTReceiveNo, W.FTWHCode, B.FTBarcodeSuplRefNo, B.FTOrderNo ,B.FNQuantity,B.FTStateScanCheck,A.FTPurchaseNo  "
        _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK)  INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS A WITH(NOLOCK) ON B.FTDocumentNo = A.FTReceiveNo LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH(NOLOCK) ON A.FNHSysWHId = W.FNHSysWHId "
        _Qry &= vbCrLf & "  WHERE ISNULL(FTBarcodeSuplRefNo,'')='" & HI.UL.ULF.rpQuoted(BarKey) & "' "
        _Qry &= vbCrLf & " AND  ISNULL(FTBarcodeSuplRefNo,'') <> ''"

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)
        RSeq = 0
        If dt.Rows.Count > 0 Then

            Me.FTReceiveNo.Text = dt.Rows(0)!FTReceiveNo.ToString
            'Me.FNHSysWHId.Text = dt.Rows(0)!FTWHCode.ToString
            'Me.FTPurchaseNo.Text = dt.Rows(0)!FTPurchaseNo.ToString

            _Desc = "" & BarKey & "  For  "
            For Each R As DataRow In dt.Rows

                RSeq = RSeq + 1

                _Qty = _Qty + Double.Parse(Val(R!FNQuantity))

                If RSeq > 1 Then
                    _Desc &= "," & R!FTOrderNo.ToString
                Else
                    _Desc &= R!FTOrderNo.ToString
                End If

            Next


            If CheckDocCloseStock() = False Then

                _Qry = "SELECT TOP 1 A.FTBarcodeNo "
                _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As A INNER JOIN "
                _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B ON A.FTBarcodeNo = B.FTBarcodeNo "
                _Qry &= vbCrLf & "   WHERE  A.FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "'  "
                _Qry &= vbCrLf & "  AND ISNULL(A.FTBarcodeSuplRefNo,'')='" & HI.UL.ULF.rpQuoted(BarKey) & "' "
                _Qry &= vbCrLf & " AND  ISNULL(A.FTBarcodeSuplRefNo,'') <> ''"

                If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") = "" Then

                    _Qry = " INSERT INTO TINVENBarcode_IN( FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FNQuantity,FTDocumentRefNo,FNHSysCmpId)"
                    _Qry &= vbCrLf & "Select        A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FTBarcodeNo, A.FTDocumentNo, A.FNHSysWHId, A.FTOrderNo, A.FNQuantity,A.FTDocumentNo, A.FNHSysCmpId "
                    _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode  As A With (NOLOCK) LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN As B On A.FTBarcodeNo = B.FTBarcodeNo And A.FTDocumentNo = B.FTDocumentNo"
                    _Qry &= vbCrLf & "   WHERE  A.FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "'  "
                    _Qry &= vbCrLf & "  AND ISNULL(A.FTBarcodeSuplRefNo,'')='" & HI.UL.ULF.rpQuoted(BarKey) & "' "
                    _Qry &= vbCrLf & " AND  ISNULL(A.FTBarcodeSuplRefNo,'') <> ''"
                    _Qry &= vbCrLf & " AND  B.FTBarcodeNo Is NULL"
                    _Qry &= vbCrLf & "  UPDATE   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode SET "
                    _Qry &= vbCrLf & "   FTStateScanCheck='1' "
                    _Qry &= vbCrLf & "   ,FTScanCheckDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & "   ,FTScanCheckTime=" & HI.UL.ULDate.FormatTimeDB & " "
                    _Qry &= vbCrLf & "   ,FTScanCheckBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                    _Qry &= vbCrLf & "   WHERE  FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "'  "
                    _Qry &= vbCrLf & "   AND ISNULL(FTBarcodeSuplRefNo,'')='" & HI.UL.ULF.rpQuoted(BarKey) & "' "
                    _Qry &= vbCrLf & "   AND  ISNULL(FTBarcodeSuplRefNo,'') <> ''"

                    ''-------------------------Update Data Receive----------------------
                    '_Qry &= vbCrLf & "   UPDATE RO SET "
                    '_Qry &= vbCrLf & "   FNQuantity = Convert(numeric(18, 4), ISNULL(D.FNQuantity, 0) * FNConvRatio)"
                    '_Qry &= vbCrLf & " 	,FNQuantityStock=ISNULL(D.FNQuantity,0)"
                    '_Qry &= vbCrLf & " ,FNNetAmt=Convert(numeric(18,2),(Convert(numeric(18,4),ISNULL(D.FNQuantity,0) * FNConvRatio)) *FNNetPrice)"
                    '_Qry &= vbCrLf & " ,FNNetStockAmt=Convert(numeric(18,2),(Convert(numeric(18,4),ISNULL(D.FNQuantity,0) )) *FNPricePerStock)"
                    '_Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RO INNER JOIN"
                    '_Qry &= vbCrLf & " (SELECT        A.FTDocumentNo, A.FNHSysRawMatId, A.FTOrderNo, SUM(A.FNQuantity) AS FNQuantity"
                    '_Qry &= vbCrLf & "  FROM          (SELECT        FTDocumentNo, FNHSysRawMatId"
                    '_Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS A "
                    '_Qry &= vbCrLf & "   WHERE  FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "'  "
                    '_Qry &= vbCrLf & "   AND ISNULL(FTBarcodeSuplRefNo,'')='" & HI.UL.ULF.rpQuoted(BarKey) & "' "
                    '_Qry &= vbCrLf & "   AND  ISNULL(FTBarcodeSuplRefNo,'') <> ''"
                    '_Qry &= vbCrLf & " 	GROUP BY FTDocumentNo, FNHSysRawMatId"
                    '_Qry &= vbCrLf & " 	) AS M INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS A ON M.FTDocumentNo = A.FTDocumentNo AND M.FNHSysRawMatId =A.FNHSysRawMatId"
                    '_Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS B ON A.FTBarcodeNo = B.FTBarcodeNo AND A.FTDocumentNo = B.FTDocumentNo"
                    '_Qry &= vbCrLf & "  GROUP BY  A.FTDocumentNo, A.FNHSysRawMatId, A.FTOrderNo) AS D"
                    '_Qry &= vbCrLf & "  ON RO.FTReceiveNo = D.FTDocumentNo "
                    '_Qry &= vbCrLf & "  AND RO.FTOrderNo = D.FTOrderNo "
                    '_Qry &= vbCrLf & "  AND RO.FNHSysRawMatId =D.FNHSysRawMatId "
                    '_Qry &= vbCrLf & "  WHERE RO.FTReceiveNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "
                    '_Qry &= vbCrLf & "    UPDATE RDO SET "
                    '_Qry &= vbCrLf & "     FNQuantity = Convert(numeric(18, 4), ISNULL(D.FNQuantity, 0))"
                    '_Qry &= vbCrLf & " ,FNNetAmt=Convert(numeric(18,2),(Convert(numeric(18,4),ISNULL(D.FNQuantity,0) )) *FNNetPrice)"
                    '_Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS RDO INNER JOIN"
                    '_Qry &= vbCrLf & "  (SELECT        A.FTReceiveNo, A.FNHSysRawMatId, SUM(A.FNQuantity) AS FNQuantity"
                    '_Qry &= vbCrLf & "  FROM          (SELECT        FTDocumentNo, FNHSysRawMatId,FTOrderNo "
                    '_Qry &= vbCrLf & " 	FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS A "
                    '_Qry &= vbCrLf & "   WHERE  FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "'  "
                    ''  _Qry &= vbCrLf & "   AND ISNULL(FTBarcodeSuplRefNo,'')='" & HI.UL.ULF.rpQuoted(BarKey) & "' "
                    '_Qry &= vbCrLf & "   AND  ISNULL(FTBarcodeSuplRefNo,'') <> ''"
                    '_Qry &= vbCrLf & " 	GROUP BY FTDocumentNo, FNHSysRawMatId,FTOrderNo"
                    '_Qry &= vbCrLf & " 	) AS M INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS A ON M.FTDocumentNo = A.FTReceiveNo AND M.FNHSysRawMatId =A.FNHSysRawMatId AND M.FTOrderNo =A.FTOrderNo  "
                    '_Qry &= vbCrLf & "  GROUP BY  A.FTReceiveNo, A.FNHSysRawMatId) AS D"
                    '_Qry &= vbCrLf & "  ON RDO.FTReceiveNo = D.FTReceiveNo"
                    '_Qry &= vbCrLf & "  AND RDO.FNHSysRawMatId =D.FNHSysRawMatId "
                    '_Qry &= vbCrLf & "  WHERE RDO.FTReceiveNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "

                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)

                Else
                    HI.MG.ShowMsg.mInfo("Barcode นี้ ถูกนำไปใช้งานแล้ว ไม่สามารถทำการลบหรือแก้ไขได้ !!!", 1708010401, Me.Text)
                End If

            End If

            Call LoadData(FTReceiveNo.Text)

            'Try
            '    Me.ogvdetail.FocusedRowHandle =ogvdetail.
            'Catch ex As Exception
            'End Try

        Else

            _Desc = HI.MG.ShowMsg.GetMessage("ไม่พบข้อมูล Barcode กรุณาทำการตรวจสอบ !!!", 1409180089)
            Call LoadData("")

        End If

        Me.olbdesc.Text = _Desc
        Me.olbqty.Text = Format(_Qty, "#," & HI.ST.Config.QtyFormat)
    End Sub

#End Region

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpID
        Me.FTBarcodeNo.Focus()
    End Sub

    Private Sub wReScanCheckImportNanyang_Load(sender As Object, e As EventArgs) Handles Me.Load
        FTBarcodeNo.EnterMoveNextControl = False
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpID
    End Sub

    Private Sub ogvdetail_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvdetail.RowCellStyle
        Try
            With ogvdetail
                Try
                    Select Case ("" & .GetRowCellValue(e.RowHandle, "FTStateScanCheck").ToString)
                        Case "1"
                            e.Appearance.ForeColor = System.Drawing.Color.Green

                    End Select

                Catch ex As Exception
                End Try
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTBarcodeNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FTBarcodeNo.KeyDown
        Select Case e.KeyCode
            Case System.Windows.Forms.Keys.Enter

                If FTBarcodeNo.Text.Trim <> "" Then

                    LoadBarcodeInfo(FTBarcodeNo.Text.Trim)
                    FTBarcodeNo.Focus()
                    FTBarcodeNo.SelectAll()
                End If

        End Select
    End Sub

    Private Sub ogvdetail_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvdetail.CellMerge
        Try
            With Me.ogvdetail

                Select Case e.Column.FieldName
                    Case "FTSelect", "FTFabricFrontSize", "FNQuantity"


                        If "" & .GetRowCellValue(e.RowHandle1, "FTBarcodeSuplRefNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTBarcodeSuplRefNo").ToString _
                          And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                End Select


                'If .GetRowCellValue(e.RowHandle1, "FTBarcodeSuplRefNo").ToString = .GetRowCellValue(e.RowHandle2, "FTBarcodeSuplRefNo").ToString Then
                '    e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                'Else
                '    e.Merge = False
                'End If

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Function UpdateData(ByRef StateMail As Boolean) As Boolean
        Dim dt As DataTable
        With CType(ogcdetail.DataSource, DataTable)
            .AcceptChanges()
            dt = .Copy
        End With
        StateMail = False
        If dt.Rows.Count > 0 Then

            Dim _Qry As String = ""

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_INVEN)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try

                For Each R As DataRow In dt.Rows

                    _Qry = "SELECT A.FTBarcodeNo "
                    _Qry &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode As A INNER JOIN "
                    _Qry &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS B ON A.FTBarcodeNo = B.FTBarcodeNo "
                    _Qry &= vbCrLf & "   WHERE  A.FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "'  "
                    _Qry &= vbCrLf & "  AND ISNULL(A.FTBarcodeSuplRefNo,'')='" & HI.UL.ULF.rpQuoted(R!FTBarcodeSuplRefNo.ToString) & "' "
                    _Qry &= vbCrLf & "  AND ISNULL(A.FTOrderNo,'')='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                    _Qry &= vbCrLf & " AND  ISNULL(A.FTBarcodeSuplRefNo,'') <> ''"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                        _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN( FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FNQuantity,FTDocumentRefNo,FNHSysCmpId)"
                        _Qry &= vbCrLf & "SELECT        A.FTInsUser, A.FDInsDate, A.FTInsTime, A.FTBarcodeNo, A.FTDocumentNo, A.FNHSysWHId, A.FTOrderNo, A.FNQuantity,A.FTDocumentNo, A.FNHSysCmpId "
                        _Qry &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode  AS A WITH (NOLOCK) LEFT OUTER JOIN"
                        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS B ON A.FTBarcodeNo = B.FTBarcodeNo AND A.FTDocumentNo = B.FTDocumentNo"
                        _Qry &= vbCrLf & "   WHERE  A.FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "'  "
                        _Qry &= vbCrLf & "  AND ISNULL(A.FTBarcodeSuplRefNo,'')='" & HI.UL.ULF.rpQuoted(R!FTBarcodeSuplRefNo.ToString) & "' "
                        _Qry &= vbCrLf & "  AND ISNULL(A.FTOrderNo,'')='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                        _Qry &= vbCrLf & " AND  ISNULL(A.FTBarcodeSuplRefNo,'') <> ''"
                        _Qry &= vbCrLf & " AND  B.FTBarcodeNo Is NULL"
                        _Qry &= vbCrLf & "  UPDATE   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode SET "
                        _Qry &= vbCrLf & "   FTStateScanCheck='1' "
                        _Qry &= vbCrLf & "   ,FTScanCheckDate=" & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & "   ,FTScanCheckTime=" & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & "   ,FTScanCheckBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & "   ,FNQuantity=" & Val(R!FNOrderQuantity.ToString) & " "
                        _Qry &= vbCrLf & "   WHERE  FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "'  "
                        _Qry &= vbCrLf & "   AND ISNULL(FTBarcodeSuplRefNo,'')='" & HI.UL.ULF.rpQuoted(R!FTBarcodeSuplRefNo.ToString) & "' "
                        _Qry &= vbCrLf & "  AND ISNULL(FTOrderNo,'')='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                        _Qry &= vbCrLf & "   AND  ISNULL(FTBarcodeSuplRefNo,'') <> ''"
                        _Qry &= vbCrLf & "  UPDATE B SET FNQuantity=A.FNQuantity "
                        _Qry &= vbCrLf & "  FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode A "
                        _Qry &= vbCrLf & "  INNER JOIN    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN  As B On  A.FTBarcodeNo = B.FTBarcodeNo And A.FTDocumentNo = B.FTDocumentNo "
                        _Qry &= vbCrLf & "   WHERE  A.FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "'  "
                        _Qry &= vbCrLf & "   AND ISNULL(A.FTBarcodeSuplRefNo,'')='" & HI.UL.ULF.rpQuoted(R!FTBarcodeSuplRefNo.ToString) & "' "
                        _Qry &= vbCrLf & "  AND ISNULL(A.FTOrderNo,'')='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                        _Qry &= vbCrLf & "   AND  ISNULL(FTBarcodeSuplRefNo,'') <> ''"

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            Return False
                        End If

                    End If

                Next

                '-------------------------Update Data Receive----------------------
                _Qry = "   UPDATE RO SET "
                _Qry &= vbCrLf & "   FNQuantity = Convert(numeric(18, 4), ISNULL(D.FNQuantity, 0) / FNConvRatio)"
                _Qry &= vbCrLf & " 	,FNQuantityStock=ISNULL(D.FNQuantity,0)"
                _Qry &= vbCrLf & " ,FNNetAmt=Convert(numeric(18,2),(Convert(numeric(18,4),ISNULL(D.FNQuantity,0) / FNConvRatio)) *FNNetPrice)"
                _Qry &= vbCrLf & " ,FNNetStockAmt=Convert(numeric(18,2),(Convert(numeric(18,4),ISNULL(D.FNQuantity,0) )) * FNPricePerStock)"
                _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RO INNER JOIN"
                _Qry &= vbCrLf & " (SELECT        A.FTDocumentNo, A.FNHSysRawMatId, A.FTOrderNo, SUM(A.FNQuantity) AS FNQuantity"
                _Qry &= vbCrLf & "  FROM          (SELECT        FTDocumentNo, FNHSysRawMatId"
                _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS A "
                _Qry &= vbCrLf & "   WHERE  FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "'  "
                _Qry &= vbCrLf & "   AND  ISNULL(FTBarcodeSuplRefNo,'') <> ''"
                _Qry &= vbCrLf & " 	GROUP BY FTDocumentNo, FNHSysRawMatId"
                _Qry &= vbCrLf & " 	) AS M INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS A ON M.FTDocumentNo = A.FTDocumentNo AND M.FNHSysRawMatId =A.FNHSysRawMatId"
                _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS B ON A.FTBarcodeNo = B.FTBarcodeNo AND A.FTDocumentNo = B.FTDocumentNo"
                _Qry &= vbCrLf & "  GROUP BY  A.FTDocumentNo, A.FNHSysRawMatId, A.FTOrderNo) AS D"
                _Qry &= vbCrLf & "  ON RO.FTReceiveNo = D.FTDocumentNo "
                _Qry &= vbCrLf & "  AND RO.FTOrderNo = D.FTOrderNo "
                _Qry &= vbCrLf & "  AND RO.FNHSysRawMatId =D.FNHSysRawMatId "
                _Qry &= vbCrLf & "  WHERE RO.FTReceiveNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "
                _Qry &= vbCrLf & "    UPDATE RDO SET "
                _Qry &= vbCrLf & "     FNQuantity = Convert(numeric(18, 4), ISNULL(D.FNQuantity, 0))"
                _Qry &= vbCrLf & " ,FNNetAmt=Convert(numeric(18,2),(Convert(numeric(18,4),ISNULL(D.FNQuantity,0) )) * FNNetPrice)"
                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS RDO INNER JOIN"
                _Qry &= vbCrLf & "  (SELECT        A.FTReceiveNo, A.FNHSysRawMatId, SUM(A.FNQuantity) AS FNQuantity"
                _Qry &= vbCrLf & "  FROM          (SELECT        FTDocumentNo, FNHSysRawMatId,FTOrderNo "
                _Qry &= vbCrLf & " 	FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS A "
                _Qry &= vbCrLf & "   WHERE  FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "'  "
                _Qry &= vbCrLf & "   AND  ISNULL(FTBarcodeSuplRefNo,'') <> ''"
                _Qry &= vbCrLf & " 	GROUP BY FTDocumentNo, FNHSysRawMatId,FTOrderNo"
                _Qry &= vbCrLf & " 	) AS M INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS A ON M.FTDocumentNo = A.FTReceiveNo AND M.FNHSysRawMatId =A.FNHSysRawMatId AND M.FTOrderNo =A.FTOrderNo "
                _Qry &= vbCrLf & "  GROUP BY  A.FTReceiveNo, A.FNHSysRawMatId) AS D"
                _Qry &= vbCrLf & "  ON RDO.FTReceiveNo = D.FTReceiveNo"
                _Qry &= vbCrLf & "  AND RDO.FNHSysRawMatId =D.FNHSysRawMatId "
                _Qry &= vbCrLf & "  WHERE RDO.FTReceiveNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Return False
                End If

                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive SET FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & ",FDReceiveDate= CASE WHEN ISNULL(FDReceiveDate,'')='' THEN " & HI.UL.ULDate.FormatDateDB & " ELSE FDReceiveDate END"
                _Qry &= vbCrLf & " WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Return False
                End If


                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode SET FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & ""
                _Qry &= vbCrLf & " WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Return False
                End If


                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN SET FNHSysWHId=" & Val(FNHSysWHId.Properties.Tag.ToString) & ""
                _Qry &= vbCrLf & " WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                    Return False
                End If

                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                StateMail = CheckSendMailAppRcvOver()
            Catch ex As Exception
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End Try

        End If

        dt.Dispose()
        Return True
    End Function

    Private Function CheckSendMailAppRcvOver() As Boolean

        Dim _Qry As String = ""
        Dim _UserMailTo As String = ""
        Dim _Str As String = ""

        Dim PoNo As String = ""
        Dim RcvDate As String = ""
        Dim RcvBy As String = ""
        Dim RcvRemark As String = ""
        Dim dtrcv As DataTable
        Dim _RcvOverQty As Decimal = 0
        Dim _RcvQty As Decimal = 0
        Dim _SysMatID As Integer = 0


        _Str = " SELECT  (CASE WHEN ISNULL(FNRcvQty,0) > 0 THEN '1' ELSE '0' END) AS FTStateSelect"
        _Str &= vbCrLf & "    ,P.FNHSysRawMatId,"
        _Str &= vbCrLf & "   P.FNQuantity,"
        _Str &= vbCrLf & "  ISNULL(RCV.FNRcvHisQty,0) As FNRcvHisQty,"
        _Str &= vbCrLf & "  (P.FNQuantity-ISNULL(RCV.FNRcvHisQty,0)) AS FNPOBalQty,"
        _Str &= vbCrLf & "  Convert(numeric(18,4),ISNULL(FNRcvQty,0)) AS FNRcvQty"
        _Str &= vbCrLf & ",'' As FTStateRcvOver"
        _Str &= vbCrLf & ",'0' As FTStateSendAppRcv"
        _Str &= vbCrLf & ",Convert(numeric(18,4),0) As FNRcvQtyPass"
        _Str &= vbCrLf & ",Convert(numeric(18,4),0) As FNRcvQtyOver"
        _Str &= vbCrLf & "   FROM "
        _Str &= vbCrLf & " (SELECT        FTPurchaseNo, FNHSysRawMatId, FNHSysUnitId,Max(FTFabricFrontSize) AS FTFabricFrontSize"

        _Str &= vbCrLf & ", SUM(FNQuantity) AS FNQuantity,ISNULL(FNSurchangeAmt,0) AS FNSurchangeAmt"

        _Str &= vbCrLf & " FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS P WITH (NOLOCK)"
        _Str &= vbCrLf & " WHERE        (FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "')"
        _Str &= vbCrLf & " GROUP BY FTPurchaseNo, FNHSysRawMatId, FNHSysUnitId,FNPrice, FNDisPer, FNDisAmt, ISNULL(FNSurchangePerUnit,0),ISNULL(FNSurchangeAmt,0)) AS P "
        _Str &= vbCrLf & "  INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial as IM WITH(NOLOCK ) ON P.FNHSysRawMatId = IM.FNHSysRawMatId "
        _Str &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit as U WITH(NOLOCK) ON P.FNHSysUnitId = U.FNHSysUnitId "
        _Str &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId"
        _Str &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
        _Str &= vbCrLf & "  INNER JOIN (SELECT        RH.FTPurchaseNo, RD.FNHSysRawMatId,MAX(RD.FTFabricFrontSize) AS FTFabricFrontSize"
        _Str &= vbCrLf & "  ,SUM(CASE WHEN RH.FTReceiveNo<>N'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' Then RD.FNQuantity  Else 0 END)  AS FNRcvHisQty ,"
        _Str &= vbCrLf & " SUM(CASE WHEN RH.FTReceiveNo=N'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' Then RD.FNQuantity  Else 0 END)  AS FNRcvQty "
        _Str &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS RH WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS RD WITH (NOLOCK) ON RH.FTReceiveNo = RD.FTReceiveNo"
        _Str &= vbCrLf & " WHERE        (RH.FTPurchaseNo = N'" & HI.UL.ULF.rpQuoted(Me.FTPurchaseNo.Text) & "')"
        _Str &= vbCrLf & " GROUP BY RH.FTPurchaseNo, RD.FNHSysRawMatId ) As RCV"
        _Str &= vbCrLf & " ON P.FTPurchaseNo = Rcv.FTPurchaseNo AND P.FNHSysRawMatId = Rcv.FNHSysRawMatId "
        _Str &= vbCrLf & " ORDER BY   IM.FTRawMatCode ,  C.FTRawMatColorCode, S.FTRawMatSizeCode"
        dtrcv = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)

        For Each Rindx As DataRow In dtrcv.Rows


            _RcvQty = Val(Rindx!FNRcvQty.ToString)
            _SysMatID = Val(Rindx!FNHSysRawMatId.ToString)

            _RcvOverQty = Me.CheckReceiveOver(Me.FTPurchaseNo.Text, Me.FTReceiveNo.Text, _SysMatID, _RcvQty)

            If _RcvOverQty > 0 Then

                Rindx!FNRcvQtyPass = (_RcvQty - _RcvOverQty)
                Rindx!FNRcvQtyOver = _RcvOverQty
                Rindx!FTStateSendAppRcv = "1"

                _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail SET "
                _Qry &= vbCrLf & " FNNetAmt=0"
                _Qry &= vbCrLf & ",FNQuantity=0 "
                _Qry &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                _Qry &= vbCrLf & " AND    FNHSysRawMatId =" & _SysMatID & " "

                _Qry &= vbCrLf & " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode SET FNHSysWHId=0"
                _Qry &= vbCrLf & " WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "
                _Qry &= vbCrLf & " AND    FNHSysRawMatId =" & _SysMatID & " "

                _Qry &= vbCrLf & " UPDATE A SET FNHSysWHId=0 "
                _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS A "
                _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode  AS B ON A.FTDocumentNo = B.FTDocumentNo AND A.FTBarcodeNo = B.FTBarcodeNo "
                _Qry &= vbCrLf & " WHERE A.FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "
                _Qry &= vbCrLf & " AND    B.FNHSysRawMatId =" & _SysMatID & " "

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_INVEN)

            End If

        Next
        Dim StateMail As Boolean = False
        If dtrcv.Select("FNRcvQty > 0 AND FTStateSendAppRcv='1' ").Length > 0 Then
            PoNo = FTPurchaseNo.Text
            _Qry = "SELECT TOP 1  CASE WHEN ISNULL(U.FTStateHelp,'') ='1' THEN  CASE WHEN ISNULL(A.FTSendAppBy,'')='' THEN A.FTPurchaseBy  ELSE  A.FTSendAppBy END ELSE A.FTPurchaseBy END AS FTPurchaseBy "
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U WITH(NOLOCK) ON A.FTPurchaseBy = U.FTUserName"
            _Qry &= vbCrLf & "  WHERE A.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PoNo) & "' "

            _UserMailTo = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            If _UserMailTo <> "" Then

                Dim tmpsubject As String = ""
                Dim tmpmessage As String = ""

                tmpsubject = "Send Approve Receive Over Receive No " & Me.FTReceiveNo.Text & "  From Warehouse " & FNHSysWHId.Text & "   "
                tmpmessage = "Send Approve Receive Over Receive No " & Me.FTReceiveNo.Text & "  From Warehouse " & FNHSysWHId.Text & "   "
                tmpmessage &= vbCrLf & "Date :" & RcvDate
                tmpmessage &= vbCrLf & "By :" & RcvBy
                tmpmessage &= vbCrLf & "PO No :" & PoNo
                tmpmessage &= vbCrLf & "Note :" & RcvRemark

                If HI.Mail.ClsSendMail.SendMail(HI.ST.UserInfo.UserName, _UserMailTo, tmpsubject, tmpmessage, 4, Me.FTReceiveNo.Text) Then

                    Dim FNHSysMailAppId As Integer = HI.TL.RunID.GetRunNoID("[HITECH_INVENTORY].dbo.TINVENMailSendAppRcvOver", "FNHSysMailAppId", Conn.DB.DataBaseName.DB_INVEN).ToString()

                    For Each R As DataRow In dtrcv.Select("FNRcvQty > 0 AND FTStateSendAppRcv='1' ")

                        _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail SET "
                        _Str &= vbCrLf & "FNNetAmt=0"
                        _Str &= vbCrLf & ",FNQuantity=0 "
                        _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                        _Str &= vbCrLf & " AND    FNHSysRawMatId =" & Val(R!FNHSysRawMatId.ToString) & " "
                        _Str &= vbCrLf & "  UPDATE  A SET FNHSysWHId=0 "
                        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS A "
                        _Str &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B ON A.FTBarcodeNo = B.FTBarcodeNo AND A.FTDocumentNo = B.FTDocumentNo  "
                        _Str &= vbCrLf & " WHERE   B.FTDocumentNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                        _Str &= vbCrLf & " AND     B.FNHSysRawMatId =" & Val(R!FNHSysRawMatId.ToString) & " "
                        _Str &= vbCrLf & " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode SET "
                        _Str &= vbCrLf & " FNHSysWHId=0"
                        _Str &= vbCrLf & " WHERE   FTDocumentNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                        _Str &= vbCrLf & " AND    FNHSysRawMatId =" & Val(R!FNHSysRawMatId.ToString) & " "

                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_INVEN)

                        _Qry = " SELECT TOP 1 FTReceiveNo "
                        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENMailSendAppRcvOver AS A with(nolock) "
                        _Qry &= vbCrLf & " WHERE FTReceiveNo='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                        _Qry &= vbCrLf & " AND FNHSysRawMatId=" & Integer.Parse(Val(R!FNHSysRawMatId.ToString)) & " "
                        _Qry &= vbCrLf & " AND NOT (ISNULL(FTStateApprove,'0') IN ('1','2')) "

                        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") = "" Then

                            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENMailSendAppRcvOver"
                            _Qry &= vbCrLf & " (FTSendUser, FDSendDate, FTSendTime, FTToUser, FNHSysMailAppId, FTReceiveNo, FNHSysRawMatId, FNPOQuantity, FNRcvHisQuantity, FNRcvQtyPass, FNRcvQtyOver, FNTotalRcvQty)"
                            _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                            _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_UserMailTo) & "' "
                            _Qry &= vbCrLf & "," & Integer.Parse(Val(FNHSysMailAppId)) & " "
                            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                            _Qry &= vbCrLf & "," & Val(R!FNHSysRawMatId.ToString) & " "
                            _Qry &= vbCrLf & "," & Val(R!FNQuantity.ToString) & " "
                            _Qry &= vbCrLf & "," & Val(R!FNRcvHisQty.ToString) & " "
                            _Qry &= vbCrLf & "," & Val(R!FNRcvQtyPass.ToString) & " "
                            _Qry &= vbCrLf & "," & Val(R!FNRcvQtyOver.ToString) & " "
                            _Qry &= vbCrLf & "," & Val(R!FNRcvQty.ToString) & " "

                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_INVEN)

                        End If

                        StateMail = True
                    Next

                End If

            End If

        End If




        Try
            dtrcv.Dispose()
        Catch ex As Exception

        End Try


        Return StateMail
    End Function

    Private Function CheckReceiveOver(PONO As String, RCVNO As String, MatID As Integer, RcvQty As Double) As Double
        Dim _RcvOverQty As Double = 0
        Dim _POQty As Double = 0
        Dim _RtsQty As Double = 0
        Dim _RcvQty As Double = RcvQty
        Dim _Str As String = ""
        Dim _dt As DataTable
        Dim _FNRcvOverPercent As Double = 0
        Dim _AdvRcvOver As Double = 0

        _Str = " SELECT A.POFNQuantity,ISNULL(B.RcvFNQuantity,0) AS RcvFNQuantity,ISNULL(C.RTSFNQuantity,0) AS RTSFNQuantity"
        _Str &= vbCrLf & "  FROM"
        _Str &= vbCrLf & "  (SELECT      FTPurchaseNo, FNHSysRawMatId, SUM(FNQuantity) AS POFNQuantity"
        _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS P WITH(NOLOCK)"
        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PONO) & "'  "
        _Str &= vbCrLf & " AND  FNHSysRawMatId=" & MatID & " "
        _Str &= vbCrLf & "  GROUP BY FTPurchaseNo, FNHSysRawMatId) AS A LEFT OUTER JOIN "
        _Str &= vbCrLf & "  (SELECT        RH.FTPurchaseNo, RD.FNHSysRawMatId, SUM(RD.FNQuantity) AS RcvFNQuantity"
        _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS RH WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail AS RD WITH (NOLOCK) ON RH.FTReceiveNo = RD.FTReceiveNo"
        _Str &= vbCrLf & "  WHERE        (RH.FTReceiveNo <> '" & HI.UL.ULF.rpQuoted(RCVNO) & "')"
        _Str &= vbCrLf & " AND  RH.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PONO) & "'  "
        _Str &= vbCrLf & " AND  RD.FNHSysRawMatId=" & MatID & " "
        _Str &= vbCrLf & "  GROUP BY RH.FTPurchaseNo, RD.FNHSysRawMatId) AS B"
        _Str &= vbCrLf & "  ON A.FTPurchaseNo = B.FTPurchaseNo"
        _Str &= vbCrLf & "   AND A.FNHSysRawMatId = B.FNHSysRawMatId"

        _Str &= vbCrLf & "  LEFT OUTER JOIN ( SELECT FTPurchaseNo,FNHSysRawMatId,Convert(numeric(18," & HI.ST.Config.QtyDigit & "),SUM(FNQuantity/FNConvRatio )) AS  RTSFNQuantity"
        _Str &= vbCrLf & "  FROM"
        _Str &= vbCrLf & "  (SELECT        H.FTPurchaseNo, B.FNHSysRawMatId, SUM(BO.FNQuantity) AS FNQuantity, RC.FNConvRatio"
        _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS BO WITH(NOLOCK)  ON B.FTBarcodeNo = BO.FTBarcodeNo INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS RC WITH(NOLOCK)  ON B.FTDocumentNo = RC.FTReceiveNo AND B.FTOrderNo = RC.FTOrderNo AND B.FNHSysRawMatId = RC.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReturnToSupplier AS H WITH (NOLOCK) ON BO.FTDocumentNo = H.FTReturnSuplNo"
        _Str &= vbCrLf & " WHERE  H.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PONO) & "'  "
        _Str &= vbCrLf & "  GROUP BY  H.FTPurchaseNo, B.FNHSysRawMatId,RC.FNConvRatio) AS RTS"
        _Str &= vbCrLf & "  GROUP BY FTPurchaseNo,FNHSysRawMatId ) AS C"
        _Str &= vbCrLf & "  ON A.FTPurchaseNo = C.FTPurchaseNo"
        _Str &= vbCrLf & "   AND A.FNHSysRawMatId = C.FNHSysRawMatId"
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)

        If _dt.Rows.Count > 0 Then

            For Each R As DataRow In _dt.Rows
                _POQty = R!POFNQuantity
                _RtsQty = R!RTSFNQuantity
                _RcvQty = (_RcvQty + R!RcvFNQuantity) - R!RTSFNQuantity
                Exit For
            Next

            If _RcvQty > _POQty Then

                _Str = "   SELECT    TOP 1  CFG.FNRcvOverPercent"
                _Str &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM INNER JOIN"
                _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM ON IM.FTRawMatCode = MM.FTMainMatCode INNER JOIN"
                _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMConfigReceiveOver AS CFG ON MM.FNHSysMatGrpId = CFG.FNHSysMatGrpId"
                _Str &= vbCrLf & " WHERE IM.FNHSysRawMatId= " & MatID & "  "
                _Str &= vbCrLf & " AND CFG.FNStartRcvQty<=" & _POQty & "  AND  CFG.FNEndRcvQty>=" & _POQty & ""

                _FNRcvOverPercent = Double.Parse(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "-1"))

                If _FNRcvOverPercent > 0 Then
                    _AdvRcvOver = CDbl(Format(((_POQty * _FNRcvOverPercent) / 100), HI.ST.Config.QtyFormat))
                    _RcvOverQty = _RcvQty - (_POQty + _AdvRcvOver)

                    If _RcvOverQty > 0 Then
                        ' HI.MG.ShowMsg.mProcessError(140200001, "คุณไม่สามารถรับเกิน PO ได้ !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning, "You Can Reveive Over " & Format(_AdvRcvOver, HI.ST.Config.QtyFormat) & " Only., Over " & Format(_RcvOverQty, HI.ST.Config.QtyFormat))
                    Else
                        _RcvOverQty = 0
                    End If

                Else
                    _RcvOverQty = _RcvQty - _POQty
                    ' HI.MG.ShowMsg.mProcessError(140200002, "คุณไม่สามารถรับเกิน PO ได้ !!!", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning, "Over " & Format(_RcvOverQty, HI.ST.Config.QtyFormat))
                End If

            End If

        End If

        Return _RcvOverQty
    End Function
    Private Function CheckDocCloseStock() As Boolean
        Dim _Qry As String = ""
        Dim FDReceiveDate As String = ""
        Dim DocOldSysWHId As Integer = 0

        Dim dt As DataTable

        _Qry = "SELECT TOP 1 A.FDReceiveDate,A.FNHSysWHId "
        _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "   WHERE  A.FTReceiveNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "'  "

        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        For Each R As DataRow In dt.Rows
            FDReceiveDate = R!FDReceiveDate.ToString
            DocOldSysWHId = Val(R!FNHSysWHId.ToString)
        Next

        dt.Dispose()

        If FDReceiveDate <> "" Then

            If DocOldSysWHId <> 0 Then
                If StockValidate.CheckCloseStock(Integer.Parse(Val(DocOldSysWHId)), FDReceiveDate) = True Then
                    Return True
                End If
            End If

            If StockValidate.CheckCloseStock(Integer.Parse(Val(FNHSysWHId.Properties.Tag.ToString)), FDReceiveDate) = True Then
                Return True
            End If

        End If
        Return False
    End Function

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsaveconfirm.Click
        If Not (Me.ogcdetail.DataSource Is Nothing) Then

            If FTReceiveNo.Text <> "" Then
                If FNHSysWHId.Text <> "" Then
                    Dim _statesave As Boolean = False
                    Dim dt As DataTable

                    With CType(ogcdetail.DataSource, DataTable)
                        .AcceptChanges()
                        dt = .Copy
                    End With

                    If dt.Rows.Count > 0 Then
                        _statesave = True
                    End If

                    dt.Dispose()

                    If _statesave Then
                        Dim _Qry As String = ""
                        Dim FDReceiveDate As String = ""
                        Dim DocOldSysWHId As Integer = 0

                        _Qry = "SELECT TOP 1 A.FTBarcodeNo "
                        _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS A WITH(NOLOCK)"
                        _Qry &= vbCrLf & "   WHERE  A.FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "'  "

                        If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
                            HI.MG.ShowMsg.mInfo("ข้อมูลเอกสารนี้ ถูกนำไปใช้งานแล้ว ไม่สามารถทำการลบหรือแก้ไขได้ !!!", 1708010403, Me.Text)
                            Exit Sub
                        End If

                        If CheckDocCloseStock() Then Exit Sub

                        If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, Me.Text) = True Then

                            Dim spls As New HI.TL.SplashScreen("Saving..... Please wait.")
                            Dim StateMail As Boolean
                            If UpdateData(StateMail) Then
                                Call LoadData(FTReceiveNo.Text)
                                spls.Close()

                                If StateMail Then
                                    HI.MG.ShowMsg.mInfo("ระบบทำการบันทึก และ ทำการส่ง Mail เพื่อการอนุมัติรับเกิน เรียบร้อยแล้ว !!!! ", 17110102251, Me.Text,, System.Windows.Forms.MessageBoxIcon.Information)
                                Else
                                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                End If

                            Else
                                spls.Close()
                            End If

                        End If

                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysWHId_lbl.Text)
                    FNHSysWHId.Focus()
                End If


            End If


        End If
    End Sub

    Private Sub RepositoryQuantity_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryQuantity.EditValueChanging

        Try

            If IsNumeric(e.NewValue) Then
                If e.NewValue < 0 Then
                    e.Cancel = True
                Else
                    e.Cancel = False
                End If
            Else
                e.Cancel = False
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub FTBarcodeNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTBarcodeNo.EditValueChanged

    End Sub

    Private Sub FTReceiveNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTReceiveNo.EditValueChanged
        If FTReceiveNo.Text.Trim <> "" Then
            FNHSysWHId.Properties.ReadOnly = True


            Dim _Qry As String
            Dim dt As DataTable

            _Qry = " SELECT     A.FTReceiveNo, W.FTWHCode,A.FTPurchaseNo  "
            _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive AS A WITH(NOLOCK)  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS W WITH(NOLOCK) ON A.FNHSysWHId = W.FNHSysWHId "
            _Qry &= vbCrLf & "  WHERE A.FTReceiveNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "' "

            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

            If dt.Rows.Count > 0 Then

                ' Me.FTReceiveNo.Text = dt.Rows(0)!FTReceiveNo.ToString
                Me.FNHSysWHId.Text = dt.Rows(0)!FTWHCode.ToString
                Me.FTPurchaseNo.Text = dt.Rows(0)!FTPurchaseNo.ToString

            End If

            dt.Dispose()

            _Qry = "SELECT TOP 1 A.FTBarcodeNo "
            _Qry &= vbCrLf & " FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS A WITH(NOLOCK)"
            _Qry &= vbCrLf & "   WHERE  A.FTDocumentRefNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "'  "

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_INVEN, "") <> "" Then
                FNHSysWHId.Properties.Buttons(0).Enabled = False
            Else
                FNHSysWHId.Properties.Buttons(0).Enabled = True
            End If

            Call LoadData(FTReceiveNo.Text)

            Me.FTStateSelectAll.Checked = False

        Else
            FNHSysWHId.Properties.ReadOnly = True
            FNHSysWHId.Properties.Buttons(0).Enabled = False
        End If

    End Sub

    Private Sub ocmdeletebarcodegen_Click(sender As Object, e As EventArgs) Handles ocmdeletebarcodegen.Click
        If FTReceiveNo.Text <> "" Then

            Dim _cmd As String = ""

            _cmd = "SELECT TOP 1 A.FTBarcodeNo "
            _cmd &= vbCrLf & "   FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_In AS A WITH(NOLOCK)"
            _cmd &= vbCrLf & "   WHERE  A.FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "'  "

            If HI.Conn.SQLConn.GetField(_cmd, Conn.DB.DataBaseName.DB_INVEN, "") = "" Then

                If Not (ogcdetail.DataSource Is Nothing) Then

                    Dim dt As DataTable

                    With CType(ogcdetail.DataSource, DataTable)
                        .AcceptChanges()
                        dt = .Copy
                    End With

                    If dt.Select("FTSelect <> '1'").Length <= 0 Then
                        dt.Dispose()
                        HI.MG.ShowMsg.mInfo("หากต้องการลบ Barcode หมดทุกดวงกรุณาทำการลบทั้งเอกสารที่หน้ารับ !!!", 144435478, Me.Text,, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    If dt.Rows.Count > 0 Then

                        If dt.Select("FTSelect='1'").Length > 0 Then

                            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text) = True Then

                                Dim dtmat As New DataTable
                                dtmat.Columns.Add("FNHSysRawMatId", GetType(Integer))
                                dtmat.Columns.Add("FNQuantity", GetType(Double))

                                Dim dtmatbarsupl As New DataTable
                                dtmatbarsupl.Columns.Add("FNHSysRawMatId", GetType(Integer))
                                dtmatbarsupl.Columns.Add("FTBarcodeSuplRefNo", GetType(String))
                                dtmatbarsupl.Columns.Add("FTFabricFrontSize", GetType(String))
                                dtmatbarsupl.Columns.Add("FNQuantity", GetType(Double))

                                For Each R As DataRow In dt.Select("FTSelect='1'")

                                    If dtmat.Select("FNHSysRawMatId=" & Val(R!FNHSysRawMatId.ToString) & "").Length <= 0 Then
                                        dtmat.Rows.Add(Val(R!FNHSysRawMatId.ToString), 0.0)
                                    End If

                                Next

                                For Each R As DataRow In dtmat.Rows

                                    For Each Rm As DataRow In dt.Select("FNHSysRawMatId=" & Val(R!FNHSysRawMatId.ToString) & " AND FTSelect<>'1'")
                                        If dtmatbarsupl.Select("FNHSysRawMatId=" & Val(Rm!FNHSysRawMatId.ToString) & " AND FTBarcodeSuplRefNo='" & HI.UL.ULF.rpQuoted(Rm!FTBarcodeSuplRefNo.ToString) & "'").Length <= 0 Then
                                            dtmatbarsupl.Rows.Add(Val(R!FNHSysRawMatId.ToString), Rm!FTBarcodeSuplRefNo.ToString, Rm!FTFabricFrontSize.ToString, Val(Rm!FNQuantity.ToString))
                                        End If
                                    Next

                                Next

                                If SaveData(dtmat, dtmatbarsupl) Then

                                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                                    LoadData(FTReceiveNo.Text)

                                End If

                                dtmat.Dispose()
                                dtmatbarsupl.Dispose()
                                dt.Dispose()
                            End If

                        Else
                            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกข้อมูลที่ต้องการลบ !!!", 180104651, Me.Text)
                        End If

                    End If
                    dt.Dispose()
                Else

                End If

            Else
                HI.MG.ShowMsg.mInfo("พบการยืนยันการรับเข้าแล้ว ไม่สารมารถทำการแก้ไขรายละเอียดได้ !!!", 1802010654, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        Else
        End If

    End Sub

    Private Function SaveData(datamat As DataTable, databarcode As DataTable) As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Data updating.....")
        Dim _Str As String
        Try

            Dim Qty As Decimal = 0.0
            For Each R As DataRow In datamat.Rows
                Qty = 0

                For Each Rm As DataRow In databarcode.Select("FNHSysRawMatId=" & Val(R!FNHSysRawMatId.ToString) & "")
                    Qty = Qty + Val(Rm!FNQuantity.ToString)
                Next

                R!FNQuantity = Qty

            Next

            For Each R As DataRow In datamat.Rows

                Call SaveDataMatId(Me.FTReceiveNo.Text, Me.FTPurchaseNo.Text, Val(R!FNHSysRawMatId.ToString), Val(R!FNQuantity))

                For Each Rm As DataRow In databarcode.Select("FNHSysRawMatId=" & Val(R!FNHSysRawMatId.ToString) & "")

                    Call SaveDataMatBarcode(Me.FTReceiveNo.Text, Me.FTPurchaseNo.Text, Val(R!FNHSysRawMatId.ToString), Val(Rm!FNQuantity), Rm!FTFabricFrontSize.ToString, Rm!FTBarcodeSuplRefNo.ToString, Rm!FTBatchNo.ToString, Rm!FTGrade.ToString, Rm!FTRollNo.ToString, _Spls)

                Next

                _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail SET "
                _Str &= vbCrLf & "FNNetAmt=0 "
                _Str &= vbCrLf & ", FNQuantity=0 "
                _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                _Str &= vbCrLf & " AND    FNHSysRawMatId =" & Val(R!FNHSysRawMatId.ToString) & " "
                HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_INVEN)

                _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order SET "
                _Str &= vbCrLf & "FNNetAmt=0 "
                _Str &= vbCrLf & ", FNQuantity=0 "
                _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(Me.FTReceiveNo.Text) & "' "
                _Str &= vbCrLf & " AND    FNHSysRawMatId =" & Val(R!FNHSysRawMatId.ToString) & " "
                HI.Conn.SQLConn.ExecuteNonQuery(_Str, Conn.DB.DataBaseName.DB_INVEN)

            Next

        Catch ex As Exception
        End Try
        _Spls.Close()
        Return True
    End Function

    Private Function SaveDataMatId(FTReceiveNo As String, _PurchaseNo As String, _FNHSysRawMatId As Integer, _BSuplQuantity As Double) As Boolean
        Dim _Str As String
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction



            _Str = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail SET "
            _Str &= vbCrLf & "FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
            _Str &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
            _Str &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
            _Str &= vbCrLf & ",FNNetAmt=FNNetPrice * " & Val(_BSuplQuantity) & " "
            _Str &= vbCrLf & ", FNQuantity=" & Val(_BSuplQuantity) & " "
            _Str &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
            _Str &= vbCrLf & " AND    FNHSysRawMatId =" & Val(_FNHSysRawMatId) & " "

            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If



            If StockValidate.EqualizeJob(FTReceiveNo, _PurchaseNo, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran, _FNHSysRawMatId) = False Then

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            End If

            _Str = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode  "
            _Str &= vbCrLf & " WHERE   FTDocumentNo ='" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
            _Str &= vbCrLf & " AND    FNHSysRawMatId =" & Val(_FNHSysRawMatId) & " "
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

        End Try

        Return True
    End Function
    Private Function SaveDataMatBarcode(FTReceiveNo As String, _PurchaseNo As String, _FNHSysRawMatId As Integer, _BSuplQuantity As Double, _FTFabricFrontSize As String, BarcodeSuplNo As String, BatchNo As String, Grade As String, RollNo As String, _Spls As HI.TL.SplashScreen) As Boolean

        If _FNHSysRawMatId > 0 Then

            Dim _Str As String
            Dim _BatcodeQty As Double
            Dim _FTBarcodeNo As String = ""
            Dim _dtGenBar As DataTable
            Dim _ConQty As Boolean = False
            Dim _GenQtyBar As Double = 0
            Dim _BarSeq As Integer = 0
            Dim _Barcode As String = ""
            Dim QtyBal As Double
            Dim OrderQty As Double

            _FTBarcodeNo = BarcodeSuplNo
            _BatcodeQty = _BSuplQuantity
            _ConQty = False
            _BarSeq = 0
            _GenQtyBar = _BatcodeQty
            QtyBal = _BatcodeQty
            OrderQty = 0

            _Str = " SELECT FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode,FTFabricFrontSize,FTPurchaseNo,FTOrderNo,FNHSysRawMatId,FNHSysUnitId"
            _Str &= vbCrLf & " 	,FNHSysWHId,FNQuantityStock,FNPricePerStock,FTUnitCode,FNBarCodeQty"
            _Str &= vbCrLf & "	,FNQuantityStock - FNBarCodeQty AS FNBarcodeBalance"
            _Str &= vbCrLf & "	,1 AS FNQtyBarcode"
            _Str &= vbCrLf & "	,'" & HI.UL.ULF.rpQuoted(BatchNo) & "' AS FTBatchNo"
            _Str &= vbCrLf & "	,'" & HI.UL.ULF.rpQuoted(Grade) & "'  AS FTGrade,FNConvRatio"
            _Str &= vbCrLf & "	,'" & HI.UL.ULF.rpQuoted(RollNo) & "' AS FTRollNo"
            _Str &= vbCrLf & "  FROM   ("
            _Str &= vbCrLf & "  Select IM.FTRawMatCode"
            _Str &= vbCrLf & "  ,ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode"
            _Str &= vbCrLf & "  ,ISNULL( S.FTRawMatSizeCode ,'') AS FTRawMatSizeCode"
            _Str &= vbCrLf & "  ,A.FTFabricFrontSize"
            _Str &= vbCrLf & "  ,A.FTPurchaseNo"
            _Str &= vbCrLf & "  , A.FTOrderNo"
            _Str &= vbCrLf & "  , A.FNHSysRawMatId"
            _Str &= vbCrLf & " 	, A.FNHSysWHId"
            _Str &= vbCrLf & " 	, A.FNQuantityStock"
            _Str &= vbCrLf & " 	, A.FNHSysUnitIdStock AS FNHSysUnitId"
            _Str &= vbCrLf & "  , A.FNPricePerStock"
            _Str &= vbCrLf & "  , U.FTUnitCode,A.FNConvRatio"
            _Str &= vbCrLf & " 	 ,ISNULL((SELECT SUM(FNQuantity) AS FNQuantity"
            _Str &= vbCrLf & " 		  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH(NOLOCK)"
            _Str &= vbCrLf & " WHERE FTDocumentNo = A.FTDocumentNo "
            _Str &= vbCrLf & " 		AND  FNHSysRawMatId = A.FNHSysRawMatId"
            _Str &= vbCrLf & " 		AND  FTOrderNo = A.FTOrderNo"
            _Str &= vbCrLf & " 		AND  FTPurchaseNo = A.FTPurchaseNo"
            _Str &= vbCrLf & "  	 ),0) AS FNBarCodeQty"
            _Str &= vbCrLf & "  FROM           ( "
            _Str &= vbCrLf & "  SELECT A.FTReceiveNo AS FTDocumentNo ,H.FTPurchaseNo,A.FTOrderNo"
            _Str &= vbCrLf & "     ,A.FTFabricFrontSize"
            _Str &= vbCrLf & "  , A.FNHSysRawMatId"
            _Str &= vbCrLf & " 	 ,H.FNHSysWHId"
            _Str &= vbCrLf & " 	  , A.FNHSysUnitIdStock"
            _Str &= vbCrLf & " 	, A.FNPricePerStock"
            _Str &= vbCrLf & " 	,A.FNQuantityStock,A.FNConvRatio"
            _Str &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order AS A WITH (NOLOCK) INNER JOIN"
            _Str &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive  AS  H WITH(NOLOCK) ON A.FTReceiveNo = H.FTReceiveNo  "
            _Str &= vbCrLf & "  WHERE A.FTReceiveNo ='" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "'"
            _Str &= vbCrLf & "  AND    A.FNHSysRawMatId=" & Integer.Parse(_FNHSysRawMatId) & " "
            _Str &= vbCrLf & "  ) AS A"
            _Str &= vbCrLf & "   INNER Join"
            _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS IM WITH (NOLOCK) ON A.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN"
            _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON A.FNHSysUnitIdStock = U.FNHSysUnitId LEFT OUTER JOIN"
            _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH (NOLOCK) ON IM.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
            _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON IM.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
            _Str &= vbCrLf & "  ) AS M"
            _Str &= vbCrLf & " WHERE (FNQuantityStock-FNBarCodeQty) >0"
            _Str &= vbCrLf & "  ORDER BY  FTRawMatCode,FTRawMatColorCode,FTRawMatSizeCode,FTFabricFrontSize,FTOrderNo"

            _dtGenBar = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_INVEN)

            For Each Rx4 As DataRow In _dtGenBar.Rows

                _BarSeq = _BarSeq + 1
                If _ConQty = False Then
                    QtyBal = Double.Parse(Format(_GenQtyBar * Double.Parse(Val(Rx4!FNConvRatio)), HI.ST.Config.QtyFormat))

                    _ConQty = True
                End If
                OrderQty = Double.Parse(Val(Rx4!FNBarcodeBalance))

                If _BarSeq = 1 Then

                    _Str = " SELECT TOP 1 FTBarcodeNo "
                    _Str &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode "
                    _Str &= vbCrLf & " WHERE FTBarcodeNo='" & HI.UL.ULF.rpQuoted(_FTBarcodeNo) & "' "

                    If HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_INVEN, "") = "" Then
                        _Barcode = _FTBarcodeNo
                    Else
                        _Barcode = HI.Conn.SQLConn.GetField(" EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_GEN_BARCODE_NO '" & HI.ST.SysInfo.CmpRunID & "' ", Conn.DB.DataBaseName.DB_INVEN, "")
                    End If

                Else

                    _Barcode = HI.Conn.SQLConn.GetField(" EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_GEN_BARCODE_NO '" & HI.ST.SysInfo.CmpRunID & "' ", Conn.DB.DataBaseName.DB_INVEN, "")

                End If

                If QtyBal >= OrderQty Then
                    _GenQtyBar = OrderQty
                Else
                    _GenQtyBar = QtyBal
                End If

                If _Barcode <> "" And QtyBal > 0 Then

                    Try

                        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
                        HI.Conn.SQLConn.SqlConnectionOpen()
                        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                        _Str = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode( FTInsUser, FDInsDate, FTInsTime,FTBarcodeNo, FNHSysWHId"
                        _Str &= vbCrLf & ", FNHSysRawMatId, FTOrderNo, FNHSysUnitId, "
                        _Str &= vbCrLf & "   FNPrice, FNQuantity, FTDocumentNo, FTFabricFrontSize, FTBatchNo, FTGrade, FTPurchaseNo,FNHSysCmpId,FTBarcodeSuplRefNo,FTRollNo,FTFabricFrontSizeRcv)"
                        _Str &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                        _Str &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_Barcode) & "' "
                        _Str &= vbCrLf & "," & Val(Rx4!FNHSysWHId.ToString) & " "
                        _Str &= vbCrLf & "," & Val(Rx4!FNHSysRawMatId.ToString) & " "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx4!FTOrderNo.ToString) & "' "
                        _Str &= vbCrLf & "," & Val(Rx4!FNHSysUnitId.ToString) & " "
                        _Str &= vbCrLf & "," & Val(Rx4!FNPricePerStock.ToString) & " "
                        _Str &= vbCrLf & "," & _GenQtyBar & " "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(FTReceiveNo) & "' "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx4!FTFabricFrontSize.ToString) & "' "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx4!FTBatchNo.ToString) & "' "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx4!FTGrade.ToString) & "' "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx4!FTPurchaseNo.ToString) & "'," & Val(HI.ST.SysInfo.CmpID) & " "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_FTBarcodeNo) & "'"
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx4!FTRollNo.ToString) & "' "
                        _Str &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Rx4!FTFabricFrontSize.ToString) & "' "

                        If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                            Return True

                        End If

                        HI.Conn.SQLConn.Tran.Commit()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                        ' Exit Sub
                    Catch ex As Exception

                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return True
                    End Try

                    QtyBal = QtyBal - _GenQtyBar

                End If

            Next



        End If

        Return True

    End Function
    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        If FTReceiveNo.Text <> "" Then
            If Not (ogcdetail.DataSource Is Nothing) Then

                Dim dt As DataTable

                With CType(ogcdetail.DataSource, DataTable)
                    .AcceptChanges()
                    dt = .Copy
                End With

                If dt.Rows.Count > 0 Then

                    If dt.Select("FTSelect='1'").Length > 0 Then

                        Dim cmd As String

                        cmd = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENPrintBarcode_Temp WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        HI.Conn.SQLConn.ExecuteOnly(cmd, Conn.DB.DataBaseName.DB_INVEN)

                        For Each R As DataRow In dt.Select("FTSelect='1'")

                            cmd = " Update   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENPrintBarcode_Temp "
                            cmd &= vbCrLf & " SET FTBarcodeNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeSuplRefNo.ToString) & "'"
                            cmd &= vbCrLf & " WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmd &= vbCrLf & " AND FTBarcodeNo='" & HI.UL.ULF.rpQuoted(R!FTBarcodeSuplRefNo.ToString) & "'"

                            If HI.Conn.SQLConn.ExecuteNonQuery(cmd, Conn.DB.DataBaseName.DB_INVEN) = False Then

                                cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENPrintBarcode_Temp (FTUserLogIn,FTBarcodeNo) "
                                cmd &= vbCrLf & " Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTBarcodeSuplRefNo.ToString) & "'"
                                HI.Conn.SQLConn.ExecuteNonQuery(cmd, Conn.DB.DataBaseName.DB_INVEN)

                            End If

                        Next

                        With New HI.RP.Report
                            .FormTitle = Me.Text
                            .ReportFolderName = "Inventrory\"
                            .ReportName = "ImportSupl_Barcode.rpt"
                            .Formular = " {TINVENPrintBarcode_Temp.FTUserLogIn}='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                            .Preview()
                        End With

                    Else

                        HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกข้อมูลที่ต้องการ !!!", 180104650, Me.Text)

                    End If

                Else

                End If

                dt.Dispose()

            Else

            End If

        Else
        End If
    End Sub

    Private Sub ocmsave_Click_1(sender As Object, e As EventArgs) Handles ocmsave.Click
        If FTReceiveNo.Text <> "" Then

            Dim _cmd As String = ""

            _cmd = "SELECT TOP 1 A.FTBarcodeNo "
            _cmd &= vbCrLf & "   FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_In AS A WITH(NOLOCK)"
            _cmd &= vbCrLf & "   WHERE  A.FTDocumentNo='" & HI.UL.ULF.rpQuoted(FTReceiveNo.Text) & "'  "

            If HI.Conn.SQLConn.GetField(_cmd, Conn.DB.DataBaseName.DB_INVEN, "") = "" Then

                If Not (ogcdetail.DataSource Is Nothing) Then

                    Dim dt As DataTable

                    With CType(ogcdetail.DataSource, DataTable)
                        .AcceptChanges()
                        dt = .Copy
                    End With

                    If dt.Rows.Count > 0 Then

                        If dt.Select("FTSelect='1'").Length > 0 Then

                            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, Me.Text) = True Then

                                Dim dtmat As New DataTable
                                dtmat.Columns.Add("FNHSysRawMatId", GetType(Integer))
                                dtmat.Columns.Add("FNQuantity", GetType(Double))

                                Dim dtmatbarsupl As New DataTable
                                dtmatbarsupl.Columns.Add("FNHSysRawMatId", GetType(Integer))
                                dtmatbarsupl.Columns.Add("FTBarcodeSuplRefNo", GetType(String))
                                dtmatbarsupl.Columns.Add("FTFabricFrontSize", GetType(String))
                                dtmatbarsupl.Columns.Add("FNQuantity", GetType(Double))

                                dtmatbarsupl.Columns.Add("FTBatchNo", GetType(String))
                                dtmatbarsupl.Columns.Add("FTGrade", GetType(String))
                                dtmatbarsupl.Columns.Add("FTRollNo", GetType(String))


                                For Each R As DataRow In dt.Select("FTSelect='1'")

                                    If dtmat.Select("FNHSysRawMatId=" & Val(R!FNHSysRawMatId.ToString) & "").Length <= 0 Then
                                        dtmat.Rows.Add(Val(R!FNHSysRawMatId.ToString), 0.0)
                                    End If

                                Next

                                For Each R As DataRow In dtmat.Rows

                                    For Each Rm As DataRow In dt.Select("FNHSysRawMatId=" & Val(R!FNHSysRawMatId.ToString) & "")
                                        If dtmatbarsupl.Select("FNHSysRawMatId=" & Val(Rm!FNHSysRawMatId.ToString) & " AND FTBarcodeSuplRefNo='" & HI.UL.ULF.rpQuoted(Rm!FTBarcodeSuplRefNo.ToString) & "'").Length <= 0 Then
                                            dtmatbarsupl.Rows.Add(Val(R!FNHSysRawMatId.ToString), Rm!FTBarcodeSuplRefNo.ToString, Rm!FTFabricFrontSize.ToString, Val(Rm!FNQuantity.ToString), Rm!FTBatchNo.ToString, Rm!FTGrade.ToString, Rm!FTRollNo.ToString)
                                        End If
                                    Next

                                Next

                                If SaveData(dtmat, dtmatbarsupl) Then

                                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                                    LoadData(FTReceiveNo.Text)

                                End If

                                dtmat.Dispose()
                                dtmatbarsupl.Dispose()
                                dt.Dispose()
                            End If

                        Else
                            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกข้อมูลที่ต้องการบันทึก !!!", 180104653, Me.Text)
                        End If

                    End If
                    dt.Dispose()
                Else

                End If

            Else
                HI.MG.ShowMsg.mInfo("พบการยืนยันการรับเข้าแล้ว ไม่สารมารถทำการแก้ไขรายละเอียดได้ !!!", 1802010654, Me.Text,, System.Windows.Forms.MessageBoxIcon.Warning)
            End If

        Else
        End If
    End Sub

    Private Sub ogvdetail_MouseDown(sender As Object, e As MouseEventArgs) Handles ogvdetail.MouseDown
        Dim tmpview As DevExpress.XtraGrid.Views.Grid.GridView = sender
        Dim hInfo As GridHitInfo = tmpview.CalcHitInfo(e.X, e.Y)

        If (hInfo.InRowCell) Then

            If Not (m_mergedCellsEdited Is Nothing) Then
                Select Case StateEditMergeCell
                    Case EditMergeCellData.ItemSelect
                        If (ogcdetail.Contains(m_mergedCellEditorSelect)) Then
                            ogcdetail.Controls.Remove(m_mergedCellEditorSelect)
                            For Each cellInfo As GridCellInfo In m_mergedCellsEdited
                                With ogvdetail

                                    .SetRowCellValue(cellInfo.RowHandle, "FTSelect", m_mergedCellEditorSelect.EditValue.ToString)



                                End With
                            Next

                        End If
                    Case EditMergeCellData.ItemFabricSize
                        If (ogcdetail.Contains(m_mergedCellEditorFabricSize)) Then
                            ogcdetail.Controls.Remove(m_mergedCellEditorFabricSize)


                            For Each cellInfo As GridCellInfo In m_mergedCellsEdited
                                With ogvdetail

                                    .SetRowCellValue(cellInfo.RowHandle, "FTFabricFrontSize", m_mergedCellEditorFabricSize.Value)

                                End With
                            Next

                        End If
                    Case EditMergeCellData.ItemQty
                        If (ogcdetail.Contains(m_mergedCellEditorQty)) Then
                            ogcdetail.Controls.Remove(m_mergedCellEditorQty)
                            For Each cellInfo As GridCellInfo In m_mergedCellsEdited
                                With ogvdetail

                                    .SetRowCellValue(cellInfo.RowHandle, "FNQuantity", m_mergedCellEditorQty.Value)




                                End With
                            Next

                        End If
                End Select

                m_mergedCellsEdited = Nothing
            End If

            Dim vInfo As GridViewInfo = tmpview.GetViewInfo()
            Dim cInfo As GridCellInfo = vInfo.GetGridCellInfo(hInfo)
            Select Case cInfo.Column.FieldName.ToString
                Case "FTSelect"

                    If (TypeOf (cInfo) Is DevExpress.XtraGrid.Views.Grid.ViewInfo.GridMergedCellInfo) Then
                        If (m_mergedCellsEdited IsNot Nothing) Then
                            ogcdetail.Controls.Remove(m_mergedCellEditorSelect)
                        End If

                        ogcdetail.Controls.Add(m_mergedCellEditorSelect)
                        m_mergedCellEditorSelect.Bounds = cInfo.Bounds
                        m_mergedCellEditorSelect.Checked = (cInfo.CellValue.ToString() = "1")

                        m_mergedCellsEdited = (CType(cInfo, GridMergedCellInfo)).MergedCells
                        StateEditMergeCell = EditMergeCellData.ItemSelect
                    End If
                Case "FTFabricFrontSize"
                    If (TypeOf (cInfo) Is DevExpress.XtraGrid.Views.Grid.ViewInfo.GridMergedCellInfo) Then
                        If (m_mergedCellsEdited IsNot Nothing) Then
                            ogcdetail.Controls.Remove(m_mergedCellEditorFabricSize)
                        End If

                        ogcdetail.Controls.Add(m_mergedCellEditorFabricSize)
                        m_mergedCellEditorFabricSize.Bounds = cInfo.Bounds
                        m_mergedCellEditorFabricSize.Value = cInfo.CellValue.ToString()

                        m_mergedCellsEdited = (CType(cInfo, GridMergedCellInfo)).MergedCells
                        StateEditMergeCell = EditMergeCellData.ItemFabricSize
                        '' ''End If
                    End If
                Case "FNQuantity"
                    If (TypeOf (cInfo) Is DevExpress.XtraGrid.Views.Grid.ViewInfo.GridMergedCellInfo) Then
                        If (m_mergedCellsEdited IsNot Nothing) Then
                            ogcdetail.Controls.Remove(m_mergedCellEditorQty)
                        End If
                        ogcdetail.Controls.Add(m_mergedCellEditorQty)
                        m_mergedCellEditorQty.Bounds = cInfo.Bounds
                        m_mergedCellEditorQty.Value = cInfo.CellValue.ToString()
                        m_mergedCellsEdited = (CType(cInfo, GridMergedCellInfo)).MergedCells
                        StateEditMergeCell = EditMergeCellData.ItemQty
                    End If
            End Select
        End If
    End Sub

    Private Sub FTStaReceiveAll_CheckedChanged(sender As Object, e As EventArgs) Handles FTStateSelectAll.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.FTStateSelectAll.Checked Then
                _State = "1"
            End If

            With ogvdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then

                    With ogvdetail
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

End Class