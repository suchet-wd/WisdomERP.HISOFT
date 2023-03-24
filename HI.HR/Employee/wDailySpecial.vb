Public Class wDailySpecial


    Private _ListEmpOver As wListEmpWorkTimeOver
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ActualDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

        Call InitGrid()

        _ListEmpOver = New wListEmpWorkTimeOver
        HI.TL.HandlerControl.AddHandlerObj(_ListEmpOver)


        Dim _SystemLang As New ST.SysLanguage
        Try
            Call _SystemLang.LoadObjectLanguage(HI.ST.SysInfo.ModuleName, _ListEmpOver.Name.ToString.Trim, _ListEmpOver)
        Catch ex As Exception
        Finally
        End Try
     
    End Sub

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

#Region "MAIN PROC"

    Private Sub ProcessSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click

        If Me.VerrifyData Then
            If Me.FNHSysEmpTypeId.Text = "" Then
                If HI.HRCAL.Time.CheckClosePeriod(FTDateRequest.Text) = True Then
                    HI.MG.ShowMsg.mInfo("ไม่สามารถทำการแก้ไขข้อมูลหลังการปิดงวดได้ กรุณาทำการตรวจสอบ", 14040001, Me.Text)
                    Exit Sub
                End If
            Else
                If HI.HRCAL.Time.CheckClosePeriod(FTDateRequest.Text, , Integer.Parse(Val(FNHSysEmpTypeId.Properties.Tag.ToString))) = True Then
                    HI.MG.ShowMsg.mInfo("ไม่สามารถทำการแก้ไขข้อมูลหลังการปิดงวดได้ กรุณาทำการตรวจสอบ", 14040001, Me.Text)
                    Exit Sub
                End If
            End If
           
            Dim _Spls As New HI.TL.SplashScreen("Saving And Calculating Work Time...   Please Wait   ")

            If Me.SaveData(_Spls) Then
                _Spls.Close()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                Me.ocmload_Click(ocmload, New System.EventArgs)
            Else
                _Spls.Close()
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If

        End If

    End Sub

    Private Sub ProcessDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If CType(ogc.DataSource, DataTable).Rows.Count > 0 Then

            If Me.FNHSysEmpTypeId.Text = "" Then
                If HI.HRCAL.Time.CheckClosePeriod(FTDateRequest.Text) = True Then
                    HI.MG.ShowMsg.mInfo("ไม่สามารถทำการแก้ไขข้อมูลหลังการปิดงวดได้ กรุณาทำการตรวจสอบ", 14040001, Me.Text)
                    Exit Sub
                End If
            Else
                If HI.HRCAL.Time.CheckClosePeriod(FTDateRequest.Text, , Integer.Parse(Val(FNHSysEmpTypeId.Properties.Tag.ToString))) = True Then
                    HI.MG.ShowMsg.mInfo("ไม่สามารถทำการแก้ไขข้อมูลหลังการปิดงวดได้ กรุณาทำการตรวจสอบ", 14040001, Me.Text)
                    Exit Sub
                End If
            End If

            CType(ogc.DataSource, DataTable).AcceptChanges()
            If CType(ogc.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then
                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบข้อมูลการวันเลิกงานพิเศษของพนักงานใช่หรือไม่ ?", 1404300010) Then

                    Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
                    If Me.DeleteData(_Spls) Then

                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                        Me.ocmload_Click(ocmload, New System.EventArgs)

                    Else
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    End If
                End If
            Else
                HI.MG.ShowMsg.mInvalidData("กรุณาทำการเลือกพนักงาน", 1304030001, Me.Text)
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

    Private Sub ProcessPreview(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmpreview.Click


        If Me.FTDateRequest.Text <> "" Then
            With New HI.RP.Report
                .FormTitle = Me.Text
                .ReportFolderName = "Human Report\"
                .ReportName = "DailySpecialSlip.rpt"

                Dim _Fm As String = " {THRTDailySpecial.FTDate}='" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"

                If Me.FNHSysEmpTypeId.Text <> "" Then
                    _Fm &= vbCrLf & " AND {THRMEmpType.FTEmpTypeCode}='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
                End If

                '------Criteria By Employeee Code
                If Me.FNHSysEmpId.Text <> "" Then
                    _Fm &= vbCrLf & " AND {THRMEmployee.FTEmpCode} >='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpId.Text) & "' "
                End If

                If Me.FNHSysEmpIdTo.Text <> "" Then
                    _Fm &= vbCrLf & " AND {THRMEmployee.FTEmpCode} <='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpIdTo.Text) & "' "
                End If

                '------Criteria By Department
                If Me.FNHSysDeptId.Text <> "" Then
                    _Fm &= vbCrLf & " AND  {TCNMDepartment.FTDeptCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptId.Text) & "' "
                End If

                If Me.FNHSysDeptIdTo.Text <> "" Then
                    _Fm &= vbCrLf & " AND  {TCNMDepartment.FTDeptCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDeptIdTo.Text) & "' "
                End If

                '------Criteria By Division
                If Me.FNHSysDivisonId.Text <> "" Then
                    _Fm &= vbCrLf & " AND  {TCNMDivision.FTDivisonCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
                End If

                If Me.FNHSysDivisonIdTo.Text <> "" Then
                    _Fm &= vbCrLf & " AND  {TCNMDivision.FTDivisonCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
                End If

                '------Criteria By Sect
                If Me.FNHSysSectId.Text <> "" Then
                    _Fm &= vbCrLf & " AND  {TCNMSect.FTSectCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
                End If

                If Me.FNHSysSectIdTo.Text <> "" Then
                    _Fm &= vbCrLf & " AND  {TCNMSect.FTSectCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
                End If

                '------Criteria Unit Sect
                If Me.FNHSysUnitSectId.Text <> "" Then
                    _Fm &= vbCrLf & " AND   {TCNMUnitSect.FTUnitSectCode}>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
                End If

                If Me.FNHSysUnitSectIdTo.Text <> "" Then
                    _Fm &= vbCrLf & " AND   {TCNMUnitSect.FTUnitSectCode}<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
                End If

                .Formular = _Fm
                .Preview()
            End With
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.SelectData, Me.Text, Me.FTDateRequest_lbl.Text)
            FTDateRequest.Focus()
        End If
    End Sub

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Procedure "

    Private Function SaveDataOver(dtdata As DataTable, Spls As HI.TL.SplashScreen) As Boolean

        Dim _Qry As String
        Dim _Dt As DataTable = dtdata

        Dim _ToatlRecord As Integer = _Dt.Select("FTSelect='1'").Length
        Dim _Rec As Integer = 0
        Try

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _Dt.Select("FTSelect='1'")

                If R!FTSelect.ToString = "1" Then
                    _Rec = _Rec + 1

                    If Not (Spls Is Nothing) Then
                        Spls.UpdateInformation("Save Data Employee " & R!FTEmpCode.ToString & "   Record   " & _Rec.ToString & " Of " & _ToatlRecord.ToString & "  (" & Format((_Rec * 100.0) / _ToatlRecord, "0.00") & " % ) ")
                    End If

                    _Qry = " SELECT TOP 1 FTDate  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailySpecial WITH (NOLOCK)"
                    _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(R!FNHSysEmpID) & " "
                    _Qry &= vbCrLf & " AND FTDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"

                    If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR, "") <> "" Then

                        _Qry = " Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailySpecial "
                        _Qry &= vbCrLf & "  SET  FTUpdUser='" & HI.ST.UserInfo.UserName & "' "
                        _Qry &= vbCrLf & " ,FTUpdDate=" & HI.UL.ULDate.FormatDateDB & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & "  "
                        _Qry &= vbCrLf & " ,FTTimeOut='" & R!FTTimeOut.ToString & "'"
                        _Qry &= vbCrLf & " , FTStateStop='" & R!FTStateStop.ToString & "' "
                        _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(R!FNHSysEmpID) & ""
                        _Qry &= vbCrLf & " AND FTDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"

                    Else

                        _Qry = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailySpecial (  FTInsUser, FTInsDate, FTInsTime "
                        _Qry &= vbCrLf & "  , FNHSysEmpID, FTDate, FTTimeOut, FTStateStop)  "
                        _Qry &= vbCrLf & " SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & "  "
                        _Qry &= vbCrLf & " ,'" & Val(R!FNHSysEmpID) & "'"
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "','" & R!FTTimeOut.ToString & "','" & R!FTStateStop.ToString & "' "

                    End If

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If
            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Rec = 0
            For Each R As DataRow In _Dt.Select("FTSelect='1'")
                If R!FTSelect.ToString = "1" Then

                    _Rec = _Rec + 1

                    If Not (Spls Is Nothing) Then
                        Spls.UpdateInformation("Calculate Work Time Employee " & R!FTEmpCode.ToString & "   Record   " & _Rec.ToString & " Of " & _ToatlRecord.ToString & "  (" & Format((_Rec * 100.0) / _ToatlRecord, "0.00") & " % ) ")
                    End If

                    HI.HRCAL.Calculate.CalculateWorkTime(HI.ST.UserInfo.UserName, Val(R!FNHSysEmpID), HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text), HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text))
                End If
            Next
            HI.HRCAL.Calculate.DisposeObject()
            Return True

        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Return False
        End Try

    End Function

    Private Function SaveData(Spls As HI.TL.SplashScreen) As Boolean

        Dim _Qry As String
        CType(ogc.DataSource, DataTable).AcceptChanges()
        Dim _Dt As DataTable = CType(ogc.DataSource, DataTable)
 
        Dim _ToatlRecord As Integer = _Dt.Select("FTSelect='1'").Length
        Dim _Rec As Integer = 0
        Try

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _Dt.Select("FTSelect='1'")

                If R!FTSelect.ToString = "1" Then
                    _Rec = _Rec + 1

                    If Not (Spls Is Nothing) Then
                        Spls.UpdateInformation("Save Data Employee " & R!FTEmpCode.ToString & "   Record   " & _Rec.ToString & " Of " & _ToatlRecord.ToString & "  (" & Format((_Rec * 100.0) / _ToatlRecord, "0.00") & " % ) ")
                    End If

                    _Qry = " SELECT TOP 1 FTDate  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailySpecial WITH (NOLOCK)"
                    _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(R!FNHSysEmpID) & " "
                    _Qry &= vbCrLf & " AND FTDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"

                    If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR, "") <> "" Then

                        _Qry = " Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailySpecial "
                        _Qry &= vbCrLf & "  SET  FTUpdUser='" & HI.ST.UserInfo.UserName & "' "
                        _Qry &= vbCrLf & " ,FTUpdDate=" & HI.UL.ULDate.FormatDateDB & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & "  "
                        _Qry &= vbCrLf & " ,FTTimeOut='" & otba1endtime.Text & "'"
                        _Qry &= vbCrLf & " , FTStateStop='" & FTStateStop.EditValue.ToString & "' "
                        _Qry &= vbCrLf & " , FTStatePlangnent='" & FTStatePlangnent.EditValue.ToString & "' "

                        _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(R!FNHSysEmpID) & ""
                        _Qry &= vbCrLf & " AND FTDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"

                    Else

                        _Qry = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailySpecial (  FTInsUser, FTInsDate, FTInsTime "
                        _Qry &= vbCrLf & "  , FNHSysEmpID, FTDate, FTTimeOut, FTStateStop,FTStatePlangnent)  "
                        _Qry &= vbCrLf & " SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & "  "
                        _Qry &= vbCrLf & " ,'" & Val(R!FNHSysEmpID) & "'"
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "','" & Me.otba1endtime.Text & "','" & FTStateStop.EditValue.ToString & "','" & FTStatePlangnent.EditValue.ToString & "' "


                    End If

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        Return False
                    End If

                End If
            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Rec = 0
            For Each R As DataRow In _Dt.Select("FTSelect='1'")
                If R!FTSelect.ToString = "1" Then

                    _Rec = _Rec + 1

                    If Not (Spls Is Nothing) Then
                        Spls.UpdateInformation("Calculate Work Time Employee " & R!FTEmpCode.ToString & "   Record   " & _Rec.ToString & " Of " & _ToatlRecord.ToString & "  (" & Format((_Rec * 100.0) / _ToatlRecord, "0.00") & " % ) ")
                    End If

                    HI.HRCAL.Calculate.CalculateWorkTime(HI.ST.UserInfo.UserName, Val(R!FNHSysEmpID), HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text), HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text))
                End If
            Next
            HI.HRCAL.Calculate.DisposeObject()
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

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _Dt.Rows

                If R!FTSelect.ToString = "1" Then

                    _Rec = _Rec + 1

                    If Not (Spls Is Nothing) Then
                        Spls.UpdateInformation("Delete Data Employee " & R!FTEmpCode.ToString & "   Record   " & _Rec.ToString & " Of " & _ToatlRecord.ToString & "  (" & Format((_Rec * 100.0) / _ToatlRecord, "0.00") & " % ) ")
                    End If

                    _Qry = " Delete FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailySpecial "
                    _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(R!FNHSysEmpID) & " "
                    _Qry &= vbCrLf & " AND FTDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "'"

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

                    HI.HRCAL.Calculate.CalculateWorkTime(HI.ST.UserInfo.UserName, Val(R!FNHSysEmpID), HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text), HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text))

                End If
            Next
            HI.HRCAL.Calculate.DisposeObject()
            Return True

        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Function VerrifyData() As Boolean
        Dim _Pass As Boolean = False
        If HI.UL.ULDate.CheckDate(FTDateRequest.Text) <> "" Then
            If Not (ogc.DataSource Is Nothing) Then
                CType(ogc.DataSource, DataTable).AcceptChanges()
                If CType(ogc.DataSource, DataTable).Rows.Count > 0 Then
                    If CType(ogc.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then


                        If (Me.otba1endtime.Text <> "") Or (Me.FTStateStop.Checked) Then

                            _Pass = True

                        Else

                            HI.MG.ShowMsg.mInvalidData("ระบุเวลา", 1304280001, Me.Text)
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

    Private Sub LoadDataInfo()
        Me.ogc.DataSource = Nothing
        ochkselectall.Checked = False

        Dim _Dt As DataTable
        Dim _Qry As String = ""

        _Qry = " SELECT      '0' AS FTSelect,  M.FNHSysEmpID, M.FTEmpCode"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & " , P.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

        Else
            _Qry &= vbCrLf & " , P.FTPreNameNameEN + ' ' +  M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
        End If

        _Qry &= vbCrLf & ", ST.FTSectCode "
        _Qry &= vbCrLf & ", US.FTUnitSectCode "


        _Qry &= vbCrLf & ", OTR.FTTimeOut, ISNULL(OTR.FTStateStop,'0') AS FTStateStop ,ISNULL(OTR.FTStatePlangnent,'0') AS FTStatePlangnent "

        _Qry &= vbCrLf & " ,ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
        _Qry &= vbCrLf & " ,ISNULL(Dept.FTDeptCode,'') AS FTDeptCode "
        _Qry &= vbCrLf & " ,ISNULL(DI.FTDivisonCode,'') AS FTDivisonCode "

        _Qry &= vbCrLf & " FROM        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        _Qry &= vbCrLf & "   THRMTimeShift AS SH WITH (NOLOCK) ON M.FNHSysShiftID = SH.FNHSysShiftID "
        _Qry &= vbCrLf & "   INNER Join "
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId "
        _Qry &= vbCrLf & " LEFT OUTER JOIN("
        _Qry &= vbCrLf & "	SELECT FTTimeOut,FTStateStop, FNHSysEmpID, FTDate,ISNULL(FTStatePlangnent,'0') AS FTStatePlangnent"
        _Qry &= vbCrLf & "	FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailySpecial AS OT WITH (NOLOCK)"
        _Qry &= vbCrLf & "	WHERE   (FTDate ='" & HI.UL.ULDate.ConvertEnDB(FTDateRequest.Text) & "')"
        _Qry &= vbCrLf & " ) AS OTR ON M.FNHSysEmpID = OTR.FNHSysEmpID"

        ''Dim _Qry2 As String = ""

        _Qry &= vbCrLf & " WHERE  M.FTEmpCode <> ''  "
        _Qry &= vbCrLf & " AND M.FDDateStart <='" & HI.UL.ULDate.ConvertEnDB(FTDateRequest.Text) & "' "
        _Qry &= vbCrLf & " AND (M.FDDateEnd ='' OR M.FDDateEnd >'" & HI.UL.ULDate.ConvertEnDB(FTDateRequest.Text) & "' )   "
        _Qry &= vbCrLf & " AND M.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "


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
    End Sub


    Private Sub LoadTimeInWeekOverDataInfo()

        Dim _Dt As New DataTable
        Dim _Qry As String = ""

        Try
            _Qry = " SELECT FTSelect,FNHSysEmpID,FTEmpCode,FTEmpName"
            _Qry &= vbCrLf & ",FTSectCode,FTUnitSectCode,FTEmpTypeCode,FTDeptCode,FTDivisonCode"
            _Qry &= vbCrLf & ", Case  WHEN FTTimeWorkInWeek > 0 AND FTTimeWorkInWeek <= FNTimeIn1 THEN Convert(varchar(5),DateAdd(MINUTE,FTTimeWorkInWeek,FTIn1),114)  "
            _Qry &= vbCrLf & "       WHEN FTTimeWorkInWeek > 0 AND FTTimeWorkInWeek >  FNTimeIn1  THEN Convert(varchar(5),DateAdd(MINUTE,FTTimeWorkInWeek -FNTimeIn1 ,FTIn2),114)  "
            _Qry &= vbCrLf & "  Else '' END AS FTTimeOut "
            _Qry &= vbCrLf & " ,CASE WHEN FTTimeWorkInWeek<=0 THEN '1' ELSE '0' END AS FTStateStop "

            _Qry &= vbCrLf & " FROM (SELECT      '1' AS FTSelect,  M.FNHSysEmpID, M.FTEmpCode"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & " , P.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

            Else
                _Qry &= vbCrLf & " , P.FTPreNameNameEN + ' ' +  M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
            End If

            _Qry &= vbCrLf & ", ST.FTSectCode "
            _Qry &= vbCrLf & ", US.FTUnitSectCode "
            _Qry &= vbCrLf & ", OTR.FTTimeOut, ISNULL(OTR.FTStateStop,'0') AS FTStateStop "
            _Qry &= vbCrLf & " ,ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
            _Qry &= vbCrLf & " ,ISNULL(Dept.FTDeptCode,'') AS FTDeptCode "
            _Qry &= vbCrLf & " ,ISNULL(DI.FTDivisonCode,'') AS FTDivisonCode "
            _Qry &= vbCrLf & " ,ISNULL(SH.FTIn1,'') AS FTIn1 "
            _Qry &= vbCrLf & " ,ISNULL(SH.FTOut1,'') AS FTOut1 "
            _Qry &= vbCrLf & " ,ISNULL(SH.FTIn2,'') AS FTIn2 "
            _Qry &= vbCrLf & " ,ISNULL(SH.FTOut2,'') AS FTOut2 "
            _Qry &= vbCrLf & " ,CASE WHEN ISNULL(SH.FTIn1,'') < ISNULL(SH.FTOut1,'') THEN  DATEDIFF(MINUTE,ISNULL(SH.FTIn1,''),ISNULL(SH.FTOut1,''))  ELSE   DATEDIFF(MINUTE,Convert(varchar(10),Getdate(),111) +' ' + ISNULL(SH.FTIn1,''),Convert(varchar(10),DateAdd(Day,1,Getdate()),111) +' ' + ISNULL(SH.FTOut1,''))   END AS FNTimeIn1 "
            _Qry &= vbCrLf & ", CASE WHEN ISNULL(SH.FTIn2,'') < ISNULL(SH.FTOut2,'') THEN  DATEDIFF(MINUTE,ISNULL(SH.FTIn2,''),ISNULL(SH.FTOut2,''))  ELSE   DATEDIFF(MINUTE,Convert(varchar(10),Getdate(),111) +' ' + ISNULL(SH.FTIn2,''),Convert(varchar(10),DateAdd(Day,1,Getdate()),111) +' ' + ISNULL(SH.FTOut2,''))   END AS FNTimeIn2 "
            _Qry &= vbCrLf & " ,[" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_LoadTimeWeekOver( M.FNHSysEmpID,'" & HI.UL.ULDate.ConvertEnDB(Me.FTDateRequest.Text) & "') AS FTTimeWorkInWeek "
            _Qry &= vbCrLf & " FROM        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
            _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS SH WITH (NOLOCK) ON M.FNHSysShiftID = SH.FNHSysShiftID "
            _Qry &= vbCrLf & "   INNER Join "
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId "
            _Qry &= vbCrLf & " LEFT OUTER JOIN("
            _Qry &= vbCrLf & "	SELECT FTTimeOut,FTStateStop, FNHSysEmpID, FTDate"
            _Qry &= vbCrLf & "	FROM    [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTDailySpecial AS OT WITH (NOLOCK)"
            _Qry &= vbCrLf & "	WHERE   (FTDate ='" & HI.UL.ULDate.ConvertEnDB(FTDateRequest.Text) & "')"
            _Qry &= vbCrLf & " ) AS OTR ON M.FNHSysEmpID = OTR.FNHSysEmpID"

            ''Dim _Qry2 As String = ""
            _Qry &= vbCrLf & " WHERE  M.FTEmpCode <> ''  "
            _Qry &= vbCrLf & " AND M.FDDateStart <='" & HI.UL.ULDate.ConvertEnDB(FTDateRequest.Text) & "' "
            _Qry &= vbCrLf & " AND (M.FDDateEnd ='' OR M.FDDateEnd >'" & HI.UL.ULDate.ConvertEnDB(FTDateRequest.Text) & "' )   "
            _Qry &= vbCrLf & " AND M.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "

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
            _Qry &= vbCrLf & " ) AS MM"
            _Qry &= vbCrLf & " WHERE FTTimeWorkInWeek < 480"
            _Qry &= vbCrLf & " ORDER BY FTUnitSectCode,FTEmpTypeCode,FTDeptCode,FTDivisonCode,FTSectCode,FTEmpCode  "

            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            HI.ST.Lang.SP_SETxLanguage(_ListEmpOver)
            With _ListEmpOver
                .ProcessOK = False
                .ogc.DataSource = _Dt
                .ShowDialog()

                If .ProcessOK Then
                    Dim _Spls As New HI.TL.SplashScreen("Saving And Calculating Work Time...   Please Wait   ")

                    Try
                        If Me.SaveDataOver(CType(.ogc.DataSource, DataTable), _Spls) Then
                            _Spls.Close()
                            HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                            Me.ocmload_Click(ocmload, New System.EventArgs)
                        Else
                            _Spls.Close()
                            HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        End If
                    Catch ex As Exception
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End Try
                  
                End If
            End With
        Catch ex As Exception
        End Try

        _Dt.Dispose()
    End Sub
#End Region

#Region "General"

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If HI.UL.ULDate.CheckDate(FTDateRequest.Text) <> "" Then
            Call LoadDataInfo()

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateRequest_lbl.Text)
            FTDateRequest.Focus()
        End If
    End Sub

    Private Sub FTDateRequest_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FTDateRequest.EditValueChanged
        Me.ogc.DataSource = Nothing
        ochkselectall.Checked = False
    End Sub

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

    Private Sub wOTRequest_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        HI.UL.AppRegistry.LoadLayoutGridFromRegistry(Me, Me.ogv)
    End Sub

#End Region

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs) Handles ocmsavelayout.Click
        HI.UL.AppRegistry.SaveLayoutGridToRegistry(Me, Me.ogv)
        HI.MG.ShowMsg.mInfo("Save Layout Grid Complete...", 1404240001, Me.Text, , System.Windows.Forms.MessageBoxIcon.Information)
    End Sub

    Private Sub ocmcal_Click(sender As Object, e As EventArgs) Handles ocmcalepworkover.Click
        If HI.UL.ULDate.CheckDate(FTDateRequest.Text) <> "" Then

            If HI.HRCAL.Time.CheckClosePeriod(FTDateRequest.Text) Then
                HI.MG.ShowMsg.mInfo("ไม่สามารถทำการแก้ไขข้อมูลหลังการปิดงวดได้ กรุณาทำการตรวจสอบ", 14040001, Me.Text)
                Exit Sub
            End If

            Call LoadTimeInWeekOverDataInfo()
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateRequest_lbl.Text)
            FTDateRequest.Focus()
        End If
    End Sub
End Class