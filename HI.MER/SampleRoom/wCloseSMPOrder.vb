Imports System.Windows.Forms

Public Class wCloseSMPOrder
    Private _StateFormLoad As Boolean = False
    Sub New()
        InitializeComponent()
        _StateFormLoad = True
    End Sub

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

    Private Function LoadDataTracking(Spls As HI.TL.SplashScreen) As Boolean
        Dim _Qry As String = ""
        Dim _dt As DataTable

        _Qry = "  SELECT '0' AS FTSelect"
        _Qry &= vbCrLf & " ,A.FTSMPOrderNo"
        _Qry &= vbCrLf & "	,CASE WHEN ISDATE(A.FDSMPOrderDate) = 1 THEN Convert(datetime,A.FDSMPOrderDate) ELSE NULL END AS FDSMPOrderDate "
        _Qry &= vbCrLf & ", CASE WHEN A.FNSMPOrderStatus = 3 THEN '1' ELSE '0' END AS FTStateJobClose"
        _Qry &= vbCrLf & ", A.FTStateClose"
        _Qry &= vbCrLf & ", A.FTStateCloseBy"
        _Qry &= vbCrLf & ", CASE WHEN ISDATE(A.FTStateCloseDate)=1 THEN CONVERT(datetime,A.FTStateCloseDate) ELSE NULL END AS FTStateCloseDate"
        _Qry &= vbCrLf & ", A.FTStateCloseTime"
        _Qry &= vbCrLf & ", A.FTStateReopenBy"
        _Qry &= vbCrLf & ", CASE WHEN ISDATE(A.FTStateReopenDate)=1 THEN CONVERT(datetime,A.FTStateReopenDate) ELSE NULL END AS  FTStateReopenDate"
        _Qry &= vbCrLf & ", A.FTStateReopenTime "

        _Qry &= vbCrLf & "  	FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "]..TSMPOrder AS A WITH(NOLOCK) "

        _Qry &= vbCrLf & "    WHERE A.FTSMPOrderNo<>''"

        If FNHSysCustId.Text <> "" Then
            _Qry &= vbCrLf & "  AND A.FNHSysCustId=" & Val(FNHSysCustId.Properties.Tag.ToString) & ""
        End If

        If FNHSysStyleId.Text <> "" Then
            _Qry &= vbCrLf & "  AND A.FNHSysStyleId=" & Val(FNHSysStyleId.Properties.Tag.ToString) & ""
        End If

        If FNHSysSeasonId.Text <> "" Then
            _Qry &= vbCrLf & "  AND A.FNHSysSeasonId=" & Val(FNHSysSeasonId.Properties.Tag.ToString) & ""
        End If

        If FNHSysMerTeamId.Text <> "" Then
            _Qry &= vbCrLf & "  AND A.FNHSysMerTeamId=" & Val(FNHSysMerTeamId.Properties.Tag.ToString) & ""
        End If

        If FTStartOrderDate.Text <> "" Then
            _Qry &= vbCrLf & "  AND A.FDSMPOrderDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartOrderDate.Text) & "'"
        End If

        If FTEndOrderDate.Text <> "" Then
            _Qry &= vbCrLf & "  AND A.FDSMPOrderDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndOrderDate.Text) & "'"
        End If

        _Qry &= vbCrLf & "   ORDER BY A.FTSMPOrderNo"

        _dt = HI.Conn.SQLConn.GetDataTable(_Qry, Conn.DB.DataBaseName.DB_PUR)
        ogcdetail.DataSource = _dt.Copy

        _dt.Dispose()

        Return True
    End Function

    Private Function CloseJob() As Boolean
        Dim _Spls As New HI.TL.SplashScreen("Closing... pleas wait...")
        Try

            Dim _dt As DataTable
            Dim _Qry As String = ""

            With CType(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy
            End With

            For Each R As DataRow In _dt.Select("FTSelect='1'")

                _Qry = " UPDATE A SET "
                _Qry &= vbCrLf & " FNSMPOrderStatus=3"
                _Qry &= vbCrLf & ", FTStateClose='1'"
                _Qry &= vbCrLf & ", FTStateCloseBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ", FTStateCloseDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ", FTStateCloseTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder As A "
                _Qry &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " AND ISNULL(FNSMPOrderStatus,0)<>3"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

            Next

            _Spls.Close()
            Return True
        Catch ex As Exception
            _Spls.Close()
            Return False
        End Try

    End Function

    Private Function ReOpenJob() As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Re Opening... pleas wait...")
        Try

            Dim _dt As DataTable
            Dim _Qry As String = ""

            With CType(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()
                _dt = .Copy
            End With

            For Each R As DataRow In _dt.Select("FTSelect='1'")

                _Qry = " UPDATE A SET "
                _Qry &= vbCrLf & " FNSMPOrderStatus=1"
                _Qry &= vbCrLf & ", FTStateClose='0'"
                _Qry &= vbCrLf & ", FTStateReopenBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ", FTStateReopenDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ", FTStateReopenTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SAMPLE) & "].dbo.TSMPOrder As A "
                _Qry &= vbCrLf & " WHERE FTSMPOrderNo='" & HI.UL.ULF.rpQuoted(R!FTSMPOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " AND ISNULL(FNSMPOrderStatus,0)=3"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_SAMPLE)

            Next

            _Spls.Close()
            Return True
        Catch ex As Exception
            _Spls.Close()
            Return False
        End Try

    End Function

    Private Sub ClearCreteria()

        HI.TL.HandlerControl.ClearControl(Me)

    End Sub

    Private Sub ocmload_Click(sender As Object, e As EventArgs) Handles ocmload.Click
        If ValidateData() Then
            Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

            If LoadDataTracking(_Spls) Then
            End If

            _Spls.Close()
        End If
    End Sub

    Private Sub ocmclear_Click(sender As Object, e As EventArgs) Handles ocmclear.Click
        'HI.TL.HandlerControl.ClearControl(Me)
        Call ClearCreteria()
        ogcdetail.DataSource = Nothing

    End Sub

    Private Function ValidateData() As Boolean

        If FNHSysStyleId.Text = "" And FNHSysMerTeamId.Text = "" And FNHSysSeasonId.Text = "" And FNHSysCustId.Text = "" And FTStartOrderDate.Text = "" And FTEndOrderDate.Text = "" Then
            HI.MG.ShowMsg.mInfo("กรุณาเลือกข้อมูลอย่างน้อยหนึ่งอย่าง !!!", 1511121406, Me.Text)
            Return False
        End If

        'If StateFTPOref.Checked = False And Me.StateFTStyleCode.Checked = False And StateFTSeasonCode.Checked = False And StateFTColorway.Checked = False _
        '    And StateFTSizeBreakDown.Checked = False And StateFTRawMatCode.Checked = False _
        '    And StateFTMatColorCode.Checked = False And StateFTMatSizeCode.Checked = False And StateFTSuplName.Checked = False _
        '    And StateFTCountryName.Checked = False And StateFDPurchaseDate.Checked = False And StateFTPurchaseNo.Checked = False Then

        '    HI.MG.ShowMsg.mInfo("กรูณาทำการเลือกข้อมูลที่ต้องการทำการแสดง !!!", 1511177546, Me.Text)
        '    Return False
        'End If

        Return True
    End Function



    Private Sub ocmexit_Click(sender As Object, e As EventArgs) Handles ocmexit.Click
        Me.Close()
    End Sub


    Private Sub wPoDetailTrackingForExport_Load(sender As Object, e As EventArgs) Handles Me.Load
        _StateFormLoad = False
        'Me.StateFTPurchaseNo.Checked = False
    End Sub

    Private Sub ocmreopenjob_Click(sender As Object, e As EventArgs) Handles ocmreopenjob.Click
        If Not (Me.ogcdetail.DataSource Is Nothing) Then

            With CType(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTSelect='1'").Length <= 0 Then

                    HI.MG.ShowMsg.mInfo("กรุณาทำการระบุ Job Order ที่ต้องการทำการ Re Open Complete !!!", 1602220550, Me.Text, , MessageBoxIcon.Warning)
                    Exit Sub

                End If

                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ เปิด Job ที่ได้ทำการเลือกไว้ใช่หรือไม่ ?", 1602220549) = True Then

                    If ReOpenJob() Then

                        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

                        If LoadDataTracking(_Spls) Then
                        End If

                        _Spls.Close()

                        HI.MG.ShowMsg.mInfo("Re Open Job Complete !!!", 1602220551, Me.Text, , MessageBoxIcon.Information)
                    Else
                        HI.MG.ShowMsg.mInfo("Re Open Job Not Complete !!!", 1602220556, Me.Text, , MessageBoxIcon.Warning)
                    End If

                End If

            End With

        End If
    End Sub

    Private Sub ocmclosejob_Click(sender As Object, e As EventArgs) Handles ocmclosejob.Click
        If Not (Me.ogcdetail.DataSource Is Nothing) Then

            With CType(Me.ogcdetail.DataSource, DataTable)
                .AcceptChanges()

                If .Select("FTSelect='1'").Length <= 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาทำการระบุ Job Order ที่ต้องการทำการ ปิด !!!", 1602220547, Me.Text, , MessageBoxIcon.Warning)
                    Exit Sub
                End If

                If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ ปิด Job ที่ได้ทำการเลือกไว้ใช่หรือไม่ ?", 1602220548) = True Then

                    If Me.CloseJob Then
                        Dim _Spls As New HI.TL.SplashScreen("Loading...   Please Wait   ")

                        If LoadDataTracking(_Spls) Then
                        End If

                        _Spls.Close()

                        HI.MG.ShowMsg.mInfo("Close Job Complete !!!", 1602220552, Me.Text, , MessageBoxIcon.Information)
                    Else
                        HI.MG.ShowMsg.mInfo("Close Job Not Complete !!!", 1602220555, Me.Text, , MessageBoxIcon.Warning)
                    End If
                    
                End If

            End With

        End If
    End Sub

    Private Sub RepFTSelect_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles RepFTSelect.EditValueChanging

        Try
            With Me.ogvdetail
                'If .GetFocusedRowCellValue("FTStateFinish").ToString <> "1" Then
                '    e.Cancel = True
                'End If
            End With
        Catch ex As Exception
        End Try

    End Sub

    Private Sub FTStateSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles FTStateSelectAll.CheckedChanged
        Try
            Dim State As String = "0"

            If FTStateSelectAll.Checked Then
                State = "1"
            End If

            With CType(ogcdetail.DataSource, DataTable)
                .AcceptChanges()

                With ogvdetail
                    For I As Integer = 0 To .RowCount - 1

                        ' If .GetFocusedRowCellValue("FTStateFinish").ToString = "1" Then
                        .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), State)
                        ' End If

                    Next
                End With

                .AcceptChanges()
            End With

        Catch ex As Exception
        End Try
    End Sub

End Class