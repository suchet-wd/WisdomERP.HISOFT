Imports C1.Win.C1FlexGrid
Imports C1.Win.C1FlexGrid.Classic
Imports System.Drawing
Imports DevExpress.XtraScheduler
Imports System.IO
Imports System.Windows.Forms
Imports System.Globalization

Public Class wPlanningDevelop

    Private _SysPathImageSystem As String = Application.StartupPath & IIf(Microsoft.VisualBasic.Right(Application.StartupPath, 1) = "\", "", "\") & "Images\System"
    Private Const _GraphNo As String = "FTGraphNo"
    Private Const _GraphObject As String = "FOGraph"
    Public Declare Auto Function GetCursorPos Lib "User32.dll" (ByRef lpPoint As Point) As Integer
    Private mousepos As Point
    Private _StateFormLoad As Boolean = False
    Private _dtGraph As DataTable
    Private _dtSewingLine As DataTable
    Private _wAddPlan As wPlanningDevelopAddPlan
    Private _ListDataSubOrder As New List(Of DataTable)
    Private _ListDataSubOrderProd As New List(Of DataTable)
    Private Const StartFixedRow As Integer = 5
    Private Const StartFixedCol As Integer = 5

    Sub New()
        _StateFormLoad = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


        _wAddPlan = New wPlanningDevelopAddPlan
      
        HI.TL.HandlerControl.AddHandlerObj(_wAddPlan)


        Dim _SystemLang As New ST.SysLanguage
        Try
            Call _SystemLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _wAddPlan.Name.ToString.Trim, _wAddPlan)
        Catch ex As Exception
        Finally
        End Try

    End Sub

#Region "Procedure"

    Enum StateWorkDate As Integer
        StateNormal = 0
        StateWeekly = 1
        StateHoliday = 2
    End Enum

    Private Sub CreateSewingLine()

        _dtSewingLine = New DataTable
        _dtSewingLine.Columns.Add("FNRowIdx", GetType(Integer))
        _dtSewingLine.Columns.Add("FNHSysUnitSectId", GetType(Integer))
        _dtSewingLine.Columns.Add("FTUnitSectCode", GetType(String))
        _dtSewingLine.Columns.Add("FNTotalEmp", GetType(Integer))
        _dtSewingLine.Columns.Add("FNWorkTimeMin", GetType(Integer))
        _dtSewingLine.Columns.Add("FNLostTimeMin", GetType(Integer))

    End Sub

    Private Sub CreateTabGraph()

        If Not (_dtGraph Is Nothing) Then
            _dtGraph.Dispose()
        End If

        _dtGraph = New DataTable
        _dtGraph.Columns.Add("FTGraphNo", GetType(String))
        _dtGraph.Columns.Add("FNRowIdx", GetType(Integer))
        _dtGraph.Columns.Add("FNHSysUnitSectId", GetType(Integer))
        _dtGraph.Columns.Add("FTOrderNo", GetType(String))
        _dtGraph.Columns.Add("FTSubOrderNo", GetType(String))
        _dtGraph.Columns.Add("FNStartCol", GetType(Integer))
        _dtGraph.Columns.Add("FNEndCol", GetType(Integer))
        _dtGraph.Columns.Add("FTStartDate", GetType(String))
        _dtGraph.Columns.Add("FTEndDate", GetType(String))
        _dtGraph.Columns.Add("FOGraph", GetType(Object))

    End Sub

    Private Function CheckStateDay(ByVal _date As String, xFNHSysCmpId As Integer) As StateWorkDate
        Try

            If Weekday(CDate(HI.UL.ULDate.ConvertEnDB(_date))) = 1 Then
                Return StateWorkDate.StateWeekly
            End If

            If Me.LoadSysHoliday(xFNHSysCmpId).Select("FDHolidayDate='" & HI.UL.ULDate.ConvertEnDB(_date) & "'").Length > 0 Then
                Return StateWorkDate.StateHoliday
            End If

        Catch ex As Exception
        End Try

        Return StateWorkDate.StateNormal
    End Function

    Private _SysHoliday As DataTable = Nothing
    Private ReadOnly Property LoadSysHoliday(xFNHSysCmpId As Integer) As DataTable
        Get
            If _SysHoliday Is Nothing Then
                Dim _Qry As String
                _Qry = "SELECt   FDHolidayDate,FNHolidayType AS FTHldType  "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMHoliday WITH(NOLOCK) "
                _Qry &= vbCrLf & "  WHERE FTStateActive='1' "

                _SysHoliday = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
            End If
            Return _SysHoliday
        End Get
    End Property

    Private _SysMonthName As DataTable = Nothing
    Private ReadOnly Property LoadSysMonthName() As DataTable
        Get
            If _SysMonthName Is Nothing Then
                Dim _Qry As String
                _Qry = "SELECt   FNListIndex, FTNameTH, FTNameEN  "
                _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK) "
                _Qry &= vbCrLf & " WHERE  (FTListName = N'FNMonth')"

                _SysMonthName = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)
            End If
            Return _SysMonthName
        End Get
    End Property

    Private Sub InitGridPlanning(xFNHSysCmpId As Integer)
        Dim _ColWidth As Integer = 7
        Dim _FTYear As String = ""
        Dim _FTMonth As String = ""
        Dim _FTMonthName As String = ""
        Dim _FTDay As String = ""
        Dim _FTDayName() As String = {"SUN", "M", "T", "W", "TH", "F", "S"}
        Dim _FTDayNameDiaplay As String = ""


        Select Case HI.ST.Lang.Language
            Case ST.Lang.eLang.TH
                _FTDayName = {"อา.", "จ.", "อ.", "พ.", "พฤ.", "ศ.", "ส."}
            Case Else
                _FTDayName = {"SUN", "M", "T", "W", "TH", "F", "S"}
        End Select

        With Me.ogvplaning
            .FontName = ""
            .Redraw = RedrawSettings.flexRDNone
            .Clear()
            .Rows = StartFixedRow
            .Cols = StartFixedCol
            .FixedRows = 5
            .FixedCols = 4

            For I As Integer = 0 To StartFixedRow - 1

                .set_TextMatrix(I, 0, "Factory")
                .SetUserData(I, 0, "Factory")
                .SetData(I, 0, "Factory")

                .set_TextMatrix(I, 1, "Prod. Line")
                .SetUserData(I, 1, "Prod. Line")
                .SetData(I, 1, "Prod. Line")

                .set_TextMatrix(I, 2, "Emp.")
                .SetUserData(I, 2, "Emp.")
                .SetData(I, 2, "Emp.")

                .set_TextMatrix(I, 3, "WorkTime")
                .SetUserData(I, 3, "WorkTime")
                .SetData(I, 3, "WorkTime")

                .set_TextMatrix(I, 4, "LostTime")
                .SetUserData(I, 4, "LostTime")
                .SetData(I, 4, "LostTime")

            Next

            If xFNHSysCmpId <= 0 Then
                For I As Integer = 0 To 4
                    .set_MergeCol(I, True)
                Next

                .set_RowHeight(4, 0)

                .set_ColWidth(0, 70)
                .set_ColWidth(1, 80)
                .set_ColWidth(2, 30)
                .set_ColWidth(3, 0)
                .set_ColWidth(4, 0)


                .MergeCells = MergeSettings.flexMergeFixedOnly
                .ExplorerBar = ExplorerBarSettings.flexExNone
                .HighLight = HighLightEnum.Never
                .FocusRect = FocusRectEnum.None
                .Editable = EditableSettings.flexEDNone
                .AllowUserResizing = AllowUserResizeSettings.flexResizeNone
                .Redraw = RedrawSettings.flexRDBuffered
                Exit Sub
            End If

            Dim _Qry As String = ""
            Dim _StartDate As String = HI.UL.ULDate.ConvertEnDB(HI.ST.UserInfo.LogINDate)
            Dim _PlanDate As String = ""

            _Qry = " SELECT   TOP 1 PD.FTDate "
            _Qry &= vbCrLf & "	FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPlan AS P WITH(NOLOCK)"
            _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPlanDate AS PD WITH(NOLOCK) ON P.FTGraphNo = PD.FTGraphNo "
            _Qry &= vbCrLf & "	WHERE P.FNHSysCmpId=" & xFNHSysCmpId & ""
            _Qry &= vbCrLf & "  AND ISNULL(P.FTStateFinish,'') <>'1'	"
            _Qry &= vbCrLf & "  AND ISNULL(PD.FTStateFinishDate,'') <>'1'"
            _Qry &= vbCrLf & "  AND ISNULL(PD.FNPlanQuantity ,0) > ISNULL(PD.FNPlanFinishQuantity,0)"
            _Qry &= vbCrLf & "  ORDER BY  PD.FTDate ASC "

            _PlanDate = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PLANNING, "")

            If _PlanDate <> "" Then
                If _PlanDate < _StartDate Then
                    _StartDate = _PlanDate
                End If
            End If

            Dim _EndDate As String = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddYear(_StartDate, 1))
            Dim CRange As CellRange

            While _StartDate <= _EndDate

                _FTYear = Microsoft.VisualBasic.Left(_StartDate, 4)
                _FTMonth = Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(_StartDate, 7), 2)
                _FTDay = Microsoft.VisualBasic.Right(_StartDate, 2)
                _FTMonthName = _FTMonth

                For Each R As DataRow In Me.LoadSysMonthName.Select("FNListIndex=" & Integer.Parse(Val(_FTMonth)) & "")

                    Select Case HI.ST.Lang.Language
                        Case ST.Lang.eLang.TH
                            _FTMonthName = R!FTNameTH.ToString
                        Case Else
                            _FTMonthName = R!FTNameEN.ToString
                    End Select

                    Exit For
                Next
                _FTDayNameDiaplay = _FTDayName(Weekday(CDate(HI.UL.ULDate.ConvertEnDB(_StartDate))) - 1)

                For I As Integer = 1 To 8
                    .Cols = .Cols + 1
                    .set_ColWidth(.Cols - 1, _ColWidth)
                    .set_ColDataType(.Cols - 1, GetType(String))

                    .set_TextMatrix(0, .Cols - 1, _FTYear)
                    .SetUserData(0, .Cols - 1, _StartDate)
                    .SetData(0, .Cols - 1, _FTYear)

                    .set_TextMatrix(1, .Cols - 1, _FTMonthName)
                    .SetUserData(1, .Cols - 1, _StartDate)
                    .SetData(1, .Cols - 1, _FTMonthName)

                    .set_TextMatrix(2, .Cols - 1, _FTDay)
                    .SetUserData(2, .Cols - 1, _StartDate)
                    .SetData(2, .Cols - 1, _FTDay)

                    .set_TextMatrix(3, .Cols - 1, _FTDayNameDiaplay)
                    .SetUserData(3, .Cols - 1, _StartDate)
                    .SetData(3, .Cols - 1, _FTDayNameDiaplay)

                    .set_TextMatrix(4, .Cols - 1, " ")
                    .SetUserData(4, .Cols - 1, _StartDate)
                    .SetData(4, .Cols - 1, _FTDay & I.ToString)

                Next

                _StartDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_StartDate, 1))
            End While

            .set_ColAlignment(0, AlignmentSettings.flexAlignLeftCenter)
            .set_ColAlignment(1, AlignmentSettings.flexAlignLeftCenter)

            .Cell(CellPropertySettings.flexcpAlignment, 0, 0, .Rows - 1, .Cols - 1) = AlignmentSettings.flexAlignCenterCenter
            .MergeCells = MergeSettings.flexMergeFree

            For I As Integer = 0 To 4
                .set_MergeCol(I, True)
                .set_MergeRow(I, True)
            Next

            .set_RowHeight(4, 0)

            .set_ColWidth(0, 70)
            .set_ColWidth(1, 80)
            .set_ColWidth(2, 30)
            .set_ColWidth(3, 0)
            .set_ColWidth(4, 0)

            .MergeCells = MergeSettings.flexMergeFixedOnly
            .ExplorerBar = ExplorerBarSettings.flexExNone
            .HighLight = HighLightEnum.Never
            .FocusRect = FocusRectEnum.None
            .Editable = EditableSettings.flexEDNone
            .AllowUserResizing = AllowUserResizeSettings.flexResizeNone
            .Redraw = RedrawSettings.flexRDBuffered
        End With

        Call CreateSewingLine()
        ' Call CreateTabGraph()
        Call InitGridProductionSewingLine(xFNHSysCmpId)
        Call InitGridLineHeader(xFNHSysCmpId)
        'Call CreateSewingPlaing(0)
        Call LoadDataSewingPlanning(xFNHSysCmpId)
    End Sub

    Private Sub LoadDataSewingPlanning(xFNHSysCmpId As Integer)

        Call CreateTabGraph()

        Dim CR As CellRange
        With Me.ogvplaning

            If .Rows - 1 < StartFixedRow Then
                Exit Sub
            End If

            .Redraw = RedrawSettings.flexRDNone

            CR = .GetCellRange(StartFixedRow, StartFixedCol, .Rows - 1, .Cols - 1)
            CR.Clear(ClearFlags.All)

            Dim _Qry As String = ""
            Dim _dtplan As New DataTable
            Dim _dtplanDate As New DataTable
            Dim _dtplanBreakdown As New DataTable
            Dim _dtplanDateAddTime As New DataTable
            Dim _GraphObject As GraphObject

            _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.SP_LOAD_DATA_SEWING_PLANNING '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & xFNHSysCmpId & ""
            _dtplan = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PLANNING)

            If _dtplan.Rows.Count > 0 Then

                Dim _SewingLineNo As String = ""
                Dim _SewingStateDate As String = ""
                Dim _SewingStateFixDate As Boolean = False
                Dim _SewingLineIndx As Integer = 0
                Dim _FNHSysUnitSectId As Integer = 0
                Dim _SamSewing As Double = 0
                Dim _FNTotalEmp As Integer = 0
                Dim _FNWorkTimeMin As Integer = 0
                Dim _FNLostTimeMin As Integer = 0
                Dim _OrderNo As String = ""
                Dim _Season As String = ""
                Dim _Style As String = ""
                Dim _SubOrderNo As String = ""
                Dim _ColorWay As String = ""
                Dim _ShipDate As String = ""
                Dim _Continent As String = ""
                Dim _Country As String = ""
                Dim _ShipMode As String = ""

                Dim _StateCreatGraph As Integer = 0
                Dim _tmpPic As Image
                Dim _ListPlanDetal As New List(Of GraphObjectDetail)
                Dim _ListPlanDetalColor As New List(Of GraphObjectDetailColor)
                Dim _StateAddGraph As Boolean = False
                Dim _TotalPlan As Integer = 0
                Dim _TotalPlanByColor As Integer = 0
                Dim _GraphBGImgIdx As Integer = 0
                Dim _TotalPlanFinish As Integer = 0

                '_StateCreatGraph = .FNCreateGraphProdType.SelectedIndex
                '_GraphBGImgIdx = .FNGraphImageIndex.SelectedIndex
                '_OrderNo = .FTOrderNo.Text.Trim()
                '_tmpPic = .FPImage.Image
                '_Season = .FNHSysSeasonId.Text
                '_Style = .FNHSysStyleId.Text

                'For Each RxLine As DataRow In Me._dtSewingLine.Select("FTUnitSectCode='" & HI.UL.ULF.rpQuoted(_SewingLineNo) & "'")

                '    _SewingLineIndx = Integer.Parse(Val(RxLine!FNRowIdx.ToString))
                '    _FNHSysUnitSectId = Integer.Parse(Val(RxLine!FNHSysUnitSectId.ToString))
                '    _FNTotalEmp = Integer.Parse(Val(RxLine!FNTotalEmp.ToString))
                '    _FNWorkTimeMin = Integer.Parse(Val(RxLine!FNWorkTimeMin.ToString))
                '    _FNLostTimeMin = Integer.Parse(Val(RxLine!FNLostTimeMin.ToString))

                '    Exit For
                'Next

                _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.SP_LOAD_DATA_SEWING_PLANNING_DATE '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & xFNHSysCmpId & ""
                _dtplanDate = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PLANNING)

                _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.SP_LOAD_DATA_SEWING_PLANNING_BREAKDOWN '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & xFNHSysCmpId & ""
                _dtplanBreakdown = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PLANNING)

                _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.SP_LOAD_DATA_SEWING_PLANNING_DATE_ADDTIME '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'," & xFNHSysCmpId & ""
                _dtplanDateAddTime = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PLANNING)

                For Each R As DataRow In _dtplan.Rows

                    _ListPlanDetal = New List(Of GraphObjectDetail)
                    _ListPlanDetalColor = New List(Of GraphObjectDetailColor)

                    For Each RxBD As DataRow In _dtplanBreakdown.Select("FTGraphNo='" & HI.UL.ULF.rpQuoted(R!FTGraphNo.ToString()) & "'")

                        Dim _GobjDetailColor As New GraphObjectDetailColor

                        With _GobjDetailColor
                            .GraphNo = R!FTGraphNo.ToString()
                            .ColorWay = RxBD!FTColorway.ToString()
                            .SizeBreakDown = RxBD!FTSizeBreakDown.ToString()
                            .TotalPlanQty = (Integer.Parse(Val(RxBD!FNQuantity.ToString())))
                            .FinishQty = (Integer.Parse(Val(RxBD!FNFinishQuantity.ToString())))
                        End With

                        _ListPlanDetalColor.Add(_GobjDetailColor)
                    Next

                    _TotalPlanFinish = 0
                    For Each Rx As DataRow In _dtplanDate.Select("FTGraphNo='" & HI.UL.ULF.rpQuoted(R!FTGraphNo.ToString()) & "'")

                        Dim _GobjDetail As New GraphObjectDetail

                        With _GobjDetail
                            .GraphNo = R!FTGraphNo.ToString()
                            .GraphDate = Rx!FTDate.ToString
                            .TotalEmp = (Integer.Parse(Val(Rx!FNTotalEmp.ToString())))
                            .WorkTimeMin = (Integer.Parse(Val(Rx!FNWorkTimeMin.ToString())))
                            .LostTimeMin = (Integer.Parse(Val(Rx!FNLostTimeMin.ToString())))
                            .SamData = (Double.Parse(Val(Rx!FNSam.ToString())))
                            .CapacityPerDay = (Integer.Parse(Val(Rx!FNCapacityPerDay.ToString())))
                            .TotalPlanQty = (Integer.Parse(Val(Rx!FNPlanQuantity.ToString())))
                            .FinishQty = (Integer.Parse(Val(Rx!FNPlanFinishQuantity.ToString())))
                            .FTStateFinishDate = (Rx!FTStateFinishDate.ToString = "1")
                            .LearningCurve = Val(Rx!FNLearningCurve.ToString)

                            _TotalPlanFinish = _TotalPlanFinish + (Integer.Parse(Val(Rx!FNPlanFinishQuantity.ToString())))

                        End With

                        _ListPlanDetal.Add(_GobjDetail)

                    Next

                    _tmpPic = Nothing
                    Try
                        _tmpPic = HI.UL.ULImage.ConvertByteArrayToImmage(R!FPOrderImage)
                    Catch ex As Exception
                    End Try

                    _FNHSysUnitSectId = Integer.Parse(Val(R!FNHSysUnitSectId.ToString))
                    _SewingLineNo = ""
                    _SewingLineIndx = 0
                    _FNTotalEmp = 0
                    _FNWorkTimeMin = 0
                    _FNLostTimeMin = 0

                    For Each RxLine As DataRow In Me._dtSewingLine.Select("FNHSysUnitSectId=" & _FNHSysUnitSectId & "")

                        _SewingLineNo = RxLine!FTUnitSectCode.ToString
                        _SewingLineIndx = Integer.Parse(Val(RxLine!FNRowIdx.ToString))
                        _FNTotalEmp = Integer.Parse(Val(RxLine!FNTotalEmp.ToString))
                        _FNWorkTimeMin = Integer.Parse(Val(RxLine!FNWorkTimeMin.ToString))
                        _FNLostTimeMin = Integer.Parse(Val(RxLine!FNLostTimeMin.ToString))

                        Exit For
                    Next

                    Dim _DtTimeAdd As New DataTable

                    If _dtplanDateAddTime.Select("FTGraphNo='" & HI.UL.ULF.rpQuoted(R!FTGraphNo.ToString()) & "'").Length > 0 Then
                        _DtTimeAdd = _dtplanDateAddTime.Select("FTGraphNo='" & HI.UL.ULF.rpQuoted(R!FTGraphNo.ToString()) & "'").CopyToDataTable
                    Else
                        _DtTimeAdd = Nothing
                    End If

                    _GraphObject = New GraphObject

                    With _GraphObject

                        .GraphNo = R!FTGraphNo.ToString()
                        .OrderNo = R!FTOrderNo.ToString
                        .OrderSubNo = R!FTSubOrderNo.ToString
                        .GraphData = R!FTOrderNo.ToString & " : " & R!FTSubOrderNo.ToString
                        .GraphUserData = R!FTGraphNo.ToString()
                        .StyleNo = R!FTStyleCode.ToString
                        .Season = R!FTSeasonCode.ToString
                        .SamData = Double.Parse(Val(R!FNSam.ToString))
                        .TotalEmp = Integer.Parse(Val(R!FNTotalEmp.ToString))
                        .WorkTimeMin = Integer.Parse(Val(R!FNWorkTimeMin.ToString))
                        .LostTimeMin = Integer.Parse(Val(R!FNLostTimeMin.ToString))
                        .SewingLineNo = _SewingLineNo
                        .GraphRowIdx = _SewingLineIndx
                        .TotalPlan = Integer.Parse(Val(R!FNTotalPlan.ToString))
                        .TotalFinish = _TotalPlanFinish
                        .Shipment = R!FDShipDate.ToString
                        .ShipContinent = R!FTContinentCode.ToString
                        .ShipCountry = R!FTCountryCode.ToString
                        .ShipMode = R!FTShipModeCode.ToString
                        .GraphStartColIdx = Integer.Parse(Val(R!FNStartCol.ToString))
                        .GraphDetailColor = _ListPlanDetalColor
                        .GraphBGImgIdx = Integer.Parse(Val(R!FNGraphBGImgIdx.ToString))
                        .StateLockDate = (R!FTStateLock.ToString = "1")
                        .StateLockDateAt = _SewingStateDate
                        .LearningCurve = R!FTLearningCurve.ToString
                        .SkillByStyle = Val(R!FTSkillByStyle.ToString)
                        .GraphStateNew = False
                        ' .OrderImage = HI.UL.ULImage.ResizeImage(_tmpPic, New Size(32, 32))

                        Try
                            .OrderImage = HI.UL.ULImage.ResizeImage(_tmpPic, New Size(32, 32))
                        Catch ex As Exception
                            .OrderImage = Nothing
                        End Try


                        .GraphStateDelete = (R!FTStateDelete.ToString = "1")

                        If _DtTimeAdd Is Nothing Then
                            .GraphDataWorkTime = Nothing
                        Else
                            .GraphDataWorkTime = _DtTimeAdd.Copy
                        End If

                    End With

                    If Not (_DtTimeAdd Is Nothing) Then
                        _DtTimeAdd.Dispose()
                    End If


                    Me._dtGraph.Rows.Add(_GraphObject.GraphUserData, _SewingLineIndx, _FNHSysUnitSectId, _OrderNo, R!FTSubOrderNo.ToString, _GraphObject.GraphStartColIdx, 999, "", "", _GraphObject)

                Next

                For I As Integer = StartFixedRow To .Rows - 1
                    Call CreateSewingPlaing(I, True)

                    'Dim _Thread As New System.Threading.Thread(AddressOf CreateSewingPlaing)
                    '_Thread.Start({I, True})
                Next

            End If

            Try
                _dtplan.Dispose()
                _dtplanDate.Dispose()
                _dtplanBreakdown.Dispose()
                _dtplanDateAddTime.Dispose()
            Catch ex As Exception
            End Try

            .AllowMerging = AllowMergingEnum.Free
            .VisualStyle = VisualStyle.Office2007Blue
            .Redraw = RedrawSettings.flexRDBuffered
        End With

    End Sub

    Private Sub InitGridLineHeader(xFNHSysCmpId As Integer)
        Dim I As Integer = 0
        Dim CRange As CellRange
        Dim CRange2 As CellRange
        Dim _StrText As String = ""
        Dim _UserData As String = ""
        Dim _Data As String = ""
        Dim _StartDate As String

        _SysHoliday = Nothing

        With Me.ogvplaning
            .Redraw = RedrawSettings.flexRDNone
            For I = StartFixedCol To .Cols - 1

                _StrText = .get_TextMatrix(2, I)
                _UserData = .GetUserData(2, I)
                _Data = .GetData(2, I)
                _StartDate = _UserData

                Dim _StateDate As StateWorkDate = Me.CheckStateDay(_StartDate, xFNHSysCmpId)

                CRange = .GetCellRange(2, I, 2, I + 7)
                ' CRange = .GetCellRange(2, I, 2, + 7)
                CRange.Clear(ClearFlags.All)

                CRange.StyleNew.UserData = _UserData
                CRange.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                CRange.Data = _Data

                Select Case _StateDate
                    Case StateWorkDate.StateHoliday
                        CRange.StyleNew.ForeColor = Color.Blue
                        CRange.StyleNew.TextEffect = FontStyle.Bold
                        CRange.StyleNew.BackColor = Color.GreenYellow

                    Case StateWorkDate.StateWeekly
                        CRange.StyleNew.ForeColor = Color.Red
                        CRange.StyleNew.TextEffect = FontStyle.Bold
                        CRange.StyleNew.BackColor = Color.GreenYellow
                    Case Else
                        CRange.StyleNew.ForeColor = Color.Black
                        CRange.StyleNew.TextEffect = FontStyle.Regular
                End Select

                _StrText = .get_TextMatrix(3, I)
                _UserData = .GetUserData(3, I)
                _Data = .GetData(3, I)

                CRange = .GetCellRange(3, I, 3, I + 7)
                ' CRange = .GetCellRange(3, I, 3, I + 7)

                CRange.Clear(ClearFlags.All)

                CRange.StyleNew.UserData = _UserData
                CRange.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                CRange.Data = _Data

                Select Case _StateDate
                    Case StateWorkDate.StateHoliday

                        CRange.StyleNew.ForeColor = Color.Blue
                        CRange.StyleNew.TextEffect = FontStyle.Bold
                        CRange.StyleNew.BackColor = Color.GreenYellow

                        'Try
                        '    CRange2 = .GetCellRange(StartFixedRow, I, .Rows - 1, I + 7)
                        '    CRange2.Clear(ClearFlags.All)
                        '    CRange2.StyleNew.BackColor = Color.GreenYellow
                        'Catch ex As Exception
                        'End Try

                    Case StateWorkDate.StateWeekly
                        CRange.StyleNew.ForeColor = Color.Red
                        CRange.StyleNew.TextEffect = FontStyle.Bold
                        CRange.StyleNew.BackColor = Color.GreenYellow

                        'Try
                        '    CRange2 = .GetCellRange(StartFixedRow, I, .Rows - 1, I + 7)
                        '    CRange2.Clear(ClearFlags.All)
                        '    CRange2.StyleNew.BackColor = Color.GreenYellow
                        'Catch ex As Exception
                        'End Try
                       
                    Case Else

                        CRange.StyleNew.ForeColor = Color.Black
                        CRange.StyleNew.TextEffect = FontStyle.Regular

                End Select

                'CRange = .GetCellRange(5, I, .Rows - 1, I + 7)

                'Select Case _StateDate
                '    Case StateWorkDate.StateHoliday
                '        CRange.StyleNew.BackColor = Color.GreenYellow
                '    Case StateWorkDate.StateWeekly
                '        CRange.StyleNew.BackColor = Color.GreenYellow
                'End Select

                I = I + 7

            Next
            .Redraw = RedrawSettings.flexRDBuffered
        End With
    End Sub

    Private Sub InitGridProductionSewingLine(xFNHSysCmpId As Integer)
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "   Select A.FTCmpCode"
        _Qry &= vbCrLf & "   , A.FPCmpImage"
        _Qry &= vbCrLf & "   , B.FTUnitSectCode"
        _Qry &= vbCrLf & "  ,B.FNHSysUnitSectId,B.FNTotalEmp,B.FNWorkTimeMin,B.FNLostTimeMin"
        _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS A WITH(NOLOCK) "
        _Qry &= vbCrLf & "  INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTCmpSewingLine AS B WITH(NOLOCK) ON A.FNHSysCmpId=B.FNHSysCmpId "
        _Qry &= vbCrLf & "  WHERE  (A.FNHSysCmpId =" & xFNHSysCmpId & ") "
        _Qry &= vbCrLf & " ORDER BY A.FTCmpCode,B.FTUnitSectCode"
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_MASTER)

        Dim _TotalRow As Integer = 0
        Dim _SratRow As Integer = 0
        Dim _StrText As String = ""
        Dim _UserData As String = ""
        Dim _Data As String = ""

        With Me.ogvplaning

            If .Rows > StartFixedRow Then
                .Redraw = RedrawSettings.flexRDNone
                Dim CRange2 As CellRange = .GetCellRange(StartFixedRow, 0, .Rows - 1, .Cols - 1)
                CRange2.Clear(ClearFlags.All)
                .Rows = StartFixedRow
                .Redraw = RedrawSettings.flexRDBuffered
            End If

            .Redraw = RedrawSettings.flexRDNone

            _SratRow = .Rows
            _TotalRow = 0

            If _dt.Rows.Count > 0 Then

                For Each R As DataRow In _dt.Rows
                    .Rows = .Rows + 1
                    _TotalRow = _TotalRow + 1

                    .set_TextMatrix(.Rows - 1, 0, R!FTCmpCode.ToString)
                    .SetUserData(.Rows - 1, 0, R!FTCmpCode.ToString)
                    .SetData(.Rows - 1, 0, R!FTCmpCode.ToString)

                    .set_TextMatrix(.Rows - 1, 1, R!FTUnitSectCode.ToString)
                    .SetUserData(.Rows - 1, 1, R!FNHSysUnitSectId.ToString)
                    .SetData(.Rows - 1, 1, R!FTUnitSectCode.ToString)

                    .set_TextMatrix(.Rows - 1, 2, R!FNTotalEmp.ToString)
                    .SetUserData(.Rows - 1, 2, R!FNTotalEmp.ToString)
                    .SetData(.Rows - 1, 2, R!FNTotalEmp.ToString)

                    .set_TextMatrix(.Rows - 1, 3, R!FNWorkTimeMIn.ToString)
                    .SetUserData(.Rows - 1, 3, R!FNWorkTimeMIn.ToString)
                    .SetData(.Rows - 1, 3, R!FNWorkTimeMIn.ToString)

                    .set_TextMatrix(.Rows - 1, 4, R!FNLostTimeMIn.ToString)
                    .SetUserData(.Rows - 1, 4, R!FNLostTimeMIn.ToString)
                    .SetData(.Rows - 1, 4, R!FNLostTimeMIn.ToString)

                    _dtSewingLine.Rows.Add(.Rows - 1, Integer.Parse(Val(R!FNHSysUnitSectId)), R!FTUnitSectCode.ToString, Val(R!FNTotalEmp.ToString), Val(R!FNWorkTimeMin.ToString), Val(R!FNLostTimeMin.ToString))

                Next

                Dim CRange As CellRange = .GetCellRange(_SratRow, 0, (_SratRow + _TotalRow) - 1, 0)

                _StrText = .get_TextMatrix((_SratRow + _TotalRow) - 1, 0)
                _UserData = .GetUserData((_SratRow + _TotalRow) - 1, 0)
                _Data = .GetData((_SratRow + _TotalRow) - 1, 0)

                CRange.Clear(ClearFlags.All)

                CRange.StyleNew.UserData = _UserData
                CRange.StyleNew.TextAlign = TextAlignEnum.CenterCenter
                CRange.Data = _Data
                CRange.StyleNew.Font = New Font("Tahoma", 15, FontStyle.Bold)

                Try
                    CRange.StyleNew.ImageAlign = ImageAlignEnum.Scale
                    CRange.Image = HI.UL.ULImage.ConvertByteArrayToImmage(HI.UL.ULImage.ConvertImageToByteArray(HI.UL.ULImage.ConvertByteArrayToImmage(_dt.Rows(0)!FPCmpImage), New Size(50, 50)))
                Catch ex As Exception
                End Try

                CRange.StyleNew.Display = DisplayEnum.Overlay

            End If

            For I As Integer = 0 To StartFixedCol - 1
                .set_MergeCol(I, True)
            Next

            .MergeCells = MergeSettings.flexMergeFixedOnly
            .Redraw = RedrawSettings.flexRDBuffered
        End With
    End Sub

    Private Delegate Sub DelegateCreateSewingPlaing(xRowLineIdx As Integer, StateBySaveData As Boolean)
    Private Sub CreateSewingPlaing(xRowLineIdx As Integer, Optional StateBySaveData As Boolean = False)

        If Me.InvokeRequired() Then
            Me.Invoke(New DelegateCreateSewingPlaing(AddressOf CreateSewingPlaing), New Object() {xRowLineIdx, StateBySaveData})
        Else
            Dim _r As Integer = 2
            Dim _TotalData As Integer = 0
            Dim _StartCol As Integer = StartFixedCol

            For Each R As DataRow In _dtGraph.Select("FNRowIdx=" & xRowLineIdx & "", "FNStartCol")
                _TotalData = _TotalData + 1

                Dim _ObjGraphData As GraphObject = R!FOGraph
                Dim _ObjGraph As GraphObject = CalCulateGraph(Integer.Parse(Val(R!FNHSysUnitSectId)), _ObjGraphData.TotalEmp, _ObjGraphData.WorkTimeMin, _ObjGraphData.LostTimeMin, _StartCol, R!FOGraph)

                If Not (_ObjGraph Is Nothing) Then

                End If
                _StartCol = _ObjGraph.GraphToColIdx + 1
                R!FNStartCol = _ObjGraph.GraphStartColIdx
                R!FNEndCol = _ObjGraph.GraphToColIdx
                R!FTStartDate = _ObjGraph.GraphSDate
                R!FTEndDate = _ObjGraph.GraphEDate
                R!FOGraph = _ObjGraph

            Next

            Dim CR As New CellRange
            Dim Rind As Integer = 0
            Dim ColIdx As Integer = 0
            Dim ColIdxTo As Integer = 0
            Dim CRange As CellRange
            Dim _StrText As String = ""

            If _TotalData <= 0 Then

                With Me.ogvplaning

                    If xRowLineIdx < StartFixedRow Then
                        Exit Sub
                    End If

                    If (StateBySaveData = False) Then
                        .Redraw = RedrawSettings.flexRDNone
                    End If


                    CR = .GetCellRange(xRowLineIdx, StartFixedCol, xRowLineIdx, .Cols - 1)
                    CR.Clear(ClearFlags.All)

                    .set_MergeRow(xRowLineIdx, True)

                    'For I = StartFixedCol To .Cols - 1
                    '    _StrText = .get_TextMatrix(xRowLineIdx, I)

                    '    If _StrText = "" Then
                    '        CR = .GetCellRange(xRowLineIdx, I)

                    '        If CR.Style.BackColor = Color.GreenYellow Then
                    '            CRange = .GetCellRange(xRowLineIdx, I)
                    '            CRange.StyleNew.BackColor = Color.GreenYellow
                    '        End If
                    '    End If
                    'Next
                    If (StateBySaveData = False) Then
                        .AllowMerging = AllowMergingEnum.Free
                        .VisualStyle = VisualStyle.Office2007Blue
                        .Redraw = RedrawSettings.flexRDBuffered
                    End If

                End With

                Exit Sub
            End If

            Try

                With Me.ogvplaning

                    If .Rows <= StartFixedRow Then
                        Exit Sub
                    End If

                    If (StateBySaveData = False) Then
                        .Redraw = RedrawSettings.flexRDNone
                    End If

                    CR = .GetCellRange(xRowLineIdx, StartFixedCol, xRowLineIdx, .Cols - 1)
                    CR.Clear(ClearFlags.All)

                    For Each R As DataRow In _dtGraph.Select("FNRowIdx=" & xRowLineIdx & "", "FNStartCol")
                        Dim _GraphObject As GraphObject = R!FOGraph

                        Rind = xRowLineIdx
                        ColIdx = _GraphObject.GraphStartColIdx
                        ColIdxTo = _GraphObject.GraphToColIdx

                        CR = .GetCellRange(Rind, ColIdx, Rind, ColIdxTo)
                        CR.Clear(ClearFlags.All)

                        CR.UserData = _GraphObject.GraphUserData
                        CR.Data = _GraphObject.GraphNo & " - FO No : " & _GraphObject.GraphData
                        CR.StyleNew.ForeColor = Color.Blue
                        CR.StyleNew.BackColor = Color.Transparent

                        If _GraphObject.StateLockDate Then
                            CR.StyleNew.Border.Color = Color.Red
                        Else
                            CR.StyleNew.Border.Color = Color.Black
                        End If

                        'CR.StyleNew.Border.Color = Color.OrangeRed

                        CR.StyleNew.Border.Style = BorderStyleEnum.Flat
                        CR.StyleNew.Border.Width = 1.2

                        Dim tPathImgDis As String = _SysPathImageSystem & "\" & "AppIcon.png"

                        Try
                            If _GraphObject.GraphEDate >= HI.UL.ULDate.ConvertEnDB(_GraphObject.Shipment) Then
                                CR.StyleNew.BackgroundImage = ImgBarbgAlert.Images(_GraphObject.GraphToColIdx)
                            Else
                                CR.StyleNew.BackgroundImage = ImgBarbg.Images(_GraphObject.GraphToColIdx)
                            End If

                        Catch ex As Exception
                            CR.StyleNew.BackgroundImage = ImgBarbg.Images(0)
                        End Try

                        CR.StyleNew.BackgroundImageLayout = ImageAlignEnum.Stretch
                        CR.StyleNew.TextAlign = TextAlignEnum.LeftCenter

                        ' CR.StyleNew.Display = DisplayEnum.Stack

                        If ColIdxTo - ColIdx > 10 Then

                            Try

                                CR.StyleNew.ImageAlign = ImageAlignEnum.LeftCenter
                                CR.Image = _GraphObject.OrderImage
                                CR.StyleNew.ImageSpacing = 50

                            Catch ex As Exception
                                CR.Image = Nothing
                            End Try

                        End If

                    Next

                    .set_MergeRow(xRowLineIdx, True)

                    If (StateBySaveData = False) Then
                        .AllowMerging = AllowMergingEnum.Free
                        .VisualStyle = VisualStyle.Office2007Blue
                        .Redraw = RedrawSettings.flexRDBuffered
                        '.DrawMode = DrawModeEnum.OwnerDraw
                    End If

                End With

            Catch ex As System.Exception
            End Try
        End If
    End Sub

    Private Function CalCulateGraph(FNHSysUnitSectId As Integer, WorkEmp As Integer, WorkTimeMinute As Integer, LostTimeMinute As Integer, StartCol As Integer, ObjGraph As GraphObject) As GraphObject

        Dim _Sam As Double = 0
        Dim _SDateOrg As String = ""
        Dim _GraphEndDate As String = ""
        Dim _SDate As String = ""
        Dim _EDate As String = ""
        Dim _SGraphDate As String = ""
        Dim _EGraphDate As String = ""
        Dim _StartHour As Integer = 0
        Dim _EndHour As Integer = 0
        Dim _TotalHour As Integer = 0
        Dim _TotalCol As Integer = 0
        Dim _TotalDay As Integer = 0
        Dim _TotalHoliDay As Integer = 0
        Dim _StateDate As StateWorkDate
        Dim _TotalWeekDay As Integer = 0
        Dim _TotalPlanQty As Integer = 0
        Dim _CapacityPerDay As Integer = 0

        Dim _PlanPerDay As Integer = 0
        Dim _PlanWorkHour As Integer = 0
        Dim _LearningCurve As Double = 1
        Dim SkillByStyle As Double = 1

        Dim _GraphCapacityPerDay As Integer = 0
        Dim _GraphWorkEmp As Integer = 0
        Dim _GraphWorkTimeMinute As Integer = 0
        Dim _GraphLostTimeMinute As Integer = 0

        With ObjGraph
            _Sam = .SamData
            _TotalPlanQty = (.TotalPlan - .TotalFinish)

            If StartCol < .GraphStartColIdx And .StateLockDate Then
                StartCol = .GraphStartColIdx
            End If

        End With

        If Not (ObjGraph.GraphDataWorkTime Is Nothing) Then
            If ObjGraph.GraphDataWorkTime.Rows.Count > 0 Then

                ObjGraph.GraphDataWorkTime.BeginInit()

                For Each Rx As DataRow In ObjGraph.GraphDataWorkTime.Select("FNHSysUnitSectId<>" & FNHSysUnitSectId & "")
                    Rx.Delete()
                Next

                ObjGraph.GraphDataWorkTime.EndInit()
            End If
        End If

        Try
            SkillByStyle = CDbl(Format(ObjGraph.SkillByStyle / 100.0, "0.00"))
        Catch ex As Exception
            ObjGraph.SkillByStyle = 100
        End Try


        If SkillByStyle <= 0 Then
            SkillByStyle = 1
        End If

        If _LearningCurve <= 0 Then
            _LearningCurve = 1
        End If


        WorkEmp = 28
        WorkTimeMinute = 480
        LostTimeMinute = 0

        _CapacityPerDay = Math.Floor((((WorkEmp * (WorkTimeMinute - LostTimeMinute)) / _Sam)) * _LearningCurve * SkillByStyle)

        With Me.ogvplaning

            If StartCol >= .Cols - 1 Or StartCol < StartFixedCol Then
                Return ObjGraph
            End If

            'ObjGraph.
            _SDate = HI.UL.ULDate.ConvertEnDB(.GetUserData(4, StartCol).ToString)

            _StateDate = Me.CheckStateDay(_SDate, Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)))

            While _StateDate <> StateWorkDate.StateNormal
                StartCol = StartCol + 1

                _SDate = HI.UL.ULDate.ConvertEnDB(.GetUserData(4, StartCol).ToString)

                _StateDate = Me.CheckStateDay(_SDate, Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)))
            End While

            _EGraphDate = HI.UL.ULDate.ConvertEnDB(.GetUserData(4, .Cols - 1).ToString)
            _SGraphDate = HI.UL.ULDate.ConvertEnDB(.GetUserData(4, 3).ToString)
            _SDateOrg = _SDate

            Dim _Gsh As Integer = 0

            For Sh As Integer = StartCol To StartFixedCol Step -1

                _Gsh = _Gsh + 1

                If HI.UL.ULDate.ConvertEnDB(.GetUserData(4, Sh).ToString) < _SDate Then
                    Exit For
                End If

            Next

            _StartHour = 1

            Dim _listData As New List(Of GraphObjectDetail)
            _TotalHoliDay = 0
            _TotalWeekDay = 0
            _TotalDay = 0
            _TotalHour = 0
            _TotalCol = 0

            Do While _SDate <= _EGraphDate And _TotalPlanQty > 0

                _StateDate = Me.CheckStateDay(_SDate, Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)))

                If Not (ObjGraph.GraphDataWorkTime Is Nothing) Then
                    If ObjGraph.GraphDataWorkTime.Rows.Count > 0 Then

                        If ObjGraph.GraphDataWorkTime.Select("FTDate='" & _SDate & "'").Length > 0 Then
                            For Each Rx As DataRow In ObjGraph.GraphDataWorkTime.Select("FTDate='" & _SDate & "'")
                                _GraphWorkEmp = Integer.Parse(Val(Rx!FNTotalEmp.ToString))
                                _GraphWorkTimeMinute = Integer.Parse(Val(Rx!FNWorkTimeMin.ToString))
                                _GraphLostTimeMinute = Integer.Parse(Val(Rx!FNLostTimeMin.ToString))
                                _GraphCapacityPerDay = Math.Floor((((_GraphWorkEmp * (_GraphWorkTimeMinute - _GraphLostTimeMinute)) / _Sam)) * _LearningCurve)
                                Exit For
                            Next
                        Else
                            _GraphWorkEmp = WorkEmp
                            _GraphWorkTimeMinute = WorkTimeMinute
                            _GraphLostTimeMinute = LostTimeMinute
                            _GraphCapacityPerDay = _CapacityPerDay
                        End If
                    Else
                        _GraphWorkEmp = WorkEmp
                        _GraphWorkTimeMinute = WorkTimeMinute
                        _GraphLostTimeMinute = LostTimeMinute
                        _GraphCapacityPerDay = _CapacityPerDay
                    End If
                Else
                    _GraphWorkEmp = WorkEmp
                    _GraphWorkTimeMinute = WorkTimeMinute
                    _GraphLostTimeMinute = LostTimeMinute
                    _GraphCapacityPerDay = _CapacityPerDay
                End If

                Select Case _StateDate
                    Case StateWorkDate.StateNormal
                        _PlanPerDay = 0
                        _PlanWorkHour = 8

                        If _SDate = _SDateOrg Then

                            If _Gsh = 1 Then
                                _PlanPerDay = _GraphCapacityPerDay
                            Else
                                _PlanPerDay = (_GraphCapacityPerDay * (8 - _Gsh)) / 8
                                _PlanWorkHour = (8 - _Gsh)
                            End If

                            If _TotalPlanQty <= _PlanPerDay Then

                                _PlanWorkHour = Math.Ceiling(((_TotalPlanQty / _PlanPerDay) * (8 - _Gsh)))

                                If _PlanWorkHour <= 0 Then
                                    _PlanWorkHour = 1
                                End If

                                _PlanPerDay = _TotalPlanQty

                            End If

                        Else

                            If _TotalPlanQty <= _GraphCapacityPerDay Then
                                _PlanWorkHour = Math.Ceiling(((_TotalPlanQty / _GraphCapacityPerDay) * 8))
                                _PlanPerDay = _TotalPlanQty
                            Else
                                _PlanPerDay = _GraphCapacityPerDay
                            End If

                        End If

                        _TotalHour = _TotalHour + _PlanWorkHour
                        _TotalDay = _TotalDay + 1
                        _TotalCol = _TotalCol + _PlanWorkHour

                        _TotalPlanQty = _TotalPlanQty - _PlanPerDay

                        If _TotalPlanQty <= 0 Then
                            _TotalPlanQty = 0
                        End If

                        Dim _GraphObjectDetail As New GraphObjectDetail
                        With _GraphObjectDetail
                            .GraphNo = ObjGraph.GraphNo
                            .GraphDate = _SDate
                            .TotalEmp = _GraphWorkEmp
                            .WorkTimeMin = _GraphWorkTimeMinute
                            .LostTimeMin = _GraphLostTimeMinute
                            .TotalHour = _PlanWorkHour
                            .TotalPlanQty = _PlanPerDay
                            .FinishQty = 0
                        End With

                        _listData.Add(_GraphObjectDetail)

                    Case StateWorkDate.StateHoliday

                        _TotalHoliDay = _TotalHoliDay + 1
                        _TotalCol = _TotalCol + 8

                    Case StateWorkDate.StateWeekly

                        _TotalWeekDay = _TotalWeekDay + 1
                        _TotalCol = _TotalCol + 8

                End Select

                _SDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_SDate, 1))

            Loop

            _GraphEndDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_SDate, -1))
            If Not (ObjGraph.GraphDataWorkTime Is Nothing) Then
                If ObjGraph.GraphDataWorkTime.Rows.Count > 0 Then

                    ObjGraph.GraphDataWorkTime.BeginInit()

                    For Each Rx As DataRow In ObjGraph.GraphDataWorkTime.Select("FTDate<'" & _SDateOrg & "'")
                        Rx.Delete()
                    Next

                    For Each Rx As DataRow In ObjGraph.GraphDataWorkTime.Select("FTDate>'" & _GraphEndDate & "'")
                        Rx.Delete()
                    Next

                    ObjGraph.GraphDataWorkTime.EndInit()

                End If
            End If

            With ObjGraph
                .FNHSysUnitSectId = FNHSysUnitSectId
                .GraphStartColIdx = StartCol
                .GraphToColIdx = StartCol + _TotalCol
                .GraphToTalCol = _TotalCol
                .GraphSDate = _SDateOrg
                .GraphEDate = _GraphEndDate
                .TotalEmp = WorkEmp
                .WorkTimeMin = WorkTimeMinute
                .LostTimeMin = LostTimeMinute
                .CapacityPerDay = _CapacityPerDay
                .TotalDay = _TotalDay
                .TotalHoliDay = _TotalHoliDay
                .TotalWeekDay = _TotalWeekDay
                .TotalHour = _TotalHour
                .GraphDetail = _listData

            End With

        End With

        Return ObjGraph

    End Function

    Private Function SaveDataPlan() As Boolean


        Dim _Spls As New HI.TL.SplashScreen("Saving... Plan ,Please wait.....")

        Dim _Qry As String = ""
        Dim _GraphNo As String = ""
        Dim _Sam As Double = 0
        HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PLANNING)
        HI.Conn.SQLConn.SqlConnectionOpen()
        HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
        HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

        Try

            For Each R As DataRow In Me._dtGraph.Rows
                Dim _ObjGraphData As GraphObject = R!FOGraph
                With _ObjGraphData
                    If .GraphStateNew = True Then
                        .GraphNo = HI.TL.Document.GetDocumentNoOnBeginTrans("HITECH_PLANNING", "TPPCTPlan", "", False)
                    End If

                    _GraphNo = .GraphNo
                    _Sam = .SamData

                    If _GraphNo = "" Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        _Spls.Close()
                        Return False
                    End If


                    _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPlan SET "
                    _Qry &= vbCrLf & "  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                    _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                    _Qry &= vbCrLf & " , FNHSysCmpId=" & Val(FNHSysCmpId.Properties.Tag.ToString) & ""
                    _Qry &= vbCrLf & " , FTOrderNo='" & HI.UL.ULF.rpQuoted(.OrderNo) & "'"
                    _Qry &= vbCrLf & " , FTSubOrderNo='" & HI.UL.ULF.rpQuoted(.OrderSubNo) & "'"
                    _Qry &= vbCrLf & " , FNHSysUnitSectId=" & .FNHSysUnitSectId & ""
                    _Qry &= vbCrLf & " , FNTotalEmp=" & .TotalEmp & ""
                    _Qry &= vbCrLf & " , FNWorkTimeMin=" & .WorkTimeMin & ""
                    _Qry &= vbCrLf & " , FNLostTimeMin=" & .LostTimeMin & ""
                    _Qry &= vbCrLf & " , FNSam=" & .SamData & ""
                    _Qry &= vbCrLf & " , FNCapacityPerDay=" & .CapacityPerDay & ""
                    _Qry &= vbCrLf & " , FTStartDate='" & HI.UL.ULDate.ConvertEnDB(.GraphSDate) & "'"
                    _Qry &= vbCrLf & " , FTEndDate='" & HI.UL.ULDate.ConvertEnDB(.GraphEDate) & "'"
                    _Qry &= vbCrLf & " , FNStartCol=" & .GraphStartColIdx & ""
                    _Qry &= vbCrLf & " , FNEndCol=" & .GraphToColIdx & ""
                    _Qry &= vbCrLf & " , FNTotalDay=" & .TotalDay & ""
                    _Qry &= vbCrLf & " , FNTotalHour=" & .TotalHour & ""
                    _Qry &= vbCrLf & " , FNTotalHoliDay=" & .TotalHoliDay & ""
                    _Qry &= vbCrLf & " , FNTotalWeekDay=" & .TotalWeekDay & ""
                    _Qry &= vbCrLf & " , FNTotalPlan=" & .TotalPlan & ""
                    _Qry &= vbCrLf & " , FNTotalPlanFinish=" & .TotalFinish & ""
                    _Qry &= vbCrLf & " , FTStateLock='" & IIf(.StateLockDate, "1", "0") & "'"
                    _Qry &= vbCrLf & " , FTStateLockAtDate='" & HI.UL.ULDate.ConvertEnDB(.StateLockDateAt) & "'"
                    _Qry &= vbCrLf & " , FNGraphBGImgIdx=" & .GraphBGImgIdx & ""
                    _Qry &= vbCrLf & " , FTStateDelete='" & IIf(.GraphStateDelete, "1", "0") & "'"
                    _Qry &= vbCrLf & " , FTLearningCurve='" & HI.UL.ULF.rpQuoted(.LearningCurve) & "'"
                    _Qry &= vbCrLf & " , FTSkillByStyle='" & HI.UL.ULF.rpQuoted(.SkillByStyle) & "'"
                    _Qry &= vbCrLf & " WHERE FTGraphNo ='" & HI.UL.ULF.rpQuoted(.GraphNo) & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPlan ( "
                        _Qry &= vbCrLf & "  FTInsUser, FDInsDate, FTInsTime, FTGraphNo, FNHSysCmpId, FTOrderNo, FTSubOrderNo"
                        _Qry &= vbCrLf & " , FNHSysUnitSectId, FNTotalEmp, FNWorkTimeMin, FNLostTimeMin, FNSam,"
                        _Qry &= vbCrLf & " FNCapacityPerDay, FTStartDate, FTEndDate, FNStartCol, FNEndCol, FNTotalDay"
                        _Qry &= vbCrLf & " , FNTotalHour, FNTotalHoliDay, FNTotalWeekDay, FNTotalPlan"
                        _Qry &= vbCrLf & " , FNTotalPlanFinish, FTStateLock, FTStateLockAtDate, FNGraphBGImgIdx,FTStateDelete,FTLearningCurve,FTSkillByStyle"
                        _Qry &= vbCrLf & " )"
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                        _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(.GraphNo) & "'"
                        _Qry &= vbCrLf & " , " & Val(FNHSysCmpId.Properties.Tag.ToString) & ""
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(.OrderNo) & "'"
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(.OrderSubNo) & "'"
                        _Qry &= vbCrLf & " ," & .FNHSysUnitSectId & ""
                        _Qry &= vbCrLf & " ," & .TotalEmp & ""
                        _Qry &= vbCrLf & " , " & .WorkTimeMin & ""
                        _Qry &= vbCrLf & " ," & .LostTimeMin & ""
                        _Qry &= vbCrLf & " , " & .SamData & ""
                        _Qry &= vbCrLf & " ," & .CapacityPerDay & ""
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(.GraphSDate) & "'"
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(.GraphEDate) & "'"
                        _Qry &= vbCrLf & " ," & .GraphStartColIdx & ""
                        _Qry &= vbCrLf & " ," & .GraphToColIdx & ""
                        _Qry &= vbCrLf & " ," & .TotalDay & ""
                        _Qry &= vbCrLf & " ," & .TotalHour & ""
                        _Qry &= vbCrLf & " ," & .TotalHoliDay & ""
                        _Qry &= vbCrLf & " ," & .TotalWeekDay & ""
                        _Qry &= vbCrLf & " ," & .TotalPlan & ""
                        _Qry &= vbCrLf & " ," & .TotalFinish & ""
                        _Qry &= vbCrLf & " ,'" & IIf(.StateLockDate, "1", "0") & "'"
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(.StateLockDateAt) & "'"
                        _Qry &= vbCrLf & " ," & .GraphBGImgIdx & ""
                        _Qry &= vbCrLf & " ,'" & IIf(.GraphStateDelete, "1", "0") & "'"
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(.LearningCurve) & "'"
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(.SkillByStyle) & "'"

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            _Spls.Close()
                            Return False
                        End If
                    End If

                    _Qry = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPlanDate  "
                    _Qry &= vbCrLf & " WHERE FTGraphNo ='" & HI.UL.ULF.rpQuoted(_GraphNo) & "' "
                    _Qry &= vbCrLf & "  AND ISNULL(FTStateFinishDate,'') <> '1' "
                    _Qry &= vbCrLf & "  AND ISNULL(FNPlanFinishQuantity,0) <=0"
                    HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    _Qry = "DELETE B "
                    _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPlanDate AS A  "
                    _Qry &= vbCrLf & " RIGHT JOIN  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPlanDateAddTime AS B  "
                    _Qry &= vbCrLf & " ON A.FTGraphNo = B.FTGraphNo AND A.FTDate = B.FTDate "
                    _Qry &= vbCrLf & " WHERE B.FTGraphNo ='" & HI.UL.ULF.rpQuoted(_GraphNo) & "' "
                    _Qry &= vbCrLf & "  AND ISNULL(A.FTStateFinishDate,'') <> '1' "
                    _Qry &= vbCrLf & "  AND ISNULL(A.FNPlanFinishQuantity,0) <=0"
                    HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    For Indx As Integer = 0 To .GraphDetail.Count - 1
                        With .GraphDetail(Indx)
                            .GraphNo = _GraphNo
                            .SamData = _Sam

                            _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPlanDate SET "
                            _Qry &= vbCrLf & "  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                            _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                            _Qry &= vbCrLf & " , FNTotalEmp=" & .TotalEmp & ""
                            _Qry &= vbCrLf & " , FNWorkTimeMin=" & .WorkTimeMin & ""
                            _Qry &= vbCrLf & " , FNLostTimeMin=" & .LostTimeMin & ""
                            _Qry &= vbCrLf & " , FNSam=" & .SamData & ""
                            _Qry &= vbCrLf & " , FNCapacityPerDay=" & .CapacityPerDay & ""
                            _Qry &= vbCrLf & " , FNPlanQuantity=" & .TotalPlanQty & ""
                            '  _Qry &= vbCrLf & " , FNPlanFinishQuantity=" & .FinishQty & ""
                            _Qry &= vbCrLf & " WHERE FTGraphNo ='" & HI.UL.ULF.rpQuoted(_GraphNo) & "'"
                            _Qry &= vbCrLf & " AND  FTDate ='" & HI.UL.ULDate.ConvertEnDB(.GraphDate) & "'"

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPlanDate ( "
                                _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime,  FTGraphNo, FTDate, FNTotalEmp, FNWorkTimeMin, FNLostTimeMin"
                                _Qry &= vbCrLf & ", FNSam, FNCapacityPerDay, FNPlanQuantity, FNPlanFinishQuantity"
                                _Qry &= vbCrLf & " )"
                                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_GraphNo) & "'"
                                _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(.GraphDate) & "'"
                                _Qry &= vbCrLf & " ," & .TotalEmp & ""
                                _Qry &= vbCrLf & " ," & .WorkTimeMin & ""
                                _Qry &= vbCrLf & " ," & .LostTimeMin & ""
                                _Qry &= vbCrLf & " ," & .SamData & ""
                                _Qry &= vbCrLf & " ," & .CapacityPerDay & ""
                                _Qry &= vbCrLf & " ," & .TotalPlanQty & ""
                                _Qry &= vbCrLf & " ," & .FinishQty & ""

                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    _Spls.Close()

                                    Return False

                                End If

                            End If
                        End With
                    Next

                    For Indx As Integer = 0 To .GraphDetailColor.Count - 1
                        With .GraphDetailColor(Indx)
                            .GraphNo = _GraphNo

                            _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPlanBreakDown SET "
                            _Qry &= vbCrLf & "  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                            _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                            _Qry &= vbCrLf & " , FNQuantity=" & .TotalPlanQty & ""
                            _Qry &= vbCrLf & " , FNFinishQuantity=" & .FinishQty & ""
                            _Qry &= vbCrLf & " WHERE FTGraphNo ='" & HI.UL.ULF.rpQuoted(_GraphNo) & "'"
                            _Qry &= vbCrLf & " AND  FTColorway ='" & HI.UL.ULF.rpQuoted(.ColorWay) & "'"
                            _Qry &= vbCrLf & " AND  FTSizeBreakDown ='" & HI.UL.ULF.rpQuoted(.SizeBreakDown) & "'"

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPlanBreakDown ( "
                                _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime, FTGraphNo, FTColorway, FTSizeBreakDown, FNQuantity, FNFinishQuantity"
                                _Qry &= vbCrLf & " )"
                                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_GraphNo) & "'"
                                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(.ColorWay) & "'"
                                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(.SizeBreakDown) & "'"
                                _Qry &= vbCrLf & " ," & .TotalPlanQty & ""
                                _Qry &= vbCrLf & " ," & .FinishQty & ""

                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    _Spls.Close()
                                    Return False
                                End If

                            End If

                        End With

                    Next


                    If Not (.GraphDataWorkTime Is Nothing) Then
                        For Each Rdx As DataRow In .GraphDataWorkTime.Rows

                            _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPlanDateAddTime SET "
                            _Qry &= vbCrLf & "  FTUpdUser='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB
                            _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB
                            _Qry &= vbCrLf & " , FNHSysUnitSectId=" & Integer.Parse(Val(Rdx!FNHSysUnitSectId.ToString)) & ""
                            _Qry &= vbCrLf & " , FNTotalEmp=" & Integer.Parse(Val(Rdx!FNTotalEmp.ToString)) & ""
                            _Qry &= vbCrLf & " , FNWorkTimeMin=" & Integer.Parse(Val(Rdx!FNWorkTimeMin.ToString)) & ""
                            _Qry &= vbCrLf & " , FNLostTimeMin=" & Integer.Parse(Val(Rdx!FNLostTimeMin.ToString)) & ""
                            _Qry &= vbCrLf & " WHERE FTGraphNo ='" & HI.UL.ULF.rpQuoted(_GraphNo) & "'"
                            _Qry &= vbCrLf & " AND  FTDate ='" & HI.UL.ULDate.ConvertEnDB(Rdx!FTDate.ToString) & "'"

                            If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                                _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPlanDateAddTime ( "
                                _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime,  FTGraphNo, FTDate,FNHSysUnitSectId, FNTotalEmp, FNWorkTimeMin, FNLostTimeMin"
                                _Qry &= vbCrLf & ", FNSam, FNCapacityPerDay, FNPlanQuantity, FNPlanFinishQuantity"
                                _Qry &= vbCrLf & " )"
                                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB
                                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB
                                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_GraphNo) & "'"
                                _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(Rdx!FTDate.ToString) & "'"
                                _Qry &= vbCrLf & " , " & Integer.Parse(Val(Rdx!FNHSysUnitSectId.ToString)) & ""
                                _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rdx!FNTotalEmp.ToString)) & ""
                                _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rdx!FNWorkTimeMin.ToString)) & ""
                                _Qry &= vbCrLf & " ," & Integer.Parse(Val(Rdx!FNLostTimeMin.ToString)) & ""

                                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                    HI.Conn.SQLConn.Tran.Rollback()
                                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                    _Spls.Close()

                                    Return False
                                End If

                            End If

                        Next
                    Else
                        _Qry = "DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PLANNING) & "].dbo.TPPCTPlanDateAddTime  "
                        _Qry &= vbCrLf & " WHERE FTGraphNo ='" & HI.UL.ULF.rpQuoted(_GraphNo) & "' "
                      
                        HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                    End If

                End With

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            Return False
        End Try

    End Function
#End Region

    Private Sub wPlanningDevelop_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        _StateFormLoad = False

        Me.FNHSysCmpId.Properties.Tag = HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysCmpId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS A WITH(NOLOCK) WHERE FTCmpCode='" & HI.UL.ULF.rpQuoted(FNHSysCmpId.Text) & "'", Conn.DB.DataBaseName.DB_MERCHAN, "")

        If Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)) > 0 Then
            Call InitGridPlanning(Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)))
        Else
            Call InitGridPlanning(0)
        End If

    End Sub

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ogvplaning_DragDrop(sender As Object, e As DragEventArgs) Handles ogvplaning.DragDrop
        Try
            Dim _MouseRow As Integer = -1
            Dim _MouseCol As Integer = -1
            With Me.ogvplaning
                _MouseRow = .MouseRow
                _MouseCol = .MouseCol

                If Me.olbgraphmove.Visible Then
                    If _MouseRow >= .FixedRows And _MouseCol >= .FixedCols And _MouseRow < .Rows And _MouseCol < .Cols Then
                        Dim _Obj As Object = olbgraphmove.Tag
                        Dim _DimDataText As String = ""

                        If Not (_Obj Is Nothing) Then
                            If TypeOf _Obj Is GraphObject Then
                                Dim _GraphObject As GraphObject = CType(_Obj, GraphObject)

                                Dim objdata As String = "" & .get_TextMatrix(_MouseRow, _MouseCol).ToString()
                                Dim NewLineSewing As String = "" & .get_TextMatrix(_MouseRow, 1).ToString()
                                Dim NewLineSewingID As String = "" & .GetUserData(_MouseRow, 1).ToString()
                                Dim _GrphData As String = _GraphObject.GraphNo & " - FO No : " & _GraphObject.GraphData

                                If objdata <> "" And _GrphData <> objdata Then
                                    Dim _Newobjdata As String = "" & .get_TextMatrix(_MouseRow, _MouseCol).ToString()
                                    While _Newobjdata <> "" And objdata = _Newobjdata
                                        _MouseCol = _MouseCol + 1

                                        If _MouseCol > .Cols - 1 Then
                                            e.Effect = DragDropEffects.None

                                            With olbgraphmove
                                                .Text = ""
                                                .Visible = False
                                            End With

                                            Exit Sub
                                        End If

                                        _Newobjdata = "" & .get_TextMatrix(_MouseRow, _MouseCol).ToString()
                                    End While
                                    _MouseCol = _MouseCol - 1
                                Else
                                    If Not (_GraphObject.StateLockDate) Then
                                        If _MouseCol > StartFixedCol Then

                                            If _GrphData = objdata Then
                                                e.Effect = DragDropEffects.None
                                                With olbgraphmove
                                                    .Text = ""
                                                    .Visible = False
                                                End With
                                                Exit Sub
                                            End If


                                            Dim IdxCol As Integer = 0
                                            For IdxCol = _MouseCol To StartFixedCol Step -1
                                                _DimDataText = "" & .get_TextMatrix(_MouseRow, IdxCol).ToString()

                                                If _DimDataText <> "" Then
                                                    IdxCol = IdxCol + 1
                                                    Exit For
                                                End If

                                            Next

                                            _MouseCol = IdxCol
                                        End If
                                    End If
                                End If

                                Dim _FNTotalEmp As Integer = 0
                                Dim _FNWorkTimeMin As Integer = 0
                                Dim _FNLostTimeMin As Integer = 0
                                Dim _SewingLineIndx As Integer = 0
                                Dim _FNHSysUnitSectId As Integer = 0

                                For Each RxLine As DataRow In Me._dtSewingLine.Select("FTUnitSectCode='" & HI.UL.ULF.rpQuoted(NewLineSewing) & "'")

                                    _SewingLineIndx = Integer.Parse(Val(RxLine!FNRowIdx.ToString))
                                    _FNHSysUnitSectId = Integer.Parse(Val(RxLine!FNHSysUnitSectId.ToString))
                                    _FNTotalEmp = Integer.Parse(Val(RxLine!FNTotalEmp.ToString))
                                    _FNWorkTimeMin = Integer.Parse(Val(RxLine!FNWorkTimeMin.ToString))
                                    _FNLostTimeMin = Integer.Parse(Val(RxLine!FNLostTimeMin.ToString))

                                    Exit For
                                Next

                                ' .Redraw = RedrawSettings.flexRDNone

                                Dim _RowInDexOld As Integer = _GraphObject.GraphRowIdx
                                Dim _RowInDexNew As Integer = _MouseRow

                                With _GraphObject
                                    .GraphRowIdx = _MouseRow
                                    .SewingLineNo = NewLineSewing
                                    .GraphStartColIdx = _MouseCol
                                    .GraphToColIdx = _MouseCol + .GraphToTalCol
                                    .GraphToTalCol = .GraphToTalCol
                                    .TotalEmp = _FNTotalEmp
                                    .WorkTimeMin = _FNWorkTimeMin
                                    .LostTimeMin = _FNLostTimeMin
                                End With
                                ' _GraphObject = CalCulateGraph(20, 480, 0, _MouseCol, _GraphObject)

                                Me._dtGraph.BeginInit()
                                For Each R As DataRow In Me._dtGraph.Select("FTGraphNo='" & HI.UL.ULF.rpQuoted(_GraphObject.GraphUserData) & "'")

                                    R!FNRowIdx = _GraphObject.GraphRowIdx
                                    R!FNStartCol = _MouseCol
                                    R!FNHSysUnitSectId = NewLineSewingID
                                    R!FOGraph = _GraphObject

                                Next

                                Me._dtGraph.EndInit()

                                If _RowInDexOld <> _RowInDexNew Then
                                    Call CreateSewingPlaing(_RowInDexOld)
                                End If

                                Call CreateSewingPlaing(_RowInDexNew)

                                '.AllowMerging = AllowMergingEnum.Free
                                '.Redraw = RedrawSettings.flexRDBuffered

                            End If
                        End If
                    End If

                    e.Effect = DragDropEffects.None
                    With olbgraphmove
                        .Text = ""
                        .Visible = False
                    End With

                End If

            End With
        Catch ex As Exception
            e.Effect = DragDropEffects.None
            With olbgraphmove
                .Text = ""
                .Visible = False
            End With

        End Try
    End Sub

    Private Sub ogvplaning_DragOver(sender As Object, e As DragEventArgs) Handles ogvplaning.DragOver
        Try
            Dim _MouseRow As Integer = -1
            Dim _MouseCol As Integer = -1
            With Me.ogvplaning
                _MouseRow = .MouseRow
                _MouseCol = .MouseCol

                If olbgraphmove.Visible Then

                    Dim _ColTop1 As Integer = .get_Cell(CellPropertySettings.flexcpTop, 5, _MouseCol)
                    Dim _ColTop2 As Integer = .get_Cell(CellPropertySettings.flexcpTop, _MouseRow, _MouseCol)

                    Dim _ColLeft1 As Integer = .get_Cell(CellPropertySettings.flexcpLeft, _MouseRow, 3)
                    Dim _ColLeft2 As Integer = .get_Cell(CellPropertySettings.flexcpLeft, _MouseRow, _MouseCol)

                    If _MouseRow >= .FixedRows And _MouseCol >= .FixedCols And _MouseRow < .Rows And _MouseCol < .Cols Then
                        e.Effect = DragDropEffects.Move

                        Dim position As Point = Me.PointToClient(MousePosition)
                        Dim positionScr As Point = Me.PointToScreen(MousePosition)

                        Dim positiontop As Point = lblpositiontop.PointToScreen(lblpositiontop.Location) 'Me.PointToClient(Me.lblpositiontop.Location)
                        Dim positionbuttom As Point = lblpositionButtom.PointToScreen(lblpositiontop.Location) 'Me.PointToClient(Me.lblpositionButtom.Location)


                        Dim X As Integer = position.X + 10 '+ 10
                        Dim Y As Integer = position.Y - 40 '+ 10

                        With olbgraphmove
                            .Location = New Point(X, Y)
                            .BringToFront()
                            .Visible = True
                        End With

                        Dim position22 As Point = .ScrollPosition
                        Dim gX As Integer = position22.X
                        Dim gY As Integer = position22.Y

                        Select Case True
                            Case (positionScr.X < positiontop.X And positionScr.Y < positiontop.Y)

                                gX = (position22.X + (3 * 8))
                                gY = (position22.Y + (3 * 8))

                            Case (positionScr.X > positionbuttom.X And positionScr.Y > positionbuttom.Y)

                                gY = (position22.Y - (3 * 8))
                                gX = (position22.X - (3 * 8))

                            Case (positionScr.X > positionbuttom.X)

                                gX = (position22.X - (3 * 8))

                            Case (positionScr.Y > positionbuttom.Y)

                                gY = (position22.Y - (3 * 8))

                            Case (positionScr.X < positiontop.X)

                                gX = (position22.X + (3 * 8))

                            Case (positionScr.Y < positiontop.Y)

                                gY = (position22.Y + (3 * 8))

                        End Select

                        'If _ScreenHeight - MousePosition.Y < 180 Then
                        '    gY = (position22.Y - (3 * 8))
                        'End If

                        'If _ScreenWidth - MousePosition.X < 200 Then
                        '    gX = (position22.X - (3 * 8))
                        'End If
                        .ScrollPosition = New Point(gX, gY)
                        'ElseIf _MouseCol = 1 And _MouseRow >= .FixedRows And _MouseRow < .Rows And _MouseCol < .Cols Then
                        '    Dim position22 As Point = .ScrollPosition
                        '    Dim gX As Integer = (position22.X + (3 * 8))
                        '    Dim gY As Integer = position22.Y
                        '    .ScrollPosition = New Point(gX, gY)
                        'ElseIf (_MouseRow = 1 Or _MouseRow = 2 Or _MouseRow = 3 Or _MouseRow = 4) And _MouseCol >= .FixedCols And _MouseRow < .Rows And _MouseCol < .Cols Then
                        '    Dim position22 As Point = .ScrollPosition
                        '    Dim gX As Integer = position22.X
                        '    Dim gY As Integer = (position22.Y + (3 * 8))
                        '    .ScrollPosition = New Point(gX, gY)
                    Else

                        With olbgraphmove
                            olbgraphmoveimage.Image = Nothing
                            olbgraphmovedesc.Text = ""
                            .Tag = _GraphObject
                            .BringToFront()
                            .Visible = False
                        End With

                        e.Effect = DragDropEffects.None
                        ogbplanning.DoDragDrop("", DragDropEffects.None)

                    End If
                End If

            End With

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ogvplaning_MouseDown(sender As Object, e As MouseEventArgs) Handles ogvplaning.MouseDown
        Try
            Dim _MouseRow As Integer = -1
            Dim _MouseCol As Integer = -1
            With Me.ogvplaning
                _MouseRow = .MouseRow
                _MouseCol = .MouseCol

                Dim position22 As Point = .ScrollPosition
             
                Select Case e.Button
                    Case System.Windows.Forms.MouseButtons.Left
                        If _MouseRow >= .FixedRows And _MouseCol >= .FixedCols Then

                            Dim _FTGraph As String = .GetUserData(_MouseRow, _MouseCol)
                            Dim _Obj As Object = Nothing

                            For Each R As DataRow In _dtGraph.Select(_GraphNo & "='" & HI.UL.ULF.rpQuoted(_FTGraph) & "'")
                                _Obj = R.Item(_GraphObject)
                                Exit For
                            Next

                            If Not (_Obj Is Nothing) Then
                                If TypeOf _Obj Is GraphObject Then
                                    Dim _GraphObject As GraphObject = CType(_Obj, GraphObject)

                                    Dim position As Point = Me.PointToClient(MousePosition)
                                    Dim X As Integer = position.X + 10 '+ 10
                                    Dim Y As Integer = position.Y - 40 '+ 10

                                    With olbgraphmove
                                        .Location = New Point(X, Y)
                                        olbgraphmoveimage.Image = Nothing
                                        olbgraphmoveimage.Image = ImgBarbg.Images(_GraphObject.GraphBGImgIdx)
                                        olbgraphmovedesc.Text = _GraphObject.GraphNo
                                        .Tag = _GraphObject
                                        .BringToFront()
                                        .Visible = True
                                    End With

                                    .DoDragDrop("", DragDropEffects.Move Or DragDropEffects.Copy)

                                Else

                                    With olbgraphmove
                                        olbgraphmoveimage.Image = Nothing
                                        olbgraphmovedesc.Text = ""
                                        .Tag = _GraphObject
                                        .BringToFront()
                                        .Visible = False
                                    End With

                                End If

                            Else

                                With olbgraphmove
                                    olbgraphmoveimage.Image = Nothing
                                    olbgraphmovedesc.Text = ""
                                    .Tag = _GraphObject
                                    .BringToFront()
                                    .Visible = False
                                End With

                            End If

                        Else

                            With olbgraphmove
                                olbgraphmoveimage.Image = Nothing
                                olbgraphmovedesc.Text = ""
                                .Tag = _GraphObject
                                .BringToFront()
                                .Visible = False
                            End With

                        End If

                    Case System.Windows.Forms.MouseButtons.Right
                        If _MouseRow >= .FixedRows And _MouseCol >= .FixedCols Then

                            Dim _FTGraph As String = .GetUserData(_MouseRow, _MouseCol)
                            Dim _Obj As Object = Nothing

                            For Each R As DataRow In _dtGraph.Select(_GraphNo & "='" & HI.UL.ULF.rpQuoted(_FTGraph) & "'")

                                _Obj = R.Item(_GraphObject)

                                Exit For

                            Next

                            Me.cnuDelJob.Enabled = False
                            Me.cnuRefreshLine.Enabled = False
                            Me.mnuSkillByStyle.Enabled = False
                            Me.mnuLearnningGroup.Enabled = False
                            Me.cnuDelJob.Tag = Nothing
                            Me.mnuSkillByStyle.Tag = Nothing

                            If Not (_Obj Is Nothing) Then
                                If TypeOf _Obj Is GraphObject Then

                                    Dim _GraphObject As GraphObject = CType(_Obj, GraphObject)
                                    Me.cnuDelJob.Enabled = ocmdelete.Enabled
                                    Me.cnuDelJob.Tag = _GraphObject.GraphNo
                                    Me.cnuDelJob.Text = "Delete Graph No " & _GraphObject.GraphNo
                                    mnuplanpopup.Tag = _GraphObject.SewingLineNo


                                    Me.mnuSkillByStyle.Tag = _GraphObject.SewingLineNo
                                    Me.mnuSkillByStyle.Enabled = True
                                End If


                            End If

                            ogvplaning.ContextMenuStrip = mnuplanpopup
                        End If

                End Select

            End With
        Catch ex As Exception

            ogbplanning.DoDragDrop("", DragDropEffects.None)

            With olbgraphmove
                olbgraphmoveimage.Image = Nothing
                olbgraphmovedesc.Text = ""
                .Tag = _GraphObject
                .BringToFront()
                .Visible = False
            End With

        End Try
    End Sub

    Private Sub ogvplaning_MouseLeave(sender As Object, e As EventArgs) Handles ogvplaning.MouseLeave

    End Sub

    Private Sub ogvplaning_MouseUp(sender As Object, e As MouseEventArgs) Handles ogvplaning.MouseUp
        Try

            If e.Button = System.Windows.Forms.MouseButtons.Left Then

                ogbplanning.DoDragDrop("", DragDropEffects.None)

                With olbgraphmove
                    olbgraphmoveimage.Image = Nothing
                    olbgraphmovedesc.Text = ""
                    .Tag = _GraphObject
                    .BringToFront()
                    .Visible = False
                End With
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub ogvplaning_OwnerDrawCell(sender As Object, e As OwnerDrawCellEventArgs) Handles ogvplaning.OwnerDrawCell
    '    If Not ogvplaning.GetUserData(e.Row, e.Col) Is Nothing AndAlso e.Row >= ogvplaning.FixedRows AndAlso e.Col >= ogvplaning.FixedCols Then
    '        Dim value As Double = 80

    '        ' calculate bar extent 
    '        Dim rc As Rectangle = e.Bounds
    '        Dim max As Double = 100

    '        rc.Width = CType((System.Math.Floor(100 * value / max)), Integer)
    '        rc.Height = 8

    '        ' draw background
    '        e.DrawCell(DrawCellFlags.Background Or DrawCellFlags.Border)

    '        ' draw bar
    '        rc.Inflate(-2, -2)
    '        e.Graphics.FillRectangle(Brushes.Gold, rc)
    '        rc.Inflate(-1, -1)
    '        e.Graphics.FillRectangle(Brushes.LightGoldenrodYellow, rc)

    '        ' draw cell content
    '        e.DrawCell(DrawCellFlags.Content)

    '    End If
    'End Sub

    Private Sub FNHSysCmpId_EditValueChanged(sender As Object, e As EventArgs) Handles FNHSysCmpId.EditValueChanged
        If (_StateFormLoad) Then Exit Sub
        Try
            If (Me.InvokeRequired) Then
                Me.Invoke(New HI.Delegate.Dele.ButtonEdit_ValueChanged(AddressOf FNHSysCmpId_EditValueChanged), New Object() {sender, e})
            Else
                If FNHSysCmpId.Text <> "" Then

                    Me.FNHSysCmpId.Properties.Tag = HI.Conn.SQLConn.GetField("SELECT TOP 1 FNHSysCmpId FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp AS A WITH(NOLOCK) WHERE FTCmpCode='" & HI.UL.ULF.rpQuoted(FNHSysCmpId.Text) & "'", Conn.DB.DataBaseName.DB_MERCHAN, "")

                    If Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)) > 0 Then
                        Dim _Spls As New HI.TL.SplashScreen("Loading... Plan ,Please wait.....")
                        Call InitGridPlanning(Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)))
                        _Spls.Close()
                    Else
                        Call InitGridPlanning(0)
                    End If

                Else
                    Call InitGridPlanning(0)
                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        Try
            If Me.FNHSysCmpId.Text <> "" Then
                If Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)) > 0 Then
                    Call InitGridPlanning(Integer.Parse(Val(Me.FNHSysCmpId.Properties.Tag.ToString)))
                Else
                    Call InitGridPlanning(0)
                End If
            Else
                Call InitGridPlanning(0)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ocmadd_Click(sender As Object, e As EventArgs) Handles ocmadd.Click

        Try
            With Me.ogvplaning
                If .Rows <= 5 Then
                    Exit Sub
                End If

                With _wAddPlan
                    .Process = False

                    .ogvsub.OptionsView.ShowAutoFilterRow = False
                    .ogvsuborder.OptionsView.ShowAutoFilterRow = False
                    .ogvsuborderorg.OptionsView.ShowAutoFilterRow = False
                    .ogvsubprod.OptionsView.ShowAutoFilterRow = False

                    .ogvsub.ActiveFilter.Clear()
                    .ogvsuborder.ActiveFilter.Clear()
                    .ogvsuborderorg.ActiveFilter.Clear()
                    .ogvsubprod.ActiveFilter.Clear()

                    .ogcsub.DataSource = Nothing
                    .ogcsuborder.DataSource = Nothing
                    .ogcsuborderorg.DataSource = Nothing
                    .ogcsubprod.DataSource = Nothing
                    .FTStateFixMinStartDate.Checked = False
                    .FTStartPlanDate.Text = HI.UL.ULDate.ConvertEN(HI.ST.UserInfo.LogINDate)
                    .FNHSysUnitSectId.Text = ""
                    .FNHSysCmpId.Text = Me.FNHSysCmpId.Text
                    .FNHSysCmpId.Properties.Tag = Me.FNHSysCmpId.Properties.Tag
                    .FTOrderNo.Text = ""
                    .FNGraphImageIndex.SelectedIndex = 0
                    .ShowDialog()

                    If (.Process) Then

                        Dim _SewingLineNo As String = .FNHSysUnitSectId.Text
                        Dim _SewingStateDate As String = HI.UL.ULDate.ConvertEnDB(.FTStartPlanDate.Text)
                        Dim _SewingStateFixDate As Boolean = .FTStateFixMinStartDate.Checked
                        Dim _SewingLineIndx As Integer = 0
                        Dim _FNHSysUnitSectId As Integer = 0
                        Dim _SamSewing As Double = .FNSam.Value
                        Dim _FNTotalEmp As Integer = 0
                        Dim _FNWorkTimeMin As Integer = 0
                        Dim _FNLostTimeMin As Integer = 0

                        Dim _dtSub As DataTable
                        With CType(.ogcsub.DataSource, DataTable)
                            .AcceptChanges()
                            _dtSub = .Copy()
                        End With

                        For Each RxLine As DataRow In Me._dtSewingLine.Select("FTUnitSectCode='" & HI.UL.ULF.rpQuoted(_SewingLineNo) & "'")

                            _SewingLineIndx = Integer.Parse(Val(RxLine!FNRowIdx.ToString))
                            _FNHSysUnitSectId = Integer.Parse(Val(RxLine!FNHSysUnitSectId.ToString))
                            _FNTotalEmp = Integer.Parse(Val(RxLine!FNTotalEmp.ToString))
                            _FNWorkTimeMin = Integer.Parse(Val(RxLine!FNWorkTimeMin.ToString))
                            _FNLostTimeMin = Integer.Parse(Val(RxLine!FNLostTimeMin.ToString))

                            Exit For
                        Next

                        If _SewingLineIndx > 4 Then

                            Dim _GraphObject As GraphObject
                            _ListDataSubOrder = ._ListDataSubOrder
                            _ListDataSubOrderProd = ._ListDataSubOrderProd

                            Dim _OrderNo As String = ""
                            Dim _Season As String = ""
                            Dim _Style As String = ""
                            Dim _SubOrderNo As String = ""
                            Dim _ColorWay As String = ""
                            Dim _ShipDate As String = ""
                            Dim _Continent As String = ""
                            Dim _Country As String = ""
                            Dim _ShipMode As String = ""

                            Dim _StateCreatGraph As Integer = 0
                            Dim _tmpPic As Image
                            Dim _ListPlanDetal As New List(Of GraphObjectDetail)
                            Dim _ListPlanDetalColor As New List(Of GraphObjectDetailColor)
                            Dim _StateAddGraph As Boolean = False
                            Dim _TotalPlan As Integer = 0
                            Dim _TotalPlanByColor As Integer = 0
                            Dim _GraphBGImgIdx As Integer = 0

                            _StateCreatGraph = .FNCreateGraphProdType.SelectedIndex
                            _GraphBGImgIdx = .FNGraphImageIndex.SelectedIndex
                            _OrderNo = .FTOrderNo.Text.Trim()
                            _tmpPic = .FPImage.Image
                            _Season = .FNHSysSeasonId.Text
                            _Style = .FNHSysStyleId.Text

                            Dim I As Integer = 0
                            Dim _FoundStartCol As Integer = -1

                            With Me.ogvplaning

                                For I = StartFixedCol To .Cols - 1

                                    If _SewingStateDate = .GetUserData(4, I).ToString Then
                                        _FoundStartCol = I
                                        Exit For
                                    End If

                                Next

                            End With

                            If _FoundStartCol = -1 Then
                                Exit Sub
                            End If

                            I = 0

                            Dim _TotalGraph As Integer = (Me._dtGraph.Rows.Count + 1)

                            For Each R As DataRow In _dtSub.Rows

                                If R!FTStateSelect.ToString = "1" Then

                                    _StateAddGraph = True
                                    _SamSewing = Val(R!FNSam.ToString())
                                    _ShipDate = R!FDShipDate.ToString

                                    _Continent = R!FTCountryCode.ToString
                                    _Country = R!FTContinentCode.ToString
                                    _ShipMode = R!FTShipModeCode.ToString

                                    Select Case _StateCreatGraph
                                        Case 0

                                            _TotalPlan = 0
                                            _ListPlanDetalColor = New List(Of GraphObjectDetailColor)
                                            _ListPlanDetal.Clear()
                                            _ListPlanDetalColor.Clear()

                                            For Each Rx As DataRow In _ListDataSubOrder(I).Rows

                                                _SubOrderNo = Rx!FTSubOrderNo.ToString
                                                _ColorWay = Rx!FTColorway.ToString
                                                _TotalPlanByColor = 0


                                                For Each Col As DataColumn In _ListDataSubOrder(I).Columns
                                                    Select Case Col.ColumnName.ToString.ToUpper
                                                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
                                                        Case Else
                                                            If (Integer.Parse(Rx.Item(Col.ColumnName.ToString))) > 0 Then

                                                                _TotalPlan = _TotalPlan + (Integer.Parse(Rx.Item(Col.ColumnName.ToString)))
                                                                _TotalPlanByColor = _TotalPlanByColor + (Integer.Parse(Rx.Item(Col.ColumnName.ToString)))

                                                                Dim _GobjDetail As New GraphObjectDetailColor
                                                                With _GobjDetail
                                                                    .GraphNo = ""
                                                                    .ColorWay = _ColorWay
                                                                    .SizeBreakDown = Col.ColumnName.ToString
                                                                    .TotalPlanQty = (Integer.Parse(Rx.Item(Col.ColumnName.ToString)))

                                                                End With

                                                                _ListPlanDetalColor.Add(_GobjDetail)

                                                            End If

                                                    End Select
                                                Next

                                                If _TotalPlanByColor > 0 Then


                                                End If
                                            Next

                                            If _TotalPlan > 0 Then
                                                _StateAddGraph = True
                                                _GraphObject = New GraphObject

                                                With _GraphObject

                                                    .GraphNo = "NEW-" & _TotalGraph.ToString
                                                    .OrderNo = _OrderNo
                                                    .OrderSubNo = R!FTSubOrderNo.ToString
                                                    .GraphData = _OrderNo & " : " & R!FTSubOrderNo.ToString
                                                    .GraphUserData = _TotalGraph
                                                    .StyleNo = _Style
                                                    .Season = _Season
                                                    .SamData = _SamSewing
                                                    .TotalEmp = _FNTotalEmp
                                                    .WorkTimeMin = _FNWorkTimeMin
                                                    .LostTimeMin = _FNLostTimeMin
                                                    .SewingLineNo = _SewingLineNo
                                                    .GraphRowIdx = _SewingLineIndx
                                                    .TotalPlan = _TotalPlan
                                                    .Shipment = _ShipDate
                                                    .ShipContinent = _Continent
                                                    .ShipCountry = _Country
                                                    .ShipMode = _ShipMode
                                                    .GraphStartColIdx = _FoundStartCol + _TotalGraph
                                                    .GraphDetailColor = _ListPlanDetalColor
                                                    .GraphBGImgIdx = _GraphBGImgIdx
                                                    .StateLockDate = _SewingStateFixDate
                                                    .StateLockDateAt = _SewingStateDate
                                                    .GraphStateNew = True
                                                    Try
                                                        .OrderImage = HI.UL.ULImage.ResizeImage(_tmpPic, New Size(32, 32))
                                                    Catch ex As Exception
                                                        .OrderImage = Nothing
                                                    End Try

                                                    _TotalGraph = _TotalGraph + 1
                                                End With

                                                Me._dtGraph.Rows.Add(_GraphObject.GraphUserData, _SewingLineIndx, _FNHSysUnitSectId, _OrderNo, R!FTSubOrderNo.ToString, _GraphObject.GraphStartColIdx, 999, "", "", _GraphObject)

                                            End If

                                        Case 1


                                            For Each Rx As DataRow In _ListDataSubOrder(I).Rows


                                                _TotalPlan = 0
                                                _ListPlanDetalColor = New List(Of GraphObjectDetailColor)
                                                _ListPlanDetal.Clear()
                                                _ListPlanDetalColor.Clear()

                                                _SubOrderNo = Rx!FTSubOrderNo.ToString
                                                _ColorWay = Rx!FTColorway.ToString
                                                _TotalPlanByColor = 0

                                                For Each Col As DataColumn In _ListDataSubOrder(I).Columns
                                                    Select Case Col.ColumnName.ToString.ToUpper
                                                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
                                                        Case Else
                                                            If (Integer.Parse(Rx.Item(Col.ColumnName.ToString))) > 0 Then

                                                                _TotalPlan = _TotalPlan + (Integer.Parse(Rx.Item(Col.ColumnName.ToString)))
                                                                _TotalPlanByColor = _TotalPlanByColor + (Integer.Parse(Rx.Item(Col.ColumnName.ToString)))

                                                                Dim _GobjDetail As New GraphObjectDetailColor
                                                                With _GobjDetail
                                                                    .GraphNo = ""
                                                                    .ColorWay = _ColorWay
                                                                    .SizeBreakDown = Col.ColumnName.ToString
                                                                    .TotalPlanQty = (Integer.Parse(Rx.Item(Col.ColumnName.ToString)))

                                                                End With

                                                                _ListPlanDetalColor.Add(_GobjDetail)

                                                            End If

                                                    End Select
                                                Next

                                                If _TotalPlan > 0 Then
                                                    _StateAddGraph = True
                                                    _GraphObject = New GraphObject

                                                    With _GraphObject

                                                        .GraphNo = "NEW-" & _TotalGraph.ToString
                                                        .OrderNo = _OrderNo
                                                        .OrderSubNo = R!FTSubOrderNo.ToString
                                                        .GraphData = _OrderNo & " : " & R!FTSubOrderNo.ToString
                                                        .GraphUserData = _TotalGraph
                                                        .StyleNo = _Style
                                                        .Season = _Season
                                                        .SamData = _SamSewing
                                                        .TotalEmp = _FNTotalEmp
                                                        .WorkTimeMin = _FNWorkTimeMin
                                                        .LostTimeMin = _FNLostTimeMin
                                                        .SewingLineNo = _SewingLineNo
                                                        .GraphRowIdx = _SewingLineIndx
                                                        .TotalPlan = _TotalPlan
                                                        .Shipment = _ShipDate
                                                        .ShipContinent = _Continent
                                                        .ShipCountry = _Country
                                                        .ShipMode = _ShipMode
                                                        .GraphStartColIdx = _FoundStartCol + _TotalGraph
                                                        .GraphDetailColor = _ListPlanDetalColor
                                                        .GraphBGImgIdx = _GraphBGImgIdx
                                                        .StateLockDate = _SewingStateFixDate
                                                        .StateLockDateAt = _SewingStateDate
                                                        .GraphStateNew = True

                                                        ' .OrderImage = HI.UL.ULImage.ResizeImage(_tmpPic, New Size(32, 32))
                                                        Try
                                                            .OrderImage = HI.UL.ULImage.ResizeImage(_tmpPic, New Size(32, 32))
                                                        Catch ex As Exception
                                                            .OrderImage = Nothing
                                                        End Try

                                                        _TotalGraph = _TotalGraph + 1
                                                    End With

                                                    Me._dtGraph.Rows.Add(_GraphObject.GraphUserData, _SewingLineIndx, _FNHSysUnitSectId, _OrderNo, R!FTSubOrderNo.ToString, _GraphObject.GraphStartColIdx, 999, "", "", _GraphObject)

                                                End If

                                            Next

                                        Case 2

                                            For Each Rx As DataRow In _ListDataSubOrder(I).Rows

                                                _SubOrderNo = Rx!FTSubOrderNo.ToString
                                                _ColorWay = Rx!FTColorway.ToString
                                                _TotalPlanByColor = 0

                                                For Each Col As DataColumn In _ListDataSubOrder(I).Columns
                                                    Select Case Col.ColumnName.ToString.ToUpper
                                                        Case "FTOrderNo".ToUpper, "FTSubOrderNo".ToUpper, "FTColorway".ToUpper, "Total".ToUpper
                                                        Case Else
                                                            If (Integer.Parse(Rx.Item(Col.ColumnName.ToString))) > 0 Then

                                                                _TotalPlan = 0
                                                                _ListPlanDetalColor = New List(Of GraphObjectDetailColor)
                                                                _ListPlanDetal.Clear()
                                                                _ListPlanDetalColor.Clear()

                                                                _TotalPlan = _TotalPlan + (Integer.Parse(Rx.Item(Col.ColumnName.ToString)))
                                                                _TotalPlanByColor = _TotalPlanByColor + (Integer.Parse(Rx.Item(Col.ColumnName.ToString)))

                                                                Dim _GobjDetail As New GraphObjectDetailColor
                                                                With _GobjDetail
                                                                    .GraphNo = ""
                                                                    .ColorWay = _ColorWay
                                                                    .SizeBreakDown = Col.ColumnName.ToString
                                                                    .TotalPlanQty = (Integer.Parse(Rx.Item(Col.ColumnName.ToString)))

                                                                End With

                                                                _ListPlanDetalColor.Add(_GobjDetail)


                                                                If _TotalPlan > 0 Then
                                                                    _StateAddGraph = True
                                                                    _GraphObject = New GraphObject

                                                                    With _GraphObject

                                                                        .GraphNo = "NEW-" & _TotalGraph.ToString
                                                                        .OrderNo = _OrderNo
                                                                        .OrderSubNo = R!FTSubOrderNo.ToString
                                                                        .GraphData = _OrderNo & " : " & R!FTSubOrderNo.ToString
                                                                        .GraphUserData = _TotalGraph
                                                                        .StyleNo = _Style
                                                                        .Season = _Season
                                                                        .SamData = _SamSewing
                                                                        .TotalEmp = _FNTotalEmp
                                                                        .WorkTimeMin = _FNWorkTimeMin
                                                                        .LostTimeMin = _FNLostTimeMin
                                                                        .SewingLineNo = _SewingLineNo
                                                                        .GraphRowIdx = _SewingLineIndx
                                                                        .TotalPlan = _TotalPlan
                                                                        .Shipment = _ShipDate
                                                                        .ShipContinent = _Continent
                                                                        .ShipCountry = _Country
                                                                        .ShipMode = _ShipMode
                                                                        .GraphStartColIdx = _FoundStartCol + _TotalGraph
                                                                        .GraphDetailColor = _ListPlanDetalColor
                                                                        .GraphBGImgIdx = _GraphBGImgIdx
                                                                        .StateLockDate = _SewingStateFixDate
                                                                        .StateLockDateAt = _SewingStateDate

                                                                        '.OrderImage = HI.UL.ULImage.ResizeImage(_tmpPic, New Size(32, 32))
                                                                        Try
                                                                            .OrderImage = HI.UL.ULImage.ResizeImage(_tmpPic, New Size(32, 32))
                                                                        Catch ex As Exception
                                                                            .OrderImage = Nothing
                                                                        End Try

                                                                        .GraphStateNew = True

                                                                        _TotalGraph = _TotalGraph + 1
                                                                    End With

                                                                    Me._dtGraph.Rows.Add(_GraphObject.GraphUserData, _SewingLineIndx, _FNHSysUnitSectId, _OrderNo, R!FTSubOrderNo.ToString, _GraphObject.GraphStartColIdx, 999, "", "", _GraphObject)

                                                                End If

                                                            End If

                                                    End Select
                                                Next


                                            Next

                                    End Select

                                End If
                                I = I + 1

                            Next

                            If _StateAddGraph Then
                                Call CreateSewingPlaing(_SewingLineIndx)
                            End If
                        End If

                    End If

                End With
            End With
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Try

            With Me.ogvplaning
                If .Rows <= 5 Then
                    HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Sewing Line ไม่สามารถทำการบันทึกได้ กรุณาทำการตรวจสอบ !!!", 1511130549, Me.Text, , MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End With

            If Me._dtGraph.Rows.Count <= 0 Then
                HI.MG.ShowMsg.mInfo("ไม่พบข้อมูล Plan ไม่สามารถทำการบันทึกได้ กรุณาทำการตรวจสอบ !!!", 1511130550, Me.Text, , MessageBoxIcon.Warning)
                Exit Sub
            End If

            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave) Then
                If Me.SaveDataPlan() Then
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                    Dim _Spls As New HI.TL.SplashScreen("Loading... Plan ,Please wait.....")

                    Call LoadDataSewingPlanning(Integer.Parse(Val(FNHSysCmpId.Properties.Tag.ToString())))
                    _Spls.Close()

                Else
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub ogvplaning_MouseHover(sender As Object, e As EventArgs) Handles ogvplaning.MouseHover

    End Sub

    Private Sub cnuDelJob_Click(sender As Object, e As EventArgs) Handles cnuDelJob.Click
        If Not (Me.cnuDelJob.Tag Is Nothing) Then
            Dim _FTGraph As String = Me.cnuDelJob.Tag.ToString
            Dim _Obj As Object = Nothing
            Dim _SewingLineNo As String = ""
            _dtGraph.BeginInit()
            For Each R As DataRow In _dtGraph.Select(_GraphNo & "='" & HI.UL.ULF.rpQuoted(_FTGraph) & "'")

                _Obj = R.Item(_GraphObject)

                If Not (_Obj Is Nothing) Then
                    If TypeOf _Obj Is GraphObject Then

                        Dim _GraphObject As GraphObject = CType(_Obj, GraphObject)

                        _SewingLineNo = _GraphObject.SewingLineNo

                    End If

                End If

                R.Delete()
            Next
            _dtGraph.EndInit()


            Dim _SewingLineIndx As Integer = 0
            Dim _FNHSysUnitSectId As Integer = 0

            For Each RxLine As DataRow In Me._dtSewingLine.Select("FTUnitSectCode='" & HI.UL.ULF.rpQuoted(_SewingLineNo) & "'")
                _SewingLineIndx = Integer.Parse(Val(RxLine!FNRowIdx.ToString))
                _FNHSysUnitSectId = Integer.Parse(Val(RxLine!FNHSysUnitSectId.ToString))

                Exit For
            Next


            If _FNHSysUnitSectId > 0 Then
                Call CreateSewingPlaing(_SewingLineIndx)
            End If


        End If
    End Sub

    Private Sub mnuSkillByStyle_Click(sender As Object, e As EventArgs) Handles mnuSkillByStyle.Click

        If Not (Me.mnuSkillByStyle.Tag Is Nothing) Then

            Dim _SewingLineIndx As Integer = 0
            Dim _FNHSysUnitSectId As Integer = 0

            For Each RxLine As DataRow In Me._dtSewingLine.Select("FTUnitSectCode='" & HI.UL.ULF.rpQuoted(Me.mnuSkillByStyle.Tag.ToString) & "'")
                _SewingLineIndx = Integer.Parse(Val(RxLine!FNRowIdx.ToString))
                _FNHSysUnitSectId = Integer.Parse(Val(RxLine!FNHSysUnitSectId.ToString))

                Exit For

            Next

            Dim dt As New DataTable
            dt.Columns.Add("FTSelect", GetType(String))
            dt.Columns.Add("FTGraphNo", GetType(String))
            dt.Columns.Add("FTJobNo", GetType(String))
            dt.Columns.Add("FTSubJobNo", GetType(String))
            dt.Columns.Add("FTStyleNo", GetType(String))
            dt.Columns.Add("FNSkillByStyle", GetType(Double))


            For Each R As DataRow In _dtGraph.Select("FNRowIdx=" & _SewingLineIndx & "", "FNStartCol")
                Dim _ObjGraphData As GraphObject = R!FOGraph

                dt.Rows.Add("0", _ObjGraphData.GraphNo, _ObjGraphData.OrderNo, _ObjGraphData.OrderSubNo, _ObjGraphData.StyleNo, _ObjGraphData.SkillByStyle)

            Next

            With New wAddSkillByStyle
                .ogcdetail.DataSource = dt
                .FTLineNo.Text = Me.mnuSkillByStyle.Tag.ToString
                .FNSkillByStyle.Value = 100.0
                .ShowDialog()

                If .AddSkill Then

                    Dim _Obj As Object = Nothing

                    For Each R As DataRow In dt.Select("FTSelect='1'")


                        For Each Rx As DataRow In _dtGraph.Select(_GraphNo & "='" & HI.UL.ULF.rpQuoted(R!FTGraphNo.ToString) & "'")

                            _Obj = Rx.Item(_GraphObject)

                            If Not (_Obj Is Nothing) Then
                                If TypeOf _Obj Is GraphObject Then

                                    Dim _GraphObject As GraphObject = CType(_Obj, GraphObject)
                                    _GraphObject.SkillByStyle = Val(R!FNSkillByStyle.ToString)


                                End If

                            End If


                        Next

                    Next


                    If _FNHSysUnitSectId > 0 Then
                        Call CreateSewingPlaing(_SewingLineIndx)
                    End If

                End If

            End With


            dt.Dispose()

        End If

    End Sub

End Class