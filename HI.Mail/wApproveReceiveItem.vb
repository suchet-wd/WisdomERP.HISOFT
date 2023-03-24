Public Class wApproveReceiveItem

    Private _ReceiveNo As String = ""
    Public Property ReceiveNo() As String
        Get
            Return _ReceiveNo
        End Get
        Set(value As String)
            _ReceiveNo = value
        End Set
    End Property

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        With ResFNRcvQty
            .Precision = HI.ST.Config.QtyDigit
            AddHandler .Leave, AddressOf HI.TL.HandlerControl.CalEdit_Leave
            AddHandler .EditValueChanged, AddressOf RcvEdit_EditChanged
        End With

        With ResFTStateSelect
            AddHandler .CheckedChanged, AddressOf CheckEdit_CheckedChanged
        End With

    End Sub

    Private Sub CheckEdit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)

            If CType(sender, DevExpress.XtraEditors.CheckEdit).Checked Then
                Dim _balQty As Double = .GetFocusedRowCellValue("FNTotalRcvQty")
                .SetFocusedRowCellValue("FNApproveRcvQty", _balQty)
            Else
                .SetFocusedRowCellValue("FNApproveRcvQty", 0)
            End If

        End With
    End Sub

    Private Sub RcvEdit_EditChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        With CType(sender.Parent.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
     
            Dim _RcvOverQty As Double = .GetFocusedRowCellValue("FNTotalRcvQty")
            Dim _RcvQty As Double = CType(sender, DevExpress.XtraEditors.CalcEdit).Value
            Dim _SysMatID As Integer = .GetFocusedRowCellValue("FNHSysRawMatId")
            If _RcvQty > _RcvOverQty Then
                .SetFocusedRowCellValue("FNApproveRcvQty", _RcvOverQty)
            Else
                .SetFocusedRowCellValue("FNApproveRcvQty", _RcvQty)
            End If
                  
            Try
                CType(sender.Parent.datasource, DataTable).AcceptChanges()
            Catch ex As Exception
            End Try

        End With
    End Sub

    Private _ProcessProc As Boolean = False
    Public Property ProcessProc As Boolean
        Get
            Return _ProcessProc
        End Get
        Set(value As Boolean)
            _ProcessProc = value
        End Set
    End Property

    Private Function ValidateData() As Boolean
        Dim _Pass As Boolean = False
        CType(ogcrcv.DataSource, DataTable).AcceptChanges()
        If CType(ogcrcv.DataSource, DataTable).Select("FTSelect='1'").Length > 0 Then
            _Pass = True
        End If

        Return _Pass
    End Function

    Private Function SaveData(Optional StateReject As Boolean = False) As Boolean
        CType(ogcrcv.DataSource, DataTable).AcceptChanges()
        Dim _dtdata As DataTable = CType(ogcrcv.DataSource, DataTable).Copy
        Dim _Spls As New HI.TL.SplashScreen("Saving Receive Detail...   Please Wait   ")
        Dim _Qry As String = ""

        Try
            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction

            Dim _FTReceiveNo As String
            Dim _FTPurchaseNo As String = ""
            Dim _FNHSysMailAppId As Integer
            Dim _FNHSysRawMatId As Integer
            Dim _AppQty As Double = 0
            Dim _FNHSysWHId As Integer = 0
            Dim StateImport As String = "0"

            For Each R As DataRow In _dtdata.Select("FTSelect='1'")

                _FTReceiveNo = R!FTReceiveNo.ToString
                _FTPurchaseNo = R!FTPurchaseNo.ToString
                _FNHSysMailAppId = Integer.Parse(Val(R!FNHSysMailAppId.ToString))
                _FNHSysRawMatId = Integer.Parse(Val(R!FNHSysRawMatId.ToString))
                _AppQty = Double.Parse(Val(R!FNApproveRcvQty.ToString))
                _FNHSysWHId = Val(R!FNHSysWHId.ToString)
                StateImport = R!FTStateImport.ToString()

                _Qry = " UPDATE [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENMailSendAppRcvOver "

                If Not (StateReject) Then

                    _Qry &= vbCrLf & " SET  FTStateApprove='1'"
                    _Qry &= vbCrLf & ",FNApproveRcvQty=" & _AppQty & " "
                    _Qry &= vbCrLf & ",FDStateDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & ",FTStateTime=" & HI.UL.ULDate.FormatTimeDB & " "

                Else

                    _Qry &= vbCrLf & " SET  FTStateApprove='2'"
                    _Qry &= vbCrLf & ",FDStateDate=" & HI.UL.ULDate.FormatDateDB & " "
                    _Qry &= vbCrLf & ",FTStateTime=" & HI.UL.ULDate.FormatTimeDB & " "

                End If

                _Qry &= vbCrLf & " WHERE FNHSysMailAppId=" & _FNHSysMailAppId & ""
                _Qry &= vbCrLf & " AND FTReceiveNo='" & HI.UL.ULF.rpQuoted(_FTReceiveNo) & "'"
                _Qry &= vbCrLf & " AND FNHSysRawMatId=" & _FNHSysRawMatId & ""

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    Return False

                End If

                If (StateReject) Then
                    _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail "
                    _Qry &= vbCrLf & " WHERE  FTReceiveNo='" & HI.UL.ULF.rpQuoted(_FTReceiveNo) & "'"
                    _Qry &= vbCrLf & " AND FNHSysRawMatId=" & _FNHSysRawMatId & ""
                    '_Qry &= vbCrLf & " AND FNQuantity<=0"

                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        _Spls.Close()

                    End If

                    If StateImport = "1" Then

                        _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order  "
                        _Qry &= vbCrLf & " WHERE   FTReceiveNo ='" & HI.UL.ULF.rpQuoted(_FTReceiveNo) & "'"
                        _Qry &= vbCrLf & " AND    FNHSysRawMatId =" & _FNHSysRawMatId & " "
                        HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                        _Qry = " DELETE A "
                        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS A "
                        _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode  AS B ON A.FTDocumentNo = B.FTDocumentNo AND A.FTBarcodeNo = B.FTBarcodeNo "
                        _Qry &= vbCrLf & " WHERE A.FTDocumentNo='" & HI.UL.ULF.rpQuoted(_FTReceiveNo) & "'"
                        _Qry &= vbCrLf & " AND    B.FNHSysRawMatId =" & _FNHSysRawMatId & " "
                        HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                        _Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode"
                        _Qry &= vbCrLf & " WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(_FTReceiveNo) & "'"
                        _Qry &= vbCrLf & " AND    FNHSysRawMatId =" & _FNHSysRawMatId & " "

                        HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)
                    End If


                    '_Qry = " DELETE FROM  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail_Order "
                    '_Qry &= vbCrLf & " WHERE  FTReceiveNo='" & HI.UL.ULF.rpQuoted(_FTReceiveNo) & "'"
                    '_Qry &= vbCrLf & " AND FNHSysRawMatId=" & _FNHSysRawMatId & ""
                    'If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then

                    'End If

                Else

                    _Qry = " UPDATE  [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENReceive_Detail "
                    _Qry &= vbCrLf & "SET FNNetAmt=Convert(numeric(18,2)," & _AppQty & " * FNNetPrice )"
                    _Qry &= vbCrLf & ", FNQuantity=" & _AppQty & " "
                    _Qry &= vbCrLf & " WHERE  FTReceiveNo='" & HI.UL.ULF.rpQuoted(_FTReceiveNo) & "'"
                    _Qry &= vbCrLf & " AND FNHSysRawMatId=" & _FNHSysRawMatId & ""
                    '  _Qry &= vbCrLf & " AND FNQuantity<=0"
                    If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                        HI.Conn.SQLConn.Tran.Rollback()
                        HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                        HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                        _Spls.Close()
                        Return False
                    End If


                    If StateImport = "1" Then
                        _Qry &= vbCrLf & " UPDATE  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode SET FNHSysWHId=" & _FNHSysWHId & " "
                        _Qry &= vbCrLf & " WHERE FTDocumentNo='" & HI.UL.ULF.rpQuoted(_FTReceiveNo) & "'"
                        _Qry &= vbCrLf & " AND    FNHSysRawMatId =" & _FNHSysRawMatId & " AND FNHSysWHId=0"
                        HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                        _Qry &= vbCrLf & " UPDATE A SET FNHSysWHId=" & _FNHSysWHId & " "
                        _Qry &= vbCrLf & " FROM [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_IN AS A "
                        _Qry &= vbCrLf & " INNER JOIN  [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode  AS B ON A.FTDocumentNo = B.FTDocumentNo AND A.FTBarcodeNo = B.FTBarcodeNo "
                        _Qry &= vbCrLf & " WHERE A.FTDocumentNo='" & HI.UL.ULF.rpQuoted(_FTReceiveNo) & "'"
                        _Qry &= vbCrLf & " AND    B.FNHSysRawMatId =" & _FNHSysRawMatId & " AND A.FNHSysWHId=0"
                        HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran)

                    Else

                        If StockValidate.EqualizeJob(_FTReceiveNo, _FTPurchaseNo, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran, Val(R!FNHSysRawMatId.ToString)) = False Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            _Spls.Close()
                            Return False
                        End If

                    End If

                End If

            Next

            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)

            Dim _DateState As String
            Dim _TimeState As String

            _Qry = "SELECT " & HI.UL.ULDate.FormatDateDB
            _DateState = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")

            _Qry = "SELECT " & HI.UL.ULDate.FormatTimeDB
            _TimeState = HI.Conn.SQLConn.GetField(_Qry, Conn.DB.DataBaseName.DB_SYSTEM, "")
            Dim _UserMailTo As String
            Dim _RawMatCode As String
            Dim _RawMatColor As String
            Dim _RawMatSize As String
            Dim _Unit As String

            For Each R As DataRow In _dtdata.Select("FTSelect='1'")
                _UserMailTo = R!FTReceiveBy.ToString
                _FTReceiveNo = R!FTReceiveNo.ToString
                _RawMatCode = R!FTRawMatCode.ToString
                _RawMatColor = R!FTRawMatColorCode.ToString
                _RawMatSize = R!FTRawMatSizeCode.ToString
                _Unit = R!FTUnitCode.ToString
                _AppQty = Double.Parse(Val(R!FNApproveRcvQty.ToString))

                If _UserMailTo <> "" Then

                    Dim tmpsubject As String = ""
                    Dim tmpmessage As String = ""

                    If Not (StateReject) Then

                        tmpsubject = "Approved Receive Over Receive No " & _FTReceiveNo & "    "

                        tmpmessage = "Approved Receive Over Receive No " & _FTReceiveNo & "    "
                        tmpmessage &= vbCrLf & "Date :" & HI.UL.ULDate.ConvertEN(_DateState) & " " & _TimeState
                        tmpmessage &= vbCrLf & "By :" & HI.ST.UserInfo.UserName
                        tmpmessage &= vbCrLf & "Rawmat Code  :" & _RawMatCode
                        tmpmessage &= vbCrLf & "Rawmat Color :" & _RawMatColor
                        tmpmessage &= vbCrLf & "Rawmat Size  :" & _RawMatSize
                        tmpmessage &= vbCrLf & "Unit         :" & _Unit
                        tmpmessage &= vbCrLf & "Approve Quantity :" & _AppQty.ToString
                    Else
                        tmpsubject = "Rejected Receive Over Receive No " & _FTReceiveNo & "    "

                        tmpmessage = "Rejected Receive Over Receive No " & _FTReceiveNo & "    "
                        tmpmessage &= vbCrLf & "Date :" & HI.UL.ULDate.ConvertEN(_DateState) & " " & _TimeState
                        tmpmessage &= vbCrLf & "By :" & HI.ST.UserInfo.UserName
                        tmpmessage &= vbCrLf & "Rawmat Code  :" & _RawMatCode
                        tmpmessage &= vbCrLf & "Rawmat Color :" & _RawMatColor
                        tmpmessage &= vbCrLf & "Rawmat Size  :" & _RawMatSize
                        tmpmessage &= vbCrLf & "Unit         :" & _Unit
                        tmpmessage &= vbCrLf & "Approve Quantity :0 "
                    End If


                    If HI.Mail.ClsSendMail.SendMail(HI.ST.UserInfo.UserName, _UserMailTo, tmpsubject, tmpmessage, -1, "") Then
                    End If

                End If
            Next

            _dtdata.BeginInit()
            For Each R As DataRow In _dtdata.Select("FTSelect='1'")
                R.Delete()
            Next
            _dtdata.EndInit()

            ogcrcv.DataSource = _dtdata
            ogcrcv.Refresh()
            CType(ogcrcv.DataSource, DataTable).AcceptChanges()
            _Spls.Close()
            Return True
        Catch ex As Exception
            _Spls.Close()

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            Return False
        End Try
      
    End Function

    Private Sub ocmreceive_Click(sender As System.Object, e As System.EventArgs) Handles ocmapprove.Click

        If Me.ValidateData Then

            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการอนุมัติการรับเกินใช่หรือไม่ ?", 1410070009) = True Then
                If Me.SaveData() = True Then
                    HI.MG.ShowMsg.mInfo("ระบบได้ทำการอนุมัติการรับเกินเรียบร้อยแล้ว !!!", 1410070011, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    If CType(ogcrcv.DataSource, DataTable).Rows.Count <= 0 Then
                        Me.ProcessProc = True
                        Me.Close()

                    End If
                Else
                    HI.MG.ShowMsg.mInfo("ไม่สามารถทำการอนุมัติการรับเกินได้ เนื่องจากพบข้อผิดพรลาดบางประการ กรุณาทำการติดต่อ Admin", 1410070010, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If
            End If
           
        End If

    End Sub

    Private Sub ocmreject_Click(sender As Object, e As EventArgs) Handles ocmreject.Click

        If Me.ValidateData Then

            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Reject การรับเกินใช่หรือไม่ ?", 1410070012) = True Then
                If Me.SaveData(True) = True Then
                    HI.MG.ShowMsg.mInfo("ระบบได้ทำการ Reject การรับเกินเรียบร้อยแล้ว !!!", 1410070013, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    If CType(ogcrcv.DataSource, DataTable).Rows.Count <= 0 Then
                        Me.ProcessProc = True
                        Me.Close()

                    End If
                Else
                    HI.MG.ShowMsg.mInfo("ไม่สามารถทำการ Reject การรับเกินได้ เนื่องจากพบข้อผิดพรลาดบางประการ กรุณาทำการติดต่อ Admin", 1410070014, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If
            End If

        End If
    End Sub

    Private Sub ocmcancel_Click(sender As System.Object, e As System.EventArgs) Handles ocmcancel.Click
        Me.ProcessProc = False
        Me.Close()
    End Sub

    Private Sub ResFTStateSelect_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles ResFTStateSelect.EditValueChanging
        Select Case e.NewValue.ToString
            Case "1"
        End Select
    End Sub

    Private Sub FTStaReceiveAll_CheckedChanged(sender As Object, e As EventArgs) Handles FTSelectAll.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.FTSelectAll.Checked Then
                _State = "1"
            End If

            With ogcrcv
                If Not (.DataSource Is Nothing) And ogvrcv.RowCount > 0 Then

                    With ogvrcv
                        For I As Integer = 0 To .RowCount - 1

                            Dim _RcvOverQty As Double = 0

                            If _State = "1" Then
                                _RcvOverQty = .GetRowCellValue(I, "FNTotalRcvQty")
                            End If

                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FTSelect"), _State)
                            .SetRowCellValue(I, .Columns.ColumnByFieldName("FNApproveRcvQty"), _RcvOverQty)

                        Next

                    End With

                    CType(.DataSource, DataTable).AcceptChanges()
                End If

            End With
        Catch ex As Exception

        End Try
    End Sub

   
End Class