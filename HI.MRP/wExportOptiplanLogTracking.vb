Imports DevExpress.Data
Imports System.IO

Public Class wExportOptiplanLogTracking

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
        ''------Start Add Summary Grid-------------
        'Dim sFieldCount As String = ""
        'Dim sFieldSum As String = "FNUsedQuantity|FNUsedPlusQuantity|FNRSVQuantity|FNPOQuantity|FNRCVQuantity|FNRTSQuantity|FNPOBalQuantity" & _
        '    "|FNRCVStockQuantity|FNTROInQuantity|FNTROOutQuantity|FNISSQuantity|FNRETQuantity|FNADJInQuantity|FNADJOutQuantity|FNTRWInQuantity|FNTRWOutQuantity|FNSaleQuantity|FNTerminateQuantity|FNOnhandQuantity"

        'Dim sFieldGrpCount As String = ""
        'Dim sFieldGrpSum As String = "FNUsedQuantity|FNUsedPlusQuantity|FNRSVQuantity|FNPOQuantity|FNRCVQuantity|FNRTSQuantity|FNPOBalQuantity" & _
        '     "|FNRCVStockQuantity|FNTROInQuantity|FNTROOutQuantity|FNISSQuantity|FNRETQuantity|FNADJInQuantity|FNADJOutQuantity|FNTRWInQuantity|FNTRWOutQuantity|FNSaleQuantity|FNTerminateQuantity|FNOnhandQuantity"

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

        _Qry = " SELECT FTExportOpUser, FDExportOpDate, FTExportOpTime, FTExportOpBuy, FTExportOpStyle, FTExportOpOrder, FTExportOpPath
"
        _Qry &= vbCrLf & "FROM"
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_LOG) & "]..HSysAuditLogExportOptiplan AS B WITH (NOLOCK) "


        _Qry &= vbCrLf & "  WHERE B.FTExportOpBuy ='" & HI.UL.ULF.rpQuoted(FNHSysBuyId.Text) & "' "

        If FTStyleKeyword.Text.Trim() <> "" Then
            _Qry &= vbCrLf & "  AND B.FTExportOpStyle LIKE N'%" & HI.UL.ULF.rpQuoted(FTStyleKeyword.Text.Trim()) & "%' "
        End If


        If FTOrderKeyword.Text.Trim() <> "" Then
            _Qry &= vbCrLf & "  AND B.FTExportOpOrder LIKE N'%" & HI.UL.ULF.rpQuoted(FTStyleKeyword.Text.Trim()) & "%' "
        End If

        _Qry &= vbCrLf & " Order By FDExportOpDate + FTExportOpTime DESC , FTExportOpUser ASC "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

        Me.ogdtime.DataSource = _dt
        _Spls.Close()

        _RowDataChange = False

    End Sub


    Private Function VerifyData() As Boolean
        Dim _Pass As Boolean = False

        If FNHSysBuyId.Text.Trim() <> "" Then
            _Pass = True
        End If

        If Not (_Pass) Then
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysBuyId_lbl.Text)
        End If

        Return _Pass
    End Function
#End Region


    'Private Sub Gridview_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvtime.CustomSummaryCalculate
    '    If e.SummaryProcess = CustomSummaryProcess.Start Then
    '        InitStartValue()
    '    End If

    '    Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
    '        Case "FNTime", "FNOT1", "FNOT1_5", "FNOT2", "FNOT3", "FNOT4"
    '            If e.SummaryProcess = CustomSummaryProcess.Calculate Then

    '                If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
    '                    If e.IsGroupSummary Then
    '                        Dim Seq As Integer = 1
    '                        For Each Str As String In e.FieldValue.ToString.Split(":")
    '                            Select Case Seq
    '                                Case 1
    '                                    GrpSum = GrpSum + (Integer.Parse(Val(Str)) * 60)
    '                                Case Else
    '                                    GrpSum = GrpSum + Integer.Parse(Val(Str))
    '                            End Select
    '                            Seq = Seq + 1
    '                        Next
    '                    End If

    '                    If e.IsTotalSummary Then
    '                        Dim Seq As Integer = 1
    '                        For Each Str As String In e.FieldValue.ToString.Split(":")
    '                            Select Case Seq
    '                                Case 1
    '                                    totalSum = totalSum + (Integer.Parse(Val(Str)) * 60)
    '                                Case Else
    '                                    totalSum = totalSum + Integer.Parse(Val(Str))
    '                            End Select

    '                            Seq = Seq + 1
    '                        Next
    '                    End If

    '                End If

    '                If e.IsGroupSummary Then
    '                    Dim GrpDisplay As String = ""
    '                    GrpDisplay = Format(((GrpSum) \ 60), "00") & " : " & Format(((GrpSum) Mod 60), "00")
    '                    e.TotalValue = GrpSum
    '                End If

    '                If e.IsTotalSummary Then
    '                    Dim NetDisplay As String = ""

    '                    If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
    '                        NetDisplay = Format(((totalSum) \ 60), "00") & " : " & Format(((totalSum) Mod 60), "00")
    '                    Else
    '                        NetDisplay = Format(((totalSum) \ 60), "00") & " : " & Format(((totalSum) Mod 60), "00")
    '                    End If

    '                    e.TotalValue = NetDisplay ' totalSum 'NetDisplay

    '                End If
    '            End If
    '    End Select
    'End Sub


#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
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

    Private Sub ogbheader_Click(sender As Object, e As EventArgs) Handles ogbheader.Click

    End Sub
End Class