Imports DevExpress.Data

Public Class wTimeAttendance
    Private dtTimeColor As DataTable
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

        Call InitGrid()

    End Sub

#Region "Variable"
    Private _RowDataChange As Boolean
#End Region

#Region "Property"

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

#Region "Initial Grid"
    Private Sub InitGrid()

        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = "FTDateTrans"
        Dim sFieldSum As String = "FNEmpWork|FNLateNormalMin|FNLateNormalCut|FNAbsent|FNCutAbsent|FNAbsentCut"
        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""
        Dim sFieldCustomSum As String = "FNTime|FNOTRequest|FNOT1|FNOT1_5|FNOT2|FNOT3|FNOT4"
        Dim sFieldCustomGrpSum As String = ""

        With ogvtime

            .ClearGrouping()
            .ClearDocument()

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

    Private Sub Cancel_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepFTScanAIn.EditValueChanging _
, RepFTScanAOTIn.EditValueChanging, RepFTScanAOTIn2.EditValueChanging, RepFTScanAOTOut.EditValueChanging _
, RepFTScanAOTOut2.EditValueChanging, RepFTScanAOut.EditValueChanging, RepFTScanMIn.EditValueChanging, RepFTTranStaCode.EditValueChanging, ReplFTScanMOut.EditValueChanging, RepTime.EditValueChanging

        Try
            With Me.ogvtime

                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                If Not (ocmsavedetail.Enabled) Then
                    e.Cancel = True
                Else
                    If HI.HRCAL.Time.CheckClosePeriod(HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTDateTrans").ToString), Val(Me.FNHSysEmpID.Properties.Tag.ToString)) = True Then
                        HI.MG.ShowMsg.mInfo("ไม่สามารถทำการแก้ไขข้อมูลหลังการปิดงวดได้ กรุณาทำการตรวจสอบ", 14040001, Me.Text)
                        e.Cancel = True
                    End If
                End If

            End With
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


    Private Sub Gridview_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles ogvtime.CustomSummaryCalculate
        If e.SummaryProcess = CustomSummaryProcess.Start Then
            InitStartValue()
        End If

        Select Case CType(e.Item, DevExpress.XtraGrid.GridSummaryItem).FieldName.ToString
            Case "FNTime", "FNOTRequest", "FNOT1", "FNOT1_5", "FNOT2", "FNOT3", "FNOT4"
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

                        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal  Then
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

#Region "Procedure"

    Private _GenTab As Boolean = False
    Private Sub GenertaeTabPayterm(ByVal FTYear As String, ByVal FNHSysEmpID As String)
        _GenTab = True

        Try

            Dim _PathEmpPic As String
            _PathEmpPic = ""
            Dim cmdstring As String = "Select Top 1 FTCfgData FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE FTCfgName='PathEmpPic'"

            _PathEmpPic = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "")

            Dim _dt As DataTable
            Dim _Qry As String = ""
            _Qry = " SELECT    TOP 1     M.FTEmpCode, M.FTEmpCodeRefer, M.FTEmpNameTH, M.FTEmpSurnameTH, M.FTEmpNicknameTH, M.FTEmpNameEN, M.FNHSysEmpTypeId, M.FNHSysDeptId, "
            _Qry &= vbCrLf & "   D.FTDeptCode, Di.FTDivisonCode, M.FNHSysDivisonId, M.FNHSysSectId, S.FTSectCode, ET.FTEmpTypeCode, M.FNHSysUnitSectId, US.FTUnitSectCode,"
            _Qry &= vbCrLf & "  M.FNHSysEmpID, M.FTEmpPicName, M.FNHSysPositId, P.FTPositCode,M.FDDateEnd "
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId = P.FNHSysPositId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
            _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID  =" & Val(FNHSysEmpID) & ""
            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            FTEmpPicName.Image = Nothing
            If _dt.Rows.Count > 0 Then
                For Each R As DataRow In _dt.Rows
                    If _PathEmpPic = "" Then
                        FTEmpPicName.Image = HI.UL.ULImage.LoadImage(HI.ST.SysInfo.SysPath & "EmpPicture\" & R!FTEmpPicName.ToString)
                    Else
                        FTEmpPicName.Image = HI.UL.ULImage.LoadImage(_PathEmpPic & R!FTEmpPicName.ToString)
                    End If
                    FNHSysEmpTypeId.Text = R!FTEmpTypeCode.ToString
                    FNHSysDeptId.Text = R!FTDeptCode.ToString
                    FNHSysDivisonId.Text = R!FTDivisonCode.ToString
                    FNHSysSectId.Text = R!FTSectCode.ToString
                    FNHSysUnitSectId.Text = R!FTUnitSectCode.ToString
                    FNHSysPositId.Text = R!FTPositCode.ToString
                    FDDateEnd.Text = HI.UL.ULDate.ConvertEN(R!FDDateEnd.ToString)
                Next
            Else
                FNHSysEmpTypeId.Text = ""
                FNHSysDeptId.Text = ""
                FNHSysDivisonId.Text = ""
                FNHSysSectId.Text = ""
                FNHSysUnitSectId.Text = ""
                FNHSysPositId.Text = ""
                FDDateEnd.Text = ""
            End If

            _Qry = "  SELECT        P.FTPayYear, P.FTPayTerm"
            _Qry &= vbCrLf & "  ,P.FDCalDateBegin "
            _Qry &= vbCrLf & " , P.FDCalDateEnd"
            _Qry &= vbCrLf & ", P.FDDateClose, P.FNMonth AS FTPayMonth, P.FTTermOfMonth"
            _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS P WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) ON P.FNHSysEmpTypeId = M.FNHSysEmpTypeId"
            _Qry &= vbCrLf & "  WHERE    M.FNHSysEmpID=" & Val(FNHSysEmpID) & " AND     (P.FTPayYear = '" & FTYear & "')"
            _Qry &= vbCrLf & "  ORDER BY P.FTPayTerm"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            otb.TabPages.Clear()
            ogdtime.DataSource = Nothing

            _Qry = " SELECT  TOP 1  FTPayTerm, FTPayYear, FNHSysEmpTypeId"
            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayHD"
            _Qry &= vbCrLf & " WHERE FNHSysEmpTypeId=" & Val("" & FNHSysEmpTypeId.Properties.Tag.ToString) & " "
            _Qry &= vbCrLf & " AND FTPayYear='" & FTYear & "' "

            Dim _CTerm As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

            Dim _Totalpage As Integer = 0
            Dim _SelectpageInd As Integer = 0
            For Each R As DataRow In _dt.Rows

                Dim Otp As New DevExpress.XtraTab.XtraTabPage()
                With Otp
                    .Name = "tp" & R!FTPayTerm.ToString
                    .Text = "    งวด " & R!FTPayTerm.ToString & vbCrLf & "(" & HI.UL.ULDate.ConvertEN(R!FDCalDateBegin.ToString) & ")"
                End With

                If R!FTPayTerm.ToString = _CTerm Then
                    _SelectpageInd = _Totalpage
                End If

                otb.TabPages.Add(Otp)
                _Totalpage = _Totalpage + 1
            Next


            Try
                If _Totalpage > 0 Then
                    otb.SelectedTabPageIndex = _SelectpageInd
                    Call LoadDatePeriod(Microsoft.VisualBasic.Right(otb.SelectedTabPage.Name.ToString, 2))
                End If
            Catch ex As Exception
            End Try
            _GenTab = False
        Catch ex As Exception

        End Try
       
        _GenTab = False
    End Sub

    Private Sub BindShiftCodeToCombox()
        Dim _Qry As String
        _Qry = " SELECT FNHSysShiftID,FTShiftCode,FTShiftNameTH,FTShiftNameEN "
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift As A With(NOLOCK) "
        _Qry &= vbCrLf & "  WHERE ISNULL(FTStateActive,'')='1' "
        _Qry &= vbCrLf & "  ORDER BY FTShiftCode "
        Dim dt As DataTable
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        RepFTShiftCode.Items.Clear()

        For Each R As DataRow In dt.Rows
            RepFTShiftCode.Items.Add(R!FTShiftCode.ToString)
        Next

        dt.Dispose()
    End Sub


    Private Sub LoadDatePeriod(ByVal Period As String)



        Dim _Term As String = Period
        Dim _Year As String = Me.FNYear.Text
        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Call BindShiftCodeToCombox()

        _Qry = " SELECT        TOP 1  FTPayTerm, FTPayYear, FNHSysEmpTypeId,FNMonth AS  FTPayMonth, FTTermOfMonth, FDPayDate, FDCalDateBegin, FDCalDateEnd, FDDateClose, "
        _Qry &= vbCrLf & "  FTStateTermEndOfYear  "
        _Qry &= vbCrLf & "   FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayDT AS D WITH (NOLOCK) "
        _Qry &= vbCrLf & " WHERE FTPayYear='" & Me.FNYear.Text & "' AND  FTPayTerm ='" & Period & "' AND FNHSysEmpTypeId=" & Val(Me.FNHSysEmpTypeId.Properties.Tag.ToString) & " "
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        If _dt.Rows.Count > 0 Then
            _StartDate = _dt.Rows(0)!FDCalDateBegin.ToString
            _EndDate = _dt.Rows(0)!FDCalDateEnd.ToString

            _Qry = "   Select  '1'"
            _Qry &= vbCrLf & "  from("
            _Qry &= vbCrLf & "  SELECT Count(T.FTDateTrans) AS TotalDay"
            _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH(Nolock) "
            _Qry &= vbCrLf & "  WHERE T.FTDateTrans>='" & _StartDate & "' AND T.FTDateTrans <='" & _EndDate & "' AND T.FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & ""
            _Qry &= vbCrLf & "  ) AS A"
            _Qry &= vbCrLf & "  WHERE (DateDiff(DAY,'" & _StartDate & "','" & _EndDate & "'))+1 = TotalDay "

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") <> "1" Then
                Dim _TmpSDate As String = _StartDate
                Dim _TmpEDate As String = _EndDate

                Do While _TmpSDate <= _TmpEDate

                    _Qry = "Exec SP_INSERT_TIME '" & _TmpSDate & "'," & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & " "
                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)

                    _TmpSDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_TmpSDate, 1))
                Loop
            End If
        End If

        dtTimeColor = HI.HRCAL.Time.LoadTimeColor

        _Qry = " SELECT  CASE WHEN  ISDATE(T.FTDateTrans) = 1 THEN Convert(varchar(10),Convert(Datetime,T.FTDateTrans),103) ELSE '' END As FTDateTrans"
        If ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",CASE WHEN  ISDATE(T.FTDateTrans) = 1 THEN  case when DATENAME(WEEKDAY, T.FTDateTrans)='Monday' then 'วันจันทร์'  "
            _Qry &= vbCrLf & "when DATENAME(WEEKDAY, T.FTDateTrans)='Tuesday' then 'วันอังคาร' "
            _Qry &= vbCrLf & "when DATENAME(WEEKDAY, T.FTDateTrans)='Wednesday' then 'วันพุธ' "
            _Qry &= vbCrLf & "when DATENAME(WEEKDAY, T.FTDateTrans)='Thursday' then 'วันพฤหัสบดี' "
            _Qry &= vbCrLf & "when DATENAME(WEEKDAY, T.FTDateTrans)='Friday' then 'วันศุกร์' "
            _Qry &= vbCrLf & "when DATENAME(WEEKDAY, T.FTDateTrans)='Saturday' then 'วันเสาร์' "
            _Qry &= vbCrLf & "when DATENAME(WEEKDAY, T.FTDateTrans)='Sunday' then 'วันอาทิตย์' end ELSE '' END AS FDDate"
        Else
            _Qry &= vbCrLf & ",CASE WHEN  ISDATE(T.FTDateTrans) = 1 THEN  DATENAME(WEEKDAY, T.FTDateTrans) ELSE '' END AS FDDate"
        End If
        _Qry &= vbCrLf & "  , T.FNHSysShiftIDOrg, T.FNHSysShiftID, SH.FTShiftCode"
        _Qry &= vbCrLf & "  ,SH.FTShiftCode AS FTShiftCodeOrg"
        _Qry &= vbCrLf & "  , SH.FTIn1 AS FTCheckIn1"
        _Qry &= vbCrLf & "  , SH.FTOut1 As FTCheckOut1"
        _Qry &= vbCrLf & "  , SH.FTIn2 As FTCheckIn2"
        _Qry &= vbCrLf & "  , SH.FTOut2 AS FTCheckOut2"
        _Qry &= vbCrLf & "  , OT.FTOtIn AS FTCheckOTAIn1"
        _Qry &= vbCrLf & "  , OT.FTOtOut AS FTCheckOTAOut1"
        _Qry &= vbCrLf & "  , OT.FTOtIn3 AS FTCheckOTAIn2"
        _Qry &= vbCrLf & "  , OT.FTOtOut3 AS FTCheckOTAOut2, T.FTIn1 , "
        _Qry &= vbCrLf & "    T.FTOut1 , T.FTIn2 , T.FTOut2 , T.FTIn3, T.FTOut3, T.FTIn4, T.FTOut4"
        _Qry &= vbCrLf & " ,FTScanMIn, FTScanMOut, FTScanAIn, FTScanAOut, FTScanAOTIn, FTScanAOTOut, FTScanAOTIn2,FTScanAOTOut2"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNTime),'.',':') AS FNTime"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT1),'.',':') AS FNOT1"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT1_5),'.',':') AS FNOT1_5"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT2),'.',':') AS FNOT2"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT3),'.',':') AS FNOT3"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT4),'.',':') AS FNOT4"
        _Qry &= vbCrLf & " , T.FNLateNormalMin, T.FNLateNormalCut, T.FNAbsentCut, T.FNAbsent, T.FNCutAbsent, T.FNHSysEmpID"
        _Qry &= vbCrLf & " ,T.FNHSysTranStaId,TM.FTTranStaCode,SH.FTOverClock "

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & ",TM.FTTranStaNameTH AS FTTranStaName"
        Else
            _Qry &= vbCrLf & ",TM.FTTranStaNameEN AS FTTranStaName"
        End If

        '_Qry &= vbCrLf & ",CASE WHEN  T.FNScanCtrl = 0 THEN CASE WHEN (FTScanMIn='' OR (FTScanAOut='' AND FTScanAOTOut =''))   THEN '1' Else '0' END"
        '_Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN (FTScanMIn='' OR FTScanAOut='') OR (OT.FTOtIn  <>'' AND (FTScanAOTIn='' OR FTScanAOTOut=''))  THEN '1' Else '0' END"
        '_Qry &= vbCrLf & "        ELSE  CASE  WHEN (FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')  OR (OT.FTOtIn  <>'' AND (FTScanAOTIn='' OR FTScanAOTOut=''))   THEN '1' Else '0' END END  AS FTStateError"

        '_Qry &= vbCrLf & ",CASE WHEN  T.FNScanCtrl = 0 THEN CASE WHEN (FTScanMIn='' OR (FTScanAOut='' AND FTScanAOTOut =''))   THEN '1' Else '0' END"
        '_Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN (FTScanMIn='' OR FTScanAOut='')   THEN '1' Else '0' END"
        '_Qry &= vbCrLf & "        ELSE  CASE  WHEN (FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')     THEN '1' Else '0' END END  AS FTStateError"

        _Qry &= vbCrLf & " ,CASE WHEN  T.FNScanCtrl = 0 THEN CASE WHEN (FTScanMIn='' OR (FTScanAOut='' AND FTScanAOTOut =''))   THEN '1' Else '0' END"
        _Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN (FTScanMIn='' OR FTScanAOut='') OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>'')))    THEN '1' Else '0' END"
        _Qry &= vbCrLf & "        ELSE  CASE  WHEN (FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')  OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>'')))      THEN '1' Else '0' END END AS FTStateError "


        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),OT.FNOtNetTime),'.',':') AS FNOTRequest "
        _Qry &= vbCrLf & " ,ISNULL(HD.FDHolidayDate,'') AS FDHolidayDate "

        _Qry &= vbCrLf & " ,CASE WHEN T.FTWeekDay=1 AND  ((EHL.FNHSysEmpID  IS NULL  AND ( ISNULL(SH.FTSunday,'0') ='1' OR ISNULL(ETHL.FDHolidayDate,'') <>'' ))  OR ISNULL(EHL.FTSunday,'0') ='1'  ) THEN '1'  "
        _Qry &= vbCrLf & "  WHEN T.FTWeekDay=2 AND   ((EHL.FNHSysEmpID  IS NULL  AND ( ISNULL(SH.FTMonday,'0') ='1' OR ISNULL(ETHL.FDHolidayDate,'') <>'' ))  OR ISNULL(EHL.FTMonday,'0') ='1'  ) THEN '1'  "
        _Qry &= vbCrLf & "  WHEN T.FTWeekDay=3 AND   ((EHL.FNHSysEmpID  IS NULL  AND ( ISNULL(SH.FTTuesday,'0') ='1' OR ISNULL(ETHL.FDHolidayDate,'') <>'' ))  OR ISNULL(EHL.FTTuesday,'0') ='1'  )  THEN '1'  "
        _Qry &= vbCrLf & "  WHEN T.FTWeekDay=4 AND   ((EHL.FNHSysEmpID  IS NULL  AND ( ISNULL(SH.FTWednesday,'0') ='1' OR ISNULL(ETHL.FDHolidayDate,'') <>'' ))  OR ISNULL(EHL.FTWednesday,'0') ='1'  )  THEN '1'  "
        _Qry &= vbCrLf & "  WHEN T.FTWeekDay=5 AND   ((EHL.FNHSysEmpID  IS NULL  AND ( ISNULL(SH.FTThursday,'0') ='1' OR ISNULL(ETHL.FDHolidayDate,'') <>'' ))  OR ISNULL(EHL.FTThursday,'0') ='1'  )  THEN '1'  "
        _Qry &= vbCrLf & "  WHEN T.FTWeekDay=6 AND   ((EHL.FNHSysEmpID  IS NULL  AND ( ISNULL(SH.FTFriday,'0') ='1' OR ISNULL(ETHL.FDHolidayDate,'') <>'' ))  OR ISNULL(EHL.FTFriday,'0') ='1'  )  THEN '1'  "
        _Qry &= vbCrLf & "  WHEN T.FTWeekDay=7 AND  ((EHL.FNHSysEmpID  IS NULL  AND ( ISNULL(SH.FTSaturday,'0') ='1' OR ISNULL(ETHL.FDHolidayDate,'') <>'' ))  OR ISNULL(EHL.FTSaturday,'0') ='1'  )  THEN '1'  "
        _Qry &= vbCrLf & " ELSE '0' END AS FTWeekly "

        _Qry &= vbCrLf & " ,ISNULL((SELECT TOP 1 FTLeaveType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave WITH(NOLOCK)  WHERE FNHSysEmpID=T.FNHSysEmpID AND FTDateTrans=T.FTDateTrans   ),'') AS FTLeaveCode "

        _Qry &= vbCrLf & ",CASE WHEN (FTScanMIn + FTScanMOut + FTScanAIn + FTScanAOut  + FTScanAOTIn+ FTScanAOTOut) <>'' Then 1 Else 0 END AS FNEmpWork "
        _Qry &= vbCrLf & ",  ISNULL(SPD.FTDate,'') AS SPD"
        _Qry &= vbCrLf & " ,  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_LoadTimeLeave(T.FNHSysEmpID,T.FTDateTrans,'" & HI.ST.Lang.Language.ToString & "') AS FTLeave"
        _Qry &= vbCrLf & ", CASE WHEN  ISDATE(ISNULL(M.FDDateEnd,'')) = 1 THEN Convert(varchar(10),Convert(Datetime,ISNULL(M.FDDateEnd,'')),103) ELSE '' END  AS FDDateEnd"
        _Qry &= vbCrLf & ",M.FNUseBarcode"
        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK) LEFT OUTER JOIN "
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS SH WITH (NOLOCK) ON T.FNHSysShiftID = SH.FNHSysShiftID LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    THRTDailyOTRequest AS OT WITH (NOLOCK) ON T.FNHSysEmpID = OT.FNHSysEmpID AND T.FTDateTrans = OT.FTDateRequest"
        _Qry &= vbCrLf & "  INNER JOIN          [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) ON T.FNHSysEmpID=M.FNHSysEmpID "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMTranStatus AS TM ON T.FNHSysTranStaId = TM.FNHSysTranStaId "

        '_Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday AS HD WITH (NOLOCK) ON T.FTDateTrans = HD.FDHolidayDate"
        _Qry &= vbCrLf & "    OUTER APPLY ( SELECT TOP 1  FNHSysHolidayId, FDHolidayDate, FTHolidayNameTH, FTHolidayNameEN, FNHSysCmpId FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday WITH (NOLOCK) WHERE ISNULL( FTStateActive,'') ='1'  AND FNHSysCmpId = M.FNHSysCmpId AND T.FTDateTrans = FDHolidayDate) AS  HD "
        _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailySpecial AS SPD WITH (NOLOCK) ON T.FTDateTrans = SPD.FTDate AND T.FNHSysEmpID=SPD.FNHSysEmpID"

        _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly AS EHL WITH (NOLOCK) ON T.FNHSysEmpID=EHL.FNHSysEmpID"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmpTypeWeeklySpecial AS ETHL WITH (NOLOCK) ON T.FTDateTrans = ETHL.FDHolidayDate AND M.FNHSysEmpTypeId=ETHL.FNHSysEmpTypeId"


        _Qry &= vbCrLf & "  WHERE T.FTDateTrans>='" & _StartDate & "' AND T.FTDateTrans <='" & _EndDate & "' AND T.FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & ""
        _Qry &= vbCrLf & "  AND  (T.FTDateTrans <=  CASE WHEN ISNULL(M.FDDateEnd,'') ='' THEN '9999/99/99' ELSE  ISNULL(M.FDDateEnd,'')  END)   "

        If HI.ST.SysInfo.HideSunday Then
            _Qry &= vbCrLf & " AND ISNULL(T.FTWeekDay,'') <>'1' "
        End If

        _Qry &= vbCrLf & "  ORDER BY T.FTDateTrans "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        Me.ogdtime.DataSource = _dt
        Me.ogvtime.BestFitColumns()
        _RowDataChange = False

    End Sub

#End Region

#Region "General"

    Private Sub FNHSysEmpID_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNHSysEmpID.EditValueChanged

        If Me.InvokeRequired Then
            Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FNHSysEmpID_EditValueChanged), New Object() {sender, e})
        Else
            If FNHSysEmpID.Text <> "" Then

                Dim _Qry As String = "SELECT TOP 1 FNHSysEmpID  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WITH(NOLOCK) WHERE FTEmpCode ='" & HI.UL.ULF.rpQuoted(FNHSysEmpID.Text) & "' "
                FNHSysEmpID.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

                Call GenertaeTabPayterm(FNYear.Text, FNHSysEmpID.Properties.Tag.ToString)
            Else
                otb.TabPages.Clear()
                ogdtime.DataSource = Nothing
            End If
        End If

    End Sub

    Private Sub FNYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FNYear.SelectedIndexChanged
        If FNHSysEmpID.Text <> "" And FNHSysEmpID.Properties.Tag.ToString <> "" Then
            Call GenertaeTabPayterm(FNYear.Text, FNHSysEmpID.Properties.Tag.ToString)
        Else
            otb.TabPages.Clear()
            ogdtime.DataSource = Nothing
        End If
    End Sub

    Private Sub otb_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles otb.SelectedPageChanged
        Try
            If (_GenTab) Then Exit Sub
            If Not (otb.SelectedTabPage Is Nothing) Then
                Call LoadDatePeriod(Microsoft.VisualBasic.Right(otb.SelectedTabPage.Name.ToString, 2))
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvtime_BeforeLeaveRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles ogvtime.BeforeLeaveRow

        With ogvtime
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            If Not (ocmsavedetail.Enabled) Then Exit Sub
            If Not (_RowDataChange) Then Exit Sub
            Dim _Qry As String = ""

            _Qry = " SELECT TOP 1  FTDateTrans  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans WITH(NOLOCK)  "
            _Qry &= vbCrLf & " WHERE FTDateTrans='" & HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTDateTrans").ToString) & "'  "
            _Qry &= vbCrLf & " AND FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & ""

            If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") = "" Then
                _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.SP_INSERT_TIME '" & HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTDateTrans").ToString) & "'," & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & ""
                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)
            End If

            Dim _ScanCardCtl As Integer = HI.Conn.SQLConn.GetField("SELECT TOP 1 FNScanCtrl FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WITH(NOLOCK) WHERE FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & " ", Conn.DB.DataBaseName.DB_HR, "0")

            Dim _CheckTimeMIn As String = ""
            Dim _CheckTimeMOut As String = ""
            Dim _CheckTimeAIn As String = ""
            Dim _CheckTimeAOut As String = ""
            Dim _CheckTimeOTIn1 As String = ""
            Dim _CheckTimeOTOut1 As String = ""
            Dim _CheckTimeOTIn2 As String = ""
            Dim _CheckTimeOTOut2 As String = ""
            Dim _TmpScanIn As String, _TmpScanOut As String
            Dim _ActualTimMIn As String, _ActualTimMOut As String
            Dim _ActualTimAIn As String, _ActualTimAOut As String
            Dim _ActualTimOTIn1 As String, _ActualTimOTOut1 As String, _ScanCardOverClock As String
            Dim _ActualTimOTIn2 As String, _ActualTimOTOut2 As String
            Dim FTScanMIn As String, FTScanMOut As String
            Dim FTScanAIn As String, FTScanAOut As String
            Dim FTScanAOTIn As String, FTScanAOTOut As String
            Dim FTScanAOTIn2 As String, FTScanAOTOut2 As String
            Dim _FTStateAcceptTimeAuto As String = ""
            Dim FTScanOTMInM, FTScanOTMOutM, _ActualScanOTInM, _ActualScanOTOutM, _FTOtMIn, _FTOtMOut As String
            FTScanOTMInM = "" : FTScanOTMOutM = "" : _ActualScanOTInM = "" : _ActualScanOTOutM = "" : _FTOtMIn = "" : _FTOtMOut = ""
            Dim _FTShiftId As String = ""

            _Qry = " SELECT TOP 1 FTStateAcceptTimeAuto "
            _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS M WITH(NOLOCK)"
            _Qry &= vbCrLf & " WHERE FNHSysEmpTypeId=" & Integer.Parse(Val(FNHSysEmpTypeId.Properties.Tag.ToString)) & ""

            _FTStateAcceptTimeAuto = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_MASTER, "")

            _FTShiftId = "" & .GetRowCellValue(.FocusedRowHandle, "FTShiftCode").ToString

            _ScanCardOverClock = "" & .GetRowCellValue(.FocusedRowHandle, "FTOverClock").ToString
            _CheckTimeMIn = "" & .GetRowCellValue(.FocusedRowHandle, "FTCheckIn1").ToString
            _CheckTimeMOut = "" & .GetRowCellValue(.FocusedRowHandle, "FTCheckOut1").ToString
            _CheckTimeAIn = "" & .GetRowCellValue(.FocusedRowHandle, "FTCheckIn2").ToString
            _CheckTimeAOut = "" & .GetRowCellValue(.FocusedRowHandle, "FTCheckOut2").ToString
            _CheckTimeOTIn1 = "" & .GetRowCellValue(.FocusedRowHandle, "FTCheckOTAIn1").ToString
            _CheckTimeOTOut1 = "" & .GetRowCellValue(.FocusedRowHandle, "FTCheckOTAOut1").ToString

            _CheckTimeOTIn2 = "" & .GetRowCellValue(.FocusedRowHandle, "FTCheckOTAIn2").ToString
            _CheckTimeOTOut2 = "" & .GetRowCellValue(.FocusedRowHandle, "FTCheckOTAOut2").ToString
            FTScanMIn = "" & .GetRowCellValue(.FocusedRowHandle, "FTScanMIn").ToString
            FTScanMOut = "" & .GetRowCellValue(.FocusedRowHandle, "FTScanMOut").ToString
            FTScanAIn = "" & .GetRowCellValue(.FocusedRowHandle, "FTScanAIn").ToString
            FTScanAOut = "" & .GetRowCellValue(.FocusedRowHandle, "FTScanAOut").ToString
            FTScanAOTIn = "" & .GetRowCellValue(.FocusedRowHandle, "FTScanAOTIn").ToString
            FTScanAOTOut = "" & .GetRowCellValue(.FocusedRowHandle, "FTScanAOTOut").ToString
            FTScanAOTIn2 = "" & .GetRowCellValue(.FocusedRowHandle, "FTScanAOTIn2").ToString
            FTScanAOTOut2 = "" & .GetRowCellValue(.FocusedRowHandle, "FTScanAOTOut2").ToString

            _ActualTimMIn = "" : _ActualTimMOut = ""
            _ActualTimAIn = "" : _ActualTimAOut = "" : _ActualTimOTIn1 = "" : _ActualTimOTOut1 = "" : _ActualTimOTIn2 = "" : _ActualTimOTOut2 = ""

            _TmpScanIn = "" : _TmpScanOut = ""
            Select Case _ScanCardCtl
                Case 0 ' รูดเข้า - ออก
                    Call HI.HRCAL.Calculate.UpdateScanTwoTime(_ActualTimMIn, _ActualTimMOut,
                             _ActualTimAIn, _ActualTimAOut, _ActualTimOTIn1, _ActualTimOTOut1,
                              _ActualTimOTIn2, _ActualTimOTOut2, FTScanMIn,
                             FTScanMOut, FTScanAIn, FTScanAOut, FTScanAOTIn,
                             FTScanAOTOut, FTScanAOTIn2, FTScanAOTOut2,
                             _CheckTimeMIn, _CheckTimeMOut, _CheckTimeAIn, _CheckTimeAOut, _CheckTimeOTIn1, _CheckTimeOTOut1, _CheckTimeOTIn2, _CheckTimeOTOut2, _ScanCardOverClock,
                             _ScanCardOverClock, Me.ActualNextDate, Me.ActualDate, _TmpScanIn, _TmpScanOut)', _FTShiftId
                Case 1
                    Call HI.HRCAL.Calculate.UpdateScanFourTime(_ActualTimMIn, _ActualTimMOut,
                            _ActualTimAIn, _ActualTimAOut, _ActualTimOTIn1, _ActualTimOTOut1,
                            _ActualTimOTIn2, _ActualTimOTOut2, FTScanMIn,
                            FTScanMOut, FTScanAIn, FTScanAOut, FTScanAOTIn,
                            FTScanAOTOut, FTScanAOTIn2, FTScanAOTOut2,
                            _CheckTimeMIn, _CheckTimeMOut, _CheckTimeAIn, _CheckTimeAOut, _CheckTimeOTIn1, _CheckTimeOTOut1, _CheckTimeOTIn2, _CheckTimeOTOut2,
                            _ScanCardOverClock, _ScanCardOverClock, ActualNextDate, ActualDate, _TmpScanIn, _TmpScanOut,
                                         FTScanOTMInM, FTScanOTMOutM, _ActualScanOTInM, _ActualScanOTOutM, _FTOtMIn, _FTOtMOut)
                Case 2
                    Call HI.HRCAL.Calculate.UpdateScanSixTime(_ActualTimMIn, _ActualTimMOut,
                            _ActualTimAIn, _ActualTimAOut, _ActualTimOTIn1, _ActualTimOTOut1,
                             _ActualTimOTIn2, _ActualTimOTOut2, FTScanMIn,
                            FTScanMOut, FTScanAIn, FTScanAOut, FTScanAOTIn,
                            FTScanAOTOut, FTScanAOTIn2, FTScanAOTOut2,
                            _CheckTimeMIn, _CheckTimeMOut, _CheckTimeAIn, _CheckTimeAOut, _CheckTimeOTIn1, _CheckTimeOTOut1, _CheckTimeOTIn2, _CheckTimeOTOut2,
                            _ScanCardOverClock, _ScanCardOverClock, ActualNextDate, ActualDate, _TmpScanIn, _TmpScanOut,
                                         FTScanOTMInM, FTScanOTMOutM, _ActualScanOTInM, _ActualScanOTOutM, _FTOtMIn, _FTOtMOut)
            End Select

            _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans SET "
            _Qry &= vbCrLf & "   FTIn1='" & _ActualTimMIn & "'"
            _Qry &= vbCrLf & " ,FTOut1='" & _ActualTimMOut & "'"
            _Qry &= vbCrLf & " ,FTIn2='" & _ActualTimAIn & "'"
            _Qry &= vbCrLf & " ,FTOut2='" & _ActualTimAOut & "'"
            _Qry &= vbCrLf & " ,FTIn3='" & _ActualTimOTIn1 & "'"
            _Qry &= vbCrLf & " ,FTOut3='" & _ActualTimOTOut1 & "'"
            _Qry &= vbCrLf & " ,FTIn4='" & _ActualTimOTIn2 & "'"
            _Qry &= vbCrLf & " ,FTOut4='" & _ActualTimOTOut2 & "'"
            _Qry &= vbCrLf & ", FNTime=" & Val(("" & .GetRowCellValue(.FocusedRowHandle, "FNTime").ToString).Replace(":", ".")) & " "
            _Qry &= vbCrLf & ", FNOT1=" & Val(("" & .GetRowCellValue(.FocusedRowHandle, "FNOT1").ToString).Replace(":", ".")) & ""
            _Qry &= vbCrLf & ", FNOT1_5=" & Val(("" & .GetRowCellValue(.FocusedRowHandle, "FNOT1_5").ToString).Replace(":", ".")) & ""
            _Qry &= vbCrLf & ", FNOT2=" & Val(("" & .GetRowCellValue(.FocusedRowHandle, "FNOT2").ToString).Replace(":", ".")) & ""
            _Qry &= vbCrLf & ", FNOT3=" & Val(("" & .GetRowCellValue(.FocusedRowHandle, "FNOT3").ToString).Replace(":", ".")) & ""
            _Qry &= vbCrLf & ", FNOT4=" & Val(("" & .GetRowCellValue(.FocusedRowHandle, "FNOT4").ToString).Replace(":", ".")) & ""
            _Qry &= vbCrLf & ",FNLateNormalMin=" & Val("" & .GetRowCellValue(.FocusedRowHandle, "FNLateNormalMin").ToString) & ""
            _Qry &= vbCrLf & ", FNLateNormalCut=" & Val("" & .GetRowCellValue(.FocusedRowHandle, "FNLateNormalCut").ToString) & ""
            _Qry &= vbCrLf & ", FNAbsentCut=" & Val("" & .GetRowCellValue(.FocusedRowHandle, "FNAbsentCut").ToString) & ""
            _Qry &= vbCrLf & ", FNAbsent=" & Val("" & .GetRowCellValue(.FocusedRowHandle, "FNAbsent").ToString) & ""
            _Qry &= vbCrLf & ", FNCutAbsent=" & Val("" & .GetRowCellValue(.FocusedRowHandle, "FNCutAbsent").ToString) & ""
            _Qry &= vbCrLf & ",FNTimeMin=" & HI.UL.ULDate.HHMMtoMin("" & .GetRowCellValue(.FocusedRowHandle, "FNTime").ToString.Replace(":", ".")) & ""
            _Qry &= vbCrLf & ",FNOT1Min=" & HI.UL.ULDate.HHMMtoMin("" & .GetRowCellValue(.FocusedRowHandle, "FNOT1").ToString.Replace(":", ".")) & ""
            _Qry &= vbCrLf & ",FNOT1_5Min=" & HI.UL.ULDate.HHMMtoMin("" & .GetRowCellValue(.FocusedRowHandle, "FNOT1_5").ToString.Replace(":", ".")) & ""
            _Qry &= vbCrLf & ",FNOT2Min=" & HI.UL.ULDate.HHMMtoMin("" & .GetRowCellValue(.FocusedRowHandle, "FNOT2").ToString.Replace(":", ".")) & ""
            _Qry &= vbCrLf & ",FNOT3Min=" & HI.UL.ULDate.HHMMtoMin("" & .GetRowCellValue(.FocusedRowHandle, "FNOT3").ToString.Replace(":", ".")) & ""
            _Qry &= vbCrLf & ",FNOT4Min=" & HI.UL.ULDate.HHMMtoMin("" & .GetRowCellValue(.FocusedRowHandle, "FNOT4").ToString.Replace(":", ".")) & ""
            _Qry &= vbCrLf & ",FTScanMIn='" & "" & .GetRowCellValue(.FocusedRowHandle, "FTScanMIn").ToString & "' "
            _Qry &= vbCrLf & ",FTScanMOut='" & "" & .GetRowCellValue(.FocusedRowHandle, "FTScanMOut").ToString & "' "
            _Qry &= vbCrLf & ",FTScanAIn='" & "" & .GetRowCellValue(.FocusedRowHandle, "FTScanAIn").ToString & "' "
            _Qry &= vbCrLf & ",FTScanAOut='" & "" & .GetRowCellValue(.FocusedRowHandle, "FTScanAOut").ToString & "' "
            _Qry &= vbCrLf & ",FTScanAOTIn='" & "" & .GetRowCellValue(.FocusedRowHandle, "FTScanAOTIn").ToString & "' "
            _Qry &= vbCrLf & ",FTScanAOTOut='" & "" & .GetRowCellValue(.FocusedRowHandle, "FTScanAOTOut").ToString & "' "
            _Qry &= vbCrLf & ",FTScanAOTIn2='" & "" & .GetRowCellValue(.FocusedRowHandle, "FTScanAOTIn2").ToString & "' "
            _Qry &= vbCrLf & ",FTScanAOTOut2='" & "" & .GetRowCellValue(.FocusedRowHandle, "FTScanAOTOut2").ToString & "' "
            _Qry &= vbCrLf & ",FNHSysTranStaId=" & Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysTranStaId").ToString) & " "
            _Qry &= vbCrLf & ",FTStateEditTime='1'"
            _Qry &= vbCrLf & ",FTStateAccept='" & _FTStateAcceptTimeAuto & "' "
            _Qry &= vbCrLf & "  WHERE FTDateTrans='" & HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTDateTrans").ToString) & "'  "
            _Qry &= vbCrLf & "  AND FNHSysEmpID=" & Val(Me.FNHSysEmpID.Properties.Tag.ToString) & ""

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            ogvtime.UpdateSummary()

            _RowDataChange = False
        End With

    End Sub

    Private Sub ogvtime_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles ogvtime.CellValueChanged
        _RowDataChange = True
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            With ogvtime

                RepFTTranStaCode.Buttons(0).Tag = "79"

                AddHandler RepFTTranStaCode.Click, AddressOf HI.TL.HandlerControl.DynamicResponButtone_Gotocus
                AddHandler RepFTTranStaCode.ButtonClick, AddressOf HI.TL.HandlerControl.DynamicResponButtone_ButtonClick
                AddHandler RepFTTranStaCode.EditValueChanged, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_EditValueChanged
                AddHandler RepFTTranStaCode.Leave, AddressOf HI.TL.HandlerControl.DynamicResponButtonedit_Leave

                .Columns.ColumnByFieldName("FTShiftCode").OptionsColumn.AllowEdit = ocmmoveshift.Enabled
            End With

            Me.FNHSysCmpId.Text = "1"
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode



            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)


        Catch ex As Exception
        End Try
    End Sub


    Private Sub ogvtime_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvtime.RowCellStyle
        With ogvtime

            If dtTimeColor Is Nothing Then
                dtTimeColor = HI.HRCAL.Time.LoadTimeColor()
            End If

            Try
                If .GetRowCellValue(e.RowHandle, "FTStateError") = "1" Then
                    e.Appearance.ForeColor = Drawing.Color.Red
                End If
            Catch ex As Exception
            End Try

            Try
                Select Case True
                    Case ("" & .GetRowCellValue(e.RowHandle, "FTLeaveCode").ToString <> "")
                        Try
                            For Each R As DataRow In dtTimeColor.Select("FTLeaveType='" & ("" & .GetRowCellValue(e.RowHandle, "FTLeaveCode").ToString) & "'")
                                e.Appearance.BackColor = Drawing.Color.FromArgb(R!FTColor.ToString)
                                Exit For
                            Next
                        Catch ex As Exception
                        End Try
                    Case ("" & .GetRowCellValue(e.RowHandle, "FDHolidayDate").ToString <> "")
                        Try
                            For Each R As DataRow In dtTimeColor.Select("FTLeaveType='H'")
                                e.Appearance.BackColor = Drawing.Color.FromArgb(R!FTColor.ToString)
                                Exit For
                            Next
                        Catch ex As Exception
                        End Try
                    Case ("" & .GetRowCellValue(e.RowHandle, "FTWeekly").ToString = "1")
                        Try
                            For Each R As DataRow In dtTimeColor.Select("FTLeaveType='W'")
                                e.Appearance.BackColor = Drawing.Color.FromArgb(R!FTColor.ToString)
                                Exit For
                            Next
                        Catch ex As Exception
                        End Try
                    Case ("" & .GetRowCellValue(e.RowHandle, "SPD").ToString <> "1")
                        Try
                            For Each R As DataRow In dtTimeColor.Select("FTLeaveType='S'")
                                e.Appearance.BackColor = Drawing.Color.FromArgb(R!FTColor.ToString)
                                Exit For
                            Next
                        Catch ex As Exception
                        End Try
                End Select

            Catch ex As Exception
            End Try

        End With
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub RepTimeEdit_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepTimeEdit.EditValueChanging
        If Not (Me.ocmsavedetail.Enabled) Then
            e.Cancel = True
        Else
            Try
                If "" & Me.ogvtime.GetFocusedRowCellValue("FNUseBarcode").ToString = "2" Then
                    e.Cancel = True
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub Caledit_Spin(sender As Object, e As DevExpress.XtraEditors.Controls.SpinEventArgs) Handles RepFTScanAIn.Spin,
        RepFTScanMIn.Spin,
        ReplFTScanMOut.Spin,
        RepFTScanAIn.Spin,
        RepFTScanAOut.Spin,
        RepFTScanAOTIn.Spin,
        RepFTScanAOTOut.Spin,
        RepFTScanAOTIn2.Spin,
        RepFTScanAOTOut2.Spin, RepTimeEdit.Spin
        e.Handled = True
    End Sub

    Private Sub RepFTScanAIn_Leave(sender As Object, e As System.EventArgs) Handles RepTimeEdit.Leave
        Try
            If sender.text <> "" Or sender.EditValue.ToString <> "" Then
                Dim _TmpData As String = sender.EditValue.ToString

                If (_TmpData).Length <= 6 Then
                    _TmpData = Me.ActualDate & " " & _TmpData
                End If

                Dim _Time As String = Format(CDate(_TmpData), "HH:mm")

                'If Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_Time, 3), 1) = ":" And Microsoft.VisualBasic.Left(_Time, 1) = "0" Then
                '    _Time = Microsoft.VisualBasic.Right(_Time, 4)
                'End If

                ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, ogvtime.FocusedColumn, _Time)

                Try
                    CType(ogdtime.DataSource, DataTable).AcceptChanges()
                Catch ex As Exception
                End Try

            End If
        Catch ex As Exception
        End Try

    End Sub

#End Region


    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvtime)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub RepFTShiftCode_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepFTShiftCode.EditValueChanging
        Try
            With Me.ogvtime

                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                If Not (ocmmoveshift.Enabled) Then
                    e.Cancel = True
                Else
                    Try
                        If "" & Me.ogvtime.GetFocusedRowCellValue("FNUseBarcode").ToString = "2" Then
                            e.Cancel = True
                        Else
                            If HI.HRCAL.Time.CheckClosePeriod(HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTDateTrans").ToString), Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID").ToString)) = True Then
                                HI.MG.ShowMsg.mInfo("ไม่สามารถทำการแก้ไขข้อมูลหลังการปิดงวดได้ กรุณาทำการตรวจสอบ", 14040001, Me.Text)
                                e.Cancel = True
                            Else
                                Dim _FNSysShiftIDOrginal As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("FNHSysShiftIDOrg").ToString))
                                If e.NewValue <> "" & .GetFocusedRowCellValue("FTShiftCodeOrg").ToString Then
                                    Dim _Qry As String
                                    Dim _DateTrans As String = HI.UL.ULDate.ConvertEnDB("" & ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTDateTrans").ToString)
                                    Dim _FNSysShiftID As Integer = Integer.Parse(HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysShiftID  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift WITH (NOLOCK) WHERE FTShiftCode='" & HI.UL.ULF.rpQuoted(e.NewValue.ToString) & "'  ", Conn.DB.DataBaseName.DB_HR, "0"))

                                    _Qry = " SELECT TOP 1 FDShiftDate  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift WITH (NOLOCK)"
                                    _Qry &= vbCrLf & " WHERE FNHSysEmpID =" & Val("" & ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FNHSysEmpID").ToString) & " "
                                    _Qry &= vbCrLf & " AND FDShiftDate = '" & HI.UL.ULDate.ConvertEnDB(_DateTrans) & "'"

                                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") <> "" Then

                                        _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift "
                                        _Qry &= vbCrLf & "  SET  FTUpdUser='" & HI.ST.UserInfo.UserName & "' "
                                        _Qry &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & "  "
                                        _Qry &= vbCrLf & " ,FNHSysShiftID=" & Val(_FNSysShiftID) & "  "
                                        _Qry &= vbCrLf & " WHERE FNHSysEmpID =" & Val("" & ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FNHSysEmpID").ToString) & " "
                                        _Qry &= vbCrLf & " AND FDShiftDate = '" & HI.UL.ULDate.ConvertEnDB(_DateTrans) & "'"
                                    Else
                                        _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift (  FTInsUser, FDInsDate, FTInsTime "
                                        _Qry &= vbCrLf & "  , FNHSysEmpID, FNHSysShiftID,FDShiftDate) "
                                        _Qry &= vbCrLf & " SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & "  "
                                        _Qry &= vbCrLf & " ," & Val("" & ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FNHSysEmpID").ToString) & ""
                                        _Qry &= vbCrLf & " ," & Val(_FNSysShiftID) & ",'" & HI.UL.ULDate.ConvertEnDB(_DateTrans) & "' "
                                    End If

                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)


                                    _Qry = " Delete  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift "
                                    _Qry &= vbCrLf & " WHERE FNHSysEmpID =" & Val("" & ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FNHSysEmpID").ToString) & " "
                                    _Qry &= vbCrLf & " AND FDShiftDate = '" & HI.UL.ULDate.ConvertEnDB(_DateTrans) & "'"
                                    _Qry &= vbCrLf & " AND FNHSysShiftID = " & Val(_FNSysShiftIDOrginal) & ""

                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)


                                    _Qry = "  UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans"
                                    _Qry &= vbCrLf & " SET FNHSysShiftID=" & Val(_FNSysShiftID) & " "
                                    _Qry &= vbCrLf & "  WHERE (FNHSysEmpID =" & Val("" & ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FNHSysEmpID").ToString) & " )"
                                    _Qry &= vbCrLf & "   AND FTDateTrans ='" & HI.UL.ULDate.ConvertEnDB(_DateTrans) & "' "
                                    HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_HR)


                                    HI.HRCAL.Calculate.TransTimeCard(HI.ST.UserInfo.UserName, HI.UL.ULDate.ConvertEnDB(_DateTrans), Val("" & ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FNHSysEmpID").ToString))

                                    _Qry = " SELECT  CASE WHEN  ISDATE(T.FTDateTrans) = 1 THEN Convert(varchar(10),Convert(Datetime,T.FTDateTrans),103) ELSE '' END As FTDateTrans"
                                    _Qry &= vbCrLf & "  , T.FNHSysShiftIDOrg, T.FNHSysShiftID, SH.FTShiftCode"
                                    _Qry &= vbCrLf & "  ,SH.FTShiftCode AS FTShiftCodeOrg"
                                    _Qry &= vbCrLf & "  , SH.FTIn1 AS FTCheckIn1"
                                    _Qry &= vbCrLf & "  , SH.FTOut1 As FTCheckOut1"
                                    _Qry &= vbCrLf & "  , SH.FTIn2 As FTCheckIn2"
                                    _Qry &= vbCrLf & "  , SH.FTOut2 AS FTCheckOut2"
                                    _Qry &= vbCrLf & "  , OT.FTOtIn AS FTCheckOTAIn1"
                                    _Qry &= vbCrLf & "  , OT.FTOtOut AS FTCheckOTAOut1"
                                    _Qry &= vbCrLf & "  , OT.FTOtIn3 AS FTCheckOTAIn2"
                                    _Qry &= vbCrLf & "  , OT.FTOtOut3 AS FTCheckOTAOut2, T.FTIn1 , "
                                    _Qry &= vbCrLf & "    T.FTOut1 , T.FTIn2 , T.FTOut2 , T.FTIn3, T.FTOut3, T.FTIn4, T.FTOut4"
                                    _Qry &= vbCrLf & " ,FTScanMIn, FTScanMOut, FTScanAIn, FTScanAOut, FTScanAOTIn, FTScanAOTOut, FTScanAOTIn2,FTScanAOTOut2"
                                    _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNTime),'.',':') AS FNTime"
                                    _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT1),'.',':') AS FNOT1"
                                    _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT1_5),'.',':') AS FNOT1_5"
                                    _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT2),'.',':') AS FNOT2"
                                    _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT3),'.',':') AS FNOT3"
                                    _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT4),'.',':') AS FNOT4"
                                    _Qry &= vbCrLf & " , T.FNLateNormalMin, T.FNLateNormalCut, T.FNAbsentCut, T.FNAbsent, T.FNCutAbsent, T.FNHSysEmpID"
                                    _Qry &= vbCrLf & " ,T.FNHSysTranStaId,TM.FTTranStaCode,SH.FTOverClock "

                                    If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                                        _Qry &= vbCrLf & ",TM.FTTranStaNameTH AS FTTranStaName"
                                    Else
                                        _Qry &= vbCrLf & ",TM.FTTranStaNameEN AS FTTranStaName"
                                    End If

                                    _Qry &= vbCrLf & ",CASE WHEN  T.FNScanCtrl = 0 THEN CASE WHEN (FTScanMIn='' OR (FTScanAOut='' AND FTScanAOTOut =''))   THEN '1' Else '0' END"
                                    _Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN (FTScanMIn='' OR FTScanAOut='')   THEN '1' Else '0' END"
                                    _Qry &= vbCrLf & "        ELSE  CASE  WHEN (FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')     THEN '1' Else '0' END END  AS FTStateError"

                                    _Qry &= vbCrLf & ", Replace(Convert(varchar(30),OT.FNOtNetTime),'.',':') AS FNOTRequest "
                                    _Qry &= vbCrLf & " ,ISNULL(HD.FDHolidayDate,'') AS FDHolidayDate "

                                    _Qry &= vbCrLf & " ,CASE WHEN T.FTWeekDay=1 AND ISNULL(SH.FTSunday,'0') ='1' THEN '1'  "
                                    _Qry &= vbCrLf & "  WHEN T.FTWeekDay=2 AND ISNULL(SH.FTMonday,'0') ='1' THEN '1'  "
                                    _Qry &= vbCrLf & "  WHEN T.FTWeekDay=3 AND ISNULL(SH.FTTuesday,'0') ='1' THEN '1'  "
                                    _Qry &= vbCrLf & "  WHEN T.FTWeekDay=4 AND ISNULL(SH.FTWednesday,'0') ='1' THEN '1'  "
                                    _Qry &= vbCrLf & "  WHEN T.FTWeekDay=5 AND ISNULL(SH.FTThursday,'0') ='1' THEN '1'  "
                                    _Qry &= vbCrLf & "  WHEN T.FTWeekDay=6 AND ISNULL(SH.FTFriday,'0') ='1' THEN '1'  "
                                    _Qry &= vbCrLf & "  WHEN T.FTWeekDay=7 AND ISNULL(SH.FTSaturday,'0') ='1' THEN '1'  "
                                    _Qry &= vbCrLf & " ELSE '0' END AS FTWeekly "

                                    _Qry &= vbCrLf & " ,ISNULL((SELECT TOP 1 FTLeaveType FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave WITH(NOLOCK)  WHERE FNHSysEmpID=T.FNHSysEmpID AND FTDateTrans=T.FTDateTrans   ),'') AS FTLeaveCode "

                                    _Qry &= vbCrLf & ",CASE WHEN (FTScanMIn + FTScanMOut + FTScanAIn + FTScanAOut  + FTScanAOTIn+ FTScanAOTOut) <>'' Then 1 Else 0 END AS FNEmpWork "

                                    _Qry &= vbCrLf & " ,  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_LoadTimeLeave(T.FNHSysEmpID,T.FTDateTrans,'" & HI.ST.Lang.Language.ToString & "') AS FTLeave"
                                    _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK) LEFT OUTER JOIN "
                                    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS SH WITH (NOLOCK) ON T.FNHSysShiftID = SH.FNHSysShiftID LEFT OUTER JOIN"
                                    _Qry &= vbCrLf & "    THRTDailyOTRequest AS OT WITH (NOLOCK) ON T.FNHSysEmpID = OT.FNHSysEmpID AND T.FTDateTrans = OT.FTDateRequest"
                                    _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMTranStatus AS TM ON T.FNHSysTranStaId = TM.FNHSysTranStaId "

                                    _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday AS HD WITH (NOLOCK) ON T.FTDateTrans = HD.FDHolidayDate AND HD.FNHSysCmpId = SH.FNHSysCmpId "
                                    _Qry &= vbCrLf & " WHERE T.FTDateTrans='" & HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTDateTrans").ToString) & "'  "
                                    _Qry &= vbCrLf & " AND T.FNHSysEmpID=" & Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID").ToString) & ""


                                    Dim _TmpDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
                                    If _TmpDt.Rows.Count > 0 Then
                                        Dim _Filter As String = "FNHSysEmpID=" & Val("" & ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FNHSysEmpID").ToString) & " AND FTDateTrans='" & HI.UL.ULDate.ConvertEN("" & ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTDateTrans").ToString) & "' "
                                        With CType(Me.ogdtime.DataSource, DataTable)
                                            For Each R As DataRow In .Select(_Filter)

                                                For Each Col As DataColumn In .Columns
                                                    Try
                                                        R.Item(Col) = _TmpDt.Rows(0).Item(Col.ColumnName.ToString)
                                                    Catch ex As Exception
                                                    End Try
                                                Next

                                                Exit For
                                            Next
                                            .AcceptChanges()
                                        End With
                                    End If

                                    _TmpDt.Dispose()
                                End If
                            End If
                        End If
                    Catch ex As Exception

                    End Try

                  
                End If

            End With
        Catch ex As Exception
        End Try

    End Sub


    Private Sub ogvtime_FocusedColumnChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles ogvtime.FocusedColumnChanged

    End Sub

    Private Sub ocmsavedetail_Click(sender As Object, e As EventArgs) Handles ocmsavedetail.Click

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
End Class