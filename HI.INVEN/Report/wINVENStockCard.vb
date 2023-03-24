Imports DevExpress.Data
Imports System.IO

Public Class wINVENStockCard

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
        Dim sFieldSum As String = "FNDocInQty|FTDocOutQty"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FNDocInQty|FTDocOutQty"

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogvtime
            .OptionsView.ShowAutoFilterRow = False
            .OptionsSelection.MultiSelect = False
            .OptionsMenu.EnableColumnMenu = False
            .OptionsMenu.ShowAutoFilterRowItem = False
            .OptionsFilter.AllowFilterEditor = False
            .OptionsFilter.AllowColumnMRUFilterList = False
            .OptionsFilter.AllowMRUFilterList = False
            .OptionsSelection.MultiSelect = False
            .OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
        End With

        With ogvtime
            .ClearGrouping()
            .ClearDocument()
            .Columns("FTWHCode").Group()
            .Columns("FTRawMatCode").Group()
            .Columns("FTRawMatName").Group()
            .Columns("FTRawMatColorCode").Group()
            .Columns("FTRawMatSizeCode").Group()
           

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
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n" & HI.ST.Config.QtyDigit.ToString & "}"
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
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n" & HI.ST.Config.QtyDigit.ToString & "})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

            .ExpandAllGroups()
            .RefreshData()


        End With
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

    Private Sub ogvtime_CellMerge(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs) Handles ogvtime.CellMerge
        Try
            If e.Column.OptionsColumn.AllowMerge Then
                If (e.CellValue1 = e.CellValue2) Then
                    e.Merge = True
                    e.Handled = True
                Else
                    e.Merge = False
                    e.Handled = True
                End If
            Else
                e.Merge = False
                e.Handled = True
            End If

        Catch ex As Exception

        End Try
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
        Dim _Sdate As String = HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text)
        Dim _Edate As String = HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text)
        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")


        If _Edate = "" Then
            _Edate = "9999/99/99"
        End If

        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTempStockTransaction WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)


        _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_Stock_Onhand '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(Me.FNHSysWHId.Text) & "','" & HI.UL.ULF.rpQuoted(Me.FNHSysWHIdTo.Text) & "','" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "','" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "','" & HI.UL.ULF.rpQuoted(Me.FNHSysMerMatId.Text) & "','" & HI.UL.ULF.rpQuoted(Me.FNHSysMerMatIdTo.Text) & "','" & _Sdate & "','" & _Edate & "' "
        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        _Qry = " SELECT   WH.FTWHCode"
        _Qry &= vbCrLf & ",  A.FTOrderNo"
        _Qry &= vbCrLf & ", B.FTRawMatCode"
        _Qry &= vbCrLf & ", ISNULL(C.FTRawMatColorCode,'') AS FTRawMatColorCode "
        _Qry &= vbCrLf & ", ISNULL(S.FTRawMatSizeCode,'') AS FTRawMatSizeCode "
        _Qry &= vbCrLf & ", ISNULL(U.FTUnitCode,'') AS FTUnitCode"
        _Qry &= vbCrLf & ",  CASE WHEN ISDATE(A.FTDocumentDate) =1 THEN Convert(nvarchar(10),Convert(Datetime,A.FTDocumentDate) ,103)  ELSE '' END  AS FTDocDate"
        _Qry &= vbCrLf & ",  A.FTDocumentTime AS FTDocTime "
        _Qry &= vbCrLf & ",  A.FTDocType"
        _Qry &= vbCrLf & ", CASE WHEN A.FNQuantity > 0 THEN A.FTDocumentNo  ELSE ''  END AS FTDocIn "
        _Qry &= vbCrLf & ", CASE WHEN A.FNQuantity > 0 THEN FNQuantity END AS FNDocInQty "
        _Qry &= vbCrLf & ", CASE WHEN A.FNQuantity < 0 THEN A.FTDocumentNo ELSE ''  END AS FTDocOut "
        _Qry &= vbCrLf & ", CASE WHEN A.FNQuantity < 0 THEN (FNQuantity*-1) END AS FTDocOutQty "

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ", B.FTRawMatNameTH AS FTRawMatName"
        Else
            _Qry &= vbCrLf & ", B.FTRawMatNameEN AS FTRawMatName"
        End If

        _Qry &= vbCrLf & ",  A.FNBalQty"

        _Qry &= vbCrLf & " FROM ("

        _Qry &= vbCrLf & " SELECT FNHSysWHId, FTOrderNo, FTDocumentNo,FTDocType"
        _Qry &= vbCrLf & " , FTDocumentDate, FTDocumentTime, FNHSysRawMatId, FNQuantity,FNSeq"
        _Qry &= vbCrLf & " ,ISNULL(("
        _Qry &= vbCrLf & "  SELECT SUM(FNQuantity) AS FNQuantity"
        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTempStockTransaction AS B  WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE B.FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        _Qry &= vbCrLf & "  AND B.FNHSysWHId=A.FNHSysWHId "
        _Qry &= vbCrLf & "  AND B.FNHSysRawMatId=A.FNHSysRawMatId "
        _Qry &= vbCrLf & "  AND B.FNSeq <=A.FNSeq "
        _Qry &= vbCrLf & "  ),0) AS FNBalQty"
        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTempStockTransaction  AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "  WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "

        _Qry &= vbCrLf & " ) AS A "
        _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B WITH(NOLOCK)  ON A.FNHSysRawMatId = B.FNHSysRawMatId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON B.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH(NOLOCK)  ON B.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S  WITH(NOLOCK) ON B.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
        _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMWarehouse AS WH WITH(NOLOCK)  ON A.FNHSysWHId = WH.FNHSysWHId "
        '_Qry &= vbCrLf & " ORDER BY   WH.FTWHCode,B.FTRawMatCode, C.FTRawMatColorCode,S.FTRawMatSizeCode ,A.FTDocumentDate, A.FTDocumentTime,A.FNSeq "

        _Qry &= vbCrLf & " WHERE  B.FTRawMatCode <> '' "

        If FNHSysRawMatColorId.Text <> "" Then
            _Qry &= vbCrLf & "  AND    ISNULL(C.FTRawMatColorCode,'') ='" & HI.UL.ULF.rpQuoted(FNHSysRawMatColorId.Text) & "' "
        End If

        If FNHSysRawMatSizeId.Text <> "" Then
            _Qry &= vbCrLf & "  AND   ISNULL(S.FTRawMatSizeCode,'')  ='" & HI.UL.ULF.rpQuoted(FNHSysRawMatSizeId.Text) & "' "
        End If


        _Qry &= vbCrLf & " ORDER BY   WH.FTWHCode,B.FTRawMatCode, C.FTRawMatColorCode,S.FNRawMatSizeSeq ,A.FTDocumentDate,A.FNSeq, A.FTDocumentTime "



        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTempStockTransaction WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        Me.ogdtime.DataSource = _dt

        Try
            Me.ogvtime.ExpandAllGroups()
        Catch ex As Exception
        End Try

        _Spls.Close()

        _RowDataChange = False

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNHSysMerMatId.Text <> "" And FNHSysMerMatId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FNHSysMerMatIdTo.Text <> "" And FNHSysMerMatIdTo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTOrderNo.Text <> "" And FTOrderNo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTOrderNoTo.Text <> "" And FTOrderNoTo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If


        If Me.FNHSysWHId.Text <> "" And FNHSysWHId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FNHSysWHIdTo.Text <> "" And FNHSysWHIdTo.Properties.Tag.ToString <> "" Then
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

            Me.ogvtime.OptionsView.ShowAutoFilterRow = False
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

    Private Sub ogvtime_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvtime.RowCellStyle
        Try

            With Me.ogvtime
                Try

                    Select Case ("" & .GetRowCellValue(e.RowHandle, "FTDocIn").ToString)
                        Case ""

                            Select Case e.Column.FieldName.ToString
                                Case "FTDocOut", "FTDocOutQty"
                                    e.Appearance.ForeColor = Drawing.Color.OrangeRed
                                Case Else

                            End Select

                        Case Else
                            Select Case e.Column.FieldName.ToString
                                Case "FTDocIn", "FNDocInQty"
                                    e.Appearance.ForeColor = Drawing.Color.Green
                                Case Else

                            End Select

                    End Select

                Catch ex As Exception
                End Try
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
End Class