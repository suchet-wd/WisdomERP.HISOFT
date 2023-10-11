Imports System.IO
Imports DevExpress.Pdf
Imports DevExpress.XtraBars
Imports DevExpress.XtraEditors
Imports System.Text
Imports System.Windows.Forms
Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing
Imports DevExpress.Spreadsheet
Imports DevExpress.Spreadsheet.Export
Imports DevExpress.XtraEditors.Controls

Public Class wImportExcelDivert

    Private PathFileExcel As String = ""
    Private SizeDivert As String = ""
    Private pListBreakDown As New List(Of JobBreakDown)()
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


    End Sub


    Private Sub FTFilePath_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTFilePath.ButtonClick
        Select Case e.Button.Index
            Case 0
                SizeDivert = ""
                Dim pMessageError As String = ""

                PathFileExcel = HI.Conn.SQLConn.GetField("SELECT TOP 1  FTCfgData FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE X.FTCfgName='ImportBomExcel'", Conn.DB.DataBaseName.DB_SYSTEM)

                If CheckWriteFile() = False Then
                    HI.MG.ShowMsg.mInfo("ไม่สามารถเข้าถึงที่เก็บ File สำหรับ Import ได้ !!!!", 1909270019, Me.Text)
                    Exit Sub
                End If

                Try
                    Dim opFileDialog As New System.Windows.Forms.OpenFileDialog
                    opFileDialog.Filter = "Excel Worksheets (*.xls, *.xlsx, *.xlsm)|*.xls;*.xlsx;*.xlsm"
                    opFileDialog.ShowDialog()

                    Try

                        If opFileDialog.FileName <> "" Then

                            Me.ogdmain.DataSource = Nothing
                            ClearColumnGrid()

                            Dim _FileName As String = opFileDialog.FileName
                            Me.FTFilePath.Text = _FileName

                            Dim Spls As New HI.TL.SplashScreen("Loading...")
                            opshet.BeginUpdate()
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
                            opshet.EndUpdate()
                            'Dim usedRange As DevExpress.Spreadsheet.CellRange = opshet.ActiveWorksheet.GetUsedRange()
                            'usedRange.NumberFormat = "@"

                            For CIdx As Integer = 0 To opshet.ActiveWorksheet.GetUsedRange().ColumnCount - 1
                                Try
                                    'If opshet.ActiveWorksheet.Columns(CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.DateTime Then
                                    'Else
                                    '    If opshet.ActiveWorksheet.Columns(CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.Numeric Then
                                    '        opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "@"
                                    '    End If
                                    'End If

                                    If opshet.ActiveWorksheet.Cells(1, CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.DateTime Then

                                    Else
                                        If opshet.ActiveWorksheet.Cells(1, CIdx).Value.Type = DevExpress.Spreadsheet.CellValueType.Numeric Then
                                            opshet.ActiveWorksheet.Columns(CIdx).NumberFormat = "@"
                                        End If
                                    End If

                                Catch ex As Exception

                                End Try


                            Next


                            Dim FileName As String = PathFileExcel & "\" & HI.ST.UserInfo.UserName & ".xlsx"
                            opshet.SaveDocument(PathFileExcel & "\" & HI.ST.UserInfo.UserName & ".xlsx")

                            Dim cmdstring As String
                            cmdstring = "  EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.USP_IMPORTFILEEXCEL_DIVERT  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(FileName) & "'"

                            Dim ds As New DataSet

                            HI.Conn.SQLConn.GetDataSet(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN, ds)

                            pMessageError = ""

                            Try

                                pMessageError = ds.Tables(3).Rows(0)!FTColumn.ToString

                                If pMessageError = "" Then
                                    Try


                                        Me.ogdmain.DataSource = ds.Tables(0).Copy


                                        SetNewColumn(ds.Tables(1).Copy)
                                        ColumnDivert(ds.Tables(2).Copy)

                                        SetFilerColumn()

                                        Try
                                            System.IO.File.Delete(FileName)
                                        Catch ex As Exception
                                        End Try

                                    Catch ex As Exception

                                    End Try
                                End If

                            Catch ex As Exception
                                pMessageError = ex.Message
                            End Try


                            ds.Dispose()

                            Spls.Close()

                            If pMessageError <> "" Then
                                MsgBox(pMessageError)
                            End If

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

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
        Me.ogdmain.DataSource = Nothing
        ClearColumnGrid()
        Call LoadProvince()
    End Sub


    Private Sub Form_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call LoadProvince()

    End Sub


    Private Function CheckWriteFile() As Boolean

        Try

            If (Not System.IO.Directory.Exists(PathFileExcel)) Then
                System.IO.Directory.CreateDirectory(PathFileExcel)
            End If

            If (Not System.IO.Directory.Exists(PathFileExcel & "\TestExcel")) Then
                System.IO.Directory.CreateDirectory(PathFileExcel & "\TestExcel")
            End If
            System.IO.Directory.Delete(PathFileExcel & "\TestExcel")


            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function

    Private Sub ocmimportbimpdf_Click(sender As Object, e As EventArgs) Handles ocmimportexcel.Click

        Dim StateImport As Boolean = False
        Dim msgshow As String = ""
        Dim cmdstring As String = ""

        If SizeDivert = "" Then Exit Sub

        Dim dt As DataTable
        With CType(Me.ogdmain.DataSource, DataTable)
            .AcceptChanges()

            dt = .Copy

        End With

        If dt.Select("FTSelect='1'").Length > 0 Then

            If dt.Select("FTSelect='1' AND (FTProvinceCode='' OR  FTShipPortCode='' OR FTBuyGrpCode='' OR  FTShipModeCode='') ").Length > 0 Then
                HI.MG.ShowMsg.mInfo("กรุณาทำการระบุข้อมูล รายละเอียด การ Divert ให้ครบ !!!", 210904562, Me.Text,, MessageBoxIcon.Warning)

            Else


                Dim Spls As New HI.TL.SplashScreen("Importing.. Data")


                Try
                    pListBreakDown = New List(Of JobBreakDown)

                    'Dim grpsuborder As List(Of String) = (dt.Select("FTSelect='1'", "SUBJOB").CopyToDataTable).AsEnumerable() _
                    '                                   .Select(Function(r) r.Field(Of String)("SUBJOB")) _
                    '                                   .Distinct() _
                    '                                   .ToList()

                    Dim StrDataSeq As String = ""
                    Dim OrderNo As String = ""
                    Dim SubOrderNo As String = ""
                    Dim SubOrderNoNew As String = ""
                    Dim BDColorway As String
                    Dim OBDColorway As String
                    Dim BDPOLine As String
                    Dim BDSize As String
                    Dim BDQty As Integer


                    'For Each StrSubFO As String In grpsuborder
                    '    StrDataSeq = ""
                    '    OrderNo = ""

                    '    BDColorway = ""
                    '    BDPOLine = ""
                    '    BDSize = ""
                    '    BDQty = 0

                    '    pListBreakDown.Clear()

                    '    For Each R As DataRow In dt.Select("FTSelect='1' AND  SUBJOB='" & HI.UL.ULF.rpQuoted(StrSubFO) & "'", "FNSeq")
                    '        OrderNo = R!FTOrderNo.ToString
                    '        BDColorway = R!ColorWay.ToString
                    '        BDPOLine = R!PONumber.ToString


                    '        For Each pSize As String In SizeDivert.Split(",")

                    '            If Val(R.Item(pSize).ToString) > 0 Then

                    '                BDSize = pSize
                    '                BDQty = Val(R.Item(pSize).ToString)


                    '                Dim mBD As New JobBreakDown


                    '                pListBreakDown.Add(mBD)

                    '            End If
                    '        Next




                    '    Next
                    'Next
                    StrDataSeq = ""
                    pListBreakDown.Clear()

                    Dim StateBool As Boolean
                    Dim _Qry As String = ""

                    Dim Colorway As String, ShipDate As String, ShipDateOrginal As String, ContinentId As Integer, CountryId As Integer, ProvinceId As Integer
                    Dim ShipModeIdOld As Integer
                    Dim ShipModeId As Integer, ShipPortId As Integer, PlantId As Integer, BuyGrpId As String, PORef As String, POTrading As String, Remark As String

                    For Each R As DataRow In dt.Select("FTSelect='1' ", "FNSeq")
                        StateBool = False

                        OrderNo = R!FTOrderNo.ToString
                        SubOrderNo = R!SUBJOB.ToString
                        BDColorway = R!ColorWay.ToString
                        OBDColorway = R!ColorWayRef.ToString.Trim()

                        If OBDColorway = "" Then
                            OBDColorway = BDColorway
                        End If


                        BDPOLine = R!POItem.ToString

                        ShipDate = R!GACDate.ToString
                        ShipDateOrginal = R!OGACDate.ToString
                        PORef = R!PONumber.ToString
                        POTrading = R!TradingCoPONumber.ToString
                        Remark = R!COMMENT.ToString

                        ContinentId = Val(R!FNHSysContinentId.ToString)
                        CountryId = Val(R!FNHSysCountryId.ToString)
                        ProvinceId = Val(R!FNHSysProvinceId.ToString)

                        ShipModeIdOld = Val(R!FNHSysShipModeIdOld.ToString)
                        ShipModeId = Val(R!FNHSysShipModeId.ToString)


                        ShipPortId = Val(R!FNHSysShipPortId.ToString)
                        PlantId = Val(R!FNHSysPlantId.ToString)
                        BuyGrpId = Val(R!FNHSysBuyGrpId.ToString)

                        SubOrderNoNew = ""

                        If ShipModeId = 0 Then
                            ShipModeId = ShipModeIdOld
                        End If


                        If R!Ping.ToString.Trim() = "I" Then

                            Dim StateDivert As Boolean = False
                            Dim DoborderDivert As String = ""
                            Dim Str2 As String = ""
                            Dim Str3 As String = ""
                            Dim Str4 As String = ""

                            Try
                                If SubOrderNo.Split("-").Length = 3 AndAlso Microsoft.VisualBasic.Left(SubOrderNo.Split("-")(SubOrderNo.Split("-").Length - 1), 1) = "D" Then
                                    StateDivert = True
                                    Str4 = SubOrderNo.Split("-")(SubOrderNo.Split("-").Length - 1)

                                    DoborderDivert = SubOrderNo.Replace("-" & Str4, "")
                                End If
                            Catch ex As Exception

                            End Try

                            If StateDivert Then


                                _Qry = "  UPDATE A SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                                _Qry &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                                _Qry &= vbCrLf & " , FDShipDate='" & HI.UL.ULDate.ConvertEnDB(ShipDate) & "' "
                                _Qry &= vbCrLf & " ,FNHSysContinentId=" & ContinentId & " "
                                _Qry &= vbCrLf & " ,FNHSysCountryId=" & CountryId & "  "
                                _Qry &= vbCrLf & " ,FNHSysProvinceId= " & ProvinceId & "  "
                                _Qry &= vbCrLf & " , FNHSysShipModeId= " & ShipModeId & "  "
                                _Qry &= vbCrLf & " ,FTRemark = FTRemark +'  ' + '" & HI.UL.ULF.rpQuoted(Remark) & "' "

                                _Qry &= vbCrLf & " ,FTPORef= '" & HI.UL.ULF.rpQuoted(PORef) & "' "
                                _Qry &= vbCrLf & ", FNHSysPlantId= " & PlantId & " "
                                _Qry &= vbCrLf & " ,FNHSysBuyGrpId= " & BuyGrpId & " "
                                _Qry &= vbCrLf & " ,FTPOTrading='" & HI.UL.ULF.rpQuoted(POTrading) & "' "
                                _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert  AS A  "
                                _Qry &= vbCrLf & " WHERE FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(DoborderDivert) & "'"
                                _Qry &= vbCrLf & " AND FNDivertSeq = " & Val(Str3) & ""

                                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN) Then

                                    If StrDataSeq = "" Then
                                        StrDataSeq = R!FNSeq.ToString
                                    Else
                                        StrDataSeq = StrDataSeq & "," & R!FNSeq.ToString
                                    End If

                                    R!FTSubOrderNoNew = SubOrderNo
                                End If

                            Else

                                _Qry = "  UPDATE A SET  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Qry &= vbCrLf & ", FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                                _Qry &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                                _Qry &= vbCrLf & " , FDShipDate='" & HI.UL.ULDate.ConvertEnDB(ShipDate) & "' "
                                _Qry &= vbCrLf & " ,FNHSysContinentId=" & ContinentId & " "
                                _Qry &= vbCrLf & " ,FNHSysCountryId=" & CountryId & "  "
                                _Qry &= vbCrLf & " ,FNHSysProvinceId= " & ProvinceId & "  "
                                _Qry &= vbCrLf & " , FNHSysShipModeId= " & ShipModeId & "  "
                                _Qry &= vbCrLf & " ,FTRemark = FTRemark +'  ' + '" & HI.UL.ULF.rpQuoted(Remark) & "' "
                                _Qry &= vbCrLf & "  ,FDShipDateOrginal='" & HI.UL.ULDate.ConvertEnDB(ShipDateOrginal) & "' "
                                _Qry &= vbCrLf & " ,FTPORef= '" & HI.UL.ULF.rpQuoted(PORef) & "' "
                                _Qry &= vbCrLf & ", FNHSysPlantId= " & PlantId & " "
                                _Qry &= vbCrLf & " ,FNHSysBuyGrpId= " & BuyGrpId & " "
                                _Qry &= vbCrLf & " ,FTPOTrading='" & HI.UL.ULF.rpQuoted(POTrading) & "' "
                                _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub  AS A  "
                                _Qry &= vbCrLf & " WHERE FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(SubOrderNo) & "'"


                                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN) Then

                                    If StrDataSeq = "" Then
                                        StrDataSeq = R!FNSeq.ToString
                                    Else
                                        StrDataSeq = StrDataSeq & "," & R!FNSeq.ToString
                                    End If

                                    R!FTSubOrderNoNew = SubOrderNo
                                End If

                            End If



                        Else
                            pListBreakDown.Clear()

                            For Each pSize As String In SizeDivert.Split(",")

                                If Val(R.Item(pSize).ToString) > 0 Then

                                    BDSize = pSize
                                    BDQty = Val(R.Item(pSize).ToString)

                                    Dim mBD As New JobBreakDown
                                    mBD.OldBreakDownColorWay = OBDColorway
                                    mBD.BreakDownColorWay = BDColorway
                                    mBD.POLine = BDPOLine
                                    mBD.BreakDownSize = BDSize
                                    mBD.BreakDownQty = BDQty

                                    pListBreakDown.Add(mBD)

                                End If

                            Next

                            If pListBreakDown.Count > 0 Then

                                If R!FTStateCutting.ToString = "1" Then

                                    StateBool = CreateNewDivert(Spls, OrderNo, SubOrderNo, BDColorway, ShipDate, ShipDateOrginal, ContinentId, CountryId, ProvinceId, ShipModeId, ShipPortId, PlantId, BuyGrpId, PORef, POTrading, Remark, pListBreakDown, SubOrderNoNew)

                                Else

                                    StateBool = CreateNewSubOrderNo(Spls, OrderNo, SubOrderNo, BDColorway, ShipDate, ShipDateOrginal, ContinentId, CountryId, ProvinceId, ShipModeId, ShipPortId, PlantId, BuyGrpId, PORef, POTrading, Remark, pListBreakDown, SubOrderNoNew)

                                End If

                                If StateBool Then

                                    If StrDataSeq = "" Then
                                        StrDataSeq = R!FNSeq.ToString
                                    Else
                                        StrDataSeq = StrDataSeq & "," & R!FNSeq.ToString
                                    End If

                                End If


                                R!FTSubOrderNoNew = SubOrderNoNew
                            End If
                        End If


                    Next

                    pListBreakDown.Clear()

                    Me.ogdmain.DataSource = dt.Copy()

                    Spls.Close()

                Catch ex As Exception

                    Spls.Close()
                End Try


            End If

        Else
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกรายการ ที่ต้องการทำการ Import Divert !!!", 210904561, Me.Text,, MessageBoxIcon.Warning)
        End If

    End Sub

    Private Sub exporter_CellValueConversionError(ByVal sender As Object, ByVal e As CellValueConversionErrorEventArgs)
        MessageBox.Show("Error In cell " & e.Cell.GetReferenceA1())
        e.DataTableValue = Nothing
        e.Action = DataTableExporterAction.Continue
    End Sub



    Private Sub FTFilePath_EditValueChanged(sender As Object, e As EventArgs) Handles FTFilePath.EditValueChanged

    End Sub



#Region "Drivert"

    Private Function CreateNewSubOrderNo(mSpls As HI.TL.SplashScreen, OrderNo As String, SubOrderNo As String, Colorway As String, ShipDate As String, ShipDateOrginal As String, ContinentId As Integer, CountryId As Integer, ProvinceId As Integer, ShipModeId As Integer, ShipPortId As Integer, PlantId As Integer, BuyGrpId As String, PORef As String, POTrading As String, Remark As String, ListBreakDown As List(Of JobBreakDown), ByRef SubOrderNoRef As String) As Boolean

        mSpls.UpdateInformation("Divert Order Breakdown Order " & OrderNo & "  SubOrder " & SubOrderNo & "   Color " & Colorway & " ...Please Wait")
        '  Dim _QryDelete As String = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        Dim _Qry As String = ""
        Dim _OrderNo As String = ""
        Dim _SubOrderNo As String = ""
        Dim _ColorWay As String = ""
        Dim _POLine As String = ""
        Dim _tmpOrderProd As String = ""
        Dim _oDtSub As System.Data.DataTable
        Dim _oDtMain As System.Data.DataTable

        Dim I As Integer = 0
        Dim DivertRec As Integer = ListBreakDown.Count

        Try

            Dim _KeyNew As String = ""
            Dim BDColorway As String = ""
            Dim OBDColorway As String = ""
            Dim BDPOLine As String
            Dim BDSize As String
            Dim BDQty As Integer

            _KeyNew = HI.Conn.SQLConn.GetField("EXEC  SP_GEN_CHARACTER_SubOrderNo '" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'", Conn.DB.DataBaseName.DB_MERCHAN, "")

            Dim dtBreakDown As DataTable
            Dim dt As DataTable

            _Qry = "    Select  FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNQuantity "
            _Qry &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(SubOrderNo) & "'"
            _Qry &= vbCrLf & "  AND ( "

            For Idx As Integer = 0 To ListBreakDown.Count - 1

                OBDColorway = ListBreakDown.Item(Idx).OldBreakDownColorWay
                BDColorway = ListBreakDown.Item(Idx).BreakDownColorWay
                BDSize = ListBreakDown.Item(Idx).BreakDownSize
                BDQty = ListBreakDown.Item(Idx).BreakDownQty

                If Idx = 0 Then
                    _Qry &= vbCrLf & "  ( FTColorway ='" & HI.UL.ULF.rpQuoted(OBDColorway) & "' AND FTSizeBreakDown ='" & HI.UL.ULF.rpQuoted(BDSize) & "' AND FNQuantity >=" & BDQty & " ) "
                Else
                    _Qry &= vbCrLf & "  OR ( FTColorway ='" & HI.UL.ULF.rpQuoted(OBDColorway) & "' AND FTSizeBreakDown ='" & HI.UL.ULF.rpQuoted(BDSize) & "' AND FNQuantity >=" & BDQty & " )  "
                End If

            Next

            _Qry &= vbCrLf & "   )"
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            If dt.Rows.Count <> DivertRec Then

                Return False
            End If

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub  ( "
            _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FDSubOrderDate, FTSubOrderBy, FDProDate, FDShipDate, FNHSysBuyId, FNHSysContinentId, FNHSysCountryId, FNHSysProvinceId,"
            _Qry &= vbCrLf & "    FNHSysShipModeId, FNHSysCurId, FNHSysGenderId, FNHSysUnitId, FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows, FTStateOther1, FTOther1Note, FTStateOther2,"
            _Qry &= vbCrLf & "    FTOther2Note, FTStateOther3, FTOther3Note1, FTRemark, FNHSysShipPortId, FDShipDateOrginal, FTCustRef, FNPackCartonSubType, FNPackPerCarton, FTSubOrderNoDivertRef, FTPORef,"
            _Qry &= vbCrLf & "   FNHSysPlantId, FNHSysBuyGrpId, FNOrderSetType, FNHSysFacProdTypeId, FTStateSewOnly, FTPOTrading"
            _Qry &= vbCrLf & "   ) "
            _Qry &= vbCrLf & " Select  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & "  , FTOrderNo, "
            _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(_KeyNew) & "' AS FTSubOrderNo "
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " AS FDSubOrderDate,"
            _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AS FTSubOrderBy, FDProDate,'" & HI.UL.ULDate.ConvertEnDB(ShipDate) & "' AS FDShipDate,"
            _Qry &= vbCrLf & " FNHSysBuyId,"
            _Qry &= vbCrLf & "  " & ContinentId & " AS  FNHSysContinentId,"
            _Qry &= vbCrLf & "  " & CountryId & " AS  FNHSysCountryId, "
            _Qry &= vbCrLf & "  " & ProvinceId & " AS   FNHSysProvinceId, "
            _Qry &= vbCrLf & "   " & ShipModeId & " AS  FNHSysShipModeId, FNHSysCurId, FNHSysGenderId, FNHSysUnitId, FTStateEmb, FTStatePrint, FTStateHeat, FTStateLaser, FTStateWindows, FTStateOther1, FTOther1Note, FTStateOther2,"
            _Qry &= vbCrLf & "   FTOther2Note, FTStateOther3, FTOther3Note1,"
            _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(Remark) & "' AS    FTRemark,"
            _Qry &= vbCrLf & "  FNHSysShipPortId,'" & HI.UL.ULDate.ConvertEnDB(ShipDateOrginal) & "' AS  FDShipDateOrginal, FTCustRef, FNPackCartonSubType, FNPackPerCarton,"
            _Qry &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(SubOrderNo) & "' AS  FTSubOrderNoDivertRef,"
            _Qry &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(PORef) & "' AS FTPORef,"
            _Qry &= vbCrLf & "   " & PlantId & " AS  FNHSysPlantId,"
            _Qry &= vbCrLf & "   " & BuyGrpId & " AS  FNHSysBuyGrpId, FNOrderSetType, FNHSysFacProdTypeId, FTStateSewOnly,"
            _Qry &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(POTrading) & "' AS FTPOTrading"
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(SubOrderNo) & "'"


            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If



            _Qry = "  "

            For Idx As Integer = 0 To ListBreakDown.Count - 1

                OBDColorway = ListBreakDown.Item(Idx).OldBreakDownColorWay
                BDColorway = ListBreakDown.Item(Idx).BreakDownColorWay
                BDPOLine = ListBreakDown.Item(Idx).POLine
                BDSize = ListBreakDown.Item(Idx).BreakDownSize
                BDQty = ListBreakDown.Item(Idx).BreakDownQty

                _Qry &= vbCrLf & " UPDATE A SET  FNQuantity = FNQuantity -" & BDQty & "   "
                _Qry &= vbCrLf & ", FNAmt = (FNAmt - (Convert(numeric(18,2), FNPrice * " & BDQty & ")))"
                _Qry &= vbCrLf & ", FNGrandQuantity = (FNGrandQuantity -" & BDQty & ") "
                _Qry &= vbCrLf & ", FNGrandAmnt = (FNGrandAmnt - ( (Convert(numeric(18,2),FNPrice * " & BDQty & "))))"
                _Qry &= vbCrLf & "    From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS A "
                _Qry &= vbCrLf & " WHERE FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(SubOrderNo) & "'"
                _Qry &= vbCrLf & "  AND  FTColorway ='" & HI.UL.ULF.rpQuoted(OBDColorway) & "' AND FTSizeBreakDown ='" & HI.UL.ULF.rpQuoted(BDSize) & "' AND FNQuantity >=" & BDQty & "  "

                _Qry &= vbCrLf & " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown ("
                _Qry &= vbCrLf & "   FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNPrice, FNQuantity, FNAmt, FNHSysMatColorId, "
                _Qry &= vbCrLf & "   FNHSysMatSizeId, FNExtraQty, FNQuantityExtra, FNGrandQuantity, FNAmntExtra, FNGrandAmnt, FNGarmentQtyTest, FNAmntQtyTest, FNPriceOrg, FTNikePOLineItem, FNCMDisPer, FNCMDisAmt,"
                _Qry &= vbCrLf & "    FNNetPrice, FTStateHold, FNOperateFee, FNOperateFeeAmt, FNNetFOB, FNNetPriceOperateFee, FNNetPriceOperateFeeAmt, FNNetNetPrice, FTStateImportNetPrice, FTImportNetPriceBy,"
                _Qry &= vbCrLf & "    FDImportNetPriceDate, FTImportNetPriceTime, FNExternalQtyTest, FNExternalAmntQtyTest"
                _Qry &= vbCrLf & " )"
                _Qry &= vbCrLf & " Select TOP 1  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & "  , FTOrderNo, "
                _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(_KeyNew) & "' AS  FTSubOrderNo,'" & HI.UL.ULF.rpQuoted(BDColorway) & "' AS  FTColorway, FTSizeBreakDown, FNPrice,"
                _Qry &= vbCrLf & " " & BDQty & "  AS FNQuantity,"
                _Qry &= vbCrLf & "  (Convert(numeric(18,2),FNPrice * " & BDQty & ")) AS FNAmt,"
                _Qry &= vbCrLf & "FNHSysMatColorId, "
                _Qry &= vbCrLf & "  FNHSysMatSizeId,0 AS  FNExtraQty,0 AS  FNQuantityExtra,"
                _Qry &= vbCrLf & " " & BDQty & "  AS FNGrandQuantity,"
                _Qry &= vbCrLf & " 0 AS FNAmntExtra,"
                _Qry &= vbCrLf & "  (Convert(numeric(18,2),FNPrice * " & BDQty & ")) AS FNGrandAmnt,"
                _Qry &= vbCrLf & "0 As FNGarmentQtyTest,0 AS  FNAmntQtyTest, FNPriceOrg,"
                _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(BDPOLine) & "' AS FTNikePOLineItem,"
                _Qry &= vbCrLf & "FNCMDisPer, FNCMDisAmt,"
                _Qry &= vbCrLf & "  FNNetPrice,'0' FTStateHold, FNOperateFee, FNOperateFeeAmt, FNNetFOB, FNNetPriceOperateFee, FNNetPriceOperateFeeAmt, FNNetNetPrice, FTStateImportNetPrice, FTImportNetPriceBy, "
                _Qry &= vbCrLf & "    FDImportNetPriceDate, FTImportNetPriceTime, 0 As  FNExternalQtyTest, 0 AS  FNExternalAmntQtyTest "
                _Qry &= vbCrLf & "    From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS A WITH(NOLOCK) "
                _Qry &= vbCrLf & " WHERE FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(SubOrderNo) & "'"
                _Qry &= vbCrLf & "  AND  FTColorway ='" & HI.UL.ULF.rpQuoted(OBDColorway) & "' AND FTSizeBreakDown ='" & HI.UL.ULF.rpQuoted(BDSize) & "'   "

            Next

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If



            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Component"
            _Qry &= "(FTInsUser, FDInsDate, FTInsTime,FTOrderNo, FTSubOrderNo, FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp, FNSeq,FNDataSeq)"
            _Qry &= vbCrLf & "Select N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & ",'" & _KeyNew & "'"
            _Qry &= vbCrLf & ", FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp, FNSeq,FNDataSeq"
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Component WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(SubOrderNo.ToString) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            End If

            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Sew"
            _Qry &= "(FTInsUser, FDInsDate, FTInsTime , FTOrderNo, FTSubOrderNo, FNSewSeq, FTSewDescription, FTSewNote, FTImage)"
            _Qry &= vbCrLf & "Select N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & ",'" & _KeyNew & "'"
            _Qry &= vbCrLf & ",FNSewSeq, FTSewDescription, FTSewNote, FTImage"
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Sew WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(SubOrderNo.ToString) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            End If

            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Pack"
            _Qry &= "( FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNPackSeq, FTPackDescription, FTPackNote, FTImage)"
            _Qry &= vbCrLf & "Select N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & ",'" & _KeyNew & "'"
            _Qry &= vbCrLf & ", FNPackSeq, FTPackDescription, FTPackNote, FTImage"
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Pack WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(SubOrderNo.ToString) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            End If

            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_SizeSpec"
            _Qry &= "( FTInsUser, FDInsDate, FTInsTime,   FTOrderNo, FTSubOrderNo, FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension, FTTolerant,FNHSysMeasId)"
            _Qry &= vbCrLf & "Select N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & ",'" & _KeyNew & "'"
            _Qry &= vbCrLf & ",  FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension, FTTolerant,FNHSysMeasId"
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_SizeSpec WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(SubOrderNo.ToString) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            End If

            SubOrderNoRef = _KeyNew

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            ' HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)

            Return False
        End Try
        ' HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)

        Return True
    End Function


    Private Function CreateNewDivert(mSpls As HI.TL.SplashScreen, OrderNo As String, SubOrderNo As String, Colorway As String, ShipDate As String, ShipDateOrginal As String, ContinentId As Integer, CountryId As Integer, ProvinceId As Integer, ShipModeId As Integer, ShipPortId As Integer, PlantId As Integer, BuyGrpId As String, PORef As String, POTrading As String, Remark As String, ListBreakDown As List(Of JobBreakDown), ByRef SubOrderNoRef As String) As Boolean

        mSpls.UpdateInformation("Divert Order Breakdown Order " & OrderNo & "  SubOrder " & SubOrderNo & "   Color " & Colorway & " ...Please Wait")
        '  Dim _QryDelete As String = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTempCreateJobProd WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        Dim _Qry As String = ""
        Dim _OrderNo As String = ""
        Dim _SubOrderNo As String = ""
        Dim _ColorWay As String = ""
        Dim _POLine As String = ""
        Dim _tmpOrderProd As String = ""
        Dim _oDtSub As System.Data.DataTable
        Dim _oDtMain As System.Data.DataTable
        Dim Maxleng As Integer
        Dim I As Integer = 0
        Dim DivertRec As Integer = ListBreakDown.Count

        Try

            Dim _KeyNew As String = ""
            Dim BDColorway As String = ""
            Dim OBDColorway As String = ""
            Dim BDPOLine As String
            Dim BDSize As String
            Dim BDQty As Integer

            _KeyNew = SubOrderNo

            Dim dtBreakDown As DataTable
            Dim dt As DataTable

            _Qry = "    Select  FTOrderNo, FTSubOrderNo, FTColorway, FTSizeBreakDown, FNQuantity "
            _Qry &= vbCrLf & "    From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(SubOrderNo) & "'"
            _Qry &= vbCrLf & "  AND ( "

            For Idx As Integer = 0 To ListBreakDown.Count - 1

                OBDColorway = ListBreakDown.Item(Idx).OldBreakDownColorWay
                BDColorway = ListBreakDown.Item(Idx).BreakDownColorWay
                BDSize = ListBreakDown.Item(Idx).BreakDownSize
                BDQty = ListBreakDown.Item(Idx).BreakDownQty


                If Idx = 0 Then


                    _Qry &= vbCrLf & "  ( FTColorway ='" & HI.UL.ULF.rpQuoted(OBDColorway) & "' AND FTSizeBreakDown ='" & HI.UL.ULF.rpQuoted(BDSize) & "' AND FNQuantity >=" & BDQty & " ) "

                Else

                    _Qry &= vbCrLf & "  OR ( FTColorway ='" & HI.UL.ULF.rpQuoted(OBDColorway) & "' AND FTSizeBreakDown ='" & HI.UL.ULF.rpQuoted(BDSize) & "' AND FNQuantity >=" & BDQty & " )  "

                End If



            Next
            _Qry &= vbCrLf & "   )"
            dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            If dt.Rows.Count <> DivertRec Then

                Return False
            End If

            _Qry = "select top 1 Max(FNDivertSeq) AS MaxlengSeq FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub_Divert"
            _Qry &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo) & "'"
            _Qry &= vbCrLf & "  AND  FTSubOrderNo='" & HI.UL.ULF.rpQuoted(SubOrderNo) & "'"

            Maxleng = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, HI.Conn.DB.DataBaseName.DB_MERCHAN, "0"))) + 1


            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert  ( "
            _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNDivertSeq,  FDShipDate,  FNHSysContinentId, FNHSysCountryId, FNHSysProvinceId,"
            _Qry &= vbCrLf & "    FNHSysShipModeId,  FTRemark, FNHSysShipPortId,  FTCustRef, FNPackCartonSubType, FNPackPerCarton,  FTPORef,"
            _Qry &= vbCrLf & "   FNHSysPlantId, FNHSysBuyGrpId, FNOrderSetType,  FTPOTrading"
            _Qry &= vbCrLf & "   ) "
            _Qry &= vbCrLf & " Select  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & "  , FTOrderNo, "
            _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(_KeyNew) & "' AS FTSubOrderNo, "
            _Qry &= vbCrLf & "" & Maxleng & " AS FNDivertSeq,"
            _Qry &= vbCrLf & " '" & HI.UL.ULDate.ConvertEnDB(ShipDate) & "' AS FDShipDate,"
            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & "  " & ContinentId & " AS  FNHSysContinentId,"
            _Qry &= vbCrLf & "  " & CountryId & " AS  FNHSysCountryId, "
            _Qry &= vbCrLf & "  " & ProvinceId & " AS   FNHSysProvinceId, "
            _Qry &= vbCrLf & "   " & ShipModeId & " AS  FNHSysShipModeId, "
            _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(Remark) & "' AS    FTRemark,"
            _Qry &= vbCrLf & "  FNHSysShipPortId, FTCustRef, FNPackCartonSubType, FNPackPerCarton,"

            _Qry &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(PORef) & "' AS FTPORef,"
            _Qry &= vbCrLf & "   " & PlantId & " AS  FNHSysPlantId,"
            _Qry &= vbCrLf & "   " & BuyGrpId & " AS  FNHSysBuyGrpId, FNOrderSetType,"
            _Qry &= vbCrLf & "  '" & HI.UL.ULF.rpQuoted(POTrading) & "' AS FTPOTrading"
            _Qry &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(SubOrderNo) & "'"


            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If



            _Qry = "  "

            For Idx As Integer = 0 To ListBreakDown.Count - 1

                OBDColorway = ListBreakDown.Item(Idx).OldBreakDownColorWay
                BDColorway = ListBreakDown.Item(Idx).BreakDownColorWay
                BDPOLine = ListBreakDown.Item(Idx).POLine
                BDSize = ListBreakDown.Item(Idx).BreakDownSize
                BDQty = ListBreakDown.Item(Idx).BreakDownQty


                _Qry &= vbCrLf & " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_BreakDown ("
                _Qry &= vbCrLf & "   FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo, FNDivertSeq,FTColorway, FTSizeBreakDown, FNPrice, FNQuantity,  "
                _Qry &= vbCrLf & "   FNPriceOrg, FTNikePOLineItem, FNCMDisPer, FNCMDisAmt,"
                _Qry &= vbCrLf & "    FNNetPrice, FTStateHold, FNOperateFee, FNOperateFeeAmt, FNNetFOB, FNNetPriceOperateFee, FNNetPriceOperateFeeAmt, FNNetNetPrice, FTStateImportNetPrice, FTImportNetPriceBy,"
                _Qry &= vbCrLf & "    FDImportNetPriceDate, FTImportNetPriceTime,FTColorwayNew"
                _Qry &= vbCrLf & " )"
                _Qry &= vbCrLf & " Select TOP 1  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & "  , FTOrderNo, "
                _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(_KeyNew) & "' AS  FTSubOrderNo," & Maxleng & " AS FNDivertSeq ,FTColorway, FTSizeBreakDown, FNPrice,"
                _Qry &= vbCrLf & " " & BDQty & "  AS FNQuantity,"
                _Qry &= vbCrLf & " FNPriceOrg,"
                _Qry &= vbCrLf & " '" & HI.UL.ULF.rpQuoted(BDPOLine) & "' AS FTNikePOLineItem,"
                _Qry &= vbCrLf & "FNCMDisPer, FNCMDisAmt,"
                _Qry &= vbCrLf & "  FNNetPrice,'0' FTStateHold, FNOperateFee, FNOperateFeeAmt, FNNetFOB, FNNetPriceOperateFee, FNNetPriceOperateFeeAmt, FNNetNetPrice, FTStateImportNetPrice, FTImportNetPriceBy, "
                _Qry &= vbCrLf & "    FDImportNetPriceDate, FTImportNetPriceTime,'" & HI.UL.ULF.rpQuoted(BDColorway) & "' "
                _Qry &= vbCrLf & "    From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS A  WITH(NOLOCK)"
                _Qry &= vbCrLf & " WHERE FTSubOrderNo ='" & HI.UL.ULF.rpQuoted(SubOrderNo) & "'"
                _Qry &= vbCrLf & "  AND  FTColorway ='" & HI.UL.ULF.rpQuoted(OBDColorway) & "' AND FTSizeBreakDown ='" & HI.UL.ULF.rpQuoted(BDSize) & "'   "

            Next

            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False
            End If



            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Component"
            _Qry &= "(FTInsUser, FDInsDate, FTInsTime,FTOrderNo, FTSubOrderNo, FNDivertSeq,FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp, FNSeq,FNDataSeq)"
            _Qry &= vbCrLf & "Select N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & ",'" & _KeyNew & "'"
            _Qry &= vbCrLf & "," & Maxleng & " AS FNDivertSeq"
            _Qry &= vbCrLf & ", FNHSysMerMatId, FNPart, FTComponent, FTRemark, FNConSmp, FNSeq,FNDataSeq"
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Component WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(SubOrderNo.ToString) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            End If

            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Sew"
            _Qry &= "(FTInsUser, FDInsDate, FTInsTime , FTOrderNo, FTSubOrderNo, FNDivertSeq,FNSewSeq, FTSewDescription, FTSewNote, FTImage)"
            _Qry &= vbCrLf & "Select N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & ",'" & _KeyNew & "'"
            _Qry &= vbCrLf & "," & Maxleng & " AS FNDivertSeq"
            _Qry &= vbCrLf & ",FNSewSeq, FTSewDescription, FTSewNote, FTImage"
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Sew WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(SubOrderNo.ToString) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            End If

            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_Pack"
            _Qry &= "( FTInsUser, FDInsDate, FTInsTime, FTOrderNo, FTSubOrderNo,FNDivertSeq, FNPackSeq, FTPackDescription, FTPackNote, FTImage)"
            _Qry &= vbCrLf & "Select N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & ",'" & _KeyNew & "'"
            _Qry &= vbCrLf & "," & Maxleng & " AS FNDivertSeq"
            _Qry &= vbCrLf & ", FNPackSeq, FTPackDescription, FTPackNote, FTImage"
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Pack WITH(NOLOCK) "
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(SubOrderNo.ToString) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            End If

            _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_Divert_SizeSpec"
            _Qry &= "( FTInsUser, FDInsDate, FTInsTime,   FTOrderNo, FTSubOrderNo,FNDivertSeq, FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension, FTTolerant,FNHSysMeasId)"
            _Qry &= vbCrLf & "Select N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & ",'" & _KeyNew & "'"
            _Qry &= vbCrLf & "," & Maxleng & " AS FNDivertSeq"
            _Qry &= vbCrLf & ",  FNSeq, FNHSysMatSizeId, FTSizeSpecDesc, FTSizeSpecExtension, FTTolerant,FNHSysMeasId"
            _Qry &= vbCrLf & "From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_SizeSpec WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(OrderNo.ToString) & "'"
            _Qry &= vbCrLf & " And FTSubOrderNo='" & HI.UL.ULF.rpQuoted(SubOrderNo.ToString) & "'"
            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

            End If

            SubOrderNoRef = _KeyNew & "-D" & Maxleng.ToString

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            ' HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)

            Return False
        End Try
        ' HI.Conn.SQLConn.ExecuteOnly(_QryDelete, Conn.DB.DataBaseName.DB_PROD)

        Return True
    End Function



#End Region


    Private Sub ClearColumnGrid()

        Try
            With ogvmain
                .BeginInit()

                'For Each c As GridColumn In .Columns
                '    Select Case c.FieldName
                '        Case "Ping", "Vendor", "FC", "SUBJOB" _
                '                , "COMMENT", "PONumber", "TradingCoPONumber", "POItem", "Documentdate", "OGACDate", "GACDate", "AFSCategory", "PROGRAM" _
                '                , "style", "Material", "Silhouette", "PlanningSeason", "Year", "PurchasingGroup" _
                '                , "Plant", "Customer", "DestinationCountry", "BuyGroup", "BuyGroup1", "Mode", "ColorWay"

                '        Case Else
                '            .Columns.Remove(c)
                '    End Select

                'Next


                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case .Columns(I).FieldName
                        Case "Ping", "Vendor", "FC", "SUBJOB", "FNSeq", "FTOrderNo", "FTSubOrderNoNew" _
                                , "COMMENT", "PONumber", "TradingCoPONumber", "POItem", "Documentdate", "OGACDate", "GACDate", "AFSCategory", "PROGRAM" _
                                , "style", "Material", "Silhouette", "PlanningSeason", "Year", "PurchasingGroup" _
                                , "Plant", "Customer", "DestinationCountry", "BuyGroup", "BuyGroup1", "Mode", "ColorWay", "FTCmpCode", "FTSelect", "FTStateCutting", "FTStateTagSuit", "FTPartName" _
                                , "FNHSysProvinceId", "FNHSysCountryId", "FNHSysContinentId", "FNHSysShipPortId", "FTContinentCode", "FTContinentNameEN", "FTCountryCode", "FTCountryNameEN", "FTProvinceCode", "FTProvinceNameEN", "RefMaterial" _
                                , "FTShipPortCode", "FTShipPortNameEN", "FNHSysBuyGrpId", "FTBuyGrpCode", "FTBuyGrpNameEN", "FNHSysShipModeId", "FTShipModeCode", "FTShipModeNameEN", "FNHSysPlantId", "FTPlantCode", "FTPlantNameEN", "FTStateDivert", "ColorWayRef", "FTShipModeCodeOld", "FNHSysShipModeIdOld"

                        Case Else

                            Dim FName As String = .Columns(I).FieldName

                            .Columns.Remove(.Columns(I))
                    End Select

                Next


                .EndInit()

            End With
        Catch ex As Exception

        End Try

    End Sub
    Private Sub ColumnDivert(dt As DataTable)

        SizeDivert = ""

        Dim StrCol As String = dt.Rows(0)!FTColumn.ToString
        If StrCol <> "" Then
            SizeDivert = StrCol
        End If

    End Sub


    Private Sub SetNewColumn(dt As DataTable)
        Try
            Dim StrCol As String = dt.Rows(0)!FTColumn.ToString

            With Me.ogvmain
                .BeginInit()



                'For I As Integer = .Columns.Count - 1 To 0 Step -1
                '    Select Case Microsoft.VisualBasic.Left(.Columns(I).Name.ToString, 4).ToUpper
                '        Case "CFIX".ToUpper

                '        Case Else

                '            Dim FName As String = .Columns(I).FieldName

                '            .Columns.Remove(.Columns(I))
                '    End Select

                'Next


                If StrCol <> "" Then

                    For Each R As String In StrCol.Split(",")


                        Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                        With ColG

                            Dim FName As String = R.Replace("[", "").Replace("]", "")

                            .FieldName = FName
                            .Name = "Size" & R.Replace(" ", "_").Replace("[", "").Replace("]", "")
                            .Caption = FName
                            .Visible = True

                            .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                            .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains


                            .DisplayFormat.FormatString = "{0:n0}"

                            With .OptionsColumn
                                .AllowMove = False
                                .AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                .AllowSort = DevExpress.Utils.DefaultBoolean.False

                                .AllowEdit = False
                                .ReadOnly = True
                            End With

                            .Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                            .SummaryItem.DisplayFormat = "{0:n0}"
                            .Width = 50

                        End With

                        .Columns.Add(ColG)
                    Next

                End If



                .EndInit()
            End With




        Catch ex As Exception

        End Try



    End Sub
    Private Sub SetFilerColumn()
        Try

            For Each c As GridColumn In ogvmain.Columns

                c.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains
                c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                c.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList

            Next



        Catch ex As Exception
        End Try

    End Sub

    Private Sub LoadProvince()
        Dim cmdstring As String = ""

        cmdstring = " SELECT     A.FNHSysProvinceId, A.FNHSysCountryId,  B.FNHSysContinentId,A.FTProvinceCode, A.FTProvinceNameEN  "
        cmdstring &= vbCrLf & "     , B.FTCountryCode, B.FTCountryNameEN, C.FTContinentCode, C.FTContinentNameEN "
        cmdstring &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCMMProvince AS A WITH(NOLOCK) INNER JOIN "
        cmdstring &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCountry AS B  WITH(NOLOCK)  ON A.FNHSysCountryId = B.FNHSysCountryId INNER JOIN "
        cmdstring &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMContinent AS C  WITH(NOLOCK)   ON B.FNHSysContinentId = C.FNHSysContinentId "
        cmdstring &= vbCrLf & "  WHERE A.FTStateActive ='1' "
        cmdstring &= vbCrLf & " ORDER BY A.FTProvinceCode,B.FTCountryCode,C.FTContinentCode"

        Dim dt As DataTable

        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

        With ItemGridLookUpEditProvince

            .PopupFormSize = New Size(800, 500)
            .AutoHeight = False
            .DataSource = dt.Copy
        End With

        dt.Dispose()

        Call LoadByGrp()
        Call LoadPlant()
        Call LoadMode()
        Call LoadShipport()

    End Sub


    Private Sub LoadByGrp()
        Dim cmdstring As String = ""

        cmdstring = " SELECT     A.FNHSysBuyGrpId,A.FTBuyGrpCode, A.FTBuyGrpNameEN  "
        cmdstring &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuyGrp AS A WITH(NOLOCK) "
        cmdstring &= vbCrLf & "  WHERE A.FTStateActive ='1' "
        cmdstring &= vbCrLf & " ORDER BY A.FTBuyGrpCode"

        Dim dt As DataTable

        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

        With ItemGridLookUpEditFTBuyGrpCode

            .PopupFormSize = New Size(250, 500)
            .AutoHeight = False
            .DataSource = dt.Copy
        End With

        dt.Dispose()
    End Sub

    Private Sub LoadPlant()
        Dim cmdstring As String = ""

        cmdstring = " SELECT     A.FNHSysPlantId,A.FTPlantCode, A.FTPlantNameEN  "
        cmdstring &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPlant AS A WITH(NOLOCK) "
        cmdstring &= vbCrLf & "  WHERE A.FTStateActive ='1' "
        cmdstring &= vbCrLf & " ORDER BY A.FTPlantCode"

        Dim dt As DataTable

        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

        With ItemGridLookUpEditFTPlantCode
            .PopupFormSize = New Size(250, 500)
            .AutoHeight = False
            .DataSource = dt.Copy
        End With


        dt.Dispose()
    End Sub

    Private Sub LoadMode()
        Dim cmdstring As String = ""

        cmdstring = " SELECT     A.FNHSysShipModeId,A.FTShipModeCode, A.FTShipModeNameEN  "
        cmdstring &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipMode AS A WITH(NOLOCK) "
        cmdstring &= vbCrLf & "  WHERE A.FTStateActive ='1' "
        cmdstring &= vbCrLf & " ORDER BY A.FTShipModeCode"

        Dim dt As DataTable

        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

        With ItemGridLookUpEditShipMode

            .PopupFormSize = New Size(250, 500)
            .AutoHeight = False
            .DataSource = dt.Copy
        End With


        dt.Dispose()
    End Sub


    Private Sub LoadShipport()
        Dim cmdstring As String = ""

        cmdstring = " SELECT     A.FNHSysShipPortId,A.FTShipPortCode, A.FTShipPortNameEN  "
        cmdstring &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMShipPort AS A WITH(NOLOCK) "
        cmdstring &= vbCrLf & "  WHERE A.FTStateActive ='1' "
        cmdstring &= vbCrLf & " ORDER BY A.FTShipPortCode"

        Dim dt As DataTable

        dt = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)

        With ItemGridLookUpEditFTShipPortCode

            .PopupFormSize = New Size(250, 500)
            .AutoHeight = False
            .DataSource = dt.Copy
        End With


        dt.Dispose()
    End Sub

    Private Sub ItemGridLookUpEditProvince_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ItemGridLookUpEditProvince.EditValueChanging
        Try

            With Me.ogvmain
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)


                .SetFocusedRowCellValue("FNHSysProvinceId", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysProvinceId").ToString)
                .SetFocusedRowCellValue("FNHSysCountryId", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysCountryId").ToString)
                .SetFocusedRowCellValue("FNHSysContinentId", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysContinentId").ToString)



                .SetFocusedRowCellValue("FTProvinceNameEN", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTProvinceNameEN").ToString)
                .SetFocusedRowCellValue("FTContinentCode", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTContinentCode").ToString)
                .SetFocusedRowCellValue("FTContinentNameEN", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTContinentNameEN").ToString)
                .SetFocusedRowCellValue("FTCountryCode", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTCountryCode").ToString)
                .SetFocusedRowCellValue("FTCountryNameEN", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTCountryNameEN").ToString)

            End With

            CType(Me.ogdmain.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ItemGridLookUpEditFTBuyGrpCode_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ItemGridLookUpEditFTBuyGrpCode.EditValueChanging
        Try

            With Me.ogvmain
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)


                .SetFocusedRowCellValue("FNHSysBuyGrpId", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysBuyGrpId").ToString)
                .SetFocusedRowCellValue("FTBuyGrpNameEN", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTBuyGrpNameEN").ToString)


            End With

            CType(Me.ogdmain.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ItemGridLookUpEditFTPlantCode_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ItemGridLookUpEditFTPlantCode.EditValueChanging
        Try

            With Me.ogvmain
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)


                .SetFocusedRowCellValue("FNHSysPlantId", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysPlantId").ToString)
                .SetFocusedRowCellValue("FTPlantNameEN", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTPlantNameEN").ToString)


            End With

            CType(Me.ogdmain.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ItemGridLookUpEditShipMode_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ItemGridLookUpEditShipMode.EditValueChanging

        Try

            With Me.ogvmain
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)


                .SetFocusedRowCellValue("FNHSysShipModeId", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysShipModeId").ToString)
                .SetFocusedRowCellValue("FTShipModeNameEN", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTShipModeNameEN").ToString)


            End With

            CType(Me.ogdmain.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub RepFTSelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepFTSelect.EditValueChanging

        Try

            With Me.ogvmain
                If .FocusedRowHandle < 0 Then Exit Sub


                If .GetFocusedRowCellValue("Ping").ToString = "I" Then
                    e.Cancel = False
                Else
                    If Val(.GetFocusedRowCellValue("Total").ToString) > Val(.GetFocusedRowCellValue("WSTotal").ToString) Then
                        e.Cancel = True
                    Else
                        e.Cancel = False
                    End If
                End If

            End With

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ItemGridLookUpEditFTShipPortCode_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles ItemGridLookUpEditFTShipPortCode.EditValueChanging

        Try

            With Me.ogvmain
                If .FocusedRowHandle < 0 Then Exit Sub

                Dim obj As DevExpress.XtraEditors.GridLookUpEdit = DirectCast(sender, DevExpress.XtraEditors.GridLookUpEdit)


                .SetFocusedRowCellValue("FNHSysShipPortId", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FNHSysShipPortId").ToString)
                .SetFocusedRowCellValue("FTShipPortNameEN", obj.Properties.View.GetRowCellValue(obj.Properties.View.FocusedRowHandle, "FTShipPortNameEN").ToString)


            End With

            CType(Me.ogdmain.DataSource, DataTable).AcceptChanges()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvmain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ogvmain.KeyPress
        e.Handled = True
    End Sub

    Private Sub ogdmain_ProcessGridKey(sender As Object, e As KeyEventArgs) Handles ogdmain.ProcessGridKey

    End Sub

    Private Sub ogdmain_EditorKeyPress(sender As Object, e As KeyPressEventArgs) Handles ogdmain.EditorKeyPress
        e.Handled = True
    End Sub

    Private Sub ogvmain_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvmain.RowStyle
        Try
            With Me.ogvmain

                Try



                    If .GetRowCellValue(e.RowHandle, "FTSubOrderNoNew").ToString <> "" Then
                        e.Appearance.ForeColor = System.Drawing.Color.Blue
                    End If



                Catch ex As Exception
                End Try

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Class JobBreakDown


        Private _OldBreakDownColorWay As String
        Public Property OldBreakDownColorWay() As String
            Get
                Return _OldBreakDownColorWay
            End Get
            Set(ByVal value As String)
                _OldBreakDownColorWay = value
            End Set
        End Property

        Private _BreakDownColorWay As String
        Public Property BreakDownColorWay() As String
            Get
                Return _BreakDownColorWay
            End Get
            Set(ByVal value As String)
                _BreakDownColorWay = value
            End Set
        End Property

        Private _POLine As String
        Public Property POLine() As String
            Get
                Return _POLine
            End Get
            Set(ByVal value As String)
                _POLine = value
            End Set
        End Property

        Private _BreakDownSize As String
        Public Property BreakDownSize() As String
            Get
                Return _BreakDownSize
            End Get
            Set(ByVal value As String)
                _BreakDownSize = value
            End Set
        End Property

        Private _BreakDownQty As Integer
        Public Property BreakDownQty() As Integer
            Get
                Return _BreakDownQty
            End Get
            Set(ByVal value As Integer)
                _BreakDownQty = value
            End Set
        End Property

    End Class

End Class


