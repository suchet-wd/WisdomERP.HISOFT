
Imports System.Linq

Public Class wChangeHoliday
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ActualDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

    End Sub

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
            Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
            Try
                If Me.SaveData(_Spls) Then
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    Me.ocmload_Click(ocmload, New System.EventArgs)
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If
            Catch ex As Exception
                _Spls.Close()
            End Try
        End If
    End Sub

    Private Sub ProcessDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If CType(ogc.DataSource, DataTable).Rows.Count > 0 Then
            If CType(ogc.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then
                If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mDelete, Me.Text) = True Then
                    Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
                    If Me.DeleteData(_Spls) Then
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                        Me.ocmload_Click(ocmload, New System.EventArgs)
                        HI.TL.HandlerControl.ClearControl(Me)
                    Else
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                    End If
                End If
            Else
                HI.MG.ShowMsg.mInvalidData("", 1104030001, Me.Text)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData("", 1104030001, Me.Text)
        End If
    End Sub

    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click
        'Me.FormRefresh()

        Me.ogc.DataSource = Nothing
        ochkselectall.Checked = False

        HI.TL.HandlerControl.ClearControl(Me)
        Me.FTDateStart.Focus()

    End Sub

    Private Sub ProcessPreview(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmpreview.Click
    End Sub

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Procedure "

    Private Function SaveData(ByVal objspl As HI.TL.SplashScreen) As Boolean

        Dim _Qry As String
        CType(ogc.DataSource, DataTable).AcceptChanges()
        Dim _dtMain As DataTable = CType(ogc.DataSource, DataTable)
        Dim _Dt As DataTable = _dtMain.Clone

        Dim _TotalHour As Double = 0
        Dim _Rest As Double = 0
        Dim _FNTotalMonute As Double = 0
        Dim _TotalHour2 As Double = 0
        Dim _Rest2 As Double = 0
        Dim _FNTotalMonute2 As Double = 0
        Dim _TotalNetHour As Double = 0
        Dim _FNTotalNetMonute As Double = 0

        Dim _EndProcDate As String = ""
        Dim _NextProcDate As String = ""

        For Each R As DataRow In _dtMain.Rows
            If R!FTSelect.ToString = "1" Then
                If _Dt.Select("FNHSysEmpID=" & Val(R!FNHSysEmpID.ToString) & "").Length <= 0 Then
                    _Dt.Rows.Add(R.ItemArray())
                End If
            End If
        Next

        _dtMain.Dispose()

        Try

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _Dt.Rows

                If R!FTSelect.ToString = "1" Then



                    _EndProcDate = HI.UL.ULDate.ConvertEnDB(Me.FTDateEnd.Text)
                    _NextProcDate = HI.UL.ULDate.ConvertEnDB(Me.FTDateStart.Text)



                    objspl.UpdateInformation("Saving Employee " & R!FTEmpCode.ToString & "   Date " & HI.UL.ULDate.ConvertEN(_NextProcDate))

                    _Qry = " SELECT TOP 1 FDShiftDate  FROM dbo. THRTChageHoliday  WITH (NOLOCK)"
                    _Qry &= vbCrLf & " WHERE FNHSysEmpID =" & Val(R!FNHSysEmpID) & " "
                    _Qry &= vbCrLf & " AND FDHolidayDate = '" & HI.UL.ULDate.ConvertEnDB(_NextProcDate) & "'"

                    If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR, "") <> "" Then

                        _Qry = " Update  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTChageHoliday  "
                        _Qry &= vbCrLf & "  SET  FTUpdUser='" & HI.ST.UserInfo.UserName & "' "
                        _Qry &= vbCrLf & " , FDUpdDate=" & HI.UL.ULDate.FormatDateDB & ", FTUpdTime=" & HI.UL.ULDate.FormatTimeDB & "  "
                        _Qry &= vbCrLf & " ,FDChangeToDate='" & HI.UL.ULDate.ConvertEnDB(_EndProcDate) & "'  "
                        _Qry &= vbCrLf & " WHERE FNHSysEmpID =" & Val(R!FNHSysEmpID) & " "
                        _Qry &= vbCrLf & " AND FDHolidayDate = '" & HI.UL.ULDate.ConvertEnDB(_NextProcDate) & "'"

                    Else

                        _Qry = " INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTChageHoliday  ( FTInsUser, FDInsDate, FTInsTime "
                        _Qry &= vbCrLf & "  , FNHSysEmpID, FDChangeToDate,FDHolidayDate) "
                        _Qry &= vbCrLf & " SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & "  "
                        _Qry &= vbCrLf & " ," & Val(R!FNHSysEmpID) & ""
                        _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(_EndProcDate) & "','" & HI.UL.ULDate.ConvertEnDB(_NextProcDate) & "' "

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

            For Each R As DataRow In _Dt.Rows
                If R!FTSelect.ToString = "1" Then

                    _EndProcDate = HI.UL.ULDate.ConvertEnDB(Me.FTDateEnd.Text)
                    _NextProcDate = HI.UL.ULDate.ConvertEnDB(Me.FTDateStart.Text)


                    objspl.UpdateInformation("Calculate Work Time " & R!FTEmpCode.ToString & "   Date " & HI.UL.ULDate.ConvertEN(_NextProcDate))

                    HI.HRCAL.Calculate.TransTimeCard(HI.ST.UserInfo.UserName, HI.UL.ULDate.ConvertEnDB(_NextProcDate), Val(R!FNHSysEmpID))
                    _NextProcDate = HI.UL.ULDate.ConvertEnDB(HI.UL.ULDate.AddDay(_NextProcDate, 1))

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

    Private Function DeleteData(ByVal objspl As HI.TL.SplashScreen) As Boolean
        Try

            CType(ogc.DataSource, DataTable).AcceptChanges()
            Dim _Dt As DataTable = CType(ogc.DataSource, DataTable)
            Dim _Qry As String = ""
            Dim _EndProcDate As String = ""
            Dim _NextProcDate As String = ""

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _Dt.Rows

                If R!FTSelect.ToString = "1" And R!FDShiftDate.ToString <> "" Then


                    objspl.UpdateInformation("Deleting Employee " & R!FTEmpCode.ToString & "   Date " & HI.UL.ULDate.ConvertEN(R!FDShiftDate.ToString))

                    _Qry = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTChageHoliday  "
                    _Qry &= vbCrLf & " WHERE FNHSysEmpID =" & Val(R!FNHSysEmpID) & " "
                    _Qry &= vbCrLf & " AND FDHolidayDate = '" & HI.UL.ULDate.ConvertEnDB(R!FDShiftDate.ToString) & "'"

                    HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

  

                End If
            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            For Each R As DataRow In _Dt.Rows
                If R!FTSelect.ToString = "1" And R!FDHolidayDate.ToString <> "" Then
       
                    objspl.UpdateInformation("Calculate Work Time " & R!FTEmpCode.ToString & "   Date " & HI.UL.ULDate.ConvertEN(R!FDHolidayDate.ToString))
                    HI.HRCAL.Calculate.TransTimeCard(HI.ST.UserInfo.UserName, HI.UL.ULDate.ConvertEnDB(R!FDHolidayDate.ToString), Val(R!FNHSysEmpID))

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

    Private Sub LoadData()
        If HI.UL.ULDate.CheckDate(FTDateStart.Text) <> "" Then
            Me.ogc.DataSource = Nothing
            ochkselectall.Checked = False

            Dim _Dt As DataTable
            Dim _Qry  As  String = ""

            _Qry = " SELECT  '0' AS FTSelect,  M.FNHSysEmpID, M.FTEmpCode"

            If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                _Qry &= vbCrLf & " , P.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

            Else
                _Qry &= vbCrLf & " , P.FTPreNameNameEN + ' ' +  M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
            End If

            _Qry &= vbCrLf & " , CASE WHEN ISDATE(MS.FDHolidayDate) =1 THEN  Convert(varchar(10),Convert(datetime,MS.FDHolidayDate),103)   ELSE '' END  AS FDHolidayDate "
            _Qry &= vbCrLf & " ,CASE WHEN ISDATE(MS.FDChangeToDate) =1 THEN  Convert(varchar(10),Convert(datetime,MS.FDChangeToDate),103)   ELSE '' END  AS FDChangeToDate "
            _Qry &= vbCrLf & " FROM        THRMEmployee AS M WITH (NOLOCK)"
            _Qry &= vbCrLf & "    INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
            _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS OrgDiv WITH (NOLOCK) ON M.FNHSysDivisonId = OrgDiv.FNHSysDivisonId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS OrgSect WITH (NOLOCK) ON M.FNHSysSectId = OrgSect.FNHSysSectId "
            _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS OrgUnitSect WITH (NOLOCK) ON M.FNHSysUnitSectId = OrgUnitSect.FNHSysUnitSectId "
            _Qry &= vbCrLf & "  LEFT OUTER JOIN "
            _Qry &= vbCrLf & " ( SELECT S.*  "


            _Qry &= vbCrLf & " FROM   THRTChageHoliday  S  WITH (NOLOCK)"

            If Me.FTDateStart.Text <> "" And Me.FTDateEnd.Text <> "" Then
                _Qry &= vbCrLf & "  WHERE FDHolidayDate >='" & HI.UL.ULDate.ConvertEnDB(Me.FTDateStart.Text) & "' "
                _Qry &= vbCrLf & "  AND FDHolidayDate <='" & HI.UL.ULDate.ConvertEnDB(Me.FTDateEnd.Text) & "'  "
            Else
                _Qry &= vbCrLf & "  WHERE FDHolidayDate ='' "
            End If

            _Qry &= vbCrLf & "  "
            _Qry &= vbCrLf & ")  AS MS  ON MS.FNHSysEmpID = M.FNHSysEmpID  "

            ''Dim _Qry2 As String = ""

            _Qry &= vbCrLf & " WHERE  M.FTEmpCode <> ''   "
            _Qry &= vbCrLf & " AND M.FDDateStart <='" & HI.UL.ULDate.ConvertEnDB(FTDateStart.Text) & "' "
            _Qry &= vbCrLf & " AND (M.FDDateEnd ='' OR M.FDDateEnd >'" & HI.UL.ULDate.ConvertEnDB(FTDateStart.Text) & "' )   "

            _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)

            If Me.FTDateStart.Text <> "" And Me.FTDateEnd.Text <> "" Then
            Else
                _Qry &= vbCrLf & "  AND  M.FTEmpCode <> M.FTEmpCode  "
            End If

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
                _Qry &= vbCrLf & " AND  OrgDiv.FTDivisonCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonId.Text) & "' "
            End If

            If Me.FNHSysDivisonIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  OrgDiv.FTDivisonCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysDivisonIdTo.Text) & "' "
            End If

            '------Criteria By Sect
            If Me.FNHSysSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND  OrgSect.FTSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectId.Text) & "' "
            End If

            If Me.FNHSysSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND  OrgSect.FTSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysSectIdTo.Text) & "' "
            End If

            '------Criteria Unit Sect
            If Me.FNHSysUnitSectId.Text <> "" Then
                _Qry &= vbCrLf & " AND   OrgUnitSect.FTUnitSectCode>='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectId.Text) & "' "
            End If

            If Me.FNHSysUnitSectIdTo.Text <> "" Then
                _Qry &= vbCrLf & " AND   OrgUnitSect.FTUnitSectCode<='" & HI.UL.ULF.rpQuoted(Me.FNHSysUnitSectIdTo.Text) & "' "
            End If



            _Qry &= vbCrLf & "  ORDER BY M.FTEmpCode ASC "

            _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

            Me.ogc.DataSource = _Dt

        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateStart_lbl.Text)
            FTDateStart.Focus()
        End If
    End Sub
#End Region

#Region "General"

    Private Function VerrifyData() As Boolean
        Dim _Pass As Boolean = False
        If HI.UL.ULDate.CheckDate(FTDateStart.Text) <> "" Then
            If HI.UL.ULDate.CheckDate(FTDateEnd.Text) <> "" Then

                If Not (ogc.DataSource Is Nothing) Then
                    If CType(ogc.DataSource, DataTable).Rows.Count > 0 Then
                        If CType(ogc.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then
                            _Pass = True
                        Else
                            HI.MG.ShowMsg.mInvalidData("", 1104030001, Me.Text)
                            FTDateStart.Focus()
                        End If
                    Else
                        HI.MG.ShowMsg.mInvalidData("", 1104030001, Me.Text)
                        FTDateStart.Focus()
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData("", 1104030001, Me.Text)
                    FTDateStart.Focus()
                End If

            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateEnd_lbl.Text)
                FTDateEnd.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTDateStart_lbl.Text)
            FTDateStart.Focus()
        End If

        Return _Pass
    End Function

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        Call LoadData()
    End Sub

    Private Sub FTDateRequest_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles FTDateStart.EditValueChanged, FTDateEnd.EditValueChanged
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
                If Not (.DataSource Is Nothing) Then

                    For Each R As DataRow In CType(.DataSource, DataTable).Rows
                        R!FTSelect = _State
                    Next

                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub wChangeHoliday_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
    End Sub

#End Region

End Class