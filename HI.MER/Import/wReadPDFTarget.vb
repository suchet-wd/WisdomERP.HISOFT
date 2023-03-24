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

Public Class wReadPDFTarget

    Private Function ExtractTextFromPDF(ByVal filePath As String) As String
        Dim documentText As String = ""
        Try

            Using documentStream As New FileStream(filePath, FileMode.Open, FileAccess.Read)
                Using documentProcessor As New PdfDocumentProcessor()
                    documentProcessor.LoadDocument(documentStream)
                    documentText = documentProcessor.Text
                End Using
            End Using

        Catch
        End Try
        Return documentText
    End Function

    Private Function CreateDatattable() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("ITEM", GetType(String))
        dt.Columns.Add("QUANTITY", GetType(Double))
        dt.Columns.Add("UNITPRICE", GetType(Double))
        dt.Columns.Add("BUYERNUMBER", GetType(String))
        dt.Columns.Add("GTINUCC", GetType(String))
        dt.Columns.Add("PRODUCT", GetType(String))
        dt.Columns.Add("VENDERCOLOR", GetType(String))
        dt.Columns.Add("VENDERSIZE", GetType(String))
        dt.Columns.Add("UNITPRICE2", GetType(Double))
        dt.Columns.Add("PACK", GetType(String))
        dt.Columns.Add("INNERPACK", GetType(String))
        dt.Columns.Add("COMMODITYCODE", GetType(String))
        dt.Columns.Add("COUNTRY", GetType(String))
        dt.Columns.Add("VENDERSTYLE", GetType(String))
        dt.Columns.Add("BUYERITEMNO", GetType(String))
        Return dt
    End Function

    Private Function CreateDatattablePOSet() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("ITEM", GetType(Integer))
        dt.Columns.Add("QUANTITY", GetType(Double))
        dt.Columns.Add("UNITPRICE", GetType(Double))
        dt.Columns.Add("BUYERNUMBER", GetType(String))
        dt.Columns.Add("GTINUCC", GetType(String))

        dt.Columns.Add("ITEMASSIGNED", GetType(Integer))
        dt.Columns.Add("FNINCLUDED", GetType(Integer))
        dt.Columns.Add("UNITPRICE2", GetType(Double))
        dt.Columns.Add("SUBBUYERNUMBER", GetType(String))
        dt.Columns.Add("VENDERSTYLE", GetType(String))
        dt.Columns.Add("GTINUCC2", GetType(String))
        dt.Columns.Add("PRODUCT", GetType(String))
        dt.Columns.Add("COMMODITYCODE", GetType(String))
        dt.Columns.Add("COUNTRY", GetType(String))

        dt.Columns.Add("VENDERCOLOR", GetType(String))
        dt.Columns.Add("VENDERSIZE", GetType(String))

        dt.Columns.Add("FNPACK", GetType(Double))
        dt.Columns.Add("FTStatePriceDiff", GetType(String))

        Return dt
    End Function

    Private Sub LoadDataPOPDFTargetNormal(Str As String)

        Try
            Dim PageNo As Integer = 1
            Dim RowNo As Integer = 1
            Dim dtMain As DataTable = Me.CreateDatattable

            Dim dtPage As New DataTable("Page")
            dtPage.Columns.Add("FNPage", GetType(Integer))

            Dim dtsource As New DataTable("Data")
            dtsource.Columns.Add("FNPage", GetType(Integer))
            dtsource.Columns.Add("FNRow", GetType(Integer))
            dtsource.Columns.Add("FTData", GetType(String))

            Using reader As New StringReader(Str)
                Dim line As String = ""
                line = reader.ReadLine()

                While (line IsNot Nothing)

                    If ((line.Trim)).ToUpper = "ITEM QUANTITY UNIT OF MEASURE UNIT PRICE".ToUpper Then
                        PageNo = PageNo + 1
                        RowNo = 0
                    End If

                    dtsource.Rows.Add(PageNo, RowNo, line)

                    If dtPage.Select("FNPage=" & PageNo & "").Length <= 0 Then
                        dtPage.Rows.Add(PageNo)
                    End If
                    RowNo = RowNo + 1
                    line = reader.ReadLine()
                End While

            End Using

            Dim _data As String = ""
            For Each R As DataRow In dtPage.Select("FNPage>1")

                Dim _Color As Boolean = False
                Dim _Size As Boolean = False
                Dim _COMMODITY As Boolean = False
                Dim _Product As Boolean = False
                Dim _Country As Boolean = False

                Dim ITEM As String = ""
                Dim QUANTITY As Double = 0
                Dim UNITPRICE As Double = 0
                Dim BUYERNUMBER As String = ""
                Dim GTINUCC As String = ""
                Dim PRODUCT As String = ""
                Dim VENDERCOLOR As String = ""
                Dim VENDERSIZE As String = ""
                Dim UNITPRICE2 As Double = 0
                Dim PACK As String = ""
                Dim INNERPACK As String = ""
                Dim COMMODITYCODE As String = ""
                Dim COUNTRY As String = ""
                Dim VENDERSTYLE As String = ""
                Dim FoundITEM As Boolean = False
                Dim _BuyerItemNo As String = ""

                For Each Rx As DataRow In dtsource.Select("FNPage=" & Integer.Parse(Val(R!FNPage.ToString)) & " AND FNRow>0", "FNRow")

                    _data = Rx!FTData.ToString
                    Select Case True
                        Case (Microsoft.VisualBasic.Left(_data, 5) = "-----")
                            ITEM = _data.Replace("-", "").Replace(" ", "").Trim
                            FoundITEM = True
                        Case (_data.Split(" ").Length >= 3) And (FoundITEM) ' (_data.ToUpper Like "*EACH*") And (_data.Split(" ").Length >= 3) And (FoundITEM)

                            Try
                                QUANTITY = Double.Parse(Trim(_data.Split(" ")(0)))
                            Catch ex As Exception
                                QUANTITY = 0
                            End Try

                            Try
                                UNITPRICE = Double.Parse(Trim(_data.Split(" ")(2)))
                            Catch ex As Exception
                                UNITPRICE = 0
                            End Try

                            FoundITEM = False
                        Case (Microsoft.VisualBasic.Left(_data, "Buyer's Item".Length).ToUpper = "Buyer's Item".ToUpper)

                            Try
                                _BuyerItemNo = _data.Split(" ")(_data.Split(" ").Length - 1)
                            Catch ex As Exception
                                _BuyerItemNo = ""
                            End Try
                        Case (Microsoft.VisualBasic.Left(_data, "Buyer's Catalog".Length).ToUpper = "Buyer's Catalog".ToUpper)

                            Try
                                BUYERNUMBER = _data.Split(" ")(_data.Split(" ").Length - 1)
                            Catch ex As Exception
                                BUYERNUMBER = ""
                            End Try

                            If BUYERNUMBER.Length > 11 Then
                                BUYERNUMBER = Microsoft.VisualBasic.Right(BUYERNUMBER, 11)
                            End If
                        Case (Microsoft.VisualBasic.Left(_data, "Vendor's Style".Length).ToUpper = "Vendor's Style".ToUpper)

                            Try
                                VENDERSTYLE = _data.Split(" ")(_data.Split(" ").Length - 1)
                            Catch ex As Exception
                                VENDERSTYLE = ""
                            End Try
                        Case (Microsoft.VisualBasic.Left(_data, "GTIN".Length).ToUpper = "GTIN".ToUpper)

                            Try
                                GTINUCC = _data.Split(" ")(_data.Split(" ").Length - 1)
                            Catch ex As Exception
                                GTINUCC = ""
                            End Try
                        Case (Microsoft.VisualBasic.Left(_data, "PACK".Length).ToUpper = "PACK".ToUpper)

                            Try
                                PACK = _data.Split(" ")(_data.Split(" ").Length - 1)
                            Catch ex As Exception
                                PACK = ""
                            End Try
                        Case (Microsoft.VisualBasic.Left(_data, "INNER PACK".Length).ToUpper = "INNER PACK".ToUpper)

                            Try
                                INNERPACK = _data.Split(" ")(_data.Split(" ").Length - 1)
                            Catch ex As Exception
                                INNERPACK = ""
                            End Try

                        Case (Microsoft.VisualBasic.Left(_data, "UNIT PRICE".Length).ToUpper = "UNIT PRICE".ToUpper)

                            Try
                                UNITPRICE2 = _data.Split(" ")(_data.Split(" ").Length - 1)
                            Catch ex As Exception
                                UNITPRICE2 = ""
                            End Try
                            'Case (Microsoft.VisualBasic.Left(_data, "Product".Length).ToUpper = "Product".ToUpper)
                        Case ((_data.Trim()).ToUpper = "Product".ToUpper)
                            _Product = True
                        Case (_Product = True)
                            _Product = False
                            PRODUCT = _data

                        Case (Microsoft.VisualBasic.Left(_data, "Vendor color".Length).ToUpper = "Vendor color".ToUpper)

                            _Color = True
                        Case (_Color = True)
                            _Color = False
                            VENDERCOLOR = _data

                        Case (Microsoft.VisualBasic.Left(_data, "Vendor size".Length).ToUpper = "Vendor size".ToUpper)
                            _Size = True
                        Case (_Size = True)
                            _Size = False
                            VENDERSIZE = _data

                        Case (_data.ToUpper = "COMMODITY CODE QUALIFIER Harmonized System-Based Schedule B".ToUpper)
                            _COMMODITY = True
                        Case (_COMMODITY = True)
                            _COMMODITY = False

                            Try
                                COMMODITYCODE = _data.Split(" ")(_data.Split(" ").Length - 1)
                            Catch ex As Exception
                                COMMODITYCODE = ""
                            End Try

                        Case (Microsoft.VisualBasic.Left(_data, "Country of Origin".Length).ToUpper = "Country of Origin".ToUpper)

                            _Country = True
                        Case (_Country = True)
                            _Country = False
                            COUNTRY = _data

                    End Select
                Next

                dtMain.Rows.Add(ITEM, QUANTITY, UNITPRICE, BUYERNUMBER, GTINUCC, PRODUCT, VENDERCOLOR, VENDERSIZE, UNITPRICE2, PACK, INNERPACK, COMMODITYCODE, COUNTRY, VENDERSTYLE, _BuyerItemNo)

            Next

            Me.ogcdetail.DataSource = dtMain.Copy
        Catch ex As Exception

        End Try
    End Sub


    Private Sub LoadDataPOPDFTargetSet(Str As String)

        Try
            Dim PageNo As Integer = 0
            Dim PackNo As Integer = 0
            Dim RowNo As Integer = 1
            Dim ItemNo As Integer = 0
            Dim dtMain As DataTable = Me.CreateDatattablePOSet
            Dim MQUANTITY As Double = 0
            Dim MUNITPRICE As Double = 0
            Dim MBUYERNUMBER As String = ""
            Dim SubItemNumber As Integer = 0

            Dim MGTINUCC As String = ""
            Dim _EndSet As Boolean = False

            Dim dtDataSet As New DataTable("SetDataDetail")
            dtDataSet.Columns.Add("FNPackNo", GetType(Integer))
            dtDataSet.Columns.Add("SubItemNo", GetType(Integer))

            Dim dtData As New DataTable("DataDetail")
            dtData.Columns.Add("FNRowNo", GetType(Integer))
            dtData.Columns.Add("FNPackNo", GetType(Integer))
            dtData.Columns.Add("SubItemNo", GetType(Integer))
            dtData.Columns.Add("FTData", GetType(String))

            Dim dtsource As New DataTable("Data")
            dtsource.Columns.Add("FNPage", GetType(Integer))
            dtsource.Columns.Add("ItemNo", GetType(Integer))
            dtsource.Columns.Add("QUANTITY", GetType(Integer))
            dtsource.Columns.Add("UNITPRICE", GetType(Double))
            dtsource.Columns.Add("FTData", GetType(String))

            Using reader As New StringReader(Str)
                Dim line As String = ""
                line = reader.ReadLine()

                While (line IsNot Nothing)


                    If Microsoft.VisualBasic.Left(((line.Trim)).ToUpper, "PACK".Length) = "PACK".ToUpper And (line.Split(" ").Length = 2) Then
                        Try
                            PackNo = Integer.Parse(Val(line.Split(" ")(line.Split(" ").Length - 1).Trim()))
                        Catch ex As Exception
                            PackNo = 0
                        End Try
                    End If

                    If Microsoft.VisualBasic.Left(((line.Trim)).ToUpper, "ASSIGNED IDENTIFICATION".Length) = "ASSIGNED IDENTIFICATION".ToUpper And (line.Split(" ").Length = 3) Then
                        Try
                            SubItemNumber = Integer.Parse(Val(line.Split(" ")(line.Split(" ").Length - 1).Trim()))
                        Catch ex As Exception
                            SubItemNumber = 0
                        End Try

                    End If

                    If ((line.Trim)).ToUpper = "ITEM QUANTITY UNIT OF MEASURE UNIT PRICE".ToUpper And (_EndSet = False) Then
                        PageNo = PageNo + 1
                        ItemNo = 0
                    End If

                    If PageNo > 0 And (_EndSet = False) Then

                        If Microsoft.VisualBasic.Left(((line.Trim)).ToUpper, "Buyer's Catalog Number".Length) = "Buyer's Catalog Number".ToUpper Then
                            Try
                                MBUYERNUMBER = line.Split(" ")(line.Split(" ").Length - 1)
                            Catch ex As Exception
                                MBUYERNUMBER = ""
                            End Try

                            If MBUYERNUMBER.Length > 9 Then
                                MBUYERNUMBER = Microsoft.VisualBasic.Right(MBUYERNUMBER, 9)
                            End If
                        ElseIf Microsoft.VisualBasic.Left(((line.Trim)).ToUpper, "GTIN UC".Length) = "GTIN UC".ToUpper Then
                            Try
                                MGTINUCC = line.Split(" ")(line.Split(" ").Length - 1)
                            Catch ex As Exception
                                MGTINUCC = ""
                            End Try

                            If MGTINUCC.Length > 12 Then
                                MGTINUCC = Microsoft.VisualBasic.Right(MGTINUCC, 12)
                            End If

                            _EndSet = True
                        ElseIf (line.ToUpper Like "*EACH*") And (line.Split(" ").Length >= 3) Then
                            ItemNo = ItemNo + 1

                            Try
                                MQUANTITY = Double.Parse(Trim(line.Split(" ")(0)))
                            Catch ex As Exception
                                MQUANTITY = 0
                            End Try

                            Try
                                MUNITPRICE = Double.Parse(Trim(line.Split(" ")(2)))
                            Catch ex As Exception
                                MUNITPRICE = 0
                            End Try

                            dtsource.Rows.Add(PageNo, ItemNo, MQUANTITY, MUNITPRICE, line)
                        End If
                    End If

                    If dtDataSet.Select("FNPackNo=" & PackNo & " AND SubItemNo=" & SubItemNumber & "").Length <= 0 Then
                        dtDataSet.Rows.Add(PackNo, SubItemNumber)
                    End If

                    dtData.Rows.Add(RowNo, PackNo, SubItemNumber, line)
                    RowNo = RowNo + 1
                    line = reader.ReadLine()

                End While

            End Using

            Dim _data As String = ""
            For Each R As DataRow In dtsource.Rows

                PackNo = Integer.Parse(Val(R!ItemNo.ToString))
                MQUANTITY = Integer.Parse(Val(R!QUANTITY.ToString))
                MUNITPRICE = Double.Parse(Val(R!UNITPRICE.ToString))

                Dim _Color As Boolean = False
                Dim _Size As Boolean = False
                Dim _COMMODITY As Boolean = False
                Dim _Product As Boolean = False
                Dim _Country As Boolean = False
                Dim _Style As Boolean = False
                Dim _GTinUcc As Boolean = False

                Dim ITEM As String = ""

                Dim QUANTITY As Double = 0
                Dim UNITPRICE As Double = 0
                Dim BUYERNUMBER As String = ""
                Dim GTINUCC As String = ""

                Dim PRODUCT As String = ""
                Dim VENDERCOLOR As String = ""
                Dim VENDERSIZE As String = ""
                Dim UNITPRICE2 As Double = 0
                Dim PACK As String = ""
                Dim INNERPACK As String = ""
                Dim COMMODITYCODE As String = ""
                Dim COUNTRY As String = ""
                Dim VENDERSTYLE As String = ""
                Dim _SumPrice As Double = 0

                For Each RmS As DataRow In dtDataSet.Select("FNPackNo=" & PackNo & " AND SubItemNo > 0", "SubItemNo")
                    SubItemNumber = Integer.Parse(Val(RmS!SubItemNo.ToString))

                    For Each Rx As DataRow In dtData.Select("FNPackNo=" & PackNo & " AND SubItemNo=" & SubItemNumber & " AND FNRowNo>0", "FNRowNo")
                        _data = Rx!FTData.ToString
                        Select Case True

                            Case (Microsoft.VisualBasic.Left(_data, "Included".Length).ToUpper = "Included".ToUpper)
                                Try
                                    QUANTITY = Double.Parse(Trim(_data.Split(" ")(1)))
                                Catch ex As Exception
                                    QUANTITY = 0
                                End Try
                            Case (Microsoft.VisualBasic.Left(_data, "UNIT PRICE".Length).ToUpper = "UNIT PRICE".ToUpper)

                                Try
                                    UNITPRICE = Double.Parse(Trim(_data.Split(" ")(2)))
                                Catch ex As Exception
                                    UNITPRICE = 0
                                End Try

                            Case (Microsoft.VisualBasic.Left(_data, "Buyer's Catalog Number".Length).ToUpper = "Buyer's Catalog Number".ToUpper)

                                Try
                                    BUYERNUMBER = _data.Split(" ")(_data.Split(" ").Length - 1)
                                Catch ex As Exception
                                    BUYERNUMBER = ""
                                End Try

                                If BUYERNUMBER.Length > 11 Then
                                    BUYERNUMBER = Microsoft.VisualBasic.Right(BUYERNUMBER, 11)
                                End If
                            Case ((_data.Trim()).ToUpper = "PRODUCT/SERVICE ID QUALIFIER Vendor's Style Number".ToUpper)
                                _Style = True
                            Case (Microsoft.VisualBasic.Left(_data, "PRODUCT/SERVICE ID".Length).ToUpper = "PRODUCT/SERVICE ID".ToUpper) And (_Style) '(Microsoft.VisualBasic.Left(_data, "Vendor's Style".Length).ToUpper = "Vendor's Style".ToUpper)
                                _Style = False
                                Try
                                    VENDERSTYLE = _data.Split(" ")(_data.Split(" ").Length - 1)
                                Catch ex As Exception
                                    VENDERSTYLE = ""
                                End Try

                            Case ((_data.Trim()).ToUpper = "PRODUCT/SERVICE ID QUALIFIER GTIN UCC - 12 Digit Data Structure".ToUpper)
                                _GTinUcc = True
                            Case (Microsoft.VisualBasic.Left(_data, "PRODUCT/SERVICE ID".Length).ToUpper = "PRODUCT/SERVICE ID".ToUpper) And (_GTinUcc) ' (Microsoft.VisualBasic.Left(_data, "GTIN".Length).ToUpper = "GTIN".ToUpper)
                                _GTinUcc = False
                                Try
                                    GTINUCC = _data.Split(" ")(_data.Split(" ").Length - 1)
                                Catch ex As Exception
                                    GTINUCC = ""
                                End Try

                            Case ((_data.Trim()).ToUpper = "Product".ToUpper)
                                _Product = True
                            Case (_Product = True)
                                _Product = False
                                PRODUCT = _data

                            Case (Microsoft.VisualBasic.Left(_data, "Vendor color".Length).ToUpper = "Vendor color".ToUpper)

                                _Color = True
                            Case (_Color = True)
                                _Color = False
                                VENDERCOLOR = _data

                            Case (Microsoft.VisualBasic.Left(_data, "Vendor size".Length).ToUpper = "Vendor size".ToUpper)
                                _Size = True
                            Case (_Size = True)
                                _Size = False
                                VENDERSIZE = _data

                            Case (_data.ToUpper = "COMMODITY CODE QUALIFIER Harmonized System-Based Schedule B".ToUpper)
                                _COMMODITY = True
                            Case (Microsoft.VisualBasic.Left(_data, "COMMODITY CODE".Length).ToUpper = "COMMODITY CODE".ToUpper) And (_COMMODITY = True)
                                _COMMODITY = False

                                Try
                                    COMMODITYCODE = _data.Split(" ")(_data.Split(" ").Length - 1)
                                Catch ex As Exception
                                    COMMODITYCODE = ""
                                End Try

                            Case (Microsoft.VisualBasic.Left(_data, "Country of Origin".Length).ToUpper = "Country of Origin".ToUpper)

                                _Country = True
                            Case (_Country = True)
                                _Country = False
                                COUNTRY = _data

                        End Select

                    Next

                    _SumPrice = _SumPrice + Double.Parse(Format((UNITPRICE * (QUANTITY / MQUANTITY)), "0.000"))
                    _SumPrice = Double.Parse(Format(_SumPrice, "0.000"))
                    dtMain.Rows.Add(PackNo, MQUANTITY, MUNITPRICE, MBUYERNUMBER, MGTINUCC, SubItemNumber, QUANTITY, UNITPRICE, BUYERNUMBER, VENDERSTYLE, GTINUCC, PRODUCT, COMMODITYCODE, COUNTRY, VENDERCOLOR, VENDERSIZE, QUANTITY / MQUANTITY)


                Next
                If _SumPrice <> MUNITPRICE Then
                    For Each RmS As DataRow In dtMain.Select("ITEM=" & PackNo & " ")
                        RmS!FTStatePriceDiff = "1"
                    Next
                End If

            Next

            Me.ogcdetailset.DataSource = dtMain.Copy
            Call InitialGridMergCell()


        Catch ex As Exception

        End Try
    End Sub


    Private Sub InitialGridMergCell()

        For Each c As GridColumn In ogvdetailset.Columns

            Select Case c.FieldName.ToString
                Case "ITEM", "QUANTITY", "UNITPRICE", "BUYERNUMBER", "GTINUCC"
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select
        Next
    End Sub

    Private Sub Grid_CellMerge(sender As Object, e As CellMergeEventArgs) Handles ogvdetailset.CellMerge
        Try
            With Me.ogvdetailset
                If .GetRowCellValue(e.RowHandle1, "ITEM").ToString = .GetRowCellValue(e.RowHandle2, "ITEM").ToString _
                    And .GetRowCellValue(e.RowHandle1, "QUANTITY").ToString = .GetRowCellValue(e.RowHandle2, "QUANTITY").ToString _
                    And .GetRowCellValue(e.RowHandle1, "UNITPRICE").ToString = .GetRowCellValue(e.RowHandle2, "UNITPRICE").ToString _
                    And .GetRowCellValue(e.RowHandle1, "BUYERNUMBER").ToString = .GetRowCellValue(e.RowHandle2, "BUYERNUMBER").ToString _
                    And .GetRowCellValue(e.RowHandle1, "GTINUCC").ToString = .GetRowCellValue(e.RowHandle2, "GTINUCC").ToString Then

                    e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                    e.Handled = True
                    e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

                Else
                    e.Merge = False
                    e.Handled = True
                End If
            End With

        Catch ex As Exception

        End Try
    End Sub


    Private Function ValidateTypePDF(Str) As String
        Dim _SetOrder As Boolean = False

        Using reader As New StringReader(Str)
            Dim line As String = ""
            line = reader.ReadLine()

            While (line IsNot Nothing)

                If ((line.Trim)).ToUpper = "SET ORDER".ToUpper Then
                    _SetOrder = True
                    Exit While
                End If
                line = reader.ReadLine()
            End While

        End Using

        Select Case True
            Case otpnormal.PageVisible

                Return Not (_SetOrder)
            Case otpset.PageVisible

                Return (_SetOrder)
        End Select

    End Function

    Private Sub Loaddata(FilePath As String)
        Dim _Spl As New HI.TL.SplashScreen("Loading.... ,Please wait.")
        Try

            Dim Str As String = ""
            Str = ExtractTextFromPDF(FilePath)

            If Me.ValidateTypePDF(Str) Then
                Select Case True
                    Case otpnormal.PageVisible
                        Call LoadDataPOPDFTargetNormal(Str)
                    Case otpset.PageVisible
                        Call LoadDataPOPDFTargetSet(Str)
                End Select
                _Spl.Close()
            Else
                _Spl.Close()
                HI.MG.ShowMsg.mInfo("ข้อมูล File PDF ไม่ถูกต้องกรูณาทำการตรวจสอบ ว่า เป็น PO Set หรือ PO Normal !!!", 1410270019, Me.Text, , MessageBoxIcon.Warning)
            End If

        Catch ex As Exception
            _Spl.Close()
        End Try


    End Sub

    Private Sub FTFilePath_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles FTFilePath.ButtonClick
        Select Case e.Button.Index
            Case 0
                Try
                    Dim opFileDialog As New System.Windows.Forms.OpenFileDialog
                    opFileDialog.Filter = "PDF Files(*.PDF)|*.PDF"
                    opFileDialog.ShowDialog()

                    Try

                        If opFileDialog.FileName <> "" Then
                            Me.FTFilePath.Text = opFileDialog.FileName
                            Call Loaddata(opFileDialog.FileName)
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
    End Sub

    Private Sub ocmloaddata_Click(sender As Object, e As EventArgs) Handles ocmloaddata.Click
        If Me.FTFilePath.Text.Trim <> "" Then
            Loaddata(Me.FTFilePath.Text)
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, ogbselectfile.Text)
        End If
    End Sub

    Private Sub ocmexportoptiplan_Click(sender As Object, e As EventArgs) Handles ocmexporttoexcel.Click
        If (ogcdetail.DataSource IsNot Nothing) Or (Me.ogvdetailset.DataSource IsNot Nothing) Then
            Try

                If Me.ogvdetail.RowCount <= 0 And Me.ogvdetailset.RowCount <= 0 Then
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมํลที่ต้องการทำการ Export !!!", 1406110009, Me.Text)
                    Exit Sub
                End If

                Dim Op As New System.Windows.Forms.SaveFileDialog
                Op.Filter = "Excel Files(.xlsx)|*.xlsx"
                Op.ShowDialog()
                Try
                    If Op.FileName <> "" Then

                        Select Case True
                            Case Me.otpnormal.PageVisible
                                With CType(ogcdetail, DevExpress.XtraGrid.GridControl)
                                    .ExportToXlsx(Op.FileName)
                                    Try
                                        Process.Start(Op.FileName)
                                    Catch ex As Exception
                                    End Try
                                End With
                            Case otpset.PageVisible
                                With CType(ogcdetailset, DevExpress.XtraGrid.GridControl)
                                    .ExportToXlsx(Op.FileName)
                                    Try
                                        Process.Start(Op.FileName)
                                    Catch ex As Exception
                                    End Try
                                End With
                        End Select

                    End If
                Catch ex As Exception
                End Try

            Catch ex As Exception
            End Try
        Else
            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการ Export ", 1406120399, Me.Text, , MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        If Me.FTFilePath.Text.Trim <> "" Then
            Loaddata(Me.FTFilePath.Text)
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, ogbselectfile.Text)
        End If
    End Sub

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvdetail)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub wReadPDFTarget_Load(sender As Object, e As EventArgs) Handles Me.Load
        HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvdetail)
    End Sub

    Private Sub FNReadPDFType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNReadPDFType.SelectedIndexChanged
        Try
            Me.otpnormal.PageVisible = (FNReadPDFType.SelectedIndex = 0)
            Me.otpset.PageVisible = (FNReadPDFType.SelectedIndex = 1)

            Me.FTFilePath.Text = ""
            Me.ogcdetail.DataSource = Nothing
            Me.ogcdetailset.DataSource = Nothing

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvdetail_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles ogvdetailset.RowCellStyle
        Try

            Try
                With Me.ogvdetailset

                    Try
                        Select Case True
                            Case ("" & .GetRowCellValue(e.RowHandle, "FTStatePriceDiff").ToString = "1")
                                e.Appearance.BackColor = Color.LemonChiffon
                                e.Appearance.ForeColor = Color.Blue
                        End Select
     
                    Catch ex As Exception
                    End Try

                End With
            Catch ex As Exception
            End Try

        Catch ex As Exception
        End Try
    End Sub

End Class