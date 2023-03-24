Public Class wConfigTaxRate

    Private Sub LoadTaxRate()
        Dim _Qry As String = ""

        _Qry = "SELECT "
        _Qry &= vbCrLf & " Cast(ISNULL(FNSeqNo,0) AS numeric(18,0)) AS FNSeqNo"
        _Qry &= vbCrLf & ",Cast(ISNULL(FCAmtBegin,0) AS numeric(18,2)) AS FCAmtBegin"
        _Qry &= vbCrLf & ",Cast(ISNULL(FCAmtEnd,0) AS numeric(18,2)) AS FCAmtEnd"
        _Qry &= vbCrLf & ",Cast(ISNULL(FCTaxRate,0) AS numeric(18,2)) AS FCTaxRate"
        _Qry &= vbCrLf & ",FTRemark"
        _Qry &= vbCrLf & ",CASE WHEN ISNULL(FTStaActive,'')='1' THEN '1' ELSE '0' END AS FTStaActive"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigTaxRate WITH(NOLOCK)"

        Me.ogd.DataSource = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)

    End Sub

    Private Sub wTaxRate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Call LoadTaxRate()
    End Sub

    Private Sub ocmexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmclear.Click
        HI.TL.HandlerControl.ClearControl(ogbheader)
        Call LoadTaxRate()
    End Sub

    Private Sub ocmsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmsave.Click
        Dim _Qry As String = ""

        _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigTaxRate SET "
        _Qry &= vbCrLf & " FCAmtBegin=" & CDbl(FCAmtBegin.Value)
        _Qry &= vbCrLf & ",FCAmtEnd=" & CDbl(FCAmtEnd.Value)
        _Qry &= vbCrLf & ",FCTaxRate=" & CDbl(FCTaxRate.Value)
        _Qry &= vbCrLf & ",FTRemark=N'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "' "
        _Qry &= vbCrLf & ",FTStaActive='1' "
        _Qry &= vbCrLf & ",FTUpdUser = N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
        _Qry &= vbCrLf & ",FDUpdDate = " & HI.UL.ULDate.FormatDateDB & ""
        _Qry &= vbCrLf & ",FTUpdTime = " & HI.UL.ULDate.FormatTimeDB & ""
        _Qry &= vbCrLf & " WHERE  FNSeqNo=" & FNSeqNo.Value & ""

        If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR) = False Then
            _Qry = "SELECT MAX(FNSeqNo) AS FNSeqNo FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigTaxRate  "

            Dim tSeqNo As String = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_HR, "0")
            tSeqNo = Val(tSeqNo) + 1

            _Qry = "INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigTaxRate(FNSeqNo, FCAmtBegin, FCAmtEnd, FCTaxRate, FTRemark, FTStaActive"
            _Qry &= vbCrLf & ", FTInsUser, FDInsDate, FTInsTime)  "
            _Qry &= vbCrLf & " SELECT " & tSeqNo & "," & CDbl(FCAmtBegin.Value) & ""
            _Qry &= vbCrLf & "," & CDbl(FCAmtEnd.Value) & "," & CDbl(FCTaxRate.Value) & ",N'" & HI.UL.ULF.rpQuoted(FTRemark.Text) & "','1' "
            _Qry &= vbCrLf & ",N'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & ""
            _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB

            HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        End If


        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigTaxRate SET FNSeqNo=FNNo"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigTaxRate INNER JOIN "
        _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FCAmtBegin,FCAmtEnd) AS FNNo, FNSeqNo"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigTaxRate "
        _Qry &= vbCrLf & ") T1 ON THRMConfigTaxRate.FNSeqNo=T1.FNSeqNo "

        HI.TL.HandlerControl.ClearControl(ogbheader)
        Call LoadTaxRate()

    End Sub

    Private Function VerifyData() As Boolean
        Dim _pass As Boolean = True

        Return _pass
    End Function

    Private Sub ocmdelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ocmdelete.Click

        If Me.FNSeqNo.Value <= 0 Then Exit Sub

        Dim _Qry As String = ""

        _Qry = " Delete  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigTaxRate  "
        _Qry &= vbCrLf & " WHERE  FNSeqNo=" & Me.FNSeqNo.Value
        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)


        _Qry = "UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigTaxRate SET FNSeqNo=FNNo"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigTaxRate INNER JOIN "
        _Qry &= vbCrLf & "(SELECT ROW_NUMBER() OVER(ORDER BY FCAmtBegin,FCAmtEnd) AS FNNo, FNSeqNo"
        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigTaxRate "
        _Qry &= vbCrLf & ") T1 ON THRMConfigTaxRate.FNSeqNo=T1.FNSeqNo "
        HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_HR)

        HI.TL.HandlerControl.ClearControl(ogbheader)
        Call LoadTaxRate()

    End Sub

    Private Sub ogv_Click(sender As Object, e As System.EventArgs) Handles ogv.Click
        With ogv
            If .FocusedRowHandle < 0 Or .FocusedRowHandle > .RowCount Then Exit Sub
            Me.FNSeqNo.Value = "" & .GetRowCellValue(.FocusedRowHandle, "FNSeqNo").ToString
            Me.FCAmtBegin.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FCAmtBegin").ToString)
            Me.FCAmtEnd.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FCAmtEnd").ToString)
            Me.FCTaxRate.Value = Val("" & .GetRowCellValue(.FocusedRowHandle, "FCTaxRate").ToString)
            Me.FTRemark.Text = "" & .GetRowCellValue(.FocusedRowHandle, "FTRemark").ToString
            Me.FCAmtBegin.Focus()
        End With
    End Sub

    Private Sub ogd_Click(sender As System.Object, e As System.EventArgs) Handles ogd.Click

    End Sub
End Class