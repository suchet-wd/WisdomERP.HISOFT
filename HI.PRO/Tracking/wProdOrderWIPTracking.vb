Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns
Imports System.Windows.Forms
Imports DevExpress.XtraPrinting

Public Class wProdOrderWIPTracking

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean
    Private _FTStateProdSMKToCutQty As Boolean
    Private _StateQtyBySPM As Boolean = False  ' get Qty by Super market

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        '  Call InitGrid()

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = ""

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        Dim sFieldCustomSum As String = "FNQuantity|FNQuantityExtra|FNGarmentQtyTest|FNGrandQuantity|FNCutQuantity|FNSewQuantity|FNPackQuantity|FNSendSuplQuantity|FNRcvSuplQuantity|FNSPMKQuantity|FNSewOutQuantity|FNBalCutQuantity|FNBalSuplQuantity|FNBalSewQuantity|FNBalPackQuantity|FNCutBalQuantity"
        sFieldCustomSum &= "|FNQtyEmbroidery|FNRcvQtyEmbroidery|FNBalQtyEmbroidery|FNQtyPrint|FNRcvQtyPrint|FNBalQtyPrint|FNQtyHeat|FNRcvQtyHeat|FNBalQtyHeat|FNQtyLaser|FNRcvQtyLaser|FNBalQtyLaser|FNQtyPadPrint|FNRcvQtyPadPrint|FNBalQtyPadPrint|FNQtyWindow|FNRcvQtyWindow|FNBalQtyWindow|FNSPMKQuantityBal"
        'sFieldCustomSum &= "|FNExpQty|FNFGInQty|FNFGBalQty|FNExpBalQty"
        Dim sFieldCustomGrpSum As String = ""

        With ogvdetailcolorsizelineg
            .ClearGrouping()
            .ClearDocument()

            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldCustomSum.Split("|")

                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If

            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldGrpCount.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldCustomGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSum.Split("|")
                'If Str <> "" Then
                '    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                'End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            .ExpandAllGroups()
            .RefreshData()

        End With


        '------End Add Summary Grid-------------
    End Sub
#End Region


#Region "Custom summaries"

    Private totalSum As Integer = 0
    Private GrpSum As Integer = 0
    Private _RowHandleHold As Integer = 0


    Private Sub InitSummaryStartValue()
        totalSum = 0
        GrpSum = 0
        _RowHandleHold = 0
    End Sub

#End Region
#Region "Property"

    Private _CallMenuName As String = ""
    Public Property CallMenuName As String
        Get
            Return _CallMenuName
        End Get
        Set(value As String)
            _CallMenuName = value
        End Set
    End Property

    Private _CallMethodName As String = ""
    Public Property CallMethodName As String
        Get
            Return _CallMethodName
        End Get
        Set(value As String)
            _CallMethodName = value
        End Set
    End Property

    Private _CallMethodParm As String = ""
    Public Property CallMethodParm As String
        Get
            Return _CallMethodParm
        End Get
        Set(value As String)
            _CallMethodParm = value
        End Set
    End Property

    Private _ActualDate As String = ""
    ReadOnly Property ActualDate As String
        Get
            Return _ActualDate
        End Get
    End Property

    Private _ActualNextDate As String = ""
    ReadOnly Property ActualNextDate As String
        Get
            Return _ActualNextDate
        End Get
    End Property


#End Region

#Region "Procedure"

    Private Sub LoadData()


        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0
        StateCal = False
        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try
            Call LoaddataDetailColorSizeByLine()
        Catch ex As Exception
        End Try

        _Spls.Close()
        _RowDataChange = False

    End Sub

    Private Sub LoaddataDetailColorSizeByLine()
        Dim _Qry As String
        Dim _dt As System.Data.DataTable
        ogcdetailcolorsizelineg.DataSource = Nothing
        Try
            If Me.FTStartDate.Text <> "" And Me.FTEndDate.Text <> "" Then

                _Qry = "exec  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "..SP_GET_OrderWIPTracking    @Date='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "' , @DateE='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
                _Qry &= vbCrLf & ", @FTOrderNo='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "',@FTOrderNoTo='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'"
                _Qry &= vbCrLf & ", @FNHSysWHId='" & HI.UL.ULF.rpQuoted(FNHSysWHId.Text) & "',@FNHSysWHIdTo='" & HI.UL.ULF.rpQuoted(FNHSysWHIdTo.Text) & "'"
                _Qry &= vbCrLf & ", @FNHSysMerMatId='" & HI.UL.ULF.rpQuoted(FNHSysMerMatId.Text) & "',@FNHSysMerMatIdTo='" & HI.UL.ULF.rpQuoted(FNHSysMerMatIdTo.Text) & "'"
                _Qry &= vbCrLf & ",@UserName = '" & HI.ST.UserInfo.UserName & "'"
                _Qry &= vbCrLf & ", @FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID)



                'HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
                'HI.Conn.SQLConn.SqlConnectionOpen()
                'HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
                'HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction
                Try
                    _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    'HI.Conn.SQLConn.Tran.Commit()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Catch ex As Exception
                    'HI.Conn.SQLConn.Tran.Rollback()
                    'HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    'HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                End Try



                Me.ogcdetailcolorsizelineg.DataSource = _dt
                '   Call InitialGridMergCell()
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, "กรุณาเลือกวันที่")
            End If




        Catch ex As Exception
        End Try

    End Sub


    Private Sub InitialGridNotSort()
        For Each c As GridColumn In ogvdetailcolorsizelineg.Columns
            c.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        Next
    End Sub

    Private Sub InitialGridMergCell()
        For Each c As GridColumn In ogvdetailcolorsizelineg.Columns
            Select Case c.FieldName.ToString
                Case "FTStyleCode", "FTPORef", "FTOrderNo", "FTCmpCode", "FTCmpName", "FDShipDate", "FTColorway", "FTSizeBreakDown",
                       "FTUnitSectCodeCut", "FTUnitSectCodeSew",
                     "FNQuantityExtra", "FNGarmentQtyTest", "FNGrandQuantity", "FNQuantity", "FNCutQuantity",
                     "FNSendSuplQuantity", "FNRcvSuplQuantity", "FNSPMKQuantity", "FNSPMKQuantityBal", "FNBalSuplQuantity", "FNCutBalQuantity", "FNBalCutQuantity",
                      "FNQtyEmbroidery", "FNRcvQtyEmbroidery", "FNBalQtyEmbroidery",
                         "FNQtyPrint", "FNRcvQtyPrint", "FNBalQtyPrint",
                          "FNQtyHeat", "FNRcvQtyHeat", "FNBalQtyHeat",
                          "FNQtyLaser", "FNRcvQtyLaser", "FNBalQtyLaser",
                          "FNQtyPadPrint", "FNRcvQtyPadPrint", "FNBalQtyPadPrint",
                          "FNQtyWindow", "FNRcvQtyWindow", "FNBalQtyWindow"

                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
                    c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                Case Else
                    c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
            End Select
        Next
    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False
        If Me.FTStartDate.Text <> "" And Me.FTEndDate.Text <> "" Then
            _Pass = True
        End If
        If Me.FTOrderNo.Text <> "" And Me.FTOrderNoTo.Text <> "" Then
            _Pass = True
        End If

        If Me.FNHSysWHId.Text <> "" And Me.FNHSysWHIdTo.Text <> "" Then
            _Pass = True
        End If

        If Me.FNHSysMerMatId.Text <> "" And Me.FNHSysMerMatIdTo.Text <> "" Then
            _Pass = True
        End If


        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อนไข อย่างน้อย 1 รายการ !!!", 2111181146, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function
#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Call InitialGridNotSort()
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvdetailcolorsizelineg)
            StateCal = False

        Catch ex As Exception
        End Try
    End Sub



    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        'If VerifyData() Then

        Dim _Qry As String = ""



        Call LoadData()

        'End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvdetailcolorsizelineg)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Sub ocmexporttoexcel_Click(sender As Object, e As EventArgs) Handles ocmexporttoexcel.Click
        Try
            With DirectCast(Me.ogcdetailcolorsizelineg.DataSource, System.Data.DataTable)
                .AcceptChanges()
                If .Rows.Count <= 0 Then Exit Sub

                Dim _FileName As String = ""
                Dim folderDlg As New SaveFileDialog
                With folderDlg
                    .Filter = "Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"
                    .FilterIndex = 1
                    Dim dr As DialogResult = .ShowDialog()
                    If (dr = System.Windows.Forms.DialogResult.OK) Then
                        Dim _Spls As New HI.TL.SplashScreen("Please Wait.....", "Export Data From File ")

                        Dim path As String = .FileNames(0).ToString
                        Dim _Strm As Stream = New MemoryStream()

                        ' Customize export options 
                        'ogvdetailcolorsizelineg.OptionsPrint.PrintHeader = True
                        'Dim advOptions As XlsxExportOptionsEx = New XlsxExportOptionsEx()
                        'advOptions.AllowGrouping = DevExpress.Utils.DefaultBoolean.False
                        'advOptions.ShowTotalSummaries = DevExpress.Utils.DefaultBoolean.False
                        'advOptions.SheetName = "sheet1"
                        ogcdetailcolorsizelineg.ExportToXlsx(path)
                        ' Open the created XLSX file with the default application. 

                        'Dim xlWorkBook As New Workbook
                        'Dim xlWorkSheet As Excel.Worksheet
                        'Dim misValue As Object = System.Reflection.Missing.Value
                        'Dim xlApp As Excel.Application = New Microsoft.Office.Interop.Excel.Application()

                        'If xlApp Is Nothing Then
                        '    HI.MG.ShowMsg.mInfo("Excel is not properly installed!!", 1803210834, Me.Text, "", MessageBoxIcon.Hand)
                        'End If

                        'xlWorkBook = xlApp.Workbooks.Add(misValue)
                        ''xlWorkSheet = xlWorkBook.Sheets("sheet1")

                        '_Strm.Seek(0, SeekOrigin.Begin)
                        'xlWorkBook.LoadFromStream(_Strm)
                        'xlWorkBook.SaveToFile(path)
                        '_Strm.Dispose()

                        ''xlWorkBook.SaveAs(_SavePath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
                        ''Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
                        'xlWorkBook.Close(True, misValue, misValue)
                        'xlApp.Quit()

                        'releaseObject(xlWorkSheet)
                        'releaseObject(xlWorkBook)
                        'releaseObject(xlApp)

                        'xlWorkSheet = Nothing
                        'xlWorkSheet = Nothing
                        'misValue = Nothing
                        Process.Start(path)
                        _Spls.Close()
                    End If
                End With

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub



    Private Sub ogvdetailcolorsizelineg_CustomSummaryCalculate(sender As Object, e As CustomSummaryEventArgs) Handles ogvdetailcolorsizelineg.CustomSummaryCalculate
        Try
            If e.SummaryProcess = CustomSummaryProcess.Start Then
                InitSummaryStartValue()
            End If

            With ogvdetailcolorsizelineg
                Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
                    Case "FNQuantity"
                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                            If e.IsTotalSummary Then
                                If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                    If (.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold, "FTOrderNo").ToString) Or e.RowHandle = _RowHandleHold Then
                                        ' .GetRowCellValue(e.RowHandle, "FTPOLineItemNo") <> .GetRowCellValue(_RowHandleHold, "FTPOLineItemNo") Or _
                                        totalSum = totalSum + Integer.Parse(Val(e.FieldValue.ToString))

                                    End If
                                End If
                                _RowHandleHold = e.RowHandle
                            End If
                            e.TotalValue = totalSum
                        End If

                    Case "FNAmountPOSuplBF", "FNAmountPOSupl", "FNCostGrossAmt", "FNCostAmt"
                        If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                            If e.IsTotalSummary Then
                                If e.RowHandle <> _RowHandleHold Or e.RowHandle = 0 Then
                                    If (.GetRowCellValue(e.RowHandle, "FTOrderNo").ToString <> .GetRowCellValue(_RowHandleHold, "FTOrderNo").ToString) Or e.RowHandle = _RowHandleHold Then
                                        ' .GetRowCellValue(e.RowHandle, "FTPOLineItemNo") <> .GetRowCellValue(_RowHandleHold, "FTPOLineItemNo") Or _
                                        totalSum = totalSum + Double.Parse(Val(e.FieldValue.ToString))

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
End Class