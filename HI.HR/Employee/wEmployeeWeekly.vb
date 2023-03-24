Public Class wEmployeeWeekly 

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ActualDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

    End Sub

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
        Try
            If CType(ogc.DataSource, DataTable).Rows.Count > 0 Then

                If CType(ogc.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then
                    Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")
                    If Me.SaveData() Then
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                        ocmload_Click(ocmload, New System.EventArgs)
                    Else
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                    End If
                Else
                    HI.MG.ShowMsg.mInvalidData("", 1104030001, Me.Text)
                End If
            Else
                HI.MG.ShowMsg.mInvalidData("", 1104030001, Me.Text)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ProcessDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If CType(ogc.DataSource, DataTable).Rows.Count > 0 Then
            If CType(ogc.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then
                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบข้อมูลการเปลี่ยนกะใช่หรือไม่ ?", 1404300004) Then
                    Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
                    If Me.DeleteData() Then
                        _Spls.Close()
                        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mDelete, Me.Text)
                        HI.TL.HandlerControl.ClearControl(Me)
                        ocmload_Click(ocmload, New System.EventArgs)
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

        Me.ogc.DataSource = Nothing
        HI.TL.HandlerControl.ClearControl(Me)

    End Sub

    Private Sub ProcessPreview(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmpreview.Click
    End Sub

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Proc "
    Private Function SaveData() As Boolean

        Dim _Qry As String
        CType(ogc.DataSource, DataTable).AcceptChanges()
        Dim _Dt As DataTable = CType(ogc.DataSource, DataTable)
        Try

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _Dt.Select("FTSelect='1' AND (FTMonday='1' OR FTTuesday='1' OR FTWednesday='1' OR  FTThursday='1' OR FTFriday='1' OR FTSaturday='1' OR FTSunday='1') ")

                ' If R!FTSelect.ToString = "1" Then
                _Qry = " SELECT TOP 1 FNHSysEmpID  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly WITH (NOLOCK)"
                _Qry &= vbCrLf & " WHERE FNHSysEmpID =" & Val(R!FNHSysEmpID) & " "

                If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR, "") <> "" Then
                    _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly "
                    _Qry &= vbCrLf & "  SET  "
                    _Qry &= vbCrLf & " FTMonday='" & R!FTMonday.ToString & "'"
                    _Qry &= vbCrLf & " ,FTTuesday='" & R!FTTuesday.ToString & "'"
                    _Qry &= vbCrLf & " ,FTWednesday='" & R!FTWednesday.ToString & "'"
                    _Qry &= vbCrLf & " ,FTThursday='" & R!FTThursday.ToString & "'"
                    _Qry &= vbCrLf & " ,FTFriday='" & R!FTFriday.ToString & "'"
                    _Qry &= vbCrLf & " ,FTSaturday='" & R!FTSaturday.ToString & "'"
                    _Qry &= vbCrLf & " ,FTSunday='" & R!FTSunday.ToString & "'"
                    _Qry &= vbCrLf & " WHERE FNHSysEmpID =" & Val(R!FNHSysEmpID) & " "

                Else

                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly (  FTInsUser, FDInsDate, FTInsTime "
                    _Qry &= vbCrLf & "  , FNHSysEmpID, FTMonday, FTTuesday, FTWednesday,  FTThursday, FTFriday, FTSaturday, FTSunday) "
                    _Qry &= vbCrLf & " SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & "  "
                    _Qry &= vbCrLf & " ," & Val(R!FNHSysEmpID) & ""
                    _Qry &= vbCrLf & " ,'" & R!FTMonday.ToString & "'"
                    _Qry &= vbCrLf & " ,'" & R!FTTuesday.ToString & "'"
                    _Qry &= vbCrLf & " ,'" & R!FTWednesday.ToString & "'"
                    _Qry &= vbCrLf & " ,'" & R!FTThursday.ToString & "'"
                    _Qry &= vbCrLf & " ,'" & R!FTFriday.ToString & "'"
                    _Qry &= vbCrLf & " ,'" & R!FTSaturday.ToString & "'"
                    _Qry &= vbCrLf & " ,'" & R!FTSunday.ToString & "'"

                End If

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
                End If

                'End If
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

    End Function

    Private Function DeleteData() As Boolean
        Try
            CType(ogc.DataSource, DataTable).AcceptChanges()
            Dim _Dt As DataTable = CType(ogc.DataSource, DataTable)
            Dim _Qry  As  String = ""

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _Dt.Rows

                If R!FTSelect.ToString = "1" Then

                    _Qry = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployeeWeekly  "
                    _Qry &= vbCrLf & " WHERE FNHSysEmpID =" & Val(R!FNHSysEmpID) & " "

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

            Return True
        Catch ex As Exception
            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try

    End Function

    Private Sub LoadData()
        Me.ogc.DataSource = Nothing

        Dim _Dt As DataTable
        Dim _Qry  As  String = ""

        _Qry = " SELECT      '0' AS FTSelect,  M.FNHSysEmpID, M.FTEmpCode"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & " , P.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

        Else
            _Qry &= vbCrLf & " , P.FTPreNameNameEN + ' ' +  M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
        End If

        _Qry &= vbCrLf & ",ISNULL(MS.FTMonday,'0') As FTMonday, ISNULL(MS.FTTuesday,'0') As FTTuesday, ISNULL(MS.FTWednesday,'0') As FTWednesday, "
        _Qry &= vbCrLf & "ISNULL(MS.FTThursday,'0') As FTThursday, ISNULL(MS.FTFriday,'0') AS FTFriday, ISNULL(MS.FTSaturday,'0') AS FTSaturday, ISNULL(MS.FTSunday,'0') As FTSunday"


        _Qry &= vbCrLf & ",OrgSect.FTSectCode "
        _Qry &= vbCrLf & ",OrgUnitSect.FTUnitSectCode "
        _Qry &= vbCrLf & ",ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
        _Qry &= vbCrLf & ",ISNULL(Dept.FTDeptCode,'') AS FTDeptCode "
        _Qry &= vbCrLf & ",ISNULL(OrgDiv.FTDivisonCode,'') AS FTDivisonCode "


        _Qry &= vbCrLf & " FROM        THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        _Qry &= vbCrLf & "   THRMTimeShift AS SH WITH (NOLOCK) ON M.FNHSysShiftID = SH.FNHSysShiftID "
        _Qry &= vbCrLf & "   INNER Join "
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH(NOLOCK)  ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS OrgDiv WITH (NOLOCK) ON M.FNHSysDivisonId = OrgDiv.FNHSysDivisonId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS OrgSect WITH (NOLOCK) ON M.FNHSysSectId = OrgSect.FNHSysSectId "
        _Qry &= vbCrLf & "   LEFT OUTER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS OrgUnitSect WITH (NOLOCK) ON M.FNHSysUnitSectId = OrgUnitSect.FNHSysUnitSectId "
        _Qry &= vbCrLf & " 	 LEFT OUTER JOIN("
        _Qry &= vbCrLf & "		SELECT        FNHSysEmpID, FTMonday, FTTuesday, FTWednesday, "
        _Qry &= vbCrLf & "   FTThursday, FTFriday, FTSaturday, FTSunday"
        _Qry &= vbCrLf & "  FROM            THRMEmployeeWeekly "
        _Qry &= vbCrLf & " ) AS MS ON M.FNHSysEmpID =MS.FNHSysEmpID"
        _Qry &= vbCrLf & "  WHERE  M.FTEmpCode <> ''  "
        _Qry &= vbCrLf & "  AND  ( (ISNULL(M.FDDateEnd,'') ='' OR ISNULL(M.FDDateEnd,'') >= Convert(varchar(10),DateAdd(day,-45,Getdate()),111 ) ) ) "
        _Qry &= vbCrLf & "  AND  M.FNHSysCmpId  =" & HI.ST.SysInfo.CmpID & "  "

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

        _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)

        _Qry &= vbCrLf & " ORDER BY  M.FTEmpCode  "

        _Dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        Me.ogc.DataSource = _Dt
    End Sub
#End Region

    Private Sub ocmload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmload.Click
        Call LoadData()
    End Sub

    Private Sub wEmployeeWeekly_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If FNHSysEmpTypeId.Enabled Then
            FNHSysEmpTypeId.Focus()
        End If
    End Sub

    Private Sub wEmployeeWeekly_BackgroundImageLayoutChanged(sender As Object, e As System.EventArgs) Handles Me.BackgroundImageLayoutChanged

    End Sub

    Private Sub wEmployeeWeekly_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode
        Call LoadData()
    End Sub

    Private Sub ochkselectall_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles ochkselectall.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ochkselectall.Checked Then
                _State = "1"
            End If

            With ogc
                If Not (.DataSource Is Nothing) Then
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

    Private Sub ochkselectallbydate_CheckedChanged(sender As Object, e As EventArgs) Handles ochkselectallbydate.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.ochkselectallbydate.Checked Then
                _State = "1"
            End If

            Dim _FieldName As String = ""
            Select Case FNDayName.SelectedIndex
                Case 0
                    _FieldName = "FTSunday"
                Case 1
                    _FieldName = "FTMonday"
                Case 2
                    _FieldName = "FTTuesday"
                Case 3
                    _FieldName = "FTWednesday"
                Case 4
                    _FieldName = "FTThursday"
                Case 5
                    _FieldName = "FTFriday"
                Case 6
                    _FieldName = "FTSaturday"
            End Select

            '_Qry &= vbCrLf & ",ISNULL(MS.FTMonday,'0') As FTMonday, ISNULL(MS.FTTuesday,'0') As FTTuesday, ISNULL(MS.FTWednesday,'0') As FTWednesday, "
            '_Qry &= vbCrLf & "ISNULL(MS.FTThursday,'0') As FTThursday, ISNULL(MS.FTFriday,'0') AS FTFriday, ISNULL(MS.FTSaturday,'0') AS FTSaturday, ISNULL(MS.FTSunday,'0') As FTSunday"


            With ogc
                If Not (.DataSource Is Nothing) Then
                    With ogv
                        For I As Integer = 0 To .RowCount - 1
                            .SetRowCellValue(I, .Columns.ColumnByFieldName(_FieldName), _State)
                        Next
                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub
End Class