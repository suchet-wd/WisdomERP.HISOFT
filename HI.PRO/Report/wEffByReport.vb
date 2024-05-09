Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Layout
Imports DevExpress.Data
Imports Microsoft.Office.Interop.Excel
Imports System.Drawing

Public Class wEffByReport
    Private m_mergedCellSelect As DevExpress.XtraEditors.CheckEdit
    Private _FileName As String = ""
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.



    End Sub


    Private Sub LoadCompany()

        Dim _Str As String
        _Str = " SELECT   '0' AS FTSelect "
        _Str &= vbCrLf & ",M.FNHSysCmpId"
        _Str &= vbCrLf & ",M.FTCmpCode,ISNULL(IPP.FTIPServer,'') AS FTIPServer"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then

            _Str &= vbCrLf & " , M.FTCmpNameTH AS FTCmpName "
            _Str &= vbCrLf & " ,ISNULL(("
            _Str &= vbCrLf & "SELECT TOP 1 FTNameTH"
            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)"
            _Str &= vbCrLf & "WHERE  (FTListName = N'FNCompensationFoundByYearOption') "
            _Str &= vbCrLf & "AND (FNListIndex = 0)"
            _Str &= vbCrLf & " ),'') AS FNCompensationFoundByYearOption "

        Else

            _Str &= vbCrLf & " , M.FTCmpNameEN AS FTCmpName "
            _Str &= vbCrLf & " ,ISNULL(("
            _Str &= vbCrLf & "SELECT TOP 1 FTNameEN"
            _Str &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)"
            _Str &= vbCrLf & "WHERE  (FTListName = N'FNCompensationFoundByYearOption') "
            _Str &= vbCrLf & "AND (FNListIndex = 0)"
            _Str &= vbCrLf & " ),'') AS FNCompensationFoundByYearOption "

        End If

        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS M WITH(NOLOCK) "
        _Str &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & "].dbo.TSECompanyIPServer AS IPP WITH(NOLOCK) ON M.FNHSysCmpId = IPP.FNHSysCmpId "
        _Str &= vbCrLf & " WHERE ISNULL(M.FTStateActive,'') ='1' AND ISNULL(IPP.FTIPServer,'') <>'' "
        _Str &= vbCrLf & " ORDER BY M.FTCmpCode"

        Me.ogccmp.DataSource = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_SECURITY)

    End Sub



#Region "Initial Grid"
    Private Sub InitGrid()

        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "" ' "H01|H02|H03|H04|H05|H06|H07|H08|H09|H10|H11|H12|H13|Total"
        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "" ' "H01|H02|H03|H04|H05|H06|H07|H08|H09|H10|H11|H12|H13|Total"

        'With ogv
        '    .ClearGrouping()
        '    .ClearDocument()
        '    .Columns.ColumnByFieldName("FTDateTrans").Group()
        '    For Each Str As String In sFieldCount.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
        '        End If
        '    Next

        '    For Each Str As String In sFieldSum.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
        '        End If
        '    Next

        '    For Each Str As String In sFieldGrpCount.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
        '        End If
        '    Next

        '    For Each Str As String In sFieldGrpSum.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
        '        End If
        '    Next

        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        '    .OptionsView.ShowFooter = True

        'End With
        ''------End Add Summary Grid-------------
    End Sub

#End Region

#Region "Property"

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

#End Region

#Region "MAIN PROC"


    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click

        Me.ogc.DataSource = Nothing


        HI.TL.HandlerControl.ClearControl(Me)
        Me.FTStartDate.Focus()

    End Sub

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Procedure "


#Region "Process Calculate"

#End Region





#End Region

#Region "Process Load Data"

    Private Sub LoadDataInfo()
        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")
        Try
            Me.ogc.DataSource = Nothing
            Dim _Dt As System.Data.DataTable = Nothing
            Dim _DtShow As System.Data.DataTable
            Dim _Qry As String = ""
            Dim _SDate As String = HI.UL.ULDate.ConvertEnDB(FTStartDate.Text)
            Dim _EDate As String = HI.UL.ULDate.ConvertEnDB(FTEndDate.Text)
            Dim dt As New System.Data.DataTable
            Dim dtline As New System.Data.DataTable

            Dim _TotalLine As Integer = 0
            Dim _PLine As Integer = 0
            Dim _DisplayLang As String = "TH"

            If HI.ST.Lang.Language <> ST.Lang.eLang.TH Then
                _DisplayLang = "EN"
            End If


            Dim _dtcmpSelect As System.Data.DataTable
            With DirectCast(Me.ogccmp.DataSource, System.Data.DataTable)
                .AcceptChanges()
                _dtcmpSelect = .Select("FTSelect='1'").CopyToDataTable
            End With


            Dim _ServerName, _UID, _PWS, _DBName As String
            Dim _ConnectString As String = ""



            For Each R As DataRow In _dtcmpSelect.Rows

                _ServerName = R!FTIPServer.ToString
                _UID = "sa"
                _PWS = "5k,mew,"
                _DBName = HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER)


                _ConnectString = "SERVER=" & _ServerName & ";UID=" & _UID & ";PWD=" & _PWS & ";Initial Catalog=" & _DBName

                _Spls.UpdateInformation("Loading.... Data Company " & R!FTCmpCode.ToString & "   Please wait....")







                _DBName = HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER)
                _ConnectString = "SERVER=" & _ServerName & ";UID=" & _UID & ";PWD=" & _PWS & ";Initial Catalog=" & _DBName
                _PLine = _PLine + 1
                _SDate = HI.UL.ULDate.ConvertEnDB(FTStartDate.Text)
                _EDate = HI.UL.ULDate.ConvertEnDB(FTEndDate.Text)
                _Spls.UpdateInformation("Loading Data Eff % Company " & R!FTCmpName.ToString & "  Total Line ( " & _PLine.ToString & " of  " & _TotalLine.ToString & " )")

                'Do While _SDate <= _EDate

                _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.[sp_getEffByReport]  '" & _SDate & "','" & _EDate & "'  "
                _Qry &= vbCrLf & "," & Val(R!FNHSysCmpId)


                dt = GetSewFGData(_Qry, _ConnectString)

                If _Dt Is Nothing Then
                    _Dt = dt.Copy
                Else

                    If dt.Rows.Count > 0 Then
                        _Dt.Merge(dt.Copy)
                    End If

                End If

                '    _SDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_SDate, 1))

                'Loop



            Next


            _Spls.Close()

            Me.ogc.DataSource = _Dt.Copy
            Me.ogv.ExpandAllGroups()




            dtline.Dispose()
            dt.Dispose()



        Catch ex As Exception
            _Spls.Close()
        End Try


    End Sub


    Private Function GetSewFGData(cmsstring As String, connstring As String) As System.Data.DataTable
        Dim _Cnn = New System.Data.SqlClient.SqlConnection()
        Dim _Cmd = New System.Data.SqlClient.SqlCommand()
        Dim objDataSet As New System.Data.DataTable
        Try

            If _Cnn.State = ConnectionState.Open Then
                _Cnn.Close()
            End If
            _Cnn.ConnectionString = connstring
            _Cnn.Open()
            _Cmd = _Cnn.CreateCommand

            Dim _Adepter As New System.Data.SqlClient.SqlDataAdapter(_Cmd)
            _Adepter.SelectCommand.CommandTimeout = 0
            _Adepter.SelectCommand.CommandType = CommandType.Text
            _Adepter.SelectCommand.CommandText = cmsstring
            _Adepter.Fill(objDataSet)
            _Adepter.Dispose()

            HI.Conn.SQLConn.DisposeSqlConnection(_Cmd)
            HI.Conn.SQLConn.DisposeSqlConnection(_Cnn)
        Catch ex As Exception
            HI.Conn.SQLConn.DisposeSqlConnection(_Cmd)
            HI.Conn.SQLConn.DisposeSqlConnection(_Cnn)
        End Try
        Return objDataSet
    End Function


#End Region
#Region "General"

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If HI.UL.ULDate.CheckDate(FTStartDate.Text) <> "" Then

            'Select Case Me.FNCalculateIncentiveType.SelectedIndex
            '    Case 0
            Call LoadDataInfo()
            '    Case 1
            '        Call LoadDataChieftSew()
            '    Case 2
            '        Call LoadDataBUCutInfo()
            '    Case 3
            '        Call LoadDataChieftBU()
            '    Case 3 + 99
            '        Call LoadDataStockFabricInfo()
            '    Case 4
            '        Call LoadDataCutAuto()
            '    Case 5

            '        Call LoadDataEmbPrint()
            '    Case 6

            '        Call LoadDataPadPrint()
            '    Case 7
            '        Call LoadDataChieftEmb()

            '    Case 8
            '        Call LoadDataChieftPadPrint()
            '    Case Else
            '        Call LoadDataInfo()
            'End Select

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTStartDate_lbl.Text)
            FTStartDate.Focus()
        End If
    End Sub

    Private Sub FTDateRequest_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FTStartDate.EditValueChanged
        Me.ogc.DataSource = Nothing

    End Sub

    Private Sub wOTRequest_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Call LoadCompany()
        HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)

    End Sub

#End Region


    Private Sub ExportExCel(_Spls As HI.TL.SplashScreen)
        Try

            Dim _oDt As System.Data.DataTable
            With DirectCast(Me.ogc.DataSource, System.Data.DataTable)
                .AcceptChanges()
                _oDt = .Copy

            End With


            'Call NewExcel(_oDt, _Spls)
            Call NewExcelNew_BankForm(_oDt, _Spls)
        Catch ex As Exception
            'MsgBox("Error Step1" & ex.Message.ToString)
        End Try
    End Sub

    Private Sub NewExcelNew_BankForm(ByVal _oDt As System.Data.DataTable, _Spls As HI.TL.SplashScreen)
        Try

            Dim _Qry As String = ""
            Dim _DateNow As String = HI.Conn.SQLConn.GetField(HI.UL.ULDate.FormatDateDB, Conn.DB.DataBaseName.DB_PROD, "")
            Dim _Rec As Integer = 0
            Dim _RecError As Integer = 0
            Dim _TotalRec As Integer = 0
            Dim _l As Integer = 0
            Dim _SumPQtyDay As Double = 0
            Dim _SumUQtyDay As Double = 0
            Dim misValue As Object = System.Reflection.Missing.Value
            Dim _Path As String = System.Windows.Forms.Application.StartupPath.ToString
            Dim TmpFile As String = _Path & "\Reports\TemplateMCO_New.xlsx"
            Dim BakFile As String = _Path & "\Reports\Blank.xlsx"
            Dim oExcel As New Microsoft.Office.Interop.Excel.Application
            Dim xlBookTmp As Workbook
            Dim xlBookBak As Workbook
            Dim Excel As New Microsoft.Office.Interop.Excel.XlDirection

            xlBookTmp = oExcel.Workbooks.Open(TmpFile)



            Dim i As Integer = 3

            For Each R As DataRow In _oDt.Rows
                i += +1

                With xlBookTmp.Worksheets(2)
                    'Date
                    '  .Rows(CStr(i) & ":" & CStr(i)).Insert(Shift:=XlDirection.xlDown)
                    ' .Cells(i, 1).Font.Color = 0
                    .Cells(i, 2) = "'" & R!FTYear.ToString
                    '  .Cells(i, 2).Font.Color = 0
                    .Cells(i, 3) = "" & R!FTMonth.ToString
                    '.Cells(i, 3).Font.Color = 0
                    .Cells(i, 4) = "'" & R!FacGroup.ToString
                    .Cells(i, 5) = "'" & R!FacCode.ToString
                    .Cells(i, 6) = "'" & R!CRcode.ToString
                    .Cells(i, 7) = "'" & R!FTCountryName.ToString
                    .Cells(i, 8) = "'" & R!FTSeasonCode.ToString
                    .Cells(i, 9) = "'" & R!FTStyleCode.ToString
                    .Cells(i, 10) = "" & R!FNSamCut.ToString
                    .Cells(i, 11) = "" & R!FNSamSew.ToString
                    .Cells(i, 12) = "" & R!FNNoSewSAM.ToString
                    .Cells(i, 13) = "" & R!FNSamPack.ToString

                    .Cells(i, 17) = "" & R!FNActualFG.ToString
                    .Cells(i, 18) = "" & R!FNActualFGNoSew.ToString

                    .Cells(i, 21) = "" & R!FNDirectLaborQty.ToString
                    .Cells(i, 22) = "" & R!FNDirectLaborQtyNoSew.ToString
                    .Cells(i, 23) = "" & R!FNWorkingHours.ToString
                    .Cells(i, 24) = "" & R!FNOTHours.ToString

                    .Cells(i, 29) = "" & R!FTRemark.ToString
                    '.Cells(i, 4).Font.Color = 0

                    ' .Cells(i, 4).NumberFormat = "#,###,###"
                End With
            Next



            'Try
            '    If oExcel.Application.Sheets.Count > 1 Then
            '        For xi As Integer = oExcel.Application.Sheets.Count To 2 Step -1
            '            Try
            '                CType(oExcel.Application.ActiveWorkbook.Sheets(xi), Worksheet).Delete()
            '                oExcel.Application.DisplayAlerts = False
            '            Catch ex As Exception
            '            End Try
            '            Try
            '                oExcel.Sheets(xi).delete()
            '                oExcel.Application.DisplayAlerts = True
            '            Catch ex As Exception
            '            End Try
            '        Next
            '    End If
            'Catch ex As Exception
            'End Try

            'Try
            '    CType(oExcel.Application.ActiveWorkbook.Sheets(1), Worksheet).Select()
            'Catch ex As Exception
            'End Try

            oExcel.DisplayAlerts = False
            '_FileName = "C:\Users\NOH-NB\Desktop\TestFile.xlsx"

            xlBookTmp.SaveAs(_FileName)
            xlBookTmp.Close()

            _Spls.Close()
            Process.Start(_FileName)
        Catch ex As Exception
            _Spls.Close()
            HI.MG.ShowMsg.mProcessError(1505029917, "Writing Excel File Error....." & vbCrLf & ex.Message.ToString, Me.Text, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ocmexporttoexcel_Click(sender As Object, e As EventArgs) Handles ocmexporttoexcel.Click
        Try
            Dim Op As New System.Windows.Forms.SaveFileDialog
            Op.Filter = "Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"

            Op.ShowDialog()

            Try
                If Op.FileName <> "" Then
                    _FileName = Op.FileName.ToString
                    'ExportExcel()
                    Dim _Spls As New HI.TL.SplashScreen("Prepre Data For Calculate.. Please Wait ")

                    Call ExportExCel(_Spls)


                End If
            Catch ex As Exception
            End Try

        Catch ex As Exception

        End Try
    End Sub


    Private _StateSetSelectAll As Boolean = True
    Private Sub oChkSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles oChkSelectAll.CheckedChanged
        Try

            If _StateSetSelectAll = False Then Exit Sub
            _StateSetSelectAll = False
            '    Me.oChkSelectAll.Checked = False

            Dim _State As String = "0"
            If Me.oChkSelectAll.Checked Then
                _State = "1"
            End If

            With ogccmp
                If Not (.DataSource Is Nothing) And ogvcmp.RowCount > 0 Then

                    With ogvcmp
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With
                    CType(.DataSource, System.Data.DataTable).AcceptChanges()
                End If
            End With

        Catch ex As Exception
        End Try
        _StateSetSelectAll = True
    End Sub

    Private Sub ogv_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogv.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim category As Integer = e.RowHandle
                If category Mod 2 = 1 Then
                    e.Appearance.BackColor = Color.White
                    e.Appearance.BackColor2 = Color.AliceBlue
                    e.HighPriority = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class