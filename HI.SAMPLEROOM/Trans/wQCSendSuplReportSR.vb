Imports System.Globalization

Public Class wQCSendSuplReportSR
    Private pGridData As DataTable
    Private pGridDataScn As DataTable
    Private _FormHeader As New List(Of HI.TL.DynamicForm)()
    Sub New()

        InitializeComponent()

    End Sub

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
    Private Sub wQCSendSuplReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
            Call CreateDatatable(Nothing, ogvdetail, Nothing, 0)
            Call GenerateGridBand(Nothing, ogvdetail, ogcdetail, Nothing, 0, Nothing)
            Call CreateDatatable(Nothing, ogvDetailScn, Nothing, 0)
            Call GenerateGridBand(Nothing, ogvDetailScn, ogcDetailScn, Nothing, 0, Nothing)

            Call CreateDatatableSum(Nothing, ogvEmSum, Nothing)
            Call GenerateGridBandSum(Nothing, ogvEmSum, ogcEmSum, Nothing)
            Call CreateDatatableSum(Nothing, ogvScnSum, Nothing)
            Call GenerateGridBandSum(Nothing, ogvScnSum, ogcScnSum, Nothing)
            Me.otabDetail.SelectedTabPageIndex = 0
            Me.otbSubEmbroidery.SelectedTabPageIndex = 0
            Me.otbSubScreen.SelectedTabPageIndex = 0
            'Me.ogvdetail.OptionsView.ShowAutoFilterRow = False
            'Me.ogvDetailScn.OptionsView.ShowAutoFilterRow = False

            Dim oSysLang As New ST.SysLanguage
            Try
                Call oSysLang.InsertObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.ToString.Trim, Me)
                Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.ToString.Trim, Me)
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Private Function _DataAll() As DataTable
        Dim _Cmd As String = ""
        Dim _odt As New DataTable

        Dim SFTDateTrans As String = "2000/01/01"
        Dim EFTDateTrans As String = "2999/01/01"
        Dim Langp As String = "2"
        Dim pOrderNo As String = ""
        Dim pOrderNoTo As String = "zzzzzz"
        Try

            If Me.SFTDateTrans.Text <> "" Then
                SFTDateTrans = HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text)
            End If
            If Me.EFTDateTrans.Text <> "" Then
                EFTDateTrans = HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text)
            End If

            Langp = ST.Lang.Language
            '*********************************************
            If Me.FTSMPOrderNo.Text <> "" Then
                pOrderNo = HI.UL.ULF.rpQuoted(Me.FTSMPOrderNo.Text)
            End If

            If Me.FTSMPOrderNoTo.Text <> "" Then
                pOrderNoTo = HI.UL.ULF.rpQuoted(Me.FTSMPOrderNoTo.Text)
            End If

            _Cmd = "SELECT * FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.FN_SenSuplDefect_Tracking_new"
            _Cmd &= vbCrLf & "('" & SFTDateTrans & "' ,'" & EFTDateTrans & "' ,'" & Langp & "','" & pOrderNo & "','" & pOrderNoTo & "'," & Integer.Parse(Me.FNHSysCmpId.Properties.Tag.ToString) & ")"
            _Cmd &= vbCrLf & "Order by FNSubQCType,FTQCSupDetailCode  ,FNHSysQCSuplDetailId desc"
            _odt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
            Return _odt
        Catch ex As Exception
            Return Nothing

        End Try
    End Function

    'Private Function _Data(ByVal _FNSendSuplType As String) As DataTable
    '    Try
    '        Dim _Cmd As String = ""

    '        If Me.SFTDateTrans.Text <> "" Then
    '            _Cmd = "Exec [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_SenSuplDefect_Tracking '" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "'"
    '        Else
    '            Dim SFTDateTrans As String = "01/01/2000"

    '            _Cmd = "Exec [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.SP_SenSuplDefect_Tracking '" & HI.UL.ULDate.ConvertEnDB(SFTDateTrans) & "'"
    '        End If
    '        If Me.EFTDateTrans.Text <> "" Then
    '            _Cmd &= " ,'" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "' "
    '        Else
    '            Dim EFTDateTrans As String = "01/01/2999"
    '            _Cmd &= ",'" & HI.UL.ULDate.ConvertEnDB(EFTDateTrans) & "'"
    '        End If

    '        _Cmd &= ",'" & HI.UL.ULF.rpQuoted(_FNSendSuplType) & "','" & ST.Lang.Language & "'"
    '        '*********************************************
    '        If Me.FTOrderNo.Text <> "" Then
    '            _Cmd &= ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
    '        Else
    '            Dim sql As String = "SELECT top 1 A.FTPORef, S.FTSeasonCode,  FTOrderNo, CASE WHEN ISDATE(A.FDOrderDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, A.FDOrderDate), 103) ELSE '' END AS FDOrderDate,                   "
    '            sql &= " (SELECT FTStyleCode FROM HITECH_MASTER.dbo.TMERMStyle AS L2 WHERE  (FNHSysStyleId = A.FNHSysStyleId)) AS FTStyleCode, ISNULL "
    '            sql &= " ((SELECT CASE WHEN ISDATE(L1.FDShipDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, L1.FDShipDate), 103) ELSE '' END AS FDShipDate  FROM      "
    '            sql &= " (SELECT X.FTOrderNo, MIN(Y.FDShipDate) AS FDShipDate  "
    '            sql &= " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + ".dbo.TMERTOrder AS X "
    '            sql &= " LEFT OUTER JOIN " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + ".dbo.TMERTOrderSub AS Y ON X.FTOrderNo = Y.FTOrderNo  GROUP BY X.FTOrderNo) AS L1  "
    '            sql &= " WHERE (FTOrderNo = A.FTOrderNo)), '') AS FDShipDate, FNOrderType, FTOrderBy "
    '            sql &= " FROM " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) + ".dbo.TMERTOrder AS A  WITH(NOLOCK)  "
    '            sql &= " LEFT OUTER JOIN " + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + ".dbo.TMERMSeason AS S WITH(NOLOCK) ON A.FNHSysSeasonId = S.FNHSysSeasonId ORDER BY FTOrderNo ASC"
    '            Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(sql, Conn.DB.DataBaseName.DB_SAMPLE)
    '            For Each R As DataRow In _dt.Rows
    '                _Cmd &= ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
    '            Next
    '        End If

    '        If Me.FTOrderNoTo.Text <> "" Then
    '            _Cmd &= ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
    '        Else
    '            Dim sql As String = "SELECT top 1 A.FTPORef, S.FTSeasonCode, FTOrderNo, CASE WHEN ISDATE(A.FDOrderDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, A.FDOrderDate), 103) ELSE '' END AS FDOrderDate,                   "
    '            sql &= "(SELECT FTStyleCode FROM HITECH_MASTER.dbo.TMERMStyle AS L2  WHERE (FNHSysStyleId = A.FNHSysStyleId)) AS FTStyleCode, ISNULL "
    '            sql &= "((SELECT CASE WHEN ISDATE(L1.FDShipDate) = 1 THEN CONVERT(VARCHAR(10), CONVERT(DATETIME, L1.FDShipDate), 103) ELSE '' END AS FDShipDate  FROM      "
    '            sql &= " (SELECT X.FTOrderNo, MIN(Y.FDShipDate) AS FDShipDate FROM HITECH_MERCHAN.dbo.TMERTOrder AS X LEFT OUTER JOIN  HITECH_MERCHAN.dbo.TMERTOrderSub AS Y ON X.FTOrderNo = Y.FTOrderNo  GROUP BY X.FTOrderNo) AS L1  "
    '            sql &= "WHERE (FTOrderNo = A.FTOrderNo)), '') AS FDShipDate, FNOrderType, FTOrderBy FROM HITECH_MERCHAN.dbo.TMERTOrder AS A WITH(NOLOCK)  "
    '            sql &= "LEFT OUTER JOIN [HITECH_MASTER]..TMERMSeason AS S WITH(NOLOCK) ON A.FNHSysSeasonId = S.FNHSysSeasonId ORDER BY FTOrderNo DESC"
    '            Dim _dt As DataTable = HI.Conn.SQLConn.GetDataTable(sql, Conn.DB.DataBaseName.DB_SAMPLE)
    '            Dim FTOrderNoTo As String
    '            For Each R As DataRow In _dt.Rows
    '                FTOrderNoTo = R!FTOrderNo
    '            Next
    '            _Cmd &= ",'" & HI.UL.ULF.rpQuoted(FTOrderNoTo) & "'"
    '        End If
    '        '***********************************************
    '        Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
    '    Catch ex As Exception
    '        Return Nothing

    '    End Try
    'End Function

    Private Function _DataGetDefectCode(ByVal _FNSendSuplType As String) As DataTable
        Try
            Dim _Cmd As String = ""
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd = "Select Case When Q.FNSubQCType = 0 Then convert(nvarchar(1),Q.FNSubQCType) + '. '+I.FTNameTH Else  convert(nvarchar(1),Q.FNSubQCType) + '. '+X.FTNameTH END AS FTQCGroup"
            Else
                _Cmd = "Select CASE WHEN Q.FNSubQCType = 0 Then convert(nvarchar(1),Q.FNSubQCType) + '. '+I.FTNameEN Else  convert(nvarchar(1),Q.FNSubQCType) + '. '+X.FTNameEN END AS FTQCGroup"
            End If
            _Cmd &= vbCrLf & " , Q.FTQCSupDetailCode"
            _Cmd &= vbCrLf & " From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQCSuplDetail AS Q WITH(NOLOCK) "
            _Cmd &= vbCrLf & " LEFT Outer Join (SELECT FNListIndex, FTNameTH, FTNameEN"
            _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH (NOLOCK)"
            _Cmd &= vbCrLf & " WHERE (FTListName = 'FNSendSuplType') ) AS I ON Q.FNSendSuplType = I.FNListIndex "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN (SELECT FNListIndex, FTNameTH, FTNameEN"
            _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH (NOLOCK)"
            _Cmd &= vbCrLf & " WHERE (FTListName = 'FNSubQCType')) AS X ON Q.FNSubQCType = X.FNListIndex"
            _Cmd &= vbCrLf & " Where Q.FNSendSuplType = '" & HI.UL.ULF.rpQuoted(_FNSendSuplType) & "'"
            _Cmd &= vbCrLf & " Order by Q.FNSubQCType ASC "
            Return HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub LoadData()
        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")
        Try
            Call CreateTab()
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub

    Private Sub CreateTab()
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            _Cmd = "SELECT  distinct   "
            _Cmd &= vbCrLf & " ISNULL(M.FNSendSuplType , S.FNSendSuplType) AS 'FNSendSuplType' "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", T.FTNameTH AS FTSendSuplTypeName "
            Else
                _Cmd &= vbCrLf & ", T.FTNameEN AS FTSendSuplTypeName"
            End If
            _Cmd &= vbCrLf & "FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect AS Q WITH (NOLOCK) "
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect_Detail AS D WITH (NOLOCK) ON Q.FTBarcodeSendSuplNo = D.FTBarcodeSendSuplNo "
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS S WITH (NOLOCK) ON S.FTBarcodeSendSuplNo = Q.FTBarcodeSendSuplNo "
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQCSuplDetail AS M WITH (NOLOCK) ON D.FNHSysQCSuplDetailId = M.FNHSysQCSuplDetailId"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN (SELECT FTListName, FNListIndex, FTNameTH, FTNameEN "
            _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK) "
            _Cmd &= vbCrLf & "WHERE (FTListName = N'FNSendSuplType')) AS T ON ISNULL(M.FNSendSuplType , S.FNSendSuplType) = T.FNListIndex "

            If Me.SFTDateTrans.Text <> "" And Me.EFTDateTrans.Text <> "" Then
                _Cmd &= vbCrLf & "Where  ((Q.FDInsDate >= '" & HI.UL.ULDate.ConvertEnDB(Me.SFTDateTrans.Text) & "' "

            Else
                Dim SFTDateTrans As String = "01/01/2000"
                _Cmd &= vbCrLf & "Where  ((Q.FDInsDate >= '" & HI.UL.ULDate.ConvertEnDB(SFTDateTrans) & "' "
            End If
            If Me.EFTDateTrans.Text <> "" Then
                _Cmd &= " and Q.FDInsDate <='" & HI.UL.ULDate.ConvertEnDB(Me.EFTDateTrans.Text) & "'"
            Else
                Dim EFTDateTrans As String = "01/01/2999"
                _Cmd &= "and Q.FDInsDate <='" & HI.UL.ULDate.ConvertEnDB(EFTDateTrans) & "' "
            End If

            _Cmd &= vbCrLf & " ))"
            _Cmd &= vbCrLf & "AND ISNULL(M.FNSendSuplType , S.FNSendSuplType) is not null "
            _Cmd &= vbCrLf & "AND ISNULL(M.FNSendSuplType , S.FNSendSuplType) <> '6' " ' Remove Semi Part #6


            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_SAMPLE)

            Dim _dtAll As DataTable = _DataAll()

            Me.otabDetail.TabPages.Clear()
            If _oDt.Rows.Count = 0 Then
                If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                    _oDt.Rows.Add(0, "ไม่มีข้อมูลตามเงื่อนไขที่กำหนด")
                Else
                    _oDt.Rows.Add(0, "There is no information according to the specified conditions.")
                End If
            End If

            For Each R As DataRow In _oDt.Rows
                'Obj new 
                Dim _TabPage As New DevExpress.XtraTab.XtraTabPage
                Dim _TabSub As New DevExpress.XtraTab.XtraTabControl
                Dim _TabPageSubHead As New DevExpress.XtraTab.XtraTabPage
                Dim _TabPageSubDetail As New DevExpress.XtraTab.XtraTabPage
                Dim _Grid As New DevExpress.XtraGrid.GridControl
                Dim _GridV As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
                Dim _GridDetail As New DevExpress.XtraGrid.GridControl
                Dim _GridVDetail As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridView
                'Obj new 

                'Tabmain
                With _TabPage
                    .Name = "otb" & R!FNSendSuplType.ToString
                    .Text = R!FTSendSuplTypeName.ToString
                    .Tag = "2|"
                End With
                'Tabmain

                'TabSub
                With _TabSub
                    .Name = "otbSub" & R!FNSendSuplType.ToString
                    .Dock = System.Windows.Forms.DockStyle.Fill
                    .TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {_TabPageSubHead, _TabPageSubDetail})
                End With

                With _TabPageSubHead
                    .Controls.Add(_Grid)
                    .Name = "otbSubHead" & R!FNSendSuplType.ToString
                    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                        .Text = "แบบสรุป"
                    Else
                        .Text = "Summary"
                    End If
                End With

                With _TabPageSubDetail
                    .Controls.Add(_GridDetail)
                    .Name = "otbSubDetail" & R!FNSendSuplType.ToString
                    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                        .Text = "แบบละเอียด"
                    Else
                        .Text = "Detail"
                    End If
                End With
                'TabSub

                'GridHead
                With _Grid
                    .Name = "ogcGSum" & R!FNSendSuplType.ToString
                    .Tag = "2|"
                End With

                With _GridV
                    .GridControl = _Grid
                    .Name = "ogvGSum" & R!FNSendSuplType.ToString
                    .OptionsPrint.AutoWidth = False
                    .OptionsCustomization.AllowQuickHideColumns = False
                    .OptionsPrint.PrintHeader = False
                    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                    .OptionsView.ShowColumnHeaders = False
                    .OptionsView.ShowGroupPanel = False
                    .OptionsView.ColumnAutoWidth = True
                    .OptionsView.ShowAutoFilterRow = True

                End With

                _Grid.BeginInit()
                _Grid.MainView = _GridV
                _Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {_GridV})
                _Grid.EndInit()
                _TabPageSubHead.Controls.Add(_Grid)

                _Grid.Dock = DockStyle.Fill
                'GridHead

                'GridDetail
                With _GridDetail
                    .Name = "ogcGSum" & R!FNSendSuplType.ToString
                    .Tag = "2|"
                End With
                With _GridVDetail
                    .GridControl = _GridDetail
                    .Name = "ogvGSum" & R!FNSendSuplType.ToString
                    .OptionsPrint.AutoWidth = False
                    .OptionsCustomization.AllowQuickHideColumns = False
                    .OptionsPrint.PrintHeader = False
                    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                    .OptionsView.ShowColumnHeaders = False
                    .OptionsView.ShowGroupPanel = False
                    .OptionsView.ColumnAutoWidth = False
                    .OptionsView.ShowAutoFilterRow = True
                End With

                _GridDetail.BeginInit()
                _GridDetail.MainView = _GridVDetail
                _GridDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {_GridVDetail})
                _GridDetail.EndInit()
                _TabPageSubDetail.Controls.Add(_GridDetail)
                _GridDetail.Dock = DockStyle.Fill

                Dim _GrandTotal As Integer
                Dim _dTDetail As DataTable

                Try
                    _dTDetail = _dtAll.Select("FNSendSuplType='" & R!FNSendSuplType.ToString & "'").CopyToDataTable
                Catch ex As Exception
                    _dTDetail = _dtAll.Clone
                End Try
                Dim _DefectCode_D As DataTable = _DataGetDefectCode(R!FNSendSuplType.ToString).Copy
                Call CreateDatatable(_dTDetail, _GridVDetail, pGridData, _GrandTotal)
                Call GenerateGridBand(_dTDetail, _GridVDetail, _GridDetail, pGridData, _GrandTotal, _DefectCode_D)

                Dim _oDtEmSum As DataTable = Nothing
                Call CreateDatatableSum(_dTDetail, _GridV, _oDtEmSum)
                Call GenerateGridBandSum(_dTDetail, _GridV, _Grid, _oDtEmSum)

                _TabPage.Controls.Add(_TabSub)
                HI.TL.HandlerControl.AddHandlerObj(_TabPage)
                Me.otabDetail.TabPages.Add(_TabPage)
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Function VerifyData() As Boolean
        Try
            If Me.SFTDateTrans.Text = "" And Me.FTSMPOrderNo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.SFTDateTrans_lbl.Text)
                Me.SFTDateTrans.Focus()
                Return False
            End If
            If Me.EFTDateTrans.Text = "" And Me.FTSMPOrderNoTo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.EFTDateTrans_lbl.Text)
                Me.EFTDateTrans.Focus()
                Return False
            End If
            If Me.SFTDateTrans.Text = "" And Me.FTSMPOrderNoTo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.SFTDateTrans_lbl.Text)
                Me.SFTDateTrans.Focus()
                Return False
            End If
            If Me.EFTDateTrans.Text = "" And Me.FTSMPOrderNo.Text = "" Then
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.EFTDateTrans_lbl.Text)
                Me.EFTDateTrans.Focus()
                Return False
            End If

            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub GenerateGridBand(ByVal EmpData As DataTable, ByVal ogv As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView, ByVal ogc As DevExpress.XtraGrid.GridControl,
                                 ByVal oDt As DataTable, ByVal _GrandTotal As Integer, ByVal oDtDefectCode As DataTable)
        Try
            Dim _Qry As String = ""
            Dim _GbandIndex As Integer = 0
            Dim _Str As String = ""
            _Str = "FTRcvSuplNo|FTBarcodeSendSuplNo|FTCmpCode|FTStyleCode|FTSeaSonCode|FTOrderNo|FNBunbleSeq|FTColorway|FTSizeBreakDown|FTPartName|FTBlock|FNBunbleQuantity|FNDefectQty|FNFacDefectQty|FNBalanceQty"

            Dim sFieldSum As String = "FNBunbleQuantity|FNDefectQty|FNFacDefectQty|FNBalanceQty"
            With ogv
                .BeginInit()
                For Each Str As String In _Str.Split("|")

                    Dim _gBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                    With _gBand
                        .AppearanceHeader.Options.UseTextOptions = True
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        If ogv.Name.ToString = "ogvdetail" Then
                            '   .Caption = Str
                        Else
                            .Caption = ogvdetail.Columns.ColumnByFieldName(Str).Caption.ToString
                            .Caption = ogvdetail.Columns.ColumnByFieldName(Str).Caption.ToString
                        End If

                        .Columns.Add(ogv.Columns.ColumnByFieldName(Str))
                        .Name = ogv.Name.ToString & "gb" & Str
                        .RowCount = 4
                        .Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
                    End With
                    .Bands.Add(_gBand)
                    _GbandIndex = _GbandIndex + 1
                Next

                If Not (EmpData Is Nothing) Then
                    Dim grp As List(Of String) = (EmpData.Select("FTQCGroup <> ''", "FTQCGroup").CopyToDataTable).AsEnumerable() _
                                                      .Select(Function(r) r.Field(Of String)("FTQCGroup")) _
                                                      .Distinct() _
                                                      .ToList()
                    Dim _StateCreateBand As Boolean = False
                    Dim _FTOrderProdNo As String = EmpData.Rows(0)!FTOrderProdNo.ToString
                    Dim _Code As String = "" : Dim _FNSumQCByGroup As String = "0" : Dim _FNSumQCByGroupSupl As String = "0" : Dim _FNSumQCByDetail As String = "0"
                    Dim _FNSumQCByDetailSupl As String = "0"
                    For Each Ind As String In grp
                        _StateCreateBand = False
                        Dim _GrbandType As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                        Dim _GrbandType2 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand

                        For Each R As DataRow In oDtDefectCode.Select("FTQCGroup='" & Ind & "'", "FTQCGroup ASC , FTQCSupDetailCode ASC")
                            If _Code <> R!FTQCSupDetailCode.ToString Then
                                _FNSumQCByDetailSupl = "0" : _FNSumQCByGroup = "0" : _FNSumQCByGroupSupl = "0" : _FNSumQCByDetail = "0"
                                For Each X As DataRow In EmpData.Select("FTQCGroup='" & R!FTQCGroup.ToString & "'") ' And FTQCSupDetailCode='" & R!FTQCSupDetailCode.ToString & "'
                                    _FNSumQCByGroup = X!FNSumQCByGroup.ToString
                                    _FNSumQCByGroupSupl = X!FNSumQCByGroupSupl.ToString
                                    Exit For
                                Next

                                For Each X As DataRow In EmpData.Select("FTQCGroup='" & R!FTQCGroup.ToString & "' And FTQCSupDetailCode='" & R!FTQCSupDetailCode.ToString & "'")
                                    _FNSumQCByDetail = X!FNSumQCByDetail.ToString
                                    _FNSumQCByDetailSupl = X!FNSumQCByDetailSupl.ToString
                                    Exit For
                                Next


                                If _StateCreateBand = False Then
                                    With _GrbandType
                                        .AppearanceHeader.Options.UseTextOptions = True
                                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                        .Caption = " O = " & _FNSumQCByGroup & " , S = " & _FNSumQCByGroupSupl & " "
                                        .Name = ogv.Name.ToString & "gbt" & R!FTQCGroup.ToString
                                        .RowCount = 1
                                    End With
                                    .Bands.Add(_GrbandType)
                                    With _GrbandType2
                                        .AppearanceHeader.Options.UseTextOptions = True
                                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                        .Caption = R!FTQCGroup.ToString
                                        .Name = ogv.Name.ToString & "gbt2" & R!FTQCGroup.ToString
                                        .RowCount = 1
                                    End With
                                    _GrbandType.Children.Add(_GrbandType2)
                                    _StateCreateBand = True
                                End If

                                Dim _GrbandCol1 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                                Dim _GrbandCol11 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                                Dim _GrbandCol2 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                                Dim _GrbandColSupl As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                                With _GrbandCol1
                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Caption = "O = " & _FNSumQCByDetail & " , S = " & _FNSumQCByDetailSupl & " "
                                    .Name = ogv.Name.ToString & "gbcol1" & R!FTQCSupDetailCode.ToString
                                    .RowCount = 1
                                End With
                                _GrbandType2.Children.Add(_GrbandCol1)
                                Dim nfi As NumberFormatInfo = New CultureInfo("en-US", False).NumberFormat
                                nfi.PercentDecimalDigits = 2
                                With _GrbandCol11
                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Caption = "" & ((Val(_FNSumQCByDetail)) / Val(_GrandTotal)).ToString("P", nfi)
                                    .Name = ogv.Name.ToString & "gbcol11" & R!FTQCSupDetailCode.ToString
                                    .RowCount = 1
                                End With
                                _GrbandCol1.Children.Add(_GrbandCol11)
                                With _GrbandCol2
                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Columns.Add(ogv.Columns.ColumnByFieldName("C" & R!FTQCSupDetailCode.ToString))
                                    .Caption = R!FTQCSupDetailCode.ToString & "(O)"
                                    .Name = ogv.Name.ToString & "gbcol2" & R!FTQCSupDetailCode.ToString
                                    .RowCount = 1
                                    .Width = 50
                                End With
                                _GrbandCol11.Children.Add(_GrbandCol2)
                                With _GrbandColSupl
                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Columns.Add(ogv.Columns.ColumnByFieldName("S" & R!FTQCSupDetailCode.ToString))
                                    .Caption = R!FTQCSupDetailCode.ToString & "(S)"
                                    .Name = ogv.Name.ToString & "gbcolSp" & R!FTQCSupDetailCode.ToString
                                    .RowCount = 1
                                    .Width = 50
                                End With
                                _GrbandCol11.Children.Add(_GrbandColSupl)
                            End If
                            _Code = R!FTQCSupDetailCode.ToString
                        Next
                    Next
                End If
                For Each Str As String In sFieldSum.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                .OptionsView.ShowFooter = True
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
                .EndInit()
            End With
            ogc.DataSource = oDt

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Try
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If VerifyData() Then
                Call LoadData()
                Me.otabpageEm.PageVisible = False
            End If
        Catch ex As Exception
        End Try
    End Sub


    Private Sub CreateDatatable(dt As DataTable, ByVal ogv As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView, ByRef oDtRef As DataTable, ByRef _GrandTotal As Integer)
        Dim _dt As New DataTable

        With _dt
            _dt.Columns.Add("FTBarcodeSendSuplNo", GetType(String))
            _dt.Columns.Add("FTRcvSuplNo", GetType(String))
            _dt.Columns.Add("FTCmpCode", GetType(String))
            _dt.Columns.Add("FTStyleCode", GetType(String))
            _dt.Columns.Add("FTSeaSonCode", GetType(String))
            _dt.Columns.Add("FTOrderNo", GetType(String))
            _dt.Columns.Add("FNBunbleSeq", GetType(Integer))
            _dt.Columns.Add("FTColorway", GetType(String))
            _dt.Columns.Add("FTSizeBreakDown", GetType(String))
            _dt.Columns.Add("FTPartName", GetType(String))
            _dt.Columns.Add("FTBlock", GetType(String))
            _dt.Columns.Add("FNQuantityNew", GetType(Double))
            _dt.Columns.Add("FNBunbleQuantity", GetType(Integer))
            _dt.Columns.Add("FNFacDefectQty", GetType(Integer))
            _dt.Columns.Add("FNDefectQty", GetType(Integer))
            _dt.Columns.Add("FNBalanceQty", GetType(Integer))
            '_dt.Columns.Add("FTNote", GetType(String))
            '_dt.Columns.Add("FTSubOrderNo", GetType(String))
        End With

        Dim _StrFilter As String = ""

        If Not (dt Is Nothing) Then
            For Each R As DataRow In dt.Select("FDRcvSuplDate<>''", "FNHSysQCSuplDetailId desc ") '"FNSubQCType asc ,FTQCSupDetailCode asc ,
                _StrFilter = "FTRcvSuplNo='" & R!FTRcvSuplNo.ToString & "' AND FTBarcodeSendSuplNo='" & R!FTBarcodeSendSuplNo.ToString & "'"
                _StrFilter &= " AND FTColorway='" & R!FTColorway.ToString & "' AND FTSizeBreakDown='" & R!FTSizeBreakDown.ToString & "'"
                _StrFilter &= " AND FTPartName='" & R!FTPartName.ToString & "' AND FNBunbleSeq=" & R!FNBunbleSeq.ToString

                'HI.UL.ULF.rpQuoted(R!FTSubOrderNo.ToString),
                If _dt.Select(_StrFilter).Length <= 0 Then
                    _dt.Rows.Add(HI.UL.ULF.rpQuoted(R!FTBarcodeSendSuplNo.ToString), HI.UL.ULF.rpQuoted(R!FTRcvSuplNo.ToString),
                                 HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString), HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString), HI.UL.ULF.rpQuoted(R!FTSeaSonCode.ToString),
                                 HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString),
                                 CInt("0" & R!FNBunbleSeq.ToString),
                                 HI.UL.ULF.rpQuoted(R!FTColorway.ToString), HI.UL.ULF.rpQuoted(R!FTSizeBreakDown.ToString), HI.UL.ULF.rpQuoted(R!FTPartName.ToString),
                                 HI.UL.ULF.rpQuoted(R!FTBlock.ToString),
                                 CDbl("0" & R!FNQuantityNew.ToString), CInt("0" & R!FNBunbleQuantity.ToString),
                                 CInt("0" & R!FNFacDefectQty.ToString), CInt("0" & R!FNDefectQty.ToString),
                                 (CInt("0" & R!FNBunbleQuantity.ToString) - (CInt("0" & R!FNFacDefectQty.ToString) + CInt("0" & R!FNDefectQty.ToString))))
                End If
                If _dt.Columns.IndexOf("C" & R!FTQCSupDetailCode.ToString) < 0 Then
                    _dt.Columns.Add("C" & R!FTQCSupDetailCode.ToString, GetType(Integer))
                End If
                If Val(R!FNDefectCount.ToString) > 0 Then
                    For Each Rx As DataRow In _dt.Select(_StrFilter)
                        Rx.Item("C" & R!FTQCSupDetailCode.ToString) = Val(Rx.Item("C" & R!FTQCSupDetailCode.ToString).ToString) + Val(R!FNDefectCount.ToString)
                    Next
                End If
                If _dt.Columns.IndexOf("S" & R!FTQCSupDetailCode.ToString) < 0 Then
                    _dt.Columns.Add("S" & R!FTQCSupDetailCode.ToString, GetType(Integer))
                End If
                If Val(R!FNDefectQtySupl.ToString) > 0 Then
                    For Each Rx As DataRow In _dt.Select(_StrFilter)
                        Rx.Item("S" & R!FTQCSupDetailCode.ToString) = Val(Rx.Item("S" & R!FTQCSupDetailCode.ToString).ToString) + Val(R!FNDefectQtySupl.ToString)
                    Next
                End If

            Next
        End If

        With ogv
            .BeginInit()
            .Columns.Clear()
            .Bands.Clear()
            For Each Col As DataColumn In _dt.Columns
                Dim _BanCol As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                With _BanCol
                    .Caption = Col.ColumnName.ToString
                    .FieldName = Col.ColumnName.ToString
                    .Name = Col.ColumnName.ToString
                    .OptionsColumn.AllowEdit = False
                    .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
                    .OptionsColumn.ReadOnly = True
                    .Visible = True
                    .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                    '"FTSubOrderNo",
                    Select Case Col.ColumnName.ToString
                        Case "FTPartName", "FTSizeBreakDown", "FTColorway", "FTStyleCode", "FTOrderProdNo", "FNSendSuplState", "FTRcvSuplBy" _
                            , "FDRcvSuplDate", "FTOrderNo", "FTBlock"
                            .Width = 60
                        Case "FTBarcodeSendSuplNo", "FTRcvSuplNo"
                            .Width = 120
                        Case "FTSuplName"
                            .Width = 250
                        Case "FNQuantityNew"
                            .Width = 50
                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .DisplayFormat.FormatString = "{0:n2}"
                        Case "FNBunbleSeq", "FNDefect", "FNHSysCmpId", "FNHSysSuplId"
                            .Width = 50

                        Case "FNFacDefectQty", "FNDefectQty", "FNBalanceQty"
                            .Width = 80
                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .DisplayFormat.FormatString = "{0:n0}"
                        Case "FNSumByDocNo"
                            .Visible = False
                        Case Else
                            If Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 1) = "C" Then
                                .Width = 50
                            ElseIf Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 1) = "S" Then
                                .Width = 50
                            Else
                                If Col.ColumnName.ToString = "FTQCSupDetailCode" Then
                                    .Width = 150
                                Else
                                    .Width = 80
                                End If
                            End If
                    End Select
                End With
                .Columns.Add(_BanCol)
            Next
            .EndInit()
        End With
        Dim _Value As Integer
        Dim _FTRcvSuplNo As String = ""
        For Each R As DataRow In _dt.Select("", "FTRcvSuplNo")
            If _FTRcvSuplNo <> R!FTRcvSuplNo.ToString Then
                _Value += +Val(R!FNQuantityNew.ToString)
            End If
            _FTRcvSuplNo = R!FTRcvSuplNo.ToString
        Next
        _GrandTotal = _Value
        oDtRef = _dt
    End Sub

    Private Sub CreateDatatableSum(dt As DataTable, ByVal obgv As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView, ByRef oDtR As DataTable)
        Dim _dt As New DataTable
        Dim _temp As New DataTable
        Dim _SumFNQuantity As Integer = 0
        Dim _SumFNSuplDefectQty As Integer = 0
        Dim _SumFNDefectQty As Integer = 0
        Dim _SumBalance As Integer = 0
        Dim _SumFNHitDefectQty As Integer = 0
        With _dt
            _dt.Columns.Add("FTRcvSuplNo", GetType(String))
            _dt.Columns.Add("FDRcvSuplDate", GetType(String))
            _dt.Columns.Add("FTSuplName", GetType(String))
            _dt.Columns.Add("FTOrderNo", GetType(String))
            _dt.Columns.Add("FTStyleCode", GetType(String))
            _dt.Columns.Add("FNQuantity", GetType(Double))
            _dt.Columns.Add("FNSuplDefectQty", GetType(Double))
            _dt.Columns.Add("FNHITDefectQty", GetType(Double))
            _dt.Columns.Add("FNBalanceQty", GetType(Integer))
            '_dt.Columns.Add("FTPORef", GetType(String))
            '_dt.Columns.Add("FTBlock", GetType(String))
            '_dt.Columns.Add("FTPartName", GetType(String))

        End With
        With _temp
            .Columns.Add("FTBarcodeSendSuplNo", GetType(String))
            .Columns.Add("FTRcvSuplNo", GetType(String))
            .Columns.Add("FNBunbleSeq", GetType(Integer))
            .Columns.Add("FTColorway", GetType(String))
            .Columns.Add("FTSizeBreakDown", GetType(String))
            .Columns.Add("FTPartName", GetType(String))
            '.Columns.Add("FTNote", GetType(String))
            '.Columns.Add("FNQuantityNew", GetType(Double))
            '.Columns.Add("FNBunbleQuantity", GetType(Integer))
            '.Columns.Add("FNFacDefectQty", GetType(Integer))
            '.Columns.Add("FNDefectQty", GetType(Integer))
            '.Columns.Add("FNBalanceQty", GetType(Integer))
        End With




        Dim _StrFilter As String = ""
        If Not (dt Is Nothing) Then
            For Each R As DataRow In dt.Select("FDRcvSuplDate<>''", "FNSubQCType,FTQCSupDetailCode")
                _StrFilter = "FTRcvSuplNo='" & R!FTRcvSuplNo.ToString & "' AND  FDRcvSuplDate='" & R!FDRcvSuplDate.ToString & "'"
                Dim _FilterByJoker As String = ""
                For Each K As DataRow In dt.Select(_StrFilter)

                    _FilterByJoker = "FTRcvSuplNo='" & K!FTRcvSuplNo.ToString & "' AND FTBarcodeSendSuplNo='" & K!FTBarcodeSendSuplNo.ToString & "'"
                    _FilterByJoker &= " AND FTColorway='" & K!FTColorway.ToString & "' AND FTSizeBreakDown='" & K!FTSizeBreakDown.ToString & "'"
                    _FilterByJoker &= " AND FTPartName='" & K!FTPartName.ToString & "' AND FNBunbleSeq=" & K!FNBunbleSeq.ToString


                    If _temp.Select(_FilterByJoker).Length <= 0 Then
                        _SumFNQuantity += Val(K!FNBunbleQuantity.ToString)
                        _SumFNSuplDefectQty += Val(K!FNSuplDefectQty.ToString)
                        _SumFNDefectQty += Val(K!FNHITDefectQty.ToString)
                        _temp.Rows.Add(K!FTBarcodeSendSuplNo.ToString, K!FTRcvSuplNo.ToString, K!FNBunbleSeq.ToString, K!FTColorway.ToString, K!FTSizeBreakDown.ToString, K!FTPartName.ToString)
                        _SumBalance += (CInt("0" & R!FNBunbleQuantity.ToString) - (CInt("0" & R!FNFacDefectQty.ToString) + CInt("0" & R!FNDefectQty.ToString)))
                        _SumFNHitDefectQty += Val(K!FNDefectQty.ToString)

                    End If
                Next

                If _dt.Select(_StrFilter).Length <= 0 Then
                    _dt.Rows.Add(R!FTRcvSuplNo.ToString, R!FDRcvSuplDate.ToString, R!FTSuplName.ToString, R!FTOrderNo.ToString, R!FTStyleCode.ToString,
                     _SumFNQuantity, _SumFNHitDefectQty,
                     Double.Parse("0" & R!FNSuplDefectQty.ToString), _SumFNQuantity - (_SumFNHitDefectQty + Double.Parse("0" & R!FNSuplDefectQty.ToString)))
                End If
                _SumBalance = 0
                _SumFNDefectQty = 0
                _SumFNQuantity = 0
                _SumFNSuplDefectQty = 0
                _SumFNHitDefectQty = 0
            Next
        End If


        With obgv
            .BeginInit()
            .Columns.Clear()
            .Bands.Clear()

            For Each Col As DataColumn In _dt.Columns

                Dim _BanCol As New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
                With _BanCol
                    .Caption = Col.ColumnName.ToString
                    .FieldName = Col.ColumnName.ToString
                    .Name = Col.ColumnName.ToString
                    .OptionsColumn.AllowEdit = False
                    .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
                    .OptionsColumn.ReadOnly = True
                    .Visible = True
                    .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains

                    Select Case Col.ColumnName.ToString
                        Case "FTSuplName", "FTPartName", "FTSizeBreakDown", "FTColorway", "FTStyleCode", "FTOrderProdNo", "FTBarcodeSendSuplNo", "FNSendSuplState", "FTRcvSuplBy" _
                            , "FDRcvSuplDate", "FTOrderNo"
                            .Width = 120

                        Case "FTRcvSuplNo"
                            .Width = 120
                        Case "FNQuantity", "FNHITDefectQty", "FNSuplDefectQty", "FNBalanceQty"
                            .Width = 80
                            .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                            .DisplayFormat.FormatString = "{0:n0}"
                        Case "FNBunbleSeq", "FNDefect", "FNHSysCmpId", "FNHSysSuplId"
                            .Width = 50

                        Case "FNSumByDocNo"
                            .Visible = False
                        Case Else
                            If Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 1) = "C" Then
                                .Width = 50
                            ElseIf Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 1) = "S" Then
                                .Width = 50
                            Else
                                If Col.ColumnName.ToString = "FTQCSupDetailCode" Then
                                    .Width = 150
                                Else
                                    .Width = 80
                                End If
                            End If

                    End Select
                End With

                .Columns.Add(_BanCol)
            Next
            .EndInit()
        End With

        oDtR = _dt
        _temp.Dispose()
    End Sub


    Private Sub GenerateGridBandSum(ByVal EmpData As DataTable, ByVal ogv As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView, ByVal ogc As DevExpress.XtraGrid.GridControl, ByVal _oDt As DataTable)
        Try
            Dim _Qry As String = ""
            Dim _GbandIndex As Integer = 0
            Dim _Str As String = ""
            _Str = "FTRcvSuplNo|FDRcvSuplDate|FTSuplName|FTOrderNo|FTStyleCode|FNQuantity|FNSuplDefectQty|FNHITDefectQty|FNBalanceQty"

            Dim sFieldSum As String = "FNQuantity|FNSuplDefectQty|FNHITDefectQty|FNBalanceQty"
            With ogv
                .BeginInit()
                For Each Str As String In _Str.Split("|")

                    Dim _gBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                    With _gBand
                        .AppearanceHeader.Options.UseTextOptions = True
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        If ogv.Name.ToString = "ogvEmSum" Then
                            '  .Caption = Str
                        Else
                            .Caption = ogvEmSum.Columns.ColumnByFieldName(Str).Caption.ToString
                        End If
                        .Columns.Add(ogv.Columns.ColumnByFieldName(Str))
                        .Name = ogv.Name.ToString & "gb" & Str
                        .RowCount = 4
                        .Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
                    End With
                    .Bands.Add(_gBand)
                    _GbandIndex = _GbandIndex + 1
                Next

                For Each Str As String In sFieldSum.Split("|")
                    If Str <> "" Then
                        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                    End If
                Next

                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                .OptionsView.ShowFooter = True
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
                .EndInit()
            End With
            ogc.DataSource = _oDt
        Catch ex As Exception
        End Try
    End Sub


    Private Sub ocmclear_Click_1(sender As Object, e As EventArgs) Handles ocmclear.Click
        Me.otabDetail.TabPages.Clear()
        Me.FormRefresh()
    End Sub

    Private Sub FormRefresh()
        HI.TL.HandlerControl.ClearControl(Me)

        For Each Obj As Object In Me.Controls.Find(Me.MainKey, True)
            Select Case HI.ENM.Control.GeTypeControl(Obj)
                Case ENM.Control.ControlType.ButtonEdit
                    With CType(Obj, DevExpress.XtraEditors.ButtonEdit)
                        .Focus()
                    End With
            End Select
        Next
    End Sub

    Public ReadOnly Property MainKey As String
        Get
            Return _FormHeader(0).MainKey
        End Get
    End Property
End Class