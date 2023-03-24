Imports DevExpress.Data
Imports System.Drawing

Public Class wTimeAttendanceAccepted

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
        Dim sFieldSum As String = "FTStateRevised|FNLateNormalMin|FNLateNormalCut"
        Dim sFieldGrpCount As String = "FTEmpCode"
        Dim sFieldGrpSum As String = "FNEmpWork|FNLateNormalMin|FNLateNormalCut"

        'T.FNLateNormalMin, T.FNLateNormalCut
        Dim sFieldCustomSum As String = "FNTime|FNOTRequest|FNOT1|FNOT1_5|FNOT2|FNOT3|FNOT4"
        Dim sFieldCustomGrpSum As String = ""

        With ogvtime
            .ClearGrouping()
            .ClearDocument()
            .Columns("FTDateTrans").Group()

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
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
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

        _Qry = " SELECT  CASE WHEN ISNULL(FTStateRevised,'0') ='1' THEN '1' ELSE '0' END AS FTStateRevised, T.FNHSysEmpID, M.FTEmpCode"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & " , PR.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

        Else
            _Qry &= vbCrLf & " , PR.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
        End If

        _Qry &= vbCrLf & " ,CASE WHEN  ISDATE(T.FTDateTrans) = 1 THEN Convert(varchar(10),Convert(Datetime,T.FTDateTrans),103) ELSE '' END As FTDateTrans"
        _Qry &= vbCrLf & "  , T.FNHSysShiftIDOrg, T.FNHSysShiftID, SH.FTShiftCode"
        _Qry &= vbCrLf & "  ,SH.FTShiftCode AS FTShiftCodeOrg"
        _Qry &= vbCrLf & "  , SH.FTIn1 AS FTCheckIn1"
        _Qry &= vbCrLf & "  , SH.FTOut1 As FTCheckOut1"
        _Qry &= vbCrLf & "  , SH.FTIn2 As FTCheckIn2"
        _Qry &= vbCrLf & "  , SH.FTOut2 AS FTCheckOut2"
        _Qry &= vbCrLf & "  , OT.FTOtIn AS FTCheckOTAIn1"
        _Qry &= vbCrLf & "  , OT.FTOtOut AS FTCheckOTAOut1"
        _Qry &= vbCrLf & "  , OT.FTOtIn3 AS FTCheckOTAIn2"
        _Qry &= vbCrLf & "  , OT.FTOtOut3 AS FTCheckOTAOut2,  T.FTIn1 , "
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

        _Qry &= vbCrLf & " ,CASE WHEN  T.FNScanCtrl = 0 THEN CASE WHEN (FTScanMIn='' OR (FTScanAOut='' AND FTScanAOTOut ='')) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2  THEN '1' Else '0' END"
        _Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN ((FTScanMIn='' OR FTScanAOut='') OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>''))) ) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2   THEN '1' Else '0' END"
        _Qry &= vbCrLf & "        ELSE  CASE  WHEN ((FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')  OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>'')))) AND ISNULL(FNTotalLeaveMin,0)<480    AND M.FNUseBarcode<>2    THEN '1' Else '0' END END AS FTStateError "

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
        _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday AS HD WITH (NOLOCK) ON T.FTDateTrans = HD.FDHolidayDate"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailySpecial AS SPD WITH (NOLOCK) ON T.FTDateTrans = SPD.FTDate AND T.FNHSysEmpID=SPD.FNHSysEmpID"


        _Qry &= vbCrLf & "    LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  (SELECT        FNHSysEmpID, FTDateTrans, SUM(FNTotalMinute) AS FNTotalLeaveMin"
        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTTransLeave AS TL WITH(NOLOCK)"
        _Qry &= vbCrLf & "  GROUP BY FNHSysEmpID, FTDateTrans ) AS TL"
        _Qry &= vbCrLf & "  ON T.FNHSysEmpID = TL.FNHSysEmpID AND T.FTDateTrans=TL.FTDateTrans"

        _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly AS EHL WITH (NOLOCK) ON T.FNHSysEmpID=EHL.FNHSysEmpID"
        _Qry &= vbCrLf & "    LEFT OUTER JOIN   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmpTypeWeeklySpecial AS ETHL WITH (NOLOCK) ON T.FTDateTrans = ETHL.FDHolidayDate AND M.FNHSysEmpTypeId=ETHL.FNHSysEmpTypeId"


        _Qry &= vbCrLf & "  WHERE  M.FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " "
        _Qry &= vbCrLf & "    AND T.FTDateTrans>='" & HI.UL.ULDate.ConvertEnDB(FDStartDate.Text) & "'  AND T.FTDateTrans <='" & HI.UL.ULDate.ConvertEnDB(FDEndDate.Text) & "' "
        _Qry &= vbCrLf & "    AND  (ISNULL(FTStateAccept,'0') = '1'  OR (ISNULL(FTStateAccept,'0') = '0' AND ISNULL(FTStateRevised,'0') = '1'    ) ) "
        _Qry &= vbCrLf & "  AND  (T.FTDateTrans <=  CASE WHEN ISNULL(M.FDDateEnd,'') ='' THEN '9999/99/99' ELSE  ISNULL(M.FDDateEnd,'')  END)   "

        If HI.ST.SysInfo.HideSunday Then
            _Qry &= vbCrLf & " AND ISNULL(T.FTWeekDay,'') <>'1' "
        End If

        If (StateTimeError) Then

            _Qry &= vbCrLf & " AND CASE WHEN  T.FNScanCtrl = 0 THEN CASE WHEN (FTScanMIn='' OR (FTScanAOut='' AND FTScanAOTOut ='')) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2   THEN '1' Else '0' END"
            _Qry &= vbCrLf & "   WHEN  T.FNScanCtrl = 1 THEN CASE WHEN ((FTScanMIn='' OR FTScanAOut='') OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>''))) ) AND ISNULL(FNTotalLeaveMin,0)<480  AND M.FNUseBarcode<>2    THEN '1' Else '0' END"
            _Qry &= vbCrLf & "        ELSE  CASE  WHEN ((FTScanMIn='' OR FTScanMOut='' OR FTScanAIn ='' OR FTScanAOut='')  OR (OT.FTOtIn  <>'' AND ((FTScanAOTIn='' AND FTScanAOTOut <>'') OR (FTScanAOTOut='' AND FTScanAOTIn<>'')))) AND ISNULL(FNTotalLeaveMin,0)<480    AND M.FNUseBarcode<>2    THEN '1' Else '0' END END  ='1' "

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

        Me.ogctime.DataSource = _dt
        Me.ogvtime.BestFitColumns()
        ogvtime.ExpandAllGroups()
        _Spls.Close()

        _RowDataChange = False

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

    Private Sub ogvtime_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles ogvtime.CellValueChanged
        _RowDataChange = True
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("en-US", True)
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"


            Me.FNHSysCmpId.Text = "1"
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

            Call InitGrid()

            Call HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)
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

    Private Sub ocmloadtimeerror_Click(sender As System.Object, e As System.EventArgs)
        If Me.FDStartDate.Text <> "" Then
            Call LoadData(True)
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateRequest_lbl.Text)
            FDStartDate.Focus()
        End If
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
                    Case ("" & .GetRowCellValue(e.RowHandle, "FDDateEnd").ToString <> "")
                        Try

                            For Each R As DataRow In dtTimeColor.Select("FTLeaveType='EMPEND'")
                                e.Appearance.BackColor = Drawing.Color.FromArgb(R!FTColor.ToString)
                                Exit For
                            Next

                        Catch ex As Exception
                        End Try

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
                    Case ("" & .GetRowCellValue(e.RowHandle, "SPD").ToString <> "")
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

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click

        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvtime)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmloadtimeerror_Click_1(sender As Object, e As EventArgs) Handles ocmloadtimeerror.Click
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

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub
End Class