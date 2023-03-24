Public Class wEmployeeOTPerWeek

    Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ActualDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")

        Call InitGrid()

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

    End Sub

    Private Sub ProcessDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If CType(ogc.DataSource, DataTable).Rows.Count > 0 Then
            If CType(ogc.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then
                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบข้อมูลใช่หรือไม่ ?", 1407170004) Then
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

            For Each R As DataRow In _Dt.Select("FTSelect='1'")


                _Qry = " SELECT TOP 1 FNHSysEmpID  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgEmployeeOTPerWeek WITH (NOLOCK)"
                _Qry &= vbCrLf & " WHERE FNHSysEmpID =" & Val(R!FNHSysEmpID) & " "

                If HI.Conn.SQLConn.GetFieldOnBeginTrans(_Qry, Conn.DB.DataBaseName.DB_HR, "") <> "" Then
                    _Qry = " Update [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgEmployeeOTPerWeek "
                    _Qry &= vbCrLf & "  SET  "
                    _Qry &= vbCrLf & " FNOTPerWeek=" & FNOTPerWeek.Value & ""
                    _Qry &= vbCrLf & " ,FTStateLockNotOver='" & FTStateLockNotOver.EditValue.ToString & "'"
                    _Qry &= vbCrLf & " ,FNStateOTPer=" & FNStateOTPer.SelectedIndex & ""
                    _Qry &= vbCrLf & " WHERE FNHSysEmpID =" & Val(R!FNHSysEmpID) & " "

                Else

                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgEmployeeOTPerWeek (  FTInsUser, FDInsDate, FTInsTime "
                    _Qry &= vbCrLf & "  , FNHSysEmpID,FNOTPerWeek,FTStateLockNotOver,FNStateOTPer) "
                    _Qry &= vbCrLf & " SELECT '" & HI.ST.UserInfo.UserName & "'," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB & "  "
                    _Qry &= vbCrLf & " ," & Val(R!FNHSysEmpID) & ""
                    _Qry &= vbCrLf & " ," & FNOTPerWeek.Value & ""
                    _Qry &= vbCrLf & " ,'" & FTStateLockNotOver.EditValue.ToString & "'"
                    _Qry &= vbCrLf & " ," & FNStateOTPer.SelectedIndex & ""

                End If

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    Return False
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

    Private Function DeleteData() As Boolean
        Try
            CType(ogc.DataSource, DataTable).AcceptChanges()
            Dim _Dt As DataTable = CType(ogc.DataSource, DataTable)
            Dim _Qry As String = ""

            HI.Conn.SQLConn._ConnString = HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_HR)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            For Each R As DataRow In _Dt.Rows

                If R!FTSelect.ToString = "1" Then

                    _Qry = " DELETE  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgEmployeeOTPerWeek  "
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
        Dim _Qry As String = ""

        _Qry = " SELECT      '0' AS FTSelect,  M.FNHSysEmpID, M.FTEmpCode"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & " ,P.FTPreNameNameTH + ' ' +  M.FTEmpNameTH + '  ' +  M.FTEmpSurnameTH AS FTEmpName"

            _Qry &= vbCrLf & " ,ISNULL(LDX.FTNameTH,'') AS FNStateOTPer"
        Else
            _Qry &= vbCrLf & " ,P.FTPreNameNameEN + ' ' +  M.FTEmpNameEN + '  ' +  M.FTEmpSurnameEN AS FTEmpName"
            _Qry &= vbCrLf & " ,ISNULL(LDX.FTNameEN,'') AS FNStateOTPer"
        End If

        _Qry &= vbCrLf & ",MS.FNOTPerWeek"
        _Qry &= vbCrLf & ",ISNULL(MS.FTStateLockNotOver,'0') AS FTStateLockNotOver"
        _Qry &= vbCrLf & ",ISNULL(OrgSect.FTSectCode,'') AS FTSectCode "
        _Qry &= vbCrLf & ",ISNULL(OrgUnitSect.FTUnitSectCode,'') AS FTUnitSectCode "
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
        _Qry &= vbCrLf & "		SELECT        FNHSysEmpID, FNOTPerWeek,FTStateLockNotOver,FNStateOTPer"
        _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMCfgEmployeeOTPerWeek WITH(NOLOCK) "
        _Qry &= vbCrLf & " ) AS MS ON M.FNHSysEmpID =MS.FNHSysEmpID"

        _Qry &= vbCrLf & " 	 LEFT OUTER JOIN("
        _Qry &= vbCrLf & "		SELECT       FNListIndex, FTNameTH, FTNameEN"
        _Qry &= vbCrLf & "  FROM     [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK) WHERE FTListName='FNStateOTPer' "
        _Qry &= vbCrLf & " ) AS LDX ON MS.FNStateOTPer =LDX.FNListIndex"


        _Qry &= vbCrLf & "  WHERE  M.FTEmpCode <> ''  AND ISNULL(M.FNPaidOTSta,0) = 0    "
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
End Class