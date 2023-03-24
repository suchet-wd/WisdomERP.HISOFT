Imports DevExpress.Data
Imports System.IO
Imports System.Data

Public Class wAdjustInsuranceDailyAndImport_VN

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


        Call InitGrid()
    End Sub

#Region "Initial Grid"
    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = "FTEmpCode"
        Dim sFieldSum As String = "FNNetProAmt|FNGGAmt|FNProOther"
        Dim sFieldGrpCount As String = "FTDateTrans"
        Dim sFieldGrpSum As String = "FNNetProAmt|FNGGAmt|FNProOther"

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


                        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
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

    Private Sub Cancel_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepFNNetProAmt.EditValueChanging

        Try
            With Me.ogvtime

                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                If Not (ocmsavedetail.Enabled) Then
                    e.Cancel = True
                Else
                    If HI.HRCAL.Time.CheckClosePeriod(HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTDateTrans").ToString), Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID").ToString)) = True Then
                        HI.MG.ShowMsg.mInfo("ไม่สามารถทำการแก้ไขข้อมูลหลังการปิดงวดได้ กรุณาทำการตรวจสอบ", 14040001, Me.Text)
                        e.Cancel = True

                    Else
                        With Me.ogvtime
                            If .FocusedRowHandle < 0 Then Exit Sub
                            Dim _Amt1 As Double = e.NewValue 'CDbl(Format(e.NewValue & 0, "0.00"))
                            Dim _Amt2 As Double = 0
                            Select Case .FocusedColumn.FieldName.ToString.ToUpper
                                Case "FNNetProAmt".ToUpper
                                    _Amt2 = CDbl("" & .GetRowCellValue(.FocusedRowHandle, "FNProOther").ToString)
                                Case "FNProOther".ToUpper
                                    _Amt2 = CDbl("" & .GetRowCellValue(.FocusedRowHandle, "FNNetProAmt").ToString)
                            End Select

                            .SetRowCellValue(.FocusedRowHandle, "FNGGAmt", CDbl(Format(_Amt1 + _Amt2, "0.00")))

                        End With
                    End If
                End If

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadData()

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable
        Dim _dtMain As DataTable = Nothing
        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        Dim _FTStartCalculateDate As String = HI.UL.ULDate.ConvertEnDB(FDStartDate.Text)
        Dim _FTEndCalculateDate As String = HI.UL.ULDate.ConvertEnDB(FDEndDate.Text)

        Do While _FTStartCalculateDate <= _FTEndCalculateDate


            _Qry = " SELECT  '" & HI.UL.ULDate.ConvertEN(_FTStartCalculateDate) & "' AS   FTDateTrans  , M.FNHSysEmpID, M.FTEmpCode,M.FNHSysEmpTypeId"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & " , PR.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"
            Else

                _Qry &= vbCrLf & " , PR.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
            End If

            _Qry &= vbCrLf & " ,ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
            _Qry &= vbCrLf & " ,ISNULL(D.FTDeptCode,'') AS FTDeptCode "
            _Qry &= vbCrLf & " ,ISNULL(Di.FTDivisonCode,'') AS FTDivisonCode "
            _Qry &= vbCrLf & " ,ISNULL(S.FTSectCode,'') AS FTSectCode "
            _Qry &= vbCrLf & " ,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode "
            _Qry &= vbCrLf & " ,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode "
            _Qry &= vbCrLf & " ,ISNULL(Prod.FNProNormal,0) AS FNNetProAmt "
            _Qry &= vbCrLf & " ,ISNULL(Prod.FNProOther,0) AS FNProOther "
            _Qry &= vbCrLf & " ,ISNULL(Prod.FNNetProAmt,0) AS FNGGAmt "
            _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH  (NOLOCK)  LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId = P.FNHSysPositId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId LEFT OUTER JOIN"
            _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
            _Qry &= vbCrLf & "  INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS PR WITH (NOLOCK) ON M.FNHSysPreNameId = PR.FNHSysPreNameId"

            _Qry &= vbCrLf & "  LEFT JOIN ("
            _Qry &= vbCrLf & "  SELECT        FNHSysEmpID, FTDateTrans, FNAmtNormal, FNAmtOT, FNNetAmt, FNProNormal, FNProOT,FNProOther, FNNetProAmt"
            _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily AS A WITH(NOLOCK) "
            _Qry &= vbCrLf & "  WHERE FTDateTrans='" & _FTStartCalculateDate & "'  AND FTDateTrans <> '' "

            _Qry &= vbCrLf & "  ) AS Prod ON M.FNHSysEmpID = Prod.FNHSysEmpID"


            Dim _Qry2 As String = ""

            _Qry2 &= vbCrLf & "  WHERE  M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & "  "
            _Qry2 &= vbCrLf & " AND M.FDDateStart <='" & _FTStartCalculateDate & "' "
            _Qry2 &= vbCrLf & " AND (M.FDDateEnd ='' OR M.FDDateEnd >'" & _FTStartCalculateDate & "' )   "
            _Qry2 &= vbCrLf & " AND  ET.FNInsurType <>0"

            _Qry = _Qry + HI.ST.Security.PermissionFilterEmployee(_Qry2)

            If Me.FNHSysEmpTypeId.Text <> "" Then
                _Qry &= vbCrLf & " AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
            End If

            '------Criteria By Employeee Code
            If Me.FNHSysEmpId.Text <> "" Then
                _Qry &= vbCrLf & " AND M.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
            End If

            If Me.FNHSysEmpIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND M.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
            End If

            '------Criteria By Department
            If Me.FNHSysDeptId.Text <> "" Then
                _Qry &= vbCrLf & " AND  D.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
            End If

            If Me.FNHSysDeptIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  D.FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
            End If

            '------Criteria By Division
            If Me.FNHSysDivisonId.Text <> "" Then
                _Qry &= vbCrLf & " AND  Di.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
            End If

            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  Di.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
            End If

            '------Criteria By Sect
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND  S.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
            End If

            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  S.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
            End If

            '------Criteria Unit Sect
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND   US.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
            End If

            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND   US.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
            End If



            _Qry &= vbCrLf & " ORDER BY  M.FTEmpCode  "

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            If _dtMain Is Nothing Then
                _dtMain = _dt.Copy
            Else
                _dtMain.Merge(_dt.Copy)
            End If

            _FTStartCalculateDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_FTStartCalculateDate, 1))

        Loop


        Me.ogdtime.DataSource = _dtMain
        Me.ogvtime.BestFitColumns()
        ogvtime.ExpandAllGroups()
        _Spls.Close()

        _RowDataChange = False

    End Sub

    Private Sub SaveGrid()
        Try
            If Not (StateCal) Then

                With ogvtime

                    If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then
                        _RowDataChange = False
                        Exit Sub
                    End If

                    If Not (ocmsavedetail.Enabled) Then
                        _RowDataChange = False
                        Exit Sub
                    End If

                    If Not (_RowDataChange) Then Exit Sub
                    StateCal = True

                    'HI..Calculate.CalculateWageDaily(HI.ST.UserInfo.UserName, "" & .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID").ToString, "" & .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpTypeId").ToString, HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTDateTrans").ToString), HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTDateTrans").ToString))
                    HI.HRVN.Calculate.CalculateWageDaily_VN(HI.ST.UserInfo.UserName, "" & .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID").ToString, "" & .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpTypeId").ToString, HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTDateTrans").ToString), HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTDateTrans").ToString))


                    Dim _Qry As String
                    Dim _Amt As Double = 0
                    Dim _AmtOther As Double = 0

                    If IsNumeric("" & .GetRowCellValue(.FocusedRowHandle, "FNNetProAmt").ToString) Then
                        _Amt = CDbl("" & .GetRowCellValue(.FocusedRowHandle, "FNNetProAmt").ToString)
                    End If

                    If IsNumeric("" & .GetRowCellValue(.FocusedRowHandle, "FNProOther").ToString) Then
                        _AmtOther = CDbl("" & .GetRowCellValue(.FocusedRowHandle, "FNProOther").ToString)
                    End If

                    _Qry = "Select Top 1 FNHSysEmpID, FTDateTrans "
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily AS T WITH(NOLOCK)"
                    _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID").ToString) & " AND FTDateTrans='" & HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTDateTrans").ToString) & "'  "

                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") <> "" Then
                        _Qry = "  UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                        _Qry &= vbCrLf & " SET FNProNormal=" & _Amt & ""
                        _Qry &= vbCrLf & ", FNProOT=0 "
                        _Qry &= vbCrLf & ", FNProOther=" & _AmtOther & ""
                        _Qry &= vbCrLf & ", FNNetProAmt=" & _Amt + _AmtOther & " "
                        _Qry &= vbCrLf & ", FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & ", FTUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID").ToString) & " AND FTDateTrans='" & HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTDateTrans").ToString) & "'  "
                    Else
                        _Qry = "  INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                        _Qry &= vbCrLf & " (FTInsUser, FTInsDate, FTInsTime,  FNHSysEmpID, FTDateTrans, FNAmtNormal, FNAmtOT, FNNetAmt, FNProNormal, FNProOT,FNProOther, FNNetProAmt) "
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & " ," & Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID").ToString) & ",'" & HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTDateTrans").ToString) & "'"
                        _Qry &= vbCrLf & " ,0"
                        _Qry &= vbCrLf & " ,0"
                        _Qry &= vbCrLf & " ,0"
                        _Qry &= vbCrLf & " ," & _Amt & ",0," & _AmtOther & "," & _Amt + _AmtOther & ""
                    End If

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    _RowDataChange = False
                End With

                StateCal = False
            End If
        Catch ex As Exception
            _RowDataChange = False
            StateCal = False
        End Try
    End Sub
#End Region

#Region "General"
    Private Sub ogvtime_BeforeLeaveRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles ogvtime.BeforeLeaveRow
        Call SaveGrid()
    End Sub

    Private Sub ogvtime_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles ogvtime.CellValueChanged
        _RowDataChange = True
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Me.FNHSysCmpId.Text = "1"
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
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

    Private Sub ogdtime_Click(sender As System.Object, e As System.EventArgs) Handles ogdtime.Click

    End Sub

    Private Sub ogvtime_FocusedColumnChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles ogvtime.FocusedColumnChanged
        Select Case e.FocusedColumn.FieldName.ToString
            Case "FTScanMIn", "FTScanMOut", "FTScanAIn", "FTScanAOut", "FTScanAOTIn", "FTScanAOTOut"
            Case Else
                Call SaveGrid()
        End Select
    End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub Caledit_Spin(sender As Object, e As DevExpress.XtraEditors.Controls.SpinEventArgs) Handles RepFNNetProAmt.Spin
        e.Handled = True
    End Sub

#End Region

    Private Sub ocmimport_Click(sender As Object, e As EventArgs) Handles ocmimport.Click
        Try
            Dim Op As New System.Windows.Forms.OpenFileDialog
            Op.Filter = "Excel Files(*.xls;*.xlsx;*.csv)|*.xls;*.xlsx;*.csv"
            Op.ShowDialog()

            Try
                If Op.FileName <> "" Then
                    Dim _extension As String = ""
                    _extension = Path.GetExtension(Op.FileName)
                    Dim _dt As New DataTable
                    Dim _ConnString As String = ""

                    Select Case _extension.ToUpper
                        Case ".xls".ToUpper
                            _ConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Op.FileName & ";Extended Properties='Excel 8.0;HDR=YES';"
                        Case ".xlsx".ToUpper, ".csv".ToUpper
                            _ConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Op.FileName & ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';"
                    End Select

                    If _ConnString <> "" Then

                        Dim cn As System.Data.OleDb.OleDbConnection

                        Dim cmd As System.Data.OleDb.OleDbDataAdapter

                        cn = New System.Data.OleDb.OleDbConnection(_ConnString)

                        ' Select the data from Sheet1 of the workbook.

                        cmd = New System.Data.OleDb.OleDbDataAdapter("select * from [Employee$]", cn)

                        cn.Open()
                        Dim _Spls As New HI.TL.SplashScreen("Reading....Please Wait.", "Import Data From File ")

                        Try

                            cmd.Fill(_dt)

                            If _dt.Rows.Count > 0 Then

                                Dim RowCount As Integer = 0
                                Dim _StrFilter As String = ""
                                Dim tmpRow() As DataRow = _dt.Select(_StrFilter)
                                RowCount = tmpRow.Length
                                Dim Ridx As Integer = 0
                                Dim _dtemp As DataTable
                                Dim _FNEmpID As Integer = 0
                                Dim _FNEmpTypeID As Integer = 0
                                Dim _Qry As String = ""
                                Dim _Date As String = ""
                                Dim _Amt As Double = 0
                                Dim _AmtOther As Double = 0
                                Try
                                    For Each R As DataRow In tmpRow

                                        Ridx = Ridx + 1

                                        _Spls.UpdateInformation("Import Data And Calulate Wage Daily.... Record  " & Ridx.ToString & " Of " & RowCount.ToString & "  (" & Format((Ridx * 100.0) / RowCount, "0.00") & " % ) ")

                                        If HI.UL.ULDate.CheckDate(R.Item(0).ToString) <> "" AndAlso IsNumeric(R.Item(8).ToString) Then

                                            _Date = HI.UL.ULDate.ConvertEnDB(R.Item(0).ToString)
                                            _Amt = 0

                                            If IsNumeric(R.Item(8).ToString) Then
                                                _Amt = CDbl(R.Item(8).ToString)
                                            End If
                                            _AmtOther = 0

                                            If IsNumeric(R.Item(9).ToString) Then
                                                _AmtOther = CDbl(R.Item(9).ToString)
                                            End If

                                            _Qry = "SELECT TOP 1  M.FNHSysEmpID, M.FTEmpCode, M.FNHSysCmpId, M.FNHSysEmpTypeId"
                                            _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK)"
                                            _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
                                            _Qry &= vbCrLf & " WHERE M.FTEmpCode='" & HI.UL.ULF.rpQuoted(R.Item(1).ToString) & "'"
                                            _Qry &= vbCrLf & " AND   M.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "
                                            _Qry &= vbCrLf & " AND  ET.FNInsurType <>0"

                                            _dtemp = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

                                            For Each Rx As DataRow In _dtemp.Rows

                                                _FNEmpID = Val(Rx!FNHSysEmpID.ToString)
                                                _FNEmpTypeID = Val(Rx!FNHSysEmpTypeId.ToString)

                                                If _FNEmpID > 0 Then
                                                    HI.HRVN.Calculate.CalculateWageDaily_VN(HI.ST.UserInfo.UserName, _FNEmpID.ToString, _FNEmpTypeID.ToString, _Date, _Date)


                                                    _Qry = "Select Top 1 FNHSysEmpID, FTDateTrans "
                                                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily AS T WITH(NOLOCK)"

                                                    _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(_FNEmpID) & " AND FTDateTrans='" & _Date & "'  "

                                                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") <> "" Then
                                                        _Qry = "  UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                                                        _Qry &= vbCrLf & " SET FNProNormal=" & _Amt & ""
                                                        _Qry &= vbCrLf & ", FNProOT=0 "
                                                        _Qry &= vbCrLf & ", FNProOther=" & _AmtOther & ""
                                                        _Qry &= vbCrLf & ", FNNetProAmt=" & _Amt + _AmtOther & " "
                                                        _Qry &= vbCrLf & ", FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                                        _Qry &= vbCrLf & ", FTUpdDate=" & HI.UL.ULDate.FormatDateDB & " "
                                                        _Qry &= vbCrLf & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & " "
                                                        _Qry &= vbCrLf & ", FTImpUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                                                        _Qry &= vbCrLf & ", FTImpDate=" & HI.UL.ULDate.FormatDateDB & " "
                                                        _Qry &= vbCrLf & ", FTImpTime=" & HI.UL.ULDate.FormatTimeDB & " "
                                                        _Qry &= vbCrLf & " WHERE  FNHSysEmpID=" & Val(_FNEmpID) & " AND FTDateTrans='" & _Date & "'  "
                                                    Else
                                                        _Qry = "  INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTWageDaily "
                                                        _Qry &= vbCrLf & " (FTInsUser, FTInsDate, FTInsTime, FTImpUser,FTImpDate,FTImpTime, FNHSysEmpID, FTDateTrans, FNAmtNormal, FNAmtOT, FNNetAmt, FNProNormal, FNProOT, FNProOther,FNNetProAmt) "
                                                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                                                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                                                        _Qry &= vbCrLf & " ," & Val(_FNEmpID) & ",'" & _Date & "'"
                                                        _Qry &= vbCrLf & " ,0"
                                                        _Qry &= vbCrLf & " ,0"
                                                        _Qry &= vbCrLf & " ,0"
                                                        _Qry &= vbCrLf & " ," & _Amt & ",0," & _AmtOther & "," & _Amt + _AmtOther & ""
                                                    End If

                                                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)
                                                End If
                                                Exit For
                                            Next

                                        End If

                                    Next

                                Catch ex As Exception
                                End Try
                                _dtemp.Dispose()
                                _Spls.Close()

                            End If

                        Catch ex As Exception
                            cn.Close()
                            _Spls.Close()
                            MsgBox("ไม่พบ  Sheet ชื่อ Employee  กรุณาทำการตรวจสอบชื่อ Sheet")
                        End Try


                    End If


                End If
            Catch ex As Exception
            End Try

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmsavedetail_Click(sender As Object, e As EventArgs) Handles ocmsavedetail.Click

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
End Class