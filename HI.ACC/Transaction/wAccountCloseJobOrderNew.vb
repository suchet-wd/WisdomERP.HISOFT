Imports System.Windows.Forms

Public Class wAccountCloseJobOrderNew
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


        _Qry = "EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.USP_GETLIST_FORCLOSEJOBMANAGESTOCK '" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "','" & HI.UL.ULF.rpQuoted(FTCustomerPO.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTStartShipDate.Text) & "','" & HI.UL.ULDate.ConvertEnDB(FTEndShipDate.Text) & "','" & HI.UL.ULF.rpQuoted(FTOrderNo.Text) & "','" & HI.UL.ULF.rpQuoted(FTOrderNo_To.Text) & "' "
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

            For Each R As DataRow In _dt.Select("FTStateSelect='1'")

                '_Qry = " UPDATE A SET "
                '_Qry &= vbCrLf & " FNJobState=3"
                '_Qry &= vbCrLf & ", FTStateClose='1'"
                '_Qry &= vbCrLf & ", FTStateCloseBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                '_Qry &= vbCrLf & ", FTStateCloseDate=" & HI.UL.ULDate.FormatDateDB & ""
                '_Qry &= vbCrLf & ", FTStateCloseTime=" & HI.UL.ULDate.FormatTimeDB & ""
                '_Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder As A "
                '_Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                '_Qry &= vbCrLf & " AND FNJobState<>3"

                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)

                _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.USP_SET_PRICECLOSEJOB  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "','0'"
                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)


                _Qry = " UPDATE   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTOrderClose SET "
                _Qry &= vbCrLf & "  FTStateClose ='1'"
                _Qry &= vbCrLf & " ,FTStateCloseBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ,FTStateCloseDate=" & HI.UL.ULDate.FormatDateDB & ""
                _Qry &= vbCrLf & "  ,FTStateCloseTime=" & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & " ,FTCmpCode='" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                _Qry &= vbCrLf & " ,FTCustCode='" & HI.UL.ULF.rpQuoted(R!FTCustCode.ToString) & "'"
                _Qry &= vbCrLf & " ,FTCustName='" & HI.UL.ULF.rpQuoted(R!FTCustName.ToString) & "'"
                _Qry &= vbCrLf & " ,FTPOCusRef='" & HI.UL.ULF.rpQuoted(R!FTPOCusRef.ToString) & "'"
                _Qry &= vbCrLf & " ,FTStyleCode='" & HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString) & "'"
                _Qry &= vbCrLf & " ,FDShipDate='" & HI.UL.ULF.rpQuoted(R!FDShipDate.ToString) & "'"
                _Qry &= vbCrLf & " ,FNQuantity=" & Val(R!FNQuantity.ToString) & ""
                _Qry &= vbCrLf & " ,FTInvoiceNo='" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString) & "'"
                _Qry &= vbCrLf & " ,FDInvoiceDate='" & HI.UL.ULF.rpQuoted(R!FDInvoiceDate.ToString) & "'"
                _Qry &= vbCrLf & " ,FNMCIQuantity=" & Val(R!FNMCIQuantity.ToString) & ""
                _Qry &= vbCrLf & " ,FNMCIAmt=" & Val(R!FNMCIAmt.ToString) & ""
                _Qry &= vbCrLf & " ,FNMCIQuantityBal='" & Val(R!FNMCIQuantityBal.ToString) & "'"
                _Qry &= vbCrLf & " ,FNFabricAmt='" & Val(R!FNFabricAmt.ToString) & "'"
                _Qry &= vbCrLf & " ,FNAccAmt='" & Val(R!FNAccAmt.ToString) & "'"
                _Qry &= vbCrLf & " ,FNFabricMinAmt='" & Val(R!FNFabricMinAmt.ToString) & "'"
                _Qry &= vbCrLf & " ,FNAccMinAmt='" & Val(R!FNAccMinAmt.ToString) & "'"
                _Qry &= vbCrLf & " ,FNFabricIssAmt='" & Val(R!FNFabricIssAmt.ToString) & "'"
                _Qry &= vbCrLf & " ,FNAccIssAmt='" & Val(R!FNAccIssAmt.ToString) & "'"
                _Qry &= vbCrLf & " ,FNWestAmt='" & Val(R!FNWestAmt.ToString) & "'"
                _Qry &= vbCrLf & " ,FNFabricOwnerWHAmt='" & Val(R!FNFabricOwnerWHAmt.ToString) & "'"
                _Qry &= vbCrLf & " ,FNAccOwnerWHAmt='" & Val(R!FNAccOwnerWHAmt.ToString) & "'"
                _Qry &= vbCrLf & " ,FNFabricOtherWHAmt='" & Val(R!FNFabricOtherWHAmt.ToString) & "'"
                _Qry &= vbCrLf & " ,FNAccOtherWHAmt='" & Val(R!FNAccOtherWHAmt.ToString) & "'"
                _Qry &= vbCrLf & " ,FNJobState='" & Val(R!FNJobState.ToString) & "'"

                _Qry &= vbCrLf & "  WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"

                If HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT) = False Then

                    _Qry = " INSERT INTO  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTOrderClose ( "
                    _Qry &= vbCrLf & " FTOrderNo, FTCmpCode, FTCustCode, FTCustName, FTPOCusRef, FTStyleCode, FDShipDate, FNQuantity, FTInvoiceNo, FDInvoiceDate, FNMCIQuantity, FNMCIAmt, FNMCIQuantityBal, FNFabricAmt, FNAccAmt, FNFabricMinAmt, "
                    _Qry &= vbCrLf & "  FNAccMinAmt, FNFabricIssAmt, FNAccIssAmt, FNWestAmt, FNFabricOwnerWHAmt, FNAccOwnerWHAmt, FNFabricOtherWHAmt, FNAccOtherWHAmt, FNJobState, FTStateClose, FTStateCloseBy, FTStateCloseDate, "
                    _Qry &= vbCrLf & "  FTStateCloseTime "
                    _Qry &= vbCrLf & " ) "
                    _Qry &= vbCrLf & " SELECT TOP 1  '" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTCmpCode.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTCustCode.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTCustName.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTPOCusRef.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTStyleCode.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FDShipDate.ToString) & "'"
                    _Qry &= vbCrLf & " ," & Val(R!FNQuantity.ToString) & ""
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FTInvoiceNo.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(R!FDInvoiceDate.ToString) & "'"
                    _Qry &= vbCrLf & " ," & Val(R!FNMCIQuantity.ToString) & ""
                    _Qry &= vbCrLf & " ," & Val(R!FNMCIAmt.ToString) & ""
                    _Qry &= vbCrLf & " ,'" & Val(R!FNMCIQuantityBal.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & Val(R!FNFabricAmt.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & Val(R!FNAccAmt.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & Val(R!FNFabricMinAmt.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & Val(R!FNAccMinAmt.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & Val(R!FNFabricIssAmt.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & Val(R!FNAccIssAmt.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & Val(R!FNWestAmt.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & Val(R!FNFabricOwnerWHAmt.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & Val(R!FNAccOwnerWHAmt.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & Val(R!FNFabricOtherWHAmt.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & Val(R!FNAccOtherWHAmt.ToString) & "'"
                    _Qry &= vbCrLf & " ,'" & Val(R!FNJobState.ToString) & "'"
                    _Qry &= vbCrLf & " ,'1'"
                    _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & ""
                    _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & ""

                    HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)


                End If



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

            For Each R As DataRow In _dt.Select("FTStateSelect='1'")

                '_Qry = " UPDATE A SET "
                '_Qry &= vbCrLf & " FNJobState=1"
                '_Qry &= vbCrLf & ", FTStateClose='0'"
                '_Qry &= vbCrLf & ", FTStateReopenBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                '_Qry &= vbCrLf & ", FTStateReopenDate=" & HI.UL.ULDate.FormatDateDB & ""
                '_Qry &= vbCrLf & ", FTStateReopenTime=" & HI.UL.ULDate.FormatTimeDB & ""
                '_Qry &= vbCrLf & "  FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MERCHAN) & "].dbo.TMERTOrder As A "
                '_Qry &= vbCrLf & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                '_Qry &= vbCrLf & " AND FNJobState=3"

                'HI.Conn.SQLConn.ExecuteNonQuery(_Qry, Conn.DB.DataBaseName.DB_MERCHAN)


                _Qry = " UPDATE   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_ACCOUNT) & "].dbo.TACCTOrderClose SET FTStateClose='0',FTStateReopenBy='" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "',FTStateReopenDate=" & HI.UL.ULDate.FormatDateDB & ",FTStateReopenTime=" & HI.UL.ULDate.FormatTimeDB & " WHERE FTOrderNo='" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "'"
                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_ACCOUNT)

                _Qry = " EXEC [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.USP_SET_PRICECLOSEJOB  '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "','" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "','1'"
                HI.Conn.SQLConn.ExecuteOnly(_Qry, Conn.DB.DataBaseName.DB_INVEN)

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

                If .Select("FTStateSelect='1'").Length <= 0 Then

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

                If .Select("FTStateSelect='1'").Length <= 0 Then
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


                        .SetRowCellValue(I, .Columns.ColumnByFieldName("FTStateSelect"), State)


                    Next
                End With

                .AcceptChanges()
            End With

        Catch ex As Exception
        End Try
    End Sub

End Class