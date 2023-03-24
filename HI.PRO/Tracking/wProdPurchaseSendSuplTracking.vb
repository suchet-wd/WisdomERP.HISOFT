Imports DevExpress.Data
Imports System.IO
Imports DevExpress.XtraGrid.Columns

Public Class wProdPurchaseSendSuplTracking

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        Call InitGrid()

    End Sub

#Region "Initial Grid"

    Private Sub InitGrid()



        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = ""
        Dim sFieldSum As String = "FNQuantity"
        Dim sFieldSumAmt As String = "FNAmount"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FNQuantity"
        Dim sFieldGrpSumAmt As String = "FNAmount"

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogvtime
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()

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

            For Each Str As String In sFieldSumAmt.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
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
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            For Each Str As String In sFieldGrpSumAmt.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            .ExpandAllGroups()
            .RefreshData()

        End With

        ''------End Add Summary Grid-------------
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

        ogdtime.DataSource = Nothing

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0

        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Try

            _Qry = "   SELECT H.FTPurchaseNo"
            _Qry &= vbCrLf & " ,CASE WHEN ISDATE(H.FDPurchaseDate) = 1 THEN  Convert(Datetime,H.FDPurchaseDate)  ELSE NULL END  AS FDPurchaseDate "
            _Qry &= vbCrLf & "  , H.FTPurchaseBy"
            _Qry &= vbCrLf & "  , S.FTSuplCode"


            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Qry &= vbCrLf & "  , S.FTSuplNameTH AS FTSuplName"
                _Qry &= vbCrLf & "  ,PT.FTPartNameTH AS FTPartName"
                _Qry &= vbCrLf & "   ,SPT.FTNameTH AS FNSendSuplTypeName"
            Else
                _Qry &= vbCrLf & "  , S.FTSuplNameEN AS FTSuplName "
                _Qry &= vbCrLf & "  ,PT.FTPartNameEN AS FTPartName"
                _Qry &= vbCrLf & "   ,SPT.FTNameEN AS FNSendSuplTypeName "
            End If

            _Qry &= vbCrLf & "    ,PT.FTPartCode "
            _Qry &= vbCrLf & "    , D.FTNote"
            _Qry &= vbCrLf & " , D.FNQuantity"
            _Qry &= vbCrLf & " , D.FNPrice"
            _Qry &= vbCrLf & " , D.FNAmount"

            _Qry &= vbCrLf & "  FROM      [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl AS H WITH(NOLOCK)  INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTPurchaseSendSupl_Detail AS D WITH(NOLOCK)  ON H.FTPurchaseNo = D.FTPurchaseNo INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK)  ON H.FNHSysSuplId = S.FNHSysSuplId INNER JOIN"
            _Qry &= vbCrLf & "            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS PT WITH(NOLOCK) ON D.FNHSysPartId = PT.FNHSysPartId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   ("
            _Qry &= vbCrLf & "   SELECT FNListIndex, FTNameTH, FTNameEN"
            _Qry &= vbCrLf & " 		FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK)"
            _Qry &= vbCrLf & " 		WHERE  (FTListName = N'FNSendSuplType')"
            _Qry &= vbCrLf & "   ) AS SPT ON D.FNSendSuplType = SPT.FNListIndex"
            _Qry &= vbCrLf & " "
            _Qry &= vbCrLf & " WHERE H.FTPurchaseNo <> '' "

            If Me.FNHSysSuplId.Text <> "" Then
                _Qry &= vbCrLf & " AND S.FTSuplCode ='" & HI.UL.ULF.rpQuoted(FNHSysSuplId.Text) & "'  "
            End If

            If Me.FTStartPO.Text <> "" Then
                _Qry &= vbCrLf & " AND H.FDPurchaseDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartPO.Text) & "'"
            End If

            If Me.FTEndPO.Text <> "" Then
                _Qry &= vbCrLf & " AND H.FDPurchaseDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndPO.Text) & "'"
            End If

            _Qry &= vbCrLf & " ORDER BY  H.FTPurchaseNo"
            _Qry &= vbCrLf & " , S.FTSuplCode"


            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Me.ogdtime.DataSource = _dt
            Call InitialGridMergCell()
        Catch ex As Exception
        End Try



        _Spls.Close()
        _RowDataChange = False

    End Sub


    Private Sub InitialGridMergCell()

        'For Each c As GridColumn In ogvtime.Columns

        '    Select Case c.FieldName.ToString
        '        Case "FNRcvQuantity", "FTPositionPartName", "FTMatColorCode", "FTRawMatColorName"
        '            c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False
        '        Case Else
        '            c.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True
        '            c.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        '    End Select

        'Next

    End Sub


    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False


        If Me.FTStartPO.Text <> "" Then
            _Pass = True
        End If

        If Me.FTEndPO.Text <> "" Then
            _Pass = True
        End If

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInfo("กรุณาทำการเลือกเงื่อไข อย่างน้อย 1 รายการ !!!", 1406170001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
        End If

        Return _Pass
    End Function
#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)
            StateCal = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If VerifyData() Then
            Call LoadData()
        End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvtime)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ogvtime_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvtime.CellMerge
        Try
            With Me.ogvtime


                Select Case e.Column.FieldName
                    Case "FTColorway"
                        If "" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString _
                            And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case "FTSizeBreakDown"

                        If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) _
                            And ("" & .GetRowCellValue(e.RowHandle1, "FTColorway").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTColorway").ToString) _
                            And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case "FNQuantity"

                        If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) _
                           And ("" & .GetRowCellValue(e.RowHandle1, "FTColorway").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTColorway").ToString) _
                           And ("" & .GetRowCellValue(e.RowHandle1, "FTSizeBreakDown").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTSizeBreakDown").ToString) _
                           And ("" & .GetRowCellValue(e.RowHandle1, "FTSenSuplTypeName").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTSenSuplTypeName").ToString) _
                           And ("" & .GetRowCellValue(e.RowHandle1, "FTPartName").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTPartName").ToString) _
                            And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case "FNSendQuantity", "FNBalRcvSupl"
                        If ("" & .GetRowCellValue(e.RowHandle1, "FTOrderNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTOrderNo").ToString) _
                          And ("" & .GetRowCellValue(e.RowHandle1, "FTColorway").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTColorway").ToString) _
                          And ("" & .GetRowCellValue(e.RowHandle1, "FTSizeBreakDown").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTSizeBreakDown").ToString) _
                          And ("" & .GetRowCellValue(e.RowHandle1, "FTSenSuplTypeName").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTSenSuplTypeName").ToString) _
                          And ("" & .GetRowCellValue(e.RowHandle1, "FTPartName").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTPartName").ToString) _
                          And ("" & .GetRowCellValue(e.RowHandle1, "FTSendSuplNo").ToString = "" & .GetRowCellValue(e.RowHandle2, "FTSendSuplNo").ToString) _
                            And "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then

                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If
                    Case "FTSuplCode", "FTSuplName", "FTOrderNo", "FTCmpCode", "FTCmpName", "FTSenSuplTypeName", "FTPartName", "FTSendSuplNo", "FDSendSuplDate", "FTRcvSuplNo", "FDRcvSuplDate", "FNSendSuplState"

                        If "" & .GetRowCellValue(e.RowHandle1, e.Column.FieldName).ToString = "" & .GetRowCellValue(e.RowHandle2, e.Column.FieldName).ToString Then
                            e.Merge = (e.CellValue1.ToString = e.CellValue2.ToString)
                            e.Handled = True
                            e.Column.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
                        Else
                            e.Merge = False
                            e.Handled = True
                        End If

                    Case Else
                        e.Merge = False
                        e.Handled = True
                End Select

            End With

        Catch ex As Exception

        End Try
    End Sub

    'Private Sub ogvtime_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvtime.RowCellStyle
    '    Try
    '        With Me.ogvtime
    '            Select Case e.Column.FieldName
    '                Case "FNCutQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNGrandQuantity")) Then
    '                        e.Appearance.BackColor = Drawing.Color.OrangeRed
    '                    Else
    '                        e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                    End If
    '                Case "FNRcvSuplQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSendSuplQuantity")) > 0 Then
    '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNRcvSuplQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSendSuplQuantity")) Then
    '                            e.Appearance.BackColor = Drawing.Color.OrangeRed
    '                        Else
    '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                        End If
    '                    End If

    '                Case "FNSPMKQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) > 0 Then
    '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSPMKQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) Then
    '                            e.Appearance.BackColor = Drawing.Color.OrangeRed
    '                        Else
    '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                        End If
    '                    End If
    '                Case "FNSewQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSPMKQuantity")) > 0 Then
    '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSPMKQuantity")) Then
    '                            e.Appearance.BackColor = Drawing.Color.OrangeRed
    '                        Else
    '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                        End If
    '                    End If

    '                Case "FNSewOutQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) > 0 Then
    '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) Then
    '                            e.Appearance.BackColor = Drawing.Color.OrangeRed
    '                        Else
    '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                        End If
    '                    End If

    '                Case "FNPackQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) > 0 Then
    '                        If Double.Parse(.GetRowCellValue(e.RowHandle, "FNPackQuantity")) < Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) Then
    '                            e.Appearance.BackColor = Drawing.Color.OrangeRed

    '                        Else
    '                            e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                        End If
    '                    End If
    '                Case "FNBalCutQuantity", "FNBalSuplQuantity", "FNBalSewQuantity", "FNBalPackQuantity", "FNCutBalQuantity"
    '                    If Double.Parse(.GetRowCellValue(e.RowHandle, e.Column.FieldName)) > 0 Then
    '                        e.Appearance.BackColor = Drawing.Color.OrangeRed
    '                    Else

    '                        Select Case e.Column.FieldName
    '                            Case "FNBalCutQuantity"
    '                                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNCutQuantity")) > 0 Then
    '                                    e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                                End If
    '                            Case "FNBalSuplQuantity"
    '                                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNRcvSuplQuantity")) > 0 Then
    '                                    e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                                End If
    '                            Case "FNBalSewQuantity"
    '                                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewQuantity")) > 0 Then
    '                                    e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                                End If
    '                            Case "FNBalPackQuantity"
    '                                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNSewOutQuantity")) > 0 Then
    '                                    e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                                End If
    '                            Case "FNCutBalQuantity"
    '                                If Double.Parse(.GetRowCellValue(e.RowHandle, "FNGrandQuantity")) > 0 Then
    '                                    e.Appearance.BackColor = Drawing.Color.GreenYellow
    '                                End If
    '                        End Select

    '                    End If

    '            End Select
    '        End With
    '    Catch ex As Exception

    '    End Try
    'End Sub



End Class