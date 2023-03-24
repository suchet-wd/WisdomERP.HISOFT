Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Public Class wFGMoveLocation
    'Private _SelectLocation As wFGLocationSelect

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        '_SelectLocation = New wFGLocationSelect

        'HI.TL.HandlerControl.AddHandlerObj(_SelectLocation)
        'Dim oSysLang As New ST.SysLanguage
        'Try
        '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _SelectLocation.Name.ToString.Trim, _SelectLocation)
        '    'Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _PackingPlandPopup.Name.ToString.Trim, _PackingPlandPopup)
        '    'Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _InvoicePost.Name.ToString.Trim, _InvoicePost)
        'Catch ex As Exception

        'End Try
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
    'Private Function CalsPalletBal(barcode As String, Optional ByVal stateDel As Boolean = False) As Boolean
    '    Try
    '        Dim _Cmd As String = "" : Dim _oDt As DataTable
    '        _Cmd = "SELECT top 1 FTBarCodeCarton  ,FNHSysCartonId "
    '        _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "]..TPRODTBarcodeScanFG WITH (NOLOCK)"
    '        _Cmd &= vbCrLf & " where FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(barcode) & "'"
    '        _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)

    '        Dim _dt As DataTable
    '        With DirectCast(Me.ogcBalCarton.DataSource, DataTable)
    '            .AcceptChanges()
    '            _dt = .Copy()
    '        End With
    '        Dim _totalCaton As Integer = 0
    '        For Each R As DataRow In _dt.Select("FNHSysCartonId=" & _oDt.Rows(0).Item("FNHSysCartonId"))

    '            If Not (stateDel) Then
    '                _totalCaton = R!FNCartonTotal + 1
    '                R!FNCartonTotal += +1
    '                R!FNCBM = (_totalCaton * R!Carton)
    '                R!FNCartonBal = R!FNCartonBal - 1


    '            Else
    '                _totalCaton = R!FNCartonTotal - 1
    '                R!FNCartonTotal = R!FNCartonTotal - 1
    '                R!FNCBM = (_totalCaton * R!Carton)
    '                R!FNCartonBal = R!FNCartonBal + 1
    '                ' R!FNCartonBal = 2.1888 -
    '            End If
    '        Next
    '        Dim x As Double = _dt.Compute("sum(FNCBM)", "FNCBM > 0")

    '        For Each R As DataRow In _dt.Rows
    '            R!FNCartonBal = Math.Floor((2.1888 - x) / R!Carton)
    '        Next
    '        Me.ogcBalCarton.DataSource = _dt
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    'Private Sub LoadPalletInfo()
    '    Try
    '        Dim _Cmd As String = ""
    '        _Cmd = "SELECT FNHSysCartonId, FTCartonCode, ((FNWidth /1000) * (FNLength /1000) )* (FNHeight /1000) AS Carton "
    '        _Cmd &= vbCrLf & " , (1.20*1.20) * 1.52 as Pallet , Floor (( (1.20*1.20) * 1.52) / (((FNWidth /1000) * (FNLength /1000) )* (FNHeight /1000))) AS FNCartonBal "
    '        _Cmd &= vbCrLf & " , 0 as FNCartonTotal , 0.00 as FNCBM"
    '        _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TCNMCarton WITH (NOLOCK)"
    '        _Cmd &= vbCrLf & " where  (FTStateActive = '1')  Order by FTCartonCode asc "
    '        Me.ogcBalCarton.DataSource = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MASTER)
    '    Catch ex As Exception

    '    End Try
    'End Sub

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
                Me.FNHSysWHFGId.Properties.Tag = HI.Conn.SQLConn.GetField(" Select Top 1 FNHSysWHFGId From " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMWarehouseFG Where FTWHFGCode = '" & Me.FNHSysWHFGId.Text & "'", Conn.DB.DataBaseName.DB_MASTER, "0")
            End If
            ' Call LoadDataWareHouse()
        Catch ex As Exception
        End Try
    End Sub



    Private Sub LoadDataWareHouse()
        Try
            Dim _whLocId As Integer = Val(Me.FNHSysWHLocId.Properties.Tag)
            Dim _WhId As Integer = Val(Me.FNHSysWHFGId.Properties.Tag)

            Dim _Cmd As String = "" : Dim _oDt As DataTable
            _Cmd = "SELECT A.FTColorWay, A.FTSizeBreakDown, A.FTOrderNo, A.FNQuantity, A.FTPackNo, A.FNCartonNo, A.FTSubOrderNo, A.FNHSysWHLocId, A.FTBarCodePallet"
            _Cmd &= vbCrLf & " , A.FNHSysCartonId, A.FTCustomerPO, A.FTPOLine, A.FTBarCodeCarton,A.FNHSysWHFGId, B.FTCartonCode"
            _Cmd &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTBarcodeScanFG AS A WITH (nolock) LEFT OUTER JOIN "
            _Cmd &= vbCrLf & "      " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMCarton AS B WITH (NoLock) ON A.FNHSysCartonId = B.FNHSysCartonId "
            _Cmd &= vbCrLf & "  WHERE  (A.FNHSysWHFGId = " & _WhId & ") "
            _Cmd &= vbCrLf & " and A.FNHSysWHLocId=" & Val(_whLocId)
            _Cmd &= vbCrLf & " and A.FTBarCodePallet='" & FTProductBarcodeNo.Text & "'"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Me.ogcwarehouse.DataSource = _oDt


        Catch ex As Exception
        End Try
    End Sub

    Private Sub FTProductBarcodeNo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles FTProductBarcodeNo.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Me.FTProductBarcodeNo.Text = "" Then
                    Me.FTProductBarcodeNo.Focus()
                    Exit Sub
                End If

                Dim _Cmd As String = ""



                Select Case Me.FNMoveType.SelectedIndex
                    Case 0
                        'ย้ายสถานที่จัดเก็บ
                        If Me.FNHSysWHLocId.Text = "" Then
                            Me.FNHSysWHLocId.Focus()
                            Exit Sub
                        End If


                        _Cmd = "UPDATE W"
                        _Cmd &= vbCrLf & "Set W.FNHSysWHLocId=" & Val(Me.FNHSysWHLocId.Properties.Tag)
                        _Cmd &= vbCrLf & "From  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & ".dbo.TPRODTBarcodeScanFG AS W "
                        _Cmd &= vbCrLf & " WHERE W.FTBarCodePallet='" & FTProductBarcodeNo.Text & "'"
                        If HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD) Then
                            HI.MG.ShowMsg.mInfo("ย้ายสถานที่จัดเก็บเรียบร้อย....", 1902041703, Me.Text)
                        End If
                        Call LoadDataWareHouse()
                        Me.FTProductBarcodeNo.Focus()
                        Me.FTProductBarcodeNo.SelectAll()

                    Case 1
                        'สลับตำแหน่งจัดเก็บ
                        If Me.FTBarcodeLocation.Text = "" Then
                            Me.FTBarcodeLocation.Focus()
                            Exit Sub
                        End If
                        SwitchPalletToLocation(Me.FTBarcodeLocation.Text, Me.FTProductBarcodeNo.Text)
                    Case 2
                        'ย้ายกล่องไปพาเลท
                        If Me.FTBarcodeLocation.Text = "" Then
                            Me.FTBarcodeLocation.Focus()
                            Exit Sub
                        End If
                        MoveCartonToPallet(Me.FTBarcodeLocation.Text, Me.FTProductBarcodeNo.Text)

                    Case Else


                End Select

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




    Private Sub wScanBarcodeFG_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            'Call LoadData()
            '  Me.FTBarCodePallet.Text = GenNewBarcodePallet()
            'Call LoadPalletInfo()

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

    Private Sub ogvBalCarton_RowStyle(sender As Object, e As RowStyleEventArgs)
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
        'Try
        '    If Me.FTBarCodePallet.Text <> "" Then
        '        With New HI.RP.Report
        '            .FormTitle = Me.Text
        '            .ReportFolderName = "Production\"
        '            .ReportName = "BarCodePalletSlip.rpt"
        '            .Formular = "{TPRODTBarcodeScanFG.FTBarCodePallet}='" & HI.UL.ULF.rpQuoted(FTBarCodePallet.Text) & "' "
        '            .Preview()
        '        End With

        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub ocmsaveweightpack_Click(sender As Object, e As EventArgs) Handles ocmsaveweightpack.Click

    End Sub

    Private Sub FTProductBarcodeNo_EditValueChanged(sender As Object, e As EventArgs) Handles FTProductBarcodeNo.EditValueChanged

    End Sub

    Private Sub TextEdit1_KeyDown(sender As Object, e As KeyEventArgs) Handles FTBarcodeLocation.KeyDown
        Try
            Dim _Cmd As String = ""
            If e.KeyCode = Keys.Enter Then
                If Me.FTBarcodeLocation.Text = "" Then Exit Sub
                _Cmd = "SELECT  Top 1  L.FNHSysWHLocId,   L.FTWHLocCode ,L.FTStateActive  , F.FTWHFGCode  ,  F.FNHSysWHFGId  "
                _Cmd &= vbCrLf & " From   " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMWarehouseLocationFG AS L With(nolock)"
                _Cmd &= vbCrLf & " INNER JOIN " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMWarehouseFG  AS F With(nolock) on L.FNHSysWHFGId = F.FNHSysWHFGId"
                _Cmd &= vbCrLf & " where L.FTWHLocCode='" & HI.UL.ULF.rpQuoted(Me.FTBarcodeLocation.Text) & "'"
                _Cmd &= vbCrLf & " and  F.FNHSysCmpId = " & Val(HI.ST.SysInfo.CmpID)
                Dim _oDt As DataTable
                _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_MASTER)
                For Each R As DataRow In _oDt.Rows
                    Me.FNHSysWHLocId.Text = R!FTWHLocCode.ToString
                    Me.FNHSysWHFGId.Text = R!FTWHFGCode.ToString
                Next


            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FTBarcodeCartonNo_KeyDown(sender As Object, e As KeyEventArgs)
        'Try
        '    ' barcode carton change to new pallet
        '    If Me.FTPalletNoTo.Text <> "" And Me.FTBarcodeCartonNo.Text <> "" Then
        '        Call MoveCartonToPallet(Me.FTPalletNoTo.Text, Me.FTBarcodeCartonNo.Text)
        '    End If
        'Catch ex As Exception
        'End Try
    End Sub

    Private Function MoveCartonToPallet(ToPalletNo As String, BarcodeCartonNo As String) As Boolean
        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            Dim _Cmd As String = "" : Dim _BarcodePallet As Boolean = False

            _Cmd = "Select Top 1  *   "
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS W  "
            _Cmd &= vbCrLf & " WHERE W.FTBarCodePallet='" & HI.UL.ULF.rpQuoted(BarcodeCartonNo) & "'"
            _BarcodePallet = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows.Count > 0

            If (_BarcodePallet) Then
                _Cmd = "UPDATE W"
                _Cmd &= vbCrLf & "Set W.FTBarCodePallet='" & HI.UL.ULF.rpQuoted(ToPalletNo) & "'"
                _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS W  "
                '_Cmd &= vbCrLf & " WHERE  W.FNHSysWHFGId=" & CInt(Me.FNHSysWHFGId.Properties.Tag)
                _Cmd &= vbCrLf & " WHERE W.FTBarCodePallet='" & HI.UL.ULF.rpQuoted(BarcodeCartonNo) & "'"
            Else
                _Cmd = "UPDATE W"
                _Cmd &= vbCrLf & "Set W.FTBarCodePallet='" & HI.UL.ULF.rpQuoted(ToPalletNo) & "'"
                _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS W  "
                '_Cmd &= vbCrLf & " WHERE  W.FNHSysWHFGId=" & CInt(Me.FNHSysWHFGId.Properties.Tag)
                _Cmd &= vbCrLf & " WHERE W.FTBarCodeCarton='" & HI.UL.ULF.rpQuoted(BarcodeCartonNo) & "'"
            End If


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

    Private Sub TextEdit3_KeyDown(sender As Object, e As KeyEventArgs)
        Try
            'If e.KeyCode = Keys.Enter Then
            '    If FTPalletFrom.Text <> "" And FTPalletTo.Text <> "" Then
            '        Call SwitchPalletToLocation(FTPalletFrom.Text, FTPalletTo.Text)
            '    End If
            'End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function SwitchPalletToLocation(LocationNo1 As String, LocationNo2 As String) As Boolean
        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try
            Dim _Cmd As String = ""
            Dim _PalletNo1 As String = "" : Dim _WHLocId1 As Integer = 0
            Dim _PalletNo2 As String = "" : Dim _WHLocId2 As Integer = 0
            _Cmd = "Select Top 1  isnull( W.FTBarCodePallet,'')  as  FTBarCodePallet , W.FNHSysWHLocId   "
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS W with(nolock) "
            _Cmd &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocationFG AS F with(nolock) ON W.FNHSysWHLocId = F.FNHSysWHLocId"
            _Cmd &= vbCrLf & " WHERE F.FTWHLocCode='" & HI.UL.ULF.rpQuoted(LocationNo1) & "'"
            _Cmd &= vbCrLf & " and isnull( W.FTBarCodePallet,'') <> '' "
            For Each Rx As DataRow In HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows
                _PalletNo1 = Rx!FTBarCodePallet.ToString
                _WHLocId1 = Rx!FNHSysWHLocId.ToString
            Next


            _Cmd = "Select Top 1  isnull( W.FTBarCodePallet,'')  as  FTBarCodePallet  , W.FNHSysWHLocId  "
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS W with(nolock) "
            _Cmd &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouseLocationFG AS F with(nolock) ON W.FNHSysWHLocId = F.FNHSysWHLocId"
            _Cmd &= vbCrLf & " WHERE F.FTWHLocCode='" & HI.UL.ULF.rpQuoted(LocationNo2) & "'"
            _Cmd &= vbCrLf & " and isnull( W.FTBarCodePallet,'') <> '' "
            For Each Rx As DataRow In HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD).Rows
                _PalletNo2 = Rx!FTBarCodePallet.ToString
                _WHLocId2 = Rx!FNHSysWHLocId.ToString
            Next


            _Cmd = "UPDATE W"
            _Cmd &= vbCrLf & "Set W.FNHSysWHLocId=" & Val(_WHLocId1)
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS W  "
            '_Cmd &= vbCrLf & " WHERE  W.FNHSysWHFGId=" & CInt(Me.FNHSysWHFGId.Properties.Tag)
            _Cmd &= vbCrLf & " WHERE W.FTBarCodePallet='" & HI.UL.ULF.rpQuoted(_PalletNo2) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                'HI.Conn.SQLConn.Tran.Rollback()
                'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                'Return False
            End If

            _Cmd = "UPDATE W"
            _Cmd &= vbCrLf & "Set W.FNHSysWHLocId=" & Val(_WHLocId2)
            _Cmd &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBarcodeScanFG AS W  "
            '_Cmd &= vbCrLf & " WHERE  W.FNHSysWHFGId=" & CInt(Me.FNHSysWHFGId.Properties.Tag)
            _Cmd &= vbCrLf & " WHERE W.FTBarCodePallet='" & HI.UL.ULF.rpQuoted(_PalletNo1) & "'"


            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                'HI.Conn.SQLConn.Tran.Rollback()
                'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                'Return False
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

End Class