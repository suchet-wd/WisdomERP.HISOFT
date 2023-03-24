Public Class wReceiveItemAsset

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
                    Dim _SysMatID As Integer = .GetFocusedRowCellValue("FNHSysFixedAssetId")

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
        If VerifyData() Then
            Me.ProcessProc = True
            Me.Close()
        Else
            MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text)
        End If

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
        _Str &= vbCrLf & "  (SELECT      FTPurchaseNo, FNHSysFixedAssetId, SUM(FNQuantity) AS POFNQuantity"
        _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTPurchase_Detail AS P WITH(NOLOCK)"
        _Str &= vbCrLf & " WHERE FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PONO) & "'  "
        _Str &= vbCrLf & " AND  FNHSysFixedAssetId=" & MatID & " "
        _Str &= vbCrLf & "  GROUP BY FTPurchaseNo, FNHSysFixedAssetId) AS A LEFT OUTER JOIN "
        _Str &= vbCrLf & "  (SELECT        RH.FTPurchaseNo, RD.FNHSysFixedAssetId, SUM(RD.FNQuantity) AS RcvFNQuantity"
        _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive AS RH WITH (NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReceive_Detail AS RD WITH (NOLOCK) ON RH.FTReceiveNo = RD.FTReceiveNo"
        _Str &= vbCrLf & "  WHERE        (RH.FTReceiveNo <> '" & HI.UL.ULF.rpQuoted(RCVNO) & "')"
        _Str &= vbCrLf & " AND  RH.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PONO) & "'  "
        _Str &= vbCrLf & " AND  RD.FNHSysFixedAssetId=" & MatID & " "
        _Str &= vbCrLf & "  GROUP BY RH.FTPurchaseNo, RD.FNHSysFixedAssetId) AS B"
        _Str &= vbCrLf & "  ON A.FTPurchaseNo = B.FTPurchaseNo"
        _Str &= vbCrLf & "   AND A.FNHSysFixedAssetId = B.FNHSysFixedAssetId"

        _Str &= vbCrLf & "  LEFT OUTER JOIN ( SELECT FTPurchaseNo,FNHSysFixedAssetId,Convert(numeric(18," & HI.ST.Config.QtyDigit & "),SUM(FNQuantity)) AS  RTSFNQuantity"
        _Str &= vbCrLf & "  FROM"
        _Str &= vbCrLf & "  (SELECT        H.FTPurchaseNo, B.FNHSysFixedAssetId, SUM(BO.FNQuantity) AS FNQuantity"
        _Str &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode AS B WITH(NOLOCK) INNER JOIN"
        _Str &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTBarcode_OUT AS BO WITH(NOLOCK)  ON B.FTBarcodeNo = BO.FTBarcodeNo INNER JOIN"
        ' _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TINVENReceive_Detail_Order AS RC WITH(NOLOCK)  ON B.FTDocumentNo = RC.FTReceiveNo AND B.FTOrderNo = RC.FTOrderNo AND B.FNHSysRawMatId = RC.FNHSysRawMatId INNER JOIN"
        _Str &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_FIXED) & "].dbo.TFIXEDTReturnToSupplier AS H WITH (NOLOCK) ON BO.FTDocumentNo = H.FTReturnSuplNo"
        _Str &= vbCrLf & " WHERE  H.FTPurchaseNo='" & HI.UL.ULF.rpQuoted(PONO) & "'  "
        _Str &= vbCrLf & "  GROUP BY  H.FTPurchaseNo, B.FNHSysFixedAssetId) AS RTS"
        _Str &= vbCrLf & "  GROUP BY FTPurchaseNo,FNHSysFixedAssetId ) AS C"
        _Str &= vbCrLf & "  ON A.FTPurchaseNo = C.FTPurchaseNo"
        _Str &= vbCrLf & "   AND A.FNHSysFixedAssetId = C.FNHSysFixedAssetId"
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_FIXED)

        If _dt.Rows.Count > 0 Then

            For Each R As DataRow In _dt.Rows
                _POQty = R!POFNQuantity
                _RtsQty = R!RTSFNQuantity
                _RcvQty = (_RcvQty + R!RcvFNQuantity) - R!RTSFNQuantity
                Exit For
            Next

            If _RcvQty > _POQty Then

                _Str = "   SELECT    TOP 1  CFG.FNRcvOverPercent"
                _Str &= vbCrLf & "   FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset AS IM INNER JOIN"
                '_Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM ON IM.FTAssetCode = MM.FTMainMatCode INNER JOIN"
                _Str &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetConfigReceiveOver AS CFG ON IM.FNHSysAssetGrpId = CFG.FNHSysAssetGrpId"
                _Str &= vbCrLf & " WHERE IM.FNHSysFixedAssetId= " & MatID & "  "
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
                    Dim _TsysAssetId As Integer
                    Dim _SysUnitPo As Integer
                    Dim _Str As String = ""
                    Dim _SysAssetCode As String = Me.ogvrcv.GetFocusedRowCellValue("FTAssetCode").ToString
                    _TsysAssetId = Integer.Parse(Val(Me.ogvrcv.GetFocusedRowCellValue("FNHSysFixedAssetId").ToString))
                    _SysUnitPo = Integer.Parse(Val(Me.ogvrcv.GetFocusedRowCellValue("FNHSysUnitId").ToString))
                    Dim _TsysAsset As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysFixedAssetId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset WHERE FNHSysFixedAssetId = " & _TsysAssetId & " AND FTAssetCode = '" & _SysAssetCode & "'", Conn.DB.DataBaseName.DB_MASTER, "")
                    If _TsysAsset <> "" Then
                        _SysUnitStock = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECt TOP 1 FNHSysUnitAssetId FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAsset WITH(NOLOCK) WHERE   FNHSysFixedAssetId =" & _TsysAssetId & " ", Conn.DB.DataBaseName.DB_SYSTEM, "0")))
                    Else
                        _SysUnitStock = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECt TOP 1 FNHSysUnitAssetId FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TASMAssetPart WITH(NOLOCK) WHERE   FNHSysAssetPartId =" & _TsysAssetId & " ", Conn.DB.DataBaseName.DB_SYSTEM, "0")))
                    End If

                    If Integer.Parse(Val(_SysUnitStock)) <> Integer.Parse(Val(_SysUnitPo)) Then

                        _Str = " SELECT      TOP 1  FNHSysUnitAssetId  "
                        _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitAssetConvert  WITH (NOLOCK) "
                        _Str &= vbCrLf & "  WHERE FNHSysUnitAssetId =" & Integer.Parse(_SysUnitPo) & " "
                        _Str &= vbCrLf & "  AND FNHSysUnitAssetIdTo =" & Integer.Parse(_SysUnitStock) & " "

                        If Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Str, Conn.DB.DataBaseName.DB_MASTER, "0"))) > 0 Then
                            e.Cancel = False
                        Else
                            e.Cancel = True
                            HI.MG.ShowMsg.mInfo("ไม่พบการตั้งค่าการแปลงหน่วยสินทรัพย์ ไม่สามารถทำการรับสินทรัพย์นี้ได้ !!!", 1710311342, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                        End If
                    Else
                        e.Cancel = False
                    End If

            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False
        Try
            With CType(ogcrcv.DataSource, DataTable)
                For Each R As DataRow In .Select("FTStateSelect='1'")
                    If R!FTStateSelect.ToString = "1" Then
                        _Pass = True
                        Exit For
                    Else
                        _Pass = False
                    End If
                Next
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
            Return _Pass
        End Try
        Return _Pass
    End Function

    Private Sub wReceiveItemAsset_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class