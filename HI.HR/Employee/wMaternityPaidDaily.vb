Imports DevExpress.XtraEditors.Controls

Public Class wMaternityPaidDaily
    Private _LstReport As HI.RP.ListReport
    Sub New()

        _ProcPrepare = True
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _Actualdate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),GETDATE(),111)", Conn.DB.DataBaseName.DB_HR, "")
        _ActualNextDate = HI.Conn.SQLConn.GetField("SELECT  CONVERT(varchar(10),DateAdd(day,1,GETDATE()),111)", Conn.DB.DataBaseName.DB_HR, "")


        _ProcPrepare = False

    End Sub

    Private _ProcPrepare As Boolean = False

    Private _Actualdate As String = ""
    ReadOnly Property Actualdate As String
        Get
            Return _Actualdate
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

    Private Function VerifyLeave() As Boolean

        Dim _Qry As String = ""
        Dim _EndDate As String = ""

        _Qry = " SELECT TOP 1  Convert(nvarchar(10),DateAdd(Day,-1,Convert(DateTime,FTStartDate)),111) AS FTStartDate  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTLeaveAdvanceDaily AS X WITH(NOLOCK) "
        _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & " AND FTStartDate <'" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
        _Qry &= vbCrLf & " And FTLeaveType = '97'"
        _Qry &= vbCrLf & " AND Left(FTStartDate,4) = left('" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "',4)"

        _EndDate = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")

        If _EndDate <> "" Then
            Me.FTEndDate.DateTime = _EndDate

        End If

        Return True
    End Function
    Private Sub ProcessSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click

        If Me.VerrifyData Then

            Dim _Msg As String = ""
            Dim _Qry As String = ""

            Dim _State As Boolean = False
            Call VerifyLeave()
            _State = HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการบันทึกข้อมูล พนักงานท้อง ได้เงิน รายวัน ใช่หรือไม่ ?", 1604257801, FNHSysEmpId_None.Text)

            If (_State) Then
                Dim _Spls As New HI.TL.SplashScreen("Saving...   Please Wait   ")

                If Me.SaveData() Then

                    HI.TL.HandlerControl.ClearControl(ogbheader)
                    Me.ogc.DataSource = Nothing
                    Me.FTEmpPicName.Image = Nothing
                    FTStartDate.Text = ""
                    FTEndDate.Text = ""
                    Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

                    Call LoadDataEmployeeLeaveHistory()

                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

                Else
                    _Spls.Close()
                    HI.MG.ShowMsg.mProcessNotComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)
                End If

            End If
        End If
    End Sub

    Private Sub ProcessDelete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click
        If Me.VerrifyData Then

            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการลบข้อมูล พนักงานท้อง ได้เงิน รายวัน ช่หรือไม่ ?", 1604257802, FNHSysEmpId_None.Text) Then
                Dim _Spls As New HI.TL.SplashScreen("Deleting...   Please Wait   ")
                If Me.DeleteData() Then

                    HI.TL.HandlerControl.ClearControl(ogbheader)
                    Me.ogc.DataSource = Nothing
                    Me.FTEmpPicName.Image = Nothing
                    FTStartDate.Text = ""
                    FTEndDate.Text = ""
                    Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

                    Call LoadDataEmployeeLeaveHistory()
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

        HI.TL.HandlerControl.ClearControl(ogbheader)
        Me.ogc.DataSource = Nothing
        Me.FTEmpPicName.Image = Nothing
        FTStartDate.Text = ""
        FTEndDate.Text = ""
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

        Call LoadDataEmployeeLeaveHistory()

    End Sub

    Private Sub ProcessClose(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

#End Region

#Region " Procedure "

    Private Function SaveData() As Boolean
        Try
            Dim _Qry As String
            Dim _Seq As Integer = 0
            _Qry = "Select Max(FNSeq) as FNSeq  From [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTMaternityPaidDaily with(nolock)  "
            _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
            _Seq = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, 0)


            Dim _PicName As String = ""
            _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTMaternityPaidDaily SET"
            _Qry &= vbCrLf & " FTUpdUser = '" & HI.ST.UserInfo.UserName & "'"
            _Qry &= vbCrLf & " ,FTUpdDate = " & HI.UL.ULDate.FormatDateDB
            _Qry &= vbCrLf & " ,FTUpdTime = " & HI.UL.ULDate.FormatTimeDB
            _Qry &= vbCrLf & " ,FTNote = N'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
            _Qry &= vbCrLf & " ,FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            _Qry &= vbCrLf & " ,FTEndDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
            _Qry &= vbCrLf & " WHERE FNHSysEmpID = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
            _Qry &= vbCrLf & " AND FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
            _Qry &= vbCrLf & " AND FNSeq =" & _Seq


            If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then

                _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTMaternityPaidDaily (FTInsUser, FTInsDate, FTInsTime"
                _Qry &= vbCrLf & " , FNHSysEmpID, FTStartDate, FTEndDate,FTNote , FNSeq)"
                _Qry &= vbCrLf & " SELECT '" & HI.ST.UserInfo.UserName & "'"
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & "," & HI.UL.ULDate.FormatTimeDB
                _Qry &= vbCrLf & " ,'" & Val(FNHSysEmpId.Properties.Tag.ToString) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"
                _Qry &= vbCrLf & " ,'" & HI.UL.ULDate.ConvertEnDB(Me.FTEndDate.Text) & "'"
                _Qry &= vbCrLf & " ,N'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "'"
                _Qry &= vbCrLf & " , " & _Seq + 1

                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then
                    Return False
                End If

            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function DeleteData() As Boolean
        Try
            Dim _Qry As String

            _Qry = "DELETE FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTMaternityPaidDaily"
            _Qry &= vbCrLf & " WHERE FNHSysEmpId = " & Val(FNHSysEmpId.Properties.Tag.ToString) & ""
            _Qry &= vbCrLf & " AND FTStartDate = '" & HI.UL.ULDate.ConvertEnDB(Me.FTStartDate.Text) & "'"

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function VerrifyData() As Boolean
        Dim _Pass As Boolean = False

        If Me.FNHSysEmpId.Text <> "" And FNHSysEmpId.Properties.Tag.ToString <> "" Then

            FTStartDate.Text = HI.UL.ULDate.ConvertEN(FTStartDate.Text)
            FTEndDate.Text = HI.UL.ULDate.ConvertEN(FTEndDate.Text)

            If HI.UL.ULDate.CheckDate(FTStartDate.Text) <> "" And HI.UL.ULDate.CheckDate(FTEndDate.Text) <> "" Then

                _Pass = True

            Else
                HI.MG.ShowMsg.mInvalidData("ไม่พบจำนวนวัน กรุณาทำการระบุข้อมูลวันให้ถูกต้อง !!!", 1304050001, Me.Text)
                If HI.UL.ULDate.CheckDate(FTStartDate.Text) = "" Then
                    FTStartDate.Focus()
                ElseIf HI.UL.ULDate.CheckDate(FTEndDate.Text) = "" Then
                    FTEndDate.Focus()
                End If
            End If
        Else

            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysEmpId_lbl.Text)
            FNHSysEmpId.Focus()

        End If

        Return _Pass
    End Function

#End Region

#Region "General"

#End Region

    Private Sub wLeave_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _ProcClick = False
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpCode

        Call LoadDataEmployeeLeaveHistory()

    End Sub

    Private Sub LoadDataEmployeeLeaveHistory()
        Dim _Qry As String = ""
        Dim _dt As DataTable


        _Qry = " SELECT A.* "
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry = _Qry & ",Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge -(A.FNMonthWorkAge % 12) )  /12)) + ' ปี ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge % 12  )) ) + ' เดือน' AS FTWorkAge "
            _Qry = _Qry & ",Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge -(A.FNMonthEmpAge % 12) )  /12)) + ' ปี ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge % 12  )) ) + ' เดือน' AS FTEmpAge "
        Else
            _Qry = _Qry & ",Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge -(A.FNMonthWorkAge % 12) )  /12)) + ' Year ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthWorkAge % 12  )) ) + ' Month' AS FTWorkAge "
            _Qry = _Qry & ",Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge -(A.FNMonthEmpAge % 12) )  /12)) + ' Year ' + Convert(varchar(30),Convert(numeric(18,0),(A.FNMonthEmpAge % 12  )) ) + ' Month' AS FTEmpAge "
        End If

        _Qry = _Qry & " FROM ( SELECT       M.FNHSysEmpID, M.FTEmpCode"

        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal Then
            _Qry &= vbCrLf & "  ,P.FTPreNameNameTH AS FTPreNameName"
            '_Qry &= vbCrLf & "  ,P.FTPreNameNameTH + ' ' + M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"
            _Qry &= vbCrLf & "  ,M.FTEmpNameTH + '  ' + M.FTEmpSurnameTH AS FTEmpName"
            _Qry &= vbCrLf & "  ,ES.FTNameTH  AS FTEmpStatusName "
            _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameTH  AS FTEmpTypeName "
            _Qry &= vbCrLf & "  ,Dept.FTDeptDescTH  AS FTDeptName "
            _Qry &= vbCrLf & "  ,DI.FTDivisonNameTH  AS FTDivisonName "
            _Qry &= vbCrLf & "  ,ST.FTSectNameTH  AS FTSectName "
            _Qry &= vbCrLf & "  ,US.FTUnitSectNameTH  AS FTUnitSectName "
            _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameTH  AS FTPositName "
            _Qry &= vbCrLf & "  ,MNT.FTNationalityNameTH  AS FTNationalityName "
            _Qry &= vbCrLf & "  ,MSX.FTNameTH  AS FTSexName "

        Else
            _Qry &= vbCrLf & "  ,P.FTPreNameNameEN AS FTPreNameName"
            '_Qry &= vbCrLf & "  ,P.FTPreNameNameEN + ' ' + M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpName"
            _Qry &= vbCrLf & "  ,M.FTEmpNameEN + '  ' + M.FTEmpSurnameEN AS FTEmpName"
            _Qry &= vbCrLf & "  ,ES.FTNameEN  AS FTEmpStatusName "
            _Qry &= vbCrLf & "  ,ET.FTEmpTypeNameEN  AS FTEmpTypeName "
            _Qry &= vbCrLf & "  ,Dept.FTDeptDescEN  AS FTDeptName "
            _Qry &= vbCrLf & "  ,DI.FTDivisonNameEN  AS FTDivisonName "
            _Qry &= vbCrLf & "  ,ST.FTSectNameEN  AS FTSectName "
            _Qry &= vbCrLf & "  ,US.FTUnitSectNameEN  AS FTUnitSectName "
            _Qry &= vbCrLf & "  ,OrgPosit.FTPositNameEN  AS FTPositName "
            _Qry &= vbCrLf & "  ,MNT.FTNationalityNameEN  AS FTNationalityName "
            _Qry &= vbCrLf & "  ,MSX.FTNameEN  AS FTSexName "

        End If

        _Qry &= vbCrLf & " , ISNULL(ET.FTEmpTypeCode,'') AS FTEmpTypeCode "
        _Qry &= vbCrLf & " , ISNULL(Dept.FTDeptCode,'') AS FTDeptCode, ISNULL(DI.FTDivisonCode,'') AS FTDivisonCode"
        _Qry &= vbCrLf & " , ISNULL(ST.FTSectCode,'') AS FTSectCode,ISNULL(US.FTUnitSectCode,'') AS FTUnitSectCode"
        _Qry &= vbCrLf & " , OrgPosit.FTPositCode, M.FNEmpStatus,M.FTEmpCodeRefer,M.FTEmpIdNo"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(M.FDDateStart) =1 THEN CONVERT(varchar(10),Convert(datetime,M.FDDateStart),103) ELSE '' END AS FDDateStart"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(M.FDDateEnd) =1 THEN CONVERT(varchar(10),Convert(datetime,M.FDDateEnd),103) ELSE '' END AS FDDateEnd"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(M.FDBirthDate) =1 THEN CONVERT(varchar(10),Convert(datetime,M.FDBirthDate),103) ELSE '' END AS FDBirthDate"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(MPDaily.FTStartDate) =1 THEN Convert(datetime,MPDaily.FTStartDate) ELSE NULL END AS  FTStartDate"
        _Qry &= vbCrLf & ",CASE WHEN ISDATE(MPDaily.FTEndDate) =1 THEN Convert(datetime,MPDaily.FTEndDate) ELSE NULL END AS   FTEndDate"
        _Qry &= vbCrLf & ",MPDaily.FTNote , MPDaily.FNSeq"
        _Qry &= vbCrLf & " , [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_Get_Emp_WorkAge(M.FDDateStart,M.FDDateEnd) AS FNMonthWorkAge"
        _Qry &= vbCrLf & " , [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.FN_Get_Emp_Age(M.FDBirthDate) AS FNMonthEmpAge"
        _Qry &= vbCrLf & "  FROM            [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMPrename AS P WITH (NOLOCK) ON M.FNHSysPreNameId = P.FNHSysPreNameId INNER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS Dept WITH (Nolock) ON M.FNHSysDeptId = Dept.FNHSysDeptId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS DI WITH (NOLOCK) ON M.FNHSysDivisonId = DI.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS ST WITH (NOLOCK) ON M.FNHSysSectId = ST.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS OrgPosit WITH (NOLOCK) ON M.FNHSysPositId = OrgPosit.FNHSysPositId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.V_MEmpStatus AS ES ON M.FNEmpStatus = ES.FNListIndex  LEFT OUTER JOIN "
        _Qry &= vbCrLf & "  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMNationality AS MNT WITH (NOLOCK) ON M.FNHSysNationalityId = MNT.FNHSysNationalityId"
        _Qry &= vbCrLf & " LEFT OUTER JOIN ("

        _Qry &= vbCrLf & "    SELECT FNListIndex, FTNameTH, FTNameEN"
        _Qry &= vbCrLf & " FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS X WITH(NOLOCK)"
        _Qry &= vbCrLf & " WHERE  (FTListName = N'FNEmpSex')"
        _Qry &= vbCrLf & " ) AS MSX ON  M.FNEmpSex=MSX.FNListIndex "
        _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRTMaternityPaidDaily AS MPDaily WITH(NOLOCK) ON M.FNHSysEmpID = MPDaily.FNHSysEmpID "

        _Qry &= vbCrLf & "  WHERE        (M.FTEmpCode <> '')"
        _Qry &= vbCrLf & "   AND  M.FNHSysCmpId =" & HI.ST.SysInfo.CmpID & "  "


        _Qry = HI.ST.Security.PermissionFilterEmployee(_Qry)
        _Qry = _Qry & " ) AS A "

        _Qry &= vbCrLf & "ORDER BY A.FNSeq , A.FTStartDate DESC"
        'FNLeaveSickType'
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        Me.ogc.DataSource = _dt

    End Sub

    Private Sub FNHSysEmpId_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FNHSysEmpId.EditValueChanged
        If (_ProcPrepare) Then Exit Sub

        If Me.InvokeRequired Then
            Me.Invoke(New HI.Delegate.Dele.FNHSysEmpID_EditValueChanged(AddressOf FNHSysEmpId_EditValueChanged), New Object() {sender, e})
        Else

            If FNHSysEmpId.Text <> "" Then
                Dim _Qry As String = "SELECT TOP 1 FNHSysEmpID  FROM   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee WITH(NOLOCK) WHERE FTEmpCode ='" & HI.UL.ULF.rpQuoted(FNHSysEmpId.Text) & "' "
                FNHSysEmpId.Properties.Tag = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "")
            End If

            Call LoadEmpInfo(FNHSysEmpId.Properties.Tag.ToString)

            Call LoadDataEmployeeLeaveHistory()
        End If

    End Sub


    Private Sub LoadEmpInfo(ByVal FNHSysEmpID As String)
        Dim _PathEmpPic As String
        _PathEmpPic = ""
        Dim cmdstring As String = "Select Top 1 FTCfgData FROM  " & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) & ".dbo.TSESystemConfig AS X WITH(NOLOCK) WHERE FTCfgName='PathEmpPic'"

        _PathEmpPic = HI.Conn.SQLConn.GetField(cmdstring, Conn.DB.DataBaseName.DB_SECURITY, "")


        Dim _dt As DataTable
        Dim _Qry As String = ""
        _Qry = " SELECT    TOP 1     M.FTEmpCode, M.FTEmpCodeRefer, M.FTEmpNameTH, M.FTEmpSurnameTH, M.FTEmpNicknameTH, M.FTEmpNameEN, M.FNHSysEmpTypeId, M.FNHSysDeptId, "
        _Qry &= vbCrLf & "   D.FTDeptCode, Di.FTDivisonCode, M.FNHSysDivisonId, M.FNHSysSectId, S.FTSectCode, ET.FTEmpTypeCode, M.FNHSysUnitSectId, US.FTUnitSectCode,"
        _Qry &= vbCrLf & "  M.FNHSysEmpID, M.FTEmpPicName, M.FNHSysPositId, P.FTPositCode"
        _Qry &= vbCrLf & "  FROM             [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMEmployee AS M WITH (NOLOCK) LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMPosition AS P WITH (NOLOCK) ON M.FNHSysPositId = P.FNHSysPositId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMUnitSect AS US WITH (NOLOCK) ON M.FNHSysUnitSectId = US.FNHSysUnitSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK) ON M.FNHSysEmpTypeId = ET.FNHSysEmpTypeId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMSect AS S WITH (NOLOCK) ON M.FNHSysSectId = S.FNHSysSectId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDivision AS Di WITH (NOLOCK) ON M.FNHSysDivisonId = Di.FNHSysDivisonId LEFT OUTER JOIN"
        _Qry &= vbCrLf & "   [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMDepartment AS D WITH (NOLOCK) ON M.FNHSysDeptId = D.FNHSysDeptId"
        _Qry &= vbCrLf & "  WHERE  M.FNHSysEmpID  =" & Val(FNHSysEmpID) & ""
        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

        FTEmpPicName.Image = Nothing
        If _dt.Rows.Count > 0 Then
            For Each R As DataRow In _dt.Rows

                If _PathEmpPic = "" Then
                    FTEmpPicName.Image = HI.UL.ULImage.LoadImage(HI.ST.SysInfo.SysPath & "EmpPicture\" & R!FTEmpPicName.ToString)
                Else
                    FTEmpPicName.Image = HI.UL.ULImage.LoadImage(_PathEmpPic & R!FTEmpPicName.ToString)
                End If
                FNHSysEmpTypeId.Text = R!FTEmpTypeCode.ToString
                FNHSysDeptId.Text = R!FTDeptCode.ToString
                FNHSysDivisonId.Text = R!FTDivisonCode.ToString
                FNHSysSectId.Text = R!FTSectCode.ToString
                FNHSysUnitSectId.Text = R!FTUnitSectCode.ToString
                FNHSysPositId.Text = R!FTPositCode.ToString
                FNHSysEmpTypeId.Properties.Tag = R!FNHSysEmpTypeId.ToString
            Next
        Else
            FNHSysEmpTypeId.Text = ""
            FNHSysDeptId.Text = ""
            FNHSysDivisonId.Text = ""
            FNHSysSectId.Text = ""
            FNHSysUnitSectId.Text = ""
            FNHSysPositId.Text = ""
            FNHSysEmpTypeId.Properties.Tag = "0"
        End If


    End Sub



    Private _ProcClick As Boolean
    Private Sub ogv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ogv.DoubleClick
        Try
            With ogv
                If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount - 1 Then Exit Sub
                _ProcClick = True
                Me.FNHSysEmpId.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTEmpCode").ToString

                Try
                    Me.FTStartDate.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTStartDate").ToString)
                Catch ex As Exception
                End Try

                Try
                    Me.FTEndDate.DateTime = HI.UL.ULDate.ConvertEnDB("" & .GetRowCellValue(.FocusedRowHandle, "FTEndDate").ToString)
                Catch ex As Exception
                End Try

                Me.FTRemark.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTLeaveNote").ToString
                Me.FTRemark.Focus()
                _ProcClick = False

            End With
        Catch ex As Exception
            _ProcClick = False
        End Try
        _ProcClick = False
    End Sub


    Private Sub FTStartDate_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles FTStartDate.Leave

        Try
            Try
                FTStartDate.DateTime = HI.UL.ULDate.ConvertEnDB(FTStartDate.Text)

            Catch ex As Exception
                FTStartDate.DateTime = Nothing
                FTStartDate.Text = ""
            End Try
        Catch ex As Exception
        End Try

        If FTEndDate.Text = "" Then
            Try
                FTEndDate.DateTime = HI.UL.ULDate.ConvertEnDB(FTStartDate.Text)
            Catch ex As Exception
                FTEndDate.DateTime = Nothing
                FTEndDate.Text = ""
            End Try
        End If
    End Sub

    Private Sub FTEndDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles FTEndDate.LostFocus

        If FTEndDate.Text <> "" And FTStartDate.Text <> "" Then
            If HI.UL.ULDate.ConvertEnDB(FTEndDate.Text) < HI.UL.ULDate.ConvertEnDB(FTStartDate.Text) Then
                Try
                    FTStartDate.DateTime = HI.UL.ULDate.ConvertEnDB(FTEndDate.Text)
                Catch ex As Exception
                    FTStartDate.DateTime = Nothing
                    FTStartDate.Text = ""
                End Try

            End If
        End If

    End Sub

    Private Sub FTEndDate_EditValueChanged(sender As Object, e As EventArgs) Handles FTEndDate.EditValueChanged

    End Sub

    Private Sub FTEndDate_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles FTEndDate.EditValueChanging

    End Sub
End Class