Public Class wConfigTimeLateDeduct

    Private Sub LoadConfigLateSet()
        Dim _Qry As String = ""

        _Qry = "SELECT "
        _Qry &= vbCrLf & " Cast(ISNULL(A.FTCfgLateCode,0) AS numeric(18,0)) AS FTCfgLateCode"
        _Qry &= vbCrLf & ",Cast(ISNULL(A.FNRateBegin,0) AS numeric(18,0)) AS FNRateBegin"
        _Qry &= vbCrLf & ",Cast(ISNULL(A.FNRateEnd,0) AS numeric(18,0)) AS FNRateEnd"
        _Qry &= vbCrLf & ",Cast(ISNULL(A.FNRateDeduct,0) AS numeric(18,2)) AS FNRateDeduct"
        _Qry &= vbCrLf & ",A.FTNote,A.FTStaDeduct"
        _Qry &= vbCrLf & ",CASE WHEN ISNULL(A.FTStaActive,'')='1' THEN '1' ELSE '0' END AS FTStaActive"
        _Qry &= vbCrLf & ",A.FNHSysEmpTypeId ,ET.FTEmpTypeCode"
        If HI.ST.Lang.Language = ST.Lang.eLang.TH Then
            _Qry &= vbCrLf & ",ET.FTEmpTypeNameTH AS FTEmpTypeName,ISNULL(B.FTNameTH,'') AS FTStaDeductName "
        Else
            _Qry &= vbCrLf & ",ET.FTEmpTypeNameEN AS FTEmpTypeName,ISNULL(B.FTNameEN,'') AS FTStaDeductName "
        End If

        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLateSet  AS A WITH(NOLOCK)"
        _Qry &= vbCrLf & "  LEFT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.THRMEmpType AS ET WITH (NOLOCK)"
        _Qry &= vbCrLf & " On A.FNHSysEmpTypeId = ET.FNHSysEmpTypeId"
        _Qry &= vbCrLf & "  LEFT OUTER JOIN  ("
        _Qry &= vbCrLf & "  SELECT   FNListIndex, FTNameTH, FTNameEN"
        _Qry &= vbCrLf & "  FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData WITH(NOLOCK) "
        _Qry &= vbCrLf & "  WHERE  (FTListName = N'FTStaDeduct')"
        _Qry &= vbCrLf & "  ) AS B ON A.FTStaDeduct = B.FNListIndex "


        _Qry &= vbCrLf & " WHERE (ISNULL(A.FNHSysCmpId,0) =0 OR ISNULL(A.FNHSysCmpId,0) = " & HI.ST.SysInfo.CmpID & " ) "

        _Qry &= vbCrLf & "ORDER BY FNHSysEmpTypeId,FTCfgLateCode"

        Me.ogd.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

    End Sub

    Private Sub wTaxRate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Call LoadConfigLateSet()
    End Sub

    Private Sub ocmexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(ogbheader)
        Call LoadConfigLateSet()
    End Sub

    Private Sub ocmsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click
        Dim _Qry As String = ""

        _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLateSet SET "
        _Qry &= vbCrLf & " FNRateBegin=" & CDbl(FNRateBegin.Value)
        _Qry &= vbCrLf & ",FNRateEnd=" & CDbl(FNRateEnd.Value)
        _Qry &= vbCrLf & ",FNRateDeduct=" & CDbl(FNRateDeduct.Value)
        _Qry &= vbCrLf & ",FTStaDeduct='" & FTStaDeduct.SelectedIndex.ToString & "' "
        _Qry &= vbCrLf & ",FTNote=N'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "' "
        _Qry &= vbCrLf & ",FTStaActive='1' "
        _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
        _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
        _Qry &= vbCrLf & ",FNHSysCmpId = " & HI.ST.SysInfo.CmpID & ""
        _Qry &= vbCrLf & " WHERE  FTCfgLateCode='" & FNSeqNo.Value.ToString & "'"
        _Qry &= vbCrLf & " AND  FNHSysEmpTypeId='" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & "'"
        _Qry &= vbCrLf & " AND  (ISNULL(FNHSysCmpId, 0) = 0 Or FNHSysCmpId = " & HI.ST.SysInfo.CmpID & " "

        If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then
            _Qry = "Select MAX(Cast(ISNULL(FTCfgLateCode,0) As numeric(18,0))) As FNSeqNo FROM THRMConfigLateSet  "

            Dim tSeqNo As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
            tSeqNo = Val(tSeqNo) + 1

            _Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLateSet(FTCfgLateCode, FNRateBegin, FNRateEnd, FNRateDeduct, FTStaDeduct,FTNote, FTStaActive, FNHSysEmpTypeId,FNHSysCmpId"
            _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
            _Qry &= vbCrLf & " Select '" & tSeqNo.ToString & "'," & CDbl(FNRateBegin.Value) & ""
            _Qry &= vbCrLf & "," & CDbl(FNRateEnd.Value) & "," & CDbl(FNRateDeduct.Value) & ",'" & FTStaDeduct.SelectedIndex.ToString & "',N'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "','1' "
            _Qry &= vbCrLf & "," & Val(FNHSysEmpTypeId.Properties.Tag.ToString)
            _Qry &= vbCrLf & "," & HI.ST.SysInfo.CmpID
            _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        End If


        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLateSet SET FTCfgLateCode=FNNo"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLateSet INNER JOIN "
        _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNRateBegin,FNRateEnd) AS FNNo, FTCfgLateCode"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLateSet WHERE FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & "  WHERE (ISNULL(FNHSysCmpId,0)=0 Or FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ")  "
        _Qry &= vbCrLf & ") T1 On THRMConfigLateSet.FTCfgLateCode=T1.FTCfgLateCode "
        _Qry &= vbCrLf & "WHERE FNHSysEmpTypeId=" & Val(FNHSysEmpTypeId.Properties.Tag.ToString) & " "
        _Qry &= vbCrLf & "  And  (ISNULL(THRMConfigLateSet.FNHSysCmpId, 0) = 0 Or THRMConfigLateSet.FNHSysCmpId = " & HI.ST.SysInfo.CmpID & ") "
        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        HI.TL.HandlerControl.ClearControl(ogbheader)
        Call LoadConfigLateSet()

    End Sub

    Private Function VerifyData() As Boolean
        Dim _pass As Boolean = True

        Return _pass
    End Function

    Private Sub ocmdelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click

        If Me.FNSeqNo.Value <= 0 Then Exit Sub
        Dim _Qry As String = ""

        _Qry = " Delete  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLateSet  "
        _Qry &= vbCrLf & " WHERE  FTCfgLateCode='" & Me.FNSeqNo.Value.ToString & "' AND (ISNULL(FNHSysCmpId,0)=0 OR FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ")  "
        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLateSet SET FTCfgLateCode=FNNo"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLateSet INNER JOIN "
        _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FNRateBegin,FNRateEnd) AS FNNo, FTCfgLateCode"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigLateSet  WHERE (ISNULL(FNHSysCmpId,0)=0 OR FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ")  "
        _Qry &= vbCrLf & ") T1 ON THRMConfigLateSet.FTCfgLateCode=T1.FTCfgLateCode  WHERE (ISNULL(THRMConfigLateSet.FNHSysCmpId,0)=0 OR THRMConfigLateSet.FNHSysCmpId=" & HI.ST.SysInfo.CmpID & ")   "
        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        HI.TL.HandlerControl.ClearControl(ogbheader)
        Call LoadConfigLateSet()

    End Sub

    Private Sub ogv_Click(sender As Object, e As System.EventArgs) Handles ogv.Click
        With ogv
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub

            Me.FNSeqNo.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FTCfgLateCode").ToString)
            Me.FNRateBegin.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNRateBegin").ToString)
            Me.FNRateEnd.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNRateEnd").ToString)
            Me.FNRateDeduct.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FNRateDeduct").ToString)
            FTStaDeduct.SelectedIndex = Val("" & .GetRowCellValue(.FocusedRowHandle, "FTStaDeduct").ToString)
            Me.FTRemark.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTNote").ToString
            Me.FNHSysEmpTypeId.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTEmpTypeCode").ToString
            Me.FNRateBegin.Focus()
        End With
    End Sub

    Private Sub ogd_Click(sender As System.Object, e As System.EventArgs) Handles ogd.Click

    End Sub
End Class