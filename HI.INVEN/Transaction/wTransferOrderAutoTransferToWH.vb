Public Class wTransferOrderAutoTransferToWH

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private _ReceiveNo As String = ""
    Public Property ReceiveNo() As String
        Get
            Return _ReceiveNo
        End Get
        Set(value As String)
            _ReceiveNo = value
        End Set
    End Property

    Private _SysWHID As Integer = 0
    Public Property WHID() As Integer
        Get
            Return _SysWHID
        End Get
        Set(value As Integer)
            _SysWHID = value
        End Set
    End Property

    Private _FNPriceTrans As Double = -1
    Public Property FNPriceTrans As Double
        Get
            Return _FNPriceTrans
        End Get
        Set(value As Double)
            _FNPriceTrans = value
        End Set
    End Property

    Private _mPriceClosed1 As Double = -1
    Public Property PriceClosed1 As Double
        Get
            Return _mPriceClosed1
        End Get
        Set(value As Double)
            _mPriceClosed1 = value
        End Set
    End Property


    Private _mPriceClosed2 As Double = -1
    Public Property PriceClosed2 As Double
        Get
            Return _mPriceClosed2
        End Get
        Set(value As Double)
            _mPriceClosed2 = value
        End Set
    End Property

    Private Sub ocmcancel_Click(sender As Object, e As EventArgs) Handles ocmcancel.Click
        Me.Close()
    End Sub

    Private Function AutoTransfer() As Boolean

        Dim _Spls As New HI.TL.SplashScreen("Auto Transfer To WH.. Please Wait.... ")
        Try
            Dim dtwh As New DataTable
            dtwh.Columns.Add("FNHSysWHId", GetType(Integer))
            With CType(Me.ogcbarcode.DataSource, DataTable)
                .AcceptChanges()

                dtwh.BeginInit()
                For Each R As DataRow In .Select("FTSelect='1'")
                    If dtwh.Select("FNHSysWHId=" & Integer.Parse(Val(R!FNHSysWHId.ToString)) & "").Length <= 0 Then
                        dtwh.Rows.Add(Integer.Parse(Val(R!FNHSysWHId.ToString)))
                    End If
                Next
                dtwh.EndInit()

            End With


            Dim _Qry As String = ""
            Dim _DocNo As String = ""
            Dim _tmpDocNo As String = ""
            Dim _CmpH As String = HI.Conn.SQLConn.GetField("SELECT TOP 1 FTDocRun FROM [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) & "].dbo.TCNMCmp WHERE FNHSysCmpId=" & Val(HI.ST.SysInfo.CmpID) & " ", Conn.DB.DataBaseName.DB_SYSTEM, "")


            HI.Conn.DB.ConnectionString(Conn.DB.DataBaseName.DB_SYSTEM)
            HI.Conn.SQLConn.SqlConnectionOpen()
            HI.Conn.SQLConn.Cmd = HI.Conn.SQLConn.Cnn.CreateCommand
            HI.Conn.SQLConn.Tran = HI.Conn.SQLConn.Cnn.BeginTransaction


            For Each Rx As DataRow In dtwh.Rows

                Me.WHID = Integer.Parse(Val(Rx!FNHSysWHId.ToString))
                _tmpDocNo = HI.TL.Document.GetDocumentNoOnBeginTrans(HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN), "TINVENTransferWH", "", False, _CmpH & "A").ToString()

                If _DocNo = "" Then
                    _DocNo = _tmpDocNo
                Else
                    _DocNo = _DocNo & "," & _tmpDocNo
                End If

                _Qry = "INSERT INTO   [" & HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_INVEN) & "].[dbo].[TINVENTransferWH]"
                _Qry &= vbCrLf & " ("
                _Qry &= vbCrLf & "FTInsUser, FDInsDate, FTInsTime, FTTransferWHNo, FDTransferWHDate, FTTransferWHBy, FNHSysWHId, FNHSysWHIdTo, FTRemark, FNHSysCmpId "
                _Qry &= vbCrLf & "  )"
                _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "'"
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatTimeDB & " "
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(_tmpDocNo) & "' "
                _Qry &= vbCrLf & " ," & HI.UL.ULDate.FormatDateDB & " "
                _Qry &= vbCrLf & " ,'" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                _Qry &= vbCrLf & " ," & Me.WHID & " "
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(Me.FNHSysWHIdTo.Properties.Tag.ToString)) & " "
                _Qry &= vbCrLf & " ,N'" & HI.UL.ULF.rpQuoted(Me.FTRemark.Text) & "' "
                _Qry &= vbCrLf & " ," & Integer.Parse(Val(HI.ST.SysInfo.CmpID)) & " "

                If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                    HI.Conn.SQLConn.Tran.Rollback()
                    HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                    HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                    _Spls.Close()
                    Return False
                End If

                With CType(Me.ogcbarcode.DataSource, DataTable)
                    .AcceptChanges()

                    For Each R As DataRow In .Select("FTSelect='1' AND FNHSysWHId=" & Val(Me.WHID) & "")

                        _Qry = " INSERT INTO [" & HI.Conn.DB.GetDataBaseName(HI.Conn.DB.DataBaseName.DB_INVEN) & "].dbo.TINVENBarcode_OUT(FTInsUser, FDInsDate, FTInsTime, FTBarcodeNo, FTDocumentNo, FNHSysWHId, FTOrderNo, FNQuantity,  FTStateReserve,FTDocumentRefNo,FNHSysCmpId,FNPriceTrans)  "
                        _Qry &= vbCrLf & " SELECT '" & HI.UL.ULF.rpQuoted(HI.ST.UserInfo.UserName) & "' "
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatDateDB & " "
                        _Qry &= vbCrLf & "," & HI.UL.ULDate.FormatTimeDB & " "
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTBarcodeNo.ToString) & "' "
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(_tmpDocNo) & "' "
                        _Qry &= vbCrLf & "," & Val(Me.WHID) & " "
                        _Qry &= vbCrLf & ",'" & HI.UL.ULF.rpQuoted(R!FTOrderNo.ToString) & "' "
                        _Qry &= vbCrLf & "," & Double.Parse(Val(R!FNQuantity.ToString)) & " "
                        _Qry &= vbCrLf & ",'','" & HI.UL.ULF.rpQuoted(Me.ReceiveNo) & "'," & Val(HI.ST.SysInfo.CmpID) & " "
                        _Qry &= vbCrLf & ",CASE WHEN " & Double.Parse(Val(R!FNPriceTrans.ToString)) & "<0 THEN NULL ELSE " & Double.Parse(Val(R!FNPriceTrans.ToString)) & "  END "

                        If HI.Conn.SQLConn.Execute_Tran(_Qry, HI.Conn.SQLConn.Cmd, HI.Conn.SQLConn.Tran) <= 0 Then
                            HI.Conn.SQLConn.Tran.Rollback()
                            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
                            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
                            _Spls.Close()
                            Return False
                        End If

                    Next

                End With
            Next


            HI.Conn.SQLConn.Tran.Commit()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()

            HI.MG.ShowMsg.mInfo("Transfer Auto Complete... ", 1410020005, Me.Text, _DocNo, System.Windows.Forms.MessageBoxIcon.Information)

            Return True

        Catch ex As Exception

            HI.Conn.SQLConn.Tran.Rollback()
            HI.Conn.SQLConn.DisposeSqlTransaction(HI.Conn.SQLConn.Tran)
            HI.Conn.SQLConn.DisposeSqlConnection(HI.Conn.SQLConn.Cmd)
            _Spls.Close()
            Return False

        End Try

        Return True
    End Function

    Private Sub ocmauto_Click(sender As Object, e As EventArgs) Handles ocmauto.Click
        If Me.FNHSysWHIdTo.Text <> "" And FNHSysWHIdTo.Properties.Tag.ToString <> "" Then
            With CType(ogcbarcode.DataSource, DataTable)
                .AcceptChanges()
                If .Select("FTSelect='1'").Length <= 0 Then
                    HI.MG.ShowMsg.mInfo("กรุณาทำการเลือก Barcode ที่ต้องการทำการ Transfer !!!", 1410020011, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End With

            If HI.MG.ShowMsg.mConfirmProcess("คุณต้องการทำการ Auto Transfer To WH ใช่หรือไม่ ?", 1471720001) = True Then

                If Me.AutoTransfer Then
                    Me.Close()
                Else
                    HI.MG.ShowMsg.mInfo("ระบบไม่สามารถทำการ Auto Transfer ได้ เนื่องจากพบข้อผิดพลาดบางประการกรูณาทำการติดต่อ System Admin !!!", 1471720002, Me.Text, , System.Windows.Forms.MessageBoxIcon.Warning)
                End If

            End If
        Else
            HI.MG.ShowMsg.mInvalidData(MG.ShowMsg.InvalidType.InputData, Me.Text, Me.FNHSysWHIdTo_lbl.Text)
            FNHSysWHIdTo.Focus()
        End If
    End Sub

    Private Sub wReceiveAutoTransferToCenter_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.FNHSysCmpId.Text = HI.ST.SysInfo.CmpID.ToString
    End Sub

    Private Sub FTStaSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles FTStaSelectAll.CheckedChanged
        Try

            Dim _State As String = "0"
            If Me.FTStaSelectAll.Checked Then
                _State = "1"
            End If

            With ogcbarcode
                If Not (.DataSource Is Nothing) And ogvbarcode.RowCount > 0 Then

                    With ogvbarcode
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