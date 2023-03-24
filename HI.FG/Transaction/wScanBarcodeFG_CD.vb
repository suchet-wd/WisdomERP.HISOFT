Imports System.Windows.Forms
Public Class wScanBarcodeFG_CD

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

    Private Sub LoadData()
        Try
            'Dim _Cmd As String = ""
            'Dim _oDt As DataTable
            '_Cmd = "SELECT  '0' AS FTSelect   ,O.FTPORef ,  B.FTOrderNo, B.FTColorway, B.FTSizeBreakDown,  sum(B.FNScanQuantity) AS FNScanQuantity, isnull(C.FTBarCodeCarton,B.FTBarcodeNo) as FTBarCodeCarton , S.FTStyleCode "
            '_Cmd &= vbCrLf & ", Case When Isdate(max(isnull(P.FDUpdDate , P.FDInsDate))) = 1 Then convert(varchar(10),convert(date,max(isnull(P.FDUpdDate , P.FDInsDate))),103) Else  '' End AS FDDateTrans   "
            '_Cmd &= vbCrLf & " , P.FNCartonNo  , P.FTPackNo , B.FTSubOrderNo "
            '_Cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKCarton AS P WITH(NOLOCK) INNER JOIN    "
            '_Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan AS B WITH (NOLOCK)  ON P.FTPackNo = B.FTPackNo and P.FNCartonNo = B.FNCartonNo"
            '_Cmd &= vbCrLf & "  LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Barcode AS C WITH (NOLOCK) ON B.FTPackNo = C.FTPackNo AND B.FNCartonNo = C.FNCartonNo"
            '_Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON B.FTOrderNo = O.FTOrderNo"
            '_Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS S WITH(NOLOCK) ON O.FNHSysStyleId = S.FNHSysStyleId "
            '_Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBarcodeScanFG  AS FG  WITH(NOLOCK)ON  isnull(C.FTBarCodeCarton,B.FTBarcodeNo) = FG.FTBarCodeCarton and P.FNCartonNo =  FG.FNCartonNo  "
            '_Cmd &= vbCrLf & " and P.FTPackNo = FG.FTPackNo "
            ''_Cmd &= vbCrLf & " WHERE isnull(C.FTBarCodeCarton,B.FTBarcodeNo) +'|'+B.FTOrderNo+'|'+B.FTColorway+'|'+B.FTSizeBreakDown +'|'+P.FTPackNo+'|'+convert(nvarchar(18) , P.FNCartonNo) not in ("
            ''_Cmd &= vbCrLf & " SELECT     FTBarCodeCarton +'|'+FTOrderNo +'|'+FTColorWay +'|'+FTSizeBreakDown+'|'+FTPackNo+'|'+convert(nvarchar(18) , FNCartonNo)  "
            ''_Cmd &= vbCrLf & " FROM TPRODTBarcodeScanFG WITH(NOLOCK) )"
            '_Cmd &= vbCrLf & "  where FG.FTBarCodeCarton is null  "
            '_Cmd &= vbCrLf & "Group by  B.FTOrderNo, B.FTColorway, B.FTSizeBreakDown,isnull(C.FTBarCodeCarton,B.FTBarcodeNo ), S.FTStyleCode ,O.FTPORef   "
            '_Cmd &= vbCrLf & " , P.FNCartonNo  , P.FTPackNo , B.FTSubOrderNo"
            '_Cmd &= vbCrLf & "Order by max(isnull(P.FDUpdDate , P.FDInsDate)) ASC "
            '_oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            'ogcdetail.DataSource = _oDt
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadDataWareHouse()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            _Cmd = "SELECT  '0' AS FTSelect ,  F.FTBarCodeCarton, F.FNHSysWHFGId, F.FTColorWay, F.FTSizeBreakDown, F.FTOrderNo, F.FNQuantity , S.FTStyleCode , O.FTPORef , F.FTSubOrderNo "
            _Cmd &= vbCrLf & "   FROM  TPRODTBarcodeScanFG AS F WITH(NOLOCK) "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrder AS O WITH(NOLOCK) ON F.FTOrderNo = O.FTOrderNo"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMStyle AS S WITH(NOLOCK) ON O.FNHSysStyleId = S.FNHSysStyleId"
            _Cmd &= vbCrLf & " Where F.FDInsDate = " & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & " AND F.FNHSysWHFGId=" & CInt(Me.FNHSysWHFGId.Properties.Tag)
            'If Me.FNHSysWHLocId.Text <> "" Then
            '    _Cmd &= vbCrLf & " AND F.FNHSysWHLocId=" & CInt(Me.FNHSysWHLocId.Properties.Tag)
            'End If

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcwarehouse.DataSource = _oDt
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            Me.LoadData()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FNHSysWHFGId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysWHFGId.EditValueChanged, FNHSysWHLocId.EditValueChanged
        Try
            If (Me.FNHSysWHFGId.Properties.Tag.ToString = "0" Or Me.FNHSysWHFGId.Properties.Tag.ToString = "") And Me.FNHSysWHFGId.Text <> "" Then
                Me.FNHSysWHFGId.Properties.Tag = HI.Conn.SQLConn.GetField(" Select Top 1 FNHSysWHFGId From [HITECH_MASTER]..TCNMWarehouseFG  with(nolock) Where FTWHFGCode = '" & Me.FNHSysWHFGId.Text & "'", Conn.DB.DataBaseName.DB_MASTER, "0")
            End If
            If (Me.FNHSysWHLocId.Properties.Tag.ToString = "0" Or Me.FNHSysWHLocId.Properties.Tag.ToString = "") And Me.FNHSysWHLocId.Text <> "" Then
                Me.FNHSysWHLocId.Properties.Tag = HI.Conn.SQLConn.GetField(" Select Top 1 FNHSysWHLocId From [HITECH_MASTER]..TCNMWarehouseLocationFG with(nolock) Where FTWHLocCode = '" & Me.FNHSysWHLocId.Text & "'", Conn.DB.DataBaseName.DB_MASTER, "0")
            End If
            Call LoadDataWareHouse()
        Catch ex As Exception
        End Try
    End Sub

    Private Function Chkmapbarcode(_FTProductBarcodeNo As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = " Select top 1 FTBarCodeEAN13 "
            _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS A WITH (NOLOCK) "
            _Cmd &= vbCrLf & " where FTBarCodeEAN13='" & _FTProductBarcodeNo & "'"
            If HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count = 1 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function ChkCartonToWH(_FTProductBarcodeNo As String) As Boolean
        Try
            Dim _Cmd As String = ""

            _Cmd = " Select Top 1  A.FTPackNo, A.FNHSysStyleId, B.FNCartonNo, B.FTBarCodeCarton, B.FTCartonNo, B.FTBarCodeEAN13 "
            _Cmd &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack AS A WITH (NOLOCK) LEFT OUTER JOIN "
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS B WITH (NOLOCK) ON A.FTPackNo = B.FTPackNo LEFT OUTER JOIN "
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS D WITH (NOLOCK) ON A.FTPackNo = D.FTPackNo AND B.FNCartonNo = D.FNCartonNo"
            _Cmd &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCarton AS C WITH(NOLOCK)  ON  D.FNHSysCartonId = C.FNHSysCartonId"
            _Cmd &= vbCrLf & " WHERE   not exists ( "
            _Cmd &= vbCrLf & "  Select *  "
            _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPRODTBarcodeScanFG AS AA with(NOLOCK) "
            _Cmd &= vbCrLf & " where AA.FTBarCodeCarton = B.FTBarCodeCarton  ) "
            _Cmd &= vbCrLf & " and B.FTBarCodeCarton ='" & _FTProductBarcodeNo & "'"
            If HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count = 1 Then
                Return True
            End If
            Return False
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
                If Me.FNHSysWHFGId.Text <> "" Then
                    If Not Me.FTStateDeleteBarcode.Checked Then
                        'Save
                        If Not (Chkmapbarcode(Me.FTProductBarcodeNo.Text.Trim)) Then
                            HI.MG.ShowMsg.mInfo("บาร์โค้ดยังไม่มีการแสกนปิดกล่อง หรือ แสกนเข้าคลังสำเร็จรูปหมดแล้ว กรุณาตรวจสอบ!!!", 1510101118, Me.Text)
                            Me.FTProductBarcodeNo.Focus()
                            Me.FTProductBarcodeNo.SelectAll()
                            Exit Sub
                        End If
                        If Not (ChkCartonToWH(Me.FTProductBarcodeNo.Text.Trim)) Then
                            HI.MG.ShowMsg.mInfo("มีแสกนเข้าคลังไปแล้ว กรุณาตรวจสอบ...", 1903120953, Me.Text)
                            Me.FTProductBarcodeNo.Focus()
                            Me.FTProductBarcodeNo.SelectAll()
                            Exit Sub
                        End If
                        If SaveData(Me.FTProductBarcodeNo.Text.ToUpper.Trim, False) Then
                            Me.LoadDataWareHouse()
                            Me.LoadData()
                            Me.FTProductBarcodeNo.Focus()
                            Me.FTProductBarcodeNo.SelectAll()
                        Else
                            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                            ' HI.MG.ShowMsg.mInfo("บาร์โค้ดยังไม่มีการแสกนปิดกล่อง หรือ แสกนเข้าคลังสำเร็จรูปหมดแล้ว กรุณาตรวจสอบ!!!", 1510101118, Me.Text)
                        End If
                    Else
                        'Delete
                        If DelData(Me.FTProductBarcodeNo.Text.ToUpper.Trim) Then
                            Me.LoadDataWareHouse()
                            Me.LoadData()
                            Me.FTProductBarcodeNo.Focus()
                            Me.FTProductBarcodeNo.SelectAll()
                        End If
                    End If
                    Me.FTProductBarcodeNo.Focus()
                    Me.FTProductBarcodeNo.SelectAll()

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, FNHSysWHFGId_lbl.Text)
                    Me.FNHSysWHFGId.Focus()
                    Exit Sub
                End If
                Me.FTProductBarcodeNo.Focus()
                Me.FTProductBarcodeNo.SelectAll()
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
            _Cmd = " Delete From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG   "
            _Cmd &= vbCrLf & " WHERE FTBarCodeCarton='" & _FTProductBarcodeNo & "'"
            _Cmd &= vbCrLf & " And FNHSysWHFGId=" & CInt(Me.FNHSysWHFGId.Properties.Tag)
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
            '_Cmd = "UPDATE W"
            '_Cmd &= vbCrLf & "Set W.FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            '_Cmd &= vbCrLf & ", W.FDUpdDate=" & HI.UL.ULDate.FormatDateDB
            '_Cmd &= vbCrLf & ",W.FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
            '_Cmd &= vbCrLf & ",W.FNQuantity=T.FNScanQuantity"

            '_Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS W INNER JOIN "
            '_Cmd &= vbCrLf & "   (SELECT  P.FTPackNo , P.FNCartonNo,  B.FTOrderNo, B.FTColorway, B.FTSizeBreakDown, sum(B.FNScanQuantity ) AS FNScanQuantity ,Isnull(C.FTBarCodeCarton , B.FTBarcodeNo) as FTBarCodeCarton "
            ''_Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS B WITH (NOLOCK) LEFT OUTER JOIN"
            '_Cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKCarton AS P WITH(NOLOCK) INNER JOIN    "
            '_Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPACKOrderPack_Carton_Scan AS B WITH (NOLOCK)  ON P.FTPackNo = B.FTPackNo and P.FNCartonNo = B.FNCartonNo"
            '_Cmd &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS C WITH (NOLOCK) ON P.FTPackNo = C.FTPackNo AND P.FNCartonNo = C.FNCartonNo "
            '_Cmd &= vbCrLf & " WHERE (C.FTBarCodeCarton='" & _FTProductBarcodeNo & "'"
            '_Cmd &= vbCrLf & " OR   B.FTBarcodeNo='" & _FTProductBarcodeNo & "')"
            'If (state) Then
            '    _Cmd &= vbCrLf & " And  B.FTOrderNo='" & _FTOrderNo & "'"
            '    _Cmd &= vbCrLf & " AND B.FTColorway='" & _FTColorway & "'"
            '    _Cmd &= vbCrLf & " And B.FTSizeBreakDown='" & _FTSizeBreakDown & "'"
            'End If

            '_Cmd &= vbCrLf & "  and  isnull(C.FTBarCodeCarton,B.FTBarcodeNo) +'|'+B.FTOrderNo+'|'+B.FTColorway+'|'+B.FTSizeBreakDown +'|'+P.FTPackNo+'|'+convert(nvarchar(18) , P.FNCartonNo) not in ("
            '_Cmd &= vbCrLf & " SELECT     FTBarCodeCarton +'|'+FTOrderNo +'|'+FTColorWay +'|'+FTSizeBreakDown+'|'+FTPackNo+'|'+convert(nvarchar(18) , FNCartonNo)  "
            '_Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG WITH(NOLOCK) )"

            '_Cmd &= vbCrLf & "Group by P.FTPackNo , P.FNCartonNo, B.FTOrderNo, B.FTColorway, B.FTSizeBreakDown ,Isnull(C.FTBarCodeCarton , B.FTBarcodeNo) ) AS T "
            '_Cmd &= vbCrLf & "ON W.FTOrderNo = T.FTOrderNo and W.FTColorWay = T.FTColorway and W.FTSizeBreakDown = T.FTSizeBreakDown and W.FTBarCodeCarton = T.FTBarCodeCarton and W.FTPackNo = T.FTPackNo and W.FNCartonNo = T.FNCartonNo"
            '_Cmd &= vbCrLf & " WHERE  W.FNHSysWHFGId=" & CInt(Me.FNHSysWHFGId.Properties.Tag)




            ' If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            _Cmd = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG (FTInsUser, FDInsDate, FTInsTime, FTBarCodeCarton, FNHSysWHFGId, FTColorWay, FTSizeBreakDown, FTOrderNo, FNQuantity , FTPackNo , FNCartonNo ,FTSubOrderNo,FNHSysWHLocId)"
            _Cmd &= vbCrLf & "Select Top 1 '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",'" & _FTProductBarcodeNo & "'"
            _Cmd &= vbCrLf & "," & CInt(Me.FNHSysWHFGId.Properties.Tag)
            _Cmd &= vbCrLf & ",D.FTColorWay"
            _Cmd &= vbCrLf & ",D.FTSizeBreakDown"
            _Cmd &= vbCrLf & ",D.FTOrderNo"
            _Cmd &= vbCrLf & ",D.FNQuantity"
            _Cmd &= vbCrLf & ",A.FTPackNo"
            _Cmd &= vbCrLf & ",B.FNCartonNo"
            _Cmd &= vbCrLf & ",D.FTSubOrderNo"
            _Cmd &= vbCrLf & "," & CInt(Me.FNHSysWHLocId.Properties.Tag)
            '_Cmd &= vbCrLf & " From (SELECT  P.FTPackNo , P.FNCartonNo,    B.FTOrderNo, B.FTColorway, B.FTSizeBreakDown, sum(B.FNScanQuantity ) AS FNScanQuantity,Isnull(C.FTBarCodeCarton , B.FTBarcodeNo) as FTBarCodeCarton "
            '_Cmd &= vbCrLf & " FROM         [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Scan AS B WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack AS A WITH (NOLOCK) LEFT OUTER JOIN "
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Barcode AS B WITH (NOLOCK) ON A.FTPackNo = B.FTPackNo LEFT OUTER JOIN "
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPACKOrderPack_Carton_Detail AS D WITH (NOLOCK) ON A.FTPackNo = D.FTPackNo AND B.FNCartonNo = D.FNCartonNo"
            _Cmd &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCarton AS C WITH(NOLOCK)  ON  D.FNHSysCartonId = C.FNHSysCartonId"
            _Cmd &= vbCrLf & " WHERE   not exists ( "
            _Cmd &= vbCrLf & "  Select *  "
            _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPRODTBarcodeScanFG AS AA with(NOLOCK) "
            _Cmd &= vbCrLf & " where AA.FTBarCodeCarton = B.FTBarCodeCarton  ) "
            _Cmd &= vbCrLf & " and B.FTBarCodeCarton ='" & _FTProductBarcodeNo & "'"





            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If
            ' End If
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
            Call LoadData()
            Me.FTProductBarcodeNo.EnterMoveNextControl = False
            Me.FTProductBarcodeNo.Focus()
            Me.FTProductBarcodeNo.SelectAll()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub oFTSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles oFTSelectAll.CheckedChanged
        Try
            Dim _State As String = IIf(oFTSelectAll.Checked = True, "1", "0")
            With ogcdetail
                If Not (.DataSource Is Nothing) And ogvdetail.RowCount > 0 Then
                    With ogvdetail
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

    Private Sub ocmToWHFG_Click(sender As Object, e As EventArgs) Handles ocmToWHFG.Click
        If Me.FNHSysWHFGId.Text <> "" Then
            Dim _Spls As New HI.TL.SplashScreen("Saving Data Pls Wait !!!")
            With CType(ogcdetail.DataSource, DataTable)
                .AcceptChanges()


                HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
                HI.Conn.SQLConn.SqlConnectionOpen()
                HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

                Dim _RowTotal As Integer = .Select("FTSelect = '1'").Length
                Dim _RSeq As Integer = 0

                For Each R As DataRow In .Select("FTSelect = '1'")
                    _RSeq += +1
                    _Spls.UpdateInformation("Saving Data Pls Wait !!! ( Record " & _RSeq & " Of " & _RowTotal & " )")

                    Call SaveDataGrid(R!FTBarCodeCarton.ToString, True, R!FNScanQuantity.ToString, R!FTPackNo.ToString, R!FNCartonNo.ToString, R!FTOrderNo.ToString, R!FTColorway.ToString, R!FTSizeBreakDown.ToString)
                Next


                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)



                Me.LoadDataWareHouse()
                Me.LoadData()
            End With
            _Spls.Close()
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, FNHSysWHFGId_lbl.Text)
            Me.FNHSysWHFGId.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmremove.Click
        Try
            If Me.FNHSysWHFGId.Text <> "" Then
                With CType(ogcwarehouse.DataSource, DataTable)
                    .AcceptChanges()
                    For Each R As DataRow In .Select("FTSelect = '1'")
                        Call DelData(R!FTBarCodeCarton.ToString)
                    Next
                    Me.LoadDataWareHouse()
                    Me.LoadData()
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

    Private Sub ogvwarehouse_RowCountChanged(sender As Object, e As EventArgs) Handles ogvwarehouse.RowCountChanged
        Try

            lblQtyScan.Text = HI.UL.ULF.rpQuoted(ogvwarehouse.RowCount.ToString)
        Catch ex As Exception

        End Try
    End Sub
End Class