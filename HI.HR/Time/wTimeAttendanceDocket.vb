Imports DevExpress.Data
Imports System.IO

Public Class wTimeAttendanceDocket

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _ActualDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

        Me.FNHSysCmpId.Text = "1"
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Me.FNHSysCmpId.Properties.ReadOnly = True
        Me.FNHSysCmpId.Properties.Buttons(0).Visible = False
        Me.FNHSysCmpId.Properties.Buttons(0).Enabled = False



    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()

        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = "FTEmpCode"
        Dim sFieldSum As String = "FNLateNormalMin|FNLateOtMin"
        Dim sFieldGrpCount As String = "FTEmpCode"
        'Dim sFieldGrpSum As String = "FNOT1Min|FNOT1_5Min|FNOT2Min|FNOT3Min|FNOT4Min|L0TT|L1TT|L2TT|L4TT|L5TT|L6TT|L7TT|L8TT|L9TT|L16TT|L17Pay|L17NotPay|L97Pay|L97NotPay|L98Pay|L99TT|FNAbsent"
        Dim sFieldGrpSum As String = "FNOT1Min"
        'FNLateNormalMin|FNLateOtMin|FNOT1Min|FNOT1_5Min|FNOT2Min|FNOT3Min|FNOT4Min|L0TT|L1TT|L2TT|L4TT|L5TT|L6TT|L7TT|L8TT|L9TT|L16TT|L17Pay|L17NotPay|L97Pay|L97NotPay|L98Pay|L99TT|FNAbsent
        'T.FNLateNormalMin, T.FNLateNormalCut
        Dim sFieldCustomSum As String = "FNTimeMin|FNOT1Min|FNOT1_5Min|FNOT2Min|FNOT3Min|FNOT4Min|L0TT|L1TT|L2TT|L4TT|L5TT|L6TT|L7TT|L8TT|L9TT|L16TT|L17TT|L17Pay|L17NotPay|L97Pay|L97NotPay|L98Pay|L99TT|L3Pay|L18Pay|L3NotPay|L18NotPay|FNAbsent|L3TT|L18TT|L19TT"
        Dim sFieldCustomGrpSum As String = "FNTimeMin|FNOT1_5Min|L0TT|L1TT|L2TT|L6TT|L16TT|L3TT|L17TT|L17Pay|L17NotPay|L97Pay|L97NotPay|L98Pay|L99TT|L3Pay|L18Pay|L3NotPay|L18NotPay|FNAbsent"

        With ogvTimAttendanceDocket
            .ClearGrouping()
            .ClearDocument()
            .Columns("FTUnitSectCode").Group()

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
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n2}"
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
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
                End If
            Next

            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            .OptionsView.ShowFooter = True
            .OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded
            .OptionsView.ShowGroupPanel = True
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
    '            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal  Then
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


    Private Sub Gridview_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvTimAttendanceDocket.CustomSummaryCalculate
        If e.SummaryProcess = CustomSummaryProcess.Start Then
            InitStartValue()
        End If

        Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
            Case "FNOT1Min", "FNOT1_5Min", "FNOT2Min", "FNOT3Min", "FNOT4Min", "L0TT", "L1TT", "L2TT", "L4TT", "L5TT", "L6TT", "L7TT", "L8TT", "L9TT", "L16TT", "L17TT", "L17Pay", "L17NotPay", "L3Pay", "L3NotPay", "L18Pay", "L18NotPay", "L97Pay", "L97NotPay", "L98Pay", "L99TT", "FNAbsent", "L3TT", "L18TT", "L19TT"
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

                        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                            NetDisplay = Format(((totalSum) \ 60), "00") & " : " & Format(((totalSum) Mod 60), "00")
                        Else
                            NetDisplay = Format(((totalSum) \ 60), "00") & " : " & Format(((totalSum) Mod 60), "00")
                        End If

                        e.TotalValue = NetDisplay ' totalSum 'NetDisplay

                    End If
                End If

            Case "FNTimeMin"
                If e.SummaryProcess = CustomSummaryProcess.Calculate Then

                    If e.FieldValue IsNot Nothing AndAlso e.FieldValue IsNot DBNull.Value Then
                        If e.IsGroupSummary Then
                            Dim Seq As Integer = 1
                            For Each Str As String In e.FieldValue.ToString.Split(New [Char]() {CChar(vbTab), "("c, ":"c, ")"c})
                                Select Case Seq
                                    Case 1
                                        GrpSum = GrpSum + (Integer.Parse(Val(Str)) * 480)
                                    Case 2
                                        GrpSum = GrpSum + (Integer.Parse(Val(Str)) * 60)
                                    Case Else
                                        GrpSum = GrpSum + Integer.Parse(Val(Str))
                                End Select

                                Seq = Seq + 1
                            Next
                        End If

                        If e.IsTotalSummary Then
                            Dim Seq As Integer = 1
                            For Each Str As String In e.FieldValue.ToString.Split(New [Char]() {CChar(vbTab), "("c, ":"c, ")"c})
                                Select Case Seq
                                    Case 1
                                        totalSum = totalSum + (Integer.Parse(Val(Str)) * 480)
                                    Case 2
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
                        GrpDisplay = Format(((GrpSum) \ 480), "00") & " : " & Format((((GrpSum Mod 480)) \ 60), "00") & " : " & Format(((GrpSum) Mod 60), "00")
                        e.TotalValue = GrpDisplay
                    End If

                    If e.IsTotalSummary Then
                        Dim NetDisplay As String = ""

                        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                            NetDisplay = Format(((totalSum) \ 480), "00") & " : " & Format((((totalSum Mod 480)) \ 60), "00") & " : " & Format(((totalSum) Mod 60), "00")
                        Else
                            NetDisplay = Format(((totalSum) \ 480), "00") & " : " & Format((((totalSum Mod 480)) \ 60), "00") & " : " & Format(((totalSum) Mod 60), "00")
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

    Private _CfgIncentiveAmtDigit As String = ""
    Public Property CfgIncentiveAmtDigit As String
        Get
            Return _CfgIncentiveAmtDigit
        End Get
        Set(value As String)
            _CfgIncentiveAmtDigit = value
        End Set
    End Property


#End Region

#Region "Procedure"

    Private Sub LoadData()

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Dim _FTStartCalculateDate As String = HI.UL.ULDate.ConvertEnDB(FDStartDate.Text)
        Dim _FTEndCalculateDate As String = HI.UL.ULDate.ConvertEnDB(FDEndDate.Text)

        Dim _Lang As String = ""
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Lang = "TH"
        Else
            _Lang = "EN"
        End If

        Dim FTEmpTypeCode_fm As String = "'0'"
        Dim FTEmpTypeCode_to As String = "'ZZZZZZZZZZ'"
        Dim FTDivisonCode_fm As String = "'0'"
        Dim FTDivisonCode_to As String = "'ZZZZZZZZZZ'"
        Dim FTDeptCode_fm As String = "'0'"
        Dim FTDeptCode_to As String = "'ZZZZZZZZZZ'"
        Dim FTSectCode_fm As String = "'0'"
        Dim FTSectCode_to As String = "'ZZZZZZZZZZ'"
        Dim FTUnitSectCode_fm As String = "'0'"
        Dim FTUnitSectCode_to As String = "'ZZZZZZZZZZ'"
        Dim FTEmpCode_fm As String = "'0'"
        Dim FTEmpCode_to As String = "'ZZZZZZZZZZ'"
        Dim FNEmpStatus_fm As String = "0"
        Dim FNEmpStatus_to As String = "2"


        Dim _SqlWhere As String = "1 "
        If Me.FNHSysEmpTypeId.Text <> "" Then
            FTEmpTypeCode_fm = "'" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
            FTEmpTypeCode_to = "'" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
        End If


        ''------Criteria By Employeee Code
        If Me.FNHSysEmpId.Text <> "" Then
            FTEmpCode_fm = "'" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
        End If
        If Me.FNHSysEmpIdTo.Text <> "" Then
            FTEmpCode_to = "'" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
        End If


        ''------Criteria By Department
        If Me.FNHSysDeptId.Text <> "" Then
            FTDeptCode_fm = "'" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
        End If
        If Me.FNHSysDeptIdTo.Text <> "" Then
            FTDeptCode_to = "'" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
        End If


        ''------Criteria By Division
        If Me.FNHSysDivisonId.Text <> "" Then
            FTDivisonCode_fm = "'" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
        End If
        If Me.FNHSysDivisonIdTo.Text <> "" Then
            FTDivisonCode_to = "'" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
        End If


        ''------Criteria By Sect
        If Me.FNHSysSectId.Text <> "" Then
            FTSectCode_fm = "'" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
        End If
        If Me.FNHSysSectIdTo.Text <> "" Then
            FTSectCode_to = "'" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
        End If


        If Me.FNHSysUnitSectId.Text <> "" Then
            FTUnitSectCode_fm = "'" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
        End If
        If Me.FNHSysUnitSectIdTo.Text <> "" Then
            FTUnitSectCode_to = "'" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
        End If

        Select Case FNEmpStatusReport.SelectedIndex
            Case 0

                FNEmpStatus_fm = "0"
                FNEmpStatus_to = "2"
            Case Else
                FNEmpStatus_fm = Val(FNEmpStatusReport.SelectedIndex - 1)
                FNEmpStatus_to = Val(FNEmpStatusReport.SelectedIndex - 1)
        End Select


        Dim _FNHSysCmpId As Integer
        _FNHSysCmpId = HI.ST.SysInfo.CmpID

        Dim _colcount As Integer = 0

        _Qry = "Exec [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_GET_DOCKET_TIME_ATTENDANCE '" & _FTStartCalculateDate & "','" & _FTEndCalculateDate & "'," & _Lang & "," & _FNHSysCmpId _
            & "," & FTEmpTypeCode_fm & "," & FTEmpTypeCode_to & "," & FTDivisonCode_fm & "," & FTDivisonCode_to _
            & "," & FTDeptCode_fm & "," & FTDeptCode_to & "," & FTSectCode_fm & "," & FTSectCode_to _
            & "," & FTUnitSectCode_fm & "," & FTUnitSectCode_to & "," & FTEmpCode_fm & "," & FTEmpCode_to & "," & FNEmpStatus_fm & "," & FNEmpStatus_to
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        'Me.ogdtime.DataSource = _dt
        'Me.ogvtime.BestFitColumns()
        'ogvtime.ExpandAllGroups()
        _Spls.Close()

        Me.ogcTimeAttendanceDocket.DataSource = _dt

        'With Me.ogvIncentiveDocket

        '    For I As Integer = .Columns.Count - 1 To 0 Step -1
        '        Select Case .Columns(I).FieldName.ToString.ToUpper

        '            Case "FNHSysEmpID".ToUpper, "FTEmpCode".ToUpper, "FTEmpName".ToUpper, "FTEmpTypeCode".ToUpper, "FTDeptCode".ToUpper, "FTDivisonCode".ToUpper, "FTSectCode".ToUpper, "FTUnitSectCode".ToUpper, "FTPositCode".ToUpper
        '                .Columns(I).OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False
        '            Case Else
        '                .Columns.Remove(.Columns(I))
        '        End Select
        '    Next

        '    If Not (_dt Is Nothing) Then
        '        For Each Col As DataColumn In _dt.Columns

        '            Select Case Col.ColumnName.ToString.ToUpper
        '                Case "FNHSysEmpID".ToUpper, "FTEmpCode".ToUpper, "FTEmpName".ToUpper, "FTEmpTypeCode".ToUpper, "FTDeptCode".ToUpper, "FTDivisonCode".ToUpper, "FTSectCode".ToUpper, "FTUnitSectCode".ToUpper, "FTPositCode".ToUpper
        '                Case Else
        '                    _colcount = _colcount + 1
        '                    Dim ColG As New DevExpress.XtraGrid.Columns.GridColumn
        '                    With ColG
        '                        .Visible = True
        '                        .FieldName = Col.ColumnName.ToString

        '                        .Name = "FTSubOrderNo" & Col.ColumnName.ToString
        '                        .Caption = Col.ColumnName.ToString

        '                    End With

        '                    .Columns.Add(ColG)

        '                    With .Columns(Col.ColumnName.ToString)

        '                        .OptionsFilter.AllowAutoFilter = False
        '                        .OptionsFilter.AllowFilter = False
        '                        .DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
        '                        .DisplayFormat.FormatString = "{0:n2}"
        '                        .AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '                        .AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
        '                        .Width = 150

        '                        With .OptionsColumn
        '                            .AllowMove = False
        '                            .AllowGroup = DevExpress.Utils.DefaultBoolean.False
        '                            .AllowSort = DevExpress.Utils.DefaultBoolean.False
        '                            .AllowEdit = False
        '                            .ReadOnly = True
        '                        End With

        '                    End With

        '                    .Columns(Col.ColumnName.ToString).Width = 90
        '                    .Columns(Col.ColumnName.ToString).Summary.Add(DevExpress.Data.SummaryItemType.Sum)
        '                    .Columns(Col.ColumnName.ToString).SummaryItem.DisplayFormat = "{0:n2}"

        '            End Select

        '        Next

        '    End If


        'End With

        'Me.ogcIncentiveDocket.DataSource = _dt.Copy

        '_colcount = 0

    End Sub

#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Call InitGrid()
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

            Me.FNHSysCmpId.Text = "1"
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode



            ' Call InitGrid()
            Call HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvTimAttendanceDocket)
            StateCal = False


        Catch ex As Exception
        End Try
    End Sub
    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If Me.FDStartDate.Text <> "" Then
            If Me.FDEndDate.Text <> "" Then
                Call LoadData()
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDEndDate_lbl.Text)
                FDEndDate.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateRequest_lbl.Text)
            FDStartDate.Focus()
        End If
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvTimAttendanceDocket)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        With Me.ogvTimAttendanceDocket

            Dim _Spls As New HI.TL.SplashScreen("Generating data...   Please Wait  ")
            Me.LoadData()
            _Spls.Close()

            Dim _Qry As String = ""

            _Qry &= "   {THRMEmployee.FNHSysCmpId}=" & HI.ST.SysInfo.CmpID & ""

            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & "AND  {V_SMP.FTEmpTypeCode}='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "'"
            End If

            '------Criteria By Employeee Code
            If Me.FDStartDate.Text <> "" Then
                _Qry &= vbCrLf & "AND  {V_SMP.FTDate}>='" & HI.UL.ULDate.ConvertEnDB(FDStartDate.Text) & "'"
            End If
            If Me.FDEndDate.Text <> "" Then
                _Qry &= vbCrLf & "AND  {V_SMP.FTDate}<='" & HI.UL.ULDate.ConvertEnDB(FDEndDate.Text) & "'"
            End If
            If Me.FNHSysEmpId.Text <> "" Then
                _Qry &= vbCrLf & " AND {V_SMP.FTEmpCode} >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
            End If

            If Me.FNHSysEmpIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND {V_SMP.FTEmpCode} <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
            End If

            '------Criteria By Department
            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & " AND  {V_SMP.FTDeptCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
            End If

            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  {V_SMP.FTDeptCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
            End If

            '------Criteria By Division
            If Me.FNHSysDivisonId.Text <> "" Then
                _Qry &= vbCrLf & " AND  {V_SMP.FTDivisonCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
            End If

            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  {V_SMP.FTDivisonCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
            End If

            '------Criteria By Sect
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND  {V_SMP.FTSectCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
            End If

            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  {V_SMP.FTSectCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
            End If

            '------Criteria Unit Sect
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND   {V_SMP.FTUnitSectCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
            End If

            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND   {V_SMP.FTUnitSectCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
            End If



            With New HI.RP.Report

                If Me.FDStartDate.Text <> "" Then
                    .AddParameter("SFTDate", Me.FDStartDate.Text)
                End If

                If Me.FDEndDate.Text <> "" Then
                    .AddParameter("EFTDate", Me.FDEndDate.Text)
                End If

                .FormTitle = Me.Text
                .ReportFolderName = "Human Report\"
                .ReportName = "RptSample.rpt"
                .Formular = _Qry
                .Preview()
            End With

        End With
    End Sub


End Class