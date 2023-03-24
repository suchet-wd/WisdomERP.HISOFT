Imports System.Globalization
Imports System.Data
Imports System.Windows.Forms
Imports System.IO

Public Class wQCSendSuplImportExcel
    Private pGridData As DataTable
    Private pGridDataScn As DataTable
    Private _PStyleCode As String = ""
    Private _POrderNo As String = ""
    Private _PSuplCode As String = ""
    Private _PSendSuplDate As String = ""
    Private _PBarCodeImport As String = ""

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


    End Sub

#Region "Property"

    Private _SeqImport As String = ""
    Public Property SeqImport As String
        Get
            Return _SeqImport
        End Get
        Set(value As String)
            _SeqImport = value
        End Set
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

            _Cmd = "SELECT        S.FTSendSuplNo,CASE WHEN ISDate(S.FDSendSuplDate)=1 Then convert(nvarchar(10),convert(datetime,S.FDSendSuplDate),103) ELSE '' END AS  FDSendSuplDate, T.FTStyleCode"
            _Cmd &= vbCrLf & ", O.FTPORef, O.FTOrderNo, Y.FNTableNo, L.FNBunbleSeq, L.FTColorway, L.FTSizeBreakDown  "
            _Cmd &= vbCrLf & "  , P.FTSuplCode, SUM(E.FNQuantity) AS FNQuantity, C.FNSendSuplType, Q.FNSubQCType, Q.FTQCSupDetailCode ,sum(TQ.FNDefectQty) AS FNDefectQty , sum(TQ.FNFacDefectQty) AS FNFacDefectQty , max(TQ.FTCheckBy) AS FTCheckBy "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & ", T.FTStyleNameTH AS FTStyleName , M.FTPartNameTH AS FTPartName , P.FTSuplNameEN as FTSuplName , Q.FTQCSupDetailNameTH  as FTQCSupDetailName "
                _Cmd &= vbCrLf & ", X.FTNameTH AS FTSubQCType  , CASE WHEN X.FTNameTH ='-' THEN  convert(nvarchar(2),Z.FNListIndex)+'.'+Z.FTNameTH ELSE convert(nvarchar(2),X.FNListIndex)+'.'+X.FTNameTH END AS FTSendSuplTypeName "
            Else
                _Cmd &= vbCrLf & ", T.FTStyleNameEN AS FTStyleName ,M.FTPartNameEN AS  FTPartName , P.FTSuplNameTH as FTSuplName ,Q.FTQCSupDetailNameEN as FTQCSupDetailName "
                _Cmd &= vbCrLf & ", X.FTNameEN AS FTSubQCType ,CASE WHEN X.FTNameEN ='-' THEN convert(nvarchar(2), Z.FNListIndex)+'.'+Z.FTNameEN ELSE convert(nvarchar(2),X.FNListIndex)+'.'+X.FTNameEN END AS FTSendSuplTypeName"
            End If
            _Cmd &= vbCrLf & " FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl AS S WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo. TPRODTSendSupl_Barcode AS B WITH (NOLOCK) ON S.FTSendSuplNo = B.FTSendSuplNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSupplier AS P WITH (NOLOCK) ON S.FNHSysSuplId = P.FNHSysSuplId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS C WITH (NOLOCK) ON B.FTBarcodeSendSuplNo = C.FTBarcodeSendSuplNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTOrderProd AS D WITH (NOLOCK) ON C.FTOrderProdNo = D.FTOrderProdNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON D.FTOrderNo = O.FTOrderNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS T WITH (NOLOCK) ON O.FNHSysStyleId = T.FNHSysStyleId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS L WITH (NOLOCK) ON C.FTBarcodeBundleNo = L.FTBarcodeBundleNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle_Detail AS E WITH (NOLOCK) ON L.FTBarcodeBundleNo = E.FTBarcodeBundleNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTLayCut AS Y WITH (NOLOCK) ON E.FTLayCutNo = Y.FTLayCutNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMPart AS M WITH (NOLOCK) ON C.FNHSysPartId = M.FNHSysPartId LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TQAMQCSuplDetail AS Q WITH (NOLOCK) ON C.FNSendSuplType = Q.FNSendSuplType LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   (SELECT        FNListIndex, FTNameTH, FTNameEN"
            _Cmd &= vbCrLf & "    FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH (NOLOCK)"
            _Cmd &= vbCrLf & "    WHERE        (FTListName = 'FNSendSuplType')) AS Z ON Q.FNSendSuplType = Z.FNListIndex LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "   (SELECT        FNListIndex, FTNameTH, FTNameEN"
            _Cmd &= vbCrLf & "    FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS HSysListData_1 WITH (NOLOCK)"
            _Cmd &= vbCrLf & "    WHERE        (FTListName = 'FNSubQCType')) AS X ON  Q.FNSubQCType = X.FNListIndex " ' OR Q.FNSubQCType =0"

            _Cmd &= vbCrLf & "LEFT OUTER JOIN ("
            _Cmd &= vbCrLf & "SELECT   T1.FTDocumentRef , T1.FNFacDefectQty , T1.FTCheckBy,  T1.FTBarcodeSendSuplNo, T2.FNHSysQCSuplDetailId, T2.FNDefectQty, T2.FTStyleCode, T2.FTOrderNo, T2.FNTableNo, T2.FNBunbleSeq, T2.FTColorway, T2.FTSizeBreakDown, T2.FTPartCode, T2.FTSuplCode "
            _Cmd &= vbCrLf & "FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect AS T1 WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect_Detail AS T2 WITH (NOLOCK) ON T1.FTBarcodeSendSuplNo = T2.FTBarcodeSendSuplNo"
            _Cmd &= vbCrLf & "WHERE        (T1.FTStateFromSupl = '1')"
            _Cmd &= vbCrLf & "and T2.FNHSysQCSuplDetailId is not null"
            _Cmd &= vbCrLf & ") AS TQ ON  Q.FNHSysQCSuplDetailId = TQ.FNHSysQCSuplDetailId and T.FTStyleCode = TQ.FTStyleCode and O.FTOrderNo = TQ.FTOrderNo and Y.FNTableNo = TQ.FNTableNo "
            _Cmd &= vbCrLf & "and L.FNBunbleSeq = TQ.FNBunbleSeq and L.FTColorway = TQ.FTColorway and L.FTSizeBreakDown = TQ.FTSizeBreakDown and M.FTPartCode = TQ.FTPartCode and P.FTSuplCode = TQ.FTSuplCode  and S.FTSendSuplNo = TQ.FTDocumentRef "

            _Cmd &= vbCrLf & " WHERE  (S.FTSendSuplNo <>'')"
            If _PStyleCode <> "" Then
                _Cmd &= vbCrLf & " AND T.FTStyleCode in (" & _PStyleCode & ")"
            End If
            If _POrderNo <> "" Then
                _Cmd &= vbCrLf & "  AND O.FTOrderNo in  (" & _POrderNo & ")"
            End If
            If _PSuplCode <> "" Then
                _Cmd &= vbCrLf & " AND  P.FTSuplCode in (" & _PSuplCode & ")"
            End If
            If _PSendSuplDate <> "" Then
                _Cmd &= vbCrLf & " AND FDSendSuplDate in (" & _PSendSuplDate & ")"
            End If
            _Cmd &= vbCrLf & " and  B.FTImportState = '" & SeqImport & "'"

            _Cmd &= vbCrLf & " GROUP BY S.FTSendSuplNo, S.FDSendSuplDate, T.FTStyleCode, T.FTStyleNameTH, T.FTStyleNameEN, O.FTPORef, O.FTOrderNo, Y.FNTableNo, L.FNBunbleSeq, L.FTColorway, L.FTSizeBreakDown, M.FTPartNameTH, "
            _Cmd &= vbCrLf & "  M.FTPartNameEN, P.FTSuplCode, P.FTSuplNameEN, P.FTSuplNameTH, C.FNSendSuplType, Q.FNSendSuplType, Q.FNSubQCType, Q.FTQCSupDetailCode, Q.FTQCSupDetailNameTH, Q.FTQCSupDetailNameEN,"
            _Cmd &= vbCrLf & "  X.FTNameTH, X.FTNameEN, Z.FTNameTH, Z.FTNameEN,Z.FNListIndex ,X.FNListIndex"
            _Cmd &= vbCrLf & "order by S.FTSendSuplNo asc , S.FDSendSuplDate asc, T.FTStyleCode asc , O.FTPORef asc , O.FTOrderNo asc, Y.FNTableNo asc, L.FNBunbleSeq asc, L.FTColorway asc, L.FTSizeBreakDown asc"
            _Cmd &= vbCrLf & ", sum(TQ.FNFacDefectQty) desc , max(TQ.FTCheckBy) desc"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_PROD)
            Call CreateTabDynamic(_oDt)


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

            Dim sFieldSum As String = "FNQuantity|FNFacDefectQty"
            With ogv
                .BeginInit()
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
                        .Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left
                    End With
                    .Bands.Add(_gBand)
                    _GbandIndex = _GbandIndex + 1
                Next




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

                        For Each R As DataRow In EmpData.Select("FTSendSuplTypeName='" & Ind & "'", "FTSendSuplTypeName , FTQCSupDetailCode")
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
                                    .Columns.Add(ogv.Columns.ColumnByFieldName("C" & R!FTQCSupDetailCode.ToString))
                                    .Caption = R!FTQCSupDetailName.ToString
                                    .Name = ogv.Name.ToString & "gbcol11" & R!FTQCSupDetailName.ToString
                                    .RowCount = 1
                                    .Columns("C" & R!FTQCSupDetailCode.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum, "C" & R!FTQCSupDetailCode.ToString)
                                    .Columns("C" & R!FTQCSupDetailCode.ToString).SummaryItem.DisplayFormat = "{0:n0}"
                                End With

                                _GrbandCol1.Children.Add(_GrbandCol11)
                            End If
                            _Code = R!FTQCSupDetailCode.ToString

                        Next
                    Next
                End If

                _Str = "FNFacDefectQty|FTCheckBy"
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

    Private Sub ocmload_Click(sender As Object, e As EventArgs)
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
            _dt.Columns.Add("FNFacDefectQty", GetType(Integer))
            _dt.Columns.Add("FTCheckBy", GetType(String), "")


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
                        _dt.Rows.Add(R!FTSendSuplNo.ToString, R!FDSendSuplDate.ToString, R!FTStyleCode.ToString, _
                                      R!FTPORef.ToString, R!FTOrderNo.ToString, R!FNTableNo.ToString, R!FNBunbleSeq.ToString, R!FTColorway.ToString, _
                                      R!FTSizeBreakDown.ToString, R!FTPartName.ToString, R!FTSuplCode.ToString, R!FNQuantity.ToString, Integer.Parse("0" & R!FNFacDefectQty.ToString), _
                                      HI.UL.ULF.rpQuoted(R!FTCheckBy.ToString))
                    End If
                Catch ex As Exception
                End Try

                If _dt.Columns.IndexOf("C" & R!FTQCSupDetailCode.ToString) < 0 Then
                    _dt.Columns.Add("C" & R!FTQCSupDetailCode.ToString, GetType(Integer))
                End If

                If Val(R!FNDefectQty.ToString) > 0 Then
                    For Each Rx As DataRow In _dt.Select(_StrFilter)
                        Rx.Item("C" & R!FTQCSupDetailCode.ToString) = Val(R!FNDefectQty.ToString)   'Val(Rx.Item("C" & R!FTQCSupDetailCode.ToString).ToString) =
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

                    Select Case Col.ColumnName.ToString
                        Case "FTSendSuplNo", "FDSendSuplDate", "FTStyleCode", "FTStyleName", "FTPORef", "FTOrderNo", "FNTableNo", "FNBunbleSeq", "FTColorway", "FTSizeBreakDown", _
                               "FTPartName", "FTSuplCode", "FTSuplName", "FNQuantity"
                            .Width = 80

                        Case Else
                            If Microsoft.VisualBasic.Left(Col.ColumnName.ToString, 1) = "C" Then
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

    Private Sub FuncExcel_Click(sender As Object, e As EventArgs) Handles ocmReadExcel.Click
        Try
            Dim _FileName As String = ""
            Dim folderDlg As New OpenFileDialog
            With folderDlg
                '.InitialDirectory = "D:\"
                '.InitialDirectory = ""
                .Filter = "Excel Worksheets(97-2003)" & "|*.xls|Excel Worksheets(2010-2013)|*.xlsx"
                .FilterIndex = 1
                .RestoreDirectory = False
                .Multiselect = False
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    _FileName = .FileName
                    Call ReadXlsfile(_FileName)
                End If
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Function getSeqImport() As String
        Try
            Dim _Cmd As String = ""
            _Cmd = "Select  Top 1   REPLACE( convert(varchar(10) ,  GETDATE() , 111) , '/','')   + RIGHT( '0000' +  convert(varchar(4) , convert(int, RIGHT( FTImportState,4)) + 1 ),4) FRom     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode "
            _Cmd &= vbCr & " where  left(FTImportState,8)  =   REPLACE( convert(varchar(10) ,  GETDATE() , 111) , '/','') "
            _Cmd &= vbCrLf & " Order by FTImportState desc  "
            If HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "") = "" Then
                _Cmd = "Select  REPLACE( convert(varchar(10) ,  GETDATE() , 111) , '/','')  +  + RIGHT( '0000' + convert(varchar(4) , convert(int, RIGHT(00 + '00000',4)) + 1 ),4) as T  "

            End If

            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub ReadXlsfile(_fileName As String)
        Try

            _SeqImport = getSeqImport()
            Dim _oDt As New DataTable
            Dim _Qry As String = ""
            Dim xlsFilename As String = Path.GetFileName(_fileName)
            _oDt = HI.UL.ReadExcel.Read(_fileName, "Sheet", -1)
            If Not (_oDt Is Nothing) Then
                _PStyleCode = ""
                _POrderNo = ""
                _PSuplCode = ""
                _PSendSuplDate = ""
                Dim _Spls As New HI.TL.SplashScreen("Reading....Please Wait.", "Import Data From File ")
                Try
                    '_cmd.Fill(_oDt)
                    Dim _DefectCode As String()
                    Dim ColumnCount As Integer = _oDt.Columns.Count - 1
                    Dim RowCount As Integer = _oDt.Rows.Count
                    Dim l As Integer = 0
                    For Each R As DataRow In _oDt.Rows
                        If R.Item(0).ToString <> "" And R.Item(1).ToString <> "" And IsNumeric(R.Item(6).ToString) Then

                            _Spls.UpdateInformation("Reading file " & xlsFilename & "  Row" & l & " of " & RowCount & " (" & Format((l * 100.0) / RowCount, "0.00") & " % ) ")

                            Dim FTSendSuplNo As String = R.Item(0).ToString
                            Dim FDSendSuplDate As String = HI.UL.ULDate.ConvertEnDB(R.Item(1).ToString)
                            Dim FTStyleCode As String = R.Item(2).ToString
                            Dim FTPORef As String = R.Item(3).ToString
                            Dim FTOrderNo As String = R.Item(4).ToString
                            Dim FNTableNo As String = R.Item(5).ToString
                            Dim FNBunbleSeq As String = R.Item(6).ToString
                            Dim FTColorway As String = R.Item(7).ToString
                            Dim FTSizeBreakDown As String = R.Item(8).ToString
                            Dim FTPartName As String = R.Item(9).ToString
                            Dim FTSuplCode As String = R.Item(10).ToString
                            Dim FTPartCode As String = HI.Conn.SQLConn.GetField("SELECT Top 1  FTPartCode FROM  TMERMPart WITH (NOLOCK) WHERE FTPartNameTH = '" & FTPartName & "' OR FTPartNameEN='" & FTPartName & "'", Conn.DB.DataBaseName.DB_MASTER, "")
                            Dim NewBarCodeFromSupl As String = "" & FTSendSuplNo & "|" & FTOrderNo & "|" & FNTableNo & "|" & FNBunbleSeq & "|" & FTPartCode & ""
                            Dim FNFacDefectQty As String = R.Item(ColumnCount - 1).ToString
                            Dim FTCheckBy As String = R.Item(ColumnCount).ToString

                            Dim _BarcodeSendSuplNo As String = getBarcodeSendSupl(FTSendSuplNo, FNBunbleSeq)

                            _Qry = "Update  TPRODTSendSupl_Barcode "
                            _Qry &= vbCrLf & " set FTImportState = '" & SeqImport & "'"
                            _Qry &= vbCrLf & "where  FTSendSuplNo ='" & FTSendSuplNo & "'"
                            _Qry &= vbCrLf & "and FTBarcodeSendSuplNo = '" & _BarcodeSendSuplNo & "'"
                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                            _Qry = "Delete From TPROSendSuplDefect WHERE FTBarcodeSendSuplNo='" & NewBarCodeFromSupl & "'"
                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)
                            _Qry = "Delete From TPROSendSuplDefect_Detail WHERE FTBarcodeSendSuplNo='" & NewBarCodeFromSupl & "'"
                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                            _Qry = "INSERT INTO TPROSendSuplDefect (FTInsUser, FDInsDate, FTInsTime,FTBarcodeSendSuplNo,  FTStateFromSupl, FTDocumentRef, FTStateActive,FNFacDefectQty , FTCheckBy)"
                            _Qry &= vbCrLf & "Select '" & HI.ST.UserInfo.UserName & "'"
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                            _Qry &= vbCrLf & ",'" & NewBarCodeFromSupl & "'"
                            _Qry &= vbCrLf & ",'1'"
                            _Qry &= vbCrLf & ",'" & FTSendSuplNo & "'"
                            _Qry &= vbCrLf & ",'0'"
                            _Qry &= vbCrLf & "," & Integer.Parse("0" & FNFacDefectQty.ToString)
                            _Qry &= vbCrLf & ",'" & FTCheckBy & "'"
                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                            For i As Integer = 12 To ColumnCount
                                Try
                                    If Integer.Parse("0" & _oDt.Rows(l).Item(i).ToString()) > 0 Then
                                        _oDt.Rows(1).Item(i).ToString()
                                        _oDt.Rows(l).Item(i).ToString()
                                        Dim _QCSuplDetailId As Integer = HI.Conn.SQLConn.GetField("Select Top 1 FNHSysQCSuplDetailId From TQAMQCSuplDetail WITH(NOLOCK) WHERE FTQCSupDetailCode='" & _oDt.Rows(1).Item(i).ToString() & "'", Conn.DB.DataBaseName.DB_MASTER, "0")
                                        If _QCSuplDetailId > 0 Then
                                            _Qry = "Insert into TPROSendSuplDefect_Detail ( FTInsUser, FDInsDate, FTInsTime, FTBarcodeSendSuplNo, FNHSysQCSuplDetailId, FNDefectQty, FNSeq"
                                            _Qry &= vbCrLf & ", FTStyleCode, FTOrderNo, FNTableNo, FNBunbleSeq, FTColorway, FTSizeBreakDown, FTPartCode,  FTSuplCode)"
                                            _Qry &= vbCrLf & "Select '" & HI.ST.UserInfo.UserName & "'"
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                                            _Qry &= vbCrLf & ",'" & NewBarCodeFromSupl & "'"
                                            _Qry &= vbCrLf & "," & _QCSuplDetailId
                                            _Qry &= vbCrLf & "," & Integer.Parse("0" & _oDt.Rows(l).Item(i).ToString())
                                            _Qry &= vbCrLf & ",1"
                                            _Qry &= vbCrLf & ",'" & FTStyleCode & "'"
                                            _Qry &= vbCrLf & ",'" & FTOrderNo & "'"
                                            _Qry &= vbCrLf & "," & FNTableNo
                                            _Qry &= vbCrLf & "," & FNBunbleSeq
                                            _Qry &= vbCrLf & ",'" & FTColorway & "'"
                                            _Qry &= vbCrLf & ",'" & FTSizeBreakDown & "'"
                                            _Qry &= vbCrLf & ",'" & FTPartCode & "'"
                                            _Qry &= vbCrLf & ",'" & FTSuplCode & "'"

                                            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)

                                        End If


                                    End If
                                Catch ex As Exception
                                End Try
                            Next


                            'criteria
                            If _PStyleCode <> "" Then _PStyleCode &= ","
                            _PStyleCode &= "'" & FTStyleCode & "'"
                            If _POrderNo <> "" Then _POrderNo &= ","
                            _POrderNo &= "'" & FTOrderNo & "'"
                            If _PSendSuplDate <> "" Then _PSendSuplDate &= ","
                            _PSendSuplDate &= "'" & FDSendSuplDate & "'"
                            If _PSuplCode <> "" Then _PSuplCode &= ","
                            _PSuplCode &= "'" & FTSuplCode & "'"
                            If _PBarCodeImport <> "" Then _PBarCodeImport &= ","
                            _PBarCodeImport &= "'" & NewBarCodeFromSupl & "'"
                            'criteria
                        End If

                        l += +1
                    Next


                    _Qry = " Delete T1"
                    _Qry &= vbCrLf & " FROM   TPROSendSuplDefect AS T1 WITH (NOLOCK) LEFT OUTER JOIN"
                    _Qry &= vbCrLf & " TPROSendSuplDefect_Detail AS T2 WITH (NOLOCK) ON T1.FTBarcodeSendSuplNo = T2.FTBarcodeSendSuplNo"
                    _Qry &= vbCrLf & " WHERE        (T1.FTStateFromSupl = '1')"
                    _Qry &= vbCrLf & " and T2.FNHSysQCSuplDetailId is null"
                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_PROD)
                    _Spls.Close()
                    Call LoadData()
                Catch ex As Exception
                    MsgBox(ex.Message)
                    _Spls.Close()
                End Try
            Else
                HI.MG.ShowMsg.mInfo("Invalid Sheet Name In Excel File..", 1509281139, Me.Text, "Sheet")
                Exit Sub
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If _PBarCodeImport = "" Then
                HI.MG.ShowMsg.mInfo("Data Is not... Pls  File Import !!", 1508151010, Me.Text)
            End If
            If confrim(_PBarCodeImport) Then
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            Else
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function confrim(_FTBarcodeSendSuplNo As String) As Boolean
        Try
            Dim _Cmd As String = ""
            _Cmd = "Update  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPROSendSuplDefect "
            _Cmd &= vbCrLf & "Set FTStateActive = '1'"
            _Cmd &= vbCrLf & "WHERE  FTBarcodeSendSuplNo in (" & _FTBarcodeSendSuplNo & ")"
            _Cmd &= vbCrLf & "And Isnull(FTStateFromSupl,'0') ='1'"
            Return HI.Conn.SQLConn.ExecuteNonQuery(_Cmd, Conn.DB.DataBaseName.DB_PROD)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function getBarcodeSendSupl(ByVal SendSulpNo As String, ByVal _BundleSeq As Integer) As String
        Try
            Dim _Cmd As String
            _Cmd = "  SELECT Top 1  A.FTBarcodeSendSuplNo  "
            _Cmd &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODBarcode_SendSupl AS A WITH (NOLOCK)  INNER JOIN  "
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTBundle AS BB WITH (NOLOCK)  ON A.FTBarcodeBundleNo = BB.FTBarcodeBundleNo INNER JOIN  "
            _Cmd &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTSendSupl_Barcode AS B  WITH (NOLOCK) ON A.FTBarcodeSendSuplNo = B.FTBarcodeSendSuplNo "
            _Cmd &= vbCrLf & " WHERE B.FTSendSuplNo='" & HI.UL.ULF.rpQuoted(SendSulpNo) & "'"
            _Cmd &= vbCrLf & " and BB.FNBunbleSeq =" & _BundleSeq

            Return HI.Conn.SQLConn.GetField(_Cmd, Conn.DB.DataBaseName.DB_PROD, "")
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class