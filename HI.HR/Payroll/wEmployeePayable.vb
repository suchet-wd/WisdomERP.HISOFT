Public Class wEmployeePayable

    Sub New()
        _ProcPrepare = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ActualDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ProcPrepare = False
    End Sub

    Private _ProcPrepare As Boolean = False

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

#End Region

#Region "MAIN PROC"

    Private Sub ProcessSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click

        If Me.VerrifyData Then
            Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
            If Me.SaveData() Then

                ocmload_Click(ocmload, New System.EventArgs)

                _Spls.Close()
                HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

            Else
                _Spls.Close()
                HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
            End If

        End If
    End Sub

    Private Sub ProcessDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If Me.VerrifyData Then
            If HI.MG.ShowMsg.mConfirmProcess(MG.ShowMsg.ProcessType.mSave, Me.Text) = True Then
                Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
                If Me.DeleteData() Then

                    ocmload_Click(ocmload, New System.EventArgs)

                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                End If
            End If
        End If
    End Sub

    Private Sub ProcessClear(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click
        Me.ogc.DataSource = Nothing
        HI.TL.HandlerControl.ClearControl(Me)
        Me.FTPayYear.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTPayYear FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgPayHD WITH(NOLOCK) Order BY FTPayYear Desc", Conn.DB.DataBaseName.DB_HR, "")
    End Sub

    Private Sub ProcessPreview(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmpreview.Click
    End Sub

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region
    Private Function VerrifyData() As Boolean
        Dim _Pass As Boolean = False

        If Not (ogc.DataSource Is Nothing) Then
            If CType(ogc.DataSource, DataTable).Rows.Count > 0 Then
                If CType(ogc.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then
                    _Pass = True
                Else
                    HI.MG.ShowMsg.mInvalidData("", 1104030001, Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mInvalidData("", 1104030001, Me.Text)
            End If
        Else
            HI.MG.ShowMsg.mInvalidData("", 1104030001, Me.Text)
        End If

        Return _Pass

    End Function

#Region " Procedure "

    Private Function SaveData() As Boolean
        Try
            Dim _Qry As String
            CType(ogc.DataSource, DataTable).AcceptChanges()
            Dim _dt As DataTable = CType(ogc.DataSource, DataTable)

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try

                For Each R As DataRow In _dt.Rows

                    If R!FTSelect.ToString = "1" Then

                        _Qry = " UPDATE THRTPayRollPayable SET "
                        _Qry &= vbCrLf & "FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                        _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
                        _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
                        _Qry &= vbCrLf & " WHERE  FNHSysEmpID='" & Val(R!FNHSysEmpID.ToString) & "'"
                        _Qry &= vbCrLf & "    AND FTPayYear='" & Me.FTPayYear.Text & "'"
                        _Qry &= vbCrLf & "    AND FTPayTerm='" & Me.FTPayTerm.Text & "'"

                        If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                            _Qry = "INSERT INTO THRTPayRollPayable(FNHSysEmpID,FTPayYear, FTPayTerm "
                            _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
                            _Qry &= vbCrLf & " SELECT " & Val(R!FNHSysEmpID.ToString) & ",'" & Me.FTPayYear.Text & "','" & Me.FTPayTerm.Text & "'"

                            _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
                            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

                            If HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                                HI.Conn.SQLConn.Tran.Rollback()
                                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                                Return False
                            End If

                        End If
                    End If
                Next

                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return True

            Catch ex As Exception

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False

            End Try
        Catch ex As Exception

            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            Dim _Qry As String
            CType(ogc.DataSource, DataTable).AcceptChanges()
            Dim _dt As DataTable = CType(ogc.DataSource, DataTable)

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Try

                For Each R As DataRow In _dt.Rows

                    If R!FTSelect.ToString = "1" Then

                        _Qry = " Delete [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollPayable "
                        _Qry &= vbCrLf & " WHERE  FNHSysEmpID='" & Val(R!FNHSysEmpID.ToString) & "'"
                        _Qry &= vbCrLf & "    AND FTPayYear='" & Me.FTPayYear.Text & "'"
                        _Qry &= vbCrLf & "    AND FTPayTerm='" & Me.FTPayTerm.Text & "'"

                        HI.Conn.SQLConn.ExecuteTran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                    End If

                Next

                HI.Conn.SQLConn.Tran.Commit()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return True

            Catch ex As Exception

                HI.Conn.SQLConn.Tran.Rollback()
                HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

                Return False

            End Try
        Catch ex As Exception

            Return False
        End Try
    End Function

#End Region

#Region "General"

#End Region

    Private Sub FNHSysEmpTypeId_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNHSysEmpTypeId.EditValueChanged
        If (_ProcPrepare) Then Exit Sub
        ogc.DataSource = Nothing

    End Sub

    Private Sub FTPayYear_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FTPayYear.EditValueChanged
        If (_ProcPrepare) Then Exit Sub
        ogc.DataSource = Nothing
    End Sub

    Private Sub FTPayTerm_EditValueChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles FTPayTerm.EditValueChanged
        If (_ProcPrepare) Then Exit Sub
        ogc.DataSource = Nothing
    End Sub

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        If Me.FNHSysEmpTypeId.Text <> "" And Me.FNHSysEmpTypeId.Properties.Tag.ToString <> "" Then
            If Me.FTPayYear.Text <> "" Then
                If Me.FTPayTerm.Text <> "" Then
                    Me.ogc.DataSource = Nothing

                    Dim _Dt As DataTable
                    Dim _Qry  As  String = ""

                    _Qry = " SELECT      '0' AS FTSelect,  M.FNHSysEmpID, M.FTEmpCode"

                    If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
                        _Qry &= vbCrLf & " , P.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

                    Else
                        _Qry &= vbCrLf & " , P.FTPreNameNameEN + ' ' +  M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
                    End If

                    _Qry &= vbCrLf & " ,ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
                    _Qry &= vbCrLf & " ,ISNULL(Dept.FTDeptCode,'') AS FTDeptCode "
                    _Qry &= vbCrLf & " ,ISNULL(DI.FTDivisonCode,'') AS FTDivisonCode "
                    _Qry &= vbCrLf & " ,ISNULL(ST.FTSectCode,'') AS FTSectCode "
                    _Qry &= vbCrLf & " ,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode "

                    _Qry &= vbCrLf & ",(CASE WHEN OTR.FTPayYear IS NULL THEN '0' Else '1' END) AS FTPayable"
                    _Qry &= vbCrLf & " FROM        [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
                    _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMTimeShift AS SH WITH (NOLOCK) ON M.FNHSysShiftID = SH.FNHSysShiftID "
                    _Qry &= vbCrLf & "   INNER Join "
                    _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
                    _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
                    _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId "
                    _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId "
                    _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId "
                    _Qry &= vbCrLf & " 	 INNER JOIN("
                    _Qry &= vbCrLf & "		SELECT  FTPayYear,FTPayTerm,FNHSysEmpID"
                    _Qry &= vbCrLf & "	 FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRoll WITH (NOLOCK)"
                    _Qry &= vbCrLf & "  WHERE FTPayYear='" & Me.FTPayYear.Text & "' AND  FTPayTerm='" & Me.FTPayTerm.Text & "'  "
                    _Qry &= vbCrLf & " ) AS MS ON M.FNHSysEmpID =MS.FNHSysEmpID"
                    _Qry &= vbCrLf & " LEFT OUTER JOIN("
                    _Qry &= vbCrLf & "	SELECT      FTPayYear,FTPayTerm,FNHSysEmpID"
                    _Qry &= vbCrLf & "    FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTPayRollPayable WITH(NOLOCK) "


                    _Qry &= vbCrLf & "  WHERE FTPayYear='" & Me.FTPayYear.Text & "' AND  FTPayTerm='" & Me.FTPayTerm.Text & "'  "
                    _Qry &= vbCrLf & " ) AS OTR ON M.FNHSysEmpID = OTR.FNHSysEmpID"
                    _Qry &= vbCrLf & " WHERE  M.FDDateStart <='" & HI.UL.ULDate.ConvertEnDB(Me.FDCalDateEnd.Text) & "' "
                    _Qry &= vbCrLf & " AND (M.FDDateEnd ='' OR M.FDDateEnd >'" & HI.UL.ULDate.ConvertEnDB(FDCalDateBegin.Text) & "' )   "
                    _Qry &= vbCrLf & " AND ET.FTEmpTypeCode='" & HI.UL.ULF.rpQuoted(Me.FNHSysEmpTypeId.Text) & "' "
                    _Qry &= vbCrLf & " AND M.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & " "

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

                    _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)

                    _Qry &= vbCrLf & " ORDER BY  M.FTEmpCode  "

                    _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
                    Me.ogc.DataSource = _Dt

                Else
                    HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPayTerm_lbl.Text)
                    FTPayTerm.Focus()
                End If
            Else
                HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FTPayYear_lbl.Text)
                FTPayYear.Focus()
            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, FNHSysEmpTypeId_lbl.Text)
            FNHSysEmpTypeId.Focus()
        End If
    End Sub

    Private Sub wPayRollPayable_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Me.FTPayYear.Text = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTPayYear FROM  THRMCfgPayHD WITH(NOLOCK) Order BY FTPayYear Desc", Conn.DB.DataBaseName.DB_HR, "")
    End Sub

    Private Sub ocmsavelayout_Click(sender As Object, e As EventArgs)

    End Sub
End Class