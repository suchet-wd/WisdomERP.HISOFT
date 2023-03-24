Public Class wMoveTeamMoveType
   
    Sub New()
        InitializeComponent()
        _ActualDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

        Call InitGrid()
    End Sub

    Private Function LoadDataInfo(Slps As HI.TL.SplashScreen) As Boolean

        Dim _Dt As DataTable
        Dim _Qry As String = ""
        Me.ogc.DataSource = Nothing
        ochkselectall.Checked = False

        _Qry = "SELECT  '0' AS FTSelect,  M.FNHSysEmpID, M.FTEmpCode,M.FNHSysEmpTypeId,M.FNHSysUnitSectId"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & ",P.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

        Else
            _Qry &= vbCrLf & ",P.FTPreNameNameEN + ' ' +  M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
        End If

        _Qry &= vbCrLf & ",ST.FTSectCode "
        _Qry &= vbCrLf & ",US.FTUnitSectCode AS FTUnitSectCode "
        _Qry &= vbCrLf & ",US.FTUnitSectNameTH AS FTUnitSectName"
        _Qry &= vbCrLf & ",US.FNHSysUnitSectId AS FNHSysUnitSectId "
        _Qry &= vbCrLf & ",ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
        _Qry &= vbCrLf & ",ISNULL(Dept.FTDeptCode,'') AS FTDeptCode "
        _Qry &= vbCrLf & ",ISNULL(DI.FTDivisonCode,'') AS FTDivisonCode "
        _Qry &= vbCrLf & ",ISNULL(EMTS.FTIn1,TS.FTIn1) AS FTIn1 "
        _Qry &= vbCrLf & ",ISNULL(EMTS.FTOut1,TS.FTOut1) AS FTOut1 "
        _Qry &= vbCrLf & ",ISNULL(EMTS.FTIn2,TS.FTIn2) AS FTIn2 "
        _Qry &= vbCrLf & ",ISNULL(EMTS.FTOut2,TS.FTOut2) AS FTOut2 "
        _Qry &= vbCrLf & ",oho.FTEmpTypeNameTH AS FNHSysEmpTypeIdTo"
        _Qry &= vbCrLf & ",oho.FTUnitSectNameTH AS FNHSysUnitSectIdTo"
        _Qry &= vbCrLf & ",oho.FTStartTime AS FTStarttime"
        _Qry &= vbCrLf & ",oho.FTEndTime AS FTEndtime"
        _Qry &= vbCrLf & ",oho.FNTotalMinute AS FNTotalMinute"
        _Qry &= vbCrLf & "  ,ISNULL(oho.FDInsDate,'') AS FDInsDate, ISNULL(oho.FTInsTime,'') AS FTInsTime, ISNULL(oho.FTInsUser,'') AS  FTInsUser, ISNULL(oho.FDUpdDate,'') AS FDUpdDate, ISNULL(oho.FTUpdTime,'') AS FTUpdTime , ISNULL(oho.FTUpdUser,'') AS  FTUpdUser "
        _Qry &= vbCrLf & " FROM      [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M  WITH (NOLOCK) "
        _Qry &= vbCrLf & "INNER JOIN"
        _Qry &= vbCrLf & "[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS TS WITH (NOLOCK) ON M.FNHSysShiftID=TS.FNHSysShiftID LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN("
        _Qry &= vbCrLf & " SELECT MT.FTStartTime,MT.FTEndTime,MT.FNHSysEmpID,MT.FNHSysUnitSectIdTo,MT.FNHSysEmpTypeIdTo,MT.FNTotalMinute,ETT.FTEmpTypeNameTH,USS.FTUnitSectNameTH"
        _Qry &= vbCrLf & " ,MT.FDInsDate, MT.FTInsTime, MT.FTInsUser , MT.FDUpdDate, MT.FTUpdTime, MT.FTUpdUser "
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType  AS MT with (NOLOCK)"
        _Qry &= vbCrLf & " INNER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS E with (NOLOCK) on MT.FNHSysEmpID=E.FNHSysEmpID"
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ETT WITH (NOLOCK) ON MT.FNHSysEmpTypeIdTo=ETT.FNHSysEmpTypeId"
        _Qry &= vbCrLf & " LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS USS WITH (NOLOCK) ON MT.FNHSysUnitSectIdTo=USS.FNHSysUnitSectId"

        _Qry &= vbCrLf & "where (MT.FDDate='" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "')"
        _Qry &= vbCrLf & ") AS oho ON M.FNHSysEmpID=oho.FNHSysEmpID"


        _Qry &= vbCrLf & " LEFT JOIN ( SELECT FNHSysEmpID "
        _Qry &= vbCrLf & " ,TSM.FTIn1 AS FTIn1  ,TSM.FTOut1 AS FTOut1 "
        _Qry &= vbCrLf & " ,TSM.FTIn2 AS FTIn2 ,TSM.FTOut2 AS FTOut2 "
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeMoveShift EMS  "
        _Qry &= vbCrLf & " LEFT JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift TSM ON EMS.FNHSysShiftID= TSM.FNHSysShiftID "
        _Qry &= vbCrLf & "  WHERE  FDShiftDate='" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "' )  EMTS ON EMTS.FNHSysEmpID = M.FNHSysEmpID "

        _Qry &= vbCrLf & " WHERE  M.FTEmpCode <> '' "
        _Qry &= vbCrLf & " AND M.FDDateStart <='" & HI.UL.ULDate.ConvertEnDB(FTDateRequest.Text) & "' "
        _Qry &= vbCrLf & " AND (M.FDDateEnd ='' OR M.FDDateEnd >'" & HI.UL.ULDate.ConvertEnDB(FTDateRequest.Text) & "' )   "
        _Qry &= vbCrLf & " AND M.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "

        If Me.FNHSysEmpTypeId.Text <> "" Then
            _Qry &= vbCrLf & " AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
        End If

        _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)

        '------Criteria By Employeee Code
        If Me.FNHSysEmpId.Text <> "" Then
            _Qry &= vbCrLf & " AND M.FTEmpCode >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
        End If

        If Me.FNHSysEmpIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND M.FTEmpCode <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
        End If

        '------Criteria By Department
        If Me.FNHSysDeptId.Text <> "" Then
            _Qry &= vbCrLf & " AND  Dept.FTDeptCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
        End If

        If Me.FNHSysDeptIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  Dept.FTDeptCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
        End If

        '------Criteria By Division
        If Me.FNHSysDivisonId.Text <> "" Then
            _Qry &= vbCrLf & " AND  DI.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
        End If

        If Me.FNHSysDivisonIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  DI.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
        End If

        '------Criteria By Sect
        If Me.FNHSysSectId.Text <> "" Then
            _Qry &= vbCrLf & " AND  ST.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
        End If

        If Me.FNHSysSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND  ST.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
        End If

        '------Criteria Unit Sect
        If Me.FNHSysUnitSectId.Text <> "" Then
            _Qry &= vbCrLf & " AND   US.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
        End If

        If Me.FNHSysUnitSectIdTo.Text <> "" Then
            _Qry &= vbCrLf & " AND   US.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
        End If


        _Qry &= vbCrLf & " ORDER BY  M.FTEmpCode  "

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.ogc.DataSource = _Dt
        Return True
    End Function

#Region "Initial Grid"

    Private Sub InitGrid()
        '------Start Add Summary Grid-------------
        Dim sFieldCount As String = "FTEmpCode"
        Dim sFieldSum As String = ""
        Dim sFieldGrpCount As String = ""
        Dim sFieldGrpSum As String = ""

        With ogv
            .ClearGrouping()
            .ClearDocument()

            For Each Str As String In sFieldCount.Split("|")
                If Str <> "" Then
                    .Columns(Str).Summary.Add(DevExpress.Data.SummaryItemType.Count, Str)
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

#End Region

    Private Function SaveData(Spls As HI.TL.SplashScreen, ByRef _Rec_Err As Integer, ByRef _Msg_Err As String) As Boolean

        Dim _Qry As String
        CType(ogc.DataSource, DataTable).AcceptChanges()
        Dim _Dt As DataTable = CType(ogc.DataSource, DataTable)


        Dim _ToatlRecord As Integer = _Dt.Select("FTSelect='1'").Length
        Dim _Rec As Integer = 0
        Dim _FoundOver As Boolean = False

        Dim _Rec_Err_Tran As Integer = 0
        'Dim _Msg_Err As String = ""

        Try
            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _Dt.Select("FTSelect='1'")
                _FoundOver = False
                If R!FTSelect.ToString = "1" Then
                    _Rec = _Rec + 1

                    _Rec_Err_Tran = 0

                    If Not (Spls Is Nothing) Then
                        Spls.UpdateInformation("Save Data Employee " & R!FTEmpCode.ToString & "   Record   " & _Rec.ToString & " Of " & _ToatlRecord.ToString & "  (" & Format((_Rec * 100.0) / _ToatlRecord, "0.00") & " % ) ")
                    End If


                    '' check  from to   and over lab

                    If Val(R!FNHSysEmpTypeId) <> Val(Me.FNHSysEmpTypeIdToMove.Properties.Tag) Then
                        If Val(R!FNHSysUnitSectId) <> Val(Me.FNHSysUnitSectIdToMove.Properties.Tag) Then


                            ''check over lab time

                            '_Qry = " SELECT  FNHSysEmpTypeId=" & Val(R!FNHSysEmpTypeId) & ""
                            '_Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType "
                            '_Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(R!FNHSysEmpID) & ""
                            '_Qry &= vbCrLf & " AND FDDate='" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"

                            '_Qry &= vbCrLf & " AND FTStartTime='" & Me.FTStartTimeMove.Text & "'"
                            '_Qry &= vbCrLf & " AND FTStartTime='" & Me.FTStartTimeMove.Text & "'"



                            If Not (_FoundOver) Then
                                _Qry = " SELECT TOP 1 FNHSysEmpID  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType WITH (NOLOCK)"
                                _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(R!FNHSysEmpID) & " "
                                _Qry &= vbCrLf & "AND FDDate='" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"
                                _Qry &= vbCrLf & "AND FTStartTime='" & Me.FTStartTimeMove.Text & "'"

                                If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_PROD, "") <> "" Then

                                    _Qry = " Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType "
                                    _Qry &= vbCrLf & "  SET FTUpdUser='" & HI.ST.UserInfo.UserName & "'  "
                                    _Qry &= vbCrLf & " ,FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ""
                                    _Qry &= vbCrLf & " ,FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & ""
                                    _Qry &= vbCrLf & " ,FNHSysEmpTypeId=" & Val(R!FNHSysEmpTypeId) & ""
                                    _Qry &= vbCrLf & " ,FNHSysUnitSectId=" & Val(R!FNHSysUnitSectId) & ""
                                    _Qry &= vbCrLf & " ,FNHSysEmpTypeIdTo='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeIdToMove.Properties.Tag) & "'"
                                    _Qry &= vbCrLf & " ,FNHSysUnitSectIdTo='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdToMove.Properties.Tag) & "'"
                                    _Qry &= vbCrLf & " ,FTEndTime='" & Me.FTEndTimeMove.Text & "'"
                                    _Qry &= vbCrLf & " ,FNTotalMinute='" & Me.ocetotaltime.Value & "'"
                                    _Qry &= vbCrLf & " ,FTStartTime='" & Me.FTStartTimeMove.Text & "'"
                                    _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(R!FNHSysEmpID) & ""
                                    _Qry &= vbCrLf & "AND FDDate='" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"
                                    _Qry &= vbCrLf & "AND FTStartTime='" & Me.FTStartTimeMove.Text & "'"

                                Else

                                    ''check overlap time
                                    _Qry = " SELECT TOP 1 FNHSysEmpID  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType WITH (NOLOCK)"
                                    _Qry &= vbCrLf & " WHERE FNHSysEmpID=" & Val(R!FNHSysEmpID) & " "
                                    _Qry &= vbCrLf & "AND FDDate='" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"
                                    _Qry &= vbCrLf & "AND (('" & Me.FTStartTimeMove.Text & "' < FTStartTime AND FTStartTime<= '" & Me.FTEndTimeMove.Text & "')"

                                    _Qry &= vbCrLf & "OR ('" & Me.FTStartTimeMove.Text & "' < FTEndTime  AND FTEndTime<= '" & Me.FTEndTimeMove.Text & "'))"


                                    If HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_PROD, "") = "" Then


                                        _Qry = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType (  FTInsUser, FDInsDate, FTInsTime"
                                        _Qry &= vbCrLf & "  , FNHSysEmpID, FDDate"
                                        _Qry &= vbCrLf & "  ,FNHSysEmpTypeId"
                                        _Qry &= vbCrLf & " ,FNHSysUnitSectId"
                                        _Qry &= vbCrLf & " ,FNHSysEmpTypeIdTo"
                                        _Qry &= vbCrLf & ",FNHSysUnitSectIdTo"
                                        _Qry &= vbCrLf & ", FTStartTime"
                                        _Qry &= vbCrLf & ", FTEndTime,FNTotalMinute)"
                                        _Qry &= vbCrLf & " SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & ""
                                        _Qry &= vbCrLf & " ,'" & Val(R!FNHSysEmpID) & "','" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"
                                        _Qry &= vbCrLf & ",'" & Val(R!FNHSysEmpTypeId) & "'"
                                        _Qry &= vbCrLf & ",'" & Val(R!FNHSysUnitSectId) & "'"
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeIdToMove.Properties.Tag) & "'"
                                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdToMove.Properties.Tag) & "'"
                                        _Qry &= vbCrLf & ",'" & Me.FTStartTimeMove.Text & "'"
                                        _Qry &= vbCrLf & ",'" & Me.FTEndTimeMove.Text & "'," & Me.ocetotaltime.Value & ""
                                    Else
                                        _Rec_Err = _Rec_Err + 1
                                        _Msg_Err = _Msg_Err + HI.UL.ULF.rpQuoted(R!FTEmpCode) & " " & HI.UL.ULF.rpQuoted(R!FTEmpName) & vbCrLf

                                        _Rec_Err_Tran = 1
                                    End If
                                End If

                                If _Rec_Err_Tran = 0 Then
                                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                        HI.Conn.SQLConn.Tran.Rollback()
                                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                        Return False
                                    End If
                                Else

                                    HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)


                                End If


                            End If
                        Else
                            _Rec_Err = _Rec_Err + 1
                            _Msg_Err = _Msg_Err + HI.UL.ULF.rpQuoted(R!FTEmpCode) & " " & HI.UL.ULF.rpQuoted(R!FTEmpName) & vbCrLf
                        End If
                    Else
                        _Rec_Err = _Rec_Err + 1
                        _Msg_Err = _Msg_Err + HI.UL.ULF.rpQuoted(R!FTEmpCode) & " " & HI.UL.ULF.rpQuoted(R!FTEmpName) & vbCrLf
                    End If
                End If
            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Rec = 0
            Return True
        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
    End Function

    Private Function DeleteData(Spls As HI.TL.SplashScreen) As Boolean
        Try

            CType(ogc.DataSource, DataTable).AcceptChanges()
            Dim _Dt As DataTable = CType(ogc.DataSource, DataTable)

            Dim _ToatlRecord As Integer = _Dt.Select("FTSelect='1'").Length
            Dim _Rec As Integer = 0

            Dim _Qry As String = ""

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_PROD)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _Dt.Rows

                If R!FTSelect.ToString = "1" Then

                    _Rec = _Rec + 1

                    If Not (Spls Is Nothing) Then
                        Spls.UpdateInformation("Delete Data Employee " & R!FTEmpCode.ToString & "   Record   " & _Rec.ToString & " Of " & _ToatlRecord.ToString & "  (" & Format((_Rec * 100.0) / _ToatlRecord, "0.00") & " % ) ")
                    End If

                    _Qry = " Delete FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_PROD) & "].dbo.TPRODTMoveTeamMoveType "
                    _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(R!FNHSysEmpID) & " "
                    _Qry &= vbCrLf & "AND FDDate='" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"
                    _Qry &= vbCrLf & "AND FTStartTime='" & R!FTStartTime.ToString & "'"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    End If
                End If
            Next
            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Rec = 0
            For Each R As DataRow In _Dt.Rows
                If R!FTSelect.ToString = "1" Then
                    _Rec = _Rec + 1

                    If Not (Spls Is Nothing) Then
                        Spls.UpdateInformation("Calculate Work Time Employee " & R!FTEmpCode.ToString & "   Record   " & _Rec.ToString & " Of " & _ToatlRecord.ToString & "  (" & Format((_Rec * 100.0) / _ToatlRecord, "0.00") & " % ) ")
                    End If

                End If
            Next
            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function
    Private Sub ochkselectall_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ochkselectall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With ogc
                If Not (.DataSource Is Nothing) And ogv.RowCount > 0 Then

                    With ogv
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ProcessSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click

        If Me.CheckData Then
            Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
            Dim _Rec_Err As Integer = 0
            Dim _Msg_Err As String = ""



            If Me.SaveData(_Spls, _Rec_Err, _Msg_Err) Then
                _Spls.Close()

                If _Rec_Err > 0 Then

                    _Msg_Err = "" & vbCrLf & _Msg_Err & vbCrLf & " "

                    HI.MG.ShowMsg.mInfo("การทำการย้ายทีมย้ายประเภท มีข้อมูลบ้างส่วนผิดพลาด !!!", 2206170001, Me.Text, _Msg_Err, System.Windows.Forms.MessageBoxIcon.Warning)
                Else
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If

                Me.ocmload_Click(ocmload, New System.EventArgs)
            Else
                _Spls.Close()


                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If
        End If
    End Sub

    Private Sub ProcessDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If CType(ogc.DataSource, DataTable).Rows.Count > 0 Then

            If HI.MG.ShowMsg.mConfirmProcess("คูณต้องการลบข้อมูลการย้าย ใช่หรือไม่?", 1506171053, FTDateRequest.Text) = True Then
                CType(ogc.DataSource, DataTable).AcceptChanges()
                If CType(ogc.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then

                    Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")

                    If Me.DeleteData(_Spls) Then
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                        Me.ocmload_Click(ocmload, New System.EventArgs)
                    Else
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    End If

                Else
                    HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกพนักงาน", 1304030001, Me.Text)
                End If
            End If
        Else
            HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกพนักงาน", 1304030001, Me.Text)
        End If
    End Sub

    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click
        Me.ogc.DataSource = Nothing
        ochkselectall.Checked = False

        HI.TL.HandlerControl.ClearControl(Me)
        Me.FTDateRequest.Focus()
    End Sub
    Private Sub ocmpreview_Click(sender As Object, e As EventArgs) Handles ocmpreview.Click

    End Sub
    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        Dim _Spls As New HI.TL.SplashScreen("Loading ... Data ")
        If HI.UL.ULDate.CheckDate(FTDateRequest.Text) <> "" Then
            Me.LoadDataInfo(_Spls)
            _Spls.Close()
        Else
            _Spls.Close()
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateRequest_lbl.Text)
            FTDateRequest.Focus()
        End If
    End Sub
    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogv)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Function CheckData() As Boolean

        Dim _Pass As Boolean = False
        Dim _WHEREFnhSysUnisectId As String = ""
        Dim _MSG As String = ""
        If HI.UL.ULDate.CheckDate(FTDateRequest.Text) <> "" Then
            If Not (ogc.DataSource Is Nothing) Then
                CType(ogc.DataSource, DataTable).AcceptChanges()
                If CType(ogc.DataSource, DataTable).Rows.Count > 0 Then
                    If CType(ogc.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then

                        If (Me.FTStartTimeMove.Text <> "" And Me.FTEndTimeMove.Text <> "") Then
                            If (Me.ocetotaltime.Value > 0) Then

                                ''check calculate incentive
                                For Each R2 As DataRow In CType(ogc.DataSource, DataTable).Select("FTSelect='1'")
                                    'If _WHEREFnhSysUnisectId = "" Then
                                    '    _WHEREFnhSysUnisectId += "'" + R2!FNHSysUniSectID.ToString + "'"
                                    'Else
                                    _WHEREFnhSysUnisectId += ",'" + R2!FNHSysUnitSectID.ToString + "'"
                                    'End If

                                Next

                                If CheckCalculateIncentive(_WHEREFnhSysUnisectId, _MSG) Then

                                    _Pass = True
                                Else
                                    _Pass = False
                                    HI.MG.ShowMsg.mInvalidData("พบการคำนวนค่าแรงจูงใจของสกัดที่เลือก!! ", 2106171100, Me.Text, _MSG)
                                    FNHSysUnitSectIdToMove.Focus()
                                End If


                            Else
                                _Pass = False
                                HI.MG.ShowMsg.mInvalidData("โปรดระบุเวลา", 1506171100, Me.Text)
                                FTStartTimeMove.Focus()
                            End If


                            If (Me.FNHSysEmpTypeIdToMove.Text = "") And (Me.FNHSysUnitSectIdToMove.Text = "") Then
                                HI.MG.ShowMsg.mInvalidData("กรุณาเลือกย้ายประเภท หรือ ทีม", 1506171058, Me.Text)
                                _Pass = False
                                FNHSysEmpTypeIdToMove.Focus()
                            End If
                        Else
                            HI.MG.ShowMsg.mInvalidData("โปรดระบุเวลา", 1506171100, Me.Text)
                            FTStartTimeMove.Focus()
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกพนักงาน", 1304030001, Me.Text)
                        FTDateRequest.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกพนักงาน", 1304030001, Me.Text)
                    FTDateRequest.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกพนักงาน", 1304030001, Me.Text)
                FTDateRequest.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateRequest_lbl.Text)
            FTDateRequest.Focus()
        End If
        Return _Pass
    End Function


    Public Function CheckCalculateIncentive(ByVal _QryWhere As String, ByRef _Msg As String) As Boolean


        Dim _FNHSysUnitSectID As String = ""
        Dim _dt As DataTable


        Dim _Qry As String
        If Val(FNHSysUnitSectIdToMove.Properties.Tag) > 0 Then


            _Qry = " SELECT U.FTUnitSectCode FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTIncentive I "
            _Qry &= vbCrLf & " LEFT JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect U  ON I.FNHSysUnitSectId = U.FNHSysUnitSectId  "
            _Qry &= vbCrLf & " WHERE I.FNHSysUnitSectID in ('" & Val(FNHSysUnitSectIdToMove.Properties.Tag) & "'" & _QryWhere & ") "
            _Qry &= vbCrLf & " AND I.FTCalDate='" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"

            _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            If _dt.Rows.Count > 0 Then
                For Each R As DataRow In _dt.Rows
                    _Msg &= R!FTUnitSectCode.ToString & " \"
                Next
                Return False
            Else
                Return True
            End If

        Else
            Return True
        End If


    End Function

    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub MoveLoad(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)
    End Sub



    'Private Sub RepTimeEdit_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepFTStarttime.EditValueChanging
    '    Try
    '        With Me.ogv

    '            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
    '            If Not (ocmsave.Enabled) Then
    '                e.Cancel = True
    '            Else

    '                Try

    '                    If HI.HRCAL.Time.CheckClosePeriod(HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTDateTrans").ToString), Val("" & .GetRowCellValue(.FocusedRowHandle, "FNHSysEmpID").ToString)) = True Then
    '                        HI.MG.ShowMsg.mInfo("ไม่สามารถทำการแก้ไขข้อมูลหลังการปิดงวดได้ กรุณาทำการตรวจสอบ", 14040001, Me.Text)
    '                        e.Cancel = True
    '                    Else

    '                    End If

    '                Catch ex As Exception
    '                End Try

    '            End If

    '        End With
    '    Catch ex As Exception
    '    End Try
    'End Sub


    Private Sub RepFTScanAIn_Leave(sender As Object, e As System.EventArgs) Handles RepFTStarttime.Leave
        Try

            If sender.EditValue Is Nothing Then
                Try
                    ogv.SetFocusedRowCellValue("" & ogv.FocusedColumn.FieldName.ToString & "M", "")
                Catch ex As Exception
                End Try
                Try
                    CType(ogc.DataSource, DataTable).AcceptChanges()
                Catch ex As Exception
                End Try

                Dim _Dt As DataTable = CType(ogc.DataSource, DataTable)
                Dim _T1 As Integer = 0
                Dim _T2 As Integer = 0
                Dim _T3 As Integer = 0
                Dim _T4 As Integer = 0
                Dim _T5 As Integer = 0
                Dim _T6 As Integer = 0
                Dim _Res As Integer = 0
                Dim time As String = "24:00"


            Else

            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub RepFTStarttime_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepFTStarttime.EditValueChanging, RepFTEndtime.EditValueChanging
        Try
            If Val(e.NewValue.ToString) < 0 Then
                e.Cancel = True
            Else
                e.Cancel = False

                With Me.ogv

                    Dim _FTStarttime As String = ("" & .GetFocusedRowCellValue("FTStarttime").ToString)
                    Dim _FTEndtime As String = ("" & .GetFocusedRowCellValue("FTEndtime").ToString)

                    Dim _FTIn1 As String = ("" & .GetFocusedRowCellValue("FTIn1").ToString)
                    Dim _FTOut1 As String = ("" & .GetFocusedRowCellValue("FTOut1").ToString)
                    Dim _FTIn2 As String = ("" & .GetFocusedRowCellValue("FTIn2").ToString)
                    Dim _FTOut2 As String = ("" & .GetFocusedRowCellValue("FTOut2").ToString)


                    '_FTIn1
                    '_FTOut1
                    '_FTIn2
                    '_FTOut2


                    Dim _FNTotalMinute As Double = 0

                    Dim _T1 As Integer = 0
                    Dim _T2 As Integer = 0
                    Dim _T3 As Integer = 0
                    Dim _T4 As Integer = 0
                    Dim _T5 As Integer = 0
                    Dim _T6 As Integer = 0
                    Dim _Res As Integer = 0
                    Dim time As String = "24:00"

                    Select Case .FocusedColumn.FieldName.ToString.ToLower
                        Case "FTStarttime".ToLower
                            _FTStarttime = Format(CDate(e.NewValue.ToString), "HH:mm")
                        Case "FTEndtime".ToLower
                            _FTEndtime = Format(CDate(e.NewValue.ToString), "HH:mm")
                    End Select

                    'Dim _Time As String = Format(CDate(_TmpData), "HH:mm")

                    If _FTStarttime.ToString <> "" And _FTEndtime.ToString <> "" And _FTStarttime.ToString <= _FTOut1.ToString And _FTEndtime.ToString <= _FTOut1.ToString Then

                        If _FTStarttime.ToString > _FTOut1.ToString Then
                            _T1 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & _FTStarttime.ToString), CDate(Me.ActualNextDate & "  " & _FTEndtime.ToString))
                        Else
                            _T1 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & _FTStarttime.ToString), CDate(Me.ActualDate & "  " & _FTEndtime.ToString))
                        End If
                        _FNTotalMinute = _T1
                    End If
                    'afternoon 13.00-17.00
                    If _FTStarttime.ToString <> "" And _FTEndtime.ToString <> "" And _FTStarttime.ToString >= _FTIn2.ToString And _FTEndtime.ToString <= _FTOut2.ToString Then

                        If _FTStarttime.ToString > _FTOut2.ToString Then
                            _T2 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & _FTStarttime.ToString), CDate(Me.ActualNextDate & "  " & _FTEndtime.ToString))
                        Else
                            _T2 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & _FTStarttime.ToString), CDate(Me.ActualDate & "  " & _FTEndtime.ToString))
                        End If
                        _FNTotalMinute = _T2
                    End If
                    'morning to evening 08.00-17.00
                    If _FTStarttime.ToString <> "" And _FTEndtime.ToString <> "" And _FTStarttime.ToString < _FTOut1.ToString And _FTEndtime.ToString <= _FTOut2.ToString And _FTEndtime.ToString >= _FTIn2.ToString Then
                        If _FTStarttime.ToString > _FTEndtime.ToString Then
                            _T3 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & _FTStarttime.ToString), CDate(Me.ActualNextDate & "  " & _FTEndtime.ToString))
                        Else
                            _T3 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & _FTStarttime.ToString), CDate(Me.ActualDate & "  " & _FTEndtime.ToString))
                        End If
                        _FNTotalMinute = _T3 - 60
                    End If
                    'OT 17.00 to infinity
                    If _FTStarttime.ToString <> "" And _FTEndtime.ToString <> "" And _FTStarttime.ToString >= _FTOut2.ToString Then
                        If _FTStarttime.ToString > _FTEndtime.ToString Then
                            _T4 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & _FTStarttime.ToString), CDate(Me.ActualNextDate & "  " & _FTEndtime.ToString))
                        Else
                            _T4 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & _FTStarttime.ToString), CDate(Me.ActualDate & "  " & _FTEndtime.ToString))
                        End If
                        _FNTotalMinute = _T4
                    End If
                    ' Sinc morming to evening AND OT
                    If _FTStarttime.ToString <> "" And _FTEndtime.ToString <> "" And _FTStarttime.ToString <= _FTOut1.ToString And _FTEndtime.ToString > _FTIn1.ToString And _FTEndtime.ToString > _FTOut1.ToString And _FTEndtime.ToString > _FTIn2.ToString And _FTEndtime.ToString > _FTOut2.ToString Then
                        If _FTStarttime.ToString > _FTEndtime.ToString Then
                            _T5 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & _FTStarttime.ToString), CDate(Me.ActualNextDate & "  " & _FTEndtime.ToString))
                        Else
                            _T5 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & _FTStarttime.ToString), CDate(Me.ActualDate & "  " & _FTEndtime.ToString))
                        End If
                        _FNTotalMinute = _T5 - 60
                    End If
                    If _FTStarttime.ToString <> "" And _FTEndtime.ToString <> "" And _FTStarttime.ToString >= _FTIn2.ToString And _FTEndtime.ToString > _FTIn1.ToString And _FTEndtime.ToString > _FTOut1.ToString And _FTEndtime.ToString > _FTIn2.ToString And _FTEndtime.ToString > _FTOut2.ToString Then
                        If _FTStarttime.ToString > _FTEndtime.ToString Then
                            _T6 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & _FTStarttime.ToString), CDate(Me.ActualNextDate & "  " & _FTEndtime.ToString))
                        Else
                            _T6 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & _FTStarttime.ToString), CDate(Me.ActualDate & "  " & _FTEndtime.ToString))
                        End If
                        _FNTotalMinute = _T6
                    End If

                    If _FNTotalMinute < 0 Then
                        _FNTotalMinute = 0
                    End If
                    .SetFocusedRowCellValue("FNTotalMinute", CInt(Format((_FNTotalMinute), "000")))

                    .SetFocusedRowCellValue("FTSelect", "1")
                End With

            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub time_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FTStartTimeMove.EditValueChanged, FTEndTimeMove.EditValueChanged

        Select Case sender.name.ToString.ToUpper
            Case "FTStartTimeMove".ToUpper, "FTEndTimeMove".ToUpper
                Dim _Dt As DataTable = CType(ogc.DataSource, DataTable)
                Dim _T1 As Integer = 0
                Dim _T2 As Integer = 0
                Dim _T3 As Integer = 0
                Dim _T4 As Integer = 0
                Dim _T5 As Integer = 0
                Dim _T6 As Integer = 0
                Dim _Res As Integer = 0
                Dim time As String = "24:00"

                If _Dt Is Nothing Then Exit Sub
                For Each R As DataRow In _Dt.Rows
                    'morning 08.00-12.00
                    If FTStartTimeMove.Text <> "" And FTEndTimeMove.Text <> "" And FTStartTimeMove.Text <= R!FTOut1.ToString And FTEndTimeMove.Text <= R!FTOut1.ToString Then

                        If FTStartTimeMove.Text > R!FTOut1.ToString Then
                            _T1 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & FTStartTimeMove.Text), CDate(Me.ActualNextDate & "  " & FTEndTimeMove.Text))
                        Else
                            _T1 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & FTStartTimeMove.Text), CDate(Me.ActualDate & "  " & FTEndTimeMove.Text))
                        End If
                        ocetotaltimea.Value = _T1
                    End If
                    'afternoon 13.00-17.00
                    If FTStartTimeMove.Text <> "" And FTEndTimeMove.Text <> "" And FTStartTimeMove.Text >= R!FTIn2.ToString And FTEndTimeMove.Text <= R!FTOut2.ToString Then

                        If FTStartTimeMove.Text > R!FTOut2.ToString Then
                            _T2 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & FTStartTimeMove.Text), CDate(Me.ActualNextDate & "  " & FTEndTimeMove.Text))
                        Else
                            _T2 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & FTStartTimeMove.Text), CDate(Me.ActualDate & "  " & FTEndTimeMove.Text))
                        End If
                        ocetotaltimea.Value = _T2
                    End If
                    'morning to evening 08.00-17.00
                    If FTStartTimeMove.Text <> "" And FTEndTimeMove.Text <> "" And FTStartTimeMove.Text < R!FTOut1.ToString And FTEndTimeMove.Text <= R!FTOut2.ToString And FTEndTimeMove.Text >= R!FTIn2 Then
                        If FTStartTimeMove.Text > FTEndTimeMove.Text Then
                            _T3 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & FTStartTimeMove.Text), CDate(Me.ActualNextDate & "  " & FTEndTimeMove.Text))
                        Else
                            _T3 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & FTStartTimeMove.Text), CDate(Me.ActualDate & "  " & FTEndTimeMove.Text))
                        End If
                        ocetotaltimea.Value = _T3 - 60
                    End If
                    'OT 17.00 to infinity
                    If FTStartTimeMove.Text <> "" And FTEndTimeMove.Text <> "" And FTStartTimeMove.Text >= R!FTOut2.ToString Then
                        If FTStartTimeMove.Text > FTEndTimeMove.Text Then
                            _T4 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & FTStartTimeMove.Text), CDate(Me.ActualNextDate & "  " & FTEndTimeMove.Text))
                        Else
                            _T4 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & FTStartTimeMove.Text), CDate(Me.ActualDate & "  " & FTEndTimeMove.Text))
                        End If
                        ocetotaltimea.Value = _T4
                    End If
                    ' Sinc morming to evening AND OT
                    If FTStartTimeMove.Text <> "" And FTEndTimeMove.Text <> "" And FTStartTimeMove.Text <= R!FTOut1.ToString And FTEndTimeMove.Text > R!FTIn1.ToString And FTEndTimeMove.Text > R!FTOut1.ToString And FTEndTimeMove.Text > R!FTIn2.ToString And FTEndTimeMove.Text > R!FTOut2.ToString Then
                        If FTStartTimeMove.Text > FTEndTimeMove.Text Then
                            _T5 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & FTStartTimeMove.Text), CDate(Me.ActualNextDate & "  " & FTEndTimeMove.Text))
                        Else
                            _T5 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & FTStartTimeMove.Text), CDate(Me.ActualDate & "  " & FTEndTimeMove.Text))
                        End If
                        ocetotaltimea.Value = _T5 - 60
                    End If
                    If FTStartTimeMove.Text <> "" And FTEndTimeMove.Text <> "" And FTStartTimeMove.Text >= R!FTIn2.ToString And FTEndTimeMove.Text > R!FTIn1.ToString And FTEndTimeMove.Text > R!FTOut1.ToString And FTEndTimeMove.Text > R!FTIn2.ToString And FTEndTimeMove.Text > R!FTOut2.ToString Then
                        If FTStartTimeMove.Text > FTEndTimeMove.Text Then
                            _T6 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & FTStartTimeMove.Text), CDate(Me.ActualNextDate & "  " & FTEndTimeMove.Text))
                        Else
                            _T6 = DateDiff(DateInterval.Minute, CDate(Me.ActualDate & "  " & FTStartTimeMove.Text), CDate(Me.ActualDate & "  " & FTEndTimeMove.Text))
                        End If
                        ocetotaltimea.Value = _T6
                    End If
                Next
        End Select
        Dim _Total As Integer = ocetotaltimea.Value
        If _Total < 0 Then
            _Total = 0
        End If

        Me.ocetotaltime.Value= CInt(Format((_Total), "000"))
    End Sub
    Private Sub ocetotaltime_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles ocetotaltime.EditValueChanged
        Try
            Me.TotalTime.Text = (Format(Me.ocetotaltime.Value, "0.00"))
        Catch ex As Exception
            Me.TotalTime.Text = "0.00"
        End Try
    End Sub

    Private Sub FTDateRequest_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FTDateRequest.EditValueChanged
        Me.ogc.DataSource = Nothing
        ochkselectall.Checked = False
    End Sub
End Class
