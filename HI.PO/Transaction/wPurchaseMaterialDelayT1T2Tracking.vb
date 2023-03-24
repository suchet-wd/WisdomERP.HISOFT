Imports DevExpress.Data
Imports DevExpress.Spreadsheet
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid
Imports Microsoft.Office.Interop
Imports System.ComponentModel
Imports System.IO
Imports System.Windows.Forms

Public Class wPurchaseMaterialDelayT1T2Tracking

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean

    Private _FormLoad As Boolean = True
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        Call InitGrid()

    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        'Dim sFieldCount As String = ""
        'Dim sFieldSum As String = "FNPOQuantity|FNRcvQuantity|FNRTsQuantity|FNPOBalQuantity"

        'Dim sFieldGrpCount As String = ""
        'Dim sFieldGrpSum As String = "FNPOQuantity|FNRcvQuantity|FNRTsQuantity|FNPOBalQuantity"

        'Dim sFieldCustomSum As String = ""
        'Dim sFieldCustomGrpSum As String = ""

        'With ogvtime
        '    .ClearGrouping()
        '    .ClearDocument()
        '    '.Columns("FTDateTrans").Group()

        '    For Each Str As String In sFieldCount.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
        '        End If
        '    Next

        '    For Each Str As String In sFieldCustomSum.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
        '        End If
        '    Next

        '    'For Each Str As String In sFieldSum.Split("|")
        '    '    If Str <> "" Then
        '    '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
        '    '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit.ToString & "}"
        '    '    End If
        '    'Next

        '    For Each Str As String In sFieldGrpCount.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
        '        End If
        '    Next

        '    For Each Str As String In sFieldCustomGrpSum.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
        '        End If
        '    Next

        '    For Each Str As String In sFieldGrpSum.Split("|")
        '        If Str <> "" Then
        '            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n" & HI.ST.Config.QtyDigit.ToString & "})")
        '        End If
        '    Next

        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        '    .OptionsView.ShowFooter = True
        '    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

        '    .ExpandAllGroups()
        '    .RefreshData()


        'End With
        '------End Add Summary Grid-------------
    End Sub
#End Region

#Region "Custom summaries"

    Private totalSum As Integer = 0
    Private GrpSum As Integer = 0

    Private Sub InitStartValue()
        totalSum = 0
        GrpSum = 0
    End Sub

    'Private Sub ogv_CustomDrawGroupRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs) Handles ogv.CustomDrawGroupRow

    '    Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo = e.Info
    '    Dim Handle As Integer = ogv.GetDataRowHandleByGroupRowHandle(e.RowHandle)

    '    'Select Case info.Column.FieldName.ToString.ToUpper
    '    '    Case "FNWorkingDay"

    '    Dim GrpDisplayText As String = ogv.GetGroupSummaryText(e.RowHandle)  'ogv.GetGroupRowValue(e.RowHandle, info.Column)
    '    Dim GrpDisplayTextReplace As String = Nothing
    '    Dim GrpDisplayTextReplaceNew As String = Nothing
    '    GrpDisplayTextReplace = GrpDisplayText.Split(")")(1)

    '    If GrpDisplayTextReplace <> "" Then
    '        If GrpDisplayTextReplace.Split("=").Length >= 2 Then
    '            Dim Title1 As String = GrpDisplayTextReplace.Split("=")(0)
    '            Dim Title2 As String = GrpDisplayTextReplace.Split("=")(1)

    '            If IsNumeric(Title2) = False Then
    '                Title2 = "0"
    '            End If
    '            Dim _Sum As Integer = CDbl(Title2)
    '            Dim NetDisplay As String = ""
    '            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
    '                NetDisplay = Format((_Sum \ 480), "00") & " วัน : " & Format(((_Sum Mod 480) \ 60), "00") & " ชั่วโมง : " & Format(((_Sum Mod 480) Mod 60), "00") & " นาที"
    '            Else
    '                NetDisplay = Format((_Sum \ 480), "00") & " Day : " & Format(((_Sum Mod 480) \ 60), "00") & " Hour : " & Format(((_Sum Mod 480) Mod 60), "00") & " Minute"
    '            End If

    '            GrpDisplayTextReplaceNew = Title1 & "=" & NetDisplay
    '            GrpDisplayText = GrpDisplayText.Replace(GrpDisplayTextReplace, GrpDisplayTextReplaceNew)
    '        End If


    '    info.GroupText = info.Column.Caption + ":" + info.GroupValueText + ""
    '    info.GroupText += "" + GrpDisplayText + ""

    '    'End Select

    'End Sub


    Private Sub Gridview_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvtime.CustomSummaryCalculate
        If e.SummaryProcess = CustomSummaryProcess.Start Then
            InitStartValue()
        End If

        Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
            Case "FNTime", "FNOT1", "FNOT1_5", "FNOT2", "FNOT3", "FNOT4"
                If e.SummaryProcess = CustomSummaryProcess.Calculate Then

                    If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                        If e.IsGroupSummary Then
                            Dim Seq As Integer = 1
                            For Each Str As String In e.FieldValue.ToString.Split(":")
                                Select Case Seq
                                    Case 1
                                        GrpSum = GrpSum + (Integer.Parse(Val(Str)) * 60)
                                    Case Else
                                        GrpSum = GrpSum + Integer.Parse(Val(Str))
                                End Select
                                Seq = Seq + 1
                            Next
                        End If

                        If e.IsTotalSummary Then
                            Dim Seq As Integer = 1
                            For Each Str As String In e.FieldValue.ToString.Split(":")
                                Select Case Seq
                                    Case 1
                                        totalSum = totalSum + (Integer.Parse(Val(Str)) * 60)
                                    Case Else
                                        totalSum = totalSum + Integer.Parse(Val(Str))
                                End Select

                                Seq = Seq + 1
                            Next
                        End If

                    End If

                    If e.IsGroupSummary Then
                        Dim GrpDisplay As String = ""
                        GrpDisplay = Format(((GrpSum) \ 60), "00") & " : " & Format(((GrpSum) Mod 60), "00")
                        e.TotalValue = GrpSum
                    End If

                    If e.IsTotalSummary Then
                        Dim NetDisplay As String = ""

                        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                            NetDisplay = Format(((totalSum) \ 60), "00") & " : " & Format(((totalSum) Mod 60), "00")
                        Else
                            NetDisplay = Format(((totalSum) \ 60), "00") & " : " & Format(((totalSum) Mod 60), "00")
                        End If

                        e.TotalValue = NetDisplay ' totalSum 'NetDisplay

                    End If
                End If
        End Select
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
        Dim _Qry As String = ""
        Dim _dtset As New DataSet


        StateCal = False


        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PUR) & "].dbo.USP_GETDATAPO_MATERIAL_DELAYT1T2 '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & FNListDocumentTrackPIData.SelectedIndex.ToString() & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartPurchaseDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndPurchaseDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTStartDelivery.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndDelivery.Text) & "','',''," & Val(FNHSysSuplId.Properties.Tag.ToString) & "," & Val(FNHSysBuyId.Properties.Tag.ToString) & ""


        HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_PUR, _dtset)



        Try
            ogdtime.DataSource = _dtset.Tables(0).Copy
        Catch ex As Exception

        End Try


        Try
            ogct1.DataSource = _dtset.Tables(1).Copy
        Catch ex As Exception

        End Try

        _dtset.Dispose()
        _Spls.Close()


        _RowDataChange = False

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = True

        Return _Pass
    End Function
#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            _FormLoad = False
            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)


            StateCal = False

            Dim Indx As Integer = 0
            Try
                Indx = Val(HI.UL.AppRegistry.ReadRegistry("ListDoc" & Me.Name))
            Catch ex As Exception
            End Try

            FNListDocumentTrackPIData.SelectedIndex = Indx


        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then
            ogdtime.DataSource = Nothing

            Call LoadData()

            Me.otb.SelectedTabPageIndex = 0

        End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        ogdtime.DataSource = Nothing


        HI.TL.HandlerControl.ClearControl(Me)

    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvtime)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ogbheader_Click(sender As Object, e As EventArgs) Handles ogbheader.Click

    End Sub



    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub hideContainerTop_Click(sender As Object, e As EventArgs)

    End Sub




    Private Sub FNListDocumentTrackPIData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FNListDocumentTrackPIData.SelectedIndexChanged
        If _FormLoad = False Then
            Call HI.UL.AppRegistry.WriteRegistry("ListDoc" & Me.Name, FNListDocumentTrackPIData.SelectedIndex.ToString)
        End If
    End Sub

    Private Sub ocmexporttoexcel_Click(sender As Object, e As EventArgs) Handles ocmexporttoexcel.Click
        Try
            If ogdtime.DataSource Is Nothing Then

            Else
                If CType(ogdtime.DataSource, DataTable).Rows.Count > 0 Then

                    Dim Op As New System.Windows.Forms.SaveFileDialog
                    Op.Filter = "Excel Files(.xlsb)|*.xlsb"
                    Op.ShowDialog()
                    Try

                        If Op.FileName <> "" Then


                            Dim _Spls As New HI.TL.SplashScreen("Exporting...Data Pleas wait.")

                            Dim dt As DataTable = CType(ogdtime.DataSource, DataTable).Copy
                            Dim dt2 As DataTable = CType(ogct1.DataSource, DataTable).Copy

                            Dim pFileName As String = System.Windows.Forms.Application.StartupPath & "\ExportPOFormat\MaterialTrackT1T2.xlsb"

                            File.Copy(pFileName, Op.FileName, True)

                            Try

                                Try

                                    opshet.CloseCellEditor(DevExpress.XtraSpreadsheet.CellEditorEnterValueMode.Default)
                                    opshet.CreateNewDocument()

                                Catch ex As Exception
                                End Try

                                opshet.LoadDocument(Op.FileName, DevExpress.Spreadsheet.DocumentFormat.Xlsb)

                                WriteDataToExcelSpret(dt, dt2, _Spls, Op.FileName)
                            Catch ex As Exception
                                _Spls.Close()
                            End Try

                            dt.Dispose()



                        End If

                    Catch ex As Exception
                    End Try

                End If
            End If


        Catch ex As Exception

        End Try
    End Sub


    Private Sub WriteDataToExcelSpret(ByVal _oDt1 As System.Data.DataTable, ByVal _oDt2 As System.Data.DataTable, _Spls As HI.TL.SplashScreen, _FileName As String)
        Dim WriteLoop As Integer = 0

        Try


            Dim _Qry As String = ""
            Dim _Rec As Integer = 0
            Dim _RecError As Integer = 0
            Dim _TotalRec As Integer = 0
            Dim _l As Integer = 0

            Dim i1 As Integer = 2
            Dim i2 As Integer = 4




            opshet.BeginUpdate()
            With opshet.Document.Worksheets(1)
                _Rec = 1
                _TotalRec = _oDt1.Rows.Count



                For Each R As DataRow In _oDt1.Rows
                    _Spls.UpdateInformation("Wiriting Data Excel Row  " & _Rec & "  of  " & _TotalRec & "")

                    .Rows(i1).Item(0).Value = R!FTCOFOCode.ToString()
                    .Rows(i1).Item(1).Value = R!FTFirstInputDate.ToString()
                    .Rows(i1).Item(2).Value = R!FTLastInputDate.ToString()
                    .Rows(i1).Item(3).Value = R!FTSeasonCode.ToString()
                    .Rows(i1).Item(4).Value = R!FTNikeVenderCode.ToString()
                    .Rows(i1).Item(5).Value = R!FTBuyMonth.ToString()
                    .Rows(i1).Item(6).Value = R!FTRawMatCode.ToString()
                    .Rows(i1).Item(7).Value = R!FTRawMatColorCode.ToString()
                    .Rows(i1).Item(8).Value = R!FTPurchaseNo.ToString()
                    .Rows(i1).Item(9).Value = R!FTPINo.ToString()
                    .Rows(i1).Item(10).Value = R!FTStyleCode.ToString()
                    .Rows(i1).Item(11).Value = R!FTVenderPramCode.ToString()
                    .Rows(i1).Item(12).Value = R!FTCmpCode.ToString()
                    .Rows(i1).Item(13).Value = R!FTOETCDate.ToString()
                    .Rows(i1).Item(14).Value = R!FTORGOETCDate.ToString()
                    .Rows(i1).Item(15).Value = R!FTFinalOETCDate.ToString()
                    .Rows(i1).Item(16).Value = R!FNORGLeadTime.ToString()
                    .Rows(i1).Item(17).Value = R!FNFinalLeadTime.ToString()
                    .Rows(i1).Item(18).Value = R!FTDelayLangth.ToString()
                    .Rows(i1).Item(19).Value = Integer.Parse(Val(R!FNPOQuantity.ToString()))
                    .Rows(i1).Item(20).Value = Integer.Parse(Val(R!FNPOBALQuantity.ToString()))
                    .Rows(i1).Item(21).Value = R!FTUnitCode.ToString()
                    .Rows(i1).Item(22).Value = R!FTDelayReasonsName.ToString()
                    .Rows(i1).Item(23).Value = R!FTDelayReasonsCode.ToString()
                    .Rows(i1).Item(24).Value = R!FTFurtherDelayReasonName.ToString()
                    .Rows(i1).Item(25).Value = R!FTFurtherDelayReasonCode.ToString()
                    .Rows(i1).Item(26).Value = ""
                    .Rows(i1).Item(27).Value = ""

                    _Rec = _Rec + 1
                    i1 = i1 + 1

                Next


            End With


            With opshet.Document.Worksheets(2)


                _Rec = 1
                _TotalRec = _oDt2.Rows.Count

                i1 = i2

                For Each R As DataRow In _oDt2.Rows
                    _Spls.UpdateInformation("Wiriting Data Excel Row  " & _Rec & "  of  " & _TotalRec & "")

                    .Rows(i1).Item(0).Value = R!FTCOFOCode.ToString()
                    .Rows(i1).Item(1).Value = R!FTFirstInputDate.ToString()
                    .Rows(i1).Item(2).Value = R!FTLastInputDate.ToString()
                    .Rows(i1).Item(3).Value = R!FTSeasonCode.ToString()
                    .Rows(i1).Item(4).Value = R!FTNikeVenderCode.ToString()
                    .Rows(i1).Item(5).Value = R!FTBuyMonth.ToString()
                    .Rows(i1).Item(6).Value = R!FTRawMatCode.ToString()
                    .Rows(i1).Item(7).Value = R!FTRawMatColorCode.ToString()
                    .Rows(i1).Item(8).Value = R!FTPurchaseNo.ToString()
                    .Rows(i1).Item(9).Value = R!FTPINo.ToString()
                    .Rows(i1).Item(10).Value = R!FTStyleCode.ToString()
                    .Rows(i1).Item(11).Value = R!FTVenderPramCode.ToString()
                    .Rows(i1).Item(12).Value = R!FTCmpCode.ToString()
                    .Rows(i1).Item(13).Value = R!FTOETCDate.ToString()
                    .Rows(i1).Item(14).Value = R!FTORGOETCDate.ToString()
                    .Rows(i1).Item(15).Value = R!FTFinalOETCDate.ToString()
                    .Rows(i1).Item(16).Value = R!FNORGLeadTime.ToString()
                    .Rows(i1).Item(17).Value = R!FNFinalLeadTime.ToString()
                    .Rows(i1).Item(18).Value = R!FTDelayLangth.ToString()
                    .Rows(i1).Item(19).Value = Integer.Parse(Val(R!FNPOQuantity.ToString()))
                    .Rows(i1).Item(20).Value = Integer.Parse(Val(R!FNPOBALQuantity.ToString()))
                    .Rows(i1).Item(21).Value = R!FTUnitCode.ToString()
                    .Rows(i1).Item(22).Value = R!FTDelayReasonsName.ToString()
                    .Rows(i1).Item(23).Value = R!FTDelayReasonsCode.ToString()
                    .Rows(i1).Item(24).Value = R!FTFurtherDelayReasonName.ToString()
                    .Rows(i1).Item(25).Value = R!FTFurtherDelayReasonCode.ToString()
                    .Rows(i1).Item(26).Value = ""
                    .Rows(i1).Item(27).Value = ""

                    .Rows(i1).Item(28).Value = R!FTBuyGrpName.ToString()
                    .Rows(i1).Item(29).Value = Integer.Parse(Val(R!FNGrandQuantity.ToString()))
                    .Rows(i1).Item(30).Value = Integer.Parse(Val(R!FNBalTotalOrder.ToString()))
                    .Rows(i1).Item(31).Value = R!FTPOref.ToString()
                    .Rows(i1).Item(32).Value = R!FDShipDateOrginal.ToString()
                    .Rows(i1).Item(33).Value = R!FDShipDate.ToString()
                    .Rows(i1).Item(34).Value = R!FTBuyName.ToString()
                    .Rows(i1).Item(35).Value = R!FTOrderNo.ToString()


                    _Rec = _Rec + 1
                    i1 = i1 + 1

                Next


            End With
            opshet.EndUpdate()

            _Spls.UpdateInformation("Exporting.... Please wait")

            opshet.SaveDocument()

            _Spls.Close()


            HI.MG.ShowMsg.mInfo("Write Data Complete !!!", 1610100587, Me.Text, , MessageBoxIcon.Information)

            ' Process.Start(_FileName)
            Process.Start(_FileName)


        Catch ex As Exception


            _Spls.Close()
            HI.MG.ShowMsg.mProcessError(15066029917, "Writing Excel File Error....." & vbCrLf & ex.Message.ToString, Me.Text, MessageBoxIcon.Error)
        End Try
    End Sub

End Class