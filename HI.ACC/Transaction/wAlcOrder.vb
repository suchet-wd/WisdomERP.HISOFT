Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing
Imports DevExpress.XtraGrid

Public Class wAlcOrder

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub wAlcOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            'Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
            Me.FDMonthUse.Text = Date.Now
            Call InitGrid()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Try
            HI.TL.HandlerControl.ClearControl(Me)
            Me.FDMonthUse.Text = Date.Now
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If Me.FDMonthUse.Text <> "" And Me.FNHSysCmpId.Text <> "" Then
                Call LoadData()
            Else

                If Me.FNHSysCmpId.Text = "" Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCmpId_lbl.Text)
                    Me.FNHSysCmpId.Focus()
                    Exit Sub
                End If

                If Me.FDMonthUse.Text = "" Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FDMonthUse_lbl.Text)
                    Me.FDMonthUse.Focus()
                    Exit Sub
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadData()
        Dim _Spls As New HI.TL.SplashScreen("Loading data please waiting....")
        Try

            Dim _Cmd As String = ""
            Dim _oDt As DataTable

            _Cmd = "SELECT        FTOrderNo,RIGHT(FDDate,7) as  FDDate , sum(FNAmount) AS FNAmount ,FNHSysCmpId , FTCmpCode ,FNHSysRawMatId,FTRawMatName,FTRawMatCode , FTRawMatSizeCode , FTRawMatColorCode ,  FTPORef FROM            ("
            _Cmd &= vbCrLf & " SELECT        I.FTOrderNo, I.FDIssueDate, T.FTBarcodeNo, T.FNQuantity, B.FNHSysRawMatId, B.FNPrice, M.FTRawMatCode , (T.FNQuantity * B.FNPrice ) AS FNAmount"
            _Cmd &= vbCrLf & " , Case when Isdate(I.FDIssueDate) = 1 Then convert(varchar(10), convert(datetime,I.FDIssueDate),103) Else '' End AS FDDate "

            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & " , M.FTRawMatNameTH as FTRawMatName "
            Else
                _Cmd &= vbCrLf & " , M.FTRawMatNameEN as FTRawMatName "
            End If

            _Cmd &= vbCrLf & ", O.FNHSysCmpId , C.FTCmpCode , S.FTRawMatSizeCode , CL.FTRawMatColorCode, O.FTPORef"
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS I WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON I.FTOrderNo = O.FTOrderNo INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS T WITH (NOLOCK) ON I.FTIssueNo = T.FTDocumentNo AND I.FTOrderNo = T.FTOrderNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH (NOLOCK) ON T.FTBarcodeNo = B.FTBarcodeNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH (NOLOCK) ON B.FNHSysRawMatId = M.FNHSysRawMatId"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH(NOLOCK)  ON O.FNHSysCmpId = C.FNHSysCmpId"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON M.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS CL WITH (NOLOCK) ON M.FNHSysRawMatColorId = CL.FNHSysRawMatColorId"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMainMat AS Z WITH(NOLOCK) ON M.FTRawMatCode = Z.FTMainMatCode "

            _Cmd &= vbCrLf & "  WHERE (O.FNOrderType in (2,3))  and Isnull(Z.FTStateNotAllocate,'0') = '0'  "
            _Cmd &= vbCrLf & " and I.FNHSysCmpId=" & Integer.Parse("0" & Me.FNHSysCmpId.Properties.Tag)
            _Cmd &= vbCrLf & " And LEFT(I.FDIssueDate,7)=LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FDMonthUse.Text) & "',7)"
            _Cmd &= vbCrLf & " ) AS T  "
            _Cmd &= vbCrLf & " group by  FTOrderNo,RIGHT(FDDate,7) ,FNHSysCmpId , FTCmpCode ,FNHSysRawMatId,FTRawMatName,FTRawMatCode , FTRawMatSizeCode , FTRawMatColorCode ,  FTPORef"
            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            Me.ogcdetail.DataSource = _oDt


            _Cmd = "SELECT        FTOrderNo, RIGHT(FDDate,7) as FDDate   ,FNHSysCmpId , FTCmpCode , isnull(FNAmtBaht,0) AS FNAmtBaht  , FTPORef  FROM            (" ' ,FNHSysRawMatId,FTRawMatName ,FTRawMatCode , FTRawMatSizeCode , FTRawMatColorCode  
            _Cmd &= vbCrLf & "SELECT        I.FTOrderNo, I.FDIssueDate"
            _Cmd &= vbCrLf & " , Case when Isdate(I.FDIssueDate) = 1 Then convert(varchar(10), convert(datetime,I.FDIssueDate),103) Else '' End AS FDDate "
      
            _Cmd &= vbCrLf & ", O.FNHSysCmpId, C.FTCmpCode ,  Z.FNAmtBaht  , O.FTPORef"
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS I WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON I.FTOrderNo = O.FTOrderNo   "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH(NOLOCK)  ON O.FNHSysCmpId = C.FNHSysCmpId"

            _Cmd &= vbCrLf & "LEFT OUTER JOIN (SELECT        O.FTOrderNo, O.FDOrderDate, O.FNHSysCmpId, sum(convert(numeric(18,2),B.FNAmt*Isnull(FNSellingRate,1))) AS  FNAmtBaht  "
            _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS B WITH (NOLOCK) ON O.FTOrderNo = B.FTOrderNo"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS S WITH(NOLOCK) ON O.FTOrderNo = S.FTOrderNo and B.FTSubOrderNo = S.FTSubOrderNo"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExchangeRate AS C WITH(NOLOCK) ON S.FNHSysCurId = C.FNHSysCurId and O.FDOrderDate = C.FDDate"
            _Cmd &= vbCrLf & "group by  O.FTOrderNo, O.FDOrderDate, O.FNHSysCmpId,   S.FNHSysCurId ) AS Z ON I.FTOrderNo = Z.FTOrderNo and O.FNHSysCmpId = Z.FNHSysCmpId"

            _Cmd &= vbCrLf & "  WHERE (O.FNOrderType in (0,22,13)) "
            _Cmd &= vbCrLf & " and I.FNHSysCmpId=" & Integer.Parse("0" & Me.FNHSysCmpId.Properties.Tag)
            _Cmd &= vbCrLf & " And LEFT(I.FDIssueDate,7)=LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FDMonthUse.Text) & "',7)"
            _Cmd &= vbCrLf & " ) AS T  "
            _Cmd &= vbCrLf & " group by  FTOrderNo,RIGHT(FDDate,7) ,FNHSysCmpId , FTCmpCode , isnull(FNAmtBaht,0)  ,  FTPORef" ',FNHSysRawMatId,FTRawMatName,FTRawMatCode, FTRawMatSizeCode , FTRawMatColorCode  "

            _oDt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)
            'Me.ogcproduction.DataSource = _oDt
            Call InitGrid(_oDt)

            ogvdetail.ExpandAllGroups()
            ogvproduction.ExpandAllGroups()
            _Spls.Close()
        Catch ex As Exception
            _Spls.Close()
        End Try
    End Sub

    Private Sub InitGrid(_oDt As DataTable)
        Try
            Dim _dt As DataTable
            Dim _Cmd As String = ""

            _Cmd = " DECLARE @cols AS NVARCHAR(MAX), @query  AS NVARCHAR(MAX)"
            _Cmd &= vbCrLf & " SELECT    FTOrderNo "
            _Cmd &= vbCrLf & "INTO #Tmp"
            _Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTALCOrder_Detail"
            _Cmd &= vbCrLf & " where FDMonth = LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FDMonthUse.Text) & "',7)"
            _Cmd &= vbCrLf & " and FNHSysCmpId = " & Integer.Parse("0" & Me.FNHSysCmpId.Properties.Tag)
            _Cmd &= vbCrLf & " Group by  FTOrderNo , FNHSysCmpId  Order by FNHSysCmpId ASC  "
            _Cmd &= vbCrLf & "select @cols = STUFF((SELECT ',' + QUOTENAME(FTOrderNo) "
            _Cmd &= vbCrLf & "from #Tmp"
            _Cmd &= vbCrLf & "   FOR XML PATH(''), TYPE"
            _Cmd &= vbCrLf & "   ).value('.', 'NVARCHAR(MAX)') "
            _Cmd &= vbCrLf & "  ,1,1,'')"
            _Cmd &= vbCrLf & "set @query = 'SELECT  RIGHT(FDMonth,2)+''/''+LEFT(FDMonth,4) as FDDate , FTOrderNoTo AS FTOrderNo,FTCmpCode , FNHSysCmpId, FTRawMatCode  , FTRawMatSizeCode , FTRawMatColorCode , FTRawMatName,FTPORef , ' + @cols + '"
            _Cmd &= vbCrLf & "  FROM  ( SELECT       D.FDMonth , O.FTPORef, D.FTOrderNo, D.FTOrderNoTo, D.FNHSysCmpId, D.FNAmount , C.FTCmpCode , M.FTRawMatCode  , S.FTRawMatSizeCode , CL.FTRawMatColorCode  "
            If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
                _Cmd &= vbCrLf & " , M.FTRawMatNameTH as FTRawMatName "
            Else
                _Cmd &= vbCrLf & " , M.FTRawMatNameEN as FTRawMatName "
            End If
            _Cmd &= vbCrLf & "	FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTALCOrder_Detail AS D  WITH(NOLOCK)  "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH(NOLOCK)  ON D.FNHSysCmpId = C.FNHSysCmpId"
            _Cmd &= vbCrLf & "  LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH (NOLOCK) ON D.FNHSysRawMatId = M.FNHSysRawMatId"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S WITH (NOLOCK) ON M.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS CL WITH (NOLOCK) ON M.FNHSysRawMatColorId = CL.FNHSysRawMatColorId"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON D.FTOrderNoTo = O.FTOrderNo "
            _Cmd &= vbCrLf & " where FDMonth = LEFT(''" & HI.UL.ULDate.ConvertEnDB(Me.FDMonthUse.Text) & "'',7)"
            _Cmd &= vbCrLf & " and D.FNHSysCmpId = " & Integer.Parse("0" & Me.FNHSysCmpId.Properties.Tag)
            _Cmd &= vbCrLf & "	) xp  pivot ( Sum(FNAmount)  for FTOrderNo in (' + @cols + ')  ) p '"
            _Cmd &= vbCrLf & "execute(@query);"
            _Cmd &= vbCrLf & "drop table #Tmp"
            _dt = HI.Conn.SQLConn.GetDataTable(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT)

            Dim _colcount As Integer = 0
            With Me.ogvproduction
                .BeginInit()
                For I As Integer = .Columns.Count - 1 To 0 Step -1
                    Select Case .Columns(I).FieldName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FDIssueDate".ToUpper, "FTBarcodeNo".ToUpper, "FNQuantity".ToUpper, "FTPORef".ToUpper, "FNHSysRawMatId".ToUpper, "FTRawMatCode".ToUpper, _
                            "FTRawMatName".ToUpper, "FNAmount".ToUpper, "FDDate".ToUpper, "FTCmpCode".ToUpper, "FNHSysCmpId".ToUpper, "FTRawMatSizeCode".ToUpper, "FTRawMatColorCode".ToUpper
                            .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                            .Columns(I).OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                            .Columns(I).OptionsColumn.AllowShowHide = DevExpress.Utils.DefaultBoolean.False
                        Case Else
                            .Columns.Remove(.Columns(I))
                    End Select
                Next
                If Not (_dt Is Nothing) Then
                    For Each Col As DataColumn In _dt.Columns
                        Select Case Col.ColumnName.ToString.ToUpper
                            Case "FTOrderNo".ToUpper, "FDIssueDate".ToUpper, "FTBarcodeNo".ToUpper, "FNQuantity".ToUpper, "FTPORef".ToUpper, "FNHSysRawMatId".ToUpper, "FTRawMatCode".ToUpper, _
                            "FTRawMatName".ToUpper, "FNAmount".ToUpper, "FDDate".ToUpper, "FTCmpCode".ToUpper, "FNHSysCmpId".ToUpper, "FTRawMatSizeCode".ToUpper, "FTRawMatColorCode".ToUpper

                            Case Else
                                _colcount = _colcount + 1
                                Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
                                With ColG
                                    .Visible = True
                                    .FieldName = Col.ColumnName.ToString
                                    .Name = Col.ColumnName.ToString
                                    .Caption = Col.ColumnName.ToString
                                End With
                                .Columns.Add(ColG)
                                With .Columns(Col.ColumnName.ToString)
                                    .OptionsFilter.AllowAutoFilter = False
                                    .OptionsFilter.AllowFilter = False
                                    .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                    .DisplayFormat.FormatString = "{0:n2}"
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                                    With .OptionsColumn
                                        .AllowMove = False
                                        '.AllowGroup = DevExpress.Utils.DefaultBoolean.False
                                        .AllowSort = DevExpress.Utils.DefaultBoolean.False
                                        .AllowShowHide = DevExpress.Utils.DefaultBoolean.False
                                        .AllowEdit = False
                                        .ReadOnly = False
                                        'If Col.ColumnName.ToString.ToUpper = "Total".ToUpper Then
                                        '    .AllowFocus = False
                                        'End If
                                    End With
                                End With
                                '.Columns(Col.ColumnName.ToString).Width = 45
                                .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
                                .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n2}"
                        End Select
                    Next
                End If
                .EndInit()
            End With


            With ogvproduction
                .Columns("FTCmpCode").Group()
                .GroupFooterShowMode = GroupFooterShowMode.VisibleAlways

                For Each Col As DataColumn In _dt.Columns
                    Select Case Col.ColumnName.ToString.ToUpper
                        Case "FTOrderNo".ToUpper, "FDIssueDate".ToUpper, "FTBarcodeNo".ToUpper, "FNQuantity".ToUpper, "FTPORef".ToUpper, "FNHSysRawMatId".ToUpper, "FTRawMatCode".ToUpper, _
                        "FTRawMatName".ToUpper, "FNAmount".ToUpper, "FDDate".ToUpper, "FTCmpCode".ToUpper, "FNHSysCmpId".ToUpper, "FTRawMatSizeCode".ToUpper, "FTRawMatColorCode".ToUpper
                        Case Else
                            Try
                                .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Col.ColumnName.ToString)
                                .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n2}"
                                .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Col.ColumnName.ToString, Nothing, "(Summary by " & .Columns.ColumnByFieldName(Col.ColumnName.ToString).Caption & "={0:n2})")

                                ' Make the group footers always visible.

                                ' Create and setup the first summary item.
                                Dim item As GridGroupSummaryItem = New GridGroupSummaryItem()
                                item.FieldName = Col.ColumnName.ToString
                                item.SummaryType = DevExpress.Data.SummaryItemType.Count
                                .GroupSummary.Add(item)
                                ' Create and setup the second summary item.
                                Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem()
                                item1.FieldName = Col.ColumnName.ToString
                                item1.SummaryType = DevExpress.Data.SummaryItemType.Sum
                                'item1.DisplayFormat = "Summary by " & .Columns.ColumnByFieldName(Col.ColumnName.ToString).Caption & " {0:n2}"
                                item1.DisplayFormat = " {0:n2}"
                                item1.ShowInGroupColumnFooter = .Columns(Col.ColumnName.ToString)
                                .GroupSummary.Add(item1)

                            Catch ex As Exception
                            End Try
                    End Select
                Next

                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                .OptionsView.ShowFooter = False
                .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
                .OptionsView.ShowGroupPanel = True
                .OptionsView.ShowAutoFilterRow = False
                .ExpandAllGroups()
                .RefreshData()
            End With

            Me.ogcproduction.DataSource = IIf(_dt.Rows.Count > 0, _dt, _oDt)
            HI.TL.HandlerControl.AddHandlerGridColumnEdit(ogvdetail)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        With ogvdetail
            .Columns("FTCmpCode").Group()
            .Columns("FTOrderNo").Group()

            .Columns("FNAmount").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNAmount")
            .Columns("FNAmount").SummaryItem.DisplayFormat = "{0:n2}"
            .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "FNAmount", Nothing, "(Summary by " & .Columns.ColumnByFieldName("FNAmount").Caption & "={0:n2})")
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = False
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            .OptionsView.ShowGroupPanel = True
            .OptionsView.ShowAutoFilterRow = False

            ' Make the group footers always visible.
            .GroupFooterShowMode = GroupFooterShowMode.VisibleAlways
            ' Create and setup the first summary item.
            Dim item As GridGroupSummaryItem = New GridGroupSummaryItem()
            item.FieldName = "FNAmount"
            item.SummaryType = DevExpress.Data.SummaryItemType.Count
            .GroupSummary.Add(item)
            ' Create and setup the second summary item.
            Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem()
            item1.FieldName = "FNAmount"
            item1.SummaryType = DevExpress.Data.SummaryItemType.Sum
            item1.DisplayFormat = "Summary by " & .Columns.ColumnByFieldName("FNAmount").Caption & " {0:n2}"
            item1.ShowInGroupColumnFooter = .Columns("FNAmount")
            .GroupSummary.Add(item1)


            .ExpandAllGroups()
            .RefreshData()
        End With

        With ogvproduction
            .Columns("FTCmpCode").Group()
            Try
                .Columns("FNAmount").Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FNAmount")
                .Columns("FNAmount").SummaryItem.DisplayFormat = "{0:n2}"
                .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "FNAmount", Nothing, "(Summary by " & .Columns.ColumnByFieldName("FNAmount").Caption & "={0:n2})")
            Catch ex As Exception
            End Try
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = False
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            .OptionsView.ShowGroupPanel = True
            .OptionsView.ShowAutoFilterRow = False

            .ExpandAllGroups()
            .RefreshData()
        End With


        '------End Add Summary Grid-------------
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try
            If Me.FDMonthUse.Text <> "" And Me.FNHSysCmpId.Text <> "" Then
                If SaveData() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Call LoadData()
                    Exit Sub
                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Exit Sub
                End If
            Else
                If Me.FNHSysCmpId.Text = "" Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FNHSysCmpId_lbl.Text)
                    Me.FNHSysCmpId.Focus()
                    Exit Sub
                End If
                If Me.FDMonthUse.Text = "" Then
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.FDMonthUse_lbl.Text)
                    Me.FDMonthUse.Focus()
                    Exit Sub
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function SaveData() As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Saving data please waiting....")
        Try
            Dim _Cmd As String = ""
            Dim _oDt As DataTable
            Dim _oDtCount As DataTable
            Dim _QtyUse As Double = 0
            Dim _QtyOrg As Double = 0
            Dim _AmtPro As Double = 0

            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_ACCOUNT)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTALCOrder "
            _Cmd &= vbCrLf & "  WHERE FDMonth = LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FDMonthUse.Text) & "',7)"
            '_Cmd &= vbCrLf & "and FNHSysCmpId=" & Integer.Parse("0" & Me.FNHSysCmpId.Properties.Tag)
            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If


            _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTALCOrder ( FTInsUser, FDInsDate, FTInsTime,   FDMonth, FTOrderNo,FNAmount ,FNHSysCmpId )"
            _Cmd &= vbCrLf & "SELECT   '" & HI.ST.UserInfo.UserName & "'"
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
            _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
            _Cmd &= vbCrLf & ",LEFT(FDIssueDate,7) ,FTOrderNo  ,sum(FNAmount) AS FNAmount  , FNHSysCmpId"
            _Cmd &= vbCrLf & " FROM  ("
            _Cmd &= vbCrLf & " SELECT     O.FNHSysCmpId, I.FTOrderNo, I.FDIssueDate, T.FTBarcodeNo, T.FNQuantity, B.FNHSysRawMatId, B.FNPrice, M.FTRawMatCode , (T.FNQuantity * B.FNPrice ) AS FNAmount"
            _Cmd &= vbCrLf & " , Case when Isdate(I.FDIssueDate) = 1 Then convert(varchar(10), convert(datetime,I.FDIssueDate),103) Else '' End AS FDDate "
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS I WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON I.FTOrderNo = O.FTOrderNo INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS T WITH (NOLOCK) ON I.FTIssueNo = T.FTDocumentNo AND I.FTOrderNo = T.FTOrderNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH (NOLOCK) ON T.FTBarcodeNo = B.FTBarcodeNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH (NOLOCK) ON B.FNHSysRawMatId = M.FNHSysRawMatId"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMainMat AS Z WITH(NOLOCK) ON M.FTRawMatCode = Z.FTMainMatCode "
            _Cmd &= vbCrLf & "  WHERE  (O.FNOrderType in (2,3)) and Isnull(Z.FTStateNotAllocate,'0') = '0' "
            _Cmd &= vbCrLf & " and I.FNHSysCmpId=" & Integer.Parse("0" & Me.FNHSysCmpId.Properties.Tag)
            _Cmd &= vbCrLf & " And LEFT(I.FDIssueDate,7)=LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FDMonthUse.Text) & "',7)"
            _Cmd &= vbCrLf & " ) AS T  "
            _Cmd &= vbCrLf & " group by  FTOrderNo,LEFT(FDIssueDate,7) ,FNHSysCmpId "
            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If
            'End If

            _Cmd = "SELECT        FTOrderNo,LEFT(FDIssueDate,7) as  FDDate , sum(FNAmount) AS FNAmount ,FNHSysCmpId,FNHSysRawMatId FROM            ("
            _Cmd &= vbCrLf & " SELECT      O.FNHSysCmpId,  I.FTOrderNo, I.FDIssueDate, T.FTBarcodeNo, T.FNQuantity, B.FNHSysRawMatId, B.FNPrice, M.FTRawMatCode , (T.FNQuantity * B.FNPrice ) AS FNAmount"
            _Cmd &= vbCrLf & " , Case when Isdate(I.FDIssueDate) = 1 Then convert(varchar(10), convert(datetime,I.FDIssueDate),103) Else '' End AS FDDate "
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS I WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON I.FTOrderNo = O.FTOrderNo INNER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT AS T WITH (NOLOCK) ON I.FTIssueNo = T.FTDocumentNo AND I.FTOrderNo = T.FTOrderNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode AS B WITH (NOLOCK) ON T.FTBarcodeNo = B.FTBarcodeNo LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS M WITH (NOLOCK) ON B.FNHSysRawMatId = M.FNHSysRawMatId"
            _Cmd &= vbCrLf & "LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "]..TMERMMainMat AS Z WITH(NOLOCK) ON M.FTRawMatCode = Z.FTMainMatCode "
            _Cmd &= vbCrLf & "  WHERE  (O.FNOrderType in (2,3))  and Isnull(Z.FTStateNotAllocate,'0') = '0' "
            _Cmd &= vbCrLf & " and I.FNHSysCmpId=" & Integer.Parse("0" & Me.FNHSysCmpId.Properties.Tag)
            _Cmd &= vbCrLf & " And LEFT(I.FDIssueDate,7)=LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FDMonthUse.Text) & "',7)"
            _Cmd &= vbCrLf & " ) AS T  "
            _Cmd &= vbCrLf & " group by  FTOrderNo,LEFT(FDIssueDate,7) , FNHSysCmpId,FNHSysRawMatId "
            _oDt = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Cmd)


            _Cmd = "Delete From  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTALCOrder_Detail "
            _Cmd &= vbCrLf & "  WHERE FDMonth = LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FDMonthUse.Text) & "',7)"
            _Cmd &= vbCrLf & "and FNHSysCmpId=" & Integer.Parse("0" & Me.FNHSysCmpId.Properties.Tag)
            If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
            End If


            _Cmd = "Select Sum(FNAmtBaht) AS FNAmtBaht From(SELECT        FTOrderNo, RIGHT(FDDate,7) as FDDate   ,FNHSysCmpId , FTCmpCode , isnull(FNAmtBaht,0) AS FNAmtBaht  FROM            (" ' ,FNHSysRawMatId,FTRawMatName ,FTRawMatCode , FTRawMatSizeCode , FTRawMatColorCode  
            _Cmd &= vbCrLf & "SELECT        I.FTOrderNo, I.FDIssueDate "
            _Cmd &= vbCrLf & " , Case when Isdate(I.FDIssueDate) = 1 Then convert(varchar(10), convert(datetime,I.FDIssueDate),103) Else '' End AS FDDate "
            _Cmd &= vbCrLf & ", O.FNHSysCmpId , C.FTCmpCode , Z.FNAmtBaht "
            _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS I WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON I.FTOrderNo = O.FTOrderNo  "
            _Cmd &= vbCrLf & " LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WITH(NOLOCK)  ON O.FNHSysCmpId = C.FNHSysCmpId"

            _Cmd &= vbCrLf & "LEFT OUTER JOIN (SELECT        O.FTOrderNo, O.FDOrderDate, O.FNHSysCmpId, sum(convert(numeric(18,2),B.FNAmt*Isnull(FNSellingRate,1))) AS  FNAmtBaht  "
            _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) LEFT OUTER JOIN"
            _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS B WITH (NOLOCK) ON O.FTOrderNo = B.FTOrderNo"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS S WITH(NOLOCK) ON O.FTOrderNo = S.FTOrderNo and B.FTSubOrderNo = S.FTSubOrderNo"
            _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExchangeRate AS C WITH(NOLOCK) ON S.FNHSysCurId = C.FNHSysCurId and O.FDOrderDate = C.FDDate"
            _Cmd &= vbCrLf & "group by  O.FTOrderNo, O.FDOrderDate, O.FNHSysCmpId ) AS Z ON I.FTOrderNo = Z.FTOrderNo and O.FNHSysCmpId = Z.FNHSysCmpId"

            _Cmd &= vbCrLf & "  WHERE (O.FNOrderType   in (0,22,13))  "
            _Cmd &= vbCrLf & " and I.FNHSysCmpId=" & Integer.Parse("0" & Me.FNHSysCmpId.Properties.Tag)
            _Cmd &= vbCrLf & " And LEFT(I.FDIssueDate,7)=LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FDMonthUse.Text) & "',7)"
            _Cmd &= vbCrLf & " ) AS T  "
            _Cmd &= vbCrLf & " group by  FTOrderNo,RIGHT(FDDate,7) ,FNHSysCmpId , FTCmpCode , isnull(FNAmtBaht,0) ) AS X  "
            _AmtPro = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "0")

            For Each R As DataRow In _oDt.Rows

                '_Cmd = " SELECT        FTOrderNo   FROM            ("
                '_Cmd &= vbCrLf & " SELECT        I.FTOrderNo, I.FDIssueDate "
                '_Cmd &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS I WITH (NOLOCK) LEFT OUTER JOIN"
                '_Cmd &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON I.FTOrderNo = O.FTOrderNo  "
                '_Cmd &= vbCrLf & "           WHERE (O.FNOrderType not in (2,3))  and O.FNHSysCmpId=" & Integer.Parse("0" & R!FNHSysCmpId.ToString)
                '_Cmd &= vbCrLf & " And LEFT(I.FDIssueDate,7)=LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FDMonthUse.Text) & "',7)"
                '_Cmd &= vbCrLf & " ) AS T  "
                '_Cmd &= vbCrLf & "  group by  FTOrderNo "
                '_oDtCount = HI.Conn.SQLConn.GetDataTableOnbeginTrans(_Cmd)


                _Cmd = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTALCOrder_Detail ( FTInsUser, FDInsDate, FTInsTime,   FDMonth, FTOrderNo, FTOrderNoTo, FNAmount , FNHSysCmpId ,FNHSysRawMatId)"
                _Cmd &= vbCrLf & "SELECT   '" & HI.ST.UserInfo.UserName & "'"
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB
                _Cmd &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB
                _Cmd &= vbCrLf & ",LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FDMonthUse.Text) & "',7)"
                _Cmd &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & ",FTOrderNo,   convert(numeric(18,2), (Isnull(FNAmtBaht,0) * ( " & (Double.Parse(R!FNAmount.ToString) & ")) /" & _AmtPro) & ")"
                _Cmd &= vbCrLf & "," & Integer.Parse("0" & R!FNHSysCmpId.ToString)
                _Cmd &= vbCrLf & "," & Integer.Parse("0" & R!FNHSysRawMatId.ToString)
                _Cmd &= vbCrLf & " FROM            ("
                _Cmd &= vbCrLf & "SELECT        I.FTOrderNo, I.FDIssueDate,Z.FNAmtBaht"
                _Cmd &= vbCrLf & " , Case when Isdate(I.FDIssueDate) = 1 Then convert(varchar(10), convert(datetime,I.FDIssueDate),103) Else '' End AS FDDate "
                _Cmd &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENIssue AS I WITH (NOLOCK) LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) ON I.FTOrderNo = O.FTOrderNo "
                _Cmd &= vbCrLf & "LEFT OUTER JOIN (SELECT        O.FTOrderNo, O.FDOrderDate, O.FNHSysCmpId, sum(convert(numeric(18,2),B.FNAmt*Isnull(FNSellingRate,1))) AS  FNAmtBaht  "
                _Cmd &= vbCrLf & "FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH (NOLOCK) LEFT OUTER JOIN"
                _Cmd &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub_BreakDown AS B WITH (NOLOCK) ON O.FTOrderNo = B.FTOrderNo"
                _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..TMERTOrderSub AS S WITH(NOLOCK) ON O.FTOrderNo = S.FTOrderNo and B.FTSubOrderNo = S.FTSubOrderNo"
                _Cmd &= vbCrLf & " LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "]..TACCTExchangeRate AS C WITH(NOLOCK) ON S.FNHSysCurId = C.FNHSysCurId and O.FDOrderDate = C.FDDate"
                _Cmd &= vbCrLf & "group by  O.FTOrderNo, O.FDOrderDate, O.FNHSysCmpId  ) AS Z ON I.FTOrderNo = Z.FTOrderNo and O.FNHSysCmpId = Z.FNHSysCmpId"



                _Cmd &= vbCrLf & "  WHERE  (O.FNOrderType  in (0,22,13)) and I.FNHSysCmpId=" & Integer.Parse("0" & R!FNHSysCmpId.ToString)
                _Cmd &= vbCrLf & " And LEFT(I.FDIssueDate,7)=LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FDMonthUse.Text) & "',7) "
                _Cmd &= vbCrLf & " ) AS T  "

                _Cmd &= vbCrLf & " group by  FTOrderNo,LEFT(FDIssueDate,7) ,FNAmtBaht  "
                If HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

            Next

            For Each R As DataRow In _oDt.Rows

                _Cmd = "Select sum(FNAmount) AS FNAmount From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTALCOrder_Detail "
                _Cmd &= vbCrLf & "Where FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Cmd &= vbCrLf & " and FDMonth = LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FDMonthUse.Text) & "',7)"
                _Cmd &= vbCrLf & " and FNHSysCmpId=" & Integer.Parse("0" & R!FNHSysCmpId.ToString)
                _Cmd &= vbCrLf & " and FNHSysRawMatId=" & Integer.Parse("0" & R!FNHSysRawMatId.ToString)
                _QtyUse = HI.Conn.SQLConn.GetFieldOnBeginTrans(_Cmd, Conn.DB.DataBaseName.DB_ACCOUNT, "0")
                If _QtyUse > Double.Parse("0" & R!FNAmount.ToString) Then
                    _Cmd = " Update  Top (1)    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTALCOrder_Detail"
                    _Cmd &= vbCrLf & " Set FNAmount = FNAmount - " & (_QtyUse - Double.Parse("0" & R!FNAmount.ToString))
                    _Cmd &= vbCrLf & "Where FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                    _Cmd &= vbCrLf & " and FDMonth = LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FDMonthUse.Text) & "',7)"
                    _Cmd &= vbCrLf & " and FNHSysCmpId=" & Integer.Parse("0" & R!FNHSysCmpId.ToString)
                    _Cmd &= vbCrLf & " and FNHSysRawMatId=" & Integer.Parse("0" & R!FNHSysRawMatId.ToString)
                    _Cmd &= vbCrLf & " and FNAmount > " & (_QtyUse - Double.Parse("0" & R!FNAmount.ToString))
                    HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                ElseIf _QtyUse < Double.Parse("0" & R!FNAmount.ToString) Then
                    _Cmd = " Update  Top (1)   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTALCOrder_Detail "
                    _Cmd &= vbCrLf & "  Set FNAmount = FNAmount + " & (Double.Parse("0" & R!FNAmount.ToString) - _QtyUse)
                    _Cmd &= vbCrLf & "Where FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                    _Cmd &= vbCrLf & " and FDMonth = LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FDMonthUse.Text) & "',7)"
                    _Cmd &= vbCrLf & " and FNHSysCmpId=" & Integer.Parse("0" & R!FNHSysCmpId.ToString)
                    _Cmd &= vbCrLf & " and FNHSysRawMatId=" & Integer.Parse("0" & R!FNHSysRawMatId.ToString)
                    HI.Conn.SQLConn.Execute_Tran(_Cmd, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                End If
            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            Return False
        End Try
    End Function

    Private Sub ogvdetail_RowStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles ogvdetail.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                If e.RowHandle Mod 2 = 0 Then
                    e.Appearance.BackColor = Color.Salmon
                    e.Appearance.BackColor2 = Color.SeaShell
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvproduction_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvproduction.RowStyle
        Try
            Dim View As GridView = sender
            If (e.RowHandle >= 0) Then
                If e.RowHandle Mod 2 = 0 Then
                    e.Appearance.BackColor = Color.SkyBlue
                    e.Appearance.BackColor2 = Color.White
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmdelete_Click(sender As Object, e As EventArgs) Handles ocmdelete.Click
        If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete) Then
            If DeleteData() Then
                Call LoadData()
            End If
        End If
    End Sub

    Private Function DeleteData() As Boolean
        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _Str As String
            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTALCOrder WHERE FDMonth= LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FDMonthUse.Text) & "',7)"
            _Str &= vbCrLf & " and FNHSysCmpId=" & Integer.Parse("0" & Me.FNHSysCmpId.Properties.Tag)
            If HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                Return False
            End If

            _Str = "Delete From  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTALCOrder_Detail WHERE FDMonth= LEFT('" & HI.UL.ULDate.ConvertEnDB(Me.FDMonthUse.Text) & "',7)"
            _Str &= vbCrLf & " and FNHSysCmpId=" & Integer.Parse("0" & Me.FNHSysCmpId.Properties.Tag)
            HI.Conn.SQLConn.Execute_Tran(_Str, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function
End Class