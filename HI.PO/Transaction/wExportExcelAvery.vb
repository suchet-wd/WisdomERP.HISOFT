Imports Microsoft.Office.Interop.Excel
Imports System.Windows.Forms
Imports System.Text
Imports System.Drawing
Imports System
Imports System.IO


Public Class wExportExcelAvery

    Private _CustItemRef As String = ""
    Private Property CustItemRef As String
        Get
            Return _CustItemRef
        End Get
        Set(value As String)
            _CustItemRef = value
        End Set
    End Property



    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmexportrycexcel_Click(sender As Object, e As EventArgs) Handles ocmexporttoexcel.Click

        If Me.FNHSysBuyId.Text <> "" Then

            If Me.FTFilePath.Text <> "" Then
                Dim _Spls As New HI.TL.SplashScreen("Loading...Data Pleas wait.")
                Dim _Cmd As String = ""
                Dim _dt As System.Data.DataTable
                Dim _dtpurchase As System.Data.DataTable

                Dim CusItemCodeRef As String = CustItemRef
                Dim PurchaseNo As String = ""
                Dim DeliveryDate As String = ""
                Dim StyleNo As String = ""
                Dim DataSeason As String = ""


                Dim ShipAddress1 As String = "HI-TECH APPAREL CO., LTD"
                Dim ShipAddress2 As String = "328 PRACHAUTHIT RD.,"
                Dim ShipAddress3 As String = "THOONGKRU, BANGKOK"
                Dim ShipAddress4 As String = "10140, THAILAND."

                With CType(ogcpurchase.DataSource, System.Data.DataTable)
                    .AcceptChanges()
                    _dtpurchase = .Copy
                End With

                PurchaseNo = ""
                DeliveryDate = ""
                DataSeason = ""
                StyleNo = ""

                For Each R As System.Data.DataRow In _dtpurchase.Select("FTStateSelect='1'", "FTPurchaseNo")

                    If PurchaseNo = "" Then

                        PurchaseNo = R!FTPurchaseNo.ToString
                        DeliveryDate = R!FDDeliveryDate.ToString
                        'StyleNo = R!FTStyleCode.ToString
                        'Try
                        '    DataSeason = R!FTSeasonCode.String()
                        'Catch ex As Exception

                        'End Try


                    Else

                        PurchaseNo = PurchaseNo & "','" & R!FTPurchaseNo.ToString
                        DeliveryDate = DeliveryDate & "','" & R!FDDeliveryDate.ToString
                        'StyleNo = StyleNo & "','" & R!FTStyleCode.ToString

                        'Try
                        '    DataSeason = DataSeason & "','" & R!FTSeasonCode.String
                        'Catch ex As Exception
                        'End Try


                    End If

                Next

                _Cmd = " SELECT ISNULL(X.FTPOref,'') AS FTPOref,SUM(X.FNQuantity)  AS FNQuantity,SUM(X.FNNetAmt) AS FNNetAmt"
                _Cmd &= vbCrLf & " FROM (SELECT A.*,PO.FTPOref "
                _Cmd &= vbCrLf & "  FROM (SELECT PH.FTPurchaseNo, PD.FNQuantity, PD.FNPrice, PD.FNDisPer, PD.FNDisAmt, PD.FNNetAmt, PD.FTOrderNo "
                ' _Cmd &= vbCrLf & "		, [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.fn_PoHeader_StyleSeason(PH.FTPurchaseNo) AS FTSeasonCode "
                _Cmd &= vbCrLf & " FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As PH WITH(NOLOCK) INNER JOIN "
                _Cmd &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo AS PD WITH(NOLOCK) ON PH.FTPurchaseNo = PD.FTPurchaseNo INNER JOIN "
                _Cmd &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM WITH(NOLOCK) On PD.FNHSysRawMatId = IM.FNHSysRawMatId INNER JOIN "
                _Cmd &= vbCrLf & "          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat AS MM WITH(NOLOCK) ON IM.FTRawMatCode = MM.FTMainMatCode "
                _Cmd &= vbCrLf & "          INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS U WITH(NOLOCK)  ON PH.FTPurchaseBy = U.FTUserName "
                _Cmd &= vbCrLf & " WHERE  (MM.FTCusItemCodeRef = N'" & HI.UL.ULF.rpQuoted(CusItemCodeRef) & "') AND (PH.FTPurchaseNo IN ('" & PurchaseNo & "')) "

                _Cmd &= vbCrLf & " ) As A "
                _Cmd &= vbCrLf & " OUTER APPLY ( "
                _Cmd &= vbCrLf & "	SELECT TOP 1 FTPOref  "
                _Cmd &= vbCrLf & "	FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_OrderSub_BreakDown_ShipDestination As X "
                _Cmd &= vbCrLf & "	WHERE X.FTOrderNo = A.FTOrderNo  "
                _Cmd &= vbCrLf & "  ) As PO "
                _Cmd &= vbCrLf & "  ) AS X "
                _Cmd &= vbCrLf & " GROUP BY ISNULL(X.FTPOref,'')"

                _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PUR)

                If _dt.Rows.Count > 0 Then


                    _Cmd = "  Select  ST.FTStyleCode,SS.FTSeasonCode  "
                    _Cmd &= vbCrLf & "   From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As PH WITH(NOLOCK) INNER Join "
                    _Cmd &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo As PD With(NOLOCK) On PH.FTPurchaseNo = PD.FTPurchaseNo  "
                    _Cmd &= vbCrLf & "    INNER Join  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON PD.FTOrderNo = O.FTOrderNo  "
                    _Cmd &= vbCrLf & "   INNER Join   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle As ST With(NOLOCK) On O.FNHSysStyleId =ST.FNHSysStyleId "
                    _Cmd &= vbCrLf & "   INNER Join   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS SS WITH(NOLOCK) ON O.FNHSysSeasonId = SS.FNHSysSeasonId "
                    _Cmd &= vbCrLf & "  WHERE(PH.FTPurchaseNo IN ('" & PurchaseNo & "'))  "
                    _Cmd &= vbCrLf & "  And O.FNOrderType  Not IN (1,2,3,4) "
                    _Cmd &= vbCrLf & "      GROUP BY ST.FTStyleCode, SS.FTSeasonCode "

                    Dim dtinfo As System.Data.DataTable
                    dtinfo = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PUR)
                    StyleNo = ""
                    DataSeason = ""

                    For Each R As System.Data.DataRow In dtinfo.Select("FTStyleCode<>''", "FTStyleCode")


                        If R!FTStyleCode.ToString <> "" Then
                            If StyleNo = "" Then
                                StyleNo = R!FTStyleCode.ToString
                            Else
                                If StyleNo.Contains(R!FTStyleCode.ToString) = False Then
                                    StyleNo = StyleNo & "," & R!FTStyleCode.ToString
                                End If

                            End If
                            End If

                        If R!FTSeasonCode.ToString <> "" Then
                            If DataSeason = "" Then
                                DataSeason = R!FTSeasonCode.ToString
                            Else
                                If DataSeason.Contains(R!FTSeasonCode.ToString) = False Then
                                    DataSeason = DataSeason & "," & R!FTSeasonCode.ToString
                                End If
                            End If
                        End If

                    Next

                    dtinfo.Dispose()

                    '  DataSeason = _dt.Rows(0)!FTSeasonCode.ToString

                    With opshet.ActiveWorksheet
                        .Rows(14).Item(1).Value = PurchaseNo
                        .Rows(15).Item(1).Value = DeliveryDate

                        .Rows(17).Item(1).Value = ShipAddress1
                        .Rows(18).Item(1).Value = ShipAddress2
                        .Rows(19).Item(1).Value = ShipAddress3
                        .Rows(20).Item(1).Value = ShipAddress4

                        .Rows(21).Item(1).Value = ShipAddress1
                        .Rows(22).Item(1).Value = ShipAddress2
                        .Rows(23).Item(1).Value = ShipAddress3
                        .Rows(24).Item(1).Value = ShipAddress4

                        Dim RowIndx As Integer = 0
                        Dim StartRow As Integer = 29
                        Dim Qty As Decimal = 0.00
                        Dim Amt As Decimal = 0.00

                        For Each R As System.Data.DataRow In _dt.Select("FTPOref<>''", "FTPOref")

                            Qty = Val(R!FNQuantity.ToString)
                            Amt = Val(R!FNNetAmt.ToString)

                            If RowIndx = 0 Then

                                For Each Rx As DataRow In _dt.Select("FTPOref=''", "FTPOref")

                                    Qty = Qty + Val(Rx!FNQuantity.ToString)
                                    Amt = Amt + Val(Rx!FNNetAmt.ToString)

                                Next

                            End If


                            If .Rows(StartRow + RowIndx).Item(0).Value.ToString.ToLower() = "TOTAL QTY".ToLower() Then
                                .Rows(StartRow + RowIndx).Insert()
                                .Rows(StartRow + RowIndx).CopyFrom(.Rows(StartRow + RowIndx + 1), DevExpress.Spreadsheet.PasteSpecial.All)
                            End If

                            .Rows(StartRow + RowIndx).Item(0).Value = R!FTPOref.ToString
                            .Rows(StartRow + RowIndx).Item(2).Value = Qty
                            '.Rows(StartRow + RowIndx).Item(3).Value = Amt

                            RowIndx = RowIndx + 1

                        Next

                        For I As Integer = (StartRow + RowIndx) To .GetUsedRange().RowCount - 1

                            If .Rows(I).Item(0).Value.ToString.ToLower() = "STYLE NR.".ToLower() Then
                                .Rows(I).Item(1).Value = StyleNo
                            ElseIf .Rows(I).Item(0).Value.ToString.ToLower() = "SEASON".ToLower() Then
                                .Rows(I).Item(1).Value = DataSeason
                            End If

                        Next

                    End With

                    opshet.SaveDocument(FTFilePath.Text)

                    _Spls.Close()
                    HI.MG.ShowMsg.mInfo("Export Data Complete !!!", 1614413254, Me.Text, , MessageBoxIcon.Warning)
                Else

                    _Spls.Close()
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการ Export กรุณาทำการตรวจสอบ !!!", 1610013254, Me.Text, , MessageBoxIcon.Warning)

                End If

            Else

                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, ogbselectfile.Text)
                FTFilePath.Focus()

            End If

        Else

            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, FNHSysBuyId_lbl.Text)
            FNHSysBuyId.Focus()

        End If

    End Sub

    Private Sub wExportYRCExcel_Load(sender As Object, e As EventArgs) Handles Me.Load
    End Sub

    Private Sub FTFilePath_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTFilePath.ButtonClick
        Select Case e.Button.Index
            Case 0
                Try
                    Dim opFileDialog As New System.Windows.Forms.OpenFileDialog
                    opFileDialog.Filter = "Excel Files(*.xls;*.xlsx;*.csv)|*.xls;*.xlsx;*.csv"
                    opFileDialog.ShowDialog()

                    Try
                        If opFileDialog.FileName <> "" Then

                            ogcpurchase.DataSource = Nothing

                            Dim _Pls As New HI.TL.SplashScreen("Reading...File Please Wait...")
                            Dim _FileName As String = opFileDialog.FileName


                            FTFilePath.Text = _FileName

                            Try
                                Dim proc = Process.GetProcessesByName("excel")
                                For i As Integer = 0 To proc.Count - 1
                                    proc(i).Kill()
                                Next i
                            Catch ex As Exception
                            End Try

                            Dim dt As System.Data.DataTable = HI.UL.ReadExcel.Read(_FileName, "sheet1", 0)
                            'Dim stream As New FileStream(_FileName, FileMode.Open)
                            'Dim length As Long = stream.Length
                            'Dim data(length) As Byte 'New Byte(length)
                            'stream.Read(data, 0, Integer.Parse(length))
                            Dim ItemRef As String = ""
                            CustItemRef = ""

                            If dt.Rows.Count > 7 Then
                                ItemRef = dt.Rows(7).Item(0).ToString
                                ItemRef = ItemRef.Split("-")(1).Trim()
                                ItemRef = ItemRef.Split("#")(1).Trim()

                                CustItemRef = ItemRef
                            End If

                            LoadPurchaseNo()

                            Select Case Path.GetExtension(_FileName)
                                Case ".xls"
                                    opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xls)

                                Case ".xlsx"
                                    opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsx)

                                Case ".xlsm"
                                    opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsm)

                                Case Else
                                    opshet.LoadDocument(File.ReadAllBytes(_FileName), DevExpress.Spreadsheet.DocumentFormat.Xlsx)


                            End Select

                            _Pls.Close()

                            End If
                    Catch ex As Exception
                    End Try

                Catch ex As Exception
                    Throw New Exception(ex.Message().ToString() & Environment.NewLine & ex.StackTrace.ToString())
                End Try

            Case Else
                '...do nothing
        End Select
    End Sub

    Private Sub LoadPurchaseNo()

        Dim _UserName As String = HI.ST.UserInfo.UserName
        Dim CusItemCodeRef As String = CustItemRef
        Dim PurchaseNo As String = ""
        Dim DeliveryDate As String = ""

        ' _UserName = "mlpsirikanya"

        Dim _Qry As String = ""
        Dim _dt As System.Data.DataTable
        _Qry = "  Select '0' AS FTStateSelect, PH.FTPurchaseNo "
        _Qry &= vbCrLf & "		, CASE WHEN ISDATE(PH.FDDeliveryDate) = 1 THEN Convert(nvarchar(10), Convert(Datetime,FDDeliveryDate)  ,103) ELSE '' END AS  FDDeliveryDate "
        '_Qry &= vbCrLf & "		, [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.fn_PoHeader_StyleInfo(PH.FTPurchaseNo) AS FTStyleCode "
        '_Qry &= vbCrLf & "		, [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.fn_PoHeader_StyleSeason(PH.FTPurchaseNo) AS FTSeasonCode "

        _Qry &= vbCrLf & "  From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase As PH With(NOLOCK) INNER Join "
        _Qry &= vbCrLf & "              [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.TPURTPurchase_OrderNo As PD With(NOLOCK) On PH.FTPurchaseNo = PD.FTPurchaseNo INNER Join "
        _Qry &= vbCrLf & "              [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial As IM On PD.FNHSysRawMatId = IM.FNHSysRawMatId INNER Join "
        _Qry &= vbCrLf & "              [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMMainMat As MM On IM.FTRawMatCode = MM.FTMainMatCode "
        _Qry &= vbCrLf & "				  INNER Join [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin As U With(NOLOCK) On PH.FTPurchaseBy = U.FTUserName  "
        _Qry &= vbCrLf & "         INNER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder As OD On PD.FTOrderNo = OD.FTOrderNo "
        _Qry &= vbCrLf & " WHERE(MM.FTCusItemCodeRef = N'" & HI.UL.ULF.rpQuoted(CusItemCodeRef) & "') "
        _Qry &= vbCrLf & " AND    (OD.FNHSysBuyId =" & Val(FNHSysBuyId.Properties.Tag.ToString) & ") "
        _Qry &= vbCrLf & " 	And U.FNHSysTeamGrpId IN  (SELECT  U.FNHSysTeamGrpId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSEUserLogin AS UX WITH(NOLOCK) WHERE UX.FTUserName ='" & HI.UL.ULF.rpQuoted(_UserName) & "' ) "
        _Qry &= vbCrLf & " GROUP BY PH.FTPurchaseNo, PH.FDDeliveryDate  "
        _Qry &= vbCrLf & " ORDER BY PH.FTPurchaseNo "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)

        ogcpurchase.DataSource = _dt.Copy()

        _dt.Dispose()

    End Sub

    Private Sub FNHSysBuyId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysBuyId.EditValueChanged
        CustItemRef = ""
        FTFilePath.Text = ""
        ogcpurchase.DataSource = Nothing
    End Sub

    Private Sub FTFilePath_EditValueChanged(sender As Object, e As EventArgs) Handles FTFilePath.EditValueChanged

    End Sub
End Class