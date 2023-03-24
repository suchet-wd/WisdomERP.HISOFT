Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Public Class wScanBarcodeMerge

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
        Catch ex As Exception
        End Try
    End Sub

    'Private Sub LoadData()
    '    Try
    '        Dim _Cmd As String = ""
    '        Dim _oDt As DataTable
    '        _Cmd = "SELECT  '0' AS FTSelect   ,O.FTPORef ,  B.FTOrderNo, B.FTColorway, B.FTSizeBreakDown,  sum(B.FNScanQuantity) AS FNScanQuantity, isnull(C.FTBarCodeCarton,B.FTBarcodeNo) as FTBarCodeCarton , S.FTStyleCode "
    '        _Cmd &= vbCrLf & ", Case When Isdate(max(isnull(P.FDUpdDate , P.FDInsDate))) = 1 Then convert(varchar(10),convert(date,max(isnull(P.FDUpdDate , P.FDInsDate))),103) Else  '' End AS FDDateTrans   "
    '        _Cmd &= vbCrLf & " , P.FNCartonNo  , P.FTPackNo , B.FTSubOrderNo "
    '        _Cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKCarton AS P WITH(NOLOCK) INNER JOIN    "
    '        _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan AS B WITH (NOLOCK)  ON P.FTPackNo = B.FTPackNo and P.FNCartonNo = B.FNCartonNo"
    '        _Cmd &= vbCrLf & "  LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode AS C WITH (NOLOCK) ON B.FTPackNo = C.FTPackNo AND B.FNCartonNo = C.FNCartonNo"
    '        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON B.FTOrderNo = O.FTOrderNo"
    '        _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS S WITH(NOLOCK) ON O.FNHSysStyleId = S.FNHSysStyleId "
    '        _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBarcodeScanFG  AS FG  WITH(NOLOCK)ON  isnull(C.FTBarCodeCarton,B.FTBarcodeNo) = FG.FTBarCodeCarton and P.FNCartonNo =  FG.FNCartonNo  "
    '        _Cmd &= vbCrLf & " and P.FTPackNo = FG.FTPackNo "
    '        '_Cmd &= vbCrLf & " WHERE isnull(C.FTBarCodeCarton,B.FTBarcodeNo) +'|'+B.FTOrderNo+'|'+B.FTColorway+'|'+B.FTSizeBreakDown +'|'+P.FTPackNo+'|'+convert(nvarchar(18) , P.FNCartonNo) not in ("
    '        '_Cmd &= vbCrLf & " SELECT     FTBarCodeCarton +'|'+FTOrderNo +'|'+FTColorWay +'|'+FTSizeBreakDown+'|'+FTPackNo+'|'+convert(nvarchar(18) , FNCartonNo)  "
    '        '_Cmd &= vbCrLf & " FROM TPRODTBarcodeScanFG WITH(NOLOCK) )"
    '        _Cmd &= vbCrLf & "  where FG.FTBarCodeCarton is null  "
    '        _Cmd &= vbCrLf & "Group by  B.FTOrderNo, B.FTColorway, B.FTSizeBreakDown,isnull(C.FTBarCodeCarton,B.FTBarcodeNo ), S.FTStyleCode ,O.FTPORef   "
    '        _Cmd &= vbCrLf & " , P.FNCartonNo  , P.FTPackNo , B.FTSubOrderNo"
    '        _Cmd &= vbCrLf & "Order by max(isnull(P.FDUpdDate , P.FDInsDate)) ASC "
    '        _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
    '        ogcdetail.DataSource = _oDt
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub LoadDataWareHouse(barcode As String)
        Try
            Dim _Cmd As String = ""
            Dim _dt As DataTable
            Dim _oDt As DataTable
            _Cmd = "SELECT Top 1 '0' AS FTSelect ,  F.FTBarCodeCarton, F.FNHSysWHFGId, F.FTColorWay, F.FTSizeBreakDown, F.FTOrderNo, F.FNQuantity , S.FTStyleCode , O.FTPORef , F.FTSubOrderNo "
            _Cmd &= vbCrLf & "   FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBarcodeScanFG AS F WITH(NOLOCK) "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON F.FTOrderNo = O.FTOrderNo"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS S WITH(NOLOCK) ON O.FNHSysStyleId = S.FNHSysStyleId"
            '_Cmd &= vbCrLf & " Where F.FDInsDate = " & HI.UL.ULDate.FormatDateDB
            '_Cmd &= vbCrLf & " AND F.FNHSysWHFGId=" & CInt(Me.FNHSysWHFGId.Properties.Tag)
            _Cmd &= vbCrLf & " Where F.FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(barcode) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            If Not (Me.ogcwarehouse.DataSource Is Nothing) Then
                With DirectCast(Me.ogcwarehouse.DataSource, DataTable)
                    .AcceptChanges()
                    _dt = .Copy
                End With

                Dim dr As DataRow = _dt.NewRow
                For Each R As DataRow In _oDt.Rows
                    dr("FTSelect") = R!FTSelect.ToString
                    dr("FTBarCodeCarton") = R!FTBarCodeCarton.ToString
                    dr("FNHSysWHFGId") = R!FNHSysWHFGId
                    dr("FTColorWay") = R!FTColorWay.ToString
                    dr("FTSizeBreakDown") = R!FTSizeBreakDown.ToString
                    dr("FTOrderNo") = R!FTOrderNo.ToString
                    dr("FTStyleCode") = R!FTStyleCode.ToString
                    dr("FNQuantity") = R!FNQuantity
                    dr("FTPORef") = R!FTPORef.ToString
                    dr("FTSubOrderNo") = R!FTSubOrderNo.ToString
                Next
                _dt.Rows.Add(dr)
                Me.ogcwarehouse.DataSource = _dt
            Else
                Me.ogcwarehouse.DataSource = _oDt
            End If


        Catch ex As Exception
        End Try
    End Sub
    Private Function CalsPalletBal(barcode As String, Optional ByVal stateDel As Boolean = False) As Boolean
        Try
            Dim _Cmd As String = "" : Dim _oDt As DataTable
            _Cmd = "SELECT top 1 FTBarCodeCarton  ,FNHSysCartonId "
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBarcodeScanFG WITH (NOLOCK)"
            _Cmd &= vbCrLf & " where FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(barcode) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

            Dim _dt As DataTable
            With DirectCast(Me.ogcBalCarton.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy()
            End With
            Dim _totalCaton As Integer = 0
            For Each R As DataRow In _dt.Select("FNHSysCartonId=" & _oDt.Rows(0).Item("FNHSysCartonId"))

                If Not (stateDel) Then
                    _totalCaton = R!FNCartonTotal + 1
                    R!FNCartonTotal += +1
                    R!FNCBM = (_totalCaton * R!Carton)
                    R!FNCartonBal = R!FNCartonBal - 1


                Else
                    _totalCaton = R!FNCartonTotal - 1
                    R!FNCartonTotal = R!FNCartonTotal - 1
                    R!FNCBM = (_totalCaton * R!Carton)
                    R!FNCartonBal = R!FNCartonBal + 1
                    ' R!FNCartonBal = 2.1888 -
                End If
            Next
            Dim x As Double = _dt.Compute("sum(FNCBM)", "FNCBM > 0")

            For Each R As DataRow In _dt.Rows
                R!FNCartonBal = Math.Floor((2.1888 - x) / R!Carton)
            Next
            Me.ogcBalCarton.DataSource = _dt
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub LoadPalletInfo()
        Try
            Dim _Cmd As String = ""
            _Cmd = "SELECT FNHSysCartonId, FTCartonCode, ((FNWidth /1000) * (FNLength /1000) )* (FNHeight /1000) AS Carton "
            _Cmd &= vbCrLf & " , (1.20*1.20) * 1.52 as Pallet , Floor (( (1.20*1.20) * 1.52) / (((FNWidth /1000) * (FNLength /1000) )* (FNHeight /1000))) AS FNCartonBal "
            _Cmd &= vbCrLf & " , 0 as FNCartonTotal , 0.00 as FNCBM"
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCarton WITH (NOLOCK)"
            _Cmd &= vbCrLf & " where  (FTStateActive = '1')  Order by FTCartonCode asc "
            Me.ogcBalCarton.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MASTER)
        Catch ex As Exception

        End Try
    End Sub

    Private Function GenNewBarcodePallet() As String
        Try
            Dim _Cmd As String = "" : Dim _DocNew As String = "" : Dim _CartonBarCode As String = ""
            Dim _CmpH As String

            _CmpH = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WITH(NOLOCK)  WHERE FNHSysCmpId=" & Val("" & HI.ST.SysInfo.CmpID) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")
            _CartonBarCode = HI.TL.Document.GetDocumentNo("HITECH_PRODUCTION", "TPRODTBarcodeScanFG", "", False, _CmpH).ToString

            _Cmd = "SELECT top 1    "
            _Cmd &= vbCrLf & "    LEFT('" & _CartonBarCode & "',Len('" & _CartonBarCode & "')-5) + RIGHT('000000' +  Convert(varchar(30),Convert(Int,RIGHT('" & _CartonBarCode & "',5)) +  ((  ROW_NUMBER() Over (Order By isnull(FTBarCodePallet,'') desc) )-1)),5) AS FTBarCodePallet "
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBarcodeScanFG WITH (NOLOCK)"
            _Cmd &= vbCrLf & " order by isnull(FTBarCodePallet,'') desc "
            _DocNew = HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "")

            Return _DocNew

        Catch ex As Exception
            Return ""
        End Try
    End Function


    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            'Me.LoadData()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHSysWHFGId_EditValueChanged(sender As Object, e As EventArgs) 'Handles FNHSysWHFGId.EditValueChanged, FNHSysWHLocId.EditValueChanged
        Try
            If (Me.FNHSysWHFGId.Properties.Tag.ToString = "0" Or Me.FNHSysWHFGId.Properties.Tag.ToString = "") And Me.FNHSysWHFGId.Text <> "" Then
                Me.FNHSysWHFGId.Properties.Tag = HI.Conn.SQLConn.GetField(" Select Top 1 FNHSysWHFGId From [HITECH_MASTER]..TCNMWarehouseFG Where FTWHFGCode = '" & Me.FNHSysWHFGId.Text & "'", Conn.DB.DataBaseName.DB_MASTER, "0")
            End If
            ' Call LoadDataWareHouse()
        Catch ex As Exception
        End Try
    End Sub

    Private Function ChkCartonBal(barcode As String) As Boolean
        Try
            Dim _Cmd As String = "" : Dim _oDt As DataTable
            _Cmd = "SELECT top 1 FTBarCodeCarton  ,FNHSysCartonId "
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBarcodeScanFG WITH (NOLOCK)"
            _Cmd &= vbCrLf & " where FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(barcode) & "'"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Dim _dt As DataTable
            With DirectCast(Me.ogcBalCarton.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy()
            End With
            Dim x As Double = _dt.Compute("sum(FNCartonBal)", "FNHSysCartonId =" & _oDt.Rows(0).Item("FNHSysCartonId"))

            Return x > 0
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub FTProductBarcodeNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FTProductBarcodeNo.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Me.FTProductBarcodeNo.Text = "" Then
                    Me.FTProductBarcodeNo.Focus()
                    Exit Sub
                End If
                If Me.FTProductBarcodeNo.Text = Me.FTBarCodePallet.Text Then
                    Me.FTBarCodePallet.Text = GenNewBarcodePallet()
                    LoadPalletInfo()
                    Me.ogcwarehouse.DataSource = Nothing
                    Me.lblQtyScan.Text = "000"
                    Exit Sub
                End If

                If Me.FNHSysWHFGId.Text <> "" Then
                    If Not Me.FTStateDeleteBarcode.Checked Then
                        'Save
                        If Not (ChkCartonBal(Me.FTProductBarcodeNo.Text.ToUpper.Trim)) Then
                            HI.MG.ShowMsg.mInfo("ปริมาณเกินขนาดพาเลท กรุณาตรวจสอบ!!!", 1904011131, Me.Text)
                            Me.FTProductBarcodeNo.Focus()
                            Me.FTProductBarcodeNo.SelectAll()
                            Exit Sub
                        End If
                        If SaveData(Me.FTProductBarcodeNo.Text.ToUpper.Trim, False) Then
                            Me.LoadDataWareHouse(Me.FTProductBarcodeNo.Text.ToUpper.Trim)
                            Me.CalsPalletBal(Me.FTProductBarcodeNo.Text.ToUpper.Trim)
                            'Me.LoadData()

                            Me.FTProductBarcodeNo.Focus()
                            Me.FTProductBarcodeNo.SelectAll()
                        Else
                            HI.MG.ShowMsg.mInfo("บาร์โค้ดยังไม่มีการแสกนปิดกล่อง หรือ แสกนเข้าคลังสำเร็จรูปหมดแล้ว กรุณาตรวจสอบ!!!", 1510101118, Me.Text)
                            Me.FTProductBarcodeNo.Focus()
                            Me.FTProductBarcodeNo.SelectAll()
                        End If
                    Else
                        'Delete
                        If DelData(Me.FTProductBarcodeNo.Text.ToUpper.Trim) Then
                            'Me.LoadDataWareHouse(Me.FTProductBarcodeNo.Text.ToUpper.Trim)
                            With ogvwarehouse
                                .DeleteRow(.LocateByValue("FTBarCodeCarton", Me.FTProductBarcodeNo.Text.ToUpper.Trim))
                                With CType(Me.ogcwarehouse.DataSource, DataTable)
                                    .AcceptChanges()
                                End With
                            End With
                            Me.CalsPalletBal(Me.FTProductBarcodeNo.Text.ToUpper.Trim, True)

                            'Me.LoadData()
                            Me.FTProductBarcodeNo.Focus()
                            Me.FTProductBarcodeNo.SelectAll()
                        Else
                            Me.FTProductBarcodeNo.Focus()
                            Me.FTProductBarcodeNo.SelectAll()
                        End If
                    End If
                    Me.lblQtyScan.Text = HI.UL.ULF.rpQuoted(Me.ogvwarehouse.RowCount)
                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, FNHSysWHFGId_lbl.Text)
                    Me.FNHSysWHFGId.Focus()
                    Exit Sub
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Function DelData(_FTProductBarcodeNo As String) As Boolean
        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
        Try
            Dim _Cmd As String = ""
            _Cmd = "UPDATE W"
            _Cmd &= vbCrLf & "Set W.FTBarCodePallet=''"
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS W  "
            '_Cmd &= vbCrLf & " WHERE  W.FNHSysWHFGId=" & CInt(Me.FNHSysWHFGId.Properties.Tag)
            _Cmd &= vbCrLf & " WHERE W.FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(_FTProductBarcodeNo) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function SaveData(_FTProductBarcodeNo As String, state As Boolean, Optional ByVal _FTOrderNo As String = "", Optional ByVal _FTColorway As String = "", Optional ByVal _FTSizeBreakDown As String = "") As Boolean
        Try
            Dim _Cmd As String = ""

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
            _Cmd = "UPDATE W"
            _Cmd &= vbCrLf & "Set W.FTBarCodePallet='" & HI.UL.ULF.rpQuoted(Me.FTBarCodePallet.Text) & "'"

            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS W  "
            '_Cmd &= vbCrLf & " WHERE  W.FNHSysWHFGId=" & CInt(Me.FNHSysWHFGId.Properties.Tag)
            _Cmd &= vbCrLf & " WHERE W.FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(_FTProductBarcodeNo) & "'"

            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
        End Try
    End Function


    Private Function SaveDataGrid(_FTProductBarcodeNo As String, state As Boolean, _Qty As Double, _PackNo As String, _CartonNo As Integer, Optional ByVal _FTOrderNo As String = "", Optional ByVal _FTColorway As String = "", Optional ByVal _FTSizeBreakDown As String = "", Optional ByVal _SubOrderNo As String = "") As Boolean
        Try
            Dim _Cmd As String = ""

            '_Cmd = "UPDATE W"
            '_Cmd &= vbCrLf & "Set W.FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '_Cmd &= vbCrLf & ", W.FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            '_Cmd &= vbCrLf & ",W.FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            '_Cmd &= vbCrLf & ",W.FNQuantity=" & _Qty

            '_Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS W "
            '_Cmd &= vbCrLf & " WHERE W.FTBarCodeCarton='" & _FTProductBarcodeNo & "'"
            '_Cmd &= vbCrLf & " And W.FTOrderNo='" & _FTOrderNo & "'"
            '_Cmd &= vbCrLf & " AND W.FTColorway='" & _FTColorway & "'"
            '_Cmd &= vbCrLf & " And W.FTSizeBreakDown='" & _FTSizeBreakDown & "'"
            '_Cmd &= vbCrLf & " AND W.FTPackNo='" & _PackNo & "'"
            '_Cmd &= vbCrLf & " And W.FNHSysWHFGId=" & CInt(Me.FNHSysWHFGId.Properties.Tag)
            '_Cmd &= vbCrLf & " And W.FNCartonNo=" & _CartonNo


            'If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG (FTInsUser, FDInsDate, FTInsTime, FTBarCodeCarton, FNHSysWHFGId, FTColorWay, FTSizeBreakDown, FTOrderNo, FNQuantity , FTPackNo , FNCartonNo ,FTSubOrderNo ,FNHSysWHLocId)"
            _Cmd &= vbCrLf & "Select '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",'" & _FTProductBarcodeNo & "'"
            _Cmd &= vbCrLf & "," & CInt(Me.FNHSysWHFGId.Properties.Tag)
            _Cmd &= vbCrLf & ",'" & _FTColorway & "'"
            _Cmd &= vbCrLf & ",'" & _FTSizeBreakDown & "'"
            _Cmd &= vbCrLf & ",'" & _FTOrderNo & "'"
            _Cmd &= vbCrLf & "," & _Qty
            _Cmd &= vbCrLf & ",'" & _PackNo & "'"
            _Cmd &= vbCrLf & "," & _CartonNo
            _Cmd &= vbCrLf & " , '" & _SubOrderNo & "'"
            _Cmd &= vbCrLf & " , " & Integer.Parse(Me.FNHSysWHLocId.Properties.Tag)
            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If
            'End If

            Return True
        Catch ex As Exception
        End Try
    End Function


    Private Sub wScanBarcodeFG_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            'Call LoadData()
            Me.FTBarCodePallet.Text = GenNewBarcodePallet()
            Call LoadPalletInfo()

            Me.FTProductBarcodeNo.EnterMoveNextControl = False
            Me.FTProductBarcodeNo.Focus()
            Me.FTProductBarcodeNo.SelectAll()
        Catch ex As Exception
        End Try
    End Sub


    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmremove.Click
        Try
            If Me.FNHSysWHFGId.Text <> "" Then
                With CType(ogcwarehouse.DataSource, DataTable)
                    .AcceptChanges()
                    For Each R As DataRow In .Select("FTSelect = '1'")
                        Call DelData(R!FTBarCodeCarton.ToString)
                    Next
                    '  Me.LoadDataWareHouse()
                    'Me.LoadData()
                    Me.FTProductBarcodeNo.Focus()
                    Me.FTProductBarcodeNo.SelectAll()
                End With
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, FNHSysWHFGId_lbl.Text)
                Me.FNHSysWHFGId.Focus()
                Exit Sub
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub oGFTSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles oGFTSelectAll.CheckedChanged
        Try
            Dim _State As String = IIf(oGFTSelectAll.Checked = True, "1", "0")
            With ogcwarehouse
                If Not (.DataSource Is Nothing) And ogvwarehouse.RowCount > 0 Then
                    With ogvwarehouse
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, "FTSelect", _State)
                        Next
                    End With
                    CType(.DataSource, DataTable).AcceptChanges()
                End If
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvBalCarton_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvBalCarton.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim category As Integer = View.GetRowCellDisplayText(e.RowHandle, View.Columns("FNCartonTotal"))
                If category > 0 Then
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                    e.HighPriority = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        Try
            If Me.FTBarCodePallet.Text <> "" Then
                With New HI.RP.Report
                    .FormTitle = Me.Text
                    .ReportFolderName = "Production\"
                    .ReportName = "BarCodePalletSlip.rpt"
                    .Formular = "{TPRODTBarcodeScanFG.FTBarCodePallet}='" & HI.UL.ULF.rpQuoted(FTBarCodePallet.Text) & "' "
                    .Preview()
                End With

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmsaveweightpack_Click(sender As Object, e As EventArgs) Handles ocmsaveweightpack.Click

    End Sub

    Private Sub FTProductBarcodeNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTProductBarcodeNo.EditValueChanged

    End Sub
End Class