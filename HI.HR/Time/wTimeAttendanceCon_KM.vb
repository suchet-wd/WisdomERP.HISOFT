Imports DevExpress.Data
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing

Public Class wTimeAttendanceCon_KM

    Private StateCal As Boolean = False
    Private _RowDataChange As Boolean
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

        'Call InitGrid()
    End Sub

#Region "Initial Grid"

    Private Sub InitGrid()

        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = "FTEmpCode"
        Dim sFieldSum As String = "FNLateNormalMin|FNLateNormalCut|FNAbsent|FNCutAbsent|FNAbsentCut"
        Dim sFieldGrpCount As String = "FTEmpCode"
        Dim sFieldGrpSum As String = "FNEmpWork|FNLateNormalMin|FNLateNormalCut|FNAbsent|FNCutAbsent|FNAbsentCut"

        'T.FNLateNormalMin, T.FNLateNormalCut
        Dim sFieldCustomSum As String = "FNTime|FNOTRequest|FNOT1|FNOT1_5|FNOT2|FNOT3|FNOT4"
        Dim sFieldCustomGrpSum As String = ""

        With ogvtime
            .ClearGrouping()
            .ClearDocument()
            .Columns("FTDateTransShow").Group()

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

    Private Sub ogv_CustomDrawGroupRow(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs) Handles ogvtime.CustomDrawGroupRow

        'Dim count As Integer = ogvtime.GetGroupSummaryValue(e.RowHandle, TryCast(ogvtime.GroupSummary(0), DevExpress.XtraGrid.GridGroupSummaryItem))
        'Dim completed As Integer = ogvtime.GetGroupSummaryValue(e.RowHandle, TryCast(ogvtime.GroupSummary(1), DevExpress.XtraGrid.GridGroupSummaryItem))
        'Dim Count As Integer = 0

        'For Each ObjGrp As DevExpress.XtraGrid.GridGroupSummaryItem In ogvtime.GroupSummary

        '    If Count = 0 Then

        '        e.Appearance.BackColor = Color.Green
        '    Else
        '        e.Appearance.BackColor = Color.Yellow
        '    End If

        '    If Count = 0 Then
        '        Count = 1
        '    Else
        '        Count = 0
        '    End If
        'Next

        '    Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo = e.Info
        '    Dim Handle As Integer = ogvtime.GetDataRowHandleByGroupRowHandle(e.RowHandle)

        '    Dim GrpDisplayText As String = ogvtime.GetGroupSummaryText(e.RowHandle)
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
        '    End If

        '    info.GroupText = info.Column.Caption + ":" + info.GroupValueText + ""
        '    info.GroupText += "" + GrpDisplayText + ""

    End Sub

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

    Private Sub Cancel_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepFTScanAIn.EditValueChanging _
              , RepFTScanAOTIn.EditValueChanging, RepFTScanAOTIn2.EditValueChanging, RepFTScanAOTOut.EditValueChanging _
              , RepFTScanAOTOut2.EditValueChanging, RepFTScanAOut.EditValueChanging, RepFTScanMIn.EditValueChanging _
              , RepFTTranStaCode.EditValueChanging, ReplFTScanMOut.EditValueChanging

        Try
            With Me.ogvtime

                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                If Not (ocmsavedetail.Enabled) Then
                    e.Cancel = True
                Else
                    If HI.HRCAL.Time.CheckClosePeriod(HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTDateTrans").ToString), Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID").ToString)) = True Then
                        HI.MG.ShowMsg.mInfo("ไม่สามารถทำการแก้ไขข้อมูลหลังการปิดงวดได้ กรุณาทำการตรวจสอบ", 14040001, Me.Text)
                        e.Cancel = True
                    End If
                End If

            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LoadData(Optional StateTimeError As Boolean = False)

        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

        Call BindShiftCodeToCombox()
        dtTimeColor = HI.HRCAL.Time.LoadTimeColor()
        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        _Qry = " SELECT T.FNHSysEmpID, M.FTEmpCode"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & " , PR.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

        Else
            _Qry &= vbCrLf & " , PR.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
        End If

        _Qry &= vbCrLf & " ,CASE WHEN  ISDATE(T.FTDateTrans) = 1 THEN Convert(varchar(10),Convert(Datetime,T.FTDateTrans),103) ELSE '' END As FTDateTrans"
        _Qry &= vbCrLf & " ,CASE WHEN  ISDATE(T.FTDateTrans) = 1 THEN Convert(Datetime,T.FTDateTrans) ELSE NULL END As FTDateTransShow"
        _Qry &= vbCrLf & "  , T.FNHSysShiftIDOrg, T.FNHSysShiftID, SH.FTShiftCode"
        _Qry &= vbCrLf & "  ,SH.FTShiftCode AS FTShiftCodeOrg"
        _Qry &= vbCrLf & "  , SH.FTIn1 AS FTCheckIn1"
        _Qry &= vbCrLf & "  , SH.FTOut1 As FTCheckOut1"
        _Qry &= vbCrLf & "  , SH.FTIn2 As FTCheckIn2"
        _Qry &= vbCrLf & "  , SH.FTOut2 AS FTCheckOut2"

        _Qry &= vbCrLf & "  , OT.FTOtMIn AS FTCheckOTMIn"
        _Qry &= vbCrLf & "  , OT.FTOtMOut AS FTCheckOTMOut"

        _Qry &= vbCrLf & "  , OT.FTOtIn AS FTCheckOTAIn1"
        _Qry &= vbCrLf & "  , OT.FTOtOut AS FTCheckOTAOut1"
        _Qry &= vbCrLf & "  , OT.FTOtIn3 AS FTCheckOTAIn2"
        _Qry &= vbCrLf & "  , OT.FTOtOut3 AS FTCheckOTAOut2,  SH.FTIn1 , "
        _Qry &= vbCrLf & "    T.FTOut1 , T.FTIn2 , T.FTOut2 , T.FTIn3, T.FTOut3, T.FTIn4, T.FTOut4"
        _Qry &= vbCrLf & " , FTScanMIn, FTScanMOut, FTScanAIn, FTScanAOut, FTScanAOTIn, FTScanAOTOut, FTScanAOTIn2,FTScanAOTOut2,FTScanOtMInM,FTScanOtMOutM,FTScanOtMIn,FTScanOtMOut"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNTime),'.',':') AS FNTime"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT1),'.',':') AS FNOT1"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT1_5),'.',':') AS FNOT1_5"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT2),'.',':') AS FNOT2"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT3),'.',':') AS FNOT3"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT4),'.',':') AS FNOT4"
        _Qry &= vbCrLf & ",  T.FNLateNormalMin, T.FNLateNormalCut, T.FNAbsentCut, T.FNAbsent, T.FNCutAbsent, T.FNHSysEmpID"
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
        '_Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN (FTScanMIn='' OR FTScanAOut='')  THEN '1' Else '0' END"
        '_Qry &= vbCrLf & "        ELSE  CASE  WHEN (FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')     THEN '1' Else '0' END END  AS FTStateError"

        'befor edit by JOKER
        '_Qry &= vbCrLf & " ,CASE WHEN  T.FNScanCtrl = 0 THEN CASE WHEN (FTScanMIn='' OR (FTScanAOut='' AND FTScanAOTOut ='')) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2  THEN '1' Else '0' END"
        '_Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN ((FTScanMIn='' OR FTScanAOut='') OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>''))) ) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2   THEN '1' Else '0' END"
        '_Qry &= vbCrLf & "        ELSE  CASE  WHEN ((FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')  OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>'')))) AND ISNULL(FNTotalLeaveMin,0)<480    AND M.FNUseBarcode<>2    THEN '1' Else '0' END END AS FTStateError "
        'end befor edit

        'new edit by joker 2017/06/06 09.43
        _Qry &= vbCrLf & " ,CASE WHEN  T.FNScanCtrl = 0 THEN CASE WHEN (FTScanMIn='' OR (FTScanAOut='' AND FTScanAOTOut ='')) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2  THEN '1' Else '0' END"
        _Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN ((FTScanMIn='' OR FTScanAOut='') OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>''))) ) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2   THEN '1' Else '0' END"
        _Qry &= vbCrLf & "        ELSE  CASE  WHEN ((FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')  OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>'')))) "
        _Qry &= vbCrLf & "And     ISNULL(FNTotalLeaveMin,0)<480     And M.FNUseBarcode<>2    THEN   "
        _Qry &= vbCrLf & "  Case when  ((isnull(SPD.FTTimeOut,'') <= FTScanMOut  ) OR FTScanMIn='' OR FTScanMOut='' OR FTScanAOut = '' OR FTScanAIN = '') and isnull(FNTotalLeaveMin,0)=0 then '1' "
        _Qry &= vbCrLf & "     when  ((isnull(SPD.FTTimeOut,'') <= FTScanAOut  ) OR FTScanMIn<>'' OR FTScanMOut<>'' OR FTScanAOut <> '' OR FTScanAIN <>'') and isnull(FNTotalLeaveMin,0)>0 then '0' "
        _Qry &= vbCrLf & "else   '1' end  else '0' END END AS FTStateError"
        'end new edit

        _Qry &= vbCrLf & " ,ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
        _Qry &= vbCrLf & " ,ISNULL(D.FTDeptCode,'') AS FTDeptCode "
        _Qry &= vbCrLf & " ,ISNULL(Di.FTDivisonCode,'') AS FTDivisonCode "
        _Qry &= vbCrLf & " ,ISNULL(S.FTSectCode,'') AS FTSectCode "
        _Qry &= vbCrLf & " ,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode "
        _Qry &= vbCrLf & ",isnull(P.FTPositCode,'')  AS FTPositCode"
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
        _Qry &= vbCrLf & ", ISNULL(SPD.FTDate,'') AS SPD"
        _Qry &= vbCrLf & " ,  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_LoadTimeLeave(T.FNHSysEmpID,T.FTDateTrans,'" & HI.ST.Lang.Language.ToString & "') AS FTLeave"
        _Qry &= vbCrLf & ", ISNULL(FTScanMInM,'') as FTScanMInM"
        _Qry &= vbCrLf & ", ISNULL(FTScanMOutM,'') AS FTScanMOutM"
        _Qry &= vbCrLf & ", ISNULL(FTScanAInM,'') AS FTScanAInM"
        _Qry &= vbCrLf & ", ISNULL(FTScanAOutM,'') AS FTScanAOutM"
        _Qry &= vbCrLf & ", ISNULL(FTScanAOTInM,'') AS FTScanAOTInM"
        _Qry &= vbCrLf & ", ISNULL(FTScanAOTOutM,'') AS FTScanAOTOutM"
        _Qry &= vbCrLf & ", CASE WHEN  ISDATE(ISNULL(M.FDDateEnd,'')) = 1 THEN Convert(varchar(10),Convert(Datetime,ISNULL(M.FDDateEnd,'')),103) ELSE '' END  AS FDDateEnd"
        _Qry &= vbCrLf & ",M.FNUseBarcode"
        _Qry &= vbCrLf & ",M.FTEmpCodeRefer"
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS SH WITH (NOLOCK) ON T.FNHSysShiftID = SH.FNHSysShiftID LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest AS OT WITH (NOLOCK) ON T.FNHSysEmpID = OT.FNHSysEmpID AND T.FTDateTrans = OT.FTDateRequest"
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMTranStatus AS TM ON T.FNHSysTranStaId = TM.FNHSysTranStaId "
        _Qry &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK)  ON  T.FNHSysEmpID =  M.FNHSysEmpID LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId = P.FNHSysPositId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
        _Qry &= vbCrLf & "    INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS PR WITH (NOLOCK) ON M.FNHSysPreNameId = PR.FNHSysPreNameId"
        _Qry &= vbCrLf & "    OUTER APPLY ( SELECT TOP 1  FNHSysHolidayId, FDHolidayDate, FTHolidayNameTH, FTHolidayNameEN, FNHSysCmpId FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday WITH (NOLOCK) WHERE ISNULL( FTStateActive,'') ='1'  AND FNHSysCmpId = M.FNHSysCmpId AND T.FTDateTrans = FDHolidayDate) AS  HD "
        _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailySpecial AS SPD WITH (NOLOCK) ON T.FTDateTrans = SPD.FTDate AND T.FNHSysEmpID=SPD.FNHSysEmpID"


        _Qry &= vbCrLf & "    LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  (SELECT        FNHSysEmpID, FTDateTrans, SUM(FNTotalMinute) AS FNTotalLeaveMin"
        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave AS TL WITH(NOLOCK)"
        _Qry &= vbCrLf & "  GROUP BY FNHSysEmpID, FTDateTrans ) AS TL"
        _Qry &= vbCrLf & "  ON T.FNHSysEmpID = TL.FNHSysEmpID AND T.FTDateTrans=TL.FTDateTrans"

        _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly AS EHL WITH (NOLOCK) ON T.FNHSysEmpID=EHL.FNHSysEmpID"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmpTypeWeeklySpecial AS ETHL WITH (NOLOCK) ON T.FTDateTrans = ETHL.FDHolidayDate AND M.FNHSysEmpTypeId=ETHL.FNHSysEmpTypeId"

        _Qry &= vbCrLf & "  WHERE  M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & "  AND T.FTDateTrans>='" & HI.UL.ULDate.ConvertEnDB(FDStartDate.Text) & "' AND T.FTDateTrans <='" & HI.UL.ULDate.ConvertEnDB(FDEndDate.Text) & "' "
        _Qry &= vbCrLf & "  AND  (T.FTDateTrans <=  CASE WHEN ISNULL(M.FDDateEnd,'') ='' THEN '9999/99/99' ELSE  ISNULL(M.FDDateEnd,'')  END)   "


        If HI.ST.SysInfo.HideSunday Then
            _Qry &= vbCrLf & " AND ISNULL(T.FTWeekDay,'') <>'1' "
        End If

        If (StateTimeError) Then
            '_Qry &= vbCrLf & " AND ( CASE WHEN  T.FNScanCtrl = 0 THEN CASE WHEN (FTScanMIn='' OR (FTScanAOut='' AND FTScanAOTOut =''))   THEN '1' Else '0' END"
            '_Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN (FTScanMIn='' OR FTScanAOut='') OR (OT.FTOtIn  <>'' AND (FTScanAOTIn='' OR FTScanAOTOut=''))  THEN '1' Else '0' END "
            '_Qry &= vbCrLf & "        ELSE  CASE  WHEN (FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')   OR (OT.FTOtIn  <>'' AND (FTScanAOTIn='' OR FTScanAOTOut=''))  THEN '1' Else '0' END END =1 ) "

            'befor edit by joker
            '_Qry &= vbCrLf & " AND CASE WHEN  T.FNScanCtrl = 0 THEN CASE WHEN (FTScanMIn='' OR (FTScanAOut='' AND FTScanAOTOut ='')) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2   THEN '1' Else '0' END"
            '_Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN ((FTScanMIn='' OR FTScanAOut='') OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>''))) ) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2    THEN '1' Else '0' END"
            '_Qry &= vbCrLf & "        ELSE  CASE  WHEN ((FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')  OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>'')))) AND ISNULL(FNTotalLeaveMin,0)<480    AND M.FNUseBarcode<>2    THEN '1' Else '0' END END  ='1' "
            'end befor edit

            'new edit by joker 201/06/06 09:48
            _Qry &= vbCrLf & " AND CASE WHEN  T.FNScanCtrl = 0 THEN CASE WHEN (FTScanMIn='' OR (FTScanAOut='' AND FTScanAOTOut ='')) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2  THEN '1' Else '0' END"
            _Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN ((FTScanMIn='' OR FTScanAOut='') OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>''))) ) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2   THEN '1' Else '0' END"
            _Qry &= vbCrLf & "        ELSE  CASE  WHEN ((FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')  OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>'')))) "
            _Qry &= vbCrLf & "And     ISNULL(FNTotalLeaveMin,0)<480     And M.FNUseBarcode<>2    THEN   "
            _Qry &= vbCrLf & "  Case when  ((isnull(SPD.FTTimeOut,'') <= FTScanMOut  ) OR FTScanMIn='' OR FTScanMOut='' OR FTScanAOut = '' OR FTScanAIN = '') and isnull(FNTotalLeaveMin,0)=0 then '1' "
            _Qry &= vbCrLf & "     when  ((isnull(SPD.FTTimeOut,'') <= FTScanAOut  ) OR FTScanMIn<>'' OR FTScanMOut<>'' OR FTScanAOut <> '' OR FTScanAIN <>'') and isnull(FNTotalLeaveMin,0)>0 then '0' "
            _Qry &= vbCrLf & "else   '1' end  else '0' END END='1' "
            'end new edit


        End If

        _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)

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

        _Qry &= vbCrLf & " ORDER BY  T.FTDateTrans,M.FTEmpCode  "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.ogdtime.DataSource = _dt
        Me.ogvtime.BestFitColumns()
        ogvtime.ExpandAllGroups()
        _Spls.Close()

        _RowDataChange = False

    End Sub

    Private Sub LoadDataLate()
        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

        Call BindShiftCodeToCombox()
        dtTimeColor = HI.HRCAL.Time.LoadTimeColor()
        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        _Qry = " SELECT T.FNHSysEmpID, M.FTEmpCode"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & " , PR.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

        Else
            _Qry &= vbCrLf & " , PR.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
        End If

        _Qry &= vbCrLf & " ,CASE WHEN  ISDATE(T.FTDateTrans) = 1 THEN Convert(varchar(10),Convert(Datetime,T.FTDateTrans),103) ELSE '' END As FTDateTrans"
        _Qry &= vbCrLf & " ,CASE WHEN  ISDATE(T.FTDateTrans) = 1 THEN Convert(Datetime,T.FTDateTrans) ELSE NULL END As FTDateTransShow"
        _Qry &= vbCrLf & "  , T.FNHSysShiftIDOrg, T.FNHSysShiftID, SH.FTShiftCode"
        _Qry &= vbCrLf & "  ,SH.FTShiftCode AS FTShiftCodeOrg"
        _Qry &= vbCrLf & "  , SH.FTIn1 AS FTCheckIn1"
        _Qry &= vbCrLf & "  , SH.FTOut1 As FTCheckOut1"
        _Qry &= vbCrLf & "  , SH.FTIn2 As FTCheckIn2"
        _Qry &= vbCrLf & "  , SH.FTOut2 AS FTCheckOut2"

        _Qry &= vbCrLf & "  , OT.FTOtMIn AS FTCheckOTMIn"
        _Qry &= vbCrLf & "  , OT.FTOtMOut AS FTCheckOTMOut"

        _Qry &= vbCrLf & "  , OT.FTOtIn AS FTCheckOTAIn1"
        _Qry &= vbCrLf & "  , OT.FTOtOut AS FTCheckOTAOut1"
        _Qry &= vbCrLf & "  , OT.FTOtIn3 AS FTCheckOTAIn2"
        _Qry &= vbCrLf & "  , OT.FTOtOut3 AS FTCheckOTAOut2,  SH.FTIn1, "
        _Qry &= vbCrLf & "    T.FTOut1 , T.FTIn2 , T.FTOut2 , T.FTIn3, T.FTOut3, T.FTIn4, T.FTOut4"
        _Qry &= vbCrLf & " , FTScanMIn, FTScanMOut, FTScanAIn, FTScanAOut, FTScanAOTIn, FTScanAOTOut, FTScanAOTIn2,FTScanAOTOut2,FTScanOtMInM,FTScanOtMOutM,FTScanOtMIn,FTScanOtMOut"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNTime),'.',':') AS FNTime"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT1),'.',':') AS FNOT1"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT1_5),'.',':') AS FNOT1_5"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT2),'.',':') AS FNOT2"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT3),'.',':') AS FNOT3"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT4),'.',':') AS FNOT4"
        _Qry &= vbCrLf & ",  T.FNLateNormalMin, T.FNLateNormalCut, T.FNAbsentCut, T.FNAbsent, T.FNCutAbsent, T.FNHSysEmpID"
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
        '_Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN (FTScanMIn='' OR FTScanAOut='')  THEN '1' Else '0' END"
        '_Qry &= vbCrLf & "        ELSE  CASE  WHEN (FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')     THEN '1' Else '0' END END  AS FTStateError"

        'befor edit by JOKER
        '_Qry &= vbCrLf & " ,CASE WHEN  T.FNScanCtrl = 0 THEN CASE WHEN (FTScanMIn='' OR (FTScanAOut='' AND FTScanAOTOut ='')) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2  THEN '1' Else '0' END"
        '_Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN ((FTScanMIn='' OR FTScanAOut='') OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>''))) ) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2   THEN '1' Else '0' END"
        '_Qry &= vbCrLf & "        ELSE  CASE  WHEN ((FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')  OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>'')))) AND ISNULL(FNTotalLeaveMin,0)<480    AND M.FNUseBarcode<>2    THEN '1' Else '0' END END AS FTStateError "
        'end befor edit

        'new edit by joker 2017/06/06 09.43
        _Qry &= vbCrLf & " ,CASE WHEN  T.FNScanCtrl = 0 THEN CASE WHEN (FTScanMIn='' OR (FTScanAOut='' AND FTScanAOTOut ='')) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2  THEN '1' Else '0' END"
        _Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN ((FTScanMIn='' OR FTScanAOut='') OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>''))) ) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2   THEN '1' Else '0' END"
        _Qry &= vbCrLf & "        ELSE  CASE  WHEN ((FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')  OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>'')))) "
        _Qry &= vbCrLf & "And     ISNULL(FNTotalLeaveMin,0)<480     And M.FNUseBarcode<>2    THEN   "
        _Qry &= vbCrLf & "  Case when  ((isnull(SPD.FTTimeOut,'') <= FTScanMOut  ) OR FTScanMIn='' OR FTScanMOut='' OR FTScanAOut = '' OR FTScanAIN = '') and isnull(FNTotalLeaveMin,0)=0 then '1' "
        _Qry &= vbCrLf & "     when  ((isnull(SPD.FTTimeOut,'') <= FTScanAOut  ) OR FTScanMIn<>'' OR FTScanMOut<>'' OR FTScanAOut <> '' OR FTScanAIN <>'') and isnull(FNTotalLeaveMin,0)>0 then '0' "
        _Qry &= vbCrLf & "else   '1' end  else '0' END END AS FTStateError"
        'end new edit

        _Qry &= vbCrLf & " ,ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
        _Qry &= vbCrLf & " ,ISNULL(D.FTDeptCode,'') AS FTDeptCode "
        _Qry &= vbCrLf & " ,ISNULL(Di.FTDivisonCode,'') AS FTDivisonCode "
        _Qry &= vbCrLf & " ,ISNULL(S.FTSectCode,'') AS FTSectCode "
        _Qry &= vbCrLf & " ,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode "
        _Qry &= vbCrLf & ",isnull(P.FTPositCode,'')  AS FTPositCode"
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
        _Qry &= vbCrLf & ", ISNULL(SPD.FTDate,'') AS SPD"
        _Qry &= vbCrLf & " ,  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_LoadTimeLeave(T.FNHSysEmpID,T.FTDateTrans,'" & HI.ST.Lang.Language.ToString & "') AS FTLeave"
        _Qry &= vbCrLf & ", ISNULL(FTScanMInM,'') as FTScanMInM"
        _Qry &= vbCrLf & ", ISNULL(FTScanMOutM,'') AS FTScanMOutM"
        _Qry &= vbCrLf & ", ISNULL(FTScanAInM,'') AS FTScanAInM"
        _Qry &= vbCrLf & ", ISNULL(FTScanAOutM,'') AS FTScanAOutM"
        _Qry &= vbCrLf & ", ISNULL(FTScanAOTInM,'') AS FTScanAOTInM"
        _Qry &= vbCrLf & ", ISNULL(FTScanAOTOutM,'') AS FTScanAOTOutM"
        _Qry &= vbCrLf & ", CASE WHEN  ISDATE(ISNULL(M.FDDateEnd,'')) = 1 THEN Convert(varchar(10),Convert(Datetime,ISNULL(M.FDDateEnd,'')),103) ELSE '' END  AS FDDateEnd"
        _Qry &= vbCrLf & ",M.FNUseBarcode"
        _Qry &= vbCrLf & ",M.FTEmpCodeRefer"
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS SH WITH (NOLOCK) ON T.FNHSysShiftID = SH.FNHSysShiftID LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest AS OT WITH (NOLOCK) ON T.FNHSysEmpID = OT.FNHSysEmpID AND T.FTDateTrans = OT.FTDateRequest"
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMTranStatus AS TM ON T.FNHSysTranStaId = TM.FNHSysTranStaId "
        _Qry &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK)  ON  T.FNHSysEmpID =  M.FNHSysEmpID LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId = P.FNHSysPositId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
        _Qry &= vbCrLf & "    INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS PR WITH (NOLOCK) ON M.FNHSysPreNameId = PR.FNHSysPreNameId"
        _Qry &= vbCrLf & "    OUTER APPLY ( SELECT TOP 1  FNHSysHolidayId, FDHolidayDate, FTHolidayNameTH, FTHolidayNameEN, FNHSysCmpId FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday WITH (NOLOCK) WHERE ISNULL( FTStateActive,'') ='1'  AND FNHSysCmpId = M.FNHSysCmpId AND T.FTDateTrans = FDHolidayDate) AS  HD "
        _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailySpecial AS SPD WITH (NOLOCK) ON T.FTDateTrans = SPD.FTDate AND T.FNHSysEmpID=SPD.FNHSysEmpID"


        _Qry &= vbCrLf & "    LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  (SELECT        FNHSysEmpID, FTDateTrans, SUM(FNTotalMinute) AS FNTotalLeaveMin"
        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave AS TL WITH(NOLOCK)"
        _Qry &= vbCrLf & "  GROUP BY FNHSysEmpID, FTDateTrans ) AS TL"
        _Qry &= vbCrLf & "  ON T.FNHSysEmpID = TL.FNHSysEmpID AND T.FTDateTrans=TL.FTDateTrans"

        _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly AS EHL WITH (NOLOCK) ON T.FNHSysEmpID=EHL.FNHSysEmpID"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmpTypeWeeklySpecial AS ETHL WITH (NOLOCK) ON T.FTDateTrans = ETHL.FDHolidayDate AND M.FNHSysEmpTypeId=ETHL.FNHSysEmpTypeId"


        _Qry &= vbCrLf & "  WHERE  M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & "  AND T.FTDateTrans>='" & HI.UL.ULDate.ConvertEnDB(FDStartDate.Text) & "' AND T.FTDateTrans <='" & HI.UL.ULDate.ConvertEnDB(FDEndDate.Text) & "' "
        _Qry &= vbCrLf & "  AND  (T.FTDateTrans <=  CASE WHEN ISNULL(M.FDDateEnd,'') ='' THEN '9999/99/99' ELSE  ISNULL(M.FDDateEnd,'')  END)   "


        If HI.ST.SysInfo.HideSunday Then
            _Qry &= vbCrLf & " AND ISNULL(T.FTWeekDay,'') <>'1' "
        End If



        _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)
        _Qry &= vbCrLf & "AND T.FNLateNormalMin>1"
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

        _Qry &= vbCrLf & " ORDER BY  T.FTDateTrans,M.FTEmpCode  "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.ogdtime.DataSource = _dt
        Me.ogvtime.BestFitColumns()
        ogvtime.ExpandAllGroups()
        _Spls.Close()

        _RowDataChange = False
    End Sub

    Private Sub LoadDataWaitLeave()
        Dim _StartDate As String = ""
        Dim _EndDate As String = ""
        Dim _Qry As String = ""
        Dim _dt As DataTable

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

        Call BindShiftCodeToCombox()
        dtTimeColor = HI.HRCAL.Time.LoadTimeColor()
        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

        _Qry = " SELECT T.FNHSysEmpID, M.FTEmpCode"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & " , PR.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

        Else
            _Qry &= vbCrLf & " , PR.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
        End If

        _Qry &= vbCrLf & " ,CASE WHEN  ISDATE(T.FTDateTrans) = 1 THEN Convert(varchar(10),Convert(Datetime,T.FTDateTrans),103) ELSE '' END As FTDateTrans"
        _Qry &= vbCrLf & " ,CASE WHEN  ISDATE(T.FTDateTrans) = 1 THEN Convert(Datetime,T.FTDateTrans) ELSE NULL END As FTDateTransShow"
        _Qry &= vbCrLf & "  , T.FNHSysShiftIDOrg, T.FNHSysShiftID, SH.FTShiftCode"
        _Qry &= vbCrLf & "  ,SH.FTShiftCode AS FTShiftCodeOrg"
        _Qry &= vbCrLf & "  , SH.FTIn1 AS FTCheckIn1"
        _Qry &= vbCrLf & "  , SH.FTOut1 As FTCheckOut1"
        _Qry &= vbCrLf & "  , SH.FTIn2 As FTCheckIn2"
        _Qry &= vbCrLf & "  , SH.FTOut2 AS FTCheckOut2"

        _Qry &= vbCrLf & "  , OT.FTOtMIn AS FTCheckOTMIn"
        _Qry &= vbCrLf & "  , OT.FTOtMOut AS FTCheckOTMOut"

        _Qry &= vbCrLf & "  , OT.FTOtIn AS FTCheckOTAIn1"
        _Qry &= vbCrLf & "  , OT.FTOtOut AS FTCheckOTAOut1"
        _Qry &= vbCrLf & "  , OT.FTOtIn3 AS FTCheckOTAIn2"
        _Qry &= vbCrLf & "  , OT.FTOtOut3 AS FTCheckOTAOut2,  SH.FTIn1 , "
        _Qry &= vbCrLf & "    T.FTOut1 , T.FTIn2 , T.FTOut2 , T.FTIn3, T.FTOut3, T.FTIn4, T.FTOut4"
        _Qry &= vbCrLf & " , FTScanMIn, FTScanMOut, FTScanAIn, FTScanAOut, FTScanAOTIn, FTScanAOTOut, FTScanAOTIn2,FTScanAOTOut2,FTScanOtMInM,FTScanOtMOutM,FTScanOtMIn,FTScanOtMOut"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNTime),'.',':') AS FNTime"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT1),'.',':') AS FNOT1"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT1_5),'.',':') AS FNOT1_5"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT2),'.',':') AS FNOT2"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT3),'.',':') AS FNOT3"
        _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT4),'.',':') AS FNOT4"
        _Qry &= vbCrLf & ",  T.FNLateNormalMin, T.FNLateNormalCut, T.FNAbsentCut, T.FNAbsent, T.FNCutAbsent, T.FNHSysEmpID"
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
        '_Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN (FTScanMIn='' OR FTScanAOut='')  THEN '1' Else '0' END"
        '_Qry &= vbCrLf & "        ELSE  CASE  WHEN (FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')     THEN '1' Else '0' END END  AS FTStateError"

        'befor edit by JOKER
        '_Qry &= vbCrLf & " ,CASE WHEN  T.FNScanCtrl = 0 THEN CASE WHEN (FTScanMIn='' OR (FTScanAOut='' AND FTScanAOTOut ='')) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2  THEN '1' Else '0' END"
        '_Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN ((FTScanMIn='' OR FTScanAOut='') OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>''))) ) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2   THEN '1' Else '0' END"
        '_Qry &= vbCrLf & "        ELSE  CASE  WHEN ((FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')  OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>'')))) AND ISNULL(FNTotalLeaveMin,0)<480    AND M.FNUseBarcode<>2    THEN '1' Else '0' END END AS FTStateError "
        'end befor edit

        'new edit by joker 2017/06/06 09.43
        _Qry &= vbCrLf & " ,CASE WHEN  T.FNScanCtrl = 0 THEN CASE WHEN (FTScanMIn='' OR (FTScanAOut='' AND FTScanAOTOut ='')) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2  THEN '1' Else '0' END"
        _Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN ((FTScanMIn='' OR FTScanAOut='') OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>''))) ) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2   THEN '1' Else '0' END"
        _Qry &= vbCrLf & "        ELSE  CASE  WHEN ((FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')  OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>'')))) "
        _Qry &= vbCrLf & "And     ISNULL(FNTotalLeaveMin,0)<480     And M.FNUseBarcode<>2    THEN   "
        _Qry &= vbCrLf & "  Case when  ((isnull(SPD.FTTimeOut,'') <= FTScanMOut  ) OR FTScanMIn='' OR FTScanMOut='' OR FTScanAOut = '' OR FTScanAIN = '') and isnull(FNTotalLeaveMin,0)=0 then '1' "
        _Qry &= vbCrLf & "     when  ((isnull(SPD.FTTimeOut,'') <= FTScanAOut  ) OR FTScanMIn<>'' OR FTScanMOut<>'' OR FTScanAOut <> '' OR FTScanAIN <>'') and isnull(FNTotalLeaveMin,0)>0 then '0' "
        _Qry &= vbCrLf & "else   '1' end  else '0' END END AS FTStateError"
        'end new edit

        _Qry &= vbCrLf & " ,ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
        _Qry &= vbCrLf & " ,ISNULL(D.FTDeptCode,'') AS FTDeptCode "
        _Qry &= vbCrLf & " ,ISNULL(Di.FTDivisonCode,'') AS FTDivisonCode "
        _Qry &= vbCrLf & " ,ISNULL(S.FTSectCode,'') AS FTSectCode "
        _Qry &= vbCrLf & " ,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode "
        _Qry &= vbCrLf & ",isnull(P.FTPositCode,'')  AS FTPositCode"
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
        _Qry &= vbCrLf & ", ISNULL(SPD.FTDate,'') AS SPD"
        _Qry &= vbCrLf & " ,  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_LoadTimeLeave(T.FNHSysEmpID,T.FTDateTrans,'" & HI.ST.Lang.Language.ToString & "') AS FTLeave"
        _Qry &= vbCrLf & ", ISNULL(FTScanMInM,'') as FTScanMInM"
        _Qry &= vbCrLf & ", ISNULL(FTScanMOutM,'') AS FTScanMOutM"
        _Qry &= vbCrLf & ", ISNULL(FTScanAInM,'') AS FTScanAInM"
        _Qry &= vbCrLf & ", ISNULL(FTScanAOutM,'') AS FTScanAOutM"
        _Qry &= vbCrLf & ", ISNULL(FTScanAOTInM,'') AS FTScanAOTInM"
        _Qry &= vbCrLf & ", ISNULL(FTScanAOTOutM,'') AS FTScanAOTOutM"
        _Qry &= vbCrLf & ", CASE WHEN  ISDATE(ISNULL(M.FDDateEnd,'')) = 1 THEN Convert(varchar(10),Convert(Datetime,ISNULL(M.FDDateEnd,'')),103) ELSE '' END  AS FDDateEnd"
        _Qry &= vbCrLf & ",M.FNUseBarcode"
        _Qry &= vbCrLf & ",M.FTEmpCodeRefer"
        _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily AS Adv WITH(NOLOCK) LEFT OUtER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK) ON Adv.FNHSysEmpID=T.FNHSysEmpID and adv.FTStartDate=T.FTDateTrans LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS SH WITH (NOLOCK) ON T.FNHSysShiftID = SH.FNHSysShiftID LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest AS OT WITH (NOLOCK) ON T.FNHSysEmpID = OT.FNHSysEmpID AND T.FTDateTrans = OT.FTDateRequest"
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMTranStatus AS TM ON T.FNHSysTranStaId = TM.FNHSysTranStaId "
        _Qry &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK)  ON  T.FNHSysEmpID =  M.FNHSysEmpID LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId = P.FNHSysPositId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
        _Qry &= vbCrLf & "    INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS PR WITH (NOLOCK) ON M.FNHSysPreNameId = PR.FNHSysPreNameId"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN  ( SELECT  FNHSysHolidayId, FDHolidayDate, FTHolidayNameTH, FTHolidayNameEN FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday WITH (NOLOCK) WHERE ISNULL( FTStateActive,'') ='1' ) AS  HD  ON T.FTDateTrans = HD.FDHolidayDate"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailySpecial AS SPD WITH (NOLOCK) ON T.FTDateTrans = SPD.FTDate AND T.FNHSysEmpID=SPD.FNHSysEmpID"


        _Qry &= vbCrLf & "    LEFT OUTER JOIN "
        _Qry &= vbCrLf & "  (SELECT        FNHSysEmpID, FTDateTrans, SUM(FNTotalMinute) AS FNTotalLeaveMin "
        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave AS TL WITH(NOLOCK)"
        _Qry &= vbCrLf & "  GROUP BY FNHSysEmpID, FTDateTrans ) AS TL "
        _Qry &= vbCrLf & "  ON T.FNHSysEmpID = TL.FNHSysEmpID AND T.FTDateTrans=TL.FTDateTrans "

        _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly AS EHL WITH (NOLOCK) ON T.FNHSysEmpID=EHL.FNHSysEmpID"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmpTypeWeeklySpecial AS ETHL WITH (NOLOCK) ON T.FTDateTrans = ETHL.FDHolidayDate AND M.FNHSysEmpTypeId=ETHL.FNHSysEmpTypeId"


        _Qry &= vbCrLf & "  WHERE  M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & "  AND T.FTDateTrans>='" & HI.UL.ULDate.ConvertEnDB(FDStartDate.Text) & "' AND T.FTDateTrans <='" & HI.UL.ULDate.ConvertEnDB(FDEndDate.Text) & "' "
        _Qry &= vbCrLf & "  AND  (T.FTDateTrans <=  CASE WHEN ISNULL(M.FDDateEnd,'') ='' THEN '9999/99/99' ELSE  ISNULL(M.FDDateEnd,'')  END)   "


        If HI.ST.SysInfo.HideSunday Then
            _Qry &= vbCrLf & " AND ISNULL(T.FTWeekDay,'') <>'1' "
        End If

        _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)
        _Qry &= vbCrLf & " AND (Adv.FTApproveState='0' or Adv.FTApproveState ='' or Adv.FTApproveState is null)"
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

        _Qry &= vbCrLf & " ORDER BY  T.FTDateTrans,M.FTEmpCode  "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.ogdtime.DataSource = _dt
        Me.ogvtime.BestFitColumns()
        ogvtime.ExpandAllGroups()
        _Spls.Close()

        _RowDataChange = False
    End Sub

    Private Sub SaveGrid(RowHandleIdx As Integer)
        Try
            If Not (StateCal) Then

                With ogvtime

                    If RowHandleIdx < 0 Or RowHandleIdx > .RowCount - 1 Then
                        _RowDataChange = False
                        Exit Sub
                    End If

                    If Not (ocmsavedetail.Enabled) Then
                        _RowDataChange = False
                        Exit Sub
                    End If

                    If Not (_RowDataChange) Then Exit Sub
                    StateCal = True

                    Dim _Qry As String = ""

                    _Qry = " SELECT TOP 1  FTDateTrans  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans   "
                    _Qry &= vbCrLf & " WHERE FTDateTrans='" & HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(RowHandleIdx, "FTDateTrans").ToString) & "'  "
                    _Qry &= vbCrLf & " AND FNHSysEmpID=" & Val("" & .GetRowCellValue(RowHandleIdx, "FNHSysEmpID").ToString) & ""

                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "") = "" Then
                        _Qry = " EXEC SP_INSERT_TIME '" & HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(RowHandleIdx, "FTDateTrans").ToString) & "'," & Val("" & .GetRowCellValue(RowHandleIdx, "FNHSysEmpID").ToString) & ""
                        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)
                    End If

                    Dim _CheckTimeMIn As String = ""
                    Dim _CheckTimeMOut As String = ""
                    Dim _CheckTimeAIn As String = ""
                    Dim _CheckTimeAOut As String = ""
                    Dim _CheckTimeOTIn1 As String = ""
                    Dim _CheckTimeOTOut1 As String = ""
                    Dim _CheckTimeOTIn2 As String = ""
                    Dim _CheckTimeOTOut2 As String = ""
                    Dim _ActualTimMIn As String, _ActualTimMOut As String
                    Dim _ActualTimAIn As String, _ActualTimAOut As String
                    Dim _ActualTimOTIn1 As String, _ActualTimOTOut1 As String, _ScanCardOverClock As String
                    Dim _ActualTimOTIn2 As String, _ActualTimOTOut2 As String

                    Dim _TmpScanIn As String, _TmpScanOut As String
                    Dim FTScanMIn As String, FTScanMOut As String
                    Dim FTScanAIn As String, FTScanAOut As String
                    Dim FTScanAOTIn As String, FTScanAOTOut As String
                    Dim FTScanAOTIn2 As String, FTScanAOTOut2 As String

                    Dim FTScanMInM As String, FTScanMOutM As String
                    Dim FTScanAInM As String, FTScanAOutM As String
                    Dim FTScanAOTInM As String, FTScanAOTOutM As String
                    Dim FTScanOTMInM, FTScanOTMOutM, _ActualScanOTInM, _ActualScanOTOutM, _FTOtMIn, _FTOtMOut As String
                    Dim _FTShiftId As String = ""
                    FTScanOTMInM = "" : FTScanOTMOutM = "" : _ActualScanOTInM = "" : _ActualScanOTOutM = "" : _FTOtMIn = "" : _FTOtMOut = ""

                    FTScanOTMInM = "" & .GetRowCellValue(RowHandleIdx, "FTScanOtMIn").ToString
                    FTScanOTMOutM = "" & .GetRowCellValue(RowHandleIdx, "FTScanOtMOut").ToString
                    _FTOtMIn = "" & .GetRowCellValue(RowHandleIdx, "FTCheckOTMIn").ToString
                    _FTOtMOut = "" & .GetRowCellValue(RowHandleIdx, "FTCheckOTMOut").ToString
                    _FTShiftId = "" & .GetRowCellValue(RowHandleIdx, "FTShiftCode").ToString
                    '_ActualScanOTInM = "" & .GetRowCellValue(RowHandleIdx, "FTScanOtMIn").ToString
                    '_ActualScanOTOutM = "" & .GetRowCellValue(RowHandleIdx, "FTScanOtMOut").ToString

                    _ScanCardOverClock = "" & .GetRowCellValue(RowHandleIdx, "FTOverClock").ToString
                    _CheckTimeMIn = "" & .GetRowCellValue(RowHandleIdx, "FTCheckIn1").ToString
                    _CheckTimeMOut = "" & .GetRowCellValue(RowHandleIdx, "FTCheckOut1").ToString
                    _CheckTimeAIn = "" & .GetRowCellValue(RowHandleIdx, "FTCheckIn2").ToString
                    _CheckTimeAOut = "" & .GetRowCellValue(RowHandleIdx, "FTCheckOut2").ToString
                    _CheckTimeOTIn1 = "" & .GetRowCellValue(RowHandleIdx, "FTCheckOTAIn1").ToString
                    _CheckTimeOTOut1 = "" & .GetRowCellValue(RowHandleIdx, "FTCheckOTAOut1").ToString

                    _CheckTimeOTIn2 = "" & .GetRowCellValue(RowHandleIdx, "FTCheckOTAIn2").ToString
                    _CheckTimeOTOut2 = "" & .GetRowCellValue(RowHandleIdx, "FTCheckOTAOut2").ToString

                    FTScanMIn = "" & .GetRowCellValue(RowHandleIdx, "FTScanMIn").ToString
                    FTScanMOut = "" & .GetRowCellValue(RowHandleIdx, "FTScanMOut").ToString
                    FTScanAIn = "" & .GetRowCellValue(RowHandleIdx, "FTScanAIn").ToString
                    FTScanAOut = "" & .GetRowCellValue(RowHandleIdx, "FTScanAOut").ToString
                    FTScanAOTIn = "" & .GetRowCellValue(RowHandleIdx, "FTScanAOTIn").ToString
                    FTScanAOTOut = "" & .GetRowCellValue(RowHandleIdx, "FTScanAOTOut").ToString
                    FTScanAOTIn2 = "" & .GetRowCellValue(RowHandleIdx, "FTScanAOTIn2").ToString
                    FTScanAOTOut2 = "" & .GetRowCellValue(RowHandleIdx, "FTScanAOTOut2").ToString

                    FTScanMInM = "" & .GetRowCellValue(RowHandleIdx, "FTScanMInM").ToString
                    FTScanMOutM = "" & .GetRowCellValue(RowHandleIdx, "FTScanMOutM").ToString
                    FTScanAInM = "" & .GetRowCellValue(RowHandleIdx, "FTScanAInM").ToString
                    FTScanAOutM = "" & .GetRowCellValue(RowHandleIdx, "FTScanAOutM").ToString
                    FTScanAOTInM = "" & .GetRowCellValue(RowHandleIdx, "FTScanAOTInM").ToString
                    FTScanAOTOutM = "" & .GetRowCellValue(RowHandleIdx, "FTScanAOTOutM").ToString

                    _ActualTimMIn = "" : _ActualTimMOut = ""
                    _ActualTimAIn = "" : _ActualTimAOut = "" : _ActualTimOTIn1 = "" : _ActualTimOTOut1 = ""
                    _ActualTimOTIn2 = "" : _ActualTimOTOut2 = ""
                    _TmpScanIn = "" : _TmpScanOut = ""

                    Dim _ScanCardCtl As Integer = HI.Conn.SQLConn.GetField("SELECT TOP 1 FNScanCtrl FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WITH(NOLOCK) WHERE FNHSysEmpID=" & Val("" & .GetRowCellValue(RowHandleIdx, "FNHSysEmpID").ToString) & " ", Conn.DB.DataBaseName.DB_HR, "0")

                    Select Case _ScanCardCtl
                        Case 0

                            Call HI.HRCAL.Calculate.UpdateScanTwoTime(_ActualTimMIn, _ActualTimMOut,
                                     _ActualTimAIn, _ActualTimAOut, _ActualTimOTIn1, _ActualTimOTOut1,
                                      _ActualTimOTIn2, _ActualTimOTOut2, FTScanMIn,
                                     FTScanMOut, FTScanAIn, FTScanAOut, FTScanAOTIn,
                                     FTScanAOTOut, FTScanAOTIn2, FTScanAOTOut2,
                                     _CheckTimeMIn, _CheckTimeMOut, _CheckTimeAIn, _CheckTimeAOut, _CheckTimeOTIn1, _CheckTimeOTOut1, _CheckTimeOTIn2, _CheckTimeOTOut2,
                                     _ScanCardOverClock, _ScanCardOverClock, Me.ActualNextDate, Me.ActualDate, _TmpScanIn, _TmpScanOut)', _FTShiftId

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
                    _Qry &= vbCrLf & "  FTIn1='" & _ActualTimMIn & "'"
                    _Qry &= vbCrLf & ",FTOut1='" & _ActualTimMOut & "'"
                    _Qry &= vbCrLf & ",FTIn2='" & _ActualTimAIn & "'"
                    _Qry &= vbCrLf & ",FTOut2='" & _ActualTimAOut & "'"
                    _Qry &= vbCrLf & ",FTIn3='" & _ActualTimOTIn1 & "'"
                    _Qry &= vbCrLf & ",FTOut3='" & _ActualTimOTOut1 & "'"
                    _Qry &= vbCrLf & ",FTIn4='" & _ActualTimOTIn2 & "'"
                    _Qry &= vbCrLf & ",FTOut4='" & _ActualTimOTOut2 & "'"
                    _Qry &= vbCrLf & ",FTOtMIn='" & _ActualScanOTInM & "'"
                    _Qry &= vbCrLf & ",FTOtMOut='" & _ActualScanOTOutM & "'"
                    'Select Case _ScanCardCtl
                    '    Case 10
                    '        _Qry &= vbCrLf & ",FTScanMIn='" & "" & Microsoft.VisualBasic.Right(_TmpScanIn, 5) & "' "
                    '        _Qry &= vbCrLf & ",FTScanMOut='' "
                    '        _Qry &= vbCrLf & ",FTScanAIn='' "
                    '        _Qry &= vbCrLf & ",FTScanAOut='" & Microsoft.VisualBasic.Right(_TmpScanOut, 5) & "' "
                    '        _Qry &= vbCrLf & ",FTScanAOTIn='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanAOTIn").ToString & "' "
                    '        _Qry &= vbCrLf & ",FTScanAOTOut='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanAOTOut").ToString & "' "
                    '        _Qry &= vbCrLf & ",FTScanAOTIn2='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanAOTIn2").ToString & "' "
                    '        _Qry &= vbCrLf & ",FTScanAOTOut2='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanAOTOut2").ToString & "' "
                    '        _Qry &= vbCrLf & ",FNHSysTranStaId=" & Val("" & .GetRowCellValue(RowHandleIdx, "FNHSysTranStaId").ToString) & " "
                    '        _Qry &= vbCrLf & ",FTStateEditTime='1'"
                    '        _Qry &= vbCrLf & ",FTScanMInM='" & Microsoft.VisualBasic.Right(_TmpScanIn, 5) & "' "
                    '        _Qry &= vbCrLf & ",FTScanMOutM='' "
                    '        _Qry &= vbCrLf & ",FTScanAInM='' "
                    '        _Qry &= vbCrLf & ",FTScanAOutM='" & Microsoft.VisualBasic.Right(_TmpScanOut, 5) & "' "
                    '        _Qry &= vbCrLf & ",FTScanAOTInM='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanAOTInM").ToString & "' "
                    '        _Qry &= vbCrLf & ",FTScanAOTOutM='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanAOTOutM").ToString & "' "
                    '    Case Else

                    _Qry &= vbCrLf & ",FTScanMIn='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanMIn").ToString & "' "
                    _Qry &= vbCrLf & ",FTScanMOut='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanMOut").ToString & "' "
                    _Qry &= vbCrLf & ",FTScanAIn='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanAIn").ToString & "' "
                    _Qry &= vbCrLf & ",FTScanAOut='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanAOut").ToString & "' "
                    _Qry &= vbCrLf & ",FTScanAOTIn='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanAOTIn").ToString & "' "
                    _Qry &= vbCrLf & ",FTScanAOTOut='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanAOTOut").ToString & "' "
                    _Qry &= vbCrLf & ",FTScanAOTIn2='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanAOTIn2").ToString & "' "
                    _Qry &= vbCrLf & ",FTScanAOTOut2='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanAOTOut2").ToString & "' "
                    _Qry &= vbCrLf & ",FNHSysTranStaId=" & Val("" & .GetRowCellValue(RowHandleIdx, "FNHSysTranStaId").ToString) & " "
                    _Qry &= vbCrLf & ",FTStateEditTime='1'"
                    _Qry &= vbCrLf & ",FTScanMInM='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanMInM").ToString & "' "
                    _Qry &= vbCrLf & ",FTScanMOutM='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanMOutM").ToString & "' "
                    _Qry &= vbCrLf & ",FTScanAInM='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanAInM").ToString & "' "
                    _Qry &= vbCrLf & ",FTScanAOutM='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanAOutM").ToString & "' "
                    _Qry &= vbCrLf & ",FTScanAOTInM='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanAOTInM").ToString & "' "
                    _Qry &= vbCrLf & ",FTScanAOTOutM='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanAOTOutM").ToString & "' "

                    _Qry &= vbCrLf & ",FTScanOTMIn='" & FTScanOTMInM & "'"
                    _Qry &= vbCrLf & ",FTScanOTMOut='" & FTScanOTMOutM & "'"
                    _Qry &= vbCrLf & ",FTScanOTMInM='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanOtMInM").ToString & "' "
                    _Qry &= vbCrLf & ",FTScanOTMOutM='" & "" & .GetRowCellValue(RowHandleIdx, "FTScanOtMOutM").ToString & "' "

                    ' End Select

                    _Qry &= vbCrLf & " WHERE FTDateTrans='" & HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(RowHandleIdx, "FTDateTrans").ToString) & "'  "
                    _Qry &= vbCrLf & " AND FNHSysEmpID=" & Val("" & .GetRowCellValue(RowHandleIdx, "FNHSysEmpID").ToString) & ""
                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

                    HI.HRCAL.Calculate.CalculateWorkTime(HI.ST.UserInfo.UserName, "" & .GetRowCellValue(RowHandleIdx, "FNHSysEmpID").ToString, HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(RowHandleIdx, "FTDateTrans").ToString), HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(RowHandleIdx, "FTDateTrans").ToString))
                    HI.HRCAL.Calculate.DisposeObject()

                    _Qry = " SELECT T.FNHSysEmpID, M.FTEmpCode"

                    If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                        _Qry &= vbCrLf & " , PR.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"
                    Else
                        _Qry &= vbCrLf & " , PR.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
                    End If

                    _Qry &= vbCrLf & " ,CASE WHEN  ISDATE(T.FTDateTrans) = 1 THEN Convert(varchar(10),Convert(Datetime,T.FTDateTrans),103) ELSE '' END As FTDateTrans"
                    _Qry &= vbCrLf & " ,CASE WHEN  ISDATE(T.FTDateTrans) = 1 THEN Convert(Datetime,T.FTDateTrans) ELSE NULL END As FTDateTransShow"
                    _Qry &= vbCrLf & "  , T.FNHSysShiftIDOrg, T.FNHSysShiftID, SH.FTShiftCode"
                    _Qry &= vbCrLf & "  ,SH.FTShiftCode AS FTShiftCodeOrg"
                    _Qry &= vbCrLf & "  , SH.FTIn1 AS FTCheckIn1"
                    _Qry &= vbCrLf & "  , SH.FTOut1 As FTCheckOut1"
                    _Qry &= vbCrLf & "  , SH.FTIn2 As FTCheckIn2"
                    _Qry &= vbCrLf & "  , SH.FTOut2 AS FTCheckOut2"
                    '_Qry &= vbCrLf & "  , OT.FTOtIn AS FTCheckOTAIn1"
                    '_Qry &= vbCrLf & "  , OT.FTOtOut AS FTCheckOTAOut1"
                    '_Qry &= vbCrLf & "  , OT.FTOtIn3 AS FTCheckOTAIn2"
                    '_Qry &= vbCrLf & "  , OT.FTOtOut3 AS FTCheckOTAOut2,  T.FTIn1 , "
                    '_Qry &= vbCrLf & "    T.FTOut1 , T.FTIn2 , T.FTOut2 , T.FTIn3, T.FTOut3, T.FTIn4, T.FTOut4"
                    '_Qry &= vbCrLf & " , FTScanMIn, FTScanMOut, FTScanAIn, FTScanAOut, FTScanAOTIn, FTScanAOTOut, FTScanAOTIn2,FTScanAOTOut2"

                    _Qry &= vbCrLf & "  , OT.FTOtMIn AS FTCheckOTMIn"
                    _Qry &= vbCrLf & "  , OT.FTOtMOut AS FTCheckOTMOut"

                    _Qry &= vbCrLf & "  , OT.FTOtIn AS FTCheckOTAIn1"
                    _Qry &= vbCrLf & "  , OT.FTOtOut AS FTCheckOTAOut1"
                    _Qry &= vbCrLf & "  , OT.FTOtIn3 AS FTCheckOTAIn2"
                    _Qry &= vbCrLf & "  , OT.FTOtOut3 AS FTCheckOTAOut2,   SH.FTIn1 , "
                    _Qry &= vbCrLf & "    T.FTOut1 , T.FTIn2 , T.FTOut2 , T.FTIn3, T.FTOut3, T.FTIn4, T.FTOut4"
                    _Qry &= vbCrLf & " , FTScanMIn, FTScanMOut, FTScanAIn, FTScanAOut, FTScanAOTIn, FTScanAOTOut, FTScanAOTIn2,FTScanAOTOut2,FTScanOtMInM,FTScanOtMOutM,FTScanOtMIn,FTScanOtMOut"

                    _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNTime),'.',':') AS FNTime"
                    _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT1),'.',':') AS FNOT1"
                    _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT1_5),'.',':') AS FNOT1_5"
                    _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT2),'.',':') AS FNOT2"
                    _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT3),'.',':') AS FNOT3"
                    _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT4),'.',':') AS FNOT4"
                    _Qry &= vbCrLf & ",  T.FNLateNormalMin, T.FNLateNormalCut, T.FNAbsentCut, T.FNAbsent, T.FNCutAbsent, T.FNHSysEmpID"
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
                    '_Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN (FTScanMIn='' OR FTScanAOut='')  THEN '1' Else '0' END"
                    '_Qry &= vbCrLf & "        ELSE  CASE  WHEN (FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')     THEN '1' Else '0' END END  AS FTStateError"

                    _Qry &= vbCrLf & " , CASE WHEN  T.FNScanCtrl = 0 THEN CASE WHEN (FTScanMIn='' OR (FTScanAOut='' AND FTScanAOTOut ='')) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2   THEN '1' Else '0' END"
                    _Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN ((FTScanMIn='' OR FTScanAOut='') OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>''))) ) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2   THEN '1' Else '0' END"
                    _Qry &= vbCrLf & "        ELSE  CASE  WHEN ((FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')  OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>'')))) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2      THEN '1' Else '0' END END AS FTStateError "

                    _Qry &= vbCrLf & " ,ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
                    _Qry &= vbCrLf & " ,ISNULL(D.FTDeptCode,'') AS FTDeptCode "
                    _Qry &= vbCrLf & " ,ISNULL(Di.FTDivisonCode,'') AS FTDivisonCode "
                    _Qry &= vbCrLf & " ,ISNULL(S.FTSectCode,'') AS FTSectCode "
                    _Qry &= vbCrLf & " ,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode "
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
                    _Qry &= vbCrLf & ", ISNULL(SPD.FTDate,'') AS SPD"
                    _Qry &= vbCrLf & " ,  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_LoadTimeLeave(T.FNHSysEmpID,T.FTDateTrans,'" & HI.ST.Lang.Language.ToString & "') AS FTLeave"

                    _Qry &= vbCrLf & ", ISNULL(FTScanMInM,'') as FTScanMInM"
                    _Qry &= vbCrLf & ", ISNULL(FTScanMOutM,'') AS FTScanMOutM"
                    _Qry &= vbCrLf & ", ISNULL(FTScanAInM,'') AS FTScanAInM"
                    _Qry &= vbCrLf & ", ISNULL(FTScanAOutM,'') AS FTScanAOutM"
                    _Qry &= vbCrLf & ", ISNULL(FTScanAOTInM,'') AS FTScanAOTInM"
                    _Qry &= vbCrLf & ", ISNULL(FTScanAOTOutM,'') AS FTScanAOTOutM"
                    _Qry &= vbCrLf & ", CASE WHEN  ISDATE(ISNULL(M.FDDateEnd,'')) = 1 THEN Convert(varchar(10),Convert(Datetime,ISNULL(M.FDDateEnd,'')),103) ELSE '' END  AS FDDateEnd"
                    _Qry &= vbCrLf & ",M.FNUseBarcode"
                    _Qry &= vbCrLf & ",M.FTEmpCodeRefer"
                    _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK) LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS SH WITH (NOLOCK) ON T.FNHSysShiftID = SH.FNHSysShiftID LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest AS OT WITH (NOLOCK) ON T.FNHSysEmpID = OT.FNHSysEmpID AND T.FTDateTrans = OT.FTDateRequest"
                    _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMTranStatus AS TM ON T.FNHSysTranStaId = TM.FNHSysTranStaId "
                    _Qry &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK)  ON  T.FNHSysEmpID =  M.FNHSysEmpID LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId = P.FNHSysPositId LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
                    _Qry &= vbCrLf & "    INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS PR WITH (NOLOCK) ON M.FNHSysPreNameId = PR.FNHSysPreNameId"
                    _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday AS HD WITH (NOLOCK) ON T.FTDateTrans = HD.FDHolidayDate AND HD.FNHSysCmpId = M.FNHSysCmpId "
                    _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailySpecial AS SPD WITH (NOLOCK) ON T.FTDateTrans = SPD.FTDate AND T.FNHSysEmpID=SPD.FNHSysEmpID"


                    _Qry &= vbCrLf & "    LEFT OUTER JOIN"
                    _Qry &= vbCrLf & "  (SELECT        FNHSysEmpID, FTDateTrans, SUM(FNTotalMinute) AS FNTotalLeaveMin"
                    _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave AS TL WITH(NOLOCK)"
                    _Qry &= vbCrLf & "  GROUP BY FNHSysEmpID, FTDateTrans ) AS TL"
                    _Qry &= vbCrLf & "  ON T.FNHSysEmpID = TL.FNHSysEmpID AND T.FTDateTrans=TL.FTDateTrans"

                    _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly AS EHL WITH (NOLOCK) ON T.FNHSysEmpID=EHL.FNHSysEmpID"
                    _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmpTypeWeeklySpecial AS ETHL WITH (NOLOCK) ON T.FTDateTrans = ETHL.FDHolidayDate AND M.FNHSysEmpTypeId=ETHL.FNHSysEmpTypeId"


                    _Qry &= vbCrLf & " WHERE T.FTDateTrans='" & HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(RowHandleIdx, "FTDateTrans").ToString) & "'  "
                    _Qry &= vbCrLf & " AND T.FNHSysEmpID=" & Val("" & .GetRowCellValue(RowHandleIdx, "FNHSysEmpID").ToString) & ""


                    Dim _TmpDt As DataTable = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)


                    If _TmpDt.Rows.Count > 0 Then
                        With CType(Me.ogdtime.DataSource, DataTable)
                            For Each R As DataRow In .Select("FNHSysEmpID=" & Val("" & ogvtime.GetRowCellValue(RowHandleIdx, "FNHSysEmpID").ToString) & " AND FTDateTrans='" & HI.UL.ULDate.ConvertEN("" & ogvtime.GetRowCellValue(RowHandleIdx, "FTDateTrans").ToString) & "' ")

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

                    _RowDataChange = False
                End With


                StateCal = False
            End If
        Catch ex As Exception
            _RowDataChange = False
            StateCal = False
        End Try
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

#End Region

#Region "General"


    Private Sub ogvtime_BeforeLeaveRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles ogvtime.BeforeLeaveRow
        Call SaveGrid(e.RowHandle)
    End Sub

    Private Sub ogvtime_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles ogvtime.CellValueChanged
        _RowDataChange = True
    End Sub

    Private Sub ocmdelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        With ogvtime
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
            If Not (ocmdelete.Enabled) Then Exit Sub
            If Not (_RowDataChange) Then Exit Sub
            Dim _Qry As String = ""

            _Qry = " SELECT TOP 1  FTDateTrans  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans   "
            _Qry &= vbCrLf & " WHERE FTDateTrans='" & HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTDateTrans").ToString) & "'  "
            _Qry &= vbCrLf & " AND FNHSysEmpID=" & Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID").ToString) & ""

        End With

    End Sub


    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"

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

            Call InitGrid()

            Call HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)
            StateCal = False
            With ogvtime
                For Each GridCol As DevExpress.XtraGrid.Columns.GridColumn In .Columns
                    With GridCol
                        If (.OptionsColumn.AllowEdit) Then
                            .AppearanceCell.BackColor = Color.Transparent
                            .OptionsColumn.AllowShowHide = False
                        End If
                    End With
                Next


            End With
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

    Private Sub ocmloadtimeerror_Click(sender As System.Object, e As System.EventArgs) Handles ocmloadtimeerror.Click
        If Me.FDStartDate.Text <> "" Then
            If Me.FDEndDate.Text <> "" Then
                Call LoadData(True)
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
        'Select Case e.FocusedColumn.FieldName.ToString
        '    Case "FTScanMIn", "FTScanMOut", "FTScanAIn", "FTScanAOut", "FTScanAOTIn", "FTScanAOTOut"
        '        If e.PrevFocusedColumn.Name.ToString Then

        '        End If
        '    Case Else
        '        Try
        '            If Not (ogdtime.DataSource Is Nothing) Then
        '                CType(ogdtime.DataSource, DataTable).AcceptChanges()
        '                Call SaveGrid(ogvtime.FocusedRowHandle)
        '            End If
        '        Catch ex As Exception
        '        End Try
        'End Select
        If Not (e.PrevFocusedColumn Is Nothing) Then
            Try
                Select Case e.PrevFocusedColumn.FieldName.ToString
                    Case "FTScanMIn", "FTScanMOut", "FTScanAIn", "FTScanAOut", "FTScanAOTIn", "FTScanAOTOut", "FTScanOtMIn", "FTScanOtMOut"
                        Try
                            If Not (ogdtime.DataSource Is Nothing) Then
                                CType(ogdtime.DataSource, DataTable).AcceptChanges()
                                Call SaveGrid(ogvtime.FocusedRowHandle)
                            End If
                        Catch ex As Exception
                        End Try
                End Select
            Catch ex As Exception
            End Try
        End If

    End Sub

    'Private Sub ogvtime_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles ogvtime.RowCellStyle

    '    'Dim FTLeaveType_0 As String = "", FTLeaveType_1 As String = "", FTLeaveType_2 As String = "", FTLeaveType_4 As String = ""
    '    'Dim FTLeaveType_5 As String = "", FTLeaveType_6 As String = "", FTLeaveType_7 As String = "", FTLeaveType_8 As String = ""
    '    'Dim FTLeaveType_9 As String = "", FTLeaveType_16 As String = "", FTLeaveType_17 As String = "", FTLeaveType_97 As String = ""
    '    'Dim FTLeaveType_98 As String = "", FTLeaveType_99 As String = "", FTLeaveType_EMPEND As String = "", FTLeaveType_H As String = ""
    '    'Dim FTLeaveType_S As String = "", FTLeaveType_W As String = ""

    '    'If dtTimeColor Is Nothing Then
    '    '    dtTimeColor = HI.HRCAL.Time.LoadTimeColor()

    '    '    For Each R As DataRow In dtTimeColor.Rows

    '    '    Next

    '    'End If

    '    With ogvtime



    '        Try
    '            If .GetRowCellValue(e.RowHandle, "FTStateError") = "1" Then
    '                e.Appearance.ForeColor = System.Drawing.Color.Red
    '            End If
    '        Catch ex As Exception
    '        End Try

    '        Try
    '            Select Case True
    '                Case ("" & .GetRowCellValue(e.RowHandle, "FDDateEnd").ToString <> "")
    '                    Try

    '                        For Each R As DataRow In dtTimeColor.Select("FTLeaveType='EMPEND'")
    '                            e.Appearance.BackColor = System.Drawing.Color.FromArgb(R!FTColor.ToString)
    '                            Exit For
    '                        Next

    '                    Catch ex As Exception
    '                    End Try

    '                Case ("" & .GetRowCellValue(e.RowHandle, "FTLeaveCode").ToString <> "")

    '                    Try
    '                        For Each R As DataRow In dtTimeColor.Select("FTLeaveType='" & ("" & .GetRowCellValue(e.RowHandle, "FTLeaveCode").ToString) & "'")
    '                            e.Appearance.BackColor = System.Drawing.Color.FromArgb(R!FTColor.ToString)
    '                            Exit For
    '                        Next
    '                    Catch ex As Exception
    '                    End Try

    '                Case ("" & .GetRowCellValue(e.RowHandle, "FDHolidayDate").ToString <> "")

    '                    Try

    '                        For Each R As DataRow In dtTimeColor.Select("FTLeaveType='H'")
    '                            e.Appearance.BackColor = System.Drawing.Color.FromArgb(R!FTColor.ToString)
    '                            Exit For
    '                        Next

    '                    Catch ex As Exception
    '                    End Try

    '                Case ("" & .GetRowCellValue(e.RowHandle, "FTWeekly").ToString = "1")

    '                    Try

    '                        For Each R As DataRow In dtTimeColor.Select("FTLeaveType='W'")
    '                            e.Appearance.BackColor = System.Drawing.Color.FromArgb(R!FTColor.ToString)
    '                            Exit For
    '                        Next

    '                    Catch ex As Exception
    '                    End Try
    '                Case ("" & .GetRowCellValue(e.RowHandle, "SPD").ToString <> "")
    '                    Try

    '                        For Each R As DataRow In dtTimeColor.Select("FTLeaveType='S'")
    '                            e.Appearance.BackColor = System.Drawing.Color.FromArgb(R!FTColor.ToString)
    '                            Exit For
    '                        Next

    '                    Catch ex As Exception
    '                    End Try
    '            End Select

    '        Catch ex As Exception

    '        End Try

    '        Try
    '            If "" & .GetRowCellValue(e.RowHandle, "FTScanAOut").ToString <> "" Then
    '                Dim starttime As String = "" & .GetRowCellValue(e.RowHandle, "FTCheckOut2").ToString
    '                Dim endtime As String = "" & .GetRowCellValue(e.RowHandle, "FTScanAOut").ToString
    '                Dim Datetrans As String = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(e.RowHandle, "FTDateTrans").ToString)


    '                Dim timediff As Integer = DateDiff(DateInterval.Minute, CDate(Datetrans & " " & starttime), CDate(Datetrans & " " & endtime))

    '                If timediff >= 60 Then
    '                    e.Appearance.ForeColor = System.Drawing.Color.Blue
    '                    e.Appearance.BackColor = Color.OrangeRed
    '                End If

    '            End If
    '        Catch ex As Exception

    '        End Try



    '    End With

    'End Sub

    Private Sub ocmclear_Click(sender As System.Object, e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(Me)
    End Sub

    Private Sub RepTimeEdit_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepTimeEdit.EditValueChanging
        Try
            With Me.ogvtime

                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                If Not (ocmsavedetail.Enabled) Then
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
                                'Try
                                '    .SetFocusedRowCellValue("" & .FocusedColumn.FieldName.ToString & "M", e.NewValue.ToString)
                                'Catch ex As Exception
                                'End Try
                            End If
                        End If

                    Catch ex As Exception
                    End Try

                End If

            End With
        Catch ex As Exception
        End Try
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
            If sender.EditValue Is Nothing Then
                Try
                    ogvtime.SetFocusedRowCellValue("" & ogvtime.FocusedColumn.FieldName.ToString & "M", "")
                Catch ex As Exception
                End Try
                Try
                    CType(ogdtime.DataSource, DataTable).AcceptChanges()
                Catch ex As Exception
                End Try
            Else
                If sender.text <> "" Or sender.EditValue.ToString <> "" Then
                    Dim _TmpData As String = sender.EditValue.ToString

                    If (_TmpData).Length <= 6 Then
                        _TmpData = Me.ActualDate & " " & _TmpData
                    End If

                    Dim _Time As String = Format(CDate(_TmpData), "HH:mm")

                    'If Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_Time, 3), 1) = ":" And Microsoft.VisualBasic.Left(_Time, 1) = "0" Then
                    '    _Time = Microsoft.VisualBasic.Right(_Time, 5)
                    'End If

                    'Dim dt As New DataTable
                    'Try
                    '    dt = CType(ogdtime.DataSource, DataTable).Copy
                    '    With dt

                    '        Dim _EmpCode As String = ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTEmpCode").ToString
                    '        Dim _Str As String = "FNHSysEmpID=" & Val("" & ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FNHSysEmpID").ToString) & " AND FTDateTrans='" & HI.UL.ULDate.ConvertEN("" & ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTDateTrans").ToString) & "' "
                    '        For Each R As DataRow In .Select(_Str)
                    '            R.Item(ogvtime.FocusedColumn.FieldName.ToString) = _Time
                    '            R.Item(ogvtime.FocusedColumn.FieldName.ToString & "M") = _Time
                    '        Next
                    '    End With

                    '    ogdtime.DataSource = dt.Copy
                    '    dt.Dispose()
                    'Catch ex As Exception
                    '    dt.Dispose()
                    'End Try

                    ogvtime.SetRowCellValue(ogvtime.FocusedRowHandle, ogvtime.FocusedColumn, _Time)

                    Try
                        ogvtime.SetFocusedRowCellValue("" & ogvtime.FocusedColumn.FieldName.ToString & "M", _Time)
                    Catch ex As Exception
                    End Try
                    Dim xValue As String = ""
                    Try
                        xValue = ogvtime.ActiveFilter.Item(ogvtime.FocusedColumn).Filter.Value
                    Catch ex As Exception
                    End Try

                    If xValue <> "" Then
                        Call SaveGrid(ogvtime.FocusedRowHandle)
                    End If

                    Try
                        CType(ogdtime.DataSource, DataTable).AcceptChanges()
                    Catch ex As Exception
                    End Try
                Else
                    Try
                        ogvtime.SetFocusedRowCellValue("" & ogvtime.FocusedColumn.FieldName.ToString & "M", "")
                    Catch ex As Exception
                    End Try
                    Try
                        CType(ogdtime.DataSource, DataTable).AcceptChanges()
                    Catch ex As Exception
                    End Try
                    'Dim dt As New DataTable
                    'Try
                    '    dt = CType(ogdtime.DataSource, DataTable).Copy
                    '    With dt
                    '        Dim _EmpCode As String = ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTEmpCode").ToString
                    '        Dim _Str As String = "FNHSysEmpID=" & Val("" & ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FNHSysEmpID").ToString) & " AND FTDateTrans='" & HI.UL.ULDate.ConvertEN("" & ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTDateTrans").ToString) & "' "
                    '        For Each R As DataRow In .Select(_Str)
                    '            R.Item(ogvtime.FocusedColumn.FieldName.ToString) = ""
                    '            R.Item(ogvtime.FocusedColumn.FieldName.ToString & "M") = ""
                    '        Next
                    '    End With

                    '    ogdtime.DataSource = dt.Copy
                    '    dt.Dispose()


                    'Catch ex As Exception
                    '    dt.Dispose()
                    'End Try

                End If
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
                            Dim _DateTrans As String = HI.UL.ULDate.ConvertEnDB("" & ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FTDateTrans").ToString)
                            Dim _FNHSysEmpId As String = "" & ogvtime.GetRowCellValue(ogvtime.FocusedRowHandle, "FNHSysEmpID").ToString

                            If HI.HRCAL.Time.CheckClosePeriod(HI.UL.ULDate.ConvertEnDB(_DateTrans), Val(_FNHSysEmpId)) = True Then
                                HI.MG.ShowMsg.mInfo("ไม่สามารถทำการแก้ไขข้อมูลหลังการปิดงวดได้ กรุณาทำการตรวจสอบ", 14040001, Me.Text)
                                e.Cancel = True
                            Else

                                Dim _FNSysShiftIDOrginal As Integer = Integer.Parse(Val("" & .GetFocusedRowCellValue("FNHSysShiftIDOrg").ToString))

                                If e.NewValue <> "" & .GetFocusedRowCellValue("FTShiftCodeOrg").ToString Then
                                    Dim _Qry As String

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
                                    HI.HRCAL.Calculate.DisposeObject()

                                    _Qry = " SELECT T.FNHSysEmpID, M.FTEmpCode"

                                    If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                                        _Qry &= vbCrLf & " , PR.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

                                    Else
                                        _Qry &= vbCrLf & " , PR.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
                                    End If

                                    _Qry &= vbCrLf & " ,CASE WHEN  ISDATE(T.FTDateTrans) = 1 THEN Convert(varchar(10),Convert(Datetime,T.FTDateTrans),103) ELSE '' END As FTDateTrans"
                                    _Qry &= vbCrLf & " ,CASE WHEN  ISDATE(T.FTDateTrans) = 1 THEN Convert(Datetime,T.FTDateTrans) ELSE NULL END As FTDateTransShow"
                                    _Qry &= vbCrLf & "  , T.FNHSysShiftIDOrg, T.FNHSysShiftID, SH.FTShiftCode"
                                    _Qry &= vbCrLf & "  ,SH.FTShiftCode AS FTShiftCodeOrg"
                                    _Qry &= vbCrLf & "  , SH.FTIn1 AS FTCheckIn1"
                                    _Qry &= vbCrLf & "  , SH.FTOut1 As FTCheckOut1"
                                    _Qry &= vbCrLf & "  , SH.FTIn2 As FTCheckIn2"
                                    _Qry &= vbCrLf & "  , SH.FTOut2 AS FTCheckOut2"
                                    _Qry &= vbCrLf & "  , OT.FTOtIn AS FTCheckOTAIn1"
                                    _Qry &= vbCrLf & "  , OT.FTOtOut AS FTCheckOTAOut1"
                                    _Qry &= vbCrLf & "  , OT.FTOtIn3 AS FTCheckOTAIn2"
                                    _Qry &= vbCrLf & "  , OT.FTOtOut3 AS FTCheckOTAOut2,  SH.FTIn1 , "
                                    _Qry &= vbCrLf & "    T.FTOut1 , T.FTIn2 , T.FTOut2 , T.FTIn3, T.FTOut3, T.FTIn4, T.FTOut4"
                                    _Qry &= vbCrLf & " , FTScanMIn, FTScanMOut, FTScanAIn, FTScanAOut, FTScanAOTIn, FTScanAOTOut, FTScanAOTIn2,FTScanAOTOut2"
                                    _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNTime),'.',':') AS FNTime"
                                    _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT1),'.',':') AS FNOT1"
                                    _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT1_5),'.',':') AS FNOT1_5"
                                    _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT2),'.',':') AS FNOT2"
                                    _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT3),'.',':') AS FNOT3"
                                    _Qry &= vbCrLf & ", Replace(Convert(varchar(30),T.FNOT4),'.',':') AS FNOT4"
                                    _Qry &= vbCrLf & ",  T.FNLateNormalMin, T.FNLateNormalCut, T.FNAbsentCut, T.FNAbsent, T.FNCutAbsent, T.FNHSysEmpID"
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
                                    '_Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN (FTScanMIn='' OR FTScanAOut='')  THEN '1' Else '0' END"
                                    '_Qry &= vbCrLf & "        ELSE  CASE  WHEN (FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')     THEN '1' Else '0' END END  AS FTStateError"


                                    _Qry &= vbCrLf & " , CASE WHEN  T.FNScanCtrl = 0 THEN CASE WHEN (FTScanMIn='' OR (FTScanAOut='' AND FTScanAOTOut ='')) AND ISNULL(FNTotalLeaveMin,0)<480  THEN '1' Else '0' END"
                                    _Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN ((FTScanMIn='' OR FTScanAOut='') OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>''))) ) AND ISNULL(FNTotalLeaveMin,0)<480   THEN '1' Else '0' END"
                                    _Qry &= vbCrLf & "        ELSE  CASE  WHEN ((FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')  OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>'')))) AND ISNULL(FNTotalLeaveMin,0)<480      THEN '1' Else '0' END END AS FTStateError "


                                    _Qry &= vbCrLf & " ,ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
                                    _Qry &= vbCrLf & " ,ISNULL(D.FTDeptCode,'') AS FTDeptCode "
                                    _Qry &= vbCrLf & " ,ISNULL(Di.FTDivisonCode,'') AS FTDivisonCode "
                                    _Qry &= vbCrLf & " ,ISNULL(S.FTSectCode,'') AS FTSectCode "
                                    _Qry &= vbCrLf & " ,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode "
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
                                    _Qry &= vbCrLf & ", ISNULL(SPD.FTDate,'') AS SPD"
                                    _Qry &= vbCrLf & " ,  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_LoadTimeLeave(T.FNHSysEmpID,T.FTDateTrans,'" & HI.ST.Lang.Language.ToString & "') AS FTLeave"
                                    _Qry &= vbCrLf & ", ISNULL(FTScanMInM,'') as FTScanMInM"
                                    _Qry &= vbCrLf & ", ISNULL(FTScanMOutM,'') AS FTScanMOutM"
                                    _Qry &= vbCrLf & ", ISNULL(FTScanAInM,'') AS FTScanAInM"
                                    _Qry &= vbCrLf & ", ISNULL(FTScanAOutM,'') AS FTScanAOutM"
                                    _Qry &= vbCrLf & ", ISNULL(FTScanAOTInM,'') AS FTScanAOTInM"
                                    _Qry &= vbCrLf & ", ISNULL(FTScanAOTOutM,'') AS FTScanAOTOutM"
                                    _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTrans AS T WITH (NOLOCK) LEFT OUTER JOIN"
                                    _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS SH WITH (NOLOCK) ON T.FNHSysShiftID = SH.FNHSysShiftID LEFT OUTER JOIN"
                                    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailyOTRequest AS OT WITH (NOLOCK) ON T.FNHSysEmpID = OT.FNHSysEmpID AND T.FTDateTrans = OT.FTDateRequest"
                                    _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMTranStatus AS TM ON T.FNHSysTranStaId = TM.FNHSysTranStaId "
                                    _Qry &= vbCrLf & "   INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK)  ON  T.FNHSysEmpID =  M.FNHSysEmpID LEFT OUTER JOIN"
                                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId = P.FNHSysPositId LEFT OUTER JOIN"
                                    _Qry &= vbCrLf & "    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
                                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
                                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
                                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId LEFT OUTER JOIN"
                                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
                                    _Qry &= vbCrLf & "    INNER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS PR WITH (NOLOCK) ON M.FNHSysPreNameId = PR.FNHSysPreNameId"
                                    _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday AS HD WITH (NOLOCK) ON T.FTDateTrans = HD.FDHolidayDate AND HD.FNHSysCmpId = M.FNHSysCmpId "
                                    _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailySpecial AS SPD WITH (NOLOCK) ON T.FTDateTrans = SPD.FTDate AND T.FNHSysEmpID=SPD.FNHSysEmpID"
                                    _Qry &= vbCrLf & "    LEFT OUTER JOIN"
                                    _Qry &= vbCrLf & "  (SELECT        FNHSysEmpID, FTDateTrans, SUM(FNTotalMinute) AS FNTotalLeaveMin"
                                    _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave AS TL WITH(NOLOCK)"
                                    _Qry &= vbCrLf & "  GROUP BY FNHSysEmpID, FTDateTrans ) AS TL"
                                    _Qry &= vbCrLf & "  ON T.FNHSysEmpID = TL.FNHSysEmpID AND T.FTDateTrans=TL.FTDateTrans"

                                    _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly AS EHL WITH (NOLOCK) ON T.FNHSysEmpID=EHL.FNHSysEmpID"
                                    _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmpTypeWeeklySpecial AS ETHL WITH (NOLOCK) ON T.FTDateTrans = ETHL.FDHolidayDate AND M.FNHSysEmpTypeId=ETHL.FNHSysEmpTypeId"

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

                                Else

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

    Private Sub ocmsavedetail_Click(sender As Object, e As EventArgs) Handles ocmsavedetail.Click

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmloadlate_Click(sender As Object, e As EventArgs) Handles ocmloadlate.Click
        If Me.FDStartDate.Text <> "" Then

            If Me.FDEndDate.Text <> "" Then
                Call LoadDataLate()
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDEndDate_lbl.Text)
                FDEndDate.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateRequest_lbl.Text)
            FDStartDate.Focus()
        End If
    End Sub

    Private Sub ocmloadwaitleave_Click(sender As Object, e As EventArgs) Handles ocmloadwaitleave.Click
        If Me.FDStartDate.Text <> "" Then

            If Me.FDEndDate.Text <> "" Then
                Call LoadDataWaitLeave()
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FDEndDate_lbl.Text)
                FDEndDate.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateRequest_lbl.Text)
            FDStartDate.Focus()
        End If
    End Sub

    Private Sub ogvtime_RowStyle(sender As Object, e As RowStyleEventArgs) Handles ogvtime.RowStyle


        'Dim FTLeaveType_0 As String = "", FTLeaveType_1 As String = "", FTLeaveType_2 As String = "", FTLeaveType_4 As String = ""
        'Dim FTLeaveType_5 As String = "", FTLeaveType_6 As String = "", FTLeaveType_7 As String = "", FTLeaveType_8 As String = ""
        'Dim FTLeaveType_9 As String = "", FTLeaveType_16 As String = "", FTLeaveType_17 As String = "", FTLeaveType_97 As String = ""
        'Dim FTLeaveType_98 As String = "", FTLeaveType_99 As String = "", FTLeaveType_EMPEND As String = "", FTLeaveType_H As String = ""
        'Dim FTLeaveType_S As String = "", FTLeaveType_W As String = ""

        If dtTimeColor Is Nothing Then
            dtTimeColor = HI.HRCAL.Time.LoadTimeColor()

        End If


        With ogvtime


            Try
                If .GetRowCellValue(e.RowHandle, "FTStateError") = "1" Then
                    e.Appearance.ForeColor = System.Drawing.Color.Red
                    ' e.Appearance.BackColor2 = System.Drawing.Color.Green
                End If
            Catch ex As Exception
            End Try

            Try
                Select Case True
                    Case ("" & .GetRowCellValue(e.RowHandle, "FDDateEnd").ToString <> "")
                        Try

                            For Each R As DataRow In dtTimeColor.Select("FTLeaveType='EMPEND'")
                                e.Appearance.BackColor = System.Drawing.Color.FromArgb(R!FTColor.ToString)
                                Exit For
                            Next

                        Catch ex As Exception
                        End Try

                    Case ("" & .GetRowCellValue(e.RowHandle, "FTLeaveCode").ToString <> "")

                        Try
                            For Each R As DataRow In dtTimeColor.Select("FTLeaveType='" & ("" & .GetRowCellValue(e.RowHandle, "FTLeaveCode").ToString) & "'")
                                e.Appearance.BackColor = System.Drawing.Color.FromArgb(R!FTColor.ToString)
                                Exit For
                            Next
                        Catch ex As Exception
                        End Try

                    Case ("" & .GetRowCellValue(e.RowHandle, "FDHolidayDate").ToString <> "")

                        Try

                            For Each R As DataRow In dtTimeColor.Select("FTLeaveType='H'")
                                e.Appearance.BackColor = System.Drawing.Color.FromArgb(R!FTColor.ToString)
                                '  e.Appearance.BackColor2 = System.Drawing.Color.FromArgb(R!FTColor.ToString)
                                '.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(R!FTColor.ToString)
                                Exit For
                            Next

                        Catch ex As Exception
                        End Try

                    Case ("" & .GetRowCellValue(e.RowHandle, "FTWeekly").ToString = "1")

                        Try

                            For Each R As DataRow In dtTimeColor.Select("FTLeaveType='W'")
                                e.Appearance.BackColor = System.Drawing.Color.FromArgb(R!FTColor.ToString)
                                Exit For
                            Next

                        Catch ex As Exception
                        End Try
                    Case ("" & .GetRowCellValue(e.RowHandle, "SPD").ToString <> "")
                        Try

                            For Each R As DataRow In dtTimeColor.Select("FTLeaveType='S'")
                                e.Appearance.BackColor = System.Drawing.Color.FromArgb(R!FTColor.ToString)
                                Exit For
                            Next

                        Catch ex As Exception
                        End Try
                End Select

            Catch ex As Exception

            End Try

            Try
                If "" & .GetRowCellValue(e.RowHandle, "FTScanAOut").ToString <> "" Then
                    Dim starttime As String = "" & .GetRowCellValue(e.RowHandle, "FTCheckOut2").ToString
                    Dim endtime As String = "" & .GetRowCellValue(e.RowHandle, "FTScanAOut").ToString
                    Dim Datetrans As String = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(e.RowHandle, "FTDateTrans").ToString)


                    Dim timediff As Integer = DateDiff(DateInterval.Minute, CDate(Datetrans & " " & starttime), CDate(Datetrans & " " & endtime))

                    If timediff >= 60 Then
                        e.Appearance.ForeColor = System.Drawing.Color.Blue
                        e.Appearance.BackColor = Color.OrangeRed
                    End If

                End If
            Catch ex As Exception

            End Try



        End With
    End Sub
End Class