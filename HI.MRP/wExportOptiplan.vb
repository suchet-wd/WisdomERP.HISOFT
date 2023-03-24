Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO
Imports Microsoft.Win32

Public Class wExportOptiplan

    Private Shared _SysImgPath As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images"
    Private Shared _MContextMenuStripGrid As System.Windows.Forms.ContextMenuStrip
    Private _wlistDataExportError As wExportOptiplanListOrder

    Private pBuyCode As String = ""
    Private pStyleCode As String = ""
    Private pOrderNo As String = ""
    Private pPath As String = ""

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Call CreateManuStripGrid()
        _DefailtPath = ""

        Try
            _DefailtPath = ReadRegistry()
        Catch ex As Exception

        End Try


        Dim oSysLang As New ST.SysLanguage
        _wlistDataExportError = New wExportOptiplanListOrder

        Try

            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wlistDataExportError.Name.ToString.Trim, _wlistDataExportError)
        Catch ex As Exception
        Finally
        End Try

        HI.TL.HandlerControl.AddHandlerObj(_wlistDataExportError)

    End Sub

#Region "Procedure"

    Private _DefailtPath As String

    Private Sub CreateOrderList()

        Try
            Dim _Qry As String
            Dim dt As DataTable = Nothing
            Dim dtorder As DataTable

            Dim StateLoad As Boolean
            Dim FNHSysStyleId As Integer = 0

            If Not (Me.ogdorder.DataSource Is Nothing) Then

                CType(Me.ogdorder.DataSource, DataTable).AcceptChanges()
                dt = CType(Me.ogdorder.DataSource, DataTable)

            Else

                _Qry = " SELECT   '1' AS FTSelect,  A.FTOrderNo, B.FNHSysStyleId, B.FTStyleCode,S.FTSeasonCode,A.FNHSysSeasonId, C.FTCmpCode"
                _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A INNER JOIN"
                _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B ON A.FNHSysStyleId = B.FNHSysStyleId"
                _Qry &= vbCrLf & "         INNER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS S ON A.FNHSysSeasonId = S.FNHSysSeasonId"
                _Qry &= vbCrLf & "        OUTER APPLY ( SELECT TOP 1 FTCmpCode FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WHERE C.FNHSysCmpId = A.FNHSysCmpId ) AS C "
                _Qry &= vbCrLf & "  WHERE (A.FNHSysStyleId =0 )"
                _Qry &= vbCrLf & "  AND (A.FNJobState IN (0,1))"

                'If Not (HI.ST.SysInfo.Admin) Then
                '    _Qry &= vbCrLf & "   AND A.FTOrderNo IN ( SELECT FTOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder WITH(NOLOCK) WHERE  FTOrderBy  IN (SELECT FTUserName FROM  dbo.FT_GetOrderPermission_PurchseTeam('" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') ) )"
                'End If

                _Qry &= vbCrLf & "  ORDER BY B.FTStyleCode"

                dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)
            End If

            With CType(Me.ogc.DataSource, DataTable)
                .AcceptChanges()

                For Each R As DataRow In .Rows '    Select("FTSelect='1'")
                    StateLoad = False

                    FNHSysStyleId = Integer.Parse(Val(R!FNHSysStyleId.ToString))

                    If dt.Select("FNHSysStyleId=" & FNHSysStyleId & "").Length <= 0 And R!FTSelect.ToString = "1" Then
                        StateLoad = True
                    Else

                        If R!FTSelect.ToString <> "1" Then
                            dt.BeginInit()
                            For Each Rx As DataRow In dt.Select("FNHSysStyleId=" & FNHSysStyleId & "")
                                Rx.Delete()
                            Next
                            dt.EndInit()
                        End If

                    End If

                    If (StateLoad) Then

                        _Qry = " SELECT   '1' AS FTSelect,  A.FTOrderNo, B.FNHSysStyleId, B.FTStyleCode,S.FTSeasonCode,A.FNHSysSeasonId,C.FTCmpCode"
                        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A INNER JOIN"
                        _Qry &= vbCrLf & "         [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS B ON A.FNHSysStyleId = B.FNHSysStyleId"
                        _Qry &= vbCrLf & "         INNER JOIN     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS S ON A.FNHSysSeasonId = S.FNHSysSeasonId"
                        _Qry &= vbCrLf & "        OUTER APPLY ( SELECT TOP 1 FTCmpCode FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS C WHERE C.FNHSysCmpId = A.FNHSysCmpId ) AS C "
                        _Qry &= vbCrLf & "  WHERE (A.FNHSysStyleId = " & FNHSysStyleId & ")"
                        _Qry &= vbCrLf & "  AND (A.FNHSysBuyId = " & Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString)) & ")"
                        _Qry &= vbCrLf & "  AND (A.FNJobState IN (0,1))"

                        If Not (HI.ST.SysInfo.Admin) Then
                            _Qry &= vbCrLf & "   AND A.FTOrderNo IN ( SELECT FTOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder WITH(NOLOCK) WHERE  FTOrderBy  IN (SELECT FTUserName FROM  dbo.FT_GetOrderPermission_PurchseTeam('" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') ) )"
                        End If

                        _Qry &= vbCrLf & "  ORDER BY  A.FTOrderNo "

                        dtorder = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, , False)

                        dt.Merge(dtorder.Copy)
                    Else

                    End If

                Next

            End With

            Me.ogdorder.DataSource = dt.Copy
            dt.Dispose()

        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadStyleInfo(ByVal FNHSysBuyId As String)
        Call ClearTab()

        Dim _dt As DataTable
        Dim _Str As String = ""
        Me.ogc.DataSource = Nothing
        Me.ochkselectall.Checked = True

        _Str = "SELECT '1' AS FTSelect, MS.FNHSysStyleId, MS.FTStyleCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " , MS.FTStyleNameTH AS FTStyleName "
        Else
            _Str &= vbCrLf & " , MS.FTStyleNameEN AS FTStyleName"
        End If

        _Str &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMStyle AS MS WITH(NOLOCK)  "
        _Str &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS O WITH(NOLOCK) ON MS.FNHSysStyleId = O.FNHSysStyleId  "
        _Str &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrderSub AS OSU WITH(NOLOCK) ON O.FTOrderNo = OSU.FTOrderNo  "
        _Str &= vbCrLf & " WHERE (O.FNHSysBuyId  =" & Val(FNHSysBuyId) & ")"

        If Not (HI.ST.SysInfo.Admin) Then
            _Str &= vbCrLf & "   AND O.FTOrderNo IN ( SELECT FTOrderNo FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder WITH(NOLOCK) WHERE  FTOrderBy  IN (SELECT FTUserName FROM  dbo.FT_GetOrderPermission_PurchseTeam('" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "') ) )"
        End If

        _Str &= vbCrLf & " GROUP BY  MS.FNHSysStyleId, MS.FTStyleCode"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Str &= vbCrLf & " , MS.FTStyleNameTH "
        Else
            _Str &= vbCrLf & " , MS.FTStyleNameEN "
        End If

        _Str &= vbCrLf & " ORDER BY MS.FTStyleCode"
        _dt = HI.Conn.SQLConn.GetDataTable(_Str, Conn.DB.DataBaseName.DB_MERCHAN)

        Me.ogc.DataSource = _dt.Copy()
        _dt.Dispose()
        Me.otb.SelectedTabPageIndex = 0
        Call CreateOrderList()

    End Sub

    Private Sub ClearTab()
        Me.otb.TabPages.Clear()
    End Sub

    Private Function GenerateOrderList(_FNHSysStyleId As Integer, _FNHSysSeasonId As Integer) As String
        Dim _OrderList As String = ""
        With CType(Me.ogdorder.DataSource, DataTable)
            .AcceptChanges()

            For Each R As DataRow In .Select("FTSelect='1' AND FNHSysStyleId =" & _FNHSysStyleId & " AND FNHSysSeasonId=" & _FNHSysSeasonId & "")
                'For Each R As DataRow In .Select("FTSelect='1' AND FNHSysStyleId =" & _FNHSysStyleId & " ")
                If _OrderList = "" Then
                    _OrderList = R!FTOrderNo.ToString
                Else
                    _OrderList &= "|" & R!FTOrderNo.ToString
                End If
            Next

            Dim dtins As New DataTable
            dtins = .Select("FTSelect='1'").CopyToDataTable()
            If dtins Is Nothing Then
                dtins.Columns.Add("FTOrderNo", GetType(String))

            Else

                dtins.BeginInit()
                For Each Col As DataColumn In .Columns
                    If Col.ColumnName = "FTOrderNo" Then
                    Else
                        dtins.Columns.Remove(Col.ColumnName)
                    End If
                Next
                dtins.BeginInit()

            End If

            HI.Conn.SQLConn.ExecuteStoredProcedure(HI.ST.UserInfo.UserName, "USP_IMPORTTEMPORDERNO", "@tblOrder", dtins, Conn.DB.DataBaseName.DB_MERCHAN)




        End With

        Return _OrderList
    End Function

    Private Sub LoadData()
        Call ClearTab()
        Dim _Spls As New HI.TL.SplashScreen("Generating Data.. Please Wait ")
        Try

            Dim _Rec As Integer = 0
            Dim _TotalRec As Integer = CType(Me.ogc.DataSource, DataTable).Select("FTSelect='1'").Length

            Dim FNHSysStyleId As Integer
            Dim FTStyleCode As String
            Dim _Qry As String = ""
            Dim dt As New DataTable
            Dim dttmp As New DataTable
            Dim dtcheck As New DataTable
            Dim dts As New DataSet
            Dim _OrderList As String = ""
            Dim _dtorder As DataTable
            Dim _FNHSysSeasonId As Integer = 0
            Dim _FNSTQuantity As Integer = 0
            Dim Supl As String = ""
            Call CreateOrderList()

            With CType(ogdorder.DataSource, DataTable)
                .AcceptChanges()
                _dtorder = .Copy
            End With

            dtcheck = Nothing

            pBuyCode = FNHSysBuyId.Text
            pStyleCode = ""
            pOrderNo = ""

            For Each Rx As DataRow In CType(Me.ogc.DataSource, DataTable).Select("FTSelect='1'")
                FNHSysStyleId = Integer.Parse(Val(Rx!FNHSysStyleId.ToString))
                FTStyleCode = Rx!FTStyleCode.ToString

                If pStyleCode = "" Then
                    pStyleCode = FTStyleCode
                Else
                    pStyleCode = pStyleCode & "," & FTStyleCode
                End If

                If _dtorder.Select("FNHSysStyleId=" & FNHSysStyleId & " AND FTSelect='1'").Length > 0 Then

                    Dim grp As List(Of String) = (_dtorder.Select("FNHSysStyleId=" & FNHSysStyleId & " AND FTSelect='1'", "FTSeasonCode").CopyToDataTable).AsEnumerable() _
                                                   .Select(Function(r) r.Field(Of String)("FTSeasonCode")) _
                                                   .Distinct() _
                                                   .ToList()

                    For Each IndSeasonCode As String In grp

                        _FNHSysSeasonId = Integer.Parse(Val(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysSeasonId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMSeason AS X WITH(NOLOCK) WHERE FTSeasonCode='" & HI.UL.ULF.rpQuoted(IndSeasonCode) & "'", Conn.DB.DataBaseName.DB_MASTER, "0")))

                        _Rec = _Rec + 1
                        _Spls.UpdateInformation("Generating Style " & FTStyleCode & "  Record  " & _Rec.ToString & " Of " & _TotalRec.ToString & "  (" & Format((_Rec * 100.0) / _TotalRec, "0.00") & " % ) ")
                        _OrderList = GenerateOrderList(FNHSysStyleId, _FNHSysSeasonId)

                        If pOrderNo = "" Then
                            pOrderNo = _OrderList + "(" & IndSeasonCode & ")"
                        Else
                            pOrderNo = pOrderNo & "," & _OrderList + "(" & IndSeasonCode & ")"
                        End If

                        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_EXPORT_OPTOPLAN " & FNHSysStyleId & ",'" & HI.UL.ULF.rpQuoted(FTStyleCode) & "','" & Me.FNHSysBuyId.Text & "','" & HI.UL.ULF.rpQuoted(_OrderList) & "'," & Me.FNCutCm.Value & " ," & _FNHSysSeasonId & ",'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

                        dts = New DataSet
                        HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, dts)

                        If dts.Tables.Count >= 2 Then

                            dt = dts.Tables(0).Copy

                            If dts.Tables(1).Rows.Count > 0 Then
                                dttmp = dts.Tables(1).Copy

                                dttmp.Columns.Add("FTStyleCode", GetType(String))

                                For Each Rtdata As DataRow In dttmp.Rows
                                    Rtdata!FTStyleCode = FTStyleCode
                                Next

                                If dtcheck Is Nothing Then
                                    dtcheck = dttmp.Copy
                                Else
                                    dtcheck.Merge(dttmp.Copy)
                                End If

                            End If

                            Try
                                _FNSTQuantity = Val(dts.Tables(2).Rows(0).Item(0).ToString())
                            Catch ex As Exception
                                _FNSTQuantity = 0
                            End Try

                            Try
                                Supl = dts.Tables(3).Rows(0).Item(0).ToString()

                                If Supl <> "" Then
                                    Supl = "-" & Supl
                                End If
                            Catch ex As Exception
                                Supl = ""
                            End Try

                            If dt.Rows.Count > 0 Then
                                Dim Otp As New DevExpress.XtraTab.XtraTabPage()
                                Dim ogcn As New DevExpress.XtraGrid.GridControl
                                Dim ogvn As New DevExpress.XtraGrid.Views.Grid.GridView
                                Dim Colg As New DevExpress.XtraGrid.Columns.GridColumn()

                                With Colg
                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Caption = "Data"
                                    .FieldName = "FTData"
                                    .Name = "TGCOL" & FNHSysStyleId.ToString
                                    .OptionsColumn.AllowEdit = False
                                    .OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
                                    .OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
                                    .OptionsColumn.AllowMove = False
                                    .OptionsColumn.AllowShowHide = False
                                    .OptionsColumn.ReadOnly = True
                                    .Visible = True
                                    .VisibleIndex = 0
                                    .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                                    .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .BestFit()
                                    .Width = 500
                                End With

                                With Otp
                                    .Name = "T" & FNHSysStyleId.ToString & _FNHSysSeasonId.ToString
                                    .Text = FTStyleCode & Supl & " ( " & IndSeasonCode & ") ( QTY  " & Format(_FNSTQuantity, "#,0") & " )"
                                End With

                                With ogvn
                                    .Name = "TGV" & FNHSysStyleId.ToString & _FNHSysSeasonId.ToString
                                    .Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Colg})
                                    .GridControl = ogcn
                                    .OptionsCustomization.AllowGroup = False
                                    .OptionsCustomization.AllowQuickHideColumns = False
                                    .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
                                    .OptionsView.ColumnAutoWidth = False
                                    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                                    .OptionsView.ShowGroupPanel = False
                                    .OptionsView.ShowAutoFilterRow = False
                                    .OptionsPrint.AutoWidth = False
                                    .OptionsPrint.PrintHeader = False
                                    .BestFitColumns()
                                End With

                                With ogcn
                                    .Name = "TG" & FNHSysStyleId.ToString & _FNHSysSeasonId.ToString
                                    ' .Dock = System.Windows.Forms.DockStyle.Fill
                                    .MainView = ogvn
                                    .ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {ogvn})
                                    .ContextMenuStrip = _MContextMenuStripGrid
                                End With
                                Otp.Controls.Add(ogcn)

                                otb.TabPages.Add(Otp)
                                ogcn.Dock = DockStyle.Fill

                                ogcn.DataSource = dt.Copy

                            End If
                        End If

                    Next

                End If
              
            Next
            dt.Dispose()
            _Spls.Close()

            If Not (dtcheck Is Nothing) Then

                Try
                    If dtcheck.Rows.Count > 0 Then

                        HI.MG.ShowMsg.mInfo("พบข้อมูลบางรายการ ที่ Breakdown มาไม่ครบ กรุณาทำการตรวจสอบ ข้อมูล Boom Sheet !!!", 1602035471, Me.Text, , MessageBoxIcon.Warning)

                        With _wlistDataExportError
                            .ogvlist.ActiveFilter.Clear()
                            .ogclist.DataSource = dtcheck.Copy
                            .ogclist.Refresh()
                            .ShowDialog()
                        End With

                    End If

                Catch ex As Exception
                End Try

            End If

            Try
                dtcheck.Dispose()
            Catch ex As Exception

            End Try
        Catch ex As Exception
            _Spls.Close()
        End Try


    End Sub

    Private Sub LoadDataMulti()
        Call ClearTab()
        Dim _Spls As New HI.TL.SplashScreen("Generating Data.. Please Wait ")
        Try

            Dim _Rec As Integer = 0
            Dim _TotalRec As Integer = CType(Me.ogc.DataSource, DataTable).Select("FTSelect='1'").Length

            Dim _Qry As String = ""
            Dim dt As New DataTable
            Dim dttmp As New DataTable
            Dim dtcheck As New DataTable
            Dim dts As New DataSet
            Dim _OrderList As String = ""
            Dim _dtorder As DataTable

            Dim _FNSTQuantity As Integer = 0
            Dim Supl As String = ""
            Call CreateOrderList()

            With CType(ogdorder.DataSource, DataTable)
                .AcceptChanges()
                _dtorder = .Copy
            End With

            dtcheck = Nothing

            pBuyCode = FNHSysBuyId.Text
            pStyleCode = ""
            pOrderNo = ""

            Dim cmdstring As String = ""
            cmdstring = "delete from  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempExportOptiplan where UserLogin='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)
            Dim MultiStyle As String = ""


            Dim grpstyle As List(Of String) = (_dtorder.Select(" FTSelect='1'", "FTStyleCode").CopyToDataTable).AsEnumerable() _
                                                   .Select(Function(r) r.Field(Of String)("FTStyleCode")) _
                                                   .Distinct() _
                                                   .ToList()


            For Each st As String In grpstyle

                If MultiStyle = "" Then
                    MultiStyle = st
                Else
                    MultiStyle = MultiStyle & "+" & st
                End If

                If pStyleCode = "" Then
                    pStyleCode = st
                Else
                    pStyleCode = pStyleCode & "," & st
                End If

            Next


            Dim grp As List(Of String) = (_dtorder.Select(" FTSelect='1'", "FTSeasonCode").CopyToDataTable).AsEnumerable() _
                                                   .Select(Function(r) r.Field(Of String)("FTSeasonCode")) _
                                                   .Distinct() _
                                                   .ToList()

            For Each IndSeasonCode As String In grp

                _OrderList = ""
                For Each Rx As DataRow In _dtorder.Select(" FTSelect='1' AND FTSeasonCode='" & IndSeasonCode & "'")
                    If _OrderList = "" Then
                        _OrderList = Rx!FTOrderNo.ToString()
                    Else
                        _OrderList = _OrderList & "," & Rx!FTOrderNo.ToString()
                    End If
                Next

                If pOrderNo = "" Then
                    pOrderNo = _OrderList + "(" & IndSeasonCode & ")"
                Else
                    pOrderNo = pOrderNo & "," & _OrderList + "(" & IndSeasonCode & ")"
                End If

            Next
            For Each Rx As DataRow In _dtorder.Select(" FTSelect='1'")

                'If MultiStyle = "" Then
                '    MultiStyle = Rx!FTStyleCode.ToString()
                'Else
                '    MultiStyle = MultiStyle & "+" & Rx!FTStyleCode.ToString
                'End If

                cmdstring = "insert into  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTTempExportOptiplan (UserLogin, FNHSysStyleId, FTStyleCode, FTOrderNo) "
                cmdstring &= vbCrLf & " select  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                cmdstring &= vbCrLf & " ," & Val(Rx!FNHSysStyleId.ToString()) & ""
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rx!FTStyleCode.ToString()) & "'"
                cmdstring &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(Rx!FTOrderNo.ToString()) & "'"
                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_MERCHAN)


            Next



            _Rec = _Rec + 1

                        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.SP_EXPORT_OPTOPLAN_MULTI '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & Me.FNHSysBuyId.Text & "'," & Me.FNCutCm.Value & "  "

                        dts = New DataSet
                        HI.Conn.SQLConn.GetDataSet(_Qry, Conn.DB.DataBaseName.DB_MERCHAN, dts)

                        If dts.Tables.Count >= 2 Then

                            dt = dts.Tables(0).Copy

                            If dts.Tables(1).Rows.Count > 0 Then
                                dttmp = dts.Tables(1).Copy

                                dttmp.Columns.Add("FTStyleCode", GetType(String))

                                For Each Rtdata As DataRow In dttmp.Rows
                                    Rtdata!FTStyleCode = FTStyleCode
                                Next

                                If dtcheck Is Nothing Then
                                    dtcheck = dttmp.Copy
                                Else
                                    dtcheck.Merge(dttmp.Copy)
                                End If

                            End If

                Try
                    _FNSTQuantity = Val(dts.Tables(2).Rows(0).Item(0).ToString())
                Catch ex As Exception
                    _FNSTQuantity = 0
                End Try

                Try
                    Supl = dts.Tables(3).Rows(0).Item(0).ToString()

                    If Supl <> "" Then
                        Supl = "-" & Supl
                    End If
                Catch ex As Exception
                    Supl = ""
                End Try

                If dt.Rows.Count > 0 Then
                                Dim Otp As New DevExpress.XtraTab.XtraTabPage()
                                Dim ogcn As New DevExpress.XtraGrid.GridControl
                                Dim ogvn As New DevExpress.XtraGrid.Views.Grid.GridView
                                Dim Colg As New DevExpress.XtraGrid.Columns.GridColumn()

                                With Colg
                                    .AppearanceHeader.Options.UseTextOptions = True
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .Caption = "Data"
                                    .FieldName = "FTData"
                                    .Name = "TGCOL" & "MultipleDataExportX"
                                    .OptionsColumn.AllowEdit = False
                                    .OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.[False]
                                    .OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.[True]
                                    .OptionsColumn.AllowMove = False
                                    .OptionsColumn.AllowShowHide = False
                                    .OptionsColumn.ReadOnly = True
                                    .Visible = True
                                    .VisibleIndex = 0
                                    .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
                                    .OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True
                                    .OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains
                                    .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                                    .BestFit()
                                    .Width = 500
                                End With

                        With Otp
                            .Name = "T" & "MultipleDataExport"
                        .Text = MultiStyle & Supl & " ( QTY  " & Format(_FNSTQuantity, "#,0") & " )"
                    End With

                        With ogvn
                                    .Name = "TGV" & "MultipleDataExport"
                                    .Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Colg})
                                    .GridControl = ogcn
                                    .OptionsCustomization.AllowGroup = False
                                    .OptionsCustomization.AllowQuickHideColumns = False
                                    .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
                                    .OptionsView.ColumnAutoWidth = False
                                    .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
                                    .OptionsView.ShowGroupPanel = False
                                    .OptionsView.ShowAutoFilterRow = False
                                    .OptionsPrint.AutoWidth = False
                                    .OptionsPrint.PrintHeader = False
                                    .BestFitColumns()
                                End With

                                With ogcn
                                    .Name = "TG" & "MultipleDataExport"
                                    ' .Dock = System.Windows.Forms.DockStyle.Fill
                                    .MainView = ogvn
                                    .ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {ogvn})
                                    .ContextMenuStrip = _MContextMenuStripGrid
                                End With
                                Otp.Controls.Add(ogcn)

                                otb.TabPages.Add(Otp)
                                ogcn.Dock = DockStyle.Fill

                                ogcn.DataSource = dt.Copy

                            End If
                        End If


            dt.Dispose()
            _Spls.Close()

            If Not (dtcheck Is Nothing) Then

                Try
                    If dtcheck.Rows.Count > 0 Then

                        HI.MG.ShowMsg.mInfo("พบข้อมูลบางรายการ ที่ Breakdown มาไม่ครบ กรุณาทำการตรวจสอบ ข้อมูล Boom Sheet !!!", 1602035471, Me.Text, , MessageBoxIcon.Warning)

                        With _wlistDataExportError
                            .ogvlist.ActiveFilter.Clear()
                            .ogclist.DataSource = dtcheck.Copy
                            .ogclist.Refresh()
                            .ShowDialog()
                        End With

                    End If

                Catch ex As Exception
                End Try

            End If

            Try
                dtcheck.Dispose()
            Catch ex As Exception

            End Try
        Catch ex As Exception
            _Spls.Close()
        End Try


    End Sub
    Private Sub CreateManuStripGrid()

        _MContextMenuStripGrid = New System.Windows.Forms.ContextMenuStrip
        Dim _ExportToExcel As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToCsv As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToPDF As New System.Windows.Forms.ToolStripMenuItem
        Dim _ExportToText As New System.Windows.Forms.ToolStripMenuItem

        With _ExportToExcel
            .Name = "ocmExportToExcel"
            .Text = "Export To Excel"

            Dim tPathImg As String = _SysImgPath & "\Func\ExportToExcel.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If

            AddHandler .Click, AddressOf HI.TL.HandlerControl.ExportToExcel_Click
        End With

        With _ExportToCsv
            .Name = "ocmExportToCsv"
            .Text = "Export To CSV"
            Dim tPathImg As String = _SysImgPath & "\Func\ExportToCSV.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf HI.TL.HandlerControl.ExportToCSV_Click
        End With

        With _ExportToPDF
            .Name = "ocmExportToPDF"
            .Text = "Export To PDF"
            Dim tPathImg As String = _SysImgPath & "\Func\ExportToPDF.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf HI.TL.HandlerControl.ExportToPDF_Click
        End With

        With _ExportToText
            .Name = "ocmExportToText"
            .Text = "Export To Text"
            Dim tPathImg As String = _SysImgPath & "\Func\ExportToText.png"
            If IO.File.Exists(tPathImg) Then
                .Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes(tPathImg)))
            End If
            AddHandler .Click, AddressOf HI.TL.HandlerControl.ExportToText_Click
        End With

        With _MContextMenuStripGrid
            .Name = "ContextMenuGrid"
            .Items.AddRange(New System.Windows.Forms.ToolStripItem() {_ExportToExcel, _ExportToCsv, _ExportToPDF, _ExportToText})
        End With

    End Sub

    Private Function verify()
        Dim _Pass As Boolean = False
        If Me.FNHSysBuyId.Text <> "" Then
            If Me.FNHSysBuyId.Properties.Tag.ToString <> "" Then
                If Not (Me.ogc.DataSource Is Nothing) Then
                    If CType(Me.ogc.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then
                        _Pass = True
                    Else
                        HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Style", 1406120003, Me.Text)
                    End If
                Else
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Style", 1406120002, Me.Text)

                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysBuyId_lbl.Text)
                FNHSysBuyId.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, FNHSysBuyId_lbl.Text)
            FNHSysBuyId.Focus()
        End If
        Return _Pass
    End Function
#End Region

    Private Sub FNHSysBuyId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysBuyId.EditValueChanged
        If (Me.InvokeRequired) Then
            Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysBuyId_EditValueChanged), New Object() {sender, e})
        Else
            If FNHSysBuyId.Text <> "" Then
                FNHSysBuyId.Properties.Tag = HI.Conn.SQLConn.GetField("Select Top 1 FNHSysBuyId From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TMERMBuy AS A WITH(NOLOCK) WHERE FTBuyCode='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' ", Conn.DB.DataBaseName.DB_MASTER, "")
                Call LoadStyleInfo(FNHSysBuyId.Properties.Tag.ToString)
                'FNCutCm.Value = 2.54
                FNCutCm.Value = 0
            End If
        End If
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Call ClearTab()
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmrefresh_Click(sender As Object, e As EventArgs) Handles ocmrefresh.Click
        Call FNHSysBuyId_EditValueChanged(FNHSysBuyId, New System.EventArgs)
        'FNCutCm.Value = 2.54
        FNCutCm.Value = 0
    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmloaddata.Click
        If Me.verify Then
            If FNExportOptiplanType.SelectedIndex = 1 Then
                Call Me.LoadDataMulti()
            Else
                Call Me.LoadData()
            End If

        End If
    End Sub

    Private Sub ocmexportoptiplan_Click(sender As Object, e As EventArgs) Handles ocmexportoptiplan.Click
        If Me.otb.TabPages.Count > 0 Then
            Try

                If Me.ogv.RowCount <= 0 Then
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมํลที่ต้องการทำการ Export !!!", 1406110009, Me.Text)
                    Exit Sub
                End If

                Dim Op As New System.Windows.Forms.FolderBrowserDialog

                If _DefailtPath <> "" Then
                    Op.SelectedPath = _DefailtPath
                End If

                Try
                    If Op.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                        If _DefailtPath <> Op.SelectedPath Then

                            WriteRegistry(Op.SelectedPath)
                            _DefailtPath = Op.SelectedPath

                        End If

                        pPath = _DefailtPath

                        For Each T As DevExpress.XtraTab.XtraTabPage In Me.otb.TabPages
                            Dim FileName As String = Op.SelectedPath & "\" & T.Text.Replace("/", "_").Replace("\", "_").Replace("%", "_").Replace("(", "").Replace(")", "").Replace(",", "") & "_" & Me.FNHSysBuyId.Text.Replace("/", "_").Replace("\", "_").Replace("%", "_").Replace("(", "").Replace(")", "") & ".txt"

                            For Each Obj As Object In T.Controls.Find("TG" & Microsoft.VisualBasic.Right(T.Name.ToString, T.Name.ToString.Length - 1), True)

                                Try

                                    With CType(Obj, DevExpress.XtraGrid.GridControl)
                                        .ExportToText(FileName)
                                    End With

                                Catch ex As Exception
                                End Try

                                Exit For

                            Next

                        Next

                        Call CreateLogExportOptiplan()

                        HI.MG.ShowMsg.mInfo("Export Data Complete..", 1406120400, Me.Text, , MessageBoxIcon.Information)

                    End If

                Catch ex As Exception
                End Try

            Catch ex As Exception
            End Try
        Else
            HI.MG.ShowMsg.mInfo("ไม่พบข้อมูลที่ต้องการทำการ Export ", 1406120399, Me.Text, , MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub ochkselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ochkselectall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With ogc
                If Not (.DataSource Is Nothing) And ogv.RowCount > 0 Then

                    With ogv
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub chkogdorder_CheckedChanged(sender As Object, e As EventArgs) Handles chkogdorder.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.chkogdorder.Checked Then
                _State = "1"
            End If

            With ogdorder
                If Not (.DataSource Is Nothing) And ogvorder.RowCount > 0 Then

                    With ogvorder
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()

                End If
            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub otbstyleandorder_Click(sender As Object, e As EventArgs) Handles otbstyleandorder.Click
    End Sub

    Private Sub otbstyleandorder_SelectedPageChanging(sender As Object, e As DevExpress.XtraTab.TabPageChangingEventArgs) Handles otbstyleandorder.SelectedPageChanging
        Select Case e.Page.Name
            Case otporder.Name
                Call CreateOrderList()
            Case Else
        End Select
    End Sub

    Public Shared Function ReadRegistry() As String
        Dim regKey As RegistryKey
        Dim valreturn As String = ""

        regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        If regKey Is Nothing Then

            Registry.CurrentUser.CreateSubKey("Software\HI SOFT", RegistryKeyPermissionCheck.ReadWriteSubTree)
            regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        End If

        valreturn = regKey.GetValue("PathExportOptiplan", "")
        regKey.Close()

        Return valreturn
    End Function

    Public Shared Sub WriteRegistry(ByVal value As Object)

        Dim regKey As RegistryKey
        regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        If regKey Is Nothing Then

            Registry.CurrentUser.CreateSubKey("Software\HI SOFT", True)
            regKey = Registry.CurrentUser.OpenSubKey("Software\HI SOFT", True)

        End If

        regKey.SetValue("PathExportOptiplan", value.ToString)
        regKey.Close()

    End Sub

    Private Sub wExportOptiplan_Load(sender As Object, e As EventArgs) Handles Me.Load

        'FNCutCm.Value = 2.54
        FNCutCm.Value = 0

    End Sub

    Private Sub CreateLogExportOptiplan()
        Dim StrSql As String = ""
        StrSql = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_LOG) & "].dbo.HSysAuditLogExportOptiplan  "
        StrSql &= vbCrLf & "  (FTExportOpUser, FDExportOpDate, FTExportOpTime, FTExportOpBuy, FTExportOpStyle, FTExportOpOrder, FTExportOpPath)  "
        StrSql &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        StrSql &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
        StrSql &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
        StrSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pBuyCode) & "'"
        StrSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pStyleCode) & "'"
        StrSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pOrderNo) & "'"
        StrSql &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(pPath) & "'"

        HI.Conn.SQLConn.ExecuteOnly(StrSql, Conn.DB.DataBaseName.DB_LOG)

    End Sub

End Class