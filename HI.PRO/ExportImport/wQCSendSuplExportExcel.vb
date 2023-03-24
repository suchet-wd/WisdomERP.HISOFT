Imports System.Globalization
Imports System.Data
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing

Public Class wQCSendSuplExportExcel
    Private pGridData As DataTable
    Private pGridDataScn As DataTable
    Private pDataState As DataTable
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
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
            Call CreateTabDynamic(Nothing)
            Dim oSysLang As New ST.SysLanguage
            Try
                Call oSysLang.InsertObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.ToString.Trim, Me)
                Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.ToString.Trim, Me)
            Catch ex As Exception
            End Try
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadData()
        Dim _Spls As New HI.TL.SplashScreen("Loading Data... Please Wait... ")
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            _Cmd = "SELECT      Isnull(S.FTStateSendExcel,0) AS FTStateSendExcel,  S.FTSendSuplNo,CASE WHEN ISDate(S.FDSendSuplDate)=1 Then convert(nvarchar(10),convert(datetime,S.FDSendSuplDate),103) ELSE '' END AS  FDSendSuplDate, T.FTStyleCode"
            _Cmd &= vbCrLf & ", O.FTPORef, O.FTOrderNo, Y.FNTableNo, L.FNBunbleSeq, L.FTColorway, L.FTSizeBreakDown  "
            _Cmd &= vbCrLf & "  , P.FTSuplCode, SUM(E.FNQuantity) AS FNQuantity, C.FNSendSuplType, Q.FNSubQCType, Q.FTQCSupDetailCode, Q.FNHSysQCSuplDetailId "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", T.FTStyleNameTH AS FTStyleName , M.FTPartNameTH AS FTPartName , P.FTSuplNameEN as FTSuplName , Q.FTQCSupDetailNameTH  as FTQCSupDetailName "
                _Cmd &= vbCrLf & ", X.FTNameTH AS FTSubQCType  , CASE WHEN X.FTNameTH ='-' THEN  convert(nvarchar(2),Z.FNListIndex)+'.'+Z.FTNameTH ELSE convert(nvarchar(2),X.FNListIndex)+'.'+X.FTNameTH END AS FTSendSuplTypeName "
            Else
                _Cmd &= vbCrLf & ", T.FTStyleNameEN AS FTStyleName ,M.FTPartNameEN AS  FTPartName , P.FTSuplNameTH as FTSuplName ,Q.FTQCSupDetailNameEN as FTQCSupDetailName "
                _Cmd &= vbCrLf & ", X.FTNameEN AS FTSubQCType ,CASE WHEN X.FTNameEN ='-' THEN convert(nvarchar(2), Z.FNListIndex)+'.'+Z.FTNameEN ELSE convert(nvarchar(2),X.FNListIndex)+'.'+X.FTNameEN END AS FTSendSuplTypeName"
            End If
            _Cmd &= vbCrLf & " FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl AS S WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPRODTSendSupl_Barcode AS B WITH (NOLOCK) ON S.FTSendSuplNo = B.FTSendSuplNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS P WITH (NOLOCK) ON S.FNHSysSuplId = P.FNHSysSuplId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS C WITH (NOLOCK) ON B.FTBarcodeSendSuplNo = C.FTBarcodeSendSuplNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS D WITH (NOLOCK) ON C.FTOrderProdNo = D.FTOrderProdNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON D.FTOrderNo = O.FTOrderNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS T WITH (NOLOCK) ON O.FNHSysStyleId = T.FNHSysStyleId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS L WITH (NOLOCK) ON C.FTBarcodeBundleNo = L.FTBarcodeBundleNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS E WITH (NOLOCK) ON L.FTBarcodeBundleNo = E.FTBarcodeBundleNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS Y WITH (NOLOCK) ON E.FTLayCutNo = Y.FTLayCutNo and C.FTOrderProdNo = Y.FTOrderProdNo  LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS M WITH (NOLOCK) ON C.FNHSysPartId = M.FNHSysPartId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " (Select * From [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQCSuplDetail WITH(NOLOCK) WHERE Isnull(FTStateActive,'0') ='1' ) AS Q   ON C.FNSendSuplType = Q.FNSendSuplType LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "     (SELECT        FNListIndex, FTNameTH, FTNameEN"
            _Cmd &= vbCrLf & "       FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH (NOLOCK)"
            _Cmd &= vbCrLf & "       WHERE        (FTListName = 'FNSendSuplType')) AS Z ON Q.FNSendSuplType = Z.FNListIndex LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "    (SELECT        FNListIndex, FTNameTH, FTNameEN"
            _Cmd &= vbCrLf & "      FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS HSysListData_1 WITH (NOLOCK)"
            _Cmd &= vbCrLf & "     WHERE        (FTListName = 'FNSubQCType')) AS X ON  Q.FNSubQCType = X.FNListIndex " 'OR Q.FNSubQCType =0"
            _Cmd &= vbCrLf & " WHERE        (S.FTSendSuplNo <>'')"
            If Me.FNHSysStyleId.Text <> "" Then
                _Cmd &= vbCrLf & "  AND T.FTStyleCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysStyleId.Text) & "'"
            End If
            If Me.FTOrderNo.Text <> "" Then
                _Cmd &= vbCrLf & " AND O.FTOrderNo >='" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _Cmd &= vbCrLf & " AND O.FTOrderNo <='" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If
            If Me.FTStartSendSupl.Text <> "" Then
                _Cmd &= vbCrLf & " AND S.FDSendSuplDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTStartSendSupl.Text) & "'"
            End If
            If Me.FTEndSendSupl.Text <> "" Then
                _Cmd &= vbCrLf & " AND S.FDSendSuplDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTEndSendSupl.Text) & "'"
            End If
            If Me.FNHSysSuplId.Text <> "" Then
                _Cmd &= vbCrLf & " AND P.FTSuplCode ='" & HI.UL.ULF.rpQuoted(Me.FNHSysSuplId.Text) & "'"
            End If
            If Me.FDDateSendFinish.Text <> "" Then
                _Cmd &= vbCrLf & " AND S.FDDateSendFinish >= '" & UL.ULDate.ConvertEnDB(Me.FDDateSendFinish.Text) & "'"
            End If
            If Me.FDDateSendFinishTo.Text <> "" Then
                _Cmd &= vbCrLf & " AND S.FDDateSendFinish <= '" & UL.ULDate.ConvertEnDB(Me.FDDateSendFinishTo.Text) & "'"
            End If
            If (Me.ockIsnotExportExcel.Checked) Then
                _Cmd &= vbCrLf & " AND Isnull(S.FTStateSendExcel,0) ='0'"
            End If
            _Cmd &= vbCrLf & " AND Isnull(S.FTStateScanSendFinish,0) ='1'"

            _Cmd &= vbCrLf & " GROUP BY Isnull(S.FTStateSendExcel,0) , S.FTSendSuplNo, S.FDSendSuplDate, T.FTStyleCode, T.FTStyleNameTH, T.FTStyleNameEN, O.FTPORef, O.FTOrderNo, Y.FNTableNo, L.FNBunbleSeq, L.FTColorway, L.FTSizeBreakDown, M.FTPartNameTH, "
            _Cmd &= vbCrLf & "  M.FTPartNameEN, P.FTSuplCode, P.FTSuplNameEN, P.FTSuplNameTH, C.FNSendSuplType, Q.FNSendSuplType, Q.FNSubQCType, Q.FTQCSupDetailCode, Q.FTQCSupDetailNameTH, Q.FTQCSupDetailNameEN,"
            _Cmd &= vbCrLf & "  X.FTNameTH, X.FTNameEN, Z.FTNameTH, Z.FTNameEN,Z.FNListIndex ,X.FNListIndex, Q.FNHSysQCSuplDetailId"

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Call CreateTabDynamic(_oDt)
            pDataState = _oDt
            'Dim oSysLang As New ST.SysLanguage
            'Try
            '    Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.ToString.Trim, Me)
            '    'Call oSysLang.InsertObjectLanguage(HI.ST.SysInfo.ModuleName, Me.Name.ToString.Trim, Me)

            'Catch ex As Exception
            'End Try

            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub
    Private Function VerrifyData() As Boolean
        Try
            Dim _State As Boolean = False
            If Me.FNHSysStyleId.Text <> "" Then
                _State = True
            End If
            If Me.FTOrderNo.Text <> "" Then
                _State = True
            End If
            If Me.FTOrderNoTo.Text <> "" Then
                _State = True
            End If
            If Me.FTStartSendSupl.Text <> "" Then
                _State = True
            End If
            If Me.FTEndSendSupl.Text <> "" Then
                _State = True
            End If
            If Me.FNHSysSuplId.Text <> "" Then
                _State = True
            End If
            If Not _State Then
                HI.MG.ShowMsg.mInfo("กรุณาเลือกข้อมูล", 1508111002, Me.Text)
                Me.FNHSysSuplId.Focus()
            End If
            Return _State
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub GenerateGridBand(ByVal EmpData As DataTable, ByVal ogv As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView, ByVal ogc As DevExpress.XtraGrid.GridControl, ByVal oDt As DataTable, ByVal _GrandTotal As Integer)
        Try
            Dim _Qry As String = ""
            Dim _GbandIndex As Integer = 0
            Dim _Str As String = ""
            _Str = "FTSendSuplNo|FDSendSuplDate|FTStyleCode|FTPORef|FTOrderNo|FNTableNo|FNBunbleSeq|FTColorway|FTSizeBreakDown|FTPartName|FTSuplCode|FNQuantity"
            '_Str &= "|FTStyleName |FTSuplName"

            Dim sFieldSum As String = "FNQuantity"
            With ogv
                .BeginInit()
                For Each Str As String In _Str.Split("|")

                    Dim _gBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                    With _gBand
                        .AppearanceHeader.Options.UseTextOptions = True
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        '.Caption = ogv.Columns.ColumnByFieldName(Str).Caption.ToString
                        .Caption = ogvdetail.Columns.ColumnByFieldName(Str).Caption.ToString
                        .Columns.Add(ogv.Columns.ColumnByFieldName(Str))
                        .Name = ogv.Name.ToString & "gb" & Str
                        .RowCount = 4
                        .Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
                    End With
                    .Bands.Add(_gBand)
                    _GbandIndex = _GbandIndex + 1
                Next
                .EndInit()

                'For Each colg As DevExpress.XtraGrid.Views.BandedGrid.GridBand In .Bands
                '    Dim _fieldName As String = Replace(colg.Name.ToString, ogv.Name.ToString & "gb", "")
                '    colg.Caption = ogvdetail.Columns.ColumnByFieldName(_fieldName).Caption.ToString
                'Next

                'For Each Col As DevExpress.XtraGrid.Columns.GridColumn In ogvdetail.Columns
                '    Dim _gBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand

                '    With _gBand
                '        .AppearanceHeader.Options.UseTextOptions = True
                '        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                '        .Caption = Col.Caption.ToString
                '        .Columns.Add(ogv.Columns.ColumnByFieldName(Col.FieldName.ToString))
                '        .Name = ogv.Name.ToString & "gb" & Col.FieldName.ToString
                '        .RowCount = 4
                '        Select Case Col.FieldName.ToString
                '            Case "FNFacDefectQty", "FNCheckBy"
                '                .Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
                '            Case Else
                '                .Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
                '        End Select

                '    End With
                '    '.Bands.Add(_gBand)
                '    '_GbandIndex = _GbandIndex + 1
                'Next

                .BeginInit()
                If Not (EmpData Is Nothing) Then
                    Dim grp As List(Of String) = (EmpData.Select("FTSendSuplTypeName <> ''", "FTSendSuplTypeName").CopyToDataTable).AsEnumerable() _
                                                      .Select(Function(r) r.Field(Of String)("FTSendSuplTypeName")) _
                                                      .Distinct() _
                                                      .ToList()
                    Dim _StateCreateBand As Boolean = False


                    Dim _Code As String = ""
                    For Each Ind As String In grp
                        _StateCreateBand = False
                        Dim _GrbandType As New DevExpress.XtraGrid.Views.BandedGrid.GridBand

                        For Each R As DataRow In EmpData.Select("FTSendSuplTypeName='" & Ind & "'", "FTSendSuplTypeName , FNSendSuplType , FNSubQCType , FTQCSupDetailCode")
                            If _Code <> R!FTQCSupDetailCode.ToString Then
                                If _StateCreateBand = False Then

                                    With _GrbandType
                                        .AppearanceHeader.Options.UseTextOptions = True
                                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                        .Caption = R!FTSendSuplTypeName.ToString
                                        .Name = ogv.Name.ToString & "gbt" & R!FTSendSuplTypeName.ToString
                                        .RowCount = 1
                                    End With
                                    .Bands.Add(_GrbandType)
                                    _StateCreateBand = True
                                End If
                                Dim _GrbandCol1 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                                Dim _GrbandCol11 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                                Dim _GrbandCol12 As New DevExpress.XtraGrid.Views.BandedGrid.GridBand

                                With _GrbandCol1
                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Caption = R!FTQCSupDetailCode.ToString
                                    .Name = ogv.Name.ToString & "gbcol1" & R!FTQCSupDetailCode.ToString
                                    .RowCount = 1
                                End With
                                _GrbandType.Children.Add(_GrbandCol1)

                                With _GrbandCol11
                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Caption = R!FTQCSupDetailName.ToString
                                    .Name = ogv.Name.ToString & "gbcol11" & R!FTQCSupDetailName.ToString
                                    .RowCount = 1
                                End With
                                _GrbandCol1.Children.Add(_GrbandCol11)

                                'With _GrbandCol12
                                '    .AppearanceHeader.Options.UseTextOptions = True
                                '    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                '    .Caption = R!FNHSysQCSuplDetailId.ToString
                                '    .Name = ogv.Name.ToString & "gbcol12" & R!FNHSysQCSuplDetailId.ToString
                                '    .RowCount = 1
                                'End With
                                '_GrbandCol11.Children.Add(_GrbandCol12)

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


                _Str = "FNFacDefectQty|FNCheckBy"
                For Each Str As String In _Str.Split("|")

                    Dim _gBand As New DevExpress.XtraGrid.Views.BandedGrid.GridBand
                    With _gBand
                        .AppearanceHeader.Options.UseTextOptions = True
                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                        '.Caption = Str
                        .Caption = ogvdetail.Columns.ColumnByFieldName(Str).Caption.ToString
                        .Columns.Add(ogv.Columns.ColumnByFieldName(Str))
                        .Name = ogv.Name.ToString & "gb" & Str
                        .RowCount = 4
                        .Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right
                    End With
                    .Bands.Add(_gBand)
                    _GbandIndex = _GbandIndex + 1
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
            If VerrifyData() Then
                Call LoadData()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CreateDatatable(dt As DataTable, ByVal ogv As DevExpress.XtraGrid.Views.BandedGrid.BandedGridView, ByRef oDtRef As DataTable, ByRef _GrandTotal As Integer)
        Dim _dt As New DataTable

        With _dt
            _dt.Columns.Add("FTStateSendExcel", GetType(String))
            _dt.Columns.Add("FTSendSuplNo", GetType(String))
            _dt.Columns.Add("FDSendSuplDate", GetType(String))
            _dt.Columns.Add("FTStyleCode", GetType(String))
            '_dt.Columns.Add("FTStyleName", GetType(String))
            _dt.Columns.Add("FTPORef", GetType(String))
            _dt.Columns.Add("FTOrderNo", GetType(String))
            _dt.Columns.Add("FNTableNo", GetType(Integer))
            _dt.Columns.Add("FNBunbleSeq", GetType(Integer))
            _dt.Columns.Add("FTColorway", GetType(String))
            _dt.Columns.Add("FTSizeBreakDown", GetType(String))
            _dt.Columns.Add("FTPartName", GetType(String))
            _dt.Columns.Add("FTSuplCode", GetType(String))
            '_dt.Columns.Add("FTSuplName", GetType(String))
            _dt.Columns.Add("FNQuantity", GetType(Double))
            '_dt.Columns.Add("FNHSysQCSuplDetailId", GetType(Integer))
        End With

        Dim _StrFilter As String = ""

        If Not (dt Is Nothing) Then
            For Each R As DataRow In dt.Select("FTSendSuplNo<>''", "FDSendSuplDate ASC ,FTSendSuplNo ASC , FTSuplCode ASC ")
                _StrFilter = "FTSendSuplNo='" & R!FTSendSuplNo.ToString & "' AND FDSendSuplDate='" & R!FDSendSuplDate.ToString & "'"
                _StrFilter &= " AND FTStyleCode='" & R!FTStyleCode.ToString & "'" ' AND FTStyleName='" & R!FTStyleName.ToString & "'"
                _StrFilter &= " AND FTPORef='" & R!FTPORef.ToString & "' AND FTOrderNo='" & R!FTOrderNo.ToString & "'"
                _StrFilter &= " AND FNTableNo=" & R!FNTableNo.ToString & "  AND FNBunbleSeq=" & Integer.Parse("0" & R!FNBunbleSeq.ToString)
                _StrFilter &= " AND FTColorway='" & R!FTColorway.ToString & "' AND FTSizeBreakDown='" & R!FTSizeBreakDown.ToString & "'"
                _StrFilter &= " AND FTPartName='" & R!FTPartName.ToString & "' AND FTSuplCode='" & R!FTSuplCode.ToString & "'"
                _StrFilter &= " AND FNQuantity=" & Double.Parse("0" & R!FNQuantity.ToString) ' AND FTSuplName='" & R!FTSuplName.ToString & "'
                Try
                    If _dt.Select(_StrFilter).Length <= 0 Then
                        _dt.Rows.Add(R!FTStateSendExcel.ToString, R!FTSendSuplNo.ToString, R!FDSendSuplDate.ToString, R!FTStyleCode.ToString,
                                      R!FTPORef.ToString, R!FTOrderNo.ToString, R!FNTableNo.ToString, R!FNBunbleSeq.ToString, R!FTColorway.ToString,
                                      R!FTSizeBreakDown.ToString, R!FTPartName.ToString, R!FTSuplCode.ToString, R!FNQuantity.ToString)

                    End If
                    If _dt.Columns.IndexOf("C" & R!FTQCSupDetailCode.ToString) < 0 Then
                        '_dt.Columns.Add("C" & R!FNHSysQCSuplDetailId.ToString, GetType(Integer))
                        _dt.Columns.Add("C" & R!FTQCSupDetailCode.ToString, GetType(String))
                    End If

                Catch ex As Exception
                End Try

            Next
        End If

        'Call SetGrid(ogv)
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

                    Select Case Col.ColumnName.ToString
                        Case "FTSendSuplNo", "FDSendSuplDate", "FTStyleCode", "FTStyleName", "FTPORef", "FTOrderNo", "FNTableNo", "FNBunbleSeq", "FTColorway", "FTSizeBreakDown",
                               "FTPartName", "FTSuplCode", "FTSuplName", "FNQuantity"
                            .Width = 80
                            .Visible = True
                        Case Else
                            Select Case Col.ColumnName.ToString
                                Case "FNHSysQCSuplDetailId"
                                    .Width = 10
                                    .Visible = False
                                Case Else
                                    If Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 1) = "C" Then
                                        .Width = 50
                                        .Visible = True
                                    Else
                                        .Visible = True
                                        If Col.ColumnName.ToString = "FTQCSupDetailCode" Then
                                            .Width = 150
                                        Else
                                            .Width = 80
                                        End If
                                    End If

                            End Select
                    End Select

                End With

                .Columns.Add(_BanCol)

            Next
            .EndInit()
        End With

        oDtRef = _dt
    End Sub
    Private Sub CreateTabDynamic(_oDt As DataTable)
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
            Me.otbdynamicdetails.TabPages.Clear()
            Dim _Fillter As String = ""
            For Each x As DataRow In _oDt.Select("FTSuplCode <>''", "FTSuplCode")
                If dt.Select("FTSuplCode='" & x!FTSuplCode.ToString & "'").Length <= 0 Then
                    dt.Rows.Add(x!FTSuplCode.ToString)
                End If
            Next
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

                Dim _GridV As New DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
                With _GridV
                    .GridControl = _Grid
                    .Name = "ogvG" & R!FTSuplCode.ToString
                    .OptionsPrint.AutoWidth = False
                    .OptionsCustomization.AllowQuickHideColumns = False
                    .OptionsPrint.PrintHeader = False
                    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                    .OptionsView.ShowColumnHeaders = False
                    .OptionsView.ShowGroupPanel = False
                    AddHandler .RowStyle, AddressOf GridView1_RowStyle
                End With

                _Grid.BeginInit()
                _Grid.MainView = _GridV
                _Grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {_GridV})
                _Grid.EndInit()

                _TabPage.Controls.Add(_Grid)
                _Grid.Dock = DockStyle.Fill

                Dim RData As New DataTable
                Dim _GrandTotal As Integer
                Dim oDt As DataTable = _oDt.Select("FTSuplCode='" & R!FTSuplCode.ToString & "'").CopyToDataTable
                If (_StateDefual) Then
                    Call CreateDatatable(Nothing, _GridV, Nothing, _GrandTotal)
                    Call GenerateGridBand(Nothing, _GridV, _Grid, Nothing, _GrandTotal)
                Else
                    Call CreateDatatable(oDt, _GridV, RData, _GrandTotal)
                    Call GenerateGridBand(oDt, _GridV, _Grid, RData, _GrandTotal)
                End If
                Me.otbdynamicdetails.TabPages.Add(_TabPage)
            Next

        Catch ex As Exception

        End Try
    End Sub
    Private Sub FuncExcel_Click(sender As Object, e As EventArgs) Handles FuncExcel.Click
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New FolderBrowserDialog
            Dim openfileDlg As New OpenFileDialog
            folderDlg.ShowNewFolderButton = True
            If (folderDlg.ShowDialog() = DialogResult.OK) Then
                _FileName = folderDlg.SelectedPath
            Else
                Exit Sub
            End If
            For Each T As DevExpress.XtraTab.XtraTabPage In Me.otbdynamicdetails.TabPages
                Dim _Sfind As String = Replace(T.Name.ToString, "otb", "")
                For Each Obj As Object In T.Controls.Find("ogcG" & _Sfind, True)
                    Try
                        DevExpress.Export.ExportSettings.DefaultExportType = DevExpress.Export.ExportType.WYSIWYG
                        With CType(Obj, DevExpress.XtraGrid.GridControl)
                            .ExportToXlsx(_FileName & "\" & _Sfind.ToString & ".xlsx")
                        End With
                    Catch ex As Exception
                    End Try
                Next
            Next
            Call setStateSendSupl()
        Catch ex As Exception
        End Try
    End Sub
    Private Function setStateSendSupl() As Boolean
        Try
            Dim _Cmd As String = ""
            Dim _FTSendSuplNo As String = ""
            Dim _oDt As DataTable

            For Each T As DevExpress.XtraTab.XtraTabPage In Me.otbdynamicdetails.TabPages
                Dim _Sfind As String = Replace(T.Name.ToString, "otb", "")
                For Each Obj As Object In T.Controls.Find("ogcG" & _Sfind, True)
                    Try
                        DevExpress.Export.ExportSettings.DefaultExportType = DevExpress.Export.ExportType.WYSIWYG
                        With CType(Obj, DevExpress.XtraGrid.GridControl)
                            For Each R As DataRow In DirectCast(.DataSource, DataTable).Rows



                            Next

                        End With
                    Catch ex As Exception
                    End Try
                Next
            Next


            For Each R As DataRow In pDataState.Select("FTSendSuplNo<>''", "FTSendSuplNo ASC")
                If (_FTSendSuplNo <> HI.UL.ULF.rpQuoted(R!FTSendSuplNo.ToString)) Then
                    _Cmd = "Update TPRODTSendSupl Set FTStateSendExcel='1'  "
                    _Cmd &= vbCrLf & ",FTUserSendExcel='" & HI.ST.UserInfo.UserName & "'"
                    _Cmd &= vbCrLf & ",FDDateSendExcel=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",FTTimeSendExcel=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & "Where FTSendSuplNo='" & HI.UL.ULF.rpQuoted(R!FTSendSuplNo.ToString) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                    _Cmd = "Update TPRODTSendSuplToBranch Set FTStateSendExcel='1'    "
                    _Cmd &= vbCrLf & ",FTUserSendExcel='" & HI.ST.UserInfo.UserName & "'"
                    _Cmd &= vbCrLf & ",FDDateSendExcel=" & HI.UL.ULDate.FormatDateDB
                    _Cmd &= vbCrLf & ",FTTimeSendExcel=" & HI.UL.ULDate.FormatTimeDB
                    _Cmd &= vbCrLf & "Where FTSendSuplNo='" & HI.UL.ULF.rpQuoted(R!FTSendSuplNo.ToString) & "'"
                    HI.Conn.SQLConn.ExecuteOnly(_Cmd, Conn.DB.DataBaseName.DB_PROD)
                End If
                _FTSendSuplNo = HI.UL.ULF.rpQuoted(R!FTSendSuplNo.ToString)
            Next
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub GridView1_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs)
        Try

            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                Dim State As String = View.GetRowCellValue(e.RowHandle, "FTStateSendExcel")

                If State = "1" Then
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

End Class