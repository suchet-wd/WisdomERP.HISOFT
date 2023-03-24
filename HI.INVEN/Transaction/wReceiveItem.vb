Imports DevExpress.XtraEditors.Controls

Public Class wReceiveItem

    Public Enum RcvType As Integer
        RcvNormal = 0
        RcvRepire = 1
        RcvFree = 2
    End Enum

    Private _PurchaseNo As String = ""
    Public Property PurchaseNo() As String
        Get
            Return _PurchaseNo
        End Get
        Set(value As String)
            _PurchaseNo = value
        End Set
    End Property

    Private _ReceiveNo As String = ""
    Public Property ReceiveNo() As String
        Get
            Return _ReceiveNo
        End Get
        Set(value As String)
            _ReceiveNo = value
        End Set
    End Property

    Private _ReceiveType As RcvType = RcvType.RcvNormal
    Public Property ReceiveType() As RcvType
        Get
            Return _ReceiveType
        End Get
        Set(value As RcvType)
            _ReceiveType = value
        End Set
    End Property

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        With ResFNPOBalQty
            .Precision = HI.ST.Config.QtyDigit
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave
        End With

        With ResFNQuantity
            .Precision = HI.ST.Config.QtyDigit
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave
        End With

        With ResFNRcvHisQty
            .Precision = HI.ST.Config.QtyDigit
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave
        End With

        With ResFNRcvQty
            .Precision = HI.ST.Config.QtyDigit
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave
            AddHandler .EditValueChanged, AddressOf RcvEdit_EditChanged
        End With

        With ResFTStateSelect
            AddHandler .CheckedChanged, AddressOf CheckEdit_CheckedChanged
        End With

    End Sub

    Private Sub CheckEdit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

            If CType(sender, DevExpress.XtraEditors.CheckEdit).Checked Then
                Dim _balQty As Double = .GetFocusedRowCellValue("FNPOBalQty")
                .SetFocusedRowCellValue("FNRcvQty", _balQty)
            Else
                .SetFocusedRowCellValue("FNRcvQty", 0)
            End If

        End With
    End Sub

    Private Sub RcvEdit_EditChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
            Select Case Me.ReceiveType
                Case RcvType.RcvNormal
                    Dim _RcvOverQty As Double = 0
                    Dim _RcvQty As Double = CType(sender, DevExpress.XtraEditors.CalcEdit).Value
                    Dim _SysMatID As Integer = .GetFocusedRowCellValue("FNHSysRawMatId")

                    _RcvOverQty = Me.CheckReceiveOver(Me.PurchaseNo, Me.ReceiveNo, _SysMatID, _RcvQty)

                    If _RcvOverQty > 0 Then

                        If .GetFocusedRowCellValue("FTStateSendAppRcv").ToString = "1" Then

                            .SetFocusedRowCellValue("FNRcvQty", _RcvQty)
                            .SetFocusedRowCellValue("FNRcvQtyPass", (_RcvQty - _RcvOverQty))
                            .SetFocusedRowCellValue("FNRcvQtyOver", _RcvOverQty)

                        Else

                            If HI.MG.ShowMsg.mConfirmProcess("คุณไม่สามารถรับเกิน PO ได้ คุณต้องการทำการส่ง Approve หรือไม่ !!!", 1410060003, "You Can Reveive Over " & Format((_RcvQty - _RcvOverQty), HI.ST.Config.QtyFormat) & " Only., Over " & Format(_RcvOverQty, HI.ST.Config.QtyFormat)) = True Then

                                .SetFocusedRowCellValue("FNRcvQty", _RcvQty)
                                .SetFocusedRowCellValue("FNRcvQtyPass", (_RcvQty - _RcvOverQty))
                                .SetFocusedRowCellValue("FNRcvQtyOver", _RcvOverQty)
                                .SetFocusedRowCellValue("FTStateSendAppRcv", "1")

                            Else

                                If _RcvQty >= _RcvOverQty Then
                                    .SetFocusedRowCellValue("FNRcvQty", (_RcvQty - _RcvOverQty))
                                Else
                                    .SetFocusedRowCellValue("FNRcvQty", 0)
                                End If

                            End If

                        End If
                    Else
                        .SetFocusedRowCellValue("FNRcvQtyPass", _RcvQty)
                        .SetFocusedRowCellValue("FNRcvQtyOver", 0)
                        .SetFocusedRowCellValue("FTStateSendAppRcv", "0")
                    End If

                    Try
                        CType(sender.Parent.datasource, DataTable).AcceptChanges()
                    Catch ex As Exception
                    End Try

                Case Else
            End Select
        End With
    End Sub

    Private _ProcessProc As Boolean = False
    Public Property ProcessProc As Boolean
        Get
            Return _ProcessProc
        End Get
        Set(value As Boolean)
            _ProcessProc = value
        End Set
    End Property

    Private Sub ocmreceive_Click(sender As System.Object, e As System.EventArgs) Handles ocmreceive.Click
        Me.ProcessProc = True
        Me.Close()
    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.ProcessProc = False
        Me.Close()
    End Sub

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

    Private Sub ResFTStateSelect_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ResFTStateSelect.EditValueChanging

        Try
            Select Case e.NewValue.ToString
                Case "1"

                    Dim _SysUnitStock As Integer
                    Dim _TsysMatId As Integer
                    Dim _SysUnitPo As Integer
                    Dim _Str As String = ""

                    _TsysMatId = Integer.Parse(Val(Me.ogvrcv.GetFocusedRowCellValue("FNHSysRawMatId").ToString))
                    _SysUnitPo = Integer.Parse(Val(Me.ogvrcv.GetFocusedRowCellValue("FNHSysUnitId").ToString))

                    _SysUnitStock = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECt TOP 1 FNHSysUnitId FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial WITH(NOLOCK) WHERE   FNHSysRawMatId =" & _TsysMatId & " ", Conn.DB.DataBaseName.DB_SYSTEM, "0")))

                    If Integer.Parse(Val(_SysUnitStock)) <> Integer.Parse(Val(_SysUnitPo)) Then

                        _Str = " SELECT      TOP 1  FNHSysUnitId  "
                        _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert  WITH (NOLOCK) "
                        _Str &= vbCrLf & "  WHERE FNHSysUnitId =" & Integer.Parse(_SysUnitPo) & " "
                        _Str &= vbCrLf & "  AND FNHSysUnitIdTo =" & Integer.Parse(_SysUnitStock) & " "

                        If Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "0"))) > 0 Then
                            e.Cancel = False
                        Else
                            e.Cancel = True
                            HI.MG.ShowMsg.mInfo("ไม่พบการตั้งค่าการแปลงหน่วยวัถุดิบ ไม่สามารถทำการรับวัตถุดิบนี้ได้ !!!", 16042701784, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        End If
                    Else
                        e.Cancel = False
                    End If

            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Sub FTStaReceiveAll_CheckedChanged(sender As Object, e As EventArgs) Handles FTStaReceiveAll.CheckedChanged
        Try
            Dim State As String = "0"

            If FTStaReceiveAll.Checked Then
                State = "1"
            End If


            With ogcrcv
                If Not (.DataSource Is Nothing) And ogvrcv.RowCount > 0 Then

                    With ogvrcv
                        For I As Integer = 0 To .RowCount - 1

                            If State = "1" Then

                                Dim _SysUnitStock As Integer
                                Dim _TsysMatId As Integer
                                Dim _SysUnitPo As Integer
                                Dim _Str As String = ""
                                Dim StateTrue As Boolean = False
                                _TsysMatId = Integer.Parse(Val(.GetRowCellValue(I, "FNHSysRawMatId").ToString))
                                _SysUnitPo = Integer.Parse(Val(.GetRowCellValue(I, "FNHSysUnitId").ToString))

                                _SysUnitStock = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECt TOP 1 FNHSysUnitId FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial WITH(NOLOCK) WHERE   FNHSysRawMatId =" & _TsysMatId & " ", Conn.DB.DataBaseName.DB_SYSTEM, "0")))

                                If Integer.Parse(Val(_SysUnitStock)) <> Integer.Parse(Val(_SysUnitPo)) Then

                                    _Str = " SELECT      TOP 1  FNHSysUnitId  "
                                    _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitConvert  WITH (NOLOCK) "
                                    _Str &= vbCrLf & "  WHERE FNHSysUnitId =" & Integer.Parse(_SysUnitPo) & " "
                                    _Str &= vbCrLf & "  AND FNHSysUnitIdTo =" & Integer.Parse(_SysUnitStock) & " "

                                    If Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "0"))) > 0 Then

                                        StateTrue = True
                                    Else
                                        StateTrue = False
                                    End If
                                Else
                                    StateTrue = True
                                End If

                                If StateTrue Then

                                    If Val(.GetRowCellValue(I, "FNPOBalQty")) > 0 Then

                                        Dim _RcvQty As Double = Val(.GetRowCellValue(I, "FNPOBalQty"))

                                        .SetRowCellValue(I, .Columns.ColumnByFieldName("FTStateSelect"), State)

                                        .SetRowCellValue(I, "FNRcvQty", _RcvQty)

                                    Else
                                        .SetRowCellValue(I, .Columns.ColumnByFieldName("FTStateSelect"), "0")

                                    End If

                                End If

                            Else
                                .SetRowCellValue(I, .Columns.ColumnByFieldName("FTStateSelect"), State)
                            End If

                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With


        Catch ex As Exception

        End Try
    End Sub

    Private Sub ResFNRcvQty_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ResFNRcvQty.EditValueChanging

    End Sub

End Class