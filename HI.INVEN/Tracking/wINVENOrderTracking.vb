Imports DevExpress.Data
Imports System.IO

Public Class wINVENOrderTracking

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
        Dim sFieldSum As String = "FNUsedQuantity|FNUsedPlusQuantity|FNRSVQuantity|FNPOQuantity|FNRCVQuantity|FNRTSQuantity|FNPOBalQuantity" & _
            "|FNRCVStockQuantity|FNTROInQuantity|FNTROOutQuantity|FNISSQuantity|FNRETQuantity|FNADJInQuantity|FNADJOutQuantity|FNTRWInQuantity|FNTRWOutQuantity|FNSaleQuantity|FNTerminateQuantity|FNOnhandQuantity|FNTRCQuantity|FNRTSAfQuantity|FNCNSInQuantity|FNCNSOutQuantity|FNRSVOutQuantity"

        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = "FNUsedQuantity|FNUsedPlusQuantity|FNRSVQuantity|FNPOQuantity|FNRCVQuantity|FNRTSQuantity|FNPOBalQuantity" & _
             "|FNRCVStockQuantity|FNTROInQuantity|FNTROOutQuantity|FNISSQuantity|FNRETQuantity|FNADJInQuantity|FNADJOutQuantity|FNTRWInQuantity|FNTRWOutQuantity|FNSaleQuantity|FNTerminateQuantity|FNOnhandQuantity|FNTRCQuantity|FNRTSAfQuantity|FNCNSInQuantity|FNCNSOutQuantity|FNRSVOutQuantity"



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

        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTempOrderTracking WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        _Qry = " SELECT  A.FTOrderNo, A.FNHSysStyleId, A.FNHSysBuyerId, ISNULL(B.FDShipDate,'') AS FDShipDate"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS A WITH(NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.V_TMERTOrder_Cut_ShipDate AS B ON A.FTOrderNo = B.MFTOrderNo"
        _Qry &= vbCrLf & " WHERE A.FTOrderNo <>''  "

        If FNHSysBuyId.Text <> "" Then
            _Qry &= vbCrLf & " AND A.FNHSysBuyId =" & Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString)) & "  "
        End If

        If FNHSysStyleId.Text <> "" Then
            _Qry &= vbCrLf & " AND A.FNHSysStyleId =" & Integer.Parse(Val(FNHSysStyleId.Properties.Tag.ToString)) & "  "
        End If

        If FTOrderNo.Text <> "" Then
            _Qry &= vbCrLf & " AND A.FTOrderNo >='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'  "
        End If

        If FTOrderNoTo.Text <> "" Then
            _Qry &= vbCrLf & " AND A.FTOrderNo <='" & HI.UL.ULF.rpQuoted(FTOrderNoTo.Text) & "'  "
        End If

        If FTStartShipment.Text <> "" Then
            _Qry &= vbCrLf & " AND ISNULL(B.FDShipDate,'') >='" & HI.UL.ULDate.ConvertEnDB(FTStartShipment.Text) & "'  "
        End If

        If FTStartShipment.Text <> "" Then
            _Qry &= vbCrLf & " AND ISNULL(B.FDShipDate,'') >='" & HI.UL.ULDate.ConvertEnDB(FTEndShipment.Text) & "'  "
        End If

        _Qry &= vbCrLf & "  ORDER BY A.FTOrderNo "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        _TotalRow = _dt.Rows.Count
        _Rx = 0

        For Each R As DataRow In _dt.Rows
            _Rx = _Rx + 1

            _Spls.UpdateInformation("Generating Data OrderNo" & R!FTOrderNo.ToString & "  Record  " & _Rx.ToString & " Of " & _TotalRow.ToString & "  (" & Format((_Rx * 100.0) / _TotalRow, "0.00") & " % ) ")
            _Qry = " Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_Order_Tracking '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "

            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        Next

        _Qry = " SELECT   A.FTUserLogIn, A.FTOrderNo, A.FNHSysRawMatId, B.FTRawMatCode, C.FTRawMatColorCode, "
        _Qry &= vbCrLf & " S.FTRawMatSizeCode, A.FNUsedQuantity, A.FNUsedPlusQuantity, U.FTUnitCode,"
        _Qry &= vbCrLf & " A.FNRSVQuantity, A.FNPOQuantity, A.FNRCVQuantity, A.FNRTSQuantity, A.FNPOBalQuantity, "
        _Qry &= vbCrLf & " A.FNRCVStockQuantity, A.FNTROInQuantity, A.FNTROOutQuantity, A.FNISSQuantity, "
        _Qry &= vbCrLf & " A.FNRETQuantity, A.FNADJInQuantity, A.FNADJOutQuantity, A.FNTRWInQuantity, A.FNTRWOutQuantity, A.FNSaleQuantity, "
        _Qry &= vbCrLf & " A.FNTerminateQuantity, A.FNOnhandQuantity, A.FNTRCQuantity, A.FNRTSAfQuantity"

        _Qry &= vbCrLf & " , A.FNCNSInQuantity, A.FNCNSOutQuantity, A.FNRSVOutQuantity"

        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ", B.FTRawMatNameTH AS FTRawMatName"
            _Qry &= vbCrLf & ", B.FTRawMatColorNameTH AS FTRawMatColorName"
        Else
            _Qry &= vbCrLf & ", B.FTRawMatNameEN AS FTRawMatName"
            _Qry &= vbCrLf & ", B.FTRawMatColorNameEN AS FTRawMatColorName"
        End If

        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTempOrderTracking AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMaterial AS B WITH(NOLOCK)  ON A.FNHSysRawMatId = B.FNHSysRawMatId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnit AS U WITH(NOLOCK) ON A.FNHSysUnitId = U.FNHSysUnitId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatColor AS C WITH(NOLOCK)  ON B.FNHSysRawMatColorId = C.FNHSysRawMatColorId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TINVENMMatSize AS S  WITH(NOLOCK) ON B.FNHSysRawMatSizeId = S.FNHSysRawMatSizeId"
        _Qry &= vbCrLf & " WHERE  A.FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        '_Qry &= vbCrLf & " ORDER BY  A.FTOrderNo,B.FTRawMatCode, C.FTRawMatColorCode,S.FTRawMatSizeCode  "
        _Qry &= vbCrLf & " ORDER BY  A.FTOrderNo,B.FTRawMatCode, C.FTRawMatColorCode,S.FNRawMatSizeSeq  "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        _Qry = " DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENTempOrderTracking WHERE FTUserLogIn='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
        HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)

        Me.ogdtime.DataSource = _dt
        _Spls.Close()

        _RowDataChange = False

    End Sub

    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNHSysBuyId.Text <> "" And FNHSysBuyId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FNHSysStyleId.Text <> "" And FNHSysStyleId.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTOrderNo.Text <> "" And FTOrderNo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTOrderNoTo.Text <> "" And FTOrderNoTo.Properties.Tag.ToString <> "" Then
            _Pass = True
        End If

        If Me.FTStartShipment.Text <> "" Then
            _Pass = True
        End If

        If Me.FTEndShipment.Text <> "" Then
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

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click

        If VerifyData() Then
            Dim _FM As String = ""

            Dim _Qry As String = ""
            _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.SP_Order_Tracking_Report '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & Integer.Parse(Val(FNHSysBuyId.Properties.Tag.ToString())) & "," & Integer.Parse(Val(Val(FNHSysStyleId.Properties.Tag.ToString()))) & ",'" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "','" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTStartShipment.Text) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTEndShipment.Text) & "'"
            HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)

            Dim strFoumalar As String = ""

            strFoumalar = "{TMERTOrder_Resource.FTUserLogIn} = '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"

            If Val(FNHSysBuyId.Properties.Tag.ToString()) > 0 Then
                If strFoumalar <> "" Then strFoumalar += " AND "
                strFoumalar += "{TMERTOrder.FNHSysBuyId} = " & FNHSysBuyId.Properties.Tag.ToString() & ""
            End If

            If Val(FNHSysStyleId.Properties.Tag.ToString()) > 0 Then
                If strFoumalar <> "" Then strFoumalar += " AND "
                strFoumalar += "{TMERTOrder.FNHSysStyleId} = " & Val(FNHSysStyleId.Properties.Tag.ToString()) & ""
            End If

            If Me.FTOrderNo.Text <> "" Then
                If strFoumalar <> "" Then strFoumalar += " AND "
                strFoumalar += " {TMERTOrder.FTOrderNo} >= '" & HI.UL.ULF.rpQuoted(Me.FTOrderNo.Text) & "'"
            End If

            If Me.FTOrderNoTo.Text <> "" Then
                If strFoumalar <> "" Then strFoumalar += " AND "
                strFoumalar += " {TMERTOrder.FTOrderNo} <= '" & HI.UL.ULF.rpQuoted(Me.FTOrderNoTo.Text) & "'"
            End If

            If Me.FTStartShipment.Text <> "" Then
                If strFoumalar <> "" Then strFoumalar += " AND "
                strFoumalar += " {V_TMERTOrder_Cut_ShipDate.FDShipDate} >= '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartShipment.Text) & "'"
            End If

            If Me.FTEndShipment.Text <> "" Then
                If strFoumalar <> "" Then strFoumalar += " AND "
                strFoumalar += " {V_TMERTOrder_Cut_ShipDate.FDShipDate} <= '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndShipment.Text) & "'"
            End If

            For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In Me.ogvtime.Columns
                Dim K As String = ""
                Dim KValue As String = ""
                Try
                    K = GridCol.FilterInfo.FilterString
                Catch ex As Exception
                End Try

                Try
                    KValue = GridCol.FilterInfo.Value.ToString
                Catch ex As Exception
                End Try

                Select Case True 'GridCol.FieldName.ToString

                    '-----------Start Order ---------------------
                    Case GridCol.FieldName.ToString = "FTOrderNo" And (K <> "") And (KValue = "")

                        If strFoumalar <> "" Then
                            strFoumalar += " AND  ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TMERTOrder."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                        Else
                            strFoumalar = " ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TMERTOrder."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                        End If

                    Case GridCol.FieldName.ToString = "FTOrderNo" And (KValue <> "")

                        If strFoumalar <> "" Then
                            strFoumalar += " AND  ({TMERTOrder.FTOrderNo} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                        Else
                            strFoumalar = "  ({TMERTOrder.FTOrderNo} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                        End If
                        '-----------End Order ---------------------


                        '-----------Start Style ---------------------
                    Case GridCol.FieldName.ToString = "FTStyleCode" And (K <> "") And (KValue = "")

                        If strFoumalar <> "" Then
                            strFoumalar += " AND  ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TMERTStyle."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                        Else
                            strFoumalar = " ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TMERTStyle."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                        End If

                    Case GridCol.FieldName.ToString = "FTStyleCode" And (KValue <> "")

                        If strFoumalar <> "" Then
                            strFoumalar += " AND  ({TMERTStyle.FTStyleCode} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                        Else
                            strFoumalar = "  ({TMERTStyle.FTStyleCode} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                        End If
                        '-----------End Style ---------------------

                        '-----------Start Material Code ---------------------
                    Case GridCol.FieldName.ToString = "FTRawMatCode" And (K <> "") And (KValue = "")

                        If strFoumalar <> "" Then
                            strFoumalar += " AND  ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TINVENMMaterial."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                        Else
                            strFoumalar = " ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TINVENMMaterial."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                        End If

                    Case GridCol.FieldName.ToString = "FTRawMatCode" And (KValue <> "")

                        If strFoumalar <> "" Then
                            strFoumalar += " AND  ({TINVENMMaterial.FTRawMatCode} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                        Else
                            strFoumalar = "  ({TINVENMMaterial.FTRawMatCode} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                        End If
                        '-----------End Material Code ---------------------

                        '-----------Start Color Code ---------------------
                    Case GridCol.FieldName.ToString = "FTRawMatColorCode" And (K <> "") And (KValue = "")
                        If strFoumalar <> "" Then
                            strFoumalar += " AND  ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TINVENMMatColor."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                        Else
                            strFoumalar = " ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TINVENMMatColor."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                        End If

                    Case GridCol.FieldName.ToString = "FTRawMatColorCode" And (KValue <> "")
                        If strFoumalar <> "" Then
                            strFoumalar += " AND  ({TINVENMMatColor.FTRawMatColorCode} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                        Else
                            strFoumalar = "  ({TINVENMMatColor.FTRawMatColorCode} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                        End If

                        '-----------End Color Code ---------------------

                        '-----------Start Color Code ---------------------
                    Case GridCol.FieldName.ToString = "FTRawMatSizeCode" And (K <> "") And (KValue = "")
                        If strFoumalar <> "" Then
                            strFoumalar += " AND  ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TINVENMMatSize."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                        Else
                            strFoumalar = " ( " & Replace(Replace(Replace(Replace(Replace(K, "[", "{TINVENMMatSize."), "]", "}"), "%", "*"), ")", "]"), "(", "[") & " ) "
                        End If

                    Case GridCol.FieldName.ToString = "FTRawMatSizeCode" And (KValue <> "")
                        If strFoumalar <> "" Then
                            strFoumalar += " AND  ({TINVENMMatSize.FTRawMatSizeCode} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                        Else
                            strFoumalar = "  ({TINVENMMatSize.FTRawMatSizeCode} LIKE '*" & HI.UL.ULF.rpQuoted(KValue) & "*')"
                        End If

                        '-----------End Color Code ---------------------

                    Case Else
                End Select
            Next

            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Inventrory\"
                .ReportName = "OrderTracking.rpt"
                .Formular = strFoumalar
                .Preview()
            End With

        End If

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
End Class