Public Class wSetBackColorTimeAttandance 


#Region "Procedure"
    Private Sub LoadData()
        Dim _Qry As String = ""
        Dim dt As DataTable

        _Qry = " SELECT A.FTLeaveType ,A.FTLeaveName,ISNULL(B.FTColor,'') AS FTColorString "

        _Qry &= vbCrLf & " FROM ( Select 1 AS FNSeq,'W' AS FTLeaveType "
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal  Then
            _Qry &= vbCrLf & ",'วันหยุดประจำสัปดาห์' AS FTLeaveName"
        Else
            _Qry &= vbCrLf & ",'Weekly holiday' AS FTLeaveName"
        End If

        _Qry &= vbCrLf & " UNION "
        _Qry &= vbCrLf & " Select 2 AS FNSeq,'H' AS FTLeaveType "
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal  Then
            _Qry &= vbCrLf & ",'วันหยุดประจำปี' AS FTLeaveName"
        Else
            _Qry &= vbCrLf & ",'Holidays' AS FTLeaveName"
        End If

        _Qry &= vbCrLf & " UNION "
        _Qry &= vbCrLf & " Select 3 AS FNSeq,'EMPEND' AS FTLeaveType "
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal  Then
            _Qry &= vbCrLf & ",'พนักงานลาออก' AS FTLeaveName"
        Else
            _Qry &= vbCrLf & ",'Employee Resignation' AS FTLeaveName"
        End If

        _Qry &= vbCrLf & " UNION "
        _Qry &= vbCrLf & " Select 3 AS FNSeq,'S' AS FTLeaveType "
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal  Then
            _Qry &= vbCrLf & ",'วันพิเศษ' AS FTLeaveName"
        Else
            _Qry &= vbCrLf & ",'Special Day' AS FTLeaveName"
        End If
        _Qry &= vbCrLf & " UNION "

        _Qry &= vbCrLf & " Select (4 + Row_Number() Over(Order BY FNListIndex)) AS FNSeq,  Convert(varchar(30),FNListIndex) as FTLeaveType"
        If HI.ST.Lang.Language = HI.ST.SysInfo.LanguageLocal  Then
            _Qry &= vbCrLf & ", FTNameTH  AS FTLeaveName"
        Else
            _Qry &= vbCrLf & " , FTNameEN AS FTLeaveName"
        End If
        _Qry &= vbCrLf & "   FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) & "].dbo.HSysListData AS A WITH (NOLOCK)"
        _Qry &= vbCrLf & " WHERE        (FTListName = N'FNLeaveType')"
        _Qry &= vbCrLf & " ) AS A "
        _Qry &= vbCrLf & "  LEFT OUTER JOIN "
        _Qry &= vbCrLf & " [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigColor AS B WITH (NOLOCK)"
        _Qry &= vbCrLf & " ON A.FTLeaveType = B.FTLeaveType "
        _Qry &= vbCrLf & " Order BY  A.FNSeq"
        dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_HR)
        dt.Columns.Add("FTColor", GetType(System.Drawing.Color))


        For Each R As DataRow In dt.Rows
            If R!FTColorString.ToString.Trim() <> "" Then

                Try
                    R!FTColor = System.Drawing.Color.FromArgb(R!FTColorString.ToString.Trim())
                Catch ex As Exception

                End Try
            End If
        Next

        Me.ogc.DataSource = dt

    End Sub
#End Region

#Region "General"

#End Region
    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        Me.LoadData()
    End Sub


    Private Sub wSetBackColorTimeAttandance_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.LoadData()
    End Sub

    Private Sub ocmsave_Click(sender As Object, e As EventArgs) Handles ocmsave.Click
        Dim Qry As String = ""

        Qry = "DELETE FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigColor"
        HI.Conn.SQLConn.ExecuteOnly(Qry, Conn.DB.DataBaseName.DB_HR)

        With CType(Me.ogc.DataSource, DataTable)
            .AcceptChanges()
            For Each R As DataRow In .Rows
                If Not (R!FTColor Is Nothing) Then
                    Try
                        Dim K As System.Drawing.Color = R!FTColor


                        Qry = "INSERT INTO [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_HR) & "].dbo.THRMConfigColor"
                        Qry &= vbCrLf & " (FTLeaveType,FTColor)"
                        Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(R!FTLeaveType.ToString) & "'"
                        Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(K.ToArgb.ToString) & "'"
                        HI.Conn.SQLConn.ExecuteOnly(Qry, Conn.DB.DataBaseName.DB_HR)

                    Catch ex As Exception
                    End Try

                End If
            Next
        End With

        HI.MG.ShowMsg.mProcessComplete(MG.ShowMsg.ProcessType.mSave, Me.Text)

    End Sub
End Class