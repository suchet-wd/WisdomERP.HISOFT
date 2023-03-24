Imports DevExpress.Data
Imports System.IO

Public Class wJobMinutesDaily

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

        _CfgIncentiveAmtDigit = HI.Conn.SQLConn.GetField("SELECT FTCfgData FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SECURITY) & "].[dbo].[TSESystemConfig] WHERE FTCfgName = 'CfgIncentiveAmtDigit' ", Conn.DB.DataBaseName.DB_SECURITY, "")


        '------Start Add Summary Grid-------------
        'Dim sFieldCount As String = "FTEmpCode"
        Dim sFieldSum As String = "FNQuantity|FNTotalMinute"
        'Dim sFieldGrpCount As String = "FTDateTrans"
        'Dim sFieldGrpSum As String = "FNNetPerDay"

        Dim sFieldCustomSum As String = ""
        Dim sFieldCustomGrpSum As String = ""

        With ogvtime
            .ClearGrouping()
            .ClearDocument()
            '.Columns("FTDateTrans").Group()

            'For Each Str As String In sFieldCount.Split("|")
            '    If Str <> "" Then
            '        .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
            '        .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
            '    End If
            'Next

            For Each Str As String In sFieldCustomSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Custom, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n0}"
                End If
            Next

            For Each Str As String In sFieldSum.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Sum, Str)
                    .Columns(Str).SummaryItem.DisplayFormat = "{0:n" + _CfgIncentiveAmtDigit + "}"
                    .Columns(Str).DisplayFormat.FormatString = "{0:n" + _CfgIncentiveAmtDigit + "}"
                End If
            Next

            'For Each Str As String In sFieldGrpCount.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, Str, Nothing, "(Count by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
            '    End If
            'Next

            For Each Str As String In sFieldCustomGrpSum.Split("|")
                If Str <> "" Then
                    .GroupSummary.Add(DevExpress.Data.SummaryItemType.Custom, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n0})")
                End If
            Next

            'For Each Str As String In sFieldGrpSum.Split("|")
            '    If Str <> "" Then
            '        .GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, Str, Nothing, "(Sum by " & .Columns.ColumnByFieldName(Str).Caption & "={0:n2})")
            '    End If
            'Next

            .OptionsView.ShowFooter = True

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
        Dim _dtDtl As DataTable
        StateCal = False

        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")
        Dim _FTStartCalculateDate As String = HI.UL.ULDate.ConvertEnDB(FDStartDate.Text)
        Dim _FTEndCalculateDate As String = HI.UL.ULDate.ConvertEnDB(FDEndDate.Text)

        _Qry = "SELECT Convert(varchar(10),Convert(Datetime,OT.FTCalDate),103)  As FTCalDate  "
        _Qry &= vbCrLf & " , MAX(OT.FTOrderNo) [FTOrderNo] ,MAX(OT.FTSubOrderNo) [FTSubOrderNo],MAX(ST.FTStyleCode) [FTStyleCode] "


        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  , ST.FTStyleNameTH AS 'FTStyleName' "

        Else
            _Qry &= vbCrLf & "  , ST.FTStyleNameEN AS 'FTStyleName' "
        End If

        _Qry &= vbCrLf & "    , SUM(OT.FNQuantity) [FNQuantity] ,SUM(OT.FNTotalMinute) [FNTotalMinute]  ,OT.FNAction "


        _Qry &= vbCrLf & " , CASE WHEN OT.FNAction = '0' THEN 'Cut' "
        _Qry &= vbCrLf & " WHEN OT.FNAction = '1' THEN 'embellishment' "
        _Qry &= vbCrLf & " WHEN OT.FNAction = '2' THEN 'SuperMarket' "
        _Qry &= vbCrLf & " WHEN OT.FNAction = '3' THEN 'Sew' "
        _Qry &= vbCrLf & " WHEN OT.FNAction = '4' THEN 'Pack To Box' "
        _Qry &= vbCrLf & " END AS 'FTAction'"



        _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRTIncentive_OrderTime] OT With  (NOLOCK)  LEFT OUTER JOIN "
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TMERMStyle] ST ON OT.FNHSysStyleId=ST.FNHSysStyleId  LEFT OUTER JOIN "
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TCNMUnitSect US ON OT.FNHSysUnitSectId=US.FNHSysUnitSectId "

        _Qry &= vbCrLf & "  WHERE  OT.FTCalDate >='" & _FTStartCalculateDate & "' "
        _Qry &= vbCrLf & " AND (OT.FTCalDate ='' OR OT.FTCalDate <='" & _FTEndCalculateDate & "' )   "



        If Me.FNHSysEmpTypeId.Text <> "" Then
            _Qry &= vbCrLf & " AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
        End If



        '------Criteria Unit Sect
        If Me.FNHSysUnitSectId.Text <> "" Then
            _Qry &= vbCrLf & " AND   US.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
        End If

        If Me.FNHSysUnitSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND   US.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
        End If


        _Qry &= vbCrLf & " GROUP BY OT.FTCalDate,  OT.FTOrderNo, OT.FTSubOrderNo, ST.FTStyleCode, ST.FTStyleNameTH, OT.FNAction   "
        _Qry &= vbCrLf & "  ORDER BY  FTCalDate,  OT.FNAction  ,  OT.FTOrderNo, OT.FTSubOrderNo  "

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.ogdtime.DataSource = _dt

        Me.ogvtime.OptionsView.ShowFooter = True
        Me.ogvtime.BestFitColumns()
        ogvtime.ExpandAllGroups()


        ''''''''''''Detail'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        _Qry = "SELECT  Convert(varchar(10),Convert(Datetime,OT.FTCalDate),103)  As FTCalDate "
        _Qry &= vbCrLf & " ,OT.FNHSysUnitSectId,OT.FNHSysStyleId "
        _Qry &= vbCrLf & " ,US.FTUnitSectCode "

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  ,US.FTUnitSectNameTH AS 'FTUnitSectName' "

        Else
            _Qry &= vbCrLf & "  ,US.FTUnitSectNameEN AS 'FTUnitSectName' "
        End If

        _Qry &= vbCrLf & " , OT.FTOrderNo ,OT.FTSubOrderNo ,ST.FTStyleCode "



        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  , ST.FTStyleNameTH AS 'FTStyleName' "

        Else
            _Qry &= vbCrLf & "  , ST.FTStyleNameEN AS 'FTStyleName' "
        End If

        _Qry &= vbCrLf & " , OT.FNSeq  ,OT.FTStartTime ,OT.FTEndTime ,OT.FNTotalMinute ,OT.FNQuantity  ,OT.FNAction "
        _Qry &= vbCrLf & " , CASE WHEN OT.FNAction = '0' THEN 'Cut' "
        _Qry &= vbCrLf & " WHEN OT.FNAction = '1' THEN 'embellishment' "
        _Qry &= vbCrLf & " WHEN OT.FNAction = '2' THEN 'SuperMarket' "
        _Qry &= vbCrLf & " WHEN OT.FNAction = '3' THEN 'Sew' "
        _Qry &= vbCrLf & " WHEN OT.FNAction = '4' THEN 'Pack To Box' "
        _Qry &= vbCrLf & " END AS 'FTAction'"



        _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].[dbo].[THRTIncentive_OrderTime] OT With  (NOLOCK)  LEFT OUTER JOIN "
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].[TMERMStyle] ST ON OT.FNHSysStyleId=ST.FNHSysStyleId  LEFT OUTER JOIN "
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].[dbo].TCNMUnitSect US ON OT.FNHSysUnitSectId=US.FNHSysUnitSectId "

        _Qry &= vbCrLf & "  WHERE  OT.FTCalDate >='" & _FTStartCalculateDate & "' "
        _Qry &= vbCrLf & " AND (OT.FTCalDate ='' OR OT.FTCalDate <='" & _FTEndCalculateDate & "' )   "



        If Me.FNHSysEmpTypeId.Text <> "" Then
            _Qry &= vbCrLf & " AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
        End If



        '------Criteria Unit Sect
        If Me.FNHSysUnitSectId.Text <> "" Then
            _Qry &= vbCrLf & " AND   US.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
        End If

        If Me.FNHSysUnitSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND   US.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
        End If

        _Qry &= vbCrLf & " ORDER BY  FTCalDate, FNAction,FTUnitSectCode, FNSeq  "

        _dtDtl = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.ogdtimeDtl.DataSource = _dtDtl
        Me.ogvtimeDtl.BestFitColumns()
        ogvtimeDtl.ExpandAllGroups()



        _Spls.Close()

        _RowDataChange = False

    End Sub

#End Region

#Region "General"

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.FNHSysCmpId.Text = "1"
            Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

            HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogvtime)
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
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogvtime)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click
        With Me.ogvtime

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