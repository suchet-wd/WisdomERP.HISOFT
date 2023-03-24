Imports System.Windows.Forms

Public Class wAccountCloseJobOrder
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
        _Qry &= vbCrLf & " ,B.FTOrderNo"
        _Qry &= vbCrLf & "	,CASE WHEN ISDATE(C.FDShipDate) = 1 THEN Convert(datetime,C.FDShipDate) ELSE NULL END AS FDShipDate "
        _Qry &= vbCrLf & ", CASE WHEN B.FNJobState = 3 THEN '1' ELSE '0' END AS FTStateJobClose"
        _Qry &= vbCrLf & ", B.FTStateClose"
        _Qry &= vbCrLf & ", B.FTStateCloseBy"
        _Qry &= vbCrLf & ", CASE WHEN ISDATE(B.FTStateCloseDate)=1 THEN CONVERT(datetime,B.FTStateCloseDate) ELSE NULL END AS FTStateCloseDate"
        _Qry &= vbCrLf & ", B.FTStateCloseTime"
        _Qry &= vbCrLf & ", B.FTStateReopenBy"
        _Qry &= vbCrLf & ", CASE WHEN ISDATE(B.FTStateReopenDate)=1 THEN CONVERT(datetime,B.FTStateReopenDate) ELSE NULL END AS  FTStateReopenDate"
        _Qry &= vbCrLf & ", B.FTStateReopenTime "
        _Qry &= vbCrLf & ", CASE WHEN ISNULL(A.FTOrderNo,'') <>'' THEN '1' ELSE '0' END AS  FTStateFinish"
        _Qry &= vbCrLf & "  FROM ( SELECT B.FTOrderNo"
        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS B WITH(NOLOCK) ON A.FTOrderNo = B.FTOrderNo"
        _Qry &= vbCrLf & "  WHERE  (A.FTStateFinish = '1')"
        _Qry &= vbCrLf & "  UNION"
        _Qry &= vbCrLf & "  Select B.FTOrderNo"
        _Qry &= vbCrLf & "  FROM   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "        [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS B WITH(NOLOCK) ON A.FTOrderNo = CONVERT(nvarchar(30), B.FNHSysStyleId)"
        _Qry &= vbCrLf & "  WHERE  (A.FTStateFinish = '1') AND B.FNOrderType IN (17,22)"
        _Qry &= vbCrLf & "  UNION "
        _Qry &= vbCrLf & "   Select B.FTOrderNo"
        _Qry &= vbCrLf & "   FROM    [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTJobCost_Invoice AS A WITH(NOLOCK) INNER JOIN"
        _Qry &= vbCrLf & "           [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS B WITH(NOLOCK) ON A.FTOrderNoRef = B.FTOrderNo"
        _Qry &= vbCrLf & "  	) AS A RIGHT OUTER JOIN [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder AS B WITH(NOLOCK) ON A.FTOrderNo = B.FTOrderNo "
        _Qry &= vbCrLf & "  INNER JOIN (SELECT  FTOrderNo,MIN(FDShipDate) AS FDShipDate"
        _Qry &= vbCrLf & "  	FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "]..V_OrderSub_BreakDown_ShipDestination"
        _Qry &= vbCrLf & "  	WHERE FTOrderNo<>''"

        If FTCustomerPO.Text <> "" Then
            _Qry &= vbCrLf & "  	AND FTPOref>='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "'"
        End If

        If FTCustomerPO_To.Text <> "" Then
            _Qry &= vbCrLf & "  	AND FTPOref<='" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "'"
        End If

        If FTStartShipDate.Text <> "" Then
            _Qry &= vbCrLf & "  	AND FDShipDate >='" & HI.UL.ULDate.ConvertEnDB(FTStartShipDate.Text) & "'"
        End If

        If FTEndShipDate.Text <> "" Then
            _Qry &= vbCrLf & "  	AND FDShipDate <='" & HI.UL.ULDate.ConvertEnDB(FTEndShipDate.Text) & "'"
        End If

        _Qry &= vbCrLf & "   	GROUP BY FTOrderNo"
        _Qry &= vbCrLf & "  ) AS C ON A.FTOrderNo = C.FTOrderNo "
        _Qry &= vbCrLf & "  	WHERE A.FTOrderNo<>''"

        If FTOrderNo.Text <> "" Then
            _Qry &= vbCrLf & "  	AND  A.FTOrderNo>='" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "'"
        End If

        If FTOrderNo_To.Text <> "" Then
            _Qry &= vbCrLf & "  	AND  A.FTOrderNo<='" & HI.UL.ULF.rpQuoted(FTOrderNo_To.Text) & "'"
        End If

        _Qry &= vbCrLf & "   ORDER BY A.FTOrderNo"

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
                _Qry &= vbCrLf & " FNJobState=3"
                _Qry &= vbCrLf & ", FTStateClose='1'"
                _Qry &= vbCrLf & ", FTStateCloseBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ", FTStateCloseDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ", FTStateCloseTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder As A "
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " AND FNJobState<>3"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

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
                _Qry &= vbCrLf & " FNJobState=1"
                _Qry &= vbCrLf & ", FTStateClose='0'"
                _Qry &= vbCrLf & ", FTStateReopenBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & ", FTStateReopenDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & ", FTStateReopenTime=" & HI.UL.ULDate.FormatTimeDB & ""
                _Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder As A "
                _Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                _Qry &= vbCrLf & " AND FNJobState=3"

                HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

            Next

            _Spls.Close()
            Return True
        Catch ex As Exception
            _Spls.Close()
            Return False
        End Try

    End Function

    Private Sub ClearCreteria()

        FTStartShipDate.Text = ""
        FTEndShipDate.Text = ""



        FTOrderNo.Properties.Tag = ""
        FTOrderNo.Text = ""

        FTOrderNo_To.Properties.Tag = ""
        FTOrderNo_To.Text = ""

        FTCustomerPO.Properties.Tag = ""
        FTCustomerPO.Text = ""

        FTCustomerPO_To.Properties.Tag = ""
        FTCustomerPO_To.Text = ""

        'FNHSysSuplId.Properties.Tag = ""
        'FNHSysSuplId.Text = ""

        'FNHSysSuplIdTo.Properties.Tag = ""
        'FNHSysSuplIdTo.Text = ""

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

        If FTStartShipDate.Text = "" And FTEndShipDate.Text = "" And FTOrderNo.Text = "" And FTOrderNo_To.Text = "" And FTCustomerPO.Text = "" And FTCustomerPO_To.Text = "" Then
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
                If .GetFocusedRowCellValue("FTStateFinish").ToString <> "1" Then
                    e.Cancel = True
                End If
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

                        If .GetFocusedRowCellValue("FTStateFinish").ToString = "1" Then
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), State)
                        End If

                    Next
                End With

                .AcceptChanges()
            End With

        Catch ex As Exception
        End Try
    End Sub

End Class