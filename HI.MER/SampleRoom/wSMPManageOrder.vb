Imports DevExpress.Data
Imports DevExpress.XtraEditors.Controls
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Public Class wSMPManageOrder

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean

    Private mSelectItem As wSMPManageOrderBomItem

    Private mSelectItemMaster As wSMPManageOrderAddItemMaster
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        mSelectItem = New wSMPManageOrderBomItem()

        HI.TL.HandlerControl.AddHandlerObj(mSelectItem)

        Dim oSysLang As New HI.ST.SysLanguage

        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, mSelectItem.Name.ToString.Trim, mSelectItem)
        Catch ex As Exception
        End Try



        mSelectItemMaster = New wSMPManageOrderAddItemMaster()

        HI.TL.HandlerControl.AddHandlerObj(mSelectItemMaster)



        Try
            Call oSysLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleID, mSelectItemMaster.Name.ToString.Trim, mSelectItemMaster)
        Catch ex As Exception
        End Try

        Call InitGrid()



    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        'Dim sFieldCount As String = ""
        'Dim sFieldSum As String = "FNBalQuantity"

        'Dim sFieldGrpCount As String = ""
        'Dim sFieldGrpSum As String = "FNBalQuantity"

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

        '    For Each Str As String In sFieldSum.Split("|")
        '        If Str <> "" Then
        '            .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
        '            .Columns(Str).SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit.ToString & "}"
        '        End If
        '    Next

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

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _TotalRow As Integer = 0
        Dim _Rx As Integer = 0

        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")


        _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.USP_GETDATASAMPLE_MANAGE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & FNListDocumentTrackPIData.SelectedIndex & ",'" & HI.UL.ULDate.ConvertEnDB(FTStartPurchaseDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndPurchaseDate.Text) & "'," & FNSMPOrderTypeListTrack.SelectedIndex & "," & Val(FNHSysBuyId.Properties.Tag) & " "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

        Me.ogdtime.DataSource = _dt.Copy

        _dt.Dispose()
        _Spls.Close()

        _RowDataChange = False

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = True



        'If Me.FTSMPOrderNo.Text <> "" And FTSMPOrderNo.Properties.Tag.ToString <> "" Then
        '    _Pass = True
        'End If

        'If Me.FTSMPOrderNoTo.Text <> "" And FTSMPOrderNoTo.Properties.Tag.ToString <> "" Then
        '    _Pass = True
        'End If


        'If Me.FNHSysWHId.Text <> "" And FNHSysWHId.Properties.Tag.ToString <> "" Then
        '    _Pass = True
        'End If



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


    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ochkselectall_CheckedChanged(sender As Object, e As EventArgs) Handles ochkselectall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With ogdtime
                If Not (.DataSource Is Nothing) And ogvtime.RowCount > 0 Then

                    With ogvtime
                        For I As Integer = 0 To .RowCount - 1

                            If _State = "1" Then
                                If CheckOwner(.GetRowCellValue(I, "FTSelect").ToString) Then
                                    .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSMPOrderBy"), _State)
                                End If
                            Else
                                .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                            End If

                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub


    Private Function CheckOwner(SMPOrderBy As String, Optional mShowMessage As Boolean = False) As Boolean
        If (HI.ST.UserInfo.UserName.ToUpper = SMPOrderBy) Or (HI.ST.SysInfo.Admin) Or SMPOrderBy = "" Then
            Return True
        Else


            Dim _Qry As String = ""
            Dim _Qry2 As String = ""
            Dim _FNHSysTeamGrpId As Integer = 0
            Dim _FNHSysTeamGrpIdTo As Integer = 0

            _Qry = "SELECT TOP 1  FNHSysMerTeamId  "
            _Qry &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(SMPOrderBy) & "' "
            _FNHSysTeamGrpId = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SECURITY, "")))

            _Qry2 = "SELECT TOP 1  FNHSysMerTeamId  "
            _Qry2 &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.[TSEUserLogin] AS A WITH(NOLOCK) "
            _Qry2 &= vbCrLf & "   WHERE  FTUserName = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'  "
            _FNHSysTeamGrpIdTo = Integer.Parse(Val(HI.Conn.SQLConn.GetField(_Qry2, Conn.DB.DataBaseName.DB_SECURITY, "")))

            If _FNHSysTeamGrpId > 0 Then

                If _FNHSysTeamGrpId = _FNHSysTeamGrpIdTo Then
                    Return True
                Else
                    If mShowMessage Then
                        HI.MG.ShowMsg.mProcessError(1405280001, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข PO นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                    End If

                    Return False
                End If

            Else

                If mShowMessage Then
                    HI.MG.ShowMsg.mProcessError(1405280001, "คุณไม่มีสิทธิ์ทำการลบหรือแก้ไข PO นี้ ", Me.Text, System.Windows.Forms.MessageBoxIcon.Warning)
                End If
                Return False

            End If


        End If

    End Function

    Private Sub RepositoryItemSelect_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles RepositoryItemSelect.EditValueChanging

        Try

            Select Case ogvtime.FocusedColumn.FieldName.ToString
                Case "FTSelect"

                    Dim PoNo As String = ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTSMPOrderBy").ToString()

                    Dim FieldName As String = ogvtime.FocusedColumn.FieldName.ToString
                    Dim State As String = "0"
                    If e.NewValue.ToString = "1" Then


                        If CheckOwner(ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTSMPOrderBy").ToString, True) Then
                            e.Cancel = False
                            ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, ogvtime.FocusedColumn.FieldName.ToString, "1")
                        Else
                            e.Cancel = True
                        End If


                    Else
                        e.Cancel = False
                        ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, ogvtime.FocusedColumn.FieldName.ToString, "0")
                    End If






            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Function GenerateOrderList() As String
        Dim _OrderList As String = ""
        With CType(Me.ogdtime.DataSource, DataTable)
            .AcceptChanges()



            Dim dtins As New DataTable
            dtins = .Select("FTSelect='1'").CopyToDataTable()
            If dtins Is Nothing Then
                dtins.Columns.Add("FTSMPOrderNo", GetType(String))

            Else

                dtins.BeginInit()
                For Each Col As DataColumn In .Columns
                    If Col.ColumnName = "FTSMPOrderNo" Then
                    Else
                        dtins.Columns.Remove(Col.ColumnName)
                    End If
                Next
                dtins.BeginInit()

            End If

            HI.Conn.SQLConn.ExecuteStoredProcedure(HI.ST.UserInfo.UserName, "USP_IMPORTTEMPORDERNO", "@tblOrder", dtins, Conn.DB.DataBaseName.DB_SAMPLE)




        End With

        Return _OrderList
    End Function

    Private Sub ocmimportcomponentfrombom_Click(sender As Object, e As EventArgs) Handles ocmimportcomponentfrombom.Click
        Dim dtpo As DataTable
        Try
            With CType(Me.ogdtime.DataSource, DataTable)
                .AcceptChanges()

                dtpo = .Copy()

            End With

            If dtpo.Select("FTSelect='1' ").Length <= 0 Then

                Exit Sub
            End If

            Call GenerateOrderList()
            Call LadBOMDataItem()

        Catch ex As Exception

        End Try



    End Sub


    Private Sub ocmaddmaterial_Click(sender As Object, e As EventArgs) Handles ocmaddmaterial.Click
        Dim dtpo As DataTable
        Try
            With CType(Me.ogdtime.DataSource, DataTable)
                .AcceptChanges()

                dtpo = .Copy()

            End With

            If dtpo.Select("FTSelect='1' ").Length <= 0 Then

                Exit Sub
            End If

            Call GenerateOrderList()

            Dim Spls As New HI.TL.SplashScreen("Loading Master Material...   Please Wait   ")

            Dim cmdstring As String = ""

            cmdstring = " Delete FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TEMPBOMADDMAT WHERE FTUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            cmdstring &= vbCrLf & " Select '0' AS FTSelect, A.FNHSysRawMatId, A.FTRawMatCode, A.FTRawMatNameTH, A.FTRawMatNameEN, A.FNHSysRawMatColorId, A.FNHSysRawMatSizeId, A.FTRawMatColorNameTH, A.FTRawMatColorNameEN, A.FNHSysUnitId, "
            cmdstring &= vbCrLf & "   A.FTFabricFrontSize, MM.FNHSysSuplId, S_1.FTSuplCode, C.FTRawMatColorCode, S.FTRawMatSizeCode,U.FTUnitCode"
            cmdstring &= vbCrLf & " From  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TINVENMMaterial As A WITH(NOLOCK) INNER Join"
            cmdstring &= vbCrLf & "   " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TMERMMainMat As MM WITH(NOLOCK) On A.FTRawMatCode = MM.FTMainMatCode LEFT OUTER Join"
            cmdstring &= vbCrLf & "   " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TINVENMMatColor As C WITH(NOLOCK)  On A.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER Join"
            cmdstring &= vbCrLf & "   " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TINVENMMatSize As S WITH(NOLOCK)  On A.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId LEFT OUTER Join"
            cmdstring &= vbCrLf & "   " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMSupplier As S_1 WITH(NOLOCK)  On MM.FNHSysSuplId = S_1.FNHSysSuplId"
            cmdstring &= vbCrLf & "  LEFT OUTER Join  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & ".dbo.TCNMUnit AS U WITH(NOLOCK)   ON A.FNHSysUnitId = U.FNHSysUnitId "


            cmdstring &= vbCrLf & " Where (A.FTStateActive = '1') "
            cmdstring &= vbCrLf & "  ORDER BY A.FTRawMatCode,ISNULL(C.FTRawMatColorCode,''),ISNULL(S.FTRawMatSizeCode,'') "


            Dim dt As DataTable = HI.Conn.SQLConn.GetDataTable(cmdstring, Conn.DB.DataBaseName.DB_MASTER)


            Spls.Close()


            With mSelectItemMaster
                .ogcrcv.DataSource = Nothing
                .ogvrcv.ClearColumnsFilter()
                .FTStaReceiveAll.Checked = False
                .ProcessProc = False
                .ogcrcv.DataSource = dt.Copy
                .ShowDialog()

                If .ProcessProc Then
                    dt = CType(.ogcrcv.DataSource, DataTable).Copy()




                End If

            End With


            If dt.Select("FTSelect='1'").Length > 0 Then
                Dim Spls2 As New HI.TL.SplashScreen("Processing Material...   Please Wait   ")
                Dim I As Integer = 0
                cmdstring = "INSERT INTO  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TEMPBOMADDMAT (FTUser,FNHSysRawMatId,FTRawMatCode,FNHSysRawMatColorId,FNHSysRawMatSizeId,FNHSysUnitId,FNHSysSuplId,FTSuplCode) "
                For Each R As DataRow In dt.Select("FTSelect='1'")


                    If I = 0 Then
                        cmdstring &= vbCrLf & "  VALUES ('" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Val(R!FNHSysRawMatId.ToString) & ",'" & HI.UL.ULF.rpQuoted(R!FTRawMatCode.ToString) & "'," & Val(R!FNHSysRawMatColorId.ToString) & "," & Val(R!FNHSysRawMatSizeId.ToString) & "," & Val(R!FNHSysUnitId.ToString) & "," & Val(R!FNHSysSuplId.ToString) & ",'" & HI.UL.ULF.rpQuoted(R!FTSuplCode.ToString) & "') "
                    Else
                        cmdstring &= vbCrLf & "  , ('" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Val(R!FNHSysRawMatId.ToString) & ",'" & HI.UL.ULF.rpQuoted(R!FTRawMatCode.ToString) & "'," & Val(R!FNHSysRawMatColorId.ToString) & "," & Val(R!FNHSysRawMatSizeId.ToString) & "," & Val(R!FNHSysUnitId.ToString) & "," & Val(R!FNHSysSuplId.ToString) & ",'" & HI.UL.ULF.rpQuoted(R!FTSuplCode.ToString) & "') "
                    End If


                    I = I + 1
                Next

                HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)

                dt.Dispose()
                Spls2.Close()
                Call LadBOMDataItem(1)
            Else
                dt.Dispose()
            End If





        Catch ex As Exception

        End Try
    End Sub

    Private Sub LadBOMDataItem(Optional State As Integer = 0)

        Try
            Dim Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

            Dim cmdstring As String
            cmdstring = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.USP_GETMANAGEBOM_ITEM  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & State & ""

            Dim ds As New DataSet
            HI.Conn.SQLConn.GetDataSet(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE, ds)

            Spls.Close()
            Dim StateOK As Boolean = False

            With mSelectItem
                .ogcrcv.DataSource = Nothing
                .ogvrcv.ClearColumnsFilter()
                .FTStaReceiveAll.Checked = False
                .ProcessProc = False
                .SetNewColumn(ds.Tables(1).Copy)
                .ogcrcv.DataSource = ds.Tables(0).Copy
                .WindowState = System.Windows.Forms.FormWindowState.Maximized
                .ShowDialog()

                If .ProcessProc Then

                    Dim strcol As String = .ItemColumn
                    Dim dt As New DataTable
                    dt = CType(.ogcrcv.DataSource, DataTable).Copy()
                    StateOK = True

                    Dim Spls2 As New HI.TL.SplashScreen("Saving...Data  Please Wait   ")

                    For Each Rx As String In strcol.Split(",")
                        Dim SmpOrderNo As String = Rx.Replace("_", "-")

                        Dim JobQty As Decimal = 0

                        cmdstring = "select sum(FNQuantity) AS FNQuantity from " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_Breakdown AS P WITH(NOLOCK) WHERE P.FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(SmpOrderNo) & "'"

                        JobQty = Val(HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE, "0"))

                        Dim FieldName As String = Rx
                        Dim FieldNameCTH As String = Rx & "_EN"
                        Dim FieldNameCEN As String = Rx & "_QTY"
                        Dim FieldNameCh As String = Rx & "_CH"


                        Dim _FNAllPart As String = ""

                        Dim StateFoundUpdateJob As Boolean = False

                        For Each R As DataRow In dt.Select("FTSelect='1'")

                            If R.Item(FieldNameCh).ToString = "1" Then

                                StateFoundUpdateJob = True
                                Try
                                    cmdstring = " INSERT INTO " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_MatList ("
                                    cmdstring &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTSMPOrderNo,FNMatSeq, FTMat, FTMatName, FTMatColor, FTMatColorName,FTMatSize,FNMatQuantity,FNHSysUnitId,FTRemark,FNHSysSuplId,FNHSysRawmatId,FTColorway,FTMatPart,FNConSump "
                                    cmdstring &= vbCrLf & " )"
                                    cmdstring &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                                    cmdstring &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & ""
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(SmpOrderNo) & "'"
                                    cmdstring &= vbCrLf & ",ISNULL((SELECT MAX(MX.FNMatSeq) FNMatSeq FROM " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder_MatList MX WITH(NOLOCK) WHERE MX.FTSMPOrderNo ='" & HI.UL.ULF.rpQuoted(SmpOrderNo) & "' ),0) +1 "
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRawMatCode.ToString) & "'"
                                    cmdstring &= vbCrLf & ",''"
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R.Item(FieldName).ToString.Trim) & "'"
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R.Item(FieldNameCTH).ToString.Trim) & "'"
                                    cmdstring &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTRawMatSizeCode.ToString) & "'"
                                    cmdstring &= vbCrLf & "," & Val(R.Item(FieldNameCEN).ToString) & ""
                                    cmdstring &= vbCrLf & "," & Val(R!FNHSysUnitId.ToString) & ""
                                    cmdstring &= vbCrLf & ",''"
                                    cmdstring &= vbCrLf & "," & Val(R!FNHSysSuplId.ToString) & ""
                                    cmdstring &= vbCrLf & "," & Val(R!FNHSysRawmatId.ToString) & ""
                                    cmdstring &= vbCrLf & ",''"
                                    cmdstring &= vbCrLf & ",''"

                                    If JobQty > 0 Then
                                        cmdstring &= vbCrLf & "," & Decimal.Parse(Format(Val(R.Item(FieldNameCEN).ToString) / JobQty, "0.0000")) & ""
                                    Else
                                        cmdstring &= vbCrLf & "," & Val(R.Item(FieldNameCEN).ToString) & ""
                                    End If


                                    HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)

                                Catch ex As Exception

                                End Try


                            End If
                        Next


                        If StateFoundUpdateJob Then

                            cmdstring = " Update  " & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder  "
                            cmdstring &= vbCrLf & " SET FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            cmdstring &= vbCrLf & ",FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                            cmdstring &= vbCrLf & ",FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                            cmdstring &= vbCrLf & "  WHERE FTSMPOrderNo  ='" & HI.UL.ULF.rpQuoted(SmpOrderNo) & "'"

                            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)

                        End If

                    Next

                    Spls2.Close()

                    Call LoadData()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                End If

            End With
        Catch ex As Exception

        End Try
    End Sub


    Private Sub ocmconfirm_Click(sender As Object, e As EventArgs) Handles ocmconfirm.Click

        Dim dtpo As DataTable
        Try
            With CType(Me.ogdtime.DataSource, DataTable)
                .AcceptChanges()

                dtpo = .Copy()

            End With

            If dtpo.Select("FTSelect='1' ").Length <= 0 Then

                Exit Sub
            End If

            Call GenerateOrderList()


            Dim Spls As New HI.TL.SplashScreen("Saving.. Confirm")
            Dim cmdstring As String = ""


            cmdstring = "UPDATE A SET A.FTStateApp='1',A.FTStateAppBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',A.FTStateAppDate=" & HI.UL.ULDate.FormatDateDB & ",A.FTStateAppTime=" & HI.UL.ULDate.FormatTimeDB & ",A.FNSMPOrderStatus=1 "
            cmdstring &= vbCrLf & " FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TSMPOrder AS A "
            cmdstring &= vbCrLf & " INNER JOIN  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & ".dbo.TTMPOrderNo AS B ON A.FTSMPOrderNo  = B.FTSMPOrderNo "
            cmdstring &= vbCrLf & "  WHERE B.FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' AND  ISNULL(A.FTStateApp,'') <>'1' "


            HI.Conn.SQLConn.ExecuteNonQuery(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)

            Spls.Close()
            Call LoadData()
            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)





        Catch ex As Exception

        End Try


    End Sub

    Private Sub ocmapprove_Click(sender As Object, e As EventArgs)
        Dim dtpo As DataTable
        Try
            With CType(Me.ogdtime.DataSource, DataTable)
                .AcceptChanges()

                dtpo = .Copy()

            End With

            If dtpo.Select("FTSelect='1' ").Length <= 0 Then

                Exit Sub
            End If

            Call GenerateOrderList()

            Dim Spls As New HI.TL.SplashScreen("Calculating.. MRP")
            Dim cmdstring As String = ""
            For Each R As DataRow In dtpo.Select("FTSelect='1' AND FTStateApp='1' ")
                cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.USP_SMPCALBOM_ITEM '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "',0"
                HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)

            Next


            Spls.Close()
            Call LoadData()
            HI.MG.ShowMsg.mInfo("ทำการคำนวณ MRP เรียบร้อยแล้ว... ", 2010224519, Me.Text,, System.Windows.Forms.MessageBoxIcon.Information)


        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmcalc_Click(sender As Object, e As EventArgs) Handles ocmcalc.Click
        Dim dtpo As DataTable
        Try
            With CType(Me.ogdtime.DataSource, DataTable)
                .AcceptChanges()

                dtpo = .Copy()

            End With

            If dtpo.Select("FTSelect='1' AND FTStateApp='1' ").Length <= 0 Then

                Exit Sub
            End If

            Call GenerateOrderList()



            Dim Spls As New HI.TL.SplashScreen("Calculating.. MRP")
            Dim cmdstring As String = ""
            For Each R As DataRow In dtpo.Select("FTSelect='1' AND FTStateApp='1' ")


                cmdstring = "select top 1 FTSMPOrderNo  from [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder_MatList AS X WITH(NOLOCK) where x.FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "'"

                If HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE, "") <> "" Then
                    cmdstring = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.USP_SMPCALBOM_ITEM '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "',0"
                    HI.Conn.SQLConn.ExecuteOnly(cmdstring, Conn.DB.DataBaseName.DB_SAMPLE)
                End If

            Next


            Spls.Close()
            Call LoadData()
            HI.MG.ShowMsg.mInfo("ทำการคำนวณ MRP เรียบร้อยแล้ว... ", 2010224519, Me.Text,, System.Windows.Forms.MessageBoxIcon.Information)



        Catch ex As Exception

        End Try
    End Sub

    Private Sub ogvtime_DoubleClick(sender As Object, e As EventArgs) Handles ogvtime.DoubleClick
        Try
            With ogvtime
                If .FocusedRowHandle < 0 Then Exit Sub
                Dim _Form As Object = .GridControl.FindForm

                Dim pt As Point = .GridControl.PointToClient(Control.MousePosition)
                Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = .CalcHitInfo(pt)

                If Not (info.InRow Or info.InRowCell) Then
                    Exit Sub
                End If

                Dim PoNo As String = ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTSMPOrderNo").ToString()

                Dim myList As New ArrayList

                myList.Add(PoNo)

                Call CallByName(_Form.Parent.Parent, "CallWindowForm", CallType.Method, {"MnuSmpCreateOrderSample2", "LoadOrderInfo", myList.ToArray(GetType(String))})

            End With



        Catch ex As Exception
        End Try
    End Sub

End Class