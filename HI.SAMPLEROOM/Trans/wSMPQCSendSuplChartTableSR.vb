Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.Data

Public Class wSMPQCSendSuplChartTableSR

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub wQCSendSuplChartTable_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
            Call CreateTabDynamic(Nothing, Nothing)
        Catch ex As Exception
        End Try
    End Sub

    Private Function VerrifyData() As Boolean
        Try
            If FTSMPOrderNo.Text <> "" And FTSMPOrderNoTo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTOrderNoTo_lbl.Text)
                Me.FTSMPOrderNoTo.Focus()
                Return False
            End If

            If FTSMPOrderNo.Text = "" And FTSMPOrderNoTo.Text <> "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTOrderNo_lbl.Text)
                Me.FTSMPOrderNo.Focus()
                Return False
            End If

            If StartRcvDate.Text <> "" And EndRcvDate.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTEndRcv_lbl.Text)
                Me.EndRcvDate.Focus()
                Return False
            End If

            If StartRcvDate.Text = "" And EndRcvDate.Text <> "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTStartRcv_lbl.Text)
                Me.StartRcvDate.Focus()
                Return False
            End If

            If StartRcvDate.Text <> "" And EndRcvDate.Text <> "" And (HI.UL.ULDate.ConvertEnDB(Me.StartRcvDate.Text) > HI.UL.ULDate.ConvertEnDB(Me.EndRcvDate.Text)) Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTStartRcv_lbl.Text)
                Me.StartRcvDate.Focus()
                Return False
            End If

            If StartQCDate.Text <> "" And EndQCDate.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.EFTDateQC_lbl.Text)
                Me.EndQCDate.Focus()
                Return False
            End If

            If StartQCDate.Text = "" And EndQCDate.Text <> "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.SFTDateQC_lbl.Text)
                Me.StartQCDate.Focus()
                Return False
            End If

            If StartQCDate.Text <> "" And EndQCDate.Text <> "" And (HI.UL.ULDate.ConvertEnDB(Me.StartQCDate.Text) > HI.UL.ULDate.ConvertEnDB(Me.EndQCDate.Text)) Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.SFTDateQC_lbl.Text)
                Me.StartQCDate.Focus()
                Return False
            End If

            'If FTSMPOrderNo.Text = "" Then

            '    If Me.StartRcvDate.Text = "" Then
            '        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTStartRcv_lbl.Text)
            '        Me.StartRcvDate.Focus()
            '        Return False
            '    End If
            '    If Me.EndRcvDate.Text = "" Then
            '        HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.FTEndRcv_lbl.Text)
            '        Me.EndRcvDate.Focus()
            '        Return False
            '    End If

            'Else

            'End If


            Return True
        Catch ex As Exception
        End Try
    End Function

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If VerrifyData() Then
                Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")
                Try
                    Call LoadData()
                    _Spls.Close()
                Catch ex As Exception
                    _Spls.Close()
                End Try
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadData()
        Dim _Cmp As String = HI.ST.SysInfo.CmpID
        Try
            Dim _Cmd As String = ""

            ' --------------------------------------------------------------------------------------------------------------'

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd = "Select max(Q.FTQCSupDetailNameTH) As FTQCSupDetailName, sum(T.FNDefectQty) AS FNDefectQty, S.FTSuplCode "  ', t.FTBarcodeSendSuplNo
            Else
                _Cmd = "Select max(Q.FTQCSupDetailNameEN) As FTQCSupDetailName, sum(T.FNDefectQty) AS FNDefectQty, S.FTSuplCode " ', t.FTBarcodeSendSuplNo
            End If

            _Cmd &= vbCrLf & " From (SELECT Q.FDInsDate, (Select Top 1 B.FTBarcodeSendSuplNo From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcode_SendSupl AS B  WITH (NOLOCK) "
            _Cmd &= vbCrLf & " INNER Join [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder AS O WITH (NOLOCK)  ON B.FTOrderProdNo = O.FTSMPOrderNo "
            _Cmd &= vbCrLf & " Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS T WITH (NOLOCK)  ON O.FNHSysStyleId = T.FNHSysStyleId "
            _Cmd &= vbCrLf & " INNER Join [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS BB WITH (NOLOCK)  ON B.FTBarcodeBundleNo = BB.FTBarcodeBundleNo "
            _Cmd &= vbCrLf & " WHERE O.FTSMPOrderNo = D.FTOrderNo And T.FTStyleCode = D.FTStyleCode "
            _Cmd &= vbCrLf & " And BB.FNBunbleSeq = D.FNBunbleSeq And BB.FTColorway =D.FTColorway "
            _Cmd &= vbCrLf & " And BB.FTSizeBreakDown = D.FTSizeBreakDown) as FTBarcodeSendSuplNo, D.FNHSysQCSuplDetailId , Q.FNDefectQty "
            _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTSendSuplDefect AS Q WITH (NOLOCK)  "
            _Cmd &= vbCrLf & " Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTSendSuplDefect_Detail AS D WITH (NOLOCK) ON Q.FTBarcodeSendSuplNo = D.FTBarcodeSendSuplNo "
            _Cmd &= vbCrLf & " WHERE Q.FNDefectQty > 0 "

            _Cmd &= vbCrLf & " UNION ALL " & vbCrLf

            _Cmd &= vbCrLf & " Select Q.FDInsDate, Q.FTBarcodeSendSuplNo, D.FNHSysQCSuplDetailId  , 1 As FNDefectQty "
            _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPtSendSuplDefect AS Q WITH (NOLOCK)  "
            _Cmd &= vbCrLf & " Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTSendSuplDefect_Detail AS D WITH (NOLOCK) ON Q.FTBarcodeSendSuplNo = D.FTBarcodeSendSuplNo "
            _Cmd &= vbCrLf & " WHERE Q.FNDefectQty > 0 ) AS T  "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQCSuplDetail AS Q WITH(NOLOCK) ON T.FNHSysQCSuplDetailId = Q.FNHSysQCSuplDetailId "
            _Cmd &= vbCrLf & " INNER Join  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcode_SendSupl AS B WITH(NOLOCK) ON T.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo "
            _Cmd &= vbCrLf & " Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS S WITH(NOLOCK) ON B.FNHSysSuplId = S.FNHSysSuplId "
            _Cmd &= vbCrLf & " WHERE B.FNHSysCmpId = '" & HI.UL.ULF.rpQuoted(_Cmp) & "'"

            _Cmd &= vbCrLf & " And exists ( Select bx.FTBarcodeSendSuplNo From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTReceiveSupl AS Rx WITH (NOLOCK) "
            _Cmd &= vbCrLf & " Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTReceiveSupl_Barcode AS Bx WITH (NOLOCK) ON Rx.FTRcvSuplNo = Bx.FTRcvSuplNo "
            _Cmd &= vbCrLf & " Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcode_SendSupl AS Sx WITH(NOLOCK) ON Bx.FTBarcodeSendSuplNo = Sx.FTBarcodeSendSuplNo "
            _Cmd &= vbCrLf & " Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS Dx WITH(NOLOCK) ON Sx.FTBarcodeBundleNo = Dx.FTBarcodeBundleNo "
            _Cmd &= vbCrLf & " Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS Px WITH(NOLOCK) ON Sx.FNHSysSuplId = Px.FNHSysSuplId "
            _Cmd &= vbCrLf & " Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTSendSuplDefect AS SSDx WITH(NOLOCK) ON SSDx.FTBarcodeSendSuplNo = Bx.FTBarcodeSendSuplNo "
            _Cmd &= vbCrLf & " WHERE Px.FTSuplCode <> ''"

            ' ---------------- Receive Date ----------------
            If Me.StartRcvDate.Text <> "" Then
                _Cmd &= vbCrLf & " AND Rx.FDRcvSuplDate  >='" & HI.UL.ULDate.ConvertEnDB(Me.StartRcvDate.Text) & "'"
            End If

            If Me.EndRcvDate.Text <> "" Then
                _Cmd &= vbCrLf & " AND Rx.FDRcvSuplDate <= '" & HI.UL.ULDate.ConvertEnDB(Me.EndRcvDate.Text) & "'"
            End If
            ' ---------------- End Receive Date ----------------

            ' ---------------- Checking Date ----------------
            If Me.StartQCDate.Text <> "" Then
                _Cmd &= vbCrLf & " AND SSDx.FDInsDate  >='" & HI.UL.ULDate.ConvertEnDB(Me.StartQCDate.Text) & "'"
            End If

            If Me.EndQCDate.Text <> "" Then
                _Cmd &= vbCrLf & " AND SSDx.FDInsDate <= '" & HI.UL.ULDate.ConvertEnDB(Me.EndQCDate.Text) & "'"
            End If
            ' ---------------- End Checking Date ----------------

            If Me.FNHSysSuplId.Text <> "" Then
                _Cmd &= vbCrLf & " AND Px.FTSuplCode = '" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "'"
            End If

            If FTSMPOrderNo.Text <> "" Then
                _Cmd &= vbCrLf & " AND Sx.FTOrderProdNo >='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"
            End If

            If FTSMPOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " AND Sx.FTOrderProdNo <='" & HI.UL.ULF.rpQuoted(FTSMPOrderNoTo.Text) & "'"
            End If
            _Cmd &= vbCrLf & " AND Sx.FNHSysCmpId= '" & HI.UL.ULF.rpQuoted(_Cmp) & "'"

            _Cmd &= vbCrLf & " And bx.FTBarcodeSendSuplNo = b.FTBarcodeSendSuplNo ) "

            _Cmd &= vbCrLf & "GROUP BY T.FNHSysQCSuplDetailId, S.FTSuplCode" ', t.FTBarcodeSendSuplNo
            _Cmd &= vbCrLf & "ORDER BY S.FTSuplCode"

            Dim _oDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)

            _Cmd = " SELECT SUM(D.FNQuantity) AS FNQuantity , P.FTSuplCode"
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTReceiveSupl AS R WITH (NOLOCK) "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTReceiveSupl_Barcode AS B WITH (NOLOCK) ON R.FTRcvSuplNo = B.FTRcvSuplNo "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBarcode_SendSupl AS S WITH(NOLOCK) ON B.FTBarcodeSendSuplNo = S.FTBarcodeSendSuplNo "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTBundle AS D WITH(NOLOCK) ON S.FTBarcodeBundleNo = D.FTBarcodeBundleNo "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS P WITH(NOLOCK) ON S.FNHSysSuplId = P.FNHSysSuplId "
            _Cmd &= vbCrLf & " Left OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPTSendSuplDefect AS SSD WITH(NOLOCK) ON SSD.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo "
            _Cmd &= vbCrLf & " WHERE P.FTSuplCode <> ''"

            ' ---------------- Receive Date ----------------
            If Me.StartRcvDate.Text <> "" Then
                _Cmd &= vbCrLf & " AND R.FDRcvSuplDate  >='" & HI.UL.ULDate.ConvertEnDB(Me.StartRcvDate.Text) & "'"
            End If

            If Me.EndRcvDate.Text <> "" Then
                _Cmd &= vbCrLf & " AND R.FDRcvSuplDate <= '" & HI.UL.ULDate.ConvertEnDB(Me.EndRcvDate.Text) & "'"
            End If
            ' ---------------- End Receive Date ----------------

            ' ---------------- Checking Date ----------------
            If Me.StartQCDate.Text <> "" Then
                _Cmd &= vbCrLf & " AND SSD.FDInsDate  >='" & HI.UL.ULDate.ConvertEnDB(Me.StartQCDate.Text) & "'"
            End If

            If Me.EndQCDate.Text <> "" Then
                _Cmd &= vbCrLf & " AND SSD.FDInsDate <= '" & HI.UL.ULDate.ConvertEnDB(Me.EndQCDate.Text) & "'"
            End If
            ' ---------------- End Checking Date ----------------

            If Me.FNHSysSuplId.Text <> "" Then
                _Cmd &= vbCrLf & " AND P.FTSuplCode = '" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "'"
            End If

            If FTSMPOrderNo.Text <> "" Then
                _Cmd &= vbCrLf & " AND S.FTOrderProdNo >='" & HI.UL.ULF.rpQuoted(FTSMPOrderNo.Text) & "'"
            End If

            If FTSMPOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " AND S.FTOrderProdNo <='" & HI.UL.ULF.rpQuoted(FTSMPOrderNoTo.Text) & "'"
            End If
            _Cmd &= vbCrLf & " AND S.FNHSysCmpId= '" & HI.UL.ULF.rpQuoted(_Cmp) & "'"

            _Cmd &= vbCrLf & " GROUP BY P.FTSuplCode"
            Dim _oDtSum As DataTable = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)

            CreateTabDynamic(_oDt, _oDtSum)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CreateTabDynamic(_oDt As DataTable, _oDtSum As DataTable)
        Try
            Dim dt As New DataTable
            Dim _StateDefual As Boolean = False
            With dt
                .Columns.Add("FTSuplCode", GetType(String))
            End With
            If _oDt Is Nothing Then
                _StateDefual = True
                _oDt = New DataTable
                _oDt.Columns.Add("FTSuplCode", GetType(String))
                _oDt.Rows.Add("Detail")
            End If
            Me.otabDetail.TabPages.Clear()
            Dim _Fillter As String = ""
            For Each x As DataRow In _oDt.Select("FTSuplCode <>''", "FTSuplCode")
                If dt.Select("FTSuplCode='" & x!FTSuplCode.ToString & "'").Length <= 0 Then
                    dt.Rows.Add(x!FTSuplCode.ToString)
                End If
            Next
            If dt.Rows.Count = 0 And _oDt.Rows.Count = 0 Then
                For Each xx As DataRow In _oDtSum.Select("FTSuplCode <>''", "FTSuplCode")
                    _oDt.Rows.Add("", 0, xx!FTSuplCode.ToString)
                    dt.Rows.Add(xx!FTSuplCode.ToString)
                Next
            End If
            For Each R As DataRow In dt.Rows
                'Me.oCTabPacking.TabPages.Add(Microsoft.VisualBasic.Left(R!FTRawMatNameEN.ToString, 20))
                Dim _TabPage As New DevExpress.XtraTab.XtraTabPage
                Dim _Grid As New DevExpress.XtraGrid.GridControl
                With _TabPage
                    .Name = "otb" & R!FTSuplCode.ToString
                    .Text = R!FTSuplCode.ToString
                    .Tag = "2|"
                End With
                With _Grid
                    .Name = "ogcG" & R!FTSuplCode.ToString
                    .Tag = "2|"
                End With
                Dim _GridV As New DevExpress.XtraGrid.Views.Grid.GridView
                With _GridV
                    .GridControl = _Grid
                    .Name = "ogvG" & R!FTSuplCode.ToString
                    .OptionsPrint.AutoWidth = False
                    .OptionsCustomization.AllowQuickHideColumns = False
                    .OptionsPrint.PrintHeader = False
                    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                    .OptionsView.ShowColumnHeaders = True
                    .OptionsView.ShowGroupPanel = False
                    .OptionsView.ColumnAutoWidth = False
                    '.OptionsView.ShowAutoFilterRow = True
                    AddHandler .RowStyle, AddressOf GridView1_RowStyle
                    AddHandler .CustomSummaryCalculate, AddressOf GridView1_CustomSummaryCalculate
                End With
                _Grid.BeginInit()
                _Grid.MainView = _GridV
                _Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {_GridV})
                _Grid.EndInit()

                _TabPage.Controls.Add(_Grid)
                _Grid.Dock = DockStyle.Fill


                Dim RData As New DataTable
                Dim _GrandTotal As Integer = 0
                Dim oDt As DataTable = _oDt.Select("FTSuplCode='" & R!FTSuplCode.ToString & "'").CopyToDataTable
                Dim _dt As New DataTable
                With _dt
                    .Columns.Add("FTName", GetType(String))
                    .Columns.Add("FNQuantity", GetType(Double))
                    .Columns.Add("FNQuantityPercent", GetType(Double))
                End With
                Try
                    For Each X As DataRow In _oDtSum.Select("FTSuplCode='" & R!FTSuplCode.ToString & "'")
                        _dt.Rows.Add(Me.FNSendSuplQty_lbl.Text, X!FNQuantity.ToString, Nothing)
                    Next
                    For Each X As DataRow In oDt.Rows
                        '   _GrandTotal += +CInt("0" & R!FNDefectQty.ToString)
                        _GrandTotal = _GrandTotal + X!FNDefectQty.ToString
                    Next
                    '************************
                    _dt.Rows.Add(Me.FNSendSuplDefectQty_lbl.Text, _GrandTotal, Nothing)
                    '
                    For Each X As DataRow In oDt.Rows
                        If _GrandTotal <> 0 Then
                            _dt.Rows.Add(X!FTQCSupDetailName.ToString, CInt("0" & X!FNDefectQty.ToString), CInt("0" & X!FNDefectQty.ToString) / _GrandTotal)
                        Else
                            _dt.Rows.Add(X!FTQCSupDetailName.ToString, CInt("0" & X!FNDefectQty.ToString), CInt("0" & X!FNDefectQty.ToString) / 1)
                        End If
                    Next
                Catch ex As Exception
                End Try

                'If (_StateDefual) Then
                '    Call CreateGrid(_GridV, _dt, _Grid)
                'Else
                '    Call CreateGrid(_GridV, _dt, _Grid)
                'End If
                Call CreateGrid(_GridV, _dt, _Grid)
                HI.TL.HandlerControl.AddHandlerObj(_TabPage)
                Me.otabDetail.TabPages.Add(_TabPage)

            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CreateGrid(ByVal ogv As DevExpress.XtraGrid.Views.Grid.GridView, _dt As DataTable, ogc As DevExpress.XtraGrid.GridControl)
        Try
            With ogv
                .BeginInit()
                .Columns.Clear()
                Dim sFieldSum As String = "FNQuantity"
                For Each Col As DataColumn In _dt.Columns
                    Dim _BanCol As New DevExpress.XtraGrid.Columns.GridColumn
                    With _BanCol
                        .Caption = ogvdetail.Columns.ColumnByFieldName(Col.ColumnName.ToString).Caption.ToString
                        .FieldName = Col.ColumnName.ToString
                        .Name = Col.ColumnName.ToString
                        .OptionsColumn.AllowEdit = False
                        .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
                        .OptionsColumn.ReadOnly = True
                        Select Case Col.ColumnName.ToString
                            Case "FTQCSupDetailName", "FNDefectQty", "FTName"
                                .Visible = True
                                .Width = 200
                            Case "FNQuantity"
                                .Visible = True
                                .Width = 200
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "{0:n0}"
                            Case "FNQuantityPercent"
                                .Visible = True
                                .Width = 200
                                .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                .DisplayFormat.FormatString = "P"
                            Case Else
                                .Visible = False
                                .Width = 20
                        End Select
                    End With
                    .Columns.Add(_BanCol)
                Next
                For Each Str As String In sFieldSum.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                .OptionsView.ShowFooter = True
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
                .EndInit()
            End With
            ogc.DataSource = _dt
        Catch ex As Exception
        End Try
    End Sub

    Private totalSum As Integer = 0
    Private GrpSum As Integer = 0
    Private _RowHandleHold As Integer = 0
    Private Sub InitStartValue()
        totalSum = 0
        GrpSum = 0
        _RowHandleHold = 0
    End Sub

    Private Sub GridView1_CustomSummaryCalculate(sender As Object, e As DevExpress.Data.CustomSummaryEventArgs)
        If e.SummaryProcess = CustomSummaryProcess.Start Then
            InitStartValue()
        End If
        Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
            Case "FNQuantity"
                If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                    If e.IsTotalSummary Then
                        If _RowHandleHold <> e.RowHandle Then
                            If sender.GetRowCellValue(e.RowHandle, "FNQuantityPercent").ToString <> "" Then
                                totalSum = totalSum + Integer.Parse(Val(e.FieldValue.ToString))
                            End If
                            _RowHandleHold = e.RowHandle
                        End If
                    End If
                    e.TotalValue = totalSum
                End If
        End Select
    End Sub

    Private Sub GridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs)
        Try
            Select Case e.RowHandle
                Case 0, 1
                    e.Appearance.BackColor = Color.Salmon

                    'e.Appearance.BackColor2 = Color.SeaShell
                Case Else
                    'With CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).Columns
                    '    .ColumnByFieldName("FTName").AppearanceCell.BackColor
                    'End With
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

End Class